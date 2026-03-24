using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamSubjectResultOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamSubjectResultOB
	{
		public ExamSubjectResultOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamSubjectResultID;
		
		//其它属性
		protected string _ExamCardID;
		protected long? _ExamPlanID;
		protected int? _PostID;
        protected decimal? _SubjectiveTopicScore;
        protected decimal? _ObjectiveTopicScore;
        protected decimal? _SumScore;
		protected string _ExamStatus;
		protected string _Status;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		
		public long? ExamSubjectResultID
		{
			get {return _ExamSubjectResultID;}
			set {_ExamSubjectResultID = value;}
		}

		public string ExamCardID
		{
			get {return _ExamCardID;}
			set {_ExamCardID = value;}
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

        public decimal? SubjectiveTopicScore
		{
			get {return _SubjectiveTopicScore;}
			set {_SubjectiveTopicScore = value;}
		}

        public decimal? ObjectiveTopicScore
		{
			get {return _ObjectiveTopicScore;}
			set {_ObjectiveTopicScore = value;}
		}

        public decimal? SumScore
		{
			get {return _SumScore;}
			set {_SumScore = value;}
		}

		public string ExamStatus
		{
			get {return _ExamStatus;}
			set {_ExamStatus = value;}
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
