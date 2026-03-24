using System;
using WS_GetData.Api.Model;

namespace WS_GetData.Api
{
    /// <summary>
    ///     执行API
    /// </summary>
    public static class Execute
    {
        /// <summary>
        ///     4.1.1 登录
        /// </summary>
        /// <returns>访问令牌</returns>
        public static string Login()
        {
            string accessToken = "";

            try
            {
                LoginResponseResult result = Core.Login();

                if (result.AckCode == "SUCCESS")
                {
                    accessToken = result.AccessToken;
                }

                if (Common.SystemSection.IsPrintPacket())
                {
                    FileLog.WriteLog("调用北京市电子证照系统【登录】接口成功！返回结果为：" + JSON.Encode(result));
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【登录】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return accessToken;
        }

        /// <summary>
        ///     4.1.1 登录
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Login(string appKey, string appSecret, string account, string password)
        {
            string accessToken = "";

            try
            {
                LoginResponseResult result = Core.Login(appKey, appSecret, account, password);

                if (result.AckCode == "SUCCESS")
                {
                    accessToken = result.AccessToken;
                }

                if (Common.SystemSection.IsPrintPacket())
                {
                    FileLog.WriteLog("调用北京市电子证照系统【登录】接口成功！返回结果为：" + JSON.Encode(result));
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【登录】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return accessToken;
        }

        /// <summary>
        ///     4.1.2 退出登录
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        public static string LogOut(string accessToken)
        {
            try
            {
                ResponseResult result = Core.LogOut(accessToken);

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【注销】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【注销】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【注销】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return accessToken;
        }

        /// <summary>
        ///     4.4.1 创建制证数据
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CreateResponseResult Create(string accessToken, string itemCode, CreateRequestMdl model)
        {
            CreateResponseResult result;
            try
            {
                result = Core.CreatePDF(accessToken, itemCode, model);

                if (result==null)
                {
                    FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：调用接口CreatePDF返回结果为Null");
                    return null;
                }

                if (result.AckCode == "SUCCESS")
                {
                    //if (Common.SystemSection.IsPrintPacket())
                    //{
                    //    FileLog.WriteLog("调用北京市电子证照系统【制证】接口成功！返回结果为：" + JSON.Encode(result));
                    //}
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog(string.Format("调用北京市电子证照系统【制证】接口失败！错误：{0},传入参数:{1}", JSON.Encode(item), JSON.Encode(model)));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }

        //public static CreateResponseResult Create(string accessToken, string itemCode, CreateRequestGBMdl model)
        //{
        //    CreateResponseResult result;
        //    try
        //    {
        //        result = Core.CreatePDF_EJUse(accessToken, itemCode, model);

        //        if (result == null)
        //        {
        //            FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：调用接口CreatePDF返回结果为Null");
        //            return null;
        //        }

        //        if (result.AckCode == "SUCCESS")
        //        {
        //            //if (Common.SystemSection.IsPrintPacket())
        //            //{
        //            //    FileLog.WriteLog("调用北京市电子证照系统【制证】接口成功！返回结果为：" + JSON.Encode(result));
        //            //}
        //        }
        //        else
        //        {
        //            if (result.Errors != null)
        //            {
        //                foreach (Error item in result.Errors)
        //                {
        //                    FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：" + JSON.Encode(item));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：" + exp.Message, exp);
        //        throw;
        //    }

        //    return result;
        //}

        /// <summary>
        ///  PDF签章制证
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CreateResponseResult CreatePDF(string accessToken, string PostTypeID, CreateRequestMdl model)
        {
            string itemCode = "";//电子证照目录编码
            switch (PostTypeID)
            {
                case "1":
                    itemCode = "100010601000021135110000";
                    break;
                case "2":
                    itemCode = "100004001000021135110000";
                    break;
                case "4":
                    itemCode = "100005501000021135110000";
                    break;
                case "5":
                    itemCode = "100011501000021135110000";
                    break;

            }

            CreateResponseResult result;
            try
            {
                result = Core.CreatePDF(accessToken, itemCode, model);

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【PDF签章制证】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【PDF签章制证】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【PDF签章制证】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }


        /// <summary>
        ///  Ofd(签章制证
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CreateResponseResult CreateOfd(string accessToken, string PostTypeID, CreateRequestGBMdl model)
        {
            string itemCode = "";//电子证照目录编码
            switch (PostTypeID)
            {
                case "0":
                    itemCode = "100088501000021135110000";//二建使用件
                    break;
                case "1":
                    itemCode = "100083201000021135110000";
                                
                    break;
                case "2":
                    itemCode = "100083101000021135110000";
                                
                    break;
            }

            CreateResponseResult result;
            try
            {
                result = Core.CreateOfd(accessToken, itemCode, model);

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【Ofd签章制证】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【Ofd签章制证】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【OfdF签章制证】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }

        /// <summary>
        ///  下载电子签章后的附件
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        public static DownResponseResult DownPDF(string accessToken, string auth_code)
        {
            DownResponseResult result;
            try
            {
                result = Core.DownPDF(accessToken, auth_code);

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 PDF 文件】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 PDF 文件】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 PDF 文件】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }

        /// <summary>
        ///  下载Ofd电子签章后的附件
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        public static DownResponseResult DownOfd(string accessToken, string auth_code)
        {
            DownResponseResult result;
            try
            {
                result = Core.DownOfd(accessToken, auth_code);

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 Ofd 文件】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 Ofd 文件】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【根据电子证照用证码获取电子证照归档 Ofd 文件】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }

        /// <summary>
        ///     签发一张证照
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="licenseCode">电子证照标识码</param>
        /// <param name="idCode">证照号码</param>
        /// <param name="serviceItemCode">事项编码</param>
        /// <param name="serviceItemName">事项名称</param>
        /// <returns></returns>
        public static SignResponseResult Sign(string accessToken, string itemCode, string licenseCode, string idCode, string serviceItemCode, string serviceItemName)
        {
            SignResponseResult result;
            try
            {
                result = Core.Sign(accessToken, itemCode, new SignRequestMdl
                {
                    Data = new SignRequestData
                    {
                        IdCode = idCode,
                        LicenseCode = licenseCode,
                        ServiceItemCode = serviceItemCode,
                        ServiceItemName = serviceItemName,
                        Operator = UserInfoMdl.SetData()
                    }
                });

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【签发】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【签发】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("调用北京市电子证照系统【签发】接口失败！错误：" + exp.Message, exp);
                throw;
            }

            return result;
        }

        /// <summary>
        /// 废止
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="dr">证书数据行</param>
        /// <returns></returns>
        public static AbolishResponseResult Abolish(string accessToken, System.Data.DataRow dr)
        {
            string itemCode="";
            string licenseCode = dr["license_code"].ToString();
            string idCode = dr["CertificateCode"].ToString(); 
            //if(dr["PostTypeID"].ToString()=="1" && dr["SignCATime"] != DBNull.Value && Convert.ToDateTime(dr["SignCATime"]) <Convert.ToDateTime("2023-03-01"))
            //{
            //    idCode = dr["CertificateCode"].ToString().Replace("（", "(").Replace("）", ")");
            //}
            string serviceItemCode = "";
            string serviceItemName = "";

            switch (dr["PostTypeID"].ToString())
            {
                case "1":
                    //目录实施码：100010601000021135110000
                    //事项编码：0100201005110000000000000021135100
                    //事项名称：建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核
                    itemCode = "100010601000021135110000";
                     serviceItemCode = "0100201005110000000000000021135100";
                     serviceItemName = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核"; 
                    break;
                case "2":
                    //目录实施码：100004001000021135110000
                    //事项编码：0100197003110000000000000021135100
                    //事项名称：建筑施工特种作业人员证书变更
                    //印章编码：DZYZ000021135etmOeZ
                    itemCode = "100004001000021135110000";
                    serviceItemCode = "0100197003110000000000000021135100";
                    serviceItemName = "建筑施工特种作业人员证书变更";                   

                    break;
                case "4":
                    //目录实施码：100005501000021135110000
                    //事项编码：0100202012110000000000000021135100
                    //事项名称：住房和城乡建设行业职业技能人员职业培训合格证核发
                    //印章编码：DZYZ000021135VIkmJb

                    itemCode = "100005501000021135110000";
                    serviceItemCode = "0100202012110000000000000021135100";
                    serviceItemName = "住房和城乡建设行业职业技能人员职业培训合格证核发";
                  
                    break;
                case "5":
                    //目录实施码：100011501000021135110000
                    //事项编码：111102345678148H2000132038001
                    //事项名称：住房和城乡建设领域专业人员岗位培训考核合格证书核发证书考核
                    //印章编码：DZYZ000021135VIkmJb
                    itemCode = "100011501000021135110000";
                    serviceItemCode = "111102345678148H2000132038001";
                    serviceItemName = "住房和城乡建设领域专业人员岗位培训考核合格证书核发证书考核";
                         break;
                default:
                    break;
            }
            return Abolish(accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
        }

        /// <summary>
        /// 废止ofd证书
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="dr">证书数据行</param>
        // <param name="usingidCode">是否使用证书编号废止证书</param>
        /// <returns></returns>
        public static AbolishResponseResult AbolishOfd(string accessToken, System.Data.DataRow dr, bool usingidCode)
        {
            string itemCode = "";
            string licenseCode = (usingidCode == true ? "" : dr["Ofd_license_code"].ToString());
            string idCode = dr["CertificateCode"].ToString().Replace("(", "（").Replace(")", "）");
            string serviceItemCode = "";
            string serviceItemName = "";

            switch (dr["PostTypeID"].ToString())
            {
                case "1":
                    //目录实施码：100083201000021135110000
                    //事项编码：0100201005110000000000000021135100
                    //事项名称：建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核
                    itemCode = "100083201000021135110000";
                    serviceItemCode = "0100201005110000000000000021135100";
                    serviceItemName = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核";
                    break;
                case "2":
                    //目录实施码：100083101000021135110000
                    //事项编码：0100197006110000000000000021135100
                    //事项名称：建筑施工特种作业人员证书变更
                    //印章编码：DZYZ000021135etmOeZ
                    itemCode = "100083101000021135110000";
                    serviceItemCode = "0100197006110000000000000021135100";
                    serviceItemName = "建筑施工特种作业人员证书考核";
                    break;
                default:
                    break;
            }
            return Abolish(accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
        }


        /// <summary>
        /// 废止
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="dr">证书数据行</param>
        /// <param name="usingidCode">是否使用证书编号废止证书</param>
        /// <returns></returns>
        public static AbolishResponseResult Abolish(string accessToken, System.Data.DataRow dr, bool usingidCode)
        {
            string itemCode = "";
            string licenseCode = (usingidCode==true?"":dr["license_code"].ToString());
            string idCode = dr["CertificateCode"].ToString();
            //if (dr["PostTypeID"].ToString() == "1" && dr["SignCATime"] != DBNull.Value && Convert.ToDateTime(dr["SignCATime"]) < Convert.ToDateTime("2023-03-01"))
            //{
            //    idCode = dr["CertificateCode"].ToString().Replace("（", "(").Replace("）", ")");
            //}
            string serviceItemCode = "";
            string serviceItemName = "";

            switch (dr["PostTypeID"].ToString())
            {
                case "1":
                    //目录实施码：100010601000021135110000
                    //事项编码：0100201005110000000000000021135100
                    //事项名称：建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核
                    itemCode = "100010601000021135110000";
                    serviceItemCode = "0100201005110000000000000021135100";
                    serviceItemName = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核";
                    break;
                case "2":
                    //目录实施码：100004001000021135110000
                    //事项编码：0100197003110000000000000021135100
                    //事项名称：建筑施工特种作业人员证书变更
                    //印章编码：DZYZ000021135etmOeZ
                    itemCode = "100004001000021135110000";
                    serviceItemCode = "0100197003110000000000000021135100";
                    serviceItemName = "建筑施工特种作业人员证书变更";

                    break;
                case "4":
                    //目录实施码：100005501000021135110000
                    //事项编码：0100202012110000000000000021135100
                    //事项名称：住房和城乡建设行业职业技能人员职业培训合格证核发
                    //印章编码：DZYZ000021135VIkmJb

                    itemCode = "100005501000021135110000";
                    serviceItemCode = "0100202012110000000000000021135100";
                    serviceItemName = "住房和城乡建设行业职业技能人员职业培训合格证核发";

                    break;
                case "5":
                    //目录实施码：100011501000021135110000
                    //事项编码：111102345678148H2000132038001
                    //事项名称：住房和城乡建设领域专业人员岗位培训考核合格证书核发证书考核
                    //印章编码：DZYZ000021135VIkmJb
                    itemCode = "100011501000021135110000";
                    serviceItemCode = "111102345678148H2000132038001";
                    serviceItemName = "住房和城乡建设领域专业人员岗位培训考核合格证书核发证书考核";
                    break;
                default:
                    break;
            }
            return Abolish(accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
        }

        /// <summary>
        /// 废止
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="licenseCode">电子证照标识码</param>
        /// <param name="idCode">证照号码</param>
        /// <param name="serviceItemCode">事项编码</param>
        /// <param name="serviceItemName">事项名称</param>
        /// <returns></returns>
        public static AbolishResponseResult Abolish(string accessToken, string itemCode, string licenseCode, string idCode, string serviceItemCode, string serviceItemName)
        {
            AbolishResponseResult result;
            try
            {
                AbolishRequestMdl model =new AbolishRequestMdl
                {
                    Data = new AbolishRequestData
                    {
                        IdCode = idCode,
                        LicenseCode = licenseCode,
                        ServiceItemCode = serviceItemCode,
                        ServiceItemName = serviceItemName,
                        BizNum = "",
                        Operator = UserInfoMdl.SetData()
                    }
                };

                result = Core.Abolish(accessToken, itemCode, model);

                if(result == null)
                {
                    FileLog.WriteLog(string.Format("调用北京市电子证照系统【废止】接口失败！返还值为空 result=null，参数：{0}" + JSON.Encode(model)));
                }

                if (result.AckCode == "SUCCESS")
                {
                    if (Common.SystemSection.IsPrintPacket())
                    {
                        FileLog.WriteLog("调用北京市电子证照系统【废止】接口成功！返回结果为：" + JSON.Encode(result));
                    }
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (Error item in result.Errors)
                        {
                            FileLog.WriteLog("调用北京市电子证照系统【废止】接口失败！错误：" + JSON.Encode(item));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog(string.Format("调用北京市电子证照系统【废止】接口失败！错误：{0}，传参：itemCode={1},licenseCode={2},idCode={3},serviceItemCode={4},serviceItemName={5}", exp.Message, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName), exp);
                throw;
            }

            return result;
        }

        ///// <summary>
        ///// 废止Ofd
        ///// </summary>
        ///// <param name="accessToken">访问令牌</param>
        ///// <param name="itemCode">电子证照目录编码</param>
        ///// <param name="licenseCode">电子证照标识码</param>
        ///// <param name="idCode">证照号码</param>
        ///// <param name="serviceItemCode">事项编码</param>
        ///// <param name="serviceItemName">事项名称</param>
        ///// <returns></returns>
        //public static AbolishResponseResult AbolishOfd(string accessToken, string itemCode, string licenseCode, string idCode, string serviceItemCode, string serviceItemName)
        //{
        //    AbolishResponseResult result;
        //    try
        //    {
        //        result = Core.AbolishOfd(accessToken, itemCode, new AbolishRequestMdl
        //        {
        //            Data = new AbolishRequestData
        //            {
        //                IdCode = idCode,
        //                LicenseCode = licenseCode,
        //                ServiceItemCode = serviceItemCode,
        //                ServiceItemName = serviceItemName,
        //                BizNum = "",
        //                Operator = UserInfoMdl.SetData()
        //            }
        //        });

        //        if (result.AckCode == "SUCCESS")
        //        {
        //            if (Common.SystemSection.IsPrintPacket())
        //            {
        //                FileLog.WriteLog("调用北京市电子证照系统【废止】接口成功！返回结果为：" + JSON.Encode(result));
        //            }
        //        }
        //        else
        //        {
        //            if (result.Errors != null)
        //            {
        //                foreach (Error item in result.Errors)
        //                {
        //                    FileLog.WriteLog("调用北京市电子证照系统【废止】接口失败！错误：" + JSON.Encode(item));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        FileLog.WriteLog("调用北京市电子证照系统【废止】接口失败！错误：" + exp.Message, exp);
        //        throw;
        //    }

        //    return result;
        //}
    }
}