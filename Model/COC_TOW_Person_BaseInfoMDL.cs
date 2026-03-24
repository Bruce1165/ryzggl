using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--COC_TOW_Person_BaseInfoMDL填写类描述
    /// </summary>
    [Serializable]
    public class COC_TOW_Person_BaseInfoMDL
    {
        public COC_TOW_Person_BaseInfoMDL()
        {
        }

        //主键
        protected string _PSN_ServerID;

        //其它属性
        protected string _ENT_ServerID;
        protected string _ENT_Name;
        protected string _ENT_OrganizationsCode;
        protected string _ENT_City;
        protected DateTime? _BeginTime;
        protected DateTime? _EndTime;
        protected string _PSN_Name;
        protected string _PSN_Sex;
        protected DateTime? _PSN_BirthDate;
        protected string _PSN_National;
        protected string _PSN_CertificateType;
        protected string _PSN_CertificateNO;
        protected string _PSN_GraduationSchool;
        protected string _PSN_Specialty;
        protected DateTime? _PSN_GraduationTime;
        protected string _PSN_Qualification;
        protected string _PSN_Degree;
        protected string _PSN_MobilePhone;
        protected string _PSN_Telephone;
        protected string _PSN_Email;
        protected string _PSN_PMGrade;
        protected string _PSN_PMCertificateNo;
        protected string _PSN_RegisteType;
        protected string _PSN_RegisterNO;
        protected string _PSN_RegisterCertificateNo;
        protected string _PSN_RegisteProfession;
        protected DateTime? _PSN_CertificationDate;
        protected DateTime? _PSN_CertificateValidity;
        protected DateTime? _PSN_RegistePermissionDate;
        protected string _PSN_ChangeReason;
        protected string _PSN_BeforENT_Name;
        protected string _PSN_BeforENT_ServerID;
        protected string _PSN_BeforPersonName;
        protected string _PSN_InterprovincialChange;
        protected string _PSN_ExpiryReasons;
        protected DateTime? _PSN_ExpiryDate;
        protected string _PSN_RenewalProfession;
        protected string _PSN_AddProfession;
        protected string _PSN_CancelPerson;
        protected string _PSN_CancelReason;
        protected string _PSN_ReReasons;
        protected string _PSN_ReContent;
        protected string _PSN_CheckCode;
        protected string _ENT_Province_Code;
        protected string _PSN_Level;
        protected string _ZGZSBH;
        protected string _CJR;
        protected DateTime? _CJSJ;
        protected string _XGR;
        protected DateTime? _XGSJ;
        protected int? _Valid;
        protected string _Memo;

        protected DateTime? _ApplyCATime;
        protected DateTime? _ReturnCATime;
        protected string _CertificateCAID;

        /// <summary>
        /// 请求签章时间
        /// </summary>
        public DateTime? ApplyCATime
        {
            get { return _ApplyCATime; }
            set { _ApplyCATime = value; }
        }

        /// <summary>
        /// 返回签章时间
        /// </summary>
        public DateTime? ReturnCATime
        {
            get { return _ReturnCATime; }
            set { _ReturnCATime = value; }
        }
        
        /// <summary>
        /// 电子签章ID
        /// </summary>
        public string CertificateCAID
        {
            get { return _CertificateCAID; }
            set { _CertificateCAID = value; }
        }

        /// <summary>
        /// 人员ID
        /// </summary>
        public string PSN_ServerID
        {
            get { return _PSN_ServerID; }
            set { _PSN_ServerID = value; }
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
        /// 区县
        /// </summary>
        public string ENT_City
        {
            get { return _ENT_City; }
            set { _ENT_City = value; }
        }

        public DateTime? BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        public DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        /// <summary>
        /// 姓名
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
        /// 出生日期
        /// </summary>
        public DateTime? PSN_BirthDate
        {
            get { return _PSN_BirthDate; }
            set { _PSN_BirthDate = value; }
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string PSN_National
        {
            get { return _PSN_National; }
            set { _PSN_National = value; }
        }
        /// <summary>
        /// 证件类型
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
        /// 毕业院校
        /// </summary>
        public string PSN_GraduationSchool
        {
            get { return _PSN_GraduationSchool; }
            set { _PSN_GraduationSchool = value; }
        }
        /// <summary>
        /// 所学专业
        /// </summary>
        public string PSN_Specialty
        {
            get { return _PSN_Specialty; }
            set { _PSN_Specialty = value; }
        }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? PSN_GraduationTime
        {
            get { return _PSN_GraduationTime; }
            set { _PSN_GraduationTime = value; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string PSN_Qualification
        {
            get { return _PSN_Qualification; }
            set { _PSN_Qualification = value; }
        }
        /// <summary>
        /// 学位
        /// </summary>
        public string PSN_Degree
        {
            get { return _PSN_Degree; }
            set { _PSN_Degree = value; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string PSN_MobilePhone
        {
            get { return _PSN_MobilePhone; }
            set { _PSN_MobilePhone = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PSN_Telephone
        {
            get { return _PSN_Telephone; }
            set { _PSN_Telephone = value; }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string PSN_Email
        {
            get { return _PSN_Email; }
            set { _PSN_Email = value; }
        }
        /// <summary>
        /// 项目经理级别
        /// </summary>
        public string PSN_PMGrade
        {
            get { return _PSN_PMGrade; }
            set { _PSN_PMGrade = value; }
        }
        /// <summary>
        /// 项目经理证书编号
        /// </summary>
        public string PSN_PMCertificateNo
        {
            get { return _PSN_PMCertificateNo; }
            set { _PSN_PMCertificateNo = value; }
        }

        /// <summary>
        /// 注册类型：
        ///01	初始注册
        ///02	变更注册
        ///03	延期注册
        ///04	增项注册
        ///05	重新注册
        ///06	遗失补办
        ///07	注销
        /// </summary>
        public string PSN_RegisteType
        {
            get { return _PSN_RegisteType; }
            set { _PSN_RegisteType = value; }
        }

        /// <summary>
        /// 注册号
        /// </summary>
        public string PSN_RegisterNO
        {
            get { return _PSN_RegisterNO; }
            set { _PSN_RegisterNO = value; }
        }

        /// <summary>
        /// 证书编号
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
        /// 初始或重新发证日期
        /// </summary>
        public DateTime? PSN_CertificationDate
        {
            get { return _PSN_CertificationDate; }
            set { _PSN_CertificationDate = value; }
        }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? PSN_CertificateValidity
        {
            get { return _PSN_CertificateValidity; }
            set { _PSN_CertificateValidity = value; }
        }

        /// <summary>
        /// 注册审批日期
        /// </summary>
        public DateTime? PSN_RegistePermissionDate
        {
            get { return _PSN_RegistePermissionDate; }
            set { _PSN_RegistePermissionDate = value; }
        }
        /// <summary>
        /// 变更原因
        /// </summary>
        public string PSN_ChangeReason
        {
            get { return _PSN_ChangeReason; }
            set { _PSN_ChangeReason = value; }
        }
        /// <summary>
        /// 变更前企业名称
        /// </summary>
        public string PSN_BeforENT_Name
        {
            get { return _PSN_BeforENT_Name; }
            set { _PSN_BeforENT_Name = value; }
        }
        /// <summary>
        /// 变更前企业ID
        /// </summary>
        public string PSN_BeforENT_ServerID
        {
            get { return _PSN_BeforENT_ServerID; }
            set { _PSN_BeforENT_ServerID = value; }
        }
        /// <summary>
        /// 变更前姓名
        /// </summary>
        public string PSN_BeforPersonName
        {
            get { return _PSN_BeforPersonName; }
            set { _PSN_BeforPersonName = value; }
        }
        /// <summary>
        /// 是否跨省变更
        /// </summary>
        public string PSN_InterprovincialChange
        {
            get { return _PSN_InterprovincialChange; }
            set { _PSN_InterprovincialChange = value; }
        }
        /// <summary>
        /// 失效原因
        /// </summary>
        public string PSN_ExpiryReasons
        {
            get { return _PSN_ExpiryReasons; }
            set { _PSN_ExpiryReasons = value; }
        }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? PSN_ExpiryDate
        {
            get { return _PSN_ExpiryDate; }
            set { _PSN_ExpiryDate = value; }
        }
        /// <summary>
        /// 延续专业
        /// </summary>
        public string PSN_RenewalProfession
        {
            get { return _PSN_RenewalProfession; }
            set { _PSN_RenewalProfession = value; }
        }
        /// <summary>
        /// 增项专业
        /// </summary>
        public string PSN_AddProfession
        {
            get { return _PSN_AddProfession; }
            set { _PSN_AddProfession = value; }
        }
        /// <summary>
        /// 申请注销人
        /// </summary>
        public string PSN_CancelPerson
        {
            get { return _PSN_CancelPerson; }
            set { _PSN_CancelPerson = value; }
        }
        /// <summary>
        /// 申请注销原因
        /// </summary>
        public string PSN_CancelReason
        {
            get { return _PSN_CancelReason; }
            set { _PSN_CancelReason = value; }
        }
        /// <summary>
        /// 补办原因
        /// </summary>
        public string PSN_ReReasons
        {
            get { return _PSN_ReReasons; }
            set { _PSN_ReReasons = value; }
        }
        /// <summary>
        /// 补办内容
        /// </summary>
        public string PSN_ReContent
        {
            get { return _PSN_ReContent; }
            set { _PSN_ReContent = value; }
        }
        /// <summary>
        /// 执业印章校验码
        /// </summary>
        public string PSN_CheckCode
        {
            get { return _PSN_CheckCode; }
            set { _PSN_CheckCode = value; }
        }
        /// <summary>
        /// 省份编码
        /// </summary>
        public string ENT_Province_Code
        {
            get { return _ENT_Province_Code; }
            set { _ENT_Province_Code = value; }
        }
        /// <summary>
        /// 证书等级：二级；二级临时
        /// </summary>
        public string PSN_Level
        {
            get { return _PSN_Level; }
            set { _PSN_Level = value; }
        }
        /// <summary>
        /// 资格证书编号
        /// </summary>
        public string ZGZSBH
        {
            get { return _ZGZSBH; }
            set { _ZGZSBH = value; }
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
        /// 最后修改人
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
        /// 有效标志:0无效；1有效
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
    }
}
