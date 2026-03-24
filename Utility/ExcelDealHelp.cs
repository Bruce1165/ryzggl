using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace Utility
{
    public class ExcelDealHelp
    {
        /// <summary>
        /// 从excel中导入数据到临时DataSet中
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="whereString">过滤条件，如“where (户主姓名='')”</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportExcell(string strFilePath, string whereString)
        {
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            using (OleDbConnection OleConn = new OleDbConnection(strConn))
            {
                OleConn.Open();

                System.Text.StringBuilder sql = new System.Text.StringBuilder();
                DataTable dt = new DataTable();
                dt = OleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);//读取sheet个数
                OleConn.Close();
              
                if ((dt != null) && (dt.Rows.Count > 0))
                {//读取excel中各sheet名称
                    DataSet OleDsExcle = new DataSet();
                    foreach (DataRow dr in dt.Rows)
                    {
                        using (OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}] {1}", dr["TABLE_NAME"], whereString), OleConn))
                        {
                            OleDaExcel.Fill(OleDsExcle, dr["TABLE_NAME"].ToString().Replace("$",""));
                            OleConn.Close();
                        }

                        //sql.Append("SELECT * FROM [").Append(dr["TABLE_NAME"].ToString()).Append("] ").Append(whereString).Append(";");
                        //break;
                    }
                  

                    //删除临时文件
                    if (strFilePath != "")
                    {
                        FileInfo file = new FileInfo(strFilePath);
                        if (file.Exists)
                            file.Delete();
                    }
                    //if (OleDsExcle != null && OleDsExcle.Tables.Count > 0 && OleDsExcle.Tables[0].Rows.Count > 0)
                    //{
                    //    OleDsExcle.Tables[0].Rows.RemoveAt(0);//删除行头
                    //}
                    return OleDsExcle;
                }
                else//没有任何数据
                {
                    if (OleConn.State == System.Data.ConnectionState.Open)
                        OleConn.Close();
                    //删除临时文件
                    if (strFilePath != "")
                    {
                        FileInfo file = new FileInfo(strFilePath);
                        if (file.Exists)
                            file.Delete();
                    }
                    return null;
                }
            }
        }
      
         /// <summary>
        /// 将服务器端Excel输出到客户端,注意文件名不能过长
        /// </summary>
        /// <param name="strFile">Excel文件路径</param>
        public static void ExportExcel(System.Web.UI.Page page, string strFile, string newName)
        {
            FileStream fs = null;
            try
            {
                page.Response.Clear();
                page.Response.ClearHeaders();
                page.Response.Charset = "GB2312";             
                page.Response.ContentEncoding = Encoding.UTF8;//Encoding.GetEncoding("GB2312");
                //page.Response.HeaderEncoding = Encoding.GetEncoding("GB2312"); ;
                page.Response.ContentType = "application/octet-stream";
                FileInfo fi = new FileInfo(strFile);
                string fname = newName;// fi.Name;
                 //if (page.Request.Browser.Browser.Trim().ToUpper() == "IE") fname = System.Web.HttpUtility.UrlEncode(fname, Encoding.UTF8);// System.Text.Encoding.UTF8
                fname = System.Web.HttpUtility.UrlEncode(fname, Encoding.UTF8);// System.Text.Encoding.UTF8
                 page.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + fname +"\"");
                page.Response.AppendHeader("Content-Length", fi.Length.ToString());
               
                byte[] tmpbyte = new byte[1024 * 8];

                fs = fi.OpenRead();
                int count;
                while ((count = fs.Read(tmpbyte, 0, tmpbyte.Length)) > 0)
                {
                    page.Response.BinaryWrite(tmpbyte);
                    page.Response.Flush();
                }

                page.Response.End();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }


        /// <summary>
        /// 使用OLEDB导出Excel
        /// Excel程序支持的文件类型不止一种。
        /// 在excel早期版本中，默认的工作薄扩展名为".xls"，这种格式的文件最多可以包含255个工作页(Worksheet)，每个zhidao工作页中包含65535行(Row)和256列(Column)。
        /// 自Office2007版本起，excel默认的工作薄扩展名为".xlsx"，这种格式的文件中每个工作页包含1048576行(Row),16384列(Column)。
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="filepath">文件目录和文件名</param>
        /// <param name="tablename">SHEET页名称</param>
        /// <param name="pagesize">每页记录数</param>
        public static void ExportToExcel(DataTable dt, string filepath, string tablename, int pagesize = 0)
        {
            int pagecount = 0;
            string connString = GetExcelConnStr(filepath, out pagecount);
            if (pagesize > 0)
            {
                pagecount = pagesize;
            }

            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))
                {         
                    con.Open();                   
                    DataTable dtSheet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    var sheetCount = dtSheet.Rows.Count;

                    //总记录数
                    var recordCount = dt.Rows.Count;
                    //列数
                    var columnCount = dt.Columns.Count;

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;

                    //开始分页
                    int page = (recordCount + pagecount - 1) / pagecount; //总页数
                    for (int i = 0; i < page; i++)
                    {
                        //新的Sheet名称
                        string tabname = tablename + (i + 1).ToString();

                        //获取已存在的表
                        if (sheetCount > 0)
                        {
                            int m = 0;
                            foreach (DataRow dr in dtSheet.Rows)
                            {
                                if (m == i)
                                {
                                    tabname = dr["TABLE_NAME"].ToString();

                                    cmd.CommandText = "DROP TABLE [" + tabname + "]";
                                    cmd.ExecuteNonQuery();// 执行创建sheet的语句
                                }
                                m++;
                            }
                        }

                        //建新sheet和表头
                        StringBuilder createSQL = new StringBuilder();
                        createSQL.Append("CREATE TABLE ").Append("[" + tabname + "]"); //每60000项建一页
                        createSQL.Append("(");
                        for (int j = 0; j < columnCount; j++)
                        {
                            createSQL.Append("[" + dt.Columns[j].ColumnName + "] text,");
                        }
                        createSQL = createSQL.Remove(createSQL.Length - 1, 1);
                        createSQL.Append(")");

                        cmd.CommandText = createSQL.ToString();
                        cmd.ExecuteNonQuery();


                        StringBuilder strfield = new StringBuilder();
                        for (int z = 0; z < columnCount; z++)
                        {
                            if (z > 0)
                            {
                                strfield.Append(",");
                            }
                            strfield.Append("[" + dt.Columns[z].ColumnName + "]");
                        }

                        //准备逐条插入数据
                        for (int j = i * pagecount; j < (i + 1) * pagecount; j++)
                        {
                            if (i == 0 || j < recordCount)
                            {
                                StringBuilder insertSQL = new StringBuilder();
                                StringBuilder strvalue = new StringBuilder();
                                for (int z = 0; z < columnCount; z++)
                                {
                                    if (z > 0)
                                    {
                                        strvalue.Append(",");
                                    }
                                    strvalue.Append("'" + dt.Rows[j][z].ToString() + "'");
                                }

                                insertSQL.Append(" insert into [" + tabname + "]( ")
                                        .Append(strfield.ToString())
                                        .Append(") values (").Append(strvalue).Append(") ");

                                cmd.CommandText = insertSQL.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("导出excel失败", ex);
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据文件后缀名判断Excel版本 链接字符串
        /// 参数HDR的值：
        /// HDR=Yes，这代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。
        /// 参数IMEX的值：
        /// 当 IMEX = 0 时为“汇出模式”，这个模式开启的 Excel 档案只能用来做“写入”用途。
        /// 当 IMEX = 1 时为“汇入模式”，这个模式开启的 Excel 档案只能用来做“读取”用途。
        /// 当 IMEX = 2 时为“链接模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。
        /// </summary>
        /// <param name="filepath">文件目录和文件名</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public static string GetExcelConnStr(string filepath, out int pagesize)
        {
            StringBuilder sb = new StringBuilder();
            string extension = Path.GetExtension(filepath);
            if (extension == ".xlsx")
            {
                pagesize = 1048575; //实际行数 1048576
                sb.Append("Provider=Microsoft.Ace.OleDb.12.0;Data Source=");
                sb.Append(filepath);
                sb.Append(";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2'");
            }
            else
            {
                pagesize = 65535; //实际行数 65536
                sb.Append("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sb.Append(filepath);
                sb.Append(";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2'");
            }
            return sb.ToString();
        }
    }
}
