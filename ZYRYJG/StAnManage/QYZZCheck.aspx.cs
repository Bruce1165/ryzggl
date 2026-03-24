using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using System.Data;
using DataAccess;

namespace ZYRYJG.StAnManage
{    

    public partial class QYZZCheck : BasePage
    {
        string[] Gridhead=new string[]{"特级","一级","二级","三级","","不分等级"};

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) RadioButtonListSZD.Items[0].Selected = true;
            bindGrid();
        }

        //为grid添加绑定列
        protected void AddBoundColumn(string headerText, string dataField)
        {
            if (RadGrid1.Columns.Contains(dataField) == false)
            {
                GridBoundColumn boundColumn = new GridBoundColumn();
                RadGrid1.MasterTableView.Columns.Add(boundColumn);
                boundColumn.UniqueName = dataField;
                boundColumn.DataField = dataField;
                boundColumn.HeaderText = headerText;
            }
        }

        private void bindGrid()
        {
            string selectTabID = RadTabStrip1.SelectedTab.PageViewID.Replace("RadPageView", "");//选择序列

            try
            {
                QueryParamOB q = new Model.QueryParamOB();
                q.Add(string.Format("zzxl={0}", selectTabID));

                //显示模板(a企业数量、g管理符合数量、j工人符合数量、p管理和工人同时符合数量,0~5表示等级)
                //DataTable dtMain = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.VIEW_QY_MAN_CHECK_RESULT"
                //    //, "distinct zzxl,zzlb,0 a0,0 g0,0 j0,0 p0,0 a1,0 g1,0 j1,0 p1,0 a2,0 g2,0 j2,0 p2,0 a3,0 g3,0 j3,0 p3,0 a5,0 g5,0 j5,0 p5"
                //     , "distinct zzxl,zzlb,'0' a0,'0' g0,'0' j0,'0' p0,'0' a1,'0' g1,'0' j1,'0' p1,'0' a2,'0' g2,'0' j2,'0' p2,'0' a3,'0' g3,'0' j3,'0' p3,'0' a5,'0' g5,'0' j5,'0' p5"
                //    , q.ToWhereString(), "zzxl,zzlb");

                string sqlMB = @"select distinct zzxl,zzlb,'0' a0,'0' g0,'0' j0,'0' p0,'0' a1,'0' g1,'0' j1,'0' p1,'0' a2,'0' g2,'0' j2,'0' p2,'0' a3,'0' g3,'0' j3,'0' p3,'0' a5,'0' g5,'0' j5,'0' p5 
                from VIEW_QY_MAN_CHECK_RESULT where zzxl={0}";
                DataTable dtMain = CommonDAL.GetDataTable(string.Format(sqlMB, selectTabID));

                DataRow drSum = dtMain.NewRow();//合计
                dtMain.Rows.Add(drSum);
                drSum["zzxl"] = 9;
                drSum["zzlb"] = "合计";

                DataRow drPercent = dtMain.NewRow();//合格率
                dtMain.Rows.Add(drPercent);
                drPercent["zzxl"] = 9;
                drPercent["zzlb"] = "合格率";

                DataColumn[] dcKeys = new DataColumn[2];
                dcKeys[0] = dtMain.Columns["zzxl"];
                dcKeys[1] = dtMain.Columns["zzlb"];
                dtMain.PrimaryKey = dcKeys;

                RadGrid1.Columns.Clear();
                AddBoundColumn("专业", "zzlb");
                RadGrid1.Columns[0].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                RadGrid1.Columns[0].HeaderStyle.Font.Bold = true;
                RadGrid1.Columns[0].ItemStyle.Font.Bold = true;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 4) continue;//0：特级,1：一级,2：二级,3：三级,5：不分等级

                    #region 定义模版列

                    GridTemplateColumn templateColumn = new GridTemplateColumn();
                    RadGrid1.MasterTableView.Columns.Add(templateColumn);
                    templateColumn.UniqueName = string.Format("zzdj{0}", i.ToString());

                    switch (selectTabID)
                    {
                        case "1"://总包                          
                            if (i == 5)//不显不分等级
                            {
                                templateColumn.Display = false;
                            }
                            break;
                        case "2"://专包                          
                            if (i == 0)//不显示特级
                            {
                                templateColumn.Display = false;
                            }
                            break;
                        case "3"://一体化                          
                            if (i == 0 || i == 5)//不显示特级、不分等级
                            {
                                templateColumn.Display = false;
                            }
                            break;
                        case "4"://分包                          
                            if (i == 0|| i==3)//不显示特级、三级
                            {
                                templateColumn.Display = false;
                            }
                            break;
                    }

                    //HeaderTemplate                    
                    string headFormat = @"<table cellspacing=""0"" cellpadding=""0"" width=""100%"" height=""100%"" border=""0"" style=""margin:0 0; "">
                        <tr>
                            <td style=""text-align:center;font-weight:bold;"" colspan=""4"" class=""rgHeader"">
                                {0}
                            </td>
                        </tr>
                        <tr>
                            <td style=""width: 25%; text-align:center;font-weight:bold;"" class=""rgHeader"">
                                <nobr>企业</br>总数</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;font-weight:bold;"" class=""rgHeader"">
                                <nobr>管理</br>符合</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;font-weight:bold;"" class=""rgHeader"">
                                <nobr>工人</br>符合</nobr>
                            </td>
                            <td style=""width: 25%; text-align:center;font-weight:bold;"" class=""rgHeader"">
                                <nobr>同时</br>符合</nobr>
                            </td>
                        </tr>
                      </table>";
                    List<string> HeadParams = new List<string>() { Gridhead[i] };
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
                         string.Format("#a{0}" ,i.ToString())
                        , string.Format("#g{0}" ,i.ToString())
                        , string.Format("#j{0}" ,i.ToString())
                        , string.Format("#p{0}" ,i.ToString())
                    };
                    Utility.TemplateCustomGridColumn Item = new Utility.TemplateCustomGridColumn(ItemFormat, ItemParams);
                    templateColumn.ItemTemplate = Item;
                    templateColumn.ItemStyle.CssClass = "gridcellItem_collapse";

                    #endregion
                }

                #region 填充grid

                //读取t统计结果
                string sql = @"select distinct  t1.zzxl,t1.zzlb,t1.zzdj,isnull(t4.all_count,0) all_count,isnull(t2.gl_pass,0) gl_pass,isnull(t3.js_pass,0) js_pass,isnull(t5.all_pass,0) all_pass  
                        from  DBO.QY_ZZYQ  t1
                        left join 
                        (
                            select z.zzxl,z.zzlb,z.zzdj,'管理人员' as yylx, count(1) as gl_pass from 
                            (select distinct zzxl,zzlb,zzdj from DBO.QY_ZZYQ ) z
                            left join dbo.QY_MAN_CHECK c 
                            on z.zzxl=c.zzxl_no and z.zzdj=c.zzdj_no and c.ZZLB like '%' + z.zzlb + '%' 
                            where cast(c.mantype as varchar(64))='管理人员' and c.checkresult=1  and c.zzxl_no={0} and c.szd like '%{1}%'
                            group by z.zzxl,z.zzlb,z.zzdj
                        ) t2 on t1.zzxl =t2.zzxl and t1.zzlb =t2.zzlb and t1.zzdj =t2.zzdj 
                        left join 
                        (
                            select z.zzxl,z.zzlb,z.zzdj,'技术工人' as yylx, count(1) as js_pass from 
                            (select distinct zzxl,zzlb,zzdj from DBO.QY_ZZYQ ) z
                            left join dbo.QY_MAN_CHECK c 
                            on z.zzxl=c.zzxl_no and z.zzdj=c.zzdj_no and c.ZZLB like '%' + z.zzlb + '%' 
                            where cast(c.mantype as varchar(64))='技术工人' and c.checkresult=1 and c.zzxl_no={0} and c.szd like '%{1}%'
                            group by z.zzxl,z.zzlb,z.zzdj
                        ) t3 on t1.zzxl =t3.zzxl and t1.zzlb =t3.zzlb and t1.zzdj =t3.zzdj 
                        left join 
                        (
                            select z.zzxl,z.zzlb,z.zzdj, count(1) as all_count from 
                            (select distinct zzxl,zzlb,zzdj from DBO.QY_ZZYQ ) z
                            left join dbo.QY_MAN_CHECK c 
                            on z.zzxl=c.zzxl_no and z.zzdj=c.zzdj_no and c.ZZLB like '%' + z.zzlb + '%' 
                            where  c.zzxl_no={0} and c.szd like '%{1}%'
                            group by z.zzxl,z.zzlb,z.zzdj
                        ) t4 on t1.zzxl =t4.zzxl and t1.zzlb =t4.zzlb and t1.zzdj =t4.zzdj 
                        left join 
                        (
                            select z.zzxl,z.zzlb,z.zzdj, count(1) as all_pass from 
                            (select distinct zzxl,zzlb,zzdj from DBO.QY_ZZYQ ) z
                            left join
                            (
                                SELECT g.qymc,g.zzjgdm,g.szd,g.zzxl_no,g.zzlb,g.zzdj_no,g.MANTYPE,g.YYSL,g.MANCOUNT,g.CHECKRESULT
                                ,j.MANTYPE j_MANTYPE,j.YYSL j_YYSL,j.MANCOUNT j_MANCOUNT,j.CHECKRESULT j_CHECKRESULT
                                FROM DBO.QY_MAN_CHECK g
                                inner join DBO.QY_MAN_CHECK j on g.zzjgdm = j.zzjgdm
                                where cast(g.mantype as varchar(64))='管理人员'  and g.checkresult =1
                                and cast(j.mantype as varchar(64))='技术工人' and j.checkresult =1
                            ) c  on z.zzxl=c.zzxl_no and z.zzdj=c.zzdj_no and c.ZZLB like '%' + z.zzlb + '%' 
                            where  c.zzxl_no={0} and c.szd like '%{1}%'
                            group by z.zzxl,z.zzlb,z.zzdj
                        ) t5 on t1.zzxl =t5.zzxl and t1.zzlb =t5.zzlb and t1.zzdj =t5.zzdj 
                        order by   t1.zzxl,t1.zzlb,t1.zzdj;";

                if (dtMain.Rows.Count > 0)
                {
                    DataTable dtScore = new DBHelper().GetFillData(string.Format(sql, selectTabID, RadioButtonListSZD.SelectedValue));

                    //DataTable dtScore = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.VIEW_QY_MAN_CHECK_RESULT", "*", q.ToWhereString(), "zzxl,zzlb");

                    DataRow drFind;
                    foreach (DataRow dr in dtScore.Rows)
                    {

                        drFind = dtMain.Rows.Find(new object[] { dr["zzxl"], dr["zzlb"] });
                        if (drFind != null)
                        {
                            drFind[string.Format("a{0}", dr["zzdj"].ToString())] = dr["all_count"];//企业数量
                            drFind[string.Format("g{0}", dr["zzdj"].ToString())] = dr["gl_pass"];//管理人员符合企业数量
                            drFind[string.Format("j{0}", dr["zzdj"].ToString())] = dr["js_pass"];//技术工人符合企业数量
                            drFind[string.Format("p{0}", dr["zzdj"].ToString())] = dr["all_pass"];//管理和工人同时符合企业数量
                        }
                    }

                    //统计
                    int sum = 0;
                    int span=0;
                    for (int i = 2; i < dtMain.Columns.Count; i++)
                    {
                        sum=0;
                        for (int j = 0; j < dtMain.Rows.Count - 2; j++)
                        {
                            sum += Convert.ToInt32(dtMain.Rows[j][i]);
                        }
                        //合计
                        dtMain.Rows[dtMain.Rows.Count - 2][i] = sum;

                        //合格率
                        if ("G,J,P".Contains(dtMain.Columns[i].ColumnName.Substring(0,1))==true)
                        {
                            switch(dtMain.Columns[i].ColumnName.Substring(0,1))
                            {
                                case "G":
                                    span =1;
                                    break;
                                     case "J":
                                    span =2;
                                    break;
                                     case "P":
                                    span =3;
                                    break;
                            }
                            if(Convert.ToInt32(dtMain.Rows[dtMain.Rows.Count - 2][i -span])==0)
                                dtMain.Rows[dtMain.Rows.Count - 1][i] = "0%";
                            else
                                dtMain.Rows[dtMain.Rows.Count - 1][i] = string.Format("{0:N1}%",sum * 100.0D / Convert.ToInt32(dtMain.Rows[dtMain.Rows.Count - 2][i - span]));
                        }
                    }
                }

                #endregion 填充grid

                //绑定数据
                RadGrid1.VirtualItemCount = dtMain.Rows.Count;
                RadGrid1.CurrentPageIndex = 0;
                RadGrid1.DataSource = dtMain;
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业资质人员检查统计失败！", ex);
                return;
            }
        }

         protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            bindGrid();
        }

         //导出审批列表
         protected void ButtonOutput_Click(object sender, EventArgs e)
         {

             if (RadGrid1.MasterTableView.Items.Count == 0)
             {
                 UIHelp.layerAlert(Page, "没有可导出的数据！");
                 return;
             }
             RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
             //RadGridExamSubjectResult.Rebind();
             RadGrid1.ExportSettings.ExportOnlyData = true;
             RadGrid1.ExportSettings.IgnorePaging = true;
             RadGrid1.ExportSettings.OpenInNewWindow = true;
             bindGrid();
             RadGrid1.MasterTableView.ExportToExcel();
         }

         //格式化Excel
         protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
         {
             //HeadCell
             GridItem item = e.Cell.Parent as GridItem;
             if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "zzlb")
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

         protected void RadioButtonListSZD_SelectedIndexChanged(object sender, EventArgs e)
         {
             bindGrid();
         }
    }
}
