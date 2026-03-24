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
    public partial class Login2 : System.Web.UI.Page
    {
        public static string RootUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string executionPath = context.Request.ApplicationPath;
                return string.Format("{0}://{1}{2}", context.Request.Url.Scheme, context.Request.Url.Authority,
                    executionPath != null && executionPath.Length == 1 ? string.Empty : executionPath);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(Request.QueryString["time"])==true)
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("非法访问！"), false);
                    return;
                }
                string checkTime = Base64Decode(Encoding.UTF8, Base64Decode(Encoding.UTF8, Request.QueryString["time"]));
                checkTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", checkTime.Substring(0, 4), checkTime.Substring(4, 2), checkTime.Substring(6, 2)
                    , checkTime.Substring(8, 2), checkTime.Substring(10, 2), checkTime.Substring(12, 2));
                if (Convert.ToDateTime(checkTime).AddSeconds(30) < DateTime.Now)
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("集成登录已失效，请重新登录！"), false);
                    return;
                }
      

                string userInfo = "";//用户登录信息
                string userType = "";//用户类型("无效","企业","个人")

                jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL;//企业资质

                EGovThirdServiceSoapClient client = new EGovThirdServiceSoapClient();
                EGovSoapHead heda = new EGovSoapHead();

                int userid = Convert.ToInt32(Base64Decode(Encoding.UTF8, Base64Decode(Encoding.UTF8, Request.QueryString["Userid"])));
             

                //int userid = Convert.ToInt32(Request.QueryString["Userid"]);//大厅用户ID       
                userType = client.CheckIsCorpOrPersonUserByUserID(ref heda, userid);
                string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
                string _personType = "";
                switch (userType)
                {
                    case "无效":
                        FileLog.WriteLog("服务大厅集成登陆身份验证失败，用户类型userType=无效");
                        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("系统忙，服务大厅集成登陆身份验证失败，用户类型无效！"), false);
                        return;
                    case "企业":
                        string qyid = "";//企业ID
                        string qymc = "";//企业名称
                        string qyregion = "";//所属区县
                        string zzjgdm = "";//组织机构代码
                        string shtyxydm = "";//社会统一信用代码

                        //大厅用户信息
                        CorpInfo corpinfo = client.GetCorpInfoByUserID(ref heda, userid);
                        if (corpinfo == null)
                        {
                            FileLog.WriteLog("服务大厅集成登陆身份验证失败GetCorpInfoByUserID()无返回结果");
                            Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("系统忙，服务大厅集成登陆身份验证失败！"), false);
                            return;
                        }

                        #region 企业登录

                        try
                        {
                            zzjgdm = corpinfo.Dept_Code;//组织机构代码
                            shtyxydm = corpinfo.Credit_Code;//社会统一信用代码

                            //????????????????????????????????????????????集成统一登录平台用户的userid是什么？
                            //corpinfo.CorpGuid

                            if (zzjgdm.Length != 9)
                            {
                                FileLog.WriteLog(string.Format("企业大厅集成登录失败，企业信息错误，组织机构代码：{0}", zzjgdm));
                                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("企业信息错误，请检查社会统一信用代码（组织机构代码）！"), false);
                                return;
                            }

                            GSJ_QY_GSDJXXMDL gs = DataAccess.UnitDAL.GetObjectUni_scid(shtyxydm); //工商信息

                            //企业基本信息
                            UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(zzjgdm);
                            if (o != null)//已存在企业基本信息表中，更新企业资质、资质等级、资质证书编号
                            {
                                qyid = o.UnitID;//企业ID
                                qymc = o.ENT_Name;//企业名称
                                qyregion = o.ENT_City;//区县
                                //把大厅接口的社会统一信用代码保存到本地unit表
                                o.CreditCode = corpinfo.Credit_Code;

                                o.ENT_OrganizationsCode = zzjgdm;//组织机构代码

                                //企业资质
                                _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(zzjgdm);

                                //更新企业资质
                                if (_jcsjk_QY_ZHXXMDL == null)//无资质设为新设立企业
                                {
                                    #region 无资质
                                    o.ENT_Sort = "新设立企业";   //企业类别
                                    o.ENT_Grade = "新设立企业"; //企业资质等级
                                    o.ENT_QualificationCertificateNo = "无";//企业资质证书编号

                                    //检查企业下是否有无二级和二级临时证书，无证书自动同步更新企业信息,有证书只更新企业资质、资质等级、资质证书编号。
                                    int CertCount = CommonDAL.GetRowCount("[COC_TOW_Person_BaseInfo]", "*", string.Format(" and [ENT_OrganizationsCode] like '%{0}%' and [PSN_RegisteType] <'07'", zzjgdm));
                                    if (CertCount == 0)
                                    {
                                        if (gs != null)//根据工商更新
                                        {
                                            o.ENT_Name = gs.ENT_NAME;//企业名称
                                            o.END_Addess = gs.DOM;//注册地址
                                            o.ENT_Corporate = gs.CORP_RPT;//法定代表人
                                        }
                                        else//从大厅获取企业信息
                                        {
                                            o.ENT_Name = corpinfo.Corp_Name;//企业名称
                                            o.END_Addess = corpinfo.Reg_Address;//注册地址
                                            o.ENT_Corporate = corpinfo.Corp_Person;//法定代表人

                                            o.ResultGSXX = 0;
                                            o.ApplyTimeGSXX = null;
                                            UnitDAL.UpdateResultGSXX(o);
                                        }
                                        qymc = o.ENT_Name;//企业名称
                                        //o.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅没有需企业自己维护。
                                    }
                                    //if (o.ENT_Name != corpinfo.Corp_Name)
                                    //{
                                    //    o.ENT_Name = corpinfo.Corp_Name;//企业名称
                                    //    qymc = o.ENT_Name;//企业名称
                                    //    o.ResultGSXX = 0;
                                    //    o.ApplyTimeGSXX = null;
                                    //    UnitDAL.UpdateResultGSXX(o);
                                    //}
                                    #endregion 无资质
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
                                        if (gs != null)//根据工商更新
                                        {
                                            o.ENT_Name = gs.ENT_NAME;//企业名称
                                            o.END_Addess = gs.DOM;//注册地址
                                            o.ENT_Corporate = gs.CORP_RPT;//法定代表人
                                            o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                                        }
                                        else
                                        {
                                            o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                                            o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                                            o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址 
                                        }
                                    }

                                    //建造师与企业资质不符
                                    System.Data.DataTable dtJZS = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07' ", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, _jcsjk_QY_ZHXXMDL.XZDQBM, _jcsjk_QY_ZHXXMDL.ZCDZ));

                                    //从业人员证书与企业资质不符
                                    System.Data.DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and PostTypeID=1 and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更'  and (PostTypeID < 2 or PostTypeID > 2) order by PostTypeID,PostID", zzjgdm, _jcsjk_QY_ZHXXMDL.QYMC, DateTime.Now.ToString("yyyy-MM-dd")));

                                    if ((dtJZS == null || dtJZS.Rows.Count == 0) && (dtPerson == null || dtPerson.Rows.Count == 0))
                                    {
                                        if (gs != null)//根据工商更新
                                        {
                                            o.ENT_Name = gs.ENT_NAME;//企业名称
                                            o.END_Addess = gs.DOM;//注册地址
                                            o.ENT_Corporate = gs.CORP_RPT;//法定代表人
                                        }
                                        else
                                        {
                                            o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;
                                            o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;
                                            o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址   
                                        }
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

                                qyregion = o.ENT_City;

                                if (gs != null)//更新验证状态
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
                            else//无企业信息（第一次登陆），去企业资质库查询,并插入到企业表
                            {
                                UnitMDL _UnitMDL = new UnitMDL();

                                //企业资质
                                _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(zzjgdm);

                                if (_jcsjk_QY_ZHXXMDL != null)//有资质
                                {
                                    #region 有资质
                                    _UnitMDL.UnitID = Guid.NewGuid().ToString();
                                    _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                                    _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                                    _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                                    _UnitMDL.ENT_OrganizationsCode = zzjgdm;//组织机构代码
                                    _UnitMDL.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                                    _UnitMDL.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM; //区县
                                    _UnitMDL.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                                    _UnitMDL.ENT_Corporate = (gs != null ? gs.CORP_RPT : _jcsjk_QY_ZHXXMDL.FDDBR);//法定代表人
                                    _UnitMDL.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                                    _UnitMDL.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                                    if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                                    {
                                        _UnitMDL.ENT_Sort = "";
                                        _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                    }
                                    else
                                    {

                                        //企业资质等级
                                        if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                                        {
                                            if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                                            {
                                                _UnitMDL.ENT_Grade = "特级";
                                            }
                                            else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                                            {
                                                _UnitMDL.ENT_Grade = "一级";
                                            }
                                            else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                                            {
                                                _UnitMDL.ENT_Grade = "二级";
                                            }
                                            else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                                            {
                                                _UnitMDL.ENT_Grade = "三级";
                                            }
                                            else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                                            {
                                                _UnitMDL.ENT_Grade = "不分等级";
                                            }
                                            else
                                            {
                                                _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                            }
                                        }
                                        else
                                        {
                                            _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                                        }
                                        _UnitMDL.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                                        string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                                        if (_UnitMDL.ENT_Grade != null)
                                        {
                                            foreach (var item in ZZ)
                                            {
                                                if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(_UnitMDL.ENT_Grade) == true)
                                                {
                                                    _UnitMDL.ENT_Sort = item;   //资质序列
                                                    break;
                                                }

                                            }
                                        }

                                    }
                                    _UnitMDL.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                                    _UnitMDL.CreditCode = corpinfo.Credit_Code;//社会统一信用代码
                                    _UnitMDL.Valid = 1;//是否有效
                                    _UnitMDL.ResultGSXX = 0;

                                    #endregion 有资质
                                }
                                else//无资质设置为新设立企业
                                {
                                    _UnitMDL.UnitID = Guid.NewGuid().ToString();
                                    _UnitMDL.ENT_Name = (gs != null ? gs.ENT_NAME:corpinfo.Corp_Name);//企业名称
                                    _UnitMDL.ENT_OrganizationsCode = zzjgdm;//组织机构代码
                                    _UnitMDL.CreditCode = corpinfo.Credit_Code;//社会统一信用代码
                                    _UnitMDL.Valid = 1;//是否有效

                                    _UnitMDL.ResultGSXX = 0;

                                    DateTime dtime = new DateTime();
                                    bool rtn = DateTime.TryParse(corpinfo.Reg_Date, out dtime);
                                    if (rtn == true)
                                    {
                                        _UnitMDL.BeginTime = Convert.ToDateTime(corpinfo.Reg_Date);//建立时间
                                    }
                                    else
                                    {
                                        _UnitMDL.BeginTime = DateTime.Now;//建立时间
                                    }
                                    _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间

                                    _UnitMDL.ENT_Economic_Nature = corpinfo.Corp_Property;//企业类型
                                    _UnitMDL.ENT_City = "";//区县
                                    _UnitMDL.END_Addess = (gs != null ? gs.DOM : corpinfo.Reg_Address);//注册地址
                                    _UnitMDL.ENT_Corporate = (gs != null ? gs.CORP_RPT : corpinfo.Corp_Person);//法定代表人
                                    _UnitMDL.ENT_Type = "";  //企业类型
                                    _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                                    _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                                    _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号                              
                                    _UnitMDL.Memo = "新设立企业";

                                }
                                _UnitMDL.CJSJ = DateTime.Now;
                                _UnitMDL.CJR = "大厅同步";
                                if (gs != null)//更新验证状态
                                {
                                    _UnitMDL.ENT_Name = gs.ENT_NAME;//企业名称
                                    _UnitMDL.END_Addess = gs.DOM;//注册地址
                                    _UnitMDL.ENT_Corporate = gs.CORP_RPT;//法定代表人
                                }
                                UnitDAL.Insert(_UnitMDL);
                                //Cookie串里面的信息
                                qyid = _UnitMDL.UnitID;//企业ID
                                qymc = _UnitMDL.ENT_Name;//企业名称
                                qyregion = _UnitMDL.ENT_City;//区县

                                if (gs != null)//更新验证状态
                                {
                                    _UnitMDL.ResultGSXX = 2;
                                }
                                else
                                {
                                    _UnitMDL.ResultGSXX = 1;
                                }
                                _UnitMDL.ApplyTimeGSXX = DateTime.Now;
                                UnitDAL.UpdateResultGSXX(_UnitMDL);
                            }
                        }
                        catch(Exception ex)
                        {
                            FileLog.WriteLog("服务大厅集成登陆失。",ex);
                            Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode(string.Format("系统忙，服务大厅集成登陆失败。{0}",ex.Message)), false);
                            return;
                        }

                        #endregion 企业登录

                        //是否为央企或大集团（有子公司）
                        int countUnit = CommonDAL.GetRowCount("[USER]","1",string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)",zzjgdm));

                        //是否为培训点
                        int ifTrainUnit = CommonDAL.GetRowCount("[TrainUnit]", "1", string.Format(" and [UnitCode]='{0}' and [UseStatus]=1 ", shtyxydm));

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
                        _personType = "3";
                        //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, qyid), loginTime, 10);
                        //Session[string.Format("{0}user{1}", _personType, qyid)] = loginTime;

                        userInfo = Cryptography.Encrypt(userInfo);
                        FormsAuthentication.SetAuthCookie(userInfo, false);
                        Session["userInfo"] = userInfo;
                        try
                        {
                            UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业住建委办事大厅统一身份认证登录");
                        }
                        catch { }

                        if (string.IsNullOrEmpty(Request["matterCode"]) == false)
                        {
                            Response.Redirect(string.Format("~/Default.aspx?action={0}", Request["matterCode"] == "ejbgzc" ? "ejChangeUnit" : Request["matterCode"] == "ezbgzc" ? "ezChangeUnit" : Request["matterCode"]), false);
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx", false);
                        }
                       
                        break;
                    case "个人":                     
                        Response.Redirect("https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=", false);
                       return;

                        //OH_PersonInfo _OH_PersonInfo = client.GetPersonInfoByUserID(ref heda, Convert.ToInt32(userid));

                        //#region 个人登录信息
                        //int sex = 1;
                        //string _IDCard = "";//身份证
                        //if (_OH_PersonInfo.perIDCard.Length == 15)
                        //{
                        //    _IDCard = Utility.Check.ConvertoIDCard15To18(_OH_PersonInfo.perIDCard);
                        //    sex = Convert.ToInt32(_IDCard.Substring(14, 1));
                        //}
                        //else if (_OH_PersonInfo.perIDCard.Length == 18)
                        //{
                        //    _IDCard = _OH_PersonInfo.perIDCard.Replace("x", "X");
                        //    sex = Convert.ToInt32(_IDCard.Substring(16, 1));
                        //}
                        //else
                        //{
                        //    _IDCard = _OH_PersonInfo.perIDCard;
                        //    sex = (_OH_PersonInfo.perSEX == "女" ? 2 : 1);
                        //}
                        ////性别
                        //string strSex = "";
                        //if (sex % 2 == 0)
                        //{
                        //    strSex = "女";
                        //}
                        //else
                        //{
                        //    strSex = "男";
                        //}

                        //WorkerOB _WorkerOB = WorkerDAL.GetUserObject(_OH_PersonInfo.perIDCard.Replace("x", "X"));
                        //if (_WorkerOB == null)//new
                        //{
                        //    _WorkerOB = new WorkerOB();
                        //    _WorkerOB.Sex = strSex;   //性别
                        //    if (_IDCard.Length == 18)
                        //    {
                        //        string _birthday = _IDCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                        //        if (Utility.Check.IfDateFormat(_birthday) == true)
                        //        {
                        //            _WorkerOB.Birthday = Convert.ToDateTime(_IDCard.Substring(6, 8).Insert(6, "-").Insert(4, "-")); //出身日期
                        //        }
                        //        _WorkerOB.CertificateType = "身份证";   //证件类别
                        //    }
                        //    else
                        //    {
                        //        _WorkerOB.CertificateType = "其它证件";   //证件类别
                        //    }

                        //    _WorkerOB.CertificateCode = _OH_PersonInfo.perIDCard.Replace("x", "X");  //证件号码
                        //    _WorkerOB.Phone = _OH_PersonInfo.perMobile;  //联系电话
                        //    _WorkerOB.Mobile = _OH_PersonInfo.perMobile;  //联系电话
                        //    _WorkerOB.WorkerName = _OH_PersonInfo.perName;     //姓名
                        //    WorkerDAL.Insert(_WorkerOB);
                        //}
                        //else
                        //{
                        //    if (_IDCard.Length == 18 || _IDCard.Length == 15)
                        //    {
                        //        _WorkerOB.CertificateType = "身份证";   //证件类别
                        //    }
                        //    else
                        //    {
                        //        _WorkerOB.CertificateType = "其它证件";   //证件类别
                        //    }
                        //    _WorkerOB.WorkerName = _OH_PersonInfo.perName;//姓名
                        //    _WorkerOB.Sex = strSex;   //性别
                        //    _WorkerOB.CertificateCode = _OH_PersonInfo.perIDCard.Replace("x", "X");  //证件号码
                        //    _WorkerOB.Phone = _OH_PersonInfo.perMobile;  //联系电话
                        //    _WorkerOB.Mobile = _OH_PersonInfo.perMobile;  //联系电话
                        //    WorkerDAL.Update(_WorkerOB);
                        //}

                        //#endregion 个人登录信息

                        ////0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码，8）用户最后登录时间       
                        //userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);

                        ////用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
                        //_personType = "2";
                        ////Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID), loginTime, 10);
                        ////Session[string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID)] = loginTime;

                        //userInfo = Cryptography.Encrypt(userInfo);
                        //FormsAuthentication.SetAuthCookie(userInfo, false);
                        //Session["userInfo"] = userInfo;
                        //try
                        //{
                        //    UIHelp.WriteOperateLog(_WorkerOB.WorkerName, _WorkerOB.WorkerID.ToString(), "登录", "个人住建委办事大厅统一身份认证登录");
                        //}
                        //catch { }
                        //Response.Redirect("~/Default.aspx", false);
                        //break;
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog("服务大厅集成登陆身份验证失败", ex);
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("系统忙，服务大厅集成登陆身份验证失败！"), false);
            }
        }

        ///// <summary>
        ///// 根据工商注册地址区县和隶属关系返回格式化后的区县
        ///// </summary>
        ///// <param name="XZDQBM">商注册地址中的区县</param>
        ///// <param name="HYLZGX">行业隶属关系</param>
        ///// <returns>格式化后的区县</returns>
        //protected string FormatCity(string XZDQBM,string HYLZGX)
        //{
        //    if (string.IsNullOrEmpty(HYLZGX) == true
        //    || XZDQBM == HYLZGX)
        //    {
        //        return XZDQBM;
        //    }

        //    if("昌平区,丰台区,海淀区,东城区,怀柔区,密云区,朝阳区,顺义区,西城区,延庆区,通州区,石景山区,平谷区,房山区,门头沟区,亦庄,大兴区".Contains(HYLZGX)==true)
        //    {
        //        return HYLZGX;
        //    }

        //    if (HYLZGX.Contains("开发区") == true)
        //    {
        //        return "亦庄";
        //    }
        //    return XZDQBM;
        //}


        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        private string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
    }
}