using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamSignUpPlaceDAL(填写类描述)
    /// </summary>
    public class ExamSignUpPlaceDAL
    {
        public ExamSignUpPlaceDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            return Insert(null, _ExamSignUpPlaceOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamSignUpPlace(ExamPlanID,SignUpPlaceID,PlaceName,Address,Phone,CheckPersonLimit)
			VALUES (@ExamPlanID,@SignUpPlaceID,@PlaceName,@Address,@Phone,@CheckPersonLimit)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUpPlaceOB.ExamPlanID));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, _ExamSignUpPlaceOB.SignUpPlaceID));
            p.Add(db.CreateParameter("PlaceName", DbType.String, _ExamSignUpPlaceOB.PlaceName));
            p.Add(db.CreateParameter("Address", DbType.String, _ExamSignUpPlaceOB.Address));
            p.Add(db.CreateParameter("Phone", DbType.String, _ExamSignUpPlaceOB.Phone));
            p.Add(db.CreateParameter("CheckPersonLimit", DbType.Int32, _ExamSignUpPlaceOB.CheckPersonLimit));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            return Update(null, _ExamSignUpPlaceOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            string sql = @"
			UPDATE dbo.ExamSignUpPlace
				SET	PlaceName =@PlaceName,Address =@Address,Phone =@Phone,CheckPersonLimit =@CheckPersonLimit
			WHERE
				ExamPlanID =@ExamPlanID and SignUpPlaceID =@SignUpPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUpPlaceOB.ExamPlanID));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, _ExamSignUpPlaceOB.SignUpPlaceID));
            p.Add(db.CreateParameter("PlaceName", DbType.String, _ExamSignUpPlaceOB.PlaceName));
            p.Add(db.CreateParameter("Address", DbType.String, _ExamSignUpPlaceOB.Address));
            p.Add(db.CreateParameter("Phone", DbType.String, _ExamSignUpPlaceOB.Phone));
            p.Add(db.CreateParameter("CheckPersonLimit", DbType.Int32, _ExamSignUpPlaceOB.CheckPersonLimit));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamSignUpPlaceID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamPlanID, long SignUpPlaceID)
        {
            return Delete(null, ExamPlanID, SignUpPlaceID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPlanID, long SignUpPlaceID)
        {
            string sql = @"DELETE FROM dbo.ExamSignUpPlace WHERE ExamPlanID =@ExamPlanID and SignUpPlaceID =@SignUpPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, SignUpPlaceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceID">主键</param>
        /// <returns></returns>
        public static int DeleteByExamPlanID(DbTransaction tran, long ExamPlanID)
        {
            string sql = @"DELETE FROM dbo.ExamSignUpPlace WHERE ExamPlanID =@ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            return Delete(null, _ExamSignUpPlaceOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpPlaceOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamSignUpPlaceOB _ExamSignUpPlaceOB)
        {
            string sql = @"DELETE FROM dbo.ExamSignUpPlace WHERE ExamPlanID =@ExamPlanID and SignUpPlaceID =@SignUpPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUpPlaceOB.ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamSignUpPlaceID">主键</param>
        public static ExamSignUpPlaceOB GetObject(long ExamPlanID, long SignUpPlaceID)
        {
            string sql = @"
			SELECT ExamPlanID,SignUpPlaceID,PlaceName,Address,Phone,CheckPersonLimit
			FROM dbo.ExamSignUpPlace
			WHERE ExamPlanID =@ExamPlanID  and SignUpPlaceID =@SignUpPlaceID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, SignUpPlaceID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamSignUpPlaceOB _ExamSignUpPlaceOB = null;
                if (reader.Read())
                {
                    _ExamSignUpPlaceOB = new ExamSignUpPlaceOB();
                    if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUpPlaceOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                    if (reader["SignUpPlaceID"] != DBNull.Value) _ExamSignUpPlaceOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
                    if (reader["PlaceName"] != DBNull.Value) _ExamSignUpPlaceOB.PlaceName = Convert.ToString(reader["PlaceName"]);
                    if (reader["Address"] != DBNull.Value) _ExamSignUpPlaceOB.Address = Convert.ToString(reader["Address"]);
                    if (reader["Phone"] != DBNull.Value) _ExamSignUpPlaceOB.Phone = Convert.ToString(reader["Phone"]);
                    if (reader["CheckPersonLimit"] != DBNull.Value) _ExamSignUpPlaceOB.CheckPersonLimit = Convert.ToInt32(reader["CheckPersonLimit"]);
                }
                reader.Close();
                db.Close();
                return _ExamSignUpPlaceOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamSignUpPlace", "*", filterWhereString, orderBy == "" ? " ExamPlanID,SignUpPlaceID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamSignUpPlace", filterWhereString);
        }

        /// <summary>
        /// 根据考试计划获取报名审核点（全量，包含未选择的培训点）
        /// </summary>
        /// <param name="ExamPlanID">考试计划</param>
        /// <returns></returns>
        public static DataTable GetListByExamPlanID(long ExamPlanID)
        {
            string sql = @"
                    SELECT SIGNUPPLACEID,PLACENAME,[ADDRESS],PHONE,CHECKPERSONLIMIT,1 CHECKED
                    FROM DBO.EXAMSIGNUPPLACE
                    where EXAMPLANID={0}
                    union 
                    SELECT SIGNUPPLACEID,PLACENAME,ADDRESS,PHONE,CHECKPERSONLIMIT,0 CHECKED
                    FROM DBO.SIGNUPPLACE
                    where SIGNUPPLACEID in
                    (
                        SELECT SIGNUPPLACEID
                        FROM DBO.SIGNUPPLACE
                        EXCEPT 
                        SELECT SIGNUPPLACEID
                        from DBO.EXAMSIGNUPPLACE 
                        where EXAMSIGNUPPLACE.EXAMPLANID={0}
                    );";
            return CommonDAL.GetDataTable(string.Format(sql, ExamPlanID));
        }

        /// <summary>
        /// 获取初审点报名信息（包括人数限制和当前已报名人数）
        /// </summary>
        /// <param name="ExamPlanID"></param>
        /// <returns></returns>
        public static DataTable GetSignUpPlaceTj(long ExamPlanID)
        {
            string sql = @"SELECT p.SIGNUPPLACEID,p.PLACENAME,p.ADDRESS,p.PHONE,p.CHECKPERSONLIMIT,e.STARTCHECKDATE,e.LATESTCHECKDATE
                                ,DATEDIFF(day,e.STARTCHECKDATE,e.LATESTCHECKDATE) +1 checkdateCount 
                                ,p.CHECKPERSONLIMIT * (DATEDIFF(day,e.STARTCHECKDATE,e.LATESTCHECKDATE) +1) ManLimit
                                ,isnull(s.SignupManCount,0) SignupManCount
                            from DBO.EXAMSIGNUPPLACE p
                            inner join dbo.examplan e on p.EXAMPLANID=e.EXAMPLANID
                            left join 
                            (
                                SELECT SIGNUPPLACEID,count(*) SignupManCount
                                FROM DBO.EXAMSIGNUP
                                where EXAMPLANID ={0}  
                                group by  SIGNUPPLACEID
                            ) s on p.SIGNUPPLACEID = s.SIGNUPPLACEID
                            where p.EXAMPLANID={0}";
            return CommonDAL.GetDataTable(string.Format(sql, ExamPlanID));
        }


        /// <summary>
        /// 获取初审点每天报名信息（包括人数限制和当前已报名人数）
        /// </summary>
        /// <param name="ExamPlanID"></param>
        /// <returns></returns>
        public static DataTable GetSignUpPlaceTjByDate(long ExamPlanID)
        {
            string sql = @"select p.SIGNUPPLACEID,p.CHECKPERSONLIMIT,isnull(s.CHECKDATEPLAN,'2000-1-1') CHECKDATEPLAN,isnull(s.SignupManCount,0) SignupManCount
                            from DBO.EXAMSIGNUPPLACE p
                            left join 
                            (
                                SELECT SIGNUPPLACEID,CHECKDATEPLAN,count(*) SignupManCount
                                FROM DBO.EXAMSIGNUP
                                where EXAMPLANID ={0}  
                                group by  SIGNUPPLACEID,CHECKDATEPLAN
                            ) s on p.SIGNUPPLACEID = s.SIGNUPPLACEID
                            where p.EXAMPLANID ={0}";
            return CommonDAL.GetDataTable(string.Format(sql, ExamPlanID));
        }
    }
}
