using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

namespace ZYRYJG.StAnManage
{
    /// <summary>
    /// 证书数量整体情况
    /// </summary>
    public partial class CertificateBaseInfo : BasePage
    {
        public int[] HistroyAarry=new int[5];
        public int[] ValidSysCreateAarry = new int[5];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dtTable = CertificateDAL.AnalysisCertificateBaseCount(DateTime.Now);
                    CreateCertificateBaseInfo(dtTable);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "统计分析证书数量基本情况失败！", ex);
                }
            }
        }

        /// <summary>
        /// 生成证书数量整体情况汇总以及Highcharts 图标
        /// </summary>
        /// <param name="dtTable"></param>
        private void CreateCertificateBaseInfo(DataTable dtTable)
        {
            LabelToday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (DataRow dr in dtTable.Rows)
            {
                Control find = divBaseInfo.FindControl(string.Format("Label{0}", dr["DataName"]));
                if (find != null)
                {
                    var label = find as Label;
                    if (label != null) label.Text = dr["DataCount"].ToString();
                }

                // '系统中证书总数','有效证书','证书过期','证书注销','证书离京变更'
                switch (dr["DataName"].ToString())
                {
                    case "HistoryCount":
                        HistroyAarry[0] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    case "SysCreateCount":
                        ValidSysCreateAarry[0] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    //有效证书历史数据
                    case "ValidHistoryCount":
                        HistroyAarry[1] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    case "ValidSysCreateCount":
                        ValidSysCreateAarry[1] = Convert.ToInt32(dr["DataCount"]);
                        break;

                    //证书过期历史数据
                    case "ExpireHistoryCount":
                        HistroyAarry[2] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    case "ExpireSysCreateCount":
                        ValidSysCreateAarry[2] = Convert.ToInt32(dr["DataCount"]);
                        break;

                    //证书注销历史数据
                    case "ZuXiaoHistoryCount":
                        HistroyAarry[3] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    case "ZuXiaoSysCreateCount":
                        ValidSysCreateAarry[3] = Convert.ToInt32(dr["DataCount"]);
                        break;

                    //证书离京历史数据
                    case "LiJingHistoryCount":
                        HistroyAarry[4] = Convert.ToInt32(dr["DataCount"]);
                        break;
                    case "LiJingSysCreateCount":
                        ValidSysCreateAarry[4] = Convert.ToInt32(dr["DataCount"]);
                        break;
                }
            }
        }
    }
}