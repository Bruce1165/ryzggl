using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyAddItemMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyAddItemMDL
	{
		public ApplyAddItemMDL()
		{			
		}
			
		//主键
		protected string _ApplyID;
		
		//其它属性
		protected string _PSN_MobilePhone;
		protected string _PSN_Email;
		protected string _ENT_Telephone;
		protected string _PSN_RegisterCertificateNo;
		protected string _PSN_RegisteProfession1;
		protected DateTime? _PSN_CertificateValidity1;
		protected string _PSN_RegisteProfession2;
		protected DateTime? _PSN_CertificateValidity2;
		protected string _PSN_RegisteProfession3;
		protected DateTime? _PSN_CertificateValidity3;
		protected string _AddItem1;
		protected string _ExamCode1;
		protected DateTime? _ExamDate1;
		protected int? _BiXiu1;
		protected int? _XuanXiu1;
		protected string _AddItem2;
		protected string _ExamCode2;
		protected DateTime? _ExamDate2;
		protected int? _BiXiu2;
		protected int? _XuanXiu2;
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

		public string ENT_Telephone
		{
			get {return _ENT_Telephone;}
			set {_ENT_Telephone = value;}
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

		public string AddItem1
		{
			get {return _AddItem1;}
			set {_AddItem1 = value;}
		}

		public string ExamCode1
		{
			get {return _ExamCode1;}
			set {_ExamCode1 = value;}
		}

		public DateTime? ExamDate1
		{
			get {return _ExamDate1;}
			set {_ExamDate1 = value;}
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

		public string AddItem2
		{
			get {return _AddItem2;}
			set {_AddItem2 = value;}
		}

		public string ExamCode2
		{
			get {return _ExamCode2;}
			set {_ExamCode2 = value;}
		}

		public DateTime? ExamDate2
		{
			get {return _ExamDate2;}
			set {_ExamDate2 = value;}
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
        public string Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }
	}
}
