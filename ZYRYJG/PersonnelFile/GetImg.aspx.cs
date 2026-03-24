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
using System.Text.RegularExpressions;

namespace ZYRYJG.PersonnelFile
{
    public partial class GetImg : BasePage
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
                string fun = "";
                if (string.IsNullOrEmpty(Request["e"])==false)
                {
                    fun = "ShowFaceImgByExamPlanIDAndCertificateCode";
                }
                else if (string.IsNullOrEmpty(Request["f"]) == false)
                {
                    fun = "ShowFaceImgByFacePhotoAndCertificateCode";
                }
                else if (string.IsNullOrEmpty(Request["w"]) == false)
                {
                    fun = "ShowFaceImgByWorkerCertificateCode";
                }

                string WorkerCertificateCode = "";
                string ExamPlanID = "";
                string FacePhoto = "";
                string[] k = null;
                string path = "";
                switch (fun)
                {
                    case "ShowFaceImgByExamPlanIDAndCertificateCode":
                        k = Utility.Cryptography.Decrypt(Request["e"]).Split(',');
                        ExamPlanID = k[0];
                        WorkerCertificateCode = k[1];
                        path = GetFacePhotoByExamPlanIDAndWorkerCertificateCode(ExamPlanID, WorkerCertificateCode);
                        break;
                    case "ShowFaceImgByFacePhotoAndCertificateCode":
                        k = Utility.Cryptography.Decrypt(Request["f"]).Split(',');
                        FacePhoto = k[0];                        
                        WorkerCertificateCode = k[1];
                        path = ShowFaceImageByFacePhotoAndWorkerCertificateCode(FacePhoto, WorkerCertificateCode);
                        break;
                    case "ShowFaceImgByWorkerCertificateCode":
                        k = Utility.Cryptography.Decrypt(Request["w"]).Split(',');
                        WorkerCertificateCode = k[0];
                        path = GetFacePhotoPath(WorkerCertificateCode);
                        break;
                    default:
                        break;
                }
                Response.ClearContent();
                Response.ContentType = "image/Jpeg";
                
                if (File.Exists(Server.MapPath(path)) == false) path = "~/Images/photo_ry.jpg";
                FileStream f = File.Open(Server.MapPath(path), FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(f);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Response.BinaryWrite(ms.ToArray());
                f.Close();
                f.Dispose();
                image.Dispose();
            }
        }

         /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="WorkerCertificateCode">证件号码</param>
        /// <returns></returns>
        protected string GetFacePhotoByExamPlanIDAndWorkerCertificateCode(string ExamPlanID, string WorkerCertificateCode)
        {
            if (WorkerCertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, WorkerCertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/photo_ry.jpg";
            }
        }

        /// <summary>
        /// 获取证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
        /// </summary>
        /// <param name="FacePhoto">照片路径</param>
        /// <param name="WorkerCertificateCode">证件号码</param>
        /// <returns></returns>
        protected string ShowFaceImageByFacePhotoAndWorkerCertificateCode(string FacePhoto, string WorkerCertificateCode)
        {
            string imgPath = "";
            if (string.IsNullOrEmpty(FacePhoto) == false)
            {
                if (File.Exists(Server.MapPath(FacePhoto)) == true)
                {
                    imgPath = FacePhoto;
                }
            }
            if (imgPath == "" && string.IsNullOrEmpty(WorkerCertificateCode) == false)
            {
                if (WorkerCertificateCode.Length > 2 && WorkerCertificateCode.IndexOf('?') == -1)
                {
                    string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
                    if (File.Exists(Server.MapPath(path)) == true)
                    {
                        imgPath = path;
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = "~/Images/photo_ry.jpg";
            }

            return imgPath;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string WorkerCertificateCode)
        {
            if (WorkerCertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }
      
    }
}
