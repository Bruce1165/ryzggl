using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LearnRecordOB
    {
        private long recordID;
        private string recordNo = String.Empty;
        private string postName = String.Empty;
        private string workerName = String.Empty;
        private string linkTel = String.Empty;
        private string certificateCode = String.Empty;
        private string workerCertificateCode = String.Empty;
        private string classHour = String.Empty;


        public long RecordID
        {
            get { return this.recordID; }
            set { this.recordID = value; }
        }

        public string RecordNo
        {
            get { return this.recordNo; }
            set { this.recordNo = value; }
        }

        public string PostName
        {
            get { return this.postName; }
            set { this.postName = value; }
        }

        public string WorkerName
        {
            get { return this.workerName; }
            set { this.workerName = value; }
        }

        public string LinkTel
        {
            get { return this.linkTel; }
            set { this.linkTel = value; }
        }

        public string CertificateCode
        {
            get { return this.certificateCode; }
            set { this.certificateCode = value; }
        }

        public string WorkerCertificateCode
        {
            get { return this.workerCertificateCode; }
            set { this.workerCertificateCode = value; }
        }

        public string ClassHour
        {
            get { return this.classHour; }
            set { this.classHour = value; }
        }
    }
}
