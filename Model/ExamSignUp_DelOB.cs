using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamSignUpOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamSignUp_DelOB
    {
        public ExamSignUp_DelOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamSignUpDelID;

        //其它属性
        protected string _DeleteMan;
        protected DateTime? _DeleteTime;
        protected long? _ExamSignUpID;
        protected string _SignUpCode;
        protected DateTime? _SignUpDate;
        protected long? _WorkerID;
        protected long? _UnitID;
        protected long? _TrainUnitID;
        protected long? _ExamPlanID;
        protected DateTime? _WorkStartDate;
        protected int? _WorkYearNumer;
        protected string _PersonDetail;
        protected string _HireUnitAdvise;
        protected string _AdminUnitAdvise;
        protected string _CheckCode;
        protected string _CheckResult;
        protected string _CheckMan;
        protected DateTime? _CheckDate;
        protected string _PayNoticeCode;
        protected string _PayNoticeResult;
        protected string _PayNoticeMan;
        protected DateTime? _PayNoticeDate;
        protected decimal? _PayMoney;
        protected string _PayConfirmCode;
        protected string _PayConfirmRult;
        protected string _PayConfirmMan;
        protected DateTime? _PayConfirmDate;
        protected string _FacePhoto;
        protected string _Status;
        protected string _WorkerName;
        protected string _CertificateType;
        protected string _CertificateCode;
        protected string _UnitName;
        protected string _UnitCode;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _SKILLLEVEL;
        private string s_sex = String.Empty;
        private DateTime? s_birthday;
        private string s_culturalLevel = String.Empty;
        private string s_phone = String.Empty;

        public string S_SEX
        {
            get { return this.s_sex; }
            set { this.s_sex = value; }
        }

        public DateTime? S_BIRTHDAY
        {
            get { return this.s_birthday; }
            set { this.s_birthday = value; }
        }
        public string S_CULTURALLEVEL
        {
            get { return this.s_culturalLevel; }
            set { this.s_culturalLevel = value; }
        }
        public string S_PHONE
        {
            get { return this.s_phone; }
            set { this.s_phone = value; }
        }
        public long? ExamSignUpDelID
        {
            get { return _ExamSignUpDelID; }
            set { _ExamSignUpDelID = value; }
        }

        public string DeleteMan
        {
            get { return _DeleteMan; }
            set { _DeleteMan = value; }
        }

        public DateTime? DeleteTime
        {
            get { return _DeleteTime; }
            set { _DeleteTime = value; }
        }

        public long? ExamSignUpID
        {
            get { return _ExamSignUpID; }
            set { _ExamSignUpID = value; }
        }

        public string SignUpCode
        {
            get { return _SignUpCode; }
            set { _SignUpCode = value; }
        }

        public DateTime? SignUpDate
        {
            get { return _SignUpDate; }
            set { _SignUpDate = value; }
        }

        public long? WorkerID
        {
            get { return _WorkerID; }
            set { _WorkerID = value; }
        }

        public long? UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        public long? TrainUnitID
        {
            get { return _TrainUnitID; }
            set { _TrainUnitID = value; }
        }

        public long? ExamPlanID
        {
            get { return _ExamPlanID; }
            set { _ExamPlanID = value; }
        }

        public DateTime? WorkStartDate
        {
            get { return _WorkStartDate; }
            set { _WorkStartDate = value; }
        }

        public int? WorkYearNumer
        {
            get { return _WorkYearNumer; }
            set { _WorkYearNumer = value; }
        }

        public string PersonDetail
        {
            get { return _PersonDetail; }
            set { _PersonDetail = value; }
        }

        public string HireUnitAdvise
        {
            get { return _HireUnitAdvise; }
            set { _HireUnitAdvise = value; }
        }

        public string AdminUnitAdvise
        {
            get { return _AdminUnitAdvise; }
            set { _AdminUnitAdvise = value; }
        }

        public string CheckCode
        {
            get { return _CheckCode; }
            set { _CheckCode = value; }
        }

        public string CheckResult
        {
            get { return _CheckResult; }
            set { _CheckResult = value; }
        }

        public string CheckMan
        {
            get { return _CheckMan; }
            set { _CheckMan = value; }
        }

        public DateTime? CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }

        public string PayNoticeCode
        {
            get { return _PayNoticeCode; }
            set { _PayNoticeCode = value; }
        }

        public string PayNoticeResult
        {
            get { return _PayNoticeResult; }
            set { _PayNoticeResult = value; }
        }

        public string PayNoticeMan
        {
            get { return _PayNoticeMan; }
            set { _PayNoticeMan = value; }
        }

        public DateTime? PayNoticeDate
        {
            get { return _PayNoticeDate; }
            set { _PayNoticeDate = value; }
        }

        public decimal? PayMoney
        {
            get { return _PayMoney; }
            set { _PayMoney = value; }
        }

        public string PayConfirmCode
        {
            get { return _PayConfirmCode; }
            set { _PayConfirmCode = value; }
        }

        public string PayConfirmRult
        {
            get { return _PayConfirmRult; }
            set { _PayConfirmRult = value; }
        }

        public string PayConfirmMan
        {
            get { return _PayConfirmMan; }
            set { _PayConfirmMan = value; }
        }

        public DateTime? PayConfirmDate
        {
            get { return _PayConfirmDate; }
            set { _PayConfirmDate = value; }
        }

        public string FacePhoto
        {
            get { return _FacePhoto; }
            set { _FacePhoto = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string WorkerName
        {
            get { return _WorkerName; }
            set { _WorkerName = value; }
        }

        public string CertificateType
        {
            get { return _CertificateType; }
            set { _CertificateType = value; }
        }

        public string CertificateCode
        {
            get { return _CertificateCode; }
            set { _CertificateCode = value; }
        }

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
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

        public string SKILLLEVEL
        {
            get { return _SKILLLEVEL; }
            set { _SKILLLEVEL = value; }
        }

    }
}
