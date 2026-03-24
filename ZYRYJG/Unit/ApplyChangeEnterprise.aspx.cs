using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;
using System.IO;
using System.Drawing;

//*******业务规则：企业信息变更**********
//1、请先变更完一级(一级临时)证书才能变更二级证书！
//2、企业有未办结的注册事项，无法变更企业信息！
//3、有过期未注销的二级建造师不允许变更
//***************************
namespace ZYRYJG.Unit
{
    public partial class ApplyChangeEnterprise : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Unit/ApplyList.aspx|County/Agency.aspx|County/BusinessQuery.aspx";
            }
        }

        /// <summary>
        /// 申请单批次号
        /// </summary>
        protected string ApplyCode
        {
            get { return ViewState["ApplyCode"] == null ? "" : ViewState["ApplyCode"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!this.IsPostBack)
            {
                // 验证工商信息是否通过                
                if (IfExistRoleID("2") == true)//企业
                {
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1 || string.IsNullOrEmpty(o.ENT_City) == true)
                    {
                        Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");
                        return;
                    }
                }
              
                //Response.Redirect("~/ResultInfoPage.aspx?o=企业信息变更业务正在升级调整，调整期暂时无法提供服务，请耐心等待！", true);
                //return;

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                ViewState["ApplyID"] = Utility.Cryptography.Decrypt(Request["a"]);//申请ID
                ViewState["zzjgdm"] = Utility.Cryptography.Decrypt(Request["zzjgdm"]);//申请ID

                #region 根据权限显示面板

                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;
                //申请操作权限
                if (IfExistRoleID("2") == true && ViewLimit == false)
                {
                    divQY.Visible = true;
                    Td2.Visible = true;
                    Td3.Visible = true;

                    //企业看不到各级申办人列
                    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    //divCheckHistory.Visible = false;
                }
                //区县受理权限
                if (IfExistRoleID("3") == true && ViewLimit == false)
                {
                    divQX.Visible = true;
                }
                //区县审查
                if (IfExistRoleID("7") == true && ViewLimit == false)
                {
                    divQXCK.Visible = true;
                }

                #endregion 根据权限显示面板

                BindData();
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyCode);
            }

        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyCode">申请批次号</param>
        private void BindCheckHistory(string ApplyID)
        {
            DataTable dt = ApplyDAL.GetCheckHistoryList(ApplyID);
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();
        }

        ///// <summary>
        ///// 绑定附件
        ///// </summary>
        ///// <param name="ApplyID"></param>
        //private void BindFile(string ApplyCode)
        //{
        //    DataTable dt_ApplyFile = COC_TOW_Person_FileDAL.GetListByPSN_RegisterNO(ApplyCode);
        //    RadGridFile.DataSource = dt_ApplyFile;
        //    RadGridFile.DataBind();
        //}

        //绑定申请记录
        private void BindData()
        {
            ApplyChangeMDL ac = null;
            if (ViewState["ApplyID"] != null && ViewState["ApplyID"].ToString() != "")//查看
            {
                ac = ApplyChangeDAL.GetObject(ViewState["ApplyID"].ToString());              
            }
            else if (ViewState["zzjgdm"] != null && ViewState["zzjgdm"].ToString()!="")//审批
            {
                string sql = "SELECT top 1 * FROM APPLY WHERE (ENT_OrganizationsCode='{0}' OR ENT_OrganizationsCode like '________{0}_') and APPLYSTATUS='{1}' and [ApplyTypeSub]='企业信息变更'";
                DataTable dt =null;
                if (IfExistRoleID("3") == true)//区县业务员
                {
                    dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.ApplyStatus.已申报));
                }
                else if (IfExistRoleID("7") == true)//区县领导
                {
                    dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.ApplyStatus.已受理));
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ac = ApplyChangeDAL.GetObject(dt.Rows[0]["ApplyID"].ToString());                
                }
            }
            else if (IfExistRoleID("2") == true)//企业
            {
                string sql = "SELECT top 1 * FROM APPLY WHERE (ENT_OrganizationsCode='{0}'OR ENT_OrganizationsCode like '________{0}_') and [ApplyTypeSub]='企业信息变更' and APPLYSTATUS <>'{1}'";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM, EnumManager.ApplyStatus.已公告));
                if (dt != null && dt.Rows.Count>0)
                {
                    ac = ApplyChangeDAL.GetObject(dt.Rows[0]["ApplyID"].ToString());
                }
                else//新增
                {
                    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(ZZJGDM);  //企业资质          
                    GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(SHTJXYDM); //工商信息
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID); //企业信息
                    string ENT_NameTo = "";//变更后名称

                    if (_jcsjk_QY_ZHXXMDL != null)//有企业信息
                    {
                        LabelENT_NameFrom.Text = o.ENT_Name;
                        LabelFromEND_Addess.Text = o.END_Addess;
                        LabelFromENT_City.Text = o.ENT_City;

                        if (g != null && string.IsNullOrEmpty(g.ENT_NAME) == false)
                        {
                            ENT_NameTo = g.ENT_NAME;

                        }
                        else
                        {
                            ENT_NameTo = _jcsjk_QY_ZHXXMDL.QYMC;
                        }
                        LabelENT_NameTo.Text = ENT_NameTo;
                        LabelToEND_Addess.Text = _jcsjk_QY_ZHXXMDL.ZCDZ;

                        RadComboBoxENT_City.FindItemByValue(_jcsjk_QY_ZHXXMDL.XZDQBM).Selected = true;
                        UIHelp.SetReadOnly(RadComboBoxENT_City, true);

                        //按钮
                        SetButtonEnable("");

                        //绑定附件信息
                        BindFile("0");

                        //变更内容提示
                        setFontColor();

                        //人员信息
                        DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City],END_Addess FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07' ", ZZJGDM, ENT_NameTo, _jcsjk_QY_ZHXXMDL.XZDQBM, _jcsjk_QY_ZHXXMDL.ZCDZ));
                        RadGridPerson.DataSource = dtPerson;
                        RadGridPerson.DataBind();
                        LabelPersonCount.Text = string.Format("(共计{0}人)", dtPerson.Rows.Count);
                        ViewState["PersonCount"] = dtPerson.Rows.Count;

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["ENT_Name"].ToString() != LabelENT_NameTo.Text)
                            {
                                LabelENT_NameFrom.Text = r["ENT_Name"].ToString();
                                break;
                            }

                        }
                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["ENT_City"].ToString() != _jcsjk_QY_ZHXXMDL.XZDQBM)
                            {
                                LabelFromENT_City.Text = r["ENT_City"].ToString();
                                break;
                            }
                        }
                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["END_Addess"].ToString() != _jcsjk_QY_ZHXXMDL.ZCDZ)
                            {
                                LabelFromEND_Addess.Text = r["END_Addess"].ToString();
                                break;
                            }
                        }

                        return;
                    }
                    else//无企业资质
                    {
                        //企业信息
                        LabelENT_NameFrom.Text = o.ENT_Name;
                        LabelFromEND_Addess.Text = o.END_Addess;
                        LabelFromENT_City.Text = o.ENT_City;

                        LabelENT_NameTo.Text = (g != null ? g.ENT_NAME : o.ENT_Name);
                        LabelToEND_Addess.Text = (g != null ? g.DOM : o.END_Addess);
                        RadComboBoxENT_City.FindItemByValue(o.ENT_City).Selected = true;
                        UIHelp.SetReadOnly(RadComboBoxENT_City, false);

                        //人员信息
                        DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City],[END_Addess] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07'"
                            , ZZJGDM, LabelENT_NameTo.Text, RadComboBoxENT_City.SelectedValue, LabelToEND_Addess.Text));
                        RadGridPerson.DataSource = dtPerson;
                        RadGridPerson.DataBind();
                        LabelPersonCount.Text = string.Format("(共计{0}人)", dtPerson.Rows.Count);
                        ViewState["PersonCount"] = dtPerson.Rows.Count;

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["ENT_Name"].ToString() != LabelENT_NameTo.Text)
                            {
                                LabelENT_NameFrom.Text = r["ENT_Name"].ToString();
                                break;
                            }

                        }
                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["ENT_City"].ToString() != RadComboBoxENT_City.SelectedValue)
                            {
                                LabelFromENT_City.Text = r["ENT_City"].ToString();
                                break;
                            }
                        }

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["END_Addess"].ToString() != LabelToEND_Addess.Text)
                            {
                                LabelFromEND_Addess.Text = r["END_Addess"].ToString();
                                break;
                            }
                        }

                        //按钮
                        SetButtonEnable("");

                        //绑定附件信息
                        BindFile("0");

                        //变更内容提示
                        setFontColor();


                        return;
                    }
                }
            }
            else
            {
                throw new Exception("参数错误！无法为你显示");
            }

            if (ac != null)
            {
                //变更前
                LabelENT_NameFrom.Text = ac.ENT_NameFrom;
                LabelFromEND_Addess.Text = ac.FromEND_Addess;
                LabelFromENT_City.Text = ac.FromENT_City;

                //变更后名字
                LabelENT_NameTo.Text = ac.ENT_NameTo;
                LabelToEND_Addess.Text = ac.ToEND_Addess;
                 RadComboBoxENT_City.FindItemByValue(ac.ToENT_City).Selected = true;
                //只读
                UIHelp.SetReadOnly(RadComboBoxENT_City, true);

                ApplyMDL a = ApplyDAL.GetObject(ac.ApplyID);
                ViewState["ENT_OrganizationsCode"] = a.ENT_OrganizationsCode;

                ViewState["ApplyMDL"] = a;

                if (string.IsNullOrEmpty(a.LastBackResult) == false && a.ApplyStatus != EnumManager.ApplyStatus.已驳回)
                {
                    RadGridCheckHistory.MasterTableView.Caption = string.Format("<apan style='color:red'>【上次退回记录】{0}</span>", a.LastBackResult);
                }

                SetButtonEnable(a.ApplyStatus);                    
 

                //附件申请号
                ViewState["ApplyCode"] = a.ApplyCode;

                //绑定附件信息
                BindFile(a.ApplyCode);

                //审核历史
                BindCheckHistory(a.ApplyID);

                DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession FROM Apply WHERE ApplyCode= '{0}'", ViewState["ApplyCode"]));
                RadGridPerson.DataSource = dtPerson;
                RadGridPerson.DataBind();
                LabelPersonCount.Text = string.Format("（共计{0}人）", dtPerson.Rows.Count);

                if (IfExistRoleID("2") == true
                     && (a.ApplyStatus == EnumManager.ApplyStatus.未申报 || a.ApplyStatus == EnumManager.ApplyStatus.已驳回)
                     )
                {

                    trFuJanTitel.Visible = true;
                    trFuJan.Visible = true;
                }
            }

            setFontColor();
        }

        //变更颜色提示
        protected void setFontColor() {
            //变更内容变色
            if (LabelENT_NameFrom.Text.Replace("（", "(").Replace("）", ")") != LabelENT_NameTo.Text.Replace("（", "(").Replace("）", ")"))
            {
                LabelENT_NameTo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LabelENT_NameTo.ForeColor = System.Drawing.Color.Black;
            }

            if (LabelFromEND_Addess.Text != LabelToEND_Addess.Text)
            {
                LabelToEND_Addess.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LabelToEND_Addess.ForeColor = System.Drawing.Color.Black;
            }

            if (LabelFromENT_City.Text != RadComboBoxENT_City.SelectedValue)
            {
                LabelFromENT_City.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LabelFromENT_City.ForeColor = System.Drawing.Color.Black;
            }
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //***************待编写：如果没有二级证书，如何申请企业信息变更，还是作业每日根据资质自动更新企业信息。
            //?????????????????????????????
            //????????????????????????????
            if (int.Parse(ViewState["PersonCount"].ToString())== 0)
            {
                UIHelp.layerAlert(Page, "无可申请人员证书信息，请核对企业资质信息是否首先做了变更，没有资质企业直接修改办事大厅中的企业信息！");
                return;
            }
            if (RadComboBoxENT_City.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "监管区县不能为空！请到企业信息页面去完善企业隶属信息");
                return;
            }

            if (LabelENT_NameFrom.Text.Trim() == LabelENT_NameTo.Text.Trim()
                && LabelFromENT_City.Text.Trim() == RadComboBoxENT_City.SelectedValue
                && LabelFromEND_Addess.Text.Trim() == LabelToEND_Addess.Text.Trim()
             )
            {
                UIHelp.layerAlert(Page, "变更前后信息没有变化，无法申请！");
                return;
            }

            if (LabelENT_NameFrom.Text.Trim().Replace("（", "(").Replace("）", ")") != LabelENT_NameTo.Text.Trim().Replace("（", "(").Replace("）", ")"))//如果变更了企业名称
            {
                DataTable yj = CommonDAL.GetDataTable("SELECT COUNT(1) FROM [dbo].[jcsjk_jzs] WHERE (psn_level = '一级' or psn_level = '一级临时') and [ENT_Name]='" + LabelENT_NameFrom.Text.Trim() + "'");
                //DataTable yjls = CommonDAL.GetDataTable("SELECT COUNT(1) FROM [dbo].[jcsjk_jzs] WHERE [ENT_Name]='" + LabelENT_NameFrom.Text.Trim() + "'");
                if (yj.Rows[0][0].ToString() != "0")
                {

                    UIHelp.layerAlert(Page, "请先变更完一级证书的企业名称再变更二级证书！");
                    return;
                }
            }

            ////不做比对了，一建拿不到准确的区县信息2025-03-12
            //if (LabelFromENT_City.Text.Trim() != RadComboBoxENT_City.Text.Trim())//如果变更了监管区县
            //{
            //    DataTable yj = CommonDAL.GetDataTable("SELECT COUNT(1) FROM [dbo].[jcsjk_jzs] WHERE  (psn_level = '一级' or psn_level = '一级临时') and [ENT_Name]='" + LabelENT_NameFrom.Text.Trim() + "'and [ENT_City]='" + LabelFromENT_City.Text.Trim() + "'");
            //    //DataTable yjls = CommonDAL.GetDataTable("SELECT COUNT(1) FROM [dbo].[jcsjk_jzs] WHERE [ENT_Name]='" + LabelENT_NameFrom.Text.Trim() + "'and [ENT_City]='" + LabelFromENT_City.Text.Trim() + "'");
            //    if (yj.Rows[0][0].ToString() != "0")
            //    {

            //        UIHelp.layerAlert(Page, "请先变更完一级证书监管区县再变更二级证书！");
            //        return;
            //    }
            //}

            DataTable Ydt = ApplyChangeDAL.SelectView_JZS_TOW_Applying(ZZJGDM);//查询原单位是否有在办业务
            DataTable Xdt = CommonDAL.GetDataTable(string.Format("select * from apply where NoticeDate is null and (ENT_OrganizationsCode='{0}' or  ENT_OrganizationsCode like '________{0}_')", ZZJGDM));//查询申请单位是否有在办业务
            if (Ydt.Rows.Count > 0 || Xdt.Rows.Count>0)
            {

                UIHelp.layerAlert(Page, "企业有未办结的注册事项，无法变更企业信息！");
                return;
            }

            ////2022-06-14取消该规则
            //int count = CommonDAL.SelectRowCount("[COC_TOW_Person_BaseInfo]", string.Format(" and (ENT_OrganizationsCode='{0}' or  ENT_OrganizationsCode like '________{0}_') and [PSN_CertificateValidity] <='{1}' and [PSN_RegisteType] <'07'", ZZJGDM, DateTime.Now.ToString("yyyy-MM-dd")));
            //if (count > 0)
            //{
            //    UIHelp.layerAlert(Page, string.Format("企业有{0}本二级建造师注册证书已过期，但未办理注销，请首先办理注销才能变更企业信息！", count));
            //    return;
            //}



            //根据组织机构代码得到企业的所有员工
            //List<COC_TOW_Person_BaseInfoMDL> _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.ListGetObject(ZZJGDM);


            DBHelper db = new DBHelper();
            string sql = "";
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyCode"] == null)
                {
                    ViewState["ApplyCode"]=ApplyDAL.GetNextApplyCode(tran, "企业信息变更");
                }

                    //申请主表
                    sql = @"INSERT INTO [dbo].[Apply]([ApplyID],[ApplyType],[ApplyTypeSub],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[PSN_ServerID],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],[PSN_RegisterNo],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[ApplyTime],[ApplyCode],[ApplyStatus],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[PSN_Level])	
                        select newid(),'变更注册','企业信息变更',[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],'{4}',[PSN_ServerID],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],[PSN_RegisterNo],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],null,'{2}','未申报','{0}','{1}','{0}','{1}',1,[PSN_Level] 
                        FROM dbo.COC_TOW_Person_BaseInfo 
                        where (ENT_OrganizationsCode = '{3}' or ENT_OrganizationsCode like '________{3}_')  and (ENT_Name <>'{5}' or ENT_City <>'{4}' or [END_Addess]<>'{6}') and [PSN_RegisteType] < '07'";
                    
                    CommonDAL.ExecSQL(tran, string.Format(sql, UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"], ZZJGDM, RadComboBoxENT_City.SelectedValue,LabelENT_NameTo.Text.Trim(),LabelToEND_Addess.Text.Trim()));

                    //申请子表
                    sql = @"INSERT INTO [dbo].[ApplyChange]([ApplyID],[PSN_MobilePhone],[PSN_Email],[PSN_RegisterNo],[ValidDate],[ChangeReason],[ENT_NameFrom],[ENT_NameTo],[FromENT_City],[ToENT_City],[FromEND_Addess],[ToEND_Addess]) 
                        select a.[ApplyID],p.[PSN_MobilePhone],p.[PSN_Email],p.[PSN_RegisterNo],p.[PSN_CertificateValidity],'企业信息变更','{0}','{1}','{2}','{3}','{4}','{5}' 
                        from [dbo].[Apply] a inner join [dbo].[COC_TOW_Person_BaseInfo] p on a.ApplyCode='{6}' and a.[PSN_ServerID] = p.[PSN_ServerID] 
                        where a.ApplyCode='{6}' ";

                    CommonDAL.ExecSQL(tran, string.Format(sql, LabelENT_NameFrom.Text, LabelENT_NameTo.Text.Trim(), LabelFromENT_City.Text, RadComboBoxENT_City.SelectedValue, LabelFromEND_Addess.Text, LabelToEND_Addess.Text.Trim(), ViewState["ApplyCode"]));

                    tran.Commit();
                   

                SetButtonEnable("未申报");

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());

                trFuJanTitel.Visible = true;
                trFuJan.Visible = true;

                UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请保存成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
                UIHelp.layerAlert(Page, "保存成功，请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）如果有其他从业人员证书，请同时在从业人员证书管理中发起企业信息变更，以免造成后期无法申请。");

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存企业信息变更申请失败", ex);
            }
          
            BindData();
        }

        //申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            if (ButtonApply.Text != "撤销申报")
            {
                //必须上传附件集合
                System.Collections.Hashtable fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.企业信息变更证明,0},
                {EnumManager.FileDataTypeName.申请表扫描件,0}            };

                //已上传附件集合
                DataTable dt = ApplyDAL.GetApplyFile(ApplyCode);

                //计数
                foreach (DataRow r in dt.Rows)
                {
                    if (fj.ContainsKey(r["DataType"].ToString()) == true)
                    {
                        fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
                    }
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string k in fj.Keys)
                {
                    if (Convert.ToInt32(fj[k]) == 0)
                    {
                        sb.Append(string.Format("、“{0}”", k));
                    }
                }

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
                    return;
                }
            }

            string sql = " Update Apply set ApplyStatus='{0}',ApplyTime='{1}',[OldUnitCheckTime]='{1}',XGR='{2}',XGSJ='{3}' ,[GetDateTime]=null,[GetResult]=null,[GetRemark]=null,[GetMan]=null,[ExamineDatetime]=null,[ExamineResult]=null,[ExamineRemark]=null,[ExamineMan]=null ,[ReportDate] =null,[ReportMan] =null,[ReportCode] =null,[CheckDate] =null,[CheckResult] =null,[CheckRemark] =null,[CheckMan] =null,[ConfirmDate] =null,[ConfirmResult] =null,[ConfirmMan] =null where ApplyCode='{4}';";

            try
            {
               
                if (ButtonApply.Text == "撤销申报")
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.ApplyStatus.未申报, null, UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    SetButtonEnable(EnumManager.ApplyStatus.未申报);
                    UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请撤销成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
                    UIHelp.layerAlert(Page, "撤销申报成功!", "var isfresh=true;"); 
              
                }
                else
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.ApplyStatus.已申报, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    SetButtonEnable(EnumManager.ApplyStatus.已申报);
                    UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请申报成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
                    UIHelp.layerAlert(Page, "申报成功!（如果有其他从业人员证书，请同时在从业人员证书管理中发起企业信息变更，以免造成后期无法申请。）", string.Format("window.location.href='ApplyHistory.aspx?c={0}';", ApplyCode)); 
                }

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业信息变更申报失败", ex);
                return;
            }
        }

        //删除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CommonDAL.ExecSQL(string.Format("DELETE FROM dbo.Apply WHERE ApplyCode='{0}'", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete  from FileInfo where FileID in(select FileID from [ApplyFile] where applyid='{0}')", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete from ApplyFile where ApplyID='{0}' ", ViewState["ApplyCode"]));
                ViewState.Clear();
                BindData();
                UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请删除成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
                UIHelp.layerAlert(Page, "删除成功！", 6, 2000);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业信息申报删除失败", ex);
            }
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_区县审查.Attributes["class"] = step_区县审查.Attributes["class"].Replace(" green", "");
            step_已上报.Attributes["class"] = step_已上报.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {
                case "未申报":
                case "已驳回":
                    step_未申报.Attributes["class"] += " green";
                    break;
                case "已申报":
                    step_已申报.Attributes["class"] += " green";
                    break;
                case "已受理":
                    step_已受理.Attributes["class"] += " green";
                    break;
                case "区县审查":
                    step_区县审查.Attributes["class"] += " green";
                    break;
                case "已上报":
                case "已公告":
                    step_已上报.Attributes["class"] += " green";
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(string ApplyStatus)
        {
            SetStep(ApplyStatus);

            if (IfExistRoleID("2") == true)//企业
            {
                switch (ApplyStatus)
                {
                    case "":
                        ButtonSave.Enabled = true;
                        ButtonApply.Enabled = false;
                        ButtonApply.Text = "申 报";
                        ButtonDelete.Enabled = false;
                        ButtonOutput.Enabled = false;
                        break;
                    case EnumManager.ApplyStatus.未申报:
                        ButtonSave.Enabled = false;
                        ButtonApply.Enabled = true;
                        ButtonApply.Text = "申 报";
                        ButtonDelete.Enabled = true;
                        ButtonOutput.Enabled = true;
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        break;
                    case EnumManager.ApplyStatus.已申报:
                        ButtonSave.Enabled = false;
                        ButtonApply.Enabled = true;
                        ButtonApply.Text = "撤销申报";
                        ButtonDelete.Enabled = false;
                        ButtonOutput.Enabled = false;
                        break;
                    case EnumManager.ApplyStatus.已驳回:
                        ButtonSave.Enabled = false;
                        ButtonApply.Enabled = true;
                        ButtonApply.Text = "申 报";
                        ButtonDelete.Enabled = true;
                        ButtonOutput.Enabled = true;
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        break;
                    default:
                        ButtonSave.Enabled = false;
                        ButtonApply.Enabled = false;
                        ButtonApply.Text = "撤销申报";
                        ButtonDelete.Enabled = false;
                        ButtonOutput.Enabled = true;
                        break;
                }
            }
            else
            {
                ButtonSave.Enabled = false;
                ButtonApply.Enabled = false;
                ButtonApply.Text = "撤销申报";
                ButtonDelete.Enabled = false;
                ButtonOutput.Enabled = false;
            }
            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {

                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级注册建造师企业信息变更注册申请表.docx");
                string fileName = "北京市二级注册建造师企业信息变更注册申请表";
                //创建一个匿名类用来存企业变更前和变更后信息
                var ApplyChange = new
                {
                    ENT_NameFrom = LabelENT_NameFrom.Text,
                    ENT_NameTo = LabelENT_NameTo.Text.Trim(),
                    FromEND_Addess = LabelFromEND_Addess.Text,
                    ToEND_Addess = LabelToEND_Addess.Text.Trim(),
                    FromENT_City = LabelFromENT_City.Text,
                    ToENT_City = RadComboBoxENT_City.SelectedValue
                };
                ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
                UnitMDL u = UnitDAL.GetObjectByENT_OrganizationsCode(_ApplyMDL.ENT_OrganizationsCode);
                
                var o = new List<object>();
                o.Add(ApplyChange);
                var ht = PrintDocument.GetProperties(o);
                ht["ApplyCode"] = _ApplyMDL.ApplyCode;
                ht["ENT_Type"] = u.ENT_Type;
                ht["FR"] = u.ENT_Corporate;
                ht["LinkMan"] = u.ENT_Contact;
                ht["ENT_Telephone"] = u.ENT_Telephone;
                ht["ENT_Sort"] = u.ENT_Sort;
                ht["ENT_Grade"] = u.ENT_Grade;
                ht["ENT_QualificationCertificateNo"] = u.ENT_QualificationCertificateNo;
                //ht["photo"] ="";
                //拿到企业下面人员信息
                DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo FROM Apply WHERE ApplyCode= '{0}'", ViewState["ApplyCode"]));
                ht["tableList"] = new List<DataTable> { dt };
                
                //表格的索引
                ht["tableIndex"] = new List<int> { 1 };
                //行的索引
                ht["insertIndex"] = new List<int> { 1 };
                ht["ContainsHeader"] = new List<bool> { true };
                ht["isCtable"] = true;
                PrintDocument.CreateDataToWordByHashtable2(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印企业信息变更申请Word失败！", ex);
            }

        }

        //区县受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DateTime GetDateTime = DateTime.Now;
            string GetMan = UserName;
            string GetResult = RadioButtonListApplyStatus.SelectedValue;
            string GetRemark = TextBoxApplyGetResult.Text.Trim();
            string ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            try
            {
                if (RadioButtonListExamineResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
                {
                    CommonDAL.ExecSQL(string.Format(@"Update Apply SET GetDateTime='{0}',GetMan='{1}',GetResult='{2}',GetRemark='{3}',ApplyStatus='{4}',LastBackResult='{8}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_')  AND ApplyStatus='{6}' and ApplyCode='{7}'"
                        , GetDateTime, GetMan, GetResult, GetRemark, ApplyStatus, ViewState["zzjgdm"].ToString(), EnumManager.ApplyStatus.已申报, ViewState["ApplyCode"]
                        , string.Format("{0}区县驳回申请，驳回说明：{1}", GetDateTime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim())
                        ));
                }
                else
                {
                    CommonDAL.ExecSQL(string.Format(@"Update Apply SET GetDateTime='{0}',GetMan='{1}',GetResult='{2}',GetRemark='{3}',ApplyStatus='{4}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_')  AND ApplyStatus='{6}' and ApplyCode='{7}'", GetDateTime, GetMan, GetResult, GetRemark, ApplyStatus, ViewState["zzjgdm"].ToString(), EnumManager.ApplyStatus.已申报, ViewState["ApplyCode"]));
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更区县受理成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
            //UIHelp.ParentAlert(Page, "受理成功！", true);
            Response.Redirect(string.Format("~/County/BusinessQuery.aspx?id={0}&&type={1}", ViewState["ApplyCode"],"企业信息变更"), true);
        }

        //区县审查
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //移动到汇总上报时校验
            //int count = CommonDAL.GetRowCount("Apply", "*", string.Format(" and (ENT_OrganizationsCode='{0}' OR ENT_OrganizationsCode like '________{0}_') AND ApplyStatus='{1}' and ApplyCode='{2}' and [CheckXSL]=1", ViewState["zzjgdm"], EnumManager.ApplyStatus.已受理, ViewState["ApplyCode"]));
            //if (count > 0)
            //{
            //    UIHelp.layerAlert(Page, "申请业务企业为新设立企业，请在企业资质审批合格后再来审批！");
            //    return;
            //}

            DateTime ExamineDatetime = DateTime.Now;
            string ExamineMan = UserName;
            string ExamineResult = RadioButtonListExamineResult.SelectedValue;
            string ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            string ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;
            try
            {
                if (RadioButtonListExamineResult.SelectedValue == "不通过")//不同意时记录最后驳回意见
                {
                    CommonDAL.ExecSQL(string.Format(@"Update Apply SET ExamineDatetime='{0}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}',XGR='{1}',XGSJ='{0}',LastBackResult='{8}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_') AND ApplyStatus='{6}' and ApplyCode='{7}'"
                        , ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, ViewState["zzjgdm"].ToString(), EnumManager.ApplyStatus.已受理, ViewState["ApplyCode"]
                        , string.Format("{0}区县驳回申请，驳回说明：{1}", ExamineDatetime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxExamineRemark1.Text.Trim())
                        ));
                }
                else
                {
                    //更新申请表
                    CommonDAL.ExecSQL(string.Format("Update Apply SET ExamineDatetime='{0}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}',XGR='{1}',XGSJ='{0}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_') AND ApplyStatus='{6}' and ApplyCode='{7}'"
                        , ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, ViewState["zzjgdm"], EnumManager.ApplyStatus.已受理, ViewState["ApplyCode"]));
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县审查失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更区县审查成功", string.Format("变更后企业名称：{0}，变更后工商地址：{1}，变更后监管区县：{2}。", LabelENT_NameTo.Text, LabelToEND_Addess.Text, RadComboBoxENT_City.Text));
            UIHelp.ParentAlert(Page, "区县审查成功！", true);

        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyCode"></param>
        private void BindFile(string ApplyCode)
        {
            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(ApplyCode);
            DataTable HB_File = dt_ApplyFile.Clone();
            HB_File.Columns["FileUrl"].MaxLength = 8000;

            string DataType = "";
            foreach (DataRow r in dt_ApplyFile.Rows)
            {
                if (r["DataType"].ToString() != DataType)
                {

                    HB_File.ImportRow(r);
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0}|{1}", r["FileUrl"], r["FileID"]);
                    DataType = r["DataType"].ToString();
                }
                else
                {
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0},{1}|{2}", HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"], r["FileUrl"], r["FileID"]);
                }
            }

            RadGridFile.DataSource = HB_File;
            RadGridFile.DataBind();
        }

        //格式化附件
        protected void RadGridFile_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGrid rg = item.FindControl("RadGrid1") as RadGrid;

                DataTable dt_ApplyFile = (ViewState["dt_ApplyFile"] as DataTable).Clone();


                string[] imgurl = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FileUrl"].ToString().Split(',');
                string[] atrt = null;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string s in imgurl)
                {
                    DataRow dr = dt_ApplyFile.NewRow();

                    atrt = s.Split('|');
                    dr["FileUrl"] = atrt[0];
                    dr["FileID"] = atrt[1];
                    dr["ApplyID"] = ApplyCode;
                    dt_ApplyFile.Rows.Add(dr);
                }

                rg.DataSource = dt_ApplyFile;
                rg.DataBind();
            }

        }

        //删除附件
        protected void RadGridFile_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //获取类型Id

            string FileID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileID"].ToString();
            string ApplyCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();
            try
            {
                ApplyFileDAL.Delete(FileID, ApplyCode);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除企业信息变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyCode);
            UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请表附件删除成功", string.Format("企业：{0}。", LabelENT_NameFrom.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

        //变换区县
        protected void RadComboBoxENT_City_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if(RadComboBoxENT_City.SelectedValue !="")
            {
                //人员信息
                DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession,PSN_CertificateValidity,[ENT_Name],[ENT_City],[END_Addess] FROM [View_JZS_TOW_WithProfession] WHERE [ENT_OrganizationsCode]= '{0}' and (ENT_Name <>'{1}' or ENT_City <>'{2}' or [END_Addess]<>'{3}') and [PSN_RegisteType] < '07'"
                    , ZZJGDM, LabelENT_NameTo.Text, RadComboBoxENT_City.SelectedValue, LabelToEND_Addess.Text));
                RadGridPerson.DataSource = dtPerson;
                RadGridPerson.DataBind();
                LabelPersonCount.Text = string.Format("(共计{0}人)", dtPerson.Rows.Count);
                ViewState["PersonCount"] = dtPerson.Rows.Count;

                foreach (DataRow r in dtPerson.Rows)
                {
                    if (r["ENT_Name"].ToString() != LabelENT_NameTo.Text)
                    {
                        LabelENT_NameFrom.Text = r["ENT_Name"].ToString();
                        break;
                    }

                }
                foreach (DataRow r in dtPerson.Rows)
                {
                    if (r["ENT_City"].ToString() != RadComboBoxENT_City.SelectedValue)
                    {
                        LabelFromENT_City.Text = r["ENT_City"].ToString();
                        break;
                    }
                }

                foreach (DataRow r in dtPerson.Rows)
                {
                    if (r["END_Addess"].ToString() != LabelToEND_Addess.Text)
                    {
                        LabelFromEND_Addess.Text = r["END_Addess"].ToString();
                        break;
                    }
                }
            }
        }

        ////上传WORD扫描件
        //protected void FileWord(ref bool ischek)
        //{
        //    //检查是否上传文件
        //    if (FileUploadWord.HasFile == false)
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, "请将盖章签字后的申请表扫描上传！");
        //        return;
        //    }
          
        //    //检查保存文件
        //    if ((FileUploadWord.HasFile == true && Path.GetExtension(FileUploadWord.FileName).ToLower() != ".jpg"))
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, "上传扫描件只支持jpg格式图片，请检查你要上传的文件是否格式正确！");
        //        return ;
        //    }
            
        //    Int64 fileSizeLimit = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]) * 1024;
        //    if ((FileUploadWord.HasFile == true && FileUploadWord.FileContent.Length > fileSizeLimit))
        //    {
        //        ischek = false;
        //        UIHelp.layerAlert(Page, string.Format("上传扫描件大小不能超过{0}kb，请检查您要上传的文件大小！", System.Configuration.ConfigurationManager.AppSettings["FileSizeLismit"]));
        //        return ;
        //    }
        //    else
        //    {
        //        //上传附件
        //        string fileID = UIHelp.UploadFile(Page, FileUploadWord, EnumManager.FileDataType.申请表扫描件, EnumManager.GetShowName(typeof(EnumManager.FileDataType), EnumManager.FileDataType.申请表扫描件), UserName);
        //        ApplyFileMDL _ApplyFileMDL = new ApplyFileMDL();
        //        _ApplyFileMDL.ApplyID = ViewState["ApplyCode"].ToString();//特殊：企业名称变更，记录的是申请批次号
        //        _ApplyFileMDL.FileID = fileID;
        //        _ApplyFileMDL.CheckResult = 0;
        //        ApplyFileDAL.Insert(_ApplyFileMDL);
        //        ischek = true;
        //    }
        //}


       //public void GetColor()
        //{
        //    if (LabelENT_NameFrom.Text != LabelENT_NameTo.Text)
        //    {
        //        LabelENT_NameTo.ForeColor = Color.Red;
        //    }
        //    if (LabelFromEND_Addess.Text != LabelToEND_Addess.Text)
        //    {
        //        LabelToEND_Addess.ForeColor = Color.Red;
        //    }
        //    if (LabelFromENT_City.Text != RadComboBoxENT_City.Text)
        //    {
        //        RadComboBoxENT_City.ForeColor = Color.Red;
        //    }
                
        
        //}

    }
}