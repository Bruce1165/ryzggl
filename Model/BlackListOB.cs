using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--BlackListOB填写类描述
	/// </summary>
	[Serializable]
	public class BlackListOB
	{
		public BlackListOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _BlackListID;
		
		//其它属性
		protected int? _PostTypeID;
		protected int? _PostID;
		protected string _WorkerName;
		protected string _CertificateCode;
		protected string _UnitName;
		protected string _UnitCode;
		protected string _TrainUnitName;
		protected string _BlackType;
		protected DateTime? _StartTime;
		protected string _BlackStatus;
		protected string _Remark;
		protected string _CreatePerson;
		protected DateTime? _CreateTime;
		protected string _ModifyPerson;
		protected DateTime? _ModifyTime;
		
		public long? BlackListID
		{
			get {return _BlackListID;}
			set {_BlackListID = value;}
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

		public string WorkerName
		{
			get {return _WorkerName;}
			set {_WorkerName = value;}
		}

		public string CertificateCode
		{
			get {return _CertificateCode;}
			set {_CertificateCode = value;}
		}

		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}

		public string TrainUnitName
		{
			get {return _TrainUnitName;}
			set {_TrainUnitName = value;}
		}

		public string BlackType
		{
			get {return _BlackType;}
			set {_BlackType = value;}
		}

		public DateTime? StartTime
		{
			get {return _StartTime;}
			set {_StartTime = value;}
		}

		public string BlackStatus
		{
			get {return _BlackStatus;}
			set {_BlackStatus = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

		public string CreatePerson
		{
			get {return _CreatePerson;}
			set {_CreatePerson = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public string ModifyPerson
		{
			get {return _ModifyPerson;}
			set {_ModifyPerson = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
	}
}
