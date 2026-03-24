//============================================================
// Product name:		
// Version: 			
// Coded by:			
// Auto generated at: 	2016-03-13 23:08:08
//============================================================

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Model;
using Utility;

namespace DataAccess
{
    /// <summary>
    /// 字典操作类
    /// </summary>
    public static class DictionaryDAL
    {
        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddDictionary(DictionaryMDL model)
        {
            return AddDictionary(null, model);
        }

        /// <summary>
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddDictionary(DbTransaction trans, DictionaryMDL model)
        {
            const string sql = "INSERT dbo.Dictionary (DicID,TypeID, TypeName, OrderID, DicName, DicDesc, Category)" +
                               "VALUES (@DicID,@TypeID, @TypeName, @OrderID, @DicName, @DicDesc, @Category)";


            SqlParameter[] para =
            {
                new SqlParameter("@DicID", model.DicID),
                new SqlParameter("@TypeID", model.TypeID),
                new SqlParameter("@TypeName", model.TypeName),
                new SqlParameter("@OrderID", model.OrderID),
                new SqlParameter("@DicName", model.DicName),
                new SqlParameter("@DicDesc", model.DicDesc),
                new SqlParameter("@Category", model.Category)
            };

            return new DBHelper().GetExcuteNonQuery(trans, sql, para) > 0;
        }


        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteDictionary(DictionaryMDL model)
        {
            return DeleteDictionaryByDicId(model.DicID);
        }

        /// <summary>
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteDictionary(DbTransaction trans, DictionaryMDL model)
        {
            return DeleteDictionaryByDicId(trans, model.DicID);
        }

        /// <summary>
        /// </summary>
        /// <param name="dicId"></param>
        /// <returns></returns>
        public static bool DeleteDictionaryByDicId(string dicId)
        {
            return DeleteDictionaryByDicId(null, dicId);
        }

        /// <summary>
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="dicId"></param>
        /// <returns></returns>
        public static bool DeleteDictionaryByDicId(DbTransaction trans, string dicId)
        {
            const string sql = "DELETE FROM dbo.Dictionary WHERE DicID = @DicID";

            SqlParameter[] para =
            {
                new SqlParameter("@DicID", dicId)
            };

            return new DBHelper().GetExcuteNonQuery(trans, sql, para) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ModifyDictionary(DictionaryMDL model)
        {
            return ModifyDictionary(null, model);
        }

        /// <summary>
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ModifyDictionary(DbTransaction trans, DictionaryMDL model)
        {
            const string sql =
                "UPDATE dbo.Dictionary " +
                "SET " +
                "TypeID = @TypeID, " +
                "TypeName = @TypeName, " +
                "OrderID = @OrderID, " +
                "DicName = @DicName, " +
                "DicDesc = @DicDesc, " +
                "Category = @Category " +
                "WHERE DicID = @DicID";

            SqlParameter[] para =
            {
                new SqlParameter("@DicID", model.DicID),
                new SqlParameter("@TypeID", model.TypeID),
                new SqlParameter("@TypeName", model.TypeName),
                new SqlParameter("@OrderID", model.OrderID),
                new SqlParameter("@DicName", model.DicName),
                new SqlParameter("@DicDesc", model.DicDesc),
                new SqlParameter("@Category", model.Category)
            };

            return new DBHelper().GetExcuteNonQuery(trans, sql, para) > 0;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static IList<DictionaryMDL> GetAllDictionaries()
        {
            const string sqlAll = "SELECT * FROM dbo.Dictionary";
            return GetDictionariesBySql(sqlAll);
        }

        /// <summary>
        /// </summary>
        /// <param name="dicId"></param>
        /// <returns></returns>
        public static DictionaryMDL GetDictionaryByDicId(string dicId)
        {
            const string sql = "SELECT * FROM dbo.Dictionary WHERE DicID = @DicID";

            SqlDataReader dr = new DBHelper().GetDataReader(sql, new SqlParameter("@DicID", dicId));
            try
            {
                if (dr.Read())
                {
                    var model = new DictionaryMDL
                    {
                        DicID = (string) Check.ConvertDBNull(dr["DicID"], SqlDbType.VarChar),
                        TypeID = (int) Check.ConvertDBNull(dr["TypeID"], SqlDbType.Int),
                        TypeName = (string) Check.ConvertDBNull(dr["TypeName"], SqlDbType.VarChar),
                        OrderID = (int) Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
                        DicName = (string) Check.ConvertDBNull(dr["DicName"], SqlDbType.VarChar),
                        DicDesc = (string) Check.ConvertDBNull(dr["DicDesc"], SqlDbType.VarChar),
                        Category = (int) Check.ConvertDBNull(dr["Category"], SqlDbType.Int),
                    };

                    dr.Close();

                    return model;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }


        /// <summary>
        ///     Table转换为List
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static IList<DictionaryMDL> TableToList(DataTable table)
        {
            return (from DataRow row in table.Rows
                select new DictionaryMDL
                {
                    DicID = (string) Check.ConvertDBNull(row["DicID"], SqlDbType.VarChar),
                    TypeID = (int) Check.ConvertDBNull(row["TypeID"], SqlDbType.Int),
                    TypeName = (string) Check.ConvertDBNull(row["TypeName"], SqlDbType.VarChar),
                    OrderID = (int) Check.ConvertDBNull(row["OrderID"], SqlDbType.Int),
                    DicName = (string) Check.ConvertDBNull(row["DicName"], SqlDbType.VarChar),
                    DicDesc = (string) Check.ConvertDBNull(row["DicDesc"], SqlDbType.VarChar),
                    Category = (int) Check.ConvertDBNull(row["Category"], SqlDbType.Int),
                }).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static IList<DictionaryMDL> GetDictionariesBySql(string safeSql)
        {
            DataTable table = new DBHelper().GetFillData(safeSql);
            return TableToList(table);
        }

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IList<DictionaryMDL> GetDictionariesBySql(string sql, params SqlParameter[] values)
        {
            DataTable table = new DBHelper().GetFillData(sql, values);

            return TableToList(table);
        }

        /// <summary>
        ///     获取
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetAllDictionary(string condition, string orderBy)
        {
            string sqlstr = string.Format("SELECT * FROM dbo.[Dictionary] WHERE 2>1 {0} {1}",
                !string.IsNullOrEmpty(condition) ? condition : "",
                !string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "");

            return (new DBHelper()).GetFillData(sqlstr, CommandType.Text);
        }

        /// <summary>
        ///     根据类型、排序ID从字典表中获取名称
        /// </summary>
        /// <param name="typeId">类型</param>
        /// <param name="orderId">排序ID</param>
        /// <returns></returns>
        public static string GetStringDicName(int typeId, string orderId)
        {
            const string sql = "SELECT DicName FROM dbo.Dictionary WHERE TypeID=@TypeID AND OrderID=@OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@TypeID", typeId),
                new SqlParameter("@OrderID", orderId)
            };
            return (new DBHelper()).ExecuteScalar<string>(sql, para);
        }

        /// <summary>
        ///     根据typeId的值来获取字典列表
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static IList<DictionaryMDL> GetDictionaryListByTypeId(int typeId)
        {
            string sql = string.Format("SELECT * FROM dbo.Dictionary WHERE TypeID=@TypeID Order By OrderID");
            DataTable dt = (new DBHelper()).GetFillData(sql, new SqlParameter("@TypeID", typeId));

            return TableToList(dt);
        }

        /// <summary>
        ///     获取填充到下拉列表的数据源
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static DataTable GetFillDropDownDataByTypeId(int typeId)
        {
            const string sql = @"SELECT DicName AS TypeName
                                       ,OrderID AS TypeValue 
                                 FROM dbo.Dictionary WHERE TypeID=@TypeID 
                                 Order By OrderID";
            return (new DBHelper()).GetFillData(sql, new SqlParameter("@TypeID", typeId));
        }

        /// <summary>
        ///     获取数据字典项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static DataTable GetDicName(int typeId)
        {
            const string sql = "SELECT * FROM dbo.Dictionary WHERE TypeID=@TypeID Order By OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@TypeID", typeId)
            };
            return (new DBHelper()).GetFillData(sql, para);
        }

        /// <summary>
        ///     获取数据字典具体项
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static DataTable GetDicName(int typeId, string orderId)
        {
            string sql = "SELECT * FROM dbo.Dictionary WHERE TypeID=" + typeId + " AND OrderID in (" + orderId + ")";
            return (new DBHelper()).GetFillData(sql);
        }

        /// <summary>
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="dicName"></param>
        /// <returns></returns>
        public static DataTable GetDicNameByTypeId(string typeId, string dicName)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT DicName FROM dbo.Dictionary WHERE 1=1 ");
            if (typeId.Trim() != "")
            {
                strSql.Append(" and TypeID='" + typeId + "' ");
            }
            if (dicName.Trim() != "")
            {
                strSql.Append(" and DicName in ('" + dicName + "') ");
            }
            strSql.Append(" Order By OrderID");

            return (new DBHelper()).GetFillData(strSql.ToString());
        }

        /// <summary>
        /// 根据类型、显示名称模糊查询OrderID值
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="dicName"></param>
        /// <returns></returns>
        public static int GetDicOrderId(int typeId, string dicName)
        {
            const string sql = "SELECT OrderID FROM dbo.Dictionary WHERE TypeID=@TypeID AND DicName LIKE @DicName";
            SqlParameter[] para =
            {
                new SqlParameter("@TypeID", typeId),
                new SqlParameter("@DicName", string.Format("%{0}%",dicName))
            };

            return (new DBHelper()).ExecuteScalar<int>(sql, para);
        }
    }
}