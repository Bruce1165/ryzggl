using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--TJ_MissExamLockMDL填写类描述
	/// </summary>
	[Serializable]
	public class TJ_MissExamLockMDL
	{
		public TJ_MissExamLockMDL()
		{			
		}
			
		//主键
		protected string _WorkerCertificateCode;
		protected DateTime? _LockStartDate;
		
		//其它属性
		protected string _WorkerName;
		protected DateTime? _FirstExamDate;
		protected DateTime? _LockEndDate;
		
		public string WorkerCertificateCode
		{
			get {return _WorkerCertificateCode;}
			set {_WorkerCertificateCode = value;}
		}

		public string WorkerName
		{
			get {return _WorkerName;}
			set {_WorkerName = value;}
		}

		public DateTime? FirstExamDate
		{
			get {return _FirstExamDate;}
			set {_FirstExamDate = value;}
		}

		public DateTime? LockStartDate
		{
			get {return _LockStartDate;}
			set {_LockStartDate = value;}
		}

		public DateTime? LockEndDate
		{
			get {return _LockEndDate;}
			set {_LockEndDate = value;}
		}
	}
}
