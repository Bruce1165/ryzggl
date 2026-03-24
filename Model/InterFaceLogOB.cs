using System;

namespace Model
{
    /// <summary>
    /// 接口日志
    /// </summary>
    public class InterFaceLogOB
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 访问日期 【格式：2014-10-10】
        /// </summary>
        public string AccessDate { set; get; }

        /// <summary>
        /// 访问时间 【格式：10:10:10】
        /// </summary>
        public string AccessTime { set; get; }

        /// <summary>
        /// 访问用户
        /// </summary>
        public string AccessUser { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// 调用方法名称
        /// </summary>
        public string CallingMethodName { get; set; }

        /// <summary>
        /// 参数数据
        /// </summary>
        public string ParameterData { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDescription { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}