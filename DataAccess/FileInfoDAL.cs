using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--FileInfoDAL(填写类描述)
    /// </summary>
    public class FileInfoDAL
    {
        public FileInfoDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(FileInfoMDL _FileInfoMDL)
        {
            return Insert(null, _FileInfoMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, FileInfoMDL _FileInfoMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.FileInfo(FileID,[FILENAME],FileSize,FileUrl,DataType,FileType,AddTime,UploadMan,OrderNo)
			VALUES (@FileID,@FileName,@FileSize,@FileUrl,@DataType,@FileType,@AddTime,@UploadMan,cast(GETDATE() as decimal(18,8)))";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, _FileInfoMDL.FileID));
            p.Add(db.CreateParameter("FileName", DbType.String, _FileInfoMDL.FileName));
            p.Add(db.CreateParameter("FileSize", DbType.Int64, _FileInfoMDL.FileSize));
            p.Add(db.CreateParameter("FileUrl", DbType.String, _FileInfoMDL.FileUrl));
            p.Add(db.CreateParameter("DataType", DbType.String, _FileInfoMDL.DataType));
            p.Add(db.CreateParameter("FileType", DbType.String, _FileInfoMDL.FileType));
            p.Add(db.CreateParameter("AddTime", DbType.DateTime, _FileInfoMDL.AddTime));
            p.Add(db.CreateParameter("UploadMan", DbType.String, _FileInfoMDL.UploadMan));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(FileInfoMDL _FileInfoMDL)
        {
            return Update(null, _FileInfoMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, FileInfoMDL _FileInfoMDL)
        {
            string sql = @"
			UPDATE dbo.FileInfo
				SET	""FILENAME"" = @FileName,FileSize = @FileSize,FileUrl = @FileUrl,DataType = @DataType,FileType = @FileType,AddTime = @AddTime,UploadMan = @UploadMan
			WHERE
				FileID = @FileID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, _FileInfoMDL.FileID));
            p.Add(db.CreateParameter("FileName", DbType.String, _FileInfoMDL.FileName));
            p.Add(db.CreateParameter("FileSize", DbType.Int64, _FileInfoMDL.FileSize));
            p.Add(db.CreateParameter("FileUrl", DbType.String, _FileInfoMDL.FileUrl));
            p.Add(db.CreateParameter("DataType", DbType.String, _FileInfoMDL.DataType));
            p.Add(db.CreateParameter("FileType", DbType.String, _FileInfoMDL.FileType));
            p.Add(db.CreateParameter("AddTime", DbType.DateTime, _FileInfoMDL.AddTime));
            p.Add(db.CreateParameter("UploadMan", DbType.String, _FileInfoMDL.UploadMan));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="FileInfoID">主键</param>
        /// <returns></returns>
        public static int Delete(string FileID)
        {
            return Delete(null, FileID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FileInfoID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string FileID)
        {
            string sql = @"DELETE FROM dbo.FileInfo WHERE FileID = @FileID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, FileID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(FileInfoMDL _FileInfoMDL)
        {
            return Delete(null, _FileInfoMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FileInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, FileInfoMDL _FileInfoMDL)
        {
            string sql = @"DELETE FROM dbo.FileInfo WHERE FileID = @FileID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, _FileInfoMDL.FileID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="FileInfoID">主键</param>
        public static FileInfoMDL GetObject(string FileID)
        {
            string sql = @"
			SELECT FileID,[FILENAME],FileSize,FileUrl,DataType,FileType,AddTime,UploadMan
			FROM dbo.FileInfo
			WHERE FileID = @FileID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("FileID", DbType.String, FileID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                FileInfoMDL _FileInfoMDL = null;
                if (reader.Read())
                {
                    _FileInfoMDL = new FileInfoMDL();
                    if (reader["FileID"] != DBNull.Value) _FileInfoMDL.FileID = Convert.ToString(reader["FileID"]);
                    if (reader["FileName"] != DBNull.Value) _FileInfoMDL.FileName = Convert.ToString(reader["FileName"]);
                    if (reader["FileSize"] != DBNull.Value) _FileInfoMDL.FileSize = Convert.ToInt64(reader["FileSize"]);
                    if (reader["FileUrl"] != DBNull.Value) _FileInfoMDL.FileUrl = Convert.ToString(reader["FileUrl"]);
                    if (reader["DataType"] != DBNull.Value) _FileInfoMDL.DataType = Convert.ToString(reader["DataType"]);
                    if (reader["FileType"] != DBNull.Value) _FileInfoMDL.FileType = Convert.ToString(reader["FileType"]);
                    if (reader["AddTime"] != DBNull.Value) _FileInfoMDL.AddTime = Convert.ToDateTime(reader["AddTime"]);
                    if (reader["UploadMan"] != DBNull.Value) _FileInfoMDL.UploadMan = Convert.ToString(reader["UploadMan"]);
                }
                reader.Close();
                db.Close();
                return _FileInfoMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.FileInfo", "*", filterWhereString, orderBy == "" ? " FileID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.FileInfo", filterWhereString);
        }

        #region 自定义方法
        /// <summary>
        /// 根据申请ID或注册编号获取附件信息
        /// </summary>
        /// <param name="ApplyID">申请单ID</param>
        /// <param name="PSN_RegisterNO">注册编号</param>
        /// <returns>附件集合信息</returns>
        public static DataTable GetListByApplyIDOrPSN_RegisterNO(string ApplyID, string PSN_RegisterNO)
        {
            string sql = @"SELECT f.[FileID]   
                        ,f.[FileName]
                        ,f.[FileSize]
                        ,f.[FileUrl]
                        ,f.[DataType]
                        ,f.[FileType]
                        ,f.[AddTime]
                        ,f.[UploadMan]
	                    ,a.[ApplyID]
                        ,a.[CheckResult]
                        ,a.[CheckDesc]
	                    ,p.[PSN_RegisterNO]
	                    ,p.[IsHistory]
                    FROM [dbo].[FileInfo] f
                    left join [dbo].[ApplyFile] a on f.[FileID] = a.[FileID]
                    left join [dbo].[COC_TOW_Person_File] p on f.[FileID] = p.[FileID]
                    where a.[ApplyID]='{0}' or p.[PSN_RegisterNO]='{1}'
                    order by 
                        case f.[DataType] 
                            when '一寸免冠照片' then 0
                            when '证件扫描件' then 1
                            when '学历证书扫描件' then 2
                            when '劳动合同扫描件' then 3
                            when '执业资格证书扫描件' then 4
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
                            else 15
                        end,
                        f.[FileName]";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID, PSN_RegisterNO));
        }

        /// <summary>
        /// 根据注册编号获取个人一寸免冠照片
        /// </summary>
        /// <param name="PSN_RegisterNO">证书注册编号</param>
        /// <returns></returns>
        public static string GetPersonPhotoByPSN_RegisterNO(string PSN_RegisterNO)
        {
            string sql = @"SELECT f.[FileUrl]  
                    FROM [dbo].[FileInfo] f
                    left join [dbo].[COC_TOW_Person_File] p on f.[FileID] = p.[FileID]
                    where p.[PSN_RegisterNO]='{0}' and f.[DataType] ='一寸免冠照片'";
            DataTable dt= CommonDAL.GetDataTable(string.Format(sql, PSN_RegisterNO));

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据申请单ID获取一寸免冠照片
        /// </summary>
        /// <param name="Applyid">申请ID</param>
        /// <returns>附件对象</returns>
        public static FileInfoMDL GetPersonPhotoByApplyid(string Applyid)
        {
            string sql = @"
			select a.* from [dbo].[FileInfo] a  inner join  dbo.ApplyFile  b on a.FileID=b.FileID  and DataType='一寸免冠照片' and b.ApplyID=@Applyid";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, Applyid));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                FileInfoMDL _FileInfoMDL = null;
                if (reader.Read())
                {
                    _FileInfoMDL = new FileInfoMDL();
                    if (reader["FileID"] != DBNull.Value) _FileInfoMDL.FileID = Convert.ToString(reader["FileID"]);
                    if (reader["FileName"] != DBNull.Value) _FileInfoMDL.FileName = Convert.ToString(reader["FileName"]);
                    if (reader["FileSize"] != DBNull.Value) _FileInfoMDL.FileSize = Convert.ToInt64(reader["FileSize"]);
                    if (reader["FileUrl"] != DBNull.Value) _FileInfoMDL.FileUrl = Convert.ToString(reader["FileUrl"]);
                    if (reader["DataType"] != DBNull.Value) _FileInfoMDL.DataType = Convert.ToString(reader["DataType"]);
                    if (reader["FileType"] != DBNull.Value) _FileInfoMDL.FileType = Convert.ToString(reader["FileType"]);
                    if (reader["AddTime"] != DBNull.Value) _FileInfoMDL.AddTime = Convert.ToDateTime(reader["AddTime"]);
                    if (reader["UploadMan"] != DBNull.Value) _FileInfoMDL.UploadMan = Convert.ToString(reader["UploadMan"]);
                }
                reader.Close();
                db.Close();
                return _FileInfoMDL;
            }
        }

        /// <summary>
        /// 根据注册编号获取个人手写签名照
        /// </summary>
        /// <param name="PSN_RegisterNO">造价师证书注册编号</param>
        /// <returns></returns>
        public static string GetSignPhotoByPSN_RegisterNO(string PSN_RegisterNO)
        {
            string sql = @"SELECT f.[FileUrl]  
                    FROM [dbo].[FileInfo] f
                    left join [dbo].[COC_TOW_Person_File] p on f.[FileID] = p.[FileID]
                    where p.[PSN_RegisterNO]='{0}' and f.[DataType] ='手写签名照'";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, PSN_RegisterNO));

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据申请单ID获取个人手写签名照片
        /// </summary>
        /// <param name="Applyid">申请ID</param>
        /// <returns>附件地址</returns>
        public static string GetSignPhotoByApplyid(string Applyid)
        {
            string sql = @"SELECT a.[FileUrl] FROM [dbo].[FileInfo] a inner join  dbo.ApplyFile  b on a.FileID=b.FileID  and DataType='手写签名照' and b.ApplyID='{0}'";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, Applyid));

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        #endregion
    }
}
