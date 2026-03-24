using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using Utility;
using Telerik.Web.UI;

namespace ZYRYJG.EXamManage
{
    public partial class BlackListEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    BlackListOB o = BlackListDAL.GetObject(Convert.ToInt64(Request["o"]));
                    if (o != null)
                    {
                        ViewState["BlackListOB"] = o;
                        if (ValidResourceIDLimit(RoleIDs, "BlackListEdit") == false || Request["v"] == "1")//只读
                        {
                            UIHelp.SetData(TdEdit, o, true);
                            PostSelect1.Enabled = false;
                        }
                        else
                        {
                            UIHelp.SetData(TdEdit, o);
                        }
                        PostSelect1.PostTypeID = o.PostTypeID.ToString();
                        PostSelect1.PostID = o.PostID.ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (PostSelect1.PostID == "")
            {
                UIHelp.layerAlert(Page, "请选择一个岗位工种！");
                return;
            }
            bool isIDCard = Utility.Check.isChinaIDCard(RadTextBoxCertificateCode.Text.Trim());//是否为有效的身份证
            BlackListOB o = ViewState["BlackListOB"] == null ? new BlackListOB() : (BlackListOB)ViewState["BlackListOB"];

            UIHelp.GetData(TdEdit, o);
            o.PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);
            o.PostID = Convert.ToInt32(PostSelect1.PostID);
            try
            {
                if (ViewState["BlackListOB"] == null)//new
                {
                    o.CreatePerson = PersonName;
                    o.CreateTime = DateTime.Now;
                    BlackListDAL.Insert(o);
                }
                else
                {
                    o.ModifyPerson = PersonName;
                    o.ModifyTime = DateTime.Now;
                    BlackListDAL.Update(o);
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存报考黑名单失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, string.Format("保存成功！{0}", isIDCard == false ? "警告：你输入的证件号码不是有效的身份证，如果你输入的确为身份证信息，请仔细检查输入是否有错！" : ""));
            ViewState["BlackListOB"] = o;
            ClientScript.RegisterStartupScript(this.GetType(), "isfresh", "var isfresh=true;", true);
        }


        protected void ButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (PostSelect1.PostID == "")
            {
                UIHelp.layerAlert(Page, "请选择一个岗位工种！");
                return;
            }
            bool isIDCard = Utility.Check.isChinaIDCard(RadTextBoxCertificateCode.Text.Trim());//是否为有效的身份证
            BlackListOB o =  new BlackListOB();

            UIHelp.GetData(TdEdit, o);
            o.PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);
            o.PostID = Convert.ToInt32(PostSelect1.PostID);
            try
            {
                o.CreatePerson = PersonName;
                o.CreateTime = DateTime.Now;
                BlackListDAL.Insert(o);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "另存报考黑名单失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, string.Format("另存成功！{0}", isIDCard == false ? "警告：你输入的证件号码不是有效的身份证，如果你输入的确为身份证信息，请仔细检查输入是否有错！" : ""));
            ViewState["BlackListOB"] = o;
            ClientScript.RegisterStartupScript(this.GetType(), "isfresh", "var isfresh=true;", true);
        }
    }
}
