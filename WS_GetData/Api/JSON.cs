using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WS_GetData.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class JSON
    {
        private const string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Encode(object o)
        {
            if (o == null || o.ToString() == "null")
                return null;

            if (o is string)
            {
                return o.ToString();
            }

            return JsonConvert.SerializeObject(o, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = DateTimeFormat,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="o"></param>
        /// <param name="customDateTimeFormat"></param>
        /// <returns></returns>
        public static string Encode(object o, string customDateTimeFormat)
        {
            if (o == null || o.ToString() == "null")
                return null;

            if (o is string)
            {
                return o.ToString();
            }

            return JsonConvert.SerializeObject(o, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = customDateTimeFormat,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object Decode(string json)
        {
            if (String.IsNullOrEmpty(json))
                return "";
            object o = JsonConvert.DeserializeObject(json);
            if (o is string)
            {
                o = JsonConvert.DeserializeObject(o.ToString());
            }
            object v = ToObject(o);
            return v;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Decode(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        private static object ToObject(object o)
        {
            if (o == null) return null;

            if (o is string)
            {
                //判断是否符合2010-09-02T10:00:00的格式
                string s = o.ToString();
                if (s.Length == 19 && s[10] == 'T' && s[4] == '-' && s[13] == ':')
                {
                    o = Convert.ToDateTime(o);
                }
            }
            else if (o is JObject)
            {
                var jo = o as JObject;

                var h = new Hashtable();

                foreach (KeyValuePair<string, JToken> entry in jo)
                {
                    h[entry.Key] = ToObject(entry.Value);
                }

                o = h;
            }
            else if (o is IList)
            {
                var list = new ArrayList();
                list.AddRange((o as IList));
                int i = 0, l = list.Count;
                for (; i < l; i++)
                {
                    list[i] = ToObject(list[i]);
                }
                o = list;
            }
            else if (typeof(JValue) == o.GetType())
            {
                var v = (JValue)o;
                o = ToObject(v.Value);
            }
            return o;
        }
    }
}