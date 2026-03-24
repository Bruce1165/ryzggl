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

namespace ZYRYJG.PersonnelFile
{
    public partial class RepeatWarning : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////个人或企业
                //if (PersonType == 2 || PersonType == 3)
                //{
                //    RadComboBoxStatus.Items[0].Visible = false;
                //    RadComboBoxStatus.Items[2].Visible = false;
                //    RadComboBoxStatus.Items[3].Visible = false;
                //    RadComboBoxStatus.Items[4].Visible = false;
                //}
                ButtonSearch_Click(sender, e);

            }
        }


        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            QueryParamOB q = new QueryParamOB();

            if (PostSelect1.PostID != "")
            {
                q.Add(string.Format("c.PostID >= {0} and c.PostID <= {0}", PostSelect1.PostID));
            }
            else if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("c.PostTypeID = {0}", PostSelect1.PostTypeID));//岗位类别
            }

            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                //证书人姓名
                q.Add(string.Format("c.WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                //证件号码
                q.Add(string.Format("c.WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }


            string sql = @"  select ROW_NUMBER() over (order by posttypeid,PostName) as RowNum,* from 
                              (
                                              select count(*) CertCount ,[WORKERCERTIFICATECODE],[POSTID], max(posttypeid) posttypeid,max([WORKERNAME]) WORKERNAME,max(PostTypeName) PostTypeName,max(Postname) PostName,'' [SKILLLEVEL]
                                              FROM [dbo].[CERTIFICATE] c
                                              left join dbo.CertificateLock l on  l.LockStatus='加锁' and l.LockEndTime >getdate() and c.CERTIFICATEID =l.CERTIFICATEID
                                              where [VALIDENDDATE] > getdate() and [STATUS]<>'离京变更' and [STATUS] <>'注销' and [WORKERCERTIFICATECODE] is not null
                                              and (posttypeid <4 or posttypeid >4) and (postid <147  or postid >147)
                                              and l.CERTIFICATEID is  null {0}
                                              group by [WORKERCERTIFICATECODE],[POSTID]
                                              having count(*) >1
                                              union all
                                              select count(c.UNITCODE) CertCount ,[WORKERCERTIFICATECODE],[POSTID],max(posttypeid) posttypeid,max([WORKERNAME]) WORKERNAME,max(PostTypeName) PostTypeName,max(Postname) PostName,'' [SKILLLEVEL]
                                              FROM [dbo].[CERTIFICATE] c
                                              left join dbo.CertificateLock l on  l.LockStatus='加锁' and l.LockEndTime >getdate() and c.CERTIFICATEID =l.CERTIFICATEID
                                              where [VALIDENDDATE] > getdate() and [STATUS]<>'离京变更' and [STATUS] <>'注销' and [WORKERCERTIFICATECODE] is not null
                                              and  postid =147  
                                              and l.CERTIFICATEID is  null {0}
                                              group by [WORKERCERTIFICATECODE],[POSTID], c.UNITCODE
                                              having count(c.UNITCODE) >1   
                                              union all
                                             select count(*) CertCount ,[WORKERCERTIFICATECODE],[POSTID], max(posttypeid) posttypeid,max([WORKERNAME]) WORKERNAME,max(PostTypeName) PostTypeName,max(Postname) PostName,[SKILLLEVEL]
                                              FROM [dbo].[CERTIFICATE]  c
                                              left join dbo.CertificateLock l on  l.LockStatus='加锁' and l.LockEndTime >getdate() and c.CERTIFICATEID =l.CERTIFICATEID
                                              where [VALIDENDDATE] > getdate() and [STATUS]<>'离京变更' and [STATUS] <>'注销' and [WORKERCERTIFICATECODE] is not null
                                              and posttypeid =4 and l.CERTIFICATEID is  null {0}
                                              group by [WORKERCERTIFICATECODE],[POSTID],[SKILLLEVEL]
                                              having count(*) >1
                               ) t";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, q.ToWhereString()));


            RadGridApply.DataSource = dt;
            RadGridApply.DataBind();
        }
    }
}
