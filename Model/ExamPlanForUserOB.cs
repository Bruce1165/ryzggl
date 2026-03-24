using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamPlanForUserOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamPlanForUserOB
	{
		public ExamPlanForUserOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _DataID;
		
		//其它属性
		protected long? _ExamPlanID;
		protected string _UserType;
		protected string _CertificateCode;
		protected string _UnitCode;
		protected long? _TrainUnitID;
		
		public long? DataID
		{
			get {return _DataID;}
			set {_DataID = value;}
		}

		public long? ExamPlanID
		{
			get {return _ExamPlanID;}
			set {_ExamPlanID = value;}
		}

		public string UserType
		{
			get {return _UserType;}
			set {_UserType = value;}
		}

		public string CertificateCode
		{
			get {return _CertificateCode;}
			set {_CertificateCode = value;}
		}

		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}

		public long? TrainUnitID
		{
			get {return _TrainUnitID;}
			set {_TrainUnitID = value;}
		}
	}
}
