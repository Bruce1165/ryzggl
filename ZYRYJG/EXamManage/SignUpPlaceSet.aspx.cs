using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using Utility;

namespace ZYRYJG.EXamManage
{
    public partial class SignUpPlaceSe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) ButtonSearch_Click(sender,e);
        }
        //添加事触发的事件
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            try
            {
                RadListBox rlbTrain = editedItem.FindControl("RadListBoxTrainUnit") as RadListBox;
                foreach (RadListBoxItem li in rlbTrain.Items)
                {
                    if (li.Checked == true)
                    {
                        SignUpPlaceOB ob = new SignUpPlaceOB();
                        ob.SignUpPlaceID = Convert.ToInt64(li.Value);
                        ob.PlaceName = li.Text;
                        ob.CheckPersonLimit = 1000;
                        SignUpPlaceDAL.Insert(ob);
                        UIHelp.WriteOperateLog(PersonName, UserID, "新增报名点", string.Format("新增报名点：{0}。", li.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "新增报名点失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, "新增报名点成功！",6,3000);          
        
        }

        //修改是触发的事件
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            SignUpPlaceOB ob = ViewState["cob"] as SignUpPlaceOB;//考点对象实例
            UIHelp.GetData(editedItem, ob);
            try
            {
                SignUpPlaceDAL.Update(ob);
                UIHelp.WriteOperateLog(PersonName, UserID, "更新报名点", string.Format("报名点名称：{0}。", ob.PlaceName));
                UIHelp.layerAlert(Page, "报名点修改成功！");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "报名点修改失败！", ex);
                return;
            }

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                    System.Web.UI.HtmlControls.HtmlTable TableEdit = e.Item.FindControl("TableEdit") as System.Web.UI.HtmlControls.HtmlTable;
                    TableEdit.Visible = false;


                    //绑定培训点列表
                    DataTable dtTrainUnit = UserDAL.GetList(0, int.MaxValue - 1, " and OrganID=245 and userID not in(SELECT SIGNUPPLACEID FROM DBO.SIGNUPPLACE)", "RelUserName");

                    RadListBox rlb = e.Item.FindControl("RadListBoxTrainUnit") as RadListBox;
                    rlb.DataSource = dtTrainUnit;
                    rlb.DataTextField = "RelUserName";
                    rlb.DataValueField = "UserID";
                    rlb.DataBind();
                    rlb.Visible = true;

                    System.Web.UI.HtmlControls.HtmlGenericControl LabelTrainUnit = e.Item.FindControl("LabelTrainUnit") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    LabelTrainUnit.Visible = true;
                }
                else//update
                {
                    RadListBox rlb = e.Item.FindControl("RadListBoxTrainUnit") as RadListBox;
                    rlb.Visible = false;
                    System.Web.UI.HtmlControls.HtmlGenericControl LabelTrainUnit = e.Item.FindControl("LabelTrainUnit") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    LabelTrainUnit.Visible = false;

                    System.Web.UI.HtmlControls.HtmlTable TableEdit = e.Item.FindControl("TableEdit") as System.Web.UI.HtmlControls.HtmlTable;
                    TableEdit.Visible = true;

                    //绑定考点
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    SignUpPlaceOB cob = SignUpPlaceDAL.GetObject(Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SIGNUPPLACEID"]));
                    UIHelp.SetData(editedItem, cob);
                    ViewState["cob"] = cob;
                }
            }
        }

        //根据输入的条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (RadTxtExamPlaceName.Text != "")
            {
                //报名点名称
                q.Add(string.Format("PlaceName like '%{0}%'", RadTxtExamPlaceName.Text.Trim()));
            }
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete")
            {
                Int64 id = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SIGNUPPLACEID"]);
                string PlaceName = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PlaceName"].ToString();
                try
                {
            
                    SignUpPlaceDAL.Delete(id);
                    UIHelp.layerAlert(this.Page, "报名点删除成功！", 6, 3000);
                    RadGrid1.DataBind();

                    UIHelp.WriteOperateLog(PersonName, UserID, "删除报名点", string.Format("报名点名称：{0}。", PlaceName));   
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "考点删除失败！", ex);
                    return;
                }

            }
        }
    }
}
