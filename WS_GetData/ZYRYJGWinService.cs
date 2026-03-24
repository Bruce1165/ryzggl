using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Text;
using System.Web;
using Synergy.Common;
using System.Linq;
using DataAccess;
using Model;
using zxja.Interface.Buisness;
using zxja.Interface.Buisness.DataSchema;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WS_GetData.Api;
using WS_GetData.Api.Model;
//using Atg.Api;

namespace WS_GetData
{
    /// <summary>
    /// 人员资格系统定时windows服务，通过控制app.config中的配置（DoTimeSet，DoTimeSpanSet，MaxCount）决定服务定期执行方法
    /// 1、本机：人员系统自身服务部署5.21。
    /// 2、公网：调用委外接口共享数据服务部署在5.49上。（例如：与建设部共享数据）
    /// 3、专网：北京专网中其他系统共享数据部署在150.109上。（例如：电子证书签章）
    /// </summary>
    public partial class ZYRYJGWinService : ServiceBase
    {
        #region 服务配置及变量

        JCSJKService.InterFaceServiceSoapClient jcsjk = new JCSJKService.InterFaceServiceSoapClient();
        DESCrypto descObj = new DESCrypto();
        string userName = "RTDL_RYKWXT";//"XNJ_ZYRYZCXT_EJ";
        string userPassword = "RTDL_2013";//"XNJ__2017";
        string iInterFaceID = "";
        string Condition = "";
        string SortText = "";
        int PageSize = 10;
        int outResultCount = 0;
        Dictionary<string, string> DoTimeSet;
        Dictionary<string, string> Finished;
        Dictionary<string, string> DoTimeSpanSet;
        SecurityKeyword G_SecurityKeyword = null; //二建本次上传时，所用密钥信息
        zxjaUpdater.DBUploaderSoapClient _DBUploader = null; //webservice实例
        BackgroundWorker worker = null;
        const string g_LicenseID = "9Qxx7Iym03uavqfIe82LvA";//许可
        string ZYRYJGWebRoot = "";//执业人员监管平台根目录

        string ExamWebRoot = System.Configuration.ConfigurationManager.AppSettings["ZYRYJGWebRoot"];//网站根目录
        string JHK_IP = System.Configuration.ConfigurationManager.AppSettings["JHK_IP"];//证照库交换库连接IP（）
        int MaxCountExe = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxCount"]);//一次最大处理电子证书数量
        string CAFile = System.Configuration.ConfigurationManager.AppSettings["CAFile"];//电子证书根目录
        string IssueCA_accessToken = "";
        DateTime IssueCA_accessTime = DateTime.Now.AddDays(-2);
        //bool IssueCA_jzsing = false;//二建Pdf电子签章服务正在运行状态
        bool IssueCA_congyeing = false;//从业人员Pdf电子签章服务正在运行状态
        bool IssueCA_zjsing = false;//二级造价工程师Pdf电子签章服务正在运行状态
        bool IssueCA_Ofd_congyeing = false;//从业人员Ofd电子签章服务正在运行状态
        Dictionary<string, long> cursor_long = new Dictionary<string, long>();//当前扫描到数字类型从业人员证书ID
        Dictionary<string, string> cursor_str = new Dictionary<string, string>();//当前扫描到文本类标识ID
        /// <summary>
        /// 质安网登录令牌（安全员员）
        /// </summary>
        string gb_accessToken = "";
        /// <summary>
        /// 质安网登录时效时间（安全员员）
        /// </summary>
        DateTime gb_expiresTime = DateTime.Now.AddDays(-2);

        /// <summary>
        /// 质安网登录令牌（特种作业）
        /// </summary>
        string gb_accessToken_tz = "";
        /// <summary>
        /// 质安网登录时效时间（特种作业）
        /// </summary>
        DateTime gb_expiresTime_tz = DateTime.Now.AddDays(-2);

        DateTime ZhiAnFunStartTime;//质安网接口调用开始时间
        int ZhiAnFunDoStepNo = 1;//质安网接口调用开始序号
        DateTime OfdReturnStartTime;//Ofd取回运行时间
        DateTime PDFReturnStartTime;//PDF取回运行时间
        DateTime MoveHisCAFileStartTime;//移动历史电子证书服务运行开始时间        

        Dictionary<string, int> SheBaoChekCount = new Dictionary<string, int>();//待社保比对各业务数据量统计
        //DateTime TjTime_SheBaoChekCount;//移动历史电子证书服务运行开始时间
        DateTime ShebaoCheckStartTime;//社保对比调用开始时间
        string ShebaoCheckStep = "cy_change";//当前社保比对进行到业务类型

        DateTime EJJZS_Use_StartTime;//二建电子证书使用件生成调用开始时间
        int EJJZS_Use_Step = 1;//二建电子证书使用件签章环节编号


        //办件汇聚网关地址(预发)
        private static readonly string gatewayUrl = "http://172.26.62.135:8086/openapi";
        //办件汇聚应用ID
        private static readonly string appId = "824635";
        //办件汇聚商户私钥
        private static readonly Atg.Api.AtgBusSecretKey secretKey = new Atg.Api.AtgBusSecretKey("w0oinszrbznxf8cx56agyq4y9l6z96y4", "zDQcK83HkSOUnHM6jnTSi8BT");
        //办件汇聚请求客户端
        private static readonly Atg.Api.IAtgBusClient Bizclient = new Atg.Api.DefaultAtgBusClient(gatewayUrl, appId, secretKey);
        DateTime ApplyGatherStartTime;//办件汇聚调用开始时间
        string ApplyGatherStep = "ApplyGatherCertChange";//当前办件汇聚进行到业务类型


        /// <summary>
        /// 人员资格定时服务
        /// </summary>
        public ZYRYJGWinService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                timer1.Enabled = true;

                ZYRYJGWebRoot = System.Configuration.ConfigurationManager.AppSettings["ZYRYJGWebRoot"];
                string[] set = System.Configuration.ConfigurationManager.AppSettings["DoTimeSet"].ToString().Split('|');
                DoTimeSet = new Dictionary<string, string>();
                Finished = new Dictionary<string, string>();
                string[] v;
                foreach (string s in set)
                {
                    v = s.Split(',');
                    DoTimeSet.Add(v[0], v[1]);
                }

                string[] setSpan = System.Configuration.ConfigurationManager.AppSettings["DoTimeSpanSet"].ToString().Split('|');

                DoTimeSpanSet = new Dictionary<string, string>();

                string[] t;
                foreach (string s in setSpan)
                {
                    t = s.Split(',');
                    DoTimeSpanSet.Add(t[0], t[1]);
                }

                cursor_long.Add("cursor_CongYe_ZhiAnDataCheck", long.MaxValue - 1);//当前扫描待数据校验的从业人员证书ID
                cursor_long.Add("cursor_CongYe_FaceImg", long.MaxValue - 1);//当前扫描待同步一寸照片的从业人员证书ID
                cursor_long.Add("cursor_CongYe_GetQRCode", long.MaxValue - 1);//当前扫描待赋码的从业人员证书ID
                cursor_long.Add("cursor_CongYe_updateCertStatus", long.MaxValue - 1);//当前扫描待更新证书状态的从业人员证书ID
                cursor_long.Add("cursor_CongYe_UpCertUrl", long.MaxValue - 1);//当前扫描待归档的从业人员证书ID
                cursor_long.Add("cursor_CongYe_CreateGuoBiaoPdf", long.MaxValue - 1);//当前扫描待生成国标的从业人员证书ID
                cursor_long.Add("IssueCA_Ofd_Cur_CertificateID", long.MaxValue - 1);//当前扫描待Ofd签章的从业人员证书ID
                cursor_long.Add("ReturnCA_Cur_CertificateID_all", long.MaxValue - 1);//当前扫描取回已签章的从业人员证书ID
                cursor_long.Add("IssueCA_Cur_CertificateID", long.MaxValue - 1);//当前扫描待签章的从业人员证书
                cursor_long.Add("CreateCA_Cur_CertificateID_CYZY", long.MaxValue - 1);//从业人员当前扫描创建签章的证书ID
                cursor_long.Add("cursor_ExamSignup_ZhiAnDataCheck", long.MaxValue - 1);//考试报名当前扫描报名ID
                cursor_long.Add("cursor_CertificateEnter_ZhiAnDataCheck", long.MaxValue - 1);//三类人进京申请当前扫描ID
                cursor_long.Add("ReturnCA_Cur_CertificateID_OFD", long.MaxValue - 1);//当前扫描取回已签章的OFD从业人员证书ID
                cursor_long.Add("cursor_CertificateChange_ZhiAnDataCheck", long.MaxValue - 1);//三类人、特种作业变更申请当前扫描ID
                cursor_long.Add("cursor_CertificateContinue_ZhiAnDataCheck", long.MaxValue - 1);//三类人、特种作业续期申请当前扫描ID

                cursor_long.Add("cursor_SheBao_cy_change", 0);//社保比对，从业人员变更，申请当前扫描ID
                cursor_long.Add("cursor_SheBao_cy_jinjing", 0);//社保比对，从业人员进京，申请当前扫描ID
                cursor_long.Add("cursor_SheBao_cy_Signup", 0);//社保比对，从业人员考试，申请当前扫描ID
                cursor_long.Add("cursor_SheBao_cy_continue", 0);//社保比对，从业人员续期，申请当前扫描ID
                cursor_long.Add("cursor_SheBao_jzs", DateTime.Now.AddYears(-10).Ticks);//社保比对，二级建造师，申请当前扫描ID
                cursor_long.Add("cursor_SheBao_zjgcs", DateTime.Now.AddYears(-10).Ticks);//社保比对，二级造价工程，申请当前扫描ID

                cursor_long.Add("cursor_EJJZS_Use_CreateCA", DateTime.Now.AddYears(-10).Ticks);//扫描创建二建使用件当时间
                cursor_long.Add("cursor_EJJZS_Use_PDF_IssueCA", DateTime.Now.AddYears(-10).Ticks);//扫描签章二建使用件pdf当前时间
                cursor_long.Add("cursor_EJJZS_Use_PDF_GetReturn", DateTime.Now.AddYears(-10).Ticks);//扫描回写二建使用件pdf当前时间
                cursor_long.Add("cursor_EJJZS_Use_OFD_IssueCA", DateTime.Now.AddYears(-10).Ticks);//扫描签章二建使用件Ofd当前时间
                cursor_long.Add("cursor_EJJZS_Use_OFD_GetReturn", DateTime.Now.AddYears(-10).Ticks);//扫描回写二建使用件Ofd当前时间

                cursor_long.Add("DownCA_CertificateID", long.MaxValue - 1);//当前扫描待下载的从业人员证书ID

                cursor_str.Add("IssueCA_Cur_PSN_CertificateNO_ZJS", "");//当前扫描待签章的二级造价工程师证书ID    
                cursor_str.Add("ReturnCA_Cur_PSN_CertificateNO_ZJS", "");//当前扫描取回已签章的二级造价工程师证书ID              
                cursor_str.Add("CreateCA_Cur_PSN_CertificateNO_ZJS", "");//当前扫描二级造价工程师创建签章的证书ID
                cursor_str.Add("UploadJSB_ZJGCS_PSN_ServerID", "");//当前扫描待上传建设部的二级造价工程师证书ID
                cursor_str.Add("cursor_CongYe_PatchChangeUnit", "");//当前扫描批量变更赋码身份证ID
                cursor_str.Add("cursor_EJJZS_Use_AutoApply", "");//当前扫描二建使用件申请证书ID

                cursor_long.Add("cursor_BJHJ_cy_change", 0);//办件汇聚，从业人员变更，申请当前扫描ID
                cursor_long.Add("cursor_BJHJ_cy_jinjing", 0);//办件汇聚，从业人员进京，申请当前扫描ID
                cursor_long.Add("cursor_BJHJ_cy_Signup", 0);//办件汇聚，从业人员考试，申请当前扫描ID
                cursor_long.Add("cursor_BJHJ_cy_continue", 0);//办件汇聚，从业人员续期，申请当前扫描ID
                cursor_long.Add("cursor_BJHJ_cy_More", 0);//办件汇聚，从业人员A本增发，申请当前扫描ID
                cursor_str.Add("cursor_BJHJ_jzs", "");//办件汇聚，二级建造师，申请当前扫描ID
                cursor_str.Add("cursor_BJHJ_zjgcs", "");//办件汇聚，二级造价工程，申请当前扫描ID
  

            }
            catch (Exception ex)
            {
                FileLog.WriteLog("服务启动失败：" + ex.Message, ex);
                return;
            }
            FileLog.WriteLog("服务启动", null);
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            timer1.Enabled = false;
            timer1.Stop();
            timer1.Dispose();
            Thread.Sleep(5000);    //等待5秒
            FileLog.WriteLog("服务停止", null);
            this.Dispose();
        }

        private void Start(string _actionCode, object _paramData)
        {
            if (this.worker == null)
            {
                this.worker = new BackgroundWorker();
                this.worker.WorkerReportsProgress = true;
                this.worker.WorkerSupportsCancellation = true;
                this.worker.DoWork += new DoWorkEventHandler(doUpgrade);//主调
                this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpgradeComplated);//异步回调
                //this.worker.ProgressChanged += new ProgressChangedEventHandler(UpgradeProgressChanged);//异步过程处理

            }
            if (!this.worker.IsBusy)
            {
                //参数
                PostParameter _PostParameter = new PostParameter();
                _PostParameter.LicenseID = g_LicenseID;
                _PostParameter.ActionCode = _actionCode;
                _PostParameter.Param = _paramData;// this.G_SecurityKeyword.PublicKey;

                this.worker.RunWorkerAsync(_PostParameter);
            }
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //每日一次性任务
            foreach (string k in DoTimeSet.Keys)
            {
                if (DateTime.Now.ToShortTimeString().Equals(DoTimeSet[k])
                    || DateTime.Now.AddMinutes(-1).ToShortTimeString().Equals(DoTimeSet[k])
                    )
                {
                    if (Finished.Keys.Contains<string>(k) == true)
                    {
                        if (Finished[k] == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            continue;
                        }
                        else
                        {
                            Finished[k] = DateTime.Now.ToString("yyyyMMdd");
                        }
                    }
                    else
                    {
                        Finished.Add(k, DateTime.Now.ToString("yyyyMMdd"));
                    }
                    this.ExecThread(k);
                }
            }

            //时间间隔类型多次执行任务
            foreach (string k in DoTimeSpanSet.Keys)
            {
                if (DateTime.Now.Minute % Convert.ToInt32(DoTimeSpanSet[k]) == 0)
                {
                    this.ExecThread(k);
                }
            }
        }

        #endregion 服务配置及变量

        //任务调用方法名称
        private void ExecThread(string funcName)
        {
            Thread tSend;
            switch (funcName)
            {
                case "Upload_SBDataOfZJGCS"://二级造价工程师社保上传比对数据服务
                    tSend = new Thread(new ThreadStart(Upload_SBDataOfZJGCS));
                    tSend.Start();
                    break;
                case "Upload_SBData"://二建社保上传比对数据服务
                    tSend = new Thread(new ThreadStart(Upload_SBData));
                    tSend.Start();
                    break;
                case "Upload_SBDataOfExam"://从业人员社保上传比对数据服务
                    tSend = new Thread(new ThreadStart(Upload_SBDataOfExam));
                    tSend.Start();
                    break;
                case "Upload_SBDataOfExam2"://从业人员社保上传比对数据服务
                    tSend = new Thread(new ThreadStart(Upload_SBDataOfExam));
                    tSend.Start();
                    break;
                case "Upload_JZS_New"://上传二级建造师到建设部(新接口,部署在5.49)
                    tSend = new Thread(new ThreadStart(Upload_JZS_New));
                    tSend.Start();
                    break;
                case "Upload_EJJZS_File"://上传二建审批附件
                    tSend = new Thread(new ThreadStart(Upload_EJJZS_File));
                    tSend.Start();
                    break;
                case "GetQYZZ"://获取企业资质及详细数据服务
                    tSend = new Thread(new ThreadStart(GetQYZZ));
                    tSend.Start();
                    break;
                case "DeleteTimeOutApply"://删除过期申请单服务（变更、续期、进京）
                    tSend = new Thread(new ThreadStart(DeleteTimeOutApply));
                    tSend.Start();
                    break;
                case "InsertInterFaceLog"://导入文件、照片接口访问日志服务
                    tSend = new Thread(new ThreadStart(InsertInterFaceLog));
                    tSend.Start();
                    break;
                case "AgeLimit"://从业人员超龄注销 + 二建专业过期注销 + 二建证书过期注销
                    tSend = new Thread(new ThreadStart(LimitAgeOutCertificate));
                    tSend.Start();
                    break;
                case "Upload_FiletoDFS"://上传证书一寸免冠照片到dfs
                    tSend = new Thread(new ThreadStart(Upload_FiletoDFS));
                    tSend.Start();
                    break;
                case "TJ_WLH"://统计数据 物理化
                    tSend = new Thread(new ThreadStart(TJ_WLH));
                    tSend.Start();
                    break;
                //case "CreateEJJZS_CA"://生成二级建造师电子证书（部署在21上）
                //    tSend = new Thread(new ThreadStart(CreateEJJZS_CA));
                //    tSend.Start();
                //    break;
                //case "SendEJJZS_CA"://发送待签章的二建证书到基础库（部署在21上）
                //    tSend = new Thread(new ThreadStart(SendEJJZS_CA));
                //    tSend.Start();
                //    break;
                //case "GetReturnEJJZS_CA"://取回已签章的二建证书（部署在21上）
                //    tSend = new Thread(new ThreadStart(GetReturnEJJZS_CA));
                //    tSend.Start();
                //    break;
                case "ApplyCA"://生成待签章的从业证书（部署在21上）
                    tSend = new Thread(new ThreadStart(ApplyCA));
                    tSend.Start();
                    break;
                case "SendCA"://发送待签章的从业证书到基础库
                    tSend = new Thread(new ThreadStart(SendCA));
                    tSend.Start();
                    break;
                case "ReturnCA"://取回已签章的从业证书（部署在21上）
                    tSend = new Thread(new ThreadStart(ReturnCA));
                    tSend.Start();
                    break;
                case "ReturnOFDCA"://取回已签章的OFD从业证书（部署在21上）
                    tSend = new Thread(new ThreadStart(ReturnOFDCA));
                    tSend.Start();
                    break;
                case "UpdateFacePhoto"://更新证书绑定一寸照片地址（部署在21上）
                    tSend = new Thread(new ThreadStart(UpdateFacePhoto));
                    tSend.Start();
                    break;
                //case "IssueCA_beijing"://市级电子证书签章服务（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(IssueCA_beijing));
                //    tSend.Start();
                //    break;
                //case "IssueCA_Certiticate"://市级电子证书签章服务_从业人员证书（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(IssueCA_Certiticate));
                //    tSend.Start();
                //    break;
                //case "IssueCA_JZS"://调用市级电子证书签章服务_二级建造师（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(IssueCA_JZS));
                //    tSend.Start();
                //    break;
                case "GuiWeiData"://规委档案数据获取
                    tSend = new Thread(new ThreadStart(GuiWeiData));
                    tSend.Start();
                    break;
                case "TipJzsContinue"://发送建造师续期短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipJzsContinue));
                    tSend.Start();
                    break;
                case "TipJzsDisenableContinue"://发送建造师有效期不足30天，无法申请延续注册短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipJzsDisenableContinue));
                    tSend.Start();
                    break;                    
                case "TipZjgcsContinue"://发送二级造价工程师续期短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipZjgcsContinue));
                    tSend.Start();
                    break;
                case "TipZjgcsDisenableContinue"://发送二级造价工程师有效期不足30天，无法申请延续注册短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipZjgcsDisenableContinue));
                    tSend.Start();
                    break;                    
                case "TipJzsShenSu"://发送建造师受理、审批不通过短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipJzsShenSu));
                    tSend.Start();
                    break;
                case "TipZjgcsCheckFalse"://发送二级造价工程师受理、审批不通过短息提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipZjgcsCheckFalse));
                    tSend.Start();
                    break;                    
                case "TipShiCaoExam"://发送特种作业实操考试短信提醒（部署在21上）
                    tSend = new Thread(new ThreadStart(TipShiCaoExam));
                    tSend.Start();
                    break;
                case "TipCongContinue"://给安管人员/特种作业人员证书有效期届满前15天仍未提交续期申请的持证人发送短信提醒。（部署在21上）
                    tSend = new Thread(new ThreadStart(TipCongContinue));
                    tSend.Start();
                    break; 
                    
                case "TipCongYeCertApply"://从业人员证书受理、审核不通过短信通知（部署在21上）
                    tSend = new Thread(new ThreadStart(TipCongYeCertApply));
                    tSend.Start();
                    break;                    
                case "ExamPhotoRar"://上机考试照片打包下载（部署在21上）
                    tSend = new Thread(new ThreadStart(ExamPhotoRar));
                    tSend.Start();
                    break;
                case "CheckJZSImgBackgroud"://检查二建证书照片是否为白底照片（部署在21上）
                    tSend = new Thread(new ThreadStart(CheckJZSImgBackgroud));
                    tSend.Start();
                    break;
                case "CreateZJS_CA"://生成二级造价工程师电子电子证书（部署在21上）
                    tSend = new Thread(new ThreadStart(CreateZJS_CA));
                    tSend.Start();
                    break;
                case "SendZJS_CA"://发送待签章的二级造价工程师电子证书到基础库（部署在21上）
                    tSend = new Thread(new ThreadStart(SendZJS_CA));
                    tSend.Start();
                    break;
                case "IssueCA_ZJS"://调用市级电子证书签章服务_二级造价工程师（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(IssueCA_ZJS));
                    tSend.Start();
                    break;
                case "GetReturnZJS_CA"://取回已签章的二级造价工程师电子证书（部署在21上）
                    tSend = new Thread(new ThreadStart(GetReturnZJS_CA));
                    tSend.Start();
                    break;
                case "UploadZJGCS_JSB"://上传二级造价工程师注册数据到建设部（部署在49上）
                    tSend = new Thread(new ThreadStart(UploadZJGCS_JSB));
                    tSend.Start();
                    break;
                //case "IssueCA_CongYeOfd"://Ofd电子证书签章服务_从业人员证书（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(IssueCA_CongYeOfd));
                //    tSend.Start();
                //    break;
                case "CongYe_ZhiAnDataCheck"://从业人员证书质安网数据校验（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(CongYe_ZhiAnDataCheck));
                    tSend.Start();
                    break;
                //case "CongYe_GetQRCode"://从业人员证书获取二维码（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(CongYe_GetQRCode));
                //    tSend.Start();
                //    break;
                //case "CongYe_UpCertUrl"://从业人员证书归集电子证书（部署在192.168.150.175)
                //    tSend = new Thread(new ThreadStart(CongYe_UpCertUrl));
                //    tSend.Start();
                //    break;
                case "CongYe_CreateGuoBiaoPdf": //从业人员证书创建国标电子证书
                    tSend = new Thread(new ThreadStart(CongYe_CreateGuoBiaoPdf));
                    tSend.Start();
                    break;
                case "CongYe_FaceImg"://为从业人员证书赋码准备免冠照片（部署在21上）
                    tSend = new Thread(new ThreadStart(CongYe_FaceImg));
                    tSend.Start();
                    break;
                case "MoveHisCAFile"://移动过期电子证书到备份服务器（部署在21上）
                    tSend = new Thread(new ThreadStart(MoveHisCAFile));
                    tSend.Start();
                    break;
                case "NewSheBaoCheck"://新版社保比对（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(NewSheBaoCheck));
                    tSend.Start();
                    break;
                case "ErJian_UseCreate"://二建电子证书使用件创建与取回（部署在21上)
                    tSend = new Thread(new ThreadStart(ErJian_UseCreate));
                    tSend.Start();
                    break;
                case "ErJian_UseCA"://二建电子证书使用件签章（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(ErJian_UseCA));
                    tSend.Start();
                    break;
                case "GetCertOut"://获取外省转出证书，供本省转入证书信息（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(GetCertOut));
                    tSend.Start();
                    break;
                case "ApplyGather"://新版办件汇聚（部署在192.168.150.175)
                    tSend = new Thread(new ThreadStart(ApplyGather));
                    tSend.Start();
                    break;
                    
            }
        }

        #region 新版国标电子证书

        //***********************************
        //国标电子证书生成流程：
        //1、调用数据校验接口；
        //2、准备base64个人照片；
        //3、调用二维码赋码接口；
        //4、生成电子证书；
        //5、复制电子证书到192.168.150.175签章系统；
        //6、pdf签章；
        //7、ofd签章；
        //8、取回签章后的电子证书到人员系统；
        //9、调用电子证书归集接口
        //***********************************


        #region 质安网测试接口地址

        ////质安网测试接口公钥
        //string gb_publicKeyJava = "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAKGMcexIV6n+A+Tv/L731RWLZQDAPPCR9Uu93fcZO/v0mXI2xhbXAr43T6jj99I/ILVvdw8vRyRTTfZeDk+8gSMCAwEAAQ==";
        ////质安网测试接口私钥
        //string gb_privateKeyJava = "MIIBVAIBADANBgkqhkiG9w0BAQEFAASCAT4wggE6AgEAAkEAopLOuVRtAECI9pA8KQW66yReZPsf1y/fq8capxdQmZcViOagR//MyvvRNZ1jie3XkeP2BmolaYPaaMzJCfFpzQIDAQABAkARqYFsARAWhuxaXTEsA8Na6IiYz++VoU3bi7YJkS7ggKzuMypWab1HysNbzBNK9vHszYkvqotD1SNsxRixADDBAiEA46D2rQ52R8qyMqABCV3bu+5m8epNlprflvEb2fnIDhcCIQC21hmjfdlZzMxh8a0RbwKv7STIYZYf1hsV86U7WzQ5uwIgXI9CXyJnVFAG3/ESGtXwmN2bPLmSrS/yxTTrp1obUCcCIHOVwFmaGsjpbp/Qn/+wdTtwqNtAzh5MRY1IHUH783U3AiEAgZSgNM+c8LYLjyjK9y21FcQ72gcxsJHJyMOCaidBwAE=";

        ///// <summary>
        ///// 获取质安网登录Token Url
        ///// </summary>
        //private const string Url_Token = "http://219.142.101.192/epoint-sso-web/rest/oauth2/token";
        ///// <summary>
        ///// 安管人员：校验接口url
        ///// </summary>
        //private const string Url_Agry_Check = "http://219.142.101.192/share/epoint-web-dzzz/rest/agrycheckrest/Agry_Check";
        ///// <summary>
        ///// 安管人员：赋码接口url
        ///// </summary>
        //private const string Url_Agry_Accept = "http://219.142.101.192/share/epoint-web-dzzz/rest/agryrest/Agry_Accept";
        ///// <summary>
        ///// 安管人员：归集接口url
        ///// </summary>
        //private const string Url_Agry_DzzGjAccept = "http://219.142.101.192/share/epoint-web-dzzz/rest/agryrest/Agry_DzzGjAccept";
        ///// <summary>
        ///// 安管人员：数据更新接口url
        ///// </summary>
        //private const string Url_Agry_Update = "http://219.142.101.192/share/epoint-web-dzzz/rest/agryrest/Agry_Update";
        ///// <summary>
        ///// 安管人员: 变更身份等关键数据接口url
        ///// </summary>
        //private const string Url_Agry_Change = "http://219.142.101.192/share/epoint-web-dzzz/rest/agryrest/Agry_Change";



        ////特种作业接口地址：
        ///// <summary>
        ///// 特种作业：校验接口url
        ///// </summary>
        //private const string Url_Tzry_Check = "http://219.142.101.192/share/epoint-web-dzzz/rest/tzrycheckrest/Tzry_Check";
        ///// <summary>
        ///// 特种作业：赋码接口url
        ///// </summary>
        //private const string Url_Tzry_Accept = "http://219.142.101.192/share/epoint-web-dzzz/rest/tzryrest/Tzry_Accept";
        ///// <summary>
        ///// 特种作业：归集接口url
        ///// </summary>
        //private const string Url_Tzry_DzzGjAccept = "http://219.142.101.192/share/epoint-web-dzzz/rest/tzryrest/Tzry_DzzGjAccept";
        ///// <summary>
        ///// 特种作业：数据更新接口url
        ///// </summary>
        //private const string Url_Tzry_Update = "http://219.142.101.192/share/epoint-web-dzzz/rest/tzryrest/Tzry_Update";
        ///// <summary>
        ///// 特种作业: 变更身份等关键数据接口url
        ///// </summary>
        //private const string Url_Tzry_Change = "http://219.142.101.192/share/epoint-web-dzzz/rest/tzryrest/Tzry_Change";

        #endregion 质安网测试接口地址

        #region 质安网正式接口地址(政务网)

        //质安网正式接口公钥       
        string gb_publicKeyJava = "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAKGMcexIV6n+A+Tv/L731RWLZQDAPPCR9Uu93fcZO/v0mXI2xhbXAr43T6jj99I/ILVvdw8vRyRTTfZeDk+8gSMCAwEAAQ==";

        //质安网正式接口私钥        
        string gb_privateKeyJava = "MIIBUwIBADANBgkqhkiG9w0BAQEFAASCAT0wggE5AgEAAkEAqIOoAv7SX3J7WGUc9gCIR0KdD9Qax/x1AUyDWRLzPDTn5CVwy0hexE4J0okhMhp0swBMd75eHxLLp/uOj+1pIQIDAQABAkAXeyDyKefOfPlK++vpK3HR3CpehkOlEjyLjcnPo6BZBxvw+rUHc4To2I5lWIE69neiS55ZrryXA073O+kFLmtxAiEA6YIlKMDkMDxMWta3fhFGSFyj5X27yDj8ZflRiRawSxsCIQC4vuKfOD0JhGCRxpYiag9rOQDFd+e41dS+a6PY0GHEcwIgfLVmFRrXg69gdA9CllCxgYoAelspQ+izW97InfeBFcsCIBFzBhifyTQS6sQaC6goeFhaQ/ZiwuI0YCm0JP+ffvx9AiALEOz9sN9Fb2ttvQdcbeEd0MCCMdLos72ReerdSmQFIw==";

        /// <summary>
        /// 获取质安网登录Token Url
        /// </summary>
        private const string Url_Token = "http://59.255.8.217/epoint-soa-web/rest/oauth2/token";

        /// <summary>
        /// 安管人员：校验接口url
        /// </summary>
        private const string Url_Agry_Check = "http://59.255.8.217/epoint-gateway/agrycheck/rest/agrycheckrest/Agry_Check";

        /// <summary>
        /// 安管人员：赋码接口url
        /// </summary>
        private const string Url_Agry_Accept = "http://59.255.8.217/epoint-gateway/agryfm/rest/aqscglryrest/agry_fm";
                                                
        /// <summary>
        /// 安管人员：归集接口url
        /// </summary>
        private const string Url_Agry_DzzGjAccept = "http://59.255.8.217/epoint-gateway/agryhj/rest/aqscglryrest/agry_gj";

        /// <summary>
        /// 安管人员：证书状态更新接口url
        /// </summary>
        private const string Url_Agry_Update = "http://59.255.8.217/epoint-gateway/agryfm/rest/aqscglryrest/agry_update";
                                                
        /// <summary>
        /// 安管人员: 证件号码变更临时接口url
        /// </summary>
        private const string Url_Agry_Change = "http://59.255.8.217/epoint-gateway/agryfm/rest/agryrest/Agry_Change";

        /// <summary>
        /// 安管人员: 一人多本证书同时变更数据接口url（批量变更单位接口）
        /// </summary>
        private const string Url_Agry_PathChangeUnit = "http://59.255.8.217/epoint-gateway/agryfm/rest/aqscglryrest/agry_fmplbgdw";

        /// <summary>
        /// 安管人员：业务数据更正接口（1.0）
        /// </summary>
        private const string Url_Agry_UpdateData = "http://59.255.8.217/epoint-gateway/agryupdate/rest/agryrest/Agry_Update";

        /// <summary>
        /// 安管人员：办理转出证照查询接口（查询外省已转出证书编号等信息）
        /// </summary>
        private const string Url_Agry_CheckOut = "http://59.255.8.217/epoint-gateway/agryfm/rest/aqscglryrest/agry_checkout";
        
        /// <summary>
        /// 安管人员：办理跨省转入证照查询接口（查询本省转出证书在外省转入后分配的新证书信息）
        /// </summary>
        private const string Url_Agry_CheckIn = "http://59.255.8.217/epoint-gateway/agryfm/rest/aqscglryrest/agry_transferinquery";


        //特种作业接口地址：
        /// <summary>
        /// 特种作业：校验接口url
        /// </summary>
        private const string Url_Tzry_Check = "http://59.255.8.217/epoint-gateway/tzrycheck/rest/tzryzsrest/tzry_check";

        /// <summary>
        /// 特种作业：赋码接口url
        /// </summary>
        private const string Url_Tzry_Accept = "http://59.255.8.217/epoint-gateway/tzryfm/rest/tzryzsrest/tzry_fm";

        /// <summary>
        /// 特种作业：归集接口url
        /// </summary>
        private const string Url_Tzry_DzzGjAccept = "http://59.255.8.217/epoint-gateway/tzryhj/rest/tzryzsrest/tzry_gj";

        /// <summary>
        /// 特种作业：证书状态更新接口url
        /// </summary>
        private const string Url_Tzry_Update = "http://59.255.8.217/epoint-gateway/tzryfm/rest/tzryzsrest/tzry_update";

        /// <summary>
        /// 特种作业: 变更身份等关键数据接口url
        /// </summary>
        private const string Url_Tzry_Change = "http://59.255.8.217/epoint-gateway/tzryfm/rest/tzryrest/Tzry_Change";

        /// <summary>
        /// 特种作业:业务数据更正接口（1.0）
        /// </summary>
        private const string Url_Tzry_UpdateData = "http://59.255.8.217/epoint-gateway/tzryupdate/rest/tzryrest/Tzry_Update";



        #endregion 质安网正式接口地址

        /// <summary>
        /// 质安网数据共享接口登录
        /// </summary>
        /// <returns>登录是否成功</returns>
        private bool GBLogin(string url)
        {
            try
            {
                //登录
                if (gb_accessToken == "" || gb_expiresTime < DateTime.Now || gb_accessToken_tz == "" || gb_expiresTime_tz < DateTime.Now)
                {
                    gb.Login.ResponseResult resultSLR = gb.Login.Core.Login(url, WS_GetData.gb.Login.CertType.三类人);
                    gb.Login.ResponseResult resultTZZY = gb.Login.Core.Login(url, WS_GetData.gb.Login.CertType.特种作业);
                    if (resultSLR.Status.Code == "1" && resultTZZY.Status.Code == "1")
                    {
                        gb_accessToken = resultSLR.Custom.AccessToken;
                        gb_expiresTime = DateTime.Now.AddSeconds(Convert.ToInt64(resultSLR.Custom.ExpiresIn)).AddMinutes(-5);

                        gb_accessToken_tz = resultTZZY.Custom.AccessToken;
                        gb_expiresTime_tz = DateTime.Now.AddSeconds(Convert.ToInt64(resultTZZY.Custom.ExpiresIn)).AddMinutes(-5);
                        //FileLog.WriteLog("登录全国工程质量安全监管信息平台电子证照归集共享系统成功。");//-------------------------------------
                        return true;
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("登录全国工程质量安全监管信息平台电子证照归集共享系统失败。三类人code={0}，特种作业code={1}", resultSLR.Status.Code, resultTZZY.Status.Code));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog(string.Format("登录全国工程质量安全监管信息平台电子证照归集共享系统失败。{0}", ex.Message), ex);
                return false;
            }
        }
               
        //从业人员证书质安网数据校验（部署在192.168.150.175)
        private void CongYe_ZhiAnDataCheck()
        {
            ////*******测试*******************
            //ZhiAnFunStartTime = DateTime.Now;
            //test_CongYe_GetQRCode(ZhiAnFunStartTime);
            //return;
            ////**************************************


            //质安网接口调用开始时间
            ZhiAnFunStartTime = DateTime.Now;

            //质安网接口调用开始序号
            switch (ZhiAnFunDoStepNo)
            {
                case 1:
                    ZhiAnFunDoStepNo = 2;
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    //CongYe_SetCertState99(ZhiAnFunStartTime);//挂起一人多证ABC
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //UpdataZAData(ZhiAnFunStartTime);//质量安全原网变更已上传失效数据接口
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    break;
                case 2:
                    ZhiAnFunDoStepNo = 3;                    
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    break;
                case 3:
                    ZhiAnFunDoStepNo = 4;
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    break;
                case 4:
                    ZhiAnFunDoStepNo = 5;                  
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    break;
                case 5:
                    ZhiAnFunDoStepNo = 6;
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    break;
                case 6:
                    ZhiAnFunDoStepNo = 7;                    
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    break;
                case 7:
                    ZhiAnFunDoStepNo = 8;                    
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期             
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    break;
                case 8:
                    ZhiAnFunDoStepNo = 9;                    
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码            
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    break;
                case 9:
                    ZhiAnFunDoStepNo = 1;                    
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期             
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    break;
                default:
                    CongYe_PatchChangeUnitGetQRCode(ZhiAnFunStartTime);//多证ABC同时变更单位赋码
                    ChangeIDCard(ZhiAnFunStartTime);//质量安全网变更身份证号接口
                    CongYe_UpdataCertStatus(ZhiAnFunStartTime);//修改证书状态：用于注销、离京、过期、暂扣、还发
                    //Certificate_ZhiAnDataCheck(ZhiAnFunStartTime);//从业人员电子证书生成前检查：全国证书查重   
                    CongYe_GetQRCode(ZhiAnFunStartTime);//从业人员证书获取二维码   
                    CongYe_UpCertUrl(ZhiAnFunStartTime);//从业人员证书归集电子证书
                    CertificateEnter_ZhiAnDataCheck(ZhiAnFunStartTime);//三类人证书进京企业确认前检查：全国证书查重
                    CertificateChange_ZhiAnDataCheck(ZhiAnFunStartTime);//变更
                    CertificateContinue_ZhiAnDataCheck(ZhiAnFunStartTime);//续期
                    ExamSignup_ZhiAnDataCheck(ZhiAnFunStartTime);//考试报名企业确认前检查：全国证书查重                                              
                    break;
            }
        }

        //从业人员电子证书签章：全国证书查重（三类人、特种作业）
        private void Certificate_ZhiAnDataCheck(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书签章全国证书查重Certificate_ZhiAnDataCheck()");
            //建筑施工特种作业
            //1 所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
            //2 身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
            //3 工种类别	operationCategory	字符型	2	是	详见7.2.3.2工种类别字典表 
            //4 证书编号	certNum	字符型	25	否	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成


            //建筑施工企业安全生产管理人员
            //1	所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
            //2	身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
            //3	岗位类别代码	categoryCode	字符型	2	是	详见7.3.3.2岗位类别字典表
            //4	统一社会信用代码	creditCode	字符型	18	是	持该证书的安管人员受聘企业的统一社会信用代码
            //5	证书编号	certNum	字符型	23	是	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成


            //            string sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],u.CreditCode
            //                                                      FROM [dbo].[CERTIFICATE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
            //                                                      where c.posttypeid <3   and c.VALIDENDDATE > getdate() and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
            //                                                      and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE]) 
            //                                                      and c.CERTIFICATEID < {1}
            //                                                      order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"]);

            //***********临时方案，只处理三类人

            //c.posttypeid <3 and ((c.VALIDENDDATE> dateadd(year,-3,getdate()) and c.VALIDENDDATE < getdate()) or c.VALIDENDDATE > getdate())  and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 10)
            {
                sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],c.UNITCODE,
                                                    c.[Job],c.WORKERNAME,c.[STATUS],c.VALIDENDDATE,u.CreditCode,i.CERTIFICATECODE as oldcertNum
                                        FROM [dbo].[CERTIFICATE] c 
                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        left join [dbo].[CERTIFICATEENTERAPPLY] i on c.[CERTIFICATEID] = i.[CERTIFICATEID]
                                        where c.posttypeid <3   
                                        and (
                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')                                                         
                                            )
                                        and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE] or (dateadd(day,1,c.[ZACheckTime])<getdate() and c.ZACheckResult=0)) 
                                        and c.CERTIFICATEID < {1}
                                        and ((c.EleCertErrStep ='{2}' and c.EleCertErrTime <DATEADD(hour,-24, GETDATE())) or c.EleCertErrTime is null or c.EleCertErrStep <>'{2}')
                                        order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"], EnumManager.EleCertDoStep.CertCheck);
            }
            else
            {
                sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],c.UNITCODE,
                                                    c.[Job],c.WORKERNAME,c.[STATUS],c.VALIDENDDATE,u.CreditCode,i.CERTIFICATECODE as oldcertNum
                                        FROM [dbo].[CERTIFICATE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        left join [dbo].[CERTIFICATEENTERAPPLY] i on c.[CERTIFICATEID] = i.[CERTIFICATEID]
                                        where c.posttypeid <3   
                                        and (
                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')                                                           
                                            )
                                        and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE] or (dateadd(day,1,c.[ZACheckTime])<getdate() and c.ZACheckResult=0)) 
                                        and (c.EleCertErrTime <DATEADD(day,-1, GETDATE()) or c.EleCertErrTime is null)
                                        and ((c.EleCertErrStep ='{2}' and c.EleCertErrTime <DATEADD(hour,-24, GETDATE())) or c.EleCertErrTime is null or c.EleCertErrStep <>'{2}')
                                        and c.CERTIFICATEID < {1}
                                        order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"], EnumManager.EleCertDoStep.CertCheck);
            }


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            gb.Verification.ResponseResult result = null;

            if (GBLogin(Url_Token) == false) return;

            ////////*****************测试***************************************
            //if (DateTime.Now < Convert.ToDateTime("2023-03-18"))
            //{
            //    try
            //    {
            //        string tempUrl = "http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check";
            //        result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
            //                            new List<gb.Verification.Aqscglry.AcceptData>
            //                {
            //                    new gb.Verification.Aqscglry.AcceptData
            //                    {
            //                        ProvinceNum = "110000",
            //                        IdentityCard = "372928199710044158",
            //                        CategoryCode = CertificateDAL.GetSLRPostCode("综合类专职安全生产管理人员"),
            //                        CreditCode= "91110113MA0194JY7H",
            //                        CertNum = "京建安C3（2021）0022585"
            //                    }
            //                }, tempUrl);

            //        FileLog.WriteLog(string.Format("调用加入白名单的数据校验测试http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check接口成功。返回码{0}", result.ReturnCode));
            //    }
            //    catch (Exception ex)
            //    {
            //        FileLog.WriteLog("调用加入白名单的数据校验测试http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check接口失败，错误信息：" + ex.Message, ex);
            //    }
            //}

            /////////********************************************************///////////////////////////////


            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CongYe_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CongYe_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
                                new List<gb.Verification.Aqscglry.AcceptData>
                            {
                                new gb.Verification.Aqscglry.AcceptData
                                {
                                    eCertID=  Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    CreditCode= dr["CreditCode"].ToString(),
                                    CertNum = dr["CERTIFICATECODE"].ToString(),
                                    operateType = (dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout?"05"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing?"05"
                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
                                                    //:dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
                                                    //:dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"08"                                                    
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.ChangeInBeiJing?"09":""),
                                    appointment=CertificateDAL.GetAppointmentCode(dr),
                                    oldcertNum=(dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?dr["oldcertNum"].ToString():"")
                                }
                            }, Url_Agry_Check);
                            break;
                        case "2"://特种作业
                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
                                new List<gb.Verification.Tzzy.AcceptData>
                            {
                                new gb.Verification.Tzzy.AcceptData
                                {
                                   eCertID =Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    categoryDescription=(CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    operateType=(dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout?"05"         
                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.ChangeInBeiJing?"08":""),
                                    CertNum = dr["CERTIFICATECODE"].ToString()
                                }
                            }, Url_Tzry_Check);
                            break;
                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));//更新数据验证时间
                        FileLog.WriteLog(string.Format("校验证书{0}成功。", dr["CERTIFICATECODE"]));
                    }
                    else if (result.ReturnCode == "0")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={3},[ZACheckRemark]='{1}',[EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                            , dr["CERTIFICATEID"]
                            , result.ReturnData.ErrorData[0].ErrorMsg
                            , EnumManager.EleCertDoStep.CertCheck
                             , result.ReturnCode
                            ));//更新电子证书生成结果

                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验"
                            , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
                            );

                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={1},[ZACheckRemark]='{2}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where [CERTIFICATEID]={0}"
                            , dr["CERTIFICATEID"]
                            , result.ReturnCode
                            , result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : ""
                            ));//更新数据验证时间                        
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("校验失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验", ex.Message);
                    continue;
                }
            }
        }

        //考试报名：全国证书查重（三类人、特种作业）
        private void ExamSignup_ZhiAnDataCheck(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员考试报名全国证书查重ExamSignup_ZhiAnDataCheck()");
            //企业尚未提交或已驳回，并且在报名期间或企业审核期间（未校验或校验未通过）每天一次，或准考证发放开始前5天（只有1天，这一天全部重新检查一遍）
            string sql = String.Format(@"select top {0} c.[EXAMSIGNUPID],c.[POSTTYPEID],c.[POSTNAME],c.[CERTIFICATECODE] as WORKERCERTIFICATECODE,c.job,u.CreditCode
                                          FROM [dbo].[VIEW_EXAMSIGNUP_NEW] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                          where c.posttypeid <3 and c.[CreateTime] > '2023-02-14' 
                                          and (
		                                        (
			                                        (c.[Status]='未提交' or  c.[Status]='退回修改' or  c.[Status]='待初审')
			                                        and (
					                                        (c.[ZACheckTime] is null and c.SIGNUPENDDATE > DATEADD(day,-3, GETDATE()))
					                                        or (c.ZACheckResult = 0 and c.[ZACheckTime] < DATEADD(hour,-24, GETDATE()) and c.SIGNUPENDDATE > DATEADD(day,-3, GETDATE()))
			                                         )  
		                                        )    
		                                        or
		                                        (
			                                        (c.[Status]='已审核' or  c.[Status]='已受理') and c.[EXAMCARDSENDSTARTDATE]= Convert(varchar(10),dateadd(day,5,getdate()),21)  and c.[ZACheckTime] < Convert(varchar(10),dateadd(day,5,getdate()),21)			
		                                        )         
	                                        )
                                          and c.EXAMSIGNUPID < {1}                             
                                          order by c.EXAMSIGNUPID desc  ", MaxCountExe, cursor_long["cursor_ExamSignup_ZhiAnDataCheck"]);


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_ExamSignup_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_ExamSignup_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["EXAMSIGNUPID"]);//记录扫描位置
            }

            gb.Verification.ResponseResult result = null;
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
                                new List<gb.Verification.Aqscglry.AcceptData>
                            {
                                new gb.Verification.Aqscglry.AcceptData
                                {
                                    eCertID=  Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    CreditCode= dr["CreditCode"].ToString(),
                                    CertNum = "",
                                    operateType="01",//新发
                                    appointment=CertificateDAL.GetAppointmentCode(dr),
                                    oldcertNum=""
                                }
                            }, Url_Agry_Check);
                            break;
                        case "2"://特种作业
                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
                                new List<gb.Verification.Tzzy.AcceptData>
                            {
                                new gb.Verification.Tzzy.AcceptData
                                {
                                    eCertID =Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    categoryDescription=(CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    operateType="01",//新发
                                    CertNum = ""
                                }
                            }, Url_Tzry_Check);
                            break;
                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[ExamSignUp] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [EXAMSIGNUPID]={0}", dr["EXAMSIGNUPID"]));//更新数据验证时间
                        //FileLog.WriteLog(string.Format("{0}考试报名{1}质安网校验成功。", dr["WORKERCERTIFICATECODE"],dr["POSTNAME"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[ExamSignUp] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [EXAMSIGNUPID]={0}"
                            , dr["EXAMSIGNUPID"]
                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
                             , result.ReturnCode
                            ));//更新数据验证时间
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("考试报名质安网查重校验失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        //三类人证书进京：全国证书查重（三类人）
        private void CertificateEnter_ZhiAnDataCheck(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书进京全国证书查重CertificateEnter_ZhiAnDataCheck()");

            //企业已经审核，尚未质安网查重
            string sql = String.Format(@"select top {0} A.APPLYID, A.POSTTYPEID,  N2.POSTNAME,A.WORKERCERTIFICATECODE,A.[Job],A.UNITCODE,A.WORKERNAME,A.[CERTIFICATECODE],u.CreditCode 
                                        FROM DBO.CERTIFICATEENTERAPPLY A
                                        inner join Unit u on A.UNITCODE = u.ENT_OrganizationsCode
                                        LEFT JOIN dbo.POSTINFO N2 ON A.POSTID = N2.POSTID
                                        where  A.[ApplyDate] >'2023-02-14' and A.ApplyStatus='{2}' 
                                        and (A.[ZACheckTime] is null or (A.[ZACheckResult]=0 and A.[ApplyDate] > dateadd(month,-1,getdate()) and A.[ZACheckTime] < DATEADD(hour,-72, GETDATE())))
                                        and A.APPLYID < {1}                             
                                        order by A.APPLYID desc  ", MaxCountExe, cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"], EnumManager.CertificateEnterStatus.WaitUnitCheck);


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["APPLYID"]);//记录扫描位置
            }

            gb.Verification.ResponseResult result = null;
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
                                new List<gb.Verification.Aqscglry.AcceptData>
                            {
                                new gb.Verification.Aqscglry.AcceptData
                                {
                                    eCertID=  Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    CreditCode= dr["CreditCode"].ToString(),
                                    CertNum = "",
                                    operateType="08",//转入
                                    appointment=CertificateDAL.GetAppointmentCode(dr),
                                    oldcertNum=dr["CERTIFICATECODE"].ToString()
                                }
                            }, Url_Agry_Check);
                            break;
                        default:
                            continue;
                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATEENTERAPPLY] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [APPLYID]={0}", dr["APPLYID"]));//更新数据验证时间
                        FileLog.WriteLog(string.Format("{0}{1}证书进京质安网校验成功。", dr["WORKERCERTIFICATECODE"], dr["POSTNAME"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATEENTERAPPLY] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [APPLYID]={0}"
                            , dr["APPLYID"]
                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
                             , result.ReturnCode
                            ));//更新数据验证时间
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("证书进京质安网校验失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        //从业人员证书变更：全国证书查重（三类人、特种作业）
        private void CertificateChange_ZhiAnDataCheck(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书变更全国证书查重CertificateChange_ZhiAnDataCheck()");

            //企业已经审核，尚未质安网查重
            string sql = String.Format(@"select top {0} A.[CERTIFICATECHANGEID],A.POSTTYPEID,A.POSTNAME,A.[NEWWORKERCERTIFICATECODE],u.CreditCode ,A.[Job],A.NEWUNITCODE as UNITCODE,A.WORKERNAME,A.[CERTIFICATECODE]
                                        FROM DBO.[VIEW_CERTIFICATECHANGE] A
                                        inner join Unit u on A.[NEWUNITCODE] = u.ENT_OrganizationsCode                                       
                                        where  A.[ApplyDate] >'2023-03-10' and (A.[Status]='{2}' or A.[Status]='{3}') 
                                        and (A.[ZACheckTime] is null or (A.[ApplyDate] > A.[ZACheckTime] and A.[ZACheckResult]=0))
                                        and A.CHANGETYPE='京内变更' 
                                        and A.CERTIFICATECHANGEID < {1}                             
                                        order by A.CERTIFICATECHANGEID desc  ", MaxCountExe, cursor_long["cursor_CertificateChange_ZhiAnDataCheck"], EnumManager.CertificateChangeStatus.WaitUnitCheck, EnumManager.CertificateChangeStatus.Applyed);


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CertificateChange_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CertificateChange_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CERTIFICATECHANGEID"]);//记录扫描位置
            }

            gb.Verification.ResponseResult result = null;
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
                                new List<gb.Verification.Aqscglry.AcceptData>
                            {
                                new gb.Verification.Aqscglry.AcceptData
                                {
                                    eCertID=  Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["NEWWORKERCERTIFICATECODE"].ToString(),
                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    CreditCode= dr["CreditCode"].ToString(),
                                    CertNum = dr["CERTIFICATECODE"].ToString(),
                                    operateType="09",//变更
                                    appointment=CertificateDAL.GetAppointmentCode(dr),
                                    oldcertNum=""
                                }
                            }, Url_Agry_Check);
                            break;
                        case "2"://特种作业
                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
                                new List<gb.Verification.Tzzy.AcceptData>
                            {
                                new gb.Verification.Tzzy.AcceptData
                                {
                                    eCertID =Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["NEWWORKERCERTIFICATECODE"].ToString(),
                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    categoryDescription=(CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    operateType="09",//变更
                                    CertNum = dr["CERTIFICATECODE"].ToString()
                                }
                            }, Url_Tzry_Check);
                            break;
                        default:
                            continue;
                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECHANGE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [CERTIFICATECHANGEID]={0}", dr["CERTIFICATECHANGEID"]));//更新数据验证时间
                        FileLog.WriteLog(string.Format("{0}{1}证书变更质安网校验成功。", dr["NEWWORKERCERTIFICATECODE"], dr["POSTNAME"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECHANGE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [CERTIFICATECHANGEID]={0}"
                            , dr["CERTIFICATECHANGEID"]
                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
                             , result.ReturnCode
                            ));//更新数据验证时间
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("从业人员证书变更质安网校验失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        //从业人员证书续期全国证书查重（三类人、特种作业）
        private void CertificateContinue_ZhiAnDataCheck(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书续期全国证书查重CertificateContinue_ZhiAnDataCheck()");
            //建筑施工特种作业
            //1 所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
            //2 身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
            //3 工种类别	operationCategory	字符型	2	是	详见7.2.3.2工种类别字典表 
            //4 证书编号	certNum	字符型	25	否	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成


            //建筑施工企业安全生产管理人员
            //1	所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
            //2	身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
            //3	岗位类别代码	categoryCode	字符型	2	是	详见7.3.3.2岗位类别字典表
            //4	统一社会信用代码	creditCode	字符型	18	是	持该证书的安管人员受聘企业的统一社会信用代码
            //5	证书编号	certNum	字符型	23	是	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成

            string sql = String.Format(@"select top {0} x.[CERTIFICATECONTINUEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],
                                                c.[WORKERCERTIFICATECODE],u.CreditCode,c.[Job],c.WORKERNAME,x.NEWUNITCODE as UNITCODE
                                        FROM DBO.CERTIFICATE c
                                        inner JOIN DBO.CERTIFICATECONTINUE x on c.CERTIFICATEID = x.CERTIFICATEID
                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        where c.posttypeid <3  and x.[ApplyDate] >'2023-09-28' and (x.[ZACheckTime] is null or (x.[ApplyDate] > x.[ZACheckTime] and x.[ZACheckResult]=0))
                                        and c.CERTIFICATEID < {1}
                                        order by x.CERTIFICATECONTINUEID desc  ", MaxCountExe, cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"]);



            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            gb.Verification.ResponseResult result = null;

            if (GBLogin(Url_Token) == false) return;


            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }

            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"] = Convert.ToInt64(dr["CERTIFICATECONTINUEID"]);//记录扫描位置

                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
                                new List<gb.Verification.Aqscglry.AcceptData>
                            {
                                new gb.Verification.Aqscglry.AcceptData
                                {
                                    eCertID=  Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    CreditCode= dr["CreditCode"].ToString(),
                                    CertNum = dr["CERTIFICATECODE"].ToString(),
                                    operateType="02",//延续
                                    appointment=CertificateDAL.GetAppointmentCode(dr),
                                    oldcertNum=""
                                }
                            }, Url_Agry_Check);
                            break;
                        case "2"://特种作业
                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
                                new List<gb.Verification.Tzzy.AcceptData>
                            {
                                new gb.Verification.Tzzy.AcceptData
                                {
                                    eCertID =Guid.NewGuid().ToString(),
                                    ProvinceNum = "110000",
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    categoryDescription=(CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    operateType="02",//延续
                                    CertNum = dr["CERTIFICATECODE"].ToString()
                                }
                            }, Url_Tzry_Check);
                            break;
                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [CERTIFICATECONTINUEID]={0}", dr["CERTIFICATECONTINUEID"]));//更新数据验证时间
                        FileLog.WriteLog(string.Format("校验证书{0}续期申请成功。", dr["CERTIFICATECODE"]));
                    }
                    else if (result.ReturnCode == "0")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [CERTIFICATECONTINUEID]={0}"
                            , dr["CERTIFICATECONTINUEID"]
                            , result.ReturnData.ErrorData[0].ErrorMsg
                             , result.ReturnCode
                            ));//更新电子证书生成结果
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={1},[ZACheckRemark]='{2}' where [CERTIFICATECONTINUEID]={0}"
                            , dr["CERTIFICATECONTINUEID"]
                            , result.ReturnCode
                            , result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : ""
                            ));//更新数据验证时间                        
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("证书续期全国证书查重校验失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验", ex.Message);
                    continue;
                }
            }
        }

        //业务数据赋码接口：从业人员证书获取二维码（部署在192.168.150.175)
        private void CongYe_GetQRCode(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书获取二维码CongYe_GetQRCode()");

            //加快批量变更
            CommonDAL.ExecSQL(@"update [dbo].[CERTIFICATE]	
                                set EleCertErrTime =null
                                where  EleCertErrTime < CHECKDATE;

                                update certificate 
                                set EleCertErrTime=null
                                where postid=1125 and REMARK like '双证合并%' and EleCertErrTime is not null and STATUS <> '注销' and STATUS <> '离京变更' and EleCertErrDesc like '%已存在%' and EleCertErrTime < dateadd(hour,-1,getdate())
                                and exists(select 1 from certificate c where c.WORKERCERTIFICATECODE = certificate.WORKERCERTIFICATECODE and c.postid=6 and c.STATUS='注销' and c.QRCodeTime > c.CHECKDATE and c.REMARK like '双证合并注销%')
                                and exists(select 1 from certificate c where c.WORKERCERTIFICATECODE = certificate.WORKERCERTIFICATECODE and c.postid=1123 and c.STATUS='注销' and c.QRCodeTime > c.CHECKDATE and c.REMARK like '双证合并注销%');");

            try
            {
                string sql = "";
                if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
                {

                    sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
                                            FROM [dbo].[CERTIFICATE] c 
                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            where c.posttypeid < 3   
                                            and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                    and (c.[QRCodeTime] is null or c.[QRCodeTime] < c.[CHECKDATE])
		                                    and c.[ZAFaceImgTime] > c.[CHECKDATE]                                              
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null                                               
                                            and p.[PauseID] is null
                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_GetQRCode"]);
                }
                else
                {
                    sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
                                            FROM [dbo].[CERTIFICATE] c 
                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            where c.posttypeid <3   
                                            and  c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                    and (c.[QRCodeTime] is null or c.[QRCodeTime] < c.[CHECKDATE])
		                                    and c.[ZAFaceImgTime] > c.[CHECKDATE]
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null
                                            and p.[PauseID] is null
                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_GetQRCode"]);
                }

                DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
                //FileLog.WriteLog(string.Format("dtOriginal={0}",dtOriginal.Rows.Count));//-----------------------------
                if (GBLogin(Url_Token) == false) return;

                if (dtOriginal.Rows.Count < MaxCountExe)
                {
                    cursor_long["cursor_CongYe_GetQRCode"] = long.MaxValue - 1;//记录扫描位置

                    if (dtOriginal.Rows.Count == 0)
                    {
                        return;
                    }
                }
                else
                {
                    cursor_long["cursor_CongYe_GetQRCode"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                }

                gb.Imputation.ResponseResult result = null;
                string _ECertID = null;//电子证书ID
                int i = 0;
                foreach (DataRow dr in dtOriginal.Rows)
                {
                    //FileLog.WriteLog(string.Format("ZhiAnFunStartTime={0},startTime={1}",ZhiAnFunStartTime, startTime));//-----------------------------
                    if (ZhiAnFunStartTime != startTime) return;
                    if (i % 5 == 0)
                    {
                        Thread.Sleep(1000);//暂停1秒
                    }
                    i += 1;

                    //正式
                    _ECertID = Guid.NewGuid().ToString();
                    //if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
                    //                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
                    //                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
                    //                )
                    //{
                    //    _ECertID = dr["CertificateCAID"].ToString();//原电子证书ID
                    //}
                    //else
                    //{
                    //    _ECertID = Guid.NewGuid().ToString();//电子证书ID，会写入证书表（生成电子证书是不再创建）
                    //}

                    ////测试
                    //_ECertID = dr["CertificateCAID"].ToString();//电子证书ID，测试时读取上次电子证书ID

                    try
                    {
                        //FileLog.WriteLog(string.Format("准备赋码：CERTIFICATECODE：{0}", dr["CERTIFICATECODE"]));

                        switch (dr["PostTypeID"].ToString())
                        {
                            case "1"://三类人
                                result = gb.Imputation.Core<gb.Imputation.Aqscglry.AcceptData>.Imputation(gb_accessToken,
                                new List<gb.Imputation.Aqscglry.AcceptData>
                            {
                                new gb.Imputation.Aqscglry.AcceptData
                                {
                                    ECertID = _ECertID.Replace("-",""),
                                    ProvinceNum = "110000",
                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
                                    IssuAuth = "北京市住房和城乡建设委员会",
                                    IssuAuthCode = "11110000000021135M",
                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
                                    CategoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    Name = dr["WORKERNAME"].ToString(),
                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
                                    BirthDate=Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
                                    CompanyName =dr["UNITNAME"].ToString(),
                                    CreditCode=dr["CreditCode"].ToString(),
                                    Appointment=CertificateDAL.GetAppointmentCode(dr),
                                    TechnicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
                                    EducationDegree=CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"] == DBNull.Value?"":dr["CULTURALLEVEL"].ToString()),
                                    Major=dr["PostName"].ToString(),
                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))),
                                    //01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
                                    CertState = CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
                                    CertStatusDescription = CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
                                    AssociatedZzeCertID = (dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?CertificateEnterApplyDAL.GetzzeCertIDByCertificateID(Convert.ToInt64(dr["CERTIFICATEID"])):(dr["zzeCertID"] == DBNull.Value?"":dr["zzeCertID"].ToString())),
                                    //BusinessInformation =[],
                                    OperateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
                                                    :(Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1)) ?"05"//过期也传注销，否则他省不认
                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"08"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02":"09"),                                  
                                    oldcertNum=(dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?CertificateEnterApplyDAL.GetCertificateCodeByCertificateID(Convert.ToInt64(dr["CERTIFICATEID"])):"")
                                }
                            }, Url_Agry_Accept);
                                break;
                            case "2"://特种作业
                                result = gb.Imputation.Core<gb.Imputation.Tzzy.AcceptData>.Imputation(gb_accessToken_tz,
                                new List<gb.Imputation.Tzzy.AcceptData>
                            {
                                new gb.Imputation.Tzzy.AcceptData
                                {
                                    ECertID = _ECertID.Replace("-",""),
                                    ProvinceNum = "110000",
                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
                                    IssuAuth = "北京市住房和城乡建设委员会",
                                    IssuAuthCode = "11110000000021135M",
                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
                                    Name = dr["WORKERNAME"].ToString(),
                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))),
                                    OperationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    CategoryDescription= (CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    CertState =  CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
                                    CertStatusDescription = CertificateDAL.Get_GB_Tzzy_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),                                    
                                    AssociatedZzeCertID = (dr["zzeCertID"] == DBNull.Value?"":dr["zzeCertID"].ToString()),
                                    //BusinessInformation =[],
                                    OperateType= ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout)?"05"
                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"
                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02"
                                    :"08")
                                  }
                               }, Url_Tzry_Accept);
                                break;
                        }

                        switch (result.ReturnCode)
                        {
                            case "1"://成功
                            case "2"://成功，有预警
                                //正式***************************************************************************************************************************
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[QRCodeCertid]='{1}',[QRCodeKey]='{2}',[CertificateCAID]='{3}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null   where [CERTIFICATEID]={0}"
                                    , dr["CERTIFICATEID"], result.ReturnData.SuccessData[0].EncryCertid, result.ReturnData.SuccessData[0].EncryKey, _ECertID));//更新二维码赋码信息

                                //File.Delete(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg", dr["CertificateID"]));
                                FileLog.WriteLog(string.Format("获取证书{0}二维码赋码成功。", dr["CERTIFICATECODE"]));
                                break;
                            case "0":
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                                , dr["CERTIFICATEID"]
                                , result.ReturnData.ErrorData[0].ErrorMsg
                                , EnumManager.EleCertDoStep.GetCode
                                ));//更新电子证书生成结果

                                WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码",
                                    string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
                                );
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        FileLog.WriteLog(string.Format("赋码失败，错误信息：{0}", ex.Message), ex);
                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", ex.Message);

                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                               , dr["CERTIFICATEID"]
                               , ex.Message
                               , EnumManager.EleCertDoStep.GetCode
                               ));//更新电子证书生成结果
                        continue;
                    }
                }
            }
            catch (Exception ex2)
            {
                FileLog.WriteLog(string.Format("赋码失败，错误信息：{0}", ex2.Message), ex2);

            }
        }

        //同时持有多本ABC证变更单位 - 批量业务数据赋码接口：从业人员证书获取二维码（部署在192.168.150.175)
        private void CongYe_PatchChangeUnitGetQRCode(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书多证批量获取二维码CongYe_PatchChangeUnitGetQRCode()");

            //加快批量变更
            CommonDAL.ExecSQL(@"update [dbo].[CERTIFICATE]	
                                set EleCertErrTime = DATEADD(day,-1,EleCertErrTime)										
                                where [EleCertErrDesc] like '%必须在同一个企业下%' and EleCertErrTime > dateadd(hour,-2,getdate())");

            //            string sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
            //                                            FROM [dbo].[CERTIFICATE] c 
            //                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
            //                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
            //                                            where   c.posttypeid = 1     
            //                                                    and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
            //                                                    and c.workercertificatecode in
            //                                                    (	
            //		                                                select top ({0} / 4) workercertificatecode
            //		                                                FROM [dbo].[CERTIFICATE]
            //		                                                where (postid =6 or postid =147 or postid =148 or postid =1123 or postid =1125)
            //		                                                and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
            //		                                                and ([EleCertErrDesc] like '%必须在同一个企业下%') 
            //                                                        and workercertificatecode >'{1}'
            //		                                                group by workercertificatecode
            //		                                                having count(*) > 1  and count(distinct unitcode) = 1	
            //                                                    )   
            //                                                    and (c.[EleCertErrDesc] like '%必须在同一个企业下%' )
            //                                                    and 
            //                                                    (
            //                                                        c.EleCertErrTime < DATEADD(hour,-1, GETDATE())
            //                                                        or c.EleCertErrTime is null
            //                                                    )                                       
            //                                                    order by c.[workercertificatecode]  ", MaxCountExe, cursor_str["cursor_CongYe_PatchChangeUnit"]);


            string sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
                                            FROM [dbo].[CERTIFICATE] c 
                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            inner join (
	                                            select distinct workercertificatecode,unitcode
	                                            FROM [dbo].[CERTIFICATE]
	                                            where posttypeid =1
	                                            and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
		                                        and ([QRCodeTime] is null or [QRCodeTime] < [CHECKDATE]) and [ZAFaceImgTime] > [CHECKDATE]
	                                            and ([EleCertErrDesc] like '%必须在同一个企业下%')
	                                            and workercertificatecode > '{1}' 
                                            ) t
                                            on c.workercertificatecode = t.workercertificatecode and c.UNITCODE = t.UNITCODE
                                             where c.posttypeid = 1     
                                            and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
                                             and  ( c.EleCertErrTime < DATEADD(hour,-12, GETDATE())  or c.EleCertErrTime is null  )   
                                             and  exists (
	                                            select count(*) FROM [dbo].[CERTIFICATE]
	                                            where [CERTIFICATE].workercertificatecode = t.workercertificatecode and [CERTIFICATE].UNITCODE = t.UNITCODE
	                                            and posttypeid =1 and [ZAFaceImgTime] > [CHECKDATE]
	                                            and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
                                                  and  (EleCertErrTime < DATEADD(hour,-12, GETDATE()) ) 
	                                            having count(*) >1
                                            )
                                            and c.[ZAFaceImgTime] > c.[CHECKDATE]
                                            and p.[PauseID] is null
                                            order by c.[workercertificatecode]  ", MaxCountExe, cursor_str["cursor_CongYe_PatchChangeUnit"]);


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_str["cursor_CongYe_PatchChangeUnit"] = "";//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_str["cursor_CongYe_PatchChangeUnit"] = dtOriginal.Rows[dtOriginal.Rows.Count - 1]["workercertificatecode"].ToString();//记录扫描位置
            }

            string IDcard = "";//身份证号
            DataTable pathChangeTB = null;//批量变更单位

            //gb.Imputation.ResponseResult result = null;
            //string _ECertID = null;//电子证书ID
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                cursor_str["cursor_CongYe_PatchChangeUnit"] = dr["workercertificatecode"].ToString();
                if (i % 10 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;

                try
                {
                    if (IDcard == "" || IDcard != dr["WorkerCertificateCode"].ToString())
                    {
                        if (IDcard != "" && IDcard != dr["WorkerCertificateCode"].ToString())
                        {
                            PatchImputation(pathChangeTB);
                        }
                        pathChangeTB = dr.Table.Clone();
                        DataColumn[] c = new DataColumn[1];
                        c[0] = pathChangeTB.Columns["QRCodeCertid"];
                        pathChangeTB.PrimaryKey = c;
                    }

                    IDcard = dr["WorkerCertificateCode"].ToString();
                    pathChangeTB.ImportRow(dr);

                    if (i == dtOriginal.Rows.Count)//最后一人
                    {
                        PatchImputation(pathChangeTB);
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("赋码CongYe_PatchChangeUnitGetQRCode失败，错误信息：{0}", ex.Message), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码",
                                string.Format("{0}", ex.Message)
                            );
                    continue;
                }
            }
        }

        /// <summary>
        /// 批量赋码（同人多本ABC同时变更单位）
        /// </summary>
        /// <param name="dt"></param>
        private void PatchImputation(DataTable dt)
        {
            List<WS_GetData.gb.Imputation.Aqscglry.AcceptData> list = new List<WS_GetData.gb.Imputation.Aqscglry.AcceptData>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("，").Append(dr["CERTIFICATECODE"].ToString());
                dr["CertificateCAID"] = Guid.NewGuid().ToString();
                dr["QRCodeCertid"] = dr["CertificateCAID"].ToString().Replace("-", "");

                list.Add(new WS_GetData.gb.Imputation.Aqscglry.AcceptData
                {
                    ECertID = dr["CertificateCAID"].ToString().Replace("-", ""),
                    ProvinceNum = "110000",
                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(", "（").Replace(")", "）"),
                    IssuAuth = "北京市住房和城乡建设委员会",
                    IssuAuthCode = "11110000000021135M",
                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                    Name = dr["WORKERNAME"].ToString(),
                    Gender = (dr["SEX"].ToString() == "女" ? "2" : "1"),
                    BirthDate = Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "999" : "111"),
                    CompanyName = dr["UNITNAME"].ToString(),
                    CreditCode = dr["CreditCode"].ToString(),
                    Appointment = CertificateDAL.GetAppointmentCode(dr),
                    TechnicalTitle = CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value ? "" : dr["SKILLLEVEL"].ToString()),
                    EducationDegree = CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"] == DBNull.Value ? "" : dr["CULTURALLEVEL"].ToString()),
                    Major = dr["PostName"].ToString(),
                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))),
                    //01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
                    CertState = CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]), ""),
                    CertStatusDescription = CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]), ""),
                    AssociatedZzeCertID = (dr["zzeCertID"] == DBNull.Value ? "" : dr["zzeCertID"].ToString()),
                    OperateType = "09"
                });
            }

            WS_GetData.gb.Imputation.ResponseResult result = WS_GetData.gb.Imputation.Core<WS_GetData.gb.Imputation.Aqscglry.AcceptData>.PatchChangeUnit(gb_accessToken, list, Url_Agry_PathChangeUnit);

            DataRow find = null;
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                switch (result.ReturnCode)
                {
                    case "1"://成功
                    case "2"://成功，有预警

                        foreach (WS_GetData.gb.Imputation.SuccessData d in result.ReturnData.SuccessData)
                        {
                            find = dt.Rows.Find(d.ECertID);
                            CommonDAL.ExecSQL(tran, string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[QRCodeCertid]='{1}',[QRCodeKey]='{2}',[CertificateCAID]='{3}',[ZACheckResult]=1,[ZACheckRemark]='校验成功',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null   where [CERTIFICATEID]={0}"
                                               , find["CERTIFICATEID"], d.EncryCertid, d.EncryKey, find["CertificateCAID"]));//更新二维码赋码信息
                        }
                        tran.Commit();
                        FileLog.WriteLog(string.Format("批量变更单位赋码成功{0}。", sb));
                        break;
                    case "0":
                        foreach (WS_GetData.gb.Imputation.ErrorData d in result.ReturnData.ErrorData)
                        {
                            find = dt.Rows.Find(d.ErrorGuid);
                            CommonDAL.ExecSQL(tran, string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                                               , find["CERTIFICATEID"], d.ErrorMsg, EnumManager.EleCertDoStep.GetCode));//更新电子证书生成结果                     
                            FileLog.WriteLog(string.Format("批量变更单位赋码失败，{0}。", d.ErrorMsg));

                            WriteEleCertError(find["CertificateCode"].ToString(), find["POSTTYPENAME"].ToString(), "赋码",
                                   string.Format("{0}，{1}", result.ReturnMsg, d.ErrorMsg)
                               );
                        }
                        tran.Commit();
                        break;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        //修改证书状态：用于注销、离京、过期、暂扣、还发,相当于数据归集，不用在调用证书归集接口（部署在192.168.150.175)
        //过期且有在办续期申请的未超过有效期20日的，暂缓推送。
        //提示：离京变更有效期过期自动注销，放在作业【国家电子证书接口2.0数据自清洗】中
        //      update [dbo].[CERTIFICATE]
        //      set [STATUS]='注销',EleCertErrTime=null,QRCodeTime=null,REMARK=CONVERT(varchar(10),getdate(),21) + '有效期过期自动注销。'
        //      where posttypeid=1 and [STATUS]='离京变更' and VALIDENDDATE < dateadd(day,-1,getdate()) and CHECKDATE > '2023-01-01'  
        private void CongYe_UpdataCertStatus(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书获取二维码CongYe_GetQRCode()");

            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
                                            FROM [dbo].[CERTIFICATE] c 
                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            where c.posttypeid <3   
                                            and 
                                            (
                                                (
		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                            and (c.[QRCodeTime] is null or c.[QRCodeTime] < c.[CHECKDATE])
                                                    and p.[PauseID] is not null
                                                )
                                                or
	                                            (
                                                    c.CHECKDATE > '2023-01-01' 
                                                    and ((c.[STATUS] ='注销' and (c.[EleCertErrDesc] is null or c.[EleCertErrDesc] not like '%注销)和原证书(无电子证照)匹配关系校验失败%' )) 
                                                            or c.[STATUS] = '离京变更')  
                                                    and ( c.[QRCodeTime] < c.[CHECKDATE] or c.[QRCodeTime] is null)
                                                )
			                                    or 
                                                (
                                                    c.VALIDENDDATE >  '2023-02-28' 
                                                    and c.VALIDENDDATE < dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更'
                                                    and (c.[QRCodeTime] < c.[VALIDENDDATE] or c.[QRCodeTime] is null) 
                                                    and (c.[EleCertErrTime] is null or  c.[EleCertErrDesc] not like '%注销)和原证书(无电子证照)匹配关系校验失败%')
                                                    and not exists(
				                                                    select 1 from CERTIFICATECONTINUE 
				                                                    where CERTIFICATEID =c.CERTIFICATEID 
				                                                    and [NewUnitCheckTime] > dateadd(day,-90,getdate())		
                                                                    and c.VALIDENDDATE > dateadd(day,-21,getdate())														
			                                                    )
                                                )		                                          
                                            )   
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null                                       
                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_updateCertStatus"]);
            }
            else
            {
                sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
                                            FROM [dbo].[CERTIFICATE] c 
                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            where c.posttypeid <3   
                                            and 
                                            (
                                                (
		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                            and (c.[QRCodeTime] is null or c.[QRCodeTime] < c.[CHECKDATE])
                                                    and p.[PauseID] is not null
                                                )
                                                or
	                                            (
                                                    c.CHECKDATE > '2023-01-01' 
                                                    and ((c.[STATUS] ='注销' and (c.[EleCertErrDesc] is null or c.[EleCertErrDesc] not like '%注销)和原证书(无电子证照)匹配关系校验失败%')) 
                                                            or c.[STATUS] = '离京变更')  
                                                    and ( c.[QRCodeTime] < c.[CHECKDATE] or c.[QRCodeTime] is null)
                                                )
			                                    or 
                                                (
                                                    c.VALIDENDDATE >  '2023-02-28' 
                                                    and c.VALIDENDDATE < dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更'
                                                    and (c.[QRCodeTime] < c.[VALIDENDDATE] or c.[QRCodeTime] is null) 
                                                    and (c.[EleCertErrDesc] is null or c.[EleCertErrDesc] not like '%注销)和原证书(无电子证照)匹配关系校验失败%')
                                                    and not exists(
				                                                    select 1 from CERTIFICATECONTINUE 
				                                                    where CERTIFICATEID =c.CERTIFICATEID 
				                                                    and [NewUnitCheckTime] > dateadd(day,-90,getdate())		
                                                                    and c.VALIDENDDATE > dateadd(day,-21,getdate())														
			                                                    )
                                                )		                                          
                                            )   
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null
                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_updateCertStatus"]);
            }

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CongYe_updateCertStatus"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CongYe_updateCertStatus"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            gb.Update.ResponseResult result = null;
            string _ECertID = null;//电子证书ID
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;

                //正式
                _ECertID = Guid.NewGuid().ToString().Replace("-","");
                //if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
                //                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
                //                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
                //                )
                //{
                //    _ECertID = dr["CertificateCAID"].ToString();//原电子证书ID
                //}
                //else
                //{
                //    _ECertID = Guid.NewGuid().ToString();//电子证书ID，会写入证书表（生成电子证书是不再创建）
                //}

                ////测试
                //_ECertID = dr["CertificateCAID"].ToString();//电子证书ID，测试时读取上次电子证书ID

                try
                {
                    //FileLog.WriteLog(string.Format("pnoto：{0}" , Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))));

                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Update.Core<gb.Update.Aqscglry.AcceptData>.UpdateCertData(gb_accessToken,
                            new List<gb.Update.Aqscglry.AcceptData>
                            {
                                new gb.Update.Aqscglry.AcceptData
                                {
                                    eCertID = _ECertID,
                                    zzeCertID=dr["zzeCertID"].ToString(),
                                    provinceNum = "110000",
                                    certNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),                                   
                                    categoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    identityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    creditCode=dr["CreditCode"].ToString(),
                                   

                                    //证书状态：01	有效,02	暂扣,03	撤销,04	注销,05	失效,06	办理转出,07	吊销,99	其他
                                    certState =  (dr["CASESTATUS"] != DBNull.Value && dr["CASESTATUS"].ToString() == "取消办理转出")?"01":CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
                                    certStatusDescription = (dr["CASESTATUS"] != DBNull.Value && dr["CASESTATUS"].ToString() == "取消办理转出")?"取消办理转出":CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
                                    /// 01	办理新发电子证照,02	办理延续,03	办理暂扣,04	办理过期失效,05	办理注销（撤销、吊销、办理转出）,06	办理暂扣发还,07	办理其他业务,08	办理转入,09	办理变更，10 取消办理转出
                                    operateType = (dr["CASESTATUS"] != DBNull.Value && dr["CASESTATUS"].ToString() == "取消办理转出")?"10":
                                                    ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"08"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02":"09")
                                }
                            }, Url_Agry_Update);
                            break;
                        case "2"://特种作业
                            result = gb.Update.Core<gb.Update.Tzzy.AcceptData>.UpdateCertData(gb_accessToken_tz,
                            new List<gb.Update.Tzzy.AcceptData>
                            {
                                new gb.Update.Tzzy.AcceptData
                                {
                                    eCertID = _ECertID.Replace("-",""),
                                    zzeCertID=dr["zzeCertID"].ToString(),
                                    provinceNum = "110000",
                                    certNum = dr["CERTIFICATECODE"].ToString(),                                  
                                    identityCard = dr["WORKERCERTIFICATECODE"].ToString(),
                                    operationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    categoryDescription= (CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
                                    certState = CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
                                    certStatusDescription = CertificateDAL.Get_GB_Tzzy_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),                                    
                                    operateType= ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout)?"05"
                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"
                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01"
                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02"
                                    :"08")
                                  }
                               }, Url_Tzry_Update);
                            break;
                    }

                    switch (result.ReturnCode)
                    {
                        case "1"://成功
                        case "2"://成功，有预警
                            //正式***************************************************************************************************************************
                            if (dr["CASESTATUS"] != DBNull.Value && dr["CASESTATUS"].ToString() == "取消办理转出")
                            {
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[STATUS]='京内变更',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新赋码时间
                            }
                            else if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
                                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
                                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
                                )
                            {
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新赋码时间
                            }
                            else if (dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString() == EnumManager.CertificatePauseStatus.ApplyPause)//暂扣
                            {
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0};UPDATE [dbo].[CertificatePause] set [PauseDoTime]=getdate(),[PauseStatus]='{2}',[PauseStatusCode]=2 WHERE [PauseID]={3};", dr["CERTIFICATEID"], _ECertID, EnumManager.CertificatePauseStatus.Pauseing, dr["PauseID"]));//更新赋码时间，暂扣状态
                            }
                            else if (dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString() == EnumManager.CertificatePauseStatus.ApplyReturn)//暂扣还发
                            {
                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[ZACheckTime]=null,[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0};UPDATE [dbo].[CertificatePause] set [PauseDoTime]=getdate(),[PauseStatus]='{2}',[PauseStatusCode]=4 WHERE [PauseID]={3};", dr["CERTIFICATEID"], _ECertID, EnumManager.CertificatePauseStatus.Returned, dr["PauseID"]));//更新赋码时间，暂扣发还状态，[ZACheckTime]=null重新生成电子证书
                            }

                            //File.Delete(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg", dr["CertificateID"]));
                            FileLog.WriteLog(string.Format("获取证书{0}二维码赋码成功。", dr["CERTIFICATECODE"]));
                            break;
                        case "0":
                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                            , dr["CERTIFICATEID"]
                            , result.ReturnData.ErrorData[0].ErrorMsg
                            , EnumManager.EleCertDoStep.GetCode
                            ));//更新电子证书生成结果

                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码",
                                string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
                            );
                            break;
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("赋码失败(CongYe_UpdataCertStatus)，错误信息：{0}", ex.Message), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", ex.Message);

                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                           , dr["CERTIFICATEID"]
                           , ex.Message
                           , EnumManager.EleCertDoStep.GetCode
                           ));//更新电子证书生成结果
                    continue;
                }
            }
        }

        /// <summary>
        /// 设置质安网证书状态为其他（相当于挂起，用于其他证书正常赋码），【放弃不用，改用批量赋码接口CongYe_PatchChangeUnitGetQRCode】
        /// </summary>
        private void CongYe_SetCertState99(DateTime startTime)
        {
            return;
//            //查询同时持有B、C；单位一致；校验或赋码报错：存在B证：京建安B or 存在证书：京建安C1 or 存在证书：京建安C2 or 存在证书：京建安C3
//            string sql = String.Format(@"select top {0} c.*,u.CreditCode
//                                        FROM [dbo].[CERTIFICATE] c
//                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode                                      
//                                        where c.posttypeid =1 
//                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
//                                        and c.workercertificatecode in
//                                        (	
//		                                    select workercertificatecode
//		                                    FROM [dbo].[CERTIFICATE] c
//		                                    where (postid =6 or postid =147 or postid =148 or postid =1123 or postid =1125)
//		                                    and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
//		                                    and ([EleCertErrDesc] like '%必须在同一个企业下%') 
//		                                    group by workercertificatecode
//		                                    having count(*) >1  and count(distinct unitcode)=1	
//                                        )
            //                                        and (c.[CASESTATUS] is null or c.[CASESTATUS] <> '质安网挂起')
//                                        and 
//                                            (
//                                                c.EleCertErrTime < DATEADD(hour,-4, GETDATE())
//                                                or c.EleCertErrTime is null
//                                            )
//                                        and (c.[EleCertErrDesc] like '%必须在同一个企业下%' )
//                                        order by  c.workercertificatecode  ", MaxCountExe);


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
//            if (dtOriginal.Rows.Count == 0)
//            {
//                return;
//            }

//            if (GBLogin(Url_Token) == false) return;

//            gb.Update.ResponseResult result = null;
//            string _ECertID = null;//电子证书ID
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;

//                _ECertID = Guid.NewGuid().ToString().Replace("-", "");

//                try
//                {
//                    //三类人
//                    result = gb.Update.Core<gb.Update.Aqscglry.AcceptData>.UpdateCertData(gb_accessToken,
//                    new List<gb.Update.Aqscglry.AcceptData>
//                            {
//                                new gb.Update.Aqscglry.AcceptData
//                                {
//                                    eCertID = _ECertID,
//                                    zzeCertID=dr["zzeCertID"].ToString(),
//                                    provinceNum = "110000",
//                                    certNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),                                   
//                                    categoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    identityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    creditCode=dr["CreditCode"].ToString(),
//                                    //证书状态：01	有效,02	暂扣,03	撤销,04	注销,05	失效,06	办理转出,07	吊销,99	其他
//                                    certState = "99",
//                                    certStatusDescription = "持证人正在办理跨企业变更业务，办理完成前本证书暂不可用",
//                                    /// 01	办理新发电子证照,02	办理延续,03	办理暂扣,04	办理过期失效,05	办理注销（撤销、吊销、办理转出）,06	办理暂扣发还,07	办理其他业务,08	办理转入,09	办理变更
//                                    operateType = "07"
//                                }
//                            }, Url_Agry_Accept);

//                    switch (result.ReturnCode)
//                    {
//                        case "1"://成功
//                        case "2"://成功，有预警
            //                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS] = '质安网挂起',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CertificateCAID]='{1}'  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新挂起状态
//                            FileLog.WriteLog(string.Format("挂起证书{0}成功。", dr["CERTIFICATECODE"]));
//                            break;
//                        case "0":
//                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                            , dr["CERTIFICATEID"]
//                            , result.ReturnData.ErrorData[0].ErrorMsg
//                            , EnumManager.EleCertDoStep.SetTemp
//                            ));//更新电子证书生成结果

//                            //CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CertificateCAID]='{1}'  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"],_ECertID));//更新挂起状态
//                            //WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "挂起",
//                            //    string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
//                            //);



//                            FileLog.WriteLog(string.Format("挂起证书{0}失败。{1}，{2}", dr["CERTIFICATECODE"], result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg));
//                            break;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("证书挂起失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "挂起", ex.Message);
//                    continue;
//                }
//            }
        }

        //证书数据归集接口：从业人员证书归集电子证书(注销、离京、过期不用归集)（部署在192.168.150.175)
        private void CongYe_UpCertUrl(DateTime startTime)
        {
            FileLog.WriteLog("开始从业人员证书归集电子证书CongYe_UpCertUrl()");

            //清除归集重复错误
            CommonDAL.ExecSQL(@"update certificate 
                                set [EleCertErrTime]=null,[EleCertErrStep]=null,[EleCertErrDesc]=null
                                where [EleCertErrStep]='归集' and ([EleCertErrDesc]='赋码操作太快，请稍后再试！' or [EleCertErrDesc]='归集操作太快，请稍后再试！')
                                      and [EleCertErrTime] < DATEADD(HOUR,-1,getdate())");


            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                sql = String.Format(@"select top {0} ca_count = (select count(*) from [dbo].[CertificateCAHistory] where [CERTIFICATEID] = c.[CERTIFICATEID]),c.*
                                            FROM [dbo].[CERTIFICATE] c                                            
                                            where c.posttypeid < 3   
                                            and 
                                            (
	                                            (
		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                                    and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                            and c.[ReturnCATime] > c.[QRCodeTime] and c.[Ofd_ReturnCATime] > c.[QRCodeTime] 
	                                            )
                                            )
                                            and ((c.[QRCodeTime] is not null and  c.[ZZUrlUpTime] is null) or ( c.[ZZUrlUpTime] < c.[QRCodeTime])) 
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null
                                         
                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"]);
            }
            else
            {
                sql = String.Format(@"select top {0} ca_count = (select count(*) from [dbo].[CertificateCAHistory] where [CERTIFICATEID] = c.[CERTIFICATEID]),c.*
                                            FROM [dbo].[CERTIFICATE] c                                            
                                            where c.posttypeid < 3   
                                            and 
                                            (
	                                            (
		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                                    and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
		                                            and c.[ReturnCATime] > c.[QRCodeTime] and c.[Ofd_ReturnCATime] > c.[QRCodeTime] 
	                                            )
                                            )
                                            and ((c.[QRCodeTime] is not null and  c.[ZZUrlUpTime] is null) or ( c.[ZZUrlUpTime] < c.[QRCodeTime])) 
                                            and c.CERTIFICATEID < {1} 
                                            and c.EleCertErrTime is null
                                           
                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"]);

            }


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CongYe_UpCertUrl"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CongYe_UpCertUrl"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            gb.Upcert.ResponseResult result = null;
            string key = "";//二维码赋码key
            string tmp_zzbs;//电子证照国标标识（无校验位）
            List<gb.Upcert.AcceptData> list = null;
            int i = 0;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                if (i % 5 == 0)
                {
                    Thread.Sleep(1000);//暂停1秒
                }
                i += 1;
                try
                {
                    key = Utility.Cryptography.RSADecryptJava(gb_privateKeyJava, dr["QRCodeKey"].ToString());

                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
                            //其中电子证照根代码固定为“1.2.156.3005.2”
                            //证照类型代码固定为“11100000000013338W011”
                            //证照颁发机构代码：11110000000021135M
                            //顺序号(证书编号后11位，即4位年+7位当年流水号)
                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
                                               "1.2.156.3005.2",//电子证照根代码
                                               "11100000000013338W011",//证照类型代码
                                               "11110000000021135M",//证照颁发机构代码
                                               dr["CERTIFICATECODE"].ToString().Substring(dr["CERTIFICATECODE"].ToString().Length - 13).Replace("（", "").Replace("）", ""),//顺序号：前4位用证照颁发年份，后7位用当年流水次序号。
                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

                            list = new List<gb.Upcert.AcceptData>
                            {
                                new gb.Upcert.AcceptData
                                {
                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
                                }
                            };

                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken, list, Url_Agry_DzzGjAccept);
                            break;
                        case "2"://特种作业
                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
                            //其中电子证照根代码固定为“1.2.156.3005.2”
                            //证照类型代码固定为“11100000000013338W032”
                            //证照颁发机构代码：11110000000021135M
                            //顺序号(证书编号后10位，即4位年+6位当年流水号)
                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
                                               "1.2.156.3005.2",//电子证照根代码
                                               "11100000000013338W032",//证照类型代码
                                               "11110000000021135M",//证照颁发机构代码
                                               dr["CERTIFICATECODE"].ToString().Substring(dr["CERTIFICATECODE"].ToString().Length - 10),//顺序号：前4位用证照颁发年份，后6位用当年流水次序号。
                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

                            list = new List<gb.Upcert.AcceptData>
                            {
                                new gb.Upcert.AcceptData
                                {
                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
                                }
                            };

                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken_tz, list, Url_Tzry_DzzGjAccept);
                            break;
                        default:
                            continue;

                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZZUrlUpTime]=GETDATE(),[zzeCertID] ='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], list[0].ZzeCertID));//更新归集时间
                        FileLog.WriteLog(string.Format("归集证书{0}成功。", dr["CERTIFICATECODE"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                          , dr["CERTIFICATEID"]
                          , result.ReturnData.ErrorData[0].ErrorMsg
                          , EnumManager.EleCertDoStep.UpCertUrl
                          ));//更新电子证书生成结果

                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg));
                    }
                }
                catch (Exception ex)
                {

                    FileLog.WriteLog(string.Format("归集失败(CongYe_UpCertUrl)，错误信息：{0}", ex.Message), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 质量安全网变更身份证号接口：触发筛选条件 [EleCertErrDesc] like '%证书编号已存在%' and [CASESTATUS] ='变更质安网身份证号'
        /// </summary>
        public void ChangeIDCard(DateTime startTime)
        {
//            string sql = String.Format(@"select top {0} *
//                                        FROM [dbo].[CERTIFICATE]                                             
//                                        where posttypeid < 3 and  VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
//                                        and [EleCertErrDesc] like '%证书编号已存在%' and [CASESTATUS] = '变更质安网身份证号'  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"], EnumManager.EleCertDoStep.UpCertUrl);
            string sql = String.Format(@"select top {0} *
                                        FROM [dbo].[CERTIFICATE]                                             
                                        where posttypeid < 3 and  VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
                                        and [CASESTATUS] = '变更质安网身份证号'
                                        and EleCertErrTime < DATEADD(hour,-24, GETDATE())"
                                        , MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"], EnumManager.EleCertDoStep.UpCertUrl);
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            gb.Change.ResponseResult result = null;

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Change.Core<gb.Change.Aqscglry.AcceptData>.changeIDCard(gb_accessToken,
                            new List<gb.Change.Aqscglry.AcceptData>
                            {
                                new gb.Change.Aqscglry.AcceptData
                                {
                                    eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
                                    provinceNum="110000",
                                    certNum=dr["CertificateCode"].ToString(),
                                    categoryCode=CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    birthDate= Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"),
                                    identityCardType=(Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
                                }
                            },
                            Url_Agry_Change);
                            break;
                        case "2"://特种作业
                            //将查询sql posttypeid <2 改为 posttypeid <3
                            result = gb.Change.Core<gb.Change.Tzzy.AcceptData>.changeIDCard(gb_accessToken_tz,
                            new List<gb.Change.Tzzy.AcceptData>
                            {
                                new gb.Change.Tzzy.AcceptData
                                {
                                    eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
                                    provinceNum="110000",
                                    certNum=dr["CertificateCode"].ToString(),
                                    birthDate= Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"),
                                    identityCardType=(Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
                                }
                            },
                            Url_Tzry_Change);
                            break;
                        default:
                            continue;

                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS]=null,[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));//

                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", "质量安全网变更身份证关键信息成功");
                        FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息成功。", dr["CERTIFICATECODE"]));
                    }
                    else
                    {

                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网变更身份证关键信息失败：{0}", result.ReturnData.ErrorData[0].ErrorMsg));
                        FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息失败：{1}", dr["CERTIFICATECODE"], result.ReturnData.ErrorData[0].ErrorMsg));

                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                             , dr["CERTIFICATEID"]
                             , string.Format("质量安全网变更身份证关键信息失败：{0}", result.ReturnData.ErrorData[0].ErrorMsg)
                             , EnumManager.EleCertDoStep.GetCode
                             ));//更新电子证书生成结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息失败。", dr["CertificateCode"]), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网变更身份证关键信息失败：{0}", ex.Message));
                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                              , dr["CERTIFICATEID"]
                              , string.Format("质量安全网变更身份证关键信息失败：{0}", ex.Message)
                              , EnumManager.EleCertDoStep.GetCode
                              ));//更新电子证书生成结果
                    continue;
                }
            }

        }

        /// <summary>
        /// 质量安全原网变更已上传失效数据接口：触发筛选条件 [CASESTATUS] ='更新质安网数据'
        /// </summary>
        public void UpdataZAData(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
                                        FROM [dbo].[CERTIFICATE] c 
                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                        where c.posttypeid < 3 and  c.[CASESTATUS] = '更新质安网数据' ", MaxCountExe);
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            gb.Update.ResponseResult result = null;

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (ZhiAnFunStartTime != startTime) return;
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "1"://三类人
                            result = gb.Update.Core<gb.Update.Aqscglry.AcceptData>.UpdateCertData(gb_accessToken,
                            new List<gb.Update.Aqscglry.AcceptData>
                            {
                                new gb.Update.Aqscglry.AcceptData
                                {    
                                    //eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
                                    eCertID=dr["CertificateCAID"].ToString().Replace("-",""),
                                    provinceNum="110000",
                                    certNum=dr["CertificateCode"].ToString(),
                                    categoryCode= CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
                                    certState =  CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),//01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString(),
                                    creditCode=dr["CreditCode"].ToString(),
                                    //technicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
                                    operateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first||dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"01"
                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Continue?"02"
                                                    :"08")
                                }
                            },
                            Url_Agry_Update);
                            break;
                        case "2"://特种作业
                            result = gb.Update.Core<gb.Update.Tzzy.AcceptData>.UpdateCertData(gb_accessToken_tz,
                            new List<gb.Update.Tzzy.AcceptData>
                            {
                                new gb.Update.Tzzy.AcceptData
                                {    
                                    provinceNum="110000",
                                    certNum=dr["CertificateCode"].ToString(),
                                    operationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
                                    certState =  CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
                                }
                            },
                            Url_Tzry_Update);
                            break;
                        default:
                            continue;

                    }
                    if (result.ReturnCode == "1")
                    {
                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));
                        FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息成功。", dr["CERTIFICATECODE"]));
                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", "质量安全网更新已为终结状态（注销、撤销、吊销）证书信息成功");
                    }
                    else
                    {
                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质质量安全网更新已为终结状态（注销、撤销、吊销）证书信息失败：{0}", result.ReturnData.ErrorData[0].ErrorMsg));
                        FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息失败：{1}", dr["CERTIFICATECODE"], result.ReturnData.ErrorData[0].ErrorMsg));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息失败。", dr["CertificateCode"]), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网更新已为终结状态（注销、撤销、吊销）证书信息失败：{0}", ex.Message));
                    continue;
                }
            }
        }

        /// <summary>
        /// 获取外省转出证书信息
        /// </summary>
        public void GetCertOut()
        {
            FileLog.WriteLog("开始查询省外办理转出证书信息GetCertOut()");

            string sql = "SELECT top 10 * FROM [dbo].[CertificateOutApply] where [checkTime] is null order by [ApplyTime] desc ";

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            gb.OutCheck.ResponseResult result = null;
            string WorkerCertificateCode = "";//身份证
            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {

                    WorkerCertificateCode = dr["WorkerCertificateCode"].ToString();
                    result = gb.OutCheck.Core<gb.OutCheck.AcceptData>.CheckCertData(gb_accessToken, new List<gb.OutCheck.AcceptData> { new gb.OutCheck.AcceptData { identityCard = WorkerCertificateCode } }, Url_Agry_CheckOut);
                    if (result.ReturnCode == "1")
                    {
                        //开启事务
                        DBHelper db = new DBHelper();
                        DbTransaction tran = db.BeginTransaction();
                        try
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            foreach (WS_GetData.gb.OutCheck.SuccessData r in result.ReturnData.SuccessData)
                            {
                                sb.AppendFormat("union select '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',getdate()",
                                                   r.certNum, r.provinceNum, r.name, r.gender, r.identityCard, r.identityCardType, r.birthDate, r.categoryCode, r.companyName, r.creditCode
                                                   , r.issuAuth, r.certState, r.issuedDate, r.effectiveDate, r.expiringDate, r.zzeCertID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                    );
                            }

                            CommonDAL.ExecSQL(tran, string.Format(@"update [dbo].[CertificateOutApply] 
                                                            set [checkRtnCode]={1},[checkInfo]='{2}',[checkTime]=getdate()
                                                            where ApplyID ={0}", dr["ApplyID"], result.ReturnCode, result.ReturnMsg));

                            if (sb.Length > 0)
                            {
                                sb.Remove(0, 6);
                                CommonDAL.ExecSQL(tran, string.Format(@"DELETE FROM [dbo].[CertificateOut] WHERE [out_identityCard]='{0}';
                                                            INSERT INTO [dbo].[CertificateOut]([out_certNum],[out_provinceNum],[out_name],[out_gender],[out_identityCard],[out_identityCardType],[out_birthDate],
                                                            [out_categoryCode],[out_companyName],[out_creditCode],[out_issuAuth],[out_certState],[out_issuedDate],[out_effectiveDate],[out_expiringDate],[out_zzeCertID],[cjsj]) 
                                                            {1}", WorkerCertificateCode, sb));
                            }
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            FileLog.WriteLog(string.Format("查询{0}的省外办理转出证书信息失败。", WorkerCertificateCode), ex);
                        }
                    }
                    else if (result.ReturnCode == "0")
                    {
                        CommonDAL.ExecSQL(string.Format(@"update [dbo].[CertificateOutApply] 
                                                            set [checkRtnCode]={1},[checkInfo]='{2}',[checkTime]=getdate()
                                                            where ApplyID ={0}", dr["ApplyID"], result.ReturnCode, result.ReturnMsg));

                        FileLog.WriteLog(string.Format("校验证书{0}省外办理转出返回有错误，{1}", WorkerCertificateCode, result.ReturnData.ErrorData[0].ErrorMsg));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("查询{0}的省外办理转出证书信息失败。", WorkerCertificateCode), ex);
                }
            }
        }

        /// <summary>
        /// 获取外省转入证书信息（尚未启用，需要调用国家接口，将状态改为注销)
        /// </summary>
        public void GetCertIn()
        {
            string sql = "SELECT top 10 * FROM [dbo].[View_CertificateChange] where [NOTICEDATE] > '2024-01-01' and [CHANGETYPE]='离京变更' and [NOTICERESULT]='通过' and [CheckInNewCertificateCode] is null order by [NOTICEDATE] desc ";

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (GBLogin(Url_Token) == false) return;

            gb.InCheck.ResponseResult result = null;
            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {
                    result = gb.InCheck.Core<gb.InCheck.AcceptData>.CheckCertInStatus(gb_accessToken, new List<gb.InCheck.AcceptData> { 
                                new gb.InCheck.AcceptData { 
                                    pageindex=0,
                                    pagesize=1,
                                    provinceNum="110000",
                                    certNum = dr["CertificateCode"].ToString()
                                } 
                            }, Url_Agry_CheckOut);
                    if (result.ReturnCode == "1")
                    {
                        if (result.ReturnData.SuccessData.Count > 0)
                        {
                            CommonDAL.ExecSQL(string.Format(@"update [dbo].[CertificateChange] 
                                                        set [CheckInNewCertificateCode]='{1}' 
                                                        where [CERTIFICATECHANGEID] = {0}", dr["CERTIFICATECHANGEID"], result.ReturnData.SuccessData[0].newCertNum
                                ));
                        }
                    }
                    else if (result.ReturnCode == "0")
                    {
                        FileLog.WriteLog(string.Format("校验证书{0}省外办理转出返回有错误，{1}", dr["CertificateCode"], result.ReturnData.ErrorData[0].ErrorMsg));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("查询{0}的省外办理转出证书信息失败。", dr["CertificateCode"]), ex);
                }
            }
        }

        #region 备份2024-5-23

//        //从业人员电子证书签章：全国证书查重（三类人、特种作业）
//        private void Certificate_ZhiAnDataCheck(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书签章全国证书查重Certificate_ZhiAnDataCheck()");
//            //建筑施工特种作业
//            //1 所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
//            //2 身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
//            //3 工种类别	operationCategory	字符型	2	是	详见7.2.3.2工种类别字典表 
//            //4 证书编号	certNum	字符型	25	否	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成


//            //建筑施工企业安全生产管理人员
//            //1	所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
//            //2	身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
//            //3	岗位类别代码	categoryCode	字符型	2	是	详见7.3.3.2岗位类别字典表
//            //4	统一社会信用代码	creditCode	字符型	18	是	持该证书的安管人员受聘企业的统一社会信用代码
//            //5	证书编号	certNum	字符型	23	是	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成


//            //            string sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],u.CreditCode
//            //                                                      FROM [dbo].[CERTIFICATE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//            //                                                      where c.posttypeid <3   and c.VALIDENDDATE > getdate() and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
//            //                                                      and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE]) 
//            //                                                      and c.CERTIFICATEID < {1}
//            //                                                      order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"]);

//            //***********临时方案，只处理三类人

//            //c.posttypeid <3 and ((c.VALIDENDDATE> dateadd(year,-3,getdate()) and c.VALIDENDDATE < getdate()) or c.VALIDENDDATE > getdate())  and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
//            string sql = "";
//            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 10)
//            {
//                sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],u.CreditCode
//                                                      FROM [dbo].[CERTIFICATE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                                      where c.posttypeid <3   
//                                                      and (
//                                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')                                                         
//                                                          )
//                                                      and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE] or (dateadd(day,1,c.[ZACheckTime])<getdate() and ZACheckResult=0)) 
//                                                      and c.CERTIFICATEID < {1}
//                                                      and ((c.EleCertErrStep ='{2}' and c.EleCertErrTime <DATEADD(hour,-4, GETDATE())) or c.EleCertErrTime is null or c.EleCertErrStep <>'{2}')
//                                                      order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"], EnumManager.EleCertDoStep.CertCheck);
//            }
//            else
//            {
//                sql = String.Format(@"select top {0} c.[CERTIFICATEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],u.CreditCode
//                                                      FROM [dbo].[CERTIFICATE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                                      where c.posttypeid <3   
//                                                       and (
//                                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')                                                           
//                                                          )
//                                                      and (c.[ZACheckTime] is null or c.[ZACheckTime] < c.[CHECKDATE] or (dateadd(day,1,c.[ZACheckTime])<getdate() and ZACheckResult=0)) 
//                                                      and (c.EleCertErrTime <DATEADD(day,-1, GETDATE()) or c.EleCertErrTime is null)
//                                                      and ((c.EleCertErrStep ='{2}' and c.EleCertErrTime <DATEADD(hour,-4, GETDATE())) or c.EleCertErrTime is null or c.EleCertErrStep <>'{2}')
//                                                      and c.CERTIFICATEID < {1}
//                                                      order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_ZhiAnDataCheck"], EnumManager.EleCertDoStep.CertCheck);
//            }


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            gb.Verification.ResponseResult result = null;

//            if (GBLogin(Url_Token) == false) return;

//            ////////*****************测试***************************************
//            //if (DateTime.Now < Convert.ToDateTime("2023-03-18"))
//            //{
//            //    try
//            //    {
//            //        string tempUrl = "http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check";
//            //        result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//            //                            new List<gb.Verification.Aqscglry.AcceptData>
//            //                {
//            //                    new gb.Verification.Aqscglry.AcceptData
//            //                    {
//            //                        ProvinceNum = "110000",
//            //                        IdentityCard = "372928199710044158",
//            //                        CategoryCode = CertificateDAL.GetSLRPostCode("综合类专职安全生产管理人员"),
//            //                        CreditCode= "91110113MA0194JY7H",
//            //                        CertNum = "京建安C3（2021）0022585"
//            //                    }
//            //                }, tempUrl);

//            //        FileLog.WriteLog(string.Format("调用加入白名单的数据校验测试http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check接口成功。返回码{0}", result.ReturnCode));
//            //    }
//            //    catch (Exception ex)
//            //    {
//            //        FileLog.WriteLog("调用加入白名单的数据校验测试http://59.255.8.217/epoint-gateway/axzchecktest/rest/axzcheckrest/Axz_Check接口失败，错误信息：" + ex.Message, ex);
//            //    }
//            //}

//            /////////********************************************************///////////////////////////////


//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CongYe_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_CongYe_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
//            }

//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//                                new List<gb.Verification.Aqscglry.AcceptData>
//                            {
//                                new gb.Verification.Aqscglry.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    CreditCode= dr["CreditCode"].ToString(),
//                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）")
//                                }
//                            }, Url_Agry_Check);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
//                                new List<gb.Verification.Tzzy.AcceptData>
//                            {
//                                new gb.Verification.Tzzy.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    CertNum = dr["CERTIFICATECODE"].ToString()
//                                }
//                            }, Url_Tzry_Check);
//                            break;
//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));//更新数据验证时间
//                        FileLog.WriteLog(string.Format("校验证书{0}成功。", dr["CERTIFICATECODE"]));
//                    }
//                    else if (result.ReturnCode == "0")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={3},[ZACheckRemark]='{1}',[EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                            , dr["CERTIFICATEID"]
//                            , result.ReturnData.ErrorData[0].ErrorMsg
//                            , EnumManager.EleCertDoStep.CertCheck
//                             , result.ReturnCode
//                            ));//更新电子证书生成结果

//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验"
//                            , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
//                            );

//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={1},[ZACheckRemark]='{2}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where [CERTIFICATEID]={0}"
//                            , dr["CERTIFICATEID"]
//                            , result.ReturnCode
//                            , result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : ""
//                            ));//更新数据验证时间                        
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("校验失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验", ex.Message);
//                    continue;
//                }
//            }
//        }

//        //考试报名：全国证书查重（三类人、特种作业）
//        private void ExamSignup_ZhiAnDataCheck(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员考试报名全国证书查重ExamSignup_ZhiAnDataCheck()");
//            //企业已经审核，尚未质安网查重（检查失败，隔4小时重新检查）
//            string sql = String.Format(@"select top {0} c.[EXAMSIGNUPID],c.[POSTTYPEID],c.[POSTNAME],c.[CERTIFICATECODE] as WORKERCERTIFICATECODE,u.CreditCode
//                                          FROM [dbo].[VIEW_EXAMSIGNUP_NEW] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                          where c.posttypeid <3 and c.[CreateTime] >'2023-02-14' and c.[Status]='{2}' 
//                                          and (
//                                                    c.[ZACheckTime] is null 
//	                                                or (c.ZACheckResult = 0 and c.[ZACheckTime] < DATEADD(hour,-4, GETDATE()) and c.SIGNUPENDDATE > DATEADD(day,-3, GETDATE()))
//                                              )
//                                          and c.EXAMSIGNUPID < {1}                             
//                                          order by c.EXAMSIGNUPID desc  ", MaxCountExe, cursor_long["cursor_ExamSignup_ZhiAnDataCheck"], EnumManager.SignUpStatus.NewSignUp);


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_ExamSignup_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_ExamSignup_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["EXAMSIGNUPID"]);//记录扫描位置
//            }

//            gb.Verification.ResponseResult result = null;
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//                                new List<gb.Verification.Aqscglry.AcceptData>
//                            {
//                                new gb.Verification.Aqscglry.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    CreditCode= dr["CreditCode"].ToString(),
//                                    CertNum = ""
//                                }
//                            }, Url_Agry_Check);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
//                                new List<gb.Verification.Tzzy.AcceptData>
//                            {
//                                new gb.Verification.Tzzy.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    CertNum = ""
//                                }
//                            }, Url_Tzry_Check);
//                            break;
//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[ExamSignUp] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [EXAMSIGNUPID]={0}", dr["EXAMSIGNUPID"]));//更新数据验证时间
//                        //FileLog.WriteLog(string.Format("{0}考试报名{1}质安网校验成功。", dr["WORKERCERTIFICATECODE"],dr["POSTNAME"]));
//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[ExamSignUp] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [EXAMSIGNUPID]={0}"
//                            , dr["EXAMSIGNUPID"]
//                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
//                             , result.ReturnCode
//                            ));//更新数据验证时间
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("考试报名质安网查重校验失败，错误信息：" + ex.Message, ex);
//                    continue;
//                }
//            }
//        }

//        //三类人证书进京：全国证书查重（三类人）
//        private void CertificateEnter_ZhiAnDataCheck(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书进京全国证书查重CertificateEnter_ZhiAnDataCheck()");

//            //企业已经审核，尚未质安网查重
//            string sql = String.Format(@"select top {0} A.APPLYID, A.POSTTYPEID,  N2.POSTNAME,A.WORKERCERTIFICATECODE,u.CreditCode 
//                                        FROM DBO.CERTIFICATEENTERAPPLY A
//                                        inner join Unit u on A.UNITCODE = u.ENT_OrganizationsCode
//                                        LEFT JOIN dbo.POSTINFO N2 ON A.POSTID = N2.POSTID
//                                        where  A.[ApplyDate] >'2023-02-14' and A.ApplyStatus='{2}' and A.[ZACheckTime] is null 
//                                        and A.APPLYID < {1}                             
//                                        order by A.APPLYID desc  ", MaxCountExe, cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"], EnumManager.CertificateEnterStatus.WaitUnitCheck);


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_CertificateEnter_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["APPLYID"]);//记录扫描位置
//            }

//            gb.Verification.ResponseResult result = null;
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//                                new List<gb.Verification.Aqscglry.AcceptData>
//                            {
//                                new gb.Verification.Aqscglry.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    CreditCode= dr["CreditCode"].ToString(),
//                                    CertNum = ""
//                                }
//                            }, Url_Agry_Check);
//                            break;
//                        default:
//                            continue;
//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATEENTERAPPLY] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [APPLYID]={0}", dr["APPLYID"]));//更新数据验证时间
//                        FileLog.WriteLog(string.Format("{0}{1}证书进京质安网校验成功。", dr["WORKERCERTIFICATECODE"], dr["POSTNAME"]));
//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATEENTERAPPLY] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [APPLYID]={0}"
//                            , dr["APPLYID"]
//                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
//                             , result.ReturnCode
//                            ));//更新数据验证时间
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("证书进京质安网校验失败，错误信息：" + ex.Message, ex);
//                    continue;
//                }
//            }
//        }

//        //从业人员证书变更：全国证书查重（三类人、特种作业）
//        private void CertificateChange_ZhiAnDataCheck(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书变更全国证书查重CertificateChange_ZhiAnDataCheck()");

//            //企业已经审核，尚未质安网查重
//            string sql = String.Format(@"select top {0} A.[CERTIFICATECHANGEID],A.POSTTYPEID,A.POSTNAME,A.[NEWWORKERCERTIFICATECODE],u.CreditCode 
//                                        FROM DBO.[VIEW_CERTIFICATECHANGE] A
//                                        inner join Unit u on A.[NEWUNITCODE] = u.ENT_OrganizationsCode                                       
//                                        where  A.[ApplyDate] >'2023-03-10' and (A.[Status]='{2}' or A.[Status]='{3}') 
//                                        and (A.[ZACheckTime] is null or (A.[ApplyDate] > A.[ZACheckTime] and A.[ZACheckResult]=0))
//                                        and A.CHANGETYPE='京内变更' 
//                                        and A.CERTIFICATECHANGEID < {1}                             
//                                        order by A.CERTIFICATECHANGEID desc  ", MaxCountExe, cursor_long["cursor_CertificateChange_ZhiAnDataCheck"], EnumManager.CertificateChangeStatus.WaitUnitCheck, EnumManager.CertificateChangeStatus.Applyed);


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CertificateChange_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_CertificateChange_ZhiAnDataCheck"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CERTIFICATECHANGEID"]);//记录扫描位置
//            }

//            gb.Verification.ResponseResult result = null;
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//                                new List<gb.Verification.Aqscglry.AcceptData>
//                            {
//                                new gb.Verification.Aqscglry.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["NEWWORKERCERTIFICATECODE"].ToString(),
//                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    CreditCode= dr["CreditCode"].ToString(),
//                                    CertNum = ""
//                                }
//                            }, Url_Agry_Check);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
//                                new List<gb.Verification.Tzzy.AcceptData>
//                            {
//                                new gb.Verification.Tzzy.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["NEWWORKERCERTIFICATECODE"].ToString(),
//                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    CertNum = ""
//                                }
//                            }, Url_Tzry_Check);
//                            break;
//                        default:
//                            continue;
//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECHANGE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [CERTIFICATECHANGEID]={0}", dr["CERTIFICATECHANGEID"]));//更新数据验证时间
//                        FileLog.WriteLog(string.Format("{0}{1}证书变更质安网校验成功。", dr["NEWWORKERCERTIFICATECODE"], dr["POSTNAME"]));
//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECHANGE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [CERTIFICATECHANGEID]={0}"
//                            , dr["CERTIFICATECHANGEID"]
//                             , string.Format("{0}，{1}", result.ReturnMsg, result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : "")
//                             , result.ReturnCode
//                            ));//更新数据验证时间
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("从业人员证书变更质安网校验失败，错误信息：" + ex.Message, ex);
//                    continue;
//                }
//            }
//        }

//        //从业人员证书续期全国证书查重（三类人、特种作业）
//        private void CertificateContinue_ZhiAnDataCheck(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书续期全国证书查重CertificateContinue_ZhiAnDataCheck()");
//            //建筑施工特种作业
//            //1 所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
//            //2 身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
//            //3 工种类别	operationCategory	字符型	2	是	详见7.2.3.2工种类别字典表 
//            //4 证书编号	certNum	字符型	25	否	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成


//            //建筑施工企业安全生产管理人员
//            //1	所属省份	provinceNum	字符型	6	是	按照民政部官网《2020年中华人民共和国行政区划代码》
//            //2	身份证件号码	identityCard	字符型	18	是	持证人员的有效身份证件号
//            //3	岗位类别代码	categoryCode	字符型	2	是	详见7.3.3.2岗位类别字典表
//            //4	统一社会信用代码	creditCode	字符型	18	是	持该证书的安管人员受聘企业的统一社会信用代码
//            //5	证书编号	certNum	字符型	23	是	证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成

//            string sql = String.Format(@"select top {0} c.[CERTIFICATECONTINUEID],c.[POSTTYPEID],c.[POSTID],c.[POSTTYPENAME],c.[POSTNAME],c.[CERTIFICATECODE],c.[WORKERCERTIFICATECODE],u.CreditCode
//                                        FROM [dbo].[VIEW_CERTIFICATECONTINUE] c inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                        where c.posttypeid <3  and c.[ApplyDate] >'2023-09-28' and (c.[ZACheckTime] is null or (c.[ApplyDate] > c.[ZACheckTime] and c.[ZACheckResult]=0))
//                                        and c.CERTIFICATEID < {1}
//                                        order by c.CERTIFICATECONTINUEID desc  ", MaxCountExe, cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"]);
            


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            gb.Verification.ResponseResult result = null;

//            if (GBLogin(Url_Token) == false) return;
 

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }

//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                cursor_long["cursor_CertificateContinue_ZhiAnDataCheck"] = Convert.ToInt64(dr["CERTIFICATECONTINUEID"]);//记录扫描位置

//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Verification.Core<gb.Verification.Aqscglry.AcceptData>.Verification(gb_accessToken,
//                                new List<gb.Verification.Aqscglry.AcceptData>
//                            {
//                                new gb.Verification.Aqscglry.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    CategoryCode = CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    CreditCode= dr["CreditCode"].ToString(),
//                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）")
//                                }
//                            }, Url_Agry_Check);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Verification.Core<gb.Verification.Tzzy.AcceptData>.Verification(gb_accessToken_tz,
//                                new List<gb.Verification.Tzzy.AcceptData>
//                            {
//                                new gb.Verification.Tzzy.AcceptData
//                                {
//                                    ProvinceNum = "110000",
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    OperationCategory = CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    CertNum = dr["CERTIFICATECODE"].ToString()
//                                }
//                            }, Url_Tzry_Check);
//                            break;
//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]=1,[ZACheckRemark]='校验成功' where [CERTIFICATECONTINUEID]={0}", dr["CERTIFICATECONTINUEID"]));//更新数据验证时间
//                        FileLog.WriteLog(string.Format("校验证书{0}续期申请成功。", dr["CERTIFICATECODE"]));
//                    }
//                    else if (result.ReturnCode == "0")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={2},[ZACheckRemark]='{1}' where [CERTIFICATECONTINUEID]={0}"
//                            , dr["CERTIFICATECONTINUEID"]
//                            , result.ReturnData.ErrorData[0].ErrorMsg
//                             , result.ReturnCode
//                            ));//更新电子证书生成结果
//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATECONTINUE] set [ZACheckTime]=GETDATE(),[ZACheckResult]={1},[ZACheckRemark]='{2}' where [CERTIFICATECONTINUEID]={0}"
//                            , dr["CERTIFICATECONTINUEID"]
//                            , result.ReturnCode
//                            , result.ReturnCode == "0" ? result.ReturnData.ErrorData[0].ErrorMsg : result.ReturnCode == "2" ? result.ReturnData.WarnData[0].WarnMsg : ""
//                            ));//更新数据验证时间                        
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("证书续期全国证书查重校验失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "校验", ex.Message);
//                    continue;
//                }
//            }
//        }

//        //业务数据赋码接口：从业人员证书获取二维码(注销、离京、过期也要赋码,相当于数据归集，不用在调用证书归集接口)（部署在192.168.150.175)
//        private void CongYe_GetQRCode(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书获取二维码CongYe_GetQRCode()");

//            string sql = "";
//            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
//            {
//                sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
//                                            FROM [dbo].[CERTIFICATE] c 
//                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
//                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
//                                            where c.posttypeid <3   
//                                            and 
//                                            (
//	                                            (
//		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <> '进京待审批'
//		                                            and c.[ZACheckResult]>0  and ((c.[ZACheckTime] is not null and  c.[QRCodeTime] is null) or ( c.[QRCodeTime] < c.[ZACheckTime]))
//		                                            and c.[ZAFaceImgTime] >c.[ZACheckTime]
//	                                            )
//	                                            or
//	                                            (
//                                                    c.CHECKDATE > '2023-01-01' and (c.[STATUS] ='注销' or c.[STATUS] ='离京变更') and c.[ZAFaceImgTime] >c.[CHECKDATE] and ( c.[QRCodeTime] < c.[CHECKDATE] or c.[QRCodeTime] is null)
//                                                )
//			                                    or 
//                                                (
//                                                    c.VALIDENDDATE >  '2023-02-28' 
//                                                    and c.VALIDENDDATE < dateadd(day,-1,getdate()) 
//                                                    and c.[ZAFaceImgTime] >c.VALIDENDDATE 
//                                                    and ( c.[QRCodeTime] < c.[VALIDENDDATE] or c.[QRCodeTime] is null)
//                                                    and not exists(
//				                                                    select 1 from CERTIFICATECONTINUE 
//				                                                    where CERTIFICATEID =c.CERTIFICATEID 
//				                                                    and ([STATUS] = '已申请' or [STATUS] = '已初审' or [STATUS] = '已审核' )
//				                                                    and [APPLYDATE] > dateadd(month,-3,getdate())													
//			                                                    )
//                                                )		                                          
//                                            )   
//                                            and c.CERTIFICATEID < {1} 
//                                            and 
//                                            (
//                                                c.EleCertErrTime <DATEADD(hour,-4, GETDATE())
//                                                or c.EleCertErrTime is null
//                                               
//                                            )
//                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_GetQRCode"]);
//            }
//            else
//            {
//                sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL,p.PauseStatus,p.[PauseID]
//                                            FROM [dbo].[CERTIFICATE] c 
//                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
//                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
//                                            where c.posttypeid <3   
//                                            and 
//                                            (
//	                                            (
//		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <> '进京待审批'
//		                                            and c.[ZACheckResult]>0  and ((c.[ZACheckTime] is not null and  c.[QRCodeTime] is null) or ( c.[QRCodeTime] < c.[ZACheckTime]))
//		                                            and c.[ZAFaceImgTime] >c.[ZACheckTime]
//	                                            )
//	                                            or
//	                                            (
//                                                    c.CHECKDATE > '2023-01-01' and (c.[STATUS] ='注销' or c.[STATUS] ='离京变更') and c.[ZAFaceImgTime] >c.[CHECKDATE] and ( c.[QRCodeTime] < c.[CHECKDATE] or c.[QRCodeTime] is null)
//                                                )
//			                                    or 
//                                                (
//                                                    c.VALIDENDDATE >  '2023-02-28' 
//                                                    and c.VALIDENDDATE < dateadd(day,-1,getdate()) 
//                                                    and c.[ZAFaceImgTime] >c.VALIDENDDATE 
//                                                    and ( c.[QRCodeTime] < c.[VALIDENDDATE] or c.[QRCodeTime] is null)
//                                                    and not exists(
//				                                                    select 1 from CERTIFICATECONTINUE 
//				                                                    where CERTIFICATEID =c.CERTIFICATEID 
//				                                                    and ([STATUS] = '已申请' or [STATUS] = '已初审' or [STATUS] = '已审核' )
//				                                                    and [APPLYDATE] > dateadd(month,-3,getdate())													
//			                                                    )
//                                                )		                                          
//                                            )   
//                                            and c.CERTIFICATEID < {1} 
//                                            and 
//                                            (
//                                                c.EleCertErrTime <DATEADD(hour,-4, GETDATE())
//                                                or c.EleCertErrTime is null
//                                            )
//                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_GetQRCode"]);
//            }

//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CongYe_GetQRCode"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_CongYe_GetQRCode"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
//            }

//            gb.Imputation.ResponseResult result = null;
//            string _ECertID = null;//电子证书ID
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;

//                //正式
//                _ECertID = Guid.NewGuid().ToString();
//                //if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
//                //                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
//                //                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
//                //                )
//                //{
//                //    _ECertID = dr["CertificateCAID"].ToString();//原电子证书ID
//                //}
//                //else
//                //{
//                //    _ECertID = Guid.NewGuid().ToString();//电子证书ID，会写入证书表（生成电子证书是不再创建）
//                //}

//                ////测试
//                //_ECertID = dr["CertificateCAID"].ToString();//电子证书ID，测试时读取上次电子证书ID

//                try
//                {
//                    //FileLog.WriteLog(string.Format("pnoto：{0}" , Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))));

//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Imputation.Core<gb.Imputation.Aqscglry.AcceptData>.Imputation(gb_accessToken,
//                            new List<gb.Imputation.Aqscglry.AcceptData>
//                            {
//                                new gb.Imputation.Aqscglry.AcceptData
//                                {
//                                    ECertID = _ECertID.Replace("-",""),
//                                    ProvinceNum = "110000",
//                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
//                                    IssuAuth = "北京市住房和城乡建设委员会",
//                                    IssuAuthCode = "11110000000021135M",
//                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
//                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
//                                    CategoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    Name = dr["WORKERNAME"].ToString(),
//                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
//                                    BirthDate=Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
//                                    CompanyName =dr["UNITNAME"].ToString(),
//                                    CreditCode=dr["CreditCode"].ToString(),
//                                    Appointment=CertificateDAL.GetAppointmentCode(dr),
//                                    TechnicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
//                                    EducationDegree=CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"] == DBNull.Value?"":dr["CULTURALLEVEL"].ToString()),
//                                    Major="",
//                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))),
//                                    //01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
//                                    CertState = CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
//                                    CertStatusDescription = CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),
//                                    AssociatedZzeCertID = (dr["zzeCertID"] == DBNull.Value?"":dr["zzeCertID"].ToString()),
//                                    //BusinessInformation =[],
//                                    OperateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
        //                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
//                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
//                                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
//                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first||dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"01":"02")//01	新发电子证照,02	延续（变更）,03	暂扣,04	过期失效,05	注销（撤销、办理转出）,06	暂扣发还
//                                }
//                            }, Url_Agry_Accept);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Imputation.Core<gb.Imputation.Tzzy.AcceptData>.Imputation(gb_accessToken_tz,
//                            new List<gb.Imputation.Tzzy.AcceptData>
//                            {
//                                new gb.Imputation.Tzzy.AcceptData
//                                {
//                                    ECertID = _ECertID.Replace("-",""),
//                                    ProvinceNum = "110000",
//                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
//                                    IssuAuth = "北京市住房和城乡建设委员会",
//                                    IssuAuthCode = "11110000000021135M",
//                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
//                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
//                                    Name = dr["WORKERNAME"].ToString(),
//                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
//                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))),
//                                    OperationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    CategoryDescription= (CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
//                                    CertState =  CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
//                                    CertStatusDescription = CertificateDAL.Get_GB_Tzzy_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"]),dr["PauseStatus"] == DBNull.Value?"":dr["PauseStatus"].ToString()),                                    
//                                    AssociatedZzeCertID = (dr["zzeCertID"] == DBNull.Value?"":dr["zzeCertID"].ToString()),
//                                    //BusinessInformation =[],

//                                    //01新发电子证照,02延续（变更）,03暂扣,04	过期失效,05	注销（撤销、吊销）,06暂扣发还
//                                    OperateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout)?"05"
        //                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"
//                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyPause?"03"
//                                    :dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString()==EnumManager.CertificatePauseStatus.ApplyReturn?"06"
//                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01":"02")
//                                  }
//                               }, Url_Tzry_Accept);
//                            break;
//                    }

//                    switch (result.ReturnCode)
//                    {
//                        case "1"://成功
//                        case "2"://成功，有预警
//                            //正式***************************************************************************************************************************
//                            if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
//                                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
//                                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
//                                )
//                            {
        //                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新赋码时间
//                            }
//                            else if (dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString() == EnumManager.CertificatePauseStatus.ApplyPause)//暂扣
//                            {
        //                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0};UPDATE [dbo].[CertificatePause] set [PauseDoTime]=getdate(),[PauseStatus]='{2}',[PauseStatusCode]=2 WHERE [PauseID]={3};", dr["CERTIFICATEID"], _ECertID, EnumManager.CertificatePauseStatus.Pauseing, dr["PauseID"]));//更新赋码时间，暂扣状态
//                            }
//                            else if (dr["PauseStatus"] != DBNull.Value && dr["PauseStatus"].ToString() == EnumManager.CertificatePauseStatus.ApplyReturn)//暂扣还发
//                            {
        //                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[ZACheckTime]=null,[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0};UPDATE [dbo].[CertificatePause] set [PauseDoTime]=getdate(),[PauseStatus]='{2}',[PauseStatusCode]=4 WHERE [PauseID]={3};", dr["CERTIFICATEID"], _ECertID, EnumManager.CertificatePauseStatus.Returned, dr["PauseID"]));//更新赋码时间，暂扣发还状态，[ZACheckTime]=null重新生成电子证书
//                            }
//                            else
//                            {
//                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[QRCodeCertid]='{1}',[QRCodeKey]='{2}',[CertificateCAID]='{3}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[ADDITEMNAME]=null   where [CERTIFICATEID]={0}"
//                                   , dr["CERTIFICATEID"], result.ReturnData.SuccessData[0].EncryCertid, result.ReturnData.SuccessData[0].EncryKey, _ECertID));//更新二维码赋码信息
//                            }


//                            File.Delete(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg", dr["CertificateID"]));
//                            FileLog.WriteLog(string.Format("获取证书{0}二维码赋码成功。", dr["CERTIFICATECODE"]));
//                            break;
//                        case "0":
//                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                            , dr["CERTIFICATEID"]
//                            , result.ReturnData.ErrorData[0].ErrorMsg
//                            , EnumManager.EleCertDoStep.GetCode
//                            ));//更新电子证书生成结果

//                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码",
//                                string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
//                            );
//                            break;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("赋码失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", ex.Message);

//                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                           , dr["CERTIFICATEID"]
//                           , ex.Message
//                           , EnumManager.EleCertDoStep.GetCode
//                           ));//更新电子证书生成结果
//                    continue;
//                }
//            }
//        }


//        /// <summary>
//        /// 设置质安网证书状态为其他（相当于挂起，用于其他证书正常赋码）
//        /// </summary>
//        private void CongYe_SetCertState99(DateTime startTime)
//        {
//            //查询同时持有B、C；单位一致；校验或赋码报错：存在B证：京建安B or 存在证书：京建安C1 or 存在证书：京建安C2 or 存在证书：京建安C3
//            string sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
//                                        FROM [dbo].[CERTIFICATE] c
//                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
//                                        where (c.postid =6 or c.postid =147 or  c.postid =148 or c.postid =1123 or c.postid =1125)
//                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
//                                        and c.workercertificatecode in
//                                        (	
//		                                    select workercertificatecode
//		                                    FROM [dbo].[CERTIFICATE] c
//		                                    where (postid =6 or postid =147 or postid =148 or postid =1123 or postid =1125)
//		                                    and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
//		                                    and ([EleCertErrDesc] like '%存在%证%：京建安%' or [EleCertErrDesc] like '人员在其他企业%存在A证，在本企业不存在A证%') 
//		                                    group by workercertificatecode
//		                                    having count(*) >1  and count(distinct unitcode)=1	
//                                        )
        //                                        and (c.[CASESTATUS] is null or c.[CASESTATUS] <> '质安网挂起')
//                                        and 
//                                            (
//                                                c.EleCertErrTime < DATEADD(hour,-4, GETDATE())
//                                                or c.EleCertErrTime is null
//                                            )
//                                        and (c.[EleCertErrDesc] like '%存在%证%' )
//                                        order by  c.workercertificatecode  ", MaxCountExe);


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
//            if (dtOriginal.Rows.Count == 0)
//            {
//                return;
//            }

//            if (GBLogin(Url_Token) == false) return;

//            gb.Imputation.ResponseResult result = null;
//            string _ECertID = null;//电子证书ID
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;

//                _ECertID = Guid.NewGuid().ToString();

//                try
//                {
//                    //三类人
//                    result = gb.Imputation.Core<gb.Imputation.Aqscglry.AcceptData>.Imputation(gb_accessToken,
//                    new List<gb.Imputation.Aqscglry.AcceptData>
//                            {
//                                new gb.Imputation.Aqscglry.AcceptData
//                                {
//                                    ECertID = _ECertID.Replace("-",""),
//                                    ProvinceNum = "110000",
//                                    CertNum = dr["CERTIFICATECODE"].ToString(),
//                                    IssuAuth = "北京市住房和城乡建设委员会",
//                                    IssuAuthCode = "11110000000021135M",
//                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
//                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
//                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
//                                    CategoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    Name = dr["WORKERNAME"].ToString(),
//                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
//                                    BirthDate=Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
//                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
//                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
//                                    CompanyName =dr["UNITNAME"].ToString(),
//                                    CreditCode=dr["CreditCode"].ToString(),
//                                    Appointment=CertificateDAL.GetAppointmentCode(dr),
//                                    TechnicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
//                                    EducationDegree=CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"] == DBNull.Value?"":dr["CULTURALLEVEL"].ToString()),
//                                    Major="",
//                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String("D://WebRoot/ZYRYJG/Images/photo_ry.jpg")),
//                                    CertState = "99",//01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
//                                    CertStatusDescription = "持证人正在办理跨企业变更业务，办理完成前本证书暂不可用",
//                                    AssociatedZzeCertID = "",
//                                    OperateType = "02"//01	新发电子证照,02	延续（变更）,03	暂扣,04	过期失效,05	注销（撤销、办理转出）,06	暂扣发还
//                                }
//                            }, Url_Agry_Accept);

//                    switch (result.ReturnCode)
//                    {
//                        case "1"://成功
//                        case "2"://成功，有预警
        //                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS] = '质安网挂起',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CertificateCAID]='{1}'  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新挂起状态
//                            FileLog.WriteLog(string.Format("挂起证书{0}成功。", dr["CERTIFICATECODE"]));
//                            break;
//                        case "0":
//                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                            , dr["CERTIFICATEID"]
//                            , result.ReturnData.ErrorData[0].ErrorMsg
//                            , EnumManager.EleCertDoStep.SetTemp
//                            ));//更新电子证书生成结果

//                            //CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CertificateCAID]='{1}'  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"],_ECertID));//更新挂起状态
//                            //WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "挂起",
//                            //    string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
//                            //);



//                            FileLog.WriteLog(string.Format("挂起证书{0}失败。{1}，{2}", dr["CERTIFICATECODE"], result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg));
//                            break;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("证书挂起失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "挂起", ex.Message);
//                    continue;
//                }
//            }
//        }

//        //证书数据归集接口：从业人员证书归集电子证书(注销、离京、过期不用归集)（部署在192.168.150.175)
//        private void CongYe_UpCertUrl(DateTime startTime)
//        {
//            FileLog.WriteLog("开始从业人员证书归集电子证书CongYe_UpCertUrl()");

//            string sql = "";
//            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
//            {
//                sql = String.Format(@"select top {0} ca_count = (select count(*) from [dbo].[CertificateCAHistory] where [CERTIFICATEID]=c.[CERTIFICATEID]),c.*
//                                            FROM [dbo].[CERTIFICATE] c                                            
//                                            where c.posttypeid <3   
//                                            and 
//                                            (
//	                                            (
//		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
//		                                            and c.[ReturnCATime]>c.[QRCodeTime] and c.[Ofd_ReturnCATime]>c.[QRCodeTime] 
//	                                            )
//                                            )
//                                            and ((c.[QRCodeTime] is not null and  c.[ZZUrlUpTime] is null) or ( c.[ZZUrlUpTime] < c.[QRCodeTime])) 
//                                            and c.CERTIFICATEID < {1} 
//                                            and (   
//                                                    c.EleCertErrTime <DATEADD(hour,-4, GETDATE()) 
//                                                    or c.EleCertErrTime is null
//                                            )
//                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"]);
//            }
//            else
//            {
//                sql = String.Format(@"select top {0} ca_count = (select count(*) from [dbo].[CertificateCAHistory] where [CERTIFICATEID]=c.[CERTIFICATEID]),c.*
//                                            FROM [dbo].[CERTIFICATE] c                                            
//                                            where c.posttypeid <3   
//                                            and 
//                                            (
//	                                            (
//		                                            c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
//		                                            and c.[ReturnCATime]>c.[QRCodeTime] and c.[Ofd_ReturnCATime]>c.[QRCodeTime] 
//	                                            )
//                                            )
//                                            and ((c.[QRCodeTime] is not null and  c.[ZZUrlUpTime] is null) or ( c.[ZZUrlUpTime] < c.[QRCodeTime])) 
//                                            and c.CERTIFICATEID < {1} 
//                                            and (   
//                                                    c.EleCertErrTime <DATEADD(hour,-4, GETDATE()) 
//                                                    or c.EleCertErrTime is null
//                                            )
//                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"]);

//            }


//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                cursor_long["cursor_CongYe_UpCertUrl"] = long.MaxValue - 1;//记录扫描位置

//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                cursor_long["cursor_CongYe_UpCertUrl"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
//            }

//            gb.Upcert.ResponseResult result = null;
//            string key = "";//二维码赋码key
//            string tmp_zzbs;//电子证照国标标识（无校验位）
//            List<gb.Upcert.AcceptData> list = null;
//            int i = 0;
//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                if (i % 5 == 0)
//                {
//                    Thread.Sleep(1000);//暂停1秒
//                }
//                i += 1;
//                try
//                {
//                    key = Utility.Cryptography.RSADecryptJava(gb_privateKeyJava, dr["QRCodeKey"].ToString());

//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
//                            //其中电子证照根代码固定为“1.2.156.3005.2”
//                            //证照类型代码固定为“11100000000013338W011”
//                            //证照颁发机构代码：11110000000021135M
//                            //顺序号(证书编号后11位，即4位年+7位当年流水号)
//                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
//                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
//                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
//                                               "1.2.156.3005.2",//电子证照根代码
//                                               "11100000000013338W011",//证照类型代码
//                                               "11110000000021135M",//证照颁发机构代码
//                                               dr["CERTIFICATECODE"].ToString().Substring(dr["CERTIFICATECODE"].ToString().Length - 13).Replace("（", "").Replace("）", ""),//顺序号：前4位用证照颁发年份，后7位用当年流水次序号。
//                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

//                            list = new List<gb.Upcert.AcceptData>
//                            {
//                                new gb.Upcert.AcceptData
//                                {
//                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
//                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
//                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
//                                }
//                            };

//                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken, list, Url_Agry_DzzGjAccept);
//                            break;
//                        case "2"://特种作业
//                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
//                            //其中电子证照根代码固定为“1.2.156.3005.2”
//                            //证照类型代码固定为“11100000000013338W032”
//                            //证照颁发机构代码：11110000000021135M
//                            //顺序号(证书编号后10位，即4位年+6位当年流水号)
//                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
//                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
//                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
//                                               "1.2.156.3005.2",//电子证照根代码
//                                               "11100000000013338W032",//证照类型代码
//                                               "11110000000021135M",//证照颁发机构代码
//                                               dr["CERTIFICATECODE"].ToString().Substring(dr["CERTIFICATECODE"].ToString().Length - 10),//顺序号：前4位用证照颁发年份，后6位用当年流水次序号。
//                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

//                            list = new List<gb.Upcert.AcceptData>
//                            {
//                                new gb.Upcert.AcceptData
//                                {
//                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
//                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, Utility.Check.GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
//                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
//                                }
//                            };

//                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken_tz, list, Url_Tzry_DzzGjAccept);
//                            break;
//                        default:
//                            continue;

//                    }
//                    if (result.ReturnCode == "1")
//                    {
        //                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZZUrlUpTime]=GETDATE(),[zzeCertID] ='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], list[0].ZzeCertID));//更新归集时间
//                        FileLog.WriteLog(string.Format("归集证书{0}成功。", dr["CERTIFICATECODE"]));
//                    }
//                    else
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
//                          , dr["CERTIFICATEID"]
//                          , result.ReturnData.ErrorData[0].ErrorMsg
//                          , EnumManager.EleCertDoStep.UpCertUrl
//                          ));//更新电子证书生成结果

//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("归集失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", ex.Message);
//                    continue;
//                }
//            }
//        }

//        /// <summary>
//        /// 质量安全网变更身份证号接口：触发筛选条件 [EleCertErrDesc] like '%证书编号已存在%' and [CASESTATUS] ='变更质安网身份证号'
//        /// </summary>
//        public void ChangeIDCard(DateTime startTime)
//        {
//            string sql = String.Format(@"select top {0} *
//                                        FROM [dbo].[CERTIFICATE]                                             
//                                        where posttypeid <3 and  VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <>'注销' and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
//                                        and [EleCertErrDesc] like '%证书编号已存在%' and [CASESTATUS] ='变更质安网身份证号'  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"], EnumManager.EleCertDoStep.UpCertUrl);
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            gb.Change.ResponseResult result = null;

//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Change.Core<gb.Change.Aqscglry.AcceptData>.changeIDCard(gb_accessToken,
//                            new List<gb.Change.Aqscglry.AcceptData>
//                            {
//                                new gb.Change.Aqscglry.AcceptData
//                                {
//                                    eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
//                                    provinceNum="110000",
//                                    certNum=dr["CertificateCode"].ToString(),
//                                    categoryCode=CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    birthDate= Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"),
//                                    identityCardType=(Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
//                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
//                                }
//                            },
//                            Url_Agry_Change);
//                            break;
//                        case "2"://特种作业
//                            //将查询sql posttypeid <2 改为 posttypeid <3
//                            result = gb.Change.Core<gb.Change.Tzzy.AcceptData>.changeIDCard(gb_accessToken_tz,
//                            new List<gb.Change.Tzzy.AcceptData>
//                            {
//                                new gb.Change.Tzzy.AcceptData
//                                {
//                                    eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
//                                    provinceNum="110000",
//                                    certNum=dr["CertificateCode"].ToString(),
//                                    birthDate= Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"),
//                                    identityCardType=(Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
//                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
//                                }
//                            },
//                            Url_Tzry_Change);
//                            break;
//                        default:
//                            continue;

//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));//

//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", "质量安全网变更身份证关键信息成功");
//                        FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息成功。", dr["CERTIFICATECODE"]));
//                    }
//                    else
//                    {

//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网变更身份证关键信息失败：{0}", result.ReturnData.ErrorData[0].ErrorMsg));
//                        FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息失败：{1}", dr["CERTIFICATECODE"], result.ReturnData.ErrorData[0].ErrorMsg));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog(string.Format("质量安全网证书{0}变更身份证关键信息失败。", dr["CertificateCode"]), ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网变更身份证关键信息失败：{0}", ex.Message));
//                    continue;
//                }
//            }

//        }

//        /// <summary>
//        /// 质量安全原网变更已上传失效数据接口：触发筛选条件 [CASESTATUS] ='更新质安网数据'
//        /// </summary>
//        public void UpdataZAData(DateTime startTime)
//        {
//            string sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
//                                        FROM [dbo].[CERTIFICATE] c 
//                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
//                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
//                                        where c.posttypeid <3 and  c.[CASESTATUS] ='更新质安网数据' ", MaxCountExe);
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (GBLogin(Url_Token) == false) return;

//            gb.Update.ResponseResult result = null;

//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (ZhiAnFunStartTime != startTime) return;
//                try
//                {
//                    switch (dr["PostTypeID"].ToString())
//                    {
//                        case "1"://三类人
//                            result = gb.Update.Core<gb.Update.Aqscglry.AcceptData>.UpdateCertData(gb_accessToken,
//                            new List<gb.Update.Aqscglry.AcceptData>
//                            {
//                                new gb.Update.Aqscglry.AcceptData
//                                {    
//                                    //eCertID=( Convert.ToDateTime(dr["ApplyCATime"])< Convert.ToDateTime("2023-03-08 20:42:00")?dr["CertificateCAID"].ToString():dr["CertificateCAID"].ToString().Replace("-","")),
//                                    eCertID=dr["CertificateCAID"].ToString().Replace("-",""),
//                                    provinceNum="110000",
//                                    certNum=dr["CertificateCode"].ToString(),
//                                    categoryCode= CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
//                                    certState =  CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),//01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
//                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString(),
//                                    creditCode=dr["CreditCode"].ToString(),
//                                    //technicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
//                                    operateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
        //                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
//                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first||dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"01":"02")//01	新发电子证照,02	延续（变更）,03	暂扣,04	过期失效,05	注销（撤销、办理转出）,06	暂扣发还
//                                }
//                            },
//                            Url_Agry_Update);
//                            break;
//                        case "2"://特种作业
//                            result = gb.Update.Core<gb.Update.Tzzy.AcceptData>.UpdateCertData(gb_accessToken_tz,
//                            new List<gb.Update.Tzzy.AcceptData>
//                            {
//                                new gb.Update.Tzzy.AcceptData
//                                {    
//                                    provinceNum="110000",
//                                    certNum=dr["CertificateCode"].ToString(),
//                                    operationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
//                                    certState =  CertificateDAL.Get_GB_Tzzy_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
//                                    identityCard=dr["WORKERCERTIFICATECODE"].ToString()
//                                }
//                            },
//                            Url_Tzry_Update);
//                            break;
//                        default:
//                            continue;

//                    }
//                    if (result.ReturnCode == "1")
//                    {
//                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [CASESTATUS]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));
//                        FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息成功。", dr["CERTIFICATECODE"]));
//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", "质量安全网更新已为终结状态（注销、撤销、吊销）证书信息成功");
//                    }
//                    else
//                    {
//                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质质量安全网更新已为终结状态（注销、撤销、吊销）证书信息失败：{0}", result.ReturnData.ErrorData[0].ErrorMsg));
//                        FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息失败：{1}", dr["CERTIFICATECODE"], result.ReturnData.ErrorData[0].ErrorMsg));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog(string.Format("质量安全网证书{0}更新已为终结状态（注销、撤销、吊销）证书信息失败。", dr["CertificateCode"]), ex);
//                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", string.Format("质量安全网更新已为终结状态（注销、撤销、吊销）证书信息失败：{0}", ex.Message));
//                    continue;
//                }
//            }


//        }

        #endregion


        //为从业人员证书赋码准备免冠照片,拷贝到109（部署在21上）
        private void CongYe_FaceImg()
        {

//            string sql = "";
//            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
//            {
//                sql = String.Format(@"select top {0} c.*
//                                            FROM [dbo].[CERTIFICATE] c                                            
//                                            where c.posttypeid <3  
//                                            and 
//                                            (
//	                                            (
//		                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')
//		                                            and c.[ZACheckResult]>0 and ((c.[ZACheckTime] is not null and  c.[ZAFaceImgTime] is null) or ( c.[ZAFaceImgTime] < c.[ZACheckTime])) 
//	                                            )
//                                                or 
//	                                            (
//		                                            c.CHECKDATE > '2023-01-01' and (c.[STATUS] ='注销' or c.[STATUS] ='离京变更') and ( c.[ZAFaceImgTime] < c.[CHECKDATE] or c.[ZAFaceImgTime] is null)
//	                                            )
//	                                            or
//	                                            (
//		                                            c.VALIDENDDATE >  '2023-02-28' and c.VALIDENDDATE < dateadd(day,-1,getdate()) and ( c.[ZAFaceImgTime] < c.VALIDENDDATE or c.[ZAFaceImgTime] is null)
//	                                            )		
//                                            )                                        
//                                            and c.CERTIFICATEID < {1} 
//                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_FaceImg"]);
//            }
//            else
//            {
//                sql = String.Format(@"select top {0} c.*
//                                            FROM [dbo].[CERTIFICATE] c                                            
//                                            where c.posttypeid <3   
//                                            and 
//                                            (
//	                                            (
//		                                            (c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批')
//		                                            and c.[ZACheckResult]>0 and ((c.[ZACheckTime] is not null and  c.[ZAFaceImgTime] is null) or ( c.[ZAFaceImgTime] < c.[ZACheckTime])) 
//	                                            )
//                                                or 
//	                                            (
//		                                            c.CHECKDATE > '2023-01-01' and (c.[STATUS] ='注销' or c.[STATUS] ='离京变更') and ( c.[ZAFaceImgTime] < c.[CHECKDATE] or c.[ZAFaceImgTime] is null)
//	                                            )
//	                                            or
//	                                            (
//		                                            c.VALIDENDDATE >  '2023-02-28' and c.VALIDENDDATE < dateadd(day,-1,getdate()) and ( c.[ZAFaceImgTime] < c.VALIDENDDATE or c.[ZAFaceImgTime] is null)
//	                                            )		
//                                            ) 
//                                            and c.CERTIFICATEID < {1} 
//                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_FaceImg"]);


//            }

            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                sql = String.Format(@"select top {0} c.*
                                            FROM [dbo].[CERTIFICATE] c                                            
                                            where c.posttypeid <3  
                                            and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                            and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
		                                    and (c.[ZAFaceImgTime] is null or c.[ZAFaceImgTime] < c.[CHECKDATE]) 
                                            and c.CERTIFICATEID < {1} 
	                                        union
                                            select top {0} c.*
                                            FROM [dbo].[CERTIFICATE] c 
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus] = '申请暂扣' or p.[PauseStatus] = '申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            inner join (
	                                            select distinct workercertificatecode,unitcode
	                                            FROM [dbo].[CERTIFICATE]
	                                            where posttypeid = 1
	                                            and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
		                                        and [ZAFaceImgTime] < [CHECKDATE]
	                                            and ([EleCertErrDesc] like '%必须在同一个企业下%')
                                            ) t
                                            on c.workercertificatecode = t.workercertificatecode and c.UNITCODE = t.UNITCODE
                                            where c.posttypeid = 1     
                                                and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
                                                and  ( c.EleCertErrTime < DATEADD(hour,-1, GETDATE())  or c.EleCertErrTime is null )   
                                                and  exists (
	                                                select count(*) FROM [dbo].[CERTIFICATE]
	                                                where [CERTIFICATE].workercertificatecode = t.workercertificatecode and [CERTIFICATE].UNITCODE = t.UNITCODE
	                                                and posttypeid = 1
	                                                and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
	                                                having count(*) >1
                                                )
                                                and c.[ZAFaceImgTime] < c.[CHECKDATE]
                                                and p.[PauseID] is null                                                   
                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_FaceImg"]);
            }
            else
            {
                sql = String.Format(@"select top {0} c.*
                                            FROM [dbo].[CERTIFICATE] c                                            
                                            where c.posttypeid <3  
                                            and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                            and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
		                                    and (c.[ZAFaceImgTime] is null or c.[ZAFaceImgTime] < c.[CHECKDATE]) 
                                            and c.CERTIFICATEID < {1} 
	                                        union
                                            select top {0} c.*
                                            FROM [dbo].[CERTIFICATE] c 
                                            left join [dbo].[CertificatePause] p on (p.[PauseStatus]='申请暂扣' or p.[PauseStatus]='申请返还') and  p.[CertificateCode] = c.[CertificateCode]
                                            inner join (
	                                            select distinct workercertificatecode,unitcode
	                                            FROM [dbo].[CERTIFICATE]
	                                            where posttypeid = 1
	                                            and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'
		                                        and [ZAFaceImgTime] < [CHECKDATE]
	                                            and ([EleCertErrDesc] like '%必须在同一个企业下%')
                                            ) t
                                            on c.workercertificatecode = t.workercertificatecode and c.UNITCODE = t.UNITCODE
                                            where c.posttypeid = 1     
                                                and c.VALIDENDDATE > dateadd(day,-1,getdate()) and c.[STATUS] <> '注销' and c.[STATUS] <> '离京变更' and c.[STATUS] <> '待审批' and c.[STATUS] <> '进京待审批'
                                                and  ( c.EleCertErrTime < DATEADD(hour,-1, GETDATE())  or c.EleCertErrTime is null )   
                                                and  exists (
	                                                select count(*) FROM [dbo].[CERTIFICATE]
	                                                where [CERTIFICATE].workercertificatecode = t.workercertificatecode and [CERTIFICATE].UNITCODE = t.UNITCODE
	                                                and posttypeid = 1
	                                                and VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <> '注销' and [STATUS] <> '离京变更' and [STATUS] <> '待审批' and [STATUS] <> '进京待审批'	                                               
	                                                having count(*) >1
                                                )
                                                and c.[ZAFaceImgTime] < c.[CHECKDATE]
                                                and p.[PauseID] is null
                                            order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_FaceImg"]);


            }

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CongYe_FaceImg"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CongYe_FaceImg"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }
            string save_pdf_url = "";//照片存放位置
            string _sfz = "";
            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (dr["FACEPHOTO"] == DBNull.Value || dr["FACEPHOTO"] == null || dr["FACEPHOTO"].ToString().Length < 5) continue;

                try
                {
                    save_pdf_url = dr["FACEPHOTO"].ToString().Replace("~", ExamWebRoot);
                    if (File.Exists(save_pdf_url) == true)
                    {
                        File.Copy(save_pdf_url, string.Format(@"{0}\{1}.jpg", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", dr["CertificateID"]), true);//拷贝照片到150.175

                        //更新证书表,免冠照片发送签章服务器时间
                        CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [ZAFaceImgTime]=GETDATE()  where CertificateID={0}", dr["CertificateID"]));
                        FileLog.WriteLog(string.Format("同步证书{0}一寸免冠照片到109成功。", dr["CERTIFICATECODE"]));
                    }
                    else
                    {
                        _sfz = dr["WORKERCERTIFICATECODE"].ToString();
                        save_pdf_url = string.Format("{0}/Upload/WorkerPhoto/{1}/{2}.jpg", ExamWebRoot, _sfz.Substring(_sfz.Length - 3, 3), _sfz);
                        if (File.Exists(save_pdf_url) == true)
                        {
                            File.Copy(save_pdf_url, string.Format(@"{0}\{1}.jpg", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", dr["CertificateID"]), true);//拷贝照片到150.175

                            //更新证书表,免冠照片发送签章服务器时间
                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [ZAFaceImgTime]=GETDATE()  where CertificateID={0}", dr["CertificateID"]));
                            FileLog.WriteLog(string.Format("同步证书{0}一寸免冠照片到109成功。", dr["CERTIFICATECODE"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("同步证书一寸免冠照片失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "备图", ex.Message);
                    continue;
                }
            }
        }

        //从业人员生成国标待签章的电子证书（部署在21上）
        private void CongYe_CreateGuoBiaoPdf()
        {
            // 获取带签章的证书(过滤：1、三类人和特种作业证书创建国标电子证书；2、证书修改时间大于申请电子证书时间（证书有变动）；3、有一寸照片) 


            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                sql = String.Format(@"Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        left join [dbo].[CertificatePause] p on p.[PauseStatus]='已暂扣' and  p.[CertificateCode] = c.[CertificateCode]
                                        Where c.PostTypeID < 3 and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and ((c.[ApplyCATime] is null and c.[QRCodeTime] is not null) or c.[ApplyCATime] < c.[QRCodeTime]) 
                                        and c.QRCodeCertid is not null
                                        and len(c.FACEPHOTO) > 3 and len(c.unitcode) >7
                                        and c.CERTIFICATEID < {1}
                                        and (
                                            c.EleCertErrTime <DATEADD(hour,-24, GETDATE()) 
                                            or c.EleCertErrTime is null 
                                        )
                                        and p.PauseStatus is null
                                        order by c.CERTIFICATEID desc ", MaxCountExe, cursor_long["cursor_CongYe_CreateGuoBiaoPdf"]);
            }
            else
            {
                sql = String.Format(@"Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        left join [dbo].[CertificatePause] p on p.[PauseStatus]='已暂扣' and  p.[CertificateCode] = c.[CertificateCode]
                                        Where c.PostTypeID < 3 and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and ((c.[ApplyCATime] is null and c.[QRCodeTime] is not null) or c.[ApplyCATime] < c.[QRCodeTime]) 
                                        and c.QRCodeCertid is not null
                                        and len(c.FACEPHOTO) > 3 and len(c.unitcode) >7
                                        and c.CERTIFICATEID < {1}
                                        and (
                                            c.EleCertErrTime <DATEADD(hour,-24, GETDATE()) 
                                            or c.EleCertErrTime is null 
                                        )
                                        and p.PauseStatus is null
                                        order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc ", MaxCountExe, cursor_long["cursor_CongYe_CreateGuoBiaoPdf"]);

            }

            //            // 测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试*********************************************************************** 
            //            string sql = String.Format(@"Select top {0} 
            //                                            case 
            //                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
            //                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
            //                                            else c.UNITCODE end as 'qydm',
            //                                            c.* 
            //                                        from DBO.CERTIFICATE  c
            //                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
            //                                        Where c.PostTypeID < 3 and c.VALIDENDDATE > getdate()  
            //                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
            //                                        and ((c.[ApplyCATime] is null and c.[QRCodeTime] is not null) or c.[ApplyCATime] < c.[QRCodeTime]) 
            //                                        and (c.[Ofd_SendCATime] <c.[ApplyCATime] or c.[Ofd_SendCATime] is null)
            //                                        and len(c.FACEPHOTO) > 3 and len(c.unitcode) >7
            //                                        and c.CERTIFICATEID < {1}
            //                                        order by c.CERTIFICATEID desc ", MaxCountExe, cursor_long["cursor_CongYe_CreateGuoBiaoPdf"]);
            //            // 测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试***********************************************************************

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_CongYe_CreateGuoBiaoPdf"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["cursor_CongYe_CreateGuoBiaoPdf"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            //pdf目录
            if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot)))
            {
                System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot));
            }

            var template_url = "";//电子证书模板地址
            var save_pdf_url = "";//生成pdf保存位置
            string CertificateCAID = "";//电子证书ID

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false)
                {
                    continue;//身份证错误的不生成电子证书，签章服务不接收
                }

                //创建pdf
                CertificateCAID = dr["CertificateCAID"].ToString();

                ////begin测试专用********************************************************************************************
                //save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}_Ofd.pdf", ExamWebRoot, CertificateCAID);//目标文件地址
                ////end测试专用**********************************************************************************************

                //正式证书存放地址
                save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, CertificateCAID);//目标文件地址

                switch (dr["PostTypeID"].ToString())
                {
                    case "1":
                        template_url = string.Format(@"{0}\Template\\国标_三类人.pdf", ExamWebRoot);
                        break;
                    case "2":
                        template_url = string.Format(@"{0}\Template\\国标_特种作业.pdf", ExamWebRoot);
                        break;
                    default:
                        return;
                }

                try
                {
                    var dic = ReadForm(template_url);//读取模板
                    FillForm_GB(template_url, save_pdf_url, dic, dr);//填充模板
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成待签章的证书失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "创建", ex.Message);
                    continue;
                }

                try
                {
                    //更新证书表,写入申请时间


                    //begin测试专用（只拷贝待签章ofd到109）************************************************************************************************************************************
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}_Ofd.pdf", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", CertificateCAID), true);//替换文件
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", CertificateCAID), true);//替换文件

                    //更新证书表,写入发送DFS时间
                    //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [Ofd_SendCATime]='{1}',[ReturnCATime]=null,[Ofd_ReturnCATime] = null where CertificateID={0}", dr["CertificateID"], DateTime.Now));
                    CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [ApplyCATime]='{1}',[SendCATime] = '{1}',[Ofd_SendCATime]='{1}',[ReturnCATime]=null,[Ofd_ReturnCATime] = null,[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where CertificateID={0}", dr["CertificateID"], DateTime.Now));

                    FileLog.WriteLog(string.Format("同步待签章的证书{0}.pdf和ofd到150.175成功。", CertificateCAID));

                    //end*********************************测试专用

                    Thread.Sleep(500);//暂停0.5秒

                    ////删除临时文件pdf ,改在电子证书签章后取回时删除。
                    //File.Delete(save_pdf_url);

                    FileLog.WriteLog(string.Format("创建电子证书{0}.pdf和ofd成功。", CertificateCAID));
                }
                catch (Exception ex)
                {
                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                         , dr["CERTIFICATEID"]
                         , ex.Message
                         , EnumManager.EleCertDoStep.ReturnCert
                         ));//更新电子证书生成结果

                    FileLog.WriteLog("保存待签章的证书信息失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// 向国标pdf模版填充内容，并生成新的文件
        /// </summary>
        /// <param name="pdfTemplate">模版路径</param>
        /// <param name="newFile">生成文件保存路径</param>
        /// <param name="dic">标签字典(即模版中需要填充的控件列表)</param>
        /// <param name="dr">数据行</param>
        public void FillForm_GB(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            switch (dr["PostName"].ToString())
            {
                case "企业主要负责人":
                    dic["PostName"] = string.Format("建筑施工{0}", dr["PostName"]);
                    break;
                case "项目负责人":
                case "土建类专职安全生产管理人员":
                case "机械类专职安全生产管理人员":
                case "综合类专职安全生产管理人员":
                    dic["PostName"] = string.Format("建筑施工企业{0}", dr["PostName"]);
                    break;
                case "汽车起重机司机":
                    dic["PostName"] = string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）", dr["PostName"]);
                    break;
                default:
                    dic["PostName"] = dr["PostName"].ToString();
                    break;
            }

            dic["WorkerName"] = dr["WorkerName"].ToString();
            dic["WorkerCertificateCode"] = dr["WorkerCertificateCode"].ToString();
            dic["Sex"] = dr["Sex"].ToString();
            dic["Birthday"] = Convert.ToDateTime(dr["Birthday"]).ToString("yyyy年M月d日");
            dic["UnitName"] = dr["UnitName"].ToString();
            dic["CertificateCode"] = dr["CertificateCode"].ToString();
            dic["ValidStartDate"] = Convert.ToDateTime(dr["CheckDate"]).ToString("yyyy年M月d日");//有效期起始
            dic["ValidEndDate"] = Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy年M月d日");//有效期截至
            dic["ConferDate"] = Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy年M月d日");//初次发证日期
            dic["CheckDateYear"] = Convert.ToDateTime(dr["CheckDate"]).Year.ToString();//发证日期年份
            dic["CheckDateMonth"] = Convert.ToDateTime(dr["CheckDate"]).Month.ToString();//发证日期月份
            dic["CheckDateDay"] = Convert.ToDateTime(dr["CheckDate"]).Day.ToString();//发证日期日期
            //dic["CheckDateYear"] = Convert.ToDateTime(dr["CheckDate"]).Year.ToString() + "　";//发证日期年份
            //dic["CheckDateMonth"] = Convert.ToDateTime(dr["CheckDate"]).Month.ToString() + "　";//发证日期月份
            //dic["CheckDateDay"] = Convert.ToDateTime(dr["CheckDate"]).Day.ToString() + "　";//发证日期日期
            dic["SkillLevel"] = CertificateDAL.GetAppointmentName(dr);//职务
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

                string iTextAsianCmaps_Path = string.Format(@"{0}\bin\iTextAsianCmaps.dll", ExamWebRoot);
                string iTextAsian_Path = string.Format(@"{0}\bin\iTextAsian.dll", ExamWebRoot);
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);//宋体填充   

                pdfFormFields.AddSubstitutionFont(baseFont);
                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }

                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印

                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetFaceImagePath(dr["FacePhoto"].ToString(), dr["WorkerCertificateCode"].ToString()));
                image.GrayFill = 100;//透明度，灰色填充
                //image.ScaleAbsolute(110, 140);
                image.ScaleAbsolute(77, 98);
                image.SetAbsolutePosition(420, 465);
                waterMarkContent.AddImage(image);//加水印

                //生成二维码  
                string myKey = Utility.Cryptography.RSADecryptJava(gb_privateKeyJava, dr["QRCodeKey"].ToString());//解密后的decryKey
                string myID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(), myKey);//解密后的certid
                //certype 建筑施工特种作业人员操作资格证书=11100000000013338W032
                //certype 安全生产管理人员安全生产考核合格证书=11100000000013338W011
                string codeUrl = string.Format("https://zlaq.mohurd.gov.cn/fwmh/middlepage.html?ID={0}&k={1}&certype={2}&province=110000"
                    , Utility.Cryptography.DESEncrypt(myID, myKey)
                    , Utility.Cryptography.RSAEncryptJava(gb_publicKeyJava, myKey)
                    , (dr["PostTypeID"].ToString() == "1" ? "11100000000013338W011" : "11100000000013338W032")
                    );

                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(codeUrl, 300, 300);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
                imgCode.ScaleAbsolute(77, 77);
                imgCode.SetAbsolutePosition(100, 170);
                imgCode.SetAbsolutePosition(100, dr["PostTypeID"].ToString() == "1" ? 145 : 170);
                waterMarkContent.AddImage(imgCode);//加水印
                pdfStamper.FormFlattening = true;

            }
            catch (Exception ex)
            {
                throw new Exception("生成国标电子证书错误", ex);
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
        /// 从业人员证书Ofd签章（部署在192.168.150.175)
        /// </summary>
        public void IssueCA_CongYeOfd(DateTime startTime)
        {
            string save_pdf_url = "";//目标文件地址

            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                //                sql = String.Format(@"
                //                                        Select top {0} c.* ,u.CreditCode,w.CULTURALLEVEL
                //                                        from DBO.CERTIFICATE  c
                //                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                //                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                //                                
                //                                        Where c.PostTypeID<3    
                //                                        and c.VALIDENDDATE > getdate() 
                //                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                //                                        and c.[Ofd_SendCATime] <'{1}' 
                //                                        and (c.[Ofd_SendCATime] >c.[Ofd_SignCATime] or (c.[Ofd_SendCATime] is not null and c.[Ofd_SignCATime] is null))
                //                                            and c.CERTIFICATEID < {2}
                //                                        order by c.[Ofd_SendCATime] ,c.CERTIFICATEID desc  ", MaxCountExe, DateTime.Now.AddMinutes(-2), cursor_long["IssueCA_Ofd_Cur_CertificateID"]);
                sql = String.Format(@"
                                        Select top {0} c.* ,u.CreditCode,w.CULTURALLEVEL
                                        from DBO.CERTIFICATE  c
                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                
                                        Where c.PostTypeID<3    
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and c.[Ofd_SendCATime] <'{1}' 
                                        and (c.[Ofd_SendCATime] >c.[Ofd_SignCATime] or (c.[Ofd_SendCATime] is not null and c.[Ofd_SignCATime] is null))
                                        and c.[Ofd_SendCATime] < DATEADD(MINUTE,-2, GETDATE())
                                        and c.CERTIFICATEID < {2}       
                                        and 
                                            (
                                                c.EleCertErrTime <DATEADD(hour,-24, GETDATE())
                                                or c.EleCertErrTime is null                                               
                                            )                                 
                                        order by c.CERTIFICATEID desc  ", MaxCountExe, DateTime.Now.AddMinutes(-2), cursor_long["IssueCA_Ofd_Cur_CertificateID"]);
            }
            else
            {
                sql = String.Format(@"
                                        Select top {0} c.* ,u.CreditCode,w.CULTURALLEVEL
                                        from DBO.CERTIFICATE  c
                                        inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
                                        left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
                                
                                        Where c.PostTypeID<3    
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and c.[Ofd_SendCATime] <'{1}' 
                                        and (c.[Ofd_SendCATime] >c.[Ofd_SignCATime] or (c.[Ofd_SendCATime] is not null and c.[Ofd_SignCATime] is null))
                                        and c.[Ofd_SendCATime] < DATEADD(MINUTE,-2, GETDATE())
                                        and c.CERTIFICATEID < {2}
                                        and 
                                            (
                                                c.EleCertErrTime <DATEADD(hour,-24, GETDATE())
                                                or c.EleCertErrTime is null                                               
                                            )  
                                        order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc  ", MaxCountExe, DateTime.Now.AddMinutes(-2), cursor_long["IssueCA_Ofd_Cur_CertificateID"]);


            }


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["IssueCA_Ofd_Cur_CertificateID"] = long.MaxValue - 1;//记录扫描位置
                if (dtOriginal.Rows.Count == 0)
                {
                    IssueCA_Ofd_congyeing = false;
                    return;
                }
            }
            else
            {
                cursor_long["IssueCA_Ofd_Cur_CertificateID"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            try
            {
                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        IssueCA_Ofd_congyeing = false;
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。");
                IssueCA_Ofd_congyeing = false;
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                try
                {
                    if (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false)
                    {
                        continue;//身份证错误的不生成电子证书，签章服务不接收
                    }
                    if (dr["Ofd_license_code"] != DBNull.Value && dr["Ofd_license_code"] != null)
                    {
                        //废弃
                        AbolishResponseResult abolishResult = Execute.AbolishOfd(IssueCA_accessToken, dr, false);
                        if (abolishResult.AckCode != "SUCCESS")
                        {
                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的Ofd失败！错误：{1}", dr["CertificateCode"], JSON.Encode(abolishResult.Errors)));
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "废止", JSON.Encode(abolishResult.Errors));
                            //continue;

                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
                            {
                                CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [Ofd_license_code]=null,[Ofd_auth_code]=null where CertificateID={0}", dr["CertificateID"]));
                            }
                        }
                        else
                        {
                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [Ofd_license_code]=null,[Ofd_auth_code]=null where CertificateID={0}", dr["CertificateID"]));
                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】Ofd电子证书。", dr["CertificateCode"]));
                        }

                    }

                    save_pdf_url = string.Format(@"D:\\zzk\EXAM_CA\GZJG\{0}.ofd", dr["CertificateCAID"]);
                    var model = new CreateRequestGBMdl
                    {
                        Data = new CreateRequestDataGB { }
                    };

                    model.Data.IniPropoertyOfd(dr);

                    FileLog.WriteLog(string.Format("准备签章Ofd证书，证书编号：{0}。", dr["CertificateCode"]));
                    CreateResponseResult result = Execute.CreateOfd(IssueCA_accessToken, dr["PostTypeID"].ToString(), model);

                    if (result != null)
                    {
                        //签发
                        if (result.AckCode == "SUCCESS" && result.Data != null)
                        {
                            FileLog.WriteLog(string.Format("成功对编号【{0}】Ofd电子证书签章！，[Ofd_license_code]={1}，[Ofd_auth_code]={2}。", dr["CertificateCode"], result.Data.LicenseCode, result.Data.AuthCode));

                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [Ofd_license_code]='{1}',[Ofd_auth_code]='{2}',Ofd_SignCATime=getdate(),[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null where CertificateID={0}", dr["CertificateID"], result.Data.LicenseCode, result.Data.AuthCode));

                            bool downAgain = false;//是否已经再次尝试下载
                        TrydownAgain://第一次下载失败，隔几秒再次尝试
                            //下载pdf
                            //DownResponseResult downResult = Execute.DownOfd(IssueCA_accessToken, result.Data.AuthCode);
                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
                            if (downResult == null)
                            {
                                FileLog.WriteLog(string.Format("下载Ofd返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                                continue;
                            }
                            if (downResult.Data == null)
                            {
                                FileLog.WriteLog(string.Format("下载Ofd返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(downResult.Errors)));
                                WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "下载", JSON.Encode(downResult.Errors));

                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
                                {
                                    Thread.Sleep(5000);//暂停5秒
                                    downAgain = true;
                                    goto TrydownAgain;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);
                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】Ofd电子证书{1}.ofd。", dr["CertificateCode"], dr["CertificateCAID"]));

                                ////删除已处理未盖章文件
                                Thread.Sleep(500);//暂停0.5秒
                                File.Delete(string.Format(@"D:\\zzk\EXAM_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]));

                                //File.Move(save_pdf_url.Replace("GZJG", "DGZ"), save_pdf_url.Replace("GZJG", "BAK_DGZ"));
                            }
                        }
                        else
                        {
                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                                    , dr["CERTIFICATEID"]
                                    , JSON.Encode(result.Errors)
                                    , EnumManager.EleCertDoStep.IssueCA
                                    ));//更新电子证书生成结果

                            FileLog.WriteLog(string.Format("创建Ofd制证数据返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(result.Errors)));
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "签发", JSON.Encode(result.Errors));

                            if (JSON.Encode(result.Errors).Contains("已存在") || JSON.Encode(result.Errors).Contains("存在重复的电子证照"))
                            {
                                try
                                {
                                    AbolishResponseResult abolishResult = Execute.AbolishOfd(IssueCA_accessToken, dr, true);
                                }
                                catch (Exception ex2)
                                {
                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书Ofd失败，错误信息：{1}", dr["CertificateCode"], ex2.Message), ex2);
                                }
                            }
                            continue;
                        }
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("从业人员证书Ofd签章,返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("从业人员证书Ofd签章失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "签发", ex.Message);
                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                                    , dr["CERTIFICATEID"]
                                    , ex.Message
                                    , EnumManager.EleCertDoStep.IssueCA
                                    ));//更新电子证书生成结果
                    continue;
                }
            }
            IssueCA_Ofd_congyeing = false;
        }

        /// <summary>
        /// 取回已签章的OFD证书（部署在5.21)
        /// </summary>
        public void ReturnOFDCA()
        {
            DateTime stratTime = DateTime.Now;
            OfdReturnStartTime = stratTime;
            try
            {
                //FileLog.WriteLog("准备回写已签章的证书");

                //获取尚未成功签章证书信息
                string sql = "";
                DataTable dtOriginal = null;
                string fileFrom = "";//签章结果在109地址
                string fileTo = "";

                //清除取回死锁错误
                CommonDAL.ExecSQL(@"update certificate 
                                set [EleCertErrTime]=null,[EleCertErrStep]=null,[EleCertErrDesc]=null
                                where [EleCertErrStep]='取回' and ([EleCertErrDesc] like '%通信缓冲区 资源上，并且已被选作死锁牺牲品。%' or [EleCertErrDesc]='归集操作太快，请稍后再试！')
                                      and [EleCertErrTime] < dateadd(MINUTE,-5,getdate())");

                

                if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
                {
                    //                    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE 
                    //                                                where PostTypeID<3  and [Ofd_SendCATime] <[Ofd_SignCATime] and [Ofd_ReturnCATime] is null 
                    //                                                and VALIDENDDATE > getdate() and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                    //                                                and CertificateID <{1} and [Ofd_SignCATime] <'{2}'
                    //                                                order by [Ofd_SignCATime] ,CertificateID desc", MaxCountExe, cursor_long["ReturnCA_Cur_CertificateID_OFD"], DateTime.Now.AddMinutes(-5));
                    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE 
                                                where PostTypeID<3  and [Ofd_SendCATime] <[Ofd_SignCATime] and [Ofd_ReturnCATime] is null 
                                                and VALIDENDDATE > dateadd(day,-1,getdate()) 
                                                and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                                                and CertificateID <{1} and [Ofd_SignCATime] <'{2}'
                                                and 
                                                (
                                                    EleCertErrTime <DATEADD(MINUTE,-6, GETDATE())
                                                    or EleCertErrTime is null                                               
                                                )   
                                                order by PostTypeID desc,CertificateID desc", MaxCountExe, cursor_long["ReturnCA_Cur_CertificateID_OFD"], DateTime.Now.AddMinutes(-6));
                }
                else
                {
                    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE 
                                                where PostTypeID<3  and [Ofd_SendCATime] <[Ofd_SignCATime] and [Ofd_ReturnCATime] is null 
                                                and VALIDENDDATE > dateadd(day,-1,getdate()) 
                                                and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                                                and CertificateID <{1} and [Ofd_SignCATime] <'{2}'
                                                and 
                                                (
                                                    EleCertErrTime <DATEADD(MINUTE,-6, GETDATE())
                                                    or EleCertErrTime is null                                               
                                                )   
                                                order by [MODIFYTIME] desc ,CertificateID desc", MaxCountExe, cursor_long["ReturnCA_Cur_CertificateID_OFD"], DateTime.Now.AddMinutes(-6));
                }
                dtOriginal = (new DBHelper()).GetFillData(sql);
                //FileLog.WriteLog(sql);
                if (dtOriginal.Rows.Count < MaxCountExe)
                {
                    cursor_long["ReturnCA_Cur_CertificateID_OFD"] = long.MaxValue - 1;//记录扫描位置
                    if (dtOriginal.Rows.Count == 0)
                    {
                        return;
                    }
                }
                else
                {
                    cursor_long["ReturnCA_Cur_CertificateID_OFD"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                }
                fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";

                foreach (DataRow dr in dtOriginal.Rows)
                {
                    if (stratTime != OfdReturnStartTime) return;
                    try
                    {
                        if (File.Exists(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"])) == true)
                        {
                            //FileLog.WriteLog(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]));
                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
                            if (!Directory.Exists(fileTo))
                            {
                                System.IO.Directory.CreateDirectory(fileTo);
                            }
                            fileTo = string.Format(@"{0}\{1}.ofd", fileTo, dr["CertificateCAID"]);

                            File.Copy(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件

                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [Ofd_ReturnCATime] = getdate(),[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null WHERE [CertificateCAID]='{0}'  and [Ofd_ReturnCATime] is null;", dr["CertificateCAID"]));

                            //File.Move(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.ofd", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));
                            Thread.Sleep(500);//暂停0.5秒
                            File.Delete(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]));

                            FileLog.WriteLog(string.Format("回写已签章的OFD证书{0}成功。", fileTo));

                        }
                        else
                        {
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "取回", string.Format("未找到要取回签章后的文件{0}{1}.ofd", fileFrom, dr["CertificateCAID"]));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  and [Ofd_ReturnCATime] is null;"
                            , dr["CertificateCAID"]
                             , string.Format("未找到要取回签章后的文件{0}{1}.ofd", fileFrom, dr["CertificateCAID"])
                        , EnumManager.EleCertDoStep.ReturnCert
                            ));//更新电子证书生成结果
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  and [Ofd_ReturnCATime] is null;"
                            , dr["CertificateCAID"]
                             , ex.Message
                        , EnumManager.EleCertDoStep.ReturnCert
                            ));//更新电子证书生成结果


                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "取回", ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("取回已签章的OFD证书失败。", ex);
            }
        }

        /// <summary>
        /// 重新下载签章过程中下载失败的pdf或ofd
        /// </summary>
        private void downPDForOFD(DateTime startTime)
        {
            string save_pdf_url = "";//目标文件地址
            DownResponseResult downResult = null;

            string sql = "";

            sql = String.Format(@"
                                        Select top {0} [CertificateCAID],[POSTTYPENAME],[CertificateCode],[auth_code] ,[SignCATime],[ReturnCATime],[Ofd_auth_code],[Ofd_SignCATime],[Ofd_ReturnCATime],[EleCertErrDesc]
                                        from DBO.CERTIFICATE    
                                        Where PostTypeID<3
                                        and VALIDENDDATE > dateadd(day,-1,getdate())  
                                        and [EleCertErrDesc] like '未找到要取回签章后的文件%'
                                        and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                                        and CERTIFICATEID < {1}
                                        and  EleCertErrTime <DATEADD(hour,-1, GETDATE())
                                        order by CERTIFICATEID desc  ", MaxCountExe, cursor_long["DownCA_CertificateID"]);


            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["DownCA_CertificateID"] = long.MaxValue - 1;//记录扫描位置
                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["DownCA_CertificateID"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            try
            {
                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。");
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                try
                {
                    if (dr["EleCertErrDesc"].ToString().IndexOf(".ofd") > 0)
                    {
                        save_pdf_url = string.Format(@"D:\\zzk\EXAM_CA\GZJG\{0}.ofd", dr["CertificateCAID"]);
                        downResult = Execute.DownPDF(IssueCA_accessToken, dr["Ofd_auth_code"].ToString());
                        if (downResult == null)
                        {
                            FileLog.WriteLog(string.Format("下载Ofd电子证书返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  ;"
                            , dr["CertificateCAID"]
                             , "下载Ofd电子证书返回结果为Null"
                        , EnumManager.EleCertDoStep.Download
                            ));//更新电子证书生成结果
                            continue;
                        }
                        if (downResult.Data == null)
                        {
                            FileLog.WriteLog(string.Format("下载Ofd电子证书返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(downResult.Errors)));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  ;"
                            , dr["CertificateCAID"]
                             , "下载Ofd电子证书返回结果Data=Null"
                        , EnumManager.EleCertDoStep.Download
                            ));//更新电子证书生成结果
                        }
                        else
                        {
                            Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);
                            FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】Ofd电子证书{1}.ofd。", dr["CertificateCode"], dr["CertificateCAID"]));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null WHERE [CertificateCAID]='{0}' ;" , dr["CertificateCAID"]));

                            ////删除已处理未盖章文件
                            Thread.Sleep(500);//暂停0.5秒
                            File.Delete(string.Format(@"D:\\zzk\EXAM_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]));
                        }
                    }
                    else if (dr["EleCertErrDesc"].ToString().IndexOf(".pdf") > 0)
                    {
                        save_pdf_url = string.Format(@"D:\\zzk\EXAM_CA\GZJG\{0}.pdf", dr["CertificateCAID"]);
                        downResult = Execute.DownPDF(IssueCA_accessToken, dr["auth_code"].ToString());
                        if (downResult == null)
                        {
                            FileLog.WriteLog(string.Format("下载pdf电子证书返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  ;"
                            , dr["CertificateCAID"]
                             , "下载pdf电子证书返回结果为Null"
                        , EnumManager.EleCertDoStep.Download
                            ));//更新电子证书生成结果
                            continue;
                        }
                        if (downResult.Data == null)
                        {
                            FileLog.WriteLog(string.Format("下载pdf电子证书返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(downResult.Errors)));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  ;"
                            , dr["CertificateCAID"]
                             , "下载pdf电子证书返回结果Data=Null"
                        , EnumManager.EleCertDoStep.Download
                            ));//更新电子证书生成结果
                        }
                        else
                        {
                            Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);
                            FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】pdf电子证书{1}.pdf。", dr["CertificateCode"], dr["CertificateCAID"]));
                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null WHERE [CertificateCAID]='{0}' ;" , dr["CertificateCAID"]));

                            ////删除已处理未盖章文件
                            Thread.Sleep(500);//暂停0.5秒
                            File.Delete(string.Format(@"D:\\zzk\EXAM_CA\DGZ\{0}.pdf", dr["CertificateCAID"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("下载【{0}】电子证书失败，错误信息：{1}" ,dr["CertificateCode"], ex.Message), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), EnumManager.EleCertDoStep.Download, ex.Message);
                    continue;
                }
            }
        }

        //        private void test_CongYe_GetQRCode(DateTime startTime)
        //        {
        //            FileLog.WriteLog("开始测试");
        //            ChangeIDCard(startTime);
        //            UpdataZAData(startTime);
        //            test2(startTime);


        //            string sql = "";

        //            sql = String.Format(@"select top {0} c.*,u.CreditCode,w.CULTURALLEVEL
        //                                            FROM [dbo].[CERTIFICATE] c 
        //                                            inner join Unit u on c.UNITCODE = u.ENT_OrganizationsCode
        //                                            left join WORKER w on c.WORKERCERTIFICATECODE = w.CERTIFICATECODE
        //                                            where c.posttypeid <3 and (c.CERTIFICATECODE='京建安A（2004）0006304'  or c.CERTIFICATECODE='京A042009000017')
        //                                            and 
        //                                            (
        //	                                            (
        //		                                            c.VALIDENDDATE > getdate() and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <> '进京待审批'
        //		                                            and c.[ZACheckResult]>0  and ((c.[ZACheckTime] is not null and  c.[QRCodeTime] is null) or ( c.[QRCodeTime] < c.[ZACheckTime]))
        //		                                            and c.[ZAFaceImgTime] >c.[ZACheckTime]
        //	                                            )
        //	                                            or
        //	                                            (
        //                                                    c.CHECKDATE > '2023-01-01' and (c.[STATUS] ='注销' or c.[STATUS] ='离京变更') and c.[ZAFaceImgTime] >c.[CHECKDATE] and ( c.[QRCodeTime] < c.[CHECKDATE] or c.[QRCodeTime] is null)
        //                                                )
        //			                                    or 
        //                                                (
        //                                                    c.VALIDENDDATE >  '2023-02-28' and c.VALIDENDDATE < dateadd(day,-1,getdate()) and c.[ZAFaceImgTime] >c.VALIDENDDATE and ( c.[QRCodeTime] < c.[VALIDENDDATE] or c.[QRCodeTime] is null)
        //                                                )		                                          
        //                                            )                                              
        //                                            order by c.CERTIFICATEID desc  ", MaxCountExe);


        //            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

        //            if (GBLogin(Url_Token) == false) return;

        //            if (dtOriginal.Rows.Count < MaxCountExe)
        //            {
        //                cursor_long["cursor_CongYe_GetQRCode"] = long.MaxValue - 1;//记录扫描位置

        //                if (dtOriginal.Rows.Count == 0)
        //                {
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                cursor_long["cursor_CongYe_GetQRCode"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
        //            }


        //            gb.Imputation.ResponseResult result = null;
        //            string _ECertID = null;//电子证书ID
        //            int i = 0;
        //            foreach (DataRow dr in dtOriginal.Rows)
        //            {
        //                if (ZhiAnFunStartTime != startTime) return;
        //                if (i % 5 == 0)
        //                {
        //                    Thread.Sleep(1000);//暂停1秒
        //                }
        //                i += 1;

        //                //正式
        //                _ECertID = Guid.NewGuid().ToString();

        //                try
        //                {
        //                    //FileLog.WriteLog(string.Format("pnoto：{0}" , Utility.ImageHelp.ImgToBase64String(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg",dr["CertificateID"]))));

        //                    switch (dr["PostTypeID"].ToString())
        //                    {
        //                        case "1"://三类人
        //                            result = gb.Imputation.Core<gb.Imputation.Aqscglry.AcceptData>.Imputation(gb_accessToken,
        //                            new List<gb.Imputation.Aqscglry.AcceptData>
        //                            {
        //                                new gb.Imputation.Aqscglry.AcceptData
        //                                {
        //                                    ECertID = _ECertID.Replace("-",""),
        //                                    ProvinceNum = "110000",
        //                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
        //                                    IssuAuth = "北京市住房和城乡建设委员会",
        //                                    IssuAuthCode = "11110000000021135M",
        //                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
        //                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
        //                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
        //                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
        //                                    CategoryCode =  CertificateDAL.GetSLRPostCode(dr["POSTNAME"].ToString()),
        //                                    Name = dr["WORKERNAME"].ToString(),
        //                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
        //                                    BirthDate=Convert.ToDateTime(dr["BIRTHDAY"]).ToString("yyyy-MM-dd"),
        //                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
        //                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
        //                                    CompanyName =dr["UNITNAME"].ToString(),
        //                                    CreditCode=dr["CreditCode"].ToString(),
        //                                    Appointment=CertificateDAL.GetAppointmentCode(dr),
        //                                    TechnicalTitle=CertificateDAL.GetTechnicalTitleCode(dr["SKILLLEVEL"] == DBNull.Value?"":dr["SKILLLEVEL"].ToString()),
        //                                    EducationDegree=CertificateDAL.GetEducationDegreeCode(dr["CULTURALLEVEL"] == DBNull.Value?"":dr["CULTURALLEVEL"].ToString()),
        //                                    Major="",
        //                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://WebRoot/CAFile/210504197705200015.jpg",dr["CertificateID"]))),
        //                                    CertState = CertificateDAL.Get_GB_Aqscglry_CertStateCode(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),//01有效，02暂扣，03撤销，04注销，05失效，06办理转出，99其他
        //                                    CertStatusDescription = CertificateDAL.Get_GB_Aqscglry_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),
        //                                    AssociatedZzeCertID = "",
        //                                    //BusinessInformation =[],
        //                                    OperateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout || dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.OutBeiJing)?"05"
        //                                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"//过期也传注销，否则他省不认
        //                                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first||dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.InBeiJing?"01":"02")//01	新发电子证照,02	延续（变更）,03	暂扣,04	过期失效,05	注销（撤销、办理转出）,06	暂扣发还
        //                                }
        //                            }, Url_Agry_Accept);
        //                            break;
        //                        case "2"://特种作业
        //                            result = gb.Imputation.Core<gb.Imputation.Tzzy.AcceptData>.Imputation(gb_accessToken_tz,
        //                            new List<gb.Imputation.Tzzy.AcceptData>
        //                            {
        //                                new gb.Imputation.Tzzy.AcceptData
        //                                {
        //                                    ECertID = _ECertID.Replace("-",""),
        //                                    ProvinceNum = "110000",
        //                                    CertNum = dr["CERTIFICATECODE"].ToString().Replace("(","（").Replace(")","）"),
        //                                    IssuAuth = "北京市住房和城乡建设委员会",
        //                                    IssuAuthCode = "11110000000021135M",
        //                                    IssuedDate = Convert.ToDateTime(dr["CONFERDATE"]).ToString("yyyy-MM-dd"),
        //                                    IssuDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
        //                                    EffectiveDate = Convert.ToDateTime(dr["CHECKDATE"]).ToString("yyyy-MM-dd"),
        //                                    ExpiringDate = Convert.ToDateTime(dr["VALIDENDDATE"]).ToString("yyyy-MM-dd"),
        //                                    Name = dr["WORKERNAME"].ToString(),
        //                                    Gender = (dr["SEX"].ToString()=="女"?"2":"1"),
        //                                    IdentityCard = dr["WORKERCERTIFICATECODE"].ToString(),
        //                                    IdentityCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false?"999":"111"),
        //                                    Photo =string.Format("data:image/jpg;base64, {0}",Utility.ImageHelp.ImgToBase64String(string.Format("D://WebRoot/CAFile/210504197705200015.jpg",dr["CertificateID"]))),
        //                                    OperationCategory=CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString()),
        //                                    CategoryDescription= (CertificateDAL.GetTZZYPostCode(dr["POSTNAME"].ToString())=="99"?string.Format("经省级以上住房和城乡建设主管部门认定的其他工种类别（{0}）",dr["POSTNAME"]):""), //工种类别描述	,工种类别为“99”时进行的补充描述
        //                                    CertState =  (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout ? "04":"01"),//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
        //                                    CertStatusDescription = CertificateDAL.Get_GB_Tzzy_CertStateDesc(dr["STATUS"].ToString(), Convert.ToDateTime(dr["VALIDENDDATE"])),                                    
        //                                    AssociatedZzeCertID = "",
        //                                    //BusinessInformation =[],

        //                                    //01新发电子证照,02延续（变更）,03暂扣,04	过期失效,05	注销（撤销、吊销）,06暂扣发还
        //                                    OperateType = ((dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.Logout)?"05"
        //                                    :Convert.ToDateTime(dr["VALIDENDDATE"])<DateTime.Now.AddDays(-1) ?"05"
        //                                    :dr["STATUS"].ToString()==EnumManager.CertificateUpdateType.first?"01":"02")
        //                                  }
        //                               }, Url_Tzry_Accept);
        //                            break;
        //                    }

        //                    switch (result.ReturnCode)
        //                    {
        //                        case "1"://成功
        //                        case "2"://成功，有预警
        //                            //正式***************************************************************************************************************************
        //                            if (dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.Logout//注销
        //                                || dr["STATUS"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing//离京
        //                                || Convert.ToDateTime(dr["VALIDENDDATE"]) < DateTime.Now.AddDays(-1)//过期
        //                                )
        //                            {
        //                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[CertificateCAID]='{1}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null  where [CERTIFICATEID]={0}", dr["CERTIFICATEID"], _ECertID));//更新赋码时间
        //                            }
        //                            else
        //                            {
        //                                CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [QRCodeTime]=GETDATE(),[QRCodeCertid]='{1}',[QRCodeKey]='{2}',[CertificateCAID]='{3}',[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[ADDITEMNAME]=null   where [CERTIFICATEID]={0}"
        //                                   , dr["CERTIFICATEID"], result.ReturnData.SuccessData[0].EncryCertid, result.ReturnData.SuccessData[0].EncryKey, _ECertID));//更新二维码赋码信息
        //                            }


        //                            //File.Delete(string.Format("D://zzk/EXAM_CA/DGZ/{0}.jpg", dr["CertificateID"]));
        //                            FileLog.WriteLog(string.Format("获取证书{0}二维码赋码成功。", dr["CERTIFICATECODE"]));
        //                            break;
        //                        case "0":
        //                            CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
        //                            , dr["CERTIFICATEID"]
        //                            , result.ReturnData.ErrorData[0].ErrorMsg
        //                            , EnumManager.EleCertDoStep.GetCode
        //                            ));//更新电子证书生成结果

        //                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码",
        //                                string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg)
        //                            );
        //                            break;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    FileLog.WriteLog("赋码失败，错误信息：" + ex.Message, ex);
        //                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "赋码", ex.Message);

        //                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
        //                           , dr["CERTIFICATEID"]
        //                           , ex.Message
        //                           , EnumManager.EleCertDoStep.GetCode
        //                           ));//更新电子证书生成结果
        //                    continue;
        //                }
        //            }


        //        }
        //        protected void test2(DateTime startTime)
        //        {
        //            //FileLog.WriteLog("开始从业人员证书归集电子证书CongYe_UpCertUrl()");



        //            string sql = String.Format(@"select top {0} ca_count = (select count(*) from [dbo].[CertificateCAHistory] where [CERTIFICATEID]=c.[CERTIFICATEID]),c.*
        //                                            FROM [dbo].[CERTIFICATE] c                                            
        //                                            where c.posttypeid <3  and (c.CERTIFICATECODE='京建安A（2004）0006304'  or c.CERTIFICATECODE='京A042009000017') 
        //                                            and 
        //                                            (
        //	                                            (
        //		                                            c.VALIDENDDATE > getdate() and c.[STATUS] <>'注销' and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
        //		                                       
        //	                                            )
        //                                            )
        //                                            and ((c.[QRCodeTime] is not null and  c.[ZZUrlUpTime] is null) or ( c.[ZZUrlUpTime] < c.[QRCodeTime])) 
        //                                            and ((c.EleCertErrStep ='{2}' and c.EleCertErrTime <DATEADD(hour,-4, GETDATE())) or c.EleCertErrTime is null or c.EleCertErrStep <>'{2}')
        //                                            order by c.CERTIFICATEID desc  ", MaxCountExe, cursor_long["cursor_CongYe_UpCertUrl"], EnumManager.EleCertDoStep.UpCertUrl);



        //            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

        //            if (GBLogin(Url_Token) == false) return;


        //            gb.Upcert.ResponseResult result = null;
        //            string key = "";//二维码赋码key
        //            string tmp_zzbs;//电子证照国标标识（无校验位）
        //            List<gb.Upcert.AcceptData> list = null;
        //            int i = 0;
        //            foreach (DataRow dr in dtOriginal.Rows)
        //            {
        //                if (ZhiAnFunStartTime != startTime) return;
        //                if (i % 5 == 0)
        //                {
        //                    Thread.Sleep(1000);//暂停1秒
        //                }
        //                i += 1;
        //                try
        //                {
        //                    key = Utility.Cryptography.RSADecryptJava(gb_privateKeyJava, dr["QRCodeKey"].ToString());

        //                    switch (dr["PostTypeID"].ToString())
        //                    {
        //                        case "1"://三类人
        //                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
        //                            //其中电子证照根代码固定为“1.2.156.3005.2”
        //                            //证照类型代码固定为“11100000000013338W011”
        //                            //证照颁发机构代码：11110000000021135M
        //                            //顺序号(证书编号后11位，即4位年+7位当年流水号)
        //                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
        //                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        //                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
        //                                               "1.2.156.3005.2",//电子证照根代码
        //                                               "11100000000013338W011",//证照类型代码
        //                                               "11110000000021135M",//证照颁发机构代码
        //                                               dr["CERTIFICATECODE"].ToString().Substring(dr["CERTIFICATECODE"].ToString().Length - 13).Replace("（", "").Replace("）", ""),//顺序号
        //                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

        //                            list = new List<gb.Upcert.AcceptData>
        //                            {
        //                                new gb.Upcert.AcceptData
        //                                {
        //                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
        //                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
        //                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
        //                                }
        //                            };

        //                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken, list, Url_Agry_DzzGjAccept);
        //                            break;
        //                        case "2"://特种作业
        //                            //电子证照根代码、证照类型代码、证照颁发机构代码、顺序号、版本号和校验位，各部分之间用点分隔符＂．“分隔
        //                            //其中电子证照根代码固定为“1.2.156.3005.2”
        //                            //证照类型代码固定为“11100000000013338W032”
        //                            //证照颁发机构代码：11110000000021135M
        //                            //顺序号(证书编号后10位，即4位年+6位当年流水号)
        //                            //版本号:初次办理时为“001”，因变更等情况换证时顺序加1。
        //                            //校验位为1 位数字或英文大写字母，用以检验证照标识的正确性。校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        //                            tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",
        //                                               "1.2.156.3005.2",//电子证照根代码
        //                                               "11100000000013338W032",//证照类型代码
        //                                               "11110000000021135M",//证照颁发机构代码
        //                                               dr["CERTIFICATECODE"].ToString().Substring(4, 10),//顺序号
        //                                               (Convert.ToInt32(dr["ca_count"]) + 1).ToString("000"));//版本号

        //                            list = new List<gb.Upcert.AcceptData>
        //                            {
        //                                new gb.Upcert.AcceptData
        //                                {
        //                                    ID = Utility.Cryptography.DESDecrypt(dr["QRCodeCertid"].ToString(),key),
        //                                    ZzeCertID = string.Format("{0}.{1}", tmp_zzbs, GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""))),
        //                                    Url = string.Format("http://120.52.185.14/CertifManage/gbCertView.aspx?o={0}",Utility.Cryptography.Encrypt(string.Format("{0},{1}",dr["CertificateCAID"],dr["CERTIFICATEID"])))
        //                                }
        //                            };

        //                            result = gb.Upcert.Core<gb.Upcert.AcceptData>.UpUrl(gb_accessToken_tz, list, Url_Tzry_DzzGjAccept);
        //                            break;
        //                        default:
        //                            continue;

        //                    }
        //                    if (result.ReturnCode == "1")
        //                    {
        //                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [ZZUrlUpTime]=GETDATE(),[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null,[CASESTATUS]=null where [CERTIFICATEID]={0}", dr["CERTIFICATEID"]));//更新归集时间
        //                        FileLog.WriteLog(string.Format("归集证书{0}成功。", dr["CERTIFICATECODE"]));
        //                    }
        //                    else
        //                    {
        //                        CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
        //                          , dr["CERTIFICATEID"]
        //                          , result.ReturnData.ErrorData[0].ErrorMsg
        //                          , EnumManager.EleCertDoStep.UpCertUrl
        //                          ));//更新电子证书生成结果

        //                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", string.Format("{0}，{1}", result.ReturnMsg, result.ReturnData.ErrorData[0].ErrorMsg));
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    FileLog.WriteLog("归集失败，错误信息：" + ex.Message, ex);
        //                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "归集", ex.Message);
        //                    continue;
        //                }
        //            }
        //        }
        
        #endregion

        #region 社保业务

        //上报二建社保比对数据(作废)
        private void Upload_SBData()
        {
            return;
            bool flag = false;//上传比对数据是否成功
            DataTable dt = null;//待上传的业务数据
            const string publishTime = "2017-02-14";//社保比对功能实施时间
            Dictionary<string, int> uploadCount = new Dictionary<string, int>();

            try
            {
                //获取服务的数据结构
                //DataSet ds = jcsjk.GetIServiceStruct(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8");
                DataSet ds = new DBHelper().GetDataSet(string.Format("select * from SBBD where cqzt=0"), CommandType.Text);
                int count = ds.Tables[0].Rows.Count;
                #region 1、注册申请数据比对（个人信息变更身份证取子表）

                string sql = @"select 
                                    case when c.[PSN_NameTo] is not null and c.[PSN_NameTo]<> a.PSN_Name
                                    then c.PSN_NameTo else a.[PSN_Name] end as 'PSN_Name',
                                    case when c.ToPSN_CertificateNO is not null and c.ToPSN_CertificateNO <> a.PSN_CertificateNO 
                                    then c.ToPSN_CertificateNO else a.[PSN_CertificateNO] end as 'PSN_CertificateNO',
                                    a.[ENT_OrganizationsCode],
                                    a.[ENT_Name],
                                    a.[ApplyTime]
                                from [dbo].[Apply] a
                                left join dbo.ApplyChange c on a.ApplyID = c.ApplyID
                                left join dbo.ShebaoCheck s on  a.ApplyTime > '{0}' and a.[ApplyID] = s.DataID and cast(a.ApplyTime as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                                where  a.ApplyTime > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime));//申请单                

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["LB"] = "03";//类别
                            dr["XM"] = source["PSN_Name"].ToString();//姓名
                            dr["SFZHM"] = source["PSN_CertificateNO"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["PSN_CertificateNO"].ToString()) == false)
                            {
                                continue;
                            }
                            try
                            {
                                dr["ZZJGDM"] = source["ENT_OrganizationsCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["PSN_Name"].ToString(), source["ENT_OrganizationsCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["ENT_Name"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["ApplyTime"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["ApplyTime"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额 
                            dr["CQZT"] = 0;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }

                    //if (ds.Tables[0].Rows.Count >= 150)
                    //{
                    //    //上传
                    //    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                    //    ds.Tables[0].Rows.Clear();//清空数据
                    //}
                }

                if (ds.Tables[0].Rows.Count > count)
                {
                    //社保上传改用作业同步，部署在150.109上作业“抽取资质审批系统社保上报信息+基本信息补充信息+动态监管处理结果+证书丢补”
                    //第2步“抽取执业人员注册监管系统社保上报信息”

                    new DBHelper().SqlBulkCopyByDatatable("SBBD", ds.Tables[0]);
                    flag = true;
                }
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    //上传
                //    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                //}

                //记录上报记录
                if (flag == true)
                {
                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                            select e.[ApplyType],e.ApplyTime,e.ApplyID
                            from [dbo].[Apply] e 
                            left join dbo.ShebaoCheck s on e.ApplyTime > '{0}' and e.ApplyID = s.DataID and e.ApplyTime = s.CHECKTIME
                            where e.ApplyTime > '{0}' and s.CheckID is null";
                    CommonDAL.ExecSQL(string.Format(sql, publishTime));
                    uploadCount.Add("二建注册申请", dt.Rows.Count);
                }

                #endregion



                System.Text.StringBuilder sb = new StringBuilder();
                int countsum = 0;
                foreach (string k in uploadCount.Keys)
                {
                    sb.Append(string.Format("；{0}:{1}条", k, uploadCount[k].ToString()));
                    countsum += uploadCount[k];
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                WriteOperateLog("系统服务", 0, string.Format("上传社保比对数据：{0}条", countsum.ToString()), sb.ToString());

            }
            catch (Exception ex)
            {
                FileLog.WriteLog("上传社保比对数据失败，错误信息：" + ex.Message, ex);
                return;
            }
        }

        //上报二级造价工程师社保比对数据(作废)
        private void Upload_SBDataOfZJGCS()
        {
            return;
            bool flag = false;//上传比对数据是否成功
            DataTable dt = null;//待上传的业务数据
            const string publishTime = "2022-08-30";//社保比对功能实施时间
            Dictionary<string, int> uploadCount = new Dictionary<string, int>();

            try
            {
                //获取服务的数据结构
                //DataSet ds = jcsjk.GetIServiceStruct(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8");
                DataSet ds = new DBHelper().GetDataSet(string.Format("select * from SBBD where cqzt=0"), CommandType.Text);
                int count = ds.Tables[0].Rows.Count;
                #region 1、注册申请数据比对（个人信息变更身份证取子表）

                string sql = @"select 
                                    case when c.[PSN_NameTo] is not null and c.[PSN_NameTo]<> a.PSN_Name
                                    then c.PSN_NameTo else a.[PSN_Name] end as 'PSN_Name',
                                    case when c.ToPSN_CertificateNO is not null and c.ToPSN_CertificateNO <> a.PSN_CertificateNO 
                                    then c.ToPSN_CertificateNO else a.[PSN_CertificateNO] end as 'PSN_CertificateNO',
                                    a.[ENT_OrganizationsCode],
                                    a.[ENT_Name],
                                    a.[ApplyTime]
                                from [dbo].[zjs_Apply] a
                                left join dbo.zjs_ApplyChange c on a.ApplyID = c.ApplyID
                                left join dbo.ShebaoCheck s on  a.ApplyTime > '{0}' and a.[ApplyID] = s.DataID and cast(a.ApplyTime as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                                where  a.ApplyTime > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime));//申请单                

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["LB"] = "03";//类别
                            dr["XM"] = source["PSN_Name"].ToString();//姓名
                            dr["SFZHM"] = source["PSN_CertificateNO"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["PSN_CertificateNO"].ToString()) == false)
                            {
                                continue;
                            }
                            try
                            {
                                dr["ZZJGDM"] = source["ENT_OrganizationsCode"].ToString().Substring(8, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["PSN_Name"].ToString(), source["ENT_OrganizationsCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["ENT_Name"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["ApplyTime"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["ApplyTime"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额 
                            dr["CQZT"] = 0;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }

                    //if (ds.Tables[0].Rows.Count >= 150)
                    //{
                    //    //上传
                    //    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                    //    ds.Tables[0].Rows.Clear();//清空数据
                    //}
                }

                if (ds.Tables[0].Rows.Count > count)
                {
                    //社保上传改用作业同步，部署在150.109上作业“抽取资质审批系统社保上报信息+基本信息补充信息+动态监管处理结果+证书丢补”
                    //第2步“抽取执业人员注册监管系统社保上报信息”

                    new DBHelper().SqlBulkCopyByDatatable("SBBD", ds.Tables[0]);
                    flag = true;
                }
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    //上传
                //    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                //}

                //记录上报记录
                if (flag == true)
                {
                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                            select e.[ApplyType],e.ApplyTime,e.ApplyID
                            from [dbo].[zjs_Apply] e 
                            left join dbo.ShebaoCheck s on e.ApplyTime > '{0}' and e.ApplyID = s.DataID and e.ApplyTime = s.CHECKTIME
                            where e.ApplyTime > '{0}' and s.CheckID is null";
                    CommonDAL.ExecSQL(string.Format(sql, publishTime));
                    uploadCount.Add("二级造价工程师注册申请", dt.Rows.Count);
                }

                #endregion



                System.Text.StringBuilder sb = new StringBuilder();
                int countsum = 0;
                foreach (string k in uploadCount.Keys)
                {
                    sb.Append(string.Format("；{0}:{1}条", k, uploadCount[k].ToString()));
                    countsum += uploadCount[k];
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                WriteOperateLog("系统服务", 0, string.Format("上传社保比对数据：{0}条", countsum.ToString()), sb.ToString());

            }
            catch (Exception ex)
            {
                FileLog.WriteLog("上传社保比对数据失败，错误信息：" + ex.Message, ex);
                return;
            }
        }

        // 从业人员业务比对社保请求(作废)
        private void Upload_SBDataOfExam()
        {
            return;
            bool flag = false;//上传比对数据是否成功
            DataTable dt = null;//待上传的业务数据
            string publishTime = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd"); //"2015-10-29";//社保比对功能实施时间
            Dictionary<string, int> uploadCount = new Dictionary<string, int>();
            StringBuilder sbUpdate = new StringBuilder();//更新脚本
            int dataCount = 0;//处理数据数量
            string sql = "";

            //RYKWXT.InterFaceServiceSoapClient rykw = new RYKWXT.InterFaceServiceSoapClient();
            jcsjk = new JCSJKService.InterFaceServiceSoapClient();
            try
            {
                //获取服务的数据结构
                DataSet ds = jcsjk.GetIServiceStruct(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8");



                #region 2、变更数据比对(京内变更 或 补办)

                jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                //不包含离京和注销变更

                sql = @"select e.CertificateChangeID,e.NewWorkerName,e.NewWorkerCertificateCode,e.NewUnitCode,e.NewUnitName,e.ApplyDate
                        from dbo.View_CertificateChange e left join dbo.ShebaoCheck s 
                        on e.ApplyDate > '{0}' and s.CheckType='{1}' and cast(e.CertificateChangeID as varchar(64)) = s.DataID and cast(e.ApplyDate as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                        where (e.changeType ='京内变更' or e.changeType ='补办') and e.ApplyDate > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Change));//变更数据

                ds.Tables[0].Rows.Clear();//清空数据

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();

                            dr["LB"] = "03";//类别
                            dr["XM"] = source["NewWorkerName"].ToString();//姓名
                            dr["SFZHM"] = source["NewWorkerCertificateCode"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["NewWorkerCertificateCode"].ToString()) == false)
                            {
                                continue;
                            }
                            //dr["ZZJGDM"] = source["NewUnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            try
                            {
                                dr["ZZJGDM"] = source["NewUnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["NewWorkerName"].ToString(), source["NewUnitCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["NewUnitName"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["ApplyDate"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["ApplyDate"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额 
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    if (ds.Tables[0].Rows.Count >= 150)
                    {
                        //上传
                        flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                        ds.Tables[0].Rows.Clear();//清空数据
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //上传
                    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                }

                //记录上报记录
                if (flag == true)
                {
                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                            select '{1}',e.ApplyDate,e.CertificateChangeID 
                            from dbo.View_CertificateChange e left join dbo.ShebaoCheck s 
                            on e.ApplyDate > '{0}' and s.CheckType='{1}' and cast(e.CertificateChangeID as varchar(64)) = s.DataID and e.ApplyDate = s.CHECKTIME
                            where (e.changeType ='京内变更' or e.changeType ='补办') and e.ApplyDate > '{0}' and s.CheckID is null";
                    CommonDAL.ExecSQL(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Change));
                    uploadCount.Add(EnumManager.ShebaoCheckType.Change, dt.Rows.Count);
                }

                #endregion

                #region 4、进京数据比对

                sql = @"select e.APPLYID,e.WORKERNAME,e.WORKERCERTIFICATECODE,e.UNITCODE,e.UNITNAME,e.ApplyDate
                        from dbo.CERTIFICATEENTERAPPLY e left join dbo.ShebaoCheck s 
                        on (e.PostTypeID=1 ) and e.ApplyDate > '{0}' and s.CheckType='{1}' and cast(e.APPLYID as varchar(64)) = s.DataID and cast(e.ApplyDate as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                        where (e.PostTypeID=1 ) and e.ApplyDate > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Enter));//进京数据

                ds.Tables[0].Rows.Clear();//清空数据

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();

                            dr["LB"] = "03";//类别
                            dr["XM"] = source["WorkerName"].ToString();//姓名
                            dr["SFZHM"] = source["WORKERCERTIFICATECODE"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["WORKERCERTIFICATECODE"].ToString()) == false)
                            {
                                continue;
                            }
                            //dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            try
                            {
                                dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["WorkerName"].ToString(), source["UnitCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["UnitName"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["ApplyDate"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["ApplyDate"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    if (ds.Tables[0].Rows.Count >= 150)
                    {
                        //上传
                        flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                        ds.Tables[0].Rows.Clear();//清空数据
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //上传
                    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                }

                //记录上报记录
                if (flag == true)
                {
                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                            select '{1}',e.ApplyDate, e.APPLYID 
                            from dbo.CERTIFICATEENTERAPPLY e left join dbo.ShebaoCheck s 
                            on (e.PostTypeID=1 ) and e.ApplyDate > '{0}' and s.CheckType='{1}' and cast(e.APPLYID as varchar(64)) = s.DataID and e.ApplyDate = s.CHECKTIME
                            where (e.PostTypeID=1) and e.ApplyDate > '{0}' and s.CheckID is null";
                    CommonDAL.ExecSQL(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Enter));
                    uploadCount.Add(EnumManager.ShebaoCheckType.Enter, dt.Rows.Count);
                }

                #endregion

                #region 5、补登记

                //                sql = @"select e.CERTIFICATEID,e.WORKERNAME,e.WORKERCERTIFICATECODE,e.UNITCODE,e.UNITNAME,e.CREATETIME
                //                        from dbo.CERTIFICATE e left join dbo.ShebaoCheck s 
                //                        on e.Remark = '证书补登记' and (e.PostTypeID=1) and e.CREATETIME > '{0}' and s.CheckType='{1}' and cast(e.CERTIFICATEID as varchar(64)) = s.DataID and cast(e.CREATETIME as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                //                        where e.Remark = '证书补登记' and (e.PostTypeID=1) and e.CREATETIME > '{0}' and s.CheckID is null";

                //                dt = new DBHelper().GetFillData(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Additional));//进京数据

                //                ds.Tables[0].Rows.Clear();//清空数据

                //                foreach (DataRow source in dt.Rows)
                //                {
                //                    for (int i = 1; i < 4; i++)//近3个月
                //                    {
                //                        for (int j = 1; j < 6; j++)//5个险种
                //                        {
                //                            DataRow dr = ds.Tables[0].NewRow();

                //                            dr["LB"] = "03";//类别
                //                            dr["XM"] = source["WorkerName"].ToString();//姓名
                //                            dr["SFZHM"] = source["WORKERCERTIFICATECODE"].ToString();//身份证号码
                //                            if (Utility.Check.isChinaIDCard(source["WORKERCERTIFICATECODE"].ToString()) == false)
                //                            {
                //                                continue;
                //                            }
                //                            //dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                //                            try
                //                            {
                //                                dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                //                            }
                //                            catch (Exception e)
                //                            {
                //                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["WorkerName"].ToString(), source["UnitCode"].ToString()), e);
                //                                continue;
                //                            }
                //                            dr["DWMC"] = source["UnitName"].ToString();//单位名称
                //                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                //                            dr["YF"] = Convert.ToDateTime(source["CREATETIME"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                //                            dr["SBYF"] = Convert.ToDateTime(source["CREATETIME"]).ToString("yyyyMM");//申报月份，YYYYMM    
                //                            dr["JFJS"] = 0;//缴费基数
                //                            dr["DWJFJE"] = 0;//单位缴费金额
                //                            dr["GRJFJE"] = 0;//个人交费金额
                //                            ds.Tables[0].Rows.Add(dr);
                //                        }
                //                    }
                //                    if (ds.Tables[0].Rows.Count >= 150)
                //                    {
                //                        //上传
                //                        flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                //                        ds.Tables[0].Rows.Clear();//清空数据
                //                    }
                //                }

                //                if (ds.Tables[0].Rows.Count > 0)
                //                {
                //                    //上传
                //                    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);
                //                }

                //                //记录上报记录
                //                if (flag == true)
                //                {
                //                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                //                            select '{1}',e.CREATETIME, e.CERTIFICATEID 
                //                            from dbo.CERTIFICATE e left join dbo.ShebaoCheck s 
                //                        on e.Remark = '证书补登记' and (e.PostTypeID=1 ) and e.CREATETIME > '{0}' and s.CheckType='{1}' and cast(e.CERTIFICATEID as varchar(64)) = s.DataID and e.CREATETIME = s.CHECKTIME
                //                        where e.Remark = '证书补登记' and (e.PostTypeID=1 ) and e.CREATETIME > '{0}' and s.CheckID is null";
                //                    CommonDAL.ExecSQL(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Additional));
                //                    uploadCount.Add(EnumManager.ShebaoCheckType.Additional, dt.Rows.Count);
                //                }

                #endregion

                #region 1、报名数据比对

                dataCount = 0;

                sql = @"select e.WorkerName,e.CertificateCode,e.UnitCode,e.UnitName,e.SignUpDate,e.ExamSignupID
                            from dbo.ExamSignup e inner join dbo.examplan p on e.examplanid = p.examplanid
                            left join dbo.ShebaoCheck s on e.SignUpDate > '{0}' and s.CheckType='{1}' and cast(e.ExamSignupID as varchar(64))= s.DataID and cast(e.SignUpDate as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                            where e.SignUpDate > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Exam));//报名数据                

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();

                            dr["LB"] = "03";//类别
                            dr["XM"] = source["WorkerName"].ToString();//姓名
                            dr["SFZHM"] = source["CertificateCode"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["CertificateCode"].ToString()) == false)
                            {
                                continue;
                            }
                            try
                            {
                                dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["WorkerName"].ToString(), source["UnitCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["UnitName"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["SignUpDate"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["SignUpDate"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额 
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    sbUpdate.Append(string.Format(" union select '{0}','{1}','{2}'", "考试报名", source["SignUpDate"], source["ExamSignupID"]));
                    if (ds.Tables[0].Rows.Count >= 150)
                    {
                        if (sbUpdate.Length > 0)
                        {
                            sbUpdate.Remove(0, 6);
                            sql = sbUpdate.ToString();
                            sbUpdate.Remove(0, sbUpdate.Length);
                        }

                        //上传
                        jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                        flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);

                        //记录上报记录
                        CommonDAL.ExecSQL(string.Format("insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID) {0}", sql));
                        dataCount += 10;

                        ds.Tables[0].Rows.Clear();//清空数据
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (sbUpdate.Length > 0)
                    {
                        sbUpdate.Remove(0, 6);
                        sql = sbUpdate.ToString();
                        sbUpdate.Remove(0, sbUpdate.Length);
                    }

                    //上传
                    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);

                    //记录上报记录
                    CommonDAL.ExecSQL(string.Format("insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID) {0}", sql));
                    dataCount += 10;
                }

                uploadCount.Add(EnumManager.ShebaoCheckType.Exam, dataCount);

                //                //记录上报记录
                //                if (flag == true)
                //                {
                //                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                //                            select '{1}',e.SignUpDate,e.ExamSignupID
                //                            from dbo.ExamSignup e inner join dbo.examplan p on e.examplanid = p.examplanid
                //                            left join dbo.ShebaoCheck s on  e.SignUpDate > '{0}' and s.CheckType='{1}' and e.ExamSignupID = s.DataID and e.SignUpDate = s.CHECKTIME
                //                            where  e.SignUpDate > '{0}' and s.CheckID is null";
                //                    CommonDAL.ExecSQL(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Exam));
                //                    uploadCount.Add(EnumManager.ShebaoCheckType.Exam, dt.Rows.Count);
                //                }

                #endregion

                #region 3、续期数据比对

                jcsjk = new JCSJKService.InterFaceServiceSoapClient();

                dataCount = 0;

                sql = @"select e.CERTIFICATECONTINUEID,e.WORKERNAME,e.WORKERCERTIFICATECODE,e.UNITCODE,e.UNITNAME,e.ApplyDate
                        from dbo.VIEW_CERTIFICATECONTINUE e left join dbo.ShebaoCheck s 
                        on (e.PostTypeID=1) and e.ApplyDate > '{0}' and s.CheckType='{1}' and cast(e.CERTIFICATECONTINUEID as varchar(64)) = s.DataID and cast(e.ApplyDate as smalldatetime) = cast(s.CHECKTIME as smalldatetime)
                        where (e.PostTypeID=1) and e.ApplyDate > '{0}' and s.CheckID is null";

                dt = new DBHelper().GetFillData(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Continue));//进京数据

                ds.Tables[0].Rows.Clear();//清空数据

                foreach (DataRow source in dt.Rows)
                {
                    for (int i = 1; i < 4; i++)//近3个月
                    {
                        for (int j = 1; j < 6; j++)//5个险种
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["LB"] = "03";//类别
                            dr["XM"] = source["WorkerName"].ToString();//姓名
                            dr["SFZHM"] = source["WORKERCERTIFICATECODE"].ToString();//身份证号码
                            if (Utility.Check.isChinaIDCard(source["WORKERCERTIFICATECODE"].ToString()) == false)
                            {
                                continue;
                            }
                            try
                            {
                                dr["ZZJGDM"] = source["UnitCode"].ToString().Substring(0, 8);//组织结构代码，8位
                            }
                            catch (Exception e)
                            {
                                FileLog.WriteLog(string.Format("上传社保比对数据失败，企业组织机构代码错误。人员：{0}。组织机构代码：{1}。", source["WorkerName"].ToString(), source["UnitCode"].ToString()), e);
                                continue;
                            }
                            dr["DWMC"] = source["UnitName"].ToString();//单位名称
                            dr["XZ"] = j.ToString("00");//保险种类（01~05）
                            dr["YF"] = Convert.ToDateTime(source["ApplyDate"]).AddMonths(-i).ToString("yyyyMM");//缴费月份，YYYYMM
                            dr["SBYF"] = Convert.ToDateTime(source["ApplyDate"]).ToString("yyyyMM");//申报月份，YYYYMM    
                            dr["JFJS"] = 0;//缴费基数
                            dr["DWJFJE"] = 0;//单位缴费金额
                            dr["GRJFJE"] = 0;//个人交费金额
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    sbUpdate.Append(string.Format(" union select '{0}','{1}','{2}'", "证书续期", source["ApplyDate"], source["CERTIFICATECONTINUEID"]));
                    if (ds.Tables[0].Rows.Count >= 150)
                    {
                        if (sbUpdate.Length > 0)
                        {
                            sbUpdate.Remove(0, 6);
                            sql = sbUpdate.ToString();
                            sbUpdate.Remove(0, sbUpdate.Length);
                        }

                        //上传
                        jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                        flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);

                        //记录上报记录
                        CommonDAL.ExecSQL(string.Format("insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID) {0}", sql));
                        dataCount += 10;
                        ds.Tables[0].Rows.Clear();//清空数据                        
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (sbUpdate.Length > 0)
                    {
                        sbUpdate.Remove(0, 6);
                        sql = sbUpdate.ToString();
                        sbUpdate.Remove(0, sbUpdate.Length);
                    }

                    //上传
                    flag = jcsjk.AddData(descObj.EncryptString(userName), descObj.EncryptString(userPassword), "274cd127-a543-4985-829f-b98f21a020e8", ds);

                    //记录上报记录
                    CommonDAL.ExecSQL(string.Format("insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID) {0}", sql));
                    dataCount += 10;
                }

                uploadCount.Add(EnumManager.ShebaoCheckType.Continue, dataCount);

                //                //记录上报记录
                //                if (flag == true)
                //                {
                //                    sql = @"insert into DBO.SHEBAOCHECK(CHECKTYPE,CHECKTIME,DATAID)
                //                            select '{1}',e.ApplyDate,e.CERTIFICATECONTINUEID
                //                            from dbo.VIEW_CERTIFICATECONTINUE e left join dbo.ShebaoCheck s 
                //                            on (e.PostTypeID=1 or e.PostTypeID=3) and e.ApplyDate > '{0}' and s.CheckType='{1}' and e.CERTIFICATECONTINUEID = s.DataID and e.ApplyDate = s.CHECKTIME
                //                            where (e.PostTypeID=1 or e.PostTypeID=3) and e.ApplyDate > '{0}' and s.CheckID is null";
                //                    CommonDAL.ExecSQL(string.Format(sql, publishTime, EnumManager.ShebaoCheckType.Continue));
                //                    uploadCount.Add(EnumManager.ShebaoCheckType.Continue, dt.Rows.Count);
                //                }

                #endregion

                System.Text.StringBuilder sb = new StringBuilder();
                int countsum = 0;
                foreach (string k in uploadCount.Keys)
                {
                    sb.Append(string.Format("；{0}:{1}条", k, uploadCount[k].ToString()));
                    countsum += uploadCount[k];
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                WriteOperateLog("系统服务", 0, string.Format("上传社保比对数据：{0}条", countsum.ToString()), sb.ToString());

            }
            catch (Exception ex)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                int countsum = 0;
                foreach (string k in uploadCount.Keys)
                {
                    sb.Append(string.Format("；{0}:{1}条", k, uploadCount[k].ToString()));
                    countsum += uploadCount[k];
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    WriteOperateLog("系统服务", 0, string.Format("上传社保比对数据：{0}条，异常中断。", countsum.ToString()), sb.ToString());
                }

                FileLog.WriteLog("上传社保比对数据失败，错误信息：" + ex.Message, ex);
                return;
            }
        }

        //获取社保缴费信息结果
        private void Get_RY_SBJFXXCJ()
        {
            return;//改用数据库存储过程获取返回结果2018-11-28
            //            //5.98计划任务每天05:45执行，读取社保缴费比对结果 7.20/d:/DBBAK/sbjf_zyryjg.txt
            //            FileLog.WriteLog("开始时间：" + DateTime.Now.ToString(), null);
            //            try
            //            {
            //                string delsql = "TRUNCATE TABLE DBO.RY_SBJFXXCJ_ING;";
            //                CommonDAL.ExecSQL(delsql);

            //                string clearsql = "delete FROM DBO.ry_sbjfxxcj where cjsj < dateadd(day,-180,getdate())";//删除六个月前的社保记录
            //                CommonDAL.ExecSQL(clearsql);

            //                string insertsql = @"bulk insert [ZYRYJG].[dbo].[RY_SBJFXXCJ_ING] from 'd:\\DBBak\\sbjf_zyryjg.txt' with (fieldterminator=',',rowterminator='\n')";
            //                CommonDAL.ExecSQL(insertsql);

            //                int dataCount = CommonDAL.SelectRowCount("dbo.RY_SBJFXXCJ_ING", "");
            //                if (dataCount > 0)//默认数据大于10000，认为导入成功,再复制到正式表。
            //                {
            //                    CommonDAL.ExecSQL(@"delete from ""DBO"".""RY_SBJFXXCJ"" where ""ID"" in(
            //                                        SELECT ""ID"" FROM ""DBO"".""RY_SBJFXXCJ""
            //                                        INTERSECT
            //                                        SELECT ""ID"" FROM ""DBO"".""RY_SBJFXXCJ_ING"");
            //
            //                                        INSERT INTO [dbo].[RY_SBJFXXCJ]([ID],[LB],[XM],[SFZHM],[ZZJGDM],[JFJS],[DWJFJE],[GRJFJE],[YF],[XZ],[JFJSSFYZ],[DWJFJESFYZ],[GRJFJESFYZ],[JFSJ],[SFZCJF],[SFZGDWCB],[VALID],[CJR],[CJDEPTID],[CJSJ],[XGR],[XGDEPTID],[XGSJ],[DWMC],[SBYF],[LSBH],[ZT],[SBRQ],[CheckCode])   
            //                                        SELECT [ID],[LB],[XM],[SFZHM],[ZZJGDM],[JFJS],[DWJFJE],[GRJFJE],[YF],[XZ],[JFJSSFYZ],[DWJFJESFYZ],[GRJFJESFYZ],[JFSJ],[SFZCJF],[SFZGDWCB],[VALID],[CJR],[CJDEPTID],[CJSJ],[XGR],[XGDEPTID],[XGSJ],[DWMC],[SBYF],[LSBH],[ZT],[SBRQ],[CheckCode]   FROM [dbo].[RY_SBJFXXCJ_ING];");

            //                    //更新社保比对结果表(近一个月申请数据)
            //                    //注册申请
            //                    CommonDAL.ExecSQL(@"MERGE INTO [dbo].[Apply] t1 USING 
            //                                        (
            //                                        select  c.[ApplyID] ,
            //                                        case when sum(case when s.sfzcjf <1 and s.sfzgdwcb <1 then 1 else 0 end ) >0 then 1 else 0 end as SHEBAOCHECK
            //                                        from dbo.RY_SBJFXXCJ s 
            //                                        inner join 
            //                                        (
            //	                                        select  
            //		                                        case when ApplyChange.ToPSN_CertificateNO is not null and ApplyChange.ToPSN_CertificateNO <> [Apply].PSN_CertificateNO 
            //		                                        then ApplyChange.ToPSN_CertificateNO 
            //		                                        else [Apply].[PSN_CertificateNO]
            //		                                        end as 'PSN_CertificateNO'
            //		                                        ,[Apply].ApplyID,[Apply].[ENT_OrganizationsCode],[ApplyTime]
            //                                            from
            //	                                        [dbo].[Apply] 
            //	                                        left join dbo.ApplyChange on [Apply].ApplyID = ApplyChange.ApplyID
            //                                        ) c 
            //                                        on c.[PSN_CertificateNO] = s.SFZHM and c.[ENT_OrganizationsCode] like s.ZZJGDM +'_'
            //                                        where c.[ApplyTime] > convert(varchar(111),dateadd(month,-1,getdate()),111)
            //                                        and  s.yf between convert(varchar(6),dateadd(month,-1,c.[ApplyTime]),112) and convert(varchar(6),dateadd(month,-1,c.[ApplyTime]),112)
            //                                        group  by c.[ApplyID]
            //                                        ) t2 ON t1.[ApplyID]=t2.[ApplyID]
            //                                        WHEN MATCHED THEN UPDATE SET t1.SHEBAOCHECK = t2.SHEBAOCHECK;");


            //                }
            //                FileLog.WriteLog("结束时间：" + DateTime.Now.ToString(), null);
            //                WriteOperateLog("系统服务", 0, string.Format("获取社保比对结果{0}条", dataCount.ToString()), "获取近7天社保比对结果");
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("批量同步社保比对结果错误：" + ex.Message, ex);
            //            }
        }

        /// <summary>
        ///统计校验的社保记录数量：取最近7天，申请时间超过1个小时
        /// </summary>
        private void UpdateNeedSheBaoCheckCount()
        {
//            string sql = @"select 'cy_change' Btype,count(*) as Bcount from [dbo].[CERTIFICATECHANGE]
//                            where ApplyDate >  DATEADD(day,-7, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE()) and changeType ='京内变更' and SHEBAOCHECK is null and [SheBaoCheckTime] is null
//                            union
//                            select 'cy_jinjing' Btype,count(*) as Bcount from [dbo].[CERTIFICATEENTERAPPLY]
//                            where ApplyDate >  DATEADD(day,-7, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE())  and SHEBAOCHECK is null and [SheBaoCheckTime] is null
//                            union
//                            select 'cy_Signup' Btype,count(*) as Bcount from [dbo].[ExamSignup]
//                            where SignUpDate >  DATEADD(day,-7, GETDATE()) and SignUpDate <  DATEADD(HOUR,-1, GETDATE())  and SHEBAOCHECK is null and [SheBaoCheckTime] is null
//                            union
//                            select 'cy_continue' Btype,count(*) as Bcount from [dbo].[CERTIFICATECONTINUE]
//                            where ApplyDate >  DATEADD(day,-7, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE())  and SHEBAOCHECK is null and [SheBaoCheckTime] is null
//                            union
//                            select 'cy_jzs' Btype,count(*) as Bcount from [dbo].[Apply]
//                            where ApplyTime >  DATEADD(day,-7, GETDATE()) and ApplyTime <  DATEADD(HOUR,-1, GETDATE())  and SHEBAOCHECK is null and [SheBaoCheckTime] is null
//                            union
//                            select 'cy_zjgcs' Btype,count(*) as Bcount from [dbo].[zjs_Apply]
//                            where ApplyTime >  DATEADD(day,-7, GETDATE()) and ApplyTime <  DATEADD(HOUR,-1, GETDATE())  and SHEBAOCHECK is null and [SheBaoCheckTime] is null";

            string sql = @"select 'cy_change' Btype,count(*) as Bcount from [dbo].[CERTIFICATECHANGE]
                            where ApplyDate >  DATEADD(day,-30, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE()) and changeType ='京内变更' and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_jinjing' Btype,count(*) as Bcount from [dbo].[CERTIFICATEENTERAPPLY]
                            where ApplyDate >  DATEADD(day,-30, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE())  and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_Signup' Btype,count(*) as Bcount from [dbo].[ExamSignup]
                            where SignUpDate >  DATEADD(day,-30, GETDATE()) and SignUpDate <  DATEADD(HOUR,-1, GETDATE())  and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (SignUpDate > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_continue' Btype,count(*) as Bcount from [dbo].[CERTIFICATECONTINUE]
                            where ApplyDate >  DATEADD(day,-30, GETDATE()) and ApplyDate <  DATEADD(HOUR,-1, GETDATE())  and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_jzs' Btype,count(*) as Bcount from [dbo].[Apply]
                            where ApplyTime >  DATEADD(day,-30, GETDATE()) and ApplyTime <  DATEADD(HOUR,-1, GETDATE())  and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyTime > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_zjgcs' Btype,count(*) as Bcount from [dbo].[zjs_Apply]
                            where ApplyTime >  DATEADD(day,-30, GETDATE()) and ApplyTime <  DATEADD(HOUR,-1, GETDATE())  and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyTime > [SheBaoCheckTime] and SHEBAOCHECK=0))
                            union
                            select 'cy_CheckFeedBack' Btype,count(*) as Bcount from [dbo].[CheckFeedBack]
                            where [PublishiTime] > DATEADD(day,-60, GETDATE()) and ( [SheBaoCheckTime] is null or [WorkerRerpotTime] > [SheBaoCheckTime])
                            union
                            select 'Task_SheBaoCheck' Btype,count(*) as Bcount from [dbo].[ShebaoTask]
                            where [ApplyDate] > DATEADD(day,-30, GETDATE()) and [SheBaoCheckTime] is null";

            DataTable dt = CommonDAL.GetDataTable(sql);
            SheBaoChekCount.Clear();
            foreach (DataRow r in dt.Rows)
            {
                SheBaoChekCount.Add(r["Btype"].ToString(), Convert.ToInt32(r["Bcount"]));
            }


        }

        /// <summary>
        /// 新版社保比对2023
        /// </summary>
        private void NewSheBaoCheck()
        {
            UpdateNeedSheBaoCheckCount();

            if (SheBaoChekCount["cy_change"] == 0
                && SheBaoChekCount["cy_jinjing"] == 0
                && SheBaoChekCount["cy_Signup"] == 0
                && SheBaoChekCount["cy_continue"] == 0
                && SheBaoChekCount["cy_jzs"] == 0
                && SheBaoChekCount["cy_zjgcs"] == 0
                && SheBaoChekCount["cy_CheckFeedBack"] == 0
                && SheBaoChekCount["Task_SheBaoCheck"] == 0
                )
            {
                return;
            }

            //社保比对开始时间
            ShebaoCheckStartTime = DateTime.Now;

            switch (ShebaoCheckStep)
            {
                case "cy_change":
                    ShebaoCheckStep = "cy_jinjing";
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "cy_jinjing":
                    ShebaoCheckStep = "cy_Signup";
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);                   
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "cy_Signup":
                    ShebaoCheckStep = "cy_continue";
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "cy_continue":
                    ShebaoCheckStep = "cy_jzs";
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);                    
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "cy_jzs":
                    ShebaoCheckStep = "cy_zjgcs";
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "cy_zjgcs":
                    ShebaoCheckStep = "Task_SheBaoCheck";
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    break;
                case "Task_SheBaoCheck":
                    ShebaoCheckStep = "cy_change";
                    Task_SheBaoCheck(ShebaoCheckStartTime);
                    cy_feedBackSheBaoCheck(ShebaoCheckStartTime);
                    cy_changeSheBaoCheck(ShebaoCheckStartTime);
                    cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
                    cy_SignupSheBaoCheck(ShebaoCheckStartTime);
                    cy_continueSheBaoCheck(ShebaoCheckStartTime);
                    jzs_SheBaoCheck(ShebaoCheckStartTime);
                    zjgcs_SheBaoCheck(ShebaoCheckStartTime);
                    break;
            }

            //switch (ShebaoCheckStep)
            //{
            //    case "cy_change":                   
            //        ShebaoCheckStep = "cy_jinjing";
            //        if (SheBaoChekCount["cy_change"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        cy_changeSheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //    case "cy_jinjing":
            //        ShebaoCheckStep = "cy_Signup";
            //        if (SheBaoChekCount["cy_jinjing"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        cy_jinjingSheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //    case "cy_Signup":
            //        ShebaoCheckStep = "cy_continue";
            //        if (SheBaoChekCount["cy_Signup"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        cy_SignupSheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //    case "cy_continue":
            //        ShebaoCheckStep = "cy_jzs";
            //        if (SheBaoChekCount["cy_continue"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        cy_continueSheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //    case "cy_jzs":
            //        ShebaoCheckStep = "cy_zjgcs";
            //        if (SheBaoChekCount["cy_jzs"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        jzs_SheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //    case "cy_zjgcs":
            //        ShebaoCheckStep = "cy_change";
            //        if (SheBaoChekCount["cy_zjgcs"] == 0)
            //        {
            //            NewSheBaoCheck();
            //            return;
            //        }
            //        zjgcs_SheBaoCheck(ShebaoCheckStartTime);
            //        break;
            //}
        }

        /// <summary>
        /// 新版【二级造价工程师】社保比对
        /// </summary>
        private void zjgcs_SheBaoCheck(DateTime startTime)
        {
            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} a.ApplyID,
                            case when c.[PSN_NameTo] is not null and c.[PSN_NameTo]<> a.PSN_Name
                            then c.PSN_NameTo else a.[PSN_Name] end as 'PSN_Name',
                            case when c.ToPSN_CertificateNO is not null and c.ToPSN_CertificateNO <> a.PSN_CertificateNO 
                            then c.ToPSN_CertificateNO else a.[PSN_CertificateNO] end as 'PSN_CertificateNO',
                            a.[ApplyTime]
                    from [dbo].[zjs_Apply] a
                    left join dbo.zjs_ApplyChange c on a.ApplyID = c.ApplyID
                    where a.ApplyTime > '{1}' and a.ApplyTime <  DATEADD(HOUR,-1, GETDATE()) 
                        and (
                                (a.SHEBAOCHECK is null and a.[SheBaoCheckTime] is null) 
                             or (a.ApplyTime > a.[SheBaoCheckTime] and a.SHEBAOCHECK=0)
                             or (a.[SheBaoCheckTime] < dateadd(day,-1,getdate()) and a.SHEBAOCHECK=0 and (a.[ApplyStatus]='未申报' or a.[ApplyStatus]='待确认' or a.[ApplyStatus]='已申报'))
                        )
                        and a.ApplyTime > '{2}'                             
                    order by a.ApplyTime ";

            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime, new DateTime(cursor_long["cursor_SheBao_zjgcs"])));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保zjgcs_SheBaoCheck失败。", ex);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_zjgcs"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_zjgcs"] = Convert.ToDateTime(r["ApplyTime"]).Ticks;//记录扫描位置

                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前

                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["PSN_Name"].ToString(), r["PSN_CertificateNO"].ToString(), Convert.ToDateTime(r["ApplyTime"]));
                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [zjs_Apply] set [SheBaoCheckTime] = getdate() where [ApplyID] = '{0}'", r["ApplyID"]));
                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保zjgcs_SheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_zjgcs"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
            }
        }

        /// <summary>
        /// 新版【二级建造师】社保比对
        /// </summary>
        private void jzs_SheBaoCheck(DateTime startTime)
        {
            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} a.ApplyID,
                            case when c.[PSN_NameTo] is not null and c.[PSN_NameTo]<> a.PSN_Name
                            then c.PSN_NameTo else a.[PSN_Name] end as 'PSN_Name',
                            case when c.ToPSN_CertificateNO is not null and c.ToPSN_CertificateNO <> a.PSN_CertificateNO 
                            then c.ToPSN_CertificateNO else a.[PSN_CertificateNO] end as 'PSN_CertificateNO',
                            a.[ApplyTime]
                    from [dbo].[Apply] a
                    left join dbo.ApplyChange c on a.ApplyID = c.ApplyID
                    where a.ApplyTime > '{1}' and a.ApplyTime <  DATEADD(HOUR,-1, GETDATE()) 
                          and (
                                    (a.SHEBAOCHECK is null and a.[SheBaoCheckTime] is null) 
                                 or (a.ApplyTime > a.[SheBaoCheckTime] and a.SHEBAOCHECK=0)
                                 or (a.[SheBaoCheckTime] < dateadd(day,-1,getdate()) and a.SHEBAOCHECK=0 and (a.[ApplyStatus]='未申报' or a.[ApplyStatus]='待确认' or a.[ApplyStatus]='已申报'))
                              )
                          and a.ApplyTime > '{2}'                             
                    order by a.ApplyTime ";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime, new DateTime(cursor_long["cursor_SheBao_jzs"])));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保jzs_SheBaoCheck失败。", ex);
                return;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_jzs"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_jzs"] = Convert.ToDateTime(r["ApplyTime"]).Ticks;//记录扫描位置
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["PSN_Name"].ToString(), r["PSN_CertificateNO"].ToString(), Convert.ToDateTime(r["ApplyTime"]));
                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [Apply] set [SheBaoCheckTime] = getdate() where [ApplyID] = '{0}'", r["ApplyID"]));
                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保jzs_SheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_jzs"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
            }
        }

        /// <summary>
        /// 新版【从业人员证书变更(京内变更)】社保比对
        /// </summary>
        private void cy_changeSheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            //不包含离京和注销变更，只查三类人
//            sql = @"select  top {0} CertificateChangeID,NewWorkerName,NewWorkerCertificateCode,ApplyDate
//                        from dbo.CertificateChange 
//                        where ApplyDate > '{1}'  and ApplyDate <  DATEADD(HOUR,-1, GETDATE()) 
//                                and changeType ='京内变更' 
//                                and (
//                                        (SHEBAOCHECK is null and [SheBaoCheckTime] is null) 
//                                     or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0)
//                                     or ([SheBaoCheckTime] < dateadd(day,-1,getdate()) and SHEBAOCHECK=0 and ([Status]='填报中' or [Status]='待单位确认' or [Status]='已申请'))
//                                )
//                                and CertificateChangeID > {2}                             
//                         order by CertificateChangeID ";
            sql = @"select  top {0} ch.CertificateChangeID,ch.NewWorkerName,ch.NewWorkerCertificateCode,ch.ApplyDate
                        from dbo.CertificateChange ch inner join certificate c on  ch.CERTIFICATEID = c.CERTIFICATEID
                        where ch.ApplyDate > '{1}'  and c.POSTTYPEID < 2
						and ch.ApplyDate <  DATEADD(HOUR,-1, GETDATE()) 
                                and ch.changeType ='京内变更' 
                                and (
                                        (ch.SHEBAOCHECK is null and ch.[SheBaoCheckTime] is null) 
                                     or (ch.ApplyDate > ch.[SheBaoCheckTime] and ch.SHEBAOCHECK=0)
                                     or (ch.[SheBaoCheckTime] < dateadd(day,-1,getdate()) and ch.SHEBAOCHECK=0 and (ch.[Status]='填报中' or ch.[Status]='待单位确认' or ch.[Status]='已申请'))
                                )
                                and CertificateChangeID > {2}                             
                         order by ch.CertificateChangeID  ";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime, cursor_long["cursor_SheBao_cy_change"]));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保cy_changeSheBaoCheck失败。", ex);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_cy_change"] = 0;//记录扫描位置
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_cy_change"] = Convert.ToInt64(r["CertificateChangeID"]);//记录扫描位置
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["NewWorkerName"].ToString(), r["NewWorkerCertificateCode"].ToString(), Convert.ToDateTime(r["ApplyDate"]));
                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [CERTIFICATECHANGE] set [SheBaoCheckTime] = getdate() where [CERTIFICATECHANGEID] = {0}", r["CertificateChangeID"]));
                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保cy_changeSheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_cy_change"] = 0;//记录扫描位置
            }
        }

        /// <summary>
        /// 新版【从业人员证书进京】社保比对
        /// </summary>
        private void cy_jinjingSheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} APPLYID,WORKERNAME,WORKERCERTIFICATECODE,ApplyDate
                        from dbo.CERTIFICATEENTERAPPLY 
                        where ApplyDate > '{1}'  and ApplyDate <  DATEADD(HOUR,-1, GETDATE()) 
                                and (
                                        (SHEBAOCHECK is null and [SheBaoCheckTime] is null) 
                                     or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0)
                                     or ([SheBaoCheckTime] < dateadd(day,-1,getdate()) and SHEBAOCHECK=0 and (ApplyStatus='填报中' or ApplyStatus='待单位确认' or ApplyStatus='已申请'))
                                )
                                and APPLYID > {2}                             
                         order by APPLYID ";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime, cursor_long["cursor_SheBao_cy_jinjing"]));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保cy_jinjingSheBaoCheck失败。", ex);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_cy_jinjing"] = 0;//记录扫描位置
                return;
            }
            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_cy_jinjing"] = Convert.ToInt64(r["APPLYID"]);//记录扫描位置
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["WORKERNAME"].ToString(), r["WORKERCERTIFICATECODE"].ToString(), Convert.ToDateTime(r["ApplyDate"]));

                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [CERTIFICATEENTERAPPLY] set [SheBaoCheckTime] = getdate() where [APPLYID] = {0}", r["APPLYID"]));

                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保cy_jinjingSheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_cy_jinjing"] = 0;//记录扫描位置                
            }
        }

        /// <summary>
        /// 新版【从业人员考试报名】社保比对
        /// </summary>
        private void cy_SignupSheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            string sql = "";
            bool QueryResult = false;//查询社保返回

            //社保比对规则：1、尚未比对；2、重新提交报名；3、社保校验结果为false超过一天再次验证(审核开始时间之前)。
            //考试报名比对添加新规则：已过每月28号之后  不在自动触发比对社保未通过申请（即不在重复比对），除非社保比对时间为空（新报名或手动清空申报比对时间的任然比对社保）
            sql = @"select  top {0} s.EXAMSIGNUPID,s.WORKERNAME,s.CERTIFICATECODE,s.SignUpDate
                        from dbo.EXAMSIGNUP s inner join EXAMPLAN p on s.EXAMPLANID = p.EXAMPLANID
                        where p.ExamCardSendStartDate > GETDATE()
						and s.SignUpDate <  DATEADD(HOUR,-1, GETDATE()) and p.POSTTYPEID < 2
                              and (
                                       (s.SHEBAOCHECK is null and s.[SheBaoCheckTime] is null) 
                                    or (day(getdate()) < 29 and s.SignUpDate > s.[SheBaoCheckTime] and s.SHEBAOCHECK=0) 
                                    or (day(getdate()) < 29 and s.[SheBaoCheckTime] < dateadd(day,-1,getdate()) and s.SHEBAOCHECK=0 and (s.[Status]='未提交' or s.[Status]='待初审' or s.[Status]='已初审'))
                              )
                              and s.EXAMSIGNUPID > {1}                             
                         order by s.EXAMSIGNUPID ";

//            sql = @"select  top {0} s.EXAMSIGNUPID,s.WORKERNAME,s.CERTIFICATECODE,s.SignUpDate
//                        from dbo.EXAMSIGNUP s inner join EXAMPLAN p on s.EXAMPLANID = p.EXAMPLANID
//                        where p.ExamCardSendStartDate > GETDATE()
//						and s.SignUpDate <  DATEADD(HOUR,-1, GETDATE()) and p.POSTTYPEID < 2
//                              and (
//                                       (s.SHEBAOCHECK is null and s.[SheBaoCheckTime] is null) 
//                                    or (day(getdate()) < 31 and s.SignUpDate > s.[SheBaoCheckTime] and s.SHEBAOCHECK=0) 
//                                    or (day(getdate()) < 31 and s.[SheBaoCheckTime] < dateadd(day,-1,getdate()) and s.SHEBAOCHECK=0 and (s.[Status]='未提交' or s.[Status]='待初审' or s.[Status]='已初审'))
//                              )
//                              and s.EXAMSIGNUPID > {1}                             
//                         order by s.EXAMSIGNUPID ";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, cursor_long["cursor_SheBao_cy_Signup"]));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保cy_SignupSheBaoCheck失败。", ex);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_cy_Signup"] = 0;//记录扫描位置
                return;
            }
            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_cy_Signup"] = Convert.ToInt64(r["EXAMSIGNUPID"]);//记录扫描位置
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["WORKERNAME"].ToString(), r["CERTIFICATECODE"].ToString(), Convert.ToDateTime(r["SignUpDate"]));

                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [EXAMSIGNUP] set [SheBaoCheckTime] = getdate() where [EXAMSIGNUPID] = {0}", r["EXAMSIGNUPID"]));

                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保cy_SignupSheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_cy_Signup"] = 0;//记录扫描位置
            }
        }

        /// <summary>
        /// 新版【从业人员证书续期】社保比对
        /// </summary>
        private void cy_continueSheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} CERTIFICATECONTINUEID,WORKERNAME,WORKERCERTIFICATECODE,ApplyDate
                        from dbo.VIEW_CERTIFICATECONTINUE 
                        where ApplyDate > '{1}'  and ApplyDate <  DATEADD(HOUR,-1, GETDATE()) and PostTypeID < 2
                                and ((SHEBAOCHECK is null and [SheBaoCheckTime] is null) or (ApplyDate > [SheBaoCheckTime] and SHEBAOCHECK=0))
                                and CERTIFICATECONTINUEID > {2}                             
                         order by CERTIFICATECONTINUEID ";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime, cursor_long["cursor_SheBao_cy_continue"]));//变更数据
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保cy_continueSheBaoCheck失败。", ex);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_SheBao_cy_continue"] = 0;//记录扫描位置
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                cursor_long["cursor_SheBao_cy_continue"] = Convert.ToInt64(r["CERTIFICATECONTINUEID"]);//记录扫描位置
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["WORKERNAME"].ToString(), r["WORKERCERTIFICATECODE"].ToString(), Convert.ToDateTime(r["ApplyDate"]));

                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [CERTIFICATECONTINUE] set [SheBaoCheckTime] = getdate() where [CERTIFICATECONTINUEID] = {0}", r["CERTIFICATECONTINUEID"]));

                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保cy_continueSheBaoCheck失败。", ex);
                    continue;
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_SheBao_cy_continue"] = 0;//记录扫描位置
            }
        }

        /// <summary>
        /// 监管反馈社保比对
        /// </summary>
        private void cy_feedBackSheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            //string publishTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string publishTime = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd"); //"2023-09-24";//社保比对功能实施时间
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} [DataID],WORKERNAME,WORKERCERTIFICATECODE,case when [WorkerRerpotTime] is null then [PublishiTime] else [WorkerRerpotTime] end as ApplyDate
                    from dbo.[CheckFeedBack] 
                    where [PublishiTime] > '{1}' and ( [SheBaoCheckTime] is null or [WorkerRerpotTime] > [SheBaoCheckTime]) 
                    order by [PatchCode] desc,[DataID]";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe, publishTime));
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保cy_feedBackSheBaoCheck失败。", ex);
            }

            foreach (DataRow r in dt.Rows)
            {
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["WORKERNAME"].ToString(), r["WORKERCERTIFICATECODE"].ToString(), Convert.ToDateTime(r["ApplyDate"]));

                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [CheckFeedBack] set [SheBaoCheckTime] = getdate() where [DataID] = '{0}'", r["DataID"]));

                        //先不更新比对结果，统一用作业在每天8点、12点更新社保比对结果
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保cy_feedBackSheBaoCheck失败。", ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// 自定义社保查询任务（无业务需求）
        /// </summary>
        private void Task_SheBaoCheck(DateTime startTime)
        {

            DataTable dt = null;//待比对的业务数据
            string sql = "";
            bool QueryResult = false;//查询社保返回

            sql = @"select  top {0} [DataID],[WorkerCertificateCode],[WorkerName],[ApplyDate],[SheBaoCheckTime]
                    from dbo.[ShebaoTask]
                    where [ApplyDate] > DATEADD(day,-30, GETDATE()) and [SheBaoCheckTime] is null
                    order by DataID";
            try
            {
                dt = new DBHelper().GetFillData(string.Format(sql, MaxCountExe));
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取待比对社保ShebaoTask失败。", ex);
            }

            foreach (DataRow r in dt.Rows)
            {
                if (ShebaoCheckStartTime != startTime) return;//新线程已将开启，停止当前
                try
                {
                    QueryResult = SheBaoDAL.SheBaoQuery(r["WORKERNAME"].ToString(), r["WORKERCERTIFICATECODE"].ToString(), Convert.ToDateTime(r["ApplyDate"]));

                    if (QueryResult == true)
                    {
                        //更新社保比对时间
                        CommonDAL.ExecSQL(string.Format("update [ShebaoTask] set [SheBaoCheckTime] = getdate() where [DataID] = {0}", r["DataID"]));

                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("比对社保ShebaoTask失败。", ex);
                    continue;
                }
            }
        }

        ///// <summary>
        ///// 获取社保待比对数量
        ///// </summary>
        ///// <param name="YeWuType">业务类型：cy_change；cy_jinjing；cy_Signup；cy_continue；cy_jzs；cy_zjgcs</param>
        ///// <returns></returns>
        //private int GetNeedSheBaoCheckCount(string YeWuType)
        //{
        //    if(SheBaoChekCount.Count <=0 || TjTime_SheBaoChekCount <DateTime.Now.AddHours(-1))
        //    {
        //        UpdateNeedSheBaoCheckCount();
        //    }
        //    return SheBaoChekCount[YeWuType];
        //}

        #endregion 社保业务

        #region 二级建造师业务

        /// <summary>
        /// 二建建造师使用件创建与取回：部署21
        /// </summary>
        private void ErJian_UseCreate()
        {
            //接口调用开始时间
            EJJZS_Use_StartTime = DateTime.Now;

            switch (EJJZS_Use_Step)
            {
                case 1:
                default:
                    EJJZS_Use_Step = 2;
                    EJJZS_Use_CreateCA(EJJZS_Use_StartTime);
                    EJJZS_Use_AutoApply(EJJZS_Use_StartTime);
                    EJJZSS_Use_PDF_GetReturn(EJJZS_Use_StartTime);
                    EJJZSS_Use_OFD_GetReturn(EJJZS_Use_StartTime);
                    break;
                case 2:
                    EJJZS_Use_Step = 3;
                    EJJZSS_Use_PDF_GetReturn(EJJZS_Use_StartTime);
                    EJJZSS_Use_OFD_GetReturn(EJJZS_Use_StartTime);
                    EJJZS_Use_AutoApply(EJJZS_Use_StartTime);
                    EJJZS_Use_CreateCA(EJJZS_Use_StartTime);
                    break;
                case 3:
                    EJJZS_Use_Step = 4;
                    EJJZSS_Use_OFD_GetReturn(EJJZS_Use_StartTime);
                    EJJZS_Use_CreateCA(EJJZS_Use_StartTime);
                    EJJZSS_Use_PDF_GetReturn(EJJZS_Use_StartTime);
                    EJJZS_Use_AutoApply(EJJZS_Use_StartTime);
                    break;
                case 4:
                    EJJZS_Use_Step = 1;
                    EJJZS_Use_AutoApply(EJJZS_Use_StartTime);
                    EJJZSS_Use_OFD_GetReturn(EJJZS_Use_StartTime);
                    EJJZS_Use_CreateCA(EJJZS_Use_StartTime);
                    EJJZSS_Use_PDF_GetReturn(EJJZS_Use_StartTime);
                    break;
            }
        }

        /// <summary>
        /// 电子证书签章：部署175（二建建造师使用件PDF、二建建造师使用件OFD，从业人员PDF证书、从业人员ODF证书、下载已签章但保存文件失败从业人员证书）
        /// </summary>
        private void ErJian_UseCA()
        {
            //接口调用开始时间
            EJJZS_Use_StartTime = DateTime.Now;

            switch (EJJZS_Use_Step)
            {
                case 1:
                default:
                    EJJZS_Use_Step = 2;
                    EJJZS_Use_PDF_IssueCA(EJJZS_Use_StartTime);
                    EJJZS_Use_OFD_IssueCA(EJJZS_Use_StartTime);
                    IssueCA_Certiticate(EJJZS_Use_StartTime);
                    IssueCA_CongYeOfd(EJJZS_Use_StartTime);
                    downPDForOFD(EJJZS_Use_StartTime);
                    break;
                case 2:
                    EJJZS_Use_Step = 3;
                    EJJZS_Use_OFD_IssueCA(EJJZS_Use_StartTime);
                    IssueCA_Certiticate(EJJZS_Use_StartTime);
                    IssueCA_CongYeOfd(EJJZS_Use_StartTime);
                    downPDForOFD(EJJZS_Use_StartTime);
                    EJJZS_Use_PDF_IssueCA(EJJZS_Use_StartTime);
                    break;
                case 3:
                    EJJZS_Use_Step = 4;                   
                    IssueCA_Certiticate(EJJZS_Use_StartTime);
                    IssueCA_CongYeOfd(EJJZS_Use_StartTime);
                    downPDForOFD(EJJZS_Use_StartTime);
                    EJJZS_Use_PDF_IssueCA(EJJZS_Use_StartTime);
                    EJJZS_Use_OFD_IssueCA(EJJZS_Use_StartTime);
                    break;
                case 4:
                    EJJZS_Use_Step = 5;                  
                    IssueCA_CongYeOfd(EJJZS_Use_StartTime);
                    downPDForOFD(EJJZS_Use_StartTime);
                    EJJZS_Use_PDF_IssueCA(EJJZS_Use_StartTime);
                    EJJZS_Use_OFD_IssueCA(EJJZS_Use_StartTime);
                    IssueCA_Certiticate(EJJZS_Use_StartTime);
                    break;
                case 5:
                    EJJZS_Use_Step = 1;                   
                    downPDForOFD(EJJZS_Use_StartTime);
                    EJJZS_Use_PDF_IssueCA(EJJZS_Use_StartTime);
                    EJJZS_Use_OFD_IssueCA(EJJZS_Use_StartTime);
                    IssueCA_Certiticate(EJJZS_Use_StartTime);
                    IssueCA_CongYeOfd(EJJZS_Use_StartTime);
                    break;                   

            }
        }

        /// <summary>
        /// 二级建造师以后公告通过的初始、变更、重新、延续、增项注册，都在公告通过后的24小时之后自动生成最大时间范围的证书使用有效期范围
        /// </summary>
        /// <param name="startTime">开始执行时间</param>
        public void EJJZS_Use_AutoApply(DateTime startTime)
        {
            string sql = String.Format(@"select top {0}  b.[PSN_ServerID],u.CreditCode
                                        from [dbo].[COC_TOW_Person_BaseInfo] b 
                                        inner join Worker w on b.PSN_CertificateNO = w.CERTIFICATECODE
                                        inner join dbo.Unit u on b.ENT_ServerID = u.UnitID
                                        left join dbo.EJCertUse e on b.[PSN_ServerID] =e.[PSN_ServerID] and e.[Valid]=1 and e.EndTime > getdate()
                                        where (e.[CertificateCAID] is null or b.[PSN_RegistePermissionDate] > e.[CJSJ])
                                        and (b.PSN_RegisteType = '01' or b.PSN_RegisteType = '02' or b.PSN_RegisteType = '03' or b.PSN_RegisteType = '04' or b.PSN_RegisteType = '05' )
                                        and b.[PSN_CertificateValidity] > getdate() and b.[PSN_RegistePermissionDate] < DATEADD(day,-1,getdate())
                                        and w.SignPhotoTime > '2000-01-01'
                                        and b.[PSN_ServerID] > '{1}'
                                        order by b.PSN_CertificateNO", MaxCountExe, cursor_str["cursor_EJJZS_Use_AutoApply"]);
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal == null || dtOriginal.Rows.Count == 0)
            {
                cursor_str["cursor_EJJZS_Use_AutoApply"] = "";//记录扫描位置
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                cursor_str["cursor_EJJZS_Use_AutoApply"] = Convert.ToString(dr["PSN_ServerID"]);//记录扫描位置

                EJCertUseMDL _EJCertUseMDL = new EJCertUseMDL();//待生成使用件
                EJCertUseMDL curEJCertUseMDL = EJCertUseDAL.GetCurrentUse(dr["PSN_ServerID"].ToString());//当前有效使用件
                COC_TOW_Person_BaseInfoMDL ob = COC_TOW_Person_BaseInfoDAL.GetObject(dr["PSN_ServerID"].ToString());

                _EJCertUseMDL.CertificateCAID = Guid.NewGuid().ToString();
                _EJCertUseMDL.PSN_ServerID = ob.PSN_ServerID;
                _EJCertUseMDL.CJSJ = DateTime.Now;
                _EJCertUseMDL.Valid = 1;

                //_EJCertUseMDL.ZZBS=ob.ZZBS;
                _EJCertUseMDL.ENT_ServerID = ob.ENT_ServerID;
                _EJCertUseMDL.ENT_Name = ob.ENT_Name;
                _EJCertUseMDL.ENT_OrganizationsCode = dr["CreditCode"].ToString();//社会统一信用代码
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
                _EJCertUseMDL.BeginTime = DateTime.Now.Date;
                _EJCertUseMDL.EndTime = ob.PSN_CertificateValidity;

                if (curEJCertUseMDL != null)
                {
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
                        CommonDAL.ExecSQL(tran, string.Format(@"update [EJCertUse]
                                                                set [Valid]=0
                                                                where [PSN_ServerID]='{0}' and [Valid]=1 and [CertificateCAID] <> '{1}'", _EJCertUseMDL.PSN_ServerID, _EJCertUseMDL.CertificateCAID));
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    FileLog.WriteLog(string.Format("自动创建二建{0}电子证书使用件失败。", ob.PSN_RegisterNO),ex);
                    continue;
                }
            }
            if (dtOriginal.Rows.Count < MaxCountExe) cursor_str["cursor_EJJZS_Use_AutoApply"] = "";//记录扫描位置
        }

        /// <summary>
        /// 生成二级建造师电子使用件
        /// </summary>
        public void EJJZS_Use_CreateCA(DateTime startTime)
        {
            //**********************************************************************************************************
            //需要改造成
            //1、服务器21生成代签名的无签章证书pdf；
            //2、发送pdf文件到175服务器 D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf；
            //3、175上服务调用接口签章（已完成）；
            //4、21取回175上pdf文件到人员系统D:\WebRoot\CAFile\XXX\GUID.pdf
            //**********************************************************************************************************
            string sql = String.Format(@"Select top {0} * FROM [dbo].[View_JZS_Use]
                                        where [Valid] > 0 and  [ApplyCATime] is null and [PSN_RegisteType] <7
                                        and CJSJ >'{1}'
                                        order by [CJSJ]", MaxCountExe, new DateTime(cursor_long["cursor_EJJZS_Use_CreateCA"]));
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal == null || dtOriginal.Rows.Count == 0)
            {
                cursor_long["cursor_EJJZS_Use_CreateCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                return;
            }

            var template_url = string.Format(@"{0}\Template\二建使用件.pdf", ExamWebRoot);
            var save_pdf_url = "";//pdf生成位置
            //string CertificateCAID = "";
            //string fileTo = "";

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                cursor_long["cursor_EJJZS_Use_CreateCA"] = Convert.ToDateTime(dr["CJSJ"]).Ticks;//记录扫描位置
                if (dr["PSN_CertificateNO"].ToString().Length == 18 && Utility.Check.isGangAoTai(dr["PSN_CertificateNO"].ToString()) == false && Utility.Check.isChinaIDCard(dr["PSN_CertificateNO"].ToString()) == false)
                {
                    continue;//身份证错误的不生成电子证书，签章服务不接收
                }

                ////创建pdf
                //CertificateCAID = Guid.NewGuid().ToString();

                //pdf目录
                if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot)))
                {
                    System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot));
                }

                save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址

                try
                {
                    var dic = ReadForm(template_url);//读取模板
                    FillFormOfJZS_Use(template_url, save_pdf_url, dic, dr);//填充模板
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级建造师电子证书使用件失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "创建", ex.Message);
                    continue;
                }

                try
                {
                    //begin测试专用（只拷贝待签章ofd到109）************************************************************************************************************************************
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}_Ofd.pdf", @"\\192.168.150.175\zzk\ERJIAN_CA\DGZ", dr["CertificateCAID"]), true);//替换文件
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\ERJIAN_CA\DGZ", dr["CertificateCAID"]), true);//替换文件

                    //更新证书表,写入申请时间
                    CommonDAL.ExecSQL(string.Format(@"update  DBO.[EJCertUse] set [ApplyCATime]='{1}' where [CertificateCAID]='{0}';", dr["CertificateCAID"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    FileLog.WriteLog(string.Format("创建二建电子证书使用件{0}.pdf成功。", dr["CertificateCAID"]));

                    //删除临时文件pdf 
                    Thread.Sleep(500);//暂停0.5秒
                    //File.Delete(save_pdf_url);
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级建造师电子证书信息失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
            if (dtOriginal.Rows.Count < MaxCountExe) cursor_long["cursor_EJJZS_Use_CreateCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
        }

        /// <summary>
        /// 二级建造师电子证书使用件PDF签章
        /// </summary>
        public void EJJZS_Use_PDF_IssueCA(DateTime startTime)
        {
            //中华人民共和国二级建造师注册证书（使用件）	
            //目录实施码：100088101000021135110000
            //事项名称：二级建造师注册证书
            //事项编码：0100909004110X330002140X100
            //印章编码：DZYZ000021135XXnQXQ

            string save_pdf_url = "";//目标文件地址
            string itemCode = "100088101000021135110000";//目录实施码
            string licenseCode = "";//电子证照标识码
            string idCode = "";//证照号码
            string serviceItemCode = "0100909004110X330002140X100";//事项编码
            string serviceItemName = "二级建造师注册证书";//事项名称

            DataTable dtOriginal = null;
            try
            {
                string sql = String.Format(@"
                                        Select top {0}
                                        ca_count = (select count(*) from [EJCertUse] where [CertificateCAID]=[View_JZS_Use].[CertificateCAID]),
                                        * 
                                        FROM [dbo].[View_JZS_Use]
                                        where ([ApplyCATime] > [Pdf_SignCATime] or ([ApplyCATime] >'1950-1-1' and [Pdf_SignCATime] is null)) and [PSN_RegisteType] < 7
                                        and CJSJ > '{1}' and CJSJ < '{2}' and [Valid] >0 and [ApplyCATime] < '{2}'
                                        order by CJSJ", MaxCountExe, new DateTime(cursor_long["cursor_EJJZS_Use_PDF_IssueCA"]), DateTime.Now.AddMinutes(-3));

                dtOriginal = (new DBHelper()).GetFillData(sql);

                if (dtOriginal == null || dtOriginal.Rows.Count == 0)
                {
                    cursor_long["cursor_EJJZS_Use_PDF_IssueCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                    return;
                }

                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        FileLog.WriteLog(string.Format("{0}登录市电子签章系统失败。", DateTime.Now));
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。",ex);
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                cursor_long["cursor_EJJZS_Use_PDF_IssueCA"] = Convert.ToDateTime(dr["CJSJ"]).Ticks;//记录扫描位置
                try
                {
                    //FileLog.WriteLog(string.Format("{0}准备签章{1}。",DateTime.Now,dr["PSN_RegisterNO"]));

                    if (dr["Pdf_license_code"] != DBNull.Value && dr["Pdf_license_code"] != null)
                    {
                        licenseCode = dr["Pdf_license_code"].ToString();
                        idCode = dr["PSN_RegisterNO"].ToString();

                        //废弃
                        //AbolishResponseResult abolishResult = Execute.Abolish(accessToken, dr);
                        AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
                        if (abolishResult.AckCode != "SUCCESS")
                        {
                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的PDF使用件失败！错误：{1}", dr["PSN_RegisterNO"], JSON.Encode(abolishResult.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "废止", JSON.Encode(abolishResult.Errors));

                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
                            {
                                CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Pdf_license_code]=null,[Pdf_auth_code]=null where CertificateCAID='{0}'", dr["CertificateCAID"]));
                            }

                            //continue;
                        }
                        else
                        {
                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                            CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Pdf_license_code]=null,[Pdf_auth_code]=null where [CertificateCAID]='{0}'", dr["CertificateCAID"]));
                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书pdf使用件。", dr["PSN_RegisterNO"]));
                        }

                    }

                    save_pdf_url = string.Format(@"D:\\zzk\ERJIAN_CA\GZJG\{0}.pdf", dr["CertificateCAID"]);
                    var model = new CreateRequestMdl
                    {
                        Data = new CreateRequestData{ }
                    };

                    model.Data.IniPropoertyEJ_PDFUse(dr);

                    //FileLog.WriteLog(string.Format("{0}签章调用开始{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    CreateResponseResult result = Execute.Create(IssueCA_accessToken, itemCode, model);
                    //FileLog.WriteLog(string.Format("{0}签章调用结束{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    if (result != null)
                    {
                        //签发
                        if (result.AckCode == "SUCCESS" && result.Data != null)
                        {
                            FileLog.WriteLog(string.Format("成功对编号【{0}】电子证书使用件签章！，[Pdf_license_code]={1}，[Pdf_auth_code]={2}。", dr["PSN_RegisterNO"], result.Data.LicenseCode, result.Data.AuthCode));

                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
                            CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Pdf_license_code]='{1}',[Pdf_auth_code]='{2}',Pdf_SignCATime=getdate() where [CertificateCAID]='{0}'", dr["CertificateCAID"], result.Data.LicenseCode, result.Data.AuthCode));

                            bool downAgain = false;//是否已经再次尝试下载
                        TrydownAgain://第一次下载失败，隔几秒再次尝试

                            //下载pdf
                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
                            if (downResult == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF使用件返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", "下载PDF使用件返回结果为Null");
                                continue;
                            }
                            if (downResult.Data == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF使用件返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(downResult.Errors)));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", JSON.Encode(downResult.Errors));

                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
                                {
                                    Thread.Sleep(5000);//暂停5秒
                                    downAgain = true;
                                    goto TrydownAgain;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {

                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);

                                ////更新证书表,写入发送DFS时间
                                //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】电子证书{1}.pdf。", dr["PSN_RegisterNO"], dr["CertificateCAID"]));


                                ////移动已处理未盖章文件
                                //File.Move(save_pdf_url.Replace("GZJG", "DGZ"), save_pdf_url.Replace("GZJG", "BAK_DGZ"));

                                //删除待盖章文件
                                Thread.Sleep(500);//暂停0.5秒
                                File.Delete(string.Format(@"D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf", dr["CertificateCAID"]));
                            }
                        }
                        else
                        {
                            FileLog.WriteLog(string.Format("创建PDF使用件制证数据返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(result.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", JSON.Encode(result.Errors));

                            if (JSON.Encode(result.Errors).Contains("已存在") || JSON.Encode(result.Errors).Contains("存在重复的电子证照"))
                            {
                                try
                                {
                                    AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, "", dr["PSN_RegisterNO"].ToString(), serviceItemCode, serviceItemName);

                                    if (abolishResult.AckCode == "SUCCESS")
                                    {                                        
                                        //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                                        CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Pdf_license_code]=null,[Pdf_auth_code]=null where [CertificateCAID]='{0}'", dr["CertificateCAID"]));
                                        FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书pdf使用件。", dr["PSN_RegisterNO"]));
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书pdf使用件失败，错误信息：{1}", dr["PSN_RegisterNO"], ex2.Message), ex2);
                                }
                            }

                            continue;
                        }
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("签章PDF使用件返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("签章PDF使用件失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", ex.Message);
                    continue;
                }
            }

            if (dtOriginal.Rows.Count < MaxCountExe) cursor_long["cursor_EJJZS_Use_PDF_IssueCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
        }

        /// <summary>
        /// 二级建造师电子证书使用件PDF回写
        /// </summary>
        public void EJJZSS_Use_PDF_GetReturn(DateTime startTime)
        {
            try
            {
                //获取尚未成功签章证书信息
                string sql = "";
                DataTable dtOriginal = null;
                string fileFrom = "";//签章结果在175地址
                string fileTo = "";

                sql = String.Format(@"Select top {0} * from DBO.[EJCertUse] 
                                        where  [ApplyCATime] <[Pdf_SignCATime] and [Pdf_ReturnCATime] is null and [CJSJ] > '{1}' and [Pdf_SignCATime] <'{2}' and [Valid] >0
                                        order by [CJSJ]", MaxCountExe, new DateTime(cursor_long["cursor_EJJZS_Use_PDF_GetReturn"]), DateTime.Now.AddMinutes(-2));
                dtOriginal = (new DBHelper()).GetFillData(sql);

                if (dtOriginal == null || dtOriginal.Rows.Count == 0)
                {
                    cursor_long["cursor_EJJZS_Use_PDF_GetReturn"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                    return;
                }

                fileFrom = @"\\192.168.150.175\zzk\ERJIAN_CA\GZJG\";

                foreach (DataRow dr in dtOriginal.Rows)
                {
                    if (EJJZS_Use_StartTime != startTime) return;
                    cursor_long["cursor_EJJZS_Use_PDF_GetReturn"] = Convert.ToDateTime(dr["CJSJ"]).Ticks;//记录扫描位置
                    try
                    {
                        if (File.Exists(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"])) == true)
                        {
                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
                            if (!Directory.Exists(fileTo))
                            {
                                System.IO.Directory.CreateDirectory(fileTo);
                            }
                            fileTo = string.Format(@"{0}\{1}.pdf", fileTo, dr["CertificateCAID"]);

                            File.Copy(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件

                            File.Move(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.pdf", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));

                            CommonDAL.ExecSQL(string.Format("UPDATE [dbo].[EJCertUse] SET [Pdf_ReturnCATime] = getdate() WHERE [CertificateCAID]='{0}'  and [Pdf_ReturnCATime] is null;", dr["CertificateCAID"]));

                            FileLog.WriteLog(string.Format("回写已签章的证书{0}使用件成功。", fileTo));
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "取回", ex.Message);
                        continue;
                    }
                }

                if (dtOriginal.Rows.Count < MaxCountExe) cursor_long["cursor_EJJZS_Use_PDF_GetReturn"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("取回已签章的证书失败。", ex);
            }
        }

        /// <summary>
        /// 二级建造师电子证书使用件OFD签章
        /// </summary>
        public void EJJZS_Use_OFD_IssueCA(DateTime startTime)
        {
            //中华人民共和国二级建造师注册证书（附件使用件）	
            //目录实施码：100088501000021135110000
            //事项名称：二级建造师注册证书
            //事项编码：0100909004110X330002140X100
            //印章编码：MTQyMTEwMDAwMDAwMTg2OTY0NjI=

            string save_pdf_url = "";//目标文件地址
            string itemCode = "100088501000021135110000";//目录实施码
            string licenseCode = "";//电子证照标识码
            string idCode = "";//证照号码
            string serviceItemCode = "0100909004110X330002140X100";//事项编码
            string serviceItemName = "二级建造师注册证书";//事项名称

            DataTable dtOriginal = null;
            try
            {
                string sql = String.Format(@"
                                        Select top {0}
                                        ca_count = (select count(*) from [EJCertUse] where [CertificateCAID]=[View_JZS_Use].[CertificateCAID]),
                                        * 
                                        FROM [dbo].[View_JZS_Use]
                                        where ([ApplyCATime] > [Ofd_SignCATime] or ([ApplyCATime] >'1950-1-1' and [Ofd_SignCATime] is null)) and [PSN_RegisteType] < 7
                                        and CJSJ > '{1}' and CJSJ < '{2}' and [Valid] >0
                                        order by CJSJ", MaxCountExe, new DateTime(cursor_long["cursor_EJJZS_Use_OFD_IssueCA"]), DateTime.Now.AddMinutes(-2));

                dtOriginal = (new DBHelper()).GetFillData(sql);

                if (dtOriginal == null || dtOriginal.Rows.Count == 0)
                {
                    cursor_long["cursor_EJJZS_Use_OFD_IssueCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                    return;
                }

                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        FileLog.WriteLog(string.Format("{0}登录市电子签章系统失败。", DateTime.Now));
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。", ex);
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                
                cursor_long["cursor_EJJZS_Use_OFD_IssueCA"] = Convert.ToDateTime(dr["CJSJ"]).Ticks;//记录扫描位置
                try
                {
                    //FileLog.WriteLog(string.Format("{0}准备签章{1}。",DateTime.Now,dr["PSN_RegisterNO"]));

                    if (dr["Ofd_license_code"] != DBNull.Value && dr["Ofd_license_code"] != null)
                    {
                        #region 废弃

                        licenseCode = dr["Ofd_license_code"].ToString();
                        idCode = dr["PSN_RegisterNO"].ToString();

                       
                        //AbolishResponseResult abolishResult = Execute.Abolish(accessToken, dr);
                        AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
                        if (abolishResult.AckCode != "SUCCESS")
                        {
                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的Ofd使用件失败！错误：{1}", dr["PSN_RegisterNO"], JSON.Encode(abolishResult.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "废止", JSON.Encode(abolishResult.Errors));

                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
                            {
                                CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Ofd_license_code]=null,[Ofd_auth_code]=null where CertificateCAID='{0}'", dr["CertificateCAID"]));
                            }

                            //continue;
                        }
                        else
                        {
                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                            CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Ofd_license_code]=null,[Ofd_auth_code]=null where [CertificateCAID]='{0}'", dr["CertificateCAID"]));
                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书Ofd使用件。", dr["PSN_RegisterNO"]));
                        }

                        #endregion 废弃
                    }

                    save_pdf_url = string.Format(@"D:\\zzk\ERJIAN_CA\GZJG\{0}.ofd", dr["CertificateCAID"]);
                    var model = new CreateRequestGBMdl
                    {
                        Data = new CreateRequestDataGB { }
                    };

                    model.Data.IniPropoertyEJ_OfdUse(dr);

                    //FileLog.WriteLog(string.Format("{0}签章调用开始{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    CreateResponseResult result = Execute.CreateOfd(IssueCA_accessToken, "0", model);
                    //FileLog.WriteLog(string.Format("{0}签章调用结束{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    if (result != null)
                    {
                        //签发
                       
                        if (result.AckCode == "SUCCESS" && result.Data != null)
                        {
                            #region 签发成功

                            FileLog.WriteLog(string.Format("成功对编号【{0}】电子证书使用件Ofd签章！，[Ofd_license_code]={1}，[Ofd_auth_code]={2}。", dr["PSN_RegisterNO"], result.Data.LicenseCode, result.Data.AuthCode));

                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
                            CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Ofd_license_code]='{1}',[Ofd_auth_code]='{2}',Ofd_SignCATime=getdate() where [CertificateCAID]='{0}'", dr["CertificateCAID"], result.Data.LicenseCode, result.Data.AuthCode));

                            bool downAgain = false;//是否已经再次尝试下载
                        TrydownAgain://第一次下载失败，隔几秒再次尝试

                            //下载pdf
                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
                            if (downResult == null)
                            {
                                FileLog.WriteLog(string.Format("下载Ofd使用件返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", "下载Ofd使用件返回结果为Null");
                                continue;
                            }
                            if (downResult.Data == null)
                            {
                                FileLog.WriteLog(string.Format("下载Ofd使用件返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(downResult.Errors)));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", JSON.Encode(downResult.Errors));

                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
                                {
                                    Thread.Sleep(5000);//暂停5秒
                                    downAgain = true;
                                    goto TrydownAgain;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {

                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);

                                ////更新证书表,写入发送DFS时间
                                //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】电子证书{1}.Ofd。", dr["PSN_RegisterNO"], dr["CertificateCAID"]));


                                ////移动已处理未盖章文件
                                //File.Move(string.Format(@"D:\\zzk\ERJIAN_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]), string.Format(@"D:\\zzk\ERJIAN_CA\BAK_DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]));

                                //删除待盖章文件
                                Thread.Sleep(500);//暂停0.5秒
                                //File.Delete(string.Format(@"D:\\zzk\ERJIAN_CA\DGZ\{0}_Ofd.pdf", dr["CertificateCAID"]));
                            }

                            #endregion 签发成功
                        }
                        else
                        {
                            FileLog.WriteLog(string.Format("创建Ofd使用件制证数据返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(result.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", JSON.Encode(result.Errors));

                            if (JSON.Encode(result.Errors).Contains("已存在") || JSON.Encode(result.Errors).Contains("存在重复的电子证照"))
                            {
                                try
                                {
                                    AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, "", dr["PSN_RegisterNO"].ToString(), serviceItemCode, serviceItemName);
                                    if (abolishResult.AckCode == "SUCCESS")
                                    {
                                        //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                                        CommonDAL.ExecSQL(string.Format("update  DBO.[EJCertUse] set [Ofd_license_code]=null,[Ofd_auth_code]=null where [CertificateCAID]='{0}'", dr["CertificateCAID"]));
                                        FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书Ofd使用件。", dr["PSN_RegisterNO"]));
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书Ofd使用件失败，错误信息：{1}", dr["PSN_RegisterNO"], ex2.Message), ex2);
                                }
                            }

                            continue;
                        }
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("签章Ofd使用件返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("签章Ofd使用件失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", ex.Message);
                    continue;
                }
            }

            if (dtOriginal.Rows.Count < MaxCountExe) cursor_long["cursor_EJJZS_Use_OFD_IssueCA"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
        }

        /// <summary>
        /// 二级建造师电子证书使用件OFD回写
        /// </summary>
        public void EJJZSS_Use_OFD_GetReturn(DateTime startTime)
        {
            try
            {
                //获取尚未成功签章证书信息
                string sql = "";
                DataTable dtOriginal = null;
                string fileFrom = "";//签章结果在175地址
                string fileTo = "";

                sql = String.Format(@"Select top {0} * from DBO.[EJCertUse] 
                                        where  [ApplyCATime] <[Ofd_SignCATime] and [Ofd_ReturnCATime] is null and [CJSJ] > '{1}' and [Ofd_SignCATime] <'{2}' and [Valid] >0
                                        order by [CJSJ]", MaxCountExe, new DateTime(cursor_long["cursor_EJJZS_Use_OFD_GetReturn"]), DateTime.Now.AddMinutes(-2));
                dtOriginal = (new DBHelper()).GetFillData(sql);

                if (dtOriginal == null || dtOriginal.Rows.Count == 0)
                {
                    cursor_long["cursor_EJJZS_Use_OFD_GetReturn"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
                    return;
                }

                fileFrom = @"\\192.168.150.175\zzk\ERJIAN_CA\GZJG\";

                foreach (DataRow dr in dtOriginal.Rows)
                {
                    if (EJJZS_Use_StartTime != startTime) return;
                    cursor_long["cursor_EJJZS_Use_OFD_GetReturn"] = Convert.ToDateTime(dr["CJSJ"]).Ticks;//记录扫描位置
                    try
                    {
                        if (File.Exists(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"])) == true)
                        {
                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
                            if (!Directory.Exists(fileTo))
                            {
                                System.IO.Directory.CreateDirectory(fileTo);
                            }
                            fileTo = string.Format(@"{0}\{1}.ofd", fileTo, dr["CertificateCAID"]);

                            File.Copy(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件

                            //File.Move(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.pdf", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));

                            CommonDAL.ExecSQL(string.Format("UPDATE [dbo].[EJCertUse] SET [Ofd_ReturnCATime] = getdate() WHERE [CertificateCAID]='{0}'  and [Ofd_ReturnCATime] is null;", dr["CertificateCAID"]));

                            Thread.Sleep(500);//暂停0.5秒
                            File.Delete(string.Format("{0}{1}.ofd", fileFrom, dr["CertificateCAID"]));

                            FileLog.WriteLog(string.Format("回写已签章的证书{0}使用件成功。", fileTo));
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "取回", ex.Message);
                        continue;
                    }
                }

                if (dtOriginal.Rows.Count < MaxCountExe) cursor_long["cursor_EJJZS_Use_OFD_GetReturn"] = DateTime.Now.AddYears(-10).Ticks;//记录扫描位置
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("取回已签章的证书失败。", ex);
            }
        }

        /// <summary>
        /// 二建上报国家库新接口（部署在192.168.5.49）
        /// </summary>
        private void Upload_JZS_New()
        {
            #region 反复尝试执行

            Utility.SFTPHelper sftp = null;
            string sql = "";
            DataTable dtOriginal = null;
            DataSet ds = null;
            string strFilePath = "";

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true; //要求缩进
            //注意如果不设置encoding默认将输出utf-16
            //注意这儿不能直接用Encoding.UTF8如果用Encoding.UTF8将在输出文本的最前面添加4个字节的非xml内容
            settings.Encoding = new UTF8Encoding(false);
            settings.NewLineChars = Environment.NewLine;//设置换行符

            try
            {

                #region 生成人员信息表xml文件

                //导出路径
                strFilePath = string.Format(@"d:\WebRoot\SFTP\110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

                if (File.Exists(strFilePath) == false)
                {
                    //人员信息表（rrxxb）
                    sql = String.Format(@"
                        SELECT 
                            p.[PSN_Name] xm	    --姓名
                            ,p.[ENT_Name] qymc	--企业名称
				            ,case when len(u.[CreditCode])>0 then u.[CreditCode]
					            else u.ENT_OrganizationsCode end as orgcode --组织机构代码/统一信用代码
                            ,p.[PSN_Sex] xb	    --性别
                            ,convert(varchar(10),p.[PSN_BirthDate],20) csny	--出生年月
                            ,p.[PSN_CertificateType] zjlb	--证件类别
                            ,p.[PSN_CertificateNO] zjhm	--证件号码
                            ,p.[PSN_RegisterCertificateNo] zsbh	--证书编号
                            ,p.[PSN_RegisterNO] zcbh	--注册编号
                            ,convert(varchar(10),p.[PSN_CertificationDate],20) fzrq	--发证日期
                            ,convert(varchar(10),p.[PSN_CertificateValidity],20) zcyxq	--证书有效期
                            ,'110000' sfbz	--省份编码
                        FROM [dbo].[COC_TOW_Person_BaseInfo] p
			            left join [dbo].[Unit] u on convert(varchar(18),p.ENT_OrganizationsCode)=u.ENT_OrganizationsCode
                        where p.[PSN_RegisteType] <'07' and p.[PSN_CertificateValidity] >getdate()");
                    dtOriginal = (new DBHelper()).GetFillData(sql);
                    dtOriginal.TableName = "ryxxb";
                    ds = new DataSet("NewDataSet");
                    ds.Tables.Add(dtOriginal.Copy());


                    using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
                    {
                        ds.WriteXml(a);
                    }
                    WriteOperateLog("系统服务", 0, string.Format("生成上传二建人员信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count), "");
                    FileLog.WriteLog(string.Format("生成上传二建人员信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count));
                }

                #endregion

                #region 生成企业信息表xml文件

                //导出路径
                strFilePath = string.Format(@"d:\WebRoot\SFTP\110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

                if (File.Exists(strFilePath) == false)
                {
                    //企业信息表（qyxxb）
                    sql = String.Format(@"
                            select distinct 
                                u.[ENT_Name] qymc	--企业名称
                                ,case when len(f.UNI_SCID) =18 then f.UNI_SCID
								when len(u.[CreditCode])>0 then u.[CreditCode]
								else u.ENT_OrganizationsCode end as orgcode	--组织机构代码/统一信用代码
                                ,'110000' sjbm	--省级编码
                                ,'北京市' gszcsf	--工商注册省份
                                ,r.RegionCode citybm	--地市编码
                                ,u.ENT_City gszcsz	--工商注册市州
                                ,u.[ENT_Corporate] fddbr	--法定代表人
                                ,u.[END_Addess] txdz	--通讯地址
                                , qylx=case 
                                when u.ent_type like '%施工%' then '施工'
                                when u.ent_type like '%监理%' then '监理'
                                when u.ent_type like '%勘察%' then '勘察'
                                when u.ent_type like '%设计%' then '设计'
                                when u.ent_type like '%造价%' then '造价咨询'
                                when u.ent_type like '%招标%' then '招标代理'
                                else '无' end	--企业资质类型
                                ,f.REG_NO yyzzh	--营业执照号
                                ,'110000' sfbz	--省份编码
                             FROM [dbo].[COC_TOW_Person_BaseInfo] p
                             inner join [dbo].[Unit] u on p.ENT_OrganizationsCode = u.ENT_OrganizationsCode
                             left join [Dict_Region] r on u.ENT_City=r.[RegionName]
                             left join [dbo].[QY_FRK] f on convert(varchar(18),u.ENT_OrganizationsCode)=f.ORG_CODE
                            where p.[PSN_RegisteType] <'07' and p.[PSN_CertificateValidity] >getdate()");
                    dtOriginal = (new DBHelper()).GetFillData(sql);
                    dtOriginal.TableName = "qyxxb";
                    ds = new DataSet("NewDataSet");
                    ds.Tables.Add(dtOriginal.Copy());

                    using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
                    {
                        ds.WriteXml(a);
                    }
                    WriteOperateLog("系统服务", 0, string.Format("生成上传二建企业信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count), "");
                    FileLog.WriteLog(string.Format("生成上传二建企业信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count));
                }

                #endregion

                #region 生成注册专业表xml文件

                //导出路径
                strFilePath = string.Format(@"d:\WebRoot\SFTP\110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

                if (File.Exists(strFilePath) == false)
                {
                    //注册专业表（zczyb）
                    sql = String.Format(@"            
                            select 
                	            p.[PSN_CertificateNO] zjhm--人员证件号码
                	            ,'03' zslx--证书类型（简称）
                	            , zczy=case 
                	            when z.[PRO_Profession] like '%建筑%' then '建筑工程'
                	            when z.[PRO_Profession] like '%公路%' then '公路工程'
                	            when z.[PRO_Profession] like '%机电%' then '机电工程'
                	            when z.[PRO_Profession] like '%水利%' then '水利水电工程'
                	            when z.[PRO_Profession] like '%市政%' then '市政公用工程'
                	            when z.[PRO_Profession] like '%矿业%' then '矿业工程'
                	            end--注册专业
                	            ,convert(varchar(10),z.[PRO_ValidityBegin],20) yxqq--有效期起
                	            ,convert(varchar(10),z.[PRO_ValidityEnd],20) yxqz--有效期止
                	            ,'110000' sfbz--省份编码
                            FROM [dbo].[COC_TOW_Person_BaseInfo] p
                            inner join [dbo].[COC_TOW_Register_Profession] z on p.PSN_ServerID = z.PSN_ServerID
                            where p.[PSN_RegisteType] <'07' and p.[PSN_CertificateValidity] >getdate() and z.[PRO_ValidityEnd] >getdate()");
                    dtOriginal = (new DBHelper()).GetFillData(sql);
                    dtOriginal.TableName = "zczyb";
                    ds = new DataSet("NewDataSet");
                    ds.Tables.Add(dtOriginal.Copy());

                    using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
                    {
                        ds.WriteXml(a);
                    }
                    WriteOperateLog("系统服务", 0, string.Format("生成上传二建专业信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count), "");
                    FileLog.WriteLog(string.Format("生成上传二建专业信息数据到建设部xml文件：{0}条", dtOriginal.Rows.Count));
                }
                #endregion

                #region SFTP传输文件

                sftp = new Utility.SFTPHelper("219.142.101.108", "4022", "110000A", "bj110000@b11");//联接SFTP

                int dCount = OperateLogDAL.SelectCount(string.Format(" and [LogTime]>'{0}' and [OperateName] like '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), "上传二建人员信息数据到建设部成功"));
                if (dCount == 0)
                {
                    sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format("/110000/110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));
                    WriteOperateLog("系统服务", 0, "上传二建人员信息数据到建设部成功", "");
                    FileLog.WriteLog("上传二建人员信息数据到建设部成功");
                }

                dCount = OperateLogDAL.SelectCount(string.Format(" and [LogTime]>'{0}' and [OperateName] like '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), "上传二建企业信息数据到建设部成功"));
                if (dCount == 0)
                {
                    sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format("/110000/110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));
                    WriteOperateLog("系统服务", 0, "上传二建企业信息数据到建设部成功", "");
                    FileLog.WriteLog("上传二建企业信息数据到建设部成功");
                }

                dCount = OperateLogDAL.SelectCount(string.Format(" and [LogTime]>'{0}' and [OperateName] like '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), "上传二建专业信息数据到建设部成功"));
                if (dCount == 0)
                {
                    sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format(@"/110000/110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));
                    WriteOperateLog("系统服务", 0, "上传二建专业信息数据到建设部成功", "");
                    FileLog.WriteLog("上传二建专业信息数据到建设部成功");
                }

                #endregion
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("同步二级建造师到建设部失败", ex);
            }
            finally
            {
                if (sftp != null && sftp.Connected)
                {
                    sftp.Disconnect();
                }
            }

            #endregion

            #region 每天执行一次
            //            Utility.SFTPHelper sftp = null;
            //            try
            //            {
            //                //人员信息表（rrxxb）
            //                string sql = String.Format(@"
            //            SELECT 
            //                p.[PSN_Name] xm	    --姓名
            //                ,p.[ENT_Name] qymc	--企业名称
            //				,case when len(u.[CreditCode])>0 then u.[CreditCode]
            //					else u.ENT_OrganizationsCode end as orgcode --组织机构代码/统一信用代码
            //                ,p.[PSN_Sex] xb	    --性别
            //                ,convert(varchar(10),p.[PSN_BirthDate],20) csny	--出生年月
            //                ,p.[PSN_CertificateType] zjlb	--证件类别
            //                ,p.[PSN_CertificateNO] zjhm	--证件号码
            //                ,p.[PSN_RegisterCertificateNo] zsbh	--证书编号
            //                ,p.[PSN_RegisterNO] zcbh	--注册编号
            //                ,convert(varchar(10),p.[PSN_CertificationDate],20) fzrq	--发证日期
            //                ,convert(varchar(10),p.[PSN_CertificateValidity],20) zcyxq	--证书有效期
            //                ,'110000' sfbz	--省份编码
            //            FROM [dbo].[COC_TOW_Person_BaseInfo] p
            //			left join [dbo].[Unit] u on convert(varchar(18),p.ENT_OrganizationsCode)=u.ENT_OrganizationsCode
            //            where p.[PSN_RegisteType] <'07'");
            //                DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
            //                dtOriginal.TableName = "ryxxb";
            //                DataSet ds = new DataSet("NewDataSet");
            //                ds.Tables.Add(dtOriginal.Copy());

            //                //导出路径
            //                string strFilePath = string.Format(@"d:\WebRoot\SFTP\110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

            //                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            //                settings.Indent = true; //要求缩进
            //                //注意如果不设置encoding默认将输出utf-16
            //                //注意这儿不能直接用Encoding.UTF8如果用Encoding.UTF8将在输出文本的最前面添加4个字节的非xml内容
            //                settings.Encoding = new UTF8Encoding(false);
            //                settings.NewLineChars = Environment.NewLine;//设置换行符

            //                using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
            //                {
            //                    ds.WriteXml(a);
            //                }

            //                sftp = new Utility.SFTPHelper("219.142.101.108", "4022", "110000A", "bi110000@a113");

            //                sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format("/110000/110000rrxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));

            //                WriteOperateLog("系统服务", 0, string.Format("上传二建人员信息数据到建设部：{0}条", dtOriginal.Rows.Count), "");
            //                FileLog.WriteLog(string.Format("上传二建人员信息数据到建设部：{0}条", dtOriginal.Rows.Count));

            //                //企业信息表（qyxxb）
            //                sql = String.Format(@"
            //                            select distinct 
            //                                u.[ENT_Name] qymc	--企业名称
            //                                ,case when len(f.UNI_SCID) =18 then f.UNI_SCID
            //								when len(u.[CreditCode])>0 then u.[CreditCode]
            //								else u.ENT_OrganizationsCode end as orgcode	--组织机构代码/统一信用代码
            //                                ,'110000' sjbm	--省级编码
            //                                ,'北京市' gszcsf	--工商注册省份
            //                                ,r.RegionCode citybm	--地市编码
            //                                ,u.ENT_City gszcsz	--工商注册市州
            //                                ,u.[ENT_Corporate] fddbr	--法定代表人
            //                                ,u.[END_Addess] txdz	--通讯地址
            //                                , qylx=case 
            //                                when u.ent_type like '%施工%' then '施工'
            //                                when u.ent_type like '%监理%' then '监理'
            //                                when u.ent_type like '%勘察%' then '勘察'
            //                                when u.ent_type like '%设计%' then '设计'
            //                                when u.ent_type like '%造价%' then '造价咨询'
            //                                when u.ent_type like '%招标%' then '招标代理'
            //                                else '无' end	--企业资质类型
            //                                ,f.REG_NO yyzzh	--营业执照号
            //                                ,'110000' sfbz	--省份编码
            //                             FROM [dbo].[COC_TOW_Person_BaseInfo] p
            //                             inner join [dbo].[Unit] u on p.ENT_OrganizationsCode = u.ENT_OrganizationsCode
            //                             left join [Dict_Region] r on u.ENT_City=r.[RegionName]
            //                             left join [dbo].[QY_FRK] f on convert(varchar(18),u.ENT_OrganizationsCode)=f.ORG_CODE
            //                            where p.[PSN_RegisteType] <'07'");
            //                dtOriginal = (new DBHelper()).GetFillData(sql);
            //                dtOriginal.TableName = "qyxxb";
            //                ds = new DataSet("NewDataSet");
            //                ds.Tables.Add(dtOriginal.Copy());

            //                //导出路径
            //                strFilePath = string.Format(@"d:\WebRoot\SFTP\110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

            //                using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
            //                {
            //                    ds.WriteXml(a);
            //                }

            //                sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format("/110000/110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));
            //                WriteOperateLog("系统服务", 0, string.Format("上传二建企业信息数据到建设部：{0}条", dtOriginal.Rows.Count), "");
            //                FileLog.WriteLog(string.Format("上传二建企业信息数据到建设部：{0}条", dtOriginal.Rows.Count));

            //                //注册专业表（zczyb）
            //                sql = String.Format(@"            
            //                            select 
            //                	            p.[PSN_CertificateNO] zjhm--人员证件号码
            //                	            ,'03' zslx--证书类型（简称）
            //                	            , zczy=case 
            //                	            when z.[PRO_Profession] like '%建筑%' then '建筑工程'
            //                	            when z.[PRO_Profession] like '%公路%' then '公路工程'
            //                	            when z.[PRO_Profession] like '%机电%' then '机电工程'
            //                	            when z.[PRO_Profession] like '%水利%' then '水利水电工程'
            //                	            when z.[PRO_Profession] like '%市政%' then '市政公用工程'
            //                	            when z.[PRO_Profession] like '%矿业%' then '矿业工程'
            //                	            end--注册专业
            //                	            ,convert(varchar(10),z.[PRO_ValidityBegin],20) yxqq--有效期起
            //                	            ,convert(varchar(10),z.[PRO_ValidityEnd],20) yxqz--有效期止
            //                	            ,'110000' sfbz--省份编码
            //                            FROM [dbo].[COC_TOW_Person_BaseInfo] p
            //                            inner join [dbo].[COC_TOW_Register_Profession] z on p.PSN_ServerID = z.PSN_ServerID
            //                            where p.[PSN_RegisteType] <'07'");
            //                dtOriginal = (new DBHelper()).GetFillData(sql);
            //                dtOriginal.TableName = "zczyb";
            //                ds = new DataSet("NewDataSet");
            //                ds.Tables.Add(dtOriginal.Copy());

            //                //导出路径
            //                strFilePath = string.Format(@"d:\WebRoot\SFTP\110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

            //                using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
            //                {
            //                    ds.WriteXml(a);
            //                }

            //                sftp.Put(string.Format(@"d:\WebRoot\SFTP\110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format(@"/110000/110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));
            //                WriteOperateLog("系统服务", 0, string.Format("上传二建专业信息数据到建设部：{0}条", dtOriginal.Rows.Count), "");
            //                FileLog.WriteLog(string.Format("上传二建专业信息数据到建设部：{0}条", dtOriginal.Rows.Count));

            //            }
            //            catch (Exception ex)
            //            {
            //                if (sftp != null && sftp.Connected)
            //                {
            //                    sftp.Disconnect();
            //                }
            //                FileLog.WriteLog("同步二级建造师到建设部失败", ex);
            //            }
            #endregion
        }

        #region 作废

        /// <summary>
        /// 生成二级建造师电子证书
        /// </summary>
        public void CreateEJJZS_CA()
        {
            return;
            //**********************************************************************************************************
            //需要改造成
            //1、生成代签名的无签章证书pdf；
            //2、发送pdf文件到109服务器 D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf；
            //3、109上服务调用接口签章（已完成）；
            //4、取回pdf文件到人员系统D:\WebRoot\CAFile\XXX\GUID.pdf
            //**********************************************************************************************************
//            string sql = String.Format(@"Select top {0} * FROM [dbo].[View_JZS_TOW_WithProfession]
//                                        where ([ApplyCATime] <[CJSJ] or [ApplyCATime] <[XGSJ] or [ApplyCATime] is null) and [PSN_RegisteType] <7
//                                        and PSN_CertificateNO >'{1}'
//                                        order by PSN_CertificateNO", MaxCountExe, CreateCA_Cur_PSN_CertificateNO_EJJZS);
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (dtOriginal.Rows.Count < MaxCountExe)
//            {
//                CreateCA_Cur_PSN_CertificateNO_EJJZS = "";//记录扫描位置
//                if (dtOriginal.Rows.Count == 0)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                CreateCA_Cur_PSN_CertificateNO_EJJZS = Convert.ToString(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"]);//记录扫描位置
//            }

//            var template_url = string.Format(@"{0}\Template\二级建造师注册证书.pdf", ExamWebRoot);
//            var save_pdf_url = "";//pdf生成位置
//            string CertificateCAID = "";
//            //string fileTo = "";

//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                if (dr["PSN_CertificateNO"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["PSN_CertificateNO"].ToString()) == false)
//                {
//                    continue;//身份证错误的不生成电子证书，签章服务不接收
//                }

//                //创建pdf
//                CertificateCAID = Guid.NewGuid().ToString();

//                //fileTo = string.Format(@"{0}\{1}", CAFile, CertificateCAID.Substring(CertificateCAID.Length - 3, 3));

//                //pdf目录
//                if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot)))
//                {
//                    System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot));
//                }


//                save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, CertificateCAID);//目标文件地址


//                try
//                {
//                    var dic = ReadForm(template_url);//读取模板
//                    FillFormOfJZS(template_url, save_pdf_url, dic, dr);//填充模板
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("生成二级建造师电子证书失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "创建", ex.Message);
//                    continue;
//                }

//                try
//                {
//                    //更新证书表,写入申请时间
//                    CommonDAL.ExecSQL(string.Format(@"update  DBO.[COC_TOW_Person_BaseInfo] set [ApplyCATime]='{1}',[CertificateCAID]='{2}',[ReturnCATime] = null,[SendCATime] = null where [PSN_ServerID]='{0}';", dr["PSN_ServerID"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CertificateCAID));
//                    FileLog.WriteLog(string.Format("创建电子证书{0}.pdf成功。", CertificateCAID));
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("生成二级建造师电子证书信息失败，错误信息：" + ex.Message, ex);
//                    continue;
//                }
//            }
        }

        /// <summary>
        /// 发送带签章二建证书到109
        /// </summary>
        public void SendEJJZS_CA()
        {
            return;
//            string save_pdf_url = "";//目标文件地址

//            string sql = String.Format(@"Select top {0} PSN_ServerID,CertificateCAID
//                                        from DBO.[COC_TOW_Person_BaseInfo] 
//                                        Where  [ApplyCATime] >'2018-01-01' and  [SendCATime] is null
//                                        order by [XGSJ] desc ", MaxCountExe);
//            //　and CertificateCAID='643ef6fa-67b9-4720-8461-1fe3a7a5b101'----测试专用

//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            if (dtOriginal.Rows.Count == 0) return;

//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                try
//                {
//                    save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址
//                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\ERJIAN_CA\DGZ", dr["CertificateCAID"]), true);//替换文件

//                    //更新证书表,写入发送DFS时间
//                    CommonDAL.ExecSQL(string.Format("update  DBO.COC_TOW_Person_BaseInfo set [SendCATime]='{1}' where [PSN_ServerID]='{0}'", dr["PSN_ServerID"], DateTime.Now));
//                    FileLog.WriteLog(string.Format("同步待签章的证书{0}.pdf到109成功。", dr["CertificateCAID"]));

//                    //删除临时文件pdf 
//                    File.Delete(save_pdf_url);
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog(string.Format("同步待签章的证书{0}.pdf到109失败。", dr["CertificateCAID"]), ex);
//                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "发送", ex.Message);
//                    continue;
//                }
//            }
        }

        /// <summary>
        /// 二建证书PDF签章（部署在192.168.150.175)
        /// </summary>
        public void IssueCA_JZS()
        {
            return;
//            if (IssueCA_jzsing == true)
//            {
//                return;
//            }
//            else
//            {
//                IssueCA_jzsing = true;
//            }
//            //目录实施码：100005801000021135110000
//            //事项编码：0100909004110X330002140X100
//            //事项名称：二级建造师注册证书
//            //印章编码：DZYZ000021135XXnQXQ

//            string save_pdf_url = "";//目标文件地址
//            string itemCode = "100005801000021135110000";//目录实施码
//            string licenseCode = "";//电子证照标识码
//            string idCode = "";//证照号码
//            string serviceItemCode = "0100909004110X330002140X100";//事项编码
//            string serviceItemName = "二级建造师注册证书";//事项名称

//            DataTable dtOriginal = null;
//            try
//            {
//                string sql = String.Format(@"
//                                        Select top {0}  
//                                        ca_count = (select count(*) from [COC_TOW_Person_BaseInfoCAHistory] where [PSN_ServerID]=c.[PSN_ServerID]),
//                                        c.*,f.UNI_SCID FROM [dbo].[View_JZS_TOW_WithProfession] c
//                                        left join [dbo].[QY_FRK] f  on c.[ENT_OrganizationsCode] = f.[ORG_CODE]
//                                        where (c.[SendCATime] >c.[SignCATime] or (c.[SendCATime] >c.[ApplyCATime] and c.[SignCATime] is null)) and c.[PSN_RegisteType] <7  
//                                        and c.PSN_CertificateNO >'{1}'  and c.[SendCATime] <'{2}'                                                                           
//                                        order by c.PSN_CertificateNO", MaxCountExe, IssueCA_Cur_PSN_CertificateNO, DateTime.Now.AddMinutes(-5));

//                dtOriginal = (new DBHelper()).GetFillData(sql);

//                if (dtOriginal.Rows.Count < MaxCountExe)
//                {
//                    IssueCA_Cur_PSN_CertificateNO = "";//记录扫描位置
//                    if (dtOriginal.Rows.Count == 0)
//                    {
//                        IssueCA_jzsing = false;
//                        return;
//                    }
//                }
//                else
//                {
//                    IssueCA_Cur_PSN_CertificateNO = Convert.ToString(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"]);//记录扫描位置
//                }

//                //登录
//                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
//                {
//                    string accessToken = Api.Execute.Login();//登录

//                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
//                    {
//                        IssueCA_accessToken = "";
//                        FileLog.WriteLog(string.Format("{0}登录市电子签章系统失败。", DateTime.Now));
//                        IssueCA_jzsing = false;
//                        return;
//                    }
//                    else
//                    {
//                        IssueCA_accessToken = accessToken;
//                        IssueCA_accessTime = DateTime.Now;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                FileLog.WriteLog("登录市电子签章系统失败。");
//                IssueCA_jzsing = false;
//                return;
//            }

//            foreach (DataRow dr in dtOriginal.Rows)
//            {
//                try
//                {
//                    //FileLog.WriteLog(string.Format("{0}准备签章{1}。",DateTime.Now,dr["PSN_RegisterNO"]));
//                    if (dr["license_code"] != DBNull.Value && dr["license_code"] != null)
//                    {
//                        licenseCode = dr["license_code"].ToString();
//                        idCode = dr["PSN_RegisterNO"].ToString();

//                        //废弃
//                        //AbolishResponseResult abolishResult = Execute.Abolish(accessToken, dr);
//                        AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, licenseCode, idCode, serviceItemCode, serviceItemName);
//                        if (abolishResult.AckCode != "SUCCESS")
//                        {
//                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的PDF失败！错误：{1}", dr["PSN_RegisterNO"], JSON.Encode(abolishResult.Errors)));
//                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "废止", JSON.Encode(abolishResult.Errors));

//                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
//                            {
//                                CommonDAL.ExecSQL(string.Format("update  DBO.COC_TOW_Person_BaseInfo set [license_code]=null,[auth_code]=null where PSN_ServerID='{0}'", dr["PSN_ServerID"]));
//                            }

//                            //continue;
//                        }
//                        else
//                        {
//                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
//                            CommonDAL.ExecSQL(string.Format("update  DBO.[COC_TOW_Person_BaseInfo] set [license_code]=null,[auth_code]=null where [PSN_ServerID]='{0}'", dr["PSN_ServerID"]));
//                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书。", dr["PSN_RegisterNO"]));
//                        }

//                    }

//                    save_pdf_url = string.Format(@"D:\\zzk\ERJIAN_CA\GZJG\{0}.pdf", dr["CertificateCAID"]);
//                    var model = new CreateRequestMdl
//                    {
//                        Data = new CreateRequestData { }
//                    };

//                    model.Data.IniPropoertyEJ(dr);

//                    //FileLog.WriteLog(string.Format("{0}签章调用开始{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
//                    CreateResponseResult result = Execute.Create(IssueCA_accessToken, itemCode, model);
//                    //FileLog.WriteLog(string.Format("{0}签章调用结束{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
//                    if (result != null)
//                    {
//                        //签发
//                        if (result.AckCode == "SUCCESS" && result.Data != null)
//                        {
//                            FileLog.WriteLog(string.Format("成功对编号【{0}】电子证书签章！，[license_code]={1}，[auth_code]={2}。", dr["PSN_RegisterNO"], result.Data.LicenseCode, result.Data.AuthCode));

//                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
//                            CommonDAL.ExecSQL(string.Format("update  DBO.COC_TOW_Person_BaseInfo set [license_code]='{1}',[auth_code]='{2}',SignCATime=getdate() where PSN_ServerID='{0}'", dr["PSN_ServerID"], result.Data.LicenseCode, result.Data.AuthCode));

//                            bool downAgain = false;//是否已经再次尝试下载
//                        TrydownAgain://第一次下载失败，隔几秒再次尝试

//                            //下载pdf
//                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
//                            if (downResult == null)
//                            {
//                                FileLog.WriteLog(string.Format("下载PDF返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
//                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", "下载PDF返回结果为Null");
//                                continue;
//                            }
//                            if (downResult.Data == null)
//                            {
//                                FileLog.WriteLog(string.Format("下载PDF返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(downResult.Errors)));
//                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "下载", JSON.Encode(downResult.Errors));

//                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
//                                {
//                                    Thread.Sleep(5000);//暂停5秒
//                                    downAgain = true;
//                                    goto TrydownAgain;
//                                }
//                                else
//                                {
//                                    continue;
//                                }
//                            }
//                            else
//                            {

//                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);

//                                ////更新证书表,写入发送DFS时间
//                                //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
//                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】电子证书{1}.pdf。", dr["PSN_RegisterNO"], dr["CertificateCAID"]));


//                                //移动已处理未盖章文件
//                                File.Move(save_pdf_url.Replace("GZJG", "DGZ"), save_pdf_url.Replace("GZJG", "BAK_DGZ"));

//                                //FileLog.WriteLog(string.Format("{0}下载签章结果完毕{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
//                            }
//                        }
//                        else
//                        {
//                            FileLog.WriteLog(string.Format("签章PDF返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(result.Errors)));
//                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", JSON.Encode(result.Errors));

//                            if (JSON.Encode(result.Errors).Contains("已存在"))
//                            {
//                                try
//                                {
//                                    AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, itemCode, "", dr["PSN_RegisterNO"].ToString(), serviceItemCode, serviceItemName);
//                                }
//                                catch (Exception ex2)
//                                {
//                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书失败，错误信息：{1}", dr["PSN_RegisterNO"], ex2.Message), ex2);
//                                }
//                            }

//                            continue;
//                        }
//                    }
//                    else
//                    {
//                        FileLog.WriteLog(string.Format("签章PDF返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    FileLog.WriteLog("签章PDF失败，错误信息：" + ex.Message, ex);
//                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "签发", ex.Message);
//                    continue;
//                }
//            }
//            IssueCA_jzsing = false;

        }

        /// <summary>
        /// 取回已签章的二建证书
        /// </summary>
        public void GetReturnEJJZS_CA()
        {
            return;
//            try
//            {
//                //获取尚未成功签章证书信息
//                string sql = "";
//                DataTable dtOriginal = null;
//                string fileFrom = "";//签章结果在109地址
//                string fileTo = "";

//                sql = String.Format(@"Select top {0} * from DBO.COC_TOW_Person_BaseInfo 
//                                        where  [SendCATime] <[SignCATime] and [ReturnCATime] is null and [PSN_CertificateNO] >'{1}' and [SignCATime] <'{2}'
//                                        order by [PSN_CertificateNO]", MaxCountExe, ReturnCA_Cur_PSN_CertificateNO, DateTime.Now.AddMinutes(-5));
//                dtOriginal = (new DBHelper()).GetFillData(sql);
//                //FileLog.WriteLog(sql);
//                if (dtOriginal.Rows.Count < MaxCountExe)
//                {
//                    ReturnCA_Cur_PSN_CertificateNO = "";//记录扫描位置
//                    if (dtOriginal.Rows.Count == 0)
//                    {
//                        return;
//                    }
//                }
//                else
//                {
//                    ReturnCA_Cur_PSN_CertificateNO = dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"].ToString();//记录扫描位置
//                }
//                fileFrom = @"\\192.168.150.175\zzk\ERJIAN_CA\GZJG\";

//                foreach (DataRow dr in dtOriginal.Rows)
//                {
//                    try
//                    {
//                        if (File.Exists(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"])) == true)
//                        {
//                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
//                            if (!Directory.Exists(fileTo))
//                            {
//                                System.IO.Directory.CreateDirectory(fileTo);
//                            }
//                            fileTo = string.Format(@"{0}\{1}.pdf", fileTo, dr["CertificateCAID"]);

//                            File.Copy(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件

//                            File.Move(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.pdf", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));

//                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[COC_TOW_Person_BaseInfo] SET [ReturnCATime] = getdate() WHERE [CertificateCAID]='{0}'  and [ReturnCATime] is null;
//                                                         if not exists(select 1 from [dbo].[COC_TOW_Person_BaseInfoCAHistory] where [CertificateCAID]='{0}') 
//                                                        INSERT INTO [dbo].[COC_TOW_Person_BaseInfoCAHistory] ([CertificateCAID],[ApplyCATime],[ReturnCATime],[PSN_ServerID])
//                                                        select [CertificateCAID],ApplyCATime,ReturnCATime,PSN_ServerID from DBO.[COC_TOW_Person_BaseInfo] where [CertificateCAID]='{0}';", dr["CertificateCAID"]));

//                            FileLog.WriteLog(string.Format("回写已签章的证书{0}成功。", fileTo));
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级注册建造师", "取回", ex.Message);
//                        continue;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                FileLog.WriteLog("取回已签章的证书失败。", ex);
//            }
        }

        //同步审核通过二建注册申请附件到DFS
        private void Upload_EJJZS_File()
        {
            return;
            //            string mes = "";//错误信息
            //            string uName = descObj.EncryptString("XNJ_ZYRYJG");
            //            string uPassword = descObj.EncryptString("59958992");
            //            string serviceID = "01a29df0-9ecc-4e05-aa9d-242188f2c71a";

            //            DataTable dt;//待上传的文件信息

            //            string sql = @" select top 5000 f.*,a.ApplyID,a.PSN_RegisterNo
            //                                FROM [ZYRYJG].[dbo].[Apply] a
            //                              inner join dbo.ApplyFile af on a.ApplyID= af.ApplyID
            //                              inner join dbo.FileInfo f on af.FileID= f.FileID
            //                              where a.NoticeDate>'2017-1-1' and 
            //                              (a.ConfirmResult='通过' or  (a.[ApplyTypeSub]='执业企业变更'   and  a.[ExamineResult]='通过' ))
            //                              and f.UpSJZXTime is  null
            //                              and f.AddTime > dateadd(month,-4,getdate())
            //                               order by  a.NoticeDate";
            //            try
            //            {
            //                dt = CommonDAL.GetDataTable(sql);
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("获取要同步的文件失败。", ex);
            //                return;
            //            }
            //            try
            //            {
            //                FileService.FileServiceSoapClient fs = new FileService.FileServiceSoapClient();
            //                DataSet ds = null;
            //                mes = fs.GetIServiceStruct(out ds, uName, uPassword, serviceID);
            //                if (mes != "成功")
            //                {
            //                    FileLog.WriteLog(string.Format("同步审核通过二建注册申请附件到数据中心失败，错误信息：{0}", mes), null);
            //                    return;
            //                }
            //                byte[] fbyte = null;
            //                foreach (DataRow dr in dt.Rows)
            //                {
            //                    ds.Tables[0].Rows.Clear();
            //                    DataRow newRow = ds.Tables[0].NewRow();
            //                    ds.Tables[0].Rows.Add(newRow);

            //                    newRow["FileShowName"] = dr["FileName"];
            //                    newRow["FileFullName"] = string.Format("{0}.{1}", dr["FileID"], dr["FileType"]);
            //                    newRow["FileID"] = dr["FileID"];
            //                    newRow["ApplyID"] = dr["ApplyID"];
            //                    newRow["PSN_RegisterNO"] = dr["PSN_RegisterNO"];
            //                    newRow["AddTime"] = dr["AddTime"];
            //                    newRow["UploadMan"] = dr["UploadMan"];
            //                    newRow["OrderNo"] = dr["OrderNo"];
            //                    newRow["FileClass"] = "二建注册申请附件";
            //                    newRow["FileSubClass"] = dr["DataType"];

            //                    try
            //                    {
            //                        fbyte = FileToByte(dr["FileUrl"].ToString().Replace("~", ZYRYJGWebRoot));
            //                        mes = fs.UploadFileWithInfo(uName, uPassword, serviceID, ds, fbyte);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        FileLog.WriteLog("文件同步状态失败。", ex);
            //                        continue;
            //                    }

            //                    if (mes != "成功")
            //                    {
            //                        FileLog.WriteLog(string.Format("同步文件{0}失败，错误信息：{1}", dr["FileUrl"], mes), null);
            //                        continue;
            //                    }
            //                    else
            //                    {
            //                        try
            //                        {
            //                            CommonDAL.ExecSQL(string.Format("update [dbo].[FileInfo] set [UpSJZXTime]='{0}' where FileID='{1}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dr["FileID"]));
            //                            FileLog.WriteLog(string.Format("同步文件{0}成功。", dr["FileUrl"]));

            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            FileLog.WriteLog(string.Format("更新文件{0}同步状态失败。", dr["FileUrl"]), ex);
            //                            continue;
            //                        }
            //                    }

            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("文件同步状态失败。", ex);
            //            }
        }

        #endregion 作废

        #endregion 二级建造师业务

        #region 从业人员电子证书业务

        /// <summary>
        /// 生成待签章的从业证书
        /// </summary>
        public void ApplyCA()
        {
            // 获取带签章的证书(过滤：1、专业技术人员岗位；2、证书修改时间大于申请电子证书时间（证书有变动）；3、有一寸照片) 
            //4、造价员、物业项目负责人、拆迁员、安全监理员、房屋建筑结构安全管理员、房屋建筑设施设备安全管理员、监理员（房屋建筑）暂时不申请电子证书

            string sql = String.Format(@"Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        Where (c.PostTypeID>3) and  c.PostID not in(55,159,1009,1021,1024)  
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate())  
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and (c.[ApplyCATime] is null or c.[ApplyCATime] < c.[MODIFYTIME] or (c.[MODIFYTIME] is null and c.[ApplyCATime] < c.[CREATETIME])) 
                                        and len(c.FACEPHOTO) > 3 
                                        and c.CERTIFICATEID < {1}
                                        order by c.CERTIFICATEID desc ", MaxCountExe, cursor_long["CreateCA_Cur_CertificateID_CYZY"]);
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["CreateCA_Cur_CertificateID_CYZY"] = long.MaxValue - 1;//记录扫描位置

                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_long["CreateCA_Cur_CertificateID_CYZY"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            //pdf目录
            if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot)))
            {
                System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot));
            }

            var template_url = string.Format(@"{0}\Template\电子证书模板.pdf", ExamWebRoot);
            var save_pdf_url = "";//pdf生成位置
            string CertificateCAID = "";

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false)
                {
                    continue;//身份证错误的不生成电子证书，签章服务不接收
                }

                //创建pdf
                CertificateCAID = Guid.NewGuid().ToString();
                save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, CertificateCAID);//目标文件地址

                switch (dr["PostTypeID"].ToString())
                {
                    //case "1":
                    //    switch (dr["PostID"].ToString())
                    //    {
                    //        case "147":
                    //            template_url = string.Format(@"{0}\Template\三类人A.pdf", ExamWebRoot);
                    //            break;
                    //        case "148":
                    //            template_url = string.Format(@"{0}\Template\三类人B.pdf", ExamWebRoot);
                    //            break;
                    //        case "6":
                    //            template_url = string.Format(@"{0}\Template\三类人C2.pdf", ExamWebRoot);
                    //            break;
                    //        case "1123":
                    //            template_url = string.Format(@"{0}\Template\三类人C1.pdf", ExamWebRoot);
                    //            break;
                    //        case "1125":
                    //            template_url = string.Format(@"{0}\Template\三类人C3.pdf", ExamWebRoot);
                    //            break;
                    //    }
                    //    break;
                    //case "2":
                    //    template_url = string.Format(@"{0}\Template\特种作业.pdf", ExamWebRoot);
                    //    break;
                    case "4":
                        template_url = string.Format(@"{0}\Template\职业技能.pdf", ExamWebRoot);
                        break;
                    case "5"://关键岗位专业技术管理人员
                        template_url = string.Format(@"{0}\Template\电子证书模板.pdf", ExamWebRoot);
                        break;
                    case "4000"://新版职业技能
                        template_url = string.Format(@"{0}\Template\新版职业技能.pdf", ExamWebRoot);
                        save_pdf_url = string.Format(@"{0}\{1}", CAFile, CertificateCAID.Substring(CertificateCAID.Length - 3, 3));
                        if (!Directory.Exists(save_pdf_url))
                        {
                            System.IO.Directory.CreateDirectory(save_pdf_url);
                        }
                        save_pdf_url = string.Format(@"{0}\{1}.pdf", save_pdf_url, CertificateCAID);
                        break;
                }

                try
                {
                    var dic = ReadForm(template_url);//读取模板
                    FillForm(template_url, save_pdf_url, dic, dr);//填充模板
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成待签章的证书失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "创建", ex.Message);
                    continue;
                }

                try
                {
                    //更新证书表,写入申请时间
                    if (dr["PostTypeID"].ToString() == "4000")//新版职业技能
                    {
                        CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [ApplyCATime]='{1}',[ReturnCATime] = '{2}',[CertificateCAID]='{3}' where CertificateID={0}", dr["CertificateID"], DateTime.Now, DateTime.Now.AddSeconds(1), CertificateCAID));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [ApplyCATime]='{1}',[CertificateCAID]='{2}',[ReturnCATime] = null,[SendCATime] = null,[WriteJHKCATime] = null where CertificateID={0}", dr["CertificateID"], DateTime.Now, CertificateCAID));
                    }
                    FileLog.WriteLog(string.Format("创建电子证书{0}.pdf成功。", CertificateCAID));
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("保存待签章的证书信息失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        //        /// <summary>
        //        /// 发送待签章的证书到基础库DFS
        //        /// </summary>
        //        public void SendCA()
        //        {
        //            //return;//*********注意，里面有测试条件，正式部署时需要修改
        //            string save_pdf_url = "";//目标文件地址

        //            string sql = String.Format(@"Select top {0} 
        //                                            case 
        //                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
        //                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
        //                                            else c.UNITCODE end as 'qydm',
        //                                            c.* 
        //                                        from DBO.CERTIFICATE  c
        //                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
        //                                        Where (c.PostTypeID<3 or c.PostTypeID>3) and  c.PostID not in(55,159,1009,1021,1024)  
        //                                        and c.VALIDENDDATE > getdate() 
        //                                        and c.[ApplyCATime] >'2018-01-01' and  c.[SendCATime] is null
        //                                        order by c.MODIFYTIME desc ", MaxCountExe);
        //            //　and CertificateCAID='643ef6fa-67b9-4720-8461-1fe3a7a5b101'----测试专用

        //            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

        //            if (dtOriginal.Rows.Count == 0) return;

        //            //dfs接口访问账号
        //            string uName = descObj.EncryptString("RTDL_RYKWXT");
        //            string uPassword = descObj.EncryptString("RTDL_2013");
        //            string serviceID_zyjs = "ff81fce8-09a4-4a71-a272-eebb17e3da81";//专业技术
        //            string serviceID_tzzy = "de6b59da-e516-4f73-9854-d41fcfcd0f6f";//特种作业
        //            string serviceID_zyjn = "9bebd7e3-9142-496e-834f-3cf428b523a2";//职业技能
        //            string serviceID_a = "dae543ff-8858-49df-b2de-7436160f0609";//企业负责人
        //            string serviceID_b = "8cb1a7f4-f4d7-4d22-bc9b-08759ca85f59";//项目负责人
        //            string serviceID_c = "52a92a1f-1d0d-45e5-8b78-a8bf64593127";//安全专职员

        //            string mes = "";//dfs错误信息
        //            FileService.FileServiceSoapClient fs = new FileService.FileServiceSoapClient();
        //            DataSet ds = null;//待同步的DFS文件数据
        //            DataRow newRow = null;
        //            mes = fs.GetIServiceStruct(out ds, uName, uPassword, serviceID_zyjs);//获取数据结构
        //            if (mes != "成功")
        //            {
        //                FileLog.WriteLog(string.Format("同步待签章的证书PDF到DFS失败，错误信息：{0}", mes), null);
        //                return;
        //            }
        //            foreach (DataRow dr in dtOriginal.Rows)
        //            {
        //                try
        //                {
        //                    ds.Tables[0].Rows.Clear();
        //                    newRow = ds.Tables[0].NewRow();
        //                    ds.Tables[0].Rows.Add(newRow);

        //                    newRow["FileShowName"] = string.Format("{0}.pdf", dr["CertificateCode"]);
        //                    newRow["FileFullName"] = string.Format("{0}.pdf", dr["CertificateCAID"]);
        //                    newRow["FileID"] = dr["CertificateCAID"];
        //                    newRow["CertificateCode"] = dr["CertificateCode"];
        //                    newRow["FileClass"] = "从业人员附件";
        //                    newRow["FileSubClass"] = string.Format("{0}电子证书", dr["PostTypeName"]);
        //                    newRow["CopyStatus"] = 0;//新增状态

        //                    save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址
        //                    ////*********************************测试专用
        //                    //  save_pdf_url = string.Format(@"{0}\UpLoad\pdf\test\dqz\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址
        //                    //*********************************测试专用

        //                    byte[] fbyte = Utility.ImageHelp.FileToByte(save_pdf_url);

        //                    try
        //                    {
        //                        switch (dr["PostTypeID"].ToString())
        //                        {
        //                            case "5":
        //                                mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_zyjs, ds, fbyte);
        //                                break;
        //                            case "4":
        //                                mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_zyjn, ds, fbyte);
        //                                break;
        //                            case "2":
        //                                mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_tzzy, ds, fbyte);
        //                                break;
        //                            case "1":
        //                                switch (dr["PostID"].ToString())
        //                                {

        //                                    case "147":
        //                                        mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_a, ds, fbyte);
        //                                        break;
        //                                    case "148":
        //                                        mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_b, ds, fbyte);
        //                                        break;
        //                                    case "6":
        //                                        mes = fs.UploadFileWithInfo(uName, uPassword, serviceID_c, ds, fbyte);
        //                                        break;
        //                                }
        //                                break;
        //                        }


        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        FileLog.WriteLog("电子证书文件同步状态失败。", ex);
        //                        continue;
        //                    }

        //                    if (mes != "成功")
        //                    {
        //                        FileLog.WriteLog(string.Format("电子证书同步文件{0}.pdf失败，错误信息：{1}", dr["CertificateCAID"], mes), null);
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            //更新证书表,写入发送DFS时间
        //                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
        //                            FileLog.WriteLog(string.Format("同步电子证书{0}.pdf到DFS成功。", dr["CertificateCAID"]));

        //                            //删除临时文件pdf 
        //                            File.Delete(save_pdf_url);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            FileLog.WriteLog(string.Format("更新电子证书文件{0}.pdf同步状态失败。", dr["CertificateCAID"]), ex);
        //                            continue;
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    FileLog.WriteLog("电子证书文件同步状态失败。", ex);
        //                }
        //            }
        //        }

        /// <summary>
        /// 发送待签章的从业证书到109
        /// </summary>
        public void SendCA()
        {
            //return;//*********注意，里面有测试条件，正式部署时需要修改
            string save_pdf_url = "";//目标文件地址

            string sql = String.Format(@"Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        Where c.PostTypeID > 3 and c.PostTypeID < 6 and  c.PostID not in(55,159,1009,1021,1024)  
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[ApplyCATime] >'2018-01-01' and  c.[SendCATime] is null
                                        order by c.MODIFYTIME desc ", MaxCountExe);
            //　and CertificateCAID='643ef6fa-67b9-4720-8461-1fe3a7a5b101'----测试专用

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count == 0) return;

            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {
                    save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址

                    //同步待签章pdf
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\EXAM_CA\DGZ", dr["CertificateCAID"]), true);

                    //更新证书表,写入发送时间
                    CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));

                    FileLog.WriteLog(string.Format("同步待签章的证书{0}.pdf到150.175成功。", dr["CertificateCAID"]));

                    //删除临时文件pdf 
                    File.Delete(save_pdf_url);
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("同步待签章的证书{0}.pdf到150.175失败。", dr["CertificateCAID"]), ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "发送", ex.Message);
                    continue;
                }

            }
        }

        /// <summary>
        /// 从业人员证书PDF签章（部署在192.168.150.175)
        /// </summary>
        public void IssueCA_Certiticate(DateTime startTime)
        {
            //if (IssueCA_congyeing == true)
            //{
            //    return;
            //}
            //else
            //{
            //    IssueCA_congyeing = true;
            //}

            //return;//*********注意，里面有测试条件，正式部署时需要修改
            string save_pdf_url = "";//目标文件地址

            string sql = "";
            if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
            {
                sql = String.Format(@"
                                        Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        Where (c.PostTypeID<3 or c.PostTypeID>3) and c.PostTypeID < 6  and  c.PostID not in(55,159,1009,1021,1024)                                      
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and c.[SendCATime] >'2018-01-01' and c.[SendCATime] <'{1}' 
                                        and (c.[SendCATime] > dateadd(MINUTE,2,c.[SignCATime]) or (c.[SendCATime] is not null and c.[SignCATime] is null))
                                        and c.[SendCATime] < DATEADD(MINUTE,-2, GETDATE())
                                        and 
                                        (
                                            c.EleCertErrTime <DATEADD(hour,-24, GETDATE())
                                            or c.EleCertErrTime is null                                               
                                        )  
                                         and c.CERTIFICATEID < {2}
                                        order by c.CERTIFICATEID desc ", MaxCountExe, DateTime.Now.AddMinutes(-2), cursor_long["IssueCA_Cur_CertificateID"]);



            }
            else
            {
                sql = String.Format(@"
                                        Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                        Where (c.PostTypeID<3 or c.PostTypeID>3) and c.PostTypeID < 6 and  c.PostID not in(55,159,1009,1021,1024)                                      
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[STATUS] <>'注销'  and c.[STATUS] <>'离京变更' and c.[STATUS] <>'待审批' and c.[STATUS] <>'进京待审批'
                                        and c.[SendCATime] >'2018-01-01' and c.[SendCATime] <'{1}' 
                                        and (c.[SendCATime] > dateadd(MINUTE,2,[SignCATime]) or (c.[SendCATime] is not null and c.[SignCATime] is null))
                                        and c.[SendCATime] < DATEADD(MINUTE,-2, GETDATE())
                                        and 
                                        (
                                            c.EleCertErrTime <DATEADD(hour,-24, GETDATE())
                                            or c.EleCertErrTime is null                                               
                                        )  
                                         and c.CERTIFICATEID < {2}
                                        order by c.[MODIFYTIME] desc,c.CERTIFICATEID desc ", MaxCountExe, DateTime.Now.AddMinutes(-2), cursor_long["IssueCA_Cur_CertificateID"]);


            }
            //　and CertificateCAID='643ef6fa-67b9-4720-8461-1fe3a7a5b101'----测试专用

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);


            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_long["IssueCA_Cur_CertificateID"] = long.MaxValue - 1;//记录扫描位置
                if (dtOriginal.Rows.Count == 0)
                {
                    IssueCA_congyeing = false;
                    return;
                }
            }
            else
            {
                cursor_long["IssueCA_Cur_CertificateID"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
            }

            try
            {
                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        IssueCA_congyeing = false;
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。");
                IssueCA_congyeing = false;
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (EJJZS_Use_StartTime != startTime) return;
                try
                {
                    if (dr["WORKERCERTIFICATECODE"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false)
                    {
                        continue;//身份证错误的不生成电子证书，签章服务不接收
                    }
                    if (dr["license_code"] != DBNull.Value && dr["license_code"] != null)
                    {
                        //废弃
                        AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, dr);
                        if (abolishResult.AckCode != "SUCCESS")
                        {
                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的PDF失败！错误：{1}", dr["CertificateCode"], JSON.Encode(abolishResult.Errors)));
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "废止", JSON.Encode(abolishResult.Errors));
                            //continue;

                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
                            {
                                CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [license_code]=null,[auth_code]=null where CertificateID={0}", dr["CertificateID"]));
                            }
                        }
                        else
                        {
                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [license_code]=null,[auth_code]=null where CertificateID={0}", dr["CertificateID"]));
                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书。", dr["CertificateCode"]));
                        }

                    }

                    save_pdf_url = string.Format(@"D:\\zzk\EXAM_CA\GZJG\{0}.pdf", dr["CertificateCAID"]);
                    var model = new CreateRequestMdl
                    {
                        Data = new CreateRequestData { }
                    };

                    model.Data.IniPropoerty(dr);

                    CreateResponseResult result = Execute.CreatePDF(IssueCA_accessToken, dr["PostTypeID"].ToString(), model);

                    if (result != null)
                    {
                        //签发
                        if (result.AckCode == "SUCCESS" && result.Data != null)
                        {
                            FileLog.WriteLog(string.Format("成功对编号【{0}】电子证书签章！，[license_code]={1}，[auth_code]={2}。", dr["CertificateCode"], result.Data.LicenseCode, result.Data.AuthCode));

                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
                            CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [license_code]='{1}',[auth_code]='{2}',SignCATime=getdate(),[EleCertErrTime]=null,[EleCertErrDesc]=null,[EleCertErrStep]=null where CertificateID={0}", dr["CertificateID"], result.Data.LicenseCode, result.Data.AuthCode));

                            bool downAgain = false;//是否已经再次尝试下载
                        TrydownAgain://第一次下载失败，隔几秒再次尝试
                            //下载pdf
                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
                            if (downResult == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                                continue;
                            }
                            if (downResult.Data == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(downResult.Errors)));
                                WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "下载", JSON.Encode(downResult.Errors));

                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
                                {
                                    Thread.Sleep(5000);//暂停5秒
                                    downAgain = true;
                                    goto TrydownAgain;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {

                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);

                                ////更新证书表,写入发送DFS时间
                                //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】电子证书{1}.pdf。", dr["CertificateCode"], dr["CertificateCAID"]));


                                //移动已处理未盖章文件
                                File.Move(save_pdf_url.Replace("GZJG", "DGZ"), save_pdf_url.Replace("GZJG", "BAK_DGZ"));
                            }
                        }
                        else
                        {
                            FileLog.WriteLog(string.Format("签章PDF制证数据返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["CertificateCode"], JSON.Encode(result.Errors)));
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "签发", JSON.Encode(result.Errors));

                            if (JSON.Encode(result.Errors).Contains("已存在") || JSON.Encode(result.Errors).Contains("存在重复的电子证照"))
                            {
                                try
                                {
                                    AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken, dr, true);
                                }
                                catch (Exception ex2)
                                {
                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书失败，错误信息：{1}", dr["CertificateCode"], ex2.Message), ex2);
                                }
                            }
                            continue;
                        }
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("签章PDF制证数据返回结果为Null，证书编号：{0}。", dr["CertificateCode"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("签章PDF制证失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "签发", ex.Message);
                    CommonDAL.ExecSQL(string.Format("update [dbo].[CERTIFICATE] set [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' where [CERTIFICATEID]={0}"
                                   , dr["CERTIFICATEID"]
                                   , ex.Message
                                   , EnumManager.EleCertDoStep.IssueCA
                                   ));//更新电子证书生成结果
                    continue;
                }
            }
            IssueCA_congyeing = false;

        }



        /// <summary>
        /// 写电子证书到中间库（已作废，采用新接口，不用写中间库了）
        /// </summary>
        public void WriteJHKCA()
        {
            //已作废，采用新接口，不用写中间库了
            return;
            string sb = String.Format(@"Select top {0} 
                                            case 
                                            when f.[UNI_SCID] is not null then f.[UNI_SCID] 
                                            when f.[REG_NO] is not null and len(f.[REG_NO]) = 15 then f.[REG_NO]
                                            else c.UNITCODE end as 'qydm',
                                            c.* 
                                        from DBO.CERTIFICATE  c
                                        left join [dbo].[QY_FRK] f  on c.UNITCODE = f.[ORG_CODE]
                                         Where (c.PostTypeID<3 or c.PostTypeID>3) and  c.PostID not in(55,159,1009,1021,1024)  
                                        and c.VALIDENDDATE > dateadd(day,-1,getdate()) 
                                        and c.[ApplyCATime] >'2018-01-01' and  c.[WriteJHKCATime] is null                                        
                                        order by c.MODIFYTIME desc", MaxCountExe);

            DataTable dtOriginal = (new DBHelper()).GetFillData(sb.ToString());

            if (dtOriginal.Rows.Count == 0) return;

            string ZZMC = "";//证照名称
            string DOCUNAME = "";//签证名称
            string BUSSRULENUM = "";//签章规则
            string TEMPLATENUM = "";//签章模板
            string TableZM = "";//照面表
            string TableFJ = "";//附件表

            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {
                    switch (dr["PostTypeID"].ToString())
                    {
                        case "4":
                            ZZMC = "住房和城乡建设行业职业技能人员职业培训合格证";//证照名称
                            DOCUNAME = "考试合格章";//签证名称
                            BUSSRULENUM = "75BB0C3973555A41";//签章规则
                            TEMPLATENUM = "TMP_17897334e21148dcb096cd364937def5";//签章模板

                            TableZM = "EXAM_ZYJN_ZM";//照面表
                            TableFJ = "EXAM_ZYJN_FJ";//附件表
                            break;
                        case "5":
                            ZZMC = "住房和城乡建设领域专业人员岗位培训考核合格证书";//证照名称
                            DOCUNAME = "考试合格章";//签证名称
                            BUSSRULENUM = "75BB0C3973555A41";//签章规则
                            TEMPLATENUM = "TMP_17897334e21148dcb096cd364937def5";//签章模板
                            TableZM = "EXAM_ZYJS_ZM";//照面表
                            TableFJ = "EXAM_ZYJS_FJ";//附件表
                            break;
                        case "1":
                            switch (dr["PostID"].ToString())
                            {
                                case "6"://专职安全生产管理人员
                                case "1123":
                                case "1125":
                                    ZZMC = "建筑施工企业专职安全生产管理员安全生产考核合格证";//证照名称
                                    DOCUNAME = "住建委资格证书专用章";//签证名称
                                    BUSSRULENUM = "438AC45E5C0DA358";//签章规则
                                    TEMPLATENUM = "TMP_2ac19152f9ac4ef5a9ebc440b226873d";//签章模板
                                    TableZM = "EXAM_C_ZM";//照面表
                                    TableFJ = "EXAM_C_FJ";//附件表
                                    break;
                                case "147"://企业主要负责人
                                    ZZMC = "建筑施工企业主要负责人安全生产考核合格证书";//证照名称
                                    DOCUNAME = "住建委资格证书专用章";//签证名称
                                    BUSSRULENUM = "438AC45E5C0DA358";//签章规则
                                    TEMPLATENUM = "TMP_2ac19152f9ac4ef5a9ebc440b226873d";//签章模板
                                    TableZM = "EXAM_A_ZM";//照面表
                                    TableFJ = "EXAM_A_FJ";//附件表
                                    break;
                                case "148"://项目负责人
                                    ZZMC = "建筑施工企业项目负责人安全生产考核合格证书";//证照名称
                                    DOCUNAME = "住建委资格证书专用章";//签证名称
                                    BUSSRULENUM = "438AC45E5C0DA358";//签章规则
                                    TEMPLATENUM = "TMP_2ac19152f9ac4ef5a9ebc440b226873d";//签章模板
                                    TableZM = "EXAM_B_ZM";//照面表
                                    TableFJ = "EXAM_B_FJ";//附件表
                                    break;
                            }

                            break;
                        case "2":
                            ZZMC = "建筑施工特种作业操作资格证书";//证照名称
                            DOCUNAME = "住建委资格证书专用章";//签证名称
                            BUSSRULENUM = "438AC45E5C0DA358";//签章规则
                            TEMPLATENUM = "TMP_2ac19152f9ac4ef5a9ebc440b226873d";//签章模板
                            TableZM = "EXAM_TZZY_ZM";//照面表
                            TableFJ = "EXAM_TZZY_FJ";//附件表
                            break;
                    }

                    CommonDAL.ExecSQL(
                    string.Format(@"delete from {18}.[SJZJ_ZZK_JHK].[dbo].[{24}] where certificateid ={4};

                                            INSERT INTO {18}.[SJZJ_ZZK_JHK].[dbo].[{24}]([ID],[ZZMC],[TYDMNO],[SFZF],[CERTIFICATEID],[CERTIFICATECODE],[WORKERNAME],[UNITNAME],[CONFERDATE],[VALIDENDDATE],[CONFERUNIT],[CHECKDATE],[WORKERCERTIFICATECODE],[UNITCODE],[POSTTYPENAME],[POSTNAME],[CREATETIME],[GXZT])
                                            select '{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17};
                                            
                                            delete from {18}.[SJZJ_ZZK_JHK].[dbo].[{25}] where [ID] ='{0}';
                                            
                                            INSERT INTO {18}.[SJZJ_ZZK_JHK].[dbo].[{25}] ([ID] ,[CREDITCODE] ,[USERNAME] ,[DOCUNAME] ,[BUSSRULENUM] ,[TEMPLATENUM] ,[SHOWFILENAME] ,[FILENAME] ,[FILETYPE] ,[DATEPKVALUE] ,[CREATETIME] ,[GXZT])
                                            select '{0}','{19}','{20}','{21}','{22}','{23}','{5}.pdf','{0}.pdf','1','{4}','{16}',{17};

                                            update  DBO.CERTIFICATE set [WriteJHKCATime]='{16}' where CertificateID={4};",
                                        dr["CertificateCAID"],
                                        ZZMC,
                                        dr["qydm"],
                                        "Y",
                                        dr["CertificateID"],
                                        dr["CERTIFICATECODE"],
                                        dr["WORKERNAME"],
                                        dr["UNITNAME"],
                                        dr["CONFERDATE"],
                                        dr["VALIDENDDATE"],
                                        dr["CONFERUNIT"],
                                        dr["CHECKDATE"],
                                        dr["WORKERCERTIFICATECODE"],
                                        dr["UNITCODE"],
                                        dr["POSTTYPENAME"],
                                        dr["POSTNAME"],
                                        DateTime.Now,
                                        0,
                                        JHK_IP,
                                        "12110000400777687U",
                                          "北京市住房和城乡建设委员会",
                                        DOCUNAME,
                                        BUSSRULENUM,
                                        TEMPLATENUM,
                                        TableZM,
                                        TableFJ
                                    )
                    );

                    FileLog.WriteLog(string.Format("电子证书写中间库{0}.pdf成功。", dr["CertificateCAID"]));
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("电子证书写中间库{0}.pdf失败。", dr["CertificateCAID"]), ex);
                    continue;
                }
            }
        }
        /// <summary>
        /// 取回已签章的从业证书
        /// </summary>
        public void ReturnCA()
        {
            GetReturnCA("all", "");

        }


        /// <summary>
        /// 取回已签章的证书
        /// </summary>
        public void GetReturnCA(string typeNo, string serviceID)
        {
            DateTime stratTime = DateTime.Now;
            PDFReturnStartTime = stratTime;
            try
            {
                //FileLog.WriteLog("准备回写已签章的证书");

                //获取尚未成功签章证书信息
                string sql = "";
                DataTable dtOriginal = null;
                string fileFrom = "";//签章结果在109地址
                string fileTo = "";
                switch (typeNo)
                {
                    case "all"://从业人员

                        if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 9)
                        {
                            sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE 
                                                where  [SendCATime] < dateadd(MINUTE,2,[SignCATime]) and [ReturnCATime] is null 
                                                and VALIDENDDATE > dateadd(day,-1,getdate()) 
                                                and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                                                and PostTypeID < 6 
                                                and CertificateID <{1} and [SignCATime] <'{2}'
                                                and 
                                                (
                                                    EleCertErrTime <DATEADD(MINUTE,-4, GETDATE())
                                                    or EleCertErrTime is null                                               
                                                )   
                                                order by CertificateID desc", MaxCountExe, cursor_long["ReturnCA_Cur_CertificateID_all"], DateTime.Now.AddMinutes(-4));



                        }
                        else
                        {
                            sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE 
                                                where  [SendCATime] < dateadd(MINUTE,2,[SignCATime]) and [ReturnCATime] is null 
                                                and VALIDENDDATE >dateadd(day,-1,getdate()) 
                                                and [STATUS] <>'注销'  and [STATUS] <>'离京变更' and [STATUS] <>'待审批' and [STATUS] <>'进京待审批'
                                                and PostTypeID < 6 
                                                and CertificateID <{1} and [SignCATime] <'{2}'
                                                and 
                                                (
                                                    EleCertErrTime <DATEADD(MINUTE,-4, GETDATE())
                                                    or EleCertErrTime is null                                               
                                                )   
                                                order by [MODIFYTIME] desc,CertificateID desc", MaxCountExe, cursor_long["ReturnCA_Cur_CertificateID_all"], DateTime.Now.AddMinutes(-4));


                        }
                        dtOriginal = (new DBHelper()).GetFillData(sql);
                        //FileLog.WriteLog(sql);
                        if (dtOriginal.Rows.Count < MaxCountExe)
                        {
                            cursor_long["ReturnCA_Cur_CertificateID_all"] = long.MaxValue - 1;//记录扫描位置
                            if (dtOriginal.Rows.Count == 0)
                            {
                                return;
                            }
                        }
                        else
                        {
                            cursor_long["ReturnCA_Cur_CertificateID_all"] = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                        }
                        fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";
                        break;
                    //case "6"://专职安全生产管理人员
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and (PostID=6 or PostID=1123 or PostID=1125) order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_6, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    //FileLog.WriteLog(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_6 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_6 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";

                    //    break;
                    //case "147"://企业主要负责人
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and PostID=147 order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_147, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_147 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_147 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";
                    //    break;
                    //case "148"://项目负责人
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and PostID=148 order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_148, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_148 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_148 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";
                    //    break;
                    //case "2"://特种作业
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and PostTypeID=2 order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_2, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_2 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_2 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_TZZY\GZJG\";
                    //    break;
                    //case "4"://职业技能
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and PostTypeID=4 order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_4, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_4 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_4 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";
                    //    break;
                    //case "5"://专业技术
                    //    sql = String.Format(@"Select top {0} * from DBO.CERTIFICATE where [SendCATime] >'2018-01-01' and [SendCATime] <'{2}' and [ReturnCATime] is null and CertificateID >{1} and PostTypeID=5 order by CertificateID", MaxCountExe, ReturnCA_Cur_CertificateID_5, DateTime.Now.AddHours(-4));
                    //    dtOriginal = (new DBHelper()).GetFillData(sql);
                    //    if (dtOriginal.Rows.Count == 0)
                    //    {
                    //        ReturnCA_Cur_CertificateID_5 = 0;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReturnCA_Cur_CertificateID_5 = Convert.ToInt64(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["CertificateID"]);//记录扫描位置
                    //    }
                    //    fileFrom = @"\\192.168.150.175\zzk\EXAM_CA\GZJG\";
                    //    break;
                }

                ////dfs接口访问账号
                //string uName = descObj.EncryptString("RTDL_RYKWXT");
                //string uPassword = descObj.EncryptString("RTDL_2013");

                //string mes = "";//dfs错误信息
                //FileService.FileServiceSoapClient fs = new FileService.FileServiceSoapClient();
                //DataSet ds = null;//待同步的DFS文件数据
                //DataRow newRow = null;
                //mes = fs.GetIServiceStruct(out ds, uName, uPassword, serviceID);//获取数据结构
                //if (mes != "成功")
                //{
                //    FileLog.WriteLog(string.Format("同步签章后的证书PDF到DFS失败，错误信息：{0}", mes), null);
                //}

                foreach (DataRow dr in dtOriginal.Rows)
                {
                    if (stratTime != PDFReturnStartTime) return;
                    //sb.Append(string.Format(" or [FileFullName]='{0}.pdf'", dr["CertificateCAID"]));

                    try
                    {
                        if (File.Exists(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"])) == true)
                        {
                            //FileLog.WriteLog(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]));
                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
                            if (!Directory.Exists(fileTo))
                            {
                                System.IO.Directory.CreateDirectory(fileTo);
                            }
                            fileTo = string.Format(@"{0}\{1}.pdf", fileTo, dr["CertificateCAID"]);

                            File.Copy(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件



                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [ReturnCATime] = getdate() WHERE [CertificateCAID]='{0}'  and [ReturnCATime] is null;
                                                        if not exists(select 1 from [dbo].[CertificateCAHistory] where [CertificateCAID]='{0}') 
                                                        INSERT INTO [dbo].[CertificateCAHistory] ([CertificateCAID],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateID])
                                                        select [CertificateCAID],ApplyCATime,SendCATime,ReturnCATime,CertificateID from DBO.CERTIFICATE where [CertificateCAID]='{0}';", dr["CertificateCAID"]));

                            //File.Move(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.pdf", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));
                            Thread.Sleep(500);//暂停0.5秒
                            File.Delete(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]));

                            try
                            {
                                File.Delete(string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]));  //删除签章前的电子证书
                            }
                            catch { }

                            FileLog.WriteLog(string.Format("回写已签章的证书{0}成功。", fileTo));

                            //try
                            //{
                            //    ds.Tables[0].Rows.Clear();
                            //    newRow = ds.Tables[0].NewRow();
                            //    ds.Tables[0].Rows.Add(newRow);

                            //    newRow["FileShowName"] = string.Format("{0}.pdf", dr["CertificateCode"]);
                            //    newRow["FileFullName"] = string.Format("{0}.pdf", dr["CertificateCAID"]);
                            //    newRow["FileID"] = dr["CertificateCAID"];
                            //    newRow["CertificateCode"] = dr["CertificateCode"];
                            //    newRow["FileClass"] = "从业人员附件";
                            //    newRow["FileSubClass"] = string.Format("{0}电子证书", dr["PostTypeName"]);
                            //    newRow["CopyStatus"] = 2;//签章完成状态                 

                            //    byte[] fbyte = Utility.ImageHelp.FileToByte(fileTo);
                            //    mes = fs.UploadFileWithInfo(uName, uPassword, serviceID, ds, fbyte);
                            //}
                            //catch (Exception ex)
                            //{
                            //    FileLog.WriteLog("同步签章后的证书PDF到DFS失败失败。", ex);
                            //    continue;
                            //}
                        }
                        else
                        {
                            WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "取回", string.Format("未找到要取回签章后的文件{0}{1}.pdf", fileFrom, dr["CertificateCAID"]));

                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  and [ReturnCATime] is null;"
                           , dr["CertificateCAID"]
                            , string.Format("未找到要取回签章后的文件{0}{1}.pdf", fileFrom, dr["CertificateCAID"])
                       , EnumManager.EleCertDoStep.ReturnCert
                           ));//更新电子证书生成结果
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteEleCertError(dr["CertificateCode"].ToString(), dr["POSTTYPENAME"].ToString(), "取回", ex.Message);

                        CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CERTIFICATE] SET [EleCertErrTime]=GETDATE(),[EleCertErrDesc]='{1}',[EleCertErrStep]='{2}' WHERE [CertificateCAID]='{0}'  and [ReturnCATime] is null;"
                           , dr["CertificateCAID"]
                            , ex.Message
                       , EnumManager.EleCertDoStep.ReturnCert
                           ));//更新电子证书生成结果
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("取回已签章的证书失败。", ex);
            }
        }

        /// <summary>
        /// 移动过期历史电子证书到备份服务器5.180
        /// </summary>
        public void MoveHisCAFile()
        {
            return;////////////////////////////////////////////////////(2024-12-05停止移动) 

            DateTime stratTime = DateTime.Now;
            MoveHisCAFileStartTime = stratTime;

            DataTable dtOriginal = null;
            string fileFrom = "";
            string fileTo = "";

            //获取尚未移动的历史电子证书
            string sql = String.Format(@"SELECT top {0} [CertificateCAHistory].[CertificateCAID]
                                                  FROM [dbo].[CertificateCAHistory]
                                                  left join [dbo].[CERTIFICATE] on [CertificateCAHistory].[CertificateCAID] = [CERTIFICATE].[CertificateCAID]
                                                  where [CERTIFICATE].[CertificateCAID] is null and hisStatus is null and [CertificateCAHistory].ReturnCATime < '{1}'
                                                  order by [CertificateCAHistory].ReturnCATime desc", MaxCountExe,DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));

            dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (stratTime != MoveHisCAFileStartTime) return;
                try
                {
                    fileFrom = string.Format(@"{0}\{1}\{2}.pdf", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3), dr["CertificateCAID"]);
                    if (File.Exists(fileFrom) == true)
                    {
                        //fileTo = string.Format(@"\\192.168.5.180\d\hisCaFile\{0}.pdf", dr["CertificateCAID"]);
                        //File.Copy(fileFrom, fileTo,true);//移动文件
                        //File.Delete(fileFrom);


                        //fileTo = string.Format(@"\\192.168.5.180\d\hisCaFile\{0}.pdf", dr["CertificateCAID"]);
                        fileTo = string.Format(@"\\192.168.5.180\test\{0}.pdf", dr["CertificateCAID"]);
                        File.Move(fileFrom, fileTo);//移动文件

                        CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CertificateCAHistory] SET [hisStatus] = 1 WHERE [CertificateCAID]='{0}'  ;", dr["CertificateCAID"]));
                        FileLog.WriteLog(string.Format("移动历史电子证书{0}.pdf到5.180。", dr["CertificateCAID"]));

                        if (File.Exists(fileFrom.Replace(".pdf", ".ofd")) == true)
                        {
                            //File.Copy(fileFrom.Replace(".pdf", ".ofd"), fileTo.Replace(".pdf", ".ofd"), true);
                            //File.Delete(fileFrom.Replace(".pdf", ".ofd"));
                            File.Move(fileFrom.Replace(".pdf", ".ofd"), fileTo.Replace(".pdf", ".ofd"));
                            FileLog.WriteLog(string.Format("移动历史电子证书{0}.ofd到5.180。", dr["CertificateCAID"]));
                        }
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CertificateCAHistory] SET [hisStatus] = 0 WHERE [CertificateCAID]='{0}'  ;", dr["CertificateCAID"]));
                    }
                }
                catch (Exception ex)
                {
                    CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[CertificateCAHistory] SET [hisStatus] = 0 WHERE [CertificateCAID]='{0}'  ;", dr["CertificateCAID"]));
                    FileLog.WriteLog(string.Format("移动历史电子证书{0}.pdf到5.180失败。", dr["CertificateCAID"]), ex);
                    continue;
                }
            }
        }

        #endregion 从业人员电子证书业务

        #region 短信业务

        /// <summary>
        /// 发送实操考试短信提醒（创建实操准考证2个小时候才允许发送，预留准考证检查撤销时间）
        /// </summary>
        private void TipShiCaoExam()
        {
            string sql = @"select max(w.PHONE) PHONE
                            FROM [dbo].[ExamResult_Operation] c
                            inner join WORKER w on c.WorkerID = w.WorkerID
                            left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
                            on 
                                d.[Addtime]  between convert(varchar(10),dateadd(day,-30,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
                                and d.[sjlx]=43 
                                and d.[dxnr] like '您已通过建筑施工特种作业理论考核，请即日起登录报名系统%'
                                and w.PHONE= d.[phone]
                            where  c.CreateTime  between dateadd(hh,-1,c.CreateTime) and dateadd(day,+10,getdate())
                            and  dateadd(hh,+2,c.CreateTime) < getdate() and c.CreateTime > dateadd(day,+20,getdate())
                            and len(w.PHONE)=11 and d.[phone]  is null
                            group by w.PHONE;";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["PHONE"] != null && r["PHONE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["PHONE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["PHONE"].ToString(), "您已通过建筑施工特种作业理论考核，请即日起登录报名系统（http://zjw.beijing.gov.cn/）下载实操准考证，按要求准时参加。");
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送实操考试短信提醒{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 短信提醒二建续期（在二级建造师注册有效期届满前90、60天，尚未申请续期的,通过短信群友提醒申请人及时办理延续注册业务）
        /// 处理方法：有效期届满前90天至80天、60天至50天，每天确认是否发送短信进行了提醒。
        /// </summary>
        private void TipJzsContinue()
        {
            string sql = @"
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[COC_TOW_Person_BaseInfo] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
	and  a.ApplyType='延期注册' and c.PSN_ServerID = a.PSN_ServerID
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级建造师注册有效期即将届满%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+80,getdate()),21) and convert(varchar(10),dateadd(day,+90,getdate()),21)
    and d.[phone]  is null
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''
union
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[COC_TOW_Person_BaseInfo] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
    and  a.ApplyType='延期注册' and c.PSN_ServerID = a.PSN_ServerID
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级建造师注册有效期即将届满%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+50,getdate()),21) and convert(varchar(10),dateadd(day,+60,getdate()),21)
    and d.[phone]  is null 
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["MOBILE"].ToString(), "您的二级建造师注册有效期即将届满,为不影响您正常开展执业活动,请登录北京市住房和城乡建设领域人员资格管理系统,按要求及时提交延续注册申请。");
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送建造师续期提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 二级建造师延续注册，在注册有效期到期前29日，向申请人发送短信提醒，内容如下:
        /// 您的二级建造师注册证书距离注册专业有效期不足30天，无法申请延续注册，可等待证书有效期满后申请重新注册，或选择注销注册后再办理重新注册。
        /// </summary>
        private void TipJzsDisenableContinue()
        {
            string sql = @"
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[COC_TOW_Person_BaseInfo] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
    and  a.ApplyType='延期注册' and c.PSN_ServerID = a.PSN_ServerID
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级建造师注册证书距离注册专业有效期不足30天%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+20,getdate()),21) and convert(varchar(10),dateadd(day,+29,getdate()),21)
    and d.[phone]  is null 
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["MOBILE"].ToString(), "您的二级建造师注册证书距离注册专业有效期不足30天，无法申请延续注册，可等待证书有效期满后申请重新注册，或选择注销注册后再办理重新注册。");
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送二级建造师有效期不足30天,无法续期提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 短信提醒二级造价工程师续期（在有效期届满前90、60天，尚未申请续期的,通过短信群友提醒申请人及时办理延续注册业务）
        /// 处理方法：有效期届满前90天至80天、60天至50天，每天确认是否发送短信进行了提醒。
        /// </summary>
        private void TipZjgcsContinue()
        {
            string sql = @"
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[zjs_Certificate] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[zjs_Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
	and  a.ApplyType='延期注册' and c.PSN_RegisterNO = a.PSN_RegisterNO
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级造价工程师注册有效期即将届满%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+80,getdate()),21) and convert(varchar(10),dateadd(day,+90,getdate()),21)
    and d.[phone]  is null
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''
union
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[zjs_Certificate] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[zjs_Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
    and  a.ApplyType='延期注册' and c.PSN_RegisterNO = a.PSN_RegisterNO
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级造价工程师注册有效期即将届满%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+50,getdate()),21) and convert(varchar(10),dateadd(day,+60,getdate()),21)
    and d.[phone]  is null 
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["MOBILE"].ToString(), "您的二级造价工程师注册有效期即将届满,为不影响您正常开展执业活动,请登录北京市住房和城乡建设领域人员资格管理系统,按要求及时提交延续注册申请。");
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送二级造价工程师续期提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 二级造价工程师延续注册，在注册有效期到期前29日，向申请人发送短信提醒，内容如下:
        /// 您的二级造价工程师注册证书距离注册专业有效期不足30天，无法申请延续注册，可等待证书有效期满后申请初始注册，或选择注销注册后再办理初始注册。
        /// </summary>
        private void TipZjgcsDisenableContinue()
        {
            string sql = @"
select w.MOBILE,c.[PSN_RegisterNO],c.[PSN_CertificateValidity]
FROM [dbo].[zjs_Certificate] c
inner join [dbo].[WORKER] w  on c.PSN_CertificateNO = w.CERTIFICATECODE
left join [dbo].[zjs_Apply] a 
on a.ApplyTime between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
    and  a.ApplyType='延期注册' and c.PSN_RegisterNO = a.PSN_RegisterNO
left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
on  d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您的二级造价工程师注册证书距离注册专业有效期不足30天%'
    and c.[PSN_MobilePhone]= d.[phone]
where c.PSN_RegisteType < '07' 
    and c.PSN_CertificateValidity between convert(varchar(10),dateadd(day,+20,getdate()),21) and convert(varchar(10),dateadd(day,+29,getdate()),21)
    and d.[phone]  is null 
    and a.ApplyID is null
    and w.MOBILE is not null and w.MOBILE > ''";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["MOBILE"].ToString(), "您的二级造价工程师注册证书距离注册专业有效期不足30天，无法申请延续注册，可等待证书有效期满后申请初始注册，或选择注销注册后再办理初始注册。");
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送二级造价工程师续期提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 原短信提醒二建申诉（由于取消公示环节，直接公告，所以停止发送申述提醒，现改为公告后提醒不通过。）
        /// 处理方法：审批公公告后10天内，每天确认是否发送短信进行了提醒。
        /// </summary>
        private void TipJzsShenSu()
        {
            /// <summary>
            /// 短信提醒二建申诉（在二级建造师初始、重新、延续注册公示期内,通过短信群发提醒审查不通过的申请人及时提交申诉资料。）
            /// 处理方法：审批公示10天内，公告前，每天确认是否发送短信进行了提醒。
            //            string sql = @"select w.MOBILE,a.[ApplyType],a.[PublicDate]
            //                              FROM [dbo].[Apply] a
            //                              inner join [dbo].[WORKER] w  on a.PSN_CertificateNO = w.CERTIFICATECODE
            //                              left join [192.168.7.89].[PlatInfo].[dbo].[t_dx_fsnr]  d 
            //                              on d.[sjlx]=43 
            //                              and d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
            //                              and d.[dxnr] like '%申诉%'
            //                              and w.MOBILE= d.[phone]
            //                              where (a.[ApplyType]='初始注册' or a.[ApplyType]='重新注册' or a.[ApplyType]='延期注册')
            //                              and a.[PublicDate] between convert(varchar(10),dateadd(day,-10,getdate()),21) and  convert(varchar(10),dateadd(day,+1,getdate()),21)
            //                              and a.[NoticeDate] is null and a.[ConfirmResult]='不通过'
            //                              and d.[phone]  is null";

            string sql = @"select w.MOBILE,case when a.[ApplyType] ='变更注册' then a.[ApplyTypeSub] else a.[ApplyType] end as ApplyType,a.[ApplyStatus]
                            FROM [dbo].[Apply] a
                            inner join [dbo].[WORKER] w  on a.PSN_CertificateNO = w.CERTIFICATECODE
                            left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
                            on d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
                                and d.[sjlx]=43
                                and d.[dxnr] like '您的二级建造师%申请审批不通过%'
                                and w.MOBILE= d.[phone]
                                where a.[NoticeDate] between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21)
                                and a.[ConfirmResult]='不通过'
                                and d.[phone]  is null and w.MOBILE is not null and w.MOBILE > ''
                            union
                            select w.MOBILE,case when a.[ApplyType] ='变更注册' then a.[ApplyTypeSub] else a.[ApplyType] end as ApplyType,a.[ApplyStatus]
                            FROM [dbo].[Apply] a
                            inner join [dbo].[WORKER] w  on a.PSN_CertificateNO = w.CERTIFICATECODE
                            left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] d 
                            on d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
                               and d.[sjlx]=43 
                               and d.[dxnr] like '您的二级建造师%被注册所在区住建委驳回不予受理%'
                               and w.MOBILE= d.[phone]
                            where a.ApplyStatus='已驳回' 
                                and (
		                                (a.GetResult='不通过' and a.GetDateTime between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21))
		                                or (a.ExamineResult='不通过' and a.ExamineDatetime between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21))
                                )                         
                                and d.[phone]  is null and w.MOBILE is not null and w.MOBILE > ''";

            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    if (r["ApplyStatus"].ToString() == "已驳回")
                    {
                        CommonDAL.SendMessage(r["MOBILE"].ToString(), string.Format("您的二级建造师{0}申请被注册所在区住建委驳回不予受理，具体原因可登录注册系统查询。",r["ApplyType"].ToString().Replace("延期", "延续")));
                    }
                    else//已公告
                    {
                        CommonDAL.SendMessage(r["MOBILE"].ToString(), string.Format("您的二级建造师{0}申请审批不通过，具体原因可登录注册系统查询。", r["ApplyType"].ToString().Replace("延期", "延续")));
                    }
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送建造师审批不通过提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 从业人员证书申请业务受理、审核驳回短信通知
        /// </summary>
        private void TipCongYeCertApply()
        {
            string sql = @"
select w.MOBILE,'您申请的考试报名业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[EXAMSIGNUP] a
    inner join [dbo].[WORKER] w  on a.CERTIFICATECODE = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的考试报名业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。%'
	    and w.MOBILE= d.[phone]
    where a.AcceptTime > dateadd(day,-2,getdate()) and a.AcceptResult <> '通过' and a.[status] = '退回修改' and d.[phone]  is null
    and w.MOBILE is not null and w.MOBILE > ''
union 
select w.MOBILE,'您申请的考试报名业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[EXAMSIGNUP] a
    inner join [dbo].[WORKER] w  on a.CERTIFICATECODE = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的考试报名业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。%'
	    and w.MOBILE= d.[phone]
    where a.CHECKDATE > dateadd(d,-2,getdate()) and a.CHECKRESULT <> '通过' and a.[status] = '退回修改' and d.[phone]  is null
    and w.MOBILE is not null and w.MOBILE > ''
union
    select w.MOBILE,'您申请的考试报名业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[EXAMSIGNUP] a
    inner join [dbo].[WORKER] w  on a.CERTIFICATECODE = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
    and d.[sjlx]=43 
    and d.[dxnr] like '您申请的考试报名业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。%'
    and w.MOBILE= d.[phone]
    where a.PayConfirmDate > dateadd(day,-2,getdate()) and a.PayConfirmRult <> '通过' and a.[status] = '退回修改' 
    and d.[phone]  is null
    and w.MOBILE is not null and w.MOBILE > ''
union
    select  w.MOBILE,'您申请的' + a.CHANGETYPE +'业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CERTIFICATECHANGE] a
    inner join [dbo].[WORKER] w  on a.[NEWWORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的' + a.CHANGETYPE +'业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.[GetDate] > dateadd(day,-2,getdate()) and a.GetResult <> '通过' and a.[status] = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
    select  w.MOBILE,'您申请的' + a.CHANGETYPE +'业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CERTIFICATECHANGE] a
    inner join [dbo].[WORKER] w  on a.[NEWWORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的' + a.CHANGETYPE +'业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.[ConfrimDate] > dateadd(day,-2,getdate()) and a.ConfrimResult <> '通过' and a.[status] = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
    select  w.MOBILE,'您申请的证书进京业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CERTIFICATEENTERAPPLY] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书进京业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.AcceptDate > dateadd(day,-2,getdate()) and a.GetResult <> '通过' and a.ApplyStatus = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书进京业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CERTIFICATEENTERAPPLY] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书进京业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.CheckDate > dateadd(day,-2,getdate()) and a.CheckResult <> '通过' and a.ApplyStatus = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
    select  w.MOBILE,'您申请的证书续期业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[VIEW_CERTIFICATECONTINUE] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on  
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43
	    and d.[dxnr] like '您申请的证书续期业务，受理意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.[GetDate] > dateadd(day,-2,getdate()) and a.GetResult <> '初审通过' and a.[STATUS] = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书续期业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[VIEW_CERTIFICATECONTINUE] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书续期业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.CheckDate > dateadd(day,-2,getdate()) and a.CheckResult <> '审核通过' and a.[STATUS] = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书续期业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[VIEW_CERTIFICATECONTINUE] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书续期业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.ConfirmDate > dateadd(day,-2,getdate()) and a.ConfirmResult <> '决定通过' and a.[STATUS] = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书合并业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CertificateMerge] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书合并业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.CheckDate > dateadd(day,-2,getdate()) and a.CheckAdvise <> '通过' and a.ApplyStatus = '退回修改' and d.[phone]  is null and a.CreatePersonID > 0
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书增发业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CertificateMore] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书增发业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.CheckDate > dateadd(day,-2,getdate()) and a.[CheckAdvise] <> '通过' and a.[ApplyStatus] = '退回修改' and d.[phone]  is null 
    and w.MOBILE is not null and w.MOBILE > ''
union
	select  w.MOBILE,'您申请的证书增发业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。' as dx
    FROM [dbo].[CertificateMore] a
    inner join [dbo].[WORKER] w  on a.[WORKERCERTIFICATECODE] = w.CERTIFICATECODE
    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
    on 
	    d.[Addtime]  between convert(varchar(10),dateadd(day,-3,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
        and d.[sjlx]=43 
	    and d.[dxnr] like '您申请的证书增发业务，审核意见为不通过，请及时登录人员资格管理系统查看具体原因。'
	    and w.MOBILE= d.[phone]
    where a.[ConfirmDate] > dateadd(day,-2,getdate()) and a.[ConfirmAdvise] <> '通过' and a.ApplyStatus = '退回修改' and d.[phone]  is null 
    and w.MOBILE is not null and w.MOBILE > ''
";
            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    CommonDAL.SendMessage(r["MOBILE"].ToString(), r["dx"].ToString());
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("从业人员业务受理、审核不通过短信提醒{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 二级造价工程师初始/变更/延续/注销注册受理不通过和决定不通过环节，向申请人发送短信提醒
        /// </summary>
        private void TipZjgcsCheckFalse()
        {

            string sql = @"select w.MOBILE,a.ApplyType,a.[ApplyStatus]
                            FROM [dbo].[zjs_Apply] a
                            inner join [dbo].[WORKER] w  on a.PSN_CertificateNO = w.CERTIFICATECODE
                            left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
                            on 
                                d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
                                and d.[sjlx]=43 
                                and d.[dxnr] like '您申请的北京市二级造价工程师%'
                                and w.MOBILE= d.[phone]
                                where a.[NoticeDate] between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21)
                                and a.[ConfirmResult]='不通过'
                                and d.[phone]  is null
                                and w.MOBILE is not null and w.MOBILE > ''
                            union
                            select w.MOBILE,a.ApplyType,a.[ApplyStatus]
                            FROM [dbo].[zjs_Apply] a
                            inner join [dbo].[WORKER] w  on a.PSN_CertificateNO = w.CERTIFICATECODE
                            left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] d 
                            on 
                                d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
                                and d.[sjlx]=43 
                                and d.[dxnr] like '您申请的北京市二级造价工程师%'
                                and w.MOBILE= d.[phone]
                            where a.ApplyStatus='已驳回' 
                                and (
		                                (a.GetResult='不通过' and a.GetDateTime between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21))
		                                or (a.ExamineResult='不通过' and a.ExamineDatetime between convert(varchar(10),dateadd(day,-4,getdate()),21) and  convert(varchar(10),dateadd(day,-1,getdate()),21))
                                )                         
                                and d.[phone]  is null
                                and w.MOBILE is not null and w.MOBILE > ''";

            DataTable dt = CommonDAL.GetDataTable(sql);
            int dCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MOBILE"] != null && r["MOBILE"] != DBNull.Value && Utility.Check.IfPhoneFormat(r["MOBILE"].ToString()) == true)
                {
                    if (r["ApplyStatus"].ToString() == "已驳回")
                    {
                        CommonDAL.SendMessage(r["MOBILE"].ToString(), string.Format("您申请的北京市二级造价工程师{0}业务，受理意见为不通过，具体原因可登录注册系统查询。", r["ApplyType"]));
                    }
                    else//已公告
                    {
                        CommonDAL.SendMessage(r["MOBILE"].ToString(), string.Format("您申请的北京市二级造价工程师{0}业务，审批意见为不通过，具体原因可登录注册系统查询。", r["ApplyType"]));
                    }
                    dCount++;
                }
            }

            if (dCount > 0)
            {
                WriteOperateLog("系统服务", 0, string.Format("发送二级造价工程师审批不通过提醒短信{0}条", dCount), "");
            }
        }

        /// <summary>
        /// 给安管人员/特种作业人员证书有效期届满前15天仍未提交续期申请的持证人发送短信提醒。
        /// 短信内容：您的安管人员证书有效期不足15天,请联系企业办理证书延期。
        /// </summary>
        private void TipCongContinue()
        {
            try
            {
                string sql = @"	select count(distinct  w.MOBILE)
	                        FROM [dbo].[Certificate] c
	                        inner join [dbo].[WORKER] w  on c.WorkerCertificateCode = w.CERTIFICATECODE
	                        left join [dbo].[CertificateContinue] a 
	                        on a.ApplyDate between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
		                        and  c.CertificateID = a.CertificateID
	                        left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
	                        on 
		                        d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
		                        and d.[sjlx]=43 
		                        and d.[dxnr] like '%证书有效期不足15天,请联系企业办理证书延期，逾期作废。%'
		                        and w.MOBILE= d.[phone]
	                        where c.PostTypeID < 3
		                        and c.ValidEndDate between convert(varchar(10),getdate(),21) and convert(varchar(10),dateadd(day,+16,getdate()),21)
		                        and  (c.[STATUS] = '首次' or c.[STATUS] = '续期' or c.[STATUS] = '进京变更' or c.[STATUS] = '京内变更' or c.[STATUS] = '补办')
		                        and d.[phone]  is null
		                        and a.CertificateID is null
                                and w.MOBILE is not null and w.MOBILE > ''";
                int dCount = CommonDAL.SelectRowCount(sql);

                sql = @"INSERT INTO [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]([sjlx],[phone],[dxnr],[Addtime],[fslx],[ID],[VALID])
                    select 43 , [phone],'您的安管人员/特种作业证书有效期不足15天,请联系企业办理证书延期，逾期作废。' ,convert(varchar(13),getdate(),120) ,0 ,newid(),1
                    from
                    (
	                    select distinct  w.MOBILE as [phone]
	                    FROM [dbo].[Certificate] c
	                    inner join [dbo].[WORKER] w  on c.WorkerCertificateCode = w.CERTIFICATECODE
	                    left join [dbo].[CertificateContinue] a 
	                    on a.ApplyDate between convert(varchar(10),dateadd(day,-90,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21) 
		                    and  c.CertificateID = a.CertificateID
	                    left join [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr]  d 
	                    on 
		                    d.[Addtime]  between convert(varchar(10),dateadd(day,-15,getdate()),21) and convert(varchar(10),dateadd(day,+1,getdate()),21)
		                    and d.[sjlx]=43 
		                    and d.[dxnr] like '%证书有效期不足15天,请联系企业办理证书延期%'
		                    and w.MOBILE= d.[phone]
	                    where c.PostTypeID < 3
		                    and c.ValidEndDate between convert(varchar(10),getdate(),21) and convert(varchar(10),dateadd(day,+16,getdate()),21)
		                    and  (c.[STATUS] = '首次' or c.[STATUS] = '续期' or c.[STATUS] = '进京变更' or c.[STATUS] = '京内变更' or c.[STATUS] = '补办')
		                    and d.[phone]  is null
		                    and a.CertificateID is null
                            and w.MOBILE is not null and w.MOBILE > ''
                    ) t ";
                CommonDAL.ExecSQL(sql);

                if (dCount > 0)
                {
                    WriteOperateLog("系统服务", 0, string.Format("发送安管人员、特种作业证书有效期不足15天提醒短信{0}条", dCount), "");
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("发送安管人员、特种作业证书有效期不足15天提醒短信失败，错误信息：" + ex.Message, ex);
                return;
            }
        }

        #endregion 短信业务

        #region 考务业务

        /// <summary>
        /// 超龄注销：项目负责人（三类人员安全考核）年龄上限为65周岁。
        /// 专职安全生产管理人员（三类人员安全考核）、专业技术管理人员、造价员和建设职业技能人员年龄上限为60周岁。
        /// 建筑施工特种作业人员年龄上限为男60周岁女50周岁。
        /// 每日按照上述限制系统自动执行超龄注销操作，在证书备注中标明超龄注销日期。
        /// </summary>
        private void LimitAgeOutCertificate()
        {
            #region 从业人员证书超龄注销
            int dataCount=0;
            DBHelper db = null;
            DbTransaction tran = null;

            string sql = @"update dbo.certificate set STATUS='注销',MODIFYTIME='{0}',CheckDate='{0}',remark ='超龄注销，注销日期：{1}' 
                            where ValidEndDate >= getdate() 
                            and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
                            and (
                                   (postid=148 and  dateadd(day,-1,dateadd(year,-65,getdate())) > [BIRTHDAY] )
                                    or 
			                        (
				                        posttypeid=2 and [SEX]='女' 
				                        and  dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 2 +1),dateadd(year,-50,getdate()))) > [BIRTHDAY] 
			                        )
                                    or
                                    (
	                                    (posttypeid=4 or posttypeid=5 or ((postid=6 or postid=1123 or postid=1125 or posttypeid=2) and [SEX]='男'))
 	                                    and dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 4 +1),dateadd(year,-60,getdate()))) > [BIRTHDAY]
                                    )
			                        or
                                    (
	                                    (postid=6 or postid=1123 or postid=1125) and [SEX]='女'
 	                                    and dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 4 +1),dateadd(year,-55,getdate()))) > [BIRTHDAY]
                                    )
                            )
                            and BIRTHDAY >'1900-01-01' and BIRTHDAY is not null";

            string sqlcount = @"and ValidEndDate >= getdate() 
                            and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
                            and (
                                   (postid=148 and  dateadd(day,-1,dateadd(year,-65,getdate())) > [BIRTHDAY] )
                                    or 
			                        (
				                        posttypeid=2 and [SEX]='女' 
				                        and  dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 2 +1),dateadd(year,-50,getdate()))) > [BIRTHDAY] 
			                        )
                                    or
                                    (
	                                    (posttypeid=4 or posttypeid=5 or ((postid=6 or postid=1123 or postid=1125 or posttypeid=2) and [SEX]='男'))
 	                                    and dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 4 +1),dateadd(year,-60,getdate()))) > [BIRTHDAY]
                                    )
			                        or
                                    (
	                                    (postid=6 or postid=1123 or postid=1125) and [SEX]='女'
 	                                    and dateadd(day,-1,dateadd(month,-((year(getdate()) -2025) *12 + Month(getdate()) / 4 +1),dateadd(year,-55,getdate()))) > [BIRTHDAY]
                                    )
                            )
                            and BIRTHDAY >'1900-01-01' and BIRTHDAY is not null";



            if(DateTime.Now > Convert.ToDateTime("2040-01-01"))
            {
                sql = @"update dbo.certificate set STATUS='注销',MODIFYTIME='{0}',CheckDate='{0}',remark ='超龄注销，注销日期：{1}' 
                            where ValidEndDate >=getdate() 
                              and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
                             and (
                                   (postid=148 and  dateadd(day,-1,dateadd(year,-65,getdate())) > [BIRTHDAY] )
                                    or 
			                        (
				                        posttypeid=2 and [SEX]='女' 
				                        and dateadd(day,-1,dateadd(year,-55,getdate())) > [BIRTHDAY] 
			                        )
                                    or
                                    (
	                                    (posttypeid=4 or posttypeid=5 or ((postid=6 or postid=1123 or postid=1125 or posttypeid=2) and [SEX]='男'))
 	                                    and dateadd(day,-1,dateadd(year,-63,getdate())) > [BIRTHDAY]
                                    )
			                        or
                                    (
	                                    (postid=6 or postid=1123 or postid=1125) and [SEX]='女'
 	                                    and dateadd(day,-1,dateadd(year,-58,getdate())) > [BIRTHDAY]
                                    )
                            )
                            and BIRTHDAY >'1900-01-01' and BIRTHDAY is not null";

                sqlcount = @"and ValidEndDate >=getdate() 
                              and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
                             and (
                                   (postid=148 and  dateadd(day,-1,dateadd(year,-65,getdate())) > [BIRTHDAY] )
                                    or 
			                        (
				                        posttypeid=2 and [SEX]='女' 
				                        and dateadd(day,-1,dateadd(year,-55,getdate())) > [BIRTHDAY] 
			                        )
                                    or
                                    (
	                                    (posttypeid=4 or posttypeid=5 or ((postid=6 or postid=1123 or postid=1125 or posttypeid=2) and [SEX]='男'))
 	                                    and dateadd(day,-1,dateadd(year,-63,getdate())) > [BIRTHDAY]
                                    )
			                        or
                                    (
	                                    (postid=6 or postid=1123 or postid=1125) and [SEX]='女'
 	                                    and dateadd(day,-1,dateadd(year,-58,getdate())) > [BIRTHDAY]
                                    )
                            )
                            and BIRTHDAY >'1900-01-01' and BIRTHDAY is not null";
            }

            try
            {
                dataCount = CommonDAL.SelectRowCount("dbo.certificate", sqlcount);
                CommonDAL.ExecSQL(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd")));
                WriteOperateLog("系统服务", 0, string.Format("超龄注销证书{0}本", dataCount), "");
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("批量超龄注销证书错误：" + ex.Message, ex);
            }

            #endregion 从业人员证书超龄注销

            #region 二级建造师过期注销（无预警数据：在施锁定、异常注册；无续期申请）

            //统计代办证书数量
            sqlcount=@"select count(*) from COC_TOW_Person_BaseInfo
                        where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1
                        and b.ApplyStatus is null
                        and A.PSN_RegisteType < '07'
                        and A.PSN_Level = '二级'
                        and (c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)
                        and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PSN_ServerID =a.PSN_ServerID group by PSN_ServerID having MAX(PRO_ValidityEnd)< Convert(varchar(10),dateadd(day,-1,getdate()),21))
                        )";
            try
            {
                dataCount = CommonDAL.SelectRowCount(sqlcount);
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("统计待注销的过期二建证书错误：" + ex.Message, ex);
                return;
            }

            //1、插入到历史表并且更新二建基本表
            sql = @"insert into COC_TOW_Person_BaseInfo_His (
                        [HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode]
                        ,[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate]) 
                    select  NEWID(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                            ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                            ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                            ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                            ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                            ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent]
                            ,[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],CONVERT(varchar(100), GETDATE(), 23)
                    from COC_TOW_Person_BaseInfo  
                    where PSN_ServerID in(
                        select A.PSN_ServerID
                        from dbo.View_JZS_TOW_WithProfession A 
                        left join [dbo].[View_JZS_TOW_Applying] B 
                            on a.PSN_ServerID=b.PSN_ServerID 
                        left join [jcsjk_RY_JZS_ZSSD] C 
                            on a.PSN_RegisterNO=c.ZCH 
                        left join [dbo].[LockJZS] L
                            on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                        where 1=1 and b.ApplyStatus is null
			                    and A.PSN_RegisteType < '07'
			                    and A.PSN_Level = '二级'
			                    and (c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)
			                    and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PSN_ServerID =a.PSN_ServerID group by PSN_ServerID having MAX(PRO_ValidityEnd)< Convert(varchar(10),dateadd(day,-1,getdate()),21))
                     );";

             db = new DBHelper();
             tran = db.BeginTransaction();
             try
             {
                 CommonDAL.ExecSQL(tran, sql);

                 //2、更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期
                 sql = @"Update COC_TOW_Person_BaseInfo 
                    set [PSN_RegisteType]='07',PSN_RegistePermissionDate='{0}',[XGR]='{1}',[PSN_CancelReason]='{2}',XGSJ='{0}',PSN_CancelPerson='省级建设主管部门' 
                    where PSN_ServerID in(
                        select A.PSN_ServerID
                        from dbo.View_JZS_TOW_WithProfession A 
                        left join [dbo].[View_JZS_TOW_Applying] B 
                            on a.PSN_ServerID=b.PSN_ServerID 
                        left join [jcsjk_RY_JZS_ZSSD] C 
                            on a.PSN_RegisterNO=c.ZCH 
                        left join [dbo].[LockJZS] L
                            on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                        where 1=1
                            and b.ApplyStatus is null
                            and A.PSN_RegisteType < '07'
                            and A.PSN_Level = '二级'
                            and (c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)
                            and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PSN_ServerID =a.PSN_ServerID group by PSN_ServerID having MAX(PRO_ValidityEnd)< Convert(varchar(10),dateadd(day,-1,getdate()),21))
                        )";
                 CommonDAL.ExecSQL(tran, string.Format(sql, DateTime.Now.Date,"系统自动服务","证书过期"));
                 tran.Commit();
                 WriteOperateLog("系统服务", 0, string.Format("批量注销过期二建注册建造师证书{0}本", dataCount), "");
             }
             catch (Exception ex)
             {
                 tran.Rollback();
                 FileLog.WriteLog("【系统服务LimitAgeOutCertificate】批量注销过期二建证书错误：" + ex.Message, ex);
                 return;
             }

            #endregion 二级建造师过期注销

             #region 二级建造师专业过期注销（无预警数据：在施锁定、异常注册；无续期申请）

             //统计代办证书数量
             sqlcount = @"select count(*) from COC_TOW_Person_BaseInfo
                        where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1
                        and b.ApplyStatus is null
                        and A.PSN_RegisteType < '07'
                        and A.PSN_Level = '二级'
                        and (c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)
                        and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                        and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <=Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                        )";
             try
             {
                 dataCount = CommonDAL.SelectRowCount(sqlcount);
             }
             catch (Exception ex)
             {
                 FileLog.WriteLog("统计待注销的过期二建证书错误：" + ex.Message, ex);
                 return;
             }

             //1、插入到历史表并且更新二建基本表
             sql = @"insert into COC_TOW_Person_BaseInfo_His (
                        [HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode]
                        ,[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate]) 
                    select  NEWID(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                            ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                            ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                            ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                            ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                            ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent]
                            ,[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],CONVERT(varchar(100), GETDATE(), 23)
                    from COC_TOW_Person_BaseInfo  
                    where PSN_ServerID in(
                        select A.PSN_ServerID
                        from dbo.View_JZS_TOW_WithProfession A 
                        left join [dbo].[View_JZS_TOW_Applying] B 
                            on a.PSN_ServerID=b.PSN_ServerID 
                        left join [jcsjk_RY_JZS_ZSSD] C 
                            on a.PSN_RegisterNO=c.ZCH 
                        left join [dbo].[LockJZS] L
                            on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                        where 1=1 and b.ApplyStatus is null
			                    and A.PSN_RegisteType < '07'
			                    and A.PSN_Level = '二级'
			                    and (c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)
			                    and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                                and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <= Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                     );";

             db = new DBHelper();
             tran = db.BeginTransaction();
             try
             {
                 CommonDAL.ExecSQL(tran, sql);

                 //2、将专业表数据添加至专业历史表
                 sql = @"insert into [dbo].[COC_TOW_Register_Profession_His](
                        [His_ID]
                        ,[PRO_ServerID]
                        ,[PSN_ServerID]
                        ,[PRO_Profession]
                        ,[PRO_ValidityBegin]
                        ,[PRO_ValidityEnd]
                        ,[DogID]
                        ,[ENT_Province_Code]
                        ,[DownType]
                        ,[LastModifyTime]
                        ,[ApplyType]
                        ,[GetDate]
                    ) 
                    select NEWID()
                        ,c.[PRO_ServerID]
                        ,c.[PSN_ServerID]
                        ,c.[PRO_Profession]
                        ,c.[PRO_ValidityBegin]
                        ,c.[PRO_ValidityEnd]
                        ,c.[DogID]
                        ,c.[ENT_Province_Code]
                        ,c.[DownType]
                        ,c.[LastModifyTime]
                        ,'注销'
                        ,CONVERT(varchar(100), GETDATE(), 23)
                    from dbo.COC_TOW_Person_BaseInfo A 
                    inner join COC_TOW_Register_Profession c on A.PSN_ServerID=c.PSN_ServerID
                    WHERE c.PRO_ValidityEnd<='{0}' 
                          and a.PSN_ServerID in(
                                select A.PSN_ServerID
                                from dbo.View_JZS_TOW_WithProfession A 
                                left join [dbo].[View_JZS_TOW_Applying] B 
                                    on a.PSN_ServerID=b.PSN_ServerID 
                                left join [jcsjk_RY_JZS_ZSSD] C 
                                    on a.PSN_RegisterNO=c.ZCH 
                                left join [dbo].[LockJZS] L
                                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                                where 1=1 and b.ApplyStatus is null
                                and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                                and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <= Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                        );";
                 CommonDAL.ExecSQL(tran, string.Format(sql, DateTime.Now.Date));

                 //3、更新该人的注册专业
                 sql = @"
                    update [dbo].[COC_TOW_Person_BaseInfo] 
                    set [dbo].[COC_TOW_Person_BaseInfo].PSN_RegisteProfession =c.PRO_Profession,
	                    [dbo].[COC_TOW_Person_BaseInfo].PSN_CertificateValidity=c.PRO_ValidityEnd,
						[dbo].[COC_TOW_Person_BaseInfo].[XGSJ] = getdate()
                    from 
                    (
                        select PSN_ServerID,
                        PRO_Profession=STUFF((select ','+[PRO_Profession] from COC_TOW_Register_Profession a where a.PSN_ServerID=b.PSN_ServerID and PRO_ValidityEnd > '{0}' for xml PATH('')), 1, 1, ''),
                        PRO_ValidityEnd=STUFF((select ','+ convert(varchar(10),max([PRO_ValidityEnd]),20)  from COC_TOW_Register_Profession a where a.PSN_ServerID=b.PSN_ServerID and PRO_ValidityEnd > '{0}' for xml PATH('')), 1, 1, '')
                        from COC_TOW_Register_Profession  b 
                        group by b.PSN_ServerID
                    ) c 
                    where [dbo].[COC_TOW_Person_BaseInfo].PSN_ServerID=c.PSN_ServerID 
                        and c.PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 and b.ApplyStatus is null
                                and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                                and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <= Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                    );";
                 CommonDAL.ExecSQL(tran, string.Format(sql, DateTime.Now.Date));

                 //4、删除专业表数据
                 sql = @"delete from COC_TOW_Register_Profession where PRO_ServerID in
                    (
                        select c.PRO_ServerID 
                        from dbo.COC_TOW_Person_BaseInfo A 
                        inner join COC_TOW_Register_Profession c on A.PSN_ServerID=c.PSN_ServerID
                        WHERE c.PRO_ValidityEnd<='{0}' 
                              and a.PSN_ServerID in(
                                    select A.PSN_ServerID
                                    from dbo.View_JZS_TOW_WithProfession A 
                                    left join [dbo].[View_JZS_TOW_Applying] B 
                                        on a.PSN_ServerID=b.PSN_ServerID 
                                    left join [jcsjk_RY_JZS_ZSSD] C 
                                        on a.PSN_RegisterNO=c.ZCH 
                                    left join [dbo].[LockJZS] L
                                        on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                                    where 1=1 and b.ApplyStatus is null
                                        and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                                        and exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <= Convert(varchar(10),dateadd(day,-1,getdate()),21) and PSN_ServerID =a.PSN_ServerID)
                            )
                    );";
                 CommonDAL.ExecSQL(tran, string.Format(sql, DateTime.Now.Date));

                 tran.Commit();
                 WriteOperateLog("系统服务", 0, string.Format("批量注销专业过期二建注册建造师证书{0}本", dataCount), "");
             }
             catch (Exception ex)
             {
                 tran.Rollback();
                 FileLog.WriteLog("【系统服务LimitAgeOutCertificate】批量注销专业过期二建证书错误：" + ex.Message, ex);
                 return;
             }

             #endregion 二级建造师专业过期注销

            #region 二级造价工程师过期注销

             //统计代办证书数量
             sqlcount = @"select count(*) FROM [dbo].[zjs_Certificate]
                        where [PSN_RegisteType] < '07' 
                        and [PSN_CertificateNO] not IN (select [PSN_CertificateNO] from [LockZJS] where LockStatus='加锁' and LockEndTime > getdate()) 
                        and [PSN_RegisterNO] not IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )
                        and [PSN_CertificateValidity] <=  Convert(varchar(10),dateadd(day,-1,getdate()),21)";
             try
             {
                 dataCount = CommonDAL.SelectRowCount(sqlcount);
             }
             catch (Exception ex)
             {
                 FileLog.WriteLog("统计待注销的过期二级造价工程师证书错误：" + ex.Message, ex);
                 return;
             }

             //1、将二级人员信息更新至历史表
             sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                        ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                        select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                        FROM [dbo].[zjs_Certificate]
                        where [PSN_RegisteType] < '07' 
                            and [PSN_CertificateNO] not IN (select [PSN_CertificateNO] from [LockZJS] where LockStatus='加锁' and LockEndTime > getdate()) 
                            and [PSN_RegisterNO] not IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )
                            and [PSN_CertificateValidity] <=  Convert(varchar(10),dateadd(day,-1,getdate()),21)";

             db = new DBHelper();
             tran = db.BeginTransaction();
             try
             {
                 CommonDAL.ExecSQL(tran, sql);

                 //2、更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期
                 sql = @"Update [zjs_Certificate] 
                            set [PSN_RegisteType]='07',
                                PSN_RegistePermissionDate='{0}',
                                [XGR]='{1}',
                                [Memo]='{0}省级建设主管部门手动注销，注销原因：{2}' ,
                                XGSJ='{0}' 
                        Where [PSN_RegisteType] < '07' 
                            and [PSN_CertificateNO] not IN (select [PSN_CertificateNO] from [LockZJS] where LockStatus='加锁' and LockEndTime > getdate()) 
                            and [PSN_RegisterNO] not IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )
                            and [PSN_CertificateValidity] <=  Convert(varchar(10),dateadd(day,-1,getdate()),21)";
                 CommonDAL.ExecSQL(tran, string.Format(sql, DateTime.Now.Date, "系统自动服务", "注册有效期满且未延续注册"));
                 tran.Commit();
                 WriteOperateLog("系统服务", 0, string.Format("批量注销过期二级造价工程师证书{0}本", dataCount), "");
             }
             catch (Exception ex)
             {
                 tran.Rollback();
                 FileLog.WriteLog("【系统服务LimitAgeOutCertificate】批量注销过期二级造价工程师错误：" + ex.Message, ex);
                 return;
             }

            #endregion 二级造价工程师过期注销
        }

        /// <summary>
        /// 更新证书绑定一寸照片地址
        /// </summary>
        private void UpdateFacePhoto()
        {
            //本项目用于考务系统一寸免冠照片与证书绑定操作。
            //原理：使用windows计划任务定时调用本应用，按证书ID倒序（即创建时间倒序）扫描，更新一寸照片到证书表FacePhoto字段。
            //扫描规则：
            //1、按证书ID倒序（即创建时间倒序）扫描，取值FacePhoto is null；
            //2、先检查报名照片（身份 证号.jpg）是否存在，如果存在拷贝照片到考务网站“Exam\UpLoad\CertificatePhoto\岗位ID\证书编号后3位\证书编号.jpg”；
            //3、如果第2步无照片，扫描人员最新一寸照片目录（身份 证号.jpg），如果存在拷贝照片到考务网站“Exam\UpLoad\CertificatePhoto\岗位ID\证书编号后3位\证书编号.jpg”；
            //4、如果第3不也无照片，更新FacePhoto='',表示已经扫描过了，但此人尚未上传照片。

            //-------------------------------
            //app.config参数说明：
            //MaxCountExe， value="100"  表示一次扫描100本证书
            //ExamRoot， value="E:/Exam" 表示网站根目录物理位置

            try
            {
                //int span = Convert.ToInt32(DoTimeSpanSet["UpdateFacePhoto"]);
                //DateTime endTime = DateTime.Now.AddMinutes(span).AddSeconds(-20);

                //repeat:

                //一次执行更新最大行数
                int MaxCountExe = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxCountExe"]);

                // 查询人大金仓数据库，获取证书 
                string sb = String.Format(@"Select top {0} CERTIFICATEID,EXAMPLANID,POSTTYPEID,CERTIFICATECODE,WORKERCERTIFICATECODE 
                                            ,case when MODIFYTIME is null then CREATETIME else  MODIFYTIME end as MODIFYTIME
                                            from DBO.CERTIFICATE 
                                            Where VALIDENDDATE > dateadd(day,-1,getdate())  and STATUS <>'注销'  and STATUS <>'离京变更'
                                            and  FacePhoto is null 
                                            order by CERTIFICATEID desc ", MaxCountExe);
                DataTable dtOriginal = (new DBHelper()).GetFillData(sb.ToString());

                if (dtOriginal.Rows.Count == 0) return;
                FileLog.WriteLog(String.Format("读取{0}条数据，开始更新！", dtOriginal.Rows.Count));

                string _path = "";
                string _sourcePath = "";
                string code = "";
                string ExamPlanID = "";
                string WorkerCertificateCode = "";
                int finishCount = 0;
                foreach (DataRow dr in dtOriginal.Rows)
                {
                    ExamPlanID = dr["EXAMPLANID"].ToString();

                    if (dr["WorkerCertificateCode"] == null || dr["WorkerCertificateCode"] == DBNull.Value || dr["WorkerCertificateCode"].ToString().Length < 3)
                    {
                        CertificateDAL.ModifyCertificate(Convert.ToInt64(dr["CERTIFICATEID"]), "");
                        continue;
                    }
                    WorkerCertificateCode = dr["WorkerCertificateCode"].ToString();
                    code = dr["CERTIFICATECODE"].ToString();
                    code = code.Substring(code.Length - 3, 3);//图片按证号后3位分目录存储
                    _path = string.Format("{0}/UpLoad/CertificatePhoto/{1}/{2}/", ExamWebRoot, dr["POSTTYPEID"], code);
                    if (!Directory.Exists(_path))
                    {
                        System.IO.Directory.CreateDirectory(_path);
                    }
                    _path = string.Format("{0}{1}.jpg", _path, dr["CERTIFICATECODE"].ToString());
                    if (ExamPlanID != "-100" && ExamPlanID != "-200" && ExamPlanID != "-300")//通过报名考试系统发证,拷贝报名照片                
                    {
                        _sourcePath = string.Format("{0}/UpLoad/SignUpPhoto/{1}/{2}.jpg", ExamWebRoot, ExamPlanID, WorkerCertificateCode);
                        if (File.Exists(_sourcePath) == true)
                        {
                            File.Copy(_sourcePath, _path, true);
                            CertificateDAL.ModifyCertificate(Convert.ToInt64(dr["CERTIFICATEID"]), _path.Replace(ExamWebRoot, "~"));
                            FileLog.WriteLog(String.Format("证书{0}证件照片已同步", dr["CERTIFICATECODE"].ToString()));
                            finishCount++;
                            continue;
                        }
                    }

                    _sourcePath = string.Format("{0}/UpLoad/WorkerPhoto/{1}/{2}.jpg", ExamWebRoot, WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
                    if (File.Exists(_sourcePath) == true)
                    {
                        File.Copy(_sourcePath, _path, true);
                        CertificateDAL.ModifyCertificate(Convert.ToInt64(dr["CERTIFICATEID"]), _path.Replace(ExamWebRoot, "~"));
                        FileLog.WriteLog(String.Format("证书{0}证件照片已同步", dr["CERTIFICATECODE"].ToString()));
                        finishCount++;
                        continue;
                    }
                    else if (Convert.ToDateTime(dr["MODIFYTIME"]).AddDays(1) < DateTime.Now)
                    {
                        CertificateDAL.ModifyCertificate(Convert.ToInt64(dr["CERTIFICATEID"]), "");
                    }
                }


                FileLog.WriteLog(String.Format("同步完成，共计{0}条数据。", finishCount));
                //if(finishCount<20 && endTime > DateTime.Now)
                //{
                //    goto repeat;
                //}
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("UpdateFacePhoto()执行失败。", ex);
            }
        }

        /// <summary>
        /// 上机考试照片打包下载
        /// </summary>
        private void ExamPhotoRar()
        {
            //var path = Application.StartupPath + @"\test.bat";

            //获取目录下指定类型的文件集合
            string path = string.Format(@"{0}\Upload\ExamFacePhoto", ZYRYJGWebRoot);

            //第一种方法
            var files = Directory.GetFiles(path, "*.txt");

            string ExamPlanID = "";
            foreach (var f in files)
            {
                try
                {
                    ExamPlanID = System.IO.Path.GetFileNameWithoutExtension(f);
                    if (File.Exists(f) == true)
                    {
                        File.Delete(f);

                        CreateBat(ExamPlanID);
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("创建上机考试人员免冠照片下载包失败，考试计划ID：{0}。", ExamPlanID), ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// 考试照片打包
        /// </summary>
        /// <param name="examplanid">考试计划ID</param>
        private void CreateBat(string examplanid)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT [CERTIFICATECODE],[EXAMCARDID] FROM [dbo].[VIEW_EXAMRESULT] where [examplanid]={0}", examplanid));

            if (dt.Rows.Count > 0)
            {
                ExamPlanOB o = ExamPlanDAL.GetObject(Convert.ToInt64(examplanid));
                //创建待压缩目录
                if (!Directory.Exists(string.Format(@"{3}\UpLoad\ExamFacePhoto\{0}{1}_{2}\", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, ZYRYJGWebRoot)))
                {
                    System.IO.Directory.CreateDirectory(string.Format(@"{3}\UpLoad\ExamFacePhoto\{0}{1}_{2}\", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, ZYRYJGWebRoot));
                }

                //复制待处理一寸照片到压缩目录
                string[] url = new string[dt.Rows.Count + 1];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    url[i] = string.Format("copy {0} {1}"
                        , string.Format(@"{2}\UpLoad\SignUpPhoto\{0}\{1}.jpg", examplanid, dt.Rows[i]["CERTIFICATECODE"], ZYRYJGWebRoot)
                        , string.Format(@"{4}\UpLoad\ExamFacePhoto\{0}{1}_{2}\{3}.jpg", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, dt.Rows[i]["EXAMCARDID"], ZYRYJGWebRoot)
                        );
                }
                url[dt.Rows.Count] = string.Format("\"c:\\Program Files\\WinRAR\\WinRAR.exe\" A -EP {0} {1}"
                      , string.Format(@"{3}\UpLoad\ExamFacePhoto\{0}{1}_{2}.rar", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, ZYRYJGWebRoot)
                    , string.Format(@"{3}\UpLoad\ExamFacePhoto\{0}{1}_{2}", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, ZYRYJGWebRoot)
                    );

                //创建压缩命令文件
                string fileUrl = string.Format(@"{3}\{0}{1}_{2}.bat", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, CAFile);
                File.WriteAllLines(fileUrl, url, System.Text.Encoding.Default);

                var p = new Process();
                p.StartInfo.WorkingDirectory = CAFile;
                p.StartInfo.FileName = fileUrl;
                p.Start();
                p.WaitForExit();
                var a = p.ExitCode;
                if (a == 0)
                {
                    File.Delete(fileUrl);
                    DirectoryInfo dir = new DirectoryInfo(string.Format(@"{3}\UpLoad\ExamFacePhoto\{0}{1}_{2}\", o.PostName, o.ExamStartDate.Value.ToString("yyyyMMdd"), examplanid, ZYRYJGWebRoot));
                    dir.Delete(true);
                }
            }
        }

        /// <summary>
        /// 考务业务申请与办理统计物理化（注意：统计时间段拉长一点，否则可能漏掉统计。如：考试申请一个月后才放成绩）
        /// </summary>
        public void TJ_WLH_YEWU()
        {
            try
            {
                # region sql

                string sql = @"DELETE FROM dbo.TJ_YEWU WHERE TJ_DATE BETWEEN '{0}' AND '{1}';

INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT CONVERT(varchar(10), Applydate, 20) TJ_DATE,posttypename,'京内变更' as funType,'京内变更申请个数' as countType,count(*) ItemCount
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  posttypename,'京内变更' as funType,'京内变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), noticedate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'京内变更' as funType,'京内变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'离京变更' as funType,'离京变更申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  posttypename,'离京变更' as funType,'离京变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), noticedate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'离京变更' as funType,'离京变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'补办' as funType,'补办申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  posttypename,'补办' as funType,'补办通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), noticedate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'补办' as funType,'补办申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'注销' as funType,'注销申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  posttypename,'注销' as funType,'注销通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), noticedate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'注销' as funType,'注销申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'进京变更' as funType,'进京变更申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), confrimdate, 20) TJ_DATE,  posttypename,'进京变更' as funType,'进京变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where confrimdate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), confrimdate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'进京变更' as funType,'进京变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where Applydate between '{0}' and '{1}' and confrimdate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'续期' as funType,'续期申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where Applydate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), confirmdate, 20) TJ_DATE,  posttypename,'续期' as funType,'续期通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where confirmdate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), confirmdate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  posttypename,'续期' as funType,'续期申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where Applydate between '{0}' and '{1}' and confirmdate between '{0}' and '{1}' and posttypeid >1
group by CONVERT(varchar(10), Applydate, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), EXAMSTARTDATE, 20) TJ_DATE,  posttypename,'考试' as funType,'考试申请个数' as countType,count(*)
FROM DBO.VIEW_EXAMRESULT
where examplanid in(
SELECT distinct EXAMPLANID
FROM DBO.VIEW_EXAMSCORE
where EXAMSTARTDATE BETWEEN '{0}' AND '{1}'
) and posttypeid >1
group by CONVERT(varchar(10), EXAMSTARTDATE, 20),posttypename;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), EXAMSTARTDATE, 20) TJ_DATE,  posttypename,'考试' as funType,'考试通过个数' as countType,count(*)
FROM DBO.VIEW_EXAMRESULT
where examplanid in(
SELECT distinct EXAMPLANID
FROM DBO.VIEW_EXAMSCORE
where EXAMRESULT='合格' and STATUS='成绩已公告' and EXAMSTARTDATE BETWEEN '{0}' AND '{1}'
) and examresult='合格' and posttypeid >1
group by CONVERT(varchar(10), EXAMSTARTDATE, 20),posttypename;

INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), p.EXAMSTARTDATE, 20) TJ_DATE,p.posttypename,'考试' as funType,'缺考人数' as countType,count(*)
FROM [dbo].[EXAMPLAN] p
inner join [dbo].[EXAMRESULT] r on  p.EXAMPLANID = r.EXAMPLANID
where p.EXAMSTARTDATE BETWEEN '{0}' AND '{1}' 
and p.posttypeid >1 and (r.[SUMSCOREDETAIL] like '%：0.0%' or r.[SUMSCOREDETAIL] like '%缺考%') and r.EXAMRESULT='不合格'
group by CONVERT(varchar(10), p.EXAMSTARTDATE, 20),p.posttypename;

INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'京内变更' as funType,'京内变更申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125)
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  postname as posttypename,'京内变更' as funType,'京内变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), noticedate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'京内变更' as funType,'京内变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'离京变更' as funType,'离京变更申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  postname as posttypename,'离京变更' as funType,'离京变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), noticedate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'离京变更' as funType,'离京变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'补办' as funType,'补办申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  postname as posttypename,'补办' as funType,'补办通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), noticedate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'补办' as funType,'补办申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'注销' as funType,'注销申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), noticedate, 20) TJ_DATE,  postname as posttypename,'注销' as funType,'注销通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), noticedate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'注销' as funType,'注销申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECHANGE
where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'进京变更' as funType,'进京变更申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), confrimdate, 20) TJ_DATE,  postname as posttypename,'进京变更' as funType,'进京变更通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where confrimdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), confrimdate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'进京变更' as funType,'进京变更申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where Applydate between '{0}' and '{1}' and confrimdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'续期' as funType,'续期申请个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), confirmdate, 20) TJ_DATE,  postname as posttypename,'续期' as funType,'续期通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where confirmdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), confirmdate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), Applydate, 20) TJ_DATE,  postname as posttypename,'续期' as funType,'续期申请并通过个数' as countType,count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where Applydate between '{0}' and '{1}' and confirmdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), Applydate, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), EXAMSTARTDATE, 20) TJ_DATE,  postname as posttypename,'考试' as funType,'考试申请个数' as countType,count(*)
FROM DBO.VIEW_EXAMSCORE
where EXAMSTARTDATE BETWEEN '{0}' AND '{1}' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), EXAMSTARTDATE, 20),postname;
INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), EXAMSTARTDATE, 20) TJ_DATE,  postname as posttypename,'考试' as funType,'考试通过个数' as countType,count(*)
FROM DBO.VIEW_EXAMRESULT
where examplanid in(
SELECT distinct EXAMPLANID
FROM DBO.VIEW_EXAMSCORE
where EXAMRESULT='合格' and STATUS='成绩已公告' and EXAMSTARTDATE BETWEEN '{0}' AND '{1}'
) and examresult='合格' and (postid=6 or postid=147 or postid=148 or postid=1123 or postid=1125 )
group by CONVERT(varchar(10), EXAMSTARTDATE, 20),postname;

INSERT INTO DBO.TJ_YEWU(TJ_DATE,POSTTYPENAME,FUNTYPE,COUNTTYPE,ITEMCOUNT )
SELECT  CONVERT(varchar(10), EXAMSTARTDATE, 20) TJ_DATE,  postname as posttypename,'考试' as funType,'缺考人数' as countType,count(*)
FROM [dbo].[EXAMPLAN] p
inner join [dbo].[EXAMRESULT] r on  p.EXAMPLANID = r.EXAMPLANID
where p.EXAMSTARTDATE BETWEEN '{0}' AND '{1}' 
and (p.postid=6 or p.postid=147 or p.postid=148 or p.postid=1123 or p.postid=1125 ) and (r.[SUMSCOREDETAIL] like '%：0.0%' or r.[SUMSCOREDETAIL] like '%缺考%') and r.EXAMRESULT='不合格'
group by CONVERT(varchar(10), p.EXAMSTARTDATE, 20),p.postname;
";

                # endregion

                CommonDAL.ExecSQL(string.Format(sql, DateTime.Now.AddDays(-120).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")));

            }
            catch (Exception ex)
            {
                FileLog.WriteLog("业务申请与办理统计物理化失败，错误信息：" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 删除无效从业人员业务申请
        /// </summary>
        public void DeleteTimeOutApply()
        {
            int r0 = this.DeleteExpiredChangeApply();//一个月未办结的变更申请
            int r1 = this.DeleteExpiredRenewApply();//证书过期三个月未办结的续期申请
            int r2 = this.DeleteExpiredEnterApply();//一个月未办结的进京申请

            WriteOperateLog("系统服务", 0, string.Format("删除过期未办结的申请{0}条", Convert.ToString(r0 + r1 + r2))
               , string.Format("一个月未办结的变更申请{0}条；证书过期三个月未办结的续期申请{1}条；一个月未办结的进京申请{2}条。"
               , r0, r1, r2));

            ////2021-07-02日应注册中心要求去掉删除报名信息，一直保留待查。
            //int r3 = this.DeleteExpiredExamSignup();
            //WriteOperateLog("系统服务", 0, string.Format("删除过期未办结的申请{0}条", Convert.ToString(r0 + r1 + r2 + r3))
            //    , string.Format("一个月未办结的变更申请{0}条；证书过期三个月未办结的续期申请{1}条；一个月未办结的进京申请{2}条。考试后未审核通过的考试报名申请表{3}条。"
            //    , r0, r1, r2, r3));
        }

        /// <summary>
        /// 统计数据 物理化
        /// </summary>
        public void TJ_WLH()
        {
            //考务业务申请与办理统计物理化
            TJ_WLH_YEWU();

        }

        #endregion 考务业务

        #region 内部方法

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
        //        index = Char36.IndexOf(c.ToString());
        //        p = p + index;
        //        p = (p % 36);
        //        if (p == 0) p = 36;
        //        p = p * 2;
        //        p = p % 37;
        //    }
        //    p = 37 - p;
        //    return Char36[p];

        //    //List<string> Char36 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        //    //// "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //    //char[] list = ZZBS.ToCharArray();
        //    //int p = 36;
        //    //int index = 0;
        //    //foreach (char c in list)
        //    //{
        //    //    index = Char36.IndexOf(c.ToString());
        //    //    p = p + index;
        //    //    p = (p % 36);
        //    //    if (p == 0) p = 36;
        //    //    p = p * 2;
        //    //    p = p % 37;
        //    //}
        //    //p = 37 - p;
        //    //return Char36[p - 1];
        //}

        /// <summary>
        /// 写入电子证书创建、签发失败记录
        /// </summary>
        /// <param name="CertNo">证书编号</param>
        /// <param name="CertType">证书类别</param>
        /// <param name="StepName">签发环节名称（创建、发送、废止、签发、下载、取回）</param>
        /// <param name="ErrorMessage">错误描述</param>
        private void WriteEleCertError(string CertNo, string CertType, string StepName, string ErrorMessage)
        {

            string sql = @"if exists(select 1 from [dbo].[EleCertError] where [CertNo] ='{0}')
	                            UPDATE [dbo].[EleCertError]
	                               SET [DoTime] = getdate(),[StepName] ='{2}',[ErrorCount] = [ErrorCount] +1,[ErrorMessage] = '{3}'
	                             WHERE [CertNo] ='{0}'
                            else
	                            INSERT INTO [dbo].[EleCertError]([CertNo],[CertType],[DoTime],[StepName],[ErrorCount],[ErrorMessage])
                                 VALUES('{0}','{1}',getdate(),'{2}',1,'{3}')";
            try
            {
                DataAccess.CommonDAL.ExecSQL(string.Format(sql, CertNo, CertType, StepName, ErrorMessage.Replace("'", "’")));
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("写入电子证书创建、签发失败记录失败，错误信息：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 将文件转化为二进制数组
        /// </summary>
        /// <param name="fileName">文件地址</param>
        /// <returns>二进制数组</returns>
        private byte[] FileToByte(string fileName)
        {
            byte[] bBuffer;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryReader binReader = new BinaryReader(fs);

                bBuffer = new byte[fs.Length];
                binReader.Read(bBuffer, 0, (int)fs.Length);
                binReader.Close();
                fs.Close();
                return bBuffer;
            }
        }

        //自动将15位身份证升位18位
        private string ConvertCardID(string CID)
        {
            if (CID.Length == 15 && Utility.Check.isChinaIDCard(CID))
            {
                return Utility.Check.ConvertoIDCard15To18(CID);
            }
            else
                return CID;
        }


        private void Txt_INSI_To_UTF8(string sourceFileUrl, string targetFilePath)
        {
            //复制到本地
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                File.WriteAllText(targetFilePath, client.DownloadString(sourceFileUrl), System.Text.Encoding.UTF8);//转化编码
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sourceFileUrl"></param>
        /// <param name="targetFilePath"></param>
        private void DownLoadFile(string sourceFileUrl, string targetFilePath)
        {
            //复制到本地
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.DownloadFile(sourceFileUrl, targetFilePath);//转化编码
            }
        }

        private string NullConvert(object data)
        {
            if (data == null)
                return "null";
            else
                return data.ToString();
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="personName">操作者全称</param>
        /// <param name="personId">操作者ID</param>
        /// <param name="operateName">操作业务名称</param>
        /// <param name="logDetail">操作详细varchar6000</param>
        private void WriteOperateLog(string personName, long personId, string operateName, string logDetail)
        {
            OperateLogOB o = new OperateLogOB();
            o.PersonName = personName;
            o.PersonID = personId.ToString();
            o.LogTime = DateTime.Now;
            o.OperateName = operateName;
            o.LogDetail = logDetail;
            try
            {
                OperateLogDAL.Insert(o);
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("记录操作日志失败！，错误信息：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 读取pdf模版中的标签
        /// </summary>
        /// <param name="pdfTemplate">模版文件路径</param>
        /// <returns></returns>
        private Dictionary<string, string> ReadForm(string pdfTemplate)
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
            catch (Exception ex)
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
        /// 向执业人员pdf模版填充内容，并生成新的文件
        /// 
        /// 模版路径
        /// 生成文件保存路径
        /// 标签字典(即模版中需要填充的控件列表)
        private void FillForm(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            dic["PostName"] = dr["PostName"].ToString();
            dic["WorkerName"] = dr["WorkerName"].ToString();
            dic["WorkerCertificateCode"] = dr["WorkerCertificateCode"].ToString();         
            dic["CertificateCode"] = dr["CertificateCode"].ToString();          
            dic["ConferDate"] = Convert.ToDateTime(dr["ConferDate"]).ToString("yyyy年M月d日");

            if (dr["POSTTYPEID"].ToString() == "4000")
            {
                dic["Sex"] = dr["Sex"].ToString();
                dic["TrainUnit"] = (dr["TrainUnitName"].ToString().Length <= 12 ? "\r\n" : "") + dr["TrainUnitName"].ToString();//培训点
                dic["SkillLevel"] = FormatSkillLevel(dr["SkillLevel"].ToString());//等级
            }
            else
            {
                dic["UnitName"] = dr.IsNull("UnitName") == true ? "" : dr["UnitName"].ToString();
                dic["ValidEndDate"] = Convert.ToDateTime(dr["ValidEndDate"]).ToString("yyyy年M月d日");
                dic["SkillLevel"] = dr.IsNull("SkillLevel") == true ? "" : dr["SkillLevel"].ToString();//等级
                dic["CreateDate"] = DateTime.Now.ToString("yyyy年M月d日");//制证日期（签章日期）
            }

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //设置支持中文字体          
                string iTextAsianCmaps_Path = string.Format(@"{0}\bin\iTextAsianCmaps.dll", ExamWebRoot);
                string iTextAsian_Path = string.Format(@"{0}\bin\iTextAsian.dll", ExamWebRoot);
                BaseFont.AddToResourceSearch(iTextAsianCmaps_Path);
                BaseFont.AddToResourceSearch(iTextAsian_Path);

                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                pdfFormFields.AddSubstitutionFont(baseFont);
                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }

                PdfContentByte waterMarkContent;
                waterMarkContent = pdfStamper.GetOverContent(1);//内容下层加水印

                //一寸照片
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetFaceImagePath(dr["FacePhoto"].ToString(), dr["WorkerCertificateCode"].ToString()));
                image.GrayFill = 100;//透明度，灰色填充
                image.ScaleAbsolute(110, 140);
                if (dr["POSTTYPEID"].ToString() == "4000")
                {                   
                    image.SetAbsolutePosition(250, 485);
                }
                else
                {
                    image.SetAbsolutePosition(410, 465);
                }
                waterMarkContent.AddImage(image);//加水印


                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["PostID"], dr["CertificateCode"]));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/exam/PersonnelFile/CertCheck.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
                imgCode.ScaleAbsolute(100, 100);
                if (dr["POSTTYPEID"].ToString() == "4000")
                {                   
                    imgCode.SetAbsolutePosition(90, 75);
                }
                else
                {
                    imgCode.SetAbsolutePosition(110, 170);
                }
                waterMarkContent.AddImage(imgCode);//加水印

                pdfStamper.FormFlattening = true;

                //-------------------------------------------
            }
            catch (Exception ex)
            {
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

        private string formatZY(string zy)
        {
            //格式化专业
            zy = zy.Replace("建筑", "建筑工程").Replace("公路", "公路工程").Replace("水利", "水利水电工程").Replace("市政", "市政公用工程").Replace("矿业", "矿业工程").Replace("机电", "机电工程");

            //格式化有效期
            string[] str = zy.Split('（');

            return string.Format("{0}（{1}）"
                , str[0]
                , Convert.ToDateTime(str[1].Replace("）", "")).ToString("yyyy年M月d日")
                );
        }

        private string formatZY_Use(string zy,DateTime ValidStartDate)
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

        /// 
        /// 向二级建造师pdf模版填充内容，并生成新的文件
        /// 
        /// 模版路径
        /// 生成文件保存路径
        /// 标签字典(即模版中需要填充的控件列表)
        private void FillFormOfJZS(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            dic["PSN_Name"] = dr["PSN_Name"].ToString();//姓名
            dic["PSN_Sex"] = dr["PSN_Sex"].ToString();//性别
            dic["PSN_BirthDate"] = Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年M月d日");//生日
            dic["ENT_Name"] = dr["ENT_Name"].ToString();//单位
            dic["PSN_RegisterNO"] = dr["PSN_RegisterNO"].ToString();//注册号
            dic["PSN_CertificationDate"] = Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy年M月d日");//发证日期
            dic["PSN_RegistePermissionDate"] = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日"); //制证日期（签章日期）

            dic["FromTo"] = string.Format("{0}-{1}", Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy年M月d日"), Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy年M月d日"));//使用有效期

            //格式化专业
            //注册专业、有效期
            string[] zy = dr["ProfessionWithValid"].ToString().Trim(' ').Split(' ');//注册专业、有效期
            StringBuilder sb = new StringBuilder();
            foreach (string s in zy)
            {
                sb.Append(formatZY(s)).Append("\n\n");
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
                string iTextAsianCmaps_Path = string.Format(@"{0}\bin\iTextAsianCmaps.dll", ExamWebRoot);
                string iTextAsian_Path = string.Format(@"{0}\bin\iTextAsian.dll", ExamWebRoot);
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
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
                imgCode.ScaleAbsolute(71, 71);
                imgCode.SetAbsolutePosition(60, 100);
                waterMarkContent.AddImage(imgCode);//加水印

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
                string iTextAsianCmaps_Path = string.Format(@"{0}\bin\iTextAsianCmaps.dll", ExamWebRoot);
                string iTextAsian_Path = string.Format(@"{0}\bin\iTextAsian.dll", ExamWebRoot);
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
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
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

        /// <summary>
        /// 获取证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
        /// </summary>
        /// <param name="FacePhoto">照片路径</param>
        /// <param name="WorkerCertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFaceImagePath(string FacePhoto, string WorkerCertificateCode)
        {
            string imgPath = "";
            if (string.IsNullOrEmpty(FacePhoto) == false)
            {
                if (File.Exists(FacePhoto.Replace("~", ExamWebRoot)) == true)
                {
                    imgPath = FacePhoto.Replace("~", ExamWebRoot);
                }
            }
            if (imgPath == "")
            {
                if (WorkerCertificateCode.IndexOf('?') == -1)
                {
                    string path = string.Format("{2}/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode, ExamWebRoot);
                    if (File.Exists(path) == true)
                    {
                        imgPath = path;
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = string.Format("{0}/Images/tup.gif", ExamWebRoot);
            }

            return imgPath;
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
                if (File.Exists(FacePhoto.Replace("~", ExamWebRoot)) == true)
                {
                    imgPath = FacePhoto.Replace("~", ExamWebRoot);
                }
            }
            if (imgPath == "")
            {
                if (PSN_CertificateNO.IndexOf('?') == -1)
                {
                    string path = string.Format("{2}/UpLoad/WorkerPhoto/{0}/{1}.jpg", PSN_CertificateNO.Substring(PSN_CertificateNO.Length - 3, 3), PSN_CertificateNO, ExamWebRoot);
                    if (File.Exists(path) == true)
                    {
                        imgPath = path;
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = string.Format("{0}/Images/tup.gif", ExamWebRoot);
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
                if (File.Exists(signPhoto.Replace("~", ExamWebRoot)) == true)
                {
                    imgPath = signPhoto.Replace("~", ExamWebRoot);
                }
            }
            if (imgPath == "")
            {
                if (PSN_CertificateNO.IndexOf('?') == -1)
                {
                    WorkerOB ob = WorkerDAL.GetUserObject(PSN_CertificateNO);
                    if (ob.SignPhotoTime.HasValue == true)
                    {
                        string path = string.Format("{2}/UpLoad/SignImg/{0}/{1}.jpg", PSN_CertificateNO.Substring(PSN_CertificateNO.Length - 3, 3), PSN_CertificateNO, ExamWebRoot);
                        if (File.Exists(path) == true)
                        {
                            imgPath = path;
                        }
                    }
                }
            }
            if (imgPath == "")
            {
                imgPath = string.Format("{0}/Images/SignNull.jpg", ExamWebRoot);
            }

            return imgPath;
        }

        /// <summary>
        /// 获取用户操作日志集合
        /// </summary>
        /// <remarks>每天将192.168.5.185/d:/log/visitlog/fileshare/目录下日志 记录保存到人员数据库的一张表InterFaceLog中</remarks>
        /// <param name="dt">时间 格式为【xxxx-xx-xx】</param>
        /// <returns></returns>
        private List<Object> GetUserOperateLogDataList(string dt)
        {
            var list = new List<Object>();
            string path = string.Format("\\\\192.168.5.185\\d$\\log\\VisitLog\\FileShare\\{0}.log", dt);
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path, Encoding.Default))
                {
                    string str = sr.ReadLine();
                    while (!string.IsNullOrEmpty(str))
                    {
                        string[] strArray = str.Split(new[] { " " }, StringSplitOptions.None);
                        list.Add(new InterFaceLogOB
                        {
                            AccessDate = strArray[0],
                            AccessTime = strArray[1],
                            AccessUser = strArray[2],
                            ServerId = strArray[3],
                            CallingMethodName = strArray[4],
                            ParameterData = strArray[5],
                            MethodDescription = strArray[6]
                        });
                        str = sr.ReadLine();
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("访问的{0}文件不存在：", path));
            }
            return list;
        }

        /// <summary>
        /// 获取InterFaceLog中 最近插入日期
        /// </summary>
        /// <returns></returns>
        private string GetInterFaceLogNewDate()
        {
            const string cmdtxt = @"SELECT distinct top 1 ACCESSDATE
                                                    FROM DBO.INTERFACELOG
                                                    order by ACCESSDATE desc";

            var dt = new DBHelper().GetFillData(null, cmdtxt);
            if ((dt != null && dt.Rows.Count > 0))
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        //删除变更申请一个月后不审核的申请
        private int DeleteExpiredChangeApply()
        {
            try
            {
                int dataCount = CommonDAL.SelectRowCount(@"DBO.CERTIFICATECHANGE", @" and dateadd(month,1,ApplyDate) < getdate() and [STATUS] <> '已告知'");

                //备份删除
                string HISsql = @"INSERT INTO DBO.CERTIFICATECHANGE_DEL (CERTIFICATECHANGEID,CERTIFICATEID,CHANGETYPE,WORKERNAME,SEX,BIRTHDAY,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,DEALWAY,OLDUNITADVISE,NEWUNITADVISE,OLDCONFERUNITADVISE,NEWCONFERUNITADVISE,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,NOTICEDATE,NOTICERESULT,NOTICEMAN,NOTICECODE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITNAME,NEWUNITNAME,UNITCODE,NEWUNITCODE,WORKERCERTIFICATECODE,LINKWAY,NEWWORKERCERTIFICATECODE,NEWWORKERNAME,NEWSEX,NEWBIRTHDAY,SHEBAOCHECK,DELTIME )
                                    select CERTIFICATECHANGEID,CERTIFICATEID,CHANGETYPE,WORKERNAME,SEX,BIRTHDAY,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,DEALWAY,OLDUNITADVISE,NEWUNITADVISE,OLDCONFERUNITADVISE,NEWCONFERUNITADVISE,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,NOTICEDATE,NOTICERESULT,NOTICEMAN,NOTICECODE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITNAME,NEWUNITNAME,UNITCODE,NEWUNITCODE,WORKERCERTIFICATECODE,LINKWAY,NEWWORKERCERTIFICATECODE,NEWWORKERNAME,NEWSEX,NEWBIRTHDAY,SHEBAOCHECK,getdate() FROM DBO.CERTIFICATECHANGE where dateadd(month,1,ApplyDate) < getdate() and [STATUS] <> '已告知'";
                CommonDAL.ExecSQL(HISsql);

                //物理删除
                string delsql = @"DELETE FROM DBO.CERTIFICATECHANGE where dateadd(month,1,ApplyDate) < getdate() and [STATUS] <> '已告知'";
                CommonDAL.ExecSQL(delsql);
                return dataCount;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("系统服务，删除变更申请一个月后不审核的申请失败。错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //删除续期申请后证书过期三个月后没有初审的申请
        private int DeleteExpiredRenewApply()
        {
            try
            {
                int dataCount = CommonDAL.SelectRowCount("dbo.certificatecontinue t",
                                    @" and (t.[STATUS] = '填报中' or t.[STATUS] = '待单位确认' or t.[STATUS] = '已申请' or t.[STATUS] ='退回修改' ) 
                                        and exists (
	                                        select 1 from dbo.certificate c where  c.certificateid = t.certificateid 
	                                        and c.POSTTYPEID <3 and dateadd(month,3,c.validenddate) < getdate()
                                        )");
                //备份删除
                string HISsql = @"INSERT INTO DBO.CERTIFICATECONTINUE_DEL (CERTIFICATECONTINUEID,CERTIFICATEID,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFIRMDATE,CONFIRMRESULT,CONFIRMMAN,CONFIRMCODE,VALIDSTARTDATE,VALIDENDDATE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITCODE,PHONE,FIRSTCHECKUNITID,NEWUNITNAME,NEWUNITCODE,SHEBAOCHECK,DELTIME )
                                                 select CERTIFICATECONTINUEID,CERTIFICATEID,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFIRMDATE,CONFIRMRESULT,CONFIRMMAN,CONFIRMCODE,VALIDSTARTDATE,VALIDENDDATE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITCODE,PHONE,FIRSTCHECKUNITID,NEWUNITNAME,NEWUNITCODE,SHEBAOCHECK,getdate()
                                                from dbo.certificatecontinue t 
                                                where (t.[STATUS] = '填报中' or t.[STATUS] = '待单位确认' or t.[STATUS] = '已申请' or t.[STATUS] ='退回修改' ) 
                                                    and exists (
	                                                    select 1 from dbo.certificate c where  c.certificateid = t.certificateid 
	                                                    and c.POSTTYPEID <3 and dateadd(month,3,c.validenddate) < getdate()
                                                    )";
                CommonDAL.ExecSQL(HISsql);

                //物理删除
                string delsql = @"delete from dbo.certificatecontinue 
                                                 where (certificatecontinue.[STATUS] = '填报中' or certificatecontinue.[STATUS] = '待单位确认' or certificatecontinue.[STATUS] = '已申请' or certificatecontinue.[STATUS] ='退回修改') 
                                                and exists (
                                                    select 1 from dbo.certificate c where  c.certificateid = certificatecontinue.certificateid 
													and c.POSTTYPEID <3 and dateadd(month,3,c.validenddate) < getdate()
                                                )";
                CommonDAL.ExecSQL(delsql);
                return dataCount;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("系统服务，删除续期申请后“证书过期三个月”后没有初审的申请失败。错误信息：" + ex.Message, ex);
                return 0;
            }



            //                        try
            //                        {
            //                            //删除规则： 截至来年2月底可申请，3月底未办结删除（2020-12-31日修改，临时方案，明年还得改）
            //                            int dataCount = CommonDAL.SelectRowCount("dbo.certificatecontinue t",
            //                                                @" and (t.[STATUS] = '填报中' or t.[STATUS] = '待单位确认' or t.[STATUS] = '已申请' or t.GetResult ='退回修改' or t.GetResult ='不予受理') 
            //                                        and exists (
            //                                        select 1 from dbo.certificate c where  c.certificateid = t.certificateid 
            //                                        and (
            //	                                        (c.POSTTYPEID=1 and  dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate())
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =6 and dateadd(day,2,dateadd(month,9,c.validenddate)) < getdate() )
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =12 and dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate() )
            //	                                        )	
            //                                        )");
            //                            //备份删除
            //                            string HISsql = @"INSERT INTO DBO.CERTIFICATECONTINUE_DEL (CERTIFICATECONTINUEID,CERTIFICATEID,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFIRMDATE,CONFIRMRESULT,CONFIRMMAN,CONFIRMCODE,VALIDSTARTDATE,VALIDENDDATE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITCODE,PHONE,FIRSTCHECKUNITID,NEWUNITNAME,NEWUNITCODE,SHEBAOCHECK,DELTIME )
            //                                     select CERTIFICATECONTINUEID,CERTIFICATEID,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFIRMDATE,CONFIRMRESULT,CONFIRMMAN,CONFIRMCODE,VALIDSTARTDATE,VALIDENDDATE,[STATUS],CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITCODE,PHONE,FIRSTCHECKUNITID,NEWUNITNAME,NEWUNITCODE,SHEBAOCHECK,getdate()
            //                                    from dbo.certificatecontinue t 
            //                                    where (t.[STATUS] = '填报中' or t.[STATUS] = '待单位确认' or t.[STATUS] = '已申请' or t.GetResult ='退回修改' or t.GetResult ='不予受理') 
            //                                    and exists (
            //                                    select 1 from dbo.certificate c where  c.certificateid = t.certificateid 
            //                                    and (
            //	                                     (c.POSTTYPEID=1 and  dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate())
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =6 and dateadd(day,2,dateadd(month,9,c.validenddate)) < getdate() )
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =12 and dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate() )
            //	                                        )	
            //                                    )";
            //                            CommonDAL.ExecSQL(HISsql);

            //                            //物理删除
            //                            string delsql = @"delete from dbo.certificatecontinue 
            //                                     where (certificatecontinue.[STATUS] = '填报中' or certificatecontinue.[STATUS] = '待单位确认' or certificatecontinue.[STATUS] = '已申请' or certificatecontinue.GetResult ='退回修改' or certificatecontinue.GetResult ='不予受理') 
            //                                    and exists (
            //                                    select 1 from dbo.certificate c where  c.certificateid = certificatecontinue.certificateid 
            //                                    and (
            //	                                     (c.POSTTYPEID=1 and  dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate())
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =6 and dateadd(day,2,dateadd(month,9,c.validenddate)) < getdate() )
            //	                                        or (c.POSTTYPEID=2 and month(c.validenddate) =12 and dateadd(day,1,dateadd(month,3,c.validenddate)) < getdate() )
            //	                                        )	
            //                                    )";
            //                            CommonDAL.ExecSQL(delsql);
            //                            return dataCount;
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            FileLog.WriteLog("系统服务，删除续期申请后“证书过期三个月”后没有初审的申请失败。错误信息：" + ex.Message, ex);
            //                            return 0;
            //                        }
        }

        //删除进京申请一个月后不审核的申请
        private int DeleteExpiredEnterApply()
        {
            try
            {


                int dataCount = CommonDAL.SelectRowCount("DBO.CERTIFICATEENTERAPPLY", " and dateadd(month,1,ApplyDate) < getdate() and (APPLYSTATUS='待单位确认' or APPLYSTATUS='退回修改' or APPLYSTATUS='已申请')");


                //备份删除
                string HISsql = @"INSERT INTO DBO.CERTIFICATEENTERAPPLY_DEL (APPLYID,WORKERID,POSTTYPEID,POSTID,WORKERNAME,SEX,BIRTHDAY,WORKERCERTIFICATECODE,OLDUNITNAME,UNITNAME,UNITCODE,PHONE,CERTIFICATECODE,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,APPLYSTATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,APPLYDATE,APPLYMAN,APPLYCODE,ACCEPTDATE,GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,CERTIFICATEID,ADDPOSTID,SHEBAOCHECK,DELTIME )
                                     select APPLYID,WORKERID,POSTTYPEID,POSTID,WORKERNAME,SEX,BIRTHDAY,WORKERCERTIFICATECODE,OLDUNITNAME,UNITNAME,UNITCODE,PHONE,CERTIFICATECODE,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,APPLYSTATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,APPLYDATE,APPLYMAN,APPLYCODE,ACCEPTDATE,GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,CERTIFICATEID,ADDPOSTID,SHEBAOCHECK, getdate()
                                    FROM DBO.CERTIFICATEENTERAPPLY where dateadd(month,1,ApplyDate) < getdate() and (APPLYSTATUS='待单位确认' or APPLYSTATUS='退回修改' or APPLYSTATUS='已申请')
                                    ";
                CommonDAL.ExecSQL(HISsql);

                //物理删除
                string delsql = @"DELETE FROM DBO.CERTIFICATEENTERAPPLY where dateadd(month,1,ApplyDate) < getdate() and (APPLYSTATUS='待单位确认' or APPLYSTATUS='退回修改' or APPLYSTATUS='已申请')";
                CommonDAL.ExecSQL(delsql);
                return dataCount;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("系统服务，删除进京申请一个月后不审核的申请失败。错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //删除考试后审核未通过的考试报名申请
        private int DeleteExpiredExamSignup()
        {
            try
            {
                //统计数量
                int dataCount = CommonDAL.SelectRowCount("dbo.VIEW_EXAMSIGNUP_NEW", @" and EXAMSTARTDATE < dateadd(day,-1,getdate()) and STATUS <>'已缴费'");

                //备份删除数据
                string sql = @"
			            INSERT INTO dbo.ExamSignUp_Del( DeleteMan, DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY)
			            select '系统', '{0}',ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY from dbo.ExamSignUp
                        where ExamSignUpID in (select ExamSignUpID FROM DBO.VIEW_EXAMSIGNUP_NEW where EXAMSTARTDATE < dateadd(day,-1,getdate()) and STATUS <>'已缴费')";
                CommonDAL.ExecSQL(string.Format(sql, DateTime.Now));

                //物理删除
                string delsql = @"DELETE FROM DBO.ExamSignUp where ExamSignUpID in (select ExamSignUpID FROM DBO.VIEW_EXAMSIGNUP_NEW where EXAMSTARTDATE < dateadd(day,-1,getdate()) and STATUS <>'已缴费')";
                CommonDAL.ExecSQL(delsql);
                return dataCount;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("系统服务，删除考试后没有审核通过的考试报名申请失败。错误信息：" + ex.Message, ex);
                return 0;
            }
        }


        //获取基础数据库企业资质详细（本地施工企业(主项资质)、中央及外地进京、设计施工一体化）
        private int Get_View_SGQYQ_RYKW()
        {
            //5.98计划任务每天22:15执行 生成文件到 d:/webroot/qyzzsub.txt
            //copy 到 人员 5.80/d:/data_backup/qyzzsub.txt

            try
            {
                string delsql = "TRUNCATE TABLE DBO.QY_BWDZZZS_DETAIL_ING;";
                CommonDAL.ExecSQL(delsql);

                //string insertsql = "COPY dbo.QY_BWDZZZS_DETAIL_ING(ZZZSBH,ZZJGDM,QYMC,FDDBR,QYFZR,JSFZR,QYZZLX,SZD,ZZLB,ZZXL,ZZDJ,QYLB ) FROM 'd:\\data_backup\\qyzzsub.txt' with  DELIMITER  '	'  CSV QUOTE ''''";
                //string insertsql = @"bulk insert [dbo].[QY_BWDZZZS_DETAIL_ING] from 'd:\\data_backup\\qyzzsub.txt' with (fieldterminator=',',rowterminator='\n')";
                string insertsql = @"INSERT INTO [dbo].[QY_BWDZZZS_DETAIL_ING]([QYLB],[ZZJGDM],[QYMC],[SZD],[ZZZSBH],[QYZZLX],[ZZLB],[ZZXL],[ZZDJ],[FDDBR],[QYFZR],[JSFZR])
                                    SELECT replace(replace(replace(isnull([qylx],''),char(10),''),char(9),''),',','，') 
                                    ,replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),char(9),''),',','，'),'-','')
                                    ,replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([xzdqbm],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([ZZZSBH],''),char(10),''),char(9),''),',','，') 
                                    ,replace(replace(replace(isnull([QYZZLX],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([zzlb],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([ZZXL],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([zzdj],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([FDDBR],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([QYFZR],''),char(10),''),char(9),''),',','，')
                                    ,replace(replace(replace(isnull([JSFZR],''),char(10),''),char(9),''),',','，')  
                                     FROM [192.168.7.56].[ShareDB].[dbo].[View_SGQYQ_RYKW];";
                CommonDAL.ExecSQL(insertsql);

                int dataCount = CommonDAL.SelectRowCount("dbo.QY_BWDZZZS_DETAIL_ING", "");
                if (dataCount > 10000)//默认数据大于10000，认为导入成功,再复制到正式表。
                {
                    CommonDAL.ExecSQL("TRUNCATE TABLE DBO.QY_BWDZZZS_DETAIL;");
                    CommonDAL.ExecSQL("insert into dbo.QY_BWDZZZS_DETAIL select * from dbo.QY_BWDZZZS_DETAIL_ING;");

                    return dataCount;
                }
                return 0;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("批量同步企业资质详细错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //三类人继续教育学时
        private void Get_LearnRecord()
        {
            //计划任务每天04:05获取三类人继续教育88服务器http://116.213.192.88/LearnRecord.csv 生成文件到 d:\\data_backup\LearnRecord.csv
            //truncate table dbo.LEARNRECORD_ING;COPY dbo.LEARNRECORD_ING(RECORDNO,POSTNAME,WORKERNAME,WORKERCERTIFICATECODE,LINKTEL,CERTIFICATECODE,CLASSHOUR) FROM 'd:\\data_backup\\learnrecord.csv' with  DELIMITER  ','  CSV QUOTE '\"';" 


            try
            {
                //string delsql = "TRUNCATE TABLE DBO.LEARNRECORD_ING;";
                //CommonDAL.ExecSQL(delsql);

                //string insertsql = "COPY dbo.LEARNRECORD_ING(RECORDNO,POSTNAME,WORKERNAME,WORKERCERTIFICATECODE,LINKTEL,CERTIFICATECODE,CLASSHOUR) FROM 'd:\\data_backup\\learnrecord.csv' with  DELIMITER  ','  CSV QUOTE '\"'";
                //CommonDAL.ExecSQL(insertsql);

                //string insertsql = @"bulk insert [dbo].[LEARNRECORD_ING] from 'd:\\data_backup\\learnrecord.txt' with (fieldterminator=',',rowterminator='\n')";


                //                string sql = @"
                //TRUNCATE TABLE DBO.LEARNRECORD_ING;
                //
                //COPY dbo.LEARNRECORD_ING(RECORDNO,POSTNAME,WORKERNAME,WORKERCERTIFICATECODE,LINKTEL,CERTIFICATECODE,CLASSHOUR) FROM 'd:\\data_backup\\learnrecord.csv' with  DELIMITER  ','  CSV QUOTE '""';
                //
                //MERGE INTO dbo.LEARNRECORD t1 USING dbo.LEARNRECORD_ING t2
                //ON t1.RECORDNO=t2.RECORDNO and t1.WORKERCERTIFICATECODE=t2.WORKERCERTIFICATECODE
                //WHEN MATCHED THEN UPDATE SET t1.POSTNAME=t2.POSTNAME,t1.WORKERNAME=t2.WORKERNAME,t1.LINKTEL=t2.LINKTEL,t1.CERTIFICATECODE=t2.CERTIFICATECODE,t1.CLASSHOUR=t2.CLASSHOUR +'学时'
                //WHEN NOT MATCHED THEN INSERT(RECORDNO,POSTNAME,WORKERNAME,WORKERCERTIFICATECODE,LINKTEL,CERTIFICATECODE,CLASSHOUR) 
                //VALUES(t2.RECORDNO,t2.POSTNAME,t2.WORKERNAME,t2.WORKERCERTIFICATECODE,t2.LINKTEL,t2.CERTIFICATECODE,t2.CLASSHOUR +'学时');";

                string sql = @"
TRUNCATE TABLE DBO.LEARNRECORD_ING;

bulk insert [dbo].[LEARNRECORD_ING] from 'd:\\data_backup\\learnrecord.txt' with (fieldterminator=',',rowterminator='\n');

MERGE INTO dbo.LEARNRECORD t1 USING dbo.LEARNRECORD_ING t2
ON t1.RECORDNO=t2.RECORDNO and t1.WORKERCERTIFICATECODE=t2.WORKERCERTIFICATECODE
WHEN MATCHED THEN UPDATE SET t1.POSTNAME=t2.POSTNAME,t1.WORKERNAME=t2.WORKERNAME,t1.LINKTEL=t2.LINKTEL,t1.CERTIFICATECODE=t2.CERTIFICATECODE,t1.CLASSHOUR=t2.CLASSHOUR +'学时'
WHEN NOT MATCHED THEN INSERT(RECORDNO,POSTNAME,WORKERNAME,WORKERCERTIFICATECODE,LINKTEL,CERTIFICATECODE,CLASSHOUR) 
VALUES(t2.RECORDNO,t2.POSTNAME,t2.WORKERNAME,t2.WORKERCERTIFICATECODE,t2.LINKTEL,t2.CERTIFICATECODE,t2.CLASSHOUR +'学时');";

                CommonDAL.ExecSQL(sql);
                WriteOperateLog("系统服务", 0, "三类人续期比对", "成功");
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("批量都倒入三类人继续教育学时信息错误：" + ex.Message, ex);
            }

            //try
            //{
            //    Txt_INSI_To_UTF8("http://192.168.5.180/Exam/Template/continue.csv","E://Exam/Template/continue.csv");
            //}
            //catch (Exception ex)
            //{
            //    FileLog.WriteLog("转化三类人导出为UTF8错误：" + ex.Message, ex);
            //}
        }

        //企业资质证书
        private int Get_QYZZZS()
        {
            try
            {
                //string delsql = "DELETE FROM dbo.QY_BWDZZZS WHERE QYLB <> '起重机械租赁企业' and  QYLB <> '设计施工一体化' ";
                string delsql = "truncate table DBO.QY_BWDZZZS_ING ";
                CommonDAL.ExecSQL(delsql);

                Condition = " QYZZLX LIKE '%施工%' and Valid=1";
                SortText = "";
                iInterFaceID = "fde0ddcb-ca6f-48ca-b213-6d25a970eac0";

                jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                DataSet ds = jcsjk.QueryData(out outResultCount, descObj.EncryptString(userName), descObj.EncryptString(userPassword), iInterFaceID, "BBB", 0, Int32.MaxValue, Condition, SortText);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string ZZJGDM = dr["ZZJGDM"].ToString();
                    string QYMC = dr["QYMC"].ToString();
                    string QYLB = "建筑施工企业";

                    string sql = @"
                    INSERT INTO dbo.QY_BWDZZZS_ING(QYLB,ZZJGDM,QYMC) VALUES('" + QYLB + "',replace(replace(replace(replace('" + ZZJGDM + "','x','X'),'-',''),' ',''),'－',''),'" + QYMC + "')";

                    CommonDAL.ExecSQL(sql);
                }
                return ds.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("企业资质错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //爆破与拆装企业资质、劳务企业资质、机械租赁企业资质
        private int Get_QYZZZS_TESHU()
        {
            //复制爆破与拆装企业资质到本地建筑企业资质库（改资质已经取消，但近期仍可以考试报名等业务）
            //5.98计划任务每天22:15执行 生成文件到 d:/webroot/爆破与拆装企业资质.txt
            //copy 到 人员 5.80/d:/data_backup/爆破与拆装企业资质.txt

            try
            {

                //string insertsql = "COPY dbo.QY_BWDZZZS_ING(QYLB,ZZJGDM,QYMC) FROM 'd:\\data_backup\\爆破与拆装企业资质.txt' with  DELIMITER  '	'  CSV QUOTE ''''";
                //string insertsql = @"bulk insert [dbo].[QY_BWDZZZS_ING] from 'd:\\data_backup\\爆破与拆装企业资质.txt' with (fieldterminator=',',rowterminator='\n')";

                //爆破与拆装企业资质
                string insertsql = @"INSERT INTO [dbo].[QY_BWDZZZS_ING]([QYLB],[ZZJGDM],[QYMC])
                                    select '建筑施工企业'
	                                ,replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),	char(9),''),',','，'),'-','')
	                                ,replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，')
	                                 from [192.168.7.89].[SJZX_BAK].[dbo].[BPYCCZYQY] 
	                                 where zzjgdm not in (
		                                 select distinct zzjgdm from [192.168.7.56].[ShareDB].[dbo].[QY_ZZZS] 
		                                 where valid=1
	                                 )
	                                 union 
	                                 select '建筑施工企业'
	                                 ,replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),char(9),''),',','，'),'-','')
	                                 ,replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，')
	                                 from  [192.168.7.56].JCSJK_HisData.dbo.QY_ZZZS_BCZZ 
	                                 where valid=1 and zzjgdm not in (
		                                 select distinct zzjgdm from [192.168.7.56].[ShareDB].[dbo].[QY_ZZZS] 
		                                 where valid=1
	                                 ) 
	                                 union 
	                                 SELECT 
	                                 [zzlx]
	                                 ,replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),char(9),''),',','，'),'-','')
	                                 ,replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，') 
	                                 FROM [192.168.7.56].[ShareDB].[dbo].[View_QYJXZLQY_RYKW];";
                CommonDAL.ExecSQL(insertsql);
                return 0;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("批量同步企业资质（爆破与拆装企业资质）错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //中央在京、外地进京
        private int Get_WDZZZS()
        {
            try
            {
                Condition = " QYZZLX LIKE '%施工%' and Valid=1";
                SortText = "";
                iInterFaceID = "ac2758ec-4a5c-4c1e-a064-c11f4948f055";
                jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                DataSet ds = jcsjk.QueryData(out outResultCount, descObj.EncryptString(userName), descObj.EncryptString(userPassword), iInterFaceID, "CCC", 0, Int32.MaxValue, Condition, SortText);

                foreach (DataRow drSG in ds.Tables[0].Rows)
                {
                    string ZZJGDM = drSG["ZZJGDM"].ToString();
                    string QYMC = drSG["QYMC"].ToString();
                    string QYLB = drSG["XYLZGX"].ToString();
                    string sql = @"
                    INSERT INTO dbo.QY_BWDZZZS_ING(QYLB,ZZJGDM,QYMC) VALUES('" + QYLB + "',replace(replace(replace(replace('" + ZZJGDM + "','x','X'),'-',''),' ',''),'－',''),'" + QYMC + "')";

                    CommonDAL.ExecSQL(sql);
                }
                return ds.Tables[0].Rows.Count;

            }
            catch (Exception ex)
            {
                FileLog.WriteLog("外地资质错误信息：" + ex.Message, ex);
                return 0;
            }
        }

        //设计施工一体化
        private int Get_SJSGYTH()
        {
            try
            {
                Condition = " Valid=1";
                SortText = "";
                iInterFaceID = "09c41849-6f14-439f-8c47-233b8741683e";
                jcsjk = new JCSJKService.InterFaceServiceSoapClient();
                DataSet ds = jcsjk.QueryData(out outResultCount, descObj.EncryptString(userName), descObj.EncryptString(userPassword), iInterFaceID, "DDD", 0, Int32.MaxValue, Condition, SortText);

                foreach (DataRow drSJ in ds.Tables[0].Rows)
                {
                    string ZZJGDM = drSJ["ZZJGDM"].ToString();
                    string QYMC = drSJ["QYMC"].ToString();
                    string QYLB = "设计施工一体化";

                    string sql = @"
                    INSERT INTO dbo.QY_BWDZZZS_ING(QYLB,ZZJGDM,QYMC) VALUES('" + QYLB + "',replace(replace(replace(replace('" + ZZJGDM + "','x','X'),'-',''),' ',''),'－',''),'" + QYMC + "')";
                    CommonDAL.ExecSQL(sql);
                }
                return ds.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("设计施工一体化错误信息：" + ex.Message, ex);
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 判断是否为白底一寸照片
        /// </summary>
        /// <param name="imgPath">照片绝对地址</param>
        /// <returns>白底返回true，否则返回false</returns>
        private bool CheckIfWhiteBackgroudPhoto(string imgPath)
        {
            System.Drawing.Color c;
            int pointCount = 0;//统计读取像素点总数
            int whitePointCount = 0;//统计白色像素点总数
            int whiteBorder = 210;//白色临界值（大于该值视为白色）

            using (System.Drawing.Bitmap map = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(imgPath))
            {
                for (int i = 0; i < map.Width; i++)
                {
                    for (int j = 0; j < (map.Height / 2); j++)
                    {
                        c = map.GetPixel(i, j);
                        pointCount++;
                        if (c.R > whiteBorder && c.G > whiteBorder && c.B > whiteBorder)
                        {
                            whitePointCount++;
                        }
                    }
                }
            }
            if ((whitePointCount * 100 / pointCount) > 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 建设二建证书白底照片，更新标志为[IfWhiteImg]：白底=1，非白底=0，失败=-1
        /// </summary>
        private void CheckJZSImgBackgroud()
        {
            string sql = String.Format(@"SELECT top {0}  b.PSN_CertificateNO,b.PSN_RegisterNO,f.FileUrl
                                        FROM [dbo].[COC_TOW_Person_File] c
                                        inner join [dbo].[FileInfo] f on c.[FileID]=f.[FileID] and f.DataType='一寸免冠照片' and c.[IsHistory]=0
                                        right join [dbo].[COC_TOW_Person_BaseInfo] b on c.[PSN_RegisterNO] =b.[PSN_RegisterNO]
                                        where b.[PSN_Level]='二级' and f.FileUrl is not null and b.[IfWhiteImg] is null
                                        order by b.PSN_CertificateNO", MaxCountExe);
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count == 0) return;

            bool ifwhite = false;
            foreach (DataRow r in dtOriginal.Rows)
            {
                try
                {
                    ifwhite = CheckIfWhiteBackgroudPhoto(r["FileUrl"].ToString().Replace("~", ExamWebRoot));
                    FileLog.WriteLog(string.Format("{0}检测白底一寸照片结果：{1}。", r["PSN_RegisterNO"], ifwhite));
                    CommonDAL.ExecSQL(string.Format("update [dbo].[COC_TOW_Person_BaseInfo] set [IfWhiteImg]={0} where PSN_RegisterNO='{1}'", (ifwhite == true ? 1 : 0), r["PSN_RegisterNO"]));
                }
                catch (Exception ex)
                {
                    CommonDAL.ExecSQL(string.Format("update [dbo].[COC_TOW_Person_BaseInfo] set [IfWhiteImg]={0} where PSN_RegisterNO='{1}'", -1, r["PSN_RegisterNO"]));
                    FileLog.WriteLog(string.Format("检测{0}白底一寸照片失败。", r["PSN_RegisterNO"]), ex);
                }
            }
        }

        /// <summary>
        /// 注册建造师
        /// 取未注销，在有效期内证书
        /// 注意：一级的不过滤有效期（尚未开展续期工作）
        /// 一级临时建造师、二级临时有效期在主表上BJJZS_EXE.dbo.View_JZS_NEW
        /// 二级建造师有效期在专业表（子表）中，其中有一个专业有效，主表证书代表有效
        /// </summary>
        private void Get_JZS()
        {
            return;//调整到存储过程获取

            //            #region sql备份
            //            //5.98视图脚本
            //            // ALTER VIEW [dbo].[View_BWDJZS_RYKWXT]
            //            //AS
            //            //select PSN_CertificateType AS 证件类别 , PSN_CertificateNO AS 证件号码,ENT_OrganizationsCode AS 组织机构代码,PSN_Name AS 姓名, PSN_RegisterNO AS 注册号,PSN_Level AS 级别, ENT_Name AS 聘用企业,  PSN_RegisteProfession AS 注册专业,LastModifyTime as 最后更新时间,'本地' AS 区域
            //            //FROM BJJZS_EXE.dbo.View_JZS_NEW  
            //            //where PSN_Level='一级' and PSN_RegisteType<>'注销'
            //            //UNION ALL
            //            //select PSN_CertificateType AS 证件类别 , PSN_CertificateNO AS 证件号码,ENT_OrganizationsCode AS 组织机构代码,PSN_Name AS 姓名, PSN_RegisterNO AS 注册号,PSN_Level AS 级别, ENT_Name AS 聘用企业,  PSN_RegisteProfession AS 注册专业,LastModifyTime as 最后更新时间,'本地' AS 区域
            //            //FROM BJJZS_EXE.dbo.View_JZS_NEW  
            //            //where (PSN_Level='一级临时' or PSN_Level='二级临时') and PSN_RegisteType<>'注销'
            //            //and PSN_CertificateValidity > getdate()
            //            //UNION ALL
            //            //select distinct 
            //            //m.PSN_CertificateType AS 证件类别 , m.PSN_CertificateNO AS 证件号码,m.ENT_OrganizationsCode AS 组织机构代码,m.PSN_Name AS 姓名, m.PSN_RegisterNO AS 注册号,m.PSN_Level AS 级别, m.ENT_Name AS 聘用企业,  m.PSN_RegisteProfession AS 注册专业,m.LastModifyTime as 最后更新时间,'本地' AS 区域
            //            //FROM BJJZS_EXE.dbo.View_JZS_NEW  m
            //            //left join [BJJZS_EXE].[dbo].[View_JZS_Profession] s
            //            //on m.PSN_Level='二级' and s.[PRO_Level]='二级' and m.[PSN_ServerID] = s.[PSN_ServerID]
            //            //where m.PSN_Level='二级' and m.PSN_RegisteType<>'注销'
            //            //and s.[PRO_ValidityEnd]> getdate()
            //            //UNION ALL
            //            //SELECT     RYZJLX, RYZJHM, ZZJGDM, XM, ZCH, JB, PYQY, ZCZY, case when xgsj is null then cjsj else xgsj end as zhgxsj,  '外地' AS 区域
            //            //FROM         [ShareDB].[dbo].[RY_WDZCJZSZSXX] where valid = 1
            //            #endregion sql备份


            //            //使用bcp同步
            //            try
            //            {
            //                //清空临时表
            //                string delsql = @"TRUNCATE TABLE DBO.RY_ZCJZS_ING;
            //                                  TRUNCATE TABLE DBO.RY_ZCJZS_WD_ING";
            //                CommonDAL.ExecSQL(delsql);

            //                //导入本地建造师到临时表
            ////                string insertsql = @"COPY dbo.RY_ZCJZS_ING(ZHGXSJ,XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,QY)
            ////                                     FROM 'd:\\data_backup\\bdjzs.txt' with  DELIMITER  '	'  CSV QUOTE ''''";
            //                string insertsql = @"bulk insert [dbo].[RY_ZCJZS_ING] from 'd:\\data_backup\\bdjzs.txt' with (fieldterminator=',',rowterminator='\n')";
            //                CommonDAL.ExecSQL(insertsql);

            //                int bd_count = CommonDAL.SelectRowCount("dbo.RY_ZCJZS_ING", "");
            //                if (bd_count > 40000)
            //                {
            //                    delsql = @"TRUNCATE TABLE DBO.RY_ZCJZS;
            //                                INSERT INTO DBO.RY_ZCJZS (XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,ZHGXSJ,QY,SHTYXYDM )
            //                                 SELECT XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,ZHGXSJ,QY,SHTYXYDM FROM DBO.RY_ZCJZS_ING;";
            //                    CommonDAL.ExecSQL(delsql);
            //                }
            //                else
            //                {
            //                    bd_count = 0;
            //                }

            //                //导入外地建造师
            ////                insertsql = @"COPY dbo.RY_ZCJZS_WD_ING(ZHGXSJ,XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,QY)
            ////                               FROM 'd:\\data_backup\\wdjzs.txt' with  DELIMITER  '	'  CSV QUOTE ''''";

            //                insertsql = @"bulk insert [dbo].[RY_ZCJZS_WD_ING] from 'd:\\data_backup\\wdjzs.txt' with (fieldterminator=',',rowterminator='\n')";

            //                CommonDAL.ExecSQL(insertsql);

            //                int wd_count = CommonDAL.SelectRowCount("dbo.RY_ZCJZS_WD_ING", "");

            //                if (wd_count > 5000)
            //                {
            //                    delsql = @"TRUNCATE TABLE DBO.RY_ZCJZS_WD;
            //                                INSERT INTO DBO.RY_ZCJZS_WD (XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,ZHGXSJ,QY )
            //                                SELECT XM,ZJLB,ZJHM,ZCZSBH,QYMC,ZCH,ZZJGDM,ZHGXSJ,QY FROM DBO.RY_ZCJZS_WD_ING;";
            //                    CommonDAL.ExecSQL(delsql);
            //                }
            //                else
            //                {
            //                    wd_count = 0;
            //                }

            //                WriteOperateLog("系统服务", 0, string.Format("获取建造师数据{0}条", (bd_count + wd_count).ToString()), string.Format("建造师数据下载：其中本地建造师{0}条，外地（含中央在京）建造师{1}条。", bd_count.ToString(), wd_count.ToString()));


            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("注册建造师错误信息：" + ex.Message, ex);
            //            }

            //            try
            //            {
            //                //处理社会统一信用代码与组织机构代码映射
            //                CommonDAL.ExecSQL(@"update dbo.ry_zcjzs set SHTYXYDM = ZZJGDM Where len(ZZJGDM) =18;
            //                                    update dbo.ry_zcjzs set ZZJGDM = substring(ZZJGDM,9,9) Where len(ZZJGDM) =18;");
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("注册建造师错误信息：" + ex.Message, ex);
            //            }
        }


        /// <summary>
        /// 获取基础数据库隶属关系（已作废：该在作业“考务每日作业任务”中执行[UP_Exam_GetQyHylsgx]）
        /// </summary>
        private void Get_HYLSGX()
        {

            //使用bcp同步
            try
            {
                string sql = "TRUNCATE TABLE DBO.QY_HYLSGX_TEMP;";
                CommonDAL.ExecSQL(sql);

                //                //导入隶属关系临时表QY_HYLSGX_TEMP
                //                sql = @"COPY dbo.QY_HYLSGX_TEMP(ID,ZZJGDM,QYMC,LSGX)
                //                                     FROM 'd:\\data_backup\\qy.txt' with  DELIMITER  '	'  CSV QUOTE ''''";

                sql = @"bulk insert [dbo].[QY_HYLSGX_TEMP] from 'd:\\data_backup\\qy.txt' with (fieldterminator=',',rowterminator='\n')";


                CommonDAL.ExecSQL(sql);

                int bd_count = CommonDAL.SelectRowCount("dbo.QY_HYLSGX_TEMP", "");
                if (bd_count > 4000)
                {
                    //写入隶属关系正式表QY_HYLSGX_ING
                    sql = @"TRUNCATE TABLE DBO.QY_HYLSGX_ING;
                            INSERT INTO DBO.QY_HYLSGX_ING (ID,ZZJGDM,QYMC,LSGX) SELECT ID,ZZJGDM,QYMC,LSGX FROM DBO.QY_HYLSGX_TEMP;
                            update DBO.QY_HYLSGX_ING set ZZJGDM = substring(zzjgdm,9,9) where len(ZZJGDM) >9; ";
                    CommonDAL.ExecSQL(sql);
                }
                else
                {
                    bd_count = 0;
                }

                //更新隶属关系设置表DBO.QY_HYLSGX
                sql = @"
                        MERGE INTO dbo.QY_HYLSGX t1 USING 
                        (
                            SELECT distinct L.ZZJGDM,L.LSGX,u.userid,u.ORGANID
                            FROM DBO.QY_HYLSGX_ING  L
                            inner join DBO.[USER] u
                            on u.ORGANID =242 
                            and L.LSGX like  replace(replace(replace(replace(u.USERNAME,'延庆县','延庆区'),'密云县','密云区'),'怀柔建委','怀柔区'),'开发区','北京市经济技术开发区')
                            and 
                            (
                                L.LSGX like '延庆%'   
                                or L.LSGX like '丰台%'    
                                or L.LSGX like '昌平%'  
                                or L.LSGX like '朝阳%'    
                                or L.LSGX like '东城%'    
                                or L.LSGX like '通州%'    
                                or L.LSGX like '西城%'    
                                or L.LSGX like '宣武%'  
                                or L.LSGX like '崇文%'  
                                or L.LSGX like '海淀%'    
                                or L.LSGX like '怀柔%'    
                                or L.LSGX like '房山%'    
                                or L.LSGX like '顺义%'    
                                or L.LSGX like '密云%'    
                                or L.LSGX like '平谷%'    
                                or L.LSGX like '大兴%'    
                                or L.LSGX like '门头沟%'    
                                or L.LSGX like '石景山%'    
                                or L.LSGX like '%开发区%'   
                            ) 
                            inner join dbo.QY_HYLSGX q on L.ZZJGDM =q.ZZJGDM and (q.ORGANID =242 or q.ORGANID is null)
                         ) t2 ON t1.ZZJGDM=t2.ZZJGDM 
                         WHEN MATCHED THEN UPDATE SET t1.LSGX = t2.LSGX,t1.USERID = t2.USERID,t1.ORGANID = t2.ORGANID;";

                CommonDAL.ExecSQL(sql);

                //新增隶属关系
                sql = @"
                    INSERT INTO dbo.QY_HYLSGX (ID,ZZJGDM,QYMC,LSGX,userid,ORGANID)
                    SELECT L.ID,L.ZZJGDM,L.QYMC,L.LSGX,u.userid,u.ORGANID
                    FROM DBO.QY_HYLSGX_ING  L
                    left join DBO.QY_HYLSGX t2 on L.ZZJGDM=t2.ZZJGDM 
                    left join DBO.[USER] u
                    on (u.ORGANID =242 or u.ORGANID =246 or u.ORGANID =247 )
                    and L.LSGX like  replace(replace(replace(replace(u.USERNAME,'延庆县','延庆区'),'密云县','密云区'),'怀柔建委','怀柔区'),'开发区','北京市经济技术开发区')
                    and 
                    (
                        L.LSGX like '延庆%'   
                        or L.LSGX like '丰台%'    
                        or L.LSGX like '昌平%'  
                        or L.LSGX like '朝阳%'    
                        or L.LSGX like '东城%'    
                        or L.LSGX like '通州%'    
                        or L.LSGX like '西城%'    
                        or L.LSGX like '宣武%' 
                        or L.LSGX like '崇文%'     
                        or L.LSGX like '海淀%'    
                        or L.LSGX like '怀柔%'    
                        or L.LSGX like '房山%'    
                        or L.LSGX like '顺义%'    
                        or L.LSGX like '密云%'    
                        or L.LSGX like '平谷%'    
                        or L.LSGX like '大兴%'    
                        or L.LSGX like '门头沟%'    
                        or L.LSGX like '石景山%'    
                        or L.LSGX like '%开发区%'   
                    ) 
                    where t2.ZZJGDM is null";
                CommonDAL.ExecSQL(sql);

                WriteOperateLog("系统服务", 0, string.Format("获取企业隶属关系{0}条", bd_count), "");
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("获取企业隶属关系错误信息：" + ex.Message, ex);
            }
        }

        #region 二建上传建设部


        /// <summary>
        /// 上传二级建造师到建设部
        /// </summary>
        /// <returns></returns>
        private void Upload_JZS()
        {
            //******************************************
            //*  已废弃，改用 Upload_JZS_New（）新接口
            //******************************************
            return;

            //CreateLink();//建立安全连接

            ////准备上传数据
            //DataSet ds = new DataSet();
            //ds.Tables.Add(Get_qyxxb("二级").Copy());
            //ds.Tables.Add(Get_ryxxb("二级").Copy());
            //ds.Tables.Add(Get_zczyb().Copy());
            ////ds.Tables[0].TableName = "qyxxb"; //企业信息DataTable 名称要指定为qyxxb
            ////ds.Tables[1].TableName = "ryxxb"; //个人信息DataTable 名称要指定要grxxb
            ////ds.Tables[2].TableName = "zczyb"; //专业信息DataTable 名称要指定为 zczyb

            //FileLog.WriteLog(string.Format("准备上传二级建造师数据。其中企业信息数据量：{0}条，个人信息数据：{1}条,专业信息数据：{2}条。", ds.Tables[0].Rows.Count, ds.Tables[1].Rows.Count, ds.Tables[2].Rows.Count), null);

            ////使用 Serializatior.SerialObjectToBinary 序列化
            ////使用 Compressor.Compress 压缩数据
            //byte[] _bytes = Compressor.Compress(Serializatior.SerialObjectToBinary(ds));

            //SecurityLib _SecurityLib = new SecurityLib();
            //this.Start("1-111", _SecurityLib.Encrypt(_bytes, this.G_SecurityKeyword.Key, this.G_SecurityKeyword.IV));


            CreateLink();//建立安全连接

            //准备上传数据
            DataSet ds = new DataSet();
            ds.Tables.Add(Get_qyxxb("二级").Copy());

            //ds.Tables[0].TableName = "qyxxb"; //企业信息DataTable 名称要指定为qyxxb
            //ds.Tables[1].TableName = "ryxxb"; //个人信息DataTable 名称要指定要grxxb
            //ds.Tables[2].TableName = "zczyb"; //专业信息DataTable 名称要指定为 zczyb

            FileLog.WriteLog(string.Format("准备上传二级建造师数据 - 企业信息数据：{0}条。", ds.Tables[0].Rows.Count), null);
            //使用 Serializatior.SerialObjectToBinary 序列化
            //使用 Compressor.Compress 压缩数据
            byte[] _bytes = Compressor.Compress(Serializatior.SerialObjectToBinary(ds));
            SecurityLib _SecurityLib = new SecurityLib();
            this.Start("1-111", _SecurityLib.Encrypt(_bytes, this.G_SecurityKeyword.Key, this.G_SecurityKeyword.IV));


            ds = new DataSet();
            ds.Tables.Add(Get_ryxxb("二级").Copy());
            FileLog.WriteLog(string.Format("准备上传二级建造师数据 - 个人信息数据：{0}条。", ds.Tables[0].Rows.Count), null);
            _bytes = Compressor.Compress(Serializatior.SerialObjectToBinary(ds));
            this.Start("1-111", _SecurityLib.Encrypt(_bytes, this.G_SecurityKeyword.Key, this.G_SecurityKeyword.IV));


            ds = new DataSet();
            ds.Tables.Add(Get_zczyb().Copy());
            FileLog.WriteLog(string.Format("准备上传二级建造师数据 - 专业信息数据：{0}条。", ds.Tables[0].Rows.Count), null);
            _bytes = Compressor.Compress(Serializatior.SerialObjectToBinary(ds));
            this.Start("1-111", _SecurityLib.Encrypt(_bytes, this.G_SecurityKeyword.Key, this.G_SecurityKeyword.IV));
        }

        /// <summary>
        /// 上传二级临时建造师到建设部
        /// </summary>
        /// <returns></returns>
        private void Upload_JZS_LS()
        {
            //******************************************
            //*  已废弃，改用 Upload_JZS_New（）新接口
            //******************************************
            return;

            CreateLink();//建立安全连接

            //准备上传数据
            DataSet ds = new DataSet();
            ds.Tables.Add(Get_qyxxb("二级临时").Copy());
            ds.Tables.Add(Get_ryxxb("二级临时").Copy());
            //ds.Tables[0].TableName = "qyxxb"; //企业信息DataTable 名称要指定为qyxxb
            //ds.Tables[1].TableName = "ryxxb"; //个人信息DataTable 名称要指定要grxxb

            FileLog.WriteLog(string.Format("准备上传二级临时建造师数据。其中企业信息数据量：{0}条，个人信息数据：{1}条。", ds.Tables[0].Rows.Count, ds.Tables[1].Rows.Count), null);

            //使用 Serializatior.SerialObjectToBinary 序列化
            //使用 Compressor.Compress 压缩数据
            byte[] _bytes = Compressor.Compress(Serializatior.SerialObjectToBinary(ds));

            SecurityLib _SecurityLib = new SecurityLib();
            this.Start("1-131", _SecurityLib.Encrypt(_bytes, this.G_SecurityKeyword.Key, this.G_SecurityKeyword.IV));
        }

        /// <summary>
        /// 异步更新主调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void doUpgrade(object sender, DoWorkEventArgs e)
        {
            try
            {
                zxja.Interface.Buisness.PostParameter _PostParameter = (zxja.Interface.Buisness.PostParameter)e.Argument;
                //this.PrintMessage("开始下载数据(actionCode:" + _PostParameter.ActionCode + ";省:" + _PostParameter.ExPara1 + ").....\r\n");


                //实例化webservice


                //调用webservice
                byte[] _result = _DBUploader.DoAction(this.GetByte(_PostParameter), null, null);

                //this.PrintMessage("数据下载完成,开始本地解压数据(actionCode:" + _PostParameter.ActionCode + ").....\r\n");
                //数据下载完成后解压、反序列化
                zxja.Interface.Buisness.ReturnObject _ReturnObject = zxja.Interface.Buisness.Serializatior.UnSerialBinaryToObject<zxja.Interface.Buisness.ReturnObject>(zxja.Interface.Buisness.Compressor.UnCompress(_result));

                e.Result = _ReturnObject;

            }
            catch (System.Net.WebException webEx)
            {

                throw new ArgumentException("连接异常!(响应代码:" + webEx.Status.ToString() + ")");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);

            }
        }
        /// <summary>
        /// 序列化、压缩对象
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public byte[] GetByte(object o)
        {
            try
            {

                if (o == null)
                    return null;
                byte[] objs = Serializatior.SerialObjectToBinary(o);
                return Compressor.Compress(objs);
            }
            catch (Exception ex)
            {
                throw new Exception("序列化失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 异步更新完毕回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpgradeComplated(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                FileLog.WriteLog("上传建造师到建设部失败，错误信息：" + e.Error.Message, null);

            }
            else if (e.Cancelled)
            {
                FileLog.WriteLog("上传建造师到建设部操作已取消。", null);
            }
            else
            {
                //==========================数据通过WebService下载后*start=============================
                try
                {
                    zxja.Interface.Buisness.ReturnObject _ReturnObject = (zxja.Interface.Buisness.ReturnObject)e.Result;
                    //_downQueue.RemoveAt(0); //移除队列中的第一项

                    //是否有服务器端异常
                    if (!_ReturnObject.IfException)  //无异常
                    {
                        byte[] _enBytes;
                        switch (_ReturnObject.ActionCode)
                        {
                            case "0-001": //获取数据加密密钥
                                _enBytes = _ReturnObject.DsBytes; //这里就是返回来的数据加密密钥了;得用私钥解密

                                //公/私钥加密类
                                RASEncryptLib _RASEncryptLib = new RASEncryptLib();
                                byte[] _denBytes = _RASEncryptLib.RSADecrypt(this.G_SecurityKeyword.PrivateKey, _enBytes);
                                //解密后，解压缩，反序列化
                                //注：得到数据加密密钥后,以后上传建造师数据时就使用这个密钥,公私钥就没用了
                                this.G_SecurityKeyword = Serializatior.UnSerialBinaryToObject<SecurityKeyword>(Compressor.UnCompress(_denBytes));

                                FileLog.WriteLog(string.Format("获取数据加密密钥成功！Key:{0}，IV:{1}。", Convert.ToBase64String(this.G_SecurityKeyword.Key), Convert.ToBase64String(this.G_SecurityKeyword.IV)), null);
                                break;
                            case "1-111":
                                FileLog.WriteLog("二级建造师数据上传成功！", null);
                                break;
                            case "1-131":

                                FileLog.WriteLog("二级建造师临时执业证书数据上传成功！", null);
                                break;
                        }
                    }
                    else //服务器端发生异常
                    {
                        FileLog.WriteLog("上传建造师到建设部失败，错误信息：" + _ReturnObject.ExceptionString, null);
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("上传建造师到建设部失败，错误信息：" + ex.Message, ex);
                }
            }

        }

        ///// <summary>
        ///// 异步调用过程处理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void UpgradeProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //}


        /// <summary>
        /// 获取二级人员及二级临时企业信息
        /// </summary>
        /// <param name="level">二级临时/二级</param>
        /// <returns></returns>
        public DataTable Get_qyxxb(string level)
        {
            //qybm	企业编码
            //qymc	企业名称
            //sjbm	省份编码
            //gszcsf	省份名称
            //citybm	市区编码
            //gszcsz	市区名称
            //zzjgdm	组织机构代码
            //yyzzh	营业执照号
            //fddbr	法定代表人
            //txdz	通讯地址
            //qylx	企业类型
            //zzlb	资质类别
            //zzdj	资质等级
            //zzzsbh	资质证书编号

            DataTable dt = new DataTable();
            string sql = "";
            try
            {

                if (level == "二级")//二级
                {
                    sql = @" select 
                                UnitID as qybm,
                                ENT_Name as qymc,
                                ENT_Province_Code as sjbm,
                                ENT_Province as gszcsf,
                                [Dict_Region].RegionCode  as   citybm,
                                ENT_City as gszcsz,
                                ENT_OrganizationsCode as zzjgdm,
                                ENT_OrganizationsCode  as yyzzh,
                                ENT_Corporate as fddbr,
                                ENT_Correspondence  as txdz,
                                left(ENT_Type,20) as qylx,
                                left(ENT_Sort,50) as zzlb,
                                left(ENT_Grade,10) as zzdj,
                                ENT_QualificationCertificateNo as zzzsbh
                            from unit
                            inner join 
                            (
		                        select distinct [ENT_ServerID]
		                        FROM [dbo].[COC_TOW_Person_BaseInfo]
		                        where [PSN_Level]='二级'
                            ) c on unit.UnitID = c.ENT_ServerID
                            left join [dbo].[Dict_Region] 
                            on [Unit].[ENT_City]  like replace(replace([Dict_Region].RegionName,'区',''),'县','') +'%'";

                    dt = new DBHelper().GetFillData(sql);
                }
                else if (level == "二级临时")//二级临时
                {
                    sql = @" select 
                                UnitID as qybm,
                                ENT_Name as qymc,
                                ENT_Province_Code as sjbm,
                                ENT_Province as gszcsf,
                                [Dict_Region].RegionCode  as   citybm,
                                ENT_City as gszcsz,
                                ENT_OrganizationsCode as zzjgdm,
                                ENT_OrganizationsCode  as yyzzh,
                                ENT_Corporate as fddbr,
                                ENT_Correspondence  as txdz,
                                left(ENT_Type,20) as qylx,
                                left(ENT_Sort,50) as zzlb,
                                left(ENT_Grade,10) as zzdj,
                                ENT_QualificationCertificateNo as zzzsbh
                            from unit
                            inner join 
							(
								 select distinct [ENT_ServerID]
								  FROM [dbo].[COC_TOW_Person_BaseInfo]
								  where [PSN_Level]='二级临时'
							) c on unit.UnitID = c.ENT_ServerID
                            left join [dbo].[Dict_Region] 
                            on [Unit].[ENT_City]  like replace(replace([Dict_Region].RegionName,'区',''),'县','') +'%'";

                    dt = new DBHelper().GetFillData(sql);//申请单 
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("企业信息表传输失败。", ex);
            }
            dt.TableName = "qyxxb";
            return dt;
        }

        /// <summary>
        /// 获取二级及二级临时人员信息
        /// </summary>
        /// <param name="level">二级临时/二级</param>
        /// <returns></returns>
        public DataTable Get_ryxxb(string level)
        {
            DataTable dt = new DataTable();
            string sql = "";
            try
            {

                if (level == "二级")//二级
                {
                    //字段名	字段描述
                    //qybm	企业编码
                    //rybm	人员编码
                    //xm	姓名
                    //qymc	企业名称
                    //xb	性别
                    //lxdh	联系电话
                    //E_Mail	电子邮件
                    //csny	出生日期
                    //zjlb	证件类别
                    //zjhm	证件号码
                    //zgzsbh	资格证书编号
                    //zsbh	注册证书编号
                    //zcbh	注册编号
                    //fzrq	发证日期
                    //zcyxq	注册有效期
                    //zyyzjym	执业印章校验码
                    //xl	学历
                    //xw	学位
                    //sjhm	手机号码
                    sql = @" select 
                                ENT_ServerID as  qybm,
                                PSN_ServerID as rybm,
                                PSN_Name as  xm,
                                ENT_Name as qymc,
                                PSN_Sex as xb,
                                PSN_Telephone as lxdh,
                                PSN_Email as E_Mail,
                                PSN_BirthDate as csny,
                                PSN_CertificateType  as  zjlb,
                                PSN_CertificateNO as zjhm, 
                                [ZGZSBH] as   zgzsbh,
                                PSN_RegisterCertificateNo  as zsbh,
                                PSN_RegisterNO  as   zcbh,
                                PSN_CertificationDate  as fzrq,
                                PSN_CertificateValidity   as zcyxq,
                                [PSN_CheckCode] as zyyzjym, 
                                PSN_Qualification as  xl,
                                PSN_Degree as xw,
                                PSN_MobilePhone  as sjhm
                                from [dbo].[COC_TOW_Person_BaseInfo]
                                where [PSN_Level]='二级' and PSN_RegisteType <'07'";

                    dt = new DBHelper().GetFillData(sql);
                }
                else if (level == "二级临时")//二级临时
                {
                    //字段名	字段描述
                    //qybm	企业编码
                    //rybm	人员编码
                    //xm	姓名
                    //qymc	企业名称
                    //xb	性别
                    //csny	出生日期
                    //lxdh	联系电话
                    //E_Mail	电子邮件
                    //zjlb	证件类别
                    //zjhm	证件号码
                    //zsbh	注册证书编号
                    //zcbh	注册编号
                    //fzrq	发证日期
                    //zcyxq	注册有效期
                    //zczy	注册专业
                    //zyyzjym	执业印章校验码
                    //xl	学历
                    //xw	学位
                    //sjhm	手机号码

                    sql = @"select
                                ENT_ServerID as  qybm,
                                PSN_ServerID as rybm,
                                PSN_Name as  xm,
                                ENT_Name as qymc,
                                PSN_Sex as xb,
                                PSN_BirthDate as csny,
                                PSN_Telephone as lxdh, 
                                PSN_Email as E_Mail,                           
                                PSN_CertificateType  as  zjlb,
                                PSN_CertificateNO as zjhm,                               
                                PSN_RegisterCertificateNo  as zsbh, 
                                PSN_RegisterNO  as   zcbh,
                                PSN_CertificationDate  as fzrq, 
                                PSN_CertificateValidity   as zcyxq,
                                [PSN_RegisteProfession] as zczy,
                                [PSN_CheckCode] as zyyzjym, 
                                PSN_Qualification as  xl,
                                PSN_Degree as xw,
                                PSN_MobilePhone  as sjhm
                            from [dbo].[COC_TOW_Person_BaseInfo]
                            where [PSN_Level]='二级临时' and PSN_RegisteType <'07'";

                    dt = new DBHelper().GetFillData(sql);//申请单 
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("二级临时人员信息表传输失败。", ex);
            }
            dt.TableName = "ryxxb";
            return dt;

        }

        ///获取二级专业信息
        public DataTable Get_zczyb()
        {
            //字段名	字段描述
            //rybm	人员编码
            //zczy	注册专业
            //yxqq	有效期-起始日期
            //yxqz	有效期-截至日期

            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                sql = @" select  
                            PSN_ServerID as rybm,
                            PRO_Profession as zczy ,
                            PRO_ValidityBegin as yxqq, 
                            PRO_ValidityEnd as yxqz
                         from  [dbo].[COC_TOW_Register_Profession]";

                dt = new DBHelper().GetFillData(sql);
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("专业信息表传输失败。", ex);
            }
            dt.TableName = "zczyb";
            return dt;

        }

        /// <summary>
        /// 建立安全连接
        /// </summary>
        private void CreateLink()
        {
            if (this._DBUploader == null)
            {
                this._DBUploader = new zxjaUpdater.DBUploaderSoapClient(); //实例化WebService实例
            }

            RASEncryptLib _RASEncryptLib = new RASEncryptLib();
            //调用开始，先随机生成公/钥对
            this.G_SecurityKeyword = _RASEncryptLib.RSAKey();

            PostParameter _PostParameter = new PostParameter();
            _PostParameter.LicenseID = g_LicenseID;
            _PostParameter.ActionCode = "0-001";
            _PostParameter.Param = this.G_SecurityKeyword.PublicKey;

            //调用webservice
            byte[] _result = _DBUploader.DoAction(this.GetByte(_PostParameter), null, null);

            //数据下载完成后解压、反序列化
            zxja.Interface.Buisness.ReturnObject _ReturnObject =
            zxja.Interface.Buisness.Serializatior.UnSerialBinaryToObject<zxja.Interface.Buisness.ReturnObject>(zxja.Interface.Buisness.Compressor.UnCompress(_result));

            //是否有服务器端异常
            if (!_ReturnObject.IfException)  //无异常
            {
                byte[] _enBytes = _ReturnObject.DsBytes; //这里就是返回来的数据加密密钥了;得用私钥解密

                byte[] _denBytes = _RASEncryptLib.RSADecrypt(this.G_SecurityKeyword.PrivateKey, _enBytes);
                //解密后，解压缩，反序列化
                //注：得到数据加密密钥后,以后上传建造师数据时就使用这个密钥,公私钥就没用了
                this.G_SecurityKeyword = Serializatior.UnSerialBinaryToObject<SecurityKeyword>(Compressor.UnCompress(_denBytes));

            }
            else //服务器端发生异常
            {
                FileLog.WriteLog(string.Format("建立安全连接失败，错误描述：{0}", _ReturnObject.ExceptionString), null);
            }
        }

        #endregion

        //获取继续教育合格证
        private void Down_JXJY()
        {
            return;//已停止
            //try
            //{
            //    DownLoadFile("http://116.213.192.69/ejjxjy.csv", "D://WebRoot/ZYRYJG/Upload/continue.csv");
            //    WriteOperateLog("系统服务", 0, "获取继续教育同步文件ejjxjy.csv", "");
            //}
            //catch (Exception ex)
            //{
            //    FileLog.WriteLog("获取继续教育同步文件ejjxjy.csv错误：" + ex.Message, ex);
            //}
        }

        ///// <summary>
        ///// 人员资格系统电子证书PDF签章（部署在192.168.150.175)
        ///// </summary>
        //public void IssueCA_beijing()
        //{
        //    IssueCA_JZS();
        //    IssueCA_Certiticate();
        //}



        /// <summary>
        /// 获取企业资质(已作废，改为数据库作业从中台获取数据)
        /// </summary>
        public void GetQYZZ()
        {
            return;
            //            int r0 = this.Get_QYZZZS();
            //            int r1 = this.Get_WDZZZS();
            //            int r2 = this.Get_SJSGYTH();
            //            int r3 = this.Get_View_SGQYQ_RYKW();//资质详细
            //            this.Get_QYZZZS_TESHU();//

            //            try
            //            {

            //                int count = CommonDAL.SelectRowCount(@"dbo.QY_BWDZZZS_ING", "");

            //                if (count < 5000
            //                    || r0 == 0
            //                    || r1 == 0
            //                    || r2 == 0
            //                    )
            //                {
            //                    string mes = "";
            //                    if (r0 == 0)
            //                    {
            //                        mes += "本地建筑施工企业0条；";
            //                    }
            //                    if (r1 == 0)
            //                    {
            //                        mes += "外地进京（含中央在京）0条；";
            //                    }
            //                    if (r2 == 0)
            //                    {
            //                        mes += "设计施工一体化0条";
            //                    }
            //                    WriteOperateLog("系统服务", 0, "获取企业资质信息失败", string.Format("获取数据量不正常，请检查相关服务。{0}", mes));
            //                }
            //                else
            //                {

            //                    string delsql = @"truncate table dbo.QY_BWDZZZS ;
            //                                    INSERT INTO DBO.QY_BWDZZZS (QYLB,ZZJGDM,QYMC ) SELECT QYLB,ZZJGDM,QYMC FROM DBO.QY_BWDZZZS_ING;";
            //                    CommonDAL.ExecSQL(delsql);

            //                    WriteOperateLog("系统服务", 0, string.Format("获取企业资质信息{0}条", Convert.ToString(r0 + r1 + r2 + r3))
            //                    , string.Format("本地建筑施工企业{0}条；外地进京（含中央在京）{1}条；设计施工一体化{2}条；企业资质详细{3}条。"
            //                    , r0.ToString(), r1.ToString(), r2.ToString(), r3.ToString()));
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("获取企业资质信息失败，错误信息：" + ex.Message, ex);
            //            }
        }

        /// <summary>
        /// 上传人员考务文件到DFS
        /// </summary>
        private void Upload_FiletoDFS()
        {
            return;

            //            //1、上传新放号证书一寸免冠照片
            //            //2、上传变更照片证书一寸照片
            //            string mes = "";//错误信息
            //            string uName = descObj.EncryptString("RTDL_RYKWXT");
            //            string uPassword = descObj.EncryptString("RTDL_2013");
            //            string serviceID = "6a55b751-824e-49ae-a0b0-57e85e67c79b";
            //            //string ExamWebRoot = System.Configuration.ConfigurationManager.AppSettings["ExamWebRoot"];//网站根目录

            //            DataTable dt;//待上传的文件信息

            //            string sql = @" select top 5000 *
            //                            FROM dbo.certificate                    
            //                            where UpSJZXTime is  null and len(facephoto) >1
            //                            union
            //                            select c.*
            //                            FROM DBO.CERTIFICATECHANGE ch
            //                            inner join dbo.certificate c
            //                            on ch.certificateid = c.certificateid
            //                            where ch.IFUPDATEPHOTO=1 and ch.NOTICEDATE > c.upsjzxtime
            //                            order by certificateid";
            //            try
            //            {
            //                dt = CommonDAL.GetDataTable(sql);
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("获取要同步的文件失败。", ex);
            //                return;
            //            }
            //            try
            //            {
            //                FileService.FileServiceSoapClient fs = new FileService.FileServiceSoapClient();
            //                DataSet ds = null;
            //                mes = fs.GetIServiceStruct(out ds, uName, uPassword, serviceID);
            //                if (mes != "成功")
            //                {
            //                    FileLog.WriteLog(string.Format("同步人员考务证书一寸免冠照片到数据中心失败，错误信息：{0}", mes), null);
            //                    return;
            //                }

            //                foreach (DataRow dr in dt.Rows)
            //                {
            //                    ds.Tables[0].Rows.Clear();
            //                    DataRow newRow = ds.Tables[0].NewRow();
            //                    ds.Tables[0].Rows.Add(newRow);

            //                    newRow["FileShowName"] = string.Format("{0}.jpg", dr["CertificateCode"]);
            //                    newRow["FileFullName"] = string.Format("{0}.jpg", dr["CertificateCode"]);
            //                    newRow["FileID"] = dr["certificateid"];
            //                    newRow["CertificateCode"] = dr["CertificateCode"];
            //                    newRow["WorkerCertificateCode"] = dr["WorkerCertificateCode"];
            //                    newRow["FileClass"] = "从业人员附件";
            //                    newRow["FileSubClass"] = "证书一寸免冠照片";

            //                    FileLog.WriteLog(string.Format("准备同步文件{0}。", dr["facephoto"]), null);

            //                    byte[] fbyte = Utility.ImageHelp.FileToByte(dr["facephoto"].ToString().Replace("~", ExamWebRoot));

            //                    try
            //                    {
            //                        mes = fs.UploadFileWithInfo(uName, uPassword, serviceID, ds, fbyte);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        FileLog.WriteLog("文件同步状态失败。", ex);
            //                        continue;
            //                    }

            //                    if (mes != "成功")
            //                    {
            //                        FileLog.WriteLog(string.Format("同步文件{0}失败，错误信息：{1}", dr["facephoto"], mes), null);
            //                        continue;
            //                    }
            //                    else
            //                    {
            //                        try
            //                        {
            //                            CommonDAL.ExecSQL(string.Format("update dbo.certificate set UpSJZXTime='{0}' where certificateid='{1}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dr["certificateid"]));
            //                            FileLog.WriteLog(string.Format("同步文件{0}成功。", dr["facephoto"]), null);
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            FileLog.WriteLog(string.Format("更新文件{0}同步状态失败。", dr["facephoto"]), ex);
            //                            continue;
            //                        }
            //                    }

            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                FileLog.WriteLog("文件同步状态失败。", ex);
            //            }


        }

        //将本地日志文件入数据库
        private void InsertInterFaceLog()
        {
            ImportAutoChangeCheckLog();
            ImportFileShareLog();
        }

        /// <summary>
        /// 导入批量自动变更审批通过日志数量统计到数据库
        /// </summary>
        private void ImportAutoChangeCheckLog()
        {
            string sql = @"Delete from DBO.TJ_AUTO_CHANGECHECK where TJDATE >='{0}';
                            INSERT INTO DBO.TJ_AUTO_CHANGECHECK (TJDATE,TJDATA )
                            select CONVERT(varchar(10), LOGTIME, 20),
                            sum(cast(replace(right(LOGDETAIL,len(LOGDETAIL) -CHARINDEX('证书数量：',LOGDETAIL) -4),'本。','') as integer))                          
                            FROM DBO.OPERATELOG
                            where LOGTIME >'{0}'and 
                            OPERATENAME like '审查决定证书变更%' and LOGDETAIL like '%社保合格%'
                            group  by  CONVERT(varchar(10), LOGTIME, 20)";
            try
            {
                CommonDAL.ExecSQL(string.Format(sql, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("导入批量变更审批日志失败！，错误信息：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 导入文件共享日志到数据库
        /// </summary>
        private void ImportFileShareLog()
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            string dbNewdt = GetInterFaceLogNewDate();
            List<object> list;

            try
            {
                list = GetUserOperateLogDataList(dt);
                //集合为空，结束
                if (list.Count <= 0)
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("记录操作日志失败！，错误信息：" + exp.Message, exp);
                //出现异常，记录，并结束
                return;
            }

            var db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                //数据库中没有数据
                if (string.IsNullOrEmpty(dbNewdt))
                {
                    CommonDAL.InsertPatch(tran, list, "DBO.INTERFACELOG",
                        @"ACCESSDATE,ACCESSTIME,ACCESSUSER,SERVERID,CALLINGMETHODNAME,PARAMETERDATA,METHODDESCRIPTION");
                    tran.Commit();
                }
                else
                {
                    TimeSpan ts = Convert.ToDateTime(dbNewdt) - Convert.ToDateTime(dt);

                    //dbNewdt<dt
                    if (ts.TotalMinutes >= 0)
                    {
                        //先删除  后插入
                        string cmdtxt = String.Format(@"DELETE DBO.INTERFACELOG    WHERE  ACCESSDATE='{0}'", dt);
                        new DBHelper().ExcuteNonQuery(cmdtxt);
                    }
                    CommonDAL.InsertPatch(tran, list, "DBO.INTERFACELOG",
                        @"ACCESSDATE,ACCESSTIME,ACCESSUSER,SERVERID,CALLINGMETHODNAME,PARAMETERDATA,METHODDESCRIPTION");
                    tran.Commit();
                }
            }
            catch (Exception exp)
            {
                tran.Rollback();
                FileLog.WriteLog("记录操作日志失败！，错误信息：" + exp.Message, exp);
            }
        }

        #endregion

        #region   南静添加规委档案数据  2021-02-01
        protected void GuiWeiData()
        {
            //string sql = "Select * from CJDAG_To_ZJW.cjdagtojw.Pre_BuildCJGTOJW";
            string sql = "Select * from [CJDAG_To_ZJW].[cjdagtojw].[Pre_BuildCJGTOJW]";
            string sql1 = "select getdate()";
            //DataTable dt = new DBHelper("DBConnStr1").GetFillData(sql);
            try
            {
                DataTable dt6 = new DBHelper("DBConnStr1").GetFillData(sql1);
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    FileLog.WriteLog("规委连接成功,失败原因：");
                    if (dt6.Rows.Count > 0)
                    {

                        var ll = dt6.Rows[0][0].ToString();
                        FileLog.WriteLog("规委连接成功,失败原因：" + ll);
                    }
                }
                DataTable dt = new DBHelper("DBConnStr1").GetFillData(sql);
                DataTable dt1 = GetTableSchema();
                if (dt != null && dt.Rows.Count > 0)
                {
                    try
                    {
                        FileLog.WriteLog("规委取值成功：" + dt.Rows[0][0].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                try
                                {
                                    DataRow r = dt1.NewRow();
                                    //r[0] = item["Id"].ToString();
                                    r[1] = item["ProjectAssessNo"] == DBNull.Value ? null : item["ProjectAssessNo"].ToString();
                                    r[2] = item["CheckOrg"] == DBNull.Value ? null : item["CheckOrg"].ToString();
                                    r[3] = item["AssessRegisterDate"] == DBNull.Value ? null : item["AssessRegisterDate"].ToString();
                                    r[4] = item["BuildCompany"] == DBNull.Value ? null : item["BuildCompany"].ToString();
                                    r[5] = item["ProjectName"] == DBNull.Value ? null : item["ProjectName"].ToString();
                                    r[6] = item["ProjectLocation"] == DBNull.Value ? null : item["ProjectLocation"].ToString();
                                    r[7] = DateTime.Now.ToString();
                                    r[8] = item["Id"] == DBNull.Value ? null : item["Id"].ToString();
                                    r[9] = item["AreaFlag"] == DBNull.Value ? null : item["AreaFlag"].ToString();
                                    dt1.Rows.Add(r);
                                }
                                catch (Exception ex)
                                {
                                    FileLog.WriteLog("赋值失败：" + ex.Message + ":" + item[0] + item[1] + item[2] + item[3] + item[4] + item[5]);
                                }

                            }
                        }
                        if (dt1.Rows.Count > 0)
                        {
                            sql = "delete from [XSZX_DSJPT_R].[dbo].[GC_DAG_YYS]";
                            try
                            {
                                var isok = new DBHelper("DBConnStr2").GetExcuteNonQuery(sql);
                                FileLog.WriteLog("109删除成功");
                                try
                                {
                                    new DBHelper("DBConnStr2").SqlBulkCopyByDatatable("GC_DAG_YYS", dt1);
                                    FileLog.WriteLog("109写入成功");
                                }
                                catch (Exception ex)
                                {
                                    FileLog.WriteLog("规委档案数据批量写入失败,失败原因：" + ex.Message, ex);
                                }
                            }
                            catch (Exception ex)
                            {

                                FileLog.WriteLog("109规委档案数据删除失败,失败原因：" + ex.Message, ex);
                            }
                        }

                        //sql = "update Pre_BuildCJGTOJW set IsToJWFlag=1";
                        //var isok = new DBHelper("DBConnStr1").GetExcuteNonQuery(sql);

                    }
                    catch (Exception ex)
                    {
                        FileLog.WriteLog("规委档案数据获取失败,失败原因：" + ex.Message, ex);
                        //throw;
                    }

                }
                else
                {
                    FileLog.WriteLog("规委已正常连接,查询数据为空");
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("规委档案数据获取失败,失败原因：" + ex.Message, ex);
            }


        }
        public static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{  
             new DataColumn("ID",typeof(string)),  
             new DataColumn("YYSYJSH",typeof(string)),  
             new DataColumn("SPBM",typeof(string)),
             new DataColumn("SPSJ",typeof(string)),
             new DataColumn("JSDW",typeof(string)),
             new DataColumn("XMMC",typeof(string)),
             new DataColumn("JSWZ",typeof(string)),
             new DataColumn("CJSJ",typeof(DateTime)),
             new DataColumn("danganid",typeof(string)),
             new DataColumn("AreaFlag",typeof(string))
            });

            return dt;
        }



        #endregion

        #region 二级造价工程师业务

        /// <summary>
        /// 生成二级造价工程师电子证书
        /// </summary>
        public void CreateZJS_CA()
        {
            //**********************************************************************************************************
            //需要改造成
            //1、生成代签名的无签章证书pdf；
            //2、发送pdf文件到109服务器 D:\\zzk\ERJIAN_CA\DGZ\{0}.pdf；
            //3、109上服务调用接口签章（已完成）；
            //4、取回pdf文件到人员系统D:\WebRoot\CAFile\XXX\GUID.pdf
            //**********************************************************************************************************
            //            string sql = String.Format(@"Select top {0} * FROM [dbo].[zjs_Certificate]
            //                                        where ([ApplyCATime] <[CJSJ] or [ApplyCATime] <[XGSJ] or [ApplyCATime] is null) and [PSN_RegisteType] <7
            //                                        and PSN_CertificateNO >'{1}'
            //                                         and ([PSN_RegisterNO]='建[造]21221100000032' or [PSN_RegisterNO]='建[造]24221100000065')
            //                                        order by PSN_CertificateNO", MaxCountExe, cursor_str["CreateCA_Cur_PSN_CertificateNO_ZJS"]);

            string sql = String.Format(@"Select top {0} * FROM [dbo].[zjs_Certificate]
                                        where ([ApplyCATime] <[CJSJ] or [ApplyCATime] <[XGSJ] or [ApplyCATime] is null) and [PSN_RegisteType] <7
                                        and PSN_CertificateNO >'{1}'
                                        order by PSN_CertificateNO", MaxCountExe, cursor_str["CreateCA_Cur_PSN_CertificateNO_ZJS"]);

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count < MaxCountExe)
            {
                cursor_str["CreateCA_Cur_PSN_CertificateNO_ZJS"] = "";//记录扫描位置
                if (dtOriginal.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_str["CreateCA_Cur_PSN_CertificateNO_ZJS"] = Convert.ToString(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"]);//记录扫描位置
            }

            var template_url = string.Format(@"{0}\Template\二级造价师注册证书.pdf", ExamWebRoot);
            var save_pdf_url = "";//pdf生成位置
            string CertificateCAID = "";
            //string fileTo = "";

            foreach (DataRow dr in dtOriginal.Rows)
            {
                if (dr["PSN_CertificateNO"].ToString().Length == 18 && Utility.Check.isChinaIDCard(dr["PSN_CertificateNO"].ToString()) == false)
                {
                    continue;//身份证错误的不生成电子证书，签章服务不接收
                }

                //创建pdf
                CertificateCAID = Guid.NewGuid().ToString();

                //pdf目录
                if (!Directory.Exists(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot)))
                {
                    System.IO.Directory.CreateDirectory(string.Format(@"{0}\UpLoad\pdf\up\", ExamWebRoot));
                }

                save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, CertificateCAID);//目标文件地址


                try
                {
                    var dic = ReadForm(template_url);//读取模板
                    FillFormOfZJS(template_url, save_pdf_url, dic, dr);//填充模板
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级造价工程师电子证书失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "创建", ex.Message);
                    continue;
                }

                try
                {
                    //更新证书表,写入申请时间
                    CommonDAL.ExecSQL(string.Format(@"update  DBO.[zjs_Certificate] set [ApplyCATime]='{1}',[CertificateCAID]='{2}',[ReturnCATime] = null,[SendCATime] = null where [PSN_ServerID]='{0}';", dr["PSN_ServerID"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CertificateCAID));
                    FileLog.WriteLog(string.Format("创建二级造价工程师电子证书{0}.pdf成功。", CertificateCAID));
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("生成二级造价工程师电子证书信息失败，错误信息：" + ex.Message, ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// 发送带签章二级造价工程师证书到5.21
        /// </summary>
        public void SendZJS_CA()
        {
            string save_pdf_url = "";//目标文件地址

            string sql = String.Format(@"Select top {0} PSN_ServerID,CertificateCAID
                                        from DBO.[zjs_Certificate] 
                                        Where   [ApplyCATime] >'2018-01-01' and [SendCATime] is null
                                        order by [XGSJ] desc ", MaxCountExe);
            //　and CertificateCAID='643ef6fa-67b9-4720-8461-1fe3a7a5b101'----测试专用

            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

            if (dtOriginal.Rows.Count == 0) return;

            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {
                    save_pdf_url = string.Format(@"{0}\UpLoad\pdf\up\{1}.pdf", ExamWebRoot, dr["CertificateCAID"]);//目标文件地址
                    File.Copy(save_pdf_url, string.Format(@"{0}\{1}.pdf", @"\\192.168.150.175\zzk\ERJIAN_CA\DGZ", dr["CertificateCAID"]), true);//替换文件

                    //更新证书表,写入发送DFS时间
                    CommonDAL.ExecSQL(string.Format("update  DBO.zjs_Certificate set [SendCATime]='{1}' where [PSN_ServerID]='{0}'", dr["PSN_ServerID"], DateTime.Now));
                    FileLog.WriteLog(string.Format("同步待签章的二级造价工程师证书{0}.pdf到109成功。", dr["CertificateCAID"]));

                    //删除临时文件pdf 
                    File.Delete(save_pdf_url);
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("同步待签章的二级造价工程师证书{0}.pdf到109失败。", dr["CertificateCAID"]), ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "发送", ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 取回已签章的二级造价工程师证书
        /// </summary>
        public void GetReturnZJS_CA()
        {
            try
            {
                //获取尚未成功签章证书信息
                string sql = "";
                DataTable dtOriginal = null;
                string fileFrom = "";//签章结果在109地址
                string fileTo = "";
               
                sql = String.Format(@"Select top {0} * from DBO.zjs_Certificate 
                                        where  [SendCATime] < dateadd(MINUTE,2,[SignCATime]) and [ReturnCATime] is null and [PSN_CertificateNO] >'{1}' and [SignCATime] <'{2}'
                                        order by [PSN_CertificateNO]", MaxCountExe, cursor_str["ReturnCA_Cur_PSN_CertificateNO_ZJS"], DateTime.Now.AddMinutes(-5));
                dtOriginal = (new DBHelper()).GetFillData(sql);
                //FileLog.WriteLog(sql);
                if (dtOriginal.Rows.Count < MaxCountExe)
                {
                    cursor_str["ReturnCA_Cur_PSN_CertificateNO_ZJS"] = "";//记录扫描位置
                    if (dtOriginal.Rows.Count == 0)
                    {
                        return;
                    }
                }
                else
                {
                    cursor_str["ReturnCA_Cur_PSN_CertificateNO_ZJS"] = dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"].ToString();//记录扫描位置
                }
                fileFrom = @"\\192.168.150.175\zzk\ERJIAN_CA\GZJG\";

                foreach (DataRow dr in dtOriginal.Rows)
                {
                    try
                    {
                        if (File.Exists(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"])) == true)
                        {
                            fileTo = string.Format(@"{0}\{1}", CAFile, dr["CertificateCAID"].ToString().Substring(dr["CertificateCAID"].ToString().Length - 3, 3));
                            if (!Directory.Exists(fileTo))
                            {
                                System.IO.Directory.CreateDirectory(fileTo);
                            }
                            fileTo = string.Format(@"{0}\{1}.pdf", fileTo, dr["CertificateCAID"]);

                            File.Copy(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), fileTo, true);//替换文件

                            File.Move(string.Format("{0}{1}.pdf", fileFrom, dr["CertificateCAID"]), string.Format("{0}{1}.pdf", fileFrom.Replace("GZJG", "BAK_GZJG"), dr["CertificateCAID"]));

                            CommonDAL.ExecSQL(string.Format(@"UPDATE [dbo].[zjs_Certificate] SET [ReturnCATime] = getdate() WHERE [CertificateCAID]='{0}'  and [ReturnCATime] is null;
                                                         if not exists(select 1 from [dbo].[zjs_CertificateCAHistory] where [CertificateCAID]='{0}') 
                                                        INSERT INTO [dbo].[zjs_CertificateCAHistory] ([CertificateCAID],[ApplyCATime],[ReturnCATime],[PSN_ServerID])
                                                        select [CertificateCAID],ApplyCATime,ReturnCATime,PSN_ServerID from DBO.[zjs_Certificate] where [CertificateCAID]='{0}';", dr["CertificateCAID"]));

                            FileLog.WriteLog(string.Format("回写已签章的二级造价工程师证书{0}成功。", fileTo));
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "取回", ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("取回已签章的二级造价工程师证书失败。", ex);
            }
        }

        /// <summary>
        /// 二级造价工程师证书PDF签章（部署在192.168.150.175)
        /// </summary>
        public void IssueCA_ZJS()
        {
            if (IssueCA_zjsing == true)
            {
                return;
            }
            else
            {
                IssueCA_zjsing = true;
            }

            //证照名称：二级造价工程师（土木建筑工程）注册证书
            //目录实施码：100077001000021135110000
            //事项名称：二级造价工程师执业资格认定
            //事项编码：11110000000021135M200011705600001
            //印章编码：DZYZ000021135etmOeZ
            //--------------------------------------------------------
            //证照名称：二级造价工程师（安装工程）注册证书
            //目录实施码：100077101000021135110000
            //事项名称：二级造价工程师执业资格认定
            //事项编码：11110000000021135M200011705600001
            //印章编码：DZYZ000021135etmOeZ

            string save_pdf_url = "";//目标文件地址
            string itemCode_tujian = "100077001000021135110000";//目录实施码
            string itemCode_anzhuang = "100077101000021135110000";//目录实施码
            string licenseCode = "";//电子证照标识码
            string idCode = "";//证照号码
            string serviceItemCode = "11110000000021135M200011705600001";//事项编码
            string serviceItemName = "二级造价工程师执业资格认定";//事项名称

            DataTable dtOriginal = null;
            try
            {
                string sql = String.Format(@"
                                        Select top {0}  
                                        ca_count = (select count(*) from [zjs_CertificateCAHistory] where [PSN_ServerID]=c.[PSN_ServerID]),
                                        c.* FROM [dbo].[zjs_Certificate] c                                     
                                        where (c.[SendCATime] > dateadd(MINUTE,2,c.[SignCATime]) or (c.[SendCATime] >c.[ApplyCATime] and c.[SignCATime] is null)) and c.[PSN_RegisteType] <7  
                                        and c.PSN_CertificateNO >'{1}'  and c.[SendCATime] <'{2}'                                                                           
                                        order by c.PSN_CertificateNO", MaxCountExe, cursor_str["IssueCA_Cur_PSN_CertificateNO_ZJS"], DateTime.Now.AddMinutes(-5));
                
                dtOriginal = (new DBHelper()).GetFillData(sql);

                if (dtOriginal.Rows.Count < MaxCountExe)
                {
                    cursor_str["IssueCA_Cur_PSN_CertificateNO_ZJS"] = "";//记录扫描位置
                    if (dtOriginal.Rows.Count == 0)
                    {
                        IssueCA_zjsing = false;
                        return;
                    }
                }
                else
                {
                    cursor_str["IssueCA_Cur_PSN_CertificateNO_ZJS"] = Convert.ToString(dtOriginal.Rows[dtOriginal.Rows.Count - 1]["PSN_CertificateNO"]);//记录扫描位置
                }

                //登录
                if (IssueCA_accessToken == "" || IssueCA_accessTime < DateTime.Now.AddMinutes(-60))
                {
                    string accessToken = Api.Execute.Login();//登录

                    if (string.IsNullOrWhiteSpace(accessToken))//登录失败
                    {
                        IssueCA_accessToken = "";
                        FileLog.WriteLog(string.Format("{0}登录市电子签章系统失败。", DateTime.Now));
                        IssueCA_zjsing = false;
                        return;
                    }
                    else
                    {
                        IssueCA_accessToken = accessToken;
                        IssueCA_accessTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("登录市电子签章系统失败。", ex);
                IssueCA_zjsing = false;
                return;
            }

            foreach (DataRow dr in dtOriginal.Rows)
            {
                try
                {
                    //FileLog.WriteLog(string.Format("{0}准备签章{1}。",DateTime.Now,dr["PSN_RegisterNO"]));
                    if (dr["license_code"] != DBNull.Value && dr["license_code"] != null)
                    {
                        licenseCode = dr["license_code"].ToString();
                        idCode = dr["PSN_RegisterNO"].ToString();

                        //废弃
                        //AbolishResponseResult abolishResult = Execute.Abolish(accessToken, dr);
                        AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken
                            , (dr["PSN_RegisteProfession"].ToString() == "土木建筑工程" ? itemCode_tujian : itemCode_anzhuang)
                            , licenseCode, idCode, serviceItemCode, serviceItemName);
                        if (abolishResult.AckCode != "SUCCESS")
                        {
                            FileLog.WriteLog(string.Format("废弃证照号码：{0}的PDF失败！错误：{1}", dr["PSN_RegisterNO"], JSON.Encode(abolishResult.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "废止", JSON.Encode(abolishResult.Errors));

                            if (JSON.Encode(abolishResult.Errors).Contains("找不到需要废止的电子证照"))
                            {
                                CommonDAL.ExecSQL(string.Format("update  DBO.zjs_Certificate set [license_code]=null,[auth_code]=null where PSN_ServerID='{0}'", dr["PSN_ServerID"]));
                            }

                            //continue;
                        }
                        else
                        {
                            //更新证书，废止icense_code电子证照标识码，auth_code电子证照查验码，废止时间为SignCATime
                            CommonDAL.ExecSQL(string.Format("update  DBO.[zjs_Certificate] set [license_code]=null,[auth_code]=null where [PSN_ServerID]='{0}'", dr["PSN_ServerID"]));
                            FileLog.WriteLog(string.Format("成功废弃编号【{0}】电子证书。", dr["PSN_RegisterNO"]));
                        }

                    }

                    save_pdf_url = string.Format(@"D:\\zzk\ERJIAN_CA\GZJG\{0}.pdf", dr["CertificateCAID"]);
                    var model = new CreateRequestMdl
                    {
                        Data = new CreateRequestData { }
                    };

                    model.Data.IniPropoertyZJGCS(dr);

                    //FileLog.WriteLog(string.Format("{0}签章调用开始{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    CreateResponseResult result = Execute.Create(IssueCA_accessToken
                        , (dr["PSN_RegisteProfession"].ToString() == "土木建筑工程" ? itemCode_tujian : itemCode_anzhuang)
                        , model);
                    //FileLog.WriteLog(string.Format("{0}签章调用结束{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                    if (result != null)
                    {
                        //签发
                        if (result.AckCode == "SUCCESS" && result.Data != null)
                        {
                            FileLog.WriteLog(string.Format("成功对编号【{0}】电子证书签章！，[license_code]={1}，[auth_code]={2}。", dr["PSN_RegisterNO"], result.Data.LicenseCode, result.Data.AuthCode));

                            //记录license_code电子证照标识码（用于作废），auth_code电子证照查验码（用于下载）
                            CommonDAL.ExecSQL(string.Format("update  DBO.zjs_Certificate set [license_code]='{1}',[auth_code]='{2}',SignCATime=getdate() where PSN_ServerID='{0}'", dr["PSN_ServerID"], result.Data.LicenseCode, result.Data.AuthCode));

                            bool downAgain = false;//是否已经再次尝试下载
                        TrydownAgain://第一次下载失败，隔几秒再次尝试

                            //下载pdf
                            DownResponseResult downResult = Execute.DownPDF(IssueCA_accessToken, result.Data.AuthCode);
                            if (downResult == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "下载", "下载PDF返回结果为Null");
                                continue;
                            }
                            if (downResult.Data == null)
                            {
                                FileLog.WriteLog(string.Format("下载PDF返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(downResult.Errors)));
                                WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "下载", JSON.Encode(downResult.Errors));

                                if (downAgain == false && JSON.Encode(downResult.Errors).Contains("授权对象不存在该电子证照"))
                                {
                                    Thread.Sleep(5000);//暂停5秒
                                    downAgain = true;
                                    goto TrydownAgain;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                Common.SystemSection.CreateFileByBase64String(save_pdf_url, downResult.Data.FileData);

                                ////更新证书表,写入发送DFS时间
                                //CommonDAL.ExecSQL(string.Format("update  DBO.CERTIFICATE set [SendCATime]='{1}' where CertificateID={0}", dr["CertificateID"], DateTime.Now));
                                FileLog.WriteLog(string.Format(@"成功下载已签章编号【{0}】电子证书{1}.pdf。", dr["PSN_RegisterNO"], dr["CertificateCAID"]));

                                //移动已处理未盖章文件
                                File.Move(save_pdf_url.Replace("GZJG", "DGZ"), save_pdf_url.Replace("GZJG", "BAK_DGZ"));
                                //FileLog.WriteLog(string.Format("{0}下载签章结果完毕{1}。", DateTime.Now, dr["PSN_RegisterNO"]));
                            }
                        }
                        else
                        {
                            FileLog.WriteLog(string.Format("创建PDF制证数据返回结果Data=Null，证书编号：{0},错误描述：{1}。", dr["PSN_RegisterNO"], JSON.Encode(result.Errors)));
                            WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "签发", JSON.Encode(result.Errors));

                            if (JSON.Encode(result.Errors).Contains("已存在") || JSON.Encode(result.Errors).Contains("存在重复的电子证照"))
                            {
                                try
                                {
                                    AbolishResponseResult abolishResult = Execute.Abolish(IssueCA_accessToken
                                        , (dr["PSN_RegisteProfession"].ToString() == "土木建筑工程" ? itemCode_tujian : itemCode_anzhuang)
                                        , "", dr["PSN_RegisterNO"].ToString(), serviceItemCode, serviceItemName);
                                }
                                catch (Exception ex2)
                                {
                                    FileLog.WriteLog(string.Format("使用证书编号{0}废止证书失败，错误信息：{1}", dr["PSN_RegisterNO"], ex2.Message), ex2);
                                }
                            }
                            continue;
                        }
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("创建PDF制证数据返回结果为Null，证书编号：{0}。", dr["PSN_RegisterNO"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog("创建PDF制证失败，错误信息：" + ex.Message, ex);
                    WriteEleCertError(dr["PSN_RegisterNO"].ToString(), "二级造价工程师", "签发", ex.Message);
                    continue;
                }
            }
            IssueCA_zjsing = false;
            //Api.Execute.LogOut(accessToken);//退出登录
        }

        /// <summary>
        /// 向二级造价工程师pdf模版填充内容，并生成新的文件
        /// </summary>
        /// <param name="pdfTemplate">模版路径</param>
        /// <param name="newFile">生成文件保存路径</param>
        /// <param name="dic">标签字典(即模版中需要填充的控件列表)</param>
        /// <param name="dr">证书数据</param>
        private void FillFormOfZJS(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            //二级造价师电子证书证面上有效期范围开始时间调整为:每次初始注册或者延期注册的最新决定日期，发证日期调整为每次初始注册的最新决定日期。

            dic["PSN_Name"] = dr["PSN_Name"].ToString();//姓名
            dic["PSN_Sex"] = dr["PSN_Sex"].ToString();//性别
            //dic["PSN_CertificateNO"] = dr["PSN_CertificateNO"].ToString();//身份证号
            dic["PSN_BirthDate"] = Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年M月d日");//生日
            dic["ENT_Name"] = dr["ENT_Name"].ToString();//单位
            dic["PSN_RegisterNO"] = dr["PSN_RegisterNO"].ToString();//注册号
            //dic["PSN_CertificationDate"] = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日"); //发证日期（签章日期）
            //dic["PSN_CertificateValidity"] = string.Format("{0}-{1}", Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy年M月d日"), Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy年M月d日"));//有效期
            dic["PSN_CertificationDate"] = Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy年M月d日"); //发证日期（签章日期）
            dic["PSN_CertificateValidity"] = string.Format("{0}-{1}", Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日"), Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy年M月d日"));//有效期
            dic["PSN_RegisteProfession"] = dr["PSN_RegisteProfession"].ToString();//注册专业

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
                string iTextAsianCmaps_Path = string.Format(@"{0}\bin\iTextAsianCmaps.dll", ExamWebRoot);
                string iTextAsian_Path = string.Format(@"{0}\bin\iTextAsian.dll", ExamWebRoot);
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
                image.SetAbsolutePosition(375, 370);
                waterMarkContent.AddImage(image);//加水印

                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["CertificateCAID"], dr["PSN_RegisterNO"]));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/PersonnelFile/CertCheckJZS.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
                imgCode.ScaleAbsolute(71, 71);
                imgCode.SetAbsolutePosition(100, 160);
                waterMarkContent.AddImage(imgCode);//加水印

                //输出签名  
                iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance(GetSignPhotoJZS(dr["PSN_RegisterNO"].ToString(), dr["PSN_CertificateNO"].ToString()));
                img_qm.GrayFill = 100;//透明度，灰色填充
                img_qm.ScaleAbsolute(99, 43);
                img_qm.SetAbsolutePosition(190, 150);
                waterMarkContent.AddImage(img_qm);//加水印

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

        /// <summary>
        /// 上传二级造价工程师注册数据到建设部（部署在192.168.5.49）
        /// </summary>
        public void UploadZJGCS_JSB()
        {
            string apiUrl = "http://zaojiaccea.jianshe99.com/cecaopsys/externalInterface/operate.do?op=userInfoOperate";//上传证书信息接口地址
            string rtnJson = "";//接口调用返回json
            zjgcsUpResultMDL rtn = null;
            zjs_ApplyMDL _zjs_ApplyMDL = null;//最新注册申请
            zjs_QualificationMDL _zjs_QualificationMDL = null;//资格证书

            //获取本地造价咨询企业资质信息
            DataTable dtUnitZJZX = CommonDAL.GetDataTable("select * from jcsjk_QY_ZHXX where SJLX='本地造价咨询企业'");
            DataColumn[] key = new DataColumn[1];
            key[0] = dtUnitZJZX.Columns["ZZJGDM"];
            dtUnitZJZX.PrimaryKey = key;
            DataRow findRow = null;

            //读取待发送的二级建造工程师(电子证书签章时间晚于上传建设部时间)
            string sql = @"select top {0} c.*,u.ENT_Economic_Nature,u.[ENT_OrganizationsCode] as ZZJGDM,u.ENT_Type,u.ENT_Telephone FROM [dbo].[zjs_Certificate] c
                            inner join [dbo].[Unit] u on c.ENT_OrganizationsCode = u.CreditCode
                            where (
                                    (c.[UpJsbTime] is null and [ReturnCATime] is not null) 
                                        or (c.[UpJsbTime]<c.[ReturnCATime])
                                        or (c.[PSN_RegisteType] =7 and (c.[UpJsbTime] is null or c.[UpJsbTime] < c.[PSN_RegistePermissionDate]))
                                    ) and c.[PSN_ServerID] >'{1}' order by c.[PSN_ServerID]";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, MaxCountExe, cursor_str["UploadJSB_ZJGCS_PSN_ServerID"]));

            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_str["UploadJSB_ZJGCS_PSN_ServerID"] = "";//记录扫描位置
                if (dt.Rows.Count == 0)
                {
                    return;
                }
            }
            else
            {
                cursor_str["UploadJSB_ZJGCS_PSN_ServerID"] = Convert.ToString(dt.Rows[dt.Rows.Count - 1]["PSN_ServerID"]);//记录扫描位置
            }

            userInfoOperate info = null;//上传信息分装包
            userInfos u = null;//上传信息
            foreach (DataRow d in dt.Rows)
            {
                try
                {
                    u = new userInfos();
                    u.outUserID = d["PSN_ServerID"].ToString();
                    u.validFlag = (d["PSN_RegisteType"].ToString() == "07" ? 3 : 1);
                    u.userName = d["PSN_Name"].ToString();
                    u.sex = (d["PSN_Sex"].ToString() == "nv" ? 0 : 1);
                    u.birthday = Convert.ToDateTime(d["PSN_BirthDate"]).ToString("yyyy-MM-dd");
                    u.nationID = Utility.Check.GetNationCode(d["PSN_National"].ToString());
                    u.titlesID = "28";
                    u.idCard = d["PSN_CertificateNO"].ToString();
                    u.soldierNumber = "";
                    u.eduLevelID = Utility.Check.GetEduLevelCode(d["PSN_Qualification"].ToString());
                    u.graduateDate = Convert.ToDateTime(d["PSN_GraduationTime"]).ToString("yyyy-MM-dd");
                    u.graduateSchool = d["PSN_GraduationSchool"].ToString();
                    u.majorName = d["PSN_Specialty"].ToString();
                    u.workYear = "";
                    u.enterPriseTypeID = Utility.Check.GetEnterPriseTypeID(d["ENT_Economic_Nature"].ToString());
                    u.areaName = "北京市";

                    switch (d["ENT_City"].ToString())
                    {
                        case "延庆区":
                            u.cityName = "延庆县";//区;
                            break;
                        case "密云区":
                            u.cityName = "密云县";//区;
                            break;
                        case "门头沟区":
                            u.cityName = "门头沟区1";//区;
                            break;
                        case "亦庄":
                            u.cityName = "大兴区";//区;
                            break;
                        default:
                            u.cityName = d["ENT_City"].ToString();//区;
                            break;
                    }
                    u.countyName = "";
                    u.instanceName = "北京市"; //"北京市住房和城乡建设委员会";
                    u.workUnitName = d["ENT_Name"].ToString();


                    u.branchUnit = "";
                    findRow = dtUnitZJZX.Rows.Find(d["ZZJGDM"].ToString());
                    if (findRow != null)
                    {
                        u.empUnitTypeID = "1";
                        u.workUnitNumber = findRow["ZZZSBH"].ToString();
                    }
                    else
                    {
                        u.empUnitTypeID = "2";
                        u.workUnitNumber = "";
                    }

                    _zjs_ApplyMDL = zjs_ApplyDAL.GetObjectLastContract(d["ENT_OrganizationsCode"].ToString());
                    if (_zjs_ApplyMDL != null)
                    {
                        u.employDateStatus = "1";
                        u.employDateBegin = _zjs_ApplyMDL.ENT_ContractStartTime.Value.ToString("yyyy-MM-dd");
                        u.employDateEnd = (_zjs_ApplyMDL.ENT_ContractENDTime.HasValue == true ? _zjs_ApplyMDL.ENT_ContractENDTime.Value.ToString("yyyy-MM-dd") : "");
                    }
                    else
                    {
                        u.employDateStatus = "2";
                        u.employDateBegin = "";
                        u.employDateEnd = "";
                    }

                    u.workUnitCode = d["ENT_OrganizationsCode"].ToString();
                    u.archiveUnitName = "";
                    u.certificateNumber = d["PSN_RegisterNO"].ToString();
                    u.signetNumber = d["PSN_RegisterNO"].ToString().Replace("建[造]", "B");
                    u.registerDate = Convert.ToDateTime(d["PSN_CertificationDate"]).ToString("yyyy-MM-dd");
                    u.invalidDate = Convert.ToDateTime(d["PSN_CertificateValidity"]).ToString("yyyy-MM-dd");
                    u.examNumber = "";
                    u.qualificationNumber = d["ZGZSBH"].ToString();
                    _zjs_QualificationMDL = zjs_QualificationDAL.GetObject(d["ZGZSBH"].ToString());

                    if (_zjs_QualificationMDL != null)
                    {
                        u.agreeDate = _zjs_QualificationMDL.QFSJ.Value.ToString("yyyy-MM-dd");
                        u.examSpecialityID = (_zjs_QualificationMDL.ZYLB.Contains("土木") == true ? "1" : "2");//报考专业	1：土建  2：安装
                        u.examYear = _zjs_QualificationMDL.QFSJ.Value.ToString("yyyy-MM-dd");
                    }

                    u.isEmployed = "1";
                    u.examAreaName = "北京市";
                    u.address = d["END_Addess"].ToString();
                    u.postNumber = "";
                    u.phone = d["ENT_Telephone"].ToString();
                    u.mobile = d["PSN_MobilePhone"].ToString();
                    u.unitMainBusinessID = Utility.Check.GetUnitMainBusinessID(d["ENT_Type"].ToString());
                    u.userRemark = "";
                    u.allPeriod = "0";

                    //封装数据
                    info = new userInfoOperate();
                    info.key = Utility.Cryptography.GetMD5Hash("ek2Uf7cjFUrRhmeS");//加密秘钥
                    //info.userInfos = string.Format(@"[{0}]", Utility.JSONHelp.Encode(u));
                    info.userInfos = string.Format(@"[{0}]", JSON.Encode(u));


                    //发送数据
                    //rtnJson = Utility.HttpHelp.DoPost(apiUrl, Utility.JSONHelp.Encode(info));
                    rtnJson = Utility.HttpHelp.DoPost(apiUrl, JSON.Encode(info));

                    //FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}。返回结果：{1}", d["PSN_RegisterNO"], rtnJson));

                    //解析返回结果
                    rtn = Newtonsoft.Json.JsonConvert.DeserializeObject<zjgcsUpResultMDL>(rtnJson);

                    if (rtn.code == "1")
                    {
                        zjs_CertificateDAL.UpdateUpJsbTime(d["PSN_ServerID"].ToString());//更新发送时间

                        FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}成功。", d["PSN_RegisterNO"]));
                    }
                    else
                    {
                        FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}失败：{1}", d["PSN_RegisterNO"], rtn.msg));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("上传二级注册造价工程师{0}失败。", d["PSN_RegisterNO"]), ex);
                }
            }

        }

        #endregion

        #region 办件汇聚

        /// <summary>
        /// 新版办件汇聚
        /// </summary>
        private void ApplyGather()
        {
            //比对开始时间            
            ApplyGatherStartTime = DateTime.Now;

            //testGather(ApplyGatherStartTime);

            switch (ApplyGatherStep)
            {
                case "ApplyGatherCertChange":
                    ApplyGatherStep = "ApplyGatherCertContinue";
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    break;
                case "ApplyGatherCertContinue":
                    ApplyGatherStep = "ApplyGatherCertEnter";
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    break;
                case "ApplyGatherCertEnter":
                    ApplyGatherStep = "ApplyGatherCertMore";
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    break;
                case "ApplyGatherCertMore":
                    ApplyGatherStep = "ApplyGatherExamSignup";
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    break;
                case "ApplyGatherExamSignup":
                    ApplyGatherStep = "ApplyGatherZJGCS";
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    break;
                case "ApplyGatherZJGCS":
                    ApplyGatherStep = "ApplyGatherJZS";
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    break;
                case "ApplyGatherJZS":
                    ApplyGatherStep = "ApplyGatherCertChange";
                    ApplyGatherJZS(ApplyGatherStartTime);
                    ApplyGatherCertChange(ApplyGatherStartTime);
                    ApplyGatherCertContinue(ApplyGatherStartTime);
                    ApplyGatherCertEnter(ApplyGatherStartTime);
                    ApplyGatherCertMore(ApplyGatherStartTime);
                    ApplyGatherExamSignup(ApplyGatherStartTime);
                    ApplyGatherZJGCS(ApplyGatherStartTime);
                    break;
            }
        }

        /// <summary>
        /// 办件汇聚事项赋码测试，每个事项测试1条
        /// </summary>
        /// <param name="startTime"></param>
        private void testGather(DateTime startTime)
        {
            string sql =@"select *
                            FROM DBO.[temp_Gather] 
                            where  [ItemData] is null  and [GatherResult] is null ";

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
           
            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {              
                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = "7b60ce3c-47bf-31f4-be47-f586b1f98104";//  申请人UID
                    r.applyCardNumber = "210504197705200015";//  申请者证件号码
                    r.applyCardType = "31";//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = "测试";//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)
                    r.serviceCodeId = dr["ItemCode"].ToString();                          


                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                   
                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("update  [dbo].[temp_Gather] set [ItemData]='{1}',[GatherResult]='成功' where [ItemCode]='{0}'", dr["ItemCode"],p.data));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("update  [dbo].[temp_Gather] set [GatherResult]='{1}' where [ItemCode]='{0}'", dr["ItemCode"],  p.resultMsg));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【test1】ItemCode={0}", dr["ItemCode"]), ex);
                }
            }
            
        }
        

        /// <summary>
        /// 从业人员变更、离京、注销办件汇聚赋码
        /// </summary>
        private void ApplyGatherCertChange(DateTime startTime)
        {

            string sql = String.Format(@"select top {0} A.CERTIFICATECHANGEID,A.CHANGETYPE,A.PostID,A.CERTIFICATECHANGEID,A.WORKERCERTIFICATECODE,A.WORKERNAME,w.[UUID]
                                        FROM DBO.[VIEW_CERTIFICATECHANGE] A
                                        inner join [WORKER] w on A.NEWWORKERCERTIFICATECODE = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='CertificateChange' and A.[CERTIFICATECHANGEID]= g.BusID                                    
                                        where  A.[ApplyDate] >'2025-08-02' and A.CheckDate > dateadd(day,-7,getdate()) and A.CheckResult = '通过'                                   
                                        and A.CERTIFICATECHANGEID > {1}  and w.[UUID] is not null 
                                        and (A.posttypeid =1 or (A.posttypeid = 2 and A.postID <> 299 and A.CHANGETYPE='注销'))
                                        and g.BusID is null    
                                        order by A.CERTIFICATECHANGEID ", MaxCountExe, cursor_long["cursor_BJHJ_cy_change"]);
    
            DataTable dt = (new DBHelper()).GetFillData(sql);

             if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_BJHJ_cy_change"] = 0;//记录扫描位置
                return;
            }

             FileLog.WriteLog(string.Format("准备{0}条从业人员变更汇聚赋码：ApplyGatherCertChange()",dt.Rows.Count));

             Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                     cursor_long["cursor_BJHJ_cy_change"] = Convert.ToInt64(dr["CERTIFICATECHANGEID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)

                    switch (dr["CHANGETYPE"].ToString())
                    {
                        case "京内变更":
                            switch (dr["PostID"].ToString())
                            {
                                case "147":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_省内变更受聘企业;
                                    break;
                                case "148":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_省内变更受聘企业;
                                    break;
                                case "6":
                                case "1123":
                                case "1125":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_省内变更受聘企业;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "离京变更":
                            switch (dr["PostID"].ToString())
                            {
                                case "147":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_跨省变更受聘企业;
                                    break;
                                case "148":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_跨省变更受聘企业;
                                    break;
                                case "6":
                                case "1123":
                                case "1125":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_跨省变更受聘企业;
                                    break;                                
                                default:
                                    break;
                            }
                            break;
                        case "注销":
                            switch (dr["PostID"].ToString())
                            {
                                case "147":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_证书注销;
                                    break;
                                case "148":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_证书注销;
                                    break;
                                case "6":
                                case "1123":
                                case "1125":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_证书注销;
                                    break;     

                                case "120":	//建筑电工
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑电工_注销;
                                    break;
                                case "123":	//建筑架子工（普通脚手架）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_注销;
                                    break;
                                case "126":	//建筑起重司索信号工
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重司索信号工_注销;
                                    break;
                                case "129":	//建筑起重机械司机（塔式起重机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_注销;
                                    break;
                                case "132":	//建筑起重机械司机（施工升降机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_注销;
                                    break;
                                case "135":	//建筑起重机械司机（物料提升机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_注销;
                                    break;
                                case "138":	//建筑起重机械安装拆卸工（塔式起重机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_注销;
                                    break;
                                case "141":	//建筑起重机械安装拆卸工（施工升降机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_注销;
                                    break;
                                case "144":	//高处作业吊篮安装拆卸工
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_注销;
                                    break;
                                case "151":	//建筑架子工（附着升降脚手架）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_注销;
                                    break;
                                case "152":	//建筑起重机械安装拆卸工（物料提升机）
                                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_注销;
                                    break;
                                //case "299":	//汽车起重机司机
                                //    r.serviceCodeId = ;
                                //    break;
                                default:
                                    break;
                            }
                            break;   
                    }

                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【CERTIFICATECHANGE】CERTIFICATECHANGEID={0}，{1}",dr["CERTIFICATECHANGEID"],Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'CertificateChange','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["CERTIFICATECHANGEID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'CertificateChange','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["CERTIFICATECHANGEID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【CERTIFICATECHANGE】CERTIFICATECHANGEID={0}", dr["CERTIFICATECHANGEID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_BJHJ_cy_change"] = 0;//记录扫描位置
            }

        }

        /// <summary>
        /// 从业人员续期办件汇聚赋码
        /// </summary>
        private void ApplyGatherCertContinue(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} x.[CERTIFICATECONTINUEID],c.[POSTID],c.[CERTIFICATECODE],
                                                c.[WORKERCERTIFICATECODE],c.WORKERNAME,w.[UUID]
                                        FROM DBO.CERTIFICATE c
                                        inner JOIN DBO.CERTIFICATECONTINUE x on c.CERTIFICATEID = x.CERTIFICATEID
                                        inner join [WORKER] w on c.WORKERCERTIFICATECODE = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='CertificateContinue' and x.[CERTIFICATECONTINUEID]= g.BusID                
                                        where c.posttypeid < 3  and x.[ApplyDate] > '2025-08-02' and x.[GetDate] > dateadd(day,-7,getdate()) and x.GetResult = '通过'
                                        and x.CERTIFICATECONTINUEID > {1}  and w.[UUID] is not null    
                                        and g.BusID is null                             
                                        order by x.CERTIFICATECONTINUEID ", MaxCountExe, cursor_long["cursor_BJHJ_cy_continue"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_BJHJ_cy_continue"] = 0;//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条从业人员续期汇聚赋码：ApplyGatherCertContinue()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_long["cursor_BJHJ_cy_continue"] = Convert.ToInt64(dr["CERTIFICATECONTINUEID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)
                    switch (dr["PostID"].ToString())
                    {
                        case "147":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_证书延期;
                            break;
                        case "148":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_证书延期;
                            break;
                        case "6":
                        case "1123":
                        case "1125":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_证书延期;
                            break;

                        case "120":	//建筑电工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑电工_延期复核;
                            break;
                        case "123":	//建筑架子工（普通脚手架）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_延期复核;
                            break;
                        case "126":	//建筑起重司索信号工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重司索信号工_延期复核;
                            break;
                        case "129":	//建筑起重机械司机（塔式起重机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_延期复核;
                            break;
                        case "132":	//建筑起重机械司机（施工升降机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_延期复核;
                            break;
                        case "135":	//建筑起重机械司机（物料提升机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_延期复核;
                            break;
                        case "138":	//建筑起重机械安装拆卸工（塔式起重机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_延期复核;
                            break;
                        case "141":	//建筑起重机械安装拆卸工（施工升降机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_延期复核;
                            break;
                        case "144":	//高处作业吊篮安装拆卸工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_延期复核;
                            break;
                        case "151":	//建筑架子工（附着升降脚手架）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_延期复核;
                            break;
                        case "152":	//建筑起重机械安装拆卸工（物料提升机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_延期复核;
                            break;
                        //case "299":	//汽车起重机司机
                        //    r.serviceCodeId = ;
                        //    break;
                        default:
                            break;
                    }

                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【CERTIFICATECONTINUE】CERTIFICATECONTINUEID={0}，{1}", dr["CERTIFICATECONTINUEID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));
                    
                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'CertificateContinue','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["CERTIFICATECONTINUEID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'CertificateContinue','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["CERTIFICATECONTINUEID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【CERTIFICATECONTINUE】CERTIFICATECONTINUEID={0}", dr["CERTIFICATECONTINUEID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_BJHJ_cy_continue"] = 0;//记录扫描位置
            }

        }

        /// <summary>
        /// 从业人员A本增发办件汇聚赋码
        /// </summary>
        /// <param name="startTime"></param>
        private void ApplyGatherCertMore(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} c.[ApplyID],c.[CERTIFICATECODE],  c.[WORKERCERTIFICATECODE],c.WORKERNAME,w.[UUID]
                                        FROM DBO.[CertificateMore] c                                      
                                        inner join [WORKER] w on c.WORKERCERTIFICATECODE = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='CertificateMore' and c.[ApplyID]= g.BusID    
                                        where  c.[NewUnitCheckTime] > '2025-08-02' and c.[CheckDate] > dateadd(day,-7,getdate()) and c.[CheckAdvise] = '通过'
                                        and c.[ApplyID] > {1}  and w.[UUID] is not null     
                                        and g.BusID is null                                
                                        order by c.[ApplyID] ", MaxCountExe, cursor_long["cursor_BJHJ_cy_More"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_BJHJ_cy_More"] = 0;//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条从业人员续期汇聚赋码：ApplyGatherCertMore()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_long["cursor_BJHJ_cy_More"] = Convert.ToInt64(dr["ApplyID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)
                    r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_考核发证;


                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【CertificateMore】ApplyID={0}，{1}", dr["ApplyID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'CertificateMore','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'CertificateMore','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【CertificateMore】ApplyID={0}", dr["ApplyID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_BJHJ_cy_More"] = 0;//记录扫描位置
            }

        }

        /// <summary>
        /// 从业人员安管证书进京办件汇聚赋码
        /// </summary>
        /// <param name="startTime"></param>
        private void ApplyGatherCertEnter(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} c.PostID,c.[ApplyID],c.[CERTIFICATECODE],  c.[WORKERCERTIFICATECODE],c.WORKERNAME,w.[UUID]
                                        FROM DBO.[CertificateEnterApply] c                                      
                                        inner join [WORKER] w on c.WORKERCERTIFICATECODE = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='CertificateEnterApply' and c.[ApplyID]= g.BusID
                                        where  c.[NewUnitCheckTime] > '2025-08-02' and c.[ACCEPTDATE] > dateadd(day,-7,getdate()) and c.[GETRESULT] = '通过'
                                        and c.[ApplyID] > {1}  and w.[UUID] is not null   
                                        and g.BusID is null                              
                                        order by c.[ApplyID] ", MaxCountExe, cursor_long["cursor_BJHJ_cy_jinjing"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_BJHJ_cy_jinjing"] = 0;//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条从业人员续期汇聚赋码：ApplyGatherCertEnter()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_long["cursor_BJHJ_cy_jinjing"] = Convert.ToInt64(dr["ApplyID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)
    
                    //岗位工种ID，147：企业主要负责人，148：项目负责人，6：土建类专职安全生产管理人员 ，1123 ：机械类专职安全生产管理人员，1125：综合类专职安全生产管理人员
                    switch (dr["PostID"].ToString())
                    {
                        case "147":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_考核发证;
                            break;
                        case "148":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_考核发证;
                            break;
                        case "6":
                        case "1123":
                        case "1125":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_考核发证;
                            break;
                        default:
                            break;
                    }


                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【CertificateEnterApply】ApplyID={0}，{1}", dr["ApplyID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'CertificateEnterApply','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'CertificateEnterApply','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【CertificateEnterApply】ApplyID={0}", dr["ApplyID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_BJHJ_cy_jinjing"] = 0;//记录扫描位置
            }

        }

        /// <summary>
        /// 从业人员安管证书考核办件汇聚赋码
        /// </summary>
        /// <param name="startTime"></param>
        private void ApplyGatherExamSignup(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} c.PostID,c.[CertificateID],c.[CERTIFICATECODE],  c.[WORKERCERTIFICATECODE],c.WORKERNAME,w.[UUID]
                                        FROM DBO.[CERTIFICATE] c                                      
                                        inner join [WORKER] w on c.WORKERCERTIFICATECODE = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='Certificate' and c.[CertificateID]= g.BusID
                                        where  c.[CreateTime] > '2025-08-02' and c.[CheckDate] > dateadd(day,-7,getdate()) 
                                        and c.[CertificateID] > {1}  and w.[UUID] is not null 
                                        and c.PostTypeID < 3 and c.postID <> 299 and c.examplanID > 0 and CheckDate is not null
                                        and g.BusID is null                              
                                        order by c.[CertificateID] ", MaxCountExe, cursor_long["cursor_BJHJ_cy_Signup"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_long["cursor_BJHJ_cy_Signup"] = 0;//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条从业人员考核汇聚赋码：ApplyGatherExamSignup()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_long["cursor_BJHJ_cy_Signup"] = Convert.ToInt64(dr["CertificateID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)

                    //岗位工种ID，147：企业主要负责人，148：项目负责人，6：土建类专职安全生产管理人员 ，1123 ：机械类专职安全生产管理人员，1125：综合类专职安全生产管理人员
                    switch (dr["PostID"].ToString())
                    {
                        case "147":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业主要负责人安全生产考核_考核发证;
                            break;
                        case "148":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业项目负责人安全生产考核_考核发证;
                            break;
                        case "6":
                        case "1123":
                        case "1125":
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工企业专职安全生产管理人员安全生产考核_考核发证;
                            break;

                        case "120":	//建筑电工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑电工_考核;
                            break;
                        case "123":	//建筑架子工（普通脚手架）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_考核;
                            break;
                        case "126":	//建筑起重司索信号工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重司索信号工_考核;
                            break;
                        case "129":	//建筑起重机械司机（塔式起重机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_考核;
                            break;
                        case "132":	//建筑起重机械司机（施工升降机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_考核;
                            break;
                        case "135":	//建筑起重机械司机（物料提升机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_考核;
                            break;
                        case "138":	//建筑起重机械安装拆卸工（塔式起重机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_考核;
                            break;
                        case "141":	//建筑起重机械安装拆卸工（施工升降机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_考核;
                            break;
                        case "144":	//高处作业吊篮安装拆卸工
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_考核;
                            break;
                        case "151":	//建筑架子工（附着升降脚手架）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_考核;
                            break;
                        case "152":	//建筑起重机械安装拆卸工（物料提升机）
                            r.serviceCodeId = EnumManager.ServiceCodeId.建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_考核;
                            break;
                        //case "299":	//汽车起重机司机
                        //    r.serviceCodeId = ;
                        //    break;
                        default:
                            break;
                    }


                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【Certificate】CertificateID={0}，{1}", dr["CertificateID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'Certificate','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["CertificateID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'Certificate','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["CertificateID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【Certificate】CertificateID={0}", dr["CertificateID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_long["cursor_BJHJ_cy_Signup"] = 0;//记录扫描位置
            }

        }

        /// <summary>
        /// 二级造价工程师办件汇聚赋码
        /// </summary>
        /// <param name="startTime"></param>
        private void ApplyGatherZJGCS(DateTime startTime)
        {
            string sql = String.Format(@"select top {0} c.[ApplyType],c.[ApplyID], c.[PSN_CertificateNO] as WORKERCERTIFICATECODE,c.[PSN_Name] as WORKERNAME,w.[UUID]
                                        FROM DBO.[zjs_Apply] c                                      
                                        inner join [WORKER] w on c.[PSN_CertificateNO] = w.CertificateCode   
                                        left join [Gather] g on g.BusTable='zjs_Apply' and c.[ApplyID]= g.BusID
                                        where  c.[GetDateTime] > '2025-08-02' and c.[GetDateTime]> dateadd(day,-7,getdate()) and c.[GetResult]= '通过'
                                        and c.[ApplyID] > '{1}'  and w.[UUID] is not null   
                                        and g.BusID is null                              
                                        order by c.[ApplyID] ", MaxCountExe, cursor_str["cursor_BJHJ_zjgcs"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_str["cursor_BJHJ_zjgcs"] = "";//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条从业人员续期汇聚赋码：ApplyGatherZJGCS()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_str["cursor_BJHJ_zjgcs"] = Convert.ToString(dr["ApplyID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    r.areaCode = "110000";//  收件部门所属行政区划编码
                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型
                    r.deptId = "11110000000021135M";//  终审部门编码
                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)
    
                    //岗位工种ID，147：企业主要负责人，148：项目负责人，6：土建类专职安全生产管理人员 ，1123 ：机械类专职安全生产管理人员，1125：综合类专职安全生产管理人员
                    switch (dr["ApplyType"].ToString())
                    {
                        case "初始注册":
                            r.serviceCodeId = EnumManager.ServiceCodeId.二级注册造价工程师注册_初始注册;
                            break;
                        case "变更注册":
                            r.serviceCodeId = EnumManager.ServiceCodeId.二级注册造价工程师注册_变更注册;
                            break;
                        case "注销":
                            r.serviceCodeId = EnumManager.ServiceCodeId.二级注册造价工程师注册_注销注册;
                            break;
                        case "延续注册":
                            r.serviceCodeId = EnumManager.ServiceCodeId.二级注册造价工程师注册_延续注册;
                            break;
                        default:
                            break;
                    }


                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【zjs_Apply】ApplyID={0}，{1}", dr["ApplyID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'zjs_Apply','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'zjs_Apply','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【zjs_Apply】ApplyID={0}", dr["ApplyID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_str["cursor_BJHJ_zjgcs"] = "";//记录扫描位置
            }

        }

        /// <summary>
        /// 二级注册建造师办件汇聚赋码
        /// </summary>
        /// <param name="startTime"></param>
        private void ApplyGatherJZS(DateTime startTime)
        {
//            string sql = String.Format(@"select  top {0} a.ApplyID,a.[ENT_City], a.[ApplyType],a.ApplyTypeSub,p.[Profession],a.[PSN_Name] as WORKERNAME,a.[PSN_CertificateNO] as WORKERCERTIFICATECODE,w.[UUID]
//                                          FROM [dbo].[Apply] a inner join 
//                                          (select '市政' as Profession union select '矿业' union select '建筑' union select '机电' union select '水利' union select '公路') p 
//                                          on a.[PSN_RegisteProfession] like ('%'+ p.Profession + '%')
//                                          inner join [WORKER] w on a.[PSN_CertificateNO] = w.CertificateCode   
//                                          left join [Gather] g on g.BusTable='Apply' and a.[ApplyID]= g.BusID
//                                          where a.[GetDateTime] > '2025-08-02' and  a.[GetDateTime] > dateadd(day,-7,getdate()) and a.[GetResult]='通过'
//                                        and a.[ApplyID] > '{1}'  and w.[UUID] is not null   
//                                        and g.BusID is null                              
//                                        order by a.[ApplyID] ", MaxCountExe, cursor_str["cursor_BJHJ_jzs"]);

            string sql = String.Format(@"
select t.ApplyID,t.[ENT_City], t.[ApplyType],t.ApplyTypeSub,t.[Profession],t.WORKERNAME,t.WORKERCERTIFICATECODE,w.[UUID]
from 
(
	--增项注册
	select  top {0} a.ApplyID,a.[ENT_City], a.[ApplyType],a.ApplyTypeSub,p.[Profession],a.[PSN_Name] as WORKERNAME,a.[PSN_CertificateNO] as WORKERCERTIFICATECODE
	FROM [dbo].[Apply] a 
	inner join 
	(
		select ApplyID,additem1 as [Profession] from [dbo].[ApplyAddItem] where additem1 is not null 
		union 
		select ApplyID,additem2 as [Profession] from [dbo].[ApplyAddItem] where additem2 is not null 
	) p on a.ApplyID = p.ApplyID
	where a.[CheckDate] > '2025-08-02' and  a.[CheckDate] > dateadd(day,-7,getdate()) and a.[CheckResult]='通过'
	and a.[ApplyID] > '{1}'  
	union--延期注册
	select  top {0} a.ApplyID,a.[ENT_City], a.[ApplyType],a.ApplyTypeSub,p.[Profession],a.[PSN_Name] as WORKERNAME,a.[PSN_CertificateNO] as WORKERCERTIFICATECODE
	FROM [dbo].[Apply] a 
	inner join 
	(
		select ApplyID,[PSN_RegisteProfession1] as [Profession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession1] is not null and [IfContinue1]=1
		union 
		select ApplyID,[PSN_RegisteProfession2] as [Profession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession2] is not null and [IfContinue2]=1
		union 
		select ApplyID,[PSN_RegisteProfession3] as [Profession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession3] is not null and [IfContinue3]=1
		union 
		select ApplyID,[PSN_RegisteProfession4] as [Profession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession4] is not null and [IfContinue4]=1
	) p on a.ApplyID = p.ApplyID
	where a.[GetDateTime] > '2025-08-02' and  a.[GetDateTime] > dateadd(day,-7,getdate()) and a.[GetResult]='通过'
	and a.[ApplyID] > '{1}'
	union--注销
	select  top {0} a.ApplyID,a.[ENT_City], a.[ApplyType],a.ApplyTypeSub,p.[Profession],a.[PSN_Name] as WORKERNAME,a.[PSN_CertificateNO] as WORKERCERTIFICATECODE
	FROM [dbo].[Apply] a 
	inner join  [dbo].[ApplyCancel] c on a.ApplyID = c.ApplyID
	inner join
	(
		select '公路' as [Profession]
		union select '机电'
		union select '建筑'
		union select '矿业'
		union select '市政'
		union select '水利'
	) p on c.[ZyItem] like  '%' + p.[Profession] +'%'
	where a.[GetDateTime] > '2025-08-02' and  a.[GetDateTime] > dateadd(day,-7,getdate()) and a.[GetResult]='通过'
	and a.[ApplyID] > '{1}' 
	union--初始注册、 重新注册、变更注册
	select  top {0} a.ApplyID,a.[ENT_City], a.[ApplyType],a.ApplyTypeSub,p.[Profession],a.[PSN_Name] as WORKERNAME,a.[PSN_CertificateNO] as WORKERCERTIFICATECODE
	FROM [dbo].[Apply] a 
	inner join
	(
		select '公路' as [Profession]
		union select '机电'
		union select '建筑'
		union select '矿业'
		union select '市政'
		union select '水利'
	) p on a.PSN_RegisteProfession like  '%' + p.[Profession] +'%'
	where (a.[ApplyType]='初始注册' or a.[ApplyType]='重新注册' or a.[ApplyType]='变更注册') 
	and a.[GetDateTime] > '2025-08-02' and  a.[GetDateTime] > dateadd(day,-7,getdate()) and a.[GetResult]='通过'
    and a.[ApplyID] > '{1}' 
	) t
inner join [WORKER] w on t.WORKERCERTIFICATECODE = w.CertificateCode   
left join [Gather] g on g.BusTable='Apply' and t.[ApplyID]= g.BusID
where  w.[UUID] is not null   
and g.BusID is null    
order by t.[ApplyID]", MaxCountExe, cursor_str["cursor_BJHJ_jzs"]);

            DataTable dt = (new DBHelper()).GetFillData(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                cursor_str["cursor_BJHJ_jzs"] = "";//记录扫描位置
                return;
            }
            FileLog.WriteLog(string.Format("准备{0}条二级建造师续期汇聚赋码：ApplyGatherJZS()", dt.Rows.Count));

            Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest r;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (ApplyGatherStartTime != startTime) return;//新线程已将开启，停止当前

                    cursor_str["cursor_BJHJ_jzs"] = Convert.ToString(dr["ApplyID"]);//记录扫描位置

                    r = new Atg.Api.Request.AtgBizAffairUnicodeGenerateRequest();
                    r.applicantUid = dr["UUID"].ToString();//  申请人UID
                    r.applyCardNumber = dr["WORKERCERTIFICATECODE"].ToString();//  申请者证件号码
                    r.applyCardType = (Utility.Check.isChinaIDCard(dr["WORKERCERTIFICATECODE"].ToString()) == false ? "49" : "31");//  申请者证件类型(31：身份证；49：其他个人证件)
                    r.applyForm = "1";//  申报来源(PC端网上申报)
                    r.applyName = dr["WORKERNAME"].ToString();//  申请者名称
                    //r.areaCode = "110000";
 
                    #region r.areaCode 收件部门所属行政区划编码
                   
                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                    {
                        case "东城": r.areaCode = "110101"; break;
                        case "西城": r.areaCode = "110102"; break;
                        case "朝阳": r.areaCode = "110105"; break;
                        case "海淀": r.areaCode = "110108"; break;
                        case "丰台": r.areaCode = "110106"; break;
                        case "石景山": r.areaCode = "110107"; break;
                        case "门头沟": r.areaCode = "110109"; break;
                        case "房山": r.areaCode = "110111"; break;
                        case "通州": r.areaCode = "110112"; break;
                        case "顺义": r.areaCode = "110113"; break;
                        case "大兴": r.areaCode = "110115"; break;
                        case "昌平": r.areaCode = "110114"; break;
                        case "平谷": r.areaCode = "110117"; break;
                        case "怀柔": r.areaCode = "110116"; break;
                        case "密云": r.areaCode = "110118"; break;
                        case "延庆": r.areaCode = "110119"; break;
                        case "亦庄": r.areaCode = "110115"; break;
                        default: r.areaCode = "110000"; break;
                    }
                    #endregion r.areaCode

                    r.belongSystem = appId;//  所属系统对接编码
                    r.busType = "1";//  业务类型

                    #region r.deptId 终审部门编码
                    if (dr["ApplyType"].ToString() == "变更注册")
                    {
                        if (dr["ApplyTypeSub"].ToString() == "企业信息变更")
                        {
                            r.deptId = "11110000000021135M";//  终审部门编码
                        }
                        else
                        {
                            switch (dr["ENT_City"].ToString().Replace("区", ""))
                            {
                                case "东城": r.deptId = "11110101069580736N"; break;
                                case "西城": r.deptId = "11110102000037874K"; break;
                                case "朝阳": r.deptId = "1111010500005384XU"; break;
                                case "海淀": r.deptId = "11110108000058608T"; break;
                                case "丰台": r.deptId = "1111010600006272X3"; break;
                                case "石景山": r.deptId = "11110107000067694R"; break;
                                case "门头沟": r.deptId = "11110109000072792R"; break;
                                case "房山": r.deptId = "11110111000077745X"; break;
                                case "通州": r.deptId = "1111011200008331XP"; break;
                                case "顺义": r.deptId = "11110110000092806F"; break;
                                case "大兴": r.deptId = "11110224MB0217883D"; break;
                                case "昌平": r.deptId = "11110221000102744P"; break;
                                case "平谷": r.deptId = "11110226000112969L"; break;
                                case "怀柔": r.deptId = "11110227000097869W"; break;
                                case "密云": r.deptId = "11110228000107772X"; break;
                                case "延庆": r.deptId = "111102290001177434"; break;
                                case "亦庄": r.deptId = "TEKFQ1357924680098"; break;
                                default: r.deptId = "11110000000021135M"; break;
                            }
                        }
                    }
                    else
                    {
                        r.deptId = "11110000000021135M";//  终审部门编码
                    }
                    #endregion r.deptId

                    r.extJson = null;//扩展字段
                    r.relBusId = "";//  联办业务标识
                    r.vcType = "2";//事项受理模式(1：由不同的系统完成收件和办件。2：既能收件又能办件。)

                    #region r.serviceCodeId 事项标准码

                    switch (dr["ApplyType"].ToString())
                    {
                        case "初始注册":
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_初始注册;
                                    break;
                                case "市政":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_初始注册;
                                    break;
                                case "矿业":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_初始注册;
                                    break;
                                case "机电":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_初始注册;
                                    break;
                                case "水利":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_初始注册;
                                    break;
                                case "公路":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_初始注册;
                                    break;

                            }
                            break;

                        case "注销":
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_注销注册;
                                    break;
                                case "市政":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_注销注册;
                                    break;
                                case "矿业":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_注销注册;
                                    break;
                                case "机电":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_注销注册;
                                    break;
                                case "水利":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_注销注册;
                                    break;
                                case "公路":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_注销注册;
                                    break;

                            }
                            break;
                        case "延期注册"://延续注册":
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_延续注册;
                                    break;
                                case "市政":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_延续注册;
                                    break;
                                case "矿业":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_延续注册;
                                    break;
                                case "机电":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_延续注册;
                                    break;
                                case "水利":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_延续注册;
                                    break;
                                case "公路":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_延续注册;
                                    break;
                            }
                            break;
                        case "重新注册":
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_重新注册;
                                    break;
                                case "市政":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_重新注册;
                                    break;
                                case "矿业":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_重新注册;
                                    break;
                                case "机电":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_重新注册;
                                    break;
                                case "水利":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_重新注册;
                                    break;
                                case "公路":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_重新注册;
                                    break;
                            }
                            break;
                        case "增项注册":
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_增项注册;
                                    break;
                                case "市政":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_增项注册;
                                    break;
                                case "矿业":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_增项注册;
                                    break;
                                case "机电":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_增项注册;
                                    break;
                                case "水利":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_增项注册;
                                    break;
                                case "公路":
                                    r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_增项注册;
                                    break;
                            }
                            break;
                        case "变更注册":
                            #region 变更注册
                            switch (dr["Profession"].ToString())
                            {
                                case "建筑":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_建筑工程专业_变更注册; break;
                                    }
                                    break;
                                case "市政":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_市政公用工程专业_变更注册; break;
                                    }
                                    break;
                                case "矿业":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_矿业工程专业_变更注册; break;
                                    }
                                    break;
                                case "机电":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_机电工程专业_变更注册; break;
                                    }
                                    break;
                                case "水利":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_水利水电工程专业_变更注册; break;
                                    }
                                    break;
                                case "公路":
                                    switch (dr["ENT_City"].ToString().Replace("区", ""))
                                    {
                                        case "朝阳": r.serviceCodeId = EnumManager.ServiceCodeId.朝阳区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "大兴": r.serviceCodeId = EnumManager.ServiceCodeId.大兴区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "昌平": r.serviceCodeId = EnumManager.ServiceCodeId.昌平区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "平谷": r.serviceCodeId = EnumManager.ServiceCodeId.平谷区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "东城": r.serviceCodeId = EnumManager.ServiceCodeId.东城区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "通州": r.serviceCodeId = EnumManager.ServiceCodeId.通州区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "亦庄": r.serviceCodeId = EnumManager.ServiceCodeId.经济技术开发区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "顺义": r.serviceCodeId = EnumManager.ServiceCodeId.顺义区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "石景山": r.serviceCodeId = EnumManager.ServiceCodeId.石景山区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "海淀": r.serviceCodeId = EnumManager.ServiceCodeId.海淀区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "延庆": r.serviceCodeId = EnumManager.ServiceCodeId.延庆区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "怀柔": r.serviceCodeId = EnumManager.ServiceCodeId.怀柔区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "西城": r.serviceCodeId = EnumManager.ServiceCodeId.西城区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "房山": r.serviceCodeId = EnumManager.ServiceCodeId.房山区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "密云": r.serviceCodeId = EnumManager.ServiceCodeId.密云区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "丰台": r.serviceCodeId = EnumManager.ServiceCodeId.丰台区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        case "门头沟": r.serviceCodeId = EnumManager.ServiceCodeId.门头沟区_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                        default: r.serviceCodeId = EnumManager.ServiceCodeId.北京市_二级建造师执业资格认定_公路工程专业_变更注册; break;
                                    }
                                    break;
                            }
                            #endregion 变更注册
                            break;
                        default:
                            break;
                    }
                    #endregion r.serviceCodeId

                    Atg.Api.Response.AtgBizAffairUnicodeGenerateResponse p = Bizclient.Execute(r);
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码成功【Apply】ApplyID={0}，{1}", dr["ApplyID"], Atg.Api.Util.JsonUtil.ConvertToJsonString(p)));

                    if (p.resultStatus == "S")//成功
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[GatherCode],[UUID]) select 'Apply','{0}','{1}',GETDATE(),1,'成功','{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.data, dr["UUID"]));
                    }
                    else
                    {
                        CommonDAL.ExecSQL(string.Format("INSERT INTO [dbo].[Gather]([BusTable],[BusID],[ItemID],[GatherTime],[GatherResultCode],[GatherResultDesc],[UUID]) select 'Apply','{0}','{1}',GETDATE(),0,'{2}','{3}'", dr["ApplyID"], r.serviceCodeId, p.resultMsg, dr["UUID"]));
                    }
                }
                catch (Exception ex)
                {
                    FileLog.WriteLog(string.Format("获取办件汇聚统一赋码失败【Apply】ApplyID={0}", dr["ApplyID"]), ex);
                }
            }
            if (dt.Rows.Count < MaxCountExe)
            {
                cursor_str["cursor_BJHJ_jzs"] = "";//记录扫描位置
            }
        }

        #endregion
    }
}