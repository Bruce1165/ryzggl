using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!IsPostBack)
            {
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
            q.Add(string.Format("(NewUnitcode like '{0}%')", ZZJGDM)); //改为现单位确认

            //初审状态
            divQX.Visible = false;
            switch (RadioButtonListStatus.SelectedValue)
            {
                case "待单位确认"://未审核                  
                    q.Add(string.Format("Status='{0}'", EnumManager.CertificateContinueStatus.WaitUnitCheck));
                    divQX.Visible = true;
                    break;
                case "退回修改"://审核不通过 
                    q.Add(string.Format("Status='{0}' and NewUnitCheckTime>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
                    break;
                case "已审查"://审核通过 
                    q.Add(string.Format("Status<>'{0}' and NewUnitCheckTime>'1950-1-1'", EnumManager.CertificateContinueStatus.SendBack));
                    break;
                case ""://全部
                    q.Add(string.Format("(Status='{0}' or NewUnitCheckTime>'1950-1-1')", EnumManager.CertificateContinueStatus.WaitUnitCheck));
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

            if (RadioButtonListjxjyway.SelectedValue != "")//继续教育形式
            {
                q.Add(string.Format("jxjyway={0}", RadioButtonListjxjyway.SelectedValue));
            }
            

            //证书编号
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));

            //证书有效期至
            if (!txtValidEndtDate.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate ='{0}'", txtValidEndtDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            //受理时间-起始
            if (!RadDatePicker_GetDateStart.IsEmpty) q.Add(string.Format("[NewUnitCheckTime]>='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //受理时间-截止
            if (!RadDatePicker_GetDateEnd.IsEmpty) q.Add(string.Format("[NewUnitCheckTime]<'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

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

            //排序规则=初审时间，企业，岗位工种，证书编号
            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "[ApplyDate]";
            sortStr1.SortOrder = GridSortOrder.Descending;
            RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
            sb.Append(",").Append("[ApplyDate] desc");

            GridSortExpression sortStr4 = new GridSortExpression();
            sortStr4.FieldName = "CERTIFICATECONTINUEID";
            sortStr4.SortOrder = GridSortOrder.Ascending;
            RadGridAccept.MasterTableView.SortExpressions.AddSortExpression(sortStr4);
            sb.Append(",").Append("CERTIFICATECONTINUEID");

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
                string colName = "";
                if (PostSelect1.PostTypeID == "2")
                {
                    colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\原单位名称\现单位名称";
                    colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\NewUnitName";
                }
                else
                {
                    colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称";
                    colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName";
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

        //企业批量受理
        protected void BtnSave_Click(object sender, EventArgs e)
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

            #region  检查续期时间段，未在允许时间段内，不允许提交审核

            DataTable dt = CommonDAL.GetDataTable("select [TypeValue] from dbo.[Types] where TypeID='106'");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] _params;
            foreach (DataRow r in dt.Rows)
            {
                _params = r["TypeValue"].ToString().Split(',');
                sb.AppendFormat(" or (case when posttypeid={0} then dateadd(d,-1,dateadd(m,{1},FORMAT(VALIDENDDATE,'yyyy-01-01'))) end <getdate()  and FORMAT(VALIDENDDATE,'M-dd')='{2}')", _params[0], _params[3], _params[1]);
            }
            if(sb.Length >0)
            {
                sb.Remove(0, 3).Append(")").Insert(0, " and (");
            }

            dt = CommonDAL.GetDataTable(string.Format("select certificatecode from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0}{1}", filterString, sb));
            if (dt.Rows.Count > 0)
            {
                sb.Remove(0,sb.Length);
                foreach(DataRow r in dt.Rows)
                {
                    sb.AppendFormat("，{0}", r["certificatecode"]);
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                }
                UIHelp.layerAlert(Page, string.Format("审核失败，下列证书已经超过续期申请开放时间，审批端已经不再接收续期申请。证书编号：{0}", sb), 5, 0);
                return;
            }

            #endregion

            rowCount = CertificateContinueDAL.SelectCount(filterString);
            CertificateContinueOB ob = new CertificateContinueOB();
            ob.NewUnitCheckTime = DateTime.Now;
            ob.NewUnitAdvise = (RadioButtonListApplyStatus.SelectedValue == "通过" ? "提交建委审核" : TextBoxApplyGetResult.Text);//单位意见
            ob.Status = (RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.CertificateContinueStatus.Applyed : EnumManager.CertificateContinueStatus.SendBack);


            try
            {
                CertificateContinueDAL.CheckUnit(ob, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));

            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "企业审核从业证书续期申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "企业审核从业证书续期申请", string.Format("审核了{0}条记录！审核结果为“{1}”，审核意见：“{2}”。", rowCount, RadioButtonListApplyStatus.SelectedValue, TextBoxApplyGetResult.Text));

            UIHelp.layerAlert(Page, string.Format("您成功的审核了{0}条记录！审核结果为“{1}”,审核意见：“{2}”。", rowCount, RadioButtonListApplyStatus.SelectedValue, TextBoxApplyGetResult.Text));
            ClearGridSelectedKeys(RadGridAccept);
            ButtonSearch_Click(BtnSave, null);
        }

        protected void RadioButtonListApplyStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonListApplyStatus.SelectedValue=="通过")
                 TextBoxApplyGetResult.Text ="提交建委审核";
            else
                TextBoxApplyGetResult.Text ="退回修改，原因：";

        }      

    }
}
