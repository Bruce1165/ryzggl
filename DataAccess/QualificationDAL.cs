using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--QualificationDAL(填写类描述)
    /// </summary>
    public class QualificationDAL
    {
        public QualificationDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(QualificationMDL _QualificationMDL)
        {
            return Insert(null, _QualificationMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, QualificationMDL _QualificationMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.Qualification(ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ])
			VALUES (@ZGZSBH,@XM,@ZJHM,@SF,@GZDW,@QDNF,@QDFS,@GLH,@ZYLB,@BYXX,@BYSJ,@SXZY,@ZGXL,@QFSJ)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _QualificationMDL.ZGZSBH));
            p.Add(db.CreateParameter("XM", DbType.String, _QualificationMDL.XM));
            p.Add(db.CreateParameter("ZJHM", DbType.String, _QualificationMDL.ZJHM));
            p.Add(db.CreateParameter("SF", DbType.String, _QualificationMDL.SF));
            p.Add(db.CreateParameter("GZDW", DbType.String, _QualificationMDL.GZDW));
            p.Add(db.CreateParameter("QDNF", DbType.String, _QualificationMDL.QDNF));
            p.Add(db.CreateParameter("QDFS", DbType.String, _QualificationMDL.QDFS));
            p.Add(db.CreateParameter("GLH", DbType.String, _QualificationMDL.GLH));
            p.Add(db.CreateParameter("ZYLB", DbType.String, _QualificationMDL.ZYLB));

            p.Add(db.CreateParameter("BYXX", DbType.String, _QualificationMDL.BYXX));
            p.Add(db.CreateParameter("BYSJ", DbType.DateTime, _QualificationMDL.BYSJ));
            p.Add(db.CreateParameter("SXZY", DbType.String, _QualificationMDL.SXZY));
            p.Add(db.CreateParameter("ZGXL", DbType.String, _QualificationMDL.ZGXL));
            p.Add(db.CreateParameter("QFSJ", DbType.DateTime, _QualificationMDL.QFSJ));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(QualificationMDL _QualificationMDL)
        {
            return Update(null, _QualificationMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, QualificationMDL _QualificationMDL)
        {
            string sql = @"
			UPDATE dbo.Qualification
				SET	XM = @XM,ZJHM = @ZJHM,SF = @SF,GZDW = @GZDW,QDNF = @QDNF,QDFS = @QDFS,ZGZSBH= @GLH,ZYLB = @ZYLB,BYXX = @BYXX,BYSJ = @BYSJ,SXZY = @SXZY,ZGXL = @ZGXL,QFSJ = @QFSJ
			WHERE
				ZGZSBH = @ZGZSBH";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _QualificationMDL.ZGZSBH));
            p.Add(db.CreateParameter("XM", DbType.String, _QualificationMDL.XM));
            p.Add(db.CreateParameter("ZJHM", DbType.String, _QualificationMDL.ZJHM));
            p.Add(db.CreateParameter("SF", DbType.String, _QualificationMDL.SF));
            p.Add(db.CreateParameter("GZDW", DbType.String, _QualificationMDL.GZDW));
            p.Add(db.CreateParameter("QDNF", DbType.String, _QualificationMDL.QDNF));
            p.Add(db.CreateParameter("QDFS", DbType.String, _QualificationMDL.QDFS));
            p.Add(db.CreateParameter("GLH", DbType.String, _QualificationMDL.GLH));
            p.Add(db.CreateParameter("ZYLB", DbType.String, _QualificationMDL.ZYLB));
            p.Add(db.CreateParameter("BYXX", DbType.String, _QualificationMDL.BYXX));
            p.Add(db.CreateParameter("BYSJ", DbType.DateTime, _QualificationMDL.BYSJ));
            p.Add(db.CreateParameter("SXZY", DbType.String, _QualificationMDL.SXZY));
            p.Add(db.CreateParameter("ZGXL", DbType.String, _QualificationMDL.ZGXL));
            p.Add(db.CreateParameter("QFSJ", DbType.DateTime, _QualificationMDL.QFSJ));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="QualificationID">主键</param>
        /// <returns></returns>
        public static int Delete(string ZGZSBH)
        {
            return Delete(null, ZGZSBH);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationID">主键</param>
        /// <returns></returns>
        public static int DeleteAdd(DbTransaction tran, string ZGZSBH)
        {
            string sql = @"DELETE FROM dbo.Qualification WHERE ZGZSBH IN (" + ZGZSBH + ")";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran, sql);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ZGZSBH)
        {
            string sql = @"DELETE FROM dbo.Qualification WHERE ZGZSBH = @ZGZSBH";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(QualificationMDL _QualificationMDL)
        {
            return Delete(null, _QualificationMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="QualificationMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, QualificationMDL _QualificationMDL)
        {
            string sql = @"DELETE FROM dbo.Qualification WHERE ZGZSBH = @ZGZSBH";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _QualificationMDL.ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="QualificationID">主键</param>
        public static QualificationMDL GetObject(string ZGZSBH)
        {
            string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ]
			FROM dbo.Qualification
			WHERE ZGZSBH = @ZGZSBH";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, ZGZSBH));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                QualificationMDL _QualificationMDL = null;
                if (reader.Read())
                {
                    _QualificationMDL = new QualificationMDL();
                    if (reader["ZGZSBH"] != DBNull.Value) _QualificationMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["XM"] != DBNull.Value) _QualificationMDL.XM = Convert.ToString(reader["XM"]);
                    if (reader["ZJHM"] != DBNull.Value) _QualificationMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
                    if (reader["SF"] != DBNull.Value) _QualificationMDL.SF = Convert.ToString(reader["SF"]);
                    if (reader["GZDW"] != DBNull.Value) _QualificationMDL.GZDW = Convert.ToString(reader["GZDW"]);
                    if (reader["QDNF"] != DBNull.Value) _QualificationMDL.QDNF = Convert.ToString(reader["QDNF"]);
                    if (reader["QDFS"] != DBNull.Value) _QualificationMDL.QDFS = Convert.ToString(reader["QDFS"]);
                    if (reader["GLH"] != DBNull.Value) _QualificationMDL.GLH = Convert.ToString(reader["GLH"]);
                    if (reader["ZYLB"] != DBNull.Value) _QualificationMDL.ZYLB = Convert.ToString(reader["ZYLB"]);

                    if (reader["BYXX"] != DBNull.Value) _QualificationMDL.BYXX = Convert.ToString(reader["BYXX"]);
                    if (reader["BYSJ"] != DBNull.Value) _QualificationMDL.BYSJ = Convert.ToDateTime(reader["BYSJ"]);
                    if (reader["SXZY"] != DBNull.Value) _QualificationMDL.SXZY = Convert.ToString(reader["SXZY"]);
                    if (reader["ZGXL"] != DBNull.Value) _QualificationMDL.ZGXL = Convert.ToString(reader["ZGXL"]);
                    if (reader["QFSJ"] != DBNull.Value) _QualificationMDL.QFSJ = Convert.ToDateTime(reader["QFSJ"]);
                }
                reader.Close();
                db.Close();
                return _QualificationMDL;
            }
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="QualificationID">主键</param>
        public static QualificationMDL GetObjectZJHM(string ZJHM)
        {
            string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ]
			FROM dbo.Qualification
			WHERE ZGZSBH = @ZJHM";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZJHM", DbType.String, ZJHM));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                QualificationMDL _QualificationMDL = null;
                if (reader.Read())
                {
                    _QualificationMDL = new QualificationMDL();
                    if (reader["ZGZSBH"] != DBNull.Value) _QualificationMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["XM"] != DBNull.Value) _QualificationMDL.XM = Convert.ToString(reader["XM"]);
                    if (reader["ZJHM"] != DBNull.Value) _QualificationMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
                    if (reader["SF"] != DBNull.Value) _QualificationMDL.SF = Convert.ToString(reader["SF"]);
                    if (reader["GZDW"] != DBNull.Value) _QualificationMDL.GZDW = Convert.ToString(reader["GZDW"]);
                    if (reader["QDNF"] != DBNull.Value) _QualificationMDL.QDNF = Convert.ToString(reader["QDNF"]);
                    if (reader["QDFS"] != DBNull.Value) _QualificationMDL.QDFS = Convert.ToString(reader["QDFS"]);
                    if (reader["GLH"] != DBNull.Value) _QualificationMDL.GLH = Convert.ToString(reader["GLH"]);
                    if (reader["ZYLB"] != DBNull.Value) _QualificationMDL.ZYLB = Convert.ToString(reader["ZYLB"]);

                    if (reader["BYXX"] != DBNull.Value) _QualificationMDL.BYXX = Convert.ToString(reader["BYXX"]);
                    if (reader["BYSJ"] != DBNull.Value) _QualificationMDL.BYSJ = Convert.ToDateTime(reader["BYSJ"]);
                    if (reader["SXZY"] != DBNull.Value) _QualificationMDL.SXZY = Convert.ToString(reader["SXZY"]);
                    if (reader["ZGXL"] != DBNull.Value) _QualificationMDL.ZGXL = Convert.ToString(reader["ZGXL"]);
                    if (reader["QFSJ"] != DBNull.Value) _QualificationMDL.QFSJ = Convert.ToDateTime(reader["QFSJ"]);
                }
                reader.Close();
                db.Close();
                return _QualificationMDL;
            }
        }
        /// <summary>
        /// 根据身份证号码获取考试资格证书
        /// </summary>
        /// <param name="QualificationID">身份证号码</param>
        public static List<QualificationMDL> GetObjectList(string ZJHM)
        {
            string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ]
			FROM dbo.Qualification
			WHERE ZJHM = @ZJHM";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZJHM", DbType.String, ZJHM));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<QualificationMDL> _ListQualificationMDL = new List<QualificationMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QualificationMDL _QualificationMDL = new QualificationMDL();
                        if (dt.Rows[i]["ZGZSBH"] != DBNull.Value) _QualificationMDL.ZGZSBH = Convert.ToString(dt.Rows[i]["ZGZSBH"]);
                        if (dt.Rows[i]["XM"] != DBNull.Value) _QualificationMDL.XM = Convert.ToString(dt.Rows[i]["XM"]);
                        if (dt.Rows[i]["ZJHM"] != DBNull.Value) _QualificationMDL.ZJHM = Convert.ToString(dt.Rows[i]["ZJHM"]);
                        if (dt.Rows[i]["SF"] != DBNull.Value) _QualificationMDL.SF = Convert.ToString(dt.Rows[i]["SF"]);
                        if (dt.Rows[i]["GZDW"] != DBNull.Value) _QualificationMDL.GZDW = Convert.ToString(dt.Rows[i]["GZDW"]);
                        if (dt.Rows[i]["QDNF"] != DBNull.Value) _QualificationMDL.QDNF = Convert.ToString(dt.Rows[i]["QDNF"]);
                        if (dt.Rows[i]["QDFS"] != DBNull.Value) _QualificationMDL.QDFS = Convert.ToString(dt.Rows[i]["QDFS"]);
                        if (dt.Rows[i]["GLH"] != DBNull.Value) _QualificationMDL.GLH = Convert.ToString(dt.Rows[i]["GLH"]);
                        if (dt.Rows[i]["ZYLB"] != DBNull.Value) _QualificationMDL.ZYLB = Convert.ToString(dt.Rows[i]["ZYLB"]);

                        if (dt.Rows[i]["BYXX"] != DBNull.Value) _QualificationMDL.BYXX = Convert.ToString(dt.Rows[i]["BYXX"]);
                        if (dt.Rows[i]["BYSJ"] != DBNull.Value) _QualificationMDL.BYSJ = Convert.ToDateTime(dt.Rows[i]["BYSJ"]);
                        if (dt.Rows[i]["SXZY"] != DBNull.Value) _QualificationMDL.SXZY = Convert.ToString(dt.Rows[i]["SXZY"]);
                        if (dt.Rows[i]["ZGXL"] != DBNull.Value) _QualificationMDL.ZGXL = Convert.ToString(dt.Rows[i]["ZGXL"]);
                        if (dt.Rows[i]["QFSJ"] != DBNull.Value) _QualificationMDL.QFSJ = Convert.ToDateTime(dt.Rows[i]["QFSJ"]);

                        _ListQualificationMDL.Add(_QualificationMDL);
                    }
                }
                db.Close();
                return _ListQualificationMDL;
            }
        }

        /// <summary>
        /// 根据身份证号码获取注册证书注销过，考试资格证书（用于重新注册）
        /// </summary>
        /// <param name="QualificationID">身份证号码</param>
        public static List<QualificationMDL> GetObjectListWithCancel(string ZJHM)
        {
            string sql = @"select *  FROM [dbo].[Qualification]
                            where zjhm = @ZJHM and  Exists(select 1 from [dbo].[COC_TOW_Person_BaseInfo] where PSN_CertificateNO=@ZJHM and PSN_RegisteType>'06')";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZJHM", DbType.String, ZJHM));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<QualificationMDL> _ListQualificationMDL = new List<QualificationMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QualificationMDL _QualificationMDL = new QualificationMDL();
                        if (dt.Rows[i]["ZGZSBH"] != DBNull.Value) _QualificationMDL.ZGZSBH = Convert.ToString(dt.Rows[i]["ZGZSBH"]);
                        if (dt.Rows[i]["XM"] != DBNull.Value) _QualificationMDL.XM = Convert.ToString(dt.Rows[i]["XM"]);
                        if (dt.Rows[i]["ZJHM"] != DBNull.Value) _QualificationMDL.ZJHM = Convert.ToString(dt.Rows[i]["ZJHM"]);
                        if (dt.Rows[i]["SF"] != DBNull.Value) _QualificationMDL.SF = Convert.ToString(dt.Rows[i]["SF"]);
                        if (dt.Rows[i]["GZDW"] != DBNull.Value) _QualificationMDL.GZDW = Convert.ToString(dt.Rows[i]["GZDW"]);
                        if (dt.Rows[i]["QDNF"] != DBNull.Value) _QualificationMDL.QDNF = Convert.ToString(dt.Rows[i]["QDNF"]);
                        if (dt.Rows[i]["QDFS"] != DBNull.Value) _QualificationMDL.QDFS = Convert.ToString(dt.Rows[i]["QDFS"]);
                        if (dt.Rows[i]["GLH"] != DBNull.Value) _QualificationMDL.GLH = Convert.ToString(dt.Rows[i]["GLH"]);
                        if (dt.Rows[i]["ZYLB"] != DBNull.Value) _QualificationMDL.ZYLB = Convert.ToString(dt.Rows[i]["ZYLB"]);

                        if (dt.Rows[i]["BYXX"] != DBNull.Value) _QualificationMDL.BYXX = Convert.ToString(dt.Rows[i]["BYXX"]);
                        if (dt.Rows[i]["BYSJ"] != DBNull.Value) _QualificationMDL.BYSJ = Convert.ToDateTime(dt.Rows[i]["BYSJ"]);
                        if (dt.Rows[i]["SXZY"] != DBNull.Value) _QualificationMDL.SXZY = Convert.ToString(dt.Rows[i]["SXZY"]);
                        if (dt.Rows[i]["ZGXL"] != DBNull.Value) _QualificationMDL.ZGXL = Convert.ToString(dt.Rows[i]["ZGXL"]);
                        if (dt.Rows[i]["QFSJ"] != DBNull.Value) _QualificationMDL.QFSJ = Convert.ToDateTime(dt.Rows[i]["QFSJ"]);
                        _ListQualificationMDL.Add(_QualificationMDL);
                    }
                }
                db.Close();
                return _ListQualificationMDL;
            }
        }

        /// <summary>
        /// 根据身份证号码获取实体集合,并且排除自己的已注册的专业
        /// </summary>
        /// <param name="ZJHM">证件号码</param>
        /// <param name="ZCZY">已注册过专业</param>
        /// <returns></returns>
        public static List<QualificationMDL> GetAddItemObjectList(string ZJHM, string ZCZY)
        {
            string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ]
			FROM dbo.Qualification
			WHERE ZJHM = @ZJHM  AND ZYLB NOT IN (@ZCZY) ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZJHM", DbType.String, ZJHM));
            p.Add(db.CreateParameter("ZCZY", DbType.String, ZCZY));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<QualificationMDL> _ListQualificationMDL = new List<QualificationMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QualificationMDL _QualificationMDL = new QualificationMDL();
                        if (dt.Rows[i]["ZGZSBH"] != DBNull.Value) _QualificationMDL.ZGZSBH = Convert.ToString(dt.Rows[i]["ZGZSBH"]);
                        if (dt.Rows[i]["XM"] != DBNull.Value) _QualificationMDL.XM = Convert.ToString(dt.Rows[i]["XM"]);
                        if (dt.Rows[i]["ZJHM"] != DBNull.Value) _QualificationMDL.ZJHM = Convert.ToString(dt.Rows[i]["ZJHM"]);
                        if (dt.Rows[i]["SF"] != DBNull.Value) _QualificationMDL.SF = Convert.ToString(dt.Rows[i]["SF"]);
                        if (dt.Rows[i]["GZDW"] != DBNull.Value) _QualificationMDL.GZDW = Convert.ToString(dt.Rows[i]["GZDW"]);
                        if (dt.Rows[i]["QDNF"] != DBNull.Value) _QualificationMDL.QDNF = Convert.ToString(dt.Rows[i]["QDNF"]);
                        if (dt.Rows[i]["QDFS"] != DBNull.Value) _QualificationMDL.QDFS = Convert.ToString(dt.Rows[i]["QDFS"]);
                        if (dt.Rows[i]["GLH"] != DBNull.Value) _QualificationMDL.GLH = Convert.ToString(dt.Rows[i]["GLH"]);
                        if (dt.Rows[i]["ZYLB"] != DBNull.Value) _QualificationMDL.ZYLB = Convert.ToString(dt.Rows[i]["ZYLB"]);

                        if (dt.Rows[i]["BYXX"] != DBNull.Value) _QualificationMDL.BYXX = Convert.ToString(dt.Rows[i]["BYXX"]);
                        if (dt.Rows[i]["BYSJ"] != DBNull.Value) _QualificationMDL.BYSJ = Convert.ToDateTime(dt.Rows[i]["BYSJ"]);
                        if (dt.Rows[i]["SXZY"] != DBNull.Value) _QualificationMDL.SXZY = Convert.ToString(dt.Rows[i]["SXZY"]);
                        if (dt.Rows[i]["ZGXL"] != DBNull.Value) _QualificationMDL.ZGXL = Convert.ToString(dt.Rows[i]["ZGXL"]);
                        if (dt.Rows[i]["QFSJ"] != DBNull.Value) _QualificationMDL.QFSJ = Convert.ToDateTime(dt.Rows[i]["QFSJ"]);
                        _ListQualificationMDL.Add(_QualificationMDL);
                    }
                }
                db.Close();
                return _ListQualificationMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Qualification", "*", filterWhereString, orderBy == "" ? " ZGZSBH" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Qualification", filterWhereString);
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
        public static DataTable GetList2(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Unit", "*", filterWhereString, orderBy == "" ? "ENT_City" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount2(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Unit", filterWhereString);
        }


        #region 自定义方法

        /// <summary>
        /// 删除正式表中与临时表交集数据（即已存在的数据）
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        protected static int DeleteExist(DbTransaction tran)
        {
            string sql = @"DELETE  FROM dbo.Qualification from Qualification inner join [Qualification_ing] on Qualification.ZGZSBH =[Qualification_ing].ZGZSBH";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran, sql);
        }

        /// <summary>
        /// 临时表写入正式表
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        protected static int InsertByTemp(DbTransaction tran)
        {
            string sql = @"INSERT INTO [dbo].[Qualification]([ZGZSBH],[XM],[ZJHM],[SF],[GZDW],[QDNF],[QDFS],[GLH],[ZYLB],[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ],[LastModifyTime])
                            SELECT [ZGZSBH],[XM],[ZJHM],[SF],[GZDW],[QDNF],[QDFS],[GLH],[ZYLB],[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ],getdate()FROM [dbo].[Qualification_ing]";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran, sql);
        }


        /// <summary>
        /// 清空导入临时表
        /// </summary>
        /// <returns></returns>
        protected static int ClearImportTempTable()
        {
            string sql = @"truncate table [Qualification_ing]";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(sql);
        }

        public static void ImportByExcel(DataTable daInsert)
        {
            //清楚临时表
            ClearImportTempTable();

            //数据导入临时表
            string headList = "资格证书编号,姓名,证件号码,省份,工作单位,取得年份,取得方式,管理号,专业类别,毕业学校,毕业时间,所学专业,最高学历,签发时间";
            string columnList = "ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,BYXX,BYSJ,SXZY,ZGXL,QFSJ";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString);
            conn.Open();
            try
            {
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
                {
                    sqlBC.BatchSize = 1000;
                    sqlBC.BulkCopyTimeout = 60;
                    sqlBC.NotifyAfter = 10000;
                    sqlBC.DestinationTableName = "Qualification_ing";

                    for (int i = daInsert.Columns.Count - 1; i >= 0; i--)
                    {
                        if (headList.Contains(daInsert.Columns[i].ColumnName) == false)
                        {
                            daInsert.Columns.RemoveAt(i);
                        }
                    }
                    string[] headSet = headList.Split(',');
                    string[] columnSet = columnList.Split(',');
                    for (int i = 0; i < columnSet.Length; i++)
                    {
                        sqlBC.ColumnMappings.Add(headSet[i], columnSet[i]);
                    }
                    sqlBC.WriteToServer(daInsert);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
            }

            //更新正式表
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                DeleteExist(tran);
                InsertByTemp(tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        #endregion
    }
}
