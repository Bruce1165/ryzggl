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

namespace ZYRYJG.RenewCertifates
{
    public partial class CertifCheckUnit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("~/ResultInfoPage.aspx?o=尚未开通！");
            if (!IsPostBack)
            {               
                //ButtonSearch_Click(sender, e);
            }
        }

        ///// <summary>
        ///// 获取查询条件
        ///// </summary>
        ///// <returns>查询参数类</returns>
        //private QueryParamOB GetQueryParamOB()
        //{
        //    QueryParamOB q = new QueryParamOB();
        //    q.Add(string.Format("(NewUnitcode like '{0}%')", ZZJGDM)); //改为现单位确认

        //    //初审状态
        //    divQX.Visible = false;
        //    switch (RadioButtonListStatus.SelectedValue)
        //    {
        //        case "待单位确认"://未审核                  
        //            q.Add(string.Format("Status='{0}'", EnumManager.CertificateContinueStatus.WaitUnitCheck));
        //            divQX.Visible = true;
        //            break;
        //        case "退回修改"://审核不通过 
        //            q.Add(string.Format("Status='{0}' and NewUnitCheckTime>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack)); 
        //            break;
        //        case "已审查"://审核通过 
        //            q.Add(string.Format("Status<>'{0}' and NewUnitCheckTime>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
        //            break;
        //        case ""://全部
        //            q.Add(string.Format("Status<>'{0}'", EnumManager.CertificateContinueStatus.NewSave));
        //            break;
        //    }
        //    //证书人姓名
        //    if (txtWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", txtWorkerName.Text.Trim()));

        //    //证件号码
        //    if (txtWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", txtWorkerCertificateCode.Text.Trim()));

        //    //岗位类别
        //    if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));

        //    //工种类别
        //    if (PostSelect1.PostID != "")
        //    {
        //        switch (PostSelect1.PostID)
        //        {
        //            case "9"://土建
        //                q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
        //                break;
        //            case "12"://安装
        //                q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
        //                break;
        //            default:
        //                q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
        //                break;
        //        }
        //    }

        //    //证书编号
        //    if (txtCertificateCode.Text.Trim() != "") q.Add(string.Format("CERTIFICATECODE like '%{0}%'", txtCertificateCode.Text.Trim()));

        //    //证书有效期至
        //    if (!txtValidEndtDate.IsEmpty)
        //    {
        //        q.Add(string.Format("ValidEndDate ='{0}'", txtValidEndtDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
        //    }         

        //    //受理时间-起始
        //    if (!RadDatePicker_GetDateStart.IsEmpty) q.Add(string.Format("[NewUnitCheckTime]>='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
        //    //受理时间-截止
        //    if (!RadDatePicker_GetDateEnd.IsEmpty) q.Add(string.Format("[NewUnitCheckTime]<'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

        //    return q;
        //}

        ///// <summary>
        ///// 设置排序方式
        ///// </summary>
        ///// <returns>排序表达式</returns>
        //private string SetSortBy()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    RadGridAccept.MasterTableView.SortExpressions.Clear();
        //    RadGridAccept.MasterTableView.AllowMultiColumnSorting = true;

        //    //排序规则=初审时间，企业，岗位工种，证书编号
        //    GridSortExpression sortStr1 = new GridSortExpression();
        //    sortStr1.FieldName = "[ApplyDate]";
        //    sortStr1.SortOrder = GridSortOrder.Descending;
        //    RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
        //    sb.Append(",").Append("[ApplyDate]");
        
        //    GridSortExpression sortStr4 = new GridSortExpression();
        //    sortStr4.FieldName = "CERTIFICATECODE";
        //    sortStr4.SortOrder = GridSortOrder.Ascending;
        //    RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr4);
        //    sb.Append(",").Append("CERTIFICATECODE");                 

        //    if (sb.Length > 0) sb.Remove(0, 1);
        //    return sb.ToString();
        //}

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            //ClearGridSelectedKeys(RadGridAccept);
            //ObjectDataSource1.SelectParameters.Clear();
            //QueryParamOB q = GetQueryParamOB();
            //ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            //SetSortBy();          

            //RadGridAccept.CurrentPageIndex = 0;
            //RadGridAccept.DataSourceID = ObjectDataSource1.ID;
        }

        //////审核不通过
        ////protected void ButtonNoAccept_Click(object sender, EventArgs e)
        ////{
        ////    FirstCheck(EnumManager.CertificateContinueCheckResult.CheckNoPass);
        ////}

        //////审核通过
        ////protected void ButtonAccept_Click(object sender, EventArgs e)
        ////{
        ////    FirstCheck(EnumManager.CertificateContinueCheckResult.CheckPass);
        ////}

        //////审核
        ////protected void FirstCheck(string checkResult)
        ////{
        ////    UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        ////    if (IsGridSelected(RadGridAccept) == false)
        ////    {
        ////        UIHelp.layerAlert(Page, "你还没有选择数据！");
        ////        return;
        ////    }            

        ////    int rowCount = 0;//处理记录数量
        ////    string filterString = "";//过滤条件
        ////    string sqsl = "";//受理批次号

        ////    if (GetGridIfCheckAll(RadGridAccept) == true)//全选
        ////    {
        ////        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
        ////    }
        ////    else
        ////    {
        ////        if (GetGridIfSelectedExclude(RadGridAccept) == true)//排除
        ////            filterString = string.Format(" {0} and CertificateContinueID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
        ////        else//包含
        ////            filterString = string.Format(" {0} and CertificateContinueID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
        ////    }

        ////    rowCount = CertificateContinueDAL.SelectCount(filterString);
        ////    CertificateContinueOB ob = new CertificateContinueOB();
        ////    ob.CheckDate = DateTime.Now;//时间
        ////    ob.CheckResult = checkResult;//结果
        ////    ob.CheckMan = PersonName;//人
        ////    ob.Status = EnumManager.CertificateContinueStatus.Checked;//状态
            
        ////    ob.ModifyPersonID = PersonID;//修改人ID
        ////    ob.ModifyTime = DateTime.Now;//修改时间 

        ////    DBHelper db = new DBHelper();
        ////    DbTransaction tran = db.BeginTransaction();//事务对象

        ////    try
        ////    {
        ////        sqsl = UIHelp.GetNextBatchNumber(tran, "XQSH");//审核批次号
        ////        ob.CheckCode = sqsl;//批次号
        ////        CertificateContinueDAL.Check(tran, ob,string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})",filterString));
        ////        tran.Commit();//提交事务               
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        tran.Rollback();
        ////        UIHelp.WriteErrorLog(Page, "证书续期审核失败！", ex);
        ////        return;
        ////    }

        ////    UIHelp.WriteOperateLog(PersonName, UserID, "审核证书续期", string.Format("审核了{0}条记录！审核结果为“{1}”，审核批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));

        ////    UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));
        ////    ClearGridSelectedKeys(RadGridAccept);
        ////    ButtonSearch_Click(ButtonAccept, null);
        ////}

        ////Grid换页
        //protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        //}

        ////Grid绑定勾选checkbox状态
        //protected void RadGridAccept_DataBound(object sender, EventArgs e)
        //{
        //    UpdateGriSelectedStatus(RadGridAccept, "CertificateContinueID");
        //}

        //////根据证件号码显示照片
        ////protected string ShowFaceimage(string WorkerCertificateCode)
        ////{
        ////    System.Random rm = new Random();
        ////    string img = string.Format("../EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), GetFacePhotoPath(WorkerCertificateCode)); //绑定照片;
        ////    return img;
        ////}

        /////// <summary>
        /////// 获取个人照片地址
        /////// </summary>
        /////// <param name="CertificateCode">证件号码</param>
        /////// <returns></returns>
        ////public string GetFacePhotoPath(string WorkerCertificateCode)
        ////{
        ////    if (WorkerCertificateCode == "") return "../Images/photo_ry.jpg";
        ////    string path = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        ////    if (File.Exists(Server.MapPath(path)) == true)
        ////        return path;
        ////    else
        ////        return "../Images/photo_ry.jpg";
        ////}

        ////导出excel
        //protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        //{
        //    QueryParamOB q = GetQueryParamOB();
        //    string sortBy = SetSortBy();

        //    string saveFile = string.Format("~/UpLoad/CertifEnterApply/续期审核_{0}.xls", PersonID.ToString());//保存文件名
        //    try
        //    {
        //        //检查临时目录
        //        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

        //        //导出数据到数据库服务器
        //        //CertificateContinueDAL.OutputXlsFile(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), q.ToWhereString(), sortBy);

        //        string colHead = "";
        //        string colName =  "";
        //        if (PostSelect1.PostTypeID == "2")
        //        {
        //            colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\原单位名称\现单位名称";
        //            colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\NewUnitName";
        //        }
        //        else
        //        {
        //            colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称";
        //            colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName";
        //        }
        //        CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
        //            , "DBO.VIEW_CERTIFICATECONTINUE"
        //            , q.ToWhereString(), sortBy, colHead, colName);

        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "导出续期审核查询结果失败！", ex);
        //        return;
        //    }

        //    List<ResultUrl> url = new List<ResultUrl>();
        //    url.Add(new ResultUrl("续期审核查询结果下载", saveFile));
        //    UIHelp.ShowMsgAndRedirect(Page, "已经为您准备好了数据，单击下面链接进行下载：", url);
        //}

        ////企业批量受理
        //protected void BtnSave_Click(object sender, EventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        //    if (IsGridSelected(RadGridAccept) == false)
        //    {
        //        UIHelp.layerAlert(Page, "你还没有选择数据！");
        //        return;
        //    }

        //    int rowCount = 0;//处理记录数量
        //    string filterString = "";//过滤条件
           
        //    if (GetGridIfCheckAll(RadGridAccept) == true)//全选
        //    {
        //        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
        //    }
        //    else
        //    {
        //        if (GetGridIfSelectedExclude(RadGridAccept) == true)//排除
        //            filterString = string.Format(" {0} and CertificateContinueID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
        //        else//包含
        //            filterString = string.Format(" {0} and CertificateContinueID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
        //    }

        //    rowCount = CertificateContinueDAL.SelectCount(filterString);
        //    CertificateContinueOB ob = new CertificateContinueOB();
        //    ob.NewUnitCheckTime = DateTime.Now;
        //    ob.NewUnitAdvise = (RadioButtonListApplyStatus.SelectedValue == "同意" ? "提交建委审核" : TextBoxApplyGetResult.Text);//单位意见
        //    ob.Status = (RadioButtonListApplyStatus.SelectedValue == "同意" ? EnumManager.CertificateContinueStatus.Applyed : EnumManager.CertificateContinueStatus.SendBack);
        
           
        //    try
        //    {
        //        CertificateContinueDAL.CheckUnit( ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));
                          
        //    }
        //    catch (Exception ex)
        //    {

        //        UIHelp.WriteErrorLog(Page, "企业审核从业证书续期申请失败！", ex);
        //        return;
        //    }

        //    UIHelp.WriteOperateLog(PersonName, UserID, "企业审核从业证书续期申请", string.Format("审核了{0}条记录！审核结果为“{1}”，审核意见：“{2}”。", rowCount, RadioButtonListApplyStatus.SelectedValue, TextBoxApplyGetResult.Text));

        //    UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核意见：“{2}”。", rowCount, RadioButtonListApplyStatus.SelectedValue, TextBoxApplyGetResult.Text));
        //    ClearGridSelectedKeys(RadGridAccept);
        //    ButtonSearch_Click(BtnSave, null);
        //}      

    }
}
