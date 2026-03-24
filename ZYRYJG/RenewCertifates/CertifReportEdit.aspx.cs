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
    public partial class CertifReportEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifReport.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (string.IsNullOrEmpty(Request["r"]) == false)
                {
                    ViewState["ReportCode"] = Request["r"];//汇总批次号
                    ButtonAccept.Visible = false;
                }
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelAccepy.Text = "三类人员";
                        break;
                    case "2":
                        LabelAccepy.Text = "特种作业";
                        break;
                    case "3":
                        LabelAccepy.Text = "造价员";
                        break;
                }
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                if (PersonType == 1 || PersonType == 6)
                {
                    PostSelect1.LockPostTypeID();
                }

                Form.DefaultButton = ButtonSearch.UniqueID;



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

            //查看隶属自己的初审数据（大集团：机构代码；区县：区县编码；市建委：100000）
            q.Add(string.Format("FirstCheckUnitCode ='{0}'", RegionCode));

            //初审状态
            q.Add(string.Format("Status='{0}' and GetResult='{1}'", EnumManager.CertificateContinueStatus.Accepted, EnumManager.CertificateContinueCheckResult.GetPass));

            //受理人
            if (RadTextBoxGetMan.Text.Trim() != "") q.Add(string.Format("[GetMan] like '{0}%'", RadTextBoxGetMan.Text.Trim()));

            //证书人姓名
            if (txtWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", txtWorkerName.Text.Trim()));

            //证件号码
            if (txtWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", txtWorkerCertificateCode.Text.Trim()));

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
            if (txtCertificateCode.Text.Trim() != "") q.Add(string.Format("CERTIFICATECODE like '%{0}%'", txtCertificateCode.Text.Trim()));

            //证书有效期至
            if (!txtValidEndtDate.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate ='{0}'", txtValidEndtDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            //所在单位
            if (txtUnitName.Text.Trim() != "") q.Add(string.Format("UnitName like '%{0}%'", txtUnitName.Text.Trim()));

            //机构代码
            if (RadTextBoxUnitCode.Text.Trim() != "") q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));

            //申请编号
            if (txtApplyCode.Text.Trim() != "") q.Add(string.Format("ApplyCode like '%{0}%'", txtApplyCode.Text.Trim()));

            //受理时间-起始
            if (!RadDatePickerGetDateStart.IsEmpty) q.Add(string.Format("[GETDATE]>='{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //受理时间-截止
            if (!RadDatePickerGetDateEnd.IsEmpty) q.Add(string.Format("[GETDATE]<'{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            //申请时间-起始
            if (!txtSApplyDate.IsEmpty) q.Add(string.Format("ApplyDate>='{0}'", txtSApplyDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //申请时间-截止
            if (!txtEApplyDate.IsEmpty) q.Add(string.Format("ApplyDate<'{0}'", txtEApplyDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));

            if (ViewState["ReportCode"] != null)//汇总批次号
            {
                q.Add(string.Format("ReportCode='{0}'", ViewState["ReportCode"]));
            }
            else
            {
                q.Add("ReportCode is null");
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


        //保存汇总
        protected void ButtonAccept_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");

            if (IsGridSelected(RadGridAccept) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            string filterString = "";
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


            string sqsl = "";//受理批次号

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();//事务对象

            try
            {
                sqsl = UIHelp.GetNextBatchNumber(tran, "XQHZ");//续期汇总批次号

                if (ViewState["ReportCode"] != null)//修改
                {

                    //取消已分配批次号
                    CertificateContinueDAL.ReportCancel(tran, ViewState["ReportCode"].ToString());

                    //重新分配批次号
                    CertificateContinueDAL.ReportAdd(tran, sqsl, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));

                }
                else//新增
                {
                    CertificateContinueDAL.ReportAdd(tran, sqsl, string.Format(" and CertificateContinueID in (select CertificateContinueID from DBO.VIEW_CERTIFICATECONTINUE where 1=1 {0})", filterString));
                }
                ViewState["ReportCode"] = sqsl;//上报编码

                tran.Commit();//提交事务
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书续期初审汇总失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "证书续期初审汇总", string.Format("汇总批次号：“{0}”。", sqsl));

            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }


        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridAccept, "CertificateContinueID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridAccept, "CertificateContinueID");
        }

    }
}
