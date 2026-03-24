using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ApplyCancelDAL(填写类描述)
    /// </summary>
    public class ApplyCancelDAL
    {
        public ApplyCancelDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(ApplyCancelMDL _ApplyCancelMDL)
        {
            return Insert(null, _ApplyCancelMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, ApplyCancelMDL _ApplyCancelMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.ApplyCancel(ApplyID,ENT_Correspondence,ENT_Postcode,ENT_Telephone,PSN_MobilePhone,PSN_RegisterCertificateNo,PSN_RegisterNO,RegisterValidity,CancelReason,PSN_Email,LinkMan,ApplyManType,ZyIDItem,ZyItem,Nation)
			VALUES (@ApplyID,@ENT_Correspondence,@ENT_Postcode,@ENT_Telephone,@PSN_MobilePhone,@PSN_RegisterCertificateNo,@PSN_RegisterNO,@RegisterValidity,@CancelReason,@PSN_Email,@LinkMan,@ApplyManType,@ZyIDItem,@ZyItem,@Nation)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyCancelMDL.ApplyID));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _ApplyCancelMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _ApplyCancelMDL.ENT_Postcode));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _ApplyCancelMDL.ENT_Telephone));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _ApplyCancelMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _ApplyCancelMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _ApplyCancelMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("RegisterValidity", DbType.DateTime, _ApplyCancelMDL.RegisterValidity));
            p.Add(db.CreateParameter("CancelReason", DbType.String, _ApplyCancelMDL.CancelReason));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _ApplyCancelMDL.PSN_Email));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ApplyCancelMDL.LinkMan));
            p.Add(db.CreateParameter("ApplyManType", DbType.String, _ApplyCancelMDL.ApplyManType));
            p.Add(db.CreateParameter("ZyIDItem", DbType.String, _ApplyCancelMDL.ZyIDItem));
            p.Add(db.CreateParameter("ZyItem", DbType.String, _ApplyCancelMDL.ZyItem));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyCancelMDL.Nation));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(ApplyCancelMDL _ApplyCancelMDL)
        {
            return Update(null, _ApplyCancelMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ApplyCancelMDL _ApplyCancelMDL)
        {
            string sql = @"
			UPDATE dbo.ApplyCancel
				SET	ENT_Correspondence = @ENT_Correspondence,ENT_Postcode = @ENT_Postcode,ENT_Telephone = @ENT_Telephone,PSN_MobilePhone = @PSN_MobilePhone,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisterNO = @PSN_RegisterNO,RegisterValidity = @RegisterValidity,CancelReason = @CancelReason,PSN_Email = @PSN_Email,LinkMan = @LinkMan,ApplyManType = @ApplyManType,ZyIDItem = @ZyIDItem,ZyItem = @ZyItem,Nation = @Nation
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyCancelMDL.ApplyID));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _ApplyCancelMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _ApplyCancelMDL.ENT_Postcode));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _ApplyCancelMDL.ENT_Telephone));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _ApplyCancelMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _ApplyCancelMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _ApplyCancelMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("RegisterValidity", DbType.DateTime, _ApplyCancelMDL.RegisterValidity));
            p.Add(db.CreateParameter("CancelReason", DbType.String, _ApplyCancelMDL.CancelReason));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _ApplyCancelMDL.PSN_Email));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ApplyCancelMDL.LinkMan));
            p.Add(db.CreateParameter("ApplyManType", DbType.String, _ApplyCancelMDL.ApplyManType));
            p.Add(db.CreateParameter("ZyIDItem", DbType.String, _ApplyCancelMDL.ZyIDItem));
            p.Add(db.CreateParameter("ZyItem", DbType.String, _ApplyCancelMDL.ZyItem));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyCancelMDL.Nation));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ApplyCancelID">主键</param>
        /// <returns></returns>
        public static int Delete(string ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyCancelID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.ApplyCancel WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ApplyCancelMDL _ApplyCancelMDL)
        {
            return Delete(null, _ApplyCancelMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyCancelMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ApplyCancelMDL _ApplyCancelMDL)
        {
            string sql = @"DELETE FROM dbo.ApplyCancel WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyCancelMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyCancelID">主键</param>
        public static ApplyCancelMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,Nation,ENT_Correspondence,ENT_Postcode,ENT_Telephone,PSN_MobilePhone,PSN_RegisterCertificateNo,PSN_RegisterNO,RegisterValidity,CancelReason,PSN_Email,LinkMan,ApplyManType,ZyIDItem,ZyItem
			FROM dbo.ApplyCancel
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyCancelMDL _ApplyCancelMDL = null;
                if (reader.Read())
                {
                    _ApplyCancelMDL = new ApplyCancelMDL();
                    if (reader["ApplyID"] != DBNull.Value) _ApplyCancelMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["Nation"] != DBNull.Value) _ApplyCancelMDL.Nation = Convert.ToString(reader["Nation"]);
                    if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyCancelMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                    if (reader["ENT_Postcode"] != DBNull.Value) _ApplyCancelMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                    if (reader["ENT_Telephone"] != DBNull.Value) _ApplyCancelMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyCancelMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyCancelMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _ApplyCancelMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["RegisterValidity"] != DBNull.Value) _ApplyCancelMDL.RegisterValidity = Convert.ToDateTime(reader["RegisterValidity"]);
                    if (reader["CancelReason"] != DBNull.Value) _ApplyCancelMDL.CancelReason = Convert.ToString(reader["CancelReason"]);
                    if (reader["PSN_Email"] != DBNull.Value) _ApplyCancelMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["LinkMan"] != DBNull.Value) _ApplyCancelMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
                    if (reader["ApplyManType"] != DBNull.Value) _ApplyCancelMDL.ApplyManType = Convert.ToString(reader["ApplyManType"]);
                    if (reader["ZyIDItem"] != DBNull.Value) _ApplyCancelMDL.ZyIDItem = Convert.ToString(reader["ZyIDItem"]);
                    if (reader["ZyItem"] != DBNull.Value) _ApplyCancelMDL.ZyItem = Convert.ToString(reader["ZyItem"]);
                }
                reader.Close();
                db.Close();
                return _ApplyCancelMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyCancel", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyCancel", filterWhereString);
        }

        #region 自定义方法

        #endregion
    }
}
