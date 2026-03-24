using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--Out_CheckCFZCMDL填写类描述
	/// </summary>
	[Serializable]
	public class Out_CheckCFZCMDL
	{
		public Out_CheckCFZCMDL()
		{			
		}
			
		//主键
		protected string _ID;
		
		//其它属性
		protected string _UserName;
		protected string _Provice;
		protected string _IDCard;
		protected string _UnitName;
		protected string _RegType;
		protected string _CertCode;
		protected string _PublishDate;
		protected string _ValidEnd;
		protected string _PublishBy;
		protected string _Sex;
		protected string _Birthday;
		protected string _Remark;
		protected string _Question;
		protected DateTime? _CreateDate;
		
		public string ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string UserName
		{
			get {return _UserName;}
			set {_UserName = value;}
		}

		public string Provice
		{
			get {return _Provice;}
			set {_Provice = value;}
		}

		public string IDCard
		{
			get {return _IDCard;}
			set {_IDCard = value;}
		}

		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

		public string RegType
		{
			get {return _RegType;}
			set {_RegType = value;}
		}

		public string CertCode
		{
			get {return _CertCode;}
			set {_CertCode = value;}
		}

		public string PublishDate
		{
			get {return _PublishDate;}
			set {_PublishDate = value;}
		}

		public string ValidEnd
		{
			get {return _ValidEnd;}
			set {_ValidEnd = value;}
		}

		public string PublishBy
		{
			get {return _PublishBy;}
			set {_PublishBy = value;}
		}

		public string Sex
		{
			get {return _Sex;}
			set {_Sex = value;}
		}

		public string Birthday
		{
			get {return _Birthday;}
			set {_Birthday = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

		public string Question
		{
			get {return _Question;}
			set {_Question = value;}
		}

		public DateTime? CreateDate
		{
			get {return _CreateDate;}
			set {_CreateDate = value;}
		}
	}
}
