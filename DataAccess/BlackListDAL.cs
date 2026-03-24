using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--BlackListDAL(填写类描述)
    /// </summary>
    public class BlackListDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(BlackListOB blackListOb)
        {
            return Insert(null, blackListOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, BlackListOB blackListOb)
        {
            var db = new DBHelper();
           

            const string sql = @"
			INSERT INTO dbo.BlackList(PostTypeID,PostID,WorkerName,CertificateCode,UnitName,UnitCode,TrainUnitName,BlackType,StartTime,BlackStatus,Remark,CreatePerson,CreateTime,ModifyPerson,ModifyTime)
			VALUES (@PostTypeID,@PostID,@WorkerName,@CertificateCode,@UnitName,@UnitCode,@TrainUnitName,@BlackType,@StartTime,@BlackStatus,@Remark,@CreatePerson,@CreateTime,@ModifyPerson,@ModifyTime);SELECT @BlackListID = @@IDENTITY";

            var p = new List<SqlParameter>
		    {
		        db.CreateOutParameter("BlackListID", DbType.Int64),
		        db.CreateParameter("PostTypeID", DbType.Int32, blackListOb.PostTypeID),
		        db.CreateParameter("PostID", DbType.Int32, blackListOb.PostID),
		        db.CreateParameter("WorkerName", DbType.String, blackListOb.WorkerName),
		        db.CreateParameter("CertificateCode", DbType.String, blackListOb.CertificateCode),
		        db.CreateParameter("UnitName", DbType.String, blackListOb.UnitName),
		        db.CreateParameter("UnitCode", DbType.String, blackListOb.UnitCode),
		        db.CreateParameter("TrainUnitName", DbType.String, blackListOb.TrainUnitName),
		        db.CreateParameter("BlackType", DbType.String, blackListOb.BlackType),
		        db.CreateParameter("StartTime", DbType.DateTime, blackListOb.StartTime),
		        db.CreateParameter("BlackStatus", DbType.String, blackListOb.BlackStatus),
		        db.CreateParameter("Remark", DbType.String, blackListOb.Remark),
		        db.CreateParameter("CreatePerson", DbType.String, blackListOb.CreatePerson),
		        db.CreateParameter("CreateTime", DbType.DateTime, blackListOb.CreateTime),
		        db.CreateParameter("ModifyPerson", DbType.String, blackListOb.ModifyPerson),
		        db.CreateParameter("ModifyTime", DbType.DateTime, blackListOb.ModifyTime)
		    };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            blackListOb.BlackListID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(BlackListOB blackListOb)
        {
            return Update(null, blackListOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, BlackListOB blackListOb)
        {
            const string sql = @"
			UPDATE dbo.BlackList
				SET	PostTypeID = @PostTypeID,PostID = @PostID,WorkerName = @WorkerName,CertificateCode = @CertificateCode,UnitName = @UnitName,UnitCode = @UnitCode,TrainUnitName = @TrainUnitName,BlackType = @BlackType,StartTime = @StartTime,BlackStatus = @BlackStatus,Remark = @Remark,CreatePerson = @CreatePerson,CreateTime = @CreateTime,ModifyPerson = @ModifyPerson,ModifyTime = @ModifyTime
			WHERE
				BlackListID = @BlackListID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
			{
			    db.CreateParameter("BlackListID", DbType.Int64, blackListOb.BlackListID),
			    db.CreateParameter("PostTypeID", DbType.Int32, blackListOb.PostTypeID),
			    db.CreateParameter("PostID", DbType.Int32, blackListOb.PostID),
			    db.CreateParameter("WorkerName", DbType.String, blackListOb.WorkerName),
			    db.CreateParameter("CertificateCode", DbType.String, blackListOb.CertificateCode),
			    db.CreateParameter("UnitName", DbType.String, blackListOb.UnitName),
			    db.CreateParameter("UnitCode", DbType.String, blackListOb.UnitCode),
			    db.CreateParameter("TrainUnitName", DbType.String, blackListOb.TrainUnitName),
			    db.CreateParameter("BlackType", DbType.String, blackListOb.BlackType),
			    db.CreateParameter("StartTime", DbType.DateTime, blackListOb.StartTime),
			    db.CreateParameter("BlackStatus", DbType.String, blackListOb.BlackStatus),
			    db.CreateParameter("Remark", DbType.String, blackListOb.Remark),
			    db.CreateParameter("CreatePerson", DbType.String, blackListOb.CreatePerson),
			    db.CreateParameter("CreateTime", DbType.DateTime, blackListOb.CreateTime),
			    db.CreateParameter("ModifyPerson", DbType.String, blackListOb.ModifyPerson),
			    db.CreateParameter("ModifyTime", DbType.DateTime, blackListOb.ModifyTime)
			};
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="blackListId">主键</param>
        /// <returns></returns>
        public static int Delete(long blackListId)
        {
            return Delete(null, blackListId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="blackListId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long blackListId)
        {
            const string sql = @"DELETE FROM dbo.BlackList WHERE BlackListID = @BlackListID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("BlackListID", DbType.Int64, blackListId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(BlackListOB blackListOb)
        {
            return Delete(null, blackListOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="blackListOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, BlackListOB blackListOb)
        {
            const string sql = @"DELETE FROM dbo.BlackList WHERE BlackListID = @BlackListID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("BlackListID", DbType.Int64, blackListOb.BlackListID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="blackListId">主键</param>
        public static BlackListOB GetObject(long blackListId)
        {
            const string sql = @"
			SELECT BlackListID,PostTypeID,PostID,WorkerName,CertificateCode,UnitName,UnitCode,TrainUnitName,BlackType,StartTime,BlackStatus,Remark,CreatePerson,CreateTime,ModifyPerson,ModifyTime
			FROM dbo.BlackList
			WHERE BlackListID = @BlackListID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("BlackListID", DbType.Int64, blackListId)
            };
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                BlackListOB blackListOb = null;
                if (reader.Read())
                {
                    blackListOb = new BlackListOB();
                    if (reader["BlackListID"] != DBNull.Value) blackListOb.BlackListID = Convert.ToInt64(reader["BlackListID"]);
                    if (reader["PostTypeID"] != DBNull.Value) blackListOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                    if (reader["PostID"] != DBNull.Value) blackListOb.PostID = Convert.ToInt32(reader["PostID"]);
                    if (reader["WorkerName"] != DBNull.Value) blackListOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["CertificateCode"] != DBNull.Value) blackListOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["UnitName"] != DBNull.Value) blackListOb.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["UnitCode"] != DBNull.Value) blackListOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["TrainUnitName"] != DBNull.Value) blackListOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                    if (reader["BlackType"] != DBNull.Value) blackListOb.BlackType = Convert.ToString(reader["BlackType"]);
                    if (reader["StartTime"] != DBNull.Value) blackListOb.StartTime = Convert.ToDateTime(reader["StartTime"]);
                    if (reader["BlackStatus"] != DBNull.Value) blackListOb.BlackStatus = Convert.ToString(reader["BlackStatus"]);
                    if (reader["Remark"] != DBNull.Value) blackListOb.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["CreatePerson"] != DBNull.Value) blackListOb.CreatePerson = Convert.ToString(reader["CreatePerson"]);
                    if (reader["CreateTime"] != DBNull.Value) blackListOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPerson"] != DBNull.Value) blackListOb.ModifyPerson = Convert.ToString(reader["ModifyPerson"]);
                    if (reader["ModifyTime"] != DBNull.Value) blackListOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                }
                reader.Close();
                db.Close();
                return blackListOb;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_BLACKLIST", "*", filterWhereString, orderBy == "" ? " BlackListID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.BlackList", filterWhereString);
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}