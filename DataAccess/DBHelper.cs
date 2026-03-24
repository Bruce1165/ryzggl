using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DataAccess
{
    /// <summary>
    ///     Sql数据库操作帮助类
    /// </summary>
    public class DBHelper : IDisposable
    {
        //定义这个类要使用的全局变量
        private static string constr;

        private SqlDataAdapter adapter;

        private SqlCommand cmd;
        private SqlConnection conn;
        private SqlDataReader dr;
        private bool badsql;//是否记录慢查询日志
        private int badSecond = 5;//超过几秒视为慢查询

        public DBHelper()
        {
            constr = WebConfigurationManager.ConnectionStrings["DBConnStr"].ToString();
            badsql = WebConfigurationManager.AppSettings["badsql"] == "on" ? true : false;
            badSecond = Convert.ToInt32(WebConfigurationManager.AppSettings["badSecond"]);

        }

        /// <summary>
        /// 创建数据库操作类
        /// </summary>
        /// <param name="dataBase">数据库连接名称</param>
        public DBHelper(string dataBase)
        {
            constr = MyWebConfig.GetConnectionStrings(dataBase);
            badsql = WebConfigurationManager.AppSettings["badsql"] == "on" ? true : false;
            badSecond = Convert.ToInt32(WebConfigurationManager.AppSettings["badSecond"]);
        }
        //public DBHelper(string a)
        //{
        //    constr = "Data Source=.;Database=PersonCertificate;User ID=sa;Password=123456";
        //    conn = new SqlConnection(constr);
        //    conn.Open();

        //}
        

        /// <summary>
        ///     数据库连接属性,连接配置文件的字符串为"ConnectionString"
        /// </summary>
        public SqlConnection Connectionstrings
        {
            get
            {
                //ConnectionStringSettingsCollection connectionStrings =
                //    WebConfigurationManager.ConnectionStrings;

                //constr = connectionStrings["DBConnStr"].ToString();

                //DotNet默认打开数据库连接池
                if (conn == null)
                {
                    conn = new SqlConnection(constr);
                }

                if (conn.State == ConnectionState.Open)
                {
                    return conn;
                }
                conn.Open();
                return conn;
            }
        }

      

        // 先做几个处理 ,该类实现了IDisposable接口，

        // 自动调用非托管堆中释放资源，在由GC自动清理。

        public void Dispose()
        {
            Close();
            cmd.Dispose();
            dr.Dispose();
            conn.Dispose();
        }

        /// <summary>
        ///     取消 Command 执行，并关闭 DataReader 对象和数据连接
        /// </summary>
        public void Close()
        {
            if (dr != null && !dr.IsClosed)
                dr.Close();
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.Close();
        }

        /// <summary>
        ///     打开连接
        /// </summary>
        public void Open()
        {
            if (conn == null) conn = new SqlConnection(constr);
            if (conn.State != ConnectionState.Open) conn.Open();
        }

        /// <summary>
        ///     开启事务处理
        /// </summary>
        public DbTransaction BeginTransaction()
        {
            Open();
            var tran = new DbTransaction(conn.BeginTransaction());
            return tran;
        }

        /// <summary>
        ///     创建一个 SQL 参数，主要实现SqlParameter[] 参数列表
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">类型</param>
        /// <param name="value">值</param>
        /// <returns>返回创建完成的参数</returns>
        public SqlParameter CreateParameter(string parameterName, SqlDbType dbType, object value)
        {
            var result = new SqlParameter(parameterName, dbType);
            result.Value = (value == null ? DBNull.Value : value);
            return result;
        }

        public SqlParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            var result = new SqlParameter(parameterName, dbType);
            result.Value = (value == null ? DBNull.Value : value);
            return result;
        }

        public SqlParameter CreateOutParameter(string parameterName, DbType dbType)
        {
            SqlParameter result = new SqlParameter(parameterName, dbType);
            result.Direction = ParameterDirection.Output;
            result.Size = 8;
            return result;
        }

        public SqlParameter CreateReturnParameter(string parameterName, DbType dbType)
        {
            SqlParameter result = new SqlParameter(parameterName, dbType);
            result.Direction = ParameterDirection.ReturnValue;
            return result;
        }

        /// <summary>
        ///     单向操作，主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(string cmdTxt)
        {
            return GetExcuteNonQuery(cmdTxt, null);
        }

        /// <summary>
        ///     带参数化的　主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">带参数列表的sql语句</param>
        /// <param name="pars">要传入的参数列表</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                cmd.CommandTimeout = 600;
                DateTime t1 = DateTime.Now;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
                conn.Close();
                return result;
            }
        }

        /// <summary>
        ///     单向操作，主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt)
        {
            return GetExcuteNonQuery(tran, cmdTxt, null);
        }

        /// <summary>
        ///     带参数化的　主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdTxt">带参数列表的sql语句</param>
        /// <param name="pars">要传入的参数列表</param>
        /// <returns></returns>
        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                if (pars != null) cmd.Parameters.AddRange(pars);
                if (tran != null) cmd.Transaction = tran.Transaction;

                int result = cmd.ExecuteNonQuery();
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
                return result;
            }
        }

        /// <summary>
        ///     对连接执行 Transact-SQL 语句或者存储过程并返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype"></param>
        /// <param name="pars">参数</param>
        /// <returns>受影响的行数</returns>
        public int GetExcuteNonQuery(string cmdText, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdText, Connectionstrings))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdText));
                    }
                }

                conn.Close();
                return result;
            }
        }

        public int GetExcuteNonQuery(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }

                return result;
            }
        }

        public int GetExcuteNonQuery(DbTransaction tran, int cmdTimeOut, string cmdTxt, CommandType cmdtype,
            params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                cmd.CommandTimeout = cmdTimeOut;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                int result = cmd.ExecuteNonQuery();

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }

                return result;
            }
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdText">SQL 语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>结果集中第一行的第一列或空引用</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(tran, cmdText, CommandType.Text, pars);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="cmdText">SQL 语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>结果集中第一行的第一列或空引用</returns>
        public T ExecuteScalar<T>(string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(null, cmdText, pars);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">参数类型[范型]</typeparam>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(string cmdText)
        {
            return ExecuteScalar<T>(cmdText, null);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">参数类型[范型]</typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdText)
        {
            return ExecuteScalar<T>(tran, cmdText, null);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdTxt">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype">决定是存储过程还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            return ExecuteScalar<T>(null, cmdTxt, cmdtype, pars);
        }

        /// <summary>
        ///     执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="cmdTxt">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype">决定是存储过程还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回T类型</returns>
        public T ExecuteScalar<T>(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                T result;

                try
                {
                    result = (T)cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    Close();
                    throw;
                }

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }

                if (tran == null) Close();
                return result;
            }
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns>一个 DataReader 对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt)
        {
            return GetDataReader(cmdTxt, null);
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <param name="pars">参数</param>
        /// <returns>一个 DataReader 对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                DateTime t1 = DateTime.Now;
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }

                return dr;
            }
        }

        public SqlDataReader GetDataReader(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader();

                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
                return dr;
            }
        }

        /// <summary>
        ///     将 cmdText 发送到 System.Data.SqlClient.SqlCommand.Connection，并使用 System.Data.CommandBehavior 值之一生成一个 DataReader
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataReader对象</returns>
        public SqlDataReader GetDataReader(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
                return dr;
            }
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">sql语句</param>
        public DataTable GetFillData(string cmdTxt)
        {
            return GetFillData(cmdTxt, null);
        }

        public DataTable GetFillData(DbTransaction tran, string cmdTxt)
        {
            return GetFillData(tran, cmdTxt, null);
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">带参数的sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回是一个数据表</returns>
        public DataTable GetFillData(string cmdTxt, params SqlParameter[] pars)
        {
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                DateTime t1 = DateTime.Now;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                    conn.Close();
                }
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetFillData(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, tran == null ? Connectionstrings : tran.Connection))
            {
                DateTime t1 = DateTime.Now;
                if (tran != null) cmd.Transaction = tran.Transaction;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        ///     做数据绑定显示作用，一般绑定的是数据查看控件
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetFillData(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            var ds = GetDataSet(cmdTxt, cmdtype, pars);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 分页数据绑定显示
        /// </summary>
        /// <param name="cmdTxt">string.format格式化sql语句,格式如:"select top {0} * from books where typeid not in (select top {1} id from books order by typeid) order by typeid"总记录数 TotalRecordCount总记录数通过executescalar获取</param>
        /// <param name="pageSize">设置的分页数大小,默认为10</param>
        /// <param name="currentIndex">当前页的索引,通常是通过querystring获取.如:string currentIndex = Request.QueryString["id"] ?? "1"</param>
        /// <returns>返回当前页的数据显示</returns>
        public DataTable GetFillData(string cmdTxt, int pageSize, int currentIndex)
        {
            DateTime t1 = DateTime.Now;
            DataTable dt = new DataTable();
            using (adapter = new SqlDataAdapter(string.Format(cmdTxt, pageSize, pageSize * (currentIndex - 1)), GetConnection()))
            {
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    Close();
                    throw ex;
                }
                Close();

                DateTime t2 = DateTime.Now;
                if (t1.AddSeconds(15) < t2)
                {
                    Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt), null);
                }
                return dt;
            }
        }

        /// <summary>
        /// 获取一个数据集
        /// </summary>
        /// <param name="cmdTxt">存储过程名称或者sql语句</param>
        /// <param name="cmdtype">决定是存储过程类型还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>返回一个DataSet</returns>
        public DataSet GetDataSet(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
          
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, Connectionstrings))
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                    conn.Close();
                }
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
            }
            return ds;
        }
        public DataSet GetDataSet2(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            
            var ds = new DataSet();
            using (cmd = new SqlCommand(cmdTxt, conn)) 
            {
                DateTime t1 = DateTime.Now;
                cmd.CommandType = cmdtype;
                if (pars != null) cmd.Parameters.AddRange(pars);
                using (adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                    conn.Close();
                }
                if (badsql == true)
                {
                    DateTime t2 = DateTime.Now;
                    if (t1.AddSeconds(badSecond) < t2)
                    {
                        Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt));
                    }
                }
            }
            return ds;
        }
      

        /// <summary>
        /// 使用SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="dt">源数据</param>
        public void SqlBulkCopyByDatatable(string tableName, DataTable dt)
        {
            using (var sqlbulkcopy = new SqlBulkCopy(Connectionstrings, SqlBulkCopyOptions.UseInternalTransaction, null))
            {
                sqlbulkcopy.BatchSize = 1000;
                sqlbulkcopy.BulkCopyTimeout = 5000;
                sqlbulkcopy.DestinationTableName = tableName;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                }
                sqlbulkcopy.WriteToServer(dt);
            }
        }

        #region Exam

        /// <summary>
        /// 获取Connecttion
        /// </summary>
        /// <returns>Connecttion</returns>
        public SqlConnection GetConnection()
        {
            if (conn == null) conn = new SqlConnection(constr);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            return conn;
        }

        // <summary>
        /// 单向操作，主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">安全的sql语句（string.format）</param>
        /// <returns></returns>
        public int ExcuteNonQuery(string cmdTxt)
        {
            return ExcuteNonQuery(null, cmdTxt);
        }

        public int ExcuteNonQuery(DbTransaction tran, string cmdTxt)
        {
            return ExcuteNonQuery(tran, cmdTxt, null);
        }

        /// <summary>
        /// 带参数化的　主要用于（增加，删除，修改）,返回受影响的行数
        /// </summary>
        /// <param name="cmdTxt">带参数列表的sql语句</param>
        /// <param name="pars">要传入的参数列表</param>
        /// <returns></returns>
        public int ExcuteNonQuery(string cmdTxt, params SqlParameter[] pars)
        {
            return ExcuteNonQuery(cmdTxt, CommandType.Text, pars);
        }

        public int ExcuteNonQuery(DbTransaction tran, string cmdTxt, params SqlParameter[] pars)
        {
            return ExcuteNonQuery(tran, cmdTxt, CommandType.Text, pars);
        }

        /// <summary>
        /// 对连接执行 Transact-SQL 语句或者存储过程并返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL 语句或者存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExcuteNonQuery(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            return ExcuteNonQuery(null, cmdTxt, CommandType.Text, pars);
        }


        public int ExcuteNonQuery(string cmdTxt, CommandType cmdtype)
        {
            DateTime t1 = DateTime.Now;
            using (cmd = new SqlCommand(cmdTxt, GetConnection()))
            {
                cmd.CommandType = cmdtype;
                cmd.CommandTimeout = 600;
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Close();
                    throw ex;
                }
                Close();
                DateTime t2 = DateTime.Now;
                if (t1.AddSeconds(15) < t2)
                {
                    Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt), null);
                }
                return result;
            }
        }

        public int ExcuteNonQuery(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            DateTime t1 = DateTime.Now;
            using (cmd = new SqlCommand(cmdTxt, tran == null ? GetConnection() : tran.Connection))
            {
                cmd.CommandType = cmdtype;
                cmd.CommandTimeout = 600;
                if (pars != null)
                {
                    foreach (SqlParameter _p in pars)
                    {
                        if (_p.Direction == ParameterDirection.Output) continue;
                        if (_p.Value == null)
                        {
                            _p.Value = DBNull.Value;
                        }
                    }
                    cmd.Parameters.AddRange(pars);
                }
                if (tran != null) cmd.Transaction = tran.Transaction;
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Close();
                    throw ex;
                }
                if (tran == null) Close();
                DateTime t2 = DateTime.Now;
                if (t1.AddSeconds(15) < t2)
                {
                    Utility.FileLog.WriteLog(string.Format("badsql：dotime【{0} - {1}】，共{2}秒。\r\n【sql:{3}】", t1.ToString("HH:mm:ss.fff"), t2.ToString("HH:mm:ss.fff"), (t2 - t1).Seconds, cmdTxt), null);
                }
                return result;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(null, cmdText);
        }

        public object ExecuteScalar(DbTransaction tran, string cmdText)
        {
            return ExecuteScalar(tran, cmdText, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdText">SQL 语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>结果集中第一行的第一列或空引用</returns>
        public object ExecuteScalar(string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar(null, cmdText, CommandType.Text, pars);
        }

        public object ExecuteScalar(DbTransaction tran, string cmdText, params SqlParameter[] pars)
        {
            return ExecuteScalar(tran, cmdText, CommandType.Text, pars);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdTxt">SQL 语句或者存储过程名称</param>
        /// <param name="cmdtype">决定是存储过程还是sql语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            return ExecuteScalar(null, cmdTxt, cmdtype, pars);
        }

        public object ExecuteScalar(DbTransaction tran, string cmdTxt, CommandType cmdtype, params SqlParameter[] pars)
        {
            using (cmd = new SqlCommand(cmdTxt, tran == null ? GetConnection() : tran.Connection))
            {
                cmd.CommandType = cmdtype;
                cmd.CommandTimeout = 600;
                if (pars != null)
                {
                    foreach (SqlParameter _p in pars)
                    {
                        if (_p.Direction == ParameterDirection.Output) continue;
                        if (_p.Value == null)
                        {
                            _p.Value = DBNull.Value;
                        }
                    }
                    cmd.Parameters.AddRange(pars);
                }
                if (tran != null) cmd.Transaction = tran.Transaction;
                object result = null;
                try
                {
                    result = cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    Close();
                    throw;
                }
                if (tran == null) Close();
                return result;
            }
        }


        #endregion
    }
}