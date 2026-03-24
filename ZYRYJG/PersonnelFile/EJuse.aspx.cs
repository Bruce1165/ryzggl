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
    public partial class EJuse : BasePage
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
                string PSN_ServerID = Utility.Cryptography.Decrypt(Request["c"]);
                ViewState["PSN_ServerID"] = PSN_ServerID;

                COC_TOW_Person_BaseInfoMDL ob = COC_TOW_Person_BaseInfoDAL.GetObject(PSN_ServerID);
                ViewState["COC_TOW_Person_BaseInfoMDL"] = ob;
                if (PersonType != 1 && PersonType != 6)//不是超管员、管理审核部门人员
                {
                    if (PersonType != 2 //不是个人登录，不允许下载二建电子证书使用件
                        || WorkerCertificateCode != ob.PSN_CertificateNO)//不允许下载别人的电子证书
                    {
                        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("非法访问！"), false);
                        return;
                    }
                }

                if (ob.PSN_RegisteType == "07")
                {
                    DivDetail.InnerHtml = "<p style='color:red'>该证书已经注销，无法下载电子证书。</p>";
                    return;
                }

                if (ob.PSN_CertificateValidity.Value.AddDays(1) < DateTime.Now)
                {
                    DivDetail.InnerHtml = "<p style='color:red'>该证书已经过期，只能先办理注销，再发起初始注册后才能下载电子证书。</p>";
                    return;
                }

                //限定打印电子证书前，未上传签名的人需要先上传签名，才能办理申请。
                string imgPath = COC_TOW_Person_FileDAL.GetFileUrl(ob.PSN_RegisterNO, EnumManager.FileDataTypeName.手写签名照);
      
                if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == false)
                {
                    imgPath = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", ob.PSN_CertificateNO.Substring(ob.PSN_CertificateNO.Length - 3, 3), ob.PSN_CertificateNO);
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == false)
                    {
                        DivDetail.InnerHtml = "<p style='color:red'>二级建造师电子证书需要您上传个人手写签名照片，请您先到“我的信息》个人信息维护”页面上传后才能申请下载。</p>";
                        return;
                    }
                    else
                    {
                        WorkerOB _WorkerOB = WorkerDAL.GetUserObject(ob.PSN_CertificateNO);
                        if (_WorkerOB.SignPhotoTime.HasValue == false)
                        {
                            DivDetail.InnerHtml = "<p style='color:red'>您上传个人手写签名照片尚未提交，请您先到“我的信息》个人信息维护”页面提交签名后再申请电子证书下载。</p>";
                            return;
                        }
                    }
                }

                refresh();
            }
        }

        private void refresh()
        {
            //使用件
            EJCertUseMDL _EJCertUseMDL = EJCertUseDAL.GetCurrentUse(ViewState["PSN_ServerID"].ToString());
            if (_EJCertUseMDL == null)
            {
                DivDetail.InnerHtml = "<p style='color:red'>持证人获取新版电子证照时，应确认电子证照的使用有效期，使用有效期应在注册专业有效期范围内。请您首先填写使用电子证书有效期。</p>";
                BindRadGridUse();
                return;
            }
            else
            {
                ViewState["EJCertUseMDL"] = _EJCertUseMDL;
                if (_EJCertUseMDL.Pdf_ReturnCATime.HasValue == true)
                {
                    ButtonDownload.Text = string.Format("{0}.pdf", _EJCertUseMDL.CertificateCAID);
                    ButtonDownload.Visible = true;
                }

                if (_EJCertUseMDL.Ofd_ReturnCATime.HasValue == true)
                {
                    ButtonDownload_OFD.Text = string.Format("{0}.ofd", _EJCertUseMDL.CertificateCAID);
                    ButtonDownload_OFD.Visible = true;
                }

                if (_EJCertUseMDL.Pdf_ReturnCATime.HasValue == false && _EJCertUseMDL.Ofd_ReturnCATime.HasValue == false)
                {

                    DivDetail.InnerHtml = "<p style='color:red'>电子证书还在生成和签章途中，请稍后再访问该页面进行下载。</p>";
                    BindRadGridUse();
                    return;
                }
            }

            UIHelp.layerAlertWithHtml(Page, @"二级注册建造师打印电子证书后，应在个人签名处手写本人签名并签署签名日期，未手写签名的，该电子证书无效。");

            BindRadGridUse();
            BindDownLog();
            DivDetail.InnerHtml = "已经为您准备好了数据，请点击链接进行下载您需要的格式。";
            ShowJianDuCard();
        }

        //pdf使用件下载
        protected void ButtonDownload_Click(object sender, EventArgs e)
        {
            EJCertUseMDL ob = (EJCertUseMDL)ViewState["EJCertUseMDL"];
                string code = ob.PSN_RegisterNO;
                string CAID = ob.CertificateCAID;
          


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

                    string rtn = "";
                    if (RootUrl.ToLower().Contains("http://localhost") == true)//本机测试
                    {
                        rtn = Utility.HttpHelp.HttpGet(string.Format("http://120.52.185.14/PersonnelFile/GetFile.aspx?o={0}",
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

        //ofd使用件下载
        protected void ButtonDownload_OFD_Click(object sender, EventArgs e)
        {
            EJCertUseMDL ob = (EJCertUseMDL)ViewState["EJCertUseMDL"];
                string code = ob.PSN_RegisterNO;
                string CAID = ob.CertificateCAID;
           


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
                    string rtn = Utility.HttpHelp.HttpGet(string.Format("http://localhost:7191/PersonnelFile/GetFile.aspx?o={0}",
                        Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                        ));

                    //string rtn = "";
                    //if (RootUrl.ToLower().Contains("http://localhost") == true)//本机测试
                    //{
                    //    rtn = Utility.HttpHelp.HttpGet(string.Format("http://120.52.185.14/PersonnelFile/GetFile.aspx?o={0}",
                    //   Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                    //   ));
                    //}
                    //else//专网
                    //{
                    //    rtn = Utility.HttpHelp.HttpGet(string.Format("http://192.168.5.21/PersonnelFile/GetFile.aspx?o={0}",
                    //   Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess", CAID, "ofd")))
                    //   ));
                    //}

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

        /// <summary>
        /// 绑定使用件申请记录
        /// </summary>
        private void BindRadGridUse()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("[PSN_ServerID] = '{0}'", ViewState["PSN_ServerID"]));

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridUse.CurrentPageIndex = 0;
            RadGridUse.DataSourceID = ObjectDataSource1.ID;
        }

        /// <summary>
        /// 绑定下载日志
        /// </summary>
        private void BindDownLog()
        {
            try
            {
                DataTable dt = CommonDAL.GetDataTable(string.Format("select top 5 * from dbo.FileDownLog where (DownFileName like '{0}.pdf' or DownFileName like '{0}.ofd') and FileTypeCode ={1} order by DownTime desc", ((EJCertUseMDL)ViewState["EJCertUseMDL"]).PSN_RegisterNO, EnumManager.FileTypeCode.CA));

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

        //添加使用范围
        protected void RadGridUse_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            EJCertUseMDL _EJCertUseMDL = new EJCertUseMDL();
            UIHelp.GetData(editedItem, _EJCertUseMDL);
            if(_EJCertUseMDL.BeginTime.HasValue==false || _EJCertUseMDL.BeginTime.Value.AddDays(1) <DateTime.Now)
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "数据校验失败：证书使用有效期起始日期不能为空，且大于等于今天！",5,0);
                return;
            }

            COC_TOW_Person_BaseInfoMDL ob = (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];

            if (_EJCertUseMDL.EndTime.HasValue == false || _EJCertUseMDL.EndTime.Value > ob.PSN_CertificateValidity.Value)
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "数据校验失败：证书使用有效期截止日期不能为空，且小于等于证书有效期截止日期！", 5, 0);
                return;
            }

            if(_EJCertUseMDL.BeginTime.Value > _EJCertUseMDL.EndTime.Value)
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "数据校验失败：证书使用有效期截止开始日期不能大于使用有效期截止日期！", 5, 0);
                return;
            }

            int count = EJCertUseDAL.SelectCount(string.Format(" and PSN_ServerID='{0}' and cjsj between '{1}' and '{2}'",
                ViewState["PSN_ServerID"], DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));
            if(count >1)
            {
                e.Canceled = true;
                UIHelp.layerAlert(Page, "创建电子证书使用件超出当日对大允许次数，请明日再试！", 5, 0);
                return;
            }
           
            UnitMDL _UnitMDL = UnitDAL.GetObject(ob.ENT_ServerID);

            EJCertUseMDL curEJCertUseMDL = ViewState["EJCertUseMDL"] == null ? null : (EJCertUseMDL)ViewState["EJCertUseMDL"];

            _EJCertUseMDL.CertificateCAID = Guid.NewGuid().ToString();
            _EJCertUseMDL.PSN_ServerID = ob.PSN_ServerID;
            _EJCertUseMDL.CJSJ = DateTime.Now;
            _EJCertUseMDL.Valid = 1;

            //_EJCertUseMDL.ZZBS=ob.ZZBS;
            _EJCertUseMDL.ENT_ServerID = ob.ENT_ServerID;
            _EJCertUseMDL.ENT_Name = ob.ENT_Name;
            _EJCertUseMDL.ENT_OrganizationsCode = _UnitMDL.CreditCode;//社会统一信用代码
            _EJCertUseMDL.PSN_Name = ob.PSN_Name;
            _EJCertUseMDL.PSN_Sex = ob.PSN_Sex;
            _EJCertUseMDL.PSN_BirthDate = ob.PSN_BirthDate;
            _EJCertUseMDL.PSN_CertificateType = ob.PSN_CertificateType;
            _EJCertUseMDL.PSN_CertificateNO = ob.PSN_CertificateNO;
            _EJCertUseMDL.PSN_RegisteType = ob.PSN_RegisteType;
            _EJCertUseMDL.PSN_RegisterNO = ob.PSN_RegisterNO;
            _EJCertUseMDL.PSN_RegisterCertificateNo = ob.PSN_RegisterCertificateNo;
            _EJCertUseMDL.PSN_RegisteProfession = ob.PSN_RegisteProfession;
            _EJCertUseMDL.PSN_CertificationDate = ob.PSN_CertificationDate;
            _EJCertUseMDL.PSN_CertificateValidity = ob.PSN_CertificateValidity;
            _EJCertUseMDL.PSN_RegistePermissionDate = ob.PSN_RegistePermissionDate;
            _EJCertUseMDL.ZGZSBH = ob.ZGZSBH;

            if (curEJCertUseMDL != null)
            {
                //_EJCertUseMDL.BeginTime=curEJCertUseMDL.BeginTime;
                //_EJCertUseMDL.EndTime=curEJCertUseMDL.EndTime;

                _EJCertUseMDL.Pdf_license_code = curEJCertUseMDL.Pdf_license_code;
                _EJCertUseMDL.Pdf_auth_code = curEJCertUseMDL.Pdf_auth_code;
                _EJCertUseMDL.Ofd_license_code = curEJCertUseMDL.Ofd_license_code;
                _EJCertUseMDL.Ofd_auth_code = curEJCertUseMDL.Ofd_auth_code;
            }

            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                EJCertUseDAL.Insert(tran, _EJCertUseMDL);
                if (curEJCertUseMDL != null)
                {
                    curEJCertUseMDL.Valid = 0;
                    EJCertUseDAL.Update(tran, curEJCertUseMDL);
                }
                CommonDAL.ExecSQL(tran, string.Format(@"update [EJCertUse]
                                                                set [Valid]=0
                                                                where [PSN_ServerID]='{0}' and [Valid]=1 and [CertificateCAID] <> '{1}'", _EJCertUseMDL.PSN_ServerID, _EJCertUseMDL.CertificateCAID));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "添加二级建造师使用范围失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "添加二级建造师使用范围", string.Format("证书注册编号：{0}，使用范围{1} - {2}。",
                ob.PSN_CertificateNO,
                _EJCertUseMDL.BeginTime.Value.ToString("yyyy.MM.dd"),
                _EJCertUseMDL.EndTime.Value.ToString("yyyy.MM.dd")
                ));
            UIHelp.layerAlert(Page, "电子证照使用有效期添加成功,请于12小时之后下载电子证照！", 6, 3000);

             refresh();
        }

    }
}
