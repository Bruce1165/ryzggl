using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Utility;
using System.Collections.Generic;
using System.Reflection;

namespace DataAccess
{
    /// <summary>
    /// 公共数据访问方法
    /// </summary>
    public class CommonDAL
    {
        /// <summary>
        /// 获取单值
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static object GetObject(string sql)
        {
            return (new DBHelper()).ExecuteScalar(sql);
        }

        /// <summary>
        /// 统计记录行数
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static int SelectRowCount(string sql)
        {
            return (new DBHelper()).ExecuteScalar<int>(sql);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="WhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCount(string tableName, string WhereString)
        {
            return SelectRowCount(null, tableName, WhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="WhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCount(DbTransaction tran, string tableName, string WhereString)
        {
            string sql = "SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, WhereString);
            return (new DBHelper()).ExecuteScalar<int>(sql);
        }

        /// <summary>
        /// 统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="isHaveGroupBy">是否有汇总</param>
        /// <param name="groupbyString">汇总字段</param>
        /// <param name="showgroupbyfield">汇总之后显示的字段</param>
        /// <returns></returns>
        public static int SelectRowCount(DbTransaction tran, string tableName, string filterWhereString, bool isHaveGroupBy, string groupbyString, string showgroupbyfield)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}", tableName, filterWhereString);
            if (isHaveGroupBy)
            {
                sql = string.Format(@"SELECT COUNT(*) FROM (SELECT {3} FROM {0} WHERE 1=1 {1} GROUP BY {2})", tableName, filterWhereString, groupbyString, showgroupbyfield);
            }
            var db = new DBHelper();
            return Convert.ToInt32(tran != null ? db.ExecuteScalar(tran, sql) : db.ExecuteScalar(sql));
        }


        /// <summary>
        ///     获取任意表或试图记录
        /// </summary>
        /// <param name="tableName">表或试图名</param>
        /// <param name="WhereString">查询条件，格式：and xxx >1</param>
        /// <returns></returns>
        public static DataTable Select(string tableName, string WhereString)
        {
            string sql = "SELECT * FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, WhereString);
            return (new DBHelper()).GetFillData(sql);
        }

        #region LL添加

        /// <summary>
        ///     物理分页查询
        /// </summary>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTable(int startRowIndex, int maximumRows, string tableName, string clolumnList,
            string filterWhereString, string orderBy)
        {
            return GetDataTable(null, startRowIndex, maximumRows, tableName, clolumnList, filterWhereString, orderBy);
        }

        /// <summary>
        ///     物理分页查询
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTable(DbTransaction tran, int startRowIndex, int maximumRows, string tableName,
            string clolumnList, string filterWhereString, string orderBy)
        {
            //string sql = "SELECT * FROM( SELECT {0},row_number() over(order by {4} ) as RowNum FROM {2} WHERE 1=1 {3}) as t WHERE RowNum between {1} and {5}";
            //sql = string.Format(sql, clolumnList, Convert.ToString(startRowIndex + 1), tableName, filterWhereString, orderBy, Convert.ToString(startRowIndex + maximumRows));

            //string sql =
            //    @"SELECT t2.*  FROM (SELECT t1.*,row_number() over(order by {3}) as RowNum FROM (SELECT {0} FROM {1} WHERE 1=1 {2}) t1) t2 WHERE RowNum between {5} and {4}";
            //sql = string.Format(sql, clolumnList, tableName, filterWhereString, orderBy,
            //    Convert.ToString(startRowIndex + maximumRows), Convert.ToString(startRowIndex + 1));

            string sql = string.Format(@"SELECT *  FROM (SELECT {0},row_number() over(order by {3}) as RowNum FROM {1} WHERE 1=1 {2}) t WHERE RowNum between {5} and {4}", clolumnList, tableName, filterWhereString, orderBy, startRowIndex + maximumRows, startRowIndex + 1);

            var db = new DBHelper();
            DataTable dt = null;

            if (tran != null)
                dt = db.GetFillData(tran, sql);
            else
                dt = db.GetFillData(sql);
            return dt;
        }

        /// <summary>
        /// 物理分页查询
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <param name="isHaveGroupBy">是否有汇总</param>
        /// <param name="groupbyString">汇总字段</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTable(DbTransaction tran, int startRowIndex, int maximumRows, string tableName, string clolumnList, string filterWhereString, string orderBy, bool isHaveGroupBy, string groupbyString)
        {
            string sql = string.Format(@"SELECT t2.*  FROM (SELECT t1.*,row_number()  over({4}) as rn FROM (SELECT {0} FROM {1} WHERE 1=1 {2} {3} ) t1) t2 WHERE rn between {6} and {5}",
                                        clolumnList,
                                        tableName,
                                        filterWhereString,
                                        isHaveGroupBy ? string.Format("GROUP BY  {0} ", groupbyString) : "",
                                        string.IsNullOrEmpty(orderBy) ? "" : string.Format("ORDER BY {0}", orderBy),
                                        startRowIndex + maximumRows,
                                        startRowIndex + 1
                                        );
            DataTable dt = tran != null ? new DBHelper().GetFillData(tran, sql) : new DBHelper().GetFillData(sql);
            return dt;
        }


        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCount(string tableName, string clolumnList, string filterWhereString)
        {
            return GetRowCount(null, tableName, clolumnList, filterWhereString);
        }

        public static int GetRowCount(string tableName,string filterWhereString)
        {
            return GetRowCount(null, tableName, "", filterWhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCount(DbTransaction tran, string tableName, string clolumnList, string filterWhereString)
        {
            string sql = @"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, filterWhereString);
            var db = new DBHelper();
            if (tran != null)
                return db.ExecuteScalar<int>(tran, sql);
            return db.ExecuteScalar<int>(sql);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQL(string sql)
        {
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQL(DbTransaction tran, string sql)
        {
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(tran, sql) > 0 ? true : false;
        }
        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTable(string sql)
        {
            return (new DBHelper()).GetFillData(sql);
        }

        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTable(DbTransaction tran, string sql)
        {
            return (new DBHelper()).GetFillData(tran, sql);
        }

        /// <summary>
        /// 获取满足查询条件结果集数据第一条行号
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="subfilterWhereString">定位条件</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns></returns>
        public static long GetRowNo(DbTransaction tran, string tableName, string clolumnList, string filterWhereString, string subfilterWhereString, string orderBy)
        {
            string sql = @"SELECT top 1 t2.rn  FROM (SELECT t1.*,row_number() over(order by {3}) rn FROM (SELECT {0} FROM {1} WHERE 1=1 {2}) t1 ) t2 WHERE 1=1 {4}";
            sql = string.Format(sql, clolumnList, tableName, filterWhereString, orderBy, subfilterWhereString);

            DBHelper db = new DBHelper();
            long val = Convert.ToInt64(db.ExecuteScalar(tran, sql));
            return val;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="tran">事务，没有填null</param>
        /// <param name="valuesList">赋值实体类集合</param>
        /// <param name="tableName">目标表名称（必须带架构名，如:dbo.table1）</param>
        /// <param name="insertColumns">赋值列名称列表，格式如：column1,column2,column3...</param>
        /// <returns></returns>
        public static long InsertPatch(DbTransaction tran, List<Object> valuesList, string tableName, string insertColumns)
        {
            //            string sql = @"
            //			INSERT INTO dbo.Certificate(ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName)
            //			select ... union select ...";

            System.Text.StringBuilder sbAll = new StringBuilder();
            System.Text.StringBuilder sbOne = new StringBuilder();
            Type type_cls = null;
            PropertyInfo[] fdInfo = null;//反射类的属性            

            foreach (Object o in valuesList)
            {
                string[] parameters = insertColumns.ToLower().Replace("\"", "").Split(',');
                object[] values = new object[parameters.Length];
                type_cls = o.GetType();
                fdInfo = type_cls.GetProperties();//反射类的属性
                sbOne.Remove(0, sbOne.Length);
                for (int j = 0; j < parameters.Length; j++)
                {
                    foreach (PropertyInfo p in fdInfo)
                    {
                        if (p.Name.ToLower() == parameters[j])
                        {
                            values[j] = p.GetValue(o, null);
                            if (values[j] == null)
                            {
                                sbOne.Append(",").Append("null");
                            }
                            else
                            {
                                if (p.PropertyType.GetGenericArguments().Length > 0)//Nullable
                                {
                                    switch (p.PropertyType.GetGenericArguments()[0].FullName)
                                    {
                                        case "System.String":
                                        case "System.DateTime":
                                            sbOne.Append(string.Format(",'{0}'", values[j].ToString()));
                                            break;
                                        default:
                                            sbOne.Append(string.Format(",{0}", values[j].ToString()));
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (p.PropertyType.ToString())
                                    {
                                        case "System.String":
                                        case "System.DateTime":
                                            sbOne.Append(string.Format(",'{0}'", values[j].ToString()));
                                            break;
                                        default:
                                            sbOne.Append(string.Format(",{0}", values[j].ToString()));
                                            break;
                                    }
                                }

                            }
                            continue;
                        }
                    }
                }
                if (sbOne.Length > 0)
                {
                    sbOne.Remove(0, 1);
                }

                sbAll.Append(string.Format(" union all select {0}", sbOne.ToString()));
            }
            if (sbAll.Length > 0)
            {
                sbAll.Remove(0, 11);
            }

            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(tran, string.Format("INSERT INTO {0}({1}) {2}", tableName, insertColumns, sbAll.ToString()));
        }
        #endregion LL添加

        #region 自定义运行库

        /// <summary>
        /// 物理分页查询
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>     
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTableDB(string DBString,int startRowIndex, int maximumRows, string tableName, string clolumnList,
            string filterWhereString, string orderBy)
        {
            return GetDataTableDB(DBString, null, startRowIndex, maximumRows, tableName, clolumnList, filterWhereString, orderBy);
        }

        /// <summary>
        ///     物理分页查询
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTableDB(string DBString, DbTransaction tran, int startRowIndex, int maximumRows, string tableName,
            string clolumnList, string filterWhereString, string orderBy)
        {
            
            string sql = string.Format(@"SELECT *  FROM (SELECT {0},row_number() over(order by {3}) as RowNum FROM {1} WHERE 1=1 {2}) t WHERE RowNum between {5} and {4}", clolumnList, tableName, filterWhereString, orderBy, startRowIndex + maximumRows, startRowIndex + 1);

            var db = new DBHelper(DBString);
            DataTable dt = null;

            if (tran != null)
                dt = db.GetFillData(tran, sql);
            else
                dt = db.GetFillData(sql);
            return dt;
        }

        /// <summary>
        /// 物理分页查询
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行</param>
        /// <param name="maximumRows">单页最大行数</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="clolumnList">查询列，用逗号分割（如果是联接表加上表名）</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="orderBy">排序条件(如：Createtime desc,Username)</param>
        /// <param name="isHaveGroupBy">是否有汇总</param>
        /// <param name="groupbyString">汇总字段</param>
        /// <returns>结果集DataTable</returns>
        public static DataTable GetDataTableDB(string DBString, DbTransaction tran, int startRowIndex, int maximumRows, string tableName, string clolumnList, string filterWhereString, string orderBy, bool isHaveGroupBy, string groupbyString)
        {
            string sql = string.Format(@"SELECT t2.*  FROM (SELECT t1.*,row_number()  over({4}) as rn FROM (SELECT {0} FROM {1} WHERE 1=1 {2} {3} ) t1) t2 WHERE rn between {6} and {5}",
                                        clolumnList,
                                        tableName,
                                        filterWhereString,
                                        isHaveGroupBy ? string.Format("GROUP BY  {0} ", groupbyString) : "",
                                        string.IsNullOrEmpty(orderBy) ? "" : string.Format("ORDER BY {0}", orderBy),
                                        startRowIndex + maximumRows,
                                        startRowIndex + 1
                                        );
            DataTable dt = tran != null ? new DBHelper(DBString).GetFillData(tran, sql) : new DBHelper(DBString).GetFillData(sql);
            return dt;
        }


        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCountDB(string DBString, string tableName, string clolumnList, string filterWhereString)
        {
            return GetRowCountDB(DBString, null, tableName, clolumnList, filterWhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int GetRowCountDB(string DBString, DbTransaction tran, string tableName, string clolumnList, string filterWhereString)
        {
            string sql = @"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, filterWhereString);
            var db = new DBHelper(DBString);
            if (tran != null)
                return db.ExecuteScalar<int>(tran, sql);
            return db.ExecuteScalar<int>(sql);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQLDB(string DBString, string sql)
        {
            DBHelper db = new DBHelper(DBString);
            return db.GetExcuteNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="sql">sql脚本</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecSQLDB(string DBString, DbTransaction tran, string sql)
        {
            DBHelper db = new DBHelper(DBString);
            return db.GetExcuteNonQuery(tran, sql) > 0 ? true : false;
        }
        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTableDB(string DBString, string sql)
        {
            return (new DBHelper(DBString)).GetFillData(sql);
        }

        /// <summary>
        /// 执行sql，返回结果集table
        /// </summary>
        /// <param name="DBString">数据库连接名称</param>   
        /// <param name="sql">sql语句</param>
        /// <returns>table</returns>
        public static DataTable GetDataTableDB(string DBString, DbTransaction tran, string sql)
        {
            return (new DBHelper(DBString)).GetFillData(tran, sql);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="WhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCountDB(string DBString, string tableName, string WhereString)
        {
            return SelectRowCountDB(DBString, null, tableName, WhereString);
        }

        /// <summary>
        ///     统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="WhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <returns>行数</returns>
        public static int SelectRowCountDB(string DBString, DbTransaction tran, string tableName, string WhereString)
        {
            string sql = "SELECT COUNT(*) FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, tableName, WhereString);
            return (new DBHelper(DBString)).ExecuteScalar<int>(sql);
        }

        /// <summary>
        /// 统计记录行数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="tableName">表名，试图名，多表联接(如: Table1 inner join Table2 on Table1.id=Table2.id)</param>
        /// <param name="filterWhereString">where过滤条件，注意不要带“where”关键字(如：UserName like 'T%')</param>
        /// <param name="isHaveGroupBy">是否有汇总</param>
        /// <param name="groupbyString">汇总字段</param>
        /// <param name="showgroupbyfield">汇总之后显示的字段</param>
        /// <returns></returns>
        public static int SelectRowCountDB(string DBString, DbTransaction tran, string tableName, string filterWhereString, bool isHaveGroupBy, string groupbyString, string showgroupbyfield)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM {0} WHERE 1=1 {1}", tableName, filterWhereString);
            if (isHaveGroupBy)
            {
                sql = string.Format(@"SELECT COUNT(*) FROM (SELECT {3} FROM {0} WHERE 1=1 {1} GROUP BY {2})", tableName, filterWhereString, groupbyString, showgroupbyfield);
            }
            var db = new DBHelper(DBString);
            return Convert.ToInt32(tran != null ? db.ExecuteScalar(tran, sql) : db.ExecuteScalar(sql));
        }

        #endregion 自定义运行库

        #region 导出Excel

        private const string HtmlStartString = @"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"">
            <head>
            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
            <style>
            td{mso-number-format:\@;border-width:0.1pt;border-style:solid;border-color:#CCCCCC;}
            tr{height:16pt;}
             .noborder{border-width:0px!important;border-style:none;border-color:transparent!important;}
            </style>
            </head>
            <body>
            <table cellspacing=""0"" border=""0"" style=""width:100%;table-layout:auto;empty-cells:show;"" DefaultRowHeight=""18"">";


        private const string HtmlEndString = @"</table></body></html>";

        /// <summary>
        /// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        /// </summary>
        /// <param name="saveFileFullName">保存文件带路径的文件名</param>
        /// <param name="tableName">要导出的表名或试图名称</param>
        /// <param name="filterSql">过滤条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        /// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        /// <param name="Caption">输出自定义表头，如果不想带边框添加属相 class="noborder"
        ///  例如
        ///  <tr>
        ///     <td style="font-weight:bold;text-align:center;" class="noborder" colspan="8">二级建造师重新注册初审汇总表</td>
        ///  </tr>
        ///  <tr>
        ///     <td style="text-align:right;" colspan="4" class="noborder">批次号：</td>
        ///     <td style="text-align:right;" class="noborder" colspan="4">申报日期：</td>
        ///  </tr>
        ///  </param>
        /// <param name="Foot">输出自定义页尾,格式同Caption</param>
        public static void OutputXls(string saveFileFullName, string tableName, string filterSql, string orderBy, string headList, string columnList, string Caption, string Foot)
        {
            string sql = @"select {0} FROM {1} where  1=1 {2} order by {3} for xml path('') ";
            StringBuilder sb = new StringBuilder();

            try
            {
                SqlDataReader sdr = new DBHelper().GetDataReader(string.Format(sql, FormatColumnWithHtml(columnList, false), tableName, filterSql, orderBy));

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileFullName, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(HtmlStartString);
                    sw.Flush();
                    sw.Write(Caption);
                    sw.Flush();
                    if (string.IsNullOrEmpty(headList) == false)
                    {
                        sw.Write(FormatColumnWithHtml(headList, true));
                        sw.Flush();
                    }

                    while (sdr.Read())
                    {
                        sb.Append(sdr[0]);
                        sb.Replace("&lt;", "<").Replace("&gt;", ">");
                        sw.Write(sb.ToString().Substring(0, sb.Length - 4));//解决尖括号跨行问题
                        sb.Remove(0, sb.Length - 4);
                        sw.Flush();
                    }
                    sw.Write(sb);
                    sw.Flush();
                    sw.Write(Foot);
                    sw.Flush();
                    sw.Write(HtmlEndString);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("导出数据失败", ex);
                throw ex;
            }
            finally
            {

            }
        }
        

        /// <summary>
        /// 格式化导出excel页头和页尾
        /// </summary>
        /// <param name="content">
        /// 要添加的内容及格式：<r>表示分行 ；<c>表示分列；<s>表示属性；<v>表示属性名和值的分隔符。
        /// </param>
        /// <returns>转化后的xml</returns>
        protected static string formatExcelCaptionFoot(string content)
        {
            if (string.IsNullOrEmpty(content) == true) return "";
            //处理excle表头输出（如：标题，创建时间，编号等等。注意不是处理表格列名）
            StringBuilder CaptionSB = new StringBuilder();
            string[] CaptionTrList = content.Replace("<r>", "|").Split('|');//分行
            int tdIndex = 0;//td位置
            StringBuilder tdStyle = new StringBuilder();//td样式
            foreach (string r in CaptionTrList)
            {
                CaptionSB.Append("<tr>");
                string[] CaptionTdList = r.Replace("<c>", "|").Split('|');//分列
                foreach (string c in CaptionTdList)
                {
                    CaptionSB.Append("<td>");
                    tdIndex = CaptionSB.Length - 1;
                    tdStyle.Remove(0, tdStyle.Length);
                    string[] CaptionStyleList = c.Replace("<s>", "|").Split('|');//属性集合
                    foreach (string s in CaptionStyleList)
                    {
                        string[] nameValue = s.Replace("<v>", "|").Split('|');//属性集合

                        switch (nameValue[0])
                        {
                            case "text"://内容
                                CaptionSB.Append(nameValue[1]);
                                break;
                            case "colspan"://合并单元格
                                CaptionSB.Insert(tdIndex, string.Format(" colspan=\"{0}\"", nameValue[1]));
                                break;
                            case "rowspan"://合并行
                                CaptionSB.Insert(tdIndex, string.Format(" rowspan=\"{0}\"", nameValue[1]));
                                break;
                            case "class"://合并行
                                CaptionSB.Insert(tdIndex, string.Format(" class=\"{0}\"", nameValue[1]));
                                break;
                            default://其他样式
                                tdStyle.Append(string.Format("{0}:{1};", nameValue[0], nameValue[1]));
                                break;
                        }
                    }
                    if (tdStyle.Length > 0)
                    {
                        CaptionSB.Insert(tdIndex, string.Format(" style=\"{0}\"", tdStyle.ToString()));
                    }
                    CaptionSB.Append("</td>");
                }
                CaptionSB.Append("</tr>");
            }
            return CaptionSB.ToString();
        }

        /// <summary>
        /// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        /// </summary>
        /// <param name="saveFileFullName">保存文件带路径的文件名</param>
        /// <param name="tableName">要导出的表名或试图名称</param>
        /// <param name="filterSql">过滤条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        /// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        public static void OutputXls(string saveFileFullName, string tableName, string filterSql, string orderBy, string headList, string columnList)
        {
            string sql = @"select {0} FROM {1} where  1=1 {2} order by {3} for xml path('') ";
            StringBuilder sb = new StringBuilder();

            try
            {
                SqlDataReader sdr = new DBHelper().GetDataReader(string.Format(sql, FormatColumnWithHtml(columnList, false), tableName, filterSql, orderBy));

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileFullName, false, System.Text.Encoding.UTF8))
                {
                    //Flush()解决大数据导出内存溢出问题
                    sw.Write(HtmlStartString);
                    sw.Flush();
                    sw.Write(FormatColumnWithHtml(headList, true));
                    sw.Flush();

                    while (sdr.Read())
                    {
                        sb.Append(sdr[0]);
                        sb.Replace("&lt;", "<").Replace("&gt;", ">");
                        if (sb.Length > 4)
                        {
                            sw.Write(sb.ToString().Substring(0, sb.Length - 4));//解决尖括号跨行问题
                            sb.Remove(0, sb.Length - 4);
                            sw.Flush();
                        }
                       
                       
                    }
                    sw.Write(sb);
                    sw.Flush();
                    sw.Write(HtmlEndString);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("导出数据失败", ex);
                throw ex;
            }
            finally
            {

            }

        }

        ///// <summary>
        ///// 导出可用excel打开的用html格式化的数据表格文件（伪excel，数据库直接导出，速度快）
        ///// </summary>
        ///// <param name="saveFileFullName">保存文件带路径的文件名</param>
        ///// <param name="SheetName">在excel中显示标签页名称</param>
        ///// <param name="tableName">要导出的表名或试图名称</param>
        ///// <param name="filterSql">过滤条件</param>
        ///// <param name="orderBy">排序条件</param>
        ///// <param name="headList">导出表格行头显示名称列表，用英文反斜杠\分隔</param>
        ///// <param name="columnList">导出列名称列表，用英文反斜杠\分隔</param>
        //public static void OutputXlsx(string saveFileFullName, string SheetName, string tableName, string filterSql, string orderBy, string headList, string columnList)
        //{
        //    string sql = @"select {0} FROM {1} where  1=1 {2} order by {3} ";
        //    StringBuilder sb = new StringBuilder();
        //    string[] h = headList.Split('\\');
        //    string[] c = columnList.Split('\\');

        //    try
        //    {
        //        for (int i = 0; i < c.Length; i++)
        //        {
        //            sb.AppendFormat(",{0} as '{1}'", c[i], h[i]);
        //        }
        //        if (sb.Length > 0)
        //        {
        //            sb.Remove(0, 1);
        //        }

        //        DataTable dt = CommonDAL.GetDataTable(string.Format(sql, sb, tableName, filterSql, orderBy));
        //        Utility.ExcelDealHelp.ExportDataToXlsx(dt, SheetName, saveFileFullName);

        //    }
        //    catch (Exception ex)
        //    {
        //        FileLog.WriteLog("导出数据到Excel失败", ex);
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //}

        /// <summary>
        /// 获取文件大小KB
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileSize(string filePath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            if (file.Exists == false) return "0k";
            if (file.Length < 1024)
                return string.Format("{0} Byte", file.Length.ToString());
            else
                return string.Format("{0} KB", (file.Length / 1024).ToString());
        }


        /// <summary>
        /// 导出html形式伪excel前格式化输出列
        /// 格式如：<tr><td>列1</td><td>列2</td><td>列3</td></tr>
        /// </summary>
        /// <param name="columnsList">列集合（用英文竖杠|分隔）</param>
        /// <param name="ifHead">列头：true，数据行：false</param>
        /// <returns></returns>
        private static string FormatColumnWithHtml(string columnsList, bool ifHead)
        {
            StringBuilder sb = new StringBuilder();
            if (ifHead == true)//列头
            {
                sb.Append("<tr><th>");
                sb.Append(columnsList.Replace(@"\", "</th><th>"));
                sb.Append("</th></tr>");
            }
            else//数据行
            {
                sb.Append("'<tr><td>'+isnull(cast(");
                sb.Append(columnsList.Replace(@"\", " as varchar(max)),'')+'</td><td>'+isnull(cast("));
                sb.Append(" as varchar(max)),'')+'</td></tr>'");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 更新打印时间和打印人
        /// </summary>
        /// <param name="List_PSN_ServerID">人员证书ID集合，格式例如：'dcbbba2b-afce-4124-bd04-e6c60363c0df','75e27f17-a4d3-46d9-ab6d-a658c5e019cc'......</param>
        /// <param name="printMan">打印人</param>
        /// <param name="printDate">打印时间</param>
        /// <returns></returns>
        public static int UpdatePrintTime(string List_PSN_ServerID, string printMan, DateTime printDate)
        {
            DBHelper db = new DBHelper();
            //            const string sql = @"
            //			UPDATE dbo.Certificate
            //				SET	PrintMan = PrintMan,PrintDate = :PrintDate
            //			WHERE
            //				CertificateID = :CertificateID";
            string sql = string.Format("UPDATE COC_TOW_Person_BaseInfo  set PrintMan='{0}',PrintTime='{1}' where PSN_ServerID in({2})", printMan, printDate, List_PSN_ServerID);
            return db.GetExcuteNonQuery(sql);
        }


        ///// <summary>
        ///// 批量更新打印时间和打印人
        ///// </summary>
        ///// <param name="certificateId">证书ID</param>
        ///// <param name="printMan">打印人</param>
        ///// <param name="printDate">打印时间</param>
        ///// <returns></returns>
        //public static int UpdatePrintTime2(string  certificateId, string printMan, DateTime printDate)
        //{
        //    DBHelper db = new DBHelper();
        //    //            const string sql = @"
        //    //			UPDATE dbo.Certificate
        //    //				SET	PrintMan = PrintMan,PrintDate = :PrintDate
        //    //			WHERE
        //    //				CertificateID = :CertificateID";
        //    string sql = string.Format("UPDATE COC_TOW_Person_BaseInfo  set PrintMan='{0}',PrintTime='{1}' where PSN_ServerID in ({2})", printMan, printDate, certificateId);
        //    return db.GetExcuteNonQuery(sql);
        //}
        #endregion

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="Phone">手机号码（请调用前校验手机号码格式）</param>
        /// <param name="Message">短信内容</param>
        /// <returns></returns>
         public static bool SendMessage(string Phone, string Message)
        {
            string sql = @"INSERT INTO [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]([sjlx],[phone],[dxnr],[Addtime],[fslx],[ID],[VALID])
                            VALUES(43,'{0}','{1}',convert(varchar(13),getdate(),120),0,newid(),1)";
            DBHelper db = new DBHelper();
            return db.GetExcuteNonQuery(string.Format(sql, Phone, Message)) > 0 ? true : false;
        }
    }
}