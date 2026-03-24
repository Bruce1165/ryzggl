using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--PageQuestionOB填写类描述
	/// </summary>
	[Serializable]
	public class PageQuestionOB
	{
		public PageQuestionOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamPageID;
		protected long? _QuestionID;
		
		//其它属性
		protected string _QuestionType;
		protected string _Title;
		protected string _Answer;
		protected int? _QuestionNo;
		protected int? _TagCode;
		protected byte? _Difficulty;
		
		public long? ExamPageID
		{
			get {return _ExamPageID;}
			set {_ExamPageID = value;}
		}

		public long? QuestionID
		{
			get {return _QuestionID;}
			set {_QuestionID = value;}
		}

		public string QuestionType
		{
			get {return _QuestionType;}
			set {_QuestionType = value;}
		}

		public string Title
		{
			get {return _Title;}
			set {_Title = value;}
		}

		public string Answer
		{
			get {return _Answer;}
			set {_Answer = value;}
		}

        public int? QuestionNo
		{
			get {return _QuestionNo;}
			set {_QuestionNo = value;}
		}

		public int? TagCode
		{
			get {return _TagCode;}
			set {_TagCode = value;}
		}

		public byte? Difficulty
		{
			get {return _Difficulty;}
			set {_Difficulty = value;}
		}
	}
}
