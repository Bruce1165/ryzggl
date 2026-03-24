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

namespace ZYRYJG.QuestionMgr
{
    public partial class MainExamOutline : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化已发布公告年度
                DataTable dtYear = CommonDAL.GetDataTable("select distinct ExamYear from dbo.ExamRange order by ExamYear desc");
                RadComboBoxYear.DataSource = dtYear;
                RadComboBoxYear.DataBind();

                BindRadGridPost();//绑定考试大纲Grid

                //string year =  RadComboBoxYear.SelectedValue==""?"2000":RadComboBoxYear.SelectedValue;
                ////显示科目列表（隐藏考试科目参考其它科目数据，即 SUBJECTID <> CodeForma）
                //DataTable dtPost = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_EXAMRANGE", "PostTypeName,PostName,SubjectName,SubjectID,EXAMYEAR,FLAG", string.Format(" and EXAMYEAR={0}", year), "PostTypeID,PostID,SubjectID");
                //RadGridPost.DataSource = dtPost;
                //RadGridPost.DataBind();

            }
        }

        //绑定考试大纲Grid
        private void BindRadGridPost()
        {
            string year = RadComboBoxYear.SelectedValue == "" ? "2000" : RadComboBoxYear.SelectedValue;
            string PostWhere = "";
            if (PostSelect.PostID != "")
                PostWhere = " and PostID=" + PostSelect.PostID;
            else if(PostSelect.PostTypeID != "")
                PostWhere = " and PostTypeID=" + PostSelect.PostTypeID;

            //显示科目列表（隐藏考试科目参考其它科目数据，即 SUBJECTID <> CodeForma）
            DataTable dtPost = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_EXAMRANGE", "PostTypeName,PostName,SubjectName,SubjectID,EXAMYEAR,FLAG,PostTypeID,PostID", string.Format(" and EXAMYEAR={0} {1}", year, PostWhere), "PostTypeID,PostID,SubjectID");
            RadGridPost.DataSource = dtPost;
            RadGridPost.DataBind();
        }


        //卷标Grid绑定数据后
        protected void RadGridInfoTag_DataBound(object sender, EventArgs e)
        {
            ////控制按钮显示
            //if (RadGridInfoTag.DataSource == null || RadGridInfoTag.Items.Count == 0)
            //{
            //    ButtonPrint.Visible = false;
            //    ButtonPrint1.Visible = false;
            //    ButtonOutputExcel.Visible = false;
            //    ButtonOutputExcel1.Visible = false;
            //    if (RadGridInfoTag.DataSource == null)
            //    {
            //        div_tip.Visible = true;
            //        RadGridInfoTag.Visible = false;
            //     }
            //    else
            //    {
            //        div_tip.Visible = false;
            //        RadGridInfoTag.Visible = true;
            //    }                
            //}
            //else
            //{
            //    RadGridInfoTag.Visible=true;
            //    ButtonPrint1.Visible = true;
            //    ButtonOutputExcel1.Visible = true;
            //    div_tip.Visible = false;
            //    if (RadGridInfoTag.Items.Count > 10)
            //    {
            //        ButtonPrint.Visible = true;
            //        ButtonOutputExcel.Visible = true;
            //    }
            //    else
            //    {
            //        ButtonPrint.Visible = false;
            //        ButtonOutputExcel.Visible = false;
            //    }

            //    //格式化考试科目及时间

            //    //select s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME
            //    //from dbo.Examplan e inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid
            //    // inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid
            //    //where k.PostName <>'实操'
            //    //order by s.examplanid,s.EXAMSTARTTIME

            //    DataTable dt = null;//考试科目集合
            //    RadTreeNode node = RadTreeViewExamPlan.SelectedNode;
            //    System.Collections.Generic.Dictionary<string, string> examplan = new Dictionary<string, string>();
            //    switch (node.Level)
            //    {
            //        case 2://同岗位同一天考试
            //            dt = CommonDAL.GetDataTable(0, int.MaxValue - 1
            //        , "dbo.Examplan e inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid"
            //        , "s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME"
            //    , string.Format(" and e.PostTypeID={0} and e.ExamStartDate between '{1}' and '{2} 23:59:59' and k.PostName <>'实操'", node.ParentNode.ParentNode.Value, node.Value, node.Value)
            //    , "s.examplanid,s.EXAMSTARTTIME");
            //            break;
            //        case 3://叶子结点，即考试计划
            //            dt = CommonDAL.GetDataTable(0, int.MaxValue - 1
            //        , "dbo.Examplan e inner join dbo.ExamPlanSubject s on e.examplanid = s.examplanid inner join dbo.postinfo k on k.posttype='3' and s.postid = k.postid"
            //        , "s.examplanid,k.PostName,s.EXAMSTARTTIME,s.EXAMENDTIME"
            //    , string.Format(" and e.ExamplanID={0} and k.PostName <>'实操'", node.Value)
            //    , "s.examplanid,s.EXAMSTARTTIME");
            //            break;
            //    }
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        if (examplan.ContainsKey(r["examplanid"].ToString()) == false)
            //        {
            //            examplan.Add(r["examplanid"].ToString(), string.Format("{0}-{1} {2}"
            //                , Convert.ToDateTime(r["EXAMSTARTTIME"]).ToString("yyyy.MM.dd日 HH:mm")
            //                , Convert.ToDateTime(r["EXAMENDTIME"]).ToString(" HH:mm")
            //                , r["PostName"].ToString()));
            //        }
            //        else
            //        {
            //            examplan[r["examplanid"].ToString()] += string.Format("<br />{0}-{1} {2}"
            //                , Convert.ToDateTime(r["EXAMSTARTTIME"]).ToString("yyyy.MM.dd日 HH:mm")
            //                , Convert.ToDateTime(r["EXAMENDTIME"]).ToString(" HH:mm")
            //                , r["PostName"].ToString());
            //        }
            //    }

            //    foreach (GridItem i in RadGridInfoTag.MasterTableView.Items)
            //    {
            //        i.Cells[RadGridInfoTag.MasterTableView.Columns.FindByUniqueName("Subject").OrderIndex].Text = examplan[RadGridInfoTag.MasterTableView.DataKeyValues[i.ItemIndex]["ExamPlanID"].ToString()];
            //    }
            //}
        }

        //批量导出Word
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //if (RadGridInfoTag.Items.Count == 0)
            //{
            //    UIHelp.layerAlert(Page, "没有可导出的数据。");
            //    return;
            //}
            //UIHelp.CheckCreateDirectory(this.Page, "~/UpLoad/SignUpTable/");
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/门标.doc"
            //    , string.Format("~/UpLoad/SignUpTable/门标_{0}_{1}.doc", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"), PersonID.ToString())
            //    , GetPrintData());
            //RadScriptManager.RegisterStartupScript(this, this.GetType(), "hideTree", "hideTree();", true);

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl(string.Format("门标_{0}", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"))
            //    , string.Format("~/UpLoad/SignUpTable/门标_{0}_{1}.doc", RadTreeViewExamPlan.SelectedNode.Text.Replace("/", "_"), PersonID.ToString())));

            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //protected void RadGridInfoTag_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.InfoTag", "*", string.Format(" and SubjectID={0}  and Flag = 1"
        //               , RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString()), "TagCode");
        //    RadGridInfoTag.DataSource = dt;
        //}

        //根据现实序号格式化返回数据库编号
        private int FormatTagCode(string ShowCode)
        {
            string rtn = "";
            int code = 0;
            try
            {
                string[] temp = ShowCode.Split('.');
                foreach (string s in temp)
                {
                    if (int.TryParse(Utility.Check.removeInputErrorChares(s), out code) == true)
                    {
                        if (code > 99 || code < 1) return -2;//每层编号有效范围1~99
                        rtn += code.ToString("00");
                    }
                    else
                    {
                        return -1;//无法转化为数字
                    }
                }
                return Convert.ToInt32((rtn + "00000000").Substring(0, 8));
            }
            catch
            {
                return -1;
            }
        }

        ////变换科目选择后
        //protected void RadGridPost_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RadGridInfoTag.Rebind();
        //    RadGridInfoTag.ExportSettings.FileName =Server.UrlEncode(string.Format("{0}-{1}-{2}-知识大纲" , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
        //        , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
        //        , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text));
        //    LabelSelectPost.Text = string.Format("当前科目：【{0} - {1} - {2}】"
        //        , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
        //        , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
        //        , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text);
        //}

        //新增试题
        protected void RadGridInfoTag_InsertCommand(object source, GridCommandEventArgs e)
        {
            InfoTagOB ob = new InfoTagOB();
            try
            {
                //科目ID
                ob.SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());

                //显示编码
                RadTextBox RadTextBoxShowCode = e.Item.FindControl("RadTextBoxShowCode") as RadTextBox;
                ob.ShowCode = RadTextBoxShowCode.Text.Trim();

                //大纲编码
                ob.TagCode = FormatTagCode(ob.ShowCode);
                if (ob.TagCode <= 0)
                {
                    UIHelp.layerAlert(Page, "序号格式错误，请检查输入！");
                    return;
                }

                //大纲标题
                RadTextBox RadTextBoxPageTitle = e.Item.FindControl("RadTextBoxTitle") as RadTextBox;
                ob.Title = RadTextBoxPageTitle.Text.Trim();

                //状态RadTextBoxFlag
                ob.Flag = 1;

                //创建人ID
                ob.CreatePersonID = PersonID;
                //创建时间
                ob.CreateTime = DateTime.Now;
                //最后修改人ID
                ob.ModifyPersonID = PersonID;
                ob.ModifyTime = DateTime.Now;
                DataAccess.InfoTagDAL.Insert(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加知识大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "新增知识大纲", string.Format("大纲名称：{0}", ob.Title));
        }

        //删除大纲
        protected void RadGridInfoTag_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{
            //    InfoTagDAL.Delete(Convert.ToInt64(RadGridInfoTag.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TagID"]));
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "删除知识大纲失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(PersonName, UserID, "删除知识大纲", string.Format("知识大纲名称：{0}。",
            //    e.Item.Cells[RadGridInfoTag.MasterTableView.Columns.FindByUniqueName("Title").OrderIndex].Text));
            //UIHelp.layerAlert(Page, "删除知识大纲成功！");
        }

        //修改大纲
        protected void RadGridInfoTag_UpdateCommand(object source, GridCommandEventArgs e)
        {
            HiddenField HiddenFieldTagID = e.Item.FindControl("HiddenFieldTagID") as HiddenField;
            InfoTagOB ob = InfoTagDAL.GetObject(Convert.ToInt64(HiddenFieldTagID.Value));//大纲对象实例

            //显示编码
            RadTextBox RadTextBoxShowCode = e.Item.FindControl("RadTextBoxShowCode") as RadTextBox;
            ob.ShowCode = RadTextBoxShowCode.Text.Trim();

            //大纲编码
            ob.TagCode = FormatTagCode(ob.ShowCode);

            //大纲标题
            RadTextBox RadTextBoxPageTitle = e.Item.FindControl("RadTextBoxTitle") as RadTextBox;
            ob.Title = RadTextBoxPageTitle.Text.Trim();

            //状态RadTextBoxFlag
            ob.Flag = 1;

            try
            {
                InfoTagDAL.Update(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修改知识大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新知识大纲", string.Format("知识大纲名称：{0}。", ob.Title));
            UIHelp.layerAlert(Page, "修改知识大纲成功！",6,3000);
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //if (RadGridInfoTag.MasterTableView.Items.Count == 0)
            //{
            //    UIHelp.layerAlert(Page, "没有可导出的数据！");
            //    return;
            //}

            //RadGridInfoTag.Columns.FindByUniqueName("Edit").Visible = false;
            //RadGridInfoTag.Columns.FindByUniqueName("Delete").Visible = false;
            //RadGridInfoTag.ExportSettings.ExportOnlyData = true;
            //RadGridInfoTag.ExportSettings.OpenInNewWindow = true;
            //RadGridInfoTag.MasterTableView.ExportToExcel();
            //RadGridInfoTag.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");

        }

        //格式化Excel
        protected void RadGridInfoTag_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "ShowCode")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Attributes.Add("align", "left");
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "left");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");

        }

        protected void RadGridPost_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            string SubjectID = dataItem.GetDataKeyValue("SubjectID").ToString();
            string year =  RadComboBoxYear.SelectedValue==""?"2000":RadComboBoxYear.SelectedValue;
            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ExamRangeSub", "*", string.Format(" and SubjectID={0}  and ExamYear = {1}", SubjectID, year), "TagCode");
            e.DetailTableView.DataSource = dt;
        }

        //新建考试大纲
        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainExamOutlineNew.aspx", false);
        }

        //变换年度过滤条件
        protected void RadComboBoxYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindRadGridPost();//绑定考试大纲Grid
        }

        //变换岗位工种过滤条件
        protected void PostSelect_SelectChange(object source, EventArgs e)
        {
            BindRadGridPost();//绑定考试大纲Grid
        }

        //变换岗位类型过滤条件
        protected void PostTypeSelect_SelectChange(object source, EventArgs e)
        {
            BindRadGridPost();//绑定考试大纲Grid
        }
    }
}