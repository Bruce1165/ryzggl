using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_QualificationDAL(填写类描述)
	/// </summary>
    public class zjs_QualificationDAL
    {
        public zjs_QualificationDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(zjs_QualificationMDL _zjs_QualificationMDL)
		{
		    return Insert(null,_zjs_QualificationMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,zjs_QualificationMDL _zjs_QualificationMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.zjs_Qualification(ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,BYXX,BYSJ,SXZY,ZGXL,QFSJ,LastModifyTime)
			VALUES (@ZGZSBH,@XM,@ZJHM,@SF,@GZDW,@QDNF,@QDFS,@GLH,@ZYLB,@BYXX,@BYSJ,@SXZY,@ZGXL,@QFSJ,@LastModifyTime)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_QualificationMDL.ZGZSBH));
			p.Add(db.CreateParameter("XM",DbType.String, _zjs_QualificationMDL.XM));
			p.Add(db.CreateParameter("ZJHM",DbType.String, _zjs_QualificationMDL.ZJHM));
			p.Add(db.CreateParameter("SF",DbType.String, _zjs_QualificationMDL.SF));
			p.Add(db.CreateParameter("GZDW",DbType.String, _zjs_QualificationMDL.GZDW));
			p.Add(db.CreateParameter("QDNF",DbType.String, _zjs_QualificationMDL.QDNF));
			p.Add(db.CreateParameter("QDFS",DbType.String, _zjs_QualificationMDL.QDFS));
			p.Add(db.CreateParameter("GLH",DbType.String, _zjs_QualificationMDL.GLH));
			p.Add(db.CreateParameter("ZYLB",DbType.String, _zjs_QualificationMDL.ZYLB));
			p.Add(db.CreateParameter("BYXX",DbType.String, _zjs_QualificationMDL.BYXX));
			p.Add(db.CreateParameter("BYSJ",DbType.DateTime, _zjs_QualificationMDL.BYSJ));
			p.Add(db.CreateParameter("SXZY",DbType.String, _zjs_QualificationMDL.SXZY));
			p.Add(db.CreateParameter("ZGXL",DbType.String, _zjs_QualificationMDL.ZGXL));
			p.Add(db.CreateParameter("QFSJ",DbType.DateTime, _zjs_QualificationMDL.QFSJ));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _zjs_QualificationMDL.LastModifyTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(zjs_QualificationMDL _zjs_QualificationMDL)
		{
			return Update(null,_zjs_QualificationMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,zjs_QualificationMDL _zjs_QualificationMDL)
		{
			string sql = @"
			UPDATE dbo.zjs_Qualification
				SET	ZGZSBH = @ZGZSBH,XM = @XM,ZJHM = @ZJHM,SF = @SF,GZDW = @GZDW,QDNF = @QDNF,QDFS = @QDFS,GLH = @GLH,ZYLB = @ZYLB,BYXX = @BYXX,BYSJ = @BYSJ,SXZY = @SXZY,ZGXL = @ZGXL,QFSJ = @QFSJ,LastModifyTime = @LastModifyTime
			WHERE
				ZGZSBH = @Old_ZGZSBH";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Old_ZGZSBH", DbType.String, _zjs_QualificationMDL.Old_ZGZSBH));
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_QualificationMDL.ZGZSBH));
			p.Add(db.CreateParameter("XM",DbType.String, _zjs_QualificationMDL.XM));
			p.Add(db.CreateParameter("ZJHM",DbType.String, _zjs_QualificationMDL.ZJHM));
			p.Add(db.CreateParameter("SF",DbType.String, _zjs_QualificationMDL.SF));
			p.Add(db.CreateParameter("GZDW",DbType.String, _zjs_QualificationMDL.GZDW));
			p.Add(db.CreateParameter("QDNF",DbType.String, _zjs_QualificationMDL.QDNF));
			p.Add(db.CreateParameter("QDFS",DbType.String, _zjs_QualificationMDL.QDFS));
			p.Add(db.CreateParameter("GLH",DbType.String, _zjs_QualificationMDL.GLH));
			p.Add(db.CreateParameter("ZYLB",DbType.String, _zjs_QualificationMDL.ZYLB));
			p.Add(db.CreateParameter("BYXX",DbType.String, _zjs_QualificationMDL.BYXX));
			p.Add(db.CreateParameter("BYSJ",DbType.DateTime, _zjs_QualificationMDL.BYSJ));
			p.Add(db.CreateParameter("SXZY",DbType.String, _zjs_QualificationMDL.SXZY));
			p.Add(db.CreateParameter("ZGXL",DbType.String, _zjs_QualificationMDL.ZGXL));
			p.Add(db.CreateParameter("QFSJ",DbType.DateTime, _zjs_QualificationMDL.QFSJ));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _zjs_QualificationMDL.LastModifyTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ZGZSBH">主键</param>
		/// <returns></returns>
        public static int Delete( string ZGZSBH )
		{
			return Delete(null, ZGZSBH);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ZGZSBH">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ZGZSBH)
		{
			string sql=@"DELETE FROM dbo.zjs_Qualification WHERE ZGZSBH = @ZGZSBH";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ZGZSBH",DbType.String,ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(zjs_QualificationMDL _zjs_QualificationMDL)
		{
			return Delete(null,_zjs_QualificationMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_QualificationMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,zjs_QualificationMDL _zjs_QualificationMDL)
		{
			string sql=@"DELETE FROM dbo.zjs_Qualification WHERE ZGZSBH = @ZGZSBH";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ZGZSBH",DbType.String,_zjs_QualificationMDL.ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ZGZSBH">主键</param>
        public static zjs_QualificationMDL GetObject( string ZGZSBH )
		{
			string sql=@"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,BYXX,BYSJ,SXZY,ZGXL,QFSJ,LastModifyTime
			FROM dbo.zjs_Qualification
			WHERE ZGZSBH = @ZGZSBH";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, ZGZSBH));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_QualificationMDL _zjs_QualificationMDL = null;
                if (reader.Read())
                {
                    _zjs_QualificationMDL = new zjs_QualificationMDL();
					if (reader["ZGZSBH"] != DBNull.Value) _zjs_QualificationMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
					if (reader["XM"] != DBNull.Value) _zjs_QualificationMDL.XM = Convert.ToString(reader["XM"]);
					if (reader["ZJHM"] != DBNull.Value) _zjs_QualificationMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
					if (reader["SF"] != DBNull.Value) _zjs_QualificationMDL.SF = Convert.ToString(reader["SF"]);
					if (reader["GZDW"] != DBNull.Value) _zjs_QualificationMDL.GZDW = Convert.ToString(reader["GZDW"]);
					if (reader["QDNF"] != DBNull.Value) _zjs_QualificationMDL.QDNF = Convert.ToString(reader["QDNF"]);
					if (reader["QDFS"] != DBNull.Value) _zjs_QualificationMDL.QDFS = Convert.ToString(reader["QDFS"]);
					if (reader["GLH"] != DBNull.Value) _zjs_QualificationMDL.GLH = Convert.ToString(reader["GLH"]);
					if (reader["ZYLB"] != DBNull.Value) _zjs_QualificationMDL.ZYLB = Convert.ToString(reader["ZYLB"]);
					if (reader["BYXX"] != DBNull.Value) _zjs_QualificationMDL.BYXX = Convert.ToString(reader["BYXX"]);
					if (reader["BYSJ"] != DBNull.Value) _zjs_QualificationMDL.BYSJ = Convert.ToDateTime(reader["BYSJ"]);
					if (reader["SXZY"] != DBNull.Value) _zjs_QualificationMDL.SXZY = Convert.ToString(reader["SXZY"]);
					if (reader["ZGXL"] != DBNull.Value) _zjs_QualificationMDL.ZGXL = Convert.ToString(reader["ZGXL"]);
					if (reader["QFSJ"] != DBNull.Value) _zjs_QualificationMDL.QFSJ = Convert.ToDateTime(reader["QFSJ"]);
					if (reader["LastModifyTime"] != DBNull.Value) _zjs_QualificationMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
                }
				reader.Close();
                db.Close();
                return _zjs_QualificationMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_Qualification", "*", filterWhereString, orderBy == "" ? " ZGZSBH" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_Qualification", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 根据身份证号码获取考试资格证书
        /// </summary>
        /// <param name="ZJHM">身份证号码</param>
        public static List<zjs_QualificationMDL> GetListByZJHM(string ZJHM)
        {
            string sql = @"
			SELECT ZGZSBH,XM,ZJHM,SF,GZDW,QDNF,QDFS,GLH,ZYLB,[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ],[LastModifyTime]
			FROM dbo.zjs_Qualification
			WHERE ZJHM = @ZJHM";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZJHM", DbType.String, ZJHM));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                List<zjs_QualificationMDL> _ListQualificationMDL = new List<zjs_QualificationMDL>();
                zjs_QualificationMDL _zjs_QualificationMDL = null;
                while (reader.Read())
                {
                    _zjs_QualificationMDL = new zjs_QualificationMDL();
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_QualificationMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["XM"] != DBNull.Value) _zjs_QualificationMDL.XM = Convert.ToString(reader["XM"]);
                    if (reader["ZJHM"] != DBNull.Value) _zjs_QualificationMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
                    if (reader["SF"] != DBNull.Value) _zjs_QualificationMDL.SF = Convert.ToString(reader["SF"]);
                    if (reader["GZDW"] != DBNull.Value) _zjs_QualificationMDL.GZDW = Convert.ToString(reader["GZDW"]);
                    if (reader["QDNF"] != DBNull.Value) _zjs_QualificationMDL.QDNF = Convert.ToString(reader["QDNF"]);
                    if (reader["QDFS"] != DBNull.Value) _zjs_QualificationMDL.QDFS = Convert.ToString(reader["QDFS"]);
                    if (reader["GLH"] != DBNull.Value) _zjs_QualificationMDL.GLH = Convert.ToString(reader["GLH"]);
                    if (reader["ZYLB"] != DBNull.Value) _zjs_QualificationMDL.ZYLB = Convert.ToString(reader["ZYLB"]);
                    if (reader["BYXX"] != DBNull.Value) _zjs_QualificationMDL.BYXX = Convert.ToString(reader["BYXX"]);
                    if (reader["BYSJ"] != DBNull.Value) _zjs_QualificationMDL.BYSJ = Convert.ToDateTime(reader["BYSJ"]);
                    if (reader["SXZY"] != DBNull.Value) _zjs_QualificationMDL.SXZY = Convert.ToString(reader["SXZY"]);
                    if (reader["ZGXL"] != DBNull.Value) _zjs_QualificationMDL.ZGXL = Convert.ToString(reader["ZGXL"]);
                    if (reader["QFSJ"] != DBNull.Value) _zjs_QualificationMDL.QFSJ = Convert.ToDateTime(reader["QFSJ"]);
                    if (reader["LastModifyTime"] != DBNull.Value) _zjs_QualificationMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
                    _ListQualificationMDL.Add(_zjs_QualificationMDL);
                }
                reader.Close();
                db.Close();
                return _ListQualificationMDL;
            }
        }

        /// <summary>
        /// 删除正式表中与临时表交集数据（即已存在的数据）
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        protected static int DeleteExist(DbTransaction tran)
        {
            string sql = @"DELETE  FROM dbo.zjs_Qualification from zjs_Qualification inner join [zjs_Qualification_ing] on zjs_Qualification.ZGZSBH =[zjs_Qualification_ing].ZGZSBH";
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
            string sql = @"INSERT INTO [dbo].[zjs_Qualification]([ZGZSBH],[XM],[ZJHM],[SF],[GZDW],[QDNF],[QDFS],[GLH],[ZYLB],[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ],[LastModifyTime])
                            SELECT [ZGZSBH],[XM],[ZJHM],[SF],[GZDW],[QDNF],[QDFS],[GLH],[ZYLB],[BYXX],[BYSJ],[SXZY],[ZGXL],[QFSJ],getdate()FROM [dbo].[zjs_Qualification_ing]";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran, sql);
        }
        

        /// <summary>
        /// 清空导入临时表
        /// </summary>
        /// <returns></returns>
        protected static int ClearImportTempTable()
        {
            string sql = @"truncate table [zjs_Qualification_ing]";
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
                    sqlBC.DestinationTableName = "zjs_Qualification_ing";

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
