<%@ Page Title="设定证书有效期" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifRegistyPop.aspx.cs" Inherits="ZYRYJG.EXamManage.CertifRegistyPop" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function returnToParent(startTime, endTime, ConferDate) {
                var oArg = new Object();
                oArg.startTime = startTime;
                oArg.endTime = endTime;
                oArg.ConferDate = ConferDate;
                var oWnd = GetRadWindow();
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
                var calendarConferDate = $find("<%= RadDatePickerConferDate.ClientID %>");
                var ConferDate = calendarConferDate.get_textBox().value;

                var startDate1 = startDate.replace(/-/g, "/");
                var endDate1 = endDate.replace(/-/g, "/");
                if (Date.parse(startDate1) - Date.parse(endDate1) > 0) {
                    alert("有效期起始时间不能大于有效期截止时间!");
                    return false;
                }
                else {
                    returnToParent(startDate, endDate, ConferDate);
                }
            }       
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="jbxxbt">
        设定证书有效期</div>
    <div class="div_out">
        <div class="table_border" style="width: 98%; margin: 0px auto;">
            <div class="content">
                <div>
                    <table style="line-height: 30px; width: 90%;">
                        <tr>
                            <td>
                                证书发证时间：
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="120px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                有效期起始时间：
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker1" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                有效期截止时间：
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="padding: 20px 0px 100px 0px;">
                                <input id="ButtonCheck" type="button" value="确 定" class="button" onclick="rtn();" />
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
