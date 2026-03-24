using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.IO;

namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterApplyView : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("2").Remove();//屏蔽特种作业类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("4").Remove();//屏蔽职业技能类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别
        }
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifEnterApplyList.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImgCode.Src = "~/Images/photo_ry.jpg";
                ViewState["rtn"] = (string.IsNullOrEmpty(Request["rtn"]) == true) ? "CertifEnterApplyList.aspx" : Request["rtn"];
                if (string.IsNullOrEmpty(Request["t"]) == false)
                {
                    ViewState["PostTypeID"] = Request["t"];//岗位类别ID
                    PostSelect1.PostTypeID = Request["t"];
                    PostSelect1.LockPostTypeID();
                }
                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    string applyid = Utility.Cryptography.Decrypt(Request["o"]);
                    CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(applyid));
                    PostSelect1.PostID = _CertificateEnterApplyOB.PostID.ToString();
                    PostSelect1.Enabled = false;
                    UIHelp.SetData(TableEdit, _CertificateEnterApplyOB);

                    if (PostSelect1.PostTypeID == "3")//造价员增项
                    {
                        CheckBoxAddItem.Style.Add("display", "inline");
                        if (PostSelect1.PostID == "9")//土建
                        {
                            CheckBoxAddItem.Text = "增安装";
                        }
                        else//=12 安装
                        {
                            CheckBoxAddItem.Text = "增土建";
                        }
                        if (string.IsNullOrEmpty(_CertificateEnterApplyOB.AddPostID) == false) CheckBoxAddItem.Checked = true;
                    }

                    LabelApplyCode.Text = _CertificateEnterApplyOB.ApplyCode; //批次号
                    LabelApplyDate.Text = _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd");
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_CertificateEnterApplyOB.WorkerCertificateCode)));
                    if (_CertificateEnterApplyOB.Sex == "男")  //性别
                    {
                        RadioButtonMan.Checked = true;
                        RadioButtonWoman.Checked = false;
                    }
                    else
                    {
                        RadioButtonMan.Checked = false;
                        RadioButtonWoman.Checked = true;
                    }

                    //保存后不能修改证件号码
                    RadTextBoxWorkerCertificateCode.Enabled = false;
                }                
            }
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
