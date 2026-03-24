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
    public partial class WorkerInfoEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                if (PersonType == 3)//企业
                    return "CompanyWorkerList.aspx";
                else
                    return base.CheckVisiteRgihtUrl;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownList(RadComboBoxCertificateType, "102");//证件类型
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族
                UIHelp.FillDropDownListWithTypeName(RadComboBoxCulturalLevel, "109", "请选择", "");//学历
                UIHelp.FillDropDownListWithTypeName(RadComboBoxPoliticalBackground, "110", "请选择", "");//政治面貌  

                Int64 id = string.IsNullOrEmpty(Request["o"]) ? PersonID : Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"]));
                WorkerOB ob = WorkerDAL.GetObject(id);
                ViewState["WorkerOB"] = ob;
                ViewState["CertificateCode"] = ob.CertificateCode;
                UIHelp.SetData(tableWorker, ob, false);
                                
                if (ob.Sex == "男")
                {
                    RadioButtonMan.Checked = true;
                    RadioButtonWoman.Checked = false;
                }
                else
                {
                    RadioButtonMan.Checked = false;
                    RadioButtonWoman.Checked = true;
                }

                UIHelp.SetReadOnly(RadTextBoxWorkerName, true);
                RadioButtonMan.Enabled = false;
                RadioButtonWoman.Enabled = false;
                UIHelp.SetReadOnly(RadComboBoxCertificateType, true);
                UIHelp.SetReadOnly(RadTextBoxCertificateCode, true);
                if (RadComboBoxCertificateType.SelectedItem.Value == "身份证" && ob.Birthday.HasValue)
                {
                    UIHelp.SetReadOnly(RadDatePickerBirthday, true);
                }

                if(string.IsNullOrEmpty(ob.Phone)==true)
                {
                    UIHelp.SetReadOnly(RadTextBoxPhone, false);
                }
                else
                {
                    UIHelp.SetReadOnly(RadTextBoxPhone, true);
                }

                //UIHelp.SetReadOnly(RadTextBoxMobile, true);             

                System.Random rm = new Random();

                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ob))); //绑定照片;

                ImgIDCard.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetIDCardImgPath(ob))); //绑定手持身份证半身照片;

                ImgSign.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetSignImgPath(ob))); //绑定签名照片;


                //RadTextBoxWorkerName.Text = ob.WorkerName;
                //RadComboBoxCertificateType.Text = ob.CertificateType;
                //RadTextCertificateCode.Text = ob.CertificateCode;
                //RadDatePickerBirthday.SelectedDate = ob.Birthday;
                //RadTextBoxNation.Text = ob.Nation;
                //RadTextBoxCulturalLevel.Text = ob.CulturalLevel;
                //RadTextBoxPoliticalBackground.Text = ob.PoliticalBackground;
                //RadTextBoxSKILLLEVEL.Text = ob.Email;
                //RadTextBoxPhone.Text = ob.Phone;
                //RadTextMobile.Text = ob.Mobile;
                //RadTextZipCode.Text = ob.ZipCode;

            }
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetFacePhotoPath(WorkerOB ob)
        {
            if (ob.CertificateCode == "") return "~/Images/photo_ry.jpg";

            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", ob.CertificateCode.Substring(ob.CertificateCode.Length - 3, 3), ob.CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
            {
                ButtonImgEdit.Visible = true;
                return path;
            }
            else
                return "~/Images/photo_ry.jpg";
        }

        /// <summary>
        /// 获取手持身份证半身照地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetIDCardImgPath(WorkerOB ob)
        {
            if (ob.CertificateCode == "") return "~/Images/IDCard.jpg";

            string path = string.Format("~/UpLoad/HandIDCard/{0}/{1}.jpg", ob.CertificateCode.Substring(ob.CertificateCode.Length - 3, 3), ob.CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
            {
                if (ob.IDCardPhotoUpdateTime.HasValue == false)//个人没有确认提交过照片，允许编辑微调照片
                {
                    ButtonImgIDCard.Visible = true;
                    RadUploadImgIDCard.Visible = true;
                }
                else
                {
                    ButtonImgIDCard.Visible = false;
                    RadUploadImgIDCard.Visible = false;
                }
                return path;
            }
            else
            {
                RadUploadImgIDCard.Visible = true;
                return "~/Images/IDCard.jpg";
            }
        }

        /// <summary>
        /// 获取手写签名照地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetSignImgPath(WorkerOB ob)
        {
            if (ob.CertificateCode == "") return "~/Images/SignImg.jpg";

            string path = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", ob.CertificateCode.Substring(ob.CertificateCode.Length - 3, 3), ob.CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
            {
                //ButtonImgEditQianMing.Visible = true;
                //RadUploadImgSign.Visible = true;
                CheckSignImgUpdate(ob.CertificateCode);

                if (ob.SignPhotoTime.HasValue == false)//个人没有确认提交过照片，允许编辑微调照片
                {
                    ButtonImgEditQianMing.Visible = true;
                    RadUploadImgSign.Visible = true;
                    ButtonSignPhotoUpdate.Visible = true;
                }
                else
                {
                    ButtonImgEditQianMing.Visible = false;
                    RadUploadImgSign.Visible = false;
                    ButtonSignPhotoUpdate.Visible = false;
                }
                return path;
            }
            else
            {
                RadUploadImgSign.Visible = true;
                return "~/Images/SignImg.jpg";
            }
        }

        /// <summary>
        /// 检查个人手写签名是否存在更新，如果有，同步更新到个人目录中
        /// </summary>
        /// <param name="WorkerCertificateCode">证件号码</param>
        protected void CheckSignImgUpdate(string WorkerCertificateCode)
        {
            string path = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
            System.IO.FileInfo Fi = new FileInfo(Server.MapPath(path));

            string sql = @"SELECT top 1 [FileInfo].[FileUrl],[FileInfo].[AddTime]
                            FROM [dbo].[FileInfo]
                            inner join [dbo].[COC_TOW_Person_File] on [FileInfo].[FileID] =[COC_TOW_Person_File].[FileID]
                            where [FileInfo].[DataType]='手写签名照' and [COC_TOW_Person_File].[PSN_RegisterNO] 
	                            in(
		                              select [PSN_RegisterNO] from [dbo].[COC_TOW_Person_BaseInfo]
		                              where [PSN_CertificateNO]='{0}'
		                              union
		                              select [PSN_RegisterNO] from [dbo].[zjs_Certificate]
		                              where [PSN_CertificateNO]='{0}'
	                              )
                            and AddTime > '{1}'
                            order by [FileInfo].[AddTime] desc";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode, Fi.CreationTime.ToString("yyyy-MM-dd")));

            if (dt != null && dt.Rows.Count > 0)
            {
                File.Copy(Server.MapPath(dt.Rows[0]["FileUrl"].ToString()), Server.MapPath(path), true);
            }
        }

        //修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 格式检查

            if (RadUploadFacePhoto.UploadedFiles.Count > 0)
            {
                if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "一寸照片格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "一寸照片大小不能超过50k！");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength < 200)
                {
                    UIHelp.layerAlert(Page, "一寸照片大小存在问题，请检查照片是否有效！");
                    return;
                }
            }
            if (RadUploadImgSign.UploadedFiles.Count > 0)
            {
                if (RadUploadImgSign.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "手写签名照格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadImgSign.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "手写签名照大小不能超过50k！");
                    return;
                }
                if (RadUploadImgSign.UploadedFiles[0].ContentLength < 200)
                {
                    UIHelp.layerAlert(Page, "手写签名照大小存在问题，请检查照片是否有效！");
                    return;
                }
            }
            if (RadUploadImgIDCard.UploadedFiles.Count > 0)
            {
                if (RadUploadImgIDCard.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "手持身份证半身照格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadImgIDCard.UploadedFiles[0].ContentLength > 512000)
                {
                    UIHelp.layerAlert(Page, "手持身份证半身照大小不能超过500k！");
                    return;
                }
                if (RadUploadImgIDCard.UploadedFiles[0].ContentLength < 200)
                {
                    UIHelp.layerAlert(Page, "手持身份证半身照大小存在问题，请检查照片是否有效！");
                    return;
                }
            }

            #endregion 格式检查

            try
            {
                WorkerOB ob = (WorkerOB)ViewState["WorkerOB"];
                ob.WorkerName = RadTextBoxWorkerName.Text;
                if (RadioButtonMan.Checked)
                {
                    ob.Sex = "男";
                }
                else
                {
                    ob.Sex = "女";
                }
                ob.CertificateType = RadComboBoxCertificateType.Text;
                ob.CertificateCode = RadTextBoxCertificateCode.Text;
                if (RadDatePickerBirthday.SelectedDate.HasValue)
                {
                    ob.Birthday = RadDatePickerBirthday.SelectedDate.Value;
                }
                ob.Nation = (RadComboBoxNation.Text == "请选择" ? "" : RadComboBoxNation.Text);   //民族
                ob.CulturalLevel = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                ob.PoliticalBackground = (RadComboBoxPoliticalBackground.Text == "请选择" ? "" : RadComboBoxPoliticalBackground.Text);  //政治面貌
                ob.Email = RadTextBoxEmail.Text;
                ob.Phone = RadTextBoxPhone.Text;
                ob.Mobile = RadTextBoxPhone.Text;
                ob.ZipCode = RadTextBoxZipCode.Text;
                ob.Address = RadTextBoxAddress.Text;
                WorkerDAL.Update(ob);
                ViewState["WorkerOB"] = ob;

                #region 上传个人照片
                //个人照片存放路径(按证件号码后3位)                              
                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = ob.CertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, ob.CertificateCode + ".jpg"), true);
                       
                        break;
                    }

                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ob))); //绑定照片;

                    //更新证书照片地址为空字符串的为null，以便出发重新生成电子证书
                    CertificateDAL.ClearFacePhoto(ob.CertificateCode);
                    //ButtonImgEdit.Visible = true;
                }
                #endregion

                #region 上传手持身份证半身照
                //个人照片存放路径(按证件号码后3位)                              
                if (RadUploadImgIDCard.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/HandIDCard/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadImgIDCard.UploadedFiles)
                    {
                        subPath = ob.CertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/HandIDCard/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/HandIDCard/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/HandIDCard/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, ob.CertificateCode + ".jpg"), true);

                        break;
                    }

                    ImgIDCard.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetIDCardImgPath(ob))); //绑定照片;
                    //ButtonImgIDCard.Visible = true;
                }              

                #endregion

                #region 上传手写签名照
                //个人照片存放路径(按证件号码后3位)                              
                if (RadUploadImgSign.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/SignImg/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadImgSign.UploadedFiles)
                    {
                        subPath = ob.CertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignImg/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignImg/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/SignImg/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, ob.CertificateCode + ".jpg"), true);

                        break;
                    }

                    ImgSign.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetSignImgPath(ob))); //绑定照片;
                    //ButtonImgEditQianMing.Visible = true;
                }

                #endregion

                UIHelp.layerAlert(Page, "个人信息修改成功！",6,3000);
         
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "个人信息修改失败！", ex);
            }
        }

        //编辑一寸照片
        protected void ButtonImgEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("WorkerImgEdit.aspx?o={0}&t={1}",Utility.Cryptography.Encrypt(ViewState["CertificateCode"].ToString()),1));
        }

        //编辑签名照片
        protected void ButtonImgEditQianMing_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("WorkerImgEdit.aspx?o={0}&t={1}", Utility.Cryptography.Encrypt(ViewState["CertificateCode"].ToString()), 2));
        }

        //编辑手持半身照
        protected void ButtonImgIDCard_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("WorkerImgEdit.aspx?o={0}&t={1}", Utility.Cryptography.Encrypt(ViewState["CertificateCode"].ToString()), 3));
        }

        //签名照提交
        protected void ButtonSignPhotoUpdate_Click(object sender, EventArgs e)
        {
            WorkerOB ob = (WorkerOB)ViewState["WorkerOB"];
            try
            {               
                ob.SignPhotoTime = DateTime.Now;
                WorkerDAL.Update(ob);


                ////二建暂时不适用手写签名照
                //COC_TOW_Person_BaseInfoMDL ej = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(ob.CertificateCode,"二级");

                //if(ej !=null)
                //{
                //    ej.XGSJ = DateTime.Now;//更新修改时间，触发重新生成电子证书
                //    COC_TOW_Person_BaseInfoDAL.Update(ej);
                //}


            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "个人提交签名照片失败！", ex);
            }
            ViewState["WorkerOB"] = ob;
            GetSignImgPath(ob);

            UIHelp.layerAlert(Page, "手写签名照已上传成功、可申请相关注册业务，如注册审核已通过，可至电子证书下载页面下载注册证书。", 6, 0);
        }
    }
}
