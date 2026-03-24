using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     下载制证数据返回结果
    /// </summary>
    [Serializable]
    public class DowResponseMdl
    {
        /// <summary>
        ///  pdf 文件名
        /// </summary>
        [JsonProperty("file_name")]
        public string FileName { set; get; }

        /// <summary>
        /// pdf 文件内容数据（base64 编码）
        /// </summary>
        [JsonProperty("file_data")]
        public string FileData { set; get; }
    }
}