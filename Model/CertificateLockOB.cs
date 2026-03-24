using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 证书锁定对象
	/// </summary>
	[Serializable]
	public class CertificateLockOB
	{
		public CertificateLockOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _LockID;
		
		//其它属性
		protected long? _CertificateID;
		protected string _LockType;
		protected DateTime? _LockTime;
		protected DateTime? _LockEndTime;
		protected string _LockPerson;
		protected string _Remark;
		protected DateTime? _UnlockTime;
		protected string _UnlockPerson;
		protected string _LockStatus;
		
		public long? LockID
		{
			get {return _LockID;}
			set {_LockID = value;}
		}

		public long? CertificateID
		{
			get {return _CertificateID;}
			set {_CertificateID = value;}
		}

		public string LockType
		{
			get {return _LockType;}
			set {_LockType = value;}
		}

		public DateTime? LockTime
		{
			get {return _LockTime;}
			set {_LockTime = value;}
		}

		public DateTime? LockEndTime
		{
			get {return _LockEndTime;}
			set {_LockEndTime = value;}
		}

		public string LockPerson
		{
			get {return _LockPerson;}
			set {_LockPerson = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

		public DateTime? UnlockTime
		{
			get {return _UnlockTime;}
			set {_UnlockTime = value;}
		}

		public string UnlockPerson
		{
			get {return _UnlockPerson;}
			set {_UnlockPerson = value;}
		}

		public string LockStatus
		{
			get {return _LockStatus;}
			set {_LockStatus = value;}
		}
	}
}
