using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.SystemManage
{
    public partial class SheBaoQuery : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SystemManage/RoleManage.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请修改输入信息。");
                return;
            }
            if (RadDatePickerEnd.SelectedDate.Value < RadDatePickerStart.SelectedDate.Value)
            {
                UIHelp.layerAlert(Page, "输入查询时间范围有误。");
                return;
            }

            if (RadDatePickerEnd.SelectedDate.Value > RadDatePickerStart.SelectedDate.Value.AddMonths(6))
            {
                UIHelp.layerAlert(Page, "一次最多查询6个月社保数据。");
                return;
            }

            string rtn = "";
            try
            {
                 rtn = Utility.HttpHelp.DoPostSheBaoQuery(
                    string.Format(@"type=GRQY010&time={0}&data={{""PAC014"":""{1}"",""ks"":""{2}"",""js"":""{3}""}}"
                    , DateTime.Now.ToString("yyyyMMddHHmmss")
                    , RadTextBoxSBCode.Text.Trim()
                    , RadDatePickerStart.SelectedDate.Value.ToString("yyyy-MM-dd")
                    , RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));

                //测试
                //string rtn = @"{""serialno"":""0436eebce1044a72a19594e9eeb6b722"",""signature"":""MEUCIQDEoR7LPh81t/3Ob+J47zP/Nq9hf9LDq/XEm78CBuiGRAIgTw0CbI7CLbqKXI4R2z8yhZndomq2JZdv6WUXZxw0ohE="",""digest"":""p//sG6qpE4e1iu2GmXay/ZtdO2LV9qWwWt9Eh6zuZec="",""secret"":""MHkCIQDAzCGFnoXnKhk7FwxZXdvdhYdtKz1cGiIOUQym1M5VAwIgHdRP1iOXyYHC+xgUftKB0pBEfGu5R6VZCuCvR1OfeGEEIDG6VUnn8xCFxkGZePzhyezyF3DNLcTHNU2XK/7T+d3LBBDM9RjNTJV3m4x0Js7GL3iR"",""time"":""20230830142229"",""code"":200,""message"":""成功"",""data"":{""CZZGSBJFYJL"":[{""AAE143"":""1"",""PAE001"":""2023-07-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""01"",""BZE016D"":""养老"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":6326},{""AAE143"":""1"",""PAE001"":""2023-06-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""01"",""BZE016D"":""养老"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":5869},{""AAE143"":""1"",""PAE001"":""2023-07-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""02"",""BZE016D"":""失业"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":6326},{""AAE143"":""1"",""PAE001"":""2023-06-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""02"",""BZE016D"":""失业"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":5869},{""AAE143"":""1"",""PAE001"":""2023-06-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""03"",""BZE016D"":""医疗"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":5869},{""AAE143"":""1"",""PAE001"":""2023-07-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""04"",""BZE016D"":""工伤"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":6326},{""AAE143"":""1"",""PAE001"":""2023-06-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""04"",""BZE016D"":""工伤"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":5869},{""AAE143"":1,""PAE001"":""2023-06-01"",""AAE143D"":""月缴费"",""AAB003"":""911101087577105140"",""BZE016"":""05"",""BZE016D"":""生育"",""AAB004"":""北京新能极科技开发有限公司"",""PIC002"":5869}]}}";

                ResponseResultSheBao rsp =Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResultSheBao>(rtn);
                if (rsp.code == 200)
                {
                    if (rsp.data == null)
                    {
                        divResult.InnerText = "社保系统无数据";
                    }
                    else
                    {
                        SheBaoMDL _SheBaoMDL = null;
                        foreach (SBJFYJL d in rsp.data.CZZGSBJFYJL)
                        {
                            _SheBaoMDL = new SheBaoMDL();
                            _SheBaoMDL.CertificateCode = RadTextBoxSBCode.Text.Trim();
                            _SheBaoMDL.WorkerName = WorkerDAL.GetUserObject(RadTextBoxSBCode.Text.Trim()).WorkerName;
                            _SheBaoMDL.ENT_Name = d.AAB004;
                            _SheBaoMDL.CreditCode = d.AAB003;
                            _SheBaoMDL.JFYF = Convert.ToInt32(Convert.ToDateTime(d.PAE001).ToString("yyyyMM"));
                            _SheBaoMDL.XZCode = d.BZE016;
                            _SheBaoMDL.XZName = d.BZE016D;
                            _SheBaoMDL.CJSJ = DateTime.Now;
                            SheBaoDAL.Insert(_SheBaoMDL);
                        }

                        ObjectDataSource1.SelectParameters.Clear();
                        QueryParamOB q = new QueryParamOB();
                        q.Add(string.Format("[CertificateCode]='{0}'", RadTextBoxSBCode.Text.Trim()));
                        q.Add(string.Format("[JFYF] >= {0}", RadDatePickerStart.SelectedDate.Value.ToString("yyyyMM")));
                        q.Add(string.Format("[JFYF] <= {0}", RadDatePickerEnd.SelectedDate.Value.ToString("yyyyMM")));

                        ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                        RadGrid1.CurrentPageIndex = 0;
                        RadGrid1.DataSourceID = ObjectDataSource1.ID;
                    }
                }
                else
                {
                    divResult.InnerText = rsp.message;
                    //divResult.InnerText = rtn;
                }
            }
            catch (Exception ex)
            {
                divResult.InnerText = string.Format("{0}。{1}", ex.Message,rtn);
                UIHelp.WriteErrorLog(Page, "查询社保失败！", ex);
                return;
            }
        }

        protected void ButtonSearchSheBaoCode_Click(object sender, EventArgs e)
        {
            string rtn = "";
            try
            {
                rtn = Utility.HttpHelp.DoPostSheBaoQuery(
                   string.Format(@"type=GRQY041&time={0}&data={{""PAC007"":""001"",""PAC008"":""{1}""}}"
                   , DateTime.Now.ToString("yyyyMMddHHmmss")
                   , RadTextBoxSBCode.Text.Trim()
                 ));

                divResult.InnerText =  rtn;
            }
            catch (Exception ex)
            {
                divResult.InnerText = string.Format("{0}。{1}", ex.Message, rtn);
                UIHelp.WriteErrorLog(Page, "查询社会保障号失败！", ex);
                return;
            }
        }
    }
}