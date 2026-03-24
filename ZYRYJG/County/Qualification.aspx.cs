using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DataAccess;
using Model;

namespace ZYRYJG.County
{
    //资格校验
    public partial class Qualification : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!this.IsPostBack)
           {
               ButtonQuery_Click(sender, e);
           }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/") + "/" + FileUpload1.FileName;
                    FileUpload1.SaveAs(filepath);
                    DataSet ds = ExcelDealHelp.ImportExcell(filepath, null);
                    //DeleteAdd(ds);
                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                    {
                        UIHelp.layerAlert(Page, "没有发现需要导入的有效数据");
                        return;
                    }
                    System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
                    System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息
                    string zy ="市政，矿业，建筑，机电，水利，公路";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)                  
                    {
                        rowErr.Remove(0, rowErr.Length);//行错误清空

                         if (ds.Tables[0].Rows[i]["姓名"].ToString() == "" )
                        {
                            rowErr.Append("<br />姓名不能为空。");
                        }
                         if (ds.Tables[0].Rows[i]["证件号码"].ToString() == "")
                        {
                            rowErr.Append("<br />证件号码不能为空。");
                        }                        	

                        if (ds.Tables[0].Rows[i]["取得年份"].ToString() == "" )
                        {
                            rowErr.Append("<br />取得年份不能为空。");
                        }
                        else if (Utility.Check.IsNumber(ds.Tables[0].Rows[i]["取得年份"].ToString()) == false)
                        {                          
                            rowErr.Append("<br />取得年份必须位4位数字年份。");
                        }
                        else if (1950 > Convert.ToInt32(ds.Tables[0].Rows[i]["取得年份"]) || Convert.ToInt32(ds.Tables[0].Rows[i]["取得年份"])  > DateTime.Now.Year)
                        {
                            rowErr.Append("<br />取得年份疑似不正确，请检查。");
                        }

                        if (ds.Tables[0].Rows[i]["管理号"].ToString() == "")
                        {
                            rowErr.Append("<br />管理号不能为空。（可以与资格证书编号保持一致）");
                        }

                        if (ds.Tables[0].Rows[i]["资格证书编号"].ToString() == "")
                        {
                            rowErr.Append("<br />资格证书编号不能为空（可以与管理号保持一致）。");
                        }
                        

                        if(ds.Tables[0].Rows[i]["专业类别"].ToString()=="" || zy.Contains(ds.Tables[0].Rows[i]["专业类别"].ToString())==false)
                        {
                            rowErr.Append("<br />专业类别允许范围【市政，矿业，建筑，机电，水利，公路】。");
                        }

                        if (ds.Tables[0].Rows[i]["毕业时间"].ToString() == "")
                        {
                            rowErr.Append("<br />毕业时间不能为空。");
                        }
                        else if (Utility.Check.IfDateOrDateTimeFormat(ds.Tables[0].Rows[i]["毕业时间"].ToString()) == false)
                         {
                             rowErr.Append("<br />毕业时间必须是有效的日期格式（如2008-10-01）");
                         }

                        if (ds.Tables[0].Rows[i]["签发时间"].ToString() == "")
                        {
                            rowErr.Append("<br />签发时间不能为空。");
                        }
                        else if (Utility.Check.IfDateOrDateTimeFormat(ds.Tables[0].Rows[i]["签发时间"].ToString()) == false)
                        {
                            rowErr.Append("<br />签发时间必须是有效的日期格式（如2008-10-01）");
                        }

                        if (rowErr.Length > 0)
                        {
                            rtnErr.Append("<br />---第【").Append(Convert.ToString(i + 1)).Append("】行：姓名“").Append(ds.Tables[0].Rows[i]["姓名"].ToString()).Append("”-------------------------------");
                            rtnErr.Append(rowErr.ToString());
                        }
                    }

                    if (rtnErr.Length > 0)
                    {
                        UIHelp.layerAlert(Page, rtnErr.ToString());
                        return;
                    }

                    QualificationDAL.ImportByExcel(ds.Tables[0]);
                    UIHelp.layerAlert(Page, string.Format("导入成功（共{0}条）", ds.Tables[0].Rows.Count));
                    ButtonQuery_Click(sender, e);
                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "资格库管理上传失败！", ex);
                throw ex;                
            }
        }

       

        ////资格库管理，增量，先删除原有的的数据，在全部新增
        //public void DeleteAdd(DataSet ds)
        //{
        //    DataTable dt = ds.Tables[0];
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //       // sb.Append("'").Append(dt.Rows[i][7].ToString()).Append("'").Append(",");
        //        sb.Append("'").Append(dt.Rows[i]["资格证书编号"].ToString()).Append("'").Append(",");
        //    }
        //    if (sb.Length > 0)
        //        sb.Remove(sb.Length - 1, 1);
        //    DBHelper db = new DBHelper();
        //    DbTransaction tran = db.BeginTransaction();
        //    try
        //    {
        //        //先删除掉原来有的东西
        //        QualificationDAL.DeleteAdd(tran,sb.ToString());
        //        //DataAccess.CommonDAL.ExecSQL(tran, "DELETE FROM dbo.Qualification WHERE ZGZSBH IN ("+sb.ToString()+")");
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            QualificationMDL _QualificationMDL = new QualificationMDL();
        //            //_QualificationMDL.XM = dt.Rows[j][0].ToString();
        //            //_QualificationMDL.ZJHM = dt.Rows[j][1].ToString(); 
        //            //_QualificationMDL.SF = dt.Rows[j][2].ToString();
        //            //_QualificationMDL.GZDW = dt.Rows[j][3].ToString();
        //            //_QualificationMDL.QDNF = dt.Rows[j][4].ToString();
        //            //_QualificationMDL.QDFS = dt.Rows[j][5].ToString();
        //            //_QualificationMDL.GLH = dt.Rows[j][6].ToString();
        //            //_QualificationMDL.ZGZSBH = dt.Rows[j][7].ToString();
        //            //_QualificationMDL.ZYLB = dt.Rows[j][8].ToString();
        //            //_QualificationMDL.BYXX = dt.Rows[j][9].ToString();
        //            //_QualificationMDL.BYSJ =Convert.ToDateTime(dt.Rows[j][10].ToString());
        //            //_QualificationMDL.SXZY = dt.Rows[j][11].ToString();
        //            //_QualificationMDL.ZGXL = dt.Rows[j][12].ToString();
        //            //_QualificationMDL.QFSJ = Convert.ToDateTime(dt.Rows[j][13].ToString());

        //            _QualificationMDL.XM = dt.Rows[j]["姓名"].ToString();
        //            _QualificationMDL.ZJHM = dt.Rows[j]["证件号码"].ToString();

        //            if (dt.Rows[j]["省份"]!=DBNull.Value)
        //            {
        //                 _QualificationMDL.SF = dt.Rows[j]["省份"].ToString();
        //            }

        //            if (dt.Rows[j]["工作单位"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.GZDW = dt.Rows[j]["工作单位"].ToString();
        //            }

        //            if (dt.Rows[j]["取得年份"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.QDNF = dt.Rows[j]["取得年份"].ToString();
        //            }
                  
        //            if (dt.Rows[j]["取得方式"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.QDFS = dt.Rows[j]["取得方式"].ToString();
        //            }
        //            if (dt.Rows[j]["管理号"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.GLH = dt.Rows[j]["管理号"].ToString();
        //            }
                    
        //            _QualificationMDL.ZGZSBH = dt.Rows[j]["资格证书编号"].ToString();
                    
                   
        //            if (dt.Rows[j]["专业类别"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.ZYLB = dt.Rows[j]["专业类别"].ToString();
        //            }
                   
        //            if (dt.Rows[j]["毕业学校"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.BYXX = dt.Rows[j]["毕业学校"].ToString();
        //            }
                   
        //            if (dt.Rows[j]["毕业时间"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.BYSJ = Convert.ToDateTime(dt.Rows[j]["毕业时间"].ToString());
        //            }
                   
                
        //             _QualificationMDL.SXZY = dt.Rows[j]["所学专业"].ToString();
                    
        //            if (dt.Rows[j]["最高学历"]!=DBNull.Value)
        //            {
        //                _QualificationMDL.ZGXL = dt.Rows[j]["最高学历"].ToString();
        //            }
        //            _QualificationMDL.QFSJ = Convert.ToDateTime(dt.Rows[j]["签发时间"].ToString());
        //            QualificationDAL.Insert(tran, _QualificationMDL);
        //        }
        //        tran.Commit();
        //        UIHelp.WriteOperateLog(UserName, UserID, "资格校验数据导入更新成功", string.Format("更新时间：{0}", DateTime.Now));
        //        UIHelp.layerAlert(Page, "数据导入更新成功！",6,2000);
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        UIHelp.WriteErrorLog(Page, "资格库管理删除新增失败！", ex);
        //    }
        //}

        protected void ButtonQuery_Click(object sender, EventArgs e)
          {
              if (UIHelp.CheckSQLParam() == false)
              {
                  UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                  return;
              }
              ObjectDataSource1.SelectParameters.Clear();
              var q = new QueryParamOB();
              if (RadComboBoxItem.SelectedValue!=null && RadTextBoxZJHM.Text.Trim() != "")
              {
                  q.Add(string.Format("{1} = '{0}'", RadTextBoxZJHM.Text.Trim(),RadComboBoxItem.SelectedValue));
              }
             ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
             RadGridZGGL.CurrentPageIndex = 0;
             RadGridZGGL.DataSourceID = ObjectDataSource1.ID;
          }
    }
}