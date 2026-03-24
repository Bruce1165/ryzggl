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
using System.Reflection;

namespace ZYRYJG.zjs
{
    public partial class zjsApplyChangeUnit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsApplyList.aspx|zjs/zjsAgency.aspx|zjs/zjsBusinessQuery.aspx";
            }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["zjs_ApplyMDL"] == null ? "" : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                zjs_ApplyMDL _zjs_ApplyMDL = null;
                try
                {
                    if (string.IsNullOrEmpty(Request.QueryString["a"]) == false)//eidt
                    {
                        List<zjs_ApplyMDL> list = zjs_ApplyDAL.GetListObjectChangeUnit(Utility.Cryptography.Decrypt(Request["a"]));
                        if (list.Count > 1)
                        {
                            ViewState["zjs_ApplyMDL2"] = list[1];
                            LabelPSN_RegisterNo2.Text = list[1].PSN_RegisterNo;
                            LabelPSN_RegisteProfession2.Text = list[1].PSN_RegisteProfession;

                            zjs_ApplyChangeMDL _zjs_ApplyChangeMDL2 = zjs_ApplyChangeDAL.GetObject(list[1].ApplyID);//变更详细表
                            ViewState["zjs_ApplyChangeMDL2"] = _zjs_ApplyChangeMDL2;
                            LabelValidDate2.Text = _zjs_ApplyChangeMDL2.ValidDate.Value.ToString("yyyy-MM-dd");
                            trZhuanYe2.Visible = true;
                        }

                        #region edit

                        _zjs_ApplyMDL = list[0];//申请表
                        ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                        zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = zjs_ApplyChangeDAL.GetObject(list[0].ApplyID);//变更详细表

                        ViewState["zjs_ApplyChangeMDL"] = _zjs_ApplyChangeMDL;

                        if (string.IsNullOrEmpty(_zjs_ApplyMDL.LastBackResult) == false && _zjs_ApplyMDL.ApplyStatus != EnumManager.ZJSApplyStatus.已驳回)
                        {
                            RadGridCheckHistory.MasterTableView.Caption = string.Format("【上次退回记录】{0}", _zjs_ApplyMDL.LastBackResult);
                        }

                        //ZZJGDM_new.Text = _zjs_ApplyMDL.ENT_OrganizationsCode;//变更后企业组织机构代码
                        //ZZJGDM_old.Text = _zjs_ApplyMDL.OldEnt_QYZJJGDM;//变更前企业组织机构代码
                        //if (string.IsNullOrEmpty(_zjs_ApplyMDL.OldEnt_QYZJJGDM))
                        //{
                        //    if (IfExistRoleID("0") == true)//个人登录后
                        //    {
                        //        WorkerCheck(WorkerCertificateCode, "1");
                        //    }
                        //}

                        

                        if (IfExistRoleID("0") == true
         && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
         )
                        {
                            UIHelp.SetData(main, _zjs_ApplyMDL, true);
                            UIHelp.SetData(main, _zjs_ApplyChangeMDL, true);                           
                            //详细开启控件启用属性
                            //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);//人员电话

                            //trFuJanTitel.Visible = true;
                            //trFuJan.Visible = true;
                            divGR.Visible = true;//个人操作按钮 
                            trSelectUnit.Visible = true;
                        }
                        else
                        {
                            UIHelp.SetData(main, _zjs_ApplyMDL, true);
                            UIHelp.SetData(main, _zjs_ApplyChangeMDL, true);
                          
                            //trFuJanTitel.Visible = false;
                            //trFuJan.Visible = false;
                            trSelectUnit.Visible = false;
                        }
                        HiddenFieldOldEnt_QYZJJGDM.Value = _zjs_ApplyMDL.OldEnt_QYZJJGDM;
                        HiddenFieldENT_OrganizationsCode.Value = _zjs_ApplyMDL.ENT_OrganizationsCode;
                        HiddenFieldENT_City.Value = _zjs_ApplyMDL.ENT_City;

                        if (IfExistRoleID("2") == true)//企业
                        {
                            if (string.IsNullOrEmpty(Request["a"]) == false)//edit
                            {
                                if (_zjs_ApplyMDL.ENT_OrganizationsCode == SHTJXYDM)//现单位
                                {
                                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                                    {
                                        ButtonApply.Text = "取消申报";
                                        divUnit.Visible = true;
                                    }
                                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认)
                                    {
                                        TextBoxOldUnitCheckRemark.Text = "提交审核";
                                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                        divUnit.Visible = true;
                                    }
                                }
                                else if (_zjs_ApplyMDL.OldEnt_QYZJJGDM == SHTJXYDM) //原单位
                                {
                                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                                    {
                                        OldQYButtonApply.Text = "取消申报";
                                        divOldQY.Visible = true;
                                    }
                                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认)
                                    {
                                        TextBoxOldQYCheckRemark.Text = "提交审核";
                                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                        divOldQY.Visible = true;
                                    }
                                }
                            }
                            //企业看不到各级申办人列
                            RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                        }

                        BindFile(_zjs_ApplyMDL.ApplyID);
                        BindCheckHistory(_zjs_ApplyMDL.ApplyID);
                        SetButtonEnable(_zjs_ApplyMDL);

                        #endregion edit
                    }
                    else//new
                    {
                        #region new

                        //#region 企业信息与造价工程师证书信息不一致
                        //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);

                        //if (PersonCount > 0)
                        //{
                        //    UIHelp.layerAlert(Page, "企业信息中的监管区县与造价工程师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                        //    return;
                        //}

                        //#endregion

                        //按PSN_RegisterNO排序获取个人证书，附件绑定在第一本证书上。
                        List<zjs_CertificateMDL> list = zjs_CertificateDAL.GetObjectByPSN_CertificateNO_NoCancel(WorkerCertificateCode);                       

                        //二造证书
                        zjs_CertificateMDL o = list[0];

                        ViewState["zjs_CertificateMDL"] = o;                      
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        LabelPSN_Name.Text = o.PSN_Name;
                        LabelPSN_Sex.Text = o.PSN_Sex;
                        LabelBirthday.Text = o.PSN_BirthDate.Value.ToString("yyyy-MM-dd");
                        LabelNation.Text = o.PSN_National;//民族
                        LabelPSN_CertificateType.Text = o.PSN_CertificateType;
                        LabelPSN_CertificateNO.Text = o.PSN_CertificateNO;
                        LabelPSN_RegisterNo.Text = o.PSN_RegisterNO;
                        LabelPSN_RegisteProfession.Text = o.PSN_RegisteProfession;
                        LabelValidDate.Text = o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");
                        LabelOldENT_Name.Text = o.ENT_Name;
                        LabelOldEND_Addess.Text = o.END_Addess;
                        if (list.Count > 1)
                        {
                            LabelPSN_RegisterNo2.Text = list[1].PSN_RegisterNO;
                            LabelPSN_RegisteProfession2.Text = list[1].PSN_RegisteProfession;
                            LabelValidDate2.Text = list[1].PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");
                            trZhuanYe2.Visible = true;
                            ViewState["zjs_CertificateMDL2"] = list[1];
                        }

                        //考生信息
                        WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                        LabelPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
                        LabelPSN_Email.Text = _WorkerOB.Email;   //邮箱

                        //企业信息
                        UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(o.ENT_OrganizationsCode);
                        if (_UnitMDL != null)
                        {
                            LabelOldENT_Name.Text = _UnitMDL.ENT_Name;
                            HiddenFieldOldEnt_QYZJJGDM.Value = o.ENT_OrganizationsCode;//统一社会信用代码
                            LabelOldEND_Addess.Text = _UnitMDL.END_Addess;
                            LabelOldENT_Type.Text = _UnitMDL.ENT_Type;
                            LabelOldFR.Text = _UnitMDL.ENT_Corporate;
                            RadTextBoxOldLinkMan.Text = _UnitMDL.ENT_Contact;
                            RadTextBoxOldENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                        }


                        if (IfExistRoleID("0") == true)//个人登录后
                        {
                            SetButtonEnable(null);
                            divGR.Visible = true;//个人操作按钮 
                            //附件信息，人员照片
                            BindFile("0");
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        int noPassCount = 0;
                        foreach (zjs_CertificateMDL z in list)
                        {
                            if (z.PSN_CertificateValidity.HasValue == true && z.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                            {
                                sb.Append(string.Format("<br/>证书编号“{0}”过期前30天内只能办理注销，不受理其他注册业务。", z.PSN_RegisterNO));
                                noPassCount += 1;
                            }
                        }
                        if (sb.Length > 0)
                        {
                            if (list.Count != noPassCount)
                            {
                                UIHelp.layerAlert(Page, string.Format("{0}无法多专业合并申请，请先注销即将过期的证书，再变更另一个专业证书到新单位，再对注销专业证书发起初始注册。", sb), 0, 0);
                            }
                            else
                            {
                                UIHelp.layerAlert(Page, string.Format("{0}请先注销证书，在发起初始注册。", sb), 0, 0);
                            }
                            divGR.Visible = false;//个人操作按钮 
                        }
                        #endregion new
                    }
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "加载造价工程师执业企业变更信息失败", ex);
                    return;
                }
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")//刷新已上传附件列表
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
            else if (Request["__EVENTTARGET"] == "selectUnit")//选择变更后的企业
            {
                #region 选择变更到企业

                string zzjgdmno = ZZJGDM_check.Text;
                string zzjgdmold = HiddenFieldOldEnt_QYZJJGDM.Value;
                if (string.IsNullOrEmpty(zzjgdmno) == true)
                {
                    UIHelp.layerAlert(Page, "选择单位信息有误，无法读取！", 5, 0);
                    return;
                }
                if (zzjgdmno.Trim() == zzjgdmold.Trim())
                {
                    UIHelp.layerAlert(Page, "选择单位与原单位相同，不允许发起申请", 5, 0);
                    return;
                }
                try
                {
                    UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdmno);
                    if (_UnitMDL != null)
                    {
                        LabelENT_Name.Text = _UnitMDL.ENT_Name;
                        HiddenFieldENT_OrganizationsCode.Value = zzjgdmno;
                        HiddenFieldENT_City.Value = (string.IsNullOrEmpty(_UnitMDL.ENT_City) ? "" : _UnitMDL.ENT_City);
                        LabelEND_Addess.Text = _UnitMDL.END_Addess;
                        RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                        RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                        LabelFR.Text = _UnitMDL.ENT_Corporate;
                        LabelENT_Type.Text = _UnitMDL.ENT_Type;
                        ButtonSave.Enabled = true;
                        ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "未查询到相关企业，请检查是否有误！", 5, 0);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    UIHelp.layerAlert(Page, "查询企业信息出错，请稍候重试", 5, 0);
                    return;
                }

                #endregion 选择变更到企业
            }
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);                
                Decide(_zjs_ApplyMDL);
            }        
        }

        //保存申报
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            zjs_CertificateMDL person = null;
            if (ViewState["zjs_ApplyMDL"] == null)
            {
                person = ViewState["zjs_CertificateMDL"] as zjs_CertificateMDL;
            }

            #region 申报规则校验

            bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(LabelPSN_CertificateNO.Text);
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
            }
            
            if (string.IsNullOrEmpty(HiddenFieldOldEnt_QYZJJGDM.Value))
            {
                UIHelp.layerAlert(Page, "无法获取原单位信息", 5, 0);
                return;
            }

            if (Utility.Check.CheckBirthdayLimit(-1, LabelPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满70周岁前90天起,不再允许发起二级造价工程师初始、延续、执业企业变更注册申请。", 5, 0);
                return;
            }
            if (string.IsNullOrEmpty(HiddenFieldENT_OrganizationsCode.Value))
            {
                UIHelp.layerAlert(Page, "请选择一个变更到单位", 5, 0);
                return;
            }

            if (string.IsNullOrEmpty(HiddenFieldENT_City.Value))//所属区县
            {
                UIHelp.layerAlert(Page, "无法读取目标企业隶属区县，请让企业登录本系统，在企业信息里面去补全企业所属区县后再申请。");
                return;
            }

            if (Utility.Check.CheckIfSubUnit(LabelENT_Name.Text) == true)
            {
                UIHelp.layerAlert(Page, UIHelp.Tip_SubUnitForbid, 5, 0);
                return;
            }

            //if(ViewState["zjs_ApplyMDL"] == null && jcsjk_RY_JZS_ZSSDDAL.IfLocking(person.PSN_RegisterNO)==true)
            //{
            //    UIHelp.layerAlert(Page, "无法申请，证书处于在施项目锁定中，请先办理在施项目解锁！", 5, 0);
            //    return;
            //}
           
            if (UnitDAL.CheckGongShang(HiddenFieldENT_OrganizationsCode.Value) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", LabelENT_Name.Text));
                return;
            }

            if (person != null)//new
            {
                if (person.PSN_CertificateValidity.HasValue == true && person.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                {
                    UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                    return;
                }

                //执业企业人员变更判断调入人员有没有在途业务（多专业，有一个专业在途也不允许办理变更单位）
                DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT * FROM [dbo].[View_ZJS_Applying]  where  PSN_CertificateNO='{0}' and  ApplyID is not null", LabelPSN_CertificateNO.Text));
                if (dt.Rows.Count > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("【{0}】正在为注册号为【{1}】的证书办理{2}，无法申请其他注册申请，请等待业务办理完成或自行联系该单位进行协商取消在办业务。"
                      , dt.Rows[0]["NewENT_Name"]
                      , dt.Rows[0]["PSN_RegisterNO"]
                      , (dt.Rows[0]["ApplyTypeSub"] != DBNull.Value ? dt.Rows[0]["ApplyTypeSub"] : dt.Rows[0]["ApplyType"])
                      ), 5, 0);
                    return;
                }
                if (zjs_ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", LabelPSN_RegisterNo.Text.Trim(), EnumManager.ZJSApplyStatus.未申报, EnumManager.ZJSApplyStatus.待确认)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }

                //                //查询证书历史表和证书表注册类型为：初始、执业企业变更的且大于一年之内（该规则作废，允许变更）
                //                DataTable dtChange = CommonDAL.GetDataTable(string.Format(@"
                //                select  top 1 * from (
                //                    select  PSN_CertificateNO,[ApplyType],ENT_Name,[CodeDate] as PSN_RegistePermissionDate
                //                    from zjs_Apply  
                //                    where PSN_CertificateNO='{0}' and  ApplyType='初始注册' and ApplyStatus='已公告'
                //                    union all 
                //                    select  PSN_CertificateNO,ApplyTypeSub as ApplyType ,ENT_Name,NoticeDate 
                //                    from zjs_Apply 
                //                    where PSN_CertificateNO='{0}' and NoticeDate is not null and  ApplyTypeSub='执业企业变更' and ApplyStatus='已公告' 
                //                ) c 
                //                where  dateadd(day,-1,dateadd(year,1,PSN_RegistePermissionDate))>'{1}'
                //                 order by PSN_RegistePermissionDate desc
                //                            ", LabelPSN_CertificateNO.Text, DateTime.Now.Date.ToString("yyyy-MM-dd")));
                //                if (dtChange.Rows.Count > 0)
                //                {
                //                    UIHelp.layerAlert(Page, "初始注册、执业企业变更未满一年内不允许做执业企业变更！", 5, 0);
                //                    return;
                //                }


                 
               
            }

            #endregion

            zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] == null ? new zjs_ApplyMDL() : (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            UIHelp.GetData(EditTable, _zjs_ApplyMDL);

            zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = ViewState["zjs_ApplyChangeMDL"] == null ? new zjs_ApplyChangeMDL() : (zjs_ApplyChangeMDL)ViewState["zjs_ApplyChangeMDL"];//详细表
            UIHelp.GetData(EditTable, _zjs_ApplyChangeMDL);

            string ApplyID2 = "";//第二专业申请单ID

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                //现企业信息,企业类型,现企业名称
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
                              _zjs_ApplyMDL.ENT_Name = LabelENT_Name.Text;
                _zjs_ApplyMDL.ENT_OrganizationsCode = HiddenFieldENT_OrganizationsCode.Value;
                _zjs_ApplyMDL.ENT_City = HiddenFieldENT_City.Value;
               
                _zjs_ApplyChangeMDL.ENT_Name = LabelENT_Name.Text;
                _zjs_ApplyChangeMDL.END_Addess = LabelEND_Addess.Text;
                _zjs_ApplyChangeMDL.LinkMan = RadTextBoxLinkMan.Text;
                _zjs_ApplyChangeMDL.ENT_Telephone = RadTextBoxENT_Telephone.Text;
                _zjs_ApplyChangeMDL.FR = LabelFR.Text;
                _zjs_ApplyChangeMDL.ENT_Type = LabelENT_Type.Text;

                if (ViewState["zjs_ApplyMDL"] == null)//new
                {
                    _zjs_ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _zjs_ApplyMDL.CJR = UserName;
                    _zjs_ApplyMDL.CJSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL.Valid = 1;
                    _zjs_ApplyMDL.ApplyType = EnumManager.ZJSApplyType.变更注册;
                    _zjs_ApplyMDL.ApplyTypeSub = EnumManager.ZJSApplyType.执业企业变更;//变更类别
                    _zjs_ApplyMDL.PSN_Name = LabelPSN_Name.Text;
                    _zjs_ApplyMDL.PSN_CertificateType = LabelPSN_CertificateType.Text;
                    _zjs_ApplyMDL.PSN_CertificateNO = LabelPSN_CertificateNO.Text;
                    _zjs_ApplyMDL.PSN_RegisteProfession = LabelPSN_RegisteProfession.Text;
                    _zjs_ApplyMDL.PSN_RegisterNo = LabelPSN_RegisterNo.Text;
                    _zjs_ApplyMDL.OldUnitName = LabelOldENT_Name.Text;
                    _zjs_ApplyMDL.OldEnt_QYZJJGDM = HiddenFieldOldEnt_QYZJJGDM.Value; //变更前企业编码
                    _zjs_ApplyMDL.ApplyCode = zjs_ApplyDAL.GetNextApplyCode(tran, EnumManager.ZJSApplyType.执业企业变更);
                    LabelApplyCode.Text = _zjs_ApplyMDL.ApplyCode;
                    _zjs_ApplyMDL.ApplyTime = _zjs_ApplyMDL.XGSJ;

                    _zjs_ApplyChangeMDL.ApplyID = _zjs_ApplyMDL.ApplyID;
                    _zjs_ApplyChangeMDL.ValidDate = person.PSN_CertificateValidity;
                    if (string.IsNullOrEmpty(_zjs_ApplyChangeMDL.PSN_MobilePhone))
                    {
                        _zjs_ApplyChangeMDL.PSN_MobilePhone = person.PSN_MobilePhone;
                    }
                    if (string.IsNullOrEmpty(_zjs_ApplyChangeMDL.PSN_Email))
                    {
                        _zjs_ApplyChangeMDL.PSN_Email = person.PSN_Email;
                    }

                    _zjs_ApplyChangeMDL.ChangeReason = EnumManager.ZJSApplyType.执业企业变更;//变更原因

                    //原企业信息
                    _zjs_ApplyChangeMDL.OldENT_Name = LabelOldENT_Name.Text;
                    _zjs_ApplyChangeMDL.OldENT_Type = LabelOldENT_Type.Text;
                    _zjs_ApplyChangeMDL.OldLinkMan = RadTextBoxOldLinkMan.Text;
                    _zjs_ApplyChangeMDL.OldENT_Telephone = RadTextBoxOldENT_Telephone.Text;

                    zjs_ApplyDAL.Insert(tran, _zjs_ApplyMDL);
                    zjs_ApplyChangeDAL.Insert(tran, _zjs_ApplyChangeMDL);
                    if (ViewState["zjs_CertificateMDL2"] != null)
                    {
                        zjs_CertificateMDL zjs_CertificateMDL2 = (zjs_CertificateMDL)ViewState["zjs_CertificateMDL2"];
                        ApplyID2 = Guid.NewGuid().ToString();
                        zjs_ApplyDAL.InsertZhuanYe2ApplyWithZhuanYe1ApplyID(tran, _zjs_ApplyMDL.ApplyID, ApplyID2, zjs_CertificateMDL2.PSN_RegisterNO, zjs_CertificateMDL2.PSN_RegisteProfession);
                        zjs_ApplyChangeDAL.InsertZhuanYe2ApplyChangeWithZhuanYe1ApplyChangeID(tran, _zjs_ApplyMDL.ApplyID, ApplyID2, zjs_CertificateMDL2.PSN_RegisterNO, zjs_CertificateMDL2.PSN_CertificateValidity.Value);
                    }

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _zjs_ApplyMDL.PSN_RegisterNo, _zjs_ApplyMDL.ApplyID, filetype);

                }
                else//update
                {
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                    zjs_ApplyChangeDAL.Update(tran, _zjs_ApplyChangeMDL);

                    if (ViewState["zjs_ApplyMDL2"] != null)
                    {
                        zjs_ApplyMDL zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                        ApplyID2 = zjs_ApplyMDL2.ApplyID;
                        zjs_ApplyDAL.UpdateZhuanYe2ApplyWithZhuanYe1ApplyID(tran, _zjs_ApplyMDL.ApplyID, zjs_ApplyMDL2.ApplyID);
                        zjs_ApplyChangeDAL.UpdateZhuanYe2ApplyChangeWithZhuanYe1ApplyID(tran, _zjs_ApplyMDL.ApplyID, zjs_ApplyMDL2.ApplyID);
                    }
                }
                tran.Commit();
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                ViewState["zjs_ApplyChangeMDL"] = _zjs_ApplyChangeMDL;

                if (string.IsNullOrEmpty(ApplyID2) == false)
                {
                    zjs_ApplyMDL zjs_ApplyMDL2 = zjs_ApplyDAL.GetObject(ApplyID2);
                    ViewState["zjs_ApplyMDL2"] = zjs_ApplyMDL2;

                    zjs_ApplyChangeMDL zjs_ApplyChangeMDL2 = zjs_ApplyChangeDAL.GetObject(ApplyID2);
                    ViewState["zjs_ApplyChangeMDL2"] = zjs_ApplyChangeMDL2;
                }

                BindFile(ApplyID);

                //操着按钮控制
                SetButtonEnable(_zjs_ApplyMDL);

                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请保存成功", string.Format("姓名：{0}，身份证号：{1}，保存时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "保存成功，请上传必要的附件，打印申请单并加盖原单位及新单位公章后扫描上传。（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");

                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师执业企业变更申请失败", ex);
            }
        }

        //提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = (zjs_ApplyChangeMDL)ViewState["zjs_ApplyChangeMDL"];

            if (ButtonUnit.Text != "取消申报")
            {
                if (_zjs_ApplyChangeMDL.ValidDate.HasValue == true && _zjs_ApplyChangeMDL.ValidDate.Value.AddDays(-30) < DateTime.Now)
                {
                    UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                    return;
                }
                //必须上传附件检查
                #region
                System.Collections.Hashtable fj = new System.Collections.Hashtable();

                //必须上传附件集合
                fj = new System.Collections.Hashtable{
                    {EnumManager.FileDataTypeName.一寸免冠照片,0},                                
                    //{EnumManager.FileDataTypeName.解除劳动合同证明,0},
                    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    {EnumManager.FileDataTypeName.申请表扫描件,0}};

                ////原单位允许调出后才可申报
                //if (string.IsNullOrEmpty(_zjs_ApplyMDL.OldUnitCheckResult) || _zjs_ApplyMDL.OldUnitCheckResult == "不通过")
                //{
                //    UIHelp.layerAlert(Page, "原单位尚未同意调出,请联系原单位！", 5, 0);
                //    return;
                //}

                //已上传附件集合
                DataTable dt = ApplyDAL.GetApplyFile(_zjs_ApplyMDL.ApplyID);

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

                //检查社保相关扫描件上传文件是否存在
                bool SheBaoCheckFile = ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.社保扫描件);
                if (SheBaoCheckFile == false //没上传社保证明
               && (_zjs_ApplyMDL.SheBaoCheck.HasValue == false || _zjs_ApplyMDL.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
               )
                {
                    sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                }

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！（社保验证结果在填写次日返回，当日填报并申请的需要自行上传社保证明）", sb), 5, 0);
                    return;
                }

                //检查一寸照片数量
                int faceImgCount = ApplyFileDAL.SelectFaceImgCountByApplyID(_zjs_ApplyMDL.ApplyID);
                if (faceImgCount > 1)
                {
                    UIHelp.layerAlert(Page, string.Format("只能上传一张一寸免冠照片，请删除多余照片！", sb), 5, 0);
                    return;
                }
                #endregion
            }

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                #region 申请1
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonUnit.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
                    _zjs_ApplyMDL.ApplyTime = null;
                    _zjs_ApplyMDL.OldUnitCheckResult = null;
                    _zjs_ApplyMDL.OldUnitCheckRemark = null;
                    _zjs_ApplyMDL.OldUnitCheckTime = null;
                    _zjs_ApplyMDL.newUnitCheckTime = null;
                    _zjs_ApplyMDL.newUnitCheckResult = null;
                    _zjs_ApplyMDL.newUnitCheckRemark = null;

                }
                else
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.待确认;
                    _zjs_ApplyMDL.ApplyTime = DateTime.Now;
                    _zjs_ApplyMDL.OldUnitCheckResult = null;
                    _zjs_ApplyMDL.OldUnitCheckRemark = null;
                    _zjs_ApplyMDL.OldUnitCheckTime = null;
                    _zjs_ApplyMDL.newUnitCheckTime = null;
                    _zjs_ApplyMDL.newUnitCheckResult = null;
                    _zjs_ApplyMDL.newUnitCheckRemark = null;
                    _zjs_ApplyMDL.GetResult = null;
                    _zjs_ApplyMDL.GetRemark = null;
                    _zjs_ApplyMDL.GetMan = null;
                    _zjs_ApplyMDL.GetDateTime = null;
                    _zjs_ApplyMDL.ExamineDatetime = null;
                    _zjs_ApplyMDL.ExamineResult = null;
                    _zjs_ApplyMDL.ExamineRemark = null;
                    _zjs_ApplyMDL.ExamineMan = null;
                    _zjs_ApplyMDL.CheckDate = null;
                    _zjs_ApplyMDL.CheckResult = null;
                    _zjs_ApplyMDL.CheckRemark = null;
                    _zjs_ApplyMDL.CheckMan = null;
                    _zjs_ApplyMDL.ConfirmDate = null;
                    _zjs_ApplyMDL.ConfirmResult = null;
                    _zjs_ApplyMDL.ConfirmMan = null;
                }
                #endregion
                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    #region 申请2
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];//申请表
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    if (ButtonUnit.Text == "取消申报")
                    {
                        _zjs_ApplyMDL2.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
                        _zjs_ApplyMDL2.ApplyTime = null;
                        _zjs_ApplyMDL2.OldUnitCheckResult = null;
                        _zjs_ApplyMDL2.OldUnitCheckRemark = null;
                        _zjs_ApplyMDL2.OldUnitCheckTime = null;
                        _zjs_ApplyMDL2.newUnitCheckTime = null;
                        _zjs_ApplyMDL2.newUnitCheckResult = null;
                        _zjs_ApplyMDL2.newUnitCheckRemark = null;

                    }
                    else
                    {
                        _zjs_ApplyMDL2.ApplyStatus = EnumManager.ZJSApplyStatus.待确认;
                        _zjs_ApplyMDL2.ApplyTime = _zjs_ApplyMDL.ApplyTime;
                        _zjs_ApplyMDL2.OldUnitCheckResult = null;
                        _zjs_ApplyMDL2.OldUnitCheckRemark = null;
                        _zjs_ApplyMDL2.OldUnitCheckTime = null;
                        _zjs_ApplyMDL2.newUnitCheckTime = null;
                        _zjs_ApplyMDL2.newUnitCheckResult = null;
                        _zjs_ApplyMDL2.newUnitCheckRemark = null;
                        _zjs_ApplyMDL2.GetResult = null;
                        _zjs_ApplyMDL2.GetRemark = null;
                        _zjs_ApplyMDL2.GetMan = null;
                        _zjs_ApplyMDL2.GetDateTime = null;
                        _zjs_ApplyMDL2.ExamineDatetime = null;
                        _zjs_ApplyMDL2.ExamineResult = null;
                        _zjs_ApplyMDL2.ExamineRemark = null;
                        _zjs_ApplyMDL2.ExamineMan = null;
                        _zjs_ApplyMDL2.CheckDate = null;
                        _zjs_ApplyMDL2.CheckResult = null;
                        _zjs_ApplyMDL2.CheckRemark = null;
                        _zjs_ApplyMDL2.CheckMan = null;
                        _zjs_ApplyMDL2.ConfirmDate = null;
                        _zjs_ApplyMDL2.ConfirmResult = null;
                        _zjs_ApplyMDL2.ConfirmMan = null;
                    }
                    #endregion
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                    ViewState["zjs_ApplyMDL2"] = _zjs_ApplyMDL2;
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师执业企业变更申请失败", ex);
            }

            SetButtonEnable(_zjs_ApplyMDL);

            //人员照片
            BindFile(_zjs_ApplyMDL.ApplyID);

            if (_zjs_ApplyMDL.ApplyStatus == "待确认")
            {
                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='zjsApplyHistory.aspx?c={2}&i=执业企业变更';", Utility.Cryptography.Encrypt("applyZJS"), Utility.Cryptography.Encrypt(_zjs_ApplyMDL.ApplyID.ToString()), _zjs_ApplyMDL.ApplyID));
            }
            else
            {
                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "撤销成功！", 6, 2000);
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
        }

        //企业确认，原单位申报 or 撤销申报 
        protected void OldQYButtonApply_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                #region 申请1
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (OldQYButtonApply.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = RadioButtonListOldQYCheckResult.SelectedValue; ;//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = TextBoxOldQYCheckRemark.Text.Trim();//现单位审核意见

                    if (RadioButtonListOldQYCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldQYCheckRemark.Visible = true;
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                        _zjs_ApplyMDL.LastBackResult = string.Format("{0}企业驳回申请，驳回说明：{1}", _zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxOldQYCheckRemark.Text.Trim());//现单位审核意见;
                    }
                    else
                    {
                        if (_zjs_ApplyMDL.newUnitCheckTime > Convert.ToDateTime("2019-01-01"))//新单位已审批，直接申报，否则只记录本单位意见
                        {
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                        }
                    

                        _zjs_ApplyMDL.GetResult = null;
                        _zjs_ApplyMDL.GetRemark = null;
                        _zjs_ApplyMDL.GetMan = null;
                        _zjs_ApplyMDL.GetDateTime = null;
                        _zjs_ApplyMDL.ExamineDatetime = null;
                        _zjs_ApplyMDL.ExamineResult = null;
                        _zjs_ApplyMDL.ExamineRemark = null;
                        _zjs_ApplyMDL.ExamineMan = null;
                        _zjs_ApplyMDL.CheckDate = null;
                        _zjs_ApplyMDL.CheckResult = null;
                        _zjs_ApplyMDL.CheckRemark = null;
                        _zjs_ApplyMDL.CheckMan = null;
                        _zjs_ApplyMDL.ConfirmDate = null;
                        _zjs_ApplyMDL.ConfirmResult = null;
                        _zjs_ApplyMDL.ConfirmMan = null;
                    }
                }
                #endregion 申请1

                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    #region 申请2
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];//申请表
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;
                    _zjs_ApplyMDL2.OldUnitCheckTime = _zjs_ApplyMDL.OldUnitCheckTime;
                    _zjs_ApplyMDL2.OldUnitCheckResult = _zjs_ApplyMDL.OldUnitCheckResult;
                    _zjs_ApplyMDL2.OldUnitCheckRemark = _zjs_ApplyMDL.OldUnitCheckRemark;
                    _zjs_ApplyMDL2.LastBackResult = _zjs_ApplyMDL.LastBackResult;
                    _zjs_ApplyMDL2.ApplyTime = _zjs_ApplyMDL.ApplyTime;
                    _zjs_ApplyMDL2.GetResult = _zjs_ApplyMDL.GetResult;
                    _zjs_ApplyMDL2.GetRemark = _zjs_ApplyMDL.GetRemark;
                    _zjs_ApplyMDL2.GetMan = _zjs_ApplyMDL.GetMan;
                    _zjs_ApplyMDL2.GetDateTime = _zjs_ApplyMDL.GetDateTime;
                    _zjs_ApplyMDL2.ExamineDatetime = _zjs_ApplyMDL.ExamineDatetime;
                    _zjs_ApplyMDL2.ExamineResult = _zjs_ApplyMDL.ExamineResult;
                    _zjs_ApplyMDL2.ExamineRemark = _zjs_ApplyMDL.ExamineRemark;
                    _zjs_ApplyMDL2.ExamineMan = _zjs_ApplyMDL.ExamineMan;
                    _zjs_ApplyMDL2.CheckDate = _zjs_ApplyMDL.CheckDate;
                    _zjs_ApplyMDL2.CheckResult = _zjs_ApplyMDL.CheckResult;
                    _zjs_ApplyMDL2.CheckRemark = _zjs_ApplyMDL.CheckRemark;
                    _zjs_ApplyMDL2.CheckMan = _zjs_ApplyMDL.CheckMan;
                    _zjs_ApplyMDL2.ConfirmDate = _zjs_ApplyMDL.ConfirmDate;
                    _zjs_ApplyMDL2.ConfirmResult = _zjs_ApplyMDL.ConfirmResult;
                    _zjs_ApplyMDL2.ConfirmMan = _zjs_ApplyMDL.ConfirmMan;
                    #endregion

                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                    ViewState["zjs_ApplyMDL2"] = _zjs_ApplyMDL2;
                }
                tran.Commit();

                SetButtonEnable(_zjs_ApplyMDL);
                //人员照片
                BindFile(_zjs_ApplyMDL.ApplyID);

                if (_zjs_ApplyMDL.ApplyStatus == "已申报" || _zjs_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                    ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "造价工程师执业企业变更申请失败！", ex);
            }
        }

        //变更单位（现单位）申报 or 撤销申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                #region 申请1
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonApply.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                    _zjs_ApplyMDL.newUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.newUnitCheckResult = "不同意";//现单位审核结果
                    _zjs_ApplyMDL.newUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    _zjs_ApplyMDL.newUnitCheckTime = DateTime.Now;//变更单位申请时间
                    _zjs_ApplyMDL.newUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//变更单位审核结果
                    _zjs_ApplyMDL.newUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//变更单位审核意见

                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldUnitCheckRemark.Visible = true;
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                        _zjs_ApplyMDL.LastBackResult = string.Format("{0}企业驳回申请，驳回说明：{1}", _zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxOldUnitCheckRemark.Text.Trim());//现单位审核意见;
                    }
                    else
                    {
                        if (_zjs_ApplyMDL.OldUnitCheckTime > Convert.ToDateTime("2019-01-01"))//新单位已审批，直接申报，否则只记录本单位意见
                        {
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                        }

                        _zjs_ApplyMDL.GetResult = null;
                        _zjs_ApplyMDL.GetRemark = null;
                        _zjs_ApplyMDL.GetMan = null;
                        _zjs_ApplyMDL.GetDateTime = null;
                        _zjs_ApplyMDL.ExamineDatetime = null;
                        _zjs_ApplyMDL.ExamineResult = null;
                        _zjs_ApplyMDL.ExamineRemark = null;
                        _zjs_ApplyMDL.ExamineMan = null;
                        _zjs_ApplyMDL.CheckDate = null;
                        _zjs_ApplyMDL.CheckResult = null;
                        _zjs_ApplyMDL.CheckRemark = null;
                        _zjs_ApplyMDL.CheckMan = null;
                        _zjs_ApplyMDL.ConfirmDate = null;
                        _zjs_ApplyMDL.ConfirmResult = null;
                        _zjs_ApplyMDL.ConfirmMan = null;
                    }
                }
                #endregion 申请1
                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    #region 申请2
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];//申请表
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;
                    _zjs_ApplyMDL2.OldUnitCheckTime = _zjs_ApplyMDL.OldUnitCheckTime;
                    _zjs_ApplyMDL2.OldUnitCheckResult = _zjs_ApplyMDL.OldUnitCheckResult;
                    _zjs_ApplyMDL2.OldUnitCheckRemark = _zjs_ApplyMDL.OldUnitCheckRemark;
                    _zjs_ApplyMDL2.newUnitCheckTime = _zjs_ApplyMDL.newUnitCheckTime;
                    _zjs_ApplyMDL2.newUnitCheckResult = _zjs_ApplyMDL.newUnitCheckResult;
                    _zjs_ApplyMDL2.newUnitCheckRemark = _zjs_ApplyMDL.newUnitCheckRemark;
                    _zjs_ApplyMDL2.LastBackResult = _zjs_ApplyMDL.LastBackResult;
                    _zjs_ApplyMDL2.ApplyTime = _zjs_ApplyMDL.ApplyTime;
                    _zjs_ApplyMDL2.GetResult = _zjs_ApplyMDL.GetResult;
                    _zjs_ApplyMDL2.GetRemark = _zjs_ApplyMDL.GetRemark;
                    _zjs_ApplyMDL2.GetMan = _zjs_ApplyMDL.GetMan;
                    _zjs_ApplyMDL2.GetDateTime = _zjs_ApplyMDL.GetDateTime;
                    _zjs_ApplyMDL2.ExamineDatetime = _zjs_ApplyMDL.ExamineDatetime;
                    _zjs_ApplyMDL2.ExamineResult = _zjs_ApplyMDL.ExamineResult;
                    _zjs_ApplyMDL2.ExamineRemark = _zjs_ApplyMDL.ExamineRemark;
                    _zjs_ApplyMDL2.ExamineMan = _zjs_ApplyMDL.ExamineMan;
                    _zjs_ApplyMDL2.CheckDate = _zjs_ApplyMDL.CheckDate;
                    _zjs_ApplyMDL2.CheckResult = _zjs_ApplyMDL.CheckResult;
                    _zjs_ApplyMDL2.CheckRemark = _zjs_ApplyMDL.CheckRemark;
                    _zjs_ApplyMDL2.CheckMan = _zjs_ApplyMDL.CheckMan;
                    _zjs_ApplyMDL2.ConfirmDate = _zjs_ApplyMDL.ConfirmDate;
                    _zjs_ApplyMDL2.ConfirmResult = _zjs_ApplyMDL.ConfirmResult;
                    _zjs_ApplyMDL2.ConfirmMan = _zjs_ApplyMDL.ConfirmMan;
                    #endregion

                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                    ViewState["zjs_ApplyMDL2"] = _zjs_ApplyMDL2;
                }
                tran.Commit();

                SetButtonEnable(_zjs_ApplyMDL);
                //人员照片
                BindFile(_zjs_ApplyMDL.ApplyID);

                if (_zjs_ApplyMDL.ApplyStatus == "已申报" || _zjs_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "执业企业变更申请申报成功", string.Format("姓名：{0}，身份证号：{1}，申报时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师执业企业变更申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，撤销时间：{2}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                    ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "造价工程师执业企业变更申请失败！", ex);
            }
        }

        //市级受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);

            #region 查询证书是否锁定
            if (RadioButtonListApplyStatus.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(_zjs_ApplyMDL.PSN_CertificateNO);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion

            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;
            _zjs_ApplyMDL.GetDateTime = DateTime.Now;
            _zjs_ApplyMDL.GetMan = UserName;
            _zjs_ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _zjs_ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ZJSApplyStatus.已受理 : EnumManager.ZJSApplyStatus.已驳回;
            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _zjs_ApplyMDL.LastBackResult = string.Format("{0}受理环节驳回申请，驳回说明：{1}", _zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }
          
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                zjs_ApplyDAL.Update(tran,_zjs_ApplyMDL);

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.GetDateTime = _zjs_ApplyMDL.GetDateTime;
                    _zjs_ApplyMDL2.GetMan = _zjs_ApplyMDL.GetMan;
                    _zjs_ApplyMDL2.GetResult = _zjs_ApplyMDL.GetResult;
                    _zjs_ApplyMDL2.GetRemark = _zjs_ApplyMDL.GetRemark;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;
                    _zjs_ApplyMDL2.LastBackResult = _zjs_ApplyMDL.LastBackResult;

                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "受理造价工程师执业企业变更失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "受理造价工程师执业企业变更", string.Format("姓名：{0}，身份证号：{1}，受理时间：{2}。审批结果：{3}，审批意见：{4}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date
                 , _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark));
            UIHelp.ParentAlert(Page, "受理成功！", true);

            //string js = string.Format("<script>window.parent.location.href='zjsBusinessQuery.aspx?id={0}&&type={1}';</script>", _zjs_ApplyMDL.ApplyID, _zjs_ApplyMDL.ApplyType);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
        }

        //市级审核
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //申请表
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);

            #region 查询证书是否锁定
            if (RadioButtonListExamineResult.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(_zjs_ApplyMDL.PSN_CertificateNO);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion

            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;
            _zjs_ApplyMDL.ExamineDatetime = DateTime.Now;
            _zjs_ApplyMDL.ExamineMan = UserName;
            _zjs_ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
            _zjs_ApplyMDL.ExamineRemark = TextBox_ExamineRemark.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus =  EnumManager.ZJSApplyStatus.已审核;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //更新申请表
                zjs_ApplyDAL.Update(tran,_zjs_ApplyMDL);

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.ExamineDatetime = _zjs_ApplyMDL.ExamineDatetime;
                    _zjs_ApplyMDL2.ExamineMan = _zjs_ApplyMDL.ExamineMan;
                    _zjs_ApplyMDL2.ExamineResult = _zjs_ApplyMDL.ExamineResult;
                    _zjs_ApplyMDL2.ExamineRemark = _zjs_ApplyMDL.ExamineRemark;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;

                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "审核造价工程师执业企业变更失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "审核造价工程师执业企业变更", string.Format("姓名：{0}，身份证号：{1}，审查时间：{2}。审核结果：{3}，审核意见：{4}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, DateTime.Now.Date
                 , _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //市级决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);

            #region 查询证书是否锁定
            if (RadioButtonListDecide.SelectedValue == "通过")
            {
                bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(_zjs_ApplyMDL.PSN_CertificateNO);
                if (IfLock == true)
                {
                    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    return;
                }
            }
            #endregion

            if (_zjs_ApplyMDL.ExamineResult != RadioButtonListDecide.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm",
                   string.Format(@"layer.confirm('【决定结果】与【审核结果】不一致，是否真的要继续操作？', {{btn: ['继续审核', '重新审核'],icon:3, title: '警告'}}, function () {{ __doPostBack('Decide', '');}});")
                    , true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonDecide.ClientID), true);
                return;
            }
            Decide(_zjs_ApplyMDL);
        }

        /// <summary>
        /// 申请单决定
        /// </summary>
        /// <param name="_zjs_ApplyMDL">申请单对象</param>
        protected void Decide(zjs_ApplyMDL _zjs_ApplyMDL)
        {
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;

            //#region 查询证书是否锁定
            //if (CheckCertLock(_zjs_ApplyMDL.PSN_RegisterNo) == true) return;           
            //#endregion

            _zjs_ApplyMDL.ConfirmDate = DateTime.Now;
            _zjs_ApplyMDL.ConfirmMan = UserName;
            _zjs_ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已决定;
            _zjs_ApplyMDL.NoticeDate = DateTime.Now;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            zjs_ApplyMDL _zjs_ApplyMDL2 = null;
            try
            {
                //更新专业1申请单
                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);

                if (ViewState["zjs_ApplyMDL2"] != null)//专业2申请表
                {
                    #region 专业2申请表
                    _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    _zjs_ApplyMDL2.ConfirmDate = _zjs_ApplyMDL.ConfirmDate;
                    _zjs_ApplyMDL2.ConfirmMan = _zjs_ApplyMDL.ConfirmMan;
                    _zjs_ApplyMDL2.ConfirmResult = _zjs_ApplyMDL.ConfirmResult;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;
                    _zjs_ApplyMDL2.NoticeDate = _zjs_ApplyMDL.NoticeDate;
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);

                    //拷贝专业1附件到专业2
                    DataTable dtFile = CommonDAL.GetDataTable(string.Format(@"select f.*,newid() as newFileID
                                                                                 FROM [dbo].[ApplyFile] a
                                                                                 inner join [dbo].[FileInfo] f on a.[ApplyID]='{0}' and a.[FileID] = f.[FileID] ", ApplyID));

                    System.Text.StringBuilder sbFileInfo = new System.Text.StringBuilder();
                    System.Text.StringBuilder sbApplyFile = new System.Text.StringBuilder();
                    foreach (DataRow r in dtFile.Rows)
                    {
                        sbFileInfo.Append(string.Format(" union SELECT '{0}',[FileName],[FileSize],[FileUrl],[DataType],[FileType],[AddTime],[UploadMan],[OrderNo],[UpSJZXTime] FROM [dbo].[FileInfo] where [FileID]='{1}'", r["newFileID"], r["FileID"]));
                        sbApplyFile.Append(string.Format(" union SELECT '{0}','{1}',0 ", r["newFileID"], _zjs_ApplyMDL2.ApplyID));
                    }

                    if (sbFileInfo.Length > 0)
                    {
                        sbFileInfo.Remove(0, 6);
                        sbFileInfo.Insert(0, "INSERT INTO [dbo].[FileInfo]([FileID],[FileName],[FileSize],[FileUrl],[DataType],[FileType],[AddTime],[UploadMan],[OrderNo],[UpSJZXTime])");

                        CommonDAL.ExecSQL(tran, sbFileInfo.ToString());

                        sbApplyFile.Remove(0, 6);
                        sbApplyFile.Insert(0, "INSERT INTO [dbo].[ApplyFile]([FileID],[ApplyID],[CheckResult])");
                        CommonDAL.ExecSQL(tran, sbApplyFile.ToString());
                    }
                    #endregion 专业2申请表
                }

                if (RadioButtonListDecide.SelectedValue == "通过")
                {
                    string sql = "";

                    #region 专业1办结更新证书表

                    //证书信息写入历史表
                    sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_RegisterNO ='{0}' ";

                    CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.PSN_RegisterNo));

                    //更新证书信息
                    sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[ENT_Name] = [zjs_Apply].ENT_Name
                                            ,[zjs_Certificate].[ENT_OrganizationsCode] = [zjs_Apply].ENT_OrganizationsCode
                                            ,[zjs_Certificate].[ENT_City] = [zjs_Apply].ENT_City 
                                            ,[zjs_Certificate].[END_Addess] = [zjs_ApplyChange].[END_Addess]                                         
                                            ,[zjs_Certificate].[PSN_RegisteType]='{2}'
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate]='{3}'
                                            ,[zjs_Certificate].[XGR] = '{4}' 
                                            ,[zjs_Certificate].[XGSJ] = '{3}' 
                                        FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply]
                                        on [zjs_Certificate].[PSN_RegisterNO] = [zjs_Apply].[PSN_RegisterNO]
                                        inner join [dbo].[zjs_ApplyChange] 
                                        on [zjs_Apply].ApplyID=[zjs_ApplyChange].ApplyID
                                        where [zjs_Apply].ApplyID='{0}' ";

                    CommonDAL.ExecSQL(tran, string.Format(sql
                        , _zjs_ApplyMDL.ApplyID
                        , EnumManager.ZJSApplyType.执业企业变更
                        , EnumManager.ZJSApplyTypeCode.变更注册//"02"
                        , DateTime.Now
                        , UserName));

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
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID='{0}' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID='{0}' 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", _zjs_ApplyMDL.ApplyID));

                    //删除要覆盖的附件
                    CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID='{0}' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID='{0}' 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", _zjs_ApplyMDL.ApplyID));

                    //将申请单附件写入证书附件库
                    CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[zjs_Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[zjs_Apply] 
                                    on [ApplyFile].ApplyID = [zjs_Apply].ApplyID 
                                    where [zjs_Apply].ApplyID='{0}' ", _zjs_ApplyMDL.ApplyID));

                    //同步更新从业人员B、C本(由企业资质)

                    #endregion 办结更新证书表

                    //专业2证书更新
                    if (ViewState["zjs_ApplyMDL2"] != null)
                    {
                        #region 专业2办结更新证书表
                        //证书信息写入历史表
                        sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_RegisterNO ='{0}' ";

                        CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL2.PSN_RegisterNo));

                        //更新证书信息
                        sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[ENT_Name] = [zjs_Apply].ENT_Name
                                            ,[zjs_Certificate].[ENT_OrganizationsCode] = [zjs_Apply].ENT_OrganizationsCode
                                            ,[zjs_Certificate].[ENT_City] = [zjs_Apply].ENT_City 
                                            ,[zjs_Certificate].[END_Addess] = [zjs_ApplyChange].[END_Addess]                                         
                                            ,[zjs_Certificate].[PSN_RegisteType]='{2}'
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate]='{3}'
                                            ,[zjs_Certificate].[XGR] = '{4}' 
                                            ,[zjs_Certificate].[XGSJ] = '{3}' 
                                        FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply]
                                        on [zjs_Certificate].[PSN_RegisterNO] = [zjs_Apply].[PSN_RegisterNO]
                                        inner join [dbo].[zjs_ApplyChange] 
                                        on [zjs_Apply].ApplyID=[zjs_ApplyChange].ApplyID
                                        where [zjs_Apply].ApplyID='{0}' ";

                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , _zjs_ApplyMDL2.ApplyID
                            , EnumManager.ZJSApplyType.执业企业变更
                            , EnumManager.ZJSApplyTypeCode.变更注册//"02"
                            , DateTime.Now
                            , UserName));

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
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID='{0}' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID='{0}' 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", _zjs_ApplyMDL2.ApplyID));

                        //删除要覆盖的附件
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID='{0}' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID='{0}' 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", _zjs_ApplyMDL2.ApplyID));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[zjs_Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[zjs_Apply] 
                                    on [ApplyFile].ApplyID = [zjs_Apply].ApplyID 
                                    where [zjs_Apply].ApplyID='{0}' ", _zjs_ApplyMDL2.ApplyID));
                        #endregion
                    }
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "决定造价工程师执业企业变更申请失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "决定造价工程师执业企业变更申请", string.Format("姓名：{0}，身份证号：{1}，决定意见：{2}", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _zjs_ApplyMDL.ConfirmResult));
            UIHelp.ParentAlert(Page, "决定成功！", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace("green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {
                case EnumManager.ZJSApplyStatus.未申报:
                case EnumManager.ZJSApplyStatus.已驳回:
                    step_未申报.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.待确认:
                    step_待确认.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    step_已申报.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已受理:
                    step_已受理.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已审核:
                    step_已审核.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    step_已决定.Attributes["class"] += " green";
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(zjs_ApplyMDL o)
        {
            SetStep(o == null ? "" : o.ApplyStatus);

            switch (o == null ? "" : o.ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trSelectUnit.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        trSelectUnit.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.待确认:
                    ButtonSave.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    //现企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确定";
                    //原企业 
                    OldQYButtonApply.Enabled = true;
                    OldQYButtonApply.Text = "确定";
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonOutput.Enabled = false;

                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";
                    //原企业
                    OldQYButtonApply.Enabled = true;
                    OldQYButtonApply.Text = "取消申报";

                    //受理权限
                    if (IfExistRoleID("20") == true)
                    {
                        divQX.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    ButtonSave.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";

                    //企业
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确定";
                    //原企业 
                    OldQYButtonApply.Enabled = false;
                    OldQYButtonApply.Text = "确定";

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        trSelectUnit.Visible = true;
                    }

                    if (o.GetDateTime.HasValue == true && o.ExamineDatetime.HasValue == false)//受理驳回
                    {
                        //注册室管理员
                        if (IfExistRoleID("1")==true ||IfExistRoleID("15") == true)
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }                  
                    break;
                case EnumManager.ZJSApplyStatus.已受理:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                        divSendBack.Visible = true;//后退
                    }

                    //市级审核
                    if (IfExistRoleID("21") == true)
                    {
                        divQXCK.Visible = true;
                        BttSave.Text = "确认提交";
                    }
                    break;

                case EnumManager.ZJSApplyStatus.已审核:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                        divSendBack.Visible = true;//后退
                    }

                    //市级决定权限
                    if (IfExistRoleID("23") == true)
                    {
                        ButtonDecide.Text = "确认提交";
                        divDecide.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        if (o.ConfirmResult == "不通过")//只有决定不通过才能后退，如果通过了（修改了证书），需要人工后退。
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
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
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(string ApplyID)
        {
            if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)
            {
                DataTable dt = zjs_ApplyDAL.GetCheckHistoryList(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
            else
            {
                DataTable dt = zjs_ApplyDAL.GetCheckHistoryList2(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
        }

        //打印申请单
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = "";
                string fileName = "";
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
               
                
                fileName = "北京市二级注册造价工程师执业企业变更注册申请表";

                zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = ViewState["zjs_ApplyChangeMDL"] as zjs_ApplyChangeMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                //UnitMDL u= UnitDAL.GetObjectByPSN_RegisterNo(_zjs_ApplyMDL.PSN_RegisterNo);
                //zjs_CertificateMDL _zjs_CertificateMDL = zjs_CertificateDAL.GetObjectByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                var o = new List<object>();
                o.Add(_zjs_ApplyMDL);
                o.Add(_zjs_ApplyChangeMDL);
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
               

                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["Birthday"] = _zjs_ApplyMDL.Birthday == null ? "" : _zjs_ApplyMDL.Birthday.Value.ToString("yyyy年MM月dd日");
                ht["ValidDate"] = _zjs_ApplyChangeMDL.ValidDate == null ? "" : _zjs_ApplyChangeMDL.ValidDate.Value.ToString("yyyy年MM月dd日");              
                ht["ENT_Corporate"] = _zjs_ApplyChangeMDL.OldFR;
                ht["OldENT_Correspondence"] = _zjs_ApplyChangeMDL.OldEND_Addess;
                ht["oldENT_Type"] = _zjs_ApplyChangeMDL.OldENT_Type;

                zjs_ApplyMDL _zjs_ApplyMDL2 = (ViewState["zjs_ApplyMDL2"] != null) ? (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"] : null;//专业2申请表
                if (_zjs_ApplyMDL2 == null)
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师执业企业变更注册申请表.docx");
                }
                else
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师执业企业变更注册多专业申请表.docx");
                    ht["PSN_RegisterNo2"] = _zjs_ApplyMDL2.PSN_RegisterNo;
                    ht["PSN_RegisteProfession2"] = _zjs_ApplyMDL2.PSN_RegisteProfession;
                    zjs_ApplyChangeMDL _zjs_ApplyChangeMDL2 = (zjs_ApplyChangeMDL)ViewState["zjs_ApplyChangeMDL2"];
                    ht["ValidDate2"] = _zjs_ApplyChangeMDL2.ValidDate == null ? "" : _zjs_ApplyChangeMDL2.ValidDate.Value.ToString("yyyy年MM月dd日");
                }

                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印造价工程师执业企业信息变更Word失败！", ex);
            }
        }

        //删除申请单
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            zjs_ApplyMDL _zjs_ApplyMDL2 = null;
            if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)
            {
                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    zjs_ApplyDAL.Delete(tran,_zjs_ApplyMDL.ApplyID);
                    if (ViewState["zjs_ApplyMDL2"] != null)
                    {
                        _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                        zjs_ApplyDAL.Delete(tran, _zjs_ApplyMDL2.ApplyID);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "删除造价工程师执业企业变更申请表失败！", ex);
                    return;
                }

                UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师执业企业变更申请表", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}{3}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text
                    , LabelPSN_RegisteProfession2.Text == "" ? LabelPSN_RegisteProfession2.Text : string.Format("，{0}", LabelPSN_RegisteProfession2.Text)));
                ViewState.Remove("zjs_ApplyMDL");
                if (_zjs_ApplyMDL2 !=null)
                {
                    ViewState.Remove("zjs_ApplyMDL2");
                }
                SetButtonEnable(_zjs_ApplyMDL);
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
            try
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
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取造价工程师注册申请单附件信息失败！", ex);
            }
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
                UIHelp.WriteErrorLog(Page, "删除造价工程师执业企业变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师执业企业变更申请表附件", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，附件ID：{3}。", LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, FileID));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }
               

        //审批节点后退
        protected void ButtonSendBack_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);

            if (_zjs_ApplyMDL.ApplyStatus == RadComboBoxReturnApplyStatus.SelectedValue)
            {
                UIHelp.layerAlert(Page, "后退节点与当前申请单所处节点相同，无需后退！");
                return;
            }

            string log = "";
            switch (_zjs_ApplyMDL.ApplyStatus)
            {
                case EnumManager.ZJSApplyStatus.已受理:
                case EnumManager.ZJSApplyStatus.已驳回:
                    log = string.Format("姓名：{0}，身份证号：{1}，受理时间：{2}，受理结果：{3}，受理意见：{4}。后退到“{5}”状态。"
                   , LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _zjs_ApplyMDL.GetDateTime, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    _zjs_ApplyMDL.GetDateTime = null;
                    _zjs_ApplyMDL.GetMan = null;
                    _zjs_ApplyMDL.GetResult = null;
                    _zjs_ApplyMDL.GetRemark = null;
                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)//取消最后驳回意见
                    {
                        _zjs_ApplyMDL.LastBackResult = null;
                    }
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                    break;
                case EnumManager.ZJSApplyStatus.已审核:
                    log = string.Format("姓名：{0}，身份证号：{1}，审核时间：{2}，审核结果：{3}，审核意见：{4}。后退到“{5}”状态。"
                           , LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _zjs_ApplyMDL.ExamineDatetime, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ZJSApplyStatus.已申报:
                            _zjs_ApplyMDL.GetDateTime = null;
                            _zjs_ApplyMDL.GetMan = null;
                            _zjs_ApplyMDL.GetResult = null;
                            _zjs_ApplyMDL.GetRemark = null;

                            _zjs_ApplyMDL.ExamineDatetime = null;
                            _zjs_ApplyMDL.ExamineMan = null;
                            _zjs_ApplyMDL.ExamineResult = null;
                            _zjs_ApplyMDL.ExamineRemark = null;
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                            break;
                        case EnumManager.ZJSApplyStatus.已受理:
                            _zjs_ApplyMDL.ExamineDatetime = null;
                            _zjs_ApplyMDL.ExamineMan = null;
                            _zjs_ApplyMDL.ExamineResult = null;
                            _zjs_ApplyMDL.ExamineRemark = null;
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已受理;
                            break;
                    }

                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    log = string.Format("姓名：{0}，身份证号：{1}，决定时间：{2}，决定结果：{3}。后退到“{4}”状态。"
                           , LabelPSN_Name.Text, LabelPSN_CertificateNO.Text, _zjs_ApplyMDL.ConfirmDate, _zjs_ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ZJSApplyStatus.已申报:
                            _zjs_ApplyMDL.GetDateTime = null;
                            _zjs_ApplyMDL.GetMan = null;
                            _zjs_ApplyMDL.GetResult = null;
                            _zjs_ApplyMDL.GetRemark = null;

                            _zjs_ApplyMDL.ExamineDatetime = null;
                            _zjs_ApplyMDL.ExamineMan = null;
                            _zjs_ApplyMDL.ExamineResult = null;
                            _zjs_ApplyMDL.ExamineRemark = null;

                            _zjs_ApplyMDL.ConfirmDate = null;
                            _zjs_ApplyMDL.ConfirmMan = null;
                            _zjs_ApplyMDL.ConfirmResult = null;
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                            break;
                        case EnumManager.ZJSApplyStatus.已受理:
                            _zjs_ApplyMDL.ExamineDatetime = null;
                            _zjs_ApplyMDL.ExamineMan = null;
                            _zjs_ApplyMDL.ExamineResult = null;
                            _zjs_ApplyMDL.ExamineRemark = null;

                            _zjs_ApplyMDL.ConfirmDate = null;
                            _zjs_ApplyMDL.ConfirmMan = null;
                            _zjs_ApplyMDL.ConfirmResult = null;
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已受理;
                            break;
                        case EnumManager.ZJSApplyStatus.已审核:
                            _zjs_ApplyMDL.ConfirmDate = null;
                            _zjs_ApplyMDL.ConfirmMan = null;
                            _zjs_ApplyMDL.ConfirmResult = null;
                            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已审核;
                            break;
                    }
                    break;
                default:
                    UIHelp.layerAlert(Page, "当前处在无法后退节点！");
                    return;
            }
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                _zjs_ApplyMDL.XGR = UserName;
                zjs_ApplyDAL.Update(tran,_zjs_ApplyMDL);

                if (ViewState["zjs_ApplyMDL2"] != null)
                {
                    zjs_ApplyMDL _zjs_ApplyMDL2 = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL2"];
                    _zjs_ApplyMDL2.GetDateTime = _zjs_ApplyMDL.GetDateTime;
                    _zjs_ApplyMDL2.GetMan = _zjs_ApplyMDL.GetMan;
                    _zjs_ApplyMDL2.GetResult = _zjs_ApplyMDL.GetResult;
                    _zjs_ApplyMDL2.GetRemark = _zjs_ApplyMDL.GetRemark;
                    _zjs_ApplyMDL2.LastBackResult = _zjs_ApplyMDL.LastBackResult;
                    _zjs_ApplyMDL2.ExamineDatetime = _zjs_ApplyMDL.ExamineDatetime;
                    _zjs_ApplyMDL2.ExamineMan = _zjs_ApplyMDL.ExamineMan;
                    _zjs_ApplyMDL2.ExamineResult = _zjs_ApplyMDL.ExamineResult;
                    _zjs_ApplyMDL2.ExamineRemark = _zjs_ApplyMDL.ExamineRemark;
                    _zjs_ApplyMDL2.ConfirmDate = _zjs_ApplyMDL.ConfirmDate;
                    _zjs_ApplyMDL2.ConfirmMan = _zjs_ApplyMDL.ConfirmMan;
                    _zjs_ApplyMDL2.ConfirmResult = _zjs_ApplyMDL.ConfirmResult;
                    _zjs_ApplyMDL2.ApplyStatus = _zjs_ApplyMDL.ApplyStatus;
                    _zjs_ApplyMDL2.XGSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL2.XGR = _zjs_ApplyMDL.XGR;
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL2);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "后退造价工程师执业企业变更审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师执业企业变更审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }
    }

}