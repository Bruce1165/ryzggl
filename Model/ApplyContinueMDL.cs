using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyContinueMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyContinueMDL
	{
		public ApplyContinueMDL()
		{			
		}
			
		//主键
		protected string _ApplyID;
		
		//其它属性
		protected string _PSN_MobilePhone;
		protected string _PSN_Email;
		protected string _LinkMan;
		protected string _ENT_Telephone;
		protected string _ENT_MobilePhone;
		protected string _ENT_Correspondence;
		protected string _ENT_Economic_Nature;
		protected string _END_Addess;
		protected string _ENT_Postcode;
		protected string _PSN_RegisterCertificateNo;
		protected string _PSN_RegisteProfession1;
		protected DateTime? _PSN_CertificateValidity1;
		protected bool? _IfContinue1;
		protected int? _BiXiu1;
		protected int? _XuanXiu1;
		protected string _Remark1;
		protected string _PSN_RegisteProfession2;
		protected DateTime? _PSN_CertificateValidity2;
		protected bool? _IfContinue2;
		protected int? _BiXiu2;
		protected int? _XuanXiu2;
		protected string _Remark2;
		protected string _PSN_RegisteProfession3;
		protected DateTime? _PSN_CertificateValidity3;
		protected bool? _IfContinue3;
		protected int? _BiXiu3;
		protected int? _XuanXiu3;
		protected string _Remark3;
		protected string _PSN_RegisteProfession4;
		protected DateTime? _PSN_CertificateValidity4;
		protected bool? _IfContinue4;
		protected int? _BiXiu4;
		protected int? _XuanXiu4;
		protected string _Remark4;
		protected string _MainJob;
		protected string _OtherCert;
        protected string _Nation;
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

		public string ENT_MobilePhone
		{
			get {return _ENT_MobilePhone;}
			set {_ENT_MobilePhone = value;}
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

		public string PSN_RegisterCertificateNo
		{
			get {return _PSN_RegisterCertificateNo;}
			set {_PSN_RegisterCertificateNo = value;}
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

		public bool? IfContinue1
		{
			get {return _IfContinue1;}
			set {_IfContinue1 = value;}
		}

		public int? BiXiu1
		{
			get {return _BiXiu1;}
			set {_BiXiu1 = value;}
		}

		public int? XuanXiu1
		{
			get {return _XuanXiu1;}
			set {_XuanXiu1 = value;}
		}

		public string Remark1
		{
			get {return _Remark1;}
			set {_Remark1 = value;}
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

		public bool? IfContinue2
		{
			get {return _IfContinue2;}
			set {_IfContinue2 = value;}
		}

		public int? BiXiu2
		{
			get {return _BiXiu2;}
			set {_BiXiu2 = value;}
		}

		public int? XuanXiu2
		{
			get {return _XuanXiu2;}
			set {_XuanXiu2 = value;}
		}

		public string Remark2
		{
			get {return _Remark2;}
			set {_Remark2 = value;}
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

		public bool? IfContinue3
		{
			get {return _IfContinue3;}
			set {_IfContinue3 = value;}
		}

		public int? BiXiu3
		{
			get {return _BiXiu3;}
			set {_BiXiu3 = value;}
		}

		public int? XuanXiu3
		{
			get {return _XuanXiu3;}
			set {_XuanXiu3 = value;}
		}

		public string Remark3
		{
			get {return _Remark3;}
			set {_Remark3 = value;}
		}

		public string PSN_RegisteProfession4
		{
			get {return _PSN_RegisteProfession4;}
			set {_PSN_RegisteProfession4 = value;}
		}

		public DateTime? PSN_CertificateValidity4
		{
			get {return _PSN_CertificateValidity4;}
			set {_PSN_CertificateValidity4 = value;}
		}

		public bool? IfContinue4
		{
			get {return _IfContinue4;}
			set {_IfContinue4 = value;}
		}

		public int? BiXiu4
		{
			get {return _BiXiu4;}
			set {_BiXiu4 = value;}
		}

		public int? XuanXiu4
		{
			get {return _XuanXiu4;}
			set {_XuanXiu4 = value;}
		}

		public string Remark4
		{
			get {return _Remark4;}
			set {_Remark4 = value;}
		}

		public string MainJob
		{
			get {return _MainJob;}
			set {_MainJob = value;}
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
