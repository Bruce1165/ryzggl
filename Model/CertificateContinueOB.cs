using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--CertificateContinueOB填写类描述
    /// </summary>
    [Serializable]
    public class CertificateContinueOB
    {
        public CertificateContinueOB()
        {
            //默认值
        }

        //主键
        protected long? _CertificateContinueID;

        //其它属性
        protected long? _CertificateID;
        protected DateTime? _ApplyDate;
        protected string _ApplyMan;
        protected string _ApplyCode;
        protected DateTime? _GetDate;
        protected string _GetResult;
        protected string _GetMan;
        protected string _GetCode;
        protected DateTime? _CheckDate;
        protected string _CheckResult;
        protected string _CheckMan;
        protected string _CheckCode;
        protected DateTime? _ConfirmDate;
        protected string _ConfirmResult;
        protected string _ConfirmMan;
        protected string _ConfirmCode;
        protected DateTime? _ValidStartDate;
        protected DateTime? _ValidEndDate;
        protected string _Status;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _UnitCode;
        protected string _Phone;
        protected long? _FirstCheckUnitID;
        protected string _NewUnitName;
        protected string _NewUnitCode;
        protected string _NewUnitAdvise;
        protected DateTime? _NewUnitCheckTime;
        protected byte? _SheBaoCheck;

        protected string _ReportCode;
        protected DateTime? _ReportDate;
        protected string _ReportMan;

        protected string _Job;
        protected DateTime? _SheBaoCheckTime;
        protected DateTime? _ZACheckTime;
        protected int? _ZACheckResult;
        protected string _ZACheckRemark;

        protected int? _jxjyway;

        /// <summary>
        /// '继续校验形式：1：面投；2：网络教育；3：面授+网络教育
        /// </summary>
        public int? jxjyway
        {
            get { return _jxjyway; }
            set { _jxjyway = value; }
        }

        public string ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }
        public string ReportMan
        {
            get { return _ReportMan; }
            set { _ReportMan = value; }
        }


        public DateTime? ReportDate
        {
            get { return _ReportDate; }
            set { _ReportDate = value; }
        }

        /// <summary>
        /// 社保比对结果：0不符合，1符合。
        /// </summary>
        public byte? SheBaoCheck
        {
            get { return _SheBaoCheck; }
            set { _SheBaoCheck = value; }
        }

        protected string _FirstCheckUnitName;
        protected string _FirstCheckUnitCode;
         public string FirstCheckUnitName
        {
            get { return _FirstCheckUnitName; }
            set { _FirstCheckUnitName = value; }
        }

         public string FirstCheckUnitCode
        {
            get { return _FirstCheckUnitCode; }
            set { _FirstCheckUnitCode = value; }
        }

        public string NewUnitAdvise
        {
            get { return _NewUnitAdvise; }
            set { _NewUnitAdvise = value; }
        }

        public DateTime? NewUnitCheckTime
        {
            get { return _NewUnitCheckTime; }
            set { _NewUnitCheckTime = value; }
        }

        public string NewUnitName
        {
            get { return _NewUnitName; }
            set { _NewUnitName = value; }
        }

        public string NewUnitCode
        {
            get { return _NewUnitCode; }
            set { _NewUnitCode = value; }
        }

        public long? FirstCheckUnitID
        {
            get { return _FirstCheckUnitID; }
            set { _FirstCheckUnitID = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        public long? CertificateContinueID
        {
            get { return _CertificateContinueID; }
            set { _CertificateContinueID = value; }
        }
        public long? CertificateID
        {
            get { return _CertificateID; }
            set { _CertificateID = value; }
        }

        public DateTime? ApplyDate
        {
            get { return _ApplyDate; }
            set { _ApplyDate = value; }
        }

        public string ApplyMan
        {
            get { return _ApplyMan; }
            set { _ApplyMan = value; }
        }
        public string ApplyCode
        {
            get { return _ApplyCode; }
            set { _ApplyCode = value; }
        }

        public DateTime? GetDate
        {
            get { return _GetDate; }
            set { _GetDate = value; }
        }

        public string GetResult
        {
            get { return _GetResult; }
            set { _GetResult = value; }
        }

        public string GetMan
        {
            get { return _GetMan; }
            set { _GetMan = value; }
        }

        public string GetCode
        {
            get { return _GetCode; }
            set { _GetCode = value; }
        }

        public DateTime? CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
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

        public string CheckCode
        {
            get { return _CheckCode; }
            set { _CheckCode = value; }
        }

        public DateTime? ConfirmDate
        {
            get { return _ConfirmDate; }
            set { _ConfirmDate = value; }
        }

        public string ConfirmResult
        {
            get { return _ConfirmResult; }
            set { _ConfirmResult = value; }
        }

        public string ConfirmMan
        {
            get { return _ConfirmMan; }
            set { _ConfirmMan = value; }
        }

        public string ConfirmCode
        {
            get { return _ConfirmCode; }
            set { _ConfirmCode = value; }
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

        /// <summary>
        /// 安管人员在受聘企业担任的职务
        /// </summary>
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }


        /// <summary>
        /// 社保比对时间
        /// </summary>
        public DateTime? SheBaoCheckTime
        {
            get { return _SheBaoCheckTime; }
            set { _SheBaoCheckTime = value; }
        }


        /// <summary>
        /// 质安网数据校验时间
        /// </summary>
        public DateTime? ZACheckTime
        {
            get { return _ZACheckTime; }
            set { _ZACheckTime = value; }
        }
        /// <summary>
        /// 质安网数据校验结果，0：失败，1：成功，2：警告
        /// </summary>
        public int? ZACheckResult
        {
            get { return _ZACheckResult; }
            set { _ZACheckResult = value; }
        }
        /// <summary>
        /// 质安网数据校验结果说明
        /// </summary>
        public string ZACheckRemark
        {
            get { return _ZACheckRemark; }
            set { _ZACheckRemark = value; }
        }
    }
}
