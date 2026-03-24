using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;

namespace ZYRYJG.StAnManage
{
    public partial class CertFacePhotoCheck : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //统计结果模版
//                string sql = @"select p.postname,c.* from 
//                                (
//                                select POSTTYPEID,count(*) validCount,count(FACEPHOTO) checkCount
//                                ,sum(case when len(FACEPHOTO)>1 then 1 else 0 end) havePhotoCount
//                                FROM DBO.CERTIFICATE
//
//                                where validenddate >getdate() and STATUS <>'注销' and STATUS <>'离京变更'
//                                group by POSTTYPEID
//                                ) c
//                                inner join dbo.postinfo p on c.posttypeid = p.postid";
                string sql = @"select *,'' as BL from TJ_CertFacePhotoCheck";
                DataTable dtBase = null;
                try
                {
                    dtBase = CommonDAL.GetDataTable(sql);
                }
                catch(Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "检索证书一寸照片采集情况失败。", ex);
                    return;
                }
                DataRow drSum = dtBase.NewRow();
                dtBase.Rows.Add(drSum);
                drSum["PostName"] = "总计";         

                //计算 合格率、合计
                System.Collections.Generic.Dictionary<string, int> sum = new Dictionary<string, int>() { { "validCount", 0 }, { "checkCount", 0 }, { "havePhotoCount", 0 }};
                string[] keys = new string[3];
                sum.Keys.CopyTo(keys, 0);
                for (int i = 0; i < dtBase.Rows.Count - 1; i++)
                {
                    foreach (string s in keys)
                    {
                        sum[s] += Convert.ToInt32(dtBase.Rows[i][s]); //合计
                    }
                    dtBase.Rows[i]["BL"] = string.Format("{0}%",(Math.Round(Convert.ToDecimal(dtBase.Rows[i]["havePhotoCount"]) * 100 / Convert.ToDecimal(dtBase.Rows[i]["checkCount"]), 2)));
                }
                foreach (string s in keys)
                {
                    drSum[s] = sum[s];
                }
                drSum["BL"] = string.Format("{0}%",(Math.Round(Convert.ToDecimal(drSum["havePhotoCount"]) * 100 / Convert.ToDecimal(drSum["checkCount"]), 2)));
                RadGrid1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
                RadGrid1.DataSource = dtBase;
                RadGrid1.DataBind();
            }
        }
    }

}