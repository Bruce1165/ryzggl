using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateMoreMDL填写类描述
	/// </summary>
	[Serializable]
	public class CertificateMoreMDL
	{
		public CertificateMoreMDL()
		{			
		}
			
		//主键
		protected long? _ApplyID;
		
		//其它属性
		protected long? _CertificateID;
		protected string _CertificateCode;
        protected DateTime? _ValidStartDate;
		protected DateTime? _ValidEndDate;
        protected DateTime? _ValidStartDateMore;
        protected DateTime? _ValidEndDateMore;
        protected string _WorkerName;
		protected string _WorkerCertificateCode;
        protected string _Sex;
        protected DateTime? _Birthday;
        protected string _PeoplePhone;
		protected string _UnitName;
		protected string _UnitCode;
		protected string _UnitNameMore;
		protected string _UnitCodeMore;
		protected string _CreatePerson;
		protected DateTime? _CreateTime;
		protected string _ModifyPerson;
		protected DateTime? _ModifyTime;
		protected string _CheckMan;
		protected string _CheckAdvise;
		protected DateTime? _CheckDate;
		protected string _CertificateCodeMore;
		protected string _ApplyStatus;
		protected string _ValID;
        protected string _CreditCode;
        protected DateTime? _NewUnitCheckTime;
        protected string _NewUnitAdvise;
        protected string _ConfirmMan;
        protected string _ConfirmAdvise;
        protected DateTime? _ConfirmDate;

        /// <summary>
        /// 增发单位审核时间
        /// </summary>
        public DateTime? NewUnitCheckTime
        {
            get { return _NewUnitCheckTime; }
            set { _NewUnitCheckTime = value; }
        }

        /// <summary>
        /// 增发单位审核意见
        /// </summary>
        public string NewUnitAdvise
        {
            get { return _NewUnitAdvise; }
            set { _NewUnitAdvise = value; }
        }

        /// <summary>
        /// 增发单位统一信用代码
        /// </summary>
        public string CreditCode
        {
            get { return _CreditCode; }
            set { _CreditCode = value; }
        }
		
        /// <summary>
        /// 申请ID（自增）
        /// </summary>
		public long? ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}
        /// <summary>
        /// 证书ID
        /// </summary>
		public long? CertificateID
		{
			get {return _CertificateID;}
			set {_CertificateID = value;}
		}
        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode
		{
			get {return _CertificateCode;}
			set {_CertificateCode = value;}
		}
        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime? ValidStartDate
        {
            get { return _ValidStartDate; }
            set { _ValidStartDate = value; }
        }
        /// <summary>
        /// 有效期至
        /// </summary>
		public DateTime? ValidEndDate
		{
			get {return _ValidEndDate;}
			set {_ValidEndDate = value;}
		}
        /// <summary>
        /// 增发A本发证日期
        /// </summary>
        public DateTime? ValidStartDateMore
        {
            get { return _ValidStartDateMore; }
            set { _ValidStartDateMore = value; }
        }
        /// <summary>
        /// 增发A本有效期至
        /// </summary>
		public DateTime? ValidEndDateMore
        {
            get { return _ValidEndDateMore; }
            set { _ValidEndDateMore = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName
		{
			get {return _WorkerName;}
			set {_WorkerName = value;}
		}
        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode
		{
			get {return _WorkerCertificateCode;}
			set {_WorkerCertificateCode = value;}
		}
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PeoplePhone
        {
            get { return _PeoplePhone; }
            set { _PeoplePhone = value; }
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}
        /// <summary>
        /// 组织机构代码
        /// </summary>
		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}
        /// <summary>
        /// 增发工作单位
        /// </summary>
		public string UnitNameMore
		{
			get {return _UnitNameMore;}
			set {_UnitNameMore = value;}
		}
        /// <summary>
        /// 增发组织机构代码
        /// </summary>
		public string UnitCodeMore
		{
			get {return _UnitCodeMore;}
			set {_UnitCodeMore = value;}
		}
        /// <summary>
        /// 创建人
        /// </summary>
		public string CreatePerson
		{
			get {return _CreatePerson;}
			set {_CreatePerson = value;}
		}
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}
        /// <summary>
        /// 最后修改人
        /// </summary>
		public string ModifyPerson
		{
			get {return _ModifyPerson;}
			set {_ModifyPerson = value;}
		}
        /// <summary>
        /// 最后修改时间
        /// </summary>
		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
        /// <summary>
        /// 审批人
        /// </summary>
		public string CheckMan
		{
			get {return _CheckMan;}
			set {_CheckMan = value;}
		}
        /// <summary>
        /// 审批意见
        /// </summary>
		public string CheckAdvise
		{
			get {return _CheckAdvise;}
			set {_CheckAdvise = value;}
		}

        /// <summary>
        /// 审批时间
        /// </summary>
		public DateTime? CheckDate
		{
			get {return _CheckDate;}
			set {_CheckDate = value;}
		}

        /// <summary>
        /// 增发证书编号
        /// </summary>
		public string CertificateCodeMore
		{
			get {return _CertificateCodeMore;}
			set {_CertificateCodeMore = value;}
		}

        /// <summary>
        /// 申请状态：已申请，已审核，已决定，退回个人
        /// </summary>
		public string ApplyStatus
		{
			get {return _ApplyStatus;}
			set {_ApplyStatus = value;}
		}

        /// <summary>
        /// 是否为有效数据
        /// </summary>
		public string ValID
		{
			get {return _ValID; }
			set { _ValID = value;}
		}
        /// <summary>
        /// 决定人
        /// </summary>
        public string ConfirmMan
        {
            get { return _ConfirmMan; }
            set { _ConfirmMan = value; }
        }
        /// <summary>
        /// 决定意见
        /// </summary>
        public string ConfirmAdvise
        {
            get { return _ConfirmAdvise; }
            set { _ConfirmAdvise = value; }
        }
        /// <summary>
        /// 决定时间
        /// </summary>
        public DateTime? ConfirmDate
        {
            get { return _ConfirmDate; }
            set { _ConfirmDate = value; }
        }
	}
}
