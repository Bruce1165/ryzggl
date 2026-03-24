using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class CheckSignUnit : BasePage
    {
        protected bool isExcelExport = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));

                RadComboBoxYear.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;

                ButtonSearch_Click(sender, e);

                UIHelp.layerAlert(Page, @"重要提示：企业在审核时,须严格把关。报名表照片为彩色一寸白底标准免冠证件照、个人信息准确无误、上传位置合格方可点击确认。企业审核把关不严的将严肃处理。<br/><br/><b>特别提示：<br/>1、企业确认截止前，企业驳回个人申请，个人应及时修改、补充材料后再次提交企业确认。企业确认截止后，个人无法修改、补充材料，企业无法确认。<br/><br/>
2、企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。<br/><br/>
3、企业确认截止后，市住建委进行一次性审核。审核不通过的，个人无法进行修改、补充及提交，请企业认真复核申请人的考试报名材料，确保符合审核要求。");
            }
        }

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <returns>排序表达式</returns>
        private string SetSortBy()
        {
            return "ExamSignUpID";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //if (RadioButtonListStatus.SelectedItem.Value == EnumManager.SignUpStatus.FirstChecked//"已初审"
            //    && PersonType == 4)////培训点不按准考证号排序，而按初审顺序方便打印后分发
            //{
            //    RadGrid1.MasterTableView.SortExpressions.Clear();
            //    GridSortExpression sortStr1 = new GridSortExpression();
            //    sortStr1.FieldName = "FirstTrialTime,ExamSignUpID";
            //    sortStr1.SortOrder = GridSortOrder.Ascending;
            //    RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
            //    return "FirstTrialTime,ExamSignUpID";
            //}
            //else
            //{
            //    return "ExamSignUpID";
            //}
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {
            
            QueryParamOB q = new QueryParamOB();
            
            q.Add(string.Format("UnitCode like '%{0}%'", ZZJGDM));// 机构代码

            if (RadioButtonListStatus.SelectedItem.Value == EnumManager.SignUpStatus.NewSignUp)//"待初审"
            {
                q.Add(string.Format("Status ='{0}'", EnumManager.SignUpStatus.NewSignUp));

                //企业审核时间为从报名开始日期+1日，至报名截至日期 +2日。
                q.Add("[SIGNUPSTARTDATE] <dateadd(day,-1,getdate())");//企业确认开始日期为个人提交开始日期＋1天
                q.Add("[SignUpEndDate] > dateadd(day,-3,getdate())");//企业确认截止日期为个人提交截至日期＋2天
                //TableUnitCheck.Visible = true;
                //ButtonCheck.Enabled = true;
                
                RadGrid1.MasterTableView.Columns.FindByUniqueName("TemplateColumn").Visible = true;
            }
            else if(RadioButtonListStatus.SelectedItem.Value =="过期无法初审")
            {
                q.Add(string.Format("Status ='{0}'", EnumManager.SignUpStatus.NewSignUp));//"待初审"
                q.Add("[SignUpEndDate] < dateadd(day,-3,getdate())");//已进入审核阶段仍未企业审核
                //TableUnitCheck.Visible = false;
                //ButtonCheck.Enabled = false;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("TemplateColumn").Visible = false;
            }
            else//已初审核过
            {
                q.Add("[FIRSTTRIALTIME] >'2001-1-1'");//已初审
                //TableUnitCheck.Visible = false;
                //ButtonCheck.Enabled = false;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("TemplateColumn").Visible = false;
            }
            //ButtonCheck.CssClass = ButtonCheck.Enabled == true ? "bt_large" : "bt_large btn_no";

            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (_ExamPlanID != "0")
            {
                q.Add("ExamPlanID=" + _ExamPlanID);
            }
            if (RadTxtSignUpCode.Text.Trim() != "")
            {
                q.Add(string.Format("SignUpCode like '%{0}%'", RadTxtSignUpCode.Text.Trim()));
            }
        
            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }

            //岗位工种
            if (PostSelect.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect.PostID));
            else if (PostSelect.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月
                      
            return q;
        }

//        //审核
//        protected void ButtonCheck_Click(object sender, EventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
//            if (!IsGridSelected(RadGrid1))
//            {
//                UIHelp.layerAlert(Page, "你还没有选择数据！");
//                return;
//            }

//            if (CheckPhoto() == false) return;//检查电子照片

//            string filterString = "";//过滤条件

//            if (GetGridIfCheckAll(RadGrid1) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
//                    filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
//                else//包含
//                    filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
//            }

//            ////判断是否超过有报名截止日1天的申请，不允许审核。
//            //if (RadioButtonListOldUnitCheckResult.SelectedValue == "同意")
//            //{
//            //    DataTable dtLimit = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, string.Format(" {0} and dateadd(day,2,SIGNUPENDDATE) < getdate()", filterString), "");
//            //    if (dtLimit != null && dtLimit.Rows.Count > 0)
//            //    {
//            //        UIHelp.layerAlert(Page, string.Format("列表中有“{0}”等{1}人申请超过考试报名截止日期,建委已经不再接收报名申请，请您参加下次报名。", dtLimit.Rows[0]["WorkerName"], dtLimit.Rows.Count), 5, 0);
//            //        return;
//            //    }
//            //}

//            DateTime _ModifyTime = DateTime.Now;
//            int count = 0;//审核人数
//            count = ExamSignUpDAL.SelectCount_New(filterString);
//            int passCount = 0;//审核通过人数
//            passCount = ExamSignUpDAL.SelectCount_New(string.Format("{0} and ZACheckResult >0", filterString));

//            ExamSignUpOB esob = new ExamSignUpOB();
//            esob.Status = (RadioButtonListOldUnitCheckResult.SelectedValue == "同意" ? EnumManager.SignUpStatus.FirstChecked : EnumManager.SignUpStatus.ReturnEdit);
//            esob.ModifyTime = _ModifyTime;
//            esob.FIRSTTRIALTIME = _ModifyTime;//单位审核时间
//            esob.HireUnitAdvise = TextBoxOldUnitCheckRemark.Text;//单位意见
                     
//            try
//            {
//                if (esob.Status == EnumManager.SignUpStatus.FirstChecked)
//                {
//                    ExamSignUpDAL.CheckFirst(esob, string.Format(" and ZACheckResult >0 and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
//                    UIHelp.WriteOperateLog(PersonName, UnitID, "报名初审", string.Format("初审{0}人，通过{1}人。", count, passCount));
//                    UIHelp.layerAlert(Page, string.Format(@"考试报名申请企业确认成功，已提交市住建委审核。<br/>初审{0}人，通过{1}人。{2}<br/><br/>
//<b>特别提示：<br/>
//1、企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。<br/>
//2、企业确认截止后，市住建委进行一次性审核。审核不通过的，个人无法进行修改、补充及提交，请企业认真复核申请人的考试报名材料，确保符合审核要求。</b>", count, passCount,
//                        count == passCount ? "" : string.Format("{0}人校验未通过（或尚未校验），请点击每个报名表查看校验未通过原因。",count -passCount)
//                        ));
//                }
//                else
//                {
//                    ExamSignUpDAL.CheckFirst(esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
//                    UIHelp.WriteOperateLog(PersonName, UnitID, "报名初审", string.Format("初审驳回{0}人。", count));
//                    UIHelp.layerAlert(Page, string.Format(@"已成功退回个人，共计{0}人。<br/>
//<b>特别提示：<br/>
//1、企业确认截止前，企业驳回个人申请，个人应及时修改、补充材料后再次提交企业确认。<br/>
//2、企业确认截止后，个人无法修改、补充材料，企业无法确认。</b>", count));
//                }
                
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "考试报名审核失败！", ex);
//                return;
//            }

            
//            ClearGridSelectedKeys(RadGrid1);
//            RadGrid1.DataBind();
//        }

        //检查照片是否都上传了
        protected bool CheckPhoto()
        {
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGrid1) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
                    filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }

            DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, filterString, "ExamSignUpID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (GetFacePhotoPath(dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()) == "~/Images/tup.gif")
                {
                    rowErr.Append(string.Format("<br >“{0}”未上传以证件号“{1}”命名的照片！", dt.Rows[i]["WorkerName"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));
                }
            }

            if (rowErr.Length > 0)
            {
                rowErr.Insert(0, "审核没有通过，以下人员尚未上传电子照片：");
                UIHelp.layerAlert(Page, rowErr.ToString());
                return false;
            }
            else 
                return true;
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGrid1);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = GetQueryParamOB();
            SetSortBy();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "ExamSignUpID");
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.VirtualItemCount > 5000 && ExamPlanSelect1.ExamPlanID.HasValue==false )
            {
                UIHelp.layerAlert(Page, "请按考试计划查询后再导出！");
                return;
            }
            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB q = GetQueryParamOB();
            string sortBy = SetSortBy();
            string saveFile = string.Format("~/UpLoad/SignUpTable/报名初审{0}_{1}.xls",DateTime.Now.ToString("yyyyMMddHHmm"), UserID);//保存文件名
             string colHead = @"报名批号\姓名\证件号码\单位名称\申报岗位工种\考试时间";
             string colName = @"SignUpCode\WorkerName\CertificateCode\UnitName\PostName\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.')  else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
             if (ExamPlanSelect1.PostID.HasValue && ExamPlanSelect1.PostID.Value == 12)
             {
                 if (ExamPlanSelect1.ExamPlanName.Contains("暖通") == true)
                 {
                     colName = @"SignUpCode\WorkerName\CertificateCode\UnitName\PostName||'（暖通）'\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
                 }
                 if (ExamPlanSelect1.ExamPlanName.Contains("电气") == true)
                 {
                     colName = @"SignUpCode\WorkerName\CertificateCode\UnitName\PostName||'（电气）'\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
                 }
             }
            // if (RoleIDs == "49" || (ExamPlanSelect1.PostTypeID.HasValue && ExamPlanSelect1.PostTypeID.Value==2))//报名点可看见电话
            //{
            //    colHead += @"\联系电话";
            //    colName += @"\Phone";
            //}
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));

                //导出数据到数据库服务器
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMSIGNUP_NEW", q.ToWhereString(), sortBy, colHead, colName);
                
               
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出报名初审查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("报名初审查询结果下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; 
                    break;
            }

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "CertificateCode")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");

        }

        //根据证件号码显示照片
        protected string ShowFaceimage(string CertificateCode, string ExamPlanID)
        {
            System.Random rm = new Random();
            string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ExamPlanID, CertificateCode))); //绑定照片;
            return img;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/tup.jpg";
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

        ////删除
        //protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    //获取类型Id
        //    Int64 _ExamSignUpID = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamSignUpID"]);             //Convert.ToInt32(e.CommandArgument);
        //    DBHelper db = new DBHelper();
        //    DbTransaction tran = db.BeginTransaction();
        //    try
        //    {
        //        ExamSignUp_DelDAL.Insert(tran, _ExamSignUpID, PersonName, DateTime.Now);
        //        ExamSignUpDAL.Delete(tran, _ExamSignUpID);
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Commit();
        //        UIHelp.WriteErrorLog(Page, "删除报名信息失败！", ex);
        //        return;
        //    }
        //    UIHelp.WriteOperateLog(PersonName, UserID, "删除报名", string.Format("初审时删除报名。报名批次号：{0}，报名人：{1}，详情见“查看报名情况/查看删除历史”。",
        //      e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("SignUpCode").OrderIndex].Text,
        //      e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("WorkerName").OrderIndex].Text));

        //    UIHelp.layerAlert(Page, "删除成功！",6,3000);
        //}

      
    }
}
