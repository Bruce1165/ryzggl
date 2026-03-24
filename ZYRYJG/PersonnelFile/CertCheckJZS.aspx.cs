using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Drawing;
using System.IO;

namespace ZYRYJG.PersonnelFile
{
    public partial class CertCheckJZS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] keys = Utility.Cryptography.Decrypt(Request["o"].Replace(" ", "+")).Split(',');
                string CertificateCAID = keys[0];
                string PSN_RegisterNO =  keys[1];

                if (PSN_RegisterNO.IndexOf("京2") > -1)
                {
                    #region 二级建造师电子证书验证
                    if(PSN_RegisterNO.Length==13)
                    {
                        //过渡期2023-11-01 至 2023-12-31，老电子证书扫码仍然可以显示
                        if(DateTime.Now >new DateTime(2024,1,1))
                        {
                            Response.Clear();
                            Response.Write("自2024年1月1日起，本市二级建造师统一使用新版电子证照，旧版电子注册证书作废。");
                            return;

                        }
                        else
                        {
                            if(Convert.ToInt32(PSN_RegisterNO.Substring(4,2)) >23)
                            {
                                PSN_RegisterNO = string.Format("{0}20{1}20{2}",PSN_RegisterNO.Substring(0,4),PSN_RegisterNO.Substring(6,2),PSN_RegisterNO.Substring(6,7));
                            }
                            else
                            {
                                PSN_RegisterNO = string.Format("{0}20{1}20{2}",PSN_RegisterNO.Substring(0,4),PSN_RegisterNO.Substring(4,2),PSN_RegisterNO.Substring(6,7));
                            }

                            pGuoDuTip.InnerText = "您扫码的是旧版电子证书，过渡期间（2023-11-01至2023-12-31）已下载的证书仍可使用。自2024年1月1日起，本市二级建造师统一使用新版电子证照，旧版电子注册证书作废，请尽快下载新版本。";
                        }
                    }
                    COC_TOW_Person_BaseInfoMDL ob = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_RegisterNO(PSN_RegisterNO);

                    LabelWorkerName.Text = ob.PSN_Name;
                    LabelUnitName.Text = ob.ENT_Name;

                    LabelSex.Text = ob.PSN_Sex;
                    LabelPostTypeID.Text = ob.PSN_Level + "建造师";

                    List<COC_TOW_Register_ProfessionMDL> list = COC_TOW_Register_ProfessionDAL.GetListGetObject(ob.PSN_ServerID);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (COC_TOW_Register_ProfessionMDL p in list)
                    {
                        sb.Append(string.Format("{0}（有效期：{1}-{2}）<br />",
                            formatZY(p.PRO_Profession),
                            ob.PSN_RegistePermissionDate.Value.ToString("yyyy年M月d日"),
                            p.PRO_ValidityEnd.Value.ToString("yyyy年M月d日")));
                    }
                    td_Profession.InnerHtml = sb.ToString();//专业
                    //LabelPostID.Text = sb.ToString();//专业

                    LabelBirthday.Text = ob.PSN_BirthDate.HasValue == false ? "" : ob.PSN_BirthDate.Value.ToString("yyyy年M月d日");//生日
                    LabelWorkerCertificateCode.Text = ob.PSN_CertificateNO;//身份证号
                    LabelCertificateCode.Text = ob.PSN_RegisterNO;//注册号

                    LabelConferUnit.Text = "北京市住房和城乡建设委员会";
                    LabelConferDate.Text = ob.PSN_RegistePermissionDate.HasValue == false ? "" : ob.PSN_RegistePermissionDate.Value.ToString("yyyy年M月d日");//发证日期
                    //LabelStatus.Text = ob.PSN_RegisteType;//状态

                    if (ob.PSN_CertificateValidity < DateTime.Now)//备注信息
                    {
                        LabelDesc.Text = "此证书为无效证书（过期）";
                        LabelDesc.ForeColor = Color.Red;
                    }
                    else if (ob.PSN_RegisteType == "07")
                    {
                        LabelDesc.Text = "此证书为无效证书（注销）";
                        LabelDesc.ForeColor = Color.Red;
                    }
                    else
                    {
                        LabelDesc.Text = "　";
                    }

                    LabelValidate.Text = ob.PSN_CertificateValidity.Value.ToString("yyyy年M月d日");//有效期

                    if (string.IsNullOrEmpty(ob.PSN_CertificateNO) == false)//照片
                    {
                        ImgCode.Src = UIHelp.ShowFaceImage(ob.PSN_CertificateNO);
                    }
                    else
                    {
                        ImgCode.Src = "~/Images/tup.gif";
                    }

                    //使用有效期
                    EJCertUseMDL _EJCertUseMDL = EJCertUseDAL.GetObject(CertificateCAID);
                    if (_EJCertUseMDL != null)
                    {
                        LabelZZRQ.Text =string.Format("{0}-{1}", _EJCertUseMDL.BeginTime.Value.ToString("yyyy年M月d日"),_EJCertUseMDL.EndTime.Value.ToString("yyyy年M月d日"));
                    }

                    #endregion
                }
                else
                {
                    #region 二级造价工程师电子证书验证

                    zjs_CertificateMDL ob = zjs_CertificateDAL.GetObjectByPSN_RegisterNO(PSN_RegisterNO);
                    LabelWorkerName.Text = ob.PSN_Name;
                    LabelUnitName.Text = ob.ENT_Name;
                    LabelSex.Text = ob.PSN_Sex;
                    LabelPostTypeID.Text = "二级造价工程师";
                    td_Profession.InnerHtml = ob.PSN_RegisteProfession;//专业
                    LabelBirthday.Text = ob.PSN_BirthDate.HasValue == false ? "" : ob.PSN_BirthDate.Value.ToString("yyyy年MM月dd日");//生日
                    LabelWorkerCertificateCode.Text = ob.PSN_CertificateNO;//身份证号
                    LabelCertificateCode.Text = ob.PSN_RegisterNO;//注册号
                    LabelValidate.Text = ob.PSN_CertificateValidity.Value.ToString("yyyy年MM月dd日");//有效期
                    LabelConferUnit.Text = "北京市住房和城乡建设委员会";
                    LabelConferDate.Text = ob.PSN_CertificationDate.HasValue == false ? "" : ob.PSN_CertificationDate.Value.ToString("yyyy年MM月dd日");//发证日期
                    LabelZZRQ.Text = ob.PSN_CertificationDate.Value.ToString("yyyy年MM月dd日");//制证日期
                    trUseSpan.Visible = false;
                    if (ob.PSN_CertificateValidity < DateTime.Now)//备注信息
                    {
                        LabelDesc.Text = "此证书为无效证书（过期）";
                        LabelDesc.ForeColor = Color.Red;
                    }
                    else if (ob.PSN_RegisteType == "07")
                    {
                        LabelDesc.Text = "此证书为无效证书（注销）";
                        LabelDesc.ForeColor = Color.Red;
                    }
                    else
                    {
                        LabelDesc.Text = "　";
                    }

                    if (string.IsNullOrEmpty(ob.PSN_CertificateNO) == false)//照片
                    {
                        ImgCode.Src = UIHelp.ShowFaceImage(ob.PSN_CertificateNO);
                    }
                    else
                    {
                        ImgCode.Src = "~/Images/tup.gif";
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog(string.Format("读取证书二维码失败，二维码【{0}】", Request["o"]), ex);
                UIHelp.layerAlert(Page, "读取证书二维码失败！");
            }
        }

        private string formatZY(string zy)
        {
            //格式化专业
            return zy.Replace("建筑", "建筑工程").Replace("公路", "公路工程").Replace("水利", "水利水电工程").Replace("市政", "市政公用工程").Replace("矿业", "矿业工程").Replace("机电", "机电工程");
        }

    }
}