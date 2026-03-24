using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.IO;
using System.Data;

namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterApplyEdit : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("2").Remove();//屏蔽特种作业类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("3").Remove();//屏蔽特种作业类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("4").Remove();//屏蔽职业技能类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别
            UIHelp.FillDropDownList(RadComboBoxSKILLLEVEL, "111", "请选择", ""," [SORTID] between 6 and 10");//技术职称 
            UIHelp.FillDropDownList(RadComboBoxJob, "112", "请选择", "");//职务 
            RadComboBoxJob.Items.Remove(RadComboBoxJob.FindItemIndexByText("其他"));
        }
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifEnterApplyList.aspx|CertifEnterCheck.aspx|CertifEnterAccepted.aspx";
            }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["CertificateEnterApplyOB"] == null ? "" : string.Format("JJ-{0}", (ViewState["CertificateEnterApplyOB"] as CertificateEnterApplyOB).ApplyID); }
        }

        /// <summary>
        /// 从社会统一代码中获取组织机构代码
        /// </summary>
        protected string UnitCode
        {
            get
            {
                if (RadTextBoxCreditCode.Text.Trim().Length == 9)
                    return RadTextBoxCreditCode.Text.Trim();
                else
                    return RadTextBoxCreditCode.Text.Trim().Substring(8, 9);    //机构号码
            }
        }

        /// <summary>
        /// 验证是否为企业法人
        /// </summary>
        protected bool IfFaRen
        {
            get
            {
                if (ViewState["IfFaRen"] == null)
                {
                    ViewState["IfFaRen"] = CertificateDAL.IFExistFRByUnitCode(UnitCode, RadTextBoxWorkerName.Text.Trim());

                }
                return Convert.ToBoolean(ViewState["IfFaRen"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                ImgCode.Src = "~/Images/photo_ry.jpg";
                ViewState["rtn"] = (string.IsNullOrEmpty(Request["rtn"]) == true) ? "CertifEnterApplyList.aspx" : Request["rtn"];
                if (string.IsNullOrEmpty(Request["t"]) == false)
                {
                    string PostTypeID = Utility.Cryptography.Decrypt(Request["t"]);
                    ViewState["PostTypeID"] = PostTypeID;//岗位类别ID
                    LabelPostType.Text = UIHelp.GetPostTypeNameByID(PostTypeID);
                    PostSelect1.PostTypeID = PostTypeID;
                    PostSelect1.LockPostTypeID();
                }
                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    string applyid = Utility.Cryptography.Decrypt(Request["o"]);
                    ButtonSave.Text = "修 改";
                    CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(applyid));
                    ViewState["CertificateEnterApplyOB"] = _CertificateEnterApplyOB;
                    PostSelect1.PostTypeID = _CertificateEnterApplyOB.PostTypeID.ToString();  
                    PostSelect1.PostID = _CertificateEnterApplyOB.PostID.ToString();
                    PostSelect1.Enabled = false;
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        UIHelp.SetData(TableEdit, _CertificateEnterApplyOB,false);
                    }
                    else
                    {
                        UIHelp.SetData(TableEdit, _CertificateEnterApplyOB,true);
                    }
                    UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话

                    if (RadComboBoxSKILLLEVEL.FindItemByText(_CertificateEnterApplyOB.SkillLevel) != null) //技术职称
                    {
                        RadComboBoxSKILLLEVEL.FindItemByText(_CertificateEnterApplyOB.SkillLevel).Selected = true; 
                    }
                    if (RadComboBoxJob.FindItemByText(_CertificateEnterApplyOB.Job) != null) //职务
                    {
                        RadComboBoxJob.FindItemByText(_CertificateEnterApplyOB.Job).Selected = true;
                    }                   

                    LabelApplyCode.Text = _CertificateEnterApplyOB.ApplyCode; //批次号
                    LabelApplyDate.Text = _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd");
                    //ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_CertificateEnterApplyOB.WorkerCertificateCode)));
                    ImgCode.Src = UIHelp.ShowFaceImage(_CertificateEnterApplyOB.WorkerCertificateCode);
                    if (_CertificateEnterApplyOB.Sex == "男")  //性别
                    {
                        RadioButtonMan.Checked = true;
                        RadioButtonWoman.Checked = false;
                    }
                    else
                    {
                        RadioButtonMan.Checked = false;
                        RadioButtonWoman.Checked = true;
                    }

                    //保存后不能修改证件号码
                    RadTextBoxWorkerCertificateCode.Enabled = false;

                    BindCheckHistory(_CertificateEnterApplyOB.ApplyID.Value);
                    SetButtonEnable(_CertificateEnterApplyOB.ApplyStatus);
                    SetStep(_CertificateEnterApplyOB.ApplyStatus);
                    BindFile(ApplyID);
                    ShowSheBao(_CertificateEnterApplyOB);

                    SetUploadFileType();

                    CertificateOutMDL _CertificateOutMDL = CertificateOutDAL.GetObject(_CertificateEnterApplyOB.CertificateCode);
                    if(_CertificateOutMDL !=null)
                    {
                        LabelCertCheckStatus.Text = "（ ✔ 证书数据已经通过全国工程质量安全监管信息平台核验 ）";
                    }
                    

//                    if(_CertificateEnterApplyOB.ZACheckResult==0)
//                    {

//                        UIHelp.layerAlertWithHtml(Page, string.Format(@"<div>进京申请没有通过<a href=\'https://zlaq.mohurd.gov.cn/fwmh/bjxcjgl/fwmh/pages/default/index.html\' target=\'_blank\'>【全国工程质量安全监管信息平台（可查询）】</a>数据校验，属于违规申请。<br/>
//                                                                    请先办理相关证书转出后，才能办理证书进京。（若外省证书已经转出，请联系原证书省份，查询数据是否已经同步到全国工程质量安全监管信息平台。）<br/><br/>
//                                                                    <b>校验结果说明：</b><span style=\'color:red\'>{0}</span><br/><br/>
//                                                                    <b>持证规则说明：</b><br/>
//                                                                    <div style=\'padding-left: 32px\'>
//                                                                        <b>A证发证要求：</b><br/>
//                                                                        >> 持证人有多本A证时，多本A证在不同企业下，最多存在一本非法人证A证；<br/>
//                                                                        >> 持证人同时持有A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业；<br/>
//                                                                        >> 一个企业只能存在一本法人A证。<br/><br/>
//
//                                                                        <b>B证发证要求：</b><br/>
//                                                                        >> 持证人在全国范围只允许持有一本B证；<br/>
//                                                                        >> 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
//
//                                                                        <b>C1/C2发证要求：</b><br/>
//                                                                        >> 同一人员（身份证），在全国只能取得一本C1或C2证，能同时取得C1和C2证，C1和C2证必须在同一企业下，在取得C1或C2证书时不能再获取C3证；<br/>
//                                                                        >> 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
//
//                                                                        <b>C3发证要求：</b><br/>
//                                                                        >> 同一人员（身份证），在全国只能取得一本C3证，在取得C3证书时不能再取得C1或C2证；<br/>
//                                                                        >> 同时存在B/C证时，B/C证都必须在同一企业下；<br/>
//                                                                        >> 持证人不能同时持有多本C1或C2或C3。<br/>
//                                                                    </div>
//                                                                  </div>", _CertificateEnterApplyOB.ZACheckRemark));    
//                    }
                }
                else//new
                {
                    if (PersonType == 2)//考生登录,默认读取个人信息
                    {
                        WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                        if (_WorkerOB != null)
                        {
                            RadTextBoxWorkerCertificateCode.Text = _WorkerOB.CertificateCode;
                            RadTextBoxWorkerCertificateCode.Enabled = false;//不准许修改证件号码
                            RadTextBoxWorkerName.Text = _WorkerOB.WorkerName;
                            UIHelp.SetReadOnly(RadTextBoxWorkerName, true);
                            RadTextBoxPhone.Text = _WorkerOB.Phone;
                            UIHelp.SetReadOnly(RadTextBoxPhone, true);//不允许修改来自大厅实名制认证电话

                            if (_WorkerOB.Birthday.HasValue) RadDatePickerBirthday.SelectedDate = _WorkerOB.Birthday;

                            UIHelp.SetReadOnly(RadTextBoxConferUnit, true);
                            UIHelp.SetReadOnly(RadDatePickerConferDate, true);
                            UIHelp.SetReadOnly(RadTextBoxCertificateCode, true);
                            UIHelp.SetReadOnly(RadDatePickerValidStartDate, true);
                            UIHelp.SetReadOnly(RadDatePickerValidEndDate, true);
                            UIHelp.SetReadOnly(RadTextBoxOldUnitName, true);


                            if (_WorkerOB.Sex == "女")
                            {
                                RadioButtonMan.Checked = false;
                                RadioButtonWoman.Checked = true;
                            }
                            else
                            {
                                RadioButtonMan.Checked = true;
                                RadioButtonWoman.Checked = false;
                            }

                            //ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_WorkerOB.CertificateCode)));
                            ImgCode.Src = UIHelp.ShowFaceImage(_WorkerOB.CertificateCode);

                        }
                        SetButtonEnable("");
                        SetStep("");
                        //UIHelp.layerAlert(Page, "请申请人登录“全国工程质量安全监管信息平台公共服务门户”-安全生产管理人员考核合格证书信息栏目查询原证书信息，应为“办理转出”状态方可提交进京申请。");
                    }
                }
            }
            else
            {
                if (Request["__EVENTTARGET"] == "refreshFile")//上传或删除附件刷新列表
                {
                    BindFile(ApplyID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
                }
                if(Request["__EVENTTARGET"] =="selectCert")//选择了一个外省转出证书
                {
                    CertificateOutMDL o = CertificateOutDAL.GetObject(Request["__EVENTARGUMENT"]);
                    if(o !=null)
                    {
                        PostSelect1.PostTypeID = "1";
                        PostSelect1.PostName = CertificateDAL.GetSLRPostNameByCode(o.out_categoryCode);
                        RadTextBoxConferUnit.Text = o.out_issuAuth;
                        RadDatePickerConferDate.SelectedDate = o.out_issuedDate;
                        RadDatePickerValidStartDate.SelectedDate = o.out_effectiveDate;
                        RadDatePickerValidEndDate.SelectedDate = o.out_expiringDate;
                        RadTextBoxCertificateCode.Text = o.out_certNum;
                        RadTextBoxOldUnitName.Text = o.out_companyName;
                    }
                    //UIHelp.layerAlert(Page, Request["__EVENTARGUMENT"]);
                }
            }
        }

        //保存申请单
        protected void ButtonSave_Click(object sender, EventArgs e)
        {            
            #region 进京规则检查

            if (string.IsNullOrEmpty(PostSelect1.PostID))
            {
                UIHelp.layerAlert(Page, "请选择岗位工种。", 5, 0);
                  return;
            }
            if (RadComboBoxJob.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "请选择所在单位职务。", 5, 0);
                return;
            }
            if (RadComboBoxSKILLLEVEL.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "请选择技术职称。", 5, 0);
                return;
            }

            if (PostSelect1.PostID == "148" && RadComboBoxJob.SelectedItem.Text != "项目负责人（项目经理）")
            {
                UIHelp.layerAlert(Page, "项目负责人职务必须选择“项目负责人（项目经理）”。", 5, 0);
                return;
            }
            if ((PostSelect1.PostID == "6" || PostSelect1.PostID == "1123" || PostSelect1.PostID == "1125") && RadComboBoxJob.SelectedItem.Text != "专职安全生产管理人员")
            {
                UIHelp.layerAlert(Page, "专职安全生产管理人员职务必须选择“专职安全生产管理人员”。", 5, 0);
                return;
            }
            if (PostSelect1.PostID == "147"
                    && (RadComboBoxJob.SelectedItem.Text == "项目负责人（项目经理）" || RadComboBoxJob.SelectedItem.Text == "专职安全生产管理人员")
                    )
            {
                UIHelp.layerAlert(Page, "企业主要负责人职务不允许选择“项目负责人（项目经理）”或“专职安全生产管理人员”，请选择其他职务。", 5, 0);
                return;
            }


            //1、三类人、造价员允许办理进京申请
            //2、证书号码已经提交过进京申请了，不能重复使用。
            //3、经提交过相同的岗位工种的进京申请了，不能重复申请（造价员有相同专业增项也不能重复申请）。
            //4、申请人员在京存在有效的同一岗位证书，不允许申请进京。（企业负责人可以重复申请，但必须保证单位不同）
            //5、每人只能有一个"项目负责人"和"专职安全生产管理人员"证，且两本必须在同一单位。
            //6、项目负责人（B本），身份证一致且新单位组织机构代码与建造师证书一致的允许进京。
            //7、三类人员证书进京，现聘用单位必须在企业资质库中。（比对施工企业、中央在京企业、设计施工一体化企业、起重机械租赁企业，不比对外地进京企业）
            //8、超龄检查：项目负责人年龄上限为65周岁；专职安全生产管理人员、造价员年龄上限为60周岁。 
            //9、证书有效期截止日期不低于十天，低于十天提示：“您的证书有效期低于10个工作日（办理进京业务审批时限），不能申请进京业务。

            if (RadDatePickerValidEndDate.SelectedDate.Value < Convert.ToDateTime(DateTime.Now.AddDays(15).ToString("yyyy-MM-dd")))
            {
                UIHelp.layerAlert(Page, "您的证书有效期低于15天（办理进京业务审批时限），不能申请进京业务。", 5, 0);
                return;
            }

            if (RadTextBoxWorkerCertificateCode.Text.Trim().Length != 18)
            {
                UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
                return;
            }
            else if (Utility.Check.isChinaIDCard(RadTextBoxWorkerCertificateCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                return;               
            }
            else
            {
                if (PostSelect1.PostTypeID == "1")
                {
                    if (PostSelect1.PostID == "147")//A本
                    {
                        if (IfFaRen == false)//非法人
                        {
                            //非企业法人，受年龄60限制
                            if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(PostSelect1.PostID), RadTextBoxWorkerCertificateCode.Text.Trim()) == true)
                            {
                                UIHelp.layerAlert(Page, "您已超龄,不予进京！");
                                return;
                            }
                        }
                        else //法人A不限制年龄
                        {
                        }
                    }
                    else//B、C1、C2、C3
                    {
                        if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(PostSelect1.PostID), RadTextBoxWorkerCertificateCode.Text.Trim()) == true)
                        {
                            UIHelp.layerAlert(Page, "您已超龄,不予进京！");
                            return;
                        }
                    }                  
                }
                else
                {
                    if (Utility.Check.CheckBirthdayLimit(Convert.ToInt32(PostSelect1.PostTypeID), RadTextBoxWorkerCertificateCode.Text.Trim()) == true)
                    {
                        UIHelp.layerAlert(Page, "您已超龄，不予进京！");
                        return;
                    }
                }
            }

            if (Utility.Check.CheckUnitCodeOrCreditCode(RadTextBoxCreditCode.Text.Trim()) == false)
            {
                UIHelp.layerAlert(Page, "“机构代码”格式不正确！请使组织机构代码（9位数字或大写字母组，其中不能带有“-”横杠）或18位社会统一信用代码");
                return;
            }
            if (PostSelect1.PostID == "")
            {
                UIHelp.layerAlert(Page, "请选择一个岗位工种！");
                return;
            }
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
            if (RadUploadFacePhoto.UploadedFiles.Count == 0)//照片
            {
                if (!File.Exists(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", RadTextBoxWorkerCertificateCode.Text.Trim().Substring(RadTextBoxWorkerCertificateCode.Text.Trim().Length - 3, 3), RadTextBoxWorkerCertificateCode.Text.Trim()))))
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }
            }

            string addPostID = "";//增项岗位ID
            if (PostSelect1.PostID == "9")//土建
                addPostID = "12";//增安装
            else if (PostSelect1.PostID == "12")//=12 安装
                addPostID = "9";//增土建";

            CertificateEnterApplyOB _CertificateEnterApplyOB = ViewState["CertificateEnterApplyOB"] == null ? new CertificateEnterApplyOB() : (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];

            if (ViewState["CertificateEnterApplyOB"] == null)//new
            {
                //检查证书号是否已经提交过进京申请
                if (CertificateEnterApplyDAL.SelectCount(string.Format(" and CertificateCode='{0}'", RadTextBoxCertificateCode.Text)) > 0)
                {
                    UIHelp.layerAlert(Page, "该证书号码已经提交过进京申请了，不能重复使用！");
                    return;
                }
                //检查是否已经提交过相同岗位进京申请
                if (CertificateEnterApplyDAL.SelectCount(string.Format(" and WorkerCertificateCode='{0}' and (PostID={1} or AddPostID='{1}') and UnitCode like '{2}' and ApplyStatus <>'{3}'"
                    , RadTextBoxWorkerCertificateCode.Text
                    , PostSelect1.PostID
                    , (PostSelect1.PostID == "147") ? UnitCode : "%"//企业负责人允许多个证书（不同企业）
                    ,EnumManager.CertificateEnterStatus.Decided)) > 0)
                {
                    UIHelp.layerAlert(Page, "您已经提交过相同的岗位工种的进京申请了，不能重复申请！");
                    return;
                }
                //if (PostSelect1.PostTypeID == "3" && CheckBoxAddItem.Checked == true)//造价员，已有申请，不能重复增项
                //{
                //    if (CertificateEnterApplyDAL.SelectCount(string.Format(" and WorkerCertificateCode='{0}' and (PostID={1} or AddPostID='{1}') and ApplyStatus <>'{2}'"
                //    , RadTextBoxWorkerCertificateCode.Text
                //    , addPostID
                //    , EnumManager.CertificateEnterStatus.Decided)) > 0)
                //    {
                //        UIHelp.layerAlert(Page, string.Format("已有造价员{0}进京申请，不能再次申请增项！", addPostID == "12" ? "安装" : "土建"));
                //        return;
                //    }
                //}
            }
            else//update
            {
                //检查证书号是否已经提交过进京申请
                if (_CertificateEnterApplyOB.CertificateCode != RadTextBoxCertificateCode.Text && CertificateEnterApplyDAL.SelectCount(string.Format(" and CertificateCode='{0}'", RadTextBoxCertificateCode.Text)) > 0)
                {
                    UIHelp.layerAlert(Page, "该证书号码已经提交过进京申请了，不能重复使用！");
                    return;
                }
                //检查是否已经提交过相同岗位进京申请
                if (_CertificateEnterApplyOB.PostID.ToString() != PostSelect1.PostID && CertificateEnterApplyDAL.SelectCount(string.Format(" and WorkerCertificateCode='{0}' and PostID={1} and UnitCode like '{2}' and ApplyStatus <>'{3}'"
                    , RadTextBoxWorkerCertificateCode.Text
                    , PostSelect1.PostID
                    , (PostSelect1.PostID == "147") ? UnitCode : "%"//企业负责人允许多个证书（不同企业）
                    , EnumManager.CertificateEnterStatus.Decided)) > 0)
                {
                    UIHelp.layerAlert(Page, "您已经提交过相同的岗位工种的进京申请了，不能重复申请！");
                    return;
                }
            }
            //检查是否存在同一有效的岗位证书
            CertificateOB certificateob = CertificateDAL.GetCertificateOBObject(RadTextBoxWorkerCertificateCode.Text.Trim(), Convert.ToInt32(PostSelect1.PostID), DateTime.Now);
            if (certificateob != null )
            {
                if (PostSelect1.PostID == "147")//是企业主要负责人证书
                {
                    if (CertificateDAL.CheckRegular_UnitMaster(certificateob, UnitCode) == false)
                    {
                        UIHelp.layerAlert(Page, "不能在同一单位取得多个“企业主要负责人”证书");
                        return;
                    }                   
                }
                else
                {
                    UIHelp.layerAlert(Page, "已有相同岗位证书，不能创建！");
                    return;
                }
            }
            //if (PostSelect1.PostTypeID == "3" && CheckBoxAddItem.Checked == true)//造价员,已有证书不能重复增项
            //{
            //    string pid = "";
            //    if (PostSelect1.PostID == "9")//土建
            //        pid = "12";//增安装
            //    else//=12 安装
            //        pid = "9";//增土建";

            //    CertificateOB zjy = CertificateDAL.GetCertificateOBObject(RadTextBoxWorkerCertificateCode.Text.Trim(), Convert.ToInt32(pid), DateTime.Now);
            //    if (zjy != null)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("已有造价员{0}证书，不能申请增项！", pid == "12" ? "安装" : "土建"));
            //        return;
            //    }
            //}
            //检验：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位
            if (CertificateDAL.CheckRegular_ItemMaster(RadTextBoxWorkerCertificateCode.Text.Trim(), UnitCode, Convert.ToInt32(PostSelect1.PostID)) == false)
            {
                UIHelp.layerAlert(Page, "只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员”证书！");
                return;
            }

            //项目负责人（B本），身份证一致且新单位组织机构代码与建造师证书一致的允许进京。
            //int countZCJZS = 0;
            if (PostSelect1.PostID == "148")
            {

                bool isJZS = UIHelp.CheckJZS("全部", RadTextBoxWorkerCertificateCode.Text.Trim(), UnitCode);
                if (isJZS == false)
                {
                    UIHelp.layerAlert(Page, @"不满足变更要求,可能原因：<br />1、B本新单位信息与建造师当前注册信息的身份证号码、组织机构代码不一致，请先变更建造师注册信息。<br />2、没有与B本对应的建造师注册信息，不允许进京。");
                    return;
                }
            }

            //三类人员企业资质检查
            if (PostSelect1.PostTypeID == "1")
            {

                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(UnitCode, true);
                if (string.IsNullOrEmpty(UnitName))
                {
                    UIHelp.layerAlert(Page, string.Format("现聘用单位“{0}”（机构代码：{1}）无建筑施工企业资质证书，不允许进京。", RadTextBoxUnitName.Text.Trim(), RadTextBoxCreditCode.Text.Trim()));
                    return;
                }
                if (UnitName != RadTextBoxUnitName.Text.Trim())
                {
                    UIHelp.layerAlert(Page, string.Format("机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", RadTextBoxCreditCode.Text.Trim(), UnitName));
                    return;
                }
            }

            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(UnitCode);//组织机构代码  
            if (_UnitMDL!=null && (_UnitMDL.ResultGSXX == 0 || _UnitMDL.ResultGSXX == 1))
            {
                UIHelp.layerAlert(Page, string.Format("企业“{0}”没有通过工商信息验证，无法办理业务！", _UnitMDL.ENT_Name));
                return;
            }

            #endregion 校验

            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();

            WorkerOB workerob = WorkerDAL.GetUserObject(RadTextBoxWorkerCertificateCode.Text.ToString());   //根据证件号码得到用户
           

            try
            {

                //#region 向从业人员表插入/修改数据
                //if (workerob == null) workerob = new WorkerOB();
                //workerob.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别
                //workerob.Birthday = RadDatePickerBirthday.SelectedDate.Value;  //出身日期
                //workerob.CertificateCode = RadTextBoxWorkerCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                //workerob.Phone = RadTextBoxPhone.Text.Trim();  //联系电话
                //workerob.WorkerName = RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", "");     //姓名
                //workerob.CertificateType = "身份证";   //证件类别
                //if (workerob.WorkerID.HasValue == false)//new
                //{
                //    WorkerDAL.Insert(dtr, workerob);
                //}

                //#endregion

                #region 向机构表插入数据
                if (_UnitMDL == null)
                {
                    _UnitMDL = new UnitMDL();

                    //企业资质
                    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(UnitCode);

                    if (_jcsjk_QY_ZHXXMDL != null)//有资质
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                        if (RadTextBoxCreditCode.Text.Trim().Length == 9)
                        {
                            _UnitMDL.ENT_OrganizationsCode = RadTextBoxCreditCode.Text.ToString();//组织机构代码
                            _UnitMDL.CreditCode = RadTextBoxCreditCode.Text.Trim();    //统一社会信用代码
                     
                        }
                        else
                        {
                            _UnitMDL.ENT_OrganizationsCode = RadTextBoxCreditCode.Text.Trim().Substring(8, 9);    //机构号码
                            _UnitMDL.CreditCode = RadTextBoxCreditCode.Text.Trim();    //统一社会信用代码
                        }
                       
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
                        _UnitMDL.CreditCode = "";//社会统一信用代码
                        _UnitMDL.Valid = 1;//是否有效
                        _UnitMDL.ResultGSXX = 0;
                    }
                    else//无资质设置为新设立企业
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = DateTime.Now;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = RadTextBoxUnitName.Text.Trim();//企业名称
                        _UnitMDL.ENT_OrganizationsCode = RadTextBoxUnitName.Text.Trim();//组织机构代码
                        _UnitMDL.ENT_Economic_Nature = "";//企业类型
                        _UnitMDL.ENT_City = "";//区县
                        _UnitMDL.END_Addess = "";//注册地址
                        _UnitMDL.ENT_Corporate = "";//法定代表人
                        //_UnitMDL.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅无企业通讯地址企业自行维护
                        _UnitMDL.ENT_Type = "";  //企业类型
                        _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                        _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                        _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号
                        _UnitMDL.CreditCode = "";//社会统一信用代码
                        _UnitMDL.Valid = 1;//是否有效
                        _UnitMDL.Memo = "新设立企业";
                        _UnitMDL.CJSJ = DateTime.Now;
                        _UnitMDL.ResultGSXX = 0;
                    }

                    UnitDAL.Insert(dtr, _UnitMDL);
                }

                #endregion

                #region 向进京申请表插入数据                

                _CertificateEnterApplyOB.WorkerID = workerob.WorkerID;//从业人员ID                              
                _CertificateEnterApplyOB.WorkerName = RadTextBoxWorkerName.Text.Replace(" ", "").Replace("　", ""); //姓名
                _CertificateEnterApplyOB.WorkerCertificateCode = RadTextBoxWorkerCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                _CertificateEnterApplyOB.Birthday = RadDatePickerBirthday.SelectedDate.Value;//生日
                _CertificateEnterApplyOB.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别

                _CertificateEnterApplyOB.OldUnitName = RadTextBoxOldUnitName.Text.Trim();    //原单位名称
                _CertificateEnterApplyOB.UnitName = Utility.Check.removeInputErrorChares( RadTextBoxUnitName.Text);    //现单位名称
                if (RadTextBoxCreditCode.Text.Trim().Length == 9)
                {
                    _CertificateEnterApplyOB.UnitCode = RadTextBoxCreditCode.Text.Trim();    //机构号码
                    _CertificateEnterApplyOB.CreditCode = RadTextBoxCreditCode.Text.Trim();    //统一社会信用代码
                }
                else
                {
                    _CertificateEnterApplyOB.UnitCode = RadTextBoxCreditCode.Text.Trim().Substring(8,9);    //机构号码
                    _CertificateEnterApplyOB.CreditCode = RadTextBoxCreditCode.Text.Trim();    //统一社会信用代码
                }
                _CertificateEnterApplyOB.Phone = RadTextBoxPhone.Text.Trim();//联系电话

                _CertificateEnterApplyOB.CertificateCode = RadTextBoxCertificateCode.Text.Trim();  //证书编号
                _CertificateEnterApplyOB.PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);   //岗位类别id
                _CertificateEnterApplyOB.PostID = Convert.ToInt32(PostSelect1.PostID);   //岗位工种id
                _CertificateEnterApplyOB.ConferDate = RadDatePickerConferDate.SelectedDate.Value;//发证日期
                _CertificateEnterApplyOB.ConferUnit = RadTextBoxConferUnit.Text.Trim();//发证机关
                _CertificateEnterApplyOB.ValidStartDate = RadDatePickerValidStartDate.SelectedDate.Value;//有效期
                _CertificateEnterApplyOB.ValidEndDate = RadDatePickerValidEndDate.SelectedDate.Value;
                _CertificateEnterApplyOB.ApplyStatus = EnumManager.CertificateEnterStatus.NewSave;  //状态
                if (RadComboBoxJob.SelectedItem.Value != "") _CertificateEnterApplyOB.Job = RadComboBoxJob.SelectedItem.Text;//职务
                if (RadComboBoxSKILLLEVEL.SelectedItem.Value != "") _CertificateEnterApplyOB.SkillLevel = RadComboBoxSKILLLEVEL.SelectedItem.Text;//技术职称


                if (ViewState["CertificateEnterApplyOB"] == null)//new
                {
                    _CertificateEnterApplyOB.ApplyCode = UIHelp.GetNextBatchNumber("JJSQ");  //批次号：进京申请
                    _CertificateEnterApplyOB.ApplyDate = DateTime.Now;   //申请日期
                    _CertificateEnterApplyOB.ApplyMan = PersonName;//申请人
                    _CertificateEnterApplyOB.CreatePersonID = PersonID;//创建人ID
                    _CertificateEnterApplyOB.CreateTime = DateTime.Now;//创建时间

                    _CertificateEnterApplyOB.ModifyTime = _CertificateEnterApplyOB.CreateTime;
                    _CertificateEnterApplyOB.ModifyPersonID = PersonID;
                    

                    CertificateEnterApplyDAL.Insert(dtr, _CertificateEnterApplyOB);
                   
                   
                    LabelApplyCode.Text = _CertificateEnterApplyOB.ApplyCode; //批次号
                    LabelApplyDate.Text = _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd");

                    //保存后才能导出和打印
                    ButtonExport.Enabled = true;
                    ButtonDelete.Visible = true;

                    //保存后不能修改证件号码
                    RadTextBoxWorkerCertificateCode.Enabled = false;
                }
                else//update
                {
                    _CertificateEnterApplyOB.NewUnitAdvise = "";//现单位意见
                    _CertificateEnterApplyOB.NewUnitCheckTime = null;

                    _CertificateEnterApplyOB.AcceptDate = null;   //受理时间
                    _CertificateEnterApplyOB.GetResult = null;     //受理结论
                    _CertificateEnterApplyOB.GetMan = null;    //受理人
                    _CertificateEnterApplyOB.GetCode = null;//受理编批号

                    _CertificateEnterApplyOB.CheckDate = null;
                    _CertificateEnterApplyOB.CheckResult = null;
                    _CertificateEnterApplyOB.CheckMan = null;
                    _CertificateEnterApplyOB.CheckCode = null;

                    _CertificateEnterApplyOB.ConfrimDate = null;
                    _CertificateEnterApplyOB.ConfrimResult = null;
                    _CertificateEnterApplyOB.ConfrimMan = null;
                    _CertificateEnterApplyOB.ConfrimCode = null;

                    _CertificateEnterApplyOB.ModifyTime = DateTime.Now; 
                    _CertificateEnterApplyOB.ModifyPersonID = PersonID;

                    CertificateEnterApplyDAL.Update(dtr, _CertificateEnterApplyOB);
                   
                }
                
                #endregion

                #region 上传个人照片
                //个人照片存放路径(按证件号码后3位)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = _CertificateEnterApplyOB.WorkerCertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, _CertificateEnterApplyOB.WorkerCertificateCode + ".jpg"), true);
                        break;
                    }
                }

                //绑定照片
                //ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_CertificateEnterApplyOB.WorkerCertificateCode)));
                ImgCode.Src = UIHelp.ShowFaceImage(_CertificateEnterApplyOB.WorkerCertificateCode);
                #endregion

                dtr.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "申请证书进京", string.Format("证书编号：{0}。"
            , _CertificateEnterApplyOB.CertificateCode));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "进京申请失败！", ex);
                return;
            }
            ViewState["CertificateEnterApplyOB"] = _CertificateEnterApplyOB;
            SetButtonEnable(_CertificateEnterApplyOB.ApplyStatus);
            SetUploadFileType();
            PostSelect1.Enabled = false;

            UIHelp.layerAlert(Page, string.Format("保存成功！<br />请打印申请表，加盖单位公章后扫描上传，提交现单位审核！{0}",
                _CertificateEnterApplyOB.ValidEndDate.Value.AddMonths(-3) < DateTime.Now ? "<br /><br /><b>特别提示：您的证书有效期不足90天，进京业务完成后，请立即联系初审单位办理证书续期。逾期未办理的，证书自动失效。</b>" : ""
                ));
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //提交单位审核、取消申请
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            if (RadDatePickerValidEndDate.SelectedDate.Value < Convert.ToDateTime(DateTime.Now.AddDays(10).ToString("yyyy-MM-dd")))
            {
                UIHelp.layerAlert(Page, "您的证书有效期低于10个工作日（办理进京业务审批时限），不能申请进京业务。", 5, 0);
                return;
            }

             CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];
             if (ButtonExit.Text == "取消申报"//取消申报
                || (PostSelect1.PostID == "147" && IfFaRen == true)//法人进京A
                || _CertificateEnterApplyOB.SheBaoCheck.HasValue == false//申报尚未校验
                || _CertificateEnterApplyOB.SheBaoCheck == 1//社保校验合格
                || (ViewState["继续提交"] != null && Convert.ToBoolean(ViewState["继续提交"]) == true)//已读8表提示并继续提交
                )
             {
                 #region  提交

                 _CertificateEnterApplyOB.NewUnitAdvise = "";//现单位意见
                 _CertificateEnterApplyOB.NewUnitCheckTime = null;

                 _CertificateEnterApplyOB.AcceptDate = null;   //受理时间
                 _CertificateEnterApplyOB.GetResult = null;     //受理结论
                 _CertificateEnterApplyOB.GetMan = null;    //受理人
                 _CertificateEnterApplyOB.GetCode = null;//受理编批号

                 _CertificateEnterApplyOB.CheckDate = null;
                 _CertificateEnterApplyOB.CheckResult = null;
                 _CertificateEnterApplyOB.CheckMan = null;
                 _CertificateEnterApplyOB.CheckCode = null;

                 _CertificateEnterApplyOB.ConfrimDate = null;
                 _CertificateEnterApplyOB.ConfrimResult = null;
                 _CertificateEnterApplyOB.ConfrimMan = null;
                 _CertificateEnterApplyOB.ConfrimCode = null;

                 _CertificateEnterApplyOB.ZACheckTime = null;
                 _CertificateEnterApplyOB.ZACheckResult = null;
                 _CertificateEnterApplyOB.ZACheckRemark = null;

                 if (ButtonExit.Text == "提交单位审核")
                 {
                     #region 必须上传附件集合

                     System.Collections.Hashtable fj = null;//必须上传附件集合
                     System.Collections.Hashtable orFj = new System.Collections.Hashtable { };//多选一附件集合

                     fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.变更申请表扫描件,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0}                        
                    };


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

                     if (PostSelect1.PostID == "147"//企业主要负责人
                             && RadComboBoxJob.SelectedItem.Text == "法定代表人")
                     {

                         if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.企业营业执照扫描件) == false)
                         {
                             sb.Append(string.Format("、“{0}”", EnumManager.FileDataTypeName.企业营业执照扫描件));
                         }
                     }
                     else
                     {

                         if (ApplyFileDAL.CheckFileUpload(dt, EnumManager.FileDataTypeName.社保扫描件) == false//没上传社保证明

                        && (_CertificateEnterApplyOB.SheBaoCheck.HasValue == false || _CertificateEnterApplyOB.SheBaoCheck.Value == 0)//没有比对社保或社保比对失败
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

                     #endregion 必须上传附件集合

                     try
                     {
                         _CertificateEnterApplyOB.ApplyDate = DateTime.Now;
                         _CertificateEnterApplyOB.ApplyStatus = EnumManager.CertificateEnterStatus.WaitUnitCheck;
                         CertificateEnterApplyDAL.Update(_CertificateEnterApplyOB);
                     }
                     catch (Exception ex)
                     {
                         UIHelp.WriteErrorLog(Page, "证书进京申请提交单位审核失败！", ex);
                         return;
                     }

                     UIHelp.WriteOperateLog(PersonName, UserID, "证书进京申请提交单位审核", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));

                     UIHelp.layerAlert(Page, string.Format("提交现单位审核成功，请您立即联系所在企业网上确认。{0}", _CertificateEnterApplyOB.ValidEndDate.Value.AddMonths(-3) < DateTime.Now ? "<br /><br /><b>特别提示：您的证书有效期不足90天，进京业务完成后，请立即联系初审单位办理证书续期。逾期未办理的，证书自动失效。</b>" : "")
                         , string.Format(@"var isfresh=true;window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("JinJing"), Utility.Cryptography.Encrypt(_CertificateEnterApplyOB.ApplyID.ToString())));
                     ViewState["继续提交"] = null;
                 }
                 else//取消申请
                 {
                     try
                     {
                         _CertificateEnterApplyOB.ModifyTime = DateTime.Now;
                         _CertificateEnterApplyOB.ModifyPersonID = PersonID;
                         _CertificateEnterApplyOB.ApplyStatus = EnumManager.CertificateEnterStatus.NewSave;
                         CertificateEnterApplyDAL.Update(_CertificateEnterApplyOB);
                     }
                     catch (Exception ex)
                     {
                         UIHelp.WriteErrorLog(Page, "取消证书进京申请失败！", ex);
                         return;
                     }
                     UIHelp.WriteOperateLog(PersonName, UserID, "取消证书进京申请", string.Format("证书编号：{0}。", RadTextBoxCertificateCode.Text));
                     UIHelp.layerAlert(Page, "取消成功！", 6, 3000);
                 }

                 ViewState["CertificateEnterApplyOB"] = _CertificateEnterApplyOB;
                 SetButtonEnable(_CertificateEnterApplyOB.ApplyStatus);
                 SetStep(_CertificateEnterApplyOB.ApplyStatus);
                 BindCheckHistory(_CertificateEnterApplyOB.ApplyID.Value);
                 BindFile(ApplyID);
                 ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);

                 ViewState["继续提交"] = null;
                 #endregion  提交
             }
             else
             {
                 #region  弹出提示，8秒阅读
                 string TipHtml = null;

                 if (PostSelect1.PostID == "6"//土建类专职安全生产管理人员
                     || PostSelect1.PostID == "1123"//机械类专职安全生产管理人员
                     || PostSelect1.PostID == "1125")//综合类专职安全生产管理人员
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
        show15();window.setTimeout(function(){{$('#{2}').focus();}},500);", ButtonYes.ClientID, ButtonNo.ClientID, ButtonExit.ClientID)
                     , true);

                 #endregion  弹出提示，8秒阅读
             }
        }

        //继续提交
        protected void ButtonYes_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
            ViewState["继续提交"] = true;
            if (TableUnitCheck.Visible == true)
            {
                ButtonUnitCheck_Click(sender, e);
            }
            else
            {
                ButtonExit_Click(sender, e);
            }
        }

        //取消提交
        protected void ButtonNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];
            if (_CertificateEnterApplyOB.ApplyStatus != EnumManager.CertificateEnterStatus.Applyed&&
                _CertificateEnterApplyOB.ApplyStatus != EnumManager.CertificateEnterStatus.NewSave&&
                _CertificateEnterApplyOB.ApplyStatus != EnumManager.CertificateEnterStatus.WaitUnitCheck&&
                _CertificateEnterApplyOB.ApplyStatus != EnumManager.CertificateEnterStatus.SendBack
                )
            {
                UIHelp.layerAlert(Page, "申请已经受理了，不能删除！");
                return;
            }

            try
            {
                CertificateEnterApplyDAL.Delete(_CertificateEnterApplyOB);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除进京申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "删除证书进京申请", string.Format("证书编号：{0}。"
            , _CertificateEnterApplyOB.CertificateCode));

            ViewState["CertificateEnterApplyOB"] = null;
            SetButtonEnable("");
            SetStep("");
            BindFile(ApplyID);
            UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //导出打印
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //CheckSaveDirectory();
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/证书进京申请表.doc", string.Format("~/UpLoad/CertifEnterApply/证书进京申请表_{0}.doc", RadTextBoxCertificateCode.Text.Trim()), GetExportData());
            ////Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifEnterApply/证书进京申请表_{0}.doc", RadTextBoxCertificateCode.Text.Trim())));

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl("证书进京申请表", string.Format("~/UpLoad/CertifEnterApply/证书进京申请表_{0}.doc", RadTextBoxCertificateCode.Text.Trim())));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

            CheckSaveDirectory();
            //var ht = PrintDocument.GetProperties((CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"]);
            PrintDocument.CreateDataToWordByHashtable(Server.MapPath("~/Template/证书进京申请表.docx"), string.Format("证书进京申请表_{0}", RadTextBoxCertificateCode.Text.Trim()), GetHashData());
        }
        
        //企业审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            if (RadDatePickerValidEndDate.SelectedDate.Value < Convert.ToDateTime(DateTime.Now.AddDays(10).ToString("yyyy-MM-dd")))
            {
                UIHelp.layerAlert(Page, "证书有效期低于10个工作日（办理进京业务审批时限），不能申请进京业务。", 5, 0);
                return;
            }

            CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];

            if ((PostSelect1.PostID == "147" && IfFaRen == true)//法人进京A
               || _CertificateEnterApplyOB.SheBaoCheck.HasValue == false//申报尚未校验
               || _CertificateEnterApplyOB.SheBaoCheck == 1//社保校验合格
               || (ViewState["继续提交"] != null && Convert.ToBoolean(ViewState["继续提交"]) == true)//已读8表提示并继续提交
               || RadioButtonListOldUnitCheckResult.SelectedValue == "不同意"
               )
            {

                if (_CertificateEnterApplyOB.ZACheckResult.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "尚未进行数据校验，请等待系统校验数据后再提交申请。");
                    return;
                }
                if (_CertificateEnterApplyOB.ZACheckResult == 0)
                {
                    UIHelp.layerAlert(Page, string.Format("校验未通过，无法提交申请。原因：{0}", _CertificateEnterApplyOB.ZACheckRemark));
                    return;
                }
                try
                {
                    _CertificateEnterApplyOB.NewUnitCheckTime = DateTime.Now;
                    _CertificateEnterApplyOB.NewUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
                    _CertificateEnterApplyOB.ApplyStatus = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.CertificateEnterStatus.Applyed : EnumManager.CertificateEnterStatus.SendBack);
                    CertificateEnterApplyDAL.Update(_CertificateEnterApplyOB);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "单位审核证书进京申请失败！", ex);
                    return;
                }
                SetStep(_CertificateEnterApplyOB.ApplyStatus);

                UIHelp.WriteOperateLog(PersonName, UserID, "单位审核证书进京申请", string.Format("证书编号：{0}，岗位工种：{1}，状态：{2}，意见：{3}"
                    , RadTextBoxCertificateCode.Text, PostSelect1.PostName, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.NewUnitAdvise));

                ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);

                ViewState["继续提交"] = null;
            }
            else
            {
                #region  弹出提示，8秒阅读
                string TipHtml = null;

                if (PostSelect1.PostID == "6"//土建类专职安全生产管理人员
                    || PostSelect1.PostID == "1123"//机械类专职安全生产管理人员
                    || PostSelect1.PostID == "1125")//综合类专职安全生产管理人员
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
        show15();window.setTimeout(function(){{$('#{2}').focus();}},500);", ButtonYes.ClientID, ButtonNo.ClientID, ButtonUnitCheck.ClientID)
                    , true);

                #endregion  弹出提示，8秒阅读
            }
        }

        //建委受理
        protected void ButtonAccept_Click(object sender, EventArgs e)
        {
            CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];

            ////启用质安网检查后取消注释
            //if (RadioButtonListJWAccept.SelectedValue == "通过")
            //{
            //    if (_CertificateEnterApplyOB.ZACheckResult.HasValue == false)
            //    {
            //        UIHelp.layerAlert(Page, "数据校验尚未比对，无法审核！");
            //        return;
            //    }
            //    else if (_CertificateEnterApplyOB.ZACheckResult.Value == 0)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("数据校验结果：{0}", _CertificateEnterApplyOB.ZACheckRemark));
            //        return;
            //    }
            //}
            try
            {
                string _GetCode = UIHelp.GetNextBatchNumber("JJSL"); //进京受理编批号                
                _CertificateEnterApplyOB.AcceptDate = DateTime.Now;   //受理时间
                _CertificateEnterApplyOB.GetResult = (RadioButtonListJWAccept.SelectedValue == "通过" ? "通过" : TextBoxGetResult.Text);  //受理结论
                _CertificateEnterApplyOB.GetMan = PersonName;    //受理人
                _CertificateEnterApplyOB.GetCode = _GetCode;//受理编批号
                _CertificateEnterApplyOB.ApplyStatus = (RadioButtonListJWAccept.SelectedValue == "通过" ? EnumManager.CertificateEnterStatus.Accepted : EnumManager.CertificateEnterStatus.SendBack);//状态
                CertificateEnterApplyDAL.Update(_CertificateEnterApplyOB);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "建委受理证书进京失败！", ex);
                return;
            }
            SetStep(_CertificateEnterApplyOB.ApplyStatus);

            UIHelp.WriteOperateLog(PersonName, UserID, "受理证书进京申请", string.Format("证书编号：{0}，岗位工种：{1}，状态：{2}，意见：{3}"
                , RadTextBoxCertificateCode.Text, PostSelect1.PostName, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.GetResult));

            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);

        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];
            try
            {
                string _CheckCode = UIHelp.GetNextBatchNumber("JJSH"); //进京审核编批号               
                _CertificateEnterApplyOB.CheckDate = DateTime.Now;   //审核时间
                _CertificateEnterApplyOB.CheckResult = (RadioButtonListJWCheck.SelectedValue == "通过" ? "通过" : TextBoxCheckResult.Text);  //审核结论
                _CertificateEnterApplyOB.CheckMan = PersonName;    //受审核人
                _CertificateEnterApplyOB.CheckCode = _CheckCode;//审核编批号
                _CertificateEnterApplyOB.ApplyStatus = (RadioButtonListJWCheck.SelectedValue == "通过" ? EnumManager.CertificateEnterStatus.Checked : EnumManager.CertificateEnterStatus.SendBack);//状态
                CertificateEnterApplyDAL.Update(_CertificateEnterApplyOB);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "建委审核证书进京申请失败！", ex);
                return;
            }
            SetStep(_CertificateEnterApplyOB.ApplyStatus);

            UIHelp.WriteOperateLog(PersonName, UserID, "建委审核证书进京申请", string.Format("证书编号：{0}，岗位工种：{1}，状态：{2}，意见：{3}"
                , RadTextBoxCertificateCode.Text, PostSelect1.PostName, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.CheckResult));

            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
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
                UIHelp.WriteErrorLog(Page, "删除进京申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除进京申请表附件成功", string.Format("证书编号：{0}，文件名称：{1}。", RadTextBoxCertificateCode.Text, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileName"]));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ApplyId)
        {
            DataTable dt = CertificateEnterApplyDAL.GetCheckHistoryList(ApplyId);
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();

        }

        //操作按钮控制
        /// <summary>
        /// 操作按钮控制
        /// </summary>
        /// <param name="ApplyStatus"></param>
        protected void SetButtonEnable(string ApplyStatus)
        {
            trFuJanTitel.Visible = false;
            trFuJan.Visible = false;
            ButtonSave.Enabled = false;//保 存
            ButtonExport.Enabled = false;//导出打印
            ButtonExit.Enabled = false;//取消申报 
            ButtonDelete.Enabled = false;//删除
            divSelectUnit.Style.Add("display", "none");
            SpanSelectCert.Style.Add("display", "none");
            switch (ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//提交单位审核 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "提交单位审核";
                    divSelectUnit.Style.Add("display", "inline");
                    SpanSelectCert.Style.Add("display", "inline");
                    
                    tr_upPhotoTitle.Visible = true;
                    tr_upPhoto.Visible = true;
                    break;
                case EnumManager.CertificateEnterStatus.NewSave:
                    ButtonSave.Enabled = true;//保 存
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位审核 
                    ButtonDelete.Enabled = true;//删除
                    ButtonExit.Text = "提交单位审核";
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        divSelectUnit.Style.Add("display", "inline");
                        SpanSelectCert.Style.Add("display", "inline");
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        tr_upPhotoTitle.Visible = true;
                        tr_upPhoto.Visible = true;
                    }
                    break;
                case EnumManager.CertificateEnterStatus.WaitUnitCheck:
                    ButtonSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = false;//删除
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = true;//取消申报 
                    ButtonExit.Text = "取消申报";
                    if (IfExistRoleID("2") == true)
                    {
                        TableUnitCheck.Visible = true;
                        UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                    }
                    break;
                case EnumManager.CertificateEnterStatus.SendBack:
                    ButtonSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = true;//删除
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位确认  
                    ButtonExit.Text = "提交单位审核";

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        divSelectUnit.Style.Add("display", "inline");
                        SpanSelectCert.Style.Add("display", "inline");
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        tr_upPhotoTitle.Visible = true;
                        tr_upPhoto.Visible = true;
                    }

                    break;
                case EnumManager.CertificateEnterStatus.Applyed://已申请
                    if (ValidPageViewLimit(RoleIDs, "CertifEnterAccepted.aspx") == true)//受理权限
                    {
                        TableJWAccept.Visible = true;
                        UIHelp.SetReadOnly(TextBoxGetResult, false);

                    }
                    ButtonExit.Text = "取消申报";
                    ButtonExit.Enabled = true;//取消申报 
                    break;
                case EnumManager.CertificateEnterStatus.Accepted://已受理
                    if (ValidPageViewLimit(RoleIDs, "CertifEnterCheck.aspx") == true)//审核权限
                    {
                        TableJWCheck.Visible = true;
                        UIHelp.SetReadOnly(TextBoxCheckResult, false);
                    }
                    break;
                case EnumManager.CertificateEnterStatus.Checked://已审核

                    break;
                case EnumManager.CertificateEnterStatus.Decided://已编号

                    break;
                default:
                    ButtonSave.Enabled = false;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//取消申报 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "取消申报";
                    break;
            }

            //个人登录后
            if (IfExistRoleID("0") == true
                && (ApplyStatus == ""
                || ApplyStatus == EnumManager.CertificateEnterStatus.NewSave
                || ApplyStatus == EnumManager.CertificateEnterStatus.WaitUnitCheck
                || ApplyStatus == EnumManager.CertificateEnterStatus.SendBack
                || ApplyStatus == EnumManager.CertificateEnterStatus.Applyed))
            {
                divWorker.Visible = true;

            }
            else
            {
                divWorker.Visible = false;
            }

            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExport.CssClass = ButtonExport.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExit.CssClass = ButtonExit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //显示社保比对结果
        private void ShowSheBao(CertificateEnterApplyOB _CertificateEnterApplyOB)
        {
            if (_CertificateEnterApplyOB.ApplyDate.Value.CompareTo(DateTime.Parse("2014-07-01")) >= 0)
            {
                divSheBao.InnerHtml = string.Format("<b>社保校验：</b><span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>"
                    , _CertificateEnterApplyOB.WorkerCertificateCode, _CertificateEnterApplyOB.UnitCode, _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd")
                    , (_CertificateEnterApplyOB.SheBaoCheck.HasValue == false ? "尚未比对（夜间比对）" : _CertificateEnterApplyOB.SheBaoCheck.Value == 1 ? "符合" : "不符合")
                    );
            }
        }
        
        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));
        }

        ////准备导出或打印标签替换数据
        //protected System.Collections.Generic.Dictionary<string, string> GetExportData()
        //{
        //    System.Collections.Generic.Dictionary<string, string> list = new Dictionary<string, string>();
        //    list.Add("ApplyDate", LabelApplyDate.Text);//申请日期            
        //    list.Add("ApplyCode", LabelApplyCode.Text);//申请批号
        //    list.Add("Birthday", RadDatePickerBirthday.SelectedDate.Value.ToString("yyyy.MM.dd"));//生日
        //    list.Add("WorkerCertificateCode", RadTextBoxWorkerCertificateCode.Text.Trim());//证件编号
        //    list.Add("WorkerName", RadTextBoxWorkerName.Text);//姓名
        //    list.Add("Sex", RadioButtonMan.Checked == true ? "男" : "女");//性别            
        //    list.Add("OldUnitName", RadTextBoxOldUnitName.Text);//原企业名称
        //    list.Add("UnitName", RadTextBoxUnitName.Text);//企业名称
        //    list.Add("UnitCode", RadTextBoxCreditCode.Text);//组织代码
        //    list.Add("Phone", RadTextBoxPhone.Text);//联系电话
        //    list.Add("PostName", PostSelect1.PostName);//岗位工种

        //    list.Add("CertificateCode", RadTextBoxCertificateCode.Text.Trim());//证书编号
        //    list.Add("ConferDate", RadDatePickerConferDate.SelectedDate.Value.ToString("yyyy.MM.dd"));//发证日期
        //    list.Add("ValidDate", string.Format("{0} - {1}", RadDatePickerValidStartDate.SelectedDate.Value.ToString("yyyy.MM.dd"), RadDatePickerValidEndDate.SelectedDate.Value.ToString("yyyy.MM.dd")));//有效期
        //    list.Add("FacePhoto", RadTextBoxCertificateCode.Text.Trim());//照片标签
        //    //list.Add("Img_FacePhoto", GetFacePhotoPath(RadTextBoxWorkerCertificateCode.Text));//绑定照片
        //    list.Add("Img_FacePhoto", ImgCode.Src);//绑定照片
        //    return list;
        //}

        protected System.Collections.Hashtable GetHashData()
        {
            var list = new System.Collections.Hashtable(); ;
            list.Add("ApplyDate", LabelApplyDate.Text);//申请日期            
            list.Add("ApplyCode", LabelApplyCode.Text);//申请批号
            list.Add("Birthday", RadDatePickerBirthday.SelectedDate.Value.ToString("yyyy.MM.dd"));//生日
            list.Add("WorkerCertificateCode", RadTextBoxWorkerCertificateCode.Text.Trim());//证件编号
            list.Add("WorkerName", RadTextBoxWorkerName.Text);//姓名
            list.Add("Sex", RadioButtonMan.Checked == true ? "男" : "女");//性别            
            list.Add("OldUnitName", RadTextBoxOldUnitName.Text);//原企业名称
            list.Add("UnitName", RadTextBoxUnitName.Text);//企业名称
            list.Add("UnitCode", RadTextBoxCreditCode.Text);//组织代码
            list.Add("Phone", RadTextBoxPhone.Text);//联系电话
            list.Add("PostName", PostSelect1.PostName);//岗位工种

            list.Add("CertificateCode", RadTextBoxCertificateCode.Text.Trim());//证书编号
            list.Add("ConferDate", RadDatePickerConferDate.SelectedDate.Value.ToString("yyyy.MM.dd"));//发证日期
            list.Add("ValidDate", string.Format("{0} - {1}", RadDatePickerValidStartDate.SelectedDate.Value.ToString("yyyy.MM.dd"), RadDatePickerValidEndDate.SelectedDate.Value.ToString("yyyy.MM.dd")));//有效期

            if (RadComboBoxJob.SelectedValue != "")
            {
                list.Add("Job", RadComboBoxJob.SelectedItem.Text);
            }
            if (RadComboBoxSKILLLEVEL.SelectedValue != "")
            {
                list.Add("SKILLLEVEL", RadComboBoxSKILLLEVEL.SelectedItem.Text);
            }

            list["photo"] = GetFacePhotoPath(RadTextBoxWorkerCertificateCode.Text.Trim());//绑定照片
            return list;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string WorkerCertificateCode)
        {
            if (WorkerCertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_填报中.Attributes["class"] = step_填报中.Attributes["class"].Replace(" green", "");
            step_待单位确认.Attributes["class"] = step_待单位确认.Attributes["class"].Replace("green", "");
            step_已申请.Attributes["class"] = step_已申请.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
            step_已编号.Attributes["class"] = step_已编号.Attributes["class"].Replace(" green", "");
            step_证书已审核.Attributes["class"] = step_证书已审核.Attributes["class"].Replace(" green", "");


            switch (ApplyStatus)
            {
                case EnumManager.CertificateEnterStatus.NewSave:
                case EnumManager.CertificateEnterStatus.SendBack:
                    step_填报中.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateEnterStatus.WaitUnitCheck:
                    step_待单位确认.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateEnterStatus.Applyed:
                    step_已申请.Attributes["class"] += " green";
                    break;             
                case EnumManager.CertificateEnterStatus.Accepted:
                    step_已受理.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateEnterStatus.Checked:
                    step_已审核.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateEnterStatus.Decided://已编号
                    try
                    {
                        CertificateEnterApplyOB _CertificateEnterApplyOB = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];
                        CertificateOB _CertificateOB = CertificateDAL.GetObject(_CertificateEnterApplyOB.CertificateID.Value);
                        if (string.IsNullOrEmpty(_CertificateOB.CertificateCAID) == false && _CertificateOB.ReturnCATime.HasValue == true)//电子证书已返回签章
                        {
                            step_证书已审核.Attributes["class"] += " green";
                        }
                        else
                        {
                            step_已编号.Attributes["class"] += " green";
                        }
                    }
                    catch{
                    }

                    break;
                default:
                    step_填报中.Attributes["class"] += " green";
                    break;
            }
        }

        /// <summary>
        /// 设置需要上传的附件
        /// </summary>
        protected void SetUploadFileType()
        {
            if(string.IsNullOrEmpty(PostSelect1.PostID)==true)
            {
                return;
            }
            int PostID = Convert.ToInt32(PostSelect1.PostID);

            if (trFuJan.Visible == false)
            {
                if (PostID == 147)
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
                ShowZACheckResult();
                return;
            }

            switch (PostID)
            {
                case 6://土建安全员                              
                case 1123://机械安全员 
                case 1125://综合安全员 
                    p_PostTyppe1_aqy.Visible = true;                   
                        div_SheBao.Visible = true;                   
                    break;
                case 147://企业主要负责人 
                    p_PostTyppe1_qyfzr.Visible = true;
                    if (IfFaRen == false)//非法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>非法人。";
                    }
                    else//法人
                    {
                        divFR.InnerHtml = "<b>法人校验：</b>是法人。";
                    }

                    string job = RadComboBoxJob.SelectedItem.Text;
                    if (job == "请选择")
                    {
                        div_SheBao.Visible = true;
                        div_YingYeZhiZhao.Visible = true;
                        p_FaRen.Visible = true;
                        p_NoFaRen.Visible = true;
                    }
                    else if (job == "法定代表人")
                    {
                           div_SheBao.Visible = false;//不用上传社保
                        div_YingYeZhiZhao.Visible = true;//显示传营业执照
                        p_FaRen.Visible = true;
                        p_NoFaRen.Visible = false;
                    }
                    else//职务选择非法人
                    {
                        div_YingYeZhiZhao.Visible = false;
                        div_SheBao.Visible = true;

                        p_FaRen.Visible = false;
                        p_NoFaRen.Visible = true;
                    }
                    break;
                case 148://项目负责人 
                    p_PostTyppe1_xmfzr.Visible = true;
                    div_SheBao.Visible = true;
                    break;
            }
            ShowZACheckResult();

        }

        /// <summary>
        /// 显示质量安全网持证规则校验结果
        /// </summary>
        protected void ShowZACheckResult()
        {
 
            if (ViewState["CertificateEnterApplyOB"] != null)
            {
                CertificateEnterApplyOB _CertifChange = (CertificateEnterApplyOB)ViewState["CertificateEnterApplyOB"];

                if (_CertifChange.ZACheckTime.HasValue == false)
                {
                    divZACheckResult.InnerHtml = "<b>持证校验：</b>尚未比对";
                }
                else if (_CertifChange.ZACheckResult == 0)
                {
                    divZACheckResult.InnerHtml = string.Format(@"<div><b>持证校验：</b><span style='color:red'>未通过。</span></div>
                <div style='padding-left: 32px'>警告：进京申请没有通过<a href='https://zlaq.mohurd.gov.cn/fwmh/bjxcjgl/fwmh/pages/default/index.html' target='_blank'>【全国工程质量安全监管信息平台（可查询）】</a>数据校验，属于违规持证。<br/>
                                                                    请先办理相关证书转出后，才能办理证书进京。（若外省证书已经注销，请联系原证书省份，查询数据是否已经同步到全国工程质量安全监管信息平台。）<br/><br/>
                                                                    <b>校验结果说明：</b><span style='color:red'>{0}</span><br/><br/>
                                                                    <b>持证规则说明：</b><br/>
                                                                    <div style='padding-left: 32px'>
                                                                        <b>A证持证要求：</b><br/> 
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人有多本A证时，多本A证在不同企业下，其中最多存在一本非法人A证，其余A证只能以法人A证的形式存在；<br/>
                                                                            > 持证人同时持有A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业；<br/>
                                                                            > 一个企业只能存在一本法人A证。<br/><br/>
                                                                        </div>
                                                                        <b>B证持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人在全国范围只允许持有一本B证；<br/>
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
                                                                  </div>", _CertifChange.ZACheckRemark);
                }
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

        //变换职务选择
        protected void RadComboBoxJob_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetUploadFileType();
        }
    }
}
