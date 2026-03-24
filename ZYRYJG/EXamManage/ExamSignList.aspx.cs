using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //是否有不受报名时间限制的报名权限
                ViewState["SignUpWithoutTimeLimit"] = ValidResourceIDLimit(RoleIDs, "SignUpWithoutTimeLimit");
                

                rbl.SelectedValue = "1";
           
                ButtonSearch_Click(sender, e);

                BindExamSignupList();

                if (DateTime.Now < Convert.ToDateTime("2024-12-08"))
                {
                    Tip7day();
                }

                if(divSignupTip.InnerText !="")
                {
                    UIHelp.layerAlertWithHtml(Page, divSignupTip.InnerHtml);
                }

//                if (DateTime.Now < Convert.ToDateTime("2023-11-24"))
//                {
//                    string ts = @" <b>考试通知：</b><br /><br />
//2023年11月份（第四批）安全生产管理人员网络在线考核：<br />
//考生报名：2023年10月16日至10月18日<br />
//企业确认：2023年10月17日至10月20日<br />
//准考证下载：2023年11月15日至11月22日<br />
//模拟测试：2023年11月15日至11月21日（每日9:00至17:00，技术支持电话：4008703877）<br />
//正式考核：2023年11月23日<br />
//<span style=""color:red"">重要提示：逾期未按要求参加模拟测试或不满足网络在线考核相关要求的，视为考生自动放弃报考资格。</span><br /><br />
                //<a href=""../Template/安管人员网络在线考核要求及系统操作手册（2024年6月）.zip"" target=""_blank"">【 安管人员网络在线考核要求及系统操作手册】</a >
//";
//                    UIHelp.layerAlertWithHtml(Page, ts);
//                }

            }
        }

        /// <summary>
        /// 疫情特殊提示
        /// </summary>
        protected void Tip7day()
        {
//             string TipHtml = @" 各位考生：<br/>
//<p style='text-indent:32px;text-align:left;'>根据北京新冠肺炎疫情防控工作领导小组第二百五十九次会议暨首都严格进京管理联防联控协调机制第二百零八次会议精神及北京市新冠肺炎疫情防控工作第392场新闻发布会的有关要求，考试前7天考生均应在京备考或环京通勤考生应符合北京市最新疫情防控要求。</p>
//<p style='text-indent:32px;text-align:left;'>1.考试前7天均在京备考要求：以9月13日考生参加考试为例，返京考生最晚于9月5日到京，并连续在京备考，期间不得离京。以此类推。</p>
//<p style='text-indent:32px;text-align:left;'>2.环京通勤考生要求：考生考试当天，北京健康宝“本人信息扫码登记”显示“通勤”绿码、通信大数据行程卡为“绿色”且考试前7天内到达或途径地只显示：北京市及环京周边区域内的一个市（县），不得有第三地行程记录。</p>
//<p style='text-indent:32px;text-align:left;'>倡导进京考生抵京24小时内进行一次核酸检测，抵京24小时后、72小时内再次进行核酸检测，抵京7日内不聚餐、不聚会、不前往人员密集场所，核酸检测阴性结果未出之前减少外出，必须外出时要做好个人防护。</p>
//<p style='text-indent:32px;text-align:left;'>近期，发现不法人员帮助部分考生伪造通信大数据行程卡、北京健康宝等涉疫证明信息，以此蒙骗考场防疫工作人员查验。<span style='font-weight:bold;color:red;'>在此郑重提示，上述行为不仅严重干扰疫情防控秩序，而且涉嫌违法犯罪，一经发现立即移送公安机关查处。请广大考生自觉履行考试疫情防控承诺，主动抵制违法犯罪行为，共同维护疫情防控秩序。</span></p>";

            string TipHtml = @" 各位考生：<br/>
<p style='text-indent:32px;text-align:left;'>报考2024年12月安管人员考核的考生，应提供10月或11月在报考单位或其分支机构或劳务派遣单位缴纳社会保险的证明。因报名期间系统不稳定，12月5日增加1天报名时间，企业确认和审核时间不变。</p>
";

                    p_ExamConvfirmDesc.InnerHtml = TipHtml;
                    DivExamConfirm.Style.Add("display", "block");


                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "show15"
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
                }}
            }}, 1000);
        }}
        show15();", ButtonExamNo.ClientID)
                        , true);
        }


        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            //计划名称
            if(RadTextBoxExam_PlanName.Text.Trim() !="") q.Add(string.Format("ExamPlanName like '%{0}%'", RadTextBoxExam_PlanName.Text.Trim()));
            //岗位工种
            if(PostSelect2.PostID !="")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //报名时间
            if (RadDatePicker_SignUpStartDate.SelectedDate.HasValue || RadDatePicker_SignUpEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN  SignUpStartDate AND SignUpEndDate) or ('{1}' BETWEEN SignUpStartDate AND SignUpEndDate) or (SignUpStartDate BETWEEN '{0}' AND '{1}') or (SignUpEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_SignUpStartDate.SelectedDate.HasValue?RadDatePicker_SignUpStartDate.SelectedDate.Value.ToString():DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_SignUpEndDate.SelectedDate.HasValue ? RadDatePicker_SignUpEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            //考试时间
            if (RadDatePicker_ExamStartDate.SelectedDate.HasValue || RadDatePicker_ExamEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN ExamStartDate AND ExamEndDate) or ('{1}' BETWEEN ExamStartDate AND ExamEndDate) or (ExamStartDate BETWEEN '{0}' AND '{1}') or (ExamEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_ExamStartDate.SelectedDate.HasValue ? RadDatePicker_ExamStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_ExamEndDate.SelectedDate.HasValue ? RadDatePicker_ExamEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            if (rbl.SelectedValue =="1")//未考试
            {
                q.Add(string.Format("ExamStartDate>'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            if (rbl.SelectedValue == "2")//已考试
            {
                q.Add(string.Format("ExamStartDate<='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }

            switch (PersonType)
            {
                case 2://考生
                    //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    //string IDCard15 = "";
                    //string IDCard18 = "";
                    //if (_WorkerOB == null)
                    //{
                    //    IDCard15 = IDCard18 = "null";
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 15)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 18)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //else
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}

                    //q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where CertificateCode='{3}' or CertificateCode='{4}')))"
                    //  , "完全公开", "部分公开", "完全公开培训点受限", IDCard15, IDCard18));

                    q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where CertificateCode='{3}')))"
                    , "完全公开", "部分公开", "完全公开培训点受限", WorkerCertificateCode));
                    break;
                case 3://企业
                    q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where UnitCode='{3}')))", "完全公开", "部分公开", "完全公开培训点受限", ZZJGDM));
                    break;
                case 4://培训点
                    q.Add(string.Format("(IfPublish='{0}' or ((IfPublish='{1}' or IfPublish='{2}') and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where TrainUnitID={3})))", "完全公开", "部分公开", "完全公开培训点受限", UnitID.ToString()));
                    break;
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;
           
        }       

        protected void BindExamSignupList()
        {
            ObjectDataSource2.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            if (string.IsNullOrEmpty(WorkerCertificateCode) == false)
            {
                q.Add(string.Format("CertificateCode like '{0}%'", WorkerCertificateCode));

                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "SignUpDate";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridExamSingup.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

    //            //缺考次数限制：报考三类人员和专业管理人员时，一年内累积三次考试缺考（0分）的（三类人员和专业管理人员都包含），锁定身份证号码一年，一年内不能报三类人员和专业管理人员考试。
    //            if (ExamResultDAL.CheckMissExamCountLimitByWorkercertificatecode(null, WorkerCertificateCode) == true)
    //            {
    //                UIHelp.layerAlert(Page, "您报考的XX月X日的XX考试缺考,已记录在档。
    //如在一年内累计缺考三次（从第一次缺考时间计算),系统将锁定人员信息,一年内不得报考我市建筑业从业人员考试。
    //为避免因缺考带来的社会资源浪费,请您认真遵守相关考试规定,谨慎选择报名日期。");
    //                return;
    //            }
            }
            else
            {
                q.Add(string.Format("CREATEPERSONID={0}",PersonID));
            }

            ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());

           

            RadGridExamSingup.CurrentPageIndex = 0;
            RadGridExamSingup.DataSourceID = ObjectDataSource2.ID;
        }

        //关闭自定义倒数数秒提示
        protected void ButtonExamNo_Click(object sender, EventArgs e)
        {
            DivExamConfirm.Style.Add("display", "none");
        }
    }
}
