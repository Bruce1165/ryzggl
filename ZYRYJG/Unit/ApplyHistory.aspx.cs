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
    public partial class ApplyHistory : BasePage
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
                    case "g":
                        return "个人信息变更";
                    case "x":
                        return "企业信息变更";
                    case "q":
                        return "执业企业变更";
                    case "y":
                        return "延期注册";
                    case "r":
                        return "重新注册";
                    case "b":
                        return "遗失补办";
                    case "z":
                        return "注销";
                    default:
                        return "";
                }
            }
        }

       
        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type, string typesub)
        {

            if (!string.IsNullOrEmpty(typesub))
            {
                switch (typesub)
                {
                    case "个人信息变更":
                        return "../Unit/ApplyChange.aspx";
                    case "执业企业变更":
                        return "../Unit/ApplyChangePersonnel.aspx";
                    case "企业信息变更":
                        return "../Unit/ApplyChangeEnterprise.aspx";
                }
            }
            switch (type)
            {
                case "增项注册":
                    return "../Unit/ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "../Unit/ApplyFirstAdd.aspx";//初始注册
                case "延期注册":
                    return "../Unit/ApplyContinue.aspx";//延期注册
                case "重新注册":
                    return "../Unit/ApplyRenew.aspx";//重新注册
                case "遗失补办":
                    return "../Unit/ApplyReplace.aspx";//遗失补办
                case "注销":
                    return "../Unit/ApplyCancel.aspx";//注销注册
                default:
                    return "#";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ApplyType"] = Request["o"];

                //指定查询申报日期
                if (string.IsNullOrEmpty(Request["b"]) == false)
                {
                    RadDatePickerGetDateStart.DbSelectedDate = Request["b"];
                }
                if (string.IsNullOrEmpty(Request["e"]) == false)
                {
                    RadDatePickerGetDateEnd.DbSelectedDate = Request["e"];
                }

                //申报类型
                Telerik.Web.UI.RadComboBoxItem f= RadComboBoxPSN_RegisteType.Items.FindItemByValue(ApplyType);
                if (f != null)
                {
                    f.Selected = true;
                }

                //指定查询状态
                if (string.IsNullOrEmpty(Request["s"]) == false)
                {
                    Telerik.Web.UI.RadComboBoxItem li = RadComboBoxApplyStatus.Items.FindItemByText(Request["s"]);
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }

                ButtonSearch_Click(sender, e);
                if (string.IsNullOrEmpty(Request["c"]) == false)
                {
                    if (Request["i"] == "执业企业变更")
                    {
                        UIHelp.layerAlert(Page, "申报成功！请联系原、现聘用单位在线审核确认", 6, 9000);
                    }
                    else {
                        UIHelp.layerAlert(Page, "申报成功！", 6, 9000);
                    }
                    //UIHelp.Alert(Page, "11111");
                    
                }
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
            if (IfExistRoleID("2")==true)//企业
            {
                string sqlwhere = string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_'", ZZJGDM);
                sqlwhere = string.Format(sqlwhere + "or OldEnt_QYZJJGDM = '{0}' or OldEnt_QYZJJGDM like '________{0}_'", ZZJGDM);
                //if (RadComboBoxPSN_RegisteType.SelectedValue != "全部")
                //{
                //    if (RadComboBoxPSN_RegisteType.SelectedValue == "执业企业变更")
                //    {
                //        sqlwhere = string.Format(sqlwhere + "or OldEnt_QYZJJGDM = '{0}' or OldEnt_QYZJJGDM like '________{0}_'", ZZJGDM);
                //    }
                //}
                sqlwhere = sqlwhere + ")";
                q.Add(sqlwhere);
            }
            if (IfExistRoleID("0")==true)//个人
            {
                q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //申报日期
             if(RadDatePickerGetDateStart.SelectedDate.HasValue == true )
             {
                  q.Add(string.Format("ApplyTime >= '{0}'",RadDatePickerGetDateStart.SelectedDate.Value));
             }
             if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
             {
                  q.Add(string.Format("ApplyTime < '{0}'",RadDatePickerGetDateEnd.SelectedDate.Value.AddDays(1)));
             }  

           
            //申请类型
             if (RadComboBoxPSN_RegisteType.SelectedValue != "全部" )
            {
                if (RadComboBoxPSN_RegisteType.SelectedValue == "个人信息变更"
                    || RadComboBoxPSN_RegisteType.SelectedValue == "执业企业变更"
                    || RadComboBoxPSN_RegisteType.SelectedValue == "企业信息变更"
                    )
                {
                    q.Add(string.Format("ApplyTypesub like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                }
                else
                {
                    q.Add(string.Format("ApplyType  like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                }
            }

             //申请状态
             switch (RadComboBoxApplyStatus.SelectedValue)
             {
                 case EnumManager.ApplyStatus.未申报:
                     q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.未申报));
                     break;
                case EnumManager.ApplyStatus.待确认:
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.待确认));
                    break;
                case EnumManager.ApplyStatus.已申报:
                     q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已申报));
                     break;
                 case EnumManager.ApplyStatus.已受理:
                     q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}')"
                         , EnumManager.ApplyStatus.已受理
                         , EnumManager.ApplyStatus.已上报
                         , EnumManager.ApplyStatus.已收件
                         , EnumManager.ApplyStatus.已审查
                         , EnumManager.ApplyStatus.已决定
                         , EnumManager.ApplyStatus.已公示));
                     break;
                 case EnumManager.ApplyStatus.已驳回:
                     q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已驳回));
                     break;
                 case "已办结":
                     //1、初始注册，编号后办结；
                     //2、其他注册公告时间不为空即表示已办结。
                     q.Add("((ApplyType='初始注册' and CodeDate > '2022-01-01') or (ApplyType<>'初始注册' and NoticeDate > '2022-01-01'))");
                     break;
             }
            
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            Telerik.Web.UI.GridSortExpression sortStr2 = new Telerik.Web.UI.GridSortExpression();
            sortStr2.FieldName = "ApplyTime";
            sortStr2.SortOrder = Telerik.Web.UI.GridSortOrder.Descending;
            RadGridQY.MasterTableView.SortExpressions.AddSortExpression(sortStr2);

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
                    return "已受理";
                case Model.EnumManager.ApplyStatus.已公告:
                    return "已办结";
                default:
                    return applyStatus;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applyStatus">申请状态</param>
        /// <param name="applyType">申请类型</param>
        /// <param name="ConfirmResult">决定结果</param>
        /// <param name="CodeMan">编号人</param>
        /// <returns></returns>
        public string BindApplyStatus(string applyStatus, string applyType, string ConfirmResult, object CodeMan)
        {
            switch (applyStatus)
            {
                case Model.EnumManager.ApplyStatus.已受理:
                case Model.EnumManager.ApplyStatus.已上报:
                case Model.EnumManager.ApplyStatus.已收件:
                case Model.EnumManager.ApplyStatus.已审查:
                case Model.EnumManager.ApplyStatus.已决定:
                case Model.EnumManager.ApplyStatus.已公示:
                    return "已受理";
                case Model.EnumManager.ApplyStatus.已公告:
                    switch(applyType)
                    {
                        case EnumManager.ApplyType.初始注册:
                        case EnumManager.ApplyType.重新注册:
                        case EnumManager.ApplyType.遗失补办:
                            if (ConfirmResult == "不通过")
                            {
                                return "已办结";
                            }
                            else
                            {
                                if (CodeMan == DBNull.Value)//未编号
                                    return "已受理";
                                else
                                    return "已办结";//已编号
                            }
                        default:
                            return "已办结";
                    }                    
                default:
                    return applyStatus;
            }
        }
        
    }
}