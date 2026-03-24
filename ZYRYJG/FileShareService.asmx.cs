using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ZYRYJG
{
    /// <summary>
    /// 人员资格管理系统文件共享服务
    /// </summary>
    [WebService(Namespace = "http://FileShareWebService.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class FileShareService : System.Web.Services.WebService
    {
        //服务ID
        private const string serviceID = "43F8E960-D347-4781-93B9-BB64168F63D7";

        private string _ErrorMessage = "";

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMessage { get { return _ErrorMessage; } }

        /// <summary>
        /// 根据证件号码获取人员一寸免冠照片
        /// </summary>
        /// <param name="UserName">登录名</param>
        /// <param name="Password">密码</param>
        /// <param name="IDCode">证件号码</param>
        /// <returns>返回值：照片byte[]格式</returns>
        [WebMethod(Description = "根据证件号码获取人员一寸免冠照片<br>UserName：登录名<br>Password：密码<br>IDCode：证件号码<br>返回值：照片byte[]格式")]
        public byte[] GetPersonIDPhotoByte(string UserName, string Password, string IDCode)
        {
            try
            {
                //身份验证
                if (!IsValidate(UserName, Password))
                {
                    throw new ApplicationException("身份认证无效!");
                }
                //证件号码验证

                //图片查找
                string path = string.Format("{0}/{1}/{2}.jpg", System.Configuration.ConfigurationManager.AppSettings["IDPhotoPath"], IDCode.Substring(IDCode.Length - 3, 3), IDCode);
                if (File.Exists(path) == false)
                {
                    return null;
                }

                //图片数据
                byte[] rtn = Utility.ImageHelp.ImgToByte(path);

                Utility.FileLog.WriteVisitLog(DateTime.Now, Utility.Cryptography.Decrypt(UserName), serviceID, "GetPersonIDPhotoByte", IDCode, "根据证件号码获取人员一寸免冠照片");

                return rtn;
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("获取人员一寸免冠照片失败", ex);
                _ErrorMessage = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 根据证书编号获取电子证书PDF
        /// </summary>
        /// <param name="UserName">登录名</param>
        /// <param name="Password">密码</param>
        /// <param name="CertCode">证书编号</param>
        /// <returns>返回值：电子证书byte[]格式</returns>
        [WebMethod(Description = "根据证书编号获取电子证书<br>UserName：登录名<br>Password：密码<br>CertCode：证书编号<br>返回值：电子证书byte[]格式")]
        public byte[] GetCertPDFtoByte(string UserName, string Password, string CertCode)
        {
            try
            {
                //身份验证
                if (!IsValidate(UserName, Password))
                {
                    throw new ApplicationException("身份认证无效!");
                }

                //证件号码验证
                DBHelper db = new DBHelper();
                //string sql = @"SELECT top 1 [CertificateCAID]  FROM [dbo].[CERTIFICATE] where [CERTIFICATECODE]='@CERTIFICATECODE' and posttypeid=2 and [ReturnCATime]>'1950-01-01'";
                string sql = @"SELECT top 1 [CertificateCAID]  FROM [dbo].[CERTIFICATE] where [CERTIFICATECODE]=@CERTIFICATECODE and [ReturnCATime]>'1950-01-01' and posttypeid =2";
                SqlParameter[] para = new SqlParameter[] {  db.CreateParameter("@CERTIFICATECODE",SqlDbType.VarChar,CertCode) };

                string CertificateCAID = db.ExecuteScalar<string>(sql, para);
                if (string.IsNullOrEmpty(CertificateCAID) == true)
                {
                    //Utility.FileLog.WriteLog("没有找到CertificateCAID=null" );
                    _ErrorMessage = "没有找到可用的电子证书。";
                    return null;
                }

                //pdf查找
                string path = string.Format(@"{0}\{1}\{2}.pdf", System.Configuration.ConfigurationManager.AppSettings["CAFile"], CertificateCAID.Substring(CertificateCAID.Length - 3, 3), CertificateCAID);
                //Utility.FileLog.WriteLog(path);
                if (File.Exists(path) == false)
                {
                    //Utility.FileLog.WriteLog("没有找到" + path);
                    return null;
                }

                //pdf数据
                byte[] rtn = Utility.ImageHelp.FileToByte(path);

                //Utility.FileLog.WriteLog("byte_count:" + rtn.Length.ToString());

                Utility.FileLog.WriteVisitLog(DateTime.Now, Utility.Cryptography.Decrypt(UserName), serviceID, "GetCertPDFtoByte", CertCode, "根据证书编号获取电子证书");

                return rtn;
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("获取电子证书PDF失败", ex);
                _ErrorMessage = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 数据加密
        /// </summary>
        /// <param name="content">待加密数据</param>
        /// <returns>加密后数据，加密失败返“数据加密失败”</returns>
        [WebMethod(Description = "数据加密<br>content：待加密数据<br>返回值：加密后数据，加密失败返“数据加密失败”")]
        //[SoapHeader("token", Direction = SoapHeaderDirection.In)]
        public string Encrypt(string content)
        {
            try
            {
                return Utility.Cryptography.Encrypt(content);
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("数据加密失败", ex);
                _ErrorMessage = "数据加密失败";
                return "数据加密失败";
            }
        }

        private bool IsValidate(string _UserName, string _Password)
        {
            int rtn = 0;
            try
            {
                string userName = Utility.Cryptography.Decrypt(_UserName);
                string userPK = Utility.Cryptography.Decrypt(_Password);

                DBHelper db = new DBHelper();
                string sql =
                    @"SELECT count(*)  FROM [192.168.7.89].[BJJS_SQS2.0Project].[dbo].[File_ServiceRight]
where UserId = (select UserId from [192.168.7.89].[BJJS_SQS2.0Project].dbo.Sys_User where [Name] = @Name and [Pwd]= {0}) and [ServiceId] = @ServiceId";


                SqlParameter[] para = new SqlParameter[]
                {    
                    db.CreateParameter("@Name",SqlDbType.VarChar,userName),
                    //db.CreateParameter("@Pwd",SqlDbType.Binary, System.Text.Encoding.UTF8.GetBytes("0x" + Utility.Cryptography.GetMD5Hash(userPK))),

                        //Convert.ToByte( SqlDbType.VarBinary,Utility.Cryptography.GetMD5Hash(userPK)),Convert.to
                    db.CreateParameter("@ServiceId",SqlDbType.VarChar,serviceID)                   
                };
                //                Utility.FileLog.WriteLog(string.Format(@"SELECT count(*)  FROM [dbo].[File_ServiceRight]
                //where UserId = (select UserId from dbo.Sys_User where [Name] = '{0}' and [Pwd]= '{1}') and [ServiceId] = '{2}'",userName,"0x" + Utility.Cryptography.GetMD5Hash(userPK),serviceID), null);
                rtn = db.ExecuteScalar<int>(string.Format(sql, "0x" + Utility.Cryptography.GetMD5Hash(userPK)), para);
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("数据加密失败", ex);
            }
            return (rtn > 0) ? true : false;


        }    
    }
}
