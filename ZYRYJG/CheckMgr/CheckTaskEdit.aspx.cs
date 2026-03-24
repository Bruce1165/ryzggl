using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Utility;
using DataAccess;
using Model;

namespace ZYRYJG.CheckMgr
{
    public partial class CheckTaskEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CheckTaskMgr.aspx";
            }
        }
        /// <summary>
        /// 监管任务批次号
        /// </summary>
        protected int? PatchCode
        {
            get
            {
                if (ViewState["CheckTaskMDL"] == null)
                    return null;
                else
                    return ((CheckTaskMDL)ViewState["CheckTaskMDL"]).PatchCode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    string _PatchCode = Utility.Cryptography.Decrypt(Request["o"]);
                    CheckTaskMDL o = CheckTaskDAL.GetObject(Convert.ToInt32(_PatchCode));
                    ViewState["CheckTaskMDL"] = o;

                    UIHelp.SelectDropDownListItemByValue(RadComboBoxCheckType, o.CheckType);
                    RadDatePickerLastReportTime.SelectedDate = o.LastReportTime;
                    //UIHelp.layerAlert(Page, o.ifPhoneNotice.ToString());
                    //return;
                    RadioButtonListifPhoneNotice.Items.FindByValue(o.ifPhoneNotice.ToString()).Selected = true;
                    RadioButtonListifTipNotice.Items.FindByValue(o.ifTipNotice.ToString()).Selected = true;
                    RadTextBoxPhoneNotice.Text = o.PhoneNotice;
                    RadTextBoxTipNotice.Text = o.TipNotice;
                    LabelCreateTime.Text = o.CreateTime.Value.ToString("yyyy-MM-dd");
                    LabelPublishiTime.Text = (o.PublishiTime.HasValue == true ? o.PublishiTime.Value.ToString("yyyy-MM-dd") : "未发布");
                }
                else
                {
                    RadTextBoxPhoneNotice.Text = string.Format("根据住房城乡建设部提供的数据显示，您存在多家社保或社保单位与注册单位不一致情况,涉嫌存在“挂证”等违法违规行为，请您于{0}前完成整改。逾期未整改将严肃处理。", DateTime.Now.AddDays(15).ToString("yyyy年M月d日"));

                    RadTextBoxTipNotice.Text = @"根据住房城乡建设部提供的数据资料显示，您的一本或多本证书（例如：一级注册建造师、二级注册建造师、注册监理工程师）注册所在单位与社保缴费单位、住房公积金缴存单位不一致。根据《住房城乡建设部办公厅、人力资源社会保障部办公厅关于开展工程建设领域专业技术人员违规“挂证”行为专项治理的通知》建办市函〔2024〕283号，请您于2024年10月31日前，在相应的注册管理系统自行整改，办理完成相关注册证书注销，监管部门将视情况不再追究相关责任。
即日起至2024年11月30日您须进入北京市住房和城乡建设领域人员资格管理信息系统-综合监管模块，查询须反馈的注册信息，按提示上传相关材料提交审核。逾期监管部门将对存在“挂证”等违法违规行为依法从严查处。";
                }
                BindRadGridTask();
            }
        }

        //导入
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (ViewState["CheckTaskMDL"] == null)
            {
                UIHelp.layerAlert(Page, "前先保存监管任务基本信息。");
                return;
            }
            else
            {
                if((ViewState["CheckTaskMDL"] as CheckTaskMDL).PublishiTime.HasValue ==true)
                {
                    UIHelp.layerAlert(Page, "监管任务基已发布，无法修改导入数据。");
                    return;
                }
            }
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/") + "/" + FileUpload1.FileName;
                    FileUpload1.SaveAs(filepath);
                    DataSet ds = ExcelDealHelp.ImportExcell(filepath, null);
                    //DeleteAdd(ds);
                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                    {
                        UIHelp.layerAlert(Page, "没有发现需要导入的有效数据");
                        return;
                    }
                    SaveTemp(ds.Tables[0]);
                    UIHelp.layerAlert(Page, string.Format("导入成功（共{0}条）", ds.Tables[0].Rows.Count));
                    UIHelp.WriteOperateLog(UserName, UserID, "导入监管任务详细记录", string.Format("批次号：{0}，共{1}条。", PatchCode, ds.Tables[0].Rows.Count));
                    BindRadGridTask();
                    ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "资格库管理上传失败！", ex);
                //throw ex;
            }
        }


        //保存导入数据
        protected void SaveTemp(DataTable daInsert)
        {
            //数据导入临时表
            string headList = "人员姓名,证件号码,证书编号,注册类别,注册单位,所属区,社保情况,社保单位,公积金情况,在施项目,数据发布时间,联系方式";
            string columnList = "WorkerName,WorkerCertificateCode,CertificateCode,PostTypeName,Unit,Country,SheBaoCase,ShebaoUnit,GongjijinCase,ProjectCase,SourceTime,phone";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString);
            conn.Open();
            try
            {
                //清楚临时表
                CommonDAL.ExecSQL("truncate table [CheckFeedBack_ing]");

                //导入临时表
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
                {
                    sqlBC.BatchSize = 1000;
                    sqlBC.BulkCopyTimeout = 60;
                    sqlBC.NotifyAfter = 10000;
                    sqlBC.DestinationTableName = "CheckFeedBack_ing";

                    for (int i = daInsert.Columns.Count - 1; i >= 0; i--)
                    {
                        if (headList.Contains(daInsert.Columns[i].ColumnName) == false)
                        {
                            daInsert.Columns.RemoveAt(i);
                        }
                    }
                    string[] headSet = headList.Split(',');
                    string[] columnSet = columnList.Split(',');
                    for (int i = 0; i < columnSet.Length; i++)
                    {
                        sqlBC.ColumnMappings.Add(headSet[i], columnSet[i]);
                    }
                    sqlBC.WriteToServer(daInsert);
                }


                //更新正式表
                CommonDAL.ExecSQL(string.Format(@"Delete from [dbo].[CheckFeedBack] where PatchCode={0};
                                                    INSERT INTO [dbo].[CheckFeedBack]([DataID],[PatchCode],[CheckType],[CreateTime],[CJR],[LastReportTime],[DataStatus],[DataStatusCode],[WorkerName],[WorkerCertificateCode],[CertificateCode],[PostTypeName],[Unit],[Country],[SheBaoCase],[ShebaoUnit],[GongjijinCase],[ProjectCase],[SourceTime],[phone],[sn])
                                                    SELECT  newid(),t.[PatchCode],t.[CheckType],t.[CreateTime],t.[CJR],t.[LastReportTime],'未发布',0,c.[WorkerName],c.[WorkerCertificateCode],c.[CertificateCode],c.[PostTypeName],c.[Unit],c.[Country],c.[SheBaoCase],c.[ShebaoUnit],c.[GongjijinCase],c.[ProjectCase],c.[SourceTime],c.[phone],row_number()  over(order by (select 0))
                    FROM dbo.CheckFeedBack_ing as c inner join [dbo].[CheckTask] as t on  t.PatchCode={0}", PatchCode));

                //更新在施锁定
                CommonDAL.ExecSQL(string.Format(@"update [dbo].[CheckFeedBack]
                  set [CheckFeedBack].[ProjectCase] = '【中标企业】：'+[jcsjk_RY_JZS_ZSSD].ZBQY +'【项目名称：】'+ [jcsjk_RY_JZS_ZSSD].XMMC
                  FROM [dbo].[CheckFeedBack]
                  inner join [dbo].[jcsjk_RY_JZS_ZSSD] on [CheckFeedBack].[CertificateCode] = [jcsjk_RY_JZS_ZSSD].ZCH
                  where [CheckFeedBack].[PatchCode]={0} and len([jcsjk_RY_JZS_ZSSD].ZBQY) >0", PatchCode));

                //更新社会统一信用代码
                CommonDAL.ExecSQL(string.Format(@"update [dbo].[CheckFeedBack]
                 set [CheckFeedBack].[UnitCode] =  [QY_GSDJXX].[UNI_SCID]
                  FROM [dbo].[CheckFeedBack]
                  inner join [dbo].[QY_GSDJXX] on [CheckFeedBack].[Unit] = [QY_GSDJXX].[ENT_NAME]
                  where [CheckFeedBack].[PatchCode]={0} ", PatchCode));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
            }
        }

        //刷新导入记录
        protected void BindRadGridTask()
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            q.Add(string.Format("PatchCode = {0}", PatchCode.HasValue == true ? PatchCode : 0));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridTask.CurrentPageIndex = 0;
            RadGridTask.DataSourceID = ObjectDataSource1.ID;

//            //区县字典
//            @"SELECT [RegionName]
//            FROM [dbo].[Dict_Region]
//            union all
//            SELECT [USERNAME]     
//            FROM [dbo].[USER]
//            where [ORGANID] =246 or [ORGANID]=247
//            union all
//            select '市住建委'";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //异常区县数据
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"
                select [Country],count(*) as DataCount
                FROM [dbo].[CheckFeedBack]
                where [PatchCode]={0} 
                and [Country] not in (
	                SELECT [RegionName]
	                FROM [dbo].[Dict_Region]
	                union
	                SELECT [USERNAME]     
	                FROM [dbo].[USER]
	                where [ORGANID] =246 or [ORGANID]=247
	                union
                    select '市住建委'
                    union
                    select '造价管理处'

                )
                group by [Country]", PatchCode.HasValue == true ? PatchCode : 0));

            if (dt != null && dt.Rows.Count > 0)           
            {
               
                foreach (DataRow r in dt.Rows)
                {
                    sb.AppendFormat("<div style='color:red'>{0}：{1}条</div>", r["Country"], r["DataCount"]);
                }
                if(sb.Length>0)
                {
                    sb.Insert(0, "<p  style='color:red'>导入数据存在非标准区县，需要按照标准库中区县进行修改，再重新导入。</p>");
                    sb.Append("<p  style='color:red'>区县标准库：东城区，西城区，崇文区，宣武区，朝阳区，丰台区，石景山区，海淀区，门头沟区，房山区，通州区，顺义区，昌平区，大兴区，怀柔区，平谷区，亦庄，密云区，延庆区，北京城建集团有限责任公司，北京建工集团有限责任公司，中国建筑，中国建筑一局（集团）有限公司，中国建筑第二工程局有限公司，中建大成，中国建筑土木建设有限公司，中国新兴建设开发总公司，中铁建电气化局，中铁十六局，中铁十九局，中铁二十二局，中国交通建设股份有限公司，中国京冶工程技术有限公司，中国中铁股份，中铁建设集团有限公司，注册中心临时用户，北京中关村开发建设集团，中国成套设备进出口（集团）总公司，中国铁路通信信号股份有限公司，中国化学工程集团公司，中建交通，中铁六局，中国有色金属建设股份有限公司，中国铁建股份有限公司，造价管理处，市住建委</p>");
                }               
            }

            //异常注册类型数据
            DataTable dtPostType = CommonDAL.GetDataTable(string.Format(@"
                select [PostTypeName],count(*) as DataCount
                FROM [dbo].[CheckFeedBack]
                where [PatchCode]={0} 
                and [PostTypeName] not in (
                    '一级注册建造师',
                    '二级注册建造师',
                    '一级造价工程师',
                    '二级造价工程师',
                    '注册监理工程师',
                    '安全生产考核三类人员'
                     )
                group by [PostTypeName]", PatchCode.HasValue == true ? PatchCode : 0));
            if (dtPostType != null && dtPostType.Rows.Count > 0)
            {
                sb.Append("<p  style='color:red'>导入数据存在非标准注册类别，需要按照标准库中注册类别进行修改，再重新导入。</p>");
                foreach (DataRow r in dtPostType.Rows)
                {
                    sb.AppendFormat("<div style='color:red'>{0}：{1}条</div>", r["PostTypeName"], r["DataCount"]);
                }
                sb.Append("<p  style='color:red'>注册类别标准库：一级注册建造师，二级注册建造师，一级造价工程师，二级造价工程师，注册监理工程师，安全生产考核三类人员。</p>");
            }

            if (sb.Length > 0)
            {
                tdImportError.InnerHtml = sb.ToString();
            }
            else
            {
                tdImportError.InnerText = "";
            }
        }

        //保存监管任务主表
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (RadComboBoxCheckType.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "监管类型不能为空。");
                return;
            }
            if (RadDatePickerLastReportTime.SelectedDate.HasValue == false)
            {
                UIHelp.layerAlert(Page, "上报截止日期不能为空。");
                return;
            }
            if (RadioButtonListifPhoneNotice.SelectedValue == "1" && RadTextBoxPhoneNotice.Text.Trim() == "")
            {
                UIHelp.layerAlert(Page, "选择短信通知，请输入短信内容。");
                return;
            }
            if (RadioButtonListifTipNotice.SelectedValue == "1" && RadTextBoxTipNotice.Text.Trim() == "")
            {
                UIHelp.layerAlert(Page, "选择弹框通知，请输入弹框内容。");
                return;
            }
            //UIHelp.layerAlert(Page, string.Format("{0}", Convert.ToBoolean(RadioButtonListifPhoneNotice.SelectedValue)));
            //return;
            try
            {
                CheckTaskMDL o = (ViewState["CheckTaskMDL"] == null ? new CheckTaskMDL() : (CheckTaskMDL)ViewState["CheckTaskMDL"]);
                o.CheckType = RadComboBoxCheckType.SelectedValue;
                o.LastReportTime = RadDatePickerLastReportTime.SelectedDate.Value;
                o.ifPhoneNotice = Convert.ToBoolean(RadioButtonListifPhoneNotice.SelectedValue);
                o.PhoneNotice = RadTextBoxPhoneNotice.Text.Trim();
                o.ifTipNotice = Convert.ToBoolean(RadioButtonListifTipNotice.SelectedValue);
                o.TipNotice = RadTextBoxTipNotice.Text.Trim();

                if (ViewState["CheckTaskMDL"] == null)
                {
                    object curPatchCode = CommonDAL.GetObject("select max([PatchCode]) from [dbo].[CheckTask] where year([CreateTime]) =year(getdate())");
                    int newPatchCode = ((curPatchCode == null || curPatchCode == DBNull.Value) ? Convert.ToInt32(string.Format("{0}0001", DateTime.Now.Year)) : Convert.ToInt32(curPatchCode) + 1);//批次号
                    o.PatchCode = newPatchCode;
                    o.CJR = UserName;
                    o.CreateTime = DateTime.Now;
                    CheckTaskDAL.Insert(o);
                }
                else
                {
                    CheckTaskDAL.Update(o);
                    CheckFeedBackDAL.Update(null,o);
                    
                }
                ViewState["CheckTaskMDL"] = o;
                BindRadGridTask();
                LabelCreateTime.Text = o.CreateTime.Value.ToString("yyyy-MM-dd");
                LabelPublishiTime.Text = (o.PublishiTime.HasValue == true ? o.PublishiTime.Value.ToString("yyyy-MM-dd") : "未发布");
                UIHelp.layerAlert(Page, "保存成功。");

                UIHelp.WriteOperateLog(UserName, UserID, "保存监管任务", string.Format("批次号：{0}。", o.PatchCode));
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存监管任务失败！", ex);
            }


        }

        //发布
        protected void ButtonPublish_Click(object sender, EventArgs e)
        {
            if (ViewState["CheckTaskMDL"] == null)
            {
                UIHelp.layerAlert(Page, "尚未保存信息。请先保存，再发布。");
                return;
            }
            if(tdImportError.InnerText !="")
            {
                UIHelp.layerAlert(Page, "导入数据中存在错误。请先修正，再发布。");
                return;
            }
            try
            {
                
                CheckTaskMDL o =  (CheckTaskMDL)ViewState["CheckTaskMDL"];
                o.PublishiTime = DateTime.Now;
                o.CheckType = RadComboBoxCheckType.SelectedValue;
                o.LastReportTime = RadDatePickerLastReportTime.SelectedDate.Value;
                o.ifPhoneNotice = Convert.ToBoolean(RadioButtonListifPhoneNotice.SelectedValue);
                o.PhoneNotice = RadTextBoxPhoneNotice.Text.Trim();
                o.ifTipNotice = Convert.ToBoolean(RadioButtonListifTipNotice.SelectedValue);
                o.TipNotice = RadTextBoxTipNotice.Text.Trim();

               
                CheckTaskDAL.Update(o);
                CheckFeedBackDAL.Update(null, o);
               
                ViewState["CheckTaskMDL"] = o;
                BindRadGridTask();
                LabelCreateTime.Text = o.CreateTime.Value.ToString("yyyy-MM-dd");
                LabelPublishiTime.Text = (o.PublishiTime.HasValue == true ? o.PublishiTime.Value.ToString("yyyy-MM-dd") : "未发布");
                UIHelp.layerAlert(Page, "发布成功。");
                UIHelp.WriteOperateLog(UserName, UserID, "发布监管任务", string.Format("批次号：{0}。", o.PatchCode));
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "发布监管任务失败！", ex);
            }
        }
    }
}