using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--Out_CheckShebaoDAL(填写类描述)
	/// </summary>
    public class Out_CheckShebaoDAL
    {
        public Out_CheckShebaoDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
		    return Insert(null,_Out_CheckShebaoMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Out_CheckShebao([ID],UserName,IDCard,RegType,Unit,Provice,ShebaoUnit,PublishDateTime,Question,Sex,Birthday,Remark,IDCard18,RegCode,RegProfession,Region,Createdate)
			VALUES (@ID,@UserName,@IDCard,@RegType,@Unit,@Provice,@ShebaoUnit,@PublishDateTime,@Question,@Sex,@Birthday,@Remark,@IDCard18,@RegCode,@RegProfession,@Region,@Createdate)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String, _Out_CheckShebaoMDL.ID));
			p.Add(db.CreateParameter("UserName",DbType.String, _Out_CheckShebaoMDL.UserName));
			p.Add(db.CreateParameter("IDCard",DbType.String, _Out_CheckShebaoMDL.IDCard));
			p.Add(db.CreateParameter("RegType",DbType.String, _Out_CheckShebaoMDL.RegType));
			p.Add(db.CreateParameter("Unit",DbType.String, _Out_CheckShebaoMDL.Unit));
			p.Add(db.CreateParameter("Provice",DbType.String, _Out_CheckShebaoMDL.Provice));
			p.Add(db.CreateParameter("ShebaoUnit",DbType.String, _Out_CheckShebaoMDL.ShebaoUnit));
			p.Add(db.CreateParameter("PublishDateTime",DbType.String, _Out_CheckShebaoMDL.PublishDateTime));
			p.Add(db.CreateParameter("Question",DbType.String, _Out_CheckShebaoMDL.Question));
			p.Add(db.CreateParameter("Sex",DbType.String, _Out_CheckShebaoMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.String, _Out_CheckShebaoMDL.Birthday));
			p.Add(db.CreateParameter("Remark",DbType.String, _Out_CheckShebaoMDL.Remark));
			p.Add(db.CreateParameter("IDCard18",DbType.String, _Out_CheckShebaoMDL.IDCard18));
			p.Add(db.CreateParameter("RegCode",DbType.String, _Out_CheckShebaoMDL.RegCode));
			p.Add(db.CreateParameter("RegProfession",DbType.String, _Out_CheckShebaoMDL.RegProfession));
			p.Add(db.CreateParameter("Region",DbType.String, _Out_CheckShebaoMDL.Region));
			p.Add(db.CreateParameter("Createdate",DbType.DateTime, _Out_CheckShebaoMDL.Createdate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
			return Update(null,_Out_CheckShebaoMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
			string sql = @"
			UPDATE dbo.Out_CheckShebao
				SET	UserName = @UserName,IDCard = @IDCard,RegType = @RegType,Unit = @Unit,Provice = @Provice,ShebaoUnit = @ShebaoUnit,PublishDateTime = @PublishDateTime,Question = @Question,Sex = @Sex,Birthday = @Birthday,Remark = @Remark,IDCard18 = @IDCard18,RegCode = @RegCode,RegProfession = @RegProfession,Region = @Region,Createdate = @Createdate
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String, _Out_CheckShebaoMDL.ID));
			p.Add(db.CreateParameter("UserName",DbType.String, _Out_CheckShebaoMDL.UserName));
			p.Add(db.CreateParameter("IDCard",DbType.String, _Out_CheckShebaoMDL.IDCard));
			p.Add(db.CreateParameter("RegType",DbType.String, _Out_CheckShebaoMDL.RegType));
			p.Add(db.CreateParameter("Unit",DbType.String, _Out_CheckShebaoMDL.Unit));
			p.Add(db.CreateParameter("Provice",DbType.String, _Out_CheckShebaoMDL.Provice));
			p.Add(db.CreateParameter("ShebaoUnit",DbType.String, _Out_CheckShebaoMDL.ShebaoUnit));
			p.Add(db.CreateParameter("PublishDateTime",DbType.String, _Out_CheckShebaoMDL.PublishDateTime));
			p.Add(db.CreateParameter("Question",DbType.String, _Out_CheckShebaoMDL.Question));
			p.Add(db.CreateParameter("Sex",DbType.String, _Out_CheckShebaoMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.String, _Out_CheckShebaoMDL.Birthday));
			p.Add(db.CreateParameter("Remark",DbType.String, _Out_CheckShebaoMDL.Remark));
			p.Add(db.CreateParameter("IDCard18",DbType.String, _Out_CheckShebaoMDL.IDCard18));
			p.Add(db.CreateParameter("RegCode",DbType.String, _Out_CheckShebaoMDL.RegCode));
			p.Add(db.CreateParameter("RegProfession",DbType.String, _Out_CheckShebaoMDL.RegProfession));
			p.Add(db.CreateParameter("Region",DbType.String, _Out_CheckShebaoMDL.Region));
			p.Add(db.CreateParameter("Createdate",DbType.DateTime, _Out_CheckShebaoMDL.Createdate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="Out_CheckShebaoID">主键</param>
		/// <returns></returns>
        public static int Delete( string ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="Out_CheckShebaoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ID)
		{
			string sql=@"DELETE FROM dbo.Out_CheckShebao WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
			return Delete(null,_Out_CheckShebaoMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Out_CheckShebaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,Out_CheckShebaoMDL _Out_CheckShebaoMDL)
		{
			string sql=@"DELETE FROM dbo.Out_CheckShebao WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.String,_Out_CheckShebaoMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="Out_CheckShebaoID">主键</param>
        public static Out_CheckShebaoMDL GetObject( string ID )
		{
			string sql=@"
			SELECT [ID],UserName,IDCard,RegType,Unit,Provice,ShebaoUnit,PublishDateTime,Question,Sex,Birthday,Remark,IDCard18,RegCode,RegProfession,Region,Createdate
			FROM dbo.Out_CheckShebao
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                Out_CheckShebaoMDL _Out_CheckShebaoMDL = null;
                if (reader.Read())
                {
                    _Out_CheckShebaoMDL = new Out_CheckShebaoMDL();
					if (reader["ID"] != DBNull.Value) _Out_CheckShebaoMDL.ID = Convert.ToString(reader["ID"]);
					if (reader["UserName"] != DBNull.Value) _Out_CheckShebaoMDL.UserName = Convert.ToString(reader["UserName"]);
					if (reader["IDCard"] != DBNull.Value) _Out_CheckShebaoMDL.IDCard = Convert.ToString(reader["IDCard"]);
					if (reader["RegType"] != DBNull.Value) _Out_CheckShebaoMDL.RegType = Convert.ToString(reader["RegType"]);
					if (reader["Unit"] != DBNull.Value) _Out_CheckShebaoMDL.Unit = Convert.ToString(reader["Unit"]);
					if (reader["Provice"] != DBNull.Value) _Out_CheckShebaoMDL.Provice = Convert.ToString(reader["Provice"]);
					if (reader["ShebaoUnit"] != DBNull.Value) _Out_CheckShebaoMDL.ShebaoUnit = Convert.ToString(reader["ShebaoUnit"]);
					if (reader["PublishDateTime"] != DBNull.Value) _Out_CheckShebaoMDL.PublishDateTime = Convert.ToString(reader["PublishDateTime"]);
					if (reader["Question"] != DBNull.Value) _Out_CheckShebaoMDL.Question = Convert.ToString(reader["Question"]);
					if (reader["Sex"] != DBNull.Value) _Out_CheckShebaoMDL.Sex = Convert.ToString(reader["Sex"]);
					if (reader["Birthday"] != DBNull.Value) _Out_CheckShebaoMDL.Birthday = Convert.ToString(reader["Birthday"]);
					if (reader["Remark"] != DBNull.Value) _Out_CheckShebaoMDL.Remark = Convert.ToString(reader["Remark"]);
					if (reader["IDCard18"] != DBNull.Value) _Out_CheckShebaoMDL.IDCard18 = Convert.ToString(reader["IDCard18"]);
					if (reader["RegCode"] != DBNull.Value) _Out_CheckShebaoMDL.RegCode = Convert.ToString(reader["RegCode"]);
					if (reader["RegProfession"] != DBNull.Value) _Out_CheckShebaoMDL.RegProfession = Convert.ToString(reader["RegProfession"]);
					if (reader["Region"] != DBNull.Value) _Out_CheckShebaoMDL.Region = Convert.ToString(reader["Region"]);
					if (reader["Createdate"] != DBNull.Value) _Out_CheckShebaoMDL.Createdate = Convert.ToDateTime(reader["Createdate"]);
                }
				reader.Close();
                db.Close();
                return _Out_CheckShebaoMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Out_CheckShebao", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Out_CheckShebao", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
