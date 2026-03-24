using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--CertificateChangeOB填写类描述
    /// </summary>
    [Serializable]
    public class CertificateChangeOB
    {
        public CertificateChangeOB()
        {
            //默认值
        }

        //主键
        protected long? _CertificateChangeID;

        //其它属性
        protected long? _CertificateID;
        protected string _ChangeType;
        protected string _WorkerName;
        protected string _Sex;
        protected DateTime? _Birthday;
        protected DateTime? _ConferDate;
        protected DateTime? _ValidStartDate;
        protected DateTime? _ValidEndDate;
        protected string _ConferUnit;
        protected string _DealWay;
        protected string _OldUnitAdvise;
        protected string _NewUnitAdvise;
        protected string _OldConferUnitAdvise;
        protected string _NewConferUnitAdvise;
        protected DateTime? _ApplyDate;
        protected string _ApplyMan;
        protected string _ApplyCode;
        protected DateTime? _GetDate;
        protected string _GetResult;
        protected string _GetMan;
        protected string _GetCode;
        protected DateTime? _CheckDate;
        protected string _CheckResult;
        protected string _CheckMan;
        protected string _CheckCode;
        protected DateTime? _ConfrimDate;
        protected string _ConfrimResult;
        protected string _ConfrimMan;
        protected string _ConfrimCode;
        protected DateTime? _NoticeDate;
        protected string _NoticeResult;
        protected string _NoticeMan;
        protected string _NoticeCode;
        protected string _Status;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _UnitName;
        protected string _NewUnitName;
        protected string _UnitCode;
        protected string _NewUnitCode;
        protected string _WorkerCertificateCode;
        protected string _LinkWay;
        protected string _NewWorkerCertificateCode;
        protected string _NewWorkerName;
        protected string _NewSex;
        protected DateTime? _NewBirthday;
        protected Byte? _SheBaoCheck;
        protected Byte? _IfUpdatePhoto;
        protected DateTime? _OldUnitCheckTime;
        protected DateTime? _NewUnitCheckTime;
        protected string _ChangeRemark;

        protected string _Job;
        protected string _SkillLevel;
        protected DateTime? _ZACheckTime;
        protected int? _ZACheckResult;
        protected string _ZACheckRemark;

        /// <summary>
        /// 安管人员在受聘企业担任的职务
        /// </summary>
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        /// <summary>
        /// 技术职称
        /// </summary>
        public string SkillLevel
        {
            get { return _SkillLevel; }
            set { _SkillLevel = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string ChangeRemark
        {
            get { return _ChangeRemark; }
            set { _ChangeRemark = value; }
        }
        public DateTime? OldUnitCheckTime
        {
            get { return _OldUnitCheckTime; }
            set { _OldUnitCheckTime = value; }
        }
        public DateTime? NewUnitCheckTime
        {
            get { return _NewUnitCheckTime; }
            set { _NewUnitCheckTime = value; }
        }
        public long? CertificateChangeID
        {
            get { return _CertificateChangeID; }
            set { _CertificateChangeID = value; }
        }

        public long? CertificateID
        {
            get { return _CertificateID; }
            set { _CertificateID = value; }
        }

        public string ChangeType
        {
            get { return _ChangeType; }
            set { _ChangeType = value; }
        }

        public string WorkerName
        {
            get { return _WorkerName; }
            set { _WorkerName = value; }
        }

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        public DateTime? ConferDate
        {
            get { return _ConferDate; }
            set { _ConferDate = value; }
        }

        public DateTime? ValidStartDate
        {
            get { return _ValidStartDate; }
            set { _ValidStartDate = value; }
        }

        public DateTime? ValidEndDate
        {
            get { return _ValidEndDate; }
            set { _ValidEndDate = value; }
        }

        public string ConferUnit
        {
            get { return _ConferUnit; }
            set { _ConferUnit = value; }
        }

        public string DealWay
        {
            get { return _DealWay; }
            set { _DealWay = value; }
        }

        public string OldUnitAdvise
        {
            get { return _OldUnitAdvise; }
            set { _OldUnitAdvise = value; }
        }

        public string NewUnitAdvise
        {
            get { return _NewUnitAdvise; }
            set { _NewUnitAdvise = value; }
        }

        public string OldConferUnitAdvise
        {
            get { return _OldConferUnitAdvise; }
            set { _OldConferUnitAdvise = value; }
        }

        public string NewConferUnitAdvise
        {
            get { return _NewConferUnitAdvise; }
            set { _NewConferUnitAdvise = value; }
        }

        public DateTime? ApplyDate
        {
            get { return _ApplyDate; }
            set { _ApplyDate = value; }
        }

        public string ApplyMan
        {
            get { return _ApplyMan; }
            set { _ApplyMan = value; }
        }

        public string ApplyCode
        {
            get { return _ApplyCode; }
            set { _ApplyCode = value; }
        }

        public DateTime? GetDate
        {
            get { return _GetDate; }
            set { _GetDate = value; }
        }

        public string GetResult
        {
            get { return _GetResult; }
            set { _GetResult = value; }
        }

        public string GetMan
        {
            get { return _GetMan; }
            set { _GetMan = value; }
        }

        public string GetCode
        {
            get { return _GetCode; }
            set { _GetCode = value; }
        }

        public DateTime? CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }

        public string CheckResult
        {
            get { return _CheckResult; }
            set { _CheckResult = value; }
        }

        public string CheckMan
        {
            get { return _CheckMan; }
            set { _CheckMan = value; }
        }

        public string CheckCode
        {
            get { return _CheckCode; }
            set { _CheckCode = value; }
        }

        public DateTime? ConfrimDate
        {
            get { return _ConfrimDate; }
            set { _ConfrimDate = value; }
        }

        public string ConfrimResult
        {
            get { return _ConfrimResult; }
            set { _ConfrimResult = value; }
        }

        public string ConfrimMan
        {
            get { return _ConfrimMan; }
            set { _ConfrimMan = value; }
        }

        public string ConfrimCode
        {
            get { return _ConfrimCode; }
            set { _ConfrimCode = value; }
        }

        public DateTime? NoticeDate
        {
            get { return _NoticeDate; }
            set { _NoticeDate = value; }
        }

        public string NoticeResult
        {
            get { return _NoticeResult; }
            set { _NoticeResult = value; }
        }

        public string NoticeMan
        {
            get { return _NoticeMan; }
            set { _NoticeMan = value; }
        }

        public string NoticeCode
        {
            get { return _NoticeCode; }
            set { _NoticeCode = value; }
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

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        public string NewUnitName
        {
            get { return _NewUnitName; }
            set { _NewUnitName = value; }
        }

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        public string NewUnitCode
        {
            get { return _NewUnitCode; }
            set { _NewUnitCode = value; }
        }

        public string WorkerCertificateCode
        {
            get { return _WorkerCertificateCode; }
            set { _WorkerCertificateCode = value; }
        }

        public string LinkWay
        {
            get { return _LinkWay; }
            set { _LinkWay = value; }
        }
        public string NewWorkerCertificateCode
        {
            get { return _NewWorkerCertificateCode; }
            set { _NewWorkerCertificateCode = value; }
        }

        public string NewWorkerName
        {
            get { return _NewWorkerName; }
            set { _NewWorkerName = value; }
        }

        public string NewSex
        {
            get { return _NewSex; }
            set { _NewSex = value; }
        }

        public DateTime? NewBirthday
        {
            get { return _NewBirthday; }
            set { _NewBirthday = value; }
        }


        public Byte? SheBaoCheck
        {
            get { return _SheBaoCheck; }
            set { _SheBaoCheck = value; }
        }
        public Byte? IfUpdatePhoto
        {
            get { return _IfUpdatePhoto; }
            set { _IfUpdatePhoto = value; }
        }
        /// <summary>
        /// 质安网数据校验时间
        /// </summary>
        public DateTime? ZACheckTime
        {
            get { return _ZACheckTime; }
            set { _ZACheckTime = value; }
        }
        /// <summary>
        /// 质安网数据校验结果，0：失败，1：成功，2：警告
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
        /// 合同类型
        /// 1：固定期限
        /// 2：无固定期限
        /// 3：以完成一定工作任务为期限
        /// 4：企业不配合办理（申请强制注销或离京时使用）
        /// 5：无法与该企业取得联系（申请强制注销或离京时使用）
        /// </summary>
        public int? ENT_ContractType { get; set; }

        /// <summary>
        /// 劳动合同开始日期（申请强制注销或离京时：该字段表示解除劳动关系日期）
        /// </summary>
        public DateTime? ENT_ContractStartTime { get; set; }

        /// <summary>
        /// 劳动合同截止日期（申请强制注销时：该字段表示不在该单位从事安全生产管理工作日期）
        /// </summary>
        public DateTime? ENT_ContractENDTime { get; set; }
    }
}
