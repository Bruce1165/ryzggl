using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;

namespace ZYRYJG.CertifManage
{
    public partial class CertificateMergeApply :BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertificateMergeApply.aspx|CertificateMergeList.aspx|ApplyQuerySLR.aspx";
            }
        }

        /// <summary>
        /// 待打印列证书ID集合
        /// </summary>
        public List<string> MergeList
        {
            get { return ViewState["MergeList"] as List<string>; }
        }

        public DataTable MergeTable
        {
            get { return ViewState["MergeTable"] as DataTable; }
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
                if (MergeList.Count < 1)
                {
                    LabelCheckCount.Text = string.Format("批量审核（已完成）");
                    ButtonCheck.Enabled = false;
                    ButtonUnitCheck.Enabled = false;
                }
                else
                {
                    LabelCheckCount.Text = string.Format("批量审核（共{0}单，当前第{1}单）", MergeList.Count, value + 1);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["r"]) == false)
                {
                    UIHelp.layerAlert(Page, "取消申报成功！", 6, 2000);
                }
                if (string.IsNullOrEmpty(Request["o"]) == false)//view
                {
                    long ApplyID = Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"]));
                    CertificateMergeMDL _CertificateMergeMDL = CertificateMergeDAL.GetObject(ApplyID);

                    if (_CertificateMergeMDL != null)//edit
                    {
                        ViewState["CertificateMergeMDL"] = _CertificateMergeMDL;
                        UIHelp.SetData(DivEdit, _CertificateMergeMDL, true);

                        SetButtonEnable(_CertificateMergeMDL.ApplyStatus);

                        BindCheckHistory(_CertificateMergeMDL.ApplyID.Value);

                        ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(string.Format("{0}", _CertificateMergeMDL.FacePhoto), string.Format("{0}", _CertificateMergeMDL.WorkerCertificateCode)));
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>无法读取申请单数据！</p>");
                        Response.End();
                    }
                }
                else if (PersonType == 2)//个人
                {
                    //c1、c2证书
                    DataTable dt = CommonDAL.GetDataTable(string.Format("select *  FROM [dbo].[CERTIFICATE] where  [WORKERCERTIFICATECODE]='{0}' and (PostID=6 or PostID=1123 )  and ValidEndDate > dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销' and [Status] <> '待审批' order by PostID desc", WorkerCertificateCode));

                    
                    //申请单
                    CertificateMergeMDL _CertificateMergeMDL = CertificateMergeDAL.GetObject(WorkerCertificateCode);
                    if (_CertificateMergeMDL != null && (
                                        (dt.Rows.Count != 2 && _CertificateMergeMDL.ApplyStatus == EnumManager.CertificateMergeStatus.Decided)//已办结
                                        || (dt.Rows.Count == 2 && _CertificateMergeMDL.ApplyStatus != EnumManager.CertificateMergeStatus.Decided)//在途
                                                        )
                        )                       
                    {
                        ViewState["CertificateMergeMDL"] = _CertificateMergeMDL;
                        UIHelp.SetData(DivEdit, _CertificateMergeMDL, true);
                        SetButtonEnable(_CertificateMergeMDL.ApplyStatus);
                        BindCheckHistory(_CertificateMergeMDL.ApplyID.Value);

                        ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(string.Format("{0}", _CertificateMergeMDL.FacePhoto), string.Format("{0}", _CertificateMergeMDL.WorkerCertificateCode)));
                    }
                    else//new
                    {
                        if (dt.Rows.Count != 2)
                        {
                            Response.Clear();
                            Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>您不满足申请合并C1、C2证书条件！</p>");
                            Response.End();
                            return;
                        }
                        if (dt.Rows[0]["PostID"].ToString() == dt.Rows[1]["PostID"].ToString())
                        {
                            Response.Clear();
                            Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>您不满足申请合并C1、C2证书条件！</p>");
                            Response.End();
                            return;
                        }

                        ViewState["C1C2"] = dt;

                        LabelWorkerCertificateCode.Text = dt.Rows[0]["WorkerCertificateCode"].ToString();
                        LabelWorkerName.Text = dt.Rows[0]["WorkerName"].ToString();
                        LabelSex.Text = dt.Rows[0]["Sex"].ToString();
                        LabelBirthday.Text = Convert.ToDateTime(dt.Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
                        LabelUnitName.Text = dt.Rows[0]["UnitName"].ToString();
                        LabelUnitCode.Text = dt.Rows[0]["UnitCode"].ToString();

                        LabelPostName1.Text = dt.Rows[0]["PostName"].ToString();
                        LabelCertificateCode1.Text = dt.Rows[0]["CertificateCode"].ToString();
                        LabelConferDate1.Text = Convert.ToDateTime(dt.Rows[0]["ConferDate"]).ToString("yyyy-MM-dd");
                        LabelValidEndDate1.Text = Convert.ToDateTime(dt.Rows[0]["VALIDENDDATE"]).ToString("yyyy-MM-dd");

                        LabelPostName2.Text = dt.Rows[1]["PostName"].ToString();
                        LabelCertificateCode2.Text = dt.Rows[1]["CertificateCode"].ToString();
                        LabelConferDate2.Text = Convert.ToDateTime(dt.Rows[1]["ConferDate"]).ToString("yyyy-MM-dd");
                        LabelValidEndDate2.Text = Convert.ToDateTime(dt.Rows[1]["VALIDENDDATE"]).ToString("yyyy-MM-dd");

                        SetButtonEnable("");

                        if (dt.Rows[0]["FacePhoto"] != null)
                        {
                            ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(string.Format("{0}", dt.Rows[0]["FacePhoto"]), string.Format("{0}", dt.Rows[0]["WorkerCertificateCode"])));
                        }
                    }

                }
                else if (Session["CertificateMergeList"] != null)//批量打印
                {
                    ViewState["MergeList"] = Session["CertificateMergeList"];
                    Session.Remove("CertificateMergeList");
                    ViewState["MergeTable"] = Session["CertificateMergeTable"];
                    Session.Remove("CertificateMergeTable");

                    tr_PatchCheck.Visible = true;
                    CheckBoxAutoPrint.Visible = true;
                    CheckBoxAutoPrintUnit.Visible = true;

                    if (MergeTable != null)
                    {
                        BindPrintDetail(0);
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>非法访问！</p>");
                    Response.End();
                }               
            }

            //Response.Expires = 0;
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            //Response.AddHeader("pragma", "no-cache");
            //Response.CacheControl = "no-cache";
        }

        //绑定带打印证书详细
        protected void BindPrintDetail(int index)
        {
            if (index > MergeList.Count - 1)//打印结束
            {               
                CurrentIndex = 0;
                CheckBoxAutoPrint.Checked = false;
                CheckBoxAutoPrintUnit.Checked = false;
               ButtonUnitCheck.Enabled=false;
               ButtonCheck.Enabled = false;
               ButtonCheck.CssClass = ButtonCheck.Enabled == true ? "bt_large" : "bt_large btn_no";
               ButtonUnitCheck.CssClass = ButtonUnitCheck.Enabled == true ? "bt_large" : "bt_large btn_no";

                Timer1.Enabled = false;
                ViewState["MergeList"] = null;
                ViewState["MergeTable"] = null;
                ViewState["CertificateMergeMDL"] = null;
                LabelCheckCount.Text = string.Format("批量审核（已完成）");
                return;
            }
            CurrentIndex = index;
            DataRow dr = (ViewState["MergeTable"] as DataTable).Rows[index];

            long ApplyID = Convert.ToInt64(dr["ApplyID"]);
            CertificateMergeMDL _CertificateMergeMDL = CertificateMergeDAL.GetObject(ApplyID);

          

            if (_CertificateMergeMDL != null)
            {
                ViewState["CertificateMergeMDL"] = _CertificateMergeMDL;
                UIHelp.SetData(DivEdit, _CertificateMergeMDL, true);

                SetButtonEnable(_CertificateMergeMDL.ApplyStatus);

                BindCheckHistory(_CertificateMergeMDL.ApplyID.Value);

                ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath(string.Format("{0}", _CertificateMergeMDL.FacePhoto), string.Format("{0}", _CertificateMergeMDL.WorkerCertificateCode)));
            }
            else
            {
                Timer1.Enabled = false;
                Response.Clear();
                Response.Write("<p style='font-size:20px;line-height:400%;text-align:center'>无法读取申请单数据！</p>");
                Response.End();
            }

            if (CheckBoxAutoPrint.Checked == true || CheckBoxAutoPrintUnit.Checked==true)//自动打印
            {
                if (Timer1.Enabled == false)
                {
                    Timer1.Interval = 5000;
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

        //批量分时审批
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (PersonType == 3)//企业审核
            {
                ButtonUnitCheck_Click(sender, e);
            }
            else//建委审核
            {
                ButtonCheck_Click(sender, e);
            }
        }

        //个人提交企业确认
        protected void btnSave_Click(object sender, EventArgs e)
        {
             DataTable dt =null;
             if (ViewState["CertificateMergeMDL"] == null)
             {
                 dt = ViewState["C1C2"] as DataTable;

                 //    如果不在同一单位情况，不允许发起合并申请，提示先变更到同一家单位。
                 if (dt.Rows[0]["UnitCode"].ToString() != dt.Rows[1]["UnitCode"].ToString())
                 {
                     UIHelp.layerAlert(Page, "你的证书不在同一单位，不允许发起合并申请，请先变更到同一家单位", 2, 0);
                     return;
                 }
                 //如果证书存在未办结的变更或续期，提示先办结业务或取消原业务申请再办理合并。
                 string sql = @"select [CERTIFICATECODE],'变更' ApplyType
                                  FROM [dbo].[VIEW_CERTIFICATECHANGE]
                                  where ([CERTIFICATECODE]='{0}' or [CERTIFICATECODE]='{1}') and [STATUS]<>'已告知'
                                  UNION 
                                  select [CERTIFICATECODE],'续期' ApplyType
                                  FROM [dbo].[VIEW_CERTIFICATECONTINUE]
                                  where ([CERTIFICATECODE]='{0}' or [CERTIFICATECODE]='{1}') and [STATUS]<>'已决定'";
                 DataTable checkRtn = CommonDAL.GetDataTable(string.Format(sql, dt.Rows[0]["CertificateCode"], dt.Rows[1]["CertificateCode"]));
                 if (checkRtn != null && checkRtn.Rows.Count > 0)
                 {
                     UIHelp.layerAlert(Page, string.Format("你的证书{0}存在一个未办结的{1}申请，请先变办结或取消申请才能办理证书合并。", checkRtn.Rows[0]["CERTIFICATECODE"], checkRtn.Rows[0]["ApplyType"]), 2, 0);
                     return;
                 }

                 string lockReason = "";
                 bool lockStatus = CertificateLockDAL.GetCertificateLockStatus((long)dt.Rows[0]["CertificateID"], ref lockReason);
                 if (lockStatus == true)//已加锁，内部锁
                 {
                     UIHelp.layerAlert(Page, string.Format("证书处于锁定中，不允许合并。锁定原因：{0}", lockReason));
                     return;
                 }
                 lockStatus = CertificateLockDAL.GetCertificateLockStatus((long)dt.Rows[1]["CertificateID"], ref lockReason);
                 if (lockStatus == true)//已加锁，内部锁
                 {
                     UIHelp.layerAlert(Page, string.Format("证书处于锁定中，不允许合并。锁定原因：{0}", lockReason));
                     return;
                 }

                 if (dt.Rows[0]["ZZUrlUpTime"] == DBNull.Value 
                     || Convert.ToDateTime(dt.Rows[0]["ZZUrlUpTime"]) < Convert.ToDateTime(dt.Rows[0]["CheckDate"])
                     || dt.Rows[1]["ZZUrlUpTime"] == DBNull.Value
                     || Convert.ToDateTime(dt.Rows[1]["ZZUrlUpTime"]) < Convert.ToDateTime(dt.Rows[1]["CheckDate"])
                     )
                 {
                     UIHelp.layerAlert(Page, "待合并证书上一次业务办理结果尚未上传到国家（没有成功生成电子证书），无法申请新业务，请到电子证书下载页面查看原因，按要求整改。");
                     return;
                 }
             }

            CertificateMergeMDL _CertificateMergeMDL = (ViewState["CertificateMergeMDL"] == null ? new CertificateMergeMDL() : ViewState["CertificateMergeMDL"] as CertificateMergeMDL);

            UIHelp.GetData(DivEdit, _CertificateMergeMDL);

            _CertificateMergeMDL.ApplyDate = DateTime.Now;
            _CertificateMergeMDL.ApplyStatus = EnumManager.CertificateMergeStatus.WaitUnitCheck;

            try
            {
                if (_CertificateMergeMDL.ApplyID.HasValue)//update
                {
                    _CertificateMergeMDL.ModifyPersonID = PersonID;
                    _CertificateMergeMDL.ModifyTime = DateTime.Now;

                    _CertificateMergeMDL.UnitAdvise = "";
                    _CertificateMergeMDL.UnitCheckTime = null;
                    _CertificateMergeMDL.CheckAdvise = "";
                    _CertificateMergeMDL.CheckDate = null;
                    _CertificateMergeMDL.CheckMan = "";

                    CertificateMergeDAL.Update(_CertificateMergeMDL);
                }
                else//new
                {                   
                    _CertificateMergeMDL.CreatePersonID = PersonID;
                    _CertificateMergeMDL.CreateTime = DateTime.Now;
                    _CertificateMergeMDL.WorkerID = Convert.ToInt64(dt.Rows[0]["WorkerID"]);
                    _CertificateMergeMDL.UnitCode = dt.Rows[0]["UnitCode"].ToString();
                    _CertificateMergeMDL.ConferUnit = dt.Rows[0]["ConferUnit"].ToString();
                    _CertificateMergeMDL.CertificateID1 = Convert.ToInt64(dt.Rows[0]["CertificateID"]);
                    _CertificateMergeMDL.ValidStartDate1 = Convert.ToDateTime(dt.Rows[0]["ValidStartDate"]);
                    _CertificateMergeMDL.CertificateID2 = Convert.ToInt64(dt.Rows[1]["CertificateID"]);
                    _CertificateMergeMDL.ValidStartDate2 = Convert.ToDateTime(dt.Rows[1]["ValidStartDate"]);
                    _CertificateMergeMDL.ApplyMan = PersonName;
                    if (dt.Rows[0]["FacePhoto"] != null)
                    {
                        _CertificateMergeMDL.FacePhoto = dt.Rows[0]["FacePhoto"].ToString();
                    }
                    else if(dt.Rows[1]["FacePhoto"] != null)
                    {
                        _CertificateMergeMDL.FacePhoto = dt.Rows[1]["FacePhoto"].ToString();
                    }
                    CertificateMergeDAL.Insert(_CertificateMergeMDL);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "个人提交证书合并申请到单位失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "个人提交证书合并申请到单位", string.Format("C1证书编号：{0}，C2证书编号：{1}。", LabelCertificateCode1.Text, LabelCertificateCode2.Text));
            UIHelp.layerAlert(Page, "提交申请成功！", 6, 2000);
            SetButtonEnable(_CertificateMergeMDL.ApplyStatus);
            BindCheckHistory(_CertificateMergeMDL.ApplyID.Value);
        }
        //个人取消申报
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateMergeDAL.Delete(ViewState["CertificateMergeMDL"] as CertificateMergeMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "取消证书合并申请失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "个人取消证书合并申请", string.Format("C1证书编号：{0}，C2证书编号：{1}。", LabelCertificateCode1.Text, LabelCertificateCode2.Text));
            Response.Redirect(string.Format("./CertificateMergeApply.aspx?r={0}", Guid.NewGuid()));
        }
        //企业提交建委审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            CertificateMergeMDL o =  CertificateMergeDAL.GetObject((ViewState["CertificateMergeMDL"] as CertificateMergeMDL).ApplyID.Value);
            if (o.ApplyStatus == EnumManager.CertificateMergeStatus.WaitUnitCheck)
            {
                //CertificateMergeMDL o = ViewState["CertificateMergeMDL"] as CertificateMergeMDL;
                o.UnitCheckTime = DateTime.Now;
                o.UnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见
                o.ApplyStatus = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.CertificateMergeStatus.Applyed : EnumManager.CertificateMergeStatus.SendBack);
                try
                {
                    CertificateMergeDAL.Update(o);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "单位审核证书合并申请失败！", ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "单位审核证书合并申请", string.Format("C1证书编号：{0}，C2证书编号：{1}。", LabelCertificateCode1.Text, LabelCertificateCode2.Text));

            }
            if (MergeTable != null)
            {
                BindPrintDetail(CurrentIndex + 1);
            }
            else
            {
                UIHelp.layerAlert(Page, "审核成功！", 6, 3000, "hideIfam(true);");
            }
        }
        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
           
            CertificateMergeMDL o =  CertificateMergeDAL.GetObject((ViewState["CertificateMergeMDL"] as CertificateMergeMDL).ApplyID.Value);
            if (o.ApplyStatus == EnumManager.CertificateMergeStatus.Applyed)
            {
                //CertificateMergeMDL o = ViewState["CertificateMergeMDL"] as CertificateMergeMDL;
                o.CheckDate = DateTime.Now;
                o.CheckMan = UserName;
                o.CheckAdvise = (RadioButtonListJWCheck.SelectedValue == "通过" ? "通过" : TextBoxCheckResult.Text);//建委意见
                o.ApplyStatus = (RadioButtonListJWCheck.SelectedValue == "通过" ? EnumManager.CertificateMergeStatus.Decided : EnumManager.CertificateMergeStatus.SendBack);

                PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(1125);

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();//事务对象
                try
                {
                    //1、创建新证书      
                    CertificateOB ceron = new CertificateOB();
                    if (o.CheckAdvise == "通过")
                    {
                        ceron.ExamPlanID = EnumManager.CertificateExamPlanID.CertificateMerge;//考试计划ID
                        ceron.WorkerID = o.WorkerID;//从业人员ID
                        ceron.WorkerName = o.WorkerName;//姓名
                        ceron.WorkerCertificateCode = o.WorkerCertificateCode;//证件号码                        
                        ceron.Sex = o.Sex;   //性别
                        ceron.Birthday = o.Birthday;  //出身日期
                        ceron.UnitName = o.UnitName;//工作单位
                        ceron.UnitCode = o.UnitCode;//组织机构代码
                        ceron.PostTypeID = 1;//岗位
                        ceron.PostID = 1125;//工种
                        ceron.PostTypeName = "安全生产考核三类人员";//岗位
                        ceron.PostName = "综合类专职安全生产管理人员";//工种                
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, tran);//证书编号
                        ceron.ValidStartDate = o.CheckDate.Value.Date;//证书有效期起
                        ceron.ValidEndDate = (o.ValidEndDate1 > o.ValidEndDate2 ? o.ValidEndDate1 : o.ValidEndDate2);//证书有效期止
                        ceron.ConferDate = ceron.ValidStartDate;//发证日期
                        ceron.ConferUnit = o.ConferUnit; //发证单位
                        ceron.CreatePersonID = PersonID;//创建人ID
                        ceron.CreateTime = o.CheckDate;//创建时间
                        ceron.ModifyPersonID = PersonID;
                        ceron.ModifyTime = o.CheckDate; 
                        ceron.CheckDate = o.CheckDate;
                        ceron.Status = EnumManager.CertificateUpdateType.first;
                        ceron.Remark = string.Format("双证合并（{0}，{1}）", o.CertificateCode1, o.CertificateCode2);
                        ceron.ApplyMan = o.ApplyMan;
                        ceron.FacePhoto = (o.FacePhoto == "" ? null : o.FacePhoto);
                        CertificateDAL.Insert(tran, ceron);

                        o.NewCertificateID = ceron.CertificateID;
                        o.NewCertificateCode = ceron.CertificateCode;
                    }

                    //2、更新申请表               
                    CertificateMergeDAL.Update(tran, o);

                    //3、注销原证书
                    if (o.CheckAdvise == "通过")
                    {
                        //根据证书id向历史表插入历史数据
                        CertificateHistoryDAL.InsertChangeHistory(tran,o.CertificateID1.Value);

                        CertificateOB c1 = CertificateDAL.GetObject(o.CertificateID1.Value);
                        c1.Status = EnumManager.CertificateUpdateType.Logout;
                        c1.Remark = string.Format("双证合并注销，新证书编号{0}", ceron.CertificateCode);
                        c1.ModifyPersonID = PersonID;
                        c1.ModifyTime = o.CheckDate;
                        c1.CheckDate = o.CheckDate;
                        c1.CheckMan = o.CheckMan;
                        CertificateDAL.Update(tran, c1);

                        CertificateHistoryDAL.InsertChangeHistory(tran, o.CertificateID2.Value);

                        CertificateOB c2 = CertificateDAL.GetObject(o.CertificateID2.Value);
                        c2.Status = EnumManager.CertificateUpdateType.Logout;
                        c2.Remark = string.Format("双证合并注销，新证书编号{0}", ceron.CertificateCode);
                        c2.ModifyPersonID = PersonID;
                        c2.ModifyTime = o.CheckDate;
                        c2.CheckDate = o.CheckDate;
                        c2.CheckMan = o.CheckMan;
                        CertificateDAL.Update(tran, c2);
                    }
                    tran.Commit();//提交事务

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "建委审核证书合并申请失败！", ex);
                    return;
                }
                UIHelp.WriteOperateLog(PersonName, UserID, "建委审核证书合并申请", string.Format("C1证书编号：{0}，C2证书编号：{1}。审核结果：{2}", LabelCertificateCode1.Text, LabelCertificateCode2.Text, o.ApplyStatus));

            }
            if (MergeTable != null)
            {
                BindPrintDetail(CurrentIndex + 1);
            }
            else
            {
                UIHelp.layerAlert(Page, "审核成功！", 6, 3000, "hideIfam(true);");
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

            if (IfExistRoleID("0") == true)
            {
                divCtl.Visible = true;
            }
            
            switch (ApplyStatus)
            {
                case "":
                    btnSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = false;//删除

                    break;

                case EnumManager.CertificateMergeStatus.WaitUnitCheck:
                    btnSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = true;//删除
                    if (IfExistRoleID("2") == true)
                    {
                        TableUnitCheck.Visible = true;
                    }
                    break;
                case EnumManager.CertificateMergeStatus.SendBack:
                    btnSave.Enabled = true;//保 存
                    ButtonDelete.Enabled = true;//删除 

                    break;
                case EnumManager.CertificateMergeStatus.Applyed://已申请
                    if (IfExistRoleID("10") == true || IfExistRoleID("1") == true)
                    {
                        TableJWCheck.Visible = true;
                    }
                       btnSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = true;//删除
                    break;

                case EnumManager.CertificateMergeStatus.Decided://已决定

                    TableJWCheck.Visible = false;
                       btnSave.Enabled = false;//保 存
                    ButtonDelete.Enabled = false;//删除
                    break;
                default:
                    btnSave.Enabled = false;//保 存                 
                    ButtonDelete.Enabled = false;//删除
                    break;
            }

            btnSave.CssClass = btnSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";

        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ApplyID)
        {
            DataTable dt = CertificateMergeDAL.GetCheckHistoryList(ApplyID);
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_填报中.Attributes["class"] = step_填报中.Attributes["class"].Replace(" green", "");
            step_待单位确认.Attributes["class"] = step_待单位确认.Attributes["class"].Replace("green", "");
            step_已申请.Attributes["class"] = step_已申请.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
            step_已办结.Attributes["class"] = step_已办结.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {

                case EnumManager.CertificateMergeStatus.SendBack:
                    step_填报中.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMergeStatus.WaitUnitCheck:
                    step_待单位确认.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMergeStatus.Applyed:
                    step_已申请.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateMergeStatus.Decided:
                    try
                    {
                        CertificateMergeMDL _CertificateMergeMDL = (CertificateMergeMDL)ViewState["CertificateMergeMDL"];
                        CertificateOB _CertificateOB = CertificateDAL.GetCertificateOBObject(_CertificateMergeMDL.NewCertificateCode);
                        if (_CertificateOB != null && _CertificateOB.ZZUrlUpTime > _CertificateMergeMDL.CheckDate)
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
    }
}