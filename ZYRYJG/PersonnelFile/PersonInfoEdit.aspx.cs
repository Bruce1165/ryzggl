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
    public partial class PersonInfoEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "PersonLock.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownList(RadComboBoxCertificateType, "102");//证件类别
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族
                UIHelp.FillDropDownListWithTypeName(RadComboBoxCulturalLevel, "109", "请选择", "");//学历
                UIHelp.FillDropDownList(RadComboBoxPoliticalBackground, "110", "请选择", "");//政治面貌    

                if (string.IsNullOrEmpty(Request["o"]) == false)//edit
                {
                    if (ValidResourceIDLimit(RoleIDs, "PersonLock") == true)//人员修正与锁定权限
                    {
                        ButtonLock.Visible = true;
                    }

                    Int64 id = Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"]));
                    WorkerOB ob = WorkerDAL.GetObject(id);
                    ViewState["WorkerOB"] = ob;

                    RadTextBoxWorkerName.Text = ob.WorkerName;
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
                    RadComboBoxCertificateType.Text = ob.CertificateType;
                    RadTextCertificateCode.Text = ob.CertificateCode;
                    RadDatePickerBirthday.SelectedDate = ob.Birthday;
                    //RadTextBoxNation.Text = ob.Nation;
                    //RadTextBoxCulturalLevel.Text = ob.CulturalLevel;
                    //RadTextBoxPoliticalBackground.Text = ob.PoliticalBackground;

                    if (RadComboBoxNation.FindItemByText(ob.Nation) != null) //民族
                    {
                        RadComboBoxNation.FindItemByText(ob.Nation).Selected = true;
                        RadComboBoxNation.Text = ob.Nation;
                    }
                    if (RadComboBoxCulturalLevel.FindItemByText(ob.CulturalLevel) != null) //文化程度
                    {
                        RadComboBoxCulturalLevel.FindItemByText(ob.CulturalLevel).Selected = true;
                        RadComboBoxCulturalLevel.Text = ob.CulturalLevel;
                    }
                    if (RadComboBoxPoliticalBackground.FindItemByText(ob.PoliticalBackground) != null) //政治面貌  
                    {
                        RadComboBoxPoliticalBackground.FindItemByText(ob.PoliticalBackground).Selected = true;
                        RadComboBoxPoliticalBackground.Text = ob.PoliticalBackground;
                    }

                    RadTextBoxEmail.Text = ob.Email;
                    RadTextBoxPhone.Text = ob.Phone;
                    RadTextMobile.Text = ob.Mobile;
                    RadTextZipCode.Text = ob.ZipCode;

                    RefreshLockStatus(ob.CertificateCode);

                    System.Random rm = new Random();

                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ob))); //绑定照片;

                    ImgIDCard.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetIDCardImgPath(ob))); //绑定手持身份证半身照片;

                    ImgSign.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetSignImgPath(ob))); //绑定签名照片;
                }
            }
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WorkerOB ob = ViewState["WorkerOB"] == null ? new WorkerOB() : (WorkerOB)ViewState["WorkerOB"];
                WorkerOB old = ob;

                if (RadComboBoxCertificateType.SelectedValue == "身份证")
                {
                    if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                    {
                        UIHelp.layerAlert(Page, "身份证格式错误，请修改。（如果带X请注意大小写）");
                        return;
                    }
                }
                if (ob.CertificateCode != RadTextCertificateCode.Text.Trim())
                {
                    int count = WorkerDAL.SelectCount(string.Format(" and CertificateCode='{0}'", RadTextCertificateCode.Text.Trim()));
                    if (count > 0)
                    {
                        UIHelp.layerAlert(Page, "证件号码已经存在，不允许重复录入人员信息！");
                        return;
                    }
                }
                ob.WorkerName = RadTextBoxWorkerName.Text.Trim();
                if (RadioButtonMan.Checked)
                {
                    ob.Sex = "男";
                }
                else
                {
                    ob.Sex = "女";
                }
                ob.CertificateType = RadComboBoxCertificateType.Text.Trim();
                ob.CertificateCode = RadTextCertificateCode.Text.Trim();
                if (RadDatePickerBirthday.SelectedDate.HasValue)
                {
                    ob.Birthday = RadDatePickerBirthday.SelectedDate.Value;
                }
                //ob.Nation = RadTextBoxNation.Text;
                //ob.CulturalLevel = RadTextBoxCulturalLevel.Text;
                //ob.PoliticalBackground = RadTextBoxPoliticalBackground.Text;
                ob.Nation = (RadComboBoxNation.Text == "请选择" ? "" : RadComboBoxNation.Text);//民族
                ob.CulturalLevel = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                ob.PoliticalBackground = (RadComboBoxPoliticalBackground.Text == "请选择" ? "" : RadComboBoxPoliticalBackground.Text);  //政治面貌
                ob.Email = RadTextBoxEmail.Text.Trim();
                ob.Phone = RadTextBoxPhone.Text.Trim();
                ob.Mobile = RadTextMobile.Text.Trim();
                ob.ZipCode = RadTextZipCode.Text.Trim();
                if (ViewState["WorkerOB"] == null)//new
                {
                    WorkerDAL.Insert(ob);
                    RefreshLockStatus(ob.CertificateCode);
                }
                else
                {
                    WorkerDAL.Update(ob);
                }
                ViewState["WorkerOB"] = ob;

                UIHelp.WriteOperateLog(PersonName, UserID, "修改个人信息", "");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "var isfresh = true;", true);
                UIHelp.layerAlert(Page, "保存人员信息成功！",6,3000);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存人员信息失败！", ex);
            }
            if (ValidResourceIDLimit(RoleIDs, "PersonLock") == true)//人员修正与锁定权限
            {
                ButtonLock.Visible = true;
            }
        }

        //加锁、解锁
        protected void ButtonLock_Click(object sender, EventArgs e)
        {
            if (ButtonLock.Text == "加 锁")
            {
                Response.Redirect(string.Format("PersonLock.aspx?o={0}", ((WorkerOB)ViewState["WorkerOB"]).WorkerID.ToString()));
            }
            else if (ButtonLock.Text == "解 锁")
            {
                WorkerLockOB o = WorkerLockDAL.GetLastObject(Convert.ToInt64(Request.QueryString["o"]));

                o.UnlockPerson = PersonName;
                o.UnlockTime = DateTime.Now;
                o.LockStatus = "解锁";
                try
                {
                    WorkerLockDAL.Update(o);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "人员解锁失败！", ex);
                    return;
                }
                RefreshLockStatus(RadTextCertificateCode.Text.Trim());
                UIHelp.WriteOperateLog(PersonName, UserID, "人员解锁", string.Format("人员证件号码：{0}。", RadTextCertificateCode.Text.Trim()));
            }
        }


        /// <summary>
        /// 刷新证书锁定状态
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        private void RefreshLockStatus(string WorkerCertificateCode)
        {
            //证书锁定状态           
            bool lockStatus = WorkerLockDAL.GetWorkerLockStatus(WorkerCertificateCode);
            if (lockStatus == false)//已解锁
            {
                ButtonLock.Text = "加 锁";
                DivDetail.Visible = false;
            }
            else
            {
                DivDetail.Visible = true;
                ButtonLock.Text = "解 锁";
            }

            //检索历次锁定记录
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("WorkerID in(select WorkerID from dbo.Worker where CertificateCode ='{0}')", WorkerCertificateCode));//证件号码
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridLock.CurrentPageIndex = 0;
            RadGridLock.DataSourceID = ObjectDataSource1.ID;
        }

        //根据证件号码显示相应的信息
        protected void RadTextCertificateCode_TextChanged(object sender, EventArgs e)
        {
            if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")//根据身份证读取性别和出生日期
            {
                if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                    return;
                }
                string certificatecode = RadTextCertificateCode.Text.ToString();
                //18位身份证号码：第7、8、9、10位为出生年份(四位数)，第11、第12位为出生月份，第13、14位代表出生日期，第17位代表性别，奇数为男，偶数为女。
                if (certificatecode.Length == 18)
                {
                    string BirthDay = string.Format("{0}-{1}-{2}", certificatecode.Substring(6, 4), certificatecode.Substring(10, 2), certificatecode.Substring(12, 2));
                    RadDatePickerBirthday.SelectedDate = Convert.ToDateTime(BirthDay);
                    //性别
                    int sex = Convert.ToInt32(certificatecode.Substring(16, 1));
                    if (sex % 2 == 0)
                    {
                        RadioButtonWoman.Checked = true;
                        RadioButtonMan.Checked = false;//代表男
                    }
                    else
                    {
                        RadioButtonMan.Checked = true;//代表男
                        RadioButtonWoman.Checked = false;
                    }
                }
                else if (certificatecode.Length == 15)
                {
                    //15位身份证号码：第7、8位为出生年份(两位数)，第9、10位为出生月份，第11、12位代表出生日期，第15位代表性别，奇数为男，偶数为女。 
                    string BirthDay = string.Format("19{0}-{1}-{2}", certificatecode.Substring(6, 2), certificatecode.Substring(8, 2), certificatecode.Substring(10, 2));
                    RadDatePickerBirthday.SelectedDate = Convert.ToDateTime(BirthDay);
                    //性别
                    int sex = Convert.ToInt32(certificatecode.Substring(14, 1));
                    if (sex % 2 == 0)
                    {
                        RadioButtonMan.Checked = false;//代表男
                        RadioButtonWoman.Checked = true;
                    }
                    else
                    {
                        RadioButtonMan.Checked = true;//代表男
                        RadioButtonWoman.Checked = false;
                    }
                }
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

                return path;
            }
            else
            {
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
                if(ob.SignPhotoTime.HasValue==true)
                {                    
                    if(IfExistRoleID("1")==true)//管理员允许授权修改签名照
                    {
                        img_lock.Visible = true;
                        ButtonOpenUpdateSignPhoto.Visible = true;
                    }
                }
                return path;
            }
            else
            {
                return "~/Images/SignImg.jpg";
            }
        }

        //授权个人修改签名照
        protected void ButtonOpenUpdateSignPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                WorkerOB ob = (WorkerOB)ViewState["WorkerOB"];
                ob.SignPhotoTime = null;
                WorkerDAL.Update(ob);
                ViewState["WorkerOB"] = ob;
                img_lock.Visible = false;
                ButtonOpenUpdateSignPhoto.Visible = false;
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "授权个人修改签名照失败！", ex);
            }
        }
    }
}
