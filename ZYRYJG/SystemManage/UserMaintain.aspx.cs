using DataAccess;
using Model;
using System;

namespace ZYRYJG.SystemManage
{
    public partial class UserMaintain : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RadWindowManager1.Skin = UIHelp.Exchange(this.Page.Theme);
                this.TextBoxUserName.Text = UserName;
                UserOB o = UserDAL.GetObject(Convert.ToInt64(UserID));
                this.TextBoxPassWord.Attributes.Add("value", o.UserPwd);

                UIHelp.SetReadOnly(TextBoxUserName, true);
                UIHelp.SetReadOnly(TextBoxPassWord, true);

                if(Request["o"]=="ruopk")//弱密码
                {
                    UIHelp.layerAlert(Page, "您的密码不符合强密码规则，请修改密码。强密码是指长度至少有 8 个字符，不包含全部或部分用户帐户名，至少包含以下四类字符中的三类：大写字母、小写字母、数字，以及键盘上的符号（如 !、@、#）。",5,0);
                }
            }
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string UserIDS = UserID;
            string PassWord = this.TextBoxNewPassWord.Text;

            string strRegex = @"^(?![a-zA-Z]+$)(?![A-Z0-9]+$)(?![A-Z\W_]+$)(?![a-z0-9]+$)(?![a-z\W_]+$)(?![0-9\W_]+$)[a-zA-Z0-9\W_]{8,16}$";
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
            if (re.IsMatch(PassWord) == false)
            {
                UIHelp.layerAlert(Page, "新密码不符合强密码规则，请修改。强密码是指长度至少有 8 个字符，不包含全部或部分用户帐户名，至少包含以下四类字符中的三类：大写字母、小写字母、数字，以及键盘上的符号（如 !、@、#）。", 5, 0);

                return;
            }

            try
            {

                UserDAL.ModifyUserPwd(PassWord, UserIDS);
                UIHelp.WriteOperateLog(UserName, UserID, "保存修正成功", string.Format("保存时间：{0}", DateTime.Now));
                UIHelp.layerAlert(Page, "修改成功！", 6, 2000);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "用户密码修改失败！", ex);
                return;
            }

            Response.Redirect("~/Login.aspx", false);
        }
    }
}