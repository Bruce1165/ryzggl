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
    public partial class CertifChangeCheckPatch : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                LabelPostType.Text = UIHelp.GetPostTypeNameByID(string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString());//岗位类别ID
                PostSelect1.LockPostTypeID();

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

            q.Add("Status = '已申请'");
            q.Add("ChangeType='京内变更'");
            q.Add("WORKERNAME=NEWWORKERNAME and SEX = NEWSEX and BIRTHDAY = NEWBIRTHDAY and WORKERCERTIFICATECODE=NEWWORKERCERTIFICATECODE");
            q.Add("ShebaoCheck = 1");
            q.Add("(ifupdatephoto is null or ifupdatephoto <1)");//未申请变更照片

            q.Add("CREATEPERSONID > 0");//排除企业申请的企业信息变更（CREATEPERSONID = 0）

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

            if (rdtxtApplyCode.Text.Trim() != "")   //申请批次号
            {
                q.Add(string.Format("ApplyCode like '%{0}%'", rdtxtApplyCode.Text.Trim()));
            }

            if (RadTextBoxApplyMan.Text.Trim() != "")//申请人
            {
                q.Add(string.Format("ApplyMan ='{0}'", RadTextBoxApplyMan.Text.Trim()));
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

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGrid1.MasterTableView.SortExpressions.AddSortExpression("PrintVer desc,CertificateChangeID");
            ////排序规则
            //RadGrid1.MasterTableView.AllowMultiColumnSorting = true;
            //RadGrid1.MasterTableView.SortExpressions.Clear();

            //GridSortExpression sortStr1 = new GridSortExpression();
            //sortStr1.FieldName = "PrintVer";
            //sortStr1.SortOrder = GridSortOrder.Descending;
            //RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            //GridSortExpression sortStr2 = new GridSortExpression();
            //sortStr2.FieldName = "CertificateChangeID";
            //sortStr2.SortOrder = GridSortOrder.Descending;
            //RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr2);


            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //审查决定
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");//更新选择状态
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }        
            int RowCount = 0;
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
            try
            {
                RowCount = CertificateChangeDAL.SelectCount(filterString);
                string bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
                string bgsh = UIHelp.GetNextBatchNumber(dtr, "BGSH"); //变更审核编批号
                //string bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
                //string bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号
                //ViewState["NoticeCode"] = bggz;

                string nowTime = DateTime.Now.ToString();

//                //1、根据证书id向历史表插入历史数据
//                string sqlHistory = @"
//			    INSERT INTO dbo.CertificateHistory(OPERATETYPE,CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,AddItemName  )
//                SELECT '变更',CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,AddItemName
//                FROM DBO.CERTIFICATE where CertificateID in(select CertificateID from dbo.VIEW_CERTIFICATECHANGE where 1=1 {0})";
//                dbhelper.ExcuteNonQuery(dtr, string.Format(sqlHistory,  filterString));

//                //2、修改原表数据
//                string sqlCertificate = @"
//                MERGE INTO dbo.Certificate t1 USING 
//                (select CERTIFICATEID,NewUnitName as UnitName,NewUnitCode as UnitCode,{0} as ModifyPersonID,'{1}' as ModifyTime,'{1}' as CheckDate
//                ,'{2}' as CheckMan,'通过' as CheckAdvise,'已归档' CaseStatus,ChangeType as STATUS,ApplyMan  from dbo.VIEW_CERTIFICATECHANGE 
//                 where 1=1 {3}
//                ) t2 ON t1.CERTIFICATEID=t2.CERTIFICATEID
//                 WHEN MATCHED THEN UPDATE SET t1.UnitName = t2.UnitName,t1.UnitCode = t2.UnitCode,t1.ModifyPersonID = t2.ModifyPersonID
//                ,t1.ModifyTime = t2.ModifyTime,t1.CheckDate = t2.CheckDate,t1.CheckMan = t2.CheckMan,t1.CheckAdvise = t2.CheckAdvise
//                ,t1.CaseStatus = t2.CaseStatus,t1.STATUS = t2.STATUS,t1.ApplyMan = t2.ApplyMan;";
//                dbhelper.ExcuteNonQuery(dtr, string.Format(sqlCertificate, PersonID, nowTime, PersonName,filterString));

                //3、修该变更记录
                string sqlChange = @"UPDATE dbo.CertificateChange
				SET	DealWay = '证书信息修改',""GETDATE"" = '{0}',GetResult = '{1}',GetMan = '{2}',GetCode = '{5}',CheckDate = '{0}',CheckResult = '{1}'
                ,CheckMan = '{2}',CheckCode = '{6}',Status = '{3}',ModifyPersonID = {4},ModifyTime = '{0}',PATCHSHEBAOCHECK=1 
			    WHERE CertificateChangeID in(select CertificateChangeID from dbo.VIEW_CERTIFICATECHANGE where 1=1 {7})";
                dbhelper.ExcuteNonQuery(dtr, string.Format(sqlChange, nowTime, "通过", PersonName, EnumManager.CertificateChangeStatus.Checked
                    , PersonID, bgslp, bgsh,  filterString));

                dtr.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "批量审查证书变更（社保符合）", string.Format("批量审核社保合格的京内变更，处理方式：{0}；审核批号：{1}；证书数量：{2}本。"
               , "证书信息修改", bgsh, RowCount));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "批量变更审核（社保符合）失败！", ex);
                return;
            }
            ClearGridSelectedKeys(RadGrid1);

            UIHelp.layerAlert(Page, string.Format("您已经成功的审核了 {0} 条数据！", RowCount.ToString()), 6, 3000);
            RadGrid1.DataBind();//刷新grid
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

    }
}
