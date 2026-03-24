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
    public partial class UnitTrainPlanEdit : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "UnitTrainPlan.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //课程年度
               DataTable dt= CommonDAL.GetDataTableDB("DBRYPX",@"select distinct [SourceYear]
                                                  FROM [dbo].[Source]
                                                  where [Status] = '启用' and[ParentSourceID] =0 order by [SourceYear] desc");
               RadComboBoxPackageYear.DataSource = dt;
               RadComboBoxPackageYear.DataTextField = "SourceYear";
               RadComboBoxPackageYear.DataValueField = "SourceYear";
               RadComboBoxPackageYear.DataBind();

               if (RadComboBoxPackageYear.Items.Count >0)
               {
                   RadComboBoxPackageYear.Items[0].Selected = true;
               }
               IniRadGridSource((RadComboBoxPackageYear.Items.Count > 0) ? Convert.ToInt32(RadComboBoxPackageYear.SelectedValue) : 2000);
               

                if (string.IsNullOrEmpty(Request["o"]) == false)//update
                {              
                    PackageMDL _PackageMDL = PackageDAL.GetObject(Convert.ToInt64(Request["o"]));
                    if (_PackageMDL != null)
                    {
                       
                        ViewState["PackageMDL"] = _PackageMDL;
                  
                        RadNumericTextBoxPeriodHour.Value = _PackageMDL.Period.Value / 45;

                        PostSelect1.PostTypeName = _PackageMDL.PostTypeName;
                        if (string.IsNullOrEmpty(_PackageMDL.PostName) == false)
                        {
                            PostSelect1.PostName = _PackageMDL.PostName;
                        }

                        RadioButtonListPublishStatus.Items.FindByValue(_PackageMDL.Status).Selected = true;

                        BindRadGridSource();
                    }
                }
                //else//new
                //{
                //        IniRadGridSource(DateTime.Now.Year);
                //}
                ComputSelectPeriod();

                IniRadGridWorker();
            }
        }

        //绑定课程列表
        protected void IniRadGridSource(int? year)
        {
            QueryParamOB q = new QueryParamOB();
            if(year.HasValue ==true)
                q.Add(string.Format("SourceYear={0} and Source.Status = '启用' and Source.ParentSourceID =0", year.Value.ToString()));
            else
                q.Add("Source.Status = '启用' and Source.ParentSourceID =0");
            DataTable dt = SourceDAL.GetListWithQuestionCount(0, int.MaxValue - 1, q.ToWhereString(), "");
            RadGridSource.DataSource = dt;           
            RadGridSource.DataBind();
        }

        protected void IniRadGridWorker()
        {
            ObjectDataSourceWorker.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();


            q.Add(" workerid in(select top 300 workerid from worker where len([CULTURALLEVEL]) >1 and len(workername)>1)");
            

            ObjectDataSourceWorker.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridworker.CurrentPageIndex = 0;
            RadGridworker.DataSourceID = ObjectDataSourceWorker.ID;
        }

        //绑定选中课程
        protected void BindRadGridSource()
        {
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("PackageID = {0}",Request["o"]));
            q.Add(string.Format("SourceYear = {0}", Convert.ToInt32(RadComboBoxPackageYear.SelectedValue)));    
            DataTable dt = PackageSourceDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "");           
            GridDataItem find;
            for (int i = 0; i < dt.Rows.Count; i++)
			{
                find = RadGridSource.MasterTableView.FindItemByKeyValue("SourceID", dt.Rows[i]["SourceID"]);
                if (find != null)
                {
                    (find.FindControl("CheckBox1") as CheckBox).Checked = true;
                    //(find.FindControl("RadNumericTextBoxSortID") as RadNumericTextBox).Value = Convert.ToInt32(dt.Rows[i]["SortID"]);

                }
			}
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //if (PostSelect1.PostTypeID == "")
            //{
            //    UIHelp.layerAlert(Page, "请选择专业！");
            //    return;
            //}
            //CheckBox cbox;
            //RadNumericTextBox rtb_sortID;
            //bool selectedItem = false;//是否选择了课程
            //for (int i = 0; i < RadGridSource.MasterTableView.Items.Count; i++)
            //{
            //    cbox = (CheckBox)RadGridSource.MasterTableView.Items[i].FindControl("CheckBox1");
            //    if (cbox.Checked == true)
            //    {
            //        selectedItem = true;
            //        break;
            //    }
            //}
            //if (selectedItem == false)
            //{
            //    UIHelp.layerAlert(Page, "至少要勾选一个课程！");
            //    return;
            //}
            //if (Convert.ToDouble(HiddenFieldSelectPreid.Value) < RadNumericTextBoxPeriodHour.Value.Value)
            //{
            //    UIHelp.layerAlert(Page, string.Format("您当前选择课程总时长为“{0}学时”，未达到要求完成学时“{1}学时”，请继续勾选课程。"
            //   , HiddenFieldSelectPreid.Value
            //   , RadNumericTextBoxPeriodHour.Value));
            //    return;
            //}

            //PackageMDL _PackageMDL = ViewState["PackageMDL"] == null ? new PackageMDL() : (PackageMDL)ViewState["PackageMDL"];


            //_PackageMDL.PackageTitle = string.Format("{0}{1}",PostSelect1.PostTypeName,PostSelect1.PostName);//标题
            //_PackageMDL.Period = Convert.ToInt32(RadNumericTextBoxPeriodHour.Value) * 45;
            //_PackageMDL.Description = RadTextBoxDescription.Text.Trim();
            //_PackageMDL.PostTypeName = PostSelect1.PostTypeName;
            //if (string.IsNullOrEmpty(PostSelect1.PostID) == false)
            //{
            //    _PackageMDL.PostName = PostSelect1.PostName;
            //}
            //else
            //{
            //    _PackageMDL.PostName = null;
            //}        
            //_PackageMDL.Status = RadioButtonListPublishStatus.SelectedValue;

            //DBHelper db = new DBHelper("DBRYPX");
            //DbTransaction trans = db.BeginTransaction();
            
            //try
            //{
            //    if (ViewState["PackageMDL"] == null)//new
            //    {
            //        PackageDAL.Insert(trans,_PackageMDL);
            //        UIHelp.WriteOperateLog(UserName, UserID, "添加培训计划", string.Format("培训计划名称：{0}。", _PackageMDL.PackageTitle));
            //    }
            //    else//update
            //    {
            //        PackageDAL.Update(trans,_PackageMDL);
            //        PackageSourceDAL.Delete(trans, _PackageMDL.PackageID.Value, Convert.ToInt32(RadComboBoxPackageYear.SelectedValue)); //清空包含课程信息
            //        UIHelp.WriteOperateLog(UserName, UserID, "更新培训计划", string.Format("培训计划名称：{0}。", _PackageMDL.PackageTitle));                  
            //    }
                
            //    //保存包含课程信息
            //    int sort = 1;
            //    int year = Convert.ToInt32(RadComboBoxPackageYear.SelectedValue);
            //    for (int i = 0; i < RadGridSource.MasterTableView.Items.Count; i++)
            //    {
            //        cbox = (CheckBox)RadGridSource.MasterTableView.Items[i].FindControl("CheckBox1");
            //        if (cbox.Checked == true)
            //        {
            //            rtb_sortID = (RadNumericTextBox)RadGridSource.MasterTableView.Items[i].FindControl("RadNumericTextBoxSortID");
            //            PackageSourceDAL.Insert(trans, Convert.ToInt64(RadGridSource.MasterTableView.DataKeyValues[i]["SourceID"]), _PackageMDL.PackageID.Value, Convert.ToInt32(rtb_sortID.Value), year);
            //            sort++;
            //        }
            //    }
            //    trans.Commit();
               
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    UIHelp.WriteErrorLog(Page, "添加培训计划失败！", ex);
            //    return;
            //}

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "parent.refreshGrid();hideIfam();", true);
        }

        //选择课程后统计选择课程总时长 
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ComputSelectPeriod();                
        }

        //计选择课程总时长 
        protected void ComputSelectPeriod()
        {
            decimal SumPeriod = 0;//总学时
            CheckBox cbox;
            for (int i = 0; i < RadGridSource.MasterTableView.Items.Count; i++)
            {
                cbox = (CheckBox)RadGridSource.MasterTableView.Items[i].FindControl("CheckBox1");
                if (cbox.Checked == true) SumPeriod += Convert.ToDecimal(RadGridSource.MasterTableView.DataKeyValues[i]["ShowPeriod"]);
            }
            decimal OtherYearPeriod =0;//其他年份已选课程总学时
            if (ViewState["PackageMDL"] != null)//update
            {
                OtherYearPeriod=PackageSourceDAL.GetPackageSourcePeriodSum(Convert.ToInt64(Request["o"]), Convert.ToInt32(RadComboBoxPackageYear.SelectedValue));
            }
            HiddenFieldSelectPreid.Value = (SumPeriod + OtherYearPeriod).ToString();

            if (RadNumericTextBoxPeriodHour.Value.HasValue == false || (SumPeriod + OtherYearPeriod) >= Convert.ToInt32(RadNumericTextBoxPeriodHour.Value.Value))
            {
                DivTip.InnerHtml = string.Format("您当前选择课程总时长为“<b style=\"color:#C41212; font-weight:bold \">{0}学时</b>”，已满足要求完成课时“<b style=\"color:#C41212; font-weight:bold \">{1}学时</b>”。"
                    , (SumPeriod + OtherYearPeriod)
                    , RadNumericTextBoxPeriodHour.Value);
            }
            else
            {
                DivTip.InnerHtml = string.Format("您当前选择课程总时长为“<b style=\"color:#C41212; font-weight:bold \">{0}学时</b>”，未达到要求完成课时“<b style=\"color:#C41212; font-weight:bold \">{1}学时</b>”，请继续勾选课程。"
              , (SumPeriod + OtherYearPeriod)
              , RadNumericTextBoxPeriodHour.Value);
            }
        }

        protected void RadComboBoxPackageYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IniRadGridSource(Convert.ToInt32(RadComboBoxPackageYear.SelectedValue));
            BindRadGridSource();
            ComputSelectPeriod();
        }
    }
}