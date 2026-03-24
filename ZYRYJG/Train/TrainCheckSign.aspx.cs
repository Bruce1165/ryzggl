using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.Train
{
    public partial class TrainCheckSign : BasePage
    {
        /// <summary>
        /// 当前培训点信息
        /// </summary>
        protected TrainUnitMDL curTrainUnit
        {
            get { return ViewState["TrainUnitMDL"] == null ? null : (ViewState["TrainUnitMDL"] as TrainUnitMDL); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                TrainUnitMDL _TrainUnitMDL = TrainUnitDAL.GetObjectBySHTYXYDM(SHTJXYDM);
                if (_TrainUnitMDL != null)
                {
                    ViewState["TrainUnitMDL"] = _TrainUnitMDL;
                }

                //根据培训点绑定可创建考试计划的工种
                Dictionary<string, string> postFilterString = new Dictionary<string, string>();
                postFilterString.Add("4000", string.Format("PostID in({0})", _TrainUnitMDL.PostSet));//
                PostSelect2.PostFilterString = postFilterString;

                //初始化岗位类别
                for (int i = 1; i < 6; i++)
                {
                    PostSelect2.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                }
                PostSelect2.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                PostSelect2.PostTypeID = "4000";

                for (int i = 2024; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                RadComboBoxYear.Items.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;

                ButtonSearch_Click(sender, e);
            }
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
            QueryParamOB q = new QueryParamOB();


            //培训点名称
            q.Add(string.Format("TRAINUNITNAME like '{0}%'", curTrainUnit.TrainUnitName));

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


            //审核状态
            if (RadioButtonListCheckStatus.SelectedValue == "未审核")
            {
                q.Add(string.Format("[Status] ='{0}'", EnumManager.SignUpStatus.NewSignUp));
            }
            else//已审核
            {
                q.Add(string.Format("([Status] ='{0}' or [Status] ='{1}')", EnumManager.SignUpStatus.ReturnEdit, EnumManager.SignUpStatus.PayConfirmed));
            }

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

            //q.Add(string.Format("LATESTCHECKDATE >='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));//审核截止前

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

        //审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
            if (!IsGridSelected(RadGrid1))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            DateTime _ModifyTime = DateTime.Now;

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

            # region 检查是否重复报名

            string sql = @"
                            select count(*),certificatecode
                             from DBO.VIEW_EXAMSIGNUP_NEW 
                             where ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})
                            group by certificatecode
                            having count(*) >1";
            DataTable dtRepeatApply2 = CommonDAL.GetDataTable(string.Format(sql, filterString));

            if (dtRepeatApply2 != null && dtRepeatApply2.Rows.Count > 0)
            {
                System.Text.StringBuilder sbRepeatApply = new System.Text.StringBuilder();
                foreach (DataRow r in dtRepeatApply2.Rows)
                {
                    sbRepeatApply.Append(string.Format("，{0}", r["CertificateCode"]));
                }
                if (sbRepeatApply.Length > 0)
                {
                    sbRepeatApply.Remove(0, 1);
                }
                sbRepeatApply.Insert(0, "审核失败。以下人员可能存在重复报名情况，请先处理记录！<br />身份证号：");
                UIHelp.layerAlert(Page, sbRepeatApply.ToString());
                return;
            }

            # endregion 检查是否重复报名

            int count = ExamSignUpDAL.SelectCount_New(filterString);//记录数

            ExamSignUpOB esob = new ExamSignUpOB();
            esob.CheckDate = DateTime.Now;
            esob.CheckMan = curTrainUnit.TrainUnitName;
            esob.CheckResult = TextBoxCheckResult.Text.Trim();
            esob.Status = (RadioButtonListCheckResult.SelectedValue == "通过" ? EnumManager.SignUpStatus.PayConfirmed : EnumManager.SignUpStatus.ReturnEdit);
            esob.ModifyTime = esob.CheckDate;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                esob.CheckCode = UIHelp.GetNextBatchNumber(tran, "BMSH");//报名审核编号

                if (esob.Status == EnumManager.SignUpStatus.PayConfirmed)
                {
                    esob.PayConfirmDate = DateTime.Now;
                    esob.PayConfirmMan = curTrainUnit.TrainUnitName;
                    esob.PayConfirmRult = "通过";
                    esob.PayConfirmCode = esob.CheckCode;

                    ExamSignUpDAL.CheckAndPayConfirm(tran, esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
                }
                else
                {
                    ExamSignUpDAL.Check(tran, esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
                }
                tran.Commit();
                UIHelp.WriteOperateLog(PersonName, UserID, "培训点报名审核", string.Format("审核批号：{0}，审核结果：{2}，人数：{1}人。", esob.CheckCode, count, RadioButtonListCheckResult.SelectedValue));
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "培训点考试报名审核失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, string.Format("考试报名审核成功！审核批号：{0}，审核结果：{2}，人数：{1}人。", esob.CheckCode, count, RadioButtonListCheckResult.SelectedValue));
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();
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
        //    UIHelp.WriteOperateLog(PersonName, UserID, "删除报名", string.Format("审核时删除报名。报名批次号：{0}，报名人：{1}，详情见“查看报名情况/查看删除历史”。",
        //     RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SignUpCode"].ToString(),
        //     e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("WorkerName").OrderIndex].Text));
        //    UIHelp.layerAlert(Page, "删除成功！",6,3000);
        //}

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
                    return "~/Images/tup.gif";
            }
        }

    }
}