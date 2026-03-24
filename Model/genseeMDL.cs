using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ResponseResultGenseePlayHis
    {
        /// <summary>
        /// 处理结果编码：0=有错误，1=成功
        /// </summary>
        [JsonProperty("Code")]
        public string Code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("message")]
        public GenseePlayHisData message { get; set; }
    }

    /// <summary>
    /// 接口调用返回信息
    /// </summary>
    [Serializable]
    public class GenseePlayHisData
    {
        /// <summary>
        ///  错误数据
        /// </summary>
        [JsonProperty("GenseePlayHis")]
        public List<GenseePlayHis> GenseePlayHis { get; set; }
    }

    /// <summary>
    /// 接口调用返回信息
    /// </summary>
    [Serializable]
    public class GenseePlayHis
    {
        /// <summary>
        ///  课件ID
        /// </summary>
        [JsonProperty("id")]
        public string id { get; set; }

        /// <summary>
        ///  用户ID
        /// </summary>
        [JsonProperty("uid")]
        public string uid { get; set; }

        /// <summary>
        ///  姓名
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        ///  加入时间
        /// </summary>
        [JsonProperty("startTime")]
        public long startTime { get; set; }

        /// <summary>
        ///  离开时间
        /// </summary>
        [JsonProperty("leaveTime")]
        public long leaveTime { get; set; }

        /// <summary>
        ///  观看时长
        /// </summary>
        [JsonProperty("duration")]
        public string duration { get; set; }

        /// <summary>
        ///  所属区域
        /// </summary>
        [JsonProperty("area")]
        public string area { get; set; }

        /// <summary>
        ///  终端类型。值说明：
        /// 0 PC
        /// 1 Mac
        /// 2 Linux
        /// 4 Ipad
        /// 8 Iphone
        /// 16 Andriod Pad
        /// 32 Andriod Phone
        /// 132 IPad(PlayerSDK)
        /// 136 IPhone(PlayerSDK)
        /// 144 Andriod Pad(PlayerSDK)
        /// 256 Andriod Phone(PlayerSDK)
        /// 237 移动设备（以前版本的移动端的playersdk和app）
        /// 0xED Mobile
        /// 21 小程序IOS端
        /// 22小程序安卓端
        /// 23 小程序sdk IOS端
        /// 24 小程序sdk安卓端
        /// 26 PC Web端(hls流)
        /// 27 PC Web端(flv流)
        /// 其他值为 Unknown
        /// </summary>
        [JsonProperty("device")]
        public string device { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [JsonProperty("ip")]
        public string ip { get; set; }

        /// <summary>
        /// 访问记录id
        /// </summary>
        [JsonProperty("recordId")]
        public string recordId { get; set; }
    }
}
