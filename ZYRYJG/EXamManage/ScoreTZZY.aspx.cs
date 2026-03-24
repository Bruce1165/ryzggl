using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;

namespace ZYRYJG.EXamManage
{
    public partial class ScoreTZZY : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PostSelect2.PostTypeID="2";
                PostSelect2.LockPostTypeID();


                for (int i = 2010; i <= (DateTime.Now.Year ); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));

                RadComboBoxYear.Items.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;

                switch (PersonType)
                {
                    case 2://个人
                        TrExamPlan.Visible = false;//隐藏考试计划控件
                        RadWindowManager1.OnClientClose = "";
                        TrPerson.Visible = false;//隐藏个人查询条件
                        break;
                    case 3://企业
                        TrExamPlan.Visible = false;//隐藏考试计划控件
                        RadWindowManager1.OnClientClose = "";
                        break;
                    default:
                        TrPost.Visible = false;//隐藏岗位工种和考试时间，必须按考试计划查询
                        break;
                }
                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {
            QueryParamOB q = new QueryParamOB();

            switch (PersonType)
            {
                case 2://个人

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
                    //q.Add(string.Format("(WorkerCertificateCode='{0}' or WorkerCertificateCode='{1}')", IDCard15, IDCard18));//证件号码

                     q.Add(string.Format("WorkerCertificateCode='{0}'", WorkerCertificateCode));//证件号码
                    break;
                case 3://企业
                    q.Add(string.Format("UnitCode='{0}'", ZZJGDM));//组织机构代码
                    break;
            }

            
            if (TrExamPlan.Visible == true )
            {
                q.Add(string.Format("ExamPlanID={0}", ExamPlanSelect1.ExamPlanID.HasValue?ExamPlanSelect1.ExamPlanID:0));// 考试计划
            }
            if (TrPost.Visible == true)
            {
                if (PostSelect2.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));//岗位类别
                if (PostSelect2.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect2.PostID));//岗位工种
                if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("year(ExamStartTime)={0}", RadComboBoxYear.SelectedValue));//考试年
                if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("month(ExamStartTime) = {0}", RadComboBoxMonth.SelectedValue));//考试月
            }
            if (TrPerson.Visible == true)
            {
                if (RadTextBoxExamCardID.Text.Trim() != "") q.Add(string.Format("ExamCardID like '{0}%'", RadTextBoxExamCardID.Text.Trim()));// 准考证
                if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '{0}%'", RadTextBoxWorkerName.Text.Trim()));// 考生姓名
                if (RadTextWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '{0}%'", RadTextWorkerCertificateCode.Text.Trim()));// 证件号码
                
            }

            return q;
        }

      
        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();//清空查询参数
            QueryParamOB q = GetQueryParamOB();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSource1.ID;          
        }

        //导出通过考试考生名单
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB q = GetQueryParamOB();
            string sortBy = "ExamPlanID desc,EXAMCARDID";
            string colHead = @"姓名\证件号码\工作单位\岗位工种\考试时间\准考证号\考试结果\合格线";
            string colName = @"WorkerName\WORKERCERTIFICATECODE\UnitName\PostName\CONVERT(varchar(16), ExamStartTime, 20)\ExamCardID\ case EXAMSTATUS when '正常' then case when SUMSCORE >= PASSLINE then '合格' when PASSLINE is null then '待定'  else  '不合格（'+ cast(cast(SUMSCORE as int) as varchar(9)) +'）' end else EXAMSTATUS end\PASSLINE";
            
            string saveFile = string.Format("~/UpLoad/CertifEnterApply/考试成绩{1}_{0}.xls", PersonID.ToString(),DateTime.Now.ToString("yyMMddHHmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMSCORE_TZZY", q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出特种作业理论考试成绩失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("理论考试成绩下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
             
    }
}
