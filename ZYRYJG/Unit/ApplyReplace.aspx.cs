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
using Utility;

namespace ZYRYJG.Unit
{
    public partial class ApplyReplace : BasePage
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
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    ApplyMDL _ApplyMDL = ApplyDAL.GetObject(_ApplyID);
                    ViewState["ApplyMDL"] = _ApplyMDL;

                    ApplyReplaceMDL _ApplyReplaceMDL = ApplyReplaceDAL.GetObject(_ApplyID);
                    ViewState["ApplyReplaceMDL"] = _ApplyReplaceMDL;

                    if (IfExistRoleID("0") == true //原来的2改为0  修改人：南静  2019-10-24
                     && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)//2019-10-24  南静添加 待确认,已申报判断条件
                     )
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, false);//基本信息    true改为false，修改人：南静  2010-10-24
                        UIHelp.SetData(EditTable, _ApplyReplaceMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxLinkMan, false);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);

                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        ButtonSave.Enabled = false;
                        divGR.Visible = true;//个人操作按钮 //2019-10-24  南静添加
                    }
                    else
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyReplaceMDL, true);
                    }
                    RadTextBoxPSN_RegisterNO.Text = _ApplyReplaceMDL.RegisterNo;
                    ListItem ReplaceType = RadioButtonListReplaceType.Items.FindByValue(_ApplyReplaceMDL.ReplaceType);
                    if (ReplaceType != null) { ReplaceType.Selected = true; }
                    ListItem ReplaceReason = RadioButtonListReplaceReason.Items.FindByValue(_ApplyReplaceMDL.ReplaceReason);
                    if (ReplaceReason != null) { ReplaceReason.Selected = true; }

                    if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回)//提交后不允许修改
                    {
                        ButtonSave.Enabled = false;
                    }
                    SetButtonEnable(_ApplyMDL.ApplyStatus);
                    //人员照片
                    BindFile(_ApplyMDL.ApplyID);
                    //审批记录
                    BindCheckHistory(_ApplyReplaceMDL.ApplyID);

                    //2019-10-24   南静添加
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


                }
                else//new
                {
                    
                    //二建人员表
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Request["o"]);

                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount > 0)
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
                    if (IfExistRoleID("0") == true)//个人登录后  2019-10-24   南静添加
                    {
                    if (o != null)
                    {
                        divGR.Visible = true;//个人操作按钮 //2019-10-24  南静添加
                        if (o.PSN_CertificateValidity.HasValue == true && o.PSN_CertificateValidity.Value.AddDays(-30) < DateTime.Now)
                        {
                            divGR.Visible = false;//个人操作按钮 //2019-10-24  南静添加
                            UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
                            SetButtonEnable("禁用");

                        }
                        else
                        {
                            SetButtonEnable("");
                        }
                        ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                        UIHelp.SetData(EditTable, o, true);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);

                        //RadTextBoxRegisterValidity.Text = o.PSN_CertificateValidity.HasValue==true?o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd"):"";
                        //UIHelp.SetReadOnly(RadTextBoxRegisterValidity, true);

                        //企业信息
                        UnitMDL _UnitMDL = UnitDAL.GetObject(o.ENT_ServerID);
                        if (_UnitMDL != null)
                        {
                            RadTextBoxENT_Telephone.Text = _UnitMDL.ENT_Telephone;
                            UIHelp.SetReadOnly(RadTextBoxENT_Telephone, true);
                            RadTextBoxLinkMan.Text = _UnitMDL.ENT_Contact;
                            UIHelp.SetReadOnly(RadTextBoxLinkMan, true);

                        }
                        //已注册专业集合
                        DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                            if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBoxPSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");
                        }
                        //执业印章校验码和有效期
                        DataTable mintime = CommonDAL.GetDataTable(string.Format(@"SELECT MIN(PRO_ValidityEnd) FROM [dbo].[COC_TOW_Register_Profession]  WHERE PSN_SERVERID='{0}'",o.PSN_ServerID));
                        RadTextBoxDisnableDate.Text = ((DateTime)mintime.Rows[0][0]).ToString("yyyy-MM-dd");
                        UIHelp.SetReadOnly(RadTextBoxDisnableDate, true);
                        UIHelp.SetReadOnly(RadTextBoxLinkMan, false);
                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        //人员照片
                        BindFile("0");
                    }
                }
                }
                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;
                ////申请操作权限   //南静注释    2019-10-24
                //if (IfExistRoleID("2") == true)
                //{
                //    divQY.Visible = true;

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
            }
        }
          //禁用控件
        private void Disabled()
        {
            RadioButtonListReplaceType.Enabled = false;
            RadioButtonListReplaceReason.Enabled = false;
        }
        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(string ApplyID)
        {
            if (IfExistRoleID("2") == true)
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
            #region 输入验证
            //单选框校验
            if (RadioButtonListReplaceType.SelectedIndex == -1 && RadioButtonListReplaceReason.SelectedIndex == -1)
            {
                UIHelp.layerAlert(Page, "请选择补办原因及原因！");
                return;
            }
            if (Convert.ToDateTime(RadTextBoxDisnableDate.Text).AddDays(-30) < DateTime.Now)
            {
                UIHelp.layerAlert(Page, "证书过期前30天内只能办理注销，不受理其他注册业务!");
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
            #endregion 输入验证

            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表
            UIHelp.GetData(EditTable, _ApplyMDL);
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            _ApplyMDL.Valid = 1;
            _ApplyMDL.ApplyType = EnumManager.ApplyType.遗失补办;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
            ApplyReplaceMDL _ApplyReplaceMDL = ViewState["ApplyReplaceMDL"] == null ? new ApplyReplaceMDL() : (ApplyReplaceMDL)ViewState["ApplyReplaceMDL"];//详细表
            #region 用户修改
            UIHelp.GetData(EditTable, _ApplyReplaceMDL);
            _ApplyReplaceMDL.ReplaceType = RadioButtonListReplaceType.SelectedValue;
            _ApplyReplaceMDL.ReplaceReason = RadioButtonListReplaceReason.SelectedValue;
            #endregion
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    //申请类型编号
                    string type = RadioButtonListReplaceReason.SelectedValue;
                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, type);
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;

                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                    _ApplyMDL.ENT_ServerID = person.ENT_ServerID;
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;
                    //申请专业
                    _ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;
                    _ApplyReplaceMDL.ApplyID = _ApplyMDL.ApplyID;
                    //遗失补办申请表信息
                    _ApplyReplaceMDL.RegisterNo = RadTextBoxPSN_RegisterNO.Text;
                    _ApplyReplaceMDL.RegisterCertificateNo = RadTextBoxPSN_RegisterCertificateNo.Text;
                    //根据人员表的企业ID获取到企业表信息,取到企业通讯地址和邮政编码；
                    UnitMDL _UnitMDL = UnitDAL.GetObject(person.ENT_ServerID);
                    _ApplyReplaceMDL.ENT_Correspondence = _UnitMDL.ENT_Correspondence;
                    _ApplyReplaceMDL.ENT_Postcode = _UnitMDL.ENT_Postcode;

                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyReplaceDAL.Insert(tran, _ApplyReplaceMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    filetype.Add(EnumManager.FileDataTypeName.执业资格证书扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
                    trFuJanTitel.Visible = true;
                    trFuJan.Visible = true;
                }
                else//update
                {
                    ApplyDAL.Update(tran, _ApplyMDL);
                    ApplyReplaceDAL.Update(tran, _ApplyReplaceMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyReplaceMDL"] = _ApplyReplaceMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                BindFile(_ApplyMDL.ApplyID);
                UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请保存成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存遗失补办申请表信息失败", ex);
            }
        }


        //南静添加   2019-10-24  提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            try
            {


                //if (ButtonApply.Text != "撤销申报") 南静注释  2019-10-24
                if (ButtonUnit.Text != "取消申报")
                {
                    //必须上传附件集合
                    System.Collections.Hashtable fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.一寸免冠照片,0},
                {EnumManager.FileDataTypeName.证件扫描件,0},
                {EnumManager.FileDataTypeName.学历证书扫描件,0},
                {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                {EnumManager.FileDataTypeName.劳动合同扫描件,0},
                {EnumManager.FileDataTypeName.申请表扫描件,0}
               
                // ,{EnumManager.FileDataTypeName.遗失声明扫描件,0}
            };

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
                // if (ButtonApply.Text == "撤销申报")//南静注释 2019-10-24   判断按钮不对
                if (ButtonUnit.Text == "取消申报")
                {
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                    _ApplyMDL.ApplyTime = null;
                    //南静  2019-10-24  添加
                    _ApplyMDL.OldUnitCheckResult = null;
                    _ApplyMDL.OldUnitCheckRemark = null;
                    _ApplyMDL.OldUnitCheckTime = null;
                }
                else
                {
                    //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
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
                //人员照片
                BindFile(_ApplyMDL.ApplyID);
                if (_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请申报成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
                    //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
                    //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 3000);

                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "遗失(污损)补办提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }




        //申报 or 撤销申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            //2019-10-24   南静注释
            //if (ButtonApply.Text != "撤销申报")
            //{
            //    //必须上传附件集合
            //    System.Collections.Hashtable fj = new System.Collections.Hashtable{
            //    {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //    {EnumManager.FileDataTypeName.证件扫描件,0},
            //    {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //    {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //    {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //    {EnumManager.FileDataTypeName.申请表扫描件,0}
               
            //    // ,{EnumManager.FileDataTypeName.遗失声明扫描件,0}
            //};

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
            try{
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            //if (ButtonApply.Text == "撤销申报") //南静  2019-10-24   注释
             if (ButtonApply.Text == "取消申报")
            {
                //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;//南静注释 2019-10-24
                //_ApplyMDL.ApplyTime = null;//南静注释 2019-10-24

                //南静  2019-10-24  添加
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见

            }
            else
            {
                //南静  2019-10-24  添加
                _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
                _ApplyMDL.OldUnitCheckRemark = TextBoxOldUnitCheckRemark.Text.Trim();//现单位审核意见

                if (RadioButtonListOldUnitCheckResult.SelectedValue == "不同意")//不同意时写现单位意见
                {
                    TextBoxOldUnitCheckRemark.Visible = true;
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                }
                else
                {
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
            BindFile( _ApplyMDL.ApplyID);
            if (_ApplyMDL.ApplyStatus == "已申报")
            {
                UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请申报成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
                //UIHelp.layerAlert(Page, "申报成功！", 6, 3000);
                //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
            else
            {
                UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
                UIHelp.layerAlert(Page, "退回个人成功！", 6, 3000);
                
            }
              }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "遗失(污损)补办申请失败！", ex);
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
            UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办申请删除成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace(" green", "");//南静添加  2019-10-24
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
                //2019-10-24     南静添加
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
                     step_已审查.Attributes["class"] += " green";
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
                    //2019-10-24   南静注释
                    //ButtonApply.Enabled = false;
                    //ButtonApply.Text = "申 报"; 
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    //2019-10-24   南静注释
                    //ButtonApply.Enabled = true;
                    //ButtonApply.Text = "提交单位确认";
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    break;
                //2019-10-24   南静添加   待确认
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
                    //2019-10-24   南静修改
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
                    //2019-10-24   南静修改
                    ButtonSave.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    //企业
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确定";
                    break;
                case "禁用":
                    ButtonSave.Enabled = false;
                    ButtonUnit.Enabled = false;
                    ButtonUnit.Text = "提交单位确认";
                    //2019-10-24   南静注释
                    //ButtonApply.Enabled = false;
                    //ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
            }
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";//2019-10-24   南静添加
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
                if (_ApplyMDL.PSN_Level == "二级")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/遗失补办.docx");
                    fileName = string.Format("遗失补办或污损更换申请表_{0}", RadTextBoxPSN_Name.Text);
                }
                if (_ApplyMDL.PSN_Level == "二级临时")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师临时执业证书遗失补办或污损更换申请表.docx");
                    fileName = string.Format("北京市二级建造师临时执业证书遗失补办或污损更换申请表_{0}", RadTextBoxPSN_Name.Text);
                    
                }
                //string sourceFile = HttpContext.Current.Server.MapPath("~/Template/遗失补办.docx");
                //string fileName = string.Format("遗失补办或污损更换申请表_{0}", RadTextBoxPSN_Name.Text);
                //ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;

                ApplyReplaceMDL _ApplyReplaceMDL = ViewState["ApplyReplaceMDL"] as ApplyReplaceMDL;
                UnitMDL _UnitMDL = UnitDAL.GetObject(_ApplyMDL.ENT_ServerID);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyReplaceMDL);
                o.Add(_UnitMDL);
                var ht = PrintDocument.GetProperties(o);

                if (_FileInfoMDL != null)
                {
                    ht["photo"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else { ht["photo"] = ""; }
                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["DisnableDate"] = _ApplyReplaceMDL.DisnableDate == null ? "" : ((DateTime)_ApplyReplaceMDL.DisnableDate).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity1"] = _ApplyReplaceMDL.PSN_CertificateValidity1 == null ? "" : ((DateTime)_ApplyReplaceMDL.PSN_CertificateValidity1).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity2"] = _ApplyReplaceMDL.PSN_CertificateValidity2 == null ? "" : ((DateTime)_ApplyReplaceMDL.PSN_CertificateValidity2).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity3"] = _ApplyReplaceMDL.PSN_CertificateValidity3 == null ? "" : ((DateTime)_ApplyReplaceMDL.PSN_CertificateValidity3).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity4"] = _ApplyReplaceMDL.PSN_CertificateValidity4 == null ? "" : ((DateTime)_ApplyReplaceMDL.PSN_CertificateValidity4).ToString("yyyy-MM-dd");
                //证件类型
                //string ZjType = ht["PSN_CertificateType"].ToString();
                //ht["SFZ"] = ZjType == "身份证" ? "☑" : "□";
                //ht["JGZ"] = ZjType == "军官证" ? "☑" : "□";
                //ht["JG"] = ZjType == "警官证" ? "☑" : "□";
                //ht["HZ"] = ZjType == "护照" ? "☑" : "□";
                ////补办更换原因
                //string BbType = ht["ReplaceReason"].ToString();
                //ht["YS"] = BbType == "污损" ? "☑" : "□";
                //ht["WS"] = BbType == "遗失" ? "☑" : "□";
                ////申请补办和更换内容
                //string SqType = ht["ReplaceType"].ToString();
                //ht["ZY"] = SqType == "执业印章" ? "☑" : "□";
                //ht["ZC"] = SqType == "注册证书" ? "☑" : "□";
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
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印遗失污损补办Word失败！", ex);
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
            _ApplyMDL.GetDateTime = DateTime.Now;
            _ApplyMDL.GetMan = UserName;
            _ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办区县受理成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
            //UIHelp.ParentAlert(Page, "受理成功！", true);
            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
           // Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
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

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办区县审查成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
            UIHelp.ParentAlert(Page, "区县审查成功！", true);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            #region 查询证书是否锁定
            bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
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
            UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办建委审核成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //建委领导决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            #region 查询证书是否锁定
            bool IfLock = DataAccess.LockJZSDAL.GetLockStatus(_ApplyMDL.PSN_ServerID);
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
                return;
            }
            #endregion
            _ApplyMDL.ConfirmDate = DateTime.Now;
            _ApplyMDL.ConfirmMan = UserName;
            _ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已公告 ;
            //给个公告时间
            _ApplyMDL.NoticeDate = DateTime.Now;

            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "决定失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "遗失(污损)补办建委决定成功", string.Format("姓名：{0}，身份证号：{1}，增项专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, RadTextBoxPSN_RegisteProfession1.Text + ',' + RadTextBoxPSN_RegisteProfession2.Text + ',' + RadTextBoxPSN_RegisteProfession3.Text));
            UIHelp.ParentAlert(Page, "决定成功！", true);
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
            UIHelp.WriteOperateLog(UserName, UserID, "遗失补办申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }
    }
}