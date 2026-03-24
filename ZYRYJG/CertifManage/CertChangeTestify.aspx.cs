using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.IO;

namespace ZYRYJG.CertifManage
{
    public partial class CertChangeTestify : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifChange.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string CertificateID = Utility.Cryptography.Decrypt(Request["o"]);
                CertificateOB _CertificateOB = CertificateDAL.GetObject(Convert.ToInt64(CertificateID));
                ViewState["CertificateOB"] = _CertificateOB;
                CertificateChangeOB certhfchange = CertificateChangeDAL.GetObjectOfLiJing(Convert.ToInt64(CertificateID));
                if (certhfchange == null)
                {
                    DivDetail.InnerText = "没有找到可用的离京证明，请核实是否在系统中做过离京变更申请，并审批通过！";
                }
                else
                {
                    //三类人证书离京业务办结后，在生成下载离京批准书的同时保留该证书原始电子证书(保留该证书到有效期截止日期）与离京同意书同时下载
                    if (certhfchange.ApplyDate > Convert.ToDateTime("2024-01-18") && _CertificateOB.ValidEndDate.Value.AddDays(1) >DateTime.Now)
                    {
                        ButtonDownPdf.Visible = true;
                    }
                    var ht = PrintDocument.GetProperties(certhfchange);
                    ht["Data_ApplyCode"] = certhfchange.ApplyCode;//申请批次号
                    ht["Data_ChangeType"] = certhfchange.ChangeType;//变更类型
                    ht["Data_ApplyDate"] = Convert.ToDateTime(certhfchange.ApplyDate.ToString()).ToString("yyyy年MM月dd日");//申请日期
                    ht["Data_PostID"] = _CertificateOB.PostName;  //证书类别
                    ht["Data_CertificateCode"] = _CertificateOB.CertificateCode;  //证书编号
                    ht["Data_WorkerName"] = certhfchange.WorkerName;  //姓名
                    ht["Data_Sex"] = certhfchange.Sex;//性别
                    ht["Data_Birthday"] = certhfchange.Birthday.Value.ToString("yyyy-MM-dd");  //出身日期
                    ht["Data_WorkerCertificateCode"] = certhfchange.WorkerCertificateCode;//证件号码            
                    ht["Data_UnitName"] = certhfchange.UnitName;//原工作单位   
                    ht["Data_UnitCode"] = certhfchange.UnitCode; //原机构代码          
                    ht["Data_LinkWay"] = certhfchange.LinkWay; //联系电话
                    ht["ConferDate"] = _CertificateOB.ConferDate.Value.ToString("yyyy年MM月dd日"); //发证日期
                    ht["ValidDate"] = _CertificateOB.ValidEndDate.Value.ToString("yyyy年MM月dd日"); //有效期
                    ht["FacePhoto"] = certhfchange.WorkerCertificateCode;//照片标签
                    ht["Img_FacePhoto"] = UIHelp.GetFaceImagePath(_CertificateOB.FacePhoto, certhfchange.WorkerCertificateCode);//绑定照片
                    ht["NOTICEDATE"] = certhfchange.NoticeDate.Value.ToString("yyyy年MM月dd日");  //决定日期
                    //ht["ChangeRemark"] =  certhfchange.ChangeRemark;//备注（离京变更在备注中显示：拟调入省份）
                    ht["ChangeRemark"] = "";//备注

                    ht["photo"] = UIHelp.GetFaceImagePath(_CertificateOB.FacePhoto, _CertificateOB.WorkerCertificateCode);//绑定照片

                    //输出二维码  
                    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", _CertificateOB.PostID, _CertificateOB.CertificateCode));
                    System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 200, 200);
                    string TCodePath = string.Format("~/Upload/CertifChangeApply/{0}.png", certhfchange.CertificateChangeID);
                    imgtemp.Save(Server.MapPath(TCodePath), System.Drawing.Imaging.ImageFormat.Png);
                    ht["photo_code"] = TCodePath;

                    ViewState["ht"] = ht;

                    DivDetail.InnerText = "已经为您 准备好了下载数据，请点击下载相应内容！";
                }
            }
        }

        //下载电子证书
        protected void ButtonDownPdf_Click(object sender, EventArgs e)
        {
            CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];

            string pdfUrl = Server.MapPath(string.Format("../Upload/pdf/lijing/{0}.pdf", ob.CertificateID));
            if (RootUrl.Contains("120.52.185.14") == true && File.Exists(pdfUrl) == false)
            {
                UIHelp.layerAlert(Page, "该证书尚无电子证书！");
                return;
            }

            try
            {
                byte[] file = Utility.ImageHelp.FileToByte(pdfUrl);

                #region 记录下载
                FileDownLogMDL o = new FileDownLogMDL();
                o.DownMAN = UserName;
                o.DownManID = UserID;
                o.DownTime = DateTime.Now;
                o.FileTypeCode = EnumManager.FileTypeCode.CA;
                o.FileID = ob.CertificateCAID;
                o.DownFileName = string.Format("{0}.pdf", ob.CertificateCode);
                FileDownLogDAL.Insert(o);
                #endregion

                Response.Clear();
                Response.ContentType = "application/pdf";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment;  filename=" + string.Format("{0}.pdf", Server.UrlEncode(ob.CertificateCode)));
                Response.BinaryWrite(file);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "下载电子证书失败", ex);
                return;
            }
        }

        //下载离京证明
        protected void ButtonDownDoc_Click(object sender, EventArgs e)
        {
            string template = "~/Template/变更离京证明.docx";
            var ht = ViewState["ht"] as System.Collections.Hashtable;
            PrintDocument.CreateDataToWordByHashtable(Server.MapPath(template), string.Format("变更离京证明_{0}", PersonID), ht);
        }

        ////准备导出或打印标签替换数据
        //protected System.Collections.Generic.Dictionary<string, string> GetExportData(CertificateOB _CertificateOB,CertificateChangeOB certhfchange)
        //{
        //    System.Collections.Generic.Dictionary<string, string> printData = new Dictionary<string, string>();

        //    printData.Add("Data_ApplyCode", certhfchange.ApplyCode);//申请批次号
        //    printData.Add("Data_ChangeType", certhfchange.ChangeType);//变更类型
        //    printData.Add("Data_ApplyDate", Convert.ToDateTime(certhfchange.ApplyDate.ToString()).ToString("yyyy年MM月dd日"));//申请日期RadDatePickerApplyDate.SelectedDate.ToString()
        //    printData.Add("Data_PostID", _CertificateOB.PostName);  //证书类别
        //    printData.Add("Data_CertificateCode", _CertificateOB.CertificateCode);  //证书编号
        //    printData.Add("Data_WorkerName", certhfchange.WorkerName);  //姓名
        //    printData.Add("Data_Sex", certhfchange.Sex);//性别
        //    printData.Add("Data_Birthday", certhfchange.Birthday.Value.ToString("yyyy年MM月dd日"));  //出身日期
        //    printData.Add("Data_WorkerCertificateCode", certhfchange.WorkerCertificateCode);//证件号码            
        //    printData.Add("Data_UnitName", certhfchange.UnitName);//原工作单位   
        //    printData.Add("Data_UnitCode", certhfchange.UnitCode); //原机构代码          
        //    printData.Add("Data_LinkWay", certhfchange.LinkWay); //联系电话
        //    printData.Add("ConferDate", _CertificateOB.ConferDate.Value.ToString("yyyy年MM月dd日")); //发证日期
        //    printData.Add("ValidDate", _CertificateOB.ValidEndDate.Value.ToString("yyyy年MM月dd日")); //有效期
        //    printData.Add("FacePhoto", certhfchange.WorkerCertificateCode);//照片标签
        //    printData.Add("Img_FacePhoto", UIHelp.GetFaceImagePath(_CertificateOB.FacePhoto, certhfchange.WorkerCertificateCode));//绑定照片
        //    printData.Add("NOTICEDATE", certhfchange.NoticeDate.Value.ToString("yyyy年MM月dd日"));  //决定日期
        //    //printData.Add("ChangeRemark", certhfchange.ChangeRemark);//备注（离京变更在备注中显示：拟调入省份）
        //    printData.Add("ChangeRemark", "");//备注
           
            

        //    //输出二维码  
        //    string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", _CertificateOB.PostID, _CertificateOB.CertificateCode));
        //    System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 200, 200);
        //    string TCodePath = string.Format("../Upload/CertifChangeApply/{0}.png", certhfchange.CertificateChangeID);
        //    imgtemp.Save(Server.MapPath(TCodePath), System.Drawing.Imaging.ImageFormat.Png);
        //    printData.Add("ImageTCodeName", certhfchange.CertificateID.ToString());
        //    printData.Add("Img_TCode", TCodePath);


        //    return printData;
        //}
    }
}