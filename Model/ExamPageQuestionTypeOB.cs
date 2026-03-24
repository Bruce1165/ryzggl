using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamPageQuestionTypeOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamPageQuestionTypeOB
	{
		public ExamPageQuestionTypeOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamPageID;
		protected string _QuestionType;
		
		//其它属性
		protected int? _QuestionCount;
		protected int? _Score;
		protected string _Remark;
		protected int? _ShowOrder;
		
		public long? ExamPageID
		{
			get {return _ExamPageID;}
			set {_ExamPageID = value;}
		}

		public string QuestionType
		{
			get {return _QuestionType;}
			set {_QuestionType = value;}
		}

		public int? QuestionCount
		{
			get {return _QuestionCount;}
			set {_QuestionCount = value;}
		}

		public int? Score
		{
			get {return _Score;}
			set {_Score = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

		public int? ShowOrder
		{
			get {return _ShowOrder;}
			set {_ShowOrder = value;}
		}
	}
}
