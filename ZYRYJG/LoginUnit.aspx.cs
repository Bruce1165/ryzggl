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
    public partial class LoginUnit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["ticket"]) == true)
                {
                    Response.Redirect("http://110.43.71.80/qykj", true);//跳转到企业空间登录页面
                    return;
                }

                string qyid = "";//企业ID
                string qymc = "";//企业名称
                string qyregion = "";//所属区县
                string zzjgdm = "";//组织机构代码
                string shtyxydm = "";//社会统一信用代码
                string userInfo = "";//用户登录信息
                string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

                System.Collections.Hashtable head =  new System.Collections.Hashtable();
                head.Add("app-secret","ad5ddbf3-65c0-4757-afd1-064e0627aa0d");
                try
                {
                    //获取企业登录信息
                    //string rtn = Utility.HttpHelp.HttpConnect(string.Format("http://110.43.71.80/api/admin/third/verifyTicket/{0}", Request["ticket"]), "GET", null, head);
                    string rtn = Utility.HttpHelp.HttpConnect(string.Format("http://192.169.226.121/api/admin/third/verifyTicket/{0}", Request["ticket"]), "GET", null, head);
                
                    //Utility.FileLog.WriteLog(rtn);
                    //Response.Write(rtn);
                    if (null == rtn)
                    {
                      
                        Utility.FileLog.WriteLog(string.Format("用户集成认证失败！获取企业信息返回空。{0}", Request["ticket"]));
                        Response.Write("用户集成认证失败！获取企业信息返回空。");
                        return;
                    }
                    else
                    {
                        Utility.JsonTool jsonTool = new Utility.JsonTool(rtn);

                        if(jsonTool.getValue("code") !="0")
                        {
                            Utility.FileLog.WriteLog(string.Format("用户集成认证失败！获取企业信息返回空。{0}", Request["ticket"]));
                            Response.Write("用户集成认证失败！获取企业信息返回空。");
                            return;
                        }
            
                        //Utility.JsonTool unit = new Utility.JsonTool(jsonTool.getValue("data"));



                        shtyxydm = jsonTool.getValue("data.tyshxydm");//社会统一信用代码
                        if (shtyxydm.Length == 18)
                        {
                            zzjgdm = Utility.Check.GetZZJGDMFromCreditCode(shtyxydm);
                        }
                        else
                        {
                            zzjgdm = shtyxydm;
                        }

                        if (zzjgdm.Length != 9)
                        {
                            FileLog.WriteLog(string.Format("企业集成登录失败，企业信息错误，社会统一信用代码：{0}", shtyxydm));
                            Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("企业信息错误，请检查社会统一信用代码！"), false);
                            return;
                        }

                        GSJ_QY_GSDJXXMDL gs = DataAccess.UnitDAL.GetObjectUni_scid(shtyxydm); //工商信息                                                
                        UnitMDL o = UnitDAL.GetObjectByENT_OrganizationsCode(shtyxydm);//企业基本信息
                        jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL;//企业资质

                        if (o != null)//已存在企业基本信息表中，更新企业资质、资质等级、资质证书编号
                        {
                            qyid = o.UnitID;//企业ID
                            qymc = o.ENT_Name;//企业名称
                            qyregion = o.ENT_City;//区县
                            //把大厅接口的社会统一信用代码保存到本地unit表
                            o.CreditCode = shtyxydm;
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
                                        o.ENT_Name = jsonTool.getValue("data.qymc");//企业名称
                                        o.END_Addess = jsonTool.getValue("data.djdz");//注册地址
                                        o.ENT_Corporate = jsonTool.getValue("data.fddbr");//法定代表人
                                    }
                                    qymc = o.ENT_Name;//企业名称
                                }
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
                        }
                        else//无企业信息（第一次登陆），去企业资质库查询,并插入到企业表
                        {
                            o = new UnitMDL();

                            //企业资质
                            _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(zzjgdm);

                            if (_jcsjk_QY_ZHXXMDL != null)//有资质
                            {
                                #region 有资质
                                o.UnitID = Guid.NewGuid().ToString();
                                o.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                                o.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                                o.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                                o.ENT_OrganizationsCode = zzjgdm;//组织机构代码
                                o.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                                o.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM; //区县
                                o.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                                o.ENT_Corporate = (gs != null ? gs.CORP_RPT : _jcsjk_QY_ZHXXMDL.FDDBR);//法定代表人
                                o.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                                o.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                                if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                                {
                                    o.ENT_Sort = "";
                                    o.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                }
                                else
                                {

                                    //企业资质等级
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
                                o.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                                o.CreditCode = shtyxydm;//社会统一信用代码
                                o.Valid = 1;//是否有效
                                o.ResultGSXX = 0;

                                #endregion 有资质
                            }
                            else//无资质设置为新设立企业
                            {
                                #region 无有资质

                                o.UnitID = Guid.NewGuid().ToString();
                                o.ENT_Name = (gs != null ? gs.ENT_NAME : jsonTool.getValue("data.qymc"));//企业名称
                                o.ENT_OrganizationsCode = zzjgdm;//组织机构代码
                                o.CreditCode = shtyxydm;//社会统一信用代码
                                o.Valid = 1;//是否有效

                                o.ResultGSXX = 0;

                                DateTime dtime = new DateTime();
                                bool ifhavecjrq = false;
                                try
                                {
                                    ifhavecjrq = DateTime.TryParse(jsonTool.getValue("data.clrq"), out dtime);
                                }
                                catch { }
                                if (ifhavecjrq == true)
                                {
                                    o.BeginTime = dtime;//成立日期
                                }
                                else
                                {
                                    o.BeginTime = DateTime.Now;//建立时间
                                }
                                o.EndTime = Convert.ToDateTime("2500-01-01");//截止时间

                                //_UnitMDL.ENT_Economic_Nature = corpinfo.Corp_Property;//企业类型
                                o.ENT_City = "";//区县
                                o.END_Addess = (gs != null ? gs.DOM : jsonTool.getValue("data.djdz"));//注册地址
                                o.ENT_Corporate = (gs != null ? gs.CORP_RPT : jsonTool.getValue("data.fddbr"));//法定代表人
                                o.ENT_Type = "";  //企业类型
                                o.ENT_Sort = "新设立企业";   //企业类别
                                o.ENT_Grade = "新设立企业"; //企业资质等级
                                o.ENT_QualificationCertificateNo = "无";  //企业资质证书编号                              
                                o.Memo = "新设立企业";

                                #endregion 无有资质
                            }
                            o.CJSJ = DateTime.Now;
                            o.CJR = "企业空间同步";
                            if (gs != null)//更新验证状态
                            {
                                o.ENT_Name = gs.ENT_NAME;//企业名称
                                o.END_Addess = gs.DOM;//注册地址
                                o.ENT_Corporate = gs.CORP_RPT;//法定代表人
                            }
                            UnitDAL.Insert(o);
                            //Cookie串里面的信息
                            qyid = o.UnitID;//企业ID
                            qymc = o.ENT_Name;//企业名称
                            qyregion = o.ENT_City;//区县                           
                        }

                        #region 更新工商验证状态

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

                        #endregion 更新工商验证状态

                        //是否为央企或大集团（有子公司）
                        int countUnit = CommonDAL.GetRowCount("[USER]", "1", string.Format(" and [LICENSE]='{0}' and ([ORGANID] =246 or [ORGANID] =247)", zzjgdm));

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

                        userInfo = Cryptography.Encrypt(userInfo);
                        FormsAuthentication.SetAuthCookie(userInfo, false);
                        Session["userInfo"] = userInfo;
                        try
                        {
                            UIHelp.WriteOperateLog(qymc, qyid, "登录", "企业空间统一身份认证登录");
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
                    }

                }
                catch (Exception ex)
                {
                    Utility.FileLog.WriteLog("企业空间用户集成认证失败！", ex);
                    Response.Write(string.Format("企业用户集成认证失败！{0}。", ex.Message));                    
                    return;
                }
            }
        }

    }
}