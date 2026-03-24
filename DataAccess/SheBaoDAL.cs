using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--SheBaoDAL(社保查询结果)
	/// </summary>
    public class SheBaoDAL
    {
        public SheBaoDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(SheBaoMDL _SheBaoMDL)
		{
		    return Insert(null,_SheBaoMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,SheBaoMDL _SheBaoMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
            if not exists(select 1 from dbo.SheBao where CertificateCode = @CertificateCode AND JFYF = @JFYF AND XZCode = @XZCode)
			INSERT INTO dbo.SheBao(CertificateCode,JFYF,XZCode,WorkerName,CreditCode,ENT_Name,XZName,CJSJ)
			VALUES (@CertificateCode,@JFYF,@XZCode,@WorkerName,@CreditCode,@ENT_Name,@XZName,@CJSJ)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _SheBaoMDL.CertificateCode));
			p.Add(db.CreateParameter("JFYF",DbType.Int32, _SheBaoMDL.JFYF));
			p.Add(db.CreateParameter("XZCode",DbType.String, _SheBaoMDL.XZCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _SheBaoMDL.WorkerName));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _SheBaoMDL.CreditCode));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _SheBaoMDL.ENT_Name));
			p.Add(db.CreateParameter("XZName",DbType.String, _SheBaoMDL.XZName));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _SheBaoMDL.CJSJ));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(SheBaoMDL _SheBaoMDL)
		{
			return Update(null,_SheBaoMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,SheBaoMDL _SheBaoMDL)
		{
			string sql = @"
			UPDATE dbo.SheBao
				SET	WorkerName = @WorkerName,CreditCode = @CreditCode,ENT_Name = @ENT_Name,XZName = @XZName,CJSJ = @CJSJ
			WHERE
				CertificateCode = @CertificateCode AND JFYF = @JFYF AND XZCode = @XZCode";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _SheBaoMDL.CertificateCode));
			p.Add(db.CreateParameter("JFYF",DbType.Int32, _SheBaoMDL.JFYF));
			p.Add(db.CreateParameter("XZCode",DbType.String, _SheBaoMDL.XZCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _SheBaoMDL.WorkerName));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _SheBaoMDL.CreditCode));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _SheBaoMDL.ENT_Name));
			p.Add(db.CreateParameter("XZName",DbType.String, _SheBaoMDL.XZName));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _SheBaoMDL.CJSJ));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="SheBaoID">主键</param>
		/// <returns></returns>
        public static int Delete( string CertificateCode, int JFYF, string XZCode )
		{
			return Delete(null, CertificateCode, JFYF, XZCode);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="SheBaoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string CertificateCode, int JFYF, string XZCode)
		{
			string sql=@"DELETE FROM dbo.SheBao WHERE CertificateCode = @CertificateCode AND JFYF = @JFYF AND XZCode = @XZCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,CertificateCode));
			p.Add(db.CreateParameter("JFYF",DbType.Int32,JFYF));
			p.Add(db.CreateParameter("XZCode",DbType.String,XZCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(SheBaoMDL _SheBaoMDL)
		{
			return Delete(null,_SheBaoMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SheBaoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,SheBaoMDL _SheBaoMDL)
		{
			string sql=@"DELETE FROM dbo.SheBao WHERE CertificateCode = @CertificateCode AND JFYF = @JFYF AND XZCode = @XZCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,_SheBaoMDL.CertificateCode));
			p.Add(db.CreateParameter("JFYF",DbType.Int32,_SheBaoMDL.JFYF));
			p.Add(db.CreateParameter("XZCode",DbType.String,_SheBaoMDL.XZCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="SheBaoID">主键</param>
        public static SheBaoMDL GetObject( string CertificateCode, int JFYF, string XZCode )
		{
			string sql=@"
			SELECT CertificateCode,JFYF,XZCode,WorkerName,CreditCode,ENT_Name,XZName,CJSJ
			FROM dbo.SheBao
			WHERE CertificateCode = @CertificateCode AND JFYF = @JFYF AND XZCode = @XZCode";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,CertificateCode));
			p.Add(db.CreateParameter("JFYF",DbType.Int32,JFYF));
			p.Add(db.CreateParameter("XZCode",DbType.String,XZCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                SheBaoMDL _SheBaoMDL = null;
                if (reader.Read())
                {
                    _SheBaoMDL = new SheBaoMDL();
					if (reader["CertificateCode"] != DBNull.Value) _SheBaoMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["JFYF"] != DBNull.Value) _SheBaoMDL.JFYF = Convert.ToInt32(reader["JFYF"]);
					if (reader["XZCode"] != DBNull.Value) _SheBaoMDL.XZCode = Convert.ToString(reader["XZCode"]);
					if (reader["WorkerName"] != DBNull.Value) _SheBaoMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["CreditCode"] != DBNull.Value) _SheBaoMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
					if (reader["ENT_Name"] != DBNull.Value) _SheBaoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
					if (reader["XZName"] != DBNull.Value) _SheBaoMDL.XZName = Convert.ToString(reader["XZName"]);
					if (reader["CJSJ"] != DBNull.Value) _SheBaoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                }
				reader.Close();
                db.Close();
                return _SheBaoMDL;
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
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.SheBao", "*", filterWhereString, orderBy == "" ? " CertificateCode, JFYF, XZCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.SheBao", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 查询个人社保记录
        /// </summary>
        /// <param name="WorkerName">姓名</param>
        /// <param name="idcard">身份证号</param>
        /// <param name="ApplyDate">业务申报时间</param>
        /// <returns></returns>
        public static bool SheBaoQuery(string WorkerName, string idcard, DateTime ApplyDate)
        {
            try
            {
                string rtn = Utility.HttpHelp.DoPostSheBaoQuery(
                    string.Format(@"type=GRQY010&time={0}&data={{""PAC014"":""{1}"",""ks"":""{2}"",""js"":""{3}""}}"
                    , DateTime.Now.ToString("yyyyMMddHHmmss")
                    , idcard
                    , ApplyDate.AddMonths(-2).ToString("yyyy-MM-01")
                    , ApplyDate.ToString("yyyy-MM-dd")));

                ResponseResultSheBao rsp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResultSheBao>(rtn);
                if (rsp.code == 200)
                {
                    if (rsp.data == null)
                    {
                        Utility.FileLog.WriteLog(string.Format("社保系统无“{0}”数据。", WorkerName));
                        return true;
                    }
                    else
                    {
                        SheBaoMDL _SheBaoMDL = null;
                        foreach (SBJFYJL d in rsp.data.CZZGSBJFYJL)
                        {
                            _SheBaoMDL=SheBaoDAL.GetObject(idcard, Convert.ToInt32(Convert.ToDateTime(d.PAE001).ToString("yyyyMM")), d.BZE016);
                            if (_SheBaoMDL == null)
                            {
                                _SheBaoMDL = new SheBaoMDL();

                                _SheBaoMDL.CertificateCode = idcard;
                                _SheBaoMDL.JFYF = Convert.ToInt32(Convert.ToDateTime(d.PAE001).ToString("yyyyMM"));
                                _SheBaoMDL.XZCode = d.BZE016;

                                _SheBaoMDL.WorkerName = WorkerName;
                                _SheBaoMDL.ENT_Name = d.AAB004;
                                _SheBaoMDL.CreditCode = d.AAB003;
                                _SheBaoMDL.XZName = d.BZE016D;
                                _SheBaoMDL.CJSJ = DateTime.Now;
                                SheBaoDAL.Insert(_SheBaoMDL);
                            }
                            else
                            {
                                _SheBaoMDL.WorkerName = WorkerName;
                                _SheBaoMDL.ENT_Name = d.AAB004;
                                _SheBaoMDL.CreditCode = d.AAB003;
                                _SheBaoMDL.XZName = d.BZE016D;
                                _SheBaoMDL.CJSJ = DateTime.Now;
                                SheBaoDAL.Update(_SheBaoMDL);
                            }
                            
                        }
                        //Utility.FileLog.WriteLog(string.Format("查询{0}{1}~{2}社保记录！", WorkerName, ApplyDate.AddMonths(-2).ToString("yyyy-MM"), ApplyDate.AddMonths(-1).ToString("yyyy-MM")));
                        return true;
                    }
                }
                else
                {
                    Utility.FileLog.WriteLog(string.Format("查询{0}{1}~{2}社保记录失败！{3}", WorkerName, ApplyDate.AddMonths(-2).ToString("yyyy-MM"), ApplyDate.AddMonths(-1).ToString("yyyy-MM"), rsp.message));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog(string.Format("查询{0}{1}~{2}社保记录失败！{3}", WorkerName, ApplyDate.AddMonths(-2).ToString("yyyy-MM"), ApplyDate.AddMonths(-1).ToString("yyyy-MM"), ex.StackTrace));
                return false;
            }
        }

        #endregion
    }
}
