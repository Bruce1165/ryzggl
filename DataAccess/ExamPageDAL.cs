using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamPageDAL(填写类描述)
    /// </summary>
    public class ExamPageDAL
    {
        public ExamPageDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamPageOB _ExamPageOB)
        {
            return Insert(null, _ExamPageOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamPageOB _ExamPageOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.EXAMPAGE(ExamYear,SubjectID,ExamPageTitle,Remark,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Difficulty,Score,Flag,TimeLimit)
			VALUES (@ExamYear,@SubjectID,@ExamPageTitle,@Remark,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@Difficulty,@Score,@Flag,@TimeLimit);SELECT @ExamPageID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("EXAMPAGEID", DbType.Int64));
            p.Add(db.CreateParameter("ExamYear", DbType.Int32, _ExamPageOB.ExamYear));
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, _ExamPageOB.SubjectID));
            p.Add(db.CreateParameter("ExamPageTitle", DbType.String, _ExamPageOB.ExamPageTitle));
            p.Add(db.CreateParameter("Remark", DbType.String, _ExamPageOB.Remark));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamPageOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamPageOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamPageOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamPageOB.ModifyTime));
            p.Add(db.CreateParameter("Difficulty", DbType.String, _ExamPageOB.Difficulty));
            p.Add(db.CreateParameter("Score", DbType.Int32, _ExamPageOB.Score));
            p.Add(db.CreateParameter("Flag", DbType.String, _ExamPageOB.Flag));
            p.Add(db.CreateParameter("TimeLimit", DbType.Int32, _ExamPageOB.TimeLimit));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamPageOB.ExamPageID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamPageOB _ExamPageOB)
        {
            return Update(null, _ExamPageOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamPageOB _ExamPageOB)
        {
            string sql = @"
			UPDATE dbo.ExamPage
				SET	ExamYear = @ExamYear,SubjectID = @SubjectID,ExamPageTitle = @ExamPageTitle,Remark = @Remark,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,Difficulty = @Difficulty,Score = @Score,Flag = @Flag,TimeLimit = @TimeLimit
			WHERE
				ExamPageID = @ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, _ExamPageOB.ExamPageID));
            p.Add(db.CreateParameter("ExamYear", DbType.Int16, _ExamPageOB.ExamYear));
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, _ExamPageOB.SubjectID));
            p.Add(db.CreateParameter("ExamPageTitle", DbType.String, _ExamPageOB.ExamPageTitle));
            p.Add(db.CreateParameter("Remark", DbType.String, _ExamPageOB.Remark));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamPageOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamPageOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamPageOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamPageOB.ModifyTime));
            p.Add(db.CreateParameter("Difficulty", DbType.String, _ExamPageOB.Difficulty));
            p.Add(db.CreateParameter("Score", DbType.Int32, _ExamPageOB.Score));
            p.Add(db.CreateParameter("Flag", DbType.String, _ExamPageOB.Flag));
            p.Add(db.CreateParameter("TimeLimit", DbType.Int32, _ExamPageOB.TimeLimit));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamPageID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamPageID)
        {
            return Delete(null, ExamPageID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPageID)
        {
            string sql = @"DELETE FROM dbo.ExamPage WHERE ExamPageID = @ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamPageOB _ExamPageOB)
        {
            return Delete(null, _ExamPageOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamPageOB _ExamPageOB)
        {
            string sql = @"DELETE FROM dbo.ExamPage WHERE ExamPageID = @ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, _ExamPageOB.ExamPageID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPageID">主键</param>
        public static ExamPageOB GetObject(long ExamPageID)
        {
            string sql = @"
			SELECT ExamPageID,ExamYear,SubjectID,ExamPageTitle,Remark,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Difficulty,Score,Flag,TimeLimit
			FROM dbo.ExamPage
			WHERE ExamPageID = @ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamPageOB _ExamPageOB = null;
                if (reader.Read())
                {
                    _ExamPageOB = new ExamPageOB();
                    if (reader["ExamPageID"] != DBNull.Value) _ExamPageOB.ExamPageID = Convert.ToInt64(reader["ExamPageID"]);
                    if (reader["ExamYear"] != DBNull.Value) _ExamPageOB.ExamYear = Convert.ToInt16(reader["ExamYear"]);
                    if (reader["SubjectID"] != DBNull.Value) _ExamPageOB.SubjectID = Convert.ToInt32(reader["SubjectID"]);
                    if (reader["ExamPageTitle"] != DBNull.Value) _ExamPageOB.ExamPageTitle = Convert.ToString(reader["ExamPageTitle"]);
                    if (reader["Remark"] != DBNull.Value) _ExamPageOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _ExamPageOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _ExamPageOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _ExamPageOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _ExamPageOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["Difficulty"] != DBNull.Value) _ExamPageOB.Difficulty = Convert.ToString(reader["Difficulty"]);
                    if (reader["Score"] != DBNull.Value) _ExamPageOB.Score = Convert.ToInt32(reader["Score"]);
                    if (reader["Flag"] != DBNull.Value) _ExamPageOB.Flag = Convert.ToString(reader["Flag"]);
                    if (reader["TimeLimit"] != DBNull.Value) _ExamPageOB.TimeLimit = Convert.ToInt32(reader["TimeLimit"]);
                }
                reader.Close();
                db.Close();
                return _ExamPageOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPage", "*", filterWhereString, orderBy == "" ? " ExamPageID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPage", filterWhereString);
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
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ExamPage", "*", filterWhereString, orderBy == "" ? " ExamPageID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectViewCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ExamPage", filterWhereString);
        }

        /// <summary>
        /// 根据试题难度更新试卷难度
        /// </summary>
        /// <param name="ExamPageID">试卷ID</param>
        /// <returns></returns>
        public static int UpdateDifficulty(long ExamPageID)
        {
            string sql = @"
			update dbo.ExamPage
            set Difficulty =(SELECT cast(cast(round(avg(cast(DIFFICULTY as numeric(9,2))),2) as numeric(9,2) ) as varchar(20)) FROM DBO.PAGEQUESTION where EXAMPAGEID=@ExamPageID)
            where EXAMPAGEID=@ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));         
            return db.ExcuteNonQuery(sql, p.ToArray());
        }
    }
}