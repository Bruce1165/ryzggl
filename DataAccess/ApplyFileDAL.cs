using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyFileDAL(填写类描述)
	/// </summary>
    public class ApplyFileDAL
    {
        public ApplyFileDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyFileMDL _ApplyFileMDL)
		{
		    return Insert(null,_ApplyFileMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyFileMDL _ApplyFileMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyFile(FileID,ApplyID,CheckResult,CheckDesc)
			VALUES (@FileID,@ApplyID,@CheckResult,@CheckDesc)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String, _ApplyFileMDL.FileID));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyFileMDL.ApplyID));
			p.Add(db.CreateParameter("CheckResult",DbType.Int32, _ApplyFileMDL.CheckResult));
			p.Add(db.CreateParameter("CheckDesc",DbType.String, _ApplyFileMDL.CheckDesc));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyFileMDL _ApplyFileMDL)
		{
			return Update(null,_ApplyFileMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyFileMDL _ApplyFileMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyFile
				SET	ApplyID = @ApplyID,CheckResult = @CheckResult,CheckDesc = @CheckDesc
			WHERE
				FileID = @FileID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String, _ApplyFileMDL.FileID));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyFileMDL.ApplyID));
			p.Add(db.CreateParameter("CheckResult",DbType.Int32, _ApplyFileMDL.CheckResult));
			p.Add(db.CreateParameter("CheckDesc",DbType.String, _ApplyFileMDL.CheckDesc));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyFileID">主键</param>
		/// <returns></returns>
        public static int Delete(string FileID, string ApplyID)
		{
            return Delete(null, FileID, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyFileID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string FileID, string ApplyID)
		{
            string sql = @"DELETE FROM dbo.ApplyFile WHERE FileID = @FileID and [ApplyID]=@ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String,FileID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyFileMDL _ApplyFileMDL)
		{
			return Delete(null,_ApplyFileMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFileMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyFileMDL _ApplyFileMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyFile WHERE FileID = @FileID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("FileID",DbType.String,_ApplyFileMDL.FileID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyFileID">主键</param>
        public static ApplyFileMDL GetObject( string FileID )
		{
			string sql=@"
			SELECT FileID,ApplyID,CheckResult,CheckDesc
			FROM dbo.ApplyFile
			WHERE FileID = @FileID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, FileID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyFileMDL _ApplyFileMDL = null;
                if (reader.Read())
                {
                    _ApplyFileMDL = new ApplyFileMDL();
					if (reader["FileID"] != DBNull.Value) _ApplyFileMDL.FileID = Convert.ToString(reader["FileID"]);
					if (reader["ApplyID"] != DBNull.Value) _ApplyFileMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["CheckResult"] != DBNull.Value) _ApplyFileMDL.CheckResult = Convert.ToInt32(reader["CheckResult"]);
					if (reader["CheckDesc"] != DBNull.Value) _ApplyFileMDL.CheckDesc = Convert.ToString(reader["CheckDesc"]);
                }
				reader.Close();
                db.Close();
                return _ApplyFileMDL;
            }
		}

        /// <summary>
        /// 根据ApplyID获取单个实体
        /// </summary>
        /// <param name="ApplyFileID">ApplyID</param>
        public static List<ApplyFileMDL> GetObjectApplyID(string ApplyID)
        {
            string sql = @"
			SELECT FileID,ApplyID,CheckResult,CheckDesc
			FROM dbo.ApplyFile
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (DataTable  dt = db.GetFillData(sql, p.ToArray()))
            {
                List<ApplyFileMDL> __ApplyFileMDL = new List<ApplyFileMDL>();
                ApplyFileMDL _ApplyFileMDL = null;
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count;i++)
                    { 
                        _ApplyFileMDL = new ApplyFileMDL();
                        if (dt.Rows[i]["FileID"] != DBNull.Value) _ApplyFileMDL.FileID = Convert.ToString(dt.Rows[i]["FileID"]);
                        if (dt.Rows[i]["ApplyID"] != DBNull.Value) _ApplyFileMDL.ApplyID = Convert.ToString(dt.Rows[i]["ApplyID"]);
                        if (dt.Rows[i]["CheckResult"] != DBNull.Value) _ApplyFileMDL.CheckResult = Convert.ToInt32(dt.Rows[i]["CheckResult"]);
                        if (dt.Rows[i]["CheckDesc"] != DBNull.Value) _ApplyFileMDL.CheckDesc = Convert.ToString(dt.Rows[i]["CheckDesc"]);
                        __ApplyFileMDL.Add(_ApplyFileMDL);
                    }
                }
                db.Close();
                return __ApplyFileMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyFile", "*", filterWhereString, orderBy == "" ? " FileID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyFile", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 申请单包含附件信息
        /// </summary>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns>附件集合信息</returns>
        public static DataTable GetListByApplyID(string ApplyID)
        {
            if(string.IsNullOrEmpty(ApplyID)==true)
            {
                ApplyID = "-1";
            }
            string sql = @"SELECT a.[FileID]
                          ,a.[ApplyID]
                          ,a.[CheckResult]
                          ,a.[CheckDesc]
                          ,f.[FileName]
                          ,f.[FileSize]
                          ,f.[FileUrl]
                          ,f.[DataType]
                          ,f.[FileType]
                          ,f.[AddTime]
                          ,f.[UploadMan]
                      FROM [dbo].[ApplyFile] a
                      inner join [dbo].[FileInfo] f on a.[ApplyID]='{0}' and a.[FileID] = f.[FileID]                    
                      order by 
                        case f.[DataType] 
                            when '一寸免冠照片' then 0
                            when '证件扫描件' then 1
                            when '学历证书扫描件' then 2
                            when '劳动合同扫描件' then 3
                            when '执业资格证书或资格考试合格通知书扫描件' then 4
                            when '社保扫描件' then 5
                            when '继续教育承诺书扫描件' then 6
                            when '继续教育证明扫描件' then 6
                            when '解除劳动合同证明' then 7
                            when '符合注销注册情形的相关证明' then 8
                            when '申请表扫描件' then 9
                            when '个人信息变更证明' then 10
                            when '符合注销注册情形的相关证明' then 11
                            when '企业信息变更证明' then 12
                            when '遗失声明扫描件' then 13
                            when '申述扫描件' then 14
                            when '安全教育培训和无违章及不良作业记录证明' then 15
                            when '安全生产考核证书扫描件' then 16
                            when '报考条件证明承诺书' then 17
                            when '个人健康承诺' then 18
                            when '技术职称扫描件' then 19
                            when '企业营业执照扫描件' then 20
                            when '手写签名照' then 21
                            when '体检合格证明' then 22
                            when '新设立企业建造师注册承诺书' then 23
                            when '增项注册告知承诺书' then 24
                            when '考试报名表扫描件' then 25
                            when '变更申请表扫描件' then 26
                            when '续期申请表扫描件' then 27
                            else 28
                        end,
                        f.[FileName]";
            return CommonDAL.GetDataTable(string.Format(sql,ApplyID));
        }

        /// <summary>
        /// 判断上传附件中是否包含某类型附件
        /// </summary>
        /// <param name="upFiles">已上传附件集合</param>
        /// <param name="fileType">待检查的附件类型</param>
        /// <returns>包含返回true，否正返回false</returns>
        public static bool CheckFileUpload(DataTable upFiles,string fileType)
        {
            
            foreach (DataRow r in upFiles.Rows)
            {
                if (r["DataType"].ToString() == fileType)
                {
                    return true;
                }               
            }
            return false;
        }

        /// <summary>
        /// 获取申请单上传一寸免冠照片数量
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns>照片数量</returns>
        public static int SelectFaceImgCountByApplyID(string applyID)
        {
            string sql = string.Format(@"SELECT  count(*)
                                          FROM [dbo].[ApplyFile] a
                                          inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
                                          where a.[ApplyID]='{0}' and f.DataType='一寸免冠照片'", applyID);

            var db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取申请单一寸照片地址
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns>一寸照片地址</returns>
        public static string SelectFaceImgPathByApplyID(string applyID)
        {
            string sql = string.Format(@"SELECT top 1  f.FileUrl
                                          FROM [dbo].[ApplyFile] a
                                          inner join [dbo].[FileInfo] f on a.[FileID] = f.[FileID]
                                          where a.[ApplyID]='{0}' and f.DataType='一寸免冠照片'", applyID);

            var db = new DBHelper();
            return Convert.ToString(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 拷贝证书当前附件到申请表
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PSN_RegisterNO">注册证号</param>
        /// <param name="fileTypeList">文件数据类型集合，如果拷贝全部传递null</param>
        public static void CopyFileFromCOC_TOW_Person_File(DbTransaction tran, string PSN_RegisterNO, string ApplyID, List<string> fileTypeList) 
        {
            string sql= @"INSERT INTO [dbo].[ApplyFile]([FileID] ,[ApplyID])
                        SELECT [COC_TOW_Person_File].[FileID],'{0}' 
                        FROM [dbo].[COC_TOW_Person_File] 
                        inner join [dbo].[FileInfo] on [COC_TOW_Person_File].FileID = [FileInfo].FileID
                        where [COC_TOW_Person_File].[PSN_RegisterNO]='{1}' {2}";

            System.Text.StringBuilder sb = new StringBuilder();
            foreach (string f in fileTypeList)
            {
                sb.Append(string.Format(",'{0}'", f));
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);

                //拷贝指定类型附件
                CommonDAL.ExecSQL(tran, string.Format(sql, ApplyID, PSN_RegisterNO, string.Format(" and FileInfo.DataType in({0})", sb)));
            }
            else
            {
                //拷贝全部附件
                CommonDAL.ExecSQL(tran, string.Format(sql, ApplyID, PSN_RegisterNO,""));
            }

            
        }

        /// <summary>
        /// 根据注册申请单ID获取个人一寸面馆照片
        /// </summary>
        /// <param name="ApplyID">注册申请单ID</param>
        /// <returns>一寸照片地址</returns>
        public static string GetPersonPhotoByApplyID(string ApplyID)
        {
            string sql = @"SELECT f.[FileUrl]  
                    FROM [dbo].[ApplyFile] a
                      inner join [dbo].[FileInfo] f on a.[ApplyID]='{0}' and a.[FileID] = f.[FileID]     
                    where  f.[DataType] ='一寸免冠照片'";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ApplyID));

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据注册申请单ID获取个人手写签名照
        /// </summary>
        /// <param name="ApplyID">注册申请单ID</param>
        /// <returns>手写签名照地址</returns>
        public static string GetSignPhotoByApplyID(string ApplyID)
        {
            string sql = @"SELECT f.[FileUrl]  
                    FROM [dbo].[ApplyFile] a
                      inner join [dbo].[FileInfo] f on a.[ApplyID]='{0}' and a.[FileID] = f.[FileID]     
                    where  f.[DataType] ='手写签名照'";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ApplyID));

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        } 
        #endregion
    }
}
