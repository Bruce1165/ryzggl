using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    //公告
    public partial class NoticeLook : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindRadGridHZSB();
            }
        }

        //绑定公告列表
        protected void BindRadGridHZSB()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("NoticeCode like'%{0}%'", RadTextBoxNoticeCode.Text));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridHZSB.CurrentPageIndex = 0;
            RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
        }

        //查询
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
            System.Text.StringBuilder zy = new System.Text.StringBuilder();//处理专业
            DateTime doTime = DateTime.Now;//处理时间
            string sql = "";
            switch (e.CommandName)
            {
                case "report"://公告
                    //开启事务
                    DBHelper db = new DBHelper();
                    DbTransaction tran = db.BeginTransaction();

                    try
                    {
                        ApplyDAL.ExeUpdateNotice(tran, EnumManager.ApplyStatus.已公告
                        , doTime
                        , UserName
                        , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString());

                        //申请类型
                        string type = RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString();
                        //公告编号
                        string noticecode = RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString();
                        switch (type)
                        {

                            case "增项注册":
                                #region 增项注册(单条循环：sql超时)

//                                DataTable zdt1 = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  NoticeCode='" + noticecode + "' and ConfirmResult='通过' ) A INNER JOIN  APPLYADDITEM B  ON A.APPLYID=B.APPLYID");
//                                //往专业表写数据
//                                for (int j = 0; j < zdt1.Rows.Count; j++)
//                                {
//                                    //把专业往历史记录表挪一次
//                                    List<COC_TOW_Register_ProfessionMDL> COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.ListGetObject(tran, zdt1.Rows[j]["PSN_ServerID"].ToString());
//                                    foreach (var c in COC_TOW_Register_ProfessionMDL)
//                                    {
//                                        COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = COC_TOW_Register_Profession_HisDAL.ListGetObject(tran, c, EnumManager.ApplyType.增项注册);
//                                        COC_TOW_Register_Profession_HisDAL.Insert(tran, _COC_TOW_Register_Profession_HisMDL);
//                                    }
//                                    //有特殊情况一次性增项两个专业，需要先判断他是否是增项两个专业
//                                    COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
//                                    _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Guid.NewGuid().ToString();
//                                    if (zdt1.Rows[j]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = zdt1.Rows[j]["PSN_ServerID"].ToString();
//                                    if (zdt1.Rows[j]["AddItem1"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = zdt1.Rows[j]["AddItem1"].ToString();
//                                    // if (zdt1.Rows[j]["ExamDate1"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = zdt1.Rows[j]["ExamDate1"] == DBNull.Value ? (DateTime?)_COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin : Convert.ToDateTime(zdt1.Rows[j]["ExamDate1"]);
//                                    //_COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = ((DateTime)_COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin).AddYears(3);
//                                    //if (zdt1.Rows[j]["ExamDate1"] != DBNull.Value)
//                                    _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(zdt1.Rows[j]["ConfirmDate"]);
//                                    _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = UIHelp.GET_PSN_CertificateValidity(Convert.ToDateTime(zdt1.Rows[j]["ConfirmDate"]).AddYears(3).Date, zdt1.Rows[j]["PSN_CertificateNO"].ToString(), zdt1.Rows[j]["PSN_Level"].ToString());
//                                    COC_TOW_Register_ProfessionDAL.Insert(tran, _COC_TOW_Register_ProfessionMDL);
//                                    if (zdt1.Rows[j]["AddItem2"].ToString() != "")
//                                    {
//                                        COC_TOW_Register_ProfessionMDL __COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
//                                        __COC_TOW_Register_ProfessionMDL.PRO_ServerID = Guid.NewGuid().ToString();
//                                        if (zdt1.Rows[j]["PSN_ServerID"] != DBNull.Value) __COC_TOW_Register_ProfessionMDL.PSN_ServerID = zdt1.Rows[j]["PSN_ServerID"].ToString();
//                                        if (zdt1.Rows[j]["AddItem2"] != DBNull.Value) __COC_TOW_Register_ProfessionMDL.PRO_Profession = zdt1.Rows[j]["AddItem2"].ToString();
//                                        //if (zdt1.Rows[j]["ExamDate2"] != DBNull.Value) 
//                                        __COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(zdt1.Rows[j]["ConfirmDate"]);
//                                        __COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = UIHelp.GET_PSN_CertificateValidity(Convert.ToDateTime(zdt1.Rows[j]["ConfirmDate"]).AddYears(3).Date, zdt1.Rows[j]["PSN_CertificateNO"].ToString(), zdt1.Rows[j]["PSN_Level"].ToString());
//                                        COC_TOW_Register_ProfessionDAL.Insert(tran, __COC_TOW_Register_ProfessionMDL);
//                                    }
//                                    //往历史记录表备份数据,根据建造师ID拿到去正式表拿到一个对象
//                                    COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(tran, zdt1.Rows[j]["PSN_ServerID"].ToString());
//                                    //先往历史纪录表导入数据
//                                    if (_COC_TOW_Person_BaseInfoMDL != null)
//                                    {
//                                        //正式表往记录表写数，右边是一个方法，根据建造师ID拿到一个记录表的信息
//                                        COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran, _COC_TOW_Person_BaseInfoMDL);
//                                        COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
//                                    }
//                                    //修改正式表的数据
//                                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession.ToString() + ',' + zdt1.Rows[j]["AddItem1"].ToString();
//                                    _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = zdt1.Rows[j]["AddItem1"].ToString();
//                                    if (zdt1.Rows[j]["AddItem2"].ToString() != "")
//                                    {
//                                        _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession + ',' + zdt1.Rows[j]["AddItem2"].ToString();
//                                        _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession + ',' + zdt1.Rows[j]["AddItem2"].ToString();
//                                    }
//                                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = UIHelp.GET_PSN_CertificateValidity(Convert.ToDateTime(zdt1.Rows[j]["ConfirmDate"]).AddYears(3).Date, zdt1.Rows[j]["PSN_CertificateNO"].ToString(), zdt1.Rows[j]["PSN_Level"].ToString());
//                                    // if (zdt1.Rows[j]["PSN_CertificateValidity1"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(zdt1.Rows[j]["PSN_CertificateValidity1"]);
//                                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = "04";
//                                    //注册审批日期
//                                    _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = doTime;
//                                    _COC_TOW_Person_BaseInfoMDL.XGSJ = DateTime.Now;
//                                    //Update正式表信息
//                                    COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);

//                                    //更新证书附件中需要被覆盖的附件为历史附件
//                                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
//                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
//                                    from [dbo].[COC_TOW_Person_File]
//                                    inner join 
//                                    (
//	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
//	                                    from 
//	                                    (
//		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[ApplyFile]
//		                                    on [FileInfo].FileID = [ApplyFile].FileID
//		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
//		                                    where [Apply].ApplyID='{0}' 
//	                                    ) a
//	                                    inner join 
//	                                    (
//		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[COC_TOW_Person_File]
//		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
//                                            inner join [dbo].[Apply] 
//                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
//		                                    where  [Apply].ApplyID='{0}' 
//	                                    ) b 
//	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
//                                    ) t
//                                    on [COC_TOW_Person_File].FileID = t.FileID", zdt1.Rows[j]["ApplyID"]));


//                                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    delete from [dbo].[COC_TOW_Person_File]
//                                    where FileID in( select [COC_TOW_Person_File].[FileID]
//                                    from [dbo].[COC_TOW_Person_File]
//                                    inner join 
//                                    (
//	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
//	                                    from 
//	                                    (
//		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[ApplyFile]
//		                                    on [FileInfo].FileID = [ApplyFile].FileID
//		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
//		                                    where [Apply].ApplyID='{0}' 
//	                                    ) a
//	                                    inner join 
//	                                    (
//		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
//		                                    from [dbo].[FileInfo]
//		                                    inner join [dbo].[COC_TOW_Person_File]
//		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
//                                            inner join [dbo].[Apply] 
//                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
//		                                    where  [Apply].ApplyID='{0}' 
//	                                    ) b 
//	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
//                                    ) t
//                                    on [COC_TOW_Person_File].FileID = t.FileID
//                                    )", zdt1.Rows[j]["ApplyID"]));

//                                    //将申请单附件写入证书附件库
//                                    CommonDAL.ExecSQL(tran, string.Format(@"
//                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
//                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
//                                    from [dbo].[ApplyFile]
//                                    inner join [dbo].[Apply] 
//                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
//                                    where [Apply].ApplyID='{0}' ", zdt1.Rows[j]["ApplyID"]));

//                                }

                                #endregion

                                #region (批量：快)
                                //专业写入历史表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 	
                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";

                                CommonDAL.ExecSQL(tran, string.Format(sql, "增项注册", doTime, noticecode));

                                //删除过期的增项专业
                                sql = @"DELETE FROM [dbo].[COC_TOW_Register_Profession]
                                WHERE [PRO_ServerID] in
                                (
                                    SELECT  p.[PRO_ServerID]
                                    FROM [dbo].[Apply] a 
                                    inner join [dbo].[ApplyAddItem] b on a.ApplyID = b.ApplyID
                                    inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID and (p.PRO_Profession = b.[AddItem1] or p.PRO_Profession = b.[AddItem2])
                                    where  a.NoticeCode='{0}' and a.ConfirmResult='通过'
                                )";

                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                                //写入增项专业表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code],[LastModifyTime])
                                  select newid(),a.PSN_ServerID,ZY,convert(char(10),a.[ConfirmDate],120),[dbo].[GET_PSN_CertificateValidity](convert(char(10),dateadd(day,-1,dateadd(year,3,a.[ConfirmDate])),120),a.PSN_CertificateNO,a.PSN_Level),'110000','{1}'
                                  FROM [dbo].[Apply] a 
                                  inner join
                                  (
                                      select applyid,[AddItem1] ZY from  [dbo].[ApplyAddItem] where [AddItem1] is not null
                                      union
                                      select applyid,[AddItem2] ZY from  [dbo].[ApplyAddItem] where [AddItem2] is not null
                                  ) z on  a.applyid = z.applyid
                                   where a.NoticeCode='{0}' and a.ConfirmResult='通过'";
                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode, doTime));

                                //人员表写入历史
                                sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where NoticeCode='{0}' and ConfirmResult='通过')";

                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                                //更新人员正式表
                                sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set 
                                    [COC_TOW_Person_BaseInfo].PSN_RegisteProfession =
		                                    replace(','+
		                                    (
		                                    select ',' +[COC_TOW_Register_Profession].PRO_Profession from [dbo].[COC_TOW_Register_Profession]
		                                     where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                     for xml path('')
		                                    ),',,','')
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
		                                    (
		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                    )               
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = a.[ConfirmDate]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='04'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_AddProfession]=                                   
		                                    (
		                                        select [ApplyAddItem].AddItem1 + case when [ApplyAddItem].AddItem2 is null then '' else ',' +[ApplyAddItem].AddItem2 end
                                                from [dbo].[ApplyAddItem]
		                                        where [ApplyAddItem].Applyid = a.Applyid		                                   
		                                    )
                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";


                                CommonDAL.ExecSQL(tran, string.Format(sql
                                    , doTime
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
                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' ", noticecode));

                                #endregion
                                break;
                            case "延期注册":
                                #region 延续注册(单条循环：sql超时)

                                //                                DataTable yxdt1 = CommonDAL.GetDataTable(tran, @"SELECT * FROM (SELECT * FROM APPLY WHERE  NoticeCode='" + noticecode + "' and ConfirmResult='通过' ) A INNER JOIN  ApplyContinue B  ON A.APPLYID=B.APPLYID");
                                //                                //修改专业表写数据
                                //                                for (int j = 0; j < yxdt1.Rows.Count; j++)
                                //                                {
                                //                                    //把专业往历史记录表挪一次
                                //                                    List<COC_TOW_Register_ProfessionMDL> COC_TOW_Register_ProfessionMDL = COC_TOW_Register_ProfessionDAL.ListGetObject(tran, yxdt1.Rows[j]["PSN_ServerID"].ToString());
                                //                                    foreach (var c in COC_TOW_Register_ProfessionMDL)
                                //                                    {
                                //                                        COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = COC_TOW_Register_Profession_HisDAL.ListGetObject(tran, c, EnumManager.ApplyType.延期注册);
                                //                                        COC_TOW_Register_Profession_HisDAL.Insert(tran, _COC_TOW_Register_Profession_HisMDL);
                                //                                    }



                                //                                    //根据建造师ID获取人员信息
                                //                                    COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObject(tran, yxdt1.Rows[j]["PSN_ServerID"].ToString());

                                //                                    //续期最小有效期
                                //                                    DateTime minValid = _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity.Value;

                                //                                    ////获取专业信息
                                //                                    //DataTable yxdt2 = CommonDAL.GetDataTable(@"SELECT * FROM COC_TOW_Register_Profession WHERE PSN_ServerID='" + yxdt1.Rows[j]["PSN_ServerID"] + "'");

                                //                                    //续期专业统一改写为最小续期专业有效期 + 3年
                                //                                    zy.Remove(0, zy.Length);
                                //                                    if (yxdt1.Rows[j]["PSN_RegisteProfession1"].ToString() != "" && (bool)yxdt1.Rows[j]["IfContinue1"] == true)
                                //                                    {
                                //                                        if (yxdt1.Rows[j]["PSN_CertificateValidity1"] != DBNull.Value && Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity1"]) < minValid)
                                //                                        {
                                //                                            minValid = Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity1"]);
                                //                                        }

                                //                                        zy.Append(",").Append(yxdt1.Rows[j]["PSN_RegisteProfession1"].ToString());
                                //                                    }
                                //                                    if (yxdt1.Rows[j]["PSN_RegisteProfession2"].ToString() != "" && (bool)yxdt1.Rows[j]["IfContinue2"] == true)
                                //                                    {
                                //                                        if (yxdt1.Rows[j]["PSN_CertificateValidity2"] != DBNull.Value && Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity2"]) < minValid)
                                //                                        {
                                //                                            minValid = Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity2"]);
                                //                                        }

                                //                                        zy.Append(",").Append(yxdt1.Rows[j]["PSN_RegisteProfession2"].ToString());
                                //                                    }
                                //                                    if (yxdt1.Rows[j]["PSN_RegisteProfession3"].ToString() != "" && (bool)yxdt1.Rows[j]["IfContinue3"] == true)
                                //                                    {
                                //                                        if (yxdt1.Rows[j]["PSN_CertificateValidity2"] != DBNull.Value && Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity3"]) < minValid)
                                //                                        {
                                //                                            minValid = Convert.ToDateTime(yxdt1.Rows[j]["PSN_CertificateValidity3"]);
                                //                                        }

                                //                                        zy.Append(",").Append(yxdt1.Rows[j]["PSN_RegisteProfession3"].ToString());
                                //                                    }
                                //                                    if (zy.Length > 0)
                                //                                    {
                                //                                        CommonDAL.ExecSQL(tran, string.Format("UPDATE COC_TOW_Register_Profession SET PRO_ValidityEnd='{0}' ,[LastModifyTime]='{3}' WHERE  PSN_ServerID='{1}' AND PRO_ValidityEnd >='{2}'", UIHelp.GET_PSN_CertificateValidity(minValid.AddYears(3), yxdt1.Rows[j]["PSN_CertificateNO"].ToString(), yxdt1.Rows[j]["PSN_Level"].ToString()), yxdt1.Rows[j]["PSN_ServerID"], minValid, doTime.ToString("yyyy-MM-dd HH:mm:ss")));

                                //                                        zy.Remove(0, 1);
                                //                                    }


                                //                                    //先往历史纪录表导入数据
                                //                                    if (_COC_TOW_Person_BaseInfoMDL != null)
                                //                                    {
                                //                                        //正式表往记录表写数，右边是一个方法，根据建造师ID拿到一个记录表的信息
                                //                                        COC_TOW_Person_BaseInfo_HisMDL __COC_TOW_Person_BaseInfo_HisMDL = COC_TOW_Person_BaseInfo_HisDAL._COC_TOW_Person_BaseInfo_HisMDL(tran, _COC_TOW_Person_BaseInfoMDL);
                                //                                        COC_TOW_Person_BaseInfo_HisDAL.Insert(tran, __COC_TOW_Person_BaseInfo_HisMDL);
                                //                                    }

                                //                                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = UIHelp.GET_PSN_CertificateValidity(minValid.AddYears(3), yxdt1.Rows[j]["PSN_CertificateNO"].ToString(), yxdt1.Rows[j]["PSN_Level"].ToString());
                                //                                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = "03";
                                //                                    _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = zy.ToString();//续期专业，用于打印
                                //                                    //注册审批日期
                                //                                    _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = doTime;
                                //                                    _COC_TOW_Person_BaseInfoMDL.XGR = UserName;
                                //                                    _COC_TOW_Person_BaseInfoMDL.XGSJ = doTime;
                                //                                    //Update正式表信息
                                //                                    COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);

                                //                                    //更新证书附件中需要被覆盖的附件为历史附件
                                //                                    CommonDAL.ExecSQL(tran, string.Format(@"
                                //                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                //                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                //                                    from [dbo].[COC_TOW_Person_File]
                                //                                    inner join 
                                //                                    (
                                //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                                //	                                    from 
                                //	                                    (
                                //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
                                //		                                    from [dbo].[FileInfo]
                                //		                                    inner join [dbo].[ApplyFile]
                                //		                                    on [FileInfo].FileID = [ApplyFile].FileID
                                //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
                                //		                                    where [Apply].ApplyID='{0}' 
                                //	                                    ) a
                                //	                                    inner join 
                                //	                                    (
                                //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                                //		                                    from [dbo].[FileInfo]
                                //		                                    inner join [dbo].[COC_TOW_Person_File]
                                //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                //                                            inner join [dbo].[Apply] 
                                //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
                                //		                                    where  [Apply].ApplyID='{0}' 
                                //	                                    ) b 
                                //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                //                                    ) t
                                //                                    on [COC_TOW_Person_File].FileID = t.FileID", yxdt1.Rows[j]["ApplyID"]));


                                //                                    CommonDAL.ExecSQL(tran, string.Format(@"
                                //                                    delete from [dbo].[COC_TOW_Person_File]
                                //                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                //                                    from [dbo].[COC_TOW_Person_File]
                                //                                    inner join 
                                //                                    (
                                //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                                //	                                    from 
                                //	                                    (
                                //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
                                //		                                    from [dbo].[FileInfo]
                                //		                                    inner join [dbo].[ApplyFile]
                                //		                                    on [FileInfo].FileID = [ApplyFile].FileID
                                //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
                                //		                                    where [Apply].ApplyID='{0}' 
                                //	                                    ) a
                                //	                                    inner join 
                                //	                                    (
                                //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                                //		                                    from [dbo].[FileInfo]
                                //		                                    inner join [dbo].[COC_TOW_Person_File]
                                //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                //                                            inner join [dbo].[Apply] 
                                //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
                                //		                                    where  [Apply].ApplyID='{0}' 
                                //	                                    ) b 
                                //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                //                                    ) t
                                //                                    on [COC_TOW_Person_File].FileID = t.FileID
                                //                                    )", yxdt1.Rows[j]["ApplyID"]));

                                //                                    //将申请单附件写入证书附件库
                                //                                    CommonDAL.ExecSQL(tran, string.Format(@"
                                //                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                //                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                //                                    from [dbo].[ApplyFile]
                                //                                    inner join [dbo].[Apply] 
                                //                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                //                                    where [Apply].ApplyID='{0}' ", yxdt1.Rows[j]["ApplyID"]));


                                //                                }

                                #endregion

                                #region 延续注册(批量：快)

                                //专业写入历史表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 	
                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";

                                CommonDAL.ExecSQL(tran, string.Format(sql, "延期注册", doTime, noticecode));

                                //更新专业表有效期（统一更新为申请续期专业最小有效期+3年）
                                sql = @"update [dbo].[COC_TOW_Register_Profession]
                                set [COC_TOW_Register_Profession].[PRO_ValidityEnd] = case when t.minValid is null then [COC_TOW_Register_Profession].[PRO_ValidityEnd] else [dbo].[GET_PSN_CertificateValidity](dateadd(day,-1,dateadd(year,3,t.minValid)),t.PSN_CertificateNO,t.PSN_Level) end
                                ,[LastModifyTime]='{1}'
                                FROM 
                                (
	                                select  a.ApplyID ,a.PSN_ServerID,a.NoticeCode,a.ConfirmResult,c.[PSN_RegisteProfession1],c.[PSN_RegisteProfession2],c.[PSN_RegisteProfession3],a.PSN_CertificateNO,a.PSN_Level
                                    ,minValid=
                                     case when isnull(c.PSN_CertificateValidity3,'2050-1-1') <
                                     (
	                                     case when isnull(c.PSN_CertificateValidity1,'2050-1-1') < isnull(c.PSN_CertificateValidity2,'2050-1-1') 
	                                     then c.PSN_CertificateValidity1
	                                     else c.PSN_CertificateValidity2 end
                                     )
                                     then c.PSN_CertificateValidity3 else 
                                     (
	                                    case  when isnull(c.PSN_CertificateValidity1,'2050-1-1') < isnull(c.PSN_CertificateValidity2,'2050-1-1') 
	                                    then c.PSN_CertificateValidity1
	                                    else c.PSN_CertificateValidity2 end
                                      )
                                     end 
	                                from [dbo].[Apply] a 
	                                inner join [dbo].[ApplyContinue] c 
	                                on a.NoticeCode ='{0}' and a.ConfirmResult='通过' and a.applyid =c.applyid
                                ) t
                                inner join [dbo].[COC_TOW_Register_Profession] 
                                on t.PSN_ServerID = [COC_TOW_Register_Profession].PSN_ServerID
                                and 
                                (
	                                t.[PSN_RegisteProfession1] = [COC_TOW_Register_Profession].[PRO_Profession] 
	                                or t.[PSN_RegisteProfession2] = [COC_TOW_Register_Profession].[PRO_Profession] 
	                                or t.[PSN_RegisteProfession3] = [COC_TOW_Register_Profession].[PRO_Profession]
                                ) 
                                where t.NoticeCode ='{0}' and t.ConfirmResult='通过'";
                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode, doTime.ToString("yyyy-MM-dd HH:mm:ss")));

                                //人员表写入历史
                                sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                              SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                              FROM [dbo].[COC_TOW_Person_BaseInfo] 
                              where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where NoticeCode ='{0}' and ConfirmResult='通过')";

                                CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

                                //更新人员正式表
                                sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set [COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
		                                    (
		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                    )               
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = a.[ConfirmDate]
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='03'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RenewalProfession]=a.PSN_RegisteProfession
                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                where a.NoticeCode ='{2}' and a.ConfirmResult='通过'";
                                CommonDAL.ExecSQL(tran, string.Format(sql
                                    , doTime
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
		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where   [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
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
		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where   [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
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
                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'  ", noticecode));

                                #endregion
                                break;
                        }

                        tran.Commit();

                        UIHelp.WriteOperateLog(UserName, UserID, "公告成功", string.Format("公告时间：{0},公告批次号：{1},公告类型：{2}", DateTime.Now, noticecode, type));
                        BindRadGridHZSB();

                        UIHelp.layerAlert(Page, "公告成功！", 6, 2000);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        UIHelp.WriteErrorLog(Page, "公告或者往申请表往专业表写入数据失败", ex);
                        return;
                    }

                    break;
                case "Cancelreport"://取消重新选择
                    try
                    {
                        ApplyDAL.DeleteUpdateNotice(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString());
                        BindRadGridHZSB();
                        UIHelp.layerAlert(Page, "取消成功！", 6, 2000);

                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "取消失败", ex);
                    }
                    break;

            }
        }

    }
}