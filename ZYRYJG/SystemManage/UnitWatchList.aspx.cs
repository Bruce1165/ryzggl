using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using DataAccess;
using Model;
using Utility;
using System.Text;

namespace ZYRYJG.SystemManage
{
    public partial class UnitWatch : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillRadComboBoxENT_City();
                ButtonSearch_Click(sender, e);
            }
        }

        //枚举行政所属
        protected void FillRadComboBoxENT_City()
        {
            DataTable dt = CommonDAL.GetDataTable("select distinct [ENT_City] from [UnitWatch] order by [ENT_City]");
            RadComboBoxENT_City.DataSource = dt;
            RadComboBoxENT_City.DataTextField = "ENT_City";
            RadComboBoxENT_City.DataValueField = "ENT_City";
            RadComboBoxENT_City.DataBind();
            RadComboBoxENT_City.Items.Insert(0, new RadComboBoxItem("全部", ""));
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxUnitName.Text != "")//企业名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }     

            if (RadComboBoxENT_City.SelectedValue != "")// 行政所属
            {
                q.Add(string.Format(" ENT_City = '{0}'", RadComboBoxENT_City.SelectedValue));
            }
 
            //创建日期
            if (datePickerCJSJ.SelectedDate.HasValue)
            {
                q.Add(string.Format(" CJSJ='{0}'", datePickerCJSJ.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            //核查有效期
            if (datePickerValidEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("ValidEnd = '{0}'", datePickerValidEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            ViewState["QueryParamOB"] = q.ToWhereString();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;
        }

        //绑定新增或编辑弹出层
        protected void RadGridExamPlan_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                }
                else//update
                {
                    UnitWatchMDL ob = UnitWatchDAL.GetObject(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CreditCode"].ToString());

                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    UIHelp.SetData(editedItem, ob);
                    ViewState["UnitWatchMDL"] = ob;
                }
            }
        }

        protected void RadGridExamPlan_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadTextBox RadTextBoxUnitName = e.Item.FindControl("RadTextBoxUnitName") as RadTextBox;
            UnitWatchMDL ob = new UnitWatchMDL();       
            UIHelp.GetData(editedItem, ob);            
            try
            {
                UnitWatchDAL.Insert(ob);
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "添加重点核查企业设置失败！", ex);
                return;
            }
            ViewState["UnitWatchMDL"] = ob;
            UIHelp.WriteOperateLog(PersonName, UserID, "新增重点核查企业设置", string.Format("企业名称：{0}。", RadTextBoxUnitName.Text));
            UIHelp.layerAlert(Page, string.Format("成功重点核查企业设置“{0}”。", RadTextBoxUnitName.Text));
        }

        //修改
        protected void RadGridExamPlan_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
           
            UnitWatchMDL ob = ViewState["UnitWatchMDL"] as UnitWatchMDL;
            string oldCreditCode=ob.CreditCode;
            RadTextBox RadTextBoxCreditCode = e.Item.FindControl("RadTextBoxCreditCode") as RadTextBox;
            string newCreditCode=RadTextBoxCreditCode.Text.Trim();
            RadTextBox RadTextBoxUnitName = e.Item.FindControl("RadTextBoxUnitName") as RadTextBox;
     
            UIHelp.GetData(editedItem, ob);

            ob.newCreditCode = newCreditCode;
            ob.CreditCode = oldCreditCode;

            try
            {
                //计划主表
                UnitWatchDAL.Update(ob);               
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "修改重点核查企业设置失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "更新重点核查企业设置", string.Format("企业名称：{0}。", RadTextBoxUnitName.Text));
            UIHelp.layerAlert(Page, "修改重点核查企业设置成功！", 6, 3000);
        }

        //删除
        protected void RadGridExamPlan_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            string CreditCode = Convert.ToString(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CreditCode"]);     
                     
            try
            {
                UnitWatchDAL.Delete(CreditCode);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除重点核查企业设置失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除重点核查企业设置", string.Format("企业名称：{0}。",e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("UnitName").OrderIndex].Text));
            UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
        }

        //导入
        protected void ButtonImport_Click(object sender, EventArgs e)
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
                    ButtonSearch_Click(sender, e);

                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "建造师重点核查企业上传失败！", ex);
                throw ex;

            }
        }

        public void DeleteAdd(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            DateTime CreateTime = DateTime.Now;

            //删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["企业名称"].ToString().Trim() == ""
                && dt.Rows[m]["社会统一信用代码"].ToString().Trim() == ""
                && dt.Rows[m]["证书编号"].ToString().Trim() == ""
                && dt.Rows[m]["行政所属"].ToString().Trim() == ""
                && dt.Rows[m]["法定代表人"].ToString().Trim() == ""
                && dt.Rows[m]["法人联系电话"].ToString().Trim() == ""
                && dt.Rows[m]["注册地址"].ToString().Trim() == ""
                && dt.Rows[m]["核查有效期开始"].ToString().Trim() == "" 
                && dt.Rows[m]["核查有效期截止"].ToString().Trim() == ""               
                )
                {
                    dt.Rows.RemoveAt(m);
                }
            }           

            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

            //验证数据
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空

                ValidNull(dt, m, "企业名称", rowErr);
                ValidNull(dt, m, "社会统一信用代码", rowErr);
                ValidNull(dt, m, "行政所属", rowErr);
                ValidNull(dt, m, "核查有效期开始", rowErr);
                ValidNull(dt, m, "核查有效期截止", rowErr);

                if (dt.Rows[m]["核查有效期开始"].ToString() != "") ValidDataTime(dt, m, "核查有效期开始", rowErr);
                if (dt.Rows[m]["核查有效期截止"].ToString() != "") ValidDataTime(dt, m, "核查有效期截止", rowErr);
                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 3)).Append("】行：企业名称“").Append(dt.Rows[m]["企业名称"].ToString()).Append("”-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            CommonDAL.ExecSQL("truncate table [dbo].[UnitWatchIng];");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(i >0 && i % 499 ==0)
                {
                    sb.Remove(0, 6);
                    sb.Insert(0, @"INSERT INTO [dbo].[UnitWatchIng]([UnitName],[CreditCode],[UnitCertCode],[ENT_City],[FaRen],[FRPhone],[Address],[CJSJ],[ValidEnd]) ");
                    CommonDAL.ExecSQL(sb.ToString());
                    sb.Remove(0, sb.Length);
                }

               // sb.Append("'").Append(dt.Rows[i][7].ToString()).Append("'").Append(",");
                sb.Append(string.Format(
@" union select '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'
 ", dt.Rows[i]["企业名称"], dt.Rows[i]["社会统一信用代码"], dt.Rows[i]["证书编号"], dt.Rows[i]["行政所属"], dt.Rows[i]["法定代表人"], dt.Rows[i]["法人联系电话"], dt.Rows[i]["注册地址"], dt.Rows[i]["核查有效期开始"], dt.Rows[i]["核查有效期截止"]));

            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 6);

                sb.Insert(0, @"
                INSERT INTO [dbo].[UnitWatchIng]([UnitName],[CreditCode],[UnitCertCode],[ENT_City],[FaRen],[FRPhone],[Address],[CJSJ],[ValidEnd]) ");

                sb.Append(@"
                ;MERGE INTO dbo.UnitWatch t1 USING 
                (
                    SELECT distinct L.[UnitName]
                        ,L.[CreditCode]
                        ,L.[UnitCertCode]
                        ,L.[ENT_City]
                        ,L.[FaRen]
                        ,L.[FRPhone]
                        ,L.[Address]
                        ,L.[CJSJ]
                        ,L.[ValidEnd]
                    FROM DBO.[UnitWatchIng]  L       
                    inner join dbo.UnitWatch q on L.[CreditCode] =q.[CreditCode] 
                ) t2 ON t1.[CreditCode]=t2.[CreditCode] 
                WHEN MATCHED THEN UPDATE SET  t1.[UnitName]		 =t2.[UnitName]			
								            ,t1.[UnitCertCode]   =t2.[UnitCertCode] 
								            ,t1.[ENT_City]       =t2.[ENT_City]     
								            ,t1.[FaRen]          =t2.[FaRen]        
								            ,t1.[FRPhone]        =t2.[FRPhone]      
								            ,t1.[Address]        =t2.[Address]      
								            ,t1.[CJSJ]           =t2.[CJSJ]         
								            ,t1.[ValidEnd]       =t2.[ValidEnd]     ;

	            INSERT INTO [dbo].[UnitWatch]([UnitName],[CreditCode],[UnitCertCode],[ENT_City],[FaRen],[FRPhone],[Address],[CJSJ],[ValidEnd])
                SELECT  L.[UnitName],L.[CreditCode],L.[UnitCertCode],L.[ENT_City],L.[FaRen],L.[FRPhone],L.[Address],L.[CJSJ],L.[ValidEnd]
                FROM DBO.[UnitWatchIng]  L
                left join DBO.UnitWatch t2 on L.[CreditCode]=t2.[CreditCode]     
                where t2.[CreditCode] is null and L.[CreditCode] is not null;
                ");
            }
            
            try
            {
                
                 CommonDAL.ExecSQL(sb.ToString());
                 UIHelp.WriteOperateLog(UserName, UserID, "建造师重点核查企业导入成功", string.Format("共导入记录：{0}条", ds.Tables[0].Rows.Count));
                 UIHelp.layerAlert(Page, string.Format("建造师重点核查企业导入成功，共导入记录：{0}条。", ds.Tables[0].Rows.Count), 6, 0);
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "建造师重点核查企业导入失败！", ex);
            }
        }

        /// <summary>
        /// 验证数据是否为空值
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowIndex">行</param>
        /// <param name="colName">列名</param>
        /// <param name="rowErr">行校验结果</param>
        private void ValidNull(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") rowErr.Append("<br />“").Append(colName).Append("”不能为空值！");
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowIndex">行</param>
        /// <param name="colName">列名</param>
        /// <param name="rowErr">行校验结果</param>
        private void ValidDataTime(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (Utility.Check.IfDateOrDateTimeFormat(dt.Rows[rowIndex][colName].ToString()) == false) rowErr.Append("<br />“").Append(colName).Append("”必须是有效的日期格式（如2010-1-1）");
        }

        //删除查询结果
        protected void ButtonDelSelect_Click(object sender, EventArgs e)
        {
            CommonDAL.ExecSQL(string.Format("delete  FROM [dbo].[UnitWatch] where 1=1 {0}", ViewState["QueryParamOB"]));

            UIHelp.WriteOperateLog(UserName, UserID, "批量删除建造师重点核查企业成功", "");
            UIHelp.layerAlert(Page, "删除成功。", 6, 2000);
            ButtonSearch_Click(sender, e);
        }

        //废止查询结果到当日
        protected void ButtonSetEnd_Click(object sender, EventArgs e)
        {
            CommonDAL.ExecSQL(string.Format("update  [dbo].[UnitWatch] set [ValidEnd] = '{0}' where 1=1 {1}",DateTime.Now.ToString("yyyy-MM-dd"), ViewState["QueryParamOB"]));

            UIHelp.WriteOperateLog(UserName, UserID, "批量废止建造师重点核查企业成功", "");
            UIHelp.layerAlert(Page, "废止成功。", 6, 2000);
            ButtonSearch_Click(sender, e);
        }

    }
}
