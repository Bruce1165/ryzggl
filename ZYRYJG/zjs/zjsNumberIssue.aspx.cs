using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;

namespace ZYRYJG.zjs
{
    public partial class zjsNumberIssue : BasePage
    {
        /// <summary>
        /// 注册类型遗失补办
        /// </summary>
        public string applytype
        {
            get { return ViewState["applytype"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
               
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    QueryParamOB q = new QueryParamOB();
                    q.Add(string.Format("NoticeCode='{0}'", Request["o"]));
                    q.Add("ConfirmResult='通过'");
                    q.Add("(([CodeDate] is not null and [PSN_RegisterNo] is not null) or ([CodeDate] is null and [PSN_RegisterNo] is null))");//未编号或已编号（不包含存在原注册编号重新注册的）
                    DataTable dt = null;
                    switch (Request["t"])
                    {
                        case "初始注册":
                            ViewState["applytype"] = "初始注册";                           
                            dt = zjs_ApplyFirstDAL.GetListView(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                            if (dt.Rows.Count == 0)
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                                UIHelp.layerAlert(Page, "没有查到需要编号的数据。");
                            }
                            else if(dt.Rows[0]["CodeDate"] != DBNull.Value && dt.Rows[0]["CodeDate"].ToString() != "")//查询是否已经放号如果放号将发放编号和保存按钮置灰
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                            }
                            else
                            {
                                RadNumericTextBoxStartRegionNo.Value = zjs_ApplyDAL.GetNextPSN_RegisterNO();
                                ButtonCreateCode.Enabled = true;
                                ButtonSave.Enabled = true;
                            }
                            ButtonCreateCode.CssClass = ButtonCreateCode.Enabled == true ? "bt_large" : "bt_large btn_no";
                            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                            RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID", "ConferDate" ,"ApplyRegisteProfession"};//添加资格证书发证日期
                            break;                      
                    }
                    RadGridRYXX.DataSource = dt;
                    RadGridRYXX.DataBind();                    
                }
            }
        }

        //放号
        protected void ButtonCreateCode_Click(object sender, EventArgs e)
        {
            if (RadNumericTextBoxStartRegionNo.Value.HasValue == false)
            {
                UIHelp.layerAlert(Page, "请输入起始注册编号！");
                return;
            }
            if(RadGridRYXX.MasterTableView.Items.Count==0)
            {
                UIHelp.layerAlert(Page, "请选择一个公告！");
                return;
            }
            DateTime cur = DateTime.Now;
            try
            {
               
                int codeReg = Convert.ToInt32(RadNumericTextBoxStartRegionNo.Value);
                foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
                {
                    //造价工程师注册证书编码由汉字和14位数字组成：
                    //汉字：建[造]
                    //（一）第1位为证书级别代码，取值为1～2，依次表示：
                    //        1一级造价工程师；
                    //        2二级造价工程师。
                    //（二）第2位为证书专业代码，取值为1～4，依次表示：
                    //        1土木建筑工程专业；
                    //        2交通运输工程专业；
                    //        3水利工程专业；
                    //        4安装工程专业。
                    //（三）第3、4位为证书核发年份代码，取核发年份的后两位数字。
                    //（四）第5、6位为省、自治区、直辖市代码，取值见代码表。（北京市：11）
                    //（五）第7、8位为行业管理机构代码，取值见代码表。（建筑：00）
                    //（六）第9～14位为证书核发顺序编号，从000001～999999依次顺序取值。
                    Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterNo");
                    (c as RadTextBox).Text = string.Format("建[造]2{0}{1}1100{2}"
                       ,GetZYCode(RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ApplyRegisteProfession"].ToString())
                      //, Convert.ToDateTime(RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ConferDate"]).ToString("yy")//取得资格证书年份
                      , cur.ToString("yy")//初始注册年份
                      , codeReg.ToString().PadLeft(6, '0'));//流水号

                    codeReg++;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "发放造价工程师注册证书编号失败！", ex);
                return;
            }
        }

        /// <summary>
        /// 根据注册专业返回类型编码
        /// </summary>
        /// <param name="RegisteProfession"></param>
        /// <returns></returns>
        protected string GetZYCode(string RegisteProfession)
        {
            switch (RegisteProfession)
            {
                case "土木建筑工程":
                    return "1";
                case "安装工程":
                    return "4";
                default:
                    throw new Exception("根据专业类型匹配专业类型代码出错。");
            }

        }

        //保存放号结果
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (RadDatePickerFZRQ.SelectedDate.HasValue == false)
            {
                UIHelp.layerAlert(Page, "请选择一个发证日期！");
                return;
            }

            if(RadGridRYXX.MasterTableView.Items.Count==0)
            {
                UIHelp.layerAlert(Page, "没有需要保存的放号结果！");
                return;
            }
            //1、更新申请表
            //2、新增证书表

            string type = Request["t"];//申请类型                  
            string noticecode = Request["o"]; //公告编号            
            string xgsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//更新时间
            DateTime beginDate = RadDatePickerFZRQ.SelectedDate.Value;//有效期起
            DateTime endDate = beginDate.AddYears(4).AddDays(-1);//有效期至
            string PSN_RegisterNo = "";//注册编号

            //更新脚本
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //注册号集合
            System.Text.StringBuilder sb_RegisterNo = new System.Text.StringBuilder();

            //"注册编号
            string sql = @"UPDATE [dbo].[zjs_Apply] SET [PSN_RegisterNo] = '{0}',[XGR] = '{1}',[XGSJ] = '{2}',[CodeMan] = '{1}',[CodeDate] = '{2}' WHERE  [ApplyID] = '{3}';";

            foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
            {
                Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterNo");
                PSN_RegisterNo = (c as RadTextBox).Text;
                sb.Append(string.Format(sql, PSN_RegisterNo, UserName, xgsj, RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ApplyID"]));
                sb_RegisterNo.Append(",'").Append(PSN_RegisterNo).Append("'");
            }

            //检查注册编号
            if (sb_RegisterNo.Length > 0)
            {
                sb_RegisterNo.Remove(0, 1);
                DataTable dt = zjs_ApplyDAL.FindExistPSN_RegisterNo(sb_RegisterNo.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("发现{0}等{1}个注册号已经存在，无法重复放号！", dt.Rows[0]["PSN_RegisterNo"], dt.Rows.Count));
                    return;
                }
            }

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //更新申请表
                CommonDAL.ExecSQL(tran, sb.ToString());
                switch (type)
                {
                    case "初始注册":
                        #region 初始注册

                        //初始注册往正式表导数据

                        #region 已加入年龄判断
                        CommonDAL.ExecSQL(tran, string.Format(@"INSERT INTO [dbo].[zjs_Certificate]
                                ([PSN_ServerID] ,[ENT_Name] ,[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex] ,[PSN_BirthDate]
                                ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime]
                                ,[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO]
                                ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession] ,[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],ZGZSBH
                                ,[CJR],[CJSJ] ,[XGR],[XGSJ],[Valid] ,[Memo])
                                SELECT newid(),ENT_Name,ENT_OrganizationsCode,ENT_City,[END_Addess],PSN_Name,PSN_Sex,Birthday,
                                Nation,PSN_CertificateType,PSN_CertificateNO,School,Major,GraduationTime,
                                XueLi,XueWei,PSN_MobilePhone,PSN_Email,PSN_Telephone,'01',A.PSN_RegisterNo,
                                PSN_RegisterCertificateNo,PSN_RegisteProfession,'{1}'
                                ,case when DateAdd(YEAR, 0, CONVERT(varchar(100), '{2}', 23)) > DateAdd(YEAR, 70,A.[Birthday]) 
                                    then  DateAdd(YEAR, 70,A.[Birthday])
                                    else '{2}' end
                                ,GETDATE(),b.PSN_ExamCertCode,
                                CJR,CJSJ,XGR,XGSJ,Valid,Memo
                                FROM(SELECT * FROM zjs_APPLY  WHERE  NoticeCode='{0}' and ConfirmResult='通过' and CodeDate is not null)A  INNER JOIN  zjs_ApplyFirst B  ON A.APPLYID=B.APPLYID"
                            , noticecode, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
                        #endregion



                        //更新证书附件中需要被覆盖的附件为历史附件
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].CodeDate is not null
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].CodeDate is not null
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", noticecode));


                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].CodeDate is not null
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].CodeDate is not null
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", noticecode));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[zjs_Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[zjs_Apply] 
                                    on [ApplyFile].ApplyID = [zjs_Apply].ApplyID 
                                    where [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].CodeDate is not null", noticecode));

                        #endregion
                        break;
                }

                tran.Commit();
                ButtonCreateCode.Enabled = false;
                ButtonSave.Enabled = false;
                ButtonCreateCode.CssClass = "bt_large btn_no";
                ButtonSave.CssClass =  "bt_large btn_no";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "造价工程师证书编号失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "造价工程师证书编号保存成功", string.Format("保存时间：{0}", DateTime.Now));
            UIHelp.layerAlert(Page, "保存成功！");
        }

        //导出放号结果
        protected void ButtonOut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["o"]) == false)
            {
                if (RadGridRYXX.MasterTableView.Items.Count == 0)
                {
                    return;
                }
                QueryParamOB q = new QueryParamOB();
                q.Add(string.Format("NoticeCode='{0}'", Request["o"]));
                q.Add("ConfirmResult='通过'");
                //DataTable dt = null;
                switch (Request["t"])
                {
                    case "初始注册":
                        q.Add("ApplyType='初始注册'");
                        break;
                }
                try
                {
                    //EXCEL表头明
                    string head = @"序号\姓名\身份证号\企业名称\注册类型\注册专业\注册编号\有效期\区县";
                    //数据表的列明
                    string column = @"row_number() over(order by ENT_City,ENT_Name)\PSN_Name\PSN_CertificateNO\ENT_Name\ApplyType\PSN_RegisteProfession\PSN_RegisterNo\PSN_RegisteProfession\ENT_City";
                    //过滤条件

                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
                    string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                    CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                        , "zjs_Apply "
                        , q.ToWhereString(), "ENT_City,ENT_Name", head.ToString(), column.ToString());
                    string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                    spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:10px;"">（{2}）</span>"
                        ,UIHelp.AddUrlReadParam( filePath.Replace("~", ".."))
                        , "点击我下载"
                        , size);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
                }            
            }
        }

    }
}