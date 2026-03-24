using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--zjs_ApplyContinueMDL填写类描述
	/// </summary>
	[Serializable]
	public class zjs_ApplyContinueMDL
	{
		public zjs_ApplyContinueMDL()
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
		protected DateTime? _PSN_CertificateValidity;
		protected int? _BiXiu;
		protected int? _XuanXiu;
		protected string _Remark;
        protected string _FR;
		
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

		public DateTime? PSN_CertificateValidity
		{
			get {return _PSN_CertificateValidity;}
			set {_PSN_CertificateValidity = value;}
		}

		public int? BiXiu
		{
			get {return _BiXiu;}
			set {_BiXiu = value;}
		}

		public int? XuanXiu
		{
			get {return _XuanXiu;}
			set {_XuanXiu = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

        public string FR
        {
            get { return _FR; }
            set { _FR = value; }
        }
	}
}
