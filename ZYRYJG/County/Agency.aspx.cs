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
    public partial class Agency :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                QueryParamOB q = new QueryParamOB();
                if (IfExistRoleID("2") == true)//企业
                {
                    q.Add(string.Format("(((ENT_OrganizationsCode like '{0}%' or ENT_OrganizationsCode like '________{0}_') and newUnitCheckTime is null )or (OldEnt_QYZJJGDM like '{0}%' and OldUnitCheckTime is null))", ZZJGDM));
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.待确认));

                    XXBG.Visible = false;
                }
                if (IfExistRoleID("3") == true)//区县业务员
                {
                    q.Add(string.Format("ent_city like '{0}%'", Region));
                    q.Add(string.Format("ApplyStatus='{0}'  and ApplyType <>'增项注册'", EnumManager.ApplyStatus.已申报));
                }
                if (IfExistRoleID("7") == true)//区县领导
                {
                    q.Add(string.Format("ent_city like '{0}%'", Region));
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已受理));

                }
                //if (IfExistRoleID("5") == true)//大厅
                //{
                //    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已上报));
                //}
                if (IfExistRoleID("4") == true)//注册中心业务员
                {
                    XXBG.Visible = false;
                    QYBG.Visible = false;
                    q.Add(string.Format("(ApplyStatus='{0}' or (ApplyType='增项注册' and ApplyStatus='{1}'))", EnumManager.ApplyStatus.已上报, EnumManager.ApplyStatus.已申报));
                }
                if (IfExistRoleID("6") == true)//注册中心领导
                {
                    XXBG.Visible = false;
                    QYBG.Visible = false;
                    q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已审查));
                }

                if (IfExistRoleID("1") == true)//系统管理员
                {
                    q.Add("[NoticeDate] is null");//未办结
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