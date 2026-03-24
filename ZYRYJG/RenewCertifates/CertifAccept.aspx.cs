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
    public partial class CertifAccept : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelAccepy.Text = "安全生产考核三类人员";
                        break;
                    case "2":
                        LabelAccepy.Text = "建筑施工特种作业";
                        break;
                    case "3":
                        LabelAccepy.Text = "造价员";
                        break;
                }
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                if (PersonType == 1 || PersonType == 3 || PersonType == 6)
                {
                    PostSelect1.LockPostTypeID();
                }
                //if (PostSelect1.PostTypeID != "1" && PostSelect1.PostTypeID != "3")//三类人、造价员比对社保
                //{
                //    RadGridAccept.MasterTableView.Columns.FindByUniqueName("sb").Visible = false;
                //}

                Form.DefaultButton = ButtonSearch.UniqueID;
                RadTextBoxCertificateContinueID.Focus();

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

            //查看隶属自己的初审数据（大集团：机构代码；区县：区县编码；市建委：100000）
            q.Add(string.Format("FirstCheckUnitCode ='{0}'", RegionCode));
           
            //初审状态
            TableFirstCheck.Visible = false;
            switch (RadioButtonListStatus.SelectedValue)
            {
                case "未受理"://未受理  
                    TableFirstCheck.Visible = true;
                    q.Add(string.Format("Status='{0}'", EnumManager.CertificateContinueStatus.Applyed));
                    break;
                case "退回修改"://退回修改                   
                     q.Add(string.Format("Status='{0}' and [GETDATE]>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
                    break;
                case "初审通过"://初审通过 
                    q.Add(string.Format("Status<>'{0}' and Status<>'{1}' and [GETDATE]>'1950-1-1' and GetResult='{2}'", EnumManager.CertificateContinueStatus.SendBack, EnumManager.CertificateContinueStatus.NewSave, EnumManager.CertificateContinueCheckResult.GetPass));
                    break;
                case ""://全部
                    q.Add(string.Format("(Status='{0}' or (Status='{1}' and [GETDATE]>'1950-1-1') or  GetResult='{2}')", EnumManager.CertificateContinueStatus.Applyed, EnumManager.CertificateContinueStatus.SendBack, EnumManager.CertificateContinueCheckResult.GetPass));
                    break;
            }
            //证书人姓名
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));

            //证件号码
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));

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
            if (RadTextBoxUnitName.Text.Trim() != "") q.Add(string.Format("NewUnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));

            //机构代码
            if (RadTextBoxUnitCode.Text.Trim() != "") q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));

            //申请编号
            if (RadTextBoxApplyCode.Text.Trim() != "") q.Add(string.Format("ApplyCode like '%{0}%'", RadTextBoxApplyCode.Text.Trim()));

            //受理时间-起始
            if (!RadDatePickerGetDateStart.IsEmpty) q.Add(string.Format("[GETDATE]>='{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //受理时间-截止
            if (!RadDatePickerGetDateEnd.IsEmpty) q.Add(string.Format("[GETDATE]<'{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //申请时间-起始
            if (!txtSApplyDate.IsEmpty) q.Add(string.Format("NewUnitCheckTime>='{0}'", txtSApplyDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //申请时间-截止
            if (!txtEApplyDate.IsEmpty) q.Add(string.Format("NewUnitCheckTime<'{0}'", txtEApplyDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            if (string.IsNullOrEmpty(RadTextBoxCertificateContinueID.Text.Trim()) == false)//申请ID（条形码）
            {
                q.Add(string.Format("CertificateContinueID ={0}", RadTextBoxCertificateContinueID.Text.Trim()));
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
            return "CERTIFICATECONTINUEID desc";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            RadGridAccept.MasterTableView.SortExpressions.Clear();
            RadGridAccept.MasterTableView.AllowMultiColumnSorting = true;
            if (RadioButtonListStatus.SelectedValue == "未受理")
            { 
                GridSortExpression sortStr2 = new GridSortExpression();
                sortStr2.FieldName = "CERTIFICATECONTINUEID";
                sortStr2.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr2);
                sb.Append(",").Append("CERTIFICATECONTINUEID");             
            }
            else
            {
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "[GETDATE]";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
                sb.Append(",").Append("[GETDATE] desc");              

                GridSortExpression sortStr4 = new GridSortExpression();
                sortStr4.FieldName = "CERTIFICATECONTINUEID";
                sortStr4.SortOrder = GridSortOrder.Ascending;
                RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr4);
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

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/续期初审_{0}.xls", UserID);//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器
                //CertificateContinueDAL.OutputXlsFile(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), q.ToWhereString(), sortBy);

                string colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称\机构代码\初审单位";
                string colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\UnitCode\FirstCheckUnitname";
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , "DBO.VIEW_CERTIFICATECONTINUE"
                    , q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出续期初审查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("续期初审查询结果下载", saveFile));
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

        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        }

        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridAccept, "CertificateContinueID");
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
                ob.GetDate = DateTime.Now;//受理时间
                ob.GetResult = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueCheckResult.GetPass : TextBoxFirstCheckRemark.Text);//受理结果
                ob.GetMan = PersonName;//受理人
                ob.Status = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueStatus.Accepted : EnumManager.CertificateContinueStatus.SendBack);//状态
                ob.GetCode = UIHelp.GetNextBatchNumber(tran, "XQSL");//受理批次号

                CertificateContinueDAL.CheckAccept(tran,ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "初审从业证书续期申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "初审从业证书续期申请", string.Format("审核了{0}条记录！审核结果为“{1}”，审核意见：“{2}”。", rowCount, RadioButtonListFirstCheckResult.SelectedValue, TextBoxFirstCheckRemark.Text));

            UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核意见：“{2}”。", rowCount, RadioButtonListFirstCheckResult.SelectedValue, TextBoxFirstCheckRemark.Text));
            ClearGridSelectedKeys(RadGridAccept);
            ButtonSearch_Click(ButtonFirstCheck, null);     
        }

        protected void RadioButtonListFirstCheckResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonListFirstCheckResult.SelectedValue=="通过")
            {
                TextBoxFirstCheckRemark.Text = "初审通过";
            }
            else
            {
                TextBoxFirstCheckRemark.Text = "退回修改，原因：";
            }
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

            if (ApplyStatus.ToString()== EnumManager.CertificateContinueStatus.SendBack
                || UpReportDate != DBNull.Value//已到市级审批
                )
            {
                return "";
            }
          
            else if (UnitApplyDate != DBNull.Value)//处在县级审批
            {
                WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));
            }            

            //return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
            //    , (WaitDays > limitDays ? "style='diaplay:block;color:red'" : ""));

            return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
              , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
        }
    }
}
