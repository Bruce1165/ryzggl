using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG
{
    public partial class rygate : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //无框架路由
                switch (Request["action"])
                {
                    case "slrkh"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核
                        Response.Redirect(string.Format("~/EXamManage/ExamSignList.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "slrxq"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书续期
                        Response.Redirect(string.Format("~/RenewCertifates/CertifApply.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "slrzx"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书注销		slrzx
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=z&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "slrbg"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书变更		slrbg
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=j&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "slrjj"://施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书进京		slrjj
                        Response.Redirect(string.Format("~/CertifEnter/CertifEnterApplyList.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "slrzf"://施工单位主要负责人安全生产考核证书增发		                                    slrzf
                        Response.Redirect(string.Format("~/CertifManage/CertifMoreApplyList?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "tzzyxq"://建筑施工特种作业人员操作资格考核证书续期		                                tzzyxq
                        Response.Redirect(string.Format("~/RenewCertifates/CertifApply.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "tzzybg"://建筑施工特种作业人员操作资格考核证书变更		                                tzzybg
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=j&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "tzzykh"://建筑施工特种作业人员操作资格考核证书考核		                                tzzykh
                        Response.Redirect(string.Format("~/EXamManage/ExamSignList.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "tzzyzx"://建筑施工特种作业人员操作资格考核证书注销		                                tzzyzx
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=z&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejyxzc": //二级注册建造师延续注册		                                                    ejyxzc
                        Response.Redirect(string.Format("~/Unit/ApplyList.aspx?o=y&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejcxzc":  //二级注册建造师重新注册		                                                    ejcxzc
                        Response.Redirect(string.Format("~/Unit/ApplyList.aspx?o=r&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejbgzc":  //二级注册建造师变更注册		                                                    ejbgzc
                        Response.Redirect(string.Format("~/Unit/ApplyChangePerson.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejcszc":  //二级建造师初始注册		                                                        ejcszc
                        Response.Redirect(string.Format("~/Unit/ApplyFirst.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejzxzc": //二级注册建造师注销注册		                                                    ejzxzc
                        Response.Redirect(string.Format("~/Unit/ApplyList.aspx?o=z&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ejadd ":  //二级注册建造师增项注册		                                                    ejadd
                        Response.Redirect(string.Format("~/Unit/ApplyList.aspx?o=a&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ezyxzc": //二级造价工程师延续注册		                                                    ezyxzc
                        Response.Redirect(string.Format("~/zjs/zjsApplyList.aspx?o=y&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ezcszc": //二级造价工程师初始注册		                                                    ezcszc
                        Response.Redirect(string.Format("~/Ezjs/zjsApplyFirst.aspx?invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ezbgzc"://二级造价工程师变更注册		                                                    ezbgzc
                        Response.Redirect(string.Format("~/zjs/zjsApplyList.aspx?o=u&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "ezzxzc": //二级造价工程师注销注册		                                                    ezzxzc
                        Response.Redirect(string.Format("~/zjs/zjsApplyList.aspx?o=z&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "zyjnbg"://职业（工种）技能鉴定证书变更		                                            zyjnbg
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=j&invoke={0}", DateTime.Now.Ticks), true);
                        break;
                    case "zyjnzx"://职业（工种）技能鉴定证书注销		                                            zyjnzx
                        Response.Redirect(string.Format("~/CertifManage/CertifChange.aspx?t=z&invoke={0}", DateTime.Now.Ticks), true);
                        break;

                    default:
                        Response.Write("没有找到你要的资源！");
                        break;
                }
            }
        }
    }
}