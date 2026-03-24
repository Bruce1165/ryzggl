using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ZYRYData
    {
        //证照名称	
        public const string _zsName = "北京市二级建造师注册证书";
        public const string _Website = "zjw.beijing.gov.cn";
        public const string _YXQFW = "三年";
        //证照名称	是
        public string ZSName
        {
            get { return _zsName; }
        }
        //查询网址  是
        public string Website
        {
            get { return _Website; }
        }
        //有效期范围	是
        public string YXQFW
        {
            get { return _YXQFW; }
        }
        //public string ZSName { get; set; }
        //企业统一信用代码或工商注册号 是
        public string TyshxydmORGs { get; set; }
        //证书状态（1：有效 0：无效）是
        public int ZSZT { get; set; }
        //注册号	是
        public string RegisterNo { get; set; }
        //执业资格证书编号	是
        public string ZyzgzsNo { get; set; }
        //姓名	是
        public string PSN_Name { get; set; }
        //企业名称	是
        public string QY_Name { get; set; }
        //证书有效期	是
        public string ZSValidDate { get; set; }
        //审批时间	是
        public string ApprovalDate { get; set; }
        //证件类型	是
        public string CertificatesType { get; set; }
        //证件号码	是
        public string CertificatesNo { get; set; }
        //专业一 是
        public string Major1 { get; set; }
        //专业一有效期 是
        public string Major1Valid { get; set; }

        //专业二
        public string Major2 { get; set; }
        //专业二有效期
        public string Major2Valid { get; set; }
        //专业三
        public string Major3 { get; set; }
        //专业三有效期
        public string Major3Valid { get; set; }
        //专业四
        public string Major4 { get; set; }
        //专业四有效期
        public string Major4Valid { get; set; }
        //专业五
        public string Major5 { get; set; }
        //专业五有效期
        public string Major5Valid { get; set; }
        //发证日期	是
        public string IssueDate { get; set; }
        //证照颁发机构	是
        public string FZJG { get; set; }
        //证照颁发机构代码	ZZBFJGDM 是
        public string ZZBFJGDM { get; set; }
        //证照颁发机构级别	ZZBFJGJB 是
        public string ZZBFJGJB { get; set; }
        //证照颁发日期	是
        public string FZRQ { get; set; }
        //区代码	是
        public string QDM { get; set; }
        //持证主体	是
        public string CertificateHolder { get; set; }
        //持证主体代码	是
        public string CertificateHolderCode { get; set; }

        //持证主体代码类型	是
        public string CertificateHolderType { get; set; }
        //持证主体代码类型代码	是
        public string CertificateHolderTypeCode { get; set; }
        //证书有效期起始时间	是
        public string ExpiryBeginDate { get; set; }
        //证书有效期截止时间	是
        public string ExpiryEndDate { get; set; }
        //证照名称	
        public string ZZName { get; set; }
        //证照类型代码	
        public string ZZTypeDM { get; set; }
        //证照编号	是
        public string ZZBH { get; set; }
        //证照标识	
        public string IdentifierCode { get; set; }
        //性别 是
        public string Sex { get; set; }
        //出生日期	是
        public string Birthday { get; set; }
        //制证日期 
        public string MakeDate { get; set; }
        //个人签名	
        public string PSN_Autograph { get; set; }
        //签名日期	
        public string PSN_AutographDate { get; set; }
        //证件照	是
        public string PSN_Photo { get; set; }



    }
}
