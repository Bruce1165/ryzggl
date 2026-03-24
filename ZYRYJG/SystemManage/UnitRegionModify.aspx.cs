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

namespace ZYRYJG.SystemManage
{
    public partial class UnitRegionModify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) ButtonSearch_Click(sender,e);
        }
        //添加事触发的事件
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            jcsjk_QY_ZHXX_CITY_ERRORMDL ob = new jcsjk_QY_ZHXX_CITY_ERRORMDL();
            UIHelp.GetData(editedItem, ob);
          
            try
            {
                jcsjk_QY_ZHXX_CITY_ERRORDAL.Insert(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加特殊企业隶属关系失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "添加特殊企业隶属关系", string.Format("{0}：{1}。", ob.QYMC,ob.XZDQBM));
            UIHelp.layerAlert(Page, "添加成功！", 6, 3000);
            ViewState["jcsjk_QY_ZHXX_CITY_ERRORMDL"] = ob;
        }

        //修改是触发的事件
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            jcsjk_QY_ZHXX_CITY_ERRORMDL ob = ViewState["jcsjk_QY_ZHXX_CITY_ERRORMDL"] as jcsjk_QY_ZHXX_CITY_ERRORMDL;//考点对象实例
            UIHelp.GetData(editedItem, ob);
            try
            {
                jcsjk_QY_ZHXX_CITY_ERRORDAL.Update(ob);               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "特殊企业隶属关系修改失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "更新特殊企业隶属关系", string.Format("{0}：{1}。", ob.QYMC, ob.XZDQBM));
            UIHelp.layerAlert(Page, "修改成功！", 6, 3000);
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    jcsjk_QY_ZHXX_CITY_ERRORMDL cob = jcsjk_QY_ZHXX_CITY_ERRORDAL.GetObject(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["QYMC"].ToString());
                    UIHelp.SetData(editedItem, cob);
                    ViewState["jcsjk_QY_ZHXX_CITY_ERRORMDL"] = cob;


                    RadTextBox RadTextBoxQYMC = (RadTextBox)editedItem.FindControl("RadTextBoxQYMC");
                    UIHelp.SetReadOnly(RadTextBoxQYMC, true);
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
         
            if (RadTextBoxUnitName.Text != "")
            {
                //考点名称
                q.Add(string.Format("QYMC like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete")
            {
                string QYMC = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["QYMC"].ToString();
                try
                {      
                    jcsjk_QY_ZHXX_CITY_ERRORDAL.Delete(QYMC);
                     
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "特殊企业隶属关系删除失败！", ex);
                    return;
                }

                UIHelp.layerAlert(this.Page, "删除成功！", 6, 3000);
                RadGrid1.DataBind();

                UIHelp.WriteOperateLog(PersonName, UserID, "删除特殊企业隶属关系", string.Format("企业名称：{0}。", QYMC));

            }
        }
    }
}
