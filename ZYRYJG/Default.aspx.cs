using System;
using System.Web.Security;
using System.Xml;
using DataAccess;

namespace ZYRYJG
{
    public partial class Default : BasePage
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
                //Utility.FileLog.WriteLog("进入BasePage角色解析PAGELOAD");
                XmlDocument xmldoc = null;
                if (IfExistRoleID("0")==true)//个人                  
                {
                    xmldoc = ResourceDAL.GetResourceULByRoleID("WorkerMenu", "0", true);

                }
                else if (IfExistRoleID("200"))//培训点
                {
                    xmldoc = ResourceDAL.GetResourceULByRoleID("UnitMenu", RoleIDs, true);

                }
                else if (IfExistRoleID("2"))//企业
                {
                    xmldoc = ResourceDAL.GetResourceULByRoleID("UnitMenu", RoleIDs, true);

                }
                else
                {
                    xmldoc = ResourceDAL.GetResourceUl("AdminMenu", UserID, true);
                }

                left_side.InnerHtml = xmldoc.InnerXml;

                ClientScript.RegisterStartupScript(Page.GetType(), "", "$(\".menu ul li a:first\").removeAttr(\"class\");$(\".menu ul li a:first\").addClass(\"active\");$(\".menu ul li ul:first\").css(\"display\",\"block\");", true);

                divNow.InnerText = DateTime.Now.ToString("yyyy年MM月dd日");
                spanLogined.InnerText = string.Format("{0}({1})", UserName,PersonType== 2?"个人":Region);

                //带框架路由
                switch (Request["action"])
                {
                    case "slrkh"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='EXamManage/ExamSignList.aspx';", true);
                        break;
                    case "slrxq"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书续期
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='RenewCertifates/CertifApply.aspx';", true);
                        break;
                    case "slrzx"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书注销		slrzx
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=z';", true);                        
                        break;
                    case "slrbg"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书变更		slrbg
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=j';", true);  
                        break;
                    case "slrjj"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书进京		slrjj
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifEnter/CertifEnterApplyList.aspx';", true);                        
                        break;
                    case "slrzf"://施工单位主要负责人安全生产考核证书增发		                                    slrzf 
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifMoreApplyList.aspx';", true);  
                        break;
                    case "tzzyxq"://建筑施工特种作业人员操作资格考核证书续期		                                tzzyxq
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='RenewCertifates/CertifApply.aspx';", true);
                        break;
                    case "tzzybg"://建筑施工特种作业人员操作资格考核证书变更		                                tzzybg
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=j';", true);  
                        break;
                    case "tzzykh"://建筑施工特种作业人员操作资格考核证书考核		                                tzzykh
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='EXamManage/ExamSignList.aspx';", true);
                        break;
                    case "tzzyzx"://建筑施工特种作业人员操作资格考核证书注销		                                tzzyzx
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=z';", true);
                        break;
                    case "ejyxzc": //二级注册建造师延续注册		                                                    ejyxzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyList.aspx?o=y';", true);                        
                        break;
                    case "ejcxzc":  //二级注册建造师重新注册		                                                    ejcxzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyList.aspx?o=r';", true);
                        break;
                    case "ejbgzc":  //二级注册建造师变更注册		                                                    ejbgzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyChangePerson.aspx';", true);
                        break;
                    case "ejcszc":  //二级建造师初始注册		                                                        ejcszc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyFirst.aspx';", true);
                        break;
                    case "ejzxzc": //二级注册建造师注销注册		                                                    ejzxzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyList.aspx?o=z';", true);
                        break;
                    case "ejadd":  //二级注册建造师增项注册		                                                    ejadd
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyList.aspx?o=a';", true);
                        break;
                    case "ezyxzc": //二级造价工程师延续注册		                                                    ezyxzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='zjs/zjsApplyList.aspx?o=y';", true);
                        break;
                    case "ezcszc": //二级造价工程师初始注册		                                                    ezcszc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='zjs/zjsApplyFirst.aspx';", true);
                        break;
                    case "ezbgzc"://二级造价工程师变更注册		                                                    ezbgzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='zjs/zjsApplyList.aspx?o=u';", true);
                        break;
                    case "ezzxzc": //二级造价工程师注销注册		                                                    ezzxzc
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='zjs/zjsApplyList.aspx?o=z';", true);
                        break;
                    case "zyjnbg"://职业（工种）技能鉴定证书变更		                                            zyjnbg
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=j';", true);  
                        break;
                    case "zyjnzx"://职业（工种）技能鉴定证书注销		                                            zyjnzx
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='CertifManage/CertifChange.aspx?t=z';", true);
                        break;
                    case "ejChangeUnit":  //二级注册建造师企业信息变更		                                                    ejChangeUnit
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='Unit/ApplyChangeEnterprise.aspx';", true);
                        break;
                    case "ezChangeUnit":  //二级注册造价工程师企业信息变更		                                                    ezChangeUnit
                        ClientScript.RegisterStartupScript(GetType(), "goto", "var cur='zjs/zjsApplyChangeUnitName.aspx';", true);
                        break;
                        
                    default:
                        break;
                }
            }
        }

        protected void LinkButtonLogout_Click(object sender, EventArgs e)
        {
            int myPersonType=  PersonType;
            Session["userInfo"] = null;
            FormsAuthentication.SignOut();
            ////关闭浏览器
            //ClientScript.RegisterClientScriptBlock(GetType(), "clise", "window.close();", true);

            if(myPersonType==2)
            {
                Response.Redirect("https://zjw.beijing.gov.cn/bjjs/zwfw/qtywxt/index.shtml", false);
            }
            else
            {
                Response.Redirect("login.aspx", false);
            }
            
        }
    }
}