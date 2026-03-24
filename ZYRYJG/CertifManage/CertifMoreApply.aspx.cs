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

namespace ZYRYJG.CertifManage
{
    public partial class CertifMoreApply : BasePage
    {
        //protected override void OnInit(EventArgs e)
        //{
        //    PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("2").Remove();//屏蔽特种作业类别
        //    PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("3").Remove();//屏蔽特种作业类别
        //    PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("4").Remove();//屏蔽职业技能类别
        //    PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别
        //}
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifMoreApplyList.aspx|ApplyQuerySLR.aspx";
            }
        }

        /// <summary>
        /// A增发附件申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["CertificateMoreMDL"] == null ? "" : string.Format("ZF-{0}", (ViewState["CertificateMoreMDL"] as CertificateMoreMDL).ApplyID); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    CertificateMoreMDL o = CertificateMoreDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                    ViewState["CertificateMoreMDL"] = o;
                    CertificateOB _CertificateOB = CertificateDAL.GetCertificateOBObject(o.CertificateCode);
                    ViewState["CertificateOB"] = _CertificateOB;                    

                    LabelWorkerCertificateCode.Text = o.WorkerCertificateCode;
                    LabelApplyDate.Text = o.CreateTime.Value.ToString("yyyy-MM-dd");
                    LabelWorkerName.Text = o.WorkerName;
                    LabelBirthday.Text = o.Birthday.Value.ToString("yyyy-MM-dd");
                    LabelCertificateCode.Text = o.CertificateCode;
                    LabelSex.Text = o.Sex;
                    LabelUnitCode.Text = o.UnitCode;
                    LabelUnitName.Text = o.UnitName;
                    LabelValidStartDate.Text = o.ValidStartDate.Value.ToString("yyyy-MM-dd");
                    LabelValidEndDate.Text = o.ValidEndDate.Value.ToString("yyyy-MM-dd");
                    RadTextBoxPeoplePhone.Text = o.PeoplePhone;
                    UIHelp.SetReadOnly(RadTextBoxPeoplePhone, true);//不允许修改来自大厅实名制认证电话
                    RadTextBoxUnitNameMore.Text = o.UnitNameMore;
                    RadTextBoxUnitCodeMore.Text = o.UnitCodeMore;

                    RadTextBoxUnitNameMore.Enabled = false;
                    RadTextBoxUnitCodeMore.Enabled = false;

                    SetButtonEnable(o.ApplyStatus);
                    BindFile(ApplyID);
                    BindCheckHistory(o.ApplyID.Value);
                    // 根据身份证同步他的照片
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(o.WorkerCertificateCode)));

                }
                else
                {
                    LabelApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    RadTextBoxPeoplePhone.Text = _WorkerOB.Phone;
                    UIHelp.SetReadOnly(RadTextBoxPeoplePhone, true);//不允许修改来自大厅实名制认证电话
                    CertificateOB o = CertificateMoreDAL.GetCertificateA(_WorkerOB.CertificateCode);
                    BindA(o);
                    SetButtonEnable("");
                }
            }
            else
            {
                if (Request["__EVENTTARGET"] == "refreshFile")//上传或删除附件刷新列表
                {
                    BindFile(ApplyID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
                }
            }
        }

        //绑定可增发信息
        private void BindA(CertificateOB o)
        {
           
            if (o == null)
            {
                LabelWorkerName.Text = "";
                LabelBirthday.Text = "";
                LabelCertificateCode.Text = "";
                LabelSex.Text = "";
                LabelUnitCode.Text = "";
                LabelUnitName.Text = "";
                LabelValidStartDate.Text = "";
                LabelValidEndDate.Text = "";
                LabelWorkerCertificateCode.Text = "";

                ViewState["CertificateOB"] = null;
                UIHelp.layerAlert(Page, "未找到您的有效的企业主要负责人（A本）信息，无法发起增发业务！");
                return;
            }
            ViewState["CertificateOB"] = o;

            LabelWorkerCertificateCode.Text = o.WorkerCertificateCode;
            LabelWorkerName.Text = o.WorkerName;
            LabelBirthday.Text = o.Birthday.Value.ToString("yyyy-MM-dd");
            LabelCertificateCode.Text = o.CertificateCode;
            LabelSex.Text = o.Sex;
            LabelUnitCode.Text = o.UnitCode;
            LabelUnitName.Text = o.UnitName;
            LabelValidStartDate.Text = o.ValidStartDate.Value.ToString("yyyy-MM-dd");
            LabelValidEndDate.Text = o.ValidEndDate.Value.ToString("yyyy-MM-dd");

            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(o.WorkerCertificateCode);
            if (_WorkerOB != null)
            {
                RadTextBoxPeoplePhone.Text = _WorkerOB.Phone;
            }


            // 根据身份证同步他的照片
            ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(o.WorkerCertificateCode)));

        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            if (ViewState["CertificateOB"] == null)
            {
                UIHelp.layerAlert(Page, "未找到您输入人员的有效的企业主要负责人（A本）信息，无法发起增发业务！");
                return;
            }
            CertificateOB _CertificateOB = ViewState["CertificateOB"] as CertificateOB;

            #region 有效性检查

            string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCodeMore.Text.Trim(), true);

            if (string.IsNullOrEmpty(UnitName))
            {
                UIHelp.layerAlert(Page, "增发企业不在本市管理的建筑企业资质库中，无法发起增发业务，请先办理企业资质。");
                return;
            }
            if (UnitName.Replace("（", "(").Replace("）", ")") != RadTextBoxUnitNameMore.Text.Trim().Replace("（", "(").Replace("）", ")"))
            {
                UIHelp.layerAlert(Page, string.Format("组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写增发企业名称。”", RadTextBoxUnitCodeMore.Text.Trim(), UnitName));
                return;
            }

            if (CertificateDAL.IFExistFRByUnitCode(RadTextBoxUnitCodeMore.Text.Trim(), LabelWorkerName.Text) == false)
            {
                UIHelp.layerAlert(Page, "法人库中查询不到您要增发企业的法人信息，无法发起增发业务。");
                return;
            }

            if (CertificateMoreDAL.IFExistA(RadTextBoxUnitCodeMore.Text.Trim(), LabelWorkerName.Text) == true)
            {
                UIHelp.layerAlert(Page, "要增发的企业已经存在企业主要负责人（A本） ，无法发起增发业务。");
                return;
            }

            CertificateOB FROb = CertificateDAL.GetFRCertA(RadTextBoxUnitCodeMore.Text.Trim());
            if (FROb != null)
            {
                UIHelp.layerAlert(Page, string.Format("校验未通过：一个单位只能允许一人以法定代表人职务持有A证，增发单位已经存在法人A证【{0}】。", FROb.CertificateCode));
                return;
            }

            if (ViewState["CertificateMoreMDL"] == null//新增
                ||
                (ViewState["CertificateMoreMDL"] != null
                && (ViewState["CertificateMoreMDL"] as CertificateMoreMDL).UnitCodeMore != RadTextBoxUnitCodeMore.Text.Trim()
                )//更换增发企业
             )
            {
                if (CertificateMoreDAL.IFExistApply(RadTextBoxUnitCodeMore.Text.Trim()) == true)
                {
                    UIHelp.layerAlert(Page, "要增发的企业已经存在一个在途增发申请 ，无法重复发起增发业务。");
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
                if (!File.Exists(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", _CertificateOB.WorkerCertificateCode.Substring(_CertificateOB.WorkerCertificateCode.Length - 3, 3), _CertificateOB.WorkerCertificateCode))))
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }
                else
                {
                    FileInfo fi = new FileInfo(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", _CertificateOB.WorkerCertificateCode.Substring(_CertificateOB.WorkerCertificateCode.Length - 3, 3), _CertificateOB.WorkerCertificateCode)));
                    if (fi.Length < 200)
                    {
                        UIHelp.layerAlert(Page, "照片大小存在问题，请检查照片是否有效！");
                        return;
                    }
                }
            }

            #endregion
            try
            {
                #region 向A本增发表插入数据
                CertificateMoreMDL _CertificateMoreMDL = (ViewState["CertificateMoreMDL"] == null ? new CertificateMoreMDL() : ViewState["CertificateMoreMDL"] as CertificateMoreMDL);
                _CertificateMoreMDL.CertificateID = _CertificateOB.CertificateID;
                _CertificateMoreMDL.CertificateCode = _CertificateOB.CertificateCode;
                _CertificateMoreMDL.WorkerName = _CertificateOB.WorkerName; //姓名
                _CertificateMoreMDL.WorkerCertificateCode = _CertificateOB.WorkerCertificateCode;  //证件号码
                _CertificateMoreMDL.Birthday = _CertificateOB.Birthday;//生日
                _CertificateMoreMDL.Sex = _CertificateOB.Sex;   //性别
                _CertificateMoreMDL.PeoplePhone = RadTextBoxPeoplePhone.Text.Trim();//联系电话

                _CertificateMoreMDL.UnitName = _CertificateOB.UnitName;    //单位名称
                _CertificateMoreMDL.UnitNameMore = RadTextBoxUnitNameMore.Text.Trim();    //增发单位名称
                _CertificateMoreMDL.UnitCode = _CertificateOB.UnitCode;    //机构号码
                _CertificateMoreMDL.UnitCodeMore = RadTextBoxUnitCodeMore.Text.Trim();    //增发单位机构号码
                _CertificateMoreMDL.ValidStartDate = _CertificateOB.ValidStartDate;//发证日期
                _CertificateMoreMDL.ValidEndDate = _CertificateOB.ValidEndDate;// 有效期
                _CertificateMoreMDL.ValidStartDateMore = _CertificateOB.ValidStartDate; //增发证书发证日期
                _CertificateMoreMDL.ValidEndDateMore = _CertificateOB.ValidEndDate;//增发证书有效期
                _CertificateMoreMDL.NewUnitAdvise = null;
                _CertificateMoreMDL.NewUnitCheckTime = null;
                _CertificateMoreMDL.CheckAdvise = null;
                _CertificateMoreMDL.CheckDate = null;
                _CertificateMoreMDL.CheckMan = null;
                _CertificateMoreMDL.ConfirmAdvise = null;
                _CertificateMoreMDL.ConfirmDate = null;
                _CertificateMoreMDL.ConfirmMan = null;
                _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.NewSave;  //状态
                _CertificateMoreMDL.ValID = "1";

                if (ViewState["CertificateMoreMDL"] == null)
                {
                    _CertificateMoreMDL.CreateTime = DateTime.Now;   //申请日期
                    _CertificateMoreMDL.CreatePerson = PersonName;//申请人
                    CertificateMoreDAL.Insert(_CertificateMoreMDL);
                }
                else
                {
                    _CertificateMoreMDL.ModifyTime = DateTime.Now;   //申请日期
                    _CertificateMoreMDL.ModifyPerson = PersonName;//申请人
                    CertificateMoreDAL.Update(_CertificateMoreMDL);
                }

                ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;

                #endregion

                #region 上传个人照片
                //个人照片存放路径(按证件号码后3位)

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {

                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = _CertificateMoreMDL.WorkerCertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, _CertificateMoreMDL.WorkerCertificateCode + ".jpg"), true);
                        break;
                    }
                }

                //绑定照片
                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_CertificateMoreMDL.WorkerCertificateCode)));
                #endregion

                SetButtonEnable(_CertificateMoreMDL.ApplyStatus);
                UIHelp.WriteOperateLog(PersonName, UserID, "A本增发申请", string.Format("身份证号：{0}申请增发{1}A本。"
            , _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.UnitNameMore));

            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "申请A本增发失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, "保存成功！<br />请打印申请表，加盖单位公章后扫描上传，提交现单位审核！", "var isfresh=true;");
        }

        //提交单位审核、取消申请
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            CertificateMoreMDL _CertificateMoreMDL = (CertificateMoreMDL)ViewState["CertificateMoreMDL"];
            _CertificateMoreMDL.NewUnitAdvise = null;
            _CertificateMoreMDL.NewUnitCheckTime = null;
            _CertificateMoreMDL.CheckAdvise = null;
            _CertificateMoreMDL.CheckDate = null;
            _CertificateMoreMDL.CheckMan = null;
            _CertificateMoreMDL.ConfirmAdvise = null;
            _CertificateMoreMDL.ConfirmDate = null;
            _CertificateMoreMDL.ConfirmMan = null;
            _CertificateMoreMDL.ModifyTime = DateTime.Now;
                    _CertificateMoreMDL.ModifyPerson = PersonName;

            if (ButtonExit.Text == "提交单位审核")
            {
                #region 必须上传附件集合

                System.Collections.Hashtable fj = null;//必须上传附件集合
                System.Collections.Hashtable orFj = new System.Collections.Hashtable { };//多选一附件集合

                fj = new System.Collections.Hashtable{
                        {EnumManager.FileDataTypeName.变更申请表扫描件,0},
                        {EnumManager.FileDataTypeName.企业营业执照扫描件,0},
                        {EnumManager.FileDataTypeName.证件扫描件,0}};


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

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再提交！", sb), 5, 0);
                    return;
                }

                #endregion 必须上传附件集合

                try
                {                   
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.WaitUnitCheck;
                    CertificateMoreDAL.Update(_CertificateMoreMDL);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "A本增发申请提交单位审核失败！", ex);
                    return;
                }

                UIHelp.WriteOperateLog(PersonName, UserID, "A本增发申请提交单位审核", string.Format("身份证号：{0}申请增发{1}A本。"
            , _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.UnitNameMore));

                UIHelp.layerAlert(Page, "提交单位审核成功，请您立即联系所在企业网上确认。", string.Format(@"var isfresh=true;window.open('../PersonnelFile/Appraise.aspx?t={0}&o={1}'); layer.closeAll();", Utility.Cryptography.Encrypt("CertMore"), Utility.Cryptography.Encrypt(_CertificateMoreMDL.ApplyID.ToString()))); 
            }
            else//取消申请
            {
                try
                {
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.NewSave;
                    CertificateMoreDAL.Update(_CertificateMoreMDL);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "取消A本增发申请失败！", ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "取消A本增发申请", string.Format("身份证号：{0}申请增发{1}A本。"
            , _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.UnitNameMore));
                UIHelp.layerAlert(Page, "取消成功！", "var isfresh=true;");
            }

            ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;
            SetButtonEnable(_CertificateMoreMDL.ApplyStatus);
            BindCheckHistory(_CertificateMoreMDL.ApplyID.Value);
            BindFile(ApplyID);
           
        }

        //企业审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            CertificateMoreMDL _CertificateMoreMDL = (CertificateMoreMDL)ViewState["CertificateMoreMDL"];
            try
            {               
                _CertificateMoreMDL.NewUnitCheckTime = DateTime.Now;
                _CertificateMoreMDL.NewUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
                _CertificateMoreMDL.ApplyStatus = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.CertificateMore.Applyed : EnumManager.CertificateMore.SendBack);
                CertificateMoreDAL.Update(_CertificateMoreMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "单位审核A本增发申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "单位审核A本增发申请", string.Format("身份证号：{0}申请增发{1}A本。"
            , _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.UnitNameMore));

            UIHelp.layerAlert(Page, "审核成功！", "hideIfam(true);");
        }
        
        //建委审查
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string RadTextBoxZJHM)
        {
            if (RadTextBoxZJHM == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", RadTextBoxZJHM.Substring(RadTextBoxZJHM.Length - 3, 3), RadTextBoxZJHM);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }
        
        //导出
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/A本增发申请表.doc", string.Format("~/UpLoad/CertifChangeApply/A本增发申请表_{0}.doc", LabelWorkerCertificateCode.Text), GetExportData());
            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifChangeApply/A本增发申请表_{0}.doc", LabelWorkerCertificateCode.Text)));
            
            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("A本增发申请表", string.Format("~/UpLoad/CertifChangeApply/A本增发申请表_{0}.doc", LabelWorkerCertificateCode.Text.Trim())));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"));
        }

        //准备导出或打印标签替换数据（单页）
        protected Dictionary<string, string> GetExportData()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            list.Add("CreateTime", LabelApplyDate.Text);//申请日期
            list.Add("Birthday", LabelBirthday.Text);//生日
            list.Add("WorkerCertificateCode", LabelWorkerCertificateCode.Text);//证件号码
            list.Add("WorkerName", LabelWorkerName.Text);//姓名
            list.Add("Sex", LabelSex.Text);//性别
            list.Add("PeoplePhone", RadTextBoxPeoplePhone.Text.Trim());//联系电话
            list.Add("UnitName", LabelUnitName.Text);//企业名称
            list.Add("UnitNameMore", RadTextBoxUnitNameMore.Text.Trim());//增发企业名称
            list.Add("UnitCode", LabelUnitCode.Text);//组织代码
            list.Add("UnitCodeMore", RadTextBoxUnitCodeMore.Text.Trim());//增发组织代码
            list.Add("CertificateCode", LabelCertificateCode.Text);//证书编号
            list.Add("CertificateCodeMore","");//增发证书编号
            list.Add("ValidEndDate", LabelValidEndDate.Text);//有效期至
            list.Add("ValidEndDateMore","");//增发有效期至
            list.Add("ValidStartDate", LabelValidStartDate.Text);//发证日期
            list.Add("ValidStartDateMore", "");//增发发证日期
            list.Add("FacePhoto", LabelWorkerCertificateCode.Text);//照片标签
            list.Add("Img_FacePhoto", GetFacePhotoPath(LabelWorkerCertificateCode.Text));//绑定照片
            list.Add("PersonName", PersonName);

            return list;
        }

        ////根据证件号码显示生日和性别
        //protected void RadTextBoxWorkerCertificateCode_TextChanged(object sender, EventArgs e)
        //{
        //    if (RadTextBoxWorkerCertificateCode.Text.Trim().Length != 18)
        //    {
        //        UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15位数为证件号码
        //        return;
        //    }
        //    else if (Utility.Check.isChinaIDCard(RadTextBoxWorkerCertificateCode.Text.Trim()) == false)
        //    {
        //        UIHelp.layerAlert(Page, "“身份证”格式不正确！");
        //        return;
        //    }
        //    string workerCertificatecode = RadTextBoxWorkerCertificateCode.Text.Trim();
            
        //    CertificateOB o = CertificateMoreDAL.GetCertificateA(workerCertificatecode);   //根据证件号码得到用户
           
        //        if (o == null)
        //        {
        //            LabelWorkerName.Text = "";
        //            LabelBirthday.Text = "";
        //            LabelCertificateCode.Text ="";
        //            LabelSex.Text = "";
        //            LabelUnitCode.Text = "";
        //            LabelUnitName.Text = "";
        //            LabelValidStartDate.Text = "";
        //            LabelValidEndDate.Text = "";

        //            ViewState["CertificateOB"] = null;
        //            UIHelp.layerAlert(Page, "未找到您输入人员的有效的企业主要负责人（A本）信息，无法发起增发业务！");
        //            return;
        //        }
        //        ViewState["CertificateOB"] = o;

        //        LabelWorkerName.Text = o.WorkerName;
        //        LabelBirthday.Text = o.Birthday.Value.ToString("yyyy-MM-dd");
        //        LabelCertificateCode.Text = o.CertificateCode;
        //        LabelSex.Text = o.Sex;
        //        LabelUnitCode.Text = o.UnitCode;
        //        LabelUnitName.Text = o.UnitName;
        //        LabelValidStartDate.Text = o.ValidStartDate.Value.ToString("yyyy-MM-dd");
        //        LabelValidEndDate.Text = o.ValidEndDate.Value.ToString("yyyy-MM-dd");
               

        //        WorkerOB _Worker = WorkerDAL.GetUserObject(workerCertificatecode);

        //        if (_Worker !=null)
        //        {
        //            RadTextBoxPeoplePhone.Text = _Worker.Phone;
        //        }
            
        //    // 根据身份证同步他的照片
        //    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), GetFacePhotoPath(workerCertificatecode));
        //}

        //删除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            CertificateMoreMDL _CertificateMoreMDL = (CertificateMoreMDL)ViewState["CertificateMoreMDL"];
            if (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided)
            {
                UIHelp.layerAlert(Page, "申请已经审核了，不能删除！");
                return;
            }

            try
            {
                CertificateMoreDAL.Delete(_CertificateMoreMDL);
            }
            catch (Exception ex)
            {
                UIHelp.layerAlert(Page, "删除失败！");
                UIHelp.WriteErrorLog(Page, "删除申请失败！", ex);
                return;
            }
            ViewState["CertificateMoreMDL"]=null;
            SetButtonEnable("");
            BindFile(ApplyID);
            UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        protected void RadTextBoxUnitNameMore_TextChanged(object sender, EventArgs e)
        {
            string UnitCode = UnitDAL.GetUnitNameByUnitNameFromQY_BWDZZZS(RadTextBoxUnitNameMore.Text.Trim(),true);
            if (string.IsNullOrEmpty(UnitCode) == false)
            {
                RadTextBoxUnitCodeMore.Text = UnitCode;  //单位名称
            }
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
                UIHelp.WriteErrorLog(Page, "删除A本增发申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除A本增发申请表附件成功", string.Format("证书编号：{0}，文件名称：{1}。", LabelCertificateCode.Text, e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileName"]));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ApplyId)
        {
            DataTable dt = CertificateMoreDAL.GetCheckHistoryList(ApplyId);
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_填报中.Attributes["class"] = step_填报中.Attributes["class"].Replace(" green", "");
            step_待单位确认.Attributes["class"] = step_待单位确认.Attributes["class"].Replace("green", "");
            step_已申请.Attributes["class"] = step_已申请.Attributes["class"].Replace(" green", "");          
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
            step_已办结.Attributes["class"] = step_已办结.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {
                case EnumManager.CertificateMore.NewSave:
                case EnumManager.CertificateMore.SendBack:
                    step_填报中.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMore.WaitUnitCheck:
                    step_待单位确认.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMore.Applyed:
                    step_已申请.Attributes["class"] += " green";
                    break;

                case EnumManager.CertificateMore.Checked:
                    step_已审核.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMore.Decided:
                    try
                    {
                        CertificateMoreMDL _CertificateMoreMDL = (CertificateMoreMDL)ViewState["CertificateMoreMDL"];
                        CertificateOB _CertificateOB = CertificateDAL.GetCertificateOBObject(_CertificateMoreMDL.CertificateCodeMore);
                        if (_CertificateOB != null && _CertificateOB.ZZUrlUpTime > _CertificateMoreMDL.ConfirmDate)
                        {
                            step_已办结.Attributes["class"] += " green";
                        }
                        else
                        {
                            step_已决定.Attributes["class"] += " green";
                        }
                    }
                    catch
                    {
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
            ButtonSave.Enabled = false;//保 存
            ButtonExport.Enabled = false;//导出打印
            ButtonExit.Enabled = false;//取消申报 
            ButtonDelete.Enabled = false;//删除
            divSelectUnit.Style.Add("display", "none");
            switch (ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;//保 存
                    ButtonExport.Enabled = false;//导出打印
                    ButtonExit.Enabled = false;//提交单位审核 
                    ButtonDelete.Enabled = false;//删除
                    ButtonExit.Text = "提交单位审核";
                    divSelectUnit.Style.Add("display", "block");
                    tr_upPhoto.Visible = true;
                    tr_upPhotoCtl.Visible = true;
                    break;
                case EnumManager.CertificateMore.NewSave:
                    ButtonSave.Enabled = true;//保 存
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位审核 
                    ButtonDelete.Enabled = true;//删除
                    ButtonExit.Text = "提交单位审核";
                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        divSelectUnit.Style.Add("display", "block");
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        tr_upPhoto.Visible = true;
                        tr_upPhotoCtl.Visible = true;
                    }
                    break;
                case EnumManager.CertificateMore.WaitUnitCheck:
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
                case EnumManager.CertificateMore.SendBack:
                    ButtonSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = true;//删除
                    ButtonExport.Enabled = true;//导出打印
                    ButtonExit.Enabled = true;//提交单位确认  
                    ButtonExit.Text = "提交单位审核";

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        divSelectUnit.Style.Add("display", "block");
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        tr_upPhoto.Visible = true;
                        tr_upPhotoCtl.Visible = true;
                    }

                    break;
                case EnumManager.CertificateMore.Applyed://已申请
                    if (ValidPageViewLimit(RoleIDs, "CertifMoreAccepted.aspx") == true)//审核权限
                    {
                        TableJWCheck.Visible = true;
                        UIHelp.SetReadOnly(TextBoxCheckResult, false);

                    }
                    ButtonExit.Text = "取消申报";
                    ButtonExit.Enabled = true;//取消申报 
                    break;              
                case EnumManager.CertificateMore.Decided://已审核
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
                || ApplyStatus == EnumManager.CertificateMore.NewSave
                || ApplyStatus == EnumManager.CertificateMore.WaitUnitCheck
                || ApplyStatus == EnumManager.CertificateMore.SendBack
                || ApplyStatus == EnumManager.CertificateMore.Applyed))
            {
                tableWorker.Visible = true;
              
            }
            else
            {
                tableWorker.Visible = false;
            }

            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExport.CssClass = ButtonExport.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonExit.CssClass = ButtonExit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

    }
}
