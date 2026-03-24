using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using DataAccess;
using Model;
using System.IO;

namespace ZYRYJG.CertifManage
{
    public partial class gbCertView : System.Web.UI.Page
    {
        //国标电子证书预览
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] keys = Utility.Cryptography.Decrypt(Request["o"].Replace(" ", "+")).Split(',');
                string CertificateCAID = keys[0];//CertificateCAID
                string CERTIFICATEID = keys[1];//CERTIFICATEID

                //CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(CERTIFICATEID));

                //生产环境电子证书存放地址
                string pdfUrl = string.Format("{2}/{0}/{1}.pdf", CertificateCAID.Substring(CertificateCAID.Length - 3, 3), CertificateCAID, MyWebConfig.CAFile);

                ////测试环境电子证书存放地址
                //string pdfUrl = string.Format("{2}/test/{1}.pdf", CertificateCAID.Substring(CertificateCAID.Length - 3, 3), CertificateCAID, MyWebConfig.CAFile);

                if (File.Exists(pdfUrl) == true)
                {
                    string jpgPath = string.Format("~/Upload/PrintCertificate/{0}.jpg", CertificateCAID);
                    Utility.ImageHelp.PdfToPic(pdfUrl, Server.MapPath(jpgPath), 2);//pdftojpg
                    Utility.ImageHelp.AddWaterMark(Server.MapPath(jpgPath), "20,700", 36, "仅供全国工程质量安全监管信息平台信息公开使用", -40, "仿宋");
                    System.Web.UI.HtmlControls.HtmlImage img = new System.Web.UI.HtmlControls.HtmlImage();
                    img.Src = UIHelp.AddUrlReadParam(jpgPath);
                    img.Align = "center";
                    img.Style.Add("height", "1000px");
                    div_view.Controls.Add(img);
                }
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("读取预览电子证书失败", ex);
                Response.Write("读取数据失败！");
            }
        }

        ////国标电子证书预览
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string[] keys = Utility.Cryptography.Decrypt(Request["o"].Replace(" ", "+")).Split(',');
        //        string CertificateCAID = keys[0];//CertificateCAID
        //        string CERTIFICATEID = keys[1];//CERTIFICATEID

        //        CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(CERTIFICATEID));

        //        createImg(ob);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.FileLog.WriteLog("读取预览电子证书失败", ex);
        //        Response.Write("读取数据失败！");
        //    }
        //}



        ///// <summary>
        ///// 创建电子证书预览图片
        ///// </summary>
        ///// <param name="ob"></param>
        //protected void createImg(CertificateOB ob)
        //{
        //    //保存预览图片地址
        //    string localPath = string.Format("~/UpLoad/PrintCertificate/{0}.jpg", ob.CertificateCAID);

        //    ////证书图片路径
        //    //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCert/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCert/"));


        //    string personPhoto = Server.MapPath(UIHelp.GetFaceImagePath(ob.FacePhoto, ob.WorkerCertificateCode));//一寸照片
        //    //**********可以修改成首次打印用考试报名照片，其它业务用人员照片
        //    if (File.Exists(personPhoto) == false)
        //    {
        //        personPhoto = Server.MapPath("~/Img/face.jpg");
        //    }
        //    System.Drawing.Image imgWarter = System.Drawing.Image.FromFile(personPhoto);//一寸照片
        //    string path = "";//证书照片背景图路径
        //    System.Drawing.Image imgSrc = null;//证书照片背景图
        //    Bitmap bmpDest = null;

        //    //证书背景模板
        //    switch (ob.PostTypeID)
        //    {
        //        case 1:
        //            path = Server.MapPath(@"~/images/anquan1.jpg");
        //            break;
        //        case 2:
        //            path = Server.MapPath(@"~/images/tezhong.jpg");
        //            break;
        //        default:
        //            break;
        //    }

        //    imgSrc = System.Drawing.Image.FromFile(path);

        //    //1654 x 2339
        //    Font f23 = new Font("黑体", 23, FontStyle.Bold);

        //    using (Graphics g = Graphics.FromImage(imgSrc))
        //    {

        //        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //        StringFormat format = new StringFormat();
        //        format.Alignment = StringAlignment.Center;

        //        //输出证书信息
        //        using (Font f = new Font("宋体", 14, FontStyle.Regular))
        //        {
        //            using (Brush b = new SolidBrush(Color.Red))
        //            {
        //                if (ob.PostTypeID == 1)
        //                {
        //                    g.DrawString("建筑施工企业主要负责人", f23, b, new Rectangle(0, 518, 2481, 90), format);//工种
        //                    g.DrawString(ob.CertificateCode, f, b, new Rectangle(1000, 857, 1050, 74));//证书编号
        //                    g.DrawString(ob.WorkerName, f, b, new Rectangle(860, 1190, 1050, 54));//姓名
        //                    g.DrawString(ob.Sex, f, b, new Rectangle(860, 1350, 1050, 54));//性别
        //                    g.DrawString(ob.Birthday.Value.ToString("yyyy年M月d日"), f, b, new Rectangle(860, 1475, 1050, 54));//出生日期

        //                    //企业名称
        //                    string 待写入字符串 = ob.UnitName;
        //                    char[] ch = 待写入字符串.ToCharArray();
        //                    PointF pf = new Point(860, 1610);
        //                    SizeF charSize;
        //                    foreach (char c in ch)
        //                    {
        //                        //获取字符尺寸
        //                        charSize = g.MeasureString(c.ToString(), f);

        //                        //逐一写入字符
        //                        g.DrawString(c.ToString(), f, b, pf);

        //                        //设置字间距
        //                        pf.X += 60;// (charSize.Width - 10);

        //                        //设置行高
        //                        if (pf.X > 1800)
        //                        {
        //                            pf.X = 232;
        //                            pf.Y += (charSize.Height + 30);
        //                        }
        //                    }

        //                    //职务

        //                    g.DrawString(ob.ConferDate.Value.ToString("yyyy年M月d日"), f, b, new Rectangle(860, 1890, 1050, 54));//初次发证日期
        //                    g.DrawString(ob.CheckDate.Value.ToString("yyyy年M月d日"), f, b, new Rectangle(860, 2030, 1050, 54));//有效期起始
        //                    g.DrawString(ob.ValidEndDate.Value.ToString("yyyy年M月d日"), f, b, new Rectangle(1490, 2030, 1050, 54));//有效期截至
        //                    g.DrawString("北京市住房和城乡建设委员会", f, b, new Rectangle(1380, 2720, 1050, 54));//发证机关
        //                    g.DrawString(ob.CheckDate.Value.ToString("yyyy　 M　　d"), f, b, new Rectangle(1380, 2845, 1050, 54));//发证日期



        //                    //输出资格专用章

        //                    using (System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/images/chapter01.png")))
        //                    {
        //                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                        g.DrawImage(imgChapter1, new Rectangle(1520, 2520, 400, 400));
        //                    }


        //                    //输出一寸照片
        //                    g.DrawImage(imgWarter, new Rectangle(1760, 1175, 281, 393));

        //                    ////输出二维码  
        //                    //using (System.Drawing.Image QRCode = System.Drawing.Image.FromFile(Server.MapPath(string.Format("~/UpLoad/PrintCert/{0}.png", LabelCertificateCode.Text))))
        //                    //{
        //                    //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    //    g.DrawImage(QRCode, new Rectangle(1094, 1386, 268, 268));
        //                    //}
        //                }
        //                else//特种作业
        //                {

        //                    //g.DrawString("北京市建筑施工特种作业人员", f22, b, new Rectangle(422, 468, 1250, 54));
        //                    //g.DrawString("网络继续教育证明", f22, b, new Rectangle(562, 578, 1050, 74));
        //                    //g.DrawString(string.Format("编号：{0}", LabelReEducateCertificateCode.Text), f32, b, new Rectangle(232, 1106, 1050, 54));

        //                    //string 待写入字符串 = string.Format("{0} 参加了北京市建筑施工特种作业人员网络继续教育平台“{1}”学习,学满{2}课时，特此证明。"
        //                    //    , LabelWorkerName.Text
        //                    //    , LabelPackageTitle.Text
        //                    //    , LabelPeriod.Text);
        //                    //char[] ch = 待写入字符串.ToCharArray();
        //                    //PointF pf = new Point(332, 1274);
        //                    //SizeF charSize;
        //                    //foreach (char c in ch)
        //                    //{
        //                    //    //获取字符尺寸
        //                    //    charSize = g.MeasureString(c.ToString(), f32);

        //                    //    //逐一写入字符
        //                    //    g.DrawString(c.ToString(), f32, b, pf);

        //                    //    //设置字间距
        //                    //    pf.X += (charSize.Width - 10);

        //                    //    //设置行高
        //                    //    if (pf.X > 1382)
        //                    //    {
        //                    //        pf.X = 232;
        //                    //        pf.Y += (charSize.Height + 30);
        //                    //    }
        //                    //}

        //                    //g.DrawString(string.Format("证书编号：{0}", LabelCertificateCode.Text), f32, b, new Rectangle(232, 1610, 1050, 54));
        //                    //g.DrawString(string.Format("岗位名称：{0}", LabelPostName.Text), f32, b, new Rectangle(232, 1700, 1050, 54));
        //                    //g.DrawString(string.Format("证件号码：{0}", LabelWorkerCertificateCode.Text), f32, b, new Rectangle(232, 1790, 1050, 54));
        //                    //g.DrawString(string.Format("签发日期：{0}", LabelReEducateConferDate.Text), f32, b, new Rectangle(232, 1880, 1050, 54));
        //                    //g.DrawString("签发单位：北京市建设教育协会", f32, b, new Rectangle(232, 1970, 1050, 54));

        //                    ////输出资格专用章
        //                    //using (System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/img/gzs.png")))
        //                    //{
        //                    //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    //    g.DrawImage(imgChapter1, new Rectangle(602, 1836, 300, 300));
        //                    //}

        //                    ////输出一寸照片
        //                    //g.DrawImage(imgWarter, new Rectangle(1094, 794, 281, 393));

        //                    ////输出二维码  
        //                    //using (System.Drawing.Image QRCode = System.Drawing.Image.FromFile(Server.MapPath(string.Format("~/UpLoad/PrintCert/{0}.png", LabelCertificateCode.Text))))
        //                    //{
        //                    //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    //    g.DrawImage(QRCode, new Rectangle(1094, 1686, 268, 268));
        //                    //}
        //                }

        //            }
        //        }
        //    }

        //    using (bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 0.8), Convert.ToInt32(imgSrc.Height * 0.8)))
        //    {
        //        using (Graphics gg = Graphics.FromImage(bmpDest))
        //        {
        //            gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 0.8), (float)(imgSrc.Height * 0.8));
        //        }
        //        bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Jpeg);

        //        //div_view.Style.Add("backgroud", string.Format("url({0}) no-repeat center center",UIHelp.AddUrlReadParam( localPath.Replace("~",".."))));
        //        System.Web.UI.HtmlControls.HtmlImage img = new System.Web.UI.HtmlControls.HtmlImage();

        //        img.Src = UIHelp.AddUrlReadParam(localPath);
        //        img.Style.Add("height", "1000px");
        //        div_view.Controls.Add(img);
        //    }
        //}
    }
}