using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
    public class WordDelHelp
    {
        /// <summary>
        /// 将服务器端word输出到客户端
        /// </summary>
        /// <param name="strFile">文件路径</param>
        public static void ExportWord(System.Web.UI.Page page, string strFile)
        {
            FileStream fs = null;
            try
            {
                page.Response.Clear();
                page.Response.Buffer = true;
                page.Response.ClearHeaders();
                page.Response.Charset = "GB2312";
                page.Response.ContentEncoding = System.Text.Encoding.UTF8;
                page.Response.ContentType = "application/octet-stream";
                FileInfo fi = new FileInfo(strFile);
                string fname = fi.Name;
                if (page.Request.Browser.Browser.Trim().ToUpper() == "IE") fname = System.Web.HttpUtility.UrlEncode(fname, System.Text.Encoding.UTF8);
                page.Response.AddHeader("Content-Disposition", "attachment;filename=" + fname);
                page.Response.AddHeader("Content-Length", fi.Length.ToString());
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
        
        #region 生成xml格式word

        /// <summary>
        /// 根据母板创建word文档(单页)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dotPath">模版路径</param>
        /// <param name="savePath">保存临时文件路径</param>
        /// <param name="replaceSet">替换书签值集合（图片字段加前缀“Img_”）</param>
        public static void CreateXMLWordWithDot(System.Web.UI.Page page, string dotPath, string savePath, System.Collections.Generic.Dictionary<string, string> replaceSet)
        {
            //模板文件拷贝到新文件
            File.Copy(page.Server.MapPath(dotPath), page.Server.MapPath(savePath), true);

            string templete = File.ReadAllText(page.Server.MapPath(dotPath), System.Text.Encoding.UTF8);//Encoding.GetEncoding("GB2312"));
            StringBuilder pageContent = new StringBuilder(templete);//单页内容
            foreach (string bookMark in replaceSet.Keys)
            {
                if (bookMark.IndexOf("Img_") != -1)//照片
                {
                    FileInfo fi = new FileInfo(page.Server.MapPath(replaceSet[bookMark]));
                    using (FileStream fs = fi.OpenRead())
                    {
                        byte[] bytes = new byte[fi.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        string img64 = Convert.ToBase64String(bytes);
                        pageContent.Replace(string.Format("[{0}]", bookMark), img64);
                        fs.Close();
                    }
                }
                else//书签
                {
                    pageContent.Replace(string.Format("[{0}]", bookMark), replaceSet[bookMark]);
                }
            }
            using (StreamWriter sw = new StreamWriter(page.Server.MapPath(savePath), false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(pageContent.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 根据母板创建word文档(多页)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dotPath">模版路径</param>
        /// <param name="saveTempPath">保存单页临时文件路径</param>
        /// <param name="savePath">保存多页Word文件路径</param>
        /// <param name="replaceSet">替换书签值集合（图片字段加前缀“Img_”）</param>
        public static void CreateXMLWordWithDot(System.Web.UI.Page page, string dotPath, string savePath, List<System.Collections.Generic.Dictionary<string, string>> replaceSet)
        {
            //模板文件拷贝到新文件
            File.Copy(page.Server.MapPath(dotPath), page.Server.MapPath(savePath), true);

            using (StreamWriter sw = new StreamWriter(page.Server.MapPath(savePath), false, System.Text.Encoding.UTF8))
            {
                //<w:body>第一页<w:br w:type="page"/>第二页</w:body>
                string templete = File.ReadAllText(page.Server.MapPath(dotPath), System.Text.Encoding.UTF8);//Encoding.GetEncoding("GB2312"));
                string head = templete.Substring(0, templete.IndexOf("<w:body>") + 8);
                string foot = templete.Substring(templete.IndexOf("</w:body>"));
                templete = templete.Substring(head.Length, templete.Length - foot.Length - head.Length);

                StringBuilder pageContent = new StringBuilder(templete.Length);//单页内容
                //StringBuilder allPageContent = new StringBuilder(templete.Length * replaceSet.Count);//多页内容

                //生成word文档
                sw.WriteLine(head); //添加头
                foreach (System.Collections.Generic.Dictionary<string, string> bookData in replaceSet)
                {
                    if (pageContent.Length != 0) sw.WriteLine("<w:br w:type=\"page\"/>");//分页

                    //重新加载母板
                    pageContent.Remove(0, pageContent.Length);
                    pageContent.Append(templete);

                    //替换书签
                    foreach (string bookMark in bookData.Keys)
                    {
                        if (bookMark.IndexOf("Img_") != -1)//照片书签
                        {
                            FileInfo fi = new FileInfo(page.Server.MapPath(bookData[bookMark]));
                            using (FileStream fs = fi.OpenRead())
                            {
                                byte[] bytes = new byte[fi.Length];
                                fs.Read(bytes, 0, bytes.Length);
                                string img64 = Convert.ToBase64String(bytes);
                                pageContent.Replace(string.Format("[{0}]", bookMark), img64);
                                fs.Close();
                            }
                        }
                        else//文字书签
                        {
                            pageContent.Replace(string.Format("[{0}]", bookMark), bookData[bookMark]);
                        }
                    }
                    sw.WriteLine(pageContent.ToString());
                }

                sw.WriteLine(foot);//添加尾
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 根据母板创建word文档(两页母板)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dotPath1">首页模版路径</param>
        /// <param name="dotPath2">非首页模版路径</param>
        /// <param name="saveTempPath">保存单页临时文件路径</param>
        /// <param name="savePath">保存多页Word文件路径</param>
        /// <param name="replaceSet">替换书签值集合（图片字段加前缀“Img_”）</param>
        public static void CreateXMLWordWithDot(System.Web.UI.Page page, string dotPath1, string dotPath2, string savePath, List<System.Collections.Generic.Dictionary<string, string>> replaceSet)
        {
            //模板文件拷贝到新文件
            File.Copy(page.Server.MapPath(dotPath1), page.Server.MapPath(savePath), true);

            using (StreamWriter sw = new StreamWriter(page.Server.MapPath(savePath), false, System.Text.Encoding.UTF8))
            {
                //<w:body>第一页<w:br w:type="page"/>第二页</w:body>
                string templete = File.ReadAllText(page.Server.MapPath(dotPath1), System.Text.Encoding.UTF8);//Encoding.GetEncoding("GB2312"));
                string head = templete.Substring(0, templete.IndexOf("<w:body>") + 8);
                string foot = templete.Substring(templete.IndexOf("</w:body>"));
                templete = templete.Substring(head.Length, templete.Length - foot.Length - head.Length);

                StringBuilder pageContent = new StringBuilder(templete.Length);//单页内容

                int i = 1;

                #region 填充首页

                sw.WriteLine(head); //添加头

                //加载母板
                pageContent.Remove(0, pageContent.Length);
                pageContent.Append(templete);
                foreach (string bookMark in replaceSet[0].Keys)
                {
                    if (bookMark.IndexOf("Img_") != -1)//照片
                    {
                        FileInfo fi = new FileInfo(page.Server.MapPath(replaceSet[0][bookMark]));
                        using (FileStream fs = fi.OpenRead())
                        {
                            byte[] bytes = new byte[fi.Length];
                            fs.Read(bytes, 0, bytes.Length);
                            string img64 = Convert.ToBase64String(bytes);
                            pageContent.Replace(string.Format("[{0}]", bookMark), img64);
                            fs.Close();
                        }
                    }
                    else//书签
                    {
                        pageContent.Replace(string.Format("[{0}]", bookMark), replaceSet[0][bookMark]);
                    }
                }
                sw.WriteLine(pageContent.ToString());

                #endregion

                #region 填充其它页
                if (replaceSet.Count > 1)
                {
                    //分析第二页母板
                    templete = File.ReadAllText(page.Server.MapPath(dotPath2), System.Text.Encoding.UTF8);
                    head = templete.Substring(0, templete.IndexOf("<w:body>") + 8);
                    foot = templete.Substring(templete.IndexOf("</w:body>"));
                    templete = templete.Substring(head.Length, templete.Length - foot.Length - head.Length);

                    //生成word文档
                    foreach (System.Collections.Generic.Dictionary<string, string> bookData in replaceSet)
                    {
                        if (i == 1)//跳过第一页
                        {
                            i++;
                            continue;
                        }
                        if (pageContent.Length != 0) sw.WriteLine("<w:br w:type=\"page\"/>");//分页

                        //重新加载母板
                        pageContent.Remove(0, pageContent.Length);
                        pageContent.Append(templete);

                        foreach (string bookMark in bookData.Keys)
                        {
                            if (bookMark.IndexOf("Img_") != -1)//照片
                            {
                                FileInfo fi = new FileInfo(page.Server.MapPath(bookData[bookMark]));
                                using (FileStream fs = fi.OpenRead())
                                {
                                    byte[] bytes = new byte[fi.Length];
                                    fs.Read(bytes, 0, bytes.Length);
                                    string img64 = Convert.ToBase64String(bytes);
                                    pageContent.Replace(string.Format("[{0}]", bookMark), img64);
                                    fs.Close();
                                }
                            }
                            else//书签
                            {
                                pageContent.Replace(string.Format("[{0}]", bookMark), bookData[bookMark]);
                            }
                        }
                        sw.WriteLine(pageContent.ToString());
                    }
                }

                #endregion

                sw.WriteLine(foot);//添加尾

                sw.Flush();
                sw.Close();
            }
        }
        
        /// <summary>
        /// 替换模板空行标签内容
        /// </summary>
        /// <param name="printData">用于替换标签的字典（格式：<标签名称,值>）</param>
        /// <param name="labelList">标签名称（不带行号）列表，用英文逗号分割</param>
        /// <param name="startRowIndex">替换起始行索引</param>
        /// <param name="endRowIndex">替换结束行索引</param>
        public static void ReplaceLabelOfNullRow(Dictionary<string, string> printData, string labelList, int startRowIndex, int endRowIndex)
        {
            string[] labels = labelList.Split(',');//替换标签名称

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = 0; j < labels.Length; j++)
                {
                    printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
                }
            }
        }
        
        #endregion


        /// <summary>
        /// Word转化html
        /// </summary>
        /// <param name="WordFilePath"></param>
        public static void WordToHtmlFile(string WordFilePath)
        {

            Word.Application newApp = new Word.Application();
            // 指定原文件和目标文件
            object Source = WordFilePath;
            string SaveHtmlPath = WordFilePath.Substring(0, WordFilePath.Length - 3) + "htm";
            object Target = SaveHtmlPath;

            // 缺省参数  
            object Unknown = Type.Missing;

            //为了保险,只读方式打开
            object readOnly = true;

            // 打开doc文件
            Word.Document doc = newApp.Documents.Open(ref Source, ref Unknown,
                 ref readOnly, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown);

            // 指定另存为格式(rtf)
            object format = Word.WdSaveFormat.wdFormatFilteredHTML;
            // 转换格式
            doc.SaveAs(ref Target, ref format,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown);

            // 关闭文档和Word程序
            doc.Close(ref Unknown, ref Unknown, ref Unknown);
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);

        }
    }
}