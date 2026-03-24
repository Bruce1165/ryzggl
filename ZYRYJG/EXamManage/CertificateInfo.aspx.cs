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

namespace ZYRYJG.EXamManage
{
    /// <summary>
    /// 证书打印（作废）
    /// </summary>
    public partial class CertificateInfo : BasePage
    {
        //岗位类型
        public string type
        {
            get { return ViewState["type"].ToString(); }
            set { ViewState["type"] = value; }
        }

        //证书ID
        public string id
        {
            get { return ViewState["id"].ToString(); }
            set { ViewState["id"] = value; }
        }

        string print = null;

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
                    LabelPage.Text = string.Format("第 {1} 条   共 {0} 条", printList.Count.ToString(), Convert.ToString(value + 1));

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

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertificatePrint.aspx";
            }
        }

        //绑定带打印证书详细
        protected void BindPrintDetail(int index)
        {
            if (index > printList.Count - 1)//打印结束
            {
                RadTextBoxCertificateCode.Text = "";
                LabelWorkerName.Text = "";
                LabelSex.Text = "";
                LabelUnitName.Text = "";
                LabelWorkerCertificateCode.Text = "";
                LabelBirthday.Text = "";
                LabelConferDate.Text = "";
                LabelCertificateCode.Text = "";
                LabelPostTypeID.Text = "";
                LabelPostID.Text = "";
                P_Remark.InnerText = "";
                LabelValidDate.Text = "";
                ButtonPrint.Enabled = false;
                HiddenFieldCertificateCode.Value = "";
                ViewState["printList"] = new List<string>();
                ViewState["printTable"] = null;
                CurrentIndex = 0;
                CheckBoxAutoPrint.Checked = false;
                P_PrintTimeSpan.Visible = false;
                Timer1.Enabled = false;
                return;
            }
            CurrentIndex = index;

            //初始化证书详细信息
            DataRow dr = (ViewState["printTable"] as DataTable).Rows[index];
            PostSelect1.PostTypeID = dr["PostTypeID"].ToString();
            PostSelect1.PostID = dr["PostID"].ToString();

            type = dr["PostTypeID"].ToString();
            id = dr["CertificateID"].ToString();
            LabelWorkerName.Text = dr["WorkerName"].ToString();
            LabelSex.Text = dr["Sex"].ToString();
            LabelUnitName.Text = dr["UnitName"].ToString();
            LabelWorkerCertificateCode.Text = dr["WorkerCertificateCode"].ToString();
            LabelBirthday.Text = Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd");
            LabelConferDate.Text = Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy-MM-dd");
            LabelCertificateCode.Text = dr["CertificateCode"].ToString();
            RadTextBoxCertificateCode.Text = dr["CertificateCode"].ToString();
            HiddenFieldCertificateCode.Value = dr["CertificateCode"].ToString();
            LabelPostTypeID.Text = dr["PostTypeName"].ToString();
            P_Remark.InnerText = dr["Remark"].ToString();
            if (dr["PostTypeID"].ToString() == "3" && dr["AddItemName"] != null && dr["AddItemName"].ToString() != "")//造价员
            {
                LabelPostID.Text = dr["AddItemName"].ToString();
            }
            else
            {
                LabelPostID.Text = dr["PostName"].ToString();
            }
            //有效期至
            LabelValidDate.Text = dr.IsNull("ValidEndDate") == true || Convert.ToDateTime(dr["ValidEndDate"]).CompareTo(new DateTime(2030, 1, 1)) > 0 ? "" : Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd");

            p_tip.Visible = false;//未查到记录提示
            if (type == "2")//特种作业
            {
                p_tzzy.Visible = true;
            }
            else
            {
                p_tzzy.Visible = false;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            //RadTextBoxCertificateCode.Focus();//保持焦点
            ////if (ButtonPrint.Visible==true) ButtonPrint.Focus();
            //if (!this.IsPostBack)
            //{
            //    if (Session["printList"] != null)//批量打印
            //    {
            //        ViewState["printList"] = Session["printList"];
            //        Session.Remove("printList");

            //        ViewState["printTable"] = Session["printTable"];
            //        Session.Remove("printTable");
            //    }
            //    else//单个打印
            //    {
            //        ViewState["printList"] = new List<string> { Request.QueryString["CertificateID"] };
            //        DataTable dt = CertificateDAL.GetList(0, int.MaxValue - 1, string.Format(" and CertificateID={0}", Request.QueryString["CertificateID"]), "CertificateID");
            //        ViewState["printTable"] = dt;
            //    }
            //    ViewState["ChangeType"] = Request["rnt_t"];//变更类型（续期=continue，变更=manage）
            //    ViewState["PostTypeID"] = Request["rtn_o"];//岗位类型ID
            //    BindPrintDetail(0);
            //}
        }

        //打印证书
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            DataRow dr = (ViewState["printTable"] as DataTable).Rows[CurrentIndex];

            CheckSaveDirectory(type);
            string CertificateType = "";
            string Certificate = "";
            switch (type)
            {
                case "1":
                    Certificate = "三类人员证书打印.doc";
                    CertificateType = string.Format("三类人员证书打印_{0}.doc", dr["CertificateCode"].ToString());
                    break;
                case "2":
                    if (RadioButtonZB.Checked == true)//正本
                    {
                        Certificate = "特种作业证书正本打印.doc";
                        CertificateType = string.Format("特种作业证书正本打印_{0}.doc", dr["CertificateCode"].ToString());
                        print = "正本";
                    }
                    else
                    {
                        Certificate = "特种作业证书副本打印.doc";
                        CertificateType = string.Format("特种作业证书副本打印_{0}.doc", dr["CertificateCode"].ToString());
                        print = "副本";
                    }
                    break;
                case "3":
                    Certificate = "造价员证书打印.doc";
                    CertificateType = string.Format("造价员证书打印_{0}.doc", dr["CertificateCode"].ToString());
                    break;
                case "4":
                    if (dr["PostID"].ToString() == "158")//特殊：村镇工匠证书使用村镇建筑工匠模版
                    {
                        Certificate = "村镇建筑工匠.doc";
                        CertificateType = string.Format("职业技能证书打印_{0}.doc", dr["CertificateCode"].ToString());
                        break;
                    }
                    Certificate = "职业技能证书打印.doc";
                    CertificateType = string.Format("职业技能证书打印_{0}.doc", dr["CertificateCode"].ToString());
                    break;
                case "5":
                    if (dr["PostID"].ToString() == "55" || dr["PostID"].ToString() == "159")//特殊：拆迁员和物业项目负责人证书使用专业管理人员证书老版模版
                    {
                        Certificate = "专业管理人员.doc";
                        CertificateType = string.Format("专业管理人员_{0}.doc", dr["CertificateCode"].ToString());
                        break;
                    }
                    if (dr["PostName"].ToString().Length > 7)
                    {
                        Certificate = "专业管理人员新版证书小字.doc";
                        CertificateType = string.Format("专业管理人员_{0}.doc", dr["CertificateCode"].ToString());
                    }
                    else
                    {
                        Certificate = "专业管理人员新版证书.doc";
                        CertificateType = string.Format("专业管理人员_{0}.doc", dr["CertificateCode"].ToString());
                    }
                    break;
            }
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, string.Format("~/Template/{0}", Certificate)
                , string.Format("~/UpLoad/PrintCertificate/{0}/{1}", dr["PostTypeID"].ToString(), CertificateType)
                , GetPrintData());


            CertificateDAL.UpdatePrintTime(Convert.ToInt64(dr["CertificateID"]), PersonName, DateTime.Now,1);

            UIHelp.WriteOperateLog(PersonName, UserID, "打印证书", string.Format("证书编号：{0}。", dr["CertificateCode"].ToString()));

            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/PrintCertificate/{0}/{1}');", dr["PostTypeID"].ToString(), CertificateType, RootUrl), true);

            BindPrintDetail(CurrentIndex + 1);
        }

        //批量分时打印
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ButtonPrint.Enabled == true) ButtonPrint_Click(sender, e);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory(string PostType)
        {
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            DataRow dr = (ViewState["printTable"] as DataTable).Rows[CurrentIndex];//证书实例
            //ExamSignUpOB _ExamSignUpOB = null;// 报名            
            printData = new Dictionary<string, string>();
            switch (type)
            {
                case "1"://三类人员证书打印                    
                    printData.Add("Name", dr["WorkerName"].ToString());//姓名
                    printData.Add("Sex", dr["Sex"].ToString());//性别
                    //出生日期(历史数据不在“1920年”之后的不打印)
                    printData.Add("Birthday",
                        dr.IsNull("Birthday") == true
                        || Convert.ToDateTime(dr["Birthday"]).CompareTo(new DateTime(1930, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["Birthday"]).ToString("yyyy.MM.dd"));
                    printData.Add("WorkerCertificateCode", dr["WorkerCertificateCode"].ToString());//身份证号

                    printData.Add("UnitName", dr["UnitName"].ToString());//企业名称

                    if (string.IsNullOrEmpty(dr["SkillLevel"].ToString()) == false)
                    {
                        printData.Add("SkillLevel", dr["SkillLevel"].ToString());//技术职称
                    }
                    else
                    {
                        //_ExamSignUpOB = ExamSignUpDAL.GetObject(dr["WorkerCertificateCode, dr["ExamPlanID"].ToString());
                        //if (_ExamSignUpOB != null && string.IsNullOrEmpty(_ExamSignUpOB.SKILLLEVEL) == false)
                        //    printData.Add("SkillLevel", _ExamSignUpOB.SKILLLEVEL);//技术职称
                        //else
                        printData.Add("SkillLevel", "");//技术职称
                    }

                    printData.Add("ZhiWu", dr["PostName"].ToString());//职务（工种）
                    printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号

                    //发证日期(历史数据不在“1980年”之后的不打印)
                    if (dr.IsNull("ConferDate") == true || Convert.ToDateTime(dr["ConferDate"]).CompareTo(new DateTime(1980, 1, 1)) < 0)
                    {
                        printData.Add("ConferDateYear", "");//发证日期-年
                        printData.Add("ConferDateMonth", "");
                        printData.Add("ConferDateDay", "");
                    }
                    else
                    {

                        printData.Add("ConferDateYear", Convert.ToDateTime(dr["ConferDate"]).Year.ToString());//发证日期-年
                        printData.Add("ConferDateMonth", Convert.ToDateTime(dr["ConferDate"]).Month.ToString("00"));
                        printData.Add("ConferDateDay", Convert.ToDateTime(dr["ConferDate"]).Day.ToString("00"));
                    }

                    //有效期（历史数据不在2030年”之后的不打印）
                    printData.Add("ValidDate", dr.IsNull("ValidEndDate") == true || Convert.ToDateTime(dr["ValidEndDate"]).CompareTo(new DateTime(2030, 1, 1)) > 0 ? "" : Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd"));
                    break;
                case "2"://特种作业
                    printData.Add("Name", dr["WorkerName"].ToString());//名称
                    printData.Add("PostType", dr["PostName"].ToString());//工种
                    printData.Add("WorkerCertificateCode", dr["WorkerCertificateCode"].ToString());//证件号码
                    printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号
                    if (print == "正本")
                    {
                        //初次领证日期(历史数据不在“1980年”之后的不打印)
                        printData.Add("ConferDate", dr.IsNull("ConferDate") == true || Convert.ToDateTime(dr["ConferDate"]).CompareTo(new DateTime(1980, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd"));

                        //使用期自:按发证日期6年一变
                        //DateTime useStatDate = Convert.ToDateTime(dr["ConferDate"]).AddYears((Convert.ToDateTime(dr["ValidEndDate"]).Year - Convert.ToDateTime(dr["ConferDate"]).Year - 1) / 6 * 6);
                        printData.Add("ValidStartDate", "");
                        printData.Add("ValidEndDate", "");

                        ////使用期至
                        //if (useStatDate.Month < 7)//上半年
                        //    printData.Add("ValidEndDate", string.Format("{0}.06.30", useStatDate.AddYears(6).Year.ToString()));
                        //else//下半年
                        //    printData.Add("ValidEndDate", string.Format("{0}.12.31", useStatDate.AddYears(6).Year.ToString()));

                        //第一次复合时间:即有效期至
                        printData.Add("CheckDate", Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd"));
                    }
                    break;
                case "3"://造价员证书打印
                    printData.Add("Name", dr["WorkerName"].ToString());//姓名
                    printData.Add("UnitName", dr["UnitName"].ToString());//工作单位
                    printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号
                    printData.Add("ConferDateYear", Convert.ToDateTime(dr["ConferDate"]).Year.ToString());//发证日期
                    printData.Add("ConferDateMonth", Convert.ToDateTime(dr["ConferDate"]).Month.ToString("00"));
                    printData.Add("ConferDateDay", Convert.ToDateTime(dr["ConferDate"]).Day.ToString("00"));
                    printData.Add("ValidDate", Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd"));//有效期

                    if (dr.IsNull("AddItemName") == false && dr["AddItemName"].ToString() != "")//有增项
                        printData.Add("PostType", string.Format("{0}", dr["AddItemName"].ToString()));
                    else//无增项
                        printData.Add("PostType", string.Format("{0}", dr["PostName"].ToString()));
                    break;
                case "4"://职业技能证书打印
                    if (dr["PostID"].ToString() == "158")//特殊：村镇工匠证书使用专业管理人员证书模版
                    {
                        printData.Add("Name", dr["WorkerName"].ToString());//姓名
                        printData.Add("Sex", dr["Sex"].ToString());//性别

                        //出生日期(历史数据不在“1920年”之前的不打印)
                        printData.Add("Birthday", dr.IsNull("Birthday") == true
                            || Convert.ToDateTime(dr["Birthday"]).CompareTo(new DateTime(1920, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["Birthday"]).ToString("yyyy.MM.dd"));
                        printData.Add("WorkerCertificateCode", dr["WorkerCertificateCode"].ToString());//身份证号

                        printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号

                        //发证日期(历史数据不在“1980年”之前的不打印)
                        printData.Add("ConferDate", dr.IsNull("ConferDate") == true
                            || Convert.ToDateTime(dr["ConferDate"]).CompareTo(new DateTime(1980, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd"));

                        printData.Add("PostType", string.Format("{0}", dr["PostName"].ToString()));//工种

                        //村镇工匠证书不打印有效期至
                        printData.Add("Valid", "");//标签
                        printData.Add("ValidDate", "");//有效期至
                        break;
                    }
                    printData.Add("PostName", dr["PostName"].ToString());//岗位工种

                    if (dr.IsNull("SkillLevel") == false)
                    {
                        printData.Add("SkillLevel", dr["SkillLevel"].ToString());//技术职称
                    }
                    else
                    {
                        //_ExamSignUpOB = ExamSignUpDAL.GetObject(dr["WorkerCertificateCode, dr["ExamPlanID.Value);
                        //if (_ExamSignUpOB != null && string.IsNullOrEmpty(_ExamSignUpOB.SKILLLEVEL) == false)
                        //    printData.Add("SkillLevel", _ExamSignUpOB.SKILLLEVEL);//技术职称或等级
                        //else
                        printData.Add("SkillLevel", "");
                    }
                    printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号
                    printData.Add("Name", dr["WorkerName"].ToString());//姓名
                    printData.Add("WorkerCertificateCode", dr["WorkerCertificateCode"].ToString());//身份证号
                    WorkerOB work = WorkerDAL.GetObject(Convert.ToInt64(dr["WorkerID"]));
                    printData.Add("CulturalLevel", work == null ? "" : work.CulturalLevel);//文化程度
                    printData.Add("UnitName", "（发证单位盖章）");//编号单位
                    //发证日期(历史数据不在“1980年”之后的不打印)
                    printData.Add("ConferDate", dr.IsNull("ConferDate") == true
                        || Convert.ToDateTime(dr["ConferDate"]).CompareTo(new DateTime(1980, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd"));

                    //有效期（历史数据不在“1980年~2015年”之间的不打印）
                    printData.Add("ValidDate", "");//ob.ValidEndDate.HasValue == false || ob.ValidEndDate.Value.CompareTo(new DateTime(2015, 1, 1)) > 0 ? "" : ob.ValidEndDate.Value.ToString("yyyy.MM.dd"));
                    break;
                case "5"://专业管理人员
                    printData.Add("Name", dr["WorkerName"].ToString());//姓名
                    printData.Add("Sex", dr["Sex"].ToString());//性别

                    //出生日期(历史数据不在“1920年”之后的不打印)
                    printData.Add("Birthday", dr.IsNull("Birthday") == true
                        || Convert.ToDateTime(dr["Birthday"]).CompareTo(new DateTime(1920, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["Birthday"]).ToString("yyyy.MM.dd"));
                    printData.Add("WorkerCertificateCode", dr["WorkerCertificateCode"].ToString());//身份证号
                    printData.Add("UnitName", dr["UnitName"].ToString());//企业名称

                    printData.Add("CertificateCode",  dr["CertificateCode"].ToString());//证书编号

                    //发证日期(历史数据不在“1980年”之后的不打印)
                    printData.Add("ConferDate", dr.IsNull("ConferDate") == true
                        || Convert.ToDateTime(dr["ConferDate"]).CompareTo(new DateTime(1980, 1, 1)) < 0 ? "" : Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy.MM.dd"));

                    //拆迁员、物业项目负责人 需要打印有效期至，但不打印岗位工种
                    if (dr["PostID"].ToString() == "55" //拆迁员
                        || dr["PostID"].ToString() == "159")//物业项目负责人
                    {
                        printData.Add("PostType", "");//工种
                        printData.Add("ValidDate", dr.IsNull("ValidEndDate") == true ? "" : Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd"));//有效期至
                        printData.Add("Valid", "有效期至:");//标签
                    }
                    else
                    {
                        printData.Add("PostType", dr["PostName"].ToString());//工种
                        printData.Add("ValidDate", dr.IsNull("ValidEndDate") == true ? "" : Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy.MM.dd"));//有效期至
                    }
                   
                    printData.Add("PrintDate", DateTime.Now.ToString("yyyy.MM.dd"));//制证日期
                    break;
            }
            list.Add(printData);
            return list;
        }

        //next
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            BindPrintDetail(CurrentIndex + 1);
        }

        //Prev
        protected void ButtonPrev_Click(object sender, EventArgs e)
        {
            BindPrintDetail(CurrentIndex - 1);
        }

        //返回
        protected void ButtonReturn_Click(object sender, EventArgs e)
        {
            if (ViewState["ChangeType"].ToString() != "" && ViewState["PostTypeID"].ToString() != "")
                Response.Redirect(string.Format("CertificatePrint.aspx?o={0}&t={1}", ViewState["PostTypeID"].ToString(), ViewState["ChangeType"].ToString()));
            else
                Response.Redirect("CertificatePrint.aspx");
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {

            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            QueryParamOB q = new QueryParamOB();
            if (PostSelect1.PostID != "")
            {
                q.Add(string.Format("PostID={0}", PostSelect1.PostID));
            }
            else if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID={0}", PostSelect1.PostTypeID));
            }

            q.Add(string.Format("CertificateCode like '%{0}'", RadTextBoxCertificateCode.Text));
            DataTable dt = CertificateDAL.GetList(0, 1, q.ToWhereString(), "CertificateID");
            ViewState["printTable"] = dt;

            //CertificateOB ob = CertificateDAL.GetCertificateOBObject(PostSelect1.PostTypeID,PostSelect1.PostID,"%" + RadTextBoxCertificateCode.Text);
            if (dt == null || dt.Rows.Count == 0)
            {
                LabelWorkerName.Text = "";
                LabelSex.Text = "";
                LabelUnitName.Text = "";
                LabelWorkerCertificateCode.Text = "";
                LabelBirthday.Text = "";
                LabelConferDate.Text = "";
                LabelCertificateCode.Text = "";
                LabelPostTypeID.Text = "";
                LabelPostID.Text = "";
                P_Remark.InnerText = "";
                LabelValidDate.Text = "";

                ButtonPrint.Enabled = false;
                p_tip.Visible = true;//查询不到提示
                HiddenFieldCertificateCode.Value = "";
                p_tzzy.Visible = false;//特种作业正副本选择框
                return;
            }
            else
            {
                ButtonPrint.Enabled = true;
                p_tip.Visible = false;
                HiddenFieldCertificateCode.Value = RadTextBoxCertificateCode.Text;
                if (dt.Rows[0]["PostTypeID"].ToString() == "2")//特种作业
                {
                    p_tzzy.Visible = true;
                }
                else
                {
                    p_tzzy.Visible = false;
                }

            }

            ViewState["printList"] = new List<string> { dt.Rows[0]["CertificateID"].ToString() };

            CurrentIndex = 0;
            PostSelect1.PostTypeID = dt.Rows[0]["PostTypeID"].ToString();
            PostSelect1.PostID = dt.Rows[0]["PostID"].ToString();
            type = dt.Rows[0]["PostTypeID"].ToString();
            id = dt.Rows[0]["CertificateID"].ToString();
            LabelWorkerName.Text = dt.Rows[0]["WorkerName"].ToString();
            LabelSex.Text = dt.Rows[0]["Sex"].ToString();
            LabelUnitName.Text = dt.Rows[0]["UnitName"].ToString();
            LabelWorkerCertificateCode.Text = dt.Rows[0]["WorkerCertificateCode"].ToString();
            LabelBirthday.Text = Convert.ToDateTime(dt.Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
            LabelConferDate.Text = Convert.ToDateTime(dt.Rows[0]["ConferDate"]).ToString("yyyy-MM-dd");
            LabelCertificateCode.Text = dt.Rows[0]["CertificateCode"].ToString();
            LabelPostTypeID.Text = dt.Rows[0]["PostTypeName"].ToString();
            P_Remark.InnerText = dt.Rows[0]["Remark"].ToString();
            if (dt.Rows[0]["PostTypeID"].ToString() == "3" && dt.Rows[0]["AddItemName"] != null && dt.Rows[0]["AddItemName"].ToString() != "")//造价员
            {
                LabelPostID.Text = dt.Rows[0]["AddItemName"].ToString();
            }
            else
            {
                LabelPostID.Text = dt.Rows[0]["PostName"].ToString();
            }
            //有效期至
            LabelValidDate.Text = dt.Rows[0]["ValidEndDate"] == null || Convert.ToDateTime(dt.Rows[0]["ValidEndDate"]).CompareTo(new DateTime(2030, 1, 1)) > 0 ? "" : Convert.ToDateTime(dt.Rows[0]["ValidEndDate"]).ToString("yyyy.MM.dd");
        }
    }
}
