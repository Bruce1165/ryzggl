using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Register
{
    public partial class CertificateNoChange : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Register/NumberChange.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Request["o"]);
                if (o != null)
                {
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                    UIHelp.SetData(EditTable, o, true);
                    switch (o.PSN_RegisteType)
                    {
                        case "01":
                            TextBoxPSN_RegisteType.Text = "初始注册";
                            break;
                        case "02":
                            TextBoxPSN_RegisteType.Text = "变更注册";
                            break;
                        case "03":
                            TextBoxPSN_RegisteType.Text = "延续注册";
                            break;
                        case "04":
                            TextBoxPSN_RegisteType.Text = "增项注册";
                            break;
                        case "05":
                            TextBoxPSN_RegisteType.Text = "重新注册";
                            break;
                        case "06":
                            TextBoxPSN_RegisteType.Text = "遗失补办";
                            break;
                        case "07":
                            TextBoxPSN_RegisteType.Text = "注销";
                            break;
                    }
                    TextBoxPSN_CertificationDate.Text = ((DateTime)o.PSN_CertificationDate).ToString("yyyy-MM-dd");
                    TextBoxPSN_CertificateValidity.Text = ((DateTime)o.PSN_CertificateValidity).ToString("yyyy-MM-dd");
                    TextBoxPSN_RegistePermissionDate.Text = ((DateTime)o.PSN_RegistePermissionDate).ToString("yyyy-MM-dd");

                    UIHelp.SetReadOnly(TextBoxPSN_RegisterCertificateNoNew, false);
                  
                }
            }
        }

        //替换证书编号
        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if(TextBoxPSN_RegisterCertificateNoNew.Text.Trim()=="")
            {
                UIHelp.layerAlert(Page, "请输入新证书编号!");
                return;
            }

            COC_TOW_Person_BaseInfoMDL o = ViewState["COC_TOW_Person_BaseInfoMDL"] as COC_TOW_Person_BaseInfoMDL;
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran,o);
            COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
            tran.Commit();
            ViewState["OldCertificateNo"] = o.PSN_RegisterCertificateNo;
            o.PSN_RegisterCertificateNo = TextBoxPSN_RegisterCertificateNoNew.Text.Trim();
            o.XGR = UserName;
            o.XGSJ = DateTime.Now;
            try
            {
                COC_TOW_Person_BaseInfoDAL.Update(o);
            }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更换证书编号失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "证书编号更换成功", string.Format("更换时间：{0},更新前：{1},更新后：{2}", DateTime.Now, ViewState["OldCertificateNo"], TextBoxPSN_RegisterCertificateNoNew.Text.Trim()));
            UIHelp.layerAlert(Page, "证书编号更换成功！",6,2000);
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }
    }
}