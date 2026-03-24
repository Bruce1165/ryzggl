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

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignView : BasePage
    {
        /// <summary>
        /// 当前培训点信息
        /// </summary>
        protected TrainUnitMDL curTrainUnit
        {
            get { return ViewState["TrainUnitMDL"] == null ? null : (ViewState["TrainUnitMDL"] as TrainUnitMDL); }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return ViewState["ExamSignUpOB"] == null ? "" : string.Format("KS-{0}", (ViewState["ExamSignUpOB"] as ExamSignUpOB).ExamSignUpID); }
        }

        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 报名人员是否为报名企业法人
        /// </summary>
        protected bool IfFaRen
        {
            get
            {
                if (ViewState["IfFaRen"] == null)
                {
                    ViewState["IfFaRen"] = CertificateDAL.IFExistFRByUnitCode(RadTextBoxUnitCode.Text.Trim(), RadTextBoxWorkerName.Text.Trim());

                }
                return Convert.ToBoolean(ViewState["IfFaRen"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                if (_ExamSignUpOB != null)
                {
                    ViewState["ExamSignUpOB"] = _ExamSignUpOB;
                    ExamPlanOB explanOB = ExamPlanDAL.GetObject(_ExamSignUpOB.ExamPlanID.Value);//考试计划信息
                    ViewState["ExamPlanOB"] = explanOB;

                    UIHelp.SetData(divExamSignUp, explanOB, true);
                    ViewState["PostTypeID"] = explanOB.PostTypeID;
                    ViewState["ExamPlanID"] = _ExamSignUpOB.ExamPlanID;
                    ViewState["SignUpDate"] = _ExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd");

                    if (explanOB.PostTypeID.Value <3 && _ExamSignUpOB.ZACheckResult == 0)
                    {
                        UIHelp.layerAlert(Page, string.Format("数据校验未通过。{0}", _ExamSignUpOB.ZACheckRemark));
                    }

                    if (explanOB.PostTypeID == 4000)
                    {
                        tableTrainUnit.Visible = true;
                        tableUnit.Visible = false;
                        LabelTrainUnit.Text = explanOB.SignUpPlace;
                        trDataChcekRow.Visible = false;
                        trDataCheckHead.Visible = false;

                        tdUnitCheck.InnerText = "培训点审核";

                        ListItem item1 = RadioButtonListOldUnitCheckResult.Items.FindByValue("同意");
                        item1.Text = "通过";
                        ListItem item2 = RadioButtonListOldUnitCheckResult.Items.FindByValue("不同意");
                        item2.Text = "不通过";
                        TextBoxOldUnitCheckRemark.Text = "通过";

                        TrainUnitMDL _TrainUnitMDL = TrainUnitDAL.GetObjectBySHTYXYDM(SHTJXYDM);
                        if (_TrainUnitMDL != null)
                        {
                            ViewState["TrainUnitMDL"] = _TrainUnitMDL;
                        }
                    }

                    #region 绑定申请单信息

                    //照片
                    System.Random rm = new Random();
                    ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(_ExamSignUpOB.ExamPlanID.ToString(), _ExamSignUpOB.CertificateCode))); //绑定照片;

                    //考生信息
                    WorkerOB _WorkerOB = WorkerDAL.GetObject(_ExamSignUpOB.WorkerID.Value);
                    if (_WorkerOB != null)
                    {
                        UIHelp.SetData(divExamSignUp, _WorkerOB, true);
                    }

                    //报名信息    
                    UIHelp.SetData(divExamSignUp, _ExamSignUpOB, true);

                    if (explanOB.PostTypeID == 2 && string.IsNullOrEmpty(_ExamSignUpOB.SafeTrainType) == false)
                    {
                        RefreshRadGridAnQuanPX();
                    }

                    if (string.IsNullOrEmpty(_ExamSignUpOB.Job) == false)
                    {
                        trJob.Visible = true;//职务
                    }
                    if (_ExamSignUpOB.ENT_ContractType.HasValue)//合同
                    {
                        switch (_ExamSignUpOB.ENT_ContractType.Value)
                        {
                            case 1:
                                LabelENT_ContractType.Text = "固定期限";
                                LabelENT_ContractStartTime.Text = string.Format("{0} 至 {1}", _ExamSignUpOB.ENT_ContractStartTime.Value.ToString("yyyy-MM-dd"), _ExamSignUpOB.ENT_ContractENDTime.Value.ToString("yyyy-MM-dd"));
                                break;
                            case 2:
                                LabelENT_ContractType.Text = "无固定期限";
                                break;
                            case 3:
                                LabelENT_ContractType.Text = "以完成一定工作任务为期限";
                                break;
                        }

                    }

                    if (_ExamSignUpOB.CreatePersonID == _ExamSignUpOB.WorkerID)//申请人
                    {
                        HiddenFieldApplyMan.Value = _ExamSignUpOB.WorkerName;
                    }
                    else if (_ExamSignUpOB.CreatePersonID == _ExamSignUpOB.TrainUnitID)
                    {
                        HiddenFieldApplyMan.Value = _ExamSignUpOB.S_TRAINUNITNAME;
                    }
                    else if (_ExamSignUpOB.CreatePersonID == _ExamSignUpOB.UnitID)
                    {
                        HiddenFieldApplyMan.Value = _ExamSignUpOB.UnitName;
                    }
                    else
                    {
                        HiddenFieldApplyMan.Value = "注册中心";
                    }

                    divCheckPlan.InnerHtml = string.Format("申请人：{0}", HiddenFieldApplyMan.Value);

                    if (_ExamSignUpOB.Promise.HasValue && _ExamSignUpOB.Promise.Value == 1)
                    {
                        CheckBoxPromise.Checked = true;
                    }

                    #endregion 绑定申请单信息

                    if (explanOB.PostTypeID.Value == 1 || explanOB.PostTypeID.Value == 2)
                    {
                        ShowZACheckResult(_ExamSignUpOB);
                    }
                    if (explanOB.PostID.Value == 147)
                    {
                        ShowFRCheckResult();
                    }

                    if (explanOB.PostTypeID.Value == 1)
                    {
                        divPostType1Tip.Visible = true;
                    }

                    ShowSheBao(_ExamSignUpOB.CertificateCode, _ExamSignUpOB.UnitCode, _ExamSignUpOB.SignUpDate.Value.ToString(), _ExamSignUpOB.SheBaoCheck);
                    BindFile(ApplyID);
                    BindCheckHistory(_ExamSignUpOB.ExamSignUpID.Value);

                    switch (_ExamSignUpOB.Status)
                    {
                        case EnumManager.SignUpStatus.NewSignUp://待初审（待企业确认）
                            if (IfExistRoleID("2") == true)
                            {
                                if (explanOB.SignUpEndDate.Value.AddDays(3) < DateTime.Now)//企业确认时间为个人提交截至时间＋2天
                                {
                                    UIHelp.layerAlert(Page, "报名企业确认时间已经截止，建委不再接收申请，请您参加下次报名。");
                                }
                                else
                                {
                                    divUnit.Visible = true;
                                    UIHelp.SetReadOnly(TextBoxOldUnitCheckRemark, false);
                                }
                            }
                            break;
                        case EnumManager.SignUpStatus.FirstChecked://已初审（企业已确认）
                            if (ValidResourceIDLimit(RoleIDs, "CheckSignFirstManage") == true
                               && explanOB.LatestCheckDate.Value.AddDays(1) > DateTime.Now
                                )
                            {
                                TableJWAccept.Visible = true;
                                UIHelp.SetReadOnly(TextBoxJWAcceptResult, false);
                            }

                            if (IfExistRoleID("2") == true)
                            {
                                if (explanOB.SignUpEndDate.Value.AddDays(3) > DateTime.Now)//企业确认时间为个人提交截至时间＋2天
                                {
                                    divCancel.Visible = true;
                                }
                            }
                            break;
                        case EnumManager.SignUpStatus.Accept:
                            if (ValidResourceIDLimit(RoleIDs, "SignUpCheck") == true)//审核权限
                            {
                                if (explanOB.LatestCheckDate.Value.AddDays(1) > DateTime.Now)
                                {
                                    TableJWCheck.Visible = true;
                                    UIHelp.SetReadOnly(TextBoxJWCheckResult, false);
                                }
                            }
                            break;
                        case EnumManager.SignUpStatus.Checked:
                            if (ValidResourceIDLimit(RoleIDs, "PayConfirm") == true)//审核权限
                            {
                                if (explanOB.LatestCheckDate.Value.AddDays(1) > DateTime.Now)
                                {
                                    TablePayConfirm.Visible = true;
                                    UIHelp.SetReadOnly(TextBoxPayConfirmResult, false);
                                }
                            }
                            break;
                        case EnumManager.SignUpStatus.ReturnEdit://退回修改
                            if (_ExamSignUpOB.AcceptTime.HasValue == true //受理意见：退回修改
                                && ValidResourceIDLimit(RoleIDs, "CheckSignFirstManage") == true //有受理权限
                                && explanOB.LatestCheckDate.Value.AddDays(1) > DateTime.Now//审核未截止
                                )
                            {
                                TableJWAccept.Visible = true;
                                UIHelp.SetReadOnly(TextBoxJWAcceptResult, false);
                                RadioButtonListJWAccept.SelectedIndex = 1;
                                TextBoxJWAcceptResult.Text = _ExamSignUpOB.AcceptResult;
                                tabkeAccept_Lock.Style.Add("display", "true");
                                if (_ExamSignUpOB.LockTime.HasValue == true)
                                {
                                    CheckBoxAcceptLock.Checked = true;
                                    TextBoxAcceptLockReasion.Text = _ExamSignUpOB.LockReason;
                                    LabelAcceptLockTimeSpan.Text = string.Format("（锁定时间：{0} - {1}）", _ExamSignUpOB.LockTime.Value.ToString("yyyy-MM-dd"), _ExamSignUpOB.LockEndTime.Value.ToString("yyyy-MM-dd"));
                                }
                                UIHelp.layerAlert(Page, "申请已被退回，再次受理将覆盖前一次受理结果。");
                            }
                            else if (_ExamSignUpOB.CheckDate.HasValue == true //审核意见：退回修改
                               && ValidResourceIDLimit(RoleIDs, "SignUpCheck") == true //有受理权限
                               && explanOB.LatestCheckDate.Value.AddDays(1) > DateTime.Now//审核未截止
                               )
                            {
                                TableJWCheck.Visible = true;
                                UIHelp.SetReadOnly(TextBoxJWCheckResult, false);
                                RadioButtonListJWCheck.SelectedIndex = 1;
                                TextBoxJWCheckResult.Text = _ExamSignUpOB.CheckResult;
                                tabke_Lock.Style.Add("display", "true");
                                if (_ExamSignUpOB.LockTime.HasValue == true)
                                {
                                    CheckBoxLock.Checked = true;
                                    TextBoxLockReasion.Text = _ExamSignUpOB.LockReason;
                                    LabelLockTimeSpan.Text = string.Format("（锁定时间：{0} - {1}）", _ExamSignUpOB.LockTime.Value.ToString("yyyy-MM-dd"), _ExamSignUpOB.LockEndTime.Value.ToString("yyyy-MM-dd"));
                                }
                                UIHelp.layerAlert(Page, "申请已被退回，再次审核将覆盖前一次审核结果。");
                            }
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyID">申请编号</param>
        private void BindCheckHistory(long ExamSignUpID)
        {
            //DataTable dt = ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID);
            //RadGridCheckHistory.DataSource = dt;
            //RadGridCheckHistory.DataBind();

            DataTable dt = null;
            ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
            if (_ExamPlanOB.PostTypeID.Value == 4000)
            {
                dt = ExamSignUpDAL.GetCheckHistoryListOfNewZYJN(ExamSignUpID);
            }
            else
            {
                //if (IfExistRoleID("2") == true)
                //{
                //    if (_ExamPlanOB.ExamCardSendStartDate.Value.AddDays(-1) < DateTime.Now)//已经放准考证
                //    {
                //        dt = ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID);
                //    }
                //    else//未放准考证
                //    {
                //        dt = ExamSignUpDAL.GetCheckHistoryListHideJWCheck(ExamSignUpID);
                //    }
                //}
                //else
                //{
                //    dt = ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID);
                //}

                //2024-05-23调整企业端查看报名审核状态的时间(调整为:按审核进度随时可查)。
                dt = ExamSignUpDAL.GetCheckHistoryList(ExamSignUpID);
            }

            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();
        }

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

        //检查图片存放路径
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/" + ViewState["ExamPlanID"].ToString()));
        }

        /// <summary>
        /// 显示社保比对结果
        /// </summary>
        /// <param name="CertificateCode"></param>
        /// <param name="UnitCode"></param>
        /// <param name="SignUpDate"></param>
        /// <param name="sheBaoCheckResult"></param>
        private void ShowSheBao(string CertificateCode, string UnitCode, string SignUpDate, int? sheBaoCheckResult)
        {
            divSheBao.InnerHtml = string.Format("<b>社保校验：</b><span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>"
                , CertificateCode, UnitCode, SignUpDate
                , sheBaoCheckResult.HasValue == false ? "尚未比对（夜间比对）" : sheBaoCheckResult == 1 ? "符合" : "不符合"
                );
        }

        /// <summary>
        /// 显示质量安全网持证规则校验结果
        /// </summary>
        protected void ShowZACheckResult(ExamSignUpOB _ExamSignUpOB)
        {
            if (_ExamSignUpOB.ZACheckTime.HasValue == false)
            {
                divZACheckResult.InnerHtml = "<b>持证校验：</b>：尚未比对";
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

        /// <summary>
        /// 显示法人认证结果结果
        /// </summary>
        protected void ShowFRCheckResult()
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

                DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID("1"); ;

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

        //单位审核
        protected void ButtonUnitCheck_Click(object sender, EventArgs e)
        {
            ExamSignUpOB esob = (ExamSignUpOB)ViewState["ExamSignUpOB"];

            ExamPlanOB explanOB = (ExamPlanOB)ViewState["ExamPlanOB"];//考试计划信息

            //************临时关闭读秒提示********************************
            ViewState["继续提交"] = true;
            //*******************************************

            if (
                explanOB.PostTypeID != 1//非三类人
                || (explanOB.PostID == 147 && IfFaRen == true)//法人报名A
                || esob.SheBaoCheck.HasValue == false//申报尚未校验
                || esob.SheBaoCheck == 1//社保校验合格
                || (ViewState["继续提交"] != null && Convert.ToBoolean(ViewState["继续提交"]) == true)//已读8表提示并继续提交
                || RadioButtonListOldUnitCheckResult.SelectedValue == "不同意"
                || RadioButtonListOldUnitCheckResult.SelectedValue == "不通过"
                   
             )
            {
                #region  提交建委

                if (explanOB.PostTypeID.Value == 4000)
                {
                    esob.CheckDate = DateTime.Now;
                    esob.CheckMan = curTrainUnit.TrainUnitName;
                    esob.CheckResult = (RadioButtonListOldUnitCheckResult.SelectedValue == "通过" || RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "通过" : TextBoxOldUnitCheckRemark.Text);
                    esob.Status = (RadioButtonListOldUnitCheckResult.SelectedValue == "通过" || RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.SignUpStatus.PayConfirmed : EnumManager.SignUpStatus.ReturnEdit);
                    esob.ModifyTime = esob.CheckDate;

                    if (esob.Status == EnumManager.SignUpStatus.PayConfirmed)
                    {
                        esob.PayConfirmDate = DateTime.Now;
                        esob.PayConfirmMan = curTrainUnit.TrainUnitName;
                        esob.PayConfirmRult = "通过";
                    }

                    try
                    {
                        ExamSignUpDAL.Update(esob);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "培训点报名审核失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "培训点审核报名", string.Format("报名批次号：{0}。报名人：{1}，岗位工种：{2}。审核结果：{3}", esob.SignUpCode, esob.WorkerName, RadTextBoxPostName.Text, esob.Status));
                    if (esob.Status == EnumManager.SignUpStatus.ReturnEdit)
                    {
                        UIHelp.layerAlert(Page, @"已成功退回个人。", "hideIfam(true);");
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, @"审核成功。", "hideIfam(true);");
                    }
                }
                else
                {
                    if (RadioButtonListOldUnitCheckResult.SelectedValue == "同意")
                    {
                        if (esob.ZACheckResult.HasValue == false)
                        {
                            UIHelp.layerAlert(Page, "尚未进行数据比对，请等待系统比对数据后再提交申请。");
                            return;
                        }
                        if (esob.ZACheckResult == 0)
                        {
                            UIHelp.layerAlert(Page, string.Format("持证校验未通过，无法提交申请。原因：{0}", esob.ZACheckRemark));
                            return;
                        }
                    }

                    //esob.ModifyPersonID = UnitID;
                    esob.ModifyTime = DateTime.Now;
                    esob.Status = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.SignUpStatus.FirstChecked : EnumManager.SignUpStatus.ReturnEdit);
                    esob.FIRSTTRIALTIME = esob.ModifyTime;//单位审核时间
                    esob.HireUnitAdvise = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? "提交建委审核" : TextBoxOldUnitCheckRemark.Text);//单位意见

                    try
                    {
                        ExamSignUpDAL.Update(esob);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "单位审核报名失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "单位审核报名", string.Format("报名批次号：{0}。报名人：{1}，岗位工种：{2}。审核结果：{3}", esob.SignUpCode, esob.WorkerName, RadTextBoxPostName.Text, esob.Status));

                    if (esob.Status == EnumManager.SignUpStatus.ReturnEdit)
                    {
                        UIHelp.layerAlert(Page, @"已成功退回个人。<br/><br/><b>特别提示：<br/>
                                        1、企业确认截止前，企业驳回个人申请，个人应及时修改、补充材料后再次提交企业确认。<br/>
                                        2、企业确认截止后，个人无法修改、补充材料，企业无法确认。</b>", "hideIfam(true);");
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, @"考试报名申请企业确认成功，已提交市住建委审核。<br/><br/><b>特别提示：<br/>1、企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。<br/>
                2、企业确认截止后，市住建委进行一次性审核。审核不通过的，个人无法进行修改、补充及提交，请企业认真复核申请人的考试报名材料，确保符合审核要求。</b>", "hideIfam(true);");
                    }
                }

                ViewState["继续提交"] = null;
                #endregion  提交建委
            }
            else
            {
                #region  弹出提示，8秒阅读
                string TipHtml = null;

                if (explanOB.PostID == 6//土建类专职安全生产管理人员
                    || explanOB.PostID == 1123//机械类专职安全生产管理人员
                    || explanOB.PostID == 1125)//综合类专职安全生产管理人员
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
            ExamSignUpOB esob = (ExamSignUpOB)ViewState["ExamSignUpOB"];

            ////启用质安网检查后取消注释
            //if (RadioButtonListJWAccept.SelectedValue == "通过")
            //{
            //    if (esob.ZACheckResult.HasValue == false)
            //    {
            //        UIHelp.layerAlert(Page, "数据校验尚未比对，无法审核！");
            //        return;
            //    }
            //    else if (esob.ZACheckResult.Value == 0)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("数据校验结果：{0}", esob.ZACheckRemark));
            //        return;
            //    }
            //}

            esob.AcceptMan = PersonName;
            esob.AcceptTime = DateTime.Now;
            esob.AcceptResult = (RadioButtonListJWAccept.SelectedValue == "通过" ? "通过" : TextBoxJWAcceptResult.Text.Trim());
            esob.Status = (RadioButtonListJWAccept.SelectedValue == "通过" ? EnumManager.SignUpStatus.Accept : EnumManager.SignUpStatus.ReturnEdit);
            esob.ModifyPersonID = PersonID;
            esob.ModifyTime = esob.AcceptTime;

            if (RadioButtonListJWAccept.SelectedValue == "不通过")
            {
                if (CheckBoxAcceptLock.Checked == true)//加入违规申报人员库（锁定一年，不允许报名）
                {
                    esob.LockTime = esob.AcceptTime.Value.AddSeconds(1);
                    esob.LockEndTime = Convert.ToDateTime(DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"));
                    esob.LockMan = PersonName;
                    esob.LockReason = TextBoxAcceptLockReasion.Text;
                }
            }
            else
            {
                if (esob.LockTime.HasValue)
                {
                    esob.LockTime = null;
                    esob.LockEndTime = null;
                    esob.LockMan = null;
                    esob.LockReason = null;
                }
            }

            try
            {
                ExamSignUpDAL.Update(esob);
                UIHelp.WriteOperateLog(PersonName, UserID, "报名受理", string.Format("姓名：{0}，受理结果：{1}。", esob.WorkerName, esob.AcceptResult));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "考试报名受理失败！", ex);
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }

        //建委审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            ExamSignUpOB esob = (ExamSignUpOB)ViewState["ExamSignUpOB"];
            esob.CheckDate = DateTime.Now;
            esob.CheckMan = PersonName;
            esob.CheckResult = (RadioButtonListJWCheck.SelectedValue == "通过" ? "通过" : TextBoxJWCheckResult.Text.Trim());
            esob.Status = (RadioButtonListJWCheck.SelectedValue == "通过" ? EnumManager.SignUpStatus.Checked : EnumManager.SignUpStatus.ReturnEdit);
            esob.ModifyTime = esob.CheckDate;

            if (RadioButtonListJWCheck.SelectedValue == "不通过")
            {
                if (CheckBoxLock.Checked == true)//加入违规申报人员库（锁定一年，不允许报名）
                {
                    esob.LockTime = esob.CheckDate.Value.AddSeconds(1);
                    esob.LockEndTime = Convert.ToDateTime(DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"));
                    esob.LockMan = PersonName;
                    esob.LockReason = TextBoxLockReasion.Text;
                }
            }
            else
            {
                if (esob.LockTime.HasValue)
                {
                    esob.LockTime = null;
                    esob.LockEndTime = null;
                    esob.LockMan = null;
                    esob.LockReason = null;
                }
            }

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                esob.CheckCode = UIHelp.GetNextBatchNumber(tran, "BMSH");//报名审核编号
                ExamSignUpDAL.Update(tran, esob);
                tran.Commit();
                UIHelp.WriteOperateLog(PersonName, UserID, "报名审核", string.Format("审核批号：{0}，姓名：{1}，审核结果：{2}。", esob.CheckCode, esob.WorkerName, esob.CheckResult));
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "考试报名审核失败！", ex);
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }

        //企业取消申报
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];
            o.Status = EnumManager.SignUpStatus.ReturnEdit;
            o.FIRSTTRIALTIME = DateTime.Now;//单位审核时间
            o.HireUnitAdvise = "企业取消申报，退回个人";

            ExamSignUpDAL.Update(o);
            UIHelp.WriteOperateLog(PersonName, UserID, "企业取消提交考试报名", string.Format("报名批次号：{0}。报名人：{1}。", o.SignUpCode, o.WorkerName));
            UIHelp.layerAlert(Page, "取消报名成功。申请单已经退回到个人。", 6, 0);
        }

        //继续提交
        protected void ButtonYes_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
            ViewState["继续提交"] = true;
            ButtonUnitCheck_Click(sender, e);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SendUnit", "__doPostBack('SendUnit', '');", true);
        }

        //取消提交
        protected void ButtonNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
        }

        /// <summary>
        /// 绑定考前安全操作培训记录
        /// </summary>
        protected void RefreshRadGridAnQuanPX()
        {
            try
            {
                ExamSignUpOB o = (ExamSignUpOB)ViewState["ExamSignUpOB"];
                DataTable dt = ExamSignupTrainDAL.GetList(0, 100, string.Format(" and ExamSignUpID={0}", o.ExamSignUpID), "DataNo");//委培记录
                RadGridAnQuanPX.DataSource = dt;

                trExamSafeTrainHead.Visible = true;
                trExamSafeTrainDetail.Visible = true;
                trExamSafeTrain1.Visible = true;
                if (o.SafeTrainType == "委托培训机构")
                {
                    trExamSafeTrain2.Visible = true;
                    trExamSafeTrain3.Visible = true;
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取考前安全操作培训记录失败！", ex);
                return;
            }
        }

        //报名审核确认
        protected void ButtonPayConfirmRult_Click(object sender, EventArgs e)
        {
            ExamSignUpOB esob = (ExamSignUpOB)ViewState["ExamSignUpOB"];
            esob.PayConfirmDate = DateTime.Now;
            esob.PayConfirmMan = PersonName;
            esob.PayConfirmRult = (RadioButtonListPayConfirm.SelectedValue == "通过" ? "通过" : TextBoxPayConfirmResult.Text.Trim());
            esob.Status = (RadioButtonListPayConfirm.SelectedValue == "通过" ? EnumManager.SignUpStatus.PayConfirmed : EnumManager.SignUpStatus.ReturnEdit);
            esob.ModifyPersonID = PersonID;
            esob.ModifyTime = esob.PayConfirmDate;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                esob.PayConfirmCode = UIHelp.GetNextBatchNumber(tran, "JFQR");//缴费确认编号
                ExamSignUpDAL.Update(tran, esob);
                tran.Commit();
                UIHelp.WriteOperateLog(PersonName, UserID, "考试报名审核确认", string.Format("确认批次号：{0}，姓名：{1}，审核确认结果：{2}。", esob.PayConfirmCode, esob.WorkerName, esob.PayConfirmRult));
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "考试报名审核确认失败！", ex);
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "hideIfam(true);", true);
        }
    }
}
