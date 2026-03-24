using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Utility;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class AcceptLineManage : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadGridExamSubjectResult.ExportSettings.FileName = Server.UrlEncode("考试合格率统计");
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    ExamPlanOB ob = ExamPlanDAL.GetObject(Convert.ToInt64(Request["o"]));
                    ExamPlanSelect1.SetValue(ob);
                }
                RefreshGrid(0);
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (ExamPlanSelect1.ExamPlanID.HasValue == false)
            {
                UIHelp.layerAlert(this.Page, "必须选择一个考试名称才能查询！");
                return;
            }
            RefreshGrid(0);
           
            //多科目考试计划没有录入所有科目成绩，不允许设置合格线
            int count = ExamPlanDAL.CheckExamPlanScoreFinish(ExamPlanSelect1.ExamPlanID.Value);
            if (count > 0)
            {
                div_tip.InnerText = string.Format("注意：本考试计划尚有{0}科成绩没有录入，请录入成绩后在设定合格线。(特例：特种作业可先只录入理论成绩并设定合格线，学员可以查看单科理论成绩,但不生成综合成绩)", count);
            }
            else
            {
                div_tip.InnerText = "";
            }
        }

        //设置datatable后两列为0
        protected void ConvertNullToZero(DataTable dt)
        {
            int columnCount = dt.Columns.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][columnCount - 1] = 0;
                dt.Rows[i][columnCount - 2] = 0;
            }
        }

        //刷新Grid
        protected void RefreshGrid(int pageIndex)
        {
            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
                       
            //培训点考生统计
            DataTable dtMain = CommonDAL.GetDataTable(string.Format(@"
select t.*,u.RelUserName,row_number() over(order by  u.RelUserName) rn from 
(
SELECT TrainUnitID as UnitID,'{0}' as PostName,count(*) as ExamerCount FROM dbo.ExamSignUp
where ExamPlanID = {1} and TrainUnitID is not null and [STATUS]='已缴费'
group by TrainUnitID
) as t 
left join dbo.[User] as u on t.UnitID = u.UserID

", ExamPlanSelect1.PostName, _ExamPlanID));
                
                //CommonDAL.GetDataTable(0, int.MaxValue - 1
                //, string.Format("(SELECT TrainUnitID as UnitID,'{0}' as PostName,count(*) as ExamerCount FROM dbo.ExamSignUp", ExamPlanSelect1.PostName)
                //, "t.*,u.RelUserName"
                ////, string.Format(" and ExamPlanID = {0} and TrainUnitID is not null and [STATUS]='已缴费' group by TrainUnitID) as t left join dbo.[User] as u on u.OrganID=245 and t.UnitID = u.UserID", _ExamPlanID)
                // , string.Format(" and ExamPlanID = {0} and TrainUnitID is not null and [STATUS]='已缴费' group by TrainUnitID) as t left join dbo.[User] as u on t.UnitID = u.UserID", _ExamPlanID)
                //, "u.RelUserName");

            int AllExamerCount = 0;//所有报名人数

            if (_ExamPlanID != "0")
            {
                //全部考生统计(UnitID=0)
                //DataTable dtAll = CommonDAL.GetDataTable(0, 1, "dbo.ExamSignUp", "0 as UnitID,'合计' as PostName,count(*) as ExamerCount,'全部' as RelUserName"
                //    , string.Format(" and 1=1  and ExamPlanID = {0} and [STATUS]='已缴费'", _ExamPlanID), "UnitID");
                DataTable dtAll = CommonDAL.GetDataTable( string.Format("select top(1) null as rn, 0 as UnitID,'合计' as PostName,count(*) as ExamerCount,'全部' as RelUserName from dbo.ExamSignUp where ExamPlanID = {0} and [STATUS]='已缴费'", _ExamPlanID));
                if (dtAll != null)
                {
                    //dtAll.Rows[0]["RowNum"] = DBNull.Value;
                    AllExamerCount = Convert.ToInt32(dtAll.Rows[0]["ExamerCount"]);
                    dtMain.ImportRow(dtAll.Rows[0]);
                }
            }

            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dtMain.Columns["UnitID"];
            dtMain.PrimaryKey = dcKeys;

            //添加考试科目列到考生信息table
            DataTable dtExamSubject = CommonDAL.GetDataTable(string.Format(
                @"select 
                    dbo.PostInfo.PostID as SubjectID,
                    dbo.PostInfo.PostName,
                    dbo.PostInfo.ExamFee as ExamFeeNormal,
                    dbo.ExamPlanSubject.*,
                    row_number() over(order by  PostInfo.PostName) rn
                  from dbo.PostInfo 
                  inner join dbo.ExamPlanSubject 
                    on dbo.ExamPlanSubject.ExamPlanID={0} 
                    and dbo.PostInfo.PostID = dbo.ExamPlanSubject.PostID
                    ", _ExamPlanID) );
                
                //CommonDAL.GetDataTable(0, int.MaxValue - 1
                //, string.Format("dbo.PostInfo inner join dbo.ExamPlanSubject on dbo.ExamPlanSubject.ExamPlanID={0} and dbo.PostInfo.PostID = dbo.ExamPlanSubject.PostID", _ExamPlanID)
                //, "dbo.PostInfo.PostID as SubjectID,dbo.PostInfo.PostName,dbo.PostInfo.ExamFee as ExamFeeNormal,dbo.ExamPlanSubject.*", "", "PostName");
           
            System.Text.StringBuilder header = new System.Text.StringBuilder();//grid头显示字段
            header.Append("空1#空2#序号#培训点#岗位工种#考试人数");

            //定义grid常规显示列
            RadGridExamSubjectResult.Columns.Clear();
            AddBoundColumn("序号", "RowNum");
            AddBoundColumn("培训点", "RelUserName", 20, HorizontalAlign.Left);
            AddBoundColumn("岗位工种", "PostName", 20, HorizontalAlign.Left);
            AddBoundColumn("考试人数", "ExamerCount");
            if (dtMain.Rows.Count > 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    
                    foreach (DataRow dr in dtExamSubject.Rows)
                    {
                        header.Append(string.Format("#分数{0}以上", (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString()));
                        header.Append(string.Format(" <nobr>{0}</nobr> 合格率(%),合格人数", dr["PostName"].ToString()));
                                               dtMain.Columns.Add("PassPercent" + i.ToString() + dr["PostID"].ToString(), typeof(decimal));//合格率
                        dtMain.Columns.Add("PassCount" + i.ToString() + dr["PostID"].ToString(), typeof(int));//合格人数   
                        ConvertNullToZero(dtMain);
                        AddBoundColumn("合格率", "PassPercent" + i.ToString() + dr["PostID"].ToString());
                        AddBoundColumn("合格人数", "PassCount" + i.ToString() + dr["PostID"].ToString());
                    }
                    if (dtExamSubject.Rows.Count > 1)//多科目显示总体合格率
                    {
                        header.Append(string.Format("#分数{0}以上", (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString()));
                        header.Append(" 合计 合格率(%),合格人数");
                        dtMain.Columns.Add("PassPercent" + i.ToString(), typeof(decimal));//合格率
                        dtMain.Columns.Add("PassCount" + i.ToString(), typeof(int));//合格人数   
                        ConvertNullToZero(dtMain);
                        AddBoundColumn("合格率", "PassPercent" + i.ToString() );
                        AddBoundColumn("合格人数", "PassCount" + i.ToString() );
                    }
                }

                ViewState["header"] = header.ToString();

                //读取统计结果
                DataTable dtScore = null;
                DataRow drFind;
                //参考线
                for (int i = 1; i <= 3; i++)
                {
                    //按培训点Group by统计各科目通过人数及比率
                    dtScore = ExamResultDAL.GetPassCountList(_ExamPlanID, (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString());
                    foreach (DataRow dr in dtScore.Rows)
                    {
                        drFind = dtMain.Rows.Find(dr["TrainUnitID"]);
                        if (drFind != null)
                        {
                            drFind["PassCount" + i.ToString() + dr["PostID"].ToString()] = dr["ExamerCount"];
                            drFind["PassPercent" + i.ToString() + dr["PostID"].ToString()] = Math.Round(Convert.ToDouble(dr["ExamerCount"]) / Convert.ToDouble(drFind["ExamerCount"]) * 100, 1);
                        }
                    }

                    //按培训点Group by统计通过人数及比率（不分科目）
                    if (dtExamSubject.Rows.Count > 1)
                    {
                        dtScore = ExamResultDAL.GetPassCountListAllSubject(_ExamPlanID, (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString(), dtExamSubject.Rows.Count.ToString());
                        foreach (DataRow dr in dtScore.Rows)
                        {
                            drFind = dtMain.Rows.Find(dr["TrainUnitID"]);
                            if (drFind != null)
                            {
                                drFind["PassCount" + i.ToString()] = dr["ExamerCount"];
                                drFind["PassPercent" + i.ToString()] = Math.Round(Convert.ToDouble(dr["ExamerCount"]) / Convert.ToDouble(drFind["ExamerCount"]) * 100, 1);
                            }
                        }
                    }

                    //统计全部
                    dtScore = ExamResultDAL.GetPassCountListAll(_ExamPlanID, (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString());
                    foreach (DataRow dr in dtScore.Rows)
                    {
                        //全部
                        drFind = dtMain.Rows.Find(0);
                        if (drFind != null)
                        {
                            drFind["PassCount" + i.ToString() + dr["PostID"].ToString()] = dr["ExamerCount"];
                            drFind["PassPercent" + i.ToString() + dr["PostID"].ToString()] = Math.Round(Convert.ToDouble(dr["ExamerCount"]) / Convert.ToDouble(drFind["ExamerCount"]) * 100, 1);
                        }
                    }
                    //按培训点Group by统计通过人数及比率（不分科目）
                    if (dtExamSubject.Rows.Count > 1)
                    {
                        //全部
                        drFind = dtMain.Rows.Find(0);
                        if (drFind != null)
                        {
                            drFind["PassCount" + i.ToString()] = ExamResultDAL.GetPassCountListAllSubjectSum(_ExamPlanID, (tdPassLine.FindControl("RadNumericTextBoxLine" + i.ToString()) as RadNumericTextBox).Value.ToString(), dtExamSubject.Rows.Count.ToString());
                            if (Convert.ToDouble(drFind["ExamerCount"]) == 0)
                            {
                                drFind["PassPercent" + i.ToString()] = 0;
                            }
                            else
                            {
                                drFind["PassPercent" + i.ToString()] = Math.Round(Convert.ToDouble(drFind["PassCount" + i.ToString()]) / Convert.ToDouble(drFind["ExamerCount"]) * 100, 1);
                            }
                        }
                    }
                }
            }

            //绑定数据
            RadGridExamSubjectResult.DataSource = dtMain;
            RadGridExamSubjectResult.DataBind();

            if (RadGridExamSubjectResult.Items.Count > 0)
            {
                ButtonOutput.Enabled = true;
                spanSetPassLine.Disabled = false;
            }
            else
            {
                ButtonOutput.Enabled = false;
                spanSetPassLine.Disabled = true;
            }
        }

        protected void RadGridExamSubjectResult_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ViewState["header"] != null)
                {
                    DynamicTHeaderHepler dHelper = new DynamicTHeaderHepler();
                    string header = ViewState["header"].ToString();
                    dHelper.SplitTableHeader(e.Item, header);
                }
            }
        }

        //为grid添加绑定列
        protected void AddBoundColumn(string headerText, string dataField, double? width, HorizontalAlign? _HorizontalAlign)
        {
            if (RadGridExamSubjectResult.Columns.Contains(dataField) == false)
            {
                GridBoundColumn boundColumn = new GridBoundColumn();
                RadGridExamSubjectResult.MasterTableView.Columns.Add(boundColumn);
                boundColumn.UniqueName = dataField;
                boundColumn.DataField = dataField;
                boundColumn.HeaderText = headerText;
                if (width.HasValue) boundColumn.ItemStyle.Width = System.Web.UI.WebControls.Unit.Percentage(width.Value);
                if (_HorizontalAlign.HasValue) boundColumn.ItemStyle.HorizontalAlign = _HorizontalAlign.Value;
                //boundColumn.ItemStyle.Wrap = false;
            }
        }

        protected void AddBoundColumn(string headerText, string dataField)
        {
            AddBoundColumn(headerText, dataField, null,null);
        }

        //导出成绩
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            //RadGridExamSubjectResult.ExportSettings.IgnorePaging = CheckBox2.Checked;  //导出翻页所有的数据，如果省了，则下面导出的是显示页的数据
            isExcelExport = true;
            RefreshGrid(0);
            RadGridExamSubjectResult.ExportSettings.OpenInNewWindow = true;
            RadGridExamSubjectResult.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGridExamSubjectResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex <= 2 )
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");

                    if (ghi.Cells[i].Controls.Count > 0)//多表头
                    {
                        if (ghi.Cells[i].Controls[0].GetType() == typeof(Literal))
                        {
                            Literal lc = ghi.Cells[i].Controls[0] as Literal;
                            lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; font-weight:bold;' ");
                        }
                        else if (ghi.Cells[i].Controls[0].GetType() == typeof(LiteralControl))
                        {
                            LiteralControl lc = ghi.Cells[i].Controls[0] as LiteralControl;
                            lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; font-weight:bold;' ");
                        }
                    }
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
            if (e.Cell.Controls.Count > 0)
            {
                if (e.Cell.Controls[0].GetType() == typeof(Literal))
                {
                    Literal lc = e.Cell.Controls[0] as Literal;
                    lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; ' ");
                }
                else if (e.Cell.Controls[0].GetType() == typeof(LiteralControl))
                {
                    LiteralControl lc = e.Cell.Controls[0] as LiteralControl;
                    lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; ' ");
                }
            }
        }
      
    }
}
