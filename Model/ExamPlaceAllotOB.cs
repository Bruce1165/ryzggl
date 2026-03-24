using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamPlaceAllotOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamPlaceAllotOB
	{
		public ExamPlaceAllotOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamPlaceAllotID;
		
		//其它属性
		protected long? _ExamPlanID;
		protected long? _ExamPlaceID;
		protected string _ExamPlaceName;
		protected int? _RoomNum;
		protected int? _ExamPersonNum;
		protected string _Status;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		
		public long? ExamPlaceAllotID
		{
			get {return _ExamPlaceAllotID;}
			set {_ExamPlaceAllotID = value;}
		}

		public long? ExamPlanID
		{
			get {return _ExamPlanID;}
			set {_ExamPlanID = value;}
		}

		public long? ExamPlaceID
		{
			get {return _ExamPlaceID;}
			set {_ExamPlaceID = value;}
		}

		public string ExamPlaceName
		{
			get {return _ExamPlaceName;}
			set {_ExamPlaceName = value;}
		}

		public int? RoomNum
		{
			get {return _RoomNum;}
			set {_RoomNum = value;}
		}

		public int? ExamPersonNum
		{
			get {return _ExamPersonNum;}
			set {_ExamPersonNum = value;}
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
