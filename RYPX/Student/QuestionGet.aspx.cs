using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

public partial class Student_QuestionGet : BasePage
{
    protected override bool IsNeedLogin
    {
        get
        {
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"
            SELECT w.[QuestionID]
                  ,w.[Flag]
                  ,w.[QuestionType]
                  ,w.[QuestionNo]
                  ,w.[Title]
                  ,w.[A]
                  ,w.[B]
                  ,w.[C]
                  ,w.[D]
                  ,w.[E]
                  ,w.[F]
	              ,s.[WorkerCertificateCode]
	              ,s.[SaveYear]
	              ,s.[SaveTime]
	              ,s.[CheckItem]
              FROM [dbo].[WenJuan] w
              left join [dbo].[WenJuanSub] s on s.[WorkerCertificateCode]='{0}' and [SaveYear]={1} and w.[QuestionID] = s.[QuestionID]
              Where w.[Flag] = 1 and w.[QuestionType] = '公益平台问卷' 
              order by w.[QuestionNo]"
              , WorkerCertificateCode,DateTime.Now.Year ));

            RadGridQuestion.DataSource = dt;
            RadGridQuestion.DataBind();
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        try
        {
            string[] list = new string[RadGridQuestion.MasterTableView.Items.Count];
            string temp = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Telerik.Web.UI.GridDataItem i in RadGridQuestion.MasterTableView.Items)
            {
                temp = GetSelect(i);
                if (temp.Length > 1)
                {
                    sb.AppendFormat("<br>{0}", temp);
                }
                else
                {
                    list[i.ItemIndex] = temp;
                }
            }
            if (sb.Length > 0)
            {
                UIHelp.layerAlert(Page, sb.ToString());
                return;
            }
            else
            {

                DateTime savetime = DateTime.Now;
           
                for (int i = 0; i < list.Length; i++)
                {
                    if (RadGridQuestion.MasterTableView.DataKeyValues[0]["WorkerCertificateCode"] == DBNull.Value)
                    {
                        sb.AppendFormat(@"INSERT INTO [dbo].[WenJuanSub]([QuestionID],[WorkerCertificateCode],[SaveYear],[SaveTime],[CheckItem])
                                        VALUES ({0},'{1}',{2},'{3}','{4}');"
                        , RadGridQuestion.MasterTableView.DataKeyValues[i]["QuestionID"], WorkerCertificateCode, savetime.Year, savetime.ToString("yyyy-MM-dd HH:mm:ss"), list[i]);
                    }
                    else
                    {
                        sb.AppendFormat(@"UPDATE [dbo].[WenJuanSub]
                                       SET [SaveTime] = '{3}',[CheckItem] = '{4}'
                                     WHERE [QuestionID] = {0} and [WorkerCertificateCode] ='{1}' and [SaveYear] = {2};"
                        , RadGridQuestion.MasterTableView.DataKeyValues[i]["QuestionID"], WorkerCertificateCode, savetime.Year, savetime.ToString("yyyy-MM-dd HH:mm:ss"), list[i]);
                    }
                }
                CommonDAL.ExecSQL(sb.ToString());

            }
        }
        catch (Exception ex)
        {
            UIHelp.WriteErrorLog(Page, "提交公益培训问卷调查失败！", ex);
            return;
        }
        UIHelp.layerAlert(Page, "提交成功", "var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);");
    }

    protected string GetSelect(GridDataItem i)
    {
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonA")).Checked) return "A";
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonB")).Checked) return "B";
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonC")).Checked) return "C";
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonD")).Checked) return "D";
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonE")).Checked) return "E";
        if (((RadioButton)i.Cells[0].FindControl("RadioButtonF")).Checked) return "F";
        return string.Format("第{0}题尚未选择答案。", i.ItemIndex + 1);
    }
}