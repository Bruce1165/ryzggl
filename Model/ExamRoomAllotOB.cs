using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamRoomAllotOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamRoomAllotOB
    {
        public ExamRoomAllotOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamRoomAllotID;

        //其它属性
        protected long? _ExamPlaceAllotID;
        protected long? _ExamPlanID;
        protected long? _ExamPlaceID;
        protected string _ExamRoomCode;
        protected int? _PersonNumber;
        protected string _ExamCardIDFromTo;
        protected string _Status;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected DateTime? _ExamStartTime;
        protected DateTime? _ExamEndTime;

        /// <summary>
        /// 考场分配记录ID
        /// </summary>
        public long? ExamRoomAllotID
        {
            get { return _ExamRoomAllotID; }
            set { _ExamRoomAllotID = value; }
        }
        /// <summary>
        /// 考点分配记录ID
        /// </summary>
        public long? ExamPlaceAllotID
        {
            get { return _ExamPlaceAllotID; }
            set { _ExamPlaceAllotID = value; }
        }
        /// <summary>
        /// 考试计划ID
        /// </summary>
        public long? ExamPlanID
        {
            get { return _ExamPlanID; }
            set { _ExamPlanID = value; }
        }
        /// <summary>
        /// 考点ID
        /// </summary>
        public long? ExamPlaceID
        {
            get { return _ExamPlaceID; }
            set { _ExamPlaceID = value; }
        }
        /// <summary>
        /// 考场编号
        /// </summary>
        public string ExamRoomCode
        {
            get { return _ExamRoomCode; }
            set { _ExamRoomCode = value; }
        }
        /// <summary>
        /// 考场人数
        /// </summary>
        public int? PersonNumber
        {
            get { return _PersonNumber; }
            set { _PersonNumber = value; }
        }
        /// <summary>
        /// 准考证范围
        /// </summary>
        public string ExamCardIDFromTo
        {
            get { return _ExamCardIDFromTo; }
            set { _ExamCardIDFromTo = value; }
        }
        /// <summary>
        /// 状态：已分配考点；已分配考生；
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public long? CreatePersonID
        {
            get { return _CreatePersonID; }
            set { _CreatePersonID = value; }
        }

        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        public long? ModifyPersonID
        {
            get { return _ModifyPersonID; }
            set { _ModifyPersonID = value; }
        }

        public DateTime? ModifyTime
        {
            get { return _ModifyTime; }
            set { _ModifyTime = value; }
        }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public DateTime? ExamStartTime
        {
            get { return _ExamStartTime; }
            set { _ExamStartTime = value; }
        }
        /// <summary>
        /// 考试截止时间
        /// </summary>
        public DateTime? ExamEndTime
        {
            get { return _ExamEndTime; }
            set { _ExamEndTime = value; }
        }
    }
}
