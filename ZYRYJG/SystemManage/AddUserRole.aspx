<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUserRole.aspx.cs" Inherits="ZYRYJG.SystemManage.AddUserRole" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ButtonClose").click(function () {
                var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                parent.layer.close(index); //再执行关闭
                parent.location.reload();
            })                                                   
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div>
            <table width="98%">
                <tr>
                    <td style="width: 30%; text-align: right">
                        <font color="red">*</font> 机构名称：
                    </td>
                    <td style="width: 70%; text-align: left">
                        <telerik:RadComboBox ID="RadComboBoxOrganID" runat="server" Width="97%" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxOrganID_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; text-align: right">
                        <font color="red">*</font> 部门：
                    </td>
                    <td style="width: 70%; text-align: left">
                        <telerik:RadComboBox ID="RadComboBoxDepartment" runat="server" Width="97%"></telerik:RadComboBox>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 30%; text-align: right">
                        <font color="red">*</font> 角色：
                    </td>
                    <td style="width: 70%; text-align: left">
                        <telerik:RadComboBox ID="RadComboBoxRole" runat="server" Width="97%"></telerik:RadComboBox>
                    </td>
                </tr>
                  <tr>
                    <td colspan="2" style="text-align:center">
                     <asp:Button ID="ButtonSave" runat="server" Text="确 定" CssClass="button" OnClick="ButtonSave_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="ButtonClose"  type="button" value="关 闭" class="button" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
