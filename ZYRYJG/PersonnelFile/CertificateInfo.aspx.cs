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
    public partial class CertificateInfo : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidResourceIDLimit(RoleIDs, "CertificateModify") == true)//有证书修正权限
                {
                    ButtonModify.Visible = true;
                    ButtonApplyCancel.Visible = true;
                }
                if (ValidResourceIDLimit(RoleIDs, "CertificateLock") == true)//有证书锁定与解锁权限
                {
                    ButtonLock.Visible = true;
                }
                if (Request.QueryString["type"] == "blank")//弹出新页打开
                {
                    divRoad.Visible = false;//隐藏路径
                    //ButtonFH.Visible = false;//隐藏返回
                }
                CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request.QueryString["o"])));
                //if (ob.PrintCount.HasValue && ob.PrintCount.Value > 0)
                //{
                //    LabelPrintCount.Text = " 补" + ob.PrintCount.ToString();
                //    LabelPrintCount.Visible = true;
                //}
                LabelWorkerName.Text = ob.WorkerName;
                if (PersonType!=2 || ob.PostTypeID != 5)//施工现场专业人员（八大员）个人端不显示证书所在企业名称
                {
                    LabelUnitName.Text = ob.UnitName;
                    LabelUnitCode.Text = ob.UnitCode;
                    if (string.IsNullOrEmpty(ob.UnitCode) == false)
                    {
                        QY_HYLSGXOB q = QY_LSGXDAL.GetObjectByZZJGDM(ob.UnitCode);

                        if (q != null && q.USERID.HasValue)
                        {
                            UserOB u = UserDAL.GetObject(q.USERID.Value);
                            if (u != null)
                            {
                                LabelLSGX.Text = u.UserName;
                            }
                        }
                    }
                }

                LabelSex.Text = ob.Sex;
                LabelPostTypeID.Text = PostInfoDAL.GetObject(ob.PostTypeID.Value).PostName;
                if (ob.PostTypeID == 3 && string.IsNullOrEmpty(ob.AddItemName) == false)//造价员
                {
                    LabelPostID.Text = ob.AddItemName;
                }
                else
                {
                    LabelPostID.Text = PostInfoDAL.GetObject(ob.PostID.Value).PostName;
                }
                LabelBirthday.Text = ob.Birthday.HasValue == false ? "" : ob.Birthday.Value.ToString("yyyy.MM.dd");
                LabelWorkerCertificateCode.Text = ob.WorkerCertificateCode;
                LabelCertificateCode.Text = ob.CertificateCode;
                if(ob.PostTypeID==1)
                {
                    LabelJob.Text = ob.Job;//职务
                }
                LabelSKILLLEVEL.Text = ob.SkillLevel;//技术等级
                LabelConferUnit.Text = ob.ConferUnit;
                LabelConferDate.Text = ob.ConferDate.HasValue == false ? "" : ob.ConferDate.Value.ToString("yyyy.MM.dd");
                P_Remark.InnerText = ob.Remark;//备注
                if (ValidResourceIDLimit(RoleIDs, "CertificateLock") == true//有证书锁定与解锁权限
                    || ValidResourceIDLimit(RoleIDs, "CertificateModify") == true//有证书修正权限
                    )
                {
                    LabelPhone.Text = GetPhone(ob);//联系电话
                }
                else
                {
                    LabelPhone.Text = "******";
                }
                LabelStatus.Text = string.Format("{0}{1}"
                    ,ob.Status
                    ,(string.IsNullOrEmpty(ob.Remark) == false && ob.Remark.Contains("超龄")==true)?"(超龄)":""
                    );
                if (PersonType == 1 || PersonType == 6)
                {
                    TR_Remark.Visible = true;//行政人员可以查看备注   
                }
                else if (ob.PostTypeID.Value == 5 && string.IsNullOrEmpty(ob.Remark) == false && ob.Remark.Contains("建设部系统（新证书编号") == true)
                {
                    TR_Remark.Visible = true;
                }

                if (ob.ValidStartDate.HasValue == false || ob.ValidEndDate.HasValue == false)
                {
                    LabelValidate.Text = "";
                }
                else if (ob.ValidEndDate.Value.ToString("yyyy-MM-dd") == "2050-01-01")
                {
                    if (PersonType == 2 || PersonType == 3 || PersonType == 4)
                    {
                        LabelValidate.Text = "当前有效证书";
                    }
                    else
                    {
                        LabelValidate.Text = ob.ValidEndDate.Value.ToString("yyyy.MM.dd");
                    }
                }
                else
                {
                    LabelValidate.Text = ob.ValidEndDate.Value.ToString("yyyy.MM.dd");
                }

                if (string.IsNullOrEmpty(ob.WorkerCertificateCode) == false)
                {
                    //ImgCode.Src = UIHelp.ShowFaceImage(ob.FacePhoto,ob.WorkerCertificateCode);
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(ob.FacePhoto, ob.WorkerCertificateCode);
                }

                if (ob.ValidEndDate.Value.AddDays(1) < DateTime.Now || ob.Status == "注销" || ob.Status == "离京变更")
                {
                    TR_Disabled.Visible = true;

                }
                if (ob.PostTypeID == 4000)
                {
                    LabelConferUnit.Text = ob.TrainUnitName;
                    LabelFZJG.Text = "培训机构";
                }
               

                RefreshLockStatus(ob.CertificateID.Value);
            }
        }

        ///// <summary>
        ///// 获取个人照片地址：先查当时报名照片，没有找个人最新上传照片
        ///// </summary>
        ///// <param name="CertificateOB">证书对象</param>
        ///// <returns>照片位置</returns>
        //public string GetFacePhotoPath(CertificateOB ob)
        //{
        //    if (string.IsNullOrEmpty(ob.WorkerCertificateCode) == true) return "~/Images/photo_ry.jpg";
        //    string path = "";

        //    if (ob.ExamPlanID.Value > 0)//通过报名考试系统发证                
        //    {
        //        path = string.Format("../UpLoad/SignUpPhoto/{0}/{1}.jpg", ob.ExamPlanID.Value.ToString(), ob.WorkerCertificateCode);
        //        if (File.Exists(Server.MapPath(path)) == true) return path;
        //    }

        //    path = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", ob.WorkerCertificateCode.Substring(ob.WorkerCertificateCode.Length - 3, 3), ob.WorkerCertificateCode);
        //    if (File.Exists(Server.MapPath(path)) == true)
        //        return path;
        //    else
        //        return "~/Images/photo_ry.jpg";
        //}

        
        //修改
        protected void ButtonModify_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CertificateInfoModify.aspx?o={0}&type={1}", Request.QueryString["o"], Request.QueryString["type"]));
        }

        //返回
        protected void ButtonFH_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.QueryString["type"]);
        }

        //获取联系电话
        private string GetPhone(CertificateOB ob)
        {
            string rtn="";
            try
            {
                //从人员基本信息中查询
                DataTable tdWorker = WorkerDAL.GetList(0, 1, string.Format("and CertificateCode='{0}' and Phone <>'' and Phone is not null", ob.WorkerCertificateCode), "WorkerID");
                if (tdWorker != null && tdWorker.Rows.Count > 0) return tdWorker.Rows[0]["Phone"].ToString();

                //从变更申请表中查询
                DataTable tdChange = CertificateChangeDAL.GetList(0, 1, string.Format("and CertificateID={0}", ob.CertificateID.ToString()), "ApplyDate desc");
                if (tdChange != null && tdChange.Rows.Count > 0) return tdChange.Rows[0]["LinkWay"].ToString();

                //从续期申请表中查询
                DataTable tdContinue = CertificateContinueDAL.GetList(0, 1, string.Format("and CertificateID={0}", ob.CertificateID.ToString()), "ApplyDate desc");
                if (tdContinue != null && tdContinue.Rows.Count > 0) return tdContinue.Rows[0]["Phone"].ToString();
            }
            catch { }
            return rtn;
        }

        //加锁
        protected void ButtonLock_Click(object sender, EventArgs e)
        {
            if (ButtonLock.Text == "加 锁")
            {
                Response.Redirect(string.Format("CertificateLock.aspx?o={0}&type={1}", Request.QueryString["o"], Request.QueryString["type"]));
            }
            else if (ButtonLock.Text == "解 锁")
            {
                CertificateLockOB o = CertificateLockDAL.GetLastObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request.QueryString["o"])));

                o.UnlockPerson = PersonName;
                o.UnlockTime = DateTime.Now;
                o.LockStatus = "解锁";
                try
                {
                    CertificateLockDAL.Update(o);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "证书解锁失败！", ex);
                    return;
                }
                RefreshLockStatus(o.CertificateID.Value);
                UIHelp.WriteOperateLog(PersonName, UserID, "证书解锁", string.Format("证书编号：{0}。", LabelCertificateCode.Text));
            }
        }
      

        /// <summary>
        /// 刷新证书锁定状态
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        private void RefreshLockStatus(long CertificateID)
        {
            //证书锁定状态           
            bool lockStatus = CertificateLockDAL.GetCertificateLockStatus(CertificateID);
            if (lockStatus==false)//已解锁
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
            q.Add(string.Format("CertificateID = {0}",CertificateID.ToString()));//证书ID
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridLock.CurrentPageIndex = 0;
            RadGridLock.DataSourceID = ObjectDataSource1.ID;

            if (PersonType == 1 || PersonType == 6)
            {
                RadGridLock.MasterTableView.Columns.FindByUniqueName("LockPerson").Visible = true;
                RadGridLock.MasterTableView.Columns.FindByUniqueName("UnlockPerson").Visible = true;
            }
        }

        //注销
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if(RadTextBoxCertRemark.Text.Trim()=="")
            {
                UIHelp.layerAlert(Page, "必须填写注销原因。");
                return;
            }

            CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request.QueryString["o"])));
            ob.Status = EnumManager.CertificateUpdateType.Logout;
            ob.ModifyPersonID = PersonID;
            ob.ModifyTime = DateTime.Now;
            ob.CheckDate = ob.ModifyTime;
            ob.Remark = string.Format("{0}[{1}日市建委执行注销，注销原因：{2}]", ob.Remark, DateTime.Now.ToString("yyyy-MM-dd"),RadTextBoxCertRemark.Text.Trim());

            try
            {
                CertificateDAL.Update(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "证书双证注销失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "证书双证注销", string.Format("证书编号：{0}", ob.CertificateCode));

            P_Remark.InnerText = ob.Remark;//备注
            LabelStatus.Text = string.Format("{0}{1}"
                  , ob.Status
                  , (string.IsNullOrEmpty(ob.Remark) == false && ob.Remark.Contains("超龄") == true) ? "(超龄)" : ""
                  );

            divCancelReison.Visible = false;
            divCertInfo.Visible = true;
        }

        //填写注销原因
        protected void ButtonApplyCancel_Click(object sender, EventArgs e)
        {
            divCancelReison.Visible = true;
            divCertInfo.Visible = false;
        }
        //取消注销
        protected void ButtonBak_Click(object sender, EventArgs e)
        {
            divCancelReison.Visible = false;
            divCertInfo.Visible = true;
        }
    }
}
