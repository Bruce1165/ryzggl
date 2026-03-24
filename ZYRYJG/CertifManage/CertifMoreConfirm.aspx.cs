using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;

namespace ZYRYJG.CertifManage
{
    public partial class CertifMoreConfirm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected string formatStauts(string status)
        {
            switch (status)
            {
                case "已审核":
                    return "未决定";
                case "已决定":
                    return "决定通过";
                case "退回修改":
                    return "退回修改";
                default:
                    return "";
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

            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }


            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }

            switch (RadioButtonListApplyStatus.SelectedItem.Value)
            {
                case ""://全部
                    q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or (ApplyStatus = '{2}' and ConfirmDate >'2000-01-01') )"
                        , EnumManager.CertificateMore.Checked
                        , EnumManager.CertificateMore.Decided
                        , EnumManager.CertificateMore.SendBack
                        ));
                    break;
                case EnumManager.CertificateMore.Decided://决定通过

                    q.Add(string.Format("ApplyStatus = '{0}' ", EnumManager.CertificateMore.Decided));
                    break;
                case EnumManager.CertificateMore.SendBack://退回修改
                    q.Add(string.Format("(ApplyStatus = '{0}' and ConfirmDate >'2000-01-01') ", EnumManager.CertificateMore.SendBack));
                    break;
                case EnumManager.CertificateMore.Checked://未决定

                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateMore.Checked));
                    break;
            }
           
            //if (RadioButtonListApplyStatus.SelectedItem.Value == "已申请")
            //{
            //    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateMore.Applyed));   //已申请
            //             LabelTitle.Text = "待受理申请列表";
            //}
            //else
            //{
            //    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateMore.Decided));   //受理过的

            //}

            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
            {
                q.Add(string.Format("CheckDate >='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
            {
                q.Add(string.Format("CheckDate <'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }


            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //批量决定
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");//更新选择状态
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGrid1) == true)//全选
            {
                filterString = string.Format("{0} and [ApplyStatus]='已审核'", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
                    filterString = string.Format(" {0} and [ApplyStatus]='已审核' and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and [ApplyStatus]='已审核' and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }
            List<string> printList = new List<string>();//待打印证书ID集合              
            DataTable dt = CertificateMoreDAL.GetList(0, int.MaxValue - 1, filterString, "ApplyID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                printList.Add(dt.Rows[i]["ApplyID"].ToString());
            }

            Session["CertificateMoreList"] = printList;
            Session["CertificateMoreTable"] = dt;
            Response.Redirect("CertifMoreCheck.aspx", false);
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");//更新选择状态
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "ApplyID");
        }
    }
}
