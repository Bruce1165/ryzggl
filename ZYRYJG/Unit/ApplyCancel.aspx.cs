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
    public partial class ApplyCancel : BasePage
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


        /// <summary>
        /// 根据业务时间，显示注销原因待选项
        /// </summary>
        /// <param name="date">申请申请单创建日期</param>
        protected void FormatDDLCancelReason(DateTime? date)
        {
            string reason = "已与聘用单位解除劳动关系的;距离注册专业有效期不足30天的;受到行政或刑事处罚且在处罚期内的;法律、法规规定其他导致注册证书失效的。";
            DateTime publishDate = Convert.ToDateTime("2021-12-22");//新注销原因上线时间
            for (int i = RadioButtonListCancelReason.Items.Count -1; i >= 0;i-- )
            {
                if (date > publishDate)//新注销原因上线后
                {
                    if (reason.Contains(RadioButtonListCancelReason.Items[i].Value) == false)
                    {

                        RadioButtonListCancelReason.Items.Remove(RadioButtonListCancelReason.Items[i]);
                    }
                }
                else
                {
                    if (reason.Contains(RadioButtonListCancelReason.Items[i].Value) == true)
                    {
                        RadioButtonListCancelReason.Items.Remove(RadioButtonListCancelReason.Items[i]);
                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族

                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;

                if (ViewLimit == true)//查看
                {
                    divViewAction.Visible = true;
                    Disabled();//禁用控件
                }

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    //注册申请表     
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    ApplyMDL _ApplyMDL = ApplyDAL.GetObject(_ApplyID);
                    ViewState["ApplyMDL"] = _ApplyMDL;

                    ApplyCancelMDL _ApplyCancelMDL = ApplyCancelDAL.GetObject(_ApplyID);
                    ViewState["ApplyCancelMDL"] = _ApplyCancelMDL;

                    if (string.IsNullOrEmpty(_ApplyMDL.LastBackResult) == false && _ApplyMDL.ApplyStatus != EnumManager.ApplyStatus.已驳回)
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("<apan style='color:red'>【上次退回记录】{0}</span>", _ApplyMDL.LastBackResult);
                    }
                    ApplyMDL lastApply = ApplyDAL.GetLastApplyObject(_ApplyMDL.ApplyID);
                    if (lastApply != null
                        && _ApplyMDL.ApplyTime.HasValue == true
                        && lastApply.ApplyType == _ApplyMDL.ApplyType
                        && lastApply.NoticeDate > _ApplyMDL.ApplyTime.Value.AddYears(-1)
                        && lastApply.ConfirmResult == "不通过"
                    )
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("{0}<p style='color:red'>该申请人已于一年内第二次提交相同注册申请，请核对资料后如有不通过情形请及时与申请人电话沟通。</p>", RadGridCheckHistory.MasterTableView.Caption);

                        if (_ApplyMDL.NoticeDate.HasValue == false)
                        {
                            if (IfExistRoleID("0"))
                            {
                                UIHelp.layerAlert(Page, string.Format("提示您，上次注册申请审核未通过的原因是：【{0}】请您在本次申请提交时，注意核对填报内容是否按照要求修改完成。",lastApply.CheckResult=="不通过"?lastApply.CheckRemark: lastApply.ConfirmRemark));
                            }
                            else if (IfExistRoleID("2") == false)
                            {
                                UIHelp.layerAlert(Page, string.Format("提示审核人员：该申请人一年内第二次提交注册申请，上次不通过的原因是：【{0}】请注意核对，如存在问题请及时驳回申请。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                            }
                        }
                    }

                    FormatDDLCancelReason(_ApplyMDL.CJSJ);

                    if (
                            (
                                (IfExistRoleID("0") == true && _ApplyCancelMDL.CancelReason != "申请强制注销（因二级建造师不配合等原因）")//个人
                                || (IfExistRoleID("2") == true && _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                            )
                        && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报
                            || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认
                            || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回
                            || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报
                            )//2019-10-25  南静添加 待确认判断条件
                    )
                    {
                        divGR.Visible = true;
                    }

                    if (
                            (
                                IfExistRoleID("0") == true//个人
                                ||(IfExistRoleID("2") == true && _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                            )
                             && 
                             (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报
                                || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回
                             )
                     )
                    {

                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyCancelMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        UIHelp.SetReadOnly(RadioButtonListCancelReason, false);
                        UIHelp.SetReadOnly(RadioButtonList_enforce, false);
                        UIHelp.SetReadOnly(CheckBoxListPSN_RegisteProfession, false);
                        //trFuJanTitel.Visible = true;
                        //trFuJan.Visible = true;

                    }
                    else//只读
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyCancelMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxLinkMan, true);
                        UIHelp.SetReadOnly(RadioButtonListCancelReason, true);
                        UIHelp.SetReadOnly(RadioButtonList_enforce, true);
                        UIHelp.SetReadOnly(CheckBoxListPSN_RegisteProfession, true);
                    }




                    //if (_ApplyMDL.ApplyStatus != EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus != EnumManager.ApplyStatus.已驳回)//提交后不允许修改
                    //{
                    //    ButtonSave.Enabled = false;
                    //}

                    //申请操作权限
                    if (IfExistRoleID("2") == true)
                    {
                        // divUnit.Visible = true;

                        //企业看不到各级申办人列
                        RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;

                        if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报 && _ApplyCancelMDL.CancelReason != "申请强制注销（因企业不配合、企业营业执照注销等原因）")
                        {
                            ButtonApply.Text = "取消申报";
                            divUnit.Visible = true;
                        }
                        if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认)
                        {
                            TextBoxOldUnitCheckRemark.Text = "提交区县审核";
                            UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                            divUnit.Visible = true;
                        }

                        //if (IfExistRoleID("2") == true)
                        //{
                        //    RadioButtonList_enforce.SelectedIndex = 1;
                        //    UIHelp.SetReadOnly(RadioButtonList_enforce, true);

                        //    RadioButtonListCancelReason.Visible = false;
                        //    RadioButtonWorkerEnforceApply.Visible = false;
                        //    RadioButtonUnitEnforceApply.Visible = true;

                        //    ButtonUnit.Text = "提交建委审核";
                        //}
                        //if (_ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                        //{
                        //    divGR.Visible = true;
                        //    if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回)
                        //    {

                        //    }
                        //}
                    }

                    //区县受理权限
                    if (IfExistRoleID("3") == true && ViewLimit == false)
                    {
                        divQX.Visible = true;
                        Disabled();//禁用控件
                    }
                    //区县审查
                    if (IfExistRoleID("7") == true && ViewLimit == false)
                    {
                        divQXCK.Visible = true;
                        Disabled();//禁用控件
                    }
                    //建委审核权限
                    if (IfExistRoleID("4") == true && ViewLimit == false)
                    {
                        divCheck.Visible = true;
                        Disabled();//禁用控件
                    }
                    //建委领导审核权限
                    if (IfExistRoleID("6") == true && ViewLimit == false)
                    {
                        divDecide.Visible = true;
                        Disabled();//禁用控件
                    }

                    SetButtonEnable(_ApplyMDL.ApplyStatus);
                    BindFile(_ApplyMDL.ApplyID);//附件                   
                    BindCheckHistory(_ApplyCancelMDL.ApplyID); //审批记录

                    #region   ---已注册专业的赋值&&专业信息CheckBoxList集合&&单选框集合
                    //已注册专业集合
                    DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(_ApplyMDL.PSN_ServerID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                        if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");
                    }
                    //-------------
                    List<COC_TOW_Register_ProfessionMDL> _COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.GetListGetObject(_ApplyMDL.PSN_ServerID);
                    CheckBoxListPSN_RegisteProfession.DataSource = _COC_TOW_Register_ProfessionMDL;
                    CheckBoxListPSN_RegisteProfession.DataTextField = "PRO_Profession";
                    CheckBoxListPSN_RegisteProfession.DataValueField = "PRO_ServerID";
                    CheckBoxListPSN_RegisteProfession.DataBind();
                    string[] arry = _ApplyCancelMDL.ZyIDItem.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < CheckBoxListPSN_RegisteProfession.Items.Count; i++)
                    {
                        for (int j = 0; j < arry.Length; j++)
                        {
                            if (CheckBoxListPSN_RegisteProfession.Items[i].Value == arry[j])
                            {
                                CheckBoxListPSN_RegisteProfession.Items[i].Selected = true;
                            }
                        }
                    }
                    ListItem ApplyManType = RadioButtonListApplyManType.Items.FindByValue(_ApplyCancelMDL.ApplyManType);
                    if (ApplyManType != null) { ApplyManType.Selected = true; }
                    ListItem CancelReason = RadioButtonListCancelReason.Items.FindByValue(_ApplyCancelMDL.CancelReason);
                    if (CancelReason != null) { CancelReason.Selected = true; }

                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）"
                       || _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    {
                        RadioButtonList_enforce.SelectedIndex = 1;
                        if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）")
                        {

                            RadioButtonWorkerEnforceApply.Visible = true;
                            RadioButtonUnitEnforceApply.Visible = false;
                            RadioButtonListCancelReason.Visible = false;
                        }
                        else
                        {
                            RadioButtonUnitEnforceApply.Visible = true;
                            RadioButtonWorkerEnforceApply.Visible = false;
                            RadioButtonListCancelReason.Visible = false;
                        }
                    }
                    else
                    {
                        RadioButtonList_enforce.SelectedIndex = 0;
                    }

                    //if (RadioButtonListCancelReason.SelectedValue == "已与聘用单位解除劳动关系的")
                    //{
                    //    trJiePinShiJian.Visible = true;
                    //}
                    //else
                    //{
                    //    trJiePinShiJian.Visible = false;
                    //}
                    #endregion

                }
                else//new
                {
                    FormatDDLCancelReason(DateTime.Now);

                    SetButtonEnable("");
                    //二建人员表
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));

                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "企业信息中的监管区县与建造师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                    //    return;
                    //}

                    //#endregion

                    ////锁定状态时，只允许做注销注册                  
                    //#region 查询证书是否锁定
                    //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(o.PSN_CertificateNO);
                    //if (count > 0)
                    //{
                    //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    //    return;
                    //}
                    //#endregion

                    if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)//个人登录后  2019-10-25  南静添加
                    {
                        divGR.Visible = true;

                        if (o != null)
                        {
                            //#region 查询证书是否打印
                            //if (o.PSN_RegistePermissionDate > DateTime.Now.AddDays(-180))
                            //{
                            //    int sum = DataAccess.COC_TOW_Person_BaseInfoDAL.PrintIsorNot(o.PSN_CertificateNO);
                            //    if (sum > 0)
                            //    {
                            //        UIHelp.layerAlert(Page, "上次业务办理证书尚未打印，请先打印证书！", 5, 0);
                            //        return;
                            //    }
                            //}
                            //#endregion


                            ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                            UIHelp.SetData(EditTable, o, true);
                            UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                            //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                            RadTextBoxRegisterValidity.Text = o.PSN_CertificateValidity.HasValue == true ? o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd") : "";
                            UIHelp.SetReadOnly(RadTextBoxRegisterValidity, true);
                            if (IfExistRoleID("2") == true)
                            {
                                RadioButtonList_enforce.SelectedIndex = 1;
                                UIHelp.SetReadOnly(RadioButtonList_enforce, true);

                                RadioButtonListCancelReason.Visible = false;
                                RadioButtonWorkerEnforceApply.Visible = false;
                                RadioButtonUnitEnforceApply.Visible = true;

                                ButtonUnit.Text = "提交建委审核";
                            }

                            //考生信息
                            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(o.PSN_CertificateNO);
                            RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
                            if (string.IsNullOrEmpty(_WorkerOB.Email) == false) RadTextBoxPSN_Email.Text = _WorkerOB.Email;//邮箱
                            if (string.IsNullOrEmpty(_WorkerOB.Nation) == false)//民族
                            {
                                RadComboBoxItem find = RadComboBoxNation.Items.FindItemByText(_WorkerOB.Nation);
                                if (find != null) find.Selected = true;
                            }


                            //企业信息
                            UnitMDL _UnitMDL = UnitDAL.GetObject(o.ENT_ServerID);
                            if (_UnitMDL != null)
                            {
                                RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                                UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                                RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                                UIHelp.SetReadOnly(RadTextBoxLinkMan, true);

                                //已注册专业集合
                                DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                                    if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");
                                }

                                //List<COC_TOW_Register_ProfessionMDL> _COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.GetListGetObject(o.PSN_ServerID);
                                CheckBoxListPSN_RegisteProfession.DataSource = dt;// _COC_TOW_Register_ProfessionMDL;
                                CheckBoxListPSN_RegisteProfession.DataTextField = "PRO_Profession";
                                CheckBoxListPSN_RegisteProfession.DataValueField = "PRO_ServerID";
                                CheckBoxListPSN_RegisteProfession.DataBind();

                                for (int i = 0; i < CheckBoxListPSN_RegisteProfession.Items.Count; i++)
                                {
                                    CheckBoxListPSN_RegisteProfession.Items[i].Selected = true;
                                }
                            }
                            //人员照片
                            BindFile("0");
                        }
                    }
                }


            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
        }
         //禁用控件
        private void Disabled()
        {
            CheckBoxListPSN_RegisteProfession.Enabled = false;
            RadioButtonListApplyManType.Enabled = false;
            RadioButtonListCancelReason.Enabled = false;
        }
        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(string ApplyID)
        {
            if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)
            {
                DataTable dt = ApplyDAL.GetCheckHistoryList(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
            else
            {
                DataTable dt = ApplyDAL.GetCheckHistoryList2(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
        }

        //个人保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            //单选按钮校验
            //if (RadioButtonListApplyManType.SelectedIndex == -1 )
            //{
            //    UIHelp.layerAlert(Page, "请选择申请注销注册人或机构！");
            //    return;
            //}
            if (RadioButtonUnitEnforceApply.Visible==false && RadioButtonWorkerEnforceApply.Visible==false && RadioButtonListCancelReason.SelectedIndex == -1 )
            {
                UIHelp.layerAlert(Page, "请选择注销原因！");
                return;
            }
            if (CheckBoxListPSN_RegisteProfession.SelectedIndex == -1)
            {
                UIHelp.layerAlert(Page, "请选择注销专业！");
                return;
            }
            if (RadTextBoxENT_Telephone.Text.Trim() == "" )
            {
                UIHelp.layerAlert(Page, "保存失败，联系电话不能为空！", 5, 0);
                return;
            }

            if (RadTextBoxPSN_MobilePhone.Text.Trim() == ""||!Check.IfTelPhoneFormat(RadTextBoxPSN_MobilePhone.Text.Trim()))
            {
                UIHelp.layerAlert(Page, "保存失败，手机号码错误！", 5, 0);
                return;
            }
            
            if (RadTextBoxPSN_Email.Text.Trim() == "" || !Check.IfMailFormat(RadTextBoxPSN_Email.Text.Trim()))
            {
                UIHelp.layerAlert(Page, "保存失败，邮箱错误！", 5, 0);
                return;
            }
            if (RadComboBoxNation.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "保存失败，民族不可为空！", 5, 0);
                return;
            }
            //if (RadioButtonListCancelReason.SelectedValue == "已与聘用单位解除劳动关系的" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == false)
            //{
            //    UIHelp.layerAlert(Page, "请选择一个解聘时间！", 5, 0);
            //    return;
            //}

            #endregion 输入验证

            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表

            if (ViewState["ApplyMDL"] == null)
            {
                jcsjk_RY_JZS_ZSSDMDL o = jcsjk_RY_JZS_ZSSDDAL.GetLockingInfo(RadTextBoxPSN_RegisterNO.Text);
                if (o != null)
                {
                    UIHelp.layerAlert(Page, string.Format("无法申请，证书处于在施项目锁定中（{0}），请先办理在施项目解锁！", o.XMMC), 5, 0);
                    return;
                }

                if (ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_RegisterNO.Text.Trim(), EnumManager.ApplyStatus.未申报, EnumManager.ApplyStatus.待确认)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }

            UIHelp.GetData(EditTable, _ApplyMDL);
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            _ApplyMDL.Valid = 1;
            _ApplyMDL.ApplyType = EnumManager.ApplyType.注销;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;

            ApplyCancelMDL _ApplyCancelMDL = ViewState["ApplyCancelMDL"] == null ? new ApplyCancelMDL() : (ApplyCancelMDL)ViewState["ApplyCancelMDL"];//详细表

            UIHelp.GetData(EditTable, _ApplyCancelMDL);

            if (PersonType == 2)
            {
                _ApplyCancelMDL.ApplyManType = "注册建造师本人";              
            }
            if (PersonType == 3)
            {
                _ApplyCancelMDL.ApplyManType = "聘用企业";
            }
            if(RadioButtonWorkerEnforceApply.Visible==true)
            {
                _ApplyCancelMDL.CancelReason = "申请强制注销（因企业不配合、企业营业执照注销等原因）";
                _ApplyMDL.Memo = "申请强制注销";
            }
            else if (RadioButtonUnitEnforceApply.Visible == true)
            {
                _ApplyCancelMDL.CancelReason = "申请强制注销（因二级建造师不配合等原因）";
                _ApplyMDL.Memo = "申请强制注销";
            }
            else
            {
                _ApplyCancelMDL.CancelReason = RadioButtonListCancelReason.SelectedValue;
                _ApplyMDL.Memo = null;
            }

            //声明一个Sting类型的集合，获取所有选择的专业
            List<string> checkbox = new List<string>();
            System.Text.StringBuilder zyid = new System.Text.StringBuilder();
            System.Text.StringBuilder zyjh = new System.Text.StringBuilder();
            for (int i = 0; i < CheckBoxListPSN_RegisteProfession.Items.Count; i++)
            {
                if (CheckBoxListPSN_RegisteProfession.Items[i].Selected)
                {
                    zyid.Append(CheckBoxListPSN_RegisteProfession.Items[i].Value + ",");
                    zyjh.Append(CheckBoxListPSN_RegisteProfession.Items[i].Text + ",");
                }
            }
            if (zyid.Length > 0) zyid.Remove(zyid.Length - 1, 1);
            if (zyjh.Length > 0) zyjh.Remove(zyjh.Length - 1, 1);
            _ApplyCancelMDL.ZyIDItem = zyid.ToString();
            _ApplyCancelMDL.ZyItem = zyjh.ToString();
            if (_ApplyCancelMDL.ZyItem != null)
            {
                _ApplyMDL.PSN_RegisteProfession = _ApplyCancelMDL.ZyItem;
            }

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "注销");
                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;
                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                    _ApplyMDL.ENT_ServerID = person.ENT_ServerID;
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;                   
                    
                    //申请注销专业
                    if (_ApplyCancelMDL.ZyItem!=null)
                    {
                        _ApplyMDL.PSN_RegisteProfession = _ApplyCancelMDL.ZyItem;
                    }
                    //_ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;

                    _ApplyCancelMDL.ApplyID = _ApplyMDL.ApplyID;
                    //根据人员表的企业ID获取到企业表信息,取到企业通讯地址和邮政编码；
                    UnitMDL _UnitMDL = UnitDAL.GetObject(person.ENT_ServerID);
                    _ApplyCancelMDL.ENT_Correspondence = _UnitMDL.ENT_Correspondence;
                    _ApplyCancelMDL.ENT_Postcode = _UnitMDL.ENT_Postcode;

                    //所属区县
                    if (string.IsNullOrEmpty(_ApplyMDL.ENT_City))
                    {
                        UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                        return;
                    }

                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyCancelDAL.Insert(tran, _ApplyCancelMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

                    //trFuJanTitel.Visible = true;
                    //trFuJan.Visible = true;
                }
                else//update
                {
                    ApplyDAL.Update(tran, _ApplyMDL);
                    ApplyCancelDAL.Update(tran, _ApplyCancelMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyCancelMDL"] = _ApplyCancelMDL;
                BindFile(ApplyID);
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存注销申请表信息失败", ex);
            }

            //Response.Expires = 0;
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            //Response.AddHeader("pragma", "no-cache");
            //Response.CacheControl = "no-cache";
            //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
        }

        //个人提交单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            ApplyCancelMDL _ApplyCancelMDL = ViewState["ApplyCancelMDL"] == null ? null : (ApplyCancelMDL)ViewState["ApplyCancelMDL"];
            try
            {
                if (ButtonUnit.Text != "取消申报")
                {
                    //必须上传附件集合
                    System.Collections.Hashtable fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.一寸免冠照片,0},
                        //{EnumManager.FileDataTypeName.符合注销注册情形的相关证明,0},
                        {EnumManager.FileDataTypeName.申请表扫描件,0}
                    };

                    if (RadioButtonListCancelReason.SelectedValue != "距离注册专业有效期不足30天的" || RadioButtonListCancelReason.Visible == false)
                    {
                        fj.Add(EnumManager.FileDataTypeName.符合注销注册情形的相关证明, 0);
                    }

                    //已上传附件集合
                    DataTable dt = ApplyDAL.GetApplyFile(_ApplyMDL.ApplyID);

                    //计数
                    foreach (DataRow r in dt.Rows)
                    {
                        if (fj.ContainsKey(r["DataType"].ToString()) == true)
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

                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1);
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
                        return;
                    }

                    //检查一寸照片数量
                    int faceImgCount = ApplyFileDAL.SelectFaceImgCountByApplyID(_ApplyMDL.ApplyID);
                    if (faceImgCount > 1)
                    {
                        UIHelp.layerAlert(Page, string.Format("只能上传一张一寸免冠照片，请删除多余照片！", sb), 5, 0);
                        return;
                    }
                }

                _ApplyMDL.XGR = UserName;
                _ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonUnit.Text == "取消申报")
                {
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                    _ApplyMDL.ApplyTime = null;

                    _ApplyMDL.OldUnitCheckResult = null;
                    _ApplyMDL.OldUnitCheckRemark = null;
                    _ApplyMDL.OldUnitCheckTime = null;
                }
                else
                {
                    
                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    {
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                    }
                    else
                    {
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
                        _ApplyMDL.OldUnitCheckResult = null;
                        _ApplyMDL.OldUnitCheckRemark = null;
                        _ApplyMDL.OldUnitCheckTime = null;
                    }
                    
                    _ApplyMDL.ApplyTime = DateTime.Now;
                    _ApplyMDL.GetResult = null;
                    _ApplyMDL.GetRemark = null;
                    _ApplyMDL.GetMan = null;
                    _ApplyMDL.GetDateTime = null;//2019-09-26 南静修改  修改原因： 受理时间未清空

                    _ApplyMDL.ExamineDatetime = null;
                    _ApplyMDL.ExamineResult = null;
                    _ApplyMDL.ExamineRemark = null;
                    _ApplyMDL.ExamineMan = null;

                    _ApplyMDL.ReportDate = null;
                    _ApplyMDL.ReportMan = null;
                    _ApplyMDL.ReportCode = null;

                    _ApplyMDL.CheckDate = null;
                    _ApplyMDL.CheckResult = null;
                    _ApplyMDL.CheckRemark = null;
                    _ApplyMDL.CheckMan = null;

                    _ApplyMDL.ConfirmDate = null;
                    _ApplyMDL.ConfirmResult = null;
                    _ApplyMDL.ConfirmMan = null;
                }
                ApplyDAL.Update(_ApplyMDL);
                ViewState["ApplyMDL"] = _ApplyMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                BindFile(_ApplyMDL.ApplyID);//附件
                if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请提交单位确认成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID));
                }
                else if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "二建注销注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）")
                    {
                        UIHelp.layerAlert(Page, "提交建委审核成功。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID));
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "提交建委审核成功！", 6, 0, "var isfresh=true;");
                    }
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "注销注册取消申报成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    UIHelp.SetReadOnly(RadioButtonListCancelReason, false);
                    UIHelp.SetReadOnly(RadioButtonList_enforce, false);
                    UIHelp.SetReadOnly(CheckBoxListPSN_RegisteProfession, false);
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "注销注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //个人删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            ApplyDAL.Delete(_ApplyMDL.ApplyID);
            ViewState.Remove("ApplyMDL");
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }
        
        //单位确认
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            try
            {
                _ApplyMDL.XGR = UserName;
                _ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonApply.Text == "取消申报")//单位取消申报
                {
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                    _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                    _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; //现单位审核结果
                    _ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldUnitCheckRemark.Visible = true;
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                    }
                    else
                    {
                        TextBoxOldUnitCheckRemark.Visible = false;
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                        _ApplyMDL.GetResult = null;
                        _ApplyMDL.GetRemark = null;
                        _ApplyMDL.GetMan = null;
                        _ApplyMDL.GetDateTime = null;

                        _ApplyMDL.ExamineDatetime = null;
                        _ApplyMDL.ExamineResult = null;
                        _ApplyMDL.ExamineRemark = null;
                        _ApplyMDL.ExamineMan = null;

                        _ApplyMDL.ReportDate = null;
                        _ApplyMDL.ReportMan = null;
                        _ApplyMDL.ReportCode = null;

                        _ApplyMDL.CheckDate = null;
                        _ApplyMDL.CheckResult = null;
                        _ApplyMDL.CheckRemark = null;
                        _ApplyMDL.CheckMan = null;

                        _ApplyMDL.ConfirmDate = null;
                        _ApplyMDL.ConfirmResult = null;
                        _ApplyMDL.ConfirmMan = null;
                    }
                }
                ApplyDAL.Update(_ApplyMDL);
                ViewState["ApplyMDL"] = _ApplyMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                BindFile(_ApplyMDL.ApplyID);

                if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, _ApplyMDL.PSN_RegisteProfession));
                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);

                    //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, _ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "注销注册申报失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace("green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
            step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");
            step_已审查.Attributes["class"] = step_已审查.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
         

            switch (ApplyStatus)
            {
                case "未申报":
                case "已驳回":
                    step_未申报.Attributes["class"] += " green";
                    break;
                case "待确认":
                    step_待确认.Attributes["class"] += " green";
                    break;
                case "已申报":
                    step_已申报.Attributes["class"] += " green";
                    break;
                case "已受理":
                    step_已受理.Attributes["class"] += " green";
                    break;
                case "区县审查":
                       step_区县审查.Attributes["class"] += " green";
                    break;
                case "已上报":
                    step_已上报.Attributes["class"] += " green";
                    break;
                case "已审查":
                    step_已审查.Attributes["class"] += " green";
                    break;
                case "已决定":
                    step_已决定.Attributes["class"] += " green";
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(string ApplyStatus)
        {
            ApplyCancelMDL _ApplyCancelMDL = ViewState["ApplyCancelMDL"] == null ? null : (ApplyCancelMDL)ViewState["ApplyCancelMDL"];

            SetStep(ApplyStatus);

            switch (ApplyStatus)
            {                
                case "":
                    ButtonSave.Enabled = true;
                      ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = true;
                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    {
                        ButtonUnit.Text = "提交建委审核";
                    }
                    else
                    {
                        ButtonUnit.Text = "提交单位确认";
                    }
                    
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    //个人登录后
                    if (IfExistRoleID("0") == true
                        || (_ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）" && IfExistRoleID("2") == true)
                      )
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }
                    break;
                case EnumManager.ApplyStatus.待确认:
                    ButtonSave.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确定";
                    break;
                case EnumManager.ApplyStatus.已申报:
                    //企业
                    //ButtonUnitChek.Enabled = true;
                    //ButtonUnitChek.Text = "取消申报";
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";
                    if (IfExistRoleID("2") == true && 
                        (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" 
                        || _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    )
                    {
                        divUnit.Visible = true;
                    }

                    //个人
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";

                    break;
                case EnumManager.ApplyStatus.已驳回:
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = true;
                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    {
                        ButtonUnit.Text = "提交建委审核";
                    }
                    else
                    {
                        ButtonUnit.Text = "提交单位确认";
                    }
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;

                    //企业
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确定";
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "取消申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
            }
            if (ApplyStatus == EnumManager.ApplyStatus.已受理
                || ApplyStatus == EnumManager.ApplyStatus.区县审查
                || ApplyStatus == EnumManager.ApplyStatus.已上报
                || ApplyStatus == EnumManager.ApplyStatus.已审查)
            {
                //注册室管理员
                if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                {
                    UIHelp.SetJZSReturnStatusList(RadComboBoxReturnApplyStatus, ApplyStatus);
                    divSendBack.Visible = true;//后退
                }
            }
            if (ApplyStatus == EnumManager.ApplyStatus.已公告)
            {
                //注册室管理员
                if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                {
                    ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                    if (_ApplyMDL.ConfirmResult == "不通过")
                    {
                        UIHelp.SetJZSReturnStatusList(RadComboBoxReturnApplyStatus, ApplyStatus);
                        divSendBack.Visible = true;//后退
                    }
                }
            }
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //导出打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = "";
                string fileName = "";
                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;               
                ApplyCancelMDL _ApplyCancelMDL = ViewState["ApplyCancelMDL"] as ApplyCancelMDL;
                UnitMDL _UnitMDL = UnitDAL.GetObject(_ApplyMDL.ENT_ServerID);
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);

                if (_ApplyMDL.PSN_Level.Trim() == "二级")
                {
                    if (_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）") {
                        sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师注销注册申请表_个人强制.docx");
                    }
                    else if (_ApplyCancelMDL.CancelReason == "申请强制注销（因二级建造师不配合等原因）")
                    {
                        sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师注销注册申请表_企业强制.docx");
                    }
                    else {
                        sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师注销注册申请表.docx");
                    }
                    fileName = string.Format("注销注册_{0}", RadTextBoxPSN_Name.Text);
                }
                if (_ApplyMDL.PSN_Level.Trim() == "二级临时")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师临时执业证书注销注册申请表.docx");
                    fileName = string.Format("二级临时注销注册_{0}", RadTextBoxPSN_Name.Text);
                }
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyCancelMDL);
                o.Add(_UnitMDL);
                var ht = PrintDocument.GetProperties(o);

                if (_FileInfoMDL != null)
                {
                    ht["photo"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else { ht["photo"] = ""; }
                if (ht["photo"].ToString() == "")
                {
                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
                    return;
                }
                ht["Birthday"] = _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate == null ? "" : ((DateTime)_COC_TOW_Person_BaseInfoMDL.PSN_BirthDate).ToString("yyyy年MM月dd日");
               // ht["Nation"] = _COC_TOW_Person_BaseInfoMDL.PSN_National;
                ht["FR"] = _UnitMDL.ENT_Corporate;
                ht["isCtable"] = false;

                DateTime? maxValidityEnd = COC_TOW_Register_ProfessionDAL.GetMaxPRO_ValidityEnd(_ApplyMDL.PSN_ServerID, _ApplyCancelMDL.ZyItem);
                //对时间类型进行格式转换
                ht["RegisterValidity"] = maxValidityEnd == null ? ((DateTime)_ApplyCancelMDL.RegisterValidity).ToString("yyyy年MM月dd日") : maxValidityEnd.Value.ToString("yyyy年MM月dd日");

                if (_ApplyCancelMDL.CancelReason == "已与聘用单位解除劳动关系的")
                {
                    ht["JiePinChengNuo"] = string.Format("\n　　本人已与{0}完成工作交接,已不在其承建的建设工程项目中继续担任施工项目负责人。", _ApplyMDL.ENT_Name);
                    ht["CancelReason"] = "已与聘用单位解除劳动关系的";
                }
                else
                {
                    ht["JiePinChengNuo"] = "";
                }                
                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印注销申请Word失败！", ex);
            }
        }

        //区县受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion
            _ApplyMDL.GetDateTime = DateTime.Now;
            _ApplyMDL.GetMan = UserName;
            _ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;

            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "注销注册区县受理", string.Format("姓名：{0}，身份证号：{1}。审批结果：{2}，审批意见：{3}。", UserName, RadTextBoxPSN_CertificateNO.Text
                 , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
            //UIHelp.ParentAlert(Page, "受理成功！", true);


            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);

            //Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
        }

        //区县审查
        protected void BttSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            
            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion

            _ApplyMDL.ExamineDatetime = DateTime.Now;
            _ApplyMDL.ExamineMan = UserName;
            _ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
            _ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;

            if (RadioButtonListExamineResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.ExamineDatetime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxExamineRemark1.Text.Trim());
            }

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "注销注册区县审查", string.Format("姓名：{0}，身份证号：{1}。审批结果：{2}，审批意见：{3}。", UserName, RadTextBoxPSN_CertificateNO.Text
                 , _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "区县审查成功！", true);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
           
            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion
            _ApplyMDL.CheckDate = DateTime.Now;
            _ApplyMDL.CheckMan = UserName;
            _ApplyMDL.CheckResult = RadioButtonListCheckResult.SelectedValue;
            _ApplyMDL.CheckRemark = TextBoxApplyCheckRemark.Text.Trim();
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已审查;

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "审核失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "注销注册建委审核", string.Format("姓名：{0}，身份证号：{1}。审批结果：{2}，审批意见：{3}。", UserName, RadTextBoxPSN_CertificateNO.Text
                , _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //建委领导决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            
            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion

            _ApplyMDL.ConfirmDate = DateTime.Now;
            _ApplyMDL.ConfirmMan = UserName;
            _ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已公告;//办结
            //此业务结束，给他个公告时间，系统就能分配可以重新申请业务
            _ApplyMDL.NoticeDate = DateTime.Now;
            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                ApplyDAL.Update(tran, _ApplyMDL);

                if (RadioButtonListDecide.SelectedValue == "通过")
                {
                    DataTable dt = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  ApplyID='" + _ApplyMDL.ApplyID + "' ) A INNER JOIN  ApplyCancel B  ON A.APPLYID=B.APPLYID");
                    //二级人员信息
                    COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(tran, dt.Rows[0]["PSN_ServerID"].ToString());
                    //判断是注销专业还是注销证书，注销专业在专业里面去掉专业，注销证书修改证书有效期
                    //往历史记录表备份数据,根据建造师ID拿到去正式表拿到一个对象
                    //把专业往历史记录表挪一次
                    //先往历史纪录表导入数据
                    if (_COC_TOW_Person_BaseInfoMDL != null)
                    {
                        //正式表往记录表写数，右边是一个方法，根据建造师ID拿到一个记录表的信息
                        COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran, _COC_TOW_Person_BaseInfoMDL);
                        COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
                    }


                    List<COC_TOW_Register_ProfessionMDL> COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.ListGetObject2(tran, dt.Rows[0]["PSN_ServerID"].ToString());
                    //专业ID集合
                    string[] arryk = dt.Rows[0]["ZyIDItem"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    //专业集合
                    string[] arry = dt.Rows[0]["ZyItem"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (arry.Length != COC_TOW_Register_ProfessionMDL.Count)//注销专业
                    {

                        for (int i = 0; i < arry.Length; i++)
                        {
                            //把专业先往历史记录表倒过去
                            COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.GetObject(arryk[i].ToString());
                            //拿到历史记录表实体对象然后插入
                            COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = COC_TOW_Register_Profession_HisDAL.ListGetObject(tran, _COC_TOW_Register_ProfessionMDL, EnumManager.ApplyType.注销);
                            //先往专业历史表插入数据，在删除专业表的数据
                            COC_TOW_Register_Profession_HisDAL.Insert(tran, _COC_TOW_Register_Profession_HisMDL);
                            //修改掉注册专业有效期
                            CommonDAL.ExecSQL(tran, @"delete from COC_TOW_Register_Profession WHERE PRO_ServerID ='" + arryk[i].ToString() + "'");
                        }
                        //有效专业
                        //List<COC_TOW_Register_ProfessionMDL> ZY_COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.ListGetObject(tran, dt.Rows[0]["PSN_ServerID"].ToString());
                        string zhuanye = null;
                        //拿到所有专业的集合
                        List<COC_TOW_Register_ProfessionMDL> zy_COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionMDL;

                        //先剔除注销的专业集合
                        foreach (var a in zy_COC_TOW_Register_ProfessionMDL)
                        {

                            for (int k = 0; k < arry.Length; k++)
                            {
                                if (arry[k] != a.PRO_Profession)
                                {
                                    zhuanye = zhuanye + a.PRO_Profession + ",";
                                }
                            }
                        }
                    
                        if (zhuanye.Length > 0)
                        { 
                          zhuanye=zhuanye.Remove(zhuanye.Length - 1, 1);
                        }
                        _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = zhuanye;
                        //证书最大有效期
                        DataTable maxtime = CommonDAL.GetDataTable(tran, @" SELECT MAX(PRO_ValidityEnd)AS MAXTIME FROM COC_TOW_Register_Profession WHERE Psn_ServerID='" + dt.Rows[0]["PSN_ServerID"].ToString() + "' AND PRO_ValidityEnd  >GETDATE()");
                        if (maxtime.Rows[0][0] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(maxtime.Rows[0][0]);
                    }
                    else//注销所有
                    {                       
                        ////证书有效期：不改变证书有效期
                        //_COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = DateTime.Now;

                        //注销状态
                        _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = "07";

                        if (dt.Rows[0]["ApplyManType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = dt.Rows[0]["ApplyManType"].ToString();
                        if (dt.Rows[0]["CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = dt.Rows[0]["CancelReason"].ToString();

                        //注册审批日期
                        _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = DateTime.Now;

                    }
                    
                    //Update正式表信息
                    _COC_TOW_Person_BaseInfoMDL.XGR = UserName;
                    _COC_TOW_Person_BaseInfoMDL.XGSJ = DateTime.Now;
                    COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);
                }

                tran.Commit();
                UIHelp.WriteOperateLog(UserName, UserID, "注销注册建委决定成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                UIHelp.ParentAlert(Page, "决定成功！", true);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "决定失败！", ex);
                return;
            }

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
                UIHelp.WriteErrorLog(Page, "删除申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "注销注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        //protected void RadioButtonListCancelReason_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(RadioButtonListCancelReason.SelectedValue=="已与聘用单位解除劳动关系的")
        //    {
        //        trJiePinShiJian.Visible = true;
        //        UIHelp.SetReadOnly(RadDatePickerENT_ContractENDTime, false);
        //    }
        //    else
        //    {
        //        trJiePinShiJian.Visible = false;
        //    }
        //}

        //审批环节后退
        protected void ButtonSendBack_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ApplyDAL.GetObject(ApplyID);

            if (_ApplyMDL.ApplyStatus == RadComboBoxReturnApplyStatus.SelectedValue)
            {
                UIHelp.layerAlert(Page, "后退节点与当前申请单所处节点相同，无需后退！");
                return;
            }

            string log = "";
            switch (_ApplyMDL.ApplyStatus)
            {
                case EnumManager.ApplyStatus.已受理:
                case EnumManager.ApplyStatus.已驳回:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，受理时间：{3}，受理结果：{4}，受理意见：{5}。后退到“{6}”状态。"
                   , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.GetDateTime, _ApplyMDL.GetResult, _ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    _ApplyMDL.GetDateTime = null;
                    _ApplyMDL.GetMan = null;
                    _ApplyMDL.GetResult = null;
                    _ApplyMDL.GetRemark = null;
                    _ApplyMDL.ExamineDatetime = null;
                    _ApplyMDL.ExamineMan = null;
                    _ApplyMDL.ExamineResult = null;
                    _ApplyMDL.ExamineRemark = null;
                    if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回)//取消最后驳回意见
                    {
                        _ApplyMDL.LastBackResult = null;
                    }
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                    break;
                case EnumManager.ApplyStatus.区县审查:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，区县审查时间：{3}，区县审查结果：{4}，区县审查意见：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.ExamineDatetime, _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ApplyStatus.已申报:
                            _ApplyMDL.GetDateTime = null;
                            _ApplyMDL.GetMan = null;
                            _ApplyMDL.GetResult = null;
                            _ApplyMDL.GetRemark = null;

                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                            break;
                        case EnumManager.ApplyStatus.已受理:
                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                    }

                    break;
                case EnumManager.ApplyStatus.已上报:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，上报时间：{3}，上报人：{4}，上报编号：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.ReportDate, _ApplyMDL.ReportMan, _ApplyMDL.ReportCode, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ApplyStatus.已申报:
                            _ApplyMDL.GetDateTime = null;
                            _ApplyMDL.GetMan = null;
                            _ApplyMDL.GetResult = null;
                            _ApplyMDL.GetRemark = null;

                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                            break;
                        case EnumManager.ApplyStatus.已受理:
                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                        case EnumManager.ApplyStatus.区县审查:
                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.区县审查;
                            break;
                    }
                    break;
                case EnumManager.ApplyStatus.已审查:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，审查时间：{3}，审查结果：{4}，审查意见：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.CheckDate, _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ApplyStatus.已申报:
                            _ApplyMDL.GetDateTime = null;
                            _ApplyMDL.GetMan = null;
                            _ApplyMDL.GetResult = null;
                            _ApplyMDL.GetRemark = null;

                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                            break;
                        case EnumManager.ApplyStatus.已受理:
                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                        case EnumManager.ApplyStatus.区县审查:
                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.区县审查;
                            break;
                        case EnumManager.ApplyStatus.已上报:

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已上报;
                            break;
                    }
                    break;
                case EnumManager.ApplyStatus.已公告:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，决定时间：{3}，决定结果：{4}。后退到“{5}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.ConfirmDate, _ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ApplyStatus.已申报:
                            _ApplyMDL.GetDateTime = null;
                            _ApplyMDL.GetMan = null;
                            _ApplyMDL.GetResult = null;
                            _ApplyMDL.GetRemark = null;

                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                            break;
                        case EnumManager.ApplyStatus.已受理:
                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;

                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                        case EnumManager.ApplyStatus.区县审查:
                            _ApplyMDL.ReportDate = null;
                            _ApplyMDL.ReportMan = null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.区县审查;
                            break;
                        case EnumManager.ApplyStatus.已上报:
                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已上报;
                            break;
                        case EnumManager.ApplyStatus.已审查:
                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已审查;
                            break;
                    }
                    break;               
                default:
                    UIHelp.layerAlert(Page, "当前处在无法后退节点！");
                    return;
            }

            try
            {
                _ApplyMDL.XGSJ = DateTime.Now;
                _ApplyMDL.XGR = UserName;
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "后退二级建造师初始注册审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退二级建造师初始注册审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }

        //变换是否强制执行
        protected void RadioButtonList_enforce_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (RadioButtonList_enforce.SelectedValue == "申请强制执行")
            {
                RadioButtonListCancelReason.Visible = false;
                if (PersonType == 2)
                {
                    RadioButtonWorkerEnforceApply.Visible = true;
                }
                if (PersonType == 3)
                {
                    RadioButtonUnitEnforceApply.Visible = true;
                }
                ButtonUnit.Text = "提交建委审核";
            }
            else
            {
                RadioButtonListCancelReason.Visible = true;
                RadioButtonWorkerEnforceApply.Visible = false;
                RadioButtonUnitEnforceApply.Visible = false;
                ButtonUnit.Text = "提交单位确认";
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

        }
    }
}