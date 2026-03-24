using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
//using Word;

namespace ZYRYJG.StAnManage
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
            var highcharts = new List<string>();
            DataTable dt;
            switch (key)
            {
                case "CertifApplyResultQuery":
                    dt = HttpContext.Current.Session["CertifApplyResultQuery_dtBase"] as DataTable;
                    if (dt != null)
                    {
                        highcharts = CreateCertifApplyResultQueryHighchartsData(dt);
                        //HttpContext.Current.Session.Remove("CertifApplyResultQuery_dtBase");
                    }
                    break;

                case "CertifUpdateQuery":
                    dt = HttpContext.Current.Session["CertifUpdateQuery_dtBase"] as DataTable;
                    if (dt != null)
                    {
                        highcharts = CreateCommonQueryHighchartsData(dt, "证书数据更新统计");
                       // HttpContext.Current.Session.Remove("CertifUpdateQuery_dtBase");
                    }
                    break;

                case "CertifManageQuery":
                    dt = HttpContext.Current.Session["CertifManageQuery_dtBase"] as DataTable;
                    if (dt != null)
                    {
                        highcharts = CreateCommonQueryHighchartsData(dt, "证书管理统计");
                        //HttpContext.Current.Session.Remove("CertifManageQuery_dtBase");
                    }
                    break;

                case "ExamManageQuery":
                    dt = HttpContext.Current.Session["ExamManageQuery_dtBase"] as DataTable;
                    if (dt != null)
                    {
                        highcharts = CreateExamManageQueryHighchartsData(dt, "考务管理统计");
                       // HttpContext.Current.Session.Remove("ExamManageQuery_dtBase");
                    }
                    break;
            }
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(highcharts));
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

        /// <summary>
        /// 为业务申请与办理统计创建图例数据源
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <returns>json串的list集合</returns>
        private List<string> CreateCertifApplyResultQueryHighchartsData(DataTable dt)
        {
            List<string> list = new List<string>();
            List<int> examinationTotalList = new List<int>();
            List<int> examinationPassTotalList = new List<int>();

            List<int> renewalTotalList = new List<int>();
            List<int> renewalPassTotalList = new List<int>();
            List<int> renewalAPassTotalList = new List<int>();

            List<int> changesBJTotalList = new List<int>();
            List<int> changesBJPassTotalList = new List<int>();
            List<int> changesBJAPassTotalList = new List<int>();

            List<int> changesLBJTotalList = new List<int>();
            List<int> changesLBJPassTotalList = new List<int>();
            List<int> changesLBJAPassTotalList = new List<int>();

            List<int> changesIBJTotalList = new List<int>();
            List<int> changesIBJPassTotalList = new List<int>();
            List<int> changesIBJAPassTotalList = new List<int>();

            List<int> cancellationTotalList = new List<int>();
            List<int> cancellationPassTotalList = new List<int>();
            List<int> cancellationAPassTotalList = new List<int>();

            List<int> reissueTotalList = new List<int>();
            List<int> reissuePassTotalList = new List<int>();
            List<int> reissueAPassTotalList = new List<int>();

            foreach (DataRow row in dt.Rows)
            {
                //过滤掉总计
                if (!String.IsNullOrEmpty(row["POSTTYPEID"].ToString()))
                {
                    //考试
                    examinationTotalList.Add(Convert.ToInt32(row["考试申请个数"].ToString()));
                    examinationPassTotalList.Add(Convert.ToInt32(row["考试通过个数"].ToString()));

                    //续期
                    renewalTotalList.Add(Convert.ToInt32(row["续期申请个数"].ToString()));
                    renewalPassTotalList.Add(Convert.ToInt32(row["续期通过个数"].ToString()));
                    renewalAPassTotalList.Add(Convert.ToInt32(row["续期申请并通过个数"].ToString()));

                    //京内变更
                    changesBJTotalList.Add(Convert.ToInt32(row["京内变更申请个数"].ToString()));
                    changesBJPassTotalList.Add(Convert.ToInt32(row["京内变更通过个数"].ToString()));
                    changesBJAPassTotalList.Add(Convert.ToInt32(row["京内变更申请并通过个数"].ToString()));

                    //离京变更
                    changesLBJTotalList.Add(Convert.ToInt32(row["离京变更申请个数"].ToString()));
                    changesLBJPassTotalList.Add(Convert.ToInt32(row["离京变更通过个数"].ToString()));
                    changesLBJAPassTotalList.Add(Convert.ToInt32(row["离京变更申请并通过个数"].ToString()));

                    //进京变更
                    changesIBJTotalList.Add(Convert.ToInt32(row["进京变更申请个数"].ToString()));
                    changesIBJPassTotalList.Add(Convert.ToInt32(row["进京变更通过个数"].ToString()));
                    changesIBJAPassTotalList.Add(Convert.ToInt32(row["进京变更申请并通过个数"].ToString()));

                    //注销
                    cancellationTotalList.Add(Convert.ToInt32(row["注销申请个数"].ToString()));
                    cancellationPassTotalList.Add(Convert.ToInt32(row["注销通过个数"].ToString()));
                    cancellationAPassTotalList.Add(Convert.ToInt32(row["注销申请并通过个数"].ToString()));

                    //补办
                    reissueTotalList.Add(Convert.ToInt32(row["补办申请个数"].ToString()));
                    reissuePassTotalList.Add(Convert.ToInt32(row["补办通过个数"].ToString()));
                    reissueAPassTotalList.Add(Convert.ToInt32(row["补办申请并通过个数"].ToString()));
                }
            }

            #region 考试

            var examinationHighchartsData = new HighchartsData
            {
                Title = "考试",
                SeriesNameList = new List<string> {"参加数量", "通过数量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, examinationTotalList},
                    {1, examinationPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(examinationHighchartsData));

            #endregion 考试

            #region 续期

            var renewalHighchartsData = new HighchartsData
            {
                Title = "续期",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, renewalTotalList},
                    {1, renewalPassTotalList},
                    {2, renewalAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(renewalHighchartsData));

            #endregion 续期

            #region 京内变更

            var changesBJHighchartsData = new HighchartsData
            {
                Title = "京内变更",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, changesBJTotalList},
                    {1, changesBJPassTotalList},
                    {2, changesBJAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(changesBJHighchartsData));

            #endregion 京内变更

            #region 离京变更

            var changesLBJHighchartsData = new HighchartsData
            {
                Title = "离京变更",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, changesLBJTotalList},
                    {1, changesLBJPassTotalList},
                    {2, changesLBJAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(changesLBJHighchartsData));

            #endregion 离京变更

            #region 进京变更

            var changesIBJHighchartsData = new HighchartsData
            {
                Title = "进京变更",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, changesIBJTotalList},
                    {1, changesIBJPassTotalList},
                    {2, changesIBJAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(changesIBJHighchartsData));

            #endregion 进京变更

            #region 注销

            var cancellationHighchartsData = new HighchartsData
            {
                Title = "注销",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, cancellationTotalList},
                    {1, cancellationPassTotalList},
                    {2, cancellationAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(cancellationHighchartsData));

            #endregion 注销

            #region 补办

            var reissueHighchartsData = new HighchartsData
            {
                Title = "补办",
                SeriesNameList = new List<string> {"申请数量", "通过数量", "申请并通过量"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, reissueTotalList},
                    {1, reissuePassTotalList},
                    {2, reissueAPassTotalList}
                }
            };

            list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(reissueHighchartsData));

            #endregion 补办

            return list;
        }

        /// <summary>
        /// 创建通用（"首次发证", "续期", "京内变更", "离京变更", "进京变更", "注销", "遗失污损补办"）图例数据源
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="title">图例标题</param>
        /// <returns>json串的list集合</returns>
        private List<string> CreateCommonQueryHighchartsData(DataTable dt, string title)
        {
            int sum0 = 0;
            int sum1 = 0;
            int sum2 = 0;
            int sum3 = 0;
            int sum4 = 0;

            int sum01 = 0;
            int sum11 = 0;
            int sum21 = 0;
            int sum31 = 0;
            int sum41 = 0;

            int sum02 = 0;
            int sum12 = 0;
            int sum22 = 0;
            int sum32 = 0;
            int sum42 = 0;

            int sum03 = 0;
            int sum13 = 0;
            int sum23 = 0;
            int sum33 = 0;
            int sum43 = 0;

            int sum04 = 0;
            int sum14 = 0;
            int sum24 = 0;
            int sum34 = 0;
            int sum44 = 0;

            int sum05 = 0;
            int sum15 = 0;
            int sum25 = 0;
            int sum35 = 0;
            int sum45 = 0;

            int sum06 = 0;
            int sum16 = 0;
            int sum26 = 0;
            int sum36 = 0;
            int sum46 = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (!dr["POSTTYPEID"].ToString().Equals(""))
                {
                    switch (Convert.ToInt32(dr["POSTTYPEID"]))
                    {
                        case 1:
                            sum0 += Convert.ToInt32(dr["首次"]);
                            sum01 += Convert.ToInt32(dr["续期"]);
                            sum02 += Convert.ToInt32(dr["京内变更"]);
                            sum03 += Convert.ToInt32(dr["离京变更"]);
                            sum04 += Convert.ToInt32(dr["进京变更"]);
                            sum05 += Convert.ToInt32(dr["注销"]);
                            sum06 += Convert.ToInt32(dr["补办"]);
                            break;

                        case 2:
                            sum1 += Convert.ToInt32(dr["首次"]);
                            sum11 += Convert.ToInt32(dr["续期"]);
                            sum12 += Convert.ToInt32(dr["京内变更"]);
                            sum13 += Convert.ToInt32(dr["离京变更"]);
                            sum14 += Convert.ToInt32(dr["进京变更"]);
                            sum15 += Convert.ToInt32(dr["注销"]);
                            sum16 += Convert.ToInt32(dr["补办"]);
                            break;

                        case 3:
                            sum2 += Convert.ToInt32(dr["首次"]);
                            sum21 += Convert.ToInt32(dr["续期"]);
                            sum22 += Convert.ToInt32(dr["京内变更"]);
                            sum23 += Convert.ToInt32(dr["离京变更"]);
                            sum24 += Convert.ToInt32(dr["进京变更"]);
                            sum25 += Convert.ToInt32(dr["注销"]);
                            sum26 += Convert.ToInt32(dr["补办"]);
                            break;

                        case 4:
                            sum3 += Convert.ToInt32(dr["首次"]);
                            sum31 += Convert.ToInt32(dr["续期"]);
                            sum32 += Convert.ToInt32(dr["京内变更"]);
                            sum33 += Convert.ToInt32(dr["离京变更"]);
                            sum34 += Convert.ToInt32(dr["进京变更"]);
                            sum35 += Convert.ToInt32(dr["注销"]);
                            sum36 += Convert.ToInt32(dr["补办"]);
                            break;

                        case 5:
                            sum4 += Convert.ToInt32(dr["首次"]);
                            sum41 += Convert.ToInt32(dr["续期"]);
                            sum42 += Convert.ToInt32(dr["京内变更"]);
                            sum43 += Convert.ToInt32(dr["离京变更"]);
                            sum44 += Convert.ToInt32(dr["进京变更"]);
                            sum45 += Convert.ToInt32(dr["注销"]);
                            sum46 += Convert.ToInt32(dr["补办"]);
                            break;
                    }
                }
            }
            var certifupdatequeryHighchartsData = new HighchartsData
            {
                Title = title,
                SeriesNameList = new List<string> {"首次发证", "续期", "京内变更", "离京变更", "进京变更", "注销", "遗失污损补办"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, new List<int> {sum0, sum1, sum2, sum3, sum4}},
                    {1, new List<int> {sum01, sum11, sum21, sum31, sum41}},
                    {2, new List<int> {sum02, sum12, sum22, sum32, sum42}},
                    {3, new List<int> {sum03, sum13, sum23, sum33, sum43}},
                    {4, new List<int> {sum04, sum14, sum24, sum34, sum44}},
                    {5, new List<int> {sum05, sum15, sum25, sum35, sum45}},
                    {6, new List<int> {sum06, sum16, sum26, sum36, sum46}}
                }
            };
            var list = new List<string>
            {
                Newtonsoft.Json.JsonConvert.SerializeObject(certifupdatequeryHighchartsData)
            };

            return list;
        }

        /// <summary>
        ///为考务管理统计创建图例数据源
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="title">图例标题</param>
        /// <returns>json串的list集合</returns>
        private List<string> CreateExamManageQueryHighchartsData(DataTable dt, string title)
        {
            int sum0 = 0;
            int sum1 = 0;
            int sum2 = 0;
            int sum3 = 0;
            int sum4 = 0;

            int sum01 = 0;
            int sum11 = 0;
            int sum21 = 0;
            int sum31 = 0;
            int sum41 = 0;

            int sum02 = 0;
            int sum12 = 0;
            int sum22 = 0;
            int sum32 = 0;
            int sum42 = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            int m = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (!dr["POSTTYPEID"].ToString().Equals(""))
                {
                    switch (Convert.ToInt32(dr["POSTTYPEID"]))
                    {
                        case 1:
                            if (!Convert.ToInt32(dr["考试人数"]).Equals(0))
                            {
                                sum0 += Convert.ToInt32(dr["考试人数"]);
                                sum01 += Convert.ToInt32(dr["合格人数"]);
                                sum02 += Convert.ToInt32(dr["合格率"] == DBNull.Value ? 0 : dr["合格率"]);
                                i += 1;
                            }
                            break;
                        case 2:
                            if (!Convert.ToInt32(dr["考试人数"]).Equals(0))
                            {
                                sum1 += Convert.ToInt32(dr["考试人数"]);
                                sum11 += Convert.ToInt32(dr["合格人数"]);
                                sum12 += Convert.ToInt32(dr["合格率"] == DBNull.Value ? 0 : dr["合格率"]);
                                j += 1;
                            }
                            break;
                        case 3:
                            if (!Convert.ToInt32(dr["考试人数"]).Equals(0))
                            {
                                sum2 += Convert.ToInt32(dr["考试人数"]);
                                sum21 += Convert.ToInt32(dr["合格人数"]);
                                sum22 += Convert.ToInt32(dr["合格率"]);
                                k += 1;
                            }
                            break;
                        case 4:
                            if (!Convert.ToInt32(dr["考试人数"]).Equals(0))
                            {
                                sum3 += Convert.ToInt32(dr["考试人数"]);
                                sum31 += Convert.ToInt32(dr["合格人数"]);
                                sum32 += Convert.ToInt32(dr["合格率"]);
                                l += 1;
                            }
                            break;
                        case 5:
                            if (!Convert.ToInt32(dr["考试人数"]).Equals(0))
                            {
                                sum4 += Convert.ToInt32(dr["考试人数"]);
                                sum41 += Convert.ToInt32(dr["合格人数"]);
                                sum42 += Convert.ToInt32(dr["合格率"]);
                                m += 1;
                            }
                            break;
                    }
                }
            }
            var certifupdatequeryHighchartsData = new HighchartsData
            {
                Title = title,
                SeriesNameList = new List<string> {"考试人数", "合格人数", "合格率"},
                SeriesDataDictionary = new Dictionary<int, List<int>>
                {
                    {0, new List<int> {sum0, sum1, sum2, sum3, sum4}},
                    {1, new List<int> {sum01, sum11, sum21, sum31, sum41}},
                    {
                        2,
                        new List<int>
                        {
                            sum02/(i == 0 ? 1 : i),
                            sum12/(j == 0 ? 1 : j),
                            sum22/(k == 0 ? 1 : k),
                            sum32/(l == 0 ? 1 : l),
                            sum42/(m == 0 ? 1 : m)
                        }
                    }
                }
            };
            var list = new List<string>
            {
                Newtonsoft.Json.JsonConvert.SerializeObject(certifupdatequeryHighchartsData)
            };

            return list;
        }
    }
}