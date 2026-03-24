using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--双随机任务共享给注册中心填报
    /// </summary>
    public class Gx_Task_ToZczxDAL
    {
      
        /// <summary>
        /// 批量更新双随机任务检查结果
        /// </summary>
        /// <param name="quertParam">查询条件</param>
        /// <param name="Jcjg">检查结果</param>
        /// <param name="Jcsjks">检查开始时间</param>
        /// <param name="Jcsjjs">检查截止时间</param> 
        /// <returns></returns>
        public static int UpdateBatch(string filterString, string Jcjg, DateTime Jcsjks, DateTime Jcsjjs)
        {
//            //本机测试
//            string sql = string.Format(@"
//			UPDATE [LESP_3.0].dbo.Gx_Task_ToZczx
//				SET	Jcjg=@Jcjg,Jcsjks=@Jcsjks,Jcsjjs=@Jcsjjs
//			WHERE 1=1 {0} ", filterString);

            //生产环境
            string sql = string.Format(@"
			UPDATE [192.168.7.175].[LESP_3.0_DataExchange].dbo.Gx_Task_ToZczx
				SET	Jcjg=@Jcjg,Jcsjks=@Jcsjks,Jcsjjs=@Jcsjjs
			WHERE 1=1 {0} ", filterString);
   
            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("Jcjg", DbType.String, Jcjg),
                db.CreateParameter("Jcsjks", DbType.DateTime, Jcsjks),
                db.CreateParameter("Jcsjjs", DbType.DateTime, Jcsjjs),
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
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
            ////本机测试
            //return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[LESP_3.0].dbo.Gx_Task_ToZczx", "*", filterWhereString, orderBy == "" ? " Yhsj desc,[ID]" : orderBy);

            //生产环境
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[192.168.7.175].[LESP_3.0_DataExchange].dbo.Gx_Task_ToZczx", "*", filterWhereString, orderBy == "" ? " Yhsj desc,[ID]" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            ////本机测试
            //return CommonDAL.SelectRowCount("[LESP_3.0].dbo.Gx_Task_ToZczx", filterWhereString);

            //生产环境
            return CommonDAL.SelectRowCount("[192.168.7.175].[LESP_3.0_DataExchange].dbo.Gx_Task_ToZczx", filterWhereString);
        }

    }
}