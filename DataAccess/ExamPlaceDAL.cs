using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamPlaceDAL(填写类描述)
    /// </summary>
    public class ExamPlaceDAL
    {
        public ExamPlaceDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamPlaceOB _ExamPlaceOB)
        {
            return Insert(null, _ExamPlaceOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamPlaceOB _ExamPlaceOB)
        {
            DBHelper db = new DBHelper();
           
            string sql = @"
			INSERT INTO dbo.ExamPlace(ExamPlaceName,ExamPlaceAddress,LinkMan,Phone,RoomNum,ExamPersonNum,Status)
			VALUES (@ExamPlaceName,@ExamPlaceAddress,@LinkMan,@Phone,@RoomNum,@ExamPersonNum,@Status);SELECT @ExamPlaceID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamPlaceID", DbType.Int64));
            p.Add(db.CreateParameter("ExamPlaceName", DbType.String, _ExamPlaceOB.ExamPlaceName));
            p.Add(db.CreateParameter("ExamPlaceAddress", DbType.String, _ExamPlaceOB.ExamPlaceAddress));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ExamPlaceOB.LinkMan));
            p.Add(db.CreateParameter("Phone", DbType.String, _ExamPlaceOB.Phone));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamPlaceOB.Status));
            p.Add(db.CreateParameter("RoomNum", DbType.Int32, _ExamPlaceOB.RoomNum));
            p.Add(db.CreateParameter("ExamPersonNum", DbType.Int32, _ExamPlaceOB.ExamPersonNum));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamPlaceOB.ExamPlaceID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamPlaceOB _ExamPlaceOB)
        {
            return Update(null, _ExamPlaceOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamPlaceOB _ExamPlaceOB)
        {
            string sql = @"
			UPDATE dbo.ExamPlace
				SET	ExamPlaceName = @ExamPlaceName,ExamPlaceAddress = @ExamPlaceAddress,LinkMan = @LinkMan,Phone = @Phone,RoomNum = @RoomNum,ExamPersonNum = @ExamPersonNum,Status = @Status
			WHERE
				ExamPlaceID = @ExamPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamPlaceOB.ExamPlaceID));
            p.Add(db.CreateParameter("ExamPlaceName", DbType.String, _ExamPlaceOB.ExamPlaceName));
            p.Add(db.CreateParameter("ExamPlaceAddress", DbType.String, _ExamPlaceOB.ExamPlaceAddress));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ExamPlaceOB.LinkMan));
            p.Add(db.CreateParameter("Phone", DbType.String, _ExamPlaceOB.Phone));
            p.Add(db.CreateParameter("RoomNum", DbType.Int32, _ExamPlaceOB.RoomNum));
            p.Add(db.CreateParameter("ExamPersonNum", DbType.Int32, _ExamPlaceOB.ExamPersonNum));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamPlaceOB.Status));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamPlaceID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamPlaceID)
        {
            return Delete(null, ExamPlaceID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPlaceID)
        {
            string sql = @"DELETE FROM dbo.ExamPlace WHERE ExamPlaceID = @ExamPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, ExamPlaceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamPlaceOB _ExamPlaceOB)
        {
            return Delete(null, _ExamPlaceOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamPlaceOB _ExamPlaceOB)
        {
            string sql = @"DELETE FROM dbo.ExamPlace WHERE ExamPlaceID = @ExamPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamPlaceOB.ExamPlaceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlaceID">主键</param>
        public static ExamPlaceOB GetObject(long ExamPlaceID)
        {
            string sql = @"
			SELECT ExamPlaceID,ExamPlaceName,ExamPlaceAddress,LinkMan,Phone,RoomNum,ExamPersonNum,Status
			FROM dbo.ExamPlace
			WHERE ExamPlaceID = @ExamPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, ExamPlaceID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamPlaceOB _ExamPlaceOB = null;
                    if (reader.Read())
                    {
                        _ExamPlaceOB = new ExamPlaceOB();
                        if (reader["ExamPlaceID"] != DBNull.Value) _ExamPlaceOB.ExamPlaceID = Convert.ToInt64(reader["ExamPlaceID"]);
                        if (reader["ExamPlaceName"] != DBNull.Value) _ExamPlaceOB.ExamPlaceName = Convert.ToString(reader["ExamPlaceName"]);
                        if (reader["ExamPlaceAddress"] != DBNull.Value) _ExamPlaceOB.ExamPlaceAddress = Convert.ToString(reader["ExamPlaceAddress"]);
                        if (reader["LinkMan"] != DBNull.Value) _ExamPlaceOB.LinkMan = Convert.ToString(reader["LinkMan"]);
                        if (reader["Phone"] != DBNull.Value) _ExamPlaceOB.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlaceOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["RoomNum"] != DBNull.Value) _ExamPlaceOB.RoomNum = Convert.ToInt32(reader["RoomNum"]);
                        if (reader["ExamPersonNum"] != DBNull.Value) _ExamPlaceOB.ExamPersonNum = Convert.ToInt32(reader["ExamPersonNum"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamPlaceOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlace", "*", filterWhereString, orderBy == "" ? " ExamPlaceID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlace", filterWhereString);
        }

        /// <summary>
        /// 获取新版职业技能培训点考点信息（对外隐藏，[STATUS]='已删除'）
        /// </summary>
        /// <param name="ExamPlaceName">培训点名称</param>
        public static ExamPlaceOB GetObjectByExamPlaceName(string ExamPlaceName)
        {
            string sql = @"
			SELECT top 1 ExamPlaceID,ExamPlaceName,ExamPlaceAddress,LinkMan,Phone,RoomNum,ExamPersonNum,Status
			FROM dbo.ExamPlace
			WHERE ExamPlaceName = @ExamPlaceName and [STATUS]='已删除'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceName", DbType.String, ExamPlaceName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamPlaceOB _ExamPlaceOB = null;
                    if (reader.Read())
                    {
                        _ExamPlaceOB = new ExamPlaceOB();
                        if (reader["ExamPlaceID"] != DBNull.Value) _ExamPlaceOB.ExamPlaceID = Convert.ToInt64(reader["ExamPlaceID"]);
                        if (reader["ExamPlaceName"] != DBNull.Value) _ExamPlaceOB.ExamPlaceName = Convert.ToString(reader["ExamPlaceName"]);
                        if (reader["ExamPlaceAddress"] != DBNull.Value) _ExamPlaceOB.ExamPlaceAddress = Convert.ToString(reader["ExamPlaceAddress"]);
                        if (reader["LinkMan"] != DBNull.Value) _ExamPlaceOB.LinkMan = Convert.ToString(reader["LinkMan"]);
                        if (reader["Phone"] != DBNull.Value) _ExamPlaceOB.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlaceOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["RoomNum"] != DBNull.Value) _ExamPlaceOB.RoomNum = Convert.ToInt32(reader["RoomNum"]);
                        if (reader["ExamPersonNum"] != DBNull.Value) _ExamPlaceOB.ExamPersonNum = Convert.ToInt32(reader["ExamPersonNum"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamPlaceOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }
    }
}
