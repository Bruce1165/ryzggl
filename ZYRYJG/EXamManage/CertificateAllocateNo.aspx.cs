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
namespace ZYRYJG.EXamManage
{
    public partial class CertificateAllocateNo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager1.AjaxSettings.AddAjaxSetting((this.Master.FindControl("RadScriptManager1") as ScriptManager), RadGridExamResult);
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);
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

            //1、待放号证书
            ObjectDataSourceExamResult.SelectParameters.Clear();
            QueryParamOB examResult = new QueryParamOB();
            examResult.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
            examResult.Add(string.Format("Status = '{0}'", EnumManager.ExamResultStatus.Published));//已公告
            examResult.Add(string.Format("ExamResult = '{0}'", EnumManager.ExamResult.Pass));//已通过
           
            if(ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 3)//造价员需要判断增项
            {
                examResult.Add(string.Format(@"workercertificatecode not IN (
SELECT workercertificatecode FROM DBO.Certificate WHERE ExamPlanID = {1}
union 
SELECT workercertificatecode FROM DBO.Certificate 
WHERE postid={0} and validenddate >=getdate() and [STATUS] <>'注销' and [STATUS] <>'离京变更' and additemname is not null 
and workercertificatecode in (select workercertificatecode from DBO.VIEW_EXAMSCORE where ExamPlanID={1} and Status = '成绩已公告' and ExamResult = '合格' )
union select ''
)", (ExamPlanSelect1.PostID.Value == 9 ? "12" : "9"), ExamPlanSelect1.ExamPlanID.ToString()));

                div_additem.Style.Add("display", "block");
            }
            else//非造价员
            {
                //examResult.Add(string.Format(@"workercertificatecode not IN (SELECT workercertificatecode FROM DBO.Certificate WHERE ExamPlanID = {0} union select '')"
                //    ,(ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0")));

                examResult.Add(string.Format(@"(SELECT count(*) FROM DBO.Certificate WHERE ExamPlanID = {0})=0"
                    , (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0")));

                div_additem.Style.Add("display", "none");
            }
            ObjectDataSourceExamResult.SelectParameters.Add("filterWhereString", examResult.ToWhereString());
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSourceExamResult.ID;

            //2、已放号证书
            ObjectDataSourceCertificate.SelectParameters.Clear();
            
            QueryParamOB allocated = new QueryParamOB();
            allocated.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
            ObjectDataSourceCertificate.SelectParameters.Add("filterWhereString", allocated.ToWhereString());
            RadGridCertificate.CurrentPageIndex = 0;
            RadGridCertificate.DataSourceID = ObjectDataSourceCertificate.ID;
            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "CertificateCode";
            sortStr1.SortOrder = GridSortOrder.Ascending;
            RadGridCertificate.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            //3、增项证书
            ObjectDataSourceAddItem.SelectParameters.Clear();
            QueryParamOB addItem = new QueryParamOB();
            if (ExamPlanSelect1.PostTypeID.HasValue == false || ExamPlanSelect1.PostTypeID.Value != 3)
            {
                addItem.Add("1=2");
            }
            else
            {
                addItem.Add(string.Format(@"postid={0} and validenddate >=getdate() and additemname is not null and [STATUS] <>'注销' and [STATUS] <>'离京变更'
and workercertificatecode in (select workercertificatecode from DBO.VIEW_EXAMSCORE where ExamPlanID={1} and Status = '成绩已公告' and ExamResult = '合格' union select '' )"
                    , (ExamPlanSelect1.PostID.Value == 9 ? "12" : "9")
                    , ExamPlanSelect1.ExamPlanID.ToString()));                
            }
            ObjectDataSourceAddItem.SelectParameters.Add("filterWhereString", addItem.ToWhereString());
            RadGridAddItem.CurrentPageIndex = 0;
            RadGridAddItem.DataSourceID = ObjectDataSourceAddItem.ID;
        }

        /// <summary>
        /// 获取培训点编码
        /// </summary>
        /// <param name="TrainUnitName">培训点名称</param>
        /// <returns>培训点编码</returns>
        private string GetTrainUnitCode(DataTable trainCode,string TrainUnitName)
        {      
    
            DataRow dr = trainCode.Rows.Find(TrainUnitName);
            if (dr == null)
            {
                UIHelp.layerAlert(Page, "培训点不在规定范围内，无法编号！");
                throw new Exception("培训点不在规定范围内，无法编号！");
            }
            return dr["UnitNo"].ToString();
        }

        //编号
        protected void ZSBH(object sender, EventArgs e)
        {
            string startTime =RadDatePicker1.SelectedDate.Value.ToString("yyyy-MM-dd");
            string endTime = RadDatePicker2.SelectedDate.Value.ToString("yyyy-MM-dd");
            string ConferDate = RadDatePickerConferDate.SelectedDate.Value.ToString("yyyy-MM-dd");

            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有要放号的数据！");
                return;
            }
            if (startTime != ConferDate)
            {
                UIHelp.layerAlert(Page, "发证日期和证书有效期起始日期必须相同。",2,0);
                DivBase.Style.Add("display", "none");
                TableTime.Style.Add("display","block");
                return;
            }
            switch (ExamPlanSelect1.PostTypeID.Value)
            {
                case 1:
                    if (RadDatePicker1.SelectedDate.Value.AddYears(3).ToString("yyyy-MM-dd") != endTime)
                    {
                        UIHelp.layerAlert(Page, "证书有效期截止日期必须为起始日期加3年。", 2, 0);
                        DivBase.Style.Add("display", "none");
                        TableTime.Style.Add("display", "block");
                        return;
                    }
                    break;
                case 2:
                    if (RadDatePicker1.SelectedDate.Value.AddYears(2).ToString("yyyy-MM-dd") != endTime)
                    {
                        UIHelp.layerAlert(Page, "证书有效期截止日期必须为起始日期加2年。", 2, 0);
                        DivBase.Style.Add("display", "none");
                        TableTime.Style.Add("display", "block");
                        return;
                    }
                    break;
            }
            

            QueryParamOB examResult = new QueryParamOB();
            examResult.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
            examResult.Add(string.Format("Status = '{0}'", EnumManager.ExamResultStatus.Published));//已公告
            examResult.Add(string.Format("ExamResult = '{0}'", EnumManager.ExamResult.Pass));//已通过
            
            if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 3)//造价员需要判断增项
            {
                examResult.Add(string.Format(@"workercertificatecode not IN (
SELECT workercertificatecode FROM DBO.Certificate WHERE ExamPlanID = {1}
union 
SELECT workercertificatecode FROM DBO.Certificate 
WHERE postid={0} and validenddate >=getdate() and [STATUS] <>'注销' and [STATUS] <>'离京变更' and additemname is null 
and workercertificatecode in (select workercertificatecode from DBO.VIEW_EXAMSCORE where ExamPlanID={1} and Status = '成绩已公告' and ExamResult = '合格' )
union select ''
)", (ExamPlanSelect1.PostID.Value == 9 ? "12" : "9"), ExamPlanSelect1.ExamPlanID.ToString()));
            }
            else//非造价员
            {
                examResult.Add(string.Format(@"workercertificatecode not IN (SELECT workercertificatecode FROM DBO.Certificate WHERE ExamPlanID = {0} union select '')"
                    , (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0")));
            }          

            //待发证数据（考试成绩）
            DataTable dt =null;
            if(ExamPlanSelect1.PostID==147)//企业负责人查询带出是否为企业法人，非法人有效期受年龄限制，法人不限。（法人：IsFR=1；非法人：IsFR=0）
            {
                dt = CommonDAL.GetDataTable(string.Format("select * from [dbo].[VIEW_EXAMSCORE_WithFR] where 1=1 {0} order by ExamSignUpID",examResult.ToWhereString()));
            }
            else
            {
                dt = ExamResultDAL.GetListView_ExamScore(0, int.MaxValue - 1, examResult.ToWhereString(), "TrainUnitName,FirstTrialTime,ExamSignUpID");
            }
          

            //待增项数据
            DataTable dtAddItem = null;
            if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 3)//造价员
            {
                QueryParamOB addItem = new QueryParamOB();
                addItem.Add(string.Format(@"postid={0} and validenddate >=getdate() and additemname is null and [STATUS] <>'注销' and [STATUS] <>'离京变更'
and workercertificatecode in (select workercertificatecode from DBO.VIEW_EXAMSCORE where ExamPlanID={1} and Status = '成绩已公告' and ExamResult = '合格' union select '')"
                    , (ExamPlanSelect1.PostID.Value == 9 ? "12" : "9")
                    , ExamPlanSelect1.ExamPlanID.ToString()));
                //待增项数据
                dtAddItem = CertificateDAL.GetList(0, int.MaxValue - 1, addItem.ToWhereString(), "certificateid");
            }

            PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(ExamPlanSelect1.PostID.Value);//工种
            ExamPlanOB _ExamPlanOB = ExamPlanDAL.GetObject(ExamPlanSelect1.ExamPlanID.Value);//考试计划
            DataTable trainCode = null;//培训点编号
            if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 4000)//职业技能
            {
                
                trainCode = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.[TrainUnit]", "[UnitNo],[TrainUnitName]", " and [UseStatus]=1 ", "[UnitNo]");
                DataColumn[] pk = new DataColumn[1];
                pk[0] = trainCode.Columns["TrainUnitName"];
                trainCode.PrimaryKey = pk;
            }
            //if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 4)//职业技能
            //{
            //    _ExamPlanOB = ExamPlanDAL.GetObject(ExamPlanSelect1.ExamPlanID.Value);//考试计划
            //    trainCode = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.[USER]", "TrainUnitCode,RELUSERNAME", " and TRAINUNITCODE is not null ", "TrainUnitCode");
            //    DataColumn[] pk = new DataColumn[1];
            //    pk[0] = trainCode.Columns["RELUSERNAME"];
            //    trainCode.PrimaryKey = pk;
            //}
            List<Object> addList = new List<object>();
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //增项
                if (dtAddItem != null && dtAddItem.Rows.Count > 0)
                {
                    //添加增项记录
                    CertificateAddItemDAL.InsertBatch(tran, ExamPlanSelect1.ExamPlanID.Value, (ExamPlanSelect1.PostID.Value == 9 ? 12 : 9));
                    
                    //更新证书表增项名称
                    CertificateDAL.UpdateAddItem(tran, ExamPlanSelect1.ExamPlanID.Value, (ExamPlanSelect1.PostID.Value == 9 ? 12 : 9), PersonID, DateTime.Now, (ExamPlanSelect1.PostID.Value == 9 ? "安装,增土建" : "土建,增安装"));
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建证书
                    CertificateOB ceron = new CertificateOB();
                    ceron.ExamPlanID = Convert.ToInt64(dt.Rows[i]["ExamPlanID"]);//考试计划ID
                    ceron.WorkerID = Convert.ToInt64(dt.Rows[i]["WorkerID"]);//从业人员ID
                    if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 4)//职业技能
                    {
                        //ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, GetTrainUnitCode(trainCode, dt.Rows[i]["TrainUnitName"].ToString()), _ExamPlanOB.PlanSkillLevel, tran);//证书编号
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, "000", _ExamPlanOB.PlanSkillLevel, tran);//证书编号
                    }
                    else if (ExamPlanSelect1.PostTypeID.HasValue == true && ExamPlanSelect1.PostTypeID.Value == 4000)//新版职业技能
                    {
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, GetTrainUnitCode(trainCode, dt.Rows[i]["TrainUnitName"].ToString()), _ExamPlanOB.PlanSkillLevel, tran);//证书编号
                        //ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, "000", _ExamPlanOB.PlanSkillLevel, tran);//证书编号
                    }

                    else
                    {
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, tran);//证书编号
                    }
                    ceron.WorkerName = dt.Rows[i]["WorkerName"].ToString();//姓名
                    ceron.WorkerCertificateCode = dt.Rows[i]["WorkerCertificateCode"].ToString();//证件号码
                    ceron.PostTypeID = Convert.ToInt32(dt.Rows[i]["PostTypeID"]);//岗位
                    if(ceron.PostTypeID.Value==1)
                    {
                        if (dt.Rows[i]["Job"] != DBNull.Value)
                        {
                            ceron.Job = dt.Rows[i]["Job"].ToString();//职务
                        }
                    }
                    ceron.PostID = Convert.ToInt32(dt.Rows[i]["PostID"]);//工种
                    ceron.PostTypeName = dt.Rows[i]["PostTypeName"].ToString();//岗位
                    ceron.PostName = dt.Rows[i]["PostName"].ToString();//工种
                    ceron.Sex = dt.Rows[i]["S_Sex"] == DBNull.Value ? "" : dt.Rows[i]["S_Sex"].ToString();//性别
                    ceron.Birthday = dt.Rows[i]["S_Birthday"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[i]["S_Birthday"]);//出生日期
                    ceron.UnitName = dt.Rows[i]["UnitName"].ToString();//工作单位
                    ceron.UnitCode = dt.Rows[i]["UnitCode"].ToString();//组织机构代码
                    ceron.ConferDate = Convert.ToDateTime(ConferDate);//发证日期
                    ceron.ConferUnit = "北京市住建委";
                    ceron.ValidStartDate = Convert.ToDateTime(startTime);//证书有效期起
                    ceron.ValidEndDate = Convert.ToDateTime(endTime);//证书有效期止（此行不能删除，后面重新计算年龄时使用该字段值）
                    ceron.CreatePersonID = PersonID;//创建人ID
                    ceron.CreateTime = DateTime.Now;//创建时间
                    ceron.ModifyPersonID = PersonID;
                    ceron.ModifyTime = ceron.CreateTime;
                    ceron.Status = EnumManager.CertificateUpdateType.WaitCheck;
                    ceron.SkillLevel = dt.Rows[i]["SkillLevel"].ToString();//技术职称(技术等级)
                    if (dt.Rows[i]["TRAINUNITNAME"] != DBNull.Value)
                    {
                        ceron.ApplyMan = dt.Rows[i]["TRAINUNITNAME"].ToString();//业务办理申请人（报名、续期、变更时都改变）
                        ceron.TrainUnitName = dt.Rows[i]["TRAINUNITNAME"].ToString();//培训点
                    }

                    ceron.ValidEndDate = GetValidEndDateWithAge(ceron, ceron.PostID == 147 ? Convert.ToInt32(dt.Rows[i]["IsFR"]) : 0);//考虑年龄显示（2024-02-05日添加）

                    addList.Add(ceron);
                }

                if (addList.Count > 0)
                {
                    CommonDAL.InsertPatch(tran, addList, "dbo.Certificate", "ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PostTypeName,PostName,Job");
                }
                tran.Commit();
                string tip = "";
                if (dtAddItem != null && dtAddItem.Rows.Count > 0) tip = string.Format("其中有{0}本证书为增项，没有进行编号。", dtAddItem.Rows.Count.ToString());

                UIHelp.WriteOperateLog(PersonName, UserID, "证书编号", string.Format("考试计划：{0}；证书数量：{1}本；{2}"
                    , ExamPlanSelect1.ExamPlanName, dt.Rows.Count.ToString(), tip));

                UIHelp.layerAlert(Page, "证书编号成功！" + tip);
                DivBase.Style.Add("display", "block");
                TableTime.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书编号失败！", ex);
                return;
            }

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
            //《国务院关于渐进式延迟法定退休年龄的办法》自2025年1月1日起对现有安管人员、特种作业人员证书有效期进行相应调整:
            //1、60周岁男职工（安管人员、特种作业人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由60周岁退休改为63周岁退休
            //2、55周岁女职工（安管人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由55周岁退休改为58周岁退休
            //3、50周岁女职工（特种作业人员）延迟法定退休年龄按每两个月延长1月进行调整，最长由50周岁退休改为55周岁退休
            DateTime rtn;
            int Spanmonth = 0;//按老规则退休超期月份数量。
            int Addmonth = 0;//延长退休月份数量。
            switch (o.PostTypeID.Value)
            {
                case 1:
                    #region posttypeid=1

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
                            if (o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                            {
                                if (o.Birthday.Value.AddYears(60) >= o.ValidEndDate.Value)
                                    rtn = o.ValidEndDate.Value;
                                else
                                    rtn = o.Birthday.Value.AddYears(60);
                            }
                            else if (o.Birthday.Value.AddYears(60) >= Convert.ToDateTime("2025-01-01") && o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                            {
                                Spanmonth = (o.Birthday.Value.AddYears(60).Year - 2025) * 12 + o.Birthday.Value.AddYears(60).Month;
                                Addmonth = (Spanmonth - 1) / 4 + 1;
                                if (o.Birthday.Value.AddYears(60).AddMonths(Addmonth) >= o.ValidEndDate.Value)
                                    rtn = o.ValidEndDate.Value;
                                else
                                    rtn = o.Birthday.Value.AddYears(60).AddMonths(Addmonth);
                            }
                            else
                            {
                                if (o.Birthday.Value.AddYears(63).AddDays(1) < o.ValidEndDate.Value)//新规则
                                    rtn = o.Birthday.Value.AddYears(63);
                                else
                                    rtn = o.ValidEndDate.Value;
                            }
                        }
                        else//女
                        {
                            if (o.Birthday.Value.AddYears(55) < Convert.ToDateTime("2025-01-01"))//老规则
                            {
                                if (o.Birthday.Value.AddYears(55) >= o.ValidEndDate.Value)
                                    rtn = o.ValidEndDate.Value;
                                else
                                    rtn = o.Birthday.Value.AddYears(55);
                            }
                            else if (o.Birthday.Value.AddYears(55) >= Convert.ToDateTime("2025-01-01") && o.Birthday.Value.AddYears(55) < Convert.ToDateTime("2040-01-01"))//过渡期
                            {
                                Spanmonth = (o.Birthday.Value.AddYears(55).Year - 2025) * 12 + o.Birthday.Value.AddYears(55).Month;
                                Addmonth = (Spanmonth - 1) / 4 + 1;
                                if (o.Birthday.Value.AddYears(55).AddMonths(Addmonth) >= o.ValidEndDate.Value)
                                    rtn = o.ValidEndDate.Value;
                                else
                                    rtn = o.Birthday.Value.AddYears(55).AddMonths(Addmonth);
                            }
                            else
                            {
                                if (o.Birthday.Value.AddYears(58).AddDays(1) < o.ValidEndDate.Value)//新规则
                                    rtn = o.Birthday.Value.AddYears(58);
                                else
                                    rtn = o.ValidEndDate.Value;
                            }
                        }
                    }

                    #endregion posttypeid=1

                    break;
                case 2:

                    #region posttypeid=2

                    if (o.Sex == "男")
                    {
                        if (o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (o.Birthday.Value.AddYears(60) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(60);
                        }
                        else if (o.Birthday.Value.AddYears(60) >= Convert.ToDateTime("2025-01-01") && o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (o.Birthday.Value.AddYears(60).Year - 2025) * 12 + o.Birthday.Value.AddYears(60).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (o.Birthday.Value.AddYears(60).AddMonths(Addmonth) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(60).AddMonths(Addmonth);
                        }
                        else
                        {
                            if (o.Birthday.Value.AddYears(63).AddDays(1) < o.ValidEndDate.Value)//新规则
                                rtn = o.Birthday.Value.AddYears(63);
                            else
                                rtn = o.ValidEndDate.Value;
                        }
                    }
                    else//女
                    {
                        if (o.Birthday.Value.AddYears(50) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (o.Birthday.Value.AddYears(50) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(50);
                        }
                        else if (o.Birthday.Value.AddYears(50) >= Convert.ToDateTime("2025-01-01") && o.Birthday.Value.AddYears(50) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (o.Birthday.Value.AddYears(50).Year - 2025) * 12 + o.Birthday.Value.AddYears(50).Month;
                            Addmonth = (Spanmonth - 1) / 2 + 1;
                            if (o.Birthday.Value.AddYears(50).AddMonths(Addmonth) >= o.ValidEndDate.Value)
                                rtn = o.ValidEndDate.Value;
                            else
                                rtn = o.Birthday.Value.AddYears(50).AddMonths(Addmonth);
                        }
                        else
                        {
                            if (o.Birthday.Value.AddYears(55).AddDays(1) < o.ValidEndDate.Value)//新规则
                                rtn = o.Birthday.Value.AddYears(55);
                            else
                                rtn = o.ValidEndDate.Value;
                        }
                    }

                    #endregion posttypeid=2

                    break;
                default:
                    if (o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                    {
                        if (o.Birthday.Value.AddYears(60) >= o.ValidEndDate.Value)
                            rtn = o.ValidEndDate.Value;
                        else
                            rtn = o.Birthday.Value.AddYears(60);
                    }
                    else if (o.Birthday.Value.AddYears(60) >= Convert.ToDateTime("2025-01-01") && o.Birthday.Value.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                    {
                        Spanmonth = (o.Birthday.Value.AddYears(60).Year - 2025) * 12 + o.Birthday.Value.AddYears(60).Month;
                        Addmonth = (Spanmonth - 1) / 4 + 1;
                        if (o.Birthday.Value.AddYears(60).AddMonths(Addmonth) >= o.ValidEndDate.Value)
                            rtn = o.ValidEndDate.Value;
                        else
                            rtn = o.Birthday.Value.AddYears(60).AddMonths(Addmonth);
                    }
                    else
                    {
                        if (o.Birthday.Value.AddYears(63).AddDays(1) < o.ValidEndDate.Value)//新规则
                            rtn = o.Birthday.Value.AddYears(63);
                        else
                            rtn = o.ValidEndDate.Value;
                    }
                    break;
            }
            return rtn;
        }
        
        //格式化
        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                BtnBH.Visible = false;
            }
            else
            {
                RadDatePicker1.DbSelectedDate = DateTime.Now.ToShortDateString();
                RadDatePickerConferDate.DbSelectedDate = RadDatePicker1.DbSelectedDate;
                switch (ExamPlanSelect1.PostTypeID.Value)
                {
                    case 1:
                        RadDatePicker2.DbSelectedDate = RadDatePicker1.SelectedDate.Value.AddYears(3);
                        break;
                    case 2:
                        RadDatePicker2.DbSelectedDate = RadDatePicker1.SelectedDate.Value.AddYears(2);
                        break;
                }

                BtnBH.Visible = true;
            }
        }

        //控制按钮显示
        protected void RadGridCertificate_DataBound(object sender, EventArgs e)
        {
            if (RadGridCertificate.MasterTableView.Items.Count == 0)
                ButtonOutput.Visible = false;
            else
                ButtonOutput.Visible = true;
        }

        //控制按钮显示
        protected void RadGridAddItem_DataBound(object sender, EventArgs e)
        {
            if (RadGridAddItem.MasterTableView.Items.Count == 0)
                ButtonOutPutAddItem.Visible = false;
            else
                ButtonOutPutAddItem.Visible = true;
        }

        //导出已放号证书
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGridCertificate.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridCertificate.PageSize = RadGridCertificate.MasterTableView.VirtualItemCount;//
            RadGridCertificate.CurrentPageIndex = 0;
            RadGridCertificate.Rebind();
            RadGridCertificate.ExportSettings.ExportOnlyData = true;
            RadGridCertificate.ExportSettings.IgnorePaging = false;
            RadGridCertificate.ExportSettings.OpenInNewWindow = true;
            RadGridCertificate.MasterTableView.ExportToExcel();
        }

        //导出增项信息
        protected void ButtonOutPutAddItem_Click(object sender, EventArgs e)
        {
            if (RadGridAddItem.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridAddItem.PageSize = RadGridAddItem.MasterTableView.VirtualItemCount;//
            RadGridAddItem.CurrentPageIndex = 0;
            RadGridAddItem.Rebind();
            RadGridAddItem.ExportSettings.ExportOnlyData = true;
            RadGridAddItem.ExportSettings.IgnorePaging = false;
            RadGridAddItem.ExportSettings.OpenInNewWindow = true;
            RadGridAddItem.MasterTableView.ExportToExcel();
        }

        //格式化导出Excel
        protected void RadGridCertificate_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
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

        //格式化导出Excel
        protected void RadGridAddItem_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
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
    }
}