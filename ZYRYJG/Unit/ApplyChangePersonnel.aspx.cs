using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;
using System.IO;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using Utility;

namespace ZYRYJG.Unit
{
    public partial class ApplyChangePersonnel : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Unit/ApplyList.aspx|County/Agency.aspx|County/BusinessQuery.aspx";
            }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ApplyMDL"] == null ? "" : (ViewState["ApplyMDL"] as ApplyMDL).ApplyID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

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
                ApplyMDL _ApplyMDL = null;
                try
                {
                    if (string.IsNullOrEmpty(Request.QueryString["a"]) == false)//eidt
                    {
                        #region edit

                        _ApplyMDL = ApplyDAL.GetObject(Utility.Cryptography.Decrypt(Request["a"]));//申请表
                        ViewState["ApplyMDL"] = _ApplyMDL;
                        ApplyChangeMDL _ApplyChangeMDL = ApplyChangeDAL.GetObject(Utility.Cryptography.Decrypt(Request["a"]));//变更详细表
                        ViewState["ApplyChangeMDL"] = _ApplyChangeMDL;

                        if (string.IsNullOrEmpty(_ApplyMDL.LastBackResult) == false && _ApplyMDL.ApplyStatus != EnumManager.ApplyStatus.已驳回)
                        {
                            RadGridCheckHistory.MasterTableView.Caption = string.Format("<p style='color:red'>【上次退回记录】{0}</p>", _ApplyMDL.LastBackResult);
                        }
                        ApplyMDL lastApply = ApplyDAL.GetLastApplyObject(_ApplyMDL.ApplyID);
                        if (lastApply != null
                            && _ApplyMDL.ApplyTime.HasValue == true
                            && lastApply.ApplyTypeSub == "执业企业变更"
                            && lastApply.ApplyStatus == "已驳回" 
                            && (
		                            (lastApply.GetResult=="不通过" && lastApply.GetDateTime > _ApplyMDL.ApplyTime.Value.AddYears(-1))
		                            || (lastApply.ExamineResult=="不通过" && lastApply.ExamineDatetime  > _ApplyMDL.ApplyTime.Value.AddYears(-1))
                                )                        
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

                         ZZJGDM_new.Text=_ApplyMDL.ENT_OrganizationsCode;//变更后企业组织机构代码
                         ZZJGDM_old.Text=_ApplyMDL.OldEnt_QYZJJGDM;//变更前企业组织机构代码
                         if (string.IsNullOrEmpty(_ApplyMDL.OldEnt_QYZJJGDM))
                         {
                             if (IfExistRoleID("0") == true)//个人登录后
                             {
                                 
                                 WorkerCheck(WorkerCertificateCode,"1");
                             }
                         }
                         UIHelp.SetData(main, _ApplyMDL, true);
                         UIHelp.SetData(main, _ApplyChangeMDL, true);

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
                        if (jcsjk_QY_ZHXXDAL.SelectCount(cnsql) < 1 )
                        {
                            ViewState["Xsl"] = "新设立";
                            cns.Visible = true;
                        }
                        else
                        {
                            ViewState["Xsl"] = "";
                            cns.Visible = false;
                        }
                        //南静注释  2019-10-29  
                   //     if (IfExistRoleID("2") == true
                   //&& (string.IsNullOrEmpty(Request.QueryString["a"]) == true || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回))
                        if (IfExistRoleID("0") == true //  修改人：南静  2019-10-29
                     && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)//2019-10-21  南静添加 待确认,已申报判断条件
                     )
                        {
                            RadTextBoxPSN_MobilePhone.Enabled = true;
                            RadTextBoxPSN_Email.Enabled = true;
                            //RadDatePickerENT_ContractStartTime.Enabled = true;
                            //RadDatePickerENT_ContractENDTime.Enabled = true;
                            //详细开启控件启用属性
                            //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);//人员电话
                            //UIHelp.SetReadOnly(RadComboBoxNation, false);//民族
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);//邮箱
                            //trFuJanTitel.Visible = true;
                            //trFuJan.Visible = true;
                            divGR.Visible = true;//个人操作按钮 //2019-10-31  南静添加
                            ZZJGDMbtn.Enabled = true;
                        }
                        else {
                            //RadioButtonListENT_ContractType.Enabled = false;
                            RadTextBoxPSN_MobilePhone.Enabled = false;
                            RadTextBoxPSN_Email.Enabled = false;
                            //详细开启控件启用属性
                            UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, true);//人员电话
                            UIHelp.SetReadOnly(RadComboBoxNation, true);//民族
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, true);//邮箱
                            //RadDatePickerENT_ContractStartTime.Enabled = false;
                            //RadDatePickerENT_ContractENDTime.Enabled = false;
                            //trFuJanTitel.Visible = false;
                            //trFuJan.Visible = false;
                            ZZJGDMbtn.Enabled = false;
                        }

                        //2019-10-31  南静添加
                        if (IfExistRoleID("2") == true)//企业
                        {
                            if (string.IsNullOrEmpty(Request["a"]) == false)//edit
                            {
                                if (_ApplyMDL.ENT_OrganizationsCode == ZZJGDM)//现单位
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
                                else //原单位
                                {
                                    if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                                    {
                                        OldQYButtonApply.Text = "取消申报";
                                        divOldQY.Visible = true;
                                    }
                                    if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认)
                                    {
                                        TextBoxOldQYCheckRemark.Text = "提交区县审核";
                                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                        divOldQY.Visible = true;
                                    }
                                }
                                
                            }
                            //企业看不到各级申办人列
                            RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                        }

                        BindFile(_ApplyMDL.ApplyID);
                        BindCheckHistory(_ApplyMDL.ApplyID);
                        SetButtonEnable(_ApplyMDL.ApplyStatus);                       

                        #endregion edit
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

                        if (IfExistRoleID("0") == true)//个人登录后
                        {
                            SetButtonEnable("");
                            //附件信息，人员照片
                            BindFile("0");
                          
                            //RadioButtonListENT_ContractType.SelectedIndex = 0;
                            WorkerCheck(WorkerCertificateCode);
                            divGR.Visible = true;//个人操作按钮 //2019-10-31  南静添加
                            ZZJGDMbtn.Enabled = true;
                            ZZJGDMbtn.Visible = true;
                            
                            //南静注释   2010-10-31
                            //UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(ZZJGDM);
                            //LabelENT_Name.Text = _UnitMDL.ENT_Name;
                            //LabelENT_Correspondence.Text = _UnitMDL.ENT_Correspondence;
                            //LabelENT_Postcode.Text = _UnitMDL.ENT_Postcode;
                            //LabelLinkMan.Text = _UnitMDL.ENT_Contact;
                            //LabelENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                            //LabelFR.Text = _UnitMDL.ENT_Corporate;
                            //LabelEND_Addess.Text = _UnitMDL.END_Addess;
                            //LabelENT_Type.Text = _UnitMDL.ENT_Type;
                            //LabelENT_Sort.Text = _UnitMDL.ENT_Sort;
                            //LabelENT_Grade.Text = _UnitMDL.ENT_Grade;
                            //LabelENT_QualificationCertificateNo.Text = _UnitMDL.ENT_QualificationCertificateNo;

                            //RadTextBoxPSN_MobilePhone.Enabled = true;
                            UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, true);//人员电话
                            //RadComboBoxNation.Enabled = true;
                            UIHelp.SetReadOnly(RadComboBoxNation, true);
                            RadTextBoxPSN_Email.Enabled = true;
                            //RadDatePickerENT_ContractStartTime.Enabled = true;
                            //RadDatePickerENT_ContractENDTime.Enabled = true;
 
                        }
                    }
                    ZZJGDMbtn.CssClass = ZZJGDMbtn.Enabled == true ? "bt_large" : "bt_large btn_no";
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "加载执业企业变更信息失败", ex);
                    return;
                }

                //申请操作权限   南静注释  2019-10-31
                //if (IfExistRoleID("2") == true)
                //{
                //    divQY.Visible = true;
                //    //企业看不到各级申办人列
                //    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                //    //divCheckHistory.Visible = false;

                    
                //}
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")//刷新已上传附件列表
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
          
        }
        //南静添加   2019-10-31
        protected void WorkerCheck(string workeridno,string coun="0")
        {
            //南静注释  2019-10-31
            //if (RadTextBoxPSN_RegisterNO_Find.Text.Trim() == "" || RadTextBoxPSN_RegisterNO_Find.Text.Trim() == null)
            //{
            //    UIHelp.layerAlert(Page, "请输入注册编号！", 5, 0);
            //    return;
            //}
            RadTextBoxPSN_CertificateNO_Find.Text = workeridno;
            if (RadTextBoxPSN_CertificateNO_Find.Text.Trim() == "" || RadTextBoxPSN_CertificateNO_Find.Text.Trim() == null)
            {
                UIHelp.layerAlert(Page, "请输入身份证号！", 5, 0);
                return;
            }
            // int gs = CommonDAL.SelectRowCount("apply", string.Format("and  PSN_CertificateNO='{0}' and ApplyTypeSub='执业企业变更' and NoticeDate !='' and ExamineResult='通过' and dateadd(year,1,NoticeDate)>'{1}'", RadTextBoxPSN_CertificateNO_Find.Text.Trim(), DateTime.Now.Date));
            //查询证书历史表和证书表注册类型为：初始、重新、执业企业变更的且大于一年之内
//            DataTable dtChange = CommonDAL.GetDataTable(string.Format(@"select  top 1 * from (
//            select a.PSN_CertificateNO,a.PSN_RegisteType, a.ENT_ServerID,a.PSN_BeforENT_ServerID,a.PSN_RegistePermissionDate from COC_TOW_Person_BaseInfo_his A  LEFT JOIN COC_TOW_Person_BaseInfo B ON A.PSN_CertificateNO=B.PSN_CertificateNO  where a.PSN_CertificateNO='{0}' AND (a.PSN_RegisteType='01' or a.PSN_RegisteType='05' or (a.PSN_RegisteType='02' and A.ENT_OrganizationsCode !=B.ENT_OrganizationsCode))
//            union  all 
//            select PSN_CertificateNO,PSN_RegisteType, ENT_ServerID,PSN_BeforENT_ServerID,PSN_RegistePermissionDate from COC_TOW_Person_BaseInfo  where PSN_CertificateNO='{0}' and (PSN_RegisteType='01' or PSN_RegisteType='05' or (PSN_RegisteType='02' and PSN_ChangeReason='执业企业变更'))
//            union all 
//            select  PSN_CertificateNO,'02' as PSN_RegisteType ,a.ENT_Name,b.OldENT_Name,NoticeDate from Apply a inner join ApplyChange  b on a.ApplyID=b.ApplyID  where PSN_CertificateNO='{0}' and NoticeDate is not null and  ApplyTypeSub='执业企业变更' and ApplyStatus='已公告' AND ReportCode is not null 
//            ) c where  PSN_CertificateNO='{0}' and (PSN_RegisteType='01' or PSN_RegisteType='05' or (PSN_RegisteType='02' and ENT_ServerID !=PSN_BeforENT_ServerID)) and dateadd(day,-1,dateadd(year,1,PSN_RegistePermissionDate))>'{1}'
//            order by PSN_RegistePermissionDate desc
//            ", RadTextBoxPSN_CertificateNO_Find.Text.Trim(), DateTime.Now.Date.ToString("yyyy-MM-dd")));
            DataTable dtChange = CommonDAL.GetDataTable(string.Format(@"
select  top 1 * from (
    select  PSN_CertificateNO,[ApplyType],ENT_Name,[CodeDate] as PSN_RegistePermissionDate
    from Apply  
    where PSN_CertificateNO='{0}' and  (ApplyType='初始注册' or  ApplyType='重新注册') and ApplyStatus='已公告'
    union all 
    select  PSN_CertificateNO,ApplyTypeSub as ApplyType ,ENT_Name,NoticeDate 
    from Apply 
    where PSN_CertificateNO='{0}' and NoticeDate is not null and  ApplyTypeSub='执业企业变更' and ApplyStatus='已公告' AND ReportCode is not null 
) c 
where  dateadd(day,-1,dateadd(year,1,PSN_RegistePermissionDate))>'{1}'
 order by PSN_RegistePermissionDate desc
            ", RadTextBoxPSN_CertificateNO_Find.Text.Trim(), DateTime.Now.Date.ToString("yyyy-MM-dd")));
            if (dtChange.Rows.Count > 0)
            {
                //UIHelp.layerAlert(Page, string.Format("初始注册、重新注册、执业企业变更未满一年内不允许做执业企业变更！可申报日期为：{0}", Convert.ToDateTime(dtChange.Rows[0]["PSN_RegistePermissionDate"]).AddYears(1).ToString("yyyy-MM-dd")), 5, 0);
                UIHelp.layerAlert(Page, "初始注册、重新注册、执业企业变更未满一年内不允许做执业企业变更！", 5, 0);
                return;
            }
            //南静注释   2019-10-31
            //DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT * FROM [dbo].[View_JZS_TOW_Applying]  where PSN_RegisterNO='{0}' and  PSN_CertificateNO='{1}'", RadTextBoxPSN_RegisterNO_Find.Text.Trim(), RadTextBoxPSN_CertificateNO_Find.Text.Trim()));
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT * FROM [dbo].[View_JZS_TOW_Applying]  where  PSN_CertificateNO='{0}' and PSN_Level <> '二级临时'", RadTextBoxPSN_CertificateNO_Find.Text.Trim()));

            if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ApplyID"] == DBNull.Value || coun=="1")//执业企业人员变更中先判断调入人员有没有在途业务中dt.Rows[0]["ApplyID"]==""表示可以办理业务
            {
                //南静添加  2019-10-31
                if (dt != null && dt.Rows.Count != 0 && dt.Rows[0]["PSN_RegisterNO"] != DBNull.Value)
                {
                    RadTextBoxPSN_RegisterNO_Find.Text = dt.Rows[0]["PSN_RegisterNO"].ToString();
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_RegisterNO(RadTextBoxPSN_RegisterNO_Find.Text.Trim());

                    if (o != null)
                    {
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

                        if (o.ENT_ServerID == UserID)
                        {
                            UIHelp.layerAlert(Page, "此证书已经在您公司名下，无需申请调入，请检查输入是否有误！", 5, 0);
                            return;
                        }
                        if (o.PSN_CertificateValidity.HasValue == true && o.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                        {
                            UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                            return;
                        }

                        if (o.PSN_Level == "二级临时")
                        {
                            if (jcsjk_QY_ZHXXDAL.SelectCount(string.Format(" and sjlx='本地施工企业' and ZZJGDM ='{0}'", ZZJGDM)) < 1)
                            {
                                UIHelp.layerAlert(Page, "二级临时建造师只能注册在本地施工企业，您的企业当前没有本地施工企业资质，无法申请注册!");
                                return;
                            }
                        }

                        LabelPSN_CertificateNO.Text = o.PSN_CertificateNO;
                        LabelPSN_RegisterNO.Text = o.PSN_RegisterNO;
                        LabelPSN_Name.Text = o.PSN_Name;
                        LabelPSN_Sex.Text = o.PSN_Sex;
                        RadTextBoxPSN_MobilePhone.Text = o.PSN_MobilePhone;
                        RadTextBoxPSN_Email.Text = o.PSN_Email;
                       if( RadComboBoxNation.Items.FindItemByValue(o.PSN_National) !=null)
                       {
                           RadComboBoxNation.Items.FindItemByValue(o.PSN_National).Selected = true;
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

                        //查询人的企业信息
                        UnitMDL _UnitMDL = UnitDAL.GetObject(o.ENT_ServerID);
                        LabelOldENT_Name.Text = _UnitMDL.ENT_Name;
                        LabelOldENT_Correspondence.Text = _UnitMDL.END_Addess;
                        LabelOldENT_Postcode.Text = _UnitMDL.ENT_Postcode;
                        LabelOldLinkMan.Text = _UnitMDL.ENT_Contact;
                        LabelOldENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                        ZZJGDM_old.Text = _UnitMDL.ENT_OrganizationsCode;
                        //附件信息，人员照片
                        BindFile("0");
                        //ButtonSave.Enabled = true;
                        //ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                        ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "查无证书，请检查输入是否有误！", 5, 0);
                        return;
                    }
                }
                //南静添加  2019-10-31
                else
                {
                    UIHelp.layerAlert(Page, "未找到该人员的证书信息，请检查输入是否有误", 5, 0);
                    return;
                }

            }
            else
            {

                UIHelp.layerAlert(Page, string.Format("【{0}】正在为注册号为【{1}】的证书办理{2}，无法申请其他注册申请，请自行联系该单位进行协商取消在办业务。"
                   , dt.Rows[0]["NewENT_Name"]
                   , dt.Rows[0]["PSN_RegisterNO"]
                   , (dt.Rows[0]["ApplyTypeSub"] != DBNull.Value ? dt.Rows[0]["ApplyTypeSub"] : dt.Rows[0]["ApplyType"])
                   ), 5, 0);
                return;
            }

        }
        //南静添加  2019-10-31
        //查询现在聘请单位企业信息
        protected void ZZJGDMbtn_Click(object sender, EventArgs e)
        {
            string zzjgdmno=ZZJGDM_check.Text;
            string zzjgdmold = ZZJGDM_old.Text;
            try
            {
                if (!string.IsNullOrEmpty(zzjgdmno))
                {
                    if (zzjgdmno.Trim() != zzjgdmold.Trim())
                    {
                        UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdmno);
                        if (_UnitMDL != null)
                        {
                            LabelENT_Name.Text = _UnitMDL.ENT_Name;
                            //LabelENT_Correspondence.Text = _UnitMDL.ENT_Correspondence;
                            LabelENT_Postcode.Text = _UnitMDL.ENT_Postcode;
                            LabelLinkMan.Text = _UnitMDL.ENT_Contact;
                            LabelENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                            LabelFR.Text = _UnitMDL.ENT_Corporate;
                            LabelEND_Addess.Text = _UnitMDL.END_Addess;
                            LabelENT_Type.Text = _UnitMDL.ENT_Type;
                            LabelENT_Sort.Text = _UnitMDL.ENT_Sort;
                            LabelENT_Grade.Text = _UnitMDL.ENT_Grade;
                            ZZJGDM_new.Text = zzjgdmno;
                            LabelENT_QualificationCertificateNo.Text = _UnitMDL.ENT_QualificationCertificateNo;
                            ButtonSave.Enabled = true;
                            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                        }
                        else
                        {
                            UIHelp.layerAlert(Page, "未查询到相关企业，请检查是否有误！", 5, 0);
                            return;
                        }
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "两家企业组织机构代码一致，不允许发起申请", 5, 0);
                        return;
                    }
                }
                else
                {
                    UIHelp.layerAlert(Page, "请输入企业组织机构代码", 5, 0);
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.layerAlert(Page, "查询企业信息出错，请稍候重试", 5, 0);
                return;
            }
        }

        //检索人员
        //合并后  该方法暂时用不到   南静  2019-10-31
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            if (RadTextBoxPSN_RegisterNO_Find.Text.Trim() == "" || RadTextBoxPSN_RegisterNO_Find.Text.Trim() == null)
            {
                UIHelp.layerAlert(Page, "请输入注册编号！", 5, 0);
                return;
            }
            if (RadTextBoxPSN_CertificateNO_Find.Text.Trim() == "" || RadTextBoxPSN_CertificateNO_Find.Text.Trim() == null)
            {
                UIHelp.layerAlert(Page, "请输入身份证号！", 5, 0);
                return;
            }
             // int gs = CommonDAL.SelectRowCount("apply", string.Format("and  PSN_CertificateNO='{0}' and ApplyTypeSub='执业企业变更' and NoticeDate !='' and ExamineResult='通过' and dateadd(year,1,NoticeDate)>'{1}'", RadTextBoxPSN_CertificateNO_Find.Text.Trim(), DateTime.Now.Date));
            //查询证书历史表和证书表注册类型为：初始、重新、执业企业变更的且大于一年之内
            DataTable dtChange = CommonDAL.GetDataTable(string.Format(@"select  top 1 * from (
            select a.PSN_CertificateNO,a.PSN_RegisteType, a.ENT_ServerID,a.PSN_BeforENT_ServerID,a.PSN_RegistePermissionDate from COC_TOW_Person_BaseInfo_his A  LEFT JOIN COC_TOW_Person_BaseInfo B ON A.PSN_CertificateNO=B.PSN_CertificateNO  where a.PSN_CertificateNO='{0}' AND (a.PSN_RegisteType='01' or a.PSN_RegisteType='05' or (a.PSN_RegisteType='02' and A.ENT_OrganizationsCode !=B.ENT_OrganizationsCode))
            union  all 
            select PSN_CertificateNO,PSN_RegisteType, ENT_ServerID,PSN_BeforENT_ServerID,PSN_RegistePermissionDate from COC_TOW_Person_BaseInfo  where PSN_CertificateNO='{0}' and (PSN_RegisteType='01' or PSN_RegisteType='05' or (PSN_RegisteType='02' and PSN_ChangeReason='执业企业变更'))
            union all 
            select  PSN_CertificateNO,'02' as PSN_RegisteType ,a.ENT_Name,b.OldENT_Name,NoticeDate from Apply a inner join ApplyChange  b on a.ApplyID=b.ApplyID  where PSN_CertificateNO='{0}' and NoticeDate is not null and  ApplyTypeSub='执业企业变更' and ApplyStatus='已公告' AND ReportCode is not null 
            ) c where  PSN_CertificateNO='{0}' and (PSN_RegisteType='01' or PSN_RegisteType='05' or (PSN_RegisteType='02' and ENT_ServerID !=PSN_BeforENT_ServerID)) and dateadd(day,-1,dateadd(year,1,PSN_RegistePermissionDate))>'{1}'
            order by PSN_RegistePermissionDate desc
            ", RadTextBoxPSN_CertificateNO_Find.Text.Trim(), DateTime.Now.Date));
            if (dtChange.Rows.Count > 0)
            {
                //UIHelp.layerAlert(Page, string.Format("初始注册、重新注册、执业企业变更未满一年内不允许做执业企业变更！可申报日期为：{0}", Convert.ToDateTime(dtChange.Rows[0]["PSN_RegistePermissionDate"]).AddYears(1).ToString("yyyy-MM-dd")), 5, 0);
                UIHelp.layerAlert(Page, "初始注册、重新注册、执业企业变更未满一年内不允许做执业企业变更！", 5, 0);
                return;
            }
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT * FROM [dbo].[View_JZS_TOW_Applying]  where PSN_RegisterNO='{0}' and  PSN_CertificateNO='{1}'", RadTextBoxPSN_RegisterNO_Find.Text.Trim(), RadTextBoxPSN_CertificateNO_Find.Text.Trim()));
            
            if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ApplyID"] == DBNull.Value)//执业企业人员变更中先判断调入人员有没有在途业务中dt.Rows[0]["ApplyID"]==""表示可以办理业务
            {
                COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_RegisterNO(RadTextBoxPSN_RegisterNO_Find.Text.Trim());
              
                if (o != null)
                {
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

                    if (o.ENT_ServerID == UserID)
                    {
                        UIHelp.layerAlert(Page, "此证书已经在您公司名下，无需申请调入，请检查输入是否有误！", 5, 0);
                        return;
                    }
                    if (o.PSN_CertificateValidity.HasValue == true && o.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                        return;
                    }

                    if (o.PSN_Level == "二级临时")
                    {
                        if (jcsjk_QY_ZHXXDAL.SelectCount(string.Format(" and sjlx='本地施工企业' and ZZJGDM ='{0}'", ZZJGDM)) < 1)
                        {
                            UIHelp.layerAlert(Page, "二级临时建造师只能注册在本地施工企业，您的企业当前没有本地施工企业资质，无法申请注册!");
                            return;
                        }  
                    }

                    LabelPSN_CertificateNO.Text = o.PSN_CertificateNO;
                    LabelPSN_RegisterNO.Text = o.PSN_RegisterNO;
                    LabelPSN_Name.Text = o.PSN_Name;
                    LabelPSN_Sex.Text = o.PSN_Sex;
                    RadTextBoxPSN_MobilePhone.Text = o.PSN_MobilePhone;
                    RadTextBoxPSN_Email.Text = o.PSN_Email;
                    //查询人的企业信息
                    UnitMDL _UnitMDL = UnitDAL.GetObject(o.ENT_ServerID);
                    LabelOldENT_Name.Text = _UnitMDL.ENT_Name;
                    LabelOldENT_Correspondence.Text = _UnitMDL.ENT_Correspondence;
                    LabelOldENT_Postcode.Text = _UnitMDL.ENT_Postcode;
                    LabelOldLinkMan.Text = _UnitMDL.ENT_Contact;
                    LabelOldENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                    //附件信息，人员照片
                    BindFile("0");
                   // ButtonSave.Enabled = true;
                    //ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                }
                else
                {
                    UIHelp.layerAlert(Page, "查无证书，请检查输入是否有误！", 5, 0);
                    return;
                }
            }
            else
            {

                UIHelp.layerAlert(Page, string.Format("【{0}】正在为注册号为【{1}】的证书办理{2}，无法申请其他注册申请，请自行联系该单位进行协商取消在办业务。"
                   , dt.Rows[0]["NewENT_Name"]
                   , dt.Rows[0]["PSN_RegisterNO"]
                   , (dt.Rows[0]["ApplyTypeSub"] != DBNull.Value ? dt.Rows[0]["ApplyTypeSub"] : dt.Rows[0]["ApplyType"])
                   ), 5, 0);
                return;
            }
            
        }

        //保存申报
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string zzjgdmno = ZZJGDM_new.Text;//变更后企业组织机构代码
            string zzjgdmold = ZZJGDM_old.Text;//变更前企业组织机构代码

            COC_TOW_Person_BaseInfoMDL person =null;
            if (ViewState["ApplyMDL"] == null)
            {
                person = ViewState["COC_TOW_Person_BaseInfoMDL"] as COC_TOW_Person_BaseInfoMDL;
            }

            #region 申报规则校验

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
            //}else if(RadioButtonListENT_ContractType.SelectedValue != "1" && RadioButtonListENT_ContractType.SelectedValue !="2"&& RadioButtonListENT_ContractType.SelectedValue != "3"){
            //    UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
            //    return;
            //}
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
            if (string.IsNullOrEmpty(ZZJGDM_old.Text))
            {
                UIHelp.layerAlert(Page, "无法获取原单位信息", 5, 0);
                return;
            }
            if (string.IsNullOrEmpty(ZZJGDM_new.Text))
            {
                UIHelp.layerAlert(Page, "请检索现单位信息", 5, 0);
                return;
            }
            if (RadComboBoxNation.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "保存失败，民族不可为空！", 5, 0);
                return;
            }

            if (Utility.Check.CheckBirthdayLimit(0, LabelPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满65周岁前90天起,不再允许发起二级建造师初始、重新、延续、增项、执业企业变更注册申请。", 5, 0);
                return;
            }

            if(ViewState["ApplyMDL"] == null && jcsjk_RY_JZS_ZSSDDAL.IfLocking(person.PSN_RegisterNO)==true)
            {
                UIHelp.layerAlert(Page, "无法申请，证书处于在施项目锁定中，请先办理在施项目解锁！", 5, 0);
                return;
            }

            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdmno);
            //所属区县
            if (string.IsNullOrEmpty(_UnitMDL.ENT_City))
            {
                UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                return;
            }

            if (Utility.Check.CheckIfSubUnit(LabelENT_Name.Text) == true)
            {
                UIHelp.layerAlert(Page, UIHelp.Tip_SubUnitForbid, 5, 0);
                return;
            }
                       
            if (UnitDAL.CheckGongShang(zzjgdmno) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", LabelENT_Name.Text));
                return;
            }
            #endregion      

            if (ViewState["ApplyMDL"] == null)
            {
                if (ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", LabelPSN_RegisterNO.Text.Trim(), EnumManager.ApplyStatus.未申报, EnumManager.ApplyStatus.待确认)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }
            
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            _ApplyMDL.Valid = 1;
            _ApplyMDL.ApplyType = EnumManager.ApplyType.变更注册;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
            //_ApplyMDL.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);  //2019-11-27南静添加            
            _ApplyMDL.ApplyTypeSub = EnumManager.ApplyType.执业企业变更;//变更类别
            ApplyChangeMDL _ApplyChangeMDL = ViewState["ApplyChangeMDL"] == null ? new ApplyChangeMDL() : (ApplyChangeMDL)ViewState["ApplyChangeMDL"];//详细表
            UIHelp.GetData(EditTable, _ApplyChangeMDL);

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            if (string.IsNullOrEmpty(_ApplyMDL.OldEnt_QYZJJGDM))
            {
                if (IfExistRoleID("0") == true)//个人登录后
                {

                    WorkerCheck(WorkerCertificateCode, "1");
                }
            }
            //变更前企业编码
            _ApplyMDL.OldEnt_QYZJJGDM = (ZZJGDM_old.Text).Trim();
            try
            {
                //现企业信息,企业类型,现企业名称
                _ApplyChangeMDL.ENT_Name = LabelENT_Name.Text;
                //_ApplyChangeMDL.ENT_Correspondence = LabelENT_Correspondence.Text;
                _ApplyChangeMDL.ENT_Postcode = LabelENT_Postcode.Text;
                _ApplyChangeMDL.LinkMan = LabelLinkMan.Text;
                _ApplyChangeMDL.ENT_Telephone = LabelENT_Telephone.Text;
                _ApplyChangeMDL.FR = LabelFR.Text;
                _ApplyChangeMDL.END_Addess = LabelEND_Addess.Text;
                _ApplyChangeMDL.ENT_Type = LabelENT_Type.Text;
                _ApplyChangeMDL.ENT_Sort = LabelENT_Sort.Text;
                _ApplyChangeMDL.ENT_Grade = LabelENT_Grade.Text;
                _ApplyChangeMDL.ENT_QualificationCertificateNo = LabelENT_QualificationCertificateNo.Text;
                
                //DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT * FROM UNIT WHERE (ENT_OrganizationsCode ='{0}' or ENT_OrganizationsCode like '________{0}_')",ZZJGDM));
                //申请表企业名称,企业组织机构代码，所属市区,建造师ID
                _ApplyMDL.ENT_Name = _UnitMDL.ENT_Name;
                _ApplyMDL.ENT_OrganizationsCode = _UnitMDL.ENT_OrganizationsCode;
                _ApplyMDL.ENT_City = _UnitMDL.ENT_City;
                _ApplyMDL.ENT_ServerID = _UnitMDL.UnitID;
                //_ApplyMDL.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;

                //_ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                ////劳动合同结束时间
                //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                //{
                //    _ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                //}
                //else
                //{
                //    _ApplyMDL.ENT_ContractENDTime = null;
                //}
               
                if (ViewState["ApplyMDL"] == null)//new
                {
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;                    
                    
                    //变更前企业编码
                   // _ApplyMDL.OldEnt_QYZJJGDM = zzjgdmold;
                    //申请表姓名，性别,证件类别，证件号码,建造师ID
                    _ApplyMDL.PSN_Name = LabelPSN_Name.Text;
                    _ApplyMDL.PSN_Sex = LabelPSN_Sex.Text;
                    _ApplyMDL.PSN_CertificateType = person.PSN_CertificateType;
                    _ApplyMDL.PSN_CertificateNO = person.PSN_CertificateNO;
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;
                    //申请专业
                    _ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;
                    _ApplyChangeMDL.ApplyID = _ApplyMDL.ApplyID;
                    _ApplyMDL.PSN_RegisterNo = person.PSN_RegisterNO;
                    _ApplyChangeMDL.ValidDate = person.PSN_CertificateValidity;
                    if (string.IsNullOrEmpty(_ApplyChangeMDL.PSN_MobilePhone))
                    {
                          _ApplyChangeMDL.PSN_MobilePhone = person.PSN_MobilePhone;
                    }
                    if (string.IsNullOrEmpty(_ApplyChangeMDL.PSN_Email))
                    {
                         _ApplyChangeMDL.PSN_Email = person.PSN_Email;
                    }                   
                  
                    //变更原因
                    _ApplyChangeMDL.ChangeReason = EnumManager.ApplyType.执业企业变更;
                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran,  EnumManager.ApplyType.执业企业变更);
                    //原企业信息
                    _ApplyChangeMDL.OldENT_Name = LabelOldENT_Name.Text;
                    _ApplyChangeMDL.OldENT_Correspondence = LabelOldENT_Correspondence.Text;
                    _ApplyChangeMDL.OldENT_Postcode = LabelOldENT_Postcode.Text;
                    _ApplyChangeMDL.OldLinkMan = LabelOldLinkMan.Text;
                    _ApplyChangeMDL.OldENT_Telephone = LabelOldENT_Telephone.Text;
                 
                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyChangeDAL.Insert(tran, _ApplyChangeMDL);

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
                    string cnsql = string.Format("and (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='工程勘察' OR SJLX='工程设计') and [ZZJGDM]='{0}'", zzjgdmno);
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

                    ApplyDAL.Update(tran, _ApplyMDL);
                    ApplyChangeDAL.Update(tran, _ApplyChangeMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyChangeMDL"] = _ApplyChangeMDL;

                BindFile(ApplyID);

                //操着按钮控制
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                
                UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请保存成功", string.Format("姓名：{0}，身份证号：{1}，保存时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.ParentAlert(Page, "保存成功，请上传必要的附件，打印申请单并加盖原单位及新单位公章后扫描上传。（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）",true);
               
                //if (_ApplyChangeMDL != null)
                //{
                //    RadTextBoxPSN_MobilePhone.Text = _ApplyChangeMDL.PSN_MobilePhone;
                //    RadTextBoxPSN_Email.Text = _ApplyChangeMDL.PSN_Email;
                //    RadComboBoxNation
                //    RadTextBoxNation.Text = _ApplyChangeMDL.Nation;
                //}
                //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存变更注册，执业企业变更申请失败", ex);
            }
        }

        //提交到单位确认  2019-10-31  南静添加
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {            
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            if (ButtonUnit.Text != "取消申报")
            {
                ApplyChangeMDL _ApplyChangeMDL =(ApplyChangeMDL)ViewState["ApplyChangeMDL"] ;
                if(_ApplyChangeMDL.ValidDate.Value.AddDays(1)< DateTime.Now)
                {
                    UIHelp.layerAlert(Page, "证书已失效，无法提交申请变更！",5, 0);
                    return;
                }

                //必须上传附件检查
                #region
                System.Collections.Hashtable fj = new System.Collections.Hashtable();
                if (ViewState["Xsl"].ToString() == "新设立")
                {
                    fj = new System.Collections.Hashtable{
                   {EnumManager.FileDataTypeName.一寸免冠照片,0},
                   {EnumManager.FileDataTypeName.证件扫描件,0},
                   //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                   //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},                
                   {EnumManager.FileDataTypeName.解除劳动合同证明,0},
                   //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                   {EnumManager.FileDataTypeName.申请表扫描件,0},
                   {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};
                }
                else
                {
                    fj = new System.Collections.Hashtable{
                    {EnumManager.FileDataTypeName.一寸免冠照片,0},
                    {EnumManager.FileDataTypeName.证件扫描件,0},
                    //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                    //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},                
                    {EnumManager.FileDataTypeName.解除劳动合同证明,0},
                    //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    {EnumManager.FileDataTypeName.申请表扫描件,0}};

                }

                ////原单位允许调出后才可申报
                //if (string.IsNullOrEmpty(_ApplyMDL.OldUnitCheckResult) || _ApplyMDL.OldUnitCheckResult == "不通过")
                //{
                //    UIHelp.layerAlert(Page, "原单位尚未同意调出,请联系原单位！", 5, 0);
                //    return;
                //}


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
                #endregion
            }               

            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            if (ButtonUnit.Text == "取消申报")
            {
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                _ApplyMDL.ApplyTime = null;
                //南静  2019-10-31 添加
                _ApplyMDL.OldUnitCheckResult = null;
                _ApplyMDL.OldUnitCheckRemark = null;
                _ApplyMDL.OldUnitCheckTime = null;

                //南静  2019-11-08
                _ApplyMDL.newUnitCheckTime = null;
                _ApplyMDL.newUnitCheckResult = null;
                _ApplyMDL.newUnitCheckRemark = null;
            }
            else
            {
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
                _ApplyMDL.ApplyTime = DateTime.Now;
                //南静  2019-10-31  添加
                _ApplyMDL.OldUnitCheckResult = null;
                _ApplyMDL.OldUnitCheckRemark = null;
                _ApplyMDL.OldUnitCheckTime = null;
                //南静  2019-11-08
                _ApplyMDL.newUnitCheckTime = null;
                _ApplyMDL.newUnitCheckResult = null;
                _ApplyMDL.newUnitCheckRemark = null;

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
                UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}&i=执业企业变更';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID)); 

            }
            else
            {
                UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "撤销成功！", 6, 2000);
                //RadDatePickerENT_ContractStartTime.Enabled = true;
                //RadDatePickerENT_ContractENDTime.Enabled = true;
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认，原单位申报 or 撤销申报   南静添加  2019-11-06
        protected void OldQYButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            if (OldQYButtonApply.Text == "取消申报")
            {
                //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;//南静注释 2019-10-31
                //_ApplyMDL.ApplyTime = null;//南静注释 2019-10-31
                //南静  2019-10-31  添加
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
            }
            else
            {
                //南静  2019-10-31  添加
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = RadioButtonListOldQYCheckResult.SelectedValue; ;//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = TextBoxOldQYCheckRemark.Text.Trim();//现单位审核意见

                if (RadioButtonListOldQYCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                {
                    TextBoxOldQYCheckRemark.Visible = true;
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                }
                else
                {
                    ApplyChangeMDL _ApplyChangeMDL = (ApplyChangeMDL)ViewState["ApplyChangeMDL"];
                    if (_ApplyChangeMDL.ValidDate.Value.AddDays(1) < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, "证书已失效，无法提交申请变更！", 5, 0);
                        return;
                    }

                    if (_ApplyMDL.newUnitCheckTime > Convert.ToDateTime("2019-01-01"))
                    {
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;              
                    }

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

            try
            {
                ApplyDAL.Update(_ApplyMDL);
                ViewState["ApplyMDL"] = _ApplyMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                //人员照片
                BindFile(_ApplyMDL.ApplyID);

                if (_ApplyMDL.ApplyStatus == "已申报" || _ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    //UIHelp.layerAlert(Page, "申报成功！", 6, 2000);
                    // Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                    //RadDatePickerENT_ContractStartTime.Enabled = true;
                    //RadDatePickerENT_ContractENDTime.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "执业企业变更申请失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //变更单位（现单位）申报 or 撤销申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            //南静注释  2019-10-31 
            //if (ButtonApply.Text != "撤销申报")
            //{
            //    //必须上传附件检查
            //    System.Collections.Hashtable fj = new System.Collections.Hashtable();
            //    #region
            //    //必须上传附件集合
            //    if (ViewState["Xsl"].ToString() == "新设立")
            //    {
            //        fj = new System.Collections.Hashtable{
            //       {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //       {EnumManager.FileDataTypeName.证件扫描件,0},
            //       {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //       {EnumManager.FileDataTypeName.执业资格证书扫描件,0},                
            //       {EnumManager.FileDataTypeName.解除劳动合同证明,0},
            //       {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //       {EnumManager.FileDataTypeName.申请表扫描件,0},
            //       {EnumManager.FileDataTypeName.新设立企业建造师注册承诺书,0}};
            //    }
            //    else
            //    {
            //        fj = new System.Collections.Hashtable{
            //        {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //        {EnumManager.FileDataTypeName.证件扫描件,0},
            //        {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //        {EnumManager.FileDataTypeName.执业资格证书扫描件,0},                
            //        {EnumManager.FileDataTypeName.解除劳动合同证明,0},
            //        {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //        {EnumManager.FileDataTypeName.申请表扫描件,0}};

            //    }

            //    //原单位允许调出后才可申报
            //    if (string.IsNullOrEmpty(_ApplyMDL.OldUnitCheckResult)||_ApplyMDL.OldUnitCheckResult=="不通过")
            //    {
            //       UIHelp.layerAlert(Page, "原单位尚未同意调出,请联系原单位！", 5, 0);
            //       return;
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
            //#endregion
            try
            {
                _ApplyMDL.XGR = UserName;
                _ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonApply.Text == "取消申报")
                {
                    //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;//南静注释 2019-10-31
                    //_ApplyMDL.ApplyTime = null;//南静注释 2019-10-31
                    //南静  2019-11-06  添加
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                    _ApplyMDL.newUnitCheckTime = DateTime.Now;//现单位申请时间
                    _ApplyMDL.newUnitCheckResult = "不同意";//现单位审核结果
                    _ApplyMDL.newUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    //南静  2019-11-06  添加
                    _ApplyMDL.newUnitCheckTime = DateTime.Now;//变更单位申请时间
                    _ApplyMDL.newUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//变更单位审核结果
                    _ApplyMDL.newUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//变更单位审核意见

                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldUnitCheckRemark.Visible = true;
                        _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                    }
                    else
                    {

                        if (_ApplyMDL.OldUnitCheckTime > Convert.ToDateTime("2019-01-01"))
                        {
                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                        }

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

                if (_ApplyMDL.ApplyStatus == "已申报" || _ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    //UIHelp.layerAlert(Page, "申报成功！", 6, 2000);
                    // Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                    //RadDatePickerENT_ContractStartTime.Enabled = true;
                    //RadDatePickerENT_ContractENDTime.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "执业企业变更申请失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"].Replace(" green", "");
            //step_原单位确认.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace("green", "");
           
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
            step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");


            switch (ApplyStatus)
            {
                case "未申报":
                case "已驳回":
                    //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? null : ViewState["ApplyMDL"] as ApplyMDL;
                    //if (ViewState["ApplyMDL"] != null && _ApplyMDL.OldUnitCheckTime.HasValue == true)
                    //{
                    //    step_原单位确认.Attributes["class"] += " green";
                    //}
                    //else
                    //{
                    //    step_未申报.Attributes["class"] += " green";
                    //}
                    step_未申报.Attributes["class"] += " green";
                    break;
                    //南静添加      2019-10-31 
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
                case "已公告":
                    step_已上报.Attributes["class"] += " green";
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
                    //2019-10-31   南静添加
                case "":
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "申 报";
                      ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";
                    ZZJGDMbtn.Visible = true;
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

                    //现企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确定";
                    //原企业  南静 2019-11-06
                    OldQYButtonApply.Enabled = true;
                    OldQYButtonApply.Text = "确定";
                    break;
                case EnumManager.ApplyStatus.已申报:
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "撤销申报";
                     ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonOutput.Enabled = false;
                    
                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";
                    //原企业  南静 2019-11-06
                    OldQYButtonApply.Enabled = true;
                    OldQYButtonApply.Text = "取消申报";
                    break;
                case EnumManager.ApplyStatus.已驳回:
                    ButtonSave.Enabled = true;
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";

                    //企业
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确定";
                    //原企业  南静 2019-11-06
                    OldQYButtonApply.Enabled = false;
                    OldQYButtonApply.Text = "确定";
                    ZZJGDMbtn.Visible = true;
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    OldQYButtonApply.Enabled = false;
                    OldQYButtonApply.Text = "撤销申报";
                    break;
            }
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
            OldQYButtonApply.CssClass = OldQYButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";

            if (string.IsNullOrEmpty(ApplyID) == true)
            {
                trButtonQuery.Visible = true;
            }
            else
            {
                trButtonQuery.Visible = false;
            }

            if (ApplyStatus == EnumManager.ApplyStatus.已受理 || ApplyStatus == EnumManager.ApplyStatus.区县审查)
            {
                //注册室管理员
                if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                {
                    UIHelp.SetJZSReturnStatusList(RadComboBoxReturnApplyStatus, ApplyStatus);
                    divSendBack.Visible = true;//后退
                }
            }
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

        //区县受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

            #region 查询证书是否锁定
            if (RadioButtonListApplyStatus.SelectedValue == "通过")
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
            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }
            if (_ApplyMDL.CheckZSSD == 1)
            {
                if (_ApplyMDL.ApplyStatus!=EnumManager.ApplyStatus.已驳回)
                {
                    UIHelp.layerAlert(Page, "有在施锁定项目、不予许做执业企业变更！", 5, 0);
                    return;
                }
                
            }
               
            #region  检查是否为新设立
            if (_ApplyMDL.CheckXSL == 1)
            {
                _NewSetUpMDL.ApplyId = _ApplyMDL.ApplyID;
                //_NewSetUpMDL.ENT_City = _ApplyMDL.ENT_City;
                //_NewSetUpMDL.ENT_Name = _ApplyMDL.ENT_Name;
                //_NewSetUpMDL.ENT_OrganizationsCode = _ApplyMDL.ENT_OrganizationsCode;
                //_NewSetUpMDL.Psn_Name = _ApplyMDL.PSN_Name;
                _NewSetUpMDL.XslDateTime = DateTime.Now;
                _NewSetUpMDL.ApplyType = _ApplyMDL.ApplyTypeSub;
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
            UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更受理", string.Format("姓名：{0}，身份证号：{1}，受理时间：{2}。审批结果：{3}，审批意见：{4}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date
                 , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
           // UIHelp.ParentAlert(Page, "受理成功！", true);

            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
            //Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
        }

        //区县审查
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //申请表
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            //移动到汇总上报时校验
            //if (_ApplyMDL.CheckXSL.HasValue == false || _ApplyMDL.CheckXSL.Value == 1)
            //{
            //    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
            //    return;
            //}
            #region 查询证书是否锁定
            bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
            }
            #endregion

            _ApplyMDL.ExamineDatetime = DateTime.Now;
            _ApplyMDL.ExamineMan = UserName;
            _ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
            _ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;
            _ApplyMDL.XGSJ = _ApplyMDL.ExamineDatetime;
            _ApplyMDL.XGR = UserName;
            if (RadioButtonListExamineResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _ApplyMDL.LastBackResult = string.Format("{0}区县驳回申请，驳回说明：{1}", _ApplyMDL.ExamineDatetime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxExamineRemark1.Text.Trim());
            }
            try
            {
                //更新申请表
                ApplyDAL.Update(_ApplyMDL); 
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更区县审查成功", string.Format("姓名：{0}，身份证号：{1}，审查时间：{2}。审批结果：{3}，审批意见：{4}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date
                 , _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "区县审查成功！", true);
        }

        //打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = "";
                string fileName = "";
                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                if (_ApplyMDL.PSN_Level == "二级")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师执业企业变更注册申请表.docx");
                    fileName = "北京市二级注册建造师执业企业变更注册申请表";
                }
                if (_ApplyMDL.PSN_Level == "二级临时")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师临时执业证书执业企业变更注册申请表.docx");
                    fileName = "北京市二级建造师临时执业证书执业企业变更注册申请表";
                }
                

                //string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师执业企业变更注册申请表.docx");
                //string fileName = "北京市二级注册建造师执业企业变更注册申请表";
                //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
               
                ApplyChangeMDL _ApplyChangeMDL = ViewState["ApplyChangeMDL"] as ApplyChangeMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                UnitMDL u= UnitDAL.GetObjectByPSN_RegisterNo(_ApplyMDL.PSN_RegisterNo);
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyChangeMDL);
                var ht = PrintDocument.GetProperties(o);
                
                if (_FileInfoMDL != null)
                {
                    ht["photo"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else 
                { 
                    ht["photo"] = ""; 
                }

                if (ht["photo"].ToString() == "")
                {
                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
                    return;
                }
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
                


                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["ValidDate"] = _ApplyChangeMDL.ValidDate == null ? "" : ((DateTime)_ApplyChangeMDL.ValidDate).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractStartTime"] = _ApplyMDL.ENT_ContractStartTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractStartTime).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractENDTime"] = _ApplyMDL.ENT_ContractENDTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractENDTime).ToString("yyyy年MM月dd日");
                ht["ENT_Corporate"] = u.ENT_Corporate;
                ht["OldENT_Correspondence"] = _ApplyChangeMDL.OldENT_Correspondence;
                ht["oldENT_Sort"] = u.ENT_Sort;
                ht["oldENT_Grade"] = u.ENT_Grade;
                ht["oldENT_QualificationCertificateNo"] = u.ENT_QualificationCertificateNo;
                ht["oldENT_Type"] = u.ENT_Type;
                ht["Birthday"] = _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate == null ? "" : ((DateTime)_COC_TOW_Person_BaseInfoMDL.PSN_BirthDate).ToString("yyyy年MM月dd日");
                //ht["Nation"] = _COC_TOW_Person_BaseInfoMDL.PSN_National;


                //级联变更证书信息
                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(_ApplyMDL.ENT_OrganizationsCode, true);//（变更到企业必须有本地建筑企业资质）
                DataTable dtOtherCert = null;
                if (string.IsNullOrEmpty(UnitName) ==false)
                {

                    dtOtherCert = CommonDAL.GetDataTable(string.Format(@"
                    SELECT [POSTNAME],[CERTIFICATECODE] FROM [dbo].[CERTIFICATE] 
                    where [WORKERCERTIFICATECODE]='{0}'  and POSTTYPEID=1 and POSTID <>147
                          and VALIDENDDATE > dateadd(day,-1,getdate())  and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                          and [UNITCODE]<>'{1}'", _ApplyMDL.PSN_CertificateNO, _ApplyMDL.ENT_OrganizationsCode
                                            ));
                }
              
                if (dtOtherCert != null && dtOtherCert.Rows.Count>0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for(int i=0;i< dtOtherCert.Rows.Count;i++)
                    {
                        sb.Append(string.Format("{0}：{1}", dtOtherCert.Rows[i]["PostName"], dtOtherCert.Rows[i]["CERTIFICATECODE"]));
                        if(i<dtOtherCert.Rows.Count -1)
                        {
                            sb.Append("\n");
                        }
                    }

                    ht["OtherChange"] = sb.ToString();
                }
                else
                {
                    ht["OtherChange"] = "无";
                }
                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印执业企业信息变更Word失败！", ex);
            }
        }

        //删除功能
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            if (_ApplyMDL.ApplyStatus ==EnumManager.ApplyStatus.未申报  || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 )
            {
                ApplyDAL.Delete(_ApplyMDL.ApplyID);
                ViewState.Remove("ApplyMDL");
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请表删除成功", string.Format("姓名：{0}，身份证号：{1}，审查时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.ParentAlert(Page, "删除成功！", true);
            }
            else
            {
                UIHelp.ParentAlert(Page, "当前不在未申报状态，不能删除！", true);
            }
        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyID">申请ID</param>
        /// <param name="PSN_RegisterNO">注册编号</param>
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
            UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}，删除时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
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
                    ZZJGDM_check.Text = _UnitMDL.ENT_OrganizationsCode;  //单位组织机构代码
                }
            }
            else
            {
                UIHelp.layerAlert(Page, "未查询到您输入的企业,请核实后重新输入！", 5, 0);
                ZZJGDM_check.Text = "";  //单位组织机构代码
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
                   , LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.GetDateTime, _ApplyMDL.GetResult, _ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                           , LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.ExamineDatetime, _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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