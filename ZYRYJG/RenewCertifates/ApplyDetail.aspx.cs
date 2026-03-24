using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.RenewCertifates
{
    public partial class ApplyDetail : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            //setFistCheckUnit(242);//区县建委
            //setFistCheckUnit(246);//市属集团总公司
            //setFistCheckUnit(247);//中央驻京单位
            setFistCheckUnit();

            base.OnInit(e);
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifApply.aspx|CertifCheckUnit.aspx|CertifAccept.aspx|CertifCheck.aspx|CertifConfirm.aspx|ApplyQuerySLR.aspx";
            }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["CertificateContinueOB"] == null ? "" : string.Format("XQ-{0}", (ViewState["CertificateContinueOB"] as CertificateContinueOB).CertificateContinueID); }
        }

        ///// <summary>
        ///// 初审单位ID
        ///// </summary>
        //public Int64 FirstCheckUnitID
        //{
        //    get
        //    {
        //        string[] checkUnitsID = new string[] { "242", "246", "247", "249" };
        //        RadioButtonList rlb;
        //        for (int i = 0; i < checkUnitsID.Length; i++)
        //        {
        //            rlb = PlaceHolder1.FindControl(string.Format("RadioButtonList{0}", checkUnitsID[i])) as RadioButtonList;
        //            if (rlb != null && rlb.SelectedIndex != -1)
        //            {
        //                return Convert.ToInt64(rlb.SelectedValue);
        //            }
        //        }
        //        return -1;
        //    }
        //    set
        //    {
        //        string[] checkUnitsID = new string[] { "242", "246", "247", "249" };
        //        RadioButtonList rlb;
        //        for (int i = 0; i < checkUnitsID.Length; i++)
        //        {
        //            rlb = PlaceHolder1.FindControl(string.Format("RadioButtonList{0}", checkUnitsID[i])) as RadioButtonList;
        //            if (rlb != null && rlb.Items.FindByValue(value.ToString()) != null)
        //            {
        //                rlb.Items.FindByValue(value.ToString()).Selected = true;
        //                break;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 初审单位
        ///// </summary>
        //public string FirstCheckUnitName
        //{
        //    get
        //    {
        //        string[] checkUnitsID = new string[] { "242", "246", "247", "249" };
        //        RadioButtonList rlb;
        //        for (int i = 0; i < checkUnitsID.Length; i++)
        //        {
        //            rlb = PlaceHolder1.FindControl(string.Format("RadioButtonList{0}", checkUnitsID[i])) as RadioButtonList;
        //            if (rlb != null && rlb.SelectedIndex != -1)
        //            {
        //                return rlb.SelectedItem.Text;
        //            }
        //        }
        //        return "";
        //    }
        //}

        /// <summary>
        /// 绑定初审单位用户
        /// </summary>
        private void setFistCheckUnit()
        {
            DataTable dt = CommonDAL.GetDataTable(@"SELECT OrganID,RelUserName,[LICENSE] FROM dbo.[User] 
                                                    WHERE (OrganID=242 or OrganID=246 or OrganID=247) 
                                                    union  select 0,'北京市住房和城乡建设委员会','100000'
                                                    order by OrganID,LICENSE");//机构

            RadComboBoxFirstCheckUnit.DataSource = dt;
            RadComboBoxFirstCheckUnit.DataTextField = "RelUserName";
            RadComboBoxFirstCheckUnit.DataValueField = "LICENSE";
            RadComboBoxFirstCheckUnit.DataBind();
            RadComboBoxFirstCheckUnit.Items.Insert(0, new RadComboBoxItem("请选择", ""));
            RadComboBoxFirstCheckUnit.Enabled = false;
        }

        protected CertificateOB myCertificateOB
        {
            get { return ViewState["CertificateOB"] == null ? null : (CertificateOB)ViewState["CertificateOB"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "enableBack", string.Format("window.history.location.replace('{0}');", Request.Url.OriginalString), true);

            if (!IsPostBack)
            {
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                if (string.IsNullOrEmpty(Request["o"]) == false || string.IsNullOrEmpty(Request["o2"]) == false)
                {
                    string CertificateContinueID = "";
                    CertificateContinueOB o2=null;

                    if (string.IsNullOrEmpty(Request["o2"]) == false)
                    {
                        CertificateContinueID = Utility.Cryptography.Decrypt(Request["o2"]);

                        if (CertificateContinueID != "0") o2 = CertificateContinueDAL.GetObject(Convert.ToInt64(CertificateContinueID));
                    }

                    string CertificateID = string.IsNullOrEmpty(Request["o"]) == true ? o2.CertificateID.ToString() : Utility.Cryptography.Decrypt(Request["o"]);

                    #region 证书信息
                    CertificateOB o = CertificateDAL.GetObject(Convert.ToInt64(CertificateID));
                    if (o != null)
                    {
                        ViewState["CertificateOB"] = o;

                        //隶属机构信息
                        QY_HYLSGXOB _QY_HYLSGXOB = QY_LSGXDAL.GetObjectByZZJGDM(o.UnitCode);

                        ViewState["PostTypeID"] = o.PostTypeID;
                        ViewState["PostID"] = o.PostID;
                        ViewState["CertificateID"] = o.CertificateID;
                        ViewState["WorkerCertificateCode"] = o.WorkerCertificateCode;
                        ViewState["FacePhoto"] = o.FacePhoto;
                        RadTextBoxUnitName.Text = o.UnitName;   //机构名称
                        RadTextBoxUnitCode.Text = o.UnitCode;   //机构代码
                        HiddenFieldOldUnitCode.Value = o.UnitCode;   //机构代码
                        LabelWorkerCertificateCode.Text = o.WorkerCertificateCode;  //证件号码
                        LabelWorkerName.Text = o.WorkerName;    //姓名   
                        LabelSex.Text = o.Sex;//性别
                        LabelBirthday.Text = o.Birthday.HasValue == false ? "" : o.Birthday.Value.ToString("yyyy-MM-dd");//生日
                        LabelCertificateCode.Text = o.CertificateCode;//证书编号
                        LabelConferDate.Text = o.ConferDate.HasValue == false ? "" : o.ConferDate.Value.ToString("yyyy-MM-dd");//发证时间
                        LabelValidDataTo.Text = o.ValidEndDate.HasValue == false ? "" : o.ValidEndDate.Value.ToString("yyyy-MM-dd");//有效期

                        //岗位工种
                        if (o.PostTypeID == 3 && string.IsNullOrEmpty(o.AddItemName) == false)//造价员
                        {
                            LabelPostName.Text = o.AddItemName;
                        }
                        else
                        {
                            LabelPostName.Text = o.PostName;
                        }
                        HiddenFieldPostTypeName.Value = o.PostTypeName;

                        //从业人信息
                        WorkerOB _WorkerOB = WorkerDAL.GetUserObject(o.WorkerCertificateCode);
                        if (_WorkerOB != null)
                        {
                            //LabelWorkStartDate.Text = _WorkerOB.WorkStartDate;//工作年限
                            //LabelSKILLLEVEL.Text = _WorkerOB.SKILLLEVEL; //技术职称或技术等级
                            RadTextBoxPhone.Text = _WorkerOB.Phone;//电话
                            if (RadTextBoxPhone.Text.Trim() != "")
                            {
                                UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话
                            }
                            else
                            {
                                UIHelp.SetReadOnly(RadTextBoxPhone, false);
                            }
                        }

                        //过滤初审单位
                        switch (o.PostTypeID.Value)
                        {
                            case 1://三类人
                                //三类人员证书续期
                                //1、凡是外地进京企业，不允许续期。
                                //2、凡是起重机械租赁企业，初审单位按注册地划分。
                                //3、其他按隶属关系自动选择隶属央企、集团或区县建委

                                p_PostTyppe1.Visible = true;//附件说明
                                spanSLR.Visible = true;//继续教育说明
                                div_FJ_jxjy.Visible = true;//继续教育附件
                                div_FJ_ShenFenZheng.Visible = true;//身份证
                                div_SafeTrain.Visible = true;//年度安全教育培训记录

                                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), true);
                                if (string.IsNullOrEmpty(UnitName))
                                {
                                    UIHelp.layerAlert(Page, "聘用单位无本地建筑施工企业资质证书，不允许续期。");
                                }

                                ////是否是外地进京企业
                                //bool ifwdjj = string.IsNullOrEmpty(UnitInfoDAL.GetUnitNameByCodeAndZZLBFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), EnumManager.UnitZZLB.qy_wdjj)) ? false : true;
                                ////是否是设备租赁企业
                                //bool ifsbzl = string.IsNullOrEmpty(UnitInfoDAL.GetUnitNameByCodeAndZZLBFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), EnumManager.UnitZZLB.qy_qzjx)) ? false : true;

                                if (_QY_HYLSGXOB != null && _QY_HYLSGXOB.USERID.HasValue == true)//有隶属关系
                                {
                                    UserOB u = UserDAL.GetObject(_QY_HYLSGXOB.USERID.Value);
                                    RadComboBoxItem li = RadComboBoxFirstCheckUnit.Items.FindItemByValue(u.License);
                                    if (li != null)
                                    {
                                        li.Selected = true;
                                    }
                                }

                                break;
                            case 2://特种作业 
                                RadComboBoxItem liBJ = RadComboBoxFirstCheckUnit.Items.FindItemByValue("100000");
                                if (liBJ != null)
                                {
                                    liBJ.Selected = true;
                                }
                                p_PostTyppe2.Visible = true;//附件说明
                                spanTZZY.Visible = true;//继续教育说明
                                div_FJ_TiJian.Visible = true;//近3个月内二级乙等以上医院体检合格材料1份
                                //div_SheBao.Visible = true;//如续期时同时提交变更单位申请，须上传社保权益记录，农业户口的上传与新单位签订的劳动合同和居民户口簿（首页、本人页、变更页）代替社保权益记录。
                                div_grjkcn.Visible = true;//个人健康承诺扫描件
                                div_SafeTrain.Visible = true;//年度安全教育培训记录

                                trjxjyway.Visible = false;//继续教育方式
                                break;
                        }
                    }

                    #endregion 证书信息
                  

                    #region 续期申请信息

                    

                    if (string.IsNullOrEmpty(Request["o2"]) == false && CertificateContinueID != "0")
                    {
   
                        if (o2 != null)//&& (o2.Status == "已申请" || (o2.Status == "已初审" && o2.GetResult == "退回修改")))
                        {


                            HiddenFieldApplyMan.Value = o2.ApplyMan;//申请人
                            ViewState["CertificateContinueOB"] = o2;
                            RadTextBoxUnitCode.Text = o2.UnitCode;   //机构代码

                            RadTextBoxUnitName.Text = o2.NewUnitName;   //机构名称
                            RadTextBoxUnitCode.Text = o2.NewUnitCode;   //机构代码
                            HiddenFieldOldUnitCode.Value = o2.UnitCode;   //原机构代码

                            RadTextBoxPhone.Text = o2.Phone;//电话
                            if (RadTextBoxPhone.Text.Trim() != "")
                            {
                                UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话
                            }
                            else
                            {
                                UIHelp.SetReadOnly(RadTextBoxPhone, false);
                            }

                            LabelApplyCode.Text = o2.ApplyCode;//续期申请批号      
                            LabelApplyDate.Text = o2.ApplyDate.Value.ToString("yyyy-MM-dd");

                            RadComboBoxItem li = RadComboBoxFirstCheckUnit.Items.FindItemByValue(o2.FirstCheckUnitCode);
                            if (li != null)
                            {
                                li.Selected = true;
                            }

                            if(o2.jxjyway.HasValue==true)
                            {
                                RadioButtonListjxjyway.Items.FindByValue(o2.jxjyway.ToString()).Selected = true;//继续教育形式
                            }

                            SetButtonEnable(o2.Status);

                            RefreshRadGridAnQuanPX();

                            //ShowSheBao(o.PostTypeID.ToString(), o2.ApplyDate.Value, o.WorkerCertificateCode, o2.UnitCode);//显示社保链接
                            BindCheckHistory(o2.CertificateContinueID.Value);
                            BindFile(ApplyID);

                            DataTable zf = CommonDAL.GetDataTable(
                                string.Format(@"SELECT top 10 [Jgmc],[WhDeputy],[Dsr_Xm],[Dsr_gzdw],[Dsr_gr_Zjhm],[ZFJC_Type],[Blxwdm],[Wfss],[cfcs] ,[Xzcfjd]   
                                                FROM [dbo].[LESP_Peopleinfo] 
                                                where [Dsr_gr_Zjhm] like '{0}%' order by Jdrq desc", LabelWorkerCertificateCode.Text));

                            if (zf !=null && zf.Rows.Count >0)
                            {
                                RadGridZhiFa.DataSource = zf;
                                RadGridZhiFa.DataBind();
                                divZhiFa.Visible = true;
                            }

                            if (o2.Status == EnumManager.CertificateContinueStatus.WaitUnitCheck
                                && TableJWCheck.Visible == true)
                            {
                                UIHelp.layerAlert(Page, "此申请尚未经过单位确认，除非原单位已注销等特殊情况，建委审批人员可酌情跳过原单位确认，直接审批。");
                            }

                            if (PersonType != 2)//禁止修改   
                            {
                                UIHelp.SetReadOnly(RadTextBoxUnitCode, true);
                                UIHelp.SetReadOnly(RadTextBoxPhone, true);
                            }
                            else
                            {
                                if (o2.ZACheckResult == 0)
                                {
                                    UIHelp.layerAlert(Page, string.Format("您的证书没有通过【全国工程质量安全监管信息平台】数据校验，属于违规持证，无法申请续期。请先办理相关证书变更、注销或转出后再发起申请。校验结果说明：{0}</br><p><b>特别提示：按要求整改后需要重新保存一下申请单，系统才会重新发起数据核验。</b></p>", o2.ZACheckRemark));
                                }
                            }
                        }
                    }
                    else//新增
                    {
                        SetButtonEnable("");
                    }

                    #endregion 续期申请信息

                    ShowFinishGYPX();//公益培训记录

                    if(RadComboBoxFirstCheckUnit.SelectedValue=="")
                    {
                        LabelNoFirstCheckUnitMessage.Visible = true;
                    }

                    //ImgCode.Src = UIHelp.ShowFaceImage(o.FacePhoto, o.WorkerCertificateCode); //绑定照片;
                    //ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(o.FacePhoto, o.WorkerCertificateCode)); //绑定照片;
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(o.FacePhoto, o.WorkerCertificateCode);
                    if (string.IsNullOrEmpty(o.FacePhoto) == true
                        && PersonType == 2
                        && divCtl.Visible == true
                        )
                    {
                        TableUploadPhoto.Visible = true;
                    }
                    else
                    {
                        TableUploadPhoto.Visible = false;
                    }

                    if (PersonType == 2)//个人绑定时，组织机构代码不为空则可修改
                    {
                        if (RadTextBoxUnitCode.Text.Trim() != "")
                        {
                            RadTextBoxUnitCode.Enabled = false;//禁止修改
                            RadTextBoxUnitName.Enabled = false;//禁止修改
                        }
                    }


                }
            }
            else
            {
                if (Request["__EVENTTARGET"] == "refreshFile")//上传或删除附件刷新列表
                {
                    BindFile(ApplyID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", btnSave.ClientID), true);
                }
            }

            if (LabelPostName.Text == "企业主要负责人"
                || LabelPostName.Text == "项目负责人"
                || LabelPostName.Text == "土建类专职安全生产管理人员"
                || LabelPostName.Text == "机械类专职安全生产管理人员"
                || LabelPostName.Text == "综合类专职安全生产管理人员")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "posttypeid", "var posttypeid=1;", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "posttypeid", "var posttypeid=2;", true);
            }
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_填报中.Attributes["class"] = step_填报中.Attributes["class"].Replace(" green", "");
            step_待单位确认.Attributes["class"] = step_待单位确认.Attributes["class"].Replace(" green", "");
            step_已申请.Attributes["class"] = step_已申请.Attributes["class"].Replace(" green", "");
            step_已初审.Attributes["class"] = step_已初审.Attributes["class"].Replace(" green", "");
            step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");
            step_已审查.Attributes["class"] = step_已审查.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
            step_已办结.Attributes["class"] = step_已办结.Attributes["class"].Replace(" green", "");

            CertificateContinueOB _CertificateContinueOB = null;
            switch (ApplyStatus)
            {
                case "填报中":
                case "退回修改":
                    step_填报中.Attributes["class"] += " green";
                    break;
                case "待单位确认":
                    step_待单位确认.Attributes["class"] += " green";
                    break;
                case "已申请":
                    step_已申请.Attributes["class"] += " green";
                    break;
                case "已初审":
                    _CertificateContinueOB = ViewState["CertificateContinueOB"] as CertificateContinueOB;
                    if (string.IsNullOrEmpty(_CertificateContinueOB.ReportMan) == false)
                        step_已上报.Attributes["class"] += " green";
                    else
                        step_已初审.Attributes["class"] += " green";
                    break;

                case "已审核":
                    step_已审查.Attributes["class"] += " green";
                    break;
                case "已决定":
                    _CertificateContinueOB = ViewState["CertificateContinueOB"] as CertificateContinueOB;
                    if (myCertificateOB != null && myCertificateOB.ZZUrlUpTime > _CertificateContinueOB.ConfirmDate)
                    {
                        step_已办结.Attributes["class"] += " green";
                    }
                    else
                    {
                        step_已决定.Attributes["class"] += " green";
                    }
                    break;   
                default:
                    step_填报中.Attributes["class"] += " green";
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

            trFuJanTitel.Visible = false;
            trFuJan.Visible = false;
            switch (ApplyStatus)
            {
                case "":
                    btnSave.Enabled = true;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//提交单位审核 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "提交单位审核";
                    if (myCertificateOB.PostTypeID == 2) trSelectUnit.Style.Add("display", "normal");
                    break;
                case EnumManager.CertificateContinueStatus.NewSave:
                    btnSave.Enabled = true;//保 存
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位审核 
                    ButtonDelete.Enabled = true;//删除
                    ButtonExit.Text = "提交单位审核";
                    //个人登录后,可上传附件
                    if (IfExistRoleID("0") == true
                         || ValidResourceIDLimit(RoleIDs, "a_ContinueApply") == true
                         || ValidResourceIDLimit(RoleIDs, "a_ContinueApply_TZZY") == true
                        )
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;

                        trAnQuanPXTitle.Visible = true;
                        trAnQuanPX.Visible = true;
                    }
                    if (myCertificateOB.PostTypeID == 2) trSelectUnit.Style.Add("display", "normal");
                    break;
                case EnumManager.CertificateContinueStatus.WaitUnitCheck:
                    btnSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = false;//删除
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = true;//取消申报 
                    ButtonExit.Text = "取消申报";
                    if (IfExistRoleID("2") == true)
                    {
                        TableUnitCheck.Visible = true;
                    }

                    break;
                case EnumManager.CertificateContinueStatus.SendBack:
                    btnSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = true;//删除
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位确认  
                    ButtonExit.Text = "提交单位审核";
                    //个人登录后,可上传附件
                    if (IfExistRoleID("0") == true
                         || ValidResourceIDLimit(RoleIDs, "a_ContinueApply") == true
                         || ValidResourceIDLimit(RoleIDs, "a_ContinueApply_TZZY") == true
                        )
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;

                        trAnQuanPXTitle.Visible = true;
                        trAnQuanPX.Visible = true;
                    }
                    if (myCertificateOB.PostTypeID == 2) trSelectUnit.Style.Add("display", "normal");
                    break;
                case EnumManager.CertificateContinueStatus.Applyed://已申请
                    if (ValidPageViewLimit(RoleIDs, "RenewCertifates/CertifAccept.aspx") == true)//受理权限
                    {
                        TableFirstCheck.Visible = true;
                    }
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = true;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;

                case EnumManager.CertificateEnterStatus.Accepted://已受理
                    if (ValidPageViewLimit(RoleIDs, "RenewCertifates/CertifCheck.aspx") == true)//审核权限
                    {
                        TableJWCheck.Visible = true;
                    }

                    break;
                case EnumManager.CertificateEnterStatus.Checked://已审核

                default:
                    btnSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;
            }


            //个人登录后
            if (IfExistRoleID("0") == true
                || ValidResourceIDLimit(RoleIDs, "a_ContinueApply") == true
                || ValidResourceIDLimit(RoleIDs, "a_ContinueApply_TZZY") == true
                )
            {
                divCtl.Visible = true;
                //divRtn.Visible = false;
            }
            else
            {
                divCtl.Visible = false;
                //divRtn.Visible = true;
            }


            if (ViewState["CertificateContinueOB"] != null)
            {
                trAnQuanPXTitle.Visible = true;
                trAnQuanPX.Visible = true;

                if (ApplyStatus == EnumManager.CertificateContinueStatus.NewSave || ApplyStatus == EnumManager.CertificateContinueStatus.SendBack)
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
                trAnQuanPXTitle.Visible = false;
                trAnQuanPX.Visible = false;
            }



            btnSave.CssClass = btnSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExport.CssClass = ButtonExport.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExit.CssClass = ButtonExit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";

        }

        //保存申请表
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 续期限制条件检查

                //1、证书处于锁定中（内部锁），不允许续期。
                //2、证书正在办理变更，未办结不能同时申请续期。
                //3、三类人员续期条件：证书所在单位无建筑施工企业资质证书或未办理来京施工备案，不允许续期。
                //4、项目负责人B证：无建本地造师注册证书（或中央在京备案、外地进京本案建造师证书）或注册证书单位与所在单位不一致，不允许续期。
                //5、三类人续期凡是外地进京企业，初审单位只能选择： 区县建委——外师培训中心。
                //6、三类人续期凡是机租赁企业，初审单位只能选择： 区县建委——机械协会。
                //7、既有起重机械租赁企业资质又有施工企业资质的初审单位任意选择。

                #region 上传个人照片

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)
                {
                    if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                    {
                        UIHelp.layerAlert(Page, "照片格式不正确！只能是有jpg格式图片");
                        return;
                    }
                    if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                    {
                        UIHelp.layerAlert(Page, "照片大小不能超过50k！");
                        return;
                    }
                }
                if (TableUploadPhoto.Visible == true && RadUploadFacePhoto.UploadedFiles.Count == 0 && ImgCode.Src.IndexOf("tup.gif") > 0)//照片
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }

                //个人照片存放路径(按证件号码后3位)
                UIHelp.CheckCreateDirectory(Page, "~/UpLoad/WorkerPhoto/");

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = LabelWorkerCertificateCode.Text;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, LabelWorkerCertificateCode.Text + ".jpg"), true);
                        break;
                    }
                    ImgCode.Src = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode("", LabelWorkerCertificateCode.Text);//绑定照片;
                }

                //ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), GetFacePhotoPath(LabelWorkerCertificateCode.Text)); //绑定照片;
                #endregion

                string lockReason = "";
                bool lockStatus = CertificateLockDAL.GetCertificateLockStatus((long)ViewState["CertificateID"], ref lockReason);
                if (lockStatus == true)//已加锁，内部锁
                {
                    UIHelp.layerAlert(Page, string.Format("证书处于锁定中，不允许变更。锁定原因：{0}", lockReason));
                    return;
                }


                if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxUnitCode.Text) == false)
                {
                    UIHelp.layerAlert(Page, "“组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                    return;
                }


                //if (ViewState["PostTypeID"].ToString() == "2" && UIHelp.UnitCodeCheck(this.Page, RadTextBoxNewUnitCode.Text) == false)
                //{
                //    UIHelp.layerAlert(Page, "“现单位组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                //    return;
                //}


                //在办变更申请数量
                int countChangeApply = CertificateChangeDAL.SelectCount(string.Format(" and CertificateID={0} and ([STATUS] = '{1}' or [STATUS] = '{2}' or [STATUS] = '{3}' or [STATUS] = '{4}' or [STATUS] = '{5}')"
                    , ViewState["CertificateID"].ToString()
                    , EnumManager.CertificateChangeStatus.NewSave
                    , EnumManager.CertificateChangeStatus.WaitUnitCheck
                    , EnumManager.CertificateChangeStatus.Applyed
                    , EnumManager.CertificateChangeStatus.SendBack
                    , EnumManager.CertificateChangeStatus.Checked
                    ));
                if (countChangeApply > 0)
                {
                    UIHelp.layerAlert(Page, "证书正在办理变更，未办结不能同时申请续期。");
                    return;
                }

                if (ViewState["PostID"].ToString() == "6" || ViewState["PostID"].ToString() == "1123")
                {
                    if (CertificateMergeDAL.IfExistsApplying(Convert.ToInt64(ViewState["CertificateID"])) == true)
                    {
                        UIHelp.layerAlert(Page, "证书正在办理C1、C2合并，不能同时申请续期。");
                        return;
                    }
                }

                if (RadComboBoxFirstCheckUnit.SelectedItem.Value == "")
                {
                    UIHelp.layerAlert(Page, "请选择一个初审单位！");
                    return;
                }

                string UnitName = "";//企业名称

                if (myCertificateOB.ZZUrlUpTime.HasValue == false || myCertificateOB.ZZUrlUpTime < myCertificateOB.CheckDate)
                {
                    UIHelp.layerAlert(Page, "上一次业务办理结果尚未上传到国家（没有成功生成电子证书），无法申请新业务，请到电子证书下载页面查看原因，按要求整改。");
                    return;
                }

                if (UnitDAL.CheckGongShang(RadTextBoxUnitCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", RadTextBoxUnitName.Text));
                    return;
                }

                //三类人员续期条件过滤(//建筑施工企业、中央在京、设计施工一体化、起重机械)
                if (ViewState["PostTypeID"].ToString() == "1")
                {


                    if (RadioButtonListjxjyway.SelectedValue == "")
                    {
                        UIHelp.layerAlert(Page, "请选择完成初审单位组织的16学时证书延续继续教育学习形式。");
                        return;
                    }


                    bool BLimit = ValidResourceIDLimit(RoleIDs, "ThreeClassContinueLimit");
                    if (BLimit == false)//需要验证
                    {
                        UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), true);
                        //int countZZZS = CommonDAL.SelectRowCount("dbo.QY_BWDZZZS", string.Format(" and (ZZJGDM='{0}' or ZZJGDM like '________{0}_')", this.RadTextBoxUnitCode.Text.Trim()));
                        if (string.IsNullOrEmpty(UnitName))
                        {
                            UIHelp.layerAlert(Page, "聘用单位无建筑施工企业资质证书，不允许续期。");
                            return;
                        }
                        else if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Replace("（", "(").Replace("）", ")"))
                        {
                            UIHelp.layerAlert(Page, string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您的单位名称不符，不允许续期。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                            return;
                        }

                        if (ViewState["PostID"].ToString() == "147")//企业主要负责人
                        {
                            if (CertificateDAL.IFExistFRByUnitCode(RadTextBoxUnitCode.Text.Trim(), LabelWorkerName.Text) == false)
                            {
                                //非企业法人A本，受年龄限制
                                if (Utility.Check.CheckContinueBirthdayLimit(147, LabelWorkerCertificateCode.Text, myCertificateOB.Birthday.Value, myCertificateOB.Sex, myCertificateOB.ValidEndDate.Value) == true)
                                {
                                    UIHelp.layerAlert(Page, "您的法定退休日期早于或等于该证书有效期截止日期，不再受理您的证书延续申请。");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Utility.Check.CheckContinueBirthdayLimit(Convert.ToInt32(ViewState["PostID"]), LabelWorkerCertificateCode.Text, myCertificateOB.Birthday.Value, myCertificateOB.Sex, myCertificateOB.ValidEndDate.Value) == true)
                            {
                                UIHelp.layerAlert(Page, "您的法定退休日期早于或等于该证书有效期截止日期，不再受理您的证书延续申请。");
                                return;
                            }
                        }

                        //项目负责人B证(外地进京不过滤)
                        if (ViewState["PostID"].ToString() == "148")
                        {
                            //15位和18位身份证号转换
                            string IDCard15 = "";
                            string IDCard18 = "";
                            if (this.LabelWorkerCertificateCode.Text.Length == 15 && Utility.Check.isChinaIDCard(LabelWorkerCertificateCode.Text) == true)
                            {
                                IDCard15 = this.LabelWorkerCertificateCode.Text;
                                IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                            }
                            else if (this.LabelWorkerCertificateCode.Text.Length == 18 && Utility.Check.isChinaIDCard(LabelWorkerCertificateCode.Text) == true)
                            {
                                IDCard15 = this.LabelWorkerCertificateCode.Text.Remove(17, 1).Remove(6, 2);
                                IDCard18 = this.LabelWorkerCertificateCode.Text;
                            }
                            else
                            {
                                IDCard15 = this.LabelWorkerCertificateCode.Text;
                                IDCard18 = this.LabelWorkerCertificateCode.Text;
                            }

                            int countZCJZS = CommonDAL.SelectRowCount("dbo.VIEW_RY_ZCJZS", string.Format(" and (ZJHM='{0}' or ZJHM='{1}') and (ZZJGDM='{2}' or ZZJGDM like '________{2}_')", IDCard15, IDCard18, this.RadTextBoxUnitCode.Text.Trim()));
                            if (countZCJZS == 0)
                            {
                                UIHelp.layerAlert(Page, "无建造师注册证书或注册证书单位与所在单位不一致，不允许续期。");
                                return;
                            }
                        }
                    }

                    //判断A（其中一本）、B、C1、C2、C3证书是否在一个单位
                    string sql = @"select [UNITCODE],count(*) C_count
                                ,A_count=(select count(*) from [CERTIFICATE] where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] =147   and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()) 
                                ,A_CertCode =(select top 1 [CERTIFICATECODE] from [CERTIFICATE] where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] =147 and [UNITCODE]='{1}'  and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()) 
                              FROM [dbo].[CERTIFICATE]
                              where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] in(6,148,1123,1125)
                              and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()
                              group by [UNITCODE]";
                    DataTable dtCheckBCUnit = CommonDAL.GetDataTable(string.Format(sql, LabelWorkerCertificateCode.Text, RadTextBoxUnitCode.Text.Trim()));

                    if (dtCheckBCUnit.Rows.Count > 1
                        || (dtCheckBCUnit.Rows.Count == 1 && Convert.ToInt32(dtCheckBCUnit.Rows[0]["A_count"]) > 0 && dtCheckBCUnit.Rows[0]["A_CertCode"] == DBNull.Value)
                        )
                    {
                        sql = @"select CertificateCode, '持证所属单位：' +UNITNAME  as UNITNAME
                                from [dbo].[CERTIFICATE]
                                where [WorkerCertificateCode]='{0}' 
                                    and posttypeid=1 and ValidEndDate >=dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销'
                                    order by CertificateCode";

                        DataTable tb = CommonDAL.GetDataTable(string.Format(sql, LabelWorkerCertificateCode.Text));
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            foreach (DataRow r in tb.Rows)
                            {
                                sb.AppendFormat("<br />{0}，{1}。", r["CertificateCode"], r["UnitName"]);
                            }
                            sb.Insert(0, "校验未通过。校验规则：持证人同时持有有效的A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业。请按规则变更到统一单位后在提交续期申请。");

                            UIHelp.layerAlert(Page, string.Format("{0}", sb), 5, 0);
                            return;
                        }
                    }

                    if (div_GongYiPX.InnerText != "" && div_GongYiPX.InnerText != "已达标")
                    {
                        UIHelp.layerAlert(Page, div_GongYiPX.InnerText, 5, 0);
                        return;
                    }
                }
                else//特种作业
                {

                    if (Utility.Check.CheckContinueBirthdayLimit(2, LabelWorkerCertificateCode.Text, myCertificateOB.Birthday.Value, myCertificateOB.Sex, myCertificateOB.ValidEndDate.Value) == true)
                    {
                        UIHelp.layerAlert(Page, "您的法定退休日期早于或等于该证书有效期截止日期，不再受理您的证书延续申请。");
                        return;
                    }

                    UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), false);
                    //int countZZZS = CommonDAL.SelectRowCount("dbo.QY_BWDZZZS", string.Format(" and (ZZJGDM='{0}' or ZZJGDM like '________{0}_')", this.RadTextBoxUnitCode.Text.Trim()));
                    if (string.IsNullOrEmpty(UnitName))
                    {
                        UIHelp.layerAlert(Page, "聘用单位无北京建筑施工企业资质或来京施工资质备案，不允许续期。");
                        return;
                    }
                    else if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitName.Text.Replace("（", "(").Replace("）", ")"))
                    {
                        UIHelp.layerAlert(Page, string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您的单位名称不符，不允许续期。”", RadTextBoxUnitCode.Text.Trim(), UnitName));
                        return;
                    }


                    if (div_GongYiPX.InnerText != "" && div_GongYiPX.InnerText != "已达标")
                    {
                        UIHelp.layerAlert(Page, div_GongYiPX.InnerText, 5, 0);
                        return;
                    }
                }

                ////企业信息
                //UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxUnitCode.Text.Trim());//企业信息
                //if (_UnitMDL !=null &&(_UnitMDL.ResultGSXX == 0 || _UnitMDL.ResultGSXX == 1))
                //{
                //    UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", _UnitMDL.ENT_Name));
                //    return;
                //}



                #endregion 续期限制条件检查

                CertificateContinueOB ob;
                if (ViewState["CertificateContinueOB"] == null)//new
                {
                    int continuedCount = CertificateContinueDAL.SelectCount(string.Format(" and CertificateID={0} and [STATUS]='{1}'", ViewState["CertificateID"].ToString(), EnumManager.CertificateContinueStatus.NewSave));
                    if (continuedCount > 0)
                    {
                        UIHelp.layerAlert(Page, "您已经提交过续期申请，无法重复申请续期！");
                        return;
                    }
                    ob = new CertificateContinueOB();
                    ob.CertificateID = (long)ViewState["CertificateID"];
                }
                else//update
                {
                    ob = (CertificateContinueOB)ViewState["CertificateContinueOB"];
                }

                ob.ApplyDate = DateTime.Now;//申请时间
                LabelApplyDate.Text = ob.ApplyDate.Value.ToString("yyyy-MM-dd");
                ob.ApplyMan = PersonName;//申请人   
                HiddenFieldApplyMan.Value = PersonName;
                ob.Phone = RadTextBoxPhone.Text.Trim();//电话
                ob.UnitCode = HiddenFieldOldUnitCode.Value;//原组织机构代码
                ob.NewUnitName = RadTextBoxUnitName.Text;
                ob.NewUnitCode = RadTextBoxUnitCode.Text.Trim();//组织机构代码
                ob.FirstCheckUnitName = RadComboBoxFirstCheckUnit.SelectedItem.Text;
                ob.FirstCheckUnitCode = RadComboBoxFirstCheckUnit.SelectedItem.Value;

                if (RadioButtonListjxjyway.SelectedValue != "")
                {
                    ob.jxjyway = Convert.ToInt32(RadioButtonListjxjyway.SelectedValue);
                }

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    if (ViewState["CertificateContinueOB"] == null)//new
                    {
                        #region 检查是否存在企业信息
                        //UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxUnitCode.Text.Trim());//企业信息
                        //if (_UnitMDL == null)
                        //{
                        //    _UnitMDL = new UnitMDL();

                        //    //企业资质
                        //    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(ob.UnitCode);

                        //    if (_jcsjk_QY_ZHXXMDL != null)//有资质
                        //    {
                        //        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        //        _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                        //        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        //        _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                        //        _UnitMDL.ENT_OrganizationsCode = ob.UnitCode;//组织机构代码
                        //        _UnitMDL.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                        //        _UnitMDL.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;//区县
                        //        _UnitMDL.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                        //        _UnitMDL.ENT_Corporate = _jcsjk_QY_ZHXXMDL.FDDBR;//法定代表人
                        //        _UnitMDL.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                        //        _UnitMDL.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                        //        if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                        //        {
                        //            _UnitMDL.ENT_Sort = "";
                        //            _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        //        }
                        //        else
                        //        {
                        //            //企业资质等级
                        //            if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                        //            {
                        //                if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                        //                {
                        //                    _UnitMDL.ENT_Grade = "特级";
                        //                }
                        //                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                        //                {
                        //                    _UnitMDL.ENT_Grade = "一级";
                        //                }
                        //                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                        //                {
                        //                    _UnitMDL.ENT_Grade = "二级";
                        //                }
                        //                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                        //                {
                        //                    _UnitMDL.ENT_Grade = "三级";
                        //                }
                        //                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                        //                {
                        //                    _UnitMDL.ENT_Grade = "不分等级";
                        //                }
                        //                else
                        //                {
                        //                    _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                        //            }
                        //            _UnitMDL.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                        //            string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                        //            if (_UnitMDL.ENT_Grade != null)
                        //            {
                        //                foreach (var item in ZZ)
                        //                {
                        //                    if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(_UnitMDL.ENT_Grade) == true)
                        //                    {
                        //                        _UnitMDL.ENT_Sort = item;   //资质序列
                        //                        break;
                        //                    }

                        //                }
                        //            }

                        //        }
                        //        _UnitMDL.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                        //        _UnitMDL.CreditCode = "";//社会统一信用代码
                        //        _UnitMDL.Valid = 1;//是否有效
                        //        _UnitMDL.ResultGSXX = 0;
                        //    }
                        //    else//无资质设置为新设立企业
                        //    {
                        //        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        //        _UnitMDL.BeginTime = DateTime.Now;//建立时间
                        //        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        //        _UnitMDL.ENT_Name = RadTextBoxUnitName.Text.Trim();//企业名称
                        //        _UnitMDL.ENT_OrganizationsCode = ob.UnitCode;//组织机构代码
                        //        _UnitMDL.ENT_Economic_Nature = "";//企业类型
                        //        _UnitMDL.ENT_City = "";//区县
                        //        _UnitMDL.END_Addess = "";//注册地址
                        //        _UnitMDL.ENT_Corporate = "";//法定代表人
                        //        //_UnitMDL.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅无企业通讯地址企业自行维护
                        //        _UnitMDL.ENT_Type = "";  //企业类型
                        //        _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                        //        _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                        //        _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号
                        //        _UnitMDL.CreditCode = "";//社会统一信用代码
                        //        _UnitMDL.Valid = 1;//是否有效
                        //        _UnitMDL.Memo = "新设立企业";
                        //        _UnitMDL.CJSJ = DateTime.Now;
                        //        _UnitMDL.ResultGSXX = 0;
                        //    }

                        //    UnitDAL.Insert(tran, _UnitMDL);
                        //}

                        #endregion 检查是否存在企业信息

                        //申请信息
                        ob.ApplyCode = UIHelp.GetNextBatchNumber(tran, "XQSQ");//续期申请批号 
                        LabelApplyCode.Text = ob.ApplyCode;
                        ob.Status = EnumManager.CertificateContinueStatus.NewSave;//续期状态
                        ob.CreatePersonID = PersonID;//创建人ID
                        ob.CreateTime = DateTime.Now;//创建时间


                        CertificateContinueDAL.Insert(tran, ob);

                    }
                    else//update
                    {
                        ob.ModifyPersonID = PersonID;
                        ob.ModifyTime = DateTime.Now;
                        ob.Status = EnumManager.CertificateContinueStatus.NewSave;//续期状态

                        ob.NewUnitCheckTime = null;
                        ob.NewUnitAdvise = null;//现单位意见

                        ob.GetDate = null;   //变更受理时间
                        ob.GetResult = null;     //变更受理结论
                        ob.GetMan = null;    //变更受理人
                        ob.GetCode = null;//变更受理编批号
                        ob.CheckCode = null;
                        ob.CheckMan = null;
                        ob.CheckDate = null;
                        ob.CheckResult = null;
                        ob.ReportCode = null;
                        ob.ReportDate = null;
                        ob.ReportMan = null;

                        CertificateContinueDAL.Update(tran, ob);

                    }

                    tran.Commit();//提交事物    
                    ViewState["CertificateContinueOB"] = ob;

                    SetButtonEnable(ob.Status);
                }
                catch (Exception ex)
                {
                    tran.Rollback();//事务回滚
                    UIHelp.WriteErrorLog(Page, "证书续期申请失败！", ex);
                    return;
                }

                if (RadTextBoxUnitCode.Enabled == true)//补充了组织机构代码
                {
                    CertificateOB _CertificateOB = CertificateDAL.GetObject(Convert.ToInt64(ViewState["CertificateID"]));
                    if (string.IsNullOrEmpty(_CertificateOB.UnitCode) == true)
                    {
                        _CertificateOB.UnitCode = RadTextBoxUnitCode.Text.Trim();
                        CertificateDAL.Update(_CertificateOB);
                    }
                }

                //ShowSheBao(ViewState["PostTypeID"].ToString(), ob.ApplyDate.Value, ViewState["WorkerCertificateCode"].ToString(), ob.UnitCode);//显示社保链接
                BindFile(ApplyID);

                RefreshRadGridAnQuanPX();

                UIHelp.WriteOperateLog(PersonName, UserID, "申请证书续期", string.Format("申请批次号：“{0}”；证书编号：{1}。"
                , ob.ApplyCode, LabelCertificateCode.Text));

                UIHelp.layerAlert(Page, "证书续期申请成功！现在您可以继续打印申请表。", 6, 3000, "isfresh=true;parent.SetCwinHeight();");
                //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex2)
            {
                UIHelp.WriteErrorLog(Page, "证书续期申请失败！", ex2);
            }
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateContinueOB o = ViewState["CertificateContinueOB"] as CertificateContinueOB;
                CertificateContinueDAL.Delete(o);
                if (ViewState["PostTypeID"].ToString() == "2")
                {
                    JxjyBaseDAL.Delete(Convert.ToInt32(ViewState["PostTypeID"]), o.CertificateContinueID.ToString());
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "删除从业人员证书续期申请", string.Format("证书编号：{0}。", LabelCertificateCode.Text));
                ViewState["CertificateContinueOB"] = null;
                SetButtonEnable("");
                BindFile(ApplyID);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除证书续期申请失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, "删除成功！", 6, 3000, "isfresh=true;");
        }

        //提交单位审核 & 取消申请
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            CertificateContinueOB o = ViewState["CertificateContinueOB"] as CertificateContinueOB;
            o.NewUnitCheckTime = null;
            o.NewUnitAdvise = null;//现单位意见

            o.GetDate = null;   //变更受理时间
            o.GetResult = null;     //变更受理结论
            o.GetMan = null;    //变更受理人
            o.GetCode = null;//变更受理编批号
            o.CheckCode = null;
            o.CheckDate = null;
            o.CheckMan = null;
            o.CheckResult = null;
            o.ReportCode = null;
            o.ReportDate = null;
            o.ReportMan = null;

            if (ButtonExit.Text == "提交单位审核")
            {
                //公益视频课时总数
                if (div_GongYiPX.InnerText != "" && div_GongYiPX.InnerText != "已达标")
                {
                    UIHelp.layerAlert(Page, div_GongYiPX.InnerText, 5, 0);
                    return;
                }
               
                //录入的选修课课时总数
                if (divAnQuanPXFinishCase.InnerText != "" && divAnQuanPXFinishCase.InnerText != "已达标")
                {
                    UIHelp.layerAlert(Page, divAnQuanPXFinishCase.InnerText, 5, 0);
                    return;
                }

                //选修课扫描件
                if (ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.年度安全教育培训记录) == false)
                {
                    UIHelp.layerAlert(Page, "缺少年度安全生产教育培训记扫描件，请在线填写年度安全生产教育培训记，单位盖章后扫描上传。");
                    return;
                }

                #region 必须上传附件集合

                System.Collections.Hashtable fj = null;//必须上传附件集合

                if (ViewState["PostTypeID"].ToString() == "1")//三类人
                {
                    fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.续期申请表扫描件,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0},
                        {EnumManager.FileDataTypeName.继续教育证明扫描件,0},
                        {EnumManager.FileDataTypeName.年度安全教育培训记录,0}                          
                    };
                }
                else//特种作业
                {
                    fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.续期申请表扫描件,0},
                         {EnumManager.FileDataTypeName.体检合格证明,0},
                          {EnumManager.FileDataTypeName.个人健康承诺,0},
                          {EnumManager.FileDataTypeName.年度安全教育培训记录,0}                          
                    };
                }

                //已上传附件集合
                DataTable dt = ApplyDAL.GetApplyFile(ApplyID);

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

                //if (ViewState["PostTypeID"].ToString() == "2"//特种作业
                //    && RadTextBoxUnitCode.Text != "" && RadTextBoxNewUnitCode.Text != RadTextBoxUnitCode.Text)//变换了单位
                //{
                //    //检查社保相关扫描件上传文件是否存在
                //    bool SheBaoCheckFile = ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.社保扫描件);

                //    if (SheBaoCheckFile == false //没上传社保证明
                //   && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
                //   )
                //    {
                //        sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.社保扫描件));
                //    }
                //}

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！", sb), 5, 0);
                    return;
                }

                #endregion 必须上传附件集合

                if (o.ZACheckResult.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "尚未进行数据校验，请等待系统校验数据后再提交申请。");
                    return;
                }
                if (o.ZACheckResult == 0)
                {
                    UIHelp.layerAlert(Page, string.Format("您的证书没有通过【全国工程质量安全监管信息平台】数据校验，属于违规持证，无法申请续期。请先办理相关证书变更、注销或转出后再发起申请。校验结果说明：{0}</br><p><b>特别提示：按要求整改后需要重新保存一下申请单，系统才会重新发起数据核验。</b></p>", o.ZACheckRemark));
                    return;
                }

                try
                {
                    o.ApplyDate = DateTime.Now;
                    o.Status = EnumManager.CertificateContinueStatus.WaitUnitCheck;
                    CertificateContinueDAL.Update(o);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "证书续期申请提交单位审核失败！", ex);
                    return;
                }

                UIHelp.WriteOperateLog(PersonName, UserID, "证书续期申请提交单位审核", string.Format("证书编号：{0}。", LabelCertificateCode.Text));
                //UIHelp.layerAlert(Page, "提交现单位审核成功！", 6, 3000, "var isfresh=true;");
                UIHelp.layerAlert(Page, "提交单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"var isfresh=true;window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("continue"), Utility.Cryptography.Encrypt(o.CertificateContinueID.ToString())));
            }
            else//取消申请
            {
                try
                {
                    o.ModifyTime = DateTime.Now;
                    o.ModifyPersonID = PersonID;
                    o.Status = EnumManager.CertificateContinueStatus.NewSave;
                    CertificateContinueDAL.Update(o);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "取消证书续期申请失败！", ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "取消从业人员证书续期申请", string.Format("证书编号：{0}。", LabelCertificateCode.Text));
                UIHelp.layerAlert(Page, "取消成功！", "var isfresh=true;");
            }

            ViewState["CertificateContinueOB"] = o;
            SetButtonEnable(o.Status);
            BindCheckHistory(o.CertificateContinueID.Value);
            BindFile(ApplyID);
        }

        //绑定审核历史记录
        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ApplyID)
        {
            DataTable dt=null;
            if (PersonType == 2 || PersonType == 3)
            {
                dt = CertificateContinueDAL.GetCheckHistoryListForGRQY(ApplyID);
            }
            else
            {
                dt = CertificateContinueDAL.GetCheckHistoryListForAdmin(ApplyID);
            }
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();

        }

        //单位审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            CertificateContinueOB o = ViewState["CertificateContinueOB"] as CertificateContinueOB;

            #region  校验

            
            if (RadioButtonListOldUnitCheckResult.SelectedValue == "同意")
            {
                CertificateOB _CertificateOB = (CertificateOB)ViewState["CertificateOB"];

                //检查续期时间段，未在允许时间段内，不允许提交审核
                if (_CertificateOB.ValidEndDate.Value.AddDays(1) < DateTime.Now)
                {
                    UIHelp.layerAlert(Page, "证书已经过期，审批端已经不再接收续期申请！", 5, 0);
                    return;
                }

                //判断A（其中一本）、B、C1、C2、C3证书是否在一个单位
                if (ViewState["PostTypeID"].ToString() == "1")
                {
                    string sql = @"select [UNITCODE],count(*) C_count
                                ,A_count=(select count(*) from [CERTIFICATE] where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] =147   and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()) 
                                ,A_CertCode =(select top 1 [CERTIFICATECODE] from [CERTIFICATE] where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] =147 and [UNITCODE]='{1}'  and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()) 
                              FROM [dbo].[CERTIFICATE]
                              where [WORKERCERTIFICATECODE] = '{0}' and [POSTID] in(6,148,1123,1125)
                              and [STATUS]<> '离京变更' and [STATUS] <>'注销' and [VALIDENDDATE] >getdate()
                              group by [UNITCODE]";
                    DataTable dtCheckBCUnit = CommonDAL.GetDataTable(string.Format(sql, LabelWorkerCertificateCode.Text, RadTextBoxUnitCode.Text.Trim()));


                    if (dtCheckBCUnit.Rows.Count > 1
                        || (dtCheckBCUnit.Rows.Count == 1 && Convert.ToInt32(dtCheckBCUnit.Rows[0]["A_count"]) > 0 && dtCheckBCUnit.Rows[0]["A_CertCode"] == DBNull.Value)
                        )
                    {

                        sql = @"select CertificateCode, '持证所属单位：' +UNITNAME  as UNITNAME
                                from [dbo].[CERTIFICATE]
                                where [WorkerCertificateCode]='{0}' 
                                    and posttypeid=1 and ValidEndDate >=dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销'
                                    order by CertificateCode";

                        DataTable tb = CommonDAL.GetDataTable(string.Format(sql, LabelWorkerCertificateCode.Text));
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            foreach (DataRow r in tb.Rows)
                            {
                                sb.AppendFormat("<br />{0}，{1}。", r["CertificateCode"], r["UnitName"]);
                            }
                            sb.Insert(0, "校验未通过。校验规则：持证人同时持有有效的A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业。请按规则变更到统一单位后在提交续期申请。");

                            UIHelp.layerAlert(Page, string.Format("{0}", sb), 5, 0);
                            return;
                        }

                    }
                }
            }

            
            
            #endregion

            o.NewUnitCheckTime = DateTime.Now;
            o.NewUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交初审单位审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
            o.Status = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.CertificateContinueStatus.Applyed : EnumManager.CertificateContinueStatus.SendBack);
            try
            {
                CertificateContinueDAL.Update(o);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "单位审核证书续期申请失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "单位审核证书续期申请", string.Format("证书编号：{0}，岗位工种：{1}。", LabelCertificateCode.Text, LabelPostName.Text));

            UIHelp.layerAlert(Page, "审核成功！", 6, 3000, "hideIfam(true);");
        }

        //初审单位初审
        protected void ButtonFirstCheck_Click(object sender, EventArgs e)
        {
            if(RadioButtonListFirstCheckResult.SelectedValue != "通过" && TextBoxFirstCheckRemark.Text.Trim()=="初审通过")
            {
                UIHelp.layerAlert(Page, "退回修改时，意见不能填写“初审通过”。");
                return;
            }
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();//事务对象

            try
            {
                CertificateContinueOB ob = ViewState["CertificateContinueOB"] as CertificateContinueOB;
                ob.GetDate = DateTime.Now;//受理时间
                ob.GetResult = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? "初审通过" : TextBoxFirstCheckRemark.Text); //受理结果
                ob.GetMan = PersonName;//受理人
                ob.Status = (RadioButtonListFirstCheckResult.SelectedValue == "通过" ? EnumManager.CertificateContinueStatus.Accepted : EnumManager.CertificateContinueStatus.SendBack);//状态
                ob.GetCode = UIHelp.GetNextBatchNumber(tran, "XQSL");//受理批次号
                CertificateContinueDAL.Update(tran, ob);

                tran.Commit();//提交事务
                UIHelp.WriteOperateLog(PersonName, UserID, "初审证书续期", string.Format("初审证书：证书编号：{0}。审核结果为“{1}”,审核意见：“{2}”。", LabelCertificateCode.Text,ob.Status, ob.GetResult));
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书续期初审失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, "审核成功！", 6, 3000, "hideIfam(true);");
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            //            string bgslp = "";//变更受理编批号
            //            string bgsh = ""; //变更审核编批号
            //            string bgjd = ""; //变更决定编批号
            //            string bggz = ""; //变更告知编批号
            //            string _path = "";//照片目标地址
            //            string _sourcePath = "";//照片原地址

            //            CertificateChangeOB certhfchange = (CertificateChangeOB)ViewState["certhfchange"];
            //            DBHelper dbhelper = new DBHelper();
            //            DbTransaction dtr = dbhelper.BeginTransaction();
            //            try
            //            {
            //                certhfchange.ModifyTime = DateTime.Now;

            //                if (RadioButtonListJWCheck.SelectedValue == "通过")
            //                {
            //                    //修改原表数据
            //                    CertificateOB certificateob = CertificateDAL.GetObject(certhfchange.CertificateID.Value);

            //                    certhfchange.Status = EnumManager.CertificateChangeStatus.Noticed;

            //                    bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
            //                    bgsh = UIHelp.GetNextBatchNumber(dtr, "BGSH"); //变更审核编批号
            //                    bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
            //                    bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号

            //                    certhfchange.GetDate = DateTime.Now;   //变更受理时间
            //                    certhfchange.GetResult = "通过";     //变更受理结论
            //                    certhfchange.GetMan = PersonName;    //变更受理人
            //                    certhfchange.GetCode = bgslp;//变更受理编批号
            //                    certhfchange.CheckDate = DateTime.Now;  //变更审核时间
            //                    certhfchange.CheckResult = "通过";  //变更审核结论
            //                    certhfchange.CheckMan = PersonName; //变更审核人
            //                    certhfchange.CheckCode = bgsh;    //变更审核批号             
            //                    certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
            //                    certhfchange.ConfrimResult = "通过";  //变更决定结论
            //                    certhfchange.ConfrimMan = PersonName;   //变更决定人
            //                    certhfchange.ConfrimCode = bgjd;   //变更决定批号
            //                    certhfchange.NoticeDate = DateTime.Now; //变更告知时间 
            //                    certhfchange.NoticeResult = "通过";  //变更告知结论
            //                    certhfchange.NoticeMan = PersonName;    //变更告知人
            //                    certhfchange.NoticeCode = bggz;   //变更告知批号
            //                    certhfchange.DealWay = "证书信息修改";     //证书处理方式 

            //                    #region 更换证书照片

            //                    //更换证书照片
            //                    if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
            //                    {
            //                        _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));
            //                        if (!Directory.Exists(Server.MapPath(_path)))
            //                        {
            //                            System.IO.Directory.CreateDirectory(Server.MapPath(_path));
            //                        }
            //                        _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
            //                        _sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
            //                        File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
            //                        certificateob.FacePhoto = _path.Replace("..", "~");

            //                    }
            //                    //补充老照片
            //                    if (string.IsNullOrEmpty(certificateob.FacePhoto) == true)
            //                    {
            //                        if (string.IsNullOrEmpty(certhfchange.WorkerCertificateCode) == false && certhfchange.WorkerCertificateCode.Length > 2)
            //                        {
            //                            _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.WorkerCertificateCode.Substring(certhfchange.WorkerCertificateCode.Length - 3, 3), certhfchange.WorkerCertificateCode);
            //                        }
            //                        else
            //                        {
            //                            _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.NewWorkerCertificateCode.Substring(certhfchange.NewWorkerCertificateCode.Length - 3, 3), certhfchange.NewWorkerCertificateCode);
            //                        }

            //                        _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));

            //                        if (!Directory.Exists(Server.MapPath(_path)))
            //                        {
            //                            System.IO.Directory.CreateDirectory(Server.MapPath(_path));
            //                        }
            //                        _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
            //                        if (File.Exists(Server.MapPath(_sourcePath)) == true)//立即同步照片
            //                        {

            //                            File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
            //                            certificateob.FacePhoto = _path.Replace("..", "~");
            //                        }
            //                        else//夜间同步照片
            //                        {
            //                            certificateob.FacePhoto = null;
            //                        }
            //                    }

            //                    #endregion 更换证书照片

            //                    certificateob.WorkerCertificateCode = certhfchange.NewWorkerCertificateCode;   //证件号码
            //                    certificateob.Birthday = certhfchange.NewBirthday;//出生日期
            //                    certificateob.Sex = certhfchange.NewSex;//性别
            //                    certificateob.WorkerName = certhfchange.NewWorkerName;    //姓名
            //                    certificateob.UnitName = certhfchange.NewUnitName;   //工作单位
            //                    certificateob.UnitCode = certhfchange.NewUnitCode;   //组织机构代码
            //                    certificateob.ModifyPersonID = PersonID;  //最后修改人
            //                    certificateob.ModifyTime = DateTime.Now;   //最后修改时间
            //                    certificateob.CheckDate = certhfchange.CheckDate;    //审批时间
            //                    certificateob.CheckMan = certhfchange.CheckMan;      //审批人
            //                    certificateob.CheckAdvise = certhfchange.CheckResult;//审批意见
            //                    certificateob.Status = certhfchange.ChangeType;      //证书更新状态（变更类型）
            //                    certificateob.CaseStatus = "已归档";//归档状态    
            //                    certificateob.PrintCount = 1;
            //                    certificateob.ApplyMan = certhfchange.ApplyMan;//申请人

            //                    //修该变更记录
            //                    CertificateChangeDAL.Update(dtr, certhfchange);

            //                    //根据证书id向历史表插入历史数据
            //                    CertificateHistoryDAL.InsertChangeHistory(dtr, Convert.ToInt32(certhfchange.CertificateID));

            //                    //更新证书
            //                    CertificateDAL.Update(dtr, certificateob);

            //                    //更新人员基本信息
            //                    WorkerOB _WorkerOB = WorkerDAL.GetUserObject(certificateob.WorkerCertificateCode);
            //                    if (_WorkerOB != null)//update
            //                    {
            //                        if (_WorkerOB.Birthday != certificateob.Birthday
            //                            || _WorkerOB.Sex != certificateob.Sex
            //                            || _WorkerOB.WorkerName != certificateob.WorkerName
            //                            )
            //                        {
            //                            _WorkerOB.Birthday = certificateob.Birthday;
            //                            _WorkerOB.Sex = certificateob.Sex;
            //                            _WorkerOB.WorkerName = certificateob.WorkerName;
            //                            WorkerDAL.Update(dtr, _WorkerOB);
            //                        }
            //                    }
            //                    else//new
            //                    {
            //                        _WorkerOB = new WorkerOB();
            //                        _WorkerOB.Birthday = certificateob.Birthday;
            //                        _WorkerOB.Sex = certificateob.Sex;
            //                        _WorkerOB.WorkerName = certificateob.WorkerName;
            //                        _WorkerOB.Phone = certhfchange.LinkWay;
            //                        _WorkerOB.CertificateCode = certificateob.WorkerCertificateCode;
            //                        if (certificateob.WorkerCertificateCode.Length == 15 || certificateob.WorkerCertificateCode.Length == 18)
            //                            _WorkerOB.CertificateType = "身份证";
            //                        else
            //                            _WorkerOB.CertificateType = "其它证件";
            //                        WorkerDAL.Insert(dtr, _WorkerOB);
            //                    }

            //                    #region 更新证书标准附件库

            //                    //更新证书附件中需要被覆盖的附件为历史附件
            //                    CommonDAL.ExecSQL(dtr, string.Format(@"
            //                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
            //                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
            //                                    from [dbo].[COC_TOW_Person_File]
            //                                    inner join 
            //                                    (
            //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
            //	                                    from 
            //	                                    (
            //		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].[CERTIFICATECODE] as PSN_RegisterNo
            //		                                    from [dbo].[FileInfo]
            //		                                    inner join [dbo].[ApplyFile]
            //		                                    on [FileInfo].FileID = [ApplyFile].FileID
            //		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
            //		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
            //	                                    ) a
            //	                                    inner join 
            //	                                    (
            //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
            //		                                    from [dbo].[FileInfo]
            //		                                    inner join [dbo].[COC_TOW_Person_File]
            //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
            //                                            inner join [dbo].[VIEW_CERTIFICATECHANGE] 
            //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
            //		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
            //	                                    ) b 
            //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
            //                                    ) t
            //                                    on [COC_TOW_Person_File].FileID = t.FileID", certhfchange.CertificateChangeID));


            //                    CommonDAL.ExecSQL(dtr, string.Format(@"
            //                                    delete from [dbo].[COC_TOW_Person_File]
            //                                    where FileID in( select [COC_TOW_Person_File].[FileID]
            //                                    from [dbo].[COC_TOW_Person_File]
            //                                    inner join 
            //                                    (
            //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
            //	                                    from 
            //	                                    (
            //		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE as PSN_RegisterNo
            //		                                    from [dbo].[FileInfo]
            //		                                    inner join [dbo].[ApplyFile]
            //		                                    on [FileInfo].FileID = [ApplyFile].FileID
            //		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
            //		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
            //	                                    ) a
            //	                                    inner join 
            //	                                    (
            //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
            //		                                    from [dbo].[FileInfo]
            //		                                    inner join [dbo].[COC_TOW_Person_File]
            //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
            //                                            inner join [dbo].[VIEW_CERTIFICATECHANGE] 
            //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
            //		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
            //	                                    ) b 
            //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
            //                                    ) t
            //                                    on [COC_TOW_Person_File].FileID = t.FileID
            //                                    )", certhfchange.CertificateChangeID));

            //                    //将申请单附件写入证书附件库
            //                    CommonDAL.ExecSQL(dtr, string.Format(@"
            //                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
            //                                    select [ApplyFile].FileID,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE,0 
            //                                    from [dbo].[ApplyFile]
            //                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] 
            //                                    on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
            //                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID='{0}' and [VIEW_CERTIFICATECHANGE].GetResult='通过'", certhfchange.CertificateChangeID));



            //                    #endregion 更新证书标准附件库

            //                    ViewState["certhfchange"] = certhfchange;
            //                    ViewState["certificateob"] = certificateob;
            //                }
            //                else //不通过
            //                {
            //                    certhfchange.Status = EnumManager.CertificateChangeStatus.SendBack;
            //                    bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
            //                    certhfchange.GetDate = DateTime.Now;   //变更受理时间
            //                    certhfchange.GetResult = TextBoxCheckResult.Text.Trim();     //变更受理结论
            //                    certhfchange.GetMan = PersonName;    //变更受理人
            //                    certhfchange.GetCode = bgslp;//变更受理编批号

            //                    //修该变更记录
            //                    CertificateChangeDAL.Update(dtr, certhfchange);
            //                    ViewState["certhfchange"] = certhfchange;
            //                }

            //                dtr.Commit();
            //            }
            //            catch (Exception ex)
            //            {
            //                dtr.Rollback();
            //                UIHelp.WriteErrorLog(Page, string.Format("单位审核证书{0}申请失败！", certhfchange.ChangeType), ex);
            //                return;
            //            }
            //            UIHelp.WriteOperateLog(PersonName, UnitID, string.Format("建委审核证书{0}申请", certhfchange.ChangeType), string.Format("证书编号：{0}，岗位工种：{1}。", RadTextBoxCertificateCode.Text, RadTextBoxPostID.Text));

            //            UIHelp.layerAlert(this.Page, "审核成功！", "hideIfam(true);");
            //            //ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            string id = "";
            if (PersonType == 3)
            {
                id = UnitID;
            }
            else
            {
                id = PersonID.ToString();
            }
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifApply/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifApply/" + id))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifApply/" + id));
        }

        //导出续期申请表
        protected void ButtonExport_Click(object sender, EventArgs e)
        {          
            CheckSaveDirectory();
            string id = "";
            if (PersonType == 3)
            {
                id = UnitID;
            }
            else
            {
                id = PersonID.ToString();
            }
            string sourceFile = "";
            CertificateContinueOB o = (CertificateContinueOB)ViewState["CertificateContinueOB"];
            var ht = PrintDocument.GetProperties(o);

            //录入的选修课课时总数
            if (divAnQuanPXFinishCase.InnerText != "" && divAnQuanPXFinishCase.InnerText != "已达标")
            {
                UIHelp.layerAlert(Page, divAnQuanPXFinishCase.InnerText, 5, 0);
                return;
            }

            //公益视频课时总数
            if (div_GongYiPX.InnerText != "" && div_GongYiPX.InnerText != "已达标")
            {
                UIHelp.layerAlert(Page, div_GongYiPX.InnerText, 5, 0);
                return;
            }

            string sql = "";
            if (ViewState["PostTypeID"].ToString() == "2")//特种作业
            {
               
                sourceFile = HttpContext.Current.Server.MapPath("~/Template/证书续期申请表_特种作业.docx");
                sql = string.Format(@"SELECT [DataNo],year([TrainDateStart]),convert(varchar(10),[TrainDateStart],21) +'至' + convert(varchar(10),[TrainDateEnd],21),[TrainName],[TrainWay],[TrainUnit],[Period]  
                                            FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}' and [PostTypeID] ={1}) order by [DataNo] "
                   , o.CertificateContinueID, ViewState["PostTypeID"]);
            }
            else//三类人
            {
                sourceFile = HttpContext.Current.Server.MapPath("~/Template/证书续期申请表.docx");
                sql = string.Format(@"SELECT [DataNo],year([TrainDateStart]),convert(varchar(10),[TrainDateStart],21) +'至' + convert(varchar(10),[TrainDateEnd],21),[TrainName],[TrainWay],[Period],[ExamResult]  
                                            FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}' and [PostTypeID] ={1}) order by [DataNo] "
                   , o.CertificateContinueID, ViewState["PostTypeID"]);
            }
            
            ht["ApplyDate"] =   LabelApplyDate.Text;//申请日期
            ht["ApplyCode"] =   LabelApplyCode.Text;//申请批号
            ht["WorkerName"] =   LabelWorkerName.Text;//姓名
            ht["WorkerCertificateCode"] =   LabelWorkerCertificateCode.Text;//证件号码
            ht["Sex"] =   LabelSex.Text;//性别
            ht["Age"] =   LabelBirthday.Text;//出生日期
            ht["CertificateCode"] =   LabelCertificateCode.Text;//证书编号   
            ht["ConferDate"] =   LabelConferDate.Text; //发证时间
            ht["ValidDate"] =   LabelValidDataTo.Text; //有效期至
            ht["Phone"] =   RadTextBoxPhone.Text.Trim();//电话
            ht["PostID"] =    LabelPostName.Text;//工种
            ht["CulturalLevel"] =   LabelCulturalLevel.Text;//文化程度
            ht["SkillLevel"] =   LabelCulturalLevel.Text;//技术职称
            ht["WorkYearNumer"] =   LabelWorkStartDate.Text;//工作时间
            ht["UnitName"] = RadTextBoxUnitName.Text;//企业名称
            ht["UnitCode"] =   RadTextBoxUnitCode.Text;//组织代码
            ht["photo"] =   UIHelp.GetFaceImagePath(ViewState["FacePhoto"] == null ? "" : ViewState["FacePhoto"].ToString(), LabelWorkerCertificateCode.Text);//绑定照片

            
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

            PrintDocument.CreateDataToWordByHashtable(sourceFile, string.Format("续期申请表_{0}", LabelWorkerName.Text), ht);

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl("证书续期申请表", string.Format("~/UpLoad/CertifApply/{0}/{1}.doc", id, LabelApplyCode.Text)));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }       

        ///// <summary>
        ///// 显示社保比对结果
        ///// </summary>
        ///// <param name="PostTypeID"></param>
        ///// <param name="ApplyDate"></param>
        ///// <param name="WorkerCertificateCode"></param>
        ///// <param name="UnitCode"></param>
        //private void ShowSheBao(string PostTypeID, DateTime ApplyDate, string WorkerCertificateCode, string UnitCode)
        //{
        //    CertificateContinueOB o = ViewState["CertificateContinueOB"] as CertificateContinueOB;
        //    string tip = "";
        //    if (PostTypeID == "2"//特种作业
        //        && RadTextBoxNewUnitCode.Text != RadTextBoxUnitCode.Text)//变换了单位
        //    {
        //        //检查社保相关扫描件上传文件是否存在
        //        bool SheBaoCheckFile = ApplyDAL.CheckIfUploadFileType(ApplyID, EnumManager.FileDataTypeName.社保扫描件);



        //        if (SheBaoCheckFile == false //没上传社保证明
        //            && (o.SheBaoCheck.HasValue == false || o.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
        //            )
        //        {
        //            tip = "<p style='color:red'>重要提示：您的申请单“社保自动核验”、“上传社保扫描件”两项都不符合要求。<br />请在保存申请单次日查看社保自动验核结果，比对合格再上报单位审核，不符合必须上传其他相关证明材料。</p>";
        //        }

        //    }

        //    if (
        //        (PostTypeID == "2" && RadTextBoxNewUnitCode.Text != RadTextBoxUnitCode.Text) && (ApplyDate.CompareTo(DateTime.Parse("2014-07-01")) >= 0)
        //        )
        //    {
        //        divSheBao.InnerHtml = string.Format("{4}社保自动验核结果：<span style='font-weight: bold;'>{3}</span>。<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>查看社保信息</nobr></span>", WorkerCertificateCode, UnitCode, ApplyDate.ToString()
        //              , o.SheBaoCheck.HasValue == false ? "尚未比对（夜间比对）" : o.SheBaoCheck.Value == 1 ? "符合" : "不符合"
        //        , tip
        //        );
        //    }
        //}

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyID"></param>
        private void BindFile(string ApplyID)
        {
            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(string.IsNullOrEmpty(ApplyID) == true ? "-1" : ApplyID);
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
                UIHelp.WriteErrorLog(Page, "删除变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除变更申请表附件成功", string.Format("证书编号：{0}，文件名称：{1}。", LabelCertificateCode.Text, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileName"]));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        //绑定待编辑年度安全生产教育培训记录
        protected void RadGridAnQuanPX_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                if (ViewState["PostTypeID"].ToString() == "1")//三类人员，不显示培训企业
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    Control trTrainUnit = editedItem.Cells[0].FindControl("trTrainUnit") as Control;
                    trTrainUnit.Visible = false;                   
                }
                else//特种作业，不显示考核结果
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    Control trExamResult = editedItem.Cells[0].FindControl("trExamResult") as Control;
                    trExamResult.Visible = false;
                }
                
                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    Button ButtonSave = editedItem.Cells[0].FindControl("ButtonSave") as Button;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);

                    Int64 id = Convert.ToInt64(RadGridAnQuanPX.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
                    JxjyDetailMDL _JxjyDetailMDL = JxjyDetailDAL.GetObject(id);
                    ViewState["JxjyDetailMDL"] = _JxjyDetailMDL;
                    UIHelp.SetData(editedItem, _JxjyDetailMDL);

                    RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
                    RadioButtonListTrainWay.Items.FindByValue(_JxjyDetailMDL.TrainWay).Selected=true;

                    RadioButtonList RadioButtonListExamResult = editedItem.Cells[0].FindControl("RadioButtonListExamResult") as RadioButtonList;
                    RadioButtonListExamResult.Items.FindByValue(_JxjyDetailMDL.ExamResult).Selected = true;
                }
            }
        }

        //新增年度安全生产教育培训记录
        protected void RadGridAnQuanPX_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            CertificateContinueOB ContinueOB = (CertificateContinueOB)ViewState["CertificateContinueOB"];

            JxjyBaseMDL _JxjyBaseMDL = JxjyBaseDAL.GetObjectByApplyID(Convert.ToInt32(ViewState["PostTypeID"]), ContinueOB.CertificateContinueID.ToString());

            JxjyDetailMDL _JxjyDetailMDL= new JxjyDetailMDL();
            RadNumericTextBox RadNumericTextBoxDataNo = editedItem.Cells[0].FindControl("RadNumericTextBoxDataNo") as RadNumericTextBox;
            _JxjyDetailMDL.DataNo = Convert.ToInt32(RadNumericTextBoxDataNo.Value);
            RadDatePicker RadDatePickerTrainDateStart = editedItem.Cells[0].FindControl("RadDatePickerTrainDateStart") as RadDatePicker;
            _JxjyDetailMDL.TrainDateStart = RadDatePickerTrainDateStart.SelectedDate;
            RadDatePicker RadDatePickerTrainDateEnd = editedItem.Cells[0].FindControl("RadDatePickerTrainDateEnd") as RadDatePicker;
            _JxjyDetailMDL.TrainDateEnd = RadDatePickerTrainDateEnd.SelectedDate;
            RadTextBox RadTextBoxTrainName = editedItem.Cells[0].FindControl("RadTextBoxTrainName") as RadTextBox;
            _JxjyDetailMDL.TrainName = UIHelp.ToHalfCode(RadTextBoxTrainName.Text);
            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _JxjyDetailMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;
            RadTextBox RadTextBoxTrainUnit = editedItem.Cells[0].FindControl("RadTextBoxTrainUnit") as RadTextBox;
            _JxjyDetailMDL.TrainUnit = RadTextBoxTrainUnit.Text;
            RadNumericTextBox RadNumericTextBoxPeriod = editedItem.Cells[0].FindControl("RadNumericTextBoxPeriod") as RadNumericTextBox;
            _JxjyDetailMDL.Period = Convert.ToInt32(RadNumericTextBoxPeriod.Value);

            RadioButtonList RadioButtonListExamResult = editedItem.Cells[0].FindControl("RadioButtonListExamResult") as RadioButtonList;
            _JxjyDetailMDL.ExamResult = RadioButtonListExamResult.SelectedValue;

            _JxjyDetailMDL.cjsj = DateTime.Now;

            if (_JxjyBaseMDL == null)
            {
                DBHelper db = new DBHelper();
                DbTransaction trans = db.BeginTransaction();
                try
                {
                    _JxjyBaseMDL = new JxjyBaseMDL();
                    _JxjyBaseMDL.PostTypeID = Convert.ToInt32(ViewState["PostTypeID"]);
                    _JxjyBaseMDL.WorkerName = LabelWorkerName.Text.Trim();
                    _JxjyBaseMDL.WorkerCertificateCode = LabelWorkerCertificateCode.Text.Trim();
                    _JxjyBaseMDL.PostTypeName = HiddenFieldPostTypeName.Value;
                    _JxjyBaseMDL.PostName = LabelPostName.Text.Trim();
                    _JxjyBaseMDL.CertificateCode = LabelCertificateCode.Text.Trim();
                    _JxjyBaseMDL.ValidEndDate = Convert.ToDateTime(LabelValidDataTo.Text);
                    _JxjyBaseMDL.UnitName = RadTextBoxUnitName.Text.Trim();
                    _JxjyBaseMDL.cjsj = DateTime.Now;
                    _JxjyBaseMDL.ApplyID = ContinueOB.CertificateContinueID.ToString();
                    JxjyBaseDAL.Insert(trans, _JxjyBaseMDL);

                    _JxjyDetailMDL.BaseID = _JxjyBaseMDL.BaseID;                  
                    JxjyDetailDAL.Insert(trans, _JxjyDetailMDL);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    UIHelp.WriteErrorLog(Page, "添加年度安全生产教育培训记录失败！", ex);
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
                    UIHelp.WriteErrorLog(Page, "添加年度安全生产教育培训记录失败！", ex);
                    return;
                }
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "添加年度安全生产教育培训记录", string.Format("续期证书编号：{0}。", LabelCertificateCode.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //修改年度安全生产教育培训记录
        protected void RadGridAnQuanPX_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadTextBox RadTextBoxTrainName = editedItem.Cells[0].FindControl("RadTextBoxTrainName") as RadTextBox;

            JxjyDetailMDL _JxjyDetailMDL = (JxjyDetailMDL)ViewState["JxjyDetailMDL"];
            UIHelp.GetData(editedItem, _JxjyDetailMDL);
            _JxjyDetailMDL.TrainName = UIHelp.ToHalfCode(RadTextBoxTrainName.Text);
            if(_JxjyDetailMDL.TrainUnit==null)
            {
                _JxjyDetailMDL.TrainUnit = "";
            }
            RadioButtonList RadioButtonListTrainWay = editedItem.Cells[0].FindControl("RadioButtonListTrainWay") as RadioButtonList;
            _JxjyDetailMDL.TrainWay = RadioButtonListTrainWay.SelectedValue;

            RadioButtonList RadioButtonListExamResult = editedItem.Cells[0].FindControl("RadioButtonListExamResult") as RadioButtonList;
            _JxjyDetailMDL.ExamResult = RadioButtonListExamResult.SelectedValue;

            try
            {
                JxjyDetailDAL.Update(_JxjyDetailMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更新年度安全生产教育培训记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新年度安全生产教育培训记录", string.Format("续期证书编号：{0}。", LabelCertificateCode.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //删除年度安全生产教育培训记录
        protected void RadGridAnQuanPX_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 DetailID = Convert.ToInt64(RadGridAnQuanPX.MasterTableView.DataKeyValues[e.Item.ItemIndex]["DetailID"]);
            try
            {
                JxjyDetailDAL.Delete(DetailID);

                JxjyBaseDAL.DeleteNullSubDetail(Convert.ToInt32(ViewState["PostTypeID"]),((CertificateContinueOB)ViewState["CertificateContinueOB"]).CertificateContinueID.ToString());

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除年度安全生产教育培训记录失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除年度安全生产教育培训记录", string.Format("续期证书编号：{0}。", LabelCertificateCode.Text));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", RadGridAnQuanPX.ClientID), true);
        }

        //提供年度安全生产教育培训记录
        protected void RadGridAnQuanPX_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshRadGridAnQuanPX();
        }

        /// <summary>
        /// 绑定年度安全生产教育培训记录（三类人：每年20学时；特种作业：每年24学时）
        /// </summary>
        protected void RefreshRadGridAnQuanPX()
        {
            int PostTypeID = Convert.ToInt32(ViewState["PostTypeID"]);
            int YearSpan = (PostTypeID == 2 ? 2 : 3);//要求年度
            int period = (PostTypeID == 2 ? 24 : 20);//每年要求学时
            try
            {
                CertificateContinueOB o = ViewState["CertificateContinueOB"] as CertificateContinueOB;
                DataTable dt = JxjyDetailDAL.GetList(PostTypeID, o.CertificateContinueID.ToString());//继续教育委培记录
                RadGridAnQuanPX.DataSource = dt;
                DateTime checkdate = (o.ApplyDate.HasValue == false ? o.ModifyTime.Value : o.ApplyDate.Value);
                //if (JxjyDetailDAL.CheckXuanXiuPeriod(YearSpan, period, checkdate, o.CertificateContinueID.ToString(), PostTypeID) == false)
                //{
                //    if (PostTypeID == 2)//特种作业每年24学分
                //    {
                //        divAnQuanPXFinishCase.InnerText = string.Format("未达标。年度安全生产教育培训记录未达到近{0}年{2}学时（每年{1}学时），请根据培训情况在线填写。", YearSpan, period, YearSpan * period);
                //    }
                //    else//三类人不限学分，每年都要有记录
                //    {
                //        divAnQuanPXFinishCase.InnerText = string.Format("未达标。年度安全生产教育培训记录未达到近{0}年参加培训，请根据培训情况在线填写。", YearSpan );
                //    }
                //}
                //else
                //{
                //    divAnQuanPXFinishCase.InnerText = "已达标";
                //}


                if (PostTypeID == 2)//特种作业每年24学分
                {
                    RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("ExamResult").Visible = false;

                    if (JxjyDetailDAL.CheckXuanXiuPeriod(YearSpan, period, checkdate, o.CertificateContinueID.ToString(), PostTypeID) == false)
                    {
                        divAnQuanPXFinishCase.InnerText = string.Format("未达标。年度安全生产教育培训记录未达到近{0}年{2}学时（每年{1}学时，请分年度填写），请根据培训情况在线填写。", YearSpan, period, YearSpan * period);
                    }
                    else
                    {
                        divAnQuanPXFinishCase.InnerText = "已达标";
                    }
                }
                else//三类人不限学分(临时)
                {
                    RadGridAnQuanPX.MasterTableView.Columns.FindByUniqueName("TrainUnit").Visible = false;

                    //if (JxjyDetailDAL.CheckXuanXiuSumPeriod(YearSpan, period, checkdate, o.CertificateContinueID.ToString(), PostTypeID) == false)
                    if (JxjyDetailDAL.CheckXuanXiuPeriod(YearSpan, period, checkdate, o.CertificateContinueID.ToString(), PostTypeID) == false)
                    {
                        divAnQuanPXFinishCase.InnerText = string.Format("未达标。年度安全生产教育培训记录未达到近{0}年{2}学时（每年{1}学时，请分年度填写），请根据培训情况在线填写。", YearSpan, period, YearSpan * period);
                    }
                    else
                    {
                        if (period == 0)
                        {
                            divAnQuanPXFinishCase.InnerText = "";
                        }
                        else
                        {
                            divAnQuanPXFinishCase.InnerText = "已达标";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取年度安全生产教育培训记录失败！", ex);
                return;
            }
        }

        /// <summary>
        /// 显示公益培训记录（三类人：3年8学时；特种作业：2年每年8学时）
        /// </summary>
        protected void ShowFinishGYPX()
        {
            trGongYiPXTitle.Visible = true;
            trGongYiPX.Visible = true;

            PackageMDL _PackageMDL = PackageDAL.GetObject(HiddenFieldPostTypeName.Value, LabelPostName.Text.Trim());
            int needPeriod = _PackageMDL.Period.Value / 45;//每年应当完成学时数
            int YearSpan = (ViewState["PostTypeID"].ToString() == "2" ? 2 : 1);//要求年度(三类人不分年度，只要求总数)

            if (ViewState["PostTypeID"].ToString() == "1")
            {               

                //本人已经完成的学时数
                decimal myPeriod = FinishSourceWareDAL.GetFinisthPeriod(LabelWorkerCertificateCode.Text.Trim(), HiddenFieldPostTypeName.Value, LabelPostName.Text.Trim(), Convert.ToDateTime(LabelValidDataTo.Text).AddYears(-3), Convert.ToDateTime(LabelValidDataTo.Text));

                if (
                    (ViewState["CertificateContinueOB"] != null && ((CertificateContinueOB)ViewState["CertificateContinueOB"]).Status == EnumManager.CertificateContinueStatus.Decided)
                    || myPeriod >= needPeriod
                    || GYPXWhiteDAL.CheckWhiteList(LabelWorkerCertificateCode.Text.Trim(),1)==true
                    )
                {
                    div_GongYiPX.InnerText = "已达标";
                }
                else
                {
                    //div_GongYiPX.InnerText = string.Format("未达标，您未完成近3年{0}学时的证书延期复核继续教育学习。点击“公益教育培训 - 我的培训 - 个人空间”选择证书对应课程学习。", needPeriod );
                    div_GongYiPX.InnerHtml = string.Format("未达标，您未完成近3年{0}学时的证书延期复核继续教育学习。点击<a href=\"../jxjy/MyTrain.aspx?o=grkj\" target=\"_blank\">【公益教育培训 - 我的培训 - 个人空间】</a>选择证书对应课程学习。", needPeriod);
                    
                }
            }
            else if (ViewState["PostTypeID"].ToString() == "2")
            {

                //本人已经完成的学时数
                decimal myPeriod = FinishSourceWareDAL.GetFinisthPeriod(LabelWorkerCertificateCode.Text.Trim(), HiddenFieldPostTypeName.Value, LabelPostName.Text.Trim(), Convert.ToDateTime(LabelValidDataTo.Text).AddYears(-2), Convert.ToDateTime(LabelValidDataTo.Text));

                if (
                    (ViewState["CertificateContinueOB"] != null && ((CertificateContinueOB)ViewState["CertificateContinueOB"]).Status==EnumManager.CertificateContinueStatus.Decided) 
                    || myPeriod >= (needPeriod *2)
                    || GYPXWhiteDAL.CheckWhiteList(LabelWorkerCertificateCode.Text.Trim(), 2) == true
                    )
                {
                    div_GongYiPX.InnerText ="已达标";
                }
                else
                {
                    div_GongYiPX.InnerHtml = string.Format("未达标，您未完成近2年{0}学时的证书延期复核继续教育学习。点击<a href=\"../jxjy/MyTrain.aspx?o=grkj\" target=\"_blank\">【公益教育培训 - 我的培训 - 个人空间】</a>选择证书对应课程学习。", needPeriod * 2);
                }
            }
        }

    }
}