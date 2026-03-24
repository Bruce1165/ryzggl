using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;
using System.IO;

namespace ZYRYJG.CertifManage
{
    public partial class CertifChangeCheckConfirm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Link_DelHistory.HRef = "DelChangeHistory.aspx?o=" + Request["o"];

                if (ValidResourceIDLimit(RoleIDs, "CheckConfirmKeyWord") == false) P_CheckConfirmKeyWord.Visible = true; //修改特殊字段权限

                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                LabelPostType.Text = UIHelp.GetPostTypeNameByID(string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString());//岗位类别ID
                PostSelect1.LockPostTypeID();
                //if (PostSelect1.PostTypeID != "1" && PostSelect1.PostTypeID != "3")//三类人、造价员比对社保
                //{
                //    RadGrid1.MasterTableView.Columns.FindByUniqueName("sb").Visible = false;
                //}
                if (string.IsNullOrEmpty(Request["code"]) == false)//按通知编号（受理编号）查询
                {
                    ViewState["NoticeCode"] = Request["code"];
                    CheckConfirmAfter(Request["code"]);
                }
                else
                {
                    Form.DefaultButton = btnSearch.UniqueID;
                    btnSearch_Click(sender, e);
                    rdtxtApplyCode.Focus();


                }
            }
        }

        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            SetRadGridDisplayColumn();

            ClearGridSelectedKeys(RadGrid1);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
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
            if (rdtxtQYMC.Text.Trim() != "")   //原企业名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }
            if (RadTextBoxNewUnit.Text.Trim() != "")   //新企业名称
            {
                q.Add(string.Format("NewUnitName like '%{0}%'", RadTextBoxNewUnit.Text.Trim()));
            }

            if (RadTextBoxUnitCode.Text.Trim() != "")   //新企业机构代码
            {
                q.Add(string.Format("NewUnitcode like '{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }

            if (rdtxtApplyCode.Text.Trim() != "")   //申请批次号
            {
                q.Add(string.Format("ApplyCode like '%{0}%'", rdtxtApplyCode.Text.Trim()));
            }

            if (RadTextBoxApplyMan.Text.Trim() != "")//申请人
            {
                q.Add(string.Format("ApplyMan ='{0}'", RadTextBoxApplyMan.Text.Trim()));
            }

            if (RadTextBoxGetMan.Text.Trim() != "")//受理人
            {
                q.Add(string.Format("GetMan ='{0}'", RadTextBoxGetMan.Text.Trim()));
            }

            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));     //岗位类别
            //if (PostSelect1.PostID != "") q.Add(string.Format("PostID = '{0}'", PostSelect1.PostID));               //岗位工种
            //岗位工种
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID >= {0} and PostID <= {0}", PostSelect1.PostID));
                        break;
                }
            }

            int posttypeid = Request.QueryString["o"] == null ? 1 : Convert.ToInt32(Request.QueryString["o"].ToString());
            q.Add(string.Format("PostTypeID={0}", posttypeid));

            if (RadioButtonListStatus.SelectedItem.Value == "已审查")
            {
                q.Add("[GETDATE] >'2000-1-1'");
                ButtonConfirm.Visible = false;
                DivDealWay.Visible = false;
                DivOutput.Visible = true;
            }
            else//未审查
            {
                //if (CheckBoxViewUnUnitChecked.Checked == true)//单位已确认或个人申请了强制执行
                //{
                //    q.Add(string.Format("((Status = '{0}') or ( Status = '{1}' and ChangeRemark='申请强制执行'))", EnumManager.CertificateChangeStatus.Applyed, EnumManager.CertificateChangeStatus.WaitUnitCheck));
                //}
                //else//单位已确认
                //{
                //    q.Add(string.Format("Status = '{0}'", EnumManager.CertificateChangeStatus.Applyed));//已申请
                //}
                q.Add(string.Format("((Status = '{0}') or ( Status = '{1}' and ChangeRemark='申请强制执行'))", EnumManager.CertificateChangeStatus.Applyed, EnumManager.CertificateChangeStatus.WaitUnitCheck));
                ButtonConfirm.Visible = true;
                DivDealWay.Visible = false;
                DivOutput.Visible = false;
            }

            if (RadComboBoxChangeType.SelectedItem.Value != "")//变更类型
            {
                q.Add(string.Format("ChangeType='{0}'", RadComboBoxChangeType.SelectedItem.Value));
            }
            if (RadComboBoxDealWay.SelectedItem.Value != "")//处理方式
            {
                q.Add(string.Format("DealWay='{0}'", RadComboBoxDealWay.SelectedItem.Value));
            }
            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
            {
                q.Add(string.Format("[GETDATE]>='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
            {
                q.Add(string.Format("[GETDATE]<'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;

        }

        System.Collections.Generic.Dictionary<string, int> unitChaiqianLimit = new Dictionary<string, int>();//企业最低拆迁员人数要求
        System.Collections.Generic.Dictionary<string, int> unitChaiqianCount = new Dictionary<string, int>();//企业当前拆迁员人数

        //检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查）
        //规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制        
        public bool CheckChaiQianLimit(string ChangeType, string UnitCode, string NewUnitCode, string UnitName)
        {
            if (ChangeType == "离京变更" || ChangeType == "注销"
                || (ChangeType == "京内变更" && UnitCode != NewUnitCode)
                || (ChangeType == "补办" && UnitCode != NewUnitCode))
            {
                if (unitChaiqianLimit.ContainsKey(UnitCode) == false)
                {
                    int workersCountLimit = UIHelp.QueryCaiQianCountLimitOfUnit(Page, UnitCode);//最低人数要求

                    if (workersCountLimit == 0) return true;

                    unitChaiqianLimit.Add(UnitCode, workersCountLimit);

                    int workersCount = CertificateDAL.SelectCount(string.Format(" and UnitCode='{1}' and PostID=55 and ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd"), UnitCode));
                    unitChaiqianCount.Add(UnitCode, workersCount);
                }

                unitChaiqianCount[UnitCode] -= 1;
                if (unitChaiqianCount[UnitCode] < unitChaiqianLimit[UnitCode])
                {
                    UIHelp.layerAlert(Page, string.Format("根据企业资质要求，本次操作将导致企业“{1}”拆迁员数量低于最低人数（{0}人）要求，无法完成此操作！", unitChaiqianLimit[UnitCode].ToString(), UnitName));
                    return false;
                }
            }
            return true;
        }

        //审查决定
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            //if (rdZSCLFS.SelectedValue == "")
            //{
            //    UIHelp.layerAlert(Page, "请选择证书处理方式！");
            //    return;
            //}

            //UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");//更新选择状态
            //if (IsGridSelected(RadGrid1) == false)
            //{
            //    UIHelp.layerAlert(Page, "至少选择一条数据！");
            //    return;
            //}

            //ViewState["certificateChangeIDList"] = null;

            //int RowCount = 0;
            //string filterString = "";//过滤条件
            //string PostID = "";//变更岗位工种
            //string ChangeType = "";//变更类型
            //DataTable dt = null;//变更申请            

            //if (GetGridIfCheckAll(RadGrid1) == true)//全选
            //{
            //    filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            //}
            //else
            //{
            //    if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
            //        filterString = string.Format(" {0} and CertificateChangeID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //    else//包含
            //        filterString = string.Format(" {0} and CertificateChangeID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            //}

            //try
            //{
            //    dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, filterString, "CertificateChangeID");
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "审查决定失败！", ex);
            //    return;
            //}

            //#region 规则检查

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (ChangeType == "")
            //    {
            //        ChangeType = dt.Rows[i]["ChangeType"].ToString();
            //    }
            //    else if (ChangeType != dt.Rows[i]["ChangeType"].ToString())
            //    {
            //        UIHelp.layerAlert(Page, "您勾选的不是同一“变更类型”数据，不能同时受理，请重新查询过滤！");
            //        return;
            //    }
            //    if (PostID == "")
            //    {
            //        PostID = dt.Rows[i]["PostID"].ToString();
            //    }
            //    else if (PostID != dt.Rows[i]["PostID"].ToString())
            //    {
            //        //造价员当处理方式选择“证书信息修改”时不分专业（因为不打印受理通知书），只要搜索到单位名称，核对人员姓名就可以批量审批，这样可以减少工作环节，减轻工作量。
            //        if ((PostID == "9" && dt.Rows[i]["PostID"].ToString() == "12")
            //            || (PostID == "12" && dt.Rows[i]["PostID"].ToString() == "9"))//9土建,12安装
            //        {
            //            if (rdZSCLFS.SelectedValue == "证书信息修改") continue;
            //        }

            //        UIHelp.layerAlert(Page, "您勾选的不是同一“岗位工种”数据，不能同时受理，请重新查询过滤！");
            //        return;
            //    }

            //    RowCount++;
            //    AddcertifIDToList(dt.Rows[i]["CertificateChangeID"].ToString());

            //    //检查拆迁企业拆迁员最低人数限制（注销时或变更单位时检查）
            //    if (dt.Rows[i]["PostID"].ToString() == "55")
            //    {
            //        if (CheckChaiQianLimit(dt.Rows[i]["ChangeType"].ToString(), dt.Rows[i]["UnitCode"].ToString(), dt.Rows[i]["NewUnitCode"].ToString(), dt.Rows[i]["UnitName"].ToString()) == false)
            //        {
            //            return;
            //        }
            //    }
            //}

            //#endregion 规则检查

            //string _path = "";
            //string _sourcePath = "";
            //DBHelper dbhelper = new DBHelper();
            //DbTransaction dtr = dbhelper.BeginTransaction();
            //try
            //{
            //    string bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
            //    string bgsh = UIHelp.GetNextBatchNumber(dtr, "BGSH"); //变更审核编批号
            //    string bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
            //    string bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号
            //    ViewState["NoticeCode"] = bggz;

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        CertificateChangeOB certhfchange = CertificateChangeDAL.GetObject(null, (long)dt.Rows[i]["CertificateChangeID"]);
            //        certhfchange.GetDate = DateTime.Now;   //变更受理时间
            //        certhfchange.GetResult = "通过";     //变更受理结论
            //        certhfchange.GetMan = PersonName;    //变更受理人
            //        certhfchange.GetCode = bgslp;//变更受理编批号
            //        certhfchange.CheckDate = DateTime.Now;  //变更审核时间
            //        certhfchange.CheckResult = "通过";  //变更审核结论
            //        certhfchange.CheckMan = PersonName; //变更审核人
            //        certhfchange.CheckCode = bgsh;    //变更审核批号
            //        certhfchange.DealWay = rdZSCLFS.SelectedValue.Trim();     //证书处理方式 
            //        certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
            //        certhfchange.ConfrimResult = "通过";  //变更决定结论
            //        certhfchange.ConfrimMan = PersonName;   //变更决定人
            //        certhfchange.ConfrimCode = bgjd;   //变更决定批号
            //        certhfchange.Status = EnumManager.CertificateChangeStatus.Noticed;     //告知状态
            //        certhfchange.NoticeDate = DateTime.Now; //变更告知时间 
            //        ViewState["rq"] = certhfchange.NoticeDate;
            //        certhfchange.NoticeResult = "通过";  //变更告知结论
            //        certhfchange.NoticeMan = PersonName;    //变更告知人
            //        certhfchange.NoticeCode = bggz;   //变更告知批号
            //        certhfchange.ModifyPersonID = PersonID;    //最后修改人
            //        certhfchange.ModifyTime = DateTime.Now; ;   //最后修改时间



            //        //修该变更记录
            //        CertificateChangeDAL.Update(dtr, certhfchange);

            //        //根据证书id向历史表插入历史数据
            //        CertificateHistoryDAL.InsertChangeHistory(dtr, Convert.ToInt32(certhfchange.CertificateID));

            //        //修改原表数据
            //        CertificateOB certificateob = CertificateDAL.GetObject(certhfchange.CertificateID.Value);

            //        //更换证书照片
            //        if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
            //        {
            //            _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));
            //            if (!Directory.Exists(Server.MapPath(_path)))
            //            {
            //                System.IO.Directory.CreateDirectory(Server.MapPath(_path));
            //            }
            //            _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
            //            _sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
            //            File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
            //            certificateob.FacePhoto = _path.Replace("..", "~");

            //        }
            //        //补充老照片
            //        if (string.IsNullOrEmpty(certificateob.FacePhoto) == true)
            //        {
            //            if (string.IsNullOrEmpty(certhfchange.WorkerCertificateCode) == false && certhfchange.WorkerCertificateCode.Length > 2)
            //            {
            //                _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.WorkerCertificateCode.Substring(certhfchange.WorkerCertificateCode.Length - 3, 3), certhfchange.WorkerCertificateCode);
            //            }
            //            else
            //            {
            //                _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.NewWorkerCertificateCode.Substring(certhfchange.NewWorkerCertificateCode.Length - 3, 3), certhfchange.NewWorkerCertificateCode);
            //            }

            //            _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));

            //            if (!Directory.Exists(Server.MapPath(_path)))
            //            {
            //                System.IO.Directory.CreateDirectory(Server.MapPath(_path));
            //            }
            //            _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
            //            if (File.Exists(Server.MapPath(_sourcePath)) == true)//立即同步照片
            //            {

            //                File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
            //                certificateob.FacePhoto = _path.Replace("..", "~");
            //            }
            //            else//夜间同步照片
            //            {
            //                certificateob.FacePhoto = null;
            //            }
            //        }

            //        if (P_CheckConfirmKeyWord.Visible == false)//修改特殊字段权限
            //        {
            //            certificateob.WorkerCertificateCode = certhfchange.NewWorkerCertificateCode;   //证件号码
            //            certificateob.Birthday = certhfchange.NewBirthday;//出生日期
            //            certificateob.Sex = certhfchange.NewSex;//性别
            //        }
            //        certificateob.WorkerName = certhfchange.NewWorkerName;    //姓名
            //        certificateob.UnitName = certhfchange.NewUnitName;   //工作单位
            //        certificateob.UnitCode = certhfchange.NewUnitCode;   //组织机构代码
            //        certificateob.ModifyPersonID = certhfchange.ModifyPersonID;  //最后修改人
            //        certificateob.ModifyTime = DateTime.Now;   //最后修改时间
            //        certificateob.CheckDate = certhfchange.CheckDate;    //审批时间
            //        certificateob.CheckMan = certhfchange.CheckMan;      //审批人
            //        certificateob.CheckAdvise = certhfchange.CheckResult;//审批意见
            //        certificateob.Status = certhfchange.ChangeType;      //证书更新状态（变更类型）
            //        //certificateob.PrintDate = null;//打印时间
            //        //certificateob.PrintMan = null;//打印人

            //        if (rdZSCLFS.SelectedValue.Trim() != "重新制作证书")
            //        {
            //            certificateob.CaseStatus = "已归档";//归档状态                        
            //        }
            //        else
            //        {
            //            certificateob.CaseStatus = null;//归档状态
            //            //更新证书打印次数
            //            certificateob.PrintCount = (certificateob.PrintCount.HasValue == false ? 1 : certificateob.PrintCount.Value + 1);
            //        }

            //        certificateob.ApplyMan = certhfchange.ApplyMan;//申请人
            //        CertificateDAL.Update(dtr, certificateob);

            //        //更新人员基本信息
            //        WorkerOB _WorkerOB = WorkerDAL.GetUserObject(certificateob.WorkerCertificateCode);
            //        if (_WorkerOB != null)//update
            //        {
            //            if (_WorkerOB.Birthday != certificateob.Birthday
            //                || _WorkerOB.Sex != certificateob.Sex
            //                || _WorkerOB.WorkerName != certificateob.WorkerName
            //                )
            //            {
            //                _WorkerOB.Birthday = certificateob.Birthday;
            //                _WorkerOB.Sex = certificateob.Sex;
            //                _WorkerOB.WorkerName = certificateob.WorkerName;
            //                WorkerDAL.Update(dtr, _WorkerOB);
            //            }
            //        }
            //        else//new
            //        {
            //            _WorkerOB = new WorkerOB();
            //            _WorkerOB.Birthday = certificateob.Birthday;
            //            _WorkerOB.Sex = certificateob.Sex;
            //            _WorkerOB.WorkerName = certificateob.WorkerName;
            //            _WorkerOB.Phone = certhfchange.LinkWay;
            //            _WorkerOB.CertificateCode = certificateob.WorkerCertificateCode;
            //            if (certificateob.WorkerCertificateCode.Length == 15 || certificateob.WorkerCertificateCode.Length == 18)
            //                _WorkerOB.CertificateType = "身份证";
            //            else
            //                _WorkerOB.CertificateType = "其它证件";
            //            WorkerDAL.Insert(dtr, _WorkerOB);
            //        }
            //    }

            //    dtr.Commit();

            //    UIHelp.WriteOperateLog(PersonName, UserID, "审查决定证书变更", string.Format("处理方式：{0}；变更告知批号：{1}；证书数量：{2}本。"
            //   , rdZSCLFS.SelectedValue, bggz, dt.Rows.Count.ToString()));
            //}
            //catch (Exception ex)
            //{
            //    dtr.Rollback();
            //    UIHelp.WriteErrorLog(Page, "审查决定失败！", ex);
            //    return;
            //}
            //ClearGridSelectedKeys(RadGrid1);
            //if (rdZSCLFS.SelectedValue.Trim() == "证书信息修改")
            //{
            //    certificateChangeIDList.Clear();
            //    UIHelp.layerAlert(Page, string.Format("您已经成功的审查决定并办结了 {0} 条数据！", RowCount.ToString()), 6, 3000);
            //    RadGrid1.DataBind();//刷新grid
            //}
            //else
            //{
            //    CheckConfirmAfter("");//显示受理通知单界面
            //}
            //rdZSCLFS.SelectedIndex = -1;//恢复默认未选处理方式
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "CertificateChangeID");
        }

        //删除无效申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            int deleteRows = 0;
            string filterString = "";//过滤条件
            if (GetGridIfCheckAll(RadGrid1) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
                    filterString = string.Format(" {0} and CertificateChangeID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and CertificateChangeID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }

            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                DataTable dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, filterString, "CertificateChangeID");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CertificateChangeDAL.Delete(dtr, (long)dt.Rows[i]["CertificateChangeID"]);
                    deleteRows++;
                    sb.Append("、").Append(dt.Rows[i]["CertificateCode"].ToString());
                }
                dtr.Commit();

                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.WriteOperateLog(PersonName, UserID, "审查决定删除证书变更", string.Format("证书数量：{0}本；证书编号：{1}。", deleteRows.ToString(), sb.ToString()));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "审查决定失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, string.Format("您已经成功的删除了 {0}条数据！", deleteRows.ToString()), 6, 3000);
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();//刷新grid
        }

        //设置grid显示列
        private void SetRadGridDisplayColumn()
        {
            if (DivSearch.Visible == false //查看通知单
                || RadioButtonListStatus.SelectedItem.Value == "未审查")//查询未审查数据
            {
                RadGrid1.Columns.FindByUniqueName("NoticeCode").Visible = false;
                RadGrid1.Columns.FindByUniqueName("GetMan").Visible = false;
                RadGrid1.Columns.FindByUniqueName("GetDate").Visible = false;
                RadGrid1.Columns.FindByUniqueName("PostName").Visible = false;
                if (DivSearch.Visible == false)
                    RadGrid1.Columns.FindByUniqueName("SelectAllColumn").Display = false;
                else
                    RadGrid1.Columns.FindByUniqueName("SelectAllColumn").Display = true;
            }
            else
            {
                RadGrid1.Columns.FindByUniqueName("NoticeCode").Visible = true;
                RadGrid1.Columns.FindByUniqueName("GetMan").Visible = true;
                RadGrid1.Columns.FindByUniqueName("GetDate").Visible = true;
                RadGrid1.Columns.FindByUniqueName("PostName").Visible = true;
                RadGrid1.Columns.FindByUniqueName("SelectAllColumn").Display = false;
            }

            if (DivSearch.Visible == false //查看通知单
                || RadioButtonListStatus.SelectedItem.Value == "已审查")
            {
                RadGrid1.Columns.FindByUniqueName("DealWay").Visible = true;
            }
            else
            {
                RadGrid1.Columns.FindByUniqueName("DealWay").Visible = false;
            }

        }

        //证书批量变更申请ID集合
        public List<string> certificateChangeIDList
        {
            get
            {
                if (ViewState["certificateChangeIDList"] == null)
                    return new List<string>();
                else
                    return ViewState["certificateChangeIDList"] as List<string>;
            }
        }

        //添加批量申请证书号
        public void AddcertifIDToList(string certifID)
        {
            List<string> _certificateChangeIDList = certificateChangeIDList;
            if (_certificateChangeIDList.Contains(certifID) == false) _certificateChangeIDList.Add(certifID);
            ViewState["certificateChangeIDList"] = _certificateChangeIDList;
        }

        //移除批量申请证书号
        public void RemovecertifIDToList(string certifID)
        {
            List<string> _certificateChangeIDList = certificateChangeIDList;
            if (_certificateChangeIDList.Contains(certifID) == true) _certificateChangeIDList.Remove(certifID);
            ViewState["certificateChangeIDList"] = _certificateChangeIDList;
        }

        //决定成功后显示本次批次数据，提供“打印受理通知书”
        protected void CheckConfirmAfter(string NoticeCode)
        {
            DivSearch.Visible = false;//查询条件面板
            ButtonConfirm.Visible = false;//审查决定
            DivDealWay.Visible = false;//处理方式面板
            btnPrint.Visible = true;//打印通知书
            ButtonReturn.Visible = true;//返回   
            //DivOutput.Visible = false;

            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (NoticeCode == "")
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string s in certificateChangeIDList)
                {
                    sb.Append(",").Append(s);
                }
                if (sb.Length > 0)
                    sb.Remove(0, 1);
                else
                    sb.Append("0");
                q.Add(string.Format("CertificateChangeID in({0})", sb.ToString()));
            }
            else//按通知编号（受理编号）查询
            {
                q.Add(string.Format("NoticeCode ='{0}'", NoticeCode));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
            RadGrid1.DataBind();
            SetRadGridDisplayColumn();
        }

        //打印受理通知书
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/变更受理通知书.doc", "~/Template/变更受理通知书2.doc"
                , string.Format("~/UpLoad/CertifChangeAccept/变更受理通知书_{0}.doc", PersonID.ToString())
                , GetPrintData());
            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertifChangeAccept/变更受理通知书_{0}.doc');", PersonID.ToString(), RootUrl), true);
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifChangeAccept/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifChangeAccept/"));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            int pageNo = 0;//第几页
            int cellNo = 0;//当前页第几单元格
            string rowNo = "";//当前第几行
            System.Text.StringBuilder unitList = new System.Text.StringBuilder();//单位名称集合
            string unitName = "";//单位名称
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);

            DataTable dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, sb.ToString(), "CertificateChangeID");
            string _NoticeDate = Convert.ToDateTime(dt.Rows[0]["NoticeDate"]).ToString("yyyy-MM-dd");
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

                        printData.Add("Data_bggz", ViewState["NoticeCode"].ToString());//批次号(告知批号)
                        printData.Add("Data_NoticeDate", _NoticeDate);//日期
                        printData.Add("ChangeType", dt.Rows[i]["ChangeType"].ToString());//变更类型(申办事项)
                        printData.Add("PostTypeName", dt.Rows[i]["PostTypeName"].ToString());//岗位（证书类别）
                        printData.Add("GetMan", dt.Rows[i]["GetMan"].ToString());//受理人
                        printData.Add("CertificateCount", dt.Rows.Count.ToString());//件数
                    }
                    rowNo = i.ToString();
                    printData.Add(string.Format("tr{0}", rowNo), dt.Rows[i]["RowNum"].ToString());//序号
                    printData.Add(string.Format("Name{0}", rowNo), dt.Rows[i]["NewWorkerName"].ToString());//姓名
                    printData.Add(string.Format("CertificateCode{0}", rowNo), dt.Rows[i]["CertificateCode"].ToString());//证书编号                
                    printData.Add(string.Format("UnitName{0}", rowNo), dt.Rows[i]["NewWorkerCertificateCode"].ToString());//证件号码


                }
                else//非首页
                {
                    cellNo = (i - 5) % 30 + 1;
                    if ((i - 5) % 30 == 0)
                    {
                        printData = new Dictionary<string, string>();
                        list.Add(printData);
                        pageNo++;

                        printData.Add("Data_bggz", ViewState["NoticeCode"].ToString());//批次号(告知批号)
                        printData.Add("Data_NoticeDate", _NoticeDate);//日期
                    }
                    rowNo = ((i - 5) % 30).ToString();
                    printData.Add(string.Format("tr{0}", rowNo), dt.Rows[i]["RowNum"].ToString());//序号
                    printData.Add(string.Format("Name{0}", rowNo), dt.Rows[i]["NewWorkerName"].ToString());//姓名
                    printData.Add(string.Format("CertificateCode{0}", rowNo), dt.Rows[i]["CertificateCode"].ToString());//证书编号                
                    printData.Add(string.Format("UnitName{0}", rowNo), dt.Rows[i]["NewWorkerCertificateCode"].ToString());//证件号码
                }

                //单位名称:所有不同单位名称集合
                if (unitName == "")
                {
                    unitName = dt.Rows[i]["NewUnitName"].ToString();
                    unitList.Append(unitName);
                }
                else if (unitName != dt.Rows[i]["NewUnitName"].ToString())
                {
                    unitName = dt.Rows[i]["NewUnitName"].ToString();
                    unitList.Append("，").Append(unitName);
                }

                if (i == dt.Rows.Count - 1)
                {
                    list[0].Add("NewUnitName", unitList.ToString());//单位名称
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
            certificateChangeIDList.Clear();
            DivSearch.Visible = true;//查询条件面板
            ButtonConfirm.Visible = true;//审查决定
            DivDealWay.Visible = false;//处理方式面板
            btnPrint.Visible = false;//打印通知书
            ButtonReturn.Visible = false;//返回   
            RadGrid1.Columns.FindByUniqueName("SelectAllColumn").Display = true;


            btnSearch_Click(sender, e);
        }

        //导出列表（excel），统计工作量
        protected void ButtonExportExcel_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGrid1.MasterTableView.Columns.FindByUniqueName("NoticeCode").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("RowNum").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("NoticeCode").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("CertificateCode").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("CertificateChangeID").Visible = false;

            RadGrid1.MasterTableView.Columns.FindByUniqueName("Output_NoticeCode").Visible = true;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("Output_CertificateCode").Visible = true;
            RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.Rebind();
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //导出excel格式化
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "NewWorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "RowNum")
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

    }
}
