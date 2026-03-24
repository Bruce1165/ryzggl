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

namespace ZYRYJG.zjs
{
    public partial class zjsApplyCancel : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["zjs_ApplyMDL"] == null ? "" : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyID; }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsApplyList.aspx|zjs/zjsAgency.aspx|zjs/zjsBusinessQuery.aspx";
            }
        }

        /// <summary>
        /// 根据业务时间，显示注销原因待选项
        /// </summary>
        /// <param name="date">申请申请单创建日期</param>
        protected void FormatDDLCancelReason(DateTime? date)
        {
            string reason = "已与聘用单位解除劳动关系的;距离注册专业有效期不足30天的;受到行政或刑事处罚且在处罚期内的;法律、法规规定其他导致注册证书失效的。";
            for (int i = RadioButtonListCancelReason.Items.Count - 1; i >= 0; i--)
            {
                if (reason.Contains(RadioButtonListCancelReason.Items[i].Value) == false)
                {

                    RadioButtonListCancelReason.Items.Remove(RadioButtonListCancelReason.Items[i]);
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
                    //divViewAction.Visible = true;
                    Disabled();//禁用控件
                }

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    //注册申请表     
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                    if (string.IsNullOrEmpty(_zjs_ApplyMDL.LastBackResult) == false && _zjs_ApplyMDL.ApplyStatus != EnumManager.ZJSApplyStatus.已驳回)
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("【上次退回记录】{0}", _zjs_ApplyMDL.LastBackResult);
                    }

                    zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = zjs_ApplyCancelDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyCancelMDL"] = _zjs_ApplyCancelMDL;
                    RadTextBoxRegisterValidity.Text = _zjs_ApplyCancelMDL.RegisterValidity.Value.ToString("yyyy-MM-dd");

                    FormatDDLCancelReason(_zjs_ApplyMDL.CJSJ);

                    if (
                            (
                                (IfExistRoleID("0") == true && _zjs_ApplyCancelMDL.CancelReason != "申请强制注销（因二级造价工程师不配合等原因）")//个人
                                || (IfExistRoleID("2") == true && _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
                            )
                     && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报
                     || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认
                     || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回
                     || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                     )
                    {
                        divGR.Visible = true;
                    }

                    if (
                           (
                               IfExistRoleID("0") == true//个人
                               || (IfExistRoleID("2") == true && _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
                           )
                            &&
                            (_zjs_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报
                               || _zjs_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回
                            )
                    )
                    {
                        UIHelp.SetData(EditTable, _zjs_ApplyMDL, true);
                        UIHelp.SetData(EditTable, _zjs_ApplyCancelMDL, true);

                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);//联系电话                       
                        UIHelp.SetReadOnly(RadTextBoxLinkMan, false);//联系人
                        UIHelp.SetReadOnly(RadioButtonListCancelReason, false);
                        UIHelp.SetReadOnly(RadioButtonList_enforce, false);

                    }
                    else
                    {
                        UIHelp.SetData(EditTable, _zjs_ApplyMDL, true);
                        UIHelp.SetData(EditTable, _zjs_ApplyCancelMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxLinkMan, true);
                        UIHelp.SetReadOnly(RadioButtonListCancelReason, true);
                        UIHelp.SetReadOnly(RadioButtonList_enforce, true);
                    }

                    //申请操作权限
                    if (IfExistRoleID("2") == true)
                    {
                        //企业看不到各级申办人列
                        RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;

                        if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报 
                            && _zjs_ApplyCancelMDL.CancelReason != "申请强制注销（因企业不配合、企业营业执照注销等原因）"
                            && _zjs_ApplyCancelMDL.CancelReason != "申请强制注销（因二级造价工程师不配合等原因）")
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

                    SetButtonEnable(_zjs_ApplyMDL);
                    BindFile(_zjs_ApplyMDL.ApplyID);//附件                   
                    BindCheckHistory(_zjs_ApplyCancelMDL.ApplyID); //审批记录
                   
                    ListItem ApplyManType = RadioButtonListApplyManType.Items.FindByValue(_zjs_ApplyCancelMDL.ApplyManType);
                    if (ApplyManType != null) { ApplyManType.Selected = true; }
                    ListItem CancelReason = RadioButtonListCancelReason.Items.FindByValue(_zjs_ApplyCancelMDL.CancelReason);
                    if (CancelReason != null) { CancelReason.Selected = true; }

                    if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）"
                       || _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
                    {
                        RadioButtonList_enforce.SelectedIndex = 1;
                        if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）")
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
                }
                else//new
                {
                    FormatDDLCancelReason(DateTime.Now);

                    SetButtonEnable(null);
                    //二建人员表
                    zjs_CertificateMDL o = zjs_CertificateDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));

                  

                    if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)//个人登录后
                    {
                        divGR.Visible = true;

                        if (o != null)
                        {
                            ViewState["zjs_CertificateMDL"] = o;
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
                            RadTextBoxPSN_CertificateType.Text = _WorkerOB.CertificateType;
                            RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
                            if (string.IsNullOrEmpty(_WorkerOB.Email) == false) RadTextBoxPSN_Email.Text = _WorkerOB.Email;//邮箱
                            if (string.IsNullOrEmpty(_WorkerOB.Nation) == false)//民族
                            {
                                RadComboBoxItem find = RadComboBoxNation.Items.FindItemByText(_WorkerOB.Nation);
                                if (find != null) find.Selected = true;
                            }
                            if (_WorkerOB.Birthday.HasValue == true)
                            {
                                RadDatePickerBirthday.SelectedDate = _WorkerOB.Birthday;
                                UIHelp.SetReadOnly(RadDatePickerBirthday, true);
                            }
                            RadTextBoxPSN_Sex.Text = _WorkerOB.Sex;

                            //企业信息
                            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(o.ENT_OrganizationsCode);
                            if (_UnitMDL != null)
                            {
                                RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                                UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                                RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                                UIHelp.SetReadOnly(RadTextBoxLinkMan, false);
                                RadTextBoxEND_Addess.Text = _UnitMDL.END_Addess;
                                RadTextBoxENT_City.Text = _UnitMDL.ENT_City;
                                RadTextBoxFR.Text = _UnitMDL.ENT_Corporate;
                                UIHelp.SetReadOnly(RadTextBoxFR, true);
                                RadTextBoxENT_Economic_Nature.Text = _UnitMDL.ENT_Economic_Nature;
                                UIHelp.SetReadOnly(RadTextBoxENT_Economic_Nature, true);
                               
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
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
                Decide(_zjs_ApplyMDL);
            }  
        }
        //禁用控件
        private void Disabled()
        {
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

        //个人保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 输入验证

            if (RadioButtonUnitEnforceApply.Visible == false && RadioButtonWorkerEnforceApply.Visible == false && RadioButtonListCancelReason.SelectedIndex == -1)
            {
                UIHelp.layerAlert(Page, "请选择注销原因！");
                return;
            }
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
         
            #endregion 输入验证

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
            _zjs_ApplyMDL.ApplyType = EnumManager.ZJSApplyType.注销;
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;

            zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = ViewState["zjs_ApplyCancelMDL"] == null ? new zjs_ApplyCancelMDL() : (zjs_ApplyCancelMDL)ViewState["zjs_ApplyCancelMDL"];//详细表
            UIHelp.GetData(EditTable, _zjs_ApplyCancelMDL);

            if (PersonType == 2)
            {
                _zjs_ApplyCancelMDL.ApplyManType = "注册造价工程师本人";
            }
            if (PersonType == 3)
            {
                _zjs_ApplyCancelMDL.ApplyManType = "聘用企业";
            }
            if (RadioButtonWorkerEnforceApply.Visible == true)
            {
                _zjs_ApplyCancelMDL.CancelReason = "申请强制注销（因企业不配合、企业营业执照注销等原因）";
                _zjs_ApplyMDL.Memo = "申请强制注销";
            }
            else if (RadioButtonUnitEnforceApply.Visible == true)
            {
                _zjs_ApplyCancelMDL.CancelReason = "申请强制注销（因二级造价工程师不配合等原因）";
                _zjs_ApplyMDL.Memo = "申请强制注销";
            }
            else
            {
                _zjs_ApplyCancelMDL.CancelReason = RadioButtonListCancelReason.SelectedValue;
                _zjs_ApplyMDL.Memo = null;
            }

            
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                if (ViewState["zjs_ApplyMDL"] == null)//new
                {
                    _zjs_ApplyMDL.ApplyCode = zjs_ApplyDAL.GetNextApplyCode(tran, "注销");
                    LabelApplyCode.Text = _zjs_ApplyMDL.ApplyCode;
                    _zjs_ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _zjs_ApplyMDL.CJR = UserName;
                    _zjs_ApplyMDL.CJSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyCancelMDL.ApplyID = _zjs_ApplyMDL.ApplyID;

                    //根据人员表的企业ID获取到企业表信息,取到企业通讯地址和邮政编码；
                    UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxENT_OrganizationsCode.Text.Trim());
                    _zjs_ApplyCancelMDL.END_Addess = _UnitMDL.END_Addess;
                    _zjs_ApplyCancelMDL.ENT_Postcode = _UnitMDL.ENT_Postcode;

                    ////所属区县
                    //if (string.IsNullOrEmpty(_zjs_ApplyMDL.ENT_City))
                    //{
                    //    UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                    //    return;
                    //}

                    zjs_ApplyDAL.Insert(tran, _zjs_ApplyMDL);
                    zjs_ApplyCancelDAL.Insert(tran, _zjs_ApplyCancelMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _zjs_ApplyMDL.PSN_RegisterNo, _zjs_ApplyMDL.ApplyID, filetype);

                }
                else//update
                {
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                    zjs_ApplyCancelDAL.Update(tran, _zjs_ApplyCancelMDL);
                }
                tran.Commit();
                ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                ViewState["zjs_ApplyCancelMDL"] = _zjs_ApplyCancelMDL;
                BindFile(ApplyID);
                SetButtonEnable(_zjs_ApplyMDL);
                UIHelp.WriteOperateLog(UserName, UserID, "保存造价工程师注销注册申请", string.Format("姓名：{0}，身份证号：{1},注册专业：{2}。", UserName, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师注销注册申请失败", ex);
            }
        }

        //个人提交单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = (zjs_ApplyCancelMDL)ViewState["zjs_ApplyCancelMDL"];
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

                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1);
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
                        return;
                    }

                    //检查一寸照片数量
                    int faceImgCount = ApplyFileDAL.SelectFaceImgCountByApplyID(_zjs_ApplyMDL.ApplyID);
                    if (faceImgCount > 1)
                    {
                        UIHelp.layerAlert(Page, string.Format("只能上传一张一寸免冠照片，请删除多余照片！", sb), 5, 0);
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
                    if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
                    {
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                    }
                    else
                    {
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
                        _zjs_ApplyMDL.OldUnitCheckResult = null;
                        _zjs_ApplyMDL.OldUnitCheckRemark = null;
                        _zjs_ApplyMDL.OldUnitCheckTime = null;
                    }

   
                    _zjs_ApplyMDL.ApplyTime = DateTime.Now;

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
                BindFile(_zjs_ApplyMDL.ApplyID);//附件
                if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师注销注册申请提交单位确认", string.Format("姓名：{0}，身份证号：{1},注册专业：{2}。", UserName, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text));

                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='zjsApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("applyZJS"), Utility.Cryptography.Encrypt(_zjs_ApplyMDL.ApplyID.ToString()), _zjs_ApplyMDL.ApplyID));

                }
                else if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "二级造价工程师注销注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}。", UserName, RadTextBoxPSN_CertificateNO.Text));
                    if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）")
                    {
                        UIHelp.layerAlert(Page, "提交建委审核成功。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='zjsApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("applyZJS"), Utility.Cryptography.Encrypt(_zjs_ApplyMDL.ApplyID.ToString()), _zjs_ApplyMDL.ApplyID));
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "提交建委审核成功！", 6, 0, "var isfresh=true;");
                    }
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "二级造价工程师注销注册取消申报成功", string.Format("姓名：{0}，身份证号：{1},注册专业：{2}。", UserName, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "取消申报成功！", 6, 0, "var isfresh=true;");

                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "造价工程师注销注册提交单位确认失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //个人删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            try
            {
                zjs_ApplyDAL.Delete(_zjs_ApplyMDL.ApplyID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除造价工程师注销注册申请失败！", ex);
            }
            ViewState.Remove("zjs_ApplyMDL");
            SetButtonEnable(_zjs_ApplyMDL);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师注销注册申请", string.Format("姓名：{0}，身份证号：{1},注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //单位确认
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            try
            {
                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonApply.Text == "取消申报")//单位取消申报
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    _zjs_ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _zjs_ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
                    _zjs_ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                    {
                        TextBoxOldUnitCheckRemark.Visible = true;
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已驳回;
                        _zjs_ApplyMDL.LastBackResult = string.Format("{0}企业驳回申请，驳回说明：{1}",_zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss") ,TextBoxOldUnitCheckRemark.Text.Trim());//现单位审核意见;
                    }
                    else
                    {
                        TextBoxOldUnitCheckRemark.Visible = false;
                        _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                        _zjs_ApplyMDL.GetResult = null;
                        _zjs_ApplyMDL.GetRemark = null;
                        _zjs_ApplyMDL.GetMan = null;
                        _zjs_ApplyMDL.GetDateTime = null;

                        _zjs_ApplyMDL.ExamineDatetime = null;
                        _zjs_ApplyMDL.ExamineResult = null;
                        _zjs_ApplyMDL.ExamineRemark = null;
                        _zjs_ApplyMDL.ExamineMan = null;

                        _zjs_ApplyMDL.ReportDate = null;
                        _zjs_ApplyMDL.ReportMan = null;
                        _zjs_ApplyMDL.ReportCode = null;

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
                BindFile(_zjs_ApplyMDL.ApplyID);

                if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师注销注册申请申报", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, _zjs_ApplyMDL.PSN_RegisteProfession));
                    string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师注销注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, _zjs_ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师注销注册申报失败！", ex);
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
            zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = (zjs_ApplyCancelMDL)ViewState["zjs_ApplyCancelMDL"];

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
                    if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
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
                       || (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）" && IfExistRoleID("2") == true)
                     )
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
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确定";

                    if (IfExistRoleID("2") == true)//企业
                    {
                        TextBoxOldUnitCheckRemark.Text = "提交审核";
                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                        divUnit.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已申报:

                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";

                    //个人
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;    
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";

                    if (IfExistRoleID("2") == true)//企业
                    {
                        ButtonApply.Text = "取消申报";
                        ButtonApply.Enabled = true;
                        if (_zjs_ApplyCancelMDL.CancelReason != "申请强制注销（因企业不配合、企业营业执照注销等原因）" && _zjs_ApplyCancelMDL.CancelReason != "申请强制注销（因二级造价工程师不配合等原因）")
                        {
                            divUnit.Visible = true;
                        }
                    }


                    //市级受理权限
                    if (IfExistRoleID("20") == true)
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
                        if (o.ConfirmResult == "不通过")//只有决定不通过才能后退，如果通过了（修改了证书），需要人工后退。
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = true;
                    if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）" || _zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
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
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
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
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
                zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = (zjs_ApplyCancelMDL)ViewState["zjs_ApplyCancelMDL"];
                if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因企业不配合、企业营业执照注销等原因）")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师注销注册申请表_个人强制.docx");
                }
                else if (_zjs_ApplyCancelMDL.CancelReason == "申请强制注销（因二级造价工程师不配合等原因）")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师注销注册申请表_企业强制.docx");
                }
                else
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师注销注册申请表.docx");
                }
                fileName = string.Format("北京市二级注册建造价工程师注销申请表_{0}", _zjs_ApplyMDL.PSN_Name);

                UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(_zjs_ApplyMDL.ENT_OrganizationsCode);
                //zjs_CertificateMDL _zjs_CertificateMDL = zjs_CertificateDAL.GetObjectByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_zjs_ApplyMDL);
                o.Add(_zjs_ApplyCancelMDL);
                //o.Add(_UnitMDL);
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
                ht["Birthday"] = _zjs_ApplyMDL.Birthday.HasValue == false ? "" : _zjs_ApplyMDL.Birthday.Value.ToString("yyyy-MM-dd");
                //ht["FR"] = _UnitMDL.ENT_Corporate;
                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["RegisterValidity"] = _zjs_ApplyCancelMDL.RegisterValidity == null ? "" : ((DateTime)_zjs_ApplyCancelMDL.RegisterValidity).ToString("yyyy-MM-dd");

                if (_zjs_ApplyCancelMDL.CancelReason == "已与聘用单位解除劳动关系的")
                {
                    ht["JiePinChengNuo"] = string.Format("\n　　本人已与{0}完成工作交接,已不在其承建的建设工程项目中继续担任施工项目负责人。", _zjs_ApplyMDL.ENT_Name);
                    ht["CancelReason"] = "已与聘用单位解除劳动关系的";
                }
                else
                {
                    ht["JiePinChengNuo"] = "";
                }

                //ht["ENT_Sort"] = string.IsNullOrEmpty(_UnitMDL.ENT_Sort) ? "" : _UnitMDL.ENT_Sort;//企业资质类别
                //ht["ENT_Grade"] = string.IsNullOrEmpty(_UnitMDL.ENT_Grade) ? "" : _UnitMDL.ENT_Grade;//资质等级
                //ht["ENT_QualificationCertificateNo"] = string.IsNullOrEmpty(_UnitMDL.ENT_QualificationCertificateNo) ? "" : _UnitMDL.ENT_QualificationCertificateNo;//资质证书编号

                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印注销申请Word失败！", ex);
            }
        }

        //市级受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;

            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_zjs_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion
            _zjs_ApplyMDL.GetDateTime = DateTime.Now;
            _zjs_ApplyMDL.GetMan = UserName;
            _zjs_ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _zjs_ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ZJSApplyStatus.已受理 : EnumManager.ZJSApplyStatus.已驳回;
            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _zjs_ApplyMDL.LastBackResult = string.Format("{0}市级驳回申请，驳回说明：{1}", _zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理造价工程师注销注册失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "受理造价工程师注销注册", string.Format("姓名：{0}，身份证号：{1}。审批结果：{2}，审批意见：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text
                 , _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark));
            UIHelp.ParentAlert(Page, "受理成功！", true);

            //string js = string.Format("<script>window.parent.location.href='../zjs/zjsBusinessQuery.aspx?id={0}&&type={1}';</script>", _zjs_ApplyMDL.ApplyID, _zjs_ApplyMDL.ApplyType);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
        }

        //市级审核
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //申请表
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;

            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_zjs_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //     UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion

            _zjs_ApplyMDL.ExamineDatetime = DateTime.Now;
            _zjs_ApplyMDL.ExamineMan = UserName;
            _zjs_ApplyMDL.ExamineResult = RadioButtonListExamineResult.SelectedValue;
            _zjs_ApplyMDL.ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus =  EnumManager.ZJSApplyStatus.已审核 ;

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师注销注册市级审核失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师注销注册市级审核", string.Format("姓名：{0}，身份证号：{1}。审核结果：{2}，审核意见：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text
                 , _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "市级审核成功！", true);
        }

        //市级决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
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

            try
            {
                //更新申请单
                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);

                if (RadioButtonListDecide.SelectedValue == "通过")
                {
                    #region 办结更新证书表

                    string sql = "";

                    //证书信息写入历史表
                    sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_RegisterNO ='{0}' ";

                    CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.PSN_RegisterNo));

                    //更新证书信息
                    sql = @"UPDATE [dbo].[zjs_Certificate]
                                SET [zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                ,[zjs_Certificate].[XGR]='{1}'
                                ,[zjs_Certificate].[XGSJ]='{2}'
                                ,[zjs_Certificate].[PSN_RegisteType]='{3}'
                            FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply]
                            on [zjs_Certificate].[PSN_RegisterNO] = [zjs_Apply].[PSN_RegisterNO]                                      
                            where [zjs_Apply].ApplyID='{4}' ";

                    CommonDAL.ExecSQL(tran, string.Format(sql
                        , _zjs_ApplyMDL.ConfirmDate.Value.ToString("yyyy-MM-dd")
                        , UserName
                        , DateTime.Now
                        , EnumManager.ZJSApplyTypeCode.注销
                        , _zjs_ApplyMDL.ApplyID
                        ));

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
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "造价工程师注销注册批准失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师注销注册批准", string.Format("姓名：{0}，身份证号：{1}，注册编号：{2}，注销专业：{3}，批准意见：{4}", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisterNo.Text, RadTextBoxPSN_RegisteProfession.Text, _zjs_ApplyMDL.ConfirmResult));
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
                UIHelp.WriteErrorLog(Page, "后退造价工程师注销审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师注销审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyID"></param>
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
                UIHelp.WriteErrorLog(Page, "删除造价工程师著注销申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师著注销申请表附件", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，附件ID：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession.Text, FileID));
            UIHelp.layerAlert(Page, "删除附件成功！", 0, 2000);
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