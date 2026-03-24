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
    public partial class ExamPhotoBatch : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamPhotoList.aspx";
            }
        }

        protected List<string> ListId
        {
            get
            {
                return ViewState["ListExamRoomAllotID"] as List<string>;
            }
            set
            {
                ViewState["ListExamRoomAllotID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ListExamRoomAllotID"] != null)
                {
                    ViewState["ListExamRoomAllotID"] = Session["ListExamRoomAllotID"];
                    Session.Remove("ListExamRoomAllotID");
                }

                bindNextData();             
            }
        }

        private void bindNextData()
        {
            string ExamRoomAllotID = ListId[0].ToString();
            //string filterWhereString = " and  ExamRoomAllotID=" + ExamRoomAllotID;

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", string.Format(" and  ExamRoomAllotID={0}",ExamRoomAllotID));
            RadListViewPhoto.CurrentPageIndex = 0;
            RadListViewPhoto.DataSourceID = ObjectDataSource1.ID;

            ExamRoomAllotOB exroom = ExamRoomAllotDAL.GetObject(Convert.ToInt64(ExamRoomAllotID));
            RadTextBoxExamPERSONNUMBER.Text = exroom.PersonNumber.ToString();  //考场人数   
            lblExamRoomCode.Text = exroom.ExamRoomCode;    //考场号
            ViewState["ExamRoomCode"] = exroom.ExamRoomCode;

            string examplaceallotid = exroom.ExamPlaceAllotID.ToString();
            ExamPlaceAllotOB explace = ExamPlaceAllotDAL.GetObject(Convert.ToInt64(examplaceallotid));
            RadTextBoxExamPlaceName.Text = explace.ExamPlaceName;    //考场名称

            string examplanid = exroom.ExamPlanID.ToString();  //考试计划id
            ExamPlanOB explan = ExamPlanDAL.GetObject(Convert.ToInt64(examplanid));
            //if (explan.ExamStartDate.Value.CompareTo(explan.ExamEndDate) == 0)
            //{
            //    RadTextBoxExamDate.Text = Convert.ToDateTime(explan.ExamStartDate).ToString("yyyy.MM.dd");//考试时间
            //}
            //else
            //{
            //    RadTextBoxExamDate.Text = Convert.ToDateTime(explan.ExamStartDate).ToString("yyyy.MM.dd") + "-" + Convert.ToDateTime(explan.ExamEndDate).ToString("yyyy.MM.dd");//考试时间
            //}
            if (explan.ExamStartDate.Value.ToString() == exroom.ExamStartTime.ToString())
            {
                RadTextBoxExamDate.Text =explan.ExamStartDate.Value.ToString("yyyy年MM月dd日");//考试时间
            }
            else
            {
                RadTextBoxExamDate.Text = string.Format("{0}-{1}", exroom.ExamStartTime.Value.ToString("yyyy年MM月dd日 HH:mm"), exroom.ExamEndTime.Value.ToString("HH:mm"));//考试时间
            }

            LabelExamPlanName.Text = string.Format("{0}【岗位工种：{1}】", explan.ExamPlanName, UIHelp.FormatPostNameByExamplanName(explan.PostID.Value, explan.PostName, explan.ExamPlanName));
            ViewState["ExamPlanID"] = examplanid;//考试计划ID

            PrintCount.InnerText = ListId.Count.ToString();
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/tup.gif";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/tup.gif";
            }
        }


        //打印
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            UIHelp.WriteOperateLog(PersonName, UserID, "打印考场照片阵列", string.Format("考试名称：{0}；考点：{1}；考场号：{2}。"
                , LabelExamPlanName.Text, RadTextBoxExamPlaceName.Text, lblExamRoomCode.Text));
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/考场照片阵列.doc"
                , string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.doc", ViewState["ExamPlanID"].ToString(), ViewState["ExamRoomCode"].ToString())
                , GetPrintData());
            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.doc');", ViewState["ExamPlanID"].ToString(), ViewState["ExamRoomCode"].ToString(), RootUrl), true);
            
            ListId.RemoveAt(0);

            if (ListId.Count == 0)
            {
                ButtonPrint.Enabled = false;
                ButtonPrint2.Enabled = false;
                PrintCount.InnerText = ListId.Count.ToString();
            }
            else
            {
                bindNextData();
            }

        }

      

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //照片阵列存放路径(~/UpLoad/ExamerPhotoList/考试计划ID/照片阵列_考场编号.doc)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/" + ViewState["ExamPlanID"].ToString()))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/" + ViewState["ExamPlanID"].ToString()));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            DataTable dt = ExamResultDAL.GetListView(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "ExamCardID");
            Dictionary<string, string> printData = null;
            int pageNo = 0;//第几页
            int cellNo = 0;//当前页第几单元格
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cellNo = i % RadListViewPhoto.PageSize + 1;
                if (i % RadListViewPhoto.PageSize == 0)
                {
                    printData = new Dictionary<string, string>();
                    list.Add(printData);
                    pageNo++;
                    
                    printData.Add("PersonNumber", RadTextBoxExamPERSONNUMBER.Text);//考场人数  
                    printData.Add("ExamRoomCode", lblExamRoomCode.Text);//考场号
                    printData.Add("ExamPlaceName", RadTextBoxExamPlaceName.Text);//考场名称
                    printData.Add("ExamDate", RadTextBoxExamDate.Text);//考试时间
                    printData.Add("ExamPlanName", LabelExamPlanName.Text);//考试名称
                    //printData.Add("PageNo", pageNo.ToString());//页码
                    //printData.Add("PageCount", RadListViewPhoto.PageCount.ToString());//页数
                    //printData.Add("ExamPlanID", ViewState["ExamPlanID"].ToString());//考试计划                    
                }
             
                printData.Add("CertificateCode_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//证件号
                printData.Add("WorkerName_" + cellNo.ToString(), dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("ExamCardID_" + cellNo.ToString(), dt.Rows[i]["ExamCardID"].ToString());//准考证
                //照片
                string path = GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString());
                if (File.Exists(Server.MapPath(path)) == false) path = "~/Images/photo_ry.jpg";
                printData.Add("FacePhoto_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//照片名称
                printData.Add("Img_FacePhoto_" + cellNo.ToString(),path);
               
            }
            if (dt.Rows.Count  % 25 != 0)//最后一页有空行
            {
                string[] labels = "CertificateCode_,WorkerName_,ExamCardID_,FacePhoto_,Img_FacePhoto_".Split(',');//替换标签名称

                for (int i = (dt.Rows.Count % 25) + 1; i <= 25; i++)
                {
                    for (int j = 0; j < labels.Length; j++)
                    {
                        if (labels[j].IndexOf("Img_") != -1)//照片
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
                        else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
                        else
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
                    }
                }
            }
            return list;
        }

    }
}
