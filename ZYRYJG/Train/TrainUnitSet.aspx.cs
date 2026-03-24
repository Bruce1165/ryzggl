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

namespace ZYRYJG.Train
{
    public partial class TrainUnitSet : BasePage
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

                DataTable dt = CommonDAL.GetDataTable("SELECT * FROM [dbo].[TrainUnit] order by [UnitNo]");
                RadGrid1.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取续期时间设置失败！", ex);
                return;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            ////格式化列
            //if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            //{
            //    string[] settings = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TypeValue"].ToString().Split(',');
            //    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("ValidTo").OrderIndex].Text = settings[1].Replace("-", "月") + "日";
            //    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("RenewMonth").OrderIndex].Text = string.Format("{0}月-{1}月", settings[2], formatContinueEndMonth(Convert.ToInt32(settings[3])));
            //}

            //绑定编辑数据
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;

                //初始化待选择岗位工种
                CheckBoxList _CheckBoxListPostID = (CheckBoxList)e.Item.FindControl("CheckBoxListPostID");


                QueryParamOB q = new QueryParamOB();
                q.Add("PostType = '2' and UpPostID = 4000");
                DataTable dtPostID = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "PostName");
                _CheckBoxListPostID.DataSource = dtPostID;
                _CheckBoxListPostID.DataBind();

                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    TrainUnitMDL ob = TrainUnitDAL.GetObject(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UnitNo"].ToString());

                    RadioButtonList RadioButtonListUseStatus = e.Item.FindControl("RadioButtonListUseStatus") as RadioButtonList;
                    RadioButtonListUseStatus.Items.FindByValue(ob.UseStatus.ToString()).Selected = true;

                    CheckBoxList CheckBoxListPostID = e.Item.FindControl("CheckBoxListPostID") as CheckBoxList;
                    string[] ListPostID = ob.PostSet.Split(',');
                    foreach(string s in ListPostID)
                    {
                        CheckBoxListPostID.Items.FindByValue(s).Selected = true;
                    }
                }

            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RefreshGrid();
        }

        //修改
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TrainUnitMDL ob = TrainUnitDAL.GetObject(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UnitNo"].ToString());
            UIHelp.GetData(editedItem, ob);

            //状态
            RadioButtonList RadioButtonListUseStatus = e.Item.FindControl("RadioButtonListUseStatus") as RadioButtonList;
            ob.UseStatus=Convert.ToInt32(RadioButtonListUseStatus.SelectedValue) ;

            //可用工种
            CheckBoxList CheckBoxListPostID = e.Item.FindControl("CheckBoxListPostID") as CheckBoxList;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (ListItem s in CheckBoxListPostID.Items)
            {
                if(s.Selected==true)
                {
                    sb.Append(",").Append(s.Value);
                }                
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            ob.PostSet = sb.ToString();
            try
            {
                TrainUnitDAL.Update(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "培训点配置失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "修改配置培训点配置", string.Format("培训点：{0}。", ob.TrainUnitName));
            UIHelp.layerAlert(Page, "保存成功！");
        }

        //新增
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TrainUnitMDL ob = new TrainUnitMDL();
            UIHelp.GetData(editedItem, ob);

            //状态
            RadioButtonList RadioButtonListUseStatus = e.Item.FindControl("RadioButtonListUseStatus") as RadioButtonList;
            ob.UseStatus = Convert.ToInt32(RadioButtonListUseStatus.SelectedValue);

            //可用工种
            CheckBoxList CheckBoxListPostID = e.Item.FindControl("CheckBoxListPostID") as CheckBoxList;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (ListItem s in CheckBoxListPostID.Items)
            {
                if (s.Selected == true)
                {
                    sb.Append(",").Append(s.Value);
                }
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            ob.PostSet = sb.ToString();

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                TrainUnitDAL.Insert(trans, ob);

                string sql = string.Format(@"INSERT INTO [dbo].[EXAMPLACE]([EXAMPLACENAME],[EXAMPLACEADDRESS],[LINKMAN],[PHONE],[ROOMNUM],[EXAMPERSONNUM],[STATUS])
                            select '{0}','{1}','{2}','{3}',1,9999,'已删除'"
                    , ob.TrainUnitName, ob.TrainUnitName, ob.UnitCode, ob.UnitNo);

                CommonDAL.ExecSQL(trans, sql);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "新增培训点配置失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "新增培训点配置", string.Format("培训点：{0}。", ob.TrainUnitName));
            UIHelp.layerAlert(Page, "保存成功！");
        }

       //删除
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string UnitNo = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UnitNo"].ToString();             //Convert.ToInt32(e.CommandArgument);

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                TrainUnitDAL.Delete(trans, UnitNo);

                string sql = string.Format("DELETE FROM [dbo].[EXAMPLACE] WHERE [STATUS]='已删除' and [PHONE] ='{0}'", UnitNo);
                CommonDAL.ExecSQL(trans, sql);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "删除培训点设置失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除培训点设置", string.Format("培训点序号：{0}。",UnitNo));
            UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
        }

    }
}
