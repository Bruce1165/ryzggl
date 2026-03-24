using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ApplyMDL填写类描述
    /// </summary>
    [Serializable]
    public class ApplyMDL
    {
        public ApplyMDL()
        {
        }

        //主键
        protected string _ApplyID;

        //其它属性
        protected string _ApplyType;
        protected string _ApplyTypeSub;
        protected string _ENT_ServerID;
        protected string _ENT_Name;
        protected string _ENT_OrganizationsCode;
        protected string _ENT_City;
        protected DateTime? _ENT_ContractStartTime;
        protected DateTime? _ENT_ContractENDTime;
        protected string _PSN_ServerID;
        protected string _PSN_Name;
        protected string _PSN_Sex;
        protected string _PSN_CertificateType;
        protected string _PSN_CertificateNO;
        protected string _PSN_RegisterNo;
        protected string _PSN_RegisterCertificateNo;
        protected string _PSN_RegisteProfession;
        protected DateTime? _ApplyTime;
        protected string _ApplyCode;
        protected string _ApplyStatus;
        protected DateTime? _GetDateTime;
        protected string _GetResult;
        protected string _GetRemark;
        protected string _GetMan;
        protected DateTime? _ExamineDatetime;
        protected string _ExamineResult;
        protected string _ExamineRemark;
        protected string _ExamineMan;
        protected DateTime? _ReportDate;
        protected string _ReportMan;
        protected string _ReportCode;
        protected DateTime? _AcceptDate;
        protected string _AcceptMan;
        protected DateTime? _CheckDate;
        protected string _CheckResult;
        protected string _CheckRemark;
        protected string _CheckMan;
        protected DateTime? _ConfirmDate;
        protected string _ConfirmResult;
        protected string _ConfirmRemark;
        protected string _ConfirmMan;
        protected DateTime? _PublicDate;
        protected string _PublicMan;
        protected string _PublicCode;
        protected DateTime? _NoticeDate;
        protected string _NoticeMan;
        protected string _NoticeCode;
        protected string _CJR;
        protected DateTime? _CJSJ;
        protected string _XGR;
        protected DateTime? _XGSJ;
        protected int? _Valid;
        protected string _Memo;
        protected string _CodeMan;
        protected DateTime? _CodeDate;
        protected int? _SheBaoCheck;
        protected string _PSN_Level;
        protected DateTime? _OtherDeptCheckDate;
        protected string _OtherDeptCheckResult;
        protected string _OtherDeptCheckRemark;
        protected string _OtherDeptCheckMan;

        protected DateTime? _OldUnitCheckTime;
        protected string _OldUnitCheckResult;
        protected string _OldUnitCheckRemark;

        protected string _OldEnt_QYZJJGDM;
        protected DateTime? _newUnitCheckTime;
        protected string _newUnitCheckResult;
        protected string _newUnitCheckRemark;

        protected int? _CheckZSSD;
        protected int? _CheckXSL;
        protected int? _CheckCFZC;
        private int? _CheckYCZC;

        private int? _ENT_ContractType;

        protected string _LastBackResult;

        /// <summary>
        /// 劳动合同类型：1固定期限；2无固定期限；3以完成一定工作任务为期限
        /// </summary>
        public int? ENT_ContractType
        {
            get { return _ENT_ContractType; }
            set { _ENT_ContractType = value; }
        }
        /// <summary>
        /// 异常注册检查
        /// </summary>
        protected int? CheckYCZC
        {
            get { return _CheckYCZC; }
            set { _CheckYCZC = value; }
        }
        /// <summary>
        /// 在施锁定检查
        /// </summary>
        public int? CheckZSSD
        {
            get { return _CheckZSSD; }
            set { _CheckZSSD = value; }
        }
        /// <summary>
        /// 新设立企业检查
        /// </summary>
        public int? CheckXSL
        {
            get { return _CheckXSL; }
            set { _CheckXSL = value; }
        }
        /// <summary>
        /// 重复注册检查
        /// </summary>
        public int? CheckCFZC
        {
            get { return _CheckCFZC; }
            set { _CheckCFZC = value; }
        }  
        /// <summary>
        /// 申请ID
        /// </summary>
        public string ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
        }
        /// <summary>
        /// 注册类型
        /// </summary>
        public string ApplyType
        {
            get { return _ApplyType; }
            set { _ApplyType = value; }
        }
        /// <summary>
        /// 注册类型子项
        /// </summary>
        public string ApplyTypeSub
        {
            get { return _ApplyTypeSub; }
            set { _ApplyTypeSub = value; }
        }
        /// <summary>
        /// 企业ID
        /// </summary>
        public string ENT_ServerID
        {
            get { return _ENT_ServerID; }
            set { _ENT_ServerID = value; }
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string ENT_Name
        {
            get { return _ENT_Name; }
            set { _ENT_Name = value; }
        }
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string ENT_OrganizationsCode
        {
            get { return _ENT_OrganizationsCode; }
            set { _ENT_OrganizationsCode = value; }
        }
        /// <summary>
        /// 所属区县
        /// </summary>
        public string ENT_City
        {
            get { return _ENT_City; }
            set { _ENT_City = value; }
        }
        /// <summary>
        /// 劳动合同开始日期
        /// </summary>
        public DateTime? ENT_ContractStartTime
        {
            get { return _ENT_ContractStartTime; }
            set { _ENT_ContractStartTime = value; }
        }
        /// <summary>
        /// 劳动合同截止日期
        /// </summary>
        public DateTime? ENT_ContractENDTime
        {
            get { return _ENT_ContractENDTime; }
            set { _ENT_ContractENDTime = value; }
        }
        /// <summary>
        /// 建造师ID
        /// </summary>
        public string PSN_ServerID
        {
            get { return _PSN_ServerID; }
            set { _PSN_ServerID = value; }
        }
        /// <summary>
        ///  姓名
        /// </summary>
        public string PSN_Name
        {
            get { return _PSN_Name; }
            set { _PSN_Name = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string PSN_Sex
        {
            get { return _PSN_Sex; }
            set { _PSN_Sex = value; }
        }
        /// <summary>
        /// 证件类别
        /// </summary>
        public string PSN_CertificateType
        {
            get { return _PSN_CertificateType; }
            set { _PSN_CertificateType = value; }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string PSN_CertificateNO
        {
            get { return _PSN_CertificateNO; }
            set { _PSN_CertificateNO = value; }
        }
        /// <summary>
        /// 注册号
        /// </summary>
        public string PSN_RegisterNo
        {
            get { return _PSN_RegisterNo; }
            set { _PSN_RegisterNo = value; }
        }
         /// <summary>
        /// 注册证书编号
        /// </summary>
        public string PSN_RegisterCertificateNo
        {
            get { return _PSN_RegisterCertificateNo; }
            set { _PSN_RegisterCertificateNo = value; }
        }
        /// <summary>
        /// 注册专业
        /// </summary>
        public string PSN_RegisteProfession
        {
            get { return _PSN_RegisteProfession; }
            set { _PSN_RegisteProfession = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplyTime
        {
            get { return _ApplyTime; }
            set { _ApplyTime = value; }
        }
        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyCode
        {
            get { return _ApplyCode; }
            set { _ApplyCode = value; }
        }
        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApplyStatus
        {
            get { return _ApplyStatus; }
            set { _ApplyStatus = value; }
        }
        /// <summary>
        /// 受理时间
        /// </summary>
        public DateTime? GetDateTime
        {
            get { return _GetDateTime; }
            set { _GetDateTime = value; }
        }
        /// <summary>
        /// 受理结论
        /// </summary>
        public string GetResult
        {
            get { return _GetResult; }
            set { _GetResult = value; }
        }
        /// <summary>
        /// 受理说明
        /// </summary>
        public string GetRemark
        {
            get { return _GetRemark; }
            set { _GetRemark = value; }
        }
        /// <summary>
        /// 受理人
        /// </summary>
        public string GetMan
        {
            get { return _GetMan; }
            set { _GetMan = value; }
        }
        /// <summary>
        /// 区县领导审查时间
        /// </summary>
        public DateTime? ExamineDatetime
        {
            get { return _ExamineDatetime; }
            set { _ExamineDatetime = value; }
        }
        /// <summary>
        /// 区县领导审查结论
        /// </summary>
        public string ExamineResult
        {
            get { return _ExamineResult; }
            set { _ExamineResult = value; }
        }
        /// <summary>
        /// 区县领导审查说明
        /// </summary>
        public string ExamineRemark
        {
            get { return _ExamineRemark; }
            set { _ExamineRemark = value; }
        }
        /// <summary>
        /// 区县领导审查人
        /// </summary>
        public string ExamineMan
        {
            get { return _ExamineMan; }
            set { _ExamineMan = value; }
        }
        /// <summary>
        /// 上报时间
        /// </summary>
        public DateTime? ReportDate
        {
            get { return _ReportDate; }
            set { _ReportDate = value; }
        }
        /// <summary>
        /// 上报人
        /// </summary>
        public string ReportMan
        {
            get { return _ReportMan; }
            set { _ReportMan = value; }
        }
        public string ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }
        /// <summary>
        /// 收件时间
        /// </summary>
        public DateTime? AcceptDate
        {
            get { return _AcceptDate; }
            set { _AcceptDate = value; }
        }
        /// <summary>
        /// 收件人
        /// </summary>
        public string AcceptMan
        {
            get { return _AcceptMan; }
            set { _AcceptMan = value; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }
        /// <summary>
        /// 审核结论
        /// </summary>
        public string CheckResult
        {
            get { return _CheckResult; }
            set { _CheckResult = value; }
        }
        /// <summary>
        /// 审核说明
        /// </summary>
        public string CheckRemark
        {
            get { return _CheckRemark; }
            set { _CheckRemark = value; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string CheckMan
        {
            get { return _CheckMan; }
            set { _CheckMan = value; }
        }
        /// <summary>
        /// 决定时间
        /// </summary>
        public DateTime? ConfirmDate
        {
            get { return _ConfirmDate; }
            set { _ConfirmDate = value; }
        }
        /// <summary>
        /// 决定结论
        /// </summary>
        public string ConfirmResult
        {
            get { return _ConfirmResult; }
            set { _ConfirmResult = value; }
        }
         /// <summary>
        /// 决定意见
        /// </summary>
        public string ConfirmRemark
        {
            get { return _ConfirmRemark; }
            set { _ConfirmRemark = value; }
        }
        
        /// <summary>
        /// 决定人
        /// </summary>
        public string ConfirmMan
        {
            get { return _ConfirmMan; }
            set { _ConfirmMan = value; }
        }
        /// <summary>
        /// 公示时间
        /// </summary>
        public DateTime? PublicDate
        {
            get { return _PublicDate; }
            set { _PublicDate = value; }
        }
        /// <summary>
        /// 公示人
        /// </summary>
        public string PublicMan
        {
            get { return _PublicMan; }
            set { _PublicMan = value; }
        }
        /// <summary>
        /// 公示批次号
        /// </summary>
        public string PublicCode
        {
            get { return _PublicCode; }
            set { _PublicCode = value; }
        }
        /// <summary>
        /// 公告时间
        /// </summary>
        public DateTime? NoticeDate
        {
            get { return _NoticeDate; }
            set { _NoticeDate = value; }
        }
        /// <summary>
        /// 公告人
        /// </summary>
        public string NoticeMan
        {
            get { return _NoticeMan; }
            set { _NoticeMan = value; }
        }
        /// <summary>
        /// 公告批次号
        /// </summary>
        public string NoticeCode
        {
            get { return _NoticeCode; }
            set { _NoticeCode = value; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CJR
        {
            get { return _CJR; }
            set { _CJR = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CJSJ
        {
            get { return _CJSJ; }
            set { _CJSJ = value; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string XGR
        {
            get { return _XGR; }
            set { _XGR = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? XGSJ
        {
            get { return _XGSJ; }
            set { _XGSJ = value; }
        }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? Valid
        {
            get { return _Valid; }
            set { _Valid = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _Memo; }
            set { _Memo = value; }
        }
        /// <summary>
        /// 编号人
        /// </summary>
        public string CodeMan
        {
            get { return _CodeMan; }
            set { _CodeMan = value; }
        }
        /// <summary>
        /// 编号时间
        /// </summary>
        public DateTime? CodeDate
        {
            get { return _CodeDate; }
            set { _CodeDate = value; }
        }
        /// <summary>
        /// 社保检查结果
        /// </summary>
        public int? SheBaoCheck
        {
            get { return _SheBaoCheck; }
            set { _SheBaoCheck = value; }
        }
        /// <summary>
        /// 注册等级：二级；二级临时
        /// </summary>
        public string PSN_Level
        {
            get { return _PSN_Level; }
            set { _PSN_Level = value; }
        }
        /// <summary>
        /// 专业委办局核查时间
        /// </summary>
        public DateTime? OtherDeptCheckDate
        {
            get { return _OtherDeptCheckDate; }
            set { _OtherDeptCheckDate = value; }
        }
        /// <summary>
        /// 专业委办局核查结论
        /// </summary>
        public string OtherDeptCheckResult
        {
            get { return _OtherDeptCheckResult; }
            set { _OtherDeptCheckResult = value; }
        }
        /// <summary>
        /// 专业委办局核查说明
        /// </summary>
        public string OtherDeptCheckRemark
        {
            get { return _OtherDeptCheckRemark; }
            set { _OtherDeptCheckRemark = value; }
        }
        /// <summary>
        /// 专业委办局核查人
        /// </summary>
        public string OtherDeptCheckMan
        {
            get { return _OtherDeptCheckMan; }
            set { _OtherDeptCheckMan = value; }
        }
        
        /// <summary>
        /// 原单位审核时间
        /// </summary>
        public DateTime? OldUnitCheckTime
        {
            get { return _OldUnitCheckTime; }
            set { _OldUnitCheckTime = value; }
        }
        /// <summary>
        /// 原单位审核结论
        /// </summary>
        public string OldUnitCheckResult
        {
            get { return _OldUnitCheckResult; }
            set { _OldUnitCheckResult = value; }
        }
        /// <summary>
        /// 原单位审核说明
        /// </summary>
        public string OldUnitCheckRemark
        {
            get { return _OldUnitCheckRemark; }
            set { _OldUnitCheckRemark = value; }
        }
        /// <summary>
        /// 原单位机构代码
        /// </summary>
        public string OldEnt_QYZJJGDM
        {
            get { return _OldEnt_QYZJJGDM; }
            set { _OldEnt_QYZJJGDM = value; }
        }
        /// <summary>
        /// 新单位审核时间
        /// </summary>
        public DateTime? newUnitCheckTime
        {
            get { return _newUnitCheckTime; }
            set { _newUnitCheckTime = value; }
        }
        /// <summary>
        /// 新单位审核结果
        /// </summary>
        public string newUnitCheckResult
        {
            get { return _newUnitCheckResult; }
            set { _newUnitCheckResult = value; }
        }
        /// <summary>
        /// 新单位审核说明
        /// </summary>
        public string newUnitCheckRemark
        {
            get { return _newUnitCheckRemark; }
            set { _newUnitCheckRemark = value; }
        }
        /// <summary>
        /// 最后驳回意见
        /// </summary>
        public string LastBackResult
        {
            get { return _LastBackResult; }
            set { _LastBackResult = value; }
        }

    }
}