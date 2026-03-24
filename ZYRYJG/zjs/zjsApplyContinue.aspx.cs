using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using System.Text.RegularExpressions;
using Utility;

namespace ZYRYJG.zjs
{
    public partial class zjsApplyContinue : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["zjs_ApplyMDL"] == null ? "" : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyID; }
        }

        /// <summary>
        /// 二级造价工程师公益培训配置
        /// </summary>
        protected PackageMDL _PackageMDL
        {
            get
            {
                if (Cache["ZJS_Package"] != null)
                {
                    return (PackageMDL)Cache["ZJS_Package"];
                }
                else
                {
                    PackageMDL _PackageMDL = PackageDAL.GetObject("二级造价工程师", RadTextBoxPSN_RegisteProfession.Text.Trim());
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "ZJS_Package", _PackageMDL, 1);//缓存1小时
                    return _PackageMDL;
                }
            }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsApplyList.aspx|zjs/zjsAgency.aspx|zjs/zjsBusinessQuery.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;

                    zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = zjs_ApplyContinueDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyContinueMDL"] = _zjs_ApplyContinueMDL;
                                       
                    if (string.IsNullOrEmpty(_zjs_ApplyMDL.LastBackResult)==false && _zjs_ApplyMDL.ApplyStatus != EnumManager.ZJSApplyStatus.已驳回)
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("【上次退回记录】{0}", _zjs_ApplyMDL.LastBackResult);

                    }
                   
                    if (IfExistRoleID("0") == true 
                     && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                     )
                    {
                        UIHelp.SetData(EditTable, _zjs_ApplyMDL, true);//基本信息    
                        UIHelp.SetData(EditTable, _zjs_ApplyContinueMDL, true);                    
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);                      
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        divGR.Visible = true;//个人操作按钮 
                    }
                    else
                    {                       
                        UIHelp.SetData(EditTable, _zjs_ApplyMDL, true);
                        UIHelp.SetData(EditTable, _zjs_ApplyContinueMDL, true);  
                    }

                    RadTextBoxPSN_CertificateValidity.Text = _zjs_ApplyContinueMDL.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");

                    RefreshRadGridXuanXiu();
                    SetButtonEnable(_zjs_ApplyMDL);
                    ShowFinishGYPX();
                    //附件
                    BindFile(_zjs_ApplyMDL.ApplyID);
                    //审批记录
                    BindCheckHistory(_zjs_ApplyContinueMDL.ApplyID);

                    if (IfExistRoleID("0") == true && _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报)
                    {
                        if (CheckContinueTimeSpan(_zjs_ApplyMDL.PSN_RegisteProfession, _zjs_ApplyContinueMDL.PSN_CertificateValidity.Value) == false)
                        {
                            ButtonApply.Enabled = false;
                            ButtonUnit.Enabled = false;
                            ButtonSave.Enabled = false;
                            UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                        }

                    }

                    if (IfExistRoleID("2") == true)//企业
                    {
                        if (string.IsNullOrEmpty(Request["a"]) == false)//edit
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
                        //企业看不到各级申办人列
                        RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    }
                }
                else//new
                {
                    SetButtonEnable(null);
                    //二造证书
                    zjs_CertificateMDL o = zjs_CertificateDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));

                    //#region 查询证书是否锁定
                    //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(o.PSN_CertificateNO);
                    //if (count > 0)
                    //{
                    //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                    //    return;
                    //}
                    //#endregion

                    if (IfExistRoleID("0") == true)//个人登录后 
                    {
                        if (o != null)
                        {
                            ViewState["zjs_CertificateMDL"] = o;
                            UIHelp.SetData(EditTable, o, true);                           
                            UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                            //考生信息
                            WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                            RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
                            RadTextBoxPSN_CertificateType.Text = _WorkerOB.CertificateType;
                            RadTextBoxPSN_Sex.Text = _WorkerOB.Sex;
                            RadDatePickerBirthday.SelectedDate = _WorkerOB.Birthday;
                            UIHelp.SetReadOnly(RadDatePickerBirthday, true);

                            if (string.IsNullOrEmpty(_WorkerOB.Email) == false)
                            {
                                RadTextBoxPSN_Email.Text = _WorkerOB.Email;//邮箱
                            }
                            if (string.IsNullOrEmpty(_WorkerOB.Nation) == false)//民族
                            {
                                RadComboBoxItem find = RadComboBoxNation.Items.FindItemByText(_WorkerOB.Nation);
                                if (find != null) find.Selected = true;
                            }
                            //企业信息
                            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(o.ENT_OrganizationsCode);
                            if (_UnitMDL != null)
                            {
                                RadTextBoxEND_Addess.Text = _UnitMDL.END_Addess;
                                RadTextBoxENT_City.Text = _UnitMDL.ENT_City;
                                RadTextBoxFR.Text = _UnitMDL.ENT_Corporate;
                                RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                                RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                                //UIHelp.SetReadOnly(RadTextBoxENT_Telephone, true);
                            }                            
                            BindFile("0");//附件
                            divGR.Visible = true;//个人操作按钮 
                            if (CheckContinueTimeSpan(o.PSN_RegisteProfession, o.PSN_CertificateValidity.Value) == false)
                            {
                                ButtonApply.Enabled = false;
                                ButtonUnit.Enabled = false;
                                ButtonSave.Enabled = false;
                                divGR.Visible = false;//个人操作按钮 
                                UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                            }
                        }
                    }
                }

                ShowFinishGYPX();
                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;
                //申请操作权限
                if (IfExistRoleID("2") == true)
                {
                    //企业看不到各级申办人列
                    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                }               
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")//刷新附件
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
                Decide(_zjs_ApplyMDL);
            }  
        }

        /// <summary>
        ///  判断是否在续期允许时间段内
        /// </summary>
        /// <param name="_Profession">专业</param>
        /// <param name="ValidEndDate">有效期截止日期</param>
        /// <returns></returns>
        private bool CheckContinueTimeSpan(string _Profession, DateTime ValidEndDate)
        {
            if (DateTime.Now >= ValidEndDate.AddDays(-Model.EnumManager.ContinueTime.开始时间)
                  && DateTime.Now <= ValidEndDate.AddDays(-Model.EnumManager.ContinueTime.结束时间 + 1)
              )
            {
                return true;
            }
            else
                return false;
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
        
        //保存申请单
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 有效校验

            bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(RadTextBoxPSN_CertificateNO.Text.Trim());
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
            }       
            if (RadTextBoxENT_Telephone.Text.Trim() == "")
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
            LabelBiXiu.Text = "60";
            LabelXuanXiu.Text = "60";
            if (Utility.Check.CheckBirthdayLimit(-1, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满70周岁前90天起,不再允许发起二级造价工程师初始、重新、延续、变更注册申请。", 5, 0);
                return;
            }
            //所属区县
            if (string.IsNullOrEmpty(RadTextBoxENT_City.Text))
            {
                UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                return;
            }

            #endregion

            if (ViewState["zjs_ApplyMDL"] == null)
            {
                if (zjs_ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_RegisterNo.Text.Trim(), EnumManager.ZJSApplyStatus.未申报, EnumManager.ZJSApplyStatus.待确认)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }

            zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] == null ? new zjs_ApplyMDL() : (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            UIHelp.GetData(EditTable, _zjs_ApplyMDL);
            _zjs_ApplyMDL.XGR = UserName;
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.Valid = 1;
            _zjs_ApplyMDL.ApplyType = EnumManager.ZJSApplyType.延续注册;
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;

            zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = ViewState["zjs_ApplyContinueMDL"] == null ? new zjs_ApplyContinueMDL() : (zjs_ApplyContinueMDL)ViewState["zjs_ApplyContinueMDL"];//详细表
            UIHelp.GetData(EditTable, _zjs_ApplyContinueMDL);          

            if (UnitDAL.CheckGongShang(RadTextBoxENT_OrganizationsCode.Text) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxENT_Name.Text));
                return;
            }

            if (LabelBiXiuFinishCase.Text != "" && LabelBiXiuFinishCase.Text != "已达标")
            {
                UIHelp.layerAlert(Page, LabelBiXiuFinishCase.Text, 5, 0);
                return;
            }
            
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["zjs_ApplyMDL"] == null)//new
                {
                    UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxENT_OrganizationsCode.Text);
                    _zjs_ApplyMDL.ApplyCode = zjs_ApplyDAL.GetNextApplyCode(tran, "延续注册");
                    LabelApplyCode.Text = _zjs_ApplyMDL.ApplyCode;
                    _zjs_ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _zjs_ApplyMDL.CJR = UserName;
                    _zjs_ApplyMDL.CJSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL.ApplyTime = _zjs_ApplyMDL.XGSJ;

                    zjs_CertificateMDL person = (zjs_CertificateMDL)ViewState["zjs_CertificateMDL"];//人员表

                    
                    //延续注册表信息
                    _zjs_ApplyContinueMDL.ApplyID = _zjs_ApplyMDL.ApplyID;
                    _zjs_ApplyContinueMDL.ENT_MobilePhone = _UnitMDL.ENT_MobilePhone;
                    _zjs_ApplyContinueMDL.ENT_Correspondence = _UnitMDL.ENT_Correspondence;
                    _zjs_ApplyContinueMDL.ENT_Economic_Nature = _UnitMDL.ENT_Economic_Nature;
                    _zjs_ApplyContinueMDL.ENT_Postcode = _UnitMDL.ENT_Postcode;

                    zjs_ApplyDAL.Insert(tran, _zjs_ApplyMDL);
                    zjs_ApplyContinueDAL.Insert(tran, _zjs_ApplyContinueMDL);

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
                    _zjs_ApplyContinueMDL.PSN_MobilePhone = RadTextBoxPSN_MobilePhone.Text.Trim();
                    zjs_ApplyContinueDAL.Update(tran, _zjs_ApplyContinueMDL);
                }
                tran.Commit();
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                ViewState["zjs_ApplyContinueMDL"] = _zjs_ApplyContinueMDL;
                BindFile(ApplyID);
                RefreshRadGridXuanXiu();
                SetButtonEnable(_zjs_ApplyMDL);
                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师延续注册申请表信息失败", ex);
            }
        }

        //提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = (zjs_ApplyContinueMDL)ViewState["zjs_ApplyContinueMDL"];
            try
            {
                if (ButtonUnit.Text != "取消申报")
                {
                    if (LabelXuanXiuFinishCase.Text != "" && LabelXuanXiuFinishCase.Text != "已达标")
                    {
                        UIHelp.layerAlert(Page, LabelXuanXiuFinishCase.Text, 5, 0);
                        return;
                    }

                    //选修课扫描件
                    if (ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.继续教育证明扫描件) == false)
                    {
                        UIHelp.layerAlert(Page, "您还没有上传继续教育合格证明扫描件，请在线填写继续教育（选修课）培训记录，导出打印、单位盖章后扫描上传。");
                        return;
                    }

                    //必须上传附件集合
                    System.Collections.Hashtable fj;
                    fj = new System.Collections.Hashtable{
                    {EnumManager.FileDataTypeName.一寸免冠照片,0},
                    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    {EnumManager.FileDataTypeName.申请表扫描件,0}};


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
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！（社保验证结果在填写次日返回，当日填报并申请的需要自行上传社保证明）", sb), 5, 0);
                        return;
                    }

                    //检查一寸照片数量
                    int faceImgCount = ApplyFileDAL.SelectFaceImgCountByApplyID(_zjs_ApplyMDL.ApplyID);
                    if (faceImgCount > 1)
                    {
                        UIHelp.layerAlert(Page, string.Format("只能上传一张一寸免冠照片，请删除多余照片！", sb), 5, 0);
                        return;
                    }

                    if (_zjs_ApplyContinueMDL.PSN_CertificateValidity.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, "证书已经过期，无法再申请续期。请删除续期申请，注销证书后发起初始注册！", 5, 0);
                        return;
                    }
                }
              
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;

                if (ButtonUnit.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
                    _zjs_ApplyMDL.ApplyTime = null;
                    _zjs_ApplyMDL.OldUnitCheckResult = null;
                    _zjs_ApplyMDL.OldUnitCheckRemark = null;
                    _zjs_ApplyMDL.OldUnitCheckTime = null;
                }
                else
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.待确认;
                    _zjs_ApplyMDL.ApplyTime = DateTime.Now;

                    _zjs_ApplyMDL.OldUnitCheckResult = null;
                    _zjs_ApplyMDL.OldUnitCheckRemark = null;
                    _zjs_ApplyMDL.OldUnitCheckTime = null;

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
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                SetButtonEnable(_zjs_ApplyMDL);
                //人员照片
                BindFile(_zjs_ApplyMDL.ApplyID);
                if (_zjs_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请提交成功", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='zjsApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("applyZJS"), Utility.Cryptography.Encrypt(_zjs_ApplyMDL.ApplyID.ToString()), _zjs_ApplyMDL.ApplyID)); 
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");                 
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "造价工程师延续注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认，申报 or 取消申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = (zjs_ApplyContinueMDL)ViewState["zjs_ApplyContinueMDL"];

            if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报)
            {
                if (CheckContinueTimeSpan(_zjs_ApplyMDL.PSN_RegisteProfession, _zjs_ApplyContinueMDL.PSN_CertificateValidity.Value) == false)
                {
                    ButtonApply.Enabled = false;
                    ButtonUnit.Enabled = false;
                    ButtonSave.Enabled = false;
                    UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                    return;
                }
            }
           
            try
            {                
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonApply.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    if (LabelBiXiuFinishCase.Text != "" && LabelBiXiuFinishCase.Text != "已达标")
                    {
                        UIHelp.layerAlert(Page, LabelBiXiuFinishCase.Text, 5, 0);
                        return;
                    }

                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldUnitCheckRemark.Visible = true;
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                        _zjs_ApplyMDL.LastBackResult = string.Format("{0}企业驳回申请，驳回说明：{1}",_zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss") ,TextBoxOldUnitCheckRemark.Text.Trim());//现单位审核意见;
                    }
                    else {
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;

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

                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                SetButtonEnable(_zjs_ApplyMDL);               

                //人员照片
                BindFile(_zjs_ApplyMDL.ApplyID);
                if (_zjs_ApplyMDL.ApplyStatus == "已申报")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
                    string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "延续注册申报失败！", ex);
            }         
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {            
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            zjs_ApplyDAL.Delete(_zjs_ApplyMDL.ApplyID);
            ViewState.Remove("zjs_ApplyMDL");
            SetButtonEnable(_zjs_ApplyMDL);
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace(" green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");            
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");          
            step_已公告.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");

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
                case EnumManager.ZJSApplyStatus.已公告:
                    step_已公告.Attributes["class"] += " green";
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
                    break;
                case EnumManager.ZJSApplyStatus.未申报:
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
                case EnumManager.ZJSApplyStatus.待确认:
                    ButtonSave.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确定";
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";

                    //个人
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";

                    //受理权限
                    if (IfExistRoleID("20") == true )
                    {
                        divQX.Visible = true;
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
                    if (IfExistRoleID("21") == true )
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
                        if (string.IsNullOrEmpty(o.NoticeCode) == true)//初始、续期注册尚未公告才能后退，如果保存了公告，需要人工后退。
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
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

                    if (o.GetDateTime.HasValue == true && o.ExamineDatetime.HasValue == false)//受理驳回
                    {
                        //注册室管理员
                        if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
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

            if (o != null)
            {
                trXuanXiuDetail.Visible = true;
                trWorkResult.Visible = true;
                LabelWorkResult.Visible = true;
                if (o.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || o.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)
                {
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                    RadGridXuanXiu.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    RadGridXuanXiu.Rebind();

                    RadGridWorkResult.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                    RadGridWorkResult.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                    RadGridWorkResult.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    RadGridWorkResult.Rebind();
                }
                else
                {
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Edit").Visible = false;
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Delete").Visible = false;
                    RadGridXuanXiu.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    RadGridXuanXiu.Rebind();

                    RadGridWorkResult.MasterTableView.Columns.FindByUniqueName("Edit").Visible = false;
                    RadGridWorkResult.MasterTableView.Columns.FindByUniqueName("Delete").Visible = false;
                    RadGridWorkResult.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    RadGridWorkResult.Rebind();
                }
            }
            else
            {
                trXuanXiuDetail.Visible = false;
                trWorkResult.Visible = false;
                LabelWorkResult.Visible = false;
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
            //公益视频课时总数
            if (LabelBiXiuFinishCase.Text != "" && LabelBiXiuFinishCase.Text != "已达标")
            {
                UIHelp.layerAlert(Page, LabelBiXiuFinishCase.Text, 5, 0);
                return;
            }

            //录入的选修课课时总数
            if (LabelXuanXiuFinishCase.Text != "" && LabelXuanXiuFinishCase.Text != "已达标")
            {
                UIHelp.layerAlert(Page, LabelXuanXiuFinishCase.Text, 5, 0);
                return;
            }

            try
            {
                string sourceFile = "";
                string fileName = "";
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;

                sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师延续注册申请表.docx");
                fileName = string.Format("北京市二级注册建造价工程师续注册申请表_{0}",_zjs_ApplyMDL.PSN_Name);

                zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = ViewState["zjs_ApplyContinueMDL"] as zjs_ApplyContinueMDL;
                UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(_zjs_ApplyMDL.ENT_OrganizationsCode);
                zjs_CertificateMDL _zjs_CertificateMDL = zjs_CertificateDAL.GetObjectByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                var o = new List<object>();                
                o.Add(_zjs_ApplyMDL);
                o.Add(_zjs_ApplyContinueMDL);
                o.Add(_UnitMDL);
                o.Add(_zjs_CertificateMDL);
                
                var ht = PrintDocument.GetProperties(o);

                ht["PSN_BirthDate"] = _zjs_ApplyMDL.Birthday == null ? "" : _zjs_ApplyMDL.Birthday.Value.ToString("yyyy年MM月dd日");
                ht["PSN_CertificateValidity"] = _zjs_ApplyContinueMDL.PSN_CertificateValidity == null ? "" : _zjs_ApplyContinueMDL.PSN_CertificateValidity.Value.ToString("yyyy年MM月dd日");
                //ht["Nation"] = _zjs_CertificateMDL.PSN_National;
                //ht["FR"] = _UnitMDL.ENT_Corporate;
              
                
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
                
                string sql = string.Format(@"SELECT [DataNo],year([TrainDateStart]),convert(varchar(10),[TrainDateStart],21) +'至' +convert(varchar(10),[TrainDateEnd],21),[TrainName],[TrainWay],[TrainUnit],[Period] 
                                            FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}'  and [PostTypeID] =-1) order by [DataNo] "
                    , _zjs_ApplyMDL.ApplyID);
                DataTable dt = CommonDAL.GetDataTable(sql);//继续教育委培记录
                int SumPeriod = 0;
                foreach (DataRow r in dt.Rows)
                {
                    SumPeriod += Convert.ToInt32(r["Period"]);
                }
                ht["SumPeriod"] = SumPeriod;//合计学时

                DataTable dtWorkResult = CommonDAL.GetDataTable(string.Format("select [DataNo],convert(varchar(10),[DateStart],21) +'至' +convert(varchar(10),[DateEnd],21),[ProjectName],[Job],[Cost] from dbo.WorkResult where ApplyID='{0}' order by DataNo", _zjs_ApplyMDL.ApplyID));//工作业绩
                if (dtWorkResult == null || dtWorkResult.Rows.Count==0)
                {
                    dtWorkResult = CommonDAL.GetDataTable("select '','','无','',''");//工作业绩
                }
                ht["tableList"] = new List<DataTable> { dtWorkResult,dt};

                //表格的索引
                ht["tableIndex"] = new List<int> { 1,3 };
                //行的索引
                ht["insertIndex"] = new List<int> { 1,1 };
                ht["ContainsHeader"] = new List<bool> { true,true };
                ht["isCtable"] = true;

                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印造价工程师价师延续注册申请失败！", ex);
            }
        }

        //受理
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
                _zjs_ApplyMDL.LastBackResult = string.Format("{0}市级受理环节驳回申请，驳回说明：{1}", _zjs_ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师延续注册受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册受理", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3},审批结果：{4}，审批意见：{5}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark));
            UIHelp.ParentAlert(Page, "受理成功！", true);

            //string js = string.Format("<script>window.parent.location.href='../zjs/zjsBusinessQuery.aspx?id={0}&&type={1}';</script>", _zjs_ApplyMDL.ApplyID, _zjs_ApplyMDL.ApplyType);
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
            _zjs_ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已审核 ;

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师延续注册审核失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册审核", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3},审批结果：{4}，审批意见：{5}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark));
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

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师延续批准失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册批准", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，延续注册专业：{3}，批准意见：{4}", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.ConfirmResult));
            UIHelp.ParentAlert(Page, "批准成功！", true);
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
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，受理时间：{3}，受理结果：{4}，受理意见：{5}。后退到“{6}”状态。"
                   , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.GetDateTime, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，审核时间：{3}，审核结果：{4}，审核意见：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.ExamineDatetime, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，决定时间：{3}，决定结果：{4}。后退到“{5}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.ConfirmDate, _zjs_ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
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

            try
            {
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                _zjs_ApplyMDL.XGR = UserName;
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "后退造价工程师延续注册审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师延续注册审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
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
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师延续注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，附件ID：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text, FileID));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }
      

//        //载入测试附件************************************************
//        protected void ButtonLoadFile_Click(object sender, EventArgs e)
//        {
//            CommonDAL.ExecSQL(string.Format(@"
//INSERT INTO [dbo].[ApplyFile]([FileID],[ApplyID],[CheckResult],[CheckDesc])
//SELECT [FileID],'{0}',[CheckResult],[CheckDesc] FROM [dbo].[ApplyFile]where [ApplyID]='43e7d3fc-907f-4013-84ce-53f60e04beef';", ApplyID));

//            BindFile(ApplyID);
//        }

        //绑定待编辑二造继续教育选修课记录
        protected void RadGridXuanXiu_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Button ButtonSave = editedItem.Cells[0].FindControl("ButtonSave") as Button;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    Int64 id = Convert.ToInt64(RadGridXuanXiu.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
                    JxjyDetailMDL _JxjyDetailMDL = JxjyDetailDAL.GetObject(id);
                    ViewState["JxjyDetailMDL"] = _JxjyDetailMDL;
                    UIHelp.SetData(editedItem, _JxjyDetailMDL);

                    RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
                    RadioButtonListTrainWay.Items.FindByValue(_JxjyDetailMDL.TrainWay).Selected = true;
                }

            }
        }

        //新增二造继续教育选修课记录
        protected void RadGridXuanXiu_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];

            JxjyBaseMDL _JxjyBaseMDL = JxjyBaseDAL.GetObjectByApplyID(-1, _zjs_ApplyMDL.ApplyID);

            JxjyDetailMDL _JxjyDetailMDL = new JxjyDetailMDL();
            RadNumericTextBox RadNumericTextBoxDataNo = editedItem.Cells[0].FindControl("RadNumericTextBoxDataNo") as RadNumericTextBox;
            _JxjyDetailMDL.DataNo = Convert.ToInt32(RadNumericTextBoxDataNo.Value);
            RadDatePicker RadDatePickerTrainDateStart = editedItem.Cells[0].FindControl("RadDatePickerTrainDateStart") as RadDatePicker;
            _JxjyDetailMDL.TrainDateStart = RadDatePickerTrainDateStart.SelectedDate;
            RadDatePicker RadDatePickerTrainDateEnd = editedItem.Cells[0].FindControl("RadDatePickerTrainDateEnd") as RadDatePicker;
            _JxjyDetailMDL.TrainDateEnd = RadDatePickerTrainDateEnd.SelectedDate;
            RadTextBox RadTextBoxTrainName = editedItem.Cells[0].FindControl("RadTextBoxTrainName") as RadTextBox;
            _JxjyDetailMDL.TrainName = RadTextBoxTrainName.Text;
            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _JxjyDetailMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;
            RadTextBox RadTextBoxTrainUnit = editedItem.Cells[0].FindControl("RadTextBoxTrainUnit") as RadTextBox;
            _JxjyDetailMDL.TrainUnit = RadTextBoxTrainUnit.Text;
            RadNumericTextBox RadNumericTextBoxPeriod = editedItem.Cells[0].FindControl("RadNumericTextBoxPeriod") as RadNumericTextBox;
            _JxjyDetailMDL.Period = Convert.ToInt32(RadNumericTextBoxPeriod.Value);
            _JxjyDetailMDL.cjsj = DateTime.Now;

            if (_JxjyBaseMDL == null)
            {
                DBHelper db = new DBHelper();
                DbTransaction trans = db.BeginTransaction();
                try
                {
                    _JxjyBaseMDL = new JxjyBaseMDL();
                    _JxjyBaseMDL.PostTypeID = -1;
                    _JxjyBaseMDL.WorkerName = RadTextBoxPSN_Name.Text.Trim();
                    _JxjyBaseMDL.WorkerCertificateCode = RadTextBoxPSN_CertificateNO.Text.Trim();
                    _JxjyBaseMDL.PostTypeName = "二级造价工程师";
                    _JxjyBaseMDL.PostName = RadTextBoxPSN_RegisteProfession.Text.Trim();
                    _JxjyBaseMDL.CertificateCode = RadTextBoxPSN_RegisterNo.Text.Trim();
                    _JxjyBaseMDL.ValidEndDate = Convert.ToDateTime(RadTextBoxPSN_CertificateValidity.Text);
                    _JxjyBaseMDL.UnitName = RadTextBoxENT_Name.Text.Trim();
                    _JxjyBaseMDL.cjsj = DateTime.Now;
                    _JxjyBaseMDL.ApplyID = _zjs_ApplyMDL.ApplyID;
                    JxjyBaseDAL.Insert(trans, _JxjyBaseMDL);

                    _JxjyDetailMDL.BaseID = _JxjyBaseMDL.BaseID;
                    JxjyDetailDAL.Insert(trans, _JxjyDetailMDL);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    UIHelp.WriteErrorLog(Page, "添加二造继续教育选修课记录失败！", ex);
                    return;
                }
            }
            else
            {
                try
                {
                    _JxjyDetailMDL.BaseID = _JxjyBaseMDL.BaseID;
                    JxjyDetailDAL.Insert(_JxjyDetailMDL);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "添加二造继续教育选修课记录失败！", ex);
                    return;
                }
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "添加二造继续教育选修课记录", string.Format("管理号：{0}。", RadTextBoxPSN_RegisterNo.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", trXuanXiuDetail.ClientID), true);
        }

        //修改二造继续教育选修课记录
        protected void RadGridXuanXiu_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            JxjyDetailMDL _JxjyDetailMDL = (JxjyDetailMDL)ViewState["JxjyDetailMDL"];
            UIHelp.GetData(editedItem, _JxjyDetailMDL);

            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _JxjyDetailMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;

            try
            {
                JxjyDetailDAL.Update(_JxjyDetailMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更新二造继续教育选修课记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新二造继续教育选修课记录", string.Format("管理号：{0}。", RadTextBoxPSN_RegisterNo.Text));
            //Button ButtonSave = editedItem.Cells[0].FindControl("ButtonSave") as Button;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

        }

        //删除二造继续教育选修课记录
        protected void RadGridXuanXiu_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 DetailID = Convert.ToInt64(RadGridXuanXiu.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
            try
            {
                JxjyDetailDAL.Delete(DetailID);

                JxjyBaseDAL.DeleteNullSubDetail(-1, ((zjs_ApplyMDL)ViewState["zjs_ApplyMDL"]).ApplyID.ToString());

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除二造继续教育选修课记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除二造继续教育选修课记录", string.Format("管理号：{0}。", RadTextBoxPSN_RegisterNo.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", trXuanXiuDetail.ClientID), true);
        }

        //提供二造继续教育选修课记录
        protected void RadGridXuanXiu_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshRadGridXuanXiu();
        }

        /// <summary>
        /// 绑定选修课教育培训记录
        /// </summary>
        protected void RefreshRadGridXuanXiu()
        {
            try
            {
                zjs_ApplyMDL o = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];

                DataTable dt = JxjyDetailDAL.GetList(-1, o.ApplyID);//继续教育委培记录
                RadGridXuanXiu.DataSource = dt;

                int sumXuanXiu = 0;
                foreach (DataRow r in dt.Rows)
                {
                    sumXuanXiu += Convert.ToInt32(r["Period"]);
                }
                if (sumXuanXiu > 0)
                {
                    LabelXuanXiu.Text = sumXuanXiu.ToString();
                }
                else
                {
                    LabelXuanXiu.Text = "";
                }

                int needPeriod = _PackageMDL.Period.Value / 45;//每年应当完成学时数(2023-10-01后实施)
                //int needPeriod = 0;//临时
                int yearSpan = 4;//逾期年度

                DateTime checkdate = (o.ApplyTime.HasValue == false ? o.XGSJ.Value : o.ApplyTime.Value);

                //if (JxjyDetailDAL.CheckXuanXiuPeriod(yearSpan, needPeriod, checkdate, o.ApplyID, -1) == false)
                //{
                //    LabelXuanXiuFinishCase.Text = string.Format("未达标。近一个注册有效期内继续教育（选修课）学习完成情况未达到{0}学时（每年{1}学时），请根据学习完成情况在线填写。", yearSpan * needPeriod, needPeriod);
                //}
                if (JxjyDetailDAL.CheckXuanXiuSumPeriod(yearSpan, needPeriod, checkdate, o.ApplyID, -1) == false)
                {
                    LabelXuanXiuFinishCase.Text = string.Format("未达标，近一个注册有效期内继续教育（选修课）学习完成情况未达到{0}学时。", yearSpan * needPeriod);
                }
                else
                {
                    LabelXuanXiuFinishCase.Text = "已达标";
                }             
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取继续教育选修课记录失败！", ex);
                return;
            }
        }

        /// <summary>
        /// 显示必修课（公益培训记录）
        /// </summary>
        protected void ShowFinishGYPX()
        {
            //规则：
            //当人员初次注册证书时，职业资格证书自批准之日起 18 个月后，首次申请初始注册的北京市二级造价工程师，应提供自申请注册之日起算.近1年不少于 15学时的继续教育学习证明
            //二级造价工程是初始注册、续期读取公益培训记录，考试资格证书取得年份过期1年需要15学时、2年需要30学习，3年需要45学时，4年及以上需要60学时。

            DateTime applyDate = (ViewState["zjs_ApplyMDL"] == null ? DateTime.Now : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyTime.Value);//申请时间

            int needPeriod = _PackageMDL.Period.Value / 45;//每年应当完成学时数(2023-10-01后实施)
            //int needPeriod = 0;//临时

            int yearSpan = 4;//逾期年度

            //本人已经完成的学时数
            decimal myPeriod = FinishSourceWareDAL.GetFinisthPeriod(RadTextBoxPSN_CertificateNO.Text.Trim(), "二级造价工程师", RadTextBoxPSN_RegisteProfession.Text.Trim(), applyDate.AddYears(-yearSpan), applyDate);

            if (myPeriod >= (needPeriod * yearSpan))
            {
                LabelBiXiu.Text = (needPeriod * yearSpan).ToString();
                LabelBiXiuFinishCase.Text = "已达标";
            }
            else
            {
                LabelBiXiu.Text = Convert.ToInt32(myPeriod).ToString();
                LabelBiXiuFinishCase.Text = string.Format("未达标：请首先完成{0}学时逾期注册续教育培训，再申请注册！点击“公益教育培训 - 我的培训 - 个人空间”选择证书对应课程学习。", needPeriod * yearSpan);
            }
        }

        protected void RadGridWorkResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                zjs_ApplyMDL o = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];

                DataTable dt = WorkResultDAL.GetList(o.ApplyID);//继续教育委培记录
                RadGridWorkResult.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取继续教育工作业绩失败！", ex);
                return;
            }
        }

        protected void RadGridWorkResult_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 DetailID = Convert.ToInt64(RadGridWorkResult.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
            try
            {
                WorkResultDAL.Delete(DetailID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除二造继续教育工作业绩失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除二造继续教育工作业绩", string.Format("ApplyID：{0}。", RadGridWorkResult.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"]));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", trWorkResult.ClientID), true);

        }

        protected void RadGridWorkResult_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            WorkResultMDL _WorkResultMDL = (WorkResultMDL)ViewState["WorkResultMDL"];
            UIHelp.GetData(editedItem, _WorkResultMDL);

  
            try
            {
                WorkResultDAL.Update(_WorkResultMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更新二造继续教育工作业绩失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新二造继续教育工作业绩", string.Format("ApplyID：{0}。", _WorkResultMDL.ApplyID)); ;
   
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

        }

        protected void RadGridWorkResult_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];


            WorkResultMDL _WorkResultMDL = new WorkResultMDL();
            RadNumericTextBox RadNumericTextBoxDataNo = editedItem.Cells[0].FindControl("RadNumericTextBoxDataNo") as RadNumericTextBox;
            _WorkResultMDL.DataNo = Convert.ToInt32(RadNumericTextBoxDataNo.Value);
            RadDatePicker RadDatePickerDateStart = editedItem.Cells[0].FindControl("RadDatePickerDateStart") as RadDatePicker;
            _WorkResultMDL.DateStart = RadDatePickerDateStart.SelectedDate;
            RadDatePicker RadDatePickerDateEnd = editedItem.Cells[0].FindControl("RadDatePickerDateEnd") as RadDatePicker;
            _WorkResultMDL.DateEnd = RadDatePickerDateEnd.SelectedDate;
            RadTextBox RadTextBoxProjectName = editedItem.Cells[0].FindControl("RadTextBoxProjectName") as RadTextBox;
            _WorkResultMDL.ProjectName = RadTextBoxProjectName.Text;

            RadTextBox RadTextBoxJob = editedItem.Cells[0].FindControl("RadTextBoxJob") as RadTextBox;
            _WorkResultMDL.Job = RadTextBoxJob.Text;

            RadNumericTextBox RadNumericCost = editedItem.Cells[0].FindControl("RadNumericCost") as RadNumericTextBox;
            _WorkResultMDL.Cost = Convert.ToString(RadNumericCost.Value);
            _WorkResultMDL.ApplyID = _zjs_ApplyMDL.ApplyID;


            try
            {
                WorkResultDAL.Insert(_WorkResultMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加二造继续教育工作业绩失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "添加二造继续教育工作业绩", string.Format("ApplyID：{0}。", _zjs_ApplyMDL.ApplyID));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", trWorkResult.ClientID), true);

        }

        protected void RadGridWorkResult_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Button ButtonSave = editedItem.Cells[0].FindControl("ButtonSave") as Button;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    Int64 id = Convert.ToInt64(RadGridWorkResult.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
                    WorkResultMDL _WorkResultMDL = WorkResultDAL.GetObject(id);
                    ViewState["WorkResultMDL"] = _WorkResultMDL;
                    UIHelp.SetData(editedItem, _WorkResultMDL);

                    RadNumericTextBox RadNumericCost = editedItem.Cells[0].FindControl("RadNumericCost") as RadNumericTextBox;
                    RadNumericCost.Value = Convert.ToInt32(_WorkResultMDL.Cost);

                    //RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
                    //RadioButtonListTrainWay.Items.FindByValue(_JxjyDetailMDL.TrainWay).Selected = true;
                }

            }
        }
    }
}