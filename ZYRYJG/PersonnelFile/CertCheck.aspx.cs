using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DataAccess;
using Model;

namespace ZYRYJG.PersonnelFile
{
    public partial class CertCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    string[] keys = Utility.Cryptography.Decrypt(Request["o"].Replace(" ", "+")).Split(',');
            //    string postID = keys[0];
            //    string CertificateCode = keys[1];
            //    System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
            //    divPhoto.Controls.Add(img1);
            //    img1.ImageUrl = string.Format(@"~/Upload/PhotoCertificate/{0}/{1}.png", postID, CertificateCode);
            //    img1.BorderStyle = BorderStyle.Solid;
            //    img1.BorderColor = Color.Gray;
            //    img1.BorderWidth = new Unit("1px");
            //}
            //catch(Exception ex)
            //{
            //    Utility.FileLog.WriteLog(string.Format("读取证书二维码失败，二维码【{0}】",Request["o"]), ex);
            //    UIHelp.layerAlert(Page, "读取证书二维码失败！");
            //}


            try
            {
                string[] keys = Utility.Cryptography.Decrypt(Request["o"].Replace(" ", "+")).Split(',');
                string postID = keys[0];
                string CertificateCode = keys[1];

                //***********新版国标电子证书实施后打开注释********************************
                string TZZYList = ",120,,123,,126,,129,,132,,135,,138,,141,,144,,151,,152,,299,";
                CertificateOB ob = null;
                if (TZZYList.Contains(string.Format(",{0},", postID)) == true)
                {
                    ob = CertificateDAL.GetCertificateByJZB_CERTIFICATECODE(postID, CertificateCode);
                }
                else
                {
                    ob = CertificateDAL.GetCertificateOBObject("", postID, CertificateCode.Replace("(", "（").Replace(")", "）"));
                }

               
                //****************************************

                //CertificateOB ob = CertificateDAL.GetCertificateOBObject("", postID, CertificateCode);


                LabelWorkerName.Text = ob.WorkerName;
                LabelUnitName.Text = ob.UnitName;
                LabelUnitCode.Text = ob.UnitCode;
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
                LabelBirthday.Text = ob.Birthday.HasValue == false ? "" : ob.Birthday.Value.ToString("yyyy年MM月dd日");
                LabelWorkerCertificateCode.Text = ob.WorkerCertificateCode;
                LabelCertificateCode.Text = ob.CertificateCode;
                LabelSKILLLEVEL.Text = ob.SkillLevel;//技术等级
                LabelConferUnit.Text = ob.ConferUnit;
                if (ob.PostTypeID == 1 || ob.PostTypeID == 2)
                {
                    td_tip.Visible = true;
                    LabelConferDate.Text = ob.CheckDate.HasValue == false ? "" : ob.CheckDate.Value.ToString("yyyy年MM月dd日");//发证日期
                }
                else
                {
                    LabelConferDate.Text = ob.ConferDate.HasValue == false ? "" : ob.ConferDate.Value.ToString("yyyy年MM月dd日");//发证日期
                }
                LabelStatus.Text = string.Format("{0}{1}"
                       , ob.Status
                       , (string.IsNullOrEmpty(ob.Remark) == false && ob.Remark.Contains("超龄") == true) ? "(超龄)" : ""
                       );//状态

                if(ob.Status == "离京变更")
                {
                    try
                    {
                        System.Data.DataTable dt = CommonDAL.GetDataTable(string.Format(@"
                        select top 1 [ChangeRemark]
                        FROM [dbo].[CERTIFICATECHANGE]
                        where [CERTIFICATEID]={0} and [CHANGETYPE]='离京变更' and [STATUS]='已告知'
	                    order by [NOTICEDATE] desc", ob.CertificateID));

                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] !=DBNull.Value)
                        {
                            LabelStatus.Text = string.Format("{0}（{1}）", LabelStatus.Text, dt.Rows[0][0]);
                        }
                    }
                    catch { }
                }

                if (ob.ValidEndDate.Value.AddDays(1) < DateTime.Now || ob.Status == "注销" || ob.Status == "离京变更")//备注信息
                {
                    LabelDesc.Text = "此证书为无效证书（过期、离京、注销），不能办理任何业务。";
                    LabelDesc.ForeColor = Color.Red;
                }
                else if (ob.PostTypeID.Value == 5 && string.IsNullOrEmpty(ob.Remark) == false && ob.Remark.Contains("建设部系统（新证书编号") == true)
                {
                    LabelDesc.Text = ob.Remark;
                }
                else
                {
                    LabelDesc.Text = "　";
                }

                if (ob.ValidStartDate.HasValue == false || ob.ValidEndDate.HasValue == false)//有效期
                    LabelValidate.Text = "";
                else if(ob.ValidEndDate.Value.ToString("yyyy.MM.dd")=="2050.01.01")
                    LabelValidate.Text = "当前有效证书";
                else
                    LabelValidate.Text = ob.ValidEndDate.Value.ToString("yyyy年MM月dd日");

                if (string.IsNullOrEmpty(ob.WorkerCertificateCode) == false)//照片
                {
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(ob.FacePhoto, ob.WorkerCertificateCode);
                }
                //if (ob.ApplyCATime.HasValue == true)//制证日期
                //{
                //    LabelZZRQ.Text = ob.ApplyCATime.Value.ToString("yyyy年MM月dd日");
                //}
                //else if(ob.PrintDate.HasValue==true)
                //{
                //    LabelZZRQ.Text = ob.PrintDate.Value.ToString("yyyy年MM月dd日");
                //}

                if (ob.PostTypeID.Value == 4 || ob.PostTypeID.Value == 5 || ob.PostTypeID.Value == 4000)
                {
                    trUnit.Visible = false;
                    trUnitCode.Visible = false;
                    //trValidate.Visible = false;
                }

                if(ob.PostTypeID.Value == 4000)
                {
                    trTrainUnit.Visible = true;
                    LabelTrainUnit.Text = ob.TrainUnitName;
                }
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog(string.Format("读取证书二维码失败，二维码【{0}】", Request["o"]), ex);
                UIHelp.layerAlert(Page, "读取证书二维码失败！");
            }
        }
    }
}