using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Text;

namespace ZYRYJG.County
{
    public partial class RegisterStatistical :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                BtnQuery_Click(sender, e);
            }
                
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //注册人员专业统计
                //类型
                StringBuilder lx = new StringBuilder();
                //数量
                StringBuilder sl = new StringBuilder();
                //所属区县
                StringBuilder city = new StringBuilder();//专业分类查询条件
                StringBuilder sqcity = new StringBuilder();//办理量查询条件

                city.Append(string.Format(" AND a.PSN_Level='{0}'", RadComboBoxIfContinue1.SelectedValue));
                city.Append(" AND a.[PSN_RegisteType]<>'07'");//排除注销
                city.Append(" and len(b.pro_profession) >0");                

                if (RadDatePickerPRO_ValidityBegin.SelectedDate.HasValue == true)
                {
                    city.Append(string.Format(" AND b.PRO_ValidityBegin >= '{0}'", RadDatePickerPRO_ValidityBegin.SelectedDate.Value));
                    sqcity.Append(string.Format(" AND NoticeDate >= '{0}'", RadDatePickerPRO_ValidityBegin.SelectedDate.Value));
                }
                if (RadDatePickerPRO_ValidityEnd.SelectedDate.HasValue == true)
                {
                    city.Append(string.Format(" AND b.PRO_ValidityEnd <= '{0}'", RadDatePickerPRO_ValidityEnd.SelectedDate.Value));
                    sqcity.Append(string.Format(" AND NoticeDate <= '{0} 23:59:59'", RadDatePickerPRO_ValidityEnd.SelectedDate.Value));
                }              
                
                //是否为区县，是就给SQL拼接上去，不是就显示注册大厅的


                if (IfExistRoleID("3")== true || IfExistRoleID("7") == true)//区县
                {
                    city.Append(string.Format(" and a.ent_city like '{0}%'", Region));
                    sqcity.Append(string.Format(" and ent_city like '{0}%'", Region));
                }

                sqcity.Append(" and NoticeDate is not null");//已公告

                DataTable dt = COC_TOW_Register_ProfessionDAL.GetZhuanYe(city.ToString());
                //构造饼图数据源
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lx.Append("\"");
                    lx.Append(dt.Rows[i][0]);
                    lx.Append("\"");
                    lx.Append(",");
                    sl.Append("{");
                    sl.Append("value:" + dt.Rows[i][1] + ",name:'" + dt.Rows[i][0] + "'");
                    sl.Append("}");
                    sl.Append(",");
                }
                //移除两个字符串最后一次加上的逗号
                if (lx.Length > 0)
                    lx.Remove(lx.Length - 1, 1);
                if (sl.Length > 0)
                    sl.Remove(sl.Length - 1, 1);
                ViewState["LX"] = lx.ToString();//专业类型
                ViewState["SL"] = sl.ToString();//数量

                //事项类型总数统计
                //事项类别
                StringBuilder sxlb = new StringBuilder();
                //事项数量
                StringBuilder sxsl = new StringBuilder();
                //变更类型数量
                StringBuilder bgsl = new StringBuilder();

                DataTable dt1 = DataAccess.ApplyDAL.GetApplyGroupByNot(sqcity.ToString());
                //构造饼图数据源
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    sxlb.Append("\"");
                    sxlb.Append(dt1.Rows[i][0]);
                    sxlb.Append("\"");
                    sxlb.Append(",");
                    if (dt1.Rows[i][0].ToString() == "个人信息变更" || dt1.Rows[i][0].ToString() == "执业企业变更" || dt1.Rows[i][0].ToString() == "企业信息变更")
                    {
                        bgsl.Append("{");
                        bgsl.Append("value:" + dt1.Rows[i][1] + ",name:'" + dt1.Rows[i][0] + "'");
                        bgsl.Append("}");
                        bgsl.Append(",");
                        continue;
                    }
                    sxsl.Append("{");
                    sxsl.Append("value:" + dt1.Rows[i][1] + ",name:'" + dt1.Rows[i][0] + "'");
                    sxsl.Append("}");
                    sxsl.Append(",");
                }

                //移除三个字符串最后一次加上的逗号
                if (sxlb.Length > 0)
                    sxlb.Remove(sxlb.Length - 1, 1);
                if (sxsl.Length > 0)
                    sxsl.Remove(sxsl.Length - 1, 1);
                if (bgsl.Length > 0)
                    bgsl.Remove(bgsl.Length - 1, 1);
                ViewState["SXLX"] = sxlb.ToString();
                ViewState["SXSL"] = sxsl.ToString();
                ViewState["BGSL"] = bgsl.ToString();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "注册人员统计数据出错", ex);
            }
        }

     
    }
}