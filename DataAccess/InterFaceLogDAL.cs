using System.Data;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--InterFaceLogDAL(填写类描述)
    /// </summary>
    public class InterFaceLogDAL
    {
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(null, "DBO.INTERFACELOG", filterWhereString, true, "ACCESSUSER", "ACCESSUSER,Count(ID) AS CountNum");
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectDetailPageCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(null, "DBO.INTERFACELOG", filterWhereString);
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
            return CommonDAL.GetDataTable(
                null,
                startRowIndex,
                maximumRows,
                "DBO.INTERFACELOG",
                "ACCESSUSER,Count(ID) AS CountNum",
                filterWhereString,
                orderBy == "" ? " ACCESSUSER ASC" : orderBy,
                true,
                "ACCESSUSER"
                );
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDetailList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(null, startRowIndex, maximumRows, "DBO.INTERFACELOG", "ID,ACCESSDATE,ACCESSTIME,ACCESSUSER,SERVERID,CALLINGMETHODNAME,PARAMETERDATA,METHODDESCRIPTION", filterWhereString, string.IsNullOrEmpty(orderBy.Trim()) ? "ACCESSDATE DESC,ACCESSTIME DESC" : orderBy);
        }
    }
}