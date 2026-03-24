using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;

namespace ZYRYJG.County
{
    //代办业务
    public partial class AgencyUnit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                QueryParamOB q = new QueryParamOB();
                if (IfExistRoleID("2") == true)//企业
                {
                    q.Add(string.Format("((ENT_OrganizationsCode like '{0}%' and newUnitCheckTime is null )or (OldEnt_QYZJJGDM like '{0}%' and OldUnitCheckTime is null))", ZZJGDM));
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.待确认));
                }
                else
                {
                    q.Add("1>2");
                }
            
                //分别计算出每个事项由多少个人
                DataTable dt = DataAccess.ApplyDAL.GetApplyGroupByApplyType(q.ToWhereString());
                //变更注册的企业变更
                DataTable dt1 = DataAccess.ApplyDAL.GetApplyGroupByApplyTypeQybg(q.ToWhereString());
                //变更注册的执业企业变更   南静添加   2019-11-02
                //DataTable dt2 = DataAccess.ApplyDAL.GetApplyGroupByApplyTypeZYQYbg(q.ToWhereString(), ZZJGDM);

                LabelChangeQY.Text = dt1.Rows.Count.ToString();
                //int num= dt2.Rows.Count;
                //LabelChangeZY.Text = num.ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["ApplyType"].ToString())
                    {
                        case "初始注册":
                            LabelFirst.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "重新注册":
                            LabelRenew.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "延期注册":
                            LabelContinue.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "增项注册":
                            LabelAddItem.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "遗失补办":
                            LabelReplace.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "个人信息变更":
                            LabelChangeGR.Text = dt.Rows[i]["Num"].ToString();
                            break;
                        case "执业企业变更":
                            LabelChangeZY.Text = dt.Rows[i]["Num"].ToString();
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