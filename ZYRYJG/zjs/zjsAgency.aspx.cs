using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;

namespace ZYRYJG.zjs
{
    //代办业务
    public partial class zjsAgency : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                QueryParamOB q = new QueryParamOB();
                if (IfExistRoleID("2") == true)//企业
                {
                    q.Add(string.Format("((ENT_OrganizationsCode like '{0}%'  and newUnitCheckTime is null ) or (OldEnt_QYZJJGDM like '{0}%' and OldUnitCheckTime is null))", SHTJXYDM));
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.待确认));
                }
                else if (IfExistRoleID("20") == true)//市级受理
                {
                    q.Add(string.Format("ApplyStatus='{0}' ", EnumManager.ZJSApplyStatus.已申报));
                }
                else if (IfExistRoleID("21") == true)//市级审核
                {
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已受理));

                }
                //else if (IfExistRoleID("22") == true)//市级复核
                //{

                //    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已审核));
                //}
                else if (IfExistRoleID("23") == true)//市级决定
                {
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已审核));
                }
                else if (IfExistRoleID("1") == true)//系统管理员
                {
                    q.Add("[NoticeDate] is null");//未办结
                }
                else
                {
                    q.Add("1=2");
                }

                //分别计算出每个事项由多少个人
                DataTable dt = DataAccess.zjs_ApplyDAL.GetApplyGroupByApplyType(q.ToWhereString());

                ////变更注册的企业变更
                //DataTable dt1 = DataAccess.ApplyDAL.GetApplyGroupByApplyTypeQybg(q.ToWhereString());
     
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["ApplyType"].ToString())
                    {
                        case "初始注册":
                            LabelFirst.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "延续注册":
                            LabelContinue.Text = dt.Rows[i]["Num"].ToString();
                            break;                      
                        case "执业企业变更":
                            LabelChangeZY.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "个人信息变更":
                            LabelChangeGR.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "企业信息变更":
                            LabelChangeQY.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "注销":
                            LabelCancel.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        default:
                            break;

                    }
                }
            }
        }
    }
}