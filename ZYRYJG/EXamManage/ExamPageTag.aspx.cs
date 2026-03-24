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
    public partial class ExamPageTag : BasePage
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
                            RadTreeNode n = new RadTreeNode(string.Format("{0}年",dr["ExamYear"].ToString()),dr["ExamYear"].ToString());
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
                             , UIHelp.FormatPostNameByExamplanName(Convert.ToInt32(dr["PostID"]), dr["PostName"].ToString(), dr["ExamPlanName"].ToString())
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
                    dt = CommonDAL.GetDataTable(string.Format("select * from dbo.VIEW_ExamPageTag where PostTypeID={0} and ExamStartTime between '{1}' and '{2} 23:59:59' and Subject <>'实操' and [STATUS] = '{3}' order by Subject,ExamCardIDFromTo", e.Node.ParentNode.ParentNode.Value, e.Node.Value, e.Node.Value, EnumManager.ExamRoomAllotStatus.AllotExamered));
                    
                    RadGridExamResult.DataSource = dt;
                    RadGridExamResult.DataBind();
                    break;
                case 3://叶子结点，即考试计划
                    dt = CommonDAL.GetDataTable(string.Format("select * from dbo.VIEW_ExamPageTag where ExamplanID={0}  and Subject <>'实操'  and [STATUS] = '{1}' order by Subject,ExamCardIDFromTo", e.Node.Value, EnumManager.ExamRoomAllotStatus.AllotExamered));
                   
                    RadGridExamResult.DataSource = dt;
                    RadGridExamResult.DataBind();
                    RadScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hideTree", "hideTree();", true);
                    break;
            }
        }

        //卷标Grid绑定数据后
        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            if (RadGridExamResult.DataSource == null || RadGridExamResult.Items.Count == 0)
            {
                ButtonPrint.Visible = false;
                ButtonPrint1.Visible = false;
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
                RadGridExamResult.Visible = true;
                ButtonPrint1.Visible = true;
                div_tip.Visible = false;
                if (RadGridExamResult.Items.Count > 10)
                    ButtonPrint.Visible = true;
                else
                    ButtonPrint.Visible = false;
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
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/卷标.doc"
                , string.Format("~/UpLoad/SignUpTable/卷标_{0}_{1}.doc",RadTreeViewExamPlan.SelectedNode.Text.Replace("/","_"), PersonID.ToString())
                , GetPrintData());
            RadScriptManager.RegisterStartupScript(this, this.GetType(), "hideTree", "hideTree();", true);

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl(string.Format("卷标_{0}", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"))
                , string.Format("~/UpLoad/SignUpTable/卷标_{0}_{1}.doc", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"), PersonID.ToString())));

            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            string rowNo = "";//当前第几行
            for (int i = 0; i < RadGridExamResult.Items.Count; i++)
            {
                if (i == 0 || (i >= 3 && (i - 3) % 3 == 0))
                {
                    printData = new Dictionary<string, string>();
                    list.Add(printData);
                }
                if (i >= 3)
                {
                    rowNo = ((i - 3) % 3 +1).ToString();
                }
                else
                {
                    rowNo = (i % 3 + 1).ToString();
                }
                printData.Add(string.Format("PostName{0}", rowNo),((System.Web.UI.DataBoundLiteralControl)RadGridExamResult.Items[i]["PostName"].Controls[0]).Text.Trim());//岗位
                printData.Add(string.Format("Subject{0}", rowNo), RadGridExamResult.Items[i]["Subject"].Text);//科目
                printData.Add(string.Format("ExamTime{0}", rowNo), (RadGridExamResult.Items[i].FindControl("ExamTime") as Literal).Text);//考试时间
                printData.Add(string.Format("R{0}", rowNo), RadGridExamResult.Items[i]["ExamRoomCode"].Text);//考场编号
                printData.Add(string.Format("PersonNumber{0}", rowNo), RadGridExamResult.Items[i]["PersonNumber"].Text);//人数
                printData.Add(string.Format("ExamPlaceName{0}", rowNo), RadGridExamResult.Items[i]["ExamPlaceName"].Text);//考点名称
            }


            if (RadGridExamResult.Items.Count < 3)//首页有空行
            {
                Utility.WordDelHelp.ReplaceLabelOfNullRow(list[0], "PostName,Subject,ExamTime,R,PersonNumber,ExamPlaceName", RadGridExamResult.Items.Count + 1, 3);
            }
            else if ((RadGridExamResult.Items.Count - 3) % 3 != 0)//最后一页有空行
            {
                Utility.WordDelHelp.ReplaceLabelOfNullRow(list[list.Count - 1], "PostName,Subject,ExamTime,R,PersonNumber,ExamPlaceName", (RadGridExamResult.Items.Count % 3 + 1), 3);
            }
            return list;
        }
    }
}
