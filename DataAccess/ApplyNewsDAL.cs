using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ApplyNewsDAL(填写类描述)
    /// </summary>
    public class ApplyNewsDAL
    {
        public ApplyNewsDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(ApplyNewsMDL _ApplyNewsMDL)
        {
            return Insert(null, _ApplyNewsMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, ApplyNewsMDL _ApplyNewsMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.ApplyNews([ID],PSN_Name,PSN_CertificateNO,PSN_RegisterNo,ApplyType,SFCK,ENT_OrganizationsCode)
			VALUES (@ID,@PSN_Name,@PSN_CertificateNO,@PSN_RegisterNo,@ApplyType,@SFCK,@ENT_OrganizationsCode)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _ApplyNewsMDL.ID));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _ApplyNewsMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _ApplyNewsMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyNewsMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _ApplyNewsMDL.ApplyType));
            p.Add(db.CreateParameter("SFCK", DbType.Boolean, _ApplyNewsMDL.SFCK));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _ApplyNewsMDL.ENT_OrganizationsCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(ApplyNewsMDL _ApplyNewsMDL)
        {
            return Update(null, _ApplyNewsMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ApplyNewsMDL _ApplyNewsMDL)
        {
            string sql = @"
			UPDATE dbo.ApplyNews
				SET	PSN_Name = @PSN_Name,PSN_CertificateNO = @PSN_CertificateNO,PSN_RegisterNo = @PSN_RegisterNo,ApplyType = @ApplyType,SFCK = @SFCK,ENT_OrganizationsCode = @ENT_OrganizationsCode
			WHERE
				ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _ApplyNewsMDL.ID));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _ApplyNewsMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _ApplyNewsMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyNewsMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _ApplyNewsMDL.ApplyType));
            p.Add(db.CreateParameter("SFCK", DbType.Boolean, _ApplyNewsMDL.SFCK));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _ApplyNewsMDL.ENT_OrganizationsCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ApplyNewsID">主键</param>
        /// <returns></returns>
        public static int Delete(string ID)
        {
            return Delete(null, ID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyNewsID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ID)
        {
            string sql = @"DELETE FROM dbo.ApplyNews WHERE ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ApplyNewsMDL _ApplyNewsMDL)
        {
            return Delete(null, _ApplyNewsMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ApplyNewsMDL _ApplyNewsMDL)
        {
            string sql = @"DELETE FROM dbo.ApplyNews WHERE ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _ApplyNewsMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyNewsID">主键</param>
        public static ApplyNewsMDL GetObject(string ID)
        {
            string sql = @"
			SELECT [ID],PSN_Name,PSN_CertificateNO,PSN_RegisterNo,ApplyType,SFCK,ENT_OrganizationsCode
			FROM dbo.ApplyNews
			WHERE ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyNewsMDL _ApplyNewsMDL = null;
                if (reader.Read())
                {
                    _ApplyNewsMDL = new ApplyNewsMDL();
                    if (reader["ID"] != DBNull.Value) _ApplyNewsMDL.ID = Convert.ToString(reader["ID"]);
                    if (reader["PSN_Name"] != DBNull.Value) _ApplyNewsMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _ApplyNewsMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _ApplyNewsMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["ApplyType"] != DBNull.Value) _ApplyNewsMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["SFCK"] != DBNull.Value) _ApplyNewsMDL.SFCK = Convert.ToBoolean(reader["SFCK"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _ApplyNewsMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                }
                reader.Close();
                db.Close();
                return _ApplyNewsMDL;
            }
        }
        /// <summary>
        /// 根据组织机构代码获取ApplyNewsMDL信息表的数据
        /// </summary>
        /// <param name="ApplyNewsID">组织机构代码</param>
        public static List<ApplyNewsMDL> GetList(string ENT_OrganizationsCode)
        {
            string sql = @"
			SELECT [ID],PSN_Name,PSN_CertificateNO,PSN_RegisterNo,ApplyType,SFCK,ENT_OrganizationsCode
			FROM dbo.ApplyNews
			WHERE ENT_OrganizationsCode = @ENT_OrganizationsCode AND SFCK=0";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, ENT_OrganizationsCode));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<ApplyNewsMDL> _ListApplyNewsMDL = new List<ApplyNewsMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ApplyNewsMDL _ApplyNewsMDL = new ApplyNewsMDL();
                        if (dt.Rows[i]["ID"] != DBNull.Value) _ApplyNewsMDL.ID = Convert.ToString(dt.Rows[i]["ID"]);
                        if (dt.Rows[i]["PSN_Name"] != DBNull.Value) _ApplyNewsMDL.PSN_Name = Convert.ToString(dt.Rows[i]["PSN_Name"]);
                        if (dt.Rows[i]["PSN_CertificateNO"] != DBNull.Value) _ApplyNewsMDL.PSN_CertificateNO = Convert.ToString(dt.Rows[i]["PSN_CertificateNO"]);
                        if (dt.Rows[i]["PSN_RegisterNo"] != DBNull.Value) _ApplyNewsMDL.PSN_RegisterNo = Convert.ToString(dt.Rows[i]["PSN_RegisterNo"]);
                        if (dt.Rows[i]["ApplyType"] != DBNull.Value) _ApplyNewsMDL.ApplyType = Convert.ToString(dt.Rows[i]["ApplyType"]);
                        if (dt.Rows[i]["SFCK"] != DBNull.Value) _ApplyNewsMDL.SFCK = Convert.ToBoolean(dt.Rows[i]["SFCK"]);
                        if (dt.Rows[i]["ENT_OrganizationsCode"] != DBNull.Value) _ApplyNewsMDL.ENT_OrganizationsCode = Convert.ToString(dt.Rows[i]["ENT_OrganizationsCode"]);
                        _ListApplyNewsMDL.Add(_ApplyNewsMDL);
                    }
                }
                db.Close();
                return _ListApplyNewsMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyNews", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyNews", filterWhereString);
        }

        #region 自定义方法

        #endregion
    }
}
