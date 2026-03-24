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
    /// <summary>
    /// 证书打印（作废）
    /// </summary>
    public partial class NewCertificatePrint : BasePage
    {
        
        //protected override string CheckVisiteRgihtUrl
        //{
        //    get
        //    {
        //        return "NewCertificatePrintManage.aspx";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
                //if (Session["printList"] != null)//批量打印
                //{
                //    ViewState["printList"] = Session["printList"];
                //    Session.Remove("printList");
                //    ViewState["printTable"] = Session["printTable"];
                //    Session.Remove("printTable");

                //    ShowCount = (ViewState["printTable"] as DataTable).Rows[0]["PostTypeID"].ToString() == "2" ? 4 : 2;
                //}
                //else//单个打印
                //{
                //    ViewState["printList"] = new List<string> { Request.QueryString["CertificateID"] };
                //    DataTable dt = CertificateDAL.GetList(0, int.MaxValue - 1, " and CertificateID=" + Request.QueryString["CertificateID"], "CertificateCode");
                //    ViewState["printTable"] = dt;
                //    ShowCount = dt.Rows[0]["PostTypeID"].ToString() == "2" ? 4 : 2; ;

                //}

                //ViewState["ChangeType"] = Request["rnt_t"];//变更类型（续期=continue，变更=manage）
                //ViewState["PostTypeID"] = Request["rtn_o"];//岗位类型ID
                ////显示前2个证书(特种作业显示4个)
                //if (printTable != null)
                //{
                //    BindPrintDetail(0);
                //}
            }
        }

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

        //private string GetFacePhotoPath(string ExamPlanID, string WorkerCertificateCode)
        //{
        //    if (string.IsNullOrEmpty(WorkerCertificateCode) == true) return "~/Images/null.gif";
        //    string path = "";

        //    if (ExamPlanID != "-100" && ExamPlanID != "-200" && ExamPlanID != "-300")//通过报名考试系统发证                
        //    {
        //        path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, WorkerCertificateCode);
        //        if (File.Exists(Server.MapPath(path)) == true) return path;
        //    }

        //    path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        //    if (File.Exists(Server.MapPath(path)) == true)
        //        return path;
        //    else
        //        return "~/Images/null.gif";
        //}

        ////验证证书图片是否创建，没有创建立即创建
        //private void BindPhoto(DataRow dr)
        //{
        //    //证书图片路径
        //    string localPath = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());

        //    //if (System.IO.File.Exists(localPath) == true)//本地证书证照存在,不创建？？（是放在审核决定时生成还是打印时生成有待考虑）
        //    //{
        //    //    return;
        //    //}
        //    //人员照片 /UpLoad/WorkerPhoto/930/110106195406133930.jpg
        //    //string personPhoto = Server.MapPath(string.Format(@"~/UpLoad/WorkerPhoto/{0}/{1}.jpg", dr["WorkerCertificateCode"].ToString().Substring(dr["WorkerCertificateCode"].ToString().Length - 3, 3), dr["WorkerCertificateCode"].ToString()));

        //    //string personPhoto = Server.MapPath(GetFacePhotoPath(dr["ExamPlanID"].ToString(),dr["WorkerCertificateCode"].ToString()));
        //    string personPhoto = Server.MapPath(UIHelp.GetFaceImagePath(dr["FACEPHOTO"].ToString(), dr["WorkerCertificateCode"].ToString()));
        //    //**********可以修改成首次打印用考试报名照片，其它业务用人员照片
        //    if (File.Exists(personPhoto) == false)
        //    {
        //        personPhoto = Server.MapPath("~/Images/photo_ry.jpg");
        //    }
        //    System.Drawing.Image imgWarter = System.Drawing.Image.FromFile(personPhoto);//一寸照片
        //    string path = "";//证书照片背景图路径
        //    System.Drawing.Image imgSrc = null;//证书照片背景图

        //    switch (dr["PostTypeID"].ToString())
        //    {
        //        case "1"://三类人（管理岗），大本 590 x 415
        //            #region 三类人
        //            //证书背景模板
        //            path = Server.MapPath(@"~/Images/certbg1.png");
        //            imgSrc = System.Drawing.Image.FromFile(path);
        //            using (Graphics g = Graphics.FromImage(imgSrc))
        //            {
        //                //输出一寸照片
        //                g.DrawImage(imgWarter, new Rectangle(430, 57, 102, 140));

        //                //输出二维码  
        //                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                //输出证书信息
        //                using (Font f = new Font("宋体", 11, FontStyle.Regular))
        //                {
        //                    using (Brush b = new SolidBrush(Color.Black))
        //                    {
        //                        g.DrawString("　　本证书由中华人民共和国住房和城乡", f, b, new Rectangle(5, 38, 280, 132));
        //                        g.DrawString("建设部监制，各省、自治区、直辖市住房", f, b, new Rectangle(5, 68, 280, 132));
        //                        g.DrawString("城乡建设主管部门批准颁发。本证书表明", f, b, new Rectangle(5, 98, 280, 132));
        //                        g.DrawString("持证人已通过住房城乡建设领域专业人员", f, b, new Rectangle(5, 128, 280, 132));
        //                        g.DrawString("岗位培训考核评价，成绩合格。", f, b, new Rectangle(5, 158, 280, 132));

        //                        g.DrawString("发证单位：（盖章）", f, b, new Rectangle(5, 310, 250, 68));
        //                        g.DrawString(string.Format("发证日期：{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 340, 250, 34));
        //                        g.DrawString("查询网址：www.bjjs.gov.cn", f, b, new Rectangle(5, 370, 250, 68));
        //                        g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 400, 250, 34));

        //                        g.DrawString(string.Format("姓　　名：{0}", dr["WorkerName"].ToString()), f, b, new Rectangle(345, 250, 250, 34));
        //                        g.DrawString(string.Format("身份证号：{0}", dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(345, 280, 250, 34));
        //                        g.DrawString(string.Format("证书编号：{0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(345, 310, 250, 34));
        //                        g.DrawString("岗位名称：", f, b, new Rectangle(345, 340, 250, 68));
        //                        g.DrawString(dr["PostName"].ToString(), f, b, new Rectangle(420, 340, 180, 68));
        //                        g.DrawString(string.Format("有效期至：{0}", dr.IsNull("ValidEndDate") == false && Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "1231" ? Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy 年 MM 月 dd日") : ""), f, b, new Rectangle(345, 400, 250, 34));
        //                    }
        //                }
        //            }

        //            #endregion 三类人
        //            break;
        //        case "2"://特种作业（工人），小本306 x 200 ，1cm=34px      
        //            #region 特种作业

        //            //imgWarter.Width = 55;
        //            //imgWarter.Height = 68;
        //            //证书背景模板
        //            path = Server.MapPath(@"~/Images/certbg2.png");
        //            imgSrc = System.Drawing.Image.FromFile(path);

        //            using (Graphics g = Graphics.FromImage(imgSrc))
        //            {
        //                //输出一寸照片
        //                //g.DrawImage(imgWarter, new Rectangle(235, 85, 80, 112), 0, 0, 102, 140, GraphicsUnit.Pixel);
        //                g.DrawImage(imgWarter, new Rectangle(235, 85, 80, 112));

        //                //输出二维码  
        //                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(6, 132, 65, 65), 0, 0, 86, 86, GraphicsUnit.Pixel);
        //                //                                                                                                                             86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel)
        //                //输出证书信息
        //                using (Font f = new Font("宋体", 10, FontStyle.Regular))
        //                {
        //                    using (Font fb = new Font("宋体", 12, FontStyle.Regular))
        //                    {
        //                        using (Brush b = new SolidBrush(Color.Black))
        //                        {
        //                            g.DrawString("建筑施工特种作业操作资格证", fb, b, new Rectangle(34, 8, 295, 34));
        //                            g.DrawString(string.Format("证号：{0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(40, 28, 295, 17));
        //                            g.DrawString(string.Format("姓名：{0}　身份证号：{1}", dr["WorkerName"].ToString(), dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(6, 48, 295, 17));
        //                            g.DrawString(string.Format("操作类别：{0}", dr["PostName"].ToString()), f, b, new Rectangle(6, 68, 295, 17));
        //                            g.DrawString(string.Format("初次领证：{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(75, 88, 179, 17));
        //                            g.DrawString(string.Format("有效期至：{0}", dr.IsNull("ValidEndDate") == false && (Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "1231" || Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "0630") ? Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd") : ""), f, b, new Rectangle(75, 108, 179, 17));
        //                            g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy.MM.dd")), f, b, new Rectangle(75, 128, 179, 17));

        //                        }
        //                    }
        //                }
        //            }
        //            #endregion 特种作业
        //            break;
        //        case "3"://造价员（管理岗），大本 590 x 415
        //            #region 造价员
        //            //证书背景模板
        //            path = Server.MapPath(@"~/Images/certbg1.png");
        //            imgSrc = System.Drawing.Image.FromFile(path);
        //            using (Graphics g = Graphics.FromImage(imgSrc))
        //            {

        //                //输出一寸照片
        //                g.DrawImage(imgWarter, new Rectangle(430, 57, 102, 140));

        //                //输出二维码  
        //                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                //输出证书信息
        //                using (Font f = new Font("宋体", 11, FontStyle.Regular))
        //                {
        //                    using (Brush b = new SolidBrush(Color.Black))
        //                    {
        //                        g.DrawString("　　本证书由中华人民共和国住房和城乡", f, b, new Rectangle(5, 38, 280, 132));
        //                        g.DrawString("建设部监制，各省、自治区、直辖市住房", f, b, new Rectangle(5, 68, 280, 132));
        //                        g.DrawString("城乡建设主管部门批准颁发。本证书表明", f, b, new Rectangle(5, 98, 280, 132));
        //                        g.DrawString("持证人已通过住房城乡建设领域专业人员", f, b, new Rectangle(5, 128, 280, 132));
        //                        g.DrawString("岗位培训考核评价，成绩合格。", f, b, new Rectangle(5, 158, 280, 132));

        //                        g.DrawString("发证单位：（盖章）", f, b, new Rectangle(5, 310, 250, 68));
        //                        g.DrawString(string.Format("发证日期：{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 340, 250, 34));
        //                        g.DrawString("查询网址：www.bjjs.gov.cn", f, b, new Rectangle(5, 370, 250, 68));
        //                        g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 400, 250, 34));

        //                        g.DrawString(string.Format("姓　　名：{0}", dr["WorkerName"].ToString()), f, b, new Rectangle(345, 250, 250, 34));
        //                        g.DrawString(string.Format("身份证号：{0}", dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(345, 280, 250, 34));
        //                        g.DrawString(string.Format("证书编号：{0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(345, 310, 250, 34));
        //                        g.DrawString("岗位名称：", f, b, new Rectangle(345, 340, 250, 68));
        //                        if (dr["AddItemName"] != null && dr["AddItemName"].ToString() != "")//造价员
        //                        {
        //                            g.DrawString(dr["AddItemName"].ToString(), f, b, new Rectangle(420, 340, 180, 68));//增项
        //                        }
        //                        else
        //                        {
        //                            g.DrawString(dr["PostName"].ToString(), f, b, new Rectangle(420, 340, 180, 68));//无增项
        //                        }
        //                        g.DrawString(string.Format("有效期至：{0}", dr.IsNull("ValidEndDate") == false && Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "1231" ? Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy 年 MM 月 dd日") : ""), f, b, new Rectangle(345, 400, 250, 34));
        //                    }
        //                }
        //            }

        //            #endregion 造价员
        //            break;
        //        case "4"://职业技能岗位（工人），大本 590 x 415
        //            #region 职业技能岗位
        //            //证书背景模板
        //            path = Server.MapPath(@"~/Images/certbg1.png");
        //            imgSrc = System.Drawing.Image.FromFile(path);
        //            using (Graphics g = Graphics.FromImage(imgSrc))
        //            {
        //                //输出一寸照片
        //                g.DrawImage(imgWarter, new Rectangle(430, 57, 102, 140));

        //                //输出二维码  
        //                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                //输出证书信息
        //                using (Font f = new Font("宋体", 11, FontStyle.Regular))
        //                {
        //                    using (Brush b = new SolidBrush(Color.Black))
        //                    {
        //                        //输出证书说明
        //                        g.DrawString("　　本证书表明持证人已通过", f, b, new Rectangle(5, 94, 230, 75));
        //                        g.DrawString("住房城乡建设行业职业技能培", f, b, new Rectangle(5, 124, 230, 75));
        //                        g.DrawString("训考核，成绩合格。", f, b, new Rectangle(5, 154, 230, 75));
        //                        //职业技能证书不打印发证单位，打印培训机构
        //                        g.DrawString("培训考核机构：", f, b, new Rectangle(5, 290, 250, 68));
        //                        //g.DrawString(dr["TrainUnitName"].ToString(), f, b, new Rectangle(110, 290, 150, 68));
        //                        DrawString(g, f, b, new Rectangle(110, 290, 170, 68), dr["TrainUnitName"].ToString(), 10, 5);

        //                        g.DrawString(string.Format("发证日期：{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 340, 250, 34));
        //                        g.DrawString("查询网址：www.bjjs.gov.cn", f, b, new Rectangle(5, 370, 250, 68));
        //                        g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 400, 250, 34));

        //                        g.DrawString(string.Format("姓　　名：{0}", dr["WorkerName"].ToString()), f, b, new Rectangle(345, 250, 250, 34));
        //                        g.DrawString(string.Format("身份证号：{0}", dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(345, 280, 250, 34));
        //                        g.DrawString(string.Format("证书编号：{0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(345, 310, 250, 34));
        //                        g.DrawString("职业(工种):", f, b, new Rectangle(345, 340, 250, 68));
        //                        //g.DrawString(dr["PostName"].ToString(), f, b, new Rectangle(430, 340, 180, 68));
        //                        DrawString(g, f, b, new Rectangle(430, 340, 180, 68), dr["PostName"].ToString(), 8, 5);
        //                        //g.DrawString(string.Format("有效期至：{0}", dr.IsNull("ValidEndDate") == false && Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "1231" ? Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy 年 MM 月 dd日") : ""), f, b, new Rectangle(345, 400, 250, 34));
        //                        g.DrawString(string.Format("等　　级：{0}", dr["SkillLevel"].ToString()), f, b, new Rectangle(345, 400, 250, 34));
        //                    }
        //                }
        //            }

        //            #endregion 职业技能岗位
        //            break;
        //        case "5"://专业技术人员（管理岗），大本 590 x 415
        //            #region 专业技术人员
        //            //证书背景模板
        //            path = Server.MapPath(@"~/Images/certbg1.png");

        //            imgSrc = System.Drawing.Image.FromFile(path);

        //            using (Graphics g = Graphics.FromImage(imgSrc))
        //            {
        //                //输出一寸照片
        //                g.DrawImage(imgWarter, new Rectangle(430, 57, 102, 140));

        //                //输出二维码  
        //                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
        //                g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

        //                //输出证书信息
        //                using (Font f = new Font("宋体", 11, FontStyle.Regular))
        //                {
        //                    using (Brush b = new SolidBrush(Color.Black))
        //                    {
        //                        g.DrawString("　　本证书由中华人民共和国住房和城乡", f, b, new Rectangle(5, 38, 280, 132));
        //                        g.DrawString("建设部监制，各省、自治区、直辖市住房", f, b, new Rectangle(5, 68, 280, 132));
        //                        g.DrawString("城乡建设主管部门批准颁发。本证书表明", f, b, new Rectangle(5, 98, 280, 132));
        //                        g.DrawString("持证人已通过住房城乡建设领域专业人员", f, b, new Rectangle(5, 128, 280, 132));
        //                        g.DrawString("岗位培训考核评价，成绩合格。", f, b, new Rectangle(5, 158, 280, 132));

        //                        g.DrawString("发证单位：（盖章）", f, b, new Rectangle(5, 310, 250, 68));
        //                        g.DrawString(string.Format("发证日期：{0}", Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 340, 250, 34));
        //                        g.DrawString("查询网址：www.bjjs.gov.cn", f, b, new Rectangle(5, 370, 250, 68));
        //                        g.DrawString(string.Format("制证日期：{0}", Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy 年 MM 月 dd日")), f, b, new Rectangle(5, 400, 250, 34));


        //                        g.DrawString(string.Format("姓　　名：{0}", dr["WorkerName"].ToString()), f, b, new Rectangle(345, 250, 250, 34));
        //                        g.DrawString(string.Format("身份证号：{0}", dr["WorkerCertificateCode"].ToString()), f, b, new Rectangle(345, 280, 250, 34));
        //                        g.DrawString(string.Format("证书编号：{0}", dr["CertificateCode"].ToString()), f, b, new Rectangle(345, 310, 250, 34));
        //                        g.DrawString("岗位名称：", f, b, new Rectangle(345, 340, 250, 68));
        //                        //g.DrawString(dr["PostName"].ToString(), f, b, new Rectangle(420, 340, 180, 68));
        //                        DrawString(g, f, b, new Rectangle(420, 340, 180, 68), dr["PostName"].ToString(), 8, 5);
        //                        g.DrawString(string.Format("有效期至：{0}", dr.IsNull("ValidEndDate") == false && Convert.ToDateTime(dr["ValidEndDate"]).ToString("MMdd") == "1231" ? Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy 年 MM 月 dd日") : ""), f, b, new Rectangle(345, 400, 250, 34));
        //                    }
        //                }
        //            }

        //            #endregion 专业技术人员
        //            break;
        //    }

        //    if (!System.IO.Directory.Exists(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", dr["PostID"].ToString()))))
        //    {
        //        System.IO.Directory.CreateDirectory(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", dr["PostID"].ToString())));
        //    }
        //    imgSrc.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);
        //}

        ////页面装载证书图片（多格）
        //private void ShowCertificatePhoto(DataRow dr)
        //{
        //    System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
        //    if (dr["PostTypeID"].ToString() == "2")//特种作业4格显示
        //    {
        //        img1.Width = new System.Web.UI.WebControls.Unit("48%");
        //    }
        //    else//其它2格显示
        //    {
        //        img1.Width = System.Web.UI.WebControls.Unit.Percentage(98);
        //    }
        //    divPhoto.Controls.Add(img1);
        //    img1.ImageUrl = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", dr["PostID"].ToString(), dr["CertificateCode"].ToString());
        //    img1.BorderStyle = BorderStyle.Solid;
        //    img1.BorderColor = Color.Gray;
        //    img1.BorderWidth = new System.Web.UI.WebControls.Unit("1px");
        //    img1.Style.Add("padding-left", "1%");
        //    //img1.Attributes.Add("onmousedown", "mousedown(this);");
        //    //img1.Attributes.Add("onmouseup", "mouseup(this);");  

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
        //    if (printTable.Rows[0]["PostTypeID"].ToString() == "2")
        //    {
        //        Certificate = "4格证书打印.doc";
        //        Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
        //    , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType)
        //    , GetPrintData(4));
        //    }
        //    else
        //    {
        //        Certificate = "2格证书打印.doc";
        //        Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
        //    , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", printTable.Rows[0]["PostTypeID"].ToString(), CertificateType)
        //    , GetPrintData(2));
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
        //            }
        //        }
        //        else
        //        {
        //            printData.Add(string.Format("Photo{0}", Convert.ToString(i)), printTable.Rows[CurrentIndex + i]["CertificateID"].ToString());//照片url
        //            printData.Add(string.Format("Img_Photo{0}", Convert.ToString(i)), GetPhotoPath(printTable.Rows[CurrentIndex + i]));//照片url     

        //            CertificateDAL.UpdatePrintTime(Convert.ToInt64(printTable.Rows[CurrentIndex + i]["CertificateID"]), PersonName, DateTime.Now,2);
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
        //        Response.Redirect(string.Format("NewCertificatePrintManage.aspx?o={0}&t={1}", ViewState["PostTypeID"].ToString(), ViewState["ChangeType"].ToString()));
        //    else
        //        Response.Redirect("NewCertificatePrintManage.aspx");
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
        //        p_tzzy.Visible = false;
        //        ButtonPrint.Visible = false;
        //        Timer1.Enabled = false;
        //        return;
        //    }
        //    CurrentIndex = index;
        //    DataRow dr = (ViewState["printTable"] as DataTable).Rows[index];

        //    int OutputCount = dr["PostTypeID"].ToString() == "2" ? 4 : 2;//一次输出证书数量
        //    if (dr["PostTypeID"].ToString() == "2")//特种作业
        //    {
        //        p_tzzy.Visible = true;
        //    }
        //    else
        //    {
        //        p_tzzy.Visible = false;
        //    }

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
