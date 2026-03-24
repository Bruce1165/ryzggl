using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamRoomAllot_OperationOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamRoomAllot_OperationOB
    {
        public ExamRoomAllot_OperationOB()
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

        public long? ExamRoomAllotID
        {
            get { return _ExamRoomAllotID; }
            set { _ExamRoomAllotID = value; }
        }

        public long? ExamPlaceAllotID
        {
            get { return _ExamPlaceAllotID; }
            set { _ExamPlaceAllotID = value; }
        }

        public long? ExamPlanID
        {
            get { return _ExamPlanID; }
            set { _ExamPlanID = value; }
        }

        public long? ExamPlaceID
        {
            get { return _ExamPlaceID; }
            set { _ExamPlaceID = value; }
        }

        public string ExamRoomCode
        {
            get { return _ExamRoomCode; }
            set { _ExamRoomCode = value; }
        }

        public int? PersonNumber
        {
            get { return _PersonNumber; }
            set { _PersonNumber = value; }
        }

        public string ExamCardIDFromTo
        {
            get { return _ExamCardIDFromTo; }
            set { _ExamCardIDFromTo = value; }
        }

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
        
        public DateTime? ExamStartTime
        {
            get { return _ExamStartTime; }
            set { _ExamStartTime = value; }
        }

        public DateTime? ExamEndTime
        {
            get { return _ExamEndTime; }
            set { _ExamEndTime = value; }
        }
    }
}
