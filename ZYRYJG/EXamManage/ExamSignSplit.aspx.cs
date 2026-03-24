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
    public partial class ExamSignSplit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ExamPlanID"] = Request["o"];

                //绑定参与报名的培训点
                DataTable trainUnit = CommonDAL.GetDataTable(string.Format("select distinct TRAINUNITID,S_TRAINUNITNAME FROM DBO.EXAMSIGNUP where EXAMPLANID={0} and status='已缴费'", ViewState["ExamPlanID"].ToString()));
                CheckBoxListTrainUnit.DataSource = trainUnit;
                CheckBoxListTrainUnit.DataTextField = "S_TRAINUNITNAME";
                CheckBoxListTrainUnit.DataValueField = "TRAINUNITID";
                CheckBoxListTrainUnit.DataBind();

                ////绑定参与报名的企业
                //DataTable Unit = CommonDAL.GetDataTable(string.Format("select distinct UNITNAME,UNITCODE FROM DBO.EXAMSIGNUP where EXAMPLANID={0}", ViewState["ExamPlanID"].ToString()));
                //CheckBoxListUnitName.DataSource = Unit;
                //CheckBoxListUnitName.DataTextField = "UNITNAME";
                //CheckBoxListUnitName.DataValueField = "UNITCODE";
                //CheckBoxListUnitName.DataBind();

                BindRadGridOld();

                BindRadGridNew();

                UIHelp.layerAlert(Page, "拆分考试计划前请提前做好原考试计划报名审核通过率等统计结果，拆分后新的考试计划审核通过率将视为100%。");
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindRadGridOld();
        }

        private void BindRadGridOld()
        {
            ObjectDataSourceOld.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add("ExamPlanID=" + ViewState["ExamPlanID"].ToString());
            q.Add("status='已缴费'");

            if (RadioButtonListSearchBy.SelectedValue == "1")
            {
                //培训点名称
                string TrainUnit = GetCheckBoxListTrainUnitSelect();
                if (TrainUnit != "")
                {
                    q.Add(TrainUnit.Replace("s_TrainUnitName", "TrainUnitName"));
                }
            }
            else
            {
                //按人数
                if (RadNumericTextBoxManCount.Value > 0)
                {
                    q.Add(string.Format(" examsignupid < (select max(examsignupid) +1 from (sELECT top {1} examsignupid FROM DBO.VIEW_EXAMSIGNUP_NEW where ExamPlanID={0} and status='已缴费' order by examsignupid ) t)", ViewState["ExamPlanID"], Convert.ToInt32(RadNumericTextBoxManCount.Value)));
                }
            }

            ObjectDataSourceOld.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridOld.CurrentPageIndex = 0;
            RadGridOld.DataSourceID = ObjectDataSourceOld.ID;

            ExamPlanOB oldExamPlanOB = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"]));

            LabelOldExamPlan.Text = string.Format("{0}【岗位：{1}，工种：{2}】", oldExamPlanOB.ExamPlanName, oldExamPlanOB.PostTypeName, oldExamPlanOB.PostName);
        }
        
        //移动人员
        protected void ButtonMove_Click(object sender, EventArgs e)
        {
            if (RadGridOld.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可移动的人员！");
                return;
            }
            if (ExamPlanSelectNew.ExamPlanID.HasValue == false)
            {
                UIHelp.layerAlert(Page, "请选择一个要拆分到的新考试计划！");
                return;
            }
            ExamPlanOB oldExamPlanOB = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"]));
            ExamPlanOB newExamPlanOB = ExamPlanDAL.GetObject(ExamPlanSelectNew.ExamPlanID.Value);
            if (oldExamPlanOB.PostID.Value != newExamPlanOB.PostID.Value)
            {
                UIHelp.layerAlert(Page, "两个考试计划不是同一工种，无法拆分人员！");
                return;
            }

            int rowCount = CommonDAL.SelectRowCount("dbo.EXAMRESULT"," and EXAMPLANID="+ViewState["ExamPlanID"].ToString() );
            if (rowCount > 0)
            {
                UIHelp.layerAlert(Page, "原考试计划已经发放了准考证，无法拆分人员！");
                return;
            }
            rowCount = CommonDAL.SelectRowCount("dbo.EXAMRESULT", " and EXAMPLANID=" + ExamPlanSelectNew.ExamPlanID.ToString());
            if (rowCount > 0)
            {
                UIHelp.layerAlert(Page, "新考试计划已经发放了准考证，无法接收拆分人员！");
                return;
            }

            string sql=@"update dbo.examsignup set examplanid = {0}
                         where examplanid = {1} and status='已缴费' and {2} ";

            try
            {

                if (RadioButtonListSearchBy.SelectedValue == "1")//培训点名称
                {
                    CommonDAL.ExecSQL(string.Format(sql, ExamPlanSelectNew.ExamPlanID, ViewState["ExamPlanID"], GetCheckBoxListTrainUnitSelect()));
                }
                else//按人数
                {
                    CommonDAL.ExecSQL(string.Format(sql, ExamPlanSelectNew.ExamPlanID, ViewState["ExamPlanID"],
                       string.Format("examsignupid in(SELECT  top {1} examsignupid FROM DBO.VIEW_EXAMSIGNUP_NEW where ExamPlanID={0} and status='已缴费' order by examsignupid)", ViewState["ExamPlanID"], Convert.ToInt32(RadNumericTextBoxManCount.Value))
                        ));
                    
                }

                
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "拆分考试计划失败！", ex);
                return;
            }

            CopySignupPhoto(ViewState["ExamPlanID"].ToString(), ExamPlanSelectNew.ExamPlanID.ToString());
            BindRadGridOld();

            BindRadGridNew();

            UIHelp.layerAlert(Page, "拆分计划成功！",6,3000);
        }

        /// <summary>
        /// 复制报名图片到拆分目的目录
        /// </summary>
        /// <param name="oldExamPlanID"></param>
        /// <param name="newExamPlanID"></param>
        protected void CopySignupPhoto(string oldExamPlanID,string newExamPlanID)
        {
            try
            {
                string pathFrom = "~/UpLoad/SignUpPhoto/" + oldExamPlanID;
                string pathTo = "~/UpLoad/SignUpPhoto/" + newExamPlanID;

                if (!Directory.Exists(Page.Server.MapPath(pathFrom)))
                {
                    return;//不存在照片
                }
                if (!Directory.Exists(Page.Server.MapPath(pathTo)))//创建目标目录
                {
                    System.IO.Directory.CreateDirectory(Page.Server.MapPath(pathTo));
                }
                //string[] files = System.IO.Directory.GetFiles(Page.Server.MapPath(pathFrom));
                DirectoryInfo dir = new DirectoryInfo(Page.Server.MapPath(pathFrom));
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    File.Copy(i.FullName, Server.MapPath(string.Format("{0}\\{1}", pathTo, i.Name)),true);
                }

            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("复制报名照片到拆分考试计划失败！", ex);
                return;
            }
        }

        //变换新考试计划
        protected void ExamPlanSelectNew_Changed(object sender, EventArgs e)
        {
            BindRadGridNew();
        }

        private void BindRadGridNew()
        {
            ObjectDataSourceNew.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (ExamPlanSelectNew.ExamPlanID.HasValue == true)
            {
                q.Add("ExamPlanID=" + ExamPlanSelectNew.ExamPlanID.ToString());
                q.Add("status='已缴费'");
            }
            else
            {
                q.Add("ExamPlanID=-999");
            }
            ObjectDataSourceNew.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridNew.CurrentPageIndex = 0;
            RadGridNew.DataSourceID = ObjectDataSourceNew.ID;
        }

        //获取勾选培训点
        private string GetCheckBoxListTrainUnitSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach(ListItem l in CheckBoxListTrainUnit.Items)
            {
                if (l.Selected == true)
                {
                    sb.Append(" or s_TrainUnitName like '").Append(l.Text).Append("'");
                }
            }
            if(sb.Length>0)
            {
                sb.Remove(0, 3).Insert(0, "(").Append(")");
            }
            else
            {
                sb.Append("1=1");
            }
            return sb.ToString();
        }

        protected void RadioButtonListSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonListSearchBy.SelectedValue=="0")
            {
                tr_TrainUnit.Visible = false;
                tr_ManCount.Visible = true;
              
            }
            else
            {
                tr_TrainUnit.Visible = true;
                tr_ManCount.Visible = false;
            }
        }

        ////获取勾选企业
        //private string GetCheckBoxListUnitNameSelect()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    foreach (ListItem l in CheckBoxListUnitName.Items)
        //    {
        //        if (l.Selected == true)
        //        {
        //            sb.Append(" or UNITNAME like '").Append(l.Text).Append("'");
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        sb.Remove(0, 3).Insert(0, "(").Append(")");
        //    }
        //    return sb.ToString();
        //}

    }
}
