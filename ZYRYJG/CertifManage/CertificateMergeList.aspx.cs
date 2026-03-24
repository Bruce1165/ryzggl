using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

namespace ZYRYJG.CertifManage
{
    public partial class CertificateMergeList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (PersonType)
                {
                    case 2://考生
                        break;
                    case 3://企业
                        RadComboBoxApplyStatus.Items.FindItemByValue("待单位确认").Selected = true;
                        break;
                    default:
                        RadComboBoxApplyStatus.Items.FindItemByValue("已申请").Selected = true;
                        break;

                }
                ButtonSearch_Click(sender, e);
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            ClearGridSelectedKeys(RadGrid1);
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            switch (PersonType)
            {
                case 2://考生
                    q.Add(string.Format("(WorkerCertificateCode='{0}' )", WorkerCertificateCode));
                    break;
                case 3://企业
                    q.Add(string.Format("(UnitCode='{0}' )", ZZJGDM));
                    break;
                default:
                    break;

            }


            //身份证号码
            if (RadTxtSFZHM.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode = '{0}'", RadTxtSFZHM.Text.Trim()));
            }

            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName = '{0}'", RadTxtWorkerName.Text.Trim()));
            }
            //证书号码
            if (RadTextBoxCertificateCode.Text != "")
            {
                q.Add(string.Format("(CertificateCode1 = '{0}' or CertificateCode2 = '{0}')", RadTextBoxCertificateCode.Text.Trim()));
            }

            if (RadComboBoxApplyStatus.SelectedValue != "")
            {
                q.Add(string.Format("ApplyStatus = '{0}'", RadComboBoxApplyStatus.SelectedValue));
            }

            if (PersonType == 3)
            {
                if (RadComboBoxApplyStatus.SelectedValue == EnumManager.CertificateMergeStatus.WaitUnitCheck)
                    ButtonCheck.Visible = true;
                else
                    ButtonCheck.Visible = false;
            }

            if (PersonType != 2 && PersonType != 3)
            {
                if (RadComboBoxApplyStatus.SelectedValue == EnumManager.CertificateMergeStatus.Applyed)
                    ButtonCheck.Visible = true;
                else
                    ButtonCheck.Visible = false;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //批量审核        
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
            if (!IsGridSelected(RadGrid1))
            {
                UIHelp.layerAlert(Page, "你还没有勾选要审核的数据！");
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
                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }
            List<string> printList = new List<string>();//待打印证书ID集合              
            DataTable dt = CertificateMergeDAL.GetList(0, int.MaxValue - 1, filterString, "ApplyID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                printList.Add(dt.Rows[i]["ApplyID"].ToString());
            }

            Session["CertificateMergeList"] = printList;
            Session["CertificateMergeTable"] = dt;
            Response.Redirect("CertificateMergeApply.aspx", false);
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "ApplyID");
        }


    }
}
