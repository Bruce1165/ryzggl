using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{
    public partial class QuestionEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "QuestionMgr.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                if (string.IsNullOrEmpty(Request["o"]) == false)//update              
                {
                    TrainQuestionMDL _TrainQuestionMDL = TrainQuestionDAL.GetObject(Convert.ToInt64(Request["o"]));
                    if (_TrainQuestionMDL != null)
                    {
                        ViewState["TrainQuestionMDL"] = _TrainQuestionMDL;
                        ViewState["QuestionID"] = _TrainQuestionMDL.QuestionID;
                        UIHelp.SetData(TableEdit, _TrainQuestionMDL);

                        if (_TrainQuestionMDL.SourceID.HasValue == true)
                        {
                            SourceMDL _SourceMDL = SourceDAL.GetObject(_TrainQuestionMDL.SourceID.Value);
                            if (_SourceMDL != null)
                            {
                                SelectSource.SetValue(_SourceMDL);
                            }
                        }
                 
                        RadNumericTextBoxScore.Text = _TrainQuestionMDL.Score.ToString();
                        RadioButtonListStatus.Items.FindByValue(_TrainQuestionMDL.Flag).Selected = true;
                        BindAttachement(_TrainQuestionMDL);//绑定试题选项
                    }
                }
            }
        }

        //绑定试题选项
        protected void BindAttachement(TrainQuestionMDL _TrainQuestionMDL)
        {

            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add("QuestionID=" + _TrainQuestionMDL.QuestionID.ToString());
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridQuestOption.CurrentPageIndex = 0;
            RadGridQuestOption.DataSourceID = ObjectDataSource1.ID;
        }

        //获取答案选项
        private string GetAnswer()
        {
            System.Web.UI.WebControls.CheckBox cbox = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i <= RadGridQuestOption.Items.Count - 1; i++)
            {
                cbox = (System.Web.UI.WebControls.CheckBox)RadGridQuestOption.Items[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    sb.Append(RadGridQuestOption.MasterTableView.DataKeyValues[i]["OptionNo"].ToString());
                }      
            }

            if (sb.Length > 0)
                return sb.ToString();
            else return "";
        }

        //保存试题
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if(SelectSource.SourceID.HasValue==false)
            {
                UIHelp.layerAlert(Page, "隶属课程不能为空！");
                return;
            }
            TrainQuestionMDL _TrainQuestionMDL = ViewState["TrainQuestionMDL"] == null ? new TrainQuestionMDL() : (TrainQuestionMDL)ViewState["TrainQuestionMDL"];
            _TrainQuestionMDL.Title = RadTextBoxTitle.Text.Trim(); //标题
            
            _TrainQuestionMDL.QuestionType = RadComboBoxQuestionType.SelectedValue;//类型
            _TrainQuestionMDL.LastModifyTime = DateTime.Now;//修改时间
            _TrainQuestionMDL.Score = Convert.ToInt32(RadNumericTextBoxScore.Value);//分数
            _TrainQuestionMDL.Flag = RadioButtonListStatus.SelectedValue;//状态
            _TrainQuestionMDL.Answer = GetAnswer();  //标注答案
            _TrainQuestionMDL.QuestionNo = RadTextBoxQuestionNo.Text.Trim();//试题编号
            _TrainQuestionMDL.SourceID = SelectSource.SourceID;//隶属课程

            if (_TrainQuestionMDL.Answer.Length > 1 && (_TrainQuestionMDL.QuestionType == "判断题" || _TrainQuestionMDL.QuestionType == "单选题"))
            {
                UIHelp.layerAlert(Page, "你选择的试题类型只能有一个答案，请重新勾选答案。");
                return;
            }

            try
            {
                if (ViewState["TrainQuestionMDL"] == null)//new
                {
                    TrainQuestionDAL.Insert(_TrainQuestionMDL);
                    UIHelp.WriteOperateLog(UserName, UserID, "新增公益教育试题", string.Format("试题ID：“{0}”。", _TrainQuestionMDL.QuestionID.ToString()));
                    ViewState["TrainQuestionMDL"] = _TrainQuestionMDL;
                    ViewState["QuestionID"] = _TrainQuestionMDL.QuestionID;
                    BindAttachement(_TrainQuestionMDL);//绑定试题选项
                    UIHelp.layerAlert(Page, "保存成功！");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true;parent.refreshGrid(); ", true);
                }
                else//update
                {
                    TrainQuestionDAL.Update(_TrainQuestionMDL);
                    UIHelp.WriteOperateLog(UserName, UserID, "更新公益教育试题", string.Format("试题ID：“{0}”。", _TrainQuestionMDL.QuestionID.ToString()));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "parent.refreshGrid();hideIfam();", true);
                    UIHelp.layerAlert(Page, "保存成功！");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true;parent.refreshGrid(); ", true);
                }

               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "公益教育试题编辑失败！", ex);
                return;
            }
            //UIHelp.layerAlert(Page, "保存成功！");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "parent.refreshGrid();hideIfam();", true);            
        }


        //添加选项
        protected void RadGridQuestOption_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (ViewState["QuestionID"] == null)
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "请先保存试题，再添加试题选项！");
                return;
            }
            GridEditableItem item = (GridEditableItem)e.Item;
            TrainQuestOptionMDL _TrainQuestOptionMDL = new TrainQuestOptionMDL();
            UIHelp.GetData(item, _TrainQuestOptionMDL);
            _TrainQuestOptionMDL.QuestionID = Convert.ToInt32(ViewState["QuestionID"]);
            try
            {
                if (TrainQuestOptionDAL.SelectCount(string.Format(" and QuestionID={0} and OptionNo ='{1}'", ViewState["QuestionID"].ToString(), _TrainQuestOptionMDL.OptionNo)) == 0)
                {
                    TrainQuestOptionDAL.Insert(_TrainQuestOptionMDL);
                }
                else
                {
                    e.Canceled = true;
                    UIHelp.layerAlert(Page, "选项已存在，不能重复创建选项！");
                    return;
                }
                //UIHelp.layerAlert(Page, "添加成功！");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加公益教育试题选项失败！", ex);
                return;
            }
            BindAttachement((TrainQuestionMDL)ViewState["TrainQuestionMDL"]);//绑定试题选项
        }

        //修改选项
        protected void RadGridQuestOption_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            TrainQuestOptionMDL _TrainQuestOptionMDL = TrainQuestOptionDAL.GetObject(Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["QuestOptionID"]));
            string OptionNo = _TrainQuestOptionMDL.OptionNo;
            UIHelp.GetData(item, _TrainQuestOptionMDL);
            try
            {
                if (_TrainQuestOptionMDL.OptionNo != OptionNo)
                {
                    if (TrainQuestOptionDAL.SelectCount(string.Format(" and QuestionID={0} and OptionNo ='{1}'", ViewState["QuestionID"].ToString(), _TrainQuestOptionMDL.OptionNo)) > 0)                
                    {
                        e.Canceled = true;
                        UIHelp.layerAlert(Page, "选项已存在，不能重复创建选项！");
                        return;
                    }
                }

                TrainQuestOptionDAL.Update(_TrainQuestOptionMDL);
                //UIHelp.layerAlert(Page, "更新成功！");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修改公益教育试题选项失败！", ex);
                return;
            }
            BindAttachement((TrainQuestionMDL)ViewState["TrainQuestionMDL"]);//绑定试题选项
        }

        //删除选项
        protected void RadGridQuestOption_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridItem item = (GridItem)e.Item;
            Int64 QuestOptionID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["QuestOptionID"]);

            try
            {
                TrainQuestOptionDAL.Delete(QuestOptionID);
                //UIHelp.layerAlert(Page, "删除成功！");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除公益教育试题选项失败！", ex);
                return;
            }

            BindAttachement((TrainQuestionMDL)ViewState["TrainQuestionMDL"]);//绑定试题选项
        }

        //绑定勾选答案
        protected void RadGridQuestOption_DataBound(object sender, EventArgs e)
        {
            if (ViewState["TrainQuestionMDL"] != null)
            {
                TrainQuestionMDL _TrainQuestionMDL = (TrainQuestionMDL)ViewState["TrainQuestionMDL"];
                if (string.IsNullOrEmpty(_TrainQuestionMDL.Answer) == false)
                {
                    System.Web.UI.WebControls.CheckBox cbox = null;
                    for (int i = 0; i <= RadGridQuestOption.Items.Count - 1; i++)
                    {
                        if (_TrainQuestionMDL.Answer.Contains(RadGridQuestOption.MasterTableView.DataKeyValues[i]["OptionNo"].ToString()) == true)
                        {
                            cbox = (System.Web.UI.WebControls.CheckBox)RadGridQuestOption.Items[i].FindControl("CheckBox1");
                            cbox.Checked = true;
                        }
                    }
                }
            }
        }     
    }
}