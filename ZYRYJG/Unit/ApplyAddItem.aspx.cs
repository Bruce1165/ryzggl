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
    public partial class ApplyAddItem : BasePage
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
        /// 我的证书
        /// </summary>
        protected COC_TOW_Person_BaseInfoMDL myCert
        {
            get{
                return (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
       

              
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    ApplyMDL _ApplyMDL = ApplyDAL.GetObject(_ApplyID);
                    ViewState["ApplyMDL"] = _ApplyMDL;

                    ApplyAddItemMDL _ApplyAddItemMDL = ApplyAddItemDAL.GetObject(_ApplyID);
                    ViewState["ApplyAddItemMDL"] = _ApplyAddItemMDL;                                      

                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;

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

                        if (IfExistRoleID("0"))
                        {
                            UIHelp.layerAlert(Page, string.Format("提示您，上次注册申请审核未通过的原因是：【{0}】请您在本次申请提交时，注意核对填报内容是否按照要求修改完成。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                        }
                        else if (IfExistRoleID("2") == false)
                        {
                            UIHelp.layerAlert(Page, string.Format("提示审核人员：该申请人一年内第二次提交注册申请，上次不通过的原因是：【{0}】请注意核对，如存在问题请及时驳回申请。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                        }
                    }

                    //if (_ApplyMDL.ENT_ContractType.HasValue)
                    //{
                    //    RadioButtonListENT_ContractType.Items.FindByValue(_ApplyMDL.ENT_ContractType.ToString()).Selected = true;

                    //    if (RadioButtonListENT_ContractType.SelectedValue == "1")
                    //    {
                    //        RadDatePickerENT_ContractENDTime.Style.Remove("display");
                    //        LabelJZSJ.Style.Remove("display");
                    //        ValidatorENT_ContractENDTime.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
                    //        LabelJZSJ.Style.Add("display", "none");
                    //        ValidatorENT_ContractENDTime.Enabled = false;
                    //    }
                    //}

                    //个人登录
                    if (IfExistRoleID("0") == true  //原来的2改为0  修改人：南静   2019-10-23
                      && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)//2019-10-23  南静添加 待确认判断条件
                      )
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);//基本信息    true改为false，修改人：南静  2010-10-23
                        UIHelp.SetData(EditTable, _ApplyAddItemMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        UIHelp.SetReadOnly(RadComboBoxNation, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        //UIHelp.SetReadOnly(RadDatePickerENT_ContractStartTime, false);
                        //UIHelp.SetReadOnly(RadDatePickerENT_ContractENDTime, false);
                      
                        //trFuJanTitel.Visible = true;
                        //trFuJan.Visible = true;
                        divGR.Visible = true;//个人操作按钮 //2019-10-23  南静添加
                    }
                    else
                    {
                        //RadioButtonListENT_ContractType.Enabled = false;
                        //RadDatePickerENT_ContractENDTime.Enabled = false;
                        //RadDatePickerENT_ContractStartTime.Enabled = false;
                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyAddItemMDL, true);

                        if (IfExistRoleID("0") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已公示)
                        {
                            divGR.Visible = true;//个人操作按钮
                        }
                        if (IfExistRoleID("6") == true && string.IsNullOrEmpty(_ApplyMDL.ConfirmResult) == true)
                        {
                            TextBoxConfirmRemark.Text = "决定通过";
                        }
                    }

                    SetButtonEnable(_ApplyMDL.ApplyStatus);
                    //附件
                    BindFile(_ApplyMDL.ApplyID);
                    //审批记录
                    BindCheckHistory(_ApplyAddItemMDL.ApplyID);
                    //2019-10-23   南静添加
                    if (IfExistRoleID("2") == true)//企业
                    {
                        if (string.IsNullOrEmpty(Request["a"]) == false)//edit
                        {
                            if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                            {
                                ButtonApply.Text = "取消申报";
                                divUnit.Visible = true;
                            }
                            if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认)
                            {
                                TextBoxOldUnitCheckRemark.Text = "提交审核";
                                UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                divUnit.Visible = true;
                            }
                        }
                        //企业看不到各级申办人列
                        RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    }

                    #region   各种权限登录

                    //是否为查看
                    bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;

                    //区县受理权限
                    if (IfExistRoleID("3") == true && ViewLimit == false)
                    {
                        divQX.Visible = true;

                    }
                    //区县审查
                    if (IfExistRoleID("7") == true && ViewLimit == false)
                    {
                        divQXCK.Visible = true;

                    }
                    //建委审核权限
                    if (IfExistRoleID("4") == true && ViewLimit == false)
                    {
                        divCheck.Visible = true;

                    }
                    //建委领导审核权限
                    if (IfExistRoleID("6") == true && ViewLimit == false)
                    {
                        divDecide.Visible = true;
                        UIHelp.SetReadOnly(TextBoxConfirmRemark, false);
                    }

                    if (ViewLimit == true)//查看
                    {
                        divViewAction.Visible = true;
                    }

                    #endregion
                }
                else//新增
                {
                    //二建人员表
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount>0)
                    //{
                    //    UIHelp.layerAlert(Page, "企业信息中的监管区县与建造师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                    //    return;
                    //}

                    //#endregion

                    #region 查询证书是否锁定
                    bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(o.PSN_ServerID);
                    if (IfLock == true)
                    {
                        UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                        return;
                    }
                    #endregion

                    #region 查询证书是否打印
                    //南静注释   合并后不需要此判断   2019-11-12
                    //int sum = DataAccess.COC_TOW_Person_BaseInfoDAL.PrintIsorNot(o.PSN_CertificateNO);
                    //if (sum > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "上次业务办理证书尚未打印，请先打印证书！", 5, 0);
                    //    return;
                    //}
                    #endregion

                    if (IfExistRoleID("0") == true)//个人登录后  2019-10-23   南静添加
                    {
                    if (o.PSN_CertificateValidity.HasValue == true && o.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                        SetButtonEnable("禁用");
                    }
                    else
                    {
                        SetButtonEnable("");
                    }

                    Tablezjhm.Visible = true;
                   
                    if (o != null)
                    {
                        //RadioButtonListENT_ContractType.SelectedIndex = 0;
                       
                        UIHelp.SetData(EditTable, o, true);
                        UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        //企业信息
                        UnitMDL _UnitMDL = UnitDAL.GetObject(o.ENT_ServerID);
                        if (_UnitMDL != null)
                        {
                            RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                            UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                       
                           
                        }
                        EditTable.Visible = false;//南静添加   2019-10-23
                        //divQY.Visible = false;//南静注释   2019-10-23
                        divGR.Visible = false;

                        //附件
                        BindFile("0");

                        //可增项专业列表
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //已注册专业集合
                        DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append("'").Append(dt.Rows[i]["PRO_Profession"].ToString()).Append("'").Append(",");
                            if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                            if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");
                        }
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }                         

                        //根据身份证检索资格库校验数据,且不能出来自己已经注册过的专业了

                        string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ]
			FROM dbo.Qualification
			WHERE ZJHM = '{0}'  AND ZYLB NOT IN ({1}) ";
                        DataTable dtzy = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO, sb.ToString()));
                        RadGridHZSB.DataSource = dtzy;
                        RadGridHZSB.DataBind();

                        if (dtzy == null || dtzy.Rows.Count == 0)
                        {
                            // divQY.Visible = false;//南静注释   2019-10-23
                            divGR.Visible = false;//南静添加   2019-10-23
                            UIHelp.layerAlert(Page, "没有可用于增项的专业！",0,4000);
                        }
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
            //UIHelp.SetReadOnly(RadComboBoxAddItem1, false);
            //UIHelp.SetReadOnly(RadComboBoxAddItem2, false);
            //UIHelp.SetReadOnly(RadDatePickerExamDate1, false);
            //UIHelp.SetReadOnly(RadDatePickerExamDate2, false);
            //UIHelp.SetReadOnly(RadTextBoxExamCode1, true);
            //UIHelp.SetReadOnly(RadTextBoxExamCode2, true);
            //UIHelp.SetReadOnly(RadNumericTextBoxBiXiu1, true);
            //UIHelp.SetReadOnly(RadNumericTextBoxXuanXiu1, true);
            //UIHelp.SetReadOnly(RadNumericTextBoxBiXiu2, true);
            //UIHelp.SetReadOnly(RadNumericTextBoxXuanXiu2, true);
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

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 有效校验

            //if (RadDatePickerENT_ContractStartTime.SelectedDate.HasValue == false)
            //{
            //    UIHelp.layerAlert(Page, "请输入劳动合同开始时间", 5, 0);
            //    return;
            //}
            //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == false)
            //{
            //    UIHelp.layerAlert(Page, "请输入劳动合同结束时间", 5, 0);
            //    return;
            //}
            //if (string.IsNullOrEmpty(RadioButtonListENT_ContractType.SelectedValue))
            //{
            //    UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
            //    return;
            //}
            //else if (RadioButtonListENT_ContractType.SelectedValue != "1" && RadioButtonListENT_ContractType.SelectedValue != "2" && RadioButtonListENT_ContractType.SelectedValue != "3")
            //{
            //    UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
            //    return;
            //}
           
            if (RadTextBoxENT_Telephone.Text.Trim() == "")
            {
                UIHelp.layerAlert(Page, "保存失败，联系电话不能为空！", 5, 0);
                return;
            }

            if (RadTextBoxPSN_MobilePhone.Text.Trim() == "" || !Check.IfTelPhoneFormat(RadTextBoxPSN_MobilePhone.Text.Trim()))
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

            if (Utility.Check.CheckBirthdayLimit(0, RadTextBoxPSN_CertificateNO.Text.Trim(),DateTime.Now,"男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满65周岁前90天起,不再允许发起二级建造师初始、重新、延续、增项、执业企业变更注册申请。", 5, 0);
                return;
            }

            ////企业信息  
            //UnitMDL _unit = DataAccess.UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxENT_OrganizationsCode.Text.Trim());
            //if (_unit.ResultGSXX == 0 || _unit.ResultGSXX == 1)
            //{
            //    UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", _unit.ENT_Name));
            //    return;
            //}

            if (UnitDAL.CheckGongShang(RadTextBoxENT_OrganizationsCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxENT_Name.Text));
                return;
            }

            //查询证书是否锁定
            bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(myCert.PSN_ServerID);
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
            }       

            #endregion

            if (ViewState["ApplyMDL"] == null)
            { 
                if (ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_RegisterNO.Text.Trim(), EnumManager.ApplyStatus.未申报, EnumManager.ApplyStatus.待确认)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }

            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表
            UIHelp.GetData(EditTable, _ApplyMDL);
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            _ApplyMDL.Valid = 1;
            _ApplyMDL.ApplyType = EnumManager.ApplyType.增项注册;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
            ApplyAddItemMDL _ApplyAddItemMDL = ViewState["ApplyAddItemMDL"] == null ? new ApplyAddItemMDL() : (ApplyAddItemMDL)ViewState["ApplyAddItemMDL"];//详细表
            //_ApplyMDL.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);
            ////劳动合同结束时间
            //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
            //{
            //    _ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
            //}
            //else
            //{
            //    _ApplyMDL.ENT_ContractENDTime = null;
            //}

            UIHelp.GetData(EditTable, _ApplyAddItemMDL);

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "增项注册");
                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;

                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                    _ApplyMDL.ENT_ServerID = person.ENT_ServerID;
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;
                    //申请专业
                    _ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;
                    _ApplyAddItemMDL.ApplyID = _ApplyMDL.ApplyID;

                    //所属区县
                    if (string.IsNullOrEmpty(_ApplyMDL.ENT_City))
                    {
                        UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                        return;
                    }

                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyAddItemDAL.Insert(tran, _ApplyAddItemMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.执业资格证书扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.增项注册告知承诺书扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

                    //trFuJanTitel.Visible = true;
                    //trFuJan.Visible = true;
                }
                else//update
                {
                    ApplyDAL.Update(tran, _ApplyMDL);
                    ApplyAddItemDAL.Update(tran, _ApplyAddItemMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyAddItemMDL"] = _ApplyAddItemMDL;
                BindFile(ApplyID);
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelAddItem1.Text+','+LabelAddItem2.Text));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存增项申请表信息失败", ex);
            }
        }

        //南静添加   2019-10-23   提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            ApplyAddItemMDL _ApplyAddItemMDL = (ApplyAddItemMDL)ViewState["ApplyAddItemMDL"];
            //ApplyRenewMDL _ApplyRenewMDL = (ApplyRenewMDL)ViewState["ApplyRenewMDL"];
            try
            {
                //if (ButtonApply.Text != "取消申报")//南静注释 2019-10-18   判断按钮不对
                if (ButtonUnit.Text != "取消申报")
                {
                    //bool ifNeedJXJY = false;//是否需要上传继续教育
                    //必须上传附件集合
                    System.Collections.Hashtable fj;
                    if (
                        (_ApplyAddItemMDL.ExamDate1.HasValue == true && _ApplyAddItemMDL.ExamDate1.Value.AddYears(3) < DateTime.Now)
                        || (_ApplyAddItemMDL.ExamDate2.HasValue == true && _ApplyAddItemMDL.ExamDate2.Value.AddYears(3) < DateTime.Now)
                        )
                    {//需要继续教育证明

                        //必须上传附件集合
                        fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.一寸免冠照片,0},
                {EnumManager.FileDataTypeName.证件扫描件,0},
                //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                {EnumManager.FileDataTypeName.增项注册告知承诺书,0},
                //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                 //{EnumManager.FileDataTypeName.继续教育证明扫描件,0},
                {EnumManager.FileDataTypeName.申请表扫描件,0}};
                    }
                    else
                    {

                        //必须上传附件集合
                        fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.一寸免冠照片,0},
                {EnumManager.FileDataTypeName.证件扫描件,0},
                //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                {EnumManager.FileDataTypeName.增项注册告知承诺书,0},
                //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                {EnumManager.FileDataTypeName.申请表扫描件,0}
                  };

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
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！", sb), 5, 0);
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
                //if (ButtonApply.Text == "取消申报")//南静注释 2019-10-18   判断按钮不对
                if (ButtonUnit.Text == "取消申报")
                {
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                    _ApplyMDL.ApplyTime = null;

                    //南静  2019-10-18  添加
                    _ApplyMDL.OldUnitCheckResult = null;
                    _ApplyMDL.OldUnitCheckRemark = null;
                    _ApplyMDL.OldUnitCheckTime = null;
                }
                else
                {
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
                    _ApplyMDL.ApplyTime = DateTime.Now;

                    //南静  2019-10-18  添加
                    _ApplyMDL.OldUnitCheckResult = null;
                    _ApplyMDL.OldUnitCheckRemark = null;
                    _ApplyMDL.OldUnitCheckTime = null;

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
                    _ApplyMDL.ConfirmRemark = null;
                }
                ApplyDAL.Update(_ApplyMDL);
                ViewState["ApplyMDL"] = _ApplyMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                //人员照片
                BindFile(_ApplyMDL.ApplyID);
                if (_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请提交成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));

                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID)); 
              
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "增项注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认，申报 or 撤销申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            //南静注释  2019-10-23
            //ApplyAddItemMDL _ApplyAddItemMDL = (ApplyAddItemMDL)ViewState["ApplyAddItemMDL"];
            
            //if (ButtonApply.Text != "撤销申报")
            //{
            //    //必须上传附件集合
            //    System.Collections.Hashtable fj;
            //    if (
            //        (_ApplyAddItemMDL.ExamDate1.HasValue == true && _ApplyAddItemMDL.ExamDate1.Value.AddYears(3) < DateTime.Now)
            //        || (_ApplyAddItemMDL.ExamDate2.HasValue == true && _ApplyAddItemMDL.ExamDate2.Value.AddYears(3) < DateTime.Now)
            //        )
            //    {//需要继续教育证明

            //        fj = new System.Collections.Hashtable{
            //    {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //    {EnumManager.FileDataTypeName.证件扫描件,0},
            //    {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //    {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //     {EnumManager.FileDataTypeName.继续教育证明扫描件,0},
            //    {EnumManager.FileDataTypeName.申请表扫描件,0}
            //};
            //    }
            //    else
            //    {
            //        fj = new System.Collections.Hashtable{
            //    {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //    {EnumManager.FileDataTypeName.证件扫描件,0},
            //    {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //    {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //    {EnumManager.FileDataTypeName.申请表扫描件,0}
            //      };
            //    }

            //    //已上传附件集合
            //    DataTable dt = ApplyDAL.GetApplyFile(_ApplyMDL.ApplyID);

            //    //计数
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        if (fj.ContainsKey(r["DataType"].ToString()) == true)
            //        {
            //            fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
            //        }
            //    }
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    foreach (string k in fj.Keys)
            //    {
            //        if (Convert.ToInt32(fj[k]) == 0)
            //        {
            //            sb.Append(string.Format("、“{0}”", k));
            //        }
            //    }

            //    if (sb.Length > 0)
            //    {
            //        sb.Remove(0, 1);
            //        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
            //        return;
            //    }
            //}
            try 
	{	        
	
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            //if (ButtonApply.Text == "撤销申报")  //南静  2019-10-22   注释
            if (ButtonApply.Text == "取消申报")
            {
                //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                //_ApplyMDL.ApplyTime = null;
                //南静  2019-10-23  添加
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
            }
            else
            {
                //南静  2019-10-23  添加
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                {
                    TextBoxOldUnitCheckRemark.Visible = true;
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                }
                else{
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;

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
                _ApplyMDL.ConfirmMan = null;}
            }
            ApplyDAL.Update(_ApplyMDL);
            ViewState["ApplyMDL"] = _ApplyMDL;
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            //人员照片
            BindFile(_ApplyMDL.ApplyID);
            if (_ApplyMDL.ApplyStatus == "已申报")
            {
                UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelAddItem1.Text + ',' + LabelAddItem2.Text));
                //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
                //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
            else
            {
                UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelAddItem1.Text + ',' + LabelAddItem2.Text));
                UIHelp.layerAlert(Page, "退回个人成功！", 6, 3000);              
            }

    }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "增项注册申报失败！", ex);
            }


            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            ApplyDAL.Delete(_ApplyMDL.ApplyID);
            ViewState.Remove("ApplyMDL");
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelAddItem1.Text + ',' + LabelAddItem2.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"]=step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace(" green", "");//南静添加  2019-10-23
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            //step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            //step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
            //step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");
            step_已审查.Attributes["class"] = step_已审查.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
            //step_已公示.Attributes["class"] = step_已公示.Attributes["class"].Replace(" green", "");
            step_已公告.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {
                case "未申报":
                case "已驳回":
                    step_未申报.Attributes["class"] += " green";
                    break;
                //2019-10-23     南静添加
                case "待确认":
                    step_待确认.Attributes["class"] += " green";
                    break;
                case "已申报":
                    step_已申报.Attributes["class"] += " green";
                    break;
                //case "已受理":
                //    step_已受理.Attributes["class"] += " green";
                //    break;
                //case "区县审查":
                //     step_区县审查.Attributes["class"] += " green";
                //    break;
                //case "已上报":
                //    step_已上报.Attributes["class"] += " green";
                //    break;
                case "已审查":
                    step_已审查.Attributes["class"] += " green";
                    break;
                case "已决定":
                    step_已决定.Attributes["class"] += " green";
                    break;
                //case "已公示":
                //    step_已公示.Attributes["class"] += " green";
                //    break;
                case "已公告":
                    step_已公告.Attributes["class"] += " green";
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(string ApplyStatus)
        {
            SetStep(ApplyStatus);

            switch (ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    //2019-10-23   南静注释
                    //ButtonApply.Enabled = false;
                    //ButtonApply.Text = "申 报"; 
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    //2019-10-23   南静注释
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "提交单位确认";
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }
                    break;
                //2019-10-23   南静添加   待确认
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
                    //ButtonSave.Enabled = false;
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "撤销申报";
                    //ButtonDelete.Enabled = false;
                    //ButtonOutput.Enabled = false;
                    //break;
                    //2019-10-23   南静修改
                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";

                    //个人
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    break;
                case EnumManager.ApplyStatus.已驳回:
                    //ButtonSave.Enabled = true;
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "申 报";
                    //ButtonDelete.Enabled = true;
                    //ButtonOutput.Enabled = true;
                    //2019-10-23   南静修改
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";
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
                case "禁用":
                    ButtonSave.Enabled = false;
                    ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    //2019-10-23   南静注释
                    //ButtonApply.Enabled = false;
                    //ButtonApply.Text = "申 报"; 
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.已公示:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "取消申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;

                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
            }
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";//2019-10-23   南静添加
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
                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师增项注册申请表.docx");
                string fileName = "北京市二级注册建造师增项注册申请表";
                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                ApplyAddItemMDL _ApplyAddItemMDL = ViewState["ApplyAddItemMDL"] as ApplyAddItemMDL;
                COC_TOW_Person_BaseInfoMDL person = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                

                //COC_TOW_Person_BaseInfoMDL person = ViewState["COC_TOW_Person_BaseInfoMDL"] as COC_TOW_Person_BaseInfoMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                UnitMDL _UnitMDL = UnitDAL.GetObject(_ApplyMDL.ENT_ServerID);
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_UnitMDL);
                o.Add(_ApplyAddItemMDL);
                var ht = PrintDocument.GetProperties(o);
                ht["Birthday"] = ((DateTime)person.PSN_BirthDate).ToString("yyyy-MM-dd");
               // ht["Nation"] = person.PSN_National;
                ht["FR"] = _UnitMDL.ENT_Corporate;
                ht["LinkMan"] = _UnitMDL.ENT_Contact;
                if (_FileInfoMDL != null)
                {
                    ht["photo"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else { ht["photo"] = ""; }
                if (ht["photo"] == "")
                {
                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
                    return;
                }
                //2019-11-27南静添加
                switch (_ApplyMDL.ENT_ContractType)
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
                    default:
                        ht["ENT_ContractType1"] = "☑";
                        ht["ENT_ContractType2"] = "□";
                        ht["ENT_ContractType3"] = "□";
                        break;
                }
                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["ENT_ContractStartTime"] = _ApplyMDL.ENT_ContractStartTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractStartTime).ToString("yyyy年MM月dd日");
                ht["ENT_ContractENDTime"] = _ApplyMDL.ENT_ContractENDTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractENDTime).ToString("yyyy年MM月dd日");
                ht["PSN_CertificateValidity1"] = _ApplyAddItemMDL.PSN_CertificateValidity1 == null ? "" : ((DateTime)_ApplyAddItemMDL.PSN_CertificateValidity1).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity2"] = _ApplyAddItemMDL.PSN_CertificateValidity2 == null ? "" : ((DateTime)_ApplyAddItemMDL.PSN_CertificateValidity2).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity3"] = _ApplyAddItemMDL.PSN_CertificateValidity3 == null ? "" : ((DateTime)_ApplyAddItemMDL.PSN_CertificateValidity3).ToString("yyyy-MM-dd");
                ht["ExamDate1"] = _ApplyAddItemMDL.ExamDate1 == null ? "" : ((DateTime)_ApplyAddItemMDL.ExamDate1).ToString("yyyy-MM-dd");
                ht["ExamDate2"] = _ApplyAddItemMDL.ExamDate2 == null ? "" : ((DateTime)_ApplyAddItemMDL.ExamDate2).ToString("yyyy-MM-dd");
                //证件类型
                //string ZjType = ht["PSN_CertificateType"].ToString();
                //ht["SFZ"] = ZjType == "身份证" ? "☑" : "□";
                //ht["JGZ"] = ZjType == "军官证" ? "☑" : "□";
                //ht["JG"] = ZjType == "警官证" ? "☑" : "□";
                //ht["HZ"] = ZjType == "护照" ? "☑" : "□";
                ////将身份证拆开，小于18位的后三位用空补齐
                //string PSN_CertificateNO = ht["PSN_CertificateNO"].ToString();
                //char[] a = PSN_CertificateNO.ToCharArray();
                //if (a.Length < 17)
                //{
                //    a[15] = Convert.ToChar("");
                //    a[16] = Convert.ToChar("");
                //    a[17] = Convert.ToChar("");
                //}
                //for (int i = 0; i < a.Length; i++)
                //{
                //    ht["Sfz" + i + ""] = a[i];
                //}
                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
               
            }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印增项申请Word失败！", ex);
            }

        }

        //区县受理（作废）
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            //if (RadioButtonListApplyStatus.SelectedValue == "通过")
            //{
            //    bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            //    if (IfLock == true)
            //    {
            //        UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //        return;
            //    }
            //}
           
            //_ApplyMDL.GetDateTime = DateTime.Now;
            //_ApplyMDL.GetMan = UserName;
            //_ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            //_ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            //_ApplyMDL.XGSJ = _ApplyMDL.GetDateTime;
            //_ApplyMDL.XGR = UserName;
            //_ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            //if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            //{
            //    _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            //}
            //try
            //{
            //    ApplyDAL.Update(_ApplyMDL);
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "受理失败！", ex);
            //    return;
            //}

            //UIHelp.WriteOperateLog(UserName, UserID, "增项注册区县受理", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelAddItem1.Text + ',' + LabelAddItem2.Text
            //     , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
            ////UIHelp.ParentAlert(Page, "受理成功！", true);
            //string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);

            ////Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
        }

        //区县审查（作废）
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            ////移动到汇总上报时校验
            ////if (_ApplyMDL.CheckXSL.HasValue == false || _ApplyMDL.CheckXSL.Value == 1)
            ////{
            ////    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
            ////    return;
            ////}
            //#region 查询证书是否锁定
            //if (RadioButtonListExamineResult.SelectedValue == "通过")
            //{
            //    bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            //    if (IfLock == true)
            //    {
            //        UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //        return;
            //    }
            //}
            //#endregion
            //_ApplyMDL.ExamineDatetime = DateTime.Now;
            //_ApplyMDL.ExamineMan = UserName;
            //_ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
            //_ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            //_ApplyMDL.ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;
            //_ApplyMDL.XGSJ = _ApplyMDL.ExamineDatetime;
            //_ApplyMDL.XGR = UserName;
            //if (RadioButtonListExamineResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
            //{
            //    _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.ExamineDatetime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxExamineRemark1.Text.Trim());
            //}

            //try
            //{
            //    ApplyDAL.Update(_ApplyMDL);
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(UserName, UserID, "增项注册区县审查", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelAddItem1.Text + ',' + LabelAddItem2.Text
            //     , _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
            //UIHelp.ParentAlert(Page, "区县审查成功！", true);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            if(Convert.ToDateTime(_ApplyMDL.OldUnitCheckTime.Value.AddDays(1).ToShortDateString()) > DateTime.Now)
            {
                UIHelp.layerAlert(Page, "不允许审核当天企业提交的申请，请隔日再审批。");
                return;
            }
            if (RadioButtonListCheckResult.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            _ApplyMDL.CheckDate = _ApplyMDL.OldUnitCheckTime.Value.AddDays(1);
            _ApplyMDL.CheckMan = UserName;
            _ApplyMDL.CheckResult = RadioButtonListCheckResult.SelectedValue;
            _ApplyMDL.CheckRemark = TextBoxApplyCheckRemark.Text.Trim();
            //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已审查 ;
            _ApplyMDL.ApplyStatus = RadioButtonListCheckResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.已审查 : EnumManager.ApplyStatus.已驳回;
            if (RadioButtonListCheckResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}市级驳回申请，驳回说明：{1}", _ApplyMDL.CheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), _ApplyMDL.CheckRemark);
            }

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "审核失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "增项注册建委审核", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelAddItem1.Text + ',' + LabelAddItem2.Text
                 , _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //专业局会审
        protected void ButtonOtherDeptCheck_Click(object sender, EventArgs e)
        {
            //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            //bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            //if (IfLock == true)
            //{
            //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            ////_ApplyMDL.OtherDeptCheckDate = DateTime.Now;
            ////会审时不设置时间，回传是统一设置时间
            //_ApplyMDL.OtherDeptCheckMan = UserName;
            //_ApplyMDL.OtherDeptCheckResult = RadioButtonListOtherDeptCheckResult.SelectedValue;
            //_ApplyMDL.OtherDeptCheckRemark = TextBoxApplyOtherDeptCheckRemark.Text.Trim();

            //try
            //{
            //    ApplyDAL.Update(_ApplyMDL);
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "会审失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(UserName, UserID, "增项注册专业局会审成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelAddItem1.Text + ',' + LabelAddItem2.Text));
            //UIHelp.ParentAlert(Page, "会审成功！", true);
        }

        //建委领导决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            if (RadioButtonListDecide.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }

            string sql = "";
            DateTime doTime = DateTime.Now;//处理时间

            _ApplyMDL.ConfirmDate = _ApplyMDL.OldUnitCheckTime.Value.AddDays(1).AddHours(1);
            _ApplyMDL.ConfirmMan = UserName;
            _ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _ApplyMDL.ConfirmRemark = TextBoxConfirmRemark.Text.Trim();
            _ApplyMDL.NoticeDate = doTime;
            _ApplyMDL.NoticeMan = UserName;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已公告;

            //2025-6-4修改未决定即刻公告。

            if (RadioButtonListDecide.SelectedValue == "通过")
            {
                #region 更新证书表

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    ApplyDAL.Update(tran, _ApplyMDL);

                    //专业写入历史表
                    sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 	
                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
                                where a.ApplyID='{2}' and a.ConfirmResult='通过'";

                    CommonDAL.ExecSQL(tran, string.Format(sql, "增项注册", doTime, _ApplyMDL.ApplyID));

                    //删除过期的增项专业
                    sql = @"DELETE FROM [dbo].[COC_TOW_Register_Profession]
                                WHERE [PRO_ServerID] in
                                (
                                    SELECT  p.[PRO_ServerID]
                                    FROM [dbo].[Apply] a 
                                    inner join [dbo].[ApplyAddItem] b on a.ApplyID = b.ApplyID
                                    inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID and (p.PRO_Profession = b.[AddItem1] or p.PRO_Profession = b.[AddItem2])
                                    where  a.ApplyID='{0}' and a.ConfirmResult='通过'
                                )";

                    CommonDAL.ExecSQL(tran, string.Format(sql, _ApplyMDL.ApplyID));

                    //写入增项专业表
                    sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code],[LastModifyTime])
                                  select newid(),a.PSN_ServerID,ZY,convert(char(10),a.[ConfirmDate],120),[dbo].[GET_PSN_CertificateValidity](convert(char(10),dateadd(day,-1,dateadd(year,3,a.[ConfirmDate])),120),a.PSN_CertificateNO,a.PSN_Level),'110000','{1}'
                                  FROM [dbo].[Apply] a 
                                  inner join
                                  (
                                      select applyid,[AddItem1] ZY from  [dbo].[ApplyAddItem] where [AddItem1] is not null
                                      union
                                      select applyid,[AddItem2] ZY from  [dbo].[ApplyAddItem] where [AddItem2] is not null
                                  ) z on  a.applyid = z.applyid
                                   where a.ApplyID='{0}' and a.ConfirmResult='通过'";
                    CommonDAL.ExecSQL(tran, string.Format(sql, _ApplyMDL.ApplyID, doTime));

                    //人员表写入历史
                    sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where ApplyID='{0}' and ConfirmResult='通过')";

                    CommonDAL.ExecSQL(tran, string.Format(sql, _ApplyMDL.ApplyID));

                    //更新人员正式表
                    sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set 
                                    [COC_TOW_Person_BaseInfo].PSN_RegisteProfession =
		                                    replace(','+
		                                    (
		                                    select ',' +[COC_TOW_Register_Profession].PRO_Profession from [dbo].[COC_TOW_Register_Profession]
		                                     where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                     for xml path('')
		                                    ),',,','')
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
		                                    (
		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                    )               
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = a.[ConfirmDate]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='04'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_AddProfession]=                                   
		                                    (
		                                        select [ApplyAddItem].AddItem1 + case when [ApplyAddItem].AddItem2 is null then '' else ',' +[ApplyAddItem].AddItem2 end
                                                from [dbo].[ApplyAddItem]
		                                        where [ApplyAddItem].Applyid = a.Applyid		                                   
		                                    )
                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                where a.ApplyID='{2}' and a.ConfirmResult='通过'";


                    CommonDAL.ExecSQL(tran, string.Format(sql
                        , doTime
                        , UserName
                        , _ApplyMDL.ApplyID));

                    //更新证书附件中需要被覆盖的附件为历史附件
                    CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID='{0}' and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID='{0}' and [Apply].ConfirmResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", _ApplyMDL.ApplyID));


                    CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID='{0}' and [Apply].ConfirmResult='通过' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID='{0}' and [Apply].ConfirmResult='通过' 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", _ApplyMDL.ApplyID));

                    //将申请单附件写入证书附件库
                    CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ApplyID='{0}' and [Apply].ConfirmResult='通过' ", _ApplyMDL.ApplyID));
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "二建增项决定失败", ex);
                    return;
                }

                #endregion
            }
            else
            {
                try
                {
                    ApplyDAL.Update(_ApplyMDL);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "二建增项决定失败！", ex);
                    return;
                }
            }


            UIHelp.WriteOperateLog(UserName, UserID, string.Format("增项注册建委决定{0}", _ApplyMDL.ConfirmResult), string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelAddItem1.Text + ',' + LabelAddItem2.Text));
            UIHelp.ParentAlert(Page, "决定成功！", true);
        }
        ////查询
        //protected void ButtonQuery_Click(object sender, EventArgs e)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    var o = ViewState["COC_TOW_Person_BaseInfoMDL"] as COC_TOW_Person_BaseInfoMDL;
            
        //    //已注册专业集合
        //    DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sb.Append("'").Append(dt.Rows[i]["PRO_Profession"].ToString()).Append("'").Append(",");
        //    }
        //    if (sb.Length > 0)
        //    {
        //        sb.Remove(sb.Length-1, 1);
        //    }
        //    var a = RadTextBoxzgzh.Text;
        //    //根据身份证检索资格库校验数据,且不能出来自己已经注册过的专业了
        //    List<QualificationMDL> dtt = QualificationDAL.GetAddItemObjectList(RadTextBoxzgzh.Text,sb.ToString());
        //    RadGridHZSB.DataSource = dtt;
        //    RadGridHZSB.DataBind();
        //}
        //确定
        protected void ButtonSaveZjh_Click(object sender, EventArgs e)
        {
            List<QualificationMDL> _ListQualificationMDL = ListQualificationMDL();
            if (_ListQualificationMDL.Count > 0)
            {
                for (int i = 0; i < _ListQualificationMDL.Count; i++)
                {
                    string bxxs = "", xxxs = "";
                    //校验继续教育情况，是否满足最近三年继续教育情况
                    if (Convert.ToDateTime(_ListQualificationMDL[i].QFSJ).AddYears(3) < DateTime.Now)
                    {
                        bxxs = "60";
                        xxxs = "60";
                    }
                    (EditTable.FindControl("LabelAddItem" + (i+1).ToString()) as Label).Text = _ListQualificationMDL[i].ZYLB;
                    (EditTable.FindControl("LabelExamCode" + (i + 1).ToString()) as Label).Text = _ListQualificationMDL[i].ZGZSBH;
                    (EditTable.FindControl("LabelExamDate" + (i + 1).ToString()) as Label).Text = _ListQualificationMDL[i].QFSJ.Value.ToString("yyyy-MM-dd");
                    (EditTable.FindControl("LabelBiXiu" + (i + 1).ToString()) as Label).Text = bxxs;
                    (EditTable.FindControl("LabelXuanXiu" + (i + 1).ToString()) as Label).Text =xxxs;
                }
              
                Tablezjhm.Visible = false;
                EditTable.Visible = true;
                divGR.Visible = true;
            }
            else
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何专业！");
                return;
            }
        }
        //获取表格勾选集合,并返回一个资格库集合
        private List<QualificationMDL> ListQualificationMDL()
        {
            List<QualificationMDL> _ListQualificationMDL = new List<QualificationMDL>();
            for (int i = 0; i < RadGridHZSB.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridHZSB.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    QualificationMDL _QualificationMDL = new QualificationMDL();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["XM"] != null) _QualificationMDL.XM = RadGridHZSB.MasterTableView.DataKeyValues[i]["XM"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["ZJHM"] != null) _QualificationMDL.ZJHM = RadGridHZSB.MasterTableView.DataKeyValues[i]["ZJHM"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["ZYLB"] != null) _QualificationMDL.ZYLB = RadGridHZSB.MasterTableView.DataKeyValues[i]["ZYLB"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["ZGZSBH"] != null) _QualificationMDL.ZGZSBH = RadGridHZSB.MasterTableView.DataKeyValues[i]["ZGZSBH"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["QDNF"] != null) _QualificationMDL.QDNF = RadGridHZSB.MasterTableView.DataKeyValues[i]["QDNF"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["QFSJ"] != null) _QualificationMDL.QFSJ = Convert.ToDateTime(RadGridHZSB.MasterTableView.DataKeyValues[i]["QFSJ"]);

                    _ListQualificationMDL.Add(_QualificationMDL);
                }
            }
            return _ListQualificationMDL;
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
            UIHelp.WriteOperateLog(UserName, UserID, "增项注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        ////选择劳动合同类型
        //protected void RadioButtonListENT_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (RadioButtonListENT_ContractType.SelectedValue == "1")
        //    {
        //        RadDatePickerENT_ContractENDTime.Style.Remove("display");
        //        LabelJZSJ.Style.Remove("display");
        //        ValidatorENT_ContractENDTime.Enabled = true;
        //    }
        //    else
        //    {
        //        RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
        //        LabelJZSJ.Style.Add("display", "none");
        //        ValidatorENT_ContractENDTime.Enabled = false;
        //    }
        //}

    }
}