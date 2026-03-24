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
    public partial class CertifMoreCheck : BasePage
    {

        /// <summary>
        /// A增发附件申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["CertificateMoreMDL"] == null ? "" : string.Format("ZF-{0}", (ViewState["CertificateMoreMDL"] as CertificateMoreMDL).ApplyID); }
        }

        /// <summary>
        /// 待打印列证书ID集合
        /// </summary>
        public List<string> MoreList
        {
            get { return ViewState["MoreList"] as List<string>; }
        }

        public DataTable MoreTable
        {
            get { return ViewState["MoreTable"] as DataTable; }

        }
        /// <summary>
        /// 当前显示证书索引，从0开始
        /// </summary>
        public int CurrentIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentIndex"]); }
            set
            {
                ViewState["CurrentIndex"] = value;
                if (MoreList.Count < 1)
                {
                    LabelCheckCount.Text = string.Format("批量决定（已完成）");
                    ButtonConfirm.Enabled = false;
                    ButtonConfirm.CssClass = ButtonConfirm.Enabled == true ? "bt_large" : "bt_large btn_no";
                }
                else
                {
                    LabelCheckCount.Text = string.Format("批量决定（共{0}单，当前第{1}单）", MoreList.Count, value + 1);
                }
            }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifMoreAccepted.aspx|CertifMoreConfirm.aspx|ApplyQuerySLR.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    CertificateMoreMDL o = CertificateMoreDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));

                    ViewState["CertificateMoreMDL"] = o;
                    SetStep(o.ApplyStatus);

                    LabelWorkerCertificateCode.Text = o.WorkerCertificateCode;
                    LabelApplyDate.Text = o.CreateTime.Value.ToString("yyyy-MM-dd");
                    LabelWorkerName.Text = o.WorkerName;
                    LabelBirthday.Text = o.Birthday.Value.ToString("yyyy-MM-dd");
                    LabelSex.Text = o.Sex;
                    LabelUnitCode.Text = o.UnitCode;
                    LabelUnitName.Text = o.UnitName;
                    LabelValidStartDate.Text = o.ValidStartDate.Value.ToString("yyyy-MM-dd");
                    LabelValidEndDate.Text = o.ValidEndDate.Value.ToString("yyyy-MM-dd");
                    RadTextBoxPeoplePhone.Text = o.PeoplePhone;
                    RadTextBoxUnitNameMore.Text = o.UnitNameMore;
                    RadTextBoxUnitCodeMore.Text = o.UnitCodeMore;

                    switch (o.ApplyStatus)
                    {
                        case EnumManager.CertificateMore.Applyed://已申请
                            PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(147);
                            LabelCertificateCodeMore.Text = PostInfoDAL.GetNextCertificateNoUnUpdate(ref _PostInfoOB);
                            LabelValidStartDateMore.Text = LabelValidStartDate.Text;
                            LabelValidEndDateMore.Text = LabelValidEndDate.Text;
                            //LabelApplyStatus.Text = o.ApplyStatus;
                            if (ValidPageViewLimit(RoleIDs, "CertifMoreAccepted.aspx") == true)
                            {
                                TableJWCheck.Visible = true;
                            }
                            break;
                        case EnumManager.CertificateMore.Checked://已审核
                            LabelCertificateCodeMore.Text = o.CertificateCodeMore;
                            LabelValidStartDateMore.Text = o.ValidStartDateMore.Value.ToString("yyyy-MM-dd");
                            LabelValidEndDateMore.Text = o.ValidEndDateMore.Value.ToString("yyyy-MM-dd");
                            if (ValidPageViewLimit(RoleIDs, "CertifMoreConfirm.aspx") == true)
                            {
                                TableJWConfirm.Visible = true;
                                LabelCheckTitle.Text = "A本增发决定";
                            }
                            break;
                        case EnumManager.CertificateMore.Decided://已决定
                            LabelCertificateCodeMore.Text = o.CertificateCodeMore;
                            LabelValidStartDateMore.Text = o.ValidStartDateMore.Value.ToString("yyyy-MM-dd");
                            LabelValidEndDateMore.Text = o.ValidEndDateMore.Value.ToString("yyyy-MM-dd");
                            break;
                        default:
                            LabelCertificateCodeMore.Text = "";
                            LabelValidStartDateMore.Text = "";
                            LabelValidEndDateMore.Text = "";
                            break;
                    }

                    //LabelCheckDate.Text = o.CheckDate.Value.ToString("yyyy-MM-dd HH:mm");
                    //LabelCheckMan.Text = o.CheckMan;
                    //LabelApplyStatus.Text = o.ApplyStatus;
                    //LabelCheckAdvise.Text = o.CheckAdvise;

                    RadTextBoxUnitNameMore.Enabled = false;
                    RadTextBoxUnitCodeMore.Enabled = false;

                    // 根据身份证同步他的照片
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(o.WorkerCertificateCode)));

                    BindFile(ApplyID);
                    BindCheckHistory(o.ApplyID.Value);
                }
                else  if (Session["CertificateMoreList"] != null)//批量打印
                {
                    LabelCheckTitle.Text = "A本增发决定";
                    ViewState["MoreList"] = Session["CertificateMoreList"];
                    Session.Remove("CertificateMoreList");
                    ViewState["MoreTable"] = Session["CertificateMoreTable"];
                    Session.Remove("CertificateMoreTable");

                    tr_PatchCheck.Visible = true;
                    CheckBoxAutoConfirm.Visible = true;

                    if (MoreTable != null)
                    {
                        BindPrintDetail(0);
                    }
                }
            }
        }
        
        ////审批通过
        //protected void ButtonSave_Click(object sender, EventArgs e)
        //{

        //    CertificateMoreMDL _CertificateMoreMDL = ViewState["CertificateMoreMDL"] as CertificateMoreMDL;//增发申请表

        //    PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(147);//放号规则

        //    CertificateOB OldCert = CertificateDAL.GetObject(_CertificateMoreMDL.CertificateID.Value);//现有A本
           
        //    DBHelper db = new DBHelper();
        //    DbTransaction tran = db.BeginTransaction();
        //    try
        //    {
        //        //更新申请表
        //        _CertificateMoreMDL.CertificateCodeMore = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, tran); 
        //        _CertificateMoreMDL.ModifyTime = DateTime.Now;   //审核日期
        //        _CertificateMoreMDL.CheckDate = _CertificateMoreMDL.ModifyTime;
        //        _CertificateMoreMDL.ModifyPerson = PersonName;//审核人
        //        _CertificateMoreMDL.CheckMan = PersonName;
        //        _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.Decided;  //状态
        //        _CertificateMoreMDL.CheckAdvise = EnumManager.CertificateMoreCheckResult.CheckPass;                
        //        CertificateMoreDAL.Update(tran,_CertificateMoreMDL);
        //        ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;

        //        //创建证书
        //        CertificateOB ceron = new CertificateOB();
        //        ceron.ExamPlanID = -400;//考试计划ID
        //        ceron.WorkerID = OldCert.WorkerID;//从业人员ID
        //        ceron.CertificateCode = _CertificateMoreMDL.CertificateCodeMore;//证书编号               
        //        ceron.WorkerName = OldCert.WorkerName;//姓名
        //        ceron.WorkerCertificateCode = OldCert.WorkerCertificateCode;//证件号码
        //        ceron.PostTypeID = OldCert.PostTypeID;//岗位
        //        ceron.PostID = OldCert.PostID;//工种
        //        ceron.PostTypeName = OldCert.PostTypeName;//岗位
        //        ceron.PostName = OldCert.PostName;//工种
        //        ceron.Sex = OldCert.Sex;//性别
        //        ceron.Birthday = OldCert.Birthday;//出生日期
        //        ceron.UnitName = _CertificateMoreMDL.UnitNameMore;//工作单位
        //        ceron.UnitCode = _CertificateMoreMDL.UnitCodeMore;//组织机构代码
        //        ceron.ConferDate = OldCert.ConferDate;//发证日期
        //        ceron.ConferUnit = "北京市住建委";
        //        ceron.ValidStartDate =  OldCert.ValidStartDate;//证书有效期起
        //        ceron.ValidEndDate =  OldCert.ValidEndDate;//证书有效期止
        //        ceron.CreatePersonID = PersonID;//创建人ID
        //        ceron.CreateTime = DateTime.Now;//创建时间
        //        ceron.Status = EnumManager.CertificateUpdateType.first;
        //        ceron.SkillLevel = OldCert.SkillLevel;//技术职称(技术等级)
        //        ceron.ApplyMan = _CertificateMoreMDL.CreatePerson;//业务办理申请人（报名、续期、变更时都改变）
        //        ceron.TrainUnitName = OldCert.TrainUnitName;//培训点
        //        ceron.Remark = string.Format("{0}日增发", ceron.CreateTime.Value.ToString("yyyy-MM-dd"));

        //        CertificateDAL.Insert(tran, ceron);
        //        tran.Commit();
              

        //        LabelCertificateCodeMore.Text = _CertificateMoreMDL.CertificateCodeMore;
        //        LabelValidStartDateMore.Text = _CertificateMoreMDL.ValidStartDateMore.Value.ToString("yyyy-MM-dd");
        //        LabelValidEndDateMore.Text = _CertificateMoreMDL.ValidEndDateMore.Value.ToString("yyyy-MM-dd");
        //        LabelCheckDate.Text = _CertificateMoreMDL.CheckDate.Value.ToString("yyyy-MM-dd HH:mm");
        //        LabelCheckMan.Text = _CertificateMoreMDL.CheckMan;
        //        LabelApplyStatus.Text = _CertificateMoreMDL.ApplyStatus;
        //        LabelCheckAdvise.Text = _CertificateMoreMDL.CheckAdvise;

        //        //保存后才能导出和打印

        //        ButtonDelete.Visible = false;
        //        ButtonSave.Visible = false;

        //        UIHelp.WriteOperateLog(PersonName, UserID, "A本增发审核", string.Format("姓名：{0}，身份证号：{1}，审核结果：{2}，增发证书编号{3}。"
        //       , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode
        //       , EnumManager.CertificateMoreCheckResult.CheckPass, _CertificateMoreMDL.CertificateCodeMore));

        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        UIHelp.WriteErrorLog(Page, "A本增发审核失败！", ex);
        //        return;
        //    }

        //    UIHelp.layerAlert(Page, "增发成功，请一到两日后自行打印电子证书！", "hideIfam(true);");
        //}

        ////审核不通过
        //protected void ButtonDelete_Click(object sender, EventArgs e)
        //{
        //    CertificateMoreMDL _CertificateMoreMDL = (CertificateMoreMDL)ViewState["CertificateMoreMDL"];

        //    try
        //    {
        //        //更新申请表
        //        _CertificateMoreMDL.ModifyTime = DateTime.Now;   //审核日期
        //        _CertificateMoreMDL.CheckDate = _CertificateMoreMDL.ModifyTime;
        //        _CertificateMoreMDL.ModifyPerson = PersonName;//审核人
        //        _CertificateMoreMDL.CheckMan = PersonName;
        //        _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.Decided;  //状态
        //        _CertificateMoreMDL.CheckAdvise = EnumManager.CertificateMoreCheckResult.CheckNoPass;
        //        _CertificateMoreMDL.ValID = "0";

        //        CertificateMoreDAL.Update(_CertificateMoreMDL);                             

        //        LabelCheckDate.Text = _CertificateMoreMDL.CheckDate.Value.ToString("yyyy-MM-dd HH:mm");
        //        LabelCheckMan.Text = _CertificateMoreMDL.CheckMan;
        //        LabelApplyStatus.Text = _CertificateMoreMDL.ApplyStatus;
        //        LabelCheckAdvise.Text = _CertificateMoreMDL.CheckAdvise;

        //        LabelCertificateCodeMore.Text = "";
        //        LabelValidStartDateMore.Text = "";
        //        LabelValidEndDateMore.Text = "";

        //        ButtonDelete.Visible = false;
        //        ButtonSave.Visible = false;

        //        UIHelp.WriteOperateLog(PersonName, UserID, "A本增发审核", string.Format("姓名：{0}，身份证号：{1}，审核结果：{2}。"
        //       , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode, EnumManager.CertificateMoreCheckResult.CheckNoPass));
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "A本增发审核失败", ex);
        //        return;
        //    }
        //}
        
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

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            CertificateMoreMDL _CertificateMoreMDL = ViewState["CertificateMoreMDL"] as CertificateMoreMDL;//增发申请表

            if (RadioButtonListJWCheck.SelectedValue == "通过")
            {

                PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(147);//放号规则

                CertificateOB OldCert = CertificateDAL.GetObject(_CertificateMoreMDL.CertificateID.Value);//现有A本

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    //更新申请表
                    _CertificateMoreMDL.CertificateCodeMore = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, tran);
                    _CertificateMoreMDL.CheckDate = DateTime.Now;   //审核日期
                    _CertificateMoreMDL.CheckMan = PersonName;
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.Checked;  //状态
                    _CertificateMoreMDL.CheckAdvise = EnumManager.CertificateMoreCheckResult.CheckPass;
                    CertificateMoreDAL.Update(tran, _CertificateMoreMDL);
                    tran.Commit();
                    ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;

                    LabelCertificateCodeMore.Text = _CertificateMoreMDL.CertificateCodeMore;
                    LabelValidStartDateMore.Text = _CertificateMoreMDL.ValidStartDateMore.Value.ToString("yyyy-MM-dd");
                    LabelValidEndDateMore.Text = _CertificateMoreMDL.ValidEndDateMore.Value.ToString("yyyy-MM-dd");

                    UIHelp.WriteOperateLog(PersonName, UserID, "A本增发审核", string.Format("姓名：{0}，身份证号：{1}，审核结果：{2}，增发证书编号{3}。"
                   , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode
                   , EnumManager.CertificateMoreCheckResult.CheckPass, _CertificateMoreMDL.CertificateCodeMore));

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "A本增发审核失败！", ex);
                    return;
                }

                UIHelp.layerAlert(Page, "A本增发审核成功", "hideIfam(true);");

            }
            else
            {
                try
                {
                    //更新申请表
                    _CertificateMoreMDL.CheckDate = DateTime.Now;   //审核日期
                    _CertificateMoreMDL.CheckMan = PersonName;
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.SendBack;  //状态
                    _CertificateMoreMDL.CheckAdvise = TextBoxCheckResult.Text;
                    CertificateMoreDAL.Update(_CertificateMoreMDL);
                    ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;
                    
                }
                catch (Exception ex)
                {

                    UIHelp.WriteErrorLog(Page, "A本增发审核失败！", ex);
                    return;
                }
                SetStep(_CertificateMoreMDL.ApplyStatus);
                UIHelp.WriteOperateLog(PersonName, UserID, "A本增发审核", string.Format("姓名：{0}，身份证号：{1}，审核结果：{2}。"
             , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode, EnumManager.CertificateMoreCheckResult.CheckNoPass));

                UIHelp.layerAlert(Page, "驳回成功。", "hideIfam(true);");
            }
        }

        //建委决定
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            CertificateMoreMDL _CertificateMoreMDL = ViewState["CertificateMoreMDL"] as CertificateMoreMDL;//增发申请表

            int countA = CertificateDAL.SelectCount(string.Format(" and CertificateCode='{0}'", _CertificateMoreMDL.CertificateCodeMore));

            if (countA > 0)
            {
                UIHelp.layerAlert(Page, "该申请已经决定过了，不能重复决定！");
                return;
            }

            if (RadioButtonListJWConfirm.SelectedValue == "通过")
            {

                PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(147);//放号规则

                CertificateOB OldCert = CertificateDAL.GetObject(_CertificateMoreMDL.CertificateID.Value);//现有A本

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    //更新申请表
                    _CertificateMoreMDL.ConfirmDate = DateTime.Now;   //审核日期
                    _CertificateMoreMDL.ConfirmMan = PersonName;
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.Decided;  //状态
                    _CertificateMoreMDL.ConfirmAdvise = EnumManager.CertificateMoreCheckResult.CheckPass;
                    CertificateMoreDAL.Update(tran, _CertificateMoreMDL);
                    ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;

                    //创建证书
                    CertificateOB ceron = new CertificateOB();
                    ceron.ExamPlanID = -400;//考试计划ID
                    ceron.WorkerID = OldCert.WorkerID;//从业人员ID
                    ceron.CertificateCode = _CertificateMoreMDL.CertificateCodeMore;//证书编号               
                    ceron.WorkerName = OldCert.WorkerName;//姓名
                    ceron.WorkerCertificateCode = OldCert.WorkerCertificateCode;//证件号码
                    ceron.PostTypeID = OldCert.PostTypeID;//岗位
                    ceron.PostID = OldCert.PostID;//工种
                    ceron.PostTypeName = OldCert.PostTypeName;//岗位
                    ceron.PostName = OldCert.PostName;//工种
                    ceron.Sex = OldCert.Sex;//性别
                    ceron.Birthday = OldCert.Birthday;//出生日期
                    ceron.UnitName = _CertificateMoreMDL.UnitNameMore;//工作单位
                    ceron.UnitCode = _CertificateMoreMDL.UnitCodeMore;//组织机构代码                    
                    ceron.ConferUnit = "北京市住建委";                   
                    ceron.CreatePersonID = PersonID;//创建人ID
                    ceron.CreateTime = DateTime.Now;//创建时间
                    ceron.ModifyPersonID = PersonID;
                    ceron.ModifyTime = DateTime.Now;
                    ceron.CheckDate = ceron.CreateTime.Value.Date;
                    ceron.ConferDate = ceron.CheckDate;//发证日期
                    ceron.ValidStartDate = ceron.CheckDate;//证书有效期起
                    ceron.ValidEndDate = OldCert.ValidEndDate;//证书有效期止
                    ceron.Status = EnumManager.CertificateUpdateType.first;
                    ceron.SkillLevel = OldCert.SkillLevel;//技术职称(技术等级)
                    ceron.ApplyMan = PersonName;//业务办理申请人（报名、续期、变更时都改变）
                    ceron.TrainUnitName = OldCert.TrainUnitName;//培训点
                    ceron.Remark = string.Format("{0}日增发", ceron.CreateTime.Value.ToString("yyyy-MM-dd"));
                    ceron.Job = "法定代表人";

                    CertificateDAL.Insert(tran, ceron);
                    tran.Commit();


                    LabelCertificateCodeMore.Text = _CertificateMoreMDL.CertificateCodeMore;
                    LabelValidStartDateMore.Text = _CertificateMoreMDL.ValidStartDateMore.Value.ToString("yyyy-MM-dd");
                    LabelValidEndDateMore.Text = _CertificateMoreMDL.ValidEndDateMore.Value.ToString("yyyy-MM-dd");
                    //LabelCheckDate.Text = _CertificateMoreMDL.CheckDate.Value.ToString("yyyy-MM-dd HH:mm");
                    //LabelCheckMan.Text = _CertificateMoreMDL.CheckMan;
                    //LabelApplyStatus.Text = _CertificateMoreMDL.ApplyStatus;
                    //LabelCheckAdvise.Text = _CertificateMoreMDL.CheckAdvise;

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "A本增发决定失败！", ex);
                    return;
                }
                SetStep(_CertificateMoreMDL.ApplyStatus);

                UIHelp.WriteOperateLog(PersonName, UserID, "A本增发决定", string.Format("姓名：{0}，身份证号：{1}，决定结果：{2}，增发证书编号{3}。"
                   , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode
                   , EnumManager.CertificateMoreCheckResult.CheckPass, _CertificateMoreMDL.CertificateCodeMore));
                
                if (MoreTable != null)
                {
                    BindPrintDetail(CurrentIndex + 1);
                }
                else
                {
                    UIHelp.layerAlert(Page, "A本增发决定成功！", 6, 3000, "hideIfam(true);");
                }
            }
            else
            {
                try
                {
                    //更新申请表
                    _CertificateMoreMDL.ConfirmDate = DateTime.Now;   //审核日期
                    _CertificateMoreMDL.ConfirmMan = PersonName;
                    _CertificateMoreMDL.ApplyStatus = EnumManager.CertificateMore.SendBack;  //状态
                    _CertificateMoreMDL.ConfirmAdvise = TextBoxConfirmResult.Text;
                    CertificateMoreDAL.Update(_CertificateMoreMDL);
                    ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;
                    SetStep(_CertificateMoreMDL.ApplyStatus);
                }
                catch (Exception ex)
                {

                    UIHelp.WriteErrorLog(Page, "A本增发决定失败！", ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "A本增发决定", string.Format("姓名：{0}，身份证号：{1}，决定结果：{2}。"
             , _CertificateMoreMDL.WorkerName, _CertificateMoreMDL.WorkerCertificateCode, EnumManager.CertificateMoreCheckResult.CheckNoPass));

                if (MoreTable != null)
                {
                    BindPrintDetail(CurrentIndex + 1);
                }
                else
                {
                    UIHelp.layerAlert(Page, "驳回成功！", 6, 3000, "hideIfam(true);");
                }
            }
        }

          //绑定增发申申请详细
        protected void BindPrintDetail(int index)
        {
            if (index > MoreList.Count - 1)//打印结束
            {               
                CurrentIndex = 0;
                CheckBoxAutoConfirm.Checked = false;
               ButtonConfirm.Enabled = false;
               ButtonConfirm.CssClass = ButtonConfirm.Enabled == true ? "bt_large" : "bt_large btn_no";


                Timer1.Enabled = false;
                ViewState["MoreList"] = null;
                ViewState["MoreTable"] = null;
                ViewState["CertificateMoreMDL"] = null;
                LabelCheckCount.Text = string.Format("批量审核（已完成）");
                return;
            }
            CurrentIndex = index;
            DataRow dr = (ViewState["MoreTable"] as DataTable).Rows[index];

            long _ApplyID = Convert.ToInt64(dr["ApplyID"]);
            CertificateMoreMDL _CertificateMoreMDL = CertificateMoreDAL.GetObject(_ApplyID);

          

            if (_CertificateMoreMDL != null)
            {
                if (ValidPageViewLimit(RoleIDs, "CertifMoreConfirm.aspx") == true)
                {
                    TableJWConfirm.Visible = true;
                }
                ViewState["CertificateMoreMDL"] = _CertificateMoreMDL;
                UIHelp.SetData(DivEdit, _CertificateMoreMDL, true);
                SetStep(_CertificateMoreMDL.ApplyStatus);

                BindFile(ApplyID);

                BindCheckHistory(_CertificateMoreMDL.ApplyID.Value);

                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_CertificateMoreMDL.WorkerCertificateCode)));
            }
            else
            {
                Timer1.Enabled = false;
                Response.Clear();
                Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>无法读取申请单数据！</p>");
                Response.End();
            }

            if (CheckBoxAutoConfirm.Checked == true )//自动决定
            {
                if (Timer1.Enabled == false)
                {
                    Timer1.Interval = 2000;
                    Timer1.Enabled = true;
                }
            }
            else
            {
                if (Timer1.Enabled == true)
                {
                    Timer1.Enabled = false;
                }
            }
        }

         //批量分时决定
        protected void Timer1_Tick(object sender, EventArgs e)
        {
                ButtonConfirm_Click(sender, e);
        }
    }
}