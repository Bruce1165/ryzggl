using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.zjs
{
    public partial class zjsCertModify : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjsCertMgr.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxPSN_National, "108", "请选择", "");//民族
                UIHelp.FillDropDownListWithTypeName(RadComboBoxPSN_Qualification, "109", "请选择", "");//学历

                zjs_CertificateMDL o = zjs_CertificateDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                if (o != null)
                {
                    ViewState["zjs_CertificateMDL"] = o;
                    //绑定注册信息
                    UIHelp.SetData(EditTable, o, false);
                    switch (o.PSN_RegisteType)
                    {
                        case "01":
                            LabelPSN_RegisteType.Text = "初始注册";
                            break;
                        case "02":
                            LabelPSN_RegisteType.Text = "变更注册";
                            break;
                        case "03":
                            LabelPSN_RegisteType.Text = "延续注册";
                            break;
                        case "07":
                            LabelPSN_RegisteType.Text = "注销";
                            break;
                    }
                    //TextBoxPSN_CertificationDate.Text = o.PSN_CertificationDate.HasValue==false?"":o.PSN_CertificationDate.Value.ToString("yyyy-MM-dd");
                    //TextBoxPSN_CertificateValidity.Text = o.PSN_CertificateValidity.HasValue == false ? "" : o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");
                    //TextBoxPSN_RegistePermissionDate.Text = o.PSN_RegistePermissionDate.HasValue == false ? "" : o.PSN_RegistePermissionDate.Value.ToString("yyyy-MM-dd");

                    //TextBoxPSN_BirthDate.Text = o.PSN_BirthDate.HasValue == false ? "" : o.PSN_BirthDate.Value.ToString("yyyy-MM-dd");

                    ImgCode.Src = UIHelp.ShowFile(UIHelp.ShowFaceImageJZS(o.PSN_RegisterNO, o.PSN_CertificateNO));



                }
            }
        }

        public string GetRegisteTypeCode(string type)
        {
            switch (type)
            {

                case "初始注册":
                    return "01";//初始注册
                case "变更注册":
                    return "02";//变更注册
                case "延续注册":
                    return "03";//延续注册              
                case "注销":
                    return "07";//注销注册
                default:
                    throw new Exception("非法的注册类型。");
            }

        }

       
        //修正
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (RadComboBoxPSN_Qualification.SelectedValue != "")
            {
                if ("博士后，博士，研究生，硕士，本科，大专".Contains(RadComboBoxPSN_Qualification.SelectedValue) == false)
                {
                    UIHelp.layerAlert(Page, "保存失败，考生报考二级造价工程师职业资格考试时，应具备工程经济、工程技术、工程管理类的大学专科及以上学历或学士及以上学位。", 5, 0);
                    return;
                }
            }
            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(TextBoxENT_OrganizationsCode.Text.Trim());
            if (_UnitMDL.ENT_Name != TextBoxENT_Name.Text.Trim())
            {
                UIHelp.layerAlert(Page, "企业名称与社会统一信息用代码不匹配，请检查是否输入有误！");
                return;
            }

            if (UnitDAL.CheckGongShang(TextBoxENT_OrganizationsCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", TextBoxENT_Name.Text));
                return;
            }


            zjs_CertificateMDL ob = (zjs_CertificateMDL)ViewState["zjs_CertificateMDL"];
            zjs_CertificateMDL oldOB = UIHelp.CopyProperties(ob);
            UIHelp.GetData(EditTable, ob);
            ob.PSN_RegisteType = oldOB.PSN_RegisteType;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
           
                if (oldOB.PSN_Name != ob.PSN_Name) sb.Append(string.Format("姓名由“{0}”变为“{1}”；", oldOB.PSN_Name, ob.PSN_Name));
                if (oldOB.PSN_Sex != ob.PSN_Sex) sb.Append(string.Format("性别由“{0}”变为“{1}”；", oldOB.PSN_Sex, ob.PSN_Sex));
                if (oldOB.PSN_BirthDate != ob.PSN_BirthDate) sb.Append(string.Format("出生日期由“{0}”变为“{1}”；", oldOB.PSN_BirthDate.HasValue ? oldOB.PSN_BirthDate.Value.ToString("yyyy-MM-dd") : "", ob.PSN_BirthDate.Value.ToString("yyyy-MM-dd")));
                if (oldOB.PSN_National != ob.PSN_National) sb.Append(string.Format("民族由“{0}”变为“{1}”；", oldOB.PSN_National, ob.PSN_National));
                if (oldOB.PSN_CertificateNO != ob.PSN_CertificateNO) sb.Append(string.Format("证件号码由“{0}”变为“{1}”；", oldOB.PSN_CertificateNO, ob.PSN_CertificateNO));

                if (oldOB.PSN_GraduationSchool != ob.PSN_GraduationSchool) sb.Append(string.Format("毕业院校由“{0}”变为“{1}”；", oldOB.PSN_GraduationSchool, ob.PSN_GraduationSchool));
                if (oldOB.PSN_Specialty != ob.PSN_Specialty) sb.Append(string.Format("所学专业由“{0}”变为“{1}”；", oldOB.PSN_Specialty, ob.PSN_Specialty));
                if (oldOB.PSN_Qualification != ob.PSN_Qualification) sb.Append(string.Format("学历由“{0}”变为“{1}”；", oldOB.PSN_Qualification, ob.PSN_Qualification));
                if (oldOB.PSN_GraduationTime != ob.PSN_GraduationTime) sb.Append(string.Format("毕业时间由“{0}”变为“{1}”；", oldOB.PSN_GraduationTime.HasValue ? oldOB.PSN_GraduationTime.Value.ToString("yyyy-MM-dd") : "", ob.PSN_GraduationTime.Value.ToString("yyyy-MM-dd")));
                if (oldOB.PSN_MobilePhone != ob.PSN_MobilePhone) sb.Append(string.Format("手机由“{0}”变为“{1}”；", oldOB.PSN_MobilePhone, ob.PSN_MobilePhone));
                if (oldOB.PSN_Email != ob.PSN_Email) sb.Append(string.Format("电子邮件由“{0}”变为“{1}”；", oldOB.PSN_Email, ob.PSN_Email));

                if (oldOB.ENT_Name != ob.ENT_Name) sb.Append(string.Format("单位全称由“{0}”变为“{1}”；", oldOB.ENT_Name, ob.ENT_Name));
                if (oldOB.END_Addess != ob.END_Addess) sb.Append(string.Format("工商注册地由“{0}”变为“{1}”；", oldOB.END_Addess, ob.END_Addess));
                if (oldOB.ENT_OrganizationsCode != ob.ENT_OrganizationsCode) sb.Append(string.Format("统一社会信息代码由“{0}”变为“{1}”；", oldOB.ENT_OrganizationsCode, ob.ENT_OrganizationsCode));
                if (oldOB.ENT_City != ob.ENT_City) sb.Append(string.Format("隶属区县由“{0}”变为“{1}”；", oldOB.ENT_City, ob.ENT_City));

                if (oldOB.PSN_RegisterNO != ob.PSN_RegisterNO) sb.Append(string.Format("注册号由“{0}”变为“{1}”；", oldOB.PSN_RegisterNO, ob.PSN_RegisterNO));
                if (oldOB.PSN_RegisteProfession != ob.PSN_RegisteProfession) sb.Append(string.Format("注册专业由“{0}”变为“{1}”；", oldOB.PSN_RegisteProfession, ob.PSN_RegisteProfession));
                if (oldOB.PSN_CertificationDate != ob.PSN_CertificationDate) sb.Append(string.Format("发证时间由“{0}”变为“{1}”；", oldOB.PSN_CertificationDate.HasValue ? oldOB.PSN_CertificationDate.Value.ToString("yyyy-MM-dd") : "", ob.PSN_CertificationDate.Value.ToString("yyyy-MM-dd")));
                if (oldOB.PSN_CertificateValidity != ob.PSN_CertificateValidity) sb.Append(string.Format("有效期至由“{0}”变为“{1}”；", oldOB.PSN_CertificateValidity.HasValue ? oldOB.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd") : "", ob.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd")));
                if (oldOB.PSN_RegistePermissionDate != ob.PSN_RegistePermissionDate) sb.Append(string.Format("注册审批日期由“{0}”变为“{1}”；", oldOB.PSN_RegistePermissionDate.HasValue ? oldOB.PSN_RegistePermissionDate.Value.ToString("yyyy-MM-dd") : "", ob.PSN_RegistePermissionDate.Value.ToString("yyyy-MM-dd")));


                ob.XGR = UserName;
                ob.XGSJ = DateTime.Now;
                zjs_CertificateDAL.Update(ob);
                ViewState["zjs_CertificateMDL"]=ob;
                UIHelp.WriteOperateLog(PersonName, UserID, "修正二级造价工程师证书信息", sb.ToString());

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修正二级造价工程师证书信息失败", ex);
                return;
            }
            UIHelp.layerAlert(Page, "证书修正成功","");
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }
    }
}