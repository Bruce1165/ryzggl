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

namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterCheck : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {

                    PostSelect1.PostTypeID = Request["o"];//岗位类别ID
                    PostSelect1.LockPostTypeID();
                    LabelPostType.Text = UIHelp.GetPostTypeNameByID(Request["o"]);

                    ButtonSearch_Click(sender, e);
                }
            }
        }

        //格式化工种，有增项的需要显示
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AddPostID"].ToString() == "9")
                    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text = "安装,增土建";
                else if (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AddPostID"].ToString() == "12")
                    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text = "土建,增安装";
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
            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));     //岗位类别
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect1.PostID));               //岗位工种

            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }

            if (rdtxtCertificateCode.Text.Trim() != "")  //证书编码
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", rdtxtCertificateCode.Text.Trim()));
            }

            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }

            if (rdtxtQYMC.Text.Trim() != "")   //现单位名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (rdtxtApplyCode.Text.Trim() != "")   //申请批次号
            {
                q.Add(string.Format("ApplyCode like '%{0}%'", rdtxtApplyCode.Text.Trim()));
            }

            if (RadioButtonListApplyStatus.SelectedItem.Value == "已受理")
            {
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateEnterStatus.Accepted));   //已受理
                TableJWCheck.Visible = true;
                LabelTitle.Text = "待审核的证书进京申请列表";
            }
            else
            {
                q.Add(string.Format("ApplyStatus not in('{0}','{1}')", EnumManager.CertificateEnterStatus.Applyed, EnumManager.CertificateEnterStatus.Accepted));   //审核过的
                TableJWCheck.Visible = false;
                LabelTitle.Text = "已审核的证书进京申请列表";
            }
            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//审核时间段起始
            {
                q.Add(string.Format("CheckDate >='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//审核时间段截止
            {
                q.Add(string.Format("CheckDate <'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "ApplyID");
        }

        //审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string _CheckCode = UIHelp.GetNextBatchNumber("JJSH"); //变更审核编批号
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

            CertificateEnterApplyOB _CertificateEnterApplyOB = new CertificateEnterApplyOB();
            _CertificateEnterApplyOB.CheckDate = DateTime.Now;   //审核时间
            _CertificateEnterApplyOB.CheckResult = (RadioButtonListJWCheck.SelectedValue == "通过" ? "通过" : TextBoxCheckResult.Text);  //审核结论
            _CertificateEnterApplyOB.CheckMan = PersonName;    //审核人
            _CertificateEnterApplyOB.CheckCode = _CheckCode;//审核编批号
            _CertificateEnterApplyOB.ApplyStatus = (RadioButtonListJWCheck.SelectedValue == "通过" ? EnumManager.CertificateEnterStatus.Checked : EnumManager.CertificateEnterStatus.SendBack);//状态

            try
            {
                CertificateEnterApplyDAL.Check(_CertificateEnterApplyOB, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "证书进京审核失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量审核证书进京", string.Format("处理结果：审核通过，审核批次号：{0}。状态：{1}，意见：{2}", _CheckCode, _CertificateEnterApplyOB.ApplyStatus, _CertificateEnterApplyOB.CheckResult));

            UIHelp.layerAlert(Page, "证书进京申请审核成功！审核批次号：" + _CheckCode,6,3000);
            ClearGridSelectedKeys(RadGrid1);
            ButtonSearch_Click(sender, e);
        }

        //删除无效申请
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 _ApplyID = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"]);  
            try
            {
                CertificateEnterApplyDAL.Delete(_ApplyID);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除证书进京申请信息失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "审核证书进京", string.Format("处理结果：删除。证书编号：{0}。"
            , e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("CertificateCode").OrderIndex].Text));

            UIHelp.layerAlert(Page, "删除成功！",6,3000);
        }
    }
}
