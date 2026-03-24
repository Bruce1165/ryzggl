using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Data;
using DataAccess;
using System.Drawing;
using Telerik.Web.UI;
using System.IO;


namespace ZYRYJG.County
{
    //证书打印（作废）
    public partial class CertPrint : BasePage
    {

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/CertPrintMgr.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Clear();
                //if (Session["printList"] != null)//批量打印
                //{
                //    ViewState["printList"] = Session["printList"];
                //    Session.Remove("printList");
                //    ViewState["printTable"] = Session["printTable"];
                //    Session.Remove("printTable");

                //    ShowCount = 1;
                //}
                //else//单个打印
                //{
                //    ViewState["printList"] = new List<string> { Request["o"] };
                //    DataTable dt = COC_TOW_Person_BaseInfoDAL.GetList(0, 1, string.Format(" and PSN_ServerID='{0}'" , Request["o"]), "PSN_ServerID");
                //    ViewState["printTable"] = dt;
                //    ShowCount = 1 ;
                //}

                //if (printTable != null)
                //{
                //    BindPrintDetail(0);
                //}
            }
        }

        //一次打印证书个数
        public int ShowCount
        {
            get { return Convert.ToInt32(ViewState["ShowCount"]); }
            set { ViewState["ShowCount"] = value; }
        }

        /// <summary>
        /// 待打印列证书ID集合
        /// </summary>
        public List<string> printList
        {
            get { return ViewState["printList"] as List<string>; }
        }

        public DataTable printTable
        {
            get { return ViewState["printTable"] as DataTable; }
        }

        /// <summary>
        /// 当前显示证书索引，从0开始
        /// </summary>
        public int CurrentIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentIndex"]); }
            set
            {
                ViewState["CurrentIndex"] = value;
                if (printList.Count < 2)
                {
                    ButtonNext.Visible = false;
                    ButtonPrev.Visible = false;
                    LabelPage.Visible = false;
                    P_PrintTimeSpan.Visible = false;
                }
                else
                {
                    P_PrintTimeSpan.Visible = true;
                    ButtonNext.Visible = true;
                    ButtonPrev.Visible = true;
                    LabelPage.Visible = true;
                    LabelPage.Text = string.Format("第 {1} 条   共 {0} 条", printList.Count, value + 1);

                    if (value == 0)
                        ButtonPrev.Enabled = false;
                    else
                        ButtonPrev.Enabled = true;

                    if (value == printList.Count - 1)
                        ButtonNext.Enabled = false;
                    else
                        ButtonNext.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 输出文字，自动折行
        /// </summary>
        /// <param name="lineSize">每行最大字数</param>
        /// <param name="linePad">行间距</param>
        /// <param name="str">输出内容</param>
        /// <param name="f">字体</param>
        /// <param name="b">画笔</param>
        /// <param name="rect">输出区域</param>
        /// <param name="lineSize">行最大输入字数</param>
        /// <param name="linePad">行间距</param>
        private void DrawString(Graphics g, Font f, Brush b, Rectangle rect, string str, int lineSize, int linePad)
        {
            int curSize = 0;//当前输出字符位置
            int lineCount = 0;//输出行数量

            while (curSize < str.Length)
            {
                if (str.Length - curSize >= lineSize)
                    g.DrawString(str.Substring(curSize, lineSize), f, b, new Rectangle(rect.X, rect.Y + (lineCount * (15 + linePad)), rect.Width, rect.Height));
                else
                    g.DrawString(str.Substring(curSize, str.Length - curSize), f, b, new Rectangle(rect.X, rect.Y + (lineCount * (15 + linePad)), rect.Width, rect.Height));
                lineCount++;
                curSize += lineSize;
            }
        }
        
        //验证证书图片是否创建，没有创建立即创建
        private void BindPhoto(DataRow dr)
        {
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PhotoCertificate/")))
            {
                Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PhotoCertificate/"));
            }

            string pathsub = dr["PSN_CertificateNO"].ToString().Substring(dr["PSN_CertificateNO"].ToString().Length - 3, 3);
            //证书图片路径
            string localPath = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", pathsub, dr["PSN_CertificateNO"]);

            if (!Directory.Exists(Page.Server.MapPath( string.Format("~/UpLoad/PhotoCertificate/{0}/" , pathsub))))
            {
                Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", pathsub)));
            }

            string path = "";//证书照片背景图路径
            System.Drawing.Image imgSrc = null;//证书照片背景图
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;



            //证书背景模板
            path = Server.MapPath(@"~/Images/certbg1.png");
            imgSrc = System.Drawing.Image.FromFile(path);
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                ////输出二维码  
                //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
                //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://210.75.213.233/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

                //输出证书信息
                using (Font f = new Font( "宋体", 11, FontStyle.Regular))
                {
                    Font f2 = new Font("宋体", 10, FontStyle.Regular);

                    using (Brush b = new SolidBrush(Color.Black))
                    {
                       
                        if (printTable.Rows[0]["PSN_Level"].ToString().Trim() == "二级")//输出资格证书编号
                        {
                            //g.DrawString(printTable.Rows[0]["ZGZSBH"] != DBNull.Value ? printTable.Rows[0]["ZGZSBH"].ToString() : "", f, b, new Rectangle(115, 250, 250, 34));//资格证书编号
                            #region 新的
                            if (printTable.Rows[0]["ZGZSBH"] != DBNull.Value && printTable.Rows[0]["ZGZSBH"].ToString().Length == 18)
                            {
                                g.DrawString("管理号 " + printTable.Rows[0]["ZGZSBH"].ToString(), f2, b, new Rectangle(25, 200, 250, 34));//管理号
                                g.DrawString("Administration Number", f2, b, new Rectangle(25, 215, 250, 34));//管理号英文
                                g.DrawString("", f, b, new Rectangle(115, 250, 250, 34));//资格证书编号

                            }
                            else
                            {

                                g.DrawString(" ", f2, b, new Rectangle(25, 200, 250, 34));//管理号
                                g.DrawString(" ", f, b, new Rectangle(25, 215, 250, 34));//管理号英文

                                //g.DrawString(dr["资格证号"].ToString(), f, b, new Rectangle(110, 230, 250, 43));//资格证书编号
                                g.DrawString(printTable.Rows[0]["ZGZSBH"] != DBNull.Value ? printTable.Rows[0]["ZGZSBH"].ToString() : "", f, b, new Rectangle(115, 250, 250, 34));//资格证书编号

                            }
                            #endregion
                            
                        }

                        g.DrawString(dr["PSN_RegisterNo"].ToString(), f, b, new Rectangle(110, 305, 250, 34));//注册编号
                        g.DrawString(dr["PSN_Name"].ToString(), f, b, new Rectangle(405, 50, 165, 34), format);//姓名
                        g.DrawString(dr["PSN_Sex"].ToString(), f, b, new Rectangle(405, 84, 165, 34), format);//性别
                        g.DrawString(Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年MM月dd日"), f, b, new Rectangle(405, 120, 165, 34), format);//出生日期
                        g.DrawString(dr["PSN_RegisteProfession"].ToString(), f, b, new Rectangle(405, 159, 165, 34), format);//专业类别
                        //g.DrawString(dr["ENT_Name"].ToString(), f, b, new Rectangle(445, 294, 250, 68));//聘用企业
                        DrawString(g, f, b, new Rectangle(405, 219, 250, 68), dr["ENT_Name"].ToString(), 11, 15);//聘用企业
                        g.DrawString(Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy　　 MM　　dd"), f, b, new Rectangle(398, 355, 250, 34));//签发日期
                        lbl_PSN_RegisterCertificateNo.Text = dr["PSN_RegisterCertificateNo"].ToString();
                    }
                }
            }

            if (!System.IO.Directory.Exists(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", pathsub))))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", pathsub)));
            }
            imgSrc.Save(Server.MapPath(localPath), System.Drawing.Imaging.ImageFormat.Png);
        }

        //页面装载证书图片（多格）
        private void ShowCertificatePhoto(DataRow dr)
        {
            System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                img1.Width = System.Web.UI.WebControls.Unit.Percentage(98);
          
            divPhoto.Controls.Add(img1);
            img1.ImageUrl = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png"
                , dr["PSN_CertificateNO"].ToString().Substring(dr["PSN_CertificateNO"].ToString().Length - 3, 3)
                , dr["PSN_CertificateNO"].ToString());
           
            img1.BorderStyle = BorderStyle.Solid;
            img1.BorderColor = Color.Gray;
            img1.BorderWidth = new System.Web.UI.WebControls.Unit("0px");
            //img1.Style.Add("padding-left", "1%");
            //img1.Attributes.Add("onmousedown", "mousedown(this);");
            //img1.Attributes.Add("onmouseup", "mouseup(this);");  

        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/"));
        }

        //打印证书
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (printTable == null) return;
            if (printTable.Rows.Count > 0)
            {
                CheckSaveDirectory();
            }


            string CertificateType = "";//生成文件名称
            string Certificate = "";//模板

            CertificateType = string.Format("证书打印_{0}_{1}.doc", DateTime.Now.ToString("yyMMddHHmmss"), UserID);
            Certificate = "二级证书打印_TAB.doc";

                Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
            , string.Format("~/UpLoad/PrintCertificate/{0}",  CertificateType)
            , GetPrintData(1));
           

            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/PrintCertificate/{0}');", CertificateType, RootUrl), true);
            //打印
            CommonDAL.UpdatePrintTime(string.Format("'{0}'", printTable.Rows[CurrentIndex]["PSN_ServerID"]), UserName, DateTime.Now);
            BindPrintDetail(CurrentIndex + ShowCount);
        }

        //获取打印数据（按ListView分页导出Word）
        protected Dictionary<string, string> GetPrintData(int ItemCount)
        {
            Dictionary<string, string> printData = new Dictionary<string, string>();
            string[] labels = "Photo,Img_Photo".Split(',');//替换标签名称
            for (int i = 0; i < ItemCount; i++)
            {
                if ((CurrentIndex + i) > printList.Count - 1)//最后一页有空行
                {
                    for (int j = 0; j < labels.Length; j++)
                    {
                        printData.Add("ZGZSBH", "");//资格证号
                        printData.Add("PSN_RegisterNo", "");//注册编号
                        printData.Add("PSN_Name","");//姓名
                        printData.Add("PSN_Sex", "");//性别
                        printData.Add("PSN_BirthDate", "");//出生日期
                        printData.Add("PSN_RegisteProfession", "");//注册专业
                        printData.Add("ENT_Name","");//企业名称
                        printData.Add("PSN_CertificationDate", "");//签发日期                      
                    }
                }
                else
                {
                    DataRow dr = printTable.Rows[CurrentIndex + i];//证书实例

                    #region 新的
                    if (printTable.Rows[CurrentIndex + i]["ZGZSBH"].ToString().Length == 18)
                    {
                        printData.Add("Admin_Number", string.Format("管理号 {0}", dr["ZGZSBH"]));//管理号
                        printData.Add("Admin_Number_Lable", "Administration Number");
                        printData.Add("ZGZSBH", string.Format("{0}", ""));//资格证号
                    }
                    else
                    {
                        printData.Add("Admin_Number", string.Format("{0}", ""));//管理号
                        printData.Add("Admin_Number_Lable", "");
                        printData.Add("ZGZSBH", string.Format("{0}", dr["ZGZSBH"]));//资格证号
                    }
                    #endregion

                    //printData.Add("ZGZSBH", string.Format("{0}", dr["ZGZSBH"]));//资格证号
                    printData.Add("PSN_RegisterNo", string.Format("{0}", dr["PSN_RegisterNo"]));//注册编号
                    printData.Add("PSN_Name", string.Format("{0}", dr["PSN_Name"]));//姓名
                    printData.Add("PSN_Sex", string.Format("{0}", dr["PSN_Sex"]));//性别
                    printData.Add("PSN_BirthDate", string.Format("{0}", dr["PSN_BirthDate"] == null ? "" : Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年MM月dd日")));//出生日期
                    printData.Add("PSN_RegisteProfession", dr["PSN_RegisteProfession"].ToString());//注册专业
                    printData.Add("ENT_Name", dr["ENT_Name"].ToString());//企业名称

                    //签发日期
                    printData.Add("PSN_CertificationDate", dr["PSN_CertificationDate"] == null ? "" : Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy　　 MM　　dd"));
                 
                }
            }            

            return printData;
        }

        public string GetPhotoPath(DataRow dr)
        {
            BindPhoto(dr);

            //证书图片路径
            string newpath = string.Format("~/Upload/PhotoCertificate/{0}/{1}.png"
                ,  dr["PSN_CertificateNO"].ToString().Substring(dr["PSN_CertificateNO"].ToString().Length - 3, 3)
                , dr["PSN_CertificateNO"].ToString());
            return newpath;
        }

        //next
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            BindPrintDetail(CurrentIndex + ShowCount);
        }

        //Prev
        protected void ButtonPrev_Click(object sender, EventArgs e)
        {
            BindPrintDetail(CurrentIndex - ShowCount);
        }

        //批量分时打印
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ButtonPrint.Enabled == true) ButtonPrint_Click(sender, e);
        }

        //绑定带打印证书详细
        protected void BindPrintDetail(int index)
        {
            if (index > printList.Count - 1)//打印结束
            {
                divPhoto.Controls.Clear();
                divPhoto.Style.Add("background", "none");
                divPhoto.Style.Add("z-index", "9998");
                System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                divPhoto.Controls.Add(img1);
                img1.ImageUrl = "~/images/print.png";
                img1.BorderStyle = BorderStyle.None;
                ViewState["printList"] = new List<string>();
                ViewState["printTable"] = null;
                CurrentIndex = 0;
                CheckBoxAutoPrint.Checked = false;
                P_PrintTimeSpan.Visible = false;
                ButtonPrint.Visible = false;
                Timer1.Enabled = false;
                return;
            }
            CurrentIndex = index;
            DataRow dr = (ViewState["printTable"] as DataTable).Rows[index];

            int OutputCount = 1;//一次输出证书数量

            divPhoto.Controls.Clear();

            for (int i = 0; i < OutputCount; i++)
            {
                if (index + i > printList.Count - 1)
                {
                    return;
                }

                dr = (ViewState["printTable"] as DataTable).Rows[index + i];
                BindPhoto(dr);
                ShowCertificatePhoto(dr);
            }

            if (CheckBoxAutoPrint.Checked == true)//自动打印
            {
                if (Timer1.Enabled == false)
                {
                    Timer1.Interval = Convert.ToInt32(RadioButtonListPrintTimeSpan.SelectedValue) * 1000;
                    Timer1.Enabled = true;
                }
            }
            else
            {
                if (Timer1.Enabled == true)
                {
                    Timer1.Enabled = false;
                }
            }
        }

        //protected void BtnReturn2_Click(object sender, EventArgs e)
        //{

        //    string js = string.Format("<script>window.parent.location.href='CertPrintMgr.aspx?';</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
        //   // Response.Write("<script>window.parent.location.href='CertPrintMgr.aspx';</script>");//可行
        //    //Response.Write("<script language=javascript>opener.window.navigate('CertPrintMgr.aspx');</script>");
        //   // Response.Write("<script>opener.window.location.href = opener.window.location.href;</script>");
        //    //Response.Write("<script language=javascript>history.go(-1);location.reload();</script>");
        //    //Response.Write("<script language=javascript>self.location=document.referrer;</script>");
        //    //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        //    //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;hideIfam()", true);
        //}

    }
}