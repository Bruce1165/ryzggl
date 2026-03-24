using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--PostInfoDAL(填写类描述)
	/// </summary>
    public class PostInfoDAL
    {
        public PostInfoDAL(){}

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_PostInfoOB">对象实体类</param>
        /// <returns></returns>
        public static int Insert(PostInfoOB _PostInfoOB)
		{
		    return Insert(null,_PostInfoOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,PostInfoOB _PostInfoOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.PostInfo(PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat)
			VALUES (:PostID,:PostType,:PostName,:UpPostID,:ExamFee)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32, _PostInfoOB.PostID));
			p.Add(db.CreateParameter("PostType",DbType.String, _PostInfoOB.PostType));
			p.Add(db.CreateParameter("PostName",DbType.String, _PostInfoOB.PostName));
			p.Add(db.CreateParameter("UpPostID",DbType.Int32, _PostInfoOB.UpPostID));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _PostInfoOB.ExamFee));
            p.Add(db.CreateParameter("CurrentNumber", DbType.Int64, _PostInfoOB.CurrentNumber));
            p.Add(db.CreateParameter("CodeYear", DbType.Int32, _PostInfoOB.CodeYear));
            p.Add(db.CreateParameter("CodeFormat", DbType.String, _PostInfoOB.CodeFormat));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_PostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(PostInfoOB _PostInfoOB)
		{
			return Update(null,_PostInfoOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,PostInfoOB _PostInfoOB)
		{
			string sql = @"
			UPDATE dbo.PostInfo
				SET	PostType =@PostType,PostName =@PostName,UpPostID =@UpPostID,ExamFee =@ExamFee,CurrentNumber =@CurrentNumber,CodeYear =@CodeYear,CodeFormat=@CodeFormat
			WHERE
				PostID =@PostID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32, _PostInfoOB.PostID));
			p.Add(db.CreateParameter("PostType",DbType.String, _PostInfoOB.PostType));
			p.Add(db.CreateParameter("PostName",DbType.String, _PostInfoOB.PostName));
			p.Add(db.CreateParameter("UpPostID",DbType.Int32, _PostInfoOB.UpPostID));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _PostInfoOB.ExamFee));

            p.Add(db.CreateParameter("CurrentNumber", DbType.Int64, _PostInfoOB.CurrentNumber));
            p.Add(db.CreateParameter("CodeYear", DbType.Int32, _PostInfoOB.CodeYear));
            p.Add(db.CreateParameter("CodeFormat", DbType.String, _PostInfoOB.CodeFormat));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="PostInfoID">主键</param>
		/// <returns></returns>
        public static int Delete( int PostID )
		{
			return Delete(null, PostID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="PostInfoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int PostID)
		{
            string sql = @"DELETE FROM dbo.PostInfo WHERE PostID =@PostID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32,PostID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="PostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(PostInfoOB _PostInfoOB)
		{
			return Delete(null,_PostInfoOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="PostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,PostInfoOB _PostInfoOB)
		{
            string sql = @"DELETE FROM dbo.PostInfo WHERE PostID =@PostID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32,_PostInfoOB.PostID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PostInfoID">主键</param>
        public static PostInfoOB GetObject(int PostID)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.PostInfo
			WHERE PostID =@PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    PostInfoOB _PostInfoOB = null;
                    if (reader.Read())
                    {
                        _PostInfoOB = new PostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _PostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _PostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _PostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _PostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _PostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);

                        if (reader["CurrentNumber"] != DBNull.Value) _PostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _PostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _PostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);
                    }
                    reader.Close();
                    db.Close();
                    return _PostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PostInfoID">主键</param>
        public static PostInfoOB GetObject(DbTransaction tran, int PostID)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.PostInfo
			WHERE PostID =@PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    PostInfoOB _PostInfoOB = null;
                    if (reader.Read())
                    {
                        _PostInfoOB = new PostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _PostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _PostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _PostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _PostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _PostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);

                        if (reader["CurrentNumber"] != DBNull.Value) _PostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _PostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _PostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return _PostInfoOB;
                }
            }
            catch (Exception ex)
            {
                if (tran == null) db.Close();
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
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.PostInfo", "*", filterWhereString, orderBy == "" ? " PostName" : orderBy);
        }

        /// <summary>
        /// 根据岗位类型ID获取考试科目名称列表
        /// </summary>
        /// <param name="PostID">岗位类型ID</param>
        /// <returns>考试科目名称列表</returns>
        public static DataTable GetListByPostID(string PostID)
        {
            //按科目ID排序
            return CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.PostInfo", "PostName,0 as SubjectID,0 as ExamPlanSubjectID,null as ExamStartTime,null as ExamEndTime,0 as ExamFee", string.Format(" and UPPOSTID in (SELECT POSTID FROM DBO.POSTINFO where UPPOSTID ={0}) group by PostName", PostID), " min(POSTID)");
        }

        /// <summary>
        /// 根据岗位类别获取第一个岗位工种对象
        /// </summary>
        /// <param name="UpPostID">岗位类别ID</param>
        /// <returns>岗位工种对象</returns>
        public static PostInfoOB GetFirtPostByPostTypeID(int UpPostID)
        {
            string sql = @"
			SELECT top(1) PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.PostInfo
			WHERE UpPostID =@UpPostID ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UpPostID", DbType.Int32, UpPostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    PostInfoOB _PostInfoOB = null;
                    if (reader.Read())
                    {
                        _PostInfoOB = new PostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _PostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _PostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _PostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _PostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _PostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["CurrentNumber"] != DBNull.Value) _PostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _PostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _PostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);

                    }
                    reader.Close();
                    db.Close();
                    return _PostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        } 

		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.PostInfo", filterWhereString);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PostInfoID">主键</param>
        public static PostInfoOB GetObject(int UpPostID, string PostName)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.PostInfo
			WHERE UpPostID =@UpPostID and PostName=@PostName";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UpPostID", DbType.Int32, UpPostID));
            p.Add(db.CreateParameter("PostName", DbType.String, PostName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    PostInfoOB _PostInfoOB = null;
                    if (reader.Read())
                    {
                        _PostInfoOB = new PostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _PostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _PostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _PostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _PostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _PostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["CurrentNumber"] != DBNull.Value) _PostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _PostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _PostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);

                    }
                    reader.Close();
                    db.Close();
                    return _PostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PostInfoID">主键</param>
        public static List<PostInfoOB> GetObject()
        {
            string sql = @"
			SELECT distinct PostName
			FROM dbo.PostInfo
			WHERE POSTTYPE = 3";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            List<PostInfoOB> dt = new List<PostInfoOB>();
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    PostInfoOB _PostInfoOB = null;
                    while (reader.Read())
                    {
                        _PostInfoOB = new PostInfoOB();

                        //if (reader["PostType"] != DBNull.Value) _PostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _PostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        //if (reader["UpPostID"] != DBNull.Value) _PostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        //if (reader["ExamFee"] != DBNull.Value) _PostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        dt.Add(_PostInfoOB);
                    }
                    reader.Close();
                    db.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 获取下一个证书编号（不更新当前）
        /// </summary>
        /// <param name="_PostInfoOB">工种OB</param>
        /// <param name="tran">事务</param>
        /// <returns>证书编号</returns>
        public static string GetNextCertificateNoUnUpdate(ref PostInfoOB _PostInfoOB)
        {
            //证书编号公式格式说明：
            //规则1、例如“京|Y,2|N,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“N,7”表示7位流水号
            //规则2、例如“京|Y,2|YN,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“YN,7”表示本年度7位流水号
            //规则3、如果公式为：Parent，表示调用上级岗位公式
            //规则4、如果公式为：Brather101，表示调用同级别兄弟ID为101的岗位公式

            if (_PostInfoOB.CodeFormat == "Parent")//使用父亲公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(_PostInfoOB.UpPostID.Value);
            }
            else if (_PostInfoOB.CodeFormat.Contains("Brather") == true)//使用兄弟公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(Convert.ToInt32(_PostInfoOB.CodeFormat.Replace("Brather", "")));
            }
            long _CurrentNumber = _PostInfoOB.CurrentNumber.HasValue ? _PostInfoOB.CurrentNumber.Value : 0;
            int year = _PostInfoOB.CodeYear.HasValue ? _PostInfoOB.CodeYear.Value : DateTime.Now.Year;//编号年度，暂时无用
            string[] _CertCodeFormat = _PostInfoOB.CodeFormat.Split('|');//格式配置
            string[] replaceValue = new string[_CertCodeFormat.Length];//替换值集合
            StringBuilder _format = new StringBuilder();//格式化字符串
            for (int i = 0; i < _CertCodeFormat.Length; i++)
            {
                _format.Append("{").Append(i.ToString()).Append("}");
                string[] temp = _CertCodeFormat[i].Split(',');
                if (temp.Length == 1)//原样拷贝
                {
                    replaceValue[i] = temp[0];
                }
                else
                {
                    switch (temp[0])
                    {
                        case "YPN"://本年度岗位类别自增数:取temp[1]位
                            if (year == DateTime.Now.Year)//是本年度
                            {
                                replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            else
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            //更新当前流水号(岗位类别统一流水号)
                    
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            _PostInfoOB.CodeYear = DateTime.Now.Year;
                            break;
                        case "YN"://本年度自增数:取temp[1]位
                            if (year == DateTime.Now.Year)//是本年度
                            {
                                replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            else
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            _PostInfoOB.CodeYear = DateTime.Now.Year;
                            break;
                        case "Y"://年份:取temp[1]位
                            replaceValue[i] = DateTime.Now.ToString("".PadLeft(Convert.ToInt32(temp[1]), 'y'));
                            break;
                        case "N"://自增数:取temp[1]位
                            replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                           
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            break;
                    }
                }
            }

            return string.Format(_format.ToString(), replaceValue);
        }

        /// <summary>
        /// 获取下一个证书编号
        /// </summary>
        /// <param name="_PostInfoOB">工种OB</param>
        /// <param name="tran">事务</param>
        /// <returns>证书编号</returns>
        public static string GetNextCertificateNo(ref PostInfoOB _PostInfoOB, DbTransaction tran)
        {
            //证书编号公式格式说明：
            //规则1、例如“京|Y,2|N,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“N,7”表示7位流水号
            //规则2、例如“京|Y,2|YN,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“YN,7”表示本年度7位流水号
            //规则3、如果公式为：Parent，表示调用上级岗位公式
            //规则4、如果公式为：Brather101，表示调用同级别兄弟ID为101的岗位公式

            if (_PostInfoOB.CodeFormat == "Parent")//使用父亲公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(tran,_PostInfoOB.UpPostID.Value);
            }
            else if (_PostInfoOB.CodeFormat.Contains("Brather") == true)//使用兄弟公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(tran,Convert.ToInt32(_PostInfoOB.CodeFormat.Replace("Brather", "")));
            }
            long _CurrentNumber = _PostInfoOB.CurrentNumber.HasValue ? _PostInfoOB.CurrentNumber.Value : 0;
            int year = _PostInfoOB.CodeYear.HasValue ? _PostInfoOB.CodeYear.Value : DateTime.Now.Year;//编号年度，暂时无用
            string[] _CertCodeFormat = _PostInfoOB.CodeFormat.Split('|');//格式配置
            string[] replaceValue =new string[_CertCodeFormat.Length];//替换值集合
            StringBuilder  _format = new StringBuilder();//格式化字符串
            for (int i = 0; i < _CertCodeFormat.Length;i++ )
            {
                _format.Append("{").Append(i.ToString()).Append("}");
                string[] temp = _CertCodeFormat[i].Split(',');
                if (temp.Length == 1)//原样拷贝
                {
                    replaceValue[i] = temp[0];
                }
                else
                {
                    switch (temp[0])
                    {
                        case "YPN"://本年度岗位类别自增数:取temp[1]位
                            if (year == DateTime.Now.Year)//是本年度
                            {
                                replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            else
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            //更新当前流水号(岗位类别统一流水号)
                            DBHelper db2 = new DBHelper();
                            db2.ExecuteScalar(tran, string.Format("UPDATE dbo.PostInfo SET CurrentNumber={0},CodeYear={2} WHERE UpPostID = {1}", replaceValue[i], _PostInfoOB.UpPostID, DateTime.Now.Year));
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            _PostInfoOB.CodeYear = DateTime.Now.Year;
                            break;
                        case "YN"://本年度本工种自增数:取temp[1]位
                            if (year == DateTime.Now.Year)//是本年度
                            {
                                replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            else
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            //更新当前流水号
                            DBHelper db1 = new DBHelper();
                            db1.ExecuteScalar(tran, string.Format("UPDATE dbo.PostInfo SET CurrentNumber={0},CodeYear={2} WHERE PostID = {1}", replaceValue[i], _PostInfoOB.PostID, DateTime.Now.Year));
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            _PostInfoOB.CodeYear = DateTime.Now.Year;
                            break;
                        case "Y"://年份:取temp[1]位
                             replaceValue[i] = DateTime.Now.ToString("".PadLeft(Convert.ToInt32(temp[1]),'y'));
                            break;
                        case "N"://自增数:取temp[1]位
                            replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            //更新当前流水号
                            DBHelper db = new DBHelper();
                            db.ExecuteScalar(tran, string.Format("UPDATE dbo.PostInfo SET CurrentNumber={0},CodeYear={2} WHERE PostID = {1}", replaceValue[i], _PostInfoOB.PostID, DateTime.Now.Year));
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            break;
                    }
                }
            }

            return string.Format(_format.ToString(),replaceValue);
        }

        /// <summary>
        /// 获取技能等级编码
        /// </summary>
        /// <param name="SkillLevelName">技能等级名称</param>
        /// <returns></returns>
        private static string GetSkillLevelCode(string SkillLevelName)
        {
            switch (SkillLevelName)
            {
                case "普工":
                    return "0";
                    case "初级工":
                    return "5";
                    case "中级工":
                    return "4";
                    case "高级工":
                    return "3";
                    case "技师":
                    return "2";
                    case "高级技师":
                    return "1";
                default:
                    throw new Exception("技能等级不在编码列表中，无法编号！");

            }
        }
      

        /// <summary>
        /// 获取下一个证书编号（职业技能）
        /// </summary>
        /// <param name="_PostInfoOB">工种OB</param>
        /// <param name="TrainUnitCdoe">培训机构编号</param>
        /// <param name="SkillLevel">技术等级名称</param>
        /// <param name="tran">事务</param>
        /// <returns>证书编号</returns>
        public static string GetNextCertificateNo(ref PostInfoOB _PostInfoOB,string TrainUnitCdoe,string SkillLevel, DbTransaction tran)
        {
            //证书编号公式格式说明：
            //规则1、例如“京|Y,2|N,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“N,7”表示7位流水号
            //规则2、例如“京|Y,2|YN,7”，其中“京”为字符串常量，“Y,2”表示两位年度，“YN,7”表示本年度7位流水号
            //规则3、如果公式为：Parent，表示调用上级岗位公式
            //规则4、如果公式为：Brather101，表示调用同级别兄弟ID为101的岗位公式
            //规则5、如果公式为：TrainCode|Level|000|UYN,4'，表示培训机构编码
            //规则6、如果公式为：Level，表示技能等级0，5，4，3，2，1分别表示普工，初级工，中级工，高级工，技师，高级技师
            //规则7、如果公式为：TLPYN,4'，表示按培训点、技能等级、岗位工种年度4位流水号。
            //规则8、如果公式为：TrainCodePYN,4，表示按培训点、岗位类别年度4位流水号。
                          
            if (_PostInfoOB.PostID.Value == 158 //村镇建筑工匠
                || _PostInfoOB.PostID.Value == 199 //普工
                )
            {
                SkillLevel = "普工";//村镇建筑工匠 和  普工 两个岗位技术等级虚拟为“普工”
            }

            if (_PostInfoOB.CodeFormat == "Parent")//使用父亲公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(tran, _PostInfoOB.UpPostID.Value);
            }
            else if (_PostInfoOB.CodeFormat.Contains("Brather") == true)//使用兄弟公式
            {
                _PostInfoOB = PostInfoDAL.GetObject(tran, Convert.ToInt32(_PostInfoOB.CodeFormat.Replace("Brather", "")));
            }
            long _CurrentNumber = _PostInfoOB.CurrentNumber.HasValue ? _PostInfoOB.CurrentNumber.Value : 0;
            int year = _PostInfoOB.CodeYear.HasValue ? _PostInfoOB.CodeYear.Value : DateTime.Now.Year;//编号年度，暂时无用
            string[] _CertCodeFormat = _PostInfoOB.CodeFormat.Split('|');//格式配置
            string[] replaceValue = new string[_CertCodeFormat.Length];//替换值集合
            
            StringBuilder _format = new StringBuilder();//格式化字符串
            for (int i = 0; i < _CertCodeFormat.Length; i++)
            {
                _format.Append("{").Append(i.ToString()).Append("}");
                string[] temp = _CertCodeFormat[i].Split(',');
                if (temp.Length == 1)
                {
                    switch (temp[0])
                    {
                        case "TrainCode"://培训点编号
                            replaceValue[i] = TrainUnitCdoe;
                            break;
                        case "Level"://技能等级
                            replaceValue[i] = GetSkillLevelCode(SkillLevel);
                            break;
                        default://原样拷贝
                            replaceValue[i] = temp[0];
                            break;
                    }
                }
                else
                {
                    switch (temp[0])
                    {
                        case "YN"://本年度自增数:取temp[1]位
                            if (year == DateTime.Now.Year)//是本年度
                            {
                                replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            else
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            }
                            //更新当前流水号
                            DBHelper db1 = new DBHelper();
                            db1.ExecuteScalar(tran, string.Format("UPDATE dbo.PostInfo SET CurrentNumber={0},CodeYear={2} WHERE PostID = {1}", replaceValue[i], _PostInfoOB.PostID, DateTime.Now.Year));
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            _PostInfoOB.CodeYear = DateTime.Now.Year;
                            break;
                        case "Y"://年份:取temp[1]位
                            replaceValue[i] = DateTime.Now.ToString("".PadLeft(Convert.ToInt32(temp[1]), 'y'));
                            break;
                        case "N"://自增数:取temp[1]位
                            replaceValue[i] = (_CurrentNumber + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                            //更新当前流水号
                            DBHelper db = new DBHelper();
                            db.ExecuteScalar(tran, string.Format("UPDATE dbo.PostInfo SET CurrentNumber={0},CodeYear={2} WHERE PostID = {1}", replaceValue[i], _PostInfoOB.PostID, DateTime.Now.Year));
                            _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            break;
                        case "TrainCodePYN"://表示按培训点岗位类别年度流水号
                            DBHelper db3 = new DBHelper();
                            object rtn2 = db3.ExecuteScalar(tran, string.Format(@"SELECT CURRENTNUMBER FROM dbo.TRAINUNITSENDCODE
                                                                    where TRAINUNITCODE='{0}' and POSTLEVEL={1} and POSTID={2} and CODEYEAR={3};", TrainUnitCdoe, "0", _PostInfoOB.UpPostID, DateTime.Now.Year));

                            if (rtn2 == null)
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                                db3.ExecuteScalar(tran, string.Format("insert into dbo.TrainUnitSendCode(TRAINUNITCODE,POSTLEVEL,POSTID,CODEYEAR,CURRENTNUMBER) values('{0}',{1},{2},{3},{4}) ", TrainUnitCdoe, "0", _PostInfoOB.UpPostID, DateTime.Now.Year, replaceValue[i]));
                                _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            }
                            else
                            {
                                replaceValue[i] = (Convert.ToInt32(rtn2) + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                                db3.ExecuteScalar(tran, string.Format(@"UPDATE dbo.TrainUnitSendCode SET CURRENTNUMBER={4} Where TRAINUNITCODE='{0}' and POSTLEVEL={1} and POSTID={2} and CODEYEAR={3}", TrainUnitCdoe, "0", _PostInfoOB.UpPostID, DateTime.Now.Year, replaceValue[i]));
                            }
                            break;
                        case "TLPYN"://表示按培训点、技能等级、岗位工种年度流水号
                            DBHelper db2 = new DBHelper();
                            object rtn = db2.ExecuteScalar(tran, string.Format(@"SELECT CURRENTNUMBER FROM dbo.TRAINUNITSENDCODE
                                                                    where TRAINUNITCODE='{0}' and POSTLEVEL={1} and POSTID={2} and CODEYEAR={3};",TrainUnitCdoe, GetSkillLevelCode(SkillLevel), _PostInfoOB.PostID, DateTime.Now.Year));

                            if (rtn == null)
                            {
                                replaceValue[i] = 1.ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                                db2.ExecuteScalar(tran, string.Format("insert into dbo.TrainUnitSendCode(TRAINUNITCODE,POSTLEVEL,POSTID,CODEYEAR,CURRENTNUMBER) values('{0}',{1},{2},{3},{4}) ", TrainUnitCdoe, GetSkillLevelCode(SkillLevel), _PostInfoOB.PostID, DateTime.Now.Year, replaceValue[i]));
                                _PostInfoOB.CurrentNumber = Convert.ToInt64(replaceValue[i]);
                            }
                            else
                            {
                                replaceValue[i] = (Convert.ToInt32(rtn) + 1).ToString("".PadLeft(Convert.ToInt32(temp[1]), '0'));
                                db2.ExecuteScalar(tran, string.Format(@"UPDATE dbo.TrainUnitSendCode SET CURRENTNUMBER={4} Where TRAINUNITCODE='{0}' and POSTLEVEL={1} and POSTID={2} and CODEYEAR={3}", TrainUnitCdoe, GetSkillLevelCode(SkillLevel), _PostInfoOB.PostID, DateTime.Now.Year, replaceValue[i]));
                            }
                            break;
                    }
                }
            }

            return string.Format(_format.ToString(), replaceValue);
        }


        /// <summary>
        /// 获取所有知识大纲
        /// </summary>
        /// <returns></returns>

   
        public static DataTable GetAllInfoTags()
        {
            string sql = @"
                        SELECT t1.* 
                        FROM 
                        (
                            SELECT *,row_number() over(order by PostName) rn  
                            FROM dbo.PostInfo
                            WHERE 1=1  and PostType = 1 
                        ) t1 
                        WHERE rn between 0 and 2147483646
                        order by rn";
            try
            {
                DBHelper db = new DBHelper();
                return db.GetFillData(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
