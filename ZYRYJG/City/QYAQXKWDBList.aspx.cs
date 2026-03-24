using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using System.Text;

namespace ZYRYJG
{
    //企业监控列表
    public partial class QYAQXKWDBList : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "City/EnterpriseMonitoring.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);


            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            try
            {
                ObjectDataSource1.SelectParameters.Clear();
                var q = new QueryParamOB();
                string qx = Region;
                switch (qx)
                {
                    case "北京市住房和城乡建设委员会":
                        qx = "全市";
                        break;
                    case "西城区":
                        qx = "西城区宣武区";
                        break;
                    case "东城区":
                        qx = "东城区崇文区";
                        break;
                    case "亦庄":
                        qx = "开发区";
                        break;
                }

                if (Region != "全市")//区县
                {
                    q.Add(string.Format("patindex('%{0}%',RegionName) >0", qx));
                }
              
                if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
                {
                    q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
                }

              
                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridQY.CurrentPageIndex = 0;
                RadGridQY.DataSourceID = ObjectDataSource1.ID;
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取人员流失率数据失败。", ex);
            }
        }
//        select isnull(d.[总量],0) CurCount
//,isnull(c.LostCount,0) LostCount
//,isnull(r.GetCount,0) GetCount
//,case when isnull(r.GetCount,0)=0 then 0 else  isnull(c.LostCount,0) * 100 /isnull(r.GetCount,0) end as LSV
//,j.*
//FROM [dbo].[jcsjk_QY_JBXX] j
//left join 
//(
//select ENT_Name_from,count(*) LostCount from jcsjk_jzs_bgqy 
//where len(ENT_Name_from) >0
//group by ENT_Name_from
//) c on j.[QYMC] = c.ENT_Name_from
//left join 
//(
//select ENT_Name_to,count(*) GetCount from jcsjk_jzs_bgqy 
//where len(ENT_Name_to) >0
//group by ENT_Name_to
//) r on j.[QYMC] = r.ENT_Name_to
//left join [dbo].[jcsjk_tj_qy_jzs] d on j.[QYMC] = d.[QYMC]
//order by case when isnull(r.GetCount,0)=0 then 0 else  isnull(c.LostCount,0) * 100 /isnull(r.GetCount,0) end  desc

    }
}