using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.Train
{
    public partial class TrainReadManage : BasePage
    {
        protected bool isExcelExport = false;//导出excel标识

        /// <summary>
        /// 当前培训点信息
        /// </summary>
        protected TrainUnitMDL curTrainUnit
        {
            get { return ViewState["TrainUnitMDL"] == null ? null : (ViewState["TrainUnitMDL"] as TrainUnitMDL); }
        }

        /// <summary>
        /// 当前培训点信息
        /// </summary>
        protected long curExamPlanID
        {
            get { return ViewState["curExamPlanID"] == null ? 0 : Convert.ToInt64(ViewState["curExamPlanID"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TrainUnitMDL _TrainUnitMDL = TrainUnitDAL.GetObjectBySHTYXYDM(SHTJXYDM);
                if (_TrainUnitMDL != null)
                {
                    ViewState["TrainUnitMDL"] = _TrainUnitMDL;
                }

                //根据培训点绑定可创建考试计划的工种
                Dictionary<string, string> postFilterString = new Dictionary<string, string>();
                postFilterString.Add("4000", string.Format("PostID in({0})", _TrainUnitMDL.PostSet));//
                PostSelect2.PostFilterString = postFilterString;

                //初始化岗位类别
                for (int i = 1; i < 6; i++)
                {
                    PostSelect2.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                }
                PostSelect2.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                PostSelect2.PostTypeID = "4000";
                PostSelect2.HideRadComboBoxPostType();

                for (int i = 2024; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                RadComboBoxYear.Items.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;


                RefreshGrid(0);
            }
        }

        ////查询
        //protected void ButtonSearch_Click(object sender, EventArgs e)
        //{
        //    if (UIHelp.CheckSQLParam() == false)
        //    {
        //        UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
        //        return;
        //    }
        //    //if (ExamPlanSelect1.ExamPlanID.HasValue == false)
        //    //{
        //    //    UIHelp.layerAlert(this.Page, "必须选择一个考试计划名称才能查询！");
        //    //    return;
        //    //}
        //    FilterRadComboBoxPostType(ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.Value: 0);
        //    RefreshGrid(0);
        //    if (RadGridExamSubjectResult.Items.Count > 0)
        //    {
        //        ButtonImportScore.Enabled = true;
        //        RadUploadSignUpTable.Enabled = true;
        //    }
        //}

        /// <summary>
        /// 过滤科目
        /// </summary>
        /// <param name="ExplanID">考试计划ID</param>
        protected void FilterRadComboBoxPostType(Int64 ExplanID)
        {
            //获取考试科目DataSet
            DataTable dt = ExamPlanSubjectDAL.GetList(ExplanID);

            List<string> list = new List<string>();//考试科目名称集合
            for(int i=0;i<dt.Rows.Count;i++)
            {
                list.Add(dt.Rows[i]["PostName"].ToString());
            }

            //RadComboBoxPostTypeID.Items.FindItemByValue().Visible=true;
            foreach (RadComboBoxItem item in RadComboBoxPostTypeID.Items)
            {
                if (list.Contains(item.Text) == true)
                    item.Visible = true;
                else
                    item.Visible = false;
            }

        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex)
        {

            //考生信息
            QueryParamOB queryExamResult = new QueryParamOB();
           queryExamResult.Add(string.Format("ExamPlanID={0}", curExamPlanID));// 考试计划
           

            //string sortString = RadioButtonListSortBy.SelectedValue;// (RadioButtonSortByUnit.Checked == true) ? "UnitName" : "ExamCardID";//排序

            int rowCount = ExamResultDAL.SelectCountView(queryExamResult.ToWhereString());
            DataTable dtMain = ExamResultDAL.GetListView(pageIndex * RadGridExamSubjectResult.PageSize, RadGridExamSubjectResult.PageSize, queryExamResult.ToWhereString(), "");
            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dtMain.Columns["ExamCardID"];
            dtMain.PrimaryKey = dcKeys;

            //if (_ExamPlanID == "0" && rowCount > 0)//只输入准考查询,定位考试计划ID，查询科目
            //{
            //    _ExamPlanID = dtMain.Rows[0]["ExamPlanID"].ToString();
            //}

            //添加考试科目列到考生信息table
            DataTable dtExamSubject = CommonDAL.GetDataTable(0, int.MaxValue - 1
                , string.Format("dbo.PostInfo inner join dbo.ExamPlanSubject on dbo.ExamPlanSubject.ExamPlanID={0} and dbo.PostInfo.PostID = dbo.ExamPlanSubject.PostID", curExamPlanID)
                ,"dbo.PostInfo.PostID as SubjectID,dbo.PostInfo.PostName,dbo.PostInfo.ExamFee as ExamFeeNormal,dbo.ExamPlanSubject.*", "", "PostName");

            RadGridExamSubjectResult.Columns.Clear();
            AddBoundColumn("序号", "RowNum");
            AddBoundColumn("准考证号", "ExamCardID");
            AddBoundColumn("考生姓名", "WorkerName");
            AddBoundColumn("证件号码", "CertificateCode");
            //AddBoundColumn("单位名称", "UnitName");
            //AddBoundColumn("报名点", "TrainUnitName");


            foreach (DataRow dr in dtExamSubject.Rows)
            {
                dtMain.Columns.Add("KeGuanScore" + dr["PostID"].ToString(), typeof(Decimal));//主观              
                dtMain.Columns.Add("ZhuGuanScore" + dr["PostID"].ToString(), typeof(Decimal));//客观               
                dtMain.Columns.Add("SumScore" + dr["PostID"].ToString(), typeof(Decimal));//总分               
                dtMain.Columns.Add("ExamStatus" + dr["PostID"].ToString(), typeof(string));//考试情况

                #region 定义模版列

                if (RadGridExamSubjectResult.Columns.Contains("Score" + dr["PostID"].ToString()) == false)
                {
                    GridTemplateColumn templateColumn = new GridTemplateColumn();
                    RadGridExamSubjectResult.MasterTableView.Columns.Add(templateColumn);
                    templateColumn.UniqueName = "Score" + dr["PostID"].ToString();                    

                    //HeaderTemplate                    
                    string headFormat = @"<table cellspacing=""0"" cellpadding=""0"" width=""100%"" height=""100%"" border=""0"" style=""margin:0 0;"">
                        <tr>
                            <td style=""text-align:center;"" colspan=""4"" class=""rgHeader"">
                                {0}
                            </td>
                        </tr>
                        <tr>
                            <td style=""width: 25%; text-align:center;"" class=""rgHeader"">
                                <nobr>&nbsp;&nbsp;客观题&nbsp;&nbsp;</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;"" class=""rgHeader"">
                                <nobr>&nbsp;&nbsp;主观题&nbsp;&nbsp;</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;"" class=""rgHeader"">
                                <nobr>&nbsp;&nbsp;总　分&nbsp;&nbsp;</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;"" class=""rgHeader"">
                                <nobr>考试情况</nobr>
                            </td>
                        </tr>
                      </table>";
                    List<string> HeadParams = new List<string>() { dr["PostName"].ToString() };
                    Utility.TemplateCustomGridColumn head = new Utility.TemplateCustomGridColumn(headFormat, HeadParams);
                    templateColumn.HeaderTemplate = head;
                    templateColumn.HeaderStyle.CssClass = "gridcell_collapse";

                    //ItemTemplate                  
                    string ItemFormat = @"<table cellspacing=""0"" cellpadding=""0"" width=""100%"" border=""0"" style=""margin:0 0; padding:0 0; "">
                        <tr >
                            <td style=""width: 25%; text-align:center;background-color:Transparent !important;""  class=""rgHeader"" >
                                {0}
                            </td>
                            <td style=""width: 25%; text-align:center;background-color:Transparent !important;"" class=""rgHeader"" >
                                {1}
                            </td>
                            <td style=""width: 25%; text-align:center;background-color:Transparent !important;"" class=""rgHeader"" >
                                {2}
                            </td>
                            <td style=""width: 25%; text-align:center;background-color:Transparent !important;"" class=""rgHeader"" >
                                {3}
                            </td>
                        </tr>
                    </table>";
                     List<string> ItemParams = new List<string>() {
                         "#KeGuanScore" + dr["PostID"].ToString()
                        , "#ZhuGuanScore" + dr["PostID"].ToString()
                        , "#SumScore" + dr["PostID"].ToString()
                        , "#ExamStatus" + dr["PostID"].ToString() };
                     Utility.TemplateCustomGridColumn Item = new Utility.TemplateCustomGridColumn(ItemFormat, ItemParams);
                    templateColumn.ItemTemplate = Item;
                    templateColumn.ItemStyle.CssClass = "gridcellItem_collapse";
                }
                #endregion
            }

            if (dtMain.Rows.Count > 0)
            {
                //读取考试成绩
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    sb.Append(string.Format(",'{0}'", dtMain.Rows[i]["ExamCardID"].ToString()));
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                DataTable dtScore = ExamSubjectResultDAL.GetList(0, int.MaxValue - 1
                    , string.Format(" and ExamPlanID ={0} and ExamCardID in({1})", curExamPlanID, sb)
                    , "ExamCardID");

                DataRow drFind;
                string _ExamCardID = "";//准考证
                string _PostID = "";//科目ID
                foreach (DataRow dr in dtScore.Rows)
                {
                    _ExamCardID = dr["ExamCardID"].ToString();
                    _PostID = dr["PostID"].ToString();

                    drFind = dtMain.Rows.Find(_ExamCardID);
                    if (drFind != null)
                    {
                        drFind["KeGuanScore" + _PostID] = dr["SubjectiveTopicScore"];
                        drFind["ZhuGuanScore" + _PostID] = dr["ObjectiveTopicScore"];
                        drFind["SumScore" + _PostID] = dr["SumScore"];
                        drFind["ExamStatus" + _PostID] = dr["ExamStatus"];
                    }
                }
            }

            //绑定数据
            RadGridExamSubjectResult.VirtualItemCount = rowCount;
            RadGridExamSubjectResult.CurrentPageIndex = pageIndex;
            RadGridExamSubjectResult.DataSource = dtMain;
            RadGridExamSubjectResult.DataBind();
        }

        //为grid添加绑定列
        protected void AddBoundColumn(string headerText, string dataField)
        {
            if (RadGridExamSubjectResult.Columns.Contains(dataField) == false)
            {
                GridBoundColumn boundColumn = new GridBoundColumn();
                RadGridExamSubjectResult.MasterTableView.Columns.Add(boundColumn);
                boundColumn.UniqueName = dataField;
                boundColumn.DataField = dataField;
                boundColumn.HeaderText = headerText;
            }
        }

        //导入成绩
        protected void ButtonImportScore_Click(object sender, EventArgs e)
        {
            if (curExamPlanID==0)
            {
                UIHelp.layerAlert(this.Page, "必须选择一个考试计划名称！");
                return;
            }
            if (RadComboBoxPostTypeID.SelectedItem.Text == "" || RadComboBoxPostTypeID.SelectedItem.Text == "请选择")
            {
                RefreshGrid(0);
                UIHelp.layerAlert(Page, "请选择科目!");
                return;
            }
            ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];

            int KeMuId = PostInfoDAL.GetObject(_ExamPlanOB.PostID.Value, RadComboBoxPostTypeID.SelectedItem.Text).PostID.Value;//考试科目id

             //设定合格线后不能再次导入成绩
            int count = ExamPlanSubjectDAL.SelectCount(string.Format(" and ExamPlanID={0} and PostID={1} and PassLine is not null", curExamPlanID, KeMuId));
            if (count > 0)
            {
                UIHelp.layerAlert(Page, string.Format("{0}已经设置了合格线，无法导入成绩!",RadComboBoxPostTypeID.SelectedItem.Text));
                RefreshGrid(0);
                return;
            }

            if (RadUploadSignUpTable.UploadedFiles.Count > 0)
            {
                //上传excel
                string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
                string filePath = Path.Combine(targetFolder, string.Format("signUpBat_{0}_{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadSignUpTable.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "导入成绩表数据失败", ex);
                    UIHelp.layerAlert(Page, "导入成绩表数据失败!");
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    RefreshGrid(0);
                    UIHelp.layerAlert(Page, "未找到任何数据!");
                    return;
                }
                else
                {
                    if (VaildImportTemplate(dsImport.Tables[0]) == false)
                    {
                        UIHelp.layerAlert(Page, "导入文件格式无效，请按考试计划名称查询后，再下载带有考生信息的模板，录入成绩后按科目分批导入!");
                        return;
                    }
                    SaveImportData(dsImport.Tables[0]);//保存
                }
            }
            else
            {
                RefreshGrid(0);
            }
        }

        //验证成绩导入母板
        protected bool VaildImportTemplate(DataTable dt)
        {
            if (dt.Columns.Count < 6) return false;//模版列：准考证,姓名,考场号,客观题成绩,主观题成绩,考试情况

            string[] colNames = new string[6] { "准考证", "姓名", "考场号", "客观题成绩", "主观题成绩", "考试情况" };
            for (int i = 0; i < 6; i++)
            {
                if (dt.Columns[i].ColumnName != colNames[i]) return false;
            }

            return true;
        }

        //保存成绩信息
        protected void SaveImportData(DataTable dt)
        {           
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

           // 删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["准考证"].ToString().Trim() == ""
                && dt.Rows[m]["客观题成绩"].ToString().Trim() == ""
                && dt.Rows[m]["主观题成绩"].ToString().Trim() == ""
                && dt.Rows[m]["考试情况"].ToString().Trim() == ""
               
                )
                {
                    dt.Rows.RemoveAt(m);
                }
            }

            //考生信息
            QueryParamOB queryExamResult = new QueryParamOB();
            queryExamResult.Add(string.Format("ExamPlanID={0}", curExamPlanID));// 考试计划
           
            DataTable dtMain = ExamResultDAL.GetListView(0, int.MaxValue -1, queryExamResult.ToWhereString(), "ExamCardID");
            List<string> List_ExamCardID = new List<string>();//准考证号码集合
            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                List_ExamCardID.Add(dtMain.Rows[i]["ExamCardID"].ToString());
            }
            List<string> listCase = new List<string>() { "正常", "违纪", "替考", "缺考" };
            //判断导入成绩准考证号是否在报名表中
            for (int m = 0;m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空
                if (List_ExamCardID.Contains(dt.Rows[m]["准考证"].ToString().Trim()) == false)
                {
                    rowErr.AppendFormat("<br />成绩表中准考证号{0}不在考试报名表中，成绩不能导入。", dt.Rows[m]["准考证"].ToString().Trim());
                }
                if (dt.Rows[m]["考试情况"].ToString().Trim() == "正常")
                {
                    if (dt.Rows[m]["客观题成绩"].ToString().Trim() == "")
                    {
                        rowErr.Append("<br />客观题成绩不能为空。");
                    }
                    else if (Utility.Check.IsDouble(dt.Rows[m]["客观题成绩"].ToString().Trim()) == false)
                    {
                        rowErr.Append("<br />客观题成绩必须为数值。");
                    }
                    if (dt.Rows[m]["主观题成绩"].ToString().Trim() == "")
                    {
                        rowErr.Append("<br />主观题成绩不能为空。");
                    }
                    else if (Utility.Check.IsDouble(dt.Rows[m]["主观题成绩"].ToString().Trim()) == false)
                    {
                        rowErr.Append("<br />主观题成绩必须为数值。");
                    }
                }
                if (listCase.Contains(dt.Rows[m]["考试情况"].ToString().Trim()) == false)
                {
                    rowErr.Append("<br />考试情况只能输入（正常、违纪、替考、缺考）中的一项。");
                }

                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 2)).Append("】行：-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                RefreshGrid(0);
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
         
            DateTime createTime = DateTime.Now;
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            string kemuName = RadComboBoxPostTypeID.SelectedItem.Text;//科目名称
            //int gongZhongID = _ExamPlanOB.PostID.Value;//工种ID
            //long explanID = curExamPlanID;//考试计划id
            int KeMuId = PostInfoDAL.GetObject(_ExamPlanOB.PostID.Value, kemuName).PostID.Value;//考试科目id

            try
            {
                //清空原科目成绩
                ExamSubjectResultDAL.DeleteByKeMuID(tran, curExamPlanID, KeMuId);

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    //向考试单科成绩表插入数据                    
                    ExamSubjectResultOB examSubjectResultOB = new ExamSubjectResultOB();
                    examSubjectResultOB .ExamCardID=dt.Rows[m]["准考证"].ToString().Trim();   //准考证号码
                    examSubjectResultOB.ExamPlanID = curExamPlanID;   //考试计划id
                    examSubjectResultOB.PostID = KeMuId;        //考试科目id
                    examSubjectResultOB.ExamStatus = dt.Rows[m]["考试情况"].ToString().Trim(); //考试情况
                    if (examSubjectResultOB.ExamStatus == "正常")
                    {
                        examSubjectResultOB.ObjectiveTopicScore = Convert.ToDecimal(dt.Rows[m]["主观题成绩"].ToString().Trim());   //主观题分数
                        examSubjectResultOB.SubjectiveTopicScore = Convert.ToDecimal(dt.Rows[m]["客观题成绩"].ToString().Trim());   //客观题分数
                        examSubjectResultOB.SumScore = examSubjectResultOB.SubjectiveTopicScore + examSubjectResultOB.ObjectiveTopicScore;   //科目总分
                    }
                    else//非正常状态成绩按0分计算
                    {
                        examSubjectResultOB.ObjectiveTopicScore = 0M;   //主观题分数
                        examSubjectResultOB.SubjectiveTopicScore = 0M;   //客观题分数
                        examSubjectResultOB.SumScore = 0M;   //科目总分
                    }
                    examSubjectResultOB .Status="通过";   //状态
                    examSubjectResultOB .CreatePersonID =Convert.ToInt64(curTrainUnit.UnitNo);  //创建人
                    examSubjectResultOB .CreateTime=DateTime .Now ;  //创建时间                  
                    ExamSubjectResultDAL.Insert(tran, examSubjectResultOB);                    
                }

                tran.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "导入考试成绩", string.Format("考试计划：{0}，科目：{1}。",
                (ViewState["ExamPlanOB"] as ExamPlanOB).PostName, kemuName));   

                UIHelp.layerAlert(Page, "成绩导入成功！",6,3000);
                RefreshGrid(0);
            }
            catch (Exception ex)
            {
                tran.Rollback();               
                UIHelp.WriteErrorLog(Page, "成绩导入失败！", ex);
                return;
            }
        }

        //导出成绩excel
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (RadGridExamSubjectResult.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridExamSubjectResult.PageSize = RadGridExamSubjectResult.MasterTableView.VirtualItemCount;//
            //RadGridExamSubjectResult.Rebind();
            isExcelExport = true;
            RadGridExamSubjectResult.ExportSettings.ExportOnlyData = true;
            RadGridExamSubjectResult.ExportSettings.IgnorePaging = true;
            RadGridExamSubjectResult.ExportSettings.OpenInNewWindow = true;
            RefreshGrid(0);           
            RadGridExamSubjectResult.MasterTableView.ExportToExcel();
        }

        //绑定科目
        protected void RadComboBoxPostTypeID_Init(object sender, EventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            List<PostInfoOB> dt = PostInfoDAL.GetObject();
            rcb.DataSource = dt;
            rcb.DataBind();
            rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));

            
        }

        //成绩导出格式化
        protected void RadGridExamSubjectResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "ExamCardID": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "ExamCardID")
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
                        LiteralControl lc = ghi.Cells[i].Controls[0] as LiteralControl;
                        lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; font-weight:bold;' ");
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
                LiteralControl lc = e.Cell.Controls[0] as LiteralControl;
                lc.Text = lc.Text.Replace("<td ", "<td style='border:solid 0.1pt #CCCCCC; ' ");
            }
        }

        //成绩分页
        protected void RadGridExamSubjectResult_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {            
            RefreshGrid(e.NewPageIndex);
        }

        //查询考试计划
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            //培训点名称
            q.Add(string.Format("SIGNUPPLACE like '{0}'", curTrainUnit.TrainUnitName));

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;
        }

        //弹出选择考试计划
        protected void ButtonSelectExamPlan_Click(object sender, EventArgs e)
        {
            TrExamPlan.Style.Add("display", "table-row");
            trSelectExamPlan.Style.Add("display", "none");
            ButtonSearch_Click(sender, e);
        }

        //选择一个考试计划
        protected void RadGridExamPlan_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 ExamPlanID = Convert.ToInt64(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]);
            ViewState["curExamPlanID"] = ExamPlanID;

            ExamPlanOB o = ExamPlanDAL.GetObject(ExamPlanID);
            ViewState["ExamPlanOB"] = o;

            RadTextBoxExamPlan.Text = RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanName"].ToString();

            TrExamPlan.Style.Add("display", "none");
            trSelectExamPlan.Style.Add("display", "table-row");
            FilterRadComboBoxPostType(curExamPlanID);
            RefreshGrid(0);

        }

        //导入成绩模板下载
        protected void ButtonDownLoadScoreTemplat_Click(object sender, EventArgs e)
        {
            if (curExamPlanID == 0)
            {
                UIHelp.layerAlert(this.Page, "必须选择一个考试计划！");
                return;
            }

            bool IfSuccess = false;
            object nul = Type.Missing;
            Excel.Application ThisApplication = null;
            Excel.Workbook ThisWorkbook = null;

            //读取考生信息        
            QueryParamOB queryExamResult = new QueryParamOB();
            queryExamResult.Add(string.Format("ExamPlanID={0}", curExamPlanID));// 考试计划
            int rowCount = ExamResultDAL.SelectCountView(queryExamResult.ToWhereString());
            DataTable dtMain = ExamResultDAL.GetListView(0, int.MaxValue - 1, queryExamResult.ToWhereString(), "ExamCardID");
            if (dtMain == null || dtMain.Rows.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有相关考生信息，请修改查询条件！");
                return;
            }

            //拷贝母板
            string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
            if (System.IO.Directory.Exists(targetFolder) == false)
            {
                System.IO.Directory.CreateDirectory(targetFolder);
            }
            string filePath = Path.Combine(targetFolder, string.Format("Score_{0}_{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss")));
            File.Copy(Server.MapPath("~/Template/成绩导入模版.xls"), filePath, true);

            //填充内容
            try
            {
                ThisApplication = new Excel.Application();
                ThisWorkbook = ThisApplication.Workbooks.Open(filePath, nul, nul, nul, nul, nul, nul, nul, nul, nul, nul, nul, nul, nul, nul);
                ThisApplication.DisplayAlerts = false;
                Excel.Worksheet xlSheet = (Excel.Worksheet)ThisWorkbook.Worksheets.get_Item(1);
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    (xlSheet.Cells[i + 2, 1] as Excel.Range).set_Value(nul, dtMain.Rows[i]["ExamCardID"]);
                    (xlSheet.Cells[i + 2, 2] as Excel.Range).set_Value(nul, dtMain.Rows[i]["WorkerName"]);
                    (xlSheet.Rows.Cells[i + 2, 3] as Excel.Range).set_Value(nul, dtMain.Rows[i]["ExamRoomCode"]);
                    (xlSheet.Rows.Cells[i + 2, 6] as Excel.Range).set_Value(nul, "正常");
                }
                IfSuccess = true;
            }
            catch (Exception ex)
            {
                IfSuccess = false;
                UIHelp.WriteErrorLog(Page, "导出成绩模板失败！", ex);
            }
            finally
            {
                ThisWorkbook.Close(true, filePath, nul);//文件保存
                ThisApplication.Workbooks.Close();
                ThisApplication.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ThisWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ThisApplication);
                ThisWorkbook = null;
                ThisApplication = null;
                GC.Collect();
            }
            if (IfSuccess == false)
            {
                UIHelp.layerAlert(Page, "模版数据载入失败！");
                return;
            }

            //输出
            Utility.ExcelDealHelp.ExportExcel(Page, filePath, "成绩_" + (ViewState["ExamPlanOB"] as ExamPlanOB).PostName + ".xls");
        }        
    }
}
