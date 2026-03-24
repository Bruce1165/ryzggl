<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="AcceptLineManage.aspx.cs" Inherits="ZYRYJG.EXamManage.AcceptLineManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ButtonOutput") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" OnClientClose="OnClientClose"
        VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
        EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamSubjectResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="spanSetPassLine" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="div_tip" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Skin="Windows7" runat="server"
        Visible="true" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试管理 &gt;&gt; <strong>合格线管理</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx" runat="server" id="DivSearch">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">考试计划：
                    </td>
                    <td align="left" width="33%">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">参考线1：
                    </td>
                    <td align="left" id="tdPassLine" runat="server" width="45%">
                        <div class="RadPicker">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxLine1" runat="server" MaxLength="3"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="50px"
                                Value="50">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                        </div>
                        <div class="RadPicker">
                            &nbsp;&nbsp;&nbsp; 参考线2：
                        </div>
                        <div class="RadPicker">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxLine2" runat="server" MaxLength="3"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="50px"
                                Value="55">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                        </div>
                        <div class="RadPicker">
                            &nbsp;&nbsp;&nbsp; 参考线3：
                        </div>
                        <div class="RadPicker">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxLine3" runat="server" MaxLength="3"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="50px"
                                Value="60">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                统计结果列表
            </div>
            <div style="width: 98%; margin: 0 auto; overflow: auto;">
                <telerik:RadGrid ID="RadGridExamSubjectResult" AutoGenerateColumns="False" runat="server"
                    AllowPaging="false" PageSize="1" Skin="Blue" EnableAjaxSkinRendering="false"
                    ExportSettings-FileName="PassRateReport" EnableEmbeddedSkins="false" Width="99%"
                    GridLines="None" OnItemCreated="RadGridExamSubjectResult_ItemCreated" OnExcelExportCellFormatting="RadGridExamSubjectResult_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                        </Columns>
                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <br />
            <div style="width: 90%; margin: 0 auto; text-align: center; font-size: 12px;">
                <div id="div_tip" runat="server" style="width: 90%; line-height: 300%; color: Red; font-size:16px; text-align: center; padding-left: 20px;">
                </div>
                <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click"
                    Enabled="False" />
                &nbsp;&nbsp; <span id="spanSetPassLine" runat="server">
                    <input type="button" class="bt_large" onclick="if (get_ExamPlanID() == '') { OpenAlert('请选择一个考试！'); return false; } location = 'AcceptLineSet.aspx?o=' + get_ExamPlanID();"
                        value="设定合格线" /></span>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
