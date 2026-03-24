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
    public partial class QYZZWDBList : BasePage
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
                        qx = "亦庄开发区经济技术开发区";
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
                UIHelp.WriteErrorLog(Page, "获取资质不达标企业数据失败。", ex);
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
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
                    qx = "亦庄开发区经济技术开发区";
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


            try
            {
                //EXCEL表头明
                string head = @"序号\机构代码\企业名称\所在区县\注册资本金\法定代表人\联系电话\注册地址";
                //数据表的列明
                string column = @"row_number() over(order by dbo.jcsjk_tj_qy_zzwdb.QYMC)\ZZJGDM\QYMC\RegionName\ZCZJ\FDDBR\LXDH\ZCDZ";
                //过滤条件

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "dbo.jcsjk_tj_qy_zzwdb"
                    ,  q.ToWhereString(), "QYMC", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-right:20px;"">（{2}）</span>"
                    ,UIHelp.ShowFile(filePath)
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }
        }

    }
}