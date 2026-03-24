using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.IO;
using Model;
using DataAccess;

namespace ZYRYJG.EXamManage
{
    /// <summary>
    /// 企业批量考试报名（作废）
    /// </summary>
    public partial class MoreAddSign : BasePage
    {
        protected bool isExcelExport = false;
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamSignList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
                //ExamPlanOB o = ExamPlanDAL.GetObject(Convert.ToInt64(Request["o"]));
                //ViewState["ExamPlanOB"] = o;
                //Label_ExamPlanName.Text = o.ExamPlanName;
                //Label_PostTypeName.Text = o.PostTypeName;
                //Label_PostName.Text = o.PostName;
                //Label_ExamDate.Text = o.ExamStartDate.Value.ToString("yyyy年MM月dd日");

                ////报名初审点选择
                //if (o.PostTypeID.Value == 1//三类人
                //  || o.PostTypeID.Value == 5)//专业技术员
                //{
                //    divSignupPlace.Visible = true;

                //    BindRadGridSignupPlace(o.ExamPlanID.Value);
                //}
                //else
                //{
                //    divSignupPlace.Visible = false;
                //}

                //if (ValidResourceIDLimit(RoleIDs, "SignUpWithoutTimeLimit") == false
                //    && (DateTime.Compare(o.SignUpStartDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0
                //        || DateTime.Compare(o.SignUpEndDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) < 0)
                //    )
                //{
                //    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("不在报名时间段内，无法报名！"), false);
                //    return;
                //}

                //ViewState["ExamPlanID"] = Request["o"];//考试计划D

                //if (PersonType == 1 || PersonType == 4 || PersonType == 6)//培训点和管理员可以按企业名称晒选导出
                //{
                //    DivUnitName.Visible = true;

                //    QueryParamOB queryOB = new QueryParamOB();
                //    queryOB.Add(string.Format("ExamPlanID={0}", ViewState["ExamPlanID"].ToString()));
                //    queryOB.Add(string.Format("CreatePersonID={0}", PersonID.ToString()));

                //    DataTable dt = CommonDAL.GetDataTable( string.Format("select distinct UnitID,UnitName from DBO.VIEW_EXAMSIGNUP_NEW where 1=1 {0}", queryOB.ToWhereString()));
                //    RadComboBoxUnitName.DataSource = dt;
                //    RadComboBoxUnitName.DataTextField = "UnitName";
                //    RadComboBoxUnitName.DataValueField = "UnitID";
                //    RadComboBoxUnitName.DataBind();
                //    RadComboBoxUnitName.Items.Insert(0, new RadComboBoxItem("全部", ""));
                //}

                //RefreshGrid();

                ////个人照片存放路径(按证件号码后3位)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));

                ////考试报名照片存放路径(按考试计划ID分类)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"));
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString()));

                ////目标路径
                //RadAsyncUploadFacePhoto.TargetFolder = "~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString();

                //if (PersonType == 3)//企业登录
                //{
                   
                //    if (o.PostTypeID.Value == 1//三类人
                //           || o.PostTypeID.Value == 5)//专业技术员
                //    {
                //        UIHelp.layerAlert(Page, "考试报名声明<br />考务与证书管理系统中关于考试报名审核点“报名人数上限”的设定，其目的在于为了有效疏导、分流，避免在报名审核时因人数过度集中产生交通拥堵以及影响审核速度。如发生接近报名人数满额的情况，我们将通过系统对考试计划进行适时调整，不会发生考生报不上名的情况，请各位考生放心报名。<br /><br />网上报名成功后，按报名表上规定的时间持报考材料到考试资格审核点现场审核（社保信息比对一致的考生、具有建造师注册证书的考生（报考B本），系统自动审核通过无需到现场审核报名材料）。审核通过的考生，按时登录务与证书管理信息系统打印准考证、按时参加考试、网上查询成绩和证书号");
                //    }
                //    else
                //    {
                //        UIHelp.layerAlert(Page, "网上报名成功后，按报名表上规定的时间持报考材料到考试资格审核点现场审核（社保信息比对一致的考生、具有建造师注册证书的考生（报考B本），系统自动审核通过无需到现场审核报名材料）。审核通过的考生，按时登录务与证书管理信息系统打印准考证、按时参加考试、网上查询成绩和证书号");
                //    }
                //}
            }

        }

        //刷新grid
        protected void RefreshGrid()
        {
            ClearGridSelectedKeys(RadGridExamSignUp);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB queryOB = new QueryParamOB();
            queryOB.Add(string.Format("ExamPlanID={0}", ViewState["ExamPlanID"].ToString()));
            queryOB.Add(string.Format("CreatePersonID={0}", PersonID.ToString()));
            if (DivUnitName.Visible == true && RadComboBoxUnitName.SelectedValue !="")
            {
                queryOB.Add(string.Format("UnitID={0}", RadComboBoxUnitName.SelectedValue));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", queryOB.ToWhereString());
            RadGridExamSignUp.CurrentPageIndex = 0;
            RadGridExamSignUp.DataSourceID = "ObjectDataSource1";
            RadGridExamSignUp.DataBind();

            if (RadGridExamSignUp.MasterTableView.Items.Count > 0)
            {
                LabelBatCode.Text = string.Format("（报名批次号：{0}）", RadGridExamSignUp.MasterTableView.DataKeyValues[0]["SignUpCode"].ToString());


            }
            else
            {
                LabelBatCode.Text = "";
            }
        }

        //Grid换页
        protected void RadGridExamSignUp_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridExamSignUp_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridExamSignUp, "ExamSignUpID");
        }

        //照片上传(从临时目录拷贝照片到考试计划目录)
        protected void ButtonUploadImg_Click(object sender, EventArgs e)
        {
            //!!!不能删除，用于提交!!!
        }

        //报名表上传
        protected void ButtonUploadSignUpTable_Click(object sender, EventArgs e)
        {

            ExamPlanOB _ExamPlanOB = ViewState["ExamPlanOB"] as ExamPlanOB;
            //初审点信息
            if (_ExamPlanOB.PostTypeID.Value == 1//三类人
              || _ExamPlanOB.PostTypeID.Value == 5)//专业技术员
            {
                //获取选中报名点
                long? SignUpPlaceID = null;
                string PlaceName;
                for (int j = 0; j < RadGridSignupPlace.MasterTableView.Items.Count; j++)
                {

                    RadioButton CheckBox1 = RadGridSignupPlace.MasterTableView.Items[j].Cells[0].FindControl("CheckBoxSIGNUPPLACEID") as RadioButton;

                    if (CheckBox1.Checked == true)
                    {
                        SignUpPlaceID = Convert.ToInt64(RadGridSignupPlace.MasterTableView.DataKeyValues[j]["SIGNUPPLACEID"]);
                        PlaceName = RadGridSignupPlace.MasterTableView.DataKeyValues[j]["PLACENAME"].ToString();

                        ViewState["SignUpPlaceID"] = SignUpPlaceID;//选中审核点ID
                        ViewState["PlaceName"] = PlaceName;//选中审核点名称
                        ViewState["CHECKPERSONLIMIT"] = RadGridSignupPlace.MasterTableView.DataKeyValues[j]["CHECKPERSONLIMIT"];//每日限审核人数
                        ViewState["ADDRESS"] = RadGridSignupPlace.MasterTableView.DataKeyValues[j]["ADDRESS"];
                        ViewState["PHONE"] = RadGridSignupPlace.MasterTableView.DataKeyValues[j]["PHONE"];

                        //剩余可报名人数
                        ViewState["LeaveCount"] = Convert.ToInt32(RadGridSignupPlace.MasterTableView.DataKeyValues[j]["ManLimit"]) - Convert.ToInt32(RadGridSignupPlace.MasterTableView.DataKeyValues[j]["SignupManCount"]);
                        break;
                    }
                }

                if (SignUpPlaceID.HasValue == false)
                {
                    UIHelp.layerAlert(Page, "必须选择一个报名初审点！");
                    return;
                }
            }

            if (RadUploadSignUpTable.UploadedFiles.Count > 0)
            {
                //上传excel
                string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
                string filePath = Path.Combine(targetFolder, string.Format("signUpBat_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadSignUpTable.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "批量导入报名表数据失败", ex);
                    UIHelp.layerAlert(Page, "批量导入报名表数据失败!");
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    UIHelp.layerAlert(Page, "未找到任何数据!");
                    return;
                }
                if (dsImport.Tables[0].Rows.Count > 2000)
                {
                    UIHelp.layerAlert(Page, "一次最多支持导入2000条数据!");
                    return;
                }
                else
                {
                    SaveImportData(dsImport.Tables[0]);//保存
                }

            }
        }

        //验证模版版本号
        protected bool VaildImportTemplate(DataTable dt)
        {
            if (dt.Rows.Count < 1 || dt.Rows[0]["证件类别"].ToString().Trim() != "选择项.") return false;

            if (dt.Columns.Count < 17) return false;//模版列：

            string[] colNames = new string[17] { "证件类别", "证件号码", "姓名", "性别", "出生日期", "民族", "文化程度", "政治面貌", "联系电话", "技术职称(或技术等级)", "单位全称", "组织机构代码", "参加工作时间", "从事工种年限", "个人简历", "聘用单位意见", "考核发证单位意见" };
            for (int i = 0; i < 17; i++)
            {
                if (dt.Columns[i].ColumnName != colNames[i]) return false;
            }

            return true;
        }

        //企业资质
        System.Collections.Generic.Dictionary<string, string> ListQY_BWDZZZS = new Dictionary<string, string>();

        //三类人员企业资质检查
        protected string ValidUnitLimit(string _UnitCode,string _UnitName)
        {
            if (ListQY_BWDZZZS.ContainsKey(_UnitCode) == false)//查缓存
            {
                string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(_UnitCode,true);
                ListQY_BWDZZZS.Add(_UnitCode, UnitName);//写缓存
            }
            if (string.IsNullOrEmpty(ListQY_BWDZZZS[_UnitCode]))
            {
                return string.Format("<br />您所在的企业（组织机构代码：{0}）不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。", _UnitCode);
            }
            else if (_UnitName.Replace("（", "(").Replace("）", ")") != ListQY_BWDZZZS[_UnitCode].Replace("（", "(").Replace("）", ")"))
            {
                return string.Format("<br />组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", _UnitCode, ListQY_BWDZZZS[_UnitCode]);
            }
            return "";
        }

        //protected string ValidQZJQZZ(string _UnitCode)
        //{
        //    List<string> zz = UnitInfoDAL.GetUnitZZLBFromQY_BWDZZZS(_UnitCode);
        //    if (zz.Count == 1 && zz[0] == "起重机械租赁企业")
        //    {
        //        UIHelp.layerAlert(Page, "根据相关政策起重机械租赁企业只允许报考专职安全生产管理人员（C本），不能报考A本和B本，“{0}”不满住包括要求。");
        //        return string.Format("<br />组织机构代码“{0}”对应的企业名称为“{1}”，请正确填写企业名称。”", _UnitCode, ListQY_BWDZZZS[_UnitCode]);
               
        //    }
        //    return "";
        //}

        //保存报名信息
        protected void SaveImportData(DataTable dt)
        {
            //注册中心人员报名时过滤不符合条件的人员，加以标识
            string validrtn = "";
            DataColumn dc = new DataColumn();
            dc.ColumnName = "是否符合条件";
            dc.DefaultValue = "符合";
            dt.Columns.Add(dc);

            //考试报名照片存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString()))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString()));
            string workerPhotoPath = "";//图片路径
            string signUpPhotoPath = "";//图片路径
            string batCode = "";//批次号
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息
            List<string> ListCertificateCode = new List<string>();//导入证件集合

            ExamPlanOB explanOB = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"].ToString()));

            #region--------验证数据------------

            //1、报名限制：年龄未满18周岁。
            //2、报名限制检查（项目负责人、专职安全生产管理人员）：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位。
            //3、报名限制检查:不能在同一家公司取得多个企业负责人证书。
            //4、已有同类型证书，尚未过期，不能报名。（但满足第五条可重复，但必须不同等级；企业负责人可重复，但必须多加单位）。
            //5、职业技能岗位证书（除“村镇建筑工匠”和“普工”）外，已取得同类型同“技术等级”证书，尚未过期，不能报名。
            //6、报考“物业项目负责人”：
            //    6.1）比对《在岗无证物业项目负责人》库，一致允许报考。
            //    6.2）不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）。
            //7、在黑名单中人员限制一年内不得报考报名
            //8、系统中存在相同岗位工种证书，证书处于锁定中，解锁前不允许报名。
            //9、人员（身份证）处在被锁定状态中，解锁前不允许报名。
            //10、三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。
            //11、项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
            //12、报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
            //13、职业技能岗位考试计划有“技术等级”分类,报名时不允许手工填写，自动获取为与考试计划一致。（其中村镇建筑工匠or普工两个岗位无等级，系统自动填写为“无”）


            if (VaildImportTemplate(dt) == false)
            {
                UIHelp.layerAlert(Page, "您使用的报名表导入模版已经过期了，请下载最新的模版!");
                return;
            }

            //读取本岗位工种一年内考试违规人员名单--------------------
            //DataTable examAdnormal = ExamSubjectResultDAL.GetListView(0, int.MaxValue - 1,
            //    string.Format(" and m.PostID={0} and m.ExamStartDate >'{1}' and (r.ExamStatus = '替考' or r.ExamStatus = '违纪')"
            //       , explanOB.PostID.ToString(), DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd")), "ExamStartDate desc,ExamSubjectResultID");

            DataTable examAdnormal = CommonDAL.GetDataTable(string.Format(@"select * from TJ_BlackList where  ExamStartDate >'{0}'"
                   , DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd")));

            //只有起重机械租赁企业资质的企业
            DataTable dtUnit_Only_QZJXZZ = CommonDAL.GetDataTable("select distinct zzjgdm from dbo.QY_BWDZZZS group by zzjgdm having count(distinct qylb)= 1 and max(qylb)='起重机械租赁企业'");
            DataColumn[] pkey = new DataColumn[1];
            pkey[0] = dtUnit_Only_QZJXZZ.Columns[0];
            dtUnit_Only_QZJXZZ.PrimaryKey = pkey;

            ArrayList workercerficateList = new ArrayList();
            for (int i = 0; i < examAdnormal.Rows.Count; i++)
            {
                workercerficateList.Add(examAdnormal.Rows[i]["CertificateCode"]);
            }
            int findAdnormal = -1;//查找违规记录

            //读取同岗位工种所有被锁定证书记录----------------------
            ArrayList lockList = new ArrayList();
            DataTable dtLock = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATELOCK", "WorkerCertificateCode,WorkerName,CertificateCode", string.Format("and PostID={0} and CertificateID in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{1}')", explanOB.PostID.ToString(), DateTime.Now.ToString("yyyy-MM-dd")), "WorkerCertificateCode");
            for (int i = 0; i < dtLock.Rows.Count; i++)
            {
                lockList.Add(dtLock.Rows[i]["WorkerCertificateCode"]);
            }
            int findlock = -1;//查找被锁定证书的证件号码

            //读取人员被锁定证书记录----------------------
            ArrayList workerlockList = new ArrayList();
            DataTable dtworkerLock = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_WORKERLOCK", "CertificateCode", string.Format("and LockStatus='加锁' and LockEndTime >'{0}'", DateTime.Now.ToString("yyyy-MM-dd")), "CertificateCode");
            for (int i = 0; i < dtworkerLock.Rows.Count; i++)
            {
                workerlockList.Add(dtworkerLock.Rows[i]["CertificateCode"]);
            }
            int findworkerlock = -1;//查找被锁定的证件号码


            if (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(0);//删除行头,类型确定列
            }

            //删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["证件类别"].ToString().Trim() == ""
                && dt.Rows[m]["证件号码"].ToString().Trim() == ""
                && dt.Rows[m]["姓名"].ToString().Trim() == ""
                && dt.Rows[m]["性别"].ToString().Trim() == ""
                && dt.Rows[m]["出生日期"].ToString().Trim() == ""
                && dt.Rows[m]["民族"].ToString().Trim() == ""
                && dt.Rows[m]["文化程度"].ToString().Trim() == ""
                && dt.Rows[m]["政治面貌"].ToString().Trim() == ""
                && dt.Rows[m]["单位全称"].ToString().Trim() == ""
                && dt.Rows[m]["组织机构代码"].ToString().Trim() == ""
                && dt.Rows[m]["参加工作时间"].ToString().Trim() == ""
                && dt.Rows[m]["从事工种年限"].ToString().Trim() == ""
                && dt.Rows[m]["个人简历"].ToString().Trim() == ""
                && dt.Rows[m]["聘用单位意见"].ToString().Trim() == ""
                && dt.Rows[m]["考核发证单位意见"].ToString().Trim() == ""
                && dt.Rows[m]["联系电话"].ToString().Trim() == ""
                && dt.Rows[m]["技术职称(或技术等级)"].ToString().Trim() == ""
                )
                {
                    dt.Rows.RemoveAt(m);
                }
            }

            
            //string JZSWrokerCertificateCodeList = UIHelp.QueryJZSWorkerCertificateCodeFromBaseDB(Page);//本市建造师身份证号集合
            DataTable jzs = UIHelp.GetJZS(Page, "本地");//本市建造师身份证号集合

        

            //初审点信息
            if (explanOB.PostTypeID.Value == 1//三类人
              || explanOB.PostTypeID.Value == 5)//专业技术员
            {
                if (dt.Rows.Count > Convert.ToInt32(ViewState["LeaveCount"]))//没有找的选中报名点可用名额
                {
                    UIHelp.layerAlert(Page, string.Format("报名审核点“{0}”报名名额无法满足您的批量报名数量，请换一家审核点试一试！", ViewState["PlaceName"]));
                    return;
                }
            }

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空

                ValidNull(dt, m, "证件类别", rowErr);
                VailidCertificateType(dt, m, "证件类别", rowErr);
                ValidNull(dt, m, "证件号码", rowErr);
                if (dt.Rows[m]["证件类别"].ToString() == "身份证") VailidICard(dt, m, "证件号码", rowErr);
                ValidNull(dt, m, "姓名", rowErr);
                if (dt.Rows[m]["性别"].ToString() != "") VailidSex(dt, m, "性别", rowErr);
                ValidNull(dt, m, "出生日期", rowErr);
                if (dt.Rows[m]["出生日期"].ToString() != "") ValidDataTime(dt, m, "出生日期", rowErr);
                ValidNull(dt, m, "单位全称", rowErr);
                ValidNull(dt, m, "组织机构代码", rowErr);
                VailidUnitCode(dt, m, "组织机构代码", rowErr);
                if (dt.Rows[m]["参加工作时间"].ToString() != "") ValidDataTime(dt, m, "参加工作时间", rowErr);
                if (dt.Rows[m]["从事工种年限"].ToString() != "") ValidNumeric(dt, m, "从事工种年限", rowErr);
                ValidNull(dt, m, "联系电话", rowErr);
                ValidNull(dt, m, "技术职称(或技术等级)", rowErr);
                ValidNull(dt, m, "个人简历", rowErr);

                if (PersonType == 3)//企业登录
                {
                    UnitMDL unitInfo = UnitDAL.GetObject(UnitID);
                    if (dt.Rows[m]["组织机构代码"].ToString() != unitInfo.ENT_OrganizationsCode
                        || dt.Rows[m]["单位全称"].ToString() != unitInfo.ENT_Name)
                    {
                        rowErr.Append("<br />报考人所在单位名称或组织机构代码与当前登录企业信息不符！");
                    }
                }

                if (dt.Rows[m]["证件类别"].ToString() == "身份证")
                {
                    //年龄超龄检查
                    if (explanOB.PostTypeID.Value == 1)
                    {
                        if (Utility.Check.CheckBirthdayLimit(explanOB.PostID.Value, dt.Rows[m]["证件号码"].ToString(), Convert.ToDateTime(dt.Rows[m]["出生日期"].ToString().Trim()), dt.Rows[m]["性别"].ToString().Trim()) == true)
                        {
                            rowErr.Append("<br />您已超龄不予报考！");                         
                        }
                    }
                    else
                    {
                        if (Utility.Check.CheckBirthdayLimit(explanOB.PostTypeID.Value, dt.Rows[m]["证件号码"].ToString(), Convert.ToDateTime(dt.Rows[m]["出生日期"].ToString().Trim()), dt.Rows[m]["性别"].ToString().Trim()) == true)
                        {
                            rowErr.Append("<br />您已超龄不予报考！");
                        }
                    }
                }

                //检查许可证是否重复
                if (ExamSignUpDAL.SelectCount_New(string.Format("and CertificateCode='{0}' and ExamPlanID={1}", dt.Rows[m]["证件号码"].ToString().Trim(), Request["o"])) > 0)
                {
                    rowErr.Append("<br />此人已经报名过了，不能重复报名！");
                }
                bool AgeLimit = false;
                AgeLimit = ValidResourceIDLimit(RoleIDs, "SignUpWithoutAgeLimit");
                if (AgeLimit == false)
                {
                    //报名限制检查（所有类型证书）：      
                    if (Utility.Check.IfDateOrDateTimeFormat(dt.Rows[m]["出生日期"].ToString()) == true
                        && CertificateDAL.CheckRegular_SpecialOperator(Convert.ToDateTime(dt.Rows[m]["出生日期"].ToString())) == false)
                    {
                        rowErr.Append("<br />年龄未满18周岁，不符合报名要求！");
                    }
                }

                //报名限制检查:检查该人是否持有有效的（有效期未过）
                CertificateOB certificateob = CertificateDAL.GetCertificateOBObject(dt.Rows[m]["证件号码"].ToString(), explanOB.PostID.Value, DateTime.Now);
                if (certificateob != null)
                {
                    //报名限制检查:不能在同一家公司取得多个企业负责人证书
                    if (explanOB.PostID.Value == 147)
                    {
                        if (CertificateDAL.CheckRegular_UnitMaster(certificateob, dt.Rows[m]["组织机构代码"].ToString().Trim()) == false)
                        {
                            rowErr.Append("<br />不能在同一单位取得多个“企业主要负责人”证书，不能报名！");
                        }
                    }
                    else if (explanOB.PostTypeID == 4 //职业技能岗位
                             && explanOB.PostID != 158//村镇建筑工匠
                             && explanOB.PostID != 199)//普工
                    {
                        if (certificateob.SkillLevel == explanOB.PlanSkillLevel)
                        {
                            rowErr.Append("<br />已取得同类型同技术等级证书，尚未过期，不能报名！");
                        }
                    }
                    else //报名限制检查:检查该人是否持有有效的（有效期未过）
                    {
                        rowErr.Append("<br />已取得同类型证书，尚未过期，不能报名！");
                    }
                }
                ////报名限制检查：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位
                //if (CertificateDAL.CheckRegular_ItemMaster(dt.Rows[m]["证件号码"].ToString().Trim(), dt.Rows[m]["组织机构代码"].ToString().Trim(), explanOB.PostID.Value, RadComboBoxJob.SelectedItem.Text) == false)
                //{
                //    rowErr.Append("<br />只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员(C1、C2、C3)”证书，并且有C3不能再报考C1和C2，有C1或C2不能报考C3，您不符合要求，不能报名！");
                //}

                //个人、企业、培训点
                if (PersonType == 2 || PersonType == 3 || PersonType == 4)
                {
                    //报考物业项目负责人
                    if (explanOB.PostID.Value == 159)
                    {
                        if ( 
                            //比对《在岗无证物业项目负责人》库，一致允许报考（姓名、身份证号）
                            (UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(dt.Rows[m]["证件号码"].ToString().Trim()) == false)// || UIHelp.QueryWYXMFZRWorkerCertificateCodeFromBaseDB(Page).Contains(dt.Rows[m]["姓名"].ToString().Trim()) == false)
                            //不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）
                            && (UIHelp.QueryWYQYFromBaseDB(Page).Contains(dt.Rows[m]["组织机构代码"].ToString()) == false)
                            )
                        {
                            rowErr.Append("<br />报名企业不在本市管理或核发的物业企业资质库中，也未在物业管理处提供的“在岗无证人员名录”内，不许报名。<br >如有疑问，请联系59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                        }

                    }

                    //报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
                    if (explanOB.PostID.Value == 1021 || explanOB.PostID.Value == 1024)
                    {
                        if (UIHelp.QueryWYQYFromBaseDB(Page).Contains(dt.Rows[m]["组织机构代码"].ToString().Trim()) == false)
                        {
                            rowErr.Append("<br />报名企业（企业名称、组织机构代码）不在本市管理或核发的物业企业资质库中。<br >请您核对报名表中企业名称及组织机构代码是否正确，如有疑问，请联系59958811，查找物业管理处相关负责人的联系电话进行相关考试咨询。");
                        }
                    }
                }


                //在黑名单中人员限制一年内不得报考报名
                findAdnormal = workercerficateList.IndexOf(dt.Rows[m]["证件号码"].ToString().Trim());
                if (findAdnormal != -1)
                {
                    rowErr.Append(string.Format("<br />报名受限：{0}在{1}进行的{2}_{3}_{4}考试中有{5}行为，在{6}内不允许你再次报考！"
                        , examAdnormal.Rows[findAdnormal]["WorkerName"].ToString()
                        , Convert.ToDateTime(examAdnormal.Rows[findAdnormal]["ExamStartDate"]).ToString("yyyy年MM月dd日")
                        , examAdnormal.Rows[findAdnormal]["PostTypeName"].ToString()
                        , examAdnormal.Rows[findAdnormal]["PostName"].ToString()
                        , examAdnormal.Rows[findAdnormal]["KeMuName"].ToString()
                        , examAdnormal.Rows[findAdnormal]["ExamStatus"].ToString()
                        , "1年")
                        );
                }
                else
                {
                    ////缺考次数限制：报考三类人员和专业管理人员时，一年内累积三次考试缺考（0分）的（三类人员和专业管理人员都包含），锁定身份证号码一年，一年内不能报三类人员和专业管理人员考试。
                    //if (ExamResultDAL.CheckMissExamCountLimitByWorkercertificatecode(null,dt.Rows[m]["证件号码"].ToString()) == true)
                    //{
                    //    rowErr.Append("<br />由于您一年内累积三次未参加考试，不能报名参加三类人员和专业管理人员考试。");
                    //}

                    //缺考次数限制：报考三类人员和专业管理人员时，一年内累积三次考试缺考（0分）的（三类人员和专业管理人员都包含），锁定身份证号码一年，一年内不能报三类人员和专业管理人员考试。
                    TJ_MissExamLockMDL _TJ_MissExamLockMDL = TJ_MissExamLockDAL.GetObject(dt.Rows[m]["证件号码"].ToString());
                    if (_TJ_MissExamLockMDL != null)
                    {
                        rowErr.Append("<br />由于您一年内累积三次未参加考试，不能报名参加三类人员和专业管理人员考试。");
                    }
                }

                //在同岗位工种有证书被锁定的人员限制不得报考报名
                findlock = lockList.IndexOf(dt.Rows[m]["证件号码"].ToString().Trim());
                if (findlock != -1)
                {
                    rowErr.Append(string.Format("<br />报名受限：{0}存在相同岗位证书，证书编号{1}：，该证书被锁定中，不允许你报考！"
                        , dtLock.Rows[findlock]["WorkerName"].ToString()
                        , dtLock.Rows[findlock]["CertificateCode"].ToString())
                        );
                }

                //证件号码被锁定的人员限制不得报考报名
                findworkerlock = workerlockList.IndexOf(dt.Rows[m]["证件号码"].ToString().Trim());
                if (findworkerlock != -1)
                {
                    rowErr.Append(string.Format("<br />报名受限：{0}已被锁定，不允许你报名，解锁请联系北京市建筑业执业资格注册中心！", dt.Rows[m]["姓名"].ToString()));
                }

                if (ListCertificateCode.Contains(dt.Rows[m]["证件号码"].ToString().Trim()) == true)
                {
                    rowErr.Append("<br />此人证件号码已经在导入模板中出现过了，不能重复报名！");
                }
                else
                {
                    ListCertificateCode.Add(dt.Rows[m]["证件号码"].ToString().Trim());
                }

                //相同岗位类型一个月只能参加一个工种考试报名
                if (ExamSignUpDAL.CheckExamSignupCount(explanOB.ExamPlanID.Value, explanOB.PostTypeID.Value, explanOB.ExamStartDate.Value.ToString("yyyyMM"), dt.Rows[m]["证件号码"].ToString().Trim()) >0)
                {
                    rowErr.Append(string.Format("<br />报名受限：{0}存在当月相同岗位类别其他工种考试报名，同一岗位类别一次只允许报名一个工种！"
                      , dt.Rows[m]["姓名"].ToString())
                      );
                }

                if (PersonType != 6)//非行政管理人员
                {
                    //三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。
                    if (explanOB.PostTypeID.Value == 1)
                    {
                        //if (UnitCodeList.Contains(dt.Rows[m]["组织机构代码"].ToString().Trim()) == false)
                        //{
                        //    rowErr.Append("<br />您所在的企业不在本市管理的建筑企业资质库中，不允许报名，如有疑问请拨打59958811咨询北京市住房和城乡建设委员会综合服务中心28号窗口。");
                        //}
                        validrtn = ValidUnitLimit(dt.Rows[m]["组织机构代码"].ToString().Trim(), Utility.Check.removeInputErrorChares(dt.Rows[m]["单位全称"].ToString()));
                        if (validrtn != "")
                        {
                            rowErr.Append(validrtn);
                        }
                        else if (explanOB.PostID.Value == 147 || explanOB.PostID.Value == 148)//（其中起重机械租赁企业只允许报考专职安全生产管理人员（C本），不能报考A本和B本）
                        {
                            if(dtUnit_Only_QZJXZZ.Rows.Find(dt.Rows[m]["组织机构代码"].ToString().Trim())!=null)
                            {
                                rowErr.Append("<br />根据相关政策起重机械租赁企业只允许报考专职安全生产管理人员（C1、C2、C3），不能报考A本和B本，您不满足报考要求。");
                            }
                        }
                    }

                    //项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
                    if (explanOB.PostID.Value == 148)
                    {
                        //if (JZSWrokerCertificateCodeList.Contains(dt.Rows[m]["证件号码"].ToString().Trim()) == false
                        //    && JZSWrokerCertificateCodeList.Contains(Utility.Check.ConvertoIDCard18To15(dt.Rows[m]["证件号码"].ToString().Trim())) == false)
                        //{
                        //    rowErr.Append("<br />报考项目负责人要求您必须取得本市企业建造师注册证书，查不到你的建造师注册信息，不允许报名。");
                        //}
                        if (jzs.Rows.Find(new string[] { dt.Rows[m]["证件号码"].ToString().Trim(), dt.Rows[m]["组织机构代码"].ToString().Trim() }) == null
                   && jzs.Rows.Find(new string[] { Utility.Check.ConvertoIDCard18To15(dt.Rows[m]["证件号码"].ToString().Trim()), dt.Rows[m]["组织机构代码"].ToString().Trim() }) == null)
                        {
                            rowErr.Append("<br />报考项目负责人要求您必须取得本市企业建造师注册证书，查不到你的建造师注册信息，不允许报名。");
                        }
                    }
                }
                else//注册中心人员不受限制
                {
                    if (explanOB.PostTypeID.Value == 1)
                    {
                        //if (UnitCodeList.Contains(dt.Rows[m]["组织机构代码"].ToString().Trim()) == false)
                        //{
                        //    dt.Rows[m]["是否符合条件"] = "不符合";
                        //}

                        validrtn = ValidUnitLimit(dt.Rows[m]["组织机构代码"].ToString().Trim(), Utility.Check.removeInputErrorChares(dt.Rows[m]["单位全称"].ToString()));
                        if (validrtn != "")
                        {
                            dt.Rows[m]["是否符合条件"] = "不符合";
                        }
                    }
                    if (explanOB.PostID.Value == 148)
                    {
                        //if (JZSWrokerCertificateCodeList.Contains(dt.Rows[m]["证件号码"].ToString().Trim()) == false
                        //   && JZSWrokerCertificateCodeList.Contains(Utility.Check.ConvertoIDCard18To15(dt.Rows[m]["证件号码"].ToString().Trim())) == false)
                        //{
                        //    dt.Rows[m]["是否符合条件"] = "不符合";
                        //}
                        if (jzs.Rows.Find(new string[] { dt.Rows[m]["证件号码"].ToString().Trim(), dt.Rows[m]["组织机构代码"].ToString().Trim() }) == null
                      && jzs.Rows.Find(new string[] { Utility.Check.ConvertoIDCard18To15(dt.Rows[m]["证件号码"].ToString().Trim()), dt.Rows[m]["组织机构代码"].ToString().Trim() }) == null)

                        {
                            dt.Rows[m]["是否符合条件"] = "不符合";
                        }
                    }
                }

                if (GetFacePhotoPath(Request["o"], dt.Rows[m]["证件号码"].ToString()) == "~/Images/tup.gif")
                {
                    rowErr.Append(string.Format("<br >未上传以证件号“{0}”命名的照片！（或照片大小有误）", dt.Rows[m]["证件号码"].ToString()));
                }


                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 3)).Append("】行：姓名“").Append(dt.Rows[m]["姓名"].ToString()).Append("”-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            #endregion 验证数据


            #region--------保存数据------------

            //审核点每日报名数量统计
                DataTable dtTj = ExamSignUpPlaceDAL.GetSignUpPlaceTjByDate(explanOB.ExamPlanID.Value);
                DataColumn[] pk = new DataColumn[2];
                pk[0] = dtTj.Columns["SIGNUPPLACEID"];
                pk[1] = dtTj.Columns["CHECKDATEPLAN"];
                dtTj.PrimaryKey = pk;

            
                DateTime NextCheckDate = explanOB.StartCheckDate.Value;//审核日
                DataRow find = null;

            DateTime createTime = DateTime.Now;//处理时间
            bool FistCheck = false;//初审结果
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            string workercertificatecode = "";
            try
            {
                if (RadGridExamSignUp.MasterTableView.Items.Count > 0)//报过名，沿用以前的批次号(即批导入的数据使用一个批次号)
                {
                    batCode = RadGridExamSignUp.MasterTableView.DataKeyValues[0]["SignUpCode"].ToString();
                }
                else//未报过名
                {
                    batCode = UIHelp.GetNextBatchNumber(tran, "XWBM");//批次号
                }
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    workercertificatecode = dt.Rows[m]["证件号码"].ToString().Trim().Replace("x", "X"); 
                    //向从业人员表插入数据--------------------------------
                    WorkerOB _WorkerOB = WorkerDAL.GetUserObject(tran, workercertificatecode);   //根据证件号码得到用户
                    if (_WorkerOB == null) _WorkerOB = new WorkerOB();

                    _WorkerOB.CertificateType = dt.Rows[m]["证件类别"].ToString().Trim();   //证件类别
                    _WorkerOB.CertificateCode = workercertificatecode;  //证件号码
                    _WorkerOB.WorkerName = dt.Rows[m]["姓名"].ToString().Replace(" ", "").Replace("　", "");     //姓名
                    if (_WorkerOB.CertificateType == "身份证")
                    {
                        //18位身份证号码：第7、8、9、10位为出生年份(四位数)，第11、第12位为出生月份，第13、14位代表出生日期，第17位代表性别，奇数为男，偶数为女。
                        if (_WorkerOB.CertificateCode.Length == 18)
                        {
                            //出生日期
                            _WorkerOB.Birthday = Convert.ToDateTime(string.Format("{0}-{1}-{2}", _WorkerOB.CertificateCode.Substring(6, 4), _WorkerOB.CertificateCode.Substring(10, 2), _WorkerOB.CertificateCode.Substring(12, 2)));

                            //性别
                            _WorkerOB.Sex = Convert.ToInt32(_WorkerOB.CertificateCode.Substring(16, 1)) % 2 == 0 ? "女" : "男";
                        }
                        else if (_WorkerOB.CertificateCode.Length == 15)
                        {
                            //15位身份证号码：第7、8位为出生年份(两位数)，第9、10位为出生月份，第11、12位代表出生日期，第15位代表性别，奇数为男，偶数为女。 
                            _WorkerOB.Birthday = Convert.ToDateTime(string.Format("19{0}-{1}-{2}", _WorkerOB.CertificateCode.Substring(6, 2), _WorkerOB.CertificateCode.Substring(8, 2), _WorkerOB.CertificateCode.Substring(10, 2)));

                            //性别
                            _WorkerOB.Sex = Convert.ToInt32(_WorkerOB.CertificateCode.Substring(14, 1)) % 2 == 0 ? "女" : "男";
                        }
                    }
                    else//其他证件
                    {
                        _WorkerOB.Sex = dt.Rows[m]["性别"].ToString().Trim();   //性别
                        if (dt.Rows[m]["出生日期"].ToString().Trim() != "")
                            _WorkerOB.Birthday = Convert.ToDateTime(dt.Rows[m]["出生日期"].ToString().Trim());  //出身日期
                        else
                            _WorkerOB.Birthday = DateTime.MinValue;
                    }
                    _WorkerOB.Nation = dt.Rows[m]["民族"].ToString().Trim();   //民族
                    _WorkerOB.CulturalLevel = dt.Rows[m]["文化程度"].ToString().Trim();   //文化程度
                    _WorkerOB.PoliticalBackground = dt.Rows[m]["政治面貌"].ToString().Trim();  //政治面貌  
                    _WorkerOB.Phone = dt.Rows[m]["联系电话"].ToString().Trim();   //联系电话
                    if (_WorkerOB.WorkerID.HasValue == false)//new
                    {
                        WorkerDAL.Insert(tran, _WorkerOB);
                    }

                    //向机构表插入数据-----------------------------------------------
 
                    UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(tran, dt.Rows[m]["组织机构代码"].ToString().Trim());//组织机构代码    
                    if (_UnitMDL == null)
                    {
                        _UnitMDL = new UnitMDL();

                        //企业资质
                        jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(dt.Rows[m]["组织机构代码"].ToString().Trim());

                        if (_jcsjk_QY_ZHXXMDL != null)//有资质
                        {
                            _UnitMDL.UnitID = Guid.NewGuid().ToString();
                            _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                            _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                            _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                            _UnitMDL.ENT_OrganizationsCode = dt.Rows[m]["组织机构代码"].ToString().Trim();//组织机构代码
                            _UnitMDL.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                            _UnitMDL.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;//区县
                            _UnitMDL.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                            _UnitMDL.ENT_Corporate = _jcsjk_QY_ZHXXMDL.FDDBR;//法定代表人
                            _UnitMDL.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                            _UnitMDL.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                            if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                            {
                                _UnitMDL.ENT_Sort = "";
                                _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                            }
                            else
                            {
                                //企业资质等级
                                if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                                {
                                    if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                                    {
                                        _UnitMDL.ENT_Grade = "特级";
                                    }
                                    else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                                    {
                                        _UnitMDL.ENT_Grade = "一级";
                                    }
                                    else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                                    {
                                        _UnitMDL.ENT_Grade = "二级";
                                    }
                                    else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                                    {
                                        _UnitMDL.ENT_Grade = "三级";
                                    }
                                    else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                                    {
                                        _UnitMDL.ENT_Grade = "不分等级";
                                    }
                                    else
                                    {
                                        _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                    }
                                }
                                else
                                {
                                    _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                                }
                                _UnitMDL.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                                string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                                if (_UnitMDL.ENT_Grade != null)
                                {
                                    foreach (var item in ZZ)
                                    {
                                        if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(_UnitMDL.ENT_Grade) == true)
                                        {
                                            _UnitMDL.ENT_Sort = item;   //资质序列
                                            break;
                                        }

                                    }
                                }

                            }
                            _UnitMDL.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                            _UnitMDL.CreditCode = "";//社会统一信用代码
                            _UnitMDL.Valid = 1;//是否有效
                            _UnitMDL.ResultGSXX = 0;
                        }
                        else//无资质设置为新设立企业
                        {
                            _UnitMDL.UnitID = Guid.NewGuid().ToString();
                            _UnitMDL.BeginTime = DateTime.Now;//建立时间
                            _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                            _UnitMDL.ENT_Name = Utility.Check.removeInputErrorChares(dt.Rows[m]["单位全称"].ToString());//企业名称
                            _UnitMDL.ENT_OrganizationsCode = dt.Rows[m]["组织机构代码"].ToString().Trim();    //机构号码
                            _UnitMDL.ENT_Economic_Nature = "";//企业类型
                            _UnitMDL.ENT_City = "";//区县
                            _UnitMDL.END_Addess = "";//注册地址
                            _UnitMDL.ENT_Corporate = "";//法定代表人
                            //_UnitMDL.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅无企业通讯地址企业自行维护
                            _UnitMDL.ENT_Type = "";  //企业类型
                            _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                            _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                            _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号
                            _UnitMDL.CreditCode = "";//社会统一信用代码
                            _UnitMDL.Valid = 1;//是否有效
                            _UnitMDL.Memo = "新设立企业";
                            _UnitMDL.CJSJ = DateTime.Now;
                            _UnitMDL.ResultGSXX = 0;
                        }

                        UnitDAL.Insert(tran, _UnitMDL);
                    }

                    //向考试报名表插入数据--------------------------
                    ExamSignUpOB _ExamSignUpOB = new ExamSignUpOB();

                    _ExamSignUpOB.Status = EnumManager.SignUpStatus.NewSignUp;  //状态

                    _ExamSignUpOB.FirstCheckType = 0;

                     //初审点信息
                    if (explanOB.PostTypeID.Value == 1//三类人
                      || explanOB.PostTypeID.Value == 5)//专业技术员
                    {
                        FistCheck = false;//初审结果
                        if (ExamResultDAL.CheckMissExamByWorkercertificatecode(tran, workercertificatecode) == true)//上次缺考，不能免检
                        {
                            _ExamSignUpOB.FirstCheckType = -1;//标记上次缺考
                        }
                        else if (explanOB.PostID.Value == 148)//B本因为有建造师，免初审
                        {
                            FistCheck = true;
                            _ExamSignUpOB.FirstCheckType = 1;
                        }
                        else
                        {
                            //两年内参加过三类人或专业技术人员考试次数（有准考证）
                            int ExamCount = ExamResultDAL.SelectCountView(tran, string.Format(" and CertificateCode like '{0}%' and (PostTypeID =1 or PostTypeID=5) and ExamStartDate > dateadd(year,-2,getdate())", workercertificatecode));

                            if (ExamCount > 0)
                            {
                                FistCheck = true;//有准考证，免审
                                _ExamSignUpOB.FirstCheckType = 2;
                            }
                        }
                        if (FistCheck == true)
                        {
                            _ExamSignUpOB.Status = EnumManager.SignUpStatus.FirstChecked;
                            _ExamSignUpOB.FIRSTTRIALTIME = createTime;
                        }

                        _ExamSignUpOB.SignUpPlaceID = Convert.ToInt64(ViewState["SignUpPlaceID"]);//初审点ID
                        _ExamSignUpOB.PlaceName = string.Format("{0}，{1}。电话：{2}", ViewState["PlaceName"], ViewState["ADDRESS"], ViewState["PHONE"]);//初审点名称
                        _ExamSignUpOB.TrainUnitID = _ExamSignUpOB.SignUpPlaceID;
                        _ExamSignUpOB.S_TRAINUNITNAME = ViewState["PlaceName"].ToString();

                        while (NextCheckDate <= explanOB.LatestCheckDate.Value)
                        {
                            find = dtTj.Rows.Find(new object[] { _ExamSignUpOB.SignUpPlaceID.Value, NextCheckDate });
                            if (find != null)//已经有人报名
                            {
                                if (Convert.ToInt32(find["SignupManCount"]) < Convert.ToInt32(find["CHECKPERSONLIMIT"]))//未达到上限
                                {
                                    _ExamSignUpOB.CheckDatePlan = NextCheckDate;//规定初审日期
                                    find["SignupManCount"] = Convert.ToInt32(find["SignupManCount"]) + 1;
                                    break;
                                }
                            }
                            else//第一个报名
                            {
                                _ExamSignUpOB.CheckDatePlan = NextCheckDate;//规定初审日期
                                DataRow newrow = dtTj.NewRow();
                                newrow["SIGNUPPLACEID"] = ViewState["SignUpPlaceID"];
                                newrow["CHECKPERSONLIMIT"] = ViewState["CHECKPERSONLIMIT"];
                                newrow["CHECKDATEPLAN"] = NextCheckDate;
                                newrow["SignupManCount"] = 1;
                                dtTj.Rows.Add(newrow);
                                break;
                            }

                            NextCheckDate = NextCheckDate.AddDays(1);
                        }

                        if (_ExamSignUpOB.CheckDatePlan.HasValue == false)//没有找的选中报名点可用名额
                        {
                            tran.Rollback();
                            UIHelp.layerAlert(Page, string.Format("报名审核点“{0}”报名名额无法满足您的批量报名数量，请换一家审核点试一试！", ViewState["PlaceName"]));
                            return;
                        }
                    }
                    _ExamSignUpOB.WorkerID = _WorkerOB.WorkerID;
                    //_ExamSignUpOB.UnitID = _UnitInfoOB.UnitID;
                    if (PersonType == 4)
                    {
                        //_ExamSignUpOB.TrainUnitID = UnitID;//培训点id
                        _ExamSignUpOB.S_TRAINUNITNAME = PersonName;//培训点名称
                    }

                    _ExamSignUpOB.SignUpCode = batCode;  //报名编号
                    _ExamSignUpOB.SignUpDate = createTime;   //报名日期

                    _ExamSignUpOB.ExamPlanID = Convert.ToInt64(Request["o"]);   //考试计划id
                    if (dt.Rows[m]["参加工作时间"].ToString().Trim() != "") _ExamSignUpOB.WorkStartDate = Convert.ToDateTime(dt.Rows[m]["参加工作时间"].ToString().Trim());
                    if (dt.Rows[m]["从事工种年限"].ToString().Trim() != "") _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(dt.Rows[m]["从事工种年限"].ToString().Trim());
                    _ExamSignUpOB.PersonDetail = dt.Rows[m]["个人简历"].ToString().Trim();
                    _ExamSignUpOB.HireUnitAdvise = dt.Rows[m]["聘用单位意见"].ToString().Trim();
                    _ExamSignUpOB.AdminUnitAdvise = dt.Rows[m]["考核发证单位意见"].ToString().Trim();

                  
                    _ExamSignUpOB.WorkerName = Utility.Check.removeInputErrorChares(dt.Rows[m]["姓名"].ToString()); //姓名
                    _ExamSignUpOB.CertificateType = dt.Rows[m]["证件类别"].ToString().Trim();  //证件类别
                    _ExamSignUpOB.CertificateCode = workercertificatecode;  //证件号码
                    _ExamSignUpOB.UnitName = Utility.Check.removeInputErrorChares(dt.Rows[m]["单位全称"].ToString());    //单位全称
                    _ExamSignUpOB.UnitCode = dt.Rows[m]["组织机构代码"].ToString().Trim();    //机构号码
                    _ExamSignUpOB.Promise = 1;

                    if (dt.Rows[m]["证件类别"].ToString().Trim() == "身份证")
                    {
                        //18位身份证号码：第7、8、9、10位为出生年份(四位数)，第11、第12位为出生月份，第13、14位代表出生日期，第17位代表性别，奇数为男，偶数为女。
                        if (workercertificatecode.Length == 18)
                        {
                            //出生日期
                            _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(string.Format("{0}-{1}-{2}", workercertificatecode.Substring(6, 4), workercertificatecode.Substring(10, 2), workercertificatecode.Substring(12, 2)));

                            //性别
                            _ExamSignUpOB.S_SEX = Convert.ToInt32(workercertificatecode.Substring(16, 1)) % 2 == 0 ? "女" : "男";
                        }
                        else if (_WorkerOB.CertificateCode.Length == 15)
                        {
                            //15位身份证号码：第7、8位为出生年份(两位数)，第9、10位为出生月份，第11、12位代表出生日期，第15位代表性别，奇数为男，偶数为女。 
                            _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(string.Format("19{0}-{1}-{2}", workercertificatecode.Substring(6, 2), workercertificatecode.Substring(8, 2), workercertificatecode.Substring(10, 2)));

                            //性别
                            _ExamSignUpOB.S_SEX = Convert.ToInt32(workercertificatecode.Substring(14, 1)) % 2 == 0 ? "女" : "男";
                        }
                    }
                    else//其他证件
                    {
                        _ExamSignUpOB.S_SEX = dt.Rows[m]["性别"].ToString().Trim();   //性别
                        if (dt.Rows[m]["出生日期"].ToString().Trim() != "")
                            _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(dt.Rows[m]["出生日期"].ToString().Trim());  //出身日期
                        else
                            _ExamSignUpOB.S_BIRTHDAY = DateTime.MinValue;
                    }                  
                    _ExamSignUpOB.S_CULTURALLEVEL = dt.Rows[m]["文化程度"].ToString().Trim();   //文化程度                    
                    _ExamSignUpOB.S_PHONE = dt.Rows[m]["联系电话"].ToString().Trim();   //联系电话

                    _ExamSignUpOB.CreatePersonID = PersonID;
                    _ExamSignUpOB.SignUpMan = PersonName; //报名操作人
                    _ExamSignUpOB.CreateTime = createTime;
                    if (explanOB.PostTypeID.Value == 4) //职业技能岗位
                    {
                        if (explanOB.PostID.Value == 158 || explanOB.PostID.Value == 199)//村镇建筑工匠or普工
                        {
                            _ExamSignUpOB.SKILLLEVEL = "无";
                        }
                        else
                        {
                            _ExamSignUpOB.SKILLLEVEL = explanOB.PlanSkillLevel;//必须等于考试计划技术等级
                        }
                    }
                    else
                    {
                        _ExamSignUpOB.SKILLLEVEL = dt.Rows[m]["技术职称(或技术等级)"].ToString().Trim();
                    }
                    _ExamSignUpOB.IsConditions = dt.Rows[m]["是否符合条件"].ToString().Trim();




                    //如果个人照片目录下“~/UpLoad/SignUpPhoto/后3为证件号码/”有该人照片，拷贝到报名照片目录中去“~/UpLoad/SignUpPhoto/考试计划ID/”
                    workerPhotoPath = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", _ExamSignUpOB.CertificateCode.Substring(_ExamSignUpOB.CertificateCode.Length - 3, 3), _ExamSignUpOB.CertificateCode);
                    if (File.Exists(Page.Server.MapPath(workerPhotoPath)))
                    {
                        signUpPhotoPath = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ViewState["ExamPlanID"].ToString(), _ExamSignUpOB.CertificateCode);
                        File.Copy(Server.MapPath(workerPhotoPath), Server.MapPath(signUpPhotoPath), true);
                    }

                    if (ExamSignUpDAL.SelectCount_New(tran, string.Format("and CertificateCode='{0}' and ExamPlanID={1}", dt.Rows[m]["证件号码"].ToString().Trim(), Request["o"])) == 0)
                    {
                        ExamSignUpDAL.Insert(tran, _ExamSignUpOB);
                    }
                }

                tran.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "批量报名", string.Format("报名批次号：{0}。考试计划：{1}。岗位工种：{2}。报名人数：{3}人。",
                batCode,
                explanOB.ExamPlanName,
                explanOB.PostName,
                dt.Rows.Count.ToString()));   

                UIHelp.layerAlert(Page, string.Format("报名成功！本次完成{0}人报名,报名批次号：{1}。", dt.Rows.Count.ToString(), batCode));
                RefreshGrid();
                //报名初审点选择
                if (explanOB.PostTypeID.Value == 1//三类人
                  || explanOB.PostTypeID.Value == 5)//专业技术员
                {
                    BindRadGridSignupPlace(explanOB.ExamPlanID.Value);
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批量报名失败！", ex);
                return;
            }

            #endregion 保存数据
        }

        #region 数据验证

        /// <summary>
        /// 验证数据是否为空值
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowIndex">行</param>
        /// <param name="colName">列名</param>
        /// <param name="rowErr">行校验结果</param>
        private void ValidNull(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") rowErr.Append("<br />“").Append(colName).Append("”不能为空值！");
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowIndex">行</param>
        /// <param name="colName">列名</param>
        /// <param name="rowErr">行校验结果</param>
        private void ValidDataTime(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (Utility.Check.IfDateOrDateTimeFormat(dt.Rows[rowIndex][colName].ToString()) == false) rowErr.Append("<br />“").Append(colName).Append("”必须是有效的日期格式（如2010-1-1）");
        }

        //验证数字
        private void ValidNumeric(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (Utility.Check.IsNumber(dt.Rows[rowIndex][colName].ToString()) == false) rowErr.Append("<br />“").Append(colName).Append("”必须位数字");
        }

        //验证身份证
        private void VailidICard(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (dt.Rows[rowIndex][colName].ToString().Length != 18)
            {
                rowErr.Append("<br />“").Append(colName).Append("”只能为18位（请使用最新证件）");//不能用15为证件
            }
            else if (Utility.Check.isChinaIDCard(dt.Rows[rowIndex][colName].ToString()) == false)
            {
                rowErr.Append("<br />“").Append(colName).Append("”格式不正确");
            }
        }

        //验证机构组织代码
        private void VailidUnitCode(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (UIHelp.UnitCodeCheck(this.Page, dt.Rows[rowIndex][colName].ToString()) == false)
                rowErr.Append("<br />“").Append(colName).Append("”格式不正确");
        }

        //验证性别
        private void VailidSex(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (dt.Rows[rowIndex][colName].ToString() != "男" && dt.Rows[rowIndex][colName].ToString() != "女") rowErr.Append("<br />“").Append(colName).Append("”只能填写“男”或“女”");
        }

        //验证证件类别
        private void VailidCertificateType(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString() == "") return;
            if (dt.Rows[rowIndex][colName].ToString() == "身份证"
                || dt.Rows[rowIndex][colName].ToString() == "军官证"
                 || dt.Rows[rowIndex][colName].ToString() == "护照"
                 || dt.Rows[rowIndex][colName].ToString() == "其它证件") return;
            rowErr.Append("<br />“").Append(colName).Append("”只能填写“身份证、军官证、护照、其它证件”中的一种");
        }

        #endregion

        //单条删除
        protected void RadGridExamSignUp_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //获取类型Id
            Int64 _ExamSignUpID = Convert.ToInt64(RadGridExamSignUp.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamSignUpID"]);             //Convert.ToInt32(e.CommandArgument);

            UIHelp.WriteOperateLog(PersonName, UserID, "取消报名", string.Format("报名批次号：{0}，报名人：{1}，详情见“查看报名情况/查看删除历史”。",
             RadGridExamSignUp.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SignUpCode"].ToString(),
              e.Item.Cells[RadGridExamSignUp.MasterTableView.Columns.FindByUniqueName("WorkerName").OrderIndex].Text));  

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                ExamSignUp_DelDAL.Insert(tran, _ExamSignUpID, PersonName, DateTime.Now);
                ExamSignUpDAL.Delete(tran, _ExamSignUpID);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Commit();
                UIHelp.WriteErrorLog(Page, "取消报名信息失败！", ex);
                return;
            }
            RefreshGrid();

            ExamPlanOB o = ViewState["ExamPlanOB"] as ExamPlanOB;
           
            //报名初审点选择
            if (o.PostTypeID.Value == 1//三类人
              || o.PostTypeID.Value == 5)//专业技术员
            {
                BindRadGridSignupPlace(o.ExamPlanID.Value);
            }
            
            UIHelp.layerAlert(Page, "取消报名成功！",6,3000);
        }

        //批量删除报名表
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = ExamSignUpDAL.GetObjectByExamPlanID(Convert.ToInt64(ViewState["ExamPlanID"].ToString()), PersonID, EnumManager.SignUpStatus.NewSignUp);
            if (dt.Rows.Count <= 0)
            {
                UIHelp.layerAlert(Page, "没有可以取消的报名数据！");
                return;
            }
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                ExamSignUp_DelDAL.Insert(tran, Convert.ToInt64(ViewState["ExamPlanID"].ToString()), PersonID, EnumManager.SignUpStatus.NewSignUp, PersonName, DateTime.Now);
                ExamSignUpDAL.DeleteByECS(tran, Convert.ToInt64(ViewState["ExamPlanID"].ToString()), PersonID, EnumManager.SignUpStatus.NewSignUp);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批量取消报名失败！", ex);
                return;
            }
            RefreshGrid();

            ExamPlanOB o = ViewState["ExamPlanOB"] as ExamPlanOB;

            //报名初审点选择
            if (o.PostTypeID.Value == 1//三类人
              || o.PostTypeID.Value == 5)//专业技术员
            {
                BindRadGridSignupPlace(o.ExamPlanID.Value);
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量取消报名", string.Format("报名批次号：{0}，取消报名人数{1}人，详情见“查看报名情况/查看删除历史”。",
                dt.Rows[0]["SignUpCode"].ToString(),
                dt.Rows.Count.ToString()));  
            UIHelp.layerAlert(Page, "批量取消报名成功！",6,3000);
        }

        //批量上传照片后
        protected void RadAsyncUploadFacePhoto_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (e.IsValid)
            {
                string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径           
                string subPath = "";//照片分类目录（证件号码后3位）
                subPath = e.File.GetNameWithoutExtension();

                ////图片名称（证件号码）不在Grid报名表中的图片不允许上传
                //if (RadGridExamSignUp.MasterTableView.FindItemByKeyValue("CertificateCode", subPath) != null)
                //{
                subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                e.File.SaveAs(Path.Combine(workerPhotoFolder, e.File.GetName()), true);
                File.Copy(Path.Combine(workerPhotoFolder, e.File.GetName()), Path.Combine(Page.Server.MapPath("~/UpLoad/SignUpPhoto/" + ViewState["ExamPlanID"].ToString() + "/"), e.File.GetName()), true);

                //}
            }

            if (RadAsyncUploadFacePhoto.UploadedFiles.Count > 0)
            {
                RefreshGrid();
                UIHelp.layerAlert(Page, string.Format("成功上传{0}张照片！", RadAsyncUploadFacePhoto.UploadedFiles.Count),6,3000);
            }
        }

        //更新个人照片目录照片
        protected void RadAsyncUploadFacePhoto_ValidatingFile(object sender, Telerik.Web.UI.Upload.ValidateFileEventArgs e)
        {

        }

        //根据证件号码显示照片
        protected string ShowFaceimage(string CertificateCode)
        {
            //Image img=RadGridExamSignUp.FindControl("Image1") as Image ;
            System.Random rm = new Random();
            string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), CertificateCode))); //绑定照片;
            return img;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/tup.gif";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
            {
                FileInfo fi = new FileInfo(Server.MapPath(path));
                if (fi.Length <= 51200 & fi.Length >= 200)
                {
                    return path;
                }
            }

            path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
            {

                FileInfo fi = new FileInfo(Server.MapPath(path));
                if (fi.Length <= 51200 & fi.Length >= 200)
                {
                    return path;
                }
            }

            return "~/Images/tup.gif";
        }

        //导出报名列表Excel
        protected void ButtonOutputExcel_Click(object sender, EventArgs e)
        {
            if (RadGridExamSignUp.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB queryOB = new QueryParamOB();
            queryOB.Add(string.Format("ExamPlanID={0}", ViewState["ExamPlanID"].ToString()));
            queryOB.Add(string.Format("CreatePersonID={0}", PersonID.ToString()));
            if (DivUnitName.Visible == true && RadComboBoxUnitName.SelectedValue != "")
            {
                queryOB.Add(string.Format("UnitID={0}", RadComboBoxUnitName.SelectedValue));
            }

            string sortBy = "ExamSignUpID";
            string saveFile = string.Format("~/UpLoad/SignUpTable/考试报名名单{0}_{1}.xls", DateTime.Now.ToString("yyyyMMddHHmm"), PersonID.ToString());//保存文件名

            ExamPlanOB explan = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"].ToString()));
            PostInfoOB postInfo = PostInfoDAL.GetObject(Convert.ToInt32(explan.PostID));

            string colHead = @"岗位类别\岗位工种\姓名\证件类型\证件号码\单位全称\组织机构代码";
            string colName = string.Format(@"PostTypeName\'{0}'\WorkerName\CertificateType\CertificateCode\UnitName\UnitCode", UIHelp.FormatPostNameByExamplanName(postInfo.PostID.Value, postInfo.PostName, explan.ExamPlanName));

            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));

                //导出数据到数据库服务器
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMSIGNUP_NEW", queryOB.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出报名名单失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("报名名单下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }     

        //批量导出报名表
        protected void ButtonOutputWord_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
            if (!IsGridSelected(RadGridExamSignUp))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            CheckSaveDirectory();
            string fileID = string.Format("{0}_{1}", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc"
            , string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", fileID)
            , GetPrintData());
            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("报名表", string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", fileID)));


            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", PersonID.ToString())));

        }

        //批量打印报名表
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamSignUp, "ExamSignUpID");
            if (!IsGridSelected(RadGridExamSignUp))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/报名表.doc"
                , string.Format("~/UpLoad/SignUpTable/报名表_{0}.doc", PersonID.ToString())
                , GetPrintData());

            ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/SignUpTable/报名表_{0}.doc');", PersonID.ToString(), RootUrl), true);
        }

        //检查临时目录
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/SignUpTable/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/SignUpTable/"));
        }

        //准备打印、导出数据
        protected List<Dictionary<string, string>> GetPrintData()
        {
            //xml换行
            string xmlBr = @"</w:t></w:r></w:p><w:p wsp:rsidR=""00872D3C"" wsp:rsidRPr=""00D14530"" wsp:rsidRDefault=""00474EF2"" wsp:rsidP=""00290734""><w:pPr><w:spacing w:line=""240"" w:line-rule=""auto""/><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr></w:pPr><w:r wsp:rsidRPr=""00474EF2""><w:rPr><w:rFonts w:ascii=""宋体"" w:h-ansi=""宋体""/><wx:font wx:val=""宋体""/><w:sz-cs w:val=""21""/></w:rPr><w:t>";

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamSignUp) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamSignUp) == true)//排除
                    filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamSignUp));
                else//包含
                    filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamSignUp));
            }


            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            ExamPlanOB explan = ExamPlanDAL.GetObject(Convert.ToInt64(ViewState["ExamPlanID"].ToString()));
            PostInfoOB postInfo = PostInfoDAL.GetObject(Convert.ToInt32(explan.PostID));
            string TCodePath = "";//条形码
            DataTable dt = ExamSignUpDAL.GetList_New(0, int.MaxValue - 1, filterString, "ExamSignUpID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();
                list.Add(printData);

                printData.Add("SignUpCode", dt.Rows[i]["SignUpCode"].ToString());//报名批号
                printData.Add("SignUpDate", Convert.ToDateTime(dt.Rows[i]["SignUpDate"]).ToString("yyyy-MM-dd"));//报名时间
                printData.Add("CertificateCode", dt.Rows[i]["CertificateCode"].ToString());//证件编号
                printData.Add("WorkerName", dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("Sex", dt.Rows[i]["Sex"].ToString());//性别
                printData.Add("Age", dt.Rows[i]["Birthday"] == DBNull.Value ? "" : Convert.ToString(DateTime.Now.Year - Convert.ToDateTime(dt.Rows[i]["Birthday"]).Year));//年龄
                printData.Add("CulturalLevel", dt.Rows[i]["CulturalLevel"].ToString());//文化程度
                printData.Add("UnitCode", dt.Rows[i]["UnitCode"].ToString());//组织代码
                printData.Add("UnitName", dt.Rows[i]["UnitName"].ToString());//企业名称
                
                printData.Add("PostID", UIHelp.FormatPostNameByExamplanName(postInfo.PostID.Value, postInfo.PostName,explan.ExamPlanName));//工种                
                    
                printData.Add("WorkStartDate", dt.Rows[i]["WorkStartDate"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[i]["WorkStartDate"]).ToString("yyyy年MM月dd日"));//工作开始时间
                printData.Add("PersonDetail", (dt.Rows[i]["PersonDetail"].ToString().Length > 180 ? dt.Rows[i]["PersonDetail"].ToString().Substring(0, 180) : dt.Rows[i]["PersonDetail"].ToString()));//工作简历
                printData.Add("HireUnitAdvise", dt.Rows[i]["HireUnitAdvise"].ToString());//聘用单位意见
                printData.Add("AdminUnitAdvise", dt.Rows[i]["AdminUnitAdvise"].ToString());//考核发证单位意见
                printData.Add("Phone", dt.Rows[i]["Phone"].ToString());  //联系电话     
                printData.Add("SKILLLEVEL", dt.Rows[i]["SKILLLEVEL"].ToString());//技术等级
                printData.Add("ImageName", dt.Rows[i]["CertificateCode"].ToString());//照片名称
                printData.Add("Img_FacePhoto", GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));//照片url

                
                //初审点信息
                if (explan.PostTypeID.ToString() == "1"//三类人
                  || explan.PostTypeID.ToString() == "5")//专业技术员
                {
                    printData.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}。{3}", dt.Rows[i]["SIGNUPMAN"], xmlBr, dt.Rows[i]["PlaceName"]
                        , dt.Rows[i]["Status"].ToString() == EnumManager.SignUpStatus.NewSignUp ? string.Format("请您 {0}到现场进行审核。{1}", Convert.ToDateTime(dt.Rows[i]["CHECKDATEPLAN"]).ToString("yyyy年MM月dd日"), (dt.Rows[i]["FIRSTCHECKTYPE"] != null && Convert.ToInt32(dt.Rows[i]["FIRSTCHECKTYPE"]) == -1 ? "由于您一年内上次未参加考试，本次须现场审核报考材料并出具上次考试缺考原因的证明材料。" : "")) : "您已通过系统审核，无需到现场审核考试材料。"
                        ));//备注
                }
                else
                {
                    printData.Add("Desc", string.Format("申请人：{0}{1}现场审核单位：{2}", dt.Rows[i]["SIGNUPMAN"], xmlBr, (dt.Rows[i]["TRAINUNITNAME"] != null && dt.Rows[i]["SIGNUPMAN"] != null && dt.Rows[i]["TRAINUNITNAME"].ToString() == dt.Rows[i]["SIGNUPMAN"].ToString()) ? dt.Rows[i]["SIGNUPMAN"].ToString() : "东、西部报名审核点"));//备注
                }

                //条码路径
                if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/SignUpTable/{0}/", explan.ExamPlanID))))
                {
                    System.IO.Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/SignUpTable/{0}/", explan.ExamPlanID)));
                }

                TCodePath = string.Format(@"../Upload/SignUpTable/{0}/{1}.png", explan.ExamPlanID, dt.Rows[i]["ExamSignUpID"]);
                
                if (System.IO.File.Exists(Server.MapPath(TCodePath)) == false)//本地不存在
                {
                    UIHelp.CreateTCode(explan.ExamPlanID, dt.Rows[i]["ExamSignUpID"]);
                }
                //条码
                printData.Add("ImageTCodeName", dt.Rows[i]["CertificateCode"].ToString());
                printData.Add("Img_TCode", TCodePath);
            }

            return list;
        }

        protected void RadComboBoxUnitName_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshGrid();
        }

        /// <summary>
        /// 绑定审核点及审核量统计
        /// </summary>
        /// <param name="ExamPlanID"></param>
        protected void BindRadGridSignupPlace(long ExamPlanID)
        {
            DataTable dtTj = ExamSignUpPlaceDAL.GetSignUpPlaceTj(ExamPlanID);
            RadGridSignupPlace.DataSource = dtTj;
            RadGridSignupPlace.DataBind();
        }

        /// <summary>
        /// 设置选中报名点
        /// </summary>
        /// <param name="SIGNUPPLACEID">报名点ID</param>
        private void BindSIGNUPPLACE_Select(long SIGNUPPLACEID)
        {
            //获取选中报名点
            for (int j = 0; j < RadGridSignupPlace.MasterTableView.Items.Count; j++)
            {
                if (Convert.ToInt64(RadGridSignupPlace.MasterTableView.DataKeyValues[j]["SIGNUPPLACEID"]) == SIGNUPPLACEID)
                {
                    RadioButton CheckBox1 = RadGridSignupPlace.MasterTableView.Items[j].Cells[0].FindControl("CheckBoxSIGNUPPLACEID") as RadioButton;
                    CheckBox1.Checked = true;
                    break;
                }
            }
        }
    }
}