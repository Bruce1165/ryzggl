using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    //公示
    public partial class PublicityLook : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //BindRadGridHZSB();
            }
        }
        protected void BindRadGridHZSB()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("PublicCode like'%{0}%'", RadTextBoxPublicCode.Text));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridHZSB.CurrentPageIndex = 0;
            RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            BindRadGridHZSB();
        }
        //模板列按钮
        protected void RadGridHZSB_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "report"://公示
                    try
                    {
                        ApplyDAL.ExePublicSelect(EnumManager.ApplyStatus.已公示
                        , DateTime.Now
                        , UserName
                        , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PublicCode"].ToString());
                        BindRadGridHZSB();
                        UIHelp.WriteOperateLog(UserName, UserID, "公示成功", string.Format("公示时间：{0}", DateTime.Now));
                        UIHelp.layerAlert(Page, "公示成功！",6,2000);

                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "公示失败", ex);
                    }
                    break;
                case "Cancelreport"://取消重新选择
                    try
                    {
                        ApplyDAL.DeleteUpdatePublic(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PublicCode"].ToString());
                        BindRadGridHZSB();
                        UIHelp.layerAlert(Page, "取消成功！",6,2000);

                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "取消失败", ex);
                    }
                    break;

            }
        }

    }
}