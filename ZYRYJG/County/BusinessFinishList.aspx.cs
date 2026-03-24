using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.County
{
    //代办业务列表
    public partial class BusinessFinishList : BasePage
    {
        /// <summary>
        /// 异常注册记录中身份证列表（重点监管人员）
        /// </summary>
        DataTable dtYCZC_sfz
        {
            get
            {
                if (Cache["dtYCZC"] == null)
                {
                    string sql = @"select distinct [COC_TOW_Person_BaseInfo].[PSN_CertificateNO] FROM [dbo].[LockJZS]
                          inner join [COC_TOW_Person_BaseInfo] on [LockJZS].[PSN_ServerID] = [COC_TOW_Person_BaseInfo].[PSN_ServerID]";


                    DataTable dtYCZC = CommonDAL.GetDataTable(sql);
                    DataColumn[] c = new DataColumn[1];
                    c[0] = dtYCZC.Columns[0];
                    dtYCZC.PrimaryKey = c;
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "dtYCZC", dtYCZC, 12);

                    return dtYCZC;
                }
                else
                {
                    return (DataTable)Cache["dtYCZC"];
                }
            }
        }

        /// <summary>
        /// 检查身份证是否在重点监管身份证列表中
        /// </summary>
        /// <param name="sfz">身份证编号</param>
        /// <returns>true：是重点检查对象，false：不是重点检查对象</returns>
        protected bool CheckYCZC_SFZ(string sfz)
        {
            if (dtYCZC_sfz.Rows.Find(sfz) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重点监管企业（组织机构代码）
        /// </summary>
        DataTable dtUnitWatch
        {
            get
            {
                if (Cache["dtUnitWatch"] == null)
                {
                    string sql = @"SELECT substring([CreditCode],9,9)  FROM [dbo].[UnitWatch]  where [ValidEnd]>getdate() and len([CreditCode])=18";

                    DataTable dtUnitWatch = CommonDAL.GetDataTable(sql);
                    DataColumn[] c = new DataColumn[1];
                    c[0] = dtUnitWatch.Columns[0];
                    dtUnitWatch.PrimaryKey = c;
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "dtUnitWatch", dtUnitWatch, 12);

                    return dtUnitWatch;
                }
                else
                {
                    return (DataTable)Cache["dtUnitWatch"];
                }
            }
        }

        /// <summary>
        /// 检查组织机构代码是否在重点监管企业列表中
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <returns>true：是重点检查企业，false：不是重点检查企业</returns>
        protected bool CheckUnitWatch(string ZZJGDM)
        {
            if (dtUnitWatch.Rows.Find(ZZJGDM) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/AgencyFinish.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //申报类型
                Telerik.Web.UI.RadComboBoxItem f = RadComboBoxPSN_RegisteType.Items.FindItemByValue(Request["type"]);
                if (f != null)
                {
                    f.Selected = true;
                }

                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请修改输入信息。");
                return;
            }

            ObjectDataSource1.SelectParameters.Clear();

            var q = new QueryParamOB();

            switch (RadComboBoxPSN_RegisteType.SelectedValue)
            {
                case "全部":
                    break;
                case "个人信息变更":
                case "执业企业变更":
                    q.Add(string.Format("ApplyTypeSub = '{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
                    break;
                default:
                    q.Add(string.Format("ApplyType = '{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
                    break;
            }            

            if (RadioButtonListCheckResult.SelectedValue != "")
            {
                q.Add(string.Format("(([GetMan]= '{0}' and [GetResult] ='{1}') or ([ExamineMan]= '{0}' and [ExamineResult]='{1}') or ([CheckMan]= '{0}' and CheckResult ='{1}') or ([ConfirmMan]= '{0}' and [ConfirmResult]='{1}') )", UserName, RadioButtonListCheckResult.SelectedValue));
            }
            else
            {
                q.Add(string.Format("([GetMan]= '{0}' or [ExamineMan]= '{0}' or [CheckMan]= '{0}' or [ConfirmMan]= '{0}')", UserName));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义文本查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            if (RadDatePickerStart.SelectedDate.HasValue == true)//自定义开始时间查询项
            {
                q.Add(string.Format("{0} >= '{1}'", RadComboBoxDateType.SelectedValue, RadDatePickerStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerEnd.SelectedDate.HasValue == true)//自定义截止时间查询项
            {
                q.Add(string.Format("{0} < '{1}'", RadComboBoxDateType.SelectedValue, RadDatePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }
            

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type, string bgzc)
        {
            string URL = "";
            if (!string.IsNullOrEmpty(bgzc))
            {
                if (bgzc == "个人信息变更")
                    URL = "../Unit/ApplyChange.aspx";
                if (bgzc == "执业企业变更")
                    URL = "../Unit/ApplyChangePersonnel.aspx";
            }
            switch (type)
            {
                case "增项注册":
                    return "../Unit/ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "../Unit/ApplyFirstAdd.aspx";//初始注册
                case "变更注册":
                    return URL;//变更注册
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
        
    }
}