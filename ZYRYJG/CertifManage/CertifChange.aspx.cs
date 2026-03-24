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
    //有效性检查
    //1、一年内最多5次变更单位（项目负责人变更特殊：当大于5次变更时，如果变更前不匹配，变更后匹配允许变更）；
    //2、已提交变更申请未审批的不能重复提交；
    //3、正在办理续期的，续期未结束不能办理变更；
    //4、证书处于锁定中，不允许变更（内部锁）；
    //5、证书为尚未发放状态（未打印），不允许变更电子版；
    //6、检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查），规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制
    //7、检查物业项目负责人是否被锁定（外部锁),（离京变更、注销或变更单位时检查），锁定时不能提交变更；
    //8、只有“安全生产考核三类人员”和“造价员”才能申请证书离京，其它（特种作业、职业技能、专业技术人员）不提供离京变更功能。
    //9、特殊：合同员不提供变更服务。
    //10、三类人员证书变更规则（只涉及京内变更、补办。离京变更和注销无限制）：
    //    10.1、组织机构代码和企业名称无变化，其它4项（姓名、证件号码、性别、出生日期）可以变更；
    //    10.2、组织机构代码不变，企业名称变更，检查新企业名称与组织机构代码是否与本（外）地企业资质库一致，一致允许变更；（相当于名称修正）
    //    10.3、组织机构代码变更，检查新企业名称与新组织机构代码是否与本地企业资质库一致，一致允许变更；（只检查本地资质库，即不允许变更到外地企业）
    //    10.4、组织机构代码变更，证书类型为项目负责人（B本）证书，检查变更后企业及人员信息是否与本地建造师（含外地进京备案建造师）企业信息和人员信息是否一致，一致允许变更；（即有B本必须有建造师，且单位、人员信息一致）

    public partial class CertifChange : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("3").Remove();//屏蔽特种作业类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别
            base.OnInit(e);
        }
        //证书变更申请列表
        public List<string> certifIDList
        {
            get
            {
                if (ViewState["certifIDList"] == null)
                    return new List<string>();
                else
                    return ViewState["certifIDList"] as List<string>;
            }
        }

        /// <summary>
        /// 变更类型
        /// j：京内变更
        /// z：注销
        /// l：离京变更
        /// </summary>
        public string ChangeType
        {
            get
            {
                return Request["t"];
            }
     
        }

        //向List<string>添加一条数据
        public void AddcertifIDToList(string certifID)
        {
            List<string> _certifIDList = certifIDList;
            if (_certifIDList.Contains(certifID) == false) _certifIDList.Add(certifID);
            ViewState["certifIDList"] = _certifIDList;
        }

        //从List<string>添加一条数据
        public void deletecertifIDToList(string certifID)
        {
            List<string> _certifIDList = certifIDList;
            if (_certifIDList.Contains(certifID) == true) _certifIDList.Remove(certifID);
            ViewState["certifIDList"] = _certifIDList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request.QueryString["t"])
                {
                    case "j":
                        LabelApply.Text = "京内变更";
                        break;
                    case "z":
                        LabelApply.Text = "注销";
                        break;
                    case "l":
                        LabelApply.Text = "离京变更";
                        LabelTip.Text = "（提示：离京申请办结后，请到电子证书下载页面下载打印离京证明。）";
                        break;
                }

                UIHelp.FillDropDownList(RadTextBoxChangeType, "101");
                RadTextBoxChangeType.Items.FindItemByValue("进京变更").Remove();
                RadTextBoxChangeType.Items.FindItemByValue("补办").Remove();

                RadTextBoxChangeType.Items.Insert(0, new RadComboBoxItem("请选择", ""));
                ViewState["PostTypeID"] = Request["o"];//用于返回按钮传递参数
                //格式化查询参数面板
                if (PersonType != 2 && PersonType != 3 && PersonType != 4 && string.IsNullOrEmpty(Request["o"]) == false)//按岗位分类
                {
                    PostSelect1.PostTypeID = Request["o"].ToString();
                    PostSelect1.LockPostTypeID();
                }
                switch (PersonType)
                {
                    case 2://考生
                        tableSearchParms.Visible = false;
                        rdtxtWorkerName.Enabled = false;
                        ButtonEdit.Visible = false;//考生屏蔽批量变更
                        break;
                    case 3://企业
                        TrUnitQuery.Visible = false;//屏蔽与企业相关查询条件
                        break;
                    case 4://培训点
                        tableSearchParms.Visible = false;
                        RadGridCertificate.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                        //RadGridCertificate.MasterTableView.Columns.FindByUniqueName("Apply").Visible = false;
                        LabelTip.Text = "（提示：逐条按“姓名+证书编号”精确匹配查询证书，然后选择批量申请或单条申请。）";
                        break;
                }

                ButtonSearch_Click(sender, e);

                //if (PersonType == 2)
                //{
                //    //UIHelp.layerAlert(Page, "根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作");
                //    UIHelp.layerAlert(Page, "系统提示：根据有关规定，停止造价员、专业管理人员考核、变更和续期、电子证书打印工作。",0,5000);
                //}
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
            ClearGridSelectedKeys(RadGridCertificate);

            QueryParamOB q = new QueryParamOB();

            //过滤证书状态为“待审核”
            q.Add(string.Format("Status <>'{0}' and Status <>'{1}'", EnumManager.CertificateUpdateType.WaitCheck, EnumManager.CertificateUpdateType.EnterWaitCheck));
            q.Add("(PostTypeID <5)");//专业管理人员
            switch (Request.QueryString["t"])
            {
                case "j":
                    q.Add(string.Format("(CHANGETYPE ='{0}' or CHANGETYPE is null)",EnumManager.CertificateUpdateType.ChangeInBeiJing));//京内变更
                  
                    break;
                case "z":
                   
                    q.Add(string.Format("(CHANGETYPE ='{0}' or CHANGETYPE is null)",EnumManager.CertificateUpdateType.Logout));//
                    break;
                case "l":
                  
                    q.Add(string.Format("(CHANGETYPE ='{0}' or (PostTypeID=1 and CHANGETYPE is null))",EnumManager.CertificateUpdateType.OutBeiJing));//
                    break;
                default:
                    q.Add(string.Format("(CHANGETYPE ='{0}' or CHANGETYPE is null)",EnumManager.CertificateUpdateType.Patch));//
                    break;

            }

            ////申请状态
            //if (RadioButtonListStatus.SelectedValue == "0")//未申请
            //{
            //    q.Add("(PostTypeID <5)");//专业管理人员

            //    RadGridCertificate.Columns.FindByUniqueName("Apply").Visible = true;
            //    RadGridCertificate.Columns.FindByUniqueName("Detail").Visible = false;
            //    RadGridCertificate.Columns.FindByUniqueName("ApplyCode").Visible = false;
            //    RadGridCertificate.Columns.FindByUniqueName("sb").Visible = false;
            //    if (PersonType != 2) ButtonEdit.Visible = true;//批量变更按钮
            //    ButtonExit.Visible = false;
            //    q.Add("CertificateChangeID =0");
            //}
            //else//已申请
            //{
            //    ButtonEdit.Visible = false;

            //    ButtonExit.Visible = true;
            //    RadGridCertificate.Columns.FindByUniqueName("Apply").Visible = false;
            //    RadGridCertificate.Columns.FindByUniqueName("Detail").Visible = true;
            //    RadGridCertificate.Columns.FindByUniqueName("ApplyCode").Visible = true;
            //    RadGridCertificate.Columns.FindByUniqueName("sb").Visible = true;
            //    q.Add("CertificateChangeID >0");
            //}

            if (tableSearchParms.Visible == true)
            {
                if (rbl.SelectedValue == "0")
                {
                    ViewState["Status"] = "未过期";
                    q.Add(string.Format("ValidEndDate >='{0}' and Status <> '{1}' and Status <> '{2}' "
                        , DateTime.Now.ToString("yyyy-MM-dd"), EnumManager.CertificateUpdateType.OutBeiJing, EnumManager.CertificateUpdateType.Logout));
                }
                else
                {
                    ViewState["Status"] = "已过期";//（含注销、离京）
                    q.Add(string.Format("(ValidEndDate <'{0}' or Status = '{1}' or Status = '{2}')"
                        , DateTime.Now.ToString("yyyy-MM-dd"), EnumManager.CertificateUpdateType.OutBeiJing, EnumManager.CertificateUpdateType.Logout));
                }
            }

            switch (PersonType)
            {
                case 2://考生，看到自己证书
                    //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    //string IDCard15 = "";
                    //string IDCard18 = "";
                    //if (_WorkerOB == null)
                    //{
                    //    IDCard15 = IDCard18 = "null";
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 15)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 18)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //else
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = _WorkerOB.CertificateCode;

                    //}
                    //q.Add(string.Format("(WORKERCERTIFICATECODE='{0}' or WORKERCERTIFICATECODE='{1}')", IDCard15, IDCard18));

                    q.Add(string.Format("WORKERCERTIFICATECODE='{0}'", WorkerCertificateCode));

                    q.Add(string.Format("(ValidEndDate >='{0}' and Status <> '{1}' and Status <> '{2}')"
                        , DateTime.Now.ToString("yyyy-MM-dd"), EnumManager.CertificateUpdateType.OutBeiJing, EnumManager.CertificateUpdateType.Logout));

                    ////临时增加两个月变更时间
                    //q.Add(string.Format("(ValidEndDate >='{0}' and Status <> '{1}' and Status <> '{2}')"
                    //    , DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"), EnumManager.CertificateUpdateType.OutBeiJing, EnumManager.CertificateUpdateType.Logout));


                    break;
                //case 4://培训点，按证书编号和姓名查询并申请
                //    string name = rdtxtWorkerName.Text.Trim();
                //    string code = rdtxtCertificateCode.Text.Trim();//证书编号
                //    string ToWhereString = string.Format(q.ToWhereString() + " and CERTIFICATECODE = '{0}' and WorkerName = '{1}'", code, name);
                //    DataTable dt = CertificateChangeDAL.GetListView(0, int.MaxValue - 1, ToWhereString, "CertificateID");
                //    QueryParamOB qp = new QueryParamOB();
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (RadioButtonListStatus.SelectedValue == "0")//未申请
                //        {
                //            if (DateTime.Compare(Convert.ToDateTime(dt.Rows[0]["VALIDENDDATE"]).AddDays(1), DateTime.Now) < 1)
                //            {
                //                UIHelp.layerAlert(Page, "证书已过期，不能变更！");
                //                return;
                //            }
                //            if (dt.Rows[0]["Status"].ToString() == EnumManager.CertificateUpdateType.OutBeiJing)
                //            {
                //                UIHelp.layerAlert(Page, "证书已做过离京变更，无法申请！");
                //                return;
                //            }
                //            if (dt.Rows[0]["Status"].ToString() == EnumManager.CertificateUpdateType.Logout)
                //            {
                //                UIHelp.layerAlert(Page, "证书已注销，无法申请！");
                //                return;
                //            }

                //            int applyingCount = CertificateChangeDAL.SelectCount(string.Format(" and CertificateID={0} and [STATUS]='{1}'", Convert.ToString(dt.Rows[0]["CertificateID"]), EnumManager.CertificateChangeStatus.Applyed));
                //            if (applyingCount > 0)
                //            {
                //                UIHelp.layerAlert(Page, "证书已经申请过变更，不能重复申请！");
                //                return;
                //            }
                //            if (RadGridCertificate.MasterTableView.Items.Count > 0 && RadGridCertificate.MasterTableView.DataKeyValues[0]["UnitCode"].ToString() != dt.Rows[0]["UnitCode"].ToString())
                //            {
                //                UIHelp.layerAlert(Page, "只有相同的企业才能进行批量变更！");
                //                return;
                //            }
                //        }
                //        AddcertifIDToList(dt.Rows[0]["CertificateID"].ToString());
                //    }

                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    foreach (string s in certifIDList)
                //    {
                //        sb.Append(",").Append(s);
                //    }
                //    if (sb.Length > 0) sb.Remove(0, 1);
                //    if (RadioButtonListStatus.SelectedValue == "0")//未申请
                //    {
                //        q.Add(string.Format("CertificateID in({0})", (sb.Length > 0) ? sb.ToString() : "0"));
                //    }
                //    else//已申请
                //    {
                //        if (dt.Rows.Count > 0) q.Add(string.Format("CertificateID ={0}", dt.Rows[0]["CertificateID"].ToString()));
                //        q.Add(string.Format("CertificateID in(select CertificateID from dbo.CertificateChange where CreatePersonID = '{0}' and Status='{1}')", PersonID.ToString(), EnumManager.CertificateChangeStatus.Applyed));//自己创建的
                //    }

                //    break;
                default:
                    if (PersonType == 3)//企业，只能看到本单位人员证书
                    {
                        q.Add(string.Format("UnitCode='{0}'", ZZJGDM));
                    }
                    else//管理者，可按企业名称、组织代码查询
                    {
                        if (rdtxtQYMC.Text.Trim() != "")   //企业名称
                        {
                            q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
                        }
                        if (rdtxtUnitCode.Text.Trim() != "")   //企业组织机构代码
                        {
                            q.Add(string.Format("UnitCode ='{0}'", rdtxtUnitCode.Text.Trim()));
                        }
                    }

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

                    //证书有效期结束时间
                    if (!txtValidStartDate.IsEmpty)
                    {
                        q.Add(string.Format("ValidEndDate>='{0}'", txtValidStartDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
                    }
                    if (!txtValidEndtDate.IsEmpty)
                    {
                        q.Add(string.Format("ValidEndDate<'{0}'", txtValidEndtDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                    }
                    if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));     //岗位类别
                    //岗位工种
                    if (PostSelect1.PostID != "")
                    {
                        switch (PostSelect1.PostID)
                        {
                            case "9"://土建
                                q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增土建')", PostSelect1.PostID));
                                break;
                            case "12"://安装
                                q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增安装')", PostSelect1.PostID));
                                break;
                            default:
                                q.Add(string.Format("PostID >= {0} and PostID <= {0}", PostSelect1.PostID));
                                break;
                        }
                    }
                    break;
            }

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridCertificate.CurrentPageIndex = 0;
            RadGridCertificate.DataSourceID = ObjectDataSource1.ID;
            //RadGridCertificate.DataBind();

            if (PostSelect1.PostTypeID == "3"
                 || PostSelect1.PostID == "9"
                 || PostSelect1.PostID == "12"
                 || PostSelect1.PostID == "55"
                 || PostSelect1.PostID == "159"
                 || PostSelect1.PostID == "1009"
                 || PostSelect1.PostID == "1021"
                 || PostSelect1.PostID == "1024"
                 )
            {
                UIHelp.layerAlert(Page, "根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作");
                return;
            }

            if (IsPostBack == true)
            {
                if (PersonType == 2 )//未申报
                {
                    if (RadGridCertificate.MasterTableView.VirtualItemCount == 0)
                    {
                        string AlertInfo = "提示：您查询的个人可能未取得北京市住建委核发的证书、可能是老证书未录入系统、也可能企业组织机构代码或人员证件号码与证书电子信息不匹配，请先在北京市住建委网站专业人员查询中核实，再咨询55598091。";
                        UIHelp.layerAlert(Page, AlertInfo);
                    }
                }
            }
        }

        //Grid换页
        protected void RadGridCertificate_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridCertificate_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridCertificate, "CertificateID");
        }

        //填写批量申请内容
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
            ViewState["GetGridIfCheckAll"] = GetGridIfCheckAll(RadGridCertificate);//是否全选
            ViewState["GetGridIfSelectedExclude"] = GetGridIfSelectedExclude(RadGridCertificate);//是否为排除模式

            if (IsGridSelected(RadGridCertificate) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择证书！");
                return;
            }
            if (ViewState["Status"] != null && ViewState["Status"].ToString() == "已过期")
            {
                UIHelp.layerAlert(Page, "证书已过期（含注销、离京），不能进行证书变更!");
                return;
            }

            long CertificateID = 0;
            string filterString = "";
            if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
            {
                CertificateID = Convert.ToInt64(RadGridCertificate.MasterTableView.DataKeyValues[0]["CertificateID"]);
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                CertificateID = Convert.ToInt64(GetGridSelectedKeys(RadGridCertificate)[0]);
                if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            //是否同单位校验
            int count = CertificateDAL.SelectDistinctCountByUnitName(filterString);
            if (count > 1)
            {
                UIHelp.layerAlert(Page, "查询结果不是同一企业的证书，不能批量变更，请按“企业名称”或“组织机构代码”重新查询过滤!");
                return;
            }

            //证书在办续期申请数量校验
            DataTable dtContinueApply = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATECONTINUE", "CertificateCode", string.Format(" and CertificateID in(select CertificateID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0}) and [STATUS] <> '{1}' and CheckResult <> '{2}' and CheckResult <> '{3}'", filterString, EnumManager.CertificateContinueStatus.Decided, EnumManager.CertificateContinueCheckResult.NoGet, EnumManager.CertificateContinueCheckResult.SendBack), "CertificateID");
            if (dtContinueApply != null && dtContinueApply.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtContinueApply.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本正在办理续期的证书，未办结不能同时申请变更。证书编号“{1}”。", dtContinueApply.Rows.Count.ToString(), sb.ToString()));
                return;
            }

            //证书锁定校验
            DataTable dtLock = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", "CertificateCode", string.Format("{0} and CertificateID in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{1}')", filterString, DateTime.Now.ToString("yyyy-MM-dd")), "CertificateID");
            if (dtLock != null && dtLock.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtLock.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本被锁定的证书，证书编号“{1}”，锁定时不允许变更。解锁请联系北京市建筑业执业资格注册中心。", dtLock.Rows.Count.ToString(), sb.ToString()));
                return;
            }

            ////证书是否首次打印过校验
            //count = CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", string.Format("{0} and printdate is null and [STATUS]='首次' and  CONFERDATE >'2011-2-1'", filterString));
            //if (count > 0)
            //{
            //    UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本尚未发放的新证书，不允许批量变更!", count.ToString()));
            //    return;
            //}

            DivCanApplyList.Visible = false;
            DivEdit.Visible = true;

            BindEditDetial(CertificateID);

            BindNoPhotoMan();
        }

        /// <summary>
        /// 绑定未上传照片人员
        /// </summary>
        protected void BindNoPhotoMan()
        {
            ViewState["FacePhotoCount"] = null;
            try
            {
                string filterString = "";
                if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
                {
                    filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
                }
                else
                {
                    if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                        filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                    else//包含
                        filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                }
                DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", "CertificateCode,WorkerName,WorkerCertificateCode,FacePhoto", filterString, "CertificateID");
                string img = "";
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    img = UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(dt.Rows[i]["FacePhoto"] == DBNull.Value ? "" : dt.Rows[i]["FacePhoto"].ToString(), dt.Rows[i]["WorkerCertificateCode"].ToString());
                    if (img.IndexOf("tup.gif") == -1)
                    {
                        //dt.Rows[i].Delete();
                        dt.Rows.RemoveAt(i);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    ButtonApply.Visible = false;
                    divNoPhoto.Visible = true;
                }
                else
                {
                    ButtonApply.Visible = true;
                    divNoPhoto.Visible = false;
                }


                RadGridNoPhto.DataSource = dt;
                RadGridNoPhto.DataBind();
              
                if (dt.Rows.Count > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("尚有{0}人无照片，请首先上传照片才能申请变更！", dt.Rows.Count));                   
                }
            
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "检测未上传照片失败！", ex);
            }
          
        }

        //刷新申请表
        public void BindEditDetial(long CertificateID)
        {
            if (RadGridCertificate.MasterTableView.DataKeyValues.Count == 0) return;
            CertificateOB certificateob = CertificateDAL.GetObject(CertificateID);
            RadTextBoxUnitName.Text = certificateob.UnitName;//原工作单位
            RadTextBoxNewUnitName.Text = certificateob.UnitName;
            RadTextBoxUnitCode.Text = certificateob.UnitCode;        //原机构代码
            RadTextBoxNewUnitCode.Text = certificateob.UnitCode;
            lblApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 检查拆迁企业拆迁员最低人数限制（注销时或变更单位时检查）
        /// 规则：该企业有效拆迁员个数 - 当前申请个数 >= 最低人数限制
        /// </summary>
        /// <returns>是否通过验证</returns>
        public bool CheckChaiQianWorkersLimit()
        {
            int workersCountLimit = UIHelp.QueryCaiQianCountLimitOfUnit(Page, RadTextBoxUnitCode.Text);//最低人数要求
            if (workersCountLimit == 0) return true;
            int selectChaiQianCount = 0;//勾选拆迁员个数
            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = " and PostID=55 " + ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" and PostID=55 {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" and PostID=55 {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            selectChaiQianCount = CertificateChangeDAL.SelectCountView(filterString);

            if (selectChaiQianCount > 0)
            {
                int workersCount = CertificateDAL.SelectCount(string.Format(" and UnitCode='{1}' and PostID=55 and ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd"), RadTextBoxUnitCode.Text));
                if ((workersCount - selectChaiQianCount) < workersCountLimit)
                {
                    UIHelp.layerAlert(Page, string.Format("根据企业资质要求，您本次申请将导致企业拆迁员数量低于最低人数（{0}人）要求，无法为您提交申请！", workersCountLimit.ToString()));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查物业项目人是否被锁定：返回空字符串“”表示没有锁定，否则返回锁定说明
        /// </summary>
        /// <returns></returns>
        public string CheckCertificateLockLimit()
        {
            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = " and PostID=159 " + ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" and PostID=159 {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" and PostID=159 {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            DataTable dt = CertificateChangeDAL.GetListView(0, int.MaxValue - 1, filterString, "");
            if (dt != null || dt.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string checkResult = "";//返回结果
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //检查物业项目负责人是否被锁定（离京变更、注销或变更单位时检查）
                    if (RadTextBoxChangeType.SelectedItem.Text == "离京变更"
                        || RadTextBoxChangeType.SelectedItem.Text == "注销"
                        || (RadTextBoxChangeType.SelectedItem.Text == "京内变更" && dt.Rows[i]["UnitCode"].ToString() != RadTextBoxNewUnitCode.Text.Trim())
                        || (RadTextBoxChangeType.SelectedItem.Text == "补办" && dt.Rows[i]["UnitCode"].ToString() != RadTextBoxNewUnitCode.Text.Trim())
                        )
                    {
                        checkResult = UIHelp.QueryCertificateLockFromBaseDB("物业项目负责人", dt.Rows[i]["CertificateCode"].ToString());
                        if (checkResult != "") sb.Append("----------<br />").Append(checkResult);
                    }
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 16);
                    return sb.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 检查证书是否被锁定（内部锁）：返回空字符串“”表示没有锁定，否则返回锁定说明
        /// </summary>
        /// <returns></returns>
        public string CheckCertificateLockLimitOfMySelf()
        {
            //SELECT max(LockTime), certificatecode
            //FROM dbo.View_CertificateLock
            //where lockstatus ='加锁' and (getdate() between locktime and lockendtime) group by certificatecode
            //order by max(LockTime)

            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.View_CertificateLock", "certificatecode,max(LockTime)"
                , string.Format(" and CertificateID in (select CertificateID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0}) and lockstatus ='加锁' and (getdate() between locktime and lockendtime) group by certificatecode", filterString)
                , " certificatecode");

            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("、").Append(dr["certificatecode"].ToString());
                }
                sb.Remove(0, 1);
                sb.Insert(0, "下列证书处于锁定中，不允许变更，解锁请联系北京市建筑业执业资格注册中心。<br />");
                return sb.ToString();
            }
        }

        /// <summary>
        /// 三类人员企业资质检查：表示完全匹配，否则返回不匹配说明
        /// </summary>
        /// <returns></returns>
        public string CheckQY_BWDZZZS()
        {

            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = " and PostTypeID=1 " + ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" and PostTypeID=1 {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" and PostTypeID=1 {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            int certCount = CertificateChangeDAL.SelectCountView(filterString);//三类人证书数量
            if (certCount == 0) return "";

            string UnitName = "";//企业名称

            //组织机构代码不变，企业名称变更，检查新企业名称与组织机构代码是否与本（外）地企业资质库一致，一致允许变更；（相当于名称修正）
            if (RadTextBoxUnitCode.Text.Trim() == RadTextBoxNewUnitCode.Text.Trim()
                && RadTextBoxUnitName.Text.Trim() != RadTextBoxNewUnitName.Text.Trim())
            {
                UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxNewUnitCode.Text.Trim(), false);
                if (string.IsNullOrEmpty(UnitName))
                {
                    return string.Format("您要变更到的企业“{0}”（组织机构代码：{1}）无建筑施工企业资质证书，不允许变更。", RadTextBoxNewUnitName.Text.Trim(), RadTextBoxNewUnitCode.Text.Trim());
                }
                else if (UnitName != RadTextBoxNewUnitName.Text.Trim())
                {
                    return string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您变更的单位名称不符，不允许变更。”", RadTextBoxNewUnitCode.Text.Trim(), UnitName);
                }
            }

            //组织机构代码变更，检查新企业名称与新组织机构代码是否与本地企业资质库一致，一致允许变更；（只检查本地资质库，即不允许变更到外地企业）
            if (RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim())
            {
                UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxNewUnitCode.Text.Trim(), true);
                if (string.IsNullOrEmpty(UnitName))
                {
                    return string.Format("您要变更到的企业“{0}”（组织机构代码：{1}）无本地建筑施工企业资质证书，不允许变更。", RadTextBoxNewUnitName.Text.Trim(), RadTextBoxNewUnitCode.Text.Trim());
                }
                if (UnitName != RadTextBoxNewUnitName.Text.Trim())
                {
                    return string.Format("企业资质库中组织机构代码“{0}”对应的企业名称为“{1}”，您变更的单位名称不符，不允许变更。”", RadTextBoxNewUnitCode.Text.Trim(), UnitName);
                }

                //组织机构代码变更，证书类型为项目负责人（B本）证书，检查变更后企业及人员信息是否与本地建造师（含外地进京备案建造师）企业信息和人员信息是否一致，一致允许变更；（即有B本必须有建造师，且单位、人员信息一致）
                return Check_CompareJZS();
            }
            return "";
        }

        /// <summary>
        /// 项目负责人比对建造师：返回空字符串“”表示完全匹配，否则返回不匹配说明
        /// </summary>
        /// <returns></returns>
        public string Check_CompareJZS()
        {
            //                        //比对语句
            //            string sql = @"select c.certificatecode
            //from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE c 
            //left join dbo.RY_ZCJZS r on c.unitcode= r.zzjgdm and c.workercertificatecode = r.zjhm  
            //where c.postid={0} and c.unitcode = '{1}' and r.zzjgdm is null";

            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = string.Format(" and PostID=148 {0}", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" and PostID=148 {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" and PostID=148 {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1,"dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", "WorkerCertificatecode,UnitCode,CertificateCode", filterString, "certificatecode");
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                DataTable jzs = UIHelp.GetJZS(Page, "全部");//本地（含进京备案）建造师身份证号集合
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    if (jzs.Rows.Find(new string[] { dr["WorkerCertificatecode"].ToString().Trim(), RadTextBoxNewUnitCode.Text.Trim() }) == null
              && jzs.Rows.Find(new string[] { Utility.Check.ConvertoIDCard18To15(dr["WorkerCertificatecode"].ToString()), RadTextBoxNewUnitCode.Text.Trim() }) == null)
                    {
                        sb.Append("、").Append(dr["certificatecode"].ToString());
                    }
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    sb.Insert(0, "变更后的项目负责人证书，单位信息和人员信息必须与建造师信息一致（且建造师未过期），您提交的变更申请中信息（单位组织机构代码，人员证件号码）不匹配，不允许变更。<br />不符合证书名单如下：<br />");

                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 京内变更次数检查（一年内最多5次）：返回空字符串“”表示完全匹配，否则返回不匹配说明
        /// </summary>
        /// <returns></returns>
        public string Check_ChangeCountLimit()
        {
            //select c.certificatecode,t.changeCount from dbo.certificate c inner join 
            //(select  certificateid,count(*) as changeCount from dbo.CertificateChange
            //where  ChangeType='京内变更' and GetResult='通过' and dateadd(year,1,CheckDate) > getdate()
            //group by certificateid having count(*)>4) t on c.certificateid = t.certificateid

            //            select  CERTIFICATECODE,count(*) as changeCount from dbo.view_CertificateChange
            //where  ChangeType='京内变更' and GetResult='通过' and dateadd(year,1,CheckDate) > getdate()
            //group by CERTIFICATECODE having count(*)>4

            string filterString = "";//过滤条件

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                //不检查项目负责人，但必须首先通过建造师比对检查
                filterString = string.Format(" and certificateid in (SELECT CERTIFICATEID FROM dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE WHERE 1=1 {0}) and ChangeType='京内变更' and GetResult='通过' and dateadd(year,1,CheckDate) > getdate()  group by CERTIFICATECODE having count(*)>4", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" and certificateid in (SELECT CERTIFICATEID FROM dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE WHERE 1=1 {0} and CertificateID not in({1})) and ChangeType='京内变更' and GetResult='通过' and dateadd(year,1,CheckDate) > getdate()  group by CERTIFICATECODE having count(*)>4", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" and certificateid in (SELECT CERTIFICATEID FROM dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE WHERE 1=1 {0} and CertificateID in({1})) and ChangeType='京内变更' and GetResult='通过' and dateadd(year,1,CheckDate) > getdate() group by CERTIFICATECODE having count(*)>4", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }
            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATECHANGE", "CERTIFICATECODE,count(*) as changeCount", filterString, "certificatecode");
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("、").Append(dr["certificatecode"].ToString());
                }
                sb.Remove(0, 1);
                sb.Insert(0, "证书超过一年内允许最大变更单位次数，无法申请证书变更。<br />不符合证书名单如下：<br />");
                return sb.ToString();
            }
        }

        //批量申请（只适用于同一单位批量变更）
        //1、一年内最多4次变更单位（项目负责人变更特殊：当大于5次变更时，如果变更前不匹配建造师比对，变更后匹配建造师比对允许变更）；
        //2、已提交变更申请未审批的不能重复提交；
        //3、正在办理续期的，续期未结束不能办理变更；
        //4、证书处于锁定中，不允许变更（内部锁）；
        //5、此证书为尚未发放（打印），不允许变更电子版；
        //6、检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查），规则：该企业有效拆迁员个数 - 当前申请个数 >= 最低人数限制
        //7、检查物业项目负责人是否被锁定（离京变更、注销或变更单位时检查），外部锁；
        //8、三类人员证书变更新单位必须在企业资质库中（京内变更、补办）
        //9、项目负责人（B本）变更时比对建造师，身份证一致且新单位组织机构代码与建造师证书一致的允许变更。
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            //UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");

            string rtn = "";//返回信息
            CertificateChangeOB certhfchange = new CertificateChangeOB();
            string filterString = "";//过滤条件
            string bgsq = "";//自动生成申请编号
            DataTable dt = null;

            #region 有效检查

            if (RadTextBoxChangeType.SelectedItem.Value == "")
            {
                UIHelp.layerAlert(Page, "请选择一个变更类型！");
                return;
            }
            if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxNewUnitCode.Text) == false)
            {
                UIHelp.layerAlert(Page, "“现单位组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                return;
            }

            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更"
                || RadTextBoxChangeType.SelectedItem.Text == "注销"
                || (RadTextBoxChangeType.SelectedItem.Text == "京内变更" && RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim())
                || (RadTextBoxChangeType.SelectedItem.Text == "补办" && RadTextBoxUnitCode.Text.Trim() != RadTextBoxNewUnitCode.Text.Trim())
                )
            {
                //检查拆迁企业拆迁员最低人数限制（注销时或变更单位时检查）
                if (CheckChaiQianWorkersLimit() == false) { return; }

                //检查物业项目人是否被锁定（注销时或变更单位时检查）
                rtn = CheckCertificateLockLimit();
                if (rtn != "")
                {
                    UIHelp.layerAlert(Page, rtn);
                    return;
                }
            }

            //三类人员企业资质检查(变更单位),项目负责人比对建造师
            if (RadTextBoxChangeType.SelectedItem.Value == "京内变更" || RadTextBoxChangeType.SelectedItem.Value == "补办")
            {
                rtn = CheckQY_BWDZZZS();
                if (rtn != "")
                {
                    UIHelp.layerAlert(Page, rtn);
                    return;
                }
            }

            //检查京内变更次数限制（ 必须放在建造师比对之后，以便忽略B本变更次数限制）
            if (RadTextBoxChangeType.SelectedItem.Value == "京内变更" || RadTextBoxChangeType.SelectedItem.Value == "补办")
            {
                bool TimesLimit = false;
                TimesLimit = ValidResourceIDLimit(RoleIDs, "ManageApplyTimesLimit");//不受京内变更次数限制
                if (TimesLimit == false)
                {
                    rtn = Check_ChangeCountLimit();
                    if (rtn != "")
                    {
                        UIHelp.layerAlert(Page, rtn);
                        return;
                    }
                }
            }

            if (Convert.ToBoolean(ViewState["GetGridIfCheckAll"]) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (Convert.ToBoolean(ViewState["GetGridIfSelectedExclude"]) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            }

            //无法申请。只有“安全生产考核三类人员”和“造价员”才能申请证书离京
            if (RadTextBoxChangeType.SelectedItem.Text == "离京变更")
            {
                int Rowcount = CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", string.Format(" and (PostTypeID=2 or PostTypeID=4 or PostTypeID=5) {0}", filterString));
                if (Rowcount > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("无法申请。只有“安全生产考核三类人员”和“造价员”才能申请证书离京。申请中有{0}本证书不符合。", Rowcount.ToString()));
                    return;
                }
            }

            //证书在办续期申请数量校验
            DataTable dtContinueApply = CertificateContinueDAL.GetList(0, int.MaxValue - 1, string.Format(" and CertificateID in (select CertificateID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0}) and [STATUS] <> '{1}' and (GetResult <> '{2}' and GetResult <> '{3}' or GetResult is null)", filterString, EnumManager.CertificateContinueStatus.Decided, EnumManager.CertificateContinueCheckResult.NoGet, EnumManager.CertificateContinueCheckResult.SendBack), "CertificateID");
            if (dtContinueApply != null && dtContinueApply.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtContinueApply.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本正在办理续期的证书，未办结不能同时申请变更。证书编号“{1}”。", dtContinueApply.Rows.Count.ToString(), sb.ToString()));
                return;
            }  

            #endregion 有效检查

            certhfchange.ChangeType = RadTextBoxChangeType.SelectedItem.Text;   //变更类型 
            certhfchange.UnitName = RadTextBoxUnitName.Text.Trim();   //原单位名称
            certhfchange.NewUnitName = RadTextBoxNewUnitName.Text.Trim();//现单位名称
            certhfchange.UnitCode = RadTextBoxUnitCode.Text.Trim(); //原单位组织机构代码
            certhfchange.NewUnitCode = RadTextBoxNewUnitCode.Text.Trim();//现单位组织机构代码
            certhfchange.OldUnitAdvise = RadTextBoxOldUnitAdvise.Text.Trim();   //原单位意见
            certhfchange.NewUnitAdvise = RadTextBoxNewUnitAdvise.Text.Trim();  //现单位意见                     
            certhfchange.ApplyDate = DateTime.Now;
            certhfchange.ApplyMan = PersonName;          //变更申请人
            certhfchange.CreatePersonID = PersonID;    //创建人
            certhfchange.CreateTime = DateTime.Now;  //创建时间
            certhfchange.LinkWay = RadTextBoxLinkWay.Text.Trim();     //联系方式
            certhfchange.Status = EnumManager.CertificateChangeStatus.Applyed;  //"已申请";状态
            certhfchange.ModifyPersonID = PersonID;   //最后修改人
            certhfchange.ModifyTime = DateTime.Now;   //最后修改时间
            certhfchange.DealWay = ""; //证书处理方式

            try
            {
                dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", "CertificateID,PostTypeID", filterString, "CertificateID");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AddcertifIDToList(dt.Rows[i]["CertificateID"].ToString());
                }
                bgsq = UIHelp.GetNextBatchNumber("BGSQ");//自动生成申请编号
                certhfchange.ApplyCode = bgsq;//申请批次号
                CertificateChangeDAL.InsertBatch(certhfchange, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "证书变更申请失败！", ex);
                return;
            }
            string changeUnit = "";
            if (RadTextBoxNewUnitName.Text != RadTextBoxUnitName.Text)
            {
                changeUnit = string.Format("从单位“{0}”变更到单位“{1}”。", RadTextBoxUnitName.Text, RadTextBoxNewUnitName.Text);
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量申请证书变更", string.Format("变更类型：{0}；申请批号：{1}；证书数量：{2}本；{3}"
           , RadTextBoxChangeType.SelectedItem.Text, bgsq, dt.Rows.Count.ToString(), changeUnit));

            ClearGridSelectedKeys(RadGridCertificate);

            if (RadTextBoxChangeType.SelectedItem.Value == "京内变更")
            {
                string ts = @"重要提示：京内变更（只变更单位信息），不需要打印新证书的申请（原证书已带二维码），系统提供网上审批，无需到现场办理，规则如下：<br/>1、新单位信息与社保信息一致的，一至两天后重新登录系统查看比对结果，比对一致的自动审批，无需再到现场提交纸质材料（无二维码的旧证书需携带证书及照片到现场）。<br/>2、新单位信息与社保信息比对不一致的，需打印申请表（盖章），持相关纸质证明材料到行政服务中心办理（适用于各版本证书）。<br/>注：比对结果不一致的，可能是证书上身份证号码、组织机构代码与社保网上的信息不一致，需先变更社保网上的信息，再重新提交申请；也可能是社保网故障，需重新提交申请。<br/>3、不能保存申请表的，应为信息填写错误或不符合变更条件。";

                UIHelp.layerAlert(Page, string.Format("证书变更申请成功！申请批号：{0}<br/>请在一个月内到住建委进行审核，否则将自动取消申请！<br/>{1}<br/>", bgsq, ts));
            }
            else
            {
                UIHelp.layerAlert(Page, string.Format("证书变更申请成功！申请批号：{0}<br/>请在一个月内到住建委进行审核，否则将自动取消申请！", bgsq));
            }

            ApplyAfter();
      
        }

        //申请成功后 
        protected void ApplyAfter()
        {
            DivEdit.Visible = false;
            DivApplyedList.Visible = true;
            ObjectDataSourceApplyed.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in certifIDList)
            {
                sb.Append(",").Append(s);
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            q.Add(string.Format("CertificateID in({0})", sb.ToString()));
            q.Add(string.Format("Status='{0}'", EnumManager.CertificateChangeStatus.Applyed));
            ObjectDataSourceApplyed.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridApplyed.CurrentPageIndex = 0;
            RadGridApplyed.DataSourceID = ObjectDataSourceApplyed.ID;
        }

        ////批量打印变更申请表
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    string ChangeType = RadGridApplyed.MasterTableView.DataKeyValues[0]["ChangeType"].ToString();
        //    string template = (ChangeType == "京内变更" || ChangeType == "注销" ? "~/Template/京内变更申请表.doc" : "~/Template/证书变更申请表.doc");

        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, template
        //        , string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())
        //        , GetPrintData());
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc');", PersonID.ToString(), RootUrl), true);

        //}

        //批量导出变更申请表
        protected void ButtonOutPut_Click(object sender, EventArgs e)
        {
            string ChangeType = RadGridApplyed.MasterTableView.DataKeyValues[0]["ChangeType"].ToString();
            string template = (ChangeType == "京内变更" || ChangeType == "注销" ? "~/Template/京内变更申请表.doc" : "~/Template/证书变更申请表.doc");
        
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, template
                , string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())
                , GetPrintData());
            Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            DataTable dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, ObjectDataSourceApplyed.SelectParameters["filterWhereString"].DefaultValue, "CertificateChangeID");
            if (dt == null) return list;
            string TCodePath = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                printData.Add("Data_ApplyCode", dt.Rows[i]["ApplyCode"].ToString());//申请批次号
                printData.Add("Data_ChangeType", dt.Rows[i]["ChangeType"].ToString());//变更类型
                printData.Add("Data_ApplyDate", Convert.ToDateTime(dt.Rows[i]["ApplyDate"].ToString()).ToString("yyyy-MM-dd"));//申请日期
                printData.Add("Data_PostID", dt.Rows[i]["PostName"].ToString());  //证书类别
                printData.Add("Data_CertificateCode", dt.Rows[i]["CertificateCode"].ToString());  //证书编号
                printData.Add("Data_WorkerName", dt.Rows[i]["WorkerName"].ToString());  //原姓名
                printData.Add("Data_NewWorkerName", dt.Rows[i]["NewWorkerName"].ToString());//姓名
                printData.Add("Data_Sex", dt.Rows[i]["Sex"].ToString());//性别
                printData.Add("Data_NewSex", dt.Rows[i]["NewSex"].ToString());
                printData.Add("Data_Birthday", Convert.ToDateTime(dt.Rows[i]["Birthday"].ToString()).ToString("yyyy-MM-dd"));  //出身日期
                printData.Add("Data_NewBirthday", Convert.ToDateTime(dt.Rows[i]["NewBirthday"].ToString()).ToString("yyyy-MM-dd"));
                printData.Add("Data_WorkerCertificateCode", dt.Rows[i]["WorkerCertificateCode"].ToString());//原证件号码
                printData.Add("Data_NewWorkerCertificateCode", dt.Rows[i]["NewWorkerCertificateCode"].ToString()); //证件号码
                printData.Add("Data_UnitName", dt.Rows[i]["UnitName"].ToString());//原工作单位
                printData.Add("Data_NewUnitName", dt.Rows[i]["NewUnitName"].ToString());//现单位名称
                printData.Add("Data_UnitCode", dt.Rows[i]["UnitCode"].ToString()); //原机构代码
                printData.Add("Data_NewUnitCode", dt.Rows[i]["NewUnitCode"].ToString());//现单位机构代码
                printData.Add("Data_OldUnitAdvise", dt.Rows[i]["OldUnitAdvise"].ToString());//原单位意见
                printData.Add("Data_NewUnitAdvise", dt.Rows[i]["NewUnitAdvise"].ToString());//现单位意见
                printData.Add("Data_OldConferUnitAdvise", dt.Rows[i]["OldConferUnitAdvise"].ToString());//原发证机关意见
                printData.Add("Data_NewConferUnitAdvise", dt.Rows[i]["NewConferUnitAdvise"].ToString()); //现发证机关意见
                printData.Add("Data_LinkWay", dt.Rows[i]["LinkWay"].ToString()); //联系电话
                printData.Add("FacePhoto", dt.Rows[i]["NewWorkerCertificateCode"].ToString());//照片标签
                printData.Add("Img_FacePhoto", UIHelp.GetFaceImagePath(dt.Rows[i]["FacePhoto"].ToString(), dt.Rows[i]["NewWorkerCertificateCode"].ToString()));//绑定照片

                //更换照片
                printData.Add("FacePhotoUpdate", dt.Rows[i]["NewWorkerCertificateCode"].ToString() + "U");//照片标签
                if (dt.Rows[i]["IfUpdatePhoto"] != DBNull.Value && dt.Rows[i]["IfUpdatePhoto"].ToString() == "1")
                {
                    printData.Add("Img_FacePhotoUpdate", string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", dt.Rows[i]["CertificateCode"].ToString().Substring(dt.Rows[i]["CertificateCode"].ToString().Length - 3, 3), dt.Rows[i]["CertificateChangeID"].ToString()));//绑定照片
                }
                else
                {
                    printData.Add("Img_FacePhotoUpdate", "../Images/null.gif");//绑定照片
                }
                if (TCodePath == "")
                {
                    TCodePath = string.Format(@"../Upload/CertifChangeApply/{0}.png", dt.Rows[i]["ApplyCode"].ToString());
                    if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
                    {
                        UIHelp.CreateITFCode("~/Upload/CertifChangeApply/", dt.Rows[i]["ApplyCode"].ToString());
                    }
                }
                //条码
                printData.Add("ImageTCodeName", dt.Rows[i]["CertificateID"].ToString());
                printData.Add("Img_TCode", TCodePath);


                //xml换行
                string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

                printData.Add("Desc", string.Format("申请人：{0}{1}初审单位：{2}", dt.Rows[i]["ApplyMan"].ToString(), xmlBr, "市住建委行政服务大厅"));//备注

                list.Add(printData);

            }
            return list;
        }

        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifChangeApply/"));

        }

        protected void RadGridCertificate_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete") //从申请列表中剔除
            {
                try
                {
                    string id = (RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CertificateID"]).ToString();
                    deletecertifIDToList(id);
                    if (certifIDList.Count == 0)
                    {
                        AddcertifIDToList("0");
                    }

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (string s in certifIDList)
                    {
                        sb.Append(",").Append(s);
                    }
                    if (sb.Length > 0) sb.Remove(0, 1);

                    ObjectDataSource1.SelectParameters.Clear();
                    QueryParamOB q = new QueryParamOB();
                    //q.Add(string.Format("CertificateID not in(select CertificateID from dbo.CertificateContinue where status='{0}' or status='{1}' or status='{2}')", EnumManager.CertificateContinueStatus.Applyed, EnumManager.CertificateContinueStatus.Accepted, EnumManager.CertificateContinueStatus.Checked));
                    //q.Add("Status is not null ");
                    q.Add(string.Format("CertificateID in({0})", sb.ToString()));
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridCertificate.CurrentPageIndex = 0;
                    RadGridCertificate.DataSourceID = ObjectDataSource1.ID;
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "取消申请失败！", ex);
                    return;
                }
            }
            else if (e.CommandName == "ApplyCode")//查询批次数据
            {
                //UIHelp.layerAlert(Page, ((System.Web.UI.WebControls.LinkButton)e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("ApplyCode").OrderIndex].Controls[0]).Text);
                DivEdit.Visible = false;
                DivCanApplyList.Visible = false;
                DivApplyedList.Visible = true;
                ObjectDataSourceApplyed.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();

                q.Add(string.Format("ApplyCode ='{0}'", ((System.Web.UI.WebControls.LinkButton)e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("ApplyCode").OrderIndex].Controls[0]).Text));
                ObjectDataSourceApplyed.SelectParameters.Add("filterWhereString", q.ToWhereString());

                RadGridApplyed.CurrentPageIndex = 0;
                RadGridApplyed.DataSourceID = ObjectDataSourceApplyed.ID;

            }
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            if (RadGridApplyed.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridApplyed.PageSize = RadGridApplyed.MasterTableView.VirtualItemCount;//
            RadGridApplyed.CurrentPageIndex = 0;
            RadGridApplyed.Rebind();
            RadGridApplyed.ExportSettings.ExportOnlyData = true;
            RadGridApplyed.ExportSettings.OpenInNewWindow = true;
            RadGridApplyed.MasterTableView.ExportToExcel();
            RadGridApplyed.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }

        //格式化Excel
        protected void RadGridApplyed_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode":
                case "WorkerCertificateCode":
                    e.Cell.Style["mso-number-format"] = @"\@";
                    break;
            }

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "CertificateCode")
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

        ////批量打印变更申请表2
        //protected void ButtonPrint2_Click(object sender, EventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
        //    if (!IsGridSelected(RadGridCertificate))
        //    {
        //        UIHelp.layerAlert(Page, "你还没有选择数据！");
        //        return;
        //    }

        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/证书变更申请表.doc"
        //        , string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())
        //        , GetPrintData2());
        //    ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc');", PersonID.ToString(), RootUrl), true);

        //}

        ////批量导出变更申请表2
        //protected void ButtonOutPut2_Click(object sender, EventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
        //    if (!IsGridSelected(RadGridCertificate))
        //    {
        //        UIHelp.layerAlert(Page, "你还没有选择数据！");
        //        return;
        //    }

        //    CheckSaveDirectory();
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/证书变更申请表.doc"
        //        , string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())
        //        , GetPrintData2());
        //    Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifChangeApply/证书变更申请表_{0}.doc", PersonID.ToString())));
        //}

        ////获取打印数据2（按ListView分页导出Word）
        //protected List<Dictionary<string, string>> GetPrintData2()
        //{
        //    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        //    string filterString = "";//过滤条件

        //    if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
        //    {
        //        filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
        //    }
        //    else
        //    {
        //        if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
        //            filterString = string.Format(" and CertificateChangeID in(select CertificateChangeID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0} and CertificateID not in({1}))", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
        //        else//包含
        //            filterString = string.Format(" and CertificateChangeID in(select CertificateChangeID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0} and CertificateID in({1}))", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
        //    }

        //    DataTable dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, filterString + " and [STATUS]='已申请'", "CertificateChangeID");
        //    if (dt == null) return list;
        //    string TCodePath = "";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        Dictionary<string, string> printData = new Dictionary<string, string>();

        //        printData.Add("Data_ApplyCode", dt.Rows[i]["ApplyCode"].ToString());//申请批次号
        //        printData.Add("Data_ChangeType", dt.Rows[i]["ChangeType"].ToString());//变更类型
        //        printData.Add("Data_ApplyDate", Convert.ToDateTime(dt.Rows[i]["ApplyDate"].ToString()).ToString("yyyy-MM-dd"));//申请日期
        //        printData.Add("Data_PostID", dt.Rows[i]["PostName"].ToString());  //证书类别
        //        printData.Add("Data_CertificateCode", dt.Rows[i]["CertificateCode"].ToString());  //证书编号
        //        printData.Add("Data_WorkerName", dt.Rows[i]["WorkerName"].ToString());  //原姓名
        //        printData.Add("Data_NewWorkerName", dt.Rows[i]["NewWorkerName"].ToString());//姓名
        //        printData.Add("Data_Sex", dt.Rows[i]["Sex"].ToString());//性别
        //        printData.Add("Data_NewSex", dt.Rows[i]["NewSex"].ToString());
        //        printData.Add("Data_Birthday", Convert.ToDateTime(dt.Rows[i]["Birthday"].ToString()).ToString("yyyy-MM-dd"));  //出身日期
        //        printData.Add("Data_NewBirthday", Convert.ToDateTime(dt.Rows[i]["NewBirthday"].ToString()).ToString("yyyy-MM-dd"));
        //        printData.Add("Data_WorkerCertificateCode", dt.Rows[i]["WorkerCertificateCode"].ToString());//原证件号码
        //        printData.Add("Data_NewWorkerCertificateCode", dt.Rows[i]["NewWorkerCertificateCode"].ToString()); //证件号码
        //        printData.Add("Data_UnitName", dt.Rows[i]["UnitName"].ToString());//原工作单位
        //        printData.Add("Data_NewUnitName", dt.Rows[i]["NewUnitName"].ToString());//现单位名称
        //        printData.Add("Data_UnitCode", dt.Rows[i]["UnitCode"].ToString()); //原机构代码
        //        printData.Add("Data_NewUnitCode", dt.Rows[i]["NewUnitCode"].ToString());//现单位机构代码
        //        printData.Add("Data_OldUnitAdvise", dt.Rows[i]["OldUnitAdvise"].ToString());//原单位意见
        //        printData.Add("Data_NewUnitAdvise", dt.Rows[i]["NewUnitAdvise"].ToString());//现单位意见
        //        printData.Add("Data_OldConferUnitAdvise", dt.Rows[i]["OldConferUnitAdvise"].ToString());//原发证机关意见
        //        printData.Add("Data_NewConferUnitAdvise", dt.Rows[i]["NewConferUnitAdvise"].ToString()); //现发证机关意见
        //        printData.Add("Data_LinkWay", dt.Rows[i]["LinkWay"].ToString()); //联系电话

        //        printData.Add("FacePhoto", dt.Rows[i]["NewWorkerCertificateCode"].ToString());//照片标签
        //        printData.Add("Img_FacePhoto", UIHelp.GetFaceImagePath(dt.Rows[i]["FacePhoto"].ToString(), dt.Rows[i]["NewWorkerCertificateCode"].ToString()));//绑定照片

        //        //更换照片
        //        printData.Add("FacePhotoUpdate", dt.Rows[i]["NewWorkerCertificateCode"].ToString() + "U");//照片标签
        //        if (dt.Rows[i]["IfUpdatePhoto"] != DBNull.Value && dt.Rows[i]["IfUpdatePhoto"].ToString() == "1")
        //        {
        //            printData.Add("Img_FacePhotoUpdate", string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", dt.Rows[i]["CertificateCode"].ToString().Substring(dt.Rows[i]["CertificateCode"].ToString().Length - 3, 3), dt.Rows[i]["CertificateChangeID"].ToString()));//绑定照片
        //        }
        //        else
        //        {
        //            printData.Add("Img_FacePhotoUpdate", "../Images/null.gif");//绑定照片
        //        }
        //        if (TCodePath == "")
        //        {
        //            TCodePath = string.Format(@"../Upload/CertifChangeApply/{0}.png", dt.Rows[i]["ApplyCode"].ToString());
        //            if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
        //            {
        //                UIHelp.CreateITFCode("~/Upload/CertifChangeApply/", dt.Rows[i]["ApplyCode"].ToString());
        //            }
        //        }
        //        //条码
        //        printData.Add("ImageTCodeName", dt.Rows[i]["CertificateID"].ToString());
        //        printData.Add("Img_TCode", TCodePath);

        //        //xml换行
        //        string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

        //        printData.Add("Desc", string.Format("申请人：{0}{1}初审单位：{2}", dt.Rows[i]["ApplyMan"].ToString(), xmlBr, "市住建委行政服务大厅"));//备注


        //        list.Add(printData);

        //    }
        //    return list;
        //}

        //取消申请
        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            
            //UpdateGridSelectedKeys(RadGridCertificate, "CertificateChangeID");
            //if (IsGridSelected(RadGridCertificate) == false)
            //{
            //    UIHelp.layerAlert(Page, "至少选择一条数据！");
            //    return;
            //}

            //string filterString = "";//过滤条件
            //if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
            //{
            //    filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            //}
            //else
            //{
            //    if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
            //        filterString = string.Format(" {0} and CertificateChangeID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            //    else//包含
            //        filterString = string.Format(" {0} and CertificateChangeID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
            //}

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //System.Text.StringBuilder sbApply = new System.Text.StringBuilder();
            //DataTable dt = CertificateChangeDAL.GetListView(0, int.MaxValue - 1, filterString, "CertificateChangeID");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    sb.Append("、").Append(dt.Rows[i]["CertificateCode"].ToString());
            //    sbApply.Append(",").Append(dt.Rows[i]["CertificateID"].ToString());
            //}
            //if (sb.Length > 0) sb.Remove(0, 1);
            //if (sbApply.Length > 0) sbApply.Remove(0, 1);
            //try
            //{
            //    CertificateChangeDAL.DeleteApplying(sbApply.ToString());
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "批量取消变更申请失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(PersonName, UserID, "批量取消证书变更申请", string.Format("证书数量：{0}本；证书编号：{1}。", dt.Rows.Count.ToString(), sb.ToString()));
            //UIHelp.layerAlert(Page, string.Format("您已经成功的取消了 {0}条数据申请！", dt.Rows.Count.ToString()),6,3000);
            //ClearGridSelectedKeys(RadGridCertificate);
            //RadGridCertificate.DataBind();//刷新grid
        }

        //返回
        protected void ButtonRtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect(string.Format("CertifChange.aspx?o={0}",ViewState["PostTypeID"]==null?"":ViewState["PostTypeID"].ToString()), false);

            DivCanApplyList.Visible = true;
            DivEdit.Visible = false;
            DivApplyedList.Visible = false;
            ButtonSearch_Click(sender, e);

        }

        //批量上传照片后
        protected void RadAsyncUploadFacePhoto_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (ViewState["FacePhotoCount"] == null)
            {
                ViewState["FacePhotoCount"] = RadAsyncUploadFacePhoto.UploadedFiles.Count;
            }
            if (e.IsValid)
            {
                string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径           
                string subPath = "";//照片分类目录（证件号码后3位）
                subPath = e.File.GetNameWithoutExtension();
                
                //图片名称（证件号码）不在Grid表中的图片不允许上传
                if (RadGridNoPhto.MasterTableView.FindItemByKeyValue("WorkerCertificateCode", subPath) != null)
                {
                    subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                    workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                    e.File.SaveAs(Path.Combine(workerPhotoFolder, e.File.GetName()), true);
                }
            }
            int FacePhotoCount =  Convert.ToInt32(ViewState["FacePhotoCount"]) - 1;
            ViewState["FacePhotoCount"] = FacePhotoCount;
            if (FacePhotoCount==0)
            {                
                BindNoPhotoMan();              
            }
        }

        //更新个人照片目录照片
        protected void RadAsyncUploadFacePhoto_ValidatingFile(object sender, Telerik.Web.UI.Upload.ValidateFileEventArgs e)
        {
            
        }
        //照片上传(从临时目录拷贝照片到考试计划目录)
        protected void ButtonUploadImg_Click(object sender, EventArgs e)
        {           
            //!!!不能删除，用于提交!!!
        }
    }
}
