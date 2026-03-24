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
namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterCode : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager1.AjaxSettings.AddAjaxSetting((this.Master.FindControl("RadScriptManager1") as ScriptManager), RadGrid1);
            if (!this.IsPostBack)
            {
                PostSelect1.PostTypeID = Request["o"];//岗位类别ID
                PostSelect1.LockPostTypeID();
                LabelPostType.Text = UIHelp.GetPostTypeNameByID(Request["o"]);

                ButtonSearch_Click(sender, e);
            }
            //else if (Request.Params["__EVENTTARGET"] == BtnBH.UniqueID)
            //{
            //    //更新状态
            //    string startTime = HiddenFieldStartTime.Value;
            //    string endTime = HiddenFieldEndTime.Value;
            //    string ConferDate = HiddenFieldConferDate.Value;
            //    ZSBH(startTime, endTime, ConferDate, sender, e);

            //    //刷新grid
            //    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "search", string.Format("document.getElementById('{0}').click();", ButtonSearch.ClientID), true);
            //}
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

            if (rdtxtZJHM.Text.Trim() != "")   //原证书编号
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }

            if (rdtxtQYMC.Text.Trim() != "")   //现单位名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (rdtxtApplyCode.Text.Trim() != "")   //编号批次号
            {
                q.Add(string.Format("ConfrimCode like '%{0}%'", rdtxtApplyCode.Text.Trim()));
            }

            if (RadioButtonListApplyStatus.SelectedItem.Value == "未编号")
            {
                q.Add(string.Format("ApplyStatus = '{0}' ", EnumManager.CertificateEnterStatus.Checked));//已审核
                ButtonCode.Attributes.Remove("disabled");
                ButtonCode.CssClass = "bt_large" ;

                ButtonOutput.Attributes["disabled"] = "true";
                ButtonOutput.CssClass = "bt_large btn_no";
                RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Visible = true;
            }
            else
            {
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateEnterStatus.Decided));//已决定
                ButtonCode.Attributes["disabled"] = "true";
                ButtonCode.CssClass = "bt_large btn_no";

                ButtonOutput.Attributes.Remove("disabled");
                ButtonOutput.CssClass = "bt_large";
                RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Visible = false;
            }

            if (RadDatePickerCheckDateStart.SelectedDate.HasValue)//审核时间段起始
            {
                q.Add(string.Format("[CHECKDATE]>='{0}'", RadDatePickerCheckDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerCheckDateEnd.SelectedDate.HasValue)//审核时间段截止
            {
                q.Add(string.Format("[CHECKDATE]<'{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
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

//        //编号
//        protected void ZSBH(string startTime, string endTime, string ConferDate, object sender, EventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
//            if (IsGridSelected(RadGrid1) == false)
//            {
//                UIHelp.layerAlert(Page, "至少选择一条数据！");
//                return;
//            }

//            string _ConfrimCode = UIHelp.GetNextBatchNumber("JJBH"); //进京编号批次号
//            string filterString = "";//过滤条件

//            if (GetGridIfCheckAll(RadGrid1) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
//                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
//                else//包含
//                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
//            }

//            int addItemCount = 0;//增项个数统计
//            DBHelper dbhelper = new DBHelper();
//            DbTransaction dtr = dbhelper.BeginTransaction();
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();
//            try
//            {

//                //DataTable dt = CertificateEnterApplyDAL.GetList(0, int.MaxValue - 1, filterString, "");
//                DataTable dt = CommonDAL.GetDataTable(
//                    string.Format(@"select  CertificateEnterApply.*, N1.POSTNAME AS POSTTYPENAME,  N2.POSTNAME           
//                                     from DBO.CertificateEnterApply
//                                     LEFT JOIN DBO.POSTINFO N1 ON N1.POSTTYPE = '1' AND CertificateEnterApply.POSTTYPEID = N1.POSTID
//                                       LEFT JOIN DBO.POSTINFO N2 ON N2.POSTTYPE = '2' AND CertificateEnterApply.POSTID = N2.POSTID
//                                    where 1=1 {0}", filterString));
//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    //读取进京申请
//                    CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(dt.Rows[i]["ApplyID"]));

//                    //读取岗位工种
//                    PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(dtr, Convert.ToInt32(dt.Rows[i]["PostID"]));

//                    //检查证书增项
//                    DataTable dtCertificate = null;
//                    if (PostSelect1.PostTypeID == "3")//造价员
//                    {
//                        //查看是否有造价员其它工种证书
//                        dtCertificate = CertificateDAL.GetList(dtr, 0, 1,
//                            string.Format(" and WorkerCertificateCode='{0}' and PostTypeID={1} and PostID <> {2} and VALIDENDDATE >= getdate() and [STATUS] <>'注销' and [STATUS] <>'离京变更'"
//                            , dt.Rows[i]["WorkerCertificateCode"].ToString()
//                            , dt.Rows[i]["PostTypeID"].ToString()
//                        , dt.Rows[i]["PostID"].ToString())
//                        , "CertificateID");
//                    }
//                    CertificateOB ceron = null;//证书

//                    if (dtCertificate != null && dtCertificate.Rows.Count > 0)//增项
//                    {
//                        addItemCount++;
//                        ceron = CertificateDAL.GetObject(dtr, Convert.ToInt64(dtCertificate.Rows[0]["CertificateID"]));

//                        //添加增项记录
//                        CertificateAddItemOB _CertificateAddItemOB = new CertificateAddItemOB();
//                        _CertificateAddItemOB.CertificateID = ceron.CertificateID;//证书ID
//                        _CertificateAddItemOB.PostTypeID = Convert.ToInt32(dt.Rows[i]["PostTypeID"]);//岗位
//                        _CertificateAddItemOB.PostID = Convert.ToInt32(dt.Rows[i]["PostID"]);//工种
//                        CertificateAddItemDAL.Insert(dtr, _CertificateAddItemOB);

//                        //更新证书表增项名称
//                        ceron.AddItemName = dtCertificate.Rows[0]["PostName"].ToString() + CertificateAddItemDAL.GetAddItemNameString(dtr, ceron.CertificateID.Value);
//                        ceron.ModifyTime = DateTime.Now;//修改时间
//                        ceron.ModifyPersonID = PersonID;//修改人
//                        CertificateDAL.Update(dtr, ceron);
//                    }
//                    else//发证
//                    {
//                        //创建证书
//                        ceron = new CertificateOB();
//                        ceron.ExamPlanID = EnumManager.CertificateExamPlanID.CertificateEnter;//考试计划ID
//                        ceron.WorkerID = Convert.ToInt64(dt.Rows[i]["WorkerID"]);//从业人员ID
//                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, dtr);//证书编号                         
//                        ceron.WorkerName = dt.Rows[i]["WorkerName"].ToString();//姓名
//                        ceron.WorkerCertificateCode = dt.Rows[i]["WorkerCertificateCode"].ToString();//证件号码
//                        ceron.PostTypeID = Convert.ToInt32(dt.Rows[i]["PostTypeID"]);//岗位
//                        ceron.PostID = Convert.ToInt32(dt.Rows[i]["PostID"]);//工种
//                        ceron.PostTypeName = Convert.ToString(dt.Rows[i]["PostTypeName"]);//岗位
//                        ceron.PostName = Convert.ToString(dt.Rows[i]["PostName"]);//工种
//                        ceron.Sex = dt.Rows[i]["Sex"] == DBNull.Value ? "" : dt.Rows[i]["Sex"].ToString();//性别
//                        ceron.Birthday = dt.Rows[i]["Birthday"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[i]["Birthday"]);//出生日期
//                        ceron.UnitName = dt.Rows[i]["UnitName"].ToString();//工作单位
//                        ceron.UnitCode = dt.Rows[i]["UnitCode"].ToString();//组织机构代码
//                        ceron.ConferDate = Convert.ToDateTime(ConferDate);//发证日期
//                        ceron.ConferUnit = "北京市住建委";
//                        ceron.ValidStartDate = Convert.ToDateTime(startTime);//证书有效期起
//                        ceron.ValidEndDate = Convert.ToDateTime(endTime);//证书有效期止
//                        ceron.CreatePersonID = PersonID;//创建人ID
//                        ceron.CreateTime = DateTime.Now;//创建时间
//                        ceron.Status = EnumManager.CertificateUpdateType.EnterWaitCheck;//进京待审批   
//                        if (dt.Rows[i]["Job"] == DBNull.Value)
//                        {
//                            ceron.Job = dt.Rows[i]["Job"].ToString();//职务
//                        }
//                        if (dt.Rows[i]["SkillLevel"] == DBNull.Value)
//                        {
//                            ceron.SkillLevel = dt.Rows[i]["SkillLevel"].ToString();//技术职称
//                        } 
//                        CertificateDAL.Insert(dtr, ceron);

//                        sb.Append("、").Append(ceron.CertificateCode);

//                        if (string.IsNullOrEmpty(_CertificateEnterApplyOB.AddPostID) == false)//进京同时有增项
//                        {
//                            //添加增项记录
//                            CertificateAddItemOB _CertificateAddItemOB = new CertificateAddItemOB();
//                            _CertificateAddItemOB.CertificateID = ceron.CertificateID;//证书ID
//                            _CertificateAddItemOB.PostTypeID = ceron.PostTypeID;//岗位类别
//                            _CertificateAddItemOB.PostID = Convert.ToInt32(_CertificateEnterApplyOB.AddPostID);//工种
//                            _CertificateAddItemOB.CreatePerson = PersonName;
//                            _CertificateAddItemOB.CreateTime = DateTime.Now;
//                            _CertificateAddItemOB.CaseStatus = "已办结";
//                            CertificateAddItemDAL.Insert(dtr, _CertificateAddItemOB);

//                            //更新证书表增项名称
//                            ceron.AddItemName = PostInfoDAL.GetObject(Convert.ToInt32(_CertificateEnterApplyOB.PostID)).PostName + CertificateAddItemDAL.GetAddItemNameString(dtr, ceron.CertificateID.Value);
//                            CertificateDAL.Update(dtr, ceron);
//                        }
//                    }

//                    //更新进京申请
//                    _CertificateEnterApplyOB.ConfrimDate = DateTime.Now;   //编号时间
//                    _CertificateEnterApplyOB.ConfrimResult = "证书编号";     //编号结论
//                    _CertificateEnterApplyOB.ConfrimMan = PersonName;    //编号人
//                    _CertificateEnterApplyOB.ConfrimCode = _ConfrimCode;//编号批次
//                    _CertificateEnterApplyOB.ApplyStatus = EnumManager.CertificateEnterStatus.Decided;//已编号状态
//                    _CertificateEnterApplyOB.CertificateID = ceron.CertificateID;//新证书ID
//                    CertificateEnterApplyDAL.Update(dtr, _CertificateEnterApplyOB);
//                }

//                dtr.Commit();

//                if (sb.Length > 0) sb.Remove(0, 1);
//            }
//            catch (Exception ex)
//            {
//                dtr.Rollback();
//                UIHelp.WriteErrorLog(Page, "进京证书编号失败！", ex);
//                return;
//            }

//            ClearGridSelectedKeys(RadGrid1);

//            //按批次号查询，为导出做准备
//            rdtxtApplyCode.Text = _ConfrimCode;
//            RadioButtonListApplyStatus.Items.FindByValue("未编号").Selected = false;
//            RadioButtonListApplyStatus.Items.FindByValue("已编号").Selected = true;
//            ButtonSearch_Click(sender, e);

//            string tip = "";
//            if (addItemCount > 0) tip = string.Format("其中有{0}本证书为增项，没有进行编号。", addItemCount.ToString());

//            UIHelp.WriteOperateLog(PersonName, UserID, "进京证书编号", string.Format("批次号：{0}。{1}证书编号：{2}。", _ConfrimCode, tip, sb.ToString()));

//            UIHelp.layerAlert(Page, string.Format("编号成功！{1}已列出本次批次号为“{0}”的编号结果，你可以继续导出列表，供领导审阅签字。", _ConfrimCode, tip));
//        }

        //导出审批列表
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.Rebind();

            RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Visible = false;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = false;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "NewCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "WorkerName")
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

        //证书编号
        protected void ButtonCode_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ApplyID");
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string _ConfrimCode = UIHelp.GetNextBatchNumber("JJBH"); //进京编号批次号
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

            int addItemCount = 0;//增项个数统计
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DateTime doTime = DateTime.Now;
            try
            {

                //DataTable dt = CertificateEnterApplyDAL.GetList(0, int.MaxValue - 1, filterString, "");
                DataTable dt = CommonDAL.GetDataTable(
                    string.Format(@"select  CertificateEnterApply.*, N1.POSTNAME AS POSTTYPENAME,  N2.POSTNAME           
                                     from DBO.CertificateEnterApply
                                     LEFT JOIN DBO.POSTINFO N1 ON N1.POSTTYPE = '1' AND CertificateEnterApply.POSTTYPEID = N1.POSTID
                                       LEFT JOIN DBO.POSTINFO N2 ON N2.POSTTYPE = '2' AND CertificateEnterApply.POSTID = N2.POSTID
                                    where 1=1 {0}", filterString));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //读取进京申请
                    CertificateEnterApplyOB _CertificateEnterApplyOB = CertificateEnterApplyDAL.GetObject(Convert.ToInt64(dt.Rows[i]["ApplyID"]));

                    //读取岗位工种
                    PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(dtr, Convert.ToInt32(dt.Rows[i]["PostID"]));

                    ////检查证书增项
                    //DataTable dtCertificate = null;
                    //if (PostSelect1.PostTypeID == "3")//造价员
                    //{
                    //    //查看是否有造价员其它工种证书
                    //    dtCertificate = CertificateDAL.GetList(dtr, 0, 1,
                    //        string.Format(" and WorkerCertificateCode='{0}' and PostTypeID={1} and PostID <> {2} and VALIDENDDATE >= getdate() and [STATUS] <>'注销' and [STATUS] <>'离京变更'"
                    //        , dt.Rows[i]["WorkerCertificateCode"].ToString()
                    //        , dt.Rows[i]["PostTypeID"].ToString()
                    //    , dt.Rows[i]["PostID"].ToString())
                    //    , "CertificateID");
                    //}
                    CertificateOB ceron = null;//证书

                    //if (dtCertificate != null && dtCertificate.Rows.Count > 0)//增项
                    //{
                    //    addItemCount++;
                    //    ceron = CertificateDAL.GetObject(dtr, Convert.ToInt64(dtCertificate.Rows[0]["CertificateID"]));

                    //    //添加增项记录
                    //    CertificateAddItemOB _CertificateAddItemOB = new CertificateAddItemOB();
                    //    _CertificateAddItemOB.CertificateID = ceron.CertificateID;//证书ID
                    //    _CertificateAddItemOB.PostTypeID = Convert.ToInt32(dt.Rows[i]["PostTypeID"]);//岗位
                    //    _CertificateAddItemOB.PostID = Convert.ToInt32(dt.Rows[i]["PostID"]);//工种
                    //    CertificateAddItemDAL.Insert(dtr, _CertificateAddItemOB);

                    //    //更新证书表增项名称
                    //    ceron.AddItemName = dtCertificate.Rows[0]["PostName"].ToString() + CertificateAddItemDAL.GetAddItemNameString(dtr, ceron.CertificateID.Value);
                    //    ceron.ModifyTime = DateTime.Now;//修改时间
                    //    ceron.ModifyPersonID = PersonID;//修改人
                    //    CertificateDAL.Update(dtr, ceron);
                    //}
                    //else//发证
                    //{
                        //创建证书
                        ceron = new CertificateOB();
                        ceron.ExamPlanID = EnumManager.CertificateExamPlanID.CertificateEnter;//考试计划ID
                        ceron.WorkerID = Convert.ToInt64(dt.Rows[i]["WorkerID"]);//从业人员ID
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, dtr);//证书编号                         
                        ceron.WorkerName = dt.Rows[i]["WorkerName"].ToString();//姓名
                        ceron.WorkerCertificateCode = dt.Rows[i]["WorkerCertificateCode"].ToString();//证件号码
                        ceron.PostTypeID = Convert.ToInt32(dt.Rows[i]["PostTypeID"]);//岗位
                        ceron.PostID = Convert.ToInt32(dt.Rows[i]["PostID"]);//工种
                        ceron.PostTypeName = Convert.ToString(dt.Rows[i]["PostTypeName"]);//岗位
                        ceron.PostName = Convert.ToString(dt.Rows[i]["PostName"]);//工种
                        ceron.Sex = dt.Rows[i]["Sex"] == DBNull.Value ? "" : dt.Rows[i]["Sex"].ToString();//性别
                        ceron.Birthday = dt.Rows[i]["Birthday"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[i]["Birthday"]);//出生日期
                        ceron.UnitName = dt.Rows[i]["UnitName"].ToString();//工作单位
                        ceron.UnitCode = dt.Rows[i]["UnitCode"].ToString();//组织机构代码
                        //ceron.ConferDate = Convert.ToDateTime(ConferDate);//发证日期
                        ceron.ConferDate = Convert.ToDateTime(doTime.ToString("yyyy-MM-dd"));//首次发证日期（今天）
                        ceron.ConferUnit = "北京市住建委";
                        //ceron.ValidStartDate = Convert.ToDateTime(startTime);//证书有效期起
                        ceron.ValidStartDate = ceron.ConferDate;//证书有效期起（今天）
                        ceron.ValidEndDate = Convert.ToDateTime(dt.Rows[i]["ValidEndDate"]);//证书有效期止（与原外省证书有效期截止保持一致）
                        ceron.CreatePersonID = PersonID;//创建人ID
                        ceron.CreateTime = doTime;//创建时间
                        ceron.ModifyPersonID = PersonID;
                        ceron.ModifyTime = ceron.CreateTime;
                        ceron.Status = EnumManager.CertificateUpdateType.EnterWaitCheck;//进京待审批   
                        if (dt.Rows[i]["Job"] != DBNull.Value)
                        {
                            ceron.Job = dt.Rows[i]["Job"].ToString();//职务
                        }
                        if (dt.Rows[i]["SkillLevel"] != DBNull.Value)
                        {
                            ceron.SkillLevel = dt.Rows[i]["SkillLevel"].ToString();//技术职称
                        }

                        ceron.ValidEndDate = GetValidEndDateWithAge(ceron, ceron.PostID == 147 ? (ceron.Job == "法定代表人"?1:0) : 0);//考虑年龄显示计算实际有效期

                        CertificateDAL.Insert(dtr, ceron);

                        sb.Append("、").Append(ceron.CertificateCode);

                        //if (string.IsNullOrEmpty(_CertificateEnterApplyOB.AddPostID) == false)//进京同时有增项
                        //{
                        //    //添加增项记录
                        //    CertificateAddItemOB _CertificateAddItemOB = new CertificateAddItemOB();
                        //    _CertificateAddItemOB.CertificateID = ceron.CertificateID;//证书ID
                        //    _CertificateAddItemOB.PostTypeID = ceron.PostTypeID;//岗位类别
                        //    _CertificateAddItemOB.PostID = Convert.ToInt32(_CertificateEnterApplyOB.AddPostID);//工种
                        //    _CertificateAddItemOB.CreatePerson = PersonName;
                        //    _CertificateAddItemOB.CreateTime = doTime;
                        //    _CertificateAddItemOB.CaseStatus = "已办结";
                        //    CertificateAddItemDAL.Insert(dtr, _CertificateAddItemOB);

                        //    //更新证书表增项名称
                        //    ceron.AddItemName = PostInfoDAL.GetObject(Convert.ToInt32(_CertificateEnterApplyOB.PostID)).PostName + CertificateAddItemDAL.GetAddItemNameString(dtr, ceron.CertificateID.Value);
                        //    CertificateDAL.Update(dtr, ceron);
                        //}
                    //}

                    //更新进京申请
                    _CertificateEnterApplyOB.ConfrimDate = doTime;   //编号时间
                    _CertificateEnterApplyOB.ConfrimResult = "证书编号";     //编号结论
                    _CertificateEnterApplyOB.ConfrimMan = PersonName;    //编号人
                    _CertificateEnterApplyOB.ConfrimCode = _ConfrimCode;//编号批次
                    _CertificateEnterApplyOB.ApplyStatus = EnumManager.CertificateEnterStatus.Decided;//已编号状态
                    _CertificateEnterApplyOB.CertificateID = ceron.CertificateID;//新证书ID
                    CertificateEnterApplyDAL.Update(dtr, _CertificateEnterApplyOB);
                }

                dtr.Commit();

                if (sb.Length > 0) sb.Remove(0, 1);
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "进京证书编号失败！", ex);
                return;
            }

            ClearGridSelectedKeys(RadGrid1);

            //按批次号查询，为导出做准备
            rdtxtApplyCode.Text = _ConfrimCode;
            RadioButtonListApplyStatus.Items.FindByValue("未编号").Selected = false;
            RadioButtonListApplyStatus.Items.FindByValue("已编号").Selected = true;
            ButtonSearch_Click(sender, e);

            string tip = "";
            if (addItemCount > 0) tip = string.Format("其中有{0}本证书为增项，没有进行编号。", addItemCount.ToString());

            UIHelp.WriteOperateLog(PersonName, UserID, "进京证书编号", string.Format("批次号：{0}。{1}证书编号：{2}。", _ConfrimCode, tip, sb.ToString()));

            UIHelp.layerAlert(Page, string.Format("编号成功！{1}已列出本次批次号为“{0}”的编号结果，你可以继续导出列表，供领导审阅签字。", _ConfrimCode, tip));
            ButtonSearch_Click(sender, e);
        }

        /// <summary>
        /// 获取综合年龄限制后的证书实际有效截止日期
        /// </summary>
        /// <param name="o">未考虑年龄显示的放号证书信息</param>
        /// <param name="IsFR">是否为法人（1：法人，0 or null：非法人）</param>
        /// <returns>实际证书有效截止日期</returns>
        private DateTime GetValidEndDateWithAge(CertificateOB o, int? IsFR)
        {
            DateTime rtn;
            switch (o.PostTypeID.Value)
            {
                case 1:
                    if ((o.PostID == 147 && IsFR.HasValue && IsFR == 1)//FR A
                        || o.PostID == 148 //B
                        )
                    {
                        rtn = o.ValidEndDate.Value;
                    }
                    else //Not FR A、C1、C2、C3
                    {
                        if (o.Sex == "男")
                        {
                            if (o.Birthday.Value.AddYears(60) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(60);
                        }
                        else//女
                        {
                            if (o.Birthday.Value.AddYears(55) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(55);
                        }
                    }
                    break;
                case 2:
                    if (o.Sex == "男")
                    {
                        if (o.Birthday.Value.AddYears(60) >= o.ValidEndDate.Value)
                            rtn = o.ValidEndDate.Value;
                        else
                            rtn = o.Birthday.Value.AddYears(60);
                    }
                    else//女
                    {
                        if (o.Birthday.Value.AddYears(50) >= o.ValidEndDate.Value)
                            rtn = o.ValidEndDate.Value;
                        else
                            rtn = o.Birthday.Value.AddYears(50);
                    }
                    break;
                default:
                    rtn = o.ValidEndDate.Value;
                    break;
            }
            return rtn;
        }
    }
}