using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.zjs
{
    public partial class zjsApplyFirst : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                //验证工商信息是否通过                
                if (IfExistRoleID("2") == true)//企业
                {
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1 || string.IsNullOrEmpty(o.ENT_City) == true)
                    {
                        Response.Write("<script>window.location.href='../Unit/UnitMgr.aspx'</script>");
                    }
                }

                if (RoleIDs == "0")//个人
                {
                    ButtonRenew.Visible = true;
                }

                ButtonQuery_Click(sender,e);
            }
        }

        //查询条件
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();

            if (IfExistRoleID("2") == true)//企业
            {
                    q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));

            }
            if(RoleIDs == "0")//个人
            {
                q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
            }
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //申报日期
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime >= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime <= '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value));
            }

            //申请状态
            switch (RadioButtonListApplyStatus.SelectedValue)
            {
                case "全部":
                    break;
                case EnumManager.ZJSApplyStatus.未申报:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.未申报));
                    break;
                case EnumManager.ZJSApplyStatus.待确认:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.待确认));
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已申报));
                    break;
                case EnumManager.ZJSApplyStatus.已受理:
                    q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' )"
                        , EnumManager.ZJSApplyStatus.已受理
                        , EnumManager.ZJSApplyStatus.已审核
                        , EnumManager.ZJSApplyStatus.已决定
                        , EnumManager.ZJSApplyStatus.已公告));
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已驳回));
                    break;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }


        /// <summary>
        /// 格式化申请单状态
        /// </summary>
        /// <param name="applyStatus">审批状态</param>
        /// <param name="IfCreateCode">是否已经放号</param>
        /// <returns></returns>
        public string BindApplyStatus(string applyStatus, bool IfCreateCode, string ConfirmResult)
        {           
            switch (applyStatus)
            {
                case Model.EnumManager.ZJSApplyStatus.已受理:
                case Model.EnumManager.ZJSApplyStatus.已审核:
                case Model.EnumManager.ZJSApplyStatus.已决定:
                    return "已受理";
                case Model.EnumManager.ZJSApplyStatus.已公告:
                    if (ConfirmResult == "不通过")
                    {
                        return "已办结";
                    }
                    else
                    {
                        if (IfCreateCode == false)//未编号
                            return "已受理";
                        else
                            return "已办结";//已编号
                    }
                default:
                    return applyStatus;
            }
        }

      
    }
}