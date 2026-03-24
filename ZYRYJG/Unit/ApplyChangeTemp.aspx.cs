using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Utility;

namespace ZYRYJG.Unit
{
    public partial class ApplyChangeTemp : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ApplyMDL"] == null ? "" : (ViewState["ApplyMDL"] as ApplyMDL).ApplyID; }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Unit/ApplyList.aspx|County/Agency.aspx|County/BusinessQuery.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
                return;
             

            }
            
        }
//        //禁用控件
//        private void Disabled()
//        {
//            UIHelp.SetReadOnly(RadTextBoxPSN_NameTo, true);
//            UIHelp.SetReadOnly(RadComboBoxToPSN_Sex, false);
//            UIHelp.SetReadOnly(RadDatePickerToPSN_BirthDate, false);
//            UIHelp.SetReadOnly(RadTextBoxTo_ZGZSBH, true);//证件类别
//            UIHelp.SetReadOnly(RadTextBoxToPSN_CertificateNO, true);

//        }
//        /// <summary>
//        /// 绑定审核历史记录
//        /// </summary>
//        /// <param name="ApplyID">申请编号</param>
//        private void BindCheckHistory(string ApplyID)
//        {
//            if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)
//            {
//                DataTable dt = ApplyDAL.GetCheckHistoryList(ApplyID);
//                RadGridCheckHistory.DataSource = dt;
//                RadGridCheckHistory.DataBind();
//            }
//            else
//            {
//                DataTable dt = ApplyDAL.GetCheckHistoryList2(ApplyID);
//                RadGridCheckHistory.DataSource = dt;
//                RadGridCheckHistory.DataBind();
//            }
//        }

//        //保存
//        protected void ButtonSave_Click(object sender, EventArgs e)
//        {

//            #region 手机号、邮箱校验


//            if (RadTextBoxPSN_MobilePhone.Text.Trim() == "" || !Check.IfTelPhoneFormat(RadTextBoxPSN_MobilePhone.Text.Trim()))
//            {
//                UIHelp.layerAlert(Page, "保存失败，手机号码错误！", 6, 0);
//                return;
//            }

//            if (RadTextBoxPSN_Email.Text.Trim() == "" || !Check.IfMailFormat(RadTextBoxPSN_Email.Text.Trim()))
//            {
//                UIHelp.layerAlert(Page, "保存失败，邮箱错误！", 6, 0);
//                return;
//            }
//            #endregion

//            //COC_TOW_Person_BaseInfoMDL mdl=( COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
//            COC_TOW_Person_BaseInfoMDL mdl = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(LabelFromPSN_CertificateNO.Text.Trim());
//            if (mdl.PSN_Level == "二级")
//            {
//                #region 资格证检查
//                if (LabelZGZSBH.Text != RadTextBoxTo_ZGZSBH.Text.Trim())//修改了资格证书编号
//                {
//                    bool checkResult = false;
//                    List<QualificationMDL> list = QualificationDAL.GetObjectList(RadTextBoxToPSN_CertificateNO.Text);
//                    foreach (QualificationMDL _QualificationMDL in list)
//                    {
//                        if (_QualificationMDL.ZGZSBH == RadTextBoxTo_ZGZSBH.Text.Trim())
//                        {
//                            checkResult = true;
//                        }
//                    }
//                    if (checkResult == false)
//                    {
//                        UIHelp.layerAlert(Page, "资格证书编号对应的人员信息与填写不符，不允许变更，请查询输入是否与资格证书信息一致！");
//                        return;
//                    }
//                }

//                #endregion 输入验证
//            }

//            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表
//            UIHelp.GetData(EditTable, _ApplyMDL);
//            _ApplyMDL.XGR = UserName;
//            _ApplyMDL.XGSJ = DateTime.Now;
//            _ApplyMDL.Valid = 1;
//            _ApplyMDL.ApplyType = EnumManager.ApplyType.变更注册;
//            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
//            //变更类别
//            _ApplyMDL.ApplyTypeSub = "个人信息变更";
//            ApplyChangeMDL _ApplyChangeMDL = ViewState["ApplyChangeMDL"] == null ? new ApplyChangeMDL() : (ApplyChangeMDL)ViewState["ApplyChangeMDL"];//详细表
//            UIHelp.GetData(EditTable, _ApplyChangeMDL);
//            DBHelper db = new DBHelper();
//            DbTransaction tran = db.BeginTransaction();
//            try
//            {
//                if (ViewState["ApplyMDL"] == null)//new
//                {
//                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
//                    _ApplyMDL.CJR = UserName;
//                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;
//                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
//                    //申请表企业名称
//                    _ApplyMDL.ENT_Name = person.ENT_Name;
//                    _ApplyMDL.ENT_ServerID = person.ENT_ServerID;
//                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;
//                    //申请专业
//                    _ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;
//                    _ApplyChangeMDL.ApplyID = _ApplyMDL.ApplyID;
//                    _ApplyChangeMDL.ValidDate = person.PSN_CertificateValidity;
//                    //变更原因
//                    _ApplyChangeMDL.ChangeReason = "个人信息变更";
//                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "个人信息变更");
//                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
//                    //变更前信息
//                    _ApplyChangeMDL.PSN_NameFrom = LabelPSN_NameFrom.Text;
//                    _ApplyChangeMDL.FromPSN_Sex = LabelFromPSN_Sex.Text;
//                    _ApplyChangeMDL.FromPSN_BirthDate = Convert.ToDateTime(LabelFromPSN_BirthDate.Text);
//                    //_ApplyChangeMDL.FromPSN_CertificateType = LabelFromPSN_CertificateType.Text;//证件类别
//                    _ApplyChangeMDL.FromPSN_CertificateNO = LabelFromPSN_CertificateNO.Text;
//                    _ApplyChangeMDL.ZGZSBH = LabelZGZSBH.Text;//资格证书编号

//                    //所属区县
//                    if (string.IsNullOrEmpty(_ApplyMDL.ENT_City))
//                    {
//                        UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
//                        return;
//                    }

//                    ApplyDAL.Insert(tran, _ApplyMDL);
//                    ApplyChangeDAL.Insert(tran, _ApplyChangeMDL);

//                    //拷贝人员当前有效附件到申请表
//                    List<string> filetype = new List<string>();
//                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
//                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
//                    filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
//                    filetype.Add(EnumManager.FileDataTypeName.执业资格证书扫描件);
//                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

//                    SetButtonEnable(_ApplyMDL.ApplyStatus);

//                    //trFuJanTitel.Visible = true;
//                    //trFuJan.Visible = true;
//                }
//                else//update
//                {
//                    ApplyDAL.Update(tran, _ApplyMDL);
//                    ApplyChangeDAL.Update(tran, _ApplyChangeMDL);
//                }
//                tran.Commit();
//                ViewState["ApplyMDL"] = _ApplyMDL;
//                ViewState["ApplyChangeMDL"] = _ApplyChangeMDL;

//                string NewImgUrl = ApplyFileDAL.GetPersonPhotoByApplyID(_ApplyMDL.ApplyID);
//                if (NewImgUrl != "")
//                {
//                    ImgUpdatePhoto.Src = NewImgUrl;
//                }

//                BindFile(ApplyID);

//                //操着按钮控制
//                SetButtonEnable(_ApplyMDL.ApplyStatus);

//                UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更申请保存成功", string.Format("姓名：{0}，身份证号：{1}，保存时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传");
//                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
//            }
//            catch (Exception ex)
//            {
//                tran.Rollback();
//                UIHelp.WriteErrorLog(Page, "保存变更注册，个人信息变更申请失败", ex);
//            }
//        }

//        //个人提交单位确认  2019-10-28   南静添加
//        protected void ButtonUnit_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
//            if (_ApplyMDL.PSN_Level == "二级")
//            {

//                RadTextBoxTo_ZGZSBH.Enabled = true;
//                RequiredFieldValidator1.Enabled = true;

//            }
//            else
//            {
//                RadTextBoxTo_ZGZSBH.Enabled = false;
//                RequiredFieldValidator1.Enabled = false;
//            }

           
//            if (ButtonUnit.Text != "取消申报")
//            {
//                //必须上传附件集合
//                System.Collections.Hashtable fj = new System.Collections.Hashtable{
//                {EnumManager.FileDataTypeName.一寸免冠照片,0},
//                {EnumManager.FileDataTypeName.证件扫描件,0},
//                {EnumManager.FileDataTypeName.学历证书扫描件,0},
//                {EnumManager.FileDataTypeName.个人信息变更证明,0},
//                {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
//                {EnumManager.FileDataTypeName.劳动合同扫描件,0},
//                {EnumManager.FileDataTypeName.申请表扫描件,0}                
//                };

//                //已上传附件集合
//                DataTable dt = ApplyDAL.GetApplyFile(_ApplyMDL.ApplyID);

//                //计数
//                foreach (DataRow r in dt.Rows)
//                {
//                    if (fj.ContainsKey(r["DataType"].ToString()) == true)
//                    {
//                        fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
//                    }
//                }
//                System.Text.StringBuilder sb = new System.Text.StringBuilder();
//                foreach (string k in fj.Keys)
//                {
//                    if (Convert.ToInt32(fj[k]) == 0)
//                    {
//                        sb.Append(string.Format("、“{0}”", k));
//                    }
//                }

//                if (sb.Length > 0)
//                {
//                    sb.Remove(0, 1);
//                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
//                    return;
//                }

//                //检查一寸照片数量
//                int faceImgCount = ApplyFileDAL.SelectFaceImgCountByApplyID(_ApplyMDL.ApplyID);
//                if(faceImgCount>1)
//                {
//                    UIHelp.layerAlert(Page, string.Format("只能上传一张一寸免冠照片，请删除多余照片！", sb), 5, 0);
//                    return;
//                }
//            }

//            _ApplyMDL.XGR = UserName;
//            _ApplyMDL.XGSJ = DateTime.Now;
//            if (ButtonUnit.Text == "取消申报")
//            {
//                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
//                _ApplyMDL.ApplyTime = null;
//                _ApplyMDL.OldUnitCheckResult = null;
//                _ApplyMDL.OldUnitCheckRemark = null;
//                _ApplyMDL.OldUnitCheckTime = null;
//            }
//            else
//            {
//                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
//                _ApplyMDL.ApplyTime = DateTime.Now;

//                _ApplyMDL.OldUnitCheckResult = null;
//                _ApplyMDL.OldUnitCheckRemark = null;
//                _ApplyMDL.OldUnitCheckTime = null;

//                _ApplyMDL.GetResult = null;
//                _ApplyMDL.GetRemark = null;
//                _ApplyMDL.GetMan = null;
//                _ApplyMDL.GetDateTime = null;//2019-09-26 南静修改  修改原因： 受理时间未清空

//                _ApplyMDL.ExamineDatetime = null;
//                _ApplyMDL.ExamineResult = null;
//                _ApplyMDL.ExamineRemark = null;
//                _ApplyMDL.ExamineMan = null;

//                _ApplyMDL.ReportDate = null;
//                _ApplyMDL.ReportMan = null;
//                _ApplyMDL.ReportCode = null;

//                _ApplyMDL.CheckDate = null;
//                _ApplyMDL.CheckResult = null;
//                _ApplyMDL.CheckRemark = null;
//                _ApplyMDL.CheckMan = null;

//                _ApplyMDL.ConfirmDate = null;
//                _ApplyMDL.ConfirmResult = null;
//                _ApplyMDL.ConfirmMan = null;
//            }
//            ApplyDAL.Update(_ApplyMDL);
//            ViewState["ApplyMDL"] = _ApplyMDL;
//            SetButtonEnable(_ApplyMDL.ApplyStatus);
//            //人员照片
//            BindFile(_ApplyMDL.ApplyID);
//            if (_ApplyMDL.ApplyStatus == "待确认")
//            {
//                UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//                //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
//                //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
//                string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
//                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
//            }
//            else
//            {
//                UIHelp.layerAlert(Page, "撤销成功！", 6, 3000);

//            }
//            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
//        }


//        //申报 or 撤销申报
//        protected void ButtonApply_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
//            try
//            {
//                if (_ApplyMDL.PSN_Level == "二级")
//                {

//                    RadTextBoxTo_ZGZSBH.Enabled = true;
//                    RequiredFieldValidator1.Enabled = true;

//                }
//                else
//                {
//                    RadTextBoxTo_ZGZSBH.Enabled = false;
//                    RequiredFieldValidator1.Enabled = false;
//                }
//                //南静 2019-10-28  注释
//                //if (ButtonApply.Text != "撤销申报")
//                //{
//                //    //必须上传附件集合
//                //    System.Collections.Hashtable fj = new System.Collections.Hashtable{
//                //    {EnumManager.FileDataTypeName.一寸免冠照片,0},
//                //    {EnumManager.FileDataTypeName.证件扫描件,0},
//                //    {EnumManager.FileDataTypeName.学历证书扫描件,0},
//                //    {EnumManager.FileDataTypeName.个人信息变更证明,0},
//                //    {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
//                //    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
//                //    {EnumManager.FileDataTypeName.申请表扫描件,0}
//                //};

//                //    //已上传附件集合
//                //    DataTable dt = ApplyDAL.GetApplyFile(_ApplyMDL.ApplyID);

//                //    //计数
//                //    foreach (DataRow r in dt.Rows)
//                //    {
//                //        if (fj.ContainsKey(r["DataType"].ToString()) == true)
//                //        {
//                //            fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
//                //        }
//                //    }
//                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
//                //    foreach (string k in fj.Keys)
//                //    {
//                //        if (Convert.ToInt32(fj[k]) == 0)
//                //        {
//                //            sb.Append(string.Format("、“{0}”", k));
//                //        }
//                //    }

//                //    if (sb.Length > 0)
//                //    {
//                //        sb.Remove(0, 1);
//                //        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
//                //        return;
//                //    }
//                //}

//                _ApplyMDL.XGR = UserName;
//                _ApplyMDL.XGSJ = DateTime.Now;
//                if (ButtonApply.Text == "取消申报")
//                {
//                    //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
//                    //_ApplyMDL.ApplyTime = null;
//                    //2019-10-28  南静修改
//                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
//                    _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
//                    _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
//                    _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
//                }
//                else
//                {
//                    //2019-10-28  南静修改
//                    _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
//                    _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
//                    _ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

//                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
//                    {
//                        TextBoxOldUnitCheckRemark.Visible = true;
//                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
//                    }
//                    else
//                    {
//                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
//                        _ApplyMDL.ApplyTime = DateTime.Now;

//                        _ApplyMDL.GetResult = null;
//                        _ApplyMDL.GetRemark = null;
//                        _ApplyMDL.GetMan = null;
//                        _ApplyMDL.GetDateTime = null;//2019-09-26 南静修改  修改原因： 受理时间未清空

//                        _ApplyMDL.ExamineDatetime = null;
//                        _ApplyMDL.ExamineResult = null;
//                        _ApplyMDL.ExamineRemark = null;
//                        _ApplyMDL.ExamineMan = null;

//                        _ApplyMDL.ReportDate = null;
//                        _ApplyMDL.ReportMan = null;
//                        _ApplyMDL.ReportCode = null;

//                        _ApplyMDL.CheckDate = null;
//                        _ApplyMDL.CheckResult = null;
//                        _ApplyMDL.CheckRemark = null;
//                        _ApplyMDL.CheckMan = null;

//                        _ApplyMDL.ConfirmDate = null;
//                        _ApplyMDL.ConfirmResult = null;
//                        _ApplyMDL.ConfirmMan = null;
//                    }
//                }
//                ApplyDAL.Update(_ApplyMDL);
//                ViewState["ApplyMDL"] = _ApplyMDL;
//                SetButtonEnable(_ApplyMDL.ApplyStatus);
//                //人员照片
//                BindFile(_ApplyMDL.ApplyID);
//                if (_ApplyMDL.ApplyStatus == "已申报")
//                {
//                    UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//                    //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
//                    //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
//                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
//                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
//                }
//                else
//                {
//                    UIHelp.layerAlert(Page, "个人信息变更申请企业退回个人！", 6, 3000);

//                }
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "个人信息变更申报失败！", ex);
//            }
//            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
//        }

//        //删除申请
//        protected void ButtonDelete_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
//            ApplyDAL.Delete(_ApplyMDL.ApplyID);
//            ViewState.Remove("ApplyMDL");
//            SetButtonEnable(_ApplyMDL.ApplyStatus);
//            UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更申请删除成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//            UIHelp.ParentAlert(Page, "删除成功！", true);
//        }

//        //展示办理进度
//        protected void SetStep(string ApplyStatus)
//        {
//            //step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
//            //step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
//            //step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
//            //step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
//            //step_已审查.Attributes["class"] = step_已审查.Attributes["class"].Replace(" green", "");
//            //step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");

//            //switch (ApplyStatus)
//            //{
//            //    case "未申报":
//            //    case "已驳回":
//            //        step_未申报.Attributes["class"] += " green";
//            //        break;
//            //    case "已申报":
//            //        step_已申报.Attributes["class"] += " green";
//            //        break;
//            //    case "已受理":
//            //        step_已受理.Attributes["class"] += " green";
//            //        break;
//            //    case "区县审查":
//            //    case "已上报":
//            //        step_区县审查.Attributes["class"] += " green";
//            //        break;
//            //    case "已审查":
//            //        step_已审查.Attributes["class"] += " green";
//            //        break;
//            //    case "已决定":
//            //        step_已决定.Attributes["class"] += " green";
//            //        break;
//            //    default:
//            //        step_未申报.Attributes["class"] += " green";
//            //        break;
//            //}

//            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
//            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace("green", "");//南静添加  2019-10-28
//            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
//            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
//            step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
//            step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");
//            step_已审查.Attributes["class"] = step_已审查.Attributes["class"].Replace(" green", "");
//            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
//            step_已公示.Attributes["class"] = step_已公示.Attributes["class"].Replace(" green", "");
//            step_已公告.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");

//            switch (ApplyStatus)
//            {
//                case "未申报":
//                case "已驳回":
//                    step_未申报.Attributes["class"] += " green";
//                    break;
//                //2019-10-22     南静添加
//                case "待确认":
//                    step_待确认.Attributes["class"] += " green";
//                    break;
//                case "已申报":
//                    step_已申报.Attributes["class"] += " green";
//                    break;
//                case "已受理":
//                    step_已受理.Attributes["class"] += " green";
//                    break;
//                case "区县审查":
//                    step_区县审查.Attributes["class"] += " green";
//                    break;
//                case "已上报":
//                    step_已上报.Attributes["class"] += " green";
//                    break;
//                case "已审查":
//                    step_已审查.Attributes["class"] += " green";
//                    break;
//                case "已决定":
//                    step_已决定.Attributes["class"] += " green";
//                    break;
//                case "已公示":
//                    step_已公示.Attributes["class"] += " green";
//                    break;
//                case "已公告":
//                    step_已公告.Attributes["class"] += " green";
//                    break;
//                default:
//                    step_未申报.Attributes["class"] += " green";
//                    break;
//            }
//        }

//        //操作按钮控制
//        protected void SetButtonEnable(string ApplyStatus)
//        {
//            SetStep(ApplyStatus);
//            switch (ApplyStatus)
//            {
//                case "":
//                    ButtonSave.Enabled = true;
//                    ButtonUnit.Enabled = false;
//                    ButtonUnit.Text = "提交单位确认";
//                    //2019-10-28  南静注释
//                    //ButtonApply.Enabled = false;
//                    //ButtonApply.Text = "申 报"; 
//                    ButtonDelete.Enabled = false;
//                    ButtonOutput.Enabled = false;
//                    break;
//                case EnumManager.ApplyStatus.未申报:
//                    ButtonSave.Enabled = true;
//                    //2019-10-28   南静注释
//                    //ButtonApply.Enabled = true;
//                    //ButtonApply.Text = "提交单位确认";
//                    ButtonUnit.Enabled = true;
//                    ButtonUnit.Text = "提交单位确认";
//                    ButtonDelete.Enabled = true;
//                    ButtonOutput.Enabled = true;
//                    //个人登录后
//                    if (IfExistRoleID("0") == true)
//                    {
//                        trFuJanTitel.Visible = true;
//                        trFuJan.Visible = true;
//                    }
//                    break;
//                //2019-10-28   南静添加   待确认
//                case EnumManager.ApplyStatus.待确认:
//                    ButtonSave.Enabled = false;
//                    ButtonUnit.Enabled = true;
//                    ButtonUnit.Text = "取消申报";
//                    ButtonDelete.Enabled = false;
//                    ButtonOutput.Enabled = false;

//                    //企业
//                    ButtonApply.Enabled = true;
//                    ButtonApply.Text = "确定";
//                    break;
//                case EnumManager.ApplyStatus.已申报:
//                    //ButtonSave.Enabled = false;
//                    //ButtonApply.Enabled = true;
//                    //ButtonApply.Text = "撤销申报";
//                    //ButtonDelete.Enabled = false;
//                    //ButtonOutput.Enabled = false;
//                    //2019-10-28   南静修改
//                    //企业
//                    ButtonApply.Enabled = true;
//                    ButtonApply.Text = "取消申报";

//                    //个人
//                    ButtonSave.Enabled = false;
//                    ButtonDelete.Enabled = false;
//                    ButtonOutput.Enabled = false;
//                    ButtonUnit.Enabled = true;
//                    ButtonUnit.Text = "取消申报";
//                    break;
//                case EnumManager.ApplyStatus.已驳回:
//                    //ButtonSave.Enabled = true;
//                    //ButtonApply.Enabled = true;
//                    //ButtonApply.Text = "申 报";
//                    //ButtonDelete.Enabled = true;
//                    //ButtonOutput.Enabled = true;
//                    //2019-10-28  南静修改
//                    ButtonSave.Enabled = true;
//                    ButtonUnit.Enabled = true;
//                    ButtonUnit.Text = "提交单位确认";
//                    ButtonDelete.Enabled = true;
//                    ButtonOutput.Enabled = true;
//                    //企业
//                    ButtonApply.Enabled = false;
//                    ButtonApply.Text = "确定";
//                    //个人登录后
//                    if (IfExistRoleID("0") == true)
//                    {
//                        trFuJanTitel.Visible = true;
//                        trFuJan.Visible = true;
//                    }
//                    break;
//                case "禁用":
//                    ButtonSave.Enabled = false;
//                    ButtonUnit.Enabled = false;
//                    ButtonUnit.Text = "提交单位确认";
//                    //2019-10-28   南静注释
//                    //ButtonApply.Enabled = false;
//                    //ButtonApply.Text = "申 报"; 
//                    ButtonDelete.Enabled = false;
//                    ButtonOutput.Enabled = false;
//                    break;
//                default:
//                    ButtonSave.Enabled = false;
//                    ButtonApply.Enabled = false;
//                    ButtonApply.Text = "撤销申报";
//                    ButtonDelete.Enabled = false;
//                    ButtonOutput.Enabled = true;
//                    break;
//            }

//            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";//2019-10-28   南静添加
//            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
//            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
//            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
//            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
//        }

//        //导出打印
//        protected void ButtonOutput_Click(object sender, EventArgs e)
//        {
//            try
//            {

//                string sourceFile = "";
//                string fileName = "";
//                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
//                if (_ApplyMDL.PSN_Level == "二级")
//                {
//                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/个人信息变更.docx");
//                    fileName = "北京市二级注册建造师个人信息变更注册申请表";
//                }
//                if (_ApplyMDL.PSN_Level == "二级临时")
//                {
//                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师临时执业证书个人信息变更注册申请表.docx");
//                    fileName = "北京市二级建造师临时执业证书个人信息变更注册申请表";
//                }
//                //string sourceFile = HttpContext.Current.Server.MapPath("~/Template/个人信息变更.docx");
//                //string fileName = "北京市二级注册建造师个人信息变更注册申请表";
//                //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

//                ApplyChangeMDL _ApplyChangeMDL = ViewState["ApplyChangeMDL"] as ApplyChangeMDL;
//                UnitMDL _UnitMDL = UnitDAL.GetObject(_ApplyMDL.ENT_ServerID);
//                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
//                var o = new List<object>();
//                o.Add(_ApplyMDL);
//                o.Add(_ApplyChangeMDL);
//                o.Add(_UnitMDL);
//                var ht = PrintDocument.GetProperties(o);

//                //新照片
//                if (_FileInfoMDL != null)
//                {
//                    ht["photo_new"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
//                }
//                else 
//                {
//                    ht["photo_new"] = "";
//                }
//                if (ht["photo_new"] == "")
//                {
//                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
//                    return;
//                }

//                //原照片
//                string oldPhontoUrl = FileInfoDAL.GetPersonPhotoByPSN_RegisterNO(_ApplyMDL.PSN_RegisterNo);
//                if (oldPhontoUrl != "")
//                {
//                    ht["photo"] = oldPhontoUrl;
//                }
//                else
//                {
//                    ht["photo"] = "~/Images/tup.gif";
//                }

//                ht["isCtable"] = false;
//                //对时间类型进行格式转换
//                ht["ValidDate"] = _ApplyChangeMDL.ValidDate == null ? "" : ((DateTime)_ApplyChangeMDL.ValidDate).ToString("yyyy-MM-dd");
//                ht["FromPSN_BirthDate"] = _ApplyChangeMDL.FromPSN_BirthDate == null ? "" : ((DateTime)_ApplyChangeMDL.FromPSN_BirthDate).ToString("yyyy-MM-dd");
//                ht["ToPSN_BirthDate"] = _ApplyChangeMDL.ToPSN_BirthDate == null ? "" : ((DateTime)_ApplyChangeMDL.ToPSN_BirthDate).ToString("yyyy-MM-dd");
//                ht["FR"] = _UnitMDL.ENT_Corporate;
//                ht["END_Addess"] = _UnitMDL.END_Addess;
//                ht["ENT_Type"] = _UnitMDL.ENT_Type;
//                ht["LinkMan"] = _UnitMDL.ENT_Contact;
//                ht["ENT_Telephone"] = _UnitMDL.ENT_Telephone;
//                ht["ENT_Sort"] = _UnitMDL.ENT_Sort;
//                ht["ENT_Grade"] = _UnitMDL.ENT_Grade;
//                ht["ENT_QualificationCertificateNo"] = _UnitMDL.ENT_QualificationCertificateNo;
//                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "打印个人信息变更申请Word失败！", ex);
//            }

//        }

//        //区县受理
//        protected void BtnSave_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

//            #region 查询证书是否锁定
//            if (RadioButtonListApplyStatus.SelectedValue == "通过")
//            {
//                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(o.PSN_ServerID);
//                if (IfLock == true)
//                {
//                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
//                    return;
//                }
//            }
//            #endregion

//            _ApplyMDL.GetDateTime = DateTime.Now;
//            _ApplyMDL.GetMan = UserName;
//            _ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
//            _ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
//            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;

//            try
//            {
//                ApplyDAL.Update(_ApplyMDL);
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
//                return;
//            }
//            UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更区县受理", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date
//                , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
//            // UIHelp.ParentAlert(Page, "受理成功！", true);


//            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
//            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
//            //Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
//        }
//        //区县审查
//        protected void BttSave_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
//            //移动到汇总上报时校验
//            //if (_ApplyMDL.CheckXSL.HasValue == false || _ApplyMDL.CheckXSL.Value == 1)
//            //{
//            //    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
//            //    return;
//            //}
//            #region 查询证书是否锁定
//            if (RadioButtonListApplyStatus.SelectedValue == "通过")
//            {
//                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(o.PSN_ServerID);
//                if (IfLock == true)
//                {
//                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
//                    return;
//                }
//            }
//            #endregion

//            _ApplyMDL.ExamineDatetime = DateTime.Now;
//            _ApplyMDL.ExamineMan = UserName;
//            _ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
//            _ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
//            _ApplyMDL.ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;

//            try
//            {
//                ApplyDAL.Update(_ApplyMDL);
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
//                return;
//            }
//            UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更区县审查", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date
//                  , _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
//            UIHelp.ParentAlert(Page, "区县审查成功！", true);
//        }

//        //建委审核
//        protected void ButtonCheck_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

//            #region 查询证书是否锁定
//            int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
//            if (count > 0)
//            {
//                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
//                return;
//            }
//            #endregion
//            _ApplyMDL.CheckDate = DateTime.Now;
//            _ApplyMDL.CheckMan = UserName;
//            _ApplyMDL.CheckResult = RadioButtonListCheckResult.SelectedValue;
//            _ApplyMDL.CheckRemark = TextBoxApplyCheckRemark.Text.Trim();
//            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已审查;

//            try
//            {
//                ApplyDAL.Update(_ApplyMDL);


//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "审核失败！", ex);
//                return;
//            }
//            UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更建委审核", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date
//                , _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
//            UIHelp.ParentAlert(Page, "审核成功！", true);
//        }

//        ////建委领导决定
//        protected void ButtonDecide_Click(object sender, EventArgs e)
//        {
//            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

//            #region 查询证书是否锁定
//            int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
//            if (count > 0)
//            {
//                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
//                return;
//            }
//            #endregion
//            _ApplyMDL.ConfirmDate = DateTime.Now;
//            _ApplyMDL.ConfirmMan = UserName;
//            _ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
//            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已公告;
//            //此业务结束，给他个公告时间，系统就能分配可以重新申请业务
//            _ApplyMDL.NoticeDate = DateTime.Now;
//            //开启事务
//            DBHelper db = new DBHelper();
//            DbTransaction tran = db.BeginTransaction();
//            try
//            {
//                ApplyDAL.Update(tran, _ApplyMDL);

//                if (RadioButtonListDecide.SelectedValue == "通过")
//                {
//                    DataTable dt = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  ApplyID='" + _ApplyMDL.ApplyID + "' ) A INNER JOIN  ApplyChange B  ON A.APPLYID=B.APPLYID");

//                    COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(tran, dt.Rows[0]["PSN_ServerID"].ToString());
//                    //先往历史纪录表导入数据
//                    if (_COC_TOW_Person_BaseInfoMDL != null)
//                    {
//                        //正式表往记录表写数，右边是一个方法，根据建造师ID拿到一个记录表的信息
//                        COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran, _COC_TOW_Person_BaseInfoMDL);
//                        COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
//                    }

//                    if (dt.Rows[0]["PSN_NameTo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = dt.Rows[0]["PSN_NameTo"].ToString();
//                    if (dt.Rows[0]["ToPSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = dt.Rows[0]["ToPSN_Sex"].ToString();
//                    if (dt.Rows[0]["ToPSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(dt.Rows[0]["ToPSN_BirthDate"]);
//                    if (dt.Rows[0]["ToPSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = dt.Rows[0]["ToPSN_CertificateNO"].ToString();
//                    if (dt.Rows[0]["PSN_NameFrom"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = dt.Rows[0]["PSN_NameFrom"].ToString();
//                    if (dt.Rows[0]["To_ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = dt.Rows[0]["To_ZGZSBH"].ToString();
//                    _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = EnumManager.ApplyType.个人信息变更;
//                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = "02";
//                    _COC_TOW_Person_BaseInfoMDL.XGR = UserName;
//                    _COC_TOW_Person_BaseInfoMDL.XGSJ = DateTime.Now;
//                    _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = null;
//                    _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = null;
//                    //注册审批日期
//                    _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = DateTime.Now;
//                    //Update正式表信息
//                    COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);

//                    //更新证书附件中需要被覆盖的附件为历史附件
//                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
//                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
//                                    from [dbo].[COC_TOW_Person_File]
//                                    inner join 
//                                    (
//	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
//	                                    from 
//	                                    (
//		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[ApplyFile]
//		                                    on [FileInfo].FileID = [ApplyFile].FileID
//		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
//		                                    where [Apply].ApplyID='{0}' 
//	                                    ) a
//	                                    inner join 
//	                                    (
//		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[COC_TOW_Person_File]
//		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
//                                            inner join [dbo].[Apply] 
//                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
//		                                    where  [Apply].ApplyID='{0}' 
//	                                    ) b 
//	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
//                                    ) t
//                                    on [COC_TOW_Person_File].FileID = t.FileID", _ApplyMDL.ApplyID));


//                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    delete from [dbo].[COC_TOW_Person_File]
//                                    where FileID in( select [COC_TOW_Person_File].[FileID]
//                                    from [dbo].[COC_TOW_Person_File]
//                                    inner join 
//                                    (
//	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
//	                                    from 
//	                                    (
//		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[ApplyFile]
//		                                    on [FileInfo].FileID = [ApplyFile].FileID
//		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
//		                                    where [Apply].ApplyID='{0}' 
//	                                    ) a
//	                                    inner join 
//	                                    (
//		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[COC_TOW_Person_File]
//		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
//                                            inner join [dbo].[Apply] 
//                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
//		                                    where  [Apply].ApplyID='{0}' 
//	                                    ) b 
//	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
//                                    ) t
//                                    on [COC_TOW_Person_File].FileID = t.FileID
//                                    )", _ApplyMDL.ApplyID));

//                    //将申请单附件写入证书附件库
//                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
//                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
//                                    from [dbo].[ApplyFile]
//                                    inner join [dbo].[Apply] 
//                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
//                                    where [Apply].ApplyID='{0}' ", _ApplyMDL.ApplyID));


//                    //给消息表发送企业消息通知
//                    CommonDAL.ExecSQL(tran, string.Format(@"INSERT INTO [dbo].[ApplyNews]([ID],[ApplyID],[PSN_Name],[PSN_CertificateNO] ,[PSN_RegisterNo] ,[ApplyType],[SFCK],[ENT_OrganizationsCode],[ENT_City])
//                    SELECT NEWID(),[ApplyID],[PSN_Name],[PSN_CertificateNO],[PSN_RegisterNo],[ApplyType],0,[ENT_OrganizationsCode],[ENT_City]
//                    FROM APPLY WHERE ApplyID='{0}'", _ApplyMDL.ApplyID));
//                }
//                tran.Commit();
//                UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更建委决定成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//                UIHelp.ParentAlert(Page, "决定成功！", true);

//            }
//            catch (Exception ex)
//            {
//                tran.Rollback();
//                UIHelp.WriteErrorLog(Page, "决定失败！", ex);
//                return;
//            }

//        }


//        /// <summary>
//        /// 绑定附件
//        /// </summary>
//        /// <param name="ApplyID"></param>
//        private void BindFile(string ApplyID)
//        {
//            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(ApplyID);
//            DataTable HB_File = dt_ApplyFile.Clone();
//            HB_File.Columns["FileUrl"].MaxLength = 8000;

//            string DataType = "";
//            foreach (DataRow r in dt_ApplyFile.Rows)
//            {
//                if (r["DataType"].ToString() != DataType)
//                {

//                    HB_File.ImportRow(r);
//                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0}|{1}", r["FileUrl"], r["FileID"]);
//                    DataType = r["DataType"].ToString();
//                }
//                else
//                {
//                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0},{1}|{2}", HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"], r["FileUrl"], r["FileID"]);
//                }
//            }

//            RadGridFile.DataSource = HB_File;
//            RadGridFile.DataBind();

//            string NewImgUrl = ApplyFileDAL.GetPersonPhotoByApplyID(ApplyID);
//            if (NewImgUrl != "")
//            {
//                ImgUpdatePhoto.Src = NewImgUrl;
//            }
//            else
//            {
//                ImgUpdatePhoto.Src = "~/Images/photo_ry.jpg";
//            }
//        }

//        //格式化附件
//        protected void RadGridFile_ItemDataBound(object sender, GridItemEventArgs e)
//        {
//            if (e.Item is GridDataItem)
//            {
//                GridDataItem item = (GridDataItem)e.Item;
//                RadGrid rg = item.FindControl("RadGrid1") as RadGrid;

//                DataTable dt_ApplyFile = (ViewState["dt_ApplyFile"] as DataTable).Clone();

//                string ApplyID = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();

//                string[] imgurl = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FileUrl"].ToString().Split(',');
//                string[] atrt = null;
//                System.Text.StringBuilder sb = new System.Text.StringBuilder();
//                foreach (string s in imgurl)
//                {
//                    DataRow dr = dt_ApplyFile.NewRow();

//                    atrt = s.Split('|');
//                    dr["FileUrl"] = atrt[0];
//                    dr["FileID"] = atrt[1];
//                    dr["ApplyID"] = ApplyID;
//                    dt_ApplyFile.Rows.Add(dr);
//                }

//                rg.DataSource = dt_ApplyFile;
//                rg.DataBind();
//            }

//        }

//        //删除附件
//        protected void RadGridFile_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
//        {
//            //获取类型Id

//            string FileID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileID"].ToString();
//            string ApplyID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();
//            try
//            {
//                ApplyFileDAL.Delete(FileID, ApplyID);
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "删除申请表附件失败！", ex);
//                return;
//            }
//            BindFile(ApplyID);
//            UIHelp.WriteOperateLog(UserName, UserID, "个人信息变更申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, DateTime.Now.Date));
//            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
//        }

//        //变更修改信息颜色
//        public void setColor()
//        {
//            if (LabelPSN_NameFrom.Text != RadTextBoxPSN_NameTo.Text)
//            {
//                RadTextBoxPSN_NameTo.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                RadTextBoxPSN_NameTo.ForeColor = System.Drawing.Color.Black;
//            }

//            if (LabelFromPSN_Sex.Text != RadComboBoxToPSN_Sex.Text)
//            {
//                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
//            }

//            if (LabelFromPSN_BirthDate.Text != RadDatePickerToPSN_BirthDate.SelectedDate.Value.ToString())
//            {
//                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
//            }
//            if (LabelFromPSN_CertificateNO.Text != RadTextBoxToPSN_CertificateNO.Text)
//            {
//                RadTextBoxToPSN_CertificateNO.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
//            }

//        }

        ////上传WORD扫描件
        //protected void FileWord(ref bool ischek)
        //{
        //    //检查是否上传文件
        //    if (FileUploadWord.HasFile == false)
        //    {
        //        ischek = false;
        //        UIHelp.Alert(Page, "请上传申请表扫描件！");
        //        return;
        //    }
        //    //检查保存文件
        //    if ((FileUploadWord.HasFile == true && Path.GetExtension(FileUploadWord.FileName).ToLower() != ".jpg"))
        //    {
        //        ischek = false;
        //        UIHelp.Alert(Page, "上传扫描件只支持jpg格式图片，请检查你要上传的文件是否格式正确！");
        //        return ;
        //    }
        //    Int64 fileSizeLimit = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]) * 1024;
        //    if ((FileUploadWord.HasFile == true && FileUploadWord.FileContent.Length > fileSizeLimit))
        //    {
        //        ischek = false;
        //        UIHelp.Alert(Page, string.Format("上传扫描件大小不能超过{0}kb，请检查您要上传的文件大小！", System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]));
        //        return ;
        //    }
        //    else
        //    {
        //        //上传附件
        //        string fileID = UIHelp.UploadFile(Page, FileUploadWord, EnumManager.FileDataType.申请表扫描件, EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.申请表扫描件), UserName);
        //        ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
        //        ApplyFileMDL _ApplyFileMDL = new ApplyFileMDL();
        //        _ApplyFileMDL.ApplyID = _ApplyMDL.ApplyID;
        //        _ApplyFileMDL.FileID = fileID;
        //        _ApplyFileMDL.CheckResult = 0;
        //        ApplyFileDAL.Insert(_ApplyFileMDL);
        //        ischek = true;
        //    }
        //}
    }
}