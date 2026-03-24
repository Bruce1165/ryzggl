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


namespace ZYRYJG.CheckMgr
{
    public partial class QuestionFeedBackDetail : BasePage
    {
        /// <summary>
        /// 监管填报数据ID
        /// </summary>
        protected string DataID
        {
            get { return ViewState["CheckFeedBackMDL"] == null ? "" : (ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL).DataID; }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "QuestionFeedBack.aspx|CheckTaskAccept.aspx|CheckTaskCheck.aspx|CheckTaskConfirm.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {        
                if(PersonType ==2)
                {
                    trPhone.Visible = false;
                }

                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    string _Data = Utility.Cryptography.Decrypt(Request["o"]);
                    CheckFeedBackMDL o = CheckFeedBackDAL.GetObject(_Data);
                    ViewState["CheckFeedBackMDL"] = o;

                    UIHelp.SetData(EditTable, o, false);

                    LabelWorkerRerpotTime.Text = o.WorkerRerpotTime.HasValue == true ? o.WorkerRerpotTime.Value.ToString("yyyy-MM-dd HH:mm") : "尚未反馈";
                    tdQuestion.InnerHtml = string.Format(
                        @"<p>
                            <span class='black'><b>数据来源：</b>住房和城乡建设部（{3}）</span><br />
                            <b>社保情况：</b>{0}<br />
                            <b>公积金情况：</b>{1}<br />
                            <b>社保单位：</b>{2}                          
                        </p>", o.SheBaoCase,o.GongjijinCase,o.ShebaoUnit,o.SourceTime.Value.ToString("yyyy年M月d日"));

                    if (string.IsNullOrEmpty(o.ProjectCase) == false)
                    {
                        tdZSSD.InnerText = o.ProjectCase;
                    }
                    else
                    {
                        tdZSSD.InnerText = "无";
                    }

                    if(string.IsNullOrEmpty(o.CaseDesc)==false)
                    {
                        RadioButtonSheBao.Checked = true;//默认

                        if (RadioButtonCancel.Text == o.CaseDesc) RadioButtonCancel.Checked = true;
                        if (RadioButtonSheBao.Text == o.CaseDesc) RadioButtonSheBao.Checked = true;
                        if (RadioButton21.Text == o.CaseDesc) RadioButton21.Checked = true;
                        if (RadioButton22.Text == o.CaseDesc) RadioButton22.Checked = true;
                        if (RadioButton23.Text == o.CaseDesc) RadioButton23.Checked = true;
                        if (RadioButton24.Text == o.CaseDesc) RadioButton24.Checked = true;
                        if (RadioButton25.Text == o.CaseDesc) RadioButton25.Checked = true;
                        if (RadioButton26.Text == o.CaseDesc) RadioButton26.Checked = true;


                    }
                    DateTime sbTime = o.WorkerRerpotTime.HasValue?o.WorkerRerpotTime.Value:o.CreateTime.Value;
                    BindShebao(o.WorkerCertificateCode, sbTime.AddMonths(-2).ToString("yyyyMM"), sbTime.AddMonths(-1).ToString("yyyyMM"));
                    ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                    BindFile(_Data);
                    BindCheckHistory(_Data);
                    SetButtonEnable(o.DataStatus);


                    if (string.IsNullOrEmpty(o.BackReason) == false && o.DataStatus != EnumManager.CheckFeedStatus.已驳回 && o.DataStatusCode < 7
                        && o.CheckResult !="不通过")
                    {
                        RadGridCheckHistory.MasterTableView.Caption = string.Format("<apan style='color:red'>【上次被{0}退回】原因：{1}</span>",o.BackUnit, o.BackReason);
                    }

                    if(o.DataStatus==EnumManager.CheckFeedStatus.待决定 && o.CheckResult=="不通过")
                    {
                        RadioButtonListDecide.Items.FindByValue("不通过").Selected = true;
                        TextBoxConfirmDesc.Text = o.BackReason;
                    }
                }
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")//上传附件刷新
            {
                BindFile(DataID);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonSave.ClientID), true);
            }
        }

        /// <summary>
        /// 绑定社保比对结果
        /// </summary>
        /// <param name="CertificateCode">身份证号</param>
        /// <param name="JFYF_from">比对月份起始</param>
        /// <param name="JFYF_to">比对月份起始结束</param>
        protected void BindShebao(string CertificateCode, string JFYF_from, string JFYF_to)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format(
            @"select * from
            (
                SELECT [CertificateCode],[WorkerName],[CreditCode],[ENT_Name],[JFYF],[XZName],[XZCode],convert(varchar(11),[CJSJ],23) as 'CJSJ'
                FROM [dbo].[SheBao]
                where [CertificateCode]='{0}' and [JFYF] between  {1} and  {2}
            ) p
            PIVOT(MAX([XZName]) FOR [XZCode] IN ([01],[02],[03],[04],[05])) AS T",CertificateCode,  JFYF_from,  JFYF_to));
            RadGridSheBao.DataSource = dt;
            RadGridSheBao.DataBind();
        }

        //提交反馈
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (RadioButtonCancel.Checked == false
                && RadioButtonSheBao.Checked == false
                && RadioButton21.Checked == false
                && RadioButton22.Checked == false
                && RadioButton23.Checked == false
                && RadioButton24.Checked == false
                && RadioButton25.Checked == false
                && RadioButton26.Checked == false
                )
            {
                UIHelp.layerAlert(Page, "请选择一种监管问题反馈类型。");
                return;
            }            

            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
            if (ButtonSubmit.Text == "提交审核")
            {
                #region 必须上传附件集合

                //必须上传附件集合
                System.Collections.Hashtable fj = new System.Collections.Hashtable { };

                //1、本人已完成整改，请上传以下相关材料
                if (RadioButtonCancel.Checked == true)
                {
                    fj.Add(EnumManager.FileDataTypeName.当前证书注册状态截图, 0);
                    fj.Add(EnumManager.FileDataTypeName.个人承诺, 0);
                }
                else if (RadioButtonSheBao.Checked == true)
                {
                    fj.Add(EnumManager.FileDataTypeName.社保扫描件, 0);
                    fj.Add(EnumManager.FileDataTypeName.当前证书注册状态截图, 0);
                    fj.Add(EnumManager.FileDataTypeName.个人承诺, 0);
                }
                else
                {
                    fj.Add(EnumManager.FileDataTypeName.符合规定情形的相关证明, 0);
                }

                //已上传附件集合
                DataTable dt = ApplyFileDAL.GetListByApplyID(DataID);

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
                o.WorkerRerpotTime = DateTime.Now;
                if (o.Country == "市住建委")//提交到市住建委
                {
                    o.DataStatus = EnumManager.CheckFeedStatus.待复审;
                    o.DataStatusCode = EnumManager.CheckFeedStatusCode.待复审;
                }
                else//提交到区县
                {
                    o.DataStatus = EnumManager.CheckFeedStatus.待审查;
                    o.DataStatusCode = EnumManager.CheckFeedStatusCode.待审查;
                }
                o.AcceptCountry = null;
                o.AcceptTime = null;
                o.AcceptMan = null;
                o.AcceptResult = null;
                o.CountryReportTime = null;
                o.CountryReportCode = null;
                o.CheckTime = null;
                o.CheckMan = null;
                o.CheckResult = null;
                o.ConfirmTime = null;
                o.ConfirmMan = null;
                o.ConfirmResult = null;
            }
            else
            {
                o.WorkerRerpotTime = null;
                o.DataStatus = EnumManager.CheckFeedStatus.待反馈;
                o.DataStatusCode = EnumManager.CheckFeedStatusCode.待反馈;
            }
            o.CaseDesc = (RadioButtonCancel.Checked == true ? RadioButtonCancel.Text :
                        RadioButtonSheBao.Checked == true ? RadioButtonSheBao.Text :
                        RadioButton21.Checked == true ? RadioButton21.Text :
                        RadioButton22.Checked == true ? RadioButton22.Text :
                        RadioButton23.Checked == true ? RadioButton23.Text :
                        RadioButton24.Checked == true ? RadioButton24.Text :
                        RadioButton25.Checked == true ? RadioButton25.Text :
                        RadioButton26.Checked == true ? RadioButton26.Text : "");
            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                LabelWorkerRerpotTime.Text = o.WorkerRerpotTime.HasValue == true ? o.WorkerRerpotTime.Value.ToString("yyyy-MM-dd HH:mm") : "尚未反馈";
                if (ButtonSubmit.Text == "提交审核")
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "个人提交监管反馈", string.Format("姓名：{0}，DataID：{1}。", o.WorkerName, o.DataID));
                    UIHelp.layerAlert(Page, string.Format("提交成功。审核机构为：“{0}”", (string.IsNullOrEmpty(o.Country) == true ? "市建委" : o.Country)));
                }
                else
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "个人取消提交监管反馈", string.Format("姓名：{0}，DataID：{1}。", o.WorkerName, o.DataID));
                    UIHelp.layerAlert(Page, "取消提交成功。");
                }               

                SetButtonEnable(o.DataStatus);
                BindCheckHistory(o.DataID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "监管反馈提交审查失败！", ex);
            }
            //刷新父页面
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refreshParent", "var isfresh=true;", true);
        }

        //保存反馈
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (RadioButtonCancel.Checked == false
                && RadioButtonSheBao.Checked == false
                && RadioButton21.Checked == false
                && RadioButton22.Checked == false
                && RadioButton23.Checked == false
                && RadioButton24.Checked == false
                && RadioButton25.Checked == false
                && RadioButton26.Checked == false
                )
            {
                UIHelp.layerAlert(Page, "请选择一种监管问题反馈类型。");
                return;
            }

            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
            o.CaseDesc = (RadioButtonCancel.Checked == true ? RadioButtonCancel.Text :
                        RadioButtonSheBao.Checked == true ? RadioButtonSheBao.Text :
                        RadioButton21.Checked == true ? RadioButton21.Text :
                        RadioButton22.Checked == true ? RadioButton22.Text :
                        RadioButton23.Checked == true ? RadioButton23.Text :
                        RadioButton24.Checked == true ? RadioButton24.Text :
                        RadioButton25.Checked == true ? RadioButton25.Text :
                        RadioButton26.Checked == true ? RadioButton26.Text : "");
            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                SetButtonEnable(o.DataStatus);
                UIHelp.WriteOperateLog(UserName, UserID, "个人编辑保存监管反馈", string.Format("姓名：{0}，DataID：{1}。", o.WorkerName, o.DataID));
                UIHelp.layerAlert(Page, "保存成功。",6,3000);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存监管反馈失败！", ex);
            }
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_待反馈.Attributes["class"] = step_待反馈.Attributes["class"].Replace(" green", "");
            step_待审查.Attributes["class"] = step_待审查.Attributes["class"].Replace("green", "");
            step_待复审.Attributes["class"] = step_待复审.Attributes["class"].Replace(" green", "");
            step_待决定.Attributes["class"] = step_待决定.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");
            switch (ApplyStatus)
            {
                case EnumManager.CheckFeedStatus.待反馈:
                case EnumManager.CheckFeedStatus.已驳回:
                    step_待反馈.Attributes["class"] += " green";
                    break;
                case EnumManager.CheckFeedStatus.待审查:
                    step_待审查.Attributes["class"] += " green";
                    break;
                case EnumManager.CheckFeedStatus.待复审:
                    step_待复审.Attributes["class"] += " green";
                    break;
                case EnumManager.CheckFeedStatus.待决定:
                    step_待决定.Attributes["class"] += " green";
                    break;
                case EnumManager.CheckFeedStatus.已决定:
                    step_已决定.Attributes["class"] += " green";
                    break;               
                default:
                    step_待反馈.Attributes["class"] += " green";
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
                case EnumManager.CheckFeedStatus.待反馈:
                case EnumManager.CheckFeedStatus.已驳回:
                    ButtonSave.Enabled = true;
                    ButtonSubmit.Enabled = true;
                    ButtonSubmit.Text = "提交审核";                  

                    //个人登录后
                    if (IfExistRoleID("0") == true)
                    {
                        trFuJan.Visible = true;
                        divGR.Visible = true;
                    }
                    break;
                case EnumManager.CheckFeedStatus.待审查:
                    //个人
                    ButtonSave.Enabled = false;
                      ButtonSubmit.Enabled = true;
                      ButtonSubmit.Text = "取消提交";

                      if (IfExistRoleID("0") == true)//个人
                    {
                        trFuJan.Visible = false;
                        divGR.Visible = true;
                    }
                      //区县审查权限
                      //if (IfExistRoleID("3") == true)
                      //{
                      //    divQX.Visible = true;
                      //}

                      if (ValidPageViewLimit(RoleIDs, "CheckTaskAccept.aspx") == true)
                      {
                          divQX.Visible = true;
                      }

                    break;
                case EnumManager.CheckFeedStatus.待复审:
                    if (IfExistRoleID("0") == true)//个人
                    {
                       CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
                       if (o.Country == "市住建委")//提交到市住建委
                       {
                           ButtonSave.Enabled = false;
                           ButtonSubmit.Enabled = true;
                           ButtonSubmit.Text = "取消提交";
                           trFuJan.Visible = false;
                           divGR.Visible = true;
                       }
                    }


                    //市级审核
                    if (ValidPageViewLimit(RoleIDs, "CheckTaskCheck.aspx") == true)
                    {
                        divCheck.Visible = true;
                        ButtonCheck.Text = "确认提交";
                        CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
                        if(string.IsNullOrEmpty(o.PassType)==false)
                        {

                        }
                    }
                  
                    break;
                case EnumManager.CheckFeedStatus.待决定:
                    ////注册室管理员
                    //if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    //{
                    //    UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                    //    divSendBack.Visible = true;//后退
                    //}

                    //市级决定权限
                    if (ValidPageViewLimit(RoleIDs, "CheckTaskConfirm.aspx") == true)
                    {
                        ButtonDecide.Text = "确认提交";
                        divDecide.Visible = true;
                    }
                  
                    break;
                //case EnumManager.CheckFeedStatus.已决定:
                //    //注册室管理员
                //    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                //    {
                //        if (string.IsNullOrEmpty(o.NoticeCode) == true)//初始、续期注册尚未公告才能后退，如果保存了公告，需要人工后退。
                //        {
                //            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                //            divSendBack.Visible = true;//后退
                //        }
                //    }
                    //break;
             
                default:
                    trFuJan.Visible = false;
                    divGR.Visible = false;
                    break;
            }

            if (ApplyStatus == EnumManager.CheckFeedStatus.待反馈 || ApplyStatus == EnumManager.CheckFeedStatus.已驳回)
            {
                DivCaseDesc.InnerHtml = "";
                DivCheckHelp.Style.Add("display", "none");
            }
            else
            {
                string CaseDesc = (ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL).CaseDesc;
                if (string.IsNullOrEmpty(CaseDesc) ==false && CaseDesc.Contains("本人满足不属于以下六类“挂证”行为情况")==true)
                {
                    DivCaseDesc.InnerHtml = string.Format("<b>个人反馈：</b>本人满足不属于以下六类“挂证”行为情况，{0}相关材料见右侧附件。", CaseDesc);
                }
                else
                {
                     DivCaseDesc.InnerHtml = string.Format("个人反馈：{0}相关材料见右侧附件。", CaseDesc);                   
                }
               
                DivCheckHelp.Style.Add("display", "block");
            }

            //市级决定权限
            if (ValidPageViewLimit(RoleIDs, "CheckTaskConfirm.aspx") == true)
            {
                SetReturnStatusList(RadComboBoxReturnApplyStatus, ApplyStatus);
                divSendBack.Visible = true;//后退
            }

            if ((ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL).LastReportTime< DateTime.Now.AddDays(-1))
            {
                trFuJan.Visible = false;
                divGR.Visible = false;
            }

            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonSubmit.CssClass = ButtonSubmit.Enabled == true ? "bt_large" : "bt_large btn_no";
            
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(string ApplyID)
        {
            if (IfExistRoleID("0") == true || IfExistRoleID("2") == true)
            {
                DataTable dt = CheckFeedBackDAL.GetCheckHistoryListOfWorker(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
            else
            {
                DataTable dt = CheckFeedBackDAL.GetCheckHistoryList(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
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
                UIHelp.WriteErrorLog(Page, "删除监管反馈附件失败！", ex);
                return;
            }
            BindFile(ApplyID);
            UIHelp.WriteOperateLog(UserName, UserID, "删除监管反馈附件", string.Format("监管记录ID：{0}，附件ID：{1}。", ApplyID, FileID));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
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

        //区县审查
        protected void BtnCountryCheck_Click(object sender, EventArgs e)
        {
            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
            o.AcceptTime = DateTime.Now;
            o.AcceptMan = UserName;
            o.DataStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.CheckFeedStatus.待复审:EnumManager.CheckFeedStatus.已驳回;
            o.DataStatusCode = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.CheckFeedStatusCode.待复审 : EnumManager.CheckFeedStatusCode.已驳回;
            o.AcceptResult = RadioButtonListApplyStatus.SelectedValue;
            if (RadioButtonListApplyStatus.SelectedValue != "通过")
            {
                o.BackReason = TextBoxApplyGetResult.Text.Trim();
                o.BackUnit = o.Country;
            }
          
            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                SetButtonEnable(o.DataStatus);
                BindCheckHistory(o.DataID);
                UIHelp.WriteOperateLog(UserName, UserID, "区县审核监管反馈", string.Format("姓名：{0}，DataID：{1}，审批结果：{2}，{3}。", o.WorkerName, o.DataID, o.AcceptResult,
                    RadioButtonListApplyStatus.SelectedValue != "通过"? TextBoxApplyGetResult.Text.Trim():""));
                UIHelp.ParentAlert(Page, "审核成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县审核监管反馈失败！", ex);
                return;
            }          
        }

        //市级复审
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            if(RadioButtonListExamineResult.SelectedValue == "通过" && RadioButtonListPassType.SelectedIndex == -1)
            {
                UIHelp.layerAlert(Page, "请选择一个合格类型");
                return;
            }
            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
            o.CheckTime = DateTime.Now;
            o.CheckMan = UserName;
            //o.DataStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.CheckFeedStatus.待决定 : EnumManager.CheckFeedStatus.已驳回;
            //o.DataStatusCode = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.CheckFeedStatusCode.待决定 : EnumManager.CheckFeedStatusCode.已驳回;
            o.DataStatus = EnumManager.CheckFeedStatus.待决定;
            o.DataStatusCode = EnumManager.CheckFeedStatusCode.待决定;
            o.CheckResult = RadioButtonListExamineResult.SelectedValue;
           
            if (RadioButtonListExamineResult.SelectedValue != "通过")
            {
                o.BackReason = TextBoxExamineRemark1.Text.Trim();
                o.BackUnit = "市建委";
                o.PassType = null;
            }
            else
            {
                o.PassType = RadioButtonListPassType.SelectedValue;
            }

            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                SetButtonEnable(o.DataStatus);
                BindCheckHistory(o.DataID);
                UIHelp.WriteOperateLog(UserName, UserID, "市级复审监管反馈", string.Format("姓名：{0}，DataID：{1}，复审结果：{2}，{3}。", o.WorkerName, o.DataID, o.CheckResult,
                    RadioButtonListExamineResult.SelectedValue != "通过" ? TextBoxExamineRemark1.Text.Trim() : ""));
                UIHelp.ParentAlert(Page, "审核成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "市级复审监管反馈失败！", ex);
                return;
            }
        }

        //市级决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;
            o.ConfirmTime = DateTime.Now;
            o.ConfirmMan = UserName;
            o.DataStatus = RadioButtonListDecide.SelectedValue == "通过" ? EnumManager.CheckFeedStatus.已决定 : EnumManager.CheckFeedStatus.已驳回;
            o.DataStatusCode = RadioButtonListDecide.SelectedValue == "通过" ? EnumManager.CheckFeedStatusCode.已决定 : EnumManager.CheckFeedStatusCode.已驳回;
            o.ConfirmResult = RadioButtonListDecide.SelectedValue;       
            if (RadioButtonListDecide.SelectedValue != "通过")
            {
                o.BackReason = TextBoxConfirmDesc.Text.Trim();
                o.BackUnit = "市建委";
            }

            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                SetButtonEnable(o.DataStatus);
                BindCheckHistory(o.DataID);
                UIHelp.WriteOperateLog(UserName, UserID, "市级决定监管反馈", string.Format("姓名：{0}，DataID：{1}，决定结果：{2}。", o.WorkerName, o.DataID, o.ConfirmResult));
                UIHelp.ParentAlert(Page, "审核成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "市级决定监管反馈失败！", ex);
                return;
            }
        }

        protected void SetReturnStatusList(RadComboBox rcb, string ApplyStatus)
        {
            int ItemCount = rcb.Items.Count;
            for (int i = ItemCount; i > 0; i--)
            {
                if (rcb.Items[i - 1].Text == ApplyStatus)
                {
                    rcb.Items.Remove(i - 1);
                    break;
                }
                else if (rcb.Items[i - 1].Text == EnumManager.CheckFeedStatus.待审查 && ApplyStatus == EnumManager.CheckFeedStatus.已驳回)
                {
                    continue;
                }
                else if (rcb.Items[i - 1].Text != ApplyStatus)
                {
                    rcb.Items.Remove(i - 1);
                }
            }
        }

                //审批环节后退
        protected void ButtonSendBack_Click(object sender, EventArgs e)
        {
            CheckFeedBackMDL o = ViewState["CheckFeedBackMDL"] as CheckFeedBackMDL;

            if (o.DataStatus == RadComboBoxReturnApplyStatus.SelectedItem.Text)
            {
                UIHelp.layerAlert(Page, "后退节点与当前申请单所处节点相同，无需后退！");
                return;
            }
            string log = "";
            switch (RadComboBoxReturnApplyStatus.SelectedItem.Text)
            {

                case EnumManager.CheckFeedStatus.待审查:
                    log = string.Format("姓名：{0}，身份证号：{1}，监管批次号：{2}。从“{3}”后退到“{4}”状态。", o.WorkerName, o.WorkerCertificateCode, o.PatchCode, o.DataStatus, RadComboBoxReturnApplyStatus.SelectedItem.Text);

                    if (o.AcceptResult == "不通过" || o.CheckResult == "不通过" || o.ConfirmResult == "不通过")
                    {
                        o.BackReason = null;
                        o.BackUnit = null;
                    }
                    o.AcceptCountry = null;
                    o.AcceptTime = null;
                    o.AcceptMan = null;
                    o.AcceptResult = null;
                    o.CountryReportTime = null;
                    o.CountryReportCode = null;
                    o.CheckTime = null;
                    o.CheckMan = null;
                    o.CheckResult = null;
                    o.ConfirmTime = null;
                    o.ConfirmMan = null;
                    o.ConfirmResult = null;
                    o.DataStatus = EnumManager.CheckFeedStatus.待审查;
                    o.DataStatusCode = EnumManager.CheckFeedStatusCode.待审查;
                    break;
                case EnumManager.CheckFeedStatus.待复审:
                    log = string.Format("姓名：{0}，身份证号：{1}，监管批次号：{2}。从“{3}”后退到“{4}”状态。", o.WorkerName, o.WorkerCertificateCode, o.PatchCode, o.DataStatus, RadComboBoxReturnApplyStatus.SelectedItem.Text);

                    if (o.CheckResult == "不通过" || o.ConfirmResult == "不通过")
                    {
                        o.BackReason = null;
                        o.BackUnit = null;
                    }
                    o.CheckTime = null;
                    o.CheckMan = null;
                    o.CheckResult = null;
                    o.ConfirmTime = null;
                    o.ConfirmMan = null;
                    o.ConfirmResult = null;
                    o.DataStatus = EnumManager.CheckFeedStatus.待复审;
                    o.DataStatusCode = EnumManager.CheckFeedStatusCode.待复审;
                    break;
                case EnumManager.CheckFeedStatus.待决定:
                    log = string.Format("姓名：{0}，身份证号：{1}，监管批次号：{2}。从“{3}”后退到“{4}”状态。", o.WorkerName, o.WorkerCertificateCode, o.PatchCode, o.DataStatus, RadComboBoxReturnApplyStatus.SelectedItem.Text);

                    if (o.ConfirmResult == "不通过")
                    {
                        o.BackReason = null;
                        o.BackUnit = null;
                    }
                  
                    o.ConfirmTime = null;
                    o.ConfirmMan = null;
                    o.ConfirmResult = null;
                    o.DataStatus = EnumManager.CheckFeedStatus.待决定;
                    o.DataStatusCode = EnumManager.CheckFeedStatusCode.待决定;
                    break;
            }

            try
            {
                CheckFeedBackDAL.Update(o);
                ViewState["CheckFeedBackMDL"] = o;
                BindCheckHistory(DataID);
                SetButtonEnable(o.DataStatus);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "后退综合监管审核节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退综合监管审核", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }
    }
}