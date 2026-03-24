namespace WS_GetData.Api.Utils
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// PDF签章基础URL地址
        /// </summary>
        public const string BaseUrl = "http://172.26.50.55:9090/license-app/v1";

        /// <summary>
        /// Ofd签章基础URL地址172.26.50.54:10002
        /// </summary>
        public const string BaseUrlOfd = "http://172.26.50.54:10002/v1";

        /// <summary>
        /// 用户登录地址
        /// </summary>
        public const string LoginUrl = "/security/login";

        /// <summary>
        /// 用户退出地址
        /// </summary>
        public const string LogoutUrl = "/security/logout";
    }
}
