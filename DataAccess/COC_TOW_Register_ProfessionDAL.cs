using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--COC_TOW_Register_ProfessionDAL(填写类描述)
    /// </summary>
    public class COC_TOW_Register_ProfessionDAL
    {
        public COC_TOW_Register_ProfessionDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            return Insert(null, _COC_TOW_Register_ProfessionMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.COC_TOW_Register_Profession(PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime)
			VALUES (@PRO_ServerID,@PSN_ServerID,@PRO_Profession,@PRO_ValidityBegin,@PRO_ValidityEnd,@DogID,@ENT_Province_Code,@DownType,@LastModifyTime)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PRO_ServerID", DbType.String, _COC_TOW_Register_ProfessionMDL.PRO_ServerID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Register_ProfessionMDL.PSN_ServerID));
            p.Add(db.CreateParameter("PRO_Profession", DbType.String, _COC_TOW_Register_ProfessionMDL.PRO_Profession));
            p.Add(db.CreateParameter("PRO_ValidityBegin", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin));
            p.Add(db.CreateParameter("PRO_ValidityEnd", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd));
            p.Add(db.CreateParameter("DogID", DbType.String, _COC_TOW_Register_ProfessionMDL.DogID));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Register_ProfessionMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("DownType", DbType.String, _COC_TOW_Register_ProfessionMDL.DownType));
            p.Add(db.CreateParameter("LastModifyTime", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.LastModifyTime));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            return Update(null, _COC_TOW_Register_ProfessionMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            string sql = @"
			UPDATE dbo.COC_TOW_Register_Profession
				SET	PSN_ServerID = @PSN_ServerID,PRO_Profession = @PRO_Profession,PRO_ValidityBegin = @PRO_ValidityBegin,PRO_ValidityEnd = @PRO_ValidityEnd,DogID = @DogID,ENT_Province_Code = @ENT_Province_Code,DownType = @DownType,LastModifyTime = @LastModifyTime
			WHERE
				PRO_ServerID = @PRO_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PRO_ServerID", DbType.String, _COC_TOW_Register_ProfessionMDL.PRO_ServerID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Register_ProfessionMDL.PSN_ServerID));
            p.Add(db.CreateParameter("PRO_Profession", DbType.String, _COC_TOW_Register_ProfessionMDL.PRO_Profession));
            p.Add(db.CreateParameter("PRO_ValidityBegin", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin));
            p.Add(db.CreateParameter("PRO_ValidityEnd", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd));
            p.Add(db.CreateParameter("DogID", DbType.String, _COC_TOW_Register_ProfessionMDL.DogID));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Register_ProfessionMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("DownType", DbType.String, _COC_TOW_Register_ProfessionMDL.DownType));
            p.Add(db.CreateParameter("LastModifyTime", DbType.DateTime, _COC_TOW_Register_ProfessionMDL.LastModifyTime));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="COC_TOW_Register_ProfessionID">主键</param>
        /// <returns></returns>
        public static int Delete(string PRO_ServerID)
        {
            return Delete(null, PRO_ServerID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_ProfessionID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string PRO_ServerID)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Register_Profession WHERE PRO_ServerID = @PRO_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PRO_ServerID", DbType.String, PRO_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            return Delete(null, _COC_TOW_Register_ProfessionMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Register_ProfessionMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Register_Profession WHERE PRO_ServerID = @PRO_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PRO_ServerID", DbType.String, _COC_TOW_Register_ProfessionMDL.PRO_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PRO_ServerID">专业ID</param>
        public static COC_TOW_Register_ProfessionMDL GetObject(string PRO_ServerID)
        {
            string sql = @"
			SELECT PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime
			FROM dbo.COC_TOW_Register_Profession
			WHERE PRO_ServerID = @PRO_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PRO_ServerID", DbType.String, PRO_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
                    if (reader["PRO_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Convert.ToString(reader["PRO_ServerID"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["PRO_Profession"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = Convert.ToString(reader["PRO_Profession"]);
                    if (reader["PRO_ValidityBegin"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(reader["PRO_ValidityBegin"]);
                    if (reader["PRO_ValidityEnd"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = Convert.ToDateTime(reader["PRO_ValidityEnd"]);
                    if (reader["DogID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DogID = Convert.ToString(reader["DogID"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["DownType"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DownType = Convert.ToString(reader["DownType"]);
                    if (reader["LastModifyTime"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Register_ProfessionMDL;
            }
        }

        /// <summary>
        /// 根据建造师ID获取专业集合
        /// </summary>
        /// <param name="PSN_ServerID">人员ID</param>
        public static List<COC_TOW_Register_ProfessionMDL> GetListGetObject(string PSN_ServerID)
        {
            string sql = @"
				SELECT PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime
			FROM dbo.COC_TOW_Register_Profession
			WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<COC_TOW_Register_ProfessionMDL> list = new List<COC_TOW_Register_ProfessionMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
                        if (dt.Rows[i]["PRO_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Convert.ToString(dt.Rows[i]["PRO_ServerID"]);
                        if (dt.Rows[i]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = Convert.ToString(dt.Rows[i]["PSN_ServerID"]);
                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = Convert.ToString(dt.Rows[i]["PRO_Profession"]);
                        if (dt.Rows[i]["PRO_ValidityBegin"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                        if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                        if (dt.Rows[i]["DogID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DogID = Convert.ToString(dt.Rows[i]["DogID"]);
                        if (dt.Rows[i]["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.ENT_Province_Code = Convert.ToString(dt.Rows[i]["ENT_Province_Code"]);
                        if (dt.Rows[i]["DownType"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DownType = Convert.ToString(dt.Rows[i]["DownType"]);
                        if (dt.Rows[i]["LastModifyTime"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.LastModifyTime = Convert.ToDateTime(dt.Rows[i]["LastModifyTime"]);
                        list.Add(_COC_TOW_Register_ProfessionMDL);
                    }
                }
                db.Close();
                return list;
            }
        }

        /// <summary>
        /// 根据建造师ID获取未过期的专业集合
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PSN_ServerID">人员ID</param>
        public static List<COC_TOW_Register_ProfessionMDL> ListGetObject(DbTransaction tran, string PSN_ServerID)
        {
            string sql = @"
				SELECT PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime
			FROM dbo.COC_TOW_Register_Profession
			WHERE PSN_ServerID = @PSN_ServerID AND PRO_ValidityEnd > getdate()";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<COC_TOW_Register_ProfessionMDL> __COC_TOW_Register_ProfessionMDL = new List<COC_TOW_Register_ProfessionMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
                        if (dt.Rows[i]["PRO_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Convert.ToString(dt.Rows[i]["PRO_ServerID"]);
                        if (dt.Rows[i]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = Convert.ToString(dt.Rows[i]["PSN_ServerID"]);
                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = Convert.ToString(dt.Rows[i]["PRO_Profession"]);
                        if (dt.Rows[i]["PRO_ValidityBegin"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                        if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                        if (dt.Rows[i]["DogID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DogID = Convert.ToString(dt.Rows[i]["DogID"]);
                        if (dt.Rows[i]["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.ENT_Province_Code = Convert.ToString(dt.Rows[i]["ENT_Province_Code"]);
                        if (dt.Rows[i]["DownType"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DownType = Convert.ToString(dt.Rows[i]["DownType"]);
                        if (dt.Rows[i]["LastModifyTime"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.LastModifyTime = Convert.ToDateTime(dt.Rows[i]["LastModifyTime"]);
                        __COC_TOW_Register_ProfessionMDL.Add(_COC_TOW_Register_ProfessionMDL);
                    }
                }
                db.Close();
                return __COC_TOW_Register_ProfessionMDL;
            }
        }


        /// <summary>
        /// 根据建造师ID获取专业集合
        /// </summary>
        /// <param name="PSN_ServerID">主键</param>
        public static List<COC_TOW_Register_ProfessionMDL> ListGetObject2(DbTransaction tran, string PSN_ServerID)
        {
            string sql = @"
				SELECT PRO_ServerID,PSN_ServerID,PRO_Profession,PRO_ValidityBegin,PRO_ValidityEnd,DogID,ENT_Province_Code,DownType,LastModifyTime
			FROM dbo.COC_TOW_Register_Profession
			WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<COC_TOW_Register_ProfessionMDL> __COC_TOW_Register_ProfessionMDL = new List<COC_TOW_Register_ProfessionMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        COC_TOW_Register_ProfessionMDL _COC_TOW_Register_ProfessionMDL = new COC_TOW_Register_ProfessionMDL();
                        if (dt.Rows[i]["PRO_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ServerID = Convert.ToString(dt.Rows[i]["PRO_ServerID"]);
                        if (dt.Rows[i]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PSN_ServerID = Convert.ToString(dt.Rows[i]["PSN_ServerID"]);
                        if (dt.Rows[i]["PRO_Profession"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_Profession = Convert.ToString(dt.Rows[i]["PRO_Profession"]);
                        if (dt.Rows[i]["PRO_ValidityBegin"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityBegin = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                        if (dt.Rows[i]["PRO_ValidityEnd"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.PRO_ValidityEnd = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                        if (dt.Rows[i]["DogID"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DogID = Convert.ToString(dt.Rows[i]["DogID"]);
                        if (dt.Rows[i]["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.ENT_Province_Code = Convert.ToString(dt.Rows[i]["ENT_Province_Code"]);
                        if (dt.Rows[i]["DownType"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.DownType = Convert.ToString(dt.Rows[i]["DownType"]);
                        if (dt.Rows[i]["LastModifyTime"] != DBNull.Value) _COC_TOW_Register_ProfessionMDL.LastModifyTime = Convert.ToDateTime(dt.Rows[i]["LastModifyTime"]);
                        __COC_TOW_Register_ProfessionMDL.Add(_COC_TOW_Register_ProfessionMDL);
                    }
                }
                db.Close();
                return __COC_TOW_Register_ProfessionMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_TOW_Register_Profession", "*", filterWhereString, orderBy == "" ? " PRO_ServerID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_TOW_Register_Profession", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 获取证书专业
        /// </summary>
        /// <param name="PSN_ServerID">人员ID</param>
        /// <returns>专业集合</returns>
        public static DataTable GetListByPSN_ServerID(string PSN_ServerID)
        {
            string sql = "select * from dbo.COC_TOW_Register_Profession where PSN_ServerID='{0}' ";
            return CommonDAL.GetDataTable(string.Format(sql, PSN_ServerID));
        }
        /// <summary>
        /// 统计该区县下分类注册人员分别有多少人
        /// </summary>
        /// <param name="city">区县名称</param>
        /// <returns></returns>
        public static DataTable GetZhuanYe(string city)
        {
            DBHelper db = new DBHelper();
            string sql = @"select b.pro_profession,count(b.pro_serverid) as num 
                           from  COC_TOW_Person_BaseInfo a left join COC_TOW_Register_Profession b on a.[PSN_ServerID]=b.[PSN_ServerID]
                           where len(b.pro_profession) >0 {0} group by b.pro_profession ";
            return db.GetFillData(string.Format(sql, city));
        }


        /// <summary>
        /// 获取证书指定专业最大有效期
        /// </summary>
        /// <param name="PSN_ServerID">证书ID</param>
        /// <param name="ListProfession">专业集合（用英文逗号分隔，如：建筑,市政）</param>
        /// <returns>专业集合中最大有效截止日期</returns>
        public static DateTime? GetMaxPRO_ValidityEnd(string PSN_ServerID, string ListProfession)
        {
            DBHelper db = new DBHelper();
            string sql = @"select max([PRO_ValidityEnd]) max_ValidityEnd
                            from [dbo].[COC_TOW_Register_Profession]
                            where [PSN_ServerID]='{0}' and CHARINDEX(PRO_Profession ,'{1}',1) >0";
            DataTable dt = db.GetFillData(string.Format(sql, PSN_ServerID, ListProfession));
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToDateTime(dt.Rows[0]["max_ValidityEnd"]);
            else
                return null;
        }
        #endregion
    }
}
