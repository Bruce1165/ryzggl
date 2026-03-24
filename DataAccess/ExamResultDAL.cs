using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamResultDAL(填写类描述)
    /// </summary>
    public class ExamResultDAL
    {
        public ExamResultDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamResultOB _ExamResultOB)
        {
            return Insert(null, _ExamResultOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamResultOB _ExamResultOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamResult(ExamRoomAllotID,ExamPlanID,WorkerID,ExamCardID,ExamResult,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamSignUp_ID)
			VALUES (@ExamRoomAllotID,@ExamPlanID,@WorkerID,@ExamCardID,@ExamResult,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@ExamSignUp_ID);SELECT @ExamResultID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamResultID", DbType.Int64));
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamResultOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamResultOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamResultOB.WorkerID));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _ExamResultOB.ExamResult));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamResultOB.ModifyTime));
            p.Add(db.CreateParameter("ExamSignUp_ID", DbType.Int64, _ExamResultOB.ExamSignUp_ID));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamResultOB.ExamResultID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamResultOB _ExamResultOB)
        {
            return Update(null, _ExamResultOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamResultOB _ExamResultOB)
        {
            string sql = @"
			UPDATE dbo.ExamResult
				SET	ExamRoomAllotID = @ExamRoomAllotID,ExamPlanID = @ExamPlanID,WorkerID = @WorkerID,ExamCardID = @ExamCardID,ExamResult = @ExamResult,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, _ExamResultOB.ExamResultID));
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamResultOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamResultOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamResultOB.WorkerID));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _ExamResultOB.ExamResult));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamResultOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamResultID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamResultID)
        {
            return Delete(null, ExamResultID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamResultID)
        {
            string sql = @"DELETE FROM dbo.ExamResult WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, ExamResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamResultOB _ExamResultOB)
        {
            return Delete(null, _ExamResultOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamResultOB _ExamResultOB)
        {
            string sql = @"DELETE FROM dbo.ExamResult WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, _ExamResultOB.ExamResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 删除考试计划已分配的准考证
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamPlanID">考试计划ID</param>
        /// <returns></returns>
        public static int DeleteByExamPlanID(DbTransaction tran, long _ExamPlanID)
        {
            string sql = @"DELETE FROM dbo.ExamResult WHERE ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamResultID">主键</param>
        public static ExamResultOB GetObject(long ExamResultID)
        {
            string sql = @"
			SELECT ExamResultID,ExamRoomAllotID,ExamPlanID,WorkerID,ExamCardID,ExamResult,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamSignUp_ID
			FROM dbo.ExamResult
			WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, ExamResultID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamResultOB _ExamResultOB = null;
                    if (reader.Read())
                    {
                        _ExamResultOB = new ExamResultOB();
                        if (reader["ExamResultID"] != DBNull.Value) _ExamResultOB.ExamResultID = Convert.ToInt64(reader["ExamResultID"]);
                        if (reader["ExamRoomAllotID"] != DBNull.Value) _ExamResultOB.ExamRoomAllotID = Convert.ToInt64(reader["ExamRoomAllotID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamResultOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamResultOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["ExamCardID"] != DBNull.Value) _ExamResultOB.ExamCardID = Convert.ToString(reader["ExamCardID"]);
                        if (reader["ExamResult"] != DBNull.Value) _ExamResultOB.ExamResult = Convert.ToString(reader["ExamResult"]);
                        if (reader["Status"] != DBNull.Value) _ExamResultOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamResultOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamResultOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamResultOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamResultOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["ExamSignUp_ID"] != DBNull.Value) _ExamResultOB.ExamSignUp_ID = Convert.ToInt64(reader["ExamSignUp_ID"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamResultOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamResult", "*", filterWhereString, orderBy == "" ? " ExamResultID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamResult", filterWhereString);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ExamResult", "*", filterWhereString, orderBy == "" ? "ExamPlanID desc,ExamCardID" : orderBy);
        }

        public static DataTable GetListView_ExamResult(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_CERTIFICATE_BEFORE", "*", filterWhereString, orderBy == "" ? "ExamCardID" : orderBy);
        }

        /// <summary>
        /// 获取证书发放培训点对照表
        /// </summary>
        /// <param name="examPlanID">考试计划ID</param>
        /// <returns></returns>
        public static DataTable GetExamCertificateTrainUnitList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.certificate", "*", filterWhereString, "TrainUnitName,PostID,CERTIFICATECODE");
        }
//        public static DataTable GetExamCertificateTrainUnitList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
//        {
//            string view = @"(SELECT ""CERTIFICATEID"",""EXAMPLANID"",""WORKERID"",""CERTIFICATETYPE"",""POSTTYPEID"",""POSTID"",""CERTIFICATECODE"",""WORKERNAME"",""SEX"",""BIRTHDAY"",""UNITNAME"",""CONFERDATE"",""VALIDSTARTDATE"",""VALIDENDDATE"",""CONFERUNIT"",[STATUS],""CHECKMAN"",""CHECKADVISE"",""CHECKDATE"",""PRINTMAN"",""PRINTDATE"",""CREATEPERSONID"",""CREATETIME"",""WORKERCERTIFICATECODE"",""UNITCODE""
//                            FROM ""DBO"".""CERTIFICATE"" 
//                            where ""EXAMPLANID"" in(select EXAMPLANID from dbo.ExamPlan where 1=1 {0}) and Status = '首次' ) as c
//                            left join dbo.ExamSignup as s on ((c.EXAMPLANID = s.EXAMPLANID) AND (c.WORKERID = s.WORKERID))
//                            left join dbo.""USER"" as u on s.TrainUnitID = u.UserID
//                            left join dbo.PostInfo as n1 on n1.PostType='1' and c.PostTypeID = n1.PostID
//                            left join dbo.PostInfo as n2 on n2.PostType='2' and c.PostID = n2.PostID";
//            return CommonDAL.GetDataTable(startRowIndex, maximumRows, string.Format(view, filterWhereString), " n1.PostName as PostTypeName,n2.PostName, c.*,u.RelUserName as TrainUnitName", "", "TrainUnitName,PostTypeID,PostID,CERTIFICATECODE");
//        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount_ExamCertificateTrainUnit(string filterWhereString)
        {
//            string view = @"(SELECT ""CERTIFICATEID"",""EXAMPLANID"",""WORKERID"",""CERTIFICATETYPE"",""POSTTYPEID"",""POSTID"",""CERTIFICATECODE"",""WORKERNAME"",""SEX"",""BIRTHDAY"",""UNITNAME"",""CONFERDATE"",""VALIDSTARTDATE"",""VALIDENDDATE"",""CONFERUNIT"",[STATUS],""CHECKMAN"",""CHECKADVISE"",""CHECKDATE"",""PRINTMAN"",""PRINTDATE"",""CREATEPERSONID"",""CREATETIME"",""WORKERCERTIFICATECODE"",""UNITCODE""
//                            FROM ""DBO"".""CERTIFICATE"" 
//                            where ""EXAMPLANID"" in(select EXAMPLANID from dbo.ExamPlan where 1=1 {0})   and Status = '首次') as c
//                            left join dbo.ExamSignup as s on ((c.EXAMPLANID = s.EXAMPLANID) AND (c.WORKERID = s.WORKERID))
//                            left join dbo.""USER"" as u on s.TrainUnitID = u.UserID
//                            left join dbo.PostInfo as n1 on n1.PostType='1' and c.PostTypeID = n1.PostID
//                            left join dbo.PostInfo as n2 on n2.PostType='2' and c.PostID = n2.PostID";
//            return CommonDAL.SelectRowCount(string.Format(view, filterWhereString), "");
            return CommonDAL.SelectRowCount("dbo.certificate", filterWhereString);
        }

        /// <summary>
        /// 获取查询结果集中考试计划ID
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>只有一个考试计划返回ID，没有查到返回0，多个考试计划返回-1</returns>
        public static long GetExamPlanID(string filterWhereString)
        {
            //DataTable dt = CommonDAL.GetDataTable(0, 2, "dbo.View_ExamResult", "distinct ExamPlanID", filterWhereString, "ExamPlanID");
            DataTable dt = CommonDAL.GetDataTable(string.Format("select distinct ExamPlanID from dbo.View_ExamResult where 1=1 {0}",  filterWhereString));
            if (dt == null || dt.Rows.Count == 0) return 0;
            if (dt.Rows.Count > 1) return -1;
            return Convert.ToInt64(dt.Rows[0]["ExamPlanID"]);
        }

        /// <summary>
        ///  获取考试结果
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListView_ExamScore(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSCORE", "*", filterWhereString, orderBy == "" ? "ExamSignUpID" : orderBy);
        }

        ////获取考试结果，合格的带证件号码
        //public static DataTable GetListView_ExamScore_CE(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        //{
        //    return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSCORE_CE", "*", filterWhereString, orderBy == "" ? "ExamSignUpID" : orderBy);
        //}
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView_ExamScore(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSCORE", filterWhereString);
        }

        /// <summary>
        ///  获取考试结果
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListView_ExamScore_New(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSCORE_NEW", "*", filterWhereString, orderBy == "" ? "ExamSignUpID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView_ExamScore_New(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSCORE_NEW", filterWhereString);
        }

        ////统计查询结果记录数
        //public static int SelectCountView_ExamScore_CE(string filterWhereString)
        //{
        //    return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSCORE_CE", filterWhereString);
        //}

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return SelectCountView(null, filterWhereString);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(DbTransaction tran, string filterWhereString)
        {
            return CommonDAL.SelectRowCount(tran,"dbo.View_ExamResult", filterWhereString);
        }
        public static int SelectCountView_ExamResult(string filterWhereString)
        {
            return SelectCountView_ExamResult(null, filterWhereString);
        }

        public static int SelectCountView_ExamResult(DbTransaction tran, string filterWhereString)
        {
            return CommonDAL.SelectRowCount(tran,"DBO.VIEW_CERTIFICATE_BEFORE", filterWhereString);
        }

        /// <summary>
        /// 不分科目统计考试通过人数(按培训点Group by)
        /// </summary>
        /// <param name="examPlanID">考试计划ID</param>
        /// <param name="passLine">合格分数参考线</param>
        /// <param name="SubjectCount">考试科目</param>
        /// <returns>统计列表（列：培训点，通过人数 TrainUnitID,ExamerCount）</returns>
        public static DataTable GetPassCountListAllSubject(string examPlanID, string passLine, string SubjectCount)
        {
            string sql = @"select TRAINUNITID,count(*) as ExamerCount from
(SELECT TRAINUNITID,WorkerID FROM DBO.EXAMSIGNUP where ExamPlanID >={0} and ExamPlanID <={0}) as t1
inner join
(SELECT e.WorkerID FROM dbo.ExamSubjectResult  as r
    inner join dbo.ExamPlanSubject as s on r.ExamPlanID = s.ExamPlanID and r.PostID = s.PostID 
    inner join dbo.ExamResult as e on r.ExamPlanID=e.ExamPlanID and r.ExamCardID = e.ExamCardID
    where r.SumScore >={1} and r.ExamPlanID >={0} and r.ExamPlanID <={0}
    group by e.WorkerID having count(*) ={2}
) as t2
on t1.WorkerID=t2.WorkerID group by TRAINUNITID";
            DBHelper db = new DBHelper();
            return db.GetFillData(string.Format(sql, examPlanID, passLine, SubjectCount));
        }

        /// <summary>
        /// 统计考试科目通过人数(按培训点Group by)
        /// </summary>
        /// <param name="examPlanID">考试计划ID</param>
        /// <param name="passLine">合格分数参考线</param>
        /// <returns>统计列表（列：培训点，科目，通过人数 TrainUnitID,PostID,ExamerCount）</returns>
        public static DataTable GetPassCountList(string examPlanID, string passLine)
        {
            string view = @"select s.TrainUnitID,sr.PostID,count(*) as ExamerCount 
                            from dbo.ExamSubjectResult as sr
                            inner join dbo.ExamResult r on sr.ExamPlanID = r.ExamPlanID and sr.ExamCardID = r.ExamCardID
                            inner join dbo.ExamSignUp as s on r.ExamPlanID = s.ExamPlanID and r.WorkerID=s.WorkerID
                            where  isnull(sr.SumScore,0) >={1} and sr.ExamPlanID >= {0} and sr.ExamPlanID <= {0} 
                            group by s.TrainUnitID,sr.PostID
                            order by s.TrainUnitID";
            //string filterWhereString = string.Format(" and isnull(sr.SumScore,0) >={1} and sr.ExamPlanID >= {0} and sr.ExamPlanID <= {0} group by s.TrainUnitID,sr.PostID", examPlanID, passLine);
            return CommonDAL.GetDataTable(string.Format(view, examPlanID, passLine));
        }

        /// <summary>
        /// 统计考试科目通过人数（all）
        /// </summary>
        /// <param name="examPlanID">考试计划ID</param>
        /// <param name="passLine">合格分数参考线</param>
        /// <returns>统计列表（列：培训点=0，科目，通过人数 TrainUnitID,PostID,ExamerCount）</returns>
        public static DataTable GetPassCountListAll(string examPlanID, string passLine)
        {
            string view = @"select 0 as TrainUnitID,sr.PostID,count(*) as ExamerCount 
                            from dbo.ExamSubjectResult as sr
                            inner join dbo.ExamResult r on sr.ExamPlanID = r.ExamPlanID and sr.ExamCardID = r.ExamCardID
                            inner join dbo.ExamSignUp as s on r.ExamPlanID = s.ExamPlanID and r.WorkerID=s.WorkerID
                            where  isnull(sr.SumScore,0) >={1} and sr.ExamPlanID >= {0} and sr.ExamPlanID <= {0} 
                            group by sr.PostID
                            order by sr.PostID";
            //string filterWhereString = string.Format(" and isnull(sr.SumScore,0) >={1} and sr.ExamPlanID >= {0} and sr.ExamPlanID <= {0} group by sr.PostID", examPlanID, passLine);
            return CommonDAL.GetDataTable(string.Format(view, examPlanID, passLine));
        }

        /// <summary>
        /// 不分科目统计考试科目通过人数（all）
        /// </summary>
        /// <param name="examPlanID">考试计划ID</param>
        /// <param name="passLine">合格分数参考线</param>
        /// <param name="SubjectCount">考试科目</param>
        /// <returns>统计列表（列：培训点=0，科目，通过人数 TrainUnitID,PostID,ExamerCount）</returns>
        public static int GetPassCountListAllSubjectSum(string examPlanID, string passLine, string SubjectCount)
        {
            string sql = @"select isnull(count(*),0) as ExamerCount from
(SELECT TRAINUNITID,WorkerID FROM DBO.EXAMSIGNUP where ExamPlanID >={0} and ExamPlanID <={0}) as t1
inner join
(SELECT e.WorkerID FROM dbo.ExamSubjectResult  as r
    inner join dbo.ExamPlanSubject as s on r.ExamPlanID = s.ExamPlanID and r.PostID = s.PostID 
    inner join dbo.ExamResult as e on r.ExamPlanID=e.ExamPlanID and r.ExamCardID = e.ExamCardID
    where r.SumScore >={1} and r.ExamPlanID >={0} and r.ExamPlanID <={0}
    group by e.WorkerID having count(*) ={2}
) as t2
on t1.WorkerID=t2.WorkerID ";
            DBHelper db = new DBHelper();
            object rtn= db.ExecuteScalar(string.Format(sql, examPlanID, passLine, SubjectCount));
            return rtn == null ? 0 : Convert.ToInt32(rtn);
        }

        /// <summary>
        /// 更新考试综合成绩（合格/不合格）
        /// </summary>
        /// <param name="tran">事务对象</param>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="SubjectCount">考试科目个数</param>
        /// <returns></returns>
        public static int UpdateExamResult(DbTransaction tran, Int64 ExamPlanID, int SubjectCount)
        {
            string sql = string.Format(@"UPDATE dbo.ExamResult 
            SET [STATUS]='{0}',ExamResult = null           
            WHERE ExamPlanID >=@ExamPlanID and ExamPlanID <=@ExamPlanID;
            UPDATE dbo.ExamResult 
            SET [STATUS]='{1}',ExamResult = '合格'           
            WHERE ExamPlanID >=@ExamPlanID and ExamPlanID <=@ExamPlanID and ExamCardID in(
            SELECT r.ExamCardID
            FROM dbo.ExamSubjectResult  as r
            inner join dbo.ExamPlanSubject as s
            on r.ExamPlanID = s.ExamPlanID and r.PostID = s.PostID 
            inner join dbo.ExamResult as e 
            on r.ExamPlanID=e.ExamPlanID and r.ExamCardID = e.ExamCardID
            where r.SumScore >=s.PassLine and r.ExamPlanID >=@ExamPlanID and r.ExamPlanID <=@ExamPlanID
            group by r.ExamCardID having count(*) =@SubjectCount
            )", EnumManager.ExamResultStatus.BeforeResult,EnumManager.ExamResultStatus.UnPublished);

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SubjectCount", DbType.Int32, SubjectCount));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 获取考试通过率（临时）
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="SubjectCount">科目数量</param>
        /// <param name="PassLineSql">科目合格线临时表sql</param>
        /// <returns>通过率</returns>
        public static string SelectTempPassPercent(Int64 ExamPlanID, int SubjectCount, string PassLineSql)
        {
            string sql = string.Format(@"select 
                cast(round((select cast(count(*) as numeric)
                from(SELECT  count(*) as PassCount
                FROM dbo.ExamSubjectResult  as r
                inner join ({0}) as s
                on r.PostID = s.PostID 
                where r.SumScore >=s.PassLine and r.ExamPlanID >=@ExamPlanID and r.ExamPlanID <=@ExamPlanID
                group by r.ExamCardID
                ) t 
                where PassCount >=@SubjectCount)
                /
                (select count(*)
                from dbo.ExamResult 
                where ExamPlanID >=@ExamPlanID and ExamPlanID <=@ExamPlanID) *100,2) as numeric(18,2))", PassLineSql);

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SubjectCount", DbType.Int32, SubjectCount));
            object rtn = db.ExecuteScalar(sql, p.ToArray());
            if (rtn == null)
                return "0%";
            else
                return rtn.ToString() + "%";
        }

        /// <summary>
        /// 获取考试通人数（临时）
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="SubjectCount">科目数量</param>
        /// <param name="PassLineSql">科目合格线临时表sql</param>
        /// <returns>通过人数</returns>
        public static string SelectTempPassCount(Int64 ExamPlanID, int SubjectCount, string PassLineSql)
        {
            string sql = string.Format(@"select cast(count(*) as numeric)
                from(SELECT  count(*) as PassCount
                FROM dbo.ExamSubjectResult  as r
                inner join ({0}) as s
                on r.PostID = s.PostID 
                where r.SumScore >=s.PassLine and r.ExamPlanID >=@ExamPlanID and r.ExamPlanID <=@ExamPlanID
                group by r.ExamCardID
                ) t 
                where PassCount >= @SubjectCount", PassLineSql);

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SubjectCount", DbType.Int32, SubjectCount));
            object rtn = db.ExecuteScalar(sql, p.ToArray());
            if (rtn == null)
                return "0（人）";
            else
                return rtn.ToString() + "（人）";
        }

        /// <summary>
        /// 获取考试通过率
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="SubjectCount">科目数量</param>
        /// <returns>通过率</returns>
        public static string SelectPassPercent(Int64 ExamPlanID, int SubjectCount)
        {
            string sql = @"select 
                cast(round((select cast(count(*) as numeric)
                from(SELECT  count(*) as PassCount
                FROM dbo.ExamSubjectResult  as r
                inner join dbo.ExamPlanSubject as s
                on r.ExamPlanID = s.ExamPlanID and r.PostID = s.PostID 
                where r.SumScore >=s.PassLine and r.ExamPlanID >=@ExamPlanID and r.ExamPlanID <=@ExamPlanID
                group by r.ExamCardID
                ) t 
                where PassCount >=@SubjectCount)
                /
                (select count(*)
                from dbo.ExamResult 
                where ExamPlanID >=@ExamPlanID and ExamPlanID <=@ExamPlanID) *100,2) as numeric(18,2))";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SubjectCount", DbType.Int32, SubjectCount));
            object rtn = db.ExecuteScalar(sql, p.ToArray());
            if (rtn == null)
                return "0%";
            else
                return rtn.ToString() + "%";
        }

        /// <summary>
        /// 获取考试通人数
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="SubjectCount">科目数量</param>
        /// <returns>通过人数</returns>
        public static string SelectPassCount(Int64 ExamPlanID, int SubjectCount)
        {
            string sql = @"select cast(count(*) as numeric)
                from(SELECT  count(*) as PassCount
                FROM dbo.ExamSubjectResult  as r
                inner join dbo.ExamPlanSubject as s
                on r.ExamPlanID = s.ExamPlanID and r.PostID = s.PostID 
                where r.SumScore >=s.PassLine and r.ExamPlanID >=@ExamPlanID and r.ExamPlanID <=@ExamPlanID
                group by r.ExamCardID
                ) t 
                where PassCount >= @SubjectCount";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("SubjectCount", DbType.Int32, SubjectCount));
            object rtn = db.ExecuteScalar(sql, p.ToArray());
            if (rtn == null)
                return "0（人）";
            else
                return rtn.ToString() + "（人）";
        }

        /// <summary>
        /// 成绩公告
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="ExamResultStatus">成绩状态</param>
        /// <param name="PublishTime">公告时间</param>
        /// <returns></returns>
        public static int Publish(long ExamPlanID, string ExamResultStatus, DateTime PublishTime)
        {
            string sql = @"
			UPDATE dbo.ExamResult
				SET	ExamResult = case ExamResult when '合格' then '合格' else '不合格' end,Status = @Status,ModifyTime = @ModifyTime
			WHERE
				ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64,ExamPlanID));
            p.Add(db.CreateParameter("Status", DbType.String, ExamResultStatus));    
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, PublishTime));
            return db.ExcuteNonQuery(sql, p.ToArray());
               
        }


         /// <summary>
        /// 检查考生1年内最近一次考试（三类人、八大员）是否缺考或0分
        /// </summary>
        /// <param name="Workercertificatecode">身份证号</param>
        /// <returns>是否缺考</returns>
        public static bool CheckMissExamByWorkercertificatecode(DbTransaction tran, string Workercertificatecode)
        {
            string sql = @"select top 1  t.SCORE
                            from EXAMPLAN e 
                            inner join 
                            (
                                SELECT s.[CERTIFICATECODE] ,t2.[EXAMPLANID],isnull(sum(t2.[SUMSCORE]),0) as SCORE
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join [dbo].[EXAMRESULT] t1
                                on t1.EXAMSIGNUP_ID = s.EXAMSIGNUPID 
                                inner join  [dbo].[EXAMSUBJECTRESULT] t2
                                on t1.[EXAMPLANID]=t2.[EXAMPLANID]
                                and t1.[EXAMCARDID]=t2.[EXAMCARDID]
                                where s.CERTIFICATECODE=@CERTIFICATECODE 
                                group by t2.[EXAMPLANID],s.[CERTIFICATECODE]
                             ) t
                             on e.EXAMPLANID = t.EXAMPLANID
                             where (e.POSTTYPEID =1 or e.POSTTYPEID =5 ) and e.EXAMSTARTDATE > dateadd(year,-1,getdate())
                             order by e.EXAMSTARTDATE desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CERTIFICATECODE", DbType.String, Workercertificatecode));
            object rtn = db.ExecuteScalar(tran,sql, p.ToArray());
            if (rtn == null)
                return false;
            else if (Convert.ToInt32(rtn) > 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 检查考生1年内考试（三类人、八大员）累计缺考或0分是否大于等于3次
        /// </summary>
        /// <param name="Workercertificatecode">身份证号</param>
        /// <returns>是否缺考超过3次（含3次）</returns>
        public static bool CheckMissExamCountLimitByWorkercertificatecode(DbTransaction tran, string Workercertificatecode)
        {
            string sql = @"select count(1) sl
                            from EXAMPLAN e 
                            inner join 
                            (
                                SELECT  s.[CERTIFICATECODE] ,t2.[EXAMPLANID],isnull(sum(t2.[SUMSCORE]),0) as 'SCORE'
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join [dbo].[EXAMRESULT] t1
                                on t1.EXAMSIGNUP_ID = s.EXAMSIGNUPID 
                                inner join  [dbo].[EXAMSUBJECTRESULT] t2
                                on t1.[EXAMPLANID]=t2.[EXAMPLANID]
                                and t1.[EXAMCARDID]=t2.[EXAMCARDID]
                                where s.CERTIFICATECODE=@CERTIFICATECODE 
                                group by t2.[EXAMPLANID],s.[CERTIFICATECODE]
                              ) t
                              on e.EXAMPLANID = t.EXAMPLANID
                              where (e.POSTTYPEID =1 or e.POSTTYPEID =5 ) and e.EXAMSTARTDATE > dateadd(year,-1,getdate()) and t.SCORE=0";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CERTIFICATECODE", DbType.String, Workercertificatecode));
            object rtn = db.ExecuteScalar(tran,sql, p.ToArray());
            if (rtn == null)
                return false;
            else if (Convert.ToInt32(rtn) < 3)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据身份证号获取三类人、专业人员考试近1年缺考考试计划信息
        /// </summary>
        /// <param name="topCount">数据条数：取最近考试的前几条记录</param>
        /// <param name="Workercertificatecode">身份证号</param>
        /// <returns></returns>
        public static DataTable GetMissExamList(int topCount, string Workercertificatecode)
        {
            string sql = @"select top {0} e.*
                            from EXAMPLAN e 
                            inner join 
                            (
                                SELECT s.[CERTIFICATECODE] ,t2.[EXAMPLANID],isnull(sum(t2.[SUMSCORE]),0) as SCORE
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join [dbo].[EXAMRESULT] t1
                                on t1.EXAMSIGNUP_ID = s.EXAMSIGNUPID 
                                inner join  [dbo].[EXAMSUBJECTRESULT] t2
                                on t1.[EXAMPLANID]=t2.[EXAMPLANID]
                                and t1.[EXAMCARDID]=t2.[EXAMCARDID]
                                where s.CERTIFICATECODE='{1}' 
                                group by t2.[EXAMPLANID],s.[CERTIFICATECODE]
                            ) t
                            on e.EXAMPLANID = t.EXAMPLANID
                            where e.EXAMSTARTDATE > dateadd(year,-1,getdate()) and (e.POSTTYPEID =1 or e.POSTTYPEID =5 ) and t.SCORE=0
                            order by e.EXAMSTARTDATE desc";

            return CommonDAL.GetDataTable(string.Format(sql, topCount, Workercertificatecode));
        }


        /// <summary>
        ///  获取特种作业理论考试结果
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListTZZYScore(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.[VIEW_EXAMSCORE_TZZY]", "*", filterWhereString, orderBy == "" ? "ExamPlanID desc,EXAMCARDID" : orderBy);
        }
   
        /// <summary>
        /// 统计查询特种作业理论考试结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountTZZYScore(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.[VIEW_EXAMSCORE_TZZY]", filterWhereString);
        }
    }
}
