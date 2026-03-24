using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--COC_TOW_Person_FileDAL(填写类描述)
	/// </summary>
    public class COC_TOW_Person_FileDAL
    {
        public COC_TOW_Person_FileDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
		    return Insert(null,_COC_TOW_Person_FileMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.COC_TOW_Person_File(FileID,PSN_RegisterNO,IsHistory)
			VALUES (@FileID,@PSN_RegisterNO,@IsHistory)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String, _COC_TOW_Person_FileMDL.FileID));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _COC_TOW_Person_FileMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("IsHistory",DbType.Boolean, _COC_TOW_Person_FileMDL.IsHistory));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
			return Update(null,_COC_TOW_Person_FileMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
			string sql = @"
			UPDATE dbo.COC_TOW_Person_File
				SET	PSN_RegisterNO = @PSN_RegisterNO,IsHistory = @IsHistory
			WHERE
				FileID = @FileID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String, _COC_TOW_Person_FileMDL.FileID));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _COC_TOW_Person_FileMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("IsHistory",DbType.Boolean, _COC_TOW_Person_FileMDL.IsHistory));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="COC_TOW_Person_FileID">主键</param>
		/// <returns></returns>
        public static int Delete( string FileID )
		{
			return Delete(null, FileID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="COC_TOW_Person_FileID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string FileID)
		{
			string sql=@"DELETE FROM dbo.COC_TOW_Person_File WHERE FileID = @FileID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String,FileID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
			return Delete(null,_COC_TOW_Person_FileMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_FileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL)
		{
			string sql=@"DELETE FROM dbo.COC_TOW_Person_File WHERE FileID = @FileID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String,_COC_TOW_Person_FileMDL.FileID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="COC_TOW_Person_FileID">主键</param>
        public static COC_TOW_Person_FileMDL GetObject( string FileID )
		{
			string sql=@"
			SELECT FileID,PSN_RegisterNO,IsHistory
			FROM dbo.COC_TOW_Person_File
			WHERE FileID = @FileID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, FileID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_FileMDL _COC_TOW_Person_FileMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_FileMDL = new COC_TOW_Person_FileMDL();
					if (reader["FileID"] != DBNull.Value) _COC_TOW_Person_FileMDL.FileID = Convert.ToString(reader["FileID"]);
					if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_FileMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
					if (reader["IsHistory"] != DBNull.Value) _COC_TOW_Person_FileMDL.IsHistory = Convert.ToBoolean(reader["IsHistory"]);
                }
				reader.Close();
                db.Close();
                return _COC_TOW_Person_FileMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_TOW_Person_File", "*", filterWhereString, orderBy == "" ? " FileID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_TOW_Person_File", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 企业变更申请中只看申请表附件
        /// </summary>
        /// <returns>附件集合信息</returns>
        public static DataTable GetListByPSN_RegisterNO(string APPLYID)
        {

            string sql = @"SELECT TOP(1) A.* FROM  [FileInfo] A INNER JOIN APPLYFILE B ON A.[FileID] = B.[FileID]
                                AND B.APPLYID='{0}' 
                                WHERE A.[FileName] ='申请表扫描件' ORDER BY AddTime desc";
            return CommonDAL.GetDataTable(string.Format(sql, APPLYID));
        } 
        /// <summary>
        /// 人员包含附件信息
        /// </summary>
        /// <param name="PSN_RegisterNO">注册编号</param>
        /// <returns>附件集合信息</returns>
        public static DataTable GetListByPSN_RegisterNO(string PSN_RegisterNO,string APPLYID)
        {
//            string sql = @"SELECT a.[FileID]
//                                  ,a.[PSN_RegisterNO]
//                                  ,a.[IsHistory]
//                                  ,f.[FileName]
//                                  ,f.[FileSize]
//                                  ,f.[FileUrl]
//                                  ,f.[DataType]
//                                  ,f.[FileType]
//                                  ,f.[AddTime]
//                                  ,f.[UploadMan]
//                              FROM [dbo].[COC_TOW_Person_File] a
//                              inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
//                              where a.[PSN_RegisterNO]='{0}' and a.IsHistory=0 and f.[FileName] !='申请表扫描件'";
            string sql = @"SELECT A.* FROM  [FileInfo] A INNER JOIN [COC_TOW_Person_File] B ON A.[FileID] = B.[FileID]
                                WHERE B.[PSN_RegisterNO]='{0}' and B.IsHistory=0 and A.[FileName] !='申请表扫描件'
                                UNION
                                SELECT TOP(1) A.* FROM  [FileInfo] A INNER JOIN APPLYFILE B ON A.[FileID] = B.[FileID]
                                AND B.APPLYID='{1}' 
                                WHERE A.[FileName] ='申请表扫描件' ORDER BY AddTime desc";
            return CommonDAL.GetDataTable(string.Format(sql, PSN_RegisterNO,APPLYID));
        }

        /// <summary>
        /// 获取二级建造师最后注册成功后附件地址
        /// </summary>
        /// <param name="PSN_RegisterNO">二级建造师注册号</param>
        /// <param name="FileDataType">附件类型，枚举EnumManager.FileDataType</param>
        /// <returns>附件地址，找不到返回空</returns>
        public static string GetFileUrl(string PSN_RegisterNO, string FileDataType)
        {          
            string sql = @"SELECT top 1 f.FileUrl
                            FROM [dbo].[COC_TOW_Person_File] c
                            inner join [dbo].[FileInfo] f on c.[FileID]=f.[FileID]
                            where c.[PSN_RegisterNO]='{0}' and f.DataType='{1}' and c.[IsHistory]=0";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, PSN_RegisterNO, FileDataType));

            if(dt!=null && dt.Rows.Count>0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
