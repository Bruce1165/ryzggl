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
    public partial class CertifBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }

        protected System.Collections.Generic.Dictionary<string, int> GetData()
        {
            int? PostTypeID = null;
            int? PostID = null;
            if (PostSelect1.PostTypeID != "")
            {
                PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);
            }

            if (PostSelect1.PostID != "")
            {
                PostID = Convert.ToInt32(PostSelect1.PostID);
            }
           
            DateTime? StartTime = txtStartDate.SelectedDate;
            DateTime? EndTime = txtEndtDate.SelectedDate;


            //统计结果模版
            DataTable dtBase = CertificateDAL.AnalysisCertificateManageBase(PostTypeID, PostID);
            DataRow drSum = dtBase.NewRow();
            dtBase.Rows.Add(drSum);
            drSum["PostName"] = "总计";
            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dtBase.Columns["PostName"];
            dtBase.PrimaryKey = dcKeys;

            //填充统计数据
            DataTable dtData = CertificateDAL.AnalysisCertificateManageData(StartTime, EndTime, PostTypeID, PostID);
            DataRow drFind = null;
            foreach (DataRow dr in dtData.Rows)
            {
                drFind = dtBase.Rows.Find(dr["PostName"]);
                if (drFind != null && dtBase.Columns.IndexOf(dr["CHANGETYPE"].ToString()) != -1) drFind[dr["CHANGETYPE"].ToString()] = dr["count"];
            }

            //计算小计、合计
            System.Collections.Generic.Dictionary<string, int> sum = new Dictionary<string, int>() { { "首次", 0 }, { "续期", 0 }, { "京内变更", 0 }, { "离京变更", 0 }, { "进京变更", 0 }, { "注销", 0 }, { "补办", 0 } };


            string[] keys = new string[7];
            sum.Keys.CopyTo(keys, 0);
            //int rowSum = 0;
            for (int i = 0; i < dtBase.Rows.Count - 1; i++)
            {
                ////小计
                //rowSum = 0;
                //foreach (string s in keys)
                //{
                //    rowSum += Convert.ToInt32(dtBase.Rows[i][s]);
                //}
                //dtBase.Rows[i]["小计"] = rowSum;

                //合计
                foreach (string s in keys)
                {
                    sum[s] += Convert.ToInt32(dtBase.Rows[i][s]);
                }
            }
            foreach (string s in keys)
            {
                drSum[s] = sum[s];
            }

            return sum;
        }



        protected void RadChart_FDC_ItemDataBound(object sender, Telerik.Charting.ChartItemDataBoundEventArgs e)
        {
            e.SeriesItem.Name = ((DataRowView)e.DataItem)[0].ToString();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RadChart_FDC.DataSource = GetData();
            RadChart_FDC.DataBind();
        }
    }
}
