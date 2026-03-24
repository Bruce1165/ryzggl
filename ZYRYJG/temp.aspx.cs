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


namespace ZYRYJG
{

    public partial class temp : System.Web.UI.Page
    {
        jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL;//企业资质

        protected void Page_Load(object sender, EventArgs e)
        {


            //GSJ_QY_GSDJXXMDL o = UnitDAL.GetObjectUni_scid("MA0218305");
            //Response.Write(o.ENT_NAME);
            ////#region 读取大对象，保存为文件

            //DataTable dt = CommonDAL.GetDataTable("select * from [dbo].[nan1] where [DOCDataOID]='9A05F900-BA53-4A8F-AA53-7CE6F64B2524'");
            ////假设只有一条记录
            //Byte[] byteImageCol = (Byte[])(dt.Rows[0]["FileBody"]);
            //File.WriteAllBytes("d://04.doc", byteImageCol);

            ////#endregion 读取大对象，保存为文件


            //电子证书测试
            //testPDFCreate();

            //DBHelper _DBHelper = new DBHelper("DBConnStr2");

            //object rtn = _DBHelper.ExecuteScalar("select count(*)  FROM [dbo].[FileDir]");
            //UIHelp.layerAlert(Page, rtn.ToString());

            //Utility.SFTPHelper sftp = null;
            //try
            //{
            //    sftp = new SFTPHelper("219.142.101.108", "4022", "110000A", "bi110000@a113");

            //    sftp.Put(string.Format(@"d:\01test\110000ryxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")), string.Format(@"110000\110000ryxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd")));

            //}
            //catch(Exception ex)
            //{
            //    if (sftp != null && sftp.Connected)
            //    {
            //        sftp.Disconnect();                   
            //    }
            //    FileLog.WriteLog("同步二级建造师到建设部失败", ex);
            //}

            //string tmp_zzbs = string.Format("{0}.{1}.{2}.{3}.{4}",                                                      
            //                                    "1.2.156.3005.2",//电子证照根代码                                
            //                                    "11100000000013338W001",//证照类型代码                           
            //                                    "11110000000021135M",//证照颁发机构代码                          
            //                                    "fce90446-8949-4e00-92b4-4bd193200933 ".Replace("-", "").ToUpper(),//流水号
            //                                     1//版本号                      
            //                                    );

            //try
            //{
            //    string checkcode = GetZZBS_CheckCode(tmp_zzbs.Replace("1.2.156.3005.2", "").Replace(".", ""));
            //    UIHelp.layerAlert(Page, checkcode);
            //}
            //catch(Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "获取二建证书证书表示校验位失败", ex);
            //}

//            //二建
//            var template_url = "";
//            var save_pdf_url = "";
//            Dictionary<string, string> dic;
//            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\二级建造师注册证书.pdf";
//            save_pdf_url = @"d:\01test\二建.pdf";
//            dic = ReadForm(template_url);

//            string sql = String.Format(@"Select top 15 * FROM [dbo].[View_JZS_TOW_WithProfession]
//                                        where   [PSN_RegisterNO] in
//(
//'京211151864508'
//,'京211131864367'
//,'京211161864256'
//,'京211161864404'
//,'京211151864131'
//,'京211161864517'
//,'京211161864362'
//,'京211161864415'
//,'京211121865371'
//,'京211161864107'
//,'京211161864497'
//,'京211151864215'
//,'京211161865295'
//,'京211161864325'
//,'京211161864110'
//)
//                                        order by [CJSJ] desc,PSN_CertificateNO");
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);

//            foreach (DataRow r in dtOriginal.Rows)
//            {
//                FillFormOfJZS(template_url, string.Format(@"d:\01test\2\{0}.pdf",r["certificateCAID"]), dic, r);//填充模板
//            }

            ////电子证书测试
            //testPDFCreate();


//            //人员信息表（rrxxb）
//            string sql = String.Format(@"
//            SELECT
//                p.[PSN_Name] xm	    --姓名
//                ,p.[ENT_Name] qymc	--企业名称
//                ,u.[CreditCode] orgcode	--组织机构代码/统一信用代码
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
//            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
//            dtOriginal.TableName = "ryxxb";
//            DataSet ds = new DataSet("NewDataSet");
//            ds.Tables.Add(dtOriginal.Copy());

//            //导出路径
//            string strFilePath = string.Format(@"d:\01test\110000ryxxb{0}.xml",DateTime.Now.ToString("yyyyMMdd"));

//            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();           
//            settings.Indent = true; //要求缩进
//            //注意如果不设置encoding默认将输出utf-16
//            //注意这儿不能直接用Encoding.UTF8如果用Encoding.UTF8将在输出文本的最前面添加4个字节的非xml内容
//            settings.Encoding = new UTF8Encoding(false);            
//            settings.NewLineChars = Environment.NewLine;//设置换行符

//            using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
//            {
//                ds.WriteXml(a);
//            }


//            //企业信息表（qyxxb）
//            sql = String.Format(@"
//            select distinct 
//                u.[ENT_Name] qymc	--企业名称
//                ,u.[CreditCode] orgcode	--组织机构代码/统一信用代码
//                ,'110000' sjbm	--省级编码
//                ,'' gszcsf	--工商注册省份
//                ,r.RegionCode citybm	--地市编码
//                ,u.ENT_City gszcsz	--工商注册市州
//                ,u.[ENT_Corporate] fddbr	--法定代表人
//                ,u.[END_Addess] txdz	--通讯地址
//                , qylx=case 
//                when u.ent_type like '%施工%' then '施工'
//                when u.ent_type like '%监理%' then '监理'
//                when u.ent_type like '%勘察%' then '勘察'
//                when u.ent_type like '%设计%' then '设计'
//                when u.ent_type like '%造价%' then '造价咨询'
//                when u.ent_type like '%招标%' then '招标代理'
//                else '无' end	--企业资质类型
//                ,f.REG_NO yyzzh	--营业执照号
//                ,'110000' sfbz	--省份编码
//             FROM [dbo].[COC_TOW_Person_BaseInfo] p
//             inner join [dbo].[Unit] u on p.ENT_OrganizationsCode = u.ENT_OrganizationsCode
//             left join [Dict_Region] r on u.ENT_City=r.[RegionName]
//             left join [dbo].[QY_FRK] f on convert(varchar(18),u.ENT_OrganizationsCode)=f.ORG_CODE
//            where p.[PSN_RegisteType] <'07'");
//            dtOriginal = (new DBHelper()).GetFillData(sql);
//            dtOriginal.TableName = "qyxxb";
//            ds = new DataSet("NewDataSet");
//            ds.Tables.Add(dtOriginal.Copy());

//            //导出路径
//            strFilePath = string.Format(@"d:\01test\110000qyxxb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));                      

//            using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
//            {
//                ds.WriteXml(a);
//            }

//            //注册专业表（zczyb）
//            sql = String.Format(@"            
//            select 
//	            p.[PSN_CertificateNO] zjhm--人员证件号码
//	            ,'03' zslx--证书类型（简称）
//	            , zczy=case 
//	            when z.[PRO_Profession] like '%建筑%' then '建筑工程'
//	            when z.[PRO_Profession] like '%公路%' then '公路工程'
//	            when z.[PRO_Profession] like '%机电%' then '机电工程'
//	            when z.[PRO_Profession] like '%水利%' then '水利水电工程'
//	            when z.[PRO_Profession] like '%市政%' then '市政公用工程'
//	            when z.[PRO_Profession] like '%矿业%' then '矿业工程'
//	            end--注册专业
//	            ,convert(varchar(10),z.[PRO_ValidityBegin],20) yxqq--有效期起
//	            ,convert(varchar(10),z.[PRO_ValidityEnd],20) yxqz--有效期止
//	            ,'110000' sfbz--省份编码
//            FROM [dbo].[COC_TOW_Person_BaseInfo] p
//            inner join [dbo].[COC_TOW_Register_Profession] z on p.PSN_ServerID = z.PSN_ServerID
//            where p.[PSN_RegisteType] <'07'");
//            dtOriginal = (new DBHelper()).GetFillData(sql);
//            dtOriginal.TableName = "zczyb";
//            ds = new DataSet("NewDataSet");
//            ds.Tables.Add(dtOriginal.Copy());

//            //导出路径
//            strFilePath = string.Format(@"d:\01test\110000zczyb{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

//            using (System.Xml.XmlWriter a = System.Xml.XmlWriter.Create(strFilePath, settings))
//            {
//                ds.WriteXml(a);
//            }


            //iniDDLogin();

           
            if (!IsPostBack && Session["userInfo"] == null)///Session["user"]根据个人逻辑要或不要
            {

                Session.Abandon();
                Request.Cookies.Clear();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            }

            //test();

           

            //if (!IsPostBack)
            //{
            //    Response.Redirect("login2.aspx?Userid=38810");
            //    return;
            //    string zzjgdm = "";//组织机构代码
            //    int userid = 38810;//组织机构代码
            //    EGovThirdServiceSoapClient client = new EGovThirdServiceSoapClient();
            //    EGovSoapHead heda = new EGovSoapHead();
            //    CorpInfo corpinfo = client.GetCorpInfoByUserID(ref heda, userid);
            //    zzjgdm = corpinfo.Dept_Code;
            //    Response.Write(zzjgdm);
            //}

            //UIHelp.layerAlert(Page, GetZZBS_CheckCode("lll00000000013338W00211320482014138860P20180725020100l"));
            //UIHelp.layerAlert(Page, GetZZBS_CheckCode("12330110470391229PA11601010003456001"));
        }

        #region  测试服务器登录

        ////个人登录
        //protected void ButtonWorker_Click(object sender, EventArgs e)
        //{
        //    string loginInfo = string.Empty;
        //    byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

        //    loginInfo = UTF8Encoding.UTF8.GetString(bytes);

        //    string[] parts = loginInfo.Split('\\');
        //    string username = parts[0];
        //    string password = parts[1];

        //    //var httpCookie = Request.Cookies["ExamCheckCode"];
        //    //if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
        //    //{
        //    //    txtValidator.Value = "";
        //    //    UIHelp.layerAlert(Page, "请输入正确的验证码！");
        //    //    return;
        //    //}

        //    if (password != "yi@34wu6")
        //    {
        //        UIHelp.layerAlert(Page, "登录失败！");
        //        return;
        //    }


        //    WorkerOB _WorkerOB = WorkerDAL.GetUserObject(username);


        //    if (_WorkerOB == null)
        //    {

        //        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的个人注册信息，请确认是否已经在建委官方网站注册个人用户！"), true);
        //    }

        //    string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码,8）用户最后登录时间        
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);

        //    //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
        //    string _personType = "2";

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;

        //    ////获取三类人、专业人员考试近1年缺考考试计划信息
        //    //try
        //    //{
        //    //    DataTable dtMissExam = ExamResultDAL.GetMissExamList(1, _WorkerOB.CertificateCode);
        //    //    if (dtMissExam != null && dtMissExam.Rows.Count > 0)
        //    //    {
        //    //        Session["myMissExam"] = string.Format("{0}|{1}", Convert.ToDateTime(dtMissExam.Rows[0]["EXAMSTARTDATE"]).ToString("yyyy-MM-dd"), dtMissExam.Rows[0]["PostName"]);
        //    //    }
        //    //}
        //    //catch { }
        //    //try
        //    //{
        //    //    UIHelp.WriteOperateLog(_WorkerOB.WorkerName, _WorkerOB.WorkerID.ToString(), "登录", "个人北京市统一身份认证平台登录@");
        //    //}
        //    //catch { }
        //    Response.Redirect("~/Default.aspx", false);
        //}

        ////企业
        //protected void ButtonQY_Click(object sender, EventArgs e)
        //{
        //    string loginInfo = string.Empty;
        //    byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

        //    loginInfo = UTF8Encoding.UTF8.GetString(bytes);

        //    string[] parts = loginInfo.Split('\\');
        //    string username = parts[0];
        //    string password = parts[1];

        //    //查询企业
        //    string qyid = "";//企业ID
        //    string qymc = "";//企业名称
        //    string qyregion = "";//所属区县
        //    string shtyxydm = "";//社会统一信用代码
        //    //var httpCookie = Request.Cookies["ExamCheckCode"];
        //    //if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
        //    //{
        //    //    txtValidator.Value = "";
        //    //    UIHelp.layerAlert(Page, "请输入正确的验证码！");
        //    //    return;
        //    //}
        //    if (password != "yi@34wu6")
        //    {
        //        UIHelp.layerAlert(Page, "登录失败！");
        //        return;
        //    }

        //    string zzjgdm = username;//组织机构代码

        //    UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);
        //    if (o != null)
        //    {
        //        qyid = o.UnitID;//企业ID
        //        qymc = o.ENT_Name;//企业名称
        //        qyregion = o.ENT_City;//区县
        //        shtyxydm = o.CreditCode;//社会统一信用代码
        //    }
        //    else
        //    {
        //        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的企业资质信息，请确认是否已经获取了企业资质！"), true);
        //    }

        //    //是否为央企或大集团（有子公司）
        //    int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

        //    //是否为培训点
        //    int ifTrainUnit = CommonDAL.GetRowCount("[TrainUnit]", "1", string.Format(" and [UnitCode]='{0}' and [UseStatus]=1 ", shtyxydm));

        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码 ,8）用户最后登录时间       
        //    string userInfo = "";// string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", qyid, qymc, qyregion, "2", qyid, "", "", zzjgdm);

        //    string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

        //    if (countUnit > 0 && ifTrainUnit > 0)//央企或大集团、培训点
        //    {
        //        userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100|200", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
        //    }
        //    else if (countUnit > 0)//央企或大集团
        //    {
        //        userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|100", shtyxydm, "", "", zzjgdm, loginTime);//央企或大集团
        //    }
        //    else if (ifTrainUnit > 0)//培训点
        //    {
        //        userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
        //    }
        //    else//一般企业
        //    {
        //        userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
        //    }

        //    //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
        //    string _personType = "3";

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    try
        //    {
        //        UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业住建委办事大厅统一身份认证登录@");
        //    }
        //    catch { }
        //    Response.Redirect("~/Default.aspx", false);
        //}

        ////管理登录
        //protected void ButtonaAdmin_Click(object sender, EventArgs e)
        //{
        //    string loginInfo = string.Empty;
        //    byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

        //    loginInfo = UTF8Encoding.UTF8.GetString(bytes);

        //    string[] parts = loginInfo.Split('\\');
        //    string username = parts[0];
        //    string password = parts[1];


        //    //var httpCookie = Request.Cookies["ExamCheckCode"];
        //    //if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
        //    //{
        //    //    txtValidator.Value = "";
        //    //    UIHelp.layerAlert(Page, "请输入正确的验证码！");
        //    //    return;
        //    //}

        //    if (password != "yi@34wu6")
        //    {
        //        UIHelp.layerAlert(Page, "登录失败！");
        //        return;
        //    }

        //    UserOB userOb = UserDAL.GetObject(username);

        //    if (userOb == null)
        //    {
        //        txtValidator.Value = "";
        //        TextBoxUserName.Focus();
        //        UIHelp.layerAlert(Page, "用户名或密码错误！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID.ToString());
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    OrganizationOB _OrganizationOB = OrganizationDAL.GetObject(Convert.ToInt64(userOb.OrganID));

        //    string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码 ,8）用户最后登录时间 
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , _OrganizationOB.OrganNature == "虚拟区县" ? _OrganizationOB.OrganName : "全市"
        //        , roleIDs.ToString()
        //        , userOb.OrganID
        //        , ""
        //        , _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName//"北京市住房和城乡建设委员会"
        //        , _OrganizationOB.OrganCode
        //        , loginTime);

        //    //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
        //    string _personType = "6";

        //    userInfo = Cryptography.Encrypt(userInfo);

        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    try
        //    {
        //        UIHelp.WriteOperateLog(userOb.RelUserName, userOb.UserID.ToString(), "登录", string.Format("管理者网站登录页面登录。隶属机构：{0}@", _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName));
        //    }
        //    catch { }
        //    Response.Redirect("~/Default.aspx", false);
        //}

        #endregion

        #region  生产服务器登录

        //个人登录
        protected void ButtonWorker_Click(object sender, EventArgs e)
        {
            string loginInfo = string.Empty;
            byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

            loginInfo = UTF8Encoding.UTF8.GetString(bytes);

            string[] parts = loginInfo.Split('\\');
            string username = parts[0];
            string password = parts[1];

            var httpCookie = Request.Cookies["ExamCheckCode"];
            if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
            {
                txtValidator.Value = "";
                UIHelp.layerAlert(Page, "请输入正确的验证码！");
                return;
            }

            //if (password != "yi@34wu6")
            //{
            //    UIHelp.layerAlert(Page, "登录失败！");
            //    return;
            //}
            if (Utility.Cryptography.Encrypt(password.Substring(0, password.Length - 4)) != "XrPLui04NGWrPTH1F1d04w==")
            {
                UIHelp.layerAlert(Page, "登录失败！");
                return;
            }


            WorkerOB _WorkerOB = WorkerDAL.GetUserObject(username);


            if (_WorkerOB == null)
            {

                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的个人注册信息，请确认是否已经在建委官方网站注册个人用户！"), true);
            }

            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码,8）用户最后登录时间        
            string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "2";

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;

            //获取三类人、专业人员考试近1年缺考考试计划信息
            try
            {
                DataTable dtMissExam = ExamResultDAL.GetMissExamList(1, _WorkerOB.CertificateCode);
                if (dtMissExam != null && dtMissExam.Rows.Count > 0)
                {
                    Session["myMissExam"] = string.Format("{0}|{1}", Convert.ToDateTime(dtMissExam.Rows[0]["EXAMSTARTDATE"]).ToString("yyyy-MM-dd"), dtMissExam.Rows[0]["PostName"]);
                }
            }
            catch { }
            try
            {
                UIHelp.WriteOperateLog(_WorkerOB.WorkerName, _WorkerOB.WorkerID.ToString(), "登录", "个人北京市统一身份认证平台登录@");
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }

        //企业
        protected void ButtonQY_Click(object sender, EventArgs e)
        {
            try
            {
                string loginInfo = string.Empty;
                byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

                loginInfo = UTF8Encoding.UTF8.GetString(bytes);

                string[] parts = loginInfo.Split('\\');
                string username = parts[0];
                string password = parts[1];

                //查询企业
                string qyid = "";//企业ID
                string qymc = "";//企业名称
                string qyregion = "";//所属区县
                string shtyxydm = "";//社会统一信用代码
                var httpCookie = Request.Cookies["ExamCheckCode"];
                if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
                {
                    txtValidator.Value = "";
                    UIHelp.layerAlert(Page, "请输入正确的验证码！");
                    return;
                }
                //if (password != "yi@34wu6")
                //{
                //    UIHelp.layerAlert(Page, "登录失败！");
                //    return;
                //}
                if (Utility.Cryptography.Encrypt(password.Substring(0, password.Length - 4)) != "XrPLui04NGWrPTH1F1d04w==")
                {
                    UIHelp.layerAlert(Page, "登录失败！");
                    return;
                }
                string zzjgdm = username;//组织机构代码

                UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);
                if (o != null)
                {
                    qyid = o.UnitID;//企业ID
                    qymc = o.ENT_Name;//企业名称
                    qyregion = o.ENT_City;//区县
                    shtyxydm = o.CreditCode;//社会统一信用代码
                    zzjgdm = o.ENT_OrganizationsCode;

                    GSJ_QY_GSDJXXMDL gs = DataAccess.UnitDAL.GetObjectUni_scid(shtyxydm); //工商信息

                    //更新企业资质
                    #region 更新企业资质

                    //企业资质
                    _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(zzjgdm);

                    if (_jcsjk_QY_ZHXXMDL == null)//无资质设为新设立企业
                    {
                        #region 无资质设

                        ////检查企业下是否有无二级和二级临时证书，无证书自动同步更新企业信息,有证书只更新企业资质、资质等级、资质证书编号。
                        //int CertCount = CommonDAL.GetRowCount("[COC_TOW_Person_BaseInfo]", "*", string.Format(" and [ENT_OrganizationsCode] like '%{0}%' and [PSN_RegisteType] <'07'", zzjgdm));
                        //if (CertCount == 0)
                        //{
                        //    o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                        //    o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                        //    o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址     
                        //}

                        ////建造师与企业资质不符
                        //System.Data.DataTable dtJZS = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07' ", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, _jcsjk_QY_ZHXXMDL.XZDQBM, _jcsjk_QY_ZHXXMDL.ZCDZ));

                        ////从业人员证书与企业资质不符
                        //System.Data.DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and PostTypeID=1 and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更'  and (PostTypeID < 2 or PostTypeID > 2) order by PostTypeID,PostID", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, DateTime.Now.ToString("yyyy-MM-dd")));

                        //if ((dtJZS == null || dtJZS.Rows.Count == 0) && (dtPerson == null || dtPerson.Rows.Count == 0))
                        //{
                        //    o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                        //    o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                        //    o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址    
                        //}


                        //o.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业性质    
                        //o.ENT_Corporate = (gs != null ? gs.CORP_RPT : _jcsjk_QY_ZHXXMDL.FDDBR);//法定代表人
                        //o.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                        //o.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                        //if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                        //{
                        //    o.ENT_Sort = "";
                        //    o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        //}
                        //else
                        //{
                        //    //o.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                        //    if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                        //    {
                        //        if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                        //        {
                        //            o.ENT_Grade = "特级";
                        //        }
                        //        else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                        //        {
                        //            o.ENT_Grade = "一级";
                        //        }
                        //        else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                        //        {
                        //            o.ENT_Grade = "二级";
                        //        }
                        //        else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                        //        {
                        //            o.ENT_Grade = "三级";
                        //        }
                        //        else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                        //        {
                        //            o.ENT_Grade = "不分等级";
                        //        }
                        //        else
                        //        {
                        //            o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                        //    }
                        //    o.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                        //    string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                        //    if (o.ENT_Grade != null)
                        //    {
                        //        foreach (var item in ZZ)
                        //        {
                        //            if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(o.ENT_Grade) == true)
                        //            {
                        //                o.ENT_Sort = item;   //资质序列
                        //                break;
                        //            }

                        //        }
                        //    }
                        //}

                        #endregion 无资质设
                    }
                    else//有资质更新企业资质
                    {
                        #region 有资质

                        if (_jcsjk_QY_ZHXXMDL.SJLX == "本地造价咨询企业")//由于造价咨询企业资质不再更新，所以取工商信息进行更新，替换资质信息
                        {
                            if (gs != null)
                            {
                                _jcsjk_QY_ZHXXMDL.QYMC = gs.ENT_NAME;
                                _jcsjk_QY_ZHXXMDL.ZCDZ = gs.DOM;//注册地址  
                            }
                        }

                        //检查企业下是否有无二级和二级临时证书，无证书自动同步更新企业信息,有证书只更新企业资质、资质等级、资质证书编号。
                        int CertCount = CommonDAL.GetRowCount("[COC_TOW_Person_BaseInfo]", "*", string.Format(" and [ENT_OrganizationsCode] like '%{0}%' and [PSN_RegisteType] <'07'", zzjgdm));
                        if (CertCount == 0)
                        {
                            o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                            o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                            o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址     
                        }

                        //建造师与企业资质不符
                        System.Data.DataTable dtJZS = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07' ", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, _jcsjk_QY_ZHXXMDL.XZDQBM, _jcsjk_QY_ZHXXMDL.ZCDZ));

                        //从业人员证书与企业资质不符
                        System.Data.DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and PostTypeID=1 and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更'  and (PostTypeID < 2 or PostTypeID > 2) order by PostTypeID,PostID", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, DateTime.Now.ToString("yyyy-MM-dd")));

                        if ((dtJZS == null || dtJZS.Rows.Count == 0) && (dtPerson == null || dtPerson.Rows.Count == 0))
                        {
                            o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                            o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                            o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址    
                        }



                        o.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业性质    
                        o.ENT_Corporate = (gs != null ? gs.CORP_RPT : _jcsjk_QY_ZHXXMDL.FDDBR);//法定代表人
                        o.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                        o.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                        if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                        {
                            o.ENT_Sort = "";
                            o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        }
                        else
                        {
                            //o.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                            if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                            {
                                if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                                {
                                    o.ENT_Grade = "特级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                                {
                                    o.ENT_Grade = "一级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                                {
                                    o.ENT_Grade = "二级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                                {
                                    o.ENT_Grade = "三级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                                {
                                    o.ENT_Grade = "不分等级";
                                }
                                else
                                {
                                    o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                }
                            }
                            else
                            {
                                o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                            }
                            o.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                            string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                            if (o.ENT_Grade != null)
                            {
                                foreach (var item in ZZ)
                                {
                                    if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(o.ENT_Grade) == true)
                                    {
                                        o.ENT_Sort = item;   //资质序列
                                        break;
                                    }

                                }
                            }
                        }
                        #endregion 有资质
                        o.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                    }

                    UnitDAL.Update(o);

                    #endregion 更新企业资质
                }
                else
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("没有查到你的企业资质信息，请确认是否已经获取了企业资质！"), true);
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
                    userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2|200", shtyxydm, "", "", zzjgdm, loginTime);//培训点
                }
                else//一般企业
                {
                    userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", qyid, qymc, qyregion, "2", shtyxydm, "", "", zzjgdm, loginTime);//一般企业
                }

                //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
                string _personType = "3";

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
            catch(Exception ex)
            {
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode(string.Format("登录失败。{0}",ex.Message)), true);
            }
        }

        //管理登录
        protected void ButtonaAdmin_Click(object sender, EventArgs e)
        {
            string loginInfo = string.Empty;
            byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

            loginInfo = UTF8Encoding.UTF8.GetString(bytes);

            string[] parts = loginInfo.Split('\\');
            string username = parts[0];
            string password = parts[1];


            var httpCookie = Request.Cookies["ExamCheckCode"];
            if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
            {
                txtValidator.Value = "";
                UIHelp.layerAlert(Page, "请输入正确的验证码！");
                return;
            }

            //if (password != "yi@34wu6")
            //{
            //    UIHelp.layerAlert(Page, "登录失败！");
            //    return;
            //}
            if (Utility.Cryptography.Encrypt(password.Substring(0, password.Length - 4)) != "XrPLui04NGWrPTH1F1d04w==")
            {
                UIHelp.layerAlert(Page, "登录失败！");
                return;
            }

            UserOB userOb = UserDAL.GetObject(username);

            if (userOb == null)
            {
                txtValidator.Value = "";
                TextBoxUserName.Focus();
                UIHelp.layerAlert(Page, "用户名或密码错误！");
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

        #endregion

        ///// <summary>
        ///// 获取二建证书证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
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
        //        p = (p % 36);
        //        if (p == 0) p = 36;
        //        p = p * 2;
        //        p = p % 37;
        //    }
        //    p = 37 - p;
        //    return Char36[p];
        //}
        protected void iniDDLogin()
        {
//              ”北京市住房和城乡建设领域人员资格管理信息系统”在统一身份认证平台注册信息如下 ：
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

            //D:\APP\WEB-INF\classes\systemConfig.properties
           

            //LinkButtonDDDL.PostBackUrl = "https://t200renzheng.zhengtoon.com/open/login/goUserLogin?client_id=100100000393&redirect_uri=http://120.52.185.14:8089&response_type=code&scope=user_info&state=";
              

        }

        ////区县
        //protected void ButtonQX_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("ft", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称 ,7)机构代码      
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);
        //}
        ////区县领导
        //protected void ButtonQXLD_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("ftld", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称  ,7)机构代码     
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);
        //}

        ////注册中心
        //protected void ButtonZCZX_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("mzk", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称   ,7)机构代码    
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);

        //    //UserMDL userOb = UserDAL.GetObject("synergy");


        //    ////角色ID集合
        //    //var roleIDs = new System.Text.StringBuilder();
        //    //DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    //for (int i = 0; i < dt.Rows.Count; i++)
        //    //{
        //    //    roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    //}
        //    //if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    //string deptId = "";
        //    //if (userOb.Dept != null)
        //    //{
        //    //    deptId = userOb.Dept.DeptID;
        //    //}


        //    ////0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称      
        //    //string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", userOb.UserID, userOb.RelUserName, userOb.Organ.RegionID, roleIDs, userOb.Organ.OrganID, deptId, userOb.Organ.OrganName, "");

        //    //userInfo = Cryptography.Encrypt(userInfo);
        //    //FormsAuthentication.SetAuthCookie(userInfo, false);
        //    //Response.Redirect("~/Default.aspx", false);
        //}

        //protected void RadComboBox2_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{

        //}

        ////服务大厅
        //protected void ButtonFWDT_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("fwdt", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称    ,7)机构代码   
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);
        //}

        ////信息中心
        //protected void ButtonXXZX_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("xxzx", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称      
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);
        //}
        ////注册中心领导
        //protected void ButtonJD_Click(object sender, EventArgs e)
        //{
        //    UserMDL userOb = UserDAL.GetObject("jd", "111111");
        //    if (userOb == null)
        //    {
        //        UIHelp.Alert(Page, "用户不存在！");
        //        return;
        //    }

        //    //角色ID集合
        //    var roleIDs = new System.Text.StringBuilder();
        //    DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
        //    }
        //    if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

        //    string deptId = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptId = userOb.Dept.DeptID;
        //    }
        //    string deptName = "";
        //    if (userOb.Dept != null)
        //    {
        //        deptName = userOb.Dept.DeptName;
        //    }
        //    //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称      
        //    string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
        //        , userOb.UserID
        //        , userOb.RelUserName
        //        , userOb.Organ.OrganName
        //        , roleIDs.ToString()
        //        , userOb.Organ.OrganID
        //        , deptId
        //        , userOb.Organ.OrganName == "北京市住房和城乡建设委员会" ? userOb.Organ.OrganName + deptName : userOb.Organ.OrganNature
        //        , userOb.Organ.OrganCode);

        //    userInfo = Cryptography.Encrypt(userInfo);
        //    FormsAuthentication.SetAuthCookie(userInfo, false);
        //    Session["userInfo"] = userInfo;
        //    Response.Redirect("~/Default.aspx", false);
        //}

        protected void RadioButtonListuserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListuserType.SelectedValue == "个人登录")
            {
                Labelsfzhm.Visible = true;
                RadTextBoxCID.Visible = true;
                ButtonWorker.Visible = true;

                Labelzzjgdm.Visible = false;
                RadTextBoxZZJGDM.Visible = false;
                ButtonQY.Visible = false;

                LabelUserName.Visible = false;
                TextBoxUserName.Visible = false;
                ButtonaAdmin.Visible = false;

            }
            else if (RadioButtonListuserType.SelectedValue == "企业登录")
            {
                Labelsfzhm.Visible = false;
                RadTextBoxCID.Visible = false;
                ButtonWorker.Visible = false;

                Labelzzjgdm.Visible = true;
                RadTextBoxZZJGDM.Visible = true;
                ButtonQY.Visible = true;

                LabelUserName.Visible = false;
                TextBoxUserName.Visible = false;
                ButtonaAdmin.Visible = false;
            }
            else
            {
                Labelsfzhm.Visible = false;
                RadTextBoxCID.Visible = false;
                ButtonWorker.Visible = false;

                Labelzzjgdm.Visible = false;
                RadTextBoxZZJGDM.Visible = false;
                ButtonQY.Visible = false;

                LabelUserName.Visible = true;
                TextBoxUserName.Visible = true;
                ButtonaAdmin.Visible = true;
            }
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

            //特种作业
            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\国标_特种作业.pdf";
            save_pdf_url = @"d:\01test\特种作业.pdf";
            dic = ReadForm(template_url);
            ob = CertificateDAL.GetObject(1294283);
            FillForm(template_url, save_pdf_url, dic, ob);

            ////A
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\三类人A.pdf";
            //save_pdf_url = @"d:\01test\三类人A.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(2131);
            //FillForm(template_url, save_pdf_url, dic, ob);

            ////B
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\三类人B.pdf";
            //save_pdf_url = @"d:\01test\三类人B.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(1156225);
            //FillForm(template_url, save_pdf_url, dic, ob);

            ////C
            //template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\三类人C2.pdf";
            //save_pdf_url = @"d:\01test\三类人C.pdf";
            //dic = ReadForm(template_url);
            //ob = CertificateDAL.GetObject(1094377);
            //FillForm(template_url, save_pdf_url, dic, ob);

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


            //二造
            template_url = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\Template\二级造价师注册证书.pdf";
            save_pdf_url = @"d:\01test\二造.pdf";
            dic = ReadForm(template_url);

            string sql = String.Format(@"Select top 1 * FROM [dbo].[zjs_Certificate]
                                        where  [PSN_RegisterNO] ='建[造]21221100001204'");
            DataTable dtOriginal = (new DBHelper()).GetFillData(sql);
            FillFormOfZJS(template_url, save_pdf_url, dic, dtOriginal.Rows[0]);//填充模板
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
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
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
        /// 填充造价师电子证书模板
        /// </summary>
        /// <param name="pdfTemplate"></param>
        /// <param name="newFile"></param>
        /// <param name="dic"></param>
        /// <param name="dr"></param>
        private void FillFormOfZJS(string pdfTemplate, string newFile, Dictionary<string, string> dic, DataRow dr)
        {
            dic["PSN_Name"] = dr["PSN_Name"].ToString();//姓名
            dic["PSN_Sex"] = dr["PSN_Sex"].ToString();//性别
            //dic["PSN_CertificateNO"] = dr["PSN_CertificateNO"].ToString();//身份证号
            dic["PSN_BirthDate"] = Convert.ToDateTime(dr["PSN_BirthDate"]).ToString("yyyy年M月d日");//生日
            dic["ENT_Name"] = dr["ENT_Name"].ToString();//单位
            dic["PSN_RegisterNO"] = dr["PSN_RegisterNO"].ToString();//注册号
            dic["PSN_CertificationDate"] = Convert.ToDateTime(dr["PSN_RegistePermissionDate"]).ToString("yyyy年M月d日"); //发证日期（签章日期）
            dic["PSN_CertificateValidity"] = string.Format("{0}-{1}", Convert.ToDateTime(dr["PSN_CertificationDate"]).ToString("yyyy年M月d日"), Convert.ToDateTime(dr["PSN_CertificateValidity"]).ToString("yyyy年M月d日"));//有效期
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
                string iTextAsianCmaps_Path = string.Format(@"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\bin\iTextAsianCmaps.dll");
                string iTextAsian_Path = string.Format(@"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\bin\iTextAsian.dll");
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
                //image.SetAbsolutePosition(410, 465);
                waterMarkContent.AddImage(image);//加水印

                //输出二维码  
                string key = Utility.Cryptography.Encrypt(string.Format("{0},{1}", dr["CertificateCAID"], dr["PSN_RegisterNO"]));
                System.Drawing.Image imgtemp = Utility.ImageHelp.CreateQRCode(string.Format("http://zjw.beijing.gov.cn/cyry/PersonnelFile/CertCheckJZS.aspx?o={0}", key), 200, 200);
                iTextSharp.text.Image imgCode = iTextSharp.text.Image.GetInstance(imgtemp, Color.BLACK);
                imgCode.ScaleAbsolute(71, 71);
                imgCode.SetAbsolutePosition(100, 160);
                //imgCode.SetAbsolutePosition(60, 100);
                waterMarkContent.AddImage(imgCode);//加水印

                //输出签名  
                //iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance(GetSignPhotoJZS(dr["PSN_CertificateNO"].ToString()));
                iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance(GetSignPhotoJZS(dr["PSN_RegisterNo"].ToString(), dr["PSN_CertificateNO"].ToString()));
                img_qm.GrayFill = 100;//透明度，灰色填充
                img_qm.ScaleAbsolute(99, 43);
                //img_qm.ScaleAbsolute(189, 72);
                img_qm.SetAbsolutePosition(190, 150);
                waterMarkContent.AddImage(img_qm);//加水印

                ////输出签名  
                //iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance("D:/work/北京市住房和城乡建设领域人员资格管理信息系统/code/人员资格解决方案/ZYRYJG/Images/SignImg.jpg");
                //img_qm.GrayFill = 100;//透明度，灰色填充
                //img_qm.ScaleAbsolute(99, 43);
                ////img_qm.ScaleAbsolute(189, 72);
                //img_qm.SetAbsolutePosition(160, 120);
                //waterMarkContent.AddImage(img_qm);//加水印

                ////红章
                //iTextSharp.text.Image imageHZ = iTextSharp.text.Image.GetInstance(string.Format("D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG/Images/chapter01ddd.png"));
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
            foreach(string s in zy)
            {
                sb.Append(formatZY(s)).Append("\n\n");
            }
            dic["PSN_RegisteProfession"] = sb.ToString();
            //if (zy.Length > 3)//多余3个专业，每行显示两个
            //{
            //    for (int i = 0; i < zy.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(zy[i]) == true)
            //        {
            //            continue;
            //        }

            //        sb.Append(formatZY(zy[i]));
            //        if (i % 2 == 1)
            //        {
            //            dic[string.Format("PSN_RegisteProfession{0}", i / 2)] = sb.ToString();
            //            sb.Remove(0, sb.Length);
            //        }
            //    }
            //    if (sb.Length > 0)
            //    {
            //        dic[string.Format("PSN_RegisteProfession{0}", sb.Length / 2)] = sb.ToString();
            //    }
            //}
            //else//少余3个专业，每行显示一个专业
            //{
            //    for (int i = 0; i < zy.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(zy[i]) == true)
            //        {
            //            continue;
            //        }
            //        dic[string.Format("PSN_RegisteProfession{0}", i)] = formatZY(zy[i]);
            //    }
            //}

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
                string iTextAsianCmaps_Path = string.Format(@"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\bin\iTextAsianCmaps.dll");
                string iTextAsian_Path = string.Format(@"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG\bin\iTextAsian.dll");
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
                //img_qm.ScaleAbsolute(189, 72);
                img_qm.SetAbsolutePosition(160, 120);
                waterMarkContent.AddImage(img_qm);//加水印

                ////输出签名  
                //iTextSharp.text.Image img_qm = iTextSharp.text.Image.GetInstance("D:/work/北京市住房和城乡建设领域人员资格管理信息系统/code/人员资格解决方案/ZYRYJG/Images/SignImg.jpg");
                //img_qm.GrayFill = 100;//透明度，灰色填充
                //img_qm.ScaleAbsolute(99, 43);
                ////img_qm.ScaleAbsolute(189, 72);
                //img_qm.SetAbsolutePosition(160, 120);
                //waterMarkContent.AddImage(img_qm);//加水印

                ////红章
                //iTextSharp.text.Image imageHZ = iTextSharp.text.Image.GetInstance(string.Format("D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG/Images/chapter01ddd.png"));
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

        /// <summary>
        /// 获取建造师证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
        /// </summary>
        /// <param name="PSN_RegisterNO">二建注册证书注册号</param>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns></returns>
        public string GetFaceImagePathJZS(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string ExamWebRoot = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG";
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
        /// 获取个人签名照
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns></returns>
        public string GetSignPhotoJZS(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string ExamWebRoot = @"D:\work\北京市住房和城乡建设领域人员资格管理信息系统\code\人员资格解决方案\ZYRYJG";
            string imgPath = "";
            string signPath = COC_TOW_Person_FileDAL.GetFileUrl(PSN_RegisterNO, EnumManager.FileDataTypeName.手写签名照);
            if (string.IsNullOrEmpty(signPath) == false)
            {
                if (File.Exists(signPath.Replace("~", ExamWebRoot)) == true)
                {
                    imgPath = signPath.Replace("~", ExamWebRoot);
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

        ////一寸照片识别
        //protected void ButtonCheckImg_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Img1.Src = TextBoxCheckImg.Text.Trim();
        //        bool ifwhite = UIHelp.CheckIfWhiteBackgroudPhoto(Server.MapPath(TextBoxCheckImg.Text.Trim()));
        //        UIHelp.layerAlert(Page, string.Format("{0}白底一寸照片。", ifwhite == true ? "是" : "不是"));
        //    }
        //    catch { }
           
        //}

        //protected void ButtonTest_Click(object sender, EventArgs e)
        //{
        //    //appMark = "BZFGLXXXT";
        //    //appWord = "666BZFGLXXXT90fZp";
        //    //publicKey = "040160054ff31ee703c1b678ad38bab57b857e52af3bf1c33e07a5aef203639c0c383a635cc2f2f87573faabd8cecc7f355c8af8ada8ad90df816472a55ac4b7c7";
        //    //privateKey = "008f29f61bc2ff7bedf2e114532e3c93ed1aa1810507a23933c77dba2c29deea2a";
        //    //source="BZFGLXXXT";
        //    //系统名称 = "北京市住房和城乡建设领域人员资格管理信息系统";
        //    //地址 = "http://120.52.185.14/";
            		

            
        //    p_haoCaPing.InnerText = "";

        //    try
        //    {
        //        //string apiUrl = string.Format("http://43.254.24.244/pubservice/appraise4OT/reShareApprInfo?access_token={2}", Constants.BaseUrl, itemCode, accessToken);

        //        //FileLog.WriteLog(string.Format("CreatePDF，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));

        //        //return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateResponseResult>(Utility.HttpHelp.DoPost(apiUrl,Utility.JSONHelp.Encode(model)));

        //        //测试地址
        //        //string apiUrl = "http://43.254.24.244/pubservice/appraise4OT/reShareApprInfo?";

        //        //正式地址
        //        //string apiUrl = "http://banshi.beijing.gov.cn/pubservice/appraise4OT/reShareApprInfo?";

        //        string apiUrl = string.Format("{0}/pubservice/appraise4OT/reShareApprInfo?",TextBoxUrl.Text.Trim());

        //        AppraiseMDL _AppraiseMDL = new AppraiseMDL();
        //        _AppraiseMDL.appMark = "BZFGLXXXT";
        //        _AppraiseMDL.time = DateTime.Now.ToString("yyyyMMddHHmmss");

        //        #region 服务事项信息

        //        DataTable dt = CommonDAL.GetDataTable("SELECT top 1 *  FROM [dbo].[VIEW_CERTIFICATECHANGE]   where [NOTICEDATE] >'2000-1-1' and posttypeid =1 and [CHANGETYPE]='京内变更' and len([LINKWAY])>1  order by [NOTICEDATE] desc");

        //        projectServiceMDL _projectService = new projectServiceMDL();

        //        /// 实施编码
        //        _projectService.taskCode = "594162DC7B50C320E050007F010070CD";

        //        /// 业务办理项编码
        //        _projectService.taskHandleItem = "";

        //        /// 事项名称
        //        _projectService.taskName = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核证书变更";

        //        /// 事项主题
        //        _projectService.subMatter = "";

        //        /// 办件编号
        //        _projectService.projectNo = "0014BGSQ" + dt.Rows[0]["CERTIFICATECHANGEID"].ToString();

        //        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //        _projectService.proStatus = "3";

        //        /// 受理部门编码        
        //        _projectService.orgcode = "11110000000021135M";

        //        /// 受理部门名称
        //        _projectService.orgName = "市建委";

        //        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //        _projectService.acceptDate = Convert.ToDateTime(dt.Rows[0]["GETDATE"]).ToString("yyyy-MM-dd HH:mm:ss");

        //        /// 申请人类型 自然人=1，企业法人=2，事业法人=3，社会组织法人=4，非法人企业=5，行政机关=6，其他组织=9
        //        _projectService.userProp = "1";

        //        /// 申请单位名称/申请人名称
        //        //_projectService.userName = dt.Rows[0]["WORKERNAME"].ToString();
        //        _projectService.userName = "廖亮";//测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用

        //        /// 申请人证件类型（见 A.2证件类型）
        //        _projectService.userPageType = "111";

        //        /// 申请人证件号码
        //        //_projectService.certKey = dt.Rows[0]["NEWWORKERCERTIFICATECODE"].ToString();
        //        _projectService.certKey = "210504197705200015";//测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用

        //        /// 代理人姓名
        //        _projectService.handleUserName = "";

        //        /// 代理人证件类型（见 A.2证件类型）
        //        _projectService.handleUserPageType = "";

        //        /// 代理人证件号码
        //        _projectService.handleUserPageCode = "";

        //        /// 服务名称 咨询、申报、补正、缴费、签收（isService 值为1 时必填）
        //        _projectService.serviceName = "申报";

        //        /// 是否为好差评服务办件1-是，0-否（当产生咨询、申报、补正、缴费及办结等服务时，该字段值为 1）
        //        _projectService.isService = "1";

        //        /// 服务时间 yyyy-MM-dd HH:mm:ss （isService 值为1 时必填）
        //        _projectService.serviceTime = Convert.ToDateTime(dt.Rows[0]["GETDATE"]).ToString("yyyy-MM-dd HH:mm:ss");

        //        /// 服务日期 yyyy-MM-dd（isService 值为1 时必填）
        //        _projectService.serviceDate = Convert.ToDateTime(dt.Rows[0]["GETDATE"]).ToString("yyyy-MM-dd");

        //        /// 数据来源（默认值为 111）
        //        _projectService.dataSource = "111";

        //        /// 办件类型： 即办件=1，承诺办件=2
        //        _projectService.projectType = 2;

        //        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //        _projectService.resultDate = Convert.ToDateTime(dt.Rows[0]["NOTICEDATE"]).ToString("yyyy-MM-dd HH:mm:ss");

        //        /// 申请时间 办件申请、补正必填
        //        _projectService.applydate = Convert.ToDateTime(dt.Rows[0]["APPLYDATE"]).ToString("yyyy-MM-dd HH:mm:ss");

        //        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
        //        _projectService.bj_zt = "45";//办结

        //        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //        _projectService.taskguid = "24ace28c-9565-11e9-8300-507b9d3e4710";//从下发Excel列表中获取

        //        /// 办件详情查看地址（需要是对接了统一认证平台的单点登录地址）
        //        _projectService.statusUrl = "";

        //        /// 行政区划（见 A.3 北京市行政区划代码）
        //        _projectService.administrative_div = "00";

        //        /// 联系人手机号
        //        //_projectService.telPhone = dt.Rows[0]["LINKWAY"].ToString();
        //        _projectService.telPhone = "13671197677";//测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用测试专用


        //        /// 服务数据初始来源标识，默认与 appMark 值相同
        //        _projectService.source = _AppraiseMDL.appMark;

        //        /// 服务数据来源，默认与appMark 值相同
        //        _projectService.sourceTJ = _AppraiseMDL.appMark;

        //        /// 位行政区划（非太极不用填写，太极必填）
        //        _projectService.areaCode = "";

        //        #endregion

        //        projectServiceParams _projectServiceParams= new projectServiceParams();
        //        _projectServiceParams.projectService=_projectService;

        //        string pubk = "040160054ff31ee703c1b678ad38bab57b857e52af3bf1c33e07a5aef203639c0c383a635cc2f2f87573faabd8cecc7f355c8af8ada8ad90df816472a55ac4b7c7";//公钥

        //        string _params = string.Format(@"[{0}]", Utility.JSONHelp.Encode(_projectServiceParams));

        //        //_AppraiseMDL.Params = string.Format(@"{{""data"":{0}}}", SM2Utils.Encrypt(Org.BouncyCastle.Utilities.Encoders.Hex.Decode(pubk), Encoding.Default.GetBytes(_params)));
        //         //_AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", SM2Utils.Encrypt(Org.BouncyCastle.Utilities.Encoders.Hex.Decode(pubk), Encoding.UTF8.GetBytes(_params)));
        //        _AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", SM2Utils.Encrypt(pubk, _params));
        //         //_AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", _params);


               
        //        //SM2(time+appMark+appWord)
        //        //byte[] sourceData = Encoding.Default.GetBytes(string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));
        //         //byte[] sourceData = Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));
        //         _AppraiseMDL.sign = SM2Utils.Encrypt(pubk, string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));


        //        //p_haoCaPing.InnerText = Utility.JSONHelp.Encode(_AppraiseMDL);
        //        //return;


        //        //Utility.FileLog.WriteLog(string.Format("测试：{0}", Utility.JSONHelp.Encode(_AppraiseMDL)));
        //        //return;

        //        //string rtn = Utility.HttpHelp.DoPost(apiUrl, Utility.JSONHelp.Encode(_AppraiseMDL));
        //        string rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("appMark={0}&sign={1}&params={2}&time={3}", _AppraiseMDL.appMark, _AppraiseMDL.sign, _AppraiseMDL.Params, _AppraiseMDL.time));
        //        p_haoCaPing.InnerText = rtn;
        //        Utility.FileLog.WriteLog(string.Format("推送好差评业务信息返回结果：{0}", rtn));
        //        AppraiseResultMDL _AppraiseResultMDL = Newtonsoft.Json.JsonConvert.DeserializeObject<AppraiseResultMDL>(rtn);

        //        ToInputHaoChaPing(_AppraiseResultMDL.serviceCode);

        //        //AppraiseResultMDL _AppraiseResultMDL = Newtonsoft.Json.JsonConvert.DeserializeObject<AppraiseResultMDL>(Utility.HttpHelp.DoPost(apiUrl, Utility.JSONHelp.Encode(_AppraiseMDL)));
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "发送办件服务数据到好差评系统错误", ex);
        //    }
        //}

        //protected void ToInputHaoChaPing(string _serviceCode)
        //{
        //    //string apiUrl = "http://43.254.24.244/pubservice/appraise4PCOthers/pre?";
        //    string client_secret = "7f243b9d28e04ea6bdc0cfd970b51ea8";//大厅加密秘钥
        //    string source = "BZFGLXXXT";//系统唯一标识
        //    string serviceCode = _serviceCode;//服务ID，调用好差评信息推送接口返回
        //    string token = SM2Utils.generate(client_secret + source);

        //    HyperLink1.NavigateUrl = string.Format("{4}/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", serviceCode, "", source, token, TextBoxUrl.Text.Trim());
        //    p_haoCaPing.InnerText = HyperLink1.NavigateUrl;
        //    Utility.FileLog.WriteLog(string.Format("好差评链接：{0}", HyperLink1.NavigateUrl));

        //    Response.Redirect(HyperLink1.NavigateUrl, true);



        //}

        //protected void ButtonInputHaoCha_Click(object sender, EventArgs e)
        //{
        //    string apiUrl = "http://43.254.24.244/pubservice/appraise4PCOthers/pre?";
        //    string client_secret="7f243b9d28e04ea6bdc0cfd970b51ea8";//大厅加密秘钥
        //    string source = "BZFGLXXXT";//系统唯一标识
        //    string serviceCode = "000FW112020092700000038";//服务ID，调用好差评信息推送接口返回
        //    string token = SM2Utils.generate(client_secret + source);


        //    HyperLink1.NavigateUrl = string.Format("http://43.254.24.244/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", serviceCode, "", source, token);
            
        //}

        //protected void ButtonDDDL_Click(object sender, EventArgs e)
        //{

        //}

        //protected  List<string> Char36 = new List<string>(){ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        //private string GetZZBS_CheckCode(string ZZBS)
        //{
        //    // "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //    char[] list = ZZBS.ToCharArray();
        //    int p = 36;
        //    int index = 0;
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
    }
}