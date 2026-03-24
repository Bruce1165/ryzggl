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

namespace ZYRYJG.zjs
{
    public partial class zjsApplyFirstAdd : BasePage
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
                    PackageMDL _PackageMDL = PackageDAL.GetObject("二级造价工程师", LabelPSN_RegisteProfession.Text.Trim());
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "ZJS_Package", _PackageMDL, 1);//缓存1小时
                    return _PackageMDL;
                }
            }
        }

        /// <summary>
        /// 是否为首次注册二级造价工程师
        /// </summary>
        protected bool IsFirstRegistration
        {
            get
            {
                if (ViewState["IsFirstRegistration"] == null)
                {
                    throw new Exception("获取是否为首次注册二级造价工程师时失败。");
                }
                return Convert.ToBoolean(ViewState["IsFirstRegistration"]);
            }
            set
            {
                ViewState["IsFirstRegistration"] = value;
            }
        }

        //权限
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
                UIHelp.FillDropDownListWithTypeName(RadComboBoxXueLi, "109", "请选择", "");//学历

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                if (string.IsNullOrEmpty(Request["a"]) == false)//eidt
                {
                    string _ApplyID = Utility.Cryptography.Decrypt(Request["a"]);

                    //注册申请表     
                    zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                    zjs_ApplyFirstMDL _zjs_ApplyFirstMDL = zjs_ApplyFirstDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyFirstMDL"] = _zjs_ApplyFirstMDL;

                    IsFirstRegistration = string.IsNullOrEmpty(_zjs_ApplyMDL.PSN_RegisterNo);

                    if (string.IsNullOrEmpty(_zjs_ApplyMDL.LastBackResult) == false && _zjs_ApplyMDL.ApplyStatus != EnumManager.ZJSApplyStatus.已驳回)
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("【上次退回记录】{0}", _zjs_ApplyMDL.LastBackResult);
                    }

                    //zjs_QualificationMDL _zjs_QualificationMDL = zjs_QualificationDAL.GetObject(_zjs_ApplyFirstMDL.PSN_ExamCertCode);//资格证信息
                    //if (_zjs_QualificationMDL != null
                    //     && (_zjs_QualificationMDL.ZGXL != _zjs_ApplyFirstMDL.XueLi//填报学历与资格证不一致
                    //        || _zjs_QualificationMDL.BYXX != _zjs_ApplyFirstMDL.School//毕业学校
                    //        || _zjs_QualificationMDL.SXZY != _zjs_ApplyFirstMDL.ZhuanYe//所学专业
                    //        || _zjs_QualificationMDL.BYSJ != _zjs_ApplyFirstMDL.GraduationTime//毕业时间
                    //        )
                    //    )
                    //{
                    //    string tip = string.Format(@"<table id=\'zgk\' style=\'width:500px\' class=\'table\'><tr><td style=\'text-align:right\'>姓名：</td> <td>{0}</tr><tr><td style=\'text-align:right\'>证件号码：</td> <td>{1}</td></tr><tr><td style=\'text-align:right\'>工作单位：</td> <td>{2}</td></tr><tr><td style=\'text-align:right\'>资格证书编号：</td> <td>{3}</td></tr><tr><td style=\'text-align:right\'>取得年份：</td> <td>{4}</td></tr><tr><td style=\'text-align:right\'>专业类别：</td> <td>{5}</td></tr><tr><td style=\'text-align:right\'>签发日期：</td> <td>{6}</td></tr><tr><td style=\'text-align:right\'>毕业学校：</td> <td>{7}</td></tr><tr><td style=\'text-align:right\'>毕业时间：</td> <td>{8}</td></tr><tr><td style=\'text-align:right\'>所学专业：</td> <td>{9}</td></tr><tr><td style=\'text-align:right\'>最高学历：</td> <td>{10}</td></tr></table>"
                    //        ,_zjs_QualificationMDL.XM
                    //        ,_zjs_QualificationMDL.ZJHM
                    //        ,_zjs_QualificationMDL.GZDW
                    //        ,_zjs_QualificationMDL.ZGZSBH
                    //        ,_zjs_QualificationMDL.QDNF
                    //        ,_zjs_QualificationMDL.ZYLB
                    //        , _zjs_QualificationMDL.QFSJ.HasValue ? _zjs_QualificationMDL.QFSJ.Value.ToString("yyyy-MM-dd") : ""
                    //        ,_zjs_QualificationMDL.BYXX
                    //        , _zjs_QualificationMDL.BYSJ.HasValue ? _zjs_QualificationMDL.BYSJ.Value.ToString("yyyy-MM-dd") : ""
                    //        ,_zjs_QualificationMDL.SXZY
                    //        ,_zjs_QualificationMDL.ZGXL);
                    //    td_XueLiCheckTip.InnerHtml = string.Format("提示：申报学历信息与人事局资格证书信息发生变化,请审核人员认真核查学历附件。<span style='color:blue;cursor:pointer' onclick=\"layer.open( {{title:'资格证书信息详细',content:'{0}',offset:'150px',area: ['600px', 'auto']}});\">>>点击查看资格证书信息</span>"
                    //        , tip.Replace(string.Format("{0}", (char)10), "<br/>").Replace(string.Format("{0}", (char)13), ""));

                    //}
                    //else
                    //{
                    //    td_XueLiCheckTip.InnerText = "";
                    //}

                    #region 格式化显示面板

                    //个人登录后
                    if (IfExistRoleID("0") == true
                        && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                        )
                    {
                        divGR.Visible = true;//个人操作按钮

                        UIHelp.SetData(AddTable, _zjs_ApplyMDL, false);
                        UIHelp.SetData(AddTable, _zjs_ApplyFirstMDL, false);

                        UIHelp.SetReadOnly(RadTextBoxPSN_Name, true);//姓名
                        UIHelp.SetReadOnly(RadTextBoxPSN_CertificateNO, true);//身份证号
                        UIHelp.SetReadOnly(RadDatePickerBirthday, true);//生日
                        UIHelp.SetReadOnly(RadComboBoxPSN_Sex, true);//性别

                        UIHelp.SetReadOnly(RadTextBoxSchool, false);//毕业院校
                        UIHelp.SetReadOnly(RadTextBoxMajor, false);//所学专业
                        UIHelp.SetReadOnly(RadDatePickerGraduationTime, false);//毕业（肄、结）时间
                        UIHelp.SetReadOnly(RadComboBoxXueLi, false);//最高学历

                    }
                    else//只读
                    {
                        UIHelp.SetData(AddTable, _zjs_ApplyMDL, true);
                        UIHelp.SetData(AddTable, _zjs_ApplyFirstMDL, true);
                        //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                        //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                        //RadioButtonListENT_ContractType.Enabled = false;
                    }
                    LabelPSN_RegisteProfession.Text = _zjs_ApplyFirstMDL.ApplyRegisteProfession;
                    LabelZSBH.Text = _zjs_ApplyFirstMDL.PSN_ExamCertCode;

                    //if (_zjs_ApplyMDL.ENT_ContractType.HasValue)
                    //{
                    //    RadioButtonListENT_ContractType.Items.FindByValue(_zjs_ApplyMDL.ENT_ContractType.ToString()).Selected = true;

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

                    #endregion 格式化显示面板

                    BindFile(_zjs_ApplyFirstMDL.ApplyID);

                    BindCheckHistory(_zjs_ApplyFirstMDL.ApplyID);
                    RefreshRadGridXuanXiu();
                    SetButtonEnable(_zjs_ApplyMDL);
                    ShowFinishGYPX();
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
                        divBase.Visible = false;
                        BindFile("0");//附件
                        //RadioButtonListENT_ContractType.SelectedIndex = 0;

                        #region 根据身份证检索资格库校验数据

                        List<zjs_QualificationMDL> dt = zjs_QualificationDAL.GetListByZJHM(WorkerCertificateCode);
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
            else if (Request["__EVENTTARGET"] == "refreshFile")//上传附件刷新
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
            else if (Request["__EVENTTARGET"] == "selectUnit")//选择注册的企业
            {
                #region 根据选择资格证书和选择企业代入个人和企业信息

                //规则检查
                if (string.IsNullOrEmpty(ZZJGDM_check.Text.Trim()) == true)
                {
                    UIHelp.layerAlert(Page, "请选择一个有效的注册单位！");
                    return;
                }

                List<zjs_QualificationMDL> _ListQualificationMDL = ListQualificationMDL();
                if (_ListQualificationMDL.Count == 0)
                {
                    UIHelp.layerAlert(Page, "请选择一个要注册的专业！");
                    return;
                }

                //在办业务检查
                DataTable dt = CommonDAL.GetDataTable(string.Format("select * from dbo.[zjs_Apply] where PSN_CertificateNO='{0}' and PSN_RegisteProfession ='{1}' and  NoticeDate is null ", WorkerCertificateCode, _ListQualificationMDL[0].ZYLB));
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ApplyID"] != DBNull.Value)
                {
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
                //      return;
                //}

                //根据用户输入的组织机构代码查询企业信息 
                UnitMDL unitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(ZZJGDM_check.Text.Trim());
                if (unitMDL == null)//企业表中不存在该企业信息
                {
                    UIHelp.layerAlert(Page, "该企业未在办事大厅注册账号，请先联系企业注册账号后再进行初始注册！");
                    return;
                }
                else
                {
                    Tablezjhm0.Visible = false;
                    Tablezjhm.Visible = false;
                    AddTable.Visible = false;
                    divGR.Visible = false;
                    BindFile("0"); //附件
                    UIHelp.SetData(AddTable, unitMDL, true);
                    RadTextBoxFR.Text = unitMDL.ENT_Corporate;  //法人
                    UIHelp.SetReadOnly(RadTextBoxFR, true);
                    RadTextBoxLinkMan.Text = unitMDL.ENT_Contact;//联系人
                    UIHelp.SetReadOnly(RadTextBoxLinkMan, true);
                    RadTextBoxENT_OrganizationsCode.Text = unitMDL.CreditCode;//统一社会信用代码
                }


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
                    Utility.FileLog.WriteLog(string.Format("初始注册根据证件号码{0}获取生日和性别失败！", certificatecode));
                }

                RadTextBoxSchool.Text = _ListQualificationMDL[0].BYXX;//毕业学校
                RadTextBoxMajor.Text = _ListQualificationMDL[0].SXZY;//所学专业

                //最高学历
                foreach (RadComboBoxItem i in RadComboBoxXueLi.Items)
                {
                    if (i.Value == "") continue;
                    if (string.IsNullOrEmpty(_ListQualificationMDL[0].ZGXL) == false && _ListQualificationMDL[0].ZGXL.IndexOf(i.Value) != -1)
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
                RadTextBoxPSN_Name.Text = _WorkerOB.WorkerName;//姓名
                LabelPSN_CertificateType.Text = _WorkerOB.CertificateType;//证件类型
                RadTextBoxPSN_CertificateNO.Text = _WorkerOB.CertificateCode;//证件号码
                RadTextBoxPSN_MobilePhone.Text = _WorkerOB.Phone;   //联系电话
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

                AddTable.Visible = true;
                divGR.Visible = true;
                divBase.Visible = true;
                Tablezjhm.Visible = false;
                SetButtonEnable(null);

                zjs_CertificateMDL _zjs_CertificateMDL = zjs_CertificateDAL.GetObjectByPSN_CertificateNOAndPSN_RegisteProfession(WorkerCertificateCode, LabelPSN_RegisteProfession.Text);
                if (_zjs_CertificateMDL != null)
                {
                    IsFirstRegistration = false;
                    if (_zjs_CertificateMDL.PSN_RegisteType != EnumManager.ZJSApplyTypeCode.注销)
                    {
                        UIHelp.layerAlert(Page, "您已经注册过证书，无法进行初始注册。要想重新注册请先进行注销，再发起初始注册申报！", 5, 0);
                        return;
                    }

                    if (RadTextBoxPSN_Email.Text.Trim() == "" && string.IsNullOrEmpty(_zjs_CertificateMDL.PSN_Email) == false)
                    {
                        RadTextBoxPSN_Email.Text = _zjs_CertificateMDL.PSN_Email;
                    }
                }
                else
                {
                    IsFirstRegistration = true;
                }

                ShowFinishGYPX();

                #endregion
            }
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
                Decide(_zjs_ApplyMDL);
            }
        }

        //保存申请单
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool IfLock = DataAccess.LockZJSDAL.GetLockStatus(RadTextBoxPSN_CertificateNO.Text.Trim());
            if (IfLock == true)
            {
                UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
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
            if (RadComboBoxNation.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "保存失败，民族不可为空！", 5, 0);
                return;
            }
            if (Utility.Check.CheckBirthdayLimit(-1, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满70周岁前90天起,不再允许发起二级造价工程师初始、延续、执业企业变更注册申请。", 5, 0);
                return;
            }
            if (RadComboBoxXueLi.SelectedValue != "")
            {
                if ("博士后，博士，研究生，硕士，本科，大专".Contains(RadComboBoxXueLi.SelectedValue) == false)
                {
                    UIHelp.layerAlert(Page, "保存失败，考生报考二级造价工程师职业资格考试时，应具备工程经济、工程技术、工程管理类的大学专科及以上学历或学士及以上学位。", 5, 0);
                    return;
                }
            }

            //zjs_CertificateMDL find = zjs_CertificateDAL.GetObjectByPSN_CertificateNOAndPSN_RegisteProfession(WorkerCertificateCode, LabelPSN_RegisteProfession.Text);
            //if (find != null && find.PSN_RegisteType != EnumManager.ZJSApplyTypeCode.注销)
            //{
            //    UIHelp.layerAlert(Page, "您已经注册过证书，无法进行初始注册。要想重新注册请先进行注销，再发起初始注册申报！", 5, 0);
            //    return;
            //}

            //检查是否注册过造价工程师、以及多专业是否注册在同一单位
            List<zjs_CertificateMDL> list = zjs_CertificateDAL.GetObjectByPSN_CertificateNO_All(WorkerCertificateCode);
            string Old_PSN_RegisterNo = "";//原注册编号（原证书注销后再次注册，走初始注册流程，申请单记录注册编号，公告后申请单存在注册编号的就不用走编号流程）
            foreach (zjs_CertificateMDL _zjs_CertificateMDL in list)
            {
                if (_zjs_CertificateMDL.PSN_RegisteProfession == LabelPSN_RegisteProfession.Text)
                {
                    if (_zjs_CertificateMDL.PSN_RegisteType != EnumManager.ZJSApplyTypeCode.注销)//已经注册，并未注销
                    {
                        UIHelp.layerAlert(Page, "您已经注册过证书，无法进行初始注册。要想重新注册请先进行注销，再发起初始注册申报！", 5, 0);
                        return;
                    }
                    else//已注销，相当于重新注册
                    {
                        Old_PSN_RegisterNo = _zjs_CertificateMDL.PSN_RegisterNO;//原注册编号
                    }
                }

                if (_zjs_CertificateMDL.PSN_RegisteProfession != LabelPSN_RegisteProfession.Text
                    && _zjs_CertificateMDL.PSN_RegisteType != EnumManager.ZJSApplyTypeCode.注销
                    && _zjs_CertificateMDL.ENT_OrganizationsCode != RadTextBoxENT_OrganizationsCode.Text)
                {
                    UIHelp.layerAlert(Page, string.Format("您已经在{0}注册过造价工程师证书，多专业必须注册在同一家企业，你选择的企业无法进行注册。", _zjs_CertificateMDL.ENT_Name), 5, 0);
                    return;
                }
            }

            //检查是否与二建注册在同一家单位
            COC_TOW_Person_BaseInfoMDL erJian = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(WorkerCertificateCode, "二级");
            if (erJian != null && erJian.PSN_RegisteType != "07" && Utility.Check.CompareUnitCode(erJian.ENT_OrganizationsCode, RadTextBoxENT_OrganizationsCode.Text) == false)
            {
                UIHelp.layerAlert(Page, string.Format("您已经在{0}注册过建造师证书，个人建造师与造价工程师必须注册在同一家企业，你选择的企业无法进行注册。", erJian.ENT_Name), 5, 0);
                return;
            }

            if (UnitDAL.CheckGongShang(RadTextBoxENT_OrganizationsCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxENT_Name.Text));
                return;
            }

            if (ViewState["zjs_ApplyMDL"] == null)
            {
                if (zjs_ApplyDAL.SelectCount(string.Format(" and PSN_CertificateNO='{0}' and PSN_RegisteProfession='{3}' and (ApplyStatus='{1}' or ApplyStatus='{2}')", RadTextBoxPSN_CertificateNO.Text.Trim(), EnumManager.ZJSApplyStatus.未申报, EnumManager.ZJSApplyStatus.待确认, LabelPSN_RegisteProfession.Text)) > 0)
                {
                    UIHelp.layerAlert(Page, "已经存在申请单，不能重复提交申请！");
                    return;
                }
            }

            //初始注册申请
            zjs_ApplyFirstMDL o = ViewState["zjs_ApplyFirstMDL"] == null ? new zjs_ApplyFirstMDL() : (zjs_ApplyFirstMDL)ViewState["zjs_ApplyFirstMDL"];//详细表
            UIHelp.GetData(AddTable, o);

            //注册专业
            o.ApplyRegisteProfession = LabelPSN_RegisteProfession.Text;

            //申请信息主表
            zjs_ApplyMDL a = ViewState["zjs_ApplyMDL"] == null ? new zjs_ApplyMDL() : (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            UIHelp.GetData(AddTable, a);

            o.PSN_ExamCertCode = LabelZSBH.Text;

            a.XGR = UserName;
            a.XGSJ = DateTime.Now;
            a.Valid = 1;
            a.ApplyType = EnumManager.ZJSApplyType.初始注册;
            a.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
            a.PSN_RegisteProfession = LabelPSN_RegisteProfession.Text;
            //a.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);

            //UnitMDL u = null; //企业信息
            UnitMDL u = DataAccess.UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxENT_OrganizationsCode.Text.Trim());
            GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(u.CreditCode);//工商记录
            string _ENT_Name = "";
            string _ENT_City = "";
            if (ViewState["zjs_ApplyMDL"] == null)//new
            {
                if (Utility.Check.CheckIfSubUnit(u.ENT_Name) == true)
                {
                    UIHelp.layerAlert(Page, UIHelp.Tip_SubUnitForbid, 5, 0);
                    return;
                }

                //所属区县
                if (string.IsNullOrEmpty(u.ENT_City))
                {
                    UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县", 5, 0);
                    return;
                }

                //企业工商验证：对注册二级造价工程师聘用企业要求无限放宽，只要企业通过工商信息验证就能注册造价工程师。
                if (u.ResultGSXX == 0)//企业尚未对比工商信息
                {                    
                    if (g != null)//更新验证状态
                    {
                        u.ResultGSXX = 2;
                        u.ApplyTimeGSXX = DateTime.Now;
                        DataAccess.UnitDAL.UpdateResultGSXX(u);
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "所选企业尚未通过工商信息验证，请通知企业登录本系统，发起工商信息验证。验证通过的企业才允许注册二级造价工程师。", 5, 0);
                        return;
                    }
                }
                else if (u.ResultGSXX == 1)//企业工商信息验证未通过
                {
                    UIHelp.layerAlert(Page, "所选企业未通过工商信息验证，请通知企业登录本系统，发起工商信息验证。验证通过的企业才允许注册二级造价工程师。", 5, 0);
                    return;
                }

                _ENT_Name = u.ENT_Name;
                _ENT_City = u.ENT_City;
            }
            else
            {
                _ENT_Name = a.ENT_Name;
                _ENT_City = a.ENT_City;
            }

            ////企业资质
            //jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(Utility.Check.GetZZJGDMFromCreditCode(ZZJGDM_check.Text.Trim()));

            if (g != null)
            {

                if (_ENT_Name.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")")//企业名称变更
                   )
                {

                    UIHelp.layerAlert(Page, string.Format("检测到所注册企业信息与工商信息中企业信息不一致（{0}），请先联系企业办理企业信息变更。",
                        _ENT_Name.Replace("（", "(").Replace("）", ")") != g.ENT_NAME.Replace("（", "(").Replace("）", ")") ? string.Format("企业名称：{0}≠{1}。", _ENT_Name, g.ENT_NAME) : ""), 5, 0);
                    return;
                }
            }

            if (LabelBiXiuFinishCase.Text != "" && LabelBiXiuFinishCase.Text != "已达标")
            {
                UIHelp.layerAlert(Page, LabelBiXiuFinishCase.Text, 5, 0);
                return;
            }

            ////读取是否存本专业注销的证书
            //zjs_CertificateMDL oldCert = zjs_CertificateDAL.GetObjectByPSN_CertificateNOAndPSN_RegisteProfession(WorkerCertificateCode, LabelPSN_RegisteProfession.Text);

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["zjs_ApplyMDL"] == null)//new
                {
                    a.ApplyID = Guid.NewGuid().ToString();
                    a.ApplyCode = zjs_ApplyDAL.GetNextApplyCode(tran, "初始注册");
                    a.CJR = UserName;
                    a.CJSJ = a.XGSJ;
                    a.ApplyTime = a.XGSJ;
                    a.ENT_Name = u.ENT_Name;
                    a.ENT_OrganizationsCode = ZZJGDM_check.Text.Trim();
                    a.ENT_City = u.ENT_City;
                    //a.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                    //if (RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                    //{
                    //    a.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                    //}
                    a.PSN_CertificateType = (a.PSN_CertificateNO.Length >= 15) ? "身份证" : "其他";
                    if (Old_PSN_RegisterNo != "")//重复注册
                    {
                        a.PSN_RegisterNo = Old_PSN_RegisterNo;//记录原注册编号，公告后不用重新编号
                    }

                    DataAccess.zjs_ApplyDAL.Insert(tran, a);

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

                    //插入初次申请信息
                    DataAccess.zjs_ApplyFirstDAL.Insert(tran, o);


                    if (string.IsNullOrEmpty(Old_PSN_RegisterNo) == false)
                    {

                        //拷贝人员当前有效附件到申请表
                        List<string> filetype = new List<string>();
                        filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                        filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                        //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);                   
                        ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, Old_PSN_RegisterNo, a.ApplyID, filetype);
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
                    zjs_ApplyDAL.Update(tran, a);
                    zjs_ApplyFirstDAL.Update(tran, o);

                }

                tran.Commit();

                LabelApplyCode.Text = a.ApplyCode;
                ViewState["zjs_ApplyMDL"] = a;
                ViewState["zjs_ApplyFirstMDL"] = o;

                //附件照片
                BindFile(a.ApplyID);
                RefreshRadGridXuanXiu();
                SetButtonEnable(a);

                UIHelp.WriteOperateLog(UserName, UserID, "保存造价工程师初始注册申请成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text));
                UIHelp.layerAlert(Page, "保存成功，请上传必要的扫描件后进行提交！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）");

                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存二级造价工程师初始注册申请表失败！", ex);
                return;
            }

        }

        //提交单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
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
            if (Utility.Check.CheckBirthdayLimit(-1, RadTextBoxPSN_CertificateNO.Text.Trim(), DateTime.Now, "男") == true)
            {
                UIHelp.layerAlert(Page, "保存失败，申请人年满70周岁前90天起,不再允许发起二级造价工程师初始、延续、执业企业变更注册申请。", 5, 0);
                return;
            }
            if (RadComboBoxXueLi.SelectedValue != "")
            {
                if ("博士后，博士，研究生，硕士，本科，大专".Contains(RadComboBoxXueLi.SelectedValue) == false)
                {
                    UIHelp.layerAlert(Page, "保存失败，考生报考二级造价工程师职业资格考试时，应具备工程经济、工程技术、工程管理类的大学专科及以上学历或学士及以上学位。", 5, 0);
                    return;
                }
            }

            string imgPath = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == false)
            {
                UIHelp.layerAlert(Page, "造价工程师电子证书需要您上传个人手写签名照片，请您先到“我的信息》个人信息维护”页面上传后再申请注册。");
                return;
            }
            else
            {
                WorkerOB ob = WorkerDAL.GetUserObject(WorkerCertificateCode);
                if (ob.SignPhotoTime.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "您上传个人手写签名照片尚未提交，请您先到“我的信息》个人信息维护”页面提交签名后再申请注册。");
                    return;
                }
            }
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            zjs_ApplyFirstMDL _zjs_ApplyFirstMDL = (zjs_ApplyFirstMDL)ViewState["zjs_ApplyFirstMDL"];

            try
            {
                if (ButtonUnit.Text != "取消申报")//提交单位确认
                {
                    if (LabelXuanXiuFinishCase.Text != "" && LabelXuanXiuFinishCase.Text != "已达标")
                    {
                        UIHelp.layerAlert(Page, LabelXuanXiuFinishCase.Text, 5, 0);
                        return;
                    }
                    if (LabelXuanXiuFinishCase.Text != "" && LabelXuanXiuFinishCase.Text == "已达标")
                    {
                        //选修课扫描件
                        if (ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.继续教育证明扫描件) == false)
                        {
                            UIHelp.layerAlert(Page, "请在线填写继续教育（选修课）培训记录，单位盖章后扫描上传。");
                            return;
                        }
                    }
                    #region 必须上传附件集合

                    //必须上传附件集合
                    System.Collections.Hashtable fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.一寸免冠照片,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0},  
                         //{EnumManager.FileDataTypeName.学历证书扫描件,0},
                        {EnumManager.FileDataTypeName.劳动合同扫描件,0},
                        {EnumManager.FileDataTypeName.申请表扫描件,0}};



                    ////校验继续教育情况，是否满足最近四年继续教育情况
                    //if (_zjs_ApplyFirstMDL.ConferDate.Value.AddYears(4) < DateTime.Now)
                    //{
                    //    fj.Add(EnumManager.FileDataTypeName.继续教育承诺书扫描件, 0);
                    //}

                    //已上传附件集合
                    DataTable dt = ApplyFileDAL.GetListByApplyID(_zjs_ApplyMDL.ApplyID);

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

                    string facePhotoUrl = ApplyFileDAL.SelectFaceImgPathByApplyID(_zjs_ApplyMDL.ApplyID);
                    if (string.IsNullOrEmpty(facePhotoUrl) == false)
                    {
                        bool ifWhiteFacePhoto = UIHelp.CheckIfWhiteBackgroudPhoto(Server.MapPath(facePhotoUrl));
                        if (ifWhiteFacePhoto == false)
                        {
                            UIHelp.layerAlert(Page, string.Format("电子证书要求提供近期一寸白底标准正面免冠证件照，请检查照片是否为白底照。", sb), 5, 0);
                            return;
                        }
                    }

                    #endregion 必须上传附件集合
                }

                _zjs_ApplyMDL.XGR = UserName;
                _zjs_ApplyMDL.XGSJ = DateTime.Now;
                if (ButtonUnit.Text == "取消申报")
                {
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
                    //_zjs_ApplyMDL.ApplyTime = null;

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
                BindFile(_zjs_ApplyMDL.ApplyID);//附件
                BindCheckHistory(_zjs_ApplyMDL.ApplyID);

                if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "提交二级造价工程师初始注册申请成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    //RadDatePickerENT_ContractStartTime.Enabled = false;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = false;//劳动合同结束时间
                    UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();window.parent.location.href='zjsApplyHistory.aspx?c={2}';", Utility.Cryptography.Encrypt("applyZJS"), Utility.Cryptography.Encrypt(_zjs_ApplyMDL.ApplyID.ToString()), _zjs_ApplyMDL.ApplyID));

                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "撤销二级造价工程师初始注册申请成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "撤销成功！", 6, 0, "var isfresh=true;");
                    //RadDatePickerENT_ContractStartTime.Enabled = true;//劳动合同开始时间
                    //RadDatePickerENT_ContractENDTime.Enabled = true;//劳动合同结束时间
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "二级造价工程师初始注册提交失败！", ex);
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //企业确认
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
                        _zjs_ApplyMDL.LastBackResult = string.Format("{0}企业驳回申请，驳回说明：{1}", _zjs_ApplyMDL.XGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxOldUnitCheckRemark.Text.Trim());//现单位审核意见;
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
                BindCheckHistory(_zjs_ApplyMDL.ApplyID);

                if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "二级造价工程师初始注册申请申报成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "二级造价工程师初始注册申请企业退回个人", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
                    UIHelp.layerAlert(Page, "退回个人成功！", 6, 2000);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "二级造价工程师初始注册申报失败！", ex);
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
            UIHelp.WriteOperateLog(UserName, UserID, "删除二级造价工程师初始注册申请成功", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
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
            step_已放号.Attributes["class"] = step_已公告.Attributes["class"].Replace(" green", "");
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
                    zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"] == null ? null : (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];

                    if (_zjs_ApplyMDL != null && string.IsNullOrEmpty(_zjs_ApplyMDL.PSN_RegisterNo) == false)
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

                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "确 定";

                    if (IfExistRoleID("2") == true)//企业
                    {
                        TextBoxOldUnitCheckRemark.Text = "提交审核";
                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                        divUnit.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
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
                        divUnit.Visible = true;
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

                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "确 定";
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
                zjs_ApplyFirstMDL a = ViewState["zjs_ApplyFirstMDL"] as zjs_ApplyFirstMDL;
                DateTime baseDate = a.ConferDate.Value;//资格证书签发日期

                DateTime applyDate = (ViewState["zjs_ApplyMDL"] == null ? DateTime.Now : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyTime.Value);//申请时间

                //签发日期距今月份数
                int monthSpan = ((applyDate.Year - baseDate.Year) * 12) + applyDate.Month - baseDate.Month + (baseDate.Day >= applyDate.Day ? 0 : 1);

                int yearSpan = monthSpan / 12;//逾期年度
                if (yearSpan > 4) yearSpan = 4;//超过4年按4年计算

                if (monthSpan > 18 || IsFirstRegistration ==false)//18个月内不需要继续教育学时
                {
                    trXuanXiuDetail.Visible = true;
                }

                if (o.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || o.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)
                {
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                    RadGridXuanXiu.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    RadGridXuanXiu.Rebind();
                }
                else
                {
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Edit").Visible = false;
                    RadGridXuanXiu.MasterTableView.Columns.FindByUniqueName("Delete").Visible = false;
                    RadGridXuanXiu.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    RadGridXuanXiu.Rebind();
                }
            }
            else
            {
                trXuanXiuDetail.Visible = false;
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
                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师初始注册申请表.docx");
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
                string fileName = string.Format("北京市二级造价工程师初始注册申请表_{0}", _zjs_ApplyMDL.PSN_Name);
                zjs_ApplyFirstMDL _zjs_ApplyFirstMDL = ViewState["zjs_ApplyFirstMDL"] as zjs_ApplyFirstMDL;
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_zjs_ApplyMDL);
                o.Add(_zjs_ApplyFirstMDL);
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
                //对时间类型进行格式转换
                ht["Birthday"] = _zjs_ApplyMDL.Birthday == null ? "" : ((DateTime)_zjs_ApplyMDL.Birthday).ToString("yyyy年MM月dd日");
                ht["GraduationTime"] = _zjs_ApplyFirstMDL.GraduationTime == null ? "" : ((DateTime)_zjs_ApplyFirstMDL.GraduationTime).ToString("yyyy年MM月dd日");
                ht["ConferDate"] = _zjs_ApplyFirstMDL.ConferDate == null ? "" : ((DateTime)_zjs_ApplyFirstMDL.ConferDate).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractStartTime"] = _zjs_ApplyMDL.ENT_ContractStartTime == null ? "" : ((DateTime)_zjs_ApplyMDL.ENT_ContractStartTime).ToString("yyyy年MM月dd日");
                //ht["ENT_ContractENDTime"] = _zjs_ApplyMDL.ENT_ContractENDTime == null ? "" : ((DateTime)_zjs_ApplyMDL.ENT_ContractENDTime).ToString("yyyy年MM月dd日");

                //switch (_zjs_ApplyMDL.ENT_ContractType)
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

                ht["tableList"] = new List<DataTable> { dt };

                //表格的索引
                ht["tableIndex"] = new List<int> { 2 };
                //行的索引
                ht["insertIndex"] = new List<int> { 1 };
                ht["ContainsHeader"] = new List<bool> { true };
                ht["isCtable"] = true;




                //DataTable dt = new DataTable();
                ////表列明意思分别（每逗号一个列）：考试合格专业类别，考试编号，签发日期，注册专业，必修课时，选修课时
                //dt.Columns.Add("KSHGZYLB");
                //dt.Columns.Add("KSBH");
                //dt.Columns.Add("QFRQ");
                //dt.Columns.Add("ZCZY");
                //dt.Columns.Add("BXKS");
                //dt.Columns.Add("XXKS");
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
                //ht["tableList"] = new List<DataTable> { dt };
                //ht["tableIndex"] = new List<int> { 1 };
                //ht["insertIndex"] = new List<int> { 2 };
                //ht["ContainsHeader"] = new List<bool> { true };
                //ht["isCtable"] = true;

                //builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center; //垂直居中对齐

                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印初始申请Word失败！", ex);
            }
        }

        //市级受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;

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

            _zjs_ApplyMDL.GetDateTime = DateTime.Now;
            _zjs_ApplyMDL.GetMan = UserName;
            _zjs_ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _zjs_ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _zjs_ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ZJSApplyStatus.已受理 : EnumManager.ZJSApplyStatus.已驳回;
            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                _zjs_ApplyMDL.LastBackResult = string.Format("{0}市级受理驳回申请，驳回说明：{1}", _zjs_ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }
            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理造价工程师初始注册失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "受理造价工程师初始注册", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text
                 , _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark));
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
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已审核;

            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "审核造价工程师初始注册失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "审核造价工程师初始注册", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。审批结果：{3}，审批意见：{4}。", _zjs_ApplyMDL.PSN_Name, _zjs_ApplyMDL.PSN_CertificateNO, LabelPSN_RegisteProfession.Text
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
            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_zjs_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;
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
                UIHelp.WriteErrorLog(Page, "批准造价工程师初始注册失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "批准造价工程师初始注册", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text));
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
                   , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _zjs_ApplyMDL.GetDateTime, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _zjs_ApplyMDL.ExamineDatetime, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                           , RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, _zjs_ApplyMDL.ConfirmDate, _zjs_ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
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
                UIHelp.WriteErrorLog(Page, "后退造价工程师初始注册审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师初始注册审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }

        //获取表格勾选集合,并返回一个资格库集合
        private List<zjs_QualificationMDL> ListQualificationMDL()
        {
            List<zjs_QualificationMDL> _ListQualificationMDL = new List<zjs_QualificationMDL>();
            for (int i = 0; i < RadGridHZSB.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridHZSB.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    zjs_QualificationMDL _QualificationMDL = new zjs_QualificationMDL();
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
                UIHelp.WriteErrorLog(Page, "删除造价工程师初始注册申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师初始注册申请表附件", string.Format("姓名：{0}，身份证号：{1}，注册专业：{2}，附件ID：{3}。", RadTextBoxPSN_Name.Text, RadTextBoxPSN_CertificateNO.Text, LabelPSN_RegisteProfession.Text, FileID));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        ////选择合同类型
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

        //格式化图片附件
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
               , UIHelp.ShowFile(atrt[0])
               , atrt[1]
               ));
            }
            return sb.ToString();
        }

        //        //载入测试附件************************************************
        //        protected void ButtonLoadFile_Click(object sender, EventArgs e)
        //        {
        //            CommonDAL.ExecSQL(string.Format(@"
        //INSERT INTO [dbo].[ApplyFile]([FileID],[ApplyID],[CheckResult],[CheckDesc])
        //SELECT [FileID],'{0}',[CheckResult],[CheckDesc] FROM [dbo].[ApplyFile]where [ApplyID]='50b18119-26f4-4cfc-8838-fdd777a10851';", ApplyID));

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
                    _JxjyBaseMDL.PostName = LabelPSN_RegisteProfession.Text.Trim();
                    _JxjyBaseMDL.CertificateCode = LabelZSBH.Text.Trim();
                    _JxjyBaseMDL.ValidEndDate = Convert.ToDateTime(LabelConferDate.Text);
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
            UIHelp.WriteOperateLog(PersonName, UserID, "添加二造继续教育选修课记录", string.Format("管理号：{0}。", LabelZSBH.Text));
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
            UIHelp.WriteOperateLog(PersonName, UserID, "更新二造继续教育选修课记录", string.Format("管理号：{0}。", LabelZSBH.Text));
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
            UIHelp.WriteOperateLog(PersonName, UserID, "删除二造继续教育选修课记录", string.Format("管理号：{0}。", LabelZSBH.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", trXuanXiuDetail.ClientID), true);
        }

        //提供二造继续教育选修课记录
        protected void RadGridXuanXiu_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshRadGridXuanXiu();
        }

        /// <summary>
        /// 绑定二造继续教育选修课记录
        /// </summary>
        protected void RefreshRadGridXuanXiu()
        {
            //规则：
            //当人员初次注册证书时，职业资格证书自批准之日起 18 个月后，首次申请初始注册的北京市二级造价工程师，应提供自申请注册之日起算.近1年15学时的继续教育学习证明
            //二级造价工程是初始注册、续期读取公益培训记录，考试资格证书取得年份过期1年需要15学时、2年需要30学习，3年需要45学时，4年及以上需要60学时。(注意：必须每年都要达到15学时)

            try
            {
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;

                DataTable dt = JxjyDetailDAL.GetList(-1, _zjs_ApplyMDL.ApplyID);//继续教育委培记录
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

                DateTime baseDate = Convert.ToDateTime(LabelConferDate.Text);//资格证书签发日期
                DateTime applyDate = (ViewState["zjs_ApplyMDL"] == null ? DateTime.Now : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyTime.Value);//申请时间

                //签发日期距今月份数
                int monthSpan = ((applyDate.Year - baseDate.Year) * 12) + applyDate.Month - baseDate.Month + (baseDate.Day >= applyDate.Day ? 0 : 1);
                int yearSpan = monthSpan / 12;//逾期年度

                if (yearSpan > 4) //超过4年按4年计算
                {
                    yearSpan = 4;
                }
                if (IsFirstRegistration == true)//首次注册,最多需要提供1年的继续教育学时（逾期超过18个月）
                {
                    yearSpan = 1;
                }

                if (monthSpan > 18 || IsFirstRegistration == false)
                {
                    zjs_ApplyMDL o = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];
                    DateTime checkdate = (o.ApplyTime.HasValue == false ? o.XGSJ.Value : o.ApplyTime.Value);

                    //if (JxjyDetailDAL.CheckXuanXiuPeriod(yearSpan, needPeriod, checkdate, _zjs_ApplyMDL.ApplyID, -1) == false)
                    //{
                    //    LabelXuanXiuFinishCase.Text = string.Format("未达标，选修课尚未满足近{0}年，每年{1}学时。", yearSpan, needPeriod);
                    //}
                    if (JxjyDetailDAL.CheckXuanXiuSumPeriod(yearSpan, needPeriod, checkdate, _zjs_ApplyMDL.ApplyID, -1) == false)
                    {
                        LabelXuanXiuFinishCase.Text = string.Format("未达标，选修课尚未满足近{0}年{1}学时。", yearSpan, yearSpan * needPeriod);
                    }
                    else
                    {
                        if (needPeriod == 0)
                        {
                            LabelXuanXiuFinishCase.Text = "";
                        }
                        else
                        {
                            LabelXuanXiuFinishCase.Text = "已达标";
                        }
                    }
                }
                else
                {
                    LabelXuanXiu.Text = "";
                    LabelXuanXiuFinishCase.Text = "";
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取继续教育选修课记录失败！", ex);
                return;
            }
        }

        ///// <summary>
        ///// 检查继续教育选修课是否满足要求
        ///// </summary>
        ///// <param name="YearSpan">检查近几年的选修课</param>
        ///// <param name="Period">每年要求的选修课学时</param>
        ///// <param name="checkTime">业务检查时点</param>
        ///// <param name="dtXuanXiu">继续教育年度选修记录TrainYear，period</param>
        ///// <returns>符合返回true，否则返回false</returns>
        //protected bool CheckXuanXiuPeriod(int YearSpan, int Period, DateTime checkTime)
        //{
        //    int finishYear = 0;//满足年数
        //    int startYear = checkTime.AddYears(-YearSpan).Year;//学习开始年度

        //    DataTable dt = JxjyDetailDAL.GetSumPeriodGroupByYear(-1, (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyID);//继续教育委培记录

        //    foreach (DataRow r in dt.Rows)
        //    {
        //        if (Convert.ToInt32(r["TrainYear"]) >= startYear && Convert.ToInt32(r["period"]) >= Period)
        //        {
        //            finishYear++;
        //            if (finishYear >= YearSpan)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        /// 显示必修课（公益培训记录）
        /// </summary>
        protected void ShowFinishGYPX()
        {
            //规则：
            //当人员初次注册证书时，职业资格证书自批准之日起 18 个月后，首次申请初始注册的北京市二级造价工程师，应提供自申请注册之日起算.近1年15学时的继续教育学习证明
            //二级造价工程是初始注册、续期读取公益培训记录，考试资格证书取得年份过期1年需要15学时、2年需要30学习，3年需要45学时，4年及以上需要60学时。

            try
            {
                DateTime applyDate;
                if (ViewState["zjs_ApplyMDL"] == null)
                {
                    applyDate = DateTime.Now;
                }
                else
                {
                    zjs_ApplyMDL _zjs_ApplyMDL = (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL);

                    if (PersonType == 2 && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回))
                    {
                        applyDate = DateTime.Now;
                    }
                    else
                    {
                        applyDate = _zjs_ApplyMDL.ApplyTime.Value;//申请时间
                    }
                }

                int needPeriod = _PackageMDL.Period.Value / 45;//每年应当完成学时数(2023-10-01后实施)
                //int needPeriod = 0;//临时

                DateTime baseDate = Convert.ToDateTime(LabelConferDate.Text);//资格证书签发日期

                //签发日期距今月份数
                int monthSpan = ((applyDate.Year - baseDate.Year) * 12) + applyDate.Month - baseDate.Month + (baseDate.Day >= applyDate.Day?0:1);
                int yearSpan = monthSpan / 12;//逾期年度
                
                if (yearSpan > 4) //超过4年按4年计算
                {
                    yearSpan = 4;
                }
                if (IsFirstRegistration == true)//首次注册,最多需要提供1年的继续教育学时（逾期超过18个月）
                {
                    yearSpan = 1;
                }

                if (IsFirstRegistration == true && monthSpan < 19)//首次注册，18个月内不需要继续教育学时
                {
                    LabelBiXiu.Text = "";
                    LabelXuanXiu.Text = "";
                }
                else
                {
                    //本人已经完成的学时数
                    decimal myPeriod = FinishSourceWareDAL.GetFinisthPeriod(RadTextBoxPSN_CertificateNO.Text.Trim(), "二级造价工程师", LabelPSN_RegisteProfession.Text.Trim(), applyDate.AddYears(-yearSpan), applyDate);

                    if (myPeriod >= (needPeriod * yearSpan))
                    {
                        if (needPeriod == 0)
                        {
                            LabelBiXiu.Text = "";
                            LabelBiXiuFinishCase.Text = "";
                        }
                        else
                        {
                            LabelBiXiu.Text = (needPeriod * yearSpan).ToString();
                            LabelBiXiuFinishCase.Text = "已达标";
                        }
                    }
                    else
                    {
                        LabelBiXiu.Text = Convert.ToInt32(myPeriod).ToString();
                        LabelBiXiuFinishCase.Text = string.Format("未达标：请首先完成{0}学时逾期注册续教育培训，再申请注册！点击“公益教育培训 - 我的培训 - 个人空间”选择证书对应课程学习。", needPeriod * yearSpan);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取继续教育必修课记录失败！", ex);
                return;
            }
        }
    }
}