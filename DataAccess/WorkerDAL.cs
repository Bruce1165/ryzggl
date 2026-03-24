using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--WorkerDAL(填写类描述)
    /// </summary>
    public class WorkerDAL
    {
        public WorkerDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(WorkerOB _WorkerOB)
        {
            return Insert(null, _WorkerOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, WorkerOB _WorkerOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.Worker(WorkerName,CertificateType,CertificateCode,Sex,Birthday,Nation,CulturalLevel,PoliticalBackground,Phone,Mobile,Email,Address,ZipCode,FacePhoto,FacePhotoUpdateTime,SignPhotoTime,IDCardPhotoUpdateTime,[UUID])
			VALUES (@WorkerName,@CertificateType,@CertificateCode,@Sex,@Birthday,@Nation,@CulturalLevel,@PoliticalBackground,@Phone,@Mobile,@Email,@Address,@ZipCode,@FacePhoto,@FacePhotoUpdateTime,@SignPhotoTime,@IDCardPhotoUpdateTime,@UUID);SELECT @WorkerID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("WorkerID", DbType.Int64));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _WorkerOB.WorkerName));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _WorkerOB.CertificateType));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _WorkerOB.CertificateCode));
            p.Add(db.CreateParameter("Sex", DbType.String, _WorkerOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _WorkerOB.Birthday));
            p.Add(db.CreateParameter("Nation", DbType.String, _WorkerOB.Nation));
            p.Add(db.CreateParameter("CulturalLevel", DbType.String, _WorkerOB.CulturalLevel));
            p.Add(db.CreateParameter("PoliticalBackground", DbType.String, _WorkerOB.PoliticalBackground));
            p.Add(db.CreateParameter("Phone", DbType.String, _WorkerOB.Phone));
            p.Add(db.CreateParameter("Mobile", DbType.String, _WorkerOB.Mobile));
            p.Add(db.CreateParameter("Email", DbType.String, _WorkerOB.Email));
            p.Add(db.CreateParameter("Address", DbType.String, _WorkerOB.Address));
            p.Add(db.CreateParameter("ZipCode", DbType.String, _WorkerOB.ZipCode));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, _WorkerOB.FacePhoto));
            p.Add(db.CreateParameter("FacePhotoUpdateTime", DbType.DateTime, _WorkerOB.FacePhotoUpdateTime));
            p.Add(db.CreateParameter("SignPhotoTime", DbType.DateTime, _WorkerOB.SignPhotoTime));
            p.Add(db.CreateParameter("IDCardPhotoUpdateTime", DbType.DateTime, _WorkerOB.IDCardPhotoUpdateTime));
            p.Add(db.CreateParameter("UUID", DbType.String, _WorkerOB.UUID));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _WorkerOB.WorkerID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(WorkerOB _WorkerOB)
        {
            return Update(null, _WorkerOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, WorkerOB _WorkerOB)
        {
            string sql = @"
			UPDATE dbo.Worker
				SET	WorkerName = @WorkerName,CertificateType = @CertificateType,CertificateCode = @CertificateCode,Sex = @Sex,Birthday = @Birthday,Nation = @Nation,CulturalLevel = @CulturalLevel,
                PoliticalBackground = @PoliticalBackground,Phone = @Phone,Mobile = @Mobile,Email = @Email,Address = @Address,ZipCode = @ZipCode,FacePhoto = @FacePhoto,
                FacePhotoUpdateTime = @FacePhotoUpdateTime,SignPhotoTime = @SignPhotoTime,IDCardPhotoUpdateTime = @IDCardPhotoUpdateTime,UUID = @UUID
			WHERE
				WorkerID = @WorkerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _WorkerOB.WorkerID));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _WorkerOB.WorkerName));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _WorkerOB.CertificateType));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _WorkerOB.CertificateCode));
            p.Add(db.CreateParameter("Sex", DbType.String, _WorkerOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _WorkerOB.Birthday));
            p.Add(db.CreateParameter("Nation", DbType.String, _WorkerOB.Nation));
            p.Add(db.CreateParameter("CulturalLevel", DbType.String, _WorkerOB.CulturalLevel));
            p.Add(db.CreateParameter("PoliticalBackground", DbType.String, _WorkerOB.PoliticalBackground));
            p.Add(db.CreateParameter("Phone", DbType.String, _WorkerOB.Phone));
            p.Add(db.CreateParameter("Mobile", DbType.String, _WorkerOB.Mobile));
            p.Add(db.CreateParameter("Email", DbType.String, _WorkerOB.Email));
            p.Add(db.CreateParameter("Address", DbType.String, _WorkerOB.Address));
            p.Add(db.CreateParameter("ZipCode", DbType.String, _WorkerOB.ZipCode));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, _WorkerOB.FacePhoto));
            p.Add(db.CreateParameter("FacePhotoUpdateTime", DbType.DateTime, _WorkerOB.FacePhotoUpdateTime));
            p.Add(db.CreateParameter("SignPhotoTime", DbType.DateTime, _WorkerOB.SignPhotoTime));
            p.Add(db.CreateParameter("IDCardPhotoUpdateTime", DbType.DateTime, _WorkerOB.IDCardPhotoUpdateTime));
            p.Add(db.CreateParameter("UUID", DbType.String, _WorkerOB.UUID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="WorkerID">主键</param>
        /// <returns></returns>
        public static int Delete(long WorkerID)
        {
            return Delete(null, WorkerID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="WorkerID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long WorkerID)
        {
            string sql = @"DELETE FROM dbo.Worker WHERE WorkerID = @WorkerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, WorkerID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(WorkerOB _WorkerOB)
        {
            return Delete(null, _WorkerOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="WorkerOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, WorkerOB _WorkerOB)
        {
            string sql = @"DELETE FROM dbo.Worker WHERE WorkerID = @WorkerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _WorkerOB.WorkerID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="WorkerID">主键</param>
        public static WorkerOB GetObject(long WorkerID)
        {
            string sql = @"
			SELECT WorkerID,WorkerName,CertificateType,CertificateCode,Sex,Birthday,Nation,CulturalLevel,PoliticalBackground,Phone,Mobile,Email,Address,ZipCode,FacePhoto,FacePhotoUpdateTime,SignPhotoTime,IDCardPhotoUpdateTime,UUID
			FROM dbo.Worker
			WHERE WorkerID = @WorkerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, WorkerID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    WorkerOB _WorkerOB = null;
                    if (reader.Read())
                    {
                        _WorkerOB = new WorkerOB();
                        if (reader["WorkerID"] != DBNull.Value) _WorkerOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["WorkerName"] != DBNull.Value) _WorkerOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _WorkerOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _WorkerOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["Sex"] != DBNull.Value) _WorkerOB.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) _WorkerOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["Nation"] != DBNull.Value) _WorkerOB.Nation = Convert.ToString(reader["Nation"]);
                        if (reader["CulturalLevel"] != DBNull.Value) _WorkerOB.CulturalLevel = Convert.ToString(reader["CulturalLevel"]);
                        if (reader["PoliticalBackground"] != DBNull.Value) _WorkerOB.PoliticalBackground = Convert.ToString(reader["PoliticalBackground"]);
                        if (reader["Phone"] != DBNull.Value) _WorkerOB.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["Mobile"] != DBNull.Value) _WorkerOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Email"] != DBNull.Value) _WorkerOB.Email = Convert.ToString(reader["Email"]);
                        if (reader["Address"] != DBNull.Value) _WorkerOB.Address = Convert.ToString(reader["Address"]);
                        if (reader["ZipCode"] != DBNull.Value) _WorkerOB.ZipCode = Convert.ToString(reader["ZipCode"]);
                        if (reader["FacePhoto"] != DBNull.Value) _WorkerOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["FacePhotoUpdateTime"] != DBNull.Value) _WorkerOB.FacePhotoUpdateTime = Convert.ToDateTime(reader["FacePhotoUpdateTime"]);
                        if (reader["SignPhotoTime"] != DBNull.Value) _WorkerOB.SignPhotoTime = Convert.ToDateTime(reader["SignPhotoTime"]);
                        if (reader["IDCardPhotoUpdateTime"] != DBNull.Value) _WorkerOB.IDCardPhotoUpdateTime = Convert.ToDateTime(reader["IDCardPhotoUpdateTime"]);
                        if (reader["UUID"] != DBNull.Value) _WorkerOB.UUID = Convert.ToString(reader["UUID"]);
                        
                    }
                    reader.Close();
                    db.Close();
                    return _WorkerOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Worker", "*", filterWhereString, orderBy == "" ? " WorkerID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Worker", filterWhereString);
        }

         /// <summary>
        /// 根据证件号码获取单个实体
        /// </summary>
        /// <param name="CertificateCode">身份证号</param>
        public static WorkerOB GetUserObject(string CertificateCode)
        {
            return GetUserObject(null, CertificateCode);
        }

        /// <summary>
        /// 根据证件号码获取单个实体
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateCode">身份证号</param>
        public static WorkerOB GetUserObject(DbTransaction tran, string CertificateCode)
        {
            string sql = @"
			SELECT WorkerID,WorkerName,CertificateType,CertificateCode,Sex,Birthday,Nation,CulturalLevel,PoliticalBackground,Phone,Mobile,Email,Address,ZipCode,FacePhoto,FacePhotoUpdateTime,SignPhotoTime,IDCardPhotoUpdateTime,UUID
			FROM dbo.Worker
			WHERE CertificateCode = @CertificateCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, CertificateCode));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    WorkerOB _WorkerOB = null;
                    if (reader.Read())
                    {
                        _WorkerOB = new WorkerOB();
                        if (reader["WorkerID"] != DBNull.Value) _WorkerOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["WorkerName"] != DBNull.Value) _WorkerOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _WorkerOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _WorkerOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["Sex"] != DBNull.Value) _WorkerOB.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) _WorkerOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["Nation"] != DBNull.Value) _WorkerOB.Nation = Convert.ToString(reader["Nation"]);
                        if (reader["CulturalLevel"] != DBNull.Value) _WorkerOB.CulturalLevel = Convert.ToString(reader["CulturalLevel"]);
                        if (reader["PoliticalBackground"] != DBNull.Value) _WorkerOB.PoliticalBackground = Convert.ToString(reader["PoliticalBackground"]);
                        if (reader["Phone"] != DBNull.Value) _WorkerOB.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["Mobile"] != DBNull.Value) _WorkerOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Email"] != DBNull.Value) _WorkerOB.Email = Convert.ToString(reader["Email"]);
                        if (reader["Address"] != DBNull.Value) _WorkerOB.Address = Convert.ToString(reader["Address"]);
                        if (reader["ZipCode"] != DBNull.Value) _WorkerOB.ZipCode = Convert.ToString(reader["ZipCode"]);
                        if (reader["FacePhoto"] != DBNull.Value) _WorkerOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["FacePhotoUpdateTime"] != DBNull.Value) _WorkerOB.FacePhotoUpdateTime = Convert.ToDateTime(reader["FacePhotoUpdateTime"]);
                        if (reader["SignPhotoTime"] != DBNull.Value) _WorkerOB.SignPhotoTime = Convert.ToDateTime(reader["SignPhotoTime"]);
                        if (reader["IDCardPhotoUpdateTime"] != DBNull.Value) _WorkerOB.IDCardPhotoUpdateTime = Convert.ToDateTime(reader["IDCardPhotoUpdateTime"]);
                        if (reader["UUID"] != DBNull.Value) _WorkerOB.UUID = Convert.ToString(reader["UUID"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return _WorkerOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 临时测试使用 2021-04-16 压力测试
        /// </summary>
        public static WorkerOB GetUserByName(string uName)
        {
            string sql = @"
			SELECT top 1 WorkerID,WorkerName,CertificateType,CertificateCode,Sex,Birthday,Nation,CulturalLevel,PoliticalBackground,Phone,Mobile,Email,Address,ZipCode,FacePhoto,FacePhotoUpdateTime,SignPhotoTime,IDCardPhotoUpdateTime
			FROM dbo.Worker
			WHERE WorkerName = @WorkerName";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerName", DbType.String, uName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    WorkerOB _WorkerOB = null;
                    if (reader.Read())
                    {
                        _WorkerOB = new WorkerOB();
                        if (reader["WorkerID"] != DBNull.Value) _WorkerOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["WorkerName"] != DBNull.Value) _WorkerOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _WorkerOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _WorkerOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["Sex"] != DBNull.Value) _WorkerOB.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) _WorkerOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["Nation"] != DBNull.Value) _WorkerOB.Nation = Convert.ToString(reader["Nation"]);
                        if (reader["CulturalLevel"] != DBNull.Value) _WorkerOB.CulturalLevel = Convert.ToString(reader["CulturalLevel"]);
                        if (reader["PoliticalBackground"] != DBNull.Value) _WorkerOB.PoliticalBackground = Convert.ToString(reader["PoliticalBackground"]);
                        if (reader["Phone"] != DBNull.Value) _WorkerOB.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["Mobile"] != DBNull.Value) _WorkerOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Email"] != DBNull.Value) _WorkerOB.Email = Convert.ToString(reader["Email"]);
                        if (reader["Address"] != DBNull.Value) _WorkerOB.Address = Convert.ToString(reader["Address"]);
                        if (reader["ZipCode"] != DBNull.Value) _WorkerOB.ZipCode = Convert.ToString(reader["ZipCode"]);
                        if (reader["FacePhoto"] != DBNull.Value) _WorkerOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["FacePhotoUpdateTime"] != DBNull.Value) _WorkerOB.FacePhotoUpdateTime = Convert.ToDateTime(reader["FacePhotoUpdateTime"]);
                        if (reader["SignPhotoTime"] != DBNull.Value) _WorkerOB.SignPhotoTime = Convert.ToDateTime(reader["SignPhotoTime"]);
                        if (reader["IDCardPhotoUpdateTime"] != DBNull.Value) _WorkerOB.IDCardPhotoUpdateTime = Convert.ToDateTime(reader["IDCardPhotoUpdateTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _WorkerOB;
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
