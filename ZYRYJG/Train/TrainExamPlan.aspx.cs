using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.Train
{
    public partial class TrainExamPlan : BasePage
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
            if (!IsPostBack)
            {
                TrainUnitMDL _TrainUnitMDL =TrainUnitDAL.GetObjectBySHTYXYDM(SHTJXYDM);
                if (_TrainUnitMDL !=null)
                {
                    ViewState["TrainUnitMDL"] =_TrainUnitMDL;
                }
               
                //根据培训点绑定可创建考试计划的工种
                Dictionary<string, string> postFilterString = new Dictionary<string, string>();
                postFilterString.Add("4000", string.Format("PostID in({0})", _TrainUnitMDL.PostSet));//
                PostSelect2.PostFilterString = postFilterString;

                //初始化岗位类别
                for (int i = 1; i < 6;i++ )
                {
                    PostSelect2.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                }
                PostSelect2.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                PostSelect2.PostTypeID = "4000";

                rbl.SelectedValue = "1";//未考试
                ButtonSearch_Click(sender, e);
            }
        }

        //绑定新增或编辑弹出层
        protected void RadGridExamPlan_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))//绑定编辑数据
            {
                PostSelect _PostSelect = (PostSelect)e.Item.FindControl("PostSelect1");
                _PostSelect.BindRadComboBoxPostType();

                if (e.Item.OwnerTableView.IsItemInserted)//new
                {
                    UIHelp.MovePopEditTable(e.Item as GridEditableItem, 10, 100);

                    for (int i = 1; i < 6; i++)
                    {
                        _PostSelect.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                    }
                    _PostSelect.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                    _PostSelect.PostTypeID = "4000";

                    PostSelect1_OnPostTypeSelectChange(_PostSelect, null);
                    
                    //添加考试计划时按岗位批量添加
                    _PostSelect.HideRadComboBoxPostID();//隐藏工种选择框

                    RadTextBox RadTextBoxExamPlanName = (RadTextBox)e.Item.FindControl("RadTextBoxExamPlanName");
                    RadTextBoxExamPlanName.Text = string.Format("{0}{1}建设职业技能岗位",curTrainUnit.TrainUnitName, DateTime.Now.ToString("yyyy年M月"));

                }
                else//update
                {
                    UIHelp.MovePopEditTable(e.Item as GridEditableItem, 10, 100);
                    //绑定计划
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    Int64 id = Convert.ToInt64(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]);
                    Button update = editedItem.Cells[0].FindControl("ButtonSave") as Button;

                    ExamPlanOB ob = ExamPlanDAL.GetObject(Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]));
                    _PostSelect.PostTypeID = ob.PostTypeID.ToString();
                    _PostSelect.PostID = ob.PostID.ToString();
                    UIHelp.SetData(editedItem, ob);
                    _PostSelect.Enabled = false;//修改是不允许改变岗位工种
                  
                    RadComboBox RadComboBoxPLANSKILLLEVEL = (RadComboBox)e.Item.FindControl("RadComboBoxPLANSKILLLEVEL");

                    //职业技能岗位考试计划有“技术等级”分类(除村镇建筑工匠、普工之外)
                    if (_PostSelect.PostTypeID == "4000")
                    {
                        RadComboBoxPLANSKILLLEVEL.Visible = true;
                    }
                    else
                    {
                        RadComboBoxPLANSKILLLEVEL.SelectedValue = "";
                        RadComboBoxPLANSKILLLEVEL.Visible = false;
                    }

                    ViewState["ExamPlanOB"] = ob;

                    //绑定科目
                    string selectPostID = _PostSelect.PostID == "" ? "0" : _PostSelect.PostID;
                    DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.PostInfo inner join dbo.ExamPlanSubject on dbo.ExamPlanSubject.ExamPlanID= " + ob.ExamPlanID.ToString() + " and dbo.PostInfo.PostID = dbo.ExamPlanSubject.PostID", "dbo.PostInfo.PostID as SubjectID,dbo.PostInfo.PostName,dbo.PostInfo.ExamFee as ExamFeeNormal,dbo.ExamPlanSubject.*", "", "ExamPlanSubjectID");
                    RadGrid RadGridSubject = editedItem.FindControl("RadGridExamPlanSubject") as RadGrid;
                    RadGridSubject.DataSource = dt;
                    RadGridSubject.DataBind();

                    SetExamPlanForUser(ob.ExamPlanID.Value, e.Item as GridEditableItem);//绑定可见用户               
                }
            }
        }

        //从界面获取可见考试计划用户对象集合
        private List<ExamPlanForUserOB> GetExamPlanForUserOBList(long ExamPlanID, GridEditableItem e)
        {
            RadListBox rlbIDCard = e.FindControl("RadListBoxIDCard") as RadListBox;
            List<ExamPlanForUserOB> list = new List<ExamPlanForUserOB>();
         
            //从业人员
            foreach (RadListBoxItem li in rlbIDCard.Items)
            {
                ExamPlanForUserOB ob = new ExamPlanForUserOB();
                ob.ExamPlanID = ExamPlanID;
                ob.UserType = "从业人员";
                ob.CertificateCode = li.Value;
                list.Add(ob);
            }

            return list;
        }

        //向界面绑定可见考试计划用户
        private void SetExamPlanForUser(long ExamPlanID, GridEditableItem e)
        {
            RadListBox rlbTrain = e.FindControl("RadListBoxTrainUnit") as RadListBox;
            RadListBox rlbUnitCode = e.FindControl("RadListBoxUnitCode") as RadListBox;
            RadListBox rlbIDCard = e.FindControl("RadListBoxIDCard") as RadListBox;
            Label lab = e.FindControl("LabelIDCardCount") as Label;

            DataTable dt = ExamPlanForUserDAL.GetList(0, int.MaxValue - 1, " and ExamPlanID=" + ExamPlanID.ToString(), "DataID");
            if (dt == null) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["UserType"].ToString())
                {
                    case "培训点":
                        RadListBoxItem li = rlbTrain.FindItemByValue(dt.Rows[i]["TrainUnitID"].ToString());
                        if (li != null) li.Checked = true;
                        break;
                    case "企业":
                        rlbUnitCode.Items.Add(new RadListBoxItem(dt.Rows[i]["UnitCode"].ToString(), dt.Rows[i]["UnitCode"].ToString()));
                        break;
                    case "从业人员":
                        rlbIDCard.Items.Add(new RadListBoxItem(dt.Rows[i]["CertificateCode"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));
                        break;
                }
            }

            ShowAddIDCardCount(lab, rlbIDCard);
        }

        //可见考试计划用户添加、删除
        protected void RadGridExamPlan_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridItem editedItem;
            if (e.Item is GridDataItem)
                editedItem = (e.Item as GridDataItem).EditFormItem as GridEditFormItem;
            else
                editedItem = e.Item;

            RadListBox rlbUnitCode = editedItem.FindControl("RadListBoxUnitCode") as RadListBox;
            RadTextBox rtbUnitCode = editedItem.FindControl("RadTextBoxUnitCode") as RadTextBox;
            RadListBox rlbIDCard = editedItem.FindControl("RadListBoxIDCard") as RadListBox;
            RadTextBox rtbIDCard = editedItem.FindControl("RadTextBoxIDCard") as RadTextBox;
            Label lab = editedItem.FindControl("LabelIDCardCount") as Label;
            switch (e.CommandName)
            {
                case "AddUnitCode"://添加可见组织机构代码
                    if (rtbUnitCode.Text.Trim() == "") return;
                    if (rlbUnitCode.FindItemByValue(rtbUnitCode.Text.Trim()) != null)
                    {
                        UIHelp.layerAlert(Page, "已经添加过了，不能重复添加！");
                        return;
                    }
                    if (UIHelp.UnitCodeCheck(this.Page, rtbUnitCode.Text.Trim()) == false)
                    {
                        UIHelp.layerAlert(Page, "组织机构代码不正确，无法添加！");
                        return;
                    }
                    rlbUnitCode.Items.Add(new RadListBoxItem(rtbUnitCode.Text.Trim(), rtbUnitCode.Text.Trim()));
                    rtbUnitCode.Text = "";
                    break;
                case "DeleteUnitCode"://删除可见组织机构代码
                    if (rlbUnitCode.SelectedItem == null)
                    {
                        UIHelp.layerAlert(Page, "请选中一个已添加的组织机构代码，再单击删除！");
                        return;
                    }
                    rlbUnitCode.Items.Remove(rlbUnitCode.SelectedItem);
                    break;
                case "AddIDCard"://添加可见证件号码
                    string[] txt = rtbIDCard.Text.Split('\n');
                    List<string> ListIDCard = new List<string>();
                    System.Text.StringBuilder er = new System.Text.StringBuilder();
                    foreach(string s in txt)
                    {
                        if (s.Trim() == "") continue;
                        if (rlbIDCard.FindItemByValue(s.Trim()) != null || ListIDCard.Contains(s.Trim())==true)
                        {                            
                            er.Append(string.Format("{0}已经添加过了，不能重复添加；<br />", s));
                            continue;
                        }
                        if (Utility.Check.isChinaIDCard(s.Trim()) == false)
                        {
                            er.Append(string.Format("{0}身份证号不正确，无法添加；<br />", s));
                            continue;
                        }
                        ListIDCard.Add(s.Trim());                        
                    }

                    if(er.Length>0)
                    {
                        UIHelp.layerAlertWithHtml(Page, string.Format("输入有误，无法正确添加。请保证每一行正确输入一个身份证号。<br />{0}", er));
                        return;
                    }

                    if (ListIDCard.Count>0)
                    { 
                        foreach(string s in ListIDCard)
                        {
                            rlbIDCard.Items.Add(new RadListBoxItem(s.Trim(),s.Trim()));
                        }
                    }
                    rtbIDCard.Text = "";
                    ShowAddIDCardCount(lab, rlbIDCard);
                    break;
                case "DeleteIDCard"://删除可见证件号码
                    if (rlbIDCard.SelectedItem == null)
                    {
                        UIHelp.layerAlert(Page, "请选中一个已添加的身份证号，再单击删除！");
                        return;
                    }
                    rlbIDCard.Items.Remove(rlbIDCard.SelectedItem);
                    ShowAddIDCardCount(lab, rlbIDCard);
                    break;
                case "ClearIDCard"://清空身份证
                    rlbIDCard.Items.Clear();
                    ShowAddIDCardCount(lab, rlbIDCard);
                    break;
            }
        }

        /// <summary>
        /// 显示已添加身份号总条数
        /// </summary>
        /// <param name="lab">显示统计Lable控件</param>
        /// <param name="list">显示列表ListBox控件</param>
        protected void ShowAddIDCardCount(Label lab,RadListBox list)
        {
            if (list.Items.Count == 0)
                lab.Text = "";
            else
                lab.Text = string.Format("已添加身份证号{0}条。", list.Items.Count);
        }

        //新增（按岗位类别批量创建）
        protected void RadGridExamPlan_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            PostSelect _PostSelect = (PostSelect)editedItem.FindControl("PostSelect1");
            RadDatePicker SignUpStartDate = editedItem.Cells[0].FindControl("RadDatePickerSignUpStartDate") as RadDatePicker;
            RadDatePicker SignUpEndDate = editedItem.Cells[0].FindControl("RadDatePickerSignUpEndDate") as RadDatePicker;
            RadDatePicker PickerExamCardSendStartDate = editedItem.Cells[0].FindControl("RadDatePickerExamCardSendStartDate") as RadDatePicker;
            RadDatePicker PickerExamCardSendEndDate = editedItem.Cells[0].FindControl("RadDatePickerExamCardSendEndDate") as RadDatePicker;
            RadDatePicker ExamStartDate = editedItem.Cells[0].FindControl("RadDatePickerExamStartDate") as RadDatePicker;
            RadDatePicker ExamEndDate = editedItem.Cells[0].FindControl("RadDatePickerExamEndDate") as RadDatePicker;
            RadDatePicker CheckEndDate = editedItem.Cells[0].FindControl("CheckEndDate") as RadDatePicker;
            RadGrid RadGridExamPlanSubject = e.Item.FindControl("RadGridExamPlanSubject") as RadGrid;
            RadDatePicker RadDatePickerStartCheckDate = e.Item.FindControl("RadDatePickerStartCheckDate") as RadDatePicker;
            RadDatePicker RadDatePickerLatestCheckDate = e.Item.FindControl("RadDatePickerLatestCheckDate") as RadDatePicker;
            CheckBoxList _CheckBoxListPostID = (CheckBoxList)editedItem.FindControl("CheckBoxListPostID");
            RadTextBox RadTextBoxExamPlanName = e.Item.FindControl("RadTextBoxExamPlanName") as RadTextBox;
            RadComboBox RadComboBoxPLANSKILLLEVEL = (RadComboBox)editedItem.FindControl("RadComboBoxPLANSKILLLEVEL");

            List<string> listPostID = new List<string>();
            System.Text.StringBuilder sbPostName = new System.Text.StringBuilder();
            foreach (ListItem item in _CheckBoxListPostID.Items)
            {
                if (item.Selected == true)
                {
                    listPostID.Add(item.Value);
                    sbPostName.Append("、").Append(item.Text);
                }
            }
            if (sbPostName.Length > 0)
            {
                sbPostName.Remove(0, 1);
            }

            #region -----规则校验----------
            if (_PostSelect.PostTypeID == "")
            {
                UIHelp.layerAlert(Page, "请选择岗位类别！");
                e.Canceled = true;
                return;
            }

            if (listPostID.Count == 0)
            {
                UIHelp.layerAlert(Page, "至少要选择一个要考试的岗位工种！");
                e.Canceled = true;
                return;
            }

            //职业技能岗位必须选择技术等级
            if (_PostSelect.PostTypeID == "4000" && RadComboBoxPLANSKILLLEVEL.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "必须选择一个技术等级！");
                e.Canceled = true;
                return;
            }

            if (SignUpStartDate.SelectedDate > SignUpEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试报名开始时间不应大于考试报名结束时间！");
                e.Canceled = true;
                return;
            }
            if (PickerExamCardSendStartDate.SelectedDate > PickerExamCardSendEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "准考证发放开始时间不应大于准考证发放截止时间！");
                e.Canceled = true;
                return;
            }
            if (ExamStartDate.SelectedDate > ExamEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试开始时间不应大于考试结束时间！");
                e.Canceled = true;
                return;
            }
            if (SignUpEndDate.SelectedDate > PickerExamCardSendStartDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试报名截止时间不应大于准考证发放开始时间！");
                e.Canceled = true;
                return;
            }
            if (PickerExamCardSendEndDate.SelectedDate > ExamStartDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "准考证发放截止时间不应大于考试开始时间！");
                e.Canceled = true;
                return;
            }
            if (RadDatePickerLatestCheckDate.SelectedDate < SignUpEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "报名截止时间不应大于审核截止日期！");
                e.Canceled = true;
                return;
            }

            if (RadDatePickerStartCheckDate.SelectedDate > RadDatePickerLatestCheckDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "审核开始时间不应大于审核截止时间！");
                e.Canceled = true;
                return;
            }
           
            Dictionary<string, DateTime[]> kemuExamTime = new Dictionary<string, DateTime[]>();//科目考试时间集合
            RadDateTimePicker rdp_start;
            RadDateTimePicker rdp_end;
            for (int i = 0; i < RadGridExamPlanSubject.MasterTableView.Items.Count; i++)
            {
                RadDatePicker pickerExamStartTime = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[1].FindControl("pickerExamStartTime") as RadDatePicker;  // editedItem.Cells[0].FindControl("pickerExamStartTime") as RadDatePicker;
                RadDatePicker pickerExamEndTime = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[2].FindControl("pickerExamEndTime") as RadDatePicker;

                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) > Convert.ToDateTime(Convert.ToDateTime(pickerExamEndTime.SelectedDate).ToString("yyyy-MM-dd")))
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不应大于结束时间！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (PickerExamCardSendEndDate.SelectedDate > Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")))
                {
                    UIHelp.layerAlert(Page, string.Format("准考证发放结束时间不应大于科目“{0}”考试开始时间！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) > ExamEndDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamEndTime.SelectedDate).ToString("yyyy-MM-dd")) > ExamEndDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试结束时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) < ExamStartDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                //考试科目开始时间
                rdp_start = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("ExamStartTime").OrderIndex].Controls[1] as RadDateTimePicker;
                //考试科目结束时间
                rdp_end = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("ExamEndTime").OrderIndex].Controls[1] as RadDateTimePicker;
                kemuExamTime.Add(RadGridExamPlanSubject.MasterTableView.DataKeyValues[i]["PostName"].ToString(), new DateTime[2] { rdp_start.SelectedDate.Value, rdp_end.SelectedDate.Value });
            }

            //if (RadioButtonPublish.Checked == false && RadioButtonUnPublish.Checked == false && RadioButtonTrainUnitLimit.Checked == false)
            //{
            //    UIHelp.layerAlert(Page, "请选择考试计划公开范围！");
            //    e.Canceled = true;
            //    return;
            //}

            //if (_PostSelect.PostTypeID != "1" && RadioButtonListExamWay.SelectedValue=="网考")
            //{
            //    UIHelp.layerAlert(Page, "网考目前支持安全生产三类人岗位，其他岗位类型不适用！");
            //    e.Canceled = true;
            //    return;
            //}

            #endregion 规则校验

            //考试科目集合
            DataTable tdExamPlanSubject = null;

            ExamPlanOB ob = new ExamPlanOB();
            ExamPlanSubjectOB subOB = null;
            UIHelp.GetData(editedItem, ob);

            ob.CreatePersonID = Convert.ToInt64(curTrainUnit.UnitNo);
            ob.SignUpPlace = curTrainUnit.TrainUnitName;
            ob.CreateTime = DateTime.Now;
            ob.Status = EnumManager.ExamPlanStatus.BeginSignUp;
            ob.PostTypeID = Convert.ToInt32(_PostSelect.PostTypeID);
            ob.IfPublish = "部分公开";
            ob.PersonLimit = 10000;
            ob.ExamWay = "机考";
            ob.ExamFee = 0;
            ob.LatestPayDate = ob.LatestCheckDate;

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                for (int i = 0; i < listPostID.Count; i++)//循环工种
                {
                    ob.PostID = Convert.ToInt32(listPostID[i]);
                    ob.PostTypeName = _PostSelect.PostTypeName;
                    ob.PostName = _CheckBoxListPostID.Items.FindByValue(listPostID[i]).Text;
                    if (RadTextBoxExamPlanName.Text== string.Format("{0}{1}建设职业技能岗位",curTrainUnit.TrainUnitName, DateTime.Now.ToString("yyyy年M月")))
                    {
                        ob.ExamPlanName = string.Format("{0}{1}", RadTextBoxExamPlanName.Text, ob.PostName);
                    }
                    //考试计划主表
                    ExamPlanDAL.Insert(trans, ob);

                    if (ob.IfPublish == "部分公开" )
                    {
                        List<ExamPlanForUserOB> list = GetExamPlanForUserOBList(ob.ExamPlanID.Value, editedItem);
                        foreach (ExamPlanForUserOB _ExamPlanForUserOB in list)
                        {
                            ExamPlanForUserDAL.Insert(trans, _ExamPlanForUserOB);
                        }
                    }

                    //考试科目集合
                    tdExamPlanSubject = PostInfoDAL.GetList(0, int.MaxValue - 1, " and UpPostID=" + listPostID[i], "PostID");

                    //从表考试科目
                    for (int j = 0; j < tdExamPlanSubject.Rows.Count; j++)//循环科目
                    {
                        subOB = new ExamPlanSubjectOB();
                        subOB.ExamPlanID = ob.ExamPlanID;//考试计划
                        subOB.PostID = Convert.ToInt32(tdExamPlanSubject.Rows[j]["PostID"]);//科目                   
                        subOB.ExamStartTime = kemuExamTime[tdExamPlanSubject.Rows[j]["PostName"].ToString()][0];//考试开始时间                   
                        subOB.ExamEndTime = kemuExamTime[tdExamPlanSubject.Rows[j]["PostName"].ToString()][1];//考试结束时间                  
                        subOB.ExamFee = 0;//Convert.ToDecimal(tdExamPlanSubject.Rows[j]["ExamFee"]);//金额
                        subOB.Status = "1";
                        subOB.CreatePersonID = ob.CreatePersonID;
                        subOB.CreateTime = DateTime.Now;
                        ExamPlanSubjectDAL.Insert(trans, subOB);
                    }


                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "添加考试计划失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "新增考试计划", string.Format("考试计划名称：{0}。岗位类别：{1}。岗位工种：{2}。", RadTextBoxExamPlanName.Text, _PostSelect.PostTypeName, sbPostName.ToString()));
            UIHelp.layerAlert(Page, string.Format("成功添加岗位类别“{0}”{1}个工种的考试计划！", _PostSelect.PostTypeName, listPostID.Count.ToString()));
        }

        //修改
        protected void RadGridExamPlan_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            PostSelect _PostSelect = (PostSelect)editedItem.FindControl("PostSelect1");
            RadGrid gridSubject = editedItem.FindControl("RadGridExamPlanSubject") as RadGrid;
            RadDatePicker SignUpStartDate = editedItem.Cells[0].FindControl("RadDatePickerSignUpStartDate") as RadDatePicker;
            RadDatePicker SignUpEndDate = editedItem.Cells[0].FindControl("RadDatePickerSignUpEndDate") as RadDatePicker;
            RadDatePicker PickerExamCardSendStartDate = editedItem.Cells[0].FindControl("RadDatePickerExamCardSendStartDate") as RadDatePicker;
            RadDatePicker PickerExamCardSendEndDate = editedItem.Cells[0].FindControl("RadDatePickerExamCardSendEndDate") as RadDatePicker;
            RadDatePicker ExamStartDate = editedItem.Cells[0].FindControl("RadDatePickerExamStartDate") as RadDatePicker;
            RadDatePicker ExamEndDate = editedItem.Cells[0].FindControl("RadDatePickerExamEndDate") as RadDatePicker;
            RadDatePicker RadDatePickerStartCheckDate = e.Item.FindControl("RadDatePickerStartCheckDate") as RadDatePicker;
            RadDatePicker RadDatePickerLatestCheckDate = e.Item.FindControl("RadDatePickerLatestCheckDate") as RadDatePicker;
            RadGrid RadGridExamPlanSubject = e.Item.FindControl("RadGridExamPlanSubject") as RadGrid;
            RadTextBox RadTextBoxExamPlanName = e.Item.FindControl("RadTextBoxExamPlanName") as RadTextBox;
            RadComboBox RadComboBoxPLANSKILLLEVEL = (RadComboBox)editedItem.FindControl("RadComboBoxPLANSKILLLEVEL");

            #region -----规则校验----------
            for (int i = 0; i < RadGridExamPlanSubject.MasterTableView.Items.Count; i++)
            {
                RadDatePicker pickerExamStartTime = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[1].FindControl("pickerExamStartTime") as RadDatePicker;  // editedItem.Cells[0].FindControl("pickerExamStartTime") as RadDatePicker;
                RadDatePicker pickerExamEndTime = RadGridExamPlanSubject.MasterTableView.Items[i].Cells[2].FindControl("pickerExamEndTime") as RadDatePicker;

                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) > Convert.ToDateTime(Convert.ToDateTime(pickerExamEndTime.SelectedDate).ToString("yyyy-MM-dd")))
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不应大于结束时间！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (PickerExamCardSendEndDate.SelectedDate > Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")))
                {
                    UIHelp.layerAlert(Page, string.Format("准考证发放结束时间不应大于科目“{0}”考试开始时间！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) > ExamEndDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamEndTime.SelectedDate).ToString("yyyy-MM-dd")) > ExamEndDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试结束时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(pickerExamStartTime.SelectedDate).ToString("yyyy-MM-dd")) < ExamStartDate.SelectedDate)
                {
                    UIHelp.layerAlert(Page, string.Format("科目“{0}”考试开始时间不在考试计划的考试时间范围内！", RadGridExamPlanSubject.MasterTableView.Items[i].Cells[RadGridExamPlanSubject.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
                    e.Canceled = true;
                    return;
                }
            }
            if (_PostSelect.PostTypeID == "")
            {
                UIHelp.layerAlert(Page, "请选择岗位！");
                e.Canceled = true;
                return;
            }

            //职业技能岗位必须选择技术等级
            if (_PostSelect.PostTypeID == "4000" && RadComboBoxPLANSKILLLEVEL.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "必须选择一个技术等级！");
                e.Canceled = true;
                return;
            }

            if (SignUpStartDate.SelectedDate > SignUpEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试报名开始时间不应大于考试报名结束时间！");
                e.Canceled = true;
                return;
            }
            if (PickerExamCardSendStartDate.SelectedDate > PickerExamCardSendEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "准考证发放开始时间不应大于准考证发放截止时间！");
                e.Canceled = true;
                return;
            }
            if (ExamStartDate.SelectedDate > ExamEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试开始时间不应大于考试结束时间！");
                e.Canceled = true;
                return;
            }
            if (SignUpEndDate.SelectedDate > PickerExamCardSendStartDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "考试报名截止时间不应大于准考证发放开始时间！");
                e.Canceled = true;
                return;
            }
            if (PickerExamCardSendEndDate.SelectedDate > ExamStartDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "准考证发放截止时间不应大于考试开始时间！");
                e.Canceled = true;
                return;
            }
            if (RadDatePickerLatestCheckDate.SelectedDate < SignUpEndDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "报名截止时时间不应大于审核截止日期！");
                e.Canceled = true;
                return;
            }
            if (RadDatePickerStartCheckDate.SelectedDate > RadDatePickerLatestCheckDate.SelectedDate)
            {
                UIHelp.layerAlert(Page, "审核开始时间不应大于审核截止时间！");
                e.Canceled = true;
                return;
            }

            #endregion 规则校验

            ExamPlanOB ob = ViewState["ExamPlanOB"] as ExamPlanOB;
            UIHelp.GetData(editedItem, ob);

            ob.ModifyPersonID = Convert.ToInt64(curTrainUnit.UnitNo); 
            ob.ModifyTime = DateTime.Now;
            ob.PostTypeID = Convert.ToInt32(_PostSelect.PostTypeID);
            ob.PostID = Convert.ToInt32(_PostSelect.PostID);

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                //计划主表
                ExamPlanDAL.Update(trans, ob);

                ExamPlanForUserDAL.Delete(trans, ob.ExamPlanID.Value);//清除所有分配记录

                if (ob.IfPublish == "部分公开" )
                {
                    List<ExamPlanForUserOB> list = GetExamPlanForUserOBList(ob.ExamPlanID.Value, editedItem);
                    foreach (ExamPlanForUserOB _ExamPlanForUserOB in list)
                    {
                        ExamPlanForUserDAL.Insert(trans, _ExamPlanForUserOB);//添加分配记录
                    }
                }

                //从表考试科目
                foreach (GridItem gi in gridSubject.MasterTableView.Items)
                {
                    ExamPlanSubjectOB subOB = ExamPlanSubjectDAL.GetObject(Convert.ToInt64(gridSubject.MasterTableView.DataKeyValues[gi.ItemIndex]["ExamPlanSubjectID"]));
                    RadDateTimePicker rdp_start = gi.Cells[gridSubject.MasterTableView.Columns.FindByUniqueName("ExamStartTime").OrderIndex].Controls[1] as RadDateTimePicker;
                    subOB.ExamStartTime = rdp_start.SelectedDate.Value;//考试开始时间
                    RadDateTimePicker rdp_end = gi.Cells[gridSubject.MasterTableView.Columns.FindByUniqueName("ExamEndTime").OrderIndex].Controls[1] as RadDateTimePicker;
                    subOB.ExamEndTime = rdp_end.SelectedDate.Value;//考试结束时间
                    subOB.ExamFee = 0;// Convert.ToDecimal(rntb_ExamFee.Value);//金额

                    subOB.ModifyPersonID = ob.CreatePersonID;
                    subOB.ModifyTime = DateTime.Now;
                    ExamPlanSubjectDAL.Update(trans, subOB);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "修改考试计划失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "更新考试计划", string.Format("考试计划名称：{0}。岗位工种：{1}。", RadTextBoxExamPlanName.Text, _PostSelect.PostName));
            UIHelp.layerAlert(Page,  "修改考试计划成功！", 6,3000);
        }

        //删除
        protected void RadGridExamPlan_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 ExamPlanID = Convert.ToInt64(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]);             //Convert.ToInt32(e.CommandArgument);

            //查看是否已经报过名了
            DataTable ExamSignUpOBdt = ExamSignUpDAL.GetObjectByExamPlanID(ExamPlanID);
            if (ExamSignUpOBdt.Rows.Count > 0)
            {
                UIHelp.layerAlert(Page, "该考试计划已经报名，不能删除！");
                return;
            }

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                //考试计划科目表（子表）
                ExamPlanSubjectDAL.DeleteByExamPlanID(trans, ExamPlanID);
                //计划主表
                ExamPlanDAL.Delete(trans, ExamPlanID);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "删除考试计划失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "删除考试计划", string.Format("考试计划名称：{0}。岗位工种：{1}。",
                e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("ExamPlanName").OrderIndex].Text,
                e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text));
            UIHelp.layerAlert(Page, "删除考试计划成功！",6,3000);
        }

        //编辑时选择一个岗位后执行
        protected void PostSelect1_OnPostTypeSelectChange(object source, EventArgs e)
        {
            if (RadGridExamPlan.MasterTableView.GetInsertItem() != null)//new
            {
                //初始化考试科目
                GridEditableItem editedItem = RadGridExamPlan.MasterTableView.GetInsertItem();
                PostSelect _PostSelect = (PostSelect)editedItem.FindControl("PostSelect1");
                RadComboBox RadComboBoxPLANSKILLLEVEL = (RadComboBox)editedItem.FindControl("RadComboBoxPLANSKILLLEVEL");
                string _PostTypeID = _PostSelect.PostTypeID == "" ? "-1" : _PostSelect.PostTypeID;
                DataTable dt = PostInfoDAL.GetListByPostID(_PostTypeID);

                RadGrid RadGridSubject = editedItem.FindControl("RadGridExamPlanSubject") as RadGrid;
                RadGridSubject.DataSource = dt;
                RadGridSubject.DataBind();

                //初始化待选择岗位工种
                CheckBoxList _CheckBoxListPostID = (CheckBoxList)editedItem.FindControl("CheckBoxListPostID");
                _CheckBoxListPostID.Visible = true;
                _CheckBoxListPostID.Items.Clear();
                if (_PostSelect.PostTypeID == "") return;

                QueryParamOB q = new QueryParamOB();
                q.Add("PostType = 2");
                q.Add("UpPostID = " + _PostSelect.PostTypeID);
                q.Add(string.Format("PostID in({0})", curTrainUnit.PostSet));
                DataTable dtPostID = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "PostName");
                _CheckBoxListPostID.DataSource = dtPostID;
                _CheckBoxListPostID.DataBind();

                //职业技能岗位考试计划有“技术等级”分类
                if (_PostTypeID == "4000")
                {
                    RadComboBoxPLANSKILLLEVEL.Visible = true;
                }
                else
                {
                    RadComboBoxPLANSKILLLEVEL.Visible = false;
                }
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
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            q.Add(string.Format("SIGNUPPLACE like '{0}'", curTrainUnit.TrainUnitName));//培训点名称

            //计划名称
            if (RadTextBoxExam_PlanName.Text.Trim() != "") q.Add(string.Format("ExamPlanName like '%{0}%'", RadTextBoxExam_PlanName.Text.Trim()));
            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //报名时间
            if (RadDatePicker_SignUpStartDate.SelectedDate.HasValue || RadDatePicker_SignUpEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN  SignUpStartDate AND SignUpEndDate) or ('{1}' BETWEEN SignUpStartDate AND SignUpEndDate) or (SignUpStartDate BETWEEN '{0}' AND '{1}') or (SignUpEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_SignUpStartDate.SelectedDate.HasValue ? RadDatePicker_SignUpStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_SignUpEndDate.SelectedDate.HasValue ? RadDatePicker_SignUpEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            //考试时间
            if (RadDatePicker_ExamStartDate.SelectedDate.HasValue || RadDatePicker_ExamEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN ExamStartDate AND ExamEndDate) or ('{1}' BETWEEN ExamStartDate AND ExamEndDate) or (ExamStartDate BETWEEN '{0}' AND '{1}') or (ExamEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_ExamStartDate.SelectedDate.HasValue ? RadDatePicker_ExamStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_ExamEndDate.SelectedDate.HasValue ? RadDatePicker_ExamEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            if (rbl.SelectedValue == "1")//未考试
            {
                q.Add(string.Format("ExamStartDate>'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            if (rbl.SelectedValue == "2")//以考试
            {
                q.Add(string.Format("ExamStartDate<='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
