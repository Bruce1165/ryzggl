using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Threading;

namespace ZYRYJG.EXamManage
{
    public partial class ExamRoomTag : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Collections.Generic.Dictionary<string, string> post = new System.Collections.Generic.Dictionary<string, string> { {"安全生产考核三类人员","1"}
                    ,{"建筑施工特种作业", "2"}
                     ,{"造价员", "3"}
                      ,{"建设职业技能岗位", "4"}
                       ,{"关键岗位专业技术管理人员", "5"}
                };

                foreach (string k in post.Keys)
                {
                    RadTreeNode n = new RadTreeNode(k, post[k]);
                    n.ImageUrl = "../Images/man.png";
                    n.ExpandMode = TreeNodeExpandMode.ServerSide;
                    n.Expanded = false;
                    this.RadTreeViewExamPlan.Nodes.Add(n);
                }
                RadScriptManager.RegisterStartupScript(this, this.GetType(), "hideTree", "hideTree();", true);
            }
        }

        //展开考试计划树
        protected void RadTreeViewExamPlan_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)//保证只同时展开一个根节点
            {
                foreach (RadTreeNode n in RadTreeViewExamPlan.Nodes)
                {
                    if (e.Node.UniqueID != n.UniqueID && n.Expanded == true)
                    {
                        n.CollapseChildNodes();
                        n.Expanded = false;
                    }
                }
            }
            if (e.Node.Nodes.Count == 0)
            {
                DataTable dt = null;
                switch (e.Node.Level)
                {
                    case 0://展开岗位，显示年度 
                        dt = CommonDAL.GetDataTable(string.Format("select distinct year(EXAMSTARTDATE) as ExamYear from dbo.ExamPlan where PostTypeID={0} order by ExamYear desc", e.Node.Value));
                        foreach (DataRow dr in dt.Rows)
                        {
                            RadTreeNode n = new RadTreeNode(string.Format("{0}年", dr["ExamYear"].ToString()), dr["ExamYear"].ToString());
                            n.ImageUrl = "../Images/time.png";
                            n.ExpandMode = TreeNodeExpandMode.ServerSide;
                            n.Expanded = false;
                            e.Node.Nodes.Add(n);
                        }
                        break;
                    case 1://展开年度，显示日期
                        dt = CommonDAL.GetDataTable(string.Format(@"
                            select 
                                distinct replace(CONVERT(varchar(10), EXAMSTARTDATE, 20),'-','.') +'日' as text,
                                CONVERT(varchar(10), EXAMSTARTDATE, 20) as value
                            from dbo.ExamPlan 
                            where PostTypeID={0} and EXAMSTARTDATE between '{1}' and '{2}' order by value desc"
                            , e.Node.ParentNode.Value
                            , string.Format("{0}-1-1", e.Node.Value), DateTime.Parse(string.Format("{0}-1-1", Convert.ToString(Convert.ToInt32(e.Node.Value) + 1))).AddDays(-1).ToString("yyyy-MM-dd"))
                            );
                        foreach (DataRow dr in dt.Rows)
                        {
                            RadTreeNode n = new RadTreeNode(dr["text"].ToString(), dr["value"].ToString());
                            n.ImageUrl = "../Images/files.png";
                            n.ExpandMode = TreeNodeExpandMode.ServerSide;
                            n.Expanded = false;
                            e.Node.Nodes.Add(n);
                        }
                        break;
                    case 2://展开日期，显示考试计划
                        dt = CommonDAL.GetDataTable(
                             string.Format("select ExamPlanName,PostName,ExamStartDate,ExamPlanID,PostID from dbo.ExamPlan where PostTypeID={0} and EXAMSTARTDATE between '{1}' and '{2}' order by ExamStartDate desc,ExamPlanID"
                            , e.Node.ParentNode.ParentNode.Value
                            , e.Node.Value, e.Node.Value)
                            );
                        foreach (DataRow dr in dt.Rows)
                        {
                            RadTreeNode n = new RadTreeNode(string.Format("{0} {1}", Convert.ToDateTime(dr["ExamStartDate"]).ToString("yyyy-MM-dd")
                                  ,UIHelp.FormatPostNameByExamplanName(Convert.ToInt32(dr["PostID"]), dr["PostName"].ToString(), dr["ExamPlanName"].ToString())
                                )
                                , dr["ExamPlanID"].ToString());
                            n.ToolTip = dr["ExamPlanName"].ToString();
                            n.ImageUrl = "../Images/plan.png";
                            n.Expanded = true;
                            n.ExpandMode = TreeNodeExpandMode.ServerSide;
                            e.Node.Nodes.Add(n);
                        }
                        break;
                }
            }
        }

        //点击考试计划树节点
        protected void RadTreeViewExamPlan_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Expanded == false)//点击展开
            {
                RadTreeViewExamPlan_NodeExpand(sender, e);
                e.Node.Expanded = true;
            }

            DataTable dt = null;
            switch (e.Node.Level)
            {
                case 0:
                case 1:
                    RadGridExamResult.DataSource = null;
                    RadGridExamResult.DataBind();
                    break;
                case 2://同岗位同一天考试
                    dt = CommonDAL.GetDataTable(string.Format("select * from dbo.VIEW_ExamRoomTag where PostTypeID={0} and ExamStartDate between '{1}' and '{2} 23:59:59'  and [STATUS] = '{3}' order by ExamCardIDFromTo", e.Node.ParentNode.ParentNode.Value, e.Node.Value, e.Node.Value, EnumManager.ExamRoomAllotStatus.AllotExamered));
                    RadGridExamResult.DataSource = dt;
                    RadGridExamResult.DataBind();
                    break;
                case 3://叶子结点，即考试计划
                    dt = CommonDAL.GetDataTable(string.Format("select * from dbo.VIEW_ExamRoomTag where ExamplanID={0} and [STATUS] = '{1}' order by ExamCardIDFromTo", e.Node.Value, EnumManager.ExamRoomAllotStatus.AllotExamered));
                    RadGridExamResult.DataSource = dt;
                    RadGridExamResult.DataBind();
                    RadScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hideTree", "hideTree();", true);
                    break;
            }           
        }

        protected void ButtonOutputExcel_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据。");
                return;
            }
           
            RadGridExamResult.ExportSettings.ExportOnlyData = true;
            RadGridExamResult.ExportSettings.OpenInNewWindow = true;
            RadGridExamResult.MasterTableView.ExportToExcel();
            RadGridExamResult.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }

        //格式化Excel
        protected void RadGridExamResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {            
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "PostName")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
        }
        
        //卷标Grid绑定数据后
        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            //控制按钮显示
            if (RadGridExamResult.DataSource == null || RadGridExamResult.Items.Count == 0)
            {
                ButtonPrint.Visible = false;
                ButtonPrint1.Visible = false;
                ButtonOutputExcel.Visible = false;
                ButtonOutputExcel1.Visible = false;
                if (RadGridExamResult.DataSource == null)
                {
                    div_tip.Visible = true;
                    RadGridExamResult.Visible = false;
                 }
                else
                {
                    div_tip.Visible = false;
                    RadGridExamResult.Visible = true;
                }                
            }
            else
            {
                RadGridExamResult.Visible=true;
                ButtonPrint1.Visible = true;
                ButtonOutputExcel1.Visible = true;
                div_tip.Visible = false;
                if (RadGridExamResult.Items.Count > 10)
                {
                    ButtonPrint.Visible = true;
                    ButtonOutputExcel.Visible = true;
                }
                else
                {
                    ButtonPrint.Visible = false;
                    ButtonOutputExcel.Visible = false;
                }

                //格式化考试科目及时间

                //select s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME
                //from dbo.Examplan e inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid
                // inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid
                //where k.PostName <>'实操'
                //order by s.examplanid,s.EXAMSTARTTIME

                DataTable dt = null;//考试科目集合
                RadTreeNode node = RadTreeViewExamPlan.SelectedNode;
                System.Collections.Generic.Dictionary<string, string> examplan = new Dictionary<string, string>();
                System.Collections.Generic.Dictionary<string, string> examplanSubKemu = new Dictionary<string, string>();
                switch (node.Level)
                {
                    case 2://同岗位同一天考试
                        dt = CommonDAL.GetDataTable(
                 string.Format(@"select s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME 
                                from 
                                dbo.Examplan e 
                                inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid 
                                inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid
                                where e.PostTypeID={0} and e.ExamStartDate between '{1}' and '{2} 23:59:59' and k.PostName <>'实操' 
                                order by s.examplanid,s.EXAMSTARTTIME", node.ParentNode.ParentNode.Value, node.Value, node.Value)
                );
                        break;
                    case 3://叶子结点，即考试计划
                        dt = CommonDAL.GetDataTable(
                            string.Format(@"select s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME
                                            from 
                                            dbo.Examplan e 
                                            inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid 
                                            inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid 
                                            where e.ExamplanID={0} and k.PostName <>'实操' 
                                            order by s.examplanid,s.EXAMSTARTTIME", node.Value)
                );
                        break;
                }
                foreach (DataRow r in dt.Rows)
                {
                    if (examplan.ContainsKey(r["examplanid"].ToString()) == false)
                    {
                        examplan.Add(r["examplanid"].ToString(), string.Format("{0}-{1} {2}"
                            , Convert.ToDateTime(r["EXAMSTARTTIME"]).ToString("yyyy.MM.dd日 HH:mm")
                            , Convert.ToDateTime(r["EXAMENDTIME"]).ToString(" HH:mm")
                            , r["PostName"].ToString()));
                        examplanSubKemu.Add(r["examplanid"].ToString(),r["PostName"].ToString());
                    }
                    else
                    {
                        examplan[r["examplanid"].ToString()] += string.Format("<br />{0}-{1} {2}"
                            , Convert.ToDateTime(r["EXAMSTARTTIME"]).ToString("yyyy.MM.dd日 HH:mm")
                            , Convert.ToDateTime(r["EXAMENDTIME"]).ToString(" HH:mm")
                            , r["PostName"].ToString());

                        examplanSubKemu.Add(r["examplanid"].ToString(), "，" + r["PostName"].ToString());
                    }
                }

                foreach (GridItem i in RadGridExamResult.MasterTableView.Items)
                {
                    if (Convert.ToDateTime(RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamStartDate"]).ToString() == Convert.ToDateTime(RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamStartTime"]).ToString())//现场考试
                    {
                        i.Cells[RadGridExamResult.MasterTableView.Columns.FindByUniqueName("Subject").OrderIndex].Text = examplan[RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamPlanID"].ToString()];
                    }
                    else//上机考试
                    {
                        i.Cells[RadGridExamResult.MasterTableView.Columns.FindByUniqueName("Subject").OrderIndex].Text = string.Format("{0} - {1} {2}"
                            ,Convert.ToDateTime(RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamStartTime"]).ToString("yyyy年MM月dd日 HH:mm")
                            , Convert.ToDateTime(RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamEndTime"]).ToString("HH:mm")
                            , examplanSubKemu[RadGridExamResult.MasterTableView.DataKeyValues[i.ItemIndex]["ExamPlanID"].ToString()]);
                    }
                }
            }
        }

        //批量导出Word
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据。");
                return;
            }
            UIHelp.CheckCreateDirectory(this.Page, "~/UpLoad/SignUpTable/");
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/门标.doc"
                , string.Format("~/UpLoad/SignUpTable/门标_{0}_{1}.doc", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"), PersonID.ToString())
                , GetPrintData());
            RadScriptManager.RegisterStartupScript(this, this.GetType(), "hideTree", "hideTree();", true);

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl(string.Format("门标_{0}", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"))
                , string.Format("~/UpLoad/SignUpTable/门标_{0}_{1}.doc", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"), PersonID.ToString())));

            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            for (int i = 0; i < RadGridExamResult.Items.Count; i++)
            {
                printData = new Dictionary<string, string>();
                list.Add(printData);
                printData.Add("PostName", ((System.Web.UI.DataBoundLiteralControl)RadGridExamResult.Items[i]["PostName"].Controls[0]).Text.Trim());
                printData.Add("ExamPlaceName", RadGridExamResult.Items[i]["ExamPlaceName"].Text);
                printData.Add("Subject", RadGridExamResult.Items[i]["Subject"].Text.Replace("<br />", "</w:t></w:r></w:p><w:p wsp:rsidR=\"00A26BA9\" wsp:rsidRPr=\"00497B24\" wsp:rsidRDefault=\"004557A6\" wsp:rsidP=\"00497B24\"><w:pPr><w:ind w:right-chars=\"-18\" w:right=\"-58\"/><w:jc w:val=\"center\"/><w:rPr><w:rFonts w:ascii=\"宋体\" w:fareast=\"宋体\" w:h-ansi=\"宋体\"/><wx:font wx:val=\"宋体\"/><w:b/><w:sz w:val=\"48\"/><w:sz-cs w:val=\"48\"/></w:rPr></w:pPr><w:r wsp:rsidRPr=\"00497B24\"><w:rPr><w:rFonts w:ascii=\"宋体\" w:fareast=\"宋体\" w:h-ansi=\"宋体\"/><wx:font wx:val=\"宋体\"/><w:b/><w:sz w:val=\"48\"/><w:sz-cs w:val=\"48\"/></w:rPr><w:t>"));
                printData.Add("ExamRoomCode", RadGridExamResult.Items[i]["ExamRoomCode"].Text);
                printData.Add("PersonNumber", RadGridExamResult.Items[i]["PersonNumber"].Text);
                printData.Add("ExamCardIDFromTo", RadGridExamResult.Items[i]["ExamCardIDFromTo"].Text);
            }
            return list;
        }
    }
}