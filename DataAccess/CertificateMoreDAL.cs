using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateMoreDAL(填写类描述)
    /// </summary>
    public class CertificateMoreDAL
    {
        /// <summary>
        /// 业务类实现--CertificateMoreDAL(A本增发)
        /// </summary>
        public CertificateMoreDAL() { }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(CertificateMoreMDL _CertificateMoreMDL)
        {
            return Insert(null, _CertificateMoreMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, CertificateMoreMDL _CertificateMoreMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
            INSERT INTO dbo.CertificateMore(CertificateID,CertificateCode,ValidStartDate,ValidEndDate,ValidStartDateMore,ValidEndDateMore,WorkerName,WorkerCertificateCode,Sex,Birthday,PeoplePhone,UnitName,UnitCode,UnitNameMore,UnitCodeMore,CreatePerson,CreateTime,ModifyPerson,ModifyTime,CheckMan,CheckAdvise,CheckDate,CertificateCodeMore,ApplyStatus,ValID,CreditCode,NewUnitCheckTime,NewUnitAdvise,ConfirmMan,ConfirmAdvise,ConfirmDate)
            VALUES (@CertificateID,@CertificateCode,@ValidStartDate,@ValidEndDate,@ValidStartDateMore,@ValidEndDateMore,@WorkerName,@WorkerCertificateCode,@Sex,@Birthday,@PeoplePhone,@UnitName,@UnitCode,@UnitNameMore,@UnitCodeMore,@CreatePerson,@CreateTime,@ModifyPerson,@ModifyTime,@CheckMan,@CheckAdvise,@CheckDate,@CertificateCodeMore,@ApplyStatus,@ValID,@CreditCode,@NewUnitCheckTime,@NewUnitAdvise,@ConfirmMan,@ConfirmAdvise,@ConfirmDate)";

            List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("ApplyID", DbType.Int64, _CertificateMoreMDL.ApplyID));
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _CertificateMoreMDL.CertificateID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _CertificateMoreMDL.CertificateCode));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _CertificateMoreMDL.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _CertificateMoreMDL.ValidEndDate));
            p.Add(db.CreateParameter("ValidStartDateMore", DbType.DateTime, _CertificateMoreMDL.ValidStartDateMore));
            p.Add(db.CreateParameter("ValidEndDateMore", DbType.DateTime, _CertificateMoreMDL.ValidEndDateMore));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _CertificateMoreMDL.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _CertificateMoreMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("Sex", DbType.String, _CertificateMoreMDL.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _CertificateMoreMDL.Birthday));
            p.Add(db.CreateParameter("PeoplePhone", DbType.String, _CertificateMoreMDL.PeoplePhone));
            p.Add(db.CreateParameter("UnitName", DbType.String, _CertificateMoreMDL.UnitName));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _CertificateMoreMDL.UnitCode));
            p.Add(db.CreateParameter("UnitNameMore", DbType.String, _CertificateMoreMDL.UnitNameMore));
            p.Add(db.CreateParameter("UnitCodeMore", DbType.String, _CertificateMoreMDL.UnitCodeMore));
            p.Add(db.CreateParameter("CreatePerson", DbType.String, _CertificateMoreMDL.CreatePerson));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _CertificateMoreMDL.CreateTime));
            p.Add(db.CreateParameter("ModifyPerson", DbType.String, _CertificateMoreMDL.ModifyPerson));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _CertificateMoreMDL.ModifyTime));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _CertificateMoreMDL.CheckMan));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, _CertificateMoreMDL.CheckAdvise));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _CertificateMoreMDL.CheckDate));
            p.Add(db.CreateParameter("CertificateCodeMore", DbType.String, _CertificateMoreMDL.CertificateCodeMore));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _CertificateMoreMDL.ApplyStatus));
            p.Add(db.CreateParameter("ValID", DbType.String, _CertificateMoreMDL.ValID));
            p.Add(db.CreateParameter("CreditCode", DbType.String, _CertificateMoreMDL.CreditCode));
            p.Add(db.CreateParameter("NewUnitCheckTime", DbType.DateTime, _CertificateMoreMDL.NewUnitCheckTime));
            p.Add(db.CreateParameter("NewUnitAdvise", DbType.String, _CertificateMoreMDL.NewUnitAdvise));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _CertificateMoreMDL.ConfirmMan));
            p.Add(db.CreateParameter("ConfirmAdvise", DbType.String, _CertificateMoreMDL.ConfirmAdvise));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _CertificateMoreMDL.ConfirmDate));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateMoreMDL _CertificateMoreMDL)
        {
            return Update(null, _CertificateMoreMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateMoreMDL _CertificateMoreMDL)
        {
            string sql = @"
			UPDATE dbo.CertificateMore
				SET	CertificateID = @CertificateID,CertificateCode = @CertificateCode,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ValidStartDateMore = @ValidStartDateMore,ValidEndDateMore = @ValidEndDateMore,WorkerName = @WorkerName,WorkerCertificateCode = @WorkerCertificateCode,Sex = @Sex,Birthday = @Birthday,PeoplePhone=@PeoplePhone,UnitName = @UnitName,UnitCode = @UnitCode,UnitNameMore = @UnitNameMore,UnitCodeMore = @UnitCodeMore,CreatePerson = @CreatePerson,CreateTime = @CreateTime,ModifyPerson = @ModifyPerson,ModifyTime = @ModifyTime,CheckMan = @CheckMan,CheckAdvise = @CheckAdvise,CheckDate = @CheckDate,CertificateCodeMore = @CertificateCodeMore,ApplyStatus = @ApplyStatus,ValID = @ValID,CreditCode =@CreditCode,NewUnitCheckTime = @NewUnitCheckTime,NewUnitAdvise = @NewUnitAdvise,ConfirmMan = @ConfirmMan,ConfirmAdvise = @ConfirmAdvise,ConfirmDate = @ConfirmDate
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, _CertificateMoreMDL.ApplyID));
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _CertificateMoreMDL.CertificateID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _CertificateMoreMDL.CertificateCode));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _CertificateMoreMDL.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _CertificateMoreMDL.ValidEndDate));
            p.Add(db.CreateParameter("ValidStartDateMore", DbType.DateTime, _CertificateMoreMDL.ValidStartDateMore));
            p.Add(db.CreateParameter("ValidEndDateMore", DbType.DateTime, _CertificateMoreMDL.ValidEndDateMore));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _CertificateMoreMDL.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _CertificateMoreMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("Sex", DbType.String, _CertificateMoreMDL.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _CertificateMoreMDL.Birthday));
            p.Add(db.CreateParameter("PeoplePhone", DbType.String, _CertificateMoreMDL.PeoplePhone));
            p.Add(db.CreateParameter("UnitName", DbType.String, _CertificateMoreMDL.UnitName));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _CertificateMoreMDL.UnitCode));
            p.Add(db.CreateParameter("UnitNameMore", DbType.String, _CertificateMoreMDL.UnitNameMore));
            p.Add(db.CreateParameter("UnitCodeMore", DbType.String, _CertificateMoreMDL.UnitCodeMore));
            p.Add(db.CreateParameter("CreatePerson", DbType.String, _CertificateMoreMDL.CreatePerson));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _CertificateMoreMDL.CreateTime));
            p.Add(db.CreateParameter("ModifyPerson", DbType.String, _CertificateMoreMDL.ModifyPerson));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _CertificateMoreMDL.ModifyTime));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _CertificateMoreMDL.CheckMan));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, _CertificateMoreMDL.CheckAdvise));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _CertificateMoreMDL.CheckDate));
            p.Add(db.CreateParameter("CertificateCodeMore", DbType.String, _CertificateMoreMDL.CertificateCodeMore));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _CertificateMoreMDL.ApplyStatus));
            p.Add(db.CreateParameter("ValID", DbType.String, _CertificateMoreMDL.ValID));
            p.Add(db.CreateParameter("CreditCode", DbType.String, _CertificateMoreMDL.CreditCode));
            p.Add(db.CreateParameter("NewUnitCheckTime", DbType.DateTime, _CertificateMoreMDL.NewUnitCheckTime));
            p.Add(db.CreateParameter("NewUnitAdvise", DbType.String, _CertificateMoreMDL.NewUnitAdvise));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _CertificateMoreMDL.ConfirmMan));
            p.Add(db.CreateParameter("ConfirmAdvise", DbType.String, _CertificateMoreMDL.ConfirmAdvise));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _CertificateMoreMDL.ConfirmDate));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(long ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ApplyID)
        {
            string sql = @"DELETE FROM dbo.CertificateMore WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, ApplyID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateMoreMDL _CertificateMoreMDL)
        {
            return Delete(null, _CertificateMoreMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CertificateMoreMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateMoreMDL _CertificateMoreMDL)
        {
            string sql = @"DELETE FROM dbo.CertificateMore WHERE WorkerCertificateCode=@WorkerCertificateCode and UnitCodeMore=@UnitCodeMore";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _CertificateMoreMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCodeMore", DbType.String, _CertificateMoreMDL.UnitCodeMore));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyID">主键</param>
        public static CertificateMoreMDL GetObject(long ApplyID)
        {
            string sql = @"
			SELECT ApplyID,CertificateID,CertificateCode,ValidStartDate,ValidEndDate,ValidStartDateMore,ValidEndDateMore,WorkerName,WorkerCertificateCode,Sex,Birthday,PeoplePhone,UnitName,UnitCode,UnitNameMore,UnitCodeMore,CreatePerson,CreateTime,ModifyPerson,ModifyTime,CheckMan,CheckAdvise,CheckDate,CertificateCodeMore,ApplyStatus,ValID,CreditCode,NewUnitCheckTime,NewUnitAdvise,ConfirmMan,ConfirmAdvise,ConfirmDate
			FROM dbo.CertificateMore
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateMoreMDL _CertificateMoreMDL = null;
                if (reader.Read())
                {
                    _CertificateMoreMDL = new CertificateMoreMDL();
                    if (reader["ApplyID"] != DBNull.Value) _CertificateMoreMDL.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["CertificateID"] != DBNull.Value) _CertificateMoreMDL.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                    if (reader["CertificateCode"] != DBNull.Value) _CertificateMoreMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["ValidStartDate"] != DBNull.Value) _CertificateMoreMDL.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                    if (reader["ValidEndDate"] != DBNull.Value) _CertificateMoreMDL.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                    if (reader["ValidStartDateMore"] != DBNull.Value) _CertificateMoreMDL.ValidStartDateMore = Convert.ToDateTime(reader["ValidStartDateMore"]);
                    if (reader["ValidEndDateMore"] != DBNull.Value) _CertificateMoreMDL.ValidEndDateMore = Convert.ToDateTime(reader["ValidEndDateMore"]);
                    if (reader["WorkerName"] != DBNull.Value) _CertificateMoreMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _CertificateMoreMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["Sex"] != DBNull.Value) _CertificateMoreMDL.Sex = Convert.ToString(reader["Sex"]);
                    if (reader["Birthday"] != DBNull.Value) _CertificateMoreMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    if (reader["PeoplePhone"] != DBNull.Value) _CertificateMoreMDL.PeoplePhone = Convert.ToString(reader["PeoplePhone"]);
                    if (reader["UnitName"] != DBNull.Value) _CertificateMoreMDL.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["UnitCode"] != DBNull.Value) _CertificateMoreMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["UnitNameMore"] != DBNull.Value) _CertificateMoreMDL.UnitNameMore = Convert.ToString(reader["UnitNameMore"]);
                    if (reader["UnitCodeMore"] != DBNull.Value) _CertificateMoreMDL.UnitCodeMore = Convert.ToString(reader["UnitCodeMore"]);
                    if (reader["CreatePerson"] != DBNull.Value) _CertificateMoreMDL.CreatePerson = Convert.ToString(reader["CreatePerson"]);
                    if (reader["CreateTime"] != DBNull.Value) _CertificateMoreMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPerson"] != DBNull.Value) _CertificateMoreMDL.ModifyPerson = Convert.ToString(reader["ModifyPerson"]);
                    if (reader["ModifyTime"] != DBNull.Value) _CertificateMoreMDL.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["CheckMan"] != DBNull.Value) _CertificateMoreMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["CheckAdvise"] != DBNull.Value) _CertificateMoreMDL.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                    if (reader["CheckDate"] != DBNull.Value) _CertificateMoreMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CertificateCodeMore"] != DBNull.Value) _CertificateMoreMDL.CertificateCodeMore = Convert.ToString(reader["CertificateCodeMore"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _CertificateMoreMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["ValID"] != DBNull.Value) _CertificateMoreMDL.ValID = Convert.ToString(reader["ValID"]);
                    if (reader["CreditCode"] != DBNull.Value) _CertificateMoreMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                    if (reader["NewUnitCheckTime"] != DBNull.Value) _CertificateMoreMDL.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                    if (reader["NewUnitAdvise"] != DBNull.Value) _CertificateMoreMDL.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                    if (reader["ConfirmMan"] != DBNull.Value) _CertificateMoreMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["ConfirmAdvise"] != DBNull.Value) _CertificateMoreMDL.ConfirmAdvise = Convert.ToString(reader["ConfirmAdvise"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _CertificateMoreMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                }
                reader.Close();
                db.Close();
                return _CertificateMoreMDL;
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
        /// <summary>
        public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateMore", "*", filterWhereString, orderBy == "" ? " ApplyID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateMore", filterWhereString);
        }

        /// <summary>
        /// A本增发申请审核
        /// </summary>
        /// <param name="certificateMoreMDL"></param>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int Accepted(CertificateMoreMDL certificateMoreMDL, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateMore SET ModifyPerson=@ModifyPerson,ModifyTime=@ModifyTime,CheckMan=@CheckMan,CheckAdvise=@CheckAdvise,CheckDate=@CheckDate,ApplyStatus=@ApplyStatus,CertificateCodeMore=@CertificateCodeMore WHERE 1=1 " + filterWhereString;
            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("ModifyPerson", DbType.DateTime, certificateMoreMDL.ModifyPerson),
                db.CreateParameter("ModifyTime", DbType.String, certificateMoreMDL.ModifyTime),
                db.CreateParameter("CheckMan", DbType.String, certificateMoreMDL.CheckMan),
                db.CreateParameter("CheckAdvise", DbType.String, certificateMoreMDL.CheckAdvise),
                db.CreateParameter("CheckDate", DbType.String, certificateMoreMDL.CheckDate),
                db.CreateParameter("ApplyStatus", DbType.String, certificateMoreMDL.ApplyStatus),
                db.CreateParameter("CertificateCodeMore", DbType.String, certificateMoreMDL.CertificateCodeMore),
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 统计查询结果现聘用单位组织机构代码数量
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>现聘用单位组织机构代码个数</returns>
        public static int SelectDistinctUnitCodeCount(string filterWhereString)
        {
            string sql = @"SELECT COUNT(distinct UnitCode) FROM dbo.CERTIFICATEENTERAPPLY WHERE 1=1 {0}";
            sql = string.Format(sql, filterWhereString);
            var db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }


        /// <summary>
        /// 获A本增发申请审批记录集合
        /// </summary>
        /// <param name="applyId">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(long applyId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
                            (
                                SELECT '个人申请' as 'Action', [CreateTime] as ActionData, case [APPLYSTATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,case [APPLYSTATUS] when '填报中' then '未提交审核' else '待现单位确认' end as ActionRemark, [WORKERNAME] as ActionMan 
                                FROM [dbo].[CertificateMore] where [APPLYID]={0}  
                                union all
                                SELECT '增发单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [UnitNameMore] as ActionMan 
                                FROM [dbo].[CertificateMore] where [APPLYID]={0} and  [NewUnitCheckTime] >'2019-1-1'
                                union all 
                                SELECT '市级审核' as 'Action', [CHECKDATE] as ActionData,case [CheckAdvise] when '通过' then '已审核' else '审核未通过' end ActionResult,[CheckAdvise] as ActionRemark, [CHECKMAN] as ActionMan 
                                FROM [dbo].[CertificateMore] where [APPLYID]={0} and [CHECKDATE] >'1950-1-1'
                                union all 
                                SELECT '市级决定' as 'Action', [ConfirmDate] as ActionData,case [ConfirmAdvise] when '通过' then '已决定' else '决定未通过' end ActionResult,[ConfirmAdvise] as ActionRemark, [ConfirmMan] as ActionMan 
                                FROM [dbo].[CertificateMore] where [APPLYID]={0} and [ConfirmDate] >'1950-1-1'
                                union all 
                                    SELECT '住建部核准' as 'Action', 
	                                        case when c.ZZUrlUpTime > b.[ConfirmDate] then  dateadd(hour,1,b.[ConfirmDate]) 
			                                    when  Convert(varchar(10),b.[ConfirmDate],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[ConfirmDate] then c.EleCertErrTime
			                                    else null
	                                        end as ActionData,
                                            case when c.ZZUrlUpTime > b.[ConfirmDate] then  '已核准'
			                                    when  Convert(varchar(10),b.[ConfirmDate],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[ConfirmDate] then '核准未通过'
			                                    else null
	                                        end as ActionResult,
                                            case when c.ZZUrlUpTime > b.[ConfirmDate] then  '已生成电子证书（办结）' 
			                                    when  Convert(varchar(10),b.[ConfirmDate],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[ConfirmDate] then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			                                    else null
	                                        end as ActionRemark, 
                                         '住建部' as ActionMan 
                                     FROM [dbo].[CertificateMore] b
                                     inner join [dbo].[CERTIFICATE] c on b.[CertificateCodeMore] = c.[CertificateCode]
                                     where b.[APPLYID]={0} and b.[ConfirmDate] >'1950-1-1'
                                     and (c.ZZUrlUpTime > b.[ConfirmDate] 
		                                  or (c.EleCertErrTime > b.[ConfirmDate] and Convert(varchar(10),b.[ConfirmDate],21) = Convert(varchar(10),c.CHECKDATE,21) )
                                     )
                            ) t
                            ";
            return CommonDAL.GetDataTable(string.Format(sql, applyId));
        }


        //        #region 导出数据到文件（伪excel）

        //        private const string HtmlStartString = @"
        //            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
        //            <html xmlns=""http://www.w3.org/1999/xhtml"">
        //            <head>
        //            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
        //            <style>
        //            td{mso-number-format:\@;border-width:0.1pt;border-style:solid;border-color:#CCCCCC;}
        //            </style>
        //            </head>
        //            <body>
        //            <table cellspacing=""0"" border=""0"" style=""width:100%;table-layout:auto;empty-cells:show;"">";

        //        private const string HtmlEndString = @"</table></body></html> ";

        //        /// <summary>
        //        /// 导出html形式伪excel前格式化输出列
        //        /// 格式如：<tr><td>列1</td><td>列2</td><td>列3</td></tr>
        //        /// </summary>
        //        /// <param name="columnsList">列集合（用英文竖杠|分隔）</param>
        //        /// <param name="ifHead">列头：true，数据行：false</param>
        //        /// <returns></returns>
        //        private static string FormatColumnWithHtml(string columnsList, bool ifHead)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            if (ifHead == true)//列头
        //            {
        //                sb.Append("<tr><th>");
        //                sb.Append(columnsList.Replace(@"\", "</th><th>"));
        //                sb.Append("</th></tr>");
        //            }
        //            else//数据行
        //            {
        //                sb.Append("'<tr><td>'+isnull(");
        //                sb.Append(columnsList.Replace(@"\", ",'')+'</td><td>'+isnull("));
        //                sb.Append(",'')+'</td></tr>'");
        //            }
        //            return sb.ToString();
        //        }

        //        /// <summary>
        //        /// 格式化导出excel页头和页尾
        //        /// </summary>
        //        /// <param name="content">
        //        /// 要添加的内容及格式：<r>表示分行 ；<c>表示分列；<s>表示属性；<v>表示属性名和值的分隔符。
        //        /// </param>
        //        /// <returns>转化后的xml</returns>
        //        protected static string formatExcelCaptionFoot(string content)
        //        {
        //            if (string.IsNullOrEmpty(content) == true) return "";
        //            //处理excle表头输出（如：标题，创建时间，编号等等。注意不是处理表格列名）
        //            StringBuilder CaptionSB = new StringBuilder();
        //            string[] CaptionTrList = content.Replace("<r>", "|").Split('|');//分行
        //            int tdIndex = 0;//td位置
        //            StringBuilder tdStyle = new StringBuilder();//td样式
        //            foreach (string r in CaptionTrList)
        //            {
        //                CaptionSB.Append("<tr>");
        //                string[] CaptionTdList = r.Replace("<c>", "|").Split('|');//分列
        //                foreach (string c in CaptionTdList)
        //                {
        //                    CaptionSB.Append("<td>");
        //                    tdIndex = CaptionSB.Length - 1;
        //                    tdStyle.Remove(0, tdStyle.Length);
        //                    string[] CaptionStyleList = c.Replace("<s>", "|").Split('|');//属性集合
        //                    foreach (string s in CaptionStyleList)
        //                    {
        //                        string[] nameValue = s.Replace("<v>", "|").Split('|');//属性集合

        //                        switch (nameValue[0])
        //                        {
        //                            case "text"://内容
        //                                CaptionSB.Append(nameValue[1]);
        //                                break;
        //                            case "colspan"://合并单元格
        //                                CaptionSB.Insert(tdIndex, string.Format(" colspan=\"{0}\"", nameValue[1]));
        //                                break;
        //                            case "rowspan"://合并行
        //                                CaptionSB.Insert(tdIndex, string.Format(" rowspan=\"{0}\"", nameValue[1]));
        //                                break;
        //                            case "class"://合并行
        //                                CaptionSB.Insert(tdIndex, string.Format(" class=\"{0}\"", nameValue[1]));
        //                                break;
        //                            default://其他样式
        //                                tdStyle.Append(string.Format("{0}:{1};", nameValue[0], nameValue[1]));
        //                                break;
        //                        }
        //                    }
        //                    if (tdStyle.Length > 0)
        //                    {
        //                        CaptionSB.Insert(tdIndex, string.Format(" style=\"{0}\"", tdStyle.ToString()));
        //                    }
        //                    CaptionSB.Append("</td>");
        //                }
        //                CaptionSB.Append("</tr>");
        //            }
        //            return CaptionSB.ToString();
        //        }

        //        /// <summary>
        //        /// 获取文件大小KB
        //        /// </summary>
        //        /// <param name="filePath"></param>
        //        /// <returns></returns>
        //        public static string GetFileSize(string filePath)
        //        {
        //            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
        //            if (file.Exists == false) return "0k";
        //            if (file.Length < 1024)
        //                return string.Format("{0} Byte", file.Length.ToString());
        //            else
        //                return string.Format("{0} KB", (file.Length / 1024).ToString());
        //        }


        //        /// <summary>
        //        /// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        //        /// </summary>
        //        /// <param name="saveFileFullName">保存文件带路径的文件名</param>
        //        /// <param name="tableName">要导出的表名或试图名称</param>
        //        /// <param name="filterSql">过滤条件</param>
        //        /// <param name="orderBy">排序条件</param>
        //        /// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        //        /// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        //        public static void OutputXls(string saveFileFullName, string tableName, string filterSql, string orderBy, string headList, string columnList)
        //        {
        //            string sql = @"select {0} FROM {1} where 1=1 {2} order by {3} for xml path('') ";
        //            StringBuilder sb = new StringBuilder();

        //            try
        //            {
        //                SqlDataReader sdr = new DBHelper().GetDataReader(string.Format(sql, FormatColumnWithHtml(columnList, false), tableName, filterSql, orderBy));

        //                while (sdr.Read())
        //                {
        //                    sb.Append(sdr[0].ToString());
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Utility.FileLog.WriteLog("导出数据失败", ex);
        //                throw ex;
        //            }
        //            finally
        //            {

        //            }

        //            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileFullName, false, System.Text.Encoding.UTF8))
        //            {
        //                sw.Write(string.Format("{0}{1}{2}{3}"
        //                    , HtmlStartString
        //                     , FormatColumnWithHtml(headList, true)

        //                    , sb.ToString().Replace("&lt;", "<").Replace("&gt;", ">")
        //                    , HtmlEndString)
        //                    );
        //                sw.Flush();
        //                sw.Close();
        //            }
        //        }

        //        /// <summary>
        //        /// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        //        /// </summary>
        //        /// <param name="saveFileFullName">保存文件带路径的文件名</param>
        //        /// <param name="tableName">要导出的表名或试图名称</param>
        //        /// <param name="filterSql">过滤条件</param>
        //        /// <param name="orderBy">排序条件</param>
        //        /// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        //        /// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        //        /// <param name="Caption">输出自定义表头，如果不想带边框添加属相 class="noborder"
        //        ///  例如
        //        ///  <tr>
        //        ///     <td style="font-weight:bold;text-align:center;" class="noborder" colspan="8">二级建造师重新注册初审汇总表</td>
        //        ///  </tr>
        //        ///  <tr>
        //        ///     <td style="text-align:right;" colspan="4" class="noborder">批次号：</td>
        //        ///     <td style="text-align:right;" class="noborder" colspan="4">申报日期：</td>
        //        ///  </tr>
        //        ///  </param>
        //        /// <param name="Foot">输出自定义页尾,格式同Caption</param>
        //        public static void OutputXls(string saveFileFullName, string tableName, string filterSql, string orderBy, string headList, string columnList, string Caption, string Foot)
        //        {
        //            string sql = @"select {0} FROM {1} where 1=1 {2} order by {3} for xml path('') ";
        //            StringBuilder sb = new StringBuilder();

        //            try
        //            {
        //                SqlDataReader sdr = new DBHelper().GetDataReader(string.Format(sql, FormatColumnWithHtml(columnList, false), tableName, filterSql, orderBy));

        //                while (sdr.Read())
        //                {
        //                    sb.Append(sdr[0].ToString());
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Utility.FileLog.WriteLog("导出数据失败", ex);
        //                throw ex;
        //            }
        //            finally
        //            {

        //            }

        //            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileFullName, false, System.Text.Encoding.UTF8))
        //            {
        //                sw.Write(
        //                    string.Format("{0}{4}{1}{2}{5}{3}"
        //                    , HtmlStartString
        //                     , FormatColumnWithHtml(headList, true)
        //                    , sb.ToString().Replace("&lt;", "<").Replace("&gt;", ">")
        //                    , HtmlEndString
        //                    , Caption//formatExcelCaptionFoot(Caption)
        //                    , Foot//formatExcelCaptionFoot(Foot)
        //                                )
        //                    );
        //                sw.Flush();
        //                sw.Close();
        //            }
        //        }

        //        #endregion 导出数据到文件（伪excel）

        #region 自定义方法

        /// <summary>
        /// 根据身份证获取人员有效期最大的有效的A本
        /// </summary>
        public static CertificateOB GetCertificateA(string WorkerCertificateCode)
        {
            return GetCertificateA(null, WorkerCertificateCode);
        }

        ///  <summary>
        ///   根据身份证获取人员有效期最大的有效的企业法人A本（比较法人库和资质库）
        ///  </summary>
        ///  <param name="WorkerCertificateCode">身份证号</param>
        /// <param name="tran">事务</param>
        public static CertificateOB GetCertificateA(DbTransaction tran, string WorkerCertificateCode)
        {
//            string sql = @"
//			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID
//			FROM dbo.Certificate
//			WHERE WorkerCertificateCode = @WorkerCertificateCode 
//            and UnitCode in
//            (
//	            select [ORG_CODE] from dbo.[QY_FRK] where [ORG_CODE]=Certificate.UnitCode and [LEGAL_PERSON]= Certificate.WorkerName
//	            union 
//	            select ZZJGDM FROM [dbo].[jcsjk_QY_ZZZS] where ZZJGDM=Certificate.UnitCode and [FDDBRZJHM]= Certificate.WORKERCERTIFICATECODE
//            )
//            and PostID=147 AND ValidEndDate > GETDATE() AND STATUS !='离京变更' AND STATUS !='注销' ORDER BY ValidEndDate DESC";

            string sql = @"
			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID
			FROM dbo.Certificate
			WHERE WorkerCertificateCode = @WorkerCertificateCode 
            and exists(select top 1 [UNI_SCID] FROM [dbo].[QY_GSDJXX] where [UNI_SCID] like '________' + Certificate.UnitCode +  '_' and [CORP_RPT] = Certificate.WorkerName)
            and PostID=147 AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销' ORDER BY ValidEndDate DESC";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取企业法人有效A本
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public static CertificateOB GetCertificateAByUnitCode(string UnitCode)
        {
            string sql = @"
			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID
			FROM dbo.Certificate
			WHERE UnitCode = @UnitCode 
            and workername =(select [CORP_RPT] from dbo.[QY_GSDJXX] where [UNI_SCID] like @UnitCode)
            and PostID=147 AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销' 
            ORDER BY ValidEndDate DESC";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitCode", DbType.String, string.Format("________{0}_", UnitCode)));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

//        /// <summary>
//        /// 验证单位法人（法人库、资质库）
//        /// </summary>
//        /// <param name="UnitCode">组织机构代码</param>
//        /// <param name="FDDBR">法定代表人</param>
//        /// <param name="WorkerCertificateCode">法定代表人身份证号</param>
//        /// <returns>true：存在；false：不存在</returns>
//        public static bool IFExistFR(string UnitCode, string FDDBR, string WorkerCertificateCode)
//        {
////            string sql = string.Format(
////                @"SELECT COUNT(*)  FROM
////                    (
////	                    SELECT 1 as c FROM dbo.[QY_FRK] WHERE [ORG_CODE]='{0}' and [LEGAL_PERSON] ='{1}'
////	                    union
////	                    select 1 as c FROM [dbo].[jcsjk_QY_ZZZS] where ZZJGDM='{0}' and [FDDBRZJHM]= '{2}'
////                ) t", UnitCode, FDDBR, WorkerCertificateCode);

//            string sql = string.Format(
//               @"select count(*) FROM [dbo].[QY_GSDJXX] where [UNI_SCID] like '________{0}_' and [CORP_RPT]='{1}'", UnitCode, FDDBR);

//            var db = new DBHelper();
//            int count = Convert.ToInt32(db.ExecuteScalar(sql));

//            if (count > 0)
//                return true;
//            else
//                return false;
//        }

        /// <summary>
        /// 验证单位是否存在有效A本
        /// </summary>
        /// <param name="UnitCode">组织机构代码</param>
        /// <param name="WorkerName">法人姓名</param>
        /// <returns>true：存在；false：不存在</returns>
        public static bool IFExistA(string UnitCode, string WorkerName)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.[Certificate] WHERE UnitCode='{0}' and workerName ='{1}' and PostID=147 AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销'", UnitCode, WorkerName);

            var db = new DBHelper();
            int count = Convert.ToInt32(db.ExecuteScalar(sql));

            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证单位是否存在增发申请
        /// </summary>
        /// <param name="UnitCode">组织机构代码</param>
        /// <returns>true：存在；false：不存在</returns>
        public static bool IFExistApply(string UnitCode)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.[CertificateMore] WHERE UnitCodeMore='{0}' and [ApplyStatus] <>'已决定'", UnitCode);
            var db = new DBHelper();
            int count = Convert.ToInt32(db.ExecuteScalar(sql));

            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
