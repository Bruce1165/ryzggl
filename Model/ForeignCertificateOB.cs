using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ForeignCertificateOB填写类描述
    /// </summary>
    [Serializable]
    public class ForeignCertificateOB
    {
        public ForeignCertificateOB()
        {
            //默认值
        }

        //主键
        protected long? _CertificateID;

        //其它属性
        protected long? _ApplyID;
        protected string _CertificateType;
        protected int? _PostTypeID;
        protected int? _PostID;
        protected string _CertificateCode;
        protected string _WorkerName;
        protected string _Sex;
        protected DateTime? _Birthday;
        protected string _UnitName;
        protected DateTime? _ConferDate;
        protected DateTime? _ValidStartDate;
        protected DateTime? _ValidEndDate;
        protected string _ConferUnit;
        protected string _Status;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _WorkerCertificateCode;
        protected string _UnitCode;
        protected string _ApplyCode;
        protected string _Phone;
        protected string _AddItemName;
        protected string _Remark;
        protected string _SkillLevel;        

        public long? CertificateID
        {
            get { return _CertificateID; }
            set { _CertificateID = value; }
        }

        public long? ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
        }

        public string CertificateType
        {
            get { return _CertificateType; }
            set { _CertificateType = value; }
        }

        public int? PostTypeID
        {
            get { return _PostTypeID; }
            set { _PostTypeID = value; }
        }

        public int? PostID
        {
            get { return _PostID; }
            set { _PostID = value; }
        }

        public string CertificateCode
        {
            get { return _CertificateCode; }
            set { _CertificateCode = value; }
        }

        public string WorkerName
        {
            get { return _WorkerName; }
            set { _WorkerName = value; }
        }

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        public DateTime? ConferDate
        {
            get { return _ConferDate; }
            set { _ConferDate = value; }
        }

        public DateTime? ValidStartDate
        {
            get { return _ValidStartDate; }
            set { _ValidStartDate = value; }
        }

        public DateTime? ValidEndDate
        {
            get { return _ValidEndDate; }
            set { _ValidEndDate = value; }
        }

        public string ConferUnit
        {
            get { return _ConferUnit; }
            set { _ConferUnit = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public long? CreatePersonID
        {
            get { return _CreatePersonID; }
            set { _CreatePersonID = value; }
        }

        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        public long? ModifyPersonID
        {
            get { return _ModifyPersonID; }
            set { _ModifyPersonID = value; }
        }

        public DateTime? ModifyTime
        {
            get { return _ModifyTime; }
            set { _ModifyTime = value; }
        }

        public string WorkerCertificateCode
        {
            get { return _WorkerCertificateCode; }
            set { _WorkerCertificateCode = value; }
        }

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        public string ApplyCode
        {
            get { return _ApplyCode; }
            set { _ApplyCode = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string AddItemName
        {
            get { return _AddItemName; }
            set { _AddItemName = value; }
        }

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        public string SkillLevel
        {
            get { return _SkillLevel; }
            set { _SkillLevel = value; }
        }
    }
}
