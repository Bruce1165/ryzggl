using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using DataAccess;
using Model;
using Telerik.Web.UI;
using Utility;
using System.IO;
using System.Text;

/// <summary>
///     UI帮助类
/// </summary>
public class UIHelp
{
    /// <summary>
    /// 为附件url地址添加访问权限参数
    /// </summary>
    /// <param name="url">附件url地址</param>
    /// <returns>带访问权限参数的url地址</returns>
    public static string AddUrlReadParam(string url)
    {
        string radom = GetReadParam();

        if (url.Contains("?") == false)
            return string.Format("{0}?read={1}", url, radom);
        else
            return string.Format("{0}&read={1}", url, radom);
    }

    /// <summary>
    /// 获取附件下载权限参数
    /// </summary>
    /// <returns>权限参数</returns>
    public static string GetReadParam()
    {
        return Utility.Cryptography.Encrypt(string.Format("{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess"));
    }

    /// <summary>
    /// 获取Url地址中排除？号及后参数的实际地址
    /// </summary>
    /// <param name="url">url地址</param>
    /// <returns>排除？号及后参数的Url地址</returns>
    public static string GetUrlNoParam(string url)
    {
        if (url.Contains("?") == false)
            return url;
        else
            return url.Substring(0, url.IndexOf("?"));
    }

    /// <summary>
    /// 二建证书锁定提示
    /// </summary>
    public static string ErJianCertLockMessage
    {
        get
        {
            return string.Format("您的证书注册状态异常，已被锁定，请联系注册中心核实相关情况！<a target=\"_blank\" href=\"http://120.52.185.14/Register/NewsView.aspx?o={0}\" style=\"text-decoration:underline\">（关于注册状态异常标记的解释及处理流程）</a>", Utility.Cryptography.Encrypt("1cf44934-db2b-4de2-9ab5-a6650417824a"));
        }
    }

    /// <summary>
    /// 下载标准提示
    /// </summary>
    public static string DownLoadTip
    {
        get
        {
            return "已经为您准备好了数据，单击下面链接进行下载(如果无法下载,请选择右键另存为) ：";
        }
    }

    /// <summary>
    /// 分公司禁止注册标准提示
    /// </summary>
    public static string Tip_SubUnitForbid
    {
        get
        {
            return "二级造价工程师和二级建造师不允许注册在分公司，请选择总公司申请注册。";
        }
    }

    #region 工作日处理


    /// <summary>
    /// 获取工作日配置集合：一般工作日和休息日不记录，只记录非常规工作日和休息日。
    /// [WorkDay]：日期；[WorkDayState]：休息/工作
    /// </summary>
    /// <returns></returns>
    public static DataTable GetWorkDayList()
    {
        DataTable dt = null;

        if (System.Web.HttpContext.Current.Cache["WorkDaySet"] != null)
        {
            dt = System.Web.HttpContext.Current.Cache["WorkDaySet"] as DataTable;
        }
        else
        {
            dt = CommonDAL.GetDataTable("SELECT [WorkDay],[WorkDayState],[AddTime] FROM [dbo].[WorkDaySet]");
            DataColumn[] pk = new DataColumn[1];
            pk[0] = dt.Columns["WorkDay"];
            dt.PrimaryKey = pk;
            System.Web.HttpContext.Current.Cache["WorkDaySet"] = dt;
        }
        return dt;
    }

    /// <summary>
    /// 计算给定时间d与当前时间相差多少工作日
    /// </summary>
    /// <param name="d">比对时间</param>
    /// <returns>工作日天数</returns>
    public static int ComputWorkDaySpan(DateTime d)
    {
        DataTable dt = GetWorkDayList();
        DateTime cur = DateTime.Now;

        int WaitDays = Convert.ToInt32((cur.Date - d.Date).TotalDays);
        if (WaitDays <= 0)
        {
            return 0;
        }

        int weeks = WaitDays / 7;
        int weekmode = WaitDays % 7;
        int week = (int)d.DayOfWeek;

        if (week + weekmode == 6)
        {
            WaitDays = WaitDays - (weeks * 2) - 1;
        }
        else if (week + weekmode > 6)
        {
            WaitDays = WaitDays - (weeks * 2) - 2;
        }
        else
        {
            WaitDays = WaitDays - (weeks * 2);
        }

        if (week == 6)
        {
            WaitDays = WaitDays + 1;
        }


        DataRow find = null;
        while (d.Date < cur.Date)
        {
            d = d.AddDays(1);
            find = dt.Rows.Find(d.Date);
            if (find != null)
            {
                if (find["WorkDayState"].ToString() == "休息")
                {
                    WaitDays = WaitDays - 1;
                }
                else
                {
                    WaitDays = WaitDays + 1;
                }
            }
        }
        return WaitDays;
    }

    /// <summary>
    /// 格式化考试报名审批时限
    /// </summary>
    /// <param name="StartCheckDate">开始审核日期</param>
    /// <param name="ApplyStatus">申请状态</param>
    /// <returns></returns>
    public static string formatCheckListExam(object StartCheckDate, object ApplyStatus)
    {

        int limitDays = 5;//审核时限（天数）
        int WaitDays = 0;//等待审批天数

        if (ApplyStatus.ToString() == EnumManager.SignUpStatus.ReturnEdit ||
            ApplyStatus.ToString() == EnumManager.SignUpStatus.SaveSignUp ||
            ApplyStatus.ToString() == EnumManager.SignUpStatus.PayConfirmed)
        {
            return "";
        }

        if (StartCheckDate != DBNull.Value)//已到市级审批
        {
            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(StartCheckDate));
        }

        return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
           , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
    }


    #endregion 工作日处理

    #region 下拉列表控件(RadComboBox)处理

    /// <summary>
    /// 填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeID">类型ID</param>
    public static void FillDropDownList(RadComboBox rcb, string typeID)
    {
        DataTable dt = TypesDAL.GetList(0, int.MaxValue - 1, "and TypeID='" + typeID + "'", "SortID");
        if (rcb == null) return;
        if (dt == null) return;
        if (dt.Rows.Count == 0) return;

        //foreach (DataRow dataRow in dt.Rows)
        //{
        //    rcb.Items.Add(new RadComboBoxItem(dataRow["TypeName"].ToString(), dataRow["TypeValue"].ToString()));
        //}
        rcb.DataSource = dt;
        rcb.DataTextField = "TypeName";
        rcb.DataValueField = "TypeValue";
        rcb.DataBind();
    }

    /// <summary>
    /// 填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeID">类型ID</param>
    /// <param name="FirstName">附加首项名称</param>
    /// /// <param name="FirtValue">附加首项值</param>
    public static void FillDropDownList(RadComboBox rcb, string typeID, string FirstName, string FirtValue)
    {
        DataTable dt = TypesDAL.GetList(0, int.MaxValue - 1, "and TypeID='" + typeID + "'", "SortID");
        if (rcb == null) return;
        if (dt == null) return;
        if (dt.Rows.Count == 0) return;

        //foreach (DataRow dataRow in dt.Rows)
        //{
        //    rcb.Items.Add(new RadComboBoxItem(dataRow["TypeName"].ToString(), dataRow["TypeValue"].ToString()));
        //}


        rcb.DataSource = dt;
        rcb.DataTextField = "TypeName";
        rcb.DataValueField = "TypeValue";
        rcb.DataBind();
        rcb.Items.Insert(0, new RadComboBoxItem(FirstName, FirtValue));
    }

    /// <summary>
    /// 填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeID">类型ID</param>
    public static void FillDropDownListWithTypeName(RadComboBox rcb, string typeID)
    {
        DataTable dt = TypesDAL.GetList(0, int.MaxValue - 1, "and TypeID='" + typeID + "'", "SortID");
        if (rcb == null) return;
        if (dt == null) return;
        if (dt.Rows.Count == 0) return;

        //foreach (DataRow dataRow in dt.Rows)
        //{
        //    rcb.Items.Add(new RadComboBoxItem(dataRow["TypeName"].ToString(), dataRow["TypeName"].ToString()));
        //}
        rcb.DataSource = dt;
        rcb.DataTextField = "TypeName";
        rcb.DataValueField = "TypeName";
        rcb.DataBind();
    }

    /// <summary>
    /// 填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeID">类型ID</param>
    /// <param name="FirstName">附加首项名称</param>
    /// /// <param name="FirtValue">附加首项值</param>
    public static void FillDropDownListWithTypeName(RadComboBox rcb, string typeID, string FirstName, string FirtValue)
    {
        DataTable dt = TypesDAL.GetList(0, int.MaxValue - 1, "and TypeID='" + typeID + "'", "SortID");
        if (rcb == null) return;
        if (dt == null) return;
        if (dt.Rows.Count == 0) return;

        rcb.DataSource = dt;
        rcb.DataTextField = "TypeName";
        rcb.DataValueField = "TypeName";
        rcb.DataBind();
        rcb.Items.Insert(0, new RadComboBoxItem(FirstName, FirtValue));
    }

    ///// <summary>
    /////     填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    ///// </summary>
    ///// <param name="rcb">下拉列表控件(RadComboBox)</param>
    ///// <param name="typeId">类型ID</param>
    ///// <param name="defaultItemDisplay">显示默认项全部</param>
    //public static void FillDropDownList(RadComboBox rcb, string typeId, bool defaultItemDisplay = true)
    //{
    //    if (rcb == null)
    //        return;
    //    var dt = DictionaryDAL.GetFillDropDownDataByTypeId(Convert.ToInt32(typeId));
    //    if (dt == null || dt.Rows.Count.Equals(0))
    //        return;

    //    rcb.Items.Clear();

    //    foreach (DataRow dataRow in dt.Rows)
    //    {
    //        rcb.Items.Add(new RadComboBoxItem(dataRow["TypeName"].ToString(), dataRow["TypeValue"].ToString()));
    //    }
    //    rcb.DataSource = dt;
    //    rcb.DataTextField = "TypeName";
    //    rcb.DataValueField = "TypeValue";
    //    rcb.DataBind();
    //    if (defaultItemDisplay)
    //        rcb.Items.Insert(0, new RadComboBoxItem("全部", "0"));
    //}

    ///// <summary>
    /////     填充系统枚举类型(表Types)到下拉列表控件(RadComboBox)
    ///// </summary>
    ///// <param name="rcb">下拉列表控件(RadComboBox)</param>
    ///// <param name="typeId">类型ID</param>
    ///// <param name="defaultName">显示默认项,如全部或请选择</param>
    ///// <param name="defaultValue">默认项实际值，如0或空</param>
    ///// <param name="isOnlyBindText">是否只绑定text，即text，value都绑定显示值</param>
    //public static void FillDropDownList(RadComboBox rcb, string typeId, string defaultName, string defaultValue,
    //    bool isOnlyBindText)
    //{
    //    if (rcb == null)
    //        return;
    //    var dt = DictionaryDAL.GetFillDropDownDataByTypeId(Convert.ToInt32(typeId));
    //    if (dt == null || dt.Rows.Count.Equals(0))
    //        return;

    //    rcb.Items.Clear();

    //    foreach (DataRow dataRow in dt.Rows)
    //    {
    //        rcb.Items.Add(new RadComboBoxItem(dataRow["TypeName"].ToString(), dataRow["TypeValue"].ToString()));
    //    }
    //    rcb.DataSource = dt;
    //    rcb.DataTextField = "TypeName";
    //    rcb.DataValueField = isOnlyBindText ? "TypeName" : "TypeValue";
    //    rcb.DataBind();
    //    rcb.Items.Insert(0, new RadComboBoxItem(defaultName, defaultValue));
    //}

    /// <summary>
    ///     设置选中下拉列表控件(RadComboBox)某一项
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeValue">绑定值TypeValue</param>
    public static void SelectDropDownListItemByValue(RadComboBox rcb, string typeValue)
    {
        int i;
        int intLen = rcb.Items.Count;
        for (i = 0; i < intLen; i++)
        {
            rcb.Items[i].Selected = false;
        }
        for (i = 0; i < intLen; i++)
        {
            if (typeValue == rcb.Items[i].Value) rcb.Items[i].Selected = true;
        }
    }

    /// <summary>
    /// 设置选中下拉列表控件(RadComboBox)某一项
    /// </summary>
    /// <param name="rcb">下拉列表控件(RadComboBox)</param>
    /// <param name="typeText">绑定值TypeText</param>
    public static void SelectDropDownListItemByText(RadComboBox rcb, string typeText)
    {
        int i;
        int intLen = rcb.Items.Count;
        for (i = 0; i < intLen; i++)
        {
            rcb.Items[i].Selected = false;
        }
        for (i = 0; i < intLen; i++)
        {
            if (typeText == rcb.Items[i].Text) rcb.Items[i].Selected = true;
        }
    }

    /// <summary>
    /// 初始化年度选择框
    /// </summary>
    /// <param name="rcb">下拉控件</param>
    /// <param name="defaultItemDisplay">默认年度</param>
    /// <param name="year">起始年度</param>
    public static void FillYearDropDownList(RadComboBox rcb, bool defaultItemDisplay, int year)
    {
        if (rcb == null)
            return;
        for (int i = DateTime.Now.Year; i >= year; i--)
        {
            rcb.Items.Add(new RadComboBoxItem(i.ToString()));
        }
        if (defaultItemDisplay)
            rcb.Items.Insert(0, new RadComboBoxItem("全部"));
    }

    #endregion

    #region  获取、赋值

    /// <summary>
    /// 设置只读属性
    /// </summary>
    /// <param name="ctl">控件</param>
    /// <param name="ReadOnly">是否只读</param>
    public static void SetReadOnly(RadTextBox ctl, bool ReadOnly)
    {
        ctl.ReadOnly = ReadOnly;
        if (ReadOnly == true)
        {
            ctl.Style.Add("border", "none");
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Remove("border");
                ctl.Style.Add("cursor", "default");
            }
            catch { }
        }
    }
    public static void SetReadOnly(RadNumericTextBox ctl, bool ReadOnly)
    {
        ctl.ReadOnly = ReadOnly;
        if (ReadOnly == true)
        {
            ctl.Style.Add("border", "none");
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Remove("border");
                ctl.Style.Add("cursor", "default");
            }
            catch { }
        }
    }
    /// <summary>
    /// 设置只读属性
    /// </summary>
    /// <param name="ctl">控件</param>
    /// <param name="ReadOnly">是否只读</param>
    public static void SetReadOnly(TextBox ctl, bool ReadOnly)
    {
        ctl.ReadOnly = ReadOnly;
        if (ReadOnly == true)
        {
            ctl.Style.Add("border", "none");
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Remove("border");
                ctl.Style.Add("cursor", "default");
            }
            catch { }
        }
    }
    /// <summary>
    /// 设置只读属性
    /// </summary>
    /// <param name="ctl">控件</param>
    /// <param name="ReadOnly">是否只读</param>
    public static void SetReadOnly(RadComboBox ctl, bool ReadOnly)
    {
        ctl.Enabled = !ReadOnly;
        if (ReadOnly == true)
        {
            ctl.Style.Add("border", "none");
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Remove("border");
                ctl.Style.Add("cursor", "default");
            }
            catch { }
        }
    }
    /// <summary>
    /// 设置只读属性
    /// </summary>
    /// <param name="ctl">控件</param>
    /// <param name="ReadOnly">是否只读</param>
    public static void SetReadOnly(RadDatePicker ctl, bool ReadOnly)
    {
        ctl.Enabled = !ReadOnly;
        if (ReadOnly == true)
        {
            ctl.Style.Add("border", "none");
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Remove("border");
                ctl.Style.Add("cursor", "default");
            }
            catch { }
        }
    }

    /// <summary>
    /// 设置按钮可用属性
    /// </summary>
    /// <param name="ctl">控件</param>
    /// <param name="Enabled">是否可用</param>
    public static void SetButtonEnable(Button ctl, bool Enabled)
    {
        ctl.Enabled = Enabled;
        if (Enabled == false)
        {
            ctl.Style.Add("cursor", "not-allowed");
        }
        else
        {
            try
            {
                ctl.Style.Add("cursor", "pointer");
            }
            catch { }
        }
    }

    /// <summary>
    ///     将对象类_Object属性值赋给编辑面板UpdatePanel面板中对应的控件
    /// </summary>
    /// <param name="container">控件容器</param>
    /// <param name="_Object">数据实体类</param>
    /// <param name="readOnly">是否将控件设置为只读</param>
    public static void SetData(GridEditableItem container, object _Object, bool readOnly)
    {
        if (_Object == null) return;
        Type type_cls = _Object.GetType();
        PropertyInfo[] fdInfo = type_cls.GetProperties(); //反射类的属性

        Control _Control = null;
        object _Value;
        for (int i = 0; i < fdInfo.Length; i++)
        {
            _Control = container.FindControl("RadTextBox" + fdInfo[i].Name);
            if (_Control != null) //给RadTextBox赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);
                ((RadTextBox)_Control).Text = (_Value == null ? "" : FormatDecimal(_Value.ToString()));
                if (readOnly)
                {
                    ((RadTextBox)_Control).ReadOnly = true;
                    //((RadTextBox)_Control).Style.Add("background-color", readonlyBackgroundColor);
                    ((RadTextBox)_Control).Style.Add("border", "none");
                    ((RadTextBox)_Control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }

            _Control = container.FindControl("RadNumericTextBox" + fdInfo[i].Name);
            if (_Control != null) //给RadNumericTextBox赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);
                ((RadNumericTextBox)_Control).Text = (_Value == null ? "" : FormatDecimal(_Value.ToString()));
                if (readOnly)
                {
                    ((RadNumericTextBox)_Control).ReadOnly = true;
                    //((RadNumericTextBox)_Control).Style.Add("background-color", readonlyBackgroundColor);
                    ((RadNumericTextBox)_Control).Style.Add("border", "none");
                    ((RadNumericTextBox)_Control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }

            _Control = container.FindControl("TextBox" + fdInfo[i].Name);
            if (_Control != null) //给TextBox赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);
                ((TextBox)_Control).Text = (_Value == null ? "" : FormatDecimal(_Value.ToString()));
                if (readOnly)
                {
                    ((TextBox)_Control).ReadOnly = true;
                    //((TextBox)_Control).Style.Add("background-color", readonlyBackgroundColor);
                    ((TextBox)_Control).Style.Add("border", "none");
                    ((TextBox)_Control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }

            _Control = container.FindControl("RadComboBox" + fdInfo[i].Name);
            if (_Control != null) //给RadComboBox赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);
                SelectDropDownListItemByValue((RadComboBox)_Control, (_Value == null ? "" : _Value.ToString()));
                if (readOnly) ((RadComboBox)_Control).Enabled = false;
                continue;
            }

            _Control = container.FindControl("RadDatePicker" + fdInfo[i].Name);
            if (_Control != null) //给RadDatePicker赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);

                if (_Value == null)
                {
                    ((RadDatePicker)_Control).SelectedDate = null;
                }
                else
                {
                    DateTime result;
                    ((RadDatePicker)_Control).SelectedDate = DateTime.TryParse(_Value.ToString(), out result)
                        ? result
                        : new DateTime(1900, 01, 01);
                }
                if (readOnly)
                {
                    ((RadDatePicker)_Control).Enabled = false;
                }
            }
        }
    }

    /// <summary>
    ///     将对象类_Object属性值赋给编辑面板面板中对应的控件
    /// </summary>
    /// <param name="container">控件容器</param>
    /// <param name="_Object">数据实体类</param>
    /// <param name="readOnly">是否将控件设置为只读</param>
    public static void SetData(HtmlControl container, object _Object, bool readOnly)
    {
        if (_Object == null) return;
        Type typeCls = _Object.GetType();
        PropertyInfo[] fdInfo = typeCls.GetProperties(); //反射类的属性

        foreach (PropertyInfo t in fdInfo)
        {
            Control control = container.FindControl("RadTextBox" + t.Name);
            object value;
            if (control != null) //给RadTextBox赋值
            {
                value = t.GetValue(_Object, null);
                ((RadTextBox)control).Text = (value == null ? "" : FormatDecimal(value.ToString()));
                if (readOnly)
                {
                    ((RadTextBox)control).ReadOnly = true;
                    ((RadTextBox)control).Style.Add("border", "none");
                    ((RadTextBox)control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }
            //control = container.FindControl("Label" + t.Name);
            //if (control != null) //给Label赋值
            //{
            //    value = t.GetValue(_Object, null);
            //    ((Label)control).Text = (value == null ? "" : FormatDecimal(value.ToString()));
            //    //if (readOnly)
            //    //{
            //    //    ((TextBox)control).ReadOnly = true;
            //    //    ((TextBox)control).Style.Add("border", "none");
            //    //    ((TextBox)control).Style.Add("cursor", "not-allowed");
            //    //}
            //    continue;
            //}

            control = container.FindControl("TextBox" + t.Name);
            if (control != null) //给TextBox赋值
            {
                value = t.GetValue(_Object, null);
                ((TextBox)control).Text = (value == null ? "" : FormatDecimal(value.ToString()));
                if (readOnly)
                {
                    ((TextBox)control).ReadOnly = true;
                    ((TextBox)control).Style.Add("border", "none");
                    ((TextBox)control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }

            control = container.FindControl("RadNumericTextBox" + t.Name);
            if (control != null) //给TextBox赋值
            {
                value = t.GetValue(_Object, null);
                if (value != null && value.ToString().Trim() != "")
                {
                    ((RadNumericTextBox)control).Text = FormatDecimal(value.ToString());
                }
                else
                {
                    ((RadNumericTextBox)control).Text = "0";
                }
                if (readOnly)
                {
                    ((RadNumericTextBox)control).ReadOnly = true;
                    ((RadNumericTextBox)control).Style.Add("border", "none");
                    ((RadNumericTextBox)control).Style.Add("cursor", "not-allowed");
                }
                continue;
            }

            control = container.FindControl("Label" + t.Name);
            if (control != null) //给Label赋值
            {
                value = t.GetValue(_Object, null);
                if (value != null)
                {
                    if (value.ToString().Contains("1900-01-01") || value.ToString().Contains("1900-1-1"))
                    {
                        //数据库默认时间
                        ((Label)control).Text = "";
                    }
                    else
                    {
                        DateTime result;
                        //时间类型
                        ((Label)control).Text = DateTime.TryParse(value.ToString(), out result)
                            ? result.ToString("yyyy-MM-dd")
                            : (FormatDecimal(value.ToString()));
                    }
                }
                continue;
            }

            control = container.FindControl("RadComboBox" + t.Name);
            if (control != null) //给RadComboBox赋值
            {
                value = t.GetValue(_Object, null);
                SelectDropDownListItemByValue((RadComboBox)control, (value == null ? "" : value.ToString()));
                if (readOnly) ((RadComboBox)control).Enabled = false;
                continue;
            }

            control = container.FindControl("RadDatePicker" + t.Name);
            if (control != null) //给RadDatePicker赋值
            {
                value = t.GetValue(_Object, null);

                if (value != null && value.ToString() != "")
                {
                    if (Convert.ToDateTime(value) != new DateTime(1900, 1, 1))
                    {
                        ((RadDatePicker)control).SelectedDate = Convert.ToDateTime(value);
                    }
                }
                if (readOnly)
                {
                    ((RadDatePicker)control).Enabled = false;
                }
            }
        }
    }

    /// <summary>
    ///     将输入的数据（UpdatePanel面板中输入控件RadTextBox、RadComboBox、RadDatePicker等的值）赋为要修改的对象类_Object
    /// </summary>
    /// <param name="container"></param>
    /// <param name="_Object">对象实体类</param>
    public static void GetData(GridEditableItem container, object _Object)
    {
        Type type_cls = _Object.GetType();
        PropertyInfo[] fdInfo = type_cls.GetProperties(); //反射类的属性

        Control _Control = null;
        string _Text = "";
        object _Value;
        for (int i = 0; i < fdInfo.Length; i++)
        {
            _Control = container.FindControl("RadTextBox" + fdInfo[i].Name); //文本编辑框
            if (_Control != null)
            {
                _Text = ((RadTextBox)_Control).Text;
            }
            else
            {
                _Control = container.FindControl("RadNumericTextBox" + fdInfo[i].Name); //数值编辑框
                if (_Control != null)
                {
                    _Text = ((RadNumericTextBox)_Control).Text;
                }
                else
                {
                    _Control = container.FindControl("TextBox" + fdInfo[i].Name); //文本编辑框
                    if (_Control != null)
                    {
                        _Text = ((TextBox)_Control).Text;
                    }
                    else
                    {
                        _Control = container.FindControl("RadComboBox" + fdInfo[i].Name); //下拉列表
                        if (_Control != null)
                        {
                            _Text = ((RadComboBox)_Control).SelectedValue;
                        }
                        else
                        {
                            _Control = container.FindControl("RadDatePicker" + fdInfo[i].Name); //下拉列表
                            if (_Control != null)
                            {
                                _Text = ((RadDatePicker)_Control).SelectedDate.ToString();
                            }
                        }
                    }
                }
            }

            if (_Control == null) continue;
            if (_Text == "")
            {
                _Value = null;
            }
            else
            {
                if (fdInfo[i].PropertyType.GetGenericArguments().Length > 0) //Nullable
                {
                    _Value = Convert.ChangeType(_Text, fdInfo[i].PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    if (fdInfo[i].PropertyType.ToString() == "System.Guid")
                        _Value = new Guid(_Text);
                    else
                        _Value = Convert.ChangeType(_Text, fdInfo[i].PropertyType);
                }
            }
            fdInfo[i].SetValue(_Object, _Value, null); //替换类中的值
        }
    }

    /// <summary>
    ///     将输入的数据（编辑面板中RadTextBox、RadComboBox、RadDatePicker的值）赋为要修改的对象类_Object
    /// </summary>
    /// <param name="_Object">对象实体类</param>
    public static void GetData(HtmlControl container, object _Object)
    {
        Type type_cls = _Object.GetType();
        PropertyInfo[] fdInfo = type_cls.GetProperties(); //反射类的属性

        Control _Control = null;
        string _Text = "";
        object _Value;
        for (int i = 0; i < fdInfo.Length; i++)
        {
            _Control = container.FindControl("RadTextBox" + fdInfo[i].Name); //文本编辑框
            if (_Control != null)
            {
                _Text = ((RadTextBox)_Control).Text;
            }
            else
            {
                _Control = container.FindControl("TextBox" + fdInfo[i].Name); //文本编辑框
                if (_Control != null)
                {
                    _Text = ((TextBox)_Control).Text;
                }
                else
                {
                    _Control = container.FindControl("RadNumericTextBox" + fdInfo[i].Name);
                    if (_Control != null)
                    {
                        _Text = ((RadNumericTextBox)_Control).Value.ToString();
                    }
                    else
                    {
                        _Control = container.FindControl("RadComboBox" + fdInfo[i].Name); //下拉列表
                        if (_Control != null)
                        {
                            _Text = ((RadComboBox)_Control).SelectedValue;
                        }
                        else
                        {
                            _Control = container.FindControl("RadDatePicker" + fdInfo[i].Name); //下拉列表
                            if (_Control != null)
                            {
                                _Text = ((RadDatePicker)_Control).SelectedDate.ToString();
                            }
                            else
                            {
                                _Control = container.FindControl("Label" + fdInfo[i].Name); //Label
                                if (_Control != null)
                                {
                                    _Text = ((Label)_Control).Text;
                                }
                            }
                        }
                    }
                }
            }

            if (_Control == null) continue;
            if (_Text == "")
            {
                _Value = null;
            }
            else
            {
                if (fdInfo[i].PropertyType.GetGenericArguments().Length > 0) //Nullable
                {
                    _Value = Convert.ChangeType(_Text, fdInfo[i].PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    if (fdInfo[i].PropertyType.ToString() == "System.Guid")
                        _Value = new Guid(_Text);
                    else
                        _Value = Convert.ChangeType(_Text, fdInfo[i].PropertyType);
                }
            }
            fdInfo[i].SetValue(_Object, _Value, null); //替换类中的值
        }
    }

    /// <summary>
    /// 设置边框为红色
    /// </summary>
    /// <param name="ctl">控件</param>
    public static void SetBorderRed(RadTextBox ctl)
    {
        ctl.Style.Add("border-style", "solid");
        ctl.Style.Add("border-width", "1px");
        ctl.Style.Add("border-color", "red");
    }

    #endregion

    #region 弹出窗口

    /// <summary>
    ///     记录错误日志
    /// </summary>
    /// <param name="page"></param>
    /// <param name="errorDesc">错误描述</param>
    /// <param name="ex">错误异常</param>
    public static void WriteErrorLog(Page page, string errorDesc, Exception ex)
    {
        FileLog.WriteLog(errorDesc, ex);

        layerAlert(page, "系统忙，请稍后再试！", 5, 0);
        //Alert(page, "Alert", "系统忙，请稍后再试！");
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    /// <param name="personName">操作者</param>
    /// <param name="personID">操作者ID</param>
    /// <param name="operateName">操作名称</param>
    /// <param name="logDetail">详细说明</param>
    public static void WriteOperateLog(string UserName, string UserID, string operateName, string logDetail)
    {
        OperateLogOB o = new OperateLogOB();
        o.PersonName = UserName;
        o.PersonID = UserID;
        o.LogTime = DateTime.Now;
        o.OperateName = operateName;
        try
        {
            o.LogDetail = string.Format("【{0}】{1}", GetIPAddress(), logDetail);
            OperateLogDAL.Insert(o);
        }
        catch (Exception ex)
        {
            Utility.FileLog.WriteLog(string.Format("记录操作日志“{0}”失败！", operateName), ex);
        }

    }

    ///// <summary>
    ///// 记录操作日志
    ///// </summary>
    ///// <param name="personName">操作者</param>
    ///// <param name="personID">操作者ID</param>
    ///// <param name="operateName">操作名称</param>
    ///// <param name="logDetail">详细说明</param>
    //public static void WriteOperateLog(string UserName, long personID, string operateName, string logDetail)
    //{
    //    OperateLogOB o = new OperateLogOB();
    //    o.PersonName = UserName;
    //    o.PersonID = personID.ToString();
    //    o.LogTime = DateTime.Now;
    //    o.OperateName = operateName;
    //    o.LogDetail = logDetail;
    //    try
    //    {
    //        OperateLogDAL.Insert(o);
    //    }
    //    catch (Exception ex)
    //    {
    //        Utility.FileLog.WriteLog(string.Format("记录操作日志“{0}”失败！", operateName), ex);
    //    }

    //}

    /// <summary>
    ///     应用于updatepanel中的弹出提示
    /// </summary>
    /// <param name="page"></param>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <param name="ifEndResponse"></param>
    public static void ScriptAlert(Page page, string key, string message, bool ifEndResponse)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), key, message, ifEndResponse);
    }

    /// <summary>
    ///     输出客户端提示
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void Alert(Page page, string message)
    {
        Alert(page, "Alert", message, false);
    }

    /// <summary>
    ///     输出客户端提示
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <param name="page"></param>
    public static void Alert(Page page, string key, string message)
    {
        Alert(page, key, message, false);
    }

    /// <summary>
    ///     输出客户端提示
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <param name="page"></param>
    /// <param name="ifEndResponse"></param>
    public static void Alert(Page page, string key, string message, bool ifEndResponse)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), key,
            "window.setTimeout(function(){ radalert(\"<div>" + message +
            "</div>\",270,185, '系统提示');return false;},500);", true);
        if (ifEndResponse) page.Response.End();

    }

    /// <summary>
    /// 提示并执行脚本
    /// </summary>
    /// <param name="exeScript">提示后执行的脚本</param>
    /// <param name="page">页引用</param>
    /// <param name="message">提示信息</param>        
    public static void ParentAlert(Page page, string message, bool IsRefresh)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", string.Format(@"hideIfam();parent.OpenAlert('{0}');window.setTimeout(function(){{{1}}},1000);", message, IsRefresh == true ? "parent.refreshGrid();" : "hideIfam();"), true);
    }

    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="message">提示信息</param>
    public static void layerAlert(Page page, string message)
    {
        layerAlert(page, message, 0, 0);
    }

    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="message">提示信息（特别注意：字符串只能写成一行，不可以有硬回车，否则无法显示提示框）</param>
    /// <param name="ico">图标：0:叹号,1:对勾,2：叉叉，3：问号，4：锁头，5：哭脸，6：笑脸</param>
    /// <param name="CloseTime">弹框自动关闭时间，毫秒。1000=1秒,0表示不自动关闭</param>
    public static void layerAlert(Page page, string message, int ico, int CloseTime)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
            string.Format(@"layer.alert('{0}',{{offset:'150px',icon:{1},time:{2},area: ['600px', 'auto']}});", message, ico, CloseTime), true);

    }


    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="message">提示信息</param>
    /// <param name="exeScript">弹出提示后执行脚本</param>
    public static void layerAlert(Page page, string message, string exeScript)
    {
        layerAlert(page, message, 0, 0, exeScript);
    }

    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="message">提示信息</param>
    /// <param name="ico">图标：0:叹号,1:对勾,2：叉叉，3：问号，4：锁头，5：哭脸，6：笑脸</param>
    /// <param name="CloseTime">弹框自动关闭时间，毫秒。1000=1秒,0表示不自动关闭</param>
    /// <param name="exeScript">弹出提示后执行脚本</param>
    //public static void layerAlert(Page page, string message, int ico, int CloseTime,string exeScript)
    //{
    //    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
    //        string.Format(@"layer.alert('{0}',{{offset:'150px',icon:{1},time:{2},area: ['600px', 'auto']}});{3}", message, ico, CloseTime, exeScript), true);

    //}
    public static void layerAlert(Page page, string message, int ico, int CloseTime, string exeScript)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
            string.Format(@"layer.alert('{0}',{{offset:'150px',icon:{1},time:{2},area: ['600px', 'auto'],yes:function(index){{{3}layer.close(index);}}}});", message, ico, CloseTime, exeScript), true);

    }


    /// <summary>
    /// 弹出提示（message可包含html标签）
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="message">提示信息</param>
    public static void layerAlertWithHtml(Page page, string message)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
           string.Format("layer.open( {{title:'系统提示',content:'{0}',offset:'150px',area: ['600px', 'auto']}});", message.Replace(string.Format("{0}", (char)10), "<br/>").Replace(string.Format("{0}", (char)13), "")), true);
    }

    public static void layerTips(Page page, string message)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "layerTips",
           string.Format("layer.msg( '{0}');", message), true);

    }

    /// <summary>
    /// 填出窗口显示处理结果及后续可用操作链接
    /// </summary>
    /// <param name="page"><引用页面/param>
    /// <param name="message">显示友好提示信息</param>
    /// <param name="listUrl">后续处理链接集合</param>
    public static void ShowMsgAndRedirect(System.Web.UI.Page page, string message, List<ResultUrl> listUrl)
    {
        System.Text.StringBuilder resultMessage = new System.Text.StringBuilder();
        resultMessage.Append("<table style=\"font-size:14px; font-weight:bold; color:blue; margin:20px 40px 40px 40px; \"><tr><td style=\"padding-bottom:10px; color:black;font-weight:normal; \"><nobr>").Append(message).Append("</nobr></td></tr>");
        System.IO.FileInfo fi;
        long size = 0;
        foreach (ResultUrl r in listUrl)
        {
            fi = new System.IO.FileInfo(page.Server.MapPath(r.Url));
            size = fi.Length / 1024;
            if (size < 1024)
                resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", BasePage.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append(size.ToString()).Append(" KB）</nobr></a></td></tr>");
            else
                resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", BasePage.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append((size / 1024).ToString()).Append(" MB）</nobr></a></td></tr>");
        }
        resultMessage.Append("</table>");
        page.Session["resultMessage"] = resultMessage.ToString();
        RadScriptManager.RegisterStartupScript(page, page.GetType(), "openWindow", string.Format("window.setTimeout(function(){{return radopen(\"{0}/ResultInfoPage.aspx?{1}\", \"RadWindow1\").maximize();}},500);", BasePage.RootUrl, Guid.NewGuid()), true);
    }

    /// <summary>
    /// 移动弹出层EditTable
    /// </summary>
    /// <param name="gItem">容器</param>
    /// <param name="left">左边距</param>
    /// <param name="top">右边距</param>
    public static void MovePopEditTable(GridEditableItem gItem, int left, int top)
    {
        (gItem.FindControl("ctl00") as System.Web.UI.WebControls.WebControl).Style.Add("top", top.ToString() + "px");
        (gItem.FindControl("ctl00") as System.Web.UI.WebControls.WebControl).Style.Add("left", left.ToString() + "px");
    }
    #endregion

    #region 附件操作

    /// <summary>
    /// 上传附件
    /// </summary>
    /// <param name="page">页面引用</param>
    /// <param name="fu">上传控件</param>
    /// <param name="dataType">数据类型</param>
    /// <param name="ShowName">附件显示名称</param>
    /// <param name="uploadMan">上传人</param>
    /// <returns>附件ID</returns>
    public static string UploadFile(Page page, FileUpload fu, string dataType, string ShowName, string uploadMan)
    {
        if (fu.HasFile == false) return "";

        //检查保存目录
        if (System.IO.Directory.Exists(page.Server.MapPath(string.Format("~/Upload/{0}", dataType))) == false)
        {
            System.IO.Directory.CreateDirectory(page.Server.MapPath(string.Format("~/Upload/{0}", dataType)));
        }

        string fileID = Guid.NewGuid().ToString();
        //指定文件路径
        string filepath = string.Format("~/Upload/{0}/{1}{2}", dataType, fileID, System.IO.Path.GetExtension(fu.FileName).ToLower());

        try
        {
            fu.SaveAs(page.Server.MapPath(filepath));
            FileInfoMDL f = new FileInfoMDL();
            f.FileID = fileID;
            f.FileName = ShowName;
            f.FileSize = fu.FileContent.Length;
            f.DataType = dataType;
            f.FileType = System.IO.Path.GetExtension(fu.FileName).ToLower();
            f.FileUrl = filepath;
            f.UploadMan = uploadMan;
            f.AddTime = DateTime.Now;
            FileInfoDAL.Insert(f);

        }
        catch (Exception ex)
        {
            UIHelp.WriteErrorLog(page, "文件上传失败！", ex);
        }
        return fileID;
    }

    #endregion

    #region 网页抓取

    /// <summary>
    /// 根据Url地址得到网页的html源码 
    /// </summary>
    /// <param name="Url">网页地址</param>
    /// <returns>html</returns>
    public static string GetWebContent(string Url)
    {
        string strResult = "";
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //声明一个HttpWebRequest请求 
            request.Timeout = 30000;
            //设置连接超时时间 
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("GB2312");
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            strResult = streamReader.ReadToEnd();
        }
        catch
        {

        }
        return strResult;
    }

    /// <summary>
    /// 根据Url地址得到网页的html源码 
    /// </summary>
    /// <param name="Url">网页地址</param>
    /// <returns>html</returns>
    public static string GetWebContent(string Url, Encoding encoding)
    {
        string strResult = "";
        try
        {
            //Utility.FileLog.WriteLog(DateTime.Now.ToString("HH:mm:ss") +"开始获取网页内容！", null);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //声明一个HttpWebRequest请求 
            request.Timeout = 30000;
            //设置连接超时时间 
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            strResult = streamReader.ReadToEnd();

            //Utility.FileLog.WriteLog(DateTime.Now.ToString("HH:mm:ss") + "结束获取网页内容！", null);
            //Utility.FileLog.WriteLog("网页内容：" + strResult, null);
        }
        catch (Exception ex)
        {
            Utility.FileLog.WriteLog("获取网页内容失败！", ex);
        }
        return strResult;
    }

    /// <summary>
    /// 判断网络连接（文件）是否存在
    /// </summary>
    /// <param name="uri">地址</param>
    /// <returns>存在返回true，不存在返回false</returns>
    public static bool IfUriExist(string uri)
    {
        HttpWebRequest req = null;
        HttpWebResponse res = null;
        try
        {
            req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "HEAD";
            req.Timeout = 300;
            res = (HttpWebResponse)req.GetResponse();
            return (res.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            return false;
        }
        finally
        {
            if (res != null)
            {
                res.Close();
                res = null;
            }
            if (req != null)
            {
                req.Abort();
                req = null;
            }
        }
    }

    /// <summary>
    /// 判断是否选择复选框（当前页）
    /// </summary>
    /// <param name="grid">列表控件</param>
    /// <returns></returns>
    public static bool IsSelected(RadGrid grid)
    {
        for (int i = 0; i <= grid.Items.Count - 1; i++)
        {
            if (grid.Items[i].ItemType == Telerik.Web.UI.GridItemType.Item || grid.Items[i].ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.CheckBox cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                if (cbox.Checked)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion

    #region 区县处理

    /// <summary>
    /// 根据地区ID返回地区名称
    /// </summary>
    /// <param name="regionId">区县ID</param>
    /// <returns>地区名称</returns>
    public static string GetRegionName(String regionId)
    {
        string sql = string.Format("SELECT [RegionName] FROM [Dict_Region] WHERE [RegionCode] ='{0}' ", regionId);
        DataTable dt = (new DBHelper()).GetFillData(sql);
        if (dt == null || dt.Rows.Count == 0)
            return "";
        else
            return dt.Rows[0][0].ToString();
    }



    /// <summary>
    /// 设置选择项
    /// </summary>
    /// <param name="cmbRegion"></param>
    /// <param name="list">value值List</param>
    /// <param name="checkBoxName">CheckBox 控件ID</param>
    /// <param name="hiddenFieldName">HiddenField 控件ID</param>
    public static void SetCheckedRegion(RadComboBox cmbRegion, List<string> list, string checkBoxName, string hiddenFieldName)
    {
        foreach (var valueStr in list)
        {
            foreach (RadComboBoxItem item in cmbRegion.Items)
            {
                var chk = (CheckBox)item.FindControl(checkBoxName);
                var hid = (HiddenField)item.FindControl(hiddenFieldName);
                if (item.Value.Equals(valueStr))
                {
                    chk.Checked = true;
                    hid.Value = valueStr;
                }
                else
                {
                    chk.Checked = false;
                    hid.Value = "";
                }
            }
        }
    }

    /// <summary>
    ///     获取选择项
    /// </summary>
    /// <param name="cmbRegion"></param>
    /// <returns></returns>
    public static string GetCheckedRegion(RadComboBox cmbRegion)
    {
        string strCompanyRegion = "";
        foreach (RadComboBoxItem item in cmbRegion.Items)
        {
            var chk = (CheckBox)item.FindControl("chkRegion");
            if (chk.Checked)
            {
                var hid = (HiddenField)item.FindControl("hidRegionID");

                if (String.IsNullOrEmpty(strCompanyRegion))
                {
                    strCompanyRegion = hid.Value;
                }
                else
                {
                    strCompanyRegion += "," + hid.Value;
                }
            }
        }

        if (String.IsNullOrEmpty(strCompanyRegion)) // 返回空
        {
            strCompanyRegion = "''";
        }
        else
        {
            strCompanyRegion += ""; // 返回多个隔离的Region
        }

        return strCompanyRegion;
    }

    /// <summary>
    /// 获取选择项
    /// </summary>
    /// <param name="cmbRegion"></param>
    /// <param name="checkBoxName">CheckBox 控件ID</param>
    /// <param name="hiddenFieldName">HiddenField 控件ID</param>
    /// <returns></returns>
    public static string GetCheckedRegion(RadComboBox cmbRegion, string checkBoxName, string hiddenFieldName)
    {
        string strCompanyRegion = "";
        foreach (RadComboBoxItem item in cmbRegion.Items)
        {
            var chk = (CheckBox)item.FindControl(checkBoxName);
            if (chk.Checked)
            {
                var hid = (HiddenField)item.FindControl(hiddenFieldName);

                if (String.IsNullOrEmpty(strCompanyRegion))
                {
                    strCompanyRegion = hid.Value;
                }
                else
                {
                    strCompanyRegion += "," + hid.Value;
                }
            }
        }

        if (String.IsNullOrEmpty(strCompanyRegion)) // 返回空
        {
            strCompanyRegion = "''";
        }
        else
        {
            strCompanyRegion += ""; // 返回多个隔离的Region
        }

        return strCompanyRegion;
    }

    #endregion 区县处理

    #region 数据格式化处理

    /// <summary>
    ///     格式化dicimal，去掉不必要的小数或0
    /// </summary>
    /// <param name="data">要格式化数据</param>
    /// <returns>格式化结果</returns>
    public static string FormatDecimal(string data)
    {
        decimal d;
        if (Decimal.TryParse(data, out d) && data.LastIndexOf('.') != -1)
        {
            string rtn = data.Substring(data.LastIndexOf('.')).TrimEnd('0').TrimEnd('.');
            if (rtn == "") //带小数0保留一位，如 0.0
                return data.Substring(0, data.LastIndexOf('.') + 1) + "0";
            return data.Substring(0, data.LastIndexOf('.')) + rtn;
        }
        return data;
    }

    /// <summary>
    ///     截取字符串（length：中文算2个）
    /// </summary>
    /// <param name="text">字符串</param>
    /// <param name="length">截取长度，中文算2个</param>
    /// <returns>截取后字符串</returns>
    public static string GetStrSub(string text, int length)
    {
        int strLength = 0;
        char[] Temp = text.ToCharArray();
        for (int i = 0; i != Temp.Length; i++)
        {
            if (Temp[i] < 255)
            {
                strLength++;
            }
            else
            {
                strLength = strLength + 2;
            }
            if (strLength > length) return text.Substring(0, i) + "...";
        }

        return text;
    }

    /// <summary>
    ///     将对象类_Object属性值与编辑面板面板中对应的控件值比较，不同标红
    /// </summary>
    /// <param name="container">控件容器</param>
    /// <param name="_Object">数据实体类</param>
    /// <param name="showColor"></param>
    public static void CompareData(HtmlControl container, object _Object, Color showColor)
    {
        if (_Object == null) return;
        Type type_cls = _Object.GetType();
        PropertyInfo[] fdInfo = type_cls.GetProperties(); //反射类的属性

        Control _Control = null;
        object _Value;
        for (int i = 0; i < fdInfo.Length; i++)
        {
            _Control = container.FindControl("Label" + fdInfo[i].Name);
            if (_Control != null) //给Label赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);

                if (_Value.GetType().Name == "Decimal")
                {
                    if (Convert.ToDecimal(((Label)_Control).Text) !=
                        (_Value == null ? 0 : Convert.ToDecimal(_Value.ToString())))
                    {
                        ((Label)_Control).ForeColor = showColor;
                    }
                    else
                    {
                        ((Label)_Control).ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (((Label)_Control).Text != (_Value == null ? "" : FormatDecimal(_Value.ToString())))
                    {
                        ((Label)_Control).ForeColor = showColor;
                    }
                    else
                    {
                        ((Label)_Control).ForeColor = Color.Black;
                    }
                }
            }
        }
    }

    /// <summary>
    ///     判断页面数据是否与实体类一致
    /// </summary>
    /// <param name="container">编辑面板容器控件</param>
    /// <param name="_Object">比较实体对象类</param>
    /// <returns>一致返回true，否则返回false</returns>
    public static bool CompareData(HtmlControl container, object _Object)
    {
        if (_Object == null) return false;
        Type type_cls = _Object.GetType();
        PropertyInfo[] fdInfo = type_cls.GetProperties(); //反射类的属性

        Control _Control = null;
        object _Value;
        for (int i = 0; i < fdInfo.Length; i++)
        {
            _Control = container.FindControl("Label" + fdInfo[i].Name);
            if (_Control != null) //给Label赋值
            {
                _Value = fdInfo[i].GetValue(_Object, null);

                if (((Label)_Control).Text !=
                    (_Value == null ? "" : FormatDataTime(FormatDecimal(_Value.ToString()), "yyyy-MM-dd")))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    ///     格式化datetime，yyyy-MM-dd
    /// </summary>
    /// <param name="data">要格式化数据</param>
    /// <param name="format"></param>
    /// <returns>格式化结果</returns>
    public static string FormatDataTime(string data, string format)
    {
        DateTime result;
        if (DateTime.TryParse(data, out result))
        {
            if (!string.IsNullOrEmpty(format))
            {
                try
                {
                    return result.ToString(format);
                }
                catch
                {
                    return data;
                }
            }
            return data;
        }
        return data;
    }

    /// <summary>
    ///     获取Double 类型数据
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double GetDouble(object obj)
    {
        if (obj == null)
        {
            return 0;
        }
        double value;
        double.TryParse(obj.ToString(), out value);
        return value;
    }

    /// <summary>
    /// 格式化英文括号为中文括号
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string FormatKuoHao(string format)
    {
        return format.Replace("(", "（").Replace(")", "）");
    }

    #endregion 数据格式化处理

    #region 一寸免冠照片处理

    /// <summary>
    /// 判断是否为白底一寸照片
    /// </summary>
    /// <param name="imgPath">照片绝对地址</param>
    /// <returns>白底返回true，否则返回false</returns>
    public static bool CheckIfWhiteBackgroudPhoto(string imgPath)
    {
        System.Drawing.Color c;
        int pointCount = 0;//统计读取像素点总数
        int whitePointCount = 0;//统计白色像素点总数
        int whiteBorder = 210;//白色临界值（大于该值视为白色）

        using (System.Drawing.Bitmap map = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(imgPath))
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < (map.Height / 2); j++)
                {
                    c = map.GetPixel(i, j);
                    pointCount++;
                    if (c.R > whiteBorder && c.G > whiteBorder && c.B > whiteBorder)
                    {
                        whitePointCount++;
                    }
                }
            }
        }
        if ((whitePointCount * 100 / pointCount) > 25)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获取个人一寸免冠照片
    /// </summary>
    /// <param name="PSN_RegisterNO">注册证书编号</param>
    /// <param name="WorkerCertificateCode">证件号码</param>
    /// <returns></returns>
    public static string ShowFaceImageJZS(string PSN_RegisterNO, string WorkerCertificateCode)
    {
        string imgPath = "";
        string FacePhoto = COC_TOW_Person_FileDAL.GetFileUrl(PSN_RegisterNO, EnumManager.FileDataTypeName.一寸免冠照片);
        if (string.IsNullOrEmpty(FacePhoto) == false)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(FacePhoto.Replace("~", ".."))) == true)
            {
                imgPath = FacePhoto.Replace("~", "..");
                return imgPath;
            }
        }

        imgPath = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == true)
        {
            return imgPath;
        }
        else
        {
            imgPath = "~/Images/tup.gif";
        }
        return imgPath;

        //System.Random rm = new Random();
        //string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), imgPath); //绑定照片;
        //return img;
    }

    /// <summary>
    /// 获取个人手写签名照
    /// </summary>
    /// <param name="PSN_RegisterNO">注册证书编号</param>
    /// <param name="WorkerCertificateCode">证件号码</param>
    /// <returns></returns>
    public static string ShowSignImageJZS(string PSN_RegisterNO, string WorkerCertificateCode)
    {
        string imgPath = "";
        string SignPhoto = COC_TOW_Person_FileDAL.GetFileUrl(PSN_RegisterNO, EnumManager.FileDataTypeName.手写签名照);
        if (string.IsNullOrEmpty(SignPhoto) == false)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(SignPhoto.Replace("~", ".."))) == true)
            {
                imgPath = SignPhoto.Replace("~", "..");
                return imgPath;
            }
        }

        imgPath = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == true)
        {
            return imgPath;
        }
        else
        {
            imgPath = "~/Images/SignImg.jpg";
        }
        return imgPath;

        //System.Random rm = new Random();
        //string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), imgPath); //绑定照片;
        //return img;
    }

    /// <summary>
    /// 获取个人一寸免冠照片
    /// </summary>
    /// <param name="WorkerCertificateCode">证件号码</param>
    /// <returns></returns>
    public static string ShowFaceImage(string WorkerCertificateCode)
    {
        string imgPath = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgPath)) == true)
        {
            return imgPath;
        }
        else
        {
            imgPath = "~/Images/tup.gif";
        }
        return imgPath;

        //System.Random rm = new Random();
        //string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), imgPath); //绑定照片;
        //return img;
    }

    /// <summary>
    /// 获取证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
    /// </summary>
    /// <param name="FacePhoto">照片路径</param>
    /// <param name="WorkerCertificateCode">证件号码</param>
    /// <returns></returns>
    public static string ShowFaceImage(string FacePhoto, string WorkerCertificateCode)
    {
        string imgPath = "";
        if (string.IsNullOrEmpty(FacePhoto) == false)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(FacePhoto)) == true)
            {
                imgPath = FacePhoto;//.Replace("~","..");
            }
        }
        if (imgPath == "" && string.IsNullOrEmpty(WorkerCertificateCode) == false)
        {
            if (WorkerCertificateCode.Length > 2 && WorkerCertificateCode.IndexOf('?') == -1)
            {
                string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
                if (File.Exists(HttpContext.Current.Server.MapPath(path)) == true)
                {
                    imgPath = path;
                }
            }
        }
        if (imgPath == "")
        {
            imgPath = "~/Images/tup.gif";
        }

        System.Random rm = new Random();
        string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(imgPath)); //绑定照片;
        return img;
    }

    /// <summary>
    /// 获取证书绑定一寸免冠照片，如果没有显示人员目录最新上传照片
    /// </summary>
    /// <param name="FacePhoto">照片路径</param>
    /// <param name="WorkerCertificateCode">证件号码</param>
    /// <returns></returns>
    public static string GetFaceImagePath(string FacePhoto, string WorkerCertificateCode)
    {
        string imgPath = "";
        string serverType = System.Configuration.ConfigurationManager.AppSettings["serverType"];
        if (string.IsNullOrEmpty(FacePhoto) == false)
        {
            if (serverType == "ww")
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(FacePhoto)) == true)
                {
                    imgPath = FacePhoto;//.Replace("~","..");
                }
            }
            else
            {
                imgPath = FacePhoto;
            }

        }
        if (imgPath == "" && string.IsNullOrEmpty(WorkerCertificateCode) == false)
        {
            if (WorkerCertificateCode.Length > 2 && WorkerCertificateCode.IndexOf('?') == -1)
            {
                string path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);

                if (serverType == "ww")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(path)) == true)
                    {
                        imgPath = path;
                    }
                }
                else
                {
                    imgPath = path;
                }
            }
        }
        if (imgPath == "")
        {
            imgPath = "~/Images/tup.gif";
        }

        return imgPath;
    }

    #endregion 一寸免冠照片处理

    #region 手写签名照处理

    /// <summary>
    /// 根据身份证号码获取个人信息设置中上传的手写签名图片
    /// </summary>
    /// <param name="WorkerCertificateCode"></param>
    /// <returns></returns>
    public static string GetSignImgPath(string WorkerCertificateCode)
    {
        if (string.IsNullOrEmpty(WorkerCertificateCode) == true) return "~/Images/SignImg.jpg";

        string path = string.Format("~/UpLoad/SignImg/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode);
        if (File.Exists(HttpContext.Current.Server.MapPath(path)) == true)
        {
            return path;
        }
        else
        {
            return "~/Images/SignImg.jpg";
        }
    }

    #endregion 手写签名照处理

    /// <summary>
    /// 获取附件URL，排除机器
    /// </summary>
    /// <param name="id">申请单ID或建造师ID</param>
    /// <returns></returns>
    public static string ShowFile(string id)
    {

        //服务器类型
        string serverType = System.Configuration.ConfigurationManager.AppSettings["serverType"];

        //外网
        string ww = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ww"]);

        //专网
        string zw = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["zw"]);

        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(id)) == true)
        {
            return serverType == "ww" ? string.Format("{0}/{1}?{2}", ww, id.Substring(2, id.Length - 2), new System.Random().Next()) : string.Format("{0}/{1}?{2}", zw, id.Substring(2, id.Length - 2), new System.Random().Next());
        }
        else
        {
            return serverType == "zw" ? string.Format("{0}/{1}?{2}", ww, id.Substring(2, id.Length - 2), new System.Random().Next()) : string.Format("{0}/{1}?{2}", zw, id.Substring(2, id.Length - 2), new System.Random().Next());
        }
    }

    /// <summary>
    ///     换肤
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    public static string Exchange(string theme)
    {
        string pf = string.Empty;
        switch (theme)
        {
            case "style2":
                pf = "Sunset";
                break;

            case "style3":
                pf = "Hay";
                break;

            case "style4":
                pf = "Outlook";
                break;

            case "style5":
                pf = "Telerik";
                break;
        }
        return pf;
    }

    /// <summary>
    /// 根据证书期限、身份证号计算证书最大有限期
    /// </summary>
    /// <returns></returns>
    public static DateTime GET_PSN_CertificateValidity(DateTime endDate, string PSN_CertificateNO)
    {
        DateTime PSN_CertificateValidity = DateTime.Now;
        DateTime card15;
        DateTime card18;
        if (PSN_CertificateNO.Length == 15)
        {
            card15 = DateTime.Parse("19" + PSN_CertificateNO.Substring(6, 2) + "-" + PSN_CertificateNO.Substring(8, 2) + "-" + PSN_CertificateNO.Substring(10, 2)).AddYears(65);
            if (endDate > card15)
            {
                PSN_CertificateValidity = card15;
            }
            else
            {
                PSN_CertificateValidity = endDate;

            }


        }
        if (PSN_CertificateNO.Length == 18)
        {
            card18 = DateTime.Parse(PSN_CertificateNO.Substring(6, 4) + "-" + PSN_CertificateNO.Substring(10, 2) + "-" + PSN_CertificateNO.Substring(12, 2)).AddYears(65);
            if (endDate > card18)
            {
                PSN_CertificateValidity = card18;
            }
            else
            {
                PSN_CertificateValidity = endDate;

            }

        }

        return PSN_CertificateValidity;
    }

    /// <summary>
    /// 根据有效期、身份证号、注册等级取证书最大有效期
    /// </summary>
    /// <param name="endDate"></param>
    /// <param name="PSN_CertificateNO"></param>
    /// <param name="PSN_Level"></param>
    /// <returns></returns>
    public static DateTime GET_PSN_CertificateValidity(DateTime endDate, string PSN_CertificateNO, string PSN_Level)
    {

        DateTime PSN_CertificateValidity = DateTime.Now;
        DateTime card15;
        DateTime card18;

        #region 15位身份证


        if (PSN_CertificateNO.Length == 15)
        {
            if (PSN_Level.Trim() == "二级")
            {
                card15 = DateTime.Parse("19" + PSN_CertificateNO.Substring(6, 2) + "-" + PSN_CertificateNO.Substring(8, 2) + "-" + PSN_CertificateNO.Substring(10, 2)).AddYears(65);
                if (endDate > card15)
                {
                    PSN_CertificateValidity = card15;
                }
                else
                {
                    PSN_CertificateValidity = endDate;

                }

            }
            if (PSN_Level.Trim() == "二级临时")
            {
                card15 = DateTime.Parse("19" + PSN_CertificateNO.Substring(6, 2) + "-" + PSN_CertificateNO.Substring(8, 2) + "-" + PSN_CertificateNO.Substring(10, 2)).AddYears(60);
                if (endDate > card15)
                {
                    PSN_CertificateValidity = card15;
                }
                else
                {
                    PSN_CertificateValidity = endDate;

                }
            }

        }


        #endregion

        #region 18位身份证号


        if (PSN_CertificateNO.Length == 18)
        {
            if (PSN_Level.Trim() == "二级")
            {

                card18 = DateTime.Parse(PSN_CertificateNO.Substring(6, 4) + "-" + PSN_CertificateNO.Substring(10, 2) + "-" + PSN_CertificateNO.Substring(12, 2)).AddYears(65);

                if (endDate > card18)
                {
                    PSN_CertificateValidity = card18;
                }
                else
                {
                    PSN_CertificateValidity = endDate;

                }
            }
            if (PSN_Level.Trim() == "二级临时")
            {
                card18 = DateTime.Parse(PSN_CertificateNO.Substring(6, 4) + "-" + PSN_CertificateNO.Substring(10, 2) + "-" + PSN_CertificateNO.Substring(12, 2)).AddYears(60);
                if (endDate > card18)
                {
                    PSN_CertificateValidity = card18;
                }
                else
                {
                    PSN_CertificateValidity = endDate;

                }
            }

        }

        return PSN_CertificateValidity;
        #endregion
    }

    # region ------从考务系统迁移过来的服务方法-------

    /// <summary>
    /// 设置GridView引用列
    /// </summary>
    /// <param name="grid">GridView</param>
    /// <param name="refOB">映射设置对象</param>
    public static void SetRefColumn(RadGrid grid, RefColumnOB refOB)
    {
        string[] refSet;
        string oldText = "";
        //枚举引用映射            
        foreach (string m in refOB.RefMenuType)
        {
            refSet = m.Split(',');
            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "Types", "TypeName,TypeValue", "TypeID='" + refSet[1] + "'", "SortID");
            //设置表的主键
            DataColumn[] dcKeys = new DataColumn[1];
            dcKeys[0] = dt.Columns["TypeValue"];
            dt.PrimaryKey = dcKeys;
            for (int i = 0; i < grid.Items.Count; i++)
            {
                oldText = grid.Items[i][refSet[0]].Text;
                DataRow dr = dt.Rows.Find(oldText);
                if (dr != null) grid.Items[i][refSet[0]].Text = dr["TypeName"].ToString();
            }
        }
        //外键引用映射 
        DBHelper db = new DBHelper();
        object newText;
        string sqlFormat = "";
        foreach (string m in refOB.RefTable)
        {
            refSet = m.Split(',');
            for (int i = 0; i < grid.Items.Count; i++)
            {
                oldText = grid.Items[i][refSet[0]].Text;
                if (refSet[4] == "true")//数值类型
                    sqlFormat = string.Format("SELECT {0} FROM {1} WHERE {2}={3}", refSet[2], refSet[1], refSet[3], oldText);
                else
                    sqlFormat = string.Format("SELECT {0} FROM {1} WHERE {2}='{3}'", refSet[2], refSet[1], refSet[3], oldText);

                newText = db.ExecuteScalar(sqlFormat);
                if (newText != null) grid.Items[i][refSet[0]].Text = newText.ToString();
            }
        }
    }

    /// <summary>
    /// 根据岗位类别ID获取名称
    /// </summary>
    /// <param name="id">岗位类别ID</param>
    /// <returns>岗位类别名称</returns>
    public static string GetPostTypeNameByID(string id)
    {
        switch (id)
        {
            case "1":
                return "三类人员";
            case "2":
                return "特种作业";
            case "3":
                return "造价员";
            case "4":
                return "职业技能";
            case "5":
                return "专业管理";
        }
        return "";
    }

    /// <summary>
    /// 检查目录是否存在，不存在创建
    /// </summary>
    /// <param name="page">触发页引用</param>
    /// <param name="path">目录相对路径</param>
    public static void CheckCreateDirectory(System.Web.UI.Page page, string path)
    {
        if (!Directory.Exists(page.Server.MapPath(path))) Directory.CreateDirectory(page.Server.MapPath(path));
    }

    /// <summary>
    /// 验证组织机构代码规则
    /// </summary>
    /// <param name="page"></param>
    /// <param name="unitCode">组织机构代码</param>
    /// <returns></returns>
    public static bool UnitCodeCheck(System.Web.UI.Page page, string unitCode)
    {
        DataTable dt = null;
        if (page.Cache["UnitCodeSet"] == null)
        {
            dt = UnitCodeSetDAL.GetList(0, int.MaxValue - 1, "", "unitcode");//特殊组织机构代码
            DataColumn[] c = new DataColumn[1];
            c[0] = dt.Columns["UnitCode"];
            dt.PrimaryKey = c;
            page.Cache["UnitCodeSet"] = dt;
        }
        else
        {
            dt = (DataTable)page.Cache["UnitCodeSet"];
        }
        if (dt.Rows.Find(unitCode) != null) return true;

        return Utility.Check.CheckUnitCode(unitCode);
    }

    ///// <summary>
    ///// 从基础数据库（本市管理的建筑企业资质库）获取有效组织机构代码
    ///// </summary>
    ///// <returns>有效组织机构代码集合，用英文逗号分隔</returns>
    //public static string QueryUnitFromBaseDB(System.Web.UI.Page page)
    //{
    //    if (page.Cache["UnitCode"] != null)
    //    {
    //        return page.Cache["UnitCode"].ToString();
    //    }
    //    else
    //    {
    //        string jzs = GetFileText(page, "~/Template/本外地施工企业.txt|~/Template/安全生产许可证.txt|~/Template/起重机械拆装企业信息.txt");
    //        page.Cache.Add("UnitCode", jzs, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
    //        return jzs;
    //    }
    //}

    ///// <summary>
    ///// 从基础数据库（本市企业建造师注册证书库）获取有效证件号码（身份证号,注意，15位18位应该通用）
    ///// </summary>
    ///// <returns>身份证号集合，用英文逗号分隔</returns>
    //public static string QueryJZSWorkerCertificateCodeFromBaseDB(System.Web.UI.Page page)
    //{
    //    if (page.Cache["jzs"] != null)
    //    {
    //        return page.Cache["jzs"].ToString();
    //    }
    //    else
    //    {
    //        string jzs = GetFileText(page, "~/Template/注册建造师.txt");
    //        page.Cache.Add("jzs", jzs, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
    //        return jzs;
    //    }
    //}

    /// <summary>
    /// 获取建造师数据（证件号码，组织机构代码）
    /// </summary>
    /// <param name="page"></param>
    /// <param name="qy">区域：可选项（本地，外地，全部）</param>
    /// <returns>建造师数据datatable(zjhm,zzjgdm)</returns>
    public static DataTable GetJZS(System.Web.UI.Page page, string qy)
    {
        if (page.Cache[string.Format("{0}建造师", qy)] != null)
        {
            return page.Cache[string.Format("{0}建造师", qy)] as DataTable;
        }
        else
        {
            DataTable dt = null;
            //switch (qy)
            //{
            //    case "本地":
            //        dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ry_zcjzs", "distinct ZJHM,ZZJGDM", "", "ZJHM");
            //        break;
            //    case "外地":
            //        dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ry_zcjzs_wd", "distinct ZJHM,ZZJGDM", "", "ZJHM");
            //        break;
            //    case "全部":
            //        dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.view_ry_zcjzs", "distinct ZJHM,ZZJGDM", "", "ZJHM");
            //        break;
            //}
            switch (qy)
            {
                case "本地":
                    dt = CommonDAL.GetDataTable("select distinct ZJHM,ZZJGDM from dbo.ry_zcjzs where ZZJGDM is not null");
                    break;
                case "外地":
                    dt = CommonDAL.GetDataTable("select distinct ZJHM,ZZJGDM from dbo.ry_zcjzs_wd where ZZJGDM is not null");
                    break;
                case "全部":
                    dt = CommonDAL.GetDataTable("select distinct ZJHM,ZZJGDM from dbo.view_ry_zcjzs where ZZJGDM is not null");
                    break;
            }
            DataColumn[] dcKeys = new DataColumn[2];
            dcKeys[0] = dt.Columns["ZJHM"];
            dcKeys[1] = dt.Columns["ZZJGDM"];
            dt.PrimaryKey = dcKeys;

            page.Cache.Add(string.Format("{0}建造师", qy), dt, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            return dt;
        }
    }

    /// <summary>
    /// 验证是否存在建造师
    /// </summary>
    /// <param name="qy">区域：可选项（本地，外地，全部）</param>
    /// <param name="zjhm">证件号码</param>
    /// <param name="zzjgdm">组织机构代码</param>
    /// <returns>存在返回true，否则返回false</returns>
    public static bool CheckJZS(string qy, string zjhm, string zzjgdm)
    {
        string zjhm15 = zjhm;
        string zjhm18 = zjhm;

        if (zjhm.Length == 15)
        {
            zjhm18 = Utility.Check.ConvertoIDCard15To18(zjhm15);
        }
        else if (zjhm.Length == 18)
        {
            zjhm15 = zjhm18.Remove(17, 1).Remove(6, 2);

        }

        int countZCJZS = CommonDAL.SelectRowCount("dbo.VIEW_RY_ZCJZS", string.Format(" and (ZJHM='{0}' or ZJHM='{1}') and ZZJGDM like '%{2}%' and QY like '{3}%'", zjhm15, zjhm18, zzjgdm, (qy == "全部" ? "" : qy)));
        if (countZCJZS == 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 报考物业项目负责人时与“在岗无证人员库”进行比对（身份证号）
    /// </summary>
    /// <returns>身份证号集合，用英文逗号分隔</returns>
    public static string QueryWYXMFZRWorkerCertificateCodeFromBaseDB(System.Web.UI.Page page)
    {
        if (page.Cache["zgwz"] != null)
        {
            return page.Cache["zgwz"].ToString();
        }
        else
        {
            DataTable dt = CommonDAL.GetDataTable(@" 
                     SELECT replace(replace(replace(replace(isnull([ZJHM],''),char(10),''),char(9),''),',','，'),'-',''),
                      replace(replace(replace(isnull([XMFZRXM],''),char(10),''),char(9),''),',','，')
                       FROM [192.168.7.56].[ShareDB].[dbo].[RY_ZGWZRY_WY] where valid = 1");
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (DataRow r in dt.Rows)
            {
                sb.Append(string.Format("{0},{1},", r[0], r[1]));
            }

            //string zgwz = GetFileText(page, "~/Template/在岗无证人员.txt");
            page.Cache.Add("zgwz", sb.ToString(), null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            return sb.ToString();
        }
    }

    ///// <summary>
    ///// 获取房地产开发企业资质库：（企业名称，组织机构代码）
    ///// </summary>
    ///// <returns>行结构：企业名称，组织机构代码</returns>
    //public static string QueryFDCKFQYFromBaseDB(System.Web.UI.Page page)
    //{
    //    if (page.Cache["fdckfqy"] != null)
    //    {
    //        return page.Cache["fdckfqy"].ToString();
    //    }
    //    else
    //    {
    //        string zgwz = GetFileText(page, "~/Template/房地产开发企业.txt");
    //        page.Cache.Add("fdckfqy", zgwz, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
    //        return zgwz;
    //    }
    //}

    /// <summary>
    /// 获取本外地物业企业资质库：（企业名称，组织机构代码）
    /// </summary>
    /// <returns>行结构：企业名称，组织机构代码</returns>
    public static string QueryWYQYFromBaseDB(System.Web.UI.Page page)
    {
        if (page.Cache["wyqy"] != null)
        {
            return page.Cache["wyqy"].ToString();
        }
        else
        {
            DataTable dt = CommonDAL.GetDataTable(@" 
                    SELECT replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),char(9),''),',','，'),'-',''),
                     replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，')
                      FROM [192.168.7.56].[ShareDB].[dbo].[QY_ZZZSWY] where valid = 1 
                      union 
                      SELECT replace(replace(replace(replace(isnull([ZZJGDM],''),char(10),''),char(9),''),',','，'),'-',''),
                      replace(replace(replace(isnull([QYMC],''),char(10),''),char(9),''),',','，')
                      FROM [192.168.7.89].[SJZX_BAK].[dbo].[QY_TSQYZZ] 
                      where valid = 1");
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (DataRow r in dt.Rows)
            {
                sb.Append(string.Format("{0},{1},", r[0], r[1]));
            }
            //string zgwz = GetFileText(page, "~/Template/本外地物业企业.txt");
            page.Cache.Add("wyqy", sb.ToString(), null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            return sb.ToString();
        }
    }



    /// <summary>
    /// 根据考试计划名称格式化岗位工种（造价员：安装postid=12，考试计划带暖通的显示“安装（暖通）”，带电气的显示“安装（电气）”）
    /// </summary>
    /// <param name="postID">岗位工种ID</param>
    /// <param name="postName">岗位工种名称</param>
    /// <param name="examplanName">考试计划名称</param>
    /// <returns></returns>
    public static string FormatPostNameByExamplanName(int postID, string postName, string examplanName)
    {
        if (postID == 12)
        {
            if (examplanName.Contains("暖通") == true)
            {
                return string.Format("{0}（暖通）", postName);//工种
            }
            else if (examplanName.Contains("电气") == true)
            {
                return string.Format("{0}（电气）", postName);//工种
            }
        }
        return postName;//工种
    }


    /// <summary>
    /// 创建报名表条码
    /// </summary>
    /// <param name="ExamPlanID">考试计划ID</param>
    /// <param name="ExamSignUpID">报名表ID</param>
    public static void CreateTCode(object ExamPlanID, object ExamSignUpID)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Upload/SignUpTable/{0}/", ExamPlanID))))
        {
            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Upload/SignUpTable/{0}/", ExamPlanID)));
        }

        //图片路径
        string localPath = HttpContext.Current.Server.MapPath(string.Format(@"~/Upload/SignUpTable/{0}/{1}.png", ExamPlanID, ExamSignUpID));

        if (System.IO.File.Exists(localPath) == true)//本地存在,不创建
        {
            return;
        }

        string input = ExamSignUpID.ToString();
        //if (input.Length % 2 == 1)
        //{
        //    input = "0" + input;
        //}

        //Utility.ImageHelp.CreateITFCode(input, 236, 50).Save(localPath, System.Drawing.Imaging.ImageFormat.Png);

        Utility.ImageHelp.CreateITFCode(input, 236, 40).Save(localPath, System.Drawing.Imaging.ImageFormat.Png);
    }

    /// <summary>
    /// 创建条码
    /// </summary>
    /// <param name="path">存放条码目录路径：例如~/Upload/SignUpTable/{0}/</param>
    /// <param name="code">条码内容</param>
    public static void CreateITFCode(string path, object code)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
        }
        //图片路径
        string localPath = HttpContext.Current.Server.MapPath(string.Format(@"{0}{1}.png", path, code));

        if (System.IO.File.Exists(localPath) == true)//本地存在,不创建
        {
            return;
        }

        Utility.ImageHelp.CreateITFCode(code.ToString(), 236, 40).Save(localPath, System.Drawing.Imaging.ImageFormat.Png);
    }

    /// <summary>
    /// 获取下一个批次号
    /// </summary>
    /// <param name="TypeID">业务类型</param>
    /// <returns></returns>
    public static string GetNextBatchNumber(string TypeID)
    {
        DBHelper db = new DBHelper();
        DbTransaction tran = db.BeginTransaction();
        object rtn = null; ;
        try
        {
            rtn = db.ExecuteScalar(tran, string.Format("SELECT RIGHT('0000000' + cast(ISNULL(CurrentNumber,0) +1 as varchar(8)),8) FROM dbo.BatchNumber WHERE TypeID = '{0}';", TypeID));
            if (rtn != null) db.ExecuteScalar(tran, string.Format("UPDATE dbo.BatchNumber SET CurrentNumber={0} WHERE TypeID = '{1}'", Convert.ToInt64(rtn).ToString(), TypeID));
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
        }
        if (rtn != null)
            return TypeID + rtn.ToString();
        else
            throw new Exception("生成批次号失败！");
    }

    /// <summary>
    /// 获取下一个批次号
    /// </summary>
    /// <param name="tran">事务</param>
    /// <param name="TypeID">业务类型</param>
    /// <returns></returns>
    /// <summary>
    public static string GetNextBatchNumber(DbTransaction tran, string TypeID)
    {
        DBHelper db = new DBHelper();
        object rtn = null; ;

        rtn = db.ExecuteScalar(tran, string.Format("SELECT RIGHT('0000000' + cast(ISNULL(CurrentNumber,0) +1 as varchar(8)),8) FROM dbo.BatchNumber WHERE TypeID = '{0}';", TypeID));
        if (rtn != null) db.ExecuteScalar(tran, string.Format("UPDATE dbo.BatchNumber SET CurrentNumber={0} WHERE TypeID = '{1}'", Convert.ToInt64(rtn).ToString(), TypeID));

        if (rtn != null)
            return TypeID + rtn.ToString();
        else
            throw new Exception("生成批次号失败！");
    }
    # endregion 业应用务

    #region SQL注入检查

    //Sql注入时,可能出现的sql关键字,可根据自己的实际情况进行初始化,每个关键字由'|'分隔开来
    //private const string StrKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and";
    private const string StrKeyWord = @"select|insert|delete|from|drop table|update|truncate|exec master|netlocalgroup administrators|:|net user|or|and";
    //Sql注入时,可能出现的特殊符号,,可根据自己的实际情况进行初始化,每个符号由'|'分隔开来
    //private const string StrRegex = @"-|;|,|/|(|)|[|]|}|{|%|@|*|!|'";
    private const string StrRegex = @"--|'|;|%";

    /// <summary>
    /// 检查URL参数中是否带有SQL注入的可能关键字。
    /// </summary>
    /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
    public static bool CheckRequestQuery()
    {
        bool result = false;
        if (HttpContext.Current.Request.QueryString.Count != 0)
        {
            //若URL中参数存在，则逐个检验参数。
            foreach (string queryName in HttpContext.Current.Request.QueryString)
            {
                //过虑一些特殊的请求状态值,主要是一些有关页面视图状态的参数
                if (queryName == "__VIEWSTATE" || queryName == "__EVENTVALIDATION")
                    continue;
                //开始检查请求参数值是否合法
                if (CheckKeyWord(HttpContext.Current.Request.QueryString[queryName]))
                {
                    //只要存在一个可能出现Sql注入的参数,则直接退出
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    /// <summary>
    /// 检查提交表单中是否存在SQL注入的可能关键字
    /// </summary>
    /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
    public static bool CheckRequestForm()
    {
        bool result = false;
        if (HttpContext.Current.Request.Form.Count > 0)
        {
            //若获取提交的表单项个数不为0,则逐个比较参数
            foreach (string queryName in HttpContext.Current.Request.Form)
            {
                //过虑一些特殊的请求状态值,主要是一些有关页面视图状态的参数
                if (queryName == "__VIEWSTATE" || queryName == "__EVENTVALIDATION")
                    continue;

                if (queryName.IndexOf("RadTextBox") == -1
                    && queryName.IndexOf("TextBox") == -1
                    && queryName.IndexOf("rdtxt") == -1
                    && queryName.IndexOf("RadTxt") == -1

                    )
                {
                    continue;
                }
                //开始检查提交的表单参数值是否合法
                if (CheckKeyWord(HttpContext.Current.Request.Form[queryName].ToLower()))
                {
                    //只要存在一个可能出现Sql注入的参数,则直接退出
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    /// <summary>
    /// 检查_sword是否包涵SQL关键字
    /// </summary>
    /// <param name="_sWord">需要检查的字符串</param>
    /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
    public static bool CheckKeyWord(string _sWord)
    {
        bool result = false;
        //模式1 : 对应Sql注入的可能关键字
        string[] patten1 = StrKeyWord.Split('|');
        //模式2 : 对应Sql注入的可能特殊符号
        string[] patten2 = StrRegex.Split('|');
        //开始检查 模式1:Sql注入的可能关键字 的注入情况
        foreach (string sqlKey in patten1)
        {
            if (_sWord.IndexOf(" " + sqlKey) >= 0 || _sWord.IndexOf(sqlKey + " ") >= 0)
            {
                //只要存在一个可能出现Sql注入的参数,则直接退出
                result = true;
                break;
            }
        }
        //开始检查 模式1:Sql注入的可能特殊符号 的注入情况
        foreach (string sqlKey in patten2)
        {
            if (_sWord.IndexOf(sqlKey) >= 0)
            {
                //只要存在一个可能出现Sql注入的参数,则直接退出
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 执行Sql注入验证
    /// </summary>
    /// <returns>返回false表示有非法字符，返回true表示无sql注入</returns>
    public static bool CheckSQLParam()
    {
        if (CheckRequestQuery() || CheckRequestForm())
        {
            return false;
        }
        return true;
    }

    #endregion SQL注入检查

    /// <summary>    
    /// 取得客户端真实IP。如果有代理则取第一个非内网地址    
    /// </summary>    
    public static string GetIPAddress()
    {

        string result = String.Empty;

        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (result != null && result != String.Empty)
        {
            //可能有代理    
            if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式    
                result = null;
            else
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。    
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址    
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式    
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP    
            }
        }

        string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (null == result || result == String.Empty)
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (result == null || result == String.Empty)
            result = HttpContext.Current.Request.UserHostAddress;

        return result;
    }
    /// <summary>   
    /// 判断是否是IP地址格式 0.0.0.0   
    /// </summary>   
    /// <param name="str1">待判断的IP地址</param>   
    /// <returns>true or false</returns>   
    public static bool IsIPAddress(string str1)
    {
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

        string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regformat, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }

    #region 造价工程师

    /// <summary>
    /// 初始化造价工程师可后退到的审批节点集合。
    /// </summary>
    /// <param name="rcb">审批节点下拉框（必须按审批步骤升序排列）</param>
    /// <param name="ApplyStatus">当前申请单所处节点状态</param>
    public static void SetZJSReturnStatusList(RadComboBox rcb, string ApplyStatus)
    {
        int ItemCount = rcb.Items.Count;
        for (int i = ItemCount; i > 0; i--)
        {
            if (rcb.Items[i - 1].Value == ApplyStatus)
            {
                rcb.Items.Remove(i - 1);
                break;
            }
            else if (rcb.Items[i - 1].Value == EnumManager.ZJSApplyStatus.已申报 && ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)
            {
                continue;
            }
            else if (rcb.Items[i - 1].Value != ApplyStatus)
            {
                rcb.Items.Remove(i - 1);
            }
        }
    }

    /// <summary>
    /// 初始化建造师可后退到的审批节点集合。
    /// </summary>
    /// <param name="rcb">审批节点下拉框（必须按审批步骤升序排列）</param>
    /// <param name="ApplyStatus">当前申请单所处节点状态</param>
    public static void SetJZSReturnStatusList(RadComboBox rcb, string ApplyStatus)
    {
        int ItemCount = rcb.Items.Count;
        for (int i = ItemCount; i > 0; i--)
        {
            if (rcb.Items[i - 1].Value == ApplyStatus)
            {
                rcb.Items.Remove(i - 1);
                break;
            }
            else if (rcb.Items[i - 1].Value == EnumManager.ApplyStatus.已申报 && ApplyStatus == EnumManager.ApplyStatus.已驳回)
            {
                continue;
            }
            else if (rcb.Items[i - 1].Value != ApplyStatus)
            {
                rcb.Items.Remove(i - 1);
            }
        }
    }

    #endregion
}

