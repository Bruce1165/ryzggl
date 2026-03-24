using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--jcsjk_tj_qy_zzwdbDAL(企业安全生产许可证未达标列表)
    /// </summary>
    public class jcsjk_tj_qy_aqxkwdbDAL
    {
        public jcsjk_tj_qy_aqxkwdbDAL() { }

     
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.TJ_QY_AQXK", "*", filterWhereString, orderBy == "" ? " QYMC" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.TJ_QY_AQXK", filterWhereString);
        }

        #region 自定义方法

        #endregion
    }
}
