<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertChangeTestify.aspx.cs" Inherits="ZYRYJG.CertifManage.CertChangeTestify" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 电子证书下载 &gt;&gt; <strong>离京证明</strong>
                </div>
            </div>
            <div id="DivDetail" runat="server" style="padding: 12px; line-height: 150px; font-size: 22px; text-align:center;min-height:150px;vertical-align:middle">
            </div>
            <div style="width: 95%; margin: 0px auto 50px auto; text-align: center;">
                <asp:Button ID="ButtonDownPdf" runat="server" Text="下载原始电子证书" CssClass="bt_large" OnClick="ButtonDownPdf_Click" Visible="false" /> <asp:Button ID="ButtonDownDoc" runat="server" Text="下载离京证明" CssClass="bt_large" style="margin-left:40px" OnClick="ButtonDownDoc_Click" />
                <input id="ButtonReturn" type="button" value="返 回" class="bt_large" onclick="javascript: hideIfam();"  style="margin-left:40px" />
            </div>
        </div>
    </form>
</body>
</html>
