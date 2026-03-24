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
    public partial class ExamInfoRight : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) ButtonSearch_Click(sender,e);
        }
        //添加事触发的事件
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ExamPlaceOB examob = new ExamPlaceOB();//考点对象实例
            UIHelp.GetData(editedItem, examob);//考点对象的属性赋值
            examob.Status = EnumManager.ExamPlaceStatus.UnDelete;// "未删除";
            try
            {
                ExamPlaceDAL.Insert(examob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "考点添加失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "新增考点", string.Format("考点名称：{0}。", examob.ExamPlaceName));
            UIHelp.layerAlert(Page, "考点添加成功！",6,3000);
        }

        //修改是触发的事件
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ExamPlaceOB examob = ViewState["cob"] as ExamPlaceOB;//考点对象实例
            UIHelp.GetData(editedItem, examob);
            try
            {
                ExamPlaceDAL.Update(examob);
                UIHelp.WriteOperateLog(PersonName, UserID, "更新考点", string.Format("考点名称：{0}。", examob.ExamPlaceName));
                UIHelp.layerAlert(Page, "考点修改成功！",6,3000);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "考点修改失败！", ex);
                return;
            }

            //Popwin.ShowMessage("考点修改成功！", this.Page);
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
                    //绑定考点
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    ExamPlaceOB cob = ExamPlaceDAL.GetObject(Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlaceID"]));
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
            q.Add(string.Format("Status='{0}'",EnumManager.ExamPlaceStatus.UnDelete));
            if (RadTxtExamPlaceName.Text != "")
            {
                //考点名称
                q.Add(string.Format("ExamPlaceName like '%{0}%'", RadTxtExamPlaceName.Text.Trim()));
            }
            if (RadTxtExamPlaceAddress.Text != "")
            {
                //考点地址
                q.Add(string.Format("ExamPlaceAddress like'%{0}%'", RadTxtExamPlaceAddress.Text.Trim()));
            }
            if (RadTxtLinkMan.Text != "")
            {
                //考点联系人
                q.Add(string.Format("LinkMan like'%{0}%'", RadTxtLinkMan.Text.Trim()));
            }
            if (RadTxtPhone.Text != "")
            {
                //考点联系人方式
                q.Add(string.Format("Phone like'%{0}%'", RadTxtPhone.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete")
            {
                Int64 id = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlaceID"]);
                try
                {
                    ExamPlaceOB ob = ExamPlaceDAL.GetObject(id);
                    ob.Status = EnumManager.ExamPlaceStatus.Deleted;// "已删除"
                    ExamPlaceDAL.Update(ob);
                    UIHelp.layerAlert(this.Page, "考点删除成功！",6,3000);
                    RadGrid1.DataBind();

                    UIHelp.WriteOperateLog(PersonName, UserID, "删除考点", string.Format("考点名称：{0}。",ob.ExamPlaceName));   
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
