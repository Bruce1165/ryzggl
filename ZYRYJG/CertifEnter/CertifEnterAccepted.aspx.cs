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
    public partial class CertifEnterAccepted : BasePage
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

                    if (string.IsNullOrEmpty(Request["code"]) == false)//受理批次号
                    {
                        ViewState["GetCode"] = Request["code"];
                    }

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
            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));      //岗位类别
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect1.PostID));               //岗位工种

            if (ViewState["GetCode"] != null)//受理通知单
            {
                q.Add(string.Format("GetCode ='{0}'", ViewState["GetCode"].ToString()));
                DivSearch.Visible = false;
                ButtonAccepte.Visible = false;
               
                ButtonOutput.Visible = true;
                ButtonReturn.Visible = true;
                LabelTitle.Text = string.Format("受理通知单，受理批次号：{0}",ViewState["GetCode"].ToString());
                RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Display = false;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("GetCode").Display = false;
                TableJWAccept.Visible = false;           
            }
            else//待初审
            {
                
                DivSearch.Visible = true;
              
                ButtonOutput.Visible = false;
                ButtonReturn.Visible = false;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Display = true;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("GetCode").Display = true;

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

                if (RadioButtonListApplyStatus.SelectedItem.Value == "已申请")
                {
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateEnterStatus.Applyed));   //已申请
                    ButtonAccepte.Visible = true;
                    LabelTitle.Text = "";
                    TableJWAccept.Visible = true;  
                }
                else
                {
                    q.Add(string.Format("ApplyStatus <> '{0}'", EnumManager.CertificateEnterStatus.Applyed));   //受理过的
                    ButtonAccepte.Visible = false;                  
                    LabelTitle.Text = "";
                    TableJWAccept.Visible = false;  
                }

                if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
                {
                    q.Add(string.Format("AcceptDate >='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                }
                if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
                {
                    q.Add(string.Format("AcceptDate <'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                }

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

        //受理
        protected void ButtonAccepte_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }
            if (isSameUnitCode() == false)
            {
                UIHelp.layerAlert(Page, "您勾选的数据中“现聘用单位”不是同一家单位，不能同时受理，请新查询过滤！");
                return;
            }

            string _GetCode = UIHelp.GetNextBatchNumber("JJSL"); //进京受理编批号
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

            ////启用质安网检查后取消注释
            //if (RadioButtonListJWAccept.SelectedValue == "通过")
            //{
            //    if (GetGridIfCheckAll(RadGrid1) == true)//全选
            //    {
            //        filterString = string.Format(" {0} and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
            //    }
            //    else
            //    {
            //        if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
            //            filterString = string.Format(" {0} and ApplyID not in({1})  and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //        else//包含
            //            filterString = string.Format(" {0} and ApplyID in({1})  and ZACheckResult=1", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
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
            //            filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //        else//包含
            //            filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //    }
            //}

            CertificateEnterApplyOB _CertificateEnterApplyOB = new CertificateEnterApplyOB();
            _CertificateEnterApplyOB.AcceptDate = DateTime.Now;   //受理时间
            _CertificateEnterApplyOB.GetResult = (RadioButtonListJWAccept.SelectedValue == "通过" ? "通过" : TextBoxGetResult.Text);     //受理结论
            _CertificateEnterApplyOB.GetMan = PersonName;    //受理人
            _CertificateEnterApplyOB.GetCode = _GetCode;//受理编批号
            _CertificateEnterApplyOB.ApplyStatus = (RadioButtonListJWAccept.SelectedValue == "通过" ? EnumManager.CertificateEnterStatus.Accepted : EnumManager.CertificateEnterStatus.SendBack);//状态

            try
            {
                CertificateEnterApplyDAL.Accepted(_CertificateEnterApplyOB, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "进京受理失败！", ex);
                return;
            }

            ViewState["GetCode"] = _GetCode;

            UIHelp.WriteOperateLog(PersonName, UserID, "受理证书进京", string.Format("处理结果：受理，受理批次号：{0}。", _GetCode));

            UIHelp.layerAlert(Page, "进京受理成功！受理批次号：" + _GetCode,6,3000);
            ClearGridSelectedKeys(RadGrid1);
            ButtonSearch_Click(sender, e);
        }

        //判断是否是为同一家现聘用单位
        protected bool isSameUnitCode()
        {
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
            int count = CertificateEnterApplyDAL.SelectDistinctUnitCodeCount(filterString);

            if (count > 1)
                return false;
            else
                return true;
        }

        ////打印受理通知书
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/变更受理通知书.doc", "~/Template/变更受理通知书2.doc"
        //        , string.Format("~/UpLoad/CertifEnterApply/进京受理通知书_{0}.doc", ViewState["GetCode"].ToString())
        //        , GetPrintData());
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertifEnterApply/进京受理通知书_{0}.doc');", ViewState["GetCode"].ToString(), RootUrl), true);
        //}

        //导出受理通知书
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/变更受理通知书.doc", "~/Template/变更受理通知书2.doc"
                , string.Format("~/UpLoad/CertifEnterApply/进京受理通知书_{0}.doc", ViewState["GetCode"].ToString())
                , GetPrintData());
            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifEnterApply/进京受理通知书_{0}.doc", ViewState["GetCode"].ToString())));

            List<ResultUrl> url = new List<ResultUrl>();
          
            url.Add(new ResultUrl("进京受理通知书", string.Format("~/UpLoad/CertifEnterApply/进京受理通知书_{0}.doc", ViewState["GetCode"])));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            int pageNo = 0;//第几页
            int cellNo = 0;//当前页第几单元格
            string rowNo = "";//当前第几行
           
            DataTable dt = CertificateEnterApplyDAL.GetList(0, int.MaxValue - 1, string.Format(" and GetCode ='{0}'",ViewState["GetCode"].ToString()), "ApplyID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i < 5)//首页
                {
                    cellNo = i % 5 + 1;
                    if (i % 5 == 0)
                    {
                        printData = new Dictionary<string, string>();
                        list.Add(printData);
                        pageNo++;

                        printData.Add("Data_bggz", ViewState["GetCode"].ToString());//受理批次号
                        printData.Add("Data_NoticeDate", Convert.ToDateTime(dt.Rows[i]["AcceptDate"]).ToString("yyyy-MM-dd"));//日期
                        printData.Add("ChangeType", "证书进京");
                        printData.Add("PostTypeName",PostSelect1.RadComboBoxPostID.Items.FindItemByValue(dt.Rows[i]["PostID"].ToString()).Text);//岗位
                        printData.Add("NewUnitName", dt.Rows[i]["UnitName"].ToString());//现聘用单位名称
                        printData.Add("GetMan", dt.Rows[i]["GetMan"].ToString());//受理人
                        printData.Add("CertificateCount", dt.Rows.Count.ToString());//件数
                    }
                    rowNo = i.ToString();
                    printData.Add(string.Format("tr{0}", rowNo), dt.Rows[i]["RowNum"].ToString());//序号
                    printData.Add(string.Format("Name{0}", rowNo), dt.Rows[i]["WorkerName"].ToString());//姓名
                    printData.Add(string.Format("CertificateCode{0}", rowNo), dt.Rows[i]["CertificateCode"].ToString());//证书编号                
                    printData.Add(string.Format("UnitName{0}", rowNo), dt.Rows[i]["WorkerCertificateCode"].ToString());//证件号码
                }
                else//非首页
                {
                    cellNo = (i - 5) % 30 + 1;
                    if ((i - 5) % 30 == 0)
                    {
                        printData = new Dictionary<string, string>();
                        list.Add(printData);
                        pageNo++;

                        printData.Add("Data_bggz", ViewState["GetCode"].ToString());//批次号
                        printData.Add("Data_NoticeDate", Convert.ToDateTime(dt.Rows[i]["AcceptDate"]).ToString("yyyy-MM-dd"));//受理日期
                    }
                    rowNo = ((i - 5) % 30).ToString();
                    printData.Add(string.Format("tr{0}", rowNo), dt.Rows[i]["RowNum"].ToString());//序号
                    printData.Add(string.Format("Name{0}", rowNo), dt.Rows[i]["WorkerName"].ToString());//姓名
                    printData.Add(string.Format("CertificateCode{0}", rowNo), dt.Rows[i]["CertificateCode"].ToString());//证书编号                
                    printData.Add(string.Format("UnitName{0}", rowNo), dt.Rows[i]["WorkerCertificateCode"].ToString());//证件号码
                }
            }
            if (dt.Rows.Count < 5)//首页有空行
            {
                Utility.WordDelHelp.ReplaceLabelOfNullRow(list[0], "tr,Name,CertificateCode,UnitName", dt.Rows.Count, 4);
            }
            else if ((dt.Rows.Count - 5) % 30 != 0)//最后一页有空行
            {
                Utility.WordDelHelp.ReplaceLabelOfNullRow(list[list.Count - 1], "tr,Name,CertificateCode,UnitName", (dt.Rows.Count - 5) % 30, 29);
            }
            return list;
        }

        //返回
        protected void ButtonReturn_Click(object sender, EventArgs e)
        {
            ViewState["GetCode"] = null;
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
            UIHelp.WriteOperateLog(PersonName, UserID, "受理证书进京", string.Format("处理结果：删除。证书编号：{0}。"
            , e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("CertificateCode").OrderIndex].Text));

            UIHelp.layerAlert(Page, "删除成功！",6,3000);
        }
    }
}
