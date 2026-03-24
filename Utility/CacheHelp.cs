using System;

namespace Utility
{
    /// <summary>
    /// 应用级缓存管理
    /// </summary>
    public class CacheHelp
    {
        /// <summary>
        /// 添加应用级绝对过期缓存
        /// </summary>
        /// <param name="page">引用页面</param>
        /// <param name="key">缓存唯一键</param>
        /// <param name="value">缓存内容</param>
        /// <param name="hours">缓存小时数</param>
        public static void AddAbsoluteeExpirationCache(System.Web.UI.Page page, string key, object value, double hours)
        {
            RemoveCache(page, key);
            page.Cache.Insert(key, value, null, DateTime.Now.AddHours(hours), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 添加应用级相对(滑动)过期缓存
        /// </summary>
        /// <param name="page">引用页面</param>
        /// <param name="key">缓存唯一键</param>
        /// <param name="value">缓存内容</param>
        /// <param name="hours">缓存小时数</param>
        public static void AddSlidingExpirationCache(System.Web.UI.Page page, string key, object value, double hours)
        {
            RemoveCache(page, key);
            page.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(hours));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="page">引用页面</param>
        /// <param name="key">缓存唯一键</param>
        public static void RemoveCache(System.Web.UI.Page page, string key)
        {
            if (page.Cache[key] != null)
            {
                page.Cache.Remove(key);
            }
        }
    }
}