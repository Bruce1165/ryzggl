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
    public partial class CheckTaskConfirm : BasePage
    {
        //作业：每日人员监管系统获取人员监管原数据中，有一步“综合监管证书注销扫描”，更新个人是否已完成整改（注销算为整改完成）
        protected object PatchCode
        {
            get
            {
                return ViewState["PatchCode"];
            }
        }

        protected object Country
        {
            get
            {
                return ViewState["Country"];
            }
        }
        protected object CommandName
        {
            get
            {
                return ViewState["CommandName"];
            }
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ini",
                string.Format(@"$(function () {{
                    $('#{0} input').each(function () {{
                          $(this).click(function () {{
                              var TextBoxApplyCheckRemark = $('#{1}');

                            if ($(this).val() == '通过') {{

                                TextBoxApplyCheckRemark.val('允许通过');
                                TextBoxApplyCheckRemark.attr('readonly', 'true');
                            }}
                            else {{
                                TextBoxApplyCheckRemark.val('决定未通过');
                                TextBoxApplyCheckRemark.removeAttr('readonly');
                            }}
                        }});
                    }});
                  }})", RadioButtonListDecide.ClientID, TextBoxConfirmDesc.ClientID)
                , true);
            if (!IsPostBack)
            {

                ButtonQuery_Click(sender, e);
            }
        }
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            

            QueryParamOB q = new QueryParamOB();

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

            if (RadioButtonListGroupType.SelectedValue == "全市")
            {
                ObjectDataSourceTaskCity.SelectParameters.Clear();
                ObjectDataSourceTaskCity.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridTask.CurrentPageIndex = 0;
                RadGridTask.DataSourceID = ObjectDataSourceTaskCity.ID;
            }
            else
            {
                ObjectDataSourceTask.SelectParameters.Clear();
                ObjectDataSourceTask.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridTask.CurrentPageIndex = 0;
                RadGridTask.DataSourceID = ObjectDataSourceTask.ID;
            }
        }

        protected QueryParamOB GetQuery()
        {
            QueryParamOB q = new QueryParamOB();
            switch (CommandName.ToString())
            {
                case "DataCount"://记录数量
                    break;
                case "WorkerUnReportCount"://个人未反馈数量
                    q.Add("[WorkerRerpotTime] is null and [DataStatusCode] < 7");
                    break;
                case "BackCount"://已驳回数量
                    q.Add(string.Format("[DataStatusCode] = {0}", EnumManager.CheckFeedStatusCode.已驳回));
                    break;
                case "UnAcceptCount"://区县未审核
                    q.Add("[DataStatusCode]=3");//已审核
                    break;
                case "UnCheckCount"://市级未复审
                    q.Add("[DataStatusCode]=4");//已审核
                    break;
                case "UnConfirmCheckPassCount"://待市级决定（市级复审通过）
                    q.Add(string.Format("[DataStatusCode] = {0} and [CheckResult]='通过'", EnumManager.CheckFeedStatusCode.待决定));
                    break;
                case "UnConfirmCheckBackCount"://待市级决定（市级复审不通过）
                    q.Add(string.Format("[DataStatusCode] = {0} and [CheckResult]='不通过'", EnumManager.CheckFeedStatusCode.待决定));
                    break;
                case "ConfirmCount"://市级已决定
                    q.Add("[ConfirmTime] > '2024-01-01'");
                    break;
                case "CanceledCount"://整改完成（已注销）
                    q.Add("[DataStatusCode] >0 and [DataStatusCode] <7 and [CertCancelCheckResult] =1");
                    break;
            }
            q.Add(string.Format("PatchCode like '{0}'", PatchCode));
            if (Country.ToString() != "全市")
            {
                q.Add(string.Format("Country like '{0}'", Country));
            }
            return q;
        }

        protected void RadGridTask_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "DataCount"
                || e.CommandName == "WorkerUnReportCount"
                || e.CommandName == "BackCount"
                || e.CommandName == "UnAcceptCount"
                || e.CommandName == "UnCheckCount"
                || e.CommandName == "UnConfirmCheckPassCount"
                || e.CommandName == "UnConfirmCheckBackCount"
                || e.CommandName == "ConfirmCount"
                || e.CommandName == "CanceledCount"
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
                    if (CommandName.ToString() == "UnConfirmCheckPassCount" || CommandName.ToString() == "UnConfirmCheckBackCount" || CommandName.ToString() == "CanceledCount" || CommandName.ToString() == "WorkerUnReportCount")
                    {
                        TableConfirm.Visible = true;
                        if (CommandName.ToString() == "CanceledCount")
                        {
                            RadioButtonListDecide.Enabled = false;
                            
                        }
                        else
                        {
                            RadioButtonListDecide.Enabled = true;
                        }
                    }
                    else
                    {
                        TableConfirm.Visible = false;
                    }
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

        protected void ButtonRtn_Click(object sender, EventArgs e)
        {
            divCheckFeedBack.Style.Add("display", "none");
            divTask.Style.Add("display", "block");
            ButtonQuery_Click(sender, e);
        }

        //市级批量决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            try
            {
                CheckFeedBackDAL.Confirm(ObjectDataSourceCheckFeedBack.SelectParameters["filterWhereString"].DefaultValue, UserName, RadioButtonListDecide.SelectedValue, TextBoxConfirmDesc.Text.Trim());

                UIHelp.WriteOperateLog(UserName, UserID, "市级批量决定监管反馈", string.Format("批次号:{0}，{1}，记录数量：{2}，决定结果：{3}。", PatchCode, Country, RadGridCheckFeedBack.MasterTableView.VirtualItemCount, RadioButtonListDecide.SelectedValue));
                UIHelp.ParentAlert(Page, "批量决定成功！", true);
                ButtonFind_Click(sender, e);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "市级批量决定监管反馈失败！", ex);
                return;
            }
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
            //RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("CheckLimit").Visible = false;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("CheckType").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("phone").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("Country").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("GongjijinCase").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("ShebaoUnit").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("SourceTime").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("AcceptTime").Visible = true;
            RadGridCheckFeedBack.MasterTableView.Columns.FindByUniqueName("CheckTime").Visible = true;



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

        //导出报表
        protected void ButtonOutReport_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQuery();
            //DataTable dtCheck =CommonDAL.GetDataTable(string.Format("select * from CheckFeedBack where 1=1 {0}",q.ToWhereString()));

//            string updateCurUnit = @"
//update [dbo].[CheckFeedBack]
//set [CheckFeedBack].CurUnit = View_JZS_OneLevel.ENT_Name
//from [CheckFeedBack] inner join View_JZS_OneLevel on [CheckFeedBack].CertificateCode = View_JZS_OneLevel.PSN_RegisterNO
//where View_JZS_OneLevel.PSN_RegisterNO is not null {0};
//
//update [dbo].[CheckFeedBack]
//set [CheckFeedBack].CurUnit = View_JZS_TOW_WithProfession.ENT_Name
//from [CheckFeedBack] inner join View_JZS_TOW_WithProfession on [CheckFeedBack].CertificateCode = View_JZS_TOW_WithProfession.PSN_RegisterNO
//where View_JZS_TOW_WithProfession.[PSN_RegisteType] < '07' and View_JZS_TOW_WithProfession.PSN_RegisterNO is not null {0};
//
//update [dbo].[CheckFeedBack]
//set [CheckFeedBack].CurUnit = jcsjk_zjs.PYDW
//from [CheckFeedBack] inner join jcsjk_zjs on [CheckFeedBack].CertificateCode = jcsjk_zjs.ZCZH
//where jcsjk_zjs.ZCZH is not null {0};
//
//update [dbo].[CheckFeedBack]
//set [CheckFeedBack].CurUnit = zjs_Certificate.ENT_Name
//from [CheckFeedBack] inner join [zjs_Certificate] on [CheckFeedBack].CertificateCode = zjs_Certificate.PSN_RegisterNO
//where zjs_Certificate.[PSN_RegisteType] < '07' and zjs_Certificate.PSN_RegisterNO is not null {0};
//
//update [dbo].[CheckFeedBack]
//set [CheckFeedBack].CurUnit = jcsjk_jls.聘用单位
//from [CheckFeedBack] inner join jcsjk_jls on [CheckFeedBack].CertificateCode = jcsjk_jls.注册号
//where jcsjk_jls.注册号 is not null {0};
//";
//            CommonDAL.ExecSQL(string.Format(updateCurUnit, q.ToWhereString()));

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
                string colName = @"row_number()  over(ORDER BY (case when DataStatusCode=7 then '已完成' else '' end) desc,sn)\WorkerName\WorkerCertificateCode\CertificateCode\PostTypeName\Unit\ShebaoUnit\''\''\case when DataStatusCode=7 then '已完成' else '' end\''\''";


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

            downurl.InnerHtml = string.Format("<a href='{0}'>文件已生成：综合监管汇总记录下载</a>", UIHelp.AddUrlReadParam(saveFile.Replace("~", "..")));
            //UIHelp.layerAlertWithHtml(Page, string.Format("<a href='{0}'>综合监管汇总记录下载</a>", saveFile.Replace("~","..")));
        }
    }
}