using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignLockView : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ExamSignUpOB"] == null ? "" : string.Format("KS-{0}", (ViewState["ExamSignUpOB"] as ExamSignUpOB).ExamSignUpID); }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "EXamManage/ExamSignLock.aspx"; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(Convert.ToInt64(Request["o"]));
                if (_ExamSignUpOB != null)
                {
                    ViewState["ExamSignUpOB"] = _ExamSignUpOB;
                    ExamPlanOB explanOB = ExamPlanDAL.GetObject(_ExamSignUpOB.ExamPlanID.Value);//考试计划信息  
                    LabelExamStart.Text = explanOB.ExamStartDate.Value.ToString("yyyy-MM-dd");//考试时间
                    LabelLockTime.Text = _ExamSignUpOB.LockTime.Value.ToString("yyyy-MM-dd");//锁定截止时间
                    //LabelLockTime.Text = _ExamSignUpOB.LockTime.Value.ToString("yyyy-MM-dd");//锁定截止时间

                    UIHelp.SetData(divExamSignUp, explanOB, true);
                    ViewState["PostTypeID"] = explanOB.PostTypeID;
                    ViewState["ExamPlanID"] = _ExamSignUpOB.ExamPlanID;
                    ViewState["SignUpDate"] = _ExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd");

                    //照片
                    System.Random rm = new Random();
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_ExamSignUpOB.ExamPlanID.ToString(), _ExamSignUpOB.CertificateCode))); //绑定照片;
 
                    //考生信息
                    WorkerOB _WorkerOB = WorkerDAL.GetObject(_ExamSignUpOB.WorkerID.Value);
                    if (_WorkerOB != null)
                    {
                        UIHelp.SetData(divExamSignUp, _WorkerOB, true);
                    }

                    //报名信息    
                    UIHelp.SetData(divExamSignUp, _ExamSignUpOB, true);

                    UIHelp.SetReadOnly(RadDatePickerLockEndTime, false);//允许修改解锁时间

                    BindFile(ApplyID);
                }
            }
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/photo_ry.jpg";
            }
        }
               
        //检查图片存放路径
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()));
        }


        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyID"></param>
        private void BindFile(string ApplyID)
        {
            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(ApplyID);
            DataTable HB_File = dt_ApplyFile.Clone();
            HB_File.Columns["FileUrl"].MaxLength = 8000;

            string DataType = "";
            foreach (DataRow r in dt_ApplyFile.Rows)
            {
                if (r["DataType"].ToString() != DataType)
                {

                    HB_File.ImportRow(r);
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0}|{1}", r["FileUrl"], r["FileID"]);
                    DataType = r["DataType"].ToString();
                }
                else
                {
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0},{1}|{2}", HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"], r["FileUrl"], r["FileID"]);
                }
            }

            RadGridFile.DataSource = HB_File;
            RadGridFile.DataBind();
        }

        //格式化附件
        protected void RadGridFile_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGrid rg = item.FindControl("RadGrid1") as RadGrid;

                DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID("1"); ;

                string ApplyID = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();

                string[] imgurl = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FileUrl"].ToString().Split(',');
                string[] atrt = null;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string s in imgurl)
                {
                    DataRow dr = dt_ApplyFile.NewRow();

                    atrt = s.Split('|');
                    dr["FileUrl"] = atrt[0];
                    dr["FileID"] = atrt[1];
                    dr["ApplyID"] = ApplyID;
                    dt_ApplyFile.Rows.Add(dr);
                }

                rg.DataSource = dt_ApplyFile;
                rg.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            ExamSignUpOB _ExamSignUpOB = ViewState["ExamSignUpOB"] as ExamSignUpOB;

            DateTime old = _ExamSignUpOB.LockEndTime.Value;

            _ExamSignUpOB.LockEndTime = RadDatePickerLockEndTime.SelectedDate.Value;

            try
            {
                ExamSignUpDAL.Update(_ExamSignUpOB);

                UIHelp.WriteOperateLog(PersonName, UserID, "修改报名违规锁定截止日期", string.Format("报名批次号：{0}；考试计划：{1}；岗位工种：{2}；报名人：{3}；原锁定截止日期：{4}；现锁定截止日期：{5}",
                _ExamSignUpOB.SignUpCode,
                RadTextBoxExamPlanName.Text,
                RadTextBoxPostName.Text,
                _ExamSignUpOB.WorkerName,
                old.ToString("yyyy-MM-dd"),
                 _ExamSignUpOB.LockEndTime.Value.ToString("yyyy-MM-dd")
                ));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修改报名违规锁定截止日期失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, "保存成功。", "hideIfam(true);");
            ViewState["ExamSignUpOB"] = _ExamSignUpOB;
        }

    }
}
