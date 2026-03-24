using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class ScoreDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ScoreView.aspx";
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            DataTable dt = ExamResultDAL.GetListView(0, 1, string.Format(" and ExamResultID={0}", Request["o"]), "ExamResultID");
            ImageFaceImg.ImageUrl = "~/Images/photo_ry.jpg";
            if (dt != null && dt.Rows.Count > 0)
            {
                LabelWorkerName.Text = dt.Rows[0]["WorkerName"].ToString();//姓名
                LabelPostTypeName.Text = dt.Rows[0]["PostTypeName"].ToString();//岗位类别
                LabelPostName.Text = dt.Rows[0]["PostName"].ToString();//工种
                LabelExamCardID.Text = dt.Rows[0]["ExamCardID"].ToString();//准考证
                LabelCertificateCode.Text = dt.Rows[0]["CertificateCode"].ToString();//证件号码
               
                LabelExamPlaceName.Text = string.Format("{0}（{1}）", dt.Rows[0]["ExamPlaceName"].ToString(), dt.Rows[0]["ExamPlaceAddress"].ToString());//考点名称
                LabelExamRoomCode.Text = dt.Rows[0]["ExamRoomCode"].ToString();//考场号
                
                if (Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).CompareTo(Convert.ToDateTime(dt.Rows[0]["ExamEndDate"])) == 0)//考一天
                    LabelExamDate.Text = string.Format("{0}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"));//考试时间
                else//考多天
                    LabelExamDate.Text = string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[0]["ExamEndDate"]).ToString("yyyy.MM.dd"));//考试时间

                //照片               
                ImageFaceImg.ImageUrl = GetFacePhotoPath(dt.Rows[0]["ExamPlanID"].ToString(), dt.Rows[0]["CertificateCode"].ToString());

                //科目（2或3科）
                DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0}", dt.Rows[0]["ExamPlanID"].ToString()), "ExamPlanSubjectID");
                
                //成绩
                DataTable score = ExamSubjectResultDAL.GetList(0, int.MaxValue - 1, string.Format(" and EXAMCARDID={0}", dt.Rows[0]["ExamCardID"].ToString()), "EXAMSUBJECTRESULTID");
                DataColumn[] key = new DataColumn[1];
                key[0] = score.Columns["PostID"];
                score.PrimaryKey = key;

                DataRow find=null;

                if (sub != null && sub.Rows.Count > 0)
                {
                    for (int i = 0; i < sub.Rows.Count; i++)
                    {
                        Literal Literal1 = new Literal();
                       
                            find = score.Rows.Find(sub.Rows[i]["PostID"].ToString());
                            Literal1.Text = string.Format(
                    @" <tr class=""GridLightBK"">
                            <td nowrap=""nowrap"" align=""right"">
                                <strong>{0}：</strong>
                            </td>
                            <td colspan=""2"">
                                {1}                     
                            </td>
                        </tr>", sub.Rows[i]["PostName"].ToString(), (find == null ? "" : find["SUMSCORE"].ToString()));
                                                                       
                        PlaceHolder1.Controls.Add(Literal1);
                        
                    }
                    //if (sub.Rows.Count % 3 != 0)//考试科目未满三门
                    //{
                    //    for (int i = sub.Rows.Count % 3 +1; i <= 3; i++)
                    //    {

                    //        list.Add(string.Format("km{0}_text", i.ToString()), "");
                    //        list.Add(string.Format("km{0}_value", i.ToString()), "");
                    //    }
                    //}

                    //ViewState["PrintData"] = list;
                }

            }
            base.Page_Init(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
            }
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/photo_ry.jpg";
            }
        }
    }
}
