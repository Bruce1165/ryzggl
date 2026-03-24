using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--Out_CheckCFZCDAL(填写类描述)
	/// </summary>
    public class Out_CheckCFZCDAL
    {
        public Out_CheckCFZCDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
		    return Insert(null,_Out_CheckCFZCMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Out_CheckCFZC([ID],UserName,Provice,IDCard,UnitName,RegType,CertCode,PublishDate,ValidEnd,PublishBy,Sex,Birthday,Remark,Question,CreateDate)
			VALUES (@ID,@UserName,@Provice,@IDCard,@UnitName,@RegType,@CertCode,@PublishDate,@ValidEnd,@PublishBy,@Sex,@Birthday,@Remark,@Question,@CreateDate)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String, _Out_CheckCFZCMDL.ID));
			p.Add(db.CreateParameter("UserName",DbType.String, _Out_CheckCFZCMDL.UserName));
			p.Add(db.CreateParameter("Provice",DbType.String, _Out_CheckCFZCMDL.Provice));
			p.Add(db.CreateParameter("IDCard",DbType.String, _Out_CheckCFZCMDL.IDCard));
			p.Add(db.CreateParameter("UnitName",DbType.String, _Out_CheckCFZCMDL.UnitName));
			p.Add(db.CreateParameter("RegType",DbType.String, _Out_CheckCFZCMDL.RegType));
			p.Add(db.CreateParameter("CertCode",DbType.String, _Out_CheckCFZCMDL.CertCode));
			p.Add(db.CreateParameter("PublishDate",DbType.String, _Out_CheckCFZCMDL.PublishDate));
			p.Add(db.CreateParameter("ValidEnd",DbType.String, _Out_CheckCFZCMDL.ValidEnd));
			p.Add(db.CreateParameter("PublishBy",DbType.String, _Out_CheckCFZCMDL.PublishBy));
			p.Add(db.CreateParameter("Sex",DbType.String, _Out_CheckCFZCMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.String, _Out_CheckCFZCMDL.Birthday));
			p.Add(db.CreateParameter("Remark",DbType.String, _Out_CheckCFZCMDL.Remark));
			p.Add(db.CreateParameter("Question",DbType.String, _Out_CheckCFZCMDL.Question));
			p.Add(db.CreateParameter("CreateDate",DbType.DateTime, _Out_CheckCFZCMDL.CreateDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
			return Update(null,_Out_CheckCFZCMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
			string sql = @"
			UPDATE dbo.Out_CheckCFZC
				SET	UserName = @UserName,Provice = @Provice,IDCard = @IDCard,UnitName = @UnitName,RegType = @RegType,CertCode = @CertCode,PublishDate = @PublishDate,ValidEnd = @ValidEnd,PublishBy = @PublishBy,Sex = @Sex,Birthday = @Birthday,Remark = @Remark,Question = @Question,CreateDate = @CreateDate
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String, _Out_CheckCFZCMDL.ID));
			p.Add(db.CreateParameter("UserName",DbType.String, _Out_CheckCFZCMDL.UserName));
			p.Add(db.CreateParameter("Provice",DbType.String, _Out_CheckCFZCMDL.Provice));
			p.Add(db.CreateParameter("IDCard",DbType.String, _Out_CheckCFZCMDL.IDCard));
			p.Add(db.CreateParameter("UnitName",DbType.String, _Out_CheckCFZCMDL.UnitName));
			p.Add(db.CreateParameter("RegType",DbType.String, _Out_CheckCFZCMDL.RegType));
			p.Add(db.CreateParameter("CertCode",DbType.String, _Out_CheckCFZCMDL.CertCode));
			p.Add(db.CreateParameter("PublishDate",DbType.String, _Out_CheckCFZCMDL.PublishDate));
			p.Add(db.CreateParameter("ValidEnd",DbType.String, _Out_CheckCFZCMDL.ValidEnd));
			p.Add(db.CreateParameter("PublishBy",DbType.String, _Out_CheckCFZCMDL.PublishBy));
			p.Add(db.CreateParameter("Sex",DbType.String, _Out_CheckCFZCMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.String, _Out_CheckCFZCMDL.Birthday));
			p.Add(db.CreateParameter("Remark",DbType.String, _Out_CheckCFZCMDL.Remark));
			p.Add(db.CreateParameter("Question",DbType.String, _Out_CheckCFZCMDL.Question));
			p.Add(db.CreateParameter("CreateDate",DbType.DateTime, _Out_CheckCFZCMDL.CreateDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="Out_CheckCFZCID">主键</param>
		/// <returns></returns>
        public static int Delete( string ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="Out_CheckCFZCID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ID)
		{
			string sql=@"DELETE FROM dbo.Out_CheckCFZC WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
			return Delete(null,_Out_CheckCFZCMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckCFZCMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,Out_CheckCFZCMDL _Out_CheckCFZCMDL)
		{
			string sql=@"DELETE FROM dbo.Out_CheckCFZC WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String,_Out_CheckCFZCMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="Out_CheckCFZCID">主键</param>
        public static Out_CheckCFZCMDL GetObject( string ID )
		{
			string sql=@"
			SELECT [ID],UserName,Provice,IDCard,UnitName,RegType,CertCode,PublishDate,ValidEnd,PublishBy,Sex,Birthday,Remark,Question,CreateDate
			FROM dbo.Out_CheckCFZC
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                Out_CheckCFZCMDL _Out_CheckCFZCMDL = null;
                if (reader.Read())
                {
                    _Out_CheckCFZCMDL = new Out_CheckCFZCMDL();
					if (reader["ID"] != DBNull.Value) _Out_CheckCFZCMDL.ID = Convert.ToString(reader["ID"]);
					if (reader["UserName"] != DBNull.Value) _Out_CheckCFZCMDL.UserName = Convert.ToString(reader["UserName"]);
					if (reader["Provice"] != DBNull.Value) _Out_CheckCFZCMDL.Provice = Convert.ToString(reader["Provice"]);
					if (reader["IDCard"] != DBNull.Value) _Out_CheckCFZCMDL.IDCard = Convert.ToString(reader["IDCard"]);
					if (reader["UnitName"] != DBNull.Value) _Out_CheckCFZCMDL.UnitName = Convert.ToString(reader["UnitName"]);
					if (reader["RegType"] != DBNull.Value) _Out_CheckCFZCMDL.RegType = Convert.ToString(reader["RegType"]);
					if (reader["CertCode"] != DBNull.Value) _Out_CheckCFZCMDL.CertCode = Convert.ToString(reader["CertCode"]);
					if (reader["PublishDate"] != DBNull.Value) _Out_CheckCFZCMDL.PublishDate = Convert.ToString(reader["PublishDate"]);
					if (reader["ValidEnd"] != DBNull.Value) _Out_CheckCFZCMDL.ValidEnd = Convert.ToString(reader["ValidEnd"]);
					if (reader["PublishBy"] != DBNull.Value) _Out_CheckCFZCMDL.PublishBy = Convert.ToString(reader["PublishBy"]);
					if (reader["Sex"] != DBNull.Value) _Out_CheckCFZCMDL.Sex = Convert.ToString(reader["Sex"]);
					if (reader["Birthday"] != DBNull.Value) _Out_CheckCFZCMDL.Birthday = Convert.ToString(reader["Birthday"]);
					if (reader["Remark"] != DBNull.Value) _Out_CheckCFZCMDL.Remark = Convert.ToString(reader["Remark"]);
					if (reader["Question"] != DBNull.Value) _Out_CheckCFZCMDL.Question = Convert.ToString(reader["Question"]);
					if (reader["CreateDate"] != DBNull.Value) _Out_CheckCFZCMDL.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                }
				reader.Close();
                db.Close();
                return _Out_CheckCFZCMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Out_CheckCFZC", "*", filterWhereString, orderBy == "" ? "IDCard,ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Out_CheckCFZC", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
