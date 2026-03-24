using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using Utility;
using ZYRYJG.Thehall;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Security.Cryptography;

using System.ComponentModel;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

using System.Drawing;
using Newtonsoft.Json;

namespace ZYRYJG
{
    public partial class test : System.Web.UI.Page
    {
        //void SplitePDF(string filepath)
        //{
        //    iTextSharp.text.pdf.PdfReader reader = null;
        //    int currentPage = 1; int pageCount = 0;
        //    //string filepath_New = filepath + "\PDFDestination\"; 
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    //byte[] arrayofPassword = encoding.GetBytes(ExistingFilePassword); 
        //    reader = new iTextSharp.text.pdf.PdfReader(filepath); 
        //    reader.RemoveUnusedObjects();
        //    pageCount = reader.NumberOfPages;
        //    string ext = System.IO.Path.GetExtension(filepath);
        //    for (int i = 1; i <= pageCount; i++)
        //    {
        //        iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(filepath);
        //        string outfile = filepath.Replace((System.IO.Path.GetFileName(filepath)), (System.IO.Path.GetFileName(filepath).Replace(".pdf", "") + "_" + i.ToString()) + ext);
        //        reader1.RemoveUnusedObjects();
        //        iTextSharp.text.Document doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
        //        iTextSharp.text.pdf.PdfCopy pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(outfile, System.IO.FileMode.Create));
        //        doc.Open();
        //        for (int j = 1; j <= 1; j++)
        //        {
        //            iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader1, currentPage);
        //            pdfCpy.SetFullCompression();
        //            pdfCpy.AddPage(page);
        //            currentPage += 1;
        //        }
        //        doc.Close();
        //        pdfCpy.Close();
        //        reader1.Close();
        //        reader.Close();
        //    }
      
        //} 

        const string ryzgRoot = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //bool check = Utility.Check.CheckContinueBirthdayLimit(148, "130103196510210034", Convert.ToDateTime("1965-10-21"), "男", Convert.ToDateTime("2025-11-28"));
                //Response.Write("建筑".Split(',')[0]);
                //ObjectDataSource1.SelectParameters.Clear();
                //var q = new QueryParamOB();
                //ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                //RadGrid1.CurrentPageIndex = 0;
                //RadGrid1.DataSourceID = ObjectDataSource1.ID;
               
                //string tmp_zzbs ="1.2.156.3005.2.11100000000013338W011.11110000000021135M.20230329701.003";

                //string  ZzeCertID = string.Format("{0}.{1}", tmp_zzbs,Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", "")));


                //Response.Write(ZzeCertID);
                ////1.2.156.3005.2.11100000000013338W011.11110000000021135M.20230329701.003.Y

                //string Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "a863be60-5f35-483f-a5b0-2f6ce0e66dec", "1523666")));
                // Response.Write(Url);
                
                //CheckContinueBirthdayLimit(int PostTypeID, string cardId,DateTime validEndDate)
                //Response.Write(string.Format("2024-12-31,420300196502010017,{0}", Utility.Check.CheckContinueBirthdayLimit(147, "420300196502010017", Convert.ToDateTime("1965-02-01"), "男", Convert.ToDateTime("2025-02-01"))));
                //Response.Write("<br/>");
                //Response.Write(string.Format("2024-12-31,420300196502010017,{0}", Utility.Check.CheckContinueBirthdayLimit(147, "420300196502010017", Convert.ToDateTime("1965-02-01"), "男", Convert.ToDateTime("2025-03-02"))));
                //Response.Write("<br/>");
                //System.Text.StringBuilder sb =new StringBuilder();
                //for (int i = 32; i < 127;i++ )
                //{
                //    sb.AppendFormat("{0}，", (char)i);
                //}
                //Response.Write(sb.ToString());
                // Response.Write("<br />");
                // Response.Write(UIHelp.CheckKeyWord(sb.ToString()));
                // Response.Write("<br />");
                // sb = new StringBuilder();
                // for (int i = 32; i < 127;i++ )
                // {
                //     sb.AppendFormat("{0}，",UIHelp.ToHalfCode(string.Format("{0}", (char)i)));
                // }
              
                // Response.Write(sb.ToString());

                // Response.Write("<br />");
                //  Response.Write(UIHelp.CheckKeyWord(sb.ToString()));

                //string t = Utility.Check.GetZZBS_CheckCode("11100000000013338W01111110000000021135M20200008117005");
                //////string t = this.GetZZBS_CheckCode("11100000000013338W01111110000000021135M20040040983006");
                //////string t = this.GetZZBS_CheckCode("11100000000013338W01111110000000021135M20110115354008");
                //////string t = this.GetZZBS_CheckCode("11100000000013338W01111110000000021135M20040030431007");
                //Response.Write(t);
                //return;


                //CreateEJUse_CA();
                //Response.Write(string.Format("http://120.52.185.14/default.aspx?action=slrkh ： {0}", Server.HtmlDecode("http://120.52.185.14/default.aspx?action=slrkh")));
            

                //Response.Write(string.Format("http://120.52.185.14/default.aspx?action=slrkh ： {0}",Server.UrlEncode("http://120.52.185.14/default.aspx?action=slrkh")));


                


                //if (null == rtn)
                //{
                //    Response.Write("用户集成认证失败！");
                //    Utility.FileLog.WriteLog("用户集成认证失败！获取 accessToken 返回空。");
                //    return;
                //}
                //else
                //{
                //    jsonTool = new Utility.JsonTool(rtn);





                //string t = DateTime.UtcNow.ToString("r");
                //Response.Write(t);
                //Response.Write("<br />");

                //string nid = Guid.NewGuid().ToString();

                ////t = DateTime.Now.ToString("r");
                //Response.Write(nid);
                //Response.Write("<br />");
                ////Response.Write(DateTime.UtcNow.ToString());
                //string signing_string = string.Format("x-date: {0}\nX-KSCC-PROFILE: {1}\nX-KSCC-NONCE: {2}", t, "prod", nid);
                //Response.Write(signing_string);
                //Response.Write("<br />");
                ////Response.Write(SM2Utils.HMAC_SM3("317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2", signing_string));
                ////string _Authorization = string.Format("1、hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\", algorithm=\"hmac-sm3\", headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.ToSM3byte(string.Format("x-date: {0}\nX-KSCC-PROFILE: {1}\nX-KSCC-NONCE: {2}", t, "prod", nid), "317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));
                //string _Authorization = Server.HtmlEncode(string.Format("1、hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\", algorithm=\"hmac-sm3\", headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.ToSM3byte(string.Format("x-date: {0}\nX-KSCC-PROFILE: {1}\nX-KSCC-NONCE: {2}", t, "prod", nid), "317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2")));
                //Response.Write(_Authorization);
                //Response.Write("<br />");
                //_Authorization = string.Format("2、hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.Encrypt(string.Format("x-date: {0}\nX-KSCC-PROFILE: {1}\nX-KSCC-NONCE: {2}", t, "prod", nid), "317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));
                ////string _Authorization = string.Format("hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.Encrypt(signing_string,"317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));

                //Response.Write(_Authorization);

                //Response.Write("<br />");











                //Response.Write(Convert.ToBase64String(Encoding.Default.GetBytes(_Authorization)));








                //string t = DateTime.UtcNow.ToString("r");
                //Response.Write(t);
                //Response.Write("<br />");
                ////Response.Write(DateTime.UtcNow.ToString());
                //string signing_string = string.Format("x-date:{0}\nX-KSCC-PROFILE:{1}\nX-KSCC-NONCE:{2}", t, "prod", "54372999-7CA0-448C-B560-7C2E6A15B491");
                //Response.Write(signing_string);
                //Response.Write("<br />");
                ////Response.Write(SM2Utils.HMAC_SM3("317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2", signing_string));
                //string _Authorization = string.Format("hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.ToSM3byte( string.Format("x-date:{0}\nX-KSCC-PROFILE:{1}\nX-KSCC-NONCE:{2}", t, "prod", "54372999-7CA0-448C-B560-7C2E6A15B491"),"317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));
                //Response.Write(_Authorization);
                //Response.Write("<br />");
                //_Authorization = string.Format("hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.Encrypt(signing_string,"317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));
                ////string _Authorization = string.Format("hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.Encrypt(signing_string,"317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));

                //Response.Write(_Authorization);

                //Response.Write("<br />");


                //Response.Write(Convert.ToBase64String(Encoding.Default.GetBytes(_Authorization)));
















                //PostInfoOB p = PostInfoDAL.GetObject(120);
                //DBHelper db = new DBHelper();
                //DbTransaction tran = db.BeginTransaction();
                //string a = PostInfoDAL.GetNextCertificateNo(ref p, tran);
                //UIHelp.layerAlert(Page, a);
                //tran.Commit();

                //Response.Write((Convert.ToInt32("156") + 1).ToString("000"));
                //Response.Write("<br />");

                //Response.Write(string.Format("http://localhost:7191/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "033eae2e-d74c-4115-a76f-962562beec8d", "784678"))));

                //Response.Write("<br />");
                //Response.Write(string.Format("京建安B（2019）0162540，{0}", "京建安B（2019）0162540".Substring("京建安B（2019）0162540".Length - 13).Replace("（", "").Replace("）", "")));

                //Response.Write("<br />");


                //Response.Write(string.Format("京建安B（2019）0162540，{0}", "京建安B（2019）0162540".Substring("京建安B（2019）0162540".Length - 13).Replace("（", "").Replace("）", "")));

                //Response.Write("<br />");
                //Response.Write(string.Format("京建安C2（2019）0283361，{0}", "京建安C2（2019）0283361".Substring("京建安C2（2019）0283361".Length - 13).Replace("（", "").Replace("）", "")));

                //Response.Write("<br />");
                //string pdfUrl = "D:/WebRoot/CAFile/ofd/d27ae29f-1d6f-4b31-a6d6-1f5c378213ac_Ofd.pdf";

                //  if (File.Exists(pdfUrl) == true)
                //  {
                //      //string jpgPath = string.Format("D:/WebRoot/CAFile/ofd/d27ae29f-1d6f-4b31-a6d6-1f5c378213ac_Ofd.jpg");
                //      string jpgPath = string.Format("~/Upload/PrintCertificate/d27ae29f-1d6f-4b31-a6d6-1f5c378213ac_Ofd.jpg");
                //      //string jpgPath = string.Format("~/Upload/PrintCertificate/{0}.jpg", "d27ae29f-1d6f-4b31-a6d6-1f5c378213ac_Ofd");
                //      //Utility.ImageHelp.ConvertPDFtoJPG(pdfUrl, Server.MapPath(jpgPath), 150);
                //      //Utility.ImageHelp.ConvertPDFtoJPG(pdfUrl, jpgPath, 150);

                //      Utility.ImageHelp.PdfToPic(pdfUrl, Server.MapPath(jpgPath),2);
                //      Utility.ImageHelp.AddWaterMark(Server.MapPath(jpgPath), "20,700", 36, "仅供全国工程质量安全监管信息平台信息公开使用", -40,"仿宋");

                //      System.Web.UI.HtmlControls.HtmlImage img = new System.Web.UI.HtmlControls.HtmlImage();
                //      img.Src = UIHelp.AddUrlReadParam(jpgPath);
                //      img.Align = "center";
                //      img.Style.Add("height", "1000px");
                //      div_view.Controls.Add(img);


                //  }

                ////SplitePDF(@"D:\WebRoot\CAFile\01a\d4e403d0-3cc9-41a1-8c6f-cc6e54abd01a.pdf");
                //  Response.Write(string.Format("京建安A(2004)0004960：http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "a5a665f0-683f-4407-8a34-b4d497276925", "731"))));
                //Response.Write("<br />");
                //Response.Write(string.Format("京A012012000392：http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "c31b4ef9-7e10-4731-b763-05da8168ae2a", "717677"))));
                //Response.Write("<br />");
                ////Response.Write(string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "d27ae29f-1d6f-4b31-a6d6-1f5c378213ac", "1494838"))));
                ////Response.Write("<br />");
                //Response.Write(string.Format("http://localhost:7191/CertifManage/gbCertView.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("{0},{1}", "d27ae29f-1d6f-4b31-a6d6-1f5c378213ac_Ofd", "1494838"))));
                //Response.Write("<br />");
                //Response.Write("特种作业：http://localhost:7191/PersonnelFile/CertCheck.aspx?o=" +Utility.Cryptography.Encrypt(string.Format("{0},{1}", 138, "京A052008000072")));
                //Response.Write("<br />");
                //Response.Write("三类人：http://localhost:7191/PersonnelFile/CertCheck.aspx?o=" + Utility.Cryptography.Encrypt(string.Format("{0},{1}", 147, "京建安A(2004)0006304")));



                //Response.Write("<br />");
                //Response.Write("建设职业技能岗位：http://localhost:7191/PersonnelFile/CertCheck.aspx?o=" + Utility.Cryptography.Encrypt(string.Format("{0},{1}", 116, "00003142")));




                //E941DF69380267CA328282F15D461A16EE8E2F319018C34F
                //BX7YCIxLg7xm18WP9X/Ib0vM9NRzPSesHSRsEZi7tC2aYT6S6uKHv6I2Dg07t5nd9MqPod/+/QONaKZyUjNfag==


                ////解密decryKey
                //Response.Write(DecryptJava( "MIIBVAIBADANBgkqhkiG9w0BAQEFAASCAT4wggE6AgEAAkEAopLOuVRtAECI9pA8KQW66yReZPsf1y/fq8capxdQmZcViOagR//MyvvRNZ1jie3XkeP2BmolaYPaaMzJCfFpzQIDAQABAkARqYFsARAWhuxaXTEsA8Na6IiYz++VoU3bi7YJkS7ggKzuMypWab1HysNbzBNK9vHszYkvqotD1SNsxRixADDBAiEA46D2rQ52R8qyMqABCV3bu+5m8epNlprflvEb2fnIDhcCIQC21hmjfdlZzMxh8a0RbwKv7STIYZYf1hsV86U7WzQ5uwIgXI9CXyJnVFAG3/ESGtXwmN2bPLmSrS/yxTTrp1obUCcCIHOVwFmaGsjpbp/Qn/+wdTtwqNtAzh5MRY1IHUH783U3AiEAgZSgNM+c8LYLjyjK9y21FcQ72gcxsJHJyMOCaidBwAE="
                //    ,"BX7YCIxLg7xm18WP9X/Ib0vM9NRzPSesHSRsEZi7tC2aYT6S6uKHv6I2Dg07t5nd9MqPod/+/QONaKZyUjNfag=="));
                ////30041532

                ////解密certid
                //Response.Write(DESDecrypt("E941DF69380267CA328282F15D461A16EE8E2F319018C34F", "30041532"));
                ////1100002023000004859

                ////加密certid
                //Response.Write(DESEncrypt("1100002023000004859", "30041532"));
                ////E941DF69380267CA328282F15D461A16EE8E2F319018C34F


                ////加密key
                //Response.Write(EncryptJava("MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAKGMcexIV6n+A+Tv/L731RWLZQDAPPCR9Uu93fcZO/v0mXI2xhbXAr43T6jj99I/ILVvdw8vRyRTTfZeDk+8gSMCAwEAAQ==", "30041532"));




                ////return;
                //电子证书测试
                //testPDFCreate();


                //                //二造


                //                string sql = String.Format(@"Select top 1 *,ca_count = (select count(*) from [zjs_CertificateCAHistory] where [PSN_ServerID]=zjs_Certificate.[PSN_ServerID]) FROM [dbo].[zjs_Certificate]
                //                                                        where  [PSN_RegisterNO] ='建[造]21221100001204'");
                //                DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

                //                string tmp_zzbs = string.Format("{0}.{1}.{2}.{3}{4}.{5}",
                //                                                "1.2.156.3005.2",//电子证照根代码
                //                                                (dtOriginal.Rows[0]["PSN_RegisteProfession"].ToString() == "土木建筑工程" ? "1110000000001332XW059" : "1110000000001332XW082"),//证照类型代码
                //                                                "11110000000021135M",//证照颁发机构代码   
                //                                                 dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Substring(dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Length - 12, 2),//流水号：2位年度+ 6位顺序号
                //                                                dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Substring(dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Length - 6),
                //                                                (Convert.ToInt32(dtOriginal.Rows[0]["ca_count"]) + 1).ToString("000")//版本号
                //                                                );

                //                UIHelp.layerAlert(Page, string.Format("{0}|{1}", dtOriginal.Rows[0]["PSN_RegisterNO"], tmp_zzbs));
                //                return;

                //                UIHelp.layerAlert(Page, string.Format("{0}|{1}{2}",
                //                    dtOriginal.Rows[0]["PSN_RegisterNO"],
                //                    dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Substring(dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Length - 12, 2),
                //                    dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Substring(dtOriginal.Rows[0]["PSN_RegisterNO"].ToString().Length - 6)));
                //            return;
            }

        }

        ///// <summary>
        ///// 获取证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        ///// </summary>
        ///// <param name="ZZBS">证书标识，不带根码，不带分隔符</param>
        ///// <returns>校验码</returns>
        //private string GetZZBS_CheckCode(string ZZBS)
        //{
        //    List<string> Char36 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        //    // "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //    char[] list = ZZBS.ToCharArray();            
        //    int index = 0;
        //    int M = ZZBS.Length;
        //    int p = M;
        //    foreach (char c in list)
        //    {
        //        index = Char36.IndexOf(c.ToString());
        //        p = p + index;
        //        p = (p % M);
        //        if (p == 0) p = M;
        //        p = p * 2;
        //        p = p % (M +1);
        //    }
        //    p = M +1 - p;
        //    return Char36[p -1];
        //}

        ///// <summary>
        ///// 获取证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        ///// </summary>
        ///// <param name="ZZBS">证书标识，不带根码，不带分隔符</param>
        ///// <returns>校验码</returns>
        //private string GetZZBS_CheckCode(string ZZBS)
        //{
        //    List<string> Char36 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        //    char[] list = ZZBS.ToCharArray();
        //    int index = 0;
        //    int p = 36;
        //    foreach (char c in list)
        //    {
        //        index = Char36.IndexOf(c.ToString()) ;
        //        p = p + index ;
        //        p = (p % 36);
        //        if (p == 0) p = 36;
        //        p = p * 2;
        //        p = p % 37;
        //    }
        //    p = 37- p;
        //    return Char36[p];
        //}

        ///// <summary>
        ///// 获取证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        ///// </summary>
        ///// <param name="ZZBS">证书标识，不带根码，不带分隔符</param>
        ///// <returns>校验码</returns>
        //private string GetZZBS_CheckCode(string ZZBS)
        //{
        //    List<string> Char36 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        //    // "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //    char[] list = ZZBS.ToCharArray();
        //    int p = 36;
        //    int index = 0;
        //    foreach (char c in list)
        //    {
        //        index = Char36.IndexOf(c.ToString());
        //        p = p + index;

        //        if (p > 36)
        //        {
        //            p = p - 36;
        //        }
        //        p = p * 2;
        //        if (p >= 37)
        //        {
        //            p = p - 37;
        //        }
        //    }
        //    p = 37 - p;
        //    if (p == 36)
        //    {
        //        p = 0;
        //    }
        //    //return Char36[p - 1];
        //    return Char36[p];
        //}
            
        ///// <summary>
            ///// pdf转图片
            ///// </summary>
            ///// <param name="filePath">pdf文件路径</param>
            ///// <param name="picPath">图片保存路径</param>
            ///// <param name="sizeTimes">图片尺寸倍数，默认1</param>
            //public void PdfToPic(string filePath, string picPath,float sizeTimes=1)
            //{
            //    var pdf = PdfiumViewer.PdfDocument.Load(filePath);
            //    var pdfpage = pdf.PageCount;
            //    var pagesizes = pdf.PageSizes;
            //    for (int i = 1; i <= pdfpage; i++)
            //    {
            //        Size size = new Size();
            //        size.Height = (int)(pagesizes[(i - 1)].Height * sizeTimes);
            //        size.Width = (int)(pagesizes[(i - 1)].Width * sizeTimes);
            //        //可以把".jpg"写成其他形式
            //        RenderPage(filePath, i, size, picPath);
            //    }
            //}
            //private void RenderPage(string pdfPath, int pageNumber, System.Drawing.Size size, string outputPath, int dpi = 300)
            //{
            //    using (var document = PdfiumViewer.PdfDocument.Load(pdfPath))
            //    using (var stream = new FileStream(outputPath, FileMode.Create))
            //    using (var image = GetPageImage(pageNumber, size, document, dpi))
            //    {
            //        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    }
            //}
            //private static System.Drawing.Image GetPageImage(int pageNumber, Size size, PdfiumViewer.PdfDocument document, int dpi)
            //{
            //    return document.Render(pageNumber - 1, size.Width, size.Height, dpi, dpi,PdfiumViewer.PdfRenderFlags.Annotations);
            //}
   
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publicKeyJava"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptJava(string publicKeyJava, string data, string encoding = "UTF-8")
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
        /// RSA加密
        /// </summary>
        /// <param name="publicKeyCSharp"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptCSharp(string publicKeyCSharp, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publicKeyCSharp);

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
        /// RSA加密PEM秘钥
        /// </summary>
        /// <param name="publicKeyPEM"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptPEM(string publicKeyPEM, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.LoadPublicKeyPEM(publicKeyPEM);

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
        public static string DecryptJava(string privateKeyJava, string data, string encoding = "UTF-8")
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

        //测试生成电子证书
        protected void testPDFCreate()
        {
            //生成电子证书

            var template_url = "";
            var save_pdf_url = "";
            Dictionary<string, string> dic;
            Model.CertificateOB ob;

            ////专业技术
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\电子证书模板.pdf";
            //save_pdf_url = @"D:\01test\专业技术管理人员.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(1088288);
            //FillForm(template_url, save_pdf_url, dic, ob);

            ////职业技能
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\职业技能.pdf";
            //save_pdf_url = @"d:\01test\职业技能.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(611078);
            //FillForm(template_url, save_pdf_url, dic, ob);

            //新版职业技能
            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\新版职业技能.pdf";
            save_pdf_url = @"d:\01test\新版职业技能.pdf";
            dic = ReadForm(template_url);
            ob = CertificateDAL.GetObject(1290635);
            FillFormNewZYJN(template_url, save_pdf_url, dic, ob);

            ////特种作业
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_特种作业.pdf";
            //save_pdf_url = @"d:\01test\特种作业.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(228630);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            ////A
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_三类人.pdf";
            //save_pdf_url = @"d:\01test\三类人A.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(2131);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            ////B
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_三类人.pdf";
            //save_pdf_url = @"d:\01test\三类人B.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(1156225);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            ////C1
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_三类人.pdf";
            //save_pdf_url = @"d:\01test\三类人C1.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(232381);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            ////C2
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_三类人.pdf";
            //save_pdf_url = @"d:\01test\三类人C2.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(726885);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            ////C3
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_三类人.pdf";
            //save_pdf_url = @"d:\01test\三类人C3.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(1279233);
            //FillForm_GB(template_url, save_pdf_url, dic, ob);

            //            //二建
            //            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\二级建造师注册证书.pdf";
            //            save_pdf_url = @"d:\01test\二建.pdf";
            //            dic = ReadForm(template_url);

            //            string sql = String.Format(@"Select top 1 * FROM [dbo].[View_JZS_TOW_WithProfession]
            //                                        where  PSN_CertificateNO ='130634198401225844'
            //--where  PSN_CertificateNO ='370983199111075318'
            //                                        order by [CJSJ] desc,PSN_CertificateNO");
            //            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
            //            FillFormOfJZS(template_url, save_pdf_url, dic, dtOriginal.Rows[0]);//填充模板


//            //二造
//            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\二级造价师注册证书.pdf";
//            save_pdf_url = @"d:\01test\二造.pdf";
//            dic = ReadForm(template_url);

//            string sql = String.Format(@"Select top 1 * FROM [dbo].[zjs_Certificate]
//                                        where  [PSN_RegisterNO] ='建[造]13110012559'");
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
//            FillFormOfZJS(template_url, save_pdf_url, dic, dtOriginal.Rows[0]);//填充模板
        }

        /// 读取pdf模版中的标签
        /// 
        /// pdf模版文件路径
        /// 
        public Dictionary<string, string> ReadForm(string pdfTemplate)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            PdfReader pdfReader = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                AcroFields pdfFormFields = pdfReader.AcroFields;
                foreach (KeyValuePair<string, string> de in pdfFormFields.Fields)
                {
                    dic.Add(de.Key, "");
                }
            }
            catch
            {

            }
            finally
            {
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
            return dic;
        }

        /// 
        /// 向pdf模版填充内容，并生成新的文件
        /// 
        /// 模版路径
        /// 生成文件保存路径
        /// 标签字典(即模版中需要填充的控件列表)
        public void FillForm_GB(string pdfTemplate, string newFile, Dictionary<string, string> dic, Model.CertificateOB ob)
        {
            switch (ob.PostName)
            {
                case "企业主要负责人":
                    dic["PostName"] = string.Format("建筑施工{0}", ob.PostName);
                    break;
                case "项目负责人":
                case "土建类专职安全生产管理人员":
                case "机械类专职安全生产管理人员":
                case "综合类专职安全生产管理人员":
                    dic["PostName"] = string.Format("建筑施工企业{0}", ob.PostName);
                    break;
                default:
                    dic["PostName"] = ob.PostName;
                    break;
            }
          
            dic["WorkerName"] = ob.WorkerName;
            dic["WorkerCertificateCode"] = ob.WorkerCertificateCode;
            dic["Sex"] = ob.Sex;
            dic["Birthday"] = ob.Birthday.Value.ToString("yyyy年M月d日");
            dic["UnitName"] = ob.UnitName;
            dic["CertificateCode"] = ob.CertificateCode;
            dic["ValidStartDate"] = ob.CheckDate.Value.ToString("yyyy年M月d日");//有效期起始
            dic["ValidEndDate"] = ob.ValidEndDate.Value.ToString("yyyy年M月d日");//有效期截至
            dic["ConferDate"] = ob.ConferDate.Value.ToString("yyyy年M月d日");//初次发证日期
            dic["CheckDateYear"] = ob.CheckDate.Value.Year.ToString();//发证日期年份
            dic["CheckDateMonth"] = ob.CheckDate.Value.Month.ToString();//发证日期月份
            dic["CheckDateDay"] = ob.CheckDate.Value.Day.ToString();//发证日期日期
            dic["SkillLevel"] = "分管生产经营的副总经理（副总裁）";// ob.SkillLevel;
            dic["ConferUnit"] = "北京市住房和城乡建设委员会";

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //设置支持中文字体

                string iTextAsianCmaps_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsianCmaps.dll";
                string iTextAsian_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsian.dll";
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);

                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);//宋体填充
                //BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simfang.ttf,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);//宋体填充

              
                pdfFormFields.AddSubstitutionFont(baseFont);

                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }


                //-----------------------------
                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印



                //iTextSharp.text.Image backgroundImage = iTextSharp.text.Image.GetInstance(@"d:\1\pdfbg.jpg");

                //backgroundImage.SetAbsolutePosition(0, 0);//设置图片的位置，是必须的，否则会报错
                //iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);      //获取第一页   
                //backgroundImage.ScaleAbsolute( psize.Width,psize.Height);
                //waterMarkContent.AddImage(backgroundImage);//加背景


                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Page.Server.MapPath(UIHelp.GetFaceImagePath(ob.FacePhoto, ob.WorkerCertificateCode)));
                image.GrayFill = 100;//透明度，灰色填充
                //image.ScaleAbsolute(110, 140);
                image.ScaleAbsolute(77, 98);
                image.SetAbsolutePosition(420, 465);
                waterMarkContent.AddImage(image);//加水印


                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", ob.PostID, ob.CertificateCode));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 300, 300);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp,iTextSharp.text.Color.BLACK);
                imgCode.ScaleAbsolute(77, 77);
                //imgCode.SetAbsolutePosition(110, 170);


                imgCode.SetAbsolutePosition(100, ob.PostTypeID.ToString()=="1"? 145:170);
                waterMarkContent.AddImage(imgCode);//加水印


                pdfStamper.FormFlattening = true;


                //-------------------------------------------
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "生成电子证书错误", ex);
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
        }

        //个人登录
        protected void ButtonWorker_Click(object sender, EventArgs e)
        {
            string sfz = "";
            switch (RadioButtonList1.SelectedValue)
            {
                case "初始注册":
                    sfz = "110103198412251831";
                    break;
                case "延续注册":
                    sfz = "142222198211290027";
                    break;
                case "注销注册":
                    sfz = "142222198211290027";
                    break;
                case "重新注册":
                    break;
                case "变更注册":
                    sfz = "142222198211290027";
                    break;
                case "从业人员":
                    sfz = "110223198112021410";
                    break;
                default:
                    sfz = "110223198112021410";
                    break;

            }


            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(sfz);


            if (_WorkerOB == null)
            {

                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的个人注册信息，请确认是否已经在建委官方网站注册个人用户！"), true);
            }

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码,8）用户最后登录时间        
            string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "2";
            //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID), loginTime, 10);
            //Session[string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID)] = loginTime;

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;


            Response.Redirect("~/Default.aspx", false);
        }
        //企业登录
        protected void ButtonQY_Click(object sender, EventArgs e)
        {
            string qyid = "";//企业ID
            string qymc = "";//企业名称
            string qyregion = "";//所属区县
            string zzjgdm = "91110000100024296D";//组织机构代码
            string shtyxydm = "";//社会统一信用代码
            switch (RadioButtonList1.SelectedValue)
            {
                case "初始注册":
                    zzjgdm = "91110105801739307X";
                    break;
                case "延续注册":
                    zzjgdm = "911101111028178635";
                    break;
                case "注销注册":
                    zzjgdm = "911101111028178635";
                    break;
                case "变更注册":
                    zzjgdm = "911101111028178635";
                    break;
            }




            ////查询企业

            //var httpCookie = Request.Cookies["ExamCheckCode"];
            //if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
            //{
            //    txtValidator.Value = "";
            //    UIHelp.layerAlert(Page, "请输入正确的验证码！");
            //    return;
            //}

            //if (Utility.Cryptography.Encrypt(password) != "XrPLui04NGWrPTH1F1d04w==")
            //{
            //    UIHelp.layerAlert(Page, "登录失败！");
            //    return;
            //}


            UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);



            if (o != null)
            {
                qyid = o.UnitID;//企业ID
                qymc = o.ENT_Name;//企业名称
                qyregion = o.ENT_City;//区县
                shtyxydm = o.CreditCode;//社会统一信用代码
                zzjgdm = o.ENT_OrganizationsCode;

                GSJ_QY_GSDJXXMDL g = UnitDAL.GetObjectUni_scid(o.CreditCode);//工商记录
                if (g != null)//更新验证状态
                {
                    o.ResultGSXX = 2;
                }
                else
                {
                    o.ResultGSXX = 1;
                }
                o.ApplyTimeGSXX = DateTime.Now;
                UnitDAL.UpdateResultGSXX(o);
            }
            else
            {
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的企业信息，请确认是否已经在系统注册了企业信息！"), true);
            }


            ////是否为央企或大集团（有子公司）
            //int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

            ////0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码 ,8）用户最后登录时间       
            //string userInfo = "";// string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", qyid, qymc, qyregion, "2", qyid, "", "", zzjgdm);

            //string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            //if (countUnit == 0)
            //{
            //    userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
            //}
            //else
            //{
            //    userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            //}

            //是否为央企或大集团（有子公司）
            int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

            //是否为培训点
            int ifTrainUnit = CommonDAL.GetRowCount("[TrainUnit]", "1", string.Format(" and [UnitCode]='{0}' and [UseStatus]=1 ", shtyxydm));

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
            string userInfo = "";
            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码)，8）用户最后登录时间
            if (countUnit > 0 && ifTrainUnit > 0)//央企或大集团、培训点
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100|200", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (countUnit > 0)//央企或大集团
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (ifTrainUnit > 0)//培训点
            {
                //userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
            }
            else//一般企业
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
            }

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "3";
            //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, qyid), loginTime, 10);
            //Session[string.Format("{0}user{1}", _personType, qyid)] = loginTime;

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;
            try
            {
                UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业住建委办事大厅统一身份认证登录@");
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }
        //管理员登录
        protected void ButtonaAdmin_Click(object sender, EventArgs e)
        {
            login("synergy");           
        }

        //受理登录
        protected void ButtonFW_Click(object sender, EventArgs e)
        {
            login("李然"); 
        }
        //审核登录
        protected void ButtonFWLD_Click(object sender, EventArgs e)
        {
            login("ly"); 
        }

        protected void login(string username)
        {
            UserOB userOb = UserDAL.GetObject(username);

            if (userOb == null)
            {
                return;
            }

            //角色ID集合
            var roleIDs = new System.Text.StringBuilder();
            DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
            }
            if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

            OrganizationOB _OrganizationOB = OrganizationDAL.GetObject(Convert.ToInt64(userOb.OrganID));

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码 ,8）用户最后登录时间 
            string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
                , userOb.UserID
                , userOb.RelUserName
                , _OrganizationOB.OrganNature == "虚拟区县" ? _OrganizationOB.OrganName : "全市"
                , roleIDs.ToString()
                , userOb.OrganID
                , ""
                , _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName//"北京市住房和城乡建设委员会"
                , _OrganizationOB.OrganCode
                , loginTime);

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "6";

            userInfo = Cryptography.Encrypt(userInfo);

            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;
            try
            {
                UIHelp.WriteOperateLog(userOb.RelUserName, userOb.UserID.ToString(), "登录", string.Format("管理者网站登录页面登录。隶属机构：{0}@", _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName));
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }

        // 审批管理人员（后退）
        protected void ButtonZCYW_Click(object sender, EventArgs e)
        {
            login("孟昭琨"); 
        }

        //批准登录
        protected void ButtonZCLD_Click(object sender, EventArgs e)
        {
            login("李丽华"); 
        }

        protected void ButtonAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        //测试建造工程师上传建设部
        protected void ButtonUpJZGCStoJSB_Click(object sender, EventArgs e)
        {
//            int MaxCountExe = 1;
//            string apiUrl = "http://zaojiaccea.jianshe99.com/cecaopsys/externalInterface/operate.do?op=userInfoOperate";
//            string rtnJson ="";//接口调用返回json
//            zjgcsUpResultMDL rtn =null;

//            zjs_ApplyMDL _zjs_ApplyMDL = null;//最新注册申请
//            zjs_QualificationMDL _zjs_QualificationMDL = null;//资格证书

//            //获取本地造价咨询企业资质信息
//            DataTable dtUnitZJZX = CommonDAL.GetDataTable("select * from jcsjk_QY_ZHXX where SJLX='本地造价咨询企业'");
//            DataColumn[] key = new DataColumn[1];
//            key[0] = dtUnitZJZX.Columns["ZZJGDM"];
//            dtUnitZJZX.PrimaryKey = key;
//            DataRow findRow = null;

//            //读取待发送的二级建造工程师
//            string sql = @"select top {0} c.*,u.ENT_Economic_Nature,u.[ENT_OrganizationsCode] as ZZJGDM,u.ENT_Type,u.ENT_Telephone FROM [dbo].[zjs_Certificate] c
//inner join [dbo].[Unit] u on c.ENT_OrganizationsCode = u.CreditCode
//where (c.[UpJsbTime] is null or c.[UpJsbTime]<c.[ApplyCATime]) order by c.[PSN_ServerID]";
//            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, MaxCountExe));

//            userInfoOperate info = null;//上传信息分装包
//            userInfos u = null;//上传信息
//            foreach (DataRow d in dt.Rows)
//            {
//                try
//                {
//                    u = new userInfos();
//                    u.outUserID = d["PSN_ServerID"].ToString();
//                    u.validFlag = (d["PSN_RegisteType"].ToString() == "07" ? 3 : 1);
//                    u.userName = d["PSN_Name"].ToString();
//                    u.sex = (d["PSN_Sex"].ToString() == "nv" ? 0 : 1);
//                    u.birthday = Convert.ToDateTime(d["PSN_BirthDate"]).ToString("yyyy-MM-dd");
//                    u.nationID = Utility.Check.GetNationCode(d["PSN_National"].ToString());
//                    u.titlesID = "28";
//                    u.idCard = d["PSN_CertificateNO"].ToString();
//                    u.soldierNumber = "";
//                    u.eduLevelID = Utility.Check.GetEduLevelCode(d["PSN_Qualification"].ToString());
//                    u.graduateDate = Convert.ToDateTime(d["PSN_GraduationTime"]).ToString("yyyy-MM-dd");
//                    u.graduateSchool = d["PSN_GraduationSchool"].ToString();
//                    u.majorName = d["PSN_Specialty"].ToString();
//                    u.workYear = "";
//                    u.enterPriseTypeID = Utility.Check.GetEnterPriseTypeID(d["ENT_Economic_Nature"].ToString());
//                    u.areaName = "北京市";
//                    u.cityName = d["ENT_City"].ToString();//"北京市";
//                    u.countyName = "";
//                    u.instanceName = "北京市"; //"北京市住房和城乡建设委员会";
//                    u.workUnitName = d["ENT_Name"].ToString();
//                    u.branchUnit = "";
//                    findRow = dtUnitZJZX.Rows.Find(d["ZZJGDM"].ToString());
//                    if (findRow != null)
//                    {
//                        u.empUnitTypeID = "1";
//                        u.workUnitNumber = findRow["ZZZSBH"].ToString();
//                    }
//                    else
//                    {
//                        u.empUnitTypeID = "2";
//                        u.workUnitNumber = "";
//                    }

//                    _zjs_ApplyMDL = zjs_ApplyDAL.GetObjectLastContract(d["ENT_OrganizationsCode"].ToString());
//                    if (_zjs_ApplyMDL != null)
//                    {
//                        u.employDateStatus = "1";
//                        u.employDateBegin = _zjs_ApplyMDL.ENT_ContractStartTime.Value.ToString("yyyy-MM-dd");
//                        u.employDateEnd = (_zjs_ApplyMDL.ENT_ContractENDTime.HasValue == true ? _zjs_ApplyMDL.ENT_ContractENDTime.Value.ToString("yyyy-MM-dd") : "");
//                    }
//                    else
//                    {
//                        u.employDateStatus = "2";
//                        u.employDateBegin = "";
//                        u.employDateEnd = "";
//                    }

//                    u.workUnitCode = d["ENT_OrganizationsCode"].ToString();
//                    u.archiveUnitName = "";
//                    u.certificateNumber = d["PSN_RegisterNO"].ToString();
//                    u.signetNumber = d["PSN_RegisterNO"].ToString().Replace("建[造]", "B");
//                    u.registerDate = Convert.ToDateTime(d["PSN_CertificationDate"]).ToString("yyyy-MM-dd");
//                    u.invalidDate = Convert.ToDateTime(d["PSN_CertificateValidity"]).ToString("yyyy-MM-dd");
//                    u.examNumber = "";
//                    u.qualificationNumber = d["ZGZSBH"].ToString();
//                    _zjs_QualificationMDL = zjs_QualificationDAL.GetObject(d["ZGZSBH"].ToString());

//                    if (_zjs_QualificationMDL != null)
//                    {
//                        u.agreeDate = _zjs_QualificationMDL.QFSJ.Value.ToString("yyyy-MM-dd");
//                        u.examSpecialityID = (_zjs_QualificationMDL.ZGXL.Contains("土木") == true ? "1" : "2");
//                        u.examYear = _zjs_QualificationMDL.QFSJ.Value.ToString("yyyy-MM-dd");
//                    }

//                    u.isEmployed = "1";
//                    u.examAreaName = "北京市";
//                    u.address = d["END_Addess"].ToString();
//                    u.postNumber = "";
//                    u.phone = d["ENT_Telephone"].ToString();
//                    u.mobile = d["PSN_MobilePhone"].ToString();
//                    u.unitMainBusinessID = Utility.Check.GetUnitMainBusinessID(d["ENT_Type"].ToString());
//                    u.userRemark = "";
//                    u.allPeriod = "0";

//                    info = new userInfoOperate();
//                    info.key = Utility.Cryptography.GetMD5Hash( "ek2Uf7cjFUrRhmeS");
//                    info.userInfos = string.Format(@"[{0}]", Utility.JSONHelp.Encode(u));

//                    ////发送数据
//                    //rtnJson = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format(@"op=[{0}]", Utility.JSONHelp.Encode(info)));
//                    //rtnJson = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format(@"op=userInfoOperate&[{0}]", Utility.JSONHelp.Encode(info)));

//                    rtnJson = Utility.HttpHelp.DoPost(apiUrl, Utility.JSONHelp.Encode(info));

//                    FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}。返回结果：{1}", d["PSN_RegisterNO"], rtnJson));

//                    rtn = Newtonsoft.Json.JsonConvert.DeserializeObject<zjgcsUpResultMDL>(rtnJson);

//                    if (rtn.code == "1")
//                    {
//                        zjs_CertificateDAL.UpdateUpJsbTime(d["PSN_ServerID"].ToString());//更新发送时间

//                        FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}成功。", d["PSN_RegisterNO"]));
//                    }
//                    else
//                    {
//                        FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}失败：{1}", d["PSN_RegisterNO"], rtn.msg));
//                    }
//                }
//                catch(Exception ex)
//                {
//                    FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}失败。", d["PSN_RegisterNO"]),ex);
//                }
//            }

        }

        /// 
        /// 向pdf模版填充内容，并生成新的文件
        /// 
        /// 模版路径
        /// 生成文件保存路径
        /// 标签字典(即模版中需要填充的控件列表)
        public void FillForm(string pdfTemplate, string newFile, Dictionary<string, string> dic, Model.CertificateOB ob)
        {
            dic["PostName"] = ob.PostName;
            dic["WorkerName"] = ob.WorkerName;
            dic["WorkerCertificateCode"] = ob.WorkerCertificateCode;
            dic["UnitName"] = ob.UnitName;
            dic["CertificateCode"] = ob.CertificateCode;
            dic["ValidEndDate"] = ob.ValidEndDate.Value.ToString("yyyy年MM月dd日");
            dic["ConferDate"] = ob.ConferDate.Value.ToString("yyyy年MM月dd日");
            dic["CreateDate"] = DateTime.Now.ToString("yyyy年MM月dd日");//制证日期（签章日期）
            dic["SkillLevel"] = ob.SkillLevel;

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //设置支持中文字体

                string iTextAsianCmaps_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsianCmaps.dll";
                string iTextAsian_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsian.dll";
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);

                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                pdfFormFields.AddSubstitutionFont(baseFont);
                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }


                //-----------------------------
                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印



                //iTextSharp.text.Image backgroundImage = iTextSharp.text.Image.GetInstance(@"d:\1\pdfbg.jpg");

                //backgroundImage.SetAbsolutePosition(0, 0);//设置图片的位置，是必须的，否则会报错
                //iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);      //获取第一页   
                //backgroundImage.ScaleAbsolute( psize.Width,psize.Height);
                //waterMarkContent.AddImage(backgroundImage);//加背景


                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Page.Server.MapPath(UIHelp.GetFaceImagePath(ob.FacePhoto, ob.WorkerCertificateCode)));
                image.GrayFill = 100;//透明度，灰色填充
                image.ScaleAbsolute(110, 140);
                image.SetAbsolutePosition(410, 465);
                waterMarkContent.AddImage(image);//加水印


                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", ob.PostID, ob.CertificateCode));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, iTextSharp.text.Color.BLACK);
                imgCode.ScaleAbsolute(100, 100);
                imgCode.SetAbsolutePosition(110, 170);
                waterMarkContent.AddImage(imgCode);//加水印


                pdfStamper.FormFlattening = true;


                //-------------------------------------------
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "生成电子证书错误", ex);
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
        }


        /// <summary>
        /// 格式化新版职业技能证书的技能等级
        /// 等级显示：加个“/”后面写对应的等级，如：中级工/四级。
        /// 职业技能一般分为五个等级，由低到高可分为：五级/初级工、四级/中级工、三级/高级工、二级/技师、一级/高级技师。
        /// </summary>
        /// <param name="SkillLevel">技能等级</param>
        /// <returns>格式化后的技能等级</returns>
        private string FormatSkillLevel(string SkillLevel)
        {
            switch (SkillLevel)
            {
                case "初级工":
                    return "初级工 / 五级";
                    case "中级工":
                    return "中级工 / 四级";
                    case "高级工":
                    return "高级工 / 三级";
                    case "技师":
                    return "技师 / 二级";
                    case "高级技师":
                    return "高级技师 / 一级";
                default:
                    return SkillLevel;
            }
        }

        /// 
        /// 向pdf模版填充内容，并生成新的文件
        /// 
        /// 模版路径
        /// 生成文件保存路径
        /// 标签字典(即模版中需要填充的控件列表)
        public void FillFormNewZYJN(string pdfTemplate, string newFile, Dictionary<string, string> dic, Model.CertificateOB ob)
        {
            dic["PostName"] = ob.PostName;
            dic["WorkerName"] = ob.WorkerName;
            dic["WorkerCertificateCode"] = ob.WorkerCertificateCode;
            dic["Sex"] = ob.Sex;
            dic["TrainUnit"] = (ob.TrainUnitName.Length <=12?"\r\n":"") + ob.TrainUnitName;
            dic["CertificateCode"] =  ob.CertificateCode;           
            dic["ConferDate"] = ob.ConferDate.Value.ToString("yyyy年MM月dd日");
            dic["SkillLevel"] = FormatSkillLevel(ob.SkillLevel);

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //设置支持中文字体

                string iTextAsianCmaps_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsianCmaps.dll";
                string iTextAsian_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsian.dll";
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);

                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                pdfFormFields.AddSubstitutionFont(baseFont);
                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }


                //-----------------------------
                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印



                //iTextSharp.text.Image backgroundImage = iTextSharp.text.Image.GetInstance(@"d:\1\pdfbg.jpg");

                //backgroundImage.SetAbsolutePosition(0, 0);//设置图片的位置，是必须的，否则会报错
                //iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);      //获取第一页   
                //backgroundImage.ScaleAbsolute( psize.Width,psize.Height);
                //waterMarkContent.AddImage(backgroundImage);//加背景


                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Page.Server.MapPath(UIHelp.GetFaceImagePath(ob.FacePhoto, ob.WorkerCertificateCode)));
                image.GrayFill = 100;//透明度，灰色填充
                image.ScaleAbsolute(110, 140);
                image.SetAbsolutePosition(250, 485);
                waterMarkContent.AddImage(image);//加水印


                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", ob.PostID, ob.CertificateCode));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, iTextSharp.text.Color.BLACK);
                imgCode.ScaleAbsolute(100, 100);
                imgCode.SetAbsolutePosition(90, 75);
                waterMarkContent.AddImage(imgCode);//加水印


                pdfStamper.FormFlattening = true;


                //-------------------------------------------
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "生成电子证书错误", ex);
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
        }

        /// <summary>
        /// 向二级建造师使用件pdf模版填充内容，并生成新的文件
        /// </summary>
        /// <param name="pdfTemplate">模版路径</param>
        /// <param name="newFile">生成文件保存路径</param>
        /// <param name="dic">标签字典(即模版中需要填充的控件列表)</param>
        /// <param name="dr">数据行</param>
        private void FillFormOfJZS_Use(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            dic["PSN_Name"] = dr["PSN_Name"].ToString();//姓名
            dic["PSN_Sex"] = dr["PSN_Sex"].ToString();//性别
            dic["PSN_BirthDate"] = Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年M月d日");//生日
            dic["ENT_Name"] = dr["ENT_Name"].ToString();//单位
            dic["PSN_RegisterNO"] = dr["PSN_RegisterNO"].ToString();//注册号
            dic["PSN_CertificationDate"] = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日");//发证日期
            //dic["PSN_RegistePermissionDate"] = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日"); //制证日期（签章日期）

            dic["FromTo"] = string.Format("{0}-{1}", Convert.ToDateTime(dr["BeginTime"]).ToString("yyyy年M月d日"), Convert.ToDateTime(dr["EndTime"]).ToString("yyyy年M月d日"));//使用有效期

            //格式化专业
            //注册专业、有效期
            string[] zy = dr["ProfessionWithValid"].ToString().Trim(' ').Split(' ');//注册专业、有效期
            StringBuilder sb = new StringBuilder();
            foreach (string s in zy)
            {
                sb.Append(formatZY_Use(s, Convert.ToDateTime(dr["PSN_RegistePermissionDate"]))).Append("\n\n");
            }
            dic["PSN_RegisteProfession"] = sb.ToString();

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //设置支持中文字体          
                //string iTextAsianCmaps_Path = "iTextAsianCmaps.dll";
                //string iTextAsian_Path = "iTextAsian.dll";
                string iTextAsianCmaps_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsianCmaps.dll";
                string iTextAsian_Path = @"D:\Work\人员考务与考证管理\SourceCode\dll\iTextAsian.dll";
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);

                //BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simhei.ttf,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                pdfFormFields.AddSubstitutionFont(baseFont);
                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }

                //-----------------------------
                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印

                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetFaceImagePathJZS(dr["PSN_RegisterNO"].ToString(), dr["PSN_CertificateNO"].ToString()));
                image.GrayFill = 100;//透明度，灰色填充
                image.ScaleAbsolute(110, 140);
                image.SetAbsolutePosition(410, 465);
                waterMarkContent.AddImage(image);//加水印

                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["CertificateCAID"], dr["PSN_RegisterNO"]));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/PersonnelFile/CertCheckJZS.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp,iTextSharp.text.Color.BLACK);
                imgCode.ScaleAbsolute(71, 71);
                imgCode.SetAbsolutePosition(60, 100);
                waterMarkContent.AddImage(imgCode);//加水印

                //输出签名  
                iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance(GetSignPhotoJZS(dr["PSN_RegisterNO"].ToString(), dr["PSN_CertificateNO"].ToString()));
                img_qm.GrayFill = 100;//透明度，灰色填充
                img_qm.ScaleAbsolute(99, 43);
                img_qm.SetAbsolutePosition(160, 120);
                waterMarkContent.AddImage(img_qm);//加水印

                ////输出签名  
                //iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance(GetSignPhotoJZS(dr["PSN_CertificateNO"].ToString()));
                //img_qm.GrayFill = 100;//透明度，灰色填充
                //img_qm.ScaleAbsolute(99, 43);
                ////img_qm.ScaleAbsolute(189, 72);
                //img_qm.SetAbsolutePosition(160, 120);
                //waterMarkContent.AddImage(img_qm);//加水印

                ////红章
                //iTextSharp.text.Image imageHZ = iTextSharp.text.Image.GetInstance(string.Format("{0}/Images/chapter01ddd.png", ExamWebRoot));
                //imageHZ.GrayFill = 100;//透明度，灰色填充
                //imageHZ.ScaleAbsolute(117, 117);
                //imageHZ.SetAbsolutePosition(350, 210);
                //waterMarkContent.AddImage(imageHZ);//加水印

                pdfStamper.FormFlattening = true;

                //-------------------------------------------
            }
            catch (Exception ex)
            {
                StringBuilder s = new StringBuilder();
                foreach (var d in dic)
                {
                    s.Append(string.Format("{0}: {1}；", d.Key, d.Value));
                }
                throw new Exception(ex.Message + string.Format("，数据【{0}】", s), ex);
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
        }

        private string formatZY_Use(string zy, DateTime ValidStartDate)
        {
            //格式化专业
            zy = zy.Replace("建筑", "建筑工程").Replace("公路", "公路工程").Replace("水利", "水利水电工程").Replace("市政", "市政公用工程").Replace("矿业", "矿业工程").Replace("机电", "机电工程");

            //格式化有效期
            string[] str = zy.Split('（');

            return string.Format("{0}（有效期：{1}至{2}）"
                , str[0]
                , ValidStartDate.ToString("yyyy-M-d")
                , Convert.ToDateTime(str[1].Replace("）", "")).ToString("yyyy-M-d")
                );
        }

        /// <summary>
        /// 获取证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
        /// </summary>
        /// <param name="PSN_RegisterNO">证书注册号</param>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns></returns>
        public string GetFaceImagePathJZS(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string imgPath = "";
            string FacePhoto = COC_TOW_Person_FileDAL.GetFileUrl(PSN_RegisterNO, EnumManager.FileDataTypeName.一寸免冠照片);
            if (string.IsNullOrEmpty(FacePhoto) == false)
            {
                if (File.Exists(FacePhoto.Replace("~",ryzgRoot )) == true)
                {
                    imgPath = FacePhoto.Replace("~", ryzgRoot);
                }
            }
            if (imgPath == "")
            {
                if (PSN_CertificateNO.IndexOf('?') == -1)
                {
                    string path = string.Format("{2}/UpLoad/WorkerPhoto/{0}/{1}.jpg", PSN_CertificateNO.Substring(PSN_CertificateNO.Length - 3, 3), PSN_CertificateNO, ryzgRoot);
                    if (File.Exists(path) == true)
                    {
                        imgPath = path;
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = string.Format("{0}/Images/tup.gif", ryzgRoot);
            }

            return imgPath;
        }

        /// <summary>
        /// 获取证书绑定个人签名照，如果没有显示人员目录最新上传个人签名照
        /// </summary>
        /// <param name="PSN_RegisterNO">证书注册号</param>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns></returns>
        public string GetSignPhotoJZS(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string imgPath = "";
            string signPhoto = COC_TOW_Person_FileDAL.GetFileUrl(PSN_RegisterNO, EnumManager.FileDataTypeName.手写签名照);
            if (string.IsNullOrEmpty(signPhoto) == false)
            {
                if (File.Exists(signPhoto.Replace("~", ryzgRoot)) == true)
                {
                    imgPath = signPhoto.Replace("~", ryzgRoot);
                }
            }
            if (imgPath == "")
            {
                if (PSN_CertificateNO.IndexOf('?') == -1)
                {
                    WorkerOB ob = WorkerDAL.GetUserObject(PSN_CertificateNO);
                    if (ob.SignPhotoTime.HasValue == true)
                    {
                        string path = string.Format("{2}/UpLoad/SignImg/{0}/{1}.jpg", PSN_CertificateNO.Substring(PSN_CertificateNO.Length - 3, 3), PSN_CertificateNO, ryzgRoot);
                        if (File.Exists(path) == true)
                        {
                            imgPath = path;
                        }
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = string.Format("{0}/Images/SignNull.jpg", ryzgRoot);
            }

            return imgPath;
        }

        /// <summary>
        /// 生成二级建造师电子使用件
        /// </summary>
        public void CreateEJUse_CA()
        {
            //**********************************************************************************************************
            //需要改造成
            //1、生成代签名的无签章证书pdf；
            //2、发送pdf文件到109服务器 D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf；
            //3、109上服务调用接口签章（已完成）；
            //4、取回pdf文件到人员系统D:\WebRoot\CAFile\XXX\GUID.pdf
            //**********************************************************************************************************

//            string sql = String.Format(@"Select top 1 * FROM [dbo].[View_JZS_Use]
//                                        where [Valid] > 0 and  [ApplyCATime] is null and [PSN_RegisteType] <7
//                                        and PSN_RegisterNO ='{0}'
//                                        order by [CJSJ]", "京2112014201653442");
            string sql = String.Format(@"Select top 1 * FROM [dbo].[View_JZS_Use]
                                        where  PSN_RegisterNO ='{0}'
                                        order by [CJSJ]", "京2112014201653442");
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal == null || dtOriginal.Rows.Count == 0)
            {
                return;
            }

            var template_url = string.Format(@"{0}\Template\二建使用件.pdf", ryzgRoot);
            var save_pdf_url = "";//pdf生成位置
            //string CertificateCAID = "";
            //string fileTo = "";

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (dr["PSN_CertificateNO"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["PSN_CertificateNO"].ToString()) == false)
                {
                    continue;//身份证错误的不生成电子证书，签章服务不接收
                }

                ////创建pdf
                //CertificateCAID = Guid.NewGuid().ToString();

                //fileTo = string.Format(@"{0}\{1}", CAFile, CertificateCAID.Substring(CertificateCAID.Length - 3, 3));

                //pdf目录
                //if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ryzgRoot)))
                //{
                //    System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ryzgRoot));
                //}

                //save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ryzgRoot, dr["CertificateCAID"]);//目标文件地址
                save_pdf_url = @"d:\01test\二建使用件.pdf";

                try
                {
                    var dic = ReadForm(template_url);//读取模板
                    FillFormOfJZS_Use(template_url, save_pdf_url, dic, dr);//填充模板
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级建造师电子证书使用件失败，错误信息：" + ex.Message, ex);
                    //WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "创建", ex.Message);
                    continue;
                }

                try
                {
                    ////begin测试专用（只拷贝待签章ofd到109）************************************************************************************************************************************
                    //File.Copy(save_pdf_url, string.Format(@"{0}\{1}_Ofd.pdf", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", dr["CertificateCAID"]), true);//替换文件
                    //File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", dr["CertificateCAID"]), true);//替换文件

                    //更新证书表,写入申请时间
                    CommonDAL.ExecSQL(string.Format(@"update  DBO.[EJCertUse] set [ApplyCATime]='{1}' where [CertificateCAID]='{0}';", dr["CertificateCAID"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    FileLog.WriteLog(string.Format("创建二建电子证书使用件{0}.pdf成功。", dr["CertificateCAID"]));
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级建造师电子证书信息失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// 培训点登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonTrainUnit_Click(object sender, EventArgs e)
        {
            string qyid = "";//企业ID
            string qymc = "";//企业名称
            string qyregion = "";//所属区县
            string zzjgdm = "91110000722617424D";//组织机构代码
            string shtyxydm = "";//社会统一信用代码
          
            UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);

            if (o != null)
            {
                qyid = o.UnitID;//企业ID
                qymc = o.ENT_Name;//企业名称
                qyregion = o.ENT_City;//区县
                shtyxydm = o.CreditCode;//社会统一信用代码

                GSJ_QY_GSDJXXMDL g = UnitDAL.GetObjectUni_scid(o.CreditCode);//工商记录
                if (g != null)//更新验证状态
                {
                    o.ResultGSXX = 2;
                }
                else
                {
                    o.ResultGSXX = 1;
                }
                o.ApplyTimeGSXX = DateTime.Now;
                UnitDAL.UpdateResultGSXX(o);
            }
            else
            {
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的企业信息，请确认是否已经在系统注册了企业信息！"), true);
            }

            //是否为央企或大集团（有子公司）
            int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

            //是否为培训点
            int ifTrainUnit = CommonDAL.GetRowCount("[TrainUnit]", "1", string.Format(" and [UnitCode]='{0}' and [UseStatus]=1 ", shtyxydm));

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
            string userInfo = "";
            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码)，8）用户最后登录时间
            if (countUnit > 0 && ifTrainUnit > 0)//央企或大集团、培训点
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100|200", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (countUnit > 0)//央企或大集团
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (ifTrainUnit > 0)//培训点
            {
                //userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
            }
            else//一般企业
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
            }

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "3";
            //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, qyid), loginTime, 10);
            //Session[string.Format("{0}user{1}", _personType, qyid)] = loginTime;

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;
            try
            {
                UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业住建委办事大厅统一身份认证登录@");
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }

        protected void ButtonTrainUnit2_Click(object sender, EventArgs e)
        {
            string qyid = "";//企业ID
            string qymc = "";//企业名称
            string qyregion = "";//所属区县
            string zzjgdm = "12110102753330936R";//组织机构代码
            string shtyxydm = "";//社会统一信用代码

            UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);

            if (o != null)
            {
                qyid = o.UnitID;//企业ID
                qymc = o.ENT_Name;//企业名称
                qyregion = o.ENT_City;//区县
                shtyxydm = o.CreditCode;//社会统一信用代码

                GSJ_QY_GSDJXXMDL g = UnitDAL.GetObjectUni_scid(o.CreditCode);//工商记录
                if (g != null)//更新验证状态
                {
                    o.ResultGSXX = 2;
                }
                else
                {
                    o.ResultGSXX = 1;
                }
                o.ApplyTimeGSXX = DateTime.Now;
                UnitDAL.UpdateResultGSXX(o);
            }
            else
            {
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的企业信息，请确认是否已经在系统注册了企业信息！"), true);
            }

            //是否为央企或大集团（有子公司）
            int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

            //是否为培训点
            int ifTrainUnit = CommonDAL.GetRowCount("[TrainUnit]", "1", string.Format(" and [UnitCode]='{0}' and [UseStatus]=1 ", shtyxydm));

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
            string userInfo = "";
            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码)，8）用户最后登录时间
            if (countUnit > 0 && ifTrainUnit > 0)//央企或大集团、培训点
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100|200", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (countUnit > 0)//央企或大集团
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
            }
            else if (ifTrainUnit > 0)//培训点
            {
                //userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
            }
            else//一般企业
            {
                userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
            }

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "3";
            //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, qyid), loginTime, 10);
            //Session[string.Format("{0}user{1}", _personType, qyid)] = loginTime;

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;
            try
            {
                UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业住建委办事大厅统一身份认证登录@");
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }

    }

    public static class RSAExtensions
    {
        /// <summary>
        ///  把java的私钥转换成.net的xml格式
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="privateJavaKey"></param>
        /// <returns></returns>
        public static string ConvertToXmlPrivateKey(this RSA rsa, string privateJavaKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateJavaKey));
            string xmlPrivateKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                         Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
            return xmlPrivateKey;
        }
        /// <summary>
        /// RSA加载JAVA  PrivateKey
        /// </summary>
        /// <param name="privateJavaKey">java提供的第三方私钥</param>
        /// <returns></returns>
        public static void FromPrivateKeyJavaString(this RSA rsa, string privateJavaKey)
        {
            string xmlPrivateKey = rsa.ConvertToXmlPrivateKey(privateJavaKey);
            rsa.FromXmlString(xmlPrivateKey);
        }

        /// <summary>
        /// 把java的公钥转换成.net的xml格式
        /// </summary>
        /// <param name="privateKey">java提供的第三方公钥</param>
        /// <returns></returns>
        public static string ConvertToXmlPublicJavaKey(this RSA rsa, string publicJavaKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicJavaKey));
            string xmlpublicKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
              Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
              Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
            return xmlpublicKey;
        }

        /// <summary>
        /// 把java的私钥转换成.net的xml格式
        /// </summary>
        /// <param name="publicJavaKey">java提供的第三方公钥</param>
        /// <returns></returns>
        public static void FromPublicKeyJavaString(this RSA rsa, string publicJavaKey)
        {
            string xmlpublicKey = rsa.ConvertToXmlPublicJavaKey(publicJavaKey);
            rsa.FromXmlString(xmlpublicKey);
        }
        /// <summary>
        /// RSA公钥格式转换，java->.net
        /// </summary>
        /// <param name="publicKey">java生成的公钥</param>
        /// <returns></returns>
        private static string ConvertJavaPublicKeyToDotNet(this RSA rsa, string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>Extension method for initializing a RSACryptoServiceProvider from PEM data string.</summary>

        #region Methods

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a DER public key blob.</summary>
        public static void LoadPublicKeyDER(this RSACryptoServiceProvider provider, byte[] DERData)
        {
            byte[] RSAData = GetRSAFromDER(DERData);
            byte[] publicKeyBlob = GetPublicKeyBlobFromRSA(RSAData);
            provider.ImportCspBlob(publicKeyBlob);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a DER private key blob.</summary>
        public static void LoadPrivateKeyDER(this RSACryptoServiceProvider provider, byte[] DERData)
        {
            byte[] privateKeyBlob = GetPrivateKeyDER(DERData);
            provider.ImportCspBlob(privateKeyBlob);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a PEM public key string.</summary>
        public static void LoadPublicKeyPEM(this RSACryptoServiceProvider provider, string sPEM)
        {
            byte[] DERData = GetDERFromPEM(sPEM);
            LoadPublicKeyDER(provider, DERData);
        }

        /// <summary>Extension method which initializes an RSACryptoServiceProvider from a PEM private key string.</summary>
        public static void LoadPrivateKeyPEM(this RSACryptoServiceProvider provider, string sPEM)
        {
            byte[] DERData = GetDERFromPEM(sPEM);
            LoadPrivateKeyDER(provider, DERData);
        }

        /// <summary>Returns a public key blob from an RSA public key.</summary>
        internal static byte[] GetPublicKeyBlobFromRSA(byte[] RSAData)
        {
            byte[] data = null;
            UInt32 dwCertPublicKeyBlobSize = 0;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), RSAData, (UInt32)RSAData.Length, CRYPT_DECODE_FLAGS.NONE,
                data, ref dwCertPublicKeyBlobSize))
            {
                data = new byte[dwCertPublicKeyBlobSize];
                if (!CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING,
                    new IntPtr((int)CRYPT_OUTPUT_TYPES.RSA_CSP_PUBLICKEYBLOB), RSAData, (UInt32)RSAData.Length, CRYPT_DECODE_FLAGS.NONE,
                    data, ref dwCertPublicKeyBlobSize))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return data;
        }

        /// <summary>Converts DER binary format to a CAPI CRYPT_PRIVATE_KEY_INFO structure.</summary>
        internal static byte[] GetPrivateKeyDER(byte[] DERData)
        {
            byte[] data = null;
            UInt32 dwRSAPrivateKeyBlobSize = 0;
            IntPtr pRSAPrivateKeyBlob = IntPtr.Zero;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.PKCS_RSA_PRIVATE_KEY),
                DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwRSAPrivateKeyBlobSize))
            {
                data = new byte[dwRSAPrivateKeyBlobSize];
                if (!CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.PKCS_RSA_PRIVATE_KEY),
                    DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwRSAPrivateKeyBlobSize))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return data;
        }

        /// <summary>Converts DER binary format to a CAPI CERT_PUBLIC_KEY_INFO structure containing an RSA key.</summary>
        internal static byte[] GetRSAFromDER(byte[] DERData)
        {
            byte[] data = null;
            byte[] publicKey = null;
            CERT_PUBLIC_KEY_INFO info;
            UInt32 dwCertPublicKeyInfoSize = 0;
            IntPtr pCertPublicKeyInfo = IntPtr.Zero;
            if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
            {
                data = new byte[dwCertPublicKeyInfoSize];
                if (CryptDecodeObject(CRYPT_ENCODING_FLAGS.X509_ASN_ENCODING | CRYPT_ENCODING_FLAGS.PKCS_7_ASN_ENCODING, new IntPtr((int)CRYPT_OUTPUT_TYPES.X509_PUBLIC_KEY_INFO),
                    DERData, (UInt32)DERData.Length, CRYPT_DECODE_FLAGS.NONE, data, ref dwCertPublicKeyInfoSize))
                {
                    GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    try
                    {
                        info = (CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CERT_PUBLIC_KEY_INFO));
                        publicKey = new byte[info.PublicKey.cbData];
                        Marshal.Copy(info.PublicKey.pbData, publicKey, 0, publicKey.Length);
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
                else
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return publicKey;
        }

        /// <summary>Extracts the binary data from a PEM file.</summary>
        internal static byte[] GetDERFromPEM(string sPEM)
        {
            UInt32 dwSkip, dwFlags;
            UInt32 dwBinarySize = 0;

            if (!CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, null, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            byte[] decodedData = new byte[dwBinarySize];
            if (!CryptStringToBinary(sPEM, (UInt32)sPEM.Length, CRYPT_STRING_FLAGS.CRYPT_STRING_BASE64HEADER, decodedData, ref dwBinarySize, out dwSkip, out dwFlags))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return decodedData;
        }

        #endregion Methods

        #region P/Invoke Constants

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_ACQUIRE_CONTEXT_FLAGS : uint
        {
            CRYPT_NEWKEYSET = 0x8,
            CRYPT_DELETEKEYSET = 0x10,
            CRYPT_MACHINE_KEYSET = 0x20,
            CRYPT_SILENT = 0x40,
            CRYPT_DEFAULT_CONTAINER_OPTIONAL = 0x80,
            CRYPT_VERIFYCONTEXT = 0xF0000000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_PROVIDER_TYPE : uint
        {
            PROV_RSA_FULL = 1
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_DECODE_FLAGS : uint
        {
            NONE = 0,
            CRYPT_DECODE_ALLOC_FLAG = 0x8000
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_ENCODING_FLAGS : uint
        {
            PKCS_7_ASN_ENCODING = 0x00010000,
            X509_ASN_ENCODING = 0x00000001,
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_OUTPUT_TYPES : int
        {
            X509_PUBLIC_KEY_INFO = 8,
            RSA_CSP_PUBLICKEYBLOB = 19,
            PKCS_RSA_PRIVATE_KEY = 43,
            PKCS_PRIVATE_KEY_INFO = 44
        }

        /// <summary>Enumeration derived from Crypto API.</summary>
        internal enum CRYPT_STRING_FLAGS : uint
        {
            CRYPT_STRING_BASE64HEADER = 0,
            CRYPT_STRING_BASE64 = 1,
            CRYPT_STRING_BINARY = 2,
            CRYPT_STRING_BASE64REQUESTHEADER = 3,
            CRYPT_STRING_HEX = 4,
            CRYPT_STRING_HEXASCII = 5,
            CRYPT_STRING_BASE64_ANY = 6,
            CRYPT_STRING_ANY = 7,
            CRYPT_STRING_HEX_ANY = 8,
            CRYPT_STRING_BASE64X509CRLHEADER = 9,
            CRYPT_STRING_HEXADDR = 10,
            CRYPT_STRING_HEXASCIIADDR = 11,
            CRYPT_STRING_HEXRAW = 12,
            CRYPT_STRING_NOCRLF = 0x40000000,
            CRYPT_STRING_NOCR = 0x80000000
        }

        #endregion P/Invoke Constants

        #region P/Invoke Structures

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_OBJID_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            internal IntPtr pszObjId;
            internal CRYPT_OBJID_BLOB Parameters;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CRYPT_BIT_BLOB
        {
            internal UInt32 cbData;
            internal IntPtr pbData;
            internal UInt32 cUnusedBits;
        }

        /// <summary>Structure from Crypto API.</summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CERT_PUBLIC_KEY_INFO
        {
            internal CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            internal CRYPT_BIT_BLOB PublicKey;
        }

        #endregion P/Invoke Structures

        #region P/Invoke Functions

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDestroyKey(IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptImportKey(IntPtr hProv, byte[] pbKeyData, UInt32 dwDataLen, IntPtr hPubKey, UInt32 dwFlags, ref IntPtr hKey);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptReleaseContext(IntPtr hProv, Int32 dwFlags);

        /// <summary>Function for Crypto API.</summary>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer, string pszProvider, CRYPT_PROVIDER_TYPE dwProvType, CRYPT_ACQUIRE_CONTEXT_FLAGS dwFlags);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptStringToBinary(string sPEM, UInt32 sPEMLength, CRYPT_STRING_FLAGS dwFlags, [Out] byte[] pbBinary, ref UInt32 pcbBinary, out UInt32 pdwSkip, out UInt32 pdwFlags);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObjectEx(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS dwFlags, IntPtr pDecodePara, ref byte[] pvStructInfo, ref UInt32 pcbStructInfo);

        /// <summary>Function from Crypto API.</summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptDecodeObject(CRYPT_ENCODING_FLAGS dwCertEncodingType, IntPtr lpszStructType, byte[] pbEncoded, UInt32 cbEncoded, CRYPT_DECODE_FLAGS flags, [In, Out] byte[] pvStructInfo, ref UInt32 cbStructInfo);

        #endregion P/Invoke Functions

        
    }
}