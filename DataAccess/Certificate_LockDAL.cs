using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Certificate_LockDAL
    {
        public Certificate_LockDAL() { }

       
        
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(Certificate_LockMDL _Certificate_LockMDL)
        {
            return Insert(null, _Certificate_LockMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran,Certificate_LockMDL _Certificate_LockMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO [dbo].[CertificateLock_JZS] (Id,Fid,LockStartTime,LockContent,LockPeople,LockEndTime,UnlockTime,UnlockContent,UnlockPeople,LX,LockStates,PSN_RegisterCertificateNo,PSN_Name,PSN_CertificateNO)
			VALUES (@Id,@Fid,@LockStartTime,@LockContent,@LockPeople,@LockEndTime,@UnlockTime,@UnlockContent,@UnlockPeople,@LX,@LockStates,@PSN_RegisterCertificateNo,@PSN_Name,@PSN_CertificateNO)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Id", DbType.String, _Certificate_LockMDL.Id));
            p.Add(db.CreateParameter("Fid", DbType.String, _Certificate_LockMDL.Fid));
            p.Add(db.CreateParameter("LockStartTime", DbType.DateTime, _Certificate_LockMDL.LockStartTime));
            p.Add(db.CreateParameter("LockContent", DbType.String, _Certificate_LockMDL.LockContent));
            p.Add(db.CreateParameter("LockPeople", DbType.String, _Certificate_LockMDL.LockPeople));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _Certificate_LockMDL.LockEndTime));
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _Certificate_LockMDL.UnlockTime));
            p.Add(db.CreateParameter("UnlockContent", DbType.String, _Certificate_LockMDL.UnlockContent));
            p.Add(db.CreateParameter("UnlockPeople", DbType.String, _Certificate_LockMDL.UnlockPeople));
            p.Add(db.CreateParameter("LX", DbType.String, _Certificate_LockMDL.LX));
            p.Add(db.CreateParameter("LockStates", DbType.String, _Certificate_LockMDL.LockStates));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _Certificate_LockMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _Certificate_LockMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _Certificate_LockMDL.PSN_CertificateNO));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(Certificate_LockMDL _Certificate_LockMDL)
        {
            return Update(null, _Certificate_LockMDL);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, Certificate_LockMDL _Certificate_LockMDL)
        {
            string sql = @"
			UPDATE [dbo].[CertificateLock_JZS]
				SET	UnlockTime = @UnlockTime,UnlockContent = @UnlockContent,UnlockPeople = @UnlockPeople,LockStates = @LockStates,LockEndTime=@LockEndTime
			WHERE
				Fid = @Fid AND LX=@LX and LockEndTime=(select max(LockEndTime) from [dbo].[CertificateLock_JZS] where Fid = @Fid AND LX=@LX) ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Fid", DbType.String, _Certificate_LockMDL.Fid));
            p.Add(db.CreateParameter("LockStartTime", DbType.DateTime, _Certificate_LockMDL.LockStartTime));
            p.Add(db.CreateParameter("LockContent", DbType.String, _Certificate_LockMDL.LockContent));
            p.Add(db.CreateParameter("LockPeople", DbType.String, _Certificate_LockMDL.LockPeople));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _Certificate_LockMDL.LockEndTime));
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _Certificate_LockMDL.UnlockTime));
            p.Add(db.CreateParameter("UnlockContent", DbType.String, _Certificate_LockMDL.UnlockContent));
            p.Add(db.CreateParameter("UnlockPeople", DbType.String, _Certificate_LockMDL.UnlockPeople));
            p.Add(db.CreateParameter("LX", DbType.String, _Certificate_LockMDL.LX));
            p.Add(db.CreateParameter("LockStates", DbType.String, _Certificate_LockMDL.LockStates));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _Certificate_LockMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _Certificate_LockMDL.PSN_Name));
            
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        public static int Insert(Certificate_LockHisMDL _Certificate_LockHisMDL)
        {
            return Insert(null, _Certificate_LockHisMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, Certificate_LockHisMDL _Certificate_LockHisMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO [dbo].[CertificateLock_His] (HisId,Id,Fid,LockStartTime,LockContent,LockPeople,LockEndTime,UnlockTime,UnlockContent,UnlockPeople,LX,LockStates,PSN_RegisterCertificateNo,PSN_Name,WriteDateTime,PSN_CertificateNO)
			VALUES (@HisId,@Id,@Fid,@LockStartTime,@LockContent,@LockPeople,@LockEndTime,@UnlockTime,@UnlockContent,@UnlockPeople,@LX,@LockStates,@PSN_RegisterCertificateNo,@PSN_Name,@WriteDateTime,@PSN_CertificateNO)";

            List<SqlParameter> p = new List<SqlParameter>();

            p.Add(db.CreateParameter("HisId", DbType.String, _Certificate_LockHisMDL.HisId));
            p.Add(db.CreateParameter("Id", DbType.String, _Certificate_LockHisMDL.Id));
            p.Add(db.CreateParameter("Fid", DbType.String, _Certificate_LockHisMDL.Fid));
            p.Add(db.CreateParameter("LockStartTime", DbType.DateTime, _Certificate_LockHisMDL.LockStartTime));
            p.Add(db.CreateParameter("LockContent", DbType.String, _Certificate_LockHisMDL.LockContent));
            p.Add(db.CreateParameter("LockPeople", DbType.String, _Certificate_LockHisMDL.LockPeople));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _Certificate_LockHisMDL.LockEndTime));
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _Certificate_LockHisMDL.UnlockTime));
            p.Add(db.CreateParameter("UnlockContent", DbType.String, _Certificate_LockHisMDL.UnlockContent));
            p.Add(db.CreateParameter("UnlockPeople", DbType.String, _Certificate_LockHisMDL.UnlockPeople));
            p.Add(db.CreateParameter("LX", DbType.String, _Certificate_LockHisMDL.LX));
            p.Add(db.CreateParameter("LockStates", DbType.String, _Certificate_LockHisMDL.LockStates));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _Certificate_LockHisMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _Certificate_LockHisMDL.PSN_Name));
            p.Add(db.CreateParameter("WriteDateTime", DbType.DateTime, _Certificate_LockHisMDL.WriteDateTime));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _Certificate_LockHisMDL.PSN_CertificateNO));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 获取锁定列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetLockList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[dbo].[CertificateLock_His]", "*", filterWhereString, orderBy == "" ? "WriteDateTime desc" : orderBy);
        }
        /// <summary>
        /// 锁定列表总个数
        /// </summary>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int GetCountLockList(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("[dbo].[CertificateLock_His]", filterWhereString);

        }


        //public static int LockIsorNOT(string PSN_CertificateNO)
        //{
        //    //string Level = PSN_Level == "二级" ? "二级建造师" : "二级临时建造师";
        //    string sql = string.Format("and PSN_CertificateNO='{0}'and (LX='二级建造师' or LX='二级' )and LockEndTime >GETDATE()", PSN_CertificateNO);
        //    return CommonDAL.SelectRowCount("[dbo].[CertificateLock_JZS]", sql);
        //}


        //public static int Batch_LockIsorNot(string PSN_CertificateNO)
        //{
        //    //string Level = PSN_Level == "二级" ? "二级建造师" : "二级临时建造师";
        //    string sql = string.Format("and PSN_CertificateNO in ({0}) and (LX='二级建造师' or LX='二级' )and LockEndTime >GETDATE()", PSN_CertificateNO);
        //    return CommonDAL.SelectRowCount("[dbo].[CertificateLock_JZS]", sql);
        
        //}
     
    }
}
