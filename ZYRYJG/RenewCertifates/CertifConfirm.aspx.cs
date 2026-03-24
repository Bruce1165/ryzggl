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
    public partial class CertifConfirm : BasePage
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

            //初审状态
            TableFirstCheck.Visible = false;
            switch (RadioButtonListStatus.SelectedValue)
            {
                case "未决定"://审核通过(未决定)  
                  TableFirstCheck.Visible = true;//可批量审核
                    q.Add(string.Format("Status='{0}' and CheckResult='{1}'", EnumManager.CertificateContinueStatus.Checked, EnumManager.CertificateContinueCheckResult.CheckPass));
                    break;
                case "退回修改"://退回修改
                     TableFirstCheck.Visible = false;
                    q.Add(string.Format("Status='{0}' and [ConfirmDate]>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
                    break;
                case "决定通过"://决定通过 
                    TableFirstCheck.Visible = false;
                    q.Add(string.Format("Status='{0}' and ConfirmResult='{1}'", EnumManager.CertificateContinueStatus.Decided, EnumManager.CertificateContinueCheckResult.DecidPass));
                    break;
                case ""://全部
                    TableFirstCheck.Visible = false;
                    q.Add(string.Format("(([ConfirmDate]>'1950-1-1') or (Status='{0}' and CheckResult='{1}'))", EnumManager.CertificateContinueStatus.Checked, EnumManager.CertificateContinueCheckResult.CheckPass));
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

            //审核批次号
            if (RadTextBoxGetCode.Text.Trim() != "") q.Add(string.Format("CheckCode like '%{0}%'", RadTextBoxGetCode.Text.Trim()));

            //审核时间-起始
            if (!RadDatePickerCheckDateStart.IsEmpty) q.Add(string.Format("CheckDate>='{0}'", RadDatePickerCheckDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //审核时间-截止
            if (!RadDatePickerCheckDateEnd.IsEmpty) q.Add(string.Format("CheckDate<'{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //决定批次号
            if (RadTextBoxConfirmCode.Text.Trim() != "") q.Add(string.Format("ConfirmCode like '%{0}%'", RadTextBoxConfirmCode.Text.Trim()));

            //决定时间-起始
            if (!RadDatePickerConfirmDateStart.IsEmpty) q.Add(string.Format("ConfirmDate>='{0}'", RadDatePickerConfirmDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //决定时间-截止
            if (!RadDatePickerConfirmDateEnd.IsEmpty) q.Add(string.Format("ConfirmDate<'{0}'", RadDatePickerConfirmDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //初审单位
            if (RadTextBoxFirstCheckUnitName.Text.Trim() != "") q.Add(string.Format("FirstCheckUnitName like '%{0}%'", RadTextBoxFirstCheckUnitName.Text.Trim()));

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
            if (RadioButtonListStatus.SelectedValue == "未决定" )
            {
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "[CHECKDATE]";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
                sb.Append(",").Append("[CHECKDATE] desc");

                GridSortExpression sortStr2 = new GridSortExpression();
                sortStr2.FieldName = "CERTIFICATECONTINUEID";
                sortStr2.SortOrder = GridSortOrder.Ascending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr2);
                sb.Append(",").Append("CERTIFICATECONTINUEID");
            }
            else
            {
                GridSortExpression sortStr5 = new GridSortExpression();
                sortStr5.FieldName = "[CONFIRMDATE]";
                sortStr5.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr5);
                sb.Append(",").Append("[CONFIRMDATE] desc");

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

        protected void RadioButtonListFirstCheckResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListFirstCheckResult.SelectedValue == "通过")
            {
                TextBoxFirstCheckRemark.Text = "决定通过";
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
            string sqsl = "";//决定批次号

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

            CertificateContinueOB ob = new CertificateContinueOB();
            ob.ConfirmDate = DateTime.Now;//时间
            ob.ConfirmResult = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueCheckResult.DecidPass : TextBoxFirstCheckRemark.Text);//结果
            ob.ConfirmMan = PersonName;//人
            ob.Status = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueStatus.Decided : EnumManager.CertificateContinueStatus.SendBack);//状态

            //证书表实例  
            CertificateOB cob = new CertificateOB();
            cob.Status = EnumManager.CertificateUpdateType.Continue;
            cob.ModifyPersonID = PersonID;  //最后修改人
            cob.ModifyTime = DateTime.Now;   //最后修改时间
            cob.CheckDate = ob.ConfirmDate;    //审批时间
            cob.CheckMan = ob.ConfirmMan;      //审批人
            cob.CheckAdvise = ob.ConfirmResult;//审批意见
            cob.PrintDate = null;//打印时间
            cob.PrintMan = null;//打印人                       
            //cob.CaseStatus = "已归档";//归档状态
            //cob.UnitCode = ob.UnitCode;//组织机构代码

            int AddYears = 0;//有效期增加几年
            switch (PostSelect1.PostTypeID)
            {
                case "1"://三类人
                    AddYears = 3;//加3年
                    break;
                case "3"://造价员
                    AddYears = 4;//加4年
                    break;
                case "2"://特种作业
                    AddYears = 2;//加2年
                    break;
            }
            rowCount = CertificateContinueDAL.SelectCount(filterString);
            string ListCertificateID = CertificateContinueDAL.GetCertificateIDList(filterString);

            //检查是否重复注册
            if (ob.ConfirmResult == EnumManager.CertificateContinueCheckResult.DecidPass)
            {
                int checkeRepeatApply = CommonDAL.SelectRowCount("dbo.Certificate", string.Format(" and CertificateID in ({0}) and ValidEndDate > dateadd(year,1,'{1}')", ListCertificateID, DateTime.Now.ToString("yyyy-MM-dd")));
                if (checkeRepeatApply > 0)
                {
                    DataTable dtRepeatApply = CommonDAL.GetDataTable(string.Format("select CertificateCode from dbo.Certificate where CertificateID in ({0}) and ValidEndDate > dateadd(year,1,'{1}')", ListCertificateID, DateTime.Now.ToString("yyyy-MM-dd")));

                    System.Text.StringBuilder sbRepeatApply = new System.Text.StringBuilder();
                    foreach (DataRow r in dtRepeatApply.Rows)
                    {
                        sbRepeatApply.Append(string.Format("，{0}", r["CertificateCode"]));
                    }
                    if (sbRepeatApply.Length > 0)
                    {
                        sbRepeatApply.Remove(0, 1);
                    }
                    sbRepeatApply.Insert(0, "审核失败。以下证书可能存在重复申请续期情况（已经续期过了！），请先否决这些记录！<br />证书编号：");
                    UIHelp.layerAlert(Page, sbRepeatApply.ToString());
                    return;
                }

                string sql = @"
                select count(*),certificatecode
                 from DBO.VIEW_CERTIFICATECONTINUE where 
                CertificateID in ({0}) 
                and Status ='已审核'
                group by certificatecode
                having count(*) >1";
                DataTable dtRepeatApply2 = CommonDAL.GetDataTable(string.Format(sql, ListCertificateID));

                if (dtRepeatApply2 != null && dtRepeatApply2.Rows.Count > 0)
                {
                    System.Text.StringBuilder sbRepeatApply = new System.Text.StringBuilder();
                    foreach (DataRow r in dtRepeatApply2.Rows)
                    {
                        sbRepeatApply.Append(string.Format("，{0}", r["CertificateCode"]));
                    }
                    if (sbRepeatApply.Length > 0)
                    {
                        sbRepeatApply.Remove(0, 1);
                    }
                    sbRepeatApply.Insert(0, "审核失败。以下证书可能存在重复申请续期情况（无法多次增加有效期），请先否决这些记录！<br />证书编号：");
                    UIHelp.layerAlert(Page, sbRepeatApply.ToString());
                    return;
                }

            }

            //更新法人验证标志
            CommonDAL.ExecSQL(string.Format(@"
                update [dbo].[CERTIFICATE]
                    set [CERTIFICATE].IfFR = case when c.WORKERNAME = q.[CORP_RPT] then 1 else 0 end
                from [CERTIFICATE] c               
                inner join [dbo].[QY_GSDJXX] q on c.UNITCODE = q.UNITCODE
                where 1=1  and CertificateID in (select CertificateID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0}) and c.postid=147 and q.VALID=1 and q.ENT_STATE=1
                ", filterString));


            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();//事务对象
            try
            {
                sqsl = UIHelp.GetNextBatchNumber(tran, "XQJD");//决定批次号
                ob.ConfirmCode = sqsl;//决定批次号

                CertificateContinueDAL.Confirm(tran, ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));

                if (ob.ConfirmResult == EnumManager.CertificateContinueCheckResult.DecidPass)
                {
                    //向历史表插入历史数据
                    CertificateHistoryDAL.InsertContinueHistoryBatch(tran, string.Format(" and CertificateID in ({0})", ListCertificateID));

                    //修改证书表
                    if (PostSelect1.PostTypeID == "2")//特种作业续期同时变更单位，需要修改证书单位及组织机构代码
                    {
                        CertificateDAL.UpdateByContinueConfirmWithChangeUnit(tran, cob, AddYears, string.Format(" and CONFIRMCODE = '{0}'", sqsl));
                    }
                    else
                    {
                        CertificateDAL.UpdateByContinueConfirm(tran, cob, AddYears, string.Format(" and CertificateID in ({0})", ListCertificateID));
                    }
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书续期决定失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "决定证书续期", string.Format("决定了{0}条记录！决定结果为“{1}”，决定批次号：“{2}”。", rowCount, ob.ConfirmResult, sqsl));

            UIHelp.layerAlert(Page, string.Format("您成功的决定了{0}条记录！决定结果为“{1}”,决定批次号：“{2}”。", rowCount, ob.ConfirmResult, sqsl));
            ClearGridSelectedKeys(RadGridAccept);
            ButtonSearch_Click(sender, e);

        }

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
            if (RadGridAccept.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            QueryParamOB q = GetQueryParamOB();
            string sortBy = SetSortBy();

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/续期决定_{0}.xls", PersonID.ToString());//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器              
                string colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称\机构代码\初审单位";
                string colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\UnitCode\FirstCheckUnitname";
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , "DBO.VIEW_CERTIFICATECONTINUE"
                    , q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出续期决定查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("续期决定查询结果下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode":
                case "WorkerCertificateCode":
                    e.Cell.Style["mso-number-format"] = @"\@";
                    break;
            }

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "CertificateCode")
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

        /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="UnitApplyDate">企业上报时间</param>
        /// <param name="UpReportDate">区县上报时间</param>
        /// <param name="ApplyStatus">申请状态</param>
        /// <returns></returns>
        protected string formatCheckList(object UnitApplyDate, object UpReportDate, object ApplyStatus)
        {

            int limitDays = 5;//审核时限（天数）
            int WaitDays = 0;//等待审批天数

            if (ApplyStatus.ToString() == EnumManager.CertificateContinueStatus.SendBack ||
                ApplyStatus.ToString() == EnumManager.CertificateContinueStatus.Decided)
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

//        //审核不通过
//        protected void ButtonNoAccept_Click(object sender, EventArgs e)
//        {
//            FirstCheck(EnumManager.CertificateContinueCheckResult.DecidNoPass);
//        }

//        //审核通过
//        protected void ButtonAccept_Click(object sender, EventArgs e)
//        {
//            FirstCheck(EnumManager.CertificateContinueCheckResult.DecidPass);
//        }

//        //审核
//        protected void FirstCheck(string checkResult)
//        {
//            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
//            if (IsGridSelected(RadGridAccept) == false)
//            {
//                UIHelp.layerAlert(Page, "你还没有选择数据！");
//                return;
//            }

//            int rowCount = 0;//处理记录数量
//            string filterString = "";//过滤条件
//            string sqsl = "";//受理批次号

//            if (GetGridIfCheckAll(RadGridAccept) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGridAccept) == true)//排除
//                    filterString = string.Format(" {0} and CertificateContinueID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
//                else//包含
//                    filterString = string.Format(" {0} and CertificateContinueID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridAccept));
//            }

//            //续期申请实例
//            CertificateContinueOB ob = new CertificateContinueOB();
//            ob.ConfirmDate = DateTime.Now;//时间
//            ob.ConfirmResult = checkResult;//结果
//            ob.ConfirmMan = PersonName;//人
//            ob.Status = EnumManager.CertificateContinueStatus.Decided;//状态           
//            ob.ModifyPersonID = PersonID;//修改人ID
//            ob.ModifyTime = DateTime.Now;//修改时间   

//            //证书表实例  
//            CertificateOB cob = new CertificateOB();
//            cob.Status = EnumManager.CertificateUpdateType.Continue;
//            cob.ModifyPersonID = PersonID;  //最后修改人
//            cob.ModifyTime = DateTime.Now;   //最后修改时间
//            cob.CheckDate = ob.ConfirmDate;    //审批时间
//            cob.CheckMan = ob.ConfirmMan;      //审批人
//            cob.CheckAdvise = ob.ConfirmResult;//审批意见
//            cob.PrintDate = null;//打印时间
//            cob.PrintMan = null;//打印人                       
//            cob.CaseStatus = "已归档";//归档状态
//            //cob.UnitCode = ob.UnitCode;//组织机构代码

//            int AddYears = 0;//有效期增加几年
//            switch (PostSelect1.PostTypeID)
//            {
//                case "1"://三类人
//                    AddYears = 3;//加3年
//                    break;
//                case "3"://造价员
//                    AddYears = 4;//加4年
//                    break;
//                case "2"://特种作业
//                    AddYears = 2;//加2年
//                    break;
//            }
//            rowCount = CertificateContinueDAL.SelectCount(filterString);
//            string ListCertificateID = CertificateContinueDAL.GetCertificateIDList(filterString);

//            //检查是否重复注册
//            if (ob.ConfirmResult == EnumManager.CertificateContinueCheckResult.DecidPass)
//            {
//                int checkeRepeatApply = CommonDAL.SelectRowCount("dbo.Certificate", string.Format(" and CertificateID in ({0}) and ValidEndDate > dateadd(year,1,'{1}')", ListCertificateID, DateTime.Now.ToString("yyyy-MM-dd")));
//                if (checkeRepeatApply > 0)
//                {
//                    DataTable dtRepeatApply = CommonDAL.GetDataTable(string.Format("select CertificateCode from dbo.Certificate where CertificateID in ({0}) and ValidEndDate > dateadd(year,1,'{1}')", ListCertificateID, DateTime.Now.ToString("yyyy-MM-dd")));

//                    System.Text.StringBuilder sbRepeatApply = new System.Text.StringBuilder();
//                    foreach (DataRow r in dtRepeatApply.Rows)
//                    {
//                        sbRepeatApply.Append(string.Format("，{0}", r["CertificateCode"]));
//                    }
//                    if (sbRepeatApply.Length > 0)
//                    {
//                        sbRepeatApply.Remove(0, 1);
//                    }
//                    sbRepeatApply.Insert(0, "审核失败。以下证书可能存在重复申请续期情况（已经续期过了！），请先否决这些记录！<br />证书编号：");
//                    UIHelp.layerAlert(Page, sbRepeatApply.ToString());
//                    return;
//                }

//                string sql = @"
//                select count(*),certificatecode
//                 from DBO.VIEW_CERTIFICATECONTINUE where 
//                CertificateID in ({0}) 
//                and Status ='已审核'
//                group by certificatecode
//                having count(*) >1";
//                DataTable dtRepeatApply2 = CommonDAL.GetDataTable(string.Format(sql, ListCertificateID));

//                if (dtRepeatApply2 != null && dtRepeatApply2.Rows.Count > 0)
//                {
//                    System.Text.StringBuilder sbRepeatApply = new System.Text.StringBuilder();
//                    foreach (DataRow r in dtRepeatApply2.Rows)
//                    {
//                        sbRepeatApply.Append(string.Format("，{0}", r["CertificateCode"]));
//                    }
//                    if (sbRepeatApply.Length > 0)
//                    {
//                        sbRepeatApply.Remove(0, 1);
//                    }
//                    sbRepeatApply.Insert(0, "审核失败。以下证书可能存在重复申请续期情况（无法多次增加有效期），请先否决这些记录！<br />证书编号：");
//                    UIHelp.layerAlert(Page, sbRepeatApply.ToString());
//                    return;
//                }

//            }


//            DBHelper db = new DBHelper();
//            DbTransaction tran = db.BeginTransaction();//事务对象
//            try
//            {
//                sqsl = UIHelp.GetNextBatchNumber(tran, "XQJD");//决定批次号
//                ob.ConfirmCode = sqsl;//批次号

//                //更新续期申请
//                CertificateContinueDAL.Confirm(tran, ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));

//                if (ob.ConfirmResult == EnumManager.CertificateContinueCheckResult.DecidPass)
//                {
//                    //向历史表插入历史数据
//                    CertificateHistoryDAL.InsertContinueHistoryBatch(tran, string.Format(" and CertificateID in ({0})", ListCertificateID));

//                    //修改证书表
//                    if (PostSelect1.PostTypeID == "2")//特种作业续期同时变更单位，需要修改证书单位及组织机构代码
//                    {
//                        CertificateDAL.UpdateByContinueConfirmWithChangeUnit(tran, cob, AddYears, string.Format(" and CONFIRMCODE = '{0}'", sqsl));
//                    }
//                    else
//                    {
//                        CertificateDAL.UpdateByContinueConfirm(tran, cob, AddYears, string.Format(" and CertificateID in ({0})", ListCertificateID));
//                    }
//                }

//                tran.Commit();//提交事务
//            }
//            catch (Exception ex)
//            {
//                tran.Rollback();
//                UIHelp.WriteErrorLog(Page, "证书续期决定失败！", ex);
//                return;
//            }

//            UIHelp.WriteOperateLog(PersonName, UserID, "决定证书续期", string.Format("决定了{0}条记录！决定结果为“{1}”，决定批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));

//            UIHelp.layerAlert(Page, string.Format("您成功的决定了{0}条记录！决定结果为“{1}”,决定批次号：“{2}”。", rowCount.ToString(), checkResult, sqsl));
//            ClearGridSelectedKeys(RadGridAccept);
//            ButtonSearch_Click(ButtonFirstCheck, null);
//        }
    }
}
