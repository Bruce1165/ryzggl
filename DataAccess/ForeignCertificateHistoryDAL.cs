using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;


namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ForeignCertificateHistoryDAL(填写类描述)
    /// </summary>
    public class ForeignCertificateHistoryDAL
    {
        public ForeignCertificateHistoryDAL() { }

        public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ForeignCertificateHistory", "*", filterWhereString, orderBy == "" ? " HistoryID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ForeignCertificateHistory", filterWhereString);
        }

    }
}
