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


namespace ZYRYJG.Register
{
    public partial class NumberIssue : BasePage
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
                    DataTable dt = null;

                    switch (Request["t"])
                    {
                        case "初始注册":
                            ViewState["applytype"] = "初始注册";
                            RadComboBoxType.Items.FindItemByValue("注册编号和证书编号").Selected = true;
                            SetInputByApplyType(RadComboBoxType.SelectedValue);
                            dt = ApplyFirstDAL.GetListView(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                            if (dt.Rows.Count == 0)
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                                UIHelp.layerAlert(Page, "没有查到需要编号的数据。");
                            }
                            else if (dt.Rows[0]["CodeDate"] != DBNull.Value && dt.Rows[0]["CodeDate"].ToString() != "")//查询是否已经放号如果放号将发放编号和保存按钮置灰
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                            }
                            else
                            {
                                ButtonCreateCode.Enabled = true;
                                ButtonSave.Enabled = true;
                                RadNumericTextBoxStartRegionNo.Value = ApplyDAL.GetNextPSN_RegisterNO();
                                RadNumericTextBoxStartCertNo.Value = ApplyDAL.GetNextPSN_RegisterCertificateNo();
                            }
                            ButtonCreateCode.CssClass = ButtonCreateCode.Enabled == true ? "bt_large" : "bt_large btn_no";
                            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                            RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID", "ConferDate" };//添加资格证书发证日期
                            break;
                        case "重新注册":
                            ViewState["applytype"] = "重新注册";
                            RadComboBoxType.Items.FindItemByValue("证书编号").Selected = true;
                            SetInputByApplyType(RadComboBoxType.SelectedValue);
                            dt = ApplyDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                            if (dt.Rows.Count == 0)
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                                UIHelp.layerAlert(Page, "没有查到需要编号的数据。");
                            }
                            else if (dt.Rows[0]["CodeDate"] != DBNull.Value && dt.Rows[0]["CodeDate"].ToString() != "")//查询是否已经放号如果放号将发放编号和保存按钮置灰
                            {
                                ButtonCreateCode.Enabled = false;
                                ButtonSave.Enabled = false;
                            }
                            else
                            {
                                ButtonCreateCode.Enabled = true;
                                ButtonSave.Enabled = true;
                                RadNumericTextBoxStartCertNo.Value = ApplyDAL.GetNextPSN_RegisterCertificateNo();
                            }
                            ButtonCreateCode.CssClass = ButtonCreateCode.Enabled == true ? "bt_large" : "bt_large btn_no";
                            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
                            break;
                    }

                    RadGridRYXX.DataSource = dt;
                    RadGridRYXX.DataBind();

                }

                if (string.IsNullOrEmpty(Request["ys"]) == false)
                {
                    ViewState["applytype"] = "遗失补办";
                    RadComboBoxType.Items.FindItemByValue("证书编号").Selected = true;
                    SetInputByApplyType(RadComboBoxType.SelectedValue);
                    RadGridRYXX.MasterTableView.Columns[6].Visible = true;
                    RadComboBoxApplyType.Visible = true;
                    RadComboBoxApplyLevel.Visible = true;
                    get();


                    QueryParamOB q = new QueryParamOB();
                    //申请状态
                    q.Add("ConfirmResult='通过'");
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
                    q.Add(string.Format("ApplyType ='{0}' and CodeDate is null", EnumManager.ApplyType.遗失补办));
                    q.Add(string.Format("PSN_Level='{0}'", RadComboBoxApplyLevel.SelectedValue));
                    DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                    RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
                    RadGridRYXX.DataSource = dt;
                    RadGridRYXX.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        ButtonCreateCode.Visible = true;
                        ButtonSave.Visible = true;
                        UIHelp.layerAlert(Page, string.Format("已查到尚未编号的遗失补办证书共计 {0} 本，您可以开始编号了。", dt.Rows.Count));
                    }
                    else
                    {
                        ButtonCreateCode.Visible = false;
                        ButtonSave.Visible = false;
                        UIHelp.layerAlert(Page, "目前没有需要编号的遗失补办证书。");
                    }
                }
            }
        }

        //变换发放类型
        protected void SetInputByApplyType(string ApplyType)
        {
            RadComboBoxType.Enabled = false;
            RadNumericTextBoxStartRegionNo.Value = null;
            RadNumericTextBoxStartCertNo.Value = null;
            switch (ApplyType)
            {
                case "证书编号":
                    divStartRegionNoLable.Style.Add("display", "none");
                    divStartRegionNo.Style.Add("display", "none");
                    divStartCertNoLable.Style.Add("display", "inline");
                    divStartCertNo.Style.Add("display", "inline");
                    divBtn.Style.Add("display", "inline");
                    divCertTypeLable.Style.Add("display", "inline");
                    divCertType.Style.Add("display", "inline");
                    RadNumericTextBoxStartRegionNo.Enabled = false;
                    RadGridRYXX.MasterTableView.Columns.FindByUniqueName("PSN_RegisterNo").Display = false;
                    break;
                case "注册编号和证书编号":
                    divStartRegionNoLable.Style.Add("display", "inline");
                    divStartRegionNo.Style.Add("display", "inline");
                    divStartCertNoLable.Style.Add("display", "inline");
                    divStartCertNo.Style.Add("display", "inline");
                    divBtn.Style.Add("display", "inline");
                    divCertTypeLable.Style.Add("display", "inline");
                    divCertType.Style.Add("display", "inline");
                    RadNumericTextBoxStartRegionNo.Enabled = true;
                    RadGridRYXX.MasterTableView.Columns.FindByUniqueName("PSN_RegisterNo").Display = true;
                    break;
                default:
                    divStartRegionNoLable.Style.Add("display", "none");
                    divStartRegionNo.Style.Add("display", "none");
                    divStartCertNoLable.Style.Add("display", "none");
                    divStartCertNo.Style.Add("display", "none");
                    divBtn.Style.Add("display", "none");
                    divCertTypeLable.Style.Add("display", "none");
                    divCertType.Style.Add("display", "none");
                    RadNumericTextBoxStartRegionNo.Enabled = false;
                    RadNumericTextBoxStartRegionNo.Enabled = false;
                    break;

            }
        }

        //放号
        protected void ButtonCreateCode_Click(object sender, EventArgs e)
        {
            DateTime cur = DateTime.Now;
            try
            {
                if (RadComboBoxType.SelectedItem.Value == "证书编号")
                {
                    if (RadNumericTextBoxStartCertNo.Value.HasValue == false)
                    {


                        UIHelp.layerAlert(Page, "请输入起始证书编号！");
                        return;
                    }

                    int code = Convert.ToInt32(RadNumericTextBoxStartCertNo.Value);

                    foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
                    {

                        Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterCertificateNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterCertificateNo");
                        (c as RadTextBox).Text = code.ToString().PadLeft(8, '0');
                        code++;
                    }
                }
                else if (RadComboBoxType.SelectedItem.Value == "注册编号和证书编号")
                {
                    if (RadNumericTextBoxStartCertNo.Value.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入起始证书编号！");
                        return;
                    }
                    if (RadNumericTextBoxStartRegionNo.Value.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入起始注册编号！");
                        return;
                    }
                    int code = Convert.ToInt32(RadNumericTextBoxStartCertNo.Value);
                    int codeReg = Convert.ToInt32(RadNumericTextBoxStartRegionNo.Value);
                    foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
                    {

                        Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterCertificateNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterCertificateNo");
                        (c as RadTextBox).Text = code.ToString().PadLeft(8, '0');
                        code++;

                        c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterNo");

                        //(c as RadTextBox).Text = string.Format("京211{0}{1}{2}"
                        //    , Convert.ToDateTime(RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ConferDate"]).ToString("yy")//取得资格证书年份
                        //    , cur.ToString("yy")//初始注册年份
                        //    , codeReg.ToString().PadLeft(5, '0'));//流水号

                        (c as RadTextBox).Text = string.Format("京211{0}{1}{2}"
                          , Convert.ToDateTime(RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ConferDate"]).ToString("yyyy")//取得资格证书年份
                          , cur.ToString("yyyy")//初始注册年份
                          , codeReg.ToString().PadLeft(5, '0'));//流水号

                        codeReg++;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "发放编号失败！", ex);
                return;
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
            //1、更新申请表
            //2、将证书表、专业表数据移动至历史表
            //3、更新证书表及专业表

            string type;//申请类型                  
            string noticecode; //公告编号            
            string xgsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//更新时间
            DateTime beginDate = RadDatePickerFZRQ.SelectedDate.Value;//有效期起
            DateTime endDate = beginDate.AddYears(3).AddDays(-1);//有效期至


            string sql = "";

            //更新脚本
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //给遗失污损补办取到ID
            System.Text.StringBuilder applyid = new System.Text.StringBuilder();

            //"注册编号和证书编号"
            string sql1 = @"UPDATE [dbo].[Apply] SET [PSN_RegisterNo] = '{0}',[PSN_RegisterCertificateNo] = '{1}',[XGR] = '{2}',[XGSJ] = '{3}',[CodeMan] = '{2}',[CodeDate] = '{3}',[PSN_ServerID]='{5}' WHERE  [ApplyID] = '{4}';";

            //证书编号
            string sql2 = @"UPDATE [dbo].[Apply] SET [PSN_RegisterCertificateNo] = '{0}',[XGR] = '{1}',[XGSJ] = '{2}',[CodeMan] = '{1}',[CodeDate] = '{2}' WHERE  [ApplyID] = '{3}';";



            if (RadComboBoxType.SelectedItem.Value == "证书编号")
            {
                foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
                {
                    Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterCertificateNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterCertificateNo");
                    sb.Append(string.Format(sql2, (c as RadTextBox).Text, UserName, xgsj, RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ApplyID"]));

                    //给遗失污损补办取到ID
                    applyid.Append("'").Append(RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ApplyID"]).Append("'").Append(",");

                    if ((c as RadTextBox).Text=="")
                    {
                        UIHelp.layerAlert(Page, "证书编号不能为空！");
                        return;
                    }
                }
            }
            else if (RadComboBoxType.SelectedItem.Value == "注册编号和证书编号")
            {
                foreach (Telerik.Web.UI.GridDataItem i in RadGridRYXX.MasterTableView.Items)
                {
                    Control c = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterCertificateNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterCertificateNo");
                    Control c2 = i.Cells[RadGridRYXX.Columns.FindByUniqueName("PSN_RegisterNo").OrderIndex].FindControl("RadTextBoxPSN_RegisterNo");
                    sb.Append(string.Format(sql1, (c2 as RadTextBox).Text, (c as RadTextBox).Text, UserName, xgsj, RadGridRYXX.MasterTableView.DataKeyValues[i.ItemIndex]["ApplyID"], Guid.NewGuid().ToString()));

                    if ((c2 as RadTextBox).Text == "")
                    {
                        UIHelp.layerAlert(Page, "注册编号不能为空！");
                        return;
                    }

                    //检查注册编号
                    ApplyMDL _ApplyMDL = ApplyDAL.GetObjectPSN_RegisterNo((c2 as RadTextBox).Text);
                    if (_ApplyMDL != null)
                    {
                        UIHelp.layerAlert(Page, string.Format("{0}注册号已经存在！", (c2 as RadTextBox).Text));
                        return;
                    }
                }
            }

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                //更新申请表
                CommonDAL.ExecSQL(tran, sb.ToString());

                //在界面选择遗失补办的时候是没有这两个对象的，只有点击遗失补办按钮才会出现此类情况
                if (Request["t"] != null && Request["o"] != null)
                {
                    type = Request["t"].ToString();
                    noticecode = Request["o"].ToString();
                }
                else
                {
                    type = "遗失补办";
                    noticecode = "";
                }
                switch (type)
                {
                    case "初始注册":
                        #region 初始注册
                        //DataTable dt = CommonDAL.GetDataTable(tran,@"Select * From Apply Where NoticeCode='" + noticecode + "'");
                        //System.Text.StringBuilder ssb = new System.Text.StringBuilder();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    ssb.Append("'").Append(dt.Rows[i]["ApplyID"].ToString()).Append("'").Append(",");
                        //    ApplyDAL.UpdateApplyFirst(tran, Guid.NewGuid().ToString(), dt.Rows[i]["ApplyID"].ToString());
                        //}
                        //if (ssb.Length > 0)
                        //    ssb.Remove(ssb.Length - 1, 1);
                        //DataTable dt1 = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  ApplyID IN(" + ssb.ToString() + ") AND NoticeCode='" + noticecode + "' ) A INNER JOIN  APPLYFIRST B  ON A.APPLYID=B.APPLYID");

                        DataTable dt1 = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  NoticeCode='" + noticecode + "' and ConfirmResult='通过' ) A INNER JOIN  APPLYFIRST B  ON A.APPLYID=B.APPLYID");
                        //初始注册往正式表导数据
                        #region 未加入年龄判断
                        //                        CommonDAL.ExecSQL(tran, string.Format(@"INSERT INTO COC_TOW_Person_BaseInfo
                        //                                                     ([PSN_ServerID] ,ENT_ServerID,[ENT_Name] ,[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex] ,[PSN_BirthDate]
                        //                                                     ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime]
                        //                                                     ,[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_RegisteType],[PSN_RegisterNO]
                        //                                                     ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession] ,[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],ZGZSBH,[CJR]
                        //                                                     ,[CJSJ] ,[XGR],[XGSJ],[Valid] ,[Memo])
                        //                                                    SELECT PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,GETDATE(),'2500-01-01 00:00:00.000',PSN_Name,PSN_Sex,Birthday,
                        //                                                    Nation,PSN_CertificateType,PSN_CertificateNO,School,Major,GraduationTime,
                        //                                                    XueLi,XueWei,PSN_MobilePhone,PSN_Telephone,PSN_Email,'01',A.PSN_RegisterNo,
                        //                                                    PSN_RegisterCertificateNo,PSN_RegisteProfession,'{1}','{2}',GETDATE(),ArchitectType,b.PSN_ExamCertCode,
                        //                                                    CJR,CJSJ,XGR,XGSJ,Valid,Memo
                        //                                                    FROM(SELECT * FROM APPLY  WHERE  NoticeCode='{0}' and ConfirmResult='通过')A  INNER JOIN  APPLYFIRST B  ON A.APPLYID=B.APPLYID", noticecode, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
                        #endregion

                        #region 已加入年龄判断
                        CommonDAL.ExecSQL(tran, string.Format(@"INSERT INTO COC_TOW_Person_BaseInfo
                                                     ([PSN_ServerID] ,ENT_ServerID,[ENT_Name] ,[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex] ,[PSN_BirthDate]
                                                     ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime]
                                                     ,[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_RegisteType],[PSN_RegisterNO]
                                                     ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession] ,[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],ZGZSBH,[CJR]
                                                     ,[CJSJ] ,[XGR],[XGSJ],[Valid] ,[Memo],[END_Addess])
                                                    SELECT PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,GETDATE(),'2500-01-01 00:00:00.000',PSN_Name,PSN_Sex,Birthday,
                                                    Nation,PSN_CertificateType,PSN_CertificateNO,School,Major,GraduationTime,
                                                    XueLi,XueWei,PSN_MobilePhone,PSN_Telephone,PSN_Email,'01',A.PSN_RegisterNo,
                                                    PSN_RegisterCertificateNo,PSN_RegisteProfession,'{1}',[dbo].[GET_PSN_CertificateValidity]('{2}',PSN_CertificateNO,PSN_Level),GETDATE(),ArchitectType,b.PSN_ExamCertCode,
                                                    CJR,CJSJ,XGR,XGSJ,Valid,Memo,[END_Addess]
                                                    FROM(SELECT * FROM APPLY  WHERE  NoticeCode='{0}' and ConfirmResult='通过')A  INNER JOIN  APPLYFIRST B  ON A.APPLYID=B.APPLYID", noticecode, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
                        #endregion

                        //往专业表写数据
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
                            _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Guid.NewGuid().ToString();
                            if (dt1.Rows[j]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = dt1.Rows[j]["PSN_ServerID"].ToString();
                            if (dt1.Rows[j]["ApplyRegisteProfession"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = dt1.Rows[j]["ApplyRegisteProfession"].ToString();
                            if (dt1.Rows[j]["ConferDate"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(beginDate.ToString("yyyy-MM-dd"));

                            #region 未加入年龄判断
                            //_COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = Convert.ToDateTime(endDate.ToString("yyyy-MM-dd"));
                            #endregion

                            #region 已加入年龄判断
                            _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = UIHelp.GET_PSN_CertificateValidity(Convert.ToDateTime(endDate.ToString()), dt1.Rows[j]["PSN_CertificateNO"].ToString(), dt1.Rows[j]["PSN_Level"].ToString());//Convert.ToDateTime(endDate.ToString("yyyy-MM-dd"));
                            #endregion

                            COC_TOW_Register_ProfessionDAL.Insert(tran, _COC_TOW_Register_ProfessionMDL);

                            //如果没有其他专业在插入时默认为,,,,,有其他专业也要插入进去
                            if (dt1.Rows[j]["ExamInfo"].ToString() != ",,,,," && !string.IsNullOrEmpty(dt1.Rows[j]["ExamInfo"].ToString()))
                            {
                                string ExamInfo = dt1.Rows[j]["ExamInfo"].ToString();
                                string[] rows = ExamInfo.Split('|');
                                for (int u = 0; u < rows.Length; u++)
                                {
                                    string[] b = rows[u].ToString().Split(',');
                                    COC_TOW_Register_ProfessionMDL COC_TOW_Register_ProfessionMDL_Min = new COC_TOW_Register_ProfessionMDL();
                                    COC_TOW_Register_ProfessionMDL_Min.PRO_ServerID = Guid.NewGuid().ToString();
                                    if (dt1.Rows[j]["PSN_ServerID"] != DBNull.Value) COC_TOW_Register_ProfessionMDL_Min.PSN_ServerID = dt1.Rows[j]["PSN_ServerID"].ToString();
                                    if (b[3] != null) COC_TOW_Register_ProfessionMDL_Min.PRO_Profession = b[3].ToString();

                                    if (b[2] != null) COC_TOW_Register_ProfessionMDL_Min.PRO_ValidityBegin = Convert.ToDateTime(beginDate.ToString("yyyy-MM-dd"));

                                    #region 未加入年龄判断
                                    //COC_TOW_Register_ProfessionMDL_Min.PRO_ValidityEnd = Convert.ToDateTime(endDate.ToString("yyyy-MM-dd"));
                                    #endregion

                                    #region 已加入年龄判断
                                    COC_TOW_Register_ProfessionMDL_Min.PRO_ValidityEnd = UIHelp.GET_PSN_CertificateValidity(Convert.ToDateTime(endDate.ToString()), dt1.Rows[j]["PSN_CertificateNO"].ToString(), dt1.Rows[j]["PSN_Level"].ToString());
                                    #endregion


                                    //if (b[2] != null) COC_TOW_Register_ProfessionMDL_Min.PRO_ValidityBegin = Convert.ToDateTime(beginDate.ToString("yyyy-MM-dd"));
                                    //COC_TOW_Register_ProfessionMDL_Min.PRO_ValidityEnd = Convert.ToDateTime(endDate.ToString("yyyy-MM-dd"));
                                    COC_TOW_Register_ProfessionDAL.Insert(tran, COC_TOW_Register_ProfessionMDL_Min);
                                }
                            }
                        }
                        ////初始注册对人员照片进行操作
                        //DataTable zp = CommonDAL.GetDataTable(tran, @"SELECT * FROM APPLY  WHERE  NoticeCode='" + noticecode + "' and ConfirmResult='通过'");
                        ////根据公告号拿到一个内存表,然后遍历取到行APPLYID，带到照片申请表去查询他信息，把申请照片信息移入到正式信息表去
                        //if (zp.Rows.Count > 0)
                        //{
                        //    for (int s = 0; s < zp.Rows.Count; s++)
                        //    {
                        //        List<ApplyFileMDL> _ApplyFileMDL = ApplyFileDAL.GetObjectApplyID(zp.Rows[s]["ApplyID"].ToString());
                        //        if (_ApplyFileMDL != null)
                        //        {
                        //            foreach (ApplyFileMDL j in _ApplyFileMDL)
                        //            {
                        //                COC_TOW_Person_FileMDL _Person_FileMDL = new COC_TOW_Person_FileMDL();
                        //                _Person_FileMDL.FileID = j.FileID;
                        //                _Person_FileMDL.PSN_RegisterNO = zp.Rows[s]["PSN_RegisterNO"].ToString();
                        //                _Person_FileMDL.IsHistory = false;
                        //                COC_TOW_Person_FileDAL.Insert(tran, _Person_FileMDL);
                        //            }
                        //        }

                        //    }
                        //}

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
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
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
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", noticecode));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'", noticecode));

                        #endregion
                        break;
                    case "重新注册":
                        #region
                        //专业写入历史表
                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 
                                inner join [dbo].[COC_TOW_Register_Profession] p
                                on a.PSN_ServerID = p.PSN_ServerID
                                where a.[NoticeCode]='{2}' and a.ConfirmResult='通过'";

                        CommonDAL.ExecSQL(tran, string.Format(sql, "重新注册", xgsj, noticecode));

                        //删除证书专业
                        sql = @"delete from [dbo].[COC_TOW_Register_Profession]
                              where exists(select 1 FROM [dbo].[Apply] where [NoticeCode]='{0}' and ConfirmResult='通过' and [Apply].PSN_ServerID = [COC_TOW_Register_Profession].PSN_ServerID );";
                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                        //新增正式专业
                        #region 未加入超龄判断

                        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code])
                        //                                    select newid(),t1.[PSN_ServerID],t2.Profession,'{1}','{2}','110000'
                        //                                    from
                        //                                    (
                        //                                        select PSN_RegisteProfession,[PSN_ServerID] FROM [dbo].[Apply] where [NoticeCode]='{0}' and ConfirmResult='通过'  
                        //                                    ) t1
                        //                                    inner join 
                        //                                    (
                        //                                        select '建筑' as Profession
                        //                                        union select '市政' 
                        //                                        union select '公路' 
                        //                                        union select '水利' 
                        //                                        union select '矿业' 
                        //                                        union select '机电' 
                        //                                    ) t2 on t1.PSN_RegisteProfession like '%' +t2.Profession +'%'";
                        #endregion

                        #region 加入超龄判断
                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code])
                                    select newid(),t1.[PSN_ServerID],t2.Profession,'{1}',[dbo].[GET_PSN_CertificateValidity]('{2}',t1.PSN_CertificateNO,t1.PSN_Level),'110000'
                                    from
                                    (
                                        select PSN_RegisteProfession,[PSN_ServerID],PSN_CertificateNO,PSN_Level FROM [dbo].[Apply] where [NoticeCode]='{0}' and ConfirmResult='通过'  
                                    ) t1
                                    inner join 
                                    (
                                        select '建筑' as Profession
                                        union select '市政' 
                                        union select '公路' 
                                        union select '水利' 
                                        union select '矿业' 
                                        union select '机电' 
                                    ) t2 on t1.PSN_RegisteProfession like '%' +t2.Profession +'%'";


                        #endregion

                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));

                        //人员表写入历史
                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where [NoticeCode]='{0}' and ConfirmResult='通过')";

                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                        //更新人员正式表
                        sql = @"UPDATE [dbo].[COC_TOW_Person_BaseInfo]
                                set [COC_TOW_Person_BaseInfo].[ENT_ServerID] = [Apply].[ENT_ServerID]
							        ,[COC_TOW_Person_BaseInfo].[ENT_Name] = [Apply].[ENT_Name]
                                    ,[COC_TOW_Person_BaseInfo].[ENT_City] = [Apply].[ENT_City]
							        ,[COC_TOW_Person_BaseInfo].[ENT_OrganizationsCode] = [Apply].[ENT_OrganizationsCode]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_BeforENT_Name] = (case when [Apply].[ENT_Name] <>[COC_TOW_Person_BaseInfo].[ENT_Name] then [COC_TOW_Person_BaseInfo].[ENT_Name] else null end)
                                    ,[COC_TOW_Person_BaseInfo].[PSN_BeforENT_ServerID] = (case when [Apply].[ENT_ServerID] <>[COC_TOW_Person_BaseInfo].[ENT_ServerID] then [COC_TOW_Person_BaseInfo].[ENT_ServerID] else null end)
                                    ,[COC_TOW_Person_BaseInfo].[PSN_Name] = [Apply].[PSN_Name]   
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateNO] = [Apply].[PSN_CertificateNO]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_BirthDate]= case when ISDATE(SUBSTRING([Apply].[PSN_CertificateNO],7,8))=1 then cast(SUBSTRING([Apply].[PSN_CertificateNO],7,8) as datetime) else  [COC_TOW_Person_BaseInfo].PSN_BirthDate end 
                                    ,[COC_TOW_Person_BaseInfo].[PSN_Sex] = [Apply].[PSN_Sex]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='05'    
                                    ,[PSN_RegisterCertificateNo]=[Apply].[PSN_RegisterCertificateNo]                     
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteProfession]=[Apply].PSN_RegisteProfession
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificationDate]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity]=[dbo].[GET_PSN_CertificateValidity]('{1}',[Apply].PSN_CertificateNO,[Apply].PSN_Level)
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate]='{2}'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{3}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{2}'
                                    ,[END_Addess] = [ApplyRenew].[END_Addess]
                                    from [dbo].[COC_TOW_Person_BaseInfo] ,[dbo].[Apply],[dbo].[ApplyRenew] 
                                    where [NoticeCode]='{4}' and ConfirmResult='通过' and [COC_TOW_Person_BaseInfo].[PSN_ServerID] = [Apply].[PSN_ServerID] and [Apply].ApplyID=[ApplyRenew].ApplyID";
                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , beginDate.ToString("yyyy-MM-dd")
                            , endDate.ToString("yyyy-MM-dd")
                            , xgsj
                            , UserName
                            , noticecode));

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
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].[NoticeCode]='{0}' and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].[NoticeCode]='{0}' and [Apply].ConfirmResult='通过'
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
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].[NoticeCode]='{0}' and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].[NoticeCode]='{0}' and [Apply].ConfirmResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", noticecode));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].[NoticeCode]='{0}' and [Apply].ConfirmResult='通过'", noticecode));

                        #endregion
                        break;
                    case "遗失补办":
                        #region   //遗失污损补办
                        if (applyid.Length > 0)
                            applyid.Remove(applyid.Length - 1, 1);
                        DataTable zdt1 = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  ApplyID IN(" + applyid.ToString() + ") and ConfirmResult='通过') A INNER JOIN  ApplyReplace B  ON A.APPLYID=B.APPLYID");
                        //往专业表写数据
                        for (int j = 0; j < zdt1.Rows.Count; j++)
                        {
                            //把专业往历史记录表挪一次
                            List<COC_TOW_Register_ProfessionMDL> COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.ListGetObject(tran, zdt1.Rows[j]["PSN_ServerID"].ToString());
                            foreach (var c in COC_TOW_Register_ProfessionMDL)
                            {
                                COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = COC_TOW_Register_Profession_HisDAL.ListGetObject(tran, c, EnumManager.ApplyType.重新注册);
                                //先往专业历史表插入数据，在删除专业表的数据
                                COC_TOW_Register_Profession_HisDAL.Insert(tran, _COC_TOW_Register_Profession_HisMDL);
                            }
                            //往历史记录表备份数据,根据建造师ID拿到去正式表拿到一个对象
                            COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(tran, zdt1.Rows[j]["PSN_ServerID"] == DBNull.Value ? "" : zdt1.Rows[j]["PSN_ServerID"] == DBNull.Value ? "" : zdt1.Rows[j]["PSN_ServerID"].ToString());
                            //先往历史纪录表导入数据
                            if (_COC_TOW_Person_BaseInfoMDL != null)
                            {
                                //正式表往记录表写数，右边是一个方法，根据建造师ID拿到一个记录表的信息
                                COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran, _COC_TOW_Person_BaseInfoMDL);
                                COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
                            }
                            if (zdt1.Rows[j]["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = zdt1.Rows[j]["PSN_RegisterCertificateNo"].ToString();
                            if (zdt1.Rows[j]["ReplaceReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = zdt1.Rows[j]["ReplaceReason"].ToString();
                            if (zdt1.Rows[j]["ReplaceType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = zdt1.Rows[j]["ReplaceType"].ToString();
                            if (zdt1.Rows[j]["ValidCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = zdt1.Rows[j]["ValidCode"].ToString();
                            _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = "06";
                            //注册审批日期
                            _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = DateTime.Now;
                            //Update正式表信息
                            COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);

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
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID IN({0}) and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID IN({0}) and [Apply].ConfirmResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", applyid));


                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID IN({0}) and [Apply].ConfirmResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID IN({0}) and [Apply].ConfirmResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", applyid));

                            //将申请单附件写入证书附件库
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ApplyID IN({0}) and [Apply].ConfirmResult='通过'", applyid));
                        }
                        //给消息表发送企业消息通知
                        CommonDAL.ExecSQL(tran, string.Format(@"INSERT INTO [dbo].[ApplyNews]([ID],[ApplyID],[PSN_Name],[PSN_CertificateNO] ,[PSN_RegisterNo] ,[ApplyType],[SFCK],[ENT_OrganizationsCode],[ENT_City])
                        SELECT NEWID(),[ApplyID],[PSN_Name],[PSN_CertificateNO],[PSN_RegisterNo],[ApplyType],0,[ENT_OrganizationsCode],[ENT_City]
                        FROM APPLY WHERE ApplyID in ({0})", applyid.ToString()));


                        #endregion
                        break;
                }

                tran.Commit();
                if (type == "遗失补办")
                {
                    get();//显示放号时间
                    RadComboBoxApplyType.SelectedIndex = 1;
                }
                UIHelp.WriteOperateLog(UserName, UserID, "证书编号保存成功", string.Format("保存时间：{0}", DateTime.Now));
                UIHelp.layerAlert(Page, "保存成功！");

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书编号失败！", ex);
                return;
            }
        }

        private void get()
        {

            string sql = @"select top 10   CONVERT(varchar(100), CodeDate, 23) as CodeDate  from Apply where ApplyType='遗失补办' and ApplyStatus='已公告' and CodeDate is not null group by   CONVERT(varchar(100), CodeDate, 23)   order by   CONVERT(varchar(100), CodeDate, 23) desc";
            DataTable dt_select = CommonDAL.GetDataTable(sql);

            RadComboBoxApplyType.DataSource = dt_select;
            RadComboBoxApplyType.DataTextField = "CodeDate";
            RadComboBoxApplyType.DataValueField = "CodeDate";
            RadComboBoxApplyType.DataBind();
            RadComboBoxApplyType.AllowCustomText = false;
            RadComboBoxApplyType.Items.Insert(0, new RadComboBoxItem("未放号", "未放号"));
        }

        //绑定遗失补办待放号记录
        protected void ButtonSelectRePlaceApply_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("NumberIssue.aspx?ys=0");

            //ViewState["applytype"] = "遗失补办";
            //RadComboBoxType.Items.FindItemByValue("证书编号").Selected = true;
            //SetInputByApplyType(RadComboBoxType.SelectedValue);
            //RadGridRYXX.MasterTableView.Columns[6].Visible = true;
            //RadComboBoxApplyType.Visible = true;
            //get();


            //QueryParamOB q = new QueryParamOB();
            ////申请状态
            //q.Add("ConfirmResult='通过'");
            //q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
            //q.Add(string.Format("ApplyType ='{0}' and CodeDate is null", EnumManager.ApplyType.遗失补办));
            //DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
            //RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
            //RadGridRYXX.DataSource = dt;
            //RadGridRYXX.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    ButtonCreateCode.Visible = true;
            //    ButtonSave.Visible = true;
            //    UIHelp.Alert(Page, string.Format("已查到尚未编号的遗失补办证书共计 {0} 本，您可以开始编号了。", dt.Rows.Count));
            //}
            //else
            //{
            //    ButtonCreateCode.Visible = false;
            //    ButtonSave.Visible = false;
            //    UIHelp.Alert(Page, "目前没有需要编号的遗失补办证书。");
            //}

        }

        //导出放号结果
        protected void ButtonOut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["o"]) == false)
            {
                QueryParamOB q = new QueryParamOB();
                q.Add(string.Format("A.NoticeCode='{0}'", Request["o"]));
                q.Add("A.ConfirmResult='通过'");
                //DataTable dt = null;
                switch (Request["t"])
                {
                    case "初始注册":
                        q.Add("ApplyType='初始注册'");
                        break;

                    case "重新注册":
                        q.Add("ApplyType='重新注册'");
                        break;
                }
                try
                {
                    //EXCEL表头明
                    string head = @"序号\姓名\身份证号\企业名称\注册类型\注册专业\注册编号\证书编号\证书及有效期\区县";
                    //数据表的列明
                    string column = @"row_number() over(order by A.ENT_City,A.ENT_Name)\A.PSN_Name\A.PSN_CertificateNO\A.ENT_Name\A.ApplyType\A.PSN_RegisteProfession\A.PSN_RegisterNo\A.PSN_RegisterCertificateNo\b.ProfessionWithValid\A.ENT_City";
                    //过滤条件

                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
                    string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                    CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                        , "Apply as A left join View_JZS_TOW_WithProfession as b on A.PSN_ServerID=b.PSN_ServerID"
                        , q.ToWhereString(), "a.ENT_City,a.ENT_Name", head.ToString(), column.ToString());
                    string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                    spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:10px;"">（{2}）</span>"
                        , UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                        , "点击我下载"
                        , size);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
                }


            }

            if (applytype == "遗失补办")
            {

                QueryParamOB q = new QueryParamOB();
                //q.Add(string.Format("A.NoticeCode='{0}'", Request["o"]));
                q.Add("A.ConfirmResult='通过'");
                //DataTable dt = null;
                q.Add(string.Format("A.ApplyType='{0}'", "遗失补办"));
                q.Add(string.Format("CONVERT(varchar(100), A.CodeDate, 23)='{0}'", RadComboBoxApplyType.SelectedValue));
                try
                {
                    //EXCEL表头明
                    string head = @"序号\姓名\身份证号\企业名称\注册类型\注册专业\注册编号\证书编号\证书及有效期\区县";
                    //数据表的列明
                    string column = @"row_number() over(order by A.ENT_City,A.ENT_Name)\A.PSN_Name\A.PSN_CertificateNO\A.ENT_Name\A.ApplyType\A.PSN_RegisteProfession\A.PSN_RegisterNo\A.PSN_RegisterCertificateNo\b.ProfessionWithValid\A.ENT_City";
                    //过滤条件

                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
                    string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                    CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                        , "Apply as A left join View_JZS_TOW_WithProfession as b on A.PSN_ServerID=b.PSN_ServerID"
                        , q.ToWhereString(), "a.ENT_City,a.ENT_Name", head.ToString(), column.ToString());
                    string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                    spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:10px;"">（{2}）</span>"
                        , UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                        , "点击我下载"
                        , size);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
                }
            }

        }

        //选择放号类型
        protected void RadComboBoxApplyType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboBoxApplyType.SelectedValue != "未放号")
            {
                QueryParamOB q = new QueryParamOB();
                //申请状态
                q.Add("ConfirmResult='通过'");
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
                q.Add(string.Format("ApplyType ='{0}' and CONVERT(varchar(100), CodeDate, 23)='{1}'", EnumManager.ApplyType.遗失补办, RadComboBoxApplyType.SelectedValue));
                q.Add(string.Format("PSN_Level='{0}'", RadComboBoxApplyLevel.SelectedValue));
                DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
                RadGridRYXX.DataSource = dt;
                RadGridRYXX.DataBind();
            }
            else
            {
                QueryParamOB q = new QueryParamOB();
                //申请状态
                q.Add("ConfirmResult='通过'");
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
                q.Add(string.Format("ApplyType ='{0}' and CodeDate is null", EnumManager.ApplyType.遗失补办));
                q.Add(string.Format("PSN_Level='{0}'", RadComboBoxApplyLevel.SelectedValue));
                DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
                RadGridRYXX.DataSource = dt;
                RadGridRYXX.DataBind();

            }
        }

        //选择证书等级
        protected void RadComboBoxApplyLevel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboBoxApplyType.SelectedValue != "未放号")
            {
                QueryParamOB q = new QueryParamOB();
                //申请状态
                q.Add("ConfirmResult='通过'");
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
                q.Add(string.Format("ApplyType ='{0}' and CONVERT(varchar(100), CodeDate, 23)='{1}'", EnumManager.ApplyType.遗失补办, RadComboBoxApplyType.SelectedValue));
                q.Add(string.Format("PSN_Level='{0}'", RadComboBoxApplyLevel.SelectedValue));
                DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
                RadGridRYXX.DataSource = dt;
                RadGridRYXX.DataBind();
            }
            else
            {
                QueryParamOB q = new QueryParamOB();
                //申请状态
                q.Add("ConfirmResult='通过'");
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));
                q.Add(string.Format("ApplyType ='{0}' and CodeDate is null", EnumManager.ApplyType.遗失补办));
                q.Add(string.Format("PSN_Level='{0}'", RadComboBoxApplyLevel.SelectedValue));
                DataTable dt = ApplyDAL.GetList3(0, int.MaxValue - 1, q.ToWhereString(), "ENT_City,ENT_Name");
                RadGridRYXX.MasterTableView.DataKeyNames = new string[] { "ApplyID" };//添加资格证书发证日期
                RadGridRYXX.DataSource = dt;
                RadGridRYXX.DataBind();

            }
        }
    }
}