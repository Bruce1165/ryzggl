<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PMCertPrint.aspx.cs" Inherits="ZYRYJG.EXamManage.PMCertPrint" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out" style="width: 98%; margin: 1px auto;">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书打印（喷墨）
            </div>
        </div>
    </div>
    <%--<div class="table_border" style="width: 98%; margin: 2px auto;">
        <p class="jbxxbt" style="text-align: center">
            证书打印（喷墨）
        </p>
        <div style="width: 95%; margin: 2px auto; text-align: center;">            
            <div runat="server" id="P_PrintTimeSpan" style="width: 18.2cm; padding-bottom: 8px; text-align: center;">
                <div style="float: left;">
                    <asp:CheckBox ID="CheckBoxAutoPrint" runat="server" Text="自动完成批量打印" TextAlign="Left" />&nbsp;&nbsp;&nbsp;时间间隔：
                </div>
                <div style="float: left;">
                    <asp:RadioButtonList ID="RadioButtonListPrintTimeSpan" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="5秒（默认）" Value="5" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="10秒" Value="10秒"></asp:ListItem>
                        <asp:ListItem Text="20秒" Value="20"></asp:ListItem>
                        <asp:ListItem Text="30秒" Value="30"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <asp:Button ID="ButtonPrev" runat="server" Text="上一条" CssClass="button" Visible="False"
                OnClick="ButtonPrev_Click" />
            &nbsp;&nbsp;
                    <asp:Button ID="ButtonPrint" runat="server" Text="打 印" CssClass="button" OnClick='ButtonPrint_Click'
                        ToolTip="提示：直接按回车键（Enter）也可打印" />
            &nbsp;&nbsp;
                    <asp:Button ID="ButtonNext" runat="server" Text="下一条" CssClass="button" Visible="False"
                        OnClick="ButtonNext_Click" />
            &nbsp;&nbsp;
                    <asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="button" OnClick="ButtonReturn_Click" />
        </div>
        <div style="width: 95%; margin: 10px auto; text-align: center;">
            <asp:Label ID="LabelPage" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
        </div>

        <div id="divPhoto" runat="server" style="width: 18.2cm; margin: 0; text-align: left; vertical-align: top;">
        </div>
        <div style="clear: both; height: 30px;"></div>
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="10000"
            EnableViewState="true">
        </asp:Timer>
    </div>--%>
</asp:Content>
