using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyRenewMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyRenewMDL
	{
		public ApplyRenewMDL()
		{			
		}
			
		//主键
		protected string _ApplyID;
		
		//其它属性
		protected string _PSN_Telephone;
		protected string _PSN_MobilePhone;
		protected string _PSN_Email;
		protected string _OldRegisterNo;
		protected string _OldRegisterCertificateNo;
		protected DateTime? _DisnableDate;
		protected string _DisnableReason;
		protected string _FR;
		protected string _LinkMan;
		protected string _ENT_Telephone;
		protected string _ENT_Correspondence;
		protected string _ENT_Economic_Nature;
		protected string _END_Addess;
		protected string _ENT_Postcode;
		protected string _ENT_Type;
		protected string _ENT_Sort;
		protected string _ENT_Grade;
		protected string _ENT_QualificationCertificateNo;
		protected string _ENT_Sort2;
		protected string _ENT_Grade2;
		protected string _ENT_QualificationCertificateNo2;
		protected string _ExamInfo;
		protected string _OtherCert;
        protected string _Nation;
		
		public string ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}

		public string PSN_Telephone
		{
			get {return _PSN_Telephone;}
			set {_PSN_Telephone = value;}
		}

		public string PSN_MobilePhone
		{
			get {return _PSN_MobilePhone;}
			set {_PSN_MobilePhone = value;}
		}

		public string PSN_Email
		{
			get {return _PSN_Email;}
			set {_PSN_Email = value;}
		}

		public string OldRegisterNo
		{
			get {return _OldRegisterNo;}
			set {_OldRegisterNo = value;}
		}

		public string OldRegisterCertificateNo
		{
			get {return _OldRegisterCertificateNo;}
			set {_OldRegisterCertificateNo = value;}
		}

		public DateTime? DisnableDate
		{
			get {return _DisnableDate;}
			set {_DisnableDate = value;}
		}

		public string DisnableReason
		{
			get {return _DisnableReason;}
			set {_DisnableReason = value;}
		}

		public string FR
		{
			get {return _FR;}
			set {_FR = value;}
		}

		public string LinkMan
		{
			get {return _LinkMan;}
			set {_LinkMan = value;}
		}

		public string ENT_Telephone
		{
			get {return _ENT_Telephone;}
			set {_ENT_Telephone = value;}
		}

		public string ENT_Correspondence
		{
			get {return _ENT_Correspondence;}
			set {_ENT_Correspondence = value;}
		}

		public string ENT_Economic_Nature
		{
			get {return _ENT_Economic_Nature;}
			set {_ENT_Economic_Nature = value;}
		}

		public string END_Addess
		{
			get {return _END_Addess;}
			set {_END_Addess = value;}
		}

		public string ENT_Postcode
		{
			get {return _ENT_Postcode;}
			set {_ENT_Postcode = value;}
		}

		public string ENT_Type
		{
			get {return _ENT_Type;}
			set {_ENT_Type = value;}
		}

		public string ENT_Sort
		{
			get {return _ENT_Sort;}
			set {_ENT_Sort = value;}
		}

		public string ENT_Grade
		{
			get {return _ENT_Grade;}
			set {_ENT_Grade = value;}
		}

		public string ENT_QualificationCertificateNo
		{
			get {return _ENT_QualificationCertificateNo;}
			set {_ENT_QualificationCertificateNo = value;}
		}

		public string ENT_Sort2
		{
			get {return _ENT_Sort2;}
			set {_ENT_Sort2 = value;}
		}

		public string ENT_Grade2
		{
			get {return _ENT_Grade2;}
			set {_ENT_Grade2 = value;}
		}

		public string ENT_QualificationCertificateNo2
		{
			get {return _ENT_QualificationCertificateNo2;}
			set {_ENT_QualificationCertificateNo2 = value;}
		}

		public string ExamInfo
		{
			get {return _ExamInfo;}
			set {_ExamInfo = value;}
		}

		public string OtherCert
		{
			get {return _OtherCert;}
			set {_OtherCert = value;}
		}

        public string Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }
	}
}
