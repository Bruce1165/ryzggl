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
    public partial class InfoTagManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //显示科目列表（隐藏考试科目参考其它科目数据，即 SUBJECTID <> CodeForma）
                DataTable dtPost =null;
                if (Cache["Exam_KeMu"] != null)
                {
                    dtPost = Cache["Exam_KeMu"] as DataTable;
                }
                else
                {
                    dtPost = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.View_PostInfo", "PostTypeID,PostID,PostTypeName,PostName,SubjectName,SubjectID", " and SUBJECTID = CodeFormat", "PostTypeID,PostID,SubjectID");

                    //在最后一次被访问后8小时自动过期
                    Utility.CacheHelp.AddSlidingExpirationCache(Page, "Exam_KeMu", dtPost, 8);
                }

                RadGridPost.DataSource = dtPost;
                RadGridPost.DataBind();
                RadGridPost.MasterTableView.Items[0].Selected = true;
                RadGridInfoTag.DataBind();

                RadGridPost_SelectedIndexChanged(sender, e);
            }
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            for (int i = 0; i < RadGridInfoTag.Items.Count; i++)
            {
                printData = new Dictionary<string, string>();
                list.Add(printData);
                printData.Add("PostName", RadGridInfoTag.Items[i]["PostName"].Text);
                printData.Add("ExamPlaceName", RadGridInfoTag.Items[i]["ExamPlaceName"].Text);
                printData.Add("Subject", RadGridInfoTag.Items[i]["Subject"].Text.Replace("<br />", "</w:t></w:r></w:p><w:p wsp:rsidR=\"00A26BA9\" wsp:rsidRPr=\"00497B24\" wsp:rsidRDefault=\"004557A6\" wsp:rsidP=\"00497B24\"><w:pPr><w:ind w:right-chars=\"-18\" w:right=\"-58\"/><w:jc w:val=\"center\"/><w:rPr><w:rFonts w:ascii=\"宋体\" w:fareast=\"宋体\" w:h-ansi=\"宋体\"/><wx:font wx:val=\"宋体\"/><w:b/><w:sz w:val=\"48\"/><w:sz-cs w:val=\"48\"/></w:rPr></w:pPr><w:r wsp:rsidRPr=\"00497B24\"><w:rPr><w:rFonts w:ascii=\"宋体\" w:fareast=\"宋体\" w:h-ansi=\"宋体\"/><wx:font wx:val=\"宋体\"/><w:b/><w:sz w:val=\"48\"/><w:sz-cs w:val=\"48\"/></w:rPr><w:t>"));
                printData.Add("ExamRoomCode", RadGridInfoTag.Items[i]["ExamRoomCode"].Text);
                printData.Add("PersonNumber", RadGridInfoTag.Items[i]["PersonNumber"].Text);
                printData.Add("ExamCardIDFromTo", RadGridInfoTag.Items[i]["ExamCardIDFromTo"].Text);
            }
            return list;
        }

        protected void RadGridInfoTag_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.InfoTag", "*", string.Format(" and SubjectID={0}  and Flag = 1"
                       , RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString()), "TagCode");
            RadGridInfoTag.DataSource = dt;

            int f1 = 0, f2 = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["Weight"] != null)
                {
                    switch (r["ShowCode"].ToString().Split('.').Length)
                    {
                        case 1:
                            f1 += Convert.ToInt32(r["Weight"]);
                            break;
                        case 2:
                            f2 += Convert.ToInt32(r["Weight"]);
                            break;
                    }
                }
            }
            if (dt.Rows.Count == 0)
            {
                LabelWeightCount.Text = "";
            }
            else if (f1 != f2 || f1 !=100)
            {
                LabelWeightCount.Text = "错误：权重合计不等于100，请修改。";
            }
            else
            {
                LabelWeightCount.Text = "权重合计 = 100";
            }
        }

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

        //变换科目选择后
        protected void RadGridPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadGridInfoTag.Rebind();
            RadGridInfoTag.ExportSettings.FileName =Server.UrlEncode(string.Format("{0}-{1}-{2}-知识大纲" , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text));
            LabelSelectPost.Text = string.Format("当前科目：【{0} - {1} - {2}】"
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text);
        }

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

                //权重
                RadTextBox RadTextBoxWeight = e.Item.FindControl("RadTextBoxWeight") as RadTextBox;
                if (RadTextBoxWeight.Text.Trim() == "")
                    ob.Weight = null;
                else
                    ob.Weight = Convert.ToInt32(RadTextBoxWeight.Text.Trim());

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
            try
            {
                InfoTagDAL.Delete(Convert.ToInt64(RadGridInfoTag.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TagID"]));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除知识大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除知识大纲", string.Format("知识大纲名称：{0}。",
                e.Item.Cells[RadGridInfoTag.MasterTableView.Columns.FindByUniqueName("Title").OrderIndex].Text));
            UIHelp.layerAlert(Page, "删除知识大纲成功！",6,3000);
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

            //权重
            RadTextBox RadTextBoxWeight = e.Item.FindControl("RadTextBoxWeight") as RadTextBox;
            if (RadTextBoxWeight.Text.Trim() == "")
                ob.Weight = null;
            else
                ob.Weight = Convert.ToInt32(RadTextBoxWeight.Text.Trim());

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
            if (RadGridInfoTag.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            RadGridInfoTag.Columns.FindByUniqueName("Edit").Visible = false;
            RadGridInfoTag.Columns.FindByUniqueName("Delete").Visible = false;
            RadGridInfoTag.ExportSettings.ExportOnlyData = true;
            RadGridInfoTag.ExportSettings.OpenInNewWindow = true;
            RadGridInfoTag.MasterTableView.ExportToExcel();
            RadGridInfoTag.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");

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

        //导入大纲
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (RadUploadFile.UploadedFiles.Count > 0)
            {
                //上传excel
                string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
                string filePath = Path.Combine(targetFolder, string.Format("InfoTagUpBat_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadFile.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "导入知识大纲数据失败", ex);
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    UIHelp.layerAlert(Page, "未找到任何可导入的数据!");
                    return;
                }
                else
                {
                    if (VaildImportTemplate(dsImport.Tables[0]) == false)
                    {
                        UIHelp.layerAlert(Page, "导入文件格式无效，请下载标准的导入模板，正确填写大纲格式，进行导入!");
                        return;
                    }
                    SaveImportData(dsImport.Tables[0]);//保存
                }
            }
            else
            {
                RadGridInfoTag.Rebind();
            }
        }

        //验证导入模板有效性
        protected bool VaildImportTemplate(DataTable dt)
        {
            if (dt.Columns.Count < 2) return false;//模版列： "序号", "大纲内容"

            string[] colNames = new string[2] { "序号", "大纲内容" };
            for (int i = 0; i < 2; i++)
            {
                if (dt.Columns[i].ColumnName != colNames[i]) return false;
            }

            return true;
        }

        //保存导入的信息
        protected void SaveImportData(DataTable dt)
        {
            #region 格式校验

            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

            // 删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["序号"].ToString().Trim() == "" && dt.Rows[m]["大纲内容"].ToString().Trim() == "" && dt.Rows[m]["权重"].ToString().Trim() == "")
                {
                    dt.Rows.RemoveAt(m);
                }
            }

            List<string> listCode = new List<string>();//已出现过的序号
            int weight=0;//权重

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空
                if (dt.Rows[m]["序号"].ToString().Trim() == "")
                {
                    rowErr.Append("<br />序号不能为空。");
                }
                else if (FormatTagCode(dt.Rows[m]["序号"].ToString().Trim()) <= 0)
                {
                    rowErr.Append("<br />序号格式不正确，请使用数字和小数点表示序号（例：1.2.1）。");
                }
                else
                {
                    if (listCode.Contains(dt.Rows[m]["序号"].ToString().Trim()) == true)
                    {
                        rowErr.Append("<br />序号{0}在大纲内出现两次，请检查序号书写。");
                    }
                    else
                    {
                        listCode.Add(dt.Rows[m]["序号"].ToString().Trim());
                    }
                }
                if (dt.Rows[m]["大纲内容"].ToString().Trim() == "")
                {
                    rowErr.Append("<br />大纲内容不能为空。");
                }
                else if (Utility.Check.Text_Length(dt.Rows[m]["大纲内容"].ToString().Trim()) > 4000)
                {
                    rowErr.Append("<br />单条大纲内容不得超过4000个字符（中文算2个）。");
                }

                if (dt.Rows[m]["权重"].ToString().Trim() != "")
                {
                    if (int.TryParse(dt.Rows[m]["权重"].ToString().Trim(), out weight) == false)
                    {
                        rowErr.Append("<br />权重格式不争取。");
                    }
                }

                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 1)).Append("】行：-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            #endregion 格式校验

            DateTime createTime = DateTime.Now;
            int SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());//科目

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            InfoTagOB ob = null;
            int TagCode = 0;//大纲编号
            try
            {
                //清除科目下已存在的大纲
                InfoTagDAL.Delete(tran, SubjectID);

                //导入新大纲
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    TagCode = FormatTagCode(dt.Rows[m]["序号"].ToString().Trim());
                    //ob = InfoTagDAL.GetObject(SubjectID, TagCode);
                    //if (ob == null)
                    //{ ob = new InfoTagOB(); }
                    ob = new InfoTagOB();
                    //科目ID
                    ob.SubjectID = SubjectID;
                    //显示编码
                    ob.ShowCode = dt.Rows[m]["序号"].ToString().Trim();
                    //大纲编码
                    ob.TagCode = TagCode;
                    //大纲标题
                    ob.Title = dt.Rows[m]["大纲内容"].ToString().Trim();

                    if (dt.Rows[m]["权重"].ToString().Trim() == "")
                        ob.Weight = null;
                    else
                        ob.Weight = Convert.ToInt32(dt.Rows[m]["权重"].ToString().Trim());

                    //状态
                    ob.Flag = 1;

                    //最后修改人ID
                    ob.ModifyPersonID = PersonID;
                    ob.ModifyTime = DateTime.Now;

                    //创建人ID
                    ob.CreatePersonID = PersonID;
                    //创建时间
                    ob.CreateTime = DateTime.Now;
                    DataAccess.InfoTagDAL.Insert(tran, ob);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批量导入知识大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量导入知识大纲", string.Format("批量导入知识大纲：科目：{0}，大纲数量{1}条。",
               RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, dt.Rows.Count.ToString()));

            UIHelp.layerAlert(Page, "导入知识大纲成功！",6,3000);
            RadGridInfoTag.Rebind();
        }
    }
}