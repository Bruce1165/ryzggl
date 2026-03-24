using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

/// <summary>
/// 播放用户校验
/// </summary>
public partial class Student_GenseePlay : System.Web.UI.Page
{
    protected DateTime GetPlayTime(string parm)
    {
        return Convert.ToDateTime(parm.Substring(0, 10) + " " + parm.Substring(10));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //key  "学习时间{0},课件ID{1},用户证件号码{2}"

        string key = Request.Form["k"];

        key = Utility.Cryptography.Decrypt(key);
        //Utility.FileLog.WriteLog(key);

        string[] parms = key.Split(',');
        if (GetPlayTime(parms[0]).AddDays(1) < DateTime.Now)//身份认证码失效
        {
            Utility.FileLog.WriteLog("连接失效！");
            Response.Write("fail");
            return;
        }
        if (parms[1] == "-1" && parms[2] == "-1")//测试人员
        {
            Response.Write("pass");
            Response.End();
            return;
        }

        int count = CommonDAL.SelectRowCountDB("DBRYPX", "[Source]", string.Format(" and SourceID={0} ", parms[1]));

        if (count == 0)
        {
            Utility.FileLog.WriteLog("非法课件，无法播放！");
            Response.Write("fail");
            return;
        }

        WorkerOB o = WorkerDAL.GetUserObject(parms[2]);
        if (o == null)
        {
            Utility.FileLog.WriteLog("非法用户，无法播放！");
            Response.Write("fail");
            return;
        }

        Response.Write("pass");
        Response.End();
    }
}