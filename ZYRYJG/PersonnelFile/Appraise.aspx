<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appraise.aspx.cs" Inherits="ZYRYJG.PersonnelFile.Appraise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.126" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%; text-align:center">
            <div id="divInfo" runat="server" style="margin:80px 20%;font-size:22px; text-align:left;line-height:180%"></div>
            <div>
                <asp:Button ID="ButtonPingJia" runat="server" Text="我要评价" OnClick="ButtonPingJia_Click" CssClass="bt_large"/><input id="Button1" type="button" class="bt_large" value="不要评价" onclick="javascript: window.close();" style="margin-left:40px" />
            </div>
        </div>
    </form>
</body>
</html>
