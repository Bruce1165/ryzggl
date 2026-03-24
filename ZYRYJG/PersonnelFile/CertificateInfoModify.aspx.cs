using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.PersonnelFile
{
    public partial class CertificateInfoModify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownList(RadComboBoxSKILLLEVEL, "111", "请选择", "");//技术职称 
                UIHelp.FillDropDownList(RadComboBoxJob, "112", "请选择", "");//职务 

                CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request.QueryString["o"])));
                ViewState["CertificateOB"] = ob;

                LabelPostTypeID.Text = PostInfoDAL.GetObject(ob.PostTypeID.Value).PostName;//类别
                if (ob.PostTypeID == 3 && string.IsNullOrEmpty(ob.AddItemName) == false)//造价员
                {
                    LabelPostID.Text = ob.AddItemName;//工种
                }
                else
                {
                    LabelPostID.Text = PostInfoDAL.GetObject(ob.PostID.Value).PostName;
                }

                RadTextBoxUnitName.Text = ob.UnitName;//单位
                RadTextBoxUnitCode.Text = ob.UnitCode;//组织机构代码

                RadTextBoxWorkerName.Text = ob.WorkerName;//姓名
                RadTextBoxWorkerCertificateCode.Text = ob.WorkerCertificateCode;//证件号码
                if (ob.Sex == "女")
                {
                    RadioButtonMan.Checked = false;
                    RadioButtonWoman.Checked = true;
                }
                else
                {
                    RadioButtonMan.Checked = true;
                    RadioButtonWoman.Checked = false;
                }
                RadDatePickerBirthday.SelectedDate = ob.Birthday;//生日

                LabelCertificateCode.Text = ob.CertificateCode;//证书编号
                RadDatePickerConferDate.SelectedDate = ob.ConferDate;//首次发证日期
                RadDatePickerValidStartDate.SelectedDate = ob.ValidStartDate;//有效期起始
                RadDatePickerValidEndDate.SelectedDate = ob.ValidEndDate;//有效期至
                LabelConferUnit.Text = ob.ConferUnit;//发证机关
                LabelStatus.Text = ob.Status;//最新业务状态
                P_Remark.InnerText = ob.Remark;//备注
                if (RadComboBoxSKILLLEVEL.FindItemByText(ob.SkillLevel) != null) //技术职称
                {
                    RadComboBoxSKILLLEVEL.FindItemByText(ob.SkillLevel).Selected = true;
                }
                if (RadComboBoxJob.FindItemByText(ob.Job) != null) //职务
                {
                    RadComboBoxJob.FindItemByText(ob.Job).Selected = true;
                }           

                if (string.IsNullOrEmpty(ob.WorkerCertificateCode) == false)
                {
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(ob.FacePhoto, ob.WorkerCertificateCode);
                }

                if (ValidResourceIDLimit(RoleIDs, "CertificateModifyAll") == false)//没有不受限制修正权限
                {
                    RadComboBoxJob.Enabled = false;
                    RadComboBoxSKILLLEVEL.Enabled = false;
                    RadTextBoxWorkerName.Enabled = false;
                    RadTextBoxWorkerCertificateCode.Enabled = false;
                    ButtonCheckIDCard.Enabled = false;
                    RadDatePickerBirthday.Enabled = false;
                    RadTextBoxUnitName.Enabled = false;
                    RadTextBoxUnitCode.Enabled = false;
                    ButtonCheckUnitcode.Enabled = false;
                    RadDatePickerValidEndDate.Enabled = false;
                    RadioButtonMan.Enabled = false;

                    if (ob.ConferDate.Value.ToString("yyyy-MM-dd") != "1900-01-01" && ob.ConferDate.Value.ToString("yyyy-MM-dd") != "2000-01-01")
                    {
                        RadDatePickerConferDate.Enabled = false;
                        ButtonModify.Enabled = false;
                        UIHelp.layerAlert(Page, "首次发证时间不是特定无效时间，无法修正，如果仍要修正请与管理员联系！");
                        return;
                    }
                }
                
            }
        }

        //修改
        protected void ButtonModify_Click(object sender, EventArgs e)
        {           
            CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];

            CertificateOB oldOB = new CertificateOB();             
            oldOB.UnitName = ob.UnitName;
            oldOB.UnitCode = ob.UnitCode;
            oldOB.WorkerName = ob.WorkerName;
            oldOB.Sex = ob.Sex;
            oldOB.Birthday = ob.Birthday;
            oldOB.WorkerCertificateCode =ob.WorkerCertificateCode;
            oldOB.ConferDate = ob.ConferDate;
            oldOB.ValidEndDate = ob.ValidEndDate;
            oldOB.SkillLevel = ob.SkillLevel;

            ob.UnitName = RadTextBoxUnitName.Text.Trim();//单位
            ob.UnitCode = RadTextBoxUnitCode.Text.Trim();//组织机构代码
            ob.WorkerName = RadTextBoxWorkerName.Text.Trim();//姓名
            ob.WorkerCertificateCode = RadTextBoxWorkerCertificateCode.Text.Trim();//证件号码
            if (RadioButtonWoman.Checked == true)
                ob.Sex = "女";
            else
                ob.Sex = "男";

            ob.Birthday = RadDatePickerBirthday.SelectedDate;//生日
            ob.ConferDate = RadDatePickerConferDate.SelectedDate;//首次发证日期
            ob.ValidStartDate = RadDatePickerValidStartDate.SelectedDate;//有效期起
            ob.ValidEndDate = RadDatePickerValidEndDate.SelectedDate;//有效期至
            if (RadComboBoxJob.SelectedItem.Text != "请选择")
            {
                ob.Job = RadComboBoxJob.SelectedItem.Text;//职务
            }
            ob.SkillLevel = RadComboBoxSKILLLEVEL.SelectedItem.Text;//技术职称
            ob.ModifyPersonID = PersonID;
            ob.ModifyTime = DateTime.Now;
            ob.CheckDate = DateTime.Now;

            #region 上传个人照片

            if (RadUploadFacePhoto.UploadedFiles.Count > 0)
            {
                if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "照片格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "照片大小不能超过50k！");
                    return;
                }

                string workerPhotoFolder = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", ob.PostTypeID, ob.CertificateCode.Substring(ob.CertificateCode.Length - 3, 3));//证书个人照片存放路径
                if (!Directory.Exists(Page.Server.MapPath(workerPhotoFolder)))
                {
                    System.IO.Directory.CreateDirectory(Page.Server.MapPath(workerPhotoFolder));
                }
                foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                {
                    validFile.SaveAs(Page.Server.MapPath(Path.Combine(workerPhotoFolder, ob.CertificateCode + ".jpg")), true);
                    ob.FacePhoto = string.Format("~/UpLoad/CertificatePhoto/{0}/{1}/{2}.jpg", ob.PostTypeID, ob.CertificateCode.Substring(ob.CertificateCode.Length - 3, 3), ob.CertificateCode);
                    break;
                }
            }

            #endregion   
           
            try
            {
                CertificateDAL.Update(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修正证书信息失败！", ex);
                return;
            }

           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oldOB.UnitName != ob.UnitName) sb.Append(string.Format("单位全称由“{0}”变为“{1}”；", oldOB.UnitName, ob.UnitName));
            if (oldOB.UnitCode != ob.UnitCode) sb.Append(string.Format("组织机构代码由“{0}”变为“{1}”；", oldOB.UnitCode, ob.UnitCode));
            if (oldOB.WorkerName != ob.WorkerName) sb.Append(string.Format("姓名由“{0}”变为“{1}”；", oldOB.WorkerName, ob.WorkerName));
            if (oldOB.Sex != ob.Sex) sb.Append(string.Format("性别由“{0}”变为“{1}”；", oldOB.Sex, ob.Sex));
            if (oldOB.Birthday != ob.Birthday) sb.Append(string.Format("出生日期由“{0}”变为“{1}”；", oldOB.Birthday.HasValue ? oldOB.Birthday.Value.ToString("yyyy-MM-dd") : "", ob.Birthday.Value.ToString("yyyy-MM-dd")));
            if (oldOB.WorkerCertificateCode != ob.WorkerCertificateCode) sb.Append(string.Format("证件号码由“{0}”变为“{1}”；", oldOB.WorkerCertificateCode, ob.WorkerCertificateCode));
            if (oldOB.ConferDate != ob.ConferDate) sb.Append(string.Format("发证时间由“{0}”变为“{1}”；", oldOB.ConferDate.HasValue ? oldOB.ConferDate.Value.ToString("yyyy-MM-dd") : "", ob.ConferDate.Value.ToString("yyyy-MM-dd")));
            if (oldOB.ValidEndDate != ob.ValidEndDate) sb.Append(string.Format("有效期至由“{0}”变为“{1}”；", oldOB.ValidEndDate.HasValue ? oldOB.ValidEndDate.Value.ToString("yyyy-MM-dd") : "", ob.ValidEndDate.Value.ToString("yyyy-MM-dd")));
            if (oldOB.SkillLevel != ob.SkillLevel) sb.Append(string.Format("技术职称或等级由“{0}”变为“{1}”；", oldOB.SkillLevel, ob.SkillLevel));

            UIHelp.WriteOperateLog(PersonName, UserID, "修正证书信息", string.Format("证书编号：{0}，状态：{1}。{2}", ob.CertificateCode, ob.Status, sb.ToString()));

            //Response.Redirect(string.Format("CertificateInfo.aspx?o={0}&type={1}", Utility.Cryptography.Encrypt(Request["o"]), Request["type"]));
            Response.Redirect(string.Format("CertificateInfo.aspx?o={0}&type={1}", Request["o"], Request["type"]));
        }

        protected void ButtonCheckIDCard_Click(object sender, EventArgs e)
        {
            if (Utility.Check.isChinaIDCard(RadTextBoxWorkerCertificateCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, "不是有效的身份证！");
            }
            else
            {
                UIHelp.layerAlert(Page, "这是有效的身份证！");
            }
        }

        protected void ButtonCheckUnitcode_Click(object sender, EventArgs e)
        {
            if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxUnitCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, "不是有效的组织机构代码！");
            }
            else
            {
                UIHelp.layerAlert(Page, "这是有效的组织机构代码！");
            }
        }

        //返回
        protected void ButtonFH_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CertificateInfo.aspx?o={0}&type={1}", Request["o"], Request["type"]));//Request["o"]已加密
        }
    }
}
