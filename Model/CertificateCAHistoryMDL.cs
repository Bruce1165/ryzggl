using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateCAHistoryMDL填写类描述
	/// </summary>
	[Serializable]
	public class CertificateCAHistoryMDL
	{
		public CertificateCAHistoryMDL()
		{			
		}
			
		//主键
		protected string _CertificateCAID;
		
		//其它属性
		protected DateTime? _ApplyCATime;
		protected DateTime? _SendCATime;
		protected DateTime? _ReturnCATime;
		protected long? _CertificateID;
		
		public string CertificateCAID
		{
			get {return _CertificateCAID;}
			set {_CertificateCAID = value;}
		}

		public DateTime? ApplyCATime
		{
			get {return _ApplyCATime;}
			set {_ApplyCATime = value;}
		}

		public DateTime? SendCATime
		{
			get {return _SendCATime;}
			set {_SendCATime = value;}
		}

		public DateTime? ReturnCATime
		{
			get {return _ReturnCATime;}
			set {_ReturnCATime = value;}
		}

		public long? CertificateID
		{
			get {return _CertificateID;}
			set {_CertificateID = value;}
		}
	}
}
