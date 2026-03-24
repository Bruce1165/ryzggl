using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.SystemManage
{
    public partial class RenewSetting : BasePage
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) RefreshGrid();
        }

        protected void RefreshGrid()
        {
            DBHelper db = new DBHelper();
            try
            {
                DataTable dt = TypesDAL.GetListByTypeID("106");//续期时间设置
                RadGrid1.DataSource = dt;
                //RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取续期时间设置失败！", ex);
                return;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //格式化列
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string[] settings = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TypeValue"].ToString().Split(',');
                e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("ValidTo").OrderIndex].Text = settings[1].Replace("-","月") +"日";
                e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("RenewMonth").OrderIndex].Text = string.Format("{0}月-{1}月", settings[2], formatContinueEndMonth(Convert.ToInt32(settings[3])));
            }

            //绑定编辑数据
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                TypesOB ob = TypesDAL.GetObject(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TypeID"].ToString(), RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TypeName"].ToString());
                ViewState["TypesOB"] = ob;
                Label LabelTypeName = e.Item.FindControl("LabelTypeName") as Label;
                Label LabelValidTo = e.Item.FindControl("LabelValidTo") as Label;
                RadComboBox RadComboBoxMonthStart = e.Item.FindControl("RadComboBoxMonthStart") as RadComboBox;
                RadComboBox RadComboBoxMonthEnd = e.Item.FindControl("RadComboBoxMonthEnd") as RadComboBox;
                string[] settings = ob.TypeValue.Split(',');
                LabelTypeName.Text = ob.TypeName;
                LabelValidTo.Text = settings[1].Replace("-", "月") + "日"; 
                RadComboBoxMonthStart.FindItemByValue(settings[2]).Selected = true;
                RadComboBoxMonthEnd.FindItemByValue(settings[3]).Selected = true;
            }
        }

        /// <summary>
        /// 格式化续期截止月份，超过12月显示：明年XX月
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        protected string formatContinueEndMonth(int month)
        {
            if (month > 12)
            {
                return string.Format("明年{0}", month - 12);
            }
            else
            {
                return month.ToString();
            }
        }

        //修改
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TypesOB ob = ViewState["TypesOB"] as TypesOB;
            string[] settings = ob.TypeValue.Split(',');
            RadComboBox RadComboBoxMonthStart = e.Item.FindControl("RadComboBoxMonthStart") as RadComboBox;
            RadComboBox RadComboBoxMonthEnd = e.Item.FindControl("RadComboBoxMonthEnd") as RadComboBox;
            ob.TypeValue = string.Format("{0},{1},{2},{3}", settings[0], settings[1], RadComboBoxMonthStart.SelectedValue, RadComboBoxMonthEnd.SelectedValue);
            try
            {
                TypesDAL.Update(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "续期时间配置失败！", ex);
                return;
            }
            if (Cache["reNewDateSpan"] != null)
            {
                Cache.Remove("reNewDateSpan");//情况缓存
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "配置续期时间", string.Format("配置类型名称：{0}，续期开放时间段：从{1}月至{2}月。"
                , ob.TypeName, RadComboBoxMonthStart.SelectedValue, RadComboBoxMonthEnd.SelectedValue));

            UIHelp.layerAlert(Page, "续期时间配置成功！");
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshGrid();
        }
    }
}
