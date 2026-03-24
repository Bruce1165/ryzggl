using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Imputation.Aqscglry
{
    /// <summary>
    /// 二维码赋码请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        /// 数据唯一标识，32位全球唯一标识符
        /// </summary>
        [JsonProperty("eCertID")]
        public string ECertID { get; set; }

        /// <summary>
        /// 所属省份(行政区划代码)
        /// </summary>
        [JsonProperty("provinceNum")]
        public string ProvinceNum { get; set; }
        /// <summary>
        /// 证书编号(证书唯一编号)
        /// </summary>
        [JsonProperty("certNum")]
        public string CertNum { get; set; }

        /// <summary>
        /// 发证机关
        /// </summary>
        [JsonProperty("issuAuth")]
        public string IssuAuth { get; set; }

        /// <summary>
        /// 发证机关代码(发证机关的统一社会信用代码)
        /// </summary>
        [JsonProperty("issuAuthCode")]
        public string IssuAuthCode { get; set; }

        /// <summary>
        /// 初次领证日期(初次领取该证书的日期，按公元纪年精确至日)
        /// </summary>
        [JsonProperty("issuedDate")]
        public string IssuedDate { get; set; }

        /// <summary>
        /// 发证日期(签发该证书的日期，按公元纪年精确至日)
        /// </summary>
        [JsonProperty("issuDate")]
        public string IssuDate { get; set; }
        /// <summary>
        /// 有效期起始日期(该证照有效期的起始日期，按照公元纪年精确至日)
        /// </summary>
        [JsonProperty("effectiveDate")]
        public string EffectiveDate { get; set; }

        /// <summary>
        /// 有效期结束日期(证照有效期的结束日期，按照公元纪年精确至日)
        /// </summary>
        [JsonProperty("expiringDate")]
        public string ExpiringDate { get; set; }

        /// <summary>
        /// 岗位类别代码A，B，C1，C2，C3
        /// </summary>
        [JsonProperty("categoryCode")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 姓名(特种作业操作资格证书持证人员的姓名)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 性别(特种作业操作资格证书持证人员的性别1表示男，2表示女)
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }
        /// <summary>
        /// 身份证件号码(持证人员的有效身份证件号)
        /// </summary>
        [JsonProperty("identityCard")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 身份证件号码类型(持证人员的有效身份证件号码类型，可取值为公民身份号码、护照号等，从GB/T 36903-2018的附录A中选取)
        /// </summary>
        [JsonProperty("identityCardType")]
        public string IdentityCardType { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 统一社会信用代码（持该证书的安管人员受聘企业的统一社会信用代码）
        /// </summary>
        [JsonProperty("creditCode")]
        public string CreditCode { get; set; }

        /// <summary>
        /// 职务（详见7.3.3.3职务字典表）
        /// </summary>
        [JsonProperty("appointment")]
        public string Appointment { get; set; }

        /// <summary>
        /// 技术职称（详见7.3.3.4技术职称字典表）
        /// </summary>
        [JsonProperty("technicalTitle")]
        public string TechnicalTitle { get; set; }

        /// <summary>
        /// 文化程度（详见7.3.3.5文化程度字典表）
        /// </summary>
        [JsonProperty("educationDegree")]
        public string EducationDegree { get; set; }

        /// <summary>
        /// 专业(持该证书的安管人员的专业 )
        /// </summary>
        [JsonProperty("major")]
        public string Major { get; set; }

        /// <summary>
        /// 照片(Base64字符串，照片为jpg、png格式，分辨率200-300dpi，大小不超过50KB)
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; }

        /// <summary>
        /// 证书状态（必填）
        /// 01	有效
        /// 02	暂扣
        /// 03	撤销
        /// 04	注销
        /// 05	失效
        /// 06	办理转出
        /// 07	吊销
        /// 99	其他
        /// </summary>
        [JsonProperty("certState")]
        public string CertState { get; set; }

        /// <summary>
        /// 证书状态描述(当证书状态为“99”其他时，必填,进行的补充描述)。
        /// </summary>
        [JsonProperty("certStatusDescription")]
        public string CertStatusDescription { get; set; }

        /// <summary>
        /// 关联证照标识(该证书关联的最近一个已失效建筑施工企业安全生产管理人员考核合格证书电子证照标识，规则同表7-2-3中的“证照标识”，当操作类型是【办理延续】、【办理变更】、【办理转入】业务时，要求必填，其中办理转入时，传转出省原证书的证照标识)
        /// 格式参考："1.2.156.3005.2.11100000000013338W011.11430000006122244L.20230000403.001.U"
        /// </summary>
        [JsonProperty("associatedZzeCertID")]
        public string AssociatedZzeCertID { get; set; }

        /// <summary>
        /// 业务信息(按JSON串方式（第一个参数为类型，第二个参数为业务信息）组织的对应业务类型代码的具体业务信息，详见7.3.3.6扩展信息类型字典表)
        /// </summary>
        [JsonProperty("businessInformation")]
        public List<BusinessInformation> BusinessInformation { get; set; }

        /// <summary>
        /// 操作类型(详见7.3.3.7操作类型字典表和7.3.3.8证书状态与操作类型关系表，操作类型为“01”或“02”时会进行二维码赋码)
        /// 操作类型，详见6.3-操作类型代码表，6.3-证书状态与操作类型关系代码表
        /// 01	办理新发电子证照
        /// 02	办理延续
        /// 03	办理暂扣
        /// 04	办理过期失效
        /// 05	办理注销（撤销、吊销、办理转出）
        /// 06	办理暂扣发还
        /// 07	办理其他业务
        /// 08	办理转入
        /// 09	办理变更
        /// 10  取消办理转出
        /// </summary>
        [JsonProperty("operateType")]
        public string OperateType { get; set; }

        /// <summary>
        /// 转出省原证照编号:当操作类型是办理转入时，要求必填
        /// </summary>
        [JsonProperty("oldcertNum")]
        public string oldcertNum { get; set; }
    }
}
