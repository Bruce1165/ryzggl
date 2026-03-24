using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Unit
{
    public partial class ApplyListUnitCancel : BasePage
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        public string ApplyType
        {
            get
            {
                return "注销";
            }
        }

        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type)
        {
            switch (type)
            {
                case "注销":
                    return "ApplyCancel.aspx";//注销注册
                default:
                    return "#";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // 验证工商信息是否通过                
                if (IfExistRoleID("2") == true)//企业
                {
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1 || string.IsNullOrEmpty(o.ENT_City)==true)
                    {
                        Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");
                    }
                }

                ViewState["ApplyType"] = Request["o"];              

                //指定查询状态
                if (string.IsNullOrEmpty(Request["s"]) == false)
                {
                    ListItem li = RadioButtonListApplyStatus.Items.FindByText(Request["s"]);
                    if (li != null)
                    {
                        li.Selected = true;
                        RadioButtonListApplyStatus.SelectedIndex = RadioButtonListApplyStatus.Items.IndexOf(li);
                    }
                }
                //指定查询申报日期
                if (string.IsNullOrEmpty(Request["b"]) == false)
                {
                    RadDatePickerGetDateStart.DbSelectedDate = Request["b"];
                }
                if (string.IsNullOrEmpty(Request["e"]) == false)
                {
                    RadDatePickerGetDateEnd.DbSelectedDate = Request["e"];
                }

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
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();

            //注销注册,显示未注销的本企业证书
            q.Add("PSN_RegisteType < '07' ");
            q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));

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
                q.Add(string.Format("ApplyTime <= '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
            }

            //申请状态
            switch (RadioButtonListApplyStatus.SelectedValue)
            {
                case "全部":
                    q.Add(string.Format("(ApplyStatus is null or ApplyType ='{0}')", ApplyType));
                    break;
                case "未填写":
                    q.Add("ApplyStatus is null");
                    break;
                case EnumManager.ApplyStatus.未申报:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.未申报));
                    q.Add(string.Format("ApplyType = '{0}'", ApplyType));
                    break;
                case EnumManager.ApplyStatus.已申报:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已申报));
                    q.Add(string.Format("ApplyType = '{0}'", ApplyType));
                    break;
                case EnumManager.ApplyStatus.已受理:
                    q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}')"
                        , EnumManager.ApplyStatus.已受理
                        , EnumManager.ApplyStatus.已上报
                        , EnumManager.ApplyStatus.已收件
                        , EnumManager.ApplyStatus.已审查
                        , EnumManager.ApplyStatus.已决定
                        , EnumManager.ApplyStatus.已公示));
                    q.Add(string.Format("ApplyType = '{0}'", ApplyType));
                    break;
                case EnumManager.ApplyStatus.已驳回:
                    q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已驳回));
                    q.Add(string.Format("ApplyType = '{0}'", ApplyType));
                    break;
                case EnumManager.ApplyStatus.待确认:
                    q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.待确认));
                    q.Add(string.Format("ApplyType = '{0}'", ApplyType));
                    break;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        public string BindApplyStatus(string applyStatus)
        {
            switch (applyStatus)
            {
                case Model.EnumManager.ApplyStatus.已受理:
                case Model.EnumManager.ApplyStatus.已上报:
                case Model.EnumManager.ApplyStatus.已收件:
                case Model.EnumManager.ApplyStatus.已审查:
                case Model.EnumManager.ApplyStatus.已决定:
                case Model.EnumManager.ApplyStatus.已公示:
                case Model.EnumManager.ApplyStatus.已公告:
                    return "已受理";
                case Model.EnumManager.ApplyStatus.待确认:
                    return "待确认";
                default:
                    return applyStatus;
            }
        }


        private bool CheckContinueTimeSpan(string _ProfessionList)
        {
            string[] zy = _ProfessionList.Split(',');
            DateTime ValidDate;
            foreach (var item in zy)
            {
                ValidDate = Convert.ToDateTime(item.Split(':')[1]);
                if (ValidDate < DateTime.Now.AddDays(Model.EnumManager.ContinueTime.结束时间)
                    || ValidDate > DateTime.Now.AddDays(Model.EnumManager.ContinueTime.开始时间)
                )
                {
                    continue;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public string formatApplyShow(string _ApplyType, string _ApplyID, string _ApplyStatus, string _ProfessionList)
        {

            if (string.IsNullOrEmpty(_ApplyID) == true || _ApplyStatus == "未申报")
            {
                return "申报";
            }
            else if (string.IsNullOrEmpty(_ApplyID) == true || _ApplyStatus == "未填写")
            {
                return "填写";
            }
            else
            {
                return "详细";
            }
        }
    }
}