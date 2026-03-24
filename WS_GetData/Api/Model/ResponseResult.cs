using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     返回公共参数
    /// </summary>
    [Serializable]
    public class ResponseResult
    {
        /// <summary>
        ///     接口调用是否成功
        ///     <remarks>SUCCESS：成功，FAILURE：失败</remarks>
        /// </summary>
        [JsonProperty("ack_code")]
        public string AckCode { set; get; }

        /// <summary>
        ///     错误信息
        /// </summary>
        [JsonProperty("errors")]
        public List<Error> Errors { set; get; }

        /// <summary>
        ///     请求参数签名值（预留）
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { set; get; }

        /// <summary>
        ///     签名方法（预留）
        /// </summary>
        [JsonProperty("sign_method")]
        public string SignMethod { set; get; }

        /// <summary>
        ///     接口响应的服务端时间。
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { set; get; }

        /// <summary>
        ///     对应的请求 request_id。如果请求提供了request_id 参数，在响应时会原样返回。
        /// </summary>
        [JsonProperty("correlation_id")]
        public string CorrelationId { set; get; }

        /// <summary>
        ///     接口返回的唯一标识号
        /// </summary>
        [JsonProperty("response_id")]
        public string ResponseId { set; get; }
    }

    /// <summary>
    /// </summary>
    [Serializable]
    public class LoginResponseResult : ResponseResult
    {
        /// <summary>
        ///     访问令牌。成功调用 login 接口后返回
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { set; get; }
    }

    /// <summary>
    ///     创建制证数据返回结果
    /// </summary>
    [Serializable]
    public class CreateResponseResult : ResponseResult
    {
        /// <summary>
        ///     创建制证数据返回结果
        /// </summary>
        [JsonProperty("data")]
        public CreateResponseMdl Data { set; get; }
    }

    /// <summary>
    ///     下载签章附件返回结果
    /// </summary>
    [Serializable]
    public class DownResponseResult : ResponseResult
    {
        /// <summary>
        ///     创建制证数据返回结果
        /// </summary>
        [JsonProperty("data")]
        public DowResponseMdl Data { set; get; }
    }

    /// <summary>
    ///     签发一张证照数据返回结果
    /// </summary>
    public class SignResponseResult : ResponseResult
    {
    }

    /// <summary>
    /// </summary>
    public class AbolishResponseResult : ResponseResult
    {
    }
}