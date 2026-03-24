using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Utility;

//*******业务规则：重新注册**********
//只有证书注销了，证书才能重新注册。
//重新注册可以重资格库任选一个或多个专业注册，不局限于原来的专业。
//重新注册可以在新单位发起。
//***************************
namespace ZYRYJG.Unit
{
    public partial class ApplyRenew : BasePage
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
            get
            {
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

                    ApplyRenewMDL _ApplyRenewMDL = ApplyRenewDAL.GetObject(_ApplyID);
                    ViewState["ApplyRenewMDL"] = _ApplyRenewMDL;

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
                                UIHelp.layerAlert(Page, string.Format("提示您，上次注册申请审核未通过的原因是：【{0}】请您在本次申请提交时，注意核对填报内容是否按照要求修改完成。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                            }
                            else if (IfExistRoleID("2") == false)
                            {
                                UIHelp.layerAlert(Page, string.Format("提示审核人员：该申请人一年内第二次提交注册申请，上次不通过的原因是：【{0}】请注意核对，如存在问题请及时驳回申请。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                            }
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

                    //根据资质判断是否显示新设立企业建造师注册承诺书
                    string cnsql = string.Format("and (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='工程勘察' OR SJLX='工程设计') and [ZZJGDM]='{0}'", _ApplyMDL.ENT_OrganizationsCode);
                    if (jcsjk_QY_ZHXXDAL.SelectCount(cnsql) < 1)
                    {
                        ViewState["Xsl"] = "新设立";
                        cns.Visible = true;
                    }
                    else {
                        ViewState["Xsl"] = "";
                        cns.Visible = false;
                    }
                    //个人登陆后
                    //&& (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回)
                    if (IfExistRoleID("0") == true  //原来的2改为0  修改人：余瑶瑶
                      && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)//2019-10-15  南静添加 待确认判断条件
                      )
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);//基本信息    true改为false，修改人：余瑶瑶
                        UIHelp.SetData(EditTable, _ApplyRenewMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxPSN_RegisterNO, true);

                        //详细开启控件启用属性
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);//人员电话
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);//邮箱
                        UIHelp.SetReadOnly(RadComboBoxNation, false);//民族
                        UIHelp.SetReadOnly(RadDatePickerDisnableDate, true);//失效日期
                        UIHelp.SetReadOnly(RadTextBoxOtherCert, false);//其他证书信息
                        //RadDatePickerENT_ContractStartTime.Enabled = true;
                        //RadDatePickerENT_ContractENDTime.Enabled = true;


                        divGR.Visible = true;//个人操作按钮 //2019-10-15  南静添加
                        //trFuJanTitel.Visible = true;
                        //trFuJan.Visible = true;
                    }
                    else//只读
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyRenewMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxPSN_RegisterNO, true);
                        //RadDatePickerENT_ContractStartTime.Enabled = false;
                        //RadDatePickerENT_ContractENDTime.Enabled = false;
                        //RadioButtonListENT_ContractType.Enabled = false;

                        if (IfExistRoleID("0") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已公示)
                        {
                            divGR.Visible = true;//个人操作按钮
                        }
                    }

                    RadTextBoxPSN_RegisterNO.Text = _ApplyRenewMDL.OldRegisterNo;

                    ListItem CancelReason = RadioButtonListDisnableReason.Items.FindByValue(_ApplyRenewMDL.DisnableReason);
                    if (CancelReason != null) { CancelReason.Selected = true; }

                    //if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回)//提交后不允许修改
                    //{
                    //    ButtonSave.Enabled = false;

                    //}
                     //2019-10-16   南静添加
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
                                TextBoxOldUnitCheckRemark.Text = "提交区县审核";
                                UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                divUnit.Visible = true;
                            }
                        }
                        //企业看不到各级申办人列
                        RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    }
                   
                    SetButtonEnable(_ApplyMDL.ApplyStatus);
                    //存到人员信息
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                    //人员照片
                    BindFile(_ApplyMDL.ApplyID);
                    //审批记录
                    BindCheckHistory(_ApplyRenewMDL.ApplyID);
                    //自动增长行内元素
                    SetExamInfo(_ApplyRenewMDL.ExamInfo);

                }
                else//new
                {
                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "企业信息中的监管区县与建造师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                    //    return;
                    //}

                    //#endregion
                    if (IfExistRoleID("0")==true)//个人登录后
                    {
                        //资格库按钮
                        Tablezjhm0.Visible = true;
                        //企业信息按钮
                        Tablezjhm1.Visible = true;
                        divCheckHistory.Visible = false;
                        divEdit.Visible = false;
                        //RadioButtonListENT_ContractType.SelectedIndex = 0;
                        divUnit.Visible = false;
                        //根据身份证检索资格库校验数据 (用于重新注册)
                        List<QualificationMDL> dt = QualificationDAL.GetObjectListWithCancel(WorkerCertificateCode);
                        RadGridHZSB.DataSource = dt;
                        RadGridHZSB.DataBind();
                        if (dt.Count == 0)
                        {
                            UIHelp.layerAlert(Page, "没有要重新注册的证书!");
                        }
                        else if (dt.Count == 1)//有一个专业的时候，默认选中
                        {
                            ((RadGridHZSB.MasterTableView.Items[0].Cells[2].Controls[1]) as CheckBox).Checked = true;
                        }

                         COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNOPSN_Level(WorkerCertificateCode);
                         if (o == null)
                         {
                             ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                         }

                        //附件
                        BindFile("0");
                    }

                }
                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;

                //申请操作权限
                //2019-10-17   南静    注释
                //if (IfExistRoleID("2") == true)
                //{
                //    if (string.IsNullOrEmpty(Request["a"]) == false)//edit
                //    {
                //        divUnit.Visible = true;
                //    }

                //    //企业看不到各级申办人列
                //    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                //    //divCheckHistory.Visible = false;

                //}
               



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
                //专业委办局核查权限
                if ((IfExistRoleID("8") == true || IfExistRoleID("9") == true) && ViewLimit == false)
                {
                    divOtherDeptCheck.Visible = true;
                }

                //建委领导审核权限
                if (IfExistRoleID("6") == true && ViewLimit == false)
                {
                    divDecide.Visible = true;
                    Disabled();//禁用控件
                }
                if (ViewLimit == true)//查看
                {
                    divViewAction.Visible = true;
                    Disabled();//禁用控件
                }
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }

        }
    
        //确定重新注册人员
        protected void ButtonSaveZjh_Click(object sender, EventArgs e)
        {
           
            #region 规则检查
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (RadTextBoxCreditCode.Text == null || RadTextBoxCreditCode.Text == "")
            {
                UIHelp.layerAlert(Page, "请填写企业统一社会信用代码（或组织机构代码）信息！");
                return;
            }

            List<QualificationMDL> _ListQualificationMDL = ListQualificationMDL();
            if (_ListQualificationMDL.Count == 0)
            {
                UIHelp.layerAlert(Page, "请选择一个要注册的专业！");
                return;
            }

            //在办业务检查
            DataTable dt1 = CommonDAL.GetDataTable(string.Format("select * from dbo.[Apply] where PSN_CertificateNO='{0}' and  ApplyStatus !='已公告' ", WorkerCertificateCode));
            if (dt1 != null && dt1.Rows.Count > 0 && dt1.Rows[0]["ApplyID"] != DBNull.Value)
            {
                //UIHelp.layerAlert(Page, "已有在办业务不允许初始注册！", 5, 0);
                UIHelp.layerAlert(Page, string.Format("【{0}】正在为【{1}】办理{2}，无法申请其他注册申请，请自行联系该单位进行协商取消在办业务。"
                  , dt1.Rows[0]["ENT_Name"]
                  , dt1.Rows[0]["PSN_Name"]
                  , dt1.Rows[0]["ApplyType"]
                  ), 5, 0);
                return;
            }


            #endregion
            

            
            //获取表格勾选集合,并返回一个资格库集合
            //List<QualificationMDL> _ListQualificationMDL = ListQualificationMDL();

            if (_ListQualificationMDL.Count > 0)
            {
                //获取该人已注销证书信息，绑定到页面上
                COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNOPSN_Level(WorkerCertificateCode);
                if(o == null)
                {
                    UIHelp.layerAlert(Page, "未找到可重新注册的证书，请走初始注册流程", 5, 0);
                    return;
                }
               
                if (o.PSN_RegisteType != "07")
                {
                    UIHelp.layerAlert(Page, "证书未注销请先注销证书！", 5, 0);
                    return;
                }
                //DBHelper db = new DBHelper();
                //string sql = string.Format("select * from dbo.[Apply] where  PSN_CertificateNO='{0}' and  ApplyStatus !='已公告' ",WorkerCertificateCode);
                DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT * FROM [dbo].[View_JZS_TOW_Applying]  where PSN_RegisterNO='{0}' and  PSN_CertificateNO='{1}'", o.PSN_RegisterNO, o.PSN_CertificateNO));

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ApplyID"] != DBNull.Value)
                {
                    //UIHelp.layerAlert(Page, "已有在办业务不允许重新注册！", 5, 0);
                    UIHelp.layerAlert(Page, string.Format("【{0}】正在为注册号为【{1}】的证书办理{2}，无法申请其他注册申请，请自行联系该单位进行协商取消在办业务。"
                    , dt.Rows[0]["NewENT_Name"]
                    , dt.Rows[0]["PSN_RegisterNO"]
                    , (dt.Rows[0]["ApplyTypeSub"] != DBNull.Value ? dt.Rows[0]["ApplyTypeSub"] : dt.Rows[0]["ApplyType"])
                    ), 5, 0);
                    return;
                }

                #region 查询证书是否锁定
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(o.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
                #endregion

                ViewState["COC_TOW_Person_BaseInfoMDL"] =o;
                UIHelp.SetData(EditTable, o, true);

                //考生信息
                WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                RadTextBoxPSN_Name.Text = _WorkerOB.WorkerName;//姓名
                RadTextBoxPSN_CertificateNO.Text = _WorkerOB.CertificateCode;//证件
                RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
                if (string.IsNullOrEmpty(_WorkerOB.Email)==false) RadTextBoxPSN_Email.Text = _WorkerOB.Email;//邮箱
                if (string.IsNullOrEmpty(_WorkerOB.Nation) == false)//民族
                {
                    RadComboBoxItem find = RadComboBoxNation.Items.FindItemByText(_WorkerOB.Nation);
                    if (find != null) find.Selected = true;
                }

                //详细开启控件启用属性            
                UIHelp.SetReadOnly(RadTextBoxLinkMan, false);//联系人
                UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false); 
                UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);//人员电话
                UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);//邮箱
                UIHelp.SetReadOnly(RadDatePickerDisnableDate, false);//失效日期
                UIHelp.SetReadOnly(RadTextBoxOtherCert, false);//其他证书信息
                RadDatePickerDisnableDate.SelectedDate = o.PSN_CertificateValidity;//余瑶瑶

                //企业信息
                UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxCreditCode.Text.Trim());//根据组织机构代码获取单位信息
                // if (_UnitMDL == null && RadTextBoxCreditCode.Text.Trim() == "")   南静修改   2019-12-09
                if (_UnitMDL == null )
                {
                    UIHelp.layerAlert(Page, "该企业未在办事大厅注册账号，请先联系企业注册账号后再进行初始注册！");
                    return;

                }
                else if (_UnitMDL != null)
                {
                    RadTextBoxENT_Name.Text = _UnitMDL.ENT_Name;
                    RadTextBoxENT_OrganizationsCode.Text = _UnitMDL.ENT_OrganizationsCode;
                    if (RadTextBoxENT_OrganizationsCode.Text.Length==18)
                    {
                       RadTextBoxENT_OrganizationsCode.Text = RadTextBoxENT_OrganizationsCode.Text.Substring(8, 9); 
                    }
                    RadTextBoxENT_City.Text = _UnitMDL.ENT_City;
                    RadTextBoxPSN_Name.Text = UserName;
                    UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                    UIHelp.SetReadOnly(RadTextBoxLinkMan, false);

                    if (string.IsNullOrEmpty(_UnitMDL.ENT_Contact) == false)
                    {
                        RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                    }
                    if (string.IsNullOrEmpty(_UnitMDL.ENT_Telephone) == false)
                    {
                        RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                    }

                }
                ////人员照片
                //BindFile(o.PSN_RegisterNO, "");


                //绑定继续教育情况  ////南静修改  2019-11-09  i=0 改为 i=1
                for (int i = 0; i < _ListQualificationMDL.Count; i++)
                {

                    //sb.Append(string.Format(@"|{0},{1},{2},{3},{4},{5}"
                    //    , _ListQualificationMDL[i].ZYLB
                    //    , _ListQualificationMDL[i].ZGZSBH
                    //    , _ListQualificationMDL[i].QDNF
                    //    , _ListQualificationMDL[i].ZYLB
                    //    , ""
                    //    , ""));
                    string bxxs = "", xxxs = "";
                    //校验继续教育情况，是否满足最近三年继续教育情况
                    if (Convert.ToDateTime(_ListQualificationMDL[i].QFSJ).AddYears(3) < DateTime.Now)
                    {

                        bxxs = "60";
                        xxxs = "60";                          
                    }
                    sb.Append(string.Format(@"|{0},{1},{2},{3},{4},{5}"
                        , _ListQualificationMDL[i].ZYLB
                        , _ListQualificationMDL[i].ZGZSBH
                        , _ListQualificationMDL[i].QFSJ.Value.ToString("yyyy-MM-dd")
                        , _ListQualificationMDL[i].ZYLB
                        , bxxs
                        , xxxs));
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                }
                SetExamInfo(sb.ToString());

                divGR.Visible = true;
                divEdit.Visible = true;
                divUnit.Visible = false;
                Tablezjhm.Visible = false;
                Tablezjhm0.Visible = false;
                SetButtonEnable("");
            }
            else
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何专业！");
                return;
            }
        }

        //保存申请单
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 有效验证           
          
            if (RadTextBoxENT_Telephone.Text.Trim() == "" )
            {
                UIHelp.layerAlert(Page, "保存失败，联系电话不能为空！", 6, 0);
                return;
            }

            if (RadTextBoxPSN_MobilePhone.Text.Trim() == "" || !Check.IfTelPhoneFormat(RadTextBoxPSN_MobilePhone.Text.Trim()))
            {
                UIHelp.layerAlert(Page, "保存失败，手机号码错误！", 6, 0);
                return;
            }

            if (RadTextBoxPSN_Email.Text.Trim() == "" || !Check.IfMailFormat(RadTextBoxPSN_Email.Text.Trim()))
            {
                UIHelp.layerAlert(Page, "保存失败，邮箱错误！", 6, 0);
                return;
            }
            if (RadComboBoxNation.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "保存失败，民族不可为空！", 6, 0);
                return;
            }
           
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

            if (Utility.Check.CheckBirthdayLimit(0, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满65周岁前90天起,不再允许发起二级建造师初始、重新、延续、增项、执业企业变更注册申请。", 5, 0);
                return;
            }

            if (Utility.Check.CheckIfSubUnit(RadTextBoxENT_Name.Text) == true)
            {
                UIHelp.layerAlert(Page, UIHelp.Tip_SubUnitForbid, 5, 0);
                return;
            }
                        
            jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(RadTextBoxENT_OrganizationsCode.Text.Trim());//企业资质
            GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(RadTextBoxENT_OrganizationsCode.Text.Trim()); //工商信息

            //更新企业资质
            if (_jcsjk_QY_ZHXXMDL != null)
            {
                if (RadTextBoxENT_Name.Text.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")")//企业名称变更
                    || RadTextBoxENT_City.Text != _jcsjk_QY_ZHXXMDL.XZDQBM//隶属区县变更
                    )
                {

                    UIHelp.layerAlert(Page, string.Format("检测到所注册企业信息与工商或资质库中企业信息不一致（{0}{1}），请先联系企业办理企业信息变更。",
                        RadTextBoxENT_Name.Text.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")") ? string.Format("企业名称：{0}≠{1}。", RadTextBoxENT_Name.Text, _jcsjk_QY_ZHXXMDL.QYMC) : "",
                    RadTextBoxENT_City.Text != _jcsjk_QY_ZHXXMDL.XZDQBM ? string.Format("隶属区县：{0}≠{1}。", RadTextBoxENT_City.Text, _jcsjk_QY_ZHXXMDL.XZDQBM) : ""
                        ), 5, 0);
                    return;
                }
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
            _ApplyMDL.ApplyType = EnumManager.ApplyType.重新注册;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;

            ApplyRenewMDL _ApplyRenewMDL = ViewState["ApplyRenewMDL"] == null ? new ApplyRenewMDL() : (ApplyRenewMDL)ViewState["ApplyRenewMDL"];//详细表            
            UIHelp.GetData(EditTable, _ApplyRenewMDL);//OBJECT控件对象自动获取

            //注销原因
            _ApplyRenewMDL.DisnableReason = RadioButtonListDisnableReason.SelectedValue;

            //其它资格考试合格专业情况
            _ApplyRenewMDL.ExamInfo = GetExamInfo();
            ////劳动合同类型
            //_ApplyMDL.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);
            ////劳动合同开始时间
            //_ApplyMDL.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
            ////劳动合同结束时间
            //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
            //{
            //    _ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
            //}
            //else
            //{
            //    _ApplyMDL.ENT_ContractENDTime = null;
            //}

            //企业信息  
            UnitMDL _unit = DataAccess.UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxENT_OrganizationsCode.Text.Trim());
            if (_unit.ResultGSXX == 0 || _unit.ResultGSXX == 1)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", _unit.ENT_Name));
                return;
            }

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    //人员信息
                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];

                    ////企业信息  
                    //UnitMDL _unit = DataAccess.UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxCreditCode.Text.Trim());

                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "重新注册");
                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();//申请ID
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;  
                  
                    _ApplyMDL.ENT_ServerID = _unit.UnitID;//新单位ID 
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;
                    

                    //申请专业
                    _ApplyMDL.PSN_RegisteProfession = GetProfession();

                    _ApplyRenewMDL.ApplyID = _ApplyMDL.ApplyID;
                    _ApplyRenewMDL.OldRegisterNo = RadTextBoxPSN_RegisterNO.Text;
                    _ApplyRenewMDL.OldRegisterCertificateNo = RadTextBoxPSN_RegisterCertificateNo.Text;
                    _ApplyRenewMDL.PSN_Telephone = person.PSN_Telephone;
                  
                    _ApplyRenewMDL.FR = _unit.ENT_Corporate;
                    _ApplyRenewMDL.ENT_Correspondence = _unit.ENT_Correspondence;
                    _ApplyRenewMDL.ENT_Economic_Nature = _unit.ENT_Economic_Nature;
                    _ApplyRenewMDL.END_Addess = _unit.END_Addess;
                    _ApplyRenewMDL.ENT_Postcode = _unit.ENT_Postcode;
                    _ApplyRenewMDL.ENT_Type = _unit.ENT_Type;
                    _ApplyRenewMDL.ENT_Sort = _unit.ENT_Sort;
                    _ApplyRenewMDL.ENT_Grade = _unit.ENT_Grade;
                    _ApplyRenewMDL.ENT_QualificationCertificateNo = _unit.ENT_QualificationCertificateNo;

                    //所属区县
                    if (string.IsNullOrEmpty(_ApplyMDL.ENT_City))
                    {
                        UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                        return;
                    }


                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyRenewDAL.Insert(tran, _ApplyRenewMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.执业资格证书扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

                    //trFuJanTitel.Visible = true;
                    //trFuJan.Visible = true;

                    //根据资质判断是否显示新设立企业建造师注册承诺书
                    string cnsql = string.Format("and (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='工程勘察' OR SJLX='工程设计') and [ZZJGDM]='{0}'", RadTextBoxENT_OrganizationsCode.Text.Trim());
                    if (jcsjk_QY_ZHXXDAL.SelectCount(cnsql) < 1)
                    {
                        ViewState["Xsl"] = "新设立";
                        cns.Visible = true;
                    }
                    else
                    {
                        ViewState["Xsl"] = "";
                        cns.Visible = false;
                    }
                }
                else//update
                {
                    ApplyDAL.Update(tran, _ApplyMDL);
                    ApplyRenewDAL.Update(tran, _ApplyRenewMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyRenewMDL"] = _ApplyRenewMDL;

                BindFile(ApplyID);

                SetButtonEnable(_ApplyMDL.ApplyStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                UIHelp.ParentAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）", true);
                //UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）", 6, 0);
                //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存重新注册申请表信息失败", ex);
            }

        }

        //提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表


            ApplyRenewMDL _ApplyRenewMDL = (ApplyRenewMDL)ViewState["ApplyRenewMDL"];
            try
            {
                //if (ButtonApply.Text != "取消申报")//南静注释 2019-10-18   判断按钮不对
                if (ButtonUnit.Text != "取消申报")
                {
                    bool ifNeedJXJY = false;//是否需要上传继续教育
                    foreach (Telerik.Web.UI.GridDataItem gi in RadGridExamInfo.MasterTableView.Items)
                    {
                        Label LabelKSQFRQ = gi.FindControl("LabelKSQFRQ") as Label;//签发日期
                        if (Convert.ToDateTime(LabelKSQFRQ.Text).AddYears(3) < DateTime.Now)
                        {
                            ifNeedJXJY = true;
                            break;
                        }
                    }

                    //必须上传附件集合
                    System.Collections.Hashtable fj;

                    //校验继续教育情况，是否满足最近三年继续教育情况
                    if (ifNeedJXJY == true)
                    {
                        if (ViewState["Xsl"].ToString() == "新设立")
                        {
                            //必须上传附件集合
                            fj = new System.Collections.Hashtable{
                     {EnumManager.FileDataTypeName.一寸免冠照片,0},
                     {EnumManager.FileDataTypeName.证件扫描件,0},
                     //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                     //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                     //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                     {EnumManager.FileDataTypeName.继续教育承诺书扫描件,0},
                     {EnumManager.FileDataTypeName.申请表扫描件,0},
                     {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};
                        }
                        else
                        {
                            //必须上传附件集合
                            fj = new System.Collections.Hashtable{
                    {EnumManager.FileDataTypeName.一寸免冠照片,0},
                    {EnumManager.FileDataTypeName.证件扫描件,0},
                    //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                    //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                    //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    {EnumManager.FileDataTypeName.继续教育承诺书扫描件,0},
                    {EnumManager.FileDataTypeName.申请表扫描件,0}};
                        }

                    }
                    else
                    {
                        if (ViewState["Xsl"].ToString() == "新设立")
                        {

                            //必须上传附件集合
                            fj = new System.Collections.Hashtable{
                     {EnumManager.FileDataTypeName.一寸免冠照片,0},
                     {EnumManager.FileDataTypeName.证件扫描件,0},
                     //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                     //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                     //{EnumManager.FileDataTypeName.劳动合同扫描件,0},

                     {EnumManager.FileDataTypeName.申请表扫描件,0},
                     {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};

                        }
                        else
                        {
                            //必须上传附件集合
                            fj = new System.Collections.Hashtable{
                     {EnumManager.FileDataTypeName.一寸免冠照片,0},
                     {EnumManager.FileDataTypeName.证件扫描件,0},
                     //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                     //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                     //{EnumManager.FileDataTypeName.劳动合同扫描件,0},

                     {EnumManager.FileDataTypeName.申请表扫描件,0}};

                        }

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



                //_ApplyMDL.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                ////劳动合同结束时间
                //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                //{
                //    _ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                //}
                //else
                //{
                //    _ApplyMDL.ENT_ContractENDTime = null;
                //}


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
                //人员照片
                BindFile(_ApplyMDL.ApplyID);
                if (_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请提交成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));

                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID)); 
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    //RadDatePickerENT_ContractStartTime.Enabled = true;
                    //RadDatePickerENT_ContractENDTime.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "重新注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            //南静注释  2019-10-18
            //ApplyRenewMDL _ApplyRenewMDL = (ApplyRenewMDL)ViewState["ApplyRenewMDL"];
            
            //if (ButtonApply.Text != "撤销申报")
            //{
            //    bool ifNeedJXJY = false;//是否需要上传继续教育
            //    foreach (Telerik.Web.UI.GridDataItem gi in RadGridExamInfo.MasterTableView.Items)
            //    {
            //        Label LabelKSQFRQ = gi.FindControl("LabelKSQFRQ") as Label;//签发日期
            //        if (Convert.ToDateTime(LabelKSQFRQ.Text).AddYears(3) < DateTime.Now)
            //        {
            //            ifNeedJXJY = true;
            //            break;
            //        }
            //    }

            //    //必须上传附件集合
            //    System.Collections.Hashtable fj;

            //    //校验继续教育情况，是否满足最近三年继续教育情况
            //    if (ifNeedJXJY == true)
            //    {
            //        if (ViewState["Xsl"].ToString() == "新设立")
            //        {
            //            //必须上传附件集合
            //            fj = new System.Collections.Hashtable{
            //         {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //         {EnumManager.FileDataTypeName.证件扫描件,0},
            //         {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //         {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //         {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //         {EnumManager.FileDataTypeName.继续教育证明扫描件,0},
            //         {EnumManager.FileDataTypeName.申请表扫描件,0},
            //         {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};
            //        }
            //        else 
            //        {
            //            //必须上传附件集合
            //            fj = new System.Collections.Hashtable{
            //        {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //        {EnumManager.FileDataTypeName.证件扫描件,0},
            //        {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //        {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //        {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //        {EnumManager.FileDataTypeName.继续教育证明扫描件,0},
            //        {EnumManager.FileDataTypeName.申请表扫描件,0}};
            //        }
                    
            //    }
            //    else
            //    {
            //        if (ViewState["Xsl"].ToString() == "新设立")
            //        {

            //            //必须上传附件集合
            //            fj = new System.Collections.Hashtable{
            //         {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //         {EnumManager.FileDataTypeName.证件扫描件,0},
            //         {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //         {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //         {EnumManager.FileDataTypeName.劳动合同扫描件,0},

            //         {EnumManager.FileDataTypeName.申请表扫描件,0},
            //         {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};

            //        }
            //        else
            //        {
            //            //必须上传附件集合
            //            fj = new System.Collections.Hashtable{
            //         {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //         {EnumManager.FileDataTypeName.证件扫描件,0},
            //         {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //         {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //         {EnumManager.FileDataTypeName.劳动合同扫描件,0},

            //         {EnumManager.FileDataTypeName.申请表扫描件,0}};
                    
            //        }
                    
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

           
            //_ApplyMDL.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
            //_ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;

            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            if (ButtonApply.Text == "取消申报")
            {
                //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;//南静注释 2019-10-18
                //_ApplyMDL.ApplyTime = null;//南静注释 2019-10-18
                //南静  2019-10-18  添加
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
            }
            else
            {
                //南静  2019-10-18  添加
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                {
                    TextBoxOldUnitCheckRemark.Visible = true;
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                }
                else {

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
                    _ApplyMDL.ConfirmMan = null;
                
                }
                
            }
            ApplyDAL.Update(_ApplyMDL);
            ViewState["ApplyMDL"] = _ApplyMDL;
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            //人员照片
            BindFile(_ApplyMDL.ApplyID);
            if (_ApplyMDL.ApplyStatus == "已申报")
            {
                UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
                //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
            else
            {
                //南静注释  2010-10-18
                //UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                //南静添加  2010-10-18
                UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                UIHelp.layerAlert(Page, "退回个人成功！", 6, 3000);
                //RadDatePickerENT_ContractStartTime.Enabled = true;
                //RadDatePickerENT_ContractENDTime.Enabled = true;
            }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "重新注册申报失败！", ex);
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
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //导出打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师重新注册申请表.docx");
                string fileName = "北京市二级建造师重新注册申请表";


                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                ApplyRenewMDL _ApplyRenewMDL = ViewState["ApplyRenewMDL"] as ApplyRenewMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                COC_TOW_Person_BaseInfoMDL person = ViewState["COC_TOW_Person_BaseInfoMDL"] as COC_TOW_Person_BaseInfoMDL;
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyRenewMDL);
                var ht = PrintDocument.GetProperties(o);
                //对时间类型进行格式转换
                if (_FileInfoMDL != null)
                {
                    ht["photo"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else { ht["photo"] = ""; }
                if (ht["photo"] == "") {
                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
                    return;
                }
                ht["PSN_BirthDate"] = ((DateTime)person.PSN_BirthDate).ToString("yyyy年MM月dd日");
                //ht["Nation"] = person.PSN_National;
                ht["DisnableDate"] = ((DateTime)_ApplyRenewMDL.DisnableDate).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractStartTime"] = _ApplyMDL.ENT_ContractStartTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractStartTime).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractENDTime"] = _ApplyMDL.ENT_ContractENDTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractENDTime).ToString("yyyy年MM月dd日");
                
                ////2019-11-27南静添加
                //switch (_ApplyMDL.ENT_ContractType)
                //{
                //    case 1:
                //        ht["ENT_ContractType1"] = "☑";
                //        ht["ENT_ContractType2"] = "□";
                //        ht["ENT_ContractType3"] = "□";
                //        break;
                //    case 2:
                //        ht["ENT_ContractType1"] = "□";
                //        ht["ENT_ContractType2"] = "☑";
                //        ht["ENT_ContractType3"] = "□";
                //        break;
                //    case 3:
                //        ht["ENT_ContractType1"] = "□";
                //        ht["ENT_ContractType2"] = "□";
                //        ht["ENT_ContractType3"] = "☑";

                //        break;
                //    default:
                //        ht["ENT_ContractType1"] = "☑";
                //        ht["ENT_ContractType2"] = "□";
                //        ht["ENT_ContractType3"] = "□";
                //        break;
                //}

              

                ////证件类型
                ////string ZjType = ht["PSN_CertificateType"].ToString();
                ////ht["SFZ"] = ZjType == "身份证" ? "☑" : "□";
                ////ht["JGZ"] = ZjType == "军官证" ? "☑" : "□";
                ////ht["JG"] = ZjType == "警官证" ? "☑" : "□";
                ////ht["HZ"] = ZjType == "护照" ? "☑" : "□";
                //////企业类型
                ////string QyType = ht["ENT_Type"].ToString();
                ////ht["SG"] = QyType == "施工" ? "☑" : "□";
                ////ht["KC"] = QyType == "勘察" ? "☑" : "□";
                ////ht["SJ"] = QyType == "设计" ? "☑" : "□";
                ////ht["JL"] = QyType == "监理" ? "☑" : "□";
                ////ht["ZBDL"] = QyType == "招标代理" ? "☑" : "□";
                ////ht["ZJZX"] = QyType == "造价咨询" ? "☑" : "□";
                //////将身份证拆开，小于18位的后三位用空补齐
                ////string PSN_CertificateNO = ht["PSN_CertificateNO"].ToString();
                ////char[] a = PSN_CertificateNO.ToCharArray();
                ////if (a.Length < 17)
                ////{
                ////    a[15] = Convert.ToChar("");
                ////    a[16] = Convert.ToChar("");
                ////    a[17] = Convert.ToChar("");
                ////}
                ////for (int i = 0; i < a.Length; i++)
                ////{
                ////    ht["Sfz" + i + ""] = a[i];
                ////}
                //////失效原因
                ////string Sxyy = ht["DisnableReason"].ToString();
                ////ht["a"] = Sxyy == "聘用企业破产的；" ? "☑" : "□";
                ////ht["b"] = Sxyy == "聘用企业被吊销营业执照的；" ? "☑" : "□";
                ////ht["c"] = Sxyy == "聘用企业被吊销或者撤回资质证书的" ? "☑" : "□";
                ////ht["d"] = Sxyy == "已与聘用企业解除聘用合同关系的；" ? "☑" : "□";
                ////ht["e"] = Sxyy == "注册有效期满且未延续注册的；" ? "☑" : "□";
                ////ht["f"] = Sxyy == "年龄超过65周岁的" ? "☑" : "□";
                ////ht["g"] = Sxyy == "死亡或不具有完全民事行为能力的；" ? "☑" : "□";
                ////ht["h"] = Sxyy == "聘用企业被吊销相应资质证书的；" ? "☑" : "□";
                ////ht["y"] = Sxyy == "其他导致注册失效的情形；" ? "☑" : "□";
                ////ht["j"] = Sxyy == "依法被撤消注册的；" ? "☑" : "□";
                ////ht["k"] = Sxyy == "依法被吊销注册证书的；" ? "☑" : "□";
                ////ht["l"] = Sxyy == "法律、法规规定应当注销注册的其他情形。" ? "☑" : "□";
                ////ht["yy"] = ht["PSN_National"];
                //把ExamInfo里面的数据拆分成一张内存表
                string arry = _ApplyRenewMDL.ExamInfo;
                string[] arryty = arry.Split('|');
                DataTable dt = new DataTable();
                //表列明意思分别（每逗号一个列）：考试合格专业类别，考试编号，签发日期，注册专业，必修课时，选修课时
                dt.Columns.Add("KSHGZYLB");
                dt.Columns.Add("KSBH");
                dt.Columns.Add("QFRQ");
                dt.Columns.Add("ZCZY");
                dt.Columns.Add("BXKS");
                dt.Columns.Add("XXKS");
                
                string[] str0 = arryty[0].Split(',');
                ht["PSN_ExamCertCode"] = str0[1];
                ht["ConferDate"] = str0[2];
                ht["ApplyRegisteProfession"] = str0[3];
                ht["BiXiu"] = str0[4];
                ht["XuanXiu"] = str0[5];
                arryty[0] = "";
                for (int h = 1; h < arryty.Length; h++)
                {
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    string[] str = arryty[h].Split(',');
                    foreach (string j in str)
                    {
                        dr[i] = j;
                        i++;
                    }
                    dt.Rows.Add(dr);
                }
                //foreach (string s in arryty)
                //{
                    
                //    DataRow dr = dt.NewRow();
                //    int i = 0;
                //    string[] str = s.Split(',');
                //    foreach (string j in str)
                //    {
                //        dr[i] = j;
                //        i++;
                //    }
                //    dt.Rows.Add(dr);
                //}
                ht["tableList"] = new List<DataTable> { dt };
                ht["tableIndex"] = new List<int> { 1 };
                ht["insertIndex"] = new List<int> { 2 };
                ht["ContainsHeader"] = new List<bool> { true };
                ht["isCtable"] = true;
                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印重新注册Word失败！", ex);
            }
        }

        //区县受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
           
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            #region 查询证书是否锁定
            if ( RadioButtonListApplyStatus.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
           
            #endregion

            NewSetUpMDL _NewSetUpMDL = new NewSetUpMDL();
            _ApplyMDL.GetDateTime = DateTime.Now;
            _ApplyMDL.GetMan = UserName;
            _ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            //_ApplyMDL.Memo=_ApplyMDL.CheckXSL == 1 ? "1" : "0"; //是否是新设立
            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }

            #region  检查是否为新设立
            if (_ApplyMDL.CheckXSL == 1 || _ApplyMDL.CheckXSL == null)
            {
                _NewSetUpMDL.ApplyId = _ApplyMDL.ApplyID;
              
                _NewSetUpMDL.XslDateTime = DateTime.Now;
                _NewSetUpMDL.ApplyType = _ApplyMDL.ApplyType;
                NewSetUpDAL.Insert(_NewSetUpMDL);
            }
            #endregion
            try
            {
                ApplyDAL.Update(_ApplyMDL);
               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册区县受理", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession
                , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
           // UIHelp.ParentAlert(Page, "受理成功！", true);

            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
            //Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
        }

        //区县审查
        protected void BttSave_Click(object sender, EventArgs e)
        {

            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
           
            #region 查询证书是否锁定

            if (RadioButtonListExamineResult.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion
            //移动到汇总上报时校验
            //if (_ApplyMDL.CheckXSL.HasValue == false || _ApplyMDL.CheckXSL.Value == 1)
            //{
            //    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
            //    return;
            //}
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
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册区县审查", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession
                , _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "区县审查成功！", true);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            #region 查询证书是否锁定
            if (RadioButtonListCheckResult.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion
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
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册建委审核", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession
                 , _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //专业局会审
        protected void ButtonOtherDeptCheck_Click(object sender, EventArgs e)
        {
            
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            #region 查询证书是否锁定
            if (RadioButtonListOtherDeptCheckResult.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion
            //_ApplyMDL.OtherDeptCheckDate = DateTime.Now;
            //会审时不设置时间，回传是统一设置时间
            _ApplyMDL.OtherDeptCheckMan = UserName;
            _ApplyMDL.OtherDeptCheckResult = RadioButtonListOtherDeptCheckResult.SelectedValue;
            _ApplyMDL.OtherDeptCheckRemark = TextBoxApplyOtherDeptCheckRemark.Text.Trim();

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "会审失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册专业局会审成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
            UIHelp.ParentAlert(Page, "会审成功！", true);
        }

        //建委领导决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
       
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            #region 查询证书是否锁定
            if (RadioButtonListDecide.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion
            _ApplyMDL.ConfirmDate = DateTime.Now;
            _ApplyMDL.ConfirmMan = UserName;
            _ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已决定;

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "决定失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册建委决定成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
            UIHelp.ParentAlert(Page, "决定成功！", true);
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
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["XM "] != null)  _QualificationMDL.XM = RadGridHZSB.MasterTableView.DataKeyValues[i]["XM"].ToString();
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

        ////上传WORD扫描件
        //protected void FileWord(ref bool ischek)
        //{
          
        //    //检查是否上传文件
        //    if (FileUploadWord.HasFile == false)
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, "请将盖章签字后的申请表扫描上传！");
        //        return ;
        //    }
        //    //检查保存文件
        //    if ((FileUploadWord.HasFile == true && Path.GetExtension(FileUploadWord.FileName).ToLower() != ".jpg"))
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, "上传扫描件只支持jpg格式图片，请检查你要上传的文件是否格式正确！");
        //        return ;
        //    }
        //    Int64 fileSizeLimit = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]) * 1024;
        //    if ((FileUploadWord.HasFile == true && FileUploadWord.FileContent.Length > fileSizeLimit))
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, string.Format("上传扫描件大小不能超过{0}kb，请检查您要上传的文件大小！", System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]));
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
            step_已公示.Attributes["class"] = step_已公示.Attributes["class"].Replace(" green", "");
            step_已公告.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");

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
                case "已公示":
                    step_已公示.Attributes["class"] += " green";
                    break;
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
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
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
            if (ApplyStatus == EnumManager.ApplyStatus.已受理
                || ApplyStatus == EnumManager.ApplyStatus.区县审查
                || ApplyStatus == EnumManager.ApplyStatus.已上报
                || ApplyStatus == EnumManager.ApplyStatus.已审查
                || ApplyStatus == EnumManager.ApplyStatus.已决定)
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

        /// <summary>
        /// 获取考试合格证信息
        /// </summary>
        /// <returns></returns>
        private string GetExamInfo()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Telerik.Web.UI.GridDataItem gi in RadGridExamInfo.MasterTableView.Items)
            {
                Label LabelKSZY = gi.FindControl("LabelKSZY") as Label;//考试合格专业

                Label LabelKSHGZMBH = gi.FindControl("LabelKSHGZMBH") as Label;//考试合格证编号

                Label LabelKSQFRQ = gi.FindControl("LabelKSQFRQ") as Label;//签发日期

                Label LabelSQZCZY = gi.FindControl("LabelSQZCZY") as Label;//申请专业

                Label LabelBiXiu = gi.FindControl("LabelBiXiu") as Label;//必须课时

                Label LabelXuanXiu = gi.FindControl("LabelXuanXiu") as Label;//选修课时

                sb.Append(string.Format("|{0},{1},{2},{3},{4},{5}"
                    , LabelKSZY.Text
                    , LabelKSHGZMBH.Text
                    , LabelKSQFRQ.Text
                    , LabelSQZCZY.Text
                    , LabelBiXiu.Text
                    , LabelXuanXiu.Text
                    ));
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取注册专业
        /// </summary>
        /// <returns>专业，用逗号分隔</returns>
        private string GetProfession()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Telerik.Web.UI.GridDataItem gi in RadGridExamInfo.MasterTableView.Items)
            {
                Label LabelKSZY = gi.FindControl("LabelKSZY") as Label;//考试合格专业

                sb.Append(",").Append(LabelKSZY.Text);

            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 绑定考试合格证信息
        /// </summary>
        /// <param name="ExamInfo"></param>
        private void SetExamInfo(string ExamInfo)
        {
            DataTable dt = null;
            if (string.IsNullOrEmpty(ExamInfo) == true)//new
            {
                dt = CommonDAL.GetDataTable(@"select 1 RowNum ");
                RadGridExamInfo.DataSource = dt;
                RadGridExamInfo.DataBind();
            }
            else//edit
            {
                string[] rows = ExamInfo.Split('|');
                string[] cols = null;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < rows.Length; i++)
                {
                    sb.Append(string.Format(" union select {0} RowNum", i));
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 6);
                }
                dt = CommonDAL.GetDataTable(sb.ToString());
                RadGridExamInfo.DataSource = dt;
                RadGridExamInfo.DataBind();

                Telerik.Web.UI.GridDataItem gi;
                for (int i = 0; i < rows.Length; i++)
                {
                    /*
                    cols = rows[i].Split(',');
                    gi = RadGridExamInfo.MasterTableView.Items[i];
                    RadComboBox RadComboBoxKSZY = gi.FindControl("RadComboBoxKSZY") as RadComboBox;//考试合格专业
                    RadComboBoxItem f1 = RadComboBoxKSZY.Items.FindItemByValue(cols[0]);
                    if (f1 != null) f1.Selected = true;

                    RadTextBox RadTextBoxKSHGZMBH = gi.FindControl("RadTextBoxKSHGZMBH") as RadTextBox;//考试合格证编号
                    RadTextBoxKSHGZMBH.Text = cols[1];

                    RadDatePicker RadDatePickerKSQFRQ = gi.FindControl("RadDatePickerKSQFRQ") as RadDatePicker;//签发日期
                    DateTime f2;
                    if (DateTime.TryParse(cols[2], out f2) == true) RadDatePickerKSQFRQ.DbSelectedDate = f2;

                    RadComboBox RadComboBoxSQZCZY = gi.FindControl("RadComboBoxSQZCZY") as RadComboBox;//申请专业
                    RadComboBoxItem f3 = RadComboBoxSQZCZY.Items.FindItemByValue(cols[3]);
                    if (f3 != null) f3.Selected = true;

                    RadNumericTextBox RadNumericTextBoxBiXiu = gi.FindControl("RadNumericTextBoxBiXiu") as RadNumericTextBox;//必须课时
                    if (string.IsNullOrEmpty(cols[4]) == false) RadNumericTextBoxBiXiu.Value = Convert.ToDouble(cols[4]);

                    RadNumericTextBox RadNumericTextBoxXuanXiu = gi.FindControl("RadNumericTextBoxXuanXiu") as RadNumericTextBox;//选修课时
                    if (string.IsNullOrEmpty(cols[5]) == false) RadNumericTextBoxXuanXiu.Value = Convert.ToDouble(cols[5]);
                     */
                    cols = rows[i].Split(',');
                    gi = RadGridExamInfo.MasterTableView.Items[i];
                    Label LabelKSZY = gi.FindControl("LabelKSZY") as Label;//考试合格专业
                    LabelKSZY.Text = cols[0];

                    Label LabelKSHGZMBH = gi.FindControl("LabelKSHGZMBH") as Label;//考试合格证编号
                    LabelKSHGZMBH.Text = cols[1];

                    Label LabelKSQFRQ = gi.FindControl("LabelKSQFRQ") as Label;//签发日期
                    LabelKSQFRQ.Text = cols[2];

                    Label LabelSQZCZY = gi.FindControl("LabelSQZCZY") as Label;//申请专业
                    LabelSQZCZY.Text = cols[3];

                    Label LabelBiXiu = gi.FindControl("LabelBiXiu") as Label;//必须课时
                    LabelBiXiu.Text = cols[4];

                    Label LabelXuanXiu = gi.FindControl("LabelXuanXiu") as Label;//选修课时
                    LabelXuanXiu.Text = cols[5];
                }
            }


        }

        //删除考试合格证信息
        protected void RadGridExamInfo_DeleteCommand(object source, GridCommandEventArgs e)
        {
            int DelrowIndex = e.Item.ItemIndex;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //其它资格考试合格专业情况
            foreach (Telerik.Web.UI.GridDataItem gi in RadGridExamInfo.MasterTableView.Items)
            {
                /*
                if (gi.ItemIndex == DelrowIndex) continue;
                RadComboBox RadComboBoxKSZY = gi.FindControl("RadComboBoxKSZY") as RadComboBox;//考试合格专业

                RadTextBox RadTextBoxKSHGZMBH = gi.FindControl("RadTextBoxKSHGZMBH") as RadTextBox;//考试合格证编号

                RadDatePicker RadDatePickerKSQFRQ = gi.FindControl("RadDatePickerKSQFRQ") as RadDatePicker;//签发日期

                RadComboBox RadComboBoxSQZCZY = gi.FindControl("RadComboBoxSQZCZY") as RadComboBox;//申请专业

                RadNumericTextBox RadNumericTextBoxBiXiu = gi.FindControl("RadNumericTextBoxBiXiu") as RadNumericTextBox;//必须课时

                RadNumericTextBox RadNumericTextBoxXuanXiu = gi.FindControl("RadNumericTextBoxXuanXiu") as RadNumericTextBox;//选修课时

                sb.Append(string.Format("|{0},{1},{2},{3},{4},{5}"
                    , RadComboBoxKSZY.SelectedValue
                    , RadTextBoxKSHGZMBH.Text.Trim()
                    , RadDatePickerKSQFRQ.SelectedDate.HasValue == true ? RadDatePickerKSQFRQ.SelectedDate.Value.ToString("yyyy-MM-dd") : ""
                    , RadComboBoxSQZCZY.SelectedValue
                    , RadNumericTextBoxBiXiu.Value
                    , RadNumericTextBoxXuanXiu.Value
                    ));
                 */
                Label LabelKSZY = gi.FindControl("LabelKSZY") as Label;//考试合格专业

                Label LabelKSHGZMBH = gi.FindControl("LabelKSHGZMBH") as Label;//考试合格证编号

                Label LabelKSQFRQ = gi.FindControl("LabelKSQFRQ") as Label;//签发日期

                Label LabelSQZCZY = gi.FindControl("LabelSQZCZY") as Label;//申请专业

                Label LabelBiXiu = gi.FindControl("LabelBiXiu") as Label;//必须课时

                Label LabelXuanXiu = gi.FindControl("LabelXuanXiu") as Label;//选修课时

                sb.Append(string.Format("|{0},{1},{2},{3},{4},{5}"
                    , LabelKSZY.Text
                    , LabelKSHGZMBH.Text
                    , LabelKSQFRQ.Text
                    , LabelSQZCZY.Text
                    , LabelBiXiu.Text
                    , LabelXuanXiu.Text
                    ));
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }

            SetExamInfo(sb.ToString());

        }

        //禁用控件
        private void Disabled()
        {
            UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, true);
            UIHelp.SetReadOnly(RadTextBoxPSN_Email, true);
            UIHelp.SetReadOnly(RadDatePickerDisnableDate, true);
            UIHelp.SetReadOnly(RadTextBoxOtherCert, true);
            RadioButtonListDisnableReason.Enabled = false;
            //ImageButtonAddRow.Enabled = false;
            RadGridExamInfo.Enabled = false;
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
            UIHelp.WriteOperateLog(UserName, UserID, "重新注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text));
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

        protected void RadTextBoxUnitName_TextChanged(object sender, EventArgs e)
        {
            //企业信息
            UnitMDL _UnitMDL = UnitDAL.GetObjectByUnitName(RadTextBoxUnitName.Text.Trim());//根据组织机构代码获取单位信息
            //string UnitCode = UnitDAL.GetUnitNameByUnitNameFromQY_BWDZZZS(RadTextBoxUnitName.Text.Trim(), true);
            if (_UnitMDL != null)
            {
                if (string.IsNullOrEmpty(_UnitMDL.ENT_OrganizationsCode) == false)
                {
                    RadTextBoxCreditCode.Text = _UnitMDL.ENT_OrganizationsCode;  //单位组织机构代码
                }
            }
            else {
                UIHelp.layerAlert(Page, "未查询到您输入的企业,请核实后重新输入！", 5, 0);
                RadTextBoxCreditCode.Text = "";  //单位组织机构代码
            }
            
        }

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
                case EnumManager.ApplyStatus.已决定:
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
                case EnumManager.ApplyStatus.已公告:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，公告时间：{3}，公告人：{4}，公告批次号：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.NoticeDate, _ApplyMDL.NoticeMan, _ApplyMDL.NoticeCode, RadComboBoxReturnApplyStatus.SelectedValue);
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

                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

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

                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

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

                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.区县审查;
                            break;
                        case EnumManager.ApplyStatus.已上报:
                            _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;

                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已上报;
                            break;
                        case EnumManager.ApplyStatus.已审查:
                            _ApplyMDL.ConfirmDate = null;
                            _ApplyMDL.ConfirmMan = null;
                            _ApplyMDL.ConfirmResult = null;

                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已审查;
                            break;

                        case EnumManager.ApplyStatus.已决定:
                            _ApplyMDL.NoticeDate = null;
                            _ApplyMDL.NoticeMan = null;
                            _ApplyMDL.NoticeCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已决定;
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
    }
}