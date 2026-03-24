<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifConfirmP.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifConfirmP" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function returnToParent(startTime, endTime) {
                var oArg = new Object();
                oArg.startTime = startTime;
                oArg.endTime = endTime;
                var oWnd = GetRadWindow();
                oWnd.BrowserWindow.refreshGrid();
                oWnd.close(oArg);
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function rtn() {
                var calendarStart = $find("<%= RadDatePicker1.ClientID %>");
                var startDate = calendarStart.get_textBox().value;
                var calendarEnd = $find("<%= RadDatePicker2.ClientID %>");
                var endDate = calendarEnd.get_textBox().value;
                returnToParent(startDate, endDate);
                //returnToParent($find("<%= RadDatePicker1.ClientID %>").get_selectedDate(), $find("<%= RadDatePicker2.ClientID %>").get_selectedDate());
            }       
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="table_border" style="width: 90%; margin: 0 auto;">
            <div class="content">
                <div>
                    <table style="font-size: 12px;" cellpadding="5" width="90%" align="center">
                        <tr>
                            <td width="20%" nowrap="nowrap">
                                新有效期起始时间：
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="RadDatePicker1" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                新有效期截止时间：
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="RadDatePicker2" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <input id="Button2" type="button" value="确 定" class="button" onclick="rtn();" />
                                <input id="Button1" type="button" value="取 消" class="button" onclick="javascript:GetRadWindow().close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
