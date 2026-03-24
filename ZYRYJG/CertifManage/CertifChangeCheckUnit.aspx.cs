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
    public partial class CertifChangeCheckUnit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSearch_Click(sender, e);
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
            ClearGridSelectedKeys(RadGrid1);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            q.Add(string.Format("(NewUnitcode like '{0}%')", ZZJGDM)); //改为现单位确认
            //q.Add(string.Format("(Unitcode like '{0}%' or NewUnitcode like '{0}%')", ZZJGDM)); //企业机构代码

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

            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));     //岗位类别
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
            
            if (RadComboBoxChangeType.SelectedItem.Value != "")//变更类型
            {
                q.Add(string.Format("ChangeType='{0}'",RadComboBoxChangeType.SelectedItem.Value));
            }

            if (RadioButtonListStatus.SelectedItem.Value == "已审查")
            {
                q.Add(string.Format("((UnitCode= '{0}' and OldUnitCheckTime >'1950-1-1') or (NewUnitCode= '{0}' and NewUnitCheckTime >'1950-1-1'))", ZZJGDM));   
            }
            else//未审查
            {
                q.Add(string.Format("Status = '待单位确认' and ((UnitCode= '{0}' and OldUnitCheckTime is null) or (NewUnitCode= '{0}' and NewUnitCheckTime is null))", ZZJGDM));   
            }

            if (RadComboBoxDealWay.SelectedItem.Value != "")//处理方式
            {
                if (RadComboBoxDealWay.SelectedItem.Value == "退回修改")
                {
                    q.Add("Status = '退回修改'");
                }
                else
                {
                    q.Add(string.Format("(Status <> '退回修改' and ((UnitCode= '{0}' and OldUnitCheckTime >'1950-1-1') or (NewUnitCode= '{0}' and NewUnitCheckTime >'1950-1-1')))", ZZJGDM));
                }
            }
            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
            {
                q.Add(string.Format("((UnitCode= '{0}' and OldUnitCheckTime >='{1}') or (NewUnitCode= '{0}' and NewUnitCheckTime >='{1}'))", ZZJGDM,RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
            {
                q.Add(string.Format("((UnitCode= '{0}' and OldUnitCheckTime <'{1}') or (NewUnitCode= '{0}' and NewUnitCheckTime <'{1}'))", ZZJGDM,  RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;

        }

        System.Collections.Generic.Dictionary<string, int> unitChaiqianLimit = new Dictionary<string,int>();//企业最低拆迁员人数要求
        System.Collections.Generic.Dictionary<string, int> unitChaiqianCount = new Dictionary<string,int>();//企业当前拆迁员人数

        //检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查）
        //规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制        
        public bool CheckChaiQianLimit(string ChangeType,string UnitCode,string NewUnitCode,string UnitName)
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
                UIHelp.WriteOperateLog(PersonName, UserID, "审查决定删除证书变更", string.Format("证书数量：{0}本；证书编号：{1}。",deleteRows.ToString(), sb.ToString()));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "审查决定失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, string.Format("您已经成功的删除了 {0}条数据！", deleteRows.ToString()),6,3000);
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();//刷新grid
        }
        
        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifChangeAccept/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifChangeAccept/"));
        }
    }
}
