using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.zjs
{
    //公告
    public partial class zjsNoticeLook : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindRadGridHZSB();
            }
        }
        protected void BindRadGridHZSB()
        {
            try
            {
                ObjectDataSource1.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();
                q.Add(string.Format("NoticeCode like'%{0}%'", RadTextBoxNoticeCode.Text));
                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridHZSB.CurrentPageIndex = 0;
                RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "查询造价工程师公告列表失败", ex);
            }

        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            BindRadGridHZSB();
        }
        //模板列按钮
        protected void RadGridHZSB_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //System.Text.StringBuilder zy = new System.Text.StringBuilder();//处理专业
            DateTime doTime = DateTime.Now;//处理时间
            string sql = "";
            switch (e.CommandName)
            {
                case "report"://公告
                     string type = RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString();//申请类型
                     string noticecode = RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString();//公告编号

                    //开启事务
                    DBHelper db = new DBHelper();
                    DbTransaction tran = db.BeginTransaction();
                    try
                    {
                        //更细申请表信息
                        zjs_ApplyDAL.ExeNoticeReport(tran, EnumManager.ApplyStatus.已公告
                        , doTime
                        , UserName
                        , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString());

                        //办结，更新证书信息
                       
                        switch (type)
                        {
                            case "初始注册":
                                #region 初始注册(批量：快)

                                //证书信息写入历史表
                                sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where [PSN_RegisterNO] in(select [PSN_RegisterNO] from [dbo].[zjs_Apply] where NoticeCode ='{0}' and ConfirmResult='通过' and PSN_RegisterNo is not null)";

                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                                //更新证书信息（发证日期=决定日期，有效期截止=决定日期+4年-1天）
                                sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[ENT_Name]=A.ENT_Name
                                                ,[zjs_Certificate].[ENT_OrganizationsCode]=A.ENT_OrganizationsCode
                                                ,[zjs_Certificate].[ENT_City]=A.ENT_City
                                                ,[zjs_Certificate].[END_Addess]=B.[END_Addess]
                                                ,[zjs_Certificate].[PSN_Name]=A.PSN_Name
                                                ,[zjs_Certificate].[PSN_Sex] =A.PSN_Sex
                                                ,[zjs_Certificate].[PSN_BirthDate]=A.Birthday
                                                ,[zjs_Certificate].[PSN_National]=A.Nation
                                                ,[zjs_Certificate].[PSN_CertificateType]=A.PSN_CertificateType
                                                ,[zjs_Certificate].[PSN_CertificateNO]=A.PSN_CertificateNO
                                                ,[zjs_Certificate].[PSN_GraduationSchool]=B.School
                                                ,[zjs_Certificate].[PSN_Specialty]=B.Major
                                                ,[zjs_Certificate].[PSN_GraduationTime]=B.GraduationTime
                                                ,[zjs_Certificate].[PSN_Qualification]=B.XueLi
                                                ,[zjs_Certificate].[PSN_Degree]=B.XueWei
                                                ,[zjs_Certificate].[PSN_MobilePhone]=B.PSN_MobilePhone
                                                ,[zjs_Certificate].[PSN_Email]=B.PSN_Email
                                                ,[zjs_Certificate].[PSN_Telephone]=B.PSN_Telephone
                                                ,[zjs_Certificate].[PSN_RegisteType]='01'
                                                ,[zjs_Certificate].[PSN_RegisterNO]=A.PSN_RegisterNo
                                                ,[zjs_Certificate].[PSN_RegisteProfession] =A.PSN_RegisteProfession
                                                ,[zjs_Certificate].[PSN_CertificationDate]=Convert(varchar(10),A.ConfirmDate,23)
                                                ,[zjs_Certificate].[PSN_CertificateValidity]=case when Convert(varchar(10),dateadd(day,-1,dateadd(year,4,A.ConfirmDate)),23) > DateAdd(YEAR, 70,A.[Birthday]) then  DateAdd(YEAR, 70,A.[Birthday]) else Convert(varchar(10),dateadd(day,-1,dateadd(year,4,A.ConfirmDate)),23) end
                                                ,[zjs_Certificate].[PSN_RegistePermissionDate]=Convert(varchar(10),A.ConfirmDate,23)
                                                ,[zjs_Certificate].[ZGZSBH]=B.PSN_ExamCertCode
                                                ,[zjs_Certificate].[XGR]=A.XGR
                                                ,[zjs_Certificate].[XGSJ]=A.XGSJ
                                                ,[zjs_Certificate].[Valid]=1                                          
                                        FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply] A
                                        on [zjs_Certificate].[PSN_RegisterNO] = A.[PSN_RegisterNO]
                                        inner join [dbo].[zjs_ApplyFirst] B
                                        on A.ApplyID=B.ApplyID
                                        where A.NoticeCode='{2}' and A.ConfirmResult='通过' and A.PSN_RegisterNo is not null";

                                CommonDAL.ExecSQL(tran, string.Format(sql, doTime, UserName, noticecode));


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
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过'  and [zjs_Apply].PSN_RegisterNo is not null
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where   [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Apply].PSN_RegisterNo is not null
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
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过'  and [zjs_Apply].PSN_RegisterNo is not null 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where   [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过'  and [zjs_Apply].PSN_RegisterNo is not null 
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
                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过'  and [zjs_Apply].PSN_RegisterNo is not null", noticecode));

                                #endregion
                                break;
                            case "延续注册":
                                #region 延续注册(批量：快)

                                //证书信息写入历史表
                                sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate]
                                    where [PSN_RegisterNO] in(select [PSN_RegisterNO] from [dbo].[zjs_Apply] where NoticeCode ='{0}' and ConfirmResult='通过')";

                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                                //更新证书信息
                                sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[PSN_CertificateValidity]=  case when DateAdd(YEAR, 0, CONVERT(varchar(100), [zjs_Certificate].[PSN_CertificateValidity], 23)) > DateAdd(YEAR, 70,[zjs_Apply].[Birthday]) 
                                                                            then  DateAdd(YEAR, 70,[zjs_Apply].[Birthday])
                                                                            else dateadd(day,-1,dateadd(year,4,[zjs_Certificate].[PSN_CertificateValidity])) end
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = [zjs_Apply].[ConfirmDate]
                                            ,[zjs_Certificate].[PSN_RegisteType] = '03'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{0}'                                     
                                            ,[zjs_Certificate].[PSN_MobilePhone]=[zjs_ApplyContinue].[PSN_MobilePhone]                                           
                                        FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply]
                                        on [zjs_Certificate].[PSN_RegisterNO] = [zjs_Apply].[PSN_RegisterNO]
                                        inner join [dbo].[zjs_ApplyContinue]
                                        on [zjs_Apply].ApplyID=[zjs_ApplyContinue].ApplyID
                                        where [zjs_Apply].NoticeCode='{2}' and [zjs_Apply].ConfirmResult='通过'";

                                CommonDAL.ExecSQL(tran, string.Format(sql, doTime, UserName, noticecode));


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
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where   [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' 
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
		                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where   [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过' 
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
                                    where  [zjs_Apply].NoticeCode='{0}' and [zjs_Apply].ConfirmResult='通过'  ", noticecode));

                                #endregion
                                break;
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        UIHelp.WriteErrorLog(Page, "造价工程师公告失败", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师公告成功", string.Format("公告批次号：{0},公告类型：{1}",  noticecode, type));
                        BindRadGridHZSB();

                        UIHelp.layerAlert(Page, "公告成功！", 6, 2000);

                    break;
                case "Cancelreport"://取消重新选择
                    try
                    {
                        zjs_ApplyDAL.DeleteUpdateNotice(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString());
                        BindRadGridHZSB();
                        UIHelp.layerAlert(Page, "取消公告成功！", 6, 2000);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "取消造价工程师公告失败", ex);
                    }
                    break;

            }
        }

    }
}