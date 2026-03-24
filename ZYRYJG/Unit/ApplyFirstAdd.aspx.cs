using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Model;
using DataAccess;
using System.Data;
using Telerik.Web.UI;

namespace ZYRYJG.Unit
{
    public partial class ApplyFirstAdd : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ApplyMDL"] == null ? "" : (ViewState["ApplyMDL"] as ApplyMDL).ApplyID; }
        }
        private string RegisteProfession
        {
            get { return ViewState["RegisteProfession"] == null ? "" : ViewState["RegisteProfession"].ToString(); }

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
            if (!this.IsPostBack)
            {
                UIHelp.FillDropDownListWithTypeName(RadComboBoxNation, "108", "请选择", "");//民族
                UIHelp.FillDropDownListWithTypeName(RadComboBoxXueLi, "109", "请选择", "");//学历

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;

                if (ViewLimit == true)//查看
                {
                    divViewAction.Visible = true;
                }


                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    ApplyMDL _ApplyMDL = ApplyDAL.GetObject(_ApplyID);
                    ViewState["ApplyMDL"] = _ApplyMDL;
                    ApplyFirstMDL _ApplyFirstMDL = ApplyFirstDAL.GetObject(_ApplyID);
                    ViewState["ApplyFirstMDL"] = _ApplyFirstMDL;
                    ViewState["RegisteProfession"] = _ApplyMDL.PSN_RegisteProfession;

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

                    #region 格式化显示面板

                    //根据资质判断是否显示新设立企业建造师注册承诺书
                    string cnsql = string.Format("and (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='工程勘察' OR SJLX='工程设计') and [ZZJGDM]='{0}'", _ApplyMDL.ENT_OrganizationsCode);

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

                    //个人登录后
                    if (IfExistRoleID("0") == true
                        && (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已驳回 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                        )
                    {
                        divGR.Visible = true;//个人操作按钮
                        //trFuJanTitel.Visible = true;
                        //trFuJan.Visible = true;

                        UIHelp.SetData(AddTable, _ApplyMDL, false);
                        UIHelp.SetData(AddTable, _ApplyFirstMDL, false);

                        UIHelp.SetReadOnly(RadTextBoxPSN_Name, true);
                        UIHelp.SetReadOnly(RadTextBoxPSN_CertificateNO, true);
                        UIHelp.SetReadOnly(RadDatePickerBirthday, true);
                        UIHelp.SetReadOnly(RadComboBoxPSN_Sex, true);
                    }
                    else//只读
                    {
                        UIHelp.SetData(AddTable, _ApplyMDL, true);
                        UIHelp.SetData(AddTable, _ApplyFirstMDL, true);
                        //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                        //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                        //RadioButtonListENT_ContractType.Enabled = false;
                        ////专业删除按钮
                        //RadGridExamInfo.MasterTableView.Columns.FindByUniqueName("Delete").Visible = false;
                        RadGridExamInfo.Enabled = false;

                         if (IfExistRoleID("0") == true&& _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已公示 )
                         {
                             divGR.Visible = true;//个人操作按钮
                         }
                    }
                    LabelPSN_RegisteProfession.Text = _ApplyFirstMDL.ApplyRegisteProfession;
                    LabelZSBH.Text = _ApplyFirstMDL.PSN_ExamCertCode;
                    ////2019-11-27南静添加
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
                    if (IfExistRoleID("2") == true)//企业
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

                    //区县受理权限
                    if (IfExistRoleID("3") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报 && ViewLimit == false)
                    {
                        divQX.Visible = true;
                    }

                    //区县审查
                    if (IfExistRoleID("7") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已受理 && ViewLimit == false)
                    {
                        divQXCK.Visible = true;
                    }

                    //建委审核权限
                    if (IfExistRoleID("4") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已上报 && ViewLimit == false)
                    {
                        divCheck.Visible = true;
                    }

                    //建委领导审核权限
                    if (IfExistRoleID("6") == true && _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已审查 && ViewLimit == false)
                    {
                        divDecide.Visible = true;
                    }

                    #endregion 格式化显示面板

                    BindFile(_ApplyFirstMDL.ApplyID);

                    SetExamInfo(_ApplyFirstMDL.ExamInfo);

                    BindCheckHistory(_ApplyFirstMDL.ApplyID);

                    SetButtonEnable(_ApplyMDL.ApplyStatus);

                }
                else//new  初始注册
                {
                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "企业信息中的监管区县与建造师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                    //    return;
                    //}

                    //#endregion

                    //个人登录后获取资格库专业信息
                    if (IfExistRoleID("0") == true)
                    {
                        divGR.Visible = true;
                        Tablezjhm0.Visible = true;//资格库按钮                       
                        Tablezjhm.Visible = true; //企业组织机构代码                       
                        ButtonSaveZjh.Visible = true; //确定按钮
                        divBase.Visible = false;
                        BindFile("0");//附件
                        //RadioButtonListENT_ContractType.SelectedIndex = 0;


                        #region 根据身份证检索资格库校验数据
                        
                        List<QualificationMDL> dt = QualificationDAL.GetObjectList(WorkerCertificateCode);
                        RadGridHZSB.DataSource = dt;
                        RadGridHZSB.DataBind();
                        if (dt.Count == 0)
                        {
                            UIHelp.layerAlert(Page, "没有在资格库中查到您的证书信息，可能是数据未同步，请联系注册中心!");
                        }
                        else if (dt.Count == 1)//有一个专业的时候，默认选中
                        {
                            ((RadGridHZSB.MasterTableView.Items[0].Cells[2].Controls[1]) as CheckBox).Checked = true;
                        }

                        #endregion
                    }
                }
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
            // ClientScript.RegisterStartupScript(GetType(), "height", "SetCwinHeight();", true);
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

        protected string FormatImage(string uriList)
        {
            string[] imgurl = uriList.Split(',');
            string[] atrt = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in imgurl)
            {
                atrt = s.Split('|');
                sb.Append(
                    string.Format("<div><img class=\"img200\" alt=\"图片\" src='{0}' /><input  runat=\"server\" type=\"image\" class=\"imgdel\" title=\"删除\" src=\"../images/Cancel.gif\" CommandName=\"Delete\" CommandArgument='{1}'  alt=\"删除\" onclick=\"if(!confirm('您确定要删除么?'))return false;\">删除</input></div>"

                    //<asp:ImageButton runat=\"server\" width=\"18px\" height=\"18px\" ImageUrl=\"../images/Cancel.gif\" CommandName=\"Delete\" CommandArgument='{1}' ToolTip=\"删除\" OnClientClick=\"return confirm('您确定要删除么?')\" >删除</asp:ImageButton></div>"

               , UIHelp.ShowFile(atrt[0])
               , atrt[1]
               ));
            }

            return sb.ToString();
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
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
            if (RadComboBoxNation.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "保存失败，民族不可为空！", 5, 0);
                return;
            }
            if (Utility.Check.CheckBirthdayLimit(0, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满65周岁前90天起,不再允许发起二级建造师初始、重新、延续、增项、执业企业变更注册申请。", 5, 0);
                return;
            }
            COC_TOW_Person_BaseInfoMDL find = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(WorkerCertificateCode);
            if (find != null)
            {
                if (find.PSN_RegisteType == EnumManager.ApplyTypeCode.注销)
                {
                    if (find.PSN_Level == "二级")
                    {
                        UIHelp.layerAlert(Page, "您已经注册过证书，无法进行初始注册，请走重新注册申报！", 5, 0);
                        return;
                    }
                }
                else
                {
                    UIHelp.layerAlert(Page, "您已经注册过证书，无法进行初始注册。要想重新注册请先进行注销，再走重新注册申报！", 5, 0);

                    return;
                }
            }

            if (ViewState["ApplyMDL"] == null)
            {
                if (ApplyDAL.SelectCount(string.Format(" and PSN_CertificateNO='{0}' and PSN_RegisteProfession='{3}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_CertificateNO.Text.Trim(), EnumManager.ApplyStatus.未申报, EnumManager.ApplyStatus.待确认, RegisteProfession)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }

            //初次申请信息
            ApplyFirstMDL o = ViewState["ApplyFirstMDL"] == null ? new ApplyFirstMDL() : (ApplyFirstMDL)ViewState["ApplyFirstMDL"];//详细表
            UIHelp.GetData(AddTable, o);
            //o.GetType = RadioButtonListGetType.SelectedValue;
            //其它资格考试合格专业情况
            o.ExamInfo = GetExamInfo();
            //注册专业
            o.ApplyRegisteProfession = LabelPSN_RegisteProfession.Text;
            //o.ApplyRegisteProfession=
            //二建申请信息
            ApplyMDL a = ViewState["ApplyMDL"] == null ? new ApplyMDL() : (ApplyMDL)ViewState["ApplyMDL"];//申请表
            UIHelp.GetData(AddTable, a);

            o.PSN_ExamCertCode = LabelZSBH.Text;

            a.XGR = UserName;
            a.XGSJ = DateTime.Now;
            a.Valid = 1;
            a.ApplyType = EnumManager.ApplyType.初始注册;
            a.ApplyStatus = EnumManager.ApplyStatus.未申报;

            //申请专业
            //a.PSN_RegisteProfession = RadComboBoxPSN_RegisteProfession.Text.Trim();

            a.PSN_RegisteProfession = RegisteProfession;
            //a.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);  //2019-11-27南静添加

            UnitMDL u = null; //企业信息
            string _ENT_Name = "";
            string _ENT_City = "";
            if (ViewState["ApplyMDL"] == null)//new
            {
                u = DataAccess.UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxFindENT_OrganizationsCode.Text.Trim());
                //所属区县
                if (string.IsNullOrEmpty(u.ENT_City))
                {
                    UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县", 5, 0);
                    return;
                }

                _ENT_Name = u.ENT_Name;
                _ENT_City = u.ENT_City;

                if (Utility.Check.CheckIfSubUnit(u.ENT_Name) == true)
                {
                    UIHelp.layerAlert(Page, UIHelp.Tip_SubUnitForbid, 5, 0);
                    return;
                }
          
                if (UnitDAL.CheckGongShang(RadTextBoxENT_OrganizationsCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxENT_Name.Text));
                    return;
                }
            }
            else
            {
                _ENT_Name = a.ENT_Name;
                _ENT_City = a.ENT_City;
            }

            //企业资质
            jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(RadTextBoxFindENT_OrganizationsCode.Text.Trim());
            GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(RadTextBoxFindENT_OrganizationsCode.Text.Trim()); //工商信息
            if (_jcsjk_QY_ZHXXMDL != null)
            {

                if (_ENT_Name.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")")//企业名称变更
                   || _ENT_City != _jcsjk_QY_ZHXXMDL.XZDQBM//隶属区县变更
                   )
                {

                    UIHelp.layerAlert(Page, string.Format("检测到所注册企业信息与工商或资质库中企业信息不一致（{0}{1}），请先联系企业办理企业信息变更。",
                        _ENT_Name.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")") ? string.Format("企业名称：{0}≠{1}。", _ENT_Name, g.ENT_NAME) : "",
                    _ENT_City != _jcsjk_QY_ZHXXMDL.XZDQBM ? string.Format("隶属区县：{0}≠{1}。", _ENT_City, _jcsjk_QY_ZHXXMDL.XZDQBM) : ""
                        ), 5, 0);
                    return;
                }
            }

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyMDL"] == null)//new
                {
                    a.ApplyID = Guid.NewGuid().ToString();
                    a.ApplyCode = ApplyDAL.GetNextApplyCode(tran, "初始注册");
                    a.CJR = UserName;
                    a.CJSJ = a.XGSJ;
                    //企业ID
                    a.ENT_ServerID = u.UnitID;
                    //企业名称
                    a.ENT_Name = u.ENT_Name;

                    //企业组织机构代码
                    if (RadTextBoxFindENT_OrganizationsCode.Text.Trim().Length == 18)
                    {
                        a.ENT_OrganizationsCode = RadTextBoxFindENT_OrganizationsCode.Text.Trim().Substring(8, 9);
                    }
                    else
                    {
                        a.ENT_OrganizationsCode = RadTextBoxFindENT_OrganizationsCode.Text.Trim();
                    }
                  
                    a.ENT_City = u.ENT_City;
                    ////劳动合同开始时间
                    //a.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                    ////劳动合同结束时间
                    //if (RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                    //{
                    //    a.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                    //}

                    a.PSN_CertificateType = (a.PSN_CertificateNO.Length >= 15) ? "身份证" : "其他";

                    DataAccess.ApplyDAL.Insert(tran, a);

                    o.ApplyID = a.ApplyID;
                    //企业法人
                    o.FR = u.ENT_Corporate;
                    //企业联系人
                    o.LinkMan = u.ENT_Contact;
                    //企业联系电话
                    o.ENT_Telephone = u.ENT_Telephone;
                    //企业通讯地址
                    o.ENT_Correspondence = u.ENT_Correspondence;
                    //企业性质
                    o.ENT_Economic_Nature = u.ENT_Economic_Nature;
                    //企业工商注册地址
                    o.END_Addess = u.END_Addess;
                    //邮政编码
                    o.ENT_Postcode = u.ENT_Postcode;
                    //企业类型
                    o.ENT_Type = u.ENT_Type;
                    //企业资质类别
                    o.ENT_Sort = u.ENT_Sort;
                    //企业资质等级
                    o.ENT_Grade = u.ENT_Grade;
                    //企业资质证书编号
                    o.ENT_QualificationCertificateNo = u.ENT_QualificationCertificateNo;


                    //是否与其他
                    //if (RadioButton2.Checked)
                    //    o.IfSameUnit = false;
                    //else
                    //    o.IfSameUnit = true;
                    //插入初次申请信息
                    DataAccess.ApplyFirstDAL.Insert(tran, o);

                    //trFuJanTitel.Visible = true;
                    //trFuJan.Visible = true;


                    //根据资质判断是否显示新设立企业建造师注册承诺书
                    string cnsql = string.Format("and (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='工程勘察' OR SJLX='工程设计') and [ZZJGDM]='{0}'", a.ENT_OrganizationsCode);
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
                    ////劳动合同开始时间
                    //a.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                    ////劳动合同结束时间
                    //if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                    //{
                    //    a.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                    //}
                    //else
                    //{
                    //    a.ENT_ContractENDTime = null;
                    //}
                    ApplyDAL.Update(tran, a);
                    ApplyFirstDAL.Update(tran, o);

                }

                tran.Commit();

                LabelApplyCode.Text = a.ApplyCode;
                ViewState["ApplyMDL"] = a;
                ViewState["ApplyFirstMDL"] = o;

                //附件照片
                BindFile(a.ApplyID);

                SetButtonEnable(a.ApplyStatus);

                //UIHelp.layerAlert(Page, "保存成功，请上传必要的扫描件后进行申报！", 6, 2000);
                UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请保存成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text));
                UIHelp.layerAlert(Page, "保存成功，请上传必要的扫描件后进行提交！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");
                // Response.Write("<script languge='javascript'>alert('保存成功，请上传必要的扫描件后进行申报！');</script>");
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存初始注册申请表失败！", ex);
                return;
            }

        }

        /// <summary>
        /// 获取附件数据类型code
        /// </summary>
        /// <param name="CotrolID">上传控件ID</param>
        /// <returns></returns>
        private string GetDataType(string CotrolID)
        {
            switch (CotrolID)
            {
                case "FileUploadZJSMJ":
                    return EnumManager.FileDataType.证件扫描件;
                case "FileUploadXLZS":
                    return EnumManager.FileDataType.学历证书扫描件;
                case "FileUploadZYZGZS":
                    return EnumManager.FileDataType.执业资格证书扫描件;
                case "FileUploadLDHT":
                    return EnumManager.FileDataType.劳动合同扫描件;
                case "FileUploadJXJY":
                    return EnumManager.FileDataType.继续教育承诺书扫描件;
                case "FileUploadWord":
                    return EnumManager.FileDataType.申请表扫描件;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取附件显示名称
        /// </summary>
        /// <param name="CotrolID">上传控件ID</param>
        /// <returns></returns>
        private string GetShowName(string CotrolID)
        {
            switch (CotrolID)
            {
                case "FileUploadZJSMJ":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.证件扫描件);
                case "FileUploadXLZS":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.学历证书扫描件);
                case "FileUploadZYZGZS":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.执业资格证书扫描件);
                case "FileUploadLDHT":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.劳动合同扫描件);
                case "FileUploadJXJY":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.继续教育承诺书扫描件);
                case "FileUploadWord":
                    return EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.申请表扫描件);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取考试合格证信息
        /// </summary>
        /// <returns></returns>
        private string GetExamInfo()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //其它资格考试合格专业情况
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
        /// 绑定考试合格证信息
        /// </summary>
        /// <param name="ExamInfo"></param>
        private void SetExamInfo(string ExamInfo)
        {
            DataTable dt = null;
            if (string.IsNullOrEmpty(ExamInfo) == true)//new
            {
                //dt = CommonDAL.GetDataTable(@"select 1 RowNum ");
                //RadGridExamInfo.DataSource = dt;
                //RadGridExamInfo.DataBind();
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
                if (gi.ItemIndex == DelrowIndex) continue;

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

        //提交单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            ApplyFirstMDL _ApplyFirstMDL = (ApplyFirstMDL)ViewState["ApplyFirstMDL"];

            try
            {
                if (ButtonUnit.Text != "取消申报")//提交单位确认
                {
                    #region 必须上传附件集合

                    //必须上传附件集合
                    System.Collections.Hashtable fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.一寸免冠照片,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0},
                        //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                        //{EnumManager.FileDataTypeName.执业资格证书扫描件,0},
                        //{EnumManager.FileDataTypeName.劳动合同扫描件,0},
                        {EnumManager.FileDataTypeName.申请表扫描件,0}};



                    //校验继续教育情况，是否满足最近三年继续教育情况
                    if (_ApplyFirstMDL.ConferDate.Value.AddYears(3) < DateTime.Now)
                    {
                        fj.Add(EnumManager.FileDataTypeName.继续教育承诺书扫描件, 0);
                    }

                    if (ViewState["Xsl"].ToString() == "新设立")
                    {
                        fj.Add(EnumManager.FileDataTypeName.新设立企业建造师注册承诺书, 0);
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

                    string facePhotoUrl = ApplyFileDAL.SelectFaceImgPathByApplyID(_ApplyMDL.ApplyID);
                    if (string.IsNullOrEmpty(facePhotoUrl) == false)
                    {
                        bool ifWhiteFacePhoto = UIHelp.CheckIfWhiteBackgroudPhoto(Server.MapPath(facePhotoUrl));
                        if(ifWhiteFacePhoto==false)
                        {
                            UIHelp.layerAlert(Page, string.Format("电子证书要求提供近期一寸白底标准正面免冠证件照，请检查照片是否为白底照。", sb), 5, 0);
                            return;
                        }
                    }

                    #endregion 必须上传附件集合
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
                    _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.待确认;
                    _ApplyMDL.ApplyTime = DateTime.Now;

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
                BindFile(_ApplyMDL.ApplyID);//附件
                if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请提交成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='ApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("apply"), Utility.Cryptography.Encrypt(_ApplyMDL.ApplyID.ToString()), _ApplyMDL.ApplyID)); 

                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请撤销成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    //RadDatePickerENT_ContractStartTime.Enabled = true;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = true;//劳动合同结束时间
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "初始注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表

            //ApplyFirstMDL _ApplyFirstMDL = (ApplyFirstMDL)ViewState["ApplyFirstMDL"];

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
                    _ApplyMDL.OldUnitCheckResult = RadioButtonListOldUnitCheckResult.SelectedValue; ;//现单位审核结果
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
                    UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    //string js = string.Format("<script>window.parent.location.href='ApplyHistory.aspx?c={0}';</script>", _ApplyMDL.ApplyID);
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);

                    //Response.Redirect(string.Format("~/Unit/ApplyHistory.aspx?c={0}", _ApplyMDL.ApplyID), true);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "初始注册申报失败！", ex);
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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请删除成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
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
            step_已公示.Attributes["class"] = step_已公示.Attributes["class"].Replace(" green", "");
            step_已公告.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");
            step_已放号.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");
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
                    ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"] == null ? null : (ApplyMDL)ViewState["ApplyMDL"];

                    if (_ApplyMDL != null && string.IsNullOrEmpty(_ApplyMDL.PSN_RegisterNo) == false)
                    {
                        step_已放号.Attributes["class"] += " green";
                    }
                    else
                    {
                        step_已公告.Attributes["class"] += " green";
                    }
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        /// <summary>
        /// 操作按钮控制
        /// </summary>
        /// <param name="ApplyStatus"></param>
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

                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确 定";
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

                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确 定";
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
                    ButtonApply.Text = "取消申报";
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

        //导出打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师初始注册申请表.docx");
                string fileName = "北京市二级建造师初始注册申请表";
                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                ApplyFirstMDL _ApplyFirstMDL = ViewState["ApplyFirstMDL"] as ApplyFirstMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_ApplyMDL);
                o.Add(_ApplyFirstMDL);
                var ht = PrintDocument.GetProperties(o);
                //对时间类型进行格式转换
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
                ht["Birthday"] = _ApplyFirstMDL.Birthday == null ? "" : ((DateTime)_ApplyFirstMDL.Birthday).ToString("yyyy年MM月dd日");
                ht["GraduationTime"] = _ApplyFirstMDL.GraduationTime == null ? "" : ((DateTime)_ApplyFirstMDL.GraduationTime).ToString("yyyy年MM月dd日");
                ht["ConferDate"] = _ApplyFirstMDL.ConferDate == null ? "" : ((DateTime)_ApplyFirstMDL.ConferDate).ToString("yyyy年MM月dd日");
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
                //string ZjType = ht["PSN_CertificateType"].ToString();
                //ht["SFZ"] = ZjType == "身份证" ? "☑" : "□";
                //ht["JGZ"] = ZjType == "军官证" ? "☑" : "□";
                //ht["JG"] = ZjType == "警官证" ? "☑" : "□";
                //ht["HZ"] = ZjType == "护照" ? "☑" : "□";
                ////企业类型
                //string QyType = ht["ENT_Type"].ToString();
                //ht["SG"] = QyType == "施工" ? "☑" : "□";
                //ht["KC"] = QyType == "勘察" ? "☑" : "□";
                //ht["SJ"] = QyType == "设计" ? "☑" : "□";
                //ht["JL"] = QyType == "监理" ? "☑" : "□";
                //ht["ZBDL"] = QyType == "招标代理" ? "☑" : "□";
                //ht["ZJZX"] = QyType == "造价咨询" ? "☑" : "□";
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
                //把ExamInfo里面的数据拆分成一张内存表
                string arry = _ApplyFirstMDL.ExamInfo;
                string[] arryty = arry.Split('|');
                DataTable dt = new DataTable();
                //表列明意思分别（每逗号一个列）：考试合格专业类别，考试编号，签发日期，注册专业，必修课时，选修课时
                dt.Columns.Add("KSHGZYLB");
                dt.Columns.Add("KSBH");
                dt.Columns.Add("QFRQ");
                dt.Columns.Add("ZCZY");
                dt.Columns.Add("BXKS");
                dt.Columns.Add("XXKS");
                foreach (string s in arryty)
                {
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    string[] str = s.Split(',');
                    foreach (string j in str)
                    {

                        dr[i] = j;
                        i++;
                    }
                    dt.Rows.Add(dr);
                }
                ht["tableList"] = new List<DataTable> { dt };
                ht["tableIndex"] = new List<int> { 1 };
                ht["insertIndex"] = new List<int> { 2 };
                ht["ContainsHeader"] = new List<bool> { true };
                ht["isCtable"] = true;

                //builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center; //垂直居中对齐

                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印初始申请Word失败！", ex);
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

            //_ApplyMDL.Memo=_ApplyMDL.CheckXSL == 1 ? "1" : "0"; //是否是新设立
            #region 检查是否为新设立
            if (_ApplyMDL.CheckXSL == 1 || _ApplyMDL.CheckXSL == null)
            {
                _NewSetUpMDL.ApplyId = _ApplyMDL.ApplyID;
                //_NewSetUpMDL.ENT_City = _ApplyMDL.ENT_City;
                //_NewSetUpMDL.ENT_Name = _ApplyMDL.ENT_Name;
                //_NewSetUpMDL.ENT_OrganizationsCode = _ApplyMDL.ENT_OrganizationsCode;
                //_NewSetUpMDL.Psn_Name = _ApplyMDL.PSN_Name;
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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册区县受理", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text
                 , _ApplyMDL.GetResult, _ApplyMDL.GetRemark));
            // UIHelp.ParentAlert(Page, "受理成功！", true);
            //this.form1.Visible = false;

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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册区县审核", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text
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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册建委审核", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _ApplyMDL.PSN_Name, _ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text
                , _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark));
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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册建委决定成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text));
            UIHelp.ParentAlert(Page, "决定成功！", true);
        }

        //确定选定注册专业资格证书
        protected void ButtonSaveZjh_Click(object sender, EventArgs e)
        {
            #region 规则检查

            if (RadTextBoxFindENT_OrganizationsCode.Text == null || RadTextBoxFindENT_OrganizationsCode.Text == "")
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
            DataTable dt = CommonDAL.GetDataTable(string.Format("select * from dbo.[Apply] where PSN_CertificateNO='{0}' and  ApplyStatus !='已公告' ", WorkerCertificateCode));
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ApplyID"] != DBNull.Value)
            {
                //UIHelp.layerAlert(Page, "已有在办业务不允许初始注册！", 5, 0);
                UIHelp.layerAlert(Page, string.Format("【{0}】正在为【{1}】办理{2}，无法申请其他注册申请，请自行联系该单位进行协商取消在办业务。"
                  , dt.Rows[0]["ENT_Name"]
                  , dt.Rows[0]["PSN_Name"]
                  , dt.Rows[0]["ApplyType"]
                  ), 5, 0);
                return;
            }

            ////查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(WorkerCertificateCode);
            //if (count > 0)
            //{
            //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    //UIHelp.layerAlert(Page, "您的注册状态异常，涉嫌存在挂靠注册证书的违法违规行为，请联系注册中心（55598090）核实相关情况！", 5, 0);
            //    return;
            //}

            //根据用户输入的组织机构代码查询企业信息 
            UnitMDL unitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxFindENT_OrganizationsCode.Text.Trim());
            if (unitMDL == null)//企业表中不存在该企业信息
            {
                UIHelp.layerAlert(Page, "该企业未在办事大厅注册账号，请先联系企业注册账号后再进行初始注册！");
                return;
            }
            else
            {
                //资格库按钮
                Tablezjhm0.Visible = false;
                //企业按钮
                Tablezjhm.Visible = false;
                AddTable.Visible = false;
                divGR.Visible = false;
                //附件
                BindFile("0");
                UIHelp.SetData(AddTable, unitMDL, true);
                //法人
                RadTextBoxFR.Text = unitMDL.ENT_Corporate;
                UIHelp.SetReadOnly(RadTextBoxFR, true);
                //联系人

                RadTextBoxLinkMan.Text = unitMDL.ENT_Contact;
                UIHelp.SetReadOnly(RadTextBoxLinkMan, true);

            }
            #endregion

            ButtonApply.Enabled = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //校验继续教育情况，是否满足最近三年继续教育情况
            if (Convert.ToDateTime(_ListQualificationMDL[0].QFSJ).AddYears(3) < DateTime.Now)
            {
                LabelBiXiu.Text = "60";
                LabelXuanXiu.Text = "60";
            }
            //
            RadTextBoxPSN_Name.Text = _ListQualificationMDL[0].XM;//姓名
            RadTextBoxPSN_CertificateNO.Text = _ListQualificationMDL[0].ZJHM;//证件号码
            LabelZhuanYe.Text = _ListQualificationMDL[0].ZYLB;
            LabelZSBH.Text = _ListQualificationMDL[0].ZGZSBH;

            LabelConferDate.Text = _ListQualificationMDL[0].QFSJ.Value.ToString("yyyy-MM-dd");
            LabelPSN_RegisteProfession.Text = _ListQualificationMDL[0].ZYLB;
            //根据身份证号码直接给性别，年月日赋值
            string certificatecode = _ListQualificationMDL[0].ZJHM;

            try
            {
                string BirthDay = string.Format("{0}-{1}-{2}", certificatecode.Substring(6, 4), certificatecode.Substring(10, 2), certificatecode.Substring(12, 2));
                RadDatePickerBirthday.SelectedDate = Convert.ToDateTime(BirthDay);
                //性别
                int sex = Convert.ToInt32(certificatecode.Substring(16, 1));
                if (sex % 2 == 0)
                {
                    RadComboBoxPSN_Sex.FindItemByValue("女").Selected = true;
                }
                else
                {
                    RadComboBoxPSN_Sex.FindItemByValue("男").Selected = true;
                }
            }
            catch
            {
                UIHelp.SetReadOnly(RadComboBoxPSN_Sex, false);
                UIHelp.SetReadOnly(RadDatePickerBirthday, false);
                Utility.FileLog.WriteLog(string.Format("初始注册根据证件号码{0}获取生日和性别失败！",certificatecode));
            }

            RadTextBoxSchool.Text = _ListQualificationMDL[0].BYXX;//毕业学校
            RadTextBoxMajor.Text = _ListQualificationMDL[0].SXZY;//所学专业
            //RadTextBoxXueLi.Text = _ListQualificationMDL[0].ZGXL;

            //最高学历
            foreach (RadComboBoxItem i in RadComboBoxXueLi.Items)
            {
                if (i.Value == "") continue;
                if(string.IsNullOrEmpty(_ListQualificationMDL[0].ZGXL)==false && _ListQualificationMDL[0].ZGXL.IndexOf(i.Value) !=-1)
                {
                    i.Selected = true;
                    break;
                }
            }
            if (_ListQualificationMDL[0].BYSJ.HasValue == true)//毕业时间
            {
                RadDatePickerGraduationTime.SelectedDate = _ListQualificationMDL[0].BYSJ.Value;
            }

            //考生信息
            WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
            RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
            RadTextBoxPSN_Telephone.Text = _WorkerOB.Phone;
            RadTextBoxPSN_Email.Text = _WorkerOB.Email;
            if (string.IsNullOrEmpty(_WorkerOB.Nation) == false)//民族
            {
                RadComboBoxItem find = RadComboBoxNation.Items.FindItemByText(_WorkerOB.Nation);
                if (find != null) find.Selected = true;
            }
            if (RadTextBoxPSN_MobilePhone.Text != "")
            {
                UIHelp.SetReadOnly(RadTextBoxPSN_MobilePhone, true);//不允许修改来自大厅实名制认证电话
            }

            UIHelp.SetReadOnly(RadTextBoxPSN_Name, true);
            UIHelp.SetReadOnly(RadTextBoxPSN_CertificateNO, true);
            UIHelp.SetReadOnly(RadDatePickerBirthday, true);
            UIHelp.SetReadOnly(RadComboBoxPSN_Sex, true);

            for (int i = 1; i < _ListQualificationMDL.Count; i++)
            {
                string bxxs = "", xxxs = "";
                //校验继续教育情况，是否满足最近三年继续教育情况
                if (Convert.ToDateTime(_ListQualificationMDL[i].QFSJ.Value).AddYears(3) < DateTime.Now)
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
            ViewState["RegisteProfession"] = GetProfession();
            SetExamInfo(sb.ToString());

            AddTable.Visible = true;
            divGR.Visible = true;
            divBase.Visible = true;
            Tablezjhm.Visible = false;
            SetButtonEnable("");
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

                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["BYXX"] != null) _QualificationMDL.BYXX = RadGridHZSB.MasterTableView.DataKeyValues[i]["BYXX"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["BYSJ"] != null) _QualificationMDL.BYSJ = Convert.ToDateTime(RadGridHZSB.MasterTableView.DataKeyValues[i]["BYSJ"]);
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["SXZY"] != null) _QualificationMDL.SXZY = RadGridHZSB.MasterTableView.DataKeyValues[i]["SXZY"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["ZGXL"] != null) _QualificationMDL.ZGXL = RadGridHZSB.MasterTableView.DataKeyValues[i]["ZGXL"].ToString();
                    if (RadGridHZSB.MasterTableView.DataKeyValues[i]["QFSJ"] != null) _QualificationMDL.QFSJ = Convert.ToDateTime(RadGridHZSB.MasterTableView.DataKeyValues[i]["QFSJ"]);
                    _ListQualificationMDL.Add(_QualificationMDL);
                }
            }
            return _ListQualificationMDL;
        }

        /// <summary>
        /// 根据勾选项获取注册专业
        /// </summary>
        /// <returns></returns>
        private string GetProfession()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < RadGridHZSB.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridHZSB.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",").Append(RadGridHZSB.MasterTableView.DataKeyValues[i]["ZYLB"].ToString());
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
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
            UIHelp.WriteOperateLog(UserName, UserID, "初始注册申请表附件删除成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

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
                    RadTextBoxFindENT_OrganizationsCode.Text = _UnitMDL.ENT_OrganizationsCode;  //单位组织机构代码
                }
            }
            else
            {
                UIHelp.layerAlert(Page, "未查询到您输入的企业,请核实后重新输入！", 5, 0);
                RadTextBoxFindENT_OrganizationsCode.Text = "";  //单位组织机构代码
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
                   , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.GetDateTime, _ApplyMDL.GetResult, _ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.ExamineDatetime, _ApplyMDL.ExamineResult, _ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.ReportDate, _ApplyMDL.ReportMan, _ApplyMDL.ReportCode, RadComboBoxReturnApplyStatus.SelectedValue);
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
                            
                            _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                            break;
                        case EnumManager.ApplyStatus.已受理:
                            _ApplyMDL.ExamineDatetime = null;
                            _ApplyMDL.ExamineMan = null;
                            _ApplyMDL.ExamineResult = null;
                            _ApplyMDL.ExamineRemark = null;
                            
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                        case EnumManager.ApplyStatus.区县审查:
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
                            _ApplyMDL.ReportCode = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.区县审查;
                            break;
                    }
                    break;
                case EnumManager.ApplyStatus.已审查:
                    log = string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，审查时间：{3}，审查结果：{4}，审查意见：{5}。后退到“{6}”状态。"
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.CheckDate, _ApplyMDL.CheckResult, _ApplyMDL.CheckRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                            
                            _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
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
                            
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
                            _ApplyMDL.ReportCode = null;

                           _ApplyMDL.CheckDate = null;
                            _ApplyMDL.CheckMan = null;
                            _ApplyMDL.CheckResult = null;

                            _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已受理;
                            break;
                        case EnumManager.ApplyStatus.区县审查:
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.ConfirmDate, _ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
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
                            
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
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
                            
                             _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
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
                                _ApplyMDL.ReportDate=null;
                            _ApplyMDL.ReportMan=null;
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _ApplyMDL.NoticeDate, _ApplyMDL.NoticeMan, _ApplyMDL.NoticeCode, RadComboBoxReturnApplyStatus.SelectedValue);
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