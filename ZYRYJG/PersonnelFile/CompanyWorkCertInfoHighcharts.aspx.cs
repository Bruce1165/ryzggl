using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.PersonnelFile
{
    public partial class CompanyWorkCertInfoHighcharts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          

                string sql = @"SELECT 
                                PostTypeName,[VALIDENDDATE],count(*) sl
                                  FROM [dbo].[CERTIFICATE]
                                   where UNITCODE='{0} '
                                   and [VALIDENDDATE] between getdate() and (cast(year(getdate()) as varchar(4))+'-12-31')
                                   and (PostTypeID =1 or PostTypeID =2)
                                  and [STATUS] in('首次','进京变更','京内变更','续期','补办')
                                 group  by PostTypeName,[VALIDENDDATE]
                                 order by PostTypeName,[VALIDENDDATE];";

               DataTable  dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM));
               
                RadGridCertificate.DataSource = dt;
                RadGridCertificate.DataBind();

                //续期开放时间段提醒
                string ts = "{0}：{1}月1日 - {2}月{3}日";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("提示：未到续期申请开放时间的将无法申请续期，目前开放时间如下，<br /><br />特种作业人员延期复核的继续教育培训由用人单位自行组织，我委不组织培训且不指定任何培训机构。<br />");
                for (int i = 0; i < reNewDateSpan.Rows.Count; i++)
                {
                    string[] _params = reNewDateSpan.Rows[i]["TypeValue"].ToString().Split(',');
                    if (sb.Length > 0) sb.Append("<br />");
                    sb.Append(string.Format(ts, reNewDateSpan.Rows[i]["TypeName"].ToString(), _params[2], formatContinueEndMonth(Convert.ToInt32(_params[3])), formatContinueEndDay(Convert.ToInt32(_params[3]))));
                }
                sb.Append("<br />造价员：根据有关规定，停止造价员考核、变更和续期工作。");
                divBaseInfo.InnerHtml= sb.ToString();
            }
        }

        /// <summary>
        /// 格式化续期截止月份，超过12月显示：明年XX月
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        protected string formatContinueEndMonth(int month)
        {
            if (month > 12)
            {
                return string.Format("明年{0}", month - 12);
            }
            else
            {
                return month.ToString();
            }
        }

        /// <summary>
        /// 格式化续期截止月份最后一天
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns>最后一天数字</returns>
        protected int formatContinueEndDay(int month)
        {
            return Convert.ToDateTime(string.Format("{0}-01-01", DateTime.Now.Year)).AddMonths(month).AddDays(-1).Day;
        }


        /// <summary>
        /// 续期时间段设置
        /// </summary>
        public DataTable reNewDateSpan
        {
            get
            {
                if (Cache["reNewDateSpan"] == null)
                {
                    DataTable dt = TypesDAL.GetListByTypeID("106");//续期时间段配置 
                    Cache["reNewDateSpan"] = dt;
                    return dt;
                }
                else
                {
                    return Cache["reNewDateSpan"] as DataTable;
                }

            }
        }
    }
}