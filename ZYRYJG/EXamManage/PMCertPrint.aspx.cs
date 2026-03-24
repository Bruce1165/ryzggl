using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using Utility;
using System.IO;
using System.Drawing;

namespace ZYRYJG.EXamManage
{
    public partial class PMCertPrint : BasePage
    {
        //protected override string CheckVisiteRgihtUrl
        //{
        //    get
        //    {
        //        return "PMCertPrintManage.aspx";
        //    }
        //}

        ////一次打印证书个数
        //public int ShowCount
        //{
        //    get { return Convert.ToInt32(ViewState["ShowCount"]); }
        //    set { ViewState["ShowCount"] = value; }
        //}

        ///// <summary>
        ///// 待打印列证书ID集合
        ///// </summary>
        //public List<string> printList
        //{
        //    get { return ViewState["printList"] as List<string>; }
        //}

        //public DataTable printTable
        //{
        //    get { return ViewState["printTable"] as DataTable; }
        //}

        ///// <summary>
        ///// 当前显示证书索引，从0开始
        ///// </summary>
        //public int CurrentIndex
        //{
        //    get { return Convert.ToInt32(ViewState["CurrentIndex"]); }
        //    set
        //    {
        //        ViewState["CurrentIndex"] = value;
        //        if (printList.Count < 2)
        //        {
        //            ButtonNext.Visible = false;
        //            ButtonPrev.Visible = false;
        //            LabelPage.Visible = false;
        //            P_PrintTimeSpan.Visible = false;
        //        }
        //        else
        //        {
        //            P_PrintTimeSpan.Visible = true;
        //            ButtonNext.Visible = true;
        //            ButtonPrev.Visible = true;
        //            LabelPage.Visible = true;
        //            LabelPage.Text = string.Format("第 {1}~{2} 条   共 {0} 条", printList.Count.ToString(), Convert.ToString(value + 1), Convert.ToString(value + ShowCount));

        //            if (value == 0)
        //                ButtonPrev.Enabled = false;
        //            else
        //                ButtonPrev.Enabled = true;

        //            if (value == printList.Count - 1)
        //                ButtonNext.Enabled = false;
        //            else
        //                ButtonNext.Enabled = true;
        //        }
        //    }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (Session["printList"] != null)//批量打印
        //        {
        //            ViewState["printList"] = Session["printList"];
        //            Session.Remove("printList");
        //            ViewState["printTable"] = Session["printTable"];
        //            Session.Remove("printTable");

        //            ShowCount = (ViewState["printTable"] as DataTable).Rows[0]["PostTypeID"].ToString() == "2" ? 4 : 1;
        //        }
        //        else//单个打印
        //        {
        //            ViewState["printList"] = new List<string> { Request.QueryString["CertificateID"] };
        //            DataTable dt = CertificateDAL.GetList(0, int.MaxValue - 1, " and CertificateID=" + Request.QueryString["CertificateID"], "CertificateCode");
        //            ViewState["printTable"] = dt;
        //            ShowCount = dt.Rows[0]["PostTypeID"].ToString() == "2" ? 4 : 1; ;

        //        }

        //        ViewState["ChangeType"] = Request["rnt_t"];//变更类型（续期=continue，变更=manage）
        //        ViewState["PostTypeID"] = Request["rtn_o"];//岗位类型ID
        //        //显示前2个证书(特种作业显示4个)
        //        if (printTable != null)
        //        {
        //            BindPrintDetail(0);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 输出文字，自动折行
        ///// </summary>
        ///// <param name="lineSize">每行最大字数</param>
        ///// <param name="linePad">行间距</param>
        ///// <param name="str">输出内容</param>
        ///// <param name="f">字体</param>
        ///// <param name="b">画笔</param>
        ///// <param name="rect">输出区域</param>
        //private void DrawString(Graphics g, Font f, Brush b, Rectangle rect, string str, int lineSize, int linePad)
        //{
        //    int curSize = 0;//当前输出字符位置
        //    int lineCount = 0;//输出行数量

        //    while (curSize < str.Length)
        //    {
        //        if (str.Length - curSize >= lineSize)
        //            g.DrawString(str.Substring(curSize, lineSize), f, b, new Rectangle(rect.X, rect.Y + (lineCount * (15 + linePad)), rect.Width, rect.Height));
        //        else
        //            g.DrawString(str.Substring(curSize, str.Length - curSize), f, b, new Rectangle(rect.X, rect.Y + (lineCount * (15 + linePad)), rect.Width, rect.Height));
        //        lineCount++;
        //        curSize += lineSize;
        //    }
        //}



        ////验证证书图片是否创建，没有创建立即创建
        //private void BindPhoto(DataRow dr)
        //{
        //    try
        //    {
        //        //证书图片路径
        //        string localPath = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //        if (!System.IO.Directory.Exists(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", dr["PostID"].ToString()))))
        //        {
        //            System.IO.Directory.CreateDirectory(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", dr["PostID"].ToString())));
        //        }

        //        //string personPhoto = Server.MapPath(GetFacePhotoPath(dr["ExamPlanID"].ToString(), dr["WorkerCertificateCode"].ToString()));
        //        string personPhoto = Server.MapPath(UIHelp.GetFaceImagePath(dr["FACEPHOTO"].ToString(), dr["WorkerCertificateCode"].ToString()));
        //        //**********可以修改成首次打印用考试报名照片，其它业务用人员照片
        //        if (File.Exists(personPhoto) == false)
        //        {
        //            personPhoto = Server.MapPath("~/Images/photo_ry.jpg");
        //        }
        //        System.Drawing.Image imgWarter = System.Drawing.Image.FromFile(personPhoto);//一寸照片
        //        string path = "";//证书照片背景图路径
        //        System.Drawing.Image imgSrc = null;//证书照片背景图
        //        Bitmap bmpDest = null;

        //        switch (dr["PostTypeID"].ToString())
        //        {
        //            case "1"://三类人（管理岗），大本 546 x 361
        //                #region 三类人
        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg4.png");
        //                imgSrc = System.Drawing.Image.FromFile(path);
        //                using (Graphics g = Graphics.FromImage(imgSrc))
        //                {
        //                    //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
        //                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //                    ////输出一寸照片
        //                    //g.DrawImage(imgWarter, new Rectangle(200, 37, 90, 126));

        //                    ////输出资格专用章
        //                    //System.Drawing.Image imgChapter = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter01.png"));
        //                    //g.DrawImage(imgChapter, new Rectangle(400, 236, 144, 144));

        //                    ////输出二维码  
        //                    //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(62, 30, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

                          
        //                    //输出证书信息
        //                    using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                    {
        //                        using (Brush b = new SolidBrush(Color.Black))
        //                        {

        //                            //姓　　名
        //                            g.DrawString(dr["WorkerName"].ToString(), f, b, new Rectangle(52, 188, 250, 34));
        //                            ////性别
        //                            g.DrawString(dr["Sex"].ToString(), f, b, new Rectangle(52, 225, 250, 34));

        //                            //出生日期(历史数据不在“1920年”之后的不打印)
        //                            g.DrawString((dr.IsNull("Birthday") == true || Convert.ToDateTime(dr["Birthday"]).CompareTo(new DateTime(1930, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["Birthday"]).ToString("yyyy年MM月dd日")), f, b, new Rectangle(52, 262, 250, 34));
        //                            //身份证号                               
        //                            g.DrawString(dr["WorkerCertificateCode"].ToString(), f, b, new Rectangle(52, 298, 250, 34));


        //                            //企业名称
        //                            DrawString(g, f, b, new Rectangle(390, 16, 250, 34), "请扫描二维码", 10, 5);
        //                            //DrawString(g, f, b, new Rectangle(375, 20, 250, 34), dr["UnitName"].ToString(), 10, 5);                              
        //                            //职务：岗位名称
        //                            g.DrawString(dr["PostName"].ToString(), f, b, new Rectangle(390, 90, 180, 68));
        //                            ////技术职称
        //                            //g.DrawString(dr["SkillLevel"].ToString(), f, b, new Rectangle(390, 127, 250, 34));
        //                            //证书编号
        //                            g.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(390, 164, 250, 34));


        //                            g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy-MM-dd")), f, b, new Rectangle(390, 285, 250, 34));
        //                            //发证日期
        //                            g.DrawString(Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy    MM    dd"), f, b, new Rectangle(410, 304, 250, 34));


        //                        }
        //                    }
        //                }

        //                //72dpi to 300dpi
        //                bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                bmpDest.SetResolution(300, 300);

        //                using (Graphics gg = Graphics.FromImage(bmpDest))
        //                {
        //                    gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //                    //输出资格专用章
        //                    System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter01.png"));
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    gg.DrawImage(imgChapter1, new Rectangle(1217, 740, 450, 450));

        //                    //输出一寸照片
        //                    gg.DrawImage(imgWarter, new Rectangle(194, 94, 281, 393));

        //                    //输出二维码  
        //                    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(500, 115, 268, 268), 0, 0, 268, 268, GraphicsUnit.Pixel);


        //                    //输出文字
        //                    gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                }
        //                bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);
        //                return;
        //                #endregion 三类人

        //            case "2"://特种作业（工人），小本340 x 226 ，1cm=34px      
        //                #region 特种作业


        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg5.png");
        //                imgSrc = System.Drawing.Image.FromFile(path);

        //                using (Graphics g = Graphics.FromImage(imgSrc))
        //                {
        //                    ////输出一寸照片
        //                    //g.DrawImage(imgWarter, new Rectangle(230, 98, 80, 112));

        //                    ////输出二维码  
        //                    //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(145, 148, 70, 70), 0, 0, 86, 86, GraphicsUnit.Pixel);
      
        //                    //输出证书信息
        //                    using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                    {
        //                        using (Font fb = new Font("宋体", 13, FontStyle.Bold))
        //                        {
        //                            using (Brush b = new SolidBrush(Color.Black))
        //                            {
        //                                g.DrawString("建筑施工特种作业操作资格证", fb, b, new Rectangle(44, 21, 295, 34));
        //                                using (Brush red = new SolidBrush(Color.Red))
        //                                {
        //                                    g.DrawString("证号", f, red, new Rectangle(50, 45, 60, 17));
        //                                }
        //                                g.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(80, 45, 295, 17));
        //                                g.DrawString(string.Format("姓名:{0} 身份证号:{1}", dr["WorkerName"].ToString(), dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(6, 71, 330, 17));
        //                                g.DrawString(string.Format("操作类别:{0}", dr["PostName"].ToString()), f, b, new Rectangle(6, 91, 320, 17));
        //                                g.DrawString(string.Format("初次领证:{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(85, 111, 179, 17));
        //                                g.DrawString(string.Format("制证日期:{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(85, 131, 179, 17));
        //                            }
        //                        }
        //                    }
        //                }

        //                //72dpi to 300dpi
        //                bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                bmpDest.SetResolution(300, 300);

        //                using (Graphics gg = Graphics.FromImage(bmpDest))
        //                {
        //                    gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //                    gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                    //输出证书专用章
        //                    System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter02.png"));
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    gg.DrawImage(imgChapter1, new Rectangle(47, 409, 269, 269));

        //                    //输出一寸照片
        //                    gg.DrawImage(imgWarter, new Rectangle(719, 326, 250, 350));

        //                    //输出二维码  
        //                    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(453, 462, 218, 218), 0, 0, 268, 268, GraphicsUnit.Pixel);
      

        //                    bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //                }

        //                //证书副本------------
        //                System.Drawing.Image imgSrc_f = null;//证书照片背景图
        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg5.png");
        //                imgSrc_f = System.Drawing.Image.FromFile(path);

        //                using (Graphics g_f = Graphics.FromImage(imgSrc_f))
        //                {
        //                    //输出证书信息
        //                    using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                    {
        //                        using (Font fb = new Font("宋体", 13, FontStyle.Bold))
        //                        {
        //                            using (Brush b = new SolidBrush(Color.Black))
        //                            {
        //                                g_f.DrawString("建筑施工特种作业操作资格证副证", fb, b, new Rectangle(25, 21, 295, 34));
        //                                using (Brush red = new SolidBrush(Color.Red))
        //                                {
        //                                    g_f.DrawString("证号", f, red, new Rectangle(50, 45, 60, 17));
        //                                }
        //                                g_f.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(80, 45, 295, 17));
        //                                g_f.DrawString(string.Format("姓名:{0} 身份证号:{1}", dr["WorkerName"].ToString(), dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(6, 71, 330, 17));
        //                                g_f.DrawString(string.Format("操作类别:{0}", dr["PostName"].ToString()), f, b, new Rectangle(6, 91, 320, 17));
        //                                g_f.DrawString("第一次复核记录:　　　　　第二次复核记录:", f, b, new Rectangle(6, 111, 300, 17));

        //                            }
        //                        }
        //                    }
        //                }
        //                string localPath_f = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}_f.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());

        //                //imgSrc_f.Save(Server.MapPath(localPath_f), System.Drawing.Imaging.ImageFormat.Png);

        //                //72dpi to 300dpi
        //                bmpDest = new Bitmap(Convert.ToInt32(imgSrc_f.Width * 300 / 96.012), Convert.ToInt32(imgSrc_f.Height * 300 / 96.012));
        //                bmpDest.SetResolution(300, 300);

        //                using (Graphics gg = Graphics.FromImage(bmpDest))
        //                {
        //                    gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                    gg.DrawImage(imgSrc_f, 0, 0, (float)(imgSrc_f.Width * 300 / 96.012), (float)(imgSrc_f.Height * 300 / 96.012));

        //                    bmpDest.Save(Server.MapPath(localPath_f), System.Drawing.Imaging.ImageFormat.Png);
        //                }
        //                return;

        //                #endregion 特种作业

        //            case "3"://造价员（管理岗），大本 590 x 415
        //                #region 造价员

        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg3.png");

        //                imgSrc = System.Drawing.Image.FromFile(path);

        //                using (Graphics g = Graphics.FromImage(imgSrc))
        //                {
        //                    ////输出一寸照片
        //                    //g.DrawImage(imgWarter, new Rectangle(345, 27, 102, 140));

        //                    ////输出二维码  
        //                    //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 165, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                    //输出证书信息
        //                    using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                    {
        //                        using (Brush b = new SolidBrush(Color.Black))
        //                        {
        //                            //发证日期
        //                            g.DrawString(Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日"), f, b, new Rectangle(80, 305, 250, 34));
        //                            //制证日期
        //                            g.DrawString(string.Format("制证日期： {0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 360, 250, 34));

        //                            //姓　　名
        //                            g.DrawString(dr["WorkerName"].ToString(), f, b, new Rectangle(430, 215, 250, 34));
        //                            //身份证号
        //                            g.DrawString(dr["WorkerCertificateCode"].ToString(), f, b, new Rectangle(430, 245, 250, 34));
        //                            //证书编号
        //                            g.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(430, 275, 250, 34));
        //                            //岗位名称                               
        //                            if (dr["AddItemName"] != null && dr["AddItemName"].ToString() != "")//造价员
        //                            {
        //                                g.DrawString(dr["AddItemName"].ToString() + "造价员", f, b, new Rectangle(430, 305, 180, 68));//增项
        //                            }
        //                            else
        //                            {
        //                                g.DrawString(dr["PostName"].ToString() + "造价员", f, b, new Rectangle(430, 305, 180, 68));//无增项
        //                            }

        //                        }
        //                    }
        //                }

        //                //72dpi to 300dpi
        //                bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                bmpDest.SetResolution(300, 300);

        //                using (Graphics gg = Graphics.FromImage(bmpDest))
        //                {
        //                    gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //                    //输出资格专用章
        //                    System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter03.png"));
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    gg.DrawImage(imgChapter1, new Rectangle(375, 550, 450, 450));

        //                    //输出一寸照片
        //                    gg.DrawImage(imgWarter, new Rectangle(1218, 184, 281, 393));

        //                    //输出二维码  
        //                    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(15, 515, 268, 268), 0, 0, 268, 268, GraphicsUnit.Pixel);


        //                    //输出文字
        //                    gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                    bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //                    return;
        //                }

        //                #endregion 造价员,只用专业人员证书模板打印
                      
        //            case "4"://职业技能岗位（工人），大本 567 x 395 边距1.27
        //                #region 职业技能岗位
        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg3.png");
        //                imgSrc = System.Drawing.Image.FromFile(path);
        //                using (Graphics g = Graphics.FromImage(imgSrc))
        //                {
        //                    if (dr["PostID"].ToString() == "158")//特殊：村镇工匠证书
        //                    {
        //                        //////输出一寸照片
        //                        //g.DrawImage(imgWarter, new Rectangle(190, 70, 90, 126));

        //                        ////输出二维码  
        //                        //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                        //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(430, 290, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                        //输出证书信息
        //                        using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                        {
        //                            using (Brush b = new SolidBrush(Color.Black))
        //                            {
        //                                //姓　　名                               
        //                                g.DrawString(string.Format("姓　　名： {0}", dr["WorkerName"].ToString()), f, b, new Rectangle(40, 178, 250, 34));
        //                                //性别
        //                                g.DrawString(string.Format("性　　别： {0}", dr["Sex"].ToString()), f, b, new Rectangle(40, 208, 250, 34));
        //                                //身份证号
        //                                g.DrawString(string.Format("身份证号： {0}", dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(40, 238, 250, 34));
        //                                //证书编号
        //                                g.DrawString(string.Format("证书编号： {0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(40, 268, 250, 34));
        //                                //发证日期
        //                                g.DrawString(string.Format("发证日期： {0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(40, 298, 250, 34));
        //                                //制证日期
        //                                g.DrawString(string.Format("制证日期： {0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(40, 328, 250, 34));

        //                                //职业(工种):                         
        //                                DrawString(g, f, b, new Rectangle(450, 27, 180, 68), dr["PostName"].ToString(), 8, 5);

        //                            }
        //                        }

        //                        //72dpi to 300dpi
        //                        bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                        bmpDest.SetResolution(300, 300);

        //                        using (Graphics gg = Graphics.FromImage(bmpDest))
        //                        {
        //                            gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                            gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                            gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //                            ////输出一寸照片
        //                            gg.DrawImage(imgWarter, new Rectangle(593, 219, 281, 394));

        //                            //输出二维码  
        //                            string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                            gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(1343, 906, 268, 268), 0, 0, 268, 268, GraphicsUnit.Pixel);


        //                            gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                            ////输出资格专用章
        //                            //System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter01.png"));
        //                            //gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                            //gg.DrawImage(imgChapter1, new Rectangle(1217, 740, 450, 450));

        //                            ////输出一寸照片
        //                            //gg.DrawImage(imgWarter, new Rectangle(16, 84, 281, 393));

        //                            bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //                            return;
        //                        }
        //                    }
        //                    else
        //                    {

        //                        ////输出一寸照片
        //                        //g.DrawImage(imgWarter, new Rectangle(430, 20, 90, 126));

        //                        ////输出二维码  
        //                        //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                        //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(15, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                        //输出证书信息
        //                        using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                        {
        //                            using (Brush b = new SolidBrush(Color.Black))
        //                            {

        //                                //培训考核机构
        //                                DrawString(g, f, b, new Rectangle(110, 290, 170, 68), dr["TrainUnitName"].ToString(), 10, 5);
        //                                //发证日期
        //                                g.DrawString(Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd 日"), f, b, new Rectangle(80, 335, 250, 34));
        //                                //制证日期
        //                                g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(80, 380, 250, 34));
        //                                //姓　　名                               
        //                                g.DrawString(dr["WorkerName"].ToString(), f, b, new Rectangle(430, 245, 250, 34));
        //                                //身份证号
        //                                g.DrawString(dr["WorkerCertificateCode"].ToString(), f, b, new Rectangle(430, 275, 250, 34));
        //                                //证书编号
        //                                g.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(430, 305, 250, 34));
        //                                //职业(工种):                         
        //                                DrawString(g, f, b, new Rectangle(430, 335, 180, 68), dr["PostName"].ToString(), 8, 5);
        //                                //等　　级
        //                                g.DrawString(dr["SkillLevel"].ToString(), f, b, new Rectangle(430, 375, 250, 34));
        //                            }
        //                        }

        //                        //72dpi to 300dpi
        //                        bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                        bmpDest.SetResolution(300, 300);

        //                        using (Graphics gg = Graphics.FromImage(bmpDest))
        //                        {
        //                            gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                            gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                            gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //                            //输出资格专用章
        //                            System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter03.png"));
        //                            gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                            gg.DrawImage(imgChapter1, new Rectangle(375, 550, 450, 450));

        //                            //输出一寸照片
        //                            gg.DrawImage(imgWarter, new Rectangle(1218, 184, 281, 393));

        //                            //输出文字
        //                            gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                            //输出二维码  
        //                            string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                            gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(47, 594, 268, 268), 0, 0, 268, 268, GraphicsUnit.Pixel);


        //                            bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //                            return;
        //                        }
        //                    }
        //                }

        //                #endregion 职业技能岗位

        //            case "5"://专业技术人员（管理岗），大本 567 x 395 边距1.27
        //                #region 专业技术人员
        //                //证书背景模板
        //                path = Server.MapPath(@"~/Images/certbg3.png");

        //                imgSrc = System.Drawing.Image.FromFile(path);

        //                using (Graphics g = Graphics.FromImage(imgSrc))
        //                {
        //                    ////输出一寸照片
        //                    //g.DrawImage(imgWarter, new Rectangle(480, 20, 90, 126));

        //                    ////输出二维码  
        //                    //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(15, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                    //输出证书信息
        //                    using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                    {
        //                        using (Brush b = new SolidBrush(Color.Black))
        //                        {
        //                            //发证日期
        //                            g.DrawString(Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd 日"), f, b, new Rectangle(80, 335, 250, 34));
        //                            //制证日期
        //                            g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(70, 380, 250, 34));

        //                            //姓　　名
        //                            g.DrawString(dr["WorkerName"].ToString(), f, b, new Rectangle(430, 245, 250, 34));
        //                            //身份证号
        //                            g.DrawString(dr["WorkerCertificateCode"].ToString(), f, b, new Rectangle(430, 275, 250, 34));
        //                            //证书编号
        //                            g.DrawString(dr["CertificateCode"].ToString(), f, b, new Rectangle(430, 305, 250, 34));
        //                            //岗位名称
        //                            DrawString(g, f, b, new Rectangle(430, 335, 180, 68), dr["PostName"].ToString(), 8, 5);

        //                        }
        //                    }
        //                }

        //                //72dpi to 300dpi
        //                bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //                bmpDest.SetResolution(300, 300);

        //                using (Graphics gg = Graphics.FromImage(bmpDest))
        //                {
        //                    gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                                        
        //                    //输出考试合格章
        //                    System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter03.png"));
        //                    gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                    gg.DrawImage(imgChapter1, new Rectangle(375, 550, 450, 450));

        //                    //输出一寸照片
        //                    gg.DrawImage(imgWarter, new Rectangle(1218, 184, 281, 393));

        //                    //输出二维码  
        //                    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                    gg.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 268, 268), new Rectangle(47, 594, 268, 268), 0, 0, 268, 268, GraphicsUnit.Pixel);


        //                    //输出文字
        //                    gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //                    bmpDest.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //                    return;
        //                }

        //                #endregion 专业技术人员

        //        }


        //        ////imgSrc.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Tiff);

        //        ////Bitmap bmpDest = new Bitmap(imgSrc, new System.Drawing.Size(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012)));
        //        //Bitmap bmpDest = new Bitmap(Convert.ToInt32(imgSrc.Width * 300 / 96.012), Convert.ToInt32(imgSrc.Height * 300 / 96.012));
        //        //bmpDest.SetResolution(300, 300);

        //        //Graphics gg = Graphics.FromImage(bmpDest);
        //        //gg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        //gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //        //gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //        //gg.DrawImage(imgSrc, 0, 0, (float)(imgSrc.Width * 300 / 96.012), (float)(imgSrc.Height * 300 / 96.012));

        //        ////输出资格专用章
        //        //System.Drawing.Image imgChapter1 = System.Drawing.Image.FromFile(Server.MapPath("~/Images/chapter01.png"));
        //        //gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //        //gg.DrawImage(imgChapter1, new Rectangle(1250, 737, 450, 450));

        //        imgSrc.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);

        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "证书打印失败", ex);
        //        return;
        //    }
        //}

        ////页面装载证书图片（多格）
        //private void ShowCertificatePhoto(DataRow dr)
        //{
        //    System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
        //    if (dr["PostTypeID"].ToString() == "2")//特种作业4格显示
        //    {
        //        img1.Width = new System.Web.UI.WebControls.Unit("48%");
        //    }
        //    else//其它1格显示
        //    {
        //        img1.Width = System.Web.UI.WebControls.Unit.Percentage(98);
        //    }
        //    divPhoto.Controls.Add(img1);
        //    img1.ImageUrl = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //    img1.BorderStyle = BorderStyle.Solid;
        //    img1.BorderColor = Color.Gray;
        //    img1.BorderWidth = new System.Web.UI.WebControls.Unit("1px");
        //    img1.Style.Add("padding-left", "1%");

        //    if (dr["PostTypeID"].ToString() == "2")//特种作业副证
        //    {
        //        System.Web.UI.WebControls.Image img2 = new System.Web.UI.WebControls.Image();
        //        img2.Width = new System.Web.UI.WebControls.Unit("48%");
        //        divPhoto.Controls.Add(img2);
        //        img2.ImageUrl = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}_f.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //        img2.BorderStyle = BorderStyle.Solid;
        //        img2.BorderColor = Color.Gray;
        //        img2.BorderWidth = new System.Web.UI.WebControls.Unit("1px");
        //        img2.Style.Add("padding-left", "1%");
        //    }
        //}

        ////检查文件保存路径
        //protected void CheckSaveDirectory(string PostType)
        //{
        //    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/"));
        //    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType));
        //}

        ////打印证书
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    if (printTable == null) return;
        //    if (printTable.Rows.Count > 0)
        //    {
        //        CheckSaveDirectory(printTable.Rows[0]["PostTypeID"].ToString());
        //    }

        //    //显示前2个证书(特种作业显示4个)
        //    string CertificateType = "";//生成文件名称
        //    string Certificate = "";//模板
        //    CertificateType = string.Format("多联证书打印_{0}_{1}.doc", DateTime.Now.ToString("yyMMddHHmmss"), PersonID.ToString());
        //    switch (printTable.Rows[0]["PostTypeID"].ToString())
        //    {
        //        case "1":
        //            Certificate = "三类人员证书打印_喷墨.doc";
        //            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
        //        , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType)
        //        , GetPrintData(1));
        //            break;

        //        case "2":
        //            Certificate = "特种作业证书打印_喷墨.doc";
        //            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
        //        , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType)
        //        , GetPrintData(4));
        //            break;
        //        default:
        //            Certificate = "专业员证书打印_喷墨.doc";
        //            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
        //        , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType)
        //        , GetPrintData(1));
        //            break;

        //    }

        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/PrintCertificate/{0}/{1}');", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType, RootUrl), true);

        //    BindPrintDetail(CurrentIndex + ShowCount);
        //}

        ////获取打印数据（按ListView分页导出Word）
        //protected Dictionary<string, string> GetPrintData(int ItemCount)
        //{
        //    Dictionary<string, string> printData = new Dictionary<string, string>();
        //    string[] labels = "Photo,Img_Photo".Split(',');//替换标签名称
        //    for (int i = 0; i < ItemCount; i++)
        //    {
        //        if ((CurrentIndex + i) > printList.Count - 1)//最后一页有空行
        //        {
        //            for (int j = 0; j < labels.Length; j++)
        //            {
        //                if (labels[j].IndexOf("Img_") != -1)//照片
        //                    printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
        //                else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
        //                    printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
        //                else
        //                    printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");

        //                if (printTable.Rows[0]["PostTypeID"].ToString() == "2")//副本
        //                {
        //                    if (labels[j].IndexOf("Img_") != -1)//照片
        //                        printData.Add(string.Format("{0}Fu{1}", labels[j], i.ToString()), "~/Images/null.gif");
        //                    else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
        //                        printData.Add(string.Format("{0}Fu{1}", labels[j], i.ToString()), "null");
        //                    else
        //                        printData.Add(string.Format("{0}Fu{1}", labels[j], i.ToString()), "");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            printData.Add(string.Format("Photo{0}", Convert.ToString(i)), printTable.Rows[CurrentIndex + i]["CertificateID"].ToString());//照片url
        //            printData.Add(string.Format("Img_Photo{0}", Convert.ToString(i)), GetPhotoPath(printTable.Rows[CurrentIndex + i]));//照片url     

        //            if (printTable.Rows[0]["PostTypeID"].ToString() == "2")//副本
        //            {
        //                printData.Add(string.Format("PhotoFu{0}", Convert.ToString(i)), "Fu" + printTable.Rows[CurrentIndex + i]["CertificateID"].ToString());//照片url
        //                printData.Add(string.Format("Img_PhotoFu{0}", Convert.ToString(i)), GetPhotoPath_Fu(printTable.Rows[CurrentIndex + i]));//照片url     

        //            }

        //            CertificateDAL.UpdatePrintTime(Convert.ToInt64(printTable.Rows[CurrentIndex + i]["CertificateID"]), PersonName, DateTime.Now, 3);
        //        }
        //    }
        //    //int cellCount = 0;//模板格子数量
        //    //if (ItemCount == 2)
        //    //{
        //    //    cellCount = 4;
        //    //}
        //    //else
        //    //{
        //    //    cellCount = 2;
        //    //}
        //    //if (printTable.Rows.Count % cellCount != 0)//最后一页有空行
        //    //{
        //    //    string[] labels = "Photo,Img_Photo".Split(',');//替换标签名称

        //    //    for (int i = (printTable.Rows.Count % cellCount) ; i < cellCount; i++)
        //    //    {
        //    //        for (int j = 0; j < labels.Length; j++)
        //    //        {
        //    //            if (labels[j].IndexOf("Img_") != -1)//照片
        //    //                printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
        //    //            else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
        //    //                printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
        //    //            else
        //    //                printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
        //    //        }
        //    //    }
        //    //}

        //    return printData;
        //}

        //public string GetPhotoPath(DataRow dr)
        //{
        //    BindPhoto(dr);

        //    //证书图片路径
        //    string newpath = string.Format("~/Upload/PhotoCertificate/{0}/{1}.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //    return newpath;
        //}

        //public string GetPhotoPath_Fu(DataRow dr)
        //{
        //    BindPhoto(dr);

        //    //证书图片路径
        //    string newpath = string.Format("~/Upload/PhotoCertificate/{0}/{1}_f.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //    return newpath;
        //}

        ////next
        //protected void ButtonNext_Click(object sender, EventArgs e)
        //{
        //    BindPrintDetail(CurrentIndex + ShowCount);
        //}

        ////Prev
        //protected void ButtonPrev_Click(object sender, EventArgs e)
        //{
        //    BindPrintDetail(CurrentIndex - ShowCount);
        //}

        ////返回
        //protected void ButtonReturn_Click(object sender, EventArgs e)
        //{

        //    if (ViewState["ChangeType"].ToString() != "" && ViewState["PostTypeID"].ToString() != "")
        //        Response.Redirect(string.Format("PMCertPrintManage.aspx?o={0}&t={1}", ViewState["PostTypeID"].ToString(), ViewState["ChangeType"].ToString()));
        //    else
        //        Response.Redirect("PMCertPrintManage.aspx");
        //}

        ////批量分时打印
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    if (ButtonPrint.Enabled == true) ButtonPrint_Click(sender, e);
        //}

        ////绑定带打印证书详细
        //protected void BindPrintDetail(int index)
        //{
        //    if (index > printList.Count - 1)//打印结束
        //    {
        //        divPhoto.Controls.Clear();
        //        System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
        //        divPhoto.Controls.Add(img1);
        //        img1.ImageUrl = "~/images/print.png";
        //        img1.BorderStyle = BorderStyle.None;
        //        ViewState["printList"] = new List<string>();
        //        ViewState["printTable"] = null;
        //        CurrentIndex = 0;
        //        CheckBoxAutoPrint.Checked = false;
        //        P_PrintTimeSpan.Visible = false;

        //        ButtonPrint.Visible = false;
        //        Timer1.Enabled = false;
        //        return;
        //    }
        //    CurrentIndex = index;
        //    DataRow dr = (ViewState["printTable"] as DataTable).Rows[index];

        //    int OutputCount = dr["PostTypeID"].ToString() == "2" ? 4 : 1;//一次输出证书数量

        //    divPhoto.Controls.Clear();

        //    for (int i = 0; i < OutputCount; i++)
        //    {
        //        if (index + i > printList.Count - 1)
        //        {
        //            return;
        //        }

        //        dr = (ViewState["printTable"] as DataTable).Rows[index + i];
        //        BindPhoto(dr);
        //        ShowCertificatePhoto(dr);
        //    }

        //    if (CheckBoxAutoPrint.Checked == true)//自动打印
        //    {
        //        if (Timer1.Enabled == false)
        //        {
        //            Timer1.Interval = Convert.ToInt32(RadioButtonListPrintTimeSpan.SelectedValue) * 1000;
        //            Timer1.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        if (Timer1.Enabled == true)
        //        {
        //            Timer1.Enabled = false;
        //        }
        //    }
        //}

    }
}
