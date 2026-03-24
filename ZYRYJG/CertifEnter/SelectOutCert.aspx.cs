using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;

namespace ZYRYJG.CertifEnter
{
    public partial class SelectOutCert : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRadGridCert();
            }
            else
            {
                if (Request["__EVENTTARGET"] == "DownCert")
                {
                    BindRadGridCert();
                }
            }
        }

        //绑定转出证书信息
        private void BindRadGridCert()
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            q.Add("out_certState ='06'");//转出状态编码
            q.Add("[out_certNum] not like  '京建安%'");//排除北京转出
            q.Add("[out_expiringDate] > getdate()");//未过期
            q.Add("[cjsj] > DATEADD(month,-2,getdate())");//近二个月获取            
            
            q.Add(string.Format("out_identityCard ='{0}'", WorkerCertificateCode));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridCert.CurrentPageIndex = 0;
            RadGridCert.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridCert_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.Style.Add("cursor", "pointer");
                e.Item.Attributes.Add("onclick", string.Format("setdata('{0}')", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["out_certNum"]));
            }
        }

        //发起同步外省转出证书数据
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            CertificateOutApplyMDL o = CertificateOutApplyDAL.GetObjectToday(WorkerCertificateCode);
            if(o !=null)
            {
                BindRadGridCert();
                if(o.checkRtnCode==0)
                {               
                    if(o.ApplyTime.Value.AddHours(0.5) < DateTime.Now)
                    {
                        o = new CertificateOutApplyMDL();
                        o.WorkerCertificateCode = WorkerCertificateCode;
                        o.ApplyTime = DateTime.Now;
                        CertificateOutApplyDAL.Insert(o);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "waitDown", "waitDown();", true);
                    }
                    else
                    {
                        BindRadGridCert();
                        UIHelp.layerAlert(Page, string.Format("同步数据出错，错误描述：{0}",o.checkInfo));
                    }
                }
                else if(o.checkRtnCode.HasValue ==false)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "waitDown", "waitDown();", true);
                }
            }
            else
            {
                o = new CertificateOutApplyMDL();
                o.WorkerCertificateCode = WorkerCertificateCode;
                o.ApplyTime = DateTime.Now;
                CertificateOutApplyDAL.Insert(o);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "waitDown", "waitDown();", true);
                //UIHelp.layerAlert(Page, "成功发起申请同步请求，请耐心等待几分钟。", "waitDown();");
            }

        }

    }
}