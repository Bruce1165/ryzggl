using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--UnitDAL(填写类描述)
	/// </summary>
    public class UnitDAL
    {
        public UnitDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="UnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(UnitMDL _UnitMDL)
		{
		    return Insert(null,_UnitMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,UnitMDL _UnitMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Unit(UnitID,BeginTime,EndTime,ENT_Name,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate,ENT_Correspondence,ENT_Postcode,ENT_Contact,ENT_Telephone,ENT_MobilePhone,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,CreditCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ResultGSXX)
			VALUES (@UnitID,@BeginTime,@EndTime,@ENT_Name,@ENT_OrganizationsCode,@ENT_Economic_Nature,@ENT_Province,@ENT_Province_Code,@ENT_City,@END_Addess,@ENT_Corporate,@ENT_Correspondence,@ENT_Postcode,@ENT_Contact,@ENT_Telephone,@ENT_MobilePhone,@ENT_Type,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@CreditCode,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo,@ResultGSXX)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitID",DbType.String, _UnitMDL.UnitID));
			p.Add(db.CreateParameter("BeginTime",DbType.DateTime, _UnitMDL.BeginTime));
			p.Add(db.CreateParameter("EndTime",DbType.DateTime, _UnitMDL.EndTime));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _UnitMDL.ENT_Name));
			p.Add(db.CreateParameter("ENT_OrganizationsCode",DbType.String, _UnitMDL.ENT_OrganizationsCode));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _UnitMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("ENT_Province",DbType.String, _UnitMDL.ENT_Province));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _UnitMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _UnitMDL.ENT_City));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _UnitMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Corporate",DbType.String, _UnitMDL.ENT_Corporate));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _UnitMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _UnitMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Contact",DbType.String, _UnitMDL.ENT_Contact));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _UnitMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_MobilePhone",DbType.String, _UnitMDL.ENT_MobilePhone));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _UnitMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _UnitMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _UnitMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _UnitMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _UnitMDL.CreditCode));
			p.Add(db.CreateParameter("CJR",DbType.String, _UnitMDL.CJR));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _UnitMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _UnitMDL.XGR));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _UnitMDL.XGSJ));
			p.Add(db.CreateParameter("Valid",DbType.Int32, _UnitMDL.Valid));
			p.Add(db.CreateParameter("Memo",DbType.String, _UnitMDL.Memo));
            p.Add(db.CreateParameter("ResultGSXX", DbType.Int32, _UnitMDL.ResultGSXX));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}


//        /// <summary>
//        /// 获取企业当日发送验证工商信息次数
//        /// </summary>
//        /// <param name="uni_scid">社会统一信息用代码</param>
//        /// <returns>单日发起验证次数</returns>
//        public static int GetSendCheckGSCount(string uni_scid)
//        {
//            string sql = @"
//			SELECT count(*) FROM [192.168.7.89].[GSJ].[dbo].[QY_GSDJSQXX]
//			WHERE reg_no = @uni_scid and cjsj between Convert(varchar(10),getdate(),21) and Convert(varchar(10),getdate(),21) +' 23:59:59'";

////            string sql = @"
////			SELECT count(*) FROM [QY_GSDJSQXX]
////			WHERE reg_no = @uni_scid and cjsj between Convert(varchar(10),getdate(),21) and Convert(varchar(10),getdate(),21) +' 23:59:59'";

//            DBHelper db = new DBHelper();
//            List<SqlParameter> p = new List<SqlParameter>();
//            p.Add(db.CreateParameter("uni_scid", DbType.String, uni_scid));

//            int rtn = 0;
//            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
//            {
      
//                if (reader.Read())
//                {
//                   rtn=  Convert.ToInt32(reader[0]);
//                }
//                reader.Close();
//                db.Close();
//                return rtn;
//            }
//        }

//        /// <summary>
//        /// 获取企业当日最后发送验证工商信息时间
//        /// </summary>
//        /// <param name="uni_scid">社会统一信息用代码</param>
//        /// <returns>最后发送验证工商信息时间</returns>
//        public static DateTime? GetLastSendCheckGSTime(string uni_scid)
//        {
//            string sql = @"
//			SELECT top 1 cjsj FROM [192.168.7.89].[GSJ].[dbo].[QY_GSDJSQXX]
//			WHERE reg_no = @uni_scid and cjsj between Convert(varchar(10),getdate(),21) and Convert(varchar(10),getdate(),21) +' 23:59:59' order by cjsj desc";
////            string sql = @"
////			SELECT top 1 cjsj FROM [QY_GSDJSQXX]
////			WHERE reg_no = @uni_scid and cjsj between Convert(varchar(10),getdate(),21) and Convert(varchar(10),getdate(),21) +' 23:59:59' order by cjsj desc";


//            DBHelper db = new DBHelper();
//            List<SqlParameter> p = new List<SqlParameter>();
//            p.Add(db.CreateParameter("uni_scid", DbType.String, uni_scid));

//            DateTime? rtn = null;
//            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
//            {

//                if (reader.Read())
//                {
//                    rtn = Convert.ToDateTime(reader[0]);
//                }
//                reader.Close();
//                db.Close();
//                return rtn;
//            }
//        }

//        /// <summary>
//        /// 申请工商验证验证
//        /// </summary>
//        /// <param name="_UnitMDL"></param>
//        /// <returns></returns>
//        public static int InsertSubmit(UnitMDL _UnitMDL)
//        {
//            return InsertSubmit(null,_UnitMDL);
//        }
//        /// <summary>
//        /// 申请验证工商信息
//        /// </summary>
//        /// <param name="tran"></param>
//        /// <param name="_UnitMDL"></param>
//        /// <returns></returns>
//        public static int InsertSubmit(DbTransaction tran, UnitMDL _UnitMDL)
//        {
//            DBHelper db = new DBHelper();
//            string sql = @"insert into [192.168.7.89].[GSJ].[dbo].[QY_GSDJSQXX](ent_name,reg_no,valid,cjr,cjsj) 
//                                    values(@ent_name,@reg_no,@valid,@cjr,@cjsj)";
////            string sql = @"insert into [QY_GSDJSQXX](ent_name,reg_no,valid,cjr,cjsj) 
////                                    values(@ent_name,@reg_no,@valid,@cjr,@cjsj)";

//            List<SqlParameter> p = new List<SqlParameter>();
//            p.Add(db.CreateParameter("ent_name", DbType.String, _UnitMDL.ENT_Name));
//            p.Add(db.CreateParameter("reg_no", DbType.String, _UnitMDL.CreditCode));
//            p.Add(db.CreateParameter("valid", DbType.String,1));
//            p.Add(db.CreateParameter("cjr", DbType.String, "北京市住房和城乡建设领域人员资格管理信息系统"));
//            p.Add(db.CreateParameter("cjsj", DbType.DateTime, DateTime.Now));
//            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
//        }
        /// <summary>
        /// 验证企业工商信息（不含外地进京备案）
        /// </summary>
        /// <param name="uni_scid">18位社会统一信用代码或9位组织机构代码</param>
        /// <returns></returns>
        public static GSJ_QY_GSDJXXMDL GetObjectUni_scid(string uni_scid)
        {
            //ENT_STATE
            //1：开业
            //2：吊销
            //3：注销
            //4：市内迁出
            //5：迁往市外
            //6：撤销
            //7：歇业
            //8：个体转企业
            string sql = @"SELECT top 1 * FROM [dbo].[QY_GSDJXX]
            			WHERE uni_scid = @uni_scid and ENT_STATE=1";

            if(uni_scid.Length==9)
            {
                sql = @"SELECT top 1 * FROM [dbo].[QY_GSDJXX]
            			WHERE uni_scid like '________' +  @uni_scid + '_' and ENT_STATE=1";
            }


            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("uni_scid", DbType.String, uni_scid));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                GSJ_QY_GSDJXXMDL _QY_GSDJXXMDL = null;
                if (reader.Read())
                {
                    _QY_GSDJXXMDL = new GSJ_QY_GSDJXXMDL();
                    if (reader["ID"] != DBNull.Value) _QY_GSDJXXMDL.ID = new Guid(Convert.ToString(reader["ID"])).ToString();
                    if (reader["ENT_NAME"] != DBNull.Value) _QY_GSDJXXMDL.ENT_NAME = Convert.ToString(reader["ENT_NAME"]);
                    if (reader["REG_NO"] != DBNull.Value) _QY_GSDJXXMDL.REG_NO = Convert.ToString(reader["REG_NO"]);
                    if (reader["OP_LOC"] != DBNull.Value) _QY_GSDJXXMDL.OP_LOC = Convert.ToString(reader["OP_LOC"]);
                    if (reader["CORP_RPT"] != DBNull.Value) _QY_GSDJXXMDL.CORP_RPT = Convert.ToString(reader["CORP_RPT"]);
                    if (reader["REG_CAP"] != DBNull.Value) _QY_GSDJXXMDL.REG_CAP = Convert.ToDecimal(reader["REG_CAP"]);
                    if (reader["EST_DATE"] != DBNull.Value) _QY_GSDJXXMDL.EST_DATE = Convert.ToDateTime(reader["EST_DATE"]);
                    if (reader["ENT_TYPE"] != DBNull.Value) _QY_GSDJXXMDL.ENT_TYPE = Convert.ToString(reader["ENT_TYPE"]);
                    if (reader["ENT_STATE"] != DBNull.Value) _QY_GSDJXXMDL.ENT_STATE = Convert.ToString(reader["ENT_STATE"]);
                    if (reader["OP_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.OP_SCOPE = Convert.ToString(reader["OP_SCOPE"]);
                    if (reader["PT_BUS_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.PT_BUS_SCOPE = Convert.ToString(reader["PT_BUS_SCOPE"]);
                    if (reader["INDUSTRY_CO"] != DBNull.Value) _QY_GSDJXXMDL.INDUSTRY_CO = Convert.ToString(reader["INDUSTRY_CO"]);
                    if (reader["UPDATE_DATE"] != DBNull.Value) _QY_GSDJXXMDL.UPDATE_DATE = Convert.ToDateTime(reader["UPDATE_DATE"]);
                    if (reader["BUSINESS_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.BUSINESS_SCOPE = Convert.ToString(reader["BUSINESS_SCOPE"]);
                    if (reader["UNI_SCID"] != DBNull.Value) _QY_GSDJXXMDL.UNI_SCID = Convert.ToString(reader["UNI_SCID"]);
                    if (reader["VALID"] != DBNull.Value) _QY_GSDJXXMDL.VALID = Convert.ToInt32(reader["VALID"]);
                    if (reader["CJR"] != DBNull.Value) _QY_GSDJXXMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJDEPTID"] != DBNull.Value) _QY_GSDJXXMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
                    if (reader["CJSJ"] != DBNull.Value) _QY_GSDJXXMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _QY_GSDJXXMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGDEPTID"] != DBNull.Value) _QY_GSDJXXMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
                    if (reader["XGSJ"] != DBNull.Value) _QY_GSDJXXMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["DOM"] != DBNull.Value) _QY_GSDJXXMDL.DOM = Convert.ToString(reader["DOM"]);
                    if (reader["CAP_CUR"] != DBNull.Value) _QY_GSDJXXMDL.CAP_CUR = Convert.ToString(reader["CAP_CUR"]);
                }
                reader.Close();
                db.Close();
                return _QY_GSDJXXMDL;
            }
        }

        /// <summary>
        /// 验证企业工商信息（包含北京工商登记信息 + 外地进京备案）
        /// </summary>
        /// <param name="uni_scid">社会统一信用代码或组织机构代码</param>
        /// <returns>开业状态返回true，否则返回false</returns>
        public static bool CheckGongShang(string uni_scid)
        {
            //1、外地进京备案可视为工商通过
            string sql = "select count(*) from dbo.QY_BWDZZZS WHERE (ZZJGDM='{0}' or ZZJGDM like '________{0}_') and qylb ='外地进京'";

            int wdba = CommonDAL.SelectRowCount(string.Format(sql,uni_scid));//外地进京备案
            if(wdba >0)
            {
                return true;
            }

            //2、验证北京市工商登记信息

            //ENT_STATE
            //1：开业
            //2：吊销
            //3：注销
            //4：全部撤销
            //5：部分撤销
            sql = @"SELECT count(*) FROM [dbo].[QY_GSDJXX]
            			WHERE (uni_scid = '{0}' or uni_scid like '________{0}_') and ENT_STATE=1";
            wdba = CommonDAL.SelectRowCount(string.Format(sql, uni_scid));//外地进京备案
            if (wdba > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
//        /// <summary>
//        /// 本地测试：获取企业工商信息（需要本地数据库中存在表[QY_GSDJXX]）
//        /// </summary>
//        /// <param name="SHTYXYDM">社会统一信用代码</param>
//        /// <returns></returns>
//        public static GSJ_QY_GSDJXXMDL GetQY_GSDJXX_BySHTYXYDM_Test22222(string SHTYXYDM)
//        {
//            string sql = @"
//            			SELECT top 1 * FROM [TEMP_QY_GSDJXX]
//            			WHERE uni_scid = @uni_scid";
//            //            string sql = @"
//            //			SELECT top 1 * FROM [QY_GSDJXX]
//            //			WHERE uni_scid = @uni_scid";

//            DBHelper db = new DBHelper();
//            List<SqlParameter> p = new List<SqlParameter>();
//            p.Add(db.CreateParameter("uni_scid", DbType.String, SHTYXYDM));
//            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
//            {
//                GSJ_QY_GSDJXXMDL _QY_GSDJXXMDL = null;
//                if (reader.Read())
//                {
//                    _QY_GSDJXXMDL = new GSJ_QY_GSDJXXMDL();
//                    if (reader["ID"] != DBNull.Value) _QY_GSDJXXMDL.ID = new Guid(Convert.ToString(reader["ID"])).ToString();
//                    if (reader["ENT_NAME"] != DBNull.Value) _QY_GSDJXXMDL.ENT_NAME = Convert.ToString(reader["ENT_NAME"]);
//                    if (reader["REG_NO"] != DBNull.Value) _QY_GSDJXXMDL.REG_NO = Convert.ToString(reader["REG_NO"]);
//                    if (reader["OP_LOC"] != DBNull.Value) _QY_GSDJXXMDL.OP_LOC = Convert.ToString(reader["OP_LOC"]);
//                    if (reader["CORP_RPT"] != DBNull.Value) _QY_GSDJXXMDL.CORP_RPT = Convert.ToString(reader["CORP_RPT"]);
//                    if (reader["REG_CAP"] != DBNull.Value) _QY_GSDJXXMDL.REG_CAP = Convert.ToDecimal(reader["REG_CAP"]);
//                    if (reader["EST_DATE"] != DBNull.Value) _QY_GSDJXXMDL.EST_DATE = Convert.ToDateTime(reader["EST_DATE"]);
//                    if (reader["ENT_TYPE"] != DBNull.Value) _QY_GSDJXXMDL.ENT_TYPE = Convert.ToString(reader["ENT_TYPE"]);
//                    if (reader["ENT_STATE"] != DBNull.Value) _QY_GSDJXXMDL.ENT_STATE = Convert.ToString(reader["ENT_STATE"]);
//                    if (reader["OP_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.OP_SCOPE = Convert.ToString(reader["OP_SCOPE"]);
//                    if (reader["PT_BUS_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.PT_BUS_SCOPE = Convert.ToString(reader["PT_BUS_SCOPE"]);
//                    if (reader["INDUSTRY_CO"] != DBNull.Value) _QY_GSDJXXMDL.INDUSTRY_CO = Convert.ToString(reader["INDUSTRY_CO"]);
//                    if (reader["UPDATE_DATE"] != DBNull.Value) _QY_GSDJXXMDL.UPDATE_DATE = Convert.ToDateTime(reader["UPDATE_DATE"]);
//                    if (reader["BUSINESS_SCOPE"] != DBNull.Value) _QY_GSDJXXMDL.BUSINESS_SCOPE = Convert.ToString(reader["BUSINESS_SCOPE"]);
//                    if (reader["UNI_SCID"] != DBNull.Value) _QY_GSDJXXMDL.UNI_SCID = Convert.ToString(reader["UNI_SCID"]);
//                    if (reader["VALID"] != DBNull.Value) _QY_GSDJXXMDL.VALID = Convert.ToInt32(reader["VALID"]);
//                    if (reader["CJR"] != DBNull.Value) _QY_GSDJXXMDL.CJR = Convert.ToString(reader["CJR"]);
//                    if (reader["CJDEPTID"] != DBNull.Value) _QY_GSDJXXMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
//                    if (reader["CJSJ"] != DBNull.Value) _QY_GSDJXXMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
//                    if (reader["XGR"] != DBNull.Value) _QY_GSDJXXMDL.XGR = Convert.ToString(reader["XGR"]);
//                    if (reader["XGDEPTID"] != DBNull.Value) _QY_GSDJXXMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
//                    if (reader["XGSJ"] != DBNull.Value) _QY_GSDJXXMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
//                    if (reader["DOM"] != DBNull.Value) _QY_GSDJXXMDL.DOM = Convert.ToString(reader["DOM"]);
//                    if (reader["CAP_CUR"] != DBNull.Value) _QY_GSDJXXMDL.CAP_CUR = Convert.ToString(reader["CAP_CUR"]);
//                }
//                reader.Close();
//                db.Close();
//                return _QY_GSDJXXMDL;
//            }
//        }

        /// <summary>
        /// 更新企业工商验证信息
        /// </summary>
        /// <param name="_UnitMDL">对象实体类</param>
        /// <returns></returns>
        public static int UpdateResultGSXX(UnitMDL _UnitMDL)
        {
            return UpdateResultGSXX(null, _UnitMDL);
        }
        /// <summary>
        /// 更新企业工商验证信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_UnitMDL">对象实体类</param>
        /// <returns></returns>
        public static int UpdateResultGSXX(DbTransaction tran, UnitMDL _UnitMDL)
        {
            string sql = @"
			UPDATE dbo.Unit
				SET	ResultGSXX=@ResultGSXX,ApplyTimeGSXX=@ApplyTimeGSXX
			WHERE
				UnitID = @UnitID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitID", DbType.String, _UnitMDL.UnitID));
            p.Add(db.CreateParameter("ResultGSXX", DbType.Int32, _UnitMDL.ResultGSXX));
            p.Add(db.CreateParameter("ApplyTimeGSXX", DbType.DateTime, _UnitMDL.ApplyTimeGSXX));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }


        #region 测试
        /// <summary>
        /// 申请验证
        /// </summary>
        /// <param name="_UnitMDL"></param>
        /// <returns></returns>
        public static int InsertSubmit2(UnitMDL _UnitMDL)
        {
            return InsertSubmit2(null, _UnitMDL);
        }
        /// <summary>
        /// 申请验证工商信息
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="_UnitMDL"></param>
        /// <returns></returns>
        public static int InsertSubmit2(DbTransaction tran, UnitMDL _UnitMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"insert into Test.dbo.GSJ2(Name,CreditCode)
                                    values(@Name,@CreditCode)";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Name", DbType.String, _UnitMDL.ENT_Name));
            p.Add(db.CreateParameter("CreditCode", DbType.String, _UnitMDL.CreditCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        //验证企业工商信息
        public static GSJ_QY_GSDJXXMDL GetObjectUni_scid2(string uni_scid)
        {
            string sql = @"
			SELECT * FROM Test.dbo.GSJ1
			WHERE CreditCode = @CreditCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CreditCode", DbType.String, uni_scid));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                GSJ_QY_GSDJXXMDL _QY_GSDJXXMDL = null;
                if (reader.Read())
                {
                    _QY_GSDJXXMDL = new GSJ_QY_GSDJXXMDL();
                    if (reader["CreditCode"] != DBNull.Value) _QY_GSDJXXMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                }
                reader.Close();
                db.Close();
                return _QY_GSDJXXMDL;
            }
        }
        #endregion

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="UnitMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(UnitMDL _UnitMDL)
		{
			return Update(null,_UnitMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,UnitMDL _UnitMDL)
		{
			string sql = @"
			UPDATE dbo.Unit
				SET	BeginTime = @BeginTime,EndTime = @EndTime,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_Economic_Nature = @ENT_Economic_Nature,ENT_Province = @ENT_Province,ENT_Province_Code = @ENT_Province_Code,ENT_City = @ENT_City,END_Addess = @END_Addess,ENT_Corporate = @ENT_Corporate,ENT_Correspondence = @ENT_Correspondence,ENT_Postcode = @ENT_Postcode,ENT_Contact = @ENT_Contact,ENT_Telephone = @ENT_Telephone,ENT_MobilePhone = @ENT_MobilePhone,ENT_Type = @ENT_Type,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,CreditCode = @CreditCode,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo
			WHERE
				UnitID = @UnitID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitID",DbType.String, _UnitMDL.UnitID));
			p.Add(db.CreateParameter("BeginTime",DbType.DateTime, _UnitMDL.BeginTime));
			p.Add(db.CreateParameter("EndTime",DbType.DateTime, _UnitMDL.EndTime));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _UnitMDL.ENT_Name));
			p.Add(db.CreateParameter("ENT_OrganizationsCode",DbType.String, _UnitMDL.ENT_OrganizationsCode));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _UnitMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("ENT_Province",DbType.String, _UnitMDL.ENT_Province));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _UnitMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _UnitMDL.ENT_City));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _UnitMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Corporate",DbType.String, _UnitMDL.ENT_Corporate));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _UnitMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _UnitMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Contact",DbType.String, _UnitMDL.ENT_Contact));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _UnitMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_MobilePhone",DbType.String, _UnitMDL.ENT_MobilePhone));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _UnitMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _UnitMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _UnitMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _UnitMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _UnitMDL.CreditCode));
			p.Add(db.CreateParameter("CJR",DbType.String, _UnitMDL.CJR));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _UnitMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _UnitMDL.XGR));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _UnitMDL.XGSJ));
			p.Add(db.CreateParameter("Valid",DbType.Int32, _UnitMDL.Valid));
			p.Add(db.CreateParameter("Memo",DbType.String, _UnitMDL.Memo));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="UnitID">主键</param>
		/// <returns></returns>
        public static int Delete( string UnitID )
		{
			return Delete(null, UnitID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="UnitID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string UnitID)
		{
			string sql=@"DELETE FROM dbo.Unit WHERE UnitID = @UnitID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitID",DbType.String,UnitID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="UnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(UnitMDL _UnitMDL)
		{
			return Delete(null,_UnitMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,UnitMDL _UnitMDL)
		{
			string sql=@"DELETE FROM dbo.Unit WHERE UnitID = @UnitID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitID",DbType.String,_UnitMDL.UnitID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UnitID">主键</param>
        public static UnitMDL GetObject(string UnitID)
		{
			string sql= @"
			SELECT UnitID,BeginTime,EndTime,ENT_Name,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate,ENT_Correspondence,ENT_Postcode,ENT_Contact,ENT_Telephone,ENT_MobilePhone,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,CreditCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ResultGSXX,ApplyTimeGSXX
			FROM dbo.Unit
			WHERE UnitID = @UnitID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitID", DbType.String, UnitID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UnitMDL _UnitMDL = null;
                if (reader.Read())
                {
                    _UnitMDL = new UnitMDL();
					if (reader["UnitID"] != DBNull.Value) _UnitMDL.UnitID = Convert.ToString(reader["UnitID"]);
					if (reader["BeginTime"] != DBNull.Value) _UnitMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
					if (reader["EndTime"] != DBNull.Value) _UnitMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
					if (reader["ENT_Name"] != DBNull.Value) _UnitMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
					if (reader["ENT_OrganizationsCode"] != DBNull.Value) _UnitMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _UnitMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
					if (reader["ENT_Province"] != DBNull.Value) _UnitMDL.ENT_Province = Convert.ToString(reader["ENT_Province"]);
					if (reader["ENT_Province_Code"] != DBNull.Value) _UnitMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
					if (reader["ENT_City"] != DBNull.Value) _UnitMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
					if (reader["END_Addess"] != DBNull.Value) _UnitMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["ENT_Corporate"] != DBNull.Value) _UnitMDL.ENT_Corporate = Convert.ToString(reader["ENT_Corporate"]);
					if (reader["ENT_Correspondence"] != DBNull.Value) _UnitMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _UnitMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["ENT_Contact"] != DBNull.Value) _UnitMDL.ENT_Contact = Convert.ToString(reader["ENT_Contact"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _UnitMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_MobilePhone"] != DBNull.Value) _UnitMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
					if (reader["ENT_Type"] != DBNull.Value) _UnitMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
					if (reader["ENT_Sort"] != DBNull.Value) _UnitMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
					if (reader["ENT_Grade"] != DBNull.Value) _UnitMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
					if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _UnitMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
					if (reader["CreditCode"] != DBNull.Value) _UnitMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
					if (reader["CJR"] != DBNull.Value) _UnitMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["CJSJ"] != DBNull.Value) _UnitMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["XGR"] != DBNull.Value) _UnitMDL.XGR = Convert.ToString(reader["XGR"]);
					if (reader["XGSJ"] != DBNull.Value) _UnitMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
					if (reader["Valid"] != DBNull.Value) _UnitMDL.Valid = Convert.ToInt32(reader["Valid"]);
					if (reader["Memo"] != DBNull.Value) _UnitMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ResultGSXX"] != DBNull.Value) _UnitMDL.ResultGSXX = Convert.ToInt32(reader["ResultGSXX"]);
                    if (reader["ApplyTimeGSXX"] != DBNull.Value) _UnitMDL.ApplyTimeGSXX = Convert.ToDateTime(reader["ApplyTimeGSXX"]);
                }
				reader.Close();
                db.Close();
                return _UnitMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Unit", "*", filterWhereString, orderBy == "" ? " UnitID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Unit", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 根据组织机构代码从企业资质库获取企业名称
        /// ，资质比对顺序：1   建筑施工企业
        /// 2	中央在京
        /// 3	外地进京
        /// 4	起重机械租赁企业
        /// 5	设计施工一体化
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <returns>企业名称</returns>
        public static string GetUnitNameByCodeFromQY_BWDZZZS(string ZZJGDM)
        {
            return GetUnitNameByCodeFromQY_BWDZZZS(ZZJGDM, false);
        }

        /// <summary>
        /// 根据组织机构代码从企业资质库获取企业名称
        /// ，资质比对顺序：
        /// 1   建筑施工企业
        /// 2	中央在京
        /// 3	外地进京
        /// 4	起重机械租赁企业
        /// 5	设计施工一体化
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <param name="Strict">是否严查(不包括企业类别为“空”和“外地进京”)</param>
        /// <returns>企业名称</returns>
        public static string GetUnitNameByCodeFromQY_BWDZZZS(string ZZJGDM, bool Strict)
        {
            string sql = "";
            if (Strict == false)
            {
                sql = @"
select QYMC,
case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2	
when '外地进京' then 3				
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno from dbo.QY_BWDZZZS 
WHERE (ZZJGDM='{0}' or ZZJGDM like '________{0}_') and qymc is not null and qymc <>'' 
order by typeno";
            }
            else
            {
                sql = @"
select QYMC,
case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2		
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno from dbo.QY_BWDZZZS 
WHERE (ZZJGDM='{0}' or ZZJGDM like '________{0}_') and qylb is not null and qylb <>'' and qylb <>'外地进京' and qymc is not null and qymc <>'' 
order by typeno";
            }
            DBHelper db = new DBHelper();

            return Convert.ToString(db.ExecuteScalar(string.Format(sql, ZZJGDM)));
        }


        /// <summary>
        /// 根据企业名称从企业资质库获取组织机构代码
        /// ，资质比对顺序：
        /// 1   建筑施工企业
        /// 2	中央在京
        /// 3	外地进京
        /// 4	起重机械租赁企业
        /// 5	设计施工一体化
        /// </summary>
        /// <param name="QYMC">企业名称</param>
        /// <param name="Strict">是否严查(不包括企业类别为“空”和“外地进京”)</param>
        /// <returns>组织机构代码</returns>
        public static string GetUnitNameByUnitNameFromQY_BWDZZZS(string QYMC, bool Strict)
        {
            string sql = "";
            if (Strict == false)
            {
                sql = @"
select ZZJGDM,
case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2	
when '外地进京' then 3				
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno from dbo.QY_BWDZZZS 
WHERE (QYMC='{0}') and qymc is not null and qymc <>'' 
order by typeno";
            }
            else
            {
                sql = @"
select ZZJGDM,
case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2		
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno from dbo.QY_BWDZZZS 
WHERE (QYMC='{0}') and qylb is not null and qylb <>'' and qylb <>'外地进京' and qymc is not null and qymc <>'' 
order by typeno";
            }
            DBHelper db = new DBHelper();

            return Convert.ToString(db.ExecuteScalar(string.Format(sql, QYMC)));
        }

        /// <summary>
        /// 根据组织机构代码和资质类型查询企业
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <param name="zzlb">资质类别：建筑施工企业，中央在京，外地进京，起重机械租赁企业，设计施工一体化</param>
        /// <returns>企业名称</returns>
        public static string GetUnitNameByCodeAndZZLBFromQY_BWDZZZS(string ZZJGDM, string zzlb)
        {
            string sql = "select QYMC from dbo.QY_BWDZZZS WHERE ZZJGDM like '%" + ZZJGDM + "%' and qylb=@zzlb and qymc is not null and qymc <>''";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("zzlb", DbType.String, zzlb));
            return Convert.ToString(db.ExecuteScalar(sql, p.ToArray()));
        }

        /// <summary>
        /// 获取企业资质类别列表
        /// </summary>
        /// <param name="ZZJGDM">企业组织机构代码</param>
        /// <returns>企业资质类别集合</returns>
        public static List<string> GetUnitZZLBFromQY_BWDZZZS(string ZZJGDM)
        {
            string sql = "select distinct qylb from dbo.QY_BWDZZZS WHERE ZZJGDM like '%" + ZZJGDM + "%' and qymc <>''";
            DBHelper db = new DBHelper();

            DataTable dt = db.GetFillData(sql);
            List<string> zzlb = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                zzlb.Add(dr["qylb"].ToString());
            }
            return zzlb;
        }

        /// <summary>
        /// 获取企业资质同步实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetList_QY_BWDZZZS(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.QY_BWDZZZS", @"distinct case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2	
when '外地进京' then 3				
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno,qylb,zzjgdm,qymc", filterWhereString, orderBy == "" ? @" case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2	
when '外地进京' then 3				
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end" : orderBy);
        }

        /// <summary>
        /// 统计企业资质同步查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount_QY_BWDZZZS(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"(select  distinct case qylb 
when '建筑施工企业' then 1
when '中央在京' then 2	
when '外地进京' then 3				
when '起重机械租赁企业' then 4	
when '设计施工一体化' then 5	
else  6
end as typeno,qylb,zzjgdm,qymc FROM DBO.QY_BWDZZZS) t ", filterWhereString);
        }

        /// <summary>
        /// 根据组织机构代码获取单个实体
        /// </summary>
        /// <param name="ENT_OrganizationsCode">组织机构代/统一社会信用代码</param>
        public static UnitMDL GetObjectByENT_OrganizationsCode(DbTransaction tran, string ENT_OrganizationsCode)
        {
            string sql = @"
			SELECT UnitID,BeginTime,EndTime,ENT_Name,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate,ENT_Correspondence,ENT_Postcode,ENT_Contact,ENT_Telephone,ENT_MobilePhone,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,CreditCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ResultGSXX,ApplyTimeGSXX
			FROM dbo.Unit
			WHERE  
           (CreditCode =  @ENT_OrganizationsCode or ENT_OrganizationsCode =@ENT_OrganizationsCode) and Valid=1 and EndTime='2500-01-01 00:00:00.000'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, ENT_OrganizationsCode));
            p.Add(db.CreateParameter("CreditCode", DbType.String, ENT_OrganizationsCode));

            using (SqlDataReader reader = db.GetDataReader(tran,sql, p.ToArray()))
            {
                try
                {
                    UnitMDL _UnitMDL = null;
                    if (reader.Read())
                    {
                        _UnitMDL = new UnitMDL();
                        if (reader["UnitID"] != DBNull.Value) _UnitMDL.UnitID = Convert.ToString(reader["UnitID"]);
                        if (reader["BeginTime"] != DBNull.Value) _UnitMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                        if (reader["EndTime"] != DBNull.Value) _UnitMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                        if (reader["ENT_Name"] != DBNull.Value) _UnitMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                        if (reader["ENT_OrganizationsCode"] != DBNull.Value) _UnitMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                        if (reader["ENT_Economic_Nature"] != DBNull.Value) _UnitMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                        if (reader["ENT_Province"] != DBNull.Value) _UnitMDL.ENT_Province = Convert.ToString(reader["ENT_Province"]);
                        if (reader["ENT_Province_Code"] != DBNull.Value) _UnitMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                        if (reader["ENT_City"] != DBNull.Value) _UnitMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                        if (reader["END_Addess"] != DBNull.Value) _UnitMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                        if (reader["ENT_Corporate"] != DBNull.Value) _UnitMDL.ENT_Corporate = Convert.ToString(reader["ENT_Corporate"]);
                        if (reader["ENT_Correspondence"] != DBNull.Value) _UnitMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                        if (reader["ENT_Postcode"] != DBNull.Value) _UnitMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                        if (reader["ENT_Contact"] != DBNull.Value) _UnitMDL.ENT_Contact = Convert.ToString(reader["ENT_Contact"]);
                        if (reader["ENT_Telephone"] != DBNull.Value) _UnitMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                        if (reader["ENT_MobilePhone"] != DBNull.Value) _UnitMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
                        if (reader["ENT_Type"] != DBNull.Value) _UnitMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
                        if (reader["ENT_Sort"] != DBNull.Value) _UnitMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
                        if (reader["ENT_Grade"] != DBNull.Value) _UnitMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
                        if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _UnitMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
                        if (reader["CreditCode"] != DBNull.Value) _UnitMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                        if (reader["CJR"] != DBNull.Value) _UnitMDL.CJR = Convert.ToString(reader["CJR"]);
                        if (reader["CJSJ"] != DBNull.Value) _UnitMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                        if (reader["XGR"] != DBNull.Value) _UnitMDL.XGR = Convert.ToString(reader["XGR"]);
                        if (reader["XGSJ"] != DBNull.Value) _UnitMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                        if (reader["Valid"] != DBNull.Value) _UnitMDL.Valid = Convert.ToInt32(reader["Valid"]);
                        if (reader["Memo"] != DBNull.Value) _UnitMDL.Memo = Convert.ToString(reader["Memo"]);
                        if (reader["ResultGSXX"] != DBNull.Value) _UnitMDL.ResultGSXX = Convert.ToInt32(reader["ResultGSXX"]);
                        if (reader["ApplyTimeGSXX"] != DBNull.Value) _UnitMDL.ApplyTimeGSXX = Convert.ToDateTime(reader["ApplyTimeGSXX"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return _UnitMDL;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据组织机构代码获取企业信息
        /// </summary>
        /// <param name="ENT_OrganizationsCode">组织机构代/统一社会信用代码</param>
        public static UnitMDL GetObjectByENT_OrganizationsCode(string ENT_OrganizationsCode)
        {
            return GetObjectByENT_OrganizationsCode(null, ENT_OrganizationsCode);           
        }


        /// <summary>
        /// 根据单位名称获取单个实体
        /// </summary>
        /// <param name="UnitName">企业名称</param>
        public static UnitMDL GetObjectByUnitName(string ENT_Name)
        {
            string sql = @"
			SELECT top 1 UnitID,BeginTime,EndTime,ENT_Name,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate,ENT_Correspondence,ENT_Postcode,ENT_Contact,ENT_Telephone,ENT_MobilePhone,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,CreditCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ResultGSXX,ApplyTimeGSXX
			FROM dbo.Unit
			WHERE ENT_Name = @ENT_Name and Valid=1 and EndTime='2500-01-01 00:00:00.000'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_Name", DbType.String, ENT_Name));

            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                try
                {
                    UnitMDL _UnitMDL = null;
                    if (reader.Read())
                    {
                        _UnitMDL = new UnitMDL();
                        if (reader["UnitID"] != DBNull.Value) _UnitMDL.UnitID = Convert.ToString(reader["UnitID"]);
                        if (reader["BeginTime"] != DBNull.Value) _UnitMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                        if (reader["EndTime"] != DBNull.Value) _UnitMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                        if (reader["ENT_Name"] != DBNull.Value) _UnitMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                        if (reader["ENT_OrganizationsCode"] != DBNull.Value) _UnitMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                        if (reader["ENT_Economic_Nature"] != DBNull.Value) _UnitMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                        if (reader["ENT_Province"] != DBNull.Value) _UnitMDL.ENT_Province = Convert.ToString(reader["ENT_Province"]);
                        if (reader["ENT_Province_Code"] != DBNull.Value) _UnitMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                        if (reader["ENT_City"] != DBNull.Value) _UnitMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                        if (reader["END_Addess"] != DBNull.Value) _UnitMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                        if (reader["ENT_Corporate"] != DBNull.Value) _UnitMDL.ENT_Corporate = Convert.ToString(reader["ENT_Corporate"]);
                        if (reader["ENT_Correspondence"] != DBNull.Value) _UnitMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                        if (reader["ENT_Postcode"] != DBNull.Value) _UnitMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                        if (reader["ENT_Contact"] != DBNull.Value) _UnitMDL.ENT_Contact = Convert.ToString(reader["ENT_Contact"]);
                        if (reader["ENT_Telephone"] != DBNull.Value) _UnitMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                        if (reader["ENT_MobilePhone"] != DBNull.Value) _UnitMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
                        if (reader["ENT_Type"] != DBNull.Value) _UnitMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
                        if (reader["ENT_Sort"] != DBNull.Value) _UnitMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
                        if (reader["ENT_Grade"] != DBNull.Value) _UnitMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
                        if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _UnitMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
                        if (reader["CreditCode"] != DBNull.Value) _UnitMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                        if (reader["CJR"] != DBNull.Value) _UnitMDL.CJR = Convert.ToString(reader["CJR"]);
                        if (reader["CJSJ"] != DBNull.Value) _UnitMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                        if (reader["XGR"] != DBNull.Value) _UnitMDL.XGR = Convert.ToString(reader["XGR"]);
                        if (reader["XGSJ"] != DBNull.Value) _UnitMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                        if (reader["Valid"] != DBNull.Value) _UnitMDL.Valid = Convert.ToInt32(reader["Valid"]);
                        if (reader["Memo"] != DBNull.Value) _UnitMDL.Memo = Convert.ToString(reader["Memo"]);
                        if (reader["ResultGSXX"] != DBNull.Value) _UnitMDL.ResultGSXX = Convert.ToInt32(reader["ResultGSXX"]);
                        if (reader["ApplyTimeGSXX"] != DBNull.Value) _UnitMDL.ApplyTimeGSXX = Convert.ToDateTime(reader["ApplyTimeGSXX"]);
                    }
                    reader.Close();
                    db.Close();
                    return _UnitMDL;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
        }

        /// <summary>
        /// 企业工商登记信息查询
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns></returns>
        public static DataTable GetQY_GSDJXX_List(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.QY_GSDJXX", "*", filterWhereString, orderBy == "" ? " [ID]" : orderBy);
        }
        /// <summary>
        /// 统计企业工商登记信息记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectQY_GSDJXX_Count(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.QY_GSDJXX", filterWhereString);
        }

        //     /// <summary>
        //     /// 根据组织机构代码获取企业信息
        //     /// </summary>
        //     /// <param name="ZZJGDM"></param>
        //     /// <returns></returns>
        //     public static List<UnitMDL> GetObjectList(string ZZJGDM)
        //     {
        //         string sql = @"
        //select UnitID
        //   ,BeginTime ,EndTime,ENT_Name ,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province ,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate
        //   ,ENT_Correspondence ,ENT_Postcode,ENT_Contact ,ENT_Telephone ,ENT_MobilePhone ,ENT_Type ,ENT_Sort ,ENT_Grade ,ENT_QualificationCertificateNo
        //   ,CreditCode,CJR,CJSJ,XGR,XGSJ,Valid,Memo,ResultGSXX,ApplyTimeGSXX  FROM [dbo].[Unit]
        // WHERE ENT_OrganizationsCode = @ZZJGDM or CreditCode= @ZZJGDM";

        //         DBHelper db = new DBHelper();
        //         List<SqlParameter> p = new List<SqlParameter>();
        //         p.Add(db.CreateParameter("ZZJGDM", DbType.String, ZZJGDM));
        //         using (DataTable dt = db.GetFillData(sql, p.ToArray()))
        //         {
        //             List<UnitMDL> _ListunitMDLs = new List<UnitMDL>();
        //             if (dt.Rows.Count > 0)
        //             {
        //                 for (int i = 0; i < dt.Rows.Count; i++)
        //                 {
        //                     UnitMDL unitMDL = new UnitMDL();
        //                     if (dt.Rows[i]["UnitID"] != DBNull.Value) unitMDL.UnitID = Convert.ToString(dt.Rows[i]["UnitID"]);
        //                     if (dt.Rows[i]["BeginTime"] != DBNull.Value) unitMDL.BeginTime = Convert.ToDateTime(dt.Rows[i]["BeginTime"]);
        //                     if (dt.Rows[i]["EndTime"] != DBNull.Value) unitMDL.EndTime = Convert.ToDateTime(dt.Rows[i]["EndTime"]);
        //                     if (dt.Rows[i]["ENT_Name"] != DBNull.Value) unitMDL.ENT_Name = Convert.ToString(dt.Rows[i]["ENT_Name"]);
        //                     if (dt.Rows[i]["ENT_OrganizationsCode"] != DBNull.Value) unitMDL.ENT_OrganizationsCode = Convert.ToString(dt.Rows[i]["ENT_OrganizationsCode"]);
        //                     if (dt.Rows[i]["ENT_Economic_Nature"] != DBNull.Value) unitMDL.ENT_Economic_Nature = Convert.ToString(dt.Rows[i]["ENT_Economic_Nature"]);
        //                     if (dt.Rows[i]["ENT_Province"] != DBNull.Value) unitMDL.ENT_Province = Convert.ToString(dt.Rows[i]["ENT_Province"]);
        //                     if (dt.Rows[i]["ENT_City"] != DBNull.Value) unitMDL.ENT_City = Convert.ToString(dt.Rows[i]["ENT_City"]);
        //                     if (dt.Rows[i]["END_Addess"] != DBNull.Value) unitMDL.END_Addess = Convert.ToString(dt.Rows[i]["END_Addess"]);
        //                     if (dt.Rows[i]["ENT_Corporate"] != DBNull.Value) unitMDL.ENT_Corporate = Convert.ToString(dt.Rows[i]["ENT_Corporate"]);
        //                     if (dt.Rows[i]["ENT_Correspondence"] != DBNull.Value) unitMDL.ENT_Correspondence = Convert.ToString(dt.Rows[i]["ENT_Correspondence"]);
        //                     if (dt.Rows[i]["ENT_Postcode"] != DBNull.Value) unitMDL.ENT_Postcode = Convert.ToString(dt.Rows[i]["ENT_Postcode"]);
        //                     if (dt.Rows[i]["ENT_Contact"] != DBNull.Value) unitMDL.ENT_Contact = Convert.ToString(dt.Rows[i]["ENT_Contact"]);
        //                     if (dt.Rows[i]["ENT_Telephone"] != DBNull.Value) unitMDL.ENT_Telephone = Convert.ToString(dt.Rows[i]["ENT_Telephone"]);
        //                     if (dt.Rows[i]["ENT_MobilePhone"] != DBNull.Value) unitMDL.ENT_MobilePhone = Convert.ToString(dt.Rows[i]["ENT_MobilePhone"]);
        //                     if (dt.Rows[i]["ENT_Type"] != DBNull.Value) unitMDL.ENT_Type = Convert.ToString(dt.Rows[i]["ENT_Type"]);
        //                     if (dt.Rows[i]["ENT_Sort"] != DBNull.Value) unitMDL.ENT_Sort = Convert.ToString(dt.Rows[i]["ENT_Sort"]);
        //                     if (dt.Rows[i]["ENT_Grade"] != DBNull.Value) unitMDL.ENT_Grade = Convert.ToString(dt.Rows[i]["ENT_Grade"]);
        //                     if (dt.Rows[i]["ENT_QualificationCertificateNo"] != DBNull.Value) unitMDL.ENT_QualificationCertificateNo = Convert.ToString(dt.Rows[i]["ENT_QualificationCertificateNo"]);
        //                     if (dt.Rows[i]["CreditCode"] != DBNull.Value) unitMDL.CreditCode = Convert.ToString(dt.Rows[i]["CreditCode"]);
        //                     if (dt.Rows[i]["CJR"] != DBNull.Value) unitMDL.CJR = Convert.ToString(dt.Rows[i]["CJR"]);
        //                     if (dt.Rows[i]["CJSJ"] != DBNull.Value) unitMDL.CJSJ = Convert.ToDateTime(dt.Rows[i]["CJSJ"]);
        //                     if (dt.Rows[i]["XGR"] != DBNull.Value) unitMDL.XGR = Convert.ToString(dt.Rows[i]["XGR"]);
        //                     if (dt.Rows[i]["XGSJ"] != DBNull.Value) unitMDL.XGSJ = Convert.ToDateTime(dt.Rows[i]["XGSJ"]);
        //                     if (dt.Rows[i]["Valid"] != DBNull.Value) unitMDL.Valid = Convert.ToInt32(dt.Rows[i]["Valid"]);
        //                     if (dt.Rows[i]["Memo"] != DBNull.Value) unitMDL.Memo = Convert.ToString(dt.Rows[i]["Memo"]);
        //                     if (dt.Rows[i]["ResultGSXX"] != DBNull.Value) unitMDL.ResultGSXX = Convert.ToInt32(dt.Rows[i]["ResultGSXX"]);

        //                     _ListunitMDLs.Add(unitMDL);
        //                 }
        //             }
        //             db.Close();
        //             return _ListunitMDLs;
        //         }
        //     }

        // /// <summary>
        // /// 根据组织机构代码查询企业信息
        // /// </summary>
        // /// <param name="ENT_OrganizationsCode"></param>
        // /// <returns></returns>
        //public static int GetByENT_OrganizationsCode(string ENT_OrganizationsCode)
        //{
        //    //string Level = PSN_Level == "二级" ? "二级建造师" : "二级临时建造师";
        //    string sql = string.Format("and ENT_OrganizationsCode='{0}'and CreditCode='{1}'and Valid=1 and EndTime='2500-01-01 00:00:00.000'", ENT_OrganizationsCode, ENT_OrganizationsCode);
        //    return CommonDAL.SelectRowCount("[dbo].[Unit]", sql);
        //}
        #endregion



        public static UnitMDL GetObjectByPSN_RegisterNo(string PSN_RegisterNo)
        {
            string sql = @"
			SELECT UnitID,BeginTime,EndTime,ENT_Name,ENT_OrganizationsCode,ENT_Economic_Nature,ENT_Province,ENT_Province_Code,ENT_City,END_Addess,ENT_Corporate,ENT_Correspondence,ENT_Postcode,ENT_Contact,ENT_Telephone,ENT_MobilePhone,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,CreditCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.Unit
			WHERE  UnitID=(select [ENT_ServerID] from [dbo].[COC_TOW_Person_BaseInfo] where PSN_RegisterNo=@PSN_RegisterNo)";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, PSN_RegisterNo));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UnitMDL _UnitMDL = null;
                if (reader.Read())
                {
                    _UnitMDL = new UnitMDL();
                    if (reader["UnitID"] != DBNull.Value) _UnitMDL.UnitID = Convert.ToString(reader["UnitID"]);
                    if (reader["BeginTime"] != DBNull.Value) _UnitMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _UnitMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["ENT_Name"] != DBNull.Value) _UnitMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _UnitMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_Economic_Nature"] != DBNull.Value) _UnitMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                    if (reader["ENT_Province"] != DBNull.Value) _UnitMDL.ENT_Province = Convert.ToString(reader["ENT_Province"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _UnitMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["ENT_City"] != DBNull.Value) _UnitMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _UnitMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["ENT_Corporate"] != DBNull.Value) _UnitMDL.ENT_Corporate = Convert.ToString(reader["ENT_Corporate"]);
                    if (reader["ENT_Correspondence"] != DBNull.Value) _UnitMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                    if (reader["ENT_Postcode"] != DBNull.Value) _UnitMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                    if (reader["ENT_Contact"] != DBNull.Value) _UnitMDL.ENT_Contact = Convert.ToString(reader["ENT_Contact"]);
                    if (reader["ENT_Telephone"] != DBNull.Value) _UnitMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                    if (reader["ENT_MobilePhone"] != DBNull.Value) _UnitMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
                    if (reader["ENT_Type"] != DBNull.Value) _UnitMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
                    if (reader["ENT_Sort"] != DBNull.Value) _UnitMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
                    if (reader["ENT_Grade"] != DBNull.Value) _UnitMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
                    if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _UnitMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
                    if (reader["CreditCode"] != DBNull.Value) _UnitMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                    if (reader["CJR"] != DBNull.Value) _UnitMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _UnitMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _UnitMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _UnitMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _UnitMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _UnitMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _UnitMDL;
            }
        }
    }
}
