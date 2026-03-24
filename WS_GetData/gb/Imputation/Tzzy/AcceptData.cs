using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Imputation.Tzzy
{
    /// <summary>
    ///  请求数据
    /// </summary>
    [Serializable]
    public class AcceptData
    {
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        [JsonProperty("eCertID")]
        public string ECertID { get; set; }

        /// <summary>
        /// 所属省份(按照民政部官网《2020年中华人民共和国行政区划代码》)
        /// </summary>
        [JsonProperty("provinceNum")]
        public string ProvinceNum { get; set; }

        /// <summary>
        /// 证书编号(证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成)
        /// </summary>
        [JsonProperty("certNum")]
        public string CertNum { get; set; }

        /// <summary>
        /// 发证机关(负责审核、签发该特种作业操作资格证书的省、自治区、直辖市人民政府住房城乡建设主管部门的全称或规范性简称)
        /// </summary>
        [JsonProperty("issuAuth")]
        public string IssuAuth { get; set; }

        /// <summary>
        /// 发证机关代码(发证机关的统一社会信用代码)
        /// </summary>
        [JsonProperty("issuAuthCode")]
        public string IssuAuthCode { get; set; }

        /// <summary>
        /// 初次领证日期(初次领取该特种作业操作资格证书的日期，按公元纪年精确至日)
        /// </summary>
        [JsonProperty("issuedDate")]
        public string IssuedDate { get; set; }

        /// <summary>
        /// 发证日期(签发该特种作业操作资格证书的日期，按公元纪年精确至日)
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
        /// 照片(Base64字符串，照片为jpg、png格式，分辨率200-300dpi，大小不超过50KB)
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; }

        /// <summary>
        /// 工种类别
        /// 01	建筑电工
        /// 02	建筑架子工（普通脚手架）
        /// 03	建筑架子工（附着升降脚手架）
        /// 04	建筑起重司索信号工
        /// 05	建筑起重机械司机（塔式起重机）
        /// 06	建筑起重机械司机（施工升降机）
        /// 07	建筑起重机械司机（物料提升机）
        /// 08	建筑起重机械安装拆卸工（塔式起重机）
        /// 09	建筑起重机械安装拆卸工（施工升降机）
        /// 10	建筑起重机械安装拆卸工（物料提升机）
        /// 11	高处作业吊篮安装拆卸工
        /// 99	经省级以上住房和城乡建设主管部门认定的其他工种类别
        /// </summary>
        [JsonProperty("operationCategory")]
        public string OperationCategory { get; set; }

        /// <summary>
        /// 工种类别描述，当操作类别为经省级以上住房和城乡建设主管部门认定的其他工种类别时，应对具体操作类别进行的补充描述，固定格式为：“经省级以上住房和城乡建设主管部门认定的其他工种类别（XXX）”
        /// </summary>
        [JsonProperty("categoryDescription")]
        public string CategoryDescription { get; set; }

        /// <summary>
        /// 证书状态(工种类别为“99”时进行的补充描述)
        /// 01	有效
        /// 02	暂扣
        /// 03	吊销
        /// 04	撤销
        /// 05	注销
        /// 06	失效
        /// 99	其他
        /// </summary>
        [JsonProperty("certState")]
        public string CertState { get; set; }

        /// <summary>
        /// 证书状态描述(详见7.2.3.1证书状态字典表)
        /// </summary>
        [JsonProperty("certStatusDescription")]
        public string CertStatusDescription { get; set; }

        /// <summary>
        /// 关联证照标识，该证书关联的最近一个已失效建筑施工特种作业操作资格证书的电子证照标识，规则同表5-2-4中的“证照标识”，当操作类型是【办理延续】、【办理变更时】，需要必填
        /// </summary>
        [JsonProperty("associatedZzeCertID")]
        public string AssociatedZzeCertID { get; set; }

        /// <summary>
        /// 业务信息(按JSON串方式（第一个参数为类型，第二个参数为业务信息）组织的对应业务类型代码的具体业务信息，详见7.2.3.3扩展信息类型字典表)
        /// </summary>
        [JsonProperty("businessInformation")]
        public List<BusinessInformation> BusinessInformation { get; set; }

        /// <summary>
        /// 操作类型(操作类型为“01”或“02”或“08”时会进行二维码赋码)
        /// 01	办理新发电子证照
        /// 02	办理延续
        /// 03	办理暂扣
        /// 04	办理过期失效
        /// 05	办理注销（撤销、吊销）
        /// 06	办理暂扣发还
        /// 07	办理其他业务
        /// 08	办理变更
        /// </summary>
        [JsonProperty("operateType")]
        public string OperateType { get; set; }
    }
}
