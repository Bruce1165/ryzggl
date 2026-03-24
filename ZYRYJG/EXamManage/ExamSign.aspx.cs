using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSign : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ExamSignUpOB"] == null ? "" : string.Format("KS-{0}", (ViewState["ExamSignUpOB"] as ExamSignUpOB).ExamSignUpID); }
        }

        /// <summary>
        /// 报名人员是否为报名企业法人
        /// </summary>
        protected bool IfFaRen
        {
            get{
                if(ViewState["IfFaRen"] == null)
                {
                    ViewState["IfFaRen"] =CertificateDAL.IFExistFRByUnitCode(RadTextBoxUnitCode.Text.Trim(), RadTextBoxWorkerName.Text.Trim());                    
                }
                return Convert.ToBoolean(ViewState["IfFaRen"]);
            }
        }

        /// <summary>
        /// 比对社保局社保是否合格
        /// </summary>
        protected bool SheBaoCheckResult
        {
            get
            {
                if (ViewState["SheBaoCheckResult"] == null)
                {
                    if (ViewState["ExamSignUpOB"] == null)
                    {
                        ViewState["SheBaoCheckResult"] = false;
                        return false;
                    }
                    ExamSignUpOB o = ViewState["ExamSignUpOB"] as ExamSignUpOB;
                    if (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                    {
                        ViewState["SheBaoCheckResult"] = false;
                    }
                    else
                    {
                        ViewState["SheBaoCheckResult"] = true;
                    }
                }
                return Convert.ToBoolean(ViewState["SheBaoCheckResult"]);
            }
        }

        //显示社保比对结果
        private void ShowSheBao(ExamSignUpOB o)
        {
            ExamPlanOB _explanOB = ViewState["ExamPlanOB"] as ExamPlanOB;

            //检查社保相关扫描件上传文件是否存在
            bool SheBaoCheckFile = ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.社保扫描件);
            string tip = "";

            if (
                (_explanOB.PostID.Value == 147//企业主要负责人
                 && IfFaRen == false)//非法定代表人提交大专以上学历、中级以上职称各1份；
                || _explanOB.PostID.Value == 148 //项目负责人
                || (_explanOB.PostID.Value == 6 || _explanOB.PostID.Value == 1123 || _explanOB.PostID.Value == 1125)//安全员C1、C2、C3
                )
            {
                if (SheBaoCheckFile == false //没上传社保证明
                    && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                    )
                {
                    tip = "<p style='color:red'>重要提示：您的申请单“社保校验”、“上传社保扫描件”两项都不符合要求。<br />请在保存申请单次日查看社保自动验核结果，比对合格再上报单位审核，不符合必须在报名截止之前上传其他相关证明材料。</p>";
                }
            }

            divSheBao.InnerHtml = string.Format("{4}<b>社保校验：</b><span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", o.CertificateCode, o.UnitCode, o.SignUpDate
                , o.SheBaoCheck.HasValue == false ? "尚未比对（夜间比对）" : o.SheBaoCheck.Value == 1 ? "符合" : "不符合"
                , tip
                );
        }

        /// <summary>
        /// 显示质量安全网持证规则校验结果
        /// </summary>
        protected void ShowZACheckResult()
        {
            string PostTypeID = string.Format("{0}", ViewState["PostTypeID"]);
            if (PostTypeID != "1" && PostTypeID != "2")//三类人和特种作业才做质安网持证检查
            {
                return;
            }
            if (ViewState["ExamSignUpOB"] != null)
            {
                ExamSignUpOB _ExamSignUpOB = (ExamSignUpOB)ViewState["ExamSignUpOB"];
               
                if (_ExamSignUpOB.ZACheckTime.HasValue == false)
                {
                    divZACheckResult.InnerHtml = "<b>持证校验：</b>尚未比对";
                }
                else if (_ExamSignUpOB.ZACheckResult == 0)
                {
                    divZACheckResult.InnerHtml = string.Format(@"<div><b>持证校验：</b><span style='color:red'>未通过。</span></div>
                <div style='padding-left: 32px'>报名申请没有通过<a href='https://zlaq.mohurd.gov.cn/fwmh/bjxcjgl/fwmh/pages/default/index.html' target='_blank'>【全国工程质量安全监管信息平台（可按身份证查询ABC持证情况）】</a>数据校验，属于违规申请。<br/>
                                                                    请先办理相关证书注销后，才能报名。（若外省证书已经注销，请联系原证书省份，查询数据是否已经同步到全国工程质量安全监管信息平台。）<br/><br/>
                                                                    <b>校验结果说明：</b><span style='color:red'>{0}</span><br/><br/>
                                                                    <b>持证规则说明：</b><br/>
                                                                    <div style='padding-left: 32px'>
                                                                        <b>A证持证要求：</b><br/> 
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人有多本A证时，多本A证在不同企业下，其中最多存在一本非法人A证，其余A证只能以法人A证的形式存在；<br/>
                                                                            > 持证人同时持有A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业；<br/>
                                                                            > 一个企业只能存在一本法人A证。（法人变更需要变更证书上职务）<br/><br/>
                                                                        </div>
                                                                        <b>B证持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人在全国范围只允许持有一本B证；<br/>
                                                                            > 持有已注册的建造师且建造师注册单位与B本单位一致；<a href='https://jzsc.mohurd.gov.cn/data/person' target='_blank'>【全国建筑市场监管公共服务平台（可按身份证查询建造师持证情况）】</a><br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C1/C2持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证号码一致），在全国范围内只允许持有一本C1或C2证，同时持有C1和C2证的，应办理C1、C2证合并C3证业务；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C3持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证号码一致），在全国范围内只允许持有一本C3证，不能同时持有C1或C2证；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下；<br/>                                                                     
                                                                        </div>
                                                                    </div>
                                                                  </div>", _ExamSignUpOB.ZACheckRemark);
                }
                //if (_ExamSignUpOB.ZACheckResult == 2)
                //{
                //    divZACheckResult.InnerText = string.Format("持证校验：通过，但有预警，{0}", _ExamSignUpOB.ZACheckRemark);
                //}
                else
                {
                    divZACheckResult.InnerHtml = "<b>持证校验：</b>通过";
                }
            }
            else
            {
                divZACheckResult.InnerHtml = "<b>持证校验：</b>尚未比对";
            }
        }
        /// <summary>
        /// 清空法人检查结果和社保检测结果
        /// </summary>
        protected void ClearCheckFaRenOrSheBao()
        {
            ViewState["IfFaRen"] = null;
            ViewState["SheBaoCheckResult"] = null;
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamSignList.aspx";
            }
        }

        protected ExamSignUpOB myExamSignUpOB
        {
            get
            {
                if (ViewState["ExamSignUpOB"] != null)
                    return (ExamSignUpOB)ViewState["ExamSignUpOB"];
                else
                    return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 页面正常加载

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                UIHelp.FillDropDownList(RadComboBoxCertificateType, "102");//证件类型
                UIHelp.FillDropDownList(RadComboBoxNation, "108", "请选择", "");//民族
                UIHelp.FillDropDownList(RadComboBoxCulturalLevel, "109", "请选择", "");//学历
                UIHelp.FillDropDownList(RadComboBoxPoliticalBackground, "110", "请选择", "");//政治面貌       
                UIHelp.FillDropDownList(RadComboBoxSKILLLEVEL, "111", "请选择", "");//技术职称或技术等级     

                ImgCode.Src = "~/Images/photo_ry.jpg";

                //考试计划信息
                long ExamPlanID = Convert.ToInt32(Utility.Cryptography.Decrypt(Request.QueryString["o"]));
                ViewState["ExamPlanID"] = ExamPlanID;

                ExamPlanOB explanOB = ExamPlanDAL.GetObject(ExamPlanID);
                if (explanOB.PostTypeID==1)//三类人员显示职务
                {
                    UIHelp.FillDropDownList(RadComboBoxJob, "112", "请选择", "");//职务 
                    RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("其他"));
                    trJob.Visible = true;
                    switch (explanOB.PostID)
                    {
                        case 147://企业主要负责人 
                            RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("项目负责人（项目经理）"));
                            RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("专职安全生产管理人员"));
                            break;
                        case 148://项目负责人 
                            RadComboBoxJob.Items.FindItemByText("项目负责人（项目经理）").Selected = true;
                            RadComboBoxJob.Enabled = false;
                            break;
                        default://安全员
                            RadComboBoxJob.Items.FindItemByText("专职安全生产管理人员").Selected = true;
                            RadComboBoxJob.Enabled = false;
                            break;
                    }

                    divPostType1Tip.Visible = true;
                }
                else
                {
                    trJob.Visible = false;
                }

                if (explanOB.PostTypeID == 4000)
                {
                    tableTrainUnit.Visible = true;
                    tableUnit.Visible = false;
                    LabelTrainUnit.Text=explanOB.SignUpPlace;
                    divStepDesc.InnerText = "报名流程：个人填写保存报名信息 》 个人提交培训点审核 》 培训点分配准考证  》 个人参加考试";

                    divSignupDesc2.Style.Add("display", "inline");
                    divSignupDesc1.Style.Add("display", "none");
                    LabelSingupTableDesc.Text = "（要求：请在本页面导出打印报名表，个人签字后扫描上传）";

                    trDataChcekRow.Visible = false;
                    trDataCheckHead.Visible = false;
                }

                #region 考试计划信息
                ViewState["ExamPlanOB"] = explanOB;
                ViewState["PostTypeID"] = explanOB.PostTypeID;//岗位类别ID
                ViewState["PostID"] = explanOB.PostID;//岗位工种ID
                ViewState["ExamStartDateYM"] = explanOB.ExamStartDate.Value.ToString("yyyyMM");

                ExamStartDate.Text = explanOB.ExamStartDate.Value.ToString("yyyy.MM.dd");
                ExamEndDate.Text = explanOB.ExamEndDate.Value.ToString("yyyy.MM.dd");
                ExamCardSendStartDate.Text = explanOB.ExamCardSendStartDate.Value.ToString("yyyy.MM.dd");
                ExamCardSendEndDate.Text = explanOB.ExamCardSendEndDate.Value.ToString("yyyy.MM.dd");
                SignUpEndDate.Text = explanOB.SignUpEndDate.Value.ToString("yyyy.MM.dd");
                SignUpStartDate.Text = explanOB.SignUpStartDate.Value.ToString("yyyy.MM.dd");
                LatestCheckDate.Text = string.Format("{0}~ {1}"
              , explanOB.StartCheckDate.HasValue == true ? explanOB.StartCheckDate.Value.ToString("yyyy.MM.dd") : explanOB.SignUpEndDate.Value.ToString("yyyy.MM.dd")
              , explanOB.LatestCheckDate.Value.ToString("yyyy.MM.dd"));
                #endregion 考试计划信息

                #region 设置应上传的附件类型

                switch (explanOB.PostTypeID.Value)
                {
                    case 1://三类人
                        switch (explanOB.PostID.Value)
                        {
                            case 6://土建安全员                              
                            case 1123://机械安全员 
                            case 1125://综合安全员 
                                p_PostTyppe1_aqy.Visible = true;
                                div_SheBao.Visible = true;
                                //divSelectCheckType.Visible = true;
                                //div_XueLi.Visible = true;
                                //div_JiShuZhiCheng.Visible = true;

                                divSelectCheckType.Style.Remove("display");
                            div_XueLi.Style.Remove("display");
                            div_JiShuZhiCheng.Style.Remove("display");
                                break;
                            case 147://企业主要负责人 
                                p_PostTyppe1_qyfzr.Visible = true;
                                div_SheBao.Visible = true;

                                //divSelectCheckType.Visible = true;
                                //div_XueLi.Visible = true;
                                //div_JiShuZhiCheng.Visible = true;
                                   divSelectCheckType.Style.Remove("display");
                            div_XueLi.Style.Remove("display");
                            div_JiShuZhiCheng.Style.Remove("display");
                                break;
                            case 148://项目负责人 
                                p_PostTyppe1_xmfzr.Visible = true;
                                div_SheBao.Visible = true;
                                break;
                        }
                        break;
                    case 2://特种作业
                        p_PostTyppe2.Visible = true;
                        div_TiJian.Visible = true;
                        //div_XueLi.Visible = true;
                        div_XueLi.Style.Remove("display");
                        div_grjkcn.Visible = true;
                        div_examsafetrain.Visible = true;
                        break;
                    case 4://职业技能（工人）
                        p_PostTyppe4.Visible = true;
                        div_ShenFenZheng.Visible = false;
                        break;
                    case 4000://职业技能（工人）
                        p_PostTyppe4000.Visible = true;
                        div_ShenFenZheng.Visible = false;
                        break;

                }

                #endregion

                //职业技能岗位考试计划有“技术等级”分类,要求与考试计划一致
                if (explanOB.PostTypeID.Value == 4 || explanOB.PostTypeID.Value == 4000)
                {
                    if (explanOB.PostID == 158//村镇建筑工匠
                       || explanOB.PostID == 199)//普工
                    {
                        RadComboBoxSKILLLEVEL.FindItemByText("无").Selected = true;
                        RadComboBoxSKILLLEVEL.Text = "无";
                    }
                    else
                    {
                        RadComboBoxSKILLLEVEL.FindItemByText(explanOB.PlanSkillLevel).Selected = true;
                        RadComboBoxSKILLLEVEL.Text = explanOB.PlanSkillLevel;  //技术职称或技术等级

                    }
                    RadComboBoxSKILLLEVEL.Enabled = false;
                }

                if (explanOB.PostTypeID.Value == 1)//三类人企业名称只能从资质库读取
                {
                    RadTextBoxUnitName.ReadOnly = true;
                }

                RadTextBoxExamPlanName.Text = explanOB.ExamPlanName;    //考试计划名称       
                RadTextPostID.Text = UIHelp.FormatPostNameByExamplanName(explanOB.PostID.Value, explanOB.PostName, explanOB.ExamPlanName);
                ExamSignUpOB _ExamSignUpOB = null;

                if (PersonType == 2)//考生
                {
                    //考生信息
                    WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);

                    #region //考生信息
                    RadComboBoxCertificateType.FindItemByValue(_WorkerOB.CertificateType).Selected = true;
                    RadComboBoxCertificateType.Text = _WorkerOB.CertificateType;   //证件类别

                    RadTextCertificateCode.Text = _WorkerOB.CertificateCode;  //证件号码

                    RadTextBoxWorkerName.Text = _WorkerOB.WorkerName;    //姓名
                    if (_WorkerOB.Sex == "男")  //性别
                    {
                        RadioButtonMan.Checked = true;
                        RadioButtonWoman.Checked = false;
                    }
                    else
                    {
                        RadioButtonWoman.Checked = true;
                        RadioButtonMan.Checked = false;
                    }


                    System.Random rm = new Random();
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ExamPlanID.ToString(), _WorkerOB.CertificateCode))); //绑定照片;
                    RadDatePickerBirthday.SelectedDate = _WorkerOB.Birthday; //出身日期

                    ////不能修改证件类型和号码
                    //RadTextCertificateCode.Enabled = false;
                    //RadComboBoxCertificateType.Enabled = false;

                    //证件类型为身份证的不能修改生日和性别
                    if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
                    {
                        RadioButtonMan.Enabled = false;
                        RadioButtonWoman.Enabled = false;
                        RadDatePickerBirthday.Enabled = false;
                    }

                    RadTextBoxPhone.Text = _WorkerOB.Phone;   //联系电话
                    if (RadTextBoxPhone.Text != "")
                    {
                        UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话
                    }
                    if (RadComboBoxNation.FindItemByText(_WorkerOB.Nation) != null) //民族
                    {
                        RadComboBoxNation.FindItemByText(_WorkerOB.Nation).Selected = true;
                        RadComboBoxNation.Text = _WorkerOB.Nation;
                    }
                    if (RadComboBoxCulturalLevel.FindItemByText(_WorkerOB.CulturalLevel) != null) //文化程度
                    {
                        RadComboBoxCulturalLevel.FindItemByText(_WorkerOB.CulturalLevel).Selected = true;
                        RadComboBoxCulturalLevel.Text = _WorkerOB.CulturalLevel;
                    }
                    if (RadComboBoxPoliticalBackground.FindItemByText(_WorkerOB.PoliticalBackground) != null) //政治面貌  
                    {
                        RadComboBoxPoliticalBackground.FindItemByText(_WorkerOB.PoliticalBackground).Selected = true;
                        RadComboBoxPoliticalBackground.Text = _WorkerOB.PoliticalBackground;
                    }
                    #endregion //考生信息

                    _ExamSignUpOB = ExamSignUpDAL.GetObject(_WorkerOB.CertificateCode, explanOB.ExamPlanID.Value);
                }

                if (_ExamSignUpOB == null && string.IsNullOrEmpty(Request["s"]) == false)
                {
                    long ExamSignUpID = Convert.ToInt32(Utility.Cryptography.Decrypt(Request.QueryString["s"]));
                    //查看是否已经报过名了
                    _ExamSignUpOB = ExamSignUpDAL.GetObject(ExamSignUpID);
                }
                if (_ExamSignUpOB != null)//修改
                {
                    ViewState["ExamSignUpOB"] = _ExamSignUpOB;
                    if (PersonType != 2)
                    {
                        //考生信息
                        WorkerOB _WorkerOB = WorkerDAL.GetObject(_ExamSignUpOB.WorkerID.Value);

                        #region //考生信息
                        RadComboBoxCertificateType.FindItemByValue(_WorkerOB.CertificateType).Selected = true;
                        RadComboBoxCertificateType.Text = _WorkerOB.CertificateType;   //证件类别

                        RadTextCertificateCode.Text = _WorkerOB.CertificateCode;  //证件号码

                        RadTextBoxWorkerName.Text = _WorkerOB.WorkerName;    //姓名
                        if (_WorkerOB.Sex == "男")  //性别
                        {
                            RadioButtonMan.Checked = true;
                            RadioButtonWoman.Checked = false;
                        }
                        else
                        {
                            RadioButtonWoman.Checked = true;
                            RadioButtonMan.Checked = false;
                        }


                        System.Random rm = new Random();
                        ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ExamPlanID.ToString(), _WorkerOB.CertificateCode))); //绑定照片;
                        RadDatePickerBirthday.SelectedDate = _WorkerOB.Birthday; //出身日期

                        ////不能修改证件类型和号码
                        //RadTextCertificateCode.Enabled = false;
                        //RadComboBoxCertificateType.Enabled = false;

                        //证件类型为身份证的不能修改生日和性别
                        if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
                        {
                            RadioButtonMan.Enabled = false;
                            RadioButtonWoman.Enabled = false;
                            RadDatePickerBirthday.Enabled = false;
                        }

                        RadTextBoxPhone.Text = _WorkerOB.Phone;   //联系电话
                        if (RadTextBoxPhone.Text != "")
                        {
                            UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话
                        }
                        if (RadComboBoxNation.FindItemByText(_WorkerOB.Nation) != null) //民族
                        {
                            RadComboBoxNation.FindItemByText(_WorkerOB.Nation).Selected = true;
                            RadComboBoxNation.Text = _WorkerOB.Nation;
                        }
                        if (RadComboBoxCulturalLevel.FindItemByText(_WorkerOB.CulturalLevel) != null) //文化程度
                        {
                            RadComboBoxCulturalLevel.FindItemByText(_WorkerOB.CulturalLevel).Selected = true;
                            RadComboBoxCulturalLevel.Text = _WorkerOB.CulturalLevel;
                        }
                        if (RadComboBoxPoliticalBackground.FindItemByText(_WorkerOB.PoliticalBackground) != null) //政治面貌  
                        {
                            RadComboBoxPoliticalBackground.FindItemByText(_WorkerOB.PoliticalBackground).Selected = true;
                            RadComboBoxPoliticalBackground.Text = _WorkerOB.PoliticalBackground;
                        }
                        #endregion //考生信息
                    }

                    #region 报名表信息

                    if (explanOB.PostTypeID==2)
                    {
                        if(string.IsNullOrEmpty(_ExamSignUpOB.SafeTrainType)==false)
                        {
                            RefreshRadGridAnQuanPX();

                            RadioButtonListSafeTrainType.SelectedValue = _ExamSignUpOB.SafeTrainType;
                            if (_ExamSignUpOB.SafeTrainType == "委托培训机构")
                            {
                                trTrainUnit1.Visible = true;
                                trTrainUnit2.Visible = true;

                                RadTextBoxSafeTrainUnit.Text = _ExamSignUpOB.SafeTrainUnit;
                                RadTextBoxSafeTrainUnitCode.Text = _ExamSignUpOB.SafeTrainUnitCode;
                                RadDatePickerSafeTrainUnitValidEndDate.SelectedDate = _ExamSignUpOB.SafeTrainUnitValidEndDate;
                                RadTextBoxSafeTrainUnitOfDept.Text = _ExamSignUpOB.SafeTrainUnitOfDept;
                            }
                            else
                            {
                                trTrainUnit1.Visible = false;
                                trTrainUnit2.Visible = false;
                            }
                        }
                    }

                    //报名信息
                    if (_ExamSignUpOB.ENT_ContractType.HasValue)//合同
                    {
                        RadioButtonListENT_ContractType.Items.FindByValue(_ExamSignUpOB.ENT_ContractType.ToString()).Selected = true;
                        RadDatePickerENT_ContractStartTime.SelectedDate = _ExamSignUpOB.ENT_ContractStartTime;
                        RadDatePickerENT_ContractENDTime.SelectedDate = _ExamSignUpOB.ENT_ContractENDTime;
                        if (RadioButtonListENT_ContractType.SelectedValue == "1")
                        {
                            RadDatePickerENT_ContractENDTime.Style.Remove("display");
                            LabelJZSJ.Style.Remove("display");
                            ValidatorENT_ContractENDTime.Enabled = true;
                        }
                        else
                        {
                            RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
                            LabelJZSJ.Style.Add("display", "none");
                            ValidatorENT_ContractENDTime.Enabled = false;
                        }
                    }

                    //if (_ExamSignUpOB.SignupPromise.HasValue)//承诺方式
                    //{
                    //    RadioButtonListSignupPromise.SelectedIndex = RadioButtonListSignupPromise.Items.IndexOf(RadioButtonListSignupPromise.Items.FindByValue(_ExamSignUpOB.SignupPromise.ToString()));
                    //    //if (RadioButtonListSignupPromise.SelectedValue == "0")
                    //    if (_ExamSignUpOB.SignupPromise.ToString() == "0")
                    //    {
                    //        div_bktjzm.Style.Add("display", "none");
                    //        div_XueLi.Style.Remove("display");
                    //        div_JiShuZhiCheng.Style.Remove("display");
                    //    }
                    //    else
                    //    {
                    //        div_bktjzm.Style.Remove("display");
                    //        div_XueLi.Style.Add("display", "none");
                    //        div_JiShuZhiCheng.Style.Add("display", "none");
                    //    }
                    //}

                    RadDatePickerWorkStartDate.SelectedDate = _ExamSignUpOB.WorkStartDate;
                    RadTextBoxWorkYearNumer.Text = _ExamSignUpOB.WorkYearNumer.HasValue ? _ExamSignUpOB.WorkYearNumer.Value.ToString() : "";
                    RadTextBoxPersonDetail.Text = _ExamSignUpOB.PersonDetail;
                    lblSignUpCode.Text = _ExamSignUpOB.SignUpCode;//报名批号

                    RadTextBoxUnitName.Text = _ExamSignUpOB.UnitName;   //机构名称
                    RadTextBoxUnitCode.Text = _ExamSignUpOB.UnitCode;   //机构代码
                    if (RadComboBoxSKILLLEVEL.FindItemByText(_ExamSignUpOB.SKILLLEVEL) != null) //技术职称或技术等级
                    {
                        RadComboBoxSKILLLEVEL.FindItemByText(_ExamSignUpOB.SKILLLEVEL).Selected = true;
                        RadComboBoxSKILLLEVEL.Text = _ExamSignUpOB.SKILLLEVEL;
                    }

                    RadComboBoxCertificateType.Text = _ExamSignUpOB.CertificateType;   //证件类别
                    RadTextCertificateCode.Text = _ExamSignUpOB.CertificateCode;  //证件号码
                    RadTextBoxWorkerName.Text = _ExamSignUpOB.WorkerName;    //姓名     
                    RadDatePickerBirthday.SelectedDate = _ExamSignUpOB.S_BIRTHDAY; //出身日期
                    RadTextBoxPhone.Text = _ExamSignUpOB.S_PHONE;   //联系电话
                    UIHelp.SetReadOnly(RadTextBoxPhone, true);

                    if (RadComboBoxCulturalLevel.FindItemByText(_ExamSignUpOB.S_CULTURALLEVEL) != null) //文化程度
                    {
                        RadComboBoxCulturalLevel.FindItemByText(_ExamSignUpOB.S_CULTURALLEVEL).Selected = true;
                        RadComboBoxCulturalLevel.Text = _ExamSignUpOB.S_CULTURALLEVEL;
                    }

                    HiddenFieldApplyMan.Value = _ExamSignUpOB.SignUpMan;//报名操作人
                    HiddenFieldFirstCheck.Value = _ExamSignUpOB.S_TRAINUNITNAME;//培训点

                    CheckBoxPromise.Checked = true; ;
                    LabelCheckStep.Text = string.Format("当前审核阶段：{0}", formatSignupStatus(_ExamSignUpOB.Status, _ExamSignUpOB.CheckDatePlan));
                    if (_ExamSignUpOB.S_SEX == "男")  //性别
                    {
                        RadioButtonMan.Checked = true;
                        RadioButtonWoman.Checked = false;
                    }
                    else
                    {
                        RadioButtonWoman.Checked = true;
                        RadioButtonMan.Checked = false;
                    }

                    if (string.IsNullOrEmpty(_ExamSignUpOB.Job) == false)
                    {
                        RadComboBoxJob.FindItemByText(_ExamSignUpOB.Job).Selected = true;//职务
                    }

                    ShowSheBao(_ExamSignUpOB);
                    SetButtonEnable(_ExamSignUpOB.Status);
                    BindCheckHistory(_ExamSignUpOB.ExamSignUpID.Value);
                    BindFile(ApplyID);

                    switch (_ExamSignUpOB.Status)
                    {
                        case EnumManager.SignUpStatus.ReturnEdit:
                            //if (explanOB.ExamCardSendStartDate.Value.AddDays(-1) < DateTime.Now)//已经放准考证
                            //{
                            UIHelp.layerAlert(Page, "您的报名已经被退回，请查看审核记录的驳回原因！");
                            //}
                            break;
                        case EnumManager.SignUpStatus.SaveSignUp:

                            if (DateTime.Now > explanOB.SignUpEndDate.Value.AddDays(1))
                            {
                                UIHelp.layerAlert(Page, "您未在规定的报名时间范围内提交报名，本次报名已截止，请您参加下次报名。");
                            }
                            else
                            {
                                if (explanOB.PostTypeID == 4000)
                                {
                                    UIHelp.layerAlert(Page, "您保存的报名表尚未提交培训机构审核，请尽快提，否则超时报名信息作废。", 6, 0);
                                }
                                else
                                {
                                    UIHelp.layerAlert(Page, "您保存的报名表尚未提交单位审核，请尽快提交单位审核，否则超时未企业确认报名信息作废。");
                                }
                            }

                            break;
                        case EnumManager.SignUpStatus.NewSignUp:
                            UIHelp.layerAlert(Page, "您的报名表已经提交单位审核，请通知单位及时审核确认，企业未在规定时间确认提交建委审核的报名信息作废！");
                            break;
                        case EnumManager.SignUpStatus.PayConfirmed:
                            UIHelp.layerAlert(Page, "已审核，请再规定的时间内打印准考证参加考试！");
                            break;
                        default:                           
                            if (PersonType == 2)
                            {
                                try
                                {
                                    DataTable dt = CommonDAL.GetDataTable(string.Format("select * from EXAMSIGNUP_Confirm where [EXAMPLANID]={0} and [CERTIFICATECODE]='{1}'", _ExamSignUpOB.ExamPlanID, _ExamSignUpOB.CertificateCode));
                                    //int count = CommonDAL.GetRowCount("EXAMSIGNUP_Confirm", "*", string.Format(" and [EXAMPLANID]={0} and [CERTIFICATECODE]='{1}'", _ExamSignUpOB.ExamPlanID, _ExamSignUpOB.CertificateCode));
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["ConfirmResult"].ToString()=="1")
                                        {
                                            UIHelp.layerAlert(Page, "已经成功确认参加本次考试，请再规定的时间内打印准考证参加考试！");
                                        }
                                        else
                                        {
                                            UIHelp.layerAlert(Page, "已经成功确认不参加本次考试，可按考试计划重新报考其他月份考核。");
                                        }
                                    }
                                    else
                                    {
                                        UIHelp.layerAlert(Page, "报名已经进入受理阶段，请耐心等待！");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    UIHelp.WriteErrorLog(Page, "读取确认考试记录失败！", ex);
                                    return;
                                }
                            }
                            break;
                    }

                    #endregion 报名表信息

                    SetUploadFileType();
                }
                else//新增
                {
                    SetButtonEnable("");

                    #region 获取最近一次考试报名信息，复用简历等信息

                    try
                    {
                        ExamSignUpOB LastExamSignUpOB = ExamSignUpDAL.GetLastExamSingup(RadTextCertificateCode.Text);
                        if (LastExamSignUpOB != null)
                        {
                            RadTextBoxPersonDetail.Text = UIHelp.ToFullWidth(RadTextBoxPersonDetail.Text);
                           
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.FileLog.WriteLog("复用上次考试报名信息失败！", ex);
                    }

                    #endregion

                    //已报名数量
                    int signUpCount = ExamSignUpDAL.SelectSignupCount(explanOB.ExamPlanID.Value);
                    if (signUpCount >= explanOB.PersonLimit)
                    {
                        UIHelp.layerAlert(Page, "抱歉本期报名人数已达上限，我们将全力以赴，争取尽快满足您的需求，请您下期再报，感谢您的支持和理解。", "window.history.back(-1);");
                        return;
                    }

                    if (PersonType == 1 || PersonType == 6)
                    {
                        RadTextCertificateCode.Enabled = true;
                        RadComboBoxCertificateType.Enabled = true;
                    }

                    if (DateTime.Compare(explanOB.SignUpStartDate.Value.AddDays(-4), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) >= 0)
                    {
                        UIHelp.layerAlert(Page, string.Format("不在报名时间段内（{0} ~ {1}），无法报名！", explanOB.SignUpStartDate.Value.ToString("yyyy年MM月dd日"), explanOB.SignUpEndDate.Value.ToString("yyyy年MM月dd日")));
                    }

                    if (DateTime.Compare(explanOB.SignUpStartDate.Value.AddDays(-4), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) < 0
                       && DateTime.Compare(explanOB.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0)
                    {
                        UIHelp.layerAlert(Page, string.Format("不在报名时间段内（{0} ~ {1}），无法提交报名，但可提前填写报名信息（为方便考生报名，允许考生在考试报名开始前，提前填写报名信息并保存。考生在考试报名开始时，点击“提交报名”即可。）。", explanOB.SignUpStartDate.Value.ToString("yyyy年MM月dd日"), explanOB.SignUpEndDate.Value.ToString("yyyy年MM月dd日")));
                    }
                }

                if (Request["c"] == "cancel")
                {
                    UIHelp.layerAlert(Page, "取消报名成功！");
                }
                else
                {
                    //if (explanOB.PostTypeID.Value == 1//三类人
                    //    || explanOB.PostTypeID.Value == 5)//专业技术员
                    //{
                    //    UIHelp.layerAlert(Page, "考试报名声明<br />考务与证书管理系统中关于考试报名审核点“报名人数上限”的设定，其目的在于为了有效疏导、分流，避免在报名审核时因人数过度集中产生交通拥堵以及影响审核速度。如发生接近报名人数满额的情况，我们将通过系统对考试计划进行适时调整，不会发生考生报不上名的情况，请各位考生放心报名。<br /><br />网上报名成功后，按报名表上规定的时间持报考材料到考试资格审核点现场审核（社保信息比对一致的考生、具有建造师注册证书的考生（报考B本），系统自动审核通过无需到现场审核报名材料）。审核通过的考生，按时登录系统打印准考证、按时参加考试、网上查询成绩和证书号");
                    //}
                    //else
                    //{
                    //    UIHelp.layerAlert(Page, "网上报名成功后，按报名表上规定的时间持报考材料到考试资格审核点现场审核（社保信息比对一致的考生、具有建造师注册证书的考生（报考B本），系统自动审核通过无需到现场审核报名材料）。审核通过的考生，按时登录务与证书管理信息系统打印准考证、按时参加考试、网上查询成绩和证书号");
                    //}
                }

                #endregion 页面正常加载

            }
            else if (Request["__EVENTTARGET"] == "refreshFile")//上传或删除附件刷新列表
            {
                BindFile(ApplyID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", btnSave.ClientID), true);
            }
            else if (Request["__EVENTTARGET"] == "selectUnit")//选择注册的企业
            {
                SetUploadFileType();
            }
        }

        /// <summary>
        /// 设置需要上传的附件
        /// </summary>
        protected void SetUploadFileType()
        {
            
            int PostID = Convert.ToInt32(ViewState["PostID"]);
            int PostTypeID = Convert.ToInt32(ViewState["PostTypeID"]);

            if (trFuJan.Visible==false)
            {
                if(PostTypeID <3 )
                {
                    ShowZACheckResult();
                }
                if(PostID==147)
                {
                    if (IfFaRen == false)//非法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                    }
                    else//法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                    }
                }
                return;
            }

            switch (PostTypeID)
            {
                case 1://三类人
                    switch (PostID)
                    {
                        case 6://土建安全员                              
                        case 1123://机械安全员 
                        case 1125://综合安全员 
                            p_PostTyppe1_aqy.Visible = true;
                            div_SheBao.Visible = true;
                            divSelectCheckType.Style.Remove("display");
                            div_XueLi.Style.Remove("display");
                            div_JiShuZhiCheng.Style.Remove("display");
                            break;
                        case 147://企业主要负责人 
                            p_PostTyppe1_qyfzr.Visible = true;
                            div_SheBao.Visible = true;
                            divSelectCheckType.Style.Remove("display");
                            div_XueLi.Style.Remove("display");
                            div_JiShuZhiCheng.Style.Remove("display");
                            break;
                        case 148://项目负责人 
                            p_PostTyppe1_xmfzr.Visible = true;
                            div_SheBao.Visible = true;
                            break;
                    }
                    ShowZACheckResult();
                    break;
                case 2://特种作业
                    p_PostTyppe2.Visible = true;
                    div_TiJian.Visible = true;
                    div_XueLi.Style.Remove("display");
                    div_grjkcn.Visible = true;
                    div_examsafetrain.Visible = true;
                    ShowZACheckResult();
                    break;                  
                case 4://职业技能（工人）
                    p_PostTyppe4.Visible = true;
                    div_ShenFenZheng.Visible = false;
                    break;
            }
            switch (PostID)
            {
                case 6://土建安全员                              
                case 1123://机械安全员 
                case 1125://综合安全员 
                    if (SheBaoCheckResult == true)
                    {
                        div_SheBao.Style.Add("display", "none");
                    }
                    else
                    {
                        div_SheBao.Style.Remove("display");
                    }

                    divSelectCheckType.Style.Remove("display");

                    if (ViewState["ExamSignUpOB"] != null)
                    {
                        ExamSignUpOB _ExamSignUpOB = (ExamSignUpOB)ViewState["ExamSignUpOB"];    
                        if (_ExamSignUpOB.SignupPromise.HasValue)//承诺方式
                        {
                            RadioButtonListSignupPromise.SelectedIndex = RadioButtonListSignupPromise.Items.IndexOf(RadioButtonListSignupPromise.Items.FindByValue(_ExamSignUpOB.SignupPromise.ToString()));
                            if (_ExamSignUpOB.SignupPromise.ToString() == "0")
                            {
                                div_bktjzm.Style.Add("display", "none");
                                div_XueLi.Style.Remove("display");
                                div_JiShuZhiCheng.Style.Remove("display");
                            }
                            else
                            {
                                div_bktjzm.Style.Remove("display");
                                div_XueLi.Style.Add("display", "none");
                                div_JiShuZhiCheng.Style.Add("display", "none");
                            }
                        }
                    }
                    else
                    { }
                    break;
                case 147://企业主要负责人
                    if (IfFaRen == false)//非法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                    }
                    else//法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                    }

                    string job = RadComboBoxJob.SelectedItem.Text;
                    if(job=="请选择")
                    {
                        div_SheBao.Style.Remove("display");
                        divSelectCheckType.Style.Remove("display");
                        div_XueLi.Style.Remove("display");
                        div_JiShuZhiCheng.Style.Remove("display");
                        p_FaRen.Visible = true;
                        p_NoFaRen.Visible = true;
                    }
                    else if (job == "法定代表人")
                    {
                        div_SheBao.Style.Remove("display");//显示社保上传控件，用于上传营业执照

                        divSelectCheckType.Style.Add("display", "none");
                        div_XueLi.Style.Add("display", "none");
                        div_JiShuZhiCheng.Style.Add("display", "none");

                        p_FaRen.Visible = true;
                        p_NoFaRen.Visible = false;
                    }
                    else//职务选择非法人
                    {
                        if (SheBaoCheckResult == true)//社保
                        {
                            div_SheBao.Style.Add("display", "none");
                        }
                        else
                        {
                            div_SheBao.Style.Remove("display");
                        }

                        divSelectCheckType.Style.Remove("display");
                        div_XueLi.Style.Remove("display");
                        div_JiShuZhiCheng.Style.Remove("display");

                        p_FaRen.Visible = false;
                        p_NoFaRen.Visible = true;
                    }

                    //if (IfFaRen == false)//非法人
                    //{
                    //    divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                    //    divSelectCheckType.Style.Remove("display");
                    //    div_XueLi.Style.Remove("display");
                    //    div_JiShuZhiCheng.Style.Remove("display");

                    //    if (SheBaoCheckResult == true)
                    //    {
                    //        div_SheBao.Style.Add("display", "none");
                    //    }
                    //    else
                    //    {
                    //        div_SheBao.Style.Remove("display");
                    //    }
                    //}
                    //else//法人
                    //{
                    //    divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                    //    divSelectCheckType.Style.Add("display", "none");
                    //    div_XueLi.Style.Add("display", "none");
                    //    div_JiShuZhiCheng.Style.Add("display", "none");
                    //}
                    break;
                case 148://项目负责人 
                    if (SheBaoCheckResult == true)
                    {
                        div_SheBao.Style.Add("display", "none");
                    }
                    else
                    {
                        div_SheBao.Style.Remove("display");
                    }
                    divSelectCheckType.Style.Add("display", "none");
                    div_XueLi.Style.Add("display", "none");
                    break;
            }
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ExamSignUpID)
        {
            //注意：报名审核记录可在3处看到：1、本页；2、ExamSignList.aspx；3、ApplyProcess.aspx；

            ////规则1：当前小于准考证下载前一天，不允许看到建委审核记录信息
            //DataTable dt =null;
            //ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
            //if (_ExamPlanOB.ExamCardSendStartDate.Value.AddDays(-1) < DateTime.Now)//已经放准考证
            //{
            //    dt = ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID);
            //}
            //else//未放准考证
            //{
            //    dt = ExamSignUpDAL.GetCheckHistoryListHideJWCheck(ExamSignUpID);
            //}
            //RadGridCheckHistory.DataSource = dt;
            //RadGridCheckHistory.DataBind();


            //规则2：正常即可显示审核记录
             ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
             DataTable dt = null;
             if (_ExamPlanOB.PostTypeID.Value == 4000)
             {
                 dt = ExamSignUpDAL.GetCheckHistoryListOfNewZYJN(ExamSignUpID);
             }
             else
             {
                 dt = ((PersonType == 2 || PersonType == 3)?
                     ExamSignUpDAL.GetCheckHistoryListForGRQY(ExamSignUpID)
                     : ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID));                
             }
             RadGridCheckHistory.DataSource = dt;
             RadGridCheckHistory.DataBind();
        }

        ///// <summary>
        ///// 绑定审核点及审核量统计
        ///// </summary>
        ///// <param name="ExamPlanID"></param>
        //protected void BindRadGridSignupPlace(long ExamPlanID)
        //{
        //    DataTable dtTj = ExamSignUpPlaceDAL.GetSignUpPlaceTj(ExamPlanID);
        //    RadGridSignupPlace.DataSource = dtTj;
        //    RadGridSignupPlace.DataBind();
        //}

        ///// <summary>
        ///// 设置选中报名点
        ///// </summary>
        ///// <param name="SIGNUPPLACEID">报名点ID</param>
        //private void BindSIGNUPPLACE_Select(long SIGNUPPLACEID)
        //{
        //    //获取选中报名点
        //    for (int j = 0; j < RadGridSignupPlace.MasterTableView.Items.Count; j++)
        //    {
        //        if (Convert.ToInt64(RadGridSignupPlace.MasterTableView.DataKeyValues[j]["SIGNUPPLACEID"]) == SIGNUPPLACEID)
        //        {
        //            RadioButton CheckBox1 = RadGridSignupPlace.MasterTableView.Items[j].Cells[0].FindControl("CheckBoxSIGNUPPLACEID") as RadioButton;
        //            CheckBox1.Checked = true;
        //            break;
        //        }
        //    }
        //}

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/photo_ry.jpg";
            }
        }

        /// <summary>
        /// 拷贝照片到考试计划目录
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public void CopyFacePhoto(string ExamPlanID, string CertificateCode)
        {
            string pathExamPlan = Page.Server.MapPath(string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode));
            string pathWork = Page.Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode));

            if (File.Exists(pathWork) == true && File.Exists(pathExamPlan) == false)
            {
                //个人照片存放路径(按证件号码后3位)
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + CertificateCode.Substring(CertificateCode.Length - 3, 3)))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + CertificateCode.Substring(CertificateCode.Length - 4, 3)));

                File.Copy(pathWork, pathExamPlan);
            }
        }

        //根据证件号码显示相应的信息
        protected void RadTextCertificateCode_TextChanged(object sender, EventArgs e)
        {
            if (RadTextCertificateCode.Text.Length < 5)
            {
                UIHelp.layerAlert(Page, "证件号码的位数不对！");
                return;
            }
            string _ExamPlanName = RadTextBoxExamPlanName.Text;    //考试计划名称
            string _PostID = RadTextPostID.Text;//岗位

            RadTextBoxWorkerName.Text = "";
            RadComboBoxNation.Text = "";
            RadComboBoxNation.SelectedIndex = -1;
            RadComboBoxCulturalLevel.Text = "";
            RadComboBoxCulturalLevel.SelectedIndex = -1;
            RadComboBoxPoliticalBackground.Text = "";
            RadComboBoxPoliticalBackground.SelectedIndex = -1;

            RadTextBoxUnitCode.Text = "";
            RadTextBoxUnitName.Text = "";

            RadTextBoxExamPlanName.Text = "";
            RadTextPostID.Text = "";
            RadTextBoxWorkYearNumer.Text = "";
            RadTextBoxPersonDetail.Text = "";
            //RadTextBoxHireUnitAdvise.Text = "";
            //RadTextBoxAdminUnitAdvise.Text = "";
            RadDatePickerWorkStartDate.SelectedDate = null;
            RadDatePickerBirthday.SelectedDate = null;

            RadTextBoxExamPlanName.Text = _ExamPlanName;
            RadTextPostID.Text = _PostID;

            //int ExamPlanID = Convert.ToInt32(Request.QueryString["o"].ToString());
            ExamPlanOB explanOB = ViewState["ExamPlanOB"] as ExamPlanOB;

            //考生信息
            string certificatecode = RadTextCertificateCode.Text.ToString();
            WorkerOB workerob = WorkerDAL.GetUserObject(certificatecode);   //根据证件号码得到用户
            if (workerob != null)
            {
                RadComboBoxCertificateType.FindItemByValue(workerob.CertificateType).Selected = true;
                RadComboBoxCertificateType.Text = workerob.CertificateType;   //证件类别
                //FacePhoto.ImageUrl = GetFacePhotoPath(Request.QueryString["o"].ToString(), certificatecode); //绑定照片
                System.Random rm = new Random();
                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), certificatecode))); //绑定照片;
                    

                //LoadBinaryImage(RadBinaryImage1, Server.MapPath(FacePhoto.Src));
                RadTextBoxWorkerName.Text = workerob.WorkerName;     //姓名
                if (workerob.Sex == "男")  //性别
                {
                    RadioButtonMan.Checked = true;
                    RadioButtonWoman.Checked = false;
                }
                else
                {
                    RadioButtonWoman.Checked = true;
                    RadioButtonMan.Checked = false;
                }
                RadDatePickerBirthday.SelectedDate = workerob.Birthday;  //出身日期
                RadTextBoxPhone.Text = workerob.Phone;  //联系电话

                if (RadComboBoxNation.FindItemByText(workerob.Nation) != null) //民族
                {
                    RadComboBoxNation.FindItemByText(workerob.Nation).Selected = true;
                    RadComboBoxNation.Text = workerob.Nation;
                }
                if (RadComboBoxCulturalLevel.FindItemByText(workerob.CulturalLevel) != null) //文化程度
                {
                    RadComboBoxCulturalLevel.FindItemByText(workerob.CulturalLevel).Selected = true;
                    RadComboBoxCulturalLevel.Text = workerob.CulturalLevel;
                }
                if (RadComboBoxPoliticalBackground.FindItemByText(workerob.PoliticalBackground) != null) //政治面貌  
                {
                    RadComboBoxPoliticalBackground.FindItemByText(workerob.PoliticalBackground).Selected = true;
                    RadComboBoxPoliticalBackground.Text = workerob.PoliticalBackground;
                }

                HiddenFieldBirthday.Value = Convert.ToDateTime(workerob.Birthday.ToString()).ToString("yyyy-MM-dd");   //.ToString("yyyy-MM-dd");

            }
            //查看是否已经报过名了
            ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(certificatecode, Convert.ToInt64(ViewState["ExamPlanID"]));
            if (_ExamSignUpOB != null)
            {
                ViewState["ExamSignUpOB"] = _ExamSignUpOB;
                #region 报名表信息

                //报名信息
                RadDatePickerWorkStartDate.SelectedDate = _ExamSignUpOB.WorkStartDate;
                RadTextBoxWorkYearNumer.Text = _ExamSignUpOB.WorkYearNumer.HasValue ? _ExamSignUpOB.WorkYearNumer.Value.ToString() : "";
                RadTextBoxPersonDetail.Text = _ExamSignUpOB.PersonDetail;
                lblSignUpCode.Text = _ExamSignUpOB.SignUpCode;//报名批号

                RadTextBoxUnitName.Text = _ExamSignUpOB.UnitName;   //机构名称
                RadTextBoxUnitCode.Text = _ExamSignUpOB.UnitCode;   //机构代码
                if (RadComboBoxSKILLLEVEL.FindItemByText(_ExamSignUpOB.SKILLLEVEL) != null) //技术职称或技术等级
                {
                    RadComboBoxSKILLLEVEL.FindItemByText(_ExamSignUpOB.SKILLLEVEL).Selected = true;
                    RadComboBoxSKILLLEVEL.Text = _ExamSignUpOB.SKILLLEVEL;
                }

                RadComboBoxCertificateType.Text = _ExamSignUpOB.CertificateType;   //证件类别
                RadTextCertificateCode.Text = _ExamSignUpOB.CertificateCode;  //证件号码
                RadTextBoxWorkerName.Text = _ExamSignUpOB.WorkerName;    //姓名     
                RadDatePickerBirthday.SelectedDate = _ExamSignUpOB.S_BIRTHDAY; //出身日期
                RadTextBoxPhone.Text = _ExamSignUpOB.S_PHONE;   //联系电话
                UIHelp.SetReadOnly(RadTextBoxPhone, true);

                if (RadComboBoxCulturalLevel.FindItemByText(_ExamSignUpOB.S_CULTURALLEVEL) != null) //文化程度
                {
                    RadComboBoxCulturalLevel.FindItemByText(_ExamSignUpOB.S_CULTURALLEVEL).Selected = true;
                    RadComboBoxCulturalLevel.Text = _ExamSignUpOB.S_CULTURALLEVEL;
                }

                HiddenFieldApplyMan.Value = _ExamSignUpOB.SignUpMan;//报名操作人
                HiddenFieldFirstCheck.Value = _ExamSignUpOB.S_TRAINUNITNAME;//培训点

                CheckBoxPromise.Checked = true; ;
                LabelCheckStep.Text = string.Format("当前审核阶段：{0}", formatSignupStatus(_ExamSignUpOB.Status, _ExamSignUpOB.CheckDatePlan));
                if (_ExamSignUpOB.S_SEX == "男")  //性别
                {
                    RadioButtonMan.Checked = true;
                    RadioButtonWoman.Checked = false;
                }
                else
                {
                    RadioButtonWoman.Checked = true;
                    RadioButtonMan.Checked = false;
                }

                ShowSheBao(_ExamSignUpOB);
                SetButtonEnable(_ExamSignUpOB.Status);
                BindCheckHistory(_ExamSignUpOB.ExamSignUpID.Value);
                BindFile(ApplyID);

                if (_ExamSignUpOB.CreatePersonID != PersonID)
                {
                    SetButtonEnable("非法读取");
                    UIHelp.layerAlert(Page, "输入人员信息不是您发起的报名，您无权修改他们报名信息！");

                }
                else
                {
                    switch (_ExamSignUpOB.Status)
                    {
                        case EnumManager.SignUpStatus.ReturnEdit:
                            UIHelp.layerAlert(Page, "您的报名已经被审核端驳回，请查看审核记录的驳回原因！");
                            break;
                        case EnumManager.SignUpStatus.SaveSignUp:
                            UIHelp.layerAlert(Page, "您保存的报名表尚未提交单位审核！");
                            break;
                        case EnumManager.SignUpStatus.NewSignUp:
                            UIHelp.layerAlert(Page, "您的报名表已经提交单位审核，请通知单位及时审核确认！");
                            break;
                        default:
                            UIHelp.layerAlert(Page, "报名已经进入受理阶段，请耐心等待！");
                            break;
                    }
                }

                #endregion 报名表信息

               
            }
            else
            {
                ViewState["ExamSignUpOB"] = null;
                SetButtonEnable("");
                BindCheckHistory(0);
                BindFile(ApplyID);
            }
        }

        //根据组织机构代码显示单位名称
        protected void RadTextBoxUnitCode_TextChanged(object sender, EventArgs e)
        {
            ToDBC(RadTextBoxUnitCode.Text.ToString());
            string unitcode = RadTextBoxUnitCode.Text.ToString();  //组织机构代码
            //UnitInfoOB unitInfoob = UnitInfoDAL.GetObjectByUnitCode(unitcode);
            //if (unitInfoob != null)
            //{
            //    RadTextBoxUnitName.Text = unitInfoob.UnitName;  //单位名称
            //}
            if (unitcode.Length==18)
            {
                unitcode=unitcode.Substring(8, 9);
            }
            string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(unitcode);
            if (string.IsNullOrEmpty(UnitName) == false)
            {
                RadTextBoxUnitName.Text = UnitName;  //单位名称
                SetUploadFileType();
            }
            else
            {
                RadTextBoxUnitName.Text = "";
                if (ViewState["PostTypeID"].ToString() == "1" && RadTextBoxUnitCode.Text != "")//三类人企业名称只能从资质库读取
                {
                    UIHelp.layerAlert(Page, "在本市管理的建筑企业资质库中查不到您所在企业名称！");
                }
            }
        }

        //保存报名表
        protected void btnSave_Click(object sender, EventArgs e)
        { 
            //1、报名限制：年龄未满18周岁；项目负责人年龄上限为65周岁；专职安全生产管理人员、专业技术管理人员、造价员和建设职业技能人员年龄上限为60周岁；建筑施工特种作业人员年龄上限为男60周岁女50周岁。
            //2、报名限制检查（项目负责人、专职安全生产管理人员）：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位。
            //3、报名限制检查:不能在同一家公司取得多个企业负责人证书。
            //4、已有同类型证书，尚未过期，不能报名。（但满足第五条可重复，但必须不同等级；企业负责人可重复，但必须多加单位）。
            //5、职业技能岗位证书（除“村镇建筑工匠”和“普工”）外，已取得同类型同“技术等级”证书，尚未过期，不能报名。
            //6、报考“物业项目负责人”：
            //    6.1）比对《在岗无证物业项目负责人》库，一致允许报考。
            //    6.2）不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）。
            //7、在黑名单中人员限制一年内不得报考报名
            //8、系统中存在相同岗位工种证书，证书处于锁定中，解锁前不允许报名。
            //9、人员（身份证）处在被锁定状态中，解锁前不允许报名。
            //10、三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。
            //11、项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
            //12、报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
            //13、职业技能岗位考试计划有“技术等级”分类,报名时不允许手工填写，自动获取为与考试计划一致。（其中村镇建筑工匠or普工两个岗位无等级，系统自动填写为“无”）

            //报名验证添加规则
            //1）、上次报名审核尚未截止，这次不允许报名。
            //2）、上次报名审核未通过，这次可以报名。
            //3）、上次报名审核通过，尚未发放准考证，不允许报名。
            //4）、上次报名审核通过，已经发放准考证，尚未公告成绩，不允许报名。
            //5）、上次报名审核通过，已经发放准考证，已经公告成绩（未通过），允许报名。
            //6）、上次报名审核通过，已经发放准考证，已经公告成绩（通过，尚未发放证书），不允许报名。

            ExamPlanOB explanOB = ViewState["ExamPlanOB"] as ExamPlanOB;

            ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(RadTextCertificateCode.Text.Trim(), explanOB.ExamPlanID.Value);

            #region 提交前校验            

            if (_ExamSignUpOB == null || _ExamSignUpOB.Status == EnumManager.SignUpStatus.SaveSignUp)
            {
                if (DateTime.Now < explanOB.SignUpStartDate.Value || DateTime.Now > explanOB.SignUpEndDate.Value.AddDays(1))
                {
                    UIHelp.layerAlert(Page, "不在报名时间范围内，无法修改报名。");
                    return;
                }
            }
            else if (_ExamSignUpOB.Status == EnumManager.SignUpStatus.ReturnEdit)
            {
                if (DateTime.Now < explanOB.SignUpStartDate.Value || DateTime.Now > explanOB.SignUpEndDate.Value.AddDays(3))
                {
                    UIHelp.layerAlert(Page, "不在报名时间范围内，无法修改报名。");
                    return;
                }
            }

            if (CheckBoxPromise.Checked == false)
            {
                UIHelp.layerAlert(Page, "请勾选本人承诺！");
                return;
            }

            if (explanOB.PostTypeID != 4000)
            {

                if (RadDatePickerENT_ContractStartTime.SelectedDate.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "请输入劳动合同开始时间", 5, 0);
                    return;
                }
                if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "请输入劳动合同结束时间", 5, 0);
                    return;
                }
                if (string.IsNullOrEmpty(RadioButtonListENT_ContractType.SelectedValue))
                {
                    UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
                    return;
                }
                else if (RadioButtonListENT_ContractType.SelectedValue != "1" && RadioButtonListENT_ContractType.SelectedValue != "2" && RadioButtonListENT_ContractType.SelectedValue != "3")
                {
                    UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
                    return;
                }
                if (RadTextBoxUnitName.Text.Replace(" ", "").Replace("　", "") == "")
                {
                    UIHelp.layerAlert(Page, "单位名称不能为空！");
                    return;
                }
                if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxUnitCode.Text) == false)
                {
                    UIHelp.layerAlert(Page, "“组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                    return;
                }
            }
            string IsConditions = "符合";

            if (RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", "") == "")
            {
                UIHelp.layerAlert(Page, "姓名不能为空！");
                return;
            }

            if (ViewState["PostTypeID"].ToString() == "1" && string.IsNullOrEmpty(RadComboBoxJob.SelectedValue) == true)
            {
                UIHelp.layerAlert(Page, "职务不能为空", 5, 0);
                return;
            }

            if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
            {
                if (RadTextCertificateCode.Text.Trim().Length != 18)
                {
                    UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
                    return;
                }
                else if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                    return;
                }
            }
            string mySex = RadioButtonMan.Checked == true ? "男" : "女";    //性别
            DateTime myBirthday = RadDatePickerBirthday.SelectedDate.Value; //出身日期

            if (ViewState["PostTypeID"].ToString() == "1")//安全生产管理人员
            {
                if (ViewState["PostID"].ToString() == "147")//A本
                {
                    if (RadComboBoxJob.SelectedItem.Text == "法定代表人")//职务
                    {
                        if (IfFaRen == false)//非法人
                        {
                            UIHelp.layerAlert(Page, "校验未通过：工商信息显示您并非报名单位企业法人，不能以法定代表人职务报考。");
                            return;
                        }

                        CertificateOB FROb = CertificateDAL.GetFRCertA(RadTextBoxUnitCode.Text.Trim());
                        if(FROb !=null)
                        {
                            UIHelp.layerAlert(Page, string.Format("校验未通过：一个单位只能允许一人以法定代表人职务持有A证，报名单位已经存在法人A证【{0}】。", FROb.CertificateCode));
                            return;
                        }
                        //法人不受年龄显限制
                    }
                    else//非法人
                    {
                        //非企业法人，受年龄限制
                        if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                        {
                            UIHelp.layerAlert(Page, "您已超龄不予报考！");
                            return;
                        }

                        CertificateOB FROb = CertificateDAL.GetOtherNoFRCertA(RadTextCertificateCode.Text.Trim(), RadTextCertificateCode.Text.Trim());
                        if (FROb != null)
                        {
                            UIHelp.layerAlert(Page, string.Format("校验未通过： 不允许持有多本非法人职务的A本，你已持有非法人职务的A本【{0}】。", FROb.CertificateCode));
                            return;
                        }
                    }                  
                }
                else//B、C1、C2、C3
                {
                    if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                    {
                        UIHelp.layerAlert(Page, "您已超龄不予报考！");
                        return;
                    }
                }
            }
            else if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostTypeID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)//非安全生产管理人员
            {
                UIHelp.layerAlert(Page, "您已超龄不予报考！");
                return;
            }           
          
       
            //已报名数量
            if (_ExamSignUpOB == null)//新增时验证
            {
                int signUpCount = ExamSignUpDAL.SelectSignupCount(explanOB.ExamPlanID.Value);
                if (signUpCount >= explanOB.PersonLimit)
                {
                    UIHelp.layerAlert(Page, "抱歉本期报名人数已达上限，我们将全力以赴，争取尽快满足您的需求，请您下期再报，感谢您的支持和理解。");
                    return;
                }
            }

            if (RadUploadFacePhoto.UploadedFiles.Count > 0)
            {
                if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "报名照片格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "报名照片大小不能超过50k！");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength < 200)
                {
                    UIHelp.layerAlert(Page, "照片大小存在问题，请检查照片是否有效！");
                    return;
                }                
            }
            if (RadUploadFacePhoto.UploadedFiles.Count == 0)//照片
            {
                if (!File.Exists(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", RadTextCertificateCode.Text.Trim().Substring(RadTextCertificateCode.Text.Trim().Length - 3, 3), RadTextCertificateCode.Text.Trim()))))
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }
                else
                {
                    FileInfo fi = new FileInfo(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", RadTextCertificateCode.Text.Trim().Substring(RadTextCertificateCode.Text.Trim().Length - 3, 3), RadTextCertificateCode.Text.Trim())));
                    if (fi.Length < 200)
                    {
                        UIHelp.layerAlert(Page, "照片大小存在问题，请检查照片是否有效！");
                        return;
                    }
                }
            }

            bool AgeLimit = false;
            AgeLimit = ValidResourceIDLimit(RoleIDs, "SignUpWithoutAgeLimit");
            if (AgeLimit == false)
            {
                //报名限制检查（所有类型证书）：
                if (CertificateDAL.CheckRegular_SpecialOperator(RadDatePickerBirthday.SelectedDate.Value) == false)
                {
                    UIHelp.layerAlert(Page, "年龄未满18周岁，不符合报名要求！");
                    return;
                }
            }
            
            CertificateOB certificateob = CertificateDAL.GetCertificateOBObject(RadTextCertificateCode.Text.Trim(), Convert.ToInt32(ViewState["PostID"]), DateTime.Now);
            if (certificateob != null)
            {
                //报名限制检查:不能在同一家公司取得多个企业负责人证书
                if (ViewState["PostID"].ToString() == "147")
                {
                    if (CertificateDAL.CheckRegular_UnitMaster(certificateob, RadTextBoxUnitCode.Text.Trim()) == false)
                    {
                        UIHelp.layerAlert(Page, "不能在同一单位取得多个“企业主要负责人”证书，不能报名！");
                        return;
                    }
                }
                else if (
                            (ViewState["PostTypeID"].ToString() == "4" //职业技能岗位
                             || ViewState["PostTypeID"].ToString() == "4000"//新版职业技能岗位
                            )
                    && ViewState["PostID"].ToString() != "158"//村镇建筑工匠
                   && ViewState["PostID"].ToString() != "199")//普工
                {
                    if (certificateob.SkillLevel == RadComboBoxSKILLLEVEL.Text)
                    {
                        UIHelp.layerAlert(Page, "已取得同类型同技术等级证书，尚未过期，不能报名！");
                        return;
                    }
                }
                else //报名限制检查:检查该人是否持有有效的（有效期未过）
                {
                    UIHelp.layerAlert(Page, "已取得同类型证书，尚未过期，不能报名！");
                    return;
                }
            }

            //报考“物业项目负责人”
            if (ViewState["PostID"].ToString() == "159")
            {
                if (
                    //比对《在岗无证物业项目负责人》库，一致允许报考（姓名、身份证号）
                    //*****************************读取文件编码gb2132与UTF-8不一致，比较中文姓名有问题，需要改造
                    (UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(RadTextCertificateCode.Text.Trim().Replace("x", "X")) == false)// || UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(RadTextBoxWorkerName.Text.Trim()) == false)
                    &&
                    //不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）
                    (UIHelp.QueryWYQYFromBaseDB(Page).Contains(RadTextBoxUnitCode.Text.Trim().Replace("x", "X")) == false)
                    )
                {
                    UIHelp.layerAlert(Page, "报名企业不在本市管理或核发的物业企业资质库中，也未在物业管理处提供的“在岗无证人员名录”内，不许报名。<br >如有疑问，请联系010-59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                    return;
                }
            }

            //报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
            if (ViewState["PostID"].ToString() == "1021" || ViewState["PostID"].ToString() == "1024")
            {
                if (UIHelp.QueryWYQYFromBaseDB(Page).Contains(RadTextBoxUnitCode.Text.Trim().Replace("x", "X")) == false)
                {
                    UIHelp.layerAlert(Page, "报名企业（企业名称、组织机构代码）不在本市管理或核发的物业企业资质库中。<br >请您核对报名表中企业名称及组织机构代码是否正确，如有疑问，请联系010-59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                    return;
                }
            }

            //在黑名单中人员限制一年内不得报考报名
            DataTable examAdnormal = CommonDAL.GetDataTable(string.Format(@"select * from TJ_BlackList where CertificateCode='{0}' and ExamStartDate >'{1}'"
                , RadTextCertificateCode.Text.Trim().Replace("x", "X"), DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd")));

            if (examAdnormal != null && examAdnormal.Rows.Count > 0)
            {
                UIHelp.layerAlert(Page, string.Format("报名受限：{0}在{1}进行的{2}_{3}_{4}考试中有{5}行为，在{6}内不允许你报考所有专业。"
                    , examAdnormal.Rows[0]["WorkerName"].ToString()
                    , Convert.ToDateTime(examAdnormal.Rows[0]["ExamStartDate"]).ToString("yyyy年MM月dd日")
                    , examAdnormal.Rows[0]["PostTypeName"].ToString()
                    , examAdnormal.Rows[0]["PostName"].ToString()
                    , examAdnormal.Rows[0]["KeMuName"].ToString()
                    , examAdnormal.Rows[0]["ExamStatus"].ToString()
                    , "1年")
                    );
                return;
            }

            //证书锁定校验
            int countLock = CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATELOCK"
                , string.Format(" and WorkerCertificateCode='{0}' and PostID={1} and CertificateID in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{2}')"
                , RadTextCertificateCode.Text.Trim(), ViewState["PostID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")));
            if (countLock > 0)
            {
                UIHelp.layerAlert(Page, "系统中存在相同岗位工种证书，证书处于锁定中，解锁前不允许报名！解锁请联系北京市建筑业执业资格注册中心。");
                return;
            }

            //人员锁定校验
            int personLock = CommonDAL.SelectRowCount("dbo.VIEW_WORKERLOCK"
                , string.Format(" and CertificateCode='{0}' and LockStatus='加锁' and LockEndTime >'{1}'"
                , RadTextCertificateCode.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd")));
            if (personLock > 0)
            {
                UIHelp.layerAlert(Page, "您处于被锁定状态中，解锁前不允许报名！解锁请联系北京市建筑业执业资格注册中心。");
                return;
            }

            //相同岗位类型一个月只能参加一个工种考试报名
            if (ExamSignUpDAL.CheckExamSignupCount(Convert.ToInt64(ViewState["ExamPlanID"]), Convert.ToInt32(ViewState["PostTypeID"]), ViewState["ExamStartDateYM"].ToString(), RadTextCertificateCode.Text.Trim()) > 0)
            {
                UIHelp.layerAlert(Page, "您存在当月相同岗位类别其他工种考试报名，同一岗位类别一次只允许报名一个工种！。");
                return;
            }

            //if (PersonType != 6)//非行政管理机构
            //{
            string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), true);

            //三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。
            if (ViewState["PostTypeID"].ToString() == "1")
            {

                if (string.IsNullOrEmpty(UnitName))
                {
                    UIHelp.layerAlert(Page, "您所在的企业不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。");
                    return;
                }
                if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                {
                    UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                    return;
                }

                if (ViewState["PostID"].ToString() == "147"
                    || ViewState["PostID"].ToString() == "148")//（其中起重机械租赁企业只允许报考专职安全生产管理人员（C本），不能报考A本和B本）
                {
                    List<string> zz = UnitDAL.GetUnitZZLBFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim());
                    if (zz.Count == 1 && zz[0] == "起重机械租赁企业")
                    {
                        UIHelp.layerAlert(Page, "根据相关政策起重机械租赁企业只允许报考专职安全生产管理人员（C1、C2、C3），不能报考A本和B本，您不满足报考要求。");
                        return;
                    }
                }

                //报名限制检查（项目负责人、专职安全生产管理人员）：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位
                if (CertificateDAL.CheckRegular_ItemMaster(RadTextCertificateCode.Text.Trim(), RadTextBoxUnitCode.Text.Trim(), Convert.ToInt32(ViewState["PostID"]), RadComboBoxJob.SelectedItem.Text) == false)
                {
                    UIHelp.layerAlert(Page, "同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员(C1、C2、C3)”证书，并且有C3不能再报考C1和C2，有C1或C2不能报考C3，您不符合要求，不能报名！");
                    return;
                }
            }
            ////对特种作业人员和职业技能人员考核业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司
            //if (ViewState["PostTypeID"].ToString() == "2" || ViewState["PostTypeID"].ToString() == "4")
            //2021-11-29日临时取消职业技能岗位报名需要取得有市住建委核发的相关资质方可报名的功能限制(古建职业技能大赛企业报名需求，因部分报名企业为事业单位。大赛结束发证后恢复原功能)
            if (ViewState["PostTypeID"].ToString() == "2" )
            {
                UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), false);
                if (string.IsNullOrEmpty(UnitName))
                {
                    UIHelp.layerAlert(Page, "您所在的企业不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。");
                    return;
                }
                if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                {
                    UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                    return;
                }
            }

            //项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
            if (ViewState["PostID"].ToString() == "148")
            {
                DataTable jzs = UIHelp.GetJZS(Page, "本地");
                if (jzs.Rows.Find(new string[] { RadTextCertificateCode.Text.Trim().Replace("x", "X"), RadTextBoxUnitCode.Text.Trim() }) == null
                    && jzs.Rows.Find(new string[] { Utility.Check.ConvertoIDCard18To15(RadTextCertificateCode.Text.Trim().Replace("x", "X")), RadTextBoxUnitCode.Text.Trim() }) == null)
                {
                    UIHelp.layerAlert(Page, "报考项目负责人要求您必须取得本市企业建造师注册证书，查不到你的建造师注册信息，不允许报名。");
                    return;
                }
            }

            if (explanOB.PostTypeID.Value == 1//三类人
             || explanOB.PostTypeID.Value == 5)//专业技术员
            {
                TJ_MissExamLockMDL _TJ_MissExamLockMDL = TJ_MissExamLockDAL.GetObject(RadTextCertificateCode.Text.Trim().Replace("x", "X"));
                if (_TJ_MissExamLockMDL != null)
                {
                    UIHelp.layerAlert(Page, string.Format("由于您一年内累积三次未参加考试，您的信息已被锁定一年，截止{0}不得报考我市建筑业从业人员考试。",_TJ_MissExamLockMDL.LockEndDate.Value.ToString("yyyy-MM-dd日")));
                    return;
                }
            }

            ExamSignUpOB _Lock = ExamSignUpDAL.GetLockIng(RadTextCertificateCode.Text.Trim());
            {
                if(_Lock!=null)
                {
                    UIHelp.layerAlertWithHtml(Page, string.Format("{3}{4}考试违规申报锁定，<br />锁定时间：{0} - {1}，<br />锁定原因：{2}"
                        , _Lock.LockTime.Value.ToString("yyyy.MM.dd")
                        , _Lock.LockEndTime.Value.ToString("yyyy.MM.dd")
                        , _Lock.LockReason.Replace("\r\n", "<br />")
                        , explanOB.ExamStartDate.Value.ToString("yyyy.MM.dd日")
                         , explanOB.PostName
                        ));
                    return;
                }
            }

            //判断考生是否存在为出成绩考试报名
            //bool checkExaming = ExamSignUpDAL.CheckIfHaveExaming(RadTextCertificateCode.Text.Trim()
            //    , (explanOB.PostID.Value == 6 || explanOB.PostID.Value == 1123 || explanOB.PostID.Value == 1125) ? "6,1123,1125" : explanOB.PostID.ToString()
            //    , 3
            //    ,explanOB.ExamPlanID.Value);
            bool checkExaming = ExamSignUpDAL.CheckIfHaveExaming(RadTextCertificateCode.Text.Trim()
                , explanOB.PostID.Value == 6 ? "6,1125" : explanOB.PostID.Value == 1123 ? "1123,1125" : explanOB.PostID.Value == 1125 ? "6,1123,1125" : explanOB.PostID.ToString()
                , 3
                , explanOB.ExamPlanID.Value);

            if (checkExaming == true)
            {
                UIHelp.layerAlert(Page, "系统发现您上次报名考试尚未出成绩，不允许申报本次考试。");
                return;
            }           

           

            if (explanOB.PostTypeID.Value <3 && UnitDAL.CheckGongShang(RadTextBoxUnitCode.Text.Trim())==false)
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxUnitName.Text));
                return;
            }

            #endregion 提交前校验

            WorkerOB workerob = WorkerDAL.GetUserObject(RadTextCertificateCode.Text.Trim());   //根据证件号码得到用户
            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxUnitCode.Text.Trim());//组织机构代码  

            DateTime doTime = DateTime.Now;//处理时间

            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                #region 向从业人员表插入/修改数据
                if (workerob == null) workerob = new WorkerOB();

                if (workerob.WorkerID.HasValue == false)//new
                {
                    workerob.CertificateType = RadComboBoxCertificateType.SelectedItem.Value;   //证件类别
                    workerob.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别
                    workerob.Birthday = RadDatePickerBirthday.SelectedDate.Value;  //出身日期
                    workerob.Nation = (RadComboBoxNation.Text == "请选择" ? "" : RadComboBoxNation.Text);   //民族
                    workerob.CulturalLevel = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                    workerob.PoliticalBackground = (RadComboBoxPoliticalBackground.Text == "请选择" ? "" : RadComboBoxPoliticalBackground.Text);  //政治面貌
                    workerob.CertificateCode = RadTextCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                    workerob.Phone = RadTextBoxPhone.Text.Trim();  //联系电话
                    workerob.WorkerName = RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", "");     //姓名
                    WorkerDAL.Insert(dtr, workerob);
                }
                else
                {
                    //workerob.CertificateType = RadComboBoxCertificateType.SelectedItem.Value;   //证件类别
                    //workerob.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别
                    //workerob.Birthday = RadDatePickerBirthday.SelectedDate.Value;  //出身日期
                    workerob.Nation = (RadComboBoxNation.Text == "请选择" ? "" : RadComboBoxNation.Text);   //民族
                    workerob.CulturalLevel = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                    workerob.PoliticalBackground = (RadComboBoxPoliticalBackground.Text == "请选择" ? "" : RadComboBoxPoliticalBackground.Text);  //政治面貌
                    //workerob.CertificateCode = RadTextCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                    //workerob.Phone = RadTextBoxPhone.Text.Trim();  //联系电话
                    //workerob.WorkerName = RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", "");     //姓名
                    WorkerDAL.Update(dtr, workerob);
                }

                #endregion

                #region 向机构表插入数据
                if (_UnitMDL == null)
                {
                    _UnitMDL = new UnitMDL();

                    //企业资质
                    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(RadTextBoxUnitCode.Text.ToString());

                    GSJ_QY_GSDJXXMDL gs = DataAccess.UnitDAL.GetObjectUni_scid(RadTextBoxUnitCode.Text.ToString()); //工商信息

                    if (_jcsjk_QY_ZHXXMDL != null)//有资质
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                        _UnitMDL.ENT_OrganizationsCode = RadTextBoxUnitCode.Text.ToString();//组织机构代码
                        _UnitMDL.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                        _UnitMDL.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;//区县
                        _UnitMDL.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                        _UnitMDL.ENT_Corporate = _jcsjk_QY_ZHXXMDL.FDDBR;//法定代表人
                        _UnitMDL.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                        _UnitMDL.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                        if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                        {
                            _UnitMDL.ENT_Sort = "";
                            _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        }
                        else
                        {
                            //企业资质等级
                            if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                            {
                                if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                                {
                                    _UnitMDL.ENT_Grade = "特级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                                {
                                    _UnitMDL.ENT_Grade = "一级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                                {
                                    _UnitMDL.ENT_Grade = "二级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                                {
                                    _UnitMDL.ENT_Grade = "三级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                                {
                                    _UnitMDL.ENT_Grade = "不分等级";
                                }
                                else
                                {
                                    _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                }
                            }
                            else
                            {
                                _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                            }
                            _UnitMDL.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                            string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                            if (_UnitMDL.ENT_Grade != null)
                            {
                                foreach (var item in ZZ)
                                {
                                    if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(_UnitMDL.ENT_Grade) == true)
                                    {
                                        _UnitMDL.ENT_Sort = item;   //资质序列
                                        break;
                                    }

                                }
                            }

                        }
                        _UnitMDL.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                       
                        _UnitMDL.Valid = 1;//是否有效
                        if (gs != null)
                        {
                            _UnitMDL.CreditCode = gs.UNI_SCID;//社会统一信用代码
                            _UnitMDL.ResultGSXX = 2;
                        }
                        else
                        {
                            _UnitMDL.ResultGSXX = 0;
                            _UnitMDL.CreditCode = "";
                        }
                    }
                    else//无资质设置为新设立企业
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = DateTime.Now;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = RadTextBoxUnitName.Text.Trim();//企业名称
                        _UnitMDL.ENT_OrganizationsCode = RadTextBoxUnitCode.Text.Trim();//组织机构代码
                        _UnitMDL.ENT_Economic_Nature = "";//企业类型
                        _UnitMDL.ENT_City = "";//区县
                        _UnitMDL.END_Addess = "";//注册地址
                        _UnitMDL.ENT_Corporate = "";//法定代表人
                        //_UnitMDL.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅无企业通讯地址企业自行维护
                        _UnitMDL.ENT_Type = "";  //企业类型
                        _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                        _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                        _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号
                          _UnitMDL.Valid = 1;//是否有效
                        _UnitMDL.Memo = "新设立企业";
                        _UnitMDL.CJSJ = DateTime.Now;
                    
                        if (gs != null)
                        {
                            _UnitMDL.CreditCode = gs.UNI_SCID;//社会统一信用代码
                            _UnitMDL.ResultGSXX = 2;
                        }
                        else
                        {
                            _UnitMDL.ResultGSXX = 0;
                            _UnitMDL.CreditCode = "";
                        }
                    }

                    UnitDAL.Insert(dtr, _UnitMDL);
                }

                #endregion

                #region 向考试报名表插入数据

                if (_ExamSignUpOB == null)
                {
                    _ExamSignUpOB = new ExamSignUpOB();
                }
                else if (_ExamSignUpOB.Status != EnumManager.SignUpStatus.NewSignUp
                    && _ExamSignUpOB.Status != EnumManager.SignUpStatus.SaveSignUp
                    && _ExamSignUpOB.Status != EnumManager.SignUpStatus.ReturnEdit
                    )
                {
                    dtr.Rollback();
                    UIHelp.layerAlert(Page, "报名已经进受理，不能修改！");
                    return;
                }
                if (explanOB.PostTypeID == 4000)
                {
                    _ExamSignUpOB.S_TRAINUNITNAME = explanOB.SignUpPlace;//培训点名称
                    HiddenFieldFirstCheck.Value = explanOB.SignUpPlace;
                }
                else
                {
                    _ExamSignUpOB.TrainUnitID = null;
                    _ExamSignUpOB.S_TRAINUNITNAME = null;//培训点名称
                }

                _ExamSignUpOB.WorkerID = workerob.WorkerID;//从业人员ID

                _ExamSignUpOB.ExamPlanID = explanOB.ExamPlanID;   //考试计划id
                if (RadDatePickerWorkStartDate.SelectedDate != null) _ExamSignUpOB.WorkStartDate = RadDatePickerWorkStartDate.SelectedDate.Value;
                if (RadTextBoxWorkYearNumer.Text.Trim() != "") _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(RadTextBoxWorkYearNumer.Text.Trim());
                _ExamSignUpOB.PersonDetail = UIHelp.ToHalfCode(RadTextBoxPersonDetail.Text.Trim());
                //_ExamSignUpOB.HireUnitAdvise = "";//RadTextBoxHireUnitAdvise.Text.Trim();
                //_ExamSignUpOB.AdminUnitAdvise = "";//RadTextBoxAdminUnitAdvise.Text.Trim();

                _ExamSignUpOB.WorkerName = RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", ""); //姓名
                _ExamSignUpOB.CertificateType = RadComboBoxCertificateType.SelectedItem.Value;   //证件类别
                _ExamSignUpOB.CertificateCode = RadTextCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                bool IfChangeUnit = false;//是否修改了选择单位
                if (string.IsNullOrEmpty(_ExamSignUpOB.UnitCode) == false && _ExamSignUpOB.UnitCode != RadTextBoxUnitCode.Text.Trim())
                {
                    IfChangeUnit = true;//变换了单位，需要重新核验社保
                    _ExamSignUpOB.SheBaoCheck = null;
                }
                _ExamSignUpOB.UnitName = Utility.Check.removeInputErrorChares(RadTextBoxUnitName.Text);    //单位名称
                _ExamSignUpOB.UnitCode = RadTextBoxUnitCode.Text.Trim();    //机构号码

                _ExamSignUpOB.SKILLLEVEL = (RadComboBoxSKILLLEVEL.Text == "请选择" ? "" : RadComboBoxSKILLLEVEL.Text);
                _ExamSignUpOB.S_SEX = RadioButtonMan.Checked == true ? "男" : "女";    //性别
                _ExamSignUpOB.S_BIRTHDAY = RadDatePickerBirthday.SelectedDate.Value; //出身日期
                _ExamSignUpOB.S_CULTURALLEVEL = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                _ExamSignUpOB.S_PHONE = RadTextBoxPhone.Text.Trim(); //联系电话
                _ExamSignUpOB.IsConditions = IsConditions;

                _ExamSignUpOB.Promise = 1;//个人承诺

                //自己取回或驳回的不能修改状态，提交企业审核时才修改状态。以便保证报名已截止，企业审核没截止，驳回后个人修改保存还可以提交到单位
                if (string.IsNullOrEmpty(_ExamSignUpOB.Status) == true || _ExamSignUpOB.Status != EnumManager.SignUpStatus.ReturnEdit)
                {
                    _ExamSignUpOB.Status = EnumManager.SignUpStatus.SaveSignUp;//状态
                }

                _ExamSignUpOB.FirstCheckType = 0;

                if (ViewState["PostID"].ToString() == "148")//B本因为有建造师，免初审
                {
                    _ExamSignUpOB.FirstCheckType = 1;
                }
                else if (ViewState["PostID"].ToString() == "147" && IfFaRen == true)//A本比对法人库，免初审
                {
                    _ExamSignUpOB.FirstCheckType = 4;
                }

                if (tableUnit.Visible == true)
                {
                    _ExamSignUpOB.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);
                    //劳动合同开始时间
                    _ExamSignUpOB.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                    //劳动合同结束时间
                    if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                    {
                        _ExamSignUpOB.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                    }
                    else
                    {
                        _ExamSignUpOB.ENT_ContractENDTime = null;
                    }
                }

                if (trJob.Visible == true)
                {
                    _ExamSignUpOB.Job = (RadComboBoxJob.SelectedItem.Text == "请选择" ? "" : RadComboBoxJob.SelectedItem.Text);
                }

                if (explanOB.PostTypeID==2)
                {
                    _ExamSignUpOB.SafeTrainType = RadioButtonListSafeTrainType.SelectedValue;

                    if (_ExamSignUpOB.SafeTrainType == "委托培训机构")
                    {
                        _ExamSignUpOB.SafeTrainUnit = RadTextBoxSafeTrainUnit.Text.Trim();
                        _ExamSignUpOB.SafeTrainUnitCode = RadTextBoxSafeTrainUnitCode.Text.Trim();
                        _ExamSignUpOB.SafeTrainUnitValidEndDate = RadDatePickerSafeTrainUnitValidEndDate.SelectedDate;
                        _ExamSignUpOB.SafeTrainUnitOfDept = RadTextBoxSafeTrainUnitOfDept.Text.Trim();
                    }
                    else
                    {
                        _ExamSignUpOB.SafeTrainUnit = null;
                        _ExamSignUpOB.SafeTrainUnitCode = null;
                        _ExamSignUpOB.SafeTrainUnitValidEndDate = null;
                        _ExamSignUpOB.SafeTrainUnitOfDept = null;
                    }
                }

                //查看是否已经报过名了
                if (_ExamSignUpOB.ExamSignUpID.HasValue == false)//初次报名
                {
                    _ExamSignUpOB.CreatePersonID = PersonID;
                    _ExamSignUpOB.SignUpMan = PersonName; //报名操作人
                    HiddenFieldApplyMan.Value = PersonName;
                    _ExamSignUpOB.CreateTime = doTime;
                    _ExamSignUpOB.SignUpDate = doTime;   //报名日期
                    _ExamSignUpOB.SignUpCode = UIHelp.GetNextBatchNumber("XWBM");
                    _ExamSignUpOB.SignupPromise = 0;//是否上传承诺书证明学历、职称
                    ExamSignUpDAL.Insert(dtr, _ExamSignUpOB);
                    ViewState["ExamSignUpOB"] = _ExamSignUpOB;

                    lblSignUpCode.Text = _ExamSignUpOB.SignUpCode; //报名编号

                    //拷贝人员当前有效附件到申请表
                    ImportFileHistory(dtr, explanOB, _ExamSignUpOB);
                }
                else
                {
                    _ExamSignUpOB.ModifyPersonID = PersonID;
                    _ExamSignUpOB.ModifyTime = doTime;
                    _ExamSignUpOB.SignUpDate = doTime; 
                    if (IfChangeUnit == true)//修改了单位，重新验证
                    {
                        _ExamSignUpOB.ZACheckTime = null;
                        _ExamSignUpOB.ZACheckResult = null;
                        _ExamSignUpOB.ZACheckRemark = null;
                    }

                    ExamSignUpDAL.Update(dtr, _ExamSignUpOB);

                    if (IfChangeUnit == true)//清空社保核验结果，重新发起核验
                    {
                        ExamSignUpDAL.ClearSheBaoCheck(dtr, _ExamSignUpOB.ExamSignUpID.Value);
                        ClearCheckFaRenOrSheBao();
                    }
                    ViewState["ExamSignUpOB"] = _ExamSignUpOB;
                }
                LabelCheckStep.Text = string.Format("当前审核阶段：{0}", formatSignupStatus(_ExamSignUpOB.Status, _ExamSignUpOB.CheckDatePlan));

                #endregion

                #region 上传个人照片
                //个人照片存放路径(按证件号码后3位)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));

                //考试报名照片存放路径(按考试计划ID分类)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + explanOB.ExamPlanID.ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + explanOB.ExamPlanID.ToString()));

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string signUpPhotoFolder = "~/UpLoad/SignUpPhoto/" + explanOB.ExamPlanID.ToString();//考试报名照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = _ExamSignUpOB.CertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, _ExamSignUpOB.CertificateCode + ".jpg"), true);

                        if (File.Exists(Path.Combine(workerPhotoFolder, _ExamSignUpOB.CertificateCode + ".jpg")))
                        {
                            File.Copy(Path.Combine(workerPhotoFolder, _ExamSignUpOB.CertificateCode + ".jpg"), Path.Combine(Server.MapPath(signUpPhotoFolder), _ExamSignUpOB.CertificateCode + ".jpg"), true);
                        }
                        break;
                    }
                }
                else
                {
                    //从个人照片目录拷贝到考试计划照片目录
                    CopyFacePhoto(explanOB.ExamPlanID.ToString(), RadTextCertificateCode.Text);
                }

                //ImgCode.Src = GetFacePhotoPath(Request.QueryString["o"].ToString(), _ExamSignUpOB.CertificateCode); //绑定照片
                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(explanOB.ExamPlanID.ToString(), _ExamSignUpOB.CertificateCode))); //绑定照片;
                #endregion

                dtr.Commit();

                //条码路径
                if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/SignUpTable/{0}/", explanOB.ExamPlanID))))
                {
                    System.IO.Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/SignUpTable/{0}/", explanOB.ExamPlanID)));
                }

                string localPath = Server.MapPath(string.Format(@"~/Upload/SignUpTable/{0}/{1}.png", explanOB.ExamPlanID, _ExamSignUpOB.ExamSignUpID));
                if (System.IO.File.Exists(localPath) == false)//本地不存在
                {
                    UIHelp.CreateTCode(explanOB.ExamPlanID, _ExamSignUpOB.ExamSignUpID);
                }

                //显示社保比对结果
                ShowSheBao(_ExamSignUpOB);

                //更新时不能修改证件类型及号码
                RadComboBoxCertificateType.Enabled = false;
                RadTextCertificateCode.Enabled = false;

                //保存后才能导出和打印
                SetButtonEnable(_ExamSignUpOB.Status);

                BindFile(ApplyID);

                SetUploadFileType();

                if (explanOB.PostTypeID == 2)
                {
                    RefreshRadGridAnQuanPX();
                }

                UIHelp.WriteOperateLog(PersonName, UserID, "个人报名", string.Format("报名批次号：{0}。考试计划：{1}。岗位工种：{2}。报名人：{3}。",
                _ExamSignUpOB.SignUpCode,
                RadTextBoxExamPlanName.Text,
                RadTextPostID.Text,
                _ExamSignUpOB.WorkerName));

                if (explanOB.PostTypeID == 4000)
                {
                    UIHelp.layerAlert(Page, "保存成功，请上传相应扫描件后提交培训机构审核。", 6, 0);
                }
                else
                {
                    UIHelp.layerAlert(Page, "保存成功，请上传相应扫描件后提交所在企业网上确认，未在个人网上报名日期内提交所在企业网上确认的（或企业未在企业网上确认日期内确认提交住建委审核的），考试报名无效。", 6, 0);
                }
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "报名失败！", ex);
                return;
            }
        }

        /// <summary>
        /// 从历史报名记录拷贝附件
        /// </summary>
        /// <param name="_explanOB"></param>
        /// <param name="_ExamSignUpOB"></param>
        protected void ImportFileHistory(DbTransaction tran,ExamPlanOB _explanOB, ExamSignUpOB _ExamSignUpOB)
        {
            #region 附件要求说明
            //<p>附件要求说明：</p>
            //                            <p runat="server" id="p_PostTyppe1_qyfzr" visible="false">
            //                                三类人员安全生产考核（企业主要负责人）报考材料：<br />
            //                                ①企业法定代表人（系统自动比对法人信息）：<br />
            //                                &nbsp;1.考试报名表1份（须加盖聘用单位公章）；<br />
            //                                &nbsp;2.身份证正反面1份；<br />
            //                                <br />
            //                                ②其他企业主要负责人除以上材料还需提供：<br />
            //                                &nbsp;3.个人社保权益记录1份（网上社保比次日对一致的不用上传，不一致的，需上传社保权益记录表或相关证明材料）；<br />
            //                                &nbsp;4.相应的学历证书1份；<br />
            //                                &nbsp;5.相应的专业技术职称证书1份；<br />
            //                                &nbsp;其中以下几种特殊情况请，个人社保权益记录可上传相应证明材料作为替代：<br />
            //                                &nbsp;1）劳动派遣人员，由劳务派遣单位或人才交流中心提交其与第三方单位签订的劳务派遣合同和人员社保权益记录单；<br />
            //                                &nbsp;2）报考单位为总公司，分公司为其缴纳的社保予以认可，报考单位为分公司，总公司为其缴纳的社保予以认可。从企业名称上无法辨别总、分公司关系的，提交总、分公司相互关系材料扫描件；<br />
            //                                &nbsp;3）退休人员可提供退休证、新单位聘用劳动合同扫描件；<br />
            //                                &nbsp;4）农业户口的上传与新单位签订的劳动合同和居民户口簿（首页、本人页、变更页）。<br />
            //                            </p>
            //                            <p runat="server" id="p_PostTyppe1_xmfzr" visible="false">
            //                                三类人员安全生产考核（项目负责人）报考材料：<br />
            //                                1.考试报名表1份（须加盖聘用单位公章）；<br />
            //                                2.身份证正反面1份；<br />
            //                                3.个人社保权益记录1份（网上社保比次日对一致的不用上传，不一致的，需上传社保权益记录表或相关证明材料）；<br />

            //                            </p>
            //                            <p runat="server" id="p_PostTyppe1_aqy" visible="false">
            //                                三类人员安全生产考核（专职安全生产管理人员）报考材料：<br />
            //                                1.考试报名表1份（须加盖聘用单位公章）；<br />
            //                                2.身份证正反面1份；<br />
            //                                3.个人社保权益记录1份（网上社保次日比对一致的不用上传，不一致的，需上传社保权益记录表或相关证明材料）；<br />
            //                                4.中专（含高中、中技、职高）及以上文化程度或初级及以上技术职称1份；<br />
            //                                <br />
            //                                其中以下几种特殊情况请，个人社保权益记录可上传相应证明材料作为替代：<br />
            //                                ①劳动派遣人员，由劳务派遣单位或人才交流中心提交其与第三方单位签订的劳务派遣合同和人员社保权益记录单；<br />
            //                                ②报考单位为总公司，分公司为其缴纳的社保予以认可，报考单位为分公司，总公司为其缴纳的社保予以认可。从企业名称上无法辨别总、分公司关系的，提交总、分公司相互关系材料扫描件；<br />
            //                                ③退休人员可提供退休证、新单位聘用劳动合同扫描件；<br />
            //                                ④农业户口的上传与新单位签订的劳动合同和居民户口簿（首页、本人页、变更页）。<br />
            //                            </p>
            //                            <p runat="server" id="p_PostTyppe2" visible="false">
            //                                北京市建筑施工特种作业人员考核报考材料：<br />
            //                                1.考试报名表1份（须加盖聘用单位公章、培训单位公章）；<br />
            //                                2. 身份证正反面1份；<br />
            //                                3.初中及以上学历1份；<br />
            //                                4. 近3个月内二级乙等以上医院体检合格原件一份；
            //                                5.个人健康承若（下载模板：<a href="../Template/个人健康承诺.docx">【个人健康承诺模板.docx】</a>）。
            //                            </p>
            //                            <p runat="server" id="p_PostTyppe4" visible="false">
            //                                北京市住房和城乡建设行业技能人员考核报考材料：<br />
            //                                1.考试报名表1份（须加盖聘用单位公章）；<br />
            //                                <%--2. 身份证正反面1份；<br />
            //                                3.初中及以上学历1份；--%>
            //                            </p>
            #endregion 附件要求说明

            try
            {
                List<string> filetype = new List<string>();
                filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                filetype.Add(EnumManager.FileDataTypeName.考试报名表扫描件);
                filetype.Add(EnumManager.FileDataTypeName.劳动合同扫描件);
                filetype.Add(EnumManager.FileDataTypeName.技术职称扫描件);
                filetype.Add(EnumManager.FileDataTypeName.体检合格证明);
                filetype.Add(EnumManager.FileDataTypeName.个人健康承诺);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string f in filetype)
                {
                    sb.Append(string.Format(",'{0}'", f));
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                }


                //获取最近一次同工种考试报名附件信息（3个月内，同工种）
                string sql = @"SELECT count(*)
                            FROM [dbo].[ApplyFile] a
                            inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
                            where a.[ApplyID] in(
	                            select top 1 'KS-' + cast([EXAMSIGNUPID] as varchar(64)) 
	                            from [dbo].[VIEW_EXAMSIGNUP_NEW] 
	                            where certificatecode='{3}' and postid={0} and [EXAMSTARTDATE] >'{1}' and ExamSignUpID <{4}
	                            order by [EXAMSTARTDATE] desc
                            ) 
                            and f.DataType in({2})";

                int examCount = CommonDAL.SelectRowCount(string.Format(sql, _explanOB.PostID, _explanOB.ExamStartDate.Value.AddMonths(-3), sb, _ExamSignUpOB.CertificateCode, _ExamSignUpOB.ExamSignUpID));

                sql = @"INSERT INTO [dbo].[ApplyFile]([FileID] ,[ApplyID])
                            SELECT a.[FileID],'KS-{4}'
                            FROM [dbo].[ApplyFile] a
                            inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
                            where a.[ApplyID] in(
	                            select top 1 'KS-' + cast([EXAMSIGNUPID] as varchar(64)) 
	                            from [dbo].[VIEW_EXAMSIGNUP_NEW] 
	                            where certificatecode='{3}'  and postid={0} and [EXAMSTARTDATE] >'{1}' and ExamSignUpID <{4}
	                            order by [EXAMSTARTDATE] desc
                            ) 
                            and f.DataType in({2})";

                //拷贝指定类型附件
                if (examCount > 0)
                {
                    CommonDAL.ExecSQL(tran, string.Format(sql, _explanOB.PostID, _explanOB.ExamStartDate.Value.AddMonths(-12), sb, _ExamSignUpOB.CertificateCode, _ExamSignUpOB.ExamSignUpID));
                }
                else
                {
                    filetype = new List<string>();
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);

                    if (_explanOB.PostID.Value == 147//企业主要负责人
                 && IfFaRen == false)//非法定代表人提交大专以上学历、中级以上职称各1份；
                    {
                        filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                        filetype.Add(EnumManager.FileDataTypeName.技术职称扫描件);
                    }

                    if (_explanOB.PostID.Value == 6 || _explanOB.PostID.Value == 1123 || _explanOB.PostID.Value == 1125)//安全员C1、C2、C3
                    {
                        filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                        filetype.Add(EnumManager.FileDataTypeName.技术职称扫描件);

                    }
                    if (_explanOB.PostTypeID.Value == 2)//特种作业
                    {
                        filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);
                    }

                    sb.Remove(0, sb.Length);
                    foreach (string f in filetype)
                    {
                        sb.Append(string.Format(",'{0}'", f));
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1);
                    }

                    //获取最近一次同工种考试报名附件信息（1年内，所有工种）
                    sql = @"INSERT INTO [dbo].[ApplyFile]([FileID] ,[ApplyID])
                            SELECT a.[FileID],'KS-{0}'
                            FROM [dbo].[ApplyFile] a
                            inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
                            where a.[ApplyID] in(
	                            select top 1 'KS-' + cast([EXAMSIGNUPID] as varchar(64)) 
	                            from [dbo].[VIEW_EXAMSIGNUP_NEW] 
	                            where certificatecode='{1}' and [EXAMSTARTDATE] >'{2}' and ExamSignUpID <{0}
	                            order by [EXAMSTARTDATE] desc
                            ) 
                            and f.DataType in({3})";
                    CommonDAL.ExecSQL(tran, string.Format(sql, _ExamSignUpOB.ExamSignUpID, _ExamSignUpOB.CertificateCode, _explanOB.ExamStartDate.Value.AddMonths(-12), sb));
                }
            }
            catch(Exception ex)
            {
                Utility.FileLog.WriteLog("从历史报名记录拷贝附件失败！", ex);
            }
        }

        /// <summary>
        /// 格式化报名状态显示
        /// </summary>
        /// <param name="SignupStatus"></param>
        /// <param name="CheckDatePlan"></param>
        /// <returns></returns>
        protected string formatSignupStatus(string SignupStatus,DateTime? CheckDatePlan)
        {
            //if (CheckDatePlan.HasValue == false) return "预填写";
            ExamPlanOB _explanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
            switch(SignupStatus)
            {
                case "未提交":
                    return (_explanOB.PostTypeID == 4000) ? "未提交培训点审核" : "未提交单位审核";
                default:                  

                    if (_explanOB.ExamCardSendStartDate.Value.AddDays(-1) < DateTime.Now)//已经放准考证
                    {
                        if (_explanOB.StartCheckDate.Value < DateTime.Now)
                        {
                            return SignupStatus.Replace("退回修改", "退回").Replace("已缴费", "审核确认");
                        }
                        else
                        {
                            return SignupStatus.Replace("已缴费", "审核确认");
                        }
                    }
                    else//未放准考证
                    {
                        if (_explanOB.StartCheckDate.Value < DateTime.Now)
                        {
                            return "审核中";                           
                        }
                        else
                        {
                            return SignupStatus.Replace("已缴费", "审核确认");
                        }                       
                    }
                   
            }
        }
               
        //提交单位（培训点）确认
        protected void ButtonSendUnit_Click(object sender, EventArgs e)
        {
            ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];

            if(ButtonSendUnit.Text != "取消申报"//非取消申报
                && ViewState["PostTypeID"].ToString() == "1"//三类人
                && ((ViewState["PostID"].ToString() == "147" && IfFaRen == false) || ViewState["PostID"].ToString() != "147")//非法人报名A
                && (ViewState["继续提交"] == null )//未读8表提示并继续提交
                && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck == 0)//社保校验不合格或未校验
             )
            {
                #region  弹出提示，8秒阅读
                string TipHtml = null;

                if (ViewState["PostID"].ToString() == "6"//土建类专职安全生产管理人员
                    || ViewState["PostID"].ToString() == "1123"//机械类专职安全生产管理人员
                    || ViewState["PostID"].ToString() == "1125")//综合类专职安全生产管理人员
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构或劳务派遣单位缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }
                else
                {
                    TipHtml = @" 
<p style='text-align:left;'>
   <span style='text-indent:32px;'>您提交的社保校验申请不通过，不符合办理条件。如您已在申请单位本市或外省市分支机构缴纳申请之日上一个月的社会保险，请确定已按要求上传相关材料。请您确认是否继续提交？</span><br/>
</p>
";
                }


                p_ExamConvfirmDesc.InnerHtml = TipHtml;
                DivExamConfirm.Style.Add("display", "block");
                Telerik.Web.UI.RadScriptManager.RegisterStartupScript(Page, this.GetType(), "show15"
                    , string.Format(@"function show15() {{
            var myVar = setInterval(function () {{
                var num = $('#spanCount').text();
                num--;
                $('#spanCount').text(num);
                if (num == 0) {{
                    $('#spanCount').text('');
                    clearInterval(myVar);
                    $('#{0}').removeClass('btn_no');
                    $('#{0}').removeAttr('disabled');
                    $('#{1}').removeClass('btn_no');
                    $('#{1}').removeAttr('disabled');
                }}
            }}, 1000);
        }}
        show15();", ButtonYes.ClientID, ButtonNo.ClientID)
                    , true);

                #endregion  弹出提示，8秒阅读
            }
            else
            {
                ExamPlanOB explanOB = (ExamPlanOB)ViewState["ExamPlanOB"];

                #region 提交前校验

                if (explanOB.PostTypeID < 3 && o.ZACheckResult.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "尚未进行持证校验，请等待系统校验数据后再提交申请。");
                    return;
                }

                if (CheckBoxPromise.Checked == false)
                {
                    UIHelp.layerAlert(Page, "请勾选本人承诺！");
                    return;
                }

                if (explanOB.PostTypeID != 4000)
                {
                    if (RadDatePickerENT_ContractStartTime.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入劳动合同开始时间", 5, 0);
                        return;
                    }
                    if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入劳动合同结束时间", 5, 0);
                        return;
                    }
                    if (string.IsNullOrEmpty(RadioButtonListENT_ContractType.SelectedValue))
                    {
                        UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
                        return;
                    }
                    else if (RadioButtonListENT_ContractType.SelectedValue != "1" && RadioButtonListENT_ContractType.SelectedValue != "2" && RadioButtonListENT_ContractType.SelectedValue != "3")
                    {
                        UIHelp.layerAlert(Page, "请选择劳动合同类型", 5, 0);
                        return;
                    }

                    if (RadTextBoxUnitName.Text.Replace(" ", "").Replace("　", "") == "")
                    {
                        UIHelp.layerAlert(Page, "单位名称不能为空！");
                        return;
                    }
                    if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxUnitCode.Text) == false)
                    {
                        UIHelp.layerAlert(Page, "“组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                        return;
                    }
                }

                if (RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", "") == "")
                {
                    UIHelp.layerAlert(Page, "姓名不能为空！");
                    return;
                }

                if (ViewState["PostTypeID"].ToString() == "1" && string.IsNullOrEmpty(RadComboBoxJob.SelectedValue) == true)
                {
                    UIHelp.layerAlert(Page, "职务不能为空", 5, 0);
                    return;
                }

                if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
                {
                    if (RadTextCertificateCode.Text.Trim().Length != 18)
                    {
                        UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
                        return;
                    }
                    else if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                    {
                        UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                        return;
                    }
                }

                string mySex = RadioButtonMan.Checked == true ? "男" : "女";    //性别
                DateTime myBirthday = RadDatePickerBirthday.SelectedDate.Value; //出身日期

                if (ViewState["PostTypeID"].ToString() == "1")
                {
                    if (ViewState["PostID"].ToString() == "147")
                    {
                        if (RadComboBoxJob.SelectedItem.Text == "法定代表人")//职务
                        {
                            if (IfFaRen == false)//非法人
                            {
                                UIHelp.layerAlert(Page, "校验未通过：工商信息显示您并非报名单位企业法人，不能以法定代表人职务报考。");
                                return;
                            }

                            CertificateOB FROb = CertificateDAL.GetFRCertA(RadTextBoxUnitCode.Text.Trim());
                            if (FROb != null)
                            {
                                UIHelp.layerAlert(Page, string.Format("校验未通过：一个单位只能允许一人以法定代表人职务持有A证，报名单位已经存在法人A证【{0}】。", FROb.CertificateCode));
                                return;
                            }
                            //法人不受年龄显限制
                        }
                        else//非法人
                        {
                            //非企业法人，受年龄限制
                            if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                            {
                                UIHelp.layerAlert(Page, "您已超龄不予报考！");
                                return;
                            }

                            CertificateOB FROb = CertificateDAL.GetOtherNoFRCertA(RadTextCertificateCode.Text.Trim(), RadTextCertificateCode.Text.Trim());
                            if (FROb != null)
                            {
                                UIHelp.layerAlert(Page, string.Format("校验未通过： 不允许持有多本非法人职务的A本，你已持有非法人职务的A本【{0}】。", FROb.CertificateCode));
                                return;
                            }
                        }
                    }
                    else//B、C1、C2、C3
                    {
                        if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                        {
                            UIHelp.layerAlert(Page, "您已超龄不予报考！");
                            return;
                        }
                    }
                }
                else if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(ViewState["PostTypeID"]), RadTextCertificateCode.Text.Trim(), myBirthday, mySex) == true)
                {
                    UIHelp.layerAlert(Page, "您已超龄不予报考！");
                    return;
                }                


                bool AgeLimit = false;
                AgeLimit = ValidResourceIDLimit(RoleIDs, "SignUpWithoutAgeLimit");
                if (AgeLimit == false)
                {
                    //报名限制检查（所有类型证书）：
                    if (CertificateDAL.CheckRegular_SpecialOperator(RadDatePickerBirthday.SelectedDate.Value) == false)
                    {
                        UIHelp.layerAlert(Page, "年龄未满18周岁，不符合报名要求！");
                        return;
                    }
                }

                CertificateOB certificateob = CertificateDAL.GetCertificateOBObject(RadTextCertificateCode.Text.Trim(), Convert.ToInt32(ViewState["PostID"]), DateTime.Now);
                if (certificateob != null)
                {
                    //报名限制检查:不能在同一家公司取得多个企业负责人证书
                    if (ViewState["PostID"].ToString() == "147")
                    {
                        if (CertificateDAL.CheckRegular_UnitMaster(certificateob, RadTextBoxUnitCode.Text.Trim()) == false)
                        {
                            UIHelp.layerAlert(Page, "不能在同一单位取得多个“企业主要负责人”证书，不能报名！");
                            return;
                        }
                    }
                    else if (
                                (ViewState["PostTypeID"].ToString() == "4" //职业技能岗位
                                 || ViewState["PostTypeID"].ToString() == "4000"//新版职业技能岗位
                                )
                        && ViewState["PostID"].ToString() != "158"//村镇建筑工匠
                            && ViewState["PostID"].ToString() != "199")//普工
                    {
                        if (certificateob.SkillLevel == RadComboBoxSKILLLEVEL.Text)
                        {
                            UIHelp.layerAlert(Page, "已取得同类型同技术等级证书，尚未过期，不能报名！");
                            return;
                        }
                    }
                    else //报名限制检查:检查该人是否持有有效的（有效期未过）
                    {
                        UIHelp.layerAlert(Page, "已取得同类型证书，尚未过期，不能报名！");
                        return;
                    }
                }

                //报考“物业项目负责人”
                if (ViewState["PostID"].ToString() == "159")
                {
                    if (
                        //比对《在岗无证物业项目负责人》库，一致允许报考（姓名、身份证号）
                        //*****************************读取文件编码gb2132与UTF-8不一致，比较中文姓名有问题，需要改造
                        (UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(RadTextCertificateCode.Text.Trim().Replace("x", "X")) == false)// || UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(RadTextBoxWorkerName.Text.Trim()) == false)
                        &&
                        //不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）
                        (UIHelp.QueryWYQYFromBaseDB(Page).Contains(RadTextBoxUnitCode.Text.Trim().Replace("x", "X")) == false)
                        )
                    {
                        UIHelp.layerAlert(Page, "报名企业不在本市管理或核发的物业企业资质库中，也未在物业管理处提供的“在岗无证人员名录”内，不许报名。<br >如有疑问，请联系010-59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                        return;
                    }
                }

                //报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
                if (ViewState["PostID"].ToString() == "1021" || ViewState["PostID"].ToString() == "1024")
                {
                    if (UIHelp.QueryWYQYFromBaseDB(Page).Contains(RadTextBoxUnitCode.Text.Trim().Replace("x", "X")) == false)
                    {
                        UIHelp.layerAlert(Page, "报名企业（企业名称、组织机构代码）不在本市管理或核发的物业企业资质库中。<br >请您核对报名表中企业名称及组织机构代码是否正确，如有疑问，请联系010-59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                        return;
                    }
                }

                //证书锁定校验
                int countLock = CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATELOCK"
                    , string.Format(" and WorkerCertificateCode='{0}' and PostID={1} and CertificateID in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{2}')"
                    , RadTextCertificateCode.Text.Trim(), ViewState["PostID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd")));
                if (countLock > 0)
                {
                    UIHelp.layerAlert(Page, "系统中存在相同岗位工种证书，证书处于锁定中，解锁前不允许报名！解锁请联系北京市建筑业执业资格注册中心。");
                    return;
                }

                //人员锁定校验
                int personLock = CommonDAL.SelectRowCount("dbo.VIEW_WORKERLOCK"
                    , string.Format(" and CertificateCode='{0}' and LockStatus='加锁' and LockEndTime >'{1}'"
                    , RadTextCertificateCode.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd")));
                if (personLock > 0)
                {
                    UIHelp.layerAlert(Page, "您处于被锁定状态中，解锁前不允许报名！解锁请联系北京市建筑业执业资格注册中心。");
                    return;
                }

                //相同岗位类型一个月只能参加一个工种考试报名
                if (ExamSignUpDAL.CheckExamSignupCount(Convert.ToInt64(ViewState["ExamPlanID"]), Convert.ToInt32(ViewState["PostTypeID"]), ViewState["ExamStartDateYM"].ToString(), RadTextCertificateCode.Text.Trim()) > 0)
                {
                    UIHelp.layerAlert(Page, "您存在当月相同岗位类别其他工种考试报名，同一岗位类别一次只允许报名一个工种！。");
                    return;
                }


                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), true);

                //三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。
                if (ViewState["PostTypeID"].ToString() == "1")
                {

                    if (string.IsNullOrEmpty(UnitName))
                    {
                        UIHelp.layerAlert(Page, "您所在的企业不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。");
                        return;
                    }
                    if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                    {
                        UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                        return;
                    }

                    if (ViewState["PostID"].ToString() == "147"
                        || ViewState["PostID"].ToString() == "148")//（其中起重机械租赁企业只允许报考专职安全生产管理人员（C本），不能报考A本和B本）
                    {
                        List<string> zz = UnitDAL.GetUnitZZLBFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim());
                        if (zz.Count == 1 && zz[0] == "起重机械租赁企业")
                        {
                            UIHelp.layerAlert(Page, "根据相关政策起重机械租赁企业只允许报考专职安全生产管理人员（C1、C2、C3），不能报考A本和B本，您不满足报考要求。");
                            return;
                        }
                    }

                    //报名限制检查（项目负责人、专职安全生产管理人员）：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位
                    if (CertificateDAL.CheckRegular_ItemMaster(RadTextCertificateCode.Text.Trim(), RadTextBoxUnitCode.Text.Trim(), Convert.ToInt32(ViewState["PostID"]),RadComboBoxJob.SelectedItem.Text) == false)
                    {
                        UIHelp.layerAlert(Page, "同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员(C1、C2、C3)”证书，并且有C3不能再报考C1和C2，有C1或C2不能报考C3，您不符合要求，不能报名！");
                        return;
                    }
                }
                ////对特种作业人员和职业技能人员考核业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司
                if (ViewState["PostTypeID"].ToString() == "2")
                {
                    UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), false);
                    if (string.IsNullOrEmpty(UnitName))
                    {
                        UIHelp.layerAlert(Page, "您所在的企业不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。");
                        return;
                    }
                    if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Trim().Replace("（", "(").Replace("）", ")"))
                    {
                        UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                        return;
                    }

                    int count = CommonDAL.GetRowCount("ExamSignupTrain", string.Format(@" and [ExamSignUpID] = {0}", o.ExamSignUpID));//委培记录
                    if (count == 0)
                    {
                        UIHelp.layerAlert(Page, "尚未填写考前安全操作培训记录，请先填写再申报。", 5, 0);
                        return;
                    }

                }

                //项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
                if (ViewState["PostID"].ToString() == "148")
                {
                    DataTable jzs = UIHelp.GetJZS(Page, "本地");
                    if (jzs.Rows.Find(new string[] { RadTextCertificateCode.Text.Trim().Replace("x", "X"), RadTextBoxUnitCode.Text.Trim() }) == null
                        && jzs.Rows.Find(new string[] { Utility.Check.ConvertoIDCard18To15(RadTextCertificateCode.Text.Trim().Replace("x", "X")), RadTextBoxUnitCode.Text.Trim() }) == null)
                    {
                        UIHelp.layerAlert(Page, "报考项目负责人要求您必须取得本市企业建造师注册证书，查不到你的建造师注册信息，不允许报名。");
                        return;
                    }
                }

                if (explanOB.PostTypeID.Value == 1//三类人
                 || explanOB.PostTypeID.Value == 5)//专业技术员
                {

                    TJ_MissExamLockMDL _TJ_MissExamLockMDL = TJ_MissExamLockDAL.GetObject(RadTextCertificateCode.Text.Trim().Replace("x", "X"));
                    if (_TJ_MissExamLockMDL != null)
                    {
                        UIHelp.layerAlert(Page, string.Format("由于您一年内累积三次未参加考试，您的信息已被锁定一年，截止{0}不得报考我市建筑业从业人员考试。", _TJ_MissExamLockMDL.LockEndDate.Value.ToString("yyyy-MM-dd日")));
                        return;
                    }
                }

                ExamSignUpOB _Lock = ExamSignUpDAL.GetLockIng(RadTextCertificateCode.Text.Trim());
                {
                    if (_Lock != null)
                    {
                        UIHelp.layerAlertWithHtml(Page, string.Format("{3}{4}考试违规申报锁定，<br />锁定时间：{0} - {1}，<br />锁定原因：{2}"
                            , _Lock.LockTime.Value.ToString("yyyy.MM.dd")
                            , _Lock.LockEndTime.Value.ToString("yyyy.MM.dd")
                            , _Lock.LockReason.Replace("\r\n", "<br />")
                            , explanOB.ExamStartDate.Value.ToString("yyyy.MM.dd日")
                             , explanOB.PostName
                            ));
                        return;
                    }
                }

                //ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(RadTextCertificateCode.Text.Trim(), explanOB.ExamPlanID.Value);

                if (o == null || o.Status == EnumManager.SignUpStatus.SaveSignUp)
                {
                    if (DateTime.Now < explanOB.SignUpStartDate.Value || DateTime.Now > explanOB.SignUpEndDate.Value.AddDays(1))
                    {
                        UIHelp.layerAlert(Page, "不在报名时间范围内，无法提交报名。");
                        return;
                    }
                }
                else if (o.Status == EnumManager.SignUpStatus.ReturnEdit)
                {
                    if (DateTime.Now < explanOB.SignUpStartDate.Value || DateTime.Now > explanOB.SignUpEndDate.Value.AddDays(3))
                    {
                        UIHelp.layerAlert(Page, "不在报名时间范围内，无法提交报名。");
                        return;
                    }
                }

                #endregion 提交前校验

                #region 必须上传附件集合

                //<p runat="server" id="p_PostTyppe1" visible="false">一、三类人员安全生产考核报考材料：<br />
                //                                   1.考试报名表1份（须加盖聘用单位公章）；<br />
                //                                   2.劳动合同主要页及个人社保权益记录各1份；<br />
                //                                   3.身份证正反面1份；<br />
                //                                   4.报考企业主要负责人，法定代表人提交企业营业执照1份；非法定代表人提交大专以上学历、中级以上职称各1份；<br />
                //                                   5.报考专职安全生产管理人员提交中专（含高中、中技、职高）及以上文化程度或初级及以上技术职称1份。<br />
                //                               </p>
                //                               <p runat="server" id="p_PostTyppe2" visible="false">二、北京市建筑施工特种作业人员考核报考材料：<br />
                //                                   1.考试报名表1份（须加盖聘用单位公章）；<br />
                //                                   2. 身份证正反面1份；<br />
                //                                   3.初中及以上学历1份；<br />
                //                                   4. 近3个月内二级乙等以上医院体检合格原件一份。
                //                               </p>
                //                               <p runat="server" id="p_PostTyppe4" visible="false">三、北京市住房和城乡建设行业技能人员考核报考材料：<br />
                //                                   1.考试报名表1份（须加盖聘用单位公章）；<br />
                //                                   2. 身份证正反面1份；<br />
                //                                   3.初中及以上学历1份；
                //                               </p>

                if (ButtonSendUnit.Text != "取消申报")//提交单位确认
                {

                    if (explanOB.StartCheckDate.Value < DateTime.Now)
                    {
                        UIHelp.layerAlert(Page, "已进入审核阶段，无法申请报名！", 5, 0);
                        return;
                    }

                    System.Collections.Hashtable fj = null;//必须上传附件集合
                    System.Collections.Hashtable orFj = new System.Collections.Hashtable { };//多选一附件集合

                    switch (explanOB.PostTypeID.Value)
                    {
                        case 1://三类人
                            fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.考试报名表扫描件,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0}};
                            break;
                        case 2://特种作业
                            fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.考试报名表扫描件,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0},
                        {EnumManager.FileDataTypeName.学历证书扫描件,0},
                        {EnumManager.FileDataTypeName.体检合格证明,0},
                        {EnumManager.FileDataTypeName.个人健康承诺,0},
                         {EnumManager.FileDataTypeName.考前安全作业培训承诺书,0}};
                            break;
                        case 4://职业技能
                            //fj = new System.Collections.Hashtable{
                            //{EnumManager.FileDataTypeName.考试报名表扫描件,0},
                            //{EnumManager.FileDataTypeName.证件扫描件,0},
                            //{EnumManager.FileDataTypeName.学历证书扫描件,0}};
                            fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.考试报名表扫描件,0}
                        };
                            break;
                        case 4000://新版职业技能
                            fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.考试报名表扫描件,0}
                        };
                            break;
                    }

                    //已上传附件集合
                    DataTable dt = ApplyDAL.GetApplyFile(ApplyID);

                    //计数
                    foreach (DataRow r in dt.Rows)
                    {
                        //附件集合
                        if (orFj.Contains(r["DataType"].ToString()) == false)
                        {
                            orFj.Add(r["DataType"].ToString(), 1);
                        }
                        else
                        {
                            orFj[r["DataType"].ToString()] = Convert.ToInt32(orFj[r["DataType"].ToString()]) + 1;
                        }

                        //必须上传附件集合
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

                    if (explanOB.PostID.Value == 147)//企业主要负责人
                    {
                        string job = RadComboBoxJob.SelectedItem.Text;//职务
                        if (job != "法定代表人")//非法定代表人提交大专以上学历、中级以上职称各1份；
                        {
                            if (o.SignupPromise == 1)
                            {
                                if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.报考条件证明承诺书) == false)
                                {
                                    sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.报考条件证明承诺书));
                                }
                            }
                            else
                            {
                                if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.学历证书扫描件) == false)
                                {
                                    sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.学历证书扫描件));
                                }
                                if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.技术职称扫描件) == false)
                                {
                                    sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.技术职称扫描件));
                                }
                            }
                            //检查社保相关扫描件上传文件是否存在

                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false //没上传社保证明
                           && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                           )
                            {
                                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                            }
                        }
                        else//法人，需要上传营业执照(上传到社保栏目)
                        {

                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false)
                            {
                                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                            }
                        }
                    }

                    if (explanOB.PostID.Value == 6 || explanOB.PostID.Value == 1123 || explanOB.PostID.Value == 1125)//安全员C1、C2、C3
                    {
                        if (o.SignupPromise == 1)
                        {
                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.报考条件证明承诺书) == false)
                            {
                                sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.报考条件证明承诺书));
                            }
                        }
                        else
                        {
                            if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.学历证书扫描件) == false && ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.技术职称扫描件) == false)
                            {
                                sb.Append(string.Format("、“{0}或{1}”", EnumManager.FileDataTypeName.学历证书扫描件, EnumManager.FileDataTypeName.技术职称扫描件));
                            }
                        }

                        //检查社保相关扫描件上传文件是否存在

                        if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false  //没上传社保证明
                       && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                       )
                        {
                            sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                        }
                    }
                    if (explanOB.PostID.Value == 148)//B本
                    {
                        //检查社保相关扫描件上传文件是否存在

                        if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false   //没上传社保证明
                       && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                       )
                        {
                            sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                        }
                    }

                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1);
                        UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！", sb), 5, 0);
                        return;
                    }

                    bool ifWhiteFacePhoto = UIHelp.CheckIfWhiteBackgroudPhoto(Server.MapPath(GetFacePhotoPath(explanOB.ExamPlanID.ToString(), o.CertificateCode)));
                    if (ifWhiteFacePhoto == false)
                    {
                        UIHelp.layerAlert(Page, string.Format("电子证书要求提供近期一寸白底标准正面免冠证件照，请检查照片是否为白底照。", sb), 5, 0);
                        return;
                    }
                }

                #endregion 必须上传附件集合

                #region 提交
                try
                {
                    o.SignUpDate = DateTime.Now;
                    o.ModifyPersonID = PersonID;
                    o.ModifyTime = DateTime.Now;
                    o.FIRSTTRIALTIME = null;//单位审核时间
                    o.HireUnitAdvise = null;//单位意见
                    o.CheckDate = null;
                    o.CheckMan = null;
                    o.CheckResult = null;
                    o.CheckCode = null;
                    o.AcceptTime = null;
                    o.AcceptResult = null;
                    o.AcceptMan = null;
                    o.PayConfirmCode = null;
                    o.PayConfirmDate = null;
                    o.PayConfirmMan = null;
                    o.PayConfirmRult = null;


                    if (ButtonSendUnit.Text != "取消申报")
                    {
                        if (RadDatePickerWorkStartDate.SelectedDate != null) o.WorkStartDate = RadDatePickerWorkStartDate.SelectedDate.Value;
                        if (RadTextBoxWorkYearNumer.Text.Trim() != "") o.WorkYearNumer = Convert.ToInt32(RadTextBoxWorkYearNumer.Text.Trim());
                        o.PersonDetail = UIHelp.ToHalfCode(RadTextBoxPersonDetail.Text.Trim());


                        o.SKILLLEVEL = (RadComboBoxSKILLLEVEL.Text == "请选择" ? "" : RadComboBoxSKILLLEVEL.Text);
                        o.S_SEX = RadioButtonMan.Checked == true ? "男" : "女";    //性别
                        o.S_BIRTHDAY = RadDatePickerBirthday.SelectedDate.Value; //出身日期
                        o.S_CULTURALLEVEL = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                        o.S_PHONE = RadTextBoxPhone.Text.Trim(); //联系电话

                        bool IfChangeUnit = false;//是否修改了选择单位

                        if (explanOB.PostTypeID != 4000)
                        {
                            if (string.IsNullOrEmpty(o.UnitCode) == false && o.UnitCode != RadTextBoxUnitCode.Text.Trim())
                            {
                                IfChangeUnit = true;//变换了单位，需要重新核验社保
                                o.SheBaoCheck = null;
                            }
                            o.UnitName = Utility.Check.removeInputErrorChares(RadTextBoxUnitName.Text);    //单位名称
                            o.UnitCode = RadTextBoxUnitCode.Text.Trim();    //机构号码

                            o.ENT_ContractType = Convert.ToInt32(RadioButtonListENT_ContractType.SelectedValue);
                            //劳动合同开始时间
                            o.ENT_ContractStartTime = RadDatePickerENT_ContractStartTime.SelectedDate;
                            //劳动合同结束时间
                            if (RadioButtonListENT_ContractType.SelectedValue == "1" && RadDatePickerENT_ContractENDTime.SelectedDate.HasValue == true)
                            {
                                o.ENT_ContractENDTime = RadDatePickerENT_ContractENDTime.SelectedDate;
                            }
                            else
                            {
                                o.ENT_ContractENDTime = null;
                            }
                        }

                        if (trJob.Visible == true)
                        {
                            o.Job = RadComboBoxJob.SelectedItem.Text;
                        }


                        if (explanOB.PostID.ToString() == "147"
                            && IfFaRen == true
                            && RadComboBoxJob.SelectedItem.Text == "法定代表人")//A本比对法人库，免初审
                        {
                            o.FirstCheckType = 4;
                        }

                        o.Status = EnumManager.SignUpStatus.NewSignUp;
                        ExamSignUpDAL.Update(o);
                        if (IfChangeUnit == true)//清空社保核验结果，重新发起核验
                        {
                            ExamSignUpDAL.ClearSheBaoCheck(o.ExamSignUpID.Value);
                            ClearCheckFaRenOrSheBao();
                        }
                        if (explanOB.PostTypeID == 4000)
                        {
                            UIHelp.WriteOperateLog(PersonName, UserID, "提交报名培训点审核", string.Format("报名批次号：{0}。报名人：{1}。", o.SignUpCode, o.WorkerName));
                            UIHelp.layerAlert(Page, "提交培训点成功，请您立即联系培训点网上审核。");
                        }
                        else
                        {
                            UIHelp.WriteOperateLog(PersonName, UserID, "提交报名单位初审", string.Format("报名批次号：{0}。报名人：{1}。", o.SignUpCode, o.WorkerName));
                            if (PersonType == 2)//考生
                            {
                                UIHelp.layerAlert(Page, @"提交现单位审核成功，请您立即联系所在企业网上确认。所在企业未在企业网上确认日期内确认提交住建委审核的，考试报名无效。<br/><br/><b>特别提示：<br/>1、个人报名截止前，个人可以点击“取消申报”，及时修改、补充材料后再次提交企业确认；<br/>
个人报名截止后，个人在本次报名期间从未提交企业确认的，本次考试报名提交不成功。<br/>
2、企业确认截止前，企业驳回个人申请，个人应及时修改、补充材料后再次提交企业确认；<br/>
企业确认截止后，个人无法修改、补充材料，企业无法确认。<br/>
3、企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。<br/>
企业确认截止后，市住建委进行一次性审核。审核不通过的，个人无法进行修改、补充及提交，请申请人认真复核考试报名材料，确保符合审核要求。</b>"
                                    , string.Format(@"window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("exam"), Utility.Cryptography.Encrypt(o.ExamSignUpID.ToString())));
                            }
                            else
                            {
                                UIHelp.layerAlert(Page, "提交现单位审核成功，请您立即联系所在企业网上确认。所在企业未在企业网上确认日期内确认提交住建委审核的，考试报名无效。<br/><br/><b>特别提示：市住建委审核为一次性审核，审核不通过不能进行第二次修改提交，请申报人提交报名和企业确认时认真复核，确保所报材料符合要求。</b>");
                            }
                        }
                        trFuJanTitel.Visible = false;
                        trFuJan.Visible = false;
                    }
                    else//取消申报
                    {
                        if (o.Status == EnumManager.SignUpStatus.FirstChecked)
                        {
                            //企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。
                            //个人报名截止后，企业审核截止前，驳回状态申请个人可以补充材料重新提交
                            o.Status = EnumManager.SignUpStatus.ReturnEdit;
                        }
                        else
                        {
                            if (DateTime.Compare(explanOB.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) <= 0//大于报名开始日期
                                && DateTime.Compare(explanOB.SignUpEndDate.Value.AddDays(1), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0//小于报名截止日期
                            )
                            {
                                o.Status = EnumManager.SignUpStatus.SaveSignUp;
                            }
                            else
                            {
                                o.Status = EnumManager.SignUpStatus.ReturnEdit;
                            }
                        }
                        ExamSignUpDAL.Update(o);
                        UIHelp.WriteOperateLog(PersonName, UserID, "取消提交考试报名", string.Format("报名批次号：{0}。报名人：{1}。", o.SignUpCode, o.WorkerName));
                        UIHelp.layerAlert(Page, "取消报名成功。", 6, 0);
                    }
                }
                catch (Exception ex)
                {

                    UIHelp.WriteErrorLog(Page, "提交报名到审核单位失败！", ex);
                    return;
                }

                #endregion 提交

                SetButtonEnable(o.Status);
                ViewState["ExamSignUpOB"] = o;
                LabelCheckStep.Text = string.Format("当前审核阶段：{0}", formatSignupStatus(o.Status, o.CheckDatePlan));
                BindFile(ApplyID);
                BindCheckHistory(o.ExamSignUpID.Value);

                ViewState["继续提交"] = null;
            }
        }

        //操作按钮控制
        /// <summary>
        /// 操作按钮控制
        /// </summary>
        /// <param name="ApplyStatus"></param>
        protected void SetButtonEnable(string SignUpStatus)
        {
            //SetStep(ApplyStatus);
            ExamPlanOB _ExamPlanOB = ViewState["ExamPlanOB"] as ExamPlanOB;
            switch (SignUpStatus)
            {
                case "":
                    btnSave.Enabled = true;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonSendUnit.Enabled = false;//提交单位确认   
                    ButtonSendUnit.Text = "提交单位确认"; 
                    ButtonDelete.Enabled = false;//删除报名                  
                    break;
                case EnumManager.SignUpStatus.SaveSignUp:
                    ButtonSendUnit.Text = "提交单位确认";
                  
                    if (DateTime.Compare(_ExamPlanOB.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) <= 0//大于报名开始日期
                        && DateTime.Compare(_ExamPlanOB.SignUpEndDate.Value.AddDays(1), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0//小于报名截止日期
                     )
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;

                        btnSave.Enabled = true;//保 存
                        ButtonExport.Enabled = true;//导出打印
                        ButtonDelete.Enabled = true;//删除报名
                        ButtonSendUnit.Enabled = true;//允许提交单位确认                    
                    }
                    else
                    {
                        ButtonSendUnit.Enabled = false;//禁止提交单位确认
                        btnSave.Enabled = false;//保 存
                        ButtonExport.Enabled = false;//导出打印
                        ButtonDelete.Enabled = true;//删除报名
                    }                  
                    break;
                case EnumManager.SignUpStatus.ReturnEdit:                                    
                    ButtonSendUnit.Text = "提交单位确认";

                    if (DateTime.Compare(_ExamPlanOB.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) <= 0//大于报名开始日期
                        && DateTime.Compare(_ExamPlanOB.SignUpEndDate.Value.AddDays(3), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0//小于企业确认截止日期（报名截止+2天）
                    )
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;

                        btnSave.Enabled = true;//保 存
                        ButtonExport.Enabled = true;//导出打印  
                        ButtonDelete.Enabled = true;//删除报名
                        ButtonSendUnit.Enabled = true;//允许提交单位确认 
                    }
                    else
                    {
                        ButtonSendUnit.Enabled = false;//禁止提交单位确认
                        btnSave.Enabled = false;//保 存
                        ButtonExport.Enabled = false;//导出打印
                        ButtonDelete.Enabled = true;//删除报名
                    }
                    break;
                case EnumManager.SignUpStatus.NewSignUp:
                case EnumManager.SignUpStatus.FirstChecked:
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印                   
                    ButtonSendUnit.Text = "取消申报";
                    ButtonDelete.Enabled = true;//删除报名   

                    if (DateTime.Compare(_ExamPlanOB.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) <= 0//大于报名开始日期
                       && DateTime.Compare(_ExamPlanOB.SignUpEndDate.Value.AddDays(3), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0//小于企业确认截止日期（报名截止+2天）
                   )
                    {
                        ButtonSendUnit.Enabled = true;//取消报名
                    }
                    else
                    {
                        ButtonSendUnit.Enabled = false;//禁止取消报名
                    }
                    break;               
                default:
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonSendUnit.Enabled = false;//提交单位确认                    
                    ButtonDelete.Enabled = false;//删除报名               
                    break;
            }

            if (_ExamPlanOB.PostTypeID==4000)
            {
                if(ButtonSendUnit.Text == "提交单位确认")
                {
                    ButtonSendUnit.Text = "提交培训机构";
                }
            }

            if (DateTime.Compare(_ExamPlanOB.SignUpEndDate.Value.AddDays(3), Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) <= 0)//大于企业确认截止日期（即报名截止 +2天）
            {
                btnSave.Enabled = false;//保 存
                ButtonExport.Enabled = false;//导出打印
                ButtonSendUnit.Enabled = false;//提交单位确认          
            }

            if (_ExamPlanOB.PostTypeID == 2)//特种作业
            {
                if (ViewState["ExamSignUpOB"] != null)
                {
                    ExamSignUpOB _ExamSignUpOB = (ExamSignUpOB)ViewState["ExamSignUpOB"];

                    trSafteTrain.Visible = true;

                    if (SignUpStatus == EnumManager.SignUpStatus.SaveSignUp || SignUpStatus == EnumManager.SignUpStatus.ReturnEdit)
                    {
                        RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                        RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                        RadGridAnQuanPX.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                        RadGridAnQuanPX.Rebind();
                    }
                    else
                    {
                        RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("Edit").Visible = false;
                        RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("Delete").Visible = false;
                        RadGridAnQuanPX.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        RadGridAnQuanPX.Rebind();
                    }
                }
                else
                {
                    trSafteTrain.Visible = false;
                }
            }

            btnSave.CssClass = btnSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExport.CssClass = ButtonExport.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonSendUnit.CssClass = ButtonSendUnit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
        }
        
        //删除未审核的报名
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(RadTextCertificateCode.Text.Trim(), Convert.ToInt64(ViewState["ExamPlanID"]));
            if (_ExamSignUpOB == null) return;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                ExamSignUp_DelDAL.Insert(tran, _ExamSignUpOB.ExamSignUpID.Value, PersonName, DateTime.Now);
                ExamSignUpDAL.Delete(tran, _ExamSignUpOB.ExamSignUpID.Value);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Commit();
                UIHelp.WriteErrorLog(Page, "取消报名信息失败！", ex);
                return;
            }

            trSafteTrain.Visible = false;

            UIHelp.WriteOperateLog(PersonName, UserID, "取消报名", string.Format("报名批次号：{0}。考试计划：{1}。岗位工种：{2}。报名人：{3}。",
              _ExamSignUpOB.SignUpCode,
              RadTextBoxExamPlanName.Text,
              RadTextPostID.Text,
              _ExamSignUpOB.WorkerName));
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cancel", "alter('取消报名成功！');", true);
            //UIHelp.layerAlert(Page,"cancel", "取消报名成功！");

            ViewState["ExamSignUpOB"] = null;
            Response.Redirect(string.Format("ExamSign.aspx?o={0}&c=cancel", Request["o"]), false);
        }

        //准备导出或打印标签替换数据
        protected System.Collections.Generic.Dictionary<string, string> GetExportData()
        {
            System.Collections.Generic.Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("SignUpCode", lblSignUpCode.Text);//报名批号
            list.Add("SignUpDate", myExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd"));//报名时间
            list.Add("CertificateCode", RadTextCertificateCode.Text);//证件编号
            list.Add("WorkerName", RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", ""));//姓名
            list.Add("Sex", RadioButtonMan.Checked == true ? "男" : "女");//性别
            list.Add("Age", RadDatePickerBirthday.SelectedDate.HasValue == false ? "" : Convert.ToString(DateTime.Now.Year - RadDatePickerBirthday.SelectedDate.Value.Year));//年龄
            list.Add("CulturalLevel", (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text));//文化程度
            list.Add("UnitCode", RadTextBoxUnitCode.Text);//组织代码
            list.Add("UnitName", RadTextBoxUnitName.Text.Replace(" ", "").Replace("　", ""));//企业名称
            list.Add("PostID", RadTextPostID.Text);//工种
            list.Add("WorkStartDate", RadDatePickerWorkStartDate.SelectedDate.HasValue == false ? "" : RadDatePickerWorkStartDate.SelectedDate.Value.ToString("yyyy年MM月dd日"));//工作开始时间
            list.Add("PersonDetail", (RadTextBoxPersonDetail.Text.Length > 180 ? RadTextBoxPersonDetail.Text.Substring(0, 180) : RadTextBoxPersonDetail.Text));//工作简历

            //list.Add("HireUnitAdvise", RadTextBoxHireUnitAdvise.Text);//聘用单位意见
            //list.Add("AdminUnitAdvise", RadTextBoxAdminUnitAdvise.Text);//考核发证单位意见
            list.Add("Phone", RadTextBoxPhone.Text);//联系电话
            list.Add("SKILLLEVEL", RadComboBoxSKILLLEVEL.Text);//技术等级
            list.Add("ImageName", RadTextCertificateCode.Text);//照片名称
            list.Add("Img_FacePhoto", GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), RadTextCertificateCode.Text));//绑定照片FacePhoto.ImageUrl

            //xml换行
            string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

            ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(RadTextCertificateCode.Text, Convert.ToInt64(ViewState["ExamPlanID"]));

             list.Add("Desc", string.Format("申请人：{0}",HiddenFieldApplyMan.Value));

            ////初审点信息
            //if (ViewState["PostTypeID"].ToString() == "1"//三类人
            //  || ViewState["PostTypeID"].ToString() == "5")//专业技术员
            //{
            //    list.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}。{3}", HiddenFieldApplyMan.Value, xmlBr, ViewState["PlaceName"]
            //        , _ExamSignUpOB.Status == EnumManager.SignUpStatus.NewSignUp ? string.Format("请您 {0}到现场进行审核。{1}", LabelCheckDatePlan.Text, (_ExamSignUpOB.FirstCheckType.Value == -1 ? "由于您一年内上次未参加考试，本次须现场审核报考材料并出具上次考试缺考原因的证明材料。" : "")) : "您已通过系统审核，无需到现场审核考试材料。"
            //        ));//备注
            //}
            //else
            //{
            //    list.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}", HiddenFieldApplyMan.Value, xmlBr, (HiddenFieldFirstCheck.Value == HiddenFieldApplyMan.Value) ? HiddenFieldApplyMan.Value : "东、西部报名审核点"));//备注
            //}


             string TCodePath = string.Format(@"../Upload/SignUpTable/{0}/{1}.png", ViewState["ExamPlanID"], _ExamSignUpOB.ExamSignUpID);
            if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
            {
                UIHelp.CreateTCode(ViewState["ExamPlanID"], _ExamSignUpOB.ExamSignUpID);
            }
            //条码
            list.Add("ImageTCodeName", RadTextCertificateCode.Text);
            list.Add("Img_TCode", TCodePath);

            return list;
        }

        private string GetCreateMan(string CertificateCode)
        {
            ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(CertificateCode, Convert.ToInt64(ViewState["ExamPlanID"]));
            if (_ExamSignUpOB == null) return PersonName;

            if (_ExamSignUpOB.WorkerID == _ExamSignUpOB.CreatePersonID) return _ExamSignUpOB.WorkerName;

            if (_ExamSignUpOB.UnitID == _ExamSignUpOB.CreatePersonID) return _ExamSignUpOB.UnitName;

            if (_ExamSignUpOB.TrainUnitID == _ExamSignUpOB.CreatePersonID) return _ExamSignUpOB.S_TRAINUNITNAME;

            return PersonName;
        }

        ////打印报名表
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc", string.Format("~/UpLoad/SignUpTable/{0}/报名表_{1}.doc", Request["o"], RadTextCertificateCode.Text.Trim()), GetExportData());
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/SignUpTable/{0}/报名表_{1}.doc');", Request["o"], RadTextCertificateCode.Text.Trim(), RootUrl), true);
        //}

        //导出报名表
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //CheckSaveDirectory();
            //if (ViewState["PostTypeID"].ToString() == "2")
            //{
            //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/特种作业报名表.doc", string.Format("~/UpLoad/SignUpTable/{0}/报名表_{1}.doc", ViewState["ExamPlanID"], RadTextCertificateCode.Text.Trim()), GetExportData());
            //}
            //else
            //{
            //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc", string.Format("~/UpLoad/SignUpTable/{0}/报名表_{1}.doc", ViewState["ExamPlanID"], RadTextCertificateCode.Text.Trim()), GetExportData());
            //}

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl("报名表", string.Format("~/UpLoad/SignUpTable/{0}/报名表_{1}.doc", ViewState["ExamPlanID"], RadTextCertificateCode.Text.Trim())));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

            ExamPlanOB _explanOB = ViewState["ExamPlanOB"] as ExamPlanOB;

            ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];

            var ht = PrintDocument.GetProperties(o);

            //考前安全操作培训记录
            DataTable dt = null;
            if (_explanOB.PostTypeID == 2)
            {
                if (o.SafeTrainType == "委托培训机构" &&
                        (string.IsNullOrEmpty(o.SafeTrainUnit) == true
                        || string.IsNullOrEmpty(o.SafeTrainUnitCode) == true
                        || string.IsNullOrEmpty(o.SafeTrainUnitOfDept) == true
                        || o.SafeTrainUnitValidEndDate.HasValue == false)
                    )
                {
                    UIHelp.layerAlert(Page, "选择“委托培训机构”类型必须填写：培训机构名称、办学许可证编号、办学许可证有效期、办学许可证发证机关。", 5, 0);
                    return;
                }

                dt = CommonDAL.GetDataTable(string.Format(@"SELECT convert(varchar(10),[TrainDateStart],21) +'至' + convert(varchar(10),[TrainDateEnd],21),[TrainType],[TrainName],[TrainWay],[Period] 
                                                            from [dbo].[ExamSignupTrain]
                                                            where [ExamSignUpID] = {0}
                                                            order by [DataNo]", o.ExamSignUpID));//委培记录
                if(dt==null || dt.Rows.Count==0)
                {
                    UIHelp.layerAlert(Page, "尚未填写考前安全操作培训记录，请先填写再导出。", 5, 0);
                    return;
                }
                else
                {
                    int sumPeriod = 0;
                    foreach (DataRow  r in dt.Rows)
                    {
                        sumPeriod += Convert.ToInt32(r["Period"]);
                    }
                    ht["SumPeriod"] = sumPeriod;

                    ht["SafeTrainType"] = o.SafeTrainType == "委托培训机构" ? "□自行   ☑委托培训机构" : "☑自行   □委托培训机构";
                    if (o.SafeTrainType == "委托培训机构")
                    {
                        ht["SafeTrainUnit"] = o.SafeTrainUnit;
                        ht["SafeTrainUnitCode"] = o.SafeTrainUnitCode;
                        ht["SafeTrainUnitOfDept"] = o.SafeTrainUnitOfDept;
                        ht["SafeTrainUnitValidEndDate"] = o.SafeTrainUnitValidEndDate.Value.ToString("yyyy年MM月dd日");
                    }
                    else
                    {
                        ht["SafeTrainUnit"] = "";
                        ht["SafeTrainUnitCode"] = "";
                        ht["SafeTrainUnitOfDept"] = "";
                        ht["SafeTrainUnitValidEndDate"] = "";
                    }

                    ht["tableList"] = new List<DataTable> { dt };

                    //表格的索引
                    ht["tableIndex"] = new List<int> { 2 };
                    //行的索引
                    ht["insertIndex"] = new List<int> { 1};
                    ht["ContainsHeader"] = new List<bool> { true };
                    ht["isCtable"] = true;
                }
            }

           
            ht["Age"]=RadDatePickerBirthday.SelectedDate.HasValue == false ? "" : Convert.ToString(DateTime.Now.Year - RadDatePickerBirthday.SelectedDate.Value.Year);//年龄
            ht["PostID"] = RadTextPostID.Text;
            ht["WorkStartDate"] = RadDatePickerWorkStartDate.SelectedDate.HasValue == false ? "" : RadDatePickerWorkStartDate.SelectedDate.Value.ToString("yyyy-MM-dd");//工作开始时间
            ht["SignUpDate"] = myExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd");//报名日期
            ht["ENT_ContractStartTime"] = o.ENT_ContractStartTime == null ? "      " : o.ENT_ContractStartTime.Value.ToString("yyyy年MM月dd日");
            ht["ENT_ContractENDTime"] = o.ENT_ContractENDTime == null ? "      " : o.ENT_ContractENDTime.Value.ToString("yyyy年MM月dd日");
            ht["photo"] = GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), RadTextCertificateCode.Text);
            if(string.IsNullOrEmpty(o.Job)==false)
            {
                ht["Job"] = o.Job;
            }

            switch (o.ENT_ContractType)
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
            if (o.SignupPromise.HasValue)
            {
                switch (o.SignupPromise)
                {
                    case 1:
                        ht["Yes"] = "☑";
                        ht["No"] = "□";
                        break;

                    case 0:
                        ht["Yes"] = "□";
                        ht["No"] = "☑";
                        break;
                }
            }
            else
            {
                ht["Yes"] = "□";
                ht["No"] = "☑";
            }

            string sourceFile = "";
            if (ViewState["PostTypeID"].ToString() == "2")
            {
                sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_特种作业.docx");
            }
            else if (ViewState["PostTypeID"].ToString() == "4000")
            {
                sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_职业技能.docx");
            }
            else
            {
                switch (Convert.ToInt32(ViewState["PostID"]))
                {
                    case 6://土建安全员    
                    case 1123://机械安全员 
                    case 1125://综合安全员 
                        sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_承诺.docx");
                        break;
                    case 147://企业主要负责人 
                        if (IfFaRen == false)
                        {
                            sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_承诺.docx");
                        }
                        else
                        {
                            sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_非承诺.docx");
                        }
                        break;
                    default:
                        sourceFile = HttpContext.Current.Server.MapPath("~/Template/报名表_非承诺.docx");
                        break;
                }

            }
            PrintDocument.CreateDataToWordByHashtable(sourceFile, string.Format("报名表_{0}_{1}",  RadTextCertificateCode.Text.Trim(),DateTime.Now.ToString("yyyyMMdd")), ht);


        }

        //检查临时文件路径
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()));
        }

        //将半角转换为全角
        public string ToDBC(string strInput)
        {
            /*全角字符从的unicode编码从65281~65374,半角字符从的unicode编码从33~126   
        差值65248,空格比较特殊,全角为12288,半角为32 */
            char[] c = strInput.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                }
                else if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
                if (checkString(c[i].ToString()))
                {
                    UIHelp.layerAlert(Page, "不能输入特殊字符！");
                    break;
                }
            }
            return new string(c);
        }

        //输入组织机构代码是检验
        protected void RadTextBoxUnitCode_KeyPress(object sender, EventArgs e)
        {
            ToDBC(RadTextBoxUnitCode.Text.ToString());
        }

        /// <summary>
        /// 严整特殊字符
        /// </summary>
        public static bool checkString(string source)
        {
            Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\-、；：。，》《]");
            return regExp.IsMatch(source);
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
            UIHelp.WriteOperateLog(UserName, UserID, "考试报名申请表附件删除成功", string.Format("申请批号：{0}，文件名称：{1}。", lblSignUpCode.Text,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileName"]));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        //变换请选择学历和职称证明方式
        protected void RadioButtonListSignupPromise_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];
            if (RadioButtonListSignupPromise.SelectedValue == "0")
            {
                //div_bktjzm.Visible = false;
                //div_XueLi.Visible = true;
                //div_JiShuZhiCheng.Visible = true;
                div_bktjzm.Style.Add("display", "none");
                div_XueLi.Style.Remove("display");
                div_JiShuZhiCheng.Style.Remove("display");
            }
            else
            {
                //div_bktjzm.Visible = true;
                //div_XueLi.Visible = false;
                //div_JiShuZhiCheng.Visible = false;
                div_bktjzm.Style.Remove("display");
                div_XueLi.Style.Add("display", "none");
                div_JiShuZhiCheng.Style.Add("display", "none");
            }
            if (o.SignupPromise.HasValue == false || RadioButtonListSignupPromise.SelectedValue != o.SignupPromise.ToString())
            {
                try
                {
                    ExamSignUpDAL.UpdateSignupPromise(o.ExamSignUpID.Value, Convert.ToInt32(RadioButtonListSignupPromise.SelectedValue));
                    o.SignupPromise = Convert.ToInt32(RadioButtonListSignupPromise.SelectedValue);
                    ViewState["ExamSignUpOB"] = o;
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "更新选择学历和职称证明方式失败！", ex);
                    return;
                }
            }
        }

        //下载报考条件证明承诺书
        protected void ButtonOutPutSignupPromise_Click(object sender, EventArgs e)
        {
            ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];
            var ht = PrintDocument.GetProperties(o);

            string sourceFile = HttpContext.Current.Server.MapPath("~/Template/报考条件证明告知承诺书.docx");
            PrintDocument.CreateDataToWordByHashtable(sourceFile, string.Format("报考条件证明告知承诺书_{0}_{1}", RadTextCertificateCode.Text.Trim(), DateTime.Now.ToString("yyyyMMdd")), ht);

        }

        //选择劳动合同类型
        protected void RadioButtonListENT_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListENT_ContractType.SelectedValue == "1")
            {
                RadDatePickerENT_ContractENDTime.Style.Remove("display");
                LabelJZSJ.Style.Remove("display");
                ValidatorENT_ContractENDTime.Enabled = true;
            }
            else
            {
                RadDatePickerENT_ContractENDTime.Style.Add("display", "none");
                LabelJZSJ.Style.Add("display", "none");
                ValidatorENT_ContractENDTime.Enabled = false;
            }
        }

        //变换职务选择
        protected void RadComboBoxJob_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetUploadFileType();
        }

        //继续提交
        protected void ButtonYes_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
            ViewState["继续提交"] = true;
            ButtonSendUnit_Click(sender, e);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SendUnit", "__doPostBack('SendUnit', '');", true);
        }

        //取消提交
        protected void ButtonNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
        }

        //绑定待编辑年度安全生产教育培训记录
        protected void RadGridAnQuanPX_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                //if (ViewState["PostTypeID"].ToString() == "1")//三类人员
                //{
                //    GridEditableItem editedItem = e.Item as GridEditableItem;
                //    Control trTrainUnit = editedItem.Cells[0].FindControl("trTrainUnit") as Control;
                //    trTrainUnit.Visible = false;
                //}

                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    try
                    {
                        GridEditableItem editedItem = e.Item as GridEditableItem;
                        Button ButtonSave = editedItem.Cells[0].FindControl("ButtonSave") as Button;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

                        Int64 id = Convert.ToInt64(RadGridAnQuanPX.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
                        ExamSignupTrainMDL _ExamSignupTrainMDL = ExamSignupTrainDAL.GetObject(id);
                        ViewState["ExamSignupTrainMDL"] = _ExamSignupTrainMDL;
                        UIHelp.SetData(editedItem, _ExamSignupTrainMDL);

                        RadioButtonList RadioButtonListTrainType = editedItem.Cells[0].FindControl("RadioButtonListTrainType") as RadioButtonList;
                        RadioButtonListTrainType.Items.FindByValue(_ExamSignupTrainMDL.TrainType).Selected = true;

                        RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
                        RadioButtonListTrainWay.Items.FindByValue(_ExamSignupTrainMDL.TrainWay).Selected = true;
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "读取考前安全作业培训记录失败！", ex);
                        return;
                    }
                }
            }
        }

        //新增年度安全生产教育培训记录
        protected void RadGridAnQuanPX_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            ExamPlanOB explanOB =(ExamPlanOB)ViewState["ExamPlanOB"];

            ExamSignUpOB _ExamSignUpOB = (ExamSignUpOB)ViewState["ExamSignUpOB"];

            ExamSignupTrainMDL _ExamSignupTrainMDL = new ExamSignupTrainMDL();
            RadNumericTextBox RadNumericTextBoxDataNo = editedItem.Cells[0].FindControl("RadNumericTextBoxDataNo") as RadNumericTextBox;
            _ExamSignupTrainMDL.DataNo = Convert.ToInt32(RadNumericTextBoxDataNo.Value);
            RadDatePicker RadDatePickerTrainDateStart = editedItem.Cells[0].FindControl("RadDatePickerTrainDateStart") as RadDatePicker;
            _ExamSignupTrainMDL.TrainDateStart = RadDatePickerTrainDateStart.SelectedDate;
            RadDatePicker RadDatePickerTrainDateEnd = editedItem.Cells[0].FindControl("RadDatePickerTrainDateEnd") as RadDatePicker;
            _ExamSignupTrainMDL.TrainDateEnd = RadDatePickerTrainDateEnd.SelectedDate;
            RadTextBox RadTextBoxTrainName = editedItem.Cells[0].FindControl("RadTextBoxTrainName") as RadTextBox;
            _ExamSignupTrainMDL.TrainName = UIHelp.ToHalfCode(RadTextBoxTrainName.Text);
            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _ExamSignupTrainMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;

            RadioButtonList RadioButtonListTrainType = editedItem.Cells[0].FindControl("RadioButtonListTrainType") as RadioButtonList;
            _ExamSignupTrainMDL.TrainType = RadioButtonListTrainType.SelectedValue;
            RadNumericTextBox RadNumericTextBoxPeriod = editedItem.Cells[0].FindControl("RadNumericTextBoxPeriod") as RadNumericTextBox;
            _ExamSignupTrainMDL.Period = Convert.ToInt32(RadNumericTextBoxPeriod.Value);
            _ExamSignupTrainMDL.cjsj = DateTime.Now;
            _ExamSignupTrainMDL.ExamSignUpID = _ExamSignUpOB.ExamSignUpID;

            if (_ExamSignupTrainMDL.TrainDateEnd > explanOB.SignUpEndDate.Value || _ExamSignupTrainMDL.TrainDateEnd < explanOB.SignUpEndDate.Value.AddYears(-1))
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "安全操作培训必须在考试报名前1年内，你填写的记录不符合要求。", 5, 0);
                return;
            }

            try
            {
                ExamSignupTrainDAL.Insert(_ExamSignupTrainMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加考前安全作业培训记录失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "添加考前安全作业培训记录", string.Format("数据ID：{0}。", _ExamSignupTrainMDL.DetailID));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //修改年度安全生产教育培训记录
        protected void RadGridAnQuanPX_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadTextBox RadTextBoxTrainName = editedItem.Cells[0].FindControl("RadTextBoxTrainName") as RadTextBox;

            ExamPlanOB explanOB = (ExamPlanOB)ViewState["ExamPlanOB"];

            ExamSignupTrainMDL _ExamSignupTrainMDL = (ExamSignupTrainMDL)ViewState["ExamSignupTrainMDL"];
            UIHelp.GetData(editedItem, _ExamSignupTrainMDL);

            RadioButtonList RadioButtonListTrainType = editedItem.Cells[0].FindControl("RadioButtonListTrainType") as RadioButtonList;
            _ExamSignupTrainMDL.TrainType = RadioButtonListTrainType.SelectedValue;

            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _ExamSignupTrainMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;

            if (_ExamSignupTrainMDL.TrainDateEnd > explanOB.SignUpEndDate.Value || _ExamSignupTrainMDL.TrainDateEnd < explanOB.SignUpEndDate.Value.AddYears(-1))
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "安全操作培训必须在考试报名前1年内，你填写的记录不符合要求。", 5, 0);
                return;
            }


            try
            {
                ExamSignupTrainDAL.Update(_ExamSignupTrainMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更新考前安全作业培训记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新考前安全作业培训记录", string.Format("数据ID：{0}。", _ExamSignupTrainMDL.DetailID));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //删除年度安全生产教育培训记录
        protected void RadGridAnQuanPX_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 DetailID = Convert.ToInt64(RadGridAnQuanPX.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
            try
            {
                ExamSignupTrainDAL.Delete(DetailID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除考前安全作业培训记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除考前安全作业培训记录", string.Format("数据ID：{0}。", DetailID));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //提供年度安全生产教育培训记录
        protected void RadGridAnQuanPX_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshRadGridAnQuanPX();
        }

        /// <summary>
        /// 绑定考前安全操作培训记录
        /// </summary>
        protected void RefreshRadGridAnQuanPX()
        {
            try
            {
                ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];
                DataTable dt = ExamSignupTrainDAL.GetList(0,100, string.Format(" and ExamSignUpID={0}", o.ExamSignUpID),"DataNo");//委培记录
                RadGridAnQuanPX.DataSource = dt;

                trSafteTrain.Visible = true;

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取考前安全操作培训记录失败！", ex);
                return;
            }
        }

        //变换安全作业培训类型
        protected void RadioButtonListSafeTrainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonListSafeTrainType.SelectedValue=="委托培训机构")
            {
                trTrainUnit1.Visible = true;
                trTrainUnit2.Visible = true;
            }
            else
            {
                trTrainUnit1.Visible = false;
                trTrainUnit2.Visible = false;
            }
        }

        //protected void RadTextBoxPersonDetail_TextChanged(object sender, EventArgs e)
        //{
        //    RadTextBoxPersonDetail.Text = UIHelp.ToFullWidth(RadTextBoxPersonDetail.Text);
        //}

      
    }
}
