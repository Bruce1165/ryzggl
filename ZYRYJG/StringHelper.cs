using System;
using System.Collections;
using System.Web;
using System.Text;

namespace ZYRYJG
 {
	public class StringHelper
	{
		public static byte[] HexStringToBytes(string hex) 
		{
			if (hex.Length==0)
			{
				return new byte[] {0};
			}

			if (hex.Length % 2 == 1)
			{
				hex = "0" + hex;
			}

			byte[] result = new byte[hex.Length/2];

			for (int i=0; i<hex.Length/2; i++)
			{					
				result[i] = byte.Parse(hex.Substring(2*i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}

			return result;
		}

		public static string BytesToHexString(byte[] input) 
		{
			StringBuilder hexString = new StringBuilder(64);

			for (int i = 0; i < input.Length; i++) 
			{
				hexString.Append(String.Format("{0:X2}", input[i]));
			}
			return hexString.ToString();
		}

		public static string BytesToDecString(byte[] input)
		{
			StringBuilder decString = new StringBuilder(64);

			for (int i = 0; i < input.Length; i++) 
			{
				decString.Append(String.Format(i==0?"{0:D3}":"-{0:D3}", input[i]));
			}
			return decString.ToString();
		}

		// Bytes are string
		public static string ASCIIBytesToString(byte[] input)
		{
			System.Text.ASCIIEncoding enc = new ASCIIEncoding();
			return enc.GetString(input);
		}
		public static string UTF16BytesToString(byte[] input)
		{
			System.Text.UnicodeEncoding enc = new UnicodeEncoding();
			return enc.GetString(input);
		}
		public static string UTF8BytesToString(byte[] input)
		{
			System.Text.UTF8Encoding enc = new UTF8Encoding();
			return enc.GetString(input);
		}

        public static string GB2312BytesToString(byte[] input)
        {
            Encoding GB2312 = Encoding.GetEncoding("GB2312");
            return GB2312.GetString(input);
        }
		// Base64
		public static string ToBase64(byte[] input)
		{
			return Convert.ToBase64String(input);
		}

		public static byte[] FromBase64(string base64)
		{
			return Convert.FromBase64String(base64);
		}

        /// <summary>
        /// 转换数字编号为中文编号（暂时最大支持2未数字）：如12 to 十二
        /// </summary>
        /// <param name="num">数字编号</param>
        /// <returns>中文编号</returns>
        public static string ToChinaNumber(int num)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            char[] list = num.ToString().ToCharArray();
            for (int i = 0; i < list.Length; i++)
            {
                if (list.Length - i == 1)
                {
                    if (list[i] != '0') sb.Append(ToNum(list[i]));
                }
                else if (list.Length - i == 2)
                {
                    if (list[i] == '1')//19~0
                        sb.Append("十");
                    else
                        sb.Append(string.Format("{0}十", ToNum(list[i])));

                }
            }
            return sb.ToString();
        }

        // 转换数字为中文
        public static char ToNum(char x)
        {
            string strChnNames = "一二三四五六七八九";
            string strNumNames = "123456789";
            return strChnNames[strNumNames.IndexOf(x)];
        }
	}

}