using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ExamPlanOB填写类描述
	/// </summary>
	[Serializable]
	public class ExamPlanOB
	{
		public ExamPlanOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _ExamPlanID;
		
		//其它属性
		protected int? _PostTypeID;
		protected int? _PostID;
		protected DateTime? _SignUpStartDate;
		protected DateTime? _SignUpEndDate;
		protected DateTime? _ExamCardSendStartDate;
		protected DateTime? _ExamCardSendEndDate;
		protected DateTime? _ExamStartDate;
		protected DateTime? _ExamEndDate;
		protected string _SignUpPlace;
		protected string _Remark;
		protected string _Status;
		protected string _ExamPlanName;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
        protected string _PostTypeName;
        protected string _PostName;
        protected DateTime? _LatestCheckDate;
        protected DateTime? _LatestPayDate;
        protected decimal? _ExamFee;
        protected string _IfPublish;
        protected string _PlanSkillLevel;
        protected DateTime? _StartCheckDate;
        protected int? _PersonLimit;
        protected string _ExamWay;

        /// <summary>
        /// 考试方式：机考；网考；
        /// </summary>
        public string ExamWay
        {
            get { return _ExamWay; }
            set { _ExamWay = value; }
        }

        /// <summary>
        /// 考试报名最大人数限制
        /// </summary>
        public int? PersonLimit
        {
            get { return _PersonLimit; }
            set { _PersonLimit = value; }
        }
        /// <summary>
        /// 审核开始时间
        /// </summary>
        public DateTime? StartCheckDate
        {
            get { return _StartCheckDate; }
            set { _StartCheckDate = value; }
        }

        /// <summary>
        /// 技术等级
        /// </summary>
        public string PlanSkillLevel
        {
            get { return _PlanSkillLevel; }
            set { _PlanSkillLevel = value; }
        }
        /// <summary>
        /// 是否公开
        /// </summary>
        public string IfPublish
        {
            get { return _IfPublish; }
            set { _IfPublish = value; }
        }
        public decimal? ExamFee
        {
            get { return _ExamFee; }
            set { _ExamFee = value; }
        }

		public long? ExamPlanID
		{
			get {return _ExamPlanID;}
			set {_ExamPlanID = value;}
		}

        /// <summary>
        /// 1：安全生产考核三类人员
        /// 2：建筑施工特种作业
        /// 3：造价员
        /// 4：建设职业技能岗位
        /// 5：关键岗位专业技术管理人员
        /// 4000：新版建设职业技能岗位
        /// </summary>
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
        /// <summary>
        /// 报名开始日期
        /// </summary>
		public DateTime? SignUpStartDate
		{
			get {return _SignUpStartDate;}
			set {_SignUpStartDate = value;}
		}
        /// <summary>
        /// 报名结束日期
        /// </summary>
		public DateTime? SignUpEndDate
		{
			get {return _SignUpEndDate;}
			set {_SignUpEndDate = value;}
		}
        /// <summary>
        /// 准考证发放开始日期
        /// </summary>
		public DateTime? ExamCardSendStartDate
		{
			get {return _ExamCardSendStartDate;}
			set {_ExamCardSendStartDate = value;}
		}
        /// <summary>
        /// 准考证发放结束日期
        /// </summary>
		public DateTime? ExamCardSendEndDate
		{
			get {return _ExamCardSendEndDate;}
			set {_ExamCardSendEndDate = value;}
		}
        /// <summary>
        /// 考试开始日期
        /// </summary>
		public DateTime? ExamStartDate
		{
			get {return _ExamStartDate;}
			set {_ExamStartDate = value;}
		}
        /// <summary>
        /// 考试结束日期
        /// </summary>
		public DateTime? ExamEndDate
		{
			get {return _ExamEndDate;}
			set {_ExamEndDate = value;}
		}
        /// <summary>
        /// 报名地点(废弃)
        /// </summary>
		public string SignUpPlace
		{
			get {return _SignUpPlace;}
			set {_SignUpPlace = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}
        /// <summary>
        /// 报名状态
        /// </summary>
		public string Status
		{
			get {return _Status;}
			set {_Status = value;}
		}
        /// <summary>
        /// 考试计划名称
        /// </summary>
		public string ExamPlanName
		{
			get {return _ExamPlanName;}
			set {_ExamPlanName = value;}
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
        /// <summary>
        /// 岗位类别
        /// </summary>
        public string PostTypeName
        {
            get { return _PostTypeName; }
            set { _PostTypeName = value; }
        }
        /// <summary>
        /// 岗位工种
        /// </summary>
        public string PostName
        {
            get { return _PostName; }
            set { _PostName = value; }
        }
        /// <summary>
        /// 审核截止日期
        /// </summary>
        public DateTime? LatestCheckDate
        {
            get { return _LatestCheckDate; }
            set { _LatestCheckDate = value; }
        }
        /// <summary>
        /// 缴费截止日期(目前免费)
        /// </summary>
        public DateTime? LatestPayDate
        {
            get { return _LatestPayDate; }
            set { _LatestPayDate = value; }
        }
	}
}
