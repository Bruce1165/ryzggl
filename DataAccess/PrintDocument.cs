using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Linq;
using Novacode;

namespace DataAccess
{
    public static class PrintDocument
    {
        /// <summary>
        /// 写数据到Word
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="printTable">DataTable</param>
        public static void CreateDataToWord(string sourceFile, string fileName, DataTable printTable)
        {
            if (printTable == null)
            {
                throw new Exception("获取打印数据失败！");
            }
            if (printTable.Rows.Count == 0)
            {
                throw new Exception("获取打印数据失败！");
            }

            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");

            using (DocX document = DocX.Load(sourceFile))
            {
                foreach (DataColumn col in printTable.Columns)
                {
                    document.ReplaceText("{$" + col.ColumnName + "$}", printTable.Rows[0][col.ColumnName].ToString());
                }

                document.SaveAs(targetFileName);
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }

        /// <summary>
        /// 写数据到Word
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="entity">实体</param>
        public static void CreateDataToWordByEntity(string sourceFile, string fileName, Object entity)
        {
            if (entity == null)
            {
                throw new Exception("获取打印数据失败！");
            }
            var ht = GetProperties(entity);
            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");

            using (DocX document = DocX.Load(sourceFile))
            {

                foreach (string propertyName in ht.Keys)
                {
                    document.ReplaceText("{$" + propertyName + "$}", ht[propertyName].ToString());
                   
                }

                //标签
                foreach (Bookmark bookmark in document.Bookmarks)
                {
                    if (ht.ContainsKey(bookmark.Name))
                    {
                        document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name].ToString());
                    }
                }

                document.SaveAs(targetFileName);
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }
        /// <summary>
        /// 写数据到Word
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="entity">泛型集合</param>
        public static void CreateDataToWordByEntity(string sourceFile, string fileName, List<Object> entity)
        {
            if (entity == null)
            {
                throw new Exception("获取打印数据失败！");
            }
            var ht = GetProperties(entity);
            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");

            using (DocX document = DocX.Load(sourceFile))
            {

                foreach (string propertyName in ht.Keys)
                {
                    document.ReplaceText("{$" + propertyName + "$}", ht[propertyName].ToString());
                }

                //标签
                foreach (Bookmark bookmark in document.Bookmarks)
                {
                    if (ht.ContainsKey(bookmark.Name))
                    {
                        document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name].ToString());
                    }
                }

                document.SaveAs(targetFileName);
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }


        /// <summary>
        /// 写数据到Word（待图片photo，photo_new）
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="ht">Hashtable键值对(ht["isCtable"]是否含有数据表table，
        ///                                  ht["tableList"] 数据表集合，
        ///                                  ht["tableIndex"]：数据表在word中的索引,
        ///                                  ht["insertIndex"]：开始插入行的序号,
        ///                                  ht["ContainsHeader"] true:含有，false 没有
        ///                                  ht["ContextStyle"]:内容样式,值为Dictionary（列序号（从0开始），样式集合表,目前只提供 字体、字号
        ///                                                             举例：ht["FontFamily"]="宋体" ht["FontSize"]=16（磅）)
        /// 注意：ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 都是List
        ///       ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 在List中的顺序由ht["tableIndex"]来决定
        /// ht["ContextStyle"] 数据结构：表序号,《列序号，样式HashTable》
        /// </param>
        public static void CreateDataToWordByHashtable(string sourceFile, string fileName, Hashtable ht)
        {
            if (ht == null || ht.Keys.Count == 0)
            {
                throw new Exception("获取打印数据失败！");
            }
            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");

            using (DocX document = DocX.Load(sourceFile))
            {
                foreach (string propertyName in ht.Keys)
                {
                    //document.ReplaceText("{$" + propertyName + "$}",
                    //    ht[propertyName] ==null? "" : ht[propertyName].ToString());

                    document.ReplaceText(string.Format("{{${0}$}}",propertyName ),
                        ht[propertyName] ==null? "" : ht[propertyName].ToString().Replace("\r\n","\r"));

                }
                //标签
                foreach (Bookmark bookmark in document.Bookmarks)
                {
                    //if (ht.ContainsKey(bookmark.Name))
                    //{
                    //    document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                    //        ? ""
                    //        : ht[bookmark.Name].ToString());
                    //}

                    //if (ht["photo"].ToString()!= "")
                    //{
                    //    Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(ht["photo"].ToString()));
                    //    Picture pPicture = pImag.CreatePicture();
                    //    pPicture.Height = 160;
                    //    pPicture.Width = 120;
                    //    bookmark.Paragraph.InsertPicture(pPicture);
                    //}
                    if (bookmark.Name == "photo" && ht["photo"]!=null && ht["photo"].ToString() != "")
                    {
                        Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(Utility.ImageHelp.GetUrlNoParam(ht["photo"].ToString())));
                        Picture pPicture = pImag.CreatePicture();
                        pPicture.Height = 140;
                        pPicture.Width = 110;
                        bookmark.Paragraph.InsertPicture(pPicture);
                    }
                    if (bookmark.Name == "photo_new" && ht["photo_new"] !=null && ht["photo_new"].ToString() != "")
                    {
                        Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(Utility.ImageHelp.GetUrlNoParam(ht["photo_new"].ToString())));
                        Picture pPicture = pImag.CreatePicture();
                        pPicture.Height = 140;
                        pPicture.Width = 110;
                        bookmark.Paragraph.InsertPicture(pPicture);
                    }
                    if (bookmark.Name == "photo_code" && ht["photo_code"] != null && ht["photo_code"].ToString() != "")
                    {
                        Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(Utility.ImageHelp.GetUrlNoParam(ht["photo_code"].ToString())));
                        Picture pPicture = pImag.CreatePicture();
                        pPicture.Height = 200;
                        pPicture.Width = 200;
                        bookmark.Paragraph.InsertPicture(pPicture);
                    }
                    if (bookmark.Name == "photo_sign" && ht["photo_sign"] != null && ht["photo_sign"].ToString() != "")
                    {
                        Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(Utility.ImageHelp.GetUrlNoParam(ht["photo_sign"].ToString())));
                        Picture pPicture = pImag.CreatePicture();
                        pPicture.Height = 43;
                        pPicture.Width = 99;
                        bookmark.Paragraph.InsertPicture(pPicture);
                    }
                    if (bookmark.Name == "photo_signnew" && ht["photo_signnew"] != null && ht["photo_signnew"].ToString() != "")
                    {
                        Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(Utility.ImageHelp.GetUrlNoParam(ht["photo_signnew"].ToString())));
                        Picture pPicture = pImag.CreatePicture();
                        pPicture.Height = 43;
                        pPicture.Width = 99;
                        bookmark.Paragraph.InsertPicture(pPicture);
                    }
                }

                //word 里面含有表格
                if (ht.ContainsKey("isCtable"))
                {
                    if ((bool)ht["isCtable"])
                    {
                        //word中表格个数
                        var tableCount = document.Tables.Count;
                        var indexlist = ht["tableIndex"] as List<int>;

                        if (indexlist != null && tableCount >= indexlist.Count)
                        {
                            for (int i = 0; i < indexlist.Count; i++)
                            {
                                var index = indexlist[i];
                                var wordTable = document.Tables[index];
                                wordTable.Alignment= Alignment.center;

                                var listTable = ht["tableList"] as List<DataTable>;
                                if (listTable != null)
                                {
                                    var datadt = listTable[i];
                                    var chlist = ht["ContainsHeader"] as List<bool>;
                                    var insertindexList = ht["insertIndex"] as List<int>;
                                    var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                    //默认从第二行插入
                                    int insertNum = 2;
                                    if (insertindexList != null)
                                    {
                                        insertNum = insertindexList[i];
                                    }

                                    //遍历数据行
                                    for (int j = 0; j < datadt.Rows.Count; j++)
                                    {
                                        var rowInsertIndex = j + insertNum;
                                        Row r = wordTable.InsertRow(rowInsertIndex);
                                        for (int k = 0; k < r.Cells.Count; k++)
                                        {
                                            r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                            r.Cells[k].MarginBottom = 0;
                                            r.Cells[k].MarginLeft = 0;
                                            r.Cells[k].MarginRight = 0;
                                            r.Cells[k].MarginTop = 0;
                                            r.Cells[k].Paragraphs.First().Alignment = Alignment.center;

                                            //设置样式
                                            if (cStyle != null)
                                            {
                                                var c = cStyle[index];
                                                if (c != null)
                                                {
                                                    Hashtable sytyleHt = c[k];
                                                    if (sytyleHt.Contains("FontFamily"))
                                                    {
                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                        {
                                                            p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("FontSize"))
                                                    {
                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                        {
                                                            p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("Alignment"))
                                                    {
                                                        var t = sytyleHt["Alignment"].ToString();

                                                        switch (t)
                                                        {
                                                            case "both":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.both;
                                                                }
                                                                break;
                                                            case "center":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.center;
                                                                }
                                                                break;
                                                            case "left":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.left;
                                                                }
                                                                break;
                                                            case "right":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.right;
                                                                }
                                                                break;
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("TextDirection"))
                                                    {
                                                        var t = sytyleHt["TextDirection"].ToString();
                                                        switch (t)
                                                        {
                                                            case "btLr":
                                                                r.Cells[k].TextDirection = TextDirection.btLr;
                                                                break;
                                                            case "right":
                                                                r.Cells[k].TextDirection = TextDirection.right;
                                                                break;
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("VerticalAlignment"))
                                                    {
                                                        var v = sytyleHt["VerticalAlignment"].ToString();

                                                        switch (v)
                                                        {
                                                            case "Center":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                break;
                                                            case "Bottom":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                break;
                                                            case "Top":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    wordTable.AutoFit = AutoFit.Contents;

                                    wordTable.Alignment = Alignment.center;

                                    if (chlist != null)
                                    {
                                        //不含有表头
                                        if (!chlist[i])
                                        {
                                            if (insertNum.Equals(0))
                                            {
                                                //移除表格最后一项
                                                wordTable.RemoveRow(wordTable.RowCount - 1);
                                            }
                                            else
                                            {
                                                //移除表格第一项
                                                wordTable.RemoveRow(0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //设置word 受保护
                EditRestrictions erReadOnly = EditRestrictions.readOnly;
                document.AddProtection(erReadOnly ,"zyryjg_2017");
                document.SaveAs(targetFileName);                 
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }

        /// <summary>
        /// 写数据到Word
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="ht">
        /// Hashtable键值对(ht["isCtable"]是否含有数据表table，
        ///                 ht["tableList"] 数据表集合，
        ///                 ht["tableIndex"]：数据表在word中的索引,
        ///                 ht["insertIndex"]：开始插入行的序号,
        ///                 ht["ContainsHeader"] true:含有，false 没有
        ///                 ht["ContextStyle"]:内容样式,值为Dictionary（列序号（从0开始），样式集合表,目前只提供 字体、字号
        ///                                     举例：ht["FontFamily"]="宋体" ht["FontSize"]=16（磅）)
        /// 注意：ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 都是List
        ///       ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 在List中的顺序由ht["tableIndex"]来决定
        /// ht["ContextStyle"] 数据结构：表序号,《列序号，样式HashTable》
        /// </param>
        public static void CreateDataToWordByHashtable2(string sourceFile, string fileName, Hashtable ht)
        {
            if (ht == null || ht.Keys.Count == 0)
            {
                throw new Exception("获取打印数据失败！");
            }
            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");

            using (DocX document = DocX.Load(sourceFile))
            {
                foreach (string propertyName in ht.Keys)
                {
                    //document.ReplaceText("{$" + propertyName + "$}",
                    //    ht[propertyName] == null ? "" : ht[propertyName].ToString());

                    document.ReplaceText("{$" + propertyName + "$}",
                        ht[propertyName] == null ? "" : ht[propertyName].ToString().Replace("\r\n","\r"));                    
                }
                //标签
                foreach (Bookmark bookmark in document.Bookmarks)
                {
                    if (ht.ContainsKey(bookmark.Name))
                    {
                        document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                            ? ""
                            : ht[bookmark.Name].ToString());
                    }

                    //if (ht["photo"].ToString() != "")
                    //{
                    //    Novacode.Image pImag = document.AddImage(HttpContext.Current.Server.MapPath(ht["photo"].ToString()));
                    //    Picture pPicture = pImag.CreatePicture();
                    //    pPicture.Height = 160;
                    //    pPicture.Width = 120;
                    //    bookmark.Paragraph.InsertPicture(pPicture);
                    //}
                }

                //word 里面含有表格
                if (ht.ContainsKey("isCtable"))
                {
                    if ((bool)ht["isCtable"])
                    {
                        //word中表格个数
                        var tableCount = document.Tables.Count;
                        var indexlist = ht["tableIndex"] as List<int>;

                        if (indexlist != null && tableCount >= indexlist.Count)
                        {
                            for (int i = 0; i < indexlist.Count; i++)
                            {
                                var index = indexlist[i];
                                var wordTable = document.Tables[index];
                                var listTable = ht["tableList"] as List<DataTable>;
                                if (listTable != null)
                                {
                                    var datadt = listTable[i];
                                    var chlist = ht["ContainsHeader"] as List<bool>;
                                    var insertindexList = ht["insertIndex"] as List<int>;
                                    var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                    //默认从第二行插入
                                    int insertNum = 2;
                                    if (insertindexList != null)
                                    {
                                        insertNum = insertindexList[i];
                                    }

                                    //遍历数据行
                                    for (int j = 0; j < datadt.Rows.Count; j++)
                                    {
                                        var rowInsertIndex = j + insertNum;

                                        //Row r = wordTable.Rows[insertNum -1];
                                        //wordTable.Rows[rowInsertIndex -1].Xml.AddAfterSelf(r.Xml);
                                        //r = wordTable.Rows[rowInsertIndex];
                                        Row r = wordTable.InsertRow(rowInsertIndex);
                                        for (int k = 0; k < r.Cells.Count; k++)
                                        {
                                            if (k >= datadt.Columns.Count) break;
                                            r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                            r.Cells[k].MarginBottom = 0;
                                            r.Cells[k].MarginLeft = 0;
                                            r.Cells[k].MarginRight = 0;
                                            r.Cells[k].MarginTop = 0;

                                            //设置样式
                                            if (cStyle != null)
                                            {
                                                var c = cStyle[index];
                                                if (c != null)
                                                {
                                                    Hashtable sytyleHt = c[k];
                                                    if (sytyleHt.Contains("FontFamily"))
                                                    {
                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                        {
                                                            p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("FontSize"))
                                                    {
                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                        {
                                                            p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("Alignment"))
                                                    {
                                                        var t = sytyleHt["Alignment"].ToString();

                                                        switch (t)
                                                        {
                                                            case "both":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.both;
                                                                }
                                                                break;
                                                            case "center":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.center;
                                                                }
                                                                break;
                                                            case "left":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.left;
                                                                }
                                                                break;
                                                            case "right":
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Alignment = Alignment.right;
                                                                }
                                                                break;
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("TextDirection"))
                                                    {
                                                        var t = sytyleHt["TextDirection"].ToString();
                                                        switch (t)
                                                        {
                                                            case "btLr":
                                                                r.Cells[k].TextDirection = TextDirection.btLr;
                                                                break;
                                                            case "right":
                                                                r.Cells[k].TextDirection = TextDirection.right;
                                                                break;
                                                        }
                                                    }

                                                    if (sytyleHt.Contains("VerticalAlignment"))
                                                    {
                                                        var v = sytyleHt["VerticalAlignment"].ToString();

                                                        switch (v)
                                                        {
                                                            case "Center":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                break;
                                                            case "Bottom":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                break;
                                                            case "Top":
                                                                r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    wordTable.AutoFit = AutoFit.Contents;

                                    wordTable.Alignment = Alignment.center;

                                    if (chlist != null)
                                    {
                                        //不含有表头
                                        if (!chlist[i])
                                        {
                                            if (insertNum.Equals(0))
                                            {
                                                //移除表格最后一项
                                                wordTable.RemoveRow(wordTable.RowCount - 1);
                                            }
                                            else
                                            {
                                                //移除表格第一项
                                                wordTable.RemoveRow(0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //设置word 受保护
                EditRestrictions erReadOnly = EditRestrictions.readOnly;
                document.AddProtection(erReadOnly, "zyryjg_2017");
                document.SaveAs(targetFileName);
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }

        /// <summary>
        ///     流方式发送文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        private static void SendFile(object file, string fileName)
        {
            #region 流方式发送文件

            var r = new FileStream(file.ToString(), FileMode.Open);

            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                
                // 设置response的Header
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachment;filename=" + HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(fileName + ".docx")));
                HttpContext.Current.Response.AddHeader("Content-Length", r.Length.ToString());
                
                long bytelength = r.Length;
                if (bytelength < 10240) //10k字节缓冲
                {
                    var buffer = new byte[bytelength];
                    r.Read(buffer, 0, Convert.ToInt32(bytelength));
                    HttpContext.Current.Response.BinaryWrite(buffer);
                }
                else
                {
                    while (true)
                    {
                        var buffer = new byte[10240];
                        int readLen = r.Read(buffer, 0, 10240);
                        if (readLen == 0) //到文件尾，结束
                            break;
                        HttpContext.Current.Response.BinaryWrite(buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.Trim());
            }
            finally
            {
                r.Close(); //关闭下载文件
                HttpContext.Current.Response.Flush();
            }

            #endregion 流方式发送文件
        }

        /// <summary>
        /// 获取实体类键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Hashtable GetProperties<T>(T t)
        {
            var ht = new Hashtable();
            if (t == null)
            {
                return ht;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return ht;
            }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t, null); //值
                //string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;//描述
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ht[name] = value;
                }
                else
                {
                    GetProperties(value);
                }
            }
            return ht;
        }

        /// <summary>
        /// 获取实体类键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Hashtable GetProperties<T>(List<T> t)
        {
            var ht = new Hashtable();
            if (t == null)
            {
                return ht;
            }
            for(int i=0;i<t.Count;i++)
            {
                PropertyInfo[] properties = t[i].GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    return ht;
                }
                foreach (PropertyInfo item in properties)
                {                    
                    string name = item.Name; //名称
                    object value = item.GetValue(t[i],null); //值
                    //string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;//描述
                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {                       
                        if (ht.ContainsKey(name))
                        {
                            ht[t[i].GetType().FullName + "_" + name] = value;
                        }
                        else
                            ht[name] = value?? "";
                    }
                    else
                    {
                        GetProperties(value);
                    }
                }
            }          
            return ht;
        }

        /// <summary>
        /// 写数据到Word
        /// </summary>
        /// <param name="sourceFile">模板地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="list">Hashtable集合，Hashtable键值对(ht["isCtable"]是否含有数据表table，
        ///                                  ht["tableList"] 数据表集合，
        ///                                  ht["tableIndex"]：数据表在word中的索引,
        ///                                  ht["insertIndex"]：开始插入行的序号,
        ///                                  ht["ContainsHeader"] true:含有，false 没有
        ///                                  ht["ContextStyle"]:内容样式,值为Dictionary（列序号（从0开始），样式集合表,目前只提供 字体、字号
        ///                                                             举例：ht["FontFamily"]="宋体" ht["FontSize"]=16（磅）)
        /// 注意：ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 都是List
        ///       ht["tableList"]、ht["insertIndex"]、ht["ContainsHeader"]、ht["ContextStyle"] 在List中的顺序由ht["tableIndex"]来决定
        /// ht["ContextStyle"] 数据结构：表序号,《列序号，样式HashTable》
        /// </param>
        public static void CreateDataToWordByHashtable(string sourceFile, string fileName, List<Hashtable> list)
        {
            if(list == null)
            {
                throw new Exception("获取打印数据失败！");
            }

            string targetFileName = HttpContext.Current.Server.MapPath("~/Upload/SignUpTable/" + Guid.NewGuid() + ".docx");
            // DocX.Load(sourceFile)
            using (DocX dx = DocX.Load(sourceFile))
            {
                int row = 0;
                foreach (Hashtable ht in list)
                {
                    if (ht == null || ht.Keys.Count == 0)
                    {
                        throw new Exception("获取打印数据失败！");
                    }

                    if (row == 0)
                    {
                        foreach (string propertyName in ht.Keys)
                        {
                            dx.ReplaceText("{$" + propertyName + "$}",
                                ht[propertyName] == null ? "" : ht[propertyName].ToString());
                        }
                        //标签
                        foreach (Bookmark bookmark in dx.Bookmarks)
                        {
                            if (ht.ContainsKey(bookmark.Name))
                            {
                                dx.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                                    ? ""
                                    : ht[bookmark.Name].ToString());
                            }
                        }

                        //数据表
                        #region 数据表

                        if (ht.ContainsKey("isCtable"))
                        {
                            if ((bool)ht["isCtable"])
                            {
                                //word中表格个数
                                var tableCount = dx.Tables.Count;
                                var indexlist = ht["tableIndex"] as List<int>;

                                if (indexlist != null && tableCount >= indexlist.Count)
                                {
                                    for (int i = 0; i < indexlist.Count; i++)
                                    {
                                        var index = indexlist[i];
                                        var wordTable = dx.Tables[index];

                                        var listTable = ht["tableList"] as List<DataTable>;
                                        if (listTable != null)
                                        {
                                            var datadt = listTable[i];
                                            var chlist = ht["ContainsHeader"] as List<bool>;
                                            var insertindexList = ht["insertIndex"] as List<int>;
                                            var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                            //默认从第二行插入
                                            int insertNum = 2;
                                            if (insertindexList != null)
                                            {
                                                insertNum = insertindexList[i];
                                            }

                                            //遍历数据行
                                            for (int j = 0; j < datadt.Rows.Count; j++)
                                            {
                                                var rowInsertIndex = j + insertNum;
                                                Row r = wordTable.InsertRow(rowInsertIndex);
                                                for (int k = 0; k < r.Cells.Count; k++)
                                                {
                                                    r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                                    r.Cells[k].MarginBottom = 0;
                                                    r.Cells[k].MarginLeft = 0;
                                                    r.Cells[k].MarginRight = 0;
                                                    r.Cells[k].MarginTop = 0;

                                                    //设置样式
                                                    if (cStyle != null)
                                                    {
                                                        var c = cStyle[index];
                                                        if (c != null)
                                                        {
                                                            Hashtable sytyleHt = c[k];
                                                            if (sytyleHt.Contains("FontFamily"))
                                                            {
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("FontSize"))
                                                            {
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("Alignment"))
                                                            {
                                                                var t = sytyleHt["Alignment"].ToString();

                                                                switch (t)
                                                                {
                                                                    case "both":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.both;
                                                                        }
                                                                        break;
                                                                    case "center":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.center;
                                                                        }
                                                                        break;
                                                                    case "left":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.left;
                                                                        }
                                                                        break;
                                                                    case "right":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.right;
                                                                        }
                                                                        break;
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("TextDirection"))
                                                            {
                                                                var t = sytyleHt["TextDirection"].ToString();
                                                                switch (t)
                                                                {
                                                                    case "btLr":
                                                                        r.Cells[k].TextDirection = TextDirection.btLr;
                                                                        break;
                                                                    case "right":
                                                                        r.Cells[k].TextDirection = TextDirection.right;
                                                                        break;
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("VerticalAlignment"))
                                                            {
                                                                var v = sytyleHt["VerticalAlignment"].ToString();

                                                                switch (v)
                                                                {
                                                                    case "Center":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                        break;
                                                                    case "Bottom":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                        break;
                                                                    case "Top":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                        break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            wordTable.AutoFit = AutoFit.Contents;

                                            wordTable.Alignment = Alignment.center;

                                            if (chlist != null)
                                            {
                                                //不含有表头
                                                if (!chlist[i])
                                                {
                                                    if (insertNum.Equals(0))
                                                    {
                                                        //移除表格最后一项
                                                        wordTable.RemoveRow(wordTable.RowCount - 1);
                                                    }
                                                    else
                                                    {
                                                        //移除表格第一项
                                                        wordTable.RemoveRow(0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion 数据表
                        row++;
                        
                        if (row!=list.Count)
                        {
                            dx.InsertSectionPageBreak(true);
                        }
                    }
                    else
                    {
                        row++;
                        using (DocX document = DocX.Load(sourceFile))
                        {
                            foreach (string propertyName in ht.Keys)
                            {
                                document.ReplaceText("{$" + propertyName + "$}",
                                    ht[propertyName] == null ? "" : ht[propertyName].ToString());
                            }
                            //标签
                            foreach (Bookmark bookmark in document.Bookmarks)
                            {
                                if (ht.ContainsKey(bookmark.Name))
                                {
                                    document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                                        ? ""
                                        : ht[bookmark.Name].ToString());
                                }
                            }

                            //数据表
                            #region 数据表

                            if (ht.ContainsKey("isCtable"))
                            {
                                if ((bool)ht["isCtable"])
                                {
                                    //word中表格个数
                                    var tableCount = document.Tables.Count;
                                    var indexlist = ht["tableIndex"] as List<int>;

                                    if (indexlist != null && tableCount >= indexlist.Count)
                                    {
                                        for (int i = 0; i < indexlist.Count; i++)
                                        {
                                            var index = indexlist[i];
                                            var wordTable = document.Tables[index];

                                            var listTable = ht["tableList"] as List<DataTable>;
                                            if (listTable != null)
                                            {
                                                var datadt = listTable[i];
                                                var chlist = ht["ContainsHeader"] as List<bool>;
                                                var insertindexList = ht["insertIndex"] as List<int>;
                                                var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                                //默认从第二行插入
                                                int insertNum = 2;
                                                if (insertindexList != null)
                                                {
                                                    insertNum = insertindexList[i];
                                                }

                                                //遍历数据行
                                                for (int j = 0; j < datadt.Rows.Count; j++)
                                                {
                                                    var rowInsertIndex = j + insertNum;
                                                    Row r = wordTable.InsertRow(rowInsertIndex);
                                                    for (int k = 0; k < r.Cells.Count; k++)
                                                    {
                                                        r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                                        r.Cells[k].MarginBottom = 0;
                                                        r.Cells[k].MarginLeft = 0;
                                                        r.Cells[k].MarginRight = 0;
                                                        r.Cells[k].MarginTop = 0;

                                                        //设置样式
                                                        if (cStyle != null)
                                                        {
                                                            var c = cStyle[index];
                                                            if (c != null)
                                                            {
                                                                Hashtable sytyleHt = c[k];
                                                                if (sytyleHt.Contains("FontFamily"))
                                                                {
                                                                    foreach (var p in r.Cells[k].Paragraphs)
                                                                    {
                                                                        p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("FontSize"))
                                                                {
                                                                    foreach (var p in r.Cells[k].Paragraphs)
                                                                    {
                                                                        p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("Alignment"))
                                                                {
                                                                    var t = sytyleHt["Alignment"].ToString();

                                                                    switch (t)
                                                                    {
                                                                        case "both":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.both;
                                                                            }
                                                                            break;
                                                                        case "center":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.center;
                                                                            }
                                                                            break;
                                                                        case "left":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.left;
                                                                            }
                                                                            break;
                                                                        case "right":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.right;
                                                                            }
                                                                            break;
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("TextDirection"))
                                                                {
                                                                    var t = sytyleHt["TextDirection"].ToString();
                                                                    switch (t)
                                                                    {
                                                                        case "btLr":
                                                                            r.Cells[k].TextDirection = TextDirection.btLr;
                                                                            break;
                                                                        case "right":
                                                                            r.Cells[k].TextDirection = TextDirection.right;
                                                                            break;
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("VerticalAlignment"))
                                                                {
                                                                    var v = sytyleHt["VerticalAlignment"].ToString();

                                                                    switch (v)
                                                                    {
                                                                        case "Center":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                            break;
                                                                        case "Bottom":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                            break;
                                                                        case "Top":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                            break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                wordTable.AutoFit = AutoFit.Contents;

                                                wordTable.Alignment = Alignment.center;

                                                if (chlist != null)
                                                {
                                                    //不含有表头
                                                    if (!chlist[i])
                                                    {
                                                        if (insertNum.Equals(0))
                                                        {
                                                            //移除表格最后一项
                                                            wordTable.RemoveRow(wordTable.RowCount - 1);
                                                        }
                                                        else
                                                        {
                                                            //移除表格第一项
                                                            wordTable.RemoveRow(0);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion 数据表

                          
                            dx.InsertDocument(document);
                            dx.InsertSectionPageBreak(true);
                  
                        }
                    }
                }
                dx.SaveAs(targetFileName);
            }

            //数据流发往客户端
            SendFile(targetFileName, fileName);

            //删除临时文件
            File.Delete(targetFileName);
        }

        /// <summary>
        /// 根据集合（多页数据）生成word
        /// </summary>
        /// <param name="sourceFile">模板word源</param>
        /// <param name="saveFileName">保存word目标（保存在服务器上）</param>
        /// <param name="list">数据集合</param>
        public static void CreateWordByHashtable(string sourceFile, string saveFileName, List<Hashtable> list, HttpServerUtility s)
        {
            if (list == null)
            {
                throw new Exception("获取打印数据失败！");
            }

            string targetFileName = s.MapPath(saveFileName);
            // DocX.Load(sourceFile)
            using (DocX dx = DocX.Load(sourceFile))
            {
                int row = 0;
                foreach (Hashtable ht in list)
                {
                    if (ht == null || ht.Keys.Count == 0)
                    {
                        throw new Exception("获取打印数据失败！");
                    }

                    if (row == 0)
                    {
                        foreach (string propertyName in ht.Keys)
                        {
                            dx.ReplaceText("{$" + propertyName + "$}",
                                ht[propertyName] == null ? "" : ht[propertyName].ToString());
                        }
                        //标签
                        foreach (Bookmark bookmark in dx.Bookmarks)
                        {
                            if (bookmark.Name == "photo" && ht["photo"].ToString() != "")
                            {
                                Novacode.Image pImag = dx.AddImage(s.MapPath(ht["photo"].ToString()));
                                Picture pPicture = pImag.CreatePicture();
                                pPicture.Height = 112;
                                pPicture.Width = 88;

                                bookmark.Paragraph.Alignment = Alignment.center;
                                bookmark.Paragraph.AppendPicture(pPicture);
                                                    }
                            else if (bookmark.Name.Contains("Img_FacePhoto_") && ht[bookmark.Name].ToString() != "")
                            {
                                Novacode.Image pImag = dx.AddImage(s.MapPath(ht[bookmark.Name].ToString()));
                                Picture pPicture = pImag.CreatePicture();
                                pPicture.Height = 112;
                                pPicture.Width = 88;
                                bookmark.Paragraph.Alignment = Alignment.center;
                                bookmark.Paragraph.AppendPicture(pPicture);            
                            }
                            else if (ht.ContainsKey(bookmark.Name))
                            {
                                dx.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                                    ? ""
                                    : ht[bookmark.Name].ToString());
                            }
                        }

                        //数据表
                        #region 数据表

                        if (ht.ContainsKey("isCtable"))
                        {
                            if ((bool)ht["isCtable"])
                            {
                                //word中表格个数
                                var tableCount = dx.Tables.Count;
                                var indexlist = ht["tableIndex"] as List<int>;

                                if (indexlist != null && tableCount >= indexlist.Count)
                                {
                                    for (int i = 0; i < indexlist.Count; i++)
                                    {
                                        var index = indexlist[i];
                                        var wordTable = dx.Tables[index];

                                        var listTable = ht["tableList"] as List<DataTable>;
                                        if (listTable != null)
                                        {
                                            var datadt = listTable[i];
                                            var chlist = ht["ContainsHeader"] as List<bool>;
                                            var insertindexList = ht["insertIndex"] as List<int>;
                                            var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                            //默认从第二行插入
                                            int insertNum = 2;
                                            if (insertindexList != null)
                                            {
                                                insertNum = insertindexList[i];
                                            }

                                            //遍历数据行
                                            for (int j = 0; j < datadt.Rows.Count; j++)
                                            {
                                                var rowInsertIndex = j + insertNum;
                                                Row r = wordTable.InsertRow(rowInsertIndex);
                                                for (int k = 0; k < r.Cells.Count; k++)
                                                {
                                                    r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                                    r.Cells[k].MarginBottom = 0;
                                                    r.Cells[k].MarginLeft = 0;
                                                    r.Cells[k].MarginRight = 0;
                                                    r.Cells[k].MarginTop = 0;

                                                    //设置样式
                                                    if (cStyle != null)
                                                    {
                                                        var c = cStyle[index];
                                                        if (c != null)
                                                        {
                                                            Hashtable sytyleHt = c[k];
                                                            if (sytyleHt.Contains("FontFamily"))
                                                            {
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("FontSize"))
                                                            {
                                                                foreach (var p in r.Cells[k].Paragraphs)
                                                                {
                                                                    p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("Alignment"))
                                                            {
                                                                var t = sytyleHt["Alignment"].ToString();

                                                                switch (t)
                                                                {
                                                                    case "both":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.both;
                                                                        }
                                                                        break;
                                                                    case "center":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.center;
                                                                        }
                                                                        break;
                                                                    case "left":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.left;
                                                                        }
                                                                        break;
                                                                    case "right":
                                                                        foreach (var p in r.Cells[k].Paragraphs)
                                                                        {
                                                                            p.Alignment = Alignment.right;
                                                                        }
                                                                        break;
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("TextDirection"))
                                                            {
                                                                var t = sytyleHt["TextDirection"].ToString();
                                                                switch (t)
                                                                {
                                                                    case "btLr":
                                                                        r.Cells[k].TextDirection = TextDirection.btLr;
                                                                        break;
                                                                    case "right":
                                                                        r.Cells[k].TextDirection = TextDirection.right;
                                                                        break;
                                                                }
                                                            }

                                                            if (sytyleHt.Contains("VerticalAlignment"))
                                                            {
                                                                var v = sytyleHt["VerticalAlignment"].ToString();

                                                                switch (v)
                                                                {
                                                                    case "Center":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                        break;
                                                                    case "Bottom":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                        break;
                                                                    case "Top":
                                                                        r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                        break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            wordTable.AutoFit = AutoFit.Contents;

                                            wordTable.Alignment = Alignment.center;

                                            if (chlist != null)
                                            {
                                                //不含有表头
                                                if (!chlist[i])
                                                {
                                                    if (insertNum.Equals(0))
                                                    {
                                                        //移除表格最后一项
                                                        wordTable.RemoveRow(wordTable.RowCount - 1);
                                                    }
                                                    else
                                                    {
                                                        //移除表格第一项
                                                        wordTable.RemoveRow(0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion 数据表
                        row++;

                        if (row != list.Count)
                        {
                            dx.InsertSectionPageBreak(true);
                        }
                    }
                    else
                    {
                        row++;
                        using (DocX document = DocX.Load(sourceFile))
                        {
                            foreach (string propertyName in ht.Keys)
                            {
                                document.ReplaceText("{$" + propertyName + "$}",
                                    ht[propertyName] == null ? "" : ht[propertyName].ToString());
                            }
                            //标签
                            foreach (Bookmark bookmark in document.Bookmarks)
                            {
                                
                                if (bookmark.Name == "photo" && ht["photo"].ToString() != "")
                                {
                                    Novacode.Image pImag = document.AddImage(s.MapPath(ht["photo"].ToString()));
                                    Picture pPicture = pImag.CreatePicture();
                                    pPicture.Height = 112;
                                    pPicture.Width = 88;
                                    bookmark.Paragraph.Alignment = Alignment.center;
                                    bookmark.Paragraph.AppendPicture(pPicture);
                                                       }
                                else if (bookmark.Name.Contains("Img_FacePhoto_") && ht[bookmark.Name].ToString() != "")
                                {
                                    Novacode.Image pImag = document.AddImage(s.MapPath(ht[bookmark.Name].ToString()));
                                    Picture pPicture = pImag.CreatePicture();
                                    pPicture.Height = 112;
                                    pPicture.Width = 88;
                                  
                                    bookmark.Paragraph.Alignment = Alignment.center;
                                    bookmark.Paragraph.AppendPicture(pPicture);
                                   
                                }
                                else if (ht.ContainsKey(bookmark.Name))
                                {
                                    document.Bookmarks[bookmark.Name].SetText(ht[bookmark.Name] == null
                                        ? ""
                                        : ht[bookmark.Name].ToString());
                                }
                            }

                            //数据表
                            #region 数据表

                            if (ht.ContainsKey("isCtable"))
                            {
                                if ((bool)ht["isCtable"])
                                {
                                    //word中表格个数
                                    var tableCount = document.Tables.Count;
                                    var indexlist = ht["tableIndex"] as List<int>;

                                    if (indexlist != null && tableCount >= indexlist.Count)
                                    {
                                        for (int i = 0; i < indexlist.Count; i++)
                                        {
                                            var index = indexlist[i];
                                            var wordTable = document.Tables[index];

                                            var listTable = ht["tableList"] as List<DataTable>;
                                            if (listTable != null)
                                            {
                                                var datadt = listTable[i];
                                                var chlist = ht["ContainsHeader"] as List<bool>;
                                                var insertindexList = ht["insertIndex"] as List<int>;
                                                var cStyle = ht["ContextStyle"] as Dictionary<int, Dictionary<int, Hashtable>>;

                                                //默认从第二行插入
                                                int insertNum = 2;
                                                if (insertindexList != null)
                                                {
                                                    insertNum = insertindexList[i];
                                                }

                                                //遍历数据行
                                                for (int j = 0; j < datadt.Rows.Count; j++)
                                                {
                                                    var rowInsertIndex = j + insertNum;
                                                    Row r = wordTable.InsertRow(rowInsertIndex);
                                                    for (int k = 0; k < r.Cells.Count; k++)
                                                    {
                                                        r.Cells[k].Paragraphs.First().Append(datadt.Rows[j][k].ToString());
                                                        r.Cells[k].MarginBottom = 0;
                                                        r.Cells[k].MarginLeft = 0;
                                                        r.Cells[k].MarginRight = 0;
                                                        r.Cells[k].MarginTop = 0;

                                                        //设置样式
                                                        if (cStyle != null)
                                                        {
                                                            var c = cStyle[index];
                                                            if (c != null)
                                                            {
                                                                Hashtable sytyleHt = c[k];
                                                                if (sytyleHt.Contains("FontFamily"))
                                                                {
                                                                    foreach (var p in r.Cells[k].Paragraphs)
                                                                    {
                                                                        p.Font(new FontFamily(sytyleHt["FontFamily"].ToString()));
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("FontSize"))
                                                                {
                                                                    foreach (var p in r.Cells[k].Paragraphs)
                                                                    {
                                                                        p.FontSize(Convert.ToDouble(sytyleHt["FontSize"]));
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("Alignment"))
                                                                {
                                                                    var t = sytyleHt["Alignment"].ToString();

                                                                    switch (t)
                                                                    {
                                                                        case "both":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.both;
                                                                            }
                                                                            break;
                                                                        case "center":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.center;
                                                                            }
                                                                            break;
                                                                        case "left":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.left;
                                                                            }
                                                                            break;
                                                                        case "right":
                                                                            foreach (var p in r.Cells[k].Paragraphs)
                                                                            {
                                                                                p.Alignment = Alignment.right;
                                                                            }
                                                                            break;
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("TextDirection"))
                                                                {
                                                                    var t = sytyleHt["TextDirection"].ToString();
                                                                    switch (t)
                                                                    {
                                                                        case "btLr":
                                                                            r.Cells[k].TextDirection = TextDirection.btLr;
                                                                            break;
                                                                        case "right":
                                                                            r.Cells[k].TextDirection = TextDirection.right;
                                                                            break;
                                                                    }
                                                                }

                                                                if (sytyleHt.Contains("VerticalAlignment"))
                                                                {
                                                                    var v = sytyleHt["VerticalAlignment"].ToString();

                                                                    switch (v)
                                                                    {
                                                                        case "Center":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Center;
                                                                            break;
                                                                        case "Bottom":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Bottom;
                                                                            break;
                                                                        case "Top":
                                                                            r.Cells[k].VerticalAlignment = VerticalAlignment.Top;
                                                                            break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                wordTable.AutoFit = AutoFit.Contents;

                                                wordTable.Alignment = Alignment.center;

                                                if (chlist != null)
                                                {
                                                    //不含有表头
                                                    if (!chlist[i])
                                                    {
                                                        if (insertNum.Equals(0))
                                                        {
                                                            //移除表格最后一项
                                                            wordTable.RemoveRow(wordTable.RowCount - 1);
                                                        }
                                                        else
                                                        {
                                                            //移除表格第一项
                                                            wordTable.RemoveRow(0);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion 数据表


                            dx.InsertDocument(document);
                            if (row < list.Count)
                            {
                                dx.InsertSectionPageBreak(true);
                            }

                        }
                    }
                }
                dx.SaveAs(targetFileName);
            }

        }
    }
}