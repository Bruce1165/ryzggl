<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="CertifConfirmTop.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifConfirmTop" %>

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
            function a() {
            
                //returnToParent($find("<%= RadDatePicker1.ClientID %>").get_selectedDate(), $find("<%= RadDatePicker2.ClientID %>").get_selectedDate());
            }       
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div class="table_border" style="width: 98%; margin: 8px auto;">
            <div class="content">
                <div>
                    <table style="line-height: 30px; padding-top: 20px; width: 90%;">
                        <tr>
                            <td>
                                新有效期起始时间：
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker1" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                新有效期截止时间：
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="padding: 20px 0px 100px 0px;">
                            <input id="Button2" type="button" value="确定" class="button" onclick="a();" />
                                <input id="Button1" type="button" value="取消" class="button" onclick="javascript:GetRadWindow().close();" />
                                <%--<input id="Button2" type="button" value="确定" class="button" onclick="javascript:returnToParent(document.getElementById('<%=RadDatePicker1.ClientID %>').text, document.getElementById('<%=RadDatePicker2.ClientID %>').text);" />
                                <input id="Button1" type="button" value="取消" class="button" onclick="javascript:GetRadWindow().close();" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>

