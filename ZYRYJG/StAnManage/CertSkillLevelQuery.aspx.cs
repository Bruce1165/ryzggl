using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;

namespace ZYRYJG.StAnManage
{
    public partial class CertSkillLevelQuery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadComboBoxTrainUnit.DataSource = CommonDAL.GetDataTable("select '000' as 'UnitNo','全部' as 'TrainUnitName' union all SELECT [UnitNo],TrainUnitName from [dbo].[TrainUnit] where [UseStatus]=1 order by UnitNo");
                RadComboBoxTrainUnit.DataTextField = "TrainUnitName";
                RadComboBoxTrainUnit.DataValueField = "TrainUnitName";
                RadComboBoxTrainUnit.DataBind();

                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //统计结果模版
            DataTable dtBase = CertificateDAL.AnalysisCertificateSkillLevelBase(Convert.ToInt32(RadioButtonListPostType.SelectedValue), null);
            DataRow drSum = dtBase.NewRow();
            dtBase.Rows.Add(drSum);
            drSum["PostName"] = "总计";
            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dtBase.Columns["PostName"];
            dtBase.PrimaryKey = dcKeys;

            //填充统计数据
            DataTable dtData = CertificateDAL.AnalysisCertificateSkillLevel(RadDatePicker_StartDate.SelectedDate, RadDatePicker_EndDate.SelectedDate, Convert.ToInt32(RadioButtonListPostType.SelectedValue), null
                , RadioButtonListPostType.SelectedValue == "4" ? "" : RadComboBoxTrainUnit.SelectedValue == "全部" ? "" : RadComboBoxTrainUnit.SelectedValue);
            DataRow drFind = null;
            foreach (DataRow dr in dtData.Rows)
            {
                drFind = dtBase.Rows.Find(dr["PostName"]);
                if (drFind != null && dtBase.Columns.Contains(dr["SkillLevel"].ToString())==true) drFind[dr["SkillLevel"].ToString()] = dr["count"];
            }

            //计算 合格率、合计
            System.Collections.Generic.Dictionary<string, int> sum = new Dictionary<string, int>() { { "初级工", 0 }, { "中级工", 0 }, { "高级工", 0 }, { "技师", 0 }, { "高级技师", 0 }, { "无", 0 }, { "小计", 0 } };
            string[] keys = new string[7];
            sum.Keys.CopyTo(keys, 0);
            int rowSum = 0;
            for (int i = 0; i < dtBase.Rows.Count - 1; i++)
            {
                ////合格率
                //if (dtBase.Rows[i]["考试人数"].ToString() != "0")
                //{
                //    dtBase.Rows[i]["合格率"] = Math.Round(Convert.ToDouble(dtBase.Rows[i]["合格人数"]) / Convert.ToDouble(dtBase.Rows[i]["考试人数"]) * 100, 1);
                //}
                //else
                //    dtBase.Rows[i]["合格率"] = DBNull.Value;

                //小计
                rowSum = 0;                
               
                foreach (string s in keys)
                {
                    rowSum += Convert.ToInt32(dtBase.Rows[i][s]); //小计
                    sum[s] += Convert.ToInt32(dtBase.Rows[i][s]); //合计
                }
                dtBase.Rows[i]["小计"] = rowSum;
            }
            foreach (string s in keys)
            {
                drSum[s] = sum[s];
            }
            //if (drSum["考试人数"].ToString() != "0")
            //{
            //    drSum["合格率"] = Math.Round(Convert.ToDouble(drSum["合格人数"]) / Convert.ToDouble(drSum["考试人数"]) * 100, 1);
            //}
            //else
            //    drSum["合格率"] = DBNull.Value;
            RadGrid1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            RadGrid1.DataSource = dtBase;
            RadGrid1.DataBind();
        }

        //导出审批列表
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.FileName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), RadioButtonListPostType.SelectedItem.Text);
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "PostTypeName")
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

        //变换证书类型
        protected void RadioButtonListPostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListPostType.SelectedValue == "4")
            {
                p_TrainUnit.Style.Add("display", "none");
                RadComboBoxTrainUnit.Visible = false;
            }
            else
            {
                p_TrainUnit.Style.Add("display", "inline");
                RadComboBoxTrainUnit.Visible = true;
            }
            ButtonSearch_Click(sender, e);
        }
    }
}