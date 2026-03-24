using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamResultOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamResultOB
    {
        public ExamResultOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamResultID;

        //其它属性
        protected long? _ExamRoomAllotID;
        protected long? _ExamPlanID;
        protected long? _WorkerID;
        protected string _ExamCardID;
        protected string _ExamResult;
        protected string _Status;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected long? _ExamSignUp_ID;

        public long? ExamResultID
        {
            get { return _ExamResultID; }
            set { _ExamResultID = value; }
        }

        public long? ExamSignUp_ID
        {
            get { return _ExamSignUp_ID; }
            set { _ExamSignUp_ID = value; }
        }

        public long? ExamRoomAllotID
        {
            get { return _ExamRoomAllotID; }
            set { _ExamRoomAllotID = value; }
        }

        public long? ExamPlanID
        {
            get { return _ExamPlanID; }
            set { _ExamPlanID = value; }
        }

        public long? WorkerID
        {
            get { return _WorkerID; }
            set { _WorkerID = value; }
        }

        public string ExamCardID
        {
            get { return _ExamCardID; }
            set { _ExamCardID = value; }
        }

        public string ExamResult
        {
            get { return _ExamResult; }
            set { _ExamResult = value; }
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
    }
}
