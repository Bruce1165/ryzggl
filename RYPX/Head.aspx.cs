using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;

public partial class Head : BasePage
{
    protected override bool IsNeedLogin
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 网站访问量
    /// </summary>
    protected long visitorCount
    {
        get
        {
            if (Cache["jxjyVisitCount"] == null)
            {
                TJ_VisitMDL tj = TJ_VisitDAL.GetObject(DateTime.Now.Year);
                if (tj == null)
                {
                    TJ_VisitMDL tjold = TJ_VisitDAL.GetObject(DateTime.Now.Year - 1);
                    tj = new TJ_VisitMDL();
                    tj.TJ_Year = DateTime.Now.Year;
                    tj.TJ_Count = (tjold == null ? 1 : tjold.TJ_Count);
                    TJ_VisitDAL.Insert(tj);
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "jxjyVisitCount", tj.TJ_Count, 2);
                }

                return tj.TJ_Count.Value;
            }
            else
            {
                return Convert.ToInt64(Cache["jxjyVisitCount"]);
            }
        }
        set {
            TJ_VisitMDL tj = TJ_VisitDAL.GetObject(DateTime.Now.Year);
            if (tj == null)
            {
                TJ_VisitMDL tjold = TJ_VisitDAL.GetObject(DateTime.Now.Year - 1);
                tj = new TJ_VisitMDL();
                tj.TJ_Year = DateTime.Now.Year;
                tj.TJ_Count = (tjold == null ? 1 : tjold.TJ_Count);
                TJ_VisitDAL.Insert(tj);
               
            }

            if (value > tj.TJ_Count)
            {
                tj.TJ_Count = value;
                TJ_VisitDAL.Update(tj);
            }
            Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "jxjyVisitCount", tj.TJ_Count, 2);
        }
    }

    /// <summary>
    /// 缓存、尚未保存的网站访问量
    /// </summary>
    protected long CachejxjyVisitCount
    {
        get
        {
            if (Cache["CachejxjyVisitCount"] == null)
                return 1;
            else
                return Convert.ToInt64(Cache["CachejxjyVisitCount"]);
        }
        set
        {
            if (value > 20)//存储超过20次更新一次数据库
            {
                visitorCount = visitorCount + value;
                Cache["CachejxjyVisitCount"] = 0;
            }
            else
            {
                Cache["CachejxjyVisitCount"] = value;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.IsAuthenticated)
            {
                CachejxjyVisitCount = CachejxjyVisitCount + 1;
                loginer.InnerHtml = string.Format("欢迎您：<span>{0}</span>，您是第<span> {1} </span>位访问者。", PersonName, visitorCount + CachejxjyVisitCount);
            }
        }
    }
}