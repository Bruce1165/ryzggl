using System.Collections.Generic;
using DataAccess;
using Model;
using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Utility;
using System.Text;

namespace ZYRYJG
{
    /// <summary>
    /// 首页
    /// </summary>
    public partial class Main : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        protected string getuInfo()
        {
            return Session["userInfo"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["o"] == "out") //退出系统
            {
                //FormsAuthentication.SignOut();
                //Response.Redirect("~/login.aspx", false);

                FormsAuthentication.SignOut();
                //关闭浏览器
                ClientScript.RegisterClientScriptBlock(GetType(), "clise", "window.close();", true);

            }
            if (!IsPostBack)
            {
                if (RoleIDs == "0")//个人
                {

                    //if (RootUrl.ToLower().Contains("localhost") == true)
                    //{
                    //    divPeiXun.Visible = true;
                    //}

                    div_question.Visible = true;//常见问题

                    div_workerTask.Visible = true;//业务办理快速通道

                    #region 缺考提示
                    try
                    {
                        if (Session["myMissExam"] != null)
                        {
                            string[] myMissExam = Session["myMissExam"].ToString().Split('|');
                            //HiddenField1.Value = string.Format(@"<div style=""line-height:180%;font-size:16px;color:red;border:1px solid #f3f3f3;background-color:#f7faf7;border-radius:12px 12px;padding:12px 12px;margin-bottom: 12px"">您报考的{0}的{1}考试缺考,已记录在档。如在一年内累计缺考三次（从第一次缺考时间计算),系统将锁定人员信息,一年内不得报考我市建筑业从业人员考试。为避免因缺考带来的社会资源浪费,请您认真遵守相关考试规定,谨慎选择报名日期。</div>", myMissExam[0], myMissExam[1]);
                            div_exampass.InnerHtml = string.Format(@"<div class=""syscheck""><span style=""font-weight:bold;color:#000000;"">警告：</span>您报考的{0}的{1}考试缺考,已记录在档。如在一年内累计缺考三次（从第一次缺考时间计算),系统将锁定人员信息,一年内不得报考我市建筑业从业人员考试。为避免因缺考带来的社会资源浪费,请您认真遵守相关考试规定,谨慎选择报名日期。</div>", myMissExam[0], myMissExam[1]);
                            //Session.Remove("myMissExam");
                            //UIHelp.layerTips(Page, string.Format(@"您报考的{0}的{1}考试缺考,已记录在档。如在一年内累计缺考三次（从第一次缺考时间计算),系统将锁定人员信息,一年内不得报考我市建筑业从业人员考试。为避免因缺考带来的社会资源浪费,请您认真遵守相关考试规定,谨慎选择报名日期。", myMissExam[0], myMissExam[1]));

                        }
                    }
                    catch { }
                    #endregion

                    //考试确认，临时弹框
                    ExamConform("3050,3051,3052,3053,3054", 1, "2023-03-06", "2023-03-10"
                      , @"
 <p style='text-indent:32px'>是否确认参加2023年3月20日-3月23日考试。确认参加选“是”，确认不参加选“否”。 </p>
 <p style='text-indent:32px'>确认不参加本次考试或未在规定时间内进行确认的，视为自动放弃本次考核，系统自动删除之前报名信息，考生可按考试计划重新报考其他月份考核。 </p>"
);

                    ////                    //本地测试*****************考试确认，临时弹框
                    //                    ExamConform("2561", 1, "2022-08-30", "2022-09-04"
                    //                        , @"根据北京新冠肺炎疫情防控工作领导小组第二百五十九次会议暨首都严格进京管理联防联控协调机制第二百零八次会议精神及北京市新冠肺炎疫情防控工作第390场新闻发布会的有关要求，<span style='font-weight:bold;color:#BD1A2D;'>考试前7天考生均应在京备考或环京通勤（北京健康宝显示“通勤”绿码）考生符合北京市最新疫情防控要求。</span>
                    //                            <p style='text-indent:32px'>1、考试前7天在京备考要求：以9月13日考生参加考试为例，返京考生最晚于9月5日到京，并连续在京备考，期间不得离京。以此类推。<br/>
                    //                             </p>
                    //                            <p style='text-indent:32px'>2.环京通勤考生要求：考生考试当天，北京健康宝“本人信息扫码登记”显示“通勤”绿码、通信大数据行程卡为“绿色”且考试前7天内到达或途径地只显示：北京市及环京周边区域内的一个市（县），不得有第三地行程记录。
                    //                            </p>
                    //                            <p style='text-indent:32px'>请您于2022年9月1-4日登录市住建委门户网站进入人员资格管理系统，对是否符合上述最新防疫要求进行重新确认。考生未在上述规定时间内进行确认的，视为自动放弃本次报考资格，不计入年度缺考次数。</p>
                    //                            <p style='text-indent:32px'>本人确认符合上述最新疫情防控要求，可按期参加9月份考试。</p>");


                    //UIHelp.layerAlertWithHtml(Page, "<p style=\"font-size:16px;line-height:180%;\">重要通知：已通过2024年北京市二级造价工程师职业资格考试(土木建筑工程、安装工程专业)的考生，系统暂时无法申请北京市二级造价工程师初始注册,待个人取得北京市二级造价工程师职业资格证书后，经市人事考评办推送相关数据，人员资格管理信息系统方可申请初始注册业务，请耐心等待。</p>");


                    object PatchCode = CommonDAL.GetObject(string.Format("select top 1 [PatchCode]  FROM [dbo].[CheckFeedBack] where [WorkerCertificateCode] ='{0}' and [DataStatusCode] < 7 and [PublishiTime] is not null and [WorkerRerpotTime] is null and LastReportTime > getdate()", WorkerCertificateCode));
                     if (PatchCode !=null)//有监管问询记录尚未反馈
                     {
                         if( Cache[PatchCode.ToString()] ==null)
                         {
                             CheckTaskMDL _CheckTaskMDL = CheckTaskDAL.GetObject(Convert.ToInt32(PatchCode));
                             Cache[PatchCode.ToString()] = _CheckTaskMDL.TipNotice;
                         }

                         p_ExamConvfirmDesc.InnerHtml = string.Format("<p style='text-indent: 32px;'>{0}</p>", Cache[PatchCode.ToString()].ToString().Replace("\r\n", "</p><p style='text-indent: 32px;'>"));

//                         p_ExamConvfirmDesc.InnerHtml =
//@"
//<p>
//根据住房城乡建设部提供的数据资料显示，您的一本或多本证书（例如：一级注册建造师、二级注册建造师、注册监理工程师）注册所在单位与社保缴费单位、住房公积金缴存单位不一致。根据《住房城乡建设部办公厅、人力资源社会保障部办公厅关于开展工程建设领域专业技术人员违规“挂证”行为专项治理的通知》建办市函〔2024〕283号，请您于2024年10月31日前，在相应的注册管理系统自行整改，办理完成相关注册证书注销，监管部门将视情况不再追究相关责任。
//</p><p>即日起至2024年11月30日您须进入北京市住房和城乡建设领域人员资格管理信息系统-综合监管模块，查询须反馈的注册信息，按提示上传相关材料提交审核。逾期监管部门将对存在“挂证”等违法违规行为依法从严查处。</p>
//";
                         DivExamConfirm.Style.Add("display", "block");
                         System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "show15", "show15();", true);
                     }
                   
                }
                if (IfExistRoleID("2") == true)//企业
                {
                    #region 代办任务
                    DataTable dt1 = CommonDAL.GetDataTable(string.Format("select * from unit where ENT_OrganizationsCode='{0}'", ZZJGDM));
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows[0]["ENT_City"] == DBNull.Value //区县
                            || dt1.Rows[0]["END_Addess"] == DBNull.Value //工商注册地址
                            || dt1.Rows[0]["ENT_Corporate"] == DBNull.Value //企业法人
                            || dt1.Rows[0]["ENT_Contact"] == DBNull.Value //联系人
                            || dt1.Rows[0]["ENT_Correspondence"] == DBNull.Value //通讯地址
                            || dt1.Rows[0]["ENT_Telephone"] == DBNull.Value //联系电话
                            || dt1.Rows[0]["ENT_Type"] == DBNull.Value //企业类型
                            || dt1.Rows[0]["ENT_Sort"] == DBNull.Value //资质类别
                            || dt1.Rows[0]["ENT_Grade"] == DBNull.Value)//资质等级
                        {
                            Response.Redirect("~/Unit/UnitMgr.aspx");
                        }
                    }
                    BindWaitCheckTask();
                    div_Task.Visible = true;
                    #endregion 代办任务

                    CheckUnitZZChange();//资质一致性检查
                }

                //*****************  临时添加飘窗****************************

                ////公益培训问卷调查
                //ClientScript.RegisterStartupScript(this.GetType(), "message", " FloatAd('#floadAD');", true);
                
                //if (DateTime.Now < Convert.ToDateTime("2021-09-23"))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "message", " FloatAd('#floadAD');", true);
                //}

                //******************************************************
                //LabelValidEndTime.Text = System.Configuration.ConfigurationManager.AppSettings["ValidEndTimeAlert"];
                //FileLog.WriteLog("进入Main界面继承BasePage角色解析失败");


                binddata();//预警通告



                //BindZCTZ();
            }
        }

        /// <summary>
        ///  缓考，考试确认，临时弹框。例如：ExamConform("213",2,"2022-07-01","2022-07-05")
        /// </summary>
        /// <param name="ListExamplanID">考试计划ID集合，用逗号分割。例如：2958,2959.....</param>
        /// <param name="ExamType">考试类型ID，1：普通考试，2：特种作业实操考试</param>
        /// <param name="TipStartDate">弹框开始日期</param>
        /// <param name="TipEndDate">弹框截止日期</param>
        /// <param name="TipHtml">提示内容</param>
        protected void ExamConform(string ListExamplanID, int ExamType, string TipStartDate, string TipEndDate, string TipHtml)
        {
            //------统计确认参加实操考试人数-----------------
            //select max(s.PostName) as 岗位,  count(s.[WORKERCERTIFICATECODE]) as '理论考试通过人数', count(c.[ConfirmTime])  '确认参加实操考试人数' 
            //FROM [dbo].[VIEW_EXAMSCORE_TZZY] s left join  [dbo].[EXAMSIGNUP_Confirm] c on s.[EXAMPLANID] = c.[EXAMPLANID] and s.[WORKERCERTIFICATECODE] =c.[CERTIFICATECODE]
            //where s.examplanid in(2929,2930,2932,2936,2974,2975,2976,2977)   and s.[EXAMSTATUS] = '正常' and  s.[SUMSCORE]>=s.[PASSLINE]
            //group by s.examplanid

            //------统计常规考试确认数量---------
            //select max(s.PostName) as 岗位,  count(s.[CERTIFICATECODE]) as '审核通过人数', count(c.[ConfirmTime])  '确认考试人数' 
            //FROM [dbo].[VIEW_EXAMSIGNUP_NEW] s left join  [dbo].[EXAMSIGNUP_Confirm] c on s.[EXAMPLANID] = c.[EXAMPLANID] and s.[CERTIFICATECODE] =c.[CERTIFICATECODE]
            //where s.examplanid in(2958,2959,2960,2961,2962)   and s.[STATUS]='已缴费' 
            //group by s.examplanid

            if (DateTime.Now < Convert.ToDateTime(TipEndDate).AddDays(1) && DateTime.Now > Convert.ToDateTime(TipStartDate))
            {
                DataTable dtExam = null;
                if (ExamType == 2)//特种作业实操考试，理论成绩合格的需要完成实操考试确认
                {
                    dtExam = CommonDAL.GetDataTable(string.Format(
                            @"select EXAMPLANID,WORKERCERTIFICATECODE from [VIEW_EXAMSCORE_TZZY] 
                            where WORKERCERTIFICATECODE='{0}' and EXAMPLANID in({1}) and [EXAMSTATUS] = '正常' and  [SUMSCORE]>=[PASSLINE]"
                            , WorkerCertificateCode, ListExamplanID));
                }
                else
                {
                    //普通考试，审核通过的需要考试确认
                    dtExam = CommonDAL.GetDataTable(string.Format(
                            @"select EXAMPLANID,CERTIFICATECODE as WORKERCERTIFICATECODE from EXAMSIGNUP 
                                                where CERTIFICATECODE='{0}' and EXAMPLANID in({1}) and [Status]='已缴费'"
                            , WorkerCertificateCode, ListExamplanID));

//                    //普通考试，报名成功的需要考试确认
//                    dtExam = CommonDAL.GetDataTable(string.Format(
//                            @"select EXAMPLANID,CERTIFICATECODE as WORKERCERTIFICATECODE from EXAMSIGNUP 
//                            where CERTIFICATECODE='{0}' and EXAMPLANID in({1}) and [CheckDatePlan] > '2022-01-01'"
//                            , WorkerCertificateCode, ListExamplanID));
                }

                if (dtExam != null && dtExam.Rows.Count > 0)//有审核通过的报名信息
                {
                    int count = CommonDAL.GetRowCount("EXAMSIGNUP_Confirm", "*", string.Format(" and [EXAMPLANID]={0} and [CERTIFICATECODE]='{1}'", dtExam.Rows[0]["ExamPlanID"], WorkerCertificateCode));
                    if (count == 0)//尚未确认，显示确认弹框
                    {
                        ViewState["ExamPlanID"] = dtExam.Rows[0]["ExamPlanID"];
                        p_ExamConvfirmDesc.InnerHtml = TipHtml;
                        DivExamConfirm.Style.Add("display", "block");
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "show15", "show15();", true);
                    }
                }
            }
        }

        //确认考试
        protected void ButtonExamYes_Click(object sender, EventArgs e)
        {
//            long ExamPlanID = (long)ViewState["ExamPlanID"];
//            try
//            {
//                CommonDAL.ExecSQL(string.Format(@"INSERT INTO [dbo].[EXAMSIGNUP_Confirm]([EXAMPLANID],[CERTIFICATECODE],[ConfirmTime],[ConfirmResult])
//                                                    select {0},'{1}',getdate(),1", ExamPlanID, WorkerCertificateCode)
//                       );
//                DivExamConfirm.Style.Add("display", "none");
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "记录确认考试失败！", ex);
//                return;
//            }

            Response.Redirect("~/CheckMgr/QuestionFeedBack.aspx", true);
        }

        //不参加考试
        protected void ButtonExamNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
//            long ExamPlanID = (long)ViewState["ExamPlanID"];
//            try
//            {
//                CommonDAL.ExecSQL(string.Format(@"INSERT INTO [dbo].[EXAMSIGNUP_Confirm]([EXAMPLANID],[CERTIFICATECODE],[ConfirmTime],[ConfirmResult])
//                                                    select {0},'{1}',getdate(),0", ExamPlanID, WorkerCertificateCode)
//                       );
//                DivExamConfirm.Style.Add("display", "none");
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "记录确认考试失败！", ex);
//                return;
//            }
        }


        /// <summary>
        /// 检查企业资质是否与现在企业信息不一致，有名称变化、区县变化并且存在证书，提示提交企业信息变更申请。
        /// </summary>
        protected void CheckUnitZZChange()
        {
            jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(ZZJGDM);  //企业资质
            if (_jcsjk_QY_ZHXXMDL == null) return;

            System.Text.StringBuilder sb = new StringBuilder();

            //建造师与企业资质不符
            DataTable dtJZS = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07' ", ZZJGDM, _jcsjk_QY_ZHXXMDL.QYMC, _jcsjk_QY_ZHXXMDL.XZDQBM, _jcsjk_QY_ZHXXMDL.ZCDZ));

            //从业人员证书与企业资质不符
            DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and PostTypeID=1 and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更'  order by PostTypeID,PostID", ZZJGDM, _jcsjk_QY_ZHXXMDL.QYMC, DateTime.Now.ToString("yyyy-MM-dd")));

            if (dtJZS != null && dtJZS.Rows.Count > 0)
            {
                sb.Append(string.Format("<p>系统发现本企业存在{0}本二级注册建造师注册信息（企业名称、工商注册地址、监管区县）与企业资质信息不一致，请尽快提交二建企业信息变更申请，经审批后将批量更新二级建造师与企业资质信息保持一致。</p>", dtJZS.Rows.Count));
            }

            if (dtPerson != null && dtPerson.Rows.Count > 0)
            {
                sb.Append(string.Format("<p>系统发现本企业存在{0}本安全生产考核三类人员证书信息（企业名称）与企业资质信息不一致，请尽快提交从业人员证书企业信息变更申请，经审批后将批量更新从业人员证书与企业资质信息保持一致。</p>", dtPerson.Rows.Count));
            }

            if (sb.Length > 0)
            {
                div_exampass.InnerHtml = string.Format("<div class='syscheck blinking-text'>{0}</div>", sb);
            }
        }


        //绑定代办任务
        protected void BindWaitCheckTask()
        {

            //if (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 23)//报名繁忙期间，不作统计
            //{
            //    return;
            //}
            QueryParamOB q = new QueryParamOB();
            if (IfExistRoleID("2") == true)//企业
            {
                q.Add(string.Format("((ENT_OrganizationsCode like '{0}%' and newUnitCheckTime is null )or (OldEnt_QYZJJGDM like '{0}%' and OldUnitCheckTime is null))", ZZJGDM));
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.待确认));
            }
            else
            {
                q.Add("1>2");
            }

            //分别计算出每个事项由多少个人
            //二建业务
            DataTable dt = DataAccess.ApplyDAL.GetApplyGroupByApplyType(q.ToWhereString());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["ApplyType"].ToString())
                {
                    case "初始注册":
                        LabelFirst.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "重新注册":
                        LabelRenew.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "延期注册":
                        LabelContinue.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "增项注册":
                        LabelAddItem.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "个人信息变更":
                        LabelChangeGR.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "执业企业变更":
                        LabelChangeZY.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "注销":
                        LabelCancel.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    default:
                        break;

                }
            }


            q = new QueryParamOB();
            if (IfExistRoleID("2") == true)//企业
            {
                q.Add(string.Format("((ENT_OrganizationsCode like '{0}%' and newUnitCheckTime is null )or (OldEnt_QYZJJGDM like '{0}%' and OldUnitCheckTime is null))", SHTJXYDM));
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.待确认));
            }
            else
            {
                q.Add("1>2");
            }

            //分别计算出每个事项由多少个人
            //二级造价工程师
            dt = DataAccess.zjs_ApplyDAL.GetApplyGroupByApplyType(q.ToWhereString());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["ApplyType"].ToString())
                {
                    case "初始注册":
                        Labelzjs_First.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "延续注册":
                        Labelzjs_Continue.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "个人信息变更":
                        Labelzjs_ChangeGR.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "执业企业变更":
                        Labelzjs_ChangeZY.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "注销":
                        Labelzjs_Cancel.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    default:
                        break;

                }
            }


            //从业人员业务
            dt = CommonDAL.GetDataTable(string.Format(
@"select '考试报名' ApplyType, count(1) Num from [dbo].[view_EXAMSIGNUP_new] where [STATUS] ='待初审' and  UnitCode like'{0}%' and [SIGNUPSTARTDATE] < dateadd(day,-1,getdate()) and [SignUpEndDate] > dateadd(day,-3,getdate())
union 
select '证书变更' ApplyType, count(1) Num from [dbo].[CERTIFICATECHANGE] where [STATUS] ='待单位确认' and NewUnitcode like '{0}%'
union 
select '证书续期' ApplyType, count(1) Num from [dbo].[CERTIFICATECONTINUE] where [STATUS] ='待单位确认' and NewUnitcode like '{0}%'
union 
select '证书进京' ApplyType, count(1) Num from [dbo].[CERTIFICATEENTERAPPLY] where [ApplySTATUS] ='待单位确认' and UnitCode like'{0}%'
union 
select 'A本增发' ApplyType, count(1) Num from [dbo].[CertificateMore] where [ApplySTATUS] ='待单位确认' and UnitCodeMore like'{0}%'
union 
select 'C1C2合并' ApplyType, count(1) Num from [dbo].[CertificateMerge] where [ApplySTATUS] ='待单位确认' and UnitCode like'{0}%'", ZZJGDM));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["ApplyType"].ToString())
                {
                    case "考试报名":
                        LabelCYExamSignup.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "证书变更":
                        LabelCYChage.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "证书续期":
                        LabelCYContinue.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "证书进京":
                        LabelCYEnter.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "A本增发":
                        LabelCYAZF.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    case "C1C2合并":
                        LabelC1C2.Text = dt.Rows[i]["Num"].ToString();
                        break;
                    default:
                        break;

                }
            }
        }


        /// <summary>
        /// 绑定预警信息、通知公告
        /// </summary>
        private void binddata()
        {
            try
            {
                if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 23 && (IfExistRoleID("0") == true || IfExistRoleID("2") == true))
                {
                    #region  证书过期提前预警月份
                    //int AlertMonth = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ValidEndTimeAlert"]);

                    if (IfExistRoleID("0") == true)
                    {
                        DataTable dt = CertificateDAL.GetListValidEndWarn(WorkerCertificateCode);
                        RadGridQY.DataSource = dt;
                        RadGridQY.DataBind();
                        RadGridQY.Visible = true;

                        if (dt.Rows.Count > 0)
                        {
                            p_endWarn.InnerText = "您的证书有效期即将过期，请联系企业及时办理证书延续！";
                        }
                        
                    }
                    else if(IfExistRoleID("2") == true)
                    {
                        bindValidEndTip();
                        
                    }
                    
                    #endregion

                    #region 证书领取通知
                    ObjectDataSource2.SelectParameters.Clear();
                    var h = new QueryParamOB();

                    if (IfExistRoleID("0") == true)//个人
                    {
                        h.Add(string.Format("PSN_CertificateNO like '{0}' ", WorkerCertificateCode));
                    }
                    if (IfExistRoleID("2") == true)//企业
                    {
                        h.Add(string.Format(" (ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_') ", ZZJGDM));
                    }
                    if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
                    {
                        h.Add(string.Format("ENT_City like '{0}%'", Region));
                    }
                    h.Add("SFCK='1'");
                    ObjectDataSource2.SelectParameters.Add("filterWhereString", h.ToWhereString());
                    RadGridZSTZ.CurrentPageIndex = 0;
                    RadGridZSTZ.DataSourceID = ObjectDataSource2.ID;
                    #endregion
                }
                else//报名繁忙期间，不作统计
                {
                    div_BanJie.Visible = false;
                    div_YuJing.Visible = false;
                }

                #region 政策发放通知

                //ObjectDataSource3.SelectParameters.Clear();
                //var k = new QueryParamOB();
                //k.Add("States='1'");
                //ObjectDataSource3.SelectParameters.Add("filterWhereString", k.ToWhereString());
                //RadGridZCTZ.CurrentPageIndex = 0;
                //RadGridZCTZ.DataSourceID = ObjectDataSource3.ID;

                //RadGridZCTZ.DataBind();

                #endregion

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "表格数据读取失败！", ex);
            }
        }

        //证书过期预警
        private void bindValidEndTip()
        {
            string sql = @"
            select 'ej' as certType, count(*) as certCount
            from [dbo].[COC_TOW_Person_BaseInfo] c 
            inner join [dbo].[COC_TOW_Register_Profession] p on c.[PSN_ServerID] = p.[PSN_ServerID]
            left join Apply a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,p.PRO_ValidityEnd)
            where p.[PRO_ValidityEnd] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and c.ENT_OrganizationsCode like '{2}' and a.applyid is null
            union
            select 'ez' as certType, count(*) as certCount
            from [dbo].[zjs_Certificate] c 
            left join [dbo].[zjs_Apply] a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,c.[PSN_CertificateValidity])
            where c.[PSN_CertificateValidity] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and c.ENT_OrganizationsCode like '{3}' and a.applyid is null
            union
            select 'slr' as certType, count(*) as certCount
            from [dbo].[CERTIFICATE] c 
            left join [dbo].[CERTIFICATECONTINUE] a on c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)
            where c.POSTTYPEID=1 and c.VALIDENDDATE between '{0}' and '{1}' and c.unitcode like '{2}'
            and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '待进京审批' AND c.[STATUS] <> '离京变更' AND c.[STATUS] <> '注销' and a.CertificateContinueid is null
            union
            select 'tzzy' as certType, count(*) as certCount
            from [dbo].[CERTIFICATE] c 
            left join [dbo].[CERTIFICATECONTINUE] a on c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)
            where c.POSTTYPEID=2 and c.VALIDENDDATE between '{0}' and '{1}' and c.unitcode like '{2}'
	        and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '注销' and a.CertificateContinueid is null";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"), ZZJGDM,SHTJXYDM));

            bool ifHaveValue = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["certCount"]) == 0)
                {
                    continue;
                }
                ifHaveValue = true;
                switch (dt.Rows[i]["certType"].ToString())
                {
                    case "ej":
                        LabelWarnEJend.Text = dt.Rows[i]["certCount"].ToString();
                        div_Warnej.Visible = true;
                        break;
                    case "ez":
                        LabelWarnEZend.Text = dt.Rows[i]["certCount"].ToString();
                        div_Warnez.Visible = true;
                        break;
                    case "slr":
                        LabelWarnSLRend.Text = dt.Rows[i]["certCount"].ToString();
                        div_Warnslr.Visible = true;
                        break;
                    case "tzzy":
                        LabelWarnTZZYend.Text = dt.Rows[i]["certCount"].ToString();
                        div_Warntzzy.Visible = true;
                        break;
                    default:
                        break;
                }
            }

            if(ifHaveValue==true)
            {
                if (IfExistRoleID("0") == true)//个人
                {
                    p_endWarn.InnerText = "您的证书有效期即将过期，请联系企业及时办理证书延续。";
                }
                else if (IfExistRoleID("2") == true)//企业
                {
                    p_endWarn.InnerText = "您公司的以下人员证书有效期即将过期，请及时组织办理证书延续。";
                }
                else
                {
                    p_endWarn.InnerText = "以下人员证书有效期即将过期。";
                }

                //if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
                //{
                //    p_endWarn.InnerText = "以下人员证书有效期即将过期。";
                //}
            }
            else
            {
                p_endWarn.InnerText = "没有预警信息。";
            }
        }

        protected void RadGridZCTZ_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            if (Cache["DataTableZCTZ"] != null)
            {
                dt = Cache["DataTableZCTZ"] as DataTable;
            }
            else
            {
                dt = CommonDAL.GetDataTable("select *,row_number() over(order by GetDateTime desc) as RowNum from dbo.PolicyNews where States='1' ");
                Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "DataTableZCTZ", dt, 4);

            }
            RadGridZCTZ.DataSource = dt;
            RadGridZCTZ.VirtualItemCount = dt.Rows.Count;

        }

        

    }
}