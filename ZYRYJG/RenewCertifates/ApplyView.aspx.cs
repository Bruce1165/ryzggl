using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.RenewCertifates
{
    public partial class ApplyView : BasePage
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
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    //续期申请信息
                    CertificateContinueOB _CertificateContinueOB = CertificateContinueDAL.GetObject(Convert.ToInt64(Request["o"]));
                    if (_CertificateContinueOB != null)
                    {
                        LabelUnitCode.Text = _CertificateContinueOB.UnitCode;   //机构代码
                        LabelPhone.Text = _CertificateContinueOB.Phone;//电话
                        LabelApplyCode.Text = _CertificateContinueOB.ApplyCode;//续期申请批号     

                        //证书信息
                        CertificateOB o = CertificateDAL.GetObject(_CertificateContinueOB.CertificateID.Value);
                        if (o != null)
                        {
                            LabelUnitName.Text = o.UnitName;   //机构名称
                            LabelWorkerCertificateCode.Text = o.WorkerCertificateCode;  //证件号码
                            LabelWorkerName.Text = o.WorkerName;    //姓名   
                            LabelSex.Text = o.Sex;//性别
                            LabelBirthday.Text = o.Birthday.HasValue == false ? "" : o.Birthday.Value.ToString("yyyy-MM-dd");//生日
                            LabelCertificateCode.Text = o.CertificateCode ;//证书编号
                            LabelPrintCount.Text =  "";
                            LabelValidDataTo.Text = o.ValidEndDate.HasValue == false ? "" : o.ValidEndDate.Value.ToString("yyyy-MM-dd");//有效期
                            //LabelPostName.Text = PostInfoDAL.GetObject(o.PostID.Value).PostName;//岗位工种

                            //岗位工种
                            if (o.PostTypeID == 3 && string.IsNullOrEmpty(o.AddItemName) == false)//造价员
                            {
                                LabelPostName.Text = o.AddItemName;
                            }
                            else
                            {
                                LabelPostName.Text = PostInfoDAL.GetObject(o.PostID.Value).PostName;
                            }

                            if (o.PostTypeID == 2)
                            {
                                LabelNewUnitName.Text = _CertificateContinueOB.NewUnitName;   //现单位名称
                                LabelNewUnitCode.Text = _CertificateContinueOB.NewUnitCode;   //现单位机构代码
                                LabelUnit.Text = "原单位名称";
                                LabelCode.Text = "原单位组织机构代码";
                                trNewUnit.Visible = true;
                            }

                            //System.Random rm = new Random();
                            //ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(o.WorkerCertificateCode))); //绑定照片;


                            //ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(o.FacePhoto, o.WorkerCertificateCode)); //绑定照片;

                            ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(o.FacePhoto, o.WorkerCertificateCode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }       
    }
}