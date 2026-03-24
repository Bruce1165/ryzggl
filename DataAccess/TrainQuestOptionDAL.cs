using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--TrainQuestOptionDAL(填写类描述)
	/// </summary>
    public class TrainQuestOptionDAL
    {
        public TrainQuestOptionDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
		    return Insert(null,_TrainQuestOptionMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.TrainQuestOption(QuestionID,OptionNo,OptionContent)
			VALUES (@QuestionID,@OptionNo,@OptionContent);SELECT @QuestOptionID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
           p.Add(db.CreateOutParameter("QuestOptionID",DbType.Int64));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64, _TrainQuestOptionMDL.QuestionID));
			p.Add(db.CreateParameter("OptionNo",DbType.String, _TrainQuestOptionMDL.OptionNo));
			p.Add(db.CreateParameter("OptionContent",DbType.String, _TrainQuestOptionMDL.OptionContent));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
			return Update(null,_TrainQuestOptionMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
			string sql = @"
			UPDATE dbo.TrainQuestOption
				SET	QuestionID = @QuestionID,OptionNo = @OptionNo,OptionContent = @OptionContent
			WHERE
				QuestOptionID = @QuestOptionID";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestOptionID",DbType.Int64, _TrainQuestOptionMDL.QuestOptionID));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64, _TrainQuestOptionMDL.QuestionID));
			p.Add(db.CreateParameter("OptionNo",DbType.String, _TrainQuestOptionMDL.OptionNo));
			p.Add(db.CreateParameter("OptionContent",DbType.String, _TrainQuestOptionMDL.OptionContent));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="QuestOptionID">主键</param>
		/// <returns></returns>
        public static int Delete( long QuestOptionID )
		{
			return Delete(null, QuestOptionID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QuestOptionID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long QuestOptionID)
		{
			string sql=@"DELETE FROM dbo.TrainQuestOption WHERE QuestOptionID = @QuestOptionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestOptionID",DbType.Int64,QuestOptionID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
			return Delete(null,_TrainQuestOptionMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestOptionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TrainQuestOptionMDL _TrainQuestOptionMDL)
		{
			string sql=@"DELETE FROM dbo.TrainQuestOption WHERE QuestOptionID = @QuestOptionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestOptionID",DbType.Int64,_TrainQuestOptionMDL.QuestOptionID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="QuestOptionID">主键</param>
        public static TrainQuestOptionMDL GetObject( long QuestOptionID )
		{
			string sql=@"
			SELECT QuestOptionID,QuestionID,OptionNo,OptionContent
			FROM dbo.TrainQuestOption
			WHERE QuestOptionID = @QuestOptionID";

            DBHelper db = new DBHelper("DBRYPX");
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("QuestOptionID", DbType.Int64, QuestOptionID));
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestOptionID",DbType.Int64,QuestOptionID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TrainQuestOptionMDL _TrainQuestOptionMDL = null;
                if (reader.Read())
                {
                    _TrainQuestOptionMDL = new TrainQuestOptionMDL();
					if (reader["QuestOptionID"] != DBNull.Value) _TrainQuestOptionMDL.QuestOptionID = Convert.ToInt64(reader["QuestOptionID"]);
					if (reader["QuestionID"] != DBNull.Value) _TrainQuestOptionMDL.QuestionID = Convert.ToInt64(reader["QuestionID"]);
					if (reader["OptionNo"] != DBNull.Value) _TrainQuestOptionMDL.OptionNo = Convert.ToString(reader["OptionNo"]);
					if (reader["OptionContent"] != DBNull.Value) _TrainQuestOptionMDL.OptionContent = Convert.ToString(reader["OptionContent"]);
                }
				reader.Close();
                db.Close();
                return _TrainQuestOptionMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.TrainQuestOption", "*", filterWhereString, orderBy == "" ? " OptionNo" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.TrainQuestOption", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
