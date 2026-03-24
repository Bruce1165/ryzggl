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
    public partial class ApplyList : BasePage
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        public string ApplyType
        {
            get
            {
                if (ViewState["ApplyType"] == null) return "";
                switch (ViewState["ApplyType"].ToString())
                {
                    case "a":
                        return "增项注册";
                    case "f":
                        return "初始注册";
                    case "c":
                        return "变更注册";
                    case "y":
                        return "延期注册";
                    case "r":
                        return "重新注册";
                    case "b":
                        return "遗失补办";
                    case "z":
                        return "注销";
                    //case "j":
                    //    return "变更注册";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type)
        {
            //string bgzcurl="";
            //if (ViewState["ApplyType"].ToString() == "c")
            //    bgzcurl= "ApplyChange.aspx";//变更注册
            //if (ViewState["ApplyType"].ToString() == "j")
            //    bgzcurl= "ApplyChangePerson.aspx";//变更注册
            switch (type)
            {
                case "增项注册":
                    return "ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "ApplyFirst.aspx";//初始注册
                case "变更注册":
                    return "ApplyChange.aspx";//变更注册
                case "延期注册":
                    return "ApplyContinue.aspx";//延续注册
                case "重新注册":
                    return "ApplyRenew.aspx";//重新注册
                case "遗失补办":
                    return "ApplyReplace.aspx";//遗失补办
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
                if (ApplyType == "重新注册")
                {
                    //有新增按钮
                    ButtonRenew.Visible = true;
                    div_applyDeleteTime.Visible = true;

                    //没有未填写状态
                    ListItem li = RadioButtonListApplyStatus.Items.FindByText("未填写");
                    if (li != null)
                    {
                        RadioButtonListApplyStatus.Items.Remove(li);
                        RadioButtonListApplyStatus.SelectedIndex = 0;
                    }
                }
                if (ApplyType == "延期注册")
                {
                    div_continueTip.Visible = true;
                    div_applyDeleteTime.Visible = true;
                }
                if (ApplyType == EnumManager.ApplyType.变更注册 || ApplyType == EnumManager.ApplyType.遗失补办 || ApplyType == EnumManager.ApplyType.增项注册)
                {
                    div30DayTip.Visible=true;
                    div_applyDeleteTime.Visible = true;
                }
                //if (ApplyType == EnumManager.ApplyType.初始注册 || ApplyType == EnumManager.ApplyType.重新注册 || ApplyType == EnumManager.ApplyType.增项注册 || ApplyType == EnumManager.ApplyType.延期注册)
                //{
                //    div_use.Visible = true;
                //}
                
                LabelPath.Text = (ApplyType == "变更注册" ? "个人信息变更" : ApplyType.Replace("延期注册", "延续注册"));
                LabelTitle.Text = (ApplyType == "变更注册" ? "个人信息变更" : ApplyType.Replace("延期注册", "延续注册"));

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

            if (IfExistRoleID("2") == true)//企业
            {
                if (ViewState["ApplyType"].ToString() == "r")//重新注册,显示自己创建的并且状态位注销的证书
                {
                    q.Add(string.Format("PSN_RegisteType >'06' and cjr='{0}'", UserName));
                }
                else if(ViewState["ApplyType"].ToString() == "z")//注销注册,显示未注销的本企业证书
                {
                    q.Add("PSN_RegisteType < '07' ");
                    q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));
                    div_applyDeleteTime.Visible = true;
                }
                else//显示未注销并且未过期的本企业证书
                {
                    q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));
                    q.Add(string.Format("(PSN_RegisteType < '07' and PSN_CertificateValidity >= '{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
                }
            }
            if (RoleIDs == "0")//个人
            {
                if (ViewState["ApplyType"].ToString() == "r")//重新注册,显示自己身份证的并且状态为注销的
                {
                    q.Add(string.Format("PSN_RegisteType >'06' and PSN_CertificateNO='{0}' and ApplyType='重新注册'", WorkerCertificateCode));
                }
                else if (ViewState["ApplyType"].ToString() == "z")//注销注册,显示未注销的个人证书
                {
                    q.Add("PSN_RegisteType < '07' ");
                    q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
                    div_applyDeleteTime.Visible = true;
                }
                else if (ViewState["ApplyType"].ToString() == "y")//延续注册
                {
                    q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
                    q.Add(string.Format("(PSN_RegisteType < '07' and PSN_CertificateValidity >= '{0}')", DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd")));
                }
                else//显示未注销并且未过期的个人证书
                {
                    q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
                    q.Add(string.Format("(PSN_RegisteType < '07' and PSN_CertificateValidity >= '{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
                }

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
                q.Add(string.Format("ApplyTime <= '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
            }
        

            //申请状态
            switch (RadioButtonListApplyStatus.SelectedValue)
            {
                case "全部":
                    if (ApplyType == "变更注册")
                    {
                        q.Add(string.Format("(ApplyStatus is null or ApplyTypeSub ='个人信息变更')", ApplyType));
                    }
                    else
                    {
                        q.Add(string.Format("(ApplyStatus is null or ApplyType ='{0}')", ApplyType));
                    }
                    
                    //q.Add(string.Format("(ApplyStatus is null or (ApplyTypeSub !='执业企业变更' and ApplyTypeSub!='企业信息变更' and ApplyType='{0}'))", ApplyType));
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
                    //余瑶瑶新增
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
            //if (_ApplyType == "延期注册"
            //     && string.IsNullOrEmpty(_ApplyID) == true
            //     && CheckContinueTimeSpan(_ProfessionList) == false)
            //{
            //    return "";
            //}
            //else
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