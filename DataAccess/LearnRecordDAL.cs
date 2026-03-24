using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    public class LearnRecordDAL
    {
        public static long Insert(DbTransaction tran, LearnRecordOB _LearnRecordOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.LearnRecord(RecordNo,PostName,WorkerName,WorkerCertificateCode,LinkTel,CertificateCode,ClassHour)
			VALUES (:RecordNo,:PostName,:WorkerName,:WorkerCertificateCode,:LinkTel,:CertificateCode,:ClassHour)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("RecordNo", DbType.String, _LearnRecordOB.RecordNo));
            p.Add(db.CreateParameter("PostName", DbType.String, _LearnRecordOB.PostName));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _LearnRecordOB.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _LearnRecordOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("LinkTel", DbType.String, _LearnRecordOB.LinkTel));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _LearnRecordOB.CertificateCode));
            p.Add(db.CreateParameter("ClassHour", DbType.String, _LearnRecordOB.ClassHour));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        public static int Update(DbTransaction tran, LearnRecordOB _LearnRecordOB)
        {
            string sql = @"
			UPDATE dbo.LearnRecord
				SET	RecordNo =@RecordNo,PostName =@PostName,WorkerName =@WorkerName,WorkerCertificateCode =@WorkerCertificateCode,LinkTel =@LinkTel,ClassHour =@ClassHour
			WHERE
				CertificateCode =@CertificateCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("RecordNo", DbType.String, _LearnRecordOB.RecordNo));
            p.Add(db.CreateParameter("PostName", DbType.String, _LearnRecordOB.PostName));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _LearnRecordOB.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _LearnRecordOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("LinkTel", DbType.String, _LearnRecordOB.LinkTel));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _LearnRecordOB.CertificateCode));
            p.Add(db.CreateParameter("ClassHour", DbType.String, _LearnRecordOB.ClassHour));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.LearnRecord", filterWhereString);
        }

        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.LearnRecord", "*", filterWhereString, orderBy == "" ? "RecordID desc" : orderBy);
        }
    }
}
