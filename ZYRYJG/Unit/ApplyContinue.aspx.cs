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

namespace ZYRYJG.Unit
{
    public partial class ApplyContinue : BasePage
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
            if (!this.IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    ApplyMDL _ApplyMDL = ApplyDAL.GetObject(_ApplyID);
                    ViewState["ApplyMDL"] = _ApplyMDL;

                    ApplyContinueMDL _ApplyContinueMDL = ApplyContinueDAL.GetObject(_ApplyID);
                    ViewState["ApplyContinueMDL"] = _ApplyContinueMDL;

                    //二建人员表
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;

                    if (string.IsNullOrEmpty(_ApplyMDL.LastBackResult) == false && _ApplyMDL.ApplyStatus != EnumManager.ApplyStatus.已驳回)
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("<apan style='color:red'>【上次退回记录】{0}</span>", _ApplyMDL.LastBackResult);
                    }
                    ApplyMDL lastApply = ApplyDAL.GetLastApplyObject(_ApplyMDL.ApplyID);
                    bool ifApplyAgain=false;//是否一年内再次申请相同注册类型
                    if (lastApply != null
                        && _ApplyMDL.ApplyTime.HasValue==true
                        && lastApply.ApplyType == _ApplyMDL.ApplyType
                        && lastApply.NoticeDate > _ApplyMDL.ApplyTime.Value.AddYears(-1)
                        && lastApply.ConfirmResult == "不通过"
                    )
                    {
                        ifApplyAgain = true;
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("{0}<p style='color:red'>该申请人已于一年内第二次提交相同注册申请，请核对资料后如有不通过情形请及时与申请人电话沟通。</p>", RadGridCheckHistory.MasterTableView.Caption);
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


                    //if (IfExistRoleID("2") == true
                    if (IfExistRoleID("0") == true //原来的2改为0  修改人：南静  2019-10-21
                     && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)//2019-10-21  南静添加 待确认,已申报判断条件
                     )
                    {
                        UIHelp.SetData(EditTable, _ApplyMDL, true);//基本信息    修改人：南静  2010-10-21
                        UIHelp.SetData(EditTable, _ApplyContinueMDL, true);

                        UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                        //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        //LabelPSN_RegisteProfession1.Text = _ApplyContinueMDL.PSN_RegisteProfession1;
                        //RadDatePickerENT_ContractStartTime.Enabled = true;
                        //RadDatePickerENT_ContractENDTime.Enabled = true;
                        //ValidatorENT_ContractStartTime.Enabled = true;
                        //ValidatorENT_ContractENDTime.Enabled = true;
                        //trFuJanTitel.Visible = true;
                        //trFuJan.Visible = true;
                        divGR.Visible = true;//个人操作按钮 //2019-10-21  南静添加
                        
                    }
                    else
                    {

                        UIHelp.SetData(EditTable, _ApplyMDL, true);
                        UIHelp.SetData(EditTable, _ApplyContinueMDL, true);
                        //RadDatePickerENT_ContractStartTime.Enabled = false;
                        //RadDatePickerENT_ContractENDTime.Enabled = false;
                        //ValidatorENT_ContractStartTime.Enabled = false;
                        //ValidatorENT_ContractENDTime.Enabled = false;
                        //RadioButtonListENT_ContractType.Enabled = false;

                        if (IfExistRoleID("0") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已公示)
                        {
                            divGR.Visible = true;//个人操作按钮
                        }
                    }

                    //延期专业详细，根据面板上已注册专业来给值
                    if (_ApplyContinueMDL.PSN_RegisteProfession1 != null) LabelPSN_RegisteProfession1.Text = _ApplyContinueMDL.PSN_RegisteProfession1;
                    if (_ApplyContinueMDL.PSN_RegisteProfession2 != null) LabelPSN_RegisteProfession2.Text = _ApplyContinueMDL.PSN_RegisteProfession2;
                    if (_ApplyContinueMDL.PSN_RegisteProfession3 != null) LabelPSN_RegisteProfession3.Text = _ApplyContinueMDL.PSN_RegisteProfession3;
                    if (_ApplyContinueMDL.PSN_RegisteProfession4 != null) LabelPSN_RegisteProfession4.Text = _ApplyContinueMDL.PSN_RegisteProfession4;

                    #region 禁用控件
                    //已注册专业集合
                    DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(_ApplyMDL.PSN_ServerID);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBox_PSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                        if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBox_PSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");

                    }
                    //根据已注册的专业给他延续专业，有几个专业给他几个延续的行数
                    //for (int i = 1; i < 4; i++)
                    //{
                        if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession1) != false)
                        {
                            RadComboBoxIfContinue1.Enabled = false;
                            RadTextBoxRemark1.Enabled = false;
                        }
                        if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession2) != false)
                        {
                            RadComboBoxIfContinue2.Enabled = false;
                            RadTextBoxRemark2.Enabled = false;
                        }
                        if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession3) != false)
                        {
                            RadComboBoxIfContinue3.Enabled = false;
                            RadTextBoxRemark3.Enabled = false;
                        }
                        if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession4) != false)
                        {
                            RadComboBoxIfContinue4.Enabled = false;
                            RadTextBoxRemark4.Enabled = false;
                        }

                    //}
                    #endregion


                    SetButtonEnable(_ApplyMDL.ApplyStatus);
                    //附件
                    BindFile(_ApplyMDL.ApplyID);
                    //审批记录
                    BindCheckHistory(_ApplyContinueMDL.ApplyID);

                    //if (IfExistRoleID("2") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报)
                    if (IfExistRoleID("0") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报)//将原来的2改成0   南静  2019-10-21
                    {
                        string _ProfessionList = "";
                        //未防止二级临时已注销、初始注册二建时出现俩个专业信息过滤到已注销的
                        string sql = string.Format("select ProfessionList from [dbo].[View_JZS_TOW_Applying] where PSN_CertificateNO='{0}'and PSN_RegisteType!='07'", _ApplyMDL.PSN_CertificateNO);
                        DataTable dt2 = CommonDAL.GetDataTable(sql);
                        if (dt2.Rows.Count > 0)
                        {
                            _ProfessionList = dt2.Rows[0]["ProfessionList"].ToString();
                        }
                        if (CheckContinueTimeSpan(_ProfessionList) == false)
                        {
                            ButtonApply.Enabled = false;
                            ButtonUnit.Enabled = false;//南静添加  2019-10-22
                            ButtonSave.Enabled = false;
                            UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                        }
                        else if (ifApplyAgain == true)
                        {
                            UIHelp.layerAlert(Page, string.Format("提示您，上次注册申请审核未通过的原因是：【{0}】请您在本次申请提交时，注意核对填报内容是否按照要求修改完成。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                        }

                    }

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

                    if (IfExistRoleID("0") == false && IfExistRoleID("2") == false && ifApplyAgain == true && _ApplyMDL.NoticeDate.HasValue==false)
                    {
                        UIHelp.layerAlert(Page, string.Format("提示审核人员：该申请人一年内第二次提交注册申请，上次不通过的原因是：【{0}】请注意核对，如存在问题请及时驳回申请。", lastApply.CheckResult == "不通过" ? lastApply.CheckRemark : lastApply.ConfirmRemark));
                    }



                    //if (IfExistRoleID("2") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报)
                    //{
                    //    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                    //    if (o.PSN_CertificateValidity.Value < DateTime.Now.AddDays(EnumManager.ContinueTime.结束时间) || o.PSN_CertificateValidity.Value > DateTime.Now.AddDays(EnumManager.ContinueTime.开始时间))//延期注册，有效期截止时间前90天开放申请，截止前30天关闭申请通道。
                    //    {
                    //        ButtonApply.Enabled = false;
                    //        ButtonSave.Enabled = false;
                    //        UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                    //    }
                    //}





                }
                else//new
                {

                    SetButtonEnable("");
                    //二建人员表
                    COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
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

                    //#region 查询证书是否打印
                    //int sum = DataAccess.COC_TOW_Person_BaseInfoDAL.PrintIsorNot(o.PSN_CertificateNO);
                    //if (sum > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "上次业务办理证书尚未打印，请先打印证书！", 5, 0);
                    //    return;
                    //}
                    //#endregion
                    if (IfExistRoleID("0") == true)//个人登录后  2019-10-21   南静添加
                    {
                        if (o != null)
                        {
                            
                            UIHelp.SetData(EditTable, o, true);
                            //RadioButtonListENT_ContractType.SelectedIndex = 0;

                            UIHelp.SetReadOnly(RadTextBoxENT_Telephone, false);
                            //UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, false);
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);

                            //考生信息
                            WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
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
                                UIHelp.SetReadOnly(RadTextBoxENT_Telephone, true);
                                //已注册专业集合
                                DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
                                int j = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("RadTextBox_PSN_RegisteProfession" + (i + 1).ToString()) as RadTextBox).Text = dt.Rows[i]["PRO_Profession"].ToString();
                                    if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) (EditTable.FindControl("RadTextBox_PSN_CertificateValidity" + (i + 1).ToString()) as RadTextBox).Text = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).ToString("yyyy-MM-dd");

                                    //专业不在续期允许时间段，不允许申请续期
                                    if (
                                     Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]).AddDays(1) >= DateTime.Now.AddDays(Model.EnumManager.ContinueTime.结束时间)
                                     && Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]) <= DateTime.Now.AddDays(Model.EnumManager.ContinueTime.开始时间)
                                   )
                                    {
                                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) (EditTable.FindControl("LabelPSN_RegisteProfession" + (j + 1)) as Label).Text = dt.Rows[i]["PRO_Profession"].ToString();
                                        j++;
                                        continue;

                                    }
                                }

                                //根据已注册的专业给他延续专业，有几个专业给他几个延续的行数
                                for (int i = j; i < 4; i++)
                                {
                                    (EditTable.FindControl("RadComboBoxIfContinue" + (i + 1)) as RadComboBox).Enabled = false;
                                    (EditTable.FindControl("RadTextBoxRemark" + (i + 1)) as RadTextBox).Enabled = false;
                                }
                            }


                            //附件
                            BindFile("0");

                            #region 继续教育信息
                            //                        if (RadTextBoxPSN_RegisteProfession1.Text != "")
                            //                        {
                            //                            DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT top(1)* FROM jcsjk_ejjxjy WHERE WorkerCertificateCode='{0}' 
                            //                                                                          AND  postName like '%{1}%' ORDER BY qfrq desc", RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession1.Text));
                            //                            if (dt.Rows.Count > 0)
                            //                            {
                            //                                //给他的签发日期加上一年必须大于系统当前时间，否则就是去年没有继续教育
                            //                                if (Convert.ToDateTime(dt.Rows[0]["qfrq"]).AddYears(1) > DateTime.Now)
                            //                                {
                            //                                    LabelBiXiu1.Text = dt.Rows[0]["bxxs"].ToString() == "" ? "0" : dt.Rows[0]["bxxs"].ToString();
                            //                                    LabelXuanXiu1.Text = dt.Rows[0]["xxxs"].ToString() == "" ? "0" : dt.Rows[0]["xxxs"].ToString(); ;
                            //                                }

                            //                            }

                            //                        }
                            //                        if (RadTextBoxPSN_RegisteProfession2.Text != "")
                            //                        {
                            //                            DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT top(1)* FROM jcsjk_ejjxjy WHERE WorkerCertificateCode='{0}' 
                            //                                                                          AND  postName like '%{1}%' ORDER BY qfrq desc", RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession2.Text));
                            //                            if (dt.Rows.Count > 0)
                            //                            {
                            //                                //给他的签发日期加上一年必须大于系统当前时间，否则就是去年没有继续教育
                            //                                if (Convert.ToDateTime(dt.Rows[0]["qfrq"]).AddYears(1) > DateTime.Now)
                            //                                {
                            //                                    LabelBiXiu2.Text = dt.Rows[0]["bxxs"].ToString() == "" ? "0" : dt.Rows[0]["bxxs"].ToString();
                            //                                    LabelXuanXiu2.Text = dt.Rows[0]["xxxs"].ToString() == "" ? "0" : dt.Rows[0]["xxxs"].ToString(); ;
                            //                                }
                            //                            }
                            //                        }
                            //                        if (RadTextBoxPSN_RegisteProfession3.Text != "")
                            //                        {
                            //                            DataTable dt = CommonDAL.GetDataTable(string.Format(@"SELECT top(1)* FROM jcsjk_ejjxjy WHERE WorkerCertificateCode='{0}' 
                            //                                                                          AND  postName like '%{1}%' ORDER BY qfrq desc", RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession3.Text));
                            //                            if (dt.Rows.Count > 0)
                            //                            {
                            //                                //给他的签发日期加上一年必须大于系统当前时间，否则就是去年没有继续教育
                            //                                if (Convert.ToDateTime(dt.Rows[0]["qfrq"]).AddYears(1) > DateTime.Now)
                            //                                {
                            //                                    LabelBiXiu3.Text = dt.Rows[0]["bxxs"].ToString() == "" ? "0" : dt.Rows[0]["bxxs"].ToString();
                            //                                    LabelXuanXiu3.Text = dt.Rows[0]["xxxs"].ToString() == "" ? "0" : dt.Rows[0]["xxxs"].ToString(); ;
                            //                                }
                            //                            }
                            //                        }
                            #endregion
                            //过期后的证书应该避免掉二建临时    南静   2020-04-16
                            string _ProfessionList = "";
                            string sql = string.Format("select ProfessionList from [dbo].[View_JZS_TOW_Applying] where [PSN_RegisterNO]='{0}' and psn_Level <> '二级临时' ", o.PSN_RegisterNO);
                            DataTable dt2 = CommonDAL.GetDataTable(sql);
                            if (dt2.Rows.Count > 0)
                            {
                                _ProfessionList = dt2.Rows[0]["ProfessionList"].ToString();
                            }
                            divGR.Visible = true;//个人操作按钮 //2019-10-21  南静添加
                            if (CheckContinueTimeSpan(_ProfessionList) == false)
                            {
                                ButtonApply.Enabled = false;
                                ButtonUnit.Enabled = false;//南静添加  2019-10-22
                                ButtonSave.Enabled = false;
                                divGR.Visible = false;//个人操作按钮 //2019-10-22  南静添加
                                UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                            }

                            //if (o.PSN_CertificateValidity.Value < DateTime.Now.AddDays(EnumManager.ContinueTime.结束时间) || o.PSN_CertificateValidity.Value > DateTime.Now.AddDays(EnumManager.ContinueTime.开始时间))//延期注册，有效期截止时间前90天开放申请，截止前30天关闭申请通道。
                            //{
                            //    ButtonApply.Enabled = false;
                            //    ButtonSave.Enabled = false;
                            //    UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                            //}
                        }
                    }

                }
                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;
                //申请操作权限
                if (IfExistRoleID("2") == true)
                {
                    //divQY.Visible = true;   //2019-10-21   南静注释

                    //企业看不到各级申办人列
                    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    //divCheckHistory.Visible = false;     
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

        private bool CheckContinueTimeSpan(string _ProfessionList)
        {
            string[] zy = _ProfessionList.Split(',');
            DateTime ValidDate;
            foreach (var item in zy)
            {
                ValidDate = Convert.ToDateTime(item.Split(':')[1]);
                if (DateTime.Now >= ValidDate.AddDays(-Model.EnumManager.ContinueTime.开始时间)
                      && DateTime.Now <= ValidDate.AddDays(-Model.EnumManager.ContinueTime.结束时间 +1)
                  )
                {
                    return true;
                }

            }
            return false;
        }

        private void Disabled()
        {
            //把三个都禁用掉
            for (int i = 0; i < 4; i++)
            {

                (EditTable.FindControl("RadComboBoxIfContinue" + (i + 1)) as RadComboBox).Enabled = false;
                //(EditTable.FindControl("RadNumericTextBoxBiXiu" + (i + 1)) as RadNumericTextBox).Enabled = false;
                //(EditTable.FindControl("RadNumericTextBoxXuanXiu" + (i + 1)) as RadNumericTextBox).Enabled = false;
                (EditTable.FindControl("RadTextBoxRemark" + (i + 1)) as RadTextBox).Enabled = false;
            }
            //UIHelp.SetReadOnly(RadTextBoxMainJob, true);
            //UIHelp.SetReadOnly(RadTextBoxOtherCert, true);
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

            //下拉列表选项校验
            if (LabelPSN_RegisteProfession1.Text != "")
            {
                if (RadComboBoxIfContinue1.Text == "请选择")
                {
                    UIHelp.layerAlert(Page, "请在下拉列表中更改请选择选项！");
                    return;
                }
                if (RadComboBoxIfContinue1.Text == "是")
                {
                    LabelBiXiu1.Text = "60";
                    LabelXuanXiu1.Text = "60";

                }
            }
            if (LabelPSN_RegisteProfession2.Text != "")
            {
                if (RadComboBoxIfContinue2.Text == "请选择")
                {
                    UIHelp.layerAlert(Page, "请在下拉列表中更改请选择选项！");
                    return;
                }
                if (RadComboBoxIfContinue2.Text == "是")
                {
                    LabelBiXiu2.Text = "60";
                    LabelXuanXiu2.Text = "60";
                }
            }
            if (LabelPSN_RegisteProfession3.Text != "")
            {
                if (RadComboBoxIfContinue3.Text == "请选择")
                {
                    UIHelp.layerAlert(Page, "请在下拉列表中更改请选择选项！");
                    return;
                }
                if (RadComboBoxIfContinue3.Text == "是")
                {
                    LabelBiXiu3.Text = "60";
                    LabelXuanXiu3.Text = "60";
                }
            }
            if (LabelPSN_RegisteProfession4.Text != "")
            {
                if (RadComboBoxIfContinue4.Text == "请选择")
                {
                    UIHelp.layerAlert(Page, "请在下拉列表中更改请选择选项！");
                    return;
                }
                if (RadComboBoxIfContinue4.Text == "是")
                {
                    LabelBiXiu3.Text = "60";
                    LabelXuanXiu3.Text = "60";
                }
            }

            if (RadComboBoxIfContinue1.Text != "是"
                && RadComboBoxIfContinue2.Text != "是"
                && RadComboBoxIfContinue3.Text != "是"
                && RadComboBoxIfContinue4.Text != "是"
                )
            {
                UIHelp.layerAlert(Page, "至少选择一个专业进行续期！");
                return;
            }

            if (Utility.Check.CheckBirthdayLimit(0, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满65周岁前90天起,不再允许发起二级建造师初始、重新、延续、增项、执业企业变更注册申请。", 5, 0);
                return;
            }

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
                if (ApplyDAL.SelectCount(string.Format(" and PSN_RegisterNO='{0}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_RegisterNo.Text.Trim(), EnumManager.ApplyStatus.未申报, EnumManager.ApplyStatus.待确认)) > 0)
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
            _ApplyMDL.ApplyType = EnumManager.ApplyType.延期注册;
            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;

            ApplyContinueMDL _ApplyContinueMDL = ViewState["ApplyContinueMDL"] == null ? new ApplyContinueMDL() : (ApplyContinueMDL)ViewState["ApplyContinueMDL"];//详细表

            //用户要填信息
            UIHelp.GetData(EditTable, _ApplyContinueMDL);
            ////劳动合同类型
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

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    _ApplyMDL.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "延期注册");
                    LabelApplyCode.Text = _ApplyMDL.ApplyCode;
                    _ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _ApplyMDL.CJR = UserName;
                    _ApplyMDL.CJSJ = _ApplyMDL.XGSJ;

                    COC_TOW_Person_BaseInfoMDL person = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];//人员表
                    _ApplyMDL.ENT_ServerID = person.ENT_ServerID;
                    _ApplyMDL.PSN_ServerID = person.PSN_ServerID;

                    //申请专业
                    //_ApplyMDL.PSN_RegisteProfession = person.PSN_RegisteProfession;
                    System.Text.StringBuilder RegisteProfession = new System.Text.StringBuilder();

                    #region 申请专业格式化

                    if (LabelPSN_RegisteProfession1.Text != "" && RadComboBoxIfContinue1.SelectedValue == "True")
                    {
                        RegisteProfession.Append(",").Append(LabelPSN_RegisteProfession1.Text);
                        if (LabelPSN_RegisteProfession1.Text == RadTextBox_PSN_RegisteProfession1.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity1 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity1.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession1.Text == RadTextBox_PSN_RegisteProfession2.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity1 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity2.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession1.Text == RadTextBox_PSN_RegisteProfession3.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity1 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity3.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession1.Text == RadTextBox_PSN_RegisteProfession4.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity1 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity4.Text.ToString());
                        }
                    }
                    if (LabelPSN_RegisteProfession2.Text != "" && RadComboBoxIfContinue2.SelectedValue == "True")
                    {
                        RegisteProfession.Append(",").Append(LabelPSN_RegisteProfession2.Text);

                        if (LabelPSN_RegisteProfession2.Text == RadTextBox_PSN_RegisteProfession1.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity2 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity1.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession2.Text == RadTextBox_PSN_RegisteProfession2.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity2 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity2.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession2.Text == RadTextBox_PSN_RegisteProfession3.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity2 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity3.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession2.Text == RadTextBox_PSN_RegisteProfession4.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity2 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity4.Text.ToString());
                        }

                    }
                    if (LabelPSN_RegisteProfession3.Text != "" && RadComboBoxIfContinue3.SelectedValue == "True")
                    {
                        RegisteProfession.Append(",").Append(LabelPSN_RegisteProfession3.Text);

                        if (LabelPSN_RegisteProfession3.Text == RadTextBox_PSN_RegisteProfession1.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity3 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity1.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession3.Text == RadTextBox_PSN_RegisteProfession2.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity3 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity2.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession3.Text == RadTextBox_PSN_RegisteProfession3.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity3 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity3.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession3.Text == RadTextBox_PSN_RegisteProfession4.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity3 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity4.Text.ToString());
                        }
                    }
                    if (LabelPSN_RegisteProfession4.Text != "" && RadComboBoxIfContinue4.SelectedValue == "True")
                    {
                        RegisteProfession.Append(",").Append(LabelPSN_RegisteProfession4.Text);

                        if (LabelPSN_RegisteProfession4.Text == RadTextBox_PSN_RegisteProfession1.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity4 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity1.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession4.Text == RadTextBox_PSN_RegisteProfession2.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity4 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity2.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession4.Text == RadTextBox_PSN_RegisteProfession3.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity4 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity3.Text.ToString());
                        }
                        if (LabelPSN_RegisteProfession4.Text == RadTextBox_PSN_RegisteProfession4.Text)
                        {
                            _ApplyContinueMDL.PSN_CertificateValidity4 = Convert.ToDateTime(RadTextBox_PSN_CertificateValidity4.Text.ToString());
                        }
                    }

                    #endregion 申请专业格式化

                    if (RegisteProfession.Length > 0)
                    {
                        RegisteProfession.Remove(0, 1);
                    }

                    _ApplyMDL.PSN_RegisteProfession = RegisteProfession.ToString();
                    //根据人员表的企业ID获取到企业表信息
                    UnitMDL _UnitMDL = UnitDAL.GetObject(person.ENT_ServerID);
                    //延续注册表信息
                    _ApplyContinueMDL.ApplyID = _ApplyMDL.ApplyID;
                    _ApplyContinueMDL.LinkMan = _UnitMDL.ENT_Contact;
                    _ApplyContinueMDL.ENT_MobilePhone = _UnitMDL.ENT_MobilePhone;
                    _ApplyContinueMDL.ENT_Correspondence = _UnitMDL.ENT_Correspondence;
                    _ApplyContinueMDL.ENT_Economic_Nature = _UnitMDL.ENT_Economic_Nature;
                    _ApplyContinueMDL.END_Addess = _UnitMDL.END_Addess;
                    _ApplyContinueMDL.ENT_Postcode = _UnitMDL.ENT_Postcode;


                    //所属区县
                    if (string.IsNullOrEmpty(_ApplyMDL.ENT_City))
                    {
                        UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                        return;
                    }

                    ApplyDAL.Insert(tran, _ApplyMDL);
                    ApplyContinueDAL.Insert(tran, _ApplyContinueMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.执业资格证书扫描件);
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _ApplyMDL.PSN_RegisterNo, _ApplyMDL.ApplyID, filetype);

                    //trFuJanTitel.Visible = true;
                    //trFuJan.Visible = true;
                }
                else//update
                {
                    ApplyDAL.Update(tran, _ApplyMDL);
                    _ApplyContinueMDL.PSN_MobilePhone = RadTextBoxPSN_MobilePhone.Text.Trim();
                    ApplyContinueDAL.Update(tran, _ApplyContinueMDL);
                }
                tran.Commit();
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyContinueMDL"] = _ApplyContinueMDL;
                BindFile(ApplyID);
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}，延期注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "isfresh", "alert('延期注册成功！');hideIfam();parent.refreshGrid()", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存延续注册申请表信息失败", ex);
            }
        }

        //南静添加   2019-10-21   提交到单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            ApplyContinueMDL _ApplyContinueMDL  = (ApplyContinueMDL)ViewState["ApplyContinueMDL"];


            //ApplyRenewMDL _ApplyRenewMDL = (ApplyRenewMDL)ViewState["ApplyRenewMDL"];
            try
            {
                //if (ButtonApply.Text != "取消申报")//南静注释 2019-10-18   判断按钮不对
                if (ButtonUnit.Text != "取消申报")
                {
                    //bool ifNeedJXJY = false;//是否需要上传继续教育
                    //必须上传附件集合
                    System.Collections.Hashtable fj;


                    //2021-04-23   南静修改    延续注册必须上传继续教育承诺书扫描件
                    //必须上传附件集合
                    fj = new System.Collections.Hashtable{
                    {EnumManager.FileDataTypeName.一寸免冠照片,0},
                    {EnumManager.FileDataTypeName.证件扫描件,0},
                    //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                    //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                    //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    {EnumManager.FileDataTypeName.继续教育承诺书扫描件,0},
                    {EnumManager.FileDataTypeName.申请表扫描件,0}};


                    //校验继续教育情况，是否满足最近三年继续教育情况
                    //if (ifNeedJXJY == true)
                    //{

                    //        //必须上传附件集合
                    //        fj = new System.Collections.Hashtable{
                    //{EnumManager.FileDataTypeName.一寸免冠照片,0},
                    //{EnumManager.FileDataTypeName.证件扫描件,0},
                    //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                    //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                    //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    //{EnumManager.FileDataTypeName.继续教育承诺书扫描件,0},
                    //{EnumManager.FileDataTypeName.申请表扫描件,0}};


                    //}
                    //else
                    //{

                    //        //必须上传附件集合
                    //        fj = new System.Collections.Hashtable{
                    // {EnumManager.FileDataTypeName.一寸免冠照片,0},
                    // {EnumManager.FileDataTypeName.证件扫描件,0},
                    // {EnumManager.FileDataTypeName.学历证书扫描件,0},
                    // {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                    // {EnumManager.FileDataTypeName.劳动合同扫描件,0},
                    // {EnumManager.FileDataTypeName.申请表扫描件,0}};



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

                    //检查要续期专业是否过期
                    if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession1) != false && _ApplyContinueMDL.PSN_CertificateValidity1.HasValue==true && _ApplyContinueMDL.PSN_CertificateValidity1.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, string.Format("{0}专业已经过期，无法再申请续期，请删除申请，重新选择要续期的专业！", _ApplyContinueMDL.PSN_RegisteProfession1), 5, 0);
                        return;
                    }
                    if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession2) != false && _ApplyContinueMDL.PSN_CertificateValidity2.HasValue == true && _ApplyContinueMDL.PSN_CertificateValidity2.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, string.Format("{0}专业已经过期，无法再申请续期，请删除申请，重新选择要续期的专业！", _ApplyContinueMDL.PSN_RegisteProfession2), 5, 0);
                        return;
                    }
                    if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession3) != false && _ApplyContinueMDL.PSN_CertificateValidity3.HasValue == true && _ApplyContinueMDL.PSN_CertificateValidity3.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, string.Format("{0}专业已经过期，无法再申请续期，请删除申请，重新选择要续期的专业！", _ApplyContinueMDL.PSN_RegisteProfession3), 5, 0);
                        return;
                    }
                    if (string.IsNullOrEmpty(_ApplyContinueMDL.PSN_RegisteProfession4) != false && _ApplyContinueMDL.PSN_CertificateValidity4.HasValue == true && _ApplyContinueMDL.PSN_CertificateValidity4.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, string.Format("{0}专业已经过期，无法再申请续期，请删除申请，重新选择要续期的专业！", _ApplyContinueMDL.PSN_RegisteProfession4), 5, 0);
                        return;
                    }
                }

                //_ApplyMDL.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                //_ApplyMDL.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;

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
                }
                ApplyDAL.Update(_ApplyMDL);
                ViewState["ApplyMDL"] = _ApplyMDL;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
                //人员照片
                BindFile(_ApplyMDL.ApplyID);
                if (_ApplyMDL.ApplyStatus == "待确认")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请提交成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID));
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    //RadDatePickerENT_ContractStartTime.Enabled = true;
                    //RadDatePickerENT_ContractENDTime.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "延续注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认，申报 or 撤销申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            //2019-10-22   南静注释
            //ApplyContinueMDL _ApplyContinueMDL =(ApplyContinueMDL)ViewState["ApplyContinueMDL"];

            //if (ButtonApply.Text != "撤销申报")
            //{
            //    //必须上传附件集合
            //    System.Collections.Hashtable fj= new System.Collections.Hashtable{
            //            {EnumManager.FileDataTypeName.一寸免冠照片,0},
            //            {EnumManager.FileDataTypeName.证件扫描件,0},
            //            {EnumManager.FileDataTypeName.学历证书扫描件,0},
            //            {EnumManager.FileDataTypeName.执业资格证书扫描件,0},
            //            {EnumManager.FileDataTypeName.劳动合同扫描件,0},
            //            {EnumManager.FileDataTypeName.继续教育证明扫描件,0},
            //            {EnumManager.FileDataTypeName.申请表扫描件,0}
            //        };

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
                //if (ButtonApply.Text == "撤销申报")  //南静  2019-10-22   注释
                if (ButtonApply.Text == "取消申报")
                {
                    //_ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;//南静注释 2019-10-22
                    //_ApplyMDL.ApplyTime = null;//南静注释 2019-10-22
                    //南静  2019-10-22  添加
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已驳回;
                    _ApplyMDL.OldUnitCheckTime = DateTime.Now;//现单位申请时间
                    _ApplyMDL.OldUnitCheckResult = "不同意";//现单位审核结果
                    _ApplyMDL.OldUnitCheckRemark = "退回个人";//现单位审核意见
                }
                else
                {
                    //南静  2019-10-22  添加
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
                        if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报)
                        {

                            //过期后的证书应该避免掉二建临时    南静   2020-04-16
                            string _ProfessionList = "";
                            string sql = string.Format("select ProfessionList from [dbo].[View_JZS_TOW_Applying] where PSN_CertificateNO='{0}' and psn_Level <> '二级临时' ", _ApplyMDL.PSN_CertificateNO);
                            DataTable dt2 = CommonDAL.GetDataTable(sql);
                            if (dt2.Rows.Count > 0)
                            {
                                _ProfessionList = dt2.Rows[0]["ProfessionList"].ToString();
                            }
                            if (CheckContinueTimeSpan(_ProfessionList) == false)
                            {
                                UIHelp.layerAlert(Page, string.Format("续期申请开放时间为有效期截止前{0}天至{1}天，此证书不在允许时间范围，不允许提交申请！", EnumManager.ContinueTime.开始时间, EnumManager.ContinueTime.结束时间));
                                return;
                            }
                        }

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
                    UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，延续注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
                    //UIHelp.layerAlert(Page, "申报成功！", 6, 2000);
                    //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    //RadDatePickerENT_ContractStartTime.Enabled = true;
                    //RadDatePickerENT_ContractENDTime.Enabled = true;
                    UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，延续注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
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

            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            ApplyDAL.Delete(_ApplyMDL.ApplyID);
            ViewState.Remove("ApplyMDL");
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}，延期注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_待确认.Attributes["class"] = step_待确认.Attributes["class"].Replace(" green", "");//南静添加  2019-10-22
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
                //2019-10-22     南静添加
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
                    //2019-10-22   南静注释
                    //ButtonApply.Enabled = false;
                    //ButtonApply.Text = "申 报"; 
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    ButtonSave.Enabled = true;
                    //2019-10-22   南静注释
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
                //2019-10-22   南静添加   待确认
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
                    //2019-10-22   南静修改
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
                    //2019-10-22   南静修改
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
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";//2019-10-22   南静添加
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
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师延续注册申请表.docx");
                    fileName = "北京市二级注册建造师延续注册申请表";
                }
                if (_ApplyMDL.PSN_Level == "二级临时")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师临时执业证书延续注册申请表.docx");
                    fileName = "北京市二级建造师临时执业证书延续注册申请表";
                }
                //string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师延续注册申请表.docx");
                //string fileName = "北京市二级注册建造师延续注册申请表";

                // ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                ApplyContinueMDL _ApplyContinueMDL = ViewState["ApplyContinueMDL"] as ApplyContinueMDL;
                UnitMDL _UnitMDL = UnitDAL.GetObject(_ApplyMDL.ENT_ServerID);
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyContinueMDL);
                o.Add(_UnitMDL);
                o.Add(_COC_TOW_Person_BaseInfoMDL);
                var ht = PrintDocument.GetProperties(o);

                ht["PSN_BirthDate"] = _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate == null ? "" : ((DateTime)_COC_TOW_Person_BaseInfoMDL.PSN_BirthDate).ToString("yyyy-MM-dd");
                //ht["Nation"] = _COC_TOW_Person_BaseInfoMDL.PSN_National;
                ht["FR"] = _UnitMDL.ENT_Corporate;


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
                ht["isCtable"] = false;
                //对时间类型进行格式转换

                //ht["PSN_CertificateValidity1"]=(EditTable.FindControl("RadTextBox_PSN_CertificateValidity1") as RadTextBox).Text;
                //ht["PSN_CertificateValidity2"] = (EditTable.FindControl("RadTextBox_PSN_CertificateValidity2") as RadTextBox).Text;
                //ht["PSN_CertificateValidity3"] = (EditTable.FindControl("RadTextBox_PSN_CertificateValidity3") as RadTextBox).Text;
                // ht["PSN_CertificateValidity4"] = (EditTable.FindControl("RadTextBox_PSN_CertificateValidity4") as RadTextBox).Text;
                // ht["PSN_CertificateValidity"] = _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity == null ? "" : ((DateTime)_COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity).ToString("yyyy-MM-dd");
                ht["PSN_CertificateValidity1"] = _ApplyContinueMDL.PSN_CertificateValidity1 == null ? "" : ((DateTime)_ApplyContinueMDL.PSN_CertificateValidity1).ToString("yyyy年MM月dd日");
                ht["PSN_CertificateValidity2"] = _ApplyContinueMDL.PSN_CertificateValidity2 == null ? "" : ((DateTime)_ApplyContinueMDL.PSN_CertificateValidity2).ToString("yyyy年MM月dd日");
                ht["PSN_CertificateValidity3"] = _ApplyContinueMDL.PSN_CertificateValidity3 == null ? "" : ((DateTime)_ApplyContinueMDL.PSN_CertificateValidity3).ToString("yyyy年MM月dd日");
                ht["PSN_CertificateValidity4"] = _ApplyContinueMDL.PSN_CertificateValidity4 == null ? "" : ((DateTime)_ApplyContinueMDL.PSN_CertificateValidity4).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractStartTime"] = _ApplyMDL.ENT_ContractStartTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractStartTime).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractENDTime"] = _ApplyMDL.ENT_ContractENDTime == null ? "" : ((DateTime)_ApplyMDL.ENT_ContractENDTime).ToString("yyyy年MM月dd日");
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
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印延续注册申请失败！", ex);
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
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册区县受理", string.Format("姓名：{0}，身份证号：{1}，延期注册专业：{2}。审批结果：{3}，审批意见：{4}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession
                , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
            //UIHelp.ParentAlert(Page, "受理成功！", true);
            string js = string.Format("<script>window.parent.location.href='../County/BusinessQuery.aspx?id={0}&&type={1}';</script>", _ApplyMDL.ApplyID, _ApplyMDL.ApplyType);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);
            //Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id='{0}'&&type={1}", _ApplyMDL.ApplyID,_ApplyMDL.ApplyType), true);
        }

        //区县审查
        protected void BttSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            //移动到汇总上报时校验
            //if (_ApplyMDL.CheckXSL.HasValue == false || _ApplyMDL.CheckXSL.Value == 1)
            //{
            //    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
            //    return;
            //}
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
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册区县审查", string.Format("姓名：{0}，身份证号：{1}，延期注册专业：{2}。审批结果：{3}，审批意见：{4}。"
                , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark));
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
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册建委审核", string.Format("姓名：{0}，身份证号：{1}，延期注册专业：{2}。审批结果：{3}，审批意见：{4}。"
                , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession, _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
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
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册建委决定成功", string.Format("姓名：{0}，身份证号：{1}，延续注册专业：{2}。"
                , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, _ApplyMDL.PSN_RegisteProfession));
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
            UIHelp.WriteOperateLog(UserName, UserID, "延续注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text));
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