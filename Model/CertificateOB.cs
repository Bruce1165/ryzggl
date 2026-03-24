using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--CertificateOB填写类描述
    /// </summary>
    [Serializable]
    public class CertificateOB
    {
        public CertificateOB()
        {
            //默认值
        }

        //主键
        protected long? _CertificateID;

        //其它属性
        protected long? _ExamPlanID;
        protected long? _WorkerID;
        protected string _CertificateType;
        protected int? _PostTypeID;
        protected int? _PostID;
        protected string _CertificateCode;
        protected string _WorkerName;
        protected string _Sex;
        protected DateTime? _Birthday;
        protected string _UnitName;
        protected DateTime? _ConferDate;
        protected DateTime? _ValidStartDate;
        protected DateTime? _ValidEndDate;
        protected string _ConferUnit;
        protected string _Status;
        protected string _CheckMan;
        protected string _CheckAdvise;
        protected DateTime? _CheckDate;
        protected string _PrintMan;
        protected DateTime? _PrintDate;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _WorkerCertificateCode;
        protected string _UnitCode;
        protected string _ApplyMan;
        protected string _CaseStatus;
        protected string _AddItemName;
        protected string _Remark;
        protected string _SkillLevel;

        protected string _TrainUnitName;
        protected int? _PrintCount;
        protected string _FacePhoto;
        protected int? _PrintVer;
        protected string _PostTypeName;
        protected string _PostName;

        protected DateTime? _ApplyCATime;
        protected DateTime? _SendCATime;
        protected DateTime? _WriteJHKCATime;
        protected DateTime? _ReturnCATime;
        protected string _CertificateCAID;

        protected string _Job;
        protected DateTime? _ZACheckTime;
        protected int? _ZACheckResult;
        protected string _ZACheckRemark;

        protected DateTime? _EleCertErrTime;
        protected string _EleCertErrStep;
        protected string _EleCertErrDesc;

        protected DateTime? _QRCodeTime;
        protected DateTime? _ZZUrlUpTime;

        protected DateTime? _Ofd_ReturnCATime;

        /// <summary>
        /// 电子证书OFD签章完毕取回时间
        /// </summary>
        public DateTime? Ofd_ReturnCATime
        {
            get { return _Ofd_ReturnCATime; }
            set { _Ofd_ReturnCATime = value; }
        }

        /// <summary>
        /// 质安网数据赋码时间
        /// </summary>
        public DateTime? QRCodeTime
        {
            get { return _QRCodeTime; }
            set { _QRCodeTime = value; }
        }
        /// <summary>
        /// 质安网数据归档时间
        /// </summary>
        public DateTime? ZZUrlUpTime
        {
            get { return _ZZUrlUpTime; }
            set { _ZZUrlUpTime = value; }
        }


        /// <summary>
        /// 质安网数据比对时间
        /// </summary>
        public DateTime? EleCertErrTime
        {
            get { return _EleCertErrTime; }
            set { _EleCertErrTime = value; }
        }
        /// <summary>
        /// 质安网数据比对环节名称：校验，赋码，归档，任何一个环节比对失败，将失败原因赋给说明EleCertErrDesc。
        /// </summary>
        public string EleCertErrStep
        {
            get { return _EleCertErrStep; }
            set { _EleCertErrStep = value; }
        }
        /// <summary>
        /// 质安网数据比对结果说明
        /// </summary>
        public string EleCertErrDesc
        {
            get { return _EleCertErrDesc; }
            set { _EleCertErrDesc = value; }
        }



        /// <summary>
        /// 安管人员在受聘企业担任的职务
        /// </summary>
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
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
        /// 电子证书ID
        /// </summary>
        public string CertificateCAID
        {
            get { return _CertificateCAID; }
            set { _CertificateCAID = value; }
        }

        /// <summary>
        /// 申请电子证书时间
        /// </summary>
        public DateTime? ApplyCATime
        {
            get { return _ApplyCATime; }
            set { _ApplyCATime = value; }
        }

        /// <summary>
        /// 电子证书发送到DFS时间
        /// </summary>
        public DateTime? SendCATime
        {
            get { return _SendCATime; }
            set { _SendCATime = value; }
        }

        /// <summary>
        /// 电子证书写入中间交换库时间
        /// </summary>
        public DateTime? WriteJHKCATime
        {
            get { return _WriteJHKCATime; }
            set { _WriteJHKCATime = value; }
        }

        /// <summary>
        /// 电子证书签章完毕取回时间
        /// </summary>
        public DateTime? ReturnCATime
        {
            get { return _ReturnCATime; }
            set { _ReturnCATime = value; }
        }

        /// <summary>
        /// 岗位类别名称
        /// </summary>
        public string PostTypeName
        {
            get { return _PostTypeName; }
            set { _PostTypeName = value; }
        }

        /// <summary>
        /// 岗位工种名称
        /// </summary>
        public string PostName
        {
            get { return _PostName; }
            set { _PostName = value; }
        }

        /// <summary>
        /// 打印版本号
        /// </summary>
        public int? PrintVer
        {
            get { return _PrintVer; }
            set { _PrintVer = value; }
        }

        /// <summary>
        /// 一寸照片
        /// </summary>
        public string FacePhoto
        {
            get { return this._FacePhoto; }
            set { this._FacePhoto = value; }
        }

        /// <summary>
        /// 有效期内打印次数
        /// </summary>
        public int? PrintCount
        {
            get { return _PrintCount; }
            set { _PrintCount = value; }
        }

        /// <summary>
        /// 报考培训点
        /// </summary>
        public string TrainUnitName
        {
            get { return this._TrainUnitName; }
            set { this._TrainUnitName = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        /// <summary>
        /// 增项
        /// </summary>
        public string AddItemName
        {
            get { return _AddItemName; }
            set { _AddItemName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ApplyMan
        {
            get { return _ApplyMan; }
            set { _ApplyMan = value; }
        }
        /// <summary>
        /// 归档状态
        /// </summary>
        public string CaseStatus
        {
            get { return _CaseStatus; }
            set { _CaseStatus = value; }
        }

        /// <summary>
        /// 证书ID
        /// </summary>
        public long? CertificateID
        {
            get { return _CertificateID; }
            set { _CertificateID = value; }
        }

        /// <summary>
        /// >0：考试计划ID；-100：证书历初始化史数据；-200：证书补登记；-300：证书进京；-400：增发
        /// </summary>
        public long? ExamPlanID
        {
            get { return _ExamPlanID; }
            set { _ExamPlanID = value; }
        }

        /// <summary>
        /// 人员ID
        /// </summary>
        public long? WorkerID
        {
            get { return _WorkerID; }
            set { _WorkerID = value; }
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
        /// 岗位类别ID：1：安全生产考核三类人员，2：建筑施工特种作业，3：造价员，4：建设职业技能岗位，5：关键岗位专业技术管理人员，4000：新版建设职业技能岗位
        /// </summary>
        public int? PostTypeID
        {
            get { return _PostTypeID; }
            set { _PostTypeID = value; }
        }

        /// <summary>
        /// 岗位工种ID，147：企业主要负责人，148：项目负责人，6：土建类专职安全生产管理人员 ，1123 ：机械类专职安全生产管理人员，1125：综合类专职安全生产管理人员
        /// </summary>
        public int? PostID
        {
            get { return _PostID; }
            set { _PostID = value; }
        }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertificateCode
        {
            get { return _CertificateCode; }
            set { _CertificateCode = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string WorkerName
        {
            get { return _WorkerName; }
            set { _WorkerName = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
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

        /// <summary>
        /// 企业名称
        /// </summary>
        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime? ConferDate
        {
            get { return _ConferDate; }
            set { _ConferDate = value; }
        }

        /// <summary>
        /// 有效期起
        /// </summary>
        public DateTime? ValidStartDate
        {
            get { return _ValidStartDate; }
            set { _ValidStartDate = value; }
        }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? ValidEndDate
        {
            get { return _ValidEndDate; }
            set { _ValidEndDate = value; }
        }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string ConferUnit
        {
            get { return _ConferUnit; }
            set { _ConferUnit = value; }
        }
        /// <summary>
        /// 最后业务状态
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
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
        /// 审核意见
        /// </summary>
        public string CheckAdvise
        {
            get { return _CheckAdvise; }
            set { _CheckAdvise = value; }
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
        /// 打印人
        /// </summary>
        public string PrintMan
        {
            get { return _PrintMan; }
            set { _PrintMan = value; }
        }
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? PrintDate
        {
            get { return _PrintDate; }
            set { _PrintDate = value; }
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
        /// 证件号码
        /// </summary>
        public string WorkerCertificateCode
        {
            get { return _WorkerCertificateCode; }
            set { _WorkerCertificateCode = value; }
        }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }
        /// <summary>
        /// 技术职称(工人：技术等级)
        /// </summary>
        public string SkillLevel
        {
            get { return _SkillLevel; }
            set { _SkillLevel = value; }
        }
    }
}
