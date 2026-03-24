using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateEnterApplyOB填写类描述
	/// </summary>
	[Serializable]
	public class CertificateEnterApplyOB
	{
		public CertificateEnterApplyOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ApplyID;        
		
		//其它属性
		protected long? _WorkerID;
		protected int? _PostTypeID;
		protected int? _PostID;
		protected string _WorkerName;
		protected string _Sex;
		protected DateTime? _Birthday;
		protected string _WorkerCertificateCode;
		protected string _OldUnitName;
		protected string _UnitName;
		protected string _UnitCode;
		protected string _Phone;
		protected string _CertificateCode;
		protected DateTime? _ConferDate;
		protected DateTime? _ValidStartDate;
		protected DateTime? _ValidEndDate;
		protected string _ConferUnit;
		protected string _ApplyStatus;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		protected DateTime? _ApplyDate;
		protected string _ApplyMan;
		protected string _ApplyCode;
		protected DateTime? _AcceptDate;
		protected string _GetResult;
		protected string _GetMan;
		protected string _GetCode;
		protected DateTime? _CheckDate;
		protected string _CheckResult;
		protected string _CheckMan;
		protected string _CheckCode;
		protected DateTime? _ConfrimDate;
		protected string _ConfrimResult;
		protected string _ConfrimMan;
		protected string _ConfrimCode;
        protected long? _CertificateID;
        protected string _AddPostID;
        protected string _CreditCode;
        protected DateTime? _NewUnitCheckTime;
        protected string _NewUnitAdvise;
        protected Byte? _SheBaoCheck;
        protected DateTime? _ZACheckTime;
        protected int? _ZACheckResult;
        protected string _ZACheckRemark;

        protected string _Job;
        protected string _SkillLevel;

        /// <summary>
        /// 安管人员在受聘企业担任的职务
        /// </summary>
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        /// <summary>
        /// 技术职称
        /// </summary>
        public string SkillLevel
        {
            get { return _SkillLevel; }
            set { _SkillLevel = value; }
        }
        
        /// <summary>
        /// 社保比对结果
        /// </summary>
        public Byte? SheBaoCheck
        {
            get { return _SheBaoCheck; }
            set { _SheBaoCheck = value; }
        }

        /// <summary>
        /// 现单位审核时间
        /// </summary>
        public DateTime? NewUnitCheckTime
        {
            get { return _NewUnitCheckTime; }
            set { _NewUnitCheckTime = value; }
        }

        /// <summary>
        /// 现单位审核意见
        /// </summary>
        public string NewUnitAdvise
        {
            get { return _NewUnitAdvise; }
            set { _NewUnitAdvise = value; }
        }

        /// <summary>
        /// 统一信用代码
        /// </summary>
        public string CreditCode
        {
            get { return _CreditCode; }
            set { _CreditCode = value; }
        }

		public long? ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}

		public long? WorkerID
		{
			get {return _WorkerID;}
			set {_WorkerID = value;}
		}

		public int? PostTypeID
		{
			get {return _PostTypeID;}
			set {_PostTypeID = value;}
		}

		public int? PostID
		{
			get {return _PostID;}
			set {_PostID = value;}
		}

		public string WorkerName
		{
			get {return _WorkerName;}
			set {_WorkerName = value;}
		}

		public string Sex
		{
			get {return _Sex;}
			set {_Sex = value;}
		}

		public DateTime? Birthday
		{
			get {return _Birthday;}
			set {_Birthday = value;}
		}

		public string WorkerCertificateCode
		{
			get {return _WorkerCertificateCode;}
			set {_WorkerCertificateCode = value;}
		}

		public string OldUnitName
		{
			get {return _OldUnitName;}
			set {_OldUnitName = value;}
		}

		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}

		public string Phone
		{
			get {return _Phone;}
			set {_Phone = value;}
		}

		public string CertificateCode
		{
			get {return _CertificateCode;}
			set {_CertificateCode = value;}
		}

		public DateTime? ConferDate
		{
			get {return _ConferDate;}
			set {_ConferDate = value;}
		}

		public DateTime? ValidStartDate
		{
			get {return _ValidStartDate;}
			set {_ValidStartDate = value;}
		}

		public DateTime? ValidEndDate
		{
			get {return _ValidEndDate;}
			set {_ValidEndDate = value;}
		}

		public string ConferUnit
		{
			get {return _ConferUnit;}
			set {_ConferUnit = value;}
		}

		public string ApplyStatus
		{
			get {return _ApplyStatus;}
			set {_ApplyStatus = value;}
		}

		public long? CreatePersonID
		{
			get {return _CreatePersonID;}
			set {_CreatePersonID = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public long? ModifyPersonID
		{
			get {return _ModifyPersonID;}
			set {_ModifyPersonID = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}

		public DateTime? ApplyDate
		{
			get {return _ApplyDate;}
			set {_ApplyDate = value;}
		}

		public string ApplyMan
		{
			get {return _ApplyMan;}
			set {_ApplyMan = value;}
		}

		public string ApplyCode
		{
			get {return _ApplyCode;}
			set {_ApplyCode = value;}
		}

		public DateTime? AcceptDate
		{
			get {return _AcceptDate;}
			set {_AcceptDate = value;}
		}

		public string GetResult
		{
			get {return _GetResult;}
			set {_GetResult = value;}
		}

		public string GetMan
		{
			get {return _GetMan;}
			set {_GetMan = value;}
		}

		public string GetCode
		{
			get {return _GetCode;}
			set {_GetCode = value;}
		}

		public DateTime? CheckDate
		{
			get {return _CheckDate;}
			set {_CheckDate = value;}
		}

		public string CheckResult
		{
			get {return _CheckResult;}
			set {_CheckResult = value;}
		}

		public string CheckMan
		{
			get {return _CheckMan;}
			set {_CheckMan = value;}
		}

		public string CheckCode
		{
			get {return _CheckCode;}
			set {_CheckCode = value;}
		}

		public DateTime? ConfrimDate
		{
			get {return _ConfrimDate;}
			set {_ConfrimDate = value;}
		}

		public string ConfrimResult
		{
			get {return _ConfrimResult;}
			set {_ConfrimResult = value;}
		}

		public string ConfrimMan
		{
			get {return _ConfrimMan;}
			set {_ConfrimMan = value;}
		}

		public string ConfrimCode
		{
			get {return _ConfrimCode;}
			set {_ConfrimCode = value;}
		}
        public long? CertificateID
        {
            get { return _CertificateID; }
            set { _CertificateID = value; }
        }

        /// <summary>
        /// 增项工种集合
        /// </summary>
        public string AddPostID
        {
            get { return _AddPostID; }
            set { _AddPostID = value; }
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
