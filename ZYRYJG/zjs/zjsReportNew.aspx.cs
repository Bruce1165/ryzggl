using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;


namespace ZYRYJG.zjs
{
    //汇总
    public partial class zjsReportNew : BasePage
    {
        //protected override string CheckVisiteRgihtUrl
        //{
        //    get
        //    {
        //        return "zjs/zjsReportSend.aspx";
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (string.IsNullOrEmpty(Request["o1"]) == false)//edit
                //{
                //    ViewState["ENT_City"]=Request["o1"];//区县
                //    ViewState["ReportCode"] = Request["o2"];//上报编码
                //     ViewState["ApplyType"]=Request["o3"];//申报类型
                //     ViewState["XiangXi"] = Request["o4"];//只读显示
                //    //修改时不能修改申报事项类型
                //    UIHelp.SelectDropDownListItemByValue(RadComboBoxIfContinue1,Request["o3"]);
                //    UIHelp.SetReadOnly(RadComboBoxIfContinue1,false);
                //}

                //ButtonSearch_Click(sender, e);
            }
        }
        
//        //查询
//        protected void ButtonSearch_Click(object sender, EventArgs e)
//        {
//            if (UIHelp.CheckSQLParam() == false)
//            {
//                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
//                return;
//            }
//            ObjectDataSource1.SelectParameters.Clear();
//            string type = RadTextBoxValue.Text.Trim();
//            var q = new QueryParamOB();
//            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)
//            {
//                q.Add(string.Format("ent_city like '{0}%'", Region));
//            }
//            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
//            {
//                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
//            }


//            //判断他是否详细
//            if (ViewState["ReportCode"] != null && ViewState["XiangXi"]!=null)//edit
//            {
//                //公示批次号
//                q.Add(string.Format("(ReportCode ='{0}')", ViewState["ReportCode"]));
//                //详细就把这两个控件隐藏掉
//                BtnSave.Visible = false;
//                tableSearch.Visible = false;
//            }
//            //判断他是否是修改
//            else  if (ViewState["ReportCode"] != null)//edit
//            {
//                //公示批次号
//                q.Add(string.Format("(ReportCode ='{0}' or (ApplyStatus = '{1}' and ReportCode is null)) AND ApplyType = '{2}'", ViewState["ReportCode"].ToString(), EnumManager.ZJSApplyStatus.已审核, ViewState["ApplyType"].ToString()));
//                tableSearch.Visible = false;
//            }
//            //判断他是否是新增
//            else//new
//            {
//                q.Add("ReportCode is null");
//                q.Add(string.Format("{0} ='{1}'"
//                    , (RadComboBoxIfContinue1.SelectedValue == "执业企业变更" || RadComboBoxIfContinue1.SelectedValue == "个人信息变更" || RadComboBoxIfContinue1.SelectedValue == "企业信息变更" ? "ApplyTypeSub" : "ApplyType")
//                    , RadComboBoxIfContinue1.SelectedValue));//申报类型
//                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已审核));
//            }
           
//            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
//            RadGridADDRY.CurrentPageIndex = 0;
//            RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
//        }

//        //保存
//        protected void BtnSave_Click(object sender, EventArgs e)
//        {
//            string selectApplyIDs = GetRadGridADDRYSelect();
//            if (selectApplyIDs == "")
//            {
//                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
//                return;
//            }
//            //汇总批次号：2位区县编号 + 8位年月日 + 注册类型 +3 位自然序号（000）
//            string ReportCode= ViewState["ReportCode"]!=null?ViewState["ReportCode"].ToString():zjs_ApplyDAL.GetNextReportCode(Region
//                , (RadComboBoxIfContinue1.SelectedValue == "执业企业变更" || RadComboBoxIfContinue1.SelectedValue == "个人信息变更" || RadComboBoxIfContinue1.SelectedValue == "企业信息变更" ? "变更注册" : RadComboBoxIfContinue1.SelectedValue)
//                );
            
//            DBHelper db = new DBHelper();
//            DbTransaction tran = db.BeginTransaction();
//            try
//            {              
//                if (ViewState["ReportCode"]!=null)//修改
//                {

//                    //取消已分配批次号
//                    zjs_ApplyDAL.UpdatePatchReport(tran, ReportCode);

//                    //重新分配批次号
//                    zjs_ApplyDAL.SavePatchReport(tran, ReportCode, selectApplyIDs);
//                    tran.Commit();
//                }
//                else//新增
//                {
//                    //分配批次号
//                    zjs_ApplyDAL.SavePatchReport(tran,ReportCode, selectApplyIDs);

//                    tran.Commit();

//                    ViewState["ENT_City"] = Region;//区县
//                    ViewState["ReportCode"] = ReportCode;//上报编码
//                    ViewState["ApplyType"] = RadComboBoxIfContinue1.SelectedValue;//申报类型                
                    
//                    //修改时不能修改申报事项类型
//                    UIHelp.SelectDropDownListItemByValue(RadComboBoxIfContinue1, Request["o3"]);
//                    UIHelp.SetReadOnly(RadComboBoxIfContinue1, false);
//                }
               
//                CreateExcel(ReportCode);               

//                ButtonSearch_Click(sender, e);
//                UIHelp.WriteOperateLog(UserName, UserID, "汇总造价工程师注册申请成功", string.Format("受理时间：{0}", DateTime.Now));
//                UIHelp.layerAlert(Page, "汇总成功！",6,2000);
//                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);

//            }
//            catch (Exception ex)
//            {
//                tran.Rollback();
//                UIHelp.WriteErrorLog(Page, "汇总造价工程师注册申请失败", ex);
//            }
//        }

//        /// <summary>
//        /// 根据汇总编码生成导出excel
//        /// </summary>
//        /// <param name="ReportCode">汇总编码</param>
//        protected void CreateExcel(string ReportCode)
//        {
//            #region 初始注册
            
           
//            if (ViewState["ApplyType"].ToString() == "初始注册")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\(select  '【' +ZGZSBH + '，' + ZYLB+ '，' +CONVERT(varchar(100), QFSJ, 23)+'】' from [Qualification] where ZJHM = zjs_Apply.PSN_CertificateNO and PATINDEX ( '%'+ZYLB+'%' , zjs_Apply.PSN_RegisteProfession ) >0 FOR XML PATH(''))\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""7"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""2"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""1"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""1"">部门公章</td>
//</tr>
//
//<tr>
//    <td class=""noborder""></td>
//    <td class=""noborder""></td>
//    <td class=""noborder""></td>
//    <td class=""noborder""></td> 
//    <td style=""width:320pt"" class=""noborder""></td>
//    <td class=""noborder""></td>
//    <td class=""noborder""></td>  
//</tr>";

//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\企业名称\执业资格证书编号/管理号_专业_签发时间\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }
//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "[dbo].[zjs_Apply]"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot

//                );
//            }
//            #endregion

//            #region 延续注册
//            if (ViewState["ApplyType"].ToString() == "延续注册")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNo\(select CONVERT(varchar(100),PSN_CertificateValidity, 23) from zjs_Certificate a where a.PSN_CertificateNO=zjs_Apply.PSN_CertificateNO and a.psn_registetype<'07')\PSN_RegisteProfession\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""3"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""2"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">部门公章</td>
//</tr>";
//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\企业名称\注册号\注册有效期\专业\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }

//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "zjs_Apply"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot
//                );

//            }

//            #endregion

//            #region 注销
//            if (ViewState["ApplyType"].ToString() == "注销")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNo\(select CONVERT(varchar(100),PSN_CertificateValidity, 23) from [zjs_Certificate] a where a.PSN_CertificateNO=zjs_Apply.PSN_CertificateNO and a.psn_registetype<'07')\PSN_RegisteProfession\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""3"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""2"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">部门公章</td>
//</tr>";
//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\企业名称\注册号\注册有效期\专业\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }

//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "zjs_Apply"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot
//                );

//            }
//            #endregion

//            #region 执业企业变更

//            if (ViewState["ApplyType"].ToString() == "执业企业变更")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNo\(select CONVERT(varchar(100),PSN_CertificateValidity, 23) from [zjs_Certificate] a where a.PSN_CertificateNO=zjs_Apply.PSN_CertificateNO and a.psn_registetype<'07')\PSN_RegisteProfession\(select OldENT_Name from zjs_ApplyChange b where b.ApplyID=zjs_Apply.ApplyID)\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""3"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""2"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">部门公章</td>
//</tr>";
//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\现聘用企业名称\注册号\注册有效期\专业\原聘用企业名称\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }
//                //if (ViewState["PSN_Level"] == null)
//                //{
//                //    ViewState["PSN_Level"] = "二级";
//                //}
//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "zjs_Apply"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot
//                );

//            }
//            #endregion

//            #region 个人信息变更

//            if (ViewState["ApplyType"].ToString() == "个人信息变更")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNo\(select CONVERT(varchar(100),PSN_CertificateValidity, 23) from [zjs_Certificate] a where a.PSN_CertificateNO=zjs_Apply.PSN_CertificateNO and a.psn_registetype<'07')\PSN_RegisteProfession\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""3"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""2"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">部门公章</td>
//</tr>";
//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\企业名称\注册号\注册有效期\专业\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }

//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "zjs_Apply"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot
//                );

//            }
//            #endregion

//            #region 企业信息变更

//            if (ViewState["ApplyType"].ToString() == "企业信息变更")
//            {
//                //数据表的列明
//                string column = "";

//                column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\(select ENT_NameTo from zjs_ApplyChange a where a.ApplyID=zjs_Apply.ApplyID)\PSN_RegisterNo\(select CONVERT(varchar(100),PSN_CertificateValidity, 23) from [zjs_Certificate] a where a.PSN_CertificateNO=zjs_Apply.PSN_CertificateNO and a.psn_registetype<'07')\PSN_RegisteProfession\ExamineResult\ExamineRemark";
//                string Caption = @"
//<tr style=""height:30pt"">
//    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">北京市二级造价工程师{0}初审意见汇总表</td>
//</tr>
//<tr>
//    <td style=""text-align:left;"" colspan=""3"" class=""noborder"">批次号：{1}</td>
//    <td style=""text-align:center;"" colspan=""2"" class=""noborder"">区县：{3}</td>
//    <td style=""text-align:right;"" class=""noborder"" colspan=""3"">创建日期：{2}</td>
//</tr>";

//                string Foot = @"
//<tr style=""height:30pt"">
//    <td style=""text-align:left;"" colspan=""2"" class=""noborder"">制表人（签字）：</td>
//    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">审核日期：</td>
//    <td style=""text-align:left;"" class=""noborder"" colspan=""2"">部门公章</td>
//</tr>";
//                //EXCEL表头明
//                string head = "";
//                head = @"序号\姓名\证件号码\企业名称\变更后的企业名称\注册号\注册有效期\专业\审核结果\审核意见";
//                //过滤条件
//                string filterSql = string.Format(" and ReportCode ='{0}'", ReportCode);
//                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
//                {
//                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
//                }

//                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", ReportCode);
//                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
//                    , "zjs_Apply"
//                    , filterSql
//                    , "ApplyID"
//                    , head.ToString()
//                    , column.ToString()
//                    , string.Format(Caption, ViewState["ApplyType"], ReportCode, DateTime.Now.ToString("yyyy-MM-dd"), ViewState["ENT_City"])
//                    , Foot
//                );
//            }
//            #endregion
//        }

//        /// <summary>
//        /// 获取grid勾选行ApplyID集合
//        /// </summary>
//        /// <returns></returns>
//        private string GetRadGridADDRYSelect()
//        {
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();

//            for (int i = 0; i < RadGridADDRY.MasterTableView.Items.Count; i++)
//            {
//                CheckBox CheckBox1 = RadGridADDRY.Items[i].FindControl("CheckBox1") as CheckBox;
//                if (CheckBox1.Checked)
//                {
//                    sb.Append(",'").Append(RadGridADDRY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
//                }
//            }
//            if (sb.Length > 0)
//            {
//                sb.Remove(0, 1);
//            }
//            return sb.ToString();
//        }

//        protected void RadGridADDRY_DataBound(object sender, EventArgs e)
//        {
//            if (RadGridADDRY.MasterTableView.DataKeyValues.Count>0 && RadGridADDRY.MasterTableView.DataKeyValues[0]["ReportDate"] != DBNull.Value)
//            {
//                step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
//                step_已上报.Attributes["class"] += " green";
//            }
//        }

    }
}