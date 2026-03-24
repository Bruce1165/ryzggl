using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class Certificate_LockMDL
    {
        
        public Certificate_LockMDL()
		{			

		}

        //主键
        private string _Id;

       
        protected string _Fid;


        protected DateTime? _LockStartTime;
        //其它属性
        protected string _LockContent;


        protected string _LockPeople;


        protected DateTime? _LockEndTime;


        protected DateTime? _UnlockTime;

        protected string _UnlockContent;

        private string _UnlockPeople;

        private string _PSN_CertificateNO;

      

        protected string _LX;

        protected string _LockStates;

        protected string _PSN_RegisterCertificateNo;

        protected string _PSN_Name;


        public  string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Fid
        {
            get { return _Fid; }
            set { _Fid = value; }
        }
        public DateTime? LockStartTime
        {
            get { return _LockStartTime; }
            set { _LockStartTime = value; }
        }
        public string LockContent
        {
            get { return _LockContent; }
            set { _LockContent = value; }
        }
        public string LockPeople
        {
            get { return _LockPeople; }
            set { _LockPeople = value; }
        }
        public DateTime? LockEndTime
        {
            get { return _LockEndTime; }
            set { _LockEndTime = value; }
        }
        public DateTime? UnlockTime
        {
            get { return _UnlockTime; }
            set { _UnlockTime = value; }
        }
        public string UnlockContent
        {
            get { return _UnlockContent; }
            set { _UnlockContent = value; }
        }

        public string UnlockPeople
        {
            get { return _UnlockPeople; }
            set { _UnlockPeople = value; }
        }
        public string PSN_CertificateNO
        {
            get { return _PSN_CertificateNO; }
            set { _PSN_CertificateNO = value; }
        }
        public string LX
        {
            get { return _LX; }
            set { _LX = value; }
        }
        public string LockStates
        {
            get { return _LockStates; }
            set { _LockStates = value; }
        }
        public string PSN_RegisterCertificateNo
        {
            get { return _PSN_RegisterCertificateNo; }
            set { _PSN_RegisterCertificateNo = value; }
        }
        public string PSN_Name
        {
            get { return _PSN_Name; }
            set { _PSN_Name = value; }
        }
    }
}
