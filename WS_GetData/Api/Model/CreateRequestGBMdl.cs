using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    /// 创建新版国标制证数据请求参数
    /// </summary>
    [Serializable]
    public class CreateRequestGBMdl
    {
        /// <summary>
        ///     证照数据内容
        /// </summary>
        [JsonProperty("data")]
        public CreateRequestDataGB Data { get; set; }
    }

    /// <summary>
    ///     证照数据内容
    /// </summary>
    [Serializable]
    public class CreateRequestDataGB : BaseRequestMdl
    {
        /// <summary>
        ///     照面模版组别名称
        /// </summary>
        [JsonProperty("license_group")]
        public string LicenseGroup { set; get; }

        /// <summary>
        ///     印章编码
        /// </summary>
        [JsonProperty("seal_code")]
        public string SealCode { set; get; }

        /// <summary>
        ///   印章序号
        /// </summary>
        [JsonProperty("seal_num")]
        public string SealNum { set; get; }

        /// <summary>
        /// 证照数据内容：json
        /// </summary>
        [JsonProperty("data_fields")]
        public object DataFields { private set; get; }

        /// <summary>
        /// 电子附件（PDF）
        /// </summary>
        [JsonProperty("attachments")]
        public List<CreateattachmentDataGB> Attachments { set; get; }
        //public Newtonsoft.Json.Linq.JArray Attachments { set; get; }

        /// <summary>
        /// 制证操作人信息：json
        /// </summary>
        [JsonProperty("operator")]
        public UserInfoMdl Operator { get; set; }

        /// <summary>
        /// 签章状态
        /// </summary>
        [JsonProperty("sign_attach")]
        public string SignAttach { set; get; }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void SetData<T>(T t) where T : class
        {
            DataFields = t;
        }

        /// <summary>
        /// 从业人员电子证照Ofd初始化
        /// </summary>
        /// <param name="dr"></param>
        public void IniPropoertyOfd(System.Data.DataRow dr)
        {
            string fileFrom = string.Format(@"D:\\zzk\EXAM_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]);

            this.Operator = UserInfoMdl.SetData();
            this.SignAttach = "true";
            this.LicenseGroup = "";
            string ZZMC = "";//证照名称

            switch (dr["PostTypeID"].ToString())
            {
                case "1":
                    //目录实施码：100010601000021135110000
                    //事项编码：0100201005110000000000000021135100
                    //事项名称：建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核
                    //印章编码seal_code：DZYZ000021135etmOeZ
                    //seal_num：MTIzMTEwMDAwMDAwMTg2OTYzNDM=	

                    this.SealCode = "DZYZ000021135etmOeZ";
                    this.SealNum = "MTIzMTEwMDAwMDAwMTg2OTYzNDM=";
                    this.ServiceItemCode = "0100201005110000000000000021135100";
                    this.ServiceItemName = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产证书考核";
                    ZZMC = "建筑施工企业安全生产管理人员考核合格证书（副本）";//证照名称  

                    //数据内容
                    this.SetData(new WS_GetData.Api.Model.Zm.EXAM_AQSC_GB
                    {
                        ZZMC = ZZMC,
                        ZZHM = dr["CERTIFICATECODE"].ToString().Replace("(", "（").Replace(")", "）"),
                        CYRMC = dr["WORKERNAME"].ToString(),
                        CYRSFZJLX = (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 ? "10" : "40"),
                        CYRSFZJHM = dr["WORKERCERTIFICATECODE"].ToString(),
                        FZJGMC = "北京市住房和城乡建设委员会",
                        FZJGZZJGDM = "12110000400777687U",
                        FZJGSSXZQHDM = "110000",
                        FZRQ = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                        YXQJSRQ = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
                        ZZLXDM = "11100000000013338W011",
                        ZZBH = dr["CERTIFICATECODE"].ToString().Replace("(", "（").Replace(")", "）"),
                        FZJG = "北京市住房和城乡建设委员会",
                        FZJGDM = "12110000400777687U",
                        CCLZRQ = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
                        YXQQSRQ = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                        AGRYLB = GetSLRPostCode(dr["POSTNAME"].ToString()),
                        XM = dr["WORKERNAME"].ToString(),
                        XB = dr["SEX"].ToString(),
                        XBDM = (dr["SEX"].ToString() == "女" ? "2" : "1"),
                        CSNY = Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
                        SFZJHM = dr["WORKERCERTIFICATECODE"].ToString(),
                        SFZJHMLX = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "其他自然人有效证件代码" : "公民身份号码"),
                        SFZJHMLXDM = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "999" : "111"),
                        DWMC = dr["UNITNAME"].ToString(),                       
                        TYSHXYDM = dr["CreditCode"].ToString(),
                        ZW = DataAccess.CertificateDAL.GetAppointmentCode(dr),
                        JSZC = DataAccess.CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"].ToString()),
                        WHCD = DataAccess.CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"].ToString()),
                        ZY = "",
                        ZP = "",
                        ZSZT = DataAccess.CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(),Convert.ToDateTime(dr["VALIDENDDATE"])),
                        ZSZTDM = DataAccess.CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),
                        ZSZTMS = "",
                        GLLX = "",
                        GLZZBZ = "",
                        YWLXDM = "",
                        YWXX = ""
                    });

                    //附件pdf
                    Attachments = new List<CreateattachmentDataGB>
                    {
                        new CreateattachmentDataGB
                        {
                            Name = dr["CertificateCAID"].ToString(),
                            FileType = "pdf",
                            FileData = Common.SystemSection.ReadSealDoc(dr["CertificateID"].ToString(),fileFrom),
                            Description = "已盖章证照",
                            IsLicenseImage = true,
                            IsShowTemplate = false,
                            Page="0",
                            X="128",
                            Y="210"
                        }
                    };
                            break;
                case "2":
                    //目录实施码：100083101000021135110000
                    //事项编码：0100197006110000000000000021135100
                    //事项名称：建筑施工特种作业人员证书考核
                    //印章编码seal_code：DZYZ000021135etmOeZ		
                    //seal_num：MTIzMTEwMDAwMDAwMTg2OTYzNDM=		

                    this.SealCode = "DZYZ000021135etmOeZ";
                    this.SealNum = "MTIzMTEwMDAwMDAwMTg2OTYzNDM=";
                    this.ServiceItemCode = "0100197006110000000000000021135100";
                    this.ServiceItemName = "建筑施工特种作业人员证书考核";
                    ZZMC = "建筑施工特种作业操作资格证书（副本）";//证照名称   

                    //数据内容
                    this.SetData(new WS_GetData.Api.Model.Zm.EXAM_TZZY_GB
                    {
                        ZZMC = ZZMC,
                        ZZHM = dr["CERTIFICATECODE"].ToString(),
                        CYRMC = dr["WORKERNAME"].ToString(),
                        CYRSFZJLX = (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 ? "10" : "40"),
                        CYRSFZJHM = dr["WORKERCERTIFICATECODE"].ToString(),
                        FZJGMC = dr["CONFERUNIT"].ToString(),
                        FZJGZZJGDM = "12110000400777687U",
                        FZJGSSXZQHDM = "110000",
                        FZRQ = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                        YXQJSRQ = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),

                        ZZLXDM = "11100000000013338W032",
                        ZZBH = dr["CERTIFICATECODE"].ToString(),
                        FZJG = "北京市住房和城乡建设委员会",
                        FZJGDM = "12110000400777687U",
                        CCLZRQ = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
                        YXQQSRQ = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),                      
                        XM = dr["WORKERNAME"].ToString(),
                        XB = dr["SEX"].ToString(),
                        XBDM = (dr["SEX"].ToString() == "女" ? "2" : "1"),                      
                        SFZJHM = dr["WORKERCERTIFICATECODE"].ToString(),
                        SFZJHMLX = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "其他自然人有效证件代码" : "公民身份号码"),
                        SFZJHMLXDM = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "999" : "111"),
                        ZP = "",
                        CZLB=DataAccess.CertificateDAL.GetTZZYPostName(dr["POSTNAME"].ToString()),
                        CZLBDM=DataAccess.CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                        QTCZLBSM = (DataAccess.CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()) == "99" ? dr["POSTNAME"].ToString() : ""),
                        ZSZT = DataAccess.CertificateDAL.Get_GB_Tzzy_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),
                        ZSZTDM = DataAccess.CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),
                        ZSZTMS = "",
                        GLLX = "",
                        YWLXDM = "",
                        YWXX = ""
                        
                    });

                    //附件pdf
                    Attachments = new List<CreateattachmentDataGB>
                    {
                        new CreateattachmentDataGB
                        {
                            Name = dr["CertificateCAID"].ToString(),
                            FileType = "pdf",
                            FileData = Common.SystemSection.ReadSealDoc(dr["CertificateID"].ToString(),fileFrom),
                            Description = "已盖章证照",
                            IsLicenseImage = true,
                            IsShowTemplate = false,
                            Page="0",
                            X="128",
                            Y="200"
                        }
                    };
                    break;
            }
        }

        /// <summary>
        /// 获取三类人员工种类别编码（char(2)）
        /// </summary>
        /// <param name="PostName">人员系统中工种名称</param>
        /// <returns>工种类别编码</returns>
        private string GetSLRPostCode(string PostName)
        {
            switch (PostName)
            {
                case "企业主要负责人":
                    return "A";
                case "项目负责人":
                    return "B";
                case "机械类专职安全生产管理人员":
                    return "C1";
                case "土建类专职安全生产管理人员":
                    return "C2";
                case "综合类专职安全生产管理人员":
                    return "C3";
                default:
                    return "";
            }
        }


        ///// <summary>
        ///// 获取证书证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
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
        //        index = Char36.IndexOf(c.ToString());
        //        p = p + index;
        //        p = (p % 36);
        //        if (p == 0) p = 36;
        //        p = p * 2;
        //        p = p % 37;
        //    }
        //    p = 37 - p;
        //    return Char36[p];
        //}

        /// <summary>
        /// 二级注册建造师电子证书初始化
        /// </summary>
        /// <param name="dr"></param>
        public void IniPropoertyEJ_OfdUse(System.Data.DataRow dr)
        {
            string fileFrom = string.Format(@"D:\\zzk\ERJIAN_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]);

            this.Operator = UserInfoMdl.SetData();
            this.SignAttach = "true";
            this.LicenseGroup = "";
            string ZZMC = "中华人民共和国二级建造师注册证书（附件使用件）";//证照名称

            //中华人民共和国二级建造师注册证书（附件使用件）	
            //目录实施码：100088501000021135110000
            //事项名称：二级建造师注册证书
            //事项编码：0100909004110X330002140X100
            //印章编码：DZYZ000021135XXnQXQ
            this.SealCode = "DZYZ000021135XXnQXQ";
            this.SealNum =  "MTIzMTEwMDAwMDAwMTg2OTYzNDM=";
            this.ServiceItemCode = "0100909004110X330002140X100";
            this.ServiceItemName = "二级建造师注册证书";

            string tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
                                                "1.2.156.3005.2",//电子证照根代码
                                                "11100000000013338W001",//证照类型代码
                                                "11110000000021135M",//证照颁发机构代码
                                                dr["PSN_ServerID"].ToString().Replace("-", "").ToUpper(),//流水号
                                                (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000")//版本号
                                                );
            //数据内容
            this.SetData(new WS_GetData.Api.Model.Zm.EJJZS
            {
                ZZMC = ZZMC,
                ZZHM = dr["PSN_RegisterNO"].ToString(),
                CYRMC = dr["PSN_Name"].ToString(),
                CYRSFZJLX = (dr["PSN_CertificateNO"].ToString().Length == 18 ? "10" : "40"),
                CYRSFZJHM = dr["PSN_CertificateNO"].ToString(),
                FZJGMC = "北京市住房和城乡建设委员会",
                FZJGZZJGDM = "12110000400777687U",
                FZJGSSXZQHDM = "110000",
                FZRQ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
                YXQJSRQ = Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy-MM-dd"),

                ZZLXDM = "11100000000013338W001",
                ZZBH = dr["PSN_RegisterNO"].ToString(),
                //电子证照根代码、证照类型代码、证照颁发机构代码、流水号、版本号和校验位，各部分之间用点分隔符＂．“分隔
                //其中电子证照根代码固定为“1.2.156.3005.2”
                //证照类型代码固定为“11100000000013338W001”
                //证照颁发机构代码：11110000000021135M
                //流水号(唯一，取PSN_ServerID，去掉其中的“-”)
                //版本号:电子文件版本的标识号， 3 位阿拉伯数字。因年检、续期等业务操作而形成新的电子证照文件时，如证照编号和流水号均不变，则应产生一个新的递增版本号加以区分。
                //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
                ZZBS = string.Format("{0}.{1}", tmp_zzbs, Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),//校验位

                CZZT = dr["PSN_Name"].ToString(),// 姓名”
                CZZTDM = dr["PSN_CertificateNO"].ToString(),//身份证件号码
                CZZTDMLX = (dr["PSN_CertificateNO"].ToString().Length == 18 ? "10" : "40"),//身份证件号码类型
                ZZYXQQSSJ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
                ZZYXQJZSJ = Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy-MM-dd"),
                ZZBFJG = "北京市住房和城乡建设委员会",
                ZZBFJGDM = "12110000400777687U",
                ZZBFRQ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
                KZ_certClass = "二级",
                KZ_empEnp = dr["ENT_Name"].ToString(),
                KZ_empEnpCode = dr["ENT_OrganizationsCode"].ToString(),
                KZ_specSub = dr["ProfessionWithValid"].ToString(),
                KZ_specSubCode = GetZY_Code(dr["ProfessionWithValid"].ToString()),
                KZ_quaCertNum = dr["ZGZSBH"].ToString(),
                KZ_subBeginDate = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
                KZ_subEndDate = dr["ProfessionWithValid"].ToString()
            });

            //附件pdf
            Attachments = new List<CreateattachmentDataGB>
            {
                new CreateattachmentDataGB
                {
                     Name = dr["CertificateCAID"].ToString(),
                    FileType = "pdf",
                    FileData = Common.SystemSection.ReadSealDoc(dr["CertificateCAID"].ToString(),fileFrom),
                    Description = "已盖章证照",
                    IsLicenseImage = true,
                    IsShowTemplate = false,
                    Page="0",
                    X="138",
                    Y="218"
                    //X="128",
                    //Y="210"
                }
            };
        }

        //public void IniPropoertyEJ_PDFUse(System.Data.DataRow dr)
        //{
        //    string fileFrom = string.Format(@"D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf", dr["CertificateCAID"]);

        //    this.Operator = UserInfoMdl.SetData();
        //    this.SignAttach = "true";
        //    this.LicenseGroup = "";
        //    string ZZMC = "中华人民共和国二级建造师注册证书（附件使用件）";//证照名称

        //    //中华人民共和国二级建造师注册证书（附件使用件）	
        //    //目录实施码：100088501000021135110000
        //    //事项名称：二级建造师注册证书
        //    //事项编码：0100909004110X330002140X100
        //    //印章编码：DZYZ000021135XXnQXQ
        //    this.SealCode = "DZYZ000021135XXnQXQ";
        //    this.SealNum = "MTIzMTEwMDAwMDAwMTg2OTYzNDM=";
        //    this.ServiceItemCode = "0100909004110X330002140X100";
        //    this.ServiceItemName = "二级建造师注册证书";

        //    string tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
        //                                        "1.2.156.3005.2",//电子证照根代码
        //                                        "11100000000013338W001",//证照类型代码
        //                                        "11110000000021135M",//证照颁发机构代码
        //                                        dr["PSN_ServerID"].ToString().Replace("-", "").ToUpper(),//流水号
        //                                        (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000")//版本号
        //                                        );
        //    //数据内容
        //    this.SetData(new WS_GetData.Api.Model.Zm.EJJZS
        //    {
        //        ZZMC = ZZMC,
        //        ZZHM = dr["PSN_RegisterNO"].ToString(),
        //        CYRMC = dr["PSN_Name"].ToString(),
        //        CYRSFZJLX = (dr["PSN_CertificateNO"].ToString().Length == 18 ? "10" : "40"),
        //        CYRSFZJHM = dr["PSN_CertificateNO"].ToString(),
        //        FZJGMC = "北京市住房和城乡建设委员会",
        //        FZJGZZJGDM = "12110000400777687U",
        //        FZJGSSXZQHDM = "110000",
        //        FZRQ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
        //        YXQJSRQ = Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy-MM-dd"),

        //        ZZLXDM = "11100000000013338W001",
        //        ZZBH = dr["PSN_RegisterNO"].ToString(),
        //        //电子证照根代码、证照类型代码、证照颁发机构代码、流水号、版本号和校验位，各部分之间用点分隔符＂．“分隔
        //        //其中电子证照根代码固定为“1.2.156.3005.2”
        //        //证照类型代码固定为“11100000000013338W001”
        //        //证照颁发机构代码：11110000000021135M
        //        //流水号(唯一，取PSN_ServerID，去掉其中的“-”)
        //        //版本号:电子文件版本的标识号， 3 位阿拉伯数字。因年检、续期等业务操作而形成新的电子证照文件时，如证照编号和流水号均不变，则应产生一个新的递增版本号加以区分。
        //        //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        //        ZZBS = string.Format("{0}.{1}", tmp_zzbs, GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),//校验位

        //        CZZT = dr["PSN_Name"].ToString(),// 姓名”
        //        CZZTDM = dr["PSN_CertificateNO"].ToString(),//身份证件号码
        //        CZZTDMLX = (dr["PSN_CertificateNO"].ToString().Length == 18 ? "10" : "40"),//身份证件号码类型
        //        ZZYXQQSSJ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
        //        ZZYXQJZSJ = Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy-MM-dd"),
        //        ZZBFJG = "北京市住房和城乡建设委员会",
        //        ZZBFJGDM = "12110000400777687U",
        //        ZZBFRQ = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
        //        KZ_certClass = "二级",
        //        KZ_empEnp = dr["ENT_Name"].ToString(),
        //        KZ_empEnpCode = dr["ENT_OrganizationsCode"].ToString(),
        //        KZ_specSub = dr["ProfessionWithValid"].ToString(),
        //        KZ_specSubCode = GetZY_Code(dr["ProfessionWithValid"].ToString()),
        //        KZ_quaCertNum = dr["ZGZSBH"].ToString(),
        //        KZ_subBeginDate = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy-MM-dd"),
        //        KZ_subEndDate = dr["ProfessionWithValid"].ToString()
        //    });

        //    //附件pdf
        //    Attachments = new List<CreateattachmentDataGB>
        //    {
        //        new CreateattachmentDataGB
        //        {
        //            Name = dr["CertificateCAID"].ToString(),
        //            FileType = "pdf",
        //            FileData = Common.SystemSection.ReadSealDoc(dr["CertificateCAID"].ToString(),fileFrom),
        //            Description = "已盖章证照",
        //            IsLicenseImage = true,
        //            IsShowTemplate = false,
        //            Page="0",
        //            X="128",
        //            Y="210"
        //        }
        //    };
        //}

        /// <summary>
        /// 获取专业代码集合
        /// </summary>
        /// <param name="ZYList">专业集合</param>
        /// <returns>专业代码集合</returns>
        private string GetZY_Code(string ZYList)
        {
            //10401001 建筑工程
            //10401002 公路工程
            //10401005 水利水电工程
            //10401007 矿业工程
            //10401010 市政公用工程
            //10401012 机电工程

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (ZYList.Contains("建筑")) sb.Append(",10401001");
            if (ZYList.Contains("公路")) sb.Append(",10401002");
            if (ZYList.Contains("水利")) sb.Append(",10401005");
            if (ZYList.Contains("矿业")) sb.Append(",10401007");
            if (ZYList.Contains("市政")) sb.Append(",10401010");
            if (ZYList.Contains("机电")) sb.Append(",10401012");

            if (ZYList.Contains("安装工程")) sb.Append("");//?????????????????????????????????????????
            if (ZYList.Contains("土木建筑工程")) sb.Append("");//???????????????????????????

            if (sb.Length > 0) sb.Remove(0, 1);

            return sb.ToString();
        }
    }

    /// <summary>
    /// </summary>
    [Serializable]
    public class CreateattachmentDataGB
    {
        /// <summary>
        ///  附件名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///  附件类型（文件后缀）
        /// </summary>
        [JsonProperty("file_type")]
        public string FileType { get; set; }

        /// <summary>
        ///  附件数据（base64 转码）
        /// </summary>
        [JsonProperty("file_data")]
        public string FileData { get; set; }

        /// <summary>
        ///  是否作为证照电子影像（默认否）
        /// </summary>
        [JsonProperty("is_license_image")]
        public bool IsLicenseImage { get; set; }

        /// <summary>
        ///  是否保留通用模板（默认是）
        /// </summary>
        [JsonProperty("is_show_template")]
        public bool IsShowTemplate { get; set; }

        /// <summary>
        ///  附件描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///  签章页数，从0开始
        /// </summary>
        [JsonProperty("page")]
        public string Page { get; set; }

        /// <summary>
        ///  x轴坐标
        /// </summary>
        [JsonProperty("x")]
        public string X { get; set; }

        /// <summary>
        ///  y轴坐标
        /// </summary>
        [JsonProperty("y")]
        public string Y { get; set; }
    }
}