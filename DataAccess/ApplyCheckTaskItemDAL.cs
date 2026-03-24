using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyCheckTaskItemDAL(填写类描述)
	/// </summary>
    public class ApplyCheckTaskItemDAL
    {
        public ApplyCheckTaskItemDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
		    return Insert(null,_ApplyCheckTaskItemMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime,CheckMan,CheckTime,CheckResult,CheckResultDesc,ReCheckMan,ReCheckTime)
			VALUES (@TaskID,@BusTypeID,@ApplyType,@ApplyTableName,@DataID,@WorkerName,@IDCard,@IDCardType,@CertificateCode,@ApplyFinishTime,@CheckMan,@CheckTime,@CheckResult,@CheckResultDesc,@ReCheckMan,@ReCheckTime);SELECT @TaskItemID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("TaskItemID", DbType.Int64));
			p.Add(db.CreateParameter("TaskID",DbType.Int64, _ApplyCheckTaskItemMDL.TaskID));
			p.Add(db.CreateParameter("BusTypeID",DbType.Int32, _ApplyCheckTaskItemMDL.BusTypeID));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _ApplyCheckTaskItemMDL.ApplyType));
			p.Add(db.CreateParameter("ApplyTableName",DbType.String, _ApplyCheckTaskItemMDL.ApplyTableName));
			p.Add(db.CreateParameter("DataID",DbType.String, _ApplyCheckTaskItemMDL.DataID));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _ApplyCheckTaskItemMDL.WorkerName));
			p.Add(db.CreateParameter("IDCard",DbType.String, _ApplyCheckTaskItemMDL.IDCard));
			p.Add(db.CreateParameter("IDCardType",DbType.String, _ApplyCheckTaskItemMDL.IDCardType));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ApplyCheckTaskItemMDL.CertificateCode));
			p.Add(db.CreateParameter("ApplyFinishTime",DbType.DateTime, _ApplyCheckTaskItemMDL.ApplyFinishTime));
			p.Add(db.CreateParameter("CheckMan",DbType.String, _ApplyCheckTaskItemMDL.CheckMan));
			p.Add(db.CreateParameter("CheckTime",DbType.DateTime, _ApplyCheckTaskItemMDL.CheckTime));
			p.Add(db.CreateParameter("CheckResult",DbType.String, _ApplyCheckTaskItemMDL.CheckResult));
			p.Add(db.CreateParameter("CheckResultDesc",DbType.String, _ApplyCheckTaskItemMDL.CheckResultDesc));
			p.Add(db.CreateParameter("ReCheckMan",DbType.String, _ApplyCheckTaskItemMDL.ReCheckMan));
			p.Add(db.CreateParameter("ReCheckTime",DbType.DateTime, _ApplyCheckTaskItemMDL.ReCheckTime));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ApplyCheckTaskItemMDL.TaskItemID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
			return Update(null,_ApplyCheckTaskItemMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyCheckTaskItem
				SET	TaskID = @TaskID,BusTypeID = @BusTypeID,ApplyType = @ApplyType,ApplyTableName = @ApplyTableName,DataID = @DataID,WorkerName = @WorkerName,IDCard = @IDCard,IDCardType = @IDCardType,CertificateCode = @CertificateCode,ApplyFinishTime = @ApplyFinishTime,CheckMan = @CheckMan,CheckTime = @CheckTime,CheckResult = @CheckResult,CheckResultDesc = @CheckResultDesc,ReCheckMan = @ReCheckMan,ReCheckTime = @ReCheckTime
			WHERE
				TaskItemID = @TaskItemID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskItemID",DbType.Int64, _ApplyCheckTaskItemMDL.TaskItemID));
			p.Add(db.CreateParameter("TaskID",DbType.Int64, _ApplyCheckTaskItemMDL.TaskID));
			p.Add(db.CreateParameter("BusTypeID",DbType.Int32, _ApplyCheckTaskItemMDL.BusTypeID));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _ApplyCheckTaskItemMDL.ApplyType));
			p.Add(db.CreateParameter("ApplyTableName",DbType.String, _ApplyCheckTaskItemMDL.ApplyTableName));
			p.Add(db.CreateParameter("DataID",DbType.String, _ApplyCheckTaskItemMDL.DataID));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _ApplyCheckTaskItemMDL.WorkerName));
			p.Add(db.CreateParameter("IDCard",DbType.String, _ApplyCheckTaskItemMDL.IDCard));
			p.Add(db.CreateParameter("IDCardType",DbType.String, _ApplyCheckTaskItemMDL.IDCardType));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ApplyCheckTaskItemMDL.CertificateCode));
			p.Add(db.CreateParameter("ApplyFinishTime",DbType.DateTime, _ApplyCheckTaskItemMDL.ApplyFinishTime));
			p.Add(db.CreateParameter("CheckMan",DbType.String, _ApplyCheckTaskItemMDL.CheckMan));
			p.Add(db.CreateParameter("CheckTime",DbType.DateTime, _ApplyCheckTaskItemMDL.CheckTime));
			p.Add(db.CreateParameter("CheckResult",DbType.String, _ApplyCheckTaskItemMDL.CheckResult));
			p.Add(db.CreateParameter("CheckResultDesc",DbType.String, _ApplyCheckTaskItemMDL.CheckResultDesc));
			p.Add(db.CreateParameter("ReCheckMan",DbType.String, _ApplyCheckTaskItemMDL.ReCheckMan));
			p.Add(db.CreateParameter("ReCheckTime",DbType.DateTime, _ApplyCheckTaskItemMDL.ReCheckTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="TaskItemID">主键</param>
		/// <returns></returns>
        public static int Delete( long TaskItemID )
		{
			return Delete(null, TaskItemID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyCheckTaskItemID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long TaskItemID)
		{
			string sql=@"DELETE FROM dbo.ApplyCheckTaskItem WHERE TaskItemID = @TaskItemID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskItemID",DbType.Int64,TaskItemID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
			return Delete(null,_ApplyCheckTaskItemMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyCheckTaskItem WHERE TaskItemID = @TaskItemID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskItemID",DbType.Int64,_ApplyCheckTaskItemMDL.TaskItemID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="TaskID">任务ID</param>
        /// <returns></returns>
        public static int DeleteByTaskID(DbTransaction tran, long TaskID)
        {
            string sql = @"DELETE FROM dbo.ApplyCheckTaskItem WHERE TaskID = @TaskID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("TaskID", DbType.Int64, TaskID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyCheckTaskItemID">主键</param>
        public static ApplyCheckTaskItemMDL GetObject( long TaskItemID )
		{
			string sql=@"
			SELECT TaskItemID,TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime,CheckMan,CheckTime,CheckResult,CheckResultDesc,ReCheckMan,ReCheckTime
			FROM dbo.ApplyCheckTaskItem
			WHERE TaskItemID = @TaskItemID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskItemID",DbType.Int64,TaskItemID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyCheckTaskItemMDL _ApplyCheckTaskItemMDL = null;
                if (reader.Read())
                {
                    _ApplyCheckTaskItemMDL = new ApplyCheckTaskItemMDL();
					if (reader["TaskItemID"] != DBNull.Value) _ApplyCheckTaskItemMDL.TaskItemID = Convert.ToInt64(reader["TaskItemID"]);
					if (reader["TaskID"] != DBNull.Value) _ApplyCheckTaskItemMDL.TaskID = Convert.ToInt64(reader["TaskID"]);
					if (reader["BusTypeID"] != DBNull.Value) _ApplyCheckTaskItemMDL.BusTypeID = Convert.ToInt32(reader["BusTypeID"]);
					if (reader["ApplyType"] != DBNull.Value) _ApplyCheckTaskItemMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
					if (reader["ApplyTableName"] != DBNull.Value) _ApplyCheckTaskItemMDL.ApplyTableName = Convert.ToString(reader["ApplyTableName"]);
					if (reader["DataID"] != DBNull.Value) _ApplyCheckTaskItemMDL.DataID = Convert.ToString(reader["DataID"]);
					if (reader["WorkerName"] != DBNull.Value) _ApplyCheckTaskItemMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["IDCard"] != DBNull.Value) _ApplyCheckTaskItemMDL.IDCard = Convert.ToString(reader["IDCard"]);
					if (reader["IDCardType"] != DBNull.Value) _ApplyCheckTaskItemMDL.IDCardType = Convert.ToString(reader["IDCardType"]);
					if (reader["CertificateCode"] != DBNull.Value) _ApplyCheckTaskItemMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["ApplyFinishTime"] != DBNull.Value) _ApplyCheckTaskItemMDL.ApplyFinishTime = Convert.ToDateTime(reader["ApplyFinishTime"]);
					if (reader["CheckMan"] != DBNull.Value) _ApplyCheckTaskItemMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
					if (reader["CheckTime"] != DBNull.Value) _ApplyCheckTaskItemMDL.CheckTime = Convert.ToDateTime(reader["CheckTime"]);
					if (reader["CheckResult"] != DBNull.Value) _ApplyCheckTaskItemMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
					if (reader["CheckResultDesc"] != DBNull.Value) _ApplyCheckTaskItemMDL.CheckResultDesc = Convert.ToString(reader["CheckResultDesc"]);
					if (reader["ReCheckMan"] != DBNull.Value) _ApplyCheckTaskItemMDL.ReCheckMan = Convert.ToString(reader["ReCheckMan"]);
					if (reader["ReCheckTime"] != DBNull.Value) _ApplyCheckTaskItemMDL.ReCheckTime = Convert.ToDateTime(reader["ReCheckTime"]);
                }
				reader.Close();
                db.Close();
                return _ApplyCheckTaskItemMDL;
            }
		}
		/// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyCheckTaskItem", "*", filterWhereString, orderBy == "" ? " TaskItemID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyCheckTaskItem", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 批量审批抽查申请单
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskMDL">抽查任务设置对象</param>
        /// <returns></returns>
        public static int InsertPatch(DbTransaction tran, ApplyCheckTaskMDL _ApplyCheckTaskMDL)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sbCount = "";
            DateTime start = _ApplyCheckTaskMDL.BusStartDate.Value;//业务起始时间
            DateTime end = _ApplyCheckTaskMDL.BusEndDate.Value;//业务截止时间
            int dataCount = 0;//待抽数据量
            int top=0;

            foreach (string s in _ApplyCheckTaskMDL.BusRangeCode.Split(','))
            {
                switch (s)
                {
                    case "1"://二建
                        sbCount=string.Format(@"
	                            SELECT count(*) dataCount
	                            FROM [dbo].[Apply] 
	                            where (ApplyType ='初始注册' or ApplyType ='重新注册' or ApplyType ='增项注册' or  ApplyType ='延期注册')  and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null
	                            and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                        //总数据量
                        dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (                
	                                SELECT  
		                                BusTypeID = 1
		                                ,ApplyType
		                                ,'Apply' ApplyTableName
		                                ,[ApplyID] DataID
		                                ,[PSN_Name] as WORKERNAME
		                                ,[PSN_CertificateNO] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,[PSN_RegisterNo] as CERTIFICATECODE
		                                ,[ConfirmDate]  ApplyFinishTime 
		                                ,CAST((RAND() * 100000000) AS bigint) randID
	                                FROM [dbo].[Apply] 
	                                where (ApplyType ='初始注册' or ApplyType ='重新注册' or ApplyType ='增项注册' or  ApplyType ='延期注册')  and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null
	                                and [ConfirmDate] between '{2}' and '{3}'
                                ) t order by randID;"
                    ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
                        break;
                    case "2"://二造
                        sbCount=string.Format(@"
	                            SELECT count(*) dataCount
	                            FROM [dbo].[zjs_Apply] 
	                            where  (ApplyType ='初始注册' or ApplyType ='延期注册') and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null 
                                and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));

                         dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (   
	                                SELECT  
		                                BusTypeID = 2
		                                ,ApplyType
		                                ,'zjs_Apply' ApplyTableName
		                                ,[ApplyID] DataID
		                                ,[PSN_Name] as WORKERNAME
		                                ,[PSN_CertificateNO] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,[PSN_RegisterNo] as CERTIFICATECODE
		                                ,[ConfirmDate]  ApplyFinishTime 
		                                ,CAST((RAND() * 100000000) AS bigint) randID
	                                FROM [dbo].[zjs_Apply] 
	                                where  (ApplyType ='初始注册' or ApplyType ='延期注册') and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null 
                                    and [ConfirmDate] between '{2}' and '{3}'
                                ) t order by randID;"
                    , top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
                        break;
                    case "3"://安管人员
                        //考试报名
                        sbCount=string.Format(@"
	                                SELECT count(*) dataCount
                                    FROM [dbo].[EXAMSIGNUP] s
                                    inner join examplan e on s.examplanid = e.examplanid
	                                where e.PostTypeID < 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null"
                            , start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                        dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (  
	                                SELECT 
		                                BusTypeID = 3 ,
		                                '考试报名' ApplyType,
		                                'EXAMSIGNUP' ApplyTableName,
		                                cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
		                                ,s.WORKERNAME
		                                ,s.[CERTIFICATECODE] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
		                                ,s.[PAYCONFIRMDATE]  ApplyFinishTime
		                                ,CAST((RAND() * 100000000) AS bigint) randID
                                    FROM [dbo].[EXAMSIGNUP] s
                                    inner join examplan e on s.examplanid = e.examplanid
	                                where e.PostTypeID < 2 and s.[PAYCONFIRMDATE] between '{2}' and '{3}' and s.[RESULTCERTIFICATECODE] is not null                                     
                                ) t order by randID;"
                            ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

                        //证书进京
                        sbCount=string.Format(@"                                  
                                    SELECT count(*) dataCount     
                                    FROM [dbo].[VIEW_CERTIFICATE_ENTER] 
	                                where [CONFRIMDATE] between '{0}' and '{1}' and [NEWCERTIFICATECODE] is not null "
                            , start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                        dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (  	                                
                                    SELECT BusTypeID =3
		                                ,'证书进京' ApplyType
		                                ,'CERTIFICATE_ENTER' ApplyTableName
		                                ,cast(ApplyID as varchar(64)) DataID
		                                ,WORKERNAME
		                                ,[WORKERCERTIFICATECODE] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,[NEWCERTIFICATECODE] as CERTIFICATECODE
		                                ,[CONFRIMDATE]  ApplyFinishTime 
		                                ,CAST((RAND() * 100000000) AS bigint) randID       
                                    FROM [dbo].[VIEW_CERTIFICATE_ENTER] 
	                                where [CONFRIMDATE] between '{2}' and '{3}' and [NEWCERTIFICATECODE] is not null                                   
                                ) t order by randID;"
                            ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

                        //证书续期
                        sbCount=string.Format(@"                                
                                    SELECT count(*) dataCount
                                    FROM [dbo].[CERTIFICATECONTINUE] x
                                    inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
	                                where c.PostTypeID < 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过' "
                            , start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                        dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                ( 	                                
                                    SELECT  
		                                BusTypeID = 3 
		                                ,'证书续期' ApplyType
		                                ,'CERTIFICATECONTINUE' ApplyTableName
		                                ,cast(x.CERTIFICATECONTINUEID as varchar(64)) DataID
		                                ,c.WORKERNAME
		                                ,c.[WORKERCERTIFICATECODE] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,c.CERTIFICATECODE
		                                ,x.[CONFIRMDATE]  ApplyFinishTime 
		                                ,CAST((RAND() * 100000000) AS bigint) randID
                                    FROM [dbo].[CERTIFICATECONTINUE] x
                                    inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
	                                where c.PostTypeID < 2 and x.[CONFIRMDATE] between '{2}' and '{3}' and x.[CONFIRMRESULT] ='决定通过' 
                                ) t order by randID;"
                            ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
                        break;
                    case "4"://特种作业
                        //考试报名
                        sbCount=string.Format(@"                              
                                   SELECT count(*) dataCount
                                    FROM [dbo].[EXAMSIGNUP] s
                                    inner join examplan e on s.examplanid = e.examplanid
	                                where e.PostTypeID = 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null "
                            , start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                         dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (  
                                   SELECT 
		                                BusTypeID = 4 ,
		                                '考试报名' ApplyType,
		                                'EXAMSIGNUP' ApplyTableName,
		                                cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
		                                ,s.WORKERNAME
		                                ,s.[CERTIFICATECODE] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
		                                ,s.[PAYCONFIRMDATE]  ApplyFinishTime
		                                ,CAST((RAND() * 100000000) AS bigint) randID
                                    FROM [dbo].[EXAMSIGNUP] s
                                    inner join examplan e on s.examplanid = e.examplanid
	                                where e.PostTypeID = 2 and s.[PAYCONFIRMDATE] between '{2}' and '{3}' and s.[RESULTCERTIFICATECODE] is not null                                   
                                ) t order by randID;"
                            ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

                        //证书续期
                        sbCount=string.Format(@"                               
                                    SELECT count(*) dataCount
                                    FROM [dbo].[CERTIFICATECONTINUE] x
                                    inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
	                                where c.PostTypeID = 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过'"
                            , start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59"));
                         dataCount = (int)CommonDAL.GetObject(sbCount);
                        top = dataCount * _ApplyCheckTaskMDL.CheckPer.Value / 1000;
                        if( top < 1)
                        {
                            top = 1;
                        }
                        sb.AppendFormat(string.Format(@"
                                INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
			                    select  top({0}) {1},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
                                (                                     
                                    SELECT  
		                                BusTypeID = 4 
		                                ,'证书续期' ApplyType
		                                ,'CERTIFICATECONTINUE' ApplyTableName
		                                ,cast(x.CERTIFICATECONTINUEID as varchar(64)) DataID
		                                ,c.WORKERNAME
		                                ,c.[WORKERCERTIFICATECODE] as [IDCard]
		                                ,'身份证' as IDCardType
		                                ,c.CERTIFICATECODE
		                                ,x.[CONFIRMDATE]  ApplyFinishTime 
		                                ,CAST((RAND() * 100000000) AS bigint) randID
                                    FROM [dbo].[CERTIFICATECONTINUE] x
                                    inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
	                                where c.PostTypeID = 2 and x.[CONFIRMDATE] between '{2}' and '{3}' and x.[CONFIRMRESULT] ='决定通过' 
                                ) t order by randID;"
                            ,top, _ApplyCheckTaskMDL.TaskID, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
                        break;
                    default:
                        break;
                }
            }

            DBHelper db = new DBHelper();         

            //Utility.FileLog.WriteLog(string.Format(sql, dataCount, _ApplyCheckTaskMDL.CheckPer, _ApplyCheckTaskMDL.TaskID, sb.Remove(0, 9)));

            return db.GetExcuteNonQuery(tran,  sb.ToString());
        }

        /// <summary>
        /// 批量审批抽查申请单
        /// </summary>
        /// <param name="CheckMan">审核人</param>
        /// <param name="CheckResult">审查结果</param>
        /// <param name="CheckResultDesc">审查结果说明</param>
        /// <param name="filterWhereString">数据过滤条件</param>
        /// <returns></returns>
        public static int BatCheck(string CheckMan, string CheckResult, string CheckResultDesc, string filterWhereString)
        {
            string sql = string.Format(@"UPDATE dbo.ApplyCheckTaskItem 
                                        SET [CheckMan] =@CheckMan,[CheckTime] = getdate(),[CheckResult] = @CheckResult,[CheckResultDesc] =@CheckResultDesc
                                        WHERE 2 > 1 {0}" ,filterWhereString);
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CheckMan", DbType.String, CheckMan),
                db.CreateParameter("CheckResult", DbType.String, CheckResult),
                db.CreateParameter("CheckResultDesc", DbType.String, CheckResultDesc)
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

//        /// <summary>
//        /// 批量插入要抽查申请单信息
//        /// </summary>
//        /// <param name="tran">事务</param>
//        /// <param name="_ApplyCheckTaskMDL">抽查任务设置对象</param>
//        /// <returns></returns>
//        public static int InsertPatch(DbTransaction tran, ApplyCheckTaskMDL _ApplyCheckTaskMDL)
//        {
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();
//            System.Text.StringBuilder sbCount = new System.Text.StringBuilder();
//            DateTime start = _ApplyCheckTaskMDL.BusStartDate.Value;//业务起始时间
//            DateTime end = _ApplyCheckTaskMDL.BusEndDate.Value;//业务截止时间
//            int dataCount = 0;//待抽数据量

//            foreach (string s in _ApplyCheckTaskMDL.BusRangeCode.Split(','))
//            {
//                switch (s)
//                {
//                    case "1":
//                        sbCount.AppendFormat(string.Format(@"union all
//	                            SELECT count(*) dataCount
//	                            FROM [dbo].[Apply] 
//	                            where (ApplyType ='初始注册' or ApplyType ='重新注册' or ApplyType ='增项注册' or  ApplyType ='延期注册')  and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null
//	                            and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

//                        sb.AppendFormat(string.Format(@"union all
//	                            SELECT  
//		                            BusTypeID = 1
//		                            ,ApplyType
//		                            ,'Apply' ApplyTableName
//		                            ,[ApplyID] DataID
//		                            ,[PSN_Name] as WORKERNAME
//		                            ,[PSN_CertificateNO] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,[PSN_RegisterNo] as CERTIFICATECODE
//		                            ,[ConfirmDate]  ApplyFinishTime 
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//	                            FROM [dbo].[Apply] 
//	                            where (ApplyType ='初始注册' or ApplyType ='重新注册' or ApplyType ='增项注册' or  ApplyType ='延期注册')  and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null
//	                            and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
//                        break;
//                    case "2":
//                        sbCount.AppendFormat(string.Format(@"union all
//	                            SELECT count(*) dataCount
//	                            FROM [dbo].[zjs_Apply] 
//	                            where  (ApplyType ='初始注册' or ApplyType ='延期注册') and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null 
//                                and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

//                        sb.AppendFormat(string.Format(@"union all
//	                            SELECT  
//		                            BusTypeID = 2
//		                            ,ApplyType
//		                            ,'zjs_Apply' ApplyTableName
//		                            ,[ApplyID] DataID
//		                            ,[PSN_Name] as WORKERNAME
//		                            ,[PSN_CertificateNO] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,[PSN_RegisterNo] as CERTIFICATECODE
//		                            ,[ConfirmDate]  ApplyFinishTime 
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//	                            FROM [dbo].[zjs_Apply] 
//	                            where  (ApplyType ='初始注册' or ApplyType ='延期注册') and [ConfirmResult]='通过' and [PSN_RegisterNo] is not null 
//                                and [ConfirmDate] between '{0}' and '{1}'", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
//                        break;
//                    case "3":
//                        sbCount.AppendFormat(string.Format(@"union all
//	                            SELECT count(*) dataCount
//                                FROM [dbo].[EXAMSIGNUP] s
//                                inner join examplan e on s.examplanid = e.examplanid
//	                            where e.PostTypeID < 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null 
//                                union all   
//                                SELECT count(*) dataCount     
//                                FROM [dbo].[VIEW_CERTIFICATE_ENTER] 
//	                            where [CONFRIMDATE] between '{0}' and '{1}' and [NEWCERTIFICATECODE] is not null                      
//                                union all
//                                SELECT count(*) dataCount
//                                FROM [dbo].[CERTIFICATECONTINUE] x
//                                inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
//	                            where c.PostTypeID < 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过' ", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

//                        sb.AppendFormat(string.Format(@"union all
//	                            SELECT 
//		                            BusTypeID = 3 ,
//		                            '考试报名' ApplyType,
//		                            'EXAMSIGNUP' ApplyTableName,
//		                            cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
//		                            ,s.WORKERNAME
//		                            ,s.[CERTIFICATECODE] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
//		                            ,s.[PAYCONFIRMDATE]  ApplyFinishTime
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//                                FROM [dbo].[EXAMSIGNUP] s
//                                inner join examplan e on s.examplanid = e.examplanid
//	                            where e.PostTypeID < 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null 
//                                union all   
//                                SELECT BusTypeID =3
//		                            ,'证书进京' ApplyType
//		                            ,'CERTIFICATE_ENTER' ApplyTableName
//		                            ,cast(ApplyID as varchar(64)) DataID
//		                            ,WORKERNAME
//		                            ,[WORKERCERTIFICATECODE] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,[NEWCERTIFICATECODE] as CERTIFICATECODE
//		                            ,[CONFRIMDATE]  ApplyFinishTime 
//		                            ,CAST((RAND() * 100000000) AS bigint) randID       
//                                FROM [dbo].[VIEW_CERTIFICATE_ENTER] 
//	                            where [CONFRIMDATE] between '{0}' and '{1}' and [NEWCERTIFICATECODE] is not null                      
//                                union all
//                                SELECT  
//		                            BusTypeID = 3 
//		                            ,'证书续期' ApplyType
//		                            ,'CERTIFICATECONTINUE' ApplyTableName
//		                            ,cast(x.CERTIFICATECONTINUEID as varchar(64)) DataID
//		                            ,c.WORKERNAME
//		                            ,c.[WORKERCERTIFICATECODE] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,c.CERTIFICATECODE
//		                            ,x.[CONFIRMDATE]  ApplyFinishTime 
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//                                FROM [dbo].[CERTIFICATECONTINUE] x
//                                inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
//	                            where c.PostTypeID < 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过' ", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
//                        break;
//                    case "4":
//                        sbCount.AppendFormat(string.Format(@"union all
//                               SELECT count(*) dataCount
//                                FROM [dbo].[EXAMSIGNUP] s
//                                inner join examplan e on s.examplanid = e.examplanid
//	                            where e.PostTypeID = 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null                         
//                                union all
//                                SELECT count(*) dataCount
//                                FROM [dbo].[CERTIFICATECONTINUE] x
//                                inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
//	                            where c.PostTypeID = 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过' ", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));

//                        sb.AppendFormat(string.Format(@"union all
//                               SELECT 
//		                            BusTypeID = 4 ,
//		                            '考试报名' ApplyType,
//		                            'EXAMSIGNUP' ApplyTableName,
//		                            cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
//		                            ,s.WORKERNAME
//		                            ,s.[CERTIFICATECODE] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
//		                            ,s.[PAYCONFIRMDATE]  ApplyFinishTime
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//                                FROM [dbo].[EXAMSIGNUP] s
//                                inner join examplan e on s.examplanid = e.examplanid
//	                            where e.PostTypeID = 2 and s.[PAYCONFIRMDATE] between '{0}' and '{1}' and s.[RESULTCERTIFICATECODE] is not null                         
//                                union all
//                                SELECT  
//		                            BusTypeID = 4 
//		                            ,'证书续期' ApplyType
//		                            ,'CERTIFICATECONTINUE' ApplyTableName
//		                            ,cast(x.CERTIFICATECONTINUEID as varchar(64)) DataID
//		                            ,c.WORKERNAME
//		                            ,c.[WORKERCERTIFICATECODE] as [IDCard]
//		                            ,'身份证' as IDCardType
//		                            ,c.CERTIFICATECODE
//		                            ,x.[CONFIRMDATE]  ApplyFinishTime 
//		                            ,CAST((RAND() * 100000000) AS bigint) randID
//                                FROM [dbo].[CERTIFICATECONTINUE] x
//                                inner join [dbo].[CERTIFICATE] c on  x.CERTIFICATEID = c.CERTIFICATEID
//	                            where c.PostTypeID = 2 and x.[CONFIRMDATE] between '{0}' and '{1}' and x.[CONFIRMRESULT] ='决定通过' ", start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd 23:59:59")));
//                        break;
//                    default:
//                        break;
//                }
//            }

//            //总数据量
//            dataCount = (int)CommonDAL.GetObject(string.Format(@"select sum(dataCount) as dataCount from ({0}) t", sbCount.Remove(0, 9)));

//            DBHelper db = new DBHelper();
//            string sql = @"
//			INSERT INTO dbo.ApplyCheckTaskItem(TaskID,BusTypeID,ApplyType,ApplyTableName,DataID,WorkerName,IDCard,IDCardType,CertificateCode,ApplyFinishTime)
//			select  top({0} * {1} /1000) {2},BusTypeID,ApplyType,ApplyTableName,DataID,WORKERNAME,[IDCard],IDCardType,CERTIFICATECODE,ApplyFinishTime  from 
//            (
//                {3}
//            ) t order by randID";

//            //Utility.FileLog.WriteLog(string.Format(sql, dataCount, _ApplyCheckTaskMDL.CheckPer, _ApplyCheckTaskMDL.TaskID, sb.Remove(0, 9)));

//            return db.GetExcuteNonQuery(tran, string.Format(sql, dataCount, _ApplyCheckTaskMDL.CheckPer, _ApplyCheckTaskMDL.TaskID, sb.Remove(0, 9)));
//        }
        
        #endregion
    }
}
