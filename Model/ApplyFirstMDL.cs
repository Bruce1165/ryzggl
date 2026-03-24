using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ApplyFirstMDL填写类描述
    /// </summary>
    [Serializable]
    public class ApplyFirstMDL
    {
        public ApplyFirstMDL()
        {
        }

        //主键
        protected string _ApplyID;

        //其它属性
        protected string _ArchitectType;
        protected string _PSN_Telephone;
        protected string _PSN_MobilePhone;
        protected string _PSN_Email;
        protected string _Nation;
        protected DateTime? _Birthday;
        protected string _School;
        protected string _Major;
        protected DateTime? _GraduationTime;
        protected string _XueLi;
        protected string _XueWei;
        protected string _FR;
        protected string _LinkMan;
        protected string _ENT_Telephone;
        protected string _ENT_Correspondence;
        protected string _ENT_Economic_Nature;
        protected string _END_Addess;
        protected string _ENT_Postcode;
        protected string _ENT_Type;
        protected string _ENT_Sort;
        protected string _ENT_Grade;
        protected string _ENT_QualificationCertificateNo;
        protected string _ZhuanYe;
        protected string _GetType;
        protected string _PSN_ExamCertCode;
        protected DateTime? _ConferDate;
        protected string _ApplyRegisteProfession;
        protected int? _BiXiu;
        protected int? _XuanXiu;
        protected string _ExamInfo;
        protected string _OtherCert;
        protected bool? _IfSameUnit;
        protected string _MainJob;

        public string ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
        }
        public string ArchitectType
        {
            get { return _ArchitectType; }
            set { _ArchitectType = value; }
        }
        public string PSN_Telephone
        {
            get { return _PSN_Telephone; }
            set { _PSN_Telephone = value; }
        }

        public string PSN_MobilePhone
        {
            get { return _PSN_MobilePhone; }
            set { _PSN_MobilePhone = value; }
        }

        public string PSN_Email
        {
            get { return _PSN_Email; }
            set { _PSN_Email = value; }
        }

        public string Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }

        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        public string School
        {
            get { return _School; }
            set { _School = value; }
        }

        public string Major
        {
            get { return _Major; }
            set { _Major = value; }
        }

        public DateTime? GraduationTime
        {
            get { return _GraduationTime; }
            set { _GraduationTime = value; }
        }

        public string XueLi
        {
            get { return _XueLi; }
            set { _XueLi = value; }
        }

        public string XueWei
        {
            get { return _XueWei; }
            set { _XueWei = value; }
        }

        public string FR
        {
            get { return _FR; }
            set { _FR = value; }
        }

        public string LinkMan
        {
            get { return _LinkMan; }
            set { _LinkMan = value; }
        }

        public string ENT_Telephone
        {
            get { return _ENT_Telephone; }
            set { _ENT_Telephone = value; }
        }

        public string ENT_Correspondence
        {
            get { return _ENT_Correspondence; }
            set { _ENT_Correspondence = value; }
        }

        public string ENT_Economic_Nature
        {
            get { return _ENT_Economic_Nature; }
            set { _ENT_Economic_Nature = value; }
        }

        public string END_Addess
        {
            get { return _END_Addess; }
            set { _END_Addess = value; }
        }

        public string ENT_Postcode
        {
            get { return _ENT_Postcode; }
            set { _ENT_Postcode = value; }
        }

        public string ENT_Type
        {
            get { return _ENT_Type; }
            set { _ENT_Type = value; }
        }

        public string ENT_Sort
        {
            get { return _ENT_Sort; }
            set { _ENT_Sort = value; }
        }

        public string ENT_Grade
        {
            get { return _ENT_Grade; }
            set { _ENT_Grade = value; }
        }

        public string ENT_QualificationCertificateNo
        {
            get { return _ENT_QualificationCertificateNo; }
            set { _ENT_QualificationCertificateNo = value; }
        }

        public string ZhuanYe
        {
            get { return _ZhuanYe; }
            set { _ZhuanYe = value; }
        }

        public string GetType
        {
            get { return _GetType; }
            set { _GetType = value; }
        }

        public string PSN_ExamCertCode
        {
            get { return _PSN_ExamCertCode; }
            set { _PSN_ExamCertCode = value; }
        }

        public DateTime? ConferDate
        {
            get { return _ConferDate; }
            set { _ConferDate = value; }
        }

        public string ApplyRegisteProfession
        {
            get { return _ApplyRegisteProfession; }
            set { _ApplyRegisteProfession = value; }
        }

        public int? BiXiu
        {
            get { return _BiXiu; }
            set { _BiXiu = value; }
        }

        public int? XuanXiu
        {
            get { return _XuanXiu; }
            set { _XuanXiu = value; }
        }

        public string ExamInfo
        {
            get { return _ExamInfo; }
            set { _ExamInfo = value; }
        }

        public string OtherCert
        {
            get { return _OtherCert; }
            set { _OtherCert = value; }
        }

        public bool? IfSameUnit
        {
            get { return _IfSameUnit; }
            set { _IfSameUnit = value; }
        }

        public string MainJob
        {
            get { return _MainJob; }
            set { _MainJob = value; }
        }
    }
}
