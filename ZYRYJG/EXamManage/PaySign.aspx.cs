using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;


namespace ZYRYJG.EXamManage
{
    public partial class PaySign : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //ButtonSearch_Click(sender, e);
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            //if (UIHelp.CheckSQLParam() == false)
            //{
            //    UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
            //    return;
            //}
            //ClearGridSelectedKeys(RadGridCheckSign);
            //ObjectDataSource1.SelectParameters.Clear();
            //QueryParamOB q = new QueryParamOB();

            ////状态           
            //if (RadioButtonListStatus.SelectedItem.Text == "未通知")
            //{
            //    q.Add(string.Format("Status ='{0}'", EnumManager.SignUpStatus.Checked));
            //    RadGridCheckSign.Columns.FindByUniqueName("PayNoticeCode").Display = false;
            //    ButtonNotice.Visible = true;
            //}
            //else//已通知
            //{
            //    q.Add(string.Format("(Status ='{0}' or Status ='{1}')", EnumManager.SignUpStatus.PayNoticed, EnumManager.SignUpStatus.PayConfirmed));
            //    RadGridCheckSign.Columns.FindByUniqueName("PayNoticeCode").Display = true;
            //    ButtonNotice.Visible = false;
            //}

            ////考试名称
            //string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            //if (_ExamPlanID != "0")
            //{
            //    q.Add("ExamPlanID=" + _ExamPlanID);

            //}
            ////培训点
            //if (RadTextBoxTrainUnitID.Text != "")
            //{
            //    q.Add(string.Format("TRAINUNITNAME like '%{0}%'", RadTextBoxTrainUnitID.Text.Trim()));
            //}
            //// 单位名称
            //if (RadTxtUnitName.Text != "")
            //{
            //    q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            //}
            ////姓名
            //if (RadTxtWorkerName.Text != "")
            //{
            //    q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            //}
            ////证件号码
            //if (RadTxtCertificateCode.Text != "")
            //{
            //    q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            //}
            ////报名批号
            //if (RadTxtSignUpCode.Text.Trim() != "")
            //{
            //    q.Add(string.Format("SignUpCode ='{0}'", RadTxtSignUpCode.Text.Trim()));
            //}
            ////审批批次号
            //if (RadTextBoxCheckCode.Text != "")
            //{
            //    q.Add(string.Format("CheckCode like '%{0}%'", RadTextBoxCheckCode.Text.Trim()));
            //}
            //ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //RadGridCheckSign.CurrentPageIndex = 0;
            //RadGridCheckSign.DataSourceID = ObjectDataSource1.ID;
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            //UpdateGridSelectedKeys(RadGridCheckSign, "ExamSignUpID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            //UpdateGriSelectedStatus(RadGridCheckSign, "ExamSignUpID");
        }

        //打印缴费通知书
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            //DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "ExamSignUpID");
            //string PaySignCode = dt.Rows[0]["PayNoticeCode"].ToString();//报名批号
            //string ExamPlanID = dt.Rows[0]["ExamPlanID"].ToString();//考试计划
            //CheckSaveDirectory(ExamPlanID);
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/缴费通知单.doc"
            //    , string.Format("~/UpLoad/PaySign/{0}/缴费通知单_{1}.doc", ExamPlanID, PaySignCode)
            //    , GetPrintData());
            //ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/PaySign/{0}/缴费通知单_{1}.doc');", ExamPlanID, PaySignCode, RootUrl), true);
        }

        //导出缴费通知书
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "ExamSignUpID");
            //string PaySignCode = dt.Rows[0]["SignUpCode"].ToString();//报名批号
            //string ExamPlanID = dt.Rows[0]["ExamPlanID"].ToString();//考试计划
            //CheckSaveDirectory(ExamPlanID);
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/缴费通知单.doc"
            //        , string.Format("~/UpLoad/PaySign/{0}/缴费通知单_{1}.doc", ExamPlanID, PaySignCode)
            //        , GetPrintData());
            ////Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/PaySign/{0}/缴费通知单_{1}.doc", ExamPlanID, PaySignCode)));

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl("缴费通知单", string.Format("~/UpLoad/PaySign/{0}/缴费通知单_{1}.doc", ExamPlanID, PaySignCode)));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory(string ExamPlanID)
        {
            ////照片阵列存放路径(~/UpLoad/ExamerPhotoList/考试计划ID/照片阵列_考场编号.doc)
            //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PaySign/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PaySign/"));
            //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PaySign/" + ExamPlanID))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PaySign/" + ExamPlanID));
        }

        ////获取打印数据（按ListView分页导出Word）
        //protected List<Dictionary<string, string>> GetPrintData()
        //{
        //    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        //    Dictionary<string, string> printData = null;
        //    int pageNo = 0;//第几页
        //    //int cellNo = 0;//当前页第几单元格
        //    DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "ExamSignUpID");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {                
        //        //cellNo = i % 30 + 1;
        //        if (i % 30 == 0)
        //        {
        //            Int64 ExamPlanID = Convert.ToInt64(dt.Rows[i]["ExamPlanID"]);
        //            ExamPlanOB epob = ExamPlanDAL.GetObject(ExamPlanID);

        //            printData = new Dictionary<string, string>();
        //            list.Add(printData);
        //            pageNo++;
        //            string ExamFee = " ";
        //            int Amount = 0;

        //            DataTable dtSubject = ExamPlanSubjectDAL.GetList(ExamPlanID);
        //            for (int j = 0; j < dtSubject.Rows.Count; j++)
        //            {
        //                PostInfoOB ob = PostInfoDAL.GetObject((int)dtSubject.Rows[j]["PostID"]);
        //                ExamFee = ExamFee + ob.PostName + "，";                        
        //            }
        //            Amount = Convert.ToInt32(epob.ExamFee) * dt.Rows.Count;

        //            printData.Add("PayNoticeCode", dt.Rows[i]["PayNoticeCode"].ToString());
        //            printData.Add("PersonCount", dt.Rows.Count.ToString());
        //            printData.Add("ExamFee", ExamFee + string.Format("收费 {0} 元", epob.ExamFee.ToString()));
        //            printData.Add("Amount", Amount.ToString());
        //            printData.Add("ExamPlanName", epob.ExamPlanName);

        //            //缴费人
        //            if (dt.Rows[i]["TRAINUNITNAME"] != DBNull.Value)
        //            {
        //                printData.Add("WorkerName", dt.Rows[i]["TRAINUNITNAME"].ToString());
        //            }
        //            else
        //            {
        //                printData.Add("WorkerName", "");
        //            }

        //            //UserOB _UserOB = UserDAL.GetObject((Int64)dt.Rows[i]["TrainUnitID"]);
        //            //if (_UserOB != null)
        //            //{
        //            //    printData.Add("WorkerName", _UserOB.RelUserName);//缴费人
        //            //}
        //            //else
        //            //{
        //            //    OrganizationOB _OrganizationOB = OrganizationDAL.GetObject((Int64)dt.Rows[i]["TrainUnitID"]);
        //            //    if (_OrganizationOB != null)
        //            //    {
        //            //        printData.Add("WorkerName", _OrganizationOB.OrganName);
        //            //    }
        //            //    else
        //            //        printData.Add("WorkerName", "");
        //            //}

        //            printData.Add("LatestPayDate", epob.LatestPayDate.HasValue ? epob.LatestPayDate.Value.ToString("yyyy-MM-dd") : "");

        //        }

        //        printData.Add(string.Format("CertificateCode{0}", (i % 30).ToString()), dt.Rows[i]["CertificateCode"].ToString());//证件号
        //        printData.Add(string.Format("Name{0}", (i % 30).ToString()), dt.Rows[i]["WorkerName"].ToString());//姓名
        //        printData.Add(string.Format("tr{0}", (i % 30).ToString()), dt.Rows[i]["RowNum"].ToString());//序号
        //        printData.Add(string.Format("UnitName{0}", (i % 30).ToString()), dt.Rows[i]["UnitName"].ToString());

        //    }
        //    if (dt.Rows.Count % 30 != 0)//最后一页有空行
        //    {
        //        Utility.WordDelHelp.ReplaceLabelOfNullRow(list[list.Count - 1], "tr,Name,CertificateCode,UnitName", dt.Rows.Count % 30, 29);
        //    }
        //    return list;
        //}

        //缴费通知
        protected void ButtonNotice_Click(object sender, EventArgs e)
        {
            //UpdateGridSelectedKeys(RadGridCheckSign, "ExamSignUpID");
            //if (!IsGridSelected(RadGridCheckSign))
            //{
            //    UIHelp.layerAlert(Page, "你还没有选择数据！");
            //    return;
            //}
            ////检查初审单位（代缴费人：培训点or报名点or注册中心）是否唯一
            //switch (ValidSameSignTrainUnit())
            //{
            //    case -1:
            //        UIHelp.layerAlert(Page, "不是在同一个培训点或报名点报名的不能一起出缴费通知单，请按培训（报名）点重新查询。");
            //        return;
            //    case -2:
            //        UIHelp.layerAlert(Page, "不是在同一个考试计划的不能一起出缴费通知单，请按考试计划重新查询。");
            //        return;
            //}

            ////已经发放准考证的考试计划不能再审核？？？？？

            //ViewState["certifIDList"] = new List<string>();//初始化缴费通知证件号码ID集合
            //DateTime _ModifyTime = DateTime.Now;
            //string filterString = "";//过滤条件

            //if (GetGridIfCheckAll(RadGridCheckSign) == true)//全选
            //{
            //    filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            //}
            //else
            //{
            //    if (GetGridIfSelectedExclude(RadGridCheckSign) == true)//排除
            //        filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCheckSign));
            //    else//包含
            //        filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCheckSign));
            //}

            ////要通知的报名ID集合
            //DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.VIEW_EXAMSIGNUP_NEW", "ExamSignUpID", filterString, "ExamSignUpID");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    AddcertifIDToList(dt.Rows[i]["ExamSignUpID"].ToString());
            //}

            //ExamSignUpOB esob = new ExamSignUpOB();
            //esob.PayNoticeDate = DateTime.Now;
            //esob.PayNoticeMan = PersonName;
            //esob.PayNoticeResult = "通过";
            //esob.Status = EnumManager.SignUpStatus.PayNoticed;
            //esob.ModifyPersonID = PersonID;
            //esob.ModifyTime = _ModifyTime;

            //DBHelper db = new DBHelper();
            //DbTransaction tran = db.BeginTransaction();
            //try
            //{
            //    esob.PayNoticeCode = UIHelp.GetNextBatchNumber(tran, "JFTZ");//缴费通知编号 
            //    ExamSignUpDAL.PayNotice(tran, esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
            //    tran.Commit();
            //    UIHelp.WriteOperateLog(PersonName, UserID, "报名缴费通知", string.Format("通知批号：{0}，通知人数{1}人。", esob.PayNoticeCode, dt.Rows.Count.ToString()));  
            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    UIHelp.WriteErrorLog(Page, "报名缴费通知失败！", ex);
            //    return;
            //}

            //UIHelp.layerAlert(Page, string.Format("报名缴费通知成功！通知批号：{0}，通知人数{1}人。", esob.PayNoticeCode, dt.Rows.Count.ToString()));
            //ClearGridSelectedKeys(RadGridCheckSign);
            //ApplyAfter();
        }
        
        ////判断是否可以出统一通知单（规则1：同一考试计划，规则2：同一报名点或培训点报名数据（即初审人唯一）。
        ////返回值：0=正确；-1=报名点不唯一；-2=考试计划不唯一。
        //protected int ValidSameSignTrainUnit()
        //{
        //    string filterString = "";//过滤条件

        //    if (GetGridIfCheckAll(RadGridCheckSign) == true)//全选
        //    {
        //        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
        //    }
        //    else
        //    {
        //        if (GetGridIfSelectedExclude(RadGridCheckSign) == true)//排除
        //            filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCheckSign));
        //        else//包含
        //            filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCheckSign));
        //    }

        //    DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, filterString, "ExamSignUpID");
        //    if (dt.Rows.Count == 1) return 0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (i != dt.Rows.Count - 1)
        //        {
        //            if (dt.Rows[i]["TrainUnitID"].ToString() != dt.Rows[i + 1]["TrainUnitID"].ToString())
        //            {
        //                return -1; ;
        //            }
        //            if (dt.Rows[i]["ExamPlanID"].ToString() != dt.Rows[i + 1]["ExamPlanID"].ToString())
        //            {
        //                return -2; ;
        //            }
        //        }
        //    }

        //    return 0;
        //}

        ////缴费通知证件号码ID集合
        //public List<string> certifIDList
        //{
        //    get
        //    {
        //        if (ViewState["certifIDList"] == null)
        //            return new List<string>();
        //        else
        //            return ViewState["certifIDList"] as List<string>;
        //    }
        //}

        ////向List<string>添加一条数据
        //public void AddcertifIDToList(string certifID)
        //{
        //    List<string> _certifIDList = certifIDList;
        //    if (_certifIDList.Contains(certifID) == false) _certifIDList.Add(certifID);
        //    ViewState["certifIDList"] = _certifIDList;
        //}

        ////缴费通知成功后出通知单
        //protected void ApplyAfter()
        //{
        //    DivSearch.Visible = false;
        //    RadWindowManager1.OnClientClose = "";
        //    ButtonNotice.Visible = false;
        //    ButtonPrint.Visible = true;
        //    ButtonExport.Visible = true;
        //    ButtonReturn.Visible = true;
        //    ObjectDataSource1.SelectParameters.Clear();
        //    QueryParamOB q = new QueryParamOB();
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    foreach (string s in certifIDList)
        //    {
        //        sb.Append(",").Append(s);
        //    }
        //    if (sb.Length > 0) sb.Remove(0, 1);
        //    q.Add(string.Format("ExamSignUpID in({0})", sb.ToString()));
        //    GridColumn column = RadGridCheckSign.MasterTableView.GetColumn("SelectAllColumn");
        //    column.Display = false;
        //    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
        //    RadGridCheckSign.CurrentPageIndex = 0;
        //    RadGridCheckSign.DataSourceID = ObjectDataSource1.ID;
        //}

        ////返回
        //protected void ButtonReturn_Click(object sender, EventArgs e)
        //{
        //    //DivSearch.Visible = true;
        //    //RadWindowManager1.OnClientClose = "OnClientClose";
        //    //ButtonNotice.Visible = true;
        //    //ButtonPrint.Visible = false;
        //    //ButtonExport.Visible = false;
        //    //ButtonReturn.Visible = false;
        //    //GridColumn column = RadGridCheckSign.MasterTableView.GetColumn("SelectAllColumn");
        //    //column.Display = true;

        //    //ObjectDataSource1.SelectParameters.Clear();
        //    //ButtonSearch_Click(sender, e);
        //}

        protected void RadGridCheckSign_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == "PayNoticeCode")
            //{
            //    DivSearch.Visible = false;
            //    RadWindowManager1.OnClientClose = "";
            //    ButtonNotice.Visible = false;
            //    ButtonPrint.Visible = true;
            //    ButtonExport.Visible = true;
            //    ButtonReturn.Visible = true;
            //    GridColumn column = RadGridCheckSign.MasterTableView.GetColumn("SelectAllColumn");
            //    column.Display = false;
            //    ObjectDataSource1.SelectParameters.Clear();
            //    QueryParamOB q = new QueryParamOB();
            //    q.Add(string.Format("PayNoticeCode ='{0}'", RadGridCheckSign.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PayNoticeCode"].ToString()));

            //    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //    RadGridCheckSign.CurrentPageIndex = 0;
            //    RadGridCheckSign.DataSourceID = ObjectDataSource1.ID;
 
            //}
        }
    }
}
