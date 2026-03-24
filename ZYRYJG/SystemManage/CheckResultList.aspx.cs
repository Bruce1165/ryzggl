using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DataAccess;
using Model;
using System.Data.SqlClient;

namespace ZYRYJG.SystemManage
{
    //资格校验
    public partial class CheckResultList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!this.IsPostBack)
           {
               if (IfExistRoleID("1") == true)//管理员可以导入数据
               {
                   linkTemplateCFZC.Visible = true;
                   linkTemplateSheBao.Visible = true;
                   ButtonImportShebao.Visible = true;
                   ButtonAdd.Visible = true;
                   FileUpload1.Visible = true;
                   FileUploadShebao.Visible = true;
               }
               if(IfExistRoleID("2") == true)//企业
               {
                   //集团属性
                   DataTable dt = CommonDAL.GetDataTable(string.Format("select * from [Dict_JTQY] where [QYMC] = '{0}'", UserName));

                   if (dt != null && dt.Rows.Count > 0)
                   {
                       //获取管理的子企业
                       DataTable dt_sub = CommonDAL.GetDataTable(string.Format(@"
                   select  [ZZJGDM],[QYMC] from [dbo].[jcsjk_HYLSGX] where [Valid]=1 and [HYLZGX]='{0}'", dt.Rows[0]["HYLZGX"]));
                       if (dt_sub != null && dt_sub.Rows.Count > 0)
                       {
                           ViewState["dt_sub"] = dt_sub;
                       }
                   }
               }
               if(IfExistRoleID("1") == true||IfExistRoleID("4") == true||IfExistRoleID("6") == true)
               {
                    trClass.Visible = true;//注册中心可分地区、企业类型查询导出数据
                    trtrClassCFZC.Visible = true;//注册中心可分地区、企业类型查询导出数据
               }
               ButtonQuerySheBao_Click(sender, e);
               ButtonQuery_Click(sender, e);
           }
        }

        //导入重复注册名单
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/") + "/" + FileUpload1.FileName;
                    FileUpload1.SaveAs(filepath);
                    DataSet ds = ExcelDealHelp.ImportExcell(filepath, null);
                    DeleteAdd(ds);
                    ButtonQuery_Click(sender, e);

                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "重复注册名单上传失败！", ex);
                throw ex;
                
            }
            

        }

        //处理重复注册导入数据
        public void DeleteAdd(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            DateTime CreateTime = DateTime.Now;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // sb.Append("'").Append(dt.Rows[i][7].ToString()).Append("'").Append(",");
                sb.Append(string.Format(
@" union select newid(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'
 ", dt.Rows[i]["姓名"], dt.Rows[i]["所属省份"], dt.Rows[i]["证件号码"], dt.Rows[i]["注册单位"], dt.Rows[i]["注册类型"], dt.Rows[i]["注册证书编号"], dt.Rows[i]["数据发布时间"], dt.Rows[i]["注册有效期"], dt.Rows[i]["性别"], dt.Rows[i]["出生年月"], dt.Rows[i]["备注"], dt.Rows[i]["问题说明"], CreateTime.ToString("yyyy-MM-dd")));

            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 6);

                sb.Insert(0, @"
                truncate table [dbo].[Out_CheckCFZC];
                INSERT INTO [dbo].[Out_CheckCFZC]
                   ([ID]
                   ,[UserName]
                   ,[Provice]
                   ,[IDCard]
                   ,[UnitName]
                   ,[RegType]
                   ,[CertCode]
                   ,[PublishDate]
                   ,[ValidEnd]
                   ,[Sex]
                   ,[Birthday]
                   ,[Remark]
                   ,[Question]
                   ,[CreateDate])
                    ");

                sb.Append(@"
                ;update [dbo].[Out_CheckCFZC] 
                set [Region] =u.[ENT_City]
                FROM [dbo].[Out_CheckCFZC] c
                inner join [dbo].[Unit] u
                on replace(replace(c.[UnitName],'（','('),'）',')') = replace(replace(u.ENT_Name,'（','('),'）',')');
                --
                update [dbo].[Out_CheckCFZC] 
                set UnitType =0
                FROM [dbo].[Out_CheckCFZC] a
                inner join (
	                select  distinct [QYMC] from [dbo].[jcsjk_HYLSGX] 
	                where [Valid]=1 and [HYLZGX] in(SELECT [HYLZGX]  FROM [dbo].[Dict_JTQY])
                ) b
                on replace(replace(a.[UnitName],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                where UnitType is null;                   
                --
                update b
                set b.UnitType =0
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[Out_CheckCFZC] b
                on  a.[UnitType] =0 and b.[UnitType] is null and 
                  ( a.[IDCard] = b.[IDCard] 
                  or substring(a.[IDCard],1,6) + substring(a.[IDCard],9,9) = b.[IDCard]
                   or substring(b.[IDCard],1,6) + substring(b.[IDCard],9,9) = a.[IDCard]);
                --
                update [dbo].[Out_CheckCFZC] 
                set UnitType =3
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[jcsjk_QY_JBXX_ZJZXQY] b
                on replace(replace(a.[UnitName],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                where UnitType is null;
                --
                update b
                set b.UnitType =3
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[Out_CheckCFZC] b
                on  a.[UnitType] =3 and b.[UnitType] is null and 
                  ( a.[IDCard] = b.[IDCard] 
                  or substring(a.[IDCard],1,6) + substring(a.[IDCard],9,9) = b.[IDCard]
                   or substring(b.[IDCard],1,6) + substring(b.[IDCard],9,9) = a.[IDCard]);
				--
                update [dbo].[Out_CheckCFZC] 
                set UnitType =1
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[jcsjk_QY_JBXX] b
                on replace(replace(a.[UnitName],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                where UnitType is null;   				               
                --
                update b
                set b.UnitType =1
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[Out_CheckCFZC] b
                on  a.[UnitType] =1 and b.[UnitType] is null and 
                  ( a.[IDCard] = b.[IDCard] 
                  or substring(a.[IDCard],1,6) + substring(a.[IDCard],9,9) = b.[IDCard]
                   or substring(b.[IDCard],1,6) + substring(b.[IDCard],9,9) = a.[IDCard]);
                --
                update [dbo].[Out_CheckCFZC] 
                set UnitType =2
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[jcsjk_QY_JBXX_GCJLQY] b
                on replace(replace(a.[UnitName],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                where UnitType is null;                
                --
                update b
                set b.UnitType =2
                FROM [dbo].[Out_CheckCFZC] a
                inner join [dbo].[Out_CheckCFZC] b
                on  a.[UnitType] =2 and b.[UnitType] is null and 
                  ( a.[IDCard] = b.[IDCard] 
                  or substring(a.[IDCard],1,6) + substring(a.[IDCard],9,9) = b.[IDCard]
                   or substring(b.[IDCard],1,6) + substring(b.[IDCard],9,9) = a.[IDCard]);
                ");
            }
            
            try
            {
                
                 CommonDAL.ExecSQL(sb.ToString());
                 UIHelp.WriteOperateLog(UserName, UserID, "重复注册名单导入成功", string.Format("共导入记录：{0}条", ds.Tables[0].Rows.Count));
                 UIHelp.layerAlert(Page, "重复注册名单导入成功！", 6, 2000);
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "重复注册名单导入失败！", ex);
            }
        }

        //查询重复注册
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadComboBoxItem.SelectedValue != null && RadTextBoxZJHM.Text.Trim() != "")
            {
                q.Add(string.Format("{1} = '{0}'", RadTextBoxZJHM.Text.Trim(), RadComboBoxItem.SelectedValue));
            }

            if (IfExistRoleID("0"))//个人
            {
                trWorkerHide.Visible = false;
                q.Add(string.Format("IDCard = '{0}'", WorkerCertificateCode));
            }
            else if (IfExistRoleID("2"))//企业
            {
                if (ViewState["dt_sub"] == null)//非集团企业
                {
                    q.Add(string.Format(@"replace(replace([UnitName],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')')", UserName));
                }
                else//集团企业
                {

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow r in ((DataTable)ViewState["dt_sub"]).Rows)
                    {
                        sb.Append(string.Format(@" or replace(replace([UnitName],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')')", r["QYMC"]));
                    }
                    q.Add(string.Format(@"(replace(replace([UnitName],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') {1})", UserName, sb));
                }
            }
            else if (IfExistRoleID("3") || IfExistRoleID("7"))//区县
            {

                q.Add(string.Format(@"[Region] like '{0}%'", Region));
            }

            if (trtrClassCFZC.Visible == true)//注册中心可分地区、企业类型查询导出数据
            {
                if (RadComboBoxJT_CFZC.SelectedValue != "")//集团
                {
                    //获取管理的子企业
                    DataTable dt_sub = CommonDAL.GetDataTable(string.Format(@"
                   select  [ZZJGDM],[QYMC] from [dbo].[jcsjk_HYLSGX] where [Valid]=1 and [HYLZGX]='{0}'", RadComboBoxJT_CFZC.SelectedItem.Text));

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow r in dt_sub.Rows)
                    {
                        sb.Append(string.Format(" or  replace(replace([UnitName],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') ", r["QYMC"]));
                    }

                    q.Add(string.Format(@"( replace(replace([UnitName],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') {1})", RadComboBoxJT_CFZC.SelectedValue, sb));

                }

                if (RadComboBoxQX_CFZC.SelectedValue != "")//区县
                {
                    q.Add(string.Format(@"[Region] like '{0}%' and [UnitType]=1", RadComboBoxQX_CFZC.SelectedValue));
                }
                if (RadComboBoxQT_CFZC.SelectedValue != "")//其他
                {
                    switch (RadComboBoxQT_CFZC.SelectedValue)
                    {
                        case "监理企业":
                            q.Add("[UnitType]=2");
                            break;
                        case "造价企业":
                            q.Add("[UnitType]=3");
                            break;
                        case "其他":
                            q.Add(@"
                                [UnitName] in
                                (
                                    select distinct b.UnitName
	                                FROM [dbo].[Out_CheckCFZC] b
	                                left join	[dbo].[Out_CheckCFZC] a
	                                on a.[UnitType]>-1 and b.[UnitType] is null and  a.[IDCard] = b.[IDCard]
	                                where a.ID is null  and b.[UnitType] is null
                                ) and [UnitType] is null");
                            break;
                    }
                }
            }
            ViewState["whereCFZC"] = q.ToWhereString();

            RefreshGridCFZC(0);
            //ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //RadGridCFZC.CurrentPageIndex = 0;
            //RadGridCFZC.DataSourceID = ObjectDataSource1.ID;


        }

        protected void RadGridCFZC_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RefreshGridCFZC(e.NewPageIndex);
        }

        protected void RefreshGridCFZC(int pageIndex)
        {
            string table = string.Format(@"
            (
                SELECT a.* FROM dbo.Out_CheckCFZC a inner join
                (
                    select distinct [IDCard] from [dbo].[Out_CheckCFZC] 
                    where 1=1 {0}     
                    union
                    select distinct substring([IDCard],1,6) + substring([IDCard],9,9) from [dbo].[Out_CheckCFZC]  
                    where 1=1 {0}                   
                ) b on a.[IDCard] = b.[IDCard]
            ) t", ViewState["whereCFZC"]);

            try
            {
                int rowCount = CommonDAL.SelectRowCount(table, "");
                DataTable dt = CommonDAL.GetDataTable(pageIndex * RadGridCFZC.PageSize, RadGridCFZC.PageSize, table, "*", "", "IDCard,ID");


                RadGridCFZC.DataSource = dt;
                RadGridCFZC.VirtualItemCount = rowCount;
                RadGridCFZC.CurrentPageIndex = pageIndex;
                RadGridCFZC.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "查询重复注册失败！", ex);
            }
        }
        //切换Tab
        protected void ButtonCheckSheBao_Click(object sender, EventArgs e)
        {
            ButtonCheckSheBao.CssClass = "Tab50 TabCur";
            ButtonCheckCFZC.CssClass = "Tab50";
            div_CheckSheBao.Visible = true;
            div_CheckCFZC.Visible = false;
        }

        //切换Tab
        protected void ButtonCheckCFZC_Click(object sender, EventArgs e)
        {
            ButtonCheckCFZC.CssClass = "Tab50 TabCur";
            ButtonCheckSheBao.CssClass = "Tab50";
            div_CheckSheBao.Visible = false;
            div_CheckCFZC.Visible = true;
        }

        //查询社保存疑
        protected void ButtonQuerySheBao_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //ObjectDataSource2.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadComboBoxSheBao.SelectedValue != null && RadTextBoxSheBao.Text.Trim() != "")
            {
                //if (RadComboBoxSheBao.SelectedValue == "Unit")//企业
                //{
                //    q.Add(string.Format("{1} = '{0}'", RadTextBoxSheBao.Text.Trim(), RadComboBoxSheBao.SelectedValue));
                //}
                q.Add(string.Format("{1} = '{0}'", RadTextBoxSheBao.Text.Trim(), RadComboBoxSheBao.SelectedValue));
            }
            if (IfExistRoleID("0"))//个人
            {
                tr_WorkHide2.Visible = false;
                q.Add(string.Format("IDCard18 = '{0}'", WorkerCertificateCode));
            }
            else if (IfExistRoleID("2"))//企业
            {
                if (ViewState["dt_sub"] == null)//非集团企业看本企业问题数据
                {
                    q.Add(string.Format(@"(replace(replace([Unit],'（','('),'）',')') = replace(replace('{0}','（','('),'）',')')  )", UserName));
                }
                else//集团企业可看包含隶属旗下企业问题数据
                {                    
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow r in ((DataTable)ViewState["dt_sub"]).Rows)
                    {
                        sb.Append(string.Format(" or  replace(replace([Unit],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') ", r["QYMC"]));
                    }

                    q.Add(string.Format(@"( replace(replace([Unit],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') {1})", UserName, sb));
                }
            }
            else if (IfExistRoleID("3") || IfExistRoleID("7"))//区县看到本区县问题数据,只看施工企业数据
            {
                q.Add(string.Format(@"[Region] like '{0}%' and [UnitType]=1", Region));
            }
            
            if (trClass.Visible == true)//注册中心可分地区、企业类型查询导出数据
            {
                if(RadComboBoxJT.SelectedValue !="")//集团
                {
                    //获取管理的子企业
                    DataTable dt_sub = CommonDAL.GetDataTable(string.Format(@"
                   select  [ZZJGDM],[QYMC] from [dbo].[jcsjk_HYLSGX] where [Valid]=1 and [HYLZGX]='{0}'", RadComboBoxJT.SelectedItem.Text));

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow r in dt_sub.Rows)
                    {
                        sb.Append(string.Format(" or  replace(replace([Unit],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') ", r["QYMC"]));
                    }

                    q.Add(string.Format(@"( replace(replace([Unit],'（','('),'）',')') =  replace(replace('{0}','（','('),'）',')') {1})", RadComboBoxJT.SelectedValue, sb));

                }

                if (RadComboBoxQX.SelectedValue != "")//区县
                {
                    q.Add(string.Format(@"[Region] like '{0}%' and [UnitType]=1", RadComboBoxQX.SelectedValue));
                }
                if (RadComboBoxQT.SelectedValue != "")//其他
                {
                    switch(RadComboBoxQT.SelectedValue)
                    {
                        case "监理企业":
                            q.Add("[UnitType]=2");
                            break;
                        case "造价企业":
                            q.Add("[UnitType]=3");
                            break;
                        case "其他":
                            q.Add("([UnitType] is null or ([UnitType]=1 and ([Region] is null or [Region]=null)))");
                            break;
                    }
                }
            }
            //q.Add("[Remark] like '%注册异常%'");
           
           

            //q.Add("[Remark] like '%注册异常%'");//只有标注“注册异常”才展示
            ViewState["whereSheBao"] = q.ToWhereString();
            RefreshGridSheBao(0);
            //ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //RadGridSheBao.CurrentPageIndex = 0;
            //RadGridSheBao.DataSourceID = ObjectDataSource2.ID;
        }


        protected void RadGridSheBao_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RefreshGridSheBao(e.NewPageIndex);
        }
        protected void RefreshGridSheBao(int pageIndex)
        {
            try
            {
                int rowCount = CommonDAL.SelectRowCount("Out_CheckShebao", ViewState["whereSheBao"].ToString());
                DataTable dt = CommonDAL.GetDataTable(pageIndex * RadGridSheBao.PageSize, RadGridSheBao.PageSize, "Out_CheckShebao", "*", ViewState["whereSheBao"].ToString(), "IDCard18,ID");
                RadGridSheBao.DataSource = dt;
                RadGridSheBao.VirtualItemCount = rowCount;
                RadGridSheBao.CurrentPageIndex = pageIndex;
                RadGridSheBao.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "查询社保存疑信息失败！", ex);
            }
        }

        //导入社保存疑名单
        protected void ButtonImportShebao_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadShebao.HasFile)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/") + "/" + FileUploadShebao.FileName;
                    FileUploadShebao.SaveAs(filepath);
                    DataSet ds = ExcelDealHelp.ImportExcell(filepath, null);
                    DeleteAddSheBao(ds);
                    ButtonQuerySheBao_Click(sender, e);

                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "社保存疑名单上传失败！", ex);
                throw ex;

            }
        }

        //处理社保存疑名单
        public void DeleteAddSheBao(DataSet ds)
        {
             CommonDAL.ExecSQL("truncate table [dbo].[Out_CheckShebao_ing]");

            DataTable daInsert = ds.Tables[0];
            string columnListFrom = "姓名,证件号码,注册类型,注册单位,所属省份,社保单位,数据发布时间,问题说明,性别,出生年月,备注,升位成18位身份证号,注册号,归属地";
            string columnListTo = "UserName,IDCard,RegType,Unit,Provice,ShebaoUnit,PublishDateTime,Question,Sex,Birthday,Remark,IDCard18,RegCode,Region";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString);
            conn.Open();
            try
            {

                using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
                {
                    sqlBC.BatchSize = 1000;
                    sqlBC.BulkCopyTimeout = 60;
                    sqlBC.NotifyAfter = 10000;
                    sqlBC.DestinationTableName = "Out_CheckShebao_ing";

                    string[] from = columnListFrom.Split(',');
                    string[] to = columnListTo.Split(',');
                    for (int i=0; i < from.Length; i++)
                    {
                        sqlBC.ColumnMappings.Add(from[i], to[i]);
                    }

                    sqlBC.WriteToServer(daInsert);

                    CommonDAL.ExecSQL(@"
                    update [Out_CheckShebao_ing] set [ShebaoUnit]='' where [ShebaoUnit] is null;
                    --
                    truncate table [dbo].[Out_CheckShebao];
                    --
                    INSERT INTO [dbo].[Out_CheckShebao]
                       ([ID]
                       ,[UserName]
                       ,[IDCard]
                       ,[RegType]
                       ,[Unit]
                       ,[Provice]
                       ,[ShebaoUnit]
                       ,[PublishDateTime]
                       ,[Question]
                       ,[Sex]
                       ,[Birthday]
                       ,[Remark]
                       ,[IDCard18]
                       ,[RegCode]
                       ,[RegProfession]
                       ,[Region]
                       ,[Createdate])
                    SELECT 
	                newid()
	                ,[UserName]
                      ,[IDCard]
                      ,[RegType]
                      ,[Unit]
                      ,[Provice]
                      ,[ShebaoUnit]
                      ,[PublishDateTime]
                      ,[Question]
                      ,[Sex]
                      ,[Birthday]
                      ,[Remark]
                      ,[IDCard18]
                      ,[RegCode]
                      ,[RegProfession]
                      ,[Region]
	                  ,getdate()
                    FROM [dbo].[Out_CheckShebao_ing];
                    --
                    update [dbo].[Out_CheckShebao] 
                    set [Region] =u.[ENT_City]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[Unit] u
                    on replace(replace(c.Unit,'（','('),'）',')') = replace(replace(u.ENT_Name,'（','('),'）',')');
                    --
                    update [dbo].[Out_CheckShebao] 
                    set UnitType =0
                    FROM [dbo].[Out_CheckShebao] a
                    inner join (
						select  distinct [QYMC] from [dbo].[jcsjk_HYLSGX] 
						where [Valid]=1 and [HYLZGX] in(SELECT [HYLZGX]  FROM [dbo].[Dict_JTQY])
					) b
                    on replace(replace(a.[Unit],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                    where UnitType is null;
					 --
                    update [dbo].[Out_CheckShebao] 
                    set UnitType =3
                    FROM [dbo].[Out_CheckShebao] a
                    inner join [dbo].[jcsjk_QY_JBXX_ZJZXQY] b
                    on replace(replace(a.[Unit],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                    where UnitType is null;
                    --
                    update [dbo].[Out_CheckShebao] 
                    set UnitType =1
                    FROM [dbo].[Out_CheckShebao] a
                    inner join [dbo].[jcsjk_QY_JBXX] b
                    on replace(replace(a.[Unit],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                    where UnitType is null;
                    --
                    update [dbo].[Out_CheckShebao] 
                    set UnitType =2
                    FROM [dbo].[Out_CheckShebao] a
                    inner join [dbo].[jcsjk_QY_JBXX_GCJLQY] b
                    on replace(replace(a.[Unit],'（','('),'）',')') = replace(replace(b.QYMC,'（','('),'）',')')
                    where UnitType is null;
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='一级注册建造师' and  u.PSN_Level='一级' and c.IDCard18= u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='一级注册建造师' and  u.PSN_Level='一级' and len(u.[PSN_CertificateNO])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[PSN_CertificateNO];  
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='一级临时建造师' and  u.PSN_Level='一级临时' and c.IDCard18= u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='一级临时建造师' and  u.PSN_Level='一级临时' and len(u.[PSN_CertificateNO])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[PSN_CertificateNO];  
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='二级注册建造师' and  u.PSN_Level='二级' and c.IDCard18= u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='二级注册建造师' and  u.PSN_Level='二级' and len(u.[PSN_CertificateNO])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='二级临时建造师' and  u.PSN_Level='二级临时' and c.IDCard18= u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[PSN_RegisterNO],RegProfession =u.[PSN_RegisteProfession]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jzs] u
                    on  c.RegType ='二级临时建造师' and  u.PSN_Level='二级临时' and len(u.[PSN_CertificateNO])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[PSN_CertificateNO]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[注册号],RegProfession =replace([注册专业1] +'，'+ [注册专业2],'，无','')
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jls] u
                    on  c.RegType ='监理工程师'  and c.IDCard18= u.[证件号]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[注册号],RegProfession =replace([注册专业1] +'，'+ [注册专业2],'，无','')
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_jls] u
                    on  c.RegType ='监理工程师' and len(u.[证件号])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[证件号]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[ZCZH]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_zjs] u
                    on  c.RegType ='造价工程师'  and c.IDCard18= u.[SFZH]; 
                    --
                    update [dbo].[Out_CheckShebao] 
                    set RegCode =u.[ZCZH]
                    FROM [dbo].[Out_CheckShebao] c
                    inner join [dbo].[jcsjk_zjs] u
                    on  c.RegType ='造价工程师' and len(u.[SFZH])=15
                    and substring( c.IDCard18,1,6) + substring( c.IDCard18,9,9)=u.[SFZH];  
            ");
                    ;

                    UIHelp.WriteOperateLog(UserName, UserID, "社保存疑名单导入成功", string.Format("共导入记录：{0}条", daInsert.Rows.Count));
                    UIHelp.layerAlert(Page, "社保存疑名单导入成功！", 6, 2000);
                }
                
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "社保扫描结果导入失败！", ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
            }
        }

        //导出社保存疑       
        protected void ButtonButtonExportSheBao_Click(object sender, EventArgs e)
        {
            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\出生年月\证件号码\注册单位\所属省份\归属地\注册类型\注册号\注册专业\社保单位\问题说明";
                //数据表的列明
                string column = @"UserName\Sex\Birthday\IDCard18\Unit\Provice\Region\RegType\RegCode\RegProfession\ShebaoUnit\Question";
                //过滤条件
                string table = string.Format(@"
                (
                    SELECT * FROM dbo.Out_CheckShebao where 1=1 {0}   
                ) t", ViewState["whereSheBao"]);

                if (RadComboBoxQT.SelectedValue == "其他")
                {
                    //EXCEL表头明
                    head = @"姓名\性别\出生年月\证件号码\注册单位\所属省份\归属地\注册类型\注册号\注册专业\社保单位\问题说明\企业资质类型";
                    //数据表的列明
                    column = @"UserName\Sex\Birthday\IDCard18\Unit\Provice\Region\RegType\RegCode\RegProfession\ShebaoUnit\Question\QYZZLX";
                    //过滤条件
                    table = string.Format(@"
                    (
                        select  t.*,j.[QYZZLX] from 
                        (select qymc,max([QYZZLX]) as QYZZLX from [dbo].[jcsjk_QY_ZHXX] group by qymc)j
                        right join 
                        (
                            SELECT * FROM dbo.Out_CheckShebao where 1=1 {0}
                        ) t
                        on j.qymc = t.unit
                    ) w", ViewState["whereSheBao"]);
                }

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , table
                    , "", "IDCard18,ID", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutputSheBao.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span>"
                    ,UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                    , "数据准备完毕，点我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "社保存疑名单导出EXCEL失败！", ex);
            }
        }

        //导出重复注册
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\出生年月\证件号码\工作单位\所属省份\注册类型\注册证书编号\发证日期\注册有效期\发证单位\备注\部反馈意见存疑";
                //数据表的列明
                string column = @"UserName\Sex\Birthday\IDCard\UnitName\Provice\RegType\CertCode\PublishDate\ValidEnd\PublishBy\Remark\Question";

                string table = string.Format(@"
                (
                    SELECT a.* FROM dbo.Out_CheckCFZC a inner join
                    (
                        select [IDCard] from [dbo].[Out_CheckCFZC] 
                        where 1=1 {0}     
                        union
                        select substring([IDCard],1,6) + substring([IDCard],9,9) from [dbo].[Out_CheckCFZC]  
                        where 1=1 {0}                   
                    ) b on a.[IDCard] = b.[IDCard]
                ) t", ViewState["whereCFZC"]);

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , table, "", "IDCard,ID", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span>"
                    , UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                    , "数据准备完毕，点我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "重复注册名单导出EXCEL失败！", ex);
            }
        }


       
    }
}