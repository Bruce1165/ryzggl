using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyReplaceMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyReplaceMDL
	{
		public ApplyReplaceMDL()
		{			
		}
			
		//主键
		protected string _ApplyID;
		
		//其它属性
		protected string _PSN_MobilePhone;
		protected string _PSN_Email;
		protected string _RegisterNo;
		protected string _RegisterCertificateNo;
		protected DateTime? _DisnableDate;
		protected string _ValidCode;
		protected string _LinkMan;
		protected string _ENT_Telephone;
		protected string _ENT_Correspondence;
		protected string _ENT_Postcode;
		protected string _PSN_RegisteProfession1;
		protected DateTime? _PSN_CertificateValidity1;
		protected string _PSN_RegisteProfession2;
		protected DateTime? _PSN_CertificateValidity2;
		protected string _PSN_RegisteProfession3;
		protected DateTime? _PSN_CertificateValidity3;
		protected DateTime? _PSN_CertificateValidity4;
		protected string _PSN_RegisteProfession4;
		protected string _ReplaceReason;
		protected string _ReplaceType;
		
		public string ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
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

		public string RegisterNo
		{
			get {return _RegisterNo;}
			set {_RegisterNo = value;}
		}

		public string RegisterCertificateNo
		{
			get {return _RegisterCertificateNo;}
			set {_RegisterCertificateNo = value;}
		}

		public DateTime? DisnableDate
		{
			get {return _DisnableDate;}
			set {_DisnableDate = value;}
		}

		public string ValidCode
		{
			get {return _ValidCode;}
			set {_ValidCode = value;}
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

		public string ENT_Postcode
		{
			get {return _ENT_Postcode;}
			set {_ENT_Postcode = value;}
		}

		public string PSN_RegisteProfession1
		{
			get {return _PSN_RegisteProfession1;}
			set {_PSN_RegisteProfession1 = value;}
		}

		public DateTime? PSN_CertificateValidity1
		{
			get {return _PSN_CertificateValidity1;}
			set {_PSN_CertificateValidity1 = value;}
		}

		public string PSN_RegisteProfession2
		{
			get {return _PSN_RegisteProfession2;}
			set {_PSN_RegisteProfession2 = value;}
		}

		public DateTime? PSN_CertificateValidity2
		{
			get {return _PSN_CertificateValidity2;}
			set {_PSN_CertificateValidity2 = value;}
		}

		public string PSN_RegisteProfession3
		{
			get {return _PSN_RegisteProfession3;}
			set {_PSN_RegisteProfession3 = value;}
		}

		public DateTime? PSN_CertificateValidity3
		{
			get {return _PSN_CertificateValidity3;}
			set {_PSN_CertificateValidity3 = value;}
		}

		public DateTime? PSN_CertificateValidity4
		{
			get {return _PSN_CertificateValidity4;}
			set {_PSN_CertificateValidity4 = value;}
		}

		public string PSN_RegisteProfession4
		{
			get {return _PSN_RegisteProfession4;}
			set {_PSN_RegisteProfession4 = value;}
		}

		public string ReplaceReason
		{
			get {return _ReplaceReason;}
			set {_ReplaceReason = value;}
		}

		public string ReplaceType
		{
			get {return _ReplaceType;}
			set {_ReplaceType = value;}
		}
	}
}
