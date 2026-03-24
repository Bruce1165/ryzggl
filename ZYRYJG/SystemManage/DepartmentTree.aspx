<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentTree.aspx.cs"
    Inherits="ZYRYJG.SystemManage.DepartmentTree" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        html {
            scrollbar-shadow-color: #ffffff;
            scrollbar-highlight-color: #ffffff;
            scrollbar-face-color: #d9d9d9;
            scrollbar-3dlight-color: #d9d9d9;
            scrollbar-darkshadow-color: #d9d9d9;
            scrollbar-track-color: #ffffff;
            scrollbar-arrow-color: #ffffff;
        }
    </style>
    <script type="text/javascript">
        function hightlyAdapterPages(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).height = centertdHeight;
        }
        function hightlyAdapterPages1(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).style.minHeight = centertdHeight + "px";
            document.getElementById(tdID).style.height = centertdHeight + "px";
        }
        function hightlyAdapterPages2(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).style.minHeight = centertdHeight + "px";
        }
        function CheckBrowser() {
            var cb = "Unknown";
            if (window.ActiveXObject) {
                cb = "IE";
            } else if (navigator.userAgent.toLowerCase().indexOf("firefox") != -1) {
                cb = "Firefox";
            } else if ((typeof document.implementation != "undefined") && (typeof document.implementation.createDocument != "undefined") && (typeof HTMLDocument != "undefined")) {
                cb = "Mozilla";
            } else if (navigator.userAgent.toLowerCase().indexOf("opera") != -1) {
                cb = "Opera";
            }
            return cb;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="border_tree" id="center_td">
            <script language="javascript" type="text/javascript">
            hightlyAdapterPages("center_td", 20);
            </script>
            <div style="margin: 0; overflow: auto; padding-left: 5px;height:500px; overflow:scroll;" id="center_td1">
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
                </telerik:RadAjaxLoadingPanel>
                <telerik:RadTreeView ID="RadTreeView1" runat="server" Skin="Simple"  OnNodeExpand="RadTreeView1_NodeExpand" >
                </telerik:RadTreeView>
                <script type="text/javascript" charset="gb2312">
                var browser = CheckBrowser();
                if (browser == "IE") {
                    hightlyAdapterPages1("center_td1", 15);
                    hightlyAdapterPages1("RadTreeView1", 15);
                } else {
                    hightlyAdapterPages2("center_td1", 15);
                }
                </script>
            </div>
        </div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadTreeView1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadTreeView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
</body>
</html>