using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{
    public partial class SourceEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SourceMgr.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrEmpty(Request["o"]) == true)//new
                    {
                        RadNumericTextBoxPeriodHour.Value = 0;
                        RadNumericTextBoxMinute.Value = 0;
                        RadNumericTextBoxSourceYear.Value = DateTime.Now.Year;
                        long sourceID = string.IsNullOrEmpty(Request["u"]) == true ? 0 : Convert.ToInt64(Utility.Cryptography.Decrypt(Request["u"]));
                        RadNumericTextBoxSortID.Value = SourceDAL.GetNextSortID(sourceID);

                        if (string.IsNullOrEmpty(Request["u"]) == false)//课件，只记录子课程(章节)名称，课时，排序键，授课教师，视频地址
                        {
                            Tr4.Visible = false;//隐藏课程简介
                            TrSourceImg.Style.Add("display" ,"none");//隐藏课程背景
                            TrBarType.Visible = false;//隐藏所属栏目
                            Label_Source.Text = "课件名称";
                            RadioButtonListSourceType.Enabled = false;//课件的选修、必修属性，教师名称，教师单位，专业，年度，状态，自动与上级课程同步
                            SourceMDL _SourceMDL = SourceDAL.GetObject(sourceID);
                            if (_SourceMDL != null)
                            {
                                RadTextBoxWorkUnit.Text = _SourceMDL.WorkUnit;
                                RadTextBoxTeacher.Text = _SourceMDL.Teacher;
                                RadioButtonListSourceType.ClearSelection();
                                RadioButtonListSourceType.Items.FindByValue(_SourceMDL.SourceType).Selected = true;
                                RadioButtonListStatus.ClearSelection();
                                RadioButtonListStatus.Items.FindByValue(_SourceMDL.Status).Selected = true;
                                RadNumericTextBoxSourceYear.Value = _SourceMDL.SourceYear;
                            }
                        }
                        else//课程
                        {
                            Tr5.Visible = false;//隐藏视频地址                        
                            RadNumericTextBoxPeriodHour.Enabled = false;
                            RadNumericTextBoxMinute.Enabled = false;
                        }
                    }
                    else//update
                    {

                        SourceMDL _SourceMDL = SourceDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                        if (_SourceMDL != null)
                        {
                            ViewState["SourceMDL"] = _SourceMDL;
                            UIHelp.SetData(TableEdit, _SourceMDL);
                            RadNumericTextBoxSortID.Text = _SourceMDL.SortID.ToString();
                            RadNumericTextBoxPeriodHour.Value = _SourceMDL.Period.Value / 60;
                            RadNumericTextBoxMinute.Value = _SourceMDL.Period.Value % 60;
                            RadNumericTextBoxSourceYear.Value = _SourceMDL.SourceYear;
                            RadioButtonListStatus.ClearSelection();
                            RadioButtonListStatus.Items.FindByValue(_SourceMDL.Status).Selected = true;
                            RadioButtonListSourceType.ClearSelection();
                            RadioButtonListSourceType.Items.FindByValue(_SourceMDL.SourceType).Selected = true;
                            LabelShowPeriod.Text = string.Format("{0}学时", _SourceMDL.ShowPeriod);

                            if (_SourceMDL.ParentSourceID != 0)//课件，只记录子课程(章节)名称，课时，排序键，视频地址
                            {
                                Tr4.Visible = false;//隐藏课程简介     
                                TrSourceImg.Style.Add("display", "none");//隐藏课程背景
                                TrBarType.Visible = false;//隐藏所属栏目
                                Label_Source.Text = "课件名称";
                                RadioButtonListSourceType.Enabled = false;//课件的选修、必修属性自动与上级课程同步
                                RadTextBoxSDKID.Text = _SourceMDL.SourceWarePlayParam;
                            }
                            else//课程
                            {
                                Tr5.Visible = false;//隐藏视频地址
                                RadNumericTextBoxPeriodHour.Enabled = false;
                                RadNumericTextBoxMinute.Enabled = false;
                                if (string.IsNullOrEmpty(_SourceMDL.SourceImg) == false)
                                {
                                    SelectImg1.SourceImg = _SourceMDL.SourceImg;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "读取课程课件信息失败！", ex);
                    return;
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SourceMDL _SourceMDL = ViewState["SourceMDL"] == null ? new SourceMDL() : (SourceMDL)ViewState["SourceMDL"];
            string _SourceType = "";
            string _Status = "";
            if (ViewState["SourceMDL"] != null)
            {
                _SourceType = _SourceMDL.SourceType;
                _Status = _SourceMDL.Status;
            }
            _SourceMDL.SourceName = RadTextBoxSourceName.Text.Trim(); //课程名
            _SourceMDL.Teacher = RadTextBoxTeacher.Text.Trim();  //老师
            _SourceMDL.WorkUnit = RadTextBoxWorkUnit.Text.Trim();//职称、工作单位          
            _SourceMDL.SourceType = RadioButtonListSourceType.SelectedValue;//课程类型（选修、必修）
            _SourceMDL.Status = RadioButtonListStatus.SelectedValue;//状态（启用、停用）          
            _SourceMDL.SortID = Convert.ToInt32(RadNumericTextBoxSortID.Text);//排序ID
            _SourceMDL.SourceYear = Convert.ToInt32(RadNumericTextBoxSourceYear.Text);//年度
            _SourceMDL.Period = Convert.ToInt32(RadNumericTextBoxPeriodHour.Value) * 60 + Convert.ToInt32(RadNumericTextBoxMinute.Value);//课时
            _SourceMDL.SourceWareUrl = RadTextBoxSourceWareUrl.Text.Trim();

            if (Tr5.Visible == true)//课件需要录入SDKID
            {
                _SourceMDL.SourceWarePlayParam = RadTextBoxSDKID.Text.Trim();
                _SourceMDL.ShowPeriod = SourceDAL.ConvertShowPeriod(_SourceMDL.Period.Value);
            }
            else//课程
            {
                _SourceMDL.Description = RadTextBoxDescription.Text.Trim();//课程简介
                _SourceMDL.SourceImg = (SelectImg1.SourceImg == "" ? null : SelectImg1.SourceImg);
                _SourceMDL.BarType = RadComboBoxBarType.SelectedValue;
            }

            DBHelper db = new DBHelper("DBRYPX");
            DbTransaction trans = db.BeginTransaction();
            try
            {
                if (ViewState["SourceMDL"] == null)//new
                {
                    _SourceMDL.ParentSourceID = (string.IsNullOrEmpty(Request["u"]) == true) ? 0 : Convert.ToInt64(Utility.Cryptography.Decrypt(Request["u"])); //上级课程ID  
                    _SourceMDL.SourceWareCount = 0;
                    SourceDAL.Insert(trans, _SourceMDL);
                    ViewState["SourceMDL"] = _SourceMDL;
                }
                else//update
                {
                    SourceDAL.Update(trans, _SourceMDL);

                    if (_SourceMDL.ParentSourceID == 0 //修改的是课程
                        && (_SourceType != _SourceMDL.SourceType || _Status != _SourceMDL.Status))//修改课程类型（选修、必修）或专业、或状态
                    {
                        SourceDAL.UpdateSubSourceType(trans, _SourceMDL.SourceID.Value, _SourceMDL.SourceType,  RadioButtonListStatus.SelectedValue);//同步更新包含课件课程类型、专业、状态
                    }                    
                }

                if (_SourceMDL.ParentSourceID != 0)//新增或修改课件后，更新课程课时=包含课件课时之和
                {
                    SourceDAL.UpdatePeriod(trans, _SourceMDL.ParentSourceID.Value);
                    SourceDAL.UpdateSourceWareCount(trans, _SourceMDL.ParentSourceID.Value);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "编辑课程失败！", ex);
                return;
            }

            if (ViewState["SourceMDL"] == null)//new
            {
                if (_SourceMDL.ParentSourceID != 0)//课件
                    UIHelp.WriteOperateLog(UserName, UserID, "添加课件", string.Format("课件名称：{0}。", _SourceMDL.SourceName));
                else
                    UIHelp.WriteOperateLog(UserName, UserID, "添加课程", string.Format("课程名称：{0}。", _SourceMDL.SourceName));
            }
            else//update
            {
                if (_SourceMDL.ParentSourceID != 0)//课件
                    UIHelp.WriteOperateLog(UserName, UserID, "更新课件", string.Format("课件名称：{0}。", _SourceMDL.SourceName));
                else
                    UIHelp.WriteOperateLog(UserName, UserID, "更新课程", string.Format("课程名称：{0}。", _SourceMDL.SourceName));
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "parent.refreshGrid();hideIfam();", true);
        }
    }
}