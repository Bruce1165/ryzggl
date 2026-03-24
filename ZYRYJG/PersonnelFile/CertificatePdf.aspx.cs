using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.PersonnelFile
{
    public partial class CertificatePdf : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("非法访问！"), false);
                    return;
                }
                if (Request.QueryString["t"] == "-99")//一级建造师、一级监理师、一级造价工程师师不在本系统提供电子证书
                {
                    DivDetail.InnerText = "所选证书类型不在本系统提供电子证书（只提供数据查看），请您转到相应发证系统平台下载！";
                    return;
                }
                string CID = Utility.Cryptography.Decrypt(Request["c"]);
                if (Request.QueryString["t"] == "0")//二建
                {
                    Response.Redirect(string.Format("EJuse.aspx?c={0}", Request["c"]), true);
                    return;
                    #region 二建

                    COC_TOW_Person_BaseInfoMDL ob = COC_TOW_Person_BaseInfoDAL.GetObject(CID);
                    if (string.IsNullOrEmpty(ob.CertificateCAID) == true)
                    {
                        DivDetail.InnerHtml = "<p>该证书尚无电子证书，可能原因如下：<br/>1、证书信息不完整（身份证验证不通过、缺电子照片、单位名称、机构代码等）,请补全信息后再下载。</p>";
                        return;
                    }
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = ob;

                    string pdfUrl = string.Format("{2}/{0}/{1}.pdf", ob.CertificateCAID.Substring(ob.CertificateCAID.Length - 3, 3), ob.CertificateCAID, MyWebConfig.CAFile);

                    if (RootUrl.Contains("120.52.185.14") == true && File.Exists(pdfUrl) == false)
                    {
                        if (ob.PSN_CertificateNO.Length == 18 && Utility.Check.isChinaIDCard(ob.PSN_CertificateNO) == false)
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！(请检查证书信息中个人证件号码是否正确)";
                        }
                        else
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！";
                        }
                        return;
                    }
                    ButtonDownload.Text = string.Format("{0}.pdf", ob.PSN_RegisterNO);
                    ButtonDownload.Visible = true;

                    //                    UIHelp.layerAlertWithHtml(Page, @"1、二级注册建造师打印电子证书后，应在个人签名处手写本人签名并签署签名日期，未手写签名的，该电子证书无效。
                    //
                    //2、未上传手写签名照的，请先在左侧“我的信息”>>“个人信息维护”中补充完整。");

                    UIHelp.layerAlertWithHtml(Page, @"二级注册建造师打印电子证书后，应在个人签名处手写本人签名并签署签名日期，未手写签名的，该电子证书无效。");

                    #endregion 二建
                }
                else if (Request.QueryString["t"] == "-1")//二级造价工程师
                {
                    #region 二造

                    zjs_CertificateMDL ob = zjs_CertificateDAL.GetObject(CID);
                    if (string.IsNullOrEmpty(ob.CertificateCAID) == true)
                    {
                        DivDetail.InnerHtml = "<p>该证书尚无电子证书，可能原因如下：<br/>1、证书信息不完整（身份证验证不通过、缺电子照片、单位名称、机构代码等）,请补全信息后再下载。</p>";
                        return;
                    }
                    ViewState["zjs_CertificateMDL"] = ob;

                    string pdfUrl = string.Format("{2}/{0}/{1}.pdf", ob.CertificateCAID.Substring(ob.CertificateCAID.Length - 3, 3), ob.CertificateCAID, MyWebConfig.CAFile);
                    if (RootUrl.Contains("120.52.185.14") == true && File.Exists(pdfUrl) == false)
                    {
                        if (ob.PSN_CertificateNO.Length == 18 && Utility.Check.isChinaIDCard(ob.PSN_CertificateNO) == false)
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！(请检查证书信息中个人证件号码是否正确)";
                        }
                        else
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！";
                        }
                        return;
                    }
                    ButtonDownload.Text = string.Format("{0}.pdf", ob.PSN_RegisterNO);
                    ButtonDownload.Visible = true;

                    //                    UIHelp.layerAlertWithHtml(Page, @"1、二级注册建造师打印电子证书后，应在个人签名处手写本人签名并签署签名日期，未手写签名的，该电子证书无效。
                    //
                    //2、未上传手写签名照的，请先在左侧“我的信息”>>“个人信息维护”中补充完整。");

                    UIHelp.layerAlertWithHtml(Page, @"二级注册造价工程师打印电子证书后，应在个人签名处手写本人签名并签署签名日期，未手写签名的，该电子证书无效。");

                    #endregion 二造
                }
                else
                {
                    #region 从业人员

                    CertificateOB ob = CertificateDAL.GetObject(Convert.ToInt64(CID));
                    ViewState["CertificateOB"] = ob;
                    if (ob.PostID == 6 || ob.PostID == 1123)
                    {
                        int count_C = CommonDAL.GetRowCount("certificate", string.Format(" and WORKERCERTIFICATECODE='{0}' and (postid = 6 or postid =1123) and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'", ob.WorkerCertificateCode));
                        if (count_C > 1)
                        {
                            DivDetail.InnerHtml = "<p>系统检测到你同时持有C1、C2证书，请您先发起证书合并业务后再下载电子证书。</p>";
                            return;
                        }
                    }

                    if (ob.PostTypeID == 1 || ob.PostTypeID == 2)
                    {
                        #region 证书全国查重校验
                                               
                        if (ob.EleCertErrStep == EnumManager.EleCertDoStep.GetCode
                            && (ob.QRCodeTime.HasValue == false || ob.QRCodeTime < ob.EleCertErrTime)
                            )
                        {
                            if (IfExistRoleID("0") && ob.CheckDate > DateTime.Now.AddDays(-2))//个人业务决定后3天才显示校验错误信息，否则提示等待
                            {
                                DivDetail.InnerHtml = "<div>证书业务决定通过后，住建部正在进行数据校验核对，生成电子证书需要1-3个工作日，请您耐心等待。</div>";
                            }
                            else
                            {
                                DivDetail.InnerHtml = string.Format(@"<div>该证书没有通过<a href='https://zlaq.mohurd.gov.cn/fwmh/bjxcjgl/fwmh/pages/default/index.html' target='_blank'>【全国工程质量安全监管信息平台（可按身份证查询ABC持证情况）】</a>数据校验，属于违规持证。<br/>
                                                                    请先办理相关证书变更、注销或转出1~2天后，申请重新校验，才能下载新版电子证照。（若外省证书已经转出或注销，请联系原证书省份，查询数据是否已经同步到全国工程质量安全监管信息平台。）<br/><br/>
                                                                    <b>最后校验时间：</b>{2}<br/><br/>
                                                                    <b>校验结果说明：</b><span style='color:red'>{0}{1}</span><br/><br/>
                                                                    <b>持证规则说明：</b>（请参考持证规则及校验结果说明整改）<br/>
                                                                    <div style='padding-left: 32px'>
                                                                        <b>A证持证要求：</b><br/> 
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人有多本A证时，多本A证在不同企业下，其中最多存在一本非法人A证，其余A证只能以法人A证的形式存在；<br/>
                                                                            > 持证人同时持有A、B或C证时，要求A（如果有多本，保证其中一本）、B、C证必须在同一个企业；<br/>
                                                                            > 一个企业只能存在一本法人A证。（法人变更需要变更证书上职务）<br/><br/>
                                                                        </div>
                                                                        <b>B证持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 持证人在全国范围只允许持有一本B证；<br/>
                                                                            > 持有已注册的建造师且建造师注册单位与B本单位一致；<a href='https://jzsc.mohurd.gov.cn/data/person' target='_blank'>【全国建筑市场监管公共服务平台（可按身份证查询建造师持证情况）】</a><br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C1/C2持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证号码一致），在全国范围内只允许持有一本C1或C2证，同时持有C1和C2证的，应办理C1、C2证合并C3证业务；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下。<br/><br/>
                                                                        </div>
                                                                        <b>C3持证要求：</b><br/>
                                                                        <div style='padding-left: 32px'>
                                                                            > 同一人员（身份证号码一致），在全国范围内只允许持有一本C3证，不能同时持有C1或C2证；<br/>
                                                                            > 同时存在B/C证时，B/C证都必须在同一企业下；<br/>                                                                     
                                                                        </div>
                                                                    </div>
                                </div>", ob.EleCertErrDesc
                                            , ob.EleCertErrDesc.Contains("已存在其他法人A证") == false ? "" : "<br/>请对该证书进行变更。如仍在该企业任职，请办理“证书变更”中的职务变更，根据实际情况选择总经理、技术负责人等非法定代表人职务；<br/>如已不在该企业任职，请通知持证人及时将证书变更至新的工作单位。"
                                            , (ob.EleCertErrTime.HasValue ? ob.EleCertErrTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "")
                                           );

                                if (IfExistRoleID("0"))
                                {
                                    if (ob.EleCertErrTime.Value.AddDays(1) < DateTime.Now)//个人日可以重新发起一次校验
                                    {
                                        ButtonReCheck.Visible = true;
                                    }
                                }
                                else if (IfExistRoleID("1") || IfExistRoleID("16"))
                                {
                                    ButtonReCheck.Visible = true;//管理员不限次数
                                }
                            }
                            return;
                        }

                        #endregion 证书全国查重校验
                    }

                    if (string.IsNullOrEmpty(ob.CertificateCAID) == true)
                    {
                        if (ob.PostTypeID == 5)
                        {
                            DivDetail.InnerHtml = "<p>此岗位证书信息不完整或格式错误（身份证验证不通过、缺电子照片、单位名称、组织机构代码等），请持证人员于2021年6月30日前,携带身份证、纸质证书原件或电子证书，到北京市政务服务中心一层C岛综合窗口更正证书身份信息或补登记,逾期不再办理。</p>";

                        }
                        else
                        {
                            DivDetail.InnerHtml = "<p>该证书尚无电子证书，可能原因如下：<br/>1、此岗位证书尚未推行电子证书；<br/>2、此岗位证书信息不完整或格式错误（身份证验证不通过、缺电子照片、单位名称、组织机构代码等）,请提交变更申请补全信息后再下载。</p>";
                        }
                        return;
                    }
                    if (ob.ReturnCATime < ob.ModifyTime)
                    {
                        DivDetail.InnerHtml = "<p>电子证书换发排队等待中，请耐心等待。若长期无法生成请请拨打平台首页技术咨询电话。</p>";
                        return;
                    }

                    

                    string pdfUrl = string.Format("{2}/{0}/{1}.pdf", ob.CertificateCAID.Substring(ob.CertificateCAID.Length - 3, 3), ob.CertificateCAID, MyWebConfig.CAFile);
                    string ofdUrl = string.Format("{2}/{0}/{1}.ofd", ob.CertificateCAID.Substring(ob.CertificateCAID.Length - 3, 3), ob.CertificateCAID, MyWebConfig.CAFile);
                    if (ob.PostTypeID == 1 || ob.PostTypeID == 2)
                    {
                        if (ValidResourceIDLimit(RoleIDs, "CertificateLock") == true)//有证书锁定与解锁权限，允许暂扣
                        {
                            ButtonPause.Visible = true;
                        }
                        CertificatePauseMDL pauseOB = CertificatePauseDAL.GetLastObject(ob.PostTypeID.Value, ob.CertificateCode);
                        if (pauseOB == null)
                        {
                            ButtonPause.Text = "申请暂扣";
                        }
                        else
                        {
                            switch (pauseOB.PauseStatusCode)
                            {
                                case 1:
                                    ButtonPause.Text = "申请暂扣";
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    ButtonPause.Text = "申请解除暂扣";
                                    break;
                            }
                            if (pauseOB.PauseStatusCode == 2 || pauseOB.PauseStatusCode == 3)
                            {
                                DivDetail.InnerHtml = "<p>证书已被市住建委暂扣，无法下载电子证书。</p>";
                                return;
                            }
                        }

                        if (ob.ReturnCATime.HasValue == true && ob.QRCodeTime.HasValue == true && ob.ReturnCATime > ob.QRCodeTime)
                        {
                            ButtonDownload.Text = string.Format("{0}.pdf", ob.CertificateCode);
                            ButtonDownload.Visible = true;

                            if (ob.Ofd_ReturnCATime.HasValue == true && ob.QRCodeTime.HasValue == true && ob.Ofd_ReturnCATime > ob.QRCodeTime)
                            {
                                ButtonDownload_OFD.Text = string.Format("{0}.ofd", ob.CertificateCode);
                                ButtonDownload_OFD.Visible = true;
                            }
                        }
                        else
                        {
                            DivDetail.InnerHtml = "<p>该证书尚无电子证书或排队生成中。若长期无法生成请请拨打平台首页技术咨询电话。</p>";
                            return;
                        }
                    }
                    else
                    {
                        if (ob.ReturnCATime.HasValue == true && ob.ApplyCATime.HasValue == true && ob.ReturnCATime > ob.ApplyCATime)
                        {
                            ButtonDownload.Text = string.Format("{0}.pdf", ob.CertificateCode);
                            ButtonDownload.Visible = true;
                        }
                        else if (ob.WorkerCertificateCode.Length == 18 && Utility.Check.isChinaIDCard(ob.WorkerCertificateCode) == false)
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！(请检查证书信息中个人证件号码是否正确)";
                            return;
                        }
                        else
                        {
                            DivDetail.InnerText = "该证书尚无电子证书！";
                            return;
                        }
                    }
                    #endregion 从业人员
                }

                BindDownLog();
                DivDetail.InnerHtml = "已经为您准备好了数据，请点击链接进行下载您需要的格式。";
                ShowJianDuCard();
                //DivDetail.InnerHtml = string.Format("已经为您准备好了数据，请点击链接进行下载。", ob.CertificateCAID.Substring(ob.CertificateCAID.Length - 3, 3), ob.CertificateCAID);
            }
        }

        protected void ButtonDownload_Click(object sender, EventArgs e)
        {
            string CAID = "";
            string code = "";
            if (ViewState["CertificateOB"] != null)//从业人员
            {
                CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];
                code = ob.CertificateCode;
                CAID = ob.CertificateCAID;
            }
            else if (ViewState["zjs_CertificateMDL"] != null)//二级造价工程师
            {
                zjs_CertificateMDL ob = (zjs_CertificateMDL)ViewState["zjs_CertificateMDL"];
                code = ob.PSN_RegisterNO;
                CAID = ob.CertificateCAID;
            }
            else//二建建造师
            {
                COC_TOW_Person_BaseInfoMDL ob = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                code = ob.PSN_RegisterNO;
                CAID = ob.CertificateCAID;
            }


            string pdfUrl = string.Format("{2}/{0}/{1}.pdf", CAID.Substring(CAID.Length - 3, 3), CAID, MyWebConfig.CAFile);
            if (RootUrl.Contains("120.52.185.14") == true && File.Exists(pdfUrl) == false)
            {
                UIHelp.layerAlert(Page, "该证书尚无电子证书！");
                return;
            }

            try
            {
                byte[] file = null;
                if (RootUrl.Contains("120.52.185.14") == true)//公网
                {
                    file = Utility.ImageHelp.FileToByte(pdfUrl);
                }
                else
                {
                    //string rtn = Utility.HttpHelp.HttpGet(string.Format("http://localhost:7191/PersonnelFile/GetFile.aspx?o={0}",
                    //    Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "pdf")))
                    //    ));

                    string rtn = "";
                    if (RootUrl.ToLower().Contains("http://localhost") == true)//本机测试
                    {
                        //rtn = Utility.HttpHelp.HttpGet(string.Format("http://120.52.185.14/PersonnelFile/GetFile.aspx?o={0}",
                        rtn = Utility.HttpHelp.HttpGet(string.Format("http://localhost:7191/PersonnelFile/GetFile.aspx?o={0}",
                       Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "pdf")))
                       ));
                    }
                    else//专网
                    {
                        rtn = Utility.HttpHelp.HttpGet(string.Format("http://192.168.5.21/PersonnelFile/GetFile.aspx?o={0}",
                       Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "pdf")))
                       ));
                    }

                    if (rtn == "访问超时" || rtn == "非法访问")
                    {
                        Response.Write(rtn);
                        return;
                    }
                    file = Convert.FromBase64String(rtn);
                }

                #region 记录下载
                FileDownLogMDL o = new FileDownLogMDL();
                o.DownMAN = UserName;
                o.DownManID = UserID;
                o.DownTime = DateTime.Now;
                o.FileTypeCode = EnumManager.FileTypeCode.CA;
                o.FileID = CAID;
                o.DownFileName = string.Format("{0}.pdf", code);
                FileDownLogDAL.Insert(o);
                BindDownLog();
                #endregion

                Response.Clear();
                Response.ContentType = "application/pdf";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment;  filename=" + string.Format("{0}.pdf", Server.UrlEncode(code)));
                Response.BinaryWrite(file);
                Response.Flush();
                Response.End();
                //Response.Close();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "下载电子证书失败", ex);
                return;
            }
            //const long ChunkSize = 1024;//1K 每次读取文件，只读取1K，这样可以缓解服务器的压力 
            //byte[] buffer = new byte[ChunkSize];

            //Response.Clear();
            //using (FileStream iStream = new FileStream(pdfUrl, FileMode.Open))
            //{
            //    long dataLengthToRead = iStream.Length;//获取下载的文件总大小 
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.pdf",Server.UrlEncode(ob.CertificateCode)));
            //    while (dataLengthToRead > 0 && Response.IsClientConnected)
            //    {
            //        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小 
            //        Response.OutputStream.Write(buffer, 0, lengthRead);
            //        Response.Flush();
            //        dataLengthToRead = dataLengthToRead - lengthRead;
            //    }
            //    Response.Close();
            //}


        }

        /// <summary>
        /// 绑定下载日志
        /// </summary>
        private void BindDownLog()
        {
            try
            {
                string code = "";
                if (ViewState["CertificateOB"] != null)
                {
                    CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];
                    code = ob.CertificateCode;
                }
                else
                {
                    COC_TOW_Person_BaseInfoMDL ob = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                    code = ob.PSN_RegisterNO;
                }
                DataTable dt = CommonDAL.GetDataTable(string.Format("select top 5 * from dbo.FileDownLog where DownFileName like '{0}.pdf' and FileTypeCode ={1} order by DownTime desc", code, EnumManager.FileTypeCode.CA));

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dt.Rows)
                {
                    sb.Append(string.Format("{0}　{1}　下载<br/>", r["DownTime"], r["DownMAN"]));
                }

                if (sb.Length > 0)
                {
                    sb.Insert(0, string.Format("本证书最近总计被下载 <span style='color:red'>{0}</span> 次<br/>", dt.Rows.Count));
                    div_downlog.InnerHtml = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取附件下载记录失败", ex);
                return;
            }
        }

        //每天上午9：00—10：00，下午2：00—3：00。	 这两个时间段，进入下载证书页面时，把这个二维码识别加上。
        private void ShowJianDuCard()
        {
            if (DateTime.Now.Hour == 9 || DateTime.Now.Hour == 14)
            {
                div_jdk.Visible = true;
            }
        }

        protected void ButtonDownload_OFD_Click(object sender, EventArgs e)
        {
            string CAID = "";
            string code = "";
            if (ViewState["CertificateOB"] != null)//从业人员
            {
                CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];
                code = ob.CertificateCode;
                CAID = ob.CertificateCAID;
            }
            else if (ViewState["zjs_CertificateMDL"] != null)//二级造价工程师
            {
                zjs_CertificateMDL ob = (zjs_CertificateMDL)ViewState["zjs_CertificateMDL"];
                code = ob.PSN_RegisterNO;
                CAID = ob.CertificateCAID;
            }
            else//二建建造师
            {
                COC_TOW_Person_BaseInfoMDL ob = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
                code = ob.PSN_RegisterNO;
                CAID = ob.CertificateCAID;
            }


            string ofdUrl = string.Format("{2}/{0}/{1}.ofd", CAID.Substring(CAID.Length - 3, 3), CAID, MyWebConfig.CAFile);

            if (RootUrl.Contains("120.52.185.14") == true && File.Exists(ofdUrl) == false)
            {
                UIHelp.layerAlert(Page, "该证书尚无电子证书！");
                return;
            }

            try
            {
                //byte[] file = Utility.ImageHelp.FileToByte(ofdUrl);

                byte[] file = null;
                if (RootUrl.Contains("120.52.185.14") == true)
                {
                    file = Utility.ImageHelp.FileToByte(ofdUrl);
                }
                else
                {
                    //string rtn = Utility.HttpHelp.HttpGet(string.Format("http://120.52.185.14/PersonnelFile/GetFile.aspx?o={0}",
                    //    Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                    //    ));

                    string rtn = "";
                    if (RootUrl.ToLower().Contains("http://localhost") == true)//本机测试
                    {
                        rtn = Utility.HttpHelp.HttpGet(string.Format("http://120.52.185.14/PersonnelFile/GetFile.aspx?o={0}",
                       Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                       ));
                    }
                    else//专网
                    {
                        rtn = Utility.HttpHelp.HttpGet(string.Format("http://192.168.5.21/PersonnelFile/GetFile.aspx?o={0}",
                       Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                       ));
                    }

                    if (rtn == "访问超时" || rtn == "非法访问")
                    {
                        Response.Write(rtn);
                        return;
                    }
                    file = Convert.FromBase64String(rtn);
                }

                #region 记录下载
                FileDownLogMDL o = new FileDownLogMDL();
                o.DownMAN = UserName;
                o.DownManID = UserID;
                o.DownTime = DateTime.Now;
                o.FileTypeCode = EnumManager.FileTypeCode.CA;
                o.FileID = CAID;
                o.DownFileName = string.Format("{0}.ofd", code);
                FileDownLogDAL.Insert(o);
                BindDownLog();
                #endregion

                Response.Clear();
                Response.ContentType = "application/pdf";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment;  filename=" + string.Format("{0}.ofd", Server.UrlEncode(code)));
                Response.BinaryWrite(file);
                Response.Flush();
                Response.End();
                //Response.Close();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "下载电子证书失败", ex);
                return;
            }
        }

        //申请暂扣 or 申请解除暂扣
        protected void ButtonPause_Click(object sender, EventArgs e)
        {
            if (ViewState["CertificateOB"] != null)//从业人员
            {
                CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];
                if (ob.PostTypeID.Value > 2) return;

                CertificatePauseMDL pauseOB = CertificatePauseDAL.GetLastObject(ob.PostTypeID.Value, ob.CertificateCode);

                if (ButtonPause.Text == "申请暂扣")
                {
                    if (pauseOB != null)
                    {
                        UIHelp.layerAlert(Page, string.Format("该证书已经申请过暂扣，尚未解除，无法再次申请！申请时间：{0}，确认时间：{1}"
                            , pauseOB.PauseApplyTime.Value.ToString("yyyy-MM-dd HH:mm")
                            , pauseOB.PauseDoTime.HasValue ? pauseOB.PauseDoTime.Value.ToString("yyyy-MM-dd HH:mm") : ""
                            ));
                        return;
                    }
                    else
                    {
                        pauseOB = new CertificatePauseMDL();
                        pauseOB.CertificateCode = ob.CertificateCode;
                        pauseOB.PauseApplyMan = PersonName;
                        pauseOB.PauseApplyTime = DateTime.Now;
                        pauseOB.PostTypeID = ob.PostTypeID;
                        pauseOB.PauseStatusCode = 1;
                        pauseOB.PauseStatus = "申请暂扣";

                        DBHelper db = new DBHelper();
                        DbTransaction tran = db.BeginTransaction();
                        try
                        {
                            CertificatePauseDAL.Insert(tran, pauseOB);
                            CertificateDAL.ClearZACheckTime(tran, ob.CertificateID.Value);
                            tran.Commit();
                            UIHelp.WriteOperateLog(UserName, UserID, "电子证书申请暂扣", string.Format("证书编号：{0}。", ob.CertificateCode));
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            UIHelp.WriteErrorLog(Page, string.Format("电子证书{0}申请暂扣失败", ob.CertificateCode), ex);
                        }

                        UIHelp.layerAlert(Page, "已成功经申请电子证书暂扣，系统之后会同步状态到国家库。");
                    }
                }
                else
                {
                    if (pauseOB.PauseStatusCode == 2)//申请解除暂扣
                    {
                        pauseOB.EndPauseApplyMan = PersonName;
                        pauseOB.EndPauseApplyTime = DateTime.Now;
                        pauseOB.PauseStatusCode = 3;
                        pauseOB.PauseStatus = "申请返还";

                        DBHelper db = new DBHelper();
                        DbTransaction tran = db.BeginTransaction();
                        try
                        {
                            CertificatePauseDAL.Update(tran, pauseOB);
                            CertificateDAL.ClearZACheckTime(tran, ob.CertificateID.Value);
                            tran.Commit();
                            UIHelp.WriteOperateLog(UserName, UserID, "电子证书申请暂扣返还", string.Format("证书编号：{0}。", ob.CertificateCode));
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            UIHelp.WriteErrorLog(Page, string.Format("电子证书{0}申请暂扣返还失败", ob.CertificateCode), ex);
                        }
                        UIHelp.layerAlert(Page, "已成功经申请电子证书暂扣返还，系统之后会同步状态到国家库。");
                    }
                    else if (pauseOB.PauseStatusCode == 3)
                    {
                        UIHelp.layerAlert(Page, string.Format("该证书已经申请了返还，无法再次申请，请等待国家处理！申请返还时间：{0}", pauseOB.EndPauseApplyTime.Value.ToString("yyyy-MM-dd HH:mm")));
                        return;
                    }
                }
            }
        }

        //重新发起国家赋码校验
        protected void ButtonReCheck_Click(object sender, EventArgs e)
        {
            if (ViewState["CertificateOB"] != null)//从业人员
            {
                CertificateOB ob = (CertificateOB)ViewState["CertificateOB"];
                if (ob.PostTypeID > 2) return;

                if (ob.EleCertErrStep == EnumManager.EleCertDoStep.GetCode
                            && (ob.QRCodeTime.HasValue == false || ob.QRCodeTime < ob.EleCertErrTime)
                   )
                {
                    ob.EleCertErrTime = null;

                    try
                    {
                        CommonDAL.ExecSQL(string.Format(@"
                            update  certificate 
                            set EleCertErrTime =null
                            where certificatecode='{0}'", ob.CertificateCode));

                        ViewState["CertificateOB"] = ob;

                        ButtonReCheck.Visible = false;
                    }
                    catch
                    {
                        UIHelp.layerAlert(Page, "申请重新校验失败!",5,0);
                        return;
                    }

                    UIHelp.layerAlert(Page, "申请重新校验成功!请关闭页面耐心等待，稍后重新打开该页面查看结果。",6,0);
                }
            }
        }
    }
}
