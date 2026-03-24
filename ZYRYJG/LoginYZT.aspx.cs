using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using ZYRYJG.Thehall;
using System.Text;

namespace ZYRYJG
{
    #region 个人首都之窗集成登录认证
    //北京市住房和城乡建设领域人员资格管理信息系统”在统一身份认证平台注册信息如下 ：
    //   测试环境：
    //     应用ID：100100000393
    //     应用secret：22bc1216aaba3c449b6e7fe38e52e79a
    //   正式环境：
    //     应用ID：100100000502
    //     应用secret：d580ba598588385c83ea83b71971e032
    //   测试环境----
    // PC登录地址：https://t200renzheng.zhengtoon.com/open/login/goUserLogin?client_id=100100000393&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=
    // H5登录地址：https://t200renzheng.zhengtoon.com/open/m/login/goUserLogin?client_id=100100000393&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=
    // H5授权地址：https://t200renzheng.zhengtoon.com/open/auth/authorize?client_id=100100000393&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=&toonType=102
    //测试环境进行用户注册时，点击获取验证码按钮后，固定输入1111.
    //   正式环境----
    // PC登录地址：https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=
    // H5登录地址：https://bjt.beijing.gov.cn/renzheng/open/m/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=
    // H5授权地址：https://bjt.beijing.gov.cn/renzheng/open/auth/authorize?client_id=100100000502&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=&toonType=102


    //1 用户唯一标示 uniqueId
    //    格式:UUID, 如: f73541fa-aeca-342c-c087-efae7945a490
    //2 用户名 userName
    //3 姓名 certName 实名用户(L2 级及以上)用户返回
    //4 证件号码 certNo
    //    64 位加密字符串, 实名用户(L2 级及以上)用户返回
    //5 证件类型代号 certNoType 格式:数字为代号
    //    1,身份证；2,护照；3,港澳通行证；4,台湾来往大陆通行证(台胞证)；5,港澳居民居住证；6,台湾居民居住证；
    //    7,港澳居民来往内地通行证(澳门)；8,港澳居民来往内地通行证(香港)；9,外国人永久居留身份证；78,港澳居民来往内地通行证
    //6 证件类型名称 certNoTypeName
    //7 认证等级 certLevel
    //    L1：未认证，匿名；
    //    L2：身份证（姓名、身份证号）认证；
    //    L3：人脸识别或者银行卡四要素认证；
    //    L4：线下（实体大厅）认证；
    //8 性别 sex 0:男, 1:女
    //9 生日 birthDay 格式: yyyyMMdd,如:19990206
    //10 民族 ethnicity 民族,如:汉族
    //11 北京通市民卡号 toonNo 格式：12 位数字,如:602251135922
    //12 版本号 version 无意义字段，建议忽略
    //13 手机号 mobile 11 位手机号
    //14 邮箱 mail
    //15 授权 / 登录标志 personToken 256 位加密字符串
    //16 应用唯一标示 clientId

    // "data":{
    //"birthDay":"19770520",
    //"certNoType":"1",
    //"clientId":"100100000502",
    //"certLevel":"L3",//认证级别
    //"mail":"",
    //"ethnicity":"",//民族
    //"sex":0,
    //"mobile":"136XXXXXXXX",//手机号码
    //"userName":"tailiangle",//用户账户名（登录名）
    //"certNoTypeName":"中国居民身份证",
    //"version":"1.0",
    //"certNo":"4286e00b4ae1261fbceb3c594080c0378405f50c8cfd17e23c26235f3e4ae47c",（需要解密）
    //"certName":"张三",//登录名
    //"toonNo":"343571185072",
    //"uniqueId":"7b60ce3c-47bf-31f4-be47-f586b1f98104"
    //}

    #endregion
    public partial class LoginYZT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["code"]) == true)
                {
                    Response.Write("用户集成认证失败！");
                    Utility.FileLog.WriteLog("用户集成认证失败！北京通未返回回调码code。");
                    return;
                }

                string client_id = "100100000502";//对接客户端ID
                string client_Secret = "d580ba598588385c83ea83b71971e032";//秘钥
                string grant_type = "authorization_code";
                string grant_code = Request["code"];
                string apiUrl = "https://bjt.beijing.gov.cn/renzheng/api/oauth/getAccessToken?";
                object currenttimemillis = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                Utility.JsonTool jsonTool;
                string auth_token;//认证Token
                string AccessToken;//访问Token
                string Code;//返回码
                string errMsg;//返回消息
                string rtn;//接口返回字符串
                string userInfo = "";//用户登录信息
                string _IDCard;//证件号码
                string certLevel;//认证级别

                try
                {
                    //AES256(“${client_id}:${client_secret}:${timestamp}”)，
                    auth_token = Utility.Cryptography.AES_Encrypt(string.Format("{0}:{1}:{2}", client_id, client_Secret, currenttimemillis), client_Secret);

                    //获取访问token
                    rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("client_id={0}&grant_type={1}&grant_code={2}&auth_token={3}", client_id, grant_type, grant_code, auth_token));

                    //Utility.FileLog.WriteLog(string.Format("用户集成认证测试1：rtn={0}",rtn));
                    if (null == rtn)
                    {
                        Response.Write("用户集成认证失败！");
                        Utility.FileLog.WriteLog("用户集成认证失败！获取 accessToken 返回空。");
                        return;
                    }
                    else
                    {
                        jsonTool = new Utility.JsonTool(rtn);
                        Code = jsonTool.getValue("meta.code");
                        errMsg = jsonTool.getValue("meta.message");

                        if (Code != "0")//失败
                        {
                            Response.Write("用户集成认证失败！");
                            Utility.FileLog.WriteLog(string.Format("用户集成认证失败！获取 accessToken 失败，错误：{0}。", errMsg));
                            return;
                        }
                        else
                        {
                            //接口返回值结构
                            //“data”: {
                            //"access_token": "79043369-5e86-390c-ae5d-185da7346ecd", 
                            //"token_type": "", 
                            //"expires_in": 1541075497635, //过期时间
                            //"refresh_token": "11043369-5e86-390c-ae5d-185da7346ecd"

                            AccessToken = jsonTool.getValue("data.access_token");
                            //Response.Write(string.Format("获取 accessToken 成功,{0}", AccessToken));

                            //Utility.FileLog.WriteLog(string.Format("用户集成认证测试2：AccessToken={0}", AccessToken));

                            //获取用户信息
                            apiUrl = "https://bjt.beijing.gov.cn/renzheng/api/info/getUserInfo?";
                            rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("access_token={0}", AccessToken));

                            //Utility.FileLog.WriteLog(string.Format("用户集成认证测试3：user data rtn={0}", rtn));
                            //Response.Write(string.Format("userInfo：{0}", rtn));

                            jsonTool = new Utility.JsonTool(rtn);

                            #region 用户信息返回结构参考

                            //1 用户唯一标示 uniqueId
                            //    格式:UUID, 如: f73541fa-aeca-342c-c087-efae7945a490
                            //2 用户名 userName
                            //3 姓名 certName 实名用户(L2 级及以上)用户返回
                            //4 证件号码 certNo
                            //    64 位加密字符串, 实名用户(L2 级及以上)用户返回
                            //5 证件类型代号 certNoType 格式:数字为代号
                            //    1,身份证；2,护照；3,港澳通行证；4,台湾来往大陆通行证(台胞证)；5,港澳居民居住证；6,台湾居民居住证；
                            //    7,港澳居民来往内地通行证(澳门)；8,港澳居民来往内地通行证(香港)；9,外国人永久居留身份证；78,港澳居民来往内地通行证
                            //6 证件类型名称 certNoTypeName
                            //7 认证等级 certLevel
                            //    L1：未认证，匿名；
                            //    L2：身份证（姓名、身份证号）认证；
                            //    L3：人脸识别或者银行卡四要素认证；
                            //    L4：线下（实体大厅）认证；
                            //8 性别 sex 0:男, 1:女
                            //9 生日 birthDay 格式: yyyyMMdd,如:19990206
                            //10 民族 ethnicity 民族,如:汉族
                            //11 北京通市民卡号 toonNo 格式：12 位数字,如:602251135922
                            //12 版本号 version 无意义字段，建议忽略
                            //13 手机号 mobile 11 位手机号
                            //14 邮箱 mail
                            //15 授权 / 登录标志 personToken 256 位加密字符串
                            //16 应用唯一标示 clientId

                            // "data":{
                            //"birthDay":"19770520",
                            //"certNoType":"1",
                            //"clientId":"100100000502",
                            //"certLevel":"L3",//认证级别
                            //"mail":"",
                            //"ethnicity":"",//民族
                            //"sex":0,
                            //"mobile":"13671197677",//手机号码
                            //"userName":"tailiangle",//用户账户名（登录名）
                            //"certNoTypeName":"中国居民身份证",
                            //"version":"1.0",
                            //"certNo":"4286e00b4ae1261fbceb3c594080c0378405f50c8cfd17e23c26235f3e4ae47c",
                            //"certName":"廖亮",//登录名
                            //"toonNo":"343571185072",
                            //"uniqueId":"7b60ce3c-47bf-31f4-be47-f586b1f98104"
                            //}

                            #endregion 用户信息返回结构参考

                            #region 更新个人信息

                            DateTime temp_date;
                            try
                            {
                                bool checkdate = DateTime.TryParse(jsonTool.getValue("data.birthDay").Insert(4, "-").Insert(7, "-"), out temp_date);//生日
                                if (checkdate == false)
                                {
                                    checkdate = DateTime.TryParse(jsonTool.getValue("data.birthDay"), out temp_date);

                                }
                                if (checkdate == false)
                                {
                                    Utility.FileLog.WriteLog(string.Format("用户集成认证失败！错误信息：data.birthDay={0}", jsonTool.getValue("data.birthDay")));
                                    Response.Write("用户集成认证失败！出生日期格式不正确。");
                                    return;
                                }
                            }
                            catch
                            {
                                Utility.FileLog.WriteLog(string.Format("用户集成认证失败！错误信息：data.birthDay={0}", jsonTool.getValue("data.birthDay")));
                                return;
                            }

                            _IDCard = Utility.Cryptography.AES_Decrypt(jsonTool.getValue("data.certNo"), client_Secret);

                            //Utility.FileLog.WriteLog(string.Format("用户集成认证测试4：_IDCard={0}", _IDCard));

                            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(_IDCard);
                            //Utility.FileLog.WriteLog(string.Format("用户集成认证测试5：WorkerOB={0}", _WorkerOB.WorkerName));

                            //Utility.FileLog.WriteLog(string.Format("用户集成认证测试6：data.certName={0}，data.sex={1}，data.birthDay={2}，data.certNoType={3}，data.mobile={4}，data.certLevel={4}"
                            //       , jsonTool.getValue("data.certName")
                            //    , jsonTool.getValue("data.sex")
                            //      , jsonTool.getValue("data.birthDay")
                            //       , jsonTool.getValue("data.certNoType")
                            //        , jsonTool.getValue("data.mobile")
                            //        , jsonTool.getValue("data.certLevel")

                            //    ));
                            if (_WorkerOB == null)//new
                            {
                                _WorkerOB = new WorkerOB();
                                _WorkerOB.WorkerName = jsonTool.getValue("data.certName");     //姓名
                                _WorkerOB.Sex = (jsonTool.getValue("data.sex") == "1" ? "女" : "男");  //性别
                                _WorkerOB.Birthday = temp_date;//生日
                                _WorkerOB.CertificateType = GetCertTypeName(jsonTool.getValue("data.certNoType")); //证件类别
                                _WorkerOB.CertificateCode = _IDCard;  //证件号码
                                _WorkerOB.Phone = jsonTool.getValue("data.mobile");  //联系电话
                                _WorkerOB.Mobile = _WorkerOB.Phone;  //联系电话
                                _WorkerOB.UUID = jsonTool.getValue("data.uniqueId");     
                                WorkerDAL.Insert(_WorkerOB);
                            }
                            else
                            {
                                _WorkerOB.WorkerName = jsonTool.getValue("data.certName");     //姓名
                                _WorkerOB.Sex = (jsonTool.getValue("data.sex") == "1" ? "女" : "男");  //性别
                                _WorkerOB.Birthday = temp_date;//生日
                                _WorkerOB.CertificateType = GetCertTypeName(jsonTool.getValue("data.certNoType")); //证件类别
                                _WorkerOB.CertificateCode = _IDCard;  //证件号码
                                _WorkerOB.Phone = jsonTool.getValue("data.mobile");  //联系电话
                                _WorkerOB.Mobile = _WorkerOB.Phone;  //联系电话
                                _WorkerOB.UUID = jsonTool.getValue("data.uniqueId");     
                                WorkerDAL.Update(_WorkerOB);

                                //Utility.FileLog.WriteLog(string.Format("用户集成认证测试6：WorkerDAL.Update={0}", _WorkerOB.CertificateCode));
                            }

                            #endregion 个人登录信息

                            certLevel = jsonTool.getValue("data.certLevel");
                            if (certLevel == "L1" || certLevel == "L2")
                            {
                                Response.Write(string.Format(@"<p>系统要求必须达到L3或以上实名制级别才允许登录系统办理业务，请到<a href=""http://www.beijing.gov.cn/\"">首都之窗</a> 进行实名制认证升级。  <br /> <br /> 
                                 L1：未认证，匿名；<br /> 
                                 L2：身份证（姓名、身份证号）认证；<br /> 
                                 L3：人脸识别或者银行卡四要素认证；<br /> 
                                 L4：线下（实体大厅）认证；<br /> 
                                </p>"
                                    ));
                                return;
                            }

                            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
                            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码) ，8）用户最后登录时间      
                            userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);

                            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
                            string _personType = "2";
                            //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID), loginTime, 10);
                            //Session[string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID)] = loginTime;

                            userInfo = Cryptography.Encrypt(userInfo);
                            FormsAuthentication.SetAuthCookie(userInfo, false);
                            Session["userInfo"] = userInfo;

                            try
                            {
                                UIHelp.WriteOperateLog(_WorkerOB.WorkerName, _WorkerOB.WorkerID.ToString(), "登录", "个人北京市统一身份认证平台登录");
                            }
                            catch { }

                            //获取三类人、专业人员考试近1年缺考考试计划信息
                            try
                            {
                                System.Data.DataTable dtMissExam = ExamResultDAL.GetMissExamList(1, _WorkerOB.CertificateCode);
                                if (dtMissExam != null && dtMissExam.Rows.Count > 0)
                                {
                                    Session["myMissExam"] = string.Format("{0}|{1}", Convert.ToDateTime(dtMissExam.Rows[0]["EXAMSTARTDATE"]).ToString("yyyy-MM-dd"), dtMissExam.Rows[0]["PostName"]);
                                }
                            }
                            catch { }

                            #region 跳转进入我们系统后，立即注销首都之窗登录
                            try
                            {
                                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                                ht.Add("access_token", AccessToken);
                                rtn = Utility.HttpHelp.DoGet("https://bjt.beijing.gov.cn/renzheng/api/login/doSSOLogout? ", ht);
                                //Utility.FileLog.WriteLog(string.Format("调用注销接口,返回参数：{0}", rtn));
                            }
                            catch { }

                            #endregion

                            if (string.IsNullOrEmpty(Request["state"]) == false)
                            {
                                Response.Redirect(string.Format("~/Default.aspx?action={0}", Request["state"]), false);
                            }
                            else
                            {
                                Response.Redirect("~/Default.aspx", false);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    Response.Write("用户集成认证失败！");
                    Utility.FileLog.WriteLog(string.Format("用户集成认证失败！错误信息：{0}<br/>{1}", ex.Message, ex.StackTrace));
                    return;
                }
            }
        }

        /// <summary>
        /// 获取证件类型名称
        /// </summary>
        /// <param name="CertCode">证件类型编号</param>
        /// <returns></returns>
        protected string GetCertTypeName(string CertCode)
        {
            switch (CertCode)
            {
                case "1":
                    return "身份证";

                case "2":
                    return "护照";

                case "3":
                    return "港澳通行证";

                case "4":
                    return "台湾来往大陆通行证(台胞证)";

                case "5":
                    return "港澳居民居住证";

                case "6":
                    return "台湾居民居住证";

                case "7":
                    return "港澳居民来往内地通行证(澳门)";

                case "8":
                    return "港澳居民来往内地通行证(香港)";

                case "9":
                    return "外国人永久居留身份证";

                case "78":
                    return "港澳居民来往内地通行证";

                case "999":
                    return "其它证件";
                default:
                    return "其它证件";
            }
        }

    }
}