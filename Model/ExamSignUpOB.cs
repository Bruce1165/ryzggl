using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamSignUpOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamSignUpOB
    {
        public ExamSignUpOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamSignUpID;

        //其它属性
        protected string _SignUpCode;
        protected DateTime? _SignUpDate;
        protected long? _WorkerID;
        protected long? _UnitID;
        protected long? _TrainUnitID;
        protected long? _ExamPlanID;
        protected DateTime? _WorkStartDate;
        protected int? _WorkYearNumer;
        protected string _PersonDetail;
        protected string _HireUnitAdvise;
        protected string _AdminUnitAdvise;
        protected string _CheckCode;
        protected string _CheckResult;
        protected string _CheckMan;
        protected DateTime? _CheckDate;
        protected string _PayNoticeCode;
        protected string _PayNoticeResult;
        protected string _PayNoticeMan;
        protected DateTime? _PayNoticeDate;
        protected decimal? _PayMoney;
        protected string _PayConfirmCode;
        protected string _PayConfirmRult;
        protected string _PayConfirmMan;
        protected DateTime? _PayConfirmDate;
        protected string _FacePhoto;
        protected string _Status;
        protected string _WorkerName;
        protected string _CertificateType;
        protected string _CertificateCode;
        protected string _UnitName;
        protected string _UnitCode;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _SKILLLEVEL;
        protected string _IsConditions;

        private string s_sex = String.Empty;
        private DateTime? s_birthday;
        private string s_culturalLevel = String.Empty;
        private string s_phone = String.Empty;
        protected string _S_TRAINUNITNAME;
        protected DateTime? _FIRSTTRIALTIME;

        protected string _SignUpMan;
        protected byte? _SheBaoCheck;

        protected int? _Promise;
        protected long? _SignUpPlaceID;
        protected DateTime? _CheckDatePlan;
        protected string _PlaceName;

        protected int? _FirstCheckType;

        protected DateTime? _LockTime;
        protected DateTime? _LockEndTime;
        protected string _LockReason;
        protected string _LockMan;

        protected string _AcceptResult;
        protected string _AcceptMan;
        protected DateTime? _AcceptTime;

        protected DateTime? _ZACheckTime;
        protected int? _ZACheckResult;
        protected string _ZACheckRemark;
        protected string _Job;

        protected string _SafeTrainType;
        protected string _SafeTrainUnit;
        protected string _SafeTrainUnitCode;
        protected DateTime? _SafeTrainUnitValidEndDate;
        protected string _SafeTrainUnitOfDept;

        /// <summary>
        /// 安管人员在受聘企业担任的职务
        /// </summary>
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }

        /// <summary>
        /// 受理结果
        /// </summary>
        public string AcceptResult
        {
            get { return _AcceptResult; }
            set { _AcceptResult = value; }
        }
        /// <summary>
        /// 受理人
        /// </summary>
        public string AcceptMan
        {
            get { return _AcceptMan; }
            set { _AcceptMan = value; }
        }
        /// <summary>
        /// 受理时间
        /// </summary>
        public DateTime? AcceptTime
        {
            get { return _AcceptTime; }
            set { _AcceptTime = value; }
        }

        /// <summary>
        /// 锁定原因
        /// </summary>
        public string LockReason
        {
            get { return _LockReason; }
            set { _LockReason = value; }
        }
        /// <summary>
        /// 锁定人
        /// </summary>
        public string LockMan
        {
            get { return _LockMan; }
            set { _LockMan = value; }
        }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LockTime
        {
            get { return _LockTime; }
            set { _LockTime = value; }
        }
        /// <summary>
        /// 锁定截止日期
        /// </summary>
        public DateTime? LockEndTime
        {
            get { return _LockEndTime; }
            set { _LockEndTime = value; }
        }

        /// <summary>
        /// 自动初审类型
        /// -1：非自动初审（上次缺考）；
        /// 0：非自动初审（其他)；
        /// 1：有建造师证书；
        /// 2：近两年参加过考试；
        /// 3：社保符合。
        /// 4：法人库匹配
        /// </summary>
        public int? FirstCheckType
        {
            get { return _FirstCheckType; }
            set { _FirstCheckType = value; }
        }

        /// <summary>
        /// 社保比对结果：0不符合，1符合。
        /// </summary>
        public byte? SheBaoCheck
        {
            get { return _SheBaoCheck; }
            set { _SheBaoCheck = value; }
        }

        /// <summary>
        /// 是否已经承诺责任
        /// </summary>
        public int? Promise
        {
            get { return _Promise; }
            set { _Promise = value; }
        }

        /// <summary>
        /// 报名初审点ID
        /// </summary>
        public long? SignUpPlaceID
        {
            get { return _SignUpPlaceID; }
            set { _SignUpPlaceID = value; }
        }

        /// <summary>
        /// 报名初审点名称
        /// </summary>
        public string PlaceName
        {
            get { return this._PlaceName; }
            set { this._PlaceName = value; }
        }

        /// <summary>
        /// 计划初审日期。2021-02-20日重新定义 原意：计划初审日期，现在用于记录个人确认报名时间（预报名后抢报名名额）
        /// </summary>
        public DateTime? CheckDatePlan
        {
            get { return _CheckDatePlan; }
            set { _CheckDatePlan = value; }
        }

        /// <summary>
        /// 报名人
        /// </summary>
        public string SignUpMan
        {
            get { return this._SignUpMan; }
            set { this._SignUpMan = value; }
        }

        /// <summary>
        /// 初审时间(单位确认时间)
        /// </summary>
        public DateTime? FIRSTTRIALTIME
        {
            get { return _FIRSTTRIALTIME; }
            set { _FIRSTTRIALTIME = value; }
        }
        /// <summary>
        /// 培训点名称
        /// </summary>
        public string S_TRAINUNITNAME
        {
            get { return this._S_TRAINUNITNAME; }
            set { this._S_TRAINUNITNAME = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string S_SEX
        {
            get { return this.s_sex; }
            set { this.s_sex = value; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? S_BIRTHDAY
        {
            get { return this.s_birthday; }
            set { this.s_birthday = value; }
        }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string S_CULTURALLEVEL
        {
            get { return this.s_culturalLevel; }
            set { this.s_culturalLevel = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string S_PHONE
        {
            get { return this.s_phone; }
            set { this.s_phone = value; }
        }

        /// <summary>
        /// 考试报名ID
        /// </summary>
        public long? ExamSignUpID
        {
            get { return _ExamSignUpID; }
            set { _ExamSignUpID = value; }
        }
        /// <summary>
        /// 报名批次号
        /// </summary>
        public string SignUpCode
        {
            get { return _SignUpCode; }
            set { _SignUpCode = value; }
        }
        /// <summary>
        /// 报名日期
        /// </summary>
        public DateTime? SignUpDate
        {
            get { return _SignUpDate; }
            set { _SignUpDate = value; }
        }
        /// <summary>
        /// 从业人员ID
        /// </summary>
        public long? WorkerID
        {
            get { return _WorkerID; }
            set { _WorkerID = value; }
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }
        /// <summary>
        /// 培训点ID
        /// </summary>
        public long? TrainUnitID
        {
            get { return _TrainUnitID; }
            set { _TrainUnitID = value; }
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
        /// 开始工作时间
        /// </summary>
        public DateTime? WorkStartDate
        {
            get { return _WorkStartDate; }
            set { _WorkStartDate = value; }
        }
        /// <summary>
        /// 工作年限
        /// </summary>
        public int? WorkYearNumer
        {
            get { return _WorkYearNumer; }
            set { _WorkYearNumer = value; }
        }
        /// <summary>
        /// 个人简历
        /// </summary>
        public string PersonDetail
        {
            get { return _PersonDetail; }
            set { _PersonDetail = value; }
        }
        /// <summary>
        /// 单位意见
        /// </summary>
        public string HireUnitAdvise
        {
            get { return _HireUnitAdvise; }
            set { _HireUnitAdvise = value; }
        }
        /// <summary>
        /// 主管机关意见
        /// </summary>
        public string AdminUnitAdvise
        {
            get { return _AdminUnitAdvise; }
            set { _AdminUnitAdvise = value; }
        }
        /// <summary>
        /// 缴费审核批次号
        /// </summary>
        public string CheckCode
        {
            get { return _CheckCode; }
            set { _CheckCode = value; }
        }
        /// <summary>
        /// 缴费审核结论
        /// </summary>
        public string CheckResult
        {
            get { return _CheckResult; }
            set { _CheckResult = value; }
        }
        /// <summary>
        /// 缴费审核人
        /// </summary>
        public string CheckMan
        {
            get { return _CheckMan; }
            set { _CheckMan = value; }
        }
        /// <summary>
        /// 缴费审核日期
        /// </summary>
        public DateTime? CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }
        /// <summary>
        /// 缴费通知批次号（废弃）
        /// </summary>
        public string PayNoticeCode
        {
            get { return _PayNoticeCode; }
            set { _PayNoticeCode = value; }
        }
        /// <summary>
        /// 缴费通知结论（废弃）
        /// </summary>
        public string PayNoticeResult
        {
            get { return _PayNoticeResult; }
            set { _PayNoticeResult = value; }
        }
        /// <summary>
        /// 缴费通知人（废弃）
        /// </summary>
        public string PayNoticeMan
        {
            get { return _PayNoticeMan; }
            set { _PayNoticeMan = value; }
        }
        /// <summary>
        /// 缴费通知日期（废弃）
        /// </summary>
        public DateTime? PayNoticeDate
        {
            get { return _PayNoticeDate; }
            set { _PayNoticeDate = value; }
        }
        /// <summary>
        /// 报名缴费金额
        /// </summary>
        public decimal? PayMoney
        {
            get { return _PayMoney; }
            set { _PayMoney = value; }
        }
        /// <summary>
        /// 决定批次号
        /// </summary>
        public string PayConfirmCode
        {
            get { return _PayConfirmCode; }
            set { _PayConfirmCode = value; }
        }
        /// <summary>
        /// 决定结果
        /// </summary>
        public string PayConfirmRult
        {
            get { return _PayConfirmRult; }
            set { _PayConfirmRult = value; }
        }

        /// <summary>
        /// 决定人
        /// </summary>
        public string PayConfirmMan
        {
            get { return _PayConfirmMan; }
            set { _PayConfirmMan = value; }
        }
        /// <summary>
        /// 决定时间
        /// </summary>
        public DateTime? PayConfirmDate
        {
            get { return _PayConfirmDate; }
            set { _PayConfirmDate = value; }
        }
        /// <summary>
        /// 照片
        /// </summary>
        public string FacePhoto
        {
            get { return _FacePhoto; }
            set { _FacePhoto = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string WorkerName
        {
            get { return _WorkerName; }
            set { _WorkerName = value; }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertificateType
        {
            get { return _CertificateType; }
            set { _CertificateType = value; }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateCode
        {
            get { return _CertificateCode; }
            set { _CertificateCode = value; }
        }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
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
        /// 专业等级
        /// </summary>
        public string SKILLLEVEL
        {
            get { return _SKILLLEVEL; }
            set { _SKILLLEVEL = value; }
        }

        public string IsConditions
        {
            get { return _IsConditions; }
            set { _IsConditions = value; }
        }

       
        /// <summary>
        /// 合同类型
        /// </summary>
        public int? ENT_ContractType { get; set; }

        /// <summary>
        /// 劳动合同开始日期
        /// </summary>
        public DateTime? ENT_ContractStartTime { get; set; }

        /// <summary>
        /// 劳动合同截止日期
        /// </summary>
        public DateTime? ENT_ContractENDTime { get; set; }

        /// <summary>
        /// 是否上传报考条件证明承诺书，0：上传学历和职称证明；1：上传承诺书
        /// </summary>
        public int? SignupPromise { get; set; }

        /// <summary>
        /// 质安网数据校验时间
        /// </summary>
        public DateTime? ZACheckTime
        {
            get { return _ZACheckTime; }
            set { _ZACheckTime = value; }
        }
        /// <summary>
        /// 质安网数据校验结果，0：失败，1：成功
        /// </summary>
        public int? ZACheckResult
        {
            get { return _ZACheckResult; }
            set { _ZACheckResult = value; }
        }
        /// <summary>
        /// 质安网数据校验结果说明
        /// </summary>
        public string ZACheckRemark
        {
            get { return _ZACheckRemark; }
            set { _ZACheckRemark = value; }
        }

        /// <summary>
        /// 考前安全培训类型：自行；委托培训机构；
        /// </summary>
        public string SafeTrainType
        {
            get { return _SafeTrainType; }
            set { _SafeTrainType = value; }
        }
        /// <summary>
        /// 安全培训机构名称
        /// </summary>
        public string SafeTrainUnit
        {
            get { return _SafeTrainUnit; }
            set { _SafeTrainUnit = value; }
        }
        /// <summary>
        /// 办学许可证编号
        /// </summary>
        public string SafeTrainUnitCode
        {
            get { return _SafeTrainUnitCode; }
            set { _SafeTrainUnitCode = value; }
        }       		
		
        /// <summary>
        /// 办学许可证有效期
        /// </summary>
        public DateTime? SafeTrainUnitValidEndDate
        {
            get { return _SafeTrainUnitValidEndDate; }
            set { _SafeTrainUnitValidEndDate = value; }
        }
        /// <summary>
        /// 办学许可证发证机关
        /// </summary>
        public string SafeTrainUnitOfDept
        {
            get { return _SafeTrainUnitOfDept; }
            set { _SafeTrainUnitOfDept = value; }
        }
    }
}
