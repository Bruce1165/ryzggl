using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamRangeOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamRangeOB
	{
		public ExamRangeOB()
		{			
			//默认值
		}
			
		//主键
		protected short? _ExamYear;
		protected int? _SubjectID;
		
		//其它属性
		protected byte? _Flag;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		protected string _Remark;
		
		public short? ExamYear
		{
			get {return _ExamYear;}
			set {_ExamYear = value;}
		}

		public int? SubjectID
		{
			get {return _SubjectID;}
			set {_SubjectID = value;}
		}

		public byte? Flag
		{
			get {return _Flag;}
			set {_Flag = value;}
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

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}
	}
}
