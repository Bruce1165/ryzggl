using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_ApplyContinueDAL(填写类描述)
	/// </summary>
    public class zjs_ApplyContinueDAL
    {
        public zjs_ApplyContinueDAL(){}

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            return Insert(null, _zjs_ApplyContinueMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.zjs_ApplyContinue(ApplyID,PSN_MobilePhone,PSN_Email,LinkMan,ENT_Telephone,ENT_MobilePhone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,PSN_CertificateValidity,BiXiu,XuanXiu,Remark,FR)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@LinkMan,@ENT_Telephone,@ENT_MobilePhone,@ENT_Correspondence,@ENT_Economic_Nature,@END_Addess,@ENT_Postcode,@PSN_CertificateValidity,@BiXiu,@XuanXiu,@Remark,@FR)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyContinueMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _zjs_ApplyContinueMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _zjs_ApplyContinueMDL.PSN_Email));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _zjs_ApplyContinueMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _zjs_ApplyContinueMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_MobilePhone", DbType.String, _zjs_ApplyContinueMDL.ENT_MobilePhone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _zjs_ApplyContinueMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _zjs_ApplyContinueMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _zjs_ApplyContinueMDL.END_Addess));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _zjs_ApplyContinueMDL.ENT_Postcode));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _zjs_ApplyContinueMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("BiXiu", DbType.Int32, _zjs_ApplyContinueMDL.BiXiu));
            p.Add(db.CreateParameter("XuanXiu", DbType.Int32, _zjs_ApplyContinueMDL.XuanXiu));
            p.Add(db.CreateParameter("Remark", DbType.String, _zjs_ApplyContinueMDL.Remark));
            p.Add(db.CreateParameter("FR", DbType.String, _zjs_ApplyContinueMDL.FR));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            return Update(null, _zjs_ApplyContinueMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            string sql = @"
			UPDATE dbo.zjs_ApplyContinue
				SET	PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_MobilePhone = @ENT_MobilePhone,ENT_Correspondence = @ENT_Correspondence,ENT_Economic_Nature = @ENT_Economic_Nature,END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,PSN_CertificateValidity = @PSN_CertificateValidity,BiXiu = @BiXiu,XuanXiu = @XuanXiu,Remark = @Remark,FR = @FR
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyContinueMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _zjs_ApplyContinueMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _zjs_ApplyContinueMDL.PSN_Email));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _zjs_ApplyContinueMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _zjs_ApplyContinueMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_MobilePhone", DbType.String, _zjs_ApplyContinueMDL.ENT_MobilePhone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _zjs_ApplyContinueMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _zjs_ApplyContinueMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _zjs_ApplyContinueMDL.END_Addess));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _zjs_ApplyContinueMDL.ENT_Postcode));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _zjs_ApplyContinueMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("BiXiu", DbType.Int32, _zjs_ApplyContinueMDL.BiXiu));
            p.Add(db.CreateParameter("XuanXiu", DbType.Int32, _zjs_ApplyContinueMDL.XuanXiu));
            p.Add(db.CreateParameter("Remark", DbType.String, _zjs_ApplyContinueMDL.Remark));
            p.Add(db.CreateParameter("FR", DbType.String, _zjs_ApplyContinueMDL.FR));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="zjs_ApplyContinueID">主键</param>
        /// <returns></returns>
        public static int Delete(string ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="zjs_ApplyContinueID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.zjs_ApplyContinue WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            return Delete(null, _zjs_ApplyContinueMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyContinueMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, zjs_ApplyContinueMDL _zjs_ApplyContinueMDL)
        {
            string sql = @"DELETE FROM dbo.zjs_ApplyContinue WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyContinueMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="zjs_ApplyContinueID">主键</param>
        public static zjs_ApplyContinueMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,PSN_MobilePhone,PSN_Email,LinkMan,ENT_Telephone,ENT_MobilePhone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,PSN_CertificateValidity,BiXiu,XuanXiu,Remark,FR
			FROM dbo.zjs_ApplyContinue
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyContinueMDL _zjs_ApplyContinueMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyContinueMDL = new zjs_ApplyContinueMDL();
                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyContinueMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_ApplyContinueMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_ApplyContinueMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["LinkMan"] != DBNull.Value) _zjs_ApplyContinueMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
                    if (reader["ENT_Telephone"] != DBNull.Value) _zjs_ApplyContinueMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                    if (reader["ENT_MobilePhone"] != DBNull.Value) _zjs_ApplyContinueMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
                    if (reader["ENT_Correspondence"] != DBNull.Value) _zjs_ApplyContinueMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                    if (reader["ENT_Economic_Nature"] != DBNull.Value) _zjs_ApplyContinueMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_ApplyContinueMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["ENT_Postcode"] != DBNull.Value) _zjs_ApplyContinueMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_ApplyContinueMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["BiXiu"] != DBNull.Value) _zjs_ApplyContinueMDL.BiXiu = Convert.ToInt32(reader["BiXiu"]);
                    if (reader["XuanXiu"] != DBNull.Value) _zjs_ApplyContinueMDL.XuanXiu = Convert.ToInt32(reader["XuanXiu"]);
                    if (reader["Remark"] != DBNull.Value) _zjs_ApplyContinueMDL.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["FR"] != DBNull.Value) _zjs_ApplyContinueMDL.FR = Convert.ToString(reader["FR"]);
                }
                reader.Close();
                db.Close();
                return _zjs_ApplyContinueMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_ApplyContinue", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_ApplyContinue", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
