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
    public partial class zjsApplyChange : BasePage
    {
        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["zjs_ApplyMDL"] == null ? "" : (ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL).ApplyID; }
        }

        /// <summary>
        /// 页面访问权限
        /// </summary>
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsApplyList.aspx|zjs/zjsAgency.aspx|zjs/zjsBusinessQuery.aspx";
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
                    zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
                    UIHelp.SetData(EditTable, _zjs_ApplyMDL, true);

                    zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = zjs_ApplyChangeDAL.GetObject(_ApplyID);
                    ViewState["zjs_ApplyChangeMDL"] = _zjs_ApplyChangeMDL;

                    //按钮控制
                    if (IfExistRoleID("0") == true
                    && (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报
                    || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认
                    || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回
                    || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                    )
                    {
                        UIHelp.SetData(EditTable, _zjs_ApplyChangeMDL, false);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                        divGR.Visible = true;//个人操作按钮
                    }
                    else//只读
                    {
                        UIHelp.SetData(EditTable, _zjs_ApplyChangeMDL, true);
                        UIHelp.SetReadOnly(RadTextBoxPSN_Email, true);
                    }

                    RadTextBoxTo_ZGZSBH.Enabled = true;
                    RequiredFieldValidator1.Enabled = true;

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
                    //按钮显示控制
                    SetButtonEnable(_zjs_ApplyMDL);
                    
                    //审批记录
                    BindCheckHistory(_zjs_ApplyChangeMDL.ApplyID);

                    //一寸照片
                    ImgOldPhoto.Src =UIHelp.ShowFile(UIHelp.ShowFaceImageJZS(_zjs_ApplyMDL.PSN_RegisterNo, _zjs_ApplyMDL.PSN_CertificateNO));
                  
                    FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                    if (_FileInfoMDL != null)
                    {
                        ImgUpdatePhoto.Src = UIHelp.ShowFile(_FileInfoMDL.FileUrl);
                    }
                    else
                    {
                        ImgUpdatePhoto.Src = ImgOldPhoto.Src;
                    }

                   

                    //手写签名照
                    ImgOldSign.Src = UIHelp.ShowFile(UIHelp.ShowSignImageJZS(_zjs_ApplyMDL.PSN_RegisterNo, _zjs_ApplyMDL.PSN_CertificateNO));
                    
                    string newSignUrl = FileInfoDAL.GetSignPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                    if (string.IsNullOrEmpty(newSignUrl) == false)
                    {
                        ImgUpdateSign.Src = UIHelp.ShowFile(newSignUrl);
                    }
                    else
                    {
                        ImgUpdateSign.Src = ImgOldSign.Src;
                    }

                    //附件
                    BindFile(_zjs_ApplyMDL.ApplyID);

                    //System.Random rm = new Random();
                    //string oldImgUrl = FileInfoDAL.GetPersonPhotoByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                    //if (oldImgUrl != "")
                    //{
                    //    ImgOldPhoto.Src = oldImgUrl;
                    //}
                    //else
                    //{
                    //    ImgOldPhoto.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(UIHelp.ShowFaceImage(_zjs_ApplyMDL.PSN_CertificateNO)));
                    //}

                    //string OldSignUrl = FileInfoDAL.GetSignPhotoByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                    //if (OldSignUrl != "")
                    //{
                    //    ImgOldSign.Src = OldSignUrl;
                    //}
                    //else
                    //{
                    //    ImgOldSign.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(UIHelp.GetSignImgPath(_zjs_ApplyMDL.PSN_CertificateNO))); //绑定签名照片;
                    //}

                    //字段有变化，文字变红
                    if (LabelPSN_NameFrom.Text != RadTextBoxPSN_NameTo.Text) UIHelp.SetBorderRed(RadTextBoxPSN_NameTo);//姓名
                    if (LabelZGZSBH.Text != RadTextBoxTo_ZGZSBH.Text) UIHelp.SetBorderRed(RadTextBoxTo_ZGZSBH);//资格管理证书
                    if (LabelFromPSN_CertificateNO.Text != RadTextBoxToPSN_CertificateNO.Text) UIHelp.SetBorderRed(RadTextBoxToPSN_CertificateNO);//证件号码
                    if (LabelFromPSN_BirthDate.Text != (_zjs_ApplyChangeMDL.ToPSN_BirthDate.HasValue == true ? _zjs_ApplyChangeMDL.ToPSN_BirthDate.Value.ToString("yyyy-MM-dd") : ""))//出生日期
                    {
                        RadDatePickerToPSN_BirthDate.BorderColor = System.Drawing.Color.Red;
                        RadDatePickerToPSN_BirthDate.BorderWidth = 1;
                    }
                    if (LabelFromPSN_Sex.Text != _zjs_ApplyChangeMDL.ToPSN_Sex)//性别
                    {
                        RadComboBoxToPSN_Sex.BorderColor = System.Drawing.Color.Red;
                        RadComboBoxToPSN_Sex.BorderWidth = 1;
                    }
                }
                else//new
                {
                    //按钮控制
                    SetButtonEnable(null);

                    //二造证书表
                    zjs_CertificateMDL o = zjs_CertificateDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));

                    //#region 企业信息与建造师证书信息不一致
                    //int PersonCount = COC_TOW_Person_BaseInfoDAL.ENT_IsorNotSame(ZZJGDM);
                    //if (PersonCount > 0)
                    //{
                    //    UIHelp.layerAlert(Page, "企业信息中的监管区县与建造师证书监管区县信息不一致，请先办理企业信息变更！", 5, 0);
                    //    return;
                    //}

                    //#endregion

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
                            UIHelp.SetData(EditTable, o, true);
                            UIHelp.SetReadOnly(RadTextBoxPSN_Email, false);
                            LabelPSN_NameFrom.Text = o.PSN_Name;
                            LabelFromPSN_Sex.Text = o.PSN_Sex;
                            LabelFromPSN_BirthDate.Text = Convert.ToDateTime(o.PSN_BirthDate).ToString("yyyy-MM-dd");
                            LabelFromPSN_CertificateNO.Text = o.PSN_CertificateNO;
        
                            BindFile("0");//附件

                            //补充要修改的信息
                            RadTextBoxPSN_NameTo.Text = o.PSN_Name;//姓名
                            RadComboBoxToPSN_Sex.SelectedValue = o.PSN_Sex;//性别
                            RadDatePickerToPSN_BirthDate.SelectedDate = o.PSN_BirthDate;//出生年月
                            RadTextBoxToPSN_CertificateNO.Text = o.PSN_CertificateNO;//证件号码
                            RadTextBoxTo_ZGZSBH.Enabled = true;
                            RequiredFieldValidator1.Enabled = true;
                            RadTextBoxTo_ZGZSBH.Text = o.ZGZSBH;//资格证书编号
                            LabelPSN_CertificateValidity.Text = o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");

                            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(o.PSN_CertificateNO);
                            if (string.IsNullOrEmpty(_WorkerOB.Email) == false) RadTextBoxPSN_Email.Text = _WorkerOB.Email;
                            if (string.IsNullOrEmpty(_WorkerOB.Mobile) == false) LabelPSN_MobilePhone.Text = _WorkerOB.Mobile;

                            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(LabelENT_OrganizationsCode.Text);
                            if (string.IsNullOrEmpty(_UnitMDL.ENT_Contact) == false) LabelLinkMan.Text = _UnitMDL.ENT_Contact;//联系人
                            if (string.IsNullOrEmpty(_UnitMDL.ENT_Telephone) == false) LabelENT_Telephone.Text = _UnitMDL.ENT_Telephone;//联系电话
                            if (string.IsNullOrEmpty(_UnitMDL.ENT_Type) == false) LabelENT_Type.Text = _UnitMDL.ENT_Type;//企业类型
                            if (string.IsNullOrEmpty(_UnitMDL.ENT_Corporate) == false) LabelFR.Text = _UnitMDL.ENT_Corporate;//法人
                            if (string.IsNullOrEmpty(_UnitMDL.END_Addess) == false) LabelEND_Addess.Text = _UnitMDL.END_Addess;//工商注册地址                            

                            ImgOldPhoto.Src = UIHelp.ShowFile(UIHelp.ShowFaceImageJZS(o.PSN_RegisterNO, o.PSN_CertificateNO));
                            ImgUpdatePhoto.Src = ImgOldPhoto.Src;
                            ImgOldSign.Src = UIHelp.ShowFile(UIHelp.ShowSignImageJZS(o.PSN_RegisterNO, o.PSN_CertificateNO));
                            ImgUpdateSign.Src = ImgOldSign.Src;
                        }
                        divGR.Visible = true;//个人操作按钮 
                    }
                }
                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;

                if (ViewLimit == true)//查看
                {
                    //divViewAction.Visible = true;
                    Disabled();//禁用控件
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

        //保存申请
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 输入验证 

            //邮箱校验
            if (RadTextBoxPSN_Email.Text.Trim() == "" || !Check.IfMailFormat(RadTextBoxPSN_Email.Text.Trim()))
            {
                UIHelp.layerAlert(Page, "保存失败，邮箱错误！", 6, 0);
                return;
            }

            //资格证检查       
            if (LabelZGZSBH.Text != RadTextBoxTo_ZGZSBH.Text.Trim())//修改了资格证书编号
            {
                zjs_QualificationMDL find = zjs_QualificationDAL.GetObject(RadTextBoxTo_ZGZSBH.Text);
                if (find == null || find.ZJHM == RadTextBoxToPSN_CertificateNO.Text.Trim())
                {
                    UIHelp.layerAlert(Page, "资格证书编号对应的人员信息与填写不符，不允许变更，请查询输入是否与资格证书信息一致！");
                    return;
                }
            }

            if (GetChange() == "")
            {
                UIHelp.layerAlert(Page, "没有变更任何内容，请检查填写是否正确！如果是变更照片或手写签名，请忽略此提示。", 0, 0);
            }

             #endregion 输入验证

            zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] == null ? new zjs_ApplyMDL() : (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            UIHelp.GetData(EditTable, _zjs_ApplyMDL);
            _zjs_ApplyMDL.XGR = UserName;
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.Valid = 1;
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
            _zjs_ApplyMDL.ApplyType = EnumManager.ApplyType.变更注册;            
            _zjs_ApplyMDL.ApplyTypeSub = "个人信息变更";
            zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = ViewState["zjs_ApplyChangeMDL"] == null ? new zjs_ApplyChangeMDL() : (zjs_ApplyChangeMDL)ViewState["zjs_ApplyChangeMDL"];//详细表
            UIHelp.GetData(EditTable, _zjs_ApplyChangeMDL);
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["zjs_ApplyMDL"] == null)//new
                {
                    _zjs_ApplyMDL.ApplyID = Guid.NewGuid().ToString();
                    _zjs_ApplyMDL.CJR = UserName;
                    _zjs_ApplyMDL.CJSJ = _zjs_ApplyMDL.XGSJ;
                    _zjs_ApplyMDL.PSN_Name = LabelPSN_NameFrom.Text;
                    _zjs_ApplyMDL.PSN_Sex = LabelFromPSN_Sex.Text;
                    _zjs_ApplyMDL.Birthday = Convert.ToDateTime(LabelFromPSN_BirthDate.Text);
                    _zjs_ApplyMDL.PSN_CertificateType = RadTextBoxPSN_CertificateType.Text;
                    _zjs_ApplyMDL.PSN_CertificateNO = LabelFromPSN_CertificateNO.Text;
                   
                    _zjs_ApplyMDL.ENT_Name = LabelENT_Name.Text; //申请表企业名称

                    //申请专业
                    _zjs_ApplyMDL.PSN_RegisteProfession = LabelPSN_RegisteProfession.Text;
                    _zjs_ApplyChangeMDL.ApplyID = _zjs_ApplyMDL.ApplyID;
                    _zjs_ApplyChangeMDL.ValidDate = Convert.ToDateTime(LabelPSN_CertificateValidity.Text);
                  
                    _zjs_ApplyChangeMDL.ChangeReason = "个人信息变更";  //变更原因
                    _zjs_ApplyMDL.ApplyCode = zjs_ApplyDAL.GetNextApplyCode(tran, "个人信息变更");
                    LabelApplyCode.Text = _zjs_ApplyMDL.ApplyCode;
                    //变更前信息
                    _zjs_ApplyChangeMDL.PSN_NameFrom = LabelPSN_NameFrom.Text;
                    _zjs_ApplyChangeMDL.FromPSN_Sex = LabelFromPSN_Sex.Text;
                    _zjs_ApplyChangeMDL.FromPSN_BirthDate = Convert.ToDateTime(LabelFromPSN_BirthDate.Text);
                    _zjs_ApplyChangeMDL.FromPSN_CertificateNO = LabelFromPSN_CertificateNO.Text;
                    _zjs_ApplyChangeMDL.ZGZSBH = LabelZGZSBH.Text;//资格证书编号

                    ////所属区县
                    //if (string.IsNullOrEmpty(_zjs_ApplyMDL.ENT_City))
                    //{
                    //    UIHelp.layerAlert(Page, "请让企业在企业信息里面去补全企业所属区县");
                    //    return;
                    //}

                    zjs_ApplyDAL.Insert(tran, _zjs_ApplyMDL);
                    zjs_ApplyChangeDAL.Insert(tran, _zjs_ApplyChangeMDL);

                    //拷贝人员当前有效附件到申请表
                    List<string> filetype = new List<string>();
                    //filetype.Add(EnumManager.FileDataTypeName.一寸免冠照片);
                    filetype.Add(EnumManager.FileDataTypeName.证件扫描件);
                    //filetype.Add(EnumManager.FileDataTypeName.学历证书扫描件);             
                    ApplyFileDAL.CopyFileFromCOC_TOW_Person_File(tran, _zjs_ApplyMDL.PSN_RegisterNo, _zjs_ApplyMDL.ApplyID, filetype);

                    SetButtonEnable(_zjs_ApplyMDL);
                }
                else//update
                {
                    zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                    zjs_ApplyChangeDAL.Update(tran, _zjs_ApplyChangeMDL);
                }
                tran.Commit();                
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师个人信息变更申请失败", ex);
            }

            ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
            ViewState["zjs_ApplyChangeMDL"] = _zjs_ApplyChangeMDL;

            //string NewImgUrl =ApplyFileDAL.GetPersonPhotoByApplyID(_zjs_ApplyMDL.ApplyID);
            //if (NewImgUrl != "")
            //{
            //    ImgUpdatePhoto.Src = UIHelp.ShowFile(NewImgUrl);
            //}

            BindFile(ApplyID);

            //操着按钮控制
            SetButtonEnable(_zjs_ApplyMDL);

            UIHelp.WriteOperateLog(UserName, UserID, "保存造价工程师个人信息变更申请", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
            UIHelp.layerAlert(Page, "保存成功，申报时请将盖章签字后的申请表扫描上传");
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            try
            {
                zjs_ApplyDAL.Delete(_zjs_ApplyMDL.ApplyID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除造价工程师个人信息变更申请失败！", ex);
                return;
            }
            ViewState.Remove("zjs_ApplyMDL");
            SetButtonEnable(_zjs_ApplyMDL);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师个人信息变更申请", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
            UIHelp.ParentAlert(Page, "删除成功！", true);
        }

        //个人提交单位确认
        protected void ButtonUnit_Click(object sender, EventArgs e)
        {
            if(GetChange()=="")
            {
                UIHelp.layerAlert(Page, "没有变更任何内容，请检查填写是否正确！", 5, 0);
                return;
            }
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表

            RadTextBoxTo_ZGZSBH.Enabled = true;
            RequiredFieldValidator1.Enabled = true;

            if (ButtonUnit.Text != "取消申报")
            {
                //必须上传附件集合
                System.Collections.Hashtable fj = new System.Collections.Hashtable{
                //{EnumManager.FileDataTypeName.一寸免冠照片,0},
                {EnumManager.FileDataTypeName.证件扫描件,0},
                {EnumManager.FileDataTypeName.申请表扫描件,0}                
                };

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
            try
            {
                zjs_ApplyDAL.Update(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "提交单位审核造价工程师个人信息变更申请失败", ex);
                return;
            }
            ViewState["zjs_ApplyMDL"] = _zjs_ApplyMDL;
            SetButtonEnable(_zjs_ApplyMDL);           
            BindFile(_zjs_ApplyMDL.ApplyID); //人员照片
            if (_zjs_ApplyMDL.ApplyStatus == "待确认")
            {
                UIHelp.WriteOperateLog(UserName, UserID, "提交单位审核造价工程师个人信息变更申请", string.Format("注册证书编号：{0}，变更内容：{1}", LabelPSN_RegisterNO.Text, GetChange()));
                string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
            else
            {
                UIHelp.WriteOperateLog(UserName, UserID, "撤销提交单位审核造价工程师个人信息变更申请", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
                UIHelp.layerAlert(Page, "撤销成功！", 6, 3000);
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
           
        }

        //企业提价申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];//申请表
            try
            {
                RadTextBoxTo_ZGZSBH.Enabled = true;
                RequiredFieldValidator1.Enabled = true;

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
                    UIHelp.WriteOperateLog(UserName, UserID, "企业确认提交造价工程师个人信息变更申请", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
                    string js = string.Format("<script>window.parent.location.href='zjsApplyHistory.aspx?c={0}';</script>", _zjs_ApplyMDL.ApplyID);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "企业取消申报造价工程师个人信息变更申请", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
                    UIHelp.layerAlert(Page, "取消申报成功！", 6, 3000);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业确认提交造价工程师个人信息变更申请失败！", ex);
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //导出打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = "";
                string fileName = "";
                zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;

                sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师个人信息变更申请表.docx");
                    fileName = "二级注册造价工程师个人信息变更申请表";
               
                zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = ViewState["zjs_ApplyChangeMDL"] as zjs_ApplyChangeMDL;
                UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(_zjs_ApplyMDL.ENT_OrganizationsCode);
                FileInfoMDL _FileInfoMDL = FileInfoDAL.GetPersonPhotoByApplyid(_zjs_ApplyMDL.ApplyID);
                var o = new List<object>();
                o.Add(_zjs_ApplyMDL);
                o.Add(_zjs_ApplyChangeMDL);
                o.Add(_UnitMDL);
                var ht = PrintDocument.GetProperties(o);

                //新照片
                if (_FileInfoMDL != null)
                {
                    ht["photo_new"] = _FileInfoMDL.FileUrl == null ? "" : _FileInfoMDL.FileUrl;
                }
                else 
                {
                    ht["photo_new"] = "";
                }
                if (ht["photo_new"].ToString() == "")
                {
                    UIHelp.layerAlert(Page, "请先上传一寸证件照！", 5, 0);
                    return;
                }

                //原照片
                string oldPhontoUrl = FileInfoDAL.GetPersonPhotoByPSN_RegisterNO(_zjs_ApplyMDL.PSN_RegisterNo);
                if (oldPhontoUrl != "")
                {
                    ht["photo"] = oldPhontoUrl;
                }
                else
                {
                    ht["photo"] = "~/Images/tup.gif";
                }

                ht["isCtable"] = false;
                //对时间类型进行格式转换
                ht["ValidDate"] = _zjs_ApplyChangeMDL.ValidDate == null ? "" : ((DateTime)_zjs_ApplyChangeMDL.ValidDate).ToString("yyyy-MM-dd");
                ht["FromPSN_BirthDate"] = _zjs_ApplyChangeMDL.FromPSN_BirthDate == null ? "" : ((DateTime)_zjs_ApplyChangeMDL.FromPSN_BirthDate).ToString("yyyy-MM-dd");
                ht["ToPSN_BirthDate"] = _zjs_ApplyChangeMDL.ToPSN_BirthDate == null ? "" : ((DateTime)_zjs_ApplyChangeMDL.ToPSN_BirthDate).ToString("yyyy-MM-dd");
                ht["FR"] = _UnitMDL.ENT_Corporate;
                ht["END_Addess"] = _UnitMDL.END_Addess;
                ht["ENT_Type"] = _UnitMDL.ENT_Type;
                ht["LinkMan"] = _UnitMDL.ENT_Contact;
                ht["ENT_Telephone"] = _UnitMDL.ENT_Telephone;
                ht["ENT_Sort"] = _UnitMDL.ENT_Sort;
                ht["ENT_Grade"] = _UnitMDL.ENT_Grade;
                ht["ENT_QualificationCertificateNo"] = _UnitMDL.ENT_QualificationCertificateNo;
                PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印造价工程师个人信息变更申请表失败！", ex);
            }
        }

        //受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(ApplyID);
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
                UIHelp.WriteErrorLog(Page, "受理造价工程师个人信息变更失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "受理造价工程师个人信息变更", string.Format("注册证书编号：{0}，受理结果：{1}，受理意见：{2}。"
                , LabelPSN_RegisterNO.Text, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark));
            UIHelp.ParentAlert(Page, "受理成功！", true);

            //string js = string.Format("<script>window.parent.location.href='../zjs/zjsBusinessQuery.aspx?id={0}&&type={1}';</script>", _zjs_ApplyMDL.ApplyID, _zjs_ApplyMDL.ApplyType);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS2", js);          
        }

        //审核
        protected void BttSave_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;

            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_zjs_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion

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
                UIHelp.WriteErrorLog(Page, "审核造价工程师个人信息变更失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "审核造价工程师个人信息变更", string.Format("注册证书编号：{0}，审核结果：{1}，审核意见：{2}。"
               , LabelPSN_RegisterNO.Text, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }

        //决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
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
            string sql = "";
            _zjs_ApplyMDL.XGSJ = DateTime.Now;
            _zjs_ApplyMDL.XGR = UserName;
            //#region 查询证书是否锁定
            //int count = DataAccess.Certificate_LockDAL.LockIsorNOT(_zjs_ApplyMDL.PSN_CertificateNO);
            //if (count > 0)
            //{
            //    UIHelp.layerAlertWithHtml(Page, UIHelp.ErJianCertLockMessage);
            //    return;
            //}
            //#endregion

            _zjs_ApplyMDL.ConfirmDate = DateTime.Now;
            _zjs_ApplyMDL.ConfirmMan = UserName;
            _zjs_ApplyMDL.ConfirmResult = RadioButtonListDecide.SelectedValue;
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已决定;
            _zjs_ApplyMDL.NoticeDate = DateTime.Now;//此业务结束，给他个公告时间，系统就能分配可以重新申请业务

            zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = (zjs_ApplyChangeMDL)ViewState["zjs_ApplyChangeMDL"];

            //证书数量（多专业）
            int CertCount = zjs_CertificateDAL.SelectCount(string.Format(" and [PSN_CertificateNO]='{0}'", _zjs_ApplyMDL.PSN_CertificateNO));

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //更新申请单
                zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);

                if (RadioButtonListDecide.SelectedValue == "通过")
                {
                    #region 更新证书

                    if (CertCount > 1 && _zjs_ApplyChangeMDL.ZGZSBH == _zjs_ApplyChangeMDL.To_ZGZSBH)//资格证书未发生变化，同时更新多个专业
                    {
                        #region 更新多本证书（按身份证更新）

                        //证书信息写入历史表
                        sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_CertificateNO ='{0}' ";
                        CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.PSN_CertificateNO));

                        //Update证书表信息
                        sql = @"update [dbo].[zjs_Certificate] 
                                         set [zjs_Certificate].[PSN_Name] = c.[PSN_NameTo]   
                                            ,[zjs_Certificate].[PSN_Sex]=c.[ToPSN_Sex]     
                                            ,[zjs_Certificate].[PSN_BirthDate]=c.[ToPSN_BirthDate]   
                                            ,[zjs_Certificate].[PSN_CertificateNO]=c.[ToPSN_CertificateNO]
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{2}'
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                        FROM [dbo].[zjs_ApplyChange] c
                                            inner join [dbo].[zjs_Apply] a on c.ApplyID = a.ApplyID
                                            inner join [dbo].[zjs_Certificate] on a.PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO
                                            where a.ApplyID ='{3}' and a.ApplyTypeSub='个人信息变更' and a.[ConfirmResult]='通过'";
                        CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.ConfirmDate.Value.ToString("yyyy-MM-dd"), UserName, DateTime.Now, _zjs_ApplyMDL.ApplyID, EnumManager.ZJSApplyTypeCode.变更注册));

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

                        #endregion
                    }
                    else
                    {
                        #region 更新一本证书

                        //证书信息写入历史表
                        sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_RegisterNO ='{0}' ";
                        CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.PSN_RegisterNo));

                        //Update证书表信息
                        sql = @"update [dbo].[zjs_Certificate] 
                                         set [zjs_Certificate].[PSN_Name] = c.[PSN_NameTo]   
                                            ,[zjs_Certificate].[PSN_Sex]=c.[ToPSN_Sex]     
                                            ,[zjs_Certificate].[PSN_BirthDate]=c.[ToPSN_BirthDate]   
                                            ,[zjs_Certificate].[PSN_CertificateNO]=c.[ToPSN_CertificateNO]
                                            ,[zjs_Certificate].[ZGZSBH]=c.[To_ZGZSBH]
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{2}'
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                        FROM [dbo].[zjs_ApplyChange] c
                                            inner join [dbo].[zjs_Apply] a on c.ApplyID = a.ApplyID
                                            inner join [dbo].[zjs_Certificate] on a.PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO AND a.PSN_RegisteProfession =zjs_Certificate.PSN_RegisteProfession
                                            where a.ApplyID ='{3}' and a.ApplyTypeSub='个人信息变更' and a.[ConfirmResult]='通过'";
                        CommonDAL.ExecSQL(tran, string.Format(sql, _zjs_ApplyMDL.ConfirmDate.Value.ToString("yyyy-MM-dd"), UserName, DateTime.Now, _zjs_ApplyMDL.ApplyID, EnumManager.ZJSApplyTypeCode.变更注册));

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
                        #endregion 更新一本证书
                    }
                    #endregion
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批准造价工程师个人信息变更失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "批准造价工程师个人信息变更", string.Format("注册证书编号：{0}", LabelPSN_RegisterNO.Text));
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
                    log = string.Format("姓名：{0}，身份证号：{1}，受理时间：{2}，受理结果：{3}，受理意见：{4}。后退到“{5}”状态。"
                   , RadTextBoxPSN_NameTo.Text, RadTextBoxToPSN_CertificateNO.Text,  _zjs_ApplyMDL.GetDateTime, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                    log = string.Format("姓名：{0}，身份证号：{1}，审核时间：{2}，审核结果：{3}，审核意见：{4}。后退到“{5}”状态。"
                           , RadTextBoxPSN_NameTo.Text, RadTextBoxToPSN_CertificateNO.Text, _zjs_ApplyMDL.ExamineDatetime, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
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
                    log = string.Format("姓名：{0}，身份证号：{1}，决定时间：{2}，决定结果：{3}。后退到“{4}”状态。"
                           , RadTextBoxPSN_NameTo.Text, RadTextBoxToPSN_CertificateNO.Text, _zjs_ApplyMDL.ConfirmDate, _zjs_ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
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
                UIHelp.WriteErrorLog(Page, "后退造价工程师个人信息变更审批节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师个人信息变更审批节点", log);
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

            //一寸照片
            string NewImgUrl = ApplyFileDAL.GetPersonPhotoByApplyID(ApplyID);
            if (NewImgUrl != "")
            {
                ImgUpdatePhoto.Src =UIHelp.ShowFile(NewImgUrl);
            }
            else
            {
                if (ImgOldPhoto.Src != ImgUpdatePhoto.Src)
                {
                    ImgUpdatePhoto.Src = ImgOldPhoto.Src;
                }
            }

            //手写签名照
            string NewSignImgUrl = ApplyFileDAL.GetSignPhotoByApplyID(ApplyID);
            if (NewSignImgUrl != "")
            {
                ImgUpdateSign.Src = UIHelp.ShowFile(NewSignImgUrl);
            }
            else
            {
                if (ImgOldSign.Src != ImgUpdateSign.Src)
                {
                    ImgUpdateSign.Src = ImgOldSign.Src;
                }
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
                UIHelp.WriteErrorLog(Page, "删除造价工程师个人信息变更附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师个人信息变更附件", string.Format("注册证书编号：{0}，FileID={1}", LabelPSN_RegisterNO.Text,FileID));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
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
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";

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
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "取消申报";
                    ButtonOutput.Enabled = false;

                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";

                    //受理权限
                    if (IfExistRoleID("20") == true)
                    {
                        divQX.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    ButtonSave.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonUnit.Enabled = true;
                    ButtonUnit.Text = "提交单位确认";

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
                        if (o.ConfirmResult == "不通过")//只有决定不通过才能后退，如果通过了（修改了证书），需要人工后退。
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    break;
            }
            ButtonUnit.CssClass = ButtonUnit.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
        }      

        //变更修改信息颜色
        protected void setColor()
        {
            if (LabelPSN_NameFrom.Text != RadTextBoxPSN_NameTo.Text)
            {
                RadTextBoxPSN_NameTo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                RadTextBoxPSN_NameTo.ForeColor = System.Drawing.Color.Black;
            }

            if (LabelFromPSN_Sex.Text != RadComboBoxToPSN_Sex.Text)
            {
                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
            }

            if (LabelFromPSN_BirthDate.Text != RadDatePickerToPSN_BirthDate.SelectedDate.Value.ToString())
            {
                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
            }
            if (LabelFromPSN_CertificateNO.Text != RadTextBoxToPSN_CertificateNO.Text)
            {
                RadTextBoxToPSN_CertificateNO.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                RadComboBoxToPSN_Sex.ForeColor = System.Drawing.Color.Black;
            }
        }
        
        /// <summary>
        /// 获取个人信息变更内容说明
        /// </summary>
        /// <returns>个人信息变更内容说明</returns>
        private string GetChange()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (LabelPSN_NameFrom.Text != RadTextBoxPSN_NameTo.Text) sb.Append(string.Format("姓名从【{0}】变为【{1}】；", LabelPSN_NameFrom.Text, RadTextBoxPSN_NameTo.Text.Trim()));//姓名
            if (LabelFromPSN_CertificateNO.Text != RadTextBoxToPSN_CertificateNO.Text) sb.Append(string.Format("证件号码从【{0}】变为【{1}】；", LabelFromPSN_CertificateNO.Text, RadTextBoxToPSN_CertificateNO.Text.Trim()));//证件号码
            if (LabelFromPSN_BirthDate.Text != RadDatePickerToPSN_BirthDate.SelectedDate.Value.ToString("yyyy-MM-dd"))//出生日期
            {
                sb.Append(string.Format("出生日期从【{0}】变为【{1}】；", LabelFromPSN_BirthDate.Text, RadDatePickerToPSN_BirthDate.SelectedDate.Value.ToString("yyyy-MM-dd")));//姓名
            }
            if (LabelFromPSN_Sex.Text != RadComboBoxToPSN_Sex.Text)//性别
            {
                sb.Append(string.Format("性别从【{0}】变为【{1}】；", LabelFromPSN_Sex.Text, RadComboBoxToPSN_Sex.Text.Trim()));//性别
            }
            if (LabelZGZSBH.Text != RadTextBoxTo_ZGZSBH.Text) sb.Append(string.Format("资格证书编号从【{0}】变为【{1}】；", LabelZGZSBH.Text, RadTextBoxTo_ZGZSBH.Text.Trim()));//资格管理证书

            if( ImgOldPhoto.Src != ImgUpdatePhoto.Src)
            {
                sb.Append("变更了一寸照片；");
            }
            if (ImgOldSign.Src != ImgUpdateSign.Src)
            {
                sb.Append("变更了手写签名照；");
            }

            return sb.ToString();
        }
        //禁用控件
        private void Disabled()
        {
            UIHelp.SetReadOnly(RadTextBoxPSN_NameTo, true);
            UIHelp.SetReadOnly(RadComboBoxToPSN_Sex, false);
            UIHelp.SetReadOnly(RadDatePickerToPSN_BirthDate, false);
            UIHelp.SetReadOnly(RadTextBoxTo_ZGZSBH, true);//证件类别
            UIHelp.SetReadOnly(RadTextBoxToPSN_CertificateNO, true);
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
      
    }
}