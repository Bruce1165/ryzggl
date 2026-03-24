using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.IO;
using Model;
using DataAccess;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignPrint : BasePage
    {
        protected bool isExcelExport = false;
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamSignList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ExamPlanID"] = Request["o"];
                ButtonSearch_Click(sender, e);
                if (PersonType == 2)//考生
                {
                    ButtonOutputExcel.Visible = false;
                }
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGridExamSignUp);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add("ExamPlanID=" + ViewState["ExamPlanID"].ToString());
            switch (PersonType)
            {
                case 2://考生
                    q.Add(string.Format("CertificateCode=(select CertificateCode from dbo.Worker where WorkerID={0})", PersonID.ToString()));
                    break;
                case 3://企业
                    q.Add(string.Format("UnitCode=(select UnitCode from dbo.Unitinfo where UnitID={0})", PersonID.ToString()));
                    break;
                case 4://培训点
                    q.Add(string.Format("TrainUnitID={0}", PersonID.ToString()));
                    break;
            }

            //申请批次号
            if (RadTxtSignUpCode.Text.Trim() != "")
            {
                q.Add(string.Format("SignUpCode ='{0}'", RadTxtSignUpCode.Text.Trim()));
            }

            // 单位名称
            if (RadTxtUnitName.Text != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            }
            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridExamSignUp.CurrentPageIndex = 0;
            RadGridExamSignUp.DataSourceID = ObjectDataSource1.ID;
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridExamSignUp_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridExamSignUp, "ExamSignUpID");
        }

        //根据证件号码显示照片
        protected string ShowFaceimage(string CertificateCode)
        {
            //Image img=RadGridExamSignUp.FindControl("Image1") as Image ;
            System.Random rm = new Random();
            string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), CertificateCode))); //绑定照片;
            return img;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/tup.gif";
            }
        }

        //导出报名列表Excel
        protected void ButtonOutputExcel_Click(object sender, EventArgs e)
        {
            if (RadGridExamSignUp.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            isExcelExport = true;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("TemplateColumn").Visible = false;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("RowNum").Visible = false;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("Status").Visible = false;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("Faceimage").Visible = false;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("PostTypeName").Visible = true;
            RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("PostName").Visible = true;

            RadGridExamSignUp.PageSize = RadGridExamSignUp.MasterTableView.VirtualItemCount;//
            RadGridExamSignUp.CurrentPageIndex = 0;
            RadGridExamSignUp.Rebind();
            RadGridExamSignUp.ExportSettings.ExportOnlyData = true;
            RadGridExamSignUp.ExportSettings.OpenInNewWindow = true;
            RadGridExamSignUp.MasterTableView.ExportToExcel();
            RadGridExamSignUp.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }

        //格式化Excel
        protected void RadGridExamSignUp_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "UnitCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "RowNum")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");


        }

        //批量导出报名表
        protected void ButtonOutputWord_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
            if (!IsGridSelected(RadGridExamSignUp))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            CheckSaveDirectory();
            string fileID = string.Format("{0}_{1}", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc"
            , string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", fileID)
            , GetPrintData());
            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("报名表", string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", fileID)));


            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", PersonID.ToString())));

        }

        //批量打印报名表
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
            if (!IsGridSelected(RadGridExamSignUp))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc"
                , string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", PersonID.ToString())
                , GetPrintData());

            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/SignUpTable/报名表_{0}.doc');", PersonID.ToString(), RootUrl), true);
        }

        //检查临时目录
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
        }

        //准备打印、导出数据
        protected List<Dictionary<string, string>> GetPrintData()
        {
            //xml换行
            string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamSignUp) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamSignUp) == true)//排除
                    filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamSignUp));
                else//包含
                    filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamSignUp));
            }


            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            //ExamPlanOB explan = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"].ToString()));
            //PostInfoOB postInfo = PostInfoDAL.GetObject(Convert.ToInt32(explan.PostID));
            string TCodePath = "";//条形码
            DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, filterString, "ExamSignUpID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();
                list.Add(printData);

                printData.Add("Sex", dt.Rows[i]["Sex"].ToString());//性别
                printData.Add("Age", dt.Rows[i]["Birthday"] == DBNull.Value ? "" : Convert.ToString(DateTime.Now.Year - Convert.ToDateTime(dt.Rows[i]["Birthday"]).Year));//年龄
                printData.Add("CulturalLevel", dt.Rows[i]["CulturalLevel"].ToString());//文化程度
                printData.Add("Phone", dt.Rows[i]["Phone"].ToString());  //联系电话     
                printData.Add("SignUpCode", dt.Rows[i]["SignUpCode"].ToString());//报名批号
                printData.Add("SignUpDate", Convert.ToDateTime(dt.Rows[i]["SignUpDate"]).ToString("yyyy-MM-dd"));//报名时间
                printData.Add("CertificateCode", dt.Rows[i]["CertificateCode"].ToString());//证件编号
                printData.Add("WorkerName", dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("UnitCode", dt.Rows[i]["UnitCode"].ToString());//组织代码
                printData.Add("UnitName", dt.Rows[i]["UnitName"].ToString());//企业名称
                printData.Add("PostID",dt.Rows[i]["PostName"].ToString());//工种
                printData.Add("WorkStartDate", dt.Rows[i]["WorkStartDate"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[i]["WorkStartDate"]).ToString("yyyy年MM月dd日"));//工作开始时间
                printData.Add("PersonDetail", (dt.Rows[i]["PersonDetail"].ToString().Length > 180 ? dt.Rows[i]["PersonDetail"].ToString().Substring(0, 180) : dt.Rows[i]["PersonDetail"].ToString()));//工作简历
                printData.Add("HireUnitAdvise", dt.Rows[i]["HireUnitAdvise"].ToString());//聘用单位意见
                printData.Add("AdminUnitAdvise", dt.Rows[i]["AdminUnitAdvise"].ToString());//考核发证单位意见
                printData.Add("SKILLLEVEL", dt.Rows[i]["SKILLLEVEL"].ToString());//技术等级
                printData.Add("ImageName", dt.Rows[i]["CertificateCode"].ToString());//照片名称
                printData.Add("Img_FacePhoto", GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));//照片url
               
                //初审点信息
                if (dt.Rows[i]["PostTypeID"].ToString() == "1"//三类人
                  || dt.Rows[i]["PostTypeID"].ToString() == "5")//专业技术员
                {
                    //printData.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}。请您 {3 }到现场进行审核。", dt.Rows[i]["SIGNUPMAN"], xmlBr, dt.Rows[i]["PlaceName"], Convert.ToDateTime(dt.Rows[i]["CheckDatePlan"]).ToString("yyyy年MM月dd日")));//备注
                    printData.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}。{1}{3}", dt.Rows[i]["SIGNUPMAN"], xmlBr, dt.Rows[i]["PlaceName"]
                        , dt.Rows[i]["Status"].ToString() == EnumManager.SignUpStatus.NewSignUp ? string.Format("请您 {0}到现场进行审核。{1}", Convert.ToDateTime(dt.Rows[i]["CHECKDATEPLAN"]).ToString("yyyy年MM月dd日"), (dt.Rows[i]["FIRSTCHECKTYPE"] != null && Convert.ToInt32(dt.Rows[i]["FIRSTCHECKTYPE"]) == -1 ? "由于您一年内上次未参加考试，本次须现场审核报考材料并出具上次考试缺考原因的证明材料。" : "")) : "您已通过系统审核，无需到现场审核考试材料。"
                        ));//备注
                }
                else
                {
                    printData.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}", dt.Rows[i]["SIGNUPMAN"], xmlBr, (dt.Rows[i]["TRAINUNITNAME"] != null && dt.Rows[i]["SIGNUPMAN"] != null && dt.Rows[i]["TRAINUNITNAME"].ToString() == dt.Rows[i]["SIGNUPMAN"].ToString()) ? dt.Rows[i]["SIGNUPMAN"].ToString() : "东、西部报名审核点"));//备注
                }

                TCodePath = string.Format(@"../Upload/SignUpTable/{0}/{1}.png", ViewState["ExamPlanID"], dt.Rows[i]["ExamSignUpID"]);
                if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
                {
                    UIHelp.CreateTCode(ViewState["ExamPlanID"], dt.Rows[i]["ExamSignUpID"]);
                }
                //条码
                printData.Add("ImageTCodeName", dt.Rows[i]["CertificateCode"].ToString());
                printData.Add("Img_TCode", TCodePath);
            }

            return list;
        }

        protected void RadGridExamSignUp_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
        }
    }
}