using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Utility;
using System.Data;

namespace ZYRYJG.PersonnelFile
{
    public partial class Appraise : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ApplyProcess.aspx";
            }
        }

        #region 旧版公服事项目录（作废）

        /// <summary>
        /// 获取事项ID
        /// </summary>
        /// <param name="PostTypeName">证书类型</param>
        /// <param name="ApplyType">业务类型</param>
        /// <returns></returns>
        private string GetTaskID(string PostTypeName, string ApplyType)
        {
            switch (PostTypeName)
            {
                case "安全生产考核三类人员":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "24960267-9565-11e9-8300-507b9d3e4710";
                        case "续期":
                            return "24acf4ae-9565-11e9-8300-507b9d3e4710";
                        case "注销":
                            return "2495f6cb-9565-11e9-8300-507b9d3e4710";
                        case "京内变更":
                        case "离京变更":
                            return "24ace28c-9565-11e9-8300-507b9d3e4710";
                        case "进京变更":
                            return "c6da0f54-5342-4544-9518-4a9e4de8f64c";
                        case"增发":
                            return "dfe7b7b2-01cc-4dd9-9b8b-3b1f8d61930f";
                        default:
                            return "";
                    }
                case "建筑施工特种作业":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "24ace1a7-9565-11e9-8300-507b9d3e4710";
                        case "续期":
                            return "2495f971-9565-11e9-8300-507b9d3e4710";
                        case "注销":
                            return "24acf035-9565-11e9-8300-507b9d3e4710";
                        case "京内变更":
                        case "离京变更":
                            return "24acf3b2-9565-11e9-8300-507b9d3e4710";
                        default:
                            return "";
                    }
                case "建设职业技能岗位":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "缺";
                        case "京内变更":
                        case "离京变更":
                            return "32bcf506-7920-4647-81a5-ada4891c744f";
                        case "注销":
                            return "fba905eb-a44d-4ee9-a9cc-15aca9cf41a9";
                        default:
                            return "";
                    }

                case "二级注册建造师":
                    switch (ApplyType)
                    {
                        case "重新注册":
                            return "876c7025-efe9-4b1a-a487-bfc7e8c3fd92";
                        case "注销":
                            return "97f2a848-d5ec-4140-b7a7-4c8dda007d75";

                        case "延期注册":
                            return "5bc55b51-55cd-4554-81d7-aa787e8da7a9";
                        case "初始注册":
                            return "8c3caa80-2bc1-46d8-a13c-6e73a9e0890c";
                        case "增项注册":
                            return "ac9be8ec-0024-4588-a2cd-a4a5b9508867";
                        case "个人信息变更":
                            return "db595eb0-1153-4165-b2aa-ecc7d7bd2219";
                        case "企业信息变更":
                        case "执业企业变更":
                            return "be1b235a-2fb7-484f-a4d4-282885b404dc";
                        default:
                            return "";
                    }
                case "二级注册造价工程师":
                    return "39217c76-f7ae-4f2b-9267-078d416d4ce2";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取事项名称
        /// </summary>
        /// <param name="PostTypeName">证书类型</param>
        /// <param name="ApplyType">业务类型</param>
        /// <returns></returns>
        private string GetTaskName(string PostTypeName, string ApplyType)
        {
            switch (PostTypeName)
            {
                case "安全生产考核三类人员":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核";
                        case "续期":
                            return "施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书续期";
                        case "注销":
                            return "施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书注销";
                        case "京内变更":
                        case "离京变更":
                             return "施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书变更";
                        case "进京变更":
                             return "施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书进京";
                        case "增发":
                             return "施工单位主要负责人安全生产考核证书增发";    

                        default:
                            return "";
                    }
                case "建筑施工特种作业":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "建筑施工特种作业人员操作资格考核证书考核";
                        case "续期":
                            return "建筑施工特种作业人员操作资格考核证书续期";
                        case "注销":
                            return "建筑施工特种作业人员操作资格考核证书注销";
                        case "京内变更":
                        case "离京变更":
                            return "建筑施工特种作业人员操作资格考核证书变更";
                        default:
                            return "";
                    }
                case "建设职业技能岗位":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "职业（工种）技能鉴定证书考核";
                        case "京内变更":
                        case "离京变更":
                            return "职业（工种）技能鉴定证书变更";
                        case "注销":
                            return "职业（工种）技能鉴定证书注销";
                        default:
                            return "";
                    }

                case "二级注册建造师":
                    switch (ApplyType)
                    {
                        case "重新注册":
                            return "二级注册建造师重新注册";
                        case "注销":
                            return "二级注册建造师注销注册";

                        case "延期注册":
                            return "二级注册建造师延续注册";
                        case "初始注册":
                            return "二级建造师初始注册";
                        case "增项注册":
                            return "二级注册建造师增项注册";
                        case "个人信息变更":
                            return "二级建造师个人信息变更";
                        case "企业信息变更":
                        case "执业企业变更":
                            return "二级注册建造师变更注册";
                        default:
                            return "";
                    }
                case "二级注册造价工程师":
                    return "二级造价工程师执业资格认定";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取实施编码
        /// </summary>
        /// <param name="PostTypeName"></param>
        /// <returns></returns>
        private string GetTaskCode(string PostTypeName)
        {
            switch (PostTypeName)
            {
                case "安全生产考核三类人员":
                    return "11110000000021135M2000117008000";
                case "建筑施工特种作业":
                    return "11110000000021135M2000117009000";
                case "建设职业技能岗位":
                    return "11110000000021135M2000717003000";
                case "二级注册建造师":
                    return "11110000000021135M2000117055002";
                case "二级注册造价工程师":
                    return "11110000000021135M2000117056000";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取业务办理项编码
        /// </summary>
        /// <param name="PostTypeName">证书类型</param>
        /// <param name="ApplyType">业务类型</param>
        /// <returns></returns>
        private string taskHandleItem(string PostTypeName, string ApplyType)
        {
            switch (PostTypeName)
            {
                case "安全生产考核三类人员":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "11110000000021135M200011700800002";
                        case "续期":
                            return "11110000000021135M200011700800003";
                        case "注销":
                            return "11110000000021135M200011700800004";
                        case "京内变更":
                        case "离京变更":
                            return "11110000000021135M200011700800006";
                        case "进京变更":
                            return "11110000000021135M200011700800007";
                        case "增发":
                            return "11110000000021135M200011700800008";
                        default:
                            return "";
                    }
                case "建筑施工特种作业":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "11110000000021135M200011700900005";
                        case "续期":
                            return "11110000000021135M200011700900001";
                        case "注销":
                            return "11110000000021135M200011700900006";
                        case "京内变更":
                        case "离京变更":
                            return "11110000000021135M200011700900002";
                        default:
                            return "";
                    }
                case "建设职业技能岗位":
                    switch (ApplyType)
                    {
                        case "考试报名":
                            return "11110000000021135M200071700300001";
                        case "京内变更":
                        case "离京变更":
                            return "11110000000021135M200071700300002";
                        case "注销":
                            return "11110000000021135M200071700300003";
                        default:
                            return "";
                    }

                case "二级注册建造师":
                    switch (ApplyType)
                    {
                        case "重新注册":
                            return "11110000000021135M200011705500207";
                        case "注销":
                            return "11110000000021135M200011705500212";
                        case "延期注册":
                            return "11110000000021135M200011705500202";
                        case "初始注册":
                            return "11110000000021135M200011705500211";
                        case "增项注册":
                            return "11110000000021135M200011705500213";
                        case "个人信息变更":
                            return "11110000000021135M200011705500206";
                        case "企业信息变更":
                        case "执业企业变更":
                            return "11110000000021135M200011705500210";
                        default:
                            return "";
                    }
                case "二级注册造价工程师":
                    return "";
                default:
                    return "";
            }
        }

        #endregion 旧版公服事项目录（作废）

        /// <summary>
        /// 获取岗位工种 
        /// </summary>
        /// <param name="PostID">岗位ID</param>
        /// <returns>岗位工种</returns>
        private string GetPostName(int PostID)
        {
            switch (PostID)
            {
                case 6://土建安全员    
                    return "土建类专职安全生产管理人员";
                case 1123://机械安全员 
                    return "机械类专职安全生产管理人员";
                case 1125://综合安全员               
                    return "综合类专职安全生产管理人员";
                case 147://企业主要负责人                    
                    return "企业主要负责人";
                case 148://项目负责人                   
                    return "项目负责人";
            }
            return "";
        }

        ///// <summary>
        ///// 定位新版公服事项目录
        ///// </summary>
        ///// <param name="PostTypeName">岗位类别</param>
        ///// <param name="PostName">岗位工种（专业）</param>
        ///// <param name="ApplyType">业务事项</param>
        ///// <param name="REGION">适用地区</param>
        ///// <returns>公服事项信息行</returns>
        //protected DataRow GetGFSX(string PostTypeName, string PostName, string ApplyType, string REGION)
        //{
        //    string TASKNAME_MASTER = "";//事项大类，岗位类别【建造师执业资格认定，建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核，建筑施工特种作业人员职业资格认定，注册造价工程师注册】
        //    string TASKNAME_SUB = "";//事项子类，专业
        //    string TASKNAME = "";//事项名称

        //    switch (PostTypeName)
        //    {
        //        case "二级注册造价工程师":
        //            TASKNAME_MASTER = "注册造价工程师注册";
        //            TASKNAME_SUB = "二级注册造价工程师注册";
        //            switch (ApplyType)
        //            {
        //                case "变更注册":
        //                    TASKNAME = "二级注册造价工程师注册（变更注册）";
        //                    break;
        //                case "初始注册":
        //                    TASKNAME = "二级注册造价工程师注册（初始注册）";
        //                    break;
        //                case "延续注册":
        //                    TASKNAME = "二级注册造价工程师注册（延续注册）";
        //                    break;
        //                case "注销":
        //                    TASKNAME = "二级注册造价工程师注册（注销注册）";
        //                    break;
        //            }
        //            break;
        //        case "二级注册建造师":
        //            TASKNAME_MASTER = "建造师执业资格认定";
        //            switch (PostName.Split(',')[0])
        //            {
        //                case "公路":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（公路工程专业）";
        //                    break;
        //                case "机电":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（机电工程专业）";
        //                    break;
        //                case "建筑":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（建筑工程专业）";
        //                    break;
        //                case "矿业":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（矿业工程专业）";
        //                    break;
        //                case "市政":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（市政公用工程专业）";
        //                    break;
        //                case "水利":
        //                    TASKNAME_SUB = "二级建造师执业资格认定（水利水电工程专业）";
        //                    break;
        //                default:
        //                    break;
        //            }

        //            TASKNAME = string.Format("{0}（{1}）", TASKNAME_SUB, ApplyType.Replace("延期注册", "延续注册").Replace("注销", "注销注册"));                           

        //            break;
        //        case "安全生产考核三类人员":
        //            TASKNAME_MASTER = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核";
        //            switch (PostName)
        //            {
        //                case "企业主要负责人":
        //                    TASKNAME_SUB = "建筑施工企业主要负责人安全生产考核";
                            
        //                    switch (ApplyType)
        //                    {
        //                        case "考核":
        //                        case "进京变更":
        //                            TASKNAME = "建筑施工企业主要负责人安全生产考核（考核发证）";
        //                            break;
        //                        case "京内变更":
        //                             TASKNAME = "建筑施工企业主要负责人安全生产考核（省内变更受聘企业）";
        //                            break;
        //                        case "离京变更":
        //                            TASKNAME = "建筑施工企业主要负责人安全生产考核（跨省变更受聘企业）";
        //                            break;
        //                        case "续期":
        //                            TASKNAME = "建筑施工企业主要负责人安全生产考核（证书延期）";
        //                            break;
        //                        case "注销":
        //                            TASKNAME = "建筑施工企业主要负责人安全生产考核（证书注销）";
        //                            break;
        //                    }
        //                    break;
        //                case "项目负责人":
        //                    TASKNAME_SUB = "建筑施工企业项目负责人安全生产考核";
        //                    switch (ApplyType)
        //                    {
        //                        case "考核":
        //                        case "进京变更":
        //                             TASKNAME = "建筑施工企业项目负责人安全生产考核（考核发证）";
        //                            break;                                   
        //                        case "京内变更":
        //                             TASKNAME = "建筑施工企业项目负责人安全生产考核（省内变更受聘企业）";
        //                            break;
        //                        case "离京变更":
        //                             TASKNAME = "建筑施工企业项目负责人安全生产考核（跨省变更受聘企业）";
        //                            break;                                   
        //                        case "续期":
        //                            TASKNAME = "建筑施工企业项目负责人安全生产考核（证书延期）";
        //                            break;
        //                        case "注销":
        //                            TASKNAME = "建筑施工企业项目负责人安全生产考核（证书注销）";
        //                            break;
        //                    }
        //                    break;
        //                case "土建类专职安全生产管理人员":
        //                case "机械类专职安全生产管理人员":
        //                case "综合类专职安全生产管理人员":
        //                    TASKNAME_SUB = "建筑施工企业专职安全生产管理人员安全生产考核";
        //                    switch (ApplyType)
        //                    {
        //                        case "考核":
        //                        case "进京变更":
        //                            TASKNAME = "建筑施工企业专职安全生产管理人员安全生产考核（考核发证）";
        //                            break;
        //                        case "京内变更":
        //                            TASKNAME = "建筑施工企业专职安全生产管理人员安全生产考核 （省内变更受聘企业）";
        //                            break;
        //                        case "离京变更":
        //                            TASKNAME = "建筑施工企业专职安全生产管理人员安全生产考核（跨省变更受聘企业）";
        //                            break;
        //                        case "续期":
        //                            TASKNAME = "建筑施工企业专职安全生产管理人员安全生产考核 （证书延期）";
        //                            break;
        //                        case "注销":
        //                            TASKNAME = "建筑施工企业专职安全生产管理人员安全生产考核（证书注销）";
        //                            break;
        //                    }
        //                    break;
        //            }
        //            break;
        //        case "建筑施工特种作业":
        //            TASKNAME_MASTER = "建筑施工特种作业人员职业资格认定";
        //            TASKNAME_SUB = string.Format("建筑施工特种作业人员职业资格认定（{0}）", PostName.IndexOf("（") > 0 ? string.Format("〔{0}〕", PostName) : string.Format("（{0}）", PostName));
        //            switch (ApplyType)
        //            {
        //                case "考核":
        //                    TASKNAME = string.Format("建筑施工特种作业人员职业资格认定{0}（考核）", PostName.IndexOf("（") > 0 ? string.Format("[{0}]", PostName) : string.Format("（{0}）", PostName));
        //                    break;
        //                case "续期":
        //                    TASKNAME = string.Format("建筑施工特种作业人员职业资格认定{0}（延期复核）", PostName.IndexOf("（") > 0 ? string.Format("[{0}]", PostName) : string.Format("（{0}）", PostName));
        //                    break;
        //                case "注销":
        //                    TASKNAME = string.Format("建筑施工特种作业人员职业资格认定{0}（注销）", PostName.IndexOf("（") > 0 ? string.Format("[{0}]", PostName) : string.Format("（{0}）", PostName));
        //                    break;
        //            }
        //            break;
        //        case "建设职业技能岗位":
        //        case "新版建设职业技能岗位":
        //            TASKNAME_MASTER = "职业（工种）技能鉴定";
        //            TASKNAME_SUB = "";
        //             switch (ApplyType)
        //            {
        //                case "变更":
        //                    TASKNAME = "职业（工种）技能鉴定证书变更";
        //                    break;
        //                case "注销":
        //                    TASKNAME = "职业（工种）技能鉴定证书注销";
        //                    break;
        //            }
        //            break;
        //        default:
        //            break;
        //    }

        //    DataTable dt;

        //    if (Cache["GFSX"] != null)
        //    {
        //        dt= (DataTable)Cache["GFSX"];                
        //    }
        //    else
        //    {
        //        dt = CommonDAL.GetDataTable("select *  FROM [dbo].[dict_sx] order by [TASKNAME_MASTER],[TASKNAME_SUB],[TASKNAME],[REGION]");
        //        DataColumn[] pk = new DataColumn[4];
        //        pk[0] = dt.Columns["TASKNAME_MASTER"];
        //        pk[1] = dt.Columns["TASKNAME_SUB"];
        //        pk[2] = dt.Columns["TASKNAME"];
        //        pk[3] = dt.Columns["REGION"];
        //        dt.PrimaryKey = pk;

        //        Utility.CacheHelp.AddSlidingExpirationCache(Page, "GFSX", dt, 24);
        //    }

        //    DataRow find = dt.Rows.Find(new object[] { TASKNAME_MASTER, TASKNAME_SUB ,TASKNAME,REGION});
        //    if (find != null)
        //    {
        //        return find;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 获取政务行政事项大类（岗位类别）
        ///// </summary>
        ///// <param name="PostType"></param>
        ///// <returns></returns>
        //protected string GetTASKNAME_MASTER(string PostType)
        //{
        //    switch (PostType)
        //    {
        //        case "二级注册造价工程师":
        //            return "注册造价工程师注册";
        //        case "二级注册建造师":                    
        //            return "建造师执业资格认定";
        //        case "安全生产考核三类人员":                  
        //            return "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核";
        //        case "建筑施工特种作业":
        //            return "建筑施工特种作业人员职业资格认定";
        //        case "建设职业技能岗位":
        //        case "新版建设职业技能岗位":
        //            return "职业（工种）技能鉴定";
        //        default:
        //            break;
        //    }
        //    return "";    
        //}
        ///// <summary>
        ///// 获取政务行政事项小类（证书专业）
        ///// </summary>
        ///// <param name="PostType">岗位类别</param>
        ///// <param name="PostName">岗位工种</param>
        ///// <returns></returns>
        //protected string GetTASKNAME_SUB(string PostType,string PostName)
        //{
        //    switch (PostType)
        //    {
        //        case "二级注册造价工程师":
        //            return "二级注册造价工程师注册";
        //        case "二级注册建造师":
        //            switch (PostName.Split(',')[0])
        //            {
        //                case "公路":
        //                    return "二级建造师执业资格认定（公路工程专业）";
        //                case "机电":
        //                    return "二级建造师执业资格认定（机电工程专业）";
        //                case "建筑":
        //                    return "二级建造师执业资格认定（建筑工程专业）";
        //                case "矿业":
        //                    return "二级建造师执业资格认定（矿业工程专业）";
        //                case "市政":
        //                    return "二级建造师执业资格认定（市政公用工程专业）";
        //                case "水利":
        //                    return "二级建造师执业资格认定（水利水电工程专业）";
        //                default:
        //                    break;
        //            }
        //            return "";
        //        case "安全生产考核三类人员":
        //            switch (PostName)
        //            {
        //                case "企业主要负责人":
        //                    return "建筑施工企业项目负责人安全生产考核";
        //                case "项目负责人":
        //                    return "建筑施工企业主要负责人安全生产考核";
        //                case "土建类专职安全生产管理人员":
        //                case "机械类专职安全生产管理人员":
        //                case "综合类专职安全生产管理人员":
        //                    return "建筑施工企业专职安全生产管理人员安全生产考核";
        //            }
        //            return "";
        //        case "建筑施工特种作业":
        //            return string.Format("建筑施工特种作业人员职业资格认定（{0}）", PostName);
        //        default:
        //            break;
        //    }
        //    return "";
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RootUrl.ToLower().Contains("http://120.52.185.14") == false)
            {
                Response.Write("测试环境，不发起服务评价，请关闭此页面。");
                return;
            }

            if (string.IsNullOrEmpty(Request["t"]) || string.IsNullOrEmpty(Request["o"]))
            {
                Response.Write("无法获取要评价的内容！");
                ButtonPingJia.Visible = false;
                return;
            }

            string DataType = Utility.Cryptography.Decrypt(Request["t"]);//数据内容类型
            string DataID = Utility.Cryptography.Decrypt(Request["o"]);//数据ID

            CertificateOB _CertificateOB = null;
            projectServiceMDL _projectService = new projectServiceMDL();
            DataRow drSX = null;//公服事项
            try
            {
                switch (DataType)
                {
                    case "change"://变更
                        #region //变更发送内容

                        CertificateChangeOB _CertificateChangeOB = CertificateChangeDAL.GetObject(Convert.ToInt64(DataID));
                        _CertificateOB = CertificateDAL.GetObject(_CertificateChangeOB.CertificateID.Value);
                        if (_CertificateOB.PostTypeID.Value > 2)
                        {
                            _projectService.taskType = "07";
                        }

                        drSX = UIHelp.GetGFSX(_CertificateOB.PostTypeName,_CertificateOB.PostName,_CertificateChangeOB.ChangeType,"北京市");
       
                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = string.Format("0014BGSQ{0}", _CertificateChangeOB.CertificateChangeID);

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        _projectService.proStatus = (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed ? "3" : "1");

                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed)
                        {
                            _projectService.acceptDate = _CertificateChangeOB.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2}", "从业人员证书变更", _CertificateChangeOB.NewWorkerCertificateCode, _CertificateChangeOB.Status));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _CertificateChangeOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
                        divInfo.InnerHtml = string.Format("请申请人<b>{0}</b>对本次业务办理流程进行真实客观评价。<br />感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateChangeOB.ApplyMan);

                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _CertificateChangeOB.NewWorkerCertificateCode;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed)
                        {
                            _projectService.resultDate = _CertificateChangeOB.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _CertificateChangeOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
                        _projectService.bj_zt = (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed) ? "45" : "12";//办结：已提交

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = _CertificateChangeOB.LinkWay;

                        #endregion
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    case "exam"://考试
                        #region //考试发送内容
                        ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(Convert.ToInt64(DataID));
                        ExamPlanOB _ExamPlanOB = ExamPlanDAL.GetObject(_ExamSignUpOB.ExamPlanID.Value);

                        if (_ExamPlanOB.PostTypeID.Value > 2)
                        {
                            _projectService.taskType = "07";
                        }

                        drSX = UIHelp.GetGFSX(_ExamPlanOB.PostTypeName, _ExamPlanOB.PostName, "考核", "北京市");

                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = string.Format("0014KHSQ{0}", _ExamSignUpOB.ExamSignUpID);

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        _projectService.proStatus = (_ExamSignUpOB.Status == EnumManager.SignUpStatus.PayConfirmed ? "3" : "1");
                        switch (_ExamSignUpOB.Status)
                        {
                            case EnumManager.SignUpStatus.PayConfirmed:
                                _projectService.proStatus = "3";
                                break;
                            case EnumManager.SignUpStatus.NewSignUp:
                            case EnumManager.SignUpStatus.SaveSignUp:
                            case EnumManager.SignUpStatus.FirstChecked:
                                _projectService.proStatus = "1";
                                break;
                            default:
                                _projectService.proStatus = "2";
                                break;

                        }

                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_ExamSignUpOB.CheckDate.HasValue)
                        {
                            _projectService.acceptDate = _ExamSignUpOB.CheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},CheckDate={3}", "从业人员考试报名", _ExamSignUpOB.CertificateCode, _ExamSignUpOB.Status, _ExamSignUpOB.CheckDate));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _ExamSignUpOB.WorkerName;// dt.Rows[0]["WORKERNAME"].ToString();
                        divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _ExamSignUpOB.WorkerName);

                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _ExamSignUpOB.CertificateCode;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_ExamSignUpOB.PayConfirmDate.HasValue)
                        {
                            _projectService.resultDate = _ExamSignUpOB.PayConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _ExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd HH:mm:ss");


                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
                        switch (_ExamSignUpOB.Status)
                        {
                            case EnumManager.SignUpStatus.FirstChecked:
                                _projectService.bj_zt = "12";//提交
                                break;
                            case EnumManager.SignUpStatus.ReturnEdit:
                                _projectService.bj_zt = "15";//预审不通过
                                break;
                            case EnumManager.SignUpStatus.Checked:
                                _projectService.bj_zt = "20";//受理
                                break;
                            case EnumManager.SignUpStatus.PayNoticed:
                                _projectService.bj_zt = "21";//审查
                                break;
                            case EnumManager.SignUpStatus.PayConfirmed:
                                _projectService.bj_zt = "45";//决定
                                break;
                            case EnumManager.SignUpStatus.NewSignUp:
                                _projectService.bj_zt = "11";//草稿
                                break;
                            default:
                                _projectService.bj_zt = "11";
                                break;

                        }

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = _ExamSignUpOB.S_PHONE;

                        #endregion //考试发送内容
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    case "continue"://续期
                        #region //续期发送内容

                        CertificateContinueOB _CertificateContinueOB = CertificateContinueDAL.GetObject(Convert.ToInt64(DataID));
                        _CertificateOB = CertificateDAL.GetObject(_CertificateContinueOB.CertificateID.Value);

                        if (_CertificateOB.PostTypeID.Value > 2)
                        {
                            _projectService.taskType = "07";
                        }

                        drSX = UIHelp.GetGFSX(_CertificateOB.PostTypeName, _CertificateOB.PostName, "续期", "北京市");

                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = string.Format("0014XQSQ{0}", _CertificateContinueOB.CertificateContinueID);

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        switch (_CertificateContinueOB.Status)
                        {
                            case EnumManager.CertificateContinueStatus.Decided:
                                _projectService.proStatus = "3";
                                break;
                            case EnumManager.CertificateContinueStatus.NewSave:
                            case EnumManager.CertificateContinueStatus.WaitUnitCheck:
                            case EnumManager.CertificateContinueStatus.Applyed:
                                _projectService.proStatus = "1";
                                break;
                            default:
                                _projectService.proStatus = "2";
                                break;

                        }


                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_CertificateContinueOB.GetDate.HasValue)
                        {
                            _projectService.acceptDate = _CertificateContinueOB.GetDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},GetDate={3}", "从业人员证书续期", _CertificateOB.WorkerCertificateCode, _CertificateContinueOB.Status, _CertificateContinueOB.GetDate));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _CertificateContinueOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
                        divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateContinueOB.ApplyMan);

                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _CertificateOB.WorkerCertificateCode;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_CertificateContinueOB.Status == EnumManager.CertificateContinueStatus.Decided)
                        {
                            _projectService.resultDate = _CertificateContinueOB.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _CertificateContinueOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
                        switch (_CertificateContinueOB.Status)
                        {
                            case EnumManager.CertificateContinueStatus.Applyed:
                                _projectService.bj_zt = "12";//提交
                                break;
                            case EnumManager.CertificateContinueStatus.SendBack:
                                _projectService.bj_zt = "15";//预审不通过
                                break;
                            case EnumManager.CertificateContinueStatus.Accepted:
                                _projectService.bj_zt = "20";//受理
                                break;
                            case EnumManager.CertificateContinueStatus.Checked:
                                _projectService.bj_zt = "21";//审查
                                break;
                            case EnumManager.CertificateContinueStatus.Decided:
                                _projectService.bj_zt = "45";//决定
                                break;
                            case EnumManager.CertificateContinueStatus.NewSave:
                                _projectService.bj_zt = "11";//草稿
                                break;
                            default:
                                _projectService.bj_zt = "11";
                                break;

                        }

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = _CertificateContinueOB.Phone;

                        #endregion
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    case "JinJing"://进京
                        #region //进京发送内容
                        CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(DataID));

                        PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(_CertificateEnterApplyOB.PostTypeID.Value);

                        drSX = UIHelp.GetGFSX("安全生产考核三类人员", GetPostName(_CertificateEnterApplyOB.PostID.Value), "进京变更", "北京市");
                        
                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = string.Format("0014JJSQ{0}", _CertificateEnterApplyOB.ApplyID);

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        switch (_CertificateEnterApplyOB.ApplyStatus)
                        {
                            case EnumManager.CertificateEnterStatus.Decided:
                                _projectService.proStatus = "3";
                                break;
                            case EnumManager.CertificateEnterStatus.NewSave:
                            case EnumManager.CertificateEnterStatus.WaitUnitCheck:
                            case EnumManager.CertificateEnterStatus.Applyed:
                                _projectService.proStatus = "1";
                                break;
                            default:
                                _projectService.proStatus = "2";
                                break;

                        }

                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_CertificateEnterApplyOB.AcceptDate.HasValue)
                        {
                            _projectService.acceptDate = _CertificateEnterApplyOB.AcceptDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},AcceptDate={3}", "从业人员证书进京", _CertificateEnterApplyOB.WorkerCertificateCode, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.AcceptDate));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _CertificateEnterApplyOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
                        divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateEnterApplyOB.ApplyMan);

                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _CertificateEnterApplyOB.WorkerCertificateCode;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_CertificateEnterApplyOB.ApplyStatus == EnumManager.CertificateEnterStatus.Decided)
                        {
                            _projectService.resultDate = _CertificateEnterApplyOB.ConfrimDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
                        switch (_CertificateEnterApplyOB.ApplyStatus)
                        {
                            case EnumManager.CertificateEnterStatus.Applyed:
                                _projectService.bj_zt = "12";//提交
                                break;
                            case EnumManager.CertificateEnterStatus.SendBack:
                                _projectService.bj_zt = "15";//预审不通过
                                break;
                            case EnumManager.CertificateEnterStatus.Accepted:
                                _projectService.bj_zt = "20";//受理
                                break;
                            case EnumManager.CertificateEnterStatus.Checked:
                                _projectService.bj_zt = "21";//审查
                                break;
                            case EnumManager.CertificateEnterStatus.Decided:
                                _projectService.bj_zt = "45";//决定
                                break;
                            case EnumManager.CertificateEnterStatus.NewSave:
                                _projectService.bj_zt = "11";//草稿
                                break;
                            default:
                                _projectService.bj_zt = "11";
                                break;

                        }

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = _CertificateEnterApplyOB.Phone;

                        #endregion //进京发送内容
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    case "CertMore"://增发
                        #region 增发

                        CertificateMoreMDL _CertificateMoreMDL = CertificateMoreDAL.GetObject(Convert.ToInt64(DataID));
                        _CertificateOB = CertificateDAL.GetCertificateOBObject(_CertificateMoreMDL.CertificateCode);

                        drSX = UIHelp.GetGFSX(_CertificateOB.PostTypeName, _CertificateOB.PostName, "考核", "北京市");

                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = string.Format("0014ZF{0}", _CertificateMoreMDL.ApplyID);

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        switch (_CertificateMoreMDL.ApplyStatus)
                        {
                            case EnumManager.CertificateMore.Decided:
                                _projectService.proStatus = "3";
                                break;
                            case EnumManager.CertificateMore.NewSave:
                            case EnumManager.CertificateMore.WaitUnitCheck:
                            case EnumManager.CertificateMore.Applyed:
                                _projectService.proStatus = "1";
                                break;
                            default:
                                _projectService.proStatus = "2";
                                break;

                        }

                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided)
                        {
                            _projectService.acceptDate = _CertificateMoreMDL.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},AcceptDate={3}", "从业人员证书增发", _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.ApplyStatus, _CertificateMoreMDL.ConfirmDate));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _CertificateMoreMDL.WorkerName;// dt.Rows[0]["WORKERNAME"].ToString();

                        divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateMoreMDL.WorkerName);

                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _CertificateMoreMDL.WorkerCertificateCode;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided)
                        {
                            _projectService.resultDate = _CertificateMoreMDL.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _CertificateMoreMDL.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
                        _projectService.bj_zt = (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided) ? "45" : "12";//办结：已提交

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = _CertificateMoreMDL.PeoplePhone;

                        #endregion
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    case "apply"://二级建造师注册
                        #region //二建注册发送内容

                        ApplyMDL _ApplyMDL = ApplyDAL.GetObject(DataID);
                        WorkerOB _WorkerOB = WorkerDAL.GetUserObject(_ApplyMDL.PSN_CertificateNO);
                        string applyType = (_ApplyMDL.ApplyType == "变更注册" ? _ApplyMDL.ApplyTypeSub : _ApplyMDL.ApplyType);

                        #region DataTable zy 根据专业进行循环调用

                        DataTable zy = CommonDAL.GetDataTable(string.Format(@"
select z.[PSN_RegisteProfession]
FROM [dbo].[Apply] a
inner join 
(
	select ApplyID,additem1 as [PSN_RegisteProfession] from [dbo].[ApplyAddItem] where additem1 is not null 
	union 
	select ApplyID,additem2 as [PSN_RegisteProfession] from [dbo].[ApplyAddItem] where additem2 is not null 
) z on a.ApplyID = z.ApplyID
where a.applyid='{0}' 
union
select z.[PSN_RegisteProfession]
FROM [dbo].[Apply] a
inner join 
(
	select ApplyID,[PSN_RegisteProfession1] as [PSN_RegisteProfession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession1] is not null and [IfContinue1]=1
	union 
	select ApplyID,[PSN_RegisteProfession2] as [PSN_RegisteProfession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession2] is not null and [IfContinue2]=1
	union 
	select ApplyID,[PSN_RegisteProfession3] as [PSN_RegisteProfession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession3] is not null and [IfContinue3]=1
	union 
	select ApplyID,[PSN_RegisteProfession4] as [PSN_RegisteProfession] from [dbo].[ApplyContinue] where [PSN_RegisteProfession4] is not null and [IfContinue4]=1
) z on a.ApplyID = z.ApplyID
where a.applyid='{0}' 
union
select z.PSN_RegisteProfession
from [dbo].[Apply] a 
inner join  [dbo].[ApplyCancel] c on a.ApplyID = c.ApplyID
inner join
(
	select '公路' as PSN_RegisteProfession
	union select '机电'
	union select '建筑'
	union select '矿业'
	union select '市政'
	union select '水利'
) z on c.[ZyItem] like  '%' + z.PSN_RegisteProfession +'%'
where a.applyid='{0}'
union
select z.PSN_RegisteProfession
 from [dbo].[Apply] a inner join
(
	select '公路' as PSN_RegisteProfession
	union select '机电'
	union select '建筑'
	union select '矿业'
	union select '市政'
	union select '水利'
) z on a.PSN_RegisteProfession like  '%' + z.PSN_RegisteProfession +'%'
where   applyid='{0}' and (a.[ApplyType]='初始注册' or a.[ApplyType]='重新注册' or a.[ApplyType]='变更注册') ", _ApplyMDL.ApplyID));

                        #endregion 根据专业进行循环调用
                        foreach (DataRow r in zy.Rows)
                        {

                            drSX = UIHelp.GetGFSX("二级注册建造师"
                                 , r["PSN_RegisteProfession"].ToString()
                                , _ApplyMDL.ApplyType
                                , (_ApplyMDL.ApplyType == "变更注册" ? _ApplyMDL.ENT_City.Replace("亦庄", "经济技术开发区") : "北京市"));

                            /// 实施编码
                            _projectService.taskCode = drSX["TASKCODE"].ToString();

                            /// 业务办理项编码
                            _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                            /// 事项名称
                            _projectService.taskName = drSX["TASKNAME"].ToString();

                            /// 办件编号
                            _projectService.projectNo = _ApplyMDL.ApplyID;

                            // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                            if (_ApplyMDL.NoticeDate.HasValue)
                            {
                                _projectService.proStatus = "3";
                            }
                            else if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                            {
                                _projectService.proStatus = "1";
                            }
                            else
                            {
                                _projectService.proStatus = "2";
                            }


                            /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                            if (_ApplyMDL.GetDateTime.HasValue || _ApplyMDL.CheckDate.HasValue)
                            {
                                _projectService.acceptDate = _ApplyMDL.GetDateTime.HasValue ? _ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : _ApplyMDL.CheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else if (_projectService.proStatus != "1")
                            {
                                FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，PSN_CertificateNO={1}，ApplyStatus={2},NoticeDate={3}", applyType, _ApplyMDL.PSN_CertificateNO, _ApplyMDL.ApplyStatus, _ApplyMDL.NoticeDate));
                                Response.Write("发送评价数据错误！");
                            }

                            /// 申请单位名称/申请人名称
                            _projectService.userName = _ApplyMDL.PSN_Name;// dt.Rows[0]["WORKERNAME"].ToString();

                            divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _ApplyMDL.PSN_Name);

                            /// 申请人证件类型（见 A.2证件类型）
                            _projectService.userPageType = "111";

                            /// 申请人证件号码
                            _projectService.certKey = _ApplyMDL.PSN_CertificateNO;

                            /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                            if (_ApplyMDL.NoticeDate.HasValue)
                            {
                                _projectService.resultDate = _ApplyMDL.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            }

                            /// 申请时间 办件申请、补正必填
                            _projectService.applydate = _ApplyMDL.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                            /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
                            switch (_ApplyMDL.ApplyStatus)
                            {
                                case EnumManager.ApplyStatus.已申报:
                                    _projectService.bj_zt = "12";//提交
                                    break;
                                case EnumManager.ApplyStatus.已驳回:
                                    _projectService.bj_zt = "15";//预审不通过
                                    break;
                                case EnumManager.ApplyStatus.已受理:
                                    _projectService.bj_zt = "20";//受理
                                    break;
                                case EnumManager.ApplyStatus.区县审查:
                                    _projectService.bj_zt = "21";//审查
                                    break;
                                case EnumManager.ApplyStatus.已公告:
                                    _projectService.bj_zt = "45";//决定
                                    break;
                                case EnumManager.ApplyStatus.待确认:
                                case EnumManager.ApplyStatus.未申报:
                                    _projectService.bj_zt = "11";//草稿
                                    break;
                                default:
                                    _projectService.bj_zt = "11";
                                    break;

                            }

                            /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                            _projectService.taskguid = drSX["ROWGUID"].ToString();

                            /// 联系人手机号
                            _projectService.telPhone = _WorkerOB.Mobile;

                            SendMessage(_projectService, DataType, DataID, drSX);
                        }

                        #endregion
                        break;
                    case "applyZJS"://二级造价工程师注册
                        #region //二级造价工程师注册

                        zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(DataID);

                        WorkerOB w = WorkerDAL.GetUserObject(_zjs_ApplyMDL.PSN_CertificateNO);

                        drSX = UIHelp.GetGFSX("二级注册造价工程师", "注册造价工程师注册", _zjs_ApplyMDL.ApplyType, "北京市");

                        /// 实施编码
                        _projectService.taskCode = drSX["TASKCODE"].ToString();

                        /// 业务办理项编码
                        _projectService.taskHandleItem = drSX["TASKHANDLEITEM"].ToString();

                        /// 事项名称
                        _projectService.taskName = drSX["TASKNAME"].ToString();

                        /// 办件编号
                        _projectService.projectNo = _zjs_ApplyMDL.ApplyID;

                        // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
                        if (_zjs_ApplyMDL.NoticeDate.HasValue)
                        {
                            _projectService.proStatus = "3";
                        }
                        else if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
                        {
                            _projectService.proStatus = "1";
                        }
                        else
                        {
                            _projectService.proStatus = "2";
                        }


                        /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
                        if (_zjs_ApplyMDL.GetDateTime.HasValue )
                        {
                            _projectService.acceptDate = _zjs_ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_projectService.proStatus != "1")
                        {
                            FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，PSN_CertificateNO={1}，Status={2},AcceptDate={3}", "二级造价工程师注册", _zjs_ApplyMDL.PSN_CertificateNO, _zjs_ApplyMDL.ApplyStatus, _zjs_ApplyMDL.GetDateTime));
                            Response.Write("发送评价数据错误！");
                        }

                        /// 申请单位名称/申请人名称
                        _projectService.userName = _zjs_ApplyMDL.PSN_Name;// dt.Rows[0]["WORKERNAME"].ToString();

                        divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _zjs_ApplyMDL.PSN_Name);


                        /// 申请人证件类型（见 A.2证件类型）
                        _projectService.userPageType = "111";

                        /// 申请人证件号码
                        _projectService.certKey = _zjs_ApplyMDL.PSN_CertificateNO;

                        /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
                        if (_zjs_ApplyMDL.NoticeDate.HasValue)
                        {
                            _projectService.resultDate = _zjs_ApplyMDL.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        /// 申请时间 办件申请、补正必填
                        _projectService.applydate = _zjs_ApplyMDL.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
                        switch (_zjs_ApplyMDL.ApplyStatus)
                        {
                            case EnumManager.ZJSApplyStatus.已申报:
                                _projectService.bj_zt = "12";//提交
                                break;
                            case EnumManager.ZJSApplyStatus.已驳回:
                                _projectService.bj_zt = "15";//预审不通过
                                break;
                            case EnumManager.ZJSApplyStatus.已受理:
                                _projectService.bj_zt = "20";//受理
                                break;
                            case EnumManager.ZJSApplyStatus.已审核:
                                _projectService.bj_zt = "21";//审查
                                break;
                            case EnumManager.ZJSApplyStatus.已决定:
                                _projectService.bj_zt = "45";//决定
                                break;
                            case EnumManager.ZJSApplyStatus.待确认:
                            case EnumManager.ZJSApplyStatus.未申报:
                                _projectService.bj_zt = "11";//草稿
                                break;
                            default:
                                _projectService.bj_zt = "11";
                                break;

                        }

                        /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
                        _projectService.taskguid = drSX["ROWGUID"].ToString();

                        /// 联系人手机号
                        _projectService.telPhone = w.Mobile;

                        #endregion
                        SendMessage(_projectService, DataType, DataID, drSX);
                        break;
                    default:
                        Response.Write("该类型数据尚不支持评价！");
                        break;
                }                
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "评价申报业务服务发生错误", ex);
            }
        }

        protected void SendMessage(projectServiceMDL _projectService, string DataType, string DataID, DataRow drSX)
        {
            #region 推送要评价的申请信息

            //测试地址
            //string apiUrl = "http://43.254.24.244/pubservice/appraise4OT/reShareApprInfo?";

            //正式地址
            string apiUrl = "http://banshi.beijing.gov.cn/pubservice/appraise4OT/reShareApprInfo?";
            string pubk = "040160054ff31ee703c1b678ad38bab57b857e52af3bf1c33e07a5aef203639c0c383a635cc2f2f87573faabd8cecc7f355c8af8ada8ad90df816472a55ac4b7c7";//公钥
            string client_secret = "7f243b9d28e04ea6bdc0cfd970b51ea8";//大厅加密秘钥
            string source = "BZFGLXXXT";//系统唯一标识
            string token = SM2Utils.generate(client_secret + source);

            AppraiseMDL _AppraiseMDL = new AppraiseMDL();
            projectServiceParams _projectServiceParams = new projectServiceParams();

            #region //固定发送内容

            _AppraiseMDL.appMark = "BZFGLXXXT";
            _AppraiseMDL.time = DateTime.Now.ToString("yyyyMMddHHmmss");

            ///// 受理部门编码        
            //_projectService.orgcode = "11110000000021135M";

            ///// 受理部门名称
            //_projectService.orgName = "北京市住房和城乡建设委员会";

            /// 受理部门编码        
            _projectService.orgcode = drSX["DEPTCODE"].ToString();

            /// 受理部门名称
            _projectService.orgName = drSX["DEPTNAME"].ToString();

            /// 代理人姓名
            _projectService.handleUserName = "";

            /// 代理人证件类型（见 A.2证件类型）
            _projectService.handleUserPageType = "";

            /// 代理人证件号码
            _projectService.handleUserPageCode = "";

            /// 服务名称 咨询、申报、补正、缴费、签收（isService 值为1 时必填）
            _projectService.serviceName = "申报";

            /// 是否为好差评服务办件1-是，0-否（当产生咨询、申报、补正、缴费及办结等服务时，该字段值为 1）
            _projectService.isService = "1";

            /// 服务时间 yyyy-MM-dd HH:mm:ss （isService 值为1 时必填）
            _projectService.serviceTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            /// 服务日期 yyyy-MM-dd（isService 值为1 时必填）
            _projectService.serviceDate = DateTime.Now.ToString("yyyy-MM-dd");

            /// 数据来源（默认值为 111）
            _projectService.dataSource = "111";

            /// 办件类型： 即办件=1，承诺办件=2
            _projectService.projectType = 2;

            /// 申请人类型 自然人=1，企业法人=2，事业法人=3，社会组织法人=4，非法人企业=5，行政机关=6，其他组织=9
            _projectService.userProp = "1";

            /// 办件详情查看地址（需要是对接了统一认证平台的单点登录地址）
            _projectService.statusUrl = "";

            /// 行政区划（见 A.3 北京市行政区划代码）
            _projectService.administrative_div = "00";

            /// 服务数据初始来源标识，默认与 appMark 值相同
            _projectService.source = _AppraiseMDL.appMark;

            /// 服务数据来源，默认与appMark 值相同
            _projectService.sourceTJ = _AppraiseMDL.appMark;

            /// 位行政区划（非太极不用填写，太极必填）
            _projectService.areaCode = "";

            /// 事项主题
            _projectService.subMatter = "";

            _projectService.taskType = "01";

            #endregion //固定发送内容

            _projectServiceParams.projectService = _projectService;
            string _params = string.Format(@"[{0}]", Utility.JSONHelp.Encode(_projectServiceParams));
            _AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", SM2Utils.Encrypt(pubk, _params));
            _AppraiseMDL.sign = SM2Utils.Encrypt(pubk, string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));
            string rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("appMark={0}&sign={1}&params={2}&time={3}", _AppraiseMDL.appMark, _AppraiseMDL.sign, _AppraiseMDL.Params, _AppraiseMDL.time));

            AppraiseResultMDL _AppraiseResultMDL = Newtonsoft.Json.JsonConvert.DeserializeObject<AppraiseResultMDL>(rtn);

            if (_AppraiseResultMDL.success == true)
            {
                ViewState["serviceCode"] = _AppraiseResultMDL.serviceCode;
                ViewState["token"] = token;
                FileLog.WriteLog(string.Format("评价申报业务【{0}】,编号：{1}", _projectService.taskName, _projectService.projectNo));
                //string toUrl = string.Format("http://banshi.beijing.gov.cn/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", _AppraiseResultMDL.serviceCode, "", source, token);
                //Response.Redirect(toUrl, true);
            }
            else
            {
                FileLog.WriteLog(string.Format("评价申报业务服务发生错误：{0}", _AppraiseResultMDL.errorMessage));
            }

            #endregion
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(Request["t"]) || string.IsNullOrEmpty(Request["o"]))
        //    {
        //        Response.Write("无法获取要评价的内容！");
        //        ButtonPingJia.Visible = false;
        //        return;
        //    }

        //    string DataType = Utility.Cryptography.Decrypt(Request["t"]);//数据内容类型
        //    string DataID = Utility.Cryptography.Decrypt(Request["o"]);//数据ID

        //    CertificateOB _CertificateOB = null;
        //    projectServiceMDL _projectService = new projectServiceMDL();

        //    try
        //    {
        //        switch (DataType)
        //        {
        //            case "change"://变更
        //                #region //变更发送内容

        //                CertificateChangeOB _CertificateChangeOB = CertificateChangeDAL.GetObject(Convert.ToInt64(DataID));
        //                _CertificateOB = CertificateDAL.GetObject(_CertificateChangeOB.CertificateID.Value);
        //                if (_CertificateOB.PostTypeID.Value > 2)
        //                {
        //                    _projectService.taskType = "07";
        //                }

        //                //DataTable dt = CommonDAL.GetDataTable("SELECT top 1 *  FROM [dbo].[VIEW_CERTIFICATECHANGE]   where [NOTICEDATE] >'2000-1-1' and posttypeid =1 and [CHANGETYPE]='京内变更' and len([LINKWAY])>1  order by [NOTICEDATE] desc");

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode(_CertificateOB.PostTypeName);

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem(_CertificateOB.PostTypeName, _CertificateChangeOB.ChangeType);

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName(_CertificateOB.PostTypeName, _CertificateChangeOB.ChangeType);

        //                /// 办件编号
        //                _projectService.projectNo = string.Format("0014BGSQ{0}", _CertificateChangeOB.CertificateChangeID);

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                _projectService.proStatus = (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed ? "3" : "1");

        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed)
        //                {
        //                    _projectService.acceptDate = _CertificateChangeOB.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2}", "从业人员证书变更", _CertificateChangeOB.NewWorkerCertificateCode, _CertificateChangeOB.Status));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _CertificateChangeOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
        //                divInfo.InnerHtml = string.Format("请申请人<b>{0}</b>对本次业务办理流程进行真实客观评价。<br />感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateChangeOB.ApplyMan);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _CertificateChangeOB.NewWorkerCertificateCode;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed)
        //                {
        //                    _projectService.resultDate = _CertificateChangeOB.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _CertificateChangeOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
        //                _projectService.bj_zt = (_CertificateChangeOB.Status == EnumManager.CertificateChangeStatus.Noticed) ? "45" : "12";//办结：已提交

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID(_CertificateOB.PostTypeName, _CertificateChangeOB.ChangeType);//"24ace28c-9565-11e9-8300-507b9d3e4710";//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _CertificateChangeOB.LinkWay;

        //                #endregion
        //                break;
        //            case "exam"://考试
        //                #region //考试发送内容
        //                ExamSignUpOB _ExamSignUpOB = ExamSignUpDAL.GetObject(Convert.ToInt64(DataID));
        //                ExamPlanOB _ExamPlanOB = ExamPlanDAL.GetObject(_ExamSignUpOB.ExamPlanID.Value);

        //                if (_ExamPlanOB.PostTypeID.Value > 2)
        //                {
        //                    _projectService.taskType = "07";
        //                }

        //                //DataTable dt = CommonDAL.GetDataTable("SELECT top 1 *  FROM [dbo].[VIEW_CERTIFICATECHANGE]   where [NOTICEDATE] >'2000-1-1' and posttypeid =1 and [CHANGETYPE]='京内变更' and len([LINKWAY])>1  order by [NOTICEDATE] desc");

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode(_ExamPlanOB.PostTypeName);

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem(_ExamPlanOB.PostTypeName, "考试报名");

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName(_ExamPlanOB.PostTypeName, "考试报名");

        //                /// 办件编号
        //                _projectService.projectNo = string.Format("0014KHSQ{0}", _ExamSignUpOB.ExamSignUpID);

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                _projectService.proStatus = (_ExamSignUpOB.Status == EnumManager.SignUpStatus.PayConfirmed ? "3" : "1");
        //                switch (_ExamSignUpOB.Status)
        //                {
        //                    case EnumManager.SignUpStatus.PayConfirmed:
        //                        _projectService.proStatus = "3";
        //                        break;
        //                    case EnumManager.SignUpStatus.NewSignUp:
        //                    case EnumManager.SignUpStatus.SaveSignUp:
        //                    case EnumManager.SignUpStatus.FirstChecked:
        //                        _projectService.proStatus = "1";
        //                        break;
        //                    default:
        //                        _projectService.proStatus = "2";
        //                        break;

        //                }

        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_ExamSignUpOB.CheckDate.HasValue)
        //                {
        //                    _projectService.acceptDate = _ExamSignUpOB.CheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},CheckDate={3}", "从业人员考试报名", _ExamSignUpOB.CertificateCode, _ExamSignUpOB.Status, _ExamSignUpOB.CheckDate));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _ExamSignUpOB.WorkerName;// dt.Rows[0]["WORKERNAME"].ToString();
        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _ExamSignUpOB.WorkerName);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _ExamSignUpOB.CertificateCode;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_ExamSignUpOB.PayConfirmDate.HasValue)
        //                {
        //                    _projectService.resultDate = _ExamSignUpOB.PayConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _ExamSignUpOB.SignUpDate.Value.ToString("yyyy-MM-dd HH:mm:ss");


        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
        //                switch (_ExamSignUpOB.Status)
        //                {
        //                    case EnumManager.SignUpStatus.FirstChecked:
        //                        _projectService.bj_zt = "12";//提交
        //                        break;
        //                    case EnumManager.SignUpStatus.ReturnEdit:
        //                        _projectService.bj_zt = "15";//预审不通过
        //                        break;
        //                    case EnumManager.SignUpStatus.Checked:
        //                        _projectService.bj_zt = "20";//受理
        //                        break;
        //                    case EnumManager.SignUpStatus.PayNoticed:
        //                        _projectService.bj_zt = "21";//审查
        //                        break;
        //                    case EnumManager.SignUpStatus.PayConfirmed:
        //                        _projectService.bj_zt = "45";//决定
        //                        break;
        //                    case EnumManager.SignUpStatus.NewSignUp:
        //                        _projectService.bj_zt = "11";//草稿
        //                        break;
        //                    default:
        //                        _projectService.bj_zt = "11";
        //                        break;

        //                }

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID(_ExamPlanOB.PostTypeName, "考试报名");//"24ace28c-9565-11e9-8300-507b9d3e4710";//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _ExamSignUpOB.S_PHONE;

        //                #endregion //考试发送内容
        //                break;
        //            case "continue"://续期
        //                #region //续期发送内容

        //                CertificateContinueOB _CertificateContinueOB = CertificateContinueDAL.GetObject(Convert.ToInt64(DataID));
        //                _CertificateOB = CertificateDAL.GetObject(_CertificateContinueOB.CertificateID.Value);

        //                if (_CertificateOB.PostTypeID.Value > 2)
        //                {
        //                    _projectService.taskType = "07";
        //                }

        //                //DataTable dt = CommonDAL.GetDataTable("SELECT top 1 *  FROM [dbo].[VIEW_CERTIFICATECHANGE]   where [NOTICEDATE] >'2000-1-1' and posttypeid =1 and [CHANGETYPE]='京内变更' and len([LINKWAY])>1  order by [NOTICEDATE] desc");

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode(_CertificateOB.PostTypeName);

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem(_CertificateOB.PostTypeName, "续期");

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName(_CertificateOB.PostTypeName, "续期");

        //                /// 办件编号
        //                _projectService.projectNo = string.Format("0014XQSQ{0}", _CertificateContinueOB.CertificateContinueID);

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                switch (_CertificateContinueOB.Status)
        //                {
        //                    case EnumManager.CertificateContinueStatus.Decided:
        //                        _projectService.proStatus = "3";
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.NewSave:
        //                    case EnumManager.CertificateContinueStatus.WaitUnitCheck:
        //                    case EnumManager.CertificateContinueStatus.Applyed:
        //                        _projectService.proStatus = "1";
        //                        break;
        //                    default:
        //                        _projectService.proStatus = "2";
        //                        break;

        //                }


        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_CertificateContinueOB.GetDate.HasValue)
        //                {
        //                    _projectService.acceptDate = _CertificateContinueOB.GetDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},GetDate={3}", "从业人员证书续期", _CertificateOB.WorkerCertificateCode, _CertificateContinueOB.Status, _CertificateContinueOB.GetDate));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _CertificateContinueOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateContinueOB.ApplyMan);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _CertificateOB.WorkerCertificateCode;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_CertificateContinueOB.Status == EnumManager.CertificateContinueStatus.Decided)
        //                {
        //                    _projectService.resultDate = _CertificateContinueOB.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _CertificateContinueOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
        //                switch (_CertificateContinueOB.Status)
        //                {
        //                    case EnumManager.CertificateContinueStatus.Applyed:
        //                        _projectService.bj_zt = "12";//提交
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.SendBack:
        //                        _projectService.bj_zt = "15";//预审不通过
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.Accepted:
        //                        _projectService.bj_zt = "20";//受理
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.Checked:
        //                        _projectService.bj_zt = "21";//审查
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.Decided:
        //                        _projectService.bj_zt = "45";//决定
        //                        break;
        //                    case EnumManager.CertificateContinueStatus.NewSave:
        //                        _projectService.bj_zt = "11";//草稿
        //                        break;
        //                    default:
        //                        _projectService.bj_zt = "11";
        //                        break;

        //                }

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID(_CertificateOB.PostTypeName, "续期");//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _CertificateContinueOB.Phone;

        //                #endregion
        //                break;
        //            case "JinJing"://进京
        //                #region //进京发送内容
        //                CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(DataID));

        //                PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(_CertificateEnterApplyOB.PostTypeID.Value);


        //                //DataTable dt = CommonDAL.GetDataTable("SELECT top 1 *  FROM [dbo].[VIEW_CERTIFICATECHANGE]   where [NOTICEDATE] >'2000-1-1' and posttypeid =1 and [CHANGETYPE]='京内变更' and len([LINKWAY])>1  order by [NOTICEDATE] desc");

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode(_PostInfoOB.PostName);

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem(_PostInfoOB.PostName, "进京变更");

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName(_PostInfoOB.PostName, "进京变更");

        //                /// 办件编号
        //                _projectService.projectNo = string.Format("0014JJSQ{0}", _CertificateEnterApplyOB.ApplyID);

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                switch (_CertificateEnterApplyOB.ApplyStatus)
        //                {
        //                    case EnumManager.CertificateEnterStatus.Decided:
        //                        _projectService.proStatus = "3";
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.NewSave:
        //                    case EnumManager.CertificateEnterStatus.WaitUnitCheck:
        //                    case EnumManager.CertificateEnterStatus.Applyed:
        //                        _projectService.proStatus = "1";
        //                        break;
        //                    default:
        //                        _projectService.proStatus = "2";
        //                        break;

        //                }

        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_CertificateEnterApplyOB.ApplyStatus == EnumManager.CertificateEnterStatus.Accepted)
        //                {
        //                    _projectService.acceptDate = _CertificateEnterApplyOB.AcceptDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},AcceptDate={3}", "从业人员证书进京", _CertificateEnterApplyOB.WorkerCertificateCode, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.AcceptDate));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _CertificateEnterApplyOB.ApplyMan;// dt.Rows[0]["WORKERNAME"].ToString();
        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateEnterApplyOB.ApplyMan);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _CertificateEnterApplyOB.WorkerCertificateCode;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_CertificateEnterApplyOB.ApplyStatus == EnumManager.CertificateEnterStatus.Decided)
        //                {
        //                    _projectService.resultDate = _CertificateEnterApplyOB.ConfrimDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _CertificateEnterApplyOB.ApplyDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
        //                switch (_CertificateEnterApplyOB.ApplyStatus)
        //                {
        //                    case EnumManager.CertificateEnterStatus.Applyed:
        //                        _projectService.bj_zt = "12";//提交
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.SendBack:
        //                        _projectService.bj_zt = "15";//预审不通过
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.Accepted:
        //                        _projectService.bj_zt = "20";//受理
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.Checked:
        //                        _projectService.bj_zt = "21";//审查
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.Decided:
        //                        _projectService.bj_zt = "45";//决定
        //                        break;
        //                    case EnumManager.CertificateEnterStatus.NewSave:
        //                        _projectService.bj_zt = "11";//草稿
        //                        break;
        //                    default:
        //                        _projectService.bj_zt = "11";
        //                        break;

        //                }

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID(_PostInfoOB.PostName, "进京变更");//"24ace28c-9565-11e9-8300-507b9d3e4710";//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _CertificateEnterApplyOB.Phone;

        //                #endregion //进京发送内容
        //                break;
        //            case "CertMore"://增发
        //                #region 增发

        //                CertificateMoreMDL _CertificateMoreMDL = CertificateMoreDAL.GetObject(Convert.ToInt64(DataID));
        //                _CertificateOB = CertificateDAL.GetCertificateOBObject(_CertificateMoreMDL.CertificateCode);

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode(_CertificateOB.PostTypeName);

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem(_CertificateOB.PostTypeName, "增发");

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName(_CertificateOB.PostTypeName, "增发");

        //                /// 办件编号
        //                _projectService.projectNo = string.Format("0014ZF{0}", _CertificateMoreMDL.ApplyID);

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                switch (_CertificateMoreMDL.ApplyStatus)
        //                {
        //                    case EnumManager.CertificateMore.Decided:
        //                        _projectService.proStatus = "3";
        //                        break;
        //                    case EnumManager.CertificateMore.NewSave:
        //                    case EnumManager.CertificateMore.WaitUnitCheck:
        //                    case EnumManager.CertificateMore.Applyed:
        //                        _projectService.proStatus = "1";
        //                        break;
        //                    default:
        //                        _projectService.proStatus = "2";
        //                        break;

        //                }

        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided)
        //                {
        //                    _projectService.acceptDate = _CertificateMoreMDL.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，WorkerCertificateCode={1}，Status={2},AcceptDate={3}", "从业人员证书增发", _CertificateMoreMDL.WorkerCertificateCode, _CertificateMoreMDL.ApplyStatus, _CertificateMoreMDL.ConfirmDate));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _CertificateMoreMDL.WorkerName;// dt.Rows[0]["WORKERNAME"].ToString();

        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _CertificateMoreMDL.WorkerName);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _CertificateMoreMDL.WorkerCertificateCode;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided)
        //                {
        //                    _projectService.resultDate = _CertificateMoreMDL.ConfirmDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _CertificateMoreMDL.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态
        //                _projectService.bj_zt = (_CertificateMoreMDL.ApplyStatus == EnumManager.CertificateMore.Decided) ? "45" : "12";//办结：已提交

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID(_CertificateOB.PostTypeName, "增发");//"24ace28c-9565-11e9-8300-507b9d3e4710";//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _CertificateMoreMDL.PeoplePhone;

        //                #endregion
        //                break;
        //            case "apply"://二级建造师注册
        //                #region //二建注册发送内容

        //                ApplyMDL _ApplyMDL = ApplyDAL.GetObject(DataID);

        //                WorkerOB _WorkerOB = WorkerDAL.GetUserObject(_ApplyMDL.PSN_CertificateNO);

        //                string applyType = (_ApplyMDL.ApplyType == "变更注册" ? _ApplyMDL.ApplyTypeSub : _ApplyMDL.ApplyType);

        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode("二级注册建造师");

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem("二级注册建造师", applyType);

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName("二级注册建造师", applyType);

        //                /// 办件编号
        //                _projectService.projectNo = _ApplyMDL.ApplyID;

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                if (_ApplyMDL.NoticeDate.HasValue)
        //                {
        //                    _projectService.proStatus = "3";
        //                }
        //                else if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.待确认 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.未申报 || _ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
        //                {
        //                    _projectService.proStatus = "1";
        //                }
        //                else
        //                {
        //                    _projectService.proStatus = "2";
        //                }


        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_ApplyMDL.AcceptDate.HasValue)
        //                {
        //                    _projectService.acceptDate = _ApplyMDL.AcceptDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，PSN_CertificateNO={1}，ApplyStatus={2},NoticeDate={3}", applyType, _ApplyMDL.PSN_CertificateNO, _ApplyMDL.ApplyStatus, _ApplyMDL.NoticeDate));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _ApplyMDL.PSN_Name;// dt.Rows[0]["WORKERNAME"].ToString();

        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _ApplyMDL.PSN_Name);

        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _ApplyMDL.PSN_CertificateNO;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_ApplyMDL.NoticeDate.HasValue)
        //                {
        //                    _projectService.resultDate = _ApplyMDL.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _ApplyMDL.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
        //                switch (_ApplyMDL.ApplyStatus)
        //                {
        //                    case EnumManager.ApplyStatus.已申报:
        //                        _projectService.bj_zt = "12";//提交
        //                        break;
        //                    case EnumManager.ApplyStatus.已驳回:
        //                        _projectService.bj_zt = "15";//预审不通过
        //                        break;
        //                    case EnumManager.ApplyStatus.已受理:
        //                        _projectService.bj_zt = "20";//受理
        //                        break;
        //                    case EnumManager.ApplyStatus.区县审查:
        //                        _projectService.bj_zt = "21";//审查
        //                        break;
        //                    case EnumManager.ApplyStatus.已公告:
        //                        _projectService.bj_zt = "45";//决定
        //                        break;
        //                    case EnumManager.ApplyStatus.待确认:
        //                    case EnumManager.ApplyStatus.未申报:
        //                        _projectService.bj_zt = "11";//草稿
        //                        break;
        //                    default:
        //                        _projectService.bj_zt = "11";
        //                        break;

        //                }

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID("二级注册建造师", applyType);//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = _WorkerOB.Mobile;

        //                #endregion
        //                break;
        //            case "applyZJS"://二级造价工程师注册
        //                #region //二级造价工程师注册

        //                zjs_ApplyMDL _zjs_ApplyMDL = zjs_ApplyDAL.GetObject(DataID);

        //                WorkerOB w = WorkerDAL.GetUserObject(_zjs_ApplyMDL.PSN_CertificateNO);


        //                /// 实施编码
        //                _projectService.taskCode = GetTaskCode("二级注册造价工程师");

        //                /// 业务办理项编码
        //                _projectService.taskHandleItem = taskHandleItem("二级注册造价工程师", "二级造价工程师注册");

        //                /// 事项名称
        //                _projectService.taskName = GetTaskName("二级注册造价工程师", "二级造价工程师注册");

        //                /// 办件编号
        //                _projectService.projectNo = _zjs_ApplyMDL.ApplyID;

        //                // 办理状态, 待受理=1，已受理=2，已办结=3否（isService 值为1 时必填）
        //                if (_zjs_ApplyMDL.NoticeDate.HasValue)
        //                {
        //                    _projectService.proStatus = "3";
        //                }
        //                else if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.待确认 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || _zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已申报)
        //                {
        //                    _projectService.proStatus = "1";
        //                }
        //                else
        //                {
        //                    _projectService.proStatus = "2";
        //                }


        //                /// 受理时间 yyyy-MM-dd HH:mm:ss 当办理状态=2 或 3 时，此项必填
        //                if (_zjs_ApplyMDL.GetDateTime.HasValue)
        //                {
        //                    _projectService.acceptDate = _zjs_ApplyMDL.GetDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else if (_projectService.proStatus != "1")
        //                {
        //                    FileLog.WriteLog(string.Format("发送评价数据错误：applyType={0}，PSN_CertificateNO={1}，Status={2},AcceptDate={3}", "二级造价工程师注册", _zjs_ApplyMDL.PSN_CertificateNO, _zjs_ApplyMDL.ApplyStatus, _zjs_ApplyMDL.GetDateTime));
        //                    Response.Write("发送评价数据错误！");
        //                }

        //                /// 申请单位名称/申请人名称
        //                _projectService.userName = _zjs_ApplyMDL.PSN_Name;// dt.Rows[0]["WORKERNAME"].ToString();

        //                divInfo.InnerText = string.Format("请申请人{0}对本次业务办理流程进行真实客观评价。感谢您对我们工作的支持，针对您反馈的意见和建议，我们将及时与您及所在单位进行沟通，请保持手机畅通。", _zjs_ApplyMDL.PSN_Name);


        //                /// 申请人证件类型（见 A.2证件类型）
        //                _projectService.userPageType = "111";

        //                /// 申请人证件号码
        //                _projectService.certKey = _zjs_ApplyMDL.PSN_CertificateNO;

        //                /// 办结时间 yyyy-MM-dd HH:mm:ss 当办理状态=3 时，此项必填
        //                if (_zjs_ApplyMDL.NoticeDate.HasValue)
        //                {
        //                    _projectService.resultDate = _zjs_ApplyMDL.NoticeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //                }

        //                /// 申请时间 办件申请、补正必填
        //                _projectService.applydate = _zjs_ApplyMDL.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

        //                /// 真正办理状态 办件表真正状态，转换成好差评要求的状态之前的状态 见A.1 办件状态              
        //                switch (_zjs_ApplyMDL.ApplyStatus)
        //                {
        //                    case EnumManager.ZJSApplyStatus.已申报:
        //                        _projectService.bj_zt = "12";//提交
        //                        break;
        //                    case EnumManager.ZJSApplyStatus.已驳回:
        //                        _projectService.bj_zt = "15";//预审不通过
        //                        break;
        //                    case EnumManager.ZJSApplyStatus.已受理:
        //                        _projectService.bj_zt = "20";//受理
        //                        break;
        //                    case EnumManager.ZJSApplyStatus.已审核:
        //                        _projectService.bj_zt = "21";//审查
        //                        break;
        //                    case EnumManager.ZJSApplyStatus.已决定:
        //                        _projectService.bj_zt = "45";//决定
        //                        break;
        //                    case EnumManager.ZJSApplyStatus.待确认:
        //                    case EnumManager.ZJSApplyStatus.未申报:
        //                        _projectService.bj_zt = "11";//草稿
        //                        break;
        //                    default:
        //                        _projectService.bj_zt = "11";
        //                        break;

        //                }

        //                /// 事项唯一性标识，对应事项记录唯 一 标 识（ROWGUID）
        //                _projectService.taskguid = GetTaskID("二级注册造价工程师", "二级造价工程师注册");//从下发Excel列表中获取

        //                /// 联系人手机号
        //                _projectService.telPhone = w.Mobile;

        //                #endregion
        //                break;
        //            default:
        //                Response.Write("该类型数据尚不支持评价！");
        //                break;
        //        }

        //        ViewState["projectServiceMDL"] = _projectService;

        //        #region 推送要评价的申请信息

        //        if (RootUrl.ToLower().Contains("http://120.52.185.14") == false)
        //        {
        //            //Response.Write("测试环境，不发起服务评价，请关闭此页面。");
        //            return;
        //        }

        //        //测试地址
        //        //string apiUrl = "http://43.254.24.244/pubservice/appraise4OT/reShareApprInfo?";

        //        //正式地址
        //        string apiUrl = "http://banshi.beijing.gov.cn/pubservice/appraise4OT/reShareApprInfo?";
        //        string pubk = "040160054ff31ee703c1b678ad38bab57b857e52af3bf1c33e07a5aef203639c0c383a635cc2f2f87573faabd8cecc7f355c8af8ada8ad90df816472a55ac4b7c7";//公钥
        //        string client_secret = "7f243b9d28e04ea6bdc0cfd970b51ea8";//大厅加密秘钥
        //        string source = "BZFGLXXXT";//系统唯一标识

        //        string token = SM2Utils.generate(client_secret + source);

        //        AppraiseMDL _AppraiseMDL = new AppraiseMDL();
        //        //projectServiceMDL _projectService = ViewState["projectServiceMDL"] as projectServiceMDL;
        //        projectServiceParams _projectServiceParams = new projectServiceParams();

        //        #region //固定发送内容

        //        _AppraiseMDL.appMark = "BZFGLXXXT";
        //        _AppraiseMDL.time = DateTime.Now.ToString("yyyyMMddHHmmss");

        //        /// 受理部门编码        
        //        _projectService.orgcode = "11110000000021135M";

        //        /// 受理部门名称
        //        _projectService.orgName = "北京市住房和城乡建设委员会";

        //        /// 代理人姓名
        //        _projectService.handleUserName = "";

        //        /// 代理人证件类型（见 A.2证件类型）
        //        _projectService.handleUserPageType = "";

        //        /// 代理人证件号码
        //        _projectService.handleUserPageCode = "";

        //        /// 服务名称 咨询、申报、补正、缴费、签收（isService 值为1 时必填）
        //        _projectService.serviceName = "申报";

        //        /// 是否为好差评服务办件1-是，0-否（当产生咨询、申报、补正、缴费及办结等服务时，该字段值为 1）
        //        _projectService.isService = "1";

        //        /// 服务时间 yyyy-MM-dd HH:mm:ss （isService 值为1 时必填）
        //        _projectService.serviceTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //        /// 服务日期 yyyy-MM-dd（isService 值为1 时必填）
        //        _projectService.serviceDate = DateTime.Now.ToString("yyyy-MM-dd");

        //        /// 数据来源（默认值为 111）
        //        _projectService.dataSource = "111";

        //        /// 办件类型： 即办件=1，承诺办件=2
        //        _projectService.projectType = 2;

        //        /// 申请人类型 自然人=1，企业法人=2，事业法人=3，社会组织法人=4，非法人企业=5，行政机关=6，其他组织=9
        //        _projectService.userProp = "1";

        //        /// 办件详情查看地址（需要是对接了统一认证平台的单点登录地址）
        //        _projectService.statusUrl = "";

        //        /// 行政区划（见 A.3 北京市行政区划代码）
        //        _projectService.administrative_div = "00";

        //        /// 服务数据初始来源标识，默认与 appMark 值相同
        //        _projectService.source = _AppraiseMDL.appMark;

        //        /// 服务数据来源，默认与appMark 值相同
        //        _projectService.sourceTJ = _AppraiseMDL.appMark;

        //        /// 位行政区划（非太极不用填写，太极必填）
        //        _projectService.areaCode = "";

        //        /// 事项主题
        //        _projectService.subMatter = "";

        //        _projectService.taskType = "01";

        //        #endregion //固定发送内容
                
        //        _projectServiceParams.projectService = _projectService;
        //        string _params = string.Format(@"[{0}]", Utility.JSONHelp.Encode(_projectServiceParams));
        //        _AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", SM2Utils.Encrypt(pubk, _params));
        //        _AppraiseMDL.sign = SM2Utils.Encrypt(pubk, string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));
        //        string rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("appMark={0}&sign={1}&params={2}&time={3}", _AppraiseMDL.appMark, _AppraiseMDL.sign, _AppraiseMDL.Params, _AppraiseMDL.time));

        //        AppraiseResultMDL _AppraiseResultMDL = Newtonsoft.Json.JsonConvert.DeserializeObject<AppraiseResultMDL>(rtn);

        //        if (_AppraiseResultMDL.success == true)
        //        {
        //            ViewState["serviceCode"] = _AppraiseResultMDL.serviceCode;
        //            ViewState["token"] = token;
        //            FileLog.WriteLog(string.Format("评价申报业务【{0}】,编号：{1}", _projectService.taskName, _projectService.projectNo));
        //            //string toUrl = string.Format("http://banshi.beijing.gov.cn/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", _AppraiseResultMDL.serviceCode, "", source, token);
        //            //Response.Redirect(toUrl, true);
        //        }
        //        else
        //        {
        //            FileLog.WriteLog(string.Format("评价申报业务服务发生错误：{0}", _AppraiseResultMDL.errorMessage));
        //        }

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "评价申报业务服务发生错误", ex);
        //    }
        //}

        protected void ButtonPingJia_Click(object sender, EventArgs e)
        {
            if (RootUrl.ToLower().Contains("http://120.52.185.14") == false)
            {
                Response.Write("测试环境，不发起服务评价，请关闭此页面。");
                return;
            }
            if (ViewState["serviceCode"] != null)
            {
                string toUrl = string.Format("http://banshi.beijing.gov.cn/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", ViewState["serviceCode"], "", "BZFGLXXXT", ViewState["token"]);
                Response.Redirect(toUrl, true);
            }


            //string pubk = "040160054ff31ee703c1b678ad38bab57b857e52af3bf1c33e07a5aef203639c0c383a635cc2f2f87573faabd8cecc7f355c8af8ada8ad90df816472a55ac4b7c7";//公钥
            //string client_secret = "7f243b9d28e04ea6bdc0cfd970b51ea8";//大厅加密秘钥
            //string source = "BZFGLXXXT";//系统唯一标识

            //if (RootUrl.ToLower().Contains("http://120.52.185.14") == false)
            //{
            //    Response.Write("测试环境，不发起服务评价，请关闭此页面。");
            //    return;
            //}

            //try
            //{
            //    string token = SM2Utils.generate(client_secret + source);


            //    //测试地址
            //    //string apiUrl = "http://43.254.24.244/pubservice/appraise4OT/reShareApprInfo?";

            //    //正式地址
            //    string apiUrl = "http://banshi.beijing.gov.cn/pubservice/appraise4OT/reShareApprInfo?";

            //    AppraiseMDL _AppraiseMDL = new AppraiseMDL();
            //    projectServiceMDL _projectService = ViewState["projectServiceMDL"] as projectServiceMDL;
            //    projectServiceParams _projectServiceParams = new projectServiceParams();

            //    #region //固定发送内容

            //    _AppraiseMDL.appMark = "BZFGLXXXT";
            //    _AppraiseMDL.time = DateTime.Now.ToString("yyyyMMddHHmmss");

            //    /// 受理部门编码        
            //    _projectService.orgcode = "11110000000021135M";

            //    /// 受理部门名称
            //    _projectService.orgName = "北京市住房和城乡建设委员会";

            //    /// 代理人姓名
            //    _projectService.handleUserName = "";

            //    /// 代理人证件类型（见 A.2证件类型）
            //    _projectService.handleUserPageType = "";

            //    /// 代理人证件号码
            //    _projectService.handleUserPageCode = "";

            //    /// 服务名称 咨询、申报、补正、缴费、签收（isService 值为1 时必填）
            //    _projectService.serviceName = "申报";

            //    /// 是否为好差评服务办件1-是，0-否（当产生咨询、申报、补正、缴费及办结等服务时，该字段值为 1）
            //    _projectService.isService = "1";

            //    /// 服务时间 yyyy-MM-dd HH:mm:ss （isService 值为1 时必填）
            //    _projectService.serviceTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //    /// 服务日期 yyyy-MM-dd（isService 值为1 时必填）
            //    _projectService.serviceDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    /// 数据来源（默认值为 111）
            //    _projectService.dataSource = "111";

            //    /// 办件类型： 即办件=1，承诺办件=2
            //    _projectService.projectType = 2;

            //    /// 申请人类型 自然人=1，企业法人=2，事业法人=3，社会组织法人=4，非法人企业=5，行政机关=6，其他组织=9
            //    _projectService.userProp = "1";

            //    /// 办件详情查看地址（需要是对接了统一认证平台的单点登录地址）
            //    _projectService.statusUrl = "";

            //    /// 行政区划（见 A.3 北京市行政区划代码）
            //    _projectService.administrative_div = "00";

            //    /// 服务数据初始来源标识，默认与 appMark 值相同
            //    _projectService.source = _AppraiseMDL.appMark;

            //    /// 服务数据来源，默认与appMark 值相同
            //    _projectService.sourceTJ = _AppraiseMDL.appMark;

            //    /// 位行政区划（非太极不用填写，太极必填）
            //    _projectService.areaCode = "";

            //    /// 事项主题
            //    _projectService.subMatter = "";

            //    _projectService.taskType = "01";

            //    #endregion //固定发送内容


            //    _projectServiceParams.projectService = _projectService;
            //    string _params = string.Format(@"[{0}]", Utility.JSONHelp.Encode(_projectServiceParams));
            //    _AppraiseMDL.Params = string.Format(@"{{""data"":""{0}""}}", SM2Utils.Encrypt(pubk, _params));
            //    _AppraiseMDL.sign = SM2Utils.Encrypt(pubk, string.Format("{0}{1}{2}", _AppraiseMDL.time, _AppraiseMDL.appMark, "666BZFGLXXXT90fZp"));
            //    string rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, string.Format("appMark={0}&sign={1}&params={2}&time={3}", _AppraiseMDL.appMark, _AppraiseMDL.sign, _AppraiseMDL.Params, _AppraiseMDL.time));

            //    AppraiseResultMDL _AppraiseResultMDL = Newtonsoft.Json.JsonConvert.DeserializeObject<AppraiseResultMDL>(rtn);

            //    if (_AppraiseResultMDL.success == true)
            //    {
            //        string toUrl = string.Format("http://banshi.beijing.gov.cn/pubservice/appraise4PCOthers/pre?serviceCode={0}&feedBackId={1}&source={2}&token={3}&userProp=1", _AppraiseResultMDL.serviceCode, "", source, token);
            //        Response.Redirect(toUrl, true);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "评价申报业务服务发生错误", ex);
            //}
        }
    }
}