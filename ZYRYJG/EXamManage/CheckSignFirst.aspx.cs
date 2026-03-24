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
    public partial class CheckSignFirst : BasePage
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

                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <returns>排序表达式</returns>
        private string SetSortBy()
        {
            return "ExamSignUpID";
        }

        /// <summary>
        /// 格式化受理状态显示
        /// </summary>
        /// <param name="status">报名审核状态</param>
        /// <returns>受理状态</returns>
        protected static string fmtStatus(string status)
        {
            switch (status)
            {
                case "待初审":
                case "已初审":
                    return status;
                case "退回修改":
                    return "审核未通过";
                default:
                    return "已受理";
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {           
            QueryParamOB q = new QueryParamOB();
            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (_ExamPlanID != "0")
            {
                q.Add("ExamPlanID=" + _ExamPlanID);
            }

            //初审类型
            switch (RadComboBoxUnitCheckType.SelectedValue)
            {
                case "待初审":
                case "已初审":
                    q.Add(string.Format("[Status] like '{0}'", RadComboBoxUnitCheckType.SelectedValue));
                    break;
                case "已受理":
                    q.Add("([Status] like '已受理'  or [Status] like '已审核'  or [Status] like '已缴费')");
                    break;
                case "退回修改":
                    q.Add("([Status] like '退回修改' and [AcceptTime]>'2000-01-01')");
                    break;
                default:
                    q.Add("([Status] like '待初审' or [Status] like '已初审' or [Status] like '已审核'  or [Status] like '已缴费' or  ([Status] like '退回修改' and [AcceptTime]>'2000-01-01') )");
                    break;
            }


            //初审类型
            if (RadComboBoxFirstCheckType.SelectedValue != "")
            {
                switch (RadComboBoxFirstCheckType.SelectedValue)
                {
                    case "<2":
                        q.Add("FirstCheckType < 2");//人工审核（全部）
                        break;
                    case "3.1":
                        q.Add("FirstCheckType = 3 and (PostID < 147 or PostID > 147)");//社保符合(非A证)
                        break;
                    case "3.2":
                        q.Add("FirstCheckType = 3 and PostID = 147");//社保符合(A证非法人)
                        break;
                    default:
                        q.Add(string.Format("FirstCheckType = {0}", RadComboBoxFirstCheckType.SelectedValue));//具体分类
                        break;
                }
            }

             //社保缴费月份
            if (RadComboBoxJFCount.SelectedValue != "")
            {
               q.Add(string.Format("JFCount ={0}",RadComboBoxJFCount.SelectedValue));
            }

            

            //学历职称证明方式
            if (RadComboBoxSignupPromise.SelectedValue != "")
            {
                if(RadComboBoxSignupPromise.SelectedValue=="1")//上传承诺书
                {
                    q.Add("SignupPromise >0");
                }
                else
                {
                    q.Add("(SignupPromise <1 or SignupPromise is null)");
                }
            }
            
            //// 单位名称
            //if (RadTxtUnitName.Text != "")
            //{
            //    q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            //}
            ////姓名
            //if (RadTxtWorkerName.Text != "")
            //{
            //    q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            //}
            ////证件号码
            //if (RadTxtCertificateCode.Text != "")
            //{
            //    q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            //}

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项（单位名称、姓名、证件号码）
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

            q.Add(string.Format("LATESTCHECKDATE >='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));//审核截止前

            
             //考试方式
            if (RadComboBoxExamWay.SelectedValue != "")
            {
                q.Add(string.Format("ExamWay ='{0}'", RadComboBoxExamWay.SelectedValue));
            }
           
            return q;
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

        //受理
        protected void ButtonAccept_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
            if (!IsGridSelected(RadGrid1))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

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

            ////启用质安网检查后取消注释
            //if (RadioButtonListCheckResult.SelectedValue == "通过")
            //{
            //    if (GetGridIfCheckAll(RadGrid1) == true)//全选
            //    {
            //        filterString = string.Format(" {0} and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
            //    }
            //    else
            //    {
            //        if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
            //            filterString = string.Format(" {0} and ExamSignUpID not in({1})  and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //        else//包含
            //            filterString = string.Format(" {0} and ExamSignUpID in({1})  and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //    }
            //}
            //else
            //{
            //    if (GetGridIfCheckAll(RadGrid1) == true)//全选
            //    {
            //        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            //    }
            //    else
            //    {
            //        if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
            //            filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //        else//包含
            //            filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //    }
            //}

            DateTime _ModifyTime = DateTime.Now;
            int count = 0;
            count = ExamSignUpDAL.SelectCount_New(filterString);

            ExamSignUpOB esob = new ExamSignUpOB();
            esob.AcceptMan = PersonName;
            esob.AcceptResult = TextBoxCheckResult.Text.Trim();
            esob.AcceptTime = DateTime.Now;
            esob.Status = (RadioButtonListCheckResult.SelectedValue == "通过" ? EnumManager.SignUpStatus.Accept : EnumManager.SignUpStatus.ReturnEdit);
            esob.ModifyPersonID = PersonID;
            esob.ModifyTime = _ModifyTime;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //特殊情况：报名点或注册中心初审时，修改报名表，将培训点记为本报名点或注册中心用户
                ExamSignUpDAL.Accept(tran, esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
                tran.Commit();         
                UIHelp.WriteOperateLog(PersonName, UserID, "考试报名受理", string.Format("报名受理，核结果：{1}，人数：{0}人。", count, RadioButtonListCheckResult.SelectedValue));
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "考试报名受理失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, string.Format("报名受理，核结果：{1}，人数：{0}人。", count, RadioButtonListCheckResult.SelectedValue));
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();
        }

        ////检查照片是否都上传了
        //protected bool CheckPhoto()
        //{
        //    System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

        //    string filterString = "";//过滤条件

        //    if (GetGridIfCheckAll(RadGrid1) == true)//全选
        //    {
        //        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
        //    }
        //    else
        //    {
        //        if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
        //            filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
        //        else//包含
        //            filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
        //    }

        //    DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, filterString, "ExamSignUpID");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (GetFacePhotoPath(dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()) == "~/Images/tup.gif")
        //        {
        //            rowErr.Append(string.Format("<br >“{0}”未上传以证件号“{1}”命名的照片！", dt.Rows[i]["WorkerName"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));
        //        }
        //    }

        //    if (rowErr.Length > 0)
        //    {
        //        rowErr.Insert(0, "审核没有通过，以下人员尚未上传电子照片：");
        //        UIHelp.layerAlert(Page, rowErr.ToString());
        //        return false;
        //    }
        //    else 
        //        return true;
        //}
              

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

            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB q = GetQueryParamOB();
            string sortBy = SetSortBy();
            string saveFile = string.Format("~/UpLoad/SignUpTable/报名受理{0}_{1}.xls",DateTime.Now.ToString("yyyyMMddHHmm"), PersonID.ToString());//保存文件名
             string colHead = @"申报日期\姓名\证件号码\单位名称\申报岗位工种\考试时间";
             string colName = @"CONVERT(varchar(10), FIRSTTRIALTIME, 20)\WorkerName\CertificateCode\UnitName\PostName\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.')  else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
             //if (ExamPlanSelect1.PostID.Value == 12)
             //{
             //    if (ExamPlanSelect1.ExamPlanName.Contains("暖通") == true)
             //    {
             //        colName = @"WorkerName\CertificateCode\UnitName\PostName||'（暖通）'\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
             //    }
             //    if (ExamPlanSelect1.ExamPlanName.Contains("电气") == true)
             //    {
             //        colName = @"WorkerName\CertificateCode\UnitName\PostName||'（电气）'\case when ExamStartDate=ExamEndDate then replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') else replace(CONVERT(varchar(10), ExamStartDate, 20),'-','.') + '-' + replace(CONVERT(varchar(10), ExamEndDate, 20),'-','.') end";
             //    }
             //}
            // if (IfExistRoleID("49") == true || (ExamPlanSelect1.PostTypeID.HasValue && ExamPlanSelect1.PostTypeID.Value == 2))//报名点可看见电话
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
                UIHelp.WriteErrorLog(Page, "导出报名受理查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("报名受理列表下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

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
