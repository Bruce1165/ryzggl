using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;

public partial class Student_WebClass : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "jxjy/MyTrain.aspx";
        }
    }

    //帮助文件//https://www.gensee.com/docs/#/Web/Gensee_Vod_Player_SDK-1.0?id=pause
    //seek
    //说明：跳转到指定时间点
    //注意：该 api 只在不采用默认控制条的前提下生效
    //参数：
    //{
    //    "timestamp":100
    //}
    //复制代码Error复制成功
    //参数	说明
    //timestamp	指定时间点，单位毫秒。
    //pause
    //说明：暂停播放
    //注意：该 api 只在不采用默认控制条的前提下生效
    //play
    //说明：开始播放 / 暂停后恢复播放
    //注意：该 api 只在不采用默认控制条的前提下生效

    /// <summary>
    /// 未答试题id集合
    /// </summary>
    public List<long> ListQuestion
    {
        get { return ViewState["ListQuestion"] as List<long>; }
        set { ViewState["ListQuestion"] = value; }
    }

    /// <summary>
    /// 考试开始时间
    /// </summary>
    public DateTime ExamStartTime
    {
        get { return Convert.ToDateTime(ViewState["ExamStartTime"]); }
        set { ViewState["ExamStartTime"] = value; }
    }

    /// <summary>
    /// 考试结束时间
    /// </summary>
    public DateTime ExamEndTime
    {
        get { return Convert.ToDateTime(ViewState["ExamStartTime"]).AddMinutes(Convert.ToInt32(QuestionCount * 1.5)); }
    }

    /// <summary>
    /// 试题总条数
    /// </summary>
    public int QuestionCount
    {
        get { return Convert.ToInt32(ViewState["QuestionCount"]); }
        set { ViewState["QuestionCount"] = value; }
    }

    /// <summary>
    /// 当前题号
    /// </summary>
    public int QuestionNo
    {
        get { return Convert.ToInt32(ViewState["QuestionNo"]); }
        set { ViewState["QuestionNo"] = value; }
    }

    /// <summary>
    /// 当前试题标准答案
    /// </summary>
    public string Answer
    {
        get { return Convert.ToString(ViewState["Answer"]); }
        set { ViewState["Answer"] = value; }
    }

    /// <summary>
    /// 答对题数
    /// </summary>
    public int passQuestionCount
    {
        get { return Convert.ToInt32(ViewState["passQuestionCount"]); }
        set { ViewState["passQuestionCount"] = value; }
    }

    /// <summary>
    /// 课程ID
    /// </summary>
    public long SourceID
    {
        get { return Convert.ToInt64(ViewState["SourceID"]); }
    }

    /// <summary>
    /// 所属栏目
    /// </summary>
    public string BarType
    {
        get { return ViewState["BarType"] == null ? "首都建设云课堂" : Convert.ToString(ViewState["BarType"]); }
        set
        {
            ViewState["BarType"] = value;
            LabelBarType.Text = value;
        }
    }

    ///// <summary>
    ///// 内容方向
    ///// </summary>
    //public string Lab
    //{
    //    get { return ViewState["Lab"]==null?"":Convert.ToString(ViewState["Lab"]); }
    //    set { 
    //        ViewState["Lab"] = (value=="全部"?null:value);
    //        switch (value)
    //        {
    //            case "全部":
    //                ButtonLabAll.CssClass = "btlabcur";
    //                ButtonLabAnGuan.CssClass = "btlab";
    //                ButtonLabLaoCheng.CssClass = "btlab";
    //                ButtonLalbZhuanYe.CssClass = "btlab";
    //                ButtonLabGongRen.CssClass = "btlab";
    //                break;
    //            default:
    //                if (ButtonLabAnGuan.Text == value)
    //                    ButtonLabAnGuan.CssClass = "btlabcur";
    //                else
    //                    ButtonLabAnGuan.CssClass = "btlab";

    //                if (ButtonLabLaoCheng.Text == value)
    //                    ButtonLabLaoCheng.CssClass = "btlabcur";
    //                else
    //                    ButtonLabLaoCheng.CssClass = "btlab";

    //                if (ButtonLalbZhuanYe.Text == value)
    //                    ButtonLalbZhuanYe.CssClass = "btlabcur";
    //                else
    //                    ButtonLalbZhuanYe.CssClass = "btlab";

    //                if (ButtonLabGongRen.Text == value)
    //                    ButtonLabGongRen.CssClass = "btlabcur";
    //                else
    //                    ButtonLabGongRen.CssClass = "btlab";
    //                break;
    //        }
    //    }
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        CachejxjyClickCount += 1;
        if (!IsPostBack)
        {
            #region 页面初始加载

            //个人收藏
            WorkerSetMDL o = WorkerSetDAL.GetObject(WorkerCertificateCode);
            if (o != null)
            {
                HiddenFieldSaveSource.Value = o.SaveSource;
            }

            //可接受参数
            //1、视频隶属专业：Request["o"]，解密后按Split('|')拆分后取第一个即为专业。
            //2、课程ID：Request["s"]，解密后=sourceID
            //3、Request["t"]，所属栏目BarType


            ////传入内容方向，自动过滤显示
            //Lab = (string.IsNullOrEmpty(Request["t"]) == true?"全部":Request["t"]);

            //传入所属栏目，自动过滤显示
            BarType = Request["t"];

            ////初始化上架年份
            //for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 3; i--)
            //{
            //    RadComboBoxSourceYear.Items.Add(new Telerik.Web.UI.RadComboBoxItem(i.ToString(), i.ToString()));
            //}
            //RadComboBoxSourceYear.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Format("{0}年之前", DateTime.Now.Year - 2), string.Format("<{0}", DateTime.Now.Year - 2)));
            //RadComboBoxSourceYear.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("近3年", string.Format(">{0}", DateTime.Now.Year - 3)));
            //RadComboBoxSourceYear.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("全部", ""));
            if (string.IsNullOrEmpty(Request["m"]) == false && Request["m"] == "sc")//我的收藏
            {
                //RadComboBoxSourceYear.Items[0].Selected = true;
                LabelBarType.Text = "我的课程";
            }
            else
            {
                //RadComboBoxSourceYear.Items[1].Selected = true;
            }

            #region 根据本人持有证书、判断可查看的专业课程
            //            //根据本人持有证书、判断可查看的专业课程
            //            string sql = @"
            //                select distinct p.[PackageID],case when p.[PostName] is null then p.[PostTypeName] else p.[PostName] end PostTypeName
            //                    from 
            //                    (
            //                    SELECT [POSTTYPENAME],[POSTNAME]
            //                    FROM [dbo].[CERTIFICATE] where WORKERCERTIFICATECODE='{0}' and Status <>'待审批' and Status <>'进京待审批'
            //                    union all  
            //                    SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession]    
            //                    FROM [dbo].[jcsjk_jzs] where [PSN_Level] like '一级%' and PSN_CertificateNO='{0}' 
            //                    union all
            //                        SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession]
            //                        FROM [dbo].[COC_TOW_Person_BaseInfo] 
            //                        where PSN_CertificateNO='{0}' 
            //                    union all
            //                        SELECT '一级造价工程师','' 
            //                        FROM [dbo].[jcsjk_zjs]
            //                        where [SFZH]='{0}' 
            //                    union all
            //                        SELECT '二级造价工程师',[PSN_RegisteProfession]
            //                        FROM [dbo].[zjs_Certificate]
            //                        where PSN_CertificateNO='{0}' 
            //                    union all
            //                        SELECT '监理师',[注册专业1]+ case [注册专业2] when '无' then '' else ','+[注册专业2] end
            //                        FROM [dbo].[jcsjk_jls]
            //                        where [证件号]='{0}' 
            //                    ) c inner join [RYPX].[dbo].[Package] p 
            //                    on ((c.[POSTTYPENAME] = p.[POSTTYPENAME]  and c.[POSTNAME] = p.[POSTNAME]) or (c.[POSTTYPENAME] = p.[POSTTYPENAME]  and p.[POSTNAME] is null)) and p.[Status]='已发布'
            //                ";

            //            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode));

            #endregion 根据本人持有证书、判断可查看的专业课程

            //初始化显示所有专业类型
            string sql = @"select distinct [PackageID],case when [PostName] is null then [PostTypeName] else [PostName] end PostTypeName
                           from  [RYPX].[dbo].[Package]  where [Status]='已发布'";
            DataTable dt = CommonDAL.GetDataTable(sql);

            ViewState["zy"] = dt;//专业培训包集合

            foreach (DataRow r in dt.Rows)
            {
                RadComboBoxPostType.Items.Add(new Telerik.Web.UI.RadComboBoxItem(r["PostTypeName"].ToString(), r["PostTypeName"].ToString()));
            }

            RadComboBoxPostType.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("与我专业匹配的课程", "与我专业匹配的课程"));
            RadComboBoxPostType.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("全部", ""));

            //传入培训包ID，自动过滤显示专业
            if (string.IsNullOrEmpty(Request["o"]) == false)
            {
                string[] q = Utility.Cryptography.Decrypt(Request["o"]).Split('|');


                Telerik.Web.UI.RadComboBoxItem f = RadComboBoxPostType.Items.FindItemByValue(q[0]);
                if (f != null)
                {
                    f.Selected = true;
                }
                else
                {
                    f = RadComboBoxPostType.Items.FindItemByValue(q[1]);
                    if (f != null)
                    {
                        f.Selected = true;
                    }
                }
            }

        

            //查询全部
            SelectFinishStatus("全部");

            if (string.IsNullOrEmpty(Request["s"]) == false)
            {
                ViewState["SourceID"] = Utility.Cryptography.Decrypt(Request["s"]);
                BindSourceDetail();
            }

            #endregion 页面初始加载
        }
        else
        {            
            if (Request["__EVENTTARGET"] == "viewSource")//点击课程进入课程详细，显示课件及测试
            {
                #region 进入课程详细页面

                string ParentSourceID = Request["__EVENTARGUMENT"];
                ViewState["SourceID"] = ParentSourceID;
                BindSourceDetail();                

                #endregion 进入课程详细页面
            }
            else if (Request["__EVENTTARGET"] == "test")//点击课程进入课程详细，显示课件及测试
            {
                #region 进入随堂测试页面

                span_save.InnerHtml = "";

                string ParentSourceID = Request["__EVENTARGUMENT"];

                ViewState["SourceID"] = ParentSourceID;

                //div_test.Visible = true;
                //div_list.Visible = false;
                //div_class.Visible = false;
                //ButtonReturn.Visible = true;

                div_test.Style.Add("display", "block");
                div_list.Style.Add("display", "none");
                div_class.Style.Add("display","none");
                ButtonReturn.Visible = true;
                ButtonSave.Text = "开始考试";
                ButtonSave.CssClass = "bt_large";
                DivQuestion.InnerHtml = "";
                DivQuestionAnswerTip.InnerText = "";
                RadGridQuestOption.DataSource = null;
                RadGridQuestOption.DataBind();

                //判断题
                DataTable dtPD = null;
                if (Cache[string.Format("dtPD{0}", ParentSourceID)] == null)
                {
                    dtPD = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='判断题' and SourceID={0}", ParentSourceID), "");
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("dtPD{0}", ParentSourceID), dtPD.Copy(), 24);
                }
                else
                {
                    dtPD = ((DataTable)Cache[string.Format("dtPD{0}", ParentSourceID)]).Copy();
                }

                //单选题
                DataTable dtDX = null;
                if (Cache[string.Format("dtDX{0}", ParentSourceID)] == null)
                {
                    dtDX = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='单选题' and SourceID={0}", ParentSourceID), "");                    
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("dtDX{0}", ParentSourceID), dtDX.Copy(), 24);
                }
                else
                {
                    dtDX = ((DataTable)Cache[string.Format("dtDX{0}", ParentSourceID)]).Copy();
                }

                //多选题
                DataTable dtDuoX = null;
                if (Cache[string.Format("dtDuoX{0}", ParentSourceID)] == null)
                {
                    dtDuoX = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='多选题' and SourceID={0}", ParentSourceID), "");
                    //Cache[string.Format("dtDuoX{0}", ParentSourceID)] = dtDuoX.Copy();
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("dtDuoX{0}", ParentSourceID), dtDuoX.Copy(), 24);
                }
                else
                {
                    dtDuoX = ((DataTable)Cache[string.Format("dtDuoX{0}", ParentSourceID)]).Copy();
                }

                //准备试题
                GetQuestionsRadom(dtDX, dtDuoX, dtPD);

                DivTip.InnerText = string.Format("试题总数：{0}道，剩余试题数{1}道。考试时长：{2}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()));
                //RefreshQuestion();

                if (QuestionCount == 0)
                {
                    DivTip.InnerText = "没有发现可用的试题。";
                    ButtonSave.Enabled = false;
                }
                else
                {
                    ButtonSave.Enabled = true;
                }

                #endregion 进入随堂测试页面
            }
            if (Request["__EVENTTARGET"] == "save" || Request["__EVENTTARGET"] == "kc_save")//添加收藏
            {
                #region 添加、取消收藏
                string ParentSourceID = Request["__EVENTARGUMENT"];

                WorkerSetMDL o = WorkerSetDAL.GetObject(WorkerCertificateCode);
                if(o==null)
                {
                    o = new WorkerSetMDL();                   
                }
                //添加或取消收藏
                if (string.IsNullOrEmpty(o.SaveSource) == true || o.SaveSource.Contains(string.Format(",{0},", ParentSourceID)) == false)
                {
                    o.SaveSource = string.Format("{0}{1}"
                        , o.SaveSource
                        , string.IsNullOrEmpty(o.SaveSource) == true ? string.Format(",{0},", ParentSourceID) : string.Format("{0},", ParentSourceID)
                        );
                }
                else//取消收藏
                {
                    o.SaveSource = o.SaveSource.Replace(string.Format(",{0},", ParentSourceID),",");
                    if(o.SaveSource ==",")
                    {
                        o.SaveSource = "";
                    }
                }
                try
                {
                    if (string.IsNullOrEmpty(o.WorkerCertificateCode) == true)
                    {
                        o.WorkerCertificateCode = WorkerCertificateCode;
                        WorkerSetDAL.Insert(o);
                    }
                    else
                    {
                        WorkerSetDAL.Update(o);
                    }
                    HiddenFieldSaveSource.Value = o.SaveSource;
                }
                catch (Exception ex)
                {
                    Utility.FileLog.WriteLog(string.Format("收藏/取消收藏失败！SourceID={0}",ParentSourceID), ex);
                    return;
                }

                //课程栏目添加、取消收藏
                Telerik.Web.UI.RadScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "setcss", string.Format("setcss('{0}');", HiddenFieldSaveSource.ClientID), true);

                if(Request["__EVENTTARGET"] == "kc_save")//课程详细页面添加、取消收藏
                {
                    Telerik.Web.UI.RadScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "setButtonSave"
                        , string.Format("setButtonSave('{0}');", HiddenFieldSaveSource.Value.Contains(string.Format(",{0},",SourceID))==true?"取消收藏":"添加收藏")
                        , true);
                }
                #endregion 添加、取消收藏
            }
        }
    }

    /// <summary>
    /// 绑定课程详细信息
    /// </summary>
    protected void BindSourceDetail()
    {
        SourceMDL kc = SourceDAL.GetObject(Convert.ToInt64(SourceID));

        DataTable kj = CommonDAL.GetDataTableDB("DBRYPX", string.Format(@"select s.*,f.[FinishPeriod],f.[StudyStatus] FROM [dbo].[Source] s left join [dbo].[FinishSourceWare] f on f.WorkerCertificateCode='{0}' and s.SourceID =f.SourceID where s.[ParentSourceID]={1} and s.[Status]='启用'", WorkerCertificateCode, SourceID));

        div_list.Style.Add("display", "none");
        div_class.Style.Add("display", "block");
        ButtonReturn.Visible = true;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (kc.BarType == "工匠讲堂")
        {
            sb.AppendFormat(@"<ul><li class=""list-item2"">
                                <div class=""content-box"">                                   
                                    <div class=""bar2 center"" style=""background: url(../img/jz/{3}); background-size: cover; text-align: center;"" content=""{0}"">
                                        <div class=""gjjt_ff22"">{0}</div>
                                        <div style='clear:both;'></div>
                                    </div>
                                    <div class=""ff5"">1、学习视频教程：<span class=""s3"">（友情提示：请按正常速度播放学习，快进播放不计入学时。）</spam></div>
                                    "
                , kc.SourceName
                , kc.Teacher
                , kc.WorkUnit
                , string.IsNullOrEmpty(kc.SourceImg) == false ? kc.SourceImg : string.Format("0{0}.jpg", (kc.SourceID % 9 + 1))
                , kc.Description
               );
        }
        else
        {
            sb.AppendFormat(@"<ul><li class=""list-item2"">
                                <div class=""content-box"">                                   
                                    <div class=""bar2 center"" style=""background: url(../img/jz/{3}); background-size: cover; text-align: center;"" content=""{0}"">
                                        <div class=""ff2"">{0}</div>
                                        <div class=""ff3 center"">主讲人：{1} </div>
                                        <div class=""ff4 center"">{4} </div><div style='clear:both;'></div>
                                       
                                    </div>
                                    <div class=""ff5"">1、学习视频教程：<span class=""s3"">（友情提示：请按正常速度播放学习，快进播放不计入学时。）</spam></div>
                                    "
                 , kc.SourceName
                , kc.Teacher
                , kc.WorkUnit
                , string.IsNullOrEmpty(kc.SourceImg) == false ? kc.SourceImg : string.Format("0{0}.jpg", (kc.SourceID % 9 + 1))
                , kc.Description
                , kc.SourceID
                );

            span_save.InnerHtml = string.Format("<input id=\"ButtonSave\" type=\"button\" value=\"{1}\" onclick=\"javascript:__doPostBack('kc_save', '{0}');\" class=\"bt_large\" />"
                , kc.SourceID
                , HiddenFieldSaveSource.Value.Contains(string.Format(",{0},", kc.SourceID)) == true ? "取消收藏" : "添加收藏"
                );
        }

        bool ifFinishAll = true;
        foreach (DataRow r in kj.Rows)
        {
//            sb.AppendFormat(@"<div class=""d1 center"" >
//                                            <div class=""link margin2"" onclick='OpenSameWindow(""{0}?nickname={1}&uid={2}&k={3}"");'>{4} </div><div class=""jd""><div class=""jdok"" style=""width:{5}%""> {5}%</div></div><div class=""kjlen""> {6}学时</div><div class=""clear""></div>
//                                        </div>"
//                                    , r["SourceWareUrl"]
//                                    , Server.UrlEncode(PersonName)
//                                    , uid()
//                                    , getPlayKey(Convert.ToInt64(r["SourceID"]), WorkerCertificateCode)
//                                    , r["SourceName"]
//                                    , r["FinishPeriod"] != DBNull.Value ? Convert.ToInt32(r["FinishPeriod"]) * 100 / (Convert.ToInt32(r["Period"]) * 60) : 0//学习进度条
//                                    //, (Convert.ToInt32(r["Period"]) / 45.0).ToString("0.0学时")
//                                     ,r["ShowPeriod"]//学时 
//                        );
            sb.AppendFormat(@"<div class=""d1 center"" >
                                            <div class=""link margin2"" onclick='OpenSameWindowFull(""SourceWarePaly.aspx?o={0}&{4}"");'>{1} </div><div class=""jd""><div class=""jdok"" style=""width:{2}%""> {2}%</div></div><div class=""kjlen""> {3}学时</div><div class=""clear""></div>
                                        </div>"
                                    , Utility.Cryptography.Encrypt(r["SourceID"].ToString())
                                    , r["SourceName"]
                                    , r["FinishPeriod"] != DBNull.Value ? Convert.ToInt32(r["FinishPeriod"]) * 100 / (Convert.ToInt32(r["Period"]) * 60) : 0//学习进度条
                                     , r["ShowPeriod"]//学时 
                                     ,DateTime.Now.Ticks
                        );
            if (r["FinishPeriod"] == DBNull.Value || Convert.ToInt32(r["FinishPeriod"]) < (Convert.ToInt32(r["Period"]) * 60))
            {
                ifFinishAll = false;
            }
        }

        if (kc.BarType == "工匠讲堂")
        {
            int QuestionCount = CommonDAL.GetRowCountDB("DBRYPX", "[TrainQuestion]", "", string.Format(" and sourceid={0} and [Flag]='启用'", kc.SourceID));
            if (QuestionCount > 0)//有试题
            {
                sb.Append(string.Format(@"
                            <div class=""ff5"">2、随堂测试达标：</div>
                            <div class=""d1"" >{0}</div>
                            </div></li></ul><div style='clear:both;'></div>"
                , kj.Rows.Count > 0 && kj.Rows[0]["StudyStatus"] != DBNull.Value && Convert.ToInt32(kj.Rows[0]["StudyStatus"]) == 1 ? "已达标"
                : ifFinishAll == false ? "尚未完成视频学习" : string.Format(@"<a class=""link"" onclick='javascript:__doPostBack(""test"", ""{0}"");'>进入测试</a>", kc.SourceID)
                                   )
                     );
            }
            else
            {
                sb.Append("</div></li></ul><div style='clear:both;'></div>");
            }
        }
        else
        {
            sb.Append(string.Format(@"
                            <div class=""ff5"">2、随堂测试达标：</div>
                            <div class=""d1"" >{0}</div>
                            </div></li></ul><div style='clear:both;'></div>"
                , kj.Rows.Count > 0 && kj.Rows[0]["StudyStatus"] != DBNull.Value && Convert.ToInt32(kj.Rows[0]["StudyStatus"]) == 1 ? "已达标"
                : ifFinishAll == false ? "尚未完成视频学习" : string.Format(@"<a class=""link"" onclick='javascript:__doPostBack(""test"", ""{0}"");'>进入测试</a>", kc.SourceID)
                                   )
                     );
        }

        div_class.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 随即准备试题，出题规则： 单选5  多选5  判断10
    /// </summary>
    /// <param name="dtDX">单选题库</param>
    /// <param name="dtDuoX">多选题库</param>
    /// <param name="dtPD">判断题库</param>
    private void GetQuestionsRadom(DataTable dtDX, DataTable dtDuoX, DataTable dtPD)
    {
        List<long> list = new List<long>();
        System.Random myRandom = new Random();
        int index = 0;

        for (int j = 0; j < 5; j++)
        {
            //多选
            if (dtDuoX.Rows.Count == 0) break;
            index = myRandom.Next(0, dtDuoX.Rows.Count - 1);
            list.Add(Convert.ToInt64(dtDuoX.Rows[index]["QuestionID"]));
            dtDuoX.Rows.RemoveAt(index);
            if (dtDuoX.Rows.Count == 0)
            {
                break;
            }
        }
        for (int j = 0; j < 5; j++)
        {
            //单选
            if (dtDX.Rows.Count == 0) break;
            index = myRandom.Next(0, dtDX.Rows.Count - 1);
            list.Insert(0, Convert.ToInt64(dtDX.Rows[index]["QuestionID"]));
            dtDX.Rows.RemoveAt(index);
            if (dtDX.Rows.Count == 0)
            {
                break;
            }
        }
        for (int j = 0; j < 10; j++)
        {
            //判断
            if (dtPD.Rows.Count == 0) break;
            index = myRandom.Next(0, dtPD.Rows.Count - 1);
            list.Insert(0, Convert.ToInt64(dtPD.Rows[index]["QuestionID"]));
            dtPD.Rows.RemoveAt(index);
            if (dtPD.Rows.Count == 0)
            {
                break;
            }
        }


        ListQuestion = list;
        QuestionCount = list.Count;
        QuestionNo = 0;
    }
    
    protected string uid()
    {
        return "1" + PersonID.ToString().PadLeft(9, '0');
    }

    /// <summary>
    /// 视频播放认证
    /// </summary>
    /// <param name="sourceID">课件ID</param>
    /// <param name="zjhm">证件号码</param>
    /// <returns></returns>
    protected string getPlayKey(long sourceID, string zjhm)
    {
        //测试环境使用如下
        return Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2}"
               , DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")//学习时间
               , sourceID//课件ID
               , zjhm//学习人证件
                )));
    }

    protected void ButtonAll_Click(object sender, EventArgs e)
    {
        SelectFinishStatus("全部");
    }
    protected void ButtonNoFisnish_Click(object sender, EventArgs e)
    {
        SelectFinishStatus("未完成");
    }
    protected void ButtonFinish_Click(object sender, EventArgs e)
    {
        SelectFinishStatus("已完成");
    }

    /// <summary>
    /// 过滤显示学习状态课程
    /// </summary>
    /// <param name="FinishStatus">学习状态：全部、未完成、已完成</param>
    private void SelectFinishStatus(string FinishStatus)
    {
        switch (FinishStatus)
        {
            case "全部":
                ButtonAll.CssClass = "btnCur";
                ButtonFinish.CssClass = "btnNo";
                ButtonNoFisnish.CssClass = "btnNo";

                break;
            case "未完成":
                ButtonAll.CssClass = "btnNo";
                ButtonFinish.CssClass = "btnNo";
                ButtonNoFisnish.CssClass = "btnCur";
                break;
            case "已完成":
                ButtonAll.CssClass = "btnNo";
                ButtonFinish.CssClass = "btnCur";
                ButtonNoFisnish.CssClass = "btnNo";
                break;
            default:
                ButtonAll.CssClass = "btnCur";
                ButtonFinish.CssClass = "btnNo";
                ButtonNoFisnish.CssClass = "btnNo";
                break;
        }
        ViewState["SelectFinishStatus"] = FinishStatus;
        BindSource();
    }

    /// <summary>
    /// 绑定我的课程
    /// </summary>
    protected void BindSource()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //展示课程包中详细章节课件信息列表
        string sql = @"
            SELECT s.[SourceID] ,s.[SourceName] ,s.[Teacher],s.[WorkUnit],s.[SourceType],s.[Period],s.[ShowPeriod],s.[SourceWareCount],s.[BarType],s.[SourceImg]
	            ,ks.finishCount,ks.FinishPeriod,ks.StudyStatus
              FROM  [dbo].[Source] s 
              left join (
                  select  k.ParentSourceID,sum(isnull(f.FinishPeriod,0)) as FinishPeriod,sum(case when f.FinishPeriod = f.Period * 60 then 1 else 0 end) as finishCount,max(isnull(f.StudyStatus,0)) as StudyStatus
                  from [dbo].[Source] k 
                  left join [dbo].[FinishSourceWare] f on f.WorkerCertificateCode='{0}' and k.SourceID =f.SourceID
                  group by k.ParentSourceID
               ) ks on  s.SourceID =ks.ParentSourceID 
              where s.ParentSourceID =0 {1} 
              order by s.SortID desc";

        var q = new QueryParamOB();

        if (string.IsNullOrEmpty(Request["m"]) == false && Request["m"] == "sc")//我的收藏
        {
            if (HiddenFieldSaveSource.Value == "")
                q.Add("1=2");
            else
                q.Add(string.Format("s.[SourceID] in({0})", HiddenFieldSaveSource.Value.Trim(',')));
        }
        else
        {
            if (BarType != ""&&  string.IsNullOrEmpty(Request["o"]) == true)//所属栏目
            {
                q.Add(string.Format("s.[BarType] ='{0}'", BarType));
            }
        }

        //完成情况
        switch (ViewState["SelectFinishStatus"] == null ? "全部" : ViewState["SelectFinishStatus"].ToString())
        {
            case "全部":
                break;
            case "未完成":
                q.Add("(s.[Period] * 60 >ks.FinishPeriod or ks.FinishPeriod is null or ks.StudyStatus=0)");
                break;
            case "已完成":
                q.Add("s.[Period] * 60 =ks.FinishPeriod and ks.StudyStatus =1");
                break;
            default:
                break;
        }
        

        //if (Lab !="")//内容方向
        //{
        //    q.Add(string.Format("s.[Lab] ='{0}'", Lab));
        //}

       

        ////课程上架时间
        //if (RadComboBoxSourceYear.SelectedValue == "")
        //{
        //}
        //else if (RadComboBoxSourceYear.SelectedValue.Contains(">") || RadComboBoxSourceYear.SelectedValue.Contains("<"))
        //{
        //    q.Add(string.Format("s.[SourceYear] {0}", RadComboBoxSourceYear.SelectedValue));
        //}
        //else
        //{
        //    q.Add(string.Format("s.[SourceYear]={0}", RadComboBoxSourceYear.SelectedValue));
        //}

        //专业            
        if (RadComboBoxPostType.SelectedValue != "")
        {
            #region 专业
            if (RadComboBoxPostType.SelectedValue == "与我专业匹配的课程")
            {
                string mysql = @"
                            select distinct p.[PackageID]
                                from 
                                (
                                SELECT [POSTTYPENAME],[POSTNAME]
                                FROM [dbo].[CERTIFICATE] where WORKERCERTIFICATECODE='{0}' and Status <>'待审批' and Status <>'进京待审批'
                                union all  
                                SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession]    
                                FROM [dbo].[jcsjk_jzs] where [PSN_Level] like '一级%' and PSN_CertificateNO='{0}' 
                                union all
                                    SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession]
                                    FROM [dbo].[COC_TOW_Person_BaseInfo] 
                                    where PSN_CertificateNO='{0}' 
                                union all
                                    SELECT '一级造价工程师','' 
                                    FROM [dbo].[jcsjk_zjs]
                                    where [SFZH]='{0}' 
                                union all
                                    SELECT '二级造价工程师',[PSN_RegisteProfession]
                                    FROM [dbo].[zjs_Certificate]
                                    where PSN_CertificateNO='{0}' 
                                union all
                                    SELECT '监理师',[注册专业1]+ case [注册专业2] when '无' then '' else ','+[注册专业2] end
                                    FROM [dbo].[jcsjk_jls]
                                    where [证件号]='{0}' 
                                ) c inner join [RYPX].[dbo].[Package] p 
                                on ((c.[POSTTYPENAME] = p.[POSTTYPENAME]  and c.[POSTNAME] = p.[POSTNAME]) or (c.[POSTTYPENAME] = p.[POSTTYPENAME]  and p.[POSTNAME] is null)) and p.[Status]='已发布'
                            ";

                DataTable dtMyPackageIDList = CommonDAL.GetDataTable(string.Format(mysql, WorkerCertificateCode));


                foreach (DataRow r in dtMyPackageIDList.Rows)
                {
                    sb.AppendFormat(",{0}", r["PackageID"]);
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    q.Add(string.Format(@"s.[SourceID] in(select SourceID from [PackageSource] where [PackageID] in({0})) ", sb));
                }
                else
                {
                    q.Add("1=2");
                }
            }
            else
            {
                q.Add(string.Format("s.[SourceID] in(select SourceID from [PackageSource] where [PackageID] in(select [PackageID] from [Package] where ([PostTypeName] like '{0}' or[PostName] like '{0}')))  ", RadComboBoxPostType.SelectedValue));
            }

            #endregion 专业
        }




        DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format(sql, WorkerCertificateCode, q.ToWhereString()));

        sb = new System.Text.StringBuilder();
        sb.Append("<ul>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["BarType"] != DBNull.Value && dt.Rows[i]["BarType"].ToString() == "工匠讲堂")
            {
                //工匠讲堂，课程不显示讲师、单位信息
                sb.AppendFormat(@"<li class=""list-item"">
                                    <div class=""content-box"">
                                        <a onclick='javascript:__doPostBack(""viewSource"", ""{0}"");'>
                                            <div class=""bar center"" style=""background: url(../img/jz/{7}); background-size: cover; text-align: center;"" content=""{4}"" >
                                                <div class=""gjjt_f2"">{4}</div>                                          
                                            </div>
                                            <div class=""s2"">{4}</div>    
<div class=""s3""><span class=""jsname""></span></div>
                                        </a>                               
                                        <div class=""k1"" >
                                            <div class=""fl"">{10}</div>
                                            <div class=""jd""><div class=""jdok"" style=""width:{8}%""> {8}%</div></div><div class=""kjlen""> {9}学时</div>
                                            <a id=""{0}"" name=""{0}"" class=""save"" onclick='javascript:__doPostBack(""save"", ""{0}"");' >添加收藏</a>
                                            <div class=""clear""></div>
                                        </div>
                                    </div>                          
                                </li>"
                                , dt.Rows[i]["SourceID"]
                                , Server.UrlEncode(PersonName)
                                , ""
                                , ""
                                , dt.Rows[i]["SourceName"]
                                 , dt.Rows[i]["Teacher"]
                                , dt.Rows[i]["WorkUnit"]
                                , (dt.Rows[i]["SourceImg"] != DBNull.Value && dt.Rows[i]["SourceImg"].ToString() != "") ? dt.Rows[i]["SourceImg"] : string.Format("0{0}.jpg", (i % 9 + 1))
                                , dt.Rows[i]["FinishPeriod"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["FinishPeriod"]) * 100 / (Convert.ToInt32(dt.Rows[i]["Period"]) * 60) : 0//学习进度条
                                , dt.Rows[i]["ShowPeriod"]//学时                               
                                ,""//, dt.Rows[i]["SourceType"]//选修、必修
                );
            }
            else
            {
                sb.AppendFormat(@"<li class=""list-item"">
                                    <div class=""content-box"">
                                        <a class=""normal"" onclick='javascript:__doPostBack(""viewSource"", ""{0}"");'>
                                            <div class=""bar center"" style=""background: url(../img/jz/{7}); background-size: cover; text-align: center;"" content=""{4}"">
                                                <div class=""f2"">{4}</div>
                                                <div class=""f3 center"">主讲人：{5} </div>
                                            </div>
                                            <div class=""s2"">{4}</div>
                                            <div class=""s3 jsimg""><span class=""jsname"">{5}</span> {6}</div>
                                         </a>
                                         <div class=""k1"" >
                                            <div class=""fl"">{10}</div>
                                            <div class=""jd""><div class=""jdok"" style=""width:{8}%""> {8}%</div></div><div class=""kjlen""> {9}学时</div>
                                            <a id=""{0}"" name=""{0}"" class=""save"" onclick='javascript:__doPostBack(""save"", ""{0}"");' >添加收藏</a>
                                            <div class=""clear""></div>
                                         </div>
                                    </div>
                                </li>"
                                , dt.Rows[i]["SourceID"]
                                , Server.UrlEncode(PersonName)
                                , ""
                                , ""
                                , dt.Rows[i]["SourceName"]
                                 , dt.Rows[i]["Teacher"]
                                , dt.Rows[i]["WorkUnit"]
                                , (dt.Rows[i]["SourceImg"] != DBNull.Value && dt.Rows[i]["SourceImg"].ToString() != "") ? dt.Rows[i]["SourceImg"] : string.Format("0{0}.jpg", (i % 9 + 1))
                                , dt.Rows[i]["FinishPeriod"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["FinishPeriod"]) * 100 / (Convert.ToInt32(dt.Rows[i]["Period"]) * 60) : 0//学习进度条
                                , dt.Rows[i]["ShowPeriod"]//学时    
                                ,""//, dt.Rows[i]["SourceType"]
                );
            }
        }

        sb.Append("</ul><div style='clear:both;'></div>");
        divClass.InnerHtml = sb.ToString();

        //WorkerSetMDL o = WorkerSetDAL.GetObject(WorkerCertificateCode);
        //if(o!=null)
        //{
        //    HiddenFieldSaveSource.Value = o.SaveSource;
        //    Telerik.Web.UI.RadScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "setcss", string.Format("setcss('{0}');", HiddenFieldSaveSource.ClientID), true);
        //}

        Telerik.Web.UI.RadScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "setcss", string.Format("setcss('{0}');", HiddenFieldSaveSource.ClientID), true);

    }

    protected void RadComboBoxSourceYear_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindSource();
    }
    protected void RadComboBoxPostType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindSource();
    }

    private void RefreshQuestion()
    {
        if (ButtonSave.Text != "开始考试" && ExamEndTime < DateTime.Now)
        {
            if (CompareAnswer() == true)
            {
                passQuestionCount += 1;
            }
            DivTip.InnerText = string.Format("试题总数：{0}道，剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"));
            DivQuestion.InnerHtml = string.Format("<p>{0}您答对{1}道试题，考试结果：{2}。</p>", "考试结束。", passQuestionCount.ToString()
                , (passQuestionCount > 0 && passQuestionCount >= (QuestionCount * 0.9)) ? "合格，您可以退出考试页面了。" : "不合格，请及时复习相关知识，重新发起测试。");
            RadGridQuestOption.DataSource = null;
            RadGridQuestOption.DataBind();
            ButtonSave.Text = "考试结束";
            UpdateExamResult();
            ButtonSave.Enabled = false;
            if (passQuestionCount >= (QuestionCount * 0.9))//考试合格,刷新
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true; ", true);
            }
            return;
        }
        if (ButtonSave.Text != "开始考试" && RadGridQuestOption.Items.Count > 0 && UIHelp.IsSelected(RadGridQuestOption) == false)
        {
            UIHelp.layerAlert(Page, "请选择一个答案！");
            return;
        }
        if (ListQuestion.Count == 0)
        {
            if (CompareAnswer() == true)
            {
                passQuestionCount += 1;
            }
            DivTip.InnerText = string.Format("试题总数：{0}道，剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"));
            DivQuestion.InnerHtml = string.Format("<p>{0}您答对{1}道试题，考试结果：{2}</p>", "考试结束。", passQuestionCount.ToString()
                , (passQuestionCount > 0 && passQuestionCount >= (QuestionCount * 0.9)) ? "合格，您可以退出测试页面了。" : "不合格，请及时复习相关知识，重新发起测试。");
            RadGridQuestOption.DataSource = null;
            RadGridQuestOption.DataBind();
            ButtonSave.Text = "考试结束";
            UpdateExamResult();
            ButtonSave.Enabled = false;
            ButtonSave.CssClass = "bt_large  btn_no";

            if (passQuestionCount >= (QuestionCount * 0.9))//考试合格,刷新
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true; ", true);
            }
            return;
        }
        else
        {
            if (ButtonSave.Text == "开始考试")
            {
                passQuestionCount = 0;
                QuestionNo = 0;
                ExamStartTime = DateTime.Now;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", string.Format(" window.setInterval(function () {{ ShowCountDown({0},{1},{2},{3},{4},{5}, 'showData'); }}, interval); ", ExamEndTime.Year.ToString(), Convert.ToString(ExamEndTime.Month - 1), ExamEndTime.Day.ToString(), ExamEndTime.ToString("HH"), ExamEndTime.ToString("mm"), ExamEndTime.ToString("ss")), true);
                ButtonSave.Text = "确 定";
            }
            else if (ButtonSave.Text == "继 续")
            {
                DivQuestionAnswerTip.InnerText = "";
                ButtonSave.Text = "确 定";
            }
            else
            {
                if (CompareAnswer() == true)
                {
                    passQuestionCount += 1;
                }
                else
                {
                    DivQuestionAnswerTip.InnerText = string.Format("您的回答错误，正确答案应为：{0}", Answer);
                    //DivQuestionAnswerTip.InnerText = "回答错误";
                    ButtonSave.Text = "继 续";
                    RadGridQuestOption.Enabled = false;
                    return;
                }
            }
            RadGridQuestOption.Enabled = true;
            QuestionNo += 1;

            DivTip.InnerText = string.Format("试题总数：{0}道，剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。"
               , QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"));

        }
        TrainQuestionMDL _questionOB = TrainQuestionDAL.GetObject(ListQuestion[0]);
        Answer = _questionOB.Answer;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(string.Format("<p>{2}</p><p>{0}、{1}</p>", QuestionNo.ToString(), _questionOB.Title, _questionOB.QuestionType));
        sb.Append(string.Format("<p>{2}<br/><b>{0}、{1}</b></p>", QuestionNo.ToString(), _questionOB.Title, _questionOB.QuestionType));

        DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format("select * from dbo.TrainQuestOption where QuestionID={0} order by OptionNo", _questionOB.QuestionID));

        DivQuestion.InnerHtml = sb.ToString();
        RadGridQuestOption.DataSource = dt;
        RadGridQuestOption.DataBind();

        ListQuestion.RemoveAt(0);

    }

    //更新考试结果
    private void UpdateExamResult()
    {
        if (passQuestionCount > 1 && passQuestionCount >= (QuestionCount * 0.9))
        {
            FinishSourceWareDAL.UpdateSourceTestStatus(WorkerCertificateCode, SourceID);
        }
    }

    /// <summary>
    /// 判断当前选择答案是否正确
    /// </summary>
    /// <returns></returns>
    private bool CompareAnswer()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        CheckBox cbox = null;
        for (int i = 0; i <= RadGridQuestOption.Items.Count - 1; i++)
        {
            cbox = (System.Web.UI.WebControls.CheckBox)RadGridQuestOption.Items[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                sb.Append(RadGridQuestOption.MasterTableView.DataKeyValues[i]["OptionNo"].ToString());
            }
        }
    
        if (sb.Length > 0 && sb.ToString() == Answer)
            return true;
        else
            return false;
    }

    //保存考试结果
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        RefreshQuestion();
    }
   
    //返回
    protected void ButtonReturn_Click(object sender, EventArgs e)
    {
        span_save.InnerHtml = "";
        if (div_test.Style["display"] == "block")
        {
            div_class.Style.Add("display", "block");
            div_test.Style.Add("display", "none");
            div_list.Style.Add("display", "none");
            ButtonReturn.Visible = true;
        }

        if (div_class.Style["display"]== "block")
        {
            div_list.Style.Add("display", "block");
            div_class.Style.Add("display", "none");
            div_test.Style.Add("display", "none");
            ButtonReturn.Visible = false;

            Telerik.Web.UI.RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "setcss", string.Format("setcss('{0}');", HiddenFieldSaveSource.ClientID), true);
        }
    }

    //protected void ButtonLabAll_Click(object sender, EventArgs e)
    //{
    //    if (((Button)sender).Text=="全部")
    //    {
    //        Lab = null;
    //    }
    //    else
    //    {
    //        Lab = ((Button)sender).Text;
    //    }
    //    BindSource();
    //}
}