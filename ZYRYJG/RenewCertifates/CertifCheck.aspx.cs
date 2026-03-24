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
    public partial class CertifCheck : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("~/ResultInfoPage.aspx?o=尚未开通！");
            if (!IsPostBack)
            {
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelCheck.Text = "安全生产考核三类人员";
                        break;
                    case "2":
                        LabelCheck.Text = "建筑施工特种作业";
                        break;
                    case "3":
                        LabelCheck.Text = "造价员";
                        break;
                }
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                if (PersonType == 1 || PersonType == 6)
                {
                    PostSelect1.LockPostTypeID();
                }
                //if (PostSelect1.PostTypeID != "1" && PostSelect1.PostTypeID != "3")//三类人、造价员比对社保
                //{
                //    RadGridAccept.MasterTableView.Columns.FindByUniqueName("sb").Visible = false;
                //}

                ReportSelect1.SetPostTypeID(PostSelect1.PostTypeID);
                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {
            QueryParamOB q = new QueryParamOB();

            //审核状态
            TableFirstCheck.Visible = false;
            switch (RadioButtonListStatus.SelectedValue)
            {
                case "未审核"://未审核  
                    TableFirstCheck.Visible = true;//可批量审核
                    q.Add(string.Format("Status='{0}' and GetResult='{1}'", EnumManager.CertificateContinueStatus.Accepted, EnumManager.CertificateContinueCheckResult.GetPass));
                    q.Add("ReportCode >''");
                    break;
                case "退回修改"://退回修改                   
                    q.Add(string.Format("Status='{0}' and [CheckDate]>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
                    break;               
                case "审核通过"://审核通过 
                    q.Add(string.Format("Status<>'{0}' and [CheckDate]>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));                    
                    break;
                case ""://全部
                    q.Add(string.Format("((Status='{0}' and GetResult='{1}' and ReportCode >'') or ([CheckDate]>'1950-1-1'))", EnumManager.CertificateContinueStatus.Accepted, EnumManager.CertificateContinueCheckResult.GetPass));
                    break;
            }
            //证书人姓名
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));

            //证件号码
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));

            //岗位类别
            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));

            //工种类别
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
                        break;
                }
            }

            //证书编号
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));

            //发证年度
            if (RadNumericTextBoxConferData.Value.HasValue == true)
            {
                //q.Add(string.Format("DATEPART(year,conferdate)={0}", RadNumericTextBoxConferData.Value));
                q.Add(string.Format("conferdate between '{0}-01-01' and '{0}-12-31'", RadNumericTextBoxConferData.Value));
            }
            //证书有效期
            if (!RadDatePickerFrom.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate >='{0}'", RadDatePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (!RadDatePickerEnd.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            //所在单位
            if (RadTextBoxUnitName.Text.Trim() != "") q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));

            //机构代码
            if (RadTextBoxUnitCode.Text.Trim() != "") q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));

            //受理批次号
            if (RadTextBoxGetCode.Text.Trim() != "") q.Add(string.Format("GetCode like '%{0}%'", RadTextBoxGetCode.Text.Trim()));

            //受理时间-起始
            if (!RadDatePickerGetDateStart.IsEmpty) q.Add(string.Format("[GETDATE]>='{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //受理时间-截止
            if (!RadDatePickerGetDateEnd.IsEmpty) q.Add(string.Format("[GETDATE]<'{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //审核时间-起始
            if (!RadDatePickerCheckDateStart.IsEmpty) q.Add(string.Format("CheckDate>='{0}'", RadDatePickerCheckDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //审核时间-截止
            if (!RadDatePickerCheckDateEnd.IsEmpty) q.Add(string.Format("CheckDate<'{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //初审单位
            if (RadTextBoxFirstCheckUnitName.Text.Trim() != "") q.Add(string.Format("FirstCheckUnitName like '%{0}%'", RadTextBoxFirstCheckUnitName.Text.Trim()));

            //汇总批次号
            if(ReportSelect1.ReportCode !="")
            {
                q.Add(string.Format("ReportCode like '{0}'", ReportSelect1.ReportCode));
            }
            if (RadioButtonListjxjyway.SelectedValue != "")//继续教育形式
            {
                q.Add(string.Format("jxjyway={0}", RadioButtonListjxjyway.SelectedValue));
            }
            return q;
        }

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <returns>排序表达式</returns>
        private string SetSortBy()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            RadGridAccept.MasterTableView.SortExpressions.Clear();
            RadGridAccept.MasterTableView.AllowMultiColumnSorting = true;
            if (RadioButtonListStatus.SelectedValue == "未审核")
            {
                //排序规则=初审时间，企业，岗位工种，证书编号
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "[GETDATE]";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
                sb.Append(",").Append("[GETDATE] desc");

                GridSortExpression sortStr2 = new GridSortExpression();
                sortStr2.FieldName = "CERTIFICATECONTINUEID";
                sortStr2.SortOrder = GridSortOrder.Ascending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr2);
                sb.Append(",").Append("CERTIFICATECONTINUEID");
              
            }
            else
            {
                GridSortExpression sortStr5 = new GridSortExpression();
                sortStr5.FieldName = "[CHECKDATE]";
                sortStr5.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr5);
                sb.Append(",").Append("[CHECKDATE] desc");

                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "[GETDATE]";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
                sb.Append(",").Append("[GETDATE] desc");

                GridSortExpression sortStr2 = new GridSortExpression();
                sortStr2.FieldName = "CERTIFICATECONTINUEID";
                sortStr2.SortOrder = GridSortOrder.Ascending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr2);
                sb.Append(",").Append("CERTIFICATECONTINUEID");

              
            }         

            if (sb.Length > 0) sb.Remove(0, 1);
            return sb.ToString();
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGridAccept);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = GetQueryParamOB();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            SetSortBy();          

            RadGridAccept.CurrentPageIndex = 0;
            RadGridAccept.DataSourceID = ObjectDataSource1.ID;
        }

        ////审核不通过
        //protected void ButtonNoAccept_Click(object sender, EventArgs e)
        //{
        //    FirstCheck(EnumManager.CertificateContinueCheckResult.CheckNoPass);
        //}

        ////审核通过
        //protected void ButtonAccept_Click(object sender, EventArgs e)
        //{
        //    FirstCheck(EnumManager.CertificateContinueCheckResult.CheckPass);
        //}

        ////审核
        //protected void FirstCheck(string checkResult)
        //{
        //    UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        //    if (IsGridSelected(RadGridAccept) == false)
        //    {
        //        UIHelp.layerAlert(Page, "你还没有选择数据！");
        //        return;
        //    }            

        //    int rowCount = 0;//处理记录数量
        //    string filterString = "";//过滤条件
        //    string sqsl = "";//受理批次号

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
        //    ob.CheckDate = DateTime.Now;//时间
        //    ob.CheckResult = checkResult;//结果
        //    ob.CheckMan = PersonName;//人
        //    ob.Status = EnumManager.CertificateContinueStatus.Checked;//状态
            
        //    ob.ModifyPersonID = PersonID;//修改人ID
        //    ob.ModifyTime = DateTime.Now;//修改时间 

        //    DBHelper db = new DBHelper();
        //    DbTransaction tran = db.BeginTransaction();//事务对象

        //    try
        //    {
        //        sqsl = UIHelp.GetNextBatchNumber(tran, "XQSH");//审核批次号
        //        ob.CheckCode = sqsl;//批次号
        //        CertificateContinueDAL.Check(tran, ob,string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})",filterString));
        //        tran.Commit();//提交事务               
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        UIHelp.WriteErrorLog(Page, "证书续期审核失败！", ex);
        //        return;
        //    }

        //    UIHelp.WriteOperateLog(PersonName, UserID, "审核证书续期", string.Format("审核了{0}条记录！审核结果为“{1}”，审核批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));

        //    UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));
        //    ClearGridSelectedKeys(RadGridAccept);
        //    ButtonSearch_Click(ButtonAccept, null);
        //}

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridAccept, "CertificateContinueID");
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParamOB();
            string sortBy = SetSortBy();

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/续期审核_{0}.xls", PersonID.ToString());//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器
                //CertificateContinueDAL.OutputXlsFile(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), q.ToWhereString(), sortBy);

                string colHead = "";
                string colName =  "";
                if (PostSelect1.PostTypeID == "2")
                {
                    colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\原单位名称\现单位名称\初审单位";
                    colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\NewUnitName\FirstCheckUnitname";
                }
                else
                {
                    colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称\机构代码\初审单位";
                    colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\UnitCode\FirstCheckUnitname";
                }
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , "DBO.VIEW_CERTIFICATECONTINUE"
                    , q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出续期审核查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("续期审核查询结果下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        protected void RadioButtonListFirstCheckResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListFirstCheckResult.SelectedValue == "通过")
            {
                TextBoxFirstCheckRemark.Text = "审核通过";
            }
            else
            {
                TextBoxFirstCheckRemark.Text = "退回修改，原因：";
            }
        }

        //保存审核结果
        protected void ButtonFirstCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
            if (IsGridSelected(RadGridAccept) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            int rowCount = 0;//处理记录数量
            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridAccept) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridAccept) == true)//排除
                    filterString = string.Format(" {0} and CertificateContinueID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
                else//包含
                    filterString = string.Format(" {0} and CertificateContinueID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
            }

            rowCount = CertificateContinueDAL.SelectCount(filterString);

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();//事务对象
            try
            {
                CertificateContinueOB ob = new CertificateContinueOB();
                ob.CheckDate = DateTime.Now;//时间
                ob.CheckResult = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueCheckResult.CheckPass : TextBoxFirstCheckRemark.Text);//结果
                ob.CheckMan = PersonName;//人
                ob.Status = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueStatus.Checked : EnumManager.CertificateContinueStatus.SendBack);//状态
                ob.CheckCode = UIHelp.GetNextBatchNumber(tran, "XQSH");//审核批次号

                CertificateContinueDAL.Check(tran,ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "建委审核从业证书续期申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "审核从业证书续期申请", string.Format("审核了{0}条记录！审核结果为“{1}”，审核意见：“{2}”。", rowCount, RadioButtonListFirstCheckResult.SelectedValue, TextBoxFirstCheckRemark.Text));

            UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核意见：“{2}”。", rowCount, RadioButtonListFirstCheckResult.SelectedValue, TextBoxFirstCheckRemark.Text));
            ClearGridSelectedKeys(RadGridAccept);
            ButtonSearch_Click(ButtonFirstCheck, null);
        }

          /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="UnitApplyDate">企业上报时间</param>
        /// <param name="UpReportDate">区县上报时间</param>
        /// <param name="ApplyStatus">申请状态</param>
        /// <returns></returns>
        protected string formatCheckList(object UnitApplyDate, object UpReportDate,object ApplyStatus)
        {

            int limitDays = 5;//审核时限（天数）
            int WaitDays = 0;//等待审批天数

            if (ApplyStatus.ToString()== EnumManager.CertificateContinueStatus.SendBack ||
                ApplyStatus.ToString() == EnumManager.CertificateContinueStatus.Checked||
                ApplyStatus.ToString()== EnumManager.CertificateContinueStatus.Decided )
            {
                return "";
            }

            if (UpReportDate != DBNull.Value)//已到市级审批
            {
                 WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UpReportDate));
            }
                  

            //return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
            //    , (WaitDays > limitDays ? "style='diaplay:block;color:red'" : ""));

            return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
               , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
        }
    

        /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="UnitApplyDate">企业上报时间</param>
        /// <param name="UpReportDate">区县上报时间</param>
        /// <returns></returns>
        //protected string formatCheckList(object UnitApplyDate, object UpReportDate)
        //{

        //    int limitDays = 5;//审核时限（天数）
        //    int WaitDays = 0;//等待审批天数
        //    int level = 0;//当前所处审批级别：0区县，1市级

        //    if (UpReportDate != DBNull.Value)//已到市级审批
        //    {
        //        level = 1;
        //        WaitDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(UpReportDate)).TotalDays);
        //    }
        //    else if (UnitApplyDate != DBNull.Value)//处在县级审批
        //    {
        //        WaitDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(UnitApplyDate)).TotalDays);
        //    }



        //    return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
        //        , (WaitDays > limitDays ? "style='diaplay:block;color:red'" : ""));
        //}
    }
}
