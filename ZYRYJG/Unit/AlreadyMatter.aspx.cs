using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.Unit
{
    public partial class AlreadyMatter : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);               
                
            }
        }

        protected string ApplyTypeParam(string ApplyType)
        {
            switch (ApplyType)
            {
                case "初始注册":
                    return "f";
                case "个人信息变更":
                    return "g";
                case "企业信息变更":
                    return "x";
                case "执业企业变更":
                    return "q";
                case "延期注册":
                    return "y";
                case "遗失补办":
                    return "b";
                case "增项注册":
                    return "a";
                case "重新注册":
                    return "r";
                case "注销注册":
                    return "z";
                default:
                    return "";
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
            //分别计算出每个事项由多少
            DataTable dt = DataAccess.ApplyDAL.GetApplyGroupByNot(UserID
                , RadDatePickerGetDateStart.SelectedDate.HasValue == false ? DateTime.Now.AddYears(-100) : RadDatePickerGetDateStart.SelectedDate.Value
                , RadDatePickerGetDateEnd.SelectedDate.HasValue == false ? DateTime.Now : RadDatePickerGetDateEnd.SelectedDate.Value);

            RadGridData.DataSource = dt;
            RadGridData.DataBind();
          
        }
    }
}