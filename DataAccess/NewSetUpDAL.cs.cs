using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class NewSetUpDAL
    {
        public NewSetUpDAL() { }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(NewSetUpMDL _NewSetUpMDL)
        {
            return Insert(null, _NewSetUpMDL);
        }

        public static int Insert(DbTransaction tran, NewSetUpMDL _NewSetUpMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"delete from [dbo].[NewSetUp] where ApplyID=@ApplyId;
            INSERT INTO [dbo].[NewSetUp] (ApplyId,XslDateTime,ApplyType) 
            VALUES(@ApplyId,@XslDateTime,@ApplyType);";
            
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyId", DbType.String, _NewSetUpMDL.ApplyId));
            ////p.Add(db.CreateParameter("ENT_City", DbType.String, _NewSetUpMDL.ENT_City));
            ////p.Add(db.CreateParameter("ENT_Name", DbType.String, _NewSetUpMDL.ENT_Name));
            ////p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _NewSetUpMDL.ENT_OrganizationsCode));
            ////p.Add(db.CreateParameter("Psn_Name", DbType.String, _NewSetUpMDL.Psn_Name));
            p.Add(db.CreateParameter("XslDateTime", DbType.DateTime, _NewSetUpMDL.XslDateTime));
            // p.Add(db.CreateParameter("XslEndTime", DbType.DateTime, _NewSetUpMDL.XslEndTime));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _NewSetUpMDL.ApplyType));

            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        public static int BatchInsert(string applyidlist, string applytype)
        {
            return BatchInsert(null, applyidlist, applytype);
        }

        public static int BatchInsert(DbTransaction tran, string applyidlist, string applytype)
        {
            DBHelper db = new DBHelper();
            string sql = string.Format(@"
             delete  from [dbo].[NewSetUp] where ApplyID in (select ApplyID from Apply where 1=1 {0});
             INSERT INTO [dbo].[NewSetUp] (ApplyId,XslDateTime,ApplyType) 
             select ApplyId,GETDATE(),'{1}' from Apply where 1=1 {0};", applyidlist, applytype);
             //select a.ApplyId,GETDATE(),'{1}' from Apply a left join NewSetUp b on a.ApplyID=b.ApplyId where a.ApplyID in ({0}) and b.ApplyId is null", applyidlist, applytype);

           

          
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("ApplyId", DbType.String, _NewSetUpMDL.ApplyId));
            ////p.Add(db.CreateParameter("ENT_City", DbType.String, _NewSetUpMDL.ENT_City));
            ////p.Add(db.CreateParameter("ENT_Name", DbType.String, _NewSetUpMDL.ENT_Name));
            ////p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _NewSetUpMDL.ENT_OrganizationsCode));
            ////p.Add(db.CreateParameter("Psn_Name", DbType.String, _NewSetUpMDL.Psn_Name));
            //p.Add(db.CreateParameter("XslDateTime", DbType.DateTime, _NewSetUpMDL.XslDateTime));
            //p.Add(db.CreateParameter("XslEndTime", DbType.DateTime, _NewSetUpMDL.XslEndTime));
            //p.Add(db.CreateParameter("ApplyType", DbType.String, _NewSetUpMDL.ApplyType));
            return db.GetExcuteNonQuery(tran, sql);
        }
    }
}
