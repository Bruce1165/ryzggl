<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsCertModify.aspx.cs" Inherits="ZYRYJG.zjs.zjsCertModify" %>

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
        .infoHead {
            width: 15%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
            border-collapse: collapse;
        }

        .formItem_2 {
            width: 35%;
            text-align: left;
            vertical-align: top;
        }

        .formItem_2 {
            text-align: left;
            vertical-align: top;
        }

        .formItem_3 {
            text-align: center;
            vertical-align: middle;
            width: 110px;
        }

        .formItem_2 input {
            line-height: 150%;
        }

        .formItem_2 input {
            line-height: 150%;
        }

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
                    当前位置 &gt;&gt;二级造价工程师注册 &gt;&gt;注册证书管理 &gt;&gt;<strong>证书信息修正</strong>
                </div>
            </div>
            <div class="content">
                <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="0" class="table" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="5" class="barTitle">人员基本信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">姓名：</td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Name" runat="server" CssClass="textEdit" MaxLength="100" Width="98%"></asp:TextBox>
                        </td>
                        <td class="infoHead">性别：</td>
                        <td class="formItem_2">
                            <telerik:RadComboBox ID="RadComboBoxPSN_Sex" runat="server" Width="98%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="男" Value="男" />
                                    <telerik:RadComboBoxItem Text="女" Value="女" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td class="formItem_3" rowspan="6">
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
                            <telerik:RadComboBox ID="RadComboBoxPSN_National" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxPSN_National"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">证件类别：
                        </td>
                        <td class="formItem_2">
                            <asp:Label ID="LabelPSN_CertificateType" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="infoHead">证件号码：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" MaxLength="50" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业院校：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_GraduationSchool" runat="server" CssClass="textEdit" MaxLength="50" Width="98%"></asp:TextBox>
                        </td>
                        <td class="infoHead">所学专业：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Specialty" runat="server" CssClass="textEdit" MaxLength="50" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业时间：
                        </td>
                        <td class="formItem_2">

                            <telerik:RadDatePicker ID="RadDatePickerPSN_GraduationTime" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="ValidatorGraduationTime" runat="Server" ControlToValidate="RadDatePickerPSN_GraduationTime"
                                ErrorMessage="请输入姓名" Display="Dynamic">*请输入毕业（肄、结）时间</asp:RequiredFieldValidator>
                        </td>
                        <td class="infoHead">学历：
                        </td>
                        <td class="formItem_2">

                            <telerik:RadComboBox ID="RadComboBoxPSN_Qualification" runat="server" Width="98%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxPSN_Qualification"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator3" ForeColor="Red" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">手机：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" MaxLength="100" Width="98%"></asp:TextBox>
                        </td>
                        <td class="infoHead">电子邮件：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_Email" runat="server" CssClass="textEdit" MaxLength="200" Width="98%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td colspan="5" class="barTitle">企业基本信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">企业名称：
                        </td>
                        <td class="formItem_2" colspan="4">
                            <asp:TextBox ID="TextBoxENT_Name" runat="server" CssClass="textEdit" MaxLength="200" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">工商注册地：
                        </td>
                        <td class="formItem_2" colspan="4">
                            <asp:TextBox ID="TextBoxEND_Addess" runat="server" CssClass="textEdit" MaxLength="200" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">机构社会统一信用代码：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" MaxLength="200" Width="98%"></asp:TextBox>
                        </td>
                        <td class="infoHead">隶属区县：
                        </td>
                        <td class="formItem_2" colspan="2">
                            <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                    <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                    <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                    <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                    <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                    <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                    <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                    <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                    <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                    <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                    <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                    <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                    <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                    <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                    <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                    <telerik:RadComboBoxItem Text="密云区" Value="密云区" />
                                    <telerik:RadComboBoxItem Text="延庆区" Value="延庆区" />
                                    <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="5" class="barTitle">注册证书信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">注册号：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" MaxLength="50" Width="98%"></asp:TextBox>
                        </td>
                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_2" colspan="2">
                            <telerik:RadComboBox ID="RadComboBoxPSN_RegisteProfession" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="土木建筑工程" Value="土木建筑工程" />
                                    <telerik:RadComboBoxItem Text="安装工程" Value="安装工程" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">发证日期：
                        </td>
                        <td class="formItem_2">

                            <telerik:RadDatePicker ID="RadDatePickerPSN_CertificationDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ControlToValidate="RadDatePickerPSN_CertificationDate"
                                ErrorMessage="请输入发证日期" Display="Dynamic">*请输入发证日期</asp:RequiredFieldValidator>
                        </td>
                        <td class="infoHead">证书有效期：
                        </td>
                        <td class="formItem_2" colspan="2">

                            <telerik:RadDatePicker ID="RadDatePickerPSN_CertificateValidity" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server" ControlToValidate="RadDatePickerPSN_CertificateValidity"
                                ErrorMessage="请输入证书有效期" Display="Dynamic">*请输入证书有效期</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">注册类别：
                        </td>
                        <td class="formItem_2">
                            <asp:Label ID="LabelPSN_RegisteType" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="infoHead">注册审批日期
                        </td>
                        <td class="formItem_2" colspan="2">

                            <telerik:RadDatePicker ID="RadDatePickerPSN_RegistePermissionDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Width="130px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="Server" ControlToValidate="RadDatePickerPSN_RegistePermissionDate"
                                ErrorMessage="请输入注册审批日期" Display="Dynamic">*请输入注册审批日期</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 95%; margin: 10px auto; text-align: center;">
                <asp:Button ID="ButtonSave" runat="server" Text="修 正" CssClass="button" OnClick="ButtonSave_Click" />&nbsp;&nbsp;<input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" /></div>
        </div>
    </form>
</body>
</html>
