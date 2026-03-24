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
    public partial class MainExamOutlineNew : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainExamOutline.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////考试年度
                //DataTable dtYear = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ExamRange", "max(ExamYear)", " and Flag=1", "ExamYear desc");
                //if (dtYear == null || dtYear.Rows.Count == 0 || dtYear.Rows[0][0] == DBNull.Value)
                //{
                //    RadNumericTextBoxYear.Value = DateTime.Now.Year;//当年
                //}
                //else
                //{
                //    RadNumericTextBoxYear.Value = Convert.ToInt32(dtYear.Rows[0][0]) + 1;//最大年度+1
                //}
            }
        }

     

        //保存考试大纲
        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            //重复检查
            if (CheckBoxClearFirst.Checked == false)//不覆盖已存在项
            {
                int rowCount = CommonDAL.SelectRowCount("dbo.VIEW_EXAMRANGE", string.Format(" and flag=1 and ExamYear={0} {1} {2}"
                    , RadNumericTextBoxYear.Value.ToString()
                    , PostSelect.PostTypeID == "" ? "" : string.Format(" and PostTypeID={0}", PostSelect.PostTypeID)
                    , PostSelect.PostID == "" ? "" : string.Format(" and PostID={0}", PostSelect.PostID)));

                if (rowCount > 0)
                {
                    UIHelp.layerAlert(Page, "您所选年度、岗位工种的考试大纲已经存在，无法重复创建。如果要重新生成请勾选覆盖已存在选项。");
                    return;
                }
            }

            //保存
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (CheckBoxClearFirst.Checked == true)
                {
                    ExamRangeDAL.Delete(tran, Convert.ToInt16(RadNumericTextBoxYear.Value), PostSelect.PostTypeID, PostSelect.PostID);
                }

                //拷贝知识大纲
                ExamRangeDAL.InsertPatch(tran, Convert.ToInt32(RadNumericTextBoxYear.Value),PersonID,DateTime.Now,PostSelect.PostTypeID,PostSelect.PostID);

                //拷贝知识大纲
                ExamRangeDAL.InsertPatchExamRangeSub(tran, Convert.ToInt32(RadNumericTextBoxYear.Value), PostSelect.PostTypeID, PostSelect.PostID);
               
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "创建考试大纲失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "创建考试大纲", string.Format("创建考试大纲：年度：{0}。", RadNumericTextBoxYear.Value.ToString()));

            UIHelp.layerAlert(Page, "创建考试大纲成功！");
        }

        //取消
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainExamOutline.aspx",false);
        }

     
    }
}