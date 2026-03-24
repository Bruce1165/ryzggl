<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="menu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="./Content/menu.css?v=1.001" rel="stylesheet" type="text/css" />
    <link href="./Content/Menu.Sitefinity.css?v=1.001" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="Singleton" Skin="Windows7" Width="400" Height="430" VisibleStatusbar="false"
            Behaviors="Close,Move, Resize" runat="server">
            <AlertTemplate>
                <div class="alertText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                        value="确 定" />
                </div>
            </AlertTemplate>
            <ConfirmTemplate>
                <div class="confrimText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                        value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
                </div>
            </ConfirmTemplate>
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>

        <div class="divMenu">
            <asp:Image ID="ImageFace" runat="server" ImageUrl="~/Img/photo.jpg" CssClass="faceimg" />
            <div class="worker" runat="server" id="divWorker"></div>
            <telerik:RadMenu ID="RadMenu1" runat="server" Width="100%" Skin="Sitefinity" Flow="Vertical" EnableAjaxSkinRendering="true"
                EnableEmbeddedSkins="false">
            </telerik:RadMenu>
            <div style="clear: both;min-height:200px;"></div>
        </div>

    </form>
</body>
</html>
