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
    //打印防伪条（作废）
    public partial class CertCodeBarPrint : BasePage
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
//                if (Session["printList"] != null)//批量打印
//                {
//                    ViewState["printList"] = Session["printList"];
//                    Session.Remove("printList");
//                    ViewState["printTable"] = Session["printTable"];
//                    Session.Remove("printTable");

//                    ShowCount = 12;
//                }
//                else//单个打印
//                {
//                    ViewState["printList"] = new List<string> { Request["o"] };
//                     string sql = @"
//                     SELECT *,
//		             PSN_RenewalProfession2= STUFF((select  '、'+ convert(varchar(20),PRO_Profession) FROM COC_TOW_Register_Profession
//		             WHERE PSN_ServerID =COC_TOW_Person_BaseInfo.PSN_ServerID
//		             and [PRO_ValidityEnd]=COC_TOW_Person_BaseInfo.PSN_CertificateValidity for xml path(''))
//		             ,1,1,'')
//	                 FROM dbo.COC_TOW_Person_BaseInfo
//		             where 1=1 and PSN_ServerID in ('{0}')";
//                    //DataTable dt = COC_TOW_Person_BaseInfoDAL.GetList(0, 1, string.Format(" and PSN_ServerID='{0}'" , Request["o"]), "PSN_ServerID");
//                    DataTable dt = dt = CommonDAL.GetDataTable(string.Format(sql, Request["o"]));
//                    ViewState["printTable"] = dt;
//                    ShowCount = 1 ;
//                }

//                if (printTable != null)
//                {
//                    BindPrintDetail(0);
//                }
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
        /// 一次打印变更贴PSN_ServerID集合
        /// </summary>
        protected string PrintList_PSN_ServerID;


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

            string pathsub = dr["PSN_RegisterNO"].ToString().Substring(dr["PSN_RegisterNO"].ToString().Length - 3, 3);
            //证书图片路径
            string localPath = string.Format(@"~/Upload/PhotoCertificate/{0}/bar_{1}.png", pathsub, dr["PSN_RegisterNO"]);

            if (!Directory.Exists(Page.Server.MapPath( string.Format("~/UpLoad/PhotoCertificate/{0}/" , pathsub))))
            {
                Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/PhotoCertificate/{0}/", pathsub)));
            }

            string path = "";//证书照片背景图路径
            System.Drawing.Image imgSrc = null;//证书照片背景图

            //证书背景模板
            path = Server.MapPath(@"~/Images/certbg2.png");
            imgSrc = System.Drawing.Image.FromFile(path);
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                ////输出二维码  
                //string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"].ToString(), dr["CertificateCode"].ToString()));
                //g.DrawImage(Utility.ImageHelp.CreateQRCode(string.Format("http://210.75.213.233/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 86, 86), new Rectangle(5, 190, 86, 86), 0, 0, 86, 86, GraphicsUnit.Pixel);

                //输出证书信息
                using (Font f = new Font("宋体", 10, FontStyle.Regular))
                {
                    using (Brush b = new SolidBrush(Color.Black))
                    {
                        int beginY = 20;
                        switch(dr["PSN_RegisteType"].ToString())
                        {
                            case "02"://变更
                                if (dr["PSN_BeforENT_Name"] != DBNull.Value && dr["PSN_BeforENT_Name"].ToString() != "" && dr["ENT_Name"].ToString() != dr["PSN_BeforENT_Name"].ToString())
                                {
                                    g.DrawString("聘用企业：", f, b, new Rectangle(115, beginY, 140, 34));//变更单位名称或变更单位
                                    beginY += 20;
                                    DrawString(g, f, b, new Rectangle(115, beginY, 140, 34), dr["ENT_Name"].ToString(), 8, 5);//聘用企业
                                    beginY += 20;
                                }
                                if (dr["PSN_BeforPersonName"] != DBNull.Value && dr["PSN_BeforPersonName"].ToString() != "" && dr["PSN_Name"].ToString() != dr["PSN_BeforPersonName"].ToString())
                                {
                                    g.DrawString("姓名：" + dr["PSN_Name"].ToString(), f, b, new Rectangle(115, beginY, 150, 34));//变更姓名
                                }
                                g.DrawString(Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy　 MM　 dd"), f, b, new Rectangle(135, 110, 150, 34));//审批日期
                                g.DrawString(dr["PSN_RegisterNo"].ToString(), f, b, new Rectangle(8, 65, 150, 34));//注册编号
                                break;
                            case "03"://延期
                                if (dr["PSN_RenewalProfession"] != DBNull.Value && dr["PSN_RenewalProfession"].ToString() != "")
                                {
                                    g.DrawString(dr["PSN_RenewalProfession2"].ToString(), f, b, new Rectangle(135, 30, 150, 34));//延续专业
                                }
                                g.DrawString(Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy　 MM　 dd"), f, b, new Rectangle(140, 105, 150, 34));//审批日期
                                g.DrawString(dr["PSN_RegisterNo"].ToString(), f, b, new Rectangle(15, 65, 150, 34));//注册编号
                                break;
                            case "04"://增项
                                if (dr["PSN_AddProfession"] != DBNull.Value && dr["PSN_AddProfession"].ToString() != "")
                                {
                                    g.DrawString(dr["PSN_AddProfession"].ToString(), f, b, new Rectangle(115, 30, 150, 34));//增项专业
                                }
                                g.DrawString(Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy　 MM　 dd"), f, b, new Rectangle(140, 105, 150, 34));//审批日期
                                g.DrawString(dr["PSN_RegisterNo"].ToString(), f, b, new Rectangle(8, 65, 150, 34));//注册编号
                                break;
                        }
                        //if (printTable.Rows[0]["PSN_Level"].ToString() == "二级")//输出资格证书编号
                        //{
                        //    //g.DrawString(dr["资格证号"].ToString(), f, b, new Rectangle(110, 230, 250, 43));//资格证书编号
                        //}

                        //g.DrawString(dr["PSN_RegisterNo"].ToString(), f, b, new Rectangle(110, 305, 250, 34));//注册编号
                        //g.DrawString( dr["PSN_Name"].ToString(), f, b, new Rectangle(405, 50, 250, 34));//姓名
                        //g.DrawString(dr["PSN_Sex"].ToString(), f, b, new Rectangle(405, 84, 250, 34));//性别
                        //g.DrawString(Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年MM月dd日"), f, b, new Rectangle(405, 120, 250, 34));//出生日期
                        //g.DrawString(dr["PSN_RegisteProfession"].ToString(), f, b, new Rectangle(405, 159, 250, 34));//专业类别
                        ////g.DrawString(dr["ENT_Name"].ToString(), f, b, new Rectangle(445, 294, 250, 68));//聘用企业
                        //DrawString(g, f, b, new Rectangle(405, 219, 250, 68), dr["ENT_Name"].ToString(), 11, 15);//聘用企业
                       
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
            divPhoto.Controls.Add(img1);
            img1.Style.Add("position", "absolute");
                //img1.Width = System.Web.UI.WebControls.Unit.Percentage(46);
          
          
            img1.ImageUrl =UIHelp.ShowFile( string.Format(@"~/Upload/PhotoCertificate/{0}/bar_{1}.png"
                , dr["PSN_RegisterNO"].ToString().Substring(dr["PSN_RegisterNO"].ToString().Length - 3, 3)
                , dr["PSN_RegisterNO"].ToString()));
           
            img1.BorderStyle = BorderStyle.Solid;
            img1.BorderColor = Color.Red;
            img1.BorderWidth = new System.Web.UI.WebControls.Unit("0px");
          

            int printNo = Convert.ToInt32(RadComboBoxBeginNo.SelectedValue);//打印位置（1-12块）
            int row = (printNo +1) / 2 ;//打印输出所在行(1 -6 )
            int coloumn = printNo % 2;//打印输出模板列(1 - 2)
            if (coloumn ==0)
            {
                img1.Style.Add("left", "370px");
            }
            else
            {
                img1.Style.Add("left", "80px");
            }
            //img1.Style.Add("left", "70px");

            img1.Style.Add("top", Convert.ToString((row - 1) * 140 + 55 + (row - 1)*9) + "px");            

        }

        //页面装载证书图片（多格）
        private void ShowCertificatePhoto(DataRow dr, int printNo)
        {
            System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
            divPhoto.Controls.Add(img1);
            img1.Style.Add("position", "absolute");
            //img1.Width = System.Web.UI.WebControls.Unit.Percentage(46);


            img1.ImageUrl = UIHelp.ShowFile( string.Format(@"~/Upload/PhotoCertificate/{0}/bar_{1}.png"
                , dr["PSN_RegisterNO"].ToString().Substring(dr["PSN_RegisterNO"].ToString().Length - 3, 3)
                , dr["PSN_RegisterNO"].ToString()));

            img1.BorderStyle = BorderStyle.Solid;
            img1.BorderColor = Color.Red;
            img1.BorderWidth = new System.Web.UI.WebControls.Unit("0px");


            //int printNo = Convert.ToInt32(RadComboBoxBeginNo.SelectedValue);//打印位置（1-12块）
            int row = (printNo + 1) / 2;//打印输出所在行(1 -6 )
            int coloumn = printNo % 2;//打印输出模板列(1 - 2)
            if (coloumn == 0)
            {
                img1.Style.Add("left", "370px");
            }
            else
            {
                img1.Style.Add("left", "80px");
            }
            //img1.Style.Add("left", "70px");

            img1.Style.Add("top", Convert.ToString((row - 1) * 140 + 55 + (row - 1) * 9) + "px");

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

            CertificateType = string.Format("变更贴打印_{0}_{1}.doc", DateTime.Now.ToString("yyMMddHHmmss"), UserID);
            Certificate = "变更贴打印.doc";

                Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
            , string.Format("~/UpLoad/PrintCertificate/{0}",  CertificateType)
            , GetPrintData(Convert.ToInt32(RadComboBoxBeginNo.SelectedValue)));
           

            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/PrintCertificate/{0}');", CertificateType, RootUrl), true);
            //打印
            CommonDAL.UpdatePrintTime(PrintList_PSN_ServerID, UserName, DateTime.Now);
           
            BindPrintDetail(CurrentIndex + ShowCount);

        }

        //获取打印数据（按ListView分页导出Word）
        protected Dictionary<string, string> GetPrintData(int startno)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Dictionary<string, string> printData = new Dictionary<string, string>();
            string[] labels = "Photo,Img_Photo".Split(',');//替换标签名称
            for (int i = 1; i < 13; i++)
            {
                if (i < startno //起始位置前
                    || (i - startno ) > printTable.Rows.Count -1 )//最后一页有空行
                {
                    for (int j = 0; j < labels.Length; j++)
                    {
                        if (labels[j].IndexOf("Img_") != -1)//照片
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
                        else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
                        else
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
                    }
                }
                else
                {
                    printData.Add(string.Format("Photo{0}", Convert.ToString(i)), printTable.Rows[CurrentIndex + i - startno]["PSN_ServerID"].ToString());//照片url
                    printData.Add(string.Format("Img_Photo{0}", Convert.ToString(i)), GetPhotoPath(printTable.Rows[CurrentIndex + i - startno]));//照片url 

                    sb.Append(string.Format(",'{0}'", printTable.Rows[CurrentIndex + i - startno]["PSN_ServerID"]));
                }
            }          
  
            if(sb.Length>0)
            {
                sb.Remove(0,1);//去掉第一个，
                PrintList_PSN_ServerID = sb.ToString();
            }

            return printData;
        }

        public string GetPhotoPath(DataRow dr)
        {
            BindPhoto(dr);

            //证书图片路径
            string newpath = string.Format("~/Upload/PhotoCertificate/{0}/bar_{1}.png"
                , dr["PSN_RegisterNO"].ToString().Substring(dr["PSN_RegisterNO"].ToString().Length - 3, 3)
                , dr["PSN_RegisterNO"].ToString());
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
            else
            {
                divPhoto.Style.Remove("background");
                switch((ViewState["printTable"] as DataTable).Rows[index]["PSN_RegisteType"].ToString())
                {
                    case "02"://变更注册
                        divPhoto.Style.Add("background", "url(../images/CodeBarBG.jpg)");
                        break;
                    case "03"://延期注册
                        divPhoto.Style.Add("background", "url(../images/CodeBarYX.jpg)");
                        break;
                    case "04"://增项注册
                        divPhoto.Style.Add("background", "url(../images/CodeBarZX.jpg)");
                        break;
                }
              
            }
            CurrentIndex = index;
            DataRow dr = (ViewState["printTable"] as DataTable).Rows[index];

            int OutputCount = 12;//一次输出证书数量

            divPhoto.Controls.Clear();

            int startNo = Convert.ToInt32(RadComboBoxBeginNo.SelectedValue);//开始输出位置

            for (int i = startNo -1; i < OutputCount; i++)
            {
                if (index + i - startNo +1 > printList.Count - 1)
                {
                    return;
                }

                dr = (ViewState["printTable"] as DataTable).Rows[index + i -(startNo -1)];
                BindPhoto(dr);
                ShowCertificatePhoto(dr,(i % 12 +1));
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

        protected void RadComboBoxBeginNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPrintDetail(0);
            //System.Web.UI.WebControls.Image img1 = divPhoto.Controls[0] as System.Web.UI.WebControls.Image;

            //int printNo = Convert.ToInt32(RadComboBoxBeginNo.SelectedValue);//打印位置（1-12块）
            //int row = printNo / 2 + 1;//打印输出所在行(1 -6 )
            //int coloumn = printNo % 2;//打印输出模板列(1 - 2)
            //if (coloumn == 0)
            //{
            //    img1.Style.Add("left", "350px");
            //}
            //else
            //{
            //    img1.Style.Add("left", "80px");
            //}

            //img1.Style.Add("top", Convert.ToString((row - 1) * 140 + 55) + "px");
           
        }

    }
}