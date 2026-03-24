using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.EXamManage
{
    public partial class ExamRoomAllot : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid();
            }
        }

        //分配考点
        protected void ButtonAllot_Click(object sender, EventArgs e)
        {
            if (ExamPlanSelect1.ExamPlanID.HasValue == false) return;
            if (RadGridExamPlace.SelectedIndexes.Count == 0) return;
            DateTime _CreateTime = DateTime.Now;
            Int64 _ExamPlanID = ExamPlanSelect1.ExamPlanID.Value;
            int ExamRoomNum = 1;//考点编号
            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                foreach (GridItem item in RadGridExamPlace.SelectedItems)
                {
                    ExamPlaceAllotOB _ExamPlaceAllotOB = new ExamPlaceAllotOB();
                    _ExamPlaceAllotOB.ExamPlaceID = Convert.ToInt64(RadGridExamPlace.MasterTableView.DataKeyValues[item.ItemIndex]["ExamPlaceID"]);
                    _ExamPlaceAllotOB.ExamPlanID = _ExamPlanID;
                    _ExamPlaceAllotOB.ExamPersonNum = 0;// Convert.ToInt32(RadGridExamPlace.MasterTableView.DataKeyValues[item.ItemIndex]["ExamPersonNum"]);
                    _ExamPlaceAllotOB.ExamPlaceName = RadGridExamPlace.MasterTableView.DataKeyValues[item.ItemIndex]["ExamPlaceName"].ToString();
                    _ExamPlaceAllotOB.RoomNum = 0;// Convert.ToInt32(RadGridExamPlace.MasterTableView.DataKeyValues[item.ItemIndex]["RoomNum"]);
                    _ExamPlaceAllotOB.Status = EnumManager.ExamPlaceAllotStatus.AllotPlaced;
                    _ExamPlaceAllotOB.CreatePersonID = PersonID;
                    _ExamPlaceAllotOB.CreateTime = _CreateTime;
                    ExamRoomNum++;
                    ExamPlaceAllotDAL.Insert(trans, _ExamPlaceAllotOB);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "分配考点时发生错误！", ex);
                return;
            }
            RefreshGrid();
        }

        //回收考点
        protected void ButtonRecover_Click(object sender, EventArgs e)
        {
            if (RadGridExamPlaceAllot.SelectedIndexes.Count == 0) return;

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                foreach (GridItem item in RadGridExamPlaceAllot.SelectedItems)
                {
                    //删除考场分配
                    ExamRoomAllotDAL.DeleteByExamPlaceAllotID(trans, Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ExamPlaceAllotID"]));
                    
                    //删除考点分配
                    ExamPlaceAllotDAL.Delete(trans, Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ExamPlaceAllotID"]));
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "回收考点分配时发生错误！", ex);
                return;
            }
            //if (ExamPlanSelect1.ExamPlanID.HasValue) ExamRoomAllotDAL.UpdateExamRoomCode(ExamPlanSelect1.ExamPlanID.Value);//更新考场编号大排行
            RefreshGrid();
        }

        //刷新可分配列表和已分配列表
        protected void RefreshGrid()
        {
            ButtonAllot.Enabled = true;
            ButtonRecover.Enabled = true;
            ButtonCreateExamCard.Enabled = true;

            //刷新已分配列表
            ObjectDataSourceExamPlaceAllot.SelectParameters.Clear();
            QueryParamOB q1 = new QueryParamOB();
            if (ExamPlanSelect1.ExamPlanID.HasValue)
                q1.Add(string.Format("ExamPlanID={0}", ExamPlanSelect1.ExamPlanID.Value.ToString()));
            else
                q1.Add("ExamPlanID=0");
            ObjectDataSourceExamPlaceAllot.SelectParameters.Add("filterWhereString", q1.ToWhereString());
            RadGridExamPlaceAllot.CurrentPageIndex = 0;
            RadGridExamPlaceAllot.DataSourceID = ObjectDataSourceExamPlaceAllot.ID;
            RadGridExamPlaceAllot.DataBind();
            RefreshAllotHelpInfo();//分配友好提示

            //刷新可分配列表
            ObjectDataSourceExamPlace.SelectParameters.Clear();
            QueryParamOB q2 = new QueryParamOB();
            q2.Add(string.Format("[STATUS]='{0}'" ,EnumManager.ExamPlaceStatus.UnDelete));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < RadGridExamPlaceAllot.MasterTableView.Items.Count; i++)
            {
                sb.Append(",").Append(RadGridExamPlaceAllot.MasterTableView.DataKeyValues[i]["ExamPlaceID"].ToString());
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            if (sb.Length > 0) q2.Add(string.Format("ExamPlaceID not in({0})", sb.ToString()));

            ObjectDataSourceExamPlace.SelectParameters.Add("filterWhereString", q2.ToWhereString());
            RadGridExamPlace.CurrentPageIndex = 0;
            RadGridExamPlace.DataSourceID = ObjectDataSourceExamPlace.ID;

            
        }

        //绑定编辑数据
        protected void RadGridExamPlaceAllot_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                switch (e.Item.OwnerTableView.Name)
                {
                    case "ExamPlaceAllot"://考点

                        if (e.Item.OwnerTableView.IsItemInserted == false)//update
                        {
                            GridEditableItem editedItem = e.Item as GridEditableItem;
                            RadTextBox _RadTextBoxExamPlaceName = (RadTextBox)editedItem.FindControl("RadTextBoxExamPlaceName");
                            ExamPlaceAllotOB ob = ExamPlaceAllotDAL.GetObject(Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlaceAllotID"]));

                            UIHelp.SetData(editedItem, ob);
                            _RadTextBoxExamPlaceName.Enabled = false;
                            ViewState["ExamPlaceAllotOB"] = ob;

                            RadDateTimePicker _pickerExamStartTime = (RadDateTimePicker)editedItem.FindControl("pickerExamStartTime");
                            RadDateTimePicker _pickerExamEndTime = (RadDateTimePicker)editedItem.FindControl("pickerExamEndTime");
                            ExamPlanOB _ExamPlanOB = (ExamPlanOB)ViewState["ExamPlanOB"];
                            _pickerExamStartTime.DbSelectedDate = _ExamPlanOB.ExamStartDate;
                            _pickerExamEndTime.DbSelectedDate = _ExamPlanOB.ExamStartDate;
                        }
                        break;
                    case "ExamRoomAllot"://考场
                        if (e.Item.OwnerTableView.IsItemInserted == false)//update
                        {
                            //绑定计划
                            GridEditableItem editedItem = e.Item as GridEditableItem;
                            ExamRoomAllotOB ob = ExamRoomAllotDAL.GetObject(Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamRoomAllotID"]));
                            UIHelp.SetData(editedItem, ob);
                            RadDateTimePicker _pickerExamStartTime = (RadDateTimePicker)editedItem.FindControl("pickerExamStartTime");
                            RadDateTimePicker _pickerExamEndTime = (RadDateTimePicker)editedItem.FindControl("pickerExamEndTime");
                            _pickerExamStartTime.DbSelectedDate = ob.ExamStartTime;
                            _pickerExamEndTime.DbSelectedDate = ob.ExamEndTime;
                            ViewState["ExamRoomAllotOB"] = ob;
                        }
                        break;
                }
            }
            else if (e.Item is GridDataItem)
            {
                switch (e.Item.OwnerTableView.Name)
                {
                    case "ExamPlaceAllot"://考点
                        //分配准考证后不允许修改考点分配信息
                        if (e.Item != null && e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Status"].ToString() == EnumManager.ExamPlaceAllotStatus.AllotExamered)
                        {
                            e.Item.Cells[RadGridExamPlaceAllot.Columns.FindByUniqueName("Edit").OrderIndex].Enabled = false;
                            //ButtonAllot.Enabled = false;
                            //ButtonRecover.Enabled = false;
                            //ButtonCreateExamCard.Enabled = false;
                            UIHelp.SetButtonEnable(ButtonAllot, false);
                            UIHelp.SetButtonEnable(ButtonRecover, false);
                            UIHelp.SetButtonEnable(ButtonCreateExamCard, false);

                            //if (IfSupperAdmin() == true)//超级管理员可以取消准考证放号
                            //{
                            //    ButtonDeleteeExamCard.Visible = true;
                            //}
                            if(IfExistRoleID("1")||IfExistRoleID("16"))//超级管理员和注册中心考务管理室管理员允许取消准考证放号
                            {
                                ButtonDeleteeExamCard.Visible = true;
                            }
                        }
                        else
                        {
                            e.Item.Cells[RadGridExamPlaceAllot.Columns.FindByUniqueName("Edit").OrderIndex].Enabled = true;
                            //ButtonAllot.Enabled = true;
                            //ButtonRecover.Enabled = true;
                            //ButtonCreateExamCard.Enabled = true;
                            ButtonDeleteeExamCard.Visible = false;
                            UIHelp.SetButtonEnable(ButtonAllot, true);
                            UIHelp.SetButtonEnable(ButtonRecover, true);
                            UIHelp.SetButtonEnable(ButtonCreateExamCard, true);
                        }
                        break;
                    case "ExamRoomAllot"://考场
                        //分配准考证后不允许修改考场分配信息
                        if (e.Item != null && e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Status"].ToString() == EnumManager.ExamRoomAllotStatus.AllotExamered)
                        {
                            e.Item.Cells[e.Item.OwnerTableView.Columns.FindByUniqueName("Edit").OrderIndex].Enabled = false;
                            e.Item.Cells[e.Item.OwnerTableView.Columns.FindByUniqueName("Delete").OrderIndex].Enabled = false;
                            //ButtonAllot.Enabled = false;
                            //ButtonRecover.Enabled = false;
                            //ButtonCreateExamCard.Enabled = false;
                            UIHelp.SetButtonEnable(ButtonAllot, false);
                            UIHelp.SetButtonEnable(ButtonRecover, false);
                            UIHelp.SetButtonEnable(ButtonCreateExamCard, false);

                            if (IfSupperAdmin() == true)//超级管理员可以取消准考证放号
                            {
                                ButtonDeleteeExamCard.Visible = true;
                            }
                        }
                        else
                        {
                            e.Item.Cells[e.Item.OwnerTableView.Columns.FindByUniqueName("Edit").OrderIndex].Enabled = true;
                            e.Item.Cells[e.Item.OwnerTableView.Columns.FindByUniqueName("Delete").OrderIndex].Enabled = true;
                            //ButtonAllot.Enabled = true;
                            //ButtonRecover.Enabled = true;
                            //ButtonCreateExamCard.Enabled = true;
                            ButtonDeleteeExamCard.Visible = false;
                            UIHelp.SetButtonEnable(ButtonAllot, true);
                            UIHelp.SetButtonEnable(ButtonRecover, true);
                            UIHelp.SetButtonEnable(ButtonCreateExamCard, true);
                        }
                        break;
                }
            }
        }

        //添加考场
        protected void RadGridExamPlaceAllot_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "ExamRoomAllot")
            {
                GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
                GridEditableItem editedItem = e.Item as GridEditableItem;
                ExamRoomAllotOB ob = new ExamRoomAllotOB();
                UIHelp.GetData(editedItem, ob);
                ob.ExamPlaceAllotID = Convert.ToInt64(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["ExamPlaceAllotID"]);
                ob.ExamPlaceID = Convert.ToInt64(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["ExamPlaceID"]);
                ob.ExamPlanID = Convert.ToInt64(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["ExamPlanID"]);
                ob.Status = EnumManager.ExamRoomAllotStatus.AllotRoomed;
                ob.CreatePersonID = PersonID;
                ob.CreateTime = DateTime.Now;
                RadDateTimePicker _pickerExamStartTime = (RadDateTimePicker)editedItem.FindControl("pickerExamStartTime");
                RadDateTimePicker _pickerExamEndTime = (RadDateTimePicker)editedItem.FindControl("pickerExamEndTime");
                ob.ExamStartTime = _pickerExamStartTime.SelectedDate;
                ob.ExamEndTime = _pickerExamEndTime.SelectedDate;
                try
                {
                    ExamRoomAllotDAL.Insert(ob);//添加考场
                    ExamPlaceAllotOB ExamPlace = ExamPlaceAllotDAL.GetObject(ob.ExamPlaceAllotID.Value);
                    ExamPlace.RoomNum = ExamPlace.RoomNum.Value + 1;
                    ExamPlace.ExamPersonNum = ExamPlace.ExamPersonNum.Value + ob.PersonNumber.Value;
                    ExamPlaceAllotDAL.Update(ExamPlace);//更新考点
                    //ExamRoomAllotDAL.UpdateExamRoomCode(ob.ExamPlanID.Value);//更新考场编号大排行
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "添加考场失败！", ex);
                    return;
                }
                
                RadGridExamPlaceAllot.DataBind();
                RefreshAllotHelpInfo();//分配友好提示
                UIHelp.layerAlert(Page, "添加考场成功！",6,3000);
            }
        }

        //修改分配考点信息，并分配考场
        protected void RadGridExamPlaceAllot_UpdateCommand(object source, GridCommandEventArgs e)
        {
            DateTime _CreateTime = DateTime.Now;
            GridEditableItem editedItem = e.Item as GridEditableItem;

            RadDateTimePicker _pickerExamStartTime = (RadDateTimePicker)editedItem.FindControl("pickerExamStartTime");
            RadDateTimePicker _pickerExamEndTime = (RadDateTimePicker)editedItem.FindControl("pickerExamEndTime");

            switch (e.Item.OwnerTableView.Name)
            {
                case "ExamPlaceAllot"://考点  
                    if (_pickerExamStartTime.SelectedDate.HasValue == false || _pickerExamEndTime.SelectedDate.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, "请输入考试开始时间和结束时间");
                        return;
                    }
                    DBHelper db = new DBHelper();
                    DbTransaction tran = db.BeginTransaction();
                    ExamPlaceAllotOB ob = ViewState["ExamPlaceAllotOB"] as ExamPlaceAllotOB;
                    UIHelp.GetData(editedItem, ob);
                    ob.ModifyPersonID = PersonID;
                    ob.ModifyTime = DateTime.Now;

                    int _PersonNumber = ob.ExamPersonNum.Value / ob.RoomNum.Value;//每个考场分配人数
                    int residue = ob.ExamPersonNum.Value % ob.RoomNum.Value;
                    RadNumericTextBox rdnt = editedItem.FindControl("RadNumericTextBoxFirstRoomCode") as RadNumericTextBox;
                    int _firstRoomCode=Convert.ToInt32(rdnt.Value);
                    string tip = "";

                    try
                    {
                        if (e.CommandArgument.ToString() == "分配考场")//全新分配，覆盖原来分配
                        {
                            //修改考点分配信息
                            ExamPlaceAllotDAL.Update(tran, ob);
                            //清除考场分配
                            ExamRoomAllotDAL.DeleteByExamPlaceAllotID(tran, ob.ExamPlaceAllotID.Value);
                        }

                        //重新分配考场
                        for (int i = 0; i < ob.RoomNum.Value; i++)
                        {
                            ExamRoomAllotOB _ExamRoomAllotOB = new ExamRoomAllotOB();
                            _ExamRoomAllotOB.ExamPlanID = ob.ExamPlanID;
                            _ExamRoomAllotOB.ExamPlaceID = ob.ExamPlaceID;
                            _ExamRoomAllotOB.ExamPlaceAllotID = ob.ExamPlaceAllotID;
                            if (i == ob.RoomNum.Value - 1 && residue != 0)
                            {
                                _ExamRoomAllotOB.PersonNumber = _PersonNumber + residue;
                                tip = string.Format("，最后一个考场分配了{0}人", _ExamRoomAllotOB.PersonNumber.Value);
                            }
                            else
                            {
                                _ExamRoomAllotOB.PersonNumber = _PersonNumber;
                            }

                            _ExamRoomAllotOB.ExamRoomCode = _firstRoomCode.ToString();
                            _firstRoomCode ++;
                            _ExamRoomAllotOB.Status = EnumManager.ExamRoomAllotStatus.AllotRoomed;
                            _ExamRoomAllotOB.CreatePersonID = PersonID;
                            _ExamRoomAllotOB.CreateTime = _CreateTime;
                            _ExamRoomAllotOB.ExamStartTime = _pickerExamStartTime.SelectedDate;
                            _ExamRoomAllotOB.ExamEndTime = _pickerExamEndTime.SelectedDate;
                            ExamRoomAllotDAL.Insert(tran, _ExamRoomAllotOB);
                        }
                        if (e.CommandArgument.ToString() == "追加考场")//更新追加后考场数量及人数
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                UPDATE [dbo].[EXAMPLACEALLOT]
                                set [ROOMNUM]= (
	                                select count(*) from [dbo].[EXAMPLACEALLOT] inner join [dbo].[EXAMROOMALLOT] 
	                                on [EXAMPLACEALLOT].[EXAMPLACEALLOTID] = [EXAMROOMALLOT].[EXAMPLACEALLOTID]
	                                where [EXAMPLACEALLOT].[EXAMPLACEALLOTID]={0}),
	                                [EXAMPERSONNUM]=(
	                                select sum(isnull([EXAMROOMALLOT].[PERSONNUMBER],0)) from [dbo].[EXAMPLACEALLOT] inner join [dbo].[EXAMROOMALLOT] 
	                                on [EXAMPLACEALLOT].[EXAMPLACEALLOTID] = [EXAMROOMALLOT].[EXAMPLACEALLOTID]
	                                where [EXAMPLACEALLOT].[EXAMPLACEALLOTID]={0}),
	                                [MODIFYPERSONID]={1},[MODIFYTIME]='{2}'
                                where [EXAMPLACEALLOT].[EXAMPLACEALLOTID]={0}", ob.ExamPlaceAllotID, PersonID, _CreateTime));
                        }

                        tran.Commit();
                        ViewState["ExamPlaceAllot"] = ob;
                        //ExamRoomAllotDAL.UpdateExamRoomCode(ob.ExamPlanID.Value);//更新考场编号大排行
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        UIHelp.WriteErrorLog(Page, "分配考场失败！", ex);
                        return;
                    }
                    RadGridExamPlaceAllot.DataBind();
                    UIHelp.layerAlert(Page, string.Format("分配考场成功！分配了{0}个考场，每个考场{1}人{2}，您可以进入编辑页面对考场人数进行微调。", ob.RoomNum.ToString(), _PersonNumber.ToString(), tip));
                    break;
                case "ExamRoomAllot"://考场
                    ExamRoomAllotOB ob2 = ViewState["ExamRoomAllotOB"] as ExamRoomAllotOB;
                    string roomCode = ob2.ExamRoomCode;
                    UIHelp.GetData(editedItem, ob2);
                    ob2.ModifyPersonID = PersonID;
                    ob2.ModifyTime = _CreateTime;
                    ob2.ExamStartTime = _pickerExamStartTime.SelectedDate;
                    ob2.ExamEndTime = _pickerExamEndTime.SelectedDate;

                    try
                    {
                        ExamRoomAllotDAL.Update(ob2);
                        ExamPlaceAllotOB ExamPlace = ExamPlaceAllotDAL.GetObject(ob2.ExamPlaceAllotID.Value);
                        int all = 0;
                        foreach (DataRow dr in ExamRoomAllotDAL.GetList(0, int.MaxValue - 1, " and ExamPlaceAllotID=" + ExamPlace.ExamPlaceAllotID.Value.ToString(), "ExamRoomAllotID").Rows)
                        {
                            all = all + Convert.ToInt32(dr["PersonNumber"]);
                        }
                        ExamPlace.ExamPersonNum = all;
                        ExamPlaceAllotDAL.Update(ExamPlace);
                        //if (roomCode != ob2.ExamRoomCode)//修改了考场编号
                        //{
                        //    ExamRoomAllotDAL.UpdateExamRoomCode(ob2.ExamPlanID.Value);//更新考场编号大排行
                        //}
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "修改考场分配考试人数失败！", ex);
                        return;
                    }
                    e.Item.OwnerTableView.DataBind();
                    UIHelp.layerAlert(Page, "修改成功！",6,3000);
                    break;
            }
            RefreshAllotHelpInfo();//分配友好提示
        }

        //发放准考证
        protected void ButtonCreateExamCard_Click(object sender, EventArgs e)
        {
            if (RadGridExamPlaceAllot.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "请首先分配考点及考场！");
                return;
            }

            Int64 _ExamPlanID = ExamPlanSelect1.ExamPlanID.Value;

            //考场最多允许考试人数
            int allotRoomPersonNumber = ExamRoomAllotDAL.GetSumOfPersonNumber(_ExamPlanID);
            if (allotRoomPersonNumber == 0)
            {
                UIHelp.layerAlert(Page, "请首先为各考点分配考场！");
                return;
            }

            //报名记录
            QueryParamOB queryOB2 = new QueryParamOB();
            queryOB2.Add(string.Format("ExamPlanID={0}", _ExamPlanID.ToString()));
            queryOB2.Add(string.Format("Status='{0}'", EnumManager.SignUpStatus.PayConfirmed));
            DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, queryOB2.ToWhereString(), "ExamSignUpID");
            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dt.Columns["WorkerID"];
            dt.PrimaryKey = dcKeys;

            int _sumSignUp = dt == null ? 0 : dt.Rows.Count;//报名缴费确认人数
            if (_sumSignUp >9999)
            {
                UIHelp.layerAlert(Page, string.Format("一次考试必须小于10000人，当前待分配准考证人数{0}人，无法分配准考证，请拆分考试计划后再分配准考证！", _sumSignUp));
                return;
            }
            if (allotRoomPersonNumber != _sumSignUp)
            {
                UIHelp.layerAlert(Page, string.Format("所有分配考场可容纳{0}人，与报名考试{1}人不相等，无法分配准考证，请重新修改考场分配！", allotRoomPersonNumber.ToString(), _sumSignUp.ToString()));
                return;
            }
            string ExamYear = DateTime.Now.ToString("yy");//考试年度，2位
            string ExamCount = ExamPlanDAL.GetIndexOfExamInYear(_ExamPlanID);//年度第几次考试，3位

            if(ExamCount.Length >3)
            {
                UIHelp.layerAlert(Page, string.Format("每年允许组织考试小于1000次，今年已经发布考试计划{0}次，超出范围，无法分配准考证。", ExamCount));
                return;
            }

            List<long> WorkIDList = new List<long>();//待分配的考生ID集合
            for (int i = 0; i < _sumSignUp; i++)
            {                
                WorkIDList.Add(Convert.ToInt64(dt.Rows[i]["WorkerID"]));
            }

            //考场记录
            QueryParamOB queryOB3 = new QueryParamOB();
            queryOB3.Add(string.Format("ExamPlanID={0}", _ExamPlanID.ToString()));
            DataTable dtRoom = ExamRoomAllotDAL.GetList(0, int.MaxValue - 1, queryOB3.ToWhereString(), " ExamPlaceAllotID,ExamStartTime,cast(ExamRoomCode as int)");

            //生成准考证号
            DateTime _CreateTime = DateTime.Now;
            int startNo = 0;//分配开始index
            int examerCount = 0;//分配人数
            Int64 _ExamPlaceAllotID = 0;//考点分配ID
            Int64 _ExamRoomAllotID = 0;//考场分配ID
            System.Random random = new Random();
            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                for (int i = 0; i < dtRoom.Rows.Count; i++)
                {
                    _ExamPlaceAllotID = Convert.ToInt64(dtRoom.Rows[i]["ExamPlaceAllotID"]);
                    _ExamRoomAllotID = Convert.ToInt64(dtRoom.Rows[i]["ExamRoomAllotID"]);
                    examerCount = Convert.ToInt32(dtRoom.Rows[i]["PersonNumber"]);
                    for (int j = startNo; j < (startNo + examerCount); j++)
                    {
                         ExamResultOB _ExamResultOB = new ExamResultOB();
                        _ExamResultOB.ExamPlanID = _ExamPlanID;//考试计划
                        _ExamResultOB.ExamRoomAllotID = _ExamRoomAllotID;//考场
                        _ExamResultOB.WorkerID = GetWorkerIDRodom(random,WorkIDList);//随机取考生  
                        _ExamResultOB.ExamSignUp_ID = Convert.ToInt64(dt.Rows.Find(_ExamResultOB.WorkerID.Value)["ExamSignUpID"]);//报名ID
                        //准考证编号规则:前两位为年份，第三、四、五位为本年第几次考试，第六至第九位为序号。
                        _ExamResultOB.ExamCardID = string.Format("{0}{1}{2}", ExamYear, ExamCount, (j +1).ToString("0000"));//准考证号
                        _ExamResultOB.Status = EnumManager.ExamResultStatus.BeforeResult;
                        _ExamResultOB.CreatePersonID = PersonID;
                        _ExamResultOB.CreateTime = _CreateTime;

                        ExamResultDAL.Insert(trans, _ExamResultOB);
                    }

                    //更新场点分配（准考证范围、状态）
                    ExamRoomAllotOB _ExamRoomAllotOB = ExamRoomAllotDAL.GetObject(_ExamRoomAllotID);
                    _ExamRoomAllotOB.ExamCardIDFromTo = string.Format("{0}{1}～{0}{2}", ExamYear + ExamCount, (startNo + 1).ToString("0000"), (startNo + examerCount).ToString("0000"));
                    _ExamRoomAllotOB.Status = EnumManager.ExamRoomAllotStatus.AllotExamered;
                    _ExamRoomAllotOB.ModifyPersonID = PersonID;
                    _ExamRoomAllotOB.ModifyTime = _CreateTime;
                    ExamRoomAllotDAL.Update(trans, _ExamRoomAllotOB);

                    startNo += examerCount;
                }

                // 更新考点
                for (int i = 0; i < RadGridExamPlaceAllot.MasterTableView.Items.Count; i++)
                {
                    ExamPlaceAllotOB _ExamPlaceAllotOB = ExamPlaceAllotDAL.GetObject(Convert.ToInt64(RadGridExamPlaceAllot.MasterTableView.DataKeyValues[i]["ExamPlaceAllotID"]));
                    _ExamPlaceAllotOB.Status = EnumManager.ExamPlaceAllotStatus.AllotExamered;
                    _ExamPlaceAllotOB.ModifyPersonID = PersonID;
                    _ExamPlaceAllotOB.ModifyTime = _CreateTime;
                    ExamPlaceAllotDAL.Update(trans, _ExamPlaceAllotOB);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "分配准考证号时发生错误！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "分配准考证号", string.Format("考试计划：{0}，分配人数：{1}人。",
                ExamPlanSelect1.ExamPlanName, _sumSignUp.ToString()));   

            UIHelp.layerAlert(Page, "分配准考证号成功，您可以查看详细考生名单！");
            RefreshGrid();
        }

        //取消发放准考证
        protected void ButtonDeleteeExamCard_Click(object sender, EventArgs e)
        {
            Int64 _ExamPlanID = ExamPlanSelect1.ExamPlanID.Value;
            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                //删除准考证
                ExamResultDAL.DeleteByExamPlanID(trans, _ExamPlanID);

                //设定考室状态为“已分配考场”
                ExamRoomAllotDAL.UpdateExamRoomStatus(trans, _ExamPlanID, EnumManager.ExamRoomAllotStatus.AllotRoomed,PersonID,DateTime.Now);

                //设定考室状态为“已分配考场”
                ExamPlaceAllotDAL.UpdateExamPlaceAllotStatus(trans, _ExamPlanID, EnumManager.ExamPlaceAllotStatus.AllotPlaced, PersonID, DateTime.Now);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "取消准考证放号时发生错误！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "取消准考证放号", string.Format("考试计划：{0}。", ExamPlanSelect1.ExamPlanName));
            UIHelp.layerAlert(Page, "取消准考证放号成功！");
            RefreshGrid();
        }

        /// <summary>
        /// 随机分配考生
        /// </summary>
        /// <param name="WorkIDList">待分配的考生ID集合</param>
        /// <returns>考生ID</returns>
        protected long GetWorkerIDRodom(System.Random myRandom, List<long> WorkIDList)
        {            
            int index = myRandom.Next(0, WorkIDList.Count - 1);
            long rtn = WorkIDList[index];
            WorkIDList.RemoveAt(index);
            return rtn;
        }

        //考场绑定
        protected void RadGridExamPlaceAllot_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "ExamRoomAllot":
                    {
                        string _ExamPlanID = dataItem.GetDataKeyValue("ExamPlanID").ToString();
                        string _ExamPlaceID = dataItem.GetDataKeyValue("ExamPlaceID").ToString();
                        ObjectDataSourceExamRoomAllot.SelectParameters.Clear();
                        ObjectDataSourceExamRoomAllot.SelectParameters.Add("filterWhereString", string.Format("and ExamPlanID={0} and ExamPlaceID={1}", _ExamPlanID, _ExamPlaceID));
                        e.DetailTableView.CurrentPageIndex = 0;
                        e.DetailTableView.DataSourceID = ObjectDataSourceExamRoomAllot.ID;

                        //分配准考证后不允许添加考场
                        if (dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["Status"].ToString() == EnumManager.ExamPlaceAllotStatus.AllotExamered)
                        {
                            e.DetailTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                        }
                        break;
                    }

            }

        }

        //选择了考试计划
        protected void ExamPlanSelect_Changed(object sender, EventArgs e)
        {
           
            ExamPlanOB o= ExamPlanDAL.GetObject(ExamPlanSelect1.ExamPlanID.Value);
            ViewState["ExamPlanOB"] = o;
            RefreshGrid();//变换考试计划选择后刷新
        }
        
        //行操作命令
        protected void RadGridExamPlaceAllot_ItemCommand(object source, GridCommandEventArgs e)
        {
            if ("ExamRoomAllot".Equals(e.Item.OwnerTableView.Name))
            {
                if (e.CommandName == "ButtonDelete")
                {
                    string id = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamRoomAllotID"].ToString();
                    try
                    {                       
                        ExamRoomAllotOB ExamRoom = ExamRoomAllotDAL.GetObject(Convert.ToInt64(id));
                        ExamRoomAllotDAL.Delete(Convert.ToInt64(id));

                        ExamPlaceAllotOB ExamPlace = ExamPlaceAllotDAL.GetObject(ExamRoom.ExamPlaceAllotID.Value);
                        ExamPlace.ExamPersonNum = ExamPlace.ExamPersonNum.Value - ExamRoom.PersonNumber.Value;
                        ExamPlace.RoomNum = ExamPlace.RoomNum.Value - 1;
                        ExamPlaceAllotDAL.Update(ExamPlace);

                        //ExamRoomAllotDAL.UpdateExamRoomCode(ExamPlace.ExamPlanID.Value);//更新考场编号大排行
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "考场删除失败！", ex);
                        UIHelp.layerAlert(Page, "考场删除失败！");
                        return;
                    }
                    
                    RefreshAllotHelpInfo();                    
                    RadGridExamPlaceAllot.DataBind();
                    UIHelp.layerAlert(Page, "考场删除成功！",6,3000);
                }
            }
        }

        //刷新分配友好提示
        protected void RefreshAllotHelpInfo()
        {            
            if (ExamPlanSelect1.ExamPlanID.HasValue)
            {
                //已分配人数
                int allotRoomPersonNumber = ExamRoomAllotDAL.GetSumOfPersonNumber(ExamPlanSelect1.ExamPlanID.Value);

                //报名缴费确认人数
                int _sumSignUp = ExamSignUpDAL.SelectCount_New(string.Format(" and ExamPlanID={0} and Status='{1}'", ExamPlanSelect1.ExamPlanID.Value.ToString(),EnumManager.SignUpStatus.PayConfirmed));

                //待分配人数
                int waitAllot = (_sumSignUp - allotRoomPersonNumber) > 0 ? (_sumSignUp - allotRoomPersonNumber) : 0;

                LabelAllotHelp.Text = string.Format("（待考试人数{0}，已分配人数 {1}，待分配人数{2}）", _sumSignUp.ToString(), allotRoomPersonNumber.ToString(), waitAllot.ToString());
            }
            else
            {
                LabelAllotHelp.Text = "";
            }
        }        
    }
}