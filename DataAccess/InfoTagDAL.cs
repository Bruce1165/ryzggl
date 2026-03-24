using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--InfoTagDAL(填写类描述)
	/// </summary>
    public class InfoTagDAL
    {
        public InfoTagDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(InfoTagOB _InfoTagOB)
		{
		    return Insert(null,_InfoTagOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,InfoTagOB _InfoTagOB)
		{
			DBHelper db = new DBHelper();				
		
			string sql = @"
			INSERT INTO dbo.InfoTag(SubjectID,TagCode,ShowCode,Title,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Weight)
			VALUES (@SubjectID,@TagCode,@ShowCode,@Title,@Flag,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@Weight);SELECT @TagID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("TagID",DbType.Int64));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _InfoTagOB.SubjectID));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _InfoTagOB.TagCode));
			p.Add(db.CreateParameter("ShowCode",DbType.String, _InfoTagOB.ShowCode));
			p.Add(db.CreateParameter("Title",DbType.String, _InfoTagOB.Title));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _InfoTagOB.Flag));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _InfoTagOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _InfoTagOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _InfoTagOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _InfoTagOB.ModifyTime));
            p.Add(db.CreateParameter("Weight", DbType.Int32, _InfoTagOB.Weight));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _InfoTagOB.TagID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(InfoTagOB _InfoTagOB)
		{
			return Update(null,_InfoTagOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,InfoTagOB _InfoTagOB)
		{
			string sql = @"
			UPDATE dbo.InfoTag
				SET	SubjectID = @SubjectID,TagCode = @TagCode,ShowCode = @ShowCode,Title = @Title,Flag = @Flag,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,Weight = @Weight
			WHERE
				TagID = @TagID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TagID",DbType.Int64, _InfoTagOB.TagID));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _InfoTagOB.SubjectID));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _InfoTagOB.TagCode));
			p.Add(db.CreateParameter("ShowCode",DbType.String, _InfoTagOB.ShowCode));
			p.Add(db.CreateParameter("Title",DbType.String, _InfoTagOB.Title));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _InfoTagOB.Flag));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _InfoTagOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _InfoTagOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _InfoTagOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _InfoTagOB.ModifyTime));
            p.Add(db.CreateParameter("Weight", DbType.Int32, _InfoTagOB.Weight));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="InfoTagID">主键</param>
		/// <returns></returns>
        public static int Delete( long TagID )
		{
			return Delete(null, TagID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="InfoTagID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long TagID)
		{
			string sql=@"DELETE FROM dbo.InfoTag WHERE TagID = @TagID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TagID",DbType.Int64,TagID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(InfoTagOB _InfoTagOB)
		{
			return Delete(null,_InfoTagOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="InfoTagOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,InfoTagOB _InfoTagOB)
		{
			string sql=@"DELETE FROM dbo.InfoTag WHERE TagID = @TagID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TagID",DbType.Int64,_InfoTagOB.TagID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}

        /// <summary>
        /// 删除科目下所有大纲(逻辑删除)
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_SubjectID">科目ID</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, int _SubjectID)
        {
            string sql = @"UPDATE dbo.InfoTag set Flag=0 WHERE SubjectID = @SubjectID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, _SubjectID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="InfoTagID">主键</param>
        public static InfoTagOB GetObject( long TagID )
		{
			string sql= @"
			SELECT TagID,SubjectID,TagCode,ShowCode,Title,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Weight
			FROM dbo.InfoTag
			WHERE TagID = @TagID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("TagID", DbType.Int64, TagID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                InfoTagOB _InfoTagOB = null;
                if (reader.Read())
                {
                    _InfoTagOB = new InfoTagOB();
					if (reader["TagID"] != DBNull.Value) _InfoTagOB.TagID = Convert.ToInt64(reader["TagID"]);
					if (reader["SubjectID"] != DBNull.Value) _InfoTagOB.SubjectID = Convert.ToInt32(reader["SubjectID"]);
					if (reader["TagCode"] != DBNull.Value) _InfoTagOB.TagCode = Convert.ToInt32(reader["TagCode"]);
					if (reader["ShowCode"] != DBNull.Value) _InfoTagOB.ShowCode = Convert.ToString(reader["ShowCode"]);
					if (reader["Title"] != DBNull.Value) _InfoTagOB.Title = Convert.ToString(reader["Title"]);
					if (reader["Flag"] != DBNull.Value) _InfoTagOB.Flag = Convert.ToByte(reader["Flag"]);
					if (reader["CreatePersonID"] != DBNull.Value) _InfoTagOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
					if (reader["CreateTime"] != DBNull.Value) _InfoTagOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["ModifyPersonID"] != DBNull.Value) _InfoTagOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
					if (reader["ModifyTime"] != DBNull.Value) _InfoTagOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["Weight"] != DBNull.Value) _InfoTagOB.Weight = Convert.ToInt32(reader["Weight"]);
                }
				reader.Close();
                db.Close();
                return _InfoTagOB;
            }
		}

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="InfoTagID">主键</param>
        public static InfoTagOB GetObject(int SubjectID, int TagCode)
        {
            string sql = @"
			SELECT TagID,SubjectID,TagCode,ShowCode,Title,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Weight
			FROM dbo.InfoTag
			WHERE SubjectID = @SubjectID and TagCode = @TagCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, SubjectID));
            p.Add(db.CreateParameter("TagCode", DbType.Int32, TagCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                InfoTagOB _InfoTagOB = null;
                if (reader.Read())
                {
                    _InfoTagOB = new InfoTagOB();
                    if (reader["TagID"] != DBNull.Value) _InfoTagOB.TagID = Convert.ToInt64(reader["TagID"]);
                    if (reader["SubjectID"] != DBNull.Value) _InfoTagOB.SubjectID = Convert.ToInt32(reader["SubjectID"]);
                    if (reader["TagCode"] != DBNull.Value) _InfoTagOB.TagCode = Convert.ToInt32(reader["TagCode"]);
                    if (reader["ShowCode"] != DBNull.Value) _InfoTagOB.ShowCode = Convert.ToString(reader["ShowCode"]);
                    if (reader["Title"] != DBNull.Value) _InfoTagOB.Title = Convert.ToString(reader["Title"]);
                    if (reader["Flag"] != DBNull.Value) _InfoTagOB.Flag = Convert.ToByte(reader["Flag"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _InfoTagOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _InfoTagOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _InfoTagOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _InfoTagOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["Weight"] != DBNull.Value) _InfoTagOB.Weight = Convert.ToInt32(reader["Weight"]);
                }
                reader.Close();
                db.Close();
                return _InfoTagOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.InfoTag", "*", filterWhereString, orderBy == "" ? " TagID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.InfoTag", filterWhereString);
        }
    }
}
