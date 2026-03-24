using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateAddItemOB填写类描述
	/// </summary>
	[Serializable]
	public class CertificateAddItemOB
	{
		public CertificateAddItemOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _CertificateAddItemID;
		
		//其它属性
		protected long? _CertificateID;
		protected int? _PostTypeID;
		protected int? _PostID;
        protected string _CreatePerson;
		protected DateTime? _CreateTime;
		protected string _CaseStatus;
		
		public long? CertificateAddItemID
		{
			get {return _CertificateAddItemID;}
			set {_CertificateAddItemID = value;}
		}

		public long? CertificateID
		{
			get {return _CertificateID;}
			set {_CertificateID = value;}
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

        public string CreatePerson
		{
            get { return _CreatePerson; }
            set { _CreatePerson = value; }
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public string CaseStatus
		{
			get {return _CaseStatus;}
			set {_CaseStatus = value;}
		}
	}
}
