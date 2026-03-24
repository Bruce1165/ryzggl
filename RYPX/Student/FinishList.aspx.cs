using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;
using System.IO;
using System.Drawing;
using Telerik.Web.UI;

public partial class Student_FinishList : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "jxjy/MyTrain.aspx";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CachejxjyClickCount += 1;
        if (!IsPostBack)
        {
            //初始化显示所有专业类型
            string sql = @"select distinct PostTypeName=case when p.PostName is null then p.PostTypeName else p.PostName end 
                          FROM [dbo].[FinishSourceWare] f
                          inner join [dbo].[Source] s on f.SourceID = s.SourceID
                          inner join [dbo].[Source] us on s.ParentSourceID = us.SourceID
                          inner join [dbo].[PackageSource] ps on us.SourceID =  ps.SourceID
                          inner join [dbo].[Package] p on ps.PackageID = p.PackageID
                          where f.WorkerCertificateCode='{0}'";
            DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format(sql, WorkerCertificateCode));
            
            foreach (DataRow r in dt.Rows)
            {
                RadComboBoxPostType.Items.Add(new Telerik.Web.UI.RadComboBoxItem(r["PostTypeName"].ToString(), r["PostTypeName"].ToString()));
            }
            RadComboBoxPostType.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("全部", ""));

            if(RadComboBoxPostType.Items.Count <3)
            {
                divCX.Style.Add("display", "none");
            }

            BindFinishList();
            BindFinishHisList();
        }
    }

    protected void BindFinishList()
    {
        try
        {
            //学习统计
            string sql = "";
            DataTable dt = null;
            if (RadComboBoxPostType.SelectedValue =="")
            {
                sql = @"select year(f.[LearnTime]) as ND,
                        sum(case when f.[FinishPeriod] >=(f.[Period] * 60) then s.[ShowPeriod] else 0 end) as FinishPeriod,
                        sum(case when f.[StudyStatus]=1 then s.[ShowPeriod] else 0.0 end) as TestPeriod
                          FROM [dbo].[FinishSourceWare] f
                          inner join [Source] s on f.SourceID = s.SourceID
                          where f.WorkerCertificateCode='{0}'
                          group by year(f.[LearnTime])
                          order by ND desc";
                dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format(sql, WorkerCertificateCode));

            }
            else
            {
                sql = @"select year(f.[LearnTime]) as ND,
                        sum(case when f.[FinishPeriod] >=(f.[Period] * 60) then s.[ShowPeriod] else 0 end) as FinishPeriod,
                        sum(case when f.[StudyStatus]=1 then s.[ShowPeriod] else 0.0 end) as TestPeriod
                          FROM [dbo].[FinishSourceWare] f
                          inner join [dbo].[Source] s on f.SourceID = s.SourceID
                          inner join [dbo].[Source] us on s.ParentSourceID = us.SourceID
                          inner join [dbo].[PackageSource] ps on us.SourceID =  ps.SourceID
                          inner join [dbo].[Package] p on ps.PackageID = p.PackageID
                          where f.WorkerCertificateCode='{0}' and ( p.PostTypeName='{1}' or p.PostName='{1}' )
                          group by year(f.[LearnTime])
                          order by ND desc";
                dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format(sql, WorkerCertificateCode, RadComboBoxPostType.SelectedValue));
            }

            RadGridFinish.DataSource = dt;
            RadGridFinish.DataBind();

           
        }
        catch (Exception ex)
        {
            UIHelp.WriteErrorLog(Page, "读取我的学习成果失败！", ex);
            return;
        }
    }

    protected void BindFinishHisList()
    {
        //学习历史记录
        ObjectDataSource1.SelectParameters.Clear();
        QueryParamOB q = new QueryParamOB();
        q.Add(string.Format("WorkerCertificateCode like '{0}'", WorkerCertificateCode));
        if (RadComboBoxPostType.SelectedValue != "")
        {
            q.Add(string.Format("PostTypeName like '%{0}%'", RadComboBoxPostType.SelectedValue));//适用岗位
        }


        ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
        RadGridSource.CurrentPageIndex = 0;
        RadGridSource.DataSourceID = ObjectDataSource1.ID;
    }

    protected void RadComboBoxPostType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindFinishList();
        BindFinishHisList();
    }

    //删除学习记录
    protected void RadGridSource_DeleteCommand(object source, GridCommandEventArgs e)
    {
        GridItem item = (GridItem)e.Item;
        Int64 SourceID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SourceID"].ToString());

        string SourceName = item.OwnerTableView.DataKeyValues[item.ItemIndex]["SourceName"].ToString();
        try
        {
            FinishSourceWareDAL.Delete(WorkerCertificateCode, SourceID);
        }
        catch (Exception ex)
        {
            UIHelp.WriteErrorLog(Page, "删除公益培训学习记录失败！", ex);
            return;
        }
        UIHelp.WriteOperateLog(UserName, UserID, "删除公益培训学习记录", string.Format("课件名称：{0}。", SourceName));
       

        //BindFinishHisList();
    }
}