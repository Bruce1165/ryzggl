using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Utility
{
    public class JSONHelp
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
                DateFormatString = DateTimeFormat
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
                //NullValueHandling = NullValueHandling.Ignore
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

    public class JsonTool
    {
        string json;
        JsonReader jsonReader;
        public JsonTool(string json)
        {
            this.json = json;
        }
        public string getValue(string key)
        {
            if (null == json || "".Equals(json))
            {
                return null;
            }
            jsonReader = new JsonTextReader(new StringReader(json));
            while (jsonReader.Read())
            {
                if (jsonReader.Path.Equals(key) && jsonReader.TokenType != JsonToken.PropertyName)
                {
                    return null == jsonReader.Value ? "" : jsonReader.Value.ToString();
                }
            }
            return null;
        }

        public static string getValue(string json, string key)
        {
            JsonTool tool = new JsonTool(json);
            return tool.getValue(key);
        }

        public string getExtProperties(string key)
        {
            if (null == json || "".Equals(json))
            {
                return null;
            }
            jsonReader = new JsonTextReader(new StringReader(json));
            while (jsonReader.Read())
            {
                if (jsonReader.Path.StartsWith("extProperties") && jsonReader.TokenType == JsonToken.String && jsonReader.Value.ToString().StartsWith(key + "="))
                {
                    return jsonReader.Value.ToString().Substring(key.Length + 1);
                }
            }
            return null;
        }

        public string getReserve3(string key)
        {
            if (null == json || "".Equals(json))
            {
                return null;
            }
            string reserve3 = getValue("reserve3");
            if (null == reserve3 || "".Equals(reserve3))
            {
                return null;
            }
            string[] parameters = reserve3.Split(new char[1] { ';' });
            foreach (string param in parameters)
            {
                if (param.StartsWith(key + ":"))
                {
                    return param.Substring(key.Length + 1);
                }
            }
            return null;
        }
    }
}
