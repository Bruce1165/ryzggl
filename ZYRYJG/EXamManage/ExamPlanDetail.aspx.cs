using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace ZYRYJG.EXamManage
{
    public partial class ExamPlanDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamSignList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showDetail();
            }
        }
        protected void showDetail()
        {

            string examplanid = Utility.Cryptography.Decrypt(Request.QueryString["o"]);
            string fwhere = "ExamplanId=" + examplanid;

            //PostTypeID.Text = dt.Rows[0]["PostTypeID"].ToString();
            ExamPlanOB examplan = ExamPlanDAL.GetObject(Convert.ToInt64(examplanid));
            ExamPlanName.Text = examplan.ExamPlanName;
            ExamStartDate.Text = examplan.ExamStartDate.Value.ToString("yyyy.MM.dd");
            ExamEndDate.Text = examplan.ExamEndDate.Value.ToString("yyyy.MM.dd");
            ExamCardSendStartDate.Text = examplan.ExamCardSendStartDate.Value.ToString("yyyy.MM.dd");
            ExamCardSendEndDate.Text = examplan.ExamCardSendEndDate.Value.ToString("yyyy.MM.dd");
            PostID.Text = examplan.PostName.ToString();
            PostInfoOB postinfo = PostInfoDAL.GetObject(Convert.ToInt32(examplan.PostTypeID));
            PostTypeID.Text = postinfo.PostName.ToString();
            if (examplan.Remark != null)
            {
                Remark.Text = examplan.Remark.ToString();
            }
            SignUpEndDate.Text = examplan.SignUpEndDate.Value.ToString("yyyy.MM.dd");
            SignUpStartDate.Text = examplan.SignUpStartDate.Value.ToString("yyyy.MM.dd");
            if (examplan.SignUpPlace != null)
            {
                SignUpPlace.Text = examplan.SignUpPlace.ToString();
            }

            LatestPersonLimit.Text = string.Format("{0}人", examplan.PersonLimit);

            LatestCheckDate.Text = string.Format("{0}~ {1}"
                , examplan.StartCheckDate.HasValue == true ? examplan.StartCheckDate.Value.ToString("yyyy.MM.dd") : examplan.SignUpEndDate.Value.ToString("yyyy.MM.dd")
                , examplan.LatestCheckDate.Value.ToString("yyyy.MM.dd"));

        }

        //protected void btnSignUp_Click(object sender, EventArgs e)
        //{
        //    string url=string .Format("ExamSign.aspx?o={0}",Request .QueryString["o"].ToString());
        //    Response .Redirect(url);
        //}

        //protected void btnMoreSignUp_Click(object sender, EventArgs e)
        //{
        //    string url = string.Format("MoreAddSign.aspx?o={0}", Request.QueryString["o"].ToString());
        //    Response.Redirect(url);
        //}
    }
}
