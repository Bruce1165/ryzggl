using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamPlanSubjectOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamPlanSubjectOB
	{
		public ExamPlanSubjectOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamPlanSubjectID;
		
		//其它属性
		protected long? _ExamPlanID;
		protected int? _PostID;
		protected DateTime? _ExamStartTime;
		protected DateTime? _ExamEndTime;
		protected int? _PassLine;
		protected decimal? _ExamFee;
		protected string _Status;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		
		public long? ExamPlanSubjectID
		{
			get {return _ExamPlanSubjectID;}
			set {_ExamPlanSubjectID = value;}
		}

		public long? ExamPlanID
		{
			get {return _ExamPlanID;}
			set {_ExamPlanID = value;}
		}

		public int? PostID
		{
			get {return _PostID;}
			set {_PostID = value;}
		}

		public DateTime? ExamStartTime
		{
			get {return _ExamStartTime;}
			set {_ExamStartTime = value;}
		}

		public DateTime? ExamEndTime
		{
			get {return _ExamEndTime;}
			set {_ExamEndTime = value;}
		}

		public int? PassLine
		{
			get {return _PassLine;}
			set {_PassLine = value;}
		}

		public decimal? ExamFee
		{
			get {return _ExamFee;}
			set {_ExamFee = value;}
		}

		public string Status
		{
			get {return _Status;}
			set {_Status = value;}
		}

		public long? CreatePersonID
		{
			get {return _CreatePersonID;}
			set {_CreatePersonID = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public long? ModifyPersonID
		{
			get {return _ModifyPersonID;}
			set {_ModifyPersonID = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
	}
}
