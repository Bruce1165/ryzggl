using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--COC_TOW_Register_Profession_HisDAL(填写类描述)
	/// </summary>
    public class COC_TOW_Register_Profession_HisDAL
    {
        public COC_TOW_Register_Profession_HisDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
		    return Insert(null,_COC_TOW_Register_Profession_HisMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.COC_TOW_Register_Profession_His(His_ID,PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime,ApplyType,[GETDATE])
			VALUES (@His_ID,@PRO_ServerID,@PSN_ServerID,@PRO_Profession,@PRO_ValidityBegin,@PRO_ValidityEnd,@DogID,@ENT_Province_Code,@DownType,@LastModifyTime,@ApplyType,@GetDate)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("His_ID",DbType.String, _COC_TOW_Register_Profession_HisMDL.His_ID));
			p.Add(db.CreateParameter("PRO_ServerID",DbType.String, _COC_TOW_Register_Profession_HisMDL.PRO_ServerID));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _COC_TOW_Register_Profession_HisMDL.PSN_ServerID));
			p.Add(db.CreateParameter("PRO_Profession",DbType.String, _COC_TOW_Register_Profession_HisMDL.PRO_Profession));
			p.Add(db.CreateParameter("PRO_ValidityBegin",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.PRO_ValidityBegin));
			p.Add(db.CreateParameter("PRO_ValidityEnd",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.PRO_ValidityEnd));
			p.Add(db.CreateParameter("DogID",DbType.String, _COC_TOW_Register_Profession_HisMDL.DogID));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _COC_TOW_Register_Profession_HisMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("DownType",DbType.String, _COC_TOW_Register_Profession_HisMDL.DownType));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.LastModifyTime));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _COC_TOW_Register_Profession_HisMDL.ApplyType));
			p.Add(db.CreateParameter("GetDate",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.GetDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
			return Update(null,_COC_TOW_Register_Profession_HisMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
			string sql = @"
			UPDATE dbo.COC_TOW_Register_Profession_His
				SET	PRO_ServerID = @PRO_ServerID,PSN_ServerID = @PSN_ServerID,PRO_Profession = @PRO_Profession,PRO_ValidityBegin = @PRO_ValidityBegin,PRO_ValidityEnd = @PRO_ValidityEnd,DogID = @DogID,ENT_Province_Code = @ENT_Province_Code,DownType = @DownType,LastModifyTime = @LastModifyTime,ApplyType = @ApplyType,""GETDATE"" = @GetDate
			WHERE
				His_ID = @His_ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("His_ID",DbType.String, _COC_TOW_Register_Profession_HisMDL.His_ID));
			p.Add(db.CreateParameter("PRO_ServerID",DbType.String, _COC_TOW_Register_Profession_HisMDL.PRO_ServerID));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _COC_TOW_Register_Profession_HisMDL.PSN_ServerID));
			p.Add(db.CreateParameter("PRO_Profession",DbType.String, _COC_TOW_Register_Profession_HisMDL.PRO_Profession));
			p.Add(db.CreateParameter("PRO_ValidityBegin",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.PRO_ValidityBegin));
			p.Add(db.CreateParameter("PRO_ValidityEnd",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.PRO_ValidityEnd));
			p.Add(db.CreateParameter("DogID",DbType.String, _COC_TOW_Register_Profession_HisMDL.DogID));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _COC_TOW_Register_Profession_HisMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("DownType",DbType.String, _COC_TOW_Register_Profession_HisMDL.DownType));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.LastModifyTime));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _COC_TOW_Register_Profession_HisMDL.ApplyType));
			p.Add(db.CreateParameter("GetDate",DbType.DateTime, _COC_TOW_Register_Profession_HisMDL.GetDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="COC_TOW_Register_Profession_HisID">主键</param>
		/// <returns></returns>
        public static int Delete( string His_ID )
		{
			return Delete(null, His_ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="COC_TOW_Register_Profession_HisID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string His_ID)
		{
			string sql=@"DELETE FROM dbo.COC_TOW_Register_Profession_His WHERE His_ID = @His_ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("His_ID",DbType.String,His_ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
			return Delete(null,_COC_TOW_Register_Profession_HisMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_Profession_HisMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL)
		{
			string sql=@"DELETE FROM dbo.COC_TOW_Register_Profession_His WHERE His_ID = @His_ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("His_ID",DbType.String,_COC_TOW_Register_Profession_HisMDL.His_ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="COC_TOW_Register_Profession_HisID">主键</param>
        public static COC_TOW_Register_Profession_HisMDL GetObject( string His_ID )
		{
			string sql=@"
			SELECT His_ID,PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime,ApplyType,[GETDATE]
			FROM dbo.COC_TOW_Register_Profession_His
			WHERE His_ID = @His_ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("His_ID", DbType.String, His_ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Register_Profession_HisMDL = new COC_TOW_Register_Profession_HisMDL();
					if (reader["His_ID"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.His_ID = Convert.ToString(reader["His_ID"]);
					if (reader["PRO_ServerID"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.PRO_ServerID = Convert.ToString(reader["PRO_ServerID"]);
					if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
					if (reader["PRO_Profession"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.PRO_Profession = Convert.ToString(reader["PRO_Profession"]);
					if (reader["PRO_ValidityBegin"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.PRO_ValidityBegin = Convert.ToDateTime(reader["PRO_ValidityBegin"]);
					if (reader["PRO_ValidityEnd"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.PRO_ValidityEnd = Convert.ToDateTime(reader["PRO_ValidityEnd"]);
					if (reader["DogID"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.DogID = Convert.ToString(reader["DogID"]);
					if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
					if (reader["DownType"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.DownType = Convert.ToString(reader["DownType"]);
					if (reader["LastModifyTime"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
					if (reader["ApplyType"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
					if (reader["GetDate"] != DBNull.Value) _COC_TOW_Register_Profession_HisMDL.GetDate = Convert.ToDateTime(reader["GetDate"]);
                }
				reader.Close();
                db.Close();
                return _COC_TOW_Register_Profession_HisMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_TOW_Register_Profession_His", "*", filterWhereString, orderBy == "" ? " His_ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_TOW_Register_Profession_His", filterWhereString);
        }
        
        #region 自定义方法
         public static  COC_TOW_Register_Profession_HisMDL ListGetObject(DbTransaction tran,COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL,string type)
        {
            COC_TOW_Register_Profession_HisMDL _COC_TOW_Register_Profession_HisMDL = new COC_TOW_Register_Profession_HisMDL();
            _COC_TOW_Register_Profession_HisMDL.His_ID = Guid.NewGuid().ToString();
            _COC_TOW_Register_Profession_HisMDL.PRO_ServerID = _COC_TOW_Register_ProfessionMDL.PRO_ServerID;
            _COC_TOW_Register_Profession_HisMDL.PSN_ServerID = _COC_TOW_Register_ProfessionMDL.PSN_ServerID;
            _COC_TOW_Register_Profession_HisMDL.PRO_Profession = _COC_TOW_Register_ProfessionMDL.PRO_Profession;
            _COC_TOW_Register_Profession_HisMDL.PRO_ValidityBegin = _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin;
            _COC_TOW_Register_Profession_HisMDL.PRO_ValidityEnd = _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd;
            _COC_TOW_Register_Profession_HisMDL.DogID = _COC_TOW_Register_ProfessionMDL.DogID;
            _COC_TOW_Register_Profession_HisMDL.ENT_Province_Code = _COC_TOW_Register_ProfessionMDL.ENT_Province_Code;
            _COC_TOW_Register_Profession_HisMDL.DownType = _COC_TOW_Register_ProfessionMDL.DownType;
            _COC_TOW_Register_Profession_HisMDL.LastModifyTime = _COC_TOW_Register_ProfessionMDL.LastModifyTime;
            _COC_TOW_Register_Profession_HisMDL.ApplyType = type;
            _COC_TOW_Register_Profession_HisMDL.GetDate = DateTime.Now;
            return _COC_TOW_Register_Profession_HisMDL;
        }
        #endregion
    }
}
