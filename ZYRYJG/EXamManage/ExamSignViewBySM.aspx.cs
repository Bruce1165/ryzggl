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
    public partial class ExamSignViewBySM : BasePage
    {
     
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CheckSignFirst.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindGrid();
                //Form.DefaultButton = ButtonSearch.UniqueID;
                //RadTextBoxExamSignupID.Focus();
            }
        }

        ///// <summary>
        ///// 获取个人照片地址
        ///// </summary>
        ///// <param name="ExamPlanID">考试计划ID</param>
        ///// <param name="CertificateCode">证件号码</param>
        ///// <returns></returns>
        //public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        //{
        //    if (CertificateCode == "") return "~/Images/photo_ry.jpg";
        //    string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
        //    if (File.Exists(Server.MapPath(path)) == true)
        //        return path;
        //    else
        //    {
        //        path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
        //        if (File.Exists(Server.MapPath(path)) == true)
        //            return path;
        //        else
        //            return "~/Images/photo_ry.jpg";
        //    }
        //}

        ////检查图片存放路径
        //protected void CheckSaveDirectory()
        //{
        //    //考试报名表存放路径(按考试计划ID分类)
        //    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
        //    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()));
        //}

        ////显示社保比对结果
        //private void ShowSheBao(byte? SheBaoCheck, string CertificateCode, string UnitCode, string SignUpDate)
        //{
        //    divSheBao.InnerHtml = string.Format("{3}<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>查看详细</nobr></span>", CertificateCode, UnitCode, SignUpDate
        //        , SheBaoCheck.HasValue == false ? "社保尚未开始比对（夜间）" : SheBaoCheck.Value==1?"社保符合要求":"社保不符合要求"
        //        );
        //}

        ////审核通过
        //protected void ButtonCheck_Click(object sender, EventArgs e)
        //{
        //    ExamSignUpOB _ExamSignUpOB = ViewState["ExamSignUpOB"] as ExamSignUpOB;

        //    _ExamSignUpOB.Status = EnumManager.SignUpStatus.FirstChecked;
        //    _ExamSignUpOB.ModifyPersonID = PersonID;
        //    _ExamSignUpOB.ModifyTime = DateTime.Now;
        //    _ExamSignUpOB.FIRSTTRIALTIME = _ExamSignUpOB.ModifyTime;
        //    _ExamSignUpOB.TrainUnitID = PersonID;
        //    _ExamSignUpOB.S_TRAINUNITNAME = PersonName;

        //    try
        //    {
        //        ExamSignUpDAL.Update(_ExamSignUpOB);
        //        UIHelp.WriteOperateLog(PersonName, UserID, "报名初审", string.Format("初审通过,姓名：{0}，身份证号：{1}，报名批次号：{2}。", _ExamSignUpOB.WorkerName, _ExamSignUpOB.CertificateCode, _ExamSignUpOB.SignUpCode));
        //    }
        //    catch (Exception ex)
        //    {

        //        UIHelp.WriteErrorLog(Page, "考试报名审核失败！", ex);
        //        return;
        //    }

        //    divShaoMa.Visible = true;
        //    divExamSignUp.Visible = false;
        //    RadTextBoxExamSignupID.Focus();
        //    BindGrid();
        //}

        ////不审核关闭
        //protected void ButtonClose_Click(object sender, EventArgs e)
        //{
        //    divExamSignUp.Visible = false;
        //    divShaoMa.Visible = true;
        //    RadTextBoxExamSignupID.Focus();
        //}

        //protected void BindGrid()
        //{

        //    QueryParamOB q = new QueryParamOB();

        //    q.Add(string.Format("Status ='{0}'", EnumManager.SignUpStatus.FirstChecked));//已初审

        //    q.Add(string.Format("TrainUnitID ={0}", UnitID.ToString()));//查看自己的


        //    DataTable dt = ExamSignUpDAL.GetList_New(0, 5, q.ToWhereString(), "FirstTrialTime desc,ExamSignUpID");
           
        //    RadGridChecked.DataSource=dt;
        //    RadGridChecked.DataBind();
        //}

        ////查询
        //protected void ButtonSearch_Click(object sender, EventArgs e)
        //{
        //    if (RadTextBoxExamSignupID.Value.Trim() == "")
        //    {
        //        RadTextBoxExamSignupID.Focus();
        //        return;
        //    }

        //    ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(Convert.ToInt64(RadTextBoxExamSignupID.Value.Trim()));
        //    if (_ExamSignUpOB != null)
        //    {
        //        ExamPlanOB explanOB = ExamPlanDAL.GetObject(_ExamSignUpOB.ExamPlanID.Value);//考试计划信息  
        //        ViewState["ExamSignUpOB"] = _ExamSignUpOB;

        //        RadTextPostID.Text = UIHelp.FormatPostNameByExamplanName(explanOB.PostID.Value, explanOB.PostName, explanOB.ExamPlanName);

        //        //照片
        //        System.Random rm = new Random();
        //        ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_ExamSignUpOB.ExamPlanID.ToString(), _ExamSignUpOB.CertificateCode))); //绑定照片;

        //        //考生信息
        //        WorkerOB _WorkerOB = WorkerDAL.GetObject(_ExamSignUpOB.WorkerID.Value);
        //        if (_WorkerOB != null)
        //        {
        //            if (_WorkerOB.Birthday.HasValue == true)
        //            {
        //                RadDatePickerBirthday.Text = Convert.ToString(DateTime.Now.Year - _WorkerOB.Birthday.Value.Year); //年龄
        //            }
        //            RadioButtonMan.Text = _WorkerOB.Sex; //性别
        //            RadTextBoxPhone.Text = _WorkerOB.Phone;   //联系电话               
        //            RadTextBoxCulturalLevel.Text = _WorkerOB.CulturalLevel; //文化程度             
        //        }

        //        //报名信息                
        //        RadDatePickerWorkStartDate.Text = _ExamSignUpOB.WorkStartDate.HasValue ? _ExamSignUpOB.WorkStartDate.Value.ToString("yyyy年MM月dd日") : "";
        //        RadTextCertificateCode.Text = _ExamSignUpOB.CertificateCode;  //证件号码
        //        RadTextBoxWorkerName.Text = _ExamSignUpOB.WorkerName;    //姓名
        //        RadTextBoxUnitName.Text = _ExamSignUpOB.UnitName;   //机构名称
        //        RadTextBoxUnitCode.Text = _ExamSignUpOB.UnitCode;   //机构代码
        //        RadTextBoxSKILLLEVEL.Text = _ExamSignUpOB.SKILLLEVEL; //技术职称或技术等级
        //        lblSignUpCode.Text = _ExamSignUpOB.SignUpCode;//报名批号
        //        LabelExamSignupDate.Text = _ExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd");
        //        LabelSignupMan.Text = _ExamSignUpOB.SignUpMan;
        //        LabelPlace.Text = _ExamSignUpOB.PlaceName;
        //        if (_ExamSignUpOB.CheckDatePlan.HasValue == true)
        //        {
        //            LabelCheckDatePlan.Text = _ExamSignUpOB.CheckDatePlan.Value.ToString("yyyy-MM-dd");
        //        }
        //        LabelStatus.Text = _ExamSignUpOB.Status;


        //        if (_ExamSignUpOB.S_BIRTHDAY.HasValue == true)
        //        {
        //            RadDatePickerBirthday.Text = Convert.ToString(DateTime.Now.Year - _WorkerOB.Birthday.Value.Year); //年龄
        //        }
        //        RadioButtonMan.Text = _ExamSignUpOB.S_SEX;

        //        RadTextBoxPhone.Text = _ExamSignUpOB.S_PHONE;   //联系电话
        //        RadTextBoxCulturalLevel.Text = _ExamSignUpOB.S_CULTURALLEVEL; //文化程度


        //        if (explanOB.PostTypeID == 1//三类人
        //     || explanOB.PostTypeID == 5)//专业技术员
        //        {
        //            divCheckPlan.InnerHtml = (_ExamSignUpOB.FirstCheckType.Value == -1 ? "由于您一年内上次未参加考试，本次须现场审核报考材料并出具上次考试缺考原因的证明材料。" : "");//备注                      
        //        }
        //        else
        //        {
        //            divCheckPlan.InnerHtml = "";
        //        }



        //        divExamSignUp.Visible = true;
        //        divShaoMa.Visible = false;

        //        if (_ExamSignUpOB.Status == EnumManager.SignUpStatus.NewSignUp)
        //        {
        //            ButtonCheck.Visible = true;
        //            ButtonCheck.Focus();
        //        }
        //        else
        //        {
        //            ButtonCheck.Visible = false;
        //            ButtonClose.Focus();
        //        }


        //        ShowSheBao(_ExamSignUpOB.SheBaoCheck, _ExamSignUpOB.CertificateCode, _ExamSignUpOB.UnitCode, _ExamSignUpOB.SignUpDate.Value.ToString());
        //        RadTextBoxExamSignupID.Value = "";

        //        if (_ExamSignUpOB.TrainUnitID.HasValue == false || _ExamSignUpOB.TrainUnitID.Value.ToString() != UserID)
        //        {
        //            ButtonCheck.Visible = false;
        //            ButtonClose.Focus();
        //            UIHelp.layerAlert(Page, "您不应再本初审点审核，请按照报名表上提示地点去现场进行审核！");
        //        }

        //    }
        //    else
        //    {
        //        RadTextBoxExamSignupID.Value = "";
        //        RadTextBoxExamSignupID.Focus();
        //        return;
        //    }

        //}
    }
}
