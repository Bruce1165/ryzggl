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
    public partial class ScorePublish : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            q.Add(string.Format("Status <> '{0}'", EnumManager.ExamResultStatus.BeforeResult));//成绩已生成的（未公告和已公告的）
            q.Add(string.Format("ExamResult = '{0}'", EnumManager.ExamResult.Pass));//只显示成绩合格的
            long _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.Value : 0;//考试计划
            q.Add(string.Format("ExamPlanID > {0} and ExamPlanID < {1}", Convert.ToString(_ExamPlanID - 1), Convert.ToString(_ExamPlanID + 1)));// 考试计划
            if (RadTextBoxExamCardID.Text.Trim() != "") q.Add(string.Format("ExamCardID like '%{0}%'", RadTextBoxExamCardID.Text.Trim()));// 准考证
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));// 考生姓名
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
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

        //公告
        protected void BtnPublish_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.Items.Count == 0) return;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                //更改公告状态
                ExamResultDAL.Publish(ExamPlanSelect1.ExamPlanID.Value, EnumManager.ExamResultStatus.Published, DateTime.Now);

                //科目
                DataTable dt = CommonDAL.GetDataTable(string.Format("select POSTID from dbo.EXAMPLANSUBJECT where examplanid={0}", ExamPlanSelect1.ExamPlanID));

                foreach(DataRow r in dt.Rows)
                {
                    sb.Append(string.Format(@"
        update t1 
        set t1.sumscoredetail=
            (
            case t2.examstatus when '正常' then 
                (case when t1.sumscoredetail is null then '' else t1.sumscoredetail + '，' end) + t2.subjectname +'：'+ cast(t2.sumscore as varchar(50))   
            else 
                (case when t1.sumscoredetail is null then '' else t1.sumscoredetail + '，' end) + t2.subjectname +'：'+ t2.examStatus 
            end
            )
        from dbo.EXAMRESULT t1
        inner join 
        ( select d.*,p.subjectname from	dbo.examsubjectresult d 
            inner join dbo.view_postinfo p on d.postid = p.subjectid where d.examplanid={0} and d.postid = {1}
        ) t2 on t1.EXAMCARDID=t2.EXAMCARDID  and t1.STATUS='成绩已公告';", ExamPlanSelect1.ExamPlanID,r["POSTID"]));
                }
                sb.Insert(0,string.Format("update dbo.EXAMRESULT set sumscoredetail=null where examplanid={0};", ExamPlanSelect1.ExamPlanID));
                CommonDAL.ExecSQL(sb.ToString());

                //更新准考证考试成绩详细
//                CREATE  FUNCTION DBO.UF_UPDATE_EXAMRESULT_SUMSCOREDETAIL_EXAMPLANID(@EXAMPLANID BIGINT)
//    RETURNS INTEGER 
//AS 
//BEGIN

//    declare @postid int
//    DECLARE mycur CURSOR FOR 
//    select POSTID from dbo.EXAMPLANSUBJECT where examplanid=@EXAMPLANID;
//    OPEN mycur
//    FETCH NEXT FROM mycur INTO @postid
//    WHILE @@FETCH_STATUS = 0
//    begin
//        update dbo.EXAMRESULT set sumscoredetail=null where examplanid=@EXAMPLANID;
//        update t1 
//        set t1.sumscoredetail=
//            (
//            case t2.examstatus when '正常' then 
//                (case when t1.sumscoredetail is null then '' else t1.sumscoredetail + '，' end) + t2.subjectname +'：'+ t2.sumscore  
//            else 
//                (case when t1.sumscoredetail is null then '' else t1.sumscoredetail + '，' end) + t2.subjectname +'：'+ t2.examStatus 
//            end
//            )
//        from dbo.EXAMRESULT t1
//        inner join 
//        ( select d.*,p.subjectname from	dbo.examsubjectresult d 
//            inner join dbo.view_postinfo p on d.postid = p.subjectid where d.examplanid=@EXAMPLANID and d.postid = @postid
//        ) t2 on t1.EXAMCARDID=t2.EXAMCARDID  and t1.STATUS='成绩已公告';

//        FETCH NEXT FROM mycur INTO @postid
//    end
//    CLOSE mycur
//    DEALLOCATE mycur
//    return 1
//END;
//GO
               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "考试成绩公告失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "公告考试成绩", string.Format("考试计划：{0}。", ExamPlanSelect1.ExamPlanName));

            UIHelp.layerAlert(Page, "考试成绩公告成功！",6,3000);
            RadGridExamResult.DataBind();
            BtnPublish.Enabled = false;

        }


        //导出通过考试考生名单
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            //if (RadGridExamResult.MasterTableView.VirtualItemCount == 0)
            //{
            //    UIHelp.layerAlert(Page, "没有可导出的数据！");
            //    return;
            //}
            //RadGridExamResult.MasterTableView.Columns.FindByUniqueName("Status").Visible = false;
            //isExcelExport = true;
            //RadGridExamResult.PageSize = RadGridExamResult.MasterTableView.VirtualItemCount;//
            //RadGridExamResult.CurrentPageIndex = 0;
            //RadGridExamResult.Rebind();
            //RadGridExamResult.ExportSettings.ExportOnlyData = true;
            //RadGridExamResult.ExportSettings.OpenInNewWindow = true;
            //RadGridExamResult.MasterTableView.ExportToExcel();


            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB q = GetQueryParamOB();
            string sortBy = "ExamSignUpID";
            string colHead = @"岗位类别\岗位工种\准考证号\考生姓名\证件号码\工作单位\报名地点";
            string colName = @"PostTypeName\PostName\ExamCardID\WorkerName\WorkerCertificateCode\UnitName\S_TrainUnitName";

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/成绩公告{1}_{0}.xls", PersonID.ToString(), DateTime.Now.ToString("yyMMddHHmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMSCORE_NEW", q.ToWhereString(), sortBy, colHead, colName);

             
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出成绩公告失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("成绩公告下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            if (RadGridExamResult.Items.Count > 0)
            {
                ButtonPrint.Enabled = true;
                if (RadGridExamResult.MasterTableView.DataKeyValues[0]["Status"].ToString() == EnumManager.ExamResultStatus.UnPublished)
                {
                    BtnPublish.Enabled = true;
                }
                else
                {
                    BtnPublish.Enabled = false;
                }
            }
            else
            {
                ButtonPrint.Enabled = false;
                BtnPublish.Enabled = false;
            }
        }

        ////导出excel格式化
        //protected void RadGridExamResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        //{
        //    switch (e.FormattedColumn.UniqueName)
        //    {
        //        case "ExamCardID": e.Cell.Style["mso-number-format"] = @"\@"; break;
        //        case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;

        //    }
        //    //HeadCell
        //    GridItem item = e.Cell.Parent as GridItem;
        //    if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "ExamCardID")
        //    {
        //        GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
        //        GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
        //        for (int i = 0; i < ghi.Cells.Count; i++)
        //        {
        //            ghi.Cells[i].Style.Add("border-width", "0.1pt");
        //            ghi.Cells[i].Style.Add("border-style", "solid");
        //            ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
        //        }
        //    }
        //    //Itemcell
        //    e.Cell.Attributes.Add("align", "center");
        //    e.Cell.Style.Add("border-width", "0.1pt");
        //    e.Cell.Style.Add("border-style", "solid");
        //    e.Cell.Style.Add("border-color", "#CCCCCC");            
        //}

    }
}
