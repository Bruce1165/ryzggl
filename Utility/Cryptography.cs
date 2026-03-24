using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

// ====================================================================
// 加密组件：
// 加密、解密、数字签名
// ====================================================================
namespace Utility
{
    public class Cryptography
    {
        private static TripleDESCryptoServiceProvider DESProvider;
        private static byte[] IV;
        private static byte[] KEY;
        private const string UsingEncoding = "utf-8";//utf-8

        static Cryptography()
        {
            IV = HexStringToByteArray("0102030405060708");
            KEY = HexStringToByteArray("434944F45F9209E45863BB447D9A5A92DB36788C68AFD50D");
            DESProvider = new TripleDESCryptoServiceProvider();
            DESProvider.Mode = CipherMode.CBC;
            DESProvider.Padding = PaddingMode.PKCS7;
        }

        /// <summary>
        /// Encrypt the string according to the settled key and vector
        /// </summary>
        /// <param name="plaintext">Content to be encrypted</param>
        /// <returns></returns>
        public static string Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext) == true)
            {
                return "";
            }
            if (null == plaintext || plaintext.Trim().Equals(""))
            {
                throw new Exception("加密内容为空！");
            }

            string Result = null;

            try
            {
                byte[] TmpIV = IV;
                byte[] TmpKEY = KEY;

                byte[] TmpResult = null;
                byte[] Tmpplaintext = Encoding.GetEncoding(UsingEncoding).GetBytes(plaintext);

                if (Encrypt(TmpKEY, TmpIV, Tmpplaintext, out TmpResult))
                {
                    Result = Convert.ToBase64String(TmpResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("加密错误！", ex);
            }

            return Result;
        }

        /// <summary>
        /// Decrypt the string according to the settled key and vector
        /// </summary>
        /// <param name="cryptographyText">Content to be decrypted</param>
        /// <returns></returns>
        public static string Decrypt(string cryptographyText)
        {
            if (string.IsNullOrEmpty(cryptographyText) == true)
            {
                return "";
            }
            if (null == cryptographyText || cryptographyText.Trim().Equals(""))
            {
                throw new Exception("解密内容为空！");
            }
            string Result = null;

            try
            {
                byte[] TmpIV = IV;
                byte[] TmpKEY = KEY;
                byte[] TmpResult = null;
                byte[] TmpcryptographyText = Convert.FromBase64String(cryptographyText.Replace(" ", "+"));
                if (Decrypt(TmpKEY, TmpIV, TmpcryptographyText, out TmpResult))
                {
                    Result = Encoding.GetEncoding(UsingEncoding).GetString(TmpResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("解密错误！", ex);
            }
            return Result;
        }

        /// <summary>
        /// Generate Authenticator by the value appointed, Authenticator=base64(3DES(SHA1(value)))
        /// </summary>
        /// <param name="s">a valid string</param>
        /// <returns></returns>
        public static string Authenticator(string s)
        {
            if (null == s || s.Trim().Equals(""))
            {
                throw new Exception("生成认证码时参数值为空");
            }

            byte[] TmpIV = IV;
            byte[] TmpKEY = KEY;

            byte[] TmpResult = null;

            if (Encrypt(TmpKEY, TmpIV, ComputeHashSHA1(s), out TmpResult))
            {
                return Convert.ToBase64String(TmpResult);
            }
            else
            {
                return null;
            }
        }

        public static string EncryptBase64(string source)
        {
            string encode = "";
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }


        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>  
        /// AES加密
        /// </summary>  
        /// <param name="encryptStr">明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns></returns>
        public static string AES256_Encrypt(string encryptStr, string key)
        {
            var _aes = new AesCryptoServiceProvider();
            _aes.BlockSize = 128;
            _aes.KeySize = 256;
            _aes.Key = Encoding.UTF8.GetBytes(key);
            _aes.IV = (byte[])(object)new sbyte[16];//Encoding.UTF8.GetBytes(IV);
            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;

            var _crypto = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            byte[] encrypted = _crypto.TransformFinalBlock(Encoding.UTF8.GetBytes(encryptStr), 0, Encoding.UTF8.GetBytes(encryptStr).Length);

            _crypto.Dispose();

            return System.Convert.ToBase64String(encrypted);
        }

        /// <summary>  
        /// AES解密
        /// </summary>  
        /// <param name="decryptStr">密文</param>  
        /// <param name="key">密钥</param>  
        /// <returns></returns>  
        public static string AES256_Decrypt(string decryptStr, string key)
        {
            var _aes = new AesCryptoServiceProvider();
            _aes.BlockSize = 128;
            _aes.KeySize = 256;
            _aes.Key = Encoding.UTF8.GetBytes(key);
            _aes.IV = (byte[])(object)new sbyte[16];//Encoding.UTF8.GetBytes(IV);
            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;

            var _crypto = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            byte[] decrypted = _crypto.TransformFinalBlock(
                System.Convert.FromBase64String(decryptStr), 0, System.Convert.FromBase64String(decryptStr).Length);
            _crypto.Dispose();
            return Encoding.UTF8.GetString(decrypted);
        }

        #region AES 加密解密与java互通

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// 16位密钥对应128位编码，24位密钥对应192位，32对应256位编码
        /// <returns></returns>
        public static string AES_Encrypt(string encryptStr, string key)
        {
            try
            {
                byte[] keyArray = hexStr2ByteArray(key);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;//编码方式
                rDel.Padding = PaddingMode.PKCS7;//填充方式
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return byteArray2HexStr(resultArray);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string AES_Decrypt(string decryptStr, string key)
        {
            try
            {
                byte[] keyArray = hexStr2ByteArray(key);
                byte[] toEncryptArray = hexStr2ByteArray(decryptStr);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public static byte[] hexStr2ByteArray(String hexString)
        {
            try
            {
                if (string.IsNullOrEmpty(hexString))
                    throw new Exception("this hexString must not be empty");

                hexString = hexString.ToLower();
                byte[] byteArray = new byte[hexString.Length / 2];
                int k = 0;
                for (int i = 0; i < byteArray.Length; i++)
                {

                    //把hexString中的两个16进制数转化为字节，一个16进制数是4位，两个就是8位即一个字节
                    //Substring(k, 2)的作用是从k开始截取2个数据
                    byteArray[i] = Convert.ToByte(hexString.Substring(k, 2), 16);
                    k += 2;
                }
                return byteArray;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public static String byteArray2HexStr(byte[] byteArray)
        {
            try
            {
                StringBuilder hexString = new StringBuilder();
                foreach (byte b in byteArray)
                {

                    //X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                    hexString.AppendFormat("{0:x2}", b);
                }
                return hexString.ToString().ToLower();
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        #endregion

        #region Cryptogram: private functions

        /// <summary>
        /// Decrypt the string according to the volatile key and vector
        /// </summary>
        /// <param name="KEY">Key</param>
        /// <param name="IV">Vector</param>
        /// <param name="cryptographyText">Content to be decrypted</param>
        /// <param name="Result">Originally content</param>
        /// <returns></returns>
        private static bool Decrypt(byte[] KEY, byte[] IV, byte[] cryptographyText, out byte[] Result)
        {
            Result = null;

            if (null == KEY)
            {
                throw new Exception("解密密钥无效");
            }

            if (null == IV)
            {
                throw new Exception("解密向量无效");
            }

            if (null == cryptographyText)
            {
                throw new Exception("解密内容为空");
            }

            bool result = false;

            try
            {
                byte[] TmpIV = { 0, 1, 2, 3, 4, 5, 6, 7 };
                byte[] TmpKEY = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };

                for (int ii = 0; ii < 8; ii++)
                {
                    TmpIV[ii] = IV[ii];
                }

                for (int ii = 0; ii < 24; ii++)
                {
                    TmpKEY[ii] = KEY[ii];
                }

                ICryptoTransform Trans = DESProvider.CreateDecryptor(TmpKEY, TmpIV);
                Result = Trans.TransformFinalBlock(cryptographyText, 0, cryptographyText.Length);
                DESProvider.Clear();

                result = true;
            }
            catch
            {
                throw new Exception("解密错误！");
            }

            return result;
        }

        /// <summary>
        /// Encrypt the string according to the volatile key and vector
        /// </summary>
        /// <param name="KEY">Key</param>
        /// <param name="IV">Vector</param>
        /// <param name="plaintext">Content to be encrypted</param>
        /// <param name="Result">Cryptograph</param>
        /// <returns></returns>
        private static bool Encrypt(byte[] KEY, byte[] IV, byte[] plaintext, out byte[] Result)
        {
            Result = null;

            if (null == KEY)
            {
                throw new Exception("加密密钥无效");
            }

            if (null == IV)
            {
                throw new Exception("加密向量无效");
            }

            if (null == plaintext)
            {
                throw new Exception("加密内容为空");
            }

            bool result = false;

            try
            {
                byte[] TmpIV = { 0, 1, 2, 3, 4, 5, 6, 7 };
                byte[] TmpKEY = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };

                for (int ii = 0; ii < 8; ii++)
                {
                    TmpIV[ii] = IV[ii];
                }

                for (int ii = 0; ii < 24; ii++)
                {
                    TmpKEY[ii] = KEY[ii];
                }

                ICryptoTransform Trans = DESProvider.CreateEncryptor(TmpKEY, TmpIV);
                Result = Trans.TransformFinalBlock(plaintext, 0, plaintext.Length);
                DESProvider.Clear();

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("加密错误！", ex);
            }

            return result;
        }

        private static byte[] ComputeHashSHA1(string s)
        {
            byte[] UTF8 = Encoding.GetEncoding(UsingEncoding).GetBytes(s);
            return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(UTF8);
        }

        private static byte[] HexStringToByteArray(string s)
        {
            byte[] buff = new byte[s.Length / 2];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = (byte)(CharToHex(s.Substring(i * 2, 1)) * 0x10 + CharToHex(s.Substring(i * 2 + 1, 1)));
            }
            return buff;
        }

        private static byte CharToHex(string ch)
        {
            switch (ch)
            {
                case "0":
                    return 0x00;

                case "1":
                    return 0x01;

                case "2":
                    return 0x02;

                case "3":
                    return 0x03;

                case "4":
                    return 0x04;

                case "5":
                    return 0x05;

                case "6":
                    return 0x06;

                case "7":
                    return 0x07;

                case "8":
                    return 0x08;

                case "9":
                    return 0x09;

                case "A":
                    return 0x0a;

                case "B":
                    return 0x0b;

                case "C":
                    return 0x0c;

                case "D":
                    return 0x0d;

                case "E":
                    return 0x0e;

                case "F":
                    return 0x0f;
            }
            return 0x00;
        }

        #endregion Cryptogram: private functions

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">待加密内容</param>
        /// <returns>加密后内容</returns>
        public static string GetMD5Hash(String input)
        {
            byte[] result = Encoding.UTF8.GetBytes(input);    //tbPass为输入密码的文本框   
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文

        }


        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publicKeyJava"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RSAEncryptJava(string publicKeyJava, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromPublicKeyJavaString(publicKeyJava);

            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有               
            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);

            return Convert.ToBase64String(cipherbytes);
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKeyJava"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecryptJava(string privateKeyJava, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromPrivateKeyJavaString(privateKeyJava);
            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有        
            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
        }
        /// <summary>
        /// 加密码
        /// </summary>
        /// <param name="strinput">加密字符串</param>
        /// <param name="strkey">密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(string strinput, string strkey)
        {

            strkey = strkey.Substring(0, 8);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.UTF8.GetBytes(strinput);
            des.Key = ASCIIEncoding.ASCII.GetBytes(strkey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(strkey);
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 解密码
        /// </summary>
        /// <param name="pToDecrypt">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文解密结果</returns>
        public static string DESDecrypt(string pToDecrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];

            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = UTF8Encoding.UTF8.GetBytes(key);
            des.IV = UTF8Encoding.UTF8.GetBytes(key);
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return Encoding.UTF8.GetString(ms.ToArray());

        }
    
    }
}