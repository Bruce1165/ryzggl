using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Threading;

namespace ZYRYJG.QuestionMgr
{
    public partial class MainExamPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化年度
                for(int i=DateTime.Now.Year +1;i >=2014;i--)
                {
                    RadComboBoxYear.Items.Add(new RadComboBoxItem(string.Format("{0}年", i.ToString()), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0,new RadComboBoxItem("全部",""));

                BindRadGridPost();//绑定试卷Grid

            }
        }

        //绑定试卷Grid
        private void BindRadGridPost()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            q.Add("Flag = 1");//有效
            if (PostSelect.PostID != "")
            {
                q.Add(string.Format("PostID = {0}", PostSelect.PostID));//岗位工种
            }
            else if (PostSelect.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect.PostTypeID)); //岗位类别
            }
            if (RadComboBoxYear.SelectedValue != "")
            {
                q.Add(string.Format("ExamYear = {0}", RadComboBoxYear.SelectedValue));//考试年度
            }           

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridPost.CurrentPageIndex = 0;
            RadGridPost.DataSourceID = ObjectDataSource1.ID;
        }

        //根据现实序号格式化返回数据库编号
        private int FormatTagCode(string ShowCode)
        {
            string rtn = "";
            int code = 0;
            try
            {
                string[] temp = ShowCode.Split('.');
                foreach (string s in temp)
                {
                    if (int.TryParse(Utility.Check.removeInputErrorChares(s), out code) == true)
                    {
                        if (code > 99 || code < 1) return -2;//每层编号有效范围1~99
                        rtn += code.ToString("00");
                    }
                    else
                    {
                        return -1;//无法转化为数字
                    }
                }
                return Convert.ToInt32((rtn + "00000000").Substring(0, 8));
            }
            catch
            {
                return -1;
            }
        }

        //新增试卷
        protected void RadGridInfoTag_InsertCommand(object source, GridCommandEventArgs e)
        {
            InfoTagOB ob = new InfoTagOB();
            try
            {
                //科目ID
                ob.SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());

                //显示编码
                RadTextBox RadTextBoxShowCode = e.Item.FindControl("RadTextBoxShowCode") as RadTextBox;
                ob.ShowCode = RadTextBoxShowCode.Text.Trim();

                //大纲编码
                ob.TagCode = FormatTagCode(ob.ShowCode);
                if (ob.TagCode <= 0)
                {
                    UIHelp.layerAlert(Page, "序号格式错误，请检查输入！");
                    return;
                }

                //大纲标题
                RadTextBox RadTextBoxPageTitle = e.Item.FindControl("RadTextBoxTitle") as RadTextBox;
                ob.Title = RadTextBoxPageTitle.Text.Trim();

                //状态RadTextBoxFlag
                ob.Flag = 1;

                //创建人ID
                ob.CreatePersonID = PersonID;
                //创建时间
                ob.CreateTime = DateTime.Now;
                //最后修改人ID
                ob.ModifyPersonID = PersonID;
                ob.ModifyTime = DateTime.Now;
                DataAccess.InfoTagDAL.Insert(ob);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加知识大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "新增知识大纲", string.Format("大纲名称：{0}", ob.Title));
        }
        
        //删除试卷
        protected void RadGridPost_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ExamPageDAL.Delete(Convert.ToInt64(RadGridPost.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPageID"]));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除试卷失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除试卷", string.Format("试卷名称：{0}。试卷ID：{1}",
                 e.Item.Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("ExamPageTitle").OrderIndex].Text,
                RadGridPost.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPageID"].ToString()));
            UIHelp.layerAlert(Page, "删除试卷成功！",6,3000);
        }

        //变换年度过滤条件
        protected void RadComboBoxYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindRadGridPost();//绑定试卷Grid
        }

        //变换岗位工种过滤条件
        protected void PostSelect_SelectChange(object source, EventArgs e)
        {
            BindRadGridPost();//绑定试卷Grid
        }

    }
}