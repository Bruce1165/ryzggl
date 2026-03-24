using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyChangeMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyChangeMDL
	{
		public ApplyChangeMDL()
		{			
		}

        //主键
        protected string _ApplyID;

        //其它属性
        protected string _PSN_MobilePhone;
        protected string _PSN_Email;
        protected string _PSN_RegisterNo;
        protected DateTime? _ValidDate;
        protected string _ChangeReason;
        protected string _OldENT_Name;
        protected string _OldLinkMan;
        protected string _OldENT_Telephone;
        protected string _OldENT_Correspondence;
        protected string _OldENT_Postcode;
        protected string _ENT_Name;
        protected string _FR;
        protected string _LinkMan;
        protected string _ENT_Telephone;
        protected string _ENT_Correspondence;
        protected string _ENT_Postcode;
        protected string _END_Addess;
        protected bool? _IfOutside;
        protected string _ENT_Type;
        protected string _ENT_Economic_Nature;
        protected string _ENT_Sort;
        protected string _ENT_Grade;
        protected string _ENT_QualificationCertificateNo;
        protected string _ENT_Sort2;
        protected string _ENT_Grade2;
        protected string _ENT_QualificationCertificateNo2;
        protected string _ENT_NameFrom;
        protected string _ENT_NameTo;
        protected string _PSN_NameFrom;
        protected string _PSN_NameTo;
        protected string _FromENT_City;
        protected string _ToENT_City;
        protected string _FromPSN_Sex;
        protected string _ToPSN_Sex; 
        protected DateTime? _FromPSN_BirthDate;
        protected DateTime? _ToPSN_BirthDate;
        protected string _FromPSN_CertificateType;
        protected string _ToPSN_CertificateType;
        protected string _FromPSN_CertificateNO;
        protected string _ToPSN_CertificateNO;
        protected string _ZGZSBH;
        protected string _To_ZGZSBH;
        protected string _FromEND_Addess;
        protected string _ToEND_Addess;
        protected string _Nation;
       
        public string ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
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

        public string PSN_RegisterNo
        {
            get { return _PSN_RegisterNo; }
            set { _PSN_RegisterNo = value; }
        }

        public DateTime? ValidDate
        {
            get { return _ValidDate; }
            set { _ValidDate = value; }
        }

        public string ChangeReason
        {
            get { return _ChangeReason; }
            set { _ChangeReason = value; }
        }
        
        
        public string OldENT_Name
        {
            get { return _OldENT_Name; }
            set { _OldENT_Name = value; }
        }

        public string OldLinkMan
        {
            get { return _OldLinkMan; }
            set { _OldLinkMan = value; }
        }

        public string OldENT_Telephone
        {
            get { return _OldENT_Telephone; }
            set { _OldENT_Telephone = value; }
        }

        public string OldENT_Correspondence
        {
            get { return _OldENT_Correspondence; }
            set { _OldENT_Correspondence = value; }
        }

        public string OldENT_Postcode
        {
            get { return _OldENT_Postcode; }
            set { _OldENT_Postcode = value; }
        }

        public string ENT_Name
        {
            get { return _ENT_Name; }
            set { _ENT_Name = value; }
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

        public string ENT_Postcode
        {
            get { return _ENT_Postcode; }
            set { _ENT_Postcode = value; }
        }

        public string END_Addess
        {
            get { return _END_Addess; }
            set { _END_Addess = value; }
        }

        public bool? IfOutside
        {
            get { return _IfOutside; }
            set { _IfOutside = value; }
        }

        public string ENT_Type
        {
            get { return _ENT_Type; }
            set { _ENT_Type = value; }
        }

        public string ENT_Economic_Nature
        {
            get { return _ENT_Economic_Nature; }
            set { _ENT_Economic_Nature = value; }
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

        public string ENT_Sort2
        {
            get { return _ENT_Sort2; }
            set { _ENT_Sort2 = value; }
        }

        public string ENT_Grade2
        {
            get { return _ENT_Grade2; }
            set { _ENT_Grade2 = value; }
        }

        public string ENT_QualificationCertificateNo2
        {
            get { return _ENT_QualificationCertificateNo2; }
            set { _ENT_QualificationCertificateNo2 = value; }
        }

        public string ENT_NameFrom
        {
            get { return _ENT_NameFrom; }
            set { _ENT_NameFrom = value; }
        }

        public string ENT_NameTo
        {
            get { return _ENT_NameTo; }
            set { _ENT_NameTo = value; }
        }

        public string PSN_NameFrom
        {
            get { return _PSN_NameFrom; }
            set { _PSN_NameFrom = value; }
        }

        public string PSN_NameTo
        {
            get { return _PSN_NameTo; }
            set { _PSN_NameTo = value; }
        }

        public string FromENT_City
        {
            get { return _FromENT_City; }
            set { _FromENT_City = value; }
        }

        public string ToENT_City
        {
            get { return _ToENT_City; }
            set { _ToENT_City = value; }
        }

        public string FromPSN_Sex
        {
            get { return _FromPSN_Sex; }
            set { _FromPSN_Sex = value; }
        }

        public string ToPSN_Sex
        {
            get { return _ToPSN_Sex; }
            set { _ToPSN_Sex = value; }
        }

        public DateTime? FromPSN_BirthDate
        {
            get { return _FromPSN_BirthDate; }
            set { _FromPSN_BirthDate = value; }
        }

        public DateTime? ToPSN_BirthDate
        {
            get { return _ToPSN_BirthDate; }
            set { _ToPSN_BirthDate = value; }
        }

        public string FromPSN_CertificateType
        {
            get { return _FromPSN_CertificateType; }
            set { _FromPSN_CertificateType = value; }
        }

        public string ToPSN_CertificateType
        {
            get { return _ToPSN_CertificateType; }
            set { _ToPSN_CertificateType = value; }
        }

        public string FromPSN_CertificateNO
        {
            get { return _FromPSN_CertificateNO; }
            set { _FromPSN_CertificateNO = value; }
        }

        public string ToPSN_CertificateNO
        {
            get { return _ToPSN_CertificateNO; }
            set { _ToPSN_CertificateNO = value; }
        }
        public string ZGZSBH
        {
            get { return _ZGZSBH; }
            set { _ZGZSBH = value; }
        }

        public string To_ZGZSBH
        {
            get { return _To_ZGZSBH; }
            set { _To_ZGZSBH = value; }
        }
        public string FromEND_Addess
        {
            get { return _FromEND_Addess; }
            set { _FromEND_Addess = value; }
        }

        public string ToEND_Addess
        {
            get { return _ToEND_Addess; }
            set { _ToEND_Addess = value; }
        }
        public string Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }
    }
}