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
    public partial class ExamCard :BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamCardManage.aspx";
            }
        }


        /// <summary>
        /// 是否为实操
        /// </summary>
        protected bool shicao
        {
            get {
                if (ViewState["shicao"] == null)
                    return false;
                else
                    return (bool)ViewState["shicao"]; }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            string _ExamResultID = Utility.Cryptography.Decrypt(Request["o"]);

            DataTable dt = null;
            if (string.IsNullOrEmpty(Request["t"]) == false && Request["t"] == "sc")//实操准考证
            {
                ViewState["shicao"] = true;
                dt = CommonDAL.GetDataTable(string.Format("select * from VIEW_EXAMRESULT_Operation where ExamCardID='{0}'", _ExamResultID));
            }
            else//正常准考证
            {
                dt = CommonDAL.GetDataTable(string.Format("select * from View_ExamResult where ExamResultID={0}", _ExamResultID));
                //dt = ExamResultDAL.GetListView(0, 1, string.Format(" and ExamResultID={0}", _ExamResultID), "ExamResultID");
            }

            ImageFaceImg.ImageUrl = "~/Images/photo_ry.jpg";
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["ExamPlanID"] = dt.Rows[0]["ExamPlanID"];
                ViewState["ExamCardID"] = dt.Rows[0]["ExamCardID"];

                LabelWorkerName.Text = dt.Rows[0]["WorkerName"].ToString();//姓名
                LabelPostTypeName.Text = dt.Rows[0]["PostTypeName"].ToString();//岗位类别

                string PostTypeID = dt.Rows[0]["PostTypeID"].ToString();//岗位类别ID
                string SkillLevel = dt.Rows[0]["SkillLevel"].ToString() == "" ? "" : "(" + dt.Rows[0]["SkillLevel"].ToString() + ")";

                if (PostTypeID == "4")
                {
                    LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + SkillLevel;
                }
                else if (dt.Rows[0]["PostID"].ToString() == "12")
                {
                    if (dt.Rows[0]["EXAMPLANNAME"].ToString().Contains("暖通") == true)
                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + "（暖通）";//工种 "（暖通）"
                    else if (dt.Rows[0]["EXAMPLANNAME"].ToString().Contains("电气") == true)
                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + "（电气）";//工种 "（暖通）"
                    else
                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString();//工种
                }
                else
                {
                    LabelPostName.Text = dt.Rows[0]["PostName"].ToString();//工种
                }
                LabelExamCardID.Text = dt.Rows[0]["ExamCardID"].ToString();//准考证
                LabelCertificateCode.Text = dt.Rows[0]["CertificateCode"].ToString();//证件号码

                LabelExamPlaceName.Text = string.Format("{0}（{1}）", dt.Rows[0]["ExamPlaceName"].ToString(), dt.Rows[0]["ExamPlaceAddress"].ToString());//考点名称
                LabelExamRoomCode.Text = dt.Rows[0]["ExamRoomCode"].ToString();//考场号

                LabelUnitName.Text = dt.Rows[0]["UnitName"].ToString();//工作单位

                if (Request["t"] == "sc")
                {
                    LabelExamDate.Text = string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("yyyy年MM月dd日 HH:mm"), Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm"));//考试时间
                }
                else
                {
                    //if (Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).CompareTo(Convert.ToDateTime(dt.Rows[0]["ExamEndDate"])) == 0)//考一天
                    //    LabelExamDate.Text = string.Format("{0}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"));//考试时间
                    //else//考多天
                    //    LabelExamDate.Text = string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[0]["ExamEndDate"]).ToString("yyyy.MM.dd"));//考试时间
                    LabelExamDate.Text = Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("yyyy年MM月dd日");
                }

                //照片               
                ImageFaceImg.ImageUrl =UIHelp.ShowFile(GetFacePhotoPath(dt.Rows[0]["ExamPlanID"].ToString(), dt.Rows[0]["CertificateCode"].ToString()));


                var list = new System.Collections.Hashtable(); 
                //Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("WorkerName", LabelWorkerName.Text);
                list.Add("PostTypeName", LabelPostTypeName.Text);
                list.Add("PostName", LabelPostName.Text);
                list.Add("ExamCardID", LabelExamCardID.Text);
                list.Add("CertificateCode", LabelCertificateCode.Text);
                list.Add("ExamPlaceName", LabelExamPlaceName.Text);
                list.Add("ExamRoomCode", LabelExamRoomCode.Text);
                list.Add("ExamDate", LabelExamDate.Text);
                //list.Add("FacePhoto", LabelExamCardID.Text);//照片标签
                //list.Add("Img_FacePhoto", ImageFaceImg.ImageUrl);//绑定照片
                list["photo"] = GetFacePhotoPath(dt.Rows[0]["ExamPlanID"].ToString(), dt.Rows[0]["CertificateCode"].ToString());
                list.Add("UnitName", LabelUnitName.Text);//工作单位
                list.Add("Phone", dt.Rows[0]["S_PHONE"].ToString().Substring(dt.Rows[0]["S_PHONE"].ToString().Length - 4));//联系电话
               

                //科目（2或3科）：注意2018-03-27日添加规则：‘测量验线员' ，'试验员'两个岗位补考实操（postID=50，54）
                DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0} and PostStatus=1 and PostInfo.PostID <> 50 and PostInfo.PostID <> 54", dt.Rows[0]["ExamPlanID"].ToString()), (Request["t"] == "sc" ? "PostOrder desc" : "PostOrder"));

                if (sub != null && sub.Rows.Count > 0)
                {

                    for (int i = 0; i < sub.Rows.Count; i++)
                    {
                        Literal Literal1 = new Literal();
                        if ((Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") //实操准考证
                            || (Request["t"] != "sc" == true && sub.Rows[i]["PostName"].ToString() != "实操"))//正常准考证非实操科目
                        {
                            if (sub.Rows[i]["PostName"].ToString() == "实操")
                            {
                                Literal1.Text = string.Format(
                               @" <tr class=""GridLightBK"">
                                    <td nowrap=""nowrap"" align=""right"">
                                        <strong>科目：</strong>
                                    </td>
                                    <td colspan=""2"">
                                        {0}                                
                                    </td>
                                </tr>", sub.Rows[i]["PostName"].ToString()
                                        );

                                //科目考试时间及名称

                                list.Add("km1_value", sub.Rows[i]["PostName"].ToString());
                            }
                            else
                            {
                                Literal1.Text = string.Format(
                         @" <tr class=""GridLightBK"">
                            <td nowrap=""nowrap"" align=""right"">
                                <strong>科目{0}：</strong>
                            </td>
                            <td colspan=""2"">
                                {1} - {2}&nbsp;{3}                                
                            </td>
                        </tr>", (sub.Rows.Count == 1) ? "" : Convert.ToString(i + 1)
                              , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("HH:mm")
                                  , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm")
                                  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")
                                  );

                                //科目考试时间及名称
                                list.Add(string.Format("km{0}_value", (i + 1)),
                                    string.Format("{0} - {1} {2}", Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("HH:mm")
                                    , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm")
                                  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));
                                //if (Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("yyyy-MM-dd") == Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("yyyy-MM-dd"))//考一天
                                //{
                                //    list.Add(string.Format("km{0}_value", sub.Rows[i]["PostName"].ToString() == "实操" ? "1" : Convert.ToString(i + 1)),
                                //    string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm")
                                //    , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm")
                                //  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));

                                //}
                                //else//考多天
                                //{
                                //    list.Add(string.Format("km{0}_value", sub.Rows[i]["PostName"].ToString() == "实操" ? "1" : Convert.ToString(i + 1)),
                                //    string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("yyyy.MM.dd HH:mm")
                                //    , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("yyyy.MM.dd HH:mm")
                                //  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));
                                //}
                            }
                        }
                        else if (sub.Rows[i]["PostName"].ToString() == "实操")
                        {
                            Literal1.Text = string.Format(
                    @" <tr class=""GridLightBK"">
                            <td nowrap=""nowrap"" align=""right"">
                                <strong>科目{0}：</strong>
                            </td>
                            <td colspan=""2"">
                                {1}（理论考试合格的考生方可参加实操考试,考试时间、地点以实操准考证为准。）                               
                            </td>
                        </tr>", Convert.ToString(i + 1), sub.Rows[i]["PostName"].ToString());

                            //科目考试时间及名称
                            list.Add(string.Format("km{0}_value", Convert.ToString(i + 1)), string.Format("{0}（理论考试合格的考生方可参加实操考试,考试时间、地点以实操准考证为准。）", sub.Rows[i]["PostName"].ToString()));
                        }
                        else
                        {
                            list.Add(string.Format("km{0}_text", i + 1), "");
                            list.Add(string.Format("km{0}_value", i + 1), "");
                            continue;
                        }

                        PlaceHolder1.Controls.Add(Literal1);
                        list.Add(string.Format("km{0}_text", (Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") ? "1" : Convert.ToString(i + 1))
                            , string.Format("科目{0}：", (Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") || sub.Rows.Count == 1 ? "" : Convert.ToString(i + 1)));

                    }


                    if (sub.Rows.Count % 3 != 0)//考试科目未满三门
                    {
                        for (int i = sub.Rows.Count % 3 + 1; i <= 3; i++)
                        {

                            list.Add(string.Format("km{0}_text", i.ToString()), "");
                            list.Add(string.Format("km{0}_value", i.ToString()), "");
                        }
                    }

                    //list.Add("ExamDate", LabelExamDate.Text);

                    ViewState["PrintData"] = list;
                }

            }
            base.Page_Init(sender, e);
        }

//        protected override void Page_Init(object sender, EventArgs e)
//        {
//            string _ExamResultID = Utility.Cryptography.Decrypt(Request["o"]);

//            DataTable dt = null;
//            if (string.IsNullOrEmpty(Request["t"]) == false && Request["t"] == "sc")//实操准考证
//            {
//                ViewState["shicao"] = true;
//                dt = CommonDAL.GetDataTable(string.Format("select * from VIEW_EXAMRESULT_Operation where ExamCardID='{0}'", _ExamResultID));
//            }
//            else//正常准考证
//            {
//                dt = CommonDAL.GetDataTable(string.Format("select * from View_ExamResult where ExamResultID={0}", _ExamResultID));
//                //dt = ExamResultDAL.GetListView(0, 1, string.Format(" and ExamResultID={0}", _ExamResultID), "ExamResultID");
//            }

//            ImageFaceImg.ImageUrl = "~/Images/photo_ry.jpg";
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                ViewState["ExamPlanID"] = dt.Rows[0]["ExamPlanID"];
//                ViewState["ExamCardID"] = dt.Rows[0]["ExamCardID"];

//                LabelWorkerName.Text = dt.Rows[0]["WorkerName"].ToString();//姓名
//                LabelPostTypeName.Text = dt.Rows[0]["PostTypeName"].ToString();//岗位类别

//                string PostTypeID = dt.Rows[0]["PostTypeID"].ToString();//岗位类别ID
//                string SkillLevel = dt.Rows[0]["SkillLevel"].ToString() == "" ? "" : "(" + dt.Rows[0]["SkillLevel"].ToString() + ")";

//                if (PostTypeID == "4")
//                {
//                    LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + SkillLevel;
//                }
//                else if (dt.Rows[0]["PostID"].ToString() == "12")
//                {
//                    if (dt.Rows[0]["EXAMPLANNAME"].ToString().Contains("暖通") == true)
//                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + "（暖通）";//工种 "（暖通）"
//                    else if (dt.Rows[0]["EXAMPLANNAME"].ToString().Contains("电气") == true)
//                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString() + "（电气）";//工种 "（暖通）"
//                    else
//                        LabelPostName.Text = dt.Rows[0]["PostName"].ToString();//工种
//                }
//                else
//                {
//                    LabelPostName.Text = dt.Rows[0]["PostName"].ToString();//工种
//                }
//                LabelExamCardID.Text = dt.Rows[0]["ExamCardID"].ToString();//准考证
//                LabelCertificateCode.Text = dt.Rows[0]["CertificateCode"].ToString();//证件号码

//                LabelExamPlaceName.Text = string.Format("{0}（{1}）", dt.Rows[0]["ExamPlaceName"].ToString(), dt.Rows[0]["ExamPlaceAddress"].ToString());//考点名称
//                LabelExamRoomCode.Text = dt.Rows[0]["ExamRoomCode"].ToString();//考场号

//                LabelUnitName.Text = dt.Rows[0]["UnitName"].ToString();//工作单位

//                if (Request["t"] == "sc")
//                {
//                    LabelExamDate.Text = string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("yyyy年MM月dd日 HH:mm"), Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm"));//考试时间
//                }
//                else
//                {
//                    //if (Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).CompareTo(Convert.ToDateTime(dt.Rows[0]["ExamEndDate"])) == 0)//考一天
//                    //    LabelExamDate.Text = string.Format("{0}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"));//考试时间
//                    //else//考多天
//                    //    LabelExamDate.Text = string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[0]["ExamEndDate"]).ToString("yyyy.MM.dd"));//考试时间
//                    LabelExamDate.Text = Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("yyyy年MM月dd日");
//                }

//                //照片               
//                ImageFaceImg.ImageUrl = GetFacePhotoPath(dt.Rows[0]["ExamPlanID"].ToString(), dt.Rows[0]["CertificateCode"].ToString());



//                Dictionary<string, string> list = new Dictionary<string, string>();
//                list.Add("WorkerName", LabelWorkerName.Text);
//                list.Add("PostTypeName", LabelPostTypeName.Text);
//                list.Add("PostName", LabelPostName.Text);
//                list.Add("ExamCardID", LabelExamCardID.Text);
//                list.Add("CertificateCode", LabelCertificateCode.Text);
//                list.Add("ExamPlaceName", LabelExamPlaceName.Text);
//                list.Add("ExamRoomCode", LabelExamRoomCode.Text);
//                list.Add("ExamDate", LabelExamDate.Text);
//                list.Add("FacePhoto", LabelExamCardID.Text);//照片标签
//                list.Add("Img_FacePhoto", ImageFaceImg.ImageUrl);//绑定照片
//                list.Add("UnitName", LabelUnitName.Text);//工作单位

//                //科目（2或3科）：注意2018-03-27日添加规则：‘测量验线员' ，'试验员'两个岗位补考实操（postID=50，54）
//                DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0} and PostStatus=1 and PostInfo.PostID <> 50 and PostInfo.PostID <> 54", dt.Rows[0]["ExamPlanID"].ToString()), (Request["t"] == "sc" ? "PostOrder desc" : "PostOrder"));

//                if (sub != null && sub.Rows.Count > 0)
//                {

//                    for (int i = 0; i < sub.Rows.Count; i++)
//                    {
//                        Literal Literal1 = new Literal();
//                        if ((Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") //实操准考证
//                            || (Request["t"] != "sc" == true && sub.Rows[i]["PostName"].ToString() != "实操"))//正常准考证非实操科目
//                        {
//                            if (sub.Rows[i]["PostName"].ToString() == "实操")
//                            {
//                                Literal1.Text = string.Format(
//                               @" <tr class=""GridLightBK"">
//                                    <td nowrap=""nowrap"" align=""right"">
//                                        <strong>科目：</strong>
//                                    </td>
//                                    <td colspan=""2"">
//                                        {0}                                
//                                    </td>
//                                </tr>", sub.Rows[i]["PostName"].ToString()
//                                        );

//                                //科目考试时间及名称

//                                list.Add("km1_value", sub.Rows[i]["PostName"].ToString());
//                            }
//                            else
//                            {
//                                Literal1.Text = string.Format(
//                         @" <tr class=""GridLightBK"">
//                            <td nowrap=""nowrap"" align=""right"">
//                                <strong>科目{0}：</strong>
//                            </td>
//                            <td colspan=""2"">
//                                {1} - {2}&nbsp;{3}                                
//                            </td>
//                        </tr>", (sub.Rows.Count==1) ? "" : Convert.ToString(i + 1)
//                              , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("HH:mm")
//                                  , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm")
//                                  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")
//                                  );

//                                //科目考试时间及名称
//                                list.Add(string.Format("km{0}_value", (i + 1)),
//                                    string.Format("{0} - {1} {2}", Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString("HH:mm")
//                                    , Convert.ToDateTime(dt.Rows[0]["ExamStartTime"]).ToString() == Convert.ToDateTime(dt.Rows[0]["ExamStartDate"]).ToString() ? Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm") : Convert.ToDateTime(dt.Rows[0]["ExamEndTime"]).ToString("HH:mm")
//                                  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));
//                                //if (Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("yyyy-MM-dd") == Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("yyyy-MM-dd"))//考一天
//                                //{
//                                //    list.Add(string.Format("km{0}_value", sub.Rows[i]["PostName"].ToString() == "实操" ? "1" : Convert.ToString(i + 1)),
//                                //    string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm")
//                                //    , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm")
//                                //  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));

//                                //}
//                                //else//考多天
//                                //{
//                                //    list.Add(string.Format("km{0}_value", sub.Rows[i]["PostName"].ToString() == "实操" ? "1" : Convert.ToString(i + 1)),
//                                //    string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("yyyy.MM.dd HH:mm")
//                                //    , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("yyyy.MM.dd HH:mm")
//                                //  , sub.Rows[i]["PostName"].ToString().Replace("法规基础", "安全生产知识考核")));
//                                //}
//                            }
//                        }
//                        else if (sub.Rows[i]["PostName"].ToString() == "实操")
//                        {
//                            Literal1.Text = string.Format(
//                    @" <tr class=""GridLightBK"">
//                            <td nowrap=""nowrap"" align=""right"">
//                                <strong>科目{0}：</strong>
//                            </td>
//                            <td colspan=""2"">
//                                {1}（理论考试合格的考生方可参加实操考试,考试时间、地点以实操准考证为准。）                               
//                            </td>
//                        </tr>", Convert.ToString(i + 1), sub.Rows[i]["PostName"].ToString());

//                            //科目考试时间及名称
//                            list.Add(string.Format("km{0}_value", Convert.ToString(i + 1)), string.Format("{0}（理论考试合格的考生方可参加实操考试,考试时间、地点以实操准考证为准。）", sub.Rows[i]["PostName"].ToString()));
//                        }
//                        else
//                        {
//                            list.Add(string.Format("km{0}_text", i + 1), "");
//                            list.Add(string.Format("km{0}_value", i + 1), "");
//                            continue;
//                        }

//                        PlaceHolder1.Controls.Add(Literal1);
//                        list.Add(string.Format("km{0}_text", (Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") ? "1" : Convert.ToString(i + 1))
//                            , string.Format("科目{0}：", (Request["t"] == "sc" && sub.Rows[i]["PostName"].ToString() == "实操") || sub.Rows.Count == 1 ? "" : Convert.ToString(i + 1)));

//                    }


//                    if (sub.Rows.Count % 3 != 0)//考试科目未满三门
//                    {
//                        for (int i = sub.Rows.Count % 3 + 1; i <= 3; i++)
//                        {

//                            list.Add(string.Format("km{0}_text", i.ToString()), "");
//                            list.Add(string.Format("km{0}_value", i.ToString()), "");
//                        }
//                    }

//                    //list.Add("ExamDate", LabelExamDate.Text);

//                    ViewState["PrintData"] = list;
//                }

//            }
//            base.Page_Init(sender, e);
//        }

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

        ////打印
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page
        //        , string.Format("~/Template/{0}.doc", shicao == true ? "准考证_实操" : "准考证")
        //        , string.Format("~/UpLoad/ExamCard/{0}/准考证_{1}.doc", ViewState["ExamPlanID"], ViewState["ExamCardID"])
        //        , ViewState["PrintData"] as Dictionary<string, string>);
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/ExamCard/{0}/准考证_{1}.doc');", ViewState["ExamPlanID"].ToString(), ViewState["ExamCardID"].ToString(), RootUrl), true);
        //}

        //导出
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //string sourceFile = HttpContext.Current.Server.MapPath(string.Format("~/Template/{0}.docx", shicao == true ? "准考证_实操" : "准考证"));
            string sourceFile = "";

            if (shicao == true)
            {
                sourceFile = HttpContext.Current.Server.MapPath("~/Template/准考证_实操.docx");
            }
            else
            {
                ExamPlanOB _ExamPlanOB = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"]));
                if (_ExamPlanOB.ExamWay == "机考")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/准考证.docx");
                }
                else
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/准考证_网考.docx");
                }
            }

            
            PrintDocument.CreateDataToWordByHashtable(sourceFile, string.Format("准考证_{0}", ViewState["ExamCardID"]), (System.Collections.Hashtable)ViewState["PrintData"]);
        }

        protected void CheckSaveDirectory()
        {
            //准考证存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamCard/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamCard/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamCard/" + ViewState["ExamPlanID"].ToString()))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamCard/" + ViewState["ExamPlanID"].ToString()));
        }
    }
}
