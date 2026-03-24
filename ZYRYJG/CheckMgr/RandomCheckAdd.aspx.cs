using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.IO;

namespace ZYRYJG.CheckMgr
{
    public partial class RandomCheckAdd : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "RandomCheckBase.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            var q = new QueryParamOB();
            RadGridQY.CurrentPageIndex = 0;

            //人员类型
            if (RadComboBoxPersonType.SelectedValue != "")
            {
                q.Add(string.Format("[POSTTYPENAME] like '{0}%'", RadComboBoxPersonType.SelectedValue));
            }

            //申请日期
            if (RadDatePicker_ApplyStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime >= '{0}'", RadDatePicker_ApplyStartDate.SelectedDate.Value));
            }
            if (RadDatePicker_ApplyEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime < '{0}'", RadDatePicker_ApplyEndDate.SelectedDate.Value.AddDays(1)));
            }

            //办理日期
            if (RadDatePicker_NoticeStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("[NoticeDate] >= '{0}'", RadDatePicker_NoticeStartDate.SelectedDate.Value));
            }
            if (RadDatePicker_NoticeEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("[NoticeDate] < '{0}'", RadDatePicker_NoticeEndDate.SelectedDate.Value.AddDays(1)));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }          

            switch (RadComboBoxPersonType.SelectedValue)
            {
                case "二级注册建造师":
                    ObjectDataSource1.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBoxPSN_RegisteType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                    }

                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource1.ID;
                    break;
                case "二级造价工程师":
                    ObjectDataSource3.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBoxEJZJGCS.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxEJZJGCS.SelectedValue));
                    }

                    ObjectDataSource3.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource3.ID;
                    break;
                default://从业人员（三类人、特种作业）
                    ObjectDataSource2.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBox_CongYeApplyType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBox_CongYeApplyType.SelectedValue));
                    }

                    ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource2.ID;
                    break;
            }

            ViewState["RandomCheckAdd_QueryParamOB"] = q;
        }       

        //变换人员类型
        protected void RadComboBoxPersonType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (RadComboBoxPersonType.SelectedValue)
            {
                case "二级注册建造师":
                    RadComboBoxPSN_RegisteType.Visible = true;
                    RadComboBoxEJZJGCS.Visible = false;
                    RadComboBox_CongYeApplyType.Visible = false;
                    break;
                case "二级造价工程师":
                    RadComboBoxPSN_RegisteType.Visible = false;
                    RadComboBoxEJZJGCS.Visible = true;
                    RadComboBox_CongYeApplyType.Visible = false;
                    break;
                default://从业人员（三类人、特种作业）
                    RadComboBoxPSN_RegisteType.Visible = false;
                    RadComboBoxEJZJGCS.Visible = false;
                    RadComboBox_CongYeApplyType.Visible = true;
                    break;
            }          
        }

        //保存筛查对象
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            QueryParamOB q = ViewState["RandomCheckAdd_QueryParamOB"] as QueryParamOB;

            try
            {
                switch (RadComboBoxPersonType.SelectedValue)
                {
                    case "二级注册建造师":
                        CommonDAL.ExecSQL(string.Format(@"
                        INSERT INTO [dbo].[CheckObject]([CheckID],[CheckYear],[PSN_RegisterNo],[PostTypeName],[PSN_Name],[PSN_CertificateNO],[ENT_Name],[ProfessionWithValid],[ApplyType],[ApplyTime],[NoticeDate])
                        select newid(),year(getdate()),PSN_RegisterNO,PostTypeName,PSN_Name,PSN_CertificateNO,ENT_Name,ProfessionWithValid,ApplyType,CONVERT(varchar(10), ApplyTime, 20),CONVERT(varchar(10), NoticeDate, 20)
                        from DBO.[TJ_JZS_TOW_Check] where 1=1 {0} order by {1}", q.ToWhereString(), "[PSN_CertificateNO]"));

                        UIHelp.WriteOperateLog(UserName, UserID, "保存双随机筛查对象", string.Format("抽查年度：{0}，人员类型：{1}，业务类型：{2}。", DateTime.Now.Year, RadComboBoxPersonType.SelectedValue, RadComboBoxPSN_RegisteType.SelectedValue));
                        break;
                    case "二级造价工程师":
                        CommonDAL.ExecSQL(string.Format(@"
                        INSERT INTO [dbo].[CheckObject]([CheckID],[CheckYear],[PSN_RegisterNo],[PostTypeName],[PSN_Name],[PSN_CertificateNO],[ENT_Name],[ProfessionWithValid],[ApplyType],[ApplyTime],[NoticeDate])
                        select newid(),year(getdate()),PSN_RegisterNO,PostTypeName,PSN_Name,PSN_CertificateNO,ENT_Name,ProfessionWithValid,ApplyType,CONVERT(varchar(10), ApplyTime, 20),CONVERT(varchar(10), NoticeDate, 20)
                        from DBO.[TJ_ZJGCS_TOW_Check] where 1=1 {0} order by {1}", q.ToWhereString(), "[PSN_CertificateNO]"));

                        UIHelp.WriteOperateLog(UserName, UserID, "保存双随机筛查对象", string.Format("抽查年度：{0}，人员类型：{1}，业务类型：{2}。", DateTime.Now.Year, RadComboBoxPersonType.SelectedValue, RadComboBoxEJZJGCS.SelectedValue));
                        break;
                    default://从业人员（三类人、特种作业）
                        CommonDAL.ExecSQL(string.Format(@"
                        INSERT INTO [dbo].[CheckObject]([CheckID],[CheckYear],[PSN_RegisterNo],[PostTypeName],[PSN_Name],[PSN_CertificateNO],[ENT_Name],[ProfessionWithValid],[ApplyType],[ApplyTime],[NoticeDate])
                        select newid(),year(getdate()),PSN_RegisterNO,PostTypeName,PSN_Name,PSN_CertificateNO,ENT_Name,ProfessionWithValid,ApplyType,CONVERT(varchar(10), ApplyTime, 20),CONVERT(varchar(10), NoticeDate, 20)
                        from DBO.[TJ_CongYe_Check] where 1=1 {0} order by {1}", q.ToWhereString(), "[PSN_CertificateNO]"));

                        UIHelp.WriteOperateLog(UserName, UserID, "保存双随机筛查对象", string.Format("抽查年度：{0}，人员类型：{1}，业务类型：{2}。", DateTime.Now.Year, RadComboBoxPersonType.SelectedValue, RadComboBox_CongYeApplyType.SelectedValue));
                        break;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存双随机筛查对象信息失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, "保存双随机筛查对象信息成功！", "var isfresh=true;hideIfam(true);");

            //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }
    }
}