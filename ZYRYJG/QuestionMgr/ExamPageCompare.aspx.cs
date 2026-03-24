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
    public partial class ExamPageCompare : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainExamPage.aspx";
            }
        }

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
            q.Add(string.Format("PostID = {0}", Request["p"]));//岗位工种
            //if (PostSelect.PostID != "")
            //{
            //    q.Add(string.Format("PostID = {0}", PostSelect.PostID));//岗位工种
            //}
            //else if (PostSelect.PostTypeID != "")
            //{
            //    q.Add(string.Format("PostTypeID = {0}", PostSelect.PostTypeID)); //岗位类别
            //}
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

     
        //变换年度过滤条件
        protected void RadComboBoxYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindRadGridPost();//绑定试卷Grid
        }
        

        //比对
        protected void ButtonCompare_Click(object source, EventArgs e)
        {
            if (RadGridPost.SelectedIndexes.Count == 0)
            {
                UIHelp.layerAlert(Page, "请选择一个要比对的试卷!");
                return;
            }
              //比较出题重复率
      
            string sql = @"select 
                            isnull((
                            SELECT count(*)
                            FROM DBO.PAGEQUESTION t1	
                            inner join DBO.PAGEQUESTION t2	
                            on  t1.EXAMPAGEID ={0} and t2.EXAMPAGEID ={1} and  t1.QUESTIONID= t2.QUESTIONID
                            ),0) 
                         * 100 
                         / (select count(*) from DBO.PAGEQUESTION where EXAMPAGEID ={0});";

            object rtn = null;
            DBHelper db = new DBHelper();
            try
            {
                rtn=db.ExecuteScalar(string.Format(sql, Request["o"], RadGridPost.MasterTableView.DataKeyValues[Convert.ToInt32(RadGridPost.SelectedIndexes[0])]["ExamPageID"].ToString()));
            }
            catch (Exception ex)
            {
                LabelResult.Text = "";
                UIHelp.WriteErrorLog(Page, "对比试卷失败", ex);
                return;
            }

            LabelResult.Text = string.Format("试题重复率：{0}%", rtn.ToString());
          }

    }
}