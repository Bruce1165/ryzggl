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
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignLock : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0,new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0,new RadComboBoxItem("全部", ""));
                ButtonSearch_Click(sender, e);
            }
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
            QueryParamOB q = GetQueryParamOB();            

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
              
        //导出excel
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParamOB(); 

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/报名信息_{0}.xls", PersonID.ToString());//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                string head="";
                string columns="";
                //导出数据到数据库服务器
             
                    head = @"考试时间\岗位工种\报名状态\姓名\证件号码\单位名称\组织机构代码\报名批号\报名地点\报名时间\审核人\审核时间";
                    columns = @"CONVERT(varchar(10), ExamStartDate, 20)\PostName\[Status]\WorkerName\CertificateCode\UnitName\UnitCode\SignUpCode\TrainUnitName\CONVERT(varchar(10), SignUpDate, 20)\CheckMan\CONVERT(varchar(10), CheckDate, 20)";
               
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMSIGNUP_NEW", q.ToWhereString(), "ExamSignUpID", head, columns);

                
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出考试报名情况查询结果失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "导出查询考试报名情况结果", q.ToWhereString());

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("考试报名情况下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

        }

        //获取查询条件
        private QueryParamOB GetQueryParamOB()
        {         
            QueryParamOB q = new QueryParamOB();
            if (ExamPlanSelect1.ExamPlanID.HasValue)
            {
                q.Add("ExamPlanID=" + ExamPlanSelect1.ExamPlanID.ToString());
            }
           
            // 单位名称
            if (RadTxtUnitName.Text != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            }
            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }
            //报名时间
             if (RadDatePicker_SignUpStartDate.SelectedDate.HasValue || RadDatePicker_SignUpEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(SignUpDate BETWEEN  '{0}' AND '{1}')"
                    , RadDatePicker_SignUpStartDate.SelectedDate.HasValue ? RadDatePicker_SignUpStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_SignUpEndDate.SelectedDate.HasValue ? RadDatePicker_SignUpEndDate.SelectedDate.Value.AddDays(1).AddMinutes(-1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }            

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

         
            switch(RadioButtonListLock.SelectedValue)
            {
                case "锁定中":
                    q.Add(string.Format("LockEndTime >= '{0}'",DateTime.Now.ToString("yyyy-MM-dd")));   
                    break;
                case "已解锁":
                    q.Add(string.Format("LockEndTime < '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));   
                    break;
                case ""://全部
                    q.Add("LockTime > '2000-01-01'");    
                    break;
            }                

            return q;
        }

    }
}
