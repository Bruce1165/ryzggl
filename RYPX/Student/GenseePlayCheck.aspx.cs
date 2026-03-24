using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

/// <summary>
/// 播放进度接收处理
/// </summary>
public partial class Student_GenseePlayCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string ClassNo = Request["ClassNo"];
            string Operatior = Request["Operator"];
            string Action = Request["Action"];

            //try
            //{
            //    Utility.FileLog.WriteLog(string.Format("播放实时监控：ClassNo={0}，Operatior={1}，Action={2}", Request["ClassNo"], Request["Operator"], Request["Action"]), null);
            //}
            //catch (Exception ex)
            //{
            //    Utility.FileLog.WriteLog("返回播放实时监控失败！", ex);
            //}

            DateTime sendTime = DateTime.Now;//响应时间
            long WORKERID = Convert.ToInt64(Operatior.Remove(0, 1));
            WorkerOB c = WorkerDAL.GetObject(WORKERID);//人员信息
            if (c == null) { return; }

            SourceMDL _sourceOB = SourceDAL.GetObjectBySourceWarePlayParam(ClassNo);//课件信息
            if (_sourceOB == null) { return; }

            FinishSourceWareMDL f = FinishSourceWareDAL.GetObject(c.CertificateCode, _sourceOB.SourceID.Value);
            if (f == null)//new
            {
                f = new FinishSourceWareMDL();//学习记录
                f.SourceID = _sourceOB.SourceID;
                f.WorkerCertificateCode = c.CertificateCode;
                f.WorkerName = c.WorkerName;
                f.Period = _sourceOB.Period;
                f.LearnTime = sendTime;
                f.FinishPeriod = 0;
                f.StudyStatus = 0;
                f.PlayAction = 111;
                FinishSourceWareDAL.Insert(f);

            }
            else if (f.Period.Value * 60 > f.FinishPeriod.Value)//update
            {
                if (f.FinishPeriod.HasValue == false) f.FinishPeriod = 0;

                if (Action == "111")//开始播放
                {

                    if (f.PlayAction == 111 || f.PlayAction == 113)//如果前一次播放结束动作尚未推送，设置为113临时状态
                    {
                        f.PlayAction = 113;
                    }
                    else
                    {
                        f.LearnTime = sendTime;
                        f.PlayAction = 111;
                    }                    
                    
                }
                else if (Action == "112")//结束播放
                {                   
                    TimeSpan ts = sendTime - f.LearnTime.Value;
                    if (ts.TotalSeconds > 0)
                    {
                        f.FinishPeriod = f.FinishPeriod.Value + Convert.ToInt32(ts.TotalSeconds);
                    }
                    if (f.PlayAction == 113)//如果为113临时状态，设置为开始播放
                    {
                        f.PlayAction = 111;
                    }
                    else
                    {
                        f.PlayAction = 112;
                    }
                    f.LearnTime = sendTime;

                }
                
                //不能超过课件本身时长
                if ((f.FinishPeriod.Value + 600) > (f.Period.Value * 60))
                {
                    f.FinishPeriod = f.Period.Value * 60;
                    f.PlayAction = 112;
                }
                FinishSourceWareDAL.Update(f);
            }
            //else
            //{
            //    f.LearnTime = sendTime;
            //    FinishSourceWareDAL.Update(f);
            //}
        }
        catch (Exception ex)
        {
            Utility.FileLog.WriteLog("记录课件学习状态失败！", ex);
            return;
        }
    }

    //主动获取播放记录（有调用次数限制，最终放弃了。）
    //string apiUrl = "http://ejiandu.gensee.com/integration/site/training/export/study/simple/history";
    //            string rtn;//接口返回字符串
    //            rtn = Utility.HttpHelp.DoPostWithUrlParams(apiUrl, "vodId=hIf6lwiyed&uid=1001270598&loginName=admin@ejiandu.com&password=LL13671197677");

    //            ResponseResultGenseePlayHis result = JsonConvert.DeserializeObject<ResponseResultGenseePlayHis>(rtn);
    //            if (result.Code == "0")
    //            {
    //                foreach (GenseePlayHis r in result.message.GenseePlayHis)
    //                {
    //                    Response.Write(string.Format("{0} - {1}：{2}"
    //                        , TimeZoneInfo.ConvertTimeFromUtc(new DateTime(r.startTime), TimeZoneInfo.Local)
    //                        , TimeZoneInfo.ConvertTimeFromUtc(new DateTime(r.leaveTime), TimeZoneInfo.Local)
    //                        , Convert.ToInt64(r.duration) / 1000
    //                        ));
    //                    Response.Write("<br />");
    //                }
    //            }
    //            else
    //            { }
    //            // Utility.JsonTool jsonTool = new Utility.JsonTool(rtn);
}