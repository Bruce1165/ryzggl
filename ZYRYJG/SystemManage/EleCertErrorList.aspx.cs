using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using DataAccess;
using Model;

namespace ZYRYJG.SystemManage
{
    public partial class EleCertErrorList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
                ButtonSearchHalf_Click(sender, e);
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
            ClearGridSelectedKeys(RadGridOperateLog);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxCertNo.Text != "")//证书编号
            {
                q.Add(string.Format("CertNo like '%{0}%'", RadTextBoxCertNo.Text.Trim()));
            }
            if (RadTextBoxErrorMessage.Text != "")//内容详细
            {
                q.Add(string.Format("ErrorMessage like '%{0}%'", RadTextBoxErrorMessage.Text.Trim()));
            }

            if (RadComboBoxCertType.SelectedValue != "")// 证书类型
            {
                q.Add(string.Format(" CertType like '{0}%'", RadComboBoxCertType.SelectedValue));
            }
             if (RadComboBoxStepName.SelectedValue != "")// 环节
            {
                q.Add(string.Format(" StepName like '{0}%'", RadComboBoxStepName.SelectedValue));
            }
            
            //操作时间
            if (datePickerFrom.SelectedDate.HasValue)
            {
                q.Add(string.Format(" DoTime>='{0}'", datePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (datePickerEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("DoTime<'{0}'", datePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridOperateLog.CurrentPageIndex = 0;
            RadGridOperateLog.DataSourceID = ObjectDataSource1.ID;
        }

        //清空日志
        protected void ButtonClearLog_Click(object sender, EventArgs e)
        {
            try
            {
                CommonDAL.ExecSQL("truncate table [dbo].[EleCertError]");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "清空电子证书签章日志失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "清空电子证书签章日志", "");
            ButtonSearch_Click(sender, e);
        }

        protected void RadGridOperateLog_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridOperateLog, "CertNo");
        }

        protected void RadGridOperateLog_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridOperateLog, "CertNo");
        }

        //停止尝试勾选记录的签章
        protected void ButtonStopTry_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridOperateLog, "CertNo");//更新选择状态
            if (IsGridSelected(RadGridOperateLog) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string filterString = "";//过滤条件
            if (GetGridIfCheckAll(RadGridOperateLog) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" {0} and CertNo not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridOperateLog));
                else//包含
                    filterString = string.Format(" {0} and CertNo in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridOperateLog));
            }

            DataTable dt = CommonDAL.GetDataTable(string.Format("select * from [EleCertError] where 1=1 {0}", filterString));
            try
            {
                foreach (DataRow r in dt.Rows)
                {

                    if (r["CertType"].ToString() == "二级注册建造师")
                    {
                        CommonDAL.ExecSQL(string.Format("update [COC_TOW_Person_BaseInfo] set [ApplyCATime]=[XGSJ],[SendCATime]=[XGSJ],[SignCATime]=[XGSJ]  where [PSN_RegisterNO]='{0}' and [ReturnCATime] is null", r["CertNo"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [CERTIFICATE] set [ApplyCATime]=[MODIFYTIME],[SendCATime]=[MODIFYTIME],[SignCATime]=[MODIFYTIME]  where [CERTIFICATECODE]='{0}' and [ReturnCATime] is null", r["CertNo"]));
                    }

                    CommonDAL.ExecSQL(string.Format("update [dbo].[EleCertError] set [ErrorMessage]='【{0}:停止尝试签章】' +[ErrorMessage] where [CertNo]='{1}' and [ErrorMessage] not like '%停止尝试签章%'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), r["CertNo"]));
                    UIHelp.layerAlert(Page, string.Format("成功对{0}本证书设置停止尝试签章",dt.Rows.Count));
                }
            }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "设置停止尝试签章失败！", ex);
            }
        }

        //重新生成电子证书
        protected void ButtonReNew_Click(object sender, EventArgs e)
        {
//            string sql = @"update [dbo].[CERTIFICATE]
//                            set [ApplyCATime] =null
//                            where [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null;
//                            update [dbo].[COC_TOW_Person_BaseInfo]
//                            set [ApplyCATime] =null
//                            where [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null;
//                            update [dbo].[zjs_Certificate]
//                            set [ApplyCATime] =null
//                            where [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null;";
//            CommonDAL.ExecSQL(sql);

//            ButtonSearchHalf_Click(sender, e);
        }

        //查询电子证书中途失败证书（不继续进行了）
        protected void ButtonSearchHalf_Click(object sender, EventArgs e)
        {
            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxCertNo.Text != "")//证书编号
            {
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertNo.Text.Trim()));
            }
            if (RadTextBoxErrorMessage.Text != "")//内容详细
            {
                q.Add(string.Format("EleCertErrDesc like '%{0}%'", RadTextBoxErrorMessage.Text.Trim()));
            }

            if (RadComboBoxCertType.SelectedValue != "")// 证书类型
            {
                q.Add(string.Format(" POSTTYPENAME like '{0}%'", RadComboBoxCertType.SelectedValue));
            }
            if (RadComboBoxStepName.SelectedValue != "")// 环节
            {
                q.Add(string.Format(" EleCertErrStep like '{0}%'", RadComboBoxStepName.SelectedValue));
            }

            //操作时间
            if (datePickerFrom.SelectedDate.HasValue)
            {
                q.Add(string.Format(" EleCertErrTime>='{0}'", datePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (datePickerEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("EleCertErrTime<'{0}'", datePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource2.SelectParameters.Clear();

            ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridHalf.CurrentPageIndex = 0;
            RadGridHalf.DataSourceID = ObjectDataSource2.ID;
        }
    }
}
