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

namespace ZYRYJG.CertifManage
{
    public partial class Application : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifChange.aspx|CertifChangeCheckUnit.aspx|CertifChangeCheckConfirm.aspx|ApplyQuerySLR.aspx";
            }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["certhfchange"] == null ? "" : string.Format("BG-{0}", (ViewState["certhfchange"] as CertificateChangeOB).CertificateChangeID); }
        }

        /// <summary>
        /// 验证是否为企业法人
        /// </summary>
        protected bool IfFaRen
        {
            get
            {
                if (ViewState["IfFaRen"] == null)
                {
                    ViewState["IfFaRen"] = CertificateDAL.IFExistFRByUnitCode(RadTextBoxNewUnitCode.Text.Trim(), RadTextBoxWorkerName.Text.Trim());

                }
                return Convert.ToBoolean(ViewState["IfFaRen"]);
            }
        }

        protected CertificateOB myCertificateOB
        {
            get { return ViewState["certificateob"] == null ? null : (CertificateOB)ViewState["certificateob"]; }
        }

        /// <summary>
        /// 是否到达1年内变更单位次数上线（AC=4次，B=2次）
        /// </summary>
        /// <returns>达到上限是返回上线次数n，否则返回0</returns>
        protected int IfChangeUnitLimit
        {
            get
            {
                if (RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim())//变更单位
                {
                    if (myCertificateOB.PostTypeID == 1)
                    {
                        int Times = CertificateChangeDAL.SelectChangeUnitCountYear(myCertificateOB.CertificateID.Value);
                        if (myCertificateOB.PostID == 148)//B
                        {                            
                            if (Times == 1)//1年内已成功变更单位1次，当前是第2次
                            {
                                return 2;
                            }
                            else
                                return 0;
                        }
                        else//A、C1、C2、C3
                        {
                            if (Times == 3)//1年内已成功变更单位3次，当前是第4次
                            {
                                return 4;
                            }
                            else
                                return 0;
                        }
                    }
                    else
                        return 0;

                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 比对社保局社保是否合格
        /// </summary>
        protected bool SheBaoCheckResult
        {
            get
            {
                if (ViewState["SheBaoCheckResult"] == null)
                {
                    if (ViewState["certhfchange"] == null)
                    {
                        ViewState["SheBaoCheckResult"] = false;
                        return false;
                    }
                    CertificateChangeOB o = ViewState["certhfchange"] as CertificateChangeOB;
                    if (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                    {
                        ViewState["SheBaoCheckResult"] = false;
                    }
                    else
                    {
                        ViewState["SheBaoCheckResult"] = true;
                    }
                }
                return Convert.ToBoolean(ViewState["SheBaoCheckResult"]);
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            
            base.Page_Init(sender, e);
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadioButtonListENT_ContractType, LabelJZSJ, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadioButtonListENT_ContractType, RadDatePickerENT_ContractENDTime, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadioButtonListENT_ContractType, ValidatorENT_ContractENDTime, RadAjaxLoadingPanel1);

            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, divZACheckResult, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_PostTyppe1_aqy, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_PostTyppe1_qyfzr, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_PostTyppe1_xmfzr, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_PostTyppe2, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_PostTyppe4, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, divFR, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, div_YingYeZhiZhao, RadAjaxLoadingPanel1);
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, div_SheBao, RadAjaxLoadingPanel1);
            //RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_FaRen, RadAjaxLoadingPanel1);
            //RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadComboBoxJob, p_NoFaRen, RadAjaxLoadingPanel1);
          
            if (!IsPostBack)
            {
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                UIHelp.FillDropDownList(RadTextBoxChangeType, "101");
                RadTextBoxChangeType.Items.FindItemByValue("进京变更").Remove();
                RadTextBoxChangeType.Items.FindItemByValue("补办").Remove();
                RadTextBoxChangeType.Items.Insert(0, new RadComboBoxItem("请选择", ""));

                UIHelp.FillDropDownList(RadComboBoxSKILLLEVEL, "111", "请选择", "", " [SORTID] between 6 and 10");//技术职称 

                UIHelp.FillDropDownList(RadComboBoxJob, "112", "请选择", "");//职务 
                RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("其他"));
                              
                //查看
                if (string.IsNullOrEmpty(Request["o"]) == false)//Request["o"]=CertificateChangeID
                {
                    #region 编辑或查看

                    CertificateChangeOB _CertificateChangeOB = CertificateChangeDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                    if (_CertificateChangeOB != null)
                    {                       
                        ViewState["certhfchange"] = _CertificateChangeOB;

                        CertificateOB certificateob = CertificateDAL.GetObject(_CertificateChangeOB.CertificateID.Value);
                        ViewState["certificateob"] = certificateob;

                        if (certificateob.PostTypeID != 1)
                        {
                            tipPosttype1.Visible = false;
                        }

                        if (certificateob.PostTypeID == 1 && _CertificateChangeOB.ChangeType == EnumManager.CertificateUpdateType.ChangeInBeiJing)//三类人员京内变更显示职务和技术职称
                        {
                            trJob.Visible = true;
                            BindCurCertStatus(certificateob.WorkerCertificateCode);

                            if (RadComboBoxSKILLLEVEL.FindItemByText(_CertificateChangeOB.SkillLevel) != null) //技术职称
                            {
                                RadComboBoxSKILLLEVEL.FindItemByText(_CertificateChangeOB.SkillLevel).Selected = true;
                            }

                            if (certificateob.PostID == 148)//项目负责人
                            {
                                RadComboBoxJob.FindItemByText("项目负责人（项目经理）").Selected = true;
                                RadComboBoxJob.Enabled = false;
                            }
                            else if (certificateob.PostID == 6 || certificateob.PostID == 6 || certificateob.PostID == 6)//安全员
                            {
                                RadComboBoxJob.FindItemByText("专职安全生产管理人员").Selected = true;
                                RadComboBoxJob.Enabled = false;
                            }
                            else if (certificateob.PostID == 147)
                            {
                                RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("项目负责人（项目经理）"));
                                RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("专职安全生产管理人员"));

                            }

                            if (RadComboBoxJob.FindItemByText(_CertificateChangeOB.Job) != null) //职务
                            {
                                RadComboBoxJob.FindItemByText(_CertificateChangeOB.Job).Selected = true;
                            }

                          
                            if (_CertificateChangeOB.ENT_ContractType.HasValue )//合同
                            {
                                tableHeTong.Style.Add("display", "inline");
                                RadioButtonListENT_ContractType.Items.FindByValue(_CertificateChangeOB.ENT_ContractType.ToString()).Selected = true;
                                RadDatePickerENT_ContractStartTime.SelectedDate = _CertificateChangeOB.ENT_ContractStartTime;
                                RadDatePickerENT_ContractENDTime.SelectedDate = _CertificateChangeOB.ENT_ContractENDTime;
                                if (RadioButtonListENT_ContractType.SelectedValue == "1")
                                {
                                    RadDatePickerENT_ContractENDTime.Style.Remove("display");
                                    LabelJZSJ.Style.Remove("display");
                                    ValidatorENT_ContractENDTime.Enabled = true;
                                }
                                else
                                {
                                    RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
                                    LabelJZSJ.Style.Add("display", "none");
                                    ValidatorENT_ContractENDTime.Enabled = false;
                                }
                            }
                            else if(PersonType==2)
                            {
                                tableHeTong.Style.Add("display", "inline");
                                ValidatorENT_ContractENDTime.Enabled = true;
                                ValidatorENT_ContractStartTime.Enabled = true;
                            }
                            else
                            {
                                ValidatorENT_ContractENDTime.Enabled = false;
                                ValidatorENT_ContractStartTime.Enabled = false;
                            }
                        }
                        else
                        {
                            trJob.Visible = false;
                            tableHeTong.Style.Add("display", "none");
                            ValidatorENT_ContractENDTime.Enabled = false;
                            ValidatorENT_ContractStartTime.Enabled = false;
                        }

                        if (string.IsNullOrEmpty(_CertificateChangeOB.WorkerCertificateCode) == false && _CertificateChangeOB.WorkerCertificateCode.Length > 2)
                        {
                            string path = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", _CertificateChangeOB.WorkerCertificateCode.Substring(_CertificateChangeOB.WorkerCertificateCode.Length - 3, 3), _CertificateChangeOB.WorkerCertificateCode);
                            if (File.Exists(HttpContext.Current.Server.MapPath(path)) == true)
                            {
                                ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, _CertificateChangeOB.WorkerCertificateCode);
                            }
                            else
                            {
                                ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, _CertificateChangeOB.NewWorkerCertificateCode);
                            }
                        }
                        else
                        {
                            ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, _CertificateChangeOB.NewWorkerCertificateCode);
                        }
                        if (string.IsNullOrEmpty(certificateob.FacePhoto) == false)
                        {
                            CheckBoxModifyPhoto.Visible = true;
                            TableUploadPhoto.Style.Add("display", "none");
                        }
                        else
                        {
                            CheckBoxModifyPhoto.Visible = false;
                            TableUploadPhoto.Style.Add("display", "none");
                        }
                        CheckBoxModifyPhoto.Enabled = false;

                        if (_CertificateChangeOB.IfUpdatePhoto.HasValue == true && _CertificateChangeOB.IfUpdatePhoto.Value == 1)//上传了照片
                        {
                            CheckBoxModifyPhoto.Checked = true;
                            TableUploadPhoto.Style.Add("display", "none");
                            //ImgUpdatePhoto.Src = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), _CertificateChangeOB.CertificateChangeID);//绑定照片
                            ImgUpdatePhoto.Src =UIHelp.ShowFile( string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), _CertificateChangeOB.CertificateChangeID));//绑定照片
                        }

                        LabelPostType.Text = UIHelp.GetPostTypeNameByID(certificateob.PostTypeID.ToString());//岗位类别ID
                        if (certificateob.PostTypeID == 3 && string.IsNullOrEmpty(certificateob.AddItemName) == false)//造价员
                        {
                            RadTextBoxPostID.Text = certificateob.AddItemName;
                        }
                        else
                        {
                            RadTextBoxPostID.Text = PostInfoDAL.GetObject(certificateob.PostID.Value).PostName;
                        }
                        RadTextBoxCertificateCode.Text = certificateob.CertificateCode;     //证书编号
                        lblWorkerName.Text = _CertificateChangeOB.WorkerName;
                        RadTextBoxWorkerName.Text = _CertificateChangeOB.NewWorkerName;          //姓名
                        lblSex.Text = _CertificateChangeOB.Sex;   //性别
                        for (int i = 0; i < 2; i++)
                        {
                            if (rdbtnlistSex.Items[i].Text == _CertificateChangeOB.NewSex.ToString())
                            {
                                rdbtnlistSex.Items[i].Selected = true;
                                break;
                            }
                        }
                        lblBirthday.Text = Convert.ToDateTime(_CertificateChangeOB.Birthday.ToString()).ToString("yyyy-MM-dd"); //原出生日期
                        RadDatePickerNewBirthday.SelectedDate = _CertificateChangeOB.NewBirthday;
                        lblWorkerCertificateCode.Text = _CertificateChangeOB.WorkerCertificateCode;
                        LabelWorkerCertificateCode.Text = _CertificateChangeOB.NewWorkerCertificateCode;     //证件号码
                        RadTextBoxUnitName.Text = _CertificateChangeOB.UnitName;//原工作单位
                        RadTextBoxNewUnitName.Text = _CertificateChangeOB.NewUnitName;
                        RadTextBoxUnitCode.Text = _CertificateChangeOB.UnitCode;        //原机构代码
                        RadTextBoxNewUnitCode.Text = _CertificateChangeOB.NewUnitCode;
                        lblApplyDate.Text = Convert.ToDateTime(_CertificateChangeOB.ApplyDate.ToString()).ToString("yyyy-MM-dd");  //申请日期
                        RadTextBoxLinkWay.Text = _CertificateChangeOB.LinkWay.ToString();
                        UIHelp.SetReadOnly(RadTextBoxLinkWay, true);//不允许修改来自大厅实名制认证电话
                        RadTextBoxChangeType.SelectedValue = _CertificateChangeOB.ChangeType;  //变更类型
                        if (_CertificateChangeOB.ChangeType == EnumManager.CertificateUpdateType.Logout
                            || _CertificateChangeOB.ChangeType == EnumManager.CertificateUpdateType.OutBeiJing)
                        {
                            DisableControl();
                        }
                        //RadTextBoxOldUnitAdvise.Text = _CertificateChangeOB.OldUnitAdvise;//原单位意见
                        //RadTextBoxNewUnitAdvise.Text = _CertificateChangeOB.NewUnitAdvise;//现单位意见
                        //RadTextBoxOldConferUnitAdvise.Text = _CertificateChangeOB.OldConferUnitAdvise;//原发证机关意见
                        //RadTextBoxNewConferUnitAdvise.Text = _CertificateChangeOB.NewConferUnitAdvise;  //现发证机关意见
                        LabelApplyCode.Text = string.Format("申请批号：{0}", _CertificateChangeOB.ApplyCode);

                        if (RadTextBoxUnitName.Text != RadTextBoxNewUnitName.Text) RadTextBoxNewUnitName.BorderColor = System.Drawing.Color.Red;
                        if (RadTextBoxUnitCode.Text != RadTextBoxNewUnitCode.Text) RadTextBoxNewUnitCode.BorderColor = System.Drawing.Color.Red;
                        if (lblWorkerName.Text != RadTextBoxWorkerName.Text) RadTextBoxWorkerName.BorderColor = System.Drawing.Color.Red;
                        if (lblWorkerCertificateCode.Text != LabelWorkerCertificateCode.Text) LabelWorkerCertificateCode.BorderColor = System.Drawing.Color.Red;
                        if (lblBirthday.Text != (_CertificateChangeOB.NewBirthday.HasValue == true ? _CertificateChangeOB.NewBirthday.Value.ToString("yyyy-MM-dd") : ""))
                        {
                            RadDatePickerNewBirthday.BorderColor = System.Drawing.Color.Red;
                            RadDatePickerNewBirthday.BorderWidth = 1;
                        }
                        if (lblSex.Text != _CertificateChangeOB.NewSex)
                        {
                            rdbtnlistSex.BorderColor = System.Drawing.Color.Red;
                            rdbtnlistSex.BorderWidth = 1;
                        }

                        if (_CertificateChangeOB.ChangeType == "离京变更" || _CertificateChangeOB.ChangeType == "注销")
                        {
                            LabelTS_WorkerName.Text = _CertificateChangeOB.WorkerName;//姓名
                            LabelTS_Sex.Text = _CertificateChangeOB.Sex;   //性别
                            LabelTS_Birthday.Text = Convert.ToDateTime(_CertificateChangeOB.Birthday.ToString()).ToString("yyyy-MM-dd"); //原出生日期                          
                            LabelTS_WorkerCertificateCode.Text = _CertificateChangeOB.WorkerCertificateCode;
                            RadTextBoxTS_OldUnitName.Text = _CertificateChangeOB.UnitName;//原工作单位   
                            RadTextBoxTS_OldUnitCode.Text = _CertificateChangeOB.UnitCode;        //原机构代码
                            tableOldCert2.Style.Add("display", "table");
                            tableOldCert1.Style.Add("display", "none");
                            step_已办结.InnerText = "上报建设部（办结）";

                            if (certificateob.PostTypeID == 1)
                            {
                                tr_enforce.Style.Remove("display");//是否强制申请

                              
                                if (_CertificateChangeOB.ChangeRemark == "申请强制执行")
                                {
                                    RadioButtonList_enforce.ClearSelection();
                                    RadioButtonList_enforce.Items.FindByValue("申请强制执行").Selected = true;
                                    div_enforceType.Style.Remove("display");//申请强制执行原因
                                    if (_CertificateChangeOB.ENT_ContractType.HasValue)
                                    {
                                        RadioButtonList_enforceType.Items.FindByValue(_CertificateChangeOB.ENT_ContractType.ToString()).Selected = true;
                                    }
                                    div_DelContractDate.Style.Remove("display");//解除劳动关系日期
                                    if (_CertificateChangeOB.ENT_ContractStartTime.HasValue)
                                    {
                                        RadDatePickerDelContractDate.SelectedDate = _CertificateChangeOB.ENT_ContractStartTime;
                                    }

                                    divCancelReason.Style.Add("display", "none");
                                    div_NotWorkDate.Style.Add("display", "none");

                                    //certhfchange.ChangeRemark = "申请强制执行";
                                    ButtonExit.Text = "提交建委审核";
                                }
                                else
                                {
                                    RadioButtonList_enforce.ClearSelection();
                                    RadioButtonList_enforce.Items.FindByValue("").Selected = true;
                                    div_enforceType.Style.Add("display", "none");
                                    div_DelContractDate.Style.Add("display", "none");
                                    div_NotWorkDate.Style.Add("display", "none");
                                    //certhfchange.ChangeRemark = null;
                                    ButtonExit.Text = "提交单位审核";

                                    if (RadTextBoxChangeType.SelectedItem.Text == "注销")
                                    {
                                        divCancelReason.Style.Remove("display");                                      

                                        if (_CertificateChangeOB.ENT_ContractENDTime.HasValue)//不在该单位从事安全生产管理工作
                                        {
                                            div_NotWorkDate.Style.Remove("display");//不在该单位从事安全生产管理工作日期
                                            RadioButtonListCancelReason.SelectedIndex = 1;
                                            RadDatePickerNotWorkDate.SelectedDate = _CertificateChangeOB.ENT_ContractENDTime;
                                        }
                                    }
                                    else
                                    {
                                        divCancelReason.Style.Add("display", "none");
                                        div_NotWorkDate.Style.Add("display", "none");
                                    }
                                  
                                }
                            }

                            if (IfExistRoleID("0") == false)
                            {
                                RadioButtonList_enforce.Enabled = false;
                                RadioButtonList_enforceType.Enabled = false;
                                RadDatePickerDelContractDate.Enabled = false;
                                RadioButtonListCancelReason.Enabled = false;
                                RadDatePickerNotWorkDate.Enabled = false;
                            }   
                        }

                        if (certificateob.PostTypeID > 1)
                        {
                            trUnitName.Style.Add("display", "none");
                            trUnitCode.Style.Add("display", "none");
                            trCheckHead.Style.Add("display", "none");
                            trCheckData.Style.Add("display", "none");
                            spanSanLeiRenTip.Visible = false;

                            trTS_OldUnitName.Style.Add("display", "none");
                            trTS_OldUnitCode.Style.Add("display", "none");
                        }

                        //td_ChangeRemark.InnerText =string.IsNullOrEmpty(_CertificateChangeOB.ChangeRemark)?"": string.Format("备注：{0}",_CertificateChangeOB.ChangeRemark);

                        //if (_CertificateChangeOB.ChangeType=="离京变更" && string.IsNullOrEmpty(_CertificateChangeOB.ChangeRemark)==false)
                        //{
                        //    foreach(RadComboBoxItem i in RadComboBoxSheng.Items)
                        //    {
                        //        if(_CertificateChangeOB.ChangeRemark.IndexOf(i.Value) != -1)
                        //        {
                        //            i.Selected=true;
                        //        }                               
                        //    }
                        //    tdLableSheng.Visible = true;
                        //    tdShenge.Visible = true;
                        //    tdLableNewUnitName.Visible = false;
                        //    tdNewUnitName.Visible = false;
                        //    divLableNewUnitCode.Visible = false;
                        //    divNewUnitCode.Visible = false;
                        //}

                        SetButtonEnable(_CertificateChangeOB);

                        ShowSheBao(certificateob, _CertificateChangeOB);

                        BindCheckHistory(_CertificateChangeOB.CertificateChangeID.Value);

                        BindFile(ApplyID);

                        //if(_CertificateChangeOB.Status==EnumManager.CertificateChangeStatus.WaitUnitCheck
                        //    && TableJWCheck.Visible == true)
                        //{
                        //    UIHelp.layerAlert(Page, "此申请尚未经过单位确认，除非原单位已注销等特殊情况，建委审批人员可酌情跳过原单位确认，直接审批。");
                        //}
                    }

                    #endregion 编辑或查看

                    SetUploadFileType();
                    //if (_CertificateChangeOB.ChangeRemark == "申请强制执行")
                    //{
                    //    RadioButtonList_enforce.ClearSelection();
                    //    RadioButtonList_enforce.Items.FindByValue("申请强制执行").Selected = true;
                    //    div_enforce.Style.Remove("display");
                    //    //div_jcldht.Style.Remove("display");
                    //}
                    //else
                    //{
                    //    RadioButtonList_enforce.ClearSelection();
                    //    RadioButtonList_enforce.Items.FindByValue("").Selected = true;
                    //    div_enforce.Style.Add("display", "none");
                    //    //div_jcldht.Style.Add("display", "none");
                    //}
                }
                else
                {
                    #region 新增

                    string ChangeType = "";
                    if (string.IsNullOrEmpty(Request.QueryString["t"]) == false)
                    {
                        RadComboBoxItem rcbi = null;
                        ChangeType = Utility.Cryptography.Decrypt(Request.QueryString["t"]);
                        switch (ChangeType)
                        {
                            case "j"://京内变更
                                rcbi = RadTextBoxChangeType.FindItemByValue(EnumManager.CertificateUpdateType.ChangeInBeiJing);
                                break;
                            case "z"://注销
                                rcbi = RadTextBoxChangeType.FindItemByValue(EnumManager.CertificateUpdateType.Logout);
                                DisableControl();
                                break;
                            case "l"://离京
                                rcbi = RadTextBoxChangeType.FindItemByValue(EnumManager.CertificateUpdateType.OutBeiJing);
                                DisableControl();
                                TSTr.Style.Add("display", "table-row");
                                //tdLableSheng.Visible = true;
                                //tdShenge.Visible = true;
                                //tdLableNewUnitName.Visible = false;
                                //tdNewUnitName.Visible = false;
                                //divLableNewUnitCode.Visible = false;
                                //divNewUnitCode.Visible = false;
                                break;
                        }
                        if (rcbi != null)
                        {
                            rcbi.Selected = true;
                        }
                    }

                    int CertificateID = Convert.ToInt32(Utility.Cryptography.Decrypt(Request.QueryString["c"]));
                    CertificateOB certificateob = CertificateDAL.GetObject(CertificateID);
                    ViewState["certificateob"] = certificateob;
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, certificateob.WorkerCertificateCode);

                    if (certificateob.PostTypeID != 1)
                    {
                        tipPosttype1.Visible = false;
                    }

                    if (certificateob.PostTypeID == 1 && ChangeType == "j" && PersonType==2)//三类人员京内变更显示职务、技术职称
                    {
                        trJob.Visible = true;
                        tableHeTong.Style.Add("display", "inline");
                        if (certificateob.PostID == 148)//项目负责人
                        {
                            RadComboBoxJob.FindItemByText("项目负责人（项目经理）").Selected = true;
                            RadComboBoxJob.Enabled = false;
                        }
                        else if (certificateob.PostID == 6 || certificateob.PostID == 1123 || certificateob.PostID == 1125)//安全员
                        {
                            RadComboBoxJob.FindItemByText("专职安全生产管理人员").Selected = true;
                            RadComboBoxJob.Enabled = false;
                        }
                        else if (certificateob.PostID == 147)
                        {
                            RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("项目负责人（项目经理）"));
                            RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("专职安全生产管理人员"));

                            if (string.IsNullOrEmpty(certificateob.Job) == false)
                            {
                                RadComboBoxItem find = RadComboBoxJob.FindItemByText(certificateob.Job);
                                if (find != null)
                                {
                                    find.Selected = true;
                                }
                            }
                        }

                        if (RadComboBoxSKILLLEVEL.FindItemByText(certificateob.SkillLevel) != null) //技术职称
                        {
                            RadComboBoxSKILLLEVEL.FindItemByText(certificateob.SkillLevel).Selected = true;
                        }
                    }
                    else
                    {
                        trJob.Visible = false;
                        tableHeTong.Style.Add("display", "none");
                        ValidatorENT_ContractENDTime.Enabled = false;
                        ValidatorENT_ContractStartTime.Enabled = false;
                    }



                    if (string.IsNullOrEmpty(certificateob.FacePhoto) == false)
                    {
                        if(RadTextBoxChangeType.SelectedValue==EnumManager.CertificateUpdateType.ChangeInBeiJing)
                        {
                            CheckBoxModifyPhoto.Visible = true;
                        }
                        else
                        {
                            CheckBoxModifyPhoto.Visible = false;
                        }
                       
                        TableUploadPhoto.Style.Add("display", "none");
                    }
                    else
                    {
                        CheckBoxModifyPhoto.Visible = false;
                        TableUploadPhoto.Style.Add("display", "block");
                    }

                    //离京申请只有“三类人员”和“造价员”才能申请
                    if (certificateob.PostTypeID != 1 && certificateob.PostTypeID != 3)
                    {
                        RadTextBoxChangeType.Items.FindItemByValue("离京变更").Remove();
                    }

                    ViewState["PostTypeID"] = certificateob.PostTypeID.Value;
                    LabelPostType.Text = UIHelp.GetPostTypeNameByID(certificateob.PostTypeID.ToString());//岗位类别ID
                    RadTextBoxCertificateCode.Text = certificateob.CertificateCode;     //证书编号
                    lblWorkerName.Text = certificateob.WorkerName;
                    RadTextBoxWorkerName.Text = certificateob.WorkerName;          //姓名


                    lblSex.Text = certificateob.Sex;   //性别
                    for (int i = 0; i < 2; i++)
                    {
                        if (rdbtnlistSex.Items[i].Text == certificateob.Sex.ToString())
                        {
                            rdbtnlistSex.Items[i].Selected = true;
                            break;
                        }
                    }

                    lblBirthday.Text = Convert.ToDateTime(certificateob.Birthday.ToString()).ToString("yyyy-MM-dd"); //原出生日期
                    RadDatePickerNewBirthday.SelectedDate = certificateob.Birthday;

                    lblWorkerCertificateCode.Text = certificateob.WorkerCertificateCode;
                    LabelWorkerCertificateCode.Text = certificateob.WorkerCertificateCode;     //证件号码


                    RadTextBoxUnitName.Text = certificateob.UnitName;//原工作单位
                    RadTextBoxNewUnitName.Text = certificateob.UnitName;


                    RadTextBoxUnitCode.Text = certificateob.UnitCode;        //原机构代码
                    RadTextBoxNewUnitCode.Text = certificateob.UnitCode;


                    if (certificateob.PostTypeID == 3 && string.IsNullOrEmpty(certificateob.AddItemName) == false)//造价员
                    {
                        RadTextBoxPostID.Text = certificateob.AddItemName;
                    }
                    else
                    {
                        RadTextBoxPostID.Text = PostInfoDAL.GetObject(certificateob.PostID.Value).PostName;
                    }

                    if (RadTextBoxChangeType.SelectedValue == "离京变更" || RadTextBoxChangeType.SelectedValue == "注销")
                    {
                        LabelTS_WorkerName.Text = certificateob.WorkerName;//姓名
                        LabelTS_Sex.Text = certificateob.Sex;   //性别
                        LabelTS_Birthday.Text =certificateob.Birthday.Value.ToString("yyyy-MM-dd"); //原出生日期                          
                        LabelTS_WorkerCertificateCode.Text = certificateob.WorkerCertificateCode;
                        RadTextBoxTS_OldUnitName.Text = certificateob.UnitName;//原工作单位   
                        RadTextBoxTS_OldUnitCode.Text = certificateob.UnitCode;        //原机构代码
                        tableOldCert2.Style.Add("display", "table");
                        tableOldCert1.Style.Add("display", "none");
                        step_已办结.InnerText = "上报建设部（办结）";

                        if (certificateob.PostTypeID == 1)
                        {
                            tr_enforce.Style.Remove("display");

                            if (RadTextBoxChangeType.SelectedValue == "注销")
                            {
                                divCancelReason.Style.Remove("display");                               
                            }
                        }                         
                    }

                    if (certificateob.PostTypeID > 1)
                    {
                        trUnitName.Style.Add("display", "none");
                        trUnitCode.Style.Add("display", "none");
                        trCheckHead.Style.Add("display", "none");
                        trCheckData.Style.Add("display", "none");
                        spanSanLeiRenTip.Visible = false;

                        trTS_OldUnitName.Style.Add("display", "none");
                        trTS_OldUnitCode.Style.Add("display", "none");
                    }


                    ////临时增加两个月变更时间
                    //if (DateTime.Compare(certificateob.ValidEndDate.Value, DateTime.Now.AddMonths(-2).AddDays(-1)) <= 0)
                    //{
                    //    UIHelp.layerAlert(Page, "证书已过期，不能变更！");
                    //    btnSave.Enabled = false;
                    //    return;
                    //}
                    if (DateTime.Compare(certificateob.ValidEndDate.Value, DateTime.Now.AddDays(-1)) <= 0)
                    {
                        UIHelp.layerAlert(Page, "证书已过期，不能变更！");
                        btnSave.Enabled = false;
                        return;
                    }
                    if (certificateob.Status == EnumManager.CertificateUpdateType.OutBeiJing)
                    {
                        UIHelp.layerAlert(Page, "证书已做过离京变更，无法申请！");
                        btnSave.Enabled = false;
                        return;
                    }
                    if (certificateob.Status == EnumManager.CertificateUpdateType.Logout)
                    {
                        UIHelp.layerAlert(Page, "证书已注销，无法申请！");
                        btnSave.Enabled = false;
                        return;
                    }
                    SetButtonEnable(null);
                    lblApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    RadTextBoxLinkWay.Text = _WorkerOB.Phone;
                    UIHelp.SetReadOnly(RadTextBoxLinkWay, true);//不允许修改来自大厅实名制认证电话
                    #endregion 新增
                }
            }
            else
            {
                TableUploadPhoto.Style.Add("display", (CheckBoxModifyPhoto.Checked == true) ? "block" : "none");
                if (Request["__EVENTTARGET"] == "refreshFile")//上传或删除附件刷新列表
                {
                    BindFile(ApplyID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", btnSave.ClientID), true);
                }
                if (Request["__EVENTTARGET"] == "ButtonExit")//
                {
                    //ButtonExit_Click(sender, e);
                   
                }
            }

        }

        /// <summary>
        /// 禁用变更字段修改
        /// </summary>
        private void DisableControl()
        {
            UIHelp.SetReadOnly(RadTextBoxWorkerName, true);
            UIHelp.SetReadOnly(RadDatePickerNewBirthday, true);
            //UIHelp.SetReadOnly(RadTextBoxWorkerCertificateCode, true);
            UIHelp.SetReadOnly(RadTextBoxNewUnitName, true);
            UIHelp.SetReadOnly(RadTextBoxNewUnitCode, true);
            trSelectUnit.Style.Add("display", "none");
            rdbtnlistSex.Enabled = false;
        }

        //操作按钮控制
        /// <summary>
        /// 操作按钮控制
        /// </summary>
        /// <param name="ApplyStatus"></param>
        protected void SetButtonEnable(CertificateChangeOB o)
        {
            string ApplyStatus = (o==null?"":o.Status);
            SetStep(ApplyStatus);
            trFuJanTitel.Visible = false;
            trFuJan.Visible = false;
            switch (ApplyStatus)
            {
                case "":
                    btnSave.Enabled = true;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//提交单位审核 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = (myCertificateOB.PostTypeID > 1?"提交建委审核":"提交单位审核");
                   
                   
                    if (RadTextBoxChangeType.SelectedItem.Text == EnumManager.CertificateUpdateType.ChangeInBeiJing)
                    {
                        if (myCertificateOB.PostTypeID ==1) trSelectUnit.Style.Add("display", "normal");
                    }
                    break;
                case EnumManager.CertificateChangeStatus.NewSave:
                    btnSave.Enabled = true;//保 存
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位审核 
                    ButtonDelete.Enabled = true;//删除
                    ButtonExit.Text = (myCertificateOB.PostTypeID > 1 ? "提交建委审核" : 
                        (o.ChangeRemark=="申请强制执行"?"提交建委审核":"提交单位审核")
                        );
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        if (RadTextBoxChangeType.SelectedItem.Text == EnumManager.CertificateUpdateType.ChangeInBeiJing)
                        {
                            //div_LaoDongHeTong.Visible = true;
                            div_SheBao.Visible = true;
                            if (myCertificateOB.PostTypeID == 1) trSelectUnit.Style.Add("display", "normal");
                        }
                    }
                    break;
                case EnumManager.CertificateChangeStatus.WaitUnitCheck:
                    btnSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = false;//删除
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = true;//取消申报 
                    ButtonExit.Text = "取消申报";
                    if (IfExistRoleID("2") == true)
                    {
                        TableUnitCheck.Visible = true;
                    }
                    if (ValidPageViewLimit(RoleIDs, "CertifManage/CertifChangeCheckConfirm.aspx") == true)//审核权限
                    {
                        TableJWCheck.Visible = true;
                    }
                    break;
                case EnumManager.CertificateChangeStatus.SendBack:
                    btnSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = true;//删除
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位确认  
                    ButtonExit.Text = (myCertificateOB.PostTypeID > 1 ? "提交建委审核" :
                         (o.ChangeRemark == "申请强制执行" ? "提交建委审核" : "提交单位审核")
                        );

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        if (RadTextBoxChangeType.SelectedItem.Text == EnumManager.CertificateUpdateType.ChangeInBeiJing)
                        {
                            //div_LaoDongHeTong.Visible = true;
                            div_SheBao.Visible = true;
                            if (myCertificateOB.PostTypeID == 1) trSelectUnit.Style.Add("display", "normal");
                        }
                    }

                    break;
                case EnumManager.CertificateChangeStatus.Applyed://已申请
                    if (ValidPageViewLimit(RoleIDs, "CertifManage/CertifChangeCheckConfirm.aspx") == true)//审核权限
                    {
                        TableJWCheck.Visible = true;
                    }
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = true;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;
                case EnumManager.CertificateChangeStatus.Checked://已审核
                    if (ValidPageViewLimit(RoleIDs, "CertifManage/CertifChangeConfirm.aspx") == true)//决定权限
                    {
                        TableConfrim.Visible = true;
                    }
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;
                case EnumManager.CertificateChangeStatus.Noticed://已告知
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;

                default:
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;
            }

            if (myCertificateOB.PostTypeID > 1)
            {
                divStepDesc.InnerText = "操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交建委审核";
            }

            //个人登录后
            if (IfExistRoleID("0") == true)
            {
                divCtl.Visible = true;
                //divRtn.Visible = false;
            }
            else
            {
                divCtl.Visible = false;
                //divRtn.Visible = true;
            }


            btnSave.CssClass = btnSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExport.CssClass = ButtonExport.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExit.CssClass = ButtonExit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";

        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_填报中.Attributes["class"] = step_填报中.Attributes["class"].Replace(" green", "");
            step_待单位确认.Attributes["class"] = step_待单位确认.Attributes["class"].Replace("green", "");
            step_已申请.Attributes["class"] = step_已申请.Attributes["class"].Replace(" green", "");
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
            step_已告知.Attributes["class"] = step_已告知.Attributes["class"].Replace(" green", "");
            step_已办结.Attributes["class"] = step_已办结.Attributes["class"].Replace(" green", "");
                        
            switch (ApplyStatus)
            {
                case EnumManager.CertificateChangeStatus.NewSave:
                    step_填报中.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.WaitUnitCheck:
                    step_待单位确认.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.Applyed:
                    step_已申请.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.Checked:
                    step_已审核.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.Noticed:
                    if (myCertificateOB.Status == "注销" || myCertificateOB.Status == "离京变更")
                    {
                        if (myCertificateOB != null && myCertificateOB.QRCodeTime > myCertificateOB.CheckDate)
                        {
                            step_已办结.Attributes["class"] += " green";
                        }
                        else
                        {
                            step_已告知.Attributes["class"] += " green";
                        }
                    }
                    else
                    {
                        CertificateChangeOB _CertificateChangeOB = (CertificateChangeOB)ViewState["certhfchange"];

                        if (myCertificateOB != null && myCertificateOB.ZZUrlUpTime > _CertificateChangeOB.ConfrimDate)
                        {
                            step_已办结.Attributes["class"] += " green";
                        }
                        else
                        {
                            step_已告知.Attributes["class"] += " green";
                        }
                    }
                    break;              
                default:
                    step_填报中.Attributes["class"] += " green";
                    break;
            }
        }

        /// <summary>
        /// 获取个人信息变更内容说明
        /// </summary>
        /// <returns>个人信息变更内容说明</returns>
        private string GetChange()
        {
            CertificateOB _CertificateOB = (CertificateOB)ViewState["certificateob"];

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (lblWorkerName.Text != RadTextBoxWorkerName.Text) sb.Append(string.Format("姓名从【{0}】变为【{1}】；", lblWorkerName.Text, RadTextBoxWorkerName.Text.Trim()));//姓名
            if (lblWorkerCertificateCode.Text != LabelWorkerCertificateCode.Text) sb.Append(string.Format("证件号码从【{0}】变为【{1}】；", lblWorkerCertificateCode.Text, LabelWorkerCertificateCode.Text.Trim()));//证件号码
            if (lblBirthday.Text != RadDatePickerNewBirthday.SelectedDate.Value.ToString("yyyy-MM-dd"))//出生日期
            {
                sb.Append(string.Format("出生日期从【{0}】变为【{1}】；", lblBirthday.Text, RadDatePickerNewBirthday.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (lblSex.Text != rdbtnlistSex.Text)//性别
            {
                sb.Append(string.Format("性别从【{0}】变为【{1}】；", lblSex.Text, rdbtnlistSex.Text.Trim()));//性别
            }
            if (_CertificateOB.PostTypeID == 1 && RadTextBoxChangeType.SelectedItem.Value == "京内变更")
            {
                if (RadTextBoxUnitName.Text != RadTextBoxNewUnitName.Text)
                {
                    sb.Append(string.Format("单位名称从【{0}】变为【{1}】；", RadTextBoxUnitName.Text, RadTextBoxNewUnitName.Text.Trim()));//单位名称
                }
                if (RadTextBoxUnitCode.Text != RadTextBoxNewUnitCode.Text)
                {
                    sb.Append(string.Format("机构代码从【{0}】变为【{1}】；", RadTextBoxUnitCode.Text, RadTextBoxNewUnitCode.Text.Trim()));//机构代码
                }
            }

            if (trJob.Visible == true)//三类人，显示职务
            {
               
                if(string.IsNullOrEmpty(_CertificateOB.Job)==true ||  _CertificateOB.Job !=RadComboBoxJob.SelectedItem.Text)
                {
                    sb.Append(string.Format("职务从【{0}】变为【{1}】；", _CertificateOB.Job, RadComboBoxJob.SelectedItem.Text));//职务
                }
                if (string.IsNullOrEmpty(_CertificateOB.SkillLevel) == true || _CertificateOB.SkillLevel != RadComboBoxSKILLLEVEL.SelectedItem.Text)
                {
                    sb.Append(string.Format("技术职称从【{0}】变为【{1}】；", _CertificateOB.SkillLevel, RadComboBoxSKILLLEVEL.SelectedItem.Text));//技术职称
                }
            }

            if (CheckBoxModifyPhoto.Checked == true)
            {
                sb.Append("变更了一寸照片；");
            }

            return sb.ToString();
        }

        //保存申请表
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //int CertificateID = Convert.ToInt32(Request.QueryString["c"].ToString());   //证书id
            CertificateOB certificateob = ViewState["certificateob"] as CertificateOB;

            #region 有效性检查

            if ( RadTextBoxChangeType.SelectedItem.Value == "离京变更"
                || (certificateob.Status == "续期" && RadTextBoxChangeType.SelectedItem.Value != "注销" && certificateob.CheckDate.Value > Convert.ToDateTime("2025-03-22"))
                || (certificateob.Status == "首次" && RadTextBoxChangeType.SelectedItem.Value != "注销" && certificateob.CheckDate.Value > Convert.ToDateTime("2025-03-22"))
                || (RadTextBoxChangeType.SelectedItem.Value == "京内变更" && certificateob.Status != "京内变更")
                )
            {
                if (myCertificateOB.ZZUrlUpTime.HasValue == false 
                    || (myCertificateOB.ZZUrlUpTime < myCertificateOB.CheckDate && RadTextBoxChangeType.SelectedItem.Value != "离京变更")
                    )
                {
                    UIHelp.layerAlert(Page, "上一次业务办理结果尚未上传到国家（没有成功生成电子证书），无法申请新业务，请到电子证书下载页面查看原因，按要求整改。");
                    return;
                }
            }

            //有效性检查
            //            1、一年内最多5次变更单位（项目负责人变更特殊：当大于5次变更时，如果变更前不匹配，变更后匹配允许变更）；
            //2、已提交变更申请未审批的不能重复提交；
            //3、正在办理续期的，续期未结束不能办理变更；
            //4、证书处于锁定中，不允许变更（内部锁）；
            //5、证书为尚未发放状态（未打印），不允许变更电子版；
            //6、检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查），规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制
            //7、检查物业项目负责人是否被锁定（外部锁),（离京变更、注销或变更单位时检查），锁定时不能提交变更；
            //8、只有“安全生产考核三类人员”和“造价员”才能申请证书离京，其它（特种作业、职业技能、专业技术人员）不提供离京变更功能。
            //9、特殊：合同员不提供变更服务。
            //10、三类人员证书变更规则（只涉及京内变更、补办。离京变更和注销无限制）：
            //    10.1、组织机构代码和企业名称无变化，其它4项（姓名、证件号码、性别、出生日期）可以变更；
            //    10.2、组织机构代码不变，企业名称变更，检查新企业名称与组织机构代码是否与本（外）地企业资质库一致，一致允许变更；（相当于名称修正）
            //    10.3、组织机构代码变更，检查新企业名称与新组织机构代码是否与本地企业资质库一致，一致允许变更；（只检查本地资质库，即不允许变更到外地企业）
            //    10.4、组织机构代码变更，证书类型为项目负责人（B本）证书，检查变更后企业及人员信息是否与本地建造师（含外地进京备案建造师）企业信息和人员信息是否一致，一致允许变更；（即有B本必须有建造师，且单位、人员信息一致）

            //if (RadTextBoxWorkerCertificateCode.Text.Trim().Length != 18)
            //{
            //    UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
            //    return;
            //}
            //else if (Utility.Check.isChinaIDCard(RadTextBoxWorkerCertificateCode.Text.Trim()) == false)
            //{
            //    UIHelp.layerAlert(Page, "“身份证”格式不正确！");
            //    return;
            //}

            if (RadTextBoxChangeType.SelectedItem.Value == "")
            {
                UIHelp.layerAlert(Page, "请选择一个变更类型！", 5, 0);
                return;
            }

            //if (RadTextBoxChangeType.SelectedItem.Text == "离京变更" && RadComboBoxSheng.SelectedValue=="")
            //{
            //    UIHelp.layerAlert(Page, "请选择一个调入省（市）。", 5, 0);
            //    return;
            //}

            if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxNewUnitCode.Text) == false)
            {
                UIHelp.layerAlert(Page, "“现单位组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）", 5, 0);
                return;
            }

            if (certificateob.PostTypeID==1 && (RadTextBoxChangeType.SelectedItem.Text == "离京变更" || RadTextBoxChangeType.SelectedItem.Text == "注销"))
            {
                if (certificateob.PostID==148 && RadTextBoxChangeType.SelectedItem.Text == "离京变更")
                {
                    //如果北京存在有效的一建或二建，不允许发起B本离京变更（即变一二建，才能变B）
                    bool ifHaveJZS=UIHelp.IfExistJZSInBeijing(LabelWorkerCertificateCode.Text.Trim());
                    if(ifHaveJZS==true)
                    {
                        UIHelp.layerAlert(Page, "系统检验到您存在北京有效一、二级建造师注册证书，不能申请B本的离京变更。", 5, 0);
                        return;
                    }
                }

                if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
                {
                    if (RadDatePickerDelContractDate.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入解除劳动关系日期", 5, 0);
                        return;
                    }                    
                }

                if (RadTextBoxChangeType.SelectedItem.Text == "注销" && RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                {
                    if (RadDatePickerNotWorkDate.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入不在该单位从事安全生产管理工作日期", 5, 0);
                        return;
                    }
                }
            }

            if(trJob.Visible==true)//三类人，显示职务
            {
                #region 三类人

                if (RadComboBoxJob.SelectedValue == "")
                {
                    UIHelp.layerAlert(Page, "请选择所在单位职务。", 5, 0);
                    return;
                }
                if (RadComboBoxSKILLLEVEL.SelectedValue == "")
                {
                    UIHelp.layerAlert(Page, "请选择技术职称。", 5, 0);
                    return;
                }

                if (certificateob.PostID == 148 && RadComboBoxJob.SelectedItem.Text != "项目负责人（项目经理）")
                {
                    UIHelp.layerAlert(Page, "项目负责人职务必须选择“项目负责人（项目经理）”。", 5, 0);
                    return;
                }
                if ((certificateob.PostID == 6 || certificateob.PostID == 1123 || certificateob.PostID == 1125) && RadComboBoxJob.SelectedItem.Text != "专职安全生产管理人员")
                {
                    UIHelp.layerAlert(Page, "专职安全生产管理人员职务必须选择“专职安全生产管理人员”。", 5, 0);
                    return;
                }
                if (certificateob.PostID == 147
                    && (RadComboBoxJob.SelectedItem.Text == "项目负责人（项目经理）" || RadComboBoxJob.SelectedItem.Text == "专职安全生产管理人员")
                    )
                {
                    UIHelp.layerAlert(Page, "企业主要负责人职务不允许选择“项目负责人（项目经理）”或“专职安全生产管理人员”，请选择其他职务。", 5, 0);
                    return;
                }

                DateTime myBirthday = RadDatePickerNewBirthday.SelectedDate.Value;//生日
                string mySex = rdbtnlistSex.SelectedValue.Trim();   //性别

                if (certificateob.PostID == 147)//A本
                {
                    if (RadTextBoxChangeType.SelectedItem.Value == "京内变更")
                    {
                        if (RadComboBoxJob.SelectedItem.Text == "法定代表人")//职务
                        {
                            if (IfFaRen == false)//非法人
                            {
                                UIHelp.layerAlert(Page, "校验未通过：工商信息显示您并非单位企业法人，不能以法定代表人职务变更。", 5, 0);
                                return;
                            }

                            CertificateOB FROb = CertificateDAL.GetFRCertA(RadTextBoxNewUnitCode.Text.Trim());
                            if (FROb != null && FROb.CertificateID != certificateob.CertificateID)
                            {
                                UIHelp.layerAlert(Page, string.Format("校验未通过：一个单位只能允许一人以法定代表人职务持有A证，变更单位已经存在法人A证【{0}】。", FROb.CertificateCode),5,0);
                                return;
                            }
                            //法人不受年龄显限制
                        }
                        else//非法人
                        {
                            //非企业法人，受年龄限制
                            if (Utility.Check.CheckBirthdayLimit(certificateob.PostID.Value, LabelWorkerCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                            {
                                UIHelp.layerAlert(Page, "校验未通过：您已超龄不予变更！", 5, 0);
                                return;
                            }

                            CertificateOB FROb = CertificateDAL.GetOtherNoFRCertA(LabelWorkerCertificateCode.Text.Trim(), certificateob.CertificateCode);
                            if (FROb != null)
                            {
                                UIHelp.layerAlert(Page, string.Format("校验未通过： 不允许持有多本非法人职务的A本。<br />你已持有非法人职务的A本【{0}】，不能重复持有。", FROb.CertificateCode), 5, 0);
                                return;
                            }
                           
                        }
                    }
                }
                else if(RadTextBoxChangeType.SelectedItem.Value == "京内变更")//B、C1、C2、C3
                {
                    if (Utility.Check.CheckBirthdayLimit(certificateob.PostID.Value, LabelWorkerCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                    {
                        UIHelp.layerAlert(Page, "您已超龄不予变更！", 5, 0);
                        return;
                    }
                }

                if (RadTextBoxChangeType.SelectedItem.Value == "京内变更")
                {
                    if (string.IsNullOrEmpty(RadioButtonListENT_ContractType.SelectedValue))
                    {
                        UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
                        return;
                    }
                    if (RadDatePickerENT_ContractStartTime.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入劳动合同开始时间", 5, 0);
                        return;
                    }
                    if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入劳动合同结束时间", 5, 0);
                        return;
                    }

                     //if (certificateob.PostID == 147//A本
                     //    && RadComboBoxJob.SelectedItem.Text =="法定代表人"
                     //    && IfFaRen ==false
                     //    )
                     //{
                     //    UIHelp.layerAlert(Page, "职务校验未通过，在工商登记信息中无法查询到变更后单位您的法人信息，请先去变更工商信息。", 5, 0);
                     //    return;
                     //}
                }

                #endregion
            }

            if (ViewState["certhfchange"] == null)//new
            {
                int changeCount = CertificateChangeDAL.SelectCount(string.Format(" and CertificateID={0} and [STATUS] <> '{1}'", certificateob.CertificateID, EnumManager.CertificateChangeStatus.Noticed));
                if (changeCount > 0)
                {
                    UIHelp.layerAlert(Page, "您已经提交过变更申请，无法重复申请！", 5, 0);
                    return;
                }
            }

            //在办续期申请数量
            int countContinueApply = CertificateContinueDAL.SelectCount(string.Format(" and CertificateID={0} and [STATUS] <> '{1}' ", certificateob.CertificateID, EnumManager.CertificateContinueStatus.Decided));
            if (countContinueApply > 0)
            {
                UIHelp.layerAlert(Page, "证书正在办理续期，未办结不能同时申请变更。", 5, 0);
                return;
            }

            if (certificateob.PostID.Value == 6 || certificateob.PostID.Value == 1123)
            {
                if (CertificateMergeDAL.IfExistsApplying(certificateob.CertificateID.Value) == true)
                {
                    UIHelp.layerAlert(Page, "证书正在办理C1、C2合并，不能同时申请变更。", 5, 0);
                    return;
                }
            }

            string lockReason = "";
            bool lockStatus = CertificateLockDAL.GetCertificateLockStatus(certificateob.CertificateID.Value, ref lockReason);
            if (lockStatus == true)//已加锁，内部锁
            {
                UIHelp.layerAlert(Page, string.Format("证书处于锁定中，不允许变更。锁定原因：{0}", lockReason), 5, 0);
                return;
            }



            //if (ViewState["certhfchange"] != null)
            //{
            //    CertificateChangeOB f = CertificateChangeDAL.GetApplyingObject(certificateob.CertificateID.Value);
            //    if (f != null)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("该证书有一个{0}申请尚未办结，无法发起另一个申请，请取消前一个申请或等待办结后再申请新业务。", f.ChangeType));
            //        return;
            //    }
            //}

            //if (string.IsNullOrEmpty(certificateob.CaseStatus) == true && certificateob.Status == "首次" && certificateob.ConferDate.Value.Year >= 2012)
            //{
            //    UIHelp.layerAlert(Page, "此证书为尚未发放的新证书，不允许变更！");
            //    return;
            //}


            if (RadTextBoxChangeType.SelectedItem.Value == "京内变更")
            {
                //UIHelp.layerAlert(Page, GetChange());
                //return;
                if (GetChange() == "")
                {
                    UIHelp.layerAlert(Page, "没有变更任何内容，请检查填写是否正确！", 5, 0);
                    return;
                }

                if (UnitDAL.CheckGongShang(RadTextBoxNewUnitCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxNewUnitName.Text.Trim()), 5, 0);
                    return;
                }
            }

            //检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查）
            //规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制
            if (certificateob.PostID.Value == 55 &&
                (RadTextBoxChangeType.SelectedItem.Text == "离京变更"
                || RadTextBoxChangeType.SelectedItem.Text == "注销"
                || (RadTextBoxChangeType.SelectedItem.Text == "京内变更" && certificateob.UnitCode != RadTextBoxNewUnitCode.Text.Trim())
                || (RadTextBoxChangeType.SelectedItem.Text == "补办" && certificateob.UnitCode != RadTextBoxNewUnitCode.Text.Trim()))
                )
            {
                int workersCountLimit = UIHelp.QueryCaiQianCountLimitOfUnit(Page, certificateob.UnitCode);//最低人数要求
                if (workersCountLimit > 0)
                {
                    int workersCount = CertificateDAL.SelectCount(string.Format(" and UnitCode='{1}' and PostID=55 and ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd"), certificateob.UnitCode));
                    if ((workersCount - 1) < workersCountLimit)
                    {
                        UIHelp.layerAlert(Page, string.Format("根据企业资质要求，您本次申请将导致企业拆迁员数量低于最低人数（{0}人）要求，无法为您提交申请！", workersCountLimit.ToString()), 5, 0);
                        return;
                    }
                }
            }

            //检查物业项目负责人是否被锁定（离京变更、注销或变更单位时检查），外部锁
            if (certificateob.PostID.Value == 159 &&
                (RadTextBoxChangeType.SelectedItem.Text == "离京变更"
                || RadTextBoxChangeType.SelectedItem.Text == "注销"
                || (RadTextBoxChangeType.SelectedItem.Text == "京内变更" && certificateob.UnitCode != RadTextBoxNewUnitCode.Text.Trim())
                || (RadTextBoxChangeType.SelectedItem.Text == "补办" && certificateob.UnitCode != RadTextBoxNewUnitCode.Text.Trim()))
                )
            {
                string checkResult = UIHelp.QueryCertificateLockFromBaseDB("物业项目负责人", certificateob.CertificateCode);
                if (checkResult != "")
                {
                    UIHelp.layerAlert(Page, checkResult, 5, 0);
                    return;
                }
            }

            //三类人员京内变更或补办
            if (certificateob.PostTypeID.Value == 1 && (RadTextBoxChangeType.SelectedItem.Value == "京内变更" || RadTextBoxChangeType.SelectedItem.Value == "补办"))
            {
                string UnitName = "";//企业名称

                //组织机构代码不变，企业名称变更，检查新企业名称与组织机构代码是否与本（外）地企业资质库一致，一致允许变更；（相当于名称修正）
                if (RadTextBoxUnitCode.Text.Trim() == RadTextBoxNewUnitCode.Text.Trim()
                    && RadTextBoxUnitName.Text.Trim() != RadTextBoxNewUnitName.Text.Trim())
                {

                    UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxNewUnitCode.Text.Trim(), false);
                    if (string.IsNullOrEmpty(UnitName))
                    {
                        UIHelp.layerAlert(Page, string.Format("您要变更到的企业“{0}”（组织机构代码：{1}）无建筑施工企业资质证书，不允许变更。", RadTextBoxNewUnitName.Text.Trim(), RadTextBoxNewUnitCode.Text.Trim()), 5, 0);
                        return;
                    }
                    else if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxNewUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                    {
                        UIHelp.layerAlert(Page, string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您变更的单位名称不符，不允许变更。”", RadTextBoxNewUnitCode.Text.Trim(), UnitName), 5, 0);
                        return;
                    }
                }

                //组织机构代码变更，检查新企业名称与新组织机构代码是否与本地企业资质库一致，一致允许变更；（只检查本地资质库，即不允许变更到外地企业）
                if (RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim())
                {
                    UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxNewUnitCode.Text.Trim(), true);
                    if (string.IsNullOrEmpty(UnitName))
                    {
                        UIHelp.layerAlert(Page, string.Format("您要变更到的企业“{0}”（组织机构代码：{1}）无本地建筑施工企业资质证书，不允许变更。", RadTextBoxNewUnitName.Text.Trim(), RadTextBoxNewUnitCode.Text.Trim()), 5, 0);
                        return;
                    }
                    if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxNewUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                    {
                        UIHelp.layerAlert(Page, string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您变更的单位名称不符，不允许变更。”", RadTextBoxNewUnitCode.Text.Trim(), UnitName), 5, 0);
                        return;
                    }

                    //组织机构代码变更，证书类型为项目负责人（B本）证书，检查变更后企业及人员信息是否与本地建造师（含外地进京备案建造师）企业信息和人员信息是否一致，一致允许变更；（即有B本必须有建造师，且单位、人员信息一致）
                    if (certificateob.PostID.Value == 148)
                    {
                        bool isJZS = UIHelp.CheckJZS("全部", LabelWorkerCertificateCode.Text.Trim(), this.RadTextBoxNewUnitCode.Text.Trim());
                        if (isJZS == false)
                        {
                            UIHelp.layerAlert(Page, @"变更后的项目负责人证书，单位信息和人员信息必须与建造师信息一致（且建造师未过期），您提交的变更申请中单位信息不匹配，不允许变更。", 5, 0);
                            return;
                        }
                    }

                    if (certificateob.PostID.Value == 147)//企业法人（A本）,不能变更到单位中存在相同人员已有A本单位
                    {
                        int countA = CommonDAL.SelectRowCount(string.Format("SELECT COUNT(*)  FROM [dbo].[CERTIFICATE]  where [WORKERCERTIFICATECODE]='{0}' and [UNITCODE]='{1}' AND PostID=147 and [STATUS]<>'注销' and [STATUS]<>'离京变更' and [VALIDENDDATE] >getdate()", LabelWorkerCertificateCode.Text.Trim(), RadTextBoxNewUnitCode.Text.Trim()));
                        if(countA >0)
                        {
                            UIHelp.layerAlert(Page, string.Format("您要变更到的企业“{0}”已经存在{1}的企业主要负责人证书，不允许多本A证变更到同一单位。", RadTextBoxNewUnitName.Text.Trim(), lblWorkerName.Text), 5, 0);
                            return;
                        }
                    }

                    if(ExamSignUpDAL.CheckIfHaveExamSinupNoFinishi(LabelWorkerCertificateCode.Text.Trim()) ==true)
                    {
                        UIHelp.layerAlert(Page, "您存在未办结的安管人员考试（报名审核中、考试未公告成绩、考试合格尚未发证），不允许变更证书到新单位。", 5, 0);
                        return;
                    }

                    //不受京内变更次数限制
                    bool TimesLimit = false;
                    TimesLimit = ValidResourceIDLimit(RoleIDs, "ManageApplyTimesLimit");
                    if (TimesLimit == false)
                    {
                        //int Times = CommonDAL.SelectRowCount("dbo.CertificateChange", string.Format(" and CertificateID={0} and ChangeType='{1}' and GetResult='通过' and (CheckDate between dateadd(year,-1,getdate()) and getdate())", certificateob.CertificateID, RadTextBoxChangeType.SelectedItem.Value));
                        //if (Times > 4)
                        //{
                        //    UIHelp.layerAlert(Page, "超过一年内允许最大变更单位次数，无法申请变更。", 5, 0);
                        //    return;
                        //}

                        int Times = CertificateChangeDAL.SelectChangeUnitCountYear(certificateob.CertificateID.Value);
                        if (certificateob.PostID == 148)
                        {
                            if (Times >= 2)
                            {
                                UIHelp.layerAlert(Page, "超过B证一年内允许最大变更单位次数2次，无法申请变更。", 5, 0);
                                return;
                            }
                        }
                        else
                        {
                            if (Times >= 4)
                            {
                                UIHelp.layerAlert(Page, "超过一年内允许最大变更单位次数4次，无法申请变更。", 5, 0);
                                return;
                            }
                        }
                    }
                }
            }

            //对特种作业人员和职业技能人员考核业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司
            if ((certificateob.PostTypeID.Value == 2 || certificateob.PostTypeID.Value == 4 || certificateob.PostTypeID.Value == 5)
                && RadTextBoxChangeType.SelectedItem.Value == "京内变更" 
                && RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim()
                )
            {
                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxNewUnitCode.Text.Trim(), false);
                if (string.IsNullOrEmpty(UnitName))
                {
                    UIHelp.layerAlert(Page, "您所在的企业不在本市管理的建筑企业资质库中，不允许变更，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。", 5, 0);
                    return;
                }
                if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxNewUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                {
                    UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxNewUnitCode.Text.Trim(), UnitName), 5, 0);
                    return;
                }
            }

            if (ViewState["certhfchange"] == null)//new
            {
                if (certificateob.PostTypeID.Value == 3
                 || certificateob.PostID.Value == 9
                 || certificateob.PostID.Value == 12
                 || certificateob.PostID.Value == 55
                 || certificateob.PostID.Value == 159
                 || certificateob.PostID.Value == 1009
                 || certificateob.PostID.Value == 1021
                 || certificateob.PostID.Value == 1024
                 )
                {
                    UIHelp.layerAlert(Page, "根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作", 5, 0);
                    return;
                }

                if (CertificateChangeDAL.SelectCount(string.Format(" and CertificateID={0} and ([Status] = '{1}' or [Status] = '{2}')", certificateob.CertificateID
                    , EnumManager.CertificateChangeStatus.NewSave
                    , EnumManager.CertificateChangeStatus.WaitUnitCheck)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！", 5, 0);
                    return;
                }
            }

            #endregion 有效性检查

            #region 上传个人照片

            if (RadUploadFacePhoto.UploadedFiles.Count > 0)
            {
                if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "照片格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "照片大小不能超过50k！");
                    return;
                }
            }

            if (CheckBoxModifyPhoto.Checked == true)//上传更新照片
            {
                if (ViewState["certhfchange"] == null //new
                    && RadUploadFacePhoto.UploadedFiles.Count == 0)//照片
                {
                    UIHelp.layerAlert(Page, "请选择要上传更新的照片！");
                    return;
                }


            }
            else//补充老照片
            {
                if (TableUploadPhoto.Visible == true && RadUploadFacePhoto.UploadedFiles.Count == 0 && ImgCode.Src.IndexOf("null.gif") > 0)//照片
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }

                //个人照片存放路径(按证件号码后3位)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = lblWorkerCertificateCode.Text.Length > 3 ? lblWorkerCertificateCode.Text : LabelWorkerCertificateCode.Text.Trim();
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");


                        if (lblWorkerCertificateCode.Text != LabelWorkerCertificateCode.Text)//更改证件号码
                        {
                            validFile.SaveAs(Path.Combine(workerPhotoFolder, LabelWorkerCertificateCode.Text + ".jpg"), true);
                            ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, LabelWorkerCertificateCode.Text);//绑定照片;
                        }
                        else
                        {
                            validFile.SaveAs(Path.Combine(workerPhotoFolder, lblWorkerCertificateCode.Text + ".jpg"), true);
                            ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(certificateob.FacePhoto, certificateob.WorkerCertificateCode);//绑定照片;
                        }

                        break;
                    }
                }
            }

            #endregion

            CertificateChangeOB certhfchange = ViewState["certhfchange"] == null ? new CertificateChangeOB() : (CertificateChangeOB)ViewState["certhfchange"];
            certhfchange.CertificateID = certificateob.CertificateID;
            certhfchange.ChangeType = RadTextBoxChangeType.SelectedItem.Text;   //变更类型 

            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更" || RadTextBoxChangeType.SelectedItem.Text == "注销")
            {
                if(RadioButtonList_enforce.SelectedValue=="申请强制执行")
                {
                    certhfchange.ChangeRemark = "申请强制执行";
                    certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                    certhfchange.ENT_ContractStartTime = RadDatePickerDelContractDate.SelectedDate;                   
                }
                else
                {
                    certhfchange.ChangeRemark = null;
                    certhfchange.ENT_ContractType = null;
                    certhfchange.ENT_ContractStartTime = null;
                    certhfchange.ENT_ContractENDTime = null;

                    if (RadTextBoxChangeType.SelectedItem.Text == "注销")
                    {
                        if (RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                        {
                            certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                            certhfchange.ChangeRemark = "不在从事安全生产管理工作";
                            certhfchange.ENT_ContractENDTime = RadDatePickerNotWorkDate.SelectedDate;
                        }
                        else
                        {
                            certhfchange.ChangeRemark = null;
                            certhfchange.ENT_ContractENDTime = null;
                        }
                    }
                    
                }
            }

            

            //if (RadTextBoxChangeType.SelectedItem.Text == "离京变更")
            //{
            //    certhfchange.ChangeRemark = string.Format("拟调入省（市）:{0}", RadComboBoxSheng.SelectedValue);
            //}

            certhfchange.WorkerName = lblWorkerName.Text.ToString();         //姓名
            certhfchange.NewWorkerName = RadTextBoxWorkerName.Text.Trim();
            certhfchange.Sex = lblSex.Text;  //性别
            certhfchange.NewSex = rdbtnlistSex.SelectedValue.Trim();
            certhfchange.Birthday = Convert.ToDateTime(lblBirthday.Text.ToString()); //出生日期
            if (RadDatePickerNewBirthday != null) certhfchange.NewBirthday = Convert.ToDateTime(RadDatePickerNewBirthday.SelectedDate);   //出生日期
            certhfchange.WorkerCertificateCode = lblWorkerCertificateCode.Text.Trim(); //证件号码
            certhfchange.NewWorkerCertificateCode = LabelWorkerCertificateCode.Text.Trim();
            certhfchange.UnitName = RadTextBoxUnitName.Text.Trim();   //原单位名称
            certhfchange.NewUnitName = RadTextBoxNewUnitName.Text.Trim();//现单位名称
            certhfchange.UnitCode = RadTextBoxUnitCode.Text.Trim(); //原单位组织机构代码
            certhfchange.NewUnitCode = RadTextBoxNewUnitCode.Text.Trim();//现单位组织机构代码
            certhfchange.OldUnitAdvise = "";// RadTextBoxOldUnitAdvise.Text.Trim();   //原单位意见
            certhfchange.NewUnitAdvise = "";// RadTextBoxNewUnitAdvise.Text.Trim();  //现单位意见
            certhfchange.OldConferUnitAdvise = "";//  RadTextBoxOldConferUnitAdvise.Text.Trim();   //原发证机关意见
            certhfchange.NewConferUnitAdvise = "";//  RadTextBoxNewConferUnitAdvise.Text.Trim();   //现发证机关意见
            certhfchange.OldUnitCheckTime = null;
            certhfchange.NewUnitCheckTime = null;
            certhfchange.GetDate = null;   //变更受理时间
            certhfchange.GetResult = null;     //变更受理结论
            certhfchange.GetMan = null;    //变更受理人
            certhfchange.GetCode = null;//变更受理编批号
            certhfchange.CheckDate = null;  //变更审核时间
            certhfchange.CheckResult = null;  //变更审核结论
            certhfchange.CheckMan = null; //变更审核人
            certhfchange.CheckCode = null;    //变更审核批号             
            certhfchange.ConfrimDate = null;  //变更决定时间 
            certhfchange.ConfrimResult = null;  //变更决定结论
            certhfchange.ConfrimMan = null;   //变更决定人
            certhfchange.ConfrimCode = null;   //变更决定批号
            certhfchange.NoticeDate = null; //变更告知时间 
            certhfchange.NoticeResult = null;  //变更告知结论
            certhfchange.NoticeMan = null;    //变更告知人
            certhfchange.NoticeCode = null;   //变更告知批号
            certhfchange.DealWay = ""; //rdZSCLFS.SelectedValue.Trim();     //证书处理方式
            //if (RadDatePickerApplyDate.SelectedDate != null)certhfchange.ApplyDate = Convert.ToDateTime(RadDatePickerApplyDate.SelectedDate);   //申请日期

            certhfchange.LinkWay = RadTextBoxLinkWay.Text.Trim();     //联系方式

            certhfchange.ConferDate = certificateob.ConferDate;  //发证日期
            certhfchange.ValidStartDate = certificateob.ValidStartDate;  //有效期起
            certhfchange.ValidEndDate = certificateob.ValidEndDate;//有效期止
            certhfchange.ConferUnit = certificateob.ConferUnit;   //发证机关
            certhfchange.ModifyPersonID = PersonID;   //最后修改人
            certhfchange.ModifyTime = DateTime.Now;   //最后修改时间
            certhfchange.Status = EnumManager.CertificateChangeStatus.NewSave;
            certhfchange.ApplyDate = DateTime.Now;

            if(RadComboBoxJob.SelectedItem.Value !="") certhfchange.Job = RadComboBoxJob.SelectedItem.Text;//职务
            if (RadComboBoxSKILLLEVEL.SelectedItem.Value != "") certhfchange.SkillLevel = RadComboBoxSKILLLEVEL.SelectedItem.Text;//技术职称

            if (RadioButtonListENT_ContractType.SelectedIndex > -1)
            {
                certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);
                //劳动合同开始时间
                certhfchange.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                //劳动合同结束时间
                if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                {
                    certhfchange.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                }
                else
                {
                    certhfchange.ENT_ContractENDTime = null;
                }
            }


            if (CheckBoxModifyPhoto.Checked == true)
            {
                certhfchange.IfUpdatePhoto = 1;//上传了新照片
            }
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                if (ViewState["certhfchange"] == null)//new
                {
                    string bgsq = UIHelp.GetNextBatchNumber(dtr,"BGSQ");//自动生成申请编号
                    certhfchange.ApplyCode = bgsq;
                    certhfchange.ApplyMan = PersonName;          //变更申请人
                    certhfchange.CreatePersonID = PersonID;    //创建人
                    certhfchange.CreateTime = DateTime.Now;  //创建时间                      

                    CertificateChangeDAL.Insert(dtr,certhfchange);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(dtr, certificateob.CertificateCode, string.Format("BG-{0}", certhfchange.CertificateChangeID), filetype);
                }
                else//edit
                {
                    CertificateChangeDAL.Update(dtr,certhfchange);
                }

                dtr.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "申请证书变更", string.Format("变更类型：{0}；证书编号：{1}。", RadTextBoxChangeType.SelectedItem.Text, RadTextBoxCertificateCode.Text));

                ViewState["certhfchange"] = certhfchange;

                if (CheckBoxModifyPhoto.Checked == true)//上传更新照片
                {
                    //变更证书个人照片
                    if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                    {
                        string workerPhotoFolder = "~/UpLoad/ChangePhoto/";//变更照片存放路径
                        string subPath = "";//照片分类目录（证件号码后3位）
                        foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                        {
                            subPath = RadTextBoxCertificateCode.Text;
                            subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证书编号后3位分目录存储
                            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ChangePhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ChangePhoto/" + subPath));
                            workerPhotoFolder = Server.MapPath("~/UpLoad/ChangePhoto/" + subPath + "/");
                            validFile.SaveAs(Path.Combine(workerPhotoFolder, certhfchange.CertificateChangeID.ToString() + ".jpg"), true);

                            ImgUpdatePhoto.Src = UIHelp.ShowFile(string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", subPath, certhfchange.CertificateChangeID));//绑定照片;
                            break;
                        }
                    }
                }

                SetButtonEnable(certhfchange);
                ShowSheBao(certificateob, certhfchange);
                BindFile(ApplyID);
                SetUploadFileType();
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "证书变更申请失败！", ex);
                return;
            }      

            ButtonExport.Enabled = true;
            ButtonExit.Enabled = true;

            //ABC持证保持相同单位检查
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (certificateob.PostTypeID == 1 && certhfchange.ChangeType == "京内变更" && certhfchange.UnitCode != certhfchange.NewUnitCode)
            {
                string sql = @"
                    select c.CertificateCode,case when a.NewUNITCODE is not null then '变更目标单位：' + a.NEWUNITNAME else '持证所属单位：' +c.UNITNAME end as UNITNAME
                    from [dbo].[CERTIFICATE] c
                    left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                    where c.[WorkerCertificateCode]='{0}' 
                        and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                        and exists(
                            select 1 from [dbo].[CERTIFICATE] c
                            left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                            where c.[WorkerCertificateCode]='{0}' 
                            and c.CertificateCode <> '{1}' and (a.NewUNITCODE <> '{2}' or a.NewUNITCODE is null)
                            and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                            and postid not in(
                                select postid from [dbo].[CERTIFICATE] c
                                left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                                where c.[WorkerCertificateCode]='{0}' 
	                                and c.POSTID = 147 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
	                                and case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end in
		                                (
			                                select  distinct case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end
			                                from [dbo].[CERTIFICATE] c
			                                left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
			                                where c.[WorkerCertificateCode]='{0}' 
			                                and c.posttypeid=1 and c.POSTID <> 147 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
			                                group by case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end
			                                having count(distinct case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end)=1
		                                )
                             ) 
                        ) order by c.CertificateCode";

                DataTable tb = CommonDAL.GetDataTable(string.Format(sql, certificateob.WorkerCertificateCode, certificateob.CertificateCode, certhfchange.NewUnitCode));
                if (tb != null && tb.Rows.Count > 0)
                {
                    foreach (DataRow r in tb.Rows)
                    {
                        sb.AppendFormat("<br />{0}，{1}。", r["CertificateCode"], r["UnitName"]);
                    }
                    sb.Insert(0, "<br /><br /><b style=color:red>重要提示：<br />住建部要求持证人同时持有有效的A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业。请您检查您的证书，同步按规则去申请其他证书变更，否则系统无法为您生成电子证书。<br /><br />当前证书状态如下：");
                    sb.Append("</b>");
                }
            }

            UIHelp.layerAlert(Page, string.Format("保存成功！<br />请打印申请表，如有要求手写签名、加盖单位公章的，请签名和加盖公章后扫描上传，提交审核！{0}", sb), "isfresh=true;parent.SetCwinHeight();");

        }

        //提交单位审核、取消申请
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            CertificateOB ob = (CertificateOB)ViewState["certificateob"];

            if (ButtonExit.Text == "取消申报"//取消申报
                || RadTextBoxChangeType.SelectedItem.Value != "京内变更"
               || (ob.PostID == 147 && IfFaRen == true)//法人进京A
               || certhfchange.SheBaoCheck.HasValue == false//申报尚未校验
               || certhfchange.SheBaoCheck == 1//社保校验合格
               || (ViewState["继续提交"] != null && Convert.ToBoolean(ViewState["继续提交"]) == true)//已读8表提示并继续提交
               )
            {
                #region  提交

                #region 清空审查结果
                certhfchange.OldUnitAdvise = "";//原单位意见
                certhfchange.NewUnitAdvise = "";//现单位意见
                certhfchange.OldConferUnitAdvise = "";//原发证机关意见
                certhfchange.NewConferUnitAdvise = "";//现发证机关意见
                certhfchange.OldUnitCheckTime = null;
                certhfchange.NewUnitCheckTime = null;
                certhfchange.DealWay = "";
                certhfchange.GetDate = null;   //变更受理时间
                certhfchange.GetResult = null;     //变更受理结论
                certhfchange.GetMan = null;    //变更受理人
                certhfchange.GetCode = null;//变更受理编批号
                certhfchange.CheckDate = null;  //变更审核时间
                certhfchange.CheckResult = null;  //变更审核结论
                certhfchange.CheckMan = null; //变更审核人
                certhfchange.CheckCode = null;    //变更审核批号             
                certhfchange.ConfrimDate = null;  //变更决定时间 
                certhfchange.ConfrimResult = null;  //变更决定结论
                certhfchange.ConfrimMan = null;   //变更决定人
                certhfchange.ConfrimCode = null;   //变更决定批号
                certhfchange.NoticeDate = null; //变更告知时间 
                certhfchange.NoticeResult = null;  //变更告知结论
                certhfchange.NoticeMan = null;    //变更告知人
                certhfchange.NoticeCode = null;   //变更告知批号

                certhfchange.ZACheckTime = null;
                certhfchange.ZACheckResult = null;
                certhfchange.ZACheckRemark = null;
                #endregion 清空审查结果


                if (ButtonExit.Text == "取消申报")//取消申报
                {
                    try
                    {
                        certhfchange.ModifyTime = DateTime.Now;
                        certhfchange.ModifyPersonID = PersonID;
                        certhfchange.Status = EnumManager.CertificateChangeStatus.NewSave;
                        CertificateChangeDAL.Update(certhfchange);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "取消证书变更申请失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "取消证书变更申请", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));
                    UIHelp.layerAlert(Page, "取消成功！", "var isfresh=true;parent.SetCwinHeight();");
                }
                else
                {
                   
                    #region 必须上传附件集合

                    System.Collections.Hashtable fj = null;//必须上传附件集合

                    //公共必须上传附件
                    fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.变更申请表扫描件,0},                   
                        {EnumManager.FileDataTypeName.证件扫描件,0}};

                    //已上传附件集合
                    DataTable dt = ApplyDAL.GetApplyFile(ApplyID);

                    //计数
                    foreach (DataRow r in dt.Rows)
                    {
                        if (fj.ContainsKey(r["DataType"].ToString()) == false)
                        {
                            fj.Add(r["DataType"].ToString(), 1);
                        }
                        else
                        {
                            fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
                        }
                    }
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (string k in fj.Keys)
                    {
                        if (Convert.ToInt32(fj[k]) == 0)
                        {
                            sb.Append(string.Format("、“{0}”", k));
                        }
                    }

                    if (RadTextBoxChangeType.SelectedItem.Value == "京内变更")
                    {

                        if (ob.PostID.Value == 147//企业主要负责人
                            && RadComboBoxJob.SelectedItem.Text == "法定代表人")
                        {

                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.企业营业执照扫描件) == false)
                            {
                                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.企业营业执照扫描件));
                            }
                        }
                        else if(ob.PostTypeID==1)
                        {
                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false //没上传社保证明
                           && (certhfchange.SheBaoCheck.HasValue == false || certhfchange.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                           )
                            {
                                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                            }
                        }
                    }
                    //else if (RadTextBoxChangeType.SelectedItem.Value == "离京变更" || RadTextBoxChangeType.SelectedItem.Value == "注销")
                    //{
                    //    if (ob.PostTypeID == 1)
                    //    {
                    //        if (certhfchange.ChangeRemark == "申请强制执行")
                    //        {
                    //            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.强制执行申请表) == false)//上传了强制执行申请表，可以不用企业确认，建委直接审核
                    //            {
                    //                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.强制执行申请表));
                    //            }

                    //            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.解除劳动合同证明) == false)
                    //            {
                    //                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.解除劳动合同证明));
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.强制执行申请表) == true
                    //                || ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.解除劳动合同证明) == true)//上传了强制执行申请表
                    //            {
                    //                UIHelp.layerAlert(Page, string.Format("不申请强制执行不要上传“强制执行申请表”和“解除劳动合同证明”扫描件，请删除再提交！", sb), 5, 0);
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}

                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1);
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！", sb), 5, 0);
                        return;
                    }

                    #endregion 必须上传附件集合

                    if (ob.PostTypeID == 1 && (RadTextBoxChangeType.SelectedItem.Text == "离京变更" || RadTextBoxChangeType.SelectedItem.Text == "注销"))
                    {
                        if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
                        {
                            if (RadDatePickerDelContractDate.SelectedDate.HasValue == false)
                            {
                                UIHelp.layerAlert(Page, "请输入解除劳动关系日期", 5, 0);
                                return;
                            }
                        }
                        if (RadTextBoxChangeType.SelectedItem.Text == "注销" && RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                        {
                            if (RadDatePickerNotWorkDate.SelectedDate.HasValue == false)
                            {
                                UIHelp.layerAlert(Page, "请输入不在该单位从事安全生产管理工作日期", 5, 0);
                                return;
                            }
                        }
                    }

                    if (ButtonExit.Text == "提交单位审核")
                    {
                        #region 提交单位审核

                        try
                        {
                            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更" || RadTextBoxChangeType.SelectedItem.Text == "注销")
                            {
                                if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
                                {
                                    certhfchange.ChangeRemark = "申请强制执行";
                                    certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                                    certhfchange.ENT_ContractStartTime = RadDatePickerDelContractDate.SelectedDate;
                                }
                                else
                                {
                                    certhfchange.ChangeRemark = null;
                                    certhfchange.ENT_ContractType = null;
                                    certhfchange.ENT_ContractStartTime = null;
                                    certhfchange.ENT_ContractENDTime = null;

                                    if (RadTextBoxChangeType.SelectedItem.Text == "注销")
                                    {
                                        if (RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                                        {
                                            certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                                            certhfchange.ChangeRemark = "不在从事安全生产管理工作";
                                            certhfchange.ENT_ContractENDTime = RadDatePickerNotWorkDate.SelectedDate;
                                        }
                                        else
                                        {
                                            certhfchange.ChangeRemark = null;
                                            certhfchange.ENT_ContractENDTime = null;
                                        }
                                    }

                                }
                            }

                            certhfchange.ApplyDate = DateTime.Now;
                            certhfchange.Status = EnumManager.CertificateChangeStatus.WaitUnitCheck;
                            CertificateChangeDAL.Update(certhfchange);
                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "证书变更申请提交单位审核失败！", ex);
                            return;
                        }

                        if (certhfchange.ChangeType == EnumManager.CertificateUpdateType.OutBeiJing)
                        {
                            //拷贝电子证书与离京证明一起下载
                            try
                            {
                                CertificateOB certificateob = ViewState["certificateob"] as CertificateOB;
                                string CAID = certificateob.CertificateCAID;
                                File.Copy(string.Format(@"{0}\{1}\{2}.pdf", MyWebConfig.CAFile, CAID.Substring(CAID.Length - 3, 3), CAID), Server.MapPath(string.Format(@"..\Upload\pdf\lijing\{0}.pdf", certificateob.CertificateID)), true);//替换文件
                            }
                            catch (Exception ex)
                            {
                                Utility.FileLog.WriteLog("复制离京变更原始电子证书到离京证明目录失败", ex);
                            }
                        }

                        UIHelp.WriteOperateLog(PersonName, UserID, "证书变更申请提交单位审核", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));

                        UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"var isfresh=true;parent.SetCwinHeight();window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("change"), Utility.Cryptography.Encrypt(certhfchange.CertificateChangeID.ToString())));
                       

                        #endregion 提交单位审核
                    }
                    else if (ButtonExit.Text == "提交建委审核")
                    {
                        #region 提交建委审核

                        try
                        {
                            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更" || RadTextBoxChangeType.SelectedItem.Text == "注销")
                            {
                                if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
                                {
                                    certhfchange.ChangeRemark = "申请强制执行";
                                    certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                                    certhfchange.ENT_ContractStartTime = RadDatePickerDelContractDate.SelectedDate;
                                }
                                else
                                {
                                    certhfchange.ChangeRemark = null;
                                    certhfchange.ENT_ContractType = null;
                                    certhfchange.ENT_ContractStartTime = null;
                                    certhfchange.ENT_ContractENDTime = null;

                                    if (RadTextBoxChangeType.SelectedItem.Text == "注销")
                                    {
                                        if (RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                                        {
                                            certhfchange.ENT_ContractType = Convert.ToInt32(RadioButtonList_enforceType.SelectedValue);
                                            certhfchange.ChangeRemark = "不在从事安全生产管理工作";
                                            certhfchange.ENT_ContractENDTime = RadDatePickerNotWorkDate.SelectedDate;
                                        }
                                        else
                                        {
                                            certhfchange.ChangeRemark = null;
                                            certhfchange.ENT_ContractENDTime = null;
                                        }
                                    }

                                }
                            }

                            certhfchange.ApplyDate = DateTime.Now;
                            certhfchange.Status = EnumManager.CertificateChangeStatus.Applyed;
                            CertificateChangeDAL.Update(certhfchange);
                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "证书变更申请提交建委审核失败！", ex);
                            return;
                        }

                        UIHelp.WriteOperateLog(PersonName, UserID, "证书变更申请提交建委审核", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));
                        //UIHelp.layerAlert(Page, "提交建委审核成功。", "parent.SetCwinHeight();");
                        UIHelp.layerAlert(Page, "提交建委审核成功。", string.Format(@"var isfresh=true;parent.SetCwinHeight();window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("change"), Utility.Cryptography.Encrypt(certhfchange.CertificateChangeID.ToString())));

                        #endregion 提交建委审核
                    }
                }

                ViewState["certhfchange"] = certhfchange;
                SetButtonEnable(certhfchange);
                SetUploadFileType();
                BindCheckHistory(certhfchange.CertificateChangeID.Value);
                BindFile(ApplyID);

                ViewState["继续提交"] = null;
                #endregion  提交
            }
            else
            {
                #region  弹出提示，8秒阅读
                string TipHtml = null;

                if (ob.PostID == 6//土建类专职安全生产管理人员
                    || ob.PostID == 1123//机械类专职安全生产管理人员
                    || ob.PostID == 1125)//综合类专职安全生产管理人员
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构或劳务派遣单位缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }
                else
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }


                p_ExamConvfirmDesc.InnerHtml = TipHtml;
                DivExamConfirm.Style.Add("display", "block");
                Telerik.Web.UI.RadScriptManager.RegisterStartupScript(Page, this.GetType(), "show15"
                    , string.Format(@"function show15() {{
            var myVar = setInterval(function () {{
                var num = $('#spanCount').text();
                num--;
                $('#spanCount').text(num);
                if (num == 0) {{
                    $('#spanCount').text('');
                    clearInterval(myVar);
                    $('#{0}').removeClass('btn_no');
                    $('#{0}').removeAttr('disabled');
                    $('#{1}').removeClass('btn_no');
                    $('#{1}').removeAttr('disabled');
                }}
            }}, 1000);
        }}
        show15();window.setTimeout(function(){{$('#{2}').focus();}},500);", ButtonYes.ClientID, ButtonNo.ClientID, ButtonExit.ClientID)
                    , true);

                #endregion  弹出提示，8秒阅读
            }

        }

        //继续提交
        protected void ButtonYes_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
            ViewState["继续提交"] = true;
            if (TableUnitCheck.Visible == true)
            {
                ButtonUnitCheck_Click(sender, e);
            }
            else
            {
                ButtonExit_Click(sender, e);
            }
        }

        //取消提交
        protected void ButtonNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
        }

        //单位审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            CertificateOB ob = (CertificateOB)ViewState["certificateob"];

            #region A、B、C证同单位校验

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" && ob.PostTypeID == 1 && certhfchange.ChangeType == "京内变更" && certhfchange.UnitCode != certhfchange.NewUnitCode)
            {
                string sql = @"
                if exists(select 1 from [dbo].[CERTIFICATE] 
	                        where [WorkerCertificateCode]='{0}' and POSTID = 147 and ValidEndDate >=dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销'
	                        group by [WorkerCertificateCode] having count(*) >1)

                    select c.CertificateCode,case when a.NewUNITCODE is not null then '变更目标单位：' + a.NEWUNITNAME else '持证所属单位：' +c.UNITNAME end as UNITNAME
                    from [dbo].[CERTIFICATE] c
                    left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                    where c.[WorkerCertificateCode]='{0}' 
                        and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                        and exists(select 1	from [dbo].[CERTIFICATE] c
	                                left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
	                                where c.[WorkerCertificateCode]='{0}' 
	                                    and c.posttypeid=1 and c.POSTID <> 147 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
	                                    and  not exists(select 1 
			                                    from [dbo].[CERTIFICATE] c2
			                                    left join [dbo].[CERTIFICATECHANGE] a2 on c2.CERTIFICATEID = a2.CERTIFICATEID and a2.ConfrimDate is null and c2.UNITCODE <> a2.NewUnitCode and a2.CHANGETYPE='京内变更'
			                                    where c2.[WorkerCertificateCode]='{0}' 
			                                    and  c2.POSTID = 147 and c2.ValidEndDate >=dateadd(day,-1,getdate()) and c2.[Status] <> '离京变更' and c2.[Status] <> '注销'
			                                    and case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end =case when a2.NewUNITCODE is not null then a2.NewUNITCODE else c2.UNITCODE end 
	                                    )
                                ) order by c.CertificateCode
                 else
                    select c.CertificateCode,case when a.NewUNITCODE is not null then '变更目标单位：' + a.NEWUNITNAME else '持证所属单位：' +c.UNITNAME end as UNITNAME
                    from [dbo].[CERTIFICATE] c
                    left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                    where c.[WorkerCertificateCode]='{0}' 
                        and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                        and exists(
                                select 1 from [dbo].[CERTIFICATE] c
                                left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                                where c.[WorkerCertificateCode]='{0}' 
                                and case when a.NewUNITCODE is not null then a.NewUNITCODE else c.UNITCODE end <> '{1}'
                                and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                             ) order by c.CertificateCode";

                DataTable tb = CommonDAL.GetDataTable(string.Format(sql, ob.WorkerCertificateCode,  certhfchange.NewUnitCode));
                if (tb != null && tb.Rows.Count > 0)
                {
                    foreach (DataRow r in tb.Rows)
                    {
                        sb.AppendFormat("<br />{0}，{1}。", r["CertificateCode"], r["UnitName"]);
                    }
                    sb.Insert(0, "校验未通过。校验规则：持证人同时持有有效的A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业。请个人按规则提交证书变更后再统一审核。");

                    UIHelp.layerAlert(Page, string.Format("{0}", sb), 5, 0);
                    return;
                }
            }

            #endregion

            if (RadTextBoxChangeType.SelectedItem.Value != "京内变更"
              || (ob.PostID == 147 && IfFaRen == true)//法人进京A
              || certhfchange.SheBaoCheck.HasValue == false//申报尚未校验
              || certhfchange.SheBaoCheck == 1//社保校验合格
              || (ViewState["继续提交"] != null && Convert.ToBoolean(ViewState["继续提交"]) == true)//已读8表提示并继续提交
              || RadioButtonListOldUnitCheckResult.SelectedValue == "不同意"
              )
            {
                certhfchange.ModifyTime = DateTime.Now;

                if (ZZJGDM == certhfchange.UnitCode)//原单位
                {
                    certhfchange.OldUnitCheckTime = certhfchange.ModifyTime;
                    certhfchange.OldUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
                }
                else//现单位
                {
                    certhfchange.NewUnitCheckTime = certhfchange.ModifyTime;
                    certhfchange.NewUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
                }

                certhfchange.Status = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.CertificateChangeStatus.Applyed : EnumManager.CertificateChangeStatus.SendBack);

                try
                {
                    CertificateChangeDAL.Update(certhfchange);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, string.Format("单位审核证书{0}申请失败！", certhfchange.ChangeType), ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, string.Format("单位审核证书{0}申请", certhfchange.ChangeType), string.Format("证书编号：{0}，岗位工种：{1}。", RadTextBoxCertificateCode.Text, RadTextBoxPostID.Text));

                UIHelp.layerAlert(this.Page, "审核成功！", "hideIfam(true);");
                ViewState["继续提交"] = null;
            }
            else
            {
                #region  弹出提示，8秒阅读
                string TipHtml = null;

                if (ob.PostID == 6//土建类专职安全生产管理人员
                    || ob.PostID == 1123//机械类专职安全生产管理人员
                    || ob.PostID == 1125)//综合类专职安全生产管理人员
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构或劳务派遣单位缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }
                else
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }


                p_ExamConvfirmDesc.InnerHtml = TipHtml;
                DivExamConfirm.Style.Add("display", "block");
                Telerik.Web.UI.RadScriptManager.RegisterStartupScript(Page, this.GetType(), "show15"
                    , string.Format(@"function show15() {{
            var myVar = setInterval(function () {{
                var num = $('#spanCount').text();
                num--;
                $('#spanCount').text(num);
                if (num == 0) {{
                    $('#spanCount').text('');
                    clearInterval(myVar);
                    $('#{0}').removeClass('btn_no');
                    $('#{0}').removeAttr('disabled');
                    $('#{1}').removeClass('btn_no');
                    $('#{1}').removeAttr('disabled');
                }}
            }}, 1000);
        }}
        show15();window.setTimeout(function(){{$('#{2}').focus();}},500);", ButtonYes.ClientID, ButtonNo.ClientID, ButtonUnitCheck.ClientID)
                    , true);

                #endregion  弹出提示，8秒阅读
            }
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateChangeOB o = ViewState["certhfchange"] as CertificateChangeOB;
                CertificateChangeDAL.Delete(o.CertificateChangeID.Value);
                ViewState["certhfchange"] = null;
            

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除证书变更申请失败！", ex);
                return;
            }
            SetButtonEnable(null);
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(PersonName, UserID, "删除证书变更申请", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));
            UIHelp.layerAlert(Page, "删除成功！","isfresh=true;");
            //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        ////打印
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    CertificateChangeOB o = ViewState["certhfchange"] as CertificateChangeOB;
        //    string template = (o.ChangeType == "京内变更" ? "~/Template/京内变更申请表.doc" : "~/Template/证书变更申请表.doc");
        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, template
        //        , string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())
        //        , GetPrintData());
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc');", PersonID.ToString(), RootUrl), true);
        //}

        //导出打印
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            CertificateChangeOB o = ViewState["certhfchange"] as CertificateChangeOB;
            CertificateOB _certificateob = (CertificateOB)ViewState["certificateob"];

            string template = "~/Template/变更申请表-京内.docx";
            switch (o.ChangeType)
            {
                case "注销":
                    if (_certificateob.PostTypeID.Value > 1)
                    {
                        template = "~/Template/变更申请表-特种作业注销.docx";
                    }
                    else
                    {
                        if (o.ChangeRemark == "申请强制执行")
                        {
                            template = "~/Template/变更申请表-强制注销.docx";
                        }
                        else
                        {
                            if (o.ChangeRemark == "不在从事安全生产管理工作")
                            {
                                template = "~/Template/变更申请表-注销不从事安全工作.docx";
                            }
                            else
                            {
                                template = "~/Template/变更申请表-注销.docx";
                            }
                        }
                    }
                    break;
                case "离京变更":
                    if (o.ChangeRemark == "申请强制执行")
                    {
                        template = "~/Template/变更申请表-强制离京.docx";
                    }
                    else
                    {
                        template = "~/Template/变更申请表-离京.docx";
                    }
                    break;
                default:
                    if (_certificateob.PostTypeID.Value == 1)
                    {
                        template = "~/Template/变更申请表-三类人京内.docx";
                    }
                    else
                    {
                        template = "~/Template/变更申请表-特种作业京内.docx";
                    }
                    break;
            }
            CheckSaveDirectory();
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, template
            //    , string.Format("~/UpLoad/CertifChangeApply/ChangeApply_{0}.doc", PersonID.ToString())
            //    , GetPrintData());

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl(string.Format("{0}申请表", o.ChangeType), string.Format("~/UpLoad/CertifChangeApply/ChangeApply_{0}.doc", PersonID.ToString())));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

            var ht = PrintDocument.GetProperties(o);
            ht["Data_ApplyCode"] = o.ApplyCode;//申请批次号
            ht["Data_ApplyDate"] = o.ApplyDate.Value.ToString("yyyy-MM-dd");//申请日期
            ht["Data_ChangeType"] = o.ChangeType;//变更类型		
            ht["Data_PostID"] = RadTextBoxPostID.Text;//岗位工种
            ht["Data_WorkerName"] = o.WorkerName;//姓 名
            ht["Data_NewWorkerName"] = o.NewWorkerName;//姓 名            		
            ht["Data_Sex"] = o.Sex;//性 别
            ht["Data_NewSex"] = o.NewSex;//性 别	
            ht["Data_WorkerCertificateCode"] = o.WorkerCertificateCode;//身份证号
            ht["Data_NewWorkerCertificateCode"] = o.NewWorkerCertificateCode;//身份证号            		
            ht["Data_Birthday"] = o.Birthday.Value.ToString("yyyy-MM-dd"); ;//出生日期
            ht["Data_NewBirthday"] = o.NewBirthday.Value.ToString("yyyy-MM-dd"); ;//出生日期		
            ht["Data_LinkWay"] = o.LinkWay;//联系电话	
            ht["Data_UnitName"] = o.UnitName;//单位名称
            ht["Data_NewUnitName"] = o.NewUnitName;//单位名称	
            ht["Data_UnitCode"] = o.UnitCode;//组织机构代码
            ht["Data_NewUnitCode"] = o.NewUnitCode;//组织机构代码
            ht["Data_CertificateCode"] = RadTextBoxCertificateCode.Text;//证书编号		
            ht["ConferDate"] = o.ConferDate.Value.ToString("yyyy-MM-dd"); //发证日期          
            if (_certificateob.PostTypeID > 2)
            {
                ht["ValidDate"] = "长期有效"; //有效期
            }
            else
            {
                ht["ValidDate"] = o.ValidEndDate.Value.ToString("yyyy-MM-dd"); //有效期
            }
            ht["ChangeRemark"] = "";// o.ChangeRemark;//备注（离京变更在备注中显示：拟调入省份）
            ht["Job"] = o.Job;
            ht["SkillLevel"] = o.SkillLevel;
            if (o.ENT_ContractType.HasValue == true)
            {
                ht["ENT_ContractStartTime"] = o.ENT_ContractStartTime == null ? "      " : o.ENT_ContractStartTime.Value.ToString("yyyy年MM月dd日");
                ht["ENT_ContractENDTime"] = o.ENT_ContractENDTime == null ? "      " : o.ENT_ContractENDTime.Value.ToString("yyyy年MM月dd日");
                switch (o.ENT_ContractType)
                {
                    case 1:
                        ht["ENT_ContractType1"] = "☑";
                        ht["ENT_ContractType2"] = "□";
                        ht["ENT_ContractType3"] = "□";
                        break;
                    case 2:
                        ht["ENT_ContractType1"] = "□";
                        ht["ENT_ContractType2"] = "☑";
                        ht["ENT_ContractType3"] = "□";
                        break;
                    case 3:
                        ht["ENT_ContractType1"] = "□";
                        ht["ENT_ContractType2"] = "□";
                        ht["ENT_ContractType3"] = "☑";
                        break;
                    case 4:                        
                        ht["ENT_ContractType4"] = "☑";
                        ht["ENT_ContractType5"] = "□";
                        break;
                    case 5:
                        ht["ENT_ContractType4"] = "□";
                        ht["ENT_ContractType5"] = "☑";
                        break;
                    default:
                        ht["ENT_ContractType1"] = "☑";
                        ht["ENT_ContractType2"] = "□";
                        ht["ENT_ContractType3"] = "□";
                        break;
                }
            }
            else
            {
                ht["ENT_ContractType1"] = "□";
                ht["ENT_ContractType2"] = "□";
                ht["ENT_ContractType3"] = "□";
                ht["ENT_ContractStartTime"] = "      ";
                ht["ENT_ContractENDTime"] = "      ";
            }

            if (string.IsNullOrEmpty(o.WorkerCertificateCode) == true)
            {
                ht["photo"] = UIHelp.GetFaceImagePath(_certificateob.FacePhoto, o.NewWorkerCertificateCode);//绑定照片
            }
            else
            {
                ht["photo"] = UIHelp.GetFaceImagePath(_certificateob.FacePhoto, o.WorkerCertificateCode);//绑定照片
            }

            //更换照片
            if (o.IfUpdatePhoto.HasValue && o.IfUpdatePhoto.Value == 1)
            {

                ht["photo_new"] = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", RadTextBoxCertificateCode.Text.Substring(RadTextBoxCertificateCode.Text.Length - 3, 3), o.CertificateChangeID);//绑定照片
            }
            //else
            //{
            //    ht["photo_new"] =  "../Images/null.gif";//绑定照片
            //}

            PrintDocument.CreateDataToWordByHashtable(Server.MapPath(template), string.Format("变更申请表_{0}", PersonID), ht);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            Dictionary<string, string> printData = new Dictionary<string, string>();

            printData.Add("Data_ApplyCode", certhfchange.ApplyCode);//申请批次号
            printData.Add("Data_ChangeType", RadTextBoxChangeType.SelectedValue);//变更类型
            printData.Add("Data_ApplyDate", Convert.ToDateTime(certhfchange.ApplyDate.ToString()).ToString("yyyy-MM-dd"));//申请日期RadDatePickerApplyDate.SelectedDate.ToString()
            printData.Add("Data_PostID", RadTextBoxPostID.Text);  //证书类别
            printData.Add("Data_CertificateCode", RadTextBoxCertificateCode.Text);  //证书编号
            printData.Add("Data_WorkerName", lblWorkerName.Text);  //原姓名
            printData.Add("Data_NewWorkerName", certhfchange.NewWorkerName);//姓名RadTextBoxWorkerName.Text.

            printData.Add("Data_Sex", lblSex.Text.ToString());//性别rdbtnlistSex.SelectedValue
            printData.Add("Data_NewSex", certhfchange.NewSex);

            printData.Add("Data_Birthday", Convert.ToDateTime(lblBirthday.Text.ToString()).ToString("yyyy-MM-dd"));  //出身日期RadDatePickerNewBirthday.SelectedDate.
            printData.Add("Data_NewBirthday", Convert.ToDateTime(certhfchange.NewBirthday.ToString()).ToString("yyyy-MM-dd"));

            printData.Add("Data_WorkerCertificateCode", lblWorkerCertificateCode.Text);//原证件号码
            printData.Add("Data_NewWorkerCertificateCode", certhfchange.NewWorkerCertificateCode); //证件号码RadTextBoxWorkerCertificateCode.Text.

            printData.Add("Data_UnitName", certhfchange.UnitName);//原工作单位RadTextBoxUnitName.Text
            printData.Add("Data_NewUnitName", certhfchange.NewUnitName);//现单位名称RadTextBoxNewUnitName.Text

            printData.Add("Data_UnitCode", certhfchange.UnitCode); //原机构代码RadTextBoxUnitCode.Text
            printData.Add("Data_NewUnitCode", certhfchange.NewUnitCode);//现单位机构代码RadTextBoxNewUnitCode.Text

            printData.Add("Data_OldUnitAdvise", "");//原单位意见RadTextBoxOldUnitAdvise.Text
            printData.Add("Data_NewUnitAdvise", "");//现单位意见RadTextBoxNewUnitAdvise.Text

            printData.Add("Data_OldConferUnitAdvise", "");//原发证机关意见RadTextBoxOldConferUnitAdvise.Text
            printData.Add("Data_NewConferUnitAdvise", ""); //现发证机关意见RadTextBoxNewConferUnitAdvise.Text

            printData.Add("Data_LinkWay", certhfchange.LinkWay); //联系电话RadTextBoxLinkWay.Text

            CertificateOB certificateob = ViewState["certificateob"] as CertificateOB;
            printData.Add("ConferDate", certificateob.ConferDate.Value.ToString("yyyy-MM-dd")); //发证日期
            printData.Add("ValidDate", certificateob.ValidEndDate.Value.ToString("yyyy-MM-dd")); //有效期

            //xml换行
            string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

            printData.Add("Desc", string.Format("申请人：{0}{1}初审单位：{2}", certhfchange.ApplyMan, xmlBr, "市住建委行政服务大厅"));//备注

            printData.Add("FacePhoto", certhfchange.NewWorkerCertificateCode);//照片标签

            if (string.IsNullOrEmpty(certhfchange.WorkerCertificateCode) == true)
            {
                printData.Add("Img_FacePhoto", UIHelp.GetFaceImagePath(certificateob.FacePhoto, certhfchange.NewWorkerCertificateCode));//绑定照片
            }
            else
            {
                printData.Add("Img_FacePhoto", UIHelp.GetFaceImagePath(certificateob.FacePhoto, certhfchange.WorkerCertificateCode));//绑定照片
            }

            //更换照片
            printData.Add("FacePhotoUpdate", certhfchange.NewWorkerCertificateCode + "U");//照片标签
            if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
            {
                printData.Add("Img_FacePhotoUpdate", string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID));//绑定照片
            }
            else
            {
                printData.Add("Img_FacePhotoUpdate", "../Images/null.gif");//绑定照片
            }

            string TCodePath = string.Format(@"../Upload/CertifChangeApply/{0}.png", certhfchange.ApplyCode);
            if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
            {
                UIHelp.CreateITFCode("~/Upload/CertifChangeApply/", certhfchange.ApplyCode);
            }
            //条码
            printData.Add("ImageTCodeName", certhfchange.CertificateID.ToString());
            printData.Add("Img_TCode", TCodePath);

            printData.Add("ChangeRemark", certhfchange.ChangeRemark);//备注（离京变更在备注中显示：拟调入省份）


            list.Add(printData);

            return list;
        }

        //显示社保比对结果
        private void ShowSheBao(CertificateOB _CertificateOB, CertificateChangeOB _CertificateChangeOB)
        {
            if (_CertificateOB.PostTypeID==2)
            {
                return;//特种作业变更取消单位、不需要验证社保
            }
            if (_CertificateChangeOB.Status==EnumManager.CertificateChangeStatus.Noticed)
            {
                return;
            }

            if (
                (_CertificateChangeOB.ApplyDate.Value.CompareTo(DateTime.Parse("2014-07-01")) >= 0)
                 && (_CertificateChangeOB.ChangeType == "京内变更" || _CertificateChangeOB.ChangeType == "补办")
                )
            {
                //检查社保相关扫描件上传文件是否存在
                bool SheBaoCheckFile = ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.社保扫描件);
                string tip = ""; 

                if (SheBaoCheckFile == false //没上传社保证明
                    && (_CertificateChangeOB.SheBaoCheck.HasValue == false || _CertificateChangeOB.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                    )
                {
                    tip = "<p style='color:red'>重要提示：您的申请单“社保自动核验”、“上传社保扫描件”两项都不符合要求。<br />请在保存申请单次日查看社保自动验核结果，比对合格再上报单位审核，不符合必须上传其他相关证明材料。</p>";
                }

                divSheBao.InnerHtml = string.Format("{4}<b>社保校验：</b><span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>"
                    , _CertificateChangeOB.NewWorkerCertificateCode, _CertificateChangeOB.NewUnitCode, _CertificateChangeOB.ApplyDate.ToString()
                     , _CertificateChangeOB.SheBaoCheck.HasValue == false ? "尚未比对（夜间比对）" : _CertificateChangeOB.SheBaoCheck.Value == 1 ? "符合" : "不符合"
                     , tip);
            }
        }

        /// <summary>
        /// 设置需要上传的附件
        /// </summary>
        protected void SetUploadFileType()
        {
            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更" )
            {
                p_lijing.Visible = true;
                div_SheBao.Visible = false;//社保
                //divSelectEnforceType.Style.Remove("display");
                //div_enforce.Style.Remove("display");
                //div_jcldht.Style.Remove("display");
            }
            else if (RadTextBoxChangeType.SelectedItem.Text == "注销")
            {
               
                div_SheBao.Visible = false;//社保

                if (myCertificateOB.PostTypeID == 1)
                {
                    p_zhuxiao.Visible = true;
                    //divSelectEnforceType.Style.Remove("display");
                    //div_enforce.Style.Remove("display");
                }
                else
                {
                    p_zhuxiao_tzzy.Visible = true;
                }
            }
            else
            {

                CertificateOB ob = (CertificateOB)ViewState["certificateob"];

                int PostID = ob.PostID.Value;
                int PostTypeID = ob.PostTypeID.Value;

                if (trFuJan.Visible == false)
                {
                    if (PostTypeID < 3)
                    {
                        ShowZACheckResult();
                    }
                    if (PostID == 147)
                    {
                        if (IfFaRen == false)//非法人
                        {
                            divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                        }
                        else//法人
                        {
                            divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                        }
                    }
                    return;
                }

                switch (PostTypeID)
                {
                    case 1://三类人
                        switch (PostID)
                        {
                            case 6://土建安全员                              
                            case 1123://机械安全员 
                            case 1125://综合安全员 
                                p_PostTyppe1_aqy.Visible = true;
                                if (SheBaoCheckResult == true)
                                {
                                    div_SheBao.Style.Add("display", "none");
                                }
                                else
                                {
                                    div_SheBao.Style.Remove("display");
                                }
                                break;
                            case 147://企业主要负责人 
                                p_PostTyppe1_qyfzr.Visible = true;
                                if (IfFaRen == false)//非法人
                                {
                                    divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                                }
                                else//法人
                                {
                                    divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                                }

                                string job = RadComboBoxJob.SelectedItem.Text;
                                if (job == "请选择")
                                {
                                    //div_SheBao.Style.Remove("display");
                                    div_SheBao.Visible = true;
                                    div_YingYeZhiZhao.Visible = true;
                                    p_FaRen.Visible = true;
                                    p_NoFaRen.Visible = true;
                                }
                                else if (job == "法定代表人")
                                {
                                    //div_SheBao.Style.Add("display", "none");//不用上传社保
                                    //div_YingYeZhiZhao.Style.Remove("display");//显示传营业执照

                                    div_SheBao.Visible = false;//不用上传社保
                                    div_YingYeZhiZhao.Visible = true;//显示传营业执照
                                    p_FaRen.Visible = true;
                                    p_NoFaRen.Visible = false;
                                }
                                else//职务选择非法人
                                {
                                    div_YingYeZhiZhao.Visible = false;

                                    if (SheBaoCheckResult == true)//社保
                                    {
                                        //div_SheBao.Style.Add("display", "none");
                                        div_SheBao.Visible = false;//不用上传社保
                                    }
                                    else
                                    {
                                        //div_SheBao.Style.Remove("display");
                                        div_SheBao.Visible = true;
                                    }

                                    p_FaRen.Visible = false;
                                    p_NoFaRen.Visible = true;
                                }
                                break;
                            case 148://项目负责人 
                                p_PostTyppe1_xmfzr.Visible = true;
                                if (SheBaoCheckResult == true)//社保
                                {
                                    //div_SheBao.Style.Add("display", "none");
                                    div_SheBao.Visible = false;//不用上传社保
                                }
                                else
                                {
                                    //div_SheBao.Style.Remove("display");
                                    div_SheBao.Visible = true;
                                }
                                break;
                        }
                        ShowZACheckResult();
                        break;
                    case 2://特种作业
                        p_PostTyppe2.Visible = true;
                        div_SheBao.Visible = false;
                        ShowZACheckResult();
                        break;
                    default://职业技能（工人）              
                        p_PostTyppe4.Visible = true;
                         div_SheBao.Visible = false;//社保
                        break;
                }
            }
        }

        /// <summary>
        /// 显示质量安全网持证规则校验结果
        /// </summary>
        protected void ShowZACheckResult()
        {
            CertificateOB ob = (CertificateOB)ViewState["certificateob"];

            if (ob.PostTypeID != 1 && ob.PostTypeID != 2)//三类人和特种作业才做质安网持证检查
            {
                return;
            }
            if (ViewState["certhfchange"] != null)
            {
                CertificateChangeOB _CertifChange = (CertificateChangeOB)ViewState["certhfchange"];

                if (_CertifChange.ZACheckTime.HasValue == false)
                {
                    divZACheckResult.InnerHtml = "<b>持证校验：</b>尚未比对";
                }
                else if (_CertifChange.ZACheckResult == 0)
                {
                    divZACheckResult.InnerHtml = string.Format(@"<div><b>持证校验：</b><span style='color:red'>未通过。</span></div>
                <div style='padding-left: 32px'>警告：证书没有通过<a href='https://zlaq.mohurd.gov.cn/fwmh/bjxcjgl/fwmh/pages/default/index.html' target='_blank'>【全国工程质量安全监管信息平台（可按身份证查询ABC持证情况）】</a>数据校验，属于违规持证。<br/>
                                                                    请按照下列持证规则变更或注销相关证书，否则后续将无法下载该电子证书。（若外省证书已经注销，请联系原证书省份，查询数据是否已经同步到全国工程质量安全监管信息平台。）<br/><br/>
                                                                    <b>校验结果说明：</b><span style='color:red'>{0}</span><br/><br/>
                                                                    <b>持证规则说明：</b><br/>
                                                                    <div style='padding-left: 32px'>
                                                                        <b>A证持证要求：</b><br/> 
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人有多本A证时，多本A证在不同企业下，最多存在一本非法人证A证；<br/>
                                                                            > 持证人同时持有A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业；<br/>
                                                                            > 一个企业只能存在一本法人A证。（法人变更需要变更证书上职务）<br/><br/>
                                                                        </div>
                                                                        <b>B证持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人在全国范围只允许持有一本B证；<br/>
                                                                            > 持有已注册的建造师且建造师注册单位与B本单位一致；<a href='https://jzsc.mohurd.gov.cn/data/person' target='_blank'>【全国建筑市场监管公共服务平台（可按身份证查询建造师持证情况）】</a><br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C1/C2持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证），在全国只能取得一本C1或C2证，能同时取得C1和C2证，C1和C2证必须在同一企业下，在取得C1或C2证书时不能再获取C3证；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C3持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证），在全国只能取得一本C3证，在取得C3证书时不能再取得C1或C2证；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下；<br/>
                                                                            > 持证人不能同时持有多本C1或C2或C3。<br/>
                                                                        </div>
                                                                    </div>
                                                                  </div>", _CertifChange.ZACheckRemark);
                }
                else
                {
                    divZACheckResult.InnerHtml = "<b>持证校验：</b>通过";
                }
            }
            else
            {
                divZACheckResult.InnerHtml = "<b>持证校验：</b>尚未比对";
            }
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long certificateChangeId)
        {
            DataTable dt = ((PersonType == 2 || PersonType == 3)?
                CertificateChangeDAL.GetCheckHistoryListForGRQY(certificateChangeId)
                : CertificateChangeDAL.GetCheckHistoryList(certificateChangeId));
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();

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

                DataTable dt_ApplyFile = (ViewState["dt_ApplyFile"] as DataTable).Clone();

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

        //删除附件
        protected void RadGridFile_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //获取类型Id

            string FileID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileID"].ToString();
            string ApplyID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();
            try
            {
                ApplyFileDAL.Delete(FileID, ApplyID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除变更申请表附件成功", string.Format("证书编号：{0}，文件名称：{1}。", RadTextBoxCertificateCode.Text, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileName"]));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            string bgslp = "";//变更受理编批号
            string bgsh = ""; //变更审核编批号

            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                certhfchange.ModifyTime = DateTime.Now;

                if (RadioButtonListJWCheck.SelectedValue == "通过")
                {
                    certhfchange.Status = EnumManager.CertificateChangeStatus.Checked;

                    bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
                    bgsh = UIHelp.GetNextBatchNumber(dtr, "BGSH"); //变更审核编批号

                    certhfchange.GetDate = DateTime.Now;   //变更受理时间
                    certhfchange.GetResult = "通过";     //变更受理结论
                    certhfchange.GetMan = PersonName;    //变更受理人
                    certhfchange.GetCode = bgslp;//变更受理编批号
                    certhfchange.CheckDate = DateTime.Now;  //变更审核时间
                    certhfchange.CheckResult = "通过";  //变更审核结论
                    certhfchange.CheckMan = PersonName; //变更审核人
                    certhfchange.CheckCode = bgsh;    //变更审核批号

                    //修该变更记录
                    CertificateChangeDAL.Update(dtr, certhfchange);
                    
                    ViewState["certhfchange"] = certhfchange;
                 
                    ButtonCheck.Enabled = false;
                }
                else //不通过
                {
                    certhfchange.Status = EnumManager.CertificateChangeStatus.SendBack;
                    bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
                    certhfchange.GetDate = DateTime.Now;   //变更受理时间
                    certhfchange.GetResult = TextBoxCheckResult.Text.Trim();     //变更受理结论
                    certhfchange.GetMan = PersonName;    //变更受理人
                    certhfchange.GetCode = bgslp;//变更受理编批号

                    //修该变更记录
                    CertificateChangeDAL.Update(dtr, certhfchange);
                    ViewState["certhfchange"] = certhfchange;
                }

                dtr.Commit();
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, string.Format("建委审核证书{0}申请失败！", certhfchange.ChangeType), ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, string.Format("建委审核证书{0}申请", certhfchange.ChangeType), string.Format("证书编号：{0}，岗位工种：{1}。审核意见：{2}", RadTextBoxCertificateCode.Text, RadTextBoxPostID.Text, TextBoxCheckResult.Text.Trim()));

            UIHelp.layerAlert(this.Page, "审核成功！", "hideIfam(true);");
            //ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }

        //建委决定
        protected void ButtonConfrim_Click(object sender, EventArgs e)
        {
            string bgjd = ""; //变更决定编批号
            string bggz = ""; //变更告知编批号
            string _path = "";//照片目标地址
            string _sourcePath = "";//照片原地址

            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                certhfchange.ModifyTime = DateTime.Now;

                if (RadioButtonListConfrim.SelectedValue == "通过")
                {
                    //修改原表数据
                    CertificateOB certificateob = CertificateDAL.GetObject(certhfchange.CertificateID.Value);

                    certhfchange.Status = EnumManager.CertificateChangeStatus.Noticed;

                    bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
                    bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号

                    certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
                    certhfchange.ConfrimResult = "通过";  //变更决定结论
                    certhfchange.ConfrimMan = PersonName;   //变更决定人
                    certhfchange.ConfrimCode = bgjd;   //变更决定批号
                    certhfchange.NoticeDate = DateTime.Now; //变更告知时间 
                    certhfchange.NoticeResult = "通过";  //变更告知结论
                    certhfchange.NoticeMan = PersonName;    //变更告知人
                    certhfchange.NoticeCode = bggz;   //变更告知批号
                    certhfchange.DealWay = "证书信息修改";     //证书处理方式 

                    //修该变更记录
                    CertificateChangeDAL.Update(dtr, certhfchange);


                    #region 更换证书照片

                    //更换证书照片
                    if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
                    {
                        //_path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));
                        //if (!Directory.Exists(Server.MapPath(_path)))
                        //{
                        //    System.IO.Directory.CreateDirectory(Server.MapPath(_path));
                        //}
                        //_path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
                        //_sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
                        //File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
                        //certificateob.FacePhoto = _path.Replace("..", "~");

                        _sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
                        certificateob.FacePhoto = _sourcePath.Replace("..", "~");

                    }
                    //补充老照片
                    if (string.IsNullOrEmpty(certificateob.FacePhoto) == true)
                    {
                        if (string.IsNullOrEmpty(certhfchange.WorkerCertificateCode) == false && certhfchange.WorkerCertificateCode.Length > 2)
                        {
                            _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.WorkerCertificateCode.Substring(certhfchange.WorkerCertificateCode.Length - 3, 3), certhfchange.WorkerCertificateCode);
                        }
                        else
                        {
                            _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.NewWorkerCertificateCode.Substring(certhfchange.NewWorkerCertificateCode.Length - 3, 3), certhfchange.NewWorkerCertificateCode);
                        }

                        _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));

                        if (!Directory.Exists(Server.MapPath(_path)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(_path));
                        }
                        _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
                        if (File.Exists(Server.MapPath(_sourcePath)) == true)//立即同步照片
                        {

                            File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
                            certificateob.FacePhoto = _path.Replace("..", "~");
                        }
                        else//夜间同步照片
                        {
                            certificateob.FacePhoto = null;
                        }
                    }

                    #endregion 更换证书照片

                    certificateob.WorkerCertificateCode = certhfchange.NewWorkerCertificateCode;   //证件号码
                    certificateob.Birthday = certhfchange.NewBirthday;//出生日期
                    certificateob.Sex = certhfchange.NewSex;//性别
                    certificateob.WorkerName = certhfchange.NewWorkerName;    //姓名
                    certificateob.UnitName = certhfchange.NewUnitName;   //工作单位
                    certificateob.UnitCode = certhfchange.NewUnitCode;   //组织机构代码
                    certificateob.ModifyPersonID = PersonID;  //最后修改人
                    certificateob.ModifyTime = DateTime.Now;   //最后修改时间
                    certificateob.ValidStartDate = certhfchange.CheckDate.Value.Date;
                    certificateob.CheckDate = certhfchange.CheckDate;    //审批时间
                    certificateob.CheckMan = certhfchange.CheckMan;      //审批人
                    certificateob.CheckAdvise = certhfchange.CheckResult;//审批意见
                    certificateob.Status = certhfchange.ChangeType;      //证书更新状态（变更类型）
                    //certificateob.CaseStatus = "已归档";//归档状态    
                    certificateob.PrintCount = 1;
                    certificateob.ApplyMan = certhfchange.ApplyMan;//申请人
                    if (string.IsNullOrEmpty(certhfchange.Job) == false)
                    {
                        certificateob.Job = certhfchange.Job;
                    }
                    if (string.IsNullOrEmpty(certhfchange.SkillLevel) == false)
                    {
                        certificateob.SkillLevel = certhfchange.SkillLevel;
                    }


                    //根据证书id向历史表插入历史数据
                    CertificateHistoryDAL.InsertChangeHistory(dtr, certhfchange.CertificateID.Value);

                    //更新证书
                    CertificateDAL.Update(dtr, certificateob);

                    //更新人员基本信息
                    WorkerOB _WorkerOB = WorkerDAL.GetUserObject(certificateob.WorkerCertificateCode);
                    if (_WorkerOB != null)//update
                    {
                        if (_WorkerOB.Birthday != certificateob.Birthday
                            || _WorkerOB.Sex != certificateob.Sex
                            || _WorkerOB.WorkerName != certificateob.WorkerName
                            )
                        {
                            _WorkerOB.Birthday = certificateob.Birthday;
                            _WorkerOB.Sex = certificateob.Sex;
                            _WorkerOB.WorkerName = certificateob.WorkerName;
                            WorkerDAL.Update(dtr, _WorkerOB);
                        }
                    }
                    else//new
                    {
                        _WorkerOB = new WorkerOB();
                        _WorkerOB.Birthday = certificateob.Birthday;
                        _WorkerOB.Sex = certificateob.Sex;
                        _WorkerOB.WorkerName = certificateob.WorkerName;
                        _WorkerOB.Phone = certhfchange.LinkWay;
                        _WorkerOB.CertificateCode = certificateob.WorkerCertificateCode;
                        if (certificateob.WorkerCertificateCode.Length == 15 || certificateob.WorkerCertificateCode.Length == 18)
                            _WorkerOB.CertificateType = "身份证";
                        else
                            _WorkerOB.CertificateType = "其它证件";
                        WorkerDAL.Insert(dtr, _WorkerOB);
                    }

                    #region 更新证书标准附件库

                    //更新证书附件中需要被覆盖的附件为历史附件
                    CommonDAL.ExecSQL(dtr, string.Format(@"
                                                        Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                                        SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                                        from [dbo].[COC_TOW_Person_File]
                                                        inner join 
                                                        (
                    	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                    	                                    from 
                    	                                    (
                    		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].[CERTIFICATECODE] as PSN_RegisterNo
                    		                                    from [dbo].[FileInfo]
                    		                                    inner join [dbo].[ApplyFile]
                    		                                    on [FileInfo].FileID = [ApplyFile].FileID
                    		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                    		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                    	                                    ) a
                    	                                    inner join 
                    	                                    (
                    		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                    		                                    from [dbo].[FileInfo]
                    		                                    inner join [dbo].[COC_TOW_Person_File]
                    		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                                                inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                                on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
                    		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                    	                                    ) b 
                    	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                                        ) t
                                                        on [COC_TOW_Person_File].FileID = t.FileID", certhfchange.CertificateChangeID));


                    CommonDAL.ExecSQL(dtr, string.Format(@"
                                                        delete from [dbo].[COC_TOW_Person_File]
                                                        where FileID in( select [COC_TOW_Person_File].[FileID]
                                                        from [dbo].[COC_TOW_Person_File]
                                                        inner join 
                                                        (
                    	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                    	                                    from 
                    	                                    (
                    		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE as PSN_RegisterNo
                    		                                    from [dbo].[FileInfo]
                    		                                    inner join [dbo].[ApplyFile]
                    		                                    on [FileInfo].FileID = [ApplyFile].FileID
                    		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                    		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                    	                                    ) a
                    	                                    inner join 
                    	                                    (
                    		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                    		                                    from [dbo].[FileInfo]
                    		                                    inner join [dbo].[COC_TOW_Person_File]
                    		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                                                inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                                on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
                    		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                    	                                    ) b 
                    	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                                        ) t
                                                        on [COC_TOW_Person_File].FileID = t.FileID
                                                        )", certhfchange.CertificateChangeID));

                    //将申请单附件写入证书附件库
                    CommonDAL.ExecSQL(dtr, string.Format(@"
                                                        INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                                        select [ApplyFile].FileID,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE,0 
                                                        from [dbo].[ApplyFile]
                                                        inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                        on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                                                        where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID='{0}' and [VIEW_CERTIFICATECHANGE].GetResult='通过'", certhfchange.CertificateChangeID));



                    #endregion 更新证书标准附件库

                    ViewState["certhfchange"] = certhfchange;
                    ViewState["certificateob"] = certificateob;
                    ButtonCheck.Enabled = false;
                }
                else //不通过
                {
                    certhfchange.Status = EnumManager.CertificateChangeStatus.SendBack;
                    bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号

                    certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
                    certhfchange.ConfrimResult =TextBoxConfrimResult.Text.Trim();  //变更决定结论
                    certhfchange.ConfrimMan = PersonName;   //变更决定人
                    certhfchange.ConfrimCode = bgjd;   //变更决定批号


                    //修该变更记录
                    CertificateChangeDAL.Update(dtr, certhfchange);
                    ViewState["certhfchange"] = certhfchange;
                }

                dtr.Commit();
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, string.Format("建委决定证书{0}申请失败！", certhfchange.ChangeType), ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, string.Format("建委决定证书{0}申请", certhfchange.ChangeType), string.Format("证书编号：{0}，岗位工种：{1}。", RadTextBoxCertificateCode.Text, RadTextBoxPostID.Text));

            UIHelp.layerAlert(this.Page, "决定成功！", "hideIfam(true);");
        }

        //变换职务选择
        protected void RadComboBoxJob_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetUploadFileType();
        }

        //变换强制执行勾选
        protected void RadioButtonList_enforce_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
            {
                div_enforceType.Style.Remove("display");//申请强制执行原因
                div_DelContractDate.Style.Remove("display");//解除劳动关系日期
                div_NotWorkDate.Style.Add("display", "none");//不在该单位从事安全生产管理工作日期
                divCancelReason.Style.Add("display", "none");

                ButtonExit.Text = "提交建委审核";
            }
            else//非强制
            {
                div_enforceType.Style.Add("display", "none");
                div_DelContractDate.Style.Add("display", "none");
 
                ButtonExit.Text = "提交单位审核";
                if (myCertificateOB.PostTypeID == 1 && RadTextBoxChangeType.SelectedItem.Text == "注销")
                {
                    divCancelReason.Style.Remove("display");//注销原因
                    if (RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
                    {
                        div_NotWorkDate.Style.Remove("display");//不在该单位从事安全生产管理工作日期
                    }
                }
            }

            //try
            //{
            //    CertificateChangeDAL.Update(certhfchange);
            //    ViewState["certhfchange"] = certhfchange;
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "证书变更申请提交单位审核失败！", ex);
            //    return;
            //}
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadioButtonList_enforce.ClientID), true);
        }

        protected void ButtonDownEnforceApply_Click(object sender, EventArgs e)
        {
            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            var ht = new System.Collections.Hashtable();
            ht["WorkerName"] = RadTextBoxWorkerName.Text;
            ht["WorkerCertificateCode"] = LabelWorkerCertificateCode.Text;
            ht["CertificateCode"] = RadTextBoxCertificateCode.Text;
            ht["UnitName"] = RadTextBoxUnitName.Text;


            string sourceFile = HttpContext.Current.Server.MapPath("~/Template/强制注销（离京）申请.docx");

            PrintDocument.CreateDataToWordByHashtable(sourceFile, string.Format("强制注销（离京）申请_{0}", RadTextBoxCertificateCode.Text.Trim()), ht);

        }

        //选择劳动合同类型
        protected void RadioButtonListENT_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListENT_ContractType.SelectedValue == "1")
            {
                RadDatePickerENT_ContractENDTime.Style.Remove("display");
                LabelJZSJ.Style.Remove("display");
                ValidatorENT_ContractENDTime.Enabled = true;
            }
            else
            {
                RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
                LabelJZSJ.Style.Add("display", "none");
                ValidatorENT_ContractENDTime.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadioButtonListENT_ContractType.ClientID), true);
        }

        /// <summary>
        /// 绑定安管人员本人持证状态
        /// </summary>
        /// <param name="CardID">身份证</param>
        private void BindCurCertStatus(string CardID)
        {
            string sql = @"select c.CertificateCode,case when a.NewUNITCODE is not null then '变更' +a.[STATUS] +'，目标单位：' + a.NEWUNITNAME else '持证所属单位：' +c.UNITNAME end as UNITNAME
                            from [dbo].[CERTIFICATE] c
                            left join [dbo].[CERTIFICATECHANGE] a on c.CERTIFICATEID = a.CERTIFICATEID and a.ConfrimDate is null and c.UNITCODE <> a.NewUnitCode and a.CHANGETYPE='京内变更'
                            where c.[WorkerCertificateCode]='{0}' and c.posttypeid=1 and c.ValidEndDate >=dateadd(day,-1,getdate()) and c.[Status] <> '离京变更' and c.[Status] <> '注销'
                            order by c.CertificateCode";
            DataTable tb = CommonDAL.GetDataTable(string.Format(sql, CardID));

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (tb != null && tb.Rows.Count > 0)
            {              
                foreach (DataRow r in tb.Rows)
                {
                    sb.AppendFormat("<br />{0}，{1}。", r["CertificateCode"], r["UnitName"]);
                }
                if (sb.Length > 0) sb.Remove(0, 6);
               
                //tableCurCertStatus.Style.Add("display", "inline");
                tableCurCertStatus.Style.Remove("display");
                tdCurCertStatus.InnerHtml = sb.ToString();
                
            }
        }

        protected void RadioButtonListCancelReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListCancelReason.SelectedItem.Text == "不在从事安全生产管理工作")
            {
                div_NotWorkDate.Style.Remove("display");//不在该单位从事安全生产管理工作日期               
            }
            else
            {
                div_NotWorkDate.Style.Add("display", "none");//不在该单位从事安全生产管理工作日期
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadioButtonListCancelReason.ClientID), true);
        }

        
    }
}
