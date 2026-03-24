<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertModify.aspx.cs" Inherits="ZYRYJG.SystemManage.CertModify" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;建造师注册管理&gt;&gt;注册证书管理 &gt;&gt;<strong>信息修正</strong>
                </div>
            </div>
            <div class="content">
                <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="0" class="EditTable" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="5" class="barTitle">人员基本信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">姓名：</td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Name" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="infoHead">性别：</td>
                        <td class="formItem_2">
                            <telerik:RadComboBox ID="RadComboBoxPSN_Sex" runat="server" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="男" Value="男" />
                                    <telerik:RadComboBoxItem Text="女" Value="女" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td class="formItem_2" rowspan="5">
                            <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/tup.gif"
                                alt="一寸照片" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">出生日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPSN_BirthDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="ValidatorPSN_BirthDate" runat="Server" ControlToValidate="RadDatePickerPSN_BirthDate"
                                ErrorMessage="请输入出生日期" Display="Dynamic">*请输入出生日期</asp:RequiredFieldValidator>
                        </td>
                        <td class="infoHead">民族：</td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_National" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">证件类别：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_CertificateType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">证件号码：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业院校：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_GraduationSchool" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">所学专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Specialty" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">学历：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Qualification" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">学位：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Degree" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业时间：
                        </td>
                        <td class="formItem_2">
                            <%--<asp:TextBox ID="TextBoxPSN_GraduationTime" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>--%>
                            <telerik:RadDatePicker ID="RadDatePickerPSN_GraduationTime" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="ValidatorPSN_GraduationTime" runat="Server" ControlToValidate="RadDatePickerPSN_GraduationTime"
                                ErrorMessage="请输入毕业时间" Display="Dynamic">*请输入毕业时间</asp:RequiredFieldValidator>
                        </td>
                        <td class="infoHead">电子邮件：
                        </td>
                        <td class="formItem_2" colspan="2">
                            <asp:TextBox ID="TextBoxPSN_Email" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">手机：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="infoHead">联系电话：
                        </td>
                        <td class="formItem_2" colspan="2">
                            <asp:TextBox ID="TextBoxPSN_Telephone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="Table1" width="100%" border="0" cellpadding="5" cellspacing="0" class="EditTable" style="text-align: center;">

                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">证书信息</td>
                    </tr>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册证书编号：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_RegisterCertificateNo" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">注册号：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td class="infoHead">发证日期：
                        </td>
                        <td class="formItem_2">
                            <%-- <asp:TextBox ID="TextBoxPSN_CertificationDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>--%>
                            <telerik:RadDatePicker ID="RadDatePickerPSN_CertificationDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="ValidatorPSN_CertificationDate" runat="Server" ControlToValidate="RadDatePickerPSN_CertificationDate"
                                ErrorMessage="请输入发证日期" Display="Dynamic">*请输入发证日期</asp:RequiredFieldValidator>

                        </td>
                        <td class="infoHead">证书有效期：
                        </td>
                        <td class="formItem_2">
                            <%-- <asp:TextBox ID="TextBoxPSN_CertificateValidity" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>--%>

                            <asp:TextBox ID="TextBoxPSN_CertificateValidity" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>

                        </td>
                    </tr>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_RegisteProfession" runat="server" CssClass="textEdit" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">单位名称：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxENT_Name" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td class="infoHead">注册类别：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_RegisteType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">注册审批日期
                        </td>
                        <td class="formItem_2">
                            <%--<asp:TextBox ID="TextBoxPSN_RegistePermissionDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>--%>
                            <telerik:RadDatePicker ID="RadDatePickerPSN_RegistePermissionDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="ValidatorPSN_RegistePermissionDate" runat="Server" ControlToValidate="RadDatePickerPSN_RegistePermissionDate"
                                ErrorMessage="请输入注册审批日期" Display="Dynamic">*请输入注册审批日期</asp:RequiredFieldValidator>

                        </td>
                    </tr>



                </table>

                <table runat="server" id="Table2" width="100%" border="0" cellpadding="5" cellspacing="0" class="EditTable" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="6" class="barTitle">专业信息</td>
                    </tr>
                    <%--水利--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionSL" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="aformItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginSL" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndSL" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                                <DateInput DisplayDateFormat="yyyy-MM-dd" DateFormat="yyyy-MM-dd"></DateInput>

                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <%-- 建筑--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionJZ" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginJZ" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndJZ" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <%-- 机电--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionJD" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginJD" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndJD" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <%-- 公路--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionGL" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginGL" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndGL" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <%-- 市政--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionSZ" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginSZ" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndSZ" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <%--矿业--%>
                    <tr class="GridLightBK">

                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPRO_ProfessionKY" runat="server" CssClass="textEdit" MaxLength="50" Width="141px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="infoHead">有效期起始日期：
                        </td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBeginKY" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="infoHead">有效期截止日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEndKY" runat="server" MinDate="1900-01-01" MaxDate="2050-01-01" Width="130px" DateFormat="yyyy-MM-dd" Culture="zh-CN" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>



                    <tr class="GridLightBK">
                        <td colspan="6" align="center">
                            <asp:Button ID="Button1" runat="server" Text="修 正" CssClass="bt_large" OnClick="ButtonSave_Click" />
                        </td>

                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
