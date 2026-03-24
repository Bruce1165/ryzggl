using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Model
{
    /// <summary>
    /// 好差评服务数据上传对象
    /// </summary>
    [Serializable]
    public class AppraiseMDL
    {
        /// <summary>
        /// 调用方标识
        /// </summary>
        [JsonProperty("appMark")]
        public string appMark { set; get; }

        /// <summary>
        /// 时间戳，样例：20200628104925
        /// </summary>
        [JsonProperty("time")]
        public string time { set; get; }

        /// <summary>
        /// SM2 加密签名，参数内容为：SM2(time+appMark+appWord)即将当前时间、第三方标识、第三方密钥按顺序连接并进行 SM2 加密。注：1、参数 appMark、appWord 由【管理员】提供；
        /// </summary>
        [JsonProperty("sign")]
        public string sign { set; get; }

        /// <summary>
        /// 业务参数 json 格式，详情见下面的服务信息由{ "data":[{}]},将[{}]进行 sm2 加密 ， data 具 体 构 成 为[{"projectService":{办件数据信息}}]。
        /// </summary>
        [JsonProperty("params")]
        public string Params { set; get; }

    }

    /// <summary>
    /// 好差评服务数据上传结果
    /// </summary>
    [Serializable]
    public class AppraiseResultMDL
    {
        /// <summary>
        /// 请求状态 成 功 true ，失败false
        /// </summary>
        [JsonProperty("success")]
        public Boolean success { set; get; }

        /// <summary>
        /// 成功或者失败的code错误码：状态码：-1 系统异常、200 系统正常、404 请求路径不存在等。
        /// </summary>
        [JsonProperty("code")]
        public string code { set; get; }

        /// <summary>
        /// 请求失败返回的提示信息
        /// </summary>
        [JsonProperty("errorMessage")]
        public object errorMessage { set; get; }

        /// <summary>
        /// 服务编号：入参是服务信息时返 回 （ 入 参isService 值 为 1时）
        /// </summary>
        [JsonProperty("serviceCode")]
        public string serviceCode { set; get; }

        /// <summary>
        /// 服务次数：入参是服务信息时返 回 （ 入 参isService 值 为 1时）
        /// </summary>
        [JsonProperty("serviceNumber")]
        public string serviceNumber { set; get; }
    }

      /// <summary>
    /// 好差评服务信息
    /// </summary>
    [Serializable]
    public class projectServiceParams
    {
        [JsonProperty("projectService")]
        public projectServiceMDL projectService { set; get; }
    }
    /// <summary>
    /// 好差评服务信息
    /// </summary>
    [Serializable]
    public class projectServiceMDL
    {
        /// <summary>
        /// 实施编码
        /// </summary>
        [JsonProperty("taskCode")]
        public string taskCode { set; get; }

        /// <summary>
        /// 业务办理项编码
        /// </summary>
        [JsonProperty("taskHandleItem")]
        public string taskHandleItem { set; get; }

        /// <summary>
        /// 事项名称
        /// </summary>
        [JsonProperty("taskName")]
        public string taskName { set; get; }

        /// <summary>
        /// 事项主题
        /// </summary>
        [JsonProperty("subMatter")]
        public string subMatter { set; get; }

        /// <summary>
        /// 办件编号
        /// </summary>
        [JsonProperty("projectNo")]
        public string projectNo { set; get; }

        /// <summary>
        /// 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        /// </summary>
        [JsonProperty("proStatus")]
        public string proStatus { set; get; }

        /// <summary>
        /// 受理部门编码
        /// </summary>
        [JsonProperty("orgcode")]
        public string orgcode { set; get; }

        /// <summary>
        /// 受理部门名称
        /// </summary>
        [JsonProperty("orgName")]
        public string orgName { set; get; }

        /// <summary>
        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        /// </summary>
        [JsonProperty("acceptDate")]
        public string acceptDate { set; get; }

        /// <summary>
        /// 申请人类型 自然人=1，企业法人=2，事业法人=3，社会组织法人=4，非法人企业=5，行政机关=6，其他组织=9
        /// </summary>
        [JsonProperty("userProp")]
        public string userProp { set; get; }

        /// <summary>
        /// 申请单位名称/申请人名称
        /// </summary>
        [JsonProperty("userName")]
        public string userName { set; get; }

        /// <summary>
        /// 申请人证件类型（见 A.2证件类型）
        /// </summary>
        [JsonProperty("userPageType")]
        public string userPageType { set; get; }

        /// <summary>
        /// 申请人证件号码
        /// </summary>
        [JsonProperty("certKey")]
        public string certKey { set; get; }

        /// <summary>
        /// 代理人姓名
        /// </summary>
        [JsonProperty("handleUserName")]
        public string handleUserName { set; get; }

        /// <summary>
        /// 代理人证件类型（见 A.2证件类型）
        /// </summary>
        [JsonProperty("handleUserPageType")]
        public string handleUserPageType { set; get; }

        /// <summary>
        /// 代理人证件号码
        /// </summary>
        [JsonProperty("handleUserPageCode")]
        public string handleUserPageCode { set; get; }

        /// <summary>
        /// 服务名称 咨询、申报、补正、缴费、签收（isService 值为1 时必填）
        /// </summary>
        [JsonProperty("serviceName")]
        public string serviceName { set; get; }

        /// <summary>
        /// 是否为好差评服务办件1-是，0-否（当产生咨询、申报、补正、缴费及办结等服务时，该字段值为 1）
        /// </summary>
        [JsonProperty("isService")]
        public string isService { set; get; }

        /// <summary>
        /// 服务时间 yyyy-MM-dd HH:mm:ss （isService 值为1 时必填）
        /// </summary>
        [JsonProperty("serviceTime")]
        public string serviceTime { set; get; }

        /// <summary>
        /// 服务日期 yyyy-MM-dd（isService 值为1 时必填）
        /// </summary>
        [JsonProperty("serviceDate")]
        public string serviceDate { set; get; }

        /// <summary>
        /// 数据来源（默认值为 111）
        /// </summary>
        [JsonProperty("dataSource")]
        public string dataSource { set; get; }

        /// <summary>
        /// 办件类型： 即办件=1，承诺办件=2
        /// </summary>
        [JsonProperty("projectType")]
        public int projectType { set; get; }

        /// <summary>
        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        /// </summary>
        [JsonProperty("resultDate")]
        public string resultDate { set; get; }

        /// <summary>
        /// 申请时间 办件申请、补正必填
        /// </summary>
        [JsonProperty("applydate")]
        public string applydate { set; get; }

        /// <summary>
        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
        /// </summary>
        [JsonProperty("bj_zt")]
        public string bj_zt { set; get; }

        /// <summary>
        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        /// </summary>
        [JsonProperty("taskguid")]
        public string taskguid { set; get; }

        /// <summary>
        /// 办件详情查看地址（需要是对接了统一认证平台的单点登录地址）
        /// </summary>
        [JsonProperty("statusUrl")]
        public string statusUrl { set; get; }

        /// <summary>
        /// 行政区划（见 A.3 北京市行政区划代码）
        /// </summary>
        [JsonProperty("administrative_div")]
        public string administrative_div { set; get; }
        /// <summary>
        /// 联系人手机号
        /// </summary>
        [JsonProperty("telPhone")]
        public string telPhone { set; get; }

        /// <summary>
        /// 服务数据初始来源标识，默认与 appMark 值相同
        /// </summary>
        [JsonProperty("source")]
        public string source { set; get; }

        /// <summary>
        /// 服务数据来源，默认与appMark 值相同
        /// </summary>
        [JsonProperty("sourceTJ")]
        public string sourceTJ { set; get; }

        /// <summary>
        /// 位行政区划（非太极不用填写，太极必填）
        /// </summary>
        [JsonProperty("areaCode")]
        public string areaCode { set; get; }

        /// <summary>
        /// 评价对应事项类型：行政许可=01，行政给付=05，行政 确认=07，行政奖励=08，行政裁决 =09，其他行政权力=10，公共服务 =20，其它类=99 
        /// </summary>
        [JsonProperty("taskType")]
        public string taskType { set; get; }
    }
}
