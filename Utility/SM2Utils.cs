using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class SM2Utils
    {
        public static void GenerateKeyPair()
        {
            SM2 sm2 = SM2.Instance;
            AsymmetricCipherKeyPair key = sm2.ecc_key_pair_generator.GenerateKeyPair();
            ECPrivateKeyParameters ecpriv = (ECPrivateKeyParameters)key.Private;
            ECPublicKeyParameters ecpub = (ECPublicKeyParameters)key.Public;
            BigInteger privateKey = ecpriv.D;
            ECPoint publicKey = ecpub.Q;

            System.Console.Out.WriteLine("公钥: " + Encoding.ASCII.GetString(Hex.Encode(publicKey.GetEncoded())).ToUpper());
            System.Console.Out.WriteLine("私钥: " + Encoding.ASCII.GetString(Hex.Encode(privateKey.ToByteArray())).ToUpper());
        }

        public static String Encrypt(byte[] publicKey, byte[] data)
        {
            if (null == publicKey || publicKey.Length == 0)
            {
                return null;
            }
            if (data == null || data.Length == 0)
            {
                return null;
            }

            byte[] source = new byte[data.Length];
            Array.Copy(data, 0, source, 0, data.Length);

            Cipher cipher = new Cipher();
            SM2 sm2 = SM2.Instance;

            ECPoint userKey = sm2.ecc_curve.DecodePoint(publicKey);

            ECPoint c1 = cipher.Init_enc(sm2, userKey);
            cipher.Encrypt(source);

            byte[] c3 = new byte[32];
            cipher.Dofinal(c3);

            String sc1 = Encoding.ASCII.GetString(Hex.Encode(c1.GetEncoded()));
            String sc2 = Encoding.ASCII.GetString(Hex.Encode(source));
            String sc3 = Encoding.ASCII.GetString(Hex.Encode(c3));

            //return (sc1 + sc2 + sc3).ToUpper();

            return (sc1 + sc2 + sc3).ToLower();
        }

        /// <summary>
        /// 加密
        /// <remark>标准为C1 || C3 || C2</remark>
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(string publicKey, string data)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                return null;
            }

            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            byte[] sourceData = Encoding.UTF8.GetBytes(data);
            byte[] source = new byte[sourceData.Length];
            Array.Copy(sourceData, 0, source, 0, sourceData.Length);

            Cipher cipher = new Cipher();
            SM2 sm2 = SM2.Instance;

            ECPoint userKey = sm2.ecc_curve.DecodePoint(publicKey.HexToByteArray());

            ECPoint c1 = cipher.Init_enc(sm2, userKey);
            cipher.Encrypt(source);

            byte[] c3 = new byte[32];
            cipher.Dofinal(c3);

            byte[] var4 = c1.GetEncoded();//sc1
            byte[] var5 = source;//sc2
            byte[] var6 = c3;//sc3
            byte[] var7 = new byte[97 + var5.Length];

            Array.Copy(var4, 0, var7, 0, 65);//sc1
            Array.Copy(var6, 0, var7, 65, 32);//sc3
            Array.Copy(var5, 0, var7, 97, var5.Length);//sc2

            //sc1+sc3+sc2
            return var7.ByteArrayToHex().ToLower();
        }

        public static string ByteArrayToHex(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }

        public static byte[] Decrypt(byte[] privateKey, byte[] encryptedData)
        {
            if (null == privateKey || privateKey.Length == 0)
            {
                return null;
            }
            if (encryptedData == null || encryptedData.Length == 0)
            {
                return null;
            }

            String data = Encoding.ASCII.GetString(Hex.Encode(encryptedData));

            byte[] c1Bytes = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(0, 130)));
            int c2Len = encryptedData.Length - 97;
            byte[] c2 = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(130, 2 * c2Len)));
            byte[] c3 = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(130 + 2 * c2Len, 64)));

            SM2 sm2 = SM2.Instance;
            BigInteger userD = new BigInteger(1, privateKey);

            ECPoint c1 = sm2.ecc_curve.DecodePoint(c1Bytes);
            Cipher cipher = new Cipher();
            cipher.Init_dec(userD, c1);
            cipher.Decrypt(c2);
            cipher.Dofinal(c3);

            return c2;
        }

        /// <summary>
        /// token加密传输
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static String generate(String msg)
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder(16);
            sb.Append(r.Next(99999999)).Append(r.Next(99999999));
            int len = sb.Length;
            if (len < 16)
            {
                for (int i = 0; i < 16 - len; i++)
                {
                    sb.Append("0");
                }
            }
            String salt = sb.ToString();
            msg = Utility.Md5Helper.Md5Hex(msg + salt);
            char[] cs = new char[48];
            for (int i = 0; i < 48; i += 3)
            {
                cs[i] = msg.CharAt(i / 3 * 2);
                char c = salt.CharAt(i / 3);
                cs[i + 1] = c;
                cs[i + 2] = msg.CharAt(i / 3 * 2 + 1);
            }
            return new String(cs);
        }

        /// <summary>
        /// 国密SM3加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="msg">待加密信息</param>
        /// <returns></returns>
        public static string HMAC_SM3(string key, string msg)
        {
            byte[] msg1 = Encoding.UTF8.GetBytes(msg);
            byte[] key1 = Encoding.UTF8.GetBytes(key);
            SM3Digest sM3Digest = new SM3Digest();

            Org.BouncyCastle.Crypto.Macs.HMac mac = new Org.BouncyCastle.Crypto.Macs.HMac(sM3Digest);
            mac.Init(new KeyParameter(key1));
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];
            mac.DoFinal(result, 0);
            return Convert.ToBase64String(result);          
        }        
    }
}
