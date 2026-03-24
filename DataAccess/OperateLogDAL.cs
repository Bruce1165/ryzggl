using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--OperateLogDAL(填写类描述)
    /// </summary>
    public class OperateLogDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(OperateLogOB operateLogOb)
        {
            return Insert(null, operateLogOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, OperateLogOB operateLogOb)
        {
            DBHelper db = new DBHelper();
              const string sql = @"
			INSERT INTO dbo.OperateLog(LogTime,PersonName,PersonID,OperateName,LogDetail)
			VALUES (@LogTime,@PersonName,@PersonID,@OperateName,@LogDetail)";

              List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("LogTime", DbType.DateTime, operateLogOb.LogTime),
                db.CreateParameter("PersonName", DbType.String, operateLogOb.PersonName),
                db.CreateParameter("PersonID", DbType.String, operateLogOb.PersonID),
                db.CreateParameter("OperateName", DbType.String, operateLogOb.OperateName),
                db.CreateParameter("LogDetail", DbType.String, operateLogOb.LogDetail)
            };
              return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(OperateLogOB operateLogOb)
        {
            return Update(null, operateLogOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, OperateLogOB operateLogOb)
        {
            const string sql = @"
			UPDATE dbo.OperateLog
				SET	LogTime = @LogTime,PersonName = @PersonName,PersonID = @PersonID,OperateName = @OperateName,LogDetail = @LogDetail
			WHERE
				LogID = @LogID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("LogID", DbType.Int64, operateLogOb.LogID),
                db.CreateParameter("LogTime", DbType.DateTime, operateLogOb.LogTime),
                db.CreateParameter("PersonName", DbType.String, operateLogOb.PersonName),
                db.CreateParameter("PersonID", DbType.String, operateLogOb.PersonID),
                db.CreateParameter("OperateName", DbType.String, operateLogOb.OperateName),
                db.CreateParameter("LogDetail", DbType.String, operateLogOb.LogDetail)
            };
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="logId">主键</param>
        /// <returns></returns>
        public static int Delete(long logId)
        {
            return Delete(null, logId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="logId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long logId)
        {
            const string sql = @"DELETE FROM dbo.OperateLog WHERE LogID = @LogID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("LogID", DbType.Int64, logId)
            };
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(OperateLogOB operateLogOb)
        {
            return Delete(null, operateLogOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="operateLogOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, OperateLogOB operateLogOb)
        {
            const string sql = @"DELETE FROM dbo.OperateLog WHERE LogID = @LogID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("LogID", DbType.Int64, operateLogOb.LogID)
            };
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="logId">主键</param>
        public static OperateLogOB GetObject(long logId)
        {
            const string sql = @"
			SELECT LogID,LogTime,PersonName,PersonID,OperateName,LogDetail
			FROM dbo.OperateLog
			WHERE LogID = @LogID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("LogID", DbType.Int64, logId)
            };
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                OperateLogOB operateLogOb = null;
                if (reader.Read())
                {
                    operateLogOb = new OperateLogOB();
                    if (reader["LogID"] != DBNull.Value) operateLogOb.LogID = Convert.ToInt64(reader["LogID"]);
                    if (reader["LogTime"] != DBNull.Value) operateLogOb.LogTime = Convert.ToDateTime(reader["LogTime"]);
                    if (reader["PersonName"] != DBNull.Value) operateLogOb.PersonName = Convert.ToString(reader["PersonName"]);
                    if (reader["PersonID"] != DBNull.Value) operateLogOb.PersonID = Convert.ToString(reader["PersonID"]);
                    if (reader["OperateName"] != DBNull.Value) operateLogOb.OperateName = Convert.ToString(reader["OperateName"]);
                    if (reader["LogDetail"] != DBNull.Value) operateLogOb.LogDetail = Convert.ToString(reader["LogDetail"]);
                }
                reader.Close();
                db.Close();
                return operateLogOb;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.OperateLog", "*", filterWhereString, orderBy == "" ? " LogID desc" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.OperateLog", filterWhereString);
        }

//        /// <summary>
//        /// 根据起始日期、结束日期来获取日志统计操作名称highcharts
//        /// </summary>
//        /// <param name="beiginDateTime">起始日期</param>
//        /// <param name="endDateTime">结束日期</param>
//        /// <returns></returns>
//        public static DataTable GetOperateLogStatisticsOperatenameHighcharts(string beiginDateTime, string endDateTime)
//        {
//            string cmdTxt = string.Format(@"SELECT  OPERATENAME,count(*) as StatisticsResult
//                                                FROM DBO.OPERATELOG
//                                                where  to_date(LOGTIME,'yyyy-mm-dd') between  '{0}'  and  '{1}'
//                                                group by OPERATENAME
//                                                order by OPERATENAME  asc;", beiginDateTime, endDateTime);
//            return new DBHelper().GetFillData(cmdTxt);
//        }

//        /// <summary>
//        /// 根据起始日期、结束日期来获取日志统计时间highcharts
//        /// </summary>
//        /// <param name="beiginDateTime">起始日期</param>
//        /// <param name="endDateTime">结束日期</param>
//        /// <returns></returns>
//        public static DataTable GetOperateLogStatisticsTimeHighcharts(string beiginDateTime, string endDateTime)
//        {
//            string cmdTxt = String.Format(@"SELECT  to_char(LOGTIME,'hh24') as HH ,count(HH)
//                                            FROM DBO.OPERATELOG
//                                            where  to_date(LOGTIME,'yyyy-mm-dd') between  '{0}'  and  '{1}'
//                                            group by HH
//                                            order by HH", beiginDateTime, endDateTime);
//            return new DBHelper().GetFillData(cmdTxt);
//        }


        /// <summary>
        /// 数据库连接数统计记录
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDBConnetTjList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, 
@"(select convert(varchar(10),dtime,120) dtime,max([MaxCount]) MaxCount,avg([AvgCount]) AvgCount
from [TJ_Connet_Hour]
group by convert(varchar(10),dtime,120)) temp"
, "*", filterWhereString, orderBy == "" ? " dtime desc" : orderBy);
        }

        /// <summary>
        /// 统计数据库连接数统计记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectDBConnetTjCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"(select convert(varchar(10),dtime,120) dtime,max([MaxCount]) MaxCount,avg([AvgCount]) AvgCount
                                                from [TJ_Connet_Hour]
                                                group by convert(varchar(10),dtime,120)) temp", filterWhereString);
        }
    }
}