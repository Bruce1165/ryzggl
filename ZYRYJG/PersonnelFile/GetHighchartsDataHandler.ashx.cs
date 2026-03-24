using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using DataAccess;

namespace ZYRYJG.PersonnelFile
{
    /// <summary>
    /// GetHighchartsDataHandler 的摘要说明
    /// </summary>
    public class GetHighchartsDataHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var key = request["Key"];
            var unitCode = request["UnitCode"];
            var highcharts = new List<string>();
            switch (key)
            {
                case "CompanyWorkCertInfoHighcharts":
                    //本企业本年度即将要过期证书(可以续期的)的统计
                    DataTable dt = CertificateDAL.AnalysisEnterprisesToExpireCertificateDataByUnitCode(unitCode);
                    //本企业员工证书统计
                    var dtAll = CertificateDAL.AnalysisEnterprisesCertificateDataByUnitCode(unitCode);
                    //POSTTYPEID,POSTTYPENAME ,count(*)
                    if (dt != null && dtAll != null)
                    {
                        highcharts.Add(CreateCompanyWorkToToExpireCertInfoHighchartsHighchartsData(dt));
                        highcharts.Add(CreateCompanyWorkCertInfoHighchartsHighchartsData(dtAll));

                    }
                    break;
            }
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(highcharts));
        }

        /// <summary>
        /// 创建本企业本年度即将要过期的企业人员证书统计信息
        /// </summary>
        /// <remarks>只有三类人、特种作业、造价员 这三种证书可以续期 posttype =1，2，3 
        ///posttype =4，5 职业技能、专业人员 不能续期，无有效期
        /// </remarks>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string CreateCompanyWorkToToExpireCertInfoHighchartsHighchartsData(DataTable dt)
        {
            var xvalueList = new List<string> {"企业人员证书总数", "安全生产考核三类人员", "建筑施工特种作业", "造价员"};
            var yvalueList = new List<int> {0, 0, 0, 0};
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (Convert.ToInt32(dt.Rows[i]["POSTTYPEID"].ToString()))
                {
                    case 1:
                        yvalueList[1] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 2:
                        yvalueList[2] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 3:
                        yvalueList[3] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                }
            }
            yvalueList[0] = yvalueList[1] + yvalueList[2] + yvalueList[3];
            var highchartsData = new HighchartsData
            {
                XValueList = xvalueList,
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, yvalueList}
                },
                Title = string.Format("{0}年企业人员即将要过期的证书数量统计", DateTime.Now.Year),
                SeriesNameList = new List<string>
                {
                    "岗位类别"
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(highchartsData);
        }

        /// <summary>
        /// 创建本企业人员证书统计信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string CreateCompanyWorkCertInfoHighchartsHighchartsData(DataTable dt)
        {
            var xvalueList = new List<string> { "企业人员证书总数", "安全生产考核三类人员", "建筑施工特种作业", "造价员", "建设职业技能岗位", "关键岗位专业技术管理人员" };
            var yvalueList = new List<int> { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (Convert.ToInt32(dt.Rows[i]["POSTTYPEID"].ToString()))
                {
                    case 1:
                        yvalueList[1] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 2:
                        yvalueList[2] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 3:
                        yvalueList[3] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 4:
                        yvalueList[4] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                    case 5:
                        yvalueList[5] = Convert.ToInt32(dt.Rows[i]["COUNT"].ToString());
                        break;
                }
            }
            yvalueList[0] = yvalueList[1] + yvalueList[2] + yvalueList[3] + yvalueList[4] + yvalueList[5];
            var highchartsData = new HighchartsData
            {
                XValueList = xvalueList,
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, yvalueList}
                },
                Title = "企业人员证书总数统计",
                SeriesNameList = new List<string>
                {
                    "企业人员证书总数"
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(highchartsData);
        }


        /// <summary>
        /// 图例类
        /// </summary>
        public class HighchartsData
        {
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { set; get; }

            public List<string> XValueList { set; get; }

            /// <summary>
            /// 服务名称
            /// </summary>
            public List<string> SeriesNameList { set; get; }

            /// <summary>
            /// 数据
            /// </summary>
            public Dictionary<int, List<int>> SeriesDataDictionary { get; set; }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}