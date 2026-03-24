using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--UnitMDL填写类描述
	/// </summary>
	[Serializable]
	public class UnitMDL
	{
		public UnitMDL()
		{			
		}
			
		//主键
		protected string _UnitID;
		
		//其它属性
		protected DateTime? _BeginTime;
		protected DateTime? _EndTime;
		protected string _ENT_Name;
		protected string _ENT_OrganizationsCode;
		protected string _ENT_Economic_Nature;
		protected string _ENT_Province;
		protected string _ENT_Province_Code;
		protected string _ENT_City;
		protected string _END_Addess;
		protected string _ENT_Corporate;
		protected string _ENT_Correspondence;
		protected string _ENT_Postcode;
		protected string _ENT_Contact;
		protected string _ENT_Telephone;
		protected string _ENT_MobilePhone;
		protected string _ENT_Type;
		protected string _ENT_Sort;
		protected string _ENT_Grade;
		protected string _ENT_QualificationCertificateNo;
		protected string _CreditCode;
		protected string _CJR;
		protected DateTime? _CJSJ;
		protected string _XGR;
		protected DateTime? _XGSJ;
		protected int? _Valid;
		protected string _Memo;
        protected int? _ResultGSXX;
        protected DateTime? _ApplyTimeGSXX;
        
        /// <summary>
        /// 企业ID
        /// </summary>
        public string UnitID
		{
			get {return _UnitID;}
			set {_UnitID = value;}
		}
        /// <summary>
        /// 数据有效开始时间
        /// </summary>
		public DateTime? BeginTime
		{
			get {return _BeginTime;}
			set {_BeginTime = value;}
		}
        /// <summary>
        /// 数据有效截止时间
        /// </summary>
		public DateTime? EndTime
		{
			get {return _EndTime;}
			set {_EndTime = value;}
		}
        /// <summary>
        /// 企业名称
        /// </summary>
		public string ENT_Name
		{
			get {return _ENT_Name;}
			set {_ENT_Name = value;}
		}
        /// <summary>
        /// 组织机构代码
        /// </summary>
		public string ENT_OrganizationsCode
		{
			get {return _ENT_OrganizationsCode;}
			set {_ENT_OrganizationsCode = value;}
		}
        /// <summary>
        /// 企业性质：有限责任公司，股份有限公司,国有企业,集体企业.....
        /// </summary>
		public string ENT_Economic_Nature
		{
			get {return _ENT_Economic_Nature;}
			set {_ENT_Economic_Nature = value;}
		}
        /// <summary>
        /// 隶属省份
        /// </summary>
		public string ENT_Province
		{
			get {return _ENT_Province;}
			set {_ENT_Province = value;}
		}
        /// <summary>
        /// 省份编码
        /// </summary>
		public string ENT_Province_Code
		{
			get {return _ENT_Province_Code;}
			set {_ENT_Province_Code = value;}
		}
        /// <summary>
        /// 隶属区县
        /// </summary>
		public string ENT_City
		{
			get {return _ENT_City;}
			set {_ENT_City = value;}
		}
        /// <summary>
        /// 企业工商注册地址
        /// </summary>
		public string END_Addess
		{
			get {return _END_Addess;}
			set {_END_Addess = value;}
		}
        /// <summary>
        /// 企业法人
        /// </summary>
		public string ENT_Corporate
		{
			get {return _ENT_Corporate;}
			set {_ENT_Corporate = value;}
		}
        /// <summary>
        /// 企业通讯地址
        /// </summary>
		public string ENT_Correspondence
		{
			get {return _ENT_Correspondence;}
			set {_ENT_Correspondence = value;}
		}
        /// <summary>
        /// 邮政编码
        /// </summary>
		public string ENT_Postcode
		{
			get {return _ENT_Postcode;}
			set {_ENT_Postcode = value;}
		}
        /// <summary>
        /// 联系人
        /// </summary>
		public string ENT_Contact
		{
			get {return _ENT_Contact;}
			set {_ENT_Contact = value;}
		}
        /// <summary>
        /// 联系电话
        /// </summary>
		public string ENT_Telephone
		{
			get {return _ENT_Telephone;}
			set {_ENT_Telephone = value;}
		}
        /// <summary>
        /// 联系手机
        /// </summary>
		public string ENT_MobilePhone
		{
			get {return _ENT_MobilePhone;}
			set {_ENT_MobilePhone = value;}
		}
        /// <summary>
        /// 企业类型：本地施工企业，本地监理企业，本地造价咨询企业，本地招标代理机构，设计施工一体化.....
        /// </summary>
		public string ENT_Type
		{
			get {return _ENT_Type;}
			set {_ENT_Type = value;}
		}
        /// <summary>
        /// 企业资质类别
        /// </summary>
		public string ENT_Sort
		{
			get {return _ENT_Sort;}
			set {_ENT_Sort = value;}
		}
        /// <summary>
        /// 企业资质等级
        /// </summary>
		public string ENT_Grade
		{
			get {return _ENT_Grade;}
			set {_ENT_Grade = value;}
		}
        /// <summary>
        /// 企业资质证书编号
        /// </summary>
		public string ENT_QualificationCertificateNo
		{
			get {return _ENT_QualificationCertificateNo;}
			set {_ENT_QualificationCertificateNo = value;}
		}
        /// <summary>
        /// 社会统一信用代码
        /// </summary>
		public string CreditCode
		{
			get {return _CreditCode;}
			set {_CreditCode = value;}
		}
        /// <summary>
        /// 创建人
        /// </summary>
		public string CJR
		{
			get {return _CJR;}
			set {_CJR = value;}
		}
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CJSJ
		{
			get {return _CJSJ;}
			set {_CJSJ = value;}
		}
        /// <summary>
        /// 最后修改人
        /// </summary>
		public string XGR
		{
			get {return _XGR;}
			set {_XGR = value;}
		}
        /// <summary>
        /// 最后修改时间
        /// </summary>
		public DateTime? XGSJ
		{
			get {return _XGSJ;}
			set {_XGSJ = value;}
		}
        /// <summary>
        /// 有效标志：1有效，0无效
        /// </summary>
		public int? Valid
		{
			get {return _Valid;}
			set {_Valid = value;}
		}
        /// <summary>
        /// 备注
        /// </summary>
		public string Memo
		{
			get {return _Memo;}
			set {_Memo = value;}
		}

        /// <summary>
        /// 工商比对结果
        /// 0：未对比
        /// 1：对比不同过
        /// 2：对比通过
        /// </summary>
        public int? ResultGSXX
        {
            get { return _ResultGSXX; }
            set { _ResultGSXX = value; }
        }
        /// <summary>
        /// 工商申请比对时间
        /// </summary>
        public DateTime? ApplyTimeGSXX
        {
            get { return _ApplyTimeGSXX; }
            set { _ApplyTimeGSXX = value; }
        }
    }
}
