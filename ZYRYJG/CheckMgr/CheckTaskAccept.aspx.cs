using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;
using System.IO;

namespace ZYRYJG.CheckMgr
{
    public partial class CheckTaskAccept : BasePage
    {
        /// <summary>
        /// 查看明细的批次号
        /// </summary>
        protected object PatchCode
        {
            get
            {
                return ViewState["PatchCode"];
            }
        }

        /// <summary>
        /// 查看明细的区县
        /// </summary>
        protected object Country
        {
            get
            {
                return ViewState["Country"];
            }
        }

        /// <summary>
        /// 进入明细时传入的统计参数
        /// </summary>
        protected object CommandName
        {
            get
            {
                return ViewState["CommandName"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ButtonQuery_Click(sender, e);
            }
        }

        //统计查询
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSourceTask.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            //区县审查权限
            if (IfExistRoleID("3") == true)
            {
                q.Add(string.Format("[Country] like '{0}%'", Region));
            }
            //集团
            if (IfExistRoleID("100") == true)
            {
                q.Add(string.Format("[Country] = (select [USERNAME] FROM [dbo].[USER] where [LICENSE] ='{0}')", ZZJGDM));
            }
            //造价管理处
            if (IfExistRoleID("300") == true)
            {
                q.Add(string.Format("[Country] = '造价管理处'", ZZJGDM));
            }

            //日期类型自定义查询条件
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("PublishiTime >= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("PublishiTime <= '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
            }
            //申报事项
            if (RadComboBoxCheckType.SelectedValue != "")
            {
                q.Add(string.Format("CheckType like '{0}'", RadComboBoxCheckType.SelectedValue));
            }
            ObjectDataSourceTask.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridTask.CurrentPageIndex = 0;
            RadGridTask.DataSourceID = ObjectDataSourceTask.ID;
        }

        protected QueryParamOB GetQuery()
        {
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("PatchCode like '{0}'", PatchCode));
            q.Add(string.Format("Country like '{0}'", Country));
            switch (CommandName.ToString())
            {
                case "DataCount"://记录数量                   
                    break;
                case "WorkerUnReportCount"://未反馈数量
                    q.Add("[WorkerRerpotTime] is null and [DataStatusCode] < 7");
                    break;
                case "WorkerReportCount"://已反馈数量
                    q.Add("[WorkerRerpotTime] > '2024-01-01'");//已提交
                    break;
                case "UnAcceptCount"://未审核
                    q.Add(string.Format("[DataStatusCode] = {0}", EnumManager.CheckFeedStatusCode.待审查));
                    break;
                case "AcceptCount"://已审核
                    q.Add("[AcceptTime] > '2024-01-01'");//已审核
                    break;
                case "AcceptBackCount"://已驳回数量
                    q.Add("[AcceptTime] > '2024-01-01' and [DataStatusCode]=2");
                    break;
            }
            return q;
        }

        //查看统计详细
        protected void RadGridTask_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "DataCount"
                || e.CommandName == "WorkerUnReportCount"
                 || e.CommandName == "WorkerReportCount"
                 || e.CommandName == "UnAcceptCount"
                 || e.CommandName == "AcceptCount"
                 || e.CommandName == "AcceptBackCount"
                )
            {
                ViewState["PatchCode"] = RadGridTask.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PatchCode"];
                ViewState["Country"] = RadGridTask.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Country"];
                ViewState["CommandName"] = e.CommandName;

                ObjectDataSourceCheckFeedBack.SelectParameters.Clear();
                QueryParamOB q = GetQuery();

                try
                {
                    divCheckFeedBack.Style.Add("display", "block");
                    divTask.Style.Add("display", "none");
                    ObjectDataSourceCheckFeedBack.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridCheckFeedBack.CurrentPageIndex = 0;
                    RadGridCheckFeedBack.DataSourceID = ObjectDataSourceCheckFeedBack.ID;

                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "获取监管记录失败！", ex);
                    return;
                }
            }
        }

        //明细查询
        protected void ButtonFind_Click(object sender, EventArgs e)
        {
            ObjectDataSourceCheckFeedBack.SelectParameters.Clear();
            QueryParamOB q = GetQuery();  
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text.Trim().Replace("[", "[[]")));
            }

            try
            {
                ObjectDataSourceCheckFeedBack.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridCheckFeedBack.CurrentPageIndex = 0;
                RadGridCheckFeedBack.DataSourceID = ObjectDataSourceCheckFeedBack.ID;

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取监管记录失败！", ex);
                return;
            }
        }

        //从明细返回到统计
        protected void ButtonRtn_Click(object sender, EventArgs e)
        {
            divCheckFeedBack.Style.Add("display", "none");
            divTask.Style.Add("display", "block");
            ButtonQuery_Click(sender, e);
        }

        //导出
        protected void ButtonOut_Click(object sender, EventArgs e)
        {           
            if (RadGridCheckFeedBack.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("Detail").Visible = false;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("CheckLimit").Visible = false;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("CheckType").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("phone").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("Country").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("GongjijinCase").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("ShebaoUnit").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("SourceTime").Visible = true;
            
            
            
            RadGridCheckFeedBack.PageSize = RadGridCheckFeedBack.MasterTableView.VirtualItemCount;
            RadGridCheckFeedBack.CurrentPageIndex = 0;
            RadGridCheckFeedBack.Rebind();
            RadGridCheckFeedBack.ExportSettings.ExportOnlyData = true;
            RadGridCheckFeedBack.ExportSettings.OpenInNewWindow = true;
            RadGridCheckFeedBack.MasterTableView.ExportToExcel();
        }

        protected void RadGridCheckFeedBack_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
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

        /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="WorkerRerpotTime">个人上报时间</param>
        /// <param name="UpReportDate">区县上报时间</param>
        /// <returns></returns>
        protected string formatCheckList(object WorkerRerpotTime, object UpReportDate, object DataStatusCode)
        {

            int limitDays = 7;//审核时限（天数）
            int WaitDays = 0;//等待审批天数
            int level = 0;//当前所处审批级别：0区县，1市级

            //if (UpReportDate != DBNull.Value)//已到市级审批
            //{
            //    level = 1;
            //    limitDays = 3;
            //    //WaitDays =Convert.ToInt32((DateTime.Now.Date- Convert.ToDateTime(UpReportDate).Date).TotalDays);
            //    WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UpReportDate));
            //}
            //else if (WorkerRerpotTime != DBNull.Value)//处在县级审批
            //{
            //    //WaitDays =Convert.ToInt32((DateTime.Now.Date- Convert.ToDateTime(UnitApplyDate).Date).TotalDays);
            //    WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(WorkerRerpotTime));
            //}

            if (Convert.ToInt32(DataStatusCode) <3)//未申报、已驳回
            {
                return "";
            }

            if (WorkerRerpotTime != DBNull.Value)
            {
                if (UpReportDate == DBNull.Value)
                {
                    WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(WorkerRerpotTime));

                    return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
                  , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
                }
                else
                {
                    return "";
                    //return string.Format("<span {1}>限{0}天,已完成</span>", limitDays,"style='diaplay:block;color:#FF0000;'" );
                }
            }
            else
            {
                return "";
                //return string.Format("<span {1}>限{0}天,尚未开始</span>", limitDays, "style='diaplay:block;color:#FF0000;'");
            }

          
        }

        //导出报表
        protected void ButtonOutReport_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQuery();
            //DataTable dtCheck =CommonDAL.GetDataTable(string.Format("select * from CheckFeedBack where 1=1 {0}",q.ToWhereString()));

            string updateCurUnit = @"
update [dbo].[CheckFeedBack]
set [CheckFeedBack].CurUnit = View_JZS_OneLevel.ENT_Name
from [CheckFeedBack] inner join View_JZS_OneLevel on [CheckFeedBack].CertificateCode = View_JZS_OneLevel.PSN_RegisterNO
where View_JZS_OneLevel.PSN_RegisterNO is not null {0};

update [dbo].[CheckFeedBack]
set [CheckFeedBack].CurUnit = View_JZS_TOW_WithProfession.ENT_Name
from [CheckFeedBack] inner join View_JZS_TOW_WithProfession on [CheckFeedBack].CertificateCode = View_JZS_TOW_WithProfession.PSN_RegisterNO
where View_JZS_TOW_WithProfession.[PSN_RegisteType] < '07' and View_JZS_TOW_WithProfession.PSN_RegisterNO is not null {0};

update [dbo].[CheckFeedBack]
set [CheckFeedBack].CurUnit = jcsjk_zjs.PYDW
from [CheckFeedBack] inner join jcsjk_zjs on [CheckFeedBack].CertificateCode = jcsjk_zjs.ZCZH
where jcsjk_zjs.ZCZH is not null {0};

update [dbo].[CheckFeedBack]
set [CheckFeedBack].CurUnit = zjs_Certificate.ENT_Name
from [CheckFeedBack] inner join [zjs_Certificate] on [CheckFeedBack].CertificateCode = zjs_Certificate.PSN_RegisterNO
where zjs_Certificate.[PSN_RegisteType] < '07' and zjs_Certificate.PSN_RegisterNO is not null {0};

update [dbo].[CheckFeedBack]
set [CheckFeedBack].CurUnit = jcsjk_jls.聘用单位
from [CheckFeedBack] inner join jcsjk_jls on [CheckFeedBack].CertificateCode = jcsjk_jls.注册号
where jcsjk_jls.注册号 is not null {0};
";
            CommonDAL.ExecSQL(string.Format(updateCurUnit,q.ToWhereString()));

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/check_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                string caption = @"
<tr><th colspan='12'>报送部门：（加盖单位印章）</th></tr>
<tr><th rowspan='2'>序号</th><th colspan='6'>专业技术人员</th><th colspan='2'>买卖租借证书单位</th><th colspan='2'>处理结果</th><th rowspan='2'>备注</th></tr>
<tr><th>姓名</th><th>证件号码</th><th>证书编号</th><th>执业资格类别</th><th>注册单位</th><th>实际工作单位</th><th>单位名称</th><th>统一社会信用代码</th><th>自查自纠</th><th>行政处罚</th></tr>";

                string foot = "<tr><th colspan='4'>填表人：</th><th colspan='3'>负责人：</th><th colspan='5'>申报时间：　　　　年　　　　月　　　　日</th></tr>";

                string colHead = @"";
                string colName = @"row_number()  over(ORDER BY (case when DataStatusCode=7 then '已完成' else '' end) desc,sn)\WorkerName\WorkerCertificateCode\CertificateCode\PostTypeName\Unit\CurUnit\''\''\case when DataStatusCode=7 then '已完成' else '' end\''\''";


                CommonDAL.OutputXls(Server.MapPath(saveFile), "DBO.[CheckFeedBack]", q.ToWhereString(), "case when DataStatusCode=7 then '已完成' else '' end desc,sn", colHead, colName, caption, foot);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出综合监管记录汇总信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("综合监管汇总记录下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}