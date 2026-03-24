<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateNoChange.aspx.cs" Inherits="ZYRYJG.Register.CertificateNoChange" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />     
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .infoHead {
            width: 20%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
            border-collapse: collapse;
        }

        .formItem_1 {
            width: 30%;
            text-align: left;
            vertical-align: top;
        }

            .formItem_1 input {
                border: none;
                line-height: 150%;
            }

        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }
        .red{color:red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
        <telerik:radwindowmanager id="RadWindowManager1" runat="server" skin="Windows7">
        </telerik:radwindowmanager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;编号管理 &gt;&gt;<strong>更换作废</strong>
                </div>
            </div>
            <div class="content">
                <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="0" class="table" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">人员基本信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">姓名：</td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Name" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="infoHead">性别：</td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Sex" runat="server" CssClass="textEdit" MaxLength="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">出生日期：</td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_BirthDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                        </td>
                        <td class="infoHead">名族：</td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_National" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">证件类别：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_CertificateType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">证件号码：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业院校：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_GraduationSchool" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">所学专业：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Specialty" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">学历：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Qualification" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">学位：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Degree" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">毕业时间：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_GraduationTime" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                        </td>
                        <td class="infoHead">电子邮件：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Email" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">手机：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="infoHead">联系电话：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_Telephone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">证书信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">注册类别：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_RegisteType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">注册号：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                       
                        <td class="infoHead">注册专业：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_RegisteProfession" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="infoHead">注册审批日期
                        </td>
                        <td class="formItem_1" colspan="3">
                            <asp:TextBox ID="TextBoxPSN_RegistePermissionDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">发证日期：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_CertificationDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                        </td>
                        <td class="infoHead">证书有效期：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_CertificateValidity" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                     <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">证书编号</td>
                    </tr>
                    <tr class="GridLightBK" >
                         <td class="infoHead red">原证书编号：
                        </td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxPSN_RegisterCertificateNo" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                         <td class="infoHead red">新证书编号：
                        </td>
                        <td class="formItem_1" >
                            <asp:TextBox ID="TextBoxPSN_RegisterCertificateNoNew" runat="server" CssClass="textEdit" MaxLength="50" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK" >
                         <td colspan="4" align="center">
                             <asp:Button ID="ButtonChange" runat="server" Text="确 定" CssClass="bt_large" OnClick="ButtonChange_Click" />
                        </td>
                       
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
